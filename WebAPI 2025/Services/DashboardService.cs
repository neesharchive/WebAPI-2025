using Microsoft.EntityFrameworkCore;
using WebAPI_2025.Data;
using WebAPI_2025.DTOs.DashboardDTO;
using WebAPI_2025.Enums;
using WebAPI_2025.Models.Wrappers;

namespace WebAPI_2025.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly AppDbContext _context;

        public DashboardService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<APIResponse<DashboardStatsDTO>> GetAdminStatsAsync()
        {
            var today = DateTime.Today;

            var reservations = await _context.bookings.CountAsync();
            var pending = await _context.bookings.CountAsync(b => b.Status == BookingStatus.Pending);
            var checkIns = await _context.bookings.CountAsync(b => b.Status == BookingStatus.Approved && b.CheckInDate.Date == today);
            var checkOuts = await _context.bookings.CountAsync(b => b.Status == BookingStatus.Approved && b.CheckOutDate.Date == today);

            var bookedBeds = await _context.bookings
                .Where(b => b.Status == BookingStatus.Approved && b.CheckInDate <= today && b.CheckOutDate >= today)
                .Select(b => b.BedID)
                .ToListAsync();

            var availableRooms = await _context.rooms
                .Where(r => _context.beds.Any(b => b.RoomID == r.RoomID && !bookedBeds.Contains(b.BedID)))
                .CountAsync();

            var dto = new DashboardStatsDTO
            {
                AvailableRooms = availableRooms,
                Reservations = reservations,
                PendingRequests = pending,
                CheckIns = checkIns,
                CheckOuts = checkOuts
            };

            return new APIResponse<DashboardStatsDTO>(true, "Dashboard stats fetched", dto);
        }

        public async Task<APIResponse<BedStatsDTO>> GetBedStatsAsync(DateTime startDate, DateTime endDate)
        {
            var today = DateTime.Today;
            var isDefaultOpenRange = startDate <= DateTime.Today.AddYears(-20) && endDate == today;
            var totalBeds = await _context.beds.CountAsync();

            var relevantBookings = await _context.bookings
                .Where(b => b.Status == BookingStatus.Approved &&
                            b.CheckInDate <= endDate &&
                            b.CheckOutDate >= startDate)
                .ToListAsync();

            var occupiedBedIds = relevantBookings.Select(b => b.BedID).Distinct().Count();

            int checkIns;
            int totalNights;

            if (isDefaultOpenRange)
            {
                var allUpToToday = await _context.bookings
                    .Where(b => b.Status == BookingStatus.Approved && b.CheckInDate <= today)
                    .ToListAsync();

                checkIns = allUpToToday.Count;

                totalNights = allUpToToday.Sum(b =>
                {
                    var co = b.CheckOutDate > today ? today : b.CheckOutDate;
                    return (co - b.CheckInDate).Days;
                });
            }
            else
            {
                checkIns = relevantBookings.Count(b => b.CheckInDate >= startDate && b.CheckInDate <= endDate);

                totalNights = relevantBookings.Sum(b =>
                {
                    var ci = b.CheckInDate < startDate ? startDate : b.CheckInDate;
                    var co = b.CheckOutDate > endDate ? endDate : b.CheckOutDate;
                    return (co - ci).Days;
                });
            }

            var dto = new BedStatsDTO
            {
                AvailableBeds = totalBeds - occupiedBedIds,
                OccupiedBeds = occupiedBedIds,
                CheckIns = checkIns,
                TotalNights = totalNights
            };

            return new APIResponse<BedStatsDTO>(true, "Bed stats retrieved", dto);
        }

    }
}

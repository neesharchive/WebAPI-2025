using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebAPI_2025.Data;
using WebAPI_2025.DTOs.BookingDTO;
using WebAPI_2025.Enums;
using WebAPI_2025.Models.Entities;
using WebAPI_2025.Repositories;

namespace WebAPI_2025.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _repository;
        private readonly AppDbContext _appDbContext;
        private readonly IEmailService _emailService;
        private readonly INotificationService _notificationService;

        public BookingService(IBookingRepository bookingRepository, AppDbContext appDbContext,
            IEmailService emailService, INotificationService notificationService)
        {
            _repository = bookingRepository;
            _appDbContext = appDbContext;
            _emailService = emailService;
            _notificationService = notificationService;
        }

        public async Task Create(BookingDTO booking)
        {
            var entity = new Booking
            {
                UserID = booking.UserID,
                BedID = booking.BedID,
                CheckInDate = booking.CheckInDate,
                CheckOutDate = booking.CheckoutDate,
                Purpose = booking.Purpose,
                Gender = booking.Gender
            };

            await _repository.Create(entity);

            try
            {
                var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.UserID == booking.UserID);
                var bed = await _appDbContext.beds.FirstOrDefaultAsync(b => b.BedID == booking.BedID);

                if (user != null && bed != null)
                {
                    var room = await _appDbContext.rooms.FirstOrDefaultAsync(r => r.RoomID == bed.RoomID);
                    var guestHouse = await _appDbContext.guestHouses.FirstOrDefaultAsync(g => g.GuestHouseID == room.guesthouseID);

                    if (room != null && guestHouse != null)
                    {
                        string subject = "New Booking Request";
                        string body = $@"
  <div style='font-family: Arial, sans-serif; padding: 20px; background-color: #f4f4f4; border-radius: 8px; max-width: 600px; margin: auto; border: 1px solid #ccc;'>
    <h2 style='color: #2c974b;'>New Booking Request</h2>
    <p style='font-size: 16px; color: #333;'>
      User <strong>{user.UserName}</strong> has submitted a booking request.
    </p>
    <table style='width: 100%; border-collapse: collapse; font-size: 15px;'>
      <tr>
        <td style='padding: 8px; font-weight: bold;'>Guest House:</td>
        <td style='padding: 8px;'>{guestHouse.Name} ({guestHouse.Location})</td>
      </tr>
      <tr>
        <td style='padding: 8px; font-weight: bold;'>Room:</td>
        <td style='padding: 8px;'>Room {room.RoomNumber}, Bed {bed.BedNumber}</td>
      </tr>
      <tr>
        <td style='padding: 8px; font-weight: bold;'>Check-In:</td>
        <td style='padding: 8px;'>{booking.CheckInDate:yyyy-MM-dd}</td>
      </tr>
      <tr>
        <td style='padding: 8px; font-weight: bold;'>Check-Out:</td>
        <td style='padding: 8px;'>{booking.CheckoutDate:yyyy-MM-dd}</td>
      </tr>
      <tr>
        <td style='padding: 8px; font-weight: bold;'>Purpose:</td>
        <td style='padding: 8px;'>{booking.Purpose}</td>
      </tr>
    </table>
    <p style='margin-top: 20px; color: #666;'>Please review the request in your dashboard.</p>
  </div>";


                        _emailService.SendEmailInBackground("nishantbhatt393@gmail.com", subject, body);

                        await _notificationService.CreateNotification(new Notification
                        {
                            TargetRole = Roles.Admin,
                            Message = $"New booking request by {user.UserName} at {guestHouse.Name}, Room {room.RoomNumber}, Bed {bed.BedNumber}.",
                            Timestamp = DateTime.UtcNow,
                            IsRead = false
                        });

                        await _notificationService.CreateNotification(new Notification
                        {
                            UserID = user.UserID,
                            Message = "Your booking request was submitted successfully.",
                            Timestamp = DateTime.UtcNow,
                            IsRead = false
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Email Error] Could not send admin booking notification: {ex.Message}");
            }
        }


        public async Task Delete(int id)
        {
            var entity = await _repository.GetByID(id);
            if (entity != null)
            {
                await _repository.Delete(entity);
            }
        }

        public async Task<List<BookingDTO>> GetAll()
        {
            var result = await _appDbContext.bookings
    .Join(_appDbContext.beds, b => b.BedID, bed => bed.BedID, (b, bed) => new { b, bed })
    .Join(_appDbContext.rooms, bb => bb.bed.RoomID, room => room.RoomID, (bb, room) => new { bb.b, bb.bed, room })
    .Join(_appDbContext.guestHouses, bbr => bbr.room.guesthouseID, gh => gh.GuestHouseID, (bbr, gh) => new { bbr.b, bbr.bed, bbr.room, gh })
    .Join(_appDbContext.Users, bbrg => bbrg.b.UserID, u => u.UserID, (bbrg, u) => new BookingDTO
    {
        BookingID = bbrg.b.BookingID,
        UserID = bbrg.b.UserID,
        BedID = bbrg.b.BedID,
        CheckInDate = bbrg.b.CheckInDate,
        CheckoutDate = bbrg.b.CheckOutDate,
        Purpose = bbrg.b.Purpose,
        Gender = bbrg.b.Gender,
        Status = bbrg.b.Status,
        BedNumber = bbrg.bed.BedNumber,
        RoomNumber = bbrg.room.RoomNumber,
        GuestHouseName = bbrg.gh.Name,
        Location = bbrg.gh.Location
    })
    .ToListAsync();


            return result;
        }

        public async Task<List<BookingDTO>> GetBookingByUserId(int Uid)
        {
            var result = await _appDbContext.bookings
                .Where(en=>en.UserID == Uid)
                .Join(_appDbContext.beds, b => b.BedID, bed => bed.BedID, (b, bed) => new { b, bed })
                .Join(_appDbContext.rooms, bb => bb.bed.RoomID, room => room.RoomID, (bb, room) => new { bb.b, bb.bed, room })
                .Join(_appDbContext.guestHouses, bbr => bbr.room.guesthouseID, gh => gh.GuestHouseID, (bbr, gh) => new BookingDTO
                {
                    BookingID = bbr.b.BookingID,
                    UserID = bbr.b.UserID,
                    BedID = bbr.b.BedID,
                    CheckInDate = bbr.b.CheckInDate,
                    CheckoutDate = bbr.b.CheckOutDate,
                    Purpose = bbr.b.Purpose,
                    Gender = bbr.b.Gender,
                    Status = bbr.b.Status,
                    BedNumber = bbr.bed.BedNumber,
                    RoomNumber = bbr.room.RoomNumber,
                    GuestHouseName = gh.Name,
                    Location = gh.Location
                })
                .ToListAsync();
            return result;
        }

        public async Task<BookingDTO?> GetById(int id)
        {
            var result = await _appDbContext.bookings
               .Where(en => en.BookingID == id)
               .Join(_appDbContext.beds, b => b.BedID, bed => bed.BedID, (b, bed) => new { b, bed })
               .Join(_appDbContext.rooms, bb => bb.bed.RoomID, room => room.RoomID, (bb, room) => new { bb.b, bb.bed, room })
               .Join(_appDbContext.guestHouses, bbr => bbr.room.guesthouseID, gh => gh.GuestHouseID, (bbr, gh) => new BookingDTO
               {
                   BookingID = bbr.b.BookingID,
                   UserID = bbr.b.UserID,
                   BedID = bbr.b.BedID,
                   CheckInDate = bbr.b.CheckInDate,
                   CheckoutDate = bbr.b.CheckOutDate,
                   Purpose = bbr.b.Purpose,
                   Gender = bbr.b.Gender,
                   Status = bbr.b.Status,
                   BedNumber = bbr.bed.BedNumber,
                   RoomNumber = bbr.room.RoomNumber,
                   GuestHouseName = gh.Name,
                   Location = gh.Location
               }).FirstOrDefaultAsync();
            return result;
        }

        public async Task Update(int id, BookingUpdateDTO booking)
        {
            var entity = await _repository.GetByID(id);
            if (entity != null)
            {
                entity.UserID = booking.UserID;
                entity.BedID = booking.BedID;
                entity.CheckInDate = booking.CheckInDate;
                entity.CheckOutDate = booking.CheckoutDate;
                entity.Purpose = booking.Purpose;
                entity.Gender = booking.Gender;

                await _repository.Update(entity);

                await _notificationService.CreateNotification(new Notification
                {
                    TargetRole = Roles.Admin,
                    Message = $"User #{booking.UserID} updated a booking.",
                    Timestamp = DateTime.UtcNow,
                    IsRead = false
                });

                await _notificationService.CreateNotification(new Notification
                {
                    UserID = booking.UserID,
                    Message = "Your booking was updated successfully.",
                    Timestamp = DateTime.UtcNow,
                    IsRead = false
                });
            }
        }

        public async Task<bool> UpdateStatus(int bookingId, BookingStatus status)
        {
            var booking = await _appDbContext.bookings.FindAsync(bookingId);
            if (booking == null) return false;

            booking.Status = status;
            await _appDbContext.SaveChangesAsync();

            var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.UserID == booking.UserID);
            var bed = await _appDbContext.beds.FirstOrDefaultAsync(b => b.BedID == booking.BedID);
            var room = await _appDbContext.rooms.FirstOrDefaultAsync(r => r.RoomID == bed.RoomID);
            var guestHouse = await _appDbContext.guestHouses.FirstOrDefaultAsync(g => g.GuestHouseID == room.guesthouseID);

            if (user != null && bed != null && room != null && guestHouse != null)
            {
                string subject = "Your Booking Status Was Updated";
                string body = $@"
        <div style=""font-family: Arial, sans-serif; padding: 20px; background-color: #f5f5f5;"">
            <div style=""max-width: 600px; margin: auto; background: #ffffff; padding: 20px; border-radius: 8px; box-shadow: 0 2px 6px rgba(0,0,0,0.1);"">
                <h2 style=""color: #1a89c0;"">Booking Status Update</h2>
                <p style=""font-size: 15px;"">Dear <strong>{user.UserName}</strong>,</p>
                <p style=""font-size: 14px;"">
                    Your booking at <strong>{guestHouse.Name}</strong> 
                    (Room <strong>{room.RoomNumber}</strong>, Bed <strong>{bed.BedNumber}</strong>) 
                    from <strong>{booking.CheckInDate:yyyy-MM-dd}</strong> to <strong>{booking.CheckOutDate:yyyy-MM-dd}</strong> 
                    has been <span style=""color:{(status == BookingStatus.Approved ? "green" : status == BookingStatus.Rejected ? "red" : "#ffa500")}; font-weight: bold;"">{status}</span>.
                </p>
                <p style=""font-size: 14px;"">Thank you for using our Guest House Booking service.</p>
                <div style=""margin-top: 20px; font-size: 12px; color: #888;"">
                    — Roomy Guest House System
                </div>
            </div>
        </div>";

                _emailService.SendEmailInBackground(user.Email, subject, body);

                await _notificationService.CreateNotification(new Notification
                {
                    UserID = user.UserID,
                    Message = $"Your booking status has been updated to {status}.",
                    Timestamp = DateTime.UtcNow,
                    IsRead = false
                });
            }


            return true;
        }
    }
}

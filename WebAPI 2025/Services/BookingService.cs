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
        public BookingService(IBookingRepository bookingRepository, AppDbContext appDbContext)
        {
            _repository = bookingRepository;
            _appDbContext = appDbContext;
        }
        public async Task Create(BookingDTO booking)
        {
            var entitiy = new Booking()
            {
                UserID = booking.UserID,
                BedID = booking.BedID,
                CheckInDate = booking.CheckInDate,
                CheckOutDate = booking.CheckoutDate,
                Purpose = booking.Purpose,
                Gender = booking.Gender
            };
            await _repository.Create(entitiy);
        }

        public void Delete(int id)
        {
            var entity=_repository.GetByID(id).Result;
            if(entity != null)
            {
                _repository.Delete(entity);
            }
            
        }

        public async Task<List<BookingDTO>> GetAll()
        {
            var result = await _appDbContext.bookings
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

        public void Update(int id, BookingDTO booking)
        {
            var entity= _repository.GetByID(id).Result;
            if(entity != null)
            {
                entity.UserID = booking.UserID;
                entity.BedID = booking.BedID;
                entity.CheckInDate = booking.CheckInDate;
                entity.CheckOutDate = booking.CheckoutDate;
                entity.Purpose = booking.Purpose;
                entity.Gender = booking.Gender;
            };
        }
        public async Task<bool> UpdateStatus(int bookingId, BookingStatus status)
        {
            var booking = await _appDbContext.bookings.FindAsync(bookingId);
            if (booking == null) return false;

            booking.Status = status;
            await _appDbContext.SaveChangesAsync();
            return true;
        }

    }
}

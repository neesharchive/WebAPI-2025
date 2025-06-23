using AutoMapper;
using WebAPI_2025.Models;
using WebAPI_2025.DTOs.GuestHouseDTO;
using WebAPI_2025.Models.Entities;
using WebAPI_2025.DTOs.BedDTO;
using WebAPI_2025.DTOs.RoomDTO;
using WebAPI_2025.DTOs.BookingDTO;
using WebAPI_2025.DTOs.UserDTO;
using WebAPI_2025.DTOs.NotificationDTO;
namespace WebAPI_2025.Mappers
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<AddGuestHouseDTO, GuestHouse>();
            CreateMap<GuestHouse, GetGuestHouseDTO>();
            CreateMap<UpdateGuestHouseDTO, GuestHouse>();
            CreateMap<Bed, GetAvailableBedDTO>();
            CreateMap<Notification,NotificationDTO>();
            CreateMap<Room, GetAvailableRoomDTO>();
            CreateMap<BookingDTO, Booking>()
                .ForMember(dest => dest.BookingID, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());



        }
    }
}

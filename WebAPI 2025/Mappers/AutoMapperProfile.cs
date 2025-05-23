using AutoMapper;
using WebAPI_2025.Models;
using WebAPI_2025.DTOs.GuestHouseDTO;
using WebAPI_2025.Models.Entities;
using WebAPI_2025.DTOs.BedDTO;
using WebAPI_2025.DTOs.RoomDTO;
using WebAPI_2025.DTOs.BookingDTO;
namespace WebAPI_2025.Mappers
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<AddGuestHouseDTO, GuestHouse>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.GH_Name))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.GH_Location))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

            CreateMap<GuestHouse,GetGuestHouseDTO>()
                .ForMember(dest => dest.GH_Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.GH_Location, opt => opt.MapFrom(src => src.Location))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));


            CreateMap<Bed, GetAvailableBedDTO>()
                .ForMember(dest => dest.BedID, opt => opt.MapFrom(src => src.BedID))
                .ForMember(dest => dest.BedNumber, opt => opt.MapFrom(src => src.BedNumber));

            CreateMap<Room, GetAvailableRoomDTO>()
                .ForMember(dest => dest.RoomID, opt => opt.MapFrom(src => src.RoomID))
                .ForMember(dest => dest.RoomNumber, opt => opt.MapFrom(src => src.RoomNumber));

            CreateMap<BookingDTO, Booking>()
                .ForMember(dest => dest.BookingID, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

        }
    }
}

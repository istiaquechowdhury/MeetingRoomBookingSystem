using AutoMapper;
using MeetingRoomBooking.Domain.Entities;
using MeetingRoomBooking.Presentation.Areas.Admin.Models;
using System.Drawing;

namespace MeetingRoomBooking.Presentation
{
    public class WebProfile : Profile
    {
        public WebProfile()
        {
            CreateMap<MeetingRoomCreateModel, MeetingRoom>().ReverseMap();
            CreateMap<MeetingRoomUpdateModel, MeetingRoom>().ReverseMap();
            CreateMap<BookingCreateModel, Booking>().ReverseMap();
            CreateMap<BookingUpdateModel, Booking>().ReverseMap();












        }
    }
}

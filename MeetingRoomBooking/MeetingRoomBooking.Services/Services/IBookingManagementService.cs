using MeetingRoomBooking.Domain;
using MeetingRoomBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomBooking.Services.Services
{
    public interface IBookingManagementService
    {
        (IList<Booking> data, int total, int totaldisplay) GetBookings(int pageIndex, int pageSize, DataTablesSearch search, string? order);

        void CreateBooking(Booking booking);
        Booking GetBooking(Guid id);
        void UpdateBooking(Booking booking);
        void DeleteBooking(Guid id);
    }
}

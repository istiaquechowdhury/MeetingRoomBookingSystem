using MeetingRoomBooking.Domain;
using MeetingRoomBooking.Domain.Entities;
using MeetingRoomBooking.Domain.UnitOfWorkInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomBooking.Services.Services
{
    public class BookingManagementService : IBookingManagementService
    {
        private readonly IMeetingRoomUnitOfWork _MeetingUnitOfWork;


        public BookingManagementService(IMeetingRoomUnitOfWork MeetingUnitOfWork)
        {
            _MeetingUnitOfWork = MeetingUnitOfWork;
        }
        public void CreateBooking(Booking booking)
        {
            _MeetingUnitOfWork.Booking.Add(booking);    
            _MeetingUnitOfWork.Save();  
        }

        public Booking GetBooking(Guid id)
        {
           return _MeetingUnitOfWork.Booking.GetById(id); 
        }

        public void UpdateBooking(Booking booking)
        {
            _MeetingUnitOfWork.Booking.Edit(booking);   
            _MeetingUnitOfWork.Save();
        }

        public (IList<Booking> data, int total, int totaldisplay) GetBookings(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            return _MeetingUnitOfWork.Booking.GetPagedBooking(pageIndex, pageSize, search, order);
        }

        public void DeleteBooking(Guid id)
        {
           _MeetingUnitOfWork.Booking.Remove(id);
            _MeetingUnitOfWork.Save();  
        }
    }
}

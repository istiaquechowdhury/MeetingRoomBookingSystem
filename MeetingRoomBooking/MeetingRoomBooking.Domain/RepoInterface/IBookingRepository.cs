using MeetingRoomBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomBooking.Domain.RepoInterface
{
    public interface IBookingRepository : IRepositoryBase<Booking, Guid>
    {
    }
}

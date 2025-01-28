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
        (IList<Booking> data, int total, int totaldisplay) GetPagedBooking(int pageIndex, int pageSize, DataTablesSearch search, string? order);
    }
}

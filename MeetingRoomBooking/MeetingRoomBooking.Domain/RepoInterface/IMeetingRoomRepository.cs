using MeetingRoomBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomBooking.Domain.RepoInterface
{
    public interface IMeetingRoomRepository : IRepositoryBase<MeetingRoom, Guid>
    {
        (IList<MeetingRoom> data, int total, int totaldisplay) GetPagedMeetingRoom(int pageIndex, int pageSize, DataTablesSearch search, string? order);
    }
}

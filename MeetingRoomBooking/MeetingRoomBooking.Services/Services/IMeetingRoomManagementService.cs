using MeetingRoomBooking.Domain;
using MeetingRoomBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomBooking.Services.Services
{
    public interface IMeetingRoomManagementService
    {
        void CreateMeeting(MeetingRoom meetingRoom);

        (IList<MeetingRoom> data, int total, int totaldisplay) GetMeetings(int pageIndex, int pageSize, DataTablesSearch search, string? order);
        MeetingRoom GetMeeting(Guid id);
       
        void UpdateMeeting(MeetingRoom meeting);
        void DeleteMeetingRoom(Guid id);
    }
}

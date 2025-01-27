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
    public class MeetingRoomManagementService : IMeetingRoomManagementService
    {
        private readonly IMeetingRoomUnitOfWork _MeetingUnitOfWork;


        public MeetingRoomManagementService(IMeetingRoomUnitOfWork MeetingUnitOfWork)
        {
            _MeetingUnitOfWork = MeetingUnitOfWork;
        }
        public void CreateMeeting(MeetingRoom meetingRoom)
        {
            _MeetingUnitOfWork.MeetingRoom.Add(meetingRoom);
            _MeetingUnitOfWork.Save();
        }

        public (IList<MeetingRoom> data, int total, int totaldisplay) GetMeetings(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            return _MeetingUnitOfWork.MeetingRoom.GetPagedMeetingRoom(pageIndex, pageSize, search, order);
        }

        public MeetingRoom GetMeeting(Guid id)
        {
            return _MeetingUnitOfWork.MeetingRoom.GetById(id);  
        }

      

        public void UpdateMeeting(MeetingRoom meeting)
        {
            _MeetingUnitOfWork.MeetingRoom.Edit(meeting);
            _MeetingUnitOfWork.Save();
        }

        public void DeleteMeetingRoom(Guid id)
        {
           _MeetingUnitOfWork.MeetingRoom.Remove(id);   
            _MeetingUnitOfWork.Save();
        }

        public async Task DeleteMeetingsAsync(List<Guid> ids)
        {
            // Ensure that the list of ids is not null or empty
            if (ids == null || !ids.Any())
            {
                return; // Exit if there are no ids to delete
            }

            // Use the Remove method with a condition that matches the ids in the provided list
            _MeetingUnitOfWork.MeetingRoom.Remove(p => ids.Contains(p.Id));

            // Save changes to the database
            await _MeetingUnitOfWork.SaveAsync();
        }

    }
}

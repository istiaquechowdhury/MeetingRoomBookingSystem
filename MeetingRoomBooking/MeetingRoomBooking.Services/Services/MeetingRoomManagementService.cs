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
        private readonly IMeetingRoomUnitOfWork _InventoryUnitOfWork;


        public MeetingRoomManagementService(IMeetingRoomUnitOfWork InventoryUnitOfWork)
        {
            _InventoryUnitOfWork = InventoryUnitOfWork;
        }
        public void CreateMeeting(MeetingRoom meetingRoom)
        {
            _InventoryUnitOfWork.MeetingRoom.Add(meetingRoom);
            _InventoryUnitOfWork.Save();
        }
    }
}

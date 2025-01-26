using MeetingRoomBooking.Domain.Entities;
using MeetingRoomBooking.Domain.RepoInterface;
using MeetingRoomBooking.Domain.UnitOfWorkInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomBooking.Domain.UnitOfWorkInterfaces
{
    public interface IMeetingRoomUnitOfWork : IUnitOfWork   
    {
        public IMeetingRoomRepository MeetingRoom { get; }




    }
}

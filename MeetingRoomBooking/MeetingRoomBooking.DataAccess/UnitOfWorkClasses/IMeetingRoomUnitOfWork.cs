
using MeetingRoomBooking.Domain.UnitOfWorkInterfaces;
using MeetingRoomBooking.DataAccess.UnitOfWorkClasses;
using MeetingRoomBooking.Domain.Entities;
using MeetingRoomBooking.Domain.RepoInterface;
using MeetingRoomBooking.Domain.UnitOfWorkInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomBooking.DataAccess.UnitOfWorkClasses
{
    public class MeetingRoomUnitOfWork : UnitOfWork, IMeetingRoomUnitOfWork
    {
        public MeetingRoomUnitOfWork(ApplicationDbContext dbContext
           ) : base(dbContext)
        {
          


        }









    }
}

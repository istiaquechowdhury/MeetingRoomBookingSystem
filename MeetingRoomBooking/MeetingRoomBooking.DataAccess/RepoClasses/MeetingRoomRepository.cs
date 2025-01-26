using Inventory.Infrastructure.RepositoryPatternClasses;
using MeetingRoomBooking.Domain.Entities;
using MeetingRoomBooking.Domain.RepoInterface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomBooking.DataAccess.RepoClasses
{
    public class MeetingRoomRepository : Repository<MeetingRoom, Guid>, IMeetingRoomRepository
    {
        public MeetingRoomRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

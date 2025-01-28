using Inventory.Infrastructure.RepositoryPatternClasses;
using MeetingRoomBooking.Domain.Entities;
using MeetingRoomBooking.Domain.RepoInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomBooking.DataAccess.RepoClasses
{
    public class BookingRepository : Repository<Booking, Guid>, IBookingRepository
    {
        public BookingRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

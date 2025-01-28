using Inventory.Infrastructure.RepositoryPatternClasses;
using MeetingRoomBooking.Domain;
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
    public class BookingRepository : Repository<Booking, Guid>, IBookingRepository
    {
        public BookingRepository(ApplicationDbContext context) : base(context)
        {
        }

        public (IList<Booking> data, int total, int totaldisplay) GetPagedBooking(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            if (string.IsNullOrWhiteSpace(search.Value))
            {
                // If there's no search value, return all data with the specified order
                return GetDynamic(
                    null, // No filter
                    order,
                    y => y.Include(z => z.MeetingRoom),


                    pageIndex,
                    pageSize,
                    true
                );
            }
            else
            {

                return GetDynamic(
                    x => x.UserName.Contains(search.Value) ||
                         x.Pin.Contains(search.Value) ||
                         x.PhoneNumber.ToString().Contains(search.Value) ||
                         x.MeetingTitle.Contains(search.Value) ||
                         x.MeetingPurpose.Contains(search.Value) ||
                         x.RepeatBooking.Contains(search.Value) ||
                         x.StatDate.ToString().Contains(search.Value) ||
                         x.StartTime.ToString().Contains(search.Value) ||
                         x.EndDate.ToString().Contains(search.Value) ||
                         x.EndTime.ToString().Contains(search.Value)||
                         x.Department.ToString().Contains(search.Value)||
                         x.Remarks.ToString().Contains(search.Value),





                            order,
                            y => y.Include(z => z.MeetingRoom),

                            pageIndex,
                            pageSize,
                            true
                );
            }

        }
    }
}

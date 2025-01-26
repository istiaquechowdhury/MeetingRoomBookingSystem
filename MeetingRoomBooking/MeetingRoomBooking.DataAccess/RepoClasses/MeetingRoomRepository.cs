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
    public class MeetingRoomRepository : Repository<MeetingRoom, Guid>, IMeetingRoomRepository
    {
        public MeetingRoomRepository(ApplicationDbContext context) : base(context)
        {
        }

        public (IList<MeetingRoom> data, int total, int totaldisplay) GetPagedMeetingRoom(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            //if (string.IsNullOrWhiteSpace(search.Value))
            //    return GetDynamic(null, order, y => y.Include(z => z.Category), pageIndex, pageSize, true);
            if (string.IsNullOrWhiteSpace(search.Value))
            {
                // If there's no search value, return all data with the specified order
                return GetDynamic(
                    null, // No filter
                    order,
                    null,

                    pageIndex,
                    pageSize,
                    true
                );
            }
            else
            {

                return GetDynamic(
                    x => x.Name.Contains(search.Value) ||
                         x.Location.Contains(search.Value) ||
                         x.Capacity.ToString().Contains(search.Value) ||
                         x.Facilities.Contains(search.Value) ||
                         x.Description.Contains(search.Value) ||
                         x.Color.Contains(search.Value) ||
                         x.ImagePath.Contains(search.Value) ||
                         x.Status.ToString().Contains(search.Value) ||
                         x.AvailableDay.ToString().Contains(search.Value) ||
                         x.Time.ToString().Contains(search.Value),
                        


                            order,
                            null,
                            pageIndex,
                            pageSize,
                            true
                );
            }

        }

    }
}

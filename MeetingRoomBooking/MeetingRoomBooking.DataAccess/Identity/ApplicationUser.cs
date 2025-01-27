using Microsoft.AspNetCore.Identity;
using System;

namespace MeetingRoomBooking.DataAccess.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}

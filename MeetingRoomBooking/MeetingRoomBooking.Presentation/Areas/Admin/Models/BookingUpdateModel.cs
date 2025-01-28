using System.ComponentModel.DataAnnotations;

namespace MeetingRoomBooking.Presentation.Areas.Admin.Models
{
    public class BookingUpdateModel
    {
        public Guid Id { get; set; }

        public string? UserName { get; set; }

        public string? Pin { get; set; }

        public string? PhoneNumber { get; set; }

        [Required]
        public string? MeetingTitle { get; set; }



        public string? MeetingPurpose { get; set; }

        public string? RepeatBooking { get; set; }

        public Guid? MeetingRoomId { get; set; }

        [Required]
        public DateOnly? StatDate { get; set; }
        [Required]
        public TimeOnly? StartTime { get; set; }
        [Required]
        public DateOnly? EndDate { get; set; }
        [Required]
        public TimeOnly? EndTime { set; get; }


        public string? Department { get; set; }

        public string? Remarks { get; set; }
    }
}

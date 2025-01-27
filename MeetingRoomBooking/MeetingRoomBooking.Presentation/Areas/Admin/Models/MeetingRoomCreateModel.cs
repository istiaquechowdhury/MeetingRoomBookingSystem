using System.ComponentModel.DataAnnotations;

namespace MeetingRoomBooking.Presentation.Areas.Admin.Models
{
    public class MeetingRoomCreateModel
    {
        [Required]
        public string? Name { get; set; }

        public string? Location { get; set; }

        [Required]
        public int? Capacity { get; set; }

        public string? Facilities { get; set; }

        public string? Description { get; set; }

        [Required]
        public string? Color { get; set; }

        public string? ImagePath { get; set; }

        public bool Status { get; set; }

        public DateOnly? AvailableDay { get; set; }

        public TimeOnly? Time { get; set; }

        public IFormFile? ImageFile { get; set; }

    }
}

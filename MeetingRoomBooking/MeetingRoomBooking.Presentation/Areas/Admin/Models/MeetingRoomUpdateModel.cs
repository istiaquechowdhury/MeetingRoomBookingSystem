﻿using System.ComponentModel.DataAnnotations;

namespace MeetingRoomBooking.Presentation.Areas.Admin.Models
{
    public class MeetingRoomUpdateModel
    {
        public Guid Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? Location { get; set; }

        public int? Capacity { get; set; }

        [Required]
        public string? Facilities { get; set; }

        public string? Description { get; set; }

        [Required]
        public string? Color { get; set; }

        public string? ImagePath { get; set; }

        public bool Status { get; set; }

        public DateOnly? AvailableDay { get; set; }

        public TimeOnly? Time { get; set; }

        public IFormFile? ImageFile { get; set; }

        public bool RemoveImage { get; set; }   
    }
}

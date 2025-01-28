using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomBooking.Domain.Entities
{
    public class Booking : IEntity<Guid>    
    {
        public Guid Id { get; set; } 
        
        public string? UserName { get; set; }

        public string? Pin {  get; set; } 
        
        public string? PhoneNumber { get; set; }     

        public string? MeetingTitle { get; set; }  
        
        public string? MeetingPurpose { get; set; }  

        public string? RepeatBooking { get; set; }   

        public Guid? MeetingRoomId { get; set; } 

        public DateOnly? StatDate { get; set; }  

        public TimeOnly? StartTime {  get; set; } 

        public DateOnly? EndDate { get; set; }   

        public TimeOnly? EndTime {  set; get; }

        public string? Department {  get; set; }

        public string? Remarks { get; set; }    
        
        public MeetingRoom MeetingRoom { get; set; }    
    }
}

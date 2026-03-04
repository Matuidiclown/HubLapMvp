using System;
using System.Collections.Generic;

namespace LAPS.Models.Entities
{
    public class BookingDetail
    {
        public int DetailId { get; set; }
        public int BookingId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? MeetingSubject { get; set; }
        public string? Notes { get; set; }
        public List<string> GuestIds { get; set; } = new List<string>();
    }
}
using System;

namespace LAPS.Web.DTOs
{
    public class BookingRequest
    {
        public string UserId { get; set; } = string.Empty;
        public int RoomId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string? Notes { get; set; }
    }
}

using System;
namespace LAPS.Models.Entities
{
    public class BookingHeader
    {
        public int BookingId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int RoomId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int BookingStatus { get; set; }
    }
}

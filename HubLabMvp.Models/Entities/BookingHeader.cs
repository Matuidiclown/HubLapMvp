using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubLap.Models.Entities
{
    public class BookingHeader
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public int AssignedRoomId { get; set; }
        public string Subject { get; set; } = string.Empty;
        public DateTime BookingStart { get; set; }
        public DateTime BookingEnd { get; set; }
        public string RoomName { get; set; } = string.Empty;
    }
}

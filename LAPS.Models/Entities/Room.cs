using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPS.Models.Entities
{
    public class Room
    {
        public int RoomId { get; set; }
        public int CategoryId { get; set; }
        public string RoomName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public bool HasProjector { get; set; }
        public bool HasWhiteboard { get; set; }
        public int OperationalStatus { get; set; }
        public bool IsActive { get; set; }
    }
}

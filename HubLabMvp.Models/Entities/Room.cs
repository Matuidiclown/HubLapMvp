using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubLap.Models.Entities
{
    public class Room
    {
        public int RoomId { get; set; }
        public int RoomCategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty; // Agrega esto
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public string Location { get; set; } = string.Empty;
        public bool HasProjector { get; set; }
        public bool HasWhiteboard { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}

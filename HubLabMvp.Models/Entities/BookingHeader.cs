using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubLap.Models.Entities
{
    public class BookingHeader : Entity
    {
        public int UserId { get; set; }
        public int StatusId { get; set; }
        public string Subject { get; set; } = string.Empty;

        // Fecha en que se hizo la reserva (auditoría)
        public DateTime BookingDate { get; set; } = DateTime.Now;

        // Fechas generales para facilitar búsquedas (opcional, pero recomendado)
        public DateTime BookingStart { get; set; }
        public DateTime BookingEnd { get; set; }

        // Propiedad para recibir el ID de la sala desde el formulario (no se guarda en DB Header, sirve para el flujo)
        public int RoomID { get; set; }

        // Relación con los detalles
        public List<BookingDetail> Details { get; set; } = new List<BookingDetail>();
    }
}

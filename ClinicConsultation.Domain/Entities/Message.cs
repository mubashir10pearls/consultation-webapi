using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicConsultation.Domain.Entities
{
    public class Message
    {
        public Guid Id { get; set; }

        public Guid ConsultationId { get; set; }

        public string Role { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
            = DateTime.UtcNow;

        public Consultation Consultation { get; set; }
    }
}

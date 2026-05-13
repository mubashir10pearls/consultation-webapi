using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicConsultation.Domain.Entities
{
    public class Consultation
    {
        public Guid Id { get; set; }

        public string PatientName { get; set; } = string.Empty;

        public string PrimaryConcern { get; set; } = string.Empty;

        public string Status { get; set; } = "Pending";

        public DateTime CreatedAt { get; set; }
            = DateTime.UtcNow;

        public ICollection<Message> Messages { get; set; }
            = new List<Message>();

        public Recommendation? Recommendation { get; set; }

        public Appointment? Appointment { get; set; }
    }
}

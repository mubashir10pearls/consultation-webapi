using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicConsultation.Domain.Entities
{
    public class Appointment
    {
        public Guid Id { get; set; }

        public Guid ConsultationId { get; set; }

        public string Treatment { get; set; } = string.Empty;

        public DateTime AppointmentDate { get; set; }

        public string Location { get; set; } = string.Empty;

        public string TargetArea { get; set; } = string.Empty;
        public string EstimateDuration { get; set; } = string.Empty;
        public string Provider { get; set; } = string.Empty;

        public Consultation Consultation { get; set; }
    }
}

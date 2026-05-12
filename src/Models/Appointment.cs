using MediCoreApi.Enums;

namespace MediCoreApi.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;

        public Patient Patient { get; set; } = null!;
        public Doctor Doctor { get; set; } = null!;

        public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
    }
}

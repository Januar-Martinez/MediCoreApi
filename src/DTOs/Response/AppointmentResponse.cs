using MediCoreApi.Enums;

namespace MediCoreApi.DTOs.Response
{
    public class AppointmentResponse
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        public int DoctorId { get; set; }

        public DateTime AppointmentDate { get; set; }

        public AppointmentStatus Status { get; set; }
    }
}

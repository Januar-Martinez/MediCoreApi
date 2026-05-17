using MediCoreApi.Enums;
using System.ComponentModel.DataAnnotations;

namespace MediCoreApi.DTOs.Request
{
    public class UpdateAppointmentRequest
    {
        [Required(ErrorMessage = "PatientId is required")]
        public int PatientId { get; set; }

        [Required(ErrorMessage = "DoctorId is required")]
        public int DoctorId { get; set; }

        [Required(ErrorMessage = "AppointmentDate is required")]
        public DateTime AppointmentDate { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public AppointmentStatus Status { get; set; }
    }
}

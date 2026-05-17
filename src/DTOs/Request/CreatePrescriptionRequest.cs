using System.ComponentModel.DataAnnotations;

namespace MediCoreApi.DTOs.Request
{
    public class CreatePrescriptionRequest
    {
        [Required(ErrorMessage = "AppointmentId is required")]
        public int AppointmentId { get; set; }

        [Required(ErrorMessage = "Medication is required")]
        [MaxLength(150, ErrorMessage = "Medication cannot exceed 150 characters")]
        public string Medication { get; set; } = string.Empty;

        [Required(ErrorMessage = "Dosage is required")]
        [MaxLength(200, ErrorMessage = "Dosage cannot exceed 200 characters")]
        public string Dosage { get; set; } = string.Empty;
    }
}

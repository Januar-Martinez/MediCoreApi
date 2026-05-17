using System.ComponentModel.DataAnnotations;

namespace MediCoreApi.DTOs.Request
{
    public class UpdateDoctorRequest
    {
        [Required(ErrorMessage = "FullName is required")]
        [MaxLength(100, ErrorMessage = "FullName cannot exceed 100 characters")]
        public string FullName { get; set; } = string.Empty;

        [MaxLength(100, ErrorMessage = "Specialty cannot exceed 100 characters")]
        public string? Specialty { get; set; }

        [Required(ErrorMessage = "LicenseNumber is required")]
        public int LicenseNumber { get; set; }
        public bool IsActive { get; set; }
    }
}

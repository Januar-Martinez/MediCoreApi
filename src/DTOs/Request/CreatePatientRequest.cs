using System.ComponentModel.DataAnnotations;

namespace MediCoreApi.DTOs.Request
{
    public class CreatePatientRequest
    {
        [Required(ErrorMessage = "FullName is required")]
        [MaxLength(100, ErrorMessage = "FullName cannot exceed 100 characters")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [MaxLength(150, ErrorMessage = "Email cannot exceed 150 characters")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "BirthDate is required")]
        public DateOnly BirthDate { get; set; }
    }
}

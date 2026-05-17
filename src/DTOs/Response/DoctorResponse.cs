namespace MediCoreApi.DTOs.Response
{
    public class DoctorResponse
    {
        public int Id { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string? Specialty { get; set; }

        public int LicenseNumber { get; set; }

        public bool IsActive { get; set; }
    }
}

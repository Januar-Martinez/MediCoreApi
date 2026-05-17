namespace MediCoreApi.DTOs.Response
{
    public class PatientResponse
    {
        public int Id { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public DateOnly BirthDate { get; set; }

        public bool IsActive { get; set; }
    }
}

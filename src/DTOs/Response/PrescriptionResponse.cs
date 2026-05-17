namespace MediCoreApi.DTOs.Response
{
    public class PrescriptionResponse
    {
        public int Id { get; set; }

        public int AppointmentId { get; set; }

        public string Medication { get; set; } = string.Empty;

        public string Dosage { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}

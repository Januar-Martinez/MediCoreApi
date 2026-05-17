namespace MediCoreApi.DTOs.Response
{
    public class MedicalStatisticsResponse
    {
        public int ActivePatients { get; set; }

        public int CancelledAppointments { get; set; }

        public IEnumerable<DoctorAppointmentStatistic> TopDoctors { get; set; } = new List<DoctorAppointmentStatistic>();

        public IEnumerable<SpecialtyStatistic> TopSpecialties { get; set; } = new List<SpecialtyStatistic>();
    }

    public class DoctorAppointmentStatistic
    {
        public int DoctorId { get; set; }

        public string DoctorName { get; set; } = string.Empty;

        public string? Specialty { get; set; }

        public int TotalAppointments { get; set; }
    }

    public class SpecialtyStatistic
    {
        public string Specialty { get; set; } = string.Empty;

        public int TotalAppointments { get; set; }
    }
}
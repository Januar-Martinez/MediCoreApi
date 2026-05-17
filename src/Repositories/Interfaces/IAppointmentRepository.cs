using MediCoreApi.Models;

namespace MediCoreApi.Repositories.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetAllAsync();
        Task<Appointment?> GetByIdAsync(int id);
        Task<IEnumerable<Appointment>> GetByPatientIdAsync(int patientId);
        Task<bool> ExistsDoctorAppointmentAsync(int doctorId, DateTime appointmentDate);
        Task<bool> ExistsPatientAppointmentAsync(int patientId, DateTime appointmentDate);
        Task<bool> ExistsDoctorAppointmentAsync(int doctorId, DateTime appointmentDate, int excludeAppointmentId);
        Task<bool> ExistsPatientAppointmentAsync(int patientId, DateTime appointmentDate, int excludeAppointmentId);
        Task<Appointment> CreateAsync(Appointment appointment);
        Task<Appointment> UpdateAsync(Appointment appointment);
        Task DeleteAsync(Appointment appointment);
    }
}

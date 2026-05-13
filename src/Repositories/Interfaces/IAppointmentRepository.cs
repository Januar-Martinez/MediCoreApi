using MediCoreApi.Models;

namespace MediCoreApi.Repositories.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetAllAsync();
        Task<Appointment?> GetByIdAsync(int id);
        Task<Appointment> CreateAsync(Appointment appointment);
        Task<Appointment?> UpdateAsync(Appointment appointment);
        Task DeleteAsync(Appointment appointment);
    }
}

using MediCoreApi.Models;

namespace MediCoreApi.Repositories.Interfaces
{
    public interface IPrescriptionRepository
    {
        Task<IEnumerable<Prescription>> GetAllAsync();
        Task<Prescription?> GetByIdAsync(int id);
        Task<IEnumerable<Prescription>> GetByAppointmentIdAsync(int appointmentId);
        Task<Prescription> CreateAsync(Prescription prescription);
    }
}

using MediCoreApi.Models;

namespace MediCoreApi.Repositories.Interfaces
{
    public interface IPrescriptionRepository
    {
        Task<IEnumerable<Prescription>> GetAllAsync();
        Task<Prescription?> GetByIdAsync(int id);
        Task<Prescription> CreateAsync(Prescription prescription);
    }
}

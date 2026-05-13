using MediCoreApi.Models;

namespace MediCoreApi.Repositories.Interfaces
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetAllAsync();
        Task<Patient?> GetByIdAsync(int id);
        Task<bool> ExistsByEmailAsync(string email);
        Task<Patient> CreateAsync(Patient patient);
        Task<Patient?> UpdateAsync(Patient patient);
    }
}

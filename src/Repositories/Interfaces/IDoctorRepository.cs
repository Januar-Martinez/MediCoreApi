using MediCoreApi.Models;

namespace MediCoreApi.Repositories.Interfaces
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetAllAsync();
        Task<Doctor?> GetByIdAsync(int id);
        Task<bool> ExistsByLicenseNumberAsync(int licenseNumber);
        Task<Doctor> CreateAsync(Doctor doctor);
        Task<Doctor?> UpdateAsync(Doctor doctor);
    }
}

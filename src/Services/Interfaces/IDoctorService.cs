using MediCoreApi.DTOs.Request;
using MediCoreApi.DTOs.Response;

namespace MediCoreApi.Services.Interfaces
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorResponse>> GetAllAsync();

        Task<DoctorResponse> GetByIdAsync(int id);

        Task<DoctorResponse> CreateAsync(CreateDoctorRequest request);

        Task<DoctorResponse> UpdateAsync(int id, UpdateDoctorRequest request);
    }
}

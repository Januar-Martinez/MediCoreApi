using MediCoreApi.DTOs.Request;
using MediCoreApi.DTOs.Response;

namespace MediCoreApi.Services.Interfaces
{
    public interface IPatientService
    {
        Task<IEnumerable<PatientResponse>> GetAllAsync();

        Task<PatientResponse> GetByIdAsync(int id);

        Task<PatientResponse> CreateAsync(CreatePatientRequest request);

        Task<PatientResponse> UpdateAsync(int id, UpdatePatientRequest request);
    }
}

using MediCoreApi.DTOs.Request;
using MediCoreApi.DTOs.Response;

namespace MediCoreApi.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentResponse>> GetAllAsync();

        Task<AppointmentResponse> GetByIdAsync(int id);

        Task<IEnumerable<AppointmentResponse>> GetByPatientIdAsync(int patientId);

        Task<AppointmentResponse> CreateAsync(CreateAppointmentRequest request);

        Task<AppointmentResponse> UpdateAsync(int id, UpdateAppointmentRequest request);

        Task DeleteAsync(int id);
    }
}

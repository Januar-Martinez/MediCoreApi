using MediCoreApi.DTOs.Request;
using MediCoreApi.DTOs.Response;

namespace MediCoreApi.Services.Interfaces
{
    public interface IPrescriptionService
    {
        Task<IEnumerable<PrescriptionResponse>> GetAllAsync();

        Task<PrescriptionResponse> GetByIdAsync(int id);

        Task<IEnumerable<PrescriptionResponse>> GetByAppointmentIdAsync(int appointmentId);

        Task<PrescriptionResponse> CreateAsync(CreatePrescriptionRequest request);
    }
}

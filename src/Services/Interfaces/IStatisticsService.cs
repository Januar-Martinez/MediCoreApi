using MediCoreApi.DTOs.Response;

namespace MediCoreApi.Services.Interfaces
{
    public interface IStatisticsService
    {
        Task<MedicalStatisticsResponse> GetStatisticsAsync();
    }
}

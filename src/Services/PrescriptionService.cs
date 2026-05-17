using MediCoreApi.Common.Errors;
using MediCoreApi.DTOs.Request;
using MediCoreApi.DTOs.Response;
using MediCoreApi.Models;
using MediCoreApi.Repositories.Interfaces;
using MediCoreApi.Services.Interfaces;

namespace MediCoreApi.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IPrescriptionRepository _prescriptionRepository;
        private readonly IAppointmentRepository _appointmentRepository;

        public PrescriptionService(IPrescriptionRepository prescriptionRepository, IAppointmentRepository appointmentRepository)
        {
            _prescriptionRepository = prescriptionRepository;
            _appointmentRepository = appointmentRepository;
        }
        public async Task<PrescriptionResponse> CreateAsync(CreatePrescriptionRequest request)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(request.AppointmentId);
            if (appointment is null) throw new AppException($"Appointment with ID {request.AppointmentId} not found", 404);

            var prescription = new Prescription
            {
                AppointmentId = request.AppointmentId,
                Medication = request.Medication,
                Dosage = request.Dosage
            };

            var createdPrescription = await _prescriptionRepository.CreateAsync(prescription);

            return MapToResponse(createdPrescription);
        }

        public async Task<IEnumerable<PrescriptionResponse>> GetAllAsync()
        {
            var prescriptions = await _prescriptionRepository.GetAllAsync();

            return prescriptions.Select(p => MapToResponse(p));
        }

        public async Task<IEnumerable<PrescriptionResponse>> GetByAppointmentIdAsync(int appointmentId)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(appointmentId);
            if (appointment is null) throw new AppException($"Appointment with ID {appointmentId} not found", 404);

            var prescription = await _prescriptionRepository.GetByAppointmentIdAsync(appointmentId);

            return prescription.Select(p => MapToResponse(p));
        }

        public async Task<PrescriptionResponse> GetByIdAsync(int id)
        {
            var prescription = await _prescriptionRepository.GetByIdAsync(id);

            if (prescription is null) throw new AppException("Prescription not found");

            return MapToResponse(prescription);
        }

        private static PrescriptionResponse MapToResponse(Prescription prescription) => new()
        {
            Id = prescription!.Id,
            AppointmentId = prescription.AppointmentId,
            Medication = prescription.Medication,
            Dosage = prescription.Dosage,
            CreatedAt = prescription.CreatedAt
        };
    }
}

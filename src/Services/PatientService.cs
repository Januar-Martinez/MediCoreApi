using MediCoreApi.Common.Errors;
using MediCoreApi.DTOs.Request;
using MediCoreApi.DTOs.Response;
using MediCoreApi.Models;
using MediCoreApi.Repositories.Interfaces;
using MediCoreApi.Services.Interfaces;

namespace MediCoreApi.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }
        public async Task<PatientResponse> CreateAsync(CreatePatientRequest request)
        {
            var emailExists = await _patientRepository.ExistsByEmailAsync(request.Email);

            if (emailExists) throw new AppException("Email already exists");

            var patient = new Patient
            {
                FullName = request.FullName,
                Email = request.Email,
                BirthDate = request.BirthDate
            };

            var createdPatient = await _patientRepository.CreateAsync(patient);

            return MapToResponse(createdPatient);
        }

        public async Task<IEnumerable<PatientResponse>> GetAllAsync()
        {
            var patients = await _patientRepository.GetAllAsync();

            return patients.Select(p => MapToResponse(p));
        }

        public async Task<PatientResponse> GetByIdAsync(int id)
        {
            var patient = await _patientRepository.GetByIdAsync(id);

            if (patient is null) throw new AppException($"Patient with ID {id} not found", 404);

            return MapToResponse(patient);
        }

        public async Task<PatientResponse> UpdateAsync(int id, UpdatePatientRequest request)
        {
            var existingPatient = await _patientRepository.GetByIdAsync(id);

            if (existingPatient is null) throw new AppException("Patient not found");

            if (existingPatient.Email != request.Email)
            {
                var emailExists = await _patientRepository.ExistsByEmailAsync(request.Email);

                if (emailExists) throw new AppException("Email already exists");
            }

            existingPatient.FullName = request.FullName;
            existingPatient.Email = request.Email;
            existingPatient.BirthDate = request.BirthDate;
            existingPatient.IsActive = request.IsActive;

            var updatedPatient = await _patientRepository.UpdateAsync(existingPatient);

            return MapToResponse(updatedPatient!);
        }

        private static PatientResponse MapToResponse(Patient patient) => new()
        {
            Id = patient.Id,
            FullName = patient.FullName,
            Email = patient.Email,
            BirthDate = patient.BirthDate,
            IsActive = patient.IsActive
        };
    }
}

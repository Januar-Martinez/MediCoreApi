using MediCoreApi.Common.Errors;
using MediCoreApi.DTOs.Request;
using MediCoreApi.DTOs.Response;
using MediCoreApi.Models;
using MediCoreApi.Repositories.Interfaces;
using MediCoreApi.Services.Interfaces;

namespace MediCoreApi.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<DoctorResponse> CreateAsync(CreateDoctorRequest request)
        {
            var licenseNumberExists = await _doctorRepository.ExistsByLicenseNumberAsync(request.LicenseNumber);

            if (licenseNumberExists) throw new AppException("LicenseNumber already exists");

            var doctor = new Doctor
            {
                FullName = request.FullName,
                Specialty = request.Specialty,
                LicenseNumber = request.LicenseNumber
            };

            var createdDoctor = await _doctorRepository.CreateAsync(doctor);

            return MapToResponse(createdDoctor);
        }

        public async Task<IEnumerable<DoctorResponse>> GetAllAsync()
        {
            var doctors = await _doctorRepository.GetAllAsync();

            return doctors.Select(d => MapToResponse(d));
        }

        public async Task<DoctorResponse> GetByIdAsync(int id)
        {
            var doctor = await _doctorRepository.GetByIdAsync(id);

            if (doctor is null) throw new AppException("Doctor not found");

            return MapToResponse(doctor);
        }

        public async Task<DoctorResponse> UpdateAsync(int id, UpdateDoctorRequest request)
        {
            var existingDoctor = await _doctorRepository.GetByIdAsync(id);

            if (existingDoctor is null) throw new AppException("Doctor not found");

            if (existingDoctor.LicenseNumber != request.LicenseNumber)
            {
                var licenseNumberExists = await _doctorRepository.ExistsByLicenseNumberAsync(request.LicenseNumber);

                if (licenseNumberExists) throw new AppException("LicenseNumber already exists");
            }

            existingDoctor.FullName = request.FullName;
            existingDoctor.Specialty = request.Specialty;
            existingDoctor.LicenseNumber = request.LicenseNumber;
            existingDoctor.IsActive = request.IsActive;

            var updatedDoctor = await _doctorRepository.UpdateAsync(existingDoctor);

            return MapToResponse(updatedDoctor!);
        }

        private static DoctorResponse MapToResponse(Doctor doctor) => new()
        {
            Id = doctor!.Id,
            FullName = doctor.FullName,
            Specialty = doctor.Specialty,
            LicenseNumber = doctor.LicenseNumber,
            IsActive = doctor.IsActive
        };
    }
}

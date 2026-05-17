using MediCoreApi.Common.Errors;
using MediCoreApi.DTOs.Request;
using MediCoreApi.DTOs.Response;
using MediCoreApi.Enums;
using MediCoreApi.Models;
using MediCoreApi.Repositories.Interfaces;
using MediCoreApi.Services.Interfaces;

namespace MediCoreApi.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorRepository _doctorRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository, IPatientRepository patientRepository, IDoctorRepository doctorRepository)
        {
            _appointmentRepository = appointmentRepository;
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
        }

        public async Task<AppointmentResponse> CreateAsync(CreateAppointmentRequest request)
        {
            var patient = await _patientRepository.GetByIdAsync(request.PatientId);
            if (patient is null) throw new AppException($"Patient with ID {request.PatientId} not found", 404);

            var doctor = await _doctorRepository.GetByIdAsync(request.DoctorId);
            if (doctor is null) throw new AppException($"Doctor with ID {request.DoctorId} not found", 404);

            var doctorBusy = await _appointmentRepository.ExistsDoctorAppointmentAsync(request.DoctorId, request.AppointmentDate);
            if (doctorBusy) throw new AppException("Doctor already has an appointment at this time", 409);

            var patientBusy = await _appointmentRepository.ExistsPatientAppointmentAsync(request.PatientId, request.AppointmentDate);
            if (patientBusy) throw new AppException("Patient already has an appointment at this time", 409);

            var appointment = new Appointment
            {
                PatientId = request.PatientId,
                DoctorId = request.DoctorId,
                AppointmentDate = request.AppointmentDate
            };

            var createdAppointment = await _appointmentRepository.CreateAsync(appointment);

            return MapToResponse(createdAppointment);
        }

        public async Task DeleteAsync(int id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);

            if (appointment is null) throw new AppException($"Appointment with ID {id} not found", 404);

            if (appointment.Status != AppointmentStatus.Cancelled) throw new AppException("Only cancelled appointments can be deleted", 409);

            await _appointmentRepository.DeleteAsync(appointment);
        }

        public async Task<IEnumerable<AppointmentResponse>> GetAllAsync()
        {
            var appointments = await _appointmentRepository.GetAllAsync();

            return appointments.Select(a => MapToResponse(a));
        }

        public async Task<AppointmentResponse> GetByIdAsync(int id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);

            if (appointment is null) throw new AppException("Appointment not found");

            return MapToResponse(appointment);
        }

        public async Task<IEnumerable<AppointmentResponse>> GetByPatientIdAsync(int patientId)
        {
            var patient = await _patientRepository.GetByIdAsync(patientId);
            if (patient is null) throw new AppException($"Patient with ID {patientId} not found", 404);

            var appointments = await _appointmentRepository.GetByPatientIdAsync(patientId);

            return appointments.Select(a => MapToResponse(a));
        }

        public async Task<AppointmentResponse> UpdateAsync(int id, UpdateAppointmentRequest request)
        {
            var existingAppointment = await _appointmentRepository.GetByIdAsync(id);
            if (existingAppointment is null) throw new AppException($"Appointment with ID {id} not found", 404);

            var patient = await _patientRepository.GetByIdAsync(request.PatientId);
            if (patient is null) throw new AppException($"Patient with ID {request.PatientId} not found", 404);

            var doctor = await _doctorRepository.GetByIdAsync(request.DoctorId);
            if (doctor is null) throw new AppException($"Doctor with ID {request.DoctorId} not found", 404);

            var doctorBusy = await _appointmentRepository.ExistsDoctorAppointmentAsync(request.DoctorId, request.AppointmentDate, id);
            if (doctorBusy) throw new AppException("Doctor already has an appointment at this time", 409);

            var patientBusy = await _appointmentRepository.ExistsPatientAppointmentAsync(request.PatientId, request.AppointmentDate, id);
            if (patientBusy) throw new AppException("Patient already has an appointment at this time", 409);

            existingAppointment.PatientId = request.PatientId;
            existingAppointment.DoctorId = request.DoctorId;
            existingAppointment.AppointmentDate = request.AppointmentDate;
            existingAppointment.Status = request.Status;

            var updatedAppointment = await _appointmentRepository.UpdateAsync(existingAppointment);

            return MapToResponse(updatedAppointment);
        }

        private static AppointmentResponse MapToResponse(Appointment appointment) => new()
        {
            Id = appointment.Id,
            PatientId = appointment.PatientId,
            DoctorId = appointment.DoctorId,
            AppointmentDate = appointment.AppointmentDate,
            Status = appointment.Status
        };
    }
}

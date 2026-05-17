using MediCoreApi.Data;
using MediCoreApi.Enums;
using MediCoreApi.Models;
using MediCoreApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MediCoreApi.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppDbContext _context;

        public AppointmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Appointment> CreateAsync(Appointment appointment)
        {
            _context.Appointments.Add(appointment);

            await _context.SaveChangesAsync();

            return appointment;
        }

        public async Task DeleteAsync(Appointment appointment)
        {
            _context.Appointments.Remove(appointment);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsDoctorAppointmentAsync(int doctorId, DateTime appointmentDate)
        {
            return await _context.Appointments
                .AnyAsync(a =>
                    a.DoctorId == doctorId &&
                    a.AppointmentDate == appointmentDate &&
                    a.Status != AppointmentStatus.Cancelled);
        }

        public async Task<bool> ExistsDoctorAppointmentAsync(int doctorId, DateTime appointmentDate, int excludeAppointmentId)
        {
            return await _context.Appointments
                .AnyAsync(a =>
                    a.DoctorId == doctorId &&
                    a.AppointmentDate == appointmentDate &&
                    a.Id != excludeAppointmentId &&
                    a.Status != AppointmentStatus.Cancelled);
        }

        public async Task<bool> ExistsPatientAppointmentAsync(int patientId, DateTime appointmentDate)
        {
            return await _context.Appointments
                .AnyAsync(a =>
                    a.PatientId == patientId &&
                    a.AppointmentDate == appointmentDate &&
                    a.Status != AppointmentStatus.Cancelled);
        }

        public async Task<bool> ExistsPatientAppointmentAsync(int patientId, DateTime appointmentDate, int excludeAppointmentId)
        {
            return await _context.Appointments
                .AnyAsync(a =>
                    a.PatientId == patientId &&
                    a.AppointmentDate == appointmentDate &&
                    a.Id != excludeAppointmentId &&
                    a.Status != AppointmentStatus.Cancelled);
        }

        public async Task<IEnumerable<Appointment>> GetAllAsync()
            => await _context.Appointments.ToListAsync();

        public async Task<Appointment?> GetByIdAsync(int id)
            => await _context.Appointments.FindAsync(id);

        public async Task<IEnumerable<Appointment>> GetByPatientIdAsync(int patientId)
        {
            return await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Where(a => a.PatientId == patientId)
                .OrderByDescending(a => a.AppointmentDate)
                .ToListAsync();
        }

        public async Task<Appointment> UpdateAsync(Appointment appointment)
        {
            _context.Appointments.Update(appointment);

            await _context.SaveChangesAsync();

            return appointment;
        }
    }
}
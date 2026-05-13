using MediCoreApi.Data;
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

        public async Task<IEnumerable<Appointment>> GetAllAsync()
            => await _context.Appointments.ToListAsync();

        public async Task<Appointment?> GetByIdAsync(int id)
            => await _context.Appointments.FindAsync(id);

        public async Task<Appointment?> UpdateAsync(Appointment appointment)
        {
            var existingAppointment = await _context.Appointments
                .FirstOrDefaultAsync(a => a.Id == appointment.Id);

            if (existingAppointment is null)
                return null;

            existingAppointment.PatientId = appointment.PatientId;
            existingAppointment.DoctorId = appointment.DoctorId;
            existingAppointment.AppointmentDate = appointment.AppointmentDate;
            existingAppointment.Status = appointment.Status;

            await _context.SaveChangesAsync();

            return existingAppointment;
        }
    }
}
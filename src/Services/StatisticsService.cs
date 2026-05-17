using MediCoreApi.Data;
using MediCoreApi.DTOs.Response;
using MediCoreApi.Enums;
using MediCoreApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MediCoreApi.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly AppDbContext _context;

        public StatisticsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<MedicalStatisticsResponse> GetStatisticsAsync()
        {
            var activePatients = await _context.Patients.CountAsync(p => p.IsActive);

            var cancelledAppointments = await _context.Appointments.CountAsync(a => a.Status == AppointmentStatus.Cancelled);

            var topDoctors = await _context.Appointments
                .Include(a => a.Doctor)
                .GroupBy(a => new
                {
                    a.DoctorId,
                    a.Doctor.FullName,
                    a.Doctor.Specialty
                })
                .Select(g => new DoctorAppointmentStatistic
                {
                    DoctorId = g.Key.DoctorId,
                    DoctorName = g.Key.FullName,
                    Specialty = g.Key.Specialty,
                    TotalAppointments = g.Count()
                })
                .OrderByDescending(d =>
                    d.TotalAppointments)
                .Take(5)
                .ToListAsync();

            var topSpecialties = await _context.Appointments
                    .Include(a => a.Doctor)
                    .GroupBy(a => a.Doctor.Specialty)
                    .Select(g => new SpecialtyStatistic
                    {
                        Specialty =
                            g.Key ?? "No specialty",

                        TotalAppointments =
                            g.Count()
                    })
                    .OrderByDescending(s =>
                        s.TotalAppointments)
                    .ToListAsync();

            return new MedicalStatisticsResponse
            {
                ActivePatients = activePatients,
                CancelledAppointments = cancelledAppointments,
                TopDoctors = topDoctors,
                TopSpecialties = topSpecialties
            };
        }
    }
}
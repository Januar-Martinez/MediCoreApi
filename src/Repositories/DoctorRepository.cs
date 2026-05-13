using MediCoreApi.Data;
using MediCoreApi.Models;
using MediCoreApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MediCoreApi.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly AppDbContext _context;

        public DoctorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Doctor> CreateAsync(Doctor doctor)
        {
            _context.Doctors.Add(doctor);

            await _context.SaveChangesAsync();

            return doctor;
        }

        public async Task<bool> ExistsByLicenseNumberAsync(int licenseNumber)
        {
            return await _context.Doctors
                .AnyAsync(d => d.LicenseNumber == licenseNumber);
        }

        public async Task<IEnumerable<Doctor>> GetAllAsync()
            => await _context.Doctors.ToListAsync();

        public async Task<Doctor?> GetByIdAsync(int id)
            => await _context.Doctors.FindAsync(id);

        public async Task<Doctor?> UpdateAsync(Doctor doctor)
        {
            var existingDoctor = await _context.Doctors
                .FirstOrDefaultAsync(d => d.Id == doctor.Id);

            if (existingDoctor is null)
                return null;

            existingDoctor.FullName = doctor.FullName;
            existingDoctor.Specialty = doctor.Specialty;
            existingDoctor.LicenseNumber = doctor.LicenseNumber;
            existingDoctor.IsActive = doctor.IsActive;

            await _context.SaveChangesAsync();

            return existingDoctor;
        }
    }
}

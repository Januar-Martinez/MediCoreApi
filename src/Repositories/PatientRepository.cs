using MediCoreApi.Data;
using MediCoreApi.Models;
using MediCoreApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MediCoreApi.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly AppDbContext _context;

        public PatientRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Patient> CreateAsync(Patient patient)
        {
            _context.Patients.Add(patient);

            await _context.SaveChangesAsync();

            return patient;
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Patients
                .AnyAsync(p => p.Email == email);
        }

        public async Task<IEnumerable<Patient>> GetAllAsync()
            => await _context.Patients.ToListAsync();

        public async Task<Patient?> GetByIdAsync(int id)
            => await _context.Patients.FindAsync(id);

        public async Task<Patient?> UpdateAsync(Patient patient)
        {
            var existingPatient = await _context.Patients
                .FirstOrDefaultAsync(p => p.Id == patient.Id);

            if (existingPatient is null)
                return null;

            existingPatient.FullName = patient.FullName;
            existingPatient.Email = patient.Email;
            existingPatient.BirthDate = patient.BirthDate;
            existingPatient.IsActive = patient.IsActive;

            await _context.SaveChangesAsync();

            return existingPatient;
        }
    }
}

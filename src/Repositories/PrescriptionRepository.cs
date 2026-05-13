using MediCoreApi.Data;
using MediCoreApi.Models;
using MediCoreApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MediCoreApi.Repositories
{
    public class PrescriptionRepository : IPrescriptionRepository
    {
        private readonly AppDbContext _context;

        public PrescriptionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Prescription> CreateAsync(
            Prescription prescription)
        {
            _context.Prescriptions.Add(prescription);

            await _context.SaveChangesAsync();

            return prescription;
        }

        public async Task<IEnumerable<Prescription>> GetAllAsync()
            => await _context.Prescriptions
                .Include(p => p.Appointment)
                .ToListAsync();

        public async Task<Prescription?> GetByIdAsync(int id)
            => await _context.Prescriptions
                .Include(p => p.Appointment)
                .FirstOrDefaultAsync(p => p.Id == id);
    }
}
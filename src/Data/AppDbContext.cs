using MediCoreApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MediCoreApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.Property(p => p.FullName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(p => p.Email)
                      .IsRequired()
                      .HasMaxLength(150);

                entity.HasIndex(p => p.Email)
                      .IsUnique();
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.Property(d => d.FullName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(d => d.Specialty)
                      .HasMaxLength(100);

                entity.HasIndex(d => d.LicenseNumber)
                      .IsUnique();
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.Property(a => a.AppointmentDate)
                      .IsRequired();

                entity.HasOne(a => a.Patient)
                      .WithMany(p => p.Appointments)
                      .HasForeignKey(a => a.PatientId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(a => a.Doctor)
                      .WithMany(d => d.Appointments)
                      .HasForeignKey(a => a.DoctorId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Prescription>(entity =>
            {
                entity.Property(p => p.Medication)
                      .IsRequired()
                      .HasMaxLength(150);

                entity.Property(p => p.Dosage)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.HasOne(p => p.Appointment)
                      .WithMany(a => a.Prescriptions)
                      .HasForeignKey(p => p.AppointmentId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}

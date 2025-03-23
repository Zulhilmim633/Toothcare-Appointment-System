using Microsoft.EntityFrameworkCore;
using Toothcare_Appointment_System.Models;

namespace Toothcare_Appointment_System.Data
{
    public class ToothcareAppointmentContext : DbContext
    {
        public ToothcareAppointmentContext(DbContextOptions<ToothcareAppointmentContext> options) : base(options) { }

        public DbSet<Staff> Staff { get; set; }
        public DbSet<Doctors> Doctors { get; set; }
        public DbSet<Patients> Patients { get; set; }
        public DbSet<Appointment> Appointment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One Doctor has many Appointments
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorID)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            // One Patient has many Appointments
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientID)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete
        }
    }
}

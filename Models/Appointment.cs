using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Toothcare_Appointment_System.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentID { get; set; }

        [ForeignKey("Doctor")]
        public int DoctorID { get; set; }
        public Doctors Doctor { get; set; } // Navigation Property

        // Foreign Key for Patient
        [ForeignKey("Patient")]
        public int PatientID { get; set; }
        public Patients Patient { get; set; } // Navigation Property

        public DateTime AppointmentDateTime { get; set; }
        public string AppointmentReason { get; set; } // Description of the appointment
        public string AppointmentStatus { get; set; } // Scheduled, Confirmed, Cancelled, Completed
        public string AppointmentNotes { get; set; } // Notes from the doctor
        public int RoomNumber { get; set; }
        public int AppointmentDuration { get; set; }
        public string AppointmentType { get; set; } // consultation, check up, procedure
    }
}

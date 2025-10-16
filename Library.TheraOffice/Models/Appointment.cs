using Library.TheraOffice.Services;

namespace Library.TheraOffice.Models
{
    public class Appointment
    {
        public int Id {get; set;}
        public int PatientId {get; set;}
        public Patient? Patient {get; set;}
        public int PhysicianId {get; set;}
        public Physician? Physician {get; set;}
        public DateTime ApptDateTime {get; set;}

        public string Display
        {        
            get
            {
                return ToString();
            }
        }
        
        public override string ToString()
        {
            return $"{Id}. Date: {ApptDateTime} - Patient: {PatientId}-{Patient?.Name} with Physician: {PhysicianId}-{Physician?.Name}";
        }

        public Appointment()
        {

        }

        public Appointment(int id)
        {
            var appointmentCopy = AppointmentServiceProxy.Current.Appointments.FirstOrDefault(p => (p?.Id ?? 0) == id);
            
            if (appointmentCopy != null)
            {
                Id = appointmentCopy.Id;
                PatientId = appointmentCopy.PatientId;
                Patient = appointmentCopy.Patient;
                PhysicianId = appointmentCopy.PhysicianId;
                Physician = appointmentCopy.Physician;
                ApptDateTime = appointmentCopy.ApptDateTime;
            }
        }
    }
}
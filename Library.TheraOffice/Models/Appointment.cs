namespace Library.TheraOffice.Models
{
    public class Appointment
    {
        public int Id {get; set;}
        public Patient? Patient {get; set;}
        public Physician? Physician {get; set;}
        public DateTime ApptDateTime {get; set;}

        public override string ToString()
        {
            string patientName = Patient?.Name ?? "Unassigned";
            string physicianName = Physician?.Name ?? "Unassigned";

            return $"{Id}. Date: {ApptDateTime}\n Patient: {patientName}\n Physician: {physicianName}\n";
        }
    }
}
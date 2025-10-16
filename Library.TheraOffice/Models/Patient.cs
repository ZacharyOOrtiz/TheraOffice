using Library.TheraOffice.Services;

namespace Library.TheraOffice.Models
{
    public class Patient
    {
        public string? Name {get; set;}
        public string? Address {get; set;}
        public string? BirthDate {get; set;}
        public string? Race {get; set;}
        public string? Gender {get; set;}
        public string? MedNotes {get; set;}
        public int Id {get; set;}

        public string Display
        {        
            get
            {
                return ToString();
            }
        }

        public override string ToString()
        {
            return $"{Id}. Name: {Name} - Address: {Address} - Birth Date: {BirthDate} - Race: {Race} - Gender: {Gender} - Medical Notes: {MedNotes}";
        }

        public Patient()
        {

        }

        public Patient(int id)
        {
            var patientCopy = PatientServiceProxy.Current.Patients.FirstOrDefault(p => (p?.Id ?? 0) == id);
            
            if (patientCopy != null)
            {
                Id = patientCopy.Id;
                Name = patientCopy.Name;
                Address = patientCopy.Address;
                BirthDate = patientCopy.BirthDate;
                Race = patientCopy.Race;
                Gender = patientCopy.Gender;
                MedNotes = patientCopy.MedNotes;
            }
        }
    }
}
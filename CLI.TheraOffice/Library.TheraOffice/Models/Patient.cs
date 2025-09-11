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

        public override string ToString()
        {
            return $"{Id}. Name: {Name}\n Address: {Address}\n Birth Date: {BirthDate}\n Race: {Race}\n Gender: {Gender}\n Medical Notes: {MedNotes}\n";
        }
    }
}

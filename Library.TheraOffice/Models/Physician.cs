using Library.TheraOffice.Services;

namespace Library.TheraOffice.Models
{
    public class Physician
    {
        public string? Name {get; set;}
        public string? LicenseNum {get; set;}
        public string? GradDate {get; set;}
        public string? Specs {get; set;}
        public int Id {get; set;}
        public List<DateTime?> Schedule {get; set;} = new List<DateTime?>();

        public string Display
        {        
            get
            {
                return ToString();
            }
        }

        public override string ToString()
        {
            return $"{Id}. Name: {Name} - License Number: {LicenseNum} - Graduation Date: {GradDate} - Specializations: {Specs}";
        }

        public Physician()
        {

        }

        public Physician(int id)
        {
            var physicianCopy = PhysicianServiceProxy.Current.Physicians.FirstOrDefault(p => (p?.Id ?? 0) == id);
            
            if (physicianCopy != null)
            {
                Id = physicianCopy.Id;
                Name = physicianCopy.Name;
                LicenseNum = physicianCopy.LicenseNum;
                GradDate = physicianCopy.GradDate;
                Specs = physicianCopy.Specs;
            }
        }
    }
}
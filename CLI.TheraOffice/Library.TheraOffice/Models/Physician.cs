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

        public bool IsAvailable(DateTime apptDateTime)
        {
            // 1. Check for double-booking
            if (Schedule.Contains(apptDateTime))
            {
                return false;
            }

            // 2. Check for valid business hours (Mon-Fri, 8am to 5pm)
            // Checks if hour is before 8 AM or is 5 PM or later
            if (apptDateTime.Hour < 8 || apptDateTime.Hour >= 17) 
            {
                return false;
            }

            // 3. Check for weekdays
            if (apptDateTime.DayOfWeek == DayOfWeek.Saturday || apptDateTime.DayOfWeek == DayOfWeek.Sunday)
            {
                return false;
            }

            return true;
        }

        public override string ToString()
        {
            return $"{Id}. Name: {Name}\n License Number: {LicenseNum}\n Graduation Date: {GradDate}\n Specializations: {Specs}\n";
        }
    }
}

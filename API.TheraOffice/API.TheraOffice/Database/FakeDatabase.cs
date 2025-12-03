using Library.TheraOffice.Models;

namespace API.TheraOffice.Nothing;

public static class FakeDatabase
{
    public static List<Patient> Patients = new List<Patient>
    {
        new Patient{Name = "Man", Address= "Place", BirthDate = "Sometime",
            Gender = "M", Race = "White", MedNotes = "Nothing", Id=1},
        new Patient{Name = "Guy", Address= "Location", BirthDate = "A time",
            Gender = "M", Race = "Black", MedNotes = "Nothing", Id=2},
        new Patient{Name = "Female", Address= "Area", BirthDate = "A point in time",
            Gender = "F", Race = "White", MedNotes = "Nothing", Id=3}
    };
}
using Library.TheraOffice.Models;
using API.TheraOffice.Database;

namespace API.TheraOffice.Enterprise;

public class PatientEC
{
    public Patient? GetById(int id)
    {
        return Filebase.Current.Patients.FirstOrDefault(b => b.Id == id);
    }
    
    public IEnumerable<Patient> GetPatients()
    {
        return Filebase.Current.Patients
            .Take(5)
            .OrderByDescending(p => p.Id);
    }
    
    public Patient? Create(Patient patient)
    {
        // Filebase handles the ID generation and Update logic internally now.
        // We just pass the object to Filebase.
        return Filebase.Current.Create(patient);
    }

    public Patient? Delete(int id)
    {
        // Retrieve the patient first so we can return it
        var patient = GetById(id);
        
        // Execute the delete in Filebase
        if (patient != null)
        {
            Filebase.Current.Delete(id);
        }
        
        return patient;
    }

    public IEnumerable<Patient?> Search(string query)
    {
        return Filebase.Current.Patients.Where
        (p =>
            (p?.Name?.ToUpper()?.Contains(query?.ToUpper() ?? string.Empty) ?? false)
            || (p?.Address?.ToUpper()?.Contains(query?.ToUpper() ?? string.Empty) ?? false)
            || (p?.BirthDate?.ToUpper()?.Contains(query?.ToUpper() ?? string.Empty) ?? false)
            || (p?.Race?.ToUpper()?.Contains(query?.ToUpper() ?? string.Empty) ?? false)
            || (p?.Gender?.ToUpper()?.Contains(query?.ToUpper() ?? string.Empty) ?? false)
            || (p?.MedNotes?.ToUpper()?.Contains(query?.ToUpper() ?? string.Empty) ?? false)
        );
    }
}
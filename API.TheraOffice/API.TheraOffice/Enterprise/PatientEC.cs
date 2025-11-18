using Library.TheraOffice.Models;
using API.TheraOffice.Database;

namespace API.TheraOffice.Enterprise;

public class PatientEC
{
    public Patient? GetById(int id)
    {
        return FakeDatabase.Patients.FirstOrDefault(b => b.Id == id);
    }
    
    public IEnumerable<Patient> GetPatients()
    {
        return FakeDatabase.Patients
            .Take(5)
            .OrderByDescending(p => p.Id);
    }
    
    public Patient? Create(Patient patient)
    {
        if (patient == null)
        {
            return null;
        }
        
        if (patient.Id <= 0) // Add
        {
            var maxId = -1;
            if (FakeDatabase.Patients.Any())
            {
                maxId = FakeDatabase.Patients.Select(b => b?.Id ?? -1).Max();
            }
            else
            {
                maxId = 0;
            }
            patient.Id = ++maxId;
            FakeDatabase.Patients.Add(patient);
        }
        else // Edit
        {
            var patientToEdit = FakeDatabase.Patients.FirstOrDefault(p => (p?.Id ?? 0) == patient.Id);

            if (patientToEdit != null)
            {
                var index = FakeDatabase.Patients.IndexOf(patientToEdit);
                FakeDatabase.Patients.RemoveAt(index);
                FakeDatabase.Patients.Insert(index, patient);
            }
        }

        return patient;
    }

    public Patient? Delete(int id)
    {
        var toRemove = GetById(id);
        if (toRemove != null)
        {
            FakeDatabase.Patients.Remove(toRemove);
        }
        return toRemove;
    }

    public IEnumerable<Patient?> Search(string query)
    {
        return FakeDatabase.Patients.Where
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
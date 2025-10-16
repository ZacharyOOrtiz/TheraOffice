using System;
using System.ComponentModel;
using Library.TheraOffice.Models;

namespace Library.TheraOffice.Services;

public class PatientServiceProxy
{
    private List<Patient?> patientRecords {get; set;}
    private PatientServiceProxy()
    {
        patientRecords = new List<Patient?>();
    }

    private static PatientServiceProxy? instance;
    private static object instanceLock = new object();
    public static PatientServiceProxy Current
    {
        get
        {
            lock (instanceLock)
            {
                if (instance == null)
                {
                    instance = new PatientServiceProxy();
                }
            }

            return instance;
        }
    }

    public List<Patient?> Patients
    {
        get
        {
            return patientRecords;
        }
    }

    public Patient? GetById(int id)
    {
        if (id <= 0)
        {
            return null;
        }
        else
        {
            return Patients.FirstOrDefault(p => p?.Id == id);
        }
    }

    public Patient? Create(Patient? patient)
    {
        if (patient == null)
        {
            return null;
        }

        if (patient.Id <= 0)
        {
            var maxId = -1;
            if (patientRecords.Any())
            {
                maxId = patientRecords.Select(b => b?.Id ?? -1).Max();
            }
            else
            {
                maxId = 0;
            }
            patient.Id = ++maxId;
            patientRecords.Add(patient);
        }
        else
        {
            var patientToEdit = Patients.FirstOrDefault(p => (p?.Id ?? 0) == patient.Id);

            if (patientToEdit != null)
            {
                var index = Patients.IndexOf(patientToEdit);
                Patients.RemoveAt(index);
                patientRecords.Insert(index, patient);
            }
        }

        return patient;
    }

    public Patient? Delete(int id)
    {
        //get patient object
        var patientToDelete = patientRecords
            .Where(b => b != null)
            .FirstOrDefault(b => (b?.Id ?? -1) == id);
        //delete it!
        patientRecords.Remove(patientToDelete);

        return patientToDelete;
    }
}
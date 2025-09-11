using System;
using System.ComponentModel;
using Library.TheraOffice.Models;

namespace Library.TheraOffice.Services;

public class PatientServiceProxy
{
    private List<Patient?> patientRecords;
    private PatientServiceProxy()
    {
        patientRecords = new List<Patient?>();
    }

    private static PatientServiceProxy? instance;
    public static PatientServiceProxy Current
    {
        get
        {
            if (instance == null)
            {
                instance = new PatientServiceProxy();
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

    public Patient? Create(Patient? patient)
    {
        if (patient == null)
        {
            return null;
        }

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

        return patient;
    }
}

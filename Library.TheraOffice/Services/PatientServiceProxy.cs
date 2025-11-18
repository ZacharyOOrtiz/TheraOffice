using System;
using System.ComponentModel;
using Library.TheraOffice.Models;
using Library.TheraOffice.Utilities;
using Library.TheraOffice.Data;
using Newtonsoft.Json;

namespace Library.TheraOffice.Services;

public class PatientServiceProxy
{
    private List<Patient?> patientRecords {get; set;}
    private PatientServiceProxy()
    {
        patientRecords = new List<Patient?>();
        var patientsResponse = new WebRequestHandler().Get("/Patient").Result;
        
        if (patientsResponse != null)
        {
            patientRecords = JsonConvert.DeserializeObject<List<Patient?>>(patientsResponse);
        }
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

    public async Task<Patient?> Create(Patient? patient)
    {
        if (patient == null)
        {
            return null;
        }

        var patientPayload = await new WebRequestHandler().Post("/Patient", patient);
        var patientFromServer = JsonConvert.DeserializeObject<Patient>(patientPayload);
        
        if (patient.Id <= 0) // Add
        {
            /*
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
            patientRecords.Add(patient); */
            patientRecords.Add(patientFromServer);
        }
        else // Edit
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
        // SERVER SIDE
        var response = new WebRequestHandler().Delete($"/Patient/{id}").Result;
        
        // CLIENT SIDE
        //get patient object
        var patientToDelete = patientRecords
            .Where(b => b != null)
            .FirstOrDefault(b => (b?.Id ?? -1) == id);
        //delete it!
        patientRecords.Remove(patientToDelete);

        return patientToDelete;
    }
    
    public async Task<List<Patient>?> Search(string query)
    {
        // Create the payload object that the server expects
        var payload = new QueryRequest { Content = query };
    
        // Send the POST request to the Search endpoint
        var response = await new WebRequestHandler().Post("/Patient/Search", payload);
    
        // Deserialize and return the results
        if (response != null)
        {
            return JsonConvert.DeserializeObject<List<Patient>>(response);
        }
    
        return new List<Patient>();
    }
}
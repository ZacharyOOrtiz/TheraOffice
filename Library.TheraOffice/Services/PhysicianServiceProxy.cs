using System;
using System.ComponentModel;
using Library.TheraOffice.Models;

namespace Library.TheraOffice.Services;

public class PhysicianServiceProxy
{
    private List<Physician?> physicianRecords;
    private PhysicianServiceProxy()
    {
        physicianRecords = new List<Physician?>();
    }

    private static PhysicianServiceProxy? instance;
    public static PhysicianServiceProxy Current
    {
        get
        {
            if (instance == null)
            {
                instance = new PhysicianServiceProxy();
            }

            return instance;
        }
    }

    public List<Physician?> Physicians
    {
        get
        {
            return physicianRecords;
        }
    }

    public Physician? Create(Physician? physician)
    {
        if (physician == null)
        {
            return null;
        }

        var maxId = -1;
        if (physicianRecords.Any())
        {
            maxId = physicianRecords.Select(b => b?.Id ?? -1).Max();
        }
        else
        {
            maxId = 0;
        }
        physician.Id = ++maxId;
        physicianRecords.Add(physician);

        return physician;
    }
}

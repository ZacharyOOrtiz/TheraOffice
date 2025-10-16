using System;
using System.ComponentModel;
using Library.TheraOffice.Models;

namespace Library.TheraOffice.Services;

public class PhysicianServiceProxy
{
    private List<Physician?> physicianRecords {get; set;}
    private PhysicianServiceProxy()
    {
        physicianRecords = new List<Physician?>();
    }

    private static PhysicianServiceProxy? instance;
    private static object instanceLock = new object();
    public static PhysicianServiceProxy Current
    {
        get
        {
            lock (instanceLock)
            {
                if (instance == null)
                {
                    instance = new PhysicianServiceProxy();
                }
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

    public Physician? GetById(int id)
    {
        if (id <= 0)
        {
            return null;
        }
        else
        {
            return Physicians.FirstOrDefault(p => p?.Id == id);
        }
    }

    public Physician? Create(Physician? physician)
    {
        if (physician == null)
        {
            return null;
        }

        if (physician.Id <= 0)
        {
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
        }
        else
        {
            var physicianToEdit = Physicians.FirstOrDefault(p => (p?.Id ?? 0) == physician.Id);

            if (physicianToEdit != null)
            {
                var index = Physicians.IndexOf(physicianToEdit);
                Physicians.RemoveAt(index);
                physicianRecords.Insert(index, physician);
            }
        }

        return physician;
    }

    public Physician? Delete(int id)
    {
        //get physician object
        var physicianToDelete = physicianRecords
            .Where(b => b != null)
            .FirstOrDefault(b => (b?.Id ?? -1) == id);
        //delete it!
        physicianRecords.Remove(physicianToDelete);

        return physicianToDelete;
    }
}
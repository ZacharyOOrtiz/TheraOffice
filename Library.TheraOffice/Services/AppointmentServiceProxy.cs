using System;
using System.ComponentModel;
using Library.TheraOffice.Models;

namespace Library.TheraOffice.Services;

public class AppointmentServiceProxy
{
    private List<Appointment?> appointmentRecords;
    private AppointmentServiceProxy()
    {
        appointmentRecords = new List<Appointment?>();
    }

    private static AppointmentServiceProxy? instance;
    public static AppointmentServiceProxy Current
    {
        get
        {
            if (instance == null)
            {
                instance = new AppointmentServiceProxy();
            }

            return instance;
        }
    }

    public List<Appointment?> Appointments
    {
        get
        {
            return appointmentRecords;
        }
    }

    public Appointment? Create(Appointment? appointment)
    {
        if (appointment == null)
        {
            return null;
        }

        var maxId = -1;
        if (appointmentRecords.Any())
        {
            maxId = appointmentRecords.Select(b => b?.Id ?? -1).Max();
        }
        else
        {
            maxId = 0;
        }
        appointment.Id = ++maxId;
        appointmentRecords.Add(appointment);

        return appointment;
    }
}
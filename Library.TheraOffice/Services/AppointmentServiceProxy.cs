using System;
using System.ComponentModel;
using System.Data.Common;
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
    private static object instanceLock = new object();
    public static AppointmentServiceProxy Current
    {
        get
        {
            lock (instanceLock)
            {
                if (instance == null)
                {
                    instance = new AppointmentServiceProxy();
                }
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
    
    // This method returns a tuple: a bool indicating success, and a string with an error message.
    public (bool IsValid, string ErrorMessage) ValidateAppointment(Appointment? newAppointment)
    {
        if (newAppointment == null)
        {
            return (false, "Appointment data is missing.");
        }

        // Rule 1: Check for valid business hours (Mon-Fri, 8am to 5pm)
        var apptDateTime = newAppointment.ApptDateTime;
        if (apptDateTime.DayOfWeek == DayOfWeek.Saturday || apptDateTime.DayOfWeek == DayOfWeek.Sunday)
        {
            return (false, "Appointments can only be scheduled on weekdays (Monday-Friday).");
        }
        if (apptDateTime.Hour < 8 || apptDateTime.Hour >= 17)
        {
            return (false, "Appointments must be between 8:00 AM and 4:59 PM.");
        }

        // Rule 2: Check for double-booking
        // We check if there's ANY existing appointment in our records that has the same physician AND the same start time.
        bool isDoubleBooked = appointmentRecords.Any(existingAppt => 
            existingAppt?.PhysicianId == newAppointment.PhysicianId && 
            existingAppt?.ApptDateTime == newAppointment.ApptDateTime);

        if (isDoubleBooked)
        {
            return (false, "This physician is already booked at the selected time.");
        }

        // If all checks pass, the appointment is valid
        return (true, string.Empty);
    }

    public Appointment? Create(Appointment? appointment)
    {
        // First, validate the appointment.
        var validationResult = ValidateAppointment(appointment);
        if (!validationResult.IsValid)
        {
            // If not valid, do not create the appointment and return null.
            return null; 
        }
        
        if (appointment == null)
        {
            return null;
        }

        appointment.Patient = PatientServiceProxy.Current.GetById(appointment.PatientId);
        appointment.Physician = PhysicianServiceProxy.Current.GetById(appointment.PhysicianId);

        if (appointment.Id <= 0)
        {
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
        }
        else
        {
            var appointmentToEdit = Appointments.FirstOrDefault(p => (p?.Id ?? 0) == appointment.Id);

            if (appointmentToEdit != null)
            {
                var index = Appointments.IndexOf(appointmentToEdit);
                Appointments.RemoveAt(index);
                appointmentRecords.Insert(index, appointment);
            }
        }

        return appointment;
    }
    
    public Appointment? Delete(int id)
    {
        //get appointment object
        var appointmentToDelete = appointmentRecords
            .Where(b => b != null)
            .FirstOrDefault(b => (b?.Id ?? -1) == id);
        //delete it!
        appointmentRecords.Remove(appointmentToDelete);

        return appointmentToDelete;
    }
}
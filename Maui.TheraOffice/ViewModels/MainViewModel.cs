using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Library.TheraOffice.Models;
using Library.TheraOffice.Services;

namespace Maui.TheraOffice.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    public ObservableCollection<PatientViewModel?> Patients { get; set; }
    public ObservableCollection<PhysicianViewModel?> Physicians { get; set; }
    public ObservableCollection<AppointmentViewModel?> Appointments { get; set; }
    public string? Query { get; set; }
    
    public MainViewModel()
    {
        Patients = new ObservableCollection<PatientViewModel?>(
            PatientServiceProxy
                .Current
                .Patients
                .Where(
                    p => (p?.Name?.ToUpper()?.Contains(Query?.ToUpper() ?? string.Empty) ?? false)
                    || (p?.Address?.ToUpper()?.Contains(Query?.ToUpper() ?? string.Empty) ?? false)
                    || (p?.BirthDate?.ToUpper()?.Contains(Query?.ToUpper() ?? string.Empty) ?? false)
                    || (p?.Race?.ToUpper()?.Contains(Query?.ToUpper() ?? string.Empty) ?? false)
                    || (p?.Gender?.ToUpper()?.Contains(Query?.ToUpper() ?? string.Empty) ?? false)
                    || (p?.MedNotes?.ToUpper()?.Contains(Query?.ToUpper() ?? string.Empty) ?? false)
                )
                .Select(p => new PatientViewModel(p)));
        
        Physicians = new ObservableCollection<PhysicianViewModel?>(
            PhysicianServiceProxy
                .Current
                .Physicians
                .Where(
                    ph => (ph?.Name?.ToUpper()?.Contains(Query?.ToUpper() ?? string.Empty) ?? false)
                    || (ph?.LicenseNum?.ToUpper()?.Contains(Query?.ToUpper() ?? string.Empty) ?? false)
                    || (ph?.GradDate?.ToUpper()?.Contains(Query?.ToUpper() ?? string.Empty) ?? false)
                    || (ph?.Specs?.ToUpper()?.Contains(Query?.ToUpper() ?? string.Empty) ?? false)
                )
                .Select(ph => new PhysicianViewModel(ph)));
        
        // Expand Patient and Physician functionality so that the query compares against Patient.Name and Physician.Name LATER
        Appointments = new ObservableCollection<AppointmentViewModel?>(
            AppointmentServiceProxy
                .Current
                .Appointments
                .Select(a => new AppointmentViewModel(a)));
    }

    public void RefreshPatients()
    {
        // Make sure the list isn't null
        if (Patients == null)
        {
            Patients = new ObservableCollection<PatientViewModel?>();
        }
        else
        {
            // Clear the existing items. The UI will react to this.
            Patients.Clear();
        }

        // Get the updated list of patients from the service
        var patientsFromService = PatientServiceProxy.Current.Patients;
        if (patientsFromService != null)
        {
            // Add the patients back into the collection. The UI will react to this too.
            foreach (var patient in patientsFromService)
            {
                Patients.Add(new PatientViewModel(patient));
            }
        }
    }

    /* FOR POTENTIAL LATER REFACTORING
    public void RefreshPatients()
    {
        NotifyPropertyChanged(nameof(Patients));
    }
    */
    
    public PatientViewModel? SelectedPatient{ get; set; }

    public void RefreshPhysicians()
    {
        // Make sure the list isn't null
        if (Physicians == null)
        {
            Physicians = new ObservableCollection<PhysicianViewModel?>();
        }
        else
        {
            // Clear the existing items. The UI will react to this.
            Physicians.Clear();
        }

        // Get the updated list of physicians from the service
        var physiciansFromService = PhysicianServiceProxy.Current.Physicians;
        if (physiciansFromService != null)
        {
            // Add the physicians back into the collection. The UI will react to this too.
            foreach (var physician in physiciansFromService)
            {
                Physicians.Add(new PhysicianViewModel(physician));
            }
        }
    }

    public PhysicianViewModel? SelectedPhysician{ get; set; }

    public void RefreshAppointments()
    {
        // Make sure the list isn't null
        if (Appointments == null)
        {
            Appointments = new ObservableCollection<AppointmentViewModel?>();
        }
        else
        {
            // Clear the existing items. The UI will react to this.
            Appointments.Clear();
        }

        // Get the updated list of patients from the service
        var appointmentsFromService = AppointmentServiceProxy.Current.Appointments;
        if (appointmentsFromService != null)
        {
            // Add the patients back into the collection. The UI will react to this too.
            foreach (var appointment in appointmentsFromService)
            {
                Appointments.Add(new AppointmentViewModel(appointment));
            }
        }
    }
    
    public AppointmentViewModel? SelectedAppointment{ get; set; }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

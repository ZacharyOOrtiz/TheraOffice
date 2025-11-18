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
                .Select(p => new PatientViewModel(p)));
        
        Physicians = new ObservableCollection<PhysicianViewModel?>(
            PhysicianServiceProxy
                .Current
                .Physicians
                .Select(ph => new PhysicianViewModel(ph)));
        
        Appointments = new ObservableCollection<AppointmentViewModel?>(
            AppointmentServiceProxy
                .Current
                .Appointments
                .Select(a => new AppointmentViewModel(a)));
    }

    /*
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
    */
    
    public async void RefreshPatients()
    {
        // 1. Setup the collection
        if (Patients == null)
        {
            Patients = new ObservableCollection<PatientViewModel?>();
        }
        else
        {
            Patients.Clear();
        }

        List<Patient?>? patientsFromService;

        // 2. Check if we are searching or listing all
        if (!string.IsNullOrEmpty(Query))
        {
            // If there is text in the search box, hit the API search endpoint
            patientsFromService = await PatientServiceProxy.Current.Search(Query);
        }
        else
        {
            // If the search box is empty, show the local cached list
            patientsFromService = PatientServiceProxy.Current.Patients;
        }

        // 3. Update the UI
        if (patientsFromService != null)
        {
            foreach (var patient in patientsFromService)
            {
                Patients.Add(new PatientViewModel(patient));
            }
        }
    }
    
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

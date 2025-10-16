using System;
using Library.TheraOffice.Models;
using Library.TheraOffice.Services;
using System.Windows.Input;

namespace Maui.TheraOffice.ViewModels;

public class AppointmentViewModel
{
    private PatientServiceProxy? _patientSvc;
    private PhysicianServiceProxy? _physicianSvc;
    private AppointmentServiceProxy? _appointmentSvc;
    private List<Appointment?> appointmentList;

    public List<Appointment?> Appointments
    {
        get
        {
            return appointmentList;
        }
    }
    
    public AppointmentViewModel()
    {
        _patientSvc = PatientServiceProxy.Current;
        _physicianSvc = PhysicianServiceProxy.Current;
        _appointmentSvc = AppointmentServiceProxy.Current;

        appointmentList = _appointmentSvc.Appointments;

        foreach (var app in appointmentList)
        {
            if (app != null)
            {
                app.Physician = _physicianSvc.GetById(app.PhysicianId);
                app.Patient = _patientSvc.GetById(app.PatientId);
            }
        }

        Model = new Appointment();
        SetUpCommands();
    }
    
    public AppointmentViewModel(Appointment? model)
    {
        _patientSvc = PatientServiceProxy.Current;
        _physicianSvc = PhysicianServiceProxy.Current;
        _appointmentSvc = AppointmentServiceProxy.Current;

        appointmentList = _appointmentSvc.Appointments;

        foreach (var app in appointmentList)
        {
            if (app != null)
            {
                app.Physician = _physicianSvc.GetById(app.PhysicianId);
                app.Patient = _patientSvc.GetById(app.PatientId);
            }
        }

        Model = model;
        SetUpCommands();
    }
    
    public Appointment? Model { get; set; }
    public ICommand? AppointmentDeleteCommand { get; set; }
    public ICommand? AppointmentEditCommand { get; set; }
    
    private void SetUpCommands()
    {
        AppointmentDeleteCommand = new Command(DoDelete);
        AppointmentEditCommand = new Command((p) => DoEdit(p as AppointmentViewModel));
    }
    
    private void DoDelete()
    {
        if (Model?.Id > 0)
        {
            AppointmentServiceProxy.Current.Delete(Model.Id);
            Shell.Current.GoToAsync("//MainPage");
        }
    }

    private void DoEdit(AppointmentViewModel? avm)
    {
        if (avm == null)
        {
            return;
        }
        var selectedAppointmentId = avm?.Model?.Id ?? 0;
        Shell.Current.GoToAsync($"//Appointment?appointmentId={selectedAppointmentId}");
    }
}

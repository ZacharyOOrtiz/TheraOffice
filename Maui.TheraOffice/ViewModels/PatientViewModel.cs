using System;
using Library.TheraOffice.Models;
using Library.TheraOffice.Services;
using System.Windows.Input;
using System.Runtime.CompilerServices;

namespace Maui.TheraOffice.ViewModels;

public class PatientViewModel
{
    public PatientViewModel()
    {
        Model = new Patient();
        SetUpCommands();
    }
    public PatientViewModel(Patient? model)
    {
        Model = model;
        SetUpCommands();
    }
    
    public Patient? Model { get; set; }
    public ICommand? PatientDeleteCommand { get; set; }
    public ICommand? PatientEditCommand { get; set; }

    private void SetUpCommands()
    {
        PatientDeleteCommand = new Command(DoDelete);
        PatientEditCommand = new Command((p) => DoEdit(p as PatientViewModel));
    }

    private void DoDelete()
    {
        if (Model?.Id > 0)
        {
            PatientServiceProxy.Current.Delete(Model.Id);
            Shell.Current.GoToAsync("//MainPage");
        }
    }

    private void DoEdit(PatientViewModel? pvm)
    {
        if (pvm == null)
        {
            return;
        }
        var selectedPatientId = pvm?.Model?.Id ?? 0;
        Shell.Current.GoToAsync($"//Patient?patientId={selectedPatientId}");
    }
}

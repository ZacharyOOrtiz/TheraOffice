using System;
using Library.TheraOffice.Models;
using Library.TheraOffice.Services;
using System.Windows.Input;
using System.Runtime.CompilerServices;

namespace Maui.TheraOffice.ViewModels;

public class PhysicianViewModel
{
    public PhysicianViewModel()
    {
        Model = new Physician();
        SetUpCommands();
    }
    public PhysicianViewModel(Physician? model)
    {
        Model = model;
        SetUpCommands();
    }
    
    public Physician? Model { get; set; }
    public ICommand? PhysicianDeleteCommand { get; set; }
    public ICommand? PhysicianEditCommand { get; set; }

    private void SetUpCommands()
    {
        PhysicianDeleteCommand = new Command(DoDelete);
        PhysicianEditCommand = new Command((p) => DoEdit(p as PhysicianViewModel));
    }

    private void DoDelete()
    {
        if (Model?.Id > 0)
        {
            PhysicianServiceProxy.Current.Delete(Model.Id);
            Shell.Current.GoToAsync("//MainPage");
        }
    }

    private void DoEdit(PhysicianViewModel? pvm)
    {
        if (pvm == null)
        {
            return;
        }
        var selectedPhysicianId = pvm?.Model?.Id ?? 0;
        Shell.Current.GoToAsync($"//Physician?physicianId={selectedPhysicianId}");
    }
}

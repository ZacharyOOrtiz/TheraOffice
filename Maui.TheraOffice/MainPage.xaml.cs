using System.Reflection.Metadata.Ecma335;
using Maui.TheraOffice.ViewModels;

namespace Maui.TheraOffice;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
		BindingContext = new MainViewModel();
	}

	private void PatientAddClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//Patient?patientId=0");
	}
	
	private void InlinePatientDeleteClicked(object sender, EventArgs e)
    {
		(BindingContext as MainViewModel)?.RefreshPatients();
    }

	private void PhysicianAddClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//Physician?physicianId=0");
	}

	private void InlinePhysicianDeleteClicked(object sender, EventArgs e)
    {
		(BindingContext as MainViewModel)?.RefreshPhysicians();
    }

    private void AppointmentAddClicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("//Appointment?appointmentId=0");
    }
    
    private void InlineAppointmentDeleteClicked(object sender, EventArgs e)
    {
	    (BindingContext as MainViewModel)?.RefreshAppointments();
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
	    (BindingContext as MainViewModel)?.RefreshPatients();
	    (BindingContext as MainViewModel)?.RefreshPhysicians();
	    (BindingContext as MainViewModel)?.RefreshAppointments();
    }
    
	/*
    private void SearchClicked(object sender, EventArgs e)
    {
		(BindingContext as MainViewModel)?.RefreshPatients();
    }
	*/
}

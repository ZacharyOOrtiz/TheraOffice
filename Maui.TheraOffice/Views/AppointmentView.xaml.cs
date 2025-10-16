using Library.TheraOffice.Models;
using Library.TheraOffice.Services;

namespace Maui.TheraOffice.Views;

[QueryProperty(nameof(AppointmentId), "appointmentId")]
public partial class AppointmentView : ContentPage
{
	public AppointmentView()
	{
		InitializeComponent();
	}

    public int AppointmentId { get; set; }
    
    private async void OkClicked(object sender, EventArgs e)
    {
        var newAppointment = BindingContext as Appointment;
        var createdAppointment = AppointmentServiceProxy.Current.Create(newAppointment);

        if (createdAppointment != null)
        {
            // Success! Go back to the main page.
            await Shell.Current.GoToAsync("//MainPage");
        }
        else
        {
            // Failure! Show an alert to the user.
            // We need to re-run validation to get the specific error message.
            var validationResult = AppointmentServiceProxy.Current.ValidateAppointment(newAppointment);
            await DisplayAlert("Unable to Book Appointment", validationResult.ErrorMessage, "OK");
        }
    }

	private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        if (AppointmentId == 0)
        {
            BindingContext = new Appointment();
        }
        else
        {
            BindingContext = new Appointment(AppointmentId);
        }
    }
}
using Library.TheraOffice.Models;
using Library.TheraOffice.Services;

namespace Maui.TheraOffice.Views;

[QueryProperty(nameof(PatientId), "patientId")]
public partial class PatientView : ContentPage
{
	public PatientView()
	{
		InitializeComponent();
	}

    public int PatientId { get; set; }

	private void OkClicked(object sender, EventArgs e)
    {
        //add the patient
        PatientServiceProxy.Current.Create(BindingContext as Patient);

        //go back to the main page
        Shell.Current.GoToAsync("//MainPage");
    }

	private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        if (PatientId == 0)
        {
            BindingContext = new Patient();
        }
        else
        {
            BindingContext = new Patient(PatientId);
        }
    }
}
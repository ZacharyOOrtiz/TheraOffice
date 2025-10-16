using Library.TheraOffice.Models;
using Library.TheraOffice.Services;

namespace Maui.TheraOffice.Views;

[QueryProperty(nameof(PhysicianId), "physicianId")]
public partial class PhysicianView : ContentPage
{
	public PhysicianView()
	{
		InitializeComponent();
	}

    public int PhysicianId { get; set; }

	private void OkClicked(object sender, EventArgs e)
    {
        Console.WriteLine("Entered OkCLicked");
        //add the physician
        PhysicianServiceProxy.Current.Create(BindingContext as Physician);
        Console.WriteLine("Physician created");

        //go back to the main page
        Shell.Current.GoToAsync("//MainPage");
    }

	private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        if (PhysicianId == 0)
        {
            BindingContext = new Physician();
        }
        else
        {
            BindingContext = new Physician(PhysicianId);
        }
    }
}
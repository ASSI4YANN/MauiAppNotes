namespace MauiAppNotesFormation;

public partial class AboutPage : ContentPage
{
	public AboutPage()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		await DisplayAlert("Message", "L'application n'est pas encore disponible sur Play store pour la partager.", "OK");
    }
}
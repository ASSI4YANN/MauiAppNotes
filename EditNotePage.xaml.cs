namespace MauiAppNotesFormation;

public partial class EditNotePage : ContentPage
{
    private string _filePath;

    public EditNotePage(string filePath)
    {
        InitializeComponent();
        _filePath = filePath;
        LoadNote();
    }

    private void LoadNote()
    {
        if (File.Exists(_filePath))
        {
            TextEditor.Text = File.ReadAllText(_filePath);
        }
    }

    private void OnSaveClicked(object sender, EventArgs e)
    {
        File.WriteAllText(_filePath, TextEditor.Text);
        DisplayAlert("Succès", "Note enregistrée.", "OK");
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Suppression", "Voulez-vous vraiment supprimer cette note ?", "Oui", "Non");
        if (confirm)
        {
            File.Delete(_filePath);
            await Navigation.PopAsync(); // Retour à la liste des notes après suppression
        }
    }
}

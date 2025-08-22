/* APPLICATION BLOC NOTES ANDROID 12 SNOW CONE : PARTIE LOGIQUE  Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)*/

using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Collections.ObjectModel;
using FileSystem = Microsoft.VisualBasic.FileSystem;

namespace MauiAppNotesFormation;

public partial class NotesPage : ContentPage
{
    string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MauiNotes");
    List<string> allNotes = new List<string>();

    public NotesPage()
    {
        InitializeComponent();
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
        LoadNotesList();
    }

    private void OnCreateOrOpenFileClicked(object sender, EventArgs e)
    {
        string fileName = FileNameEntry.Text?.Trim();
        if (string.IsNullOrEmpty(fileName))
        {
            DisplayAlert("Erreur", "Veuillez entrer un nom de fichier.", "OK");
            return;
        }

        string filePath = Path.Combine(directoryPath, fileName);
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, ""); // Crée un fichier vide
        }

        LoadNotesList();
        Navigation.PushAsync(new EditNotePage(filePath)); // Ouvre la page d'édition
    }

    private void LoadNotesList()
    {
        allNotes = Directory.GetFiles(directoryPath)
                            .Select(file => Path.GetFileName(file) ?? string.Empty)
                            .OrderBy(name => name)
                            .ToList();

        NotesListView.ItemsSource = allNotes;
    }

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        string searchText = e.NewTextValue?.ToLower() ?? "";
        NotesListView.ItemsSource = allNotes
            .Where(note => note.ToLower().Contains(searchText))
            .ToList();
    }

    private void OnNoteTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item == null) return;
        string selectedFile = e.Item.ToString();
        string filePath = Path.Combine(directoryPath, selectedFile);

        Navigation.PushAsync(new EditNotePage(filePath)); // Ouvre la page d'édition

        // Désélectionner l'élément après l'action
        ((ListView)sender).SelectedItem = null;
    }
}





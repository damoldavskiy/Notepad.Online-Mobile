using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotepadOnlineMobile
{
    public class RenameEventArgs : EventArgs
    {
        public string OldName { get; }
        public string NewName { get; }
        
        public RenameEventArgs(string oldName, string newName)
        {
            OldName = oldName;
            NewName = newName;
        }
    }

    public class EditEventArgs : EventArgs
    {
        public string Name { get; }
        public string NewDescription { get; }

        public EditEventArgs(string name, string newDescription)
        {
            Name = name;
            NewDescription = newDescription;
        }
    }

    public class DeleteEventArgs : EventArgs
    {
        public string Name { get; }

        public DeleteEventArgs(string name)
        {
            Name = name;
        }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditorPage : ContentPage
    {
        public event RenameEventHandler Renamed;
        public event EditEventHandler Edited;
        public event DeleteEventHandler Deleted;

        public delegate void RenameEventHandler(object sender, RenameEventArgs e);
        public delegate void EditEventHandler(object sender, EditEventArgs e);
        public delegate void DeleteEventHandler(object sender, DeleteEventArgs e);

        private string name;
        private string text;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                OnPropertyChanged(nameof(Text));
            }
        }

        public EditorPage(string name)
        {
            InitializeComponent();
            BindingContext = this;

            Name = name;
            //input.Submit.Clicked += Rename_Submit_Clicked;

            LoadData();
        }

        private async Task LoadData()
        {
            IsBusy = true;
            var result = await DataBase.Manager.GetDataAsync(Name);
            IsBusy = false;

            if (result.Item1 != DataBase.ReturnCode.Success)
            {
                await DisplayAlert("Error", $"An error occurred while opening file: {result}", "OK");
                return;
            }

            Text = result.Item3;
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            IsBusy = true;
            var result = await DataBase.Manager.EditTextAsync(Name, Text);

            if (result != DataBase.ReturnCode.Success)
            {
                IsBusy = false;
                await DisplayAlert("Error", $"An error occurred while creating new file: {result}", "OK");
                return;
            }

            string description;
            if (Settings.Storage.UseKeyWords)
                try
                {
                    var words = await CognitiveServices.TextAnalytics.KeyPhrasesAsync(new[] { Text });
                    description = string.Join("; ", words[0]);
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"An error occurred while getting key words: {ex.Message}", "OK");
                    description = Text.Length <= 60 ? Text : Text.Substring(0, 60) + "...";
                }
            else
                description = (Text.Length <= 60 ? Text : Text.Substring(0, 60) + "...").Replace('\n', ' ');

            result = await DataBase.Manager.EditDescriptionAsync(Name, description);
            if (result != DataBase.ReturnCode.Success)
            {
                IsBusy = false;
                await DisplayAlert("Error", $"An error occurred while updating file's description: {result}", "OK");
                return;
            }

            Edited?.Invoke(this, new EditEventArgs(Name, description));

            IsBusy = false;
        }

        private void Rename_Clicked(object sender, EventArgs e)
        {
            input.Text = Name;
            input.IsVisible = true;
        }

        private async void RenameSubmit_Clicked(object sender, EventArgs e)
        {
            input.IsVisible = false;
            var newname = input.Text.Trim();

            if (newname == Name)
                return;

            if (!WindowsNamingRules.IsNameCorrect(newname))
            {
                await DisplayAlert("Error", "The new name contains unacceptable symbols", "OK");
                return;
            }

            IsBusy = true;
            var result = await DataBase.Manager.EditNameAsync(Name, newname);
            IsBusy = false;

            if (result == DataBase.ReturnCode.Success)
            {
                Renamed?.Invoke(this, new RenameEventArgs(Name, newname));
                Name = newname;
            }
            else
            {
                await DisplayAlert("Error", $"An error occurred while renaming: {result}", "OK");
            }
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            if (Settings.Storage.AskDelete)
            {
                var ans = await DisplayActionSheet($"Do you really want to delete {Name}?", null, null, "Yes", "Cancel");
                if (ans == "Cancel")
                    return;
            }

            IsBusy = true;
            var result = await DataBase.Manager.DelDataAsync(Name);
            IsBusy = false;

            if (result != DataBase.ReturnCode.Success)
            {
                await DisplayAlert("Error", $"An error occurred while creating new file: {result}", "OK");
                return;
            }

            Deleted?.Invoke(this, new DeleteEventArgs(Name));
            await Navigation.PopAsync();
        }
    }
}
using System;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace NotepadOnlineMobile
{
    public partial class EditorPage : ContentPage
    {
        public event RenameEventHandler Renamed;
        public event EditEventHandler Edited;
        public event DeleteEventHandler Deleted;

        public delegate void RenameEventHandler(object sender, RenameEventArgs e);
        public delegate void EditEventHandler(object sender, EditEventArgs e);
        public delegate void DeleteEventHandler(object sender, DeleteEventArgs e);

        string name;
        string text;

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

        public int FontSize
        {
            get
            {
                return Settings.Storage.FontSize;
            }
        }

        public EditorPage(string name)
        {
            InitializeComponent();
            BindingContext = this;
            
            Name = name;

            Load();
        }

        public EditorPage(string name, string text)
        {
            InitializeComponent();
            BindingContext = this;

            Name = name;
            Text = text;
        }

        async Task Load()
        {
            IsBusy = true;
            var result = await DataBase.Manager.GetDataAsync(Name);
            IsBusy = false;

            if (result.Item1 != DataBase.ReturnCode.Success)
            {
                await DisplayAlert("Error", $"An error occurred while opening file: {result.Item1}", "OK");
                return;
            }

            Text = result.Item3;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            if (IsBusy || input.IsVisible)
                return;
            IsBusy = true;

            var result = await DataBase.Manager.EditTextAsync(Name, Text);

            if (result != DataBase.ReturnCode.Success)
            {
                IsBusy = false;
                await DisplayAlert("Error", $"An error occurred while creating new file: {result}", "OK");
                return;
            }

            string description;
            if (string.IsNullOrWhiteSpace(Text))
                description = "Empty file";
            else if (Settings.Storage.UseKeyWords)
                try
                {
                    var words = await CognitiveServices.TextAnalytics.KeyPhrasesAsync(new[] { Text });
                    if (words[0].Length > 0)
                        description = string.Join("; ", words[0]);
                    else
                        description = "No key words";
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"An error occurred while getting key words: {ex.Message}", "OK");
                    description = Text;
                }
            else
                description = Text;

            result = await DataBase.Manager.EditDescriptionAsync(Name, description);
            if (result != DataBase.ReturnCode.Success)
            {
                IsBusy = false;
                await DisplayAlert("Error", $"An error occurred while updating file's description: {result}", "OK");
                return;
            }

            Edited?.Invoke(this, new EditEventArgs(Name, description, Text));

            IsBusy = false;
        }

        async void Rename_Clicked(object sender, EventArgs e)
        {
            if (IsBusy || input.IsVisible)
                return;

            input.Text = Name;
            await input.Show();
        }

        async void RenameSubmit_Clicked(object sender, EventArgs e)
        {
            input.Hide();
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

        async void Delete_Clicked(object sender, EventArgs e)
        {
            if (IsBusy || input.IsVisible)
                return;

            if (Settings.Storage.AskDelete)
            {
                var ans = await DisplayActionSheet($"Do you really want to delete {Name}?", null, null, "Yes", "Cancel");
                if (ans != "Yes")
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

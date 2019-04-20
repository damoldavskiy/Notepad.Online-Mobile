using System;
using System.Threading.Tasks;

using Xamarin.Forms;

using static DataBase.ReturnCodeDescriptions;

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

        public string FontFamily
        {
            get
            {
                return Settings.Storage.FontFamily;
            }
        }

        public EditorPage(string name)
        {
            InitializeComponent();
            BindingContext = this;
            
            Name = name;

            Task.Run(Load);
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
                await Navigation.PopAsync();
                await DisplayAlert(Resource.Error, Resource.OpenFileError + " " + result.Item1.GetDescription(), Resource.Ok);
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
                await DisplayAlert(Resource.Error, Resource.SaveFileError + " " + result.GetDescription(), Resource.Ok);
                return;
            }

            string description;
            if (string.IsNullOrWhiteSpace(Text))
                description = Resource.EmptyFile;
            else if (Settings.Storage.UseKeyWords)
                try
                {
                    var words = await CognitiveServices.TextAnalytics.KeyPhrasesAsync(new[] { Text });
                    if (words[0].Length > 0)
                        description = string.Join("; ", words[0]);
                    else
                        description = Resource.NoKeyWords;
                }
                catch (Exception ex)
                {
                    await DisplayAlert(Resource.Error, Resource.GetKeyWordsError + " " + ex.Message, Resource.Ok);
                    description = Text;
                }
            else
                description = Text.Trim();

            if (description.Length > 200)
                description = description.Substring(0, 200);

            result = await DataBase.Manager.EditDescriptionAsync(Name, description);
            if (result != DataBase.ReturnCode.Success)
            {
                IsBusy = false;
                await DisplayAlert(Resource.Error, Resource.UpdatingDescriptionError + " " + result.GetDescription(), Resource.Ok);
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
            var newname = input.Text.Trim();

            if (!WindowsNamingRules.IsNameCorrect(newname))
            {
                await DisplayAlert(Resource.Error, Resource.UnacceptableSymbols, Resource.Ok);
                return;
            }

            _ = input.Hide();

            if (newname == Name)
                return;

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
                await DisplayAlert(Resource.Error, Resource.RenamingError + " " + result.GetDescription(), Resource.Ok);
            }
        }

        async void Delete_Clicked(object sender, EventArgs e)
        {
            if (IsBusy || input.IsVisible)
                return;

            if (Settings.Storage.AskDelete)
            {
                var ans = await DisplayActionSheet(Resource.DoYouWantToDelete + " " + Name + "?", null, null, Resource.Yes, Resource.Cancel);
                if (ans != Resource.Yes)
                    return;
            }

            IsBusy = true;
            var result = await DataBase.Manager.DelDataAsync(Name);
            IsBusy = false;

            if (result != DataBase.ReturnCode.Success)
            {
                await DisplayAlert(Resource.Error, Resource.DeleteFileError + " " + result.GetDescription(), Resource.Ok);
                return;
            }

            Deleted?.Invoke(this, new DeleteEventArgs(Name));
            await Navigation.PopAsync();
        }
    }
}

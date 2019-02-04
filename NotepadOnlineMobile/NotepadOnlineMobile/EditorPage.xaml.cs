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
        public string Name { get; }
        public string NewName { get; }
        
        public RenameEventArgs(string name, string newName)
        {
            Name = name;
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

        event EventHandler PageLoaded;

        string name;

        public EditorPage(string name)
        {
            InitializeComponent();

            this.name = name;

            Title = name;

            var save = new ToolbarItem
            {
                Order = ToolbarItemOrder.Secondary,
                Text = "Save"
            };
            ToolbarItems.Add(save);
            save.Clicked += Save_Clicked;

            var rename = new ToolbarItem
            {
                Order = ToolbarItemOrder.Secondary,
                Text = "Rename"
            };
            ToolbarItems.Add(rename);
            rename.Clicked += Rename_Clicked;
            input.Submit.Clicked += Rename_Submit_Clicked;

            var delete = new ToolbarItem
            {
                Order = ToolbarItemOrder.Secondary,
                Text = "Delete"
            };
            ToolbarItems.Add(delete);
            delete.Clicked += Delete_Clicked;

            PageLoaded += EditorPage_PageLoaded;
            PageLoaded(this, EventArgs.Empty);
        }

        private async void EditorPage_PageLoaded(object sender, EventArgs e)
        {
            loading.IsVisible = true;
            var result = await DataBase.Manager.GetDataAsync(name);
            loading.IsVisible = false;

            if (result.Item1 != DataBase.ReturnCode.Success)
            {
                await DisplayAlert("Error", $"An error occurred while opening file: {result}", "OK");
                return;
            }

            editor.Text = result.Item3;
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            loading.IsVisible = true;
            var result = await DataBase.Manager.EditTextAsync(name, editor.Text);

            if (result != DataBase.ReturnCode.Success)
            {
                loading.IsVisible = false;
                await DisplayAlert("Error", $"An error occurred while creating new file: {result}", "OK");
                return;
            }

            string description;
            string text = editor.Text;
            if ((bool)Settings.Storage.Get("keywords"))
                try
                {
                    description = await Services.TextAnalytics.GetDescriptionAsync(text);
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"An error occurred while getting key words: {ex.Message}", "OK");
                    description = text.Length <= 60 ? text : text.Substring(0, 60) + "...";
                }
            else
                description = (text.Length <= 60 ? text : text.Substring(0, 60) + "...").Replace('\n', ' ');

            result = await DataBase.Manager.EditDescriptionAsync(name, description);
            if (result != DataBase.ReturnCode.Success)
            {
                loading.IsVisible = false;
                await DisplayAlert("Error", $"An error occurred while updating file's description: {result}", "OK");
                return;
            }

            Edited?.Invoke(this, new EditEventArgs(name, description));

            loading.IsVisible = false;
        }

        private void Rename_Clicked(object sender, EventArgs e)
        {
            input.Field.Text = name;
            input.IsVisible = true;
        }

        private async void Rename_Submit_Clicked(object sender, EventArgs e)
        {
            input.IsVisible = false;
            var text = input.Field.Text.Trim();

            if (text == name)
                return;

            if (!WindowsNamingRules.IsNameCorrect(text))
            {
                await DisplayAlert("Error", "The new name contains unacceptable symbols", "OK");
                return;
            }

            loading.IsVisible = true;
            var result = await DataBase.Manager.EditNameAsync(name, text);
            loading.IsVisible = false;

            if (result == DataBase.ReturnCode.Success)
            {
                Renamed?.Invoke(this, new RenameEventArgs(name, text));
                name = text;
                Title = name;
            }
            else
            {
                await DisplayAlert("Error", $"An error occurred while renaming: {result}", "OK");
            }
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            if ((bool)Settings.Storage.Get("askdel"))
            {
                var ans = await DisplayActionSheet($"Do you really want to delete {name}?", null, null, "Yes", "Cancel");
                if (ans == "Cancel")
                    return;
            }

            loading.IsVisible = true;
            var result = await DataBase.Manager.DelDataAsync(name);
            loading.IsVisible = false;

            if (result != DataBase.ReturnCode.Success)
            {
                await DisplayAlert("Error", $"An error occurred while creating new file: {result}", "OK");
                return;
            }

            Deleted?.Invoke(this, new DeleteEventArgs(name));
            await Navigation.PopAsync();
        }
    }
}
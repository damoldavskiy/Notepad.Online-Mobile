using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Plugin.Media;
using Plugin.Media.Abstractions;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotepadOnlineMobile
{
    class DataItem : INotifyPropertyChanged
    {
        string name;
        string description;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExplorerPage : ContentPage
    {
        event EventHandler PageLoaded;

        ObservableCollection<DataItem> items;

        public ExplorerPage()
        {
            InitializeComponent();

            var photo = new ToolbarItem
            {
                Text = "Photo"
            };
            ToolbarItems.Add(photo);
            photo.Clicked += AddPhoto_Clicked;

            var add = new ToolbarItem
            {
                Text = "New"
            };
            ToolbarItems.Add(add);
            add.Clicked += AddItem_Clicked;
            
            PageLoaded += ExplorerPage_PageLoaded;
            PageLoaded(this, EventArgs.Empty);
            
            menu.ItemSelected += Menu_ItemSelected;
        }

        private async void ExplorerPage_PageLoaded(object sender, EventArgs e)
        {
            loading.IsVisible = true;
            var result = await DataBase.Manager.GetNamesAsync();

            items = new ObservableCollection<DataItem>();
            menu.ItemsSource = items;

            if (result.Item1 != DataBase.ReturnCode.Success)
            {
                await DisplayAlert("Error", $"An error occurred while observing files: {result.Item1}", "OK");
                loading.IsVisible = false;
                return;
            }

            foreach (var name in result.Item2)
            {
                var item = await DataBase.Manager.GetDataAsync(name);

                if (result.Item1 != DataBase.ReturnCode.Success)
                {
                    await DisplayAlert("Error", $"An error occurred while downloading file {name}: {result.Item1}", "OK");
                    loading.IsVisible = false;
                    return;
                }

                items.Add(new DataItem { Name = name, Description = item.Item2 });
            }

            loading.IsVisible = false;
        }

        private async Task CreateFileAsync(string name="New file", string desc="Empty file", string text="")
        {
            if (loading.IsVisible)
                return;
            
            for (int i = 0; items.Count(c => c.Name == name + ".txt") > 0; name = "New file " + ++i) ;
            name += ".txt";

            loading.IsVisible = true;
            var result = await DataBase.Manager.AddDataAsync(name, desc, text);
            loading.IsVisible = false;

            if (result != DataBase.ReturnCode.Success)
            {
                await DisplayAlert("Error", $"An error occurred while creating file: {result}", "OK");
                return;
            }

            items.Add(new DataItem() { Name = name, Description = desc });
        }

        private void Update_Tapped(object sender, EventArgs e)
        {
            ExplorerPage_PageLoaded(sender, e);
        }

        private async void AddItem_Clicked(object sender, EventArgs e)
        {
            await CreateFileAsync();
        }

        private async void AddPhoto_Clicked(object sender, EventArgs e)
        {
            if (loading.IsVisible)
                return;

            try
            {
                if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsPickPhotoSupported)
                {
                    MediaFile photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()
                    {
                        Name = "image",
                        PhotoSize = PhotoSize.Medium
                    });

                    if (photo == null)
                        return;
                    
                    loading.IsVisible = true;
                    var text = await CognitiveServices.ComputerVision.OCRAsync(photo.Path);
                    loading.IsVisible = false;

                    await CreateFileAsync("New file", "Recognized text from photo", text);
                }
                else
                    await DisplayAlert("Error", "Camera is not available", "OK");
            }
            catch (Exception ex)
            {
                loading.IsVisible = false;
                await DisplayAlert("Error", $"An error occurred while getting text from image: {ex.Message}", "OK");
            }
        }

        private void Menu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (DataItem)e.SelectedItem;
            
            if (item == null)
                return;

            menu.SelectedItem = null;

            var editorPage = new EditorPage(item.Name);
            editorPage.Renamed += (object r_sender, RenameEventArgs r_e) =>
            {
                item.Name = r_e.NewName;
            };
            editorPage.Edited += (object e_sender, EditEventArgs e_e) =>
            {
                item.Description = e_e.NewDescription;
            };
            editorPage.Deleted += (object d_sender, DeleteEventArgs d_e) =>
            {
                items.Remove(item);
            };

            Navigation.PushAsync(editorPage);
        }
    }
}
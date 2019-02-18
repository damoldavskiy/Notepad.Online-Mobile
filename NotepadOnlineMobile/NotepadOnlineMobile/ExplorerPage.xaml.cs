using Plugin.Media;
using Plugin.Media.Abstractions;

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace NotepadOnlineMobile
{   
    public partial class ExplorerPage : ContentPage
    {
        public class DataItem : INotifyPropertyChanged
        {
            string name;
            string description;
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
                    description = (value.Length <= 60 ? value : value.Substring(0, 60) + "...").Replace('\n', ' ');
                    OnPropertyChanged("Description");
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
                    OnPropertyChanged("Text");
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            void OnPropertyChanged(string name)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

        private ObservableCollection<DataItem> items;

        public ObservableCollection<DataItem> Items
        {
            get
            {
                return items;
            }
            set
            {
                items = value;
                OnPropertyChanged("Items");
            }
        }

        public ExplorerPage()
        {
            InitializeComponent();
            BindingContext = this;

            Load();
        }

        private async Task Load()
        {
            IsBusy = true;
            var result = await DataBase.Manager.GetNamesAsync();

            if (result.Item1 != DataBase.ReturnCode.Success)
            {
                await DisplayAlert("Error", $"An error occurred while observing files: {result.Item1}", "OK");
                IsBusy = false;
                return;
            }

            Items = new ObservableCollection<DataItem>();

            foreach (var name in result.Item2)
            {
                var item = await DataBase.Manager.GetDataAsync(name);

                if (result.Item1 != DataBase.ReturnCode.Success)
                {
                    await DisplayAlert("Error", $"An error occurred while downloading file {name}: {result.Item1}", "OK");
                    IsBusy = false;
                    return;
                }

                Items.Add(new DataItem { Name = name, Description = item.Item2, Text = Settings.Storage.Preload ? item.Item3 : null });
            }

            IsBusy = false;
        }

        private async Task CreateFileAsync(string name="New file", string desc="New empty file", string text="")
        {
            for (int i = 0; items.Count(c => c.Name == name) > 0; name = "New file " + ++i) ;

            IsBusy = true;
            var result = await DataBase.Manager.AddDataAsync(name, desc, text);
            IsBusy = false;

            if (result != DataBase.ReturnCode.Success)
            {
                await DisplayAlert("Error", $"An error occurred while creating file: {result}", "OK");
                return;
            }

            items.Add(new DataItem() { Name = name, Description = desc });
        }

        private async void AddItem_Clicked(object sender, EventArgs e)
        {
            if (IsBusy)
                return;

            await CreateFileAsync();
        }

        private async void AddItemSpecified_Clicked(object sender, EventArgs e)
        {
            if (IsBusy)
                return;

            var action = await DisplayActionSheet("New file", "Cancel", null, "Empty file", "Pick from gallery", "Take photo");

            MediaFile photo = null;
            var options = new StoreCameraMediaOptions()
            {
                Name = "image",
                PhotoSize = PhotoSize.Medium
            };

            switch (action)
            {
                case "Empty file":
                    AddItem_Clicked(sender, e);
                    return;
                
                case "Pick from gallery":
                    if (CrossMedia.Current.IsPickPhotoSupported)
                        try
                        {
                            photo = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions()
                            {
                                PhotoSize = PhotoSize.Medium
                            });
                        }
                        catch (MediaPermissionException)
                        {
                            await DisplayAlert("Error", "No permission to pick photo", "OK");
                        }
                    else
                        await DisplayAlert("Error", "Gallery is not available", "OK");
                    break;
                
                case "Take photo":
                    if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
                        try
                        {
                            photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()
                            {
                                PhotoSize = PhotoSize.Medium
                            });
                        }
                        catch (MediaPermissionException)
                        {
                            await DisplayAlert("Error", "No permission to take photo", "OK");
                        }
                    else
                        await DisplayAlert("Error", "Camera is not available", "OK");
                    break;

                default:
                    return;
            }

            if (photo == null)
                return;
                    
            IsBusy = true;
            var text = await CognitiveServices.ComputerVision.OCRAsync(photo.Path);
            await CreateFileAsync("New file", "Recognized text from photo", text);
            IsBusy = false;
        }

        private async void Menu_Refreshing(object sender, EventArgs e)
        {
            ((ListView)sender).IsRefreshing = false;
            await Load();
        }

        private async void Menu_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = (DataItem)e.Item;

            EditorPage editorPage;
            if (Settings.Storage.Preload)
                editorPage = new EditorPage(item.Name, item.Text);
            else
                editorPage = new EditorPage(item.Name);

            editorPage.Renamed += (object r_sender, RenameEventArgs r_e) =>
            {
                item.Name = r_e.NewName;
            };
            editorPage.Edited += (object e_sender, EditEventArgs e_e) =>
            {
                item.Description = e_e.NewDescription;
                if (Settings.Storage.Preload)
                    item.Text = e_e.NewText;
            };
            editorPage.Deleted += (object d_sender, DeleteEventArgs d_e) =>
            {
                items.Remove(item);
            };
            
            await Navigation.PushAsync(editorPage);
        }
    }
}
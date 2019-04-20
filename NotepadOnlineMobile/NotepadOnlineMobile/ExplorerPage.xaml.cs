using Plugin.Media;
using Plugin.Media.Abstractions;

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;

using static DataBase.ReturnCodeDescriptions;

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
                    OnPropertyChanged(nameof(Name));
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
                    OnPropertyChanged(nameof(Description));
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

            public event PropertyChangedEventHandler PropertyChanged;

            public void OnPropertyChanged(string name)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

        ObservableCollection<DataItem> items;

        public ObservableCollection<DataItem> Items
        {
            get
            {
                return items;
            }
            set
            {
                items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        public ExplorerPage()
        {
            InitializeComponent();
            BindingContext = this;
            
            Task.Run(Load);
        }

        async Task Load()
        {
            IsBusy = true;
            string[] names, descriptions, text = new string[0];

            var result = await DataBase.Manager.GetNamesAsync();
            if (result.Item1 != DataBase.ReturnCode.Success)
            {
                await DisplayAlert(Resource.Error, Resource.ObservingFilesError + " " + result.Item1.GetDescription(), Resource.Ok);
                IsBusy = false;
                return;
            }
            names = result.Item2;

            result = await DataBase.Manager.GetDescriptionsAsync();
            if (result.Item1 != DataBase.ReturnCode.Success)
            {
                await DisplayAlert(Resource.Error, Resource.ObservingDescriptionsError + " " + result.Item1.GetDescription(), Resource.Ok);
                IsBusy = false;
                return;
            }
            descriptions = result.Item2;
            
            if (Settings.Storage.Preload)
            {
                result = await DataBase.Manager.GetTextAsync();
                if (result.Item1 != DataBase.ReturnCode.Success)
                {
                    await DisplayAlert(Resource.Error, Resource.ObservingDataError, Resource.Ok);
                    IsBusy = false;
                    return;
                }
                text = result.Item2;
            }

            Items = new ObservableCollection<DataItem>();

            for (int i = 0; i < names.Length; i++)
                Items.Add(new DataItem { Name = names[i], Description = descriptions[i], Text = Settings.Storage.Preload ? text[i] : null });

            IsBusy = false;
        }

        async Task CreateFileAsync(string name, string desc, string text="")
        {
            for (int i = 0; items.Count(c => c.Name == name) > 0; name = Resource.NewFile + " " + ++i) ;

            IsBusy = true;
            var result = await DataBase.Manager.AddDataAsync(name, desc, text);
            IsBusy = false;

            if (result != DataBase.ReturnCode.Success)
            {
                await DisplayAlert(Resource.Error, Resource.CreatingFileError + " " + result.GetDescription(), Resource.Ok);
                return;
            }

            var item = new DataItem
            {
                Name = name,
                Description = desc,
                Text = Settings.Storage.Preload ? text : null
            };
            items.Add(item);

            Menu_ItemTapped(this, new ItemTappedEventArgs(items, item));
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            if (IsBusy)
                return;

            await CreateFileAsync(Resource.NewFile, Resource.NewEmptyFile, "");
        }

        async void AddItemSpecified_Clicked(object sender, EventArgs e)
        {
            if (IsBusy)
                return;

            var action = await DisplayActionSheet(Resource.NewFile, Resource.Cancel, null, Resource.EmptyFile, Resource.PickFromGallery, Resource.TakePhoto);

            MediaFile photo = null;
            var options = new StoreCameraMediaOptions
            {
                Name = "image",
                PhotoSize = PhotoSize.Medium
            };

            if (action == Resource.EmptyFile)
                AddItem_Clicked(sender, e);

            else if (action == Resource.PickFromGallery)
            {
                if (CrossMedia.Current.IsPickPhotoSupported)
                    try
                    {
                        photo = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                        {
                            PhotoSize = PhotoSize.Medium
                        });
                    }
                    catch (MediaPermissionException)
                    {
                        await DisplayAlert(Resource.Error, Resource.NoPickPhotoPermission, Resource.Ok);
                    }
                else
                    await DisplayAlert(Resource.Error, Resource.GalleryNotAvailable, Resource.Ok);
            }
            else if (action == Resource.TakePhoto)
            {
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
                        await DisplayAlert(Resource.Error, Resource.NoTakePhotoPermission, Resource.Ok);
                    }
                else
                    await DisplayAlert(Resource.Error, Resource.CameraNotAvailable, Resource.Ok);
            }
            else
                return;

            if (photo == null)
                return;
                    
            IsBusy = true;
            var text = await CognitiveServices.ComputerVision.OCRAsync(photo.Path);
            await CreateFileAsync(Resource.NewFile, Resource.RecognizedText, text);
            IsBusy = false;
        }

        async void Menu_Refreshing(object sender, EventArgs e)
        {
            ((ListView)sender).IsRefreshing = false;
            await Load();
        }

        async void Menu_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (IsBusy)
                return;
            IsBusy = true;
            
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

            IsBusy = false;
        }
    }
}

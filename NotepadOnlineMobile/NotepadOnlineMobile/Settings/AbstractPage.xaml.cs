using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace NotepadOnlineMobile.Settings
{
    public partial class AbstractPage : ContentPage
	{
        private ObservableCollection<SettingsItem> items;

        public ObservableCollection<SettingsItem> Items
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

		public AbstractPage(string title)
		{
			InitializeComponent();
            BindingContext = this;

            Title = title;
            Items = new ObservableCollection<SettingsItem>();
		}

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = (SettingsItem)e.Item;
            item.Action(item);
        }
    }
}
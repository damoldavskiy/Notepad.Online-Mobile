using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotepadOnlineMobile.Settings
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AbstractPage : ContentPage
	{
        public ObservableCollection<SettingsItem> Items { get; set; } = new ObservableCollection<SettingsItem>();

		public AbstractPage(string title)
		{
			InitializeComponent();

            Title = title;
            list.ItemsSource = Items;
		}

        private void ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (SettingsItem)e.SelectedItem;
            ((ListView)sender).SelectedItem = null;
            if (item == null)
                return;

            item.Action(item);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NotepadOnlineMobile
{
    enum MenuItemType
    {
        Explorer,
        Settings,
        About
    }

    class MenuItem
    {
        public string Title { get; set; }
        public string Image { get; set; }
        public MenuItemType Type { get; set; }
    }

    public partial class MainPage : MasterDetailPage
    {
        public string UserName
        {
            get
            {
                return DataBase.Manager.Login;
            }
        }

        public MainPage()
        {
            InitializeComponent();

            BindingContext = this;

            var items = new List<MenuItem>();
            items.Add(new MenuItem { Title = "Explorer", Image = "list.png", Type = MenuItemType.Explorer});
            items.Add(new MenuItem { Title = "Settings", Image = "settings.png", Type = MenuItemType.Settings });
            items.Add(new MenuItem { Title = "About", Image = "about.png", Type = MenuItemType.About });

            menu.ItemsSource = items;
            menu.ItemSelected += Menu_ItemSelected;

            Detail = new NavigationPage(new ExplorerPage());
        }

        private void Menu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            IsPresented = false;
            var item = (MenuItem)e.SelectedItem;

            if (item == null)
                return;

            menu.SelectedItem = null;

            var currentPage = ((NavigationPage)Detail).CurrentPage;
            switch (item.Type)
            {
                case MenuItemType.Explorer:
                    if (!(currentPage is ExplorerPage))
                        Detail = new NavigationPage(new ExplorerPage());
                    break;
                case MenuItemType.Settings:
                    if (!(currentPage is Settings.AbstractPage))
                        Detail = new NavigationPage(new Settings.RootPage());
                    break;
                case MenuItemType.About:
                    if (!(currentPage is AboutPage))
                        Detail = new NavigationPage(new AboutPage());
                    break;
            }
        }
    }
}

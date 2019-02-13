using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace NotepadOnlineMobile
{
    public partial class MainPage : MasterDetailPage
    {
        public enum MenuItemType
        {
            Explorer,
            Settings,
            About
        }

        public class MenuItem
        {
            public string Title { get; set; }
            public string Image { get; set; }
            public MenuItemType Type { get; set; }
        }

        public string User
        {
            get
            {
                return DataBase.Manager.Login;
            }
        }

        public ObservableCollection<MenuItem> Items
        {
            get
            {
                return new ObservableCollection<MenuItem>()
                {
                    new MenuItem { Title = "Explorer", Image = "list.png", Type = MenuItemType.Explorer},
                    new MenuItem { Title = "Settings", Image = "settings.png", Type = MenuItemType.Settings },
                    new MenuItem { Title = "About", Image = "about.png", Type = MenuItemType.About }
                };
            }
        }

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
            Detail = new NavigationPage(new ExplorerPage());
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            IsPresented = false;
            var currentPage = ((NavigationPage)Detail).CurrentPage;

            switch (((MenuItem)e.Item).Type)
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

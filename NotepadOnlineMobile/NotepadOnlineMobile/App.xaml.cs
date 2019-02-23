using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace NotepadOnlineMobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Settings.Storage.Initialize();
            
            DependencyService.Get<IColorSetter>().SetStatusBarColor((Color)Resources["PrimaryDark"]);

            MainPage = new LoginRegisterPage();
        }

        protected override void OnSleep()
        {
            Settings.Storage.Email = DataBase.Manager.Email;
            Settings.Storage.Password = DataBase.Manager.Password;
            Settings.Storage.Token = DataBase.Manager.Token;
        }
    }
}

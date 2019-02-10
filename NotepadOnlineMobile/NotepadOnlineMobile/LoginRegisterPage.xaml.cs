using Xamarin.Forms;

namespace NotepadOnlineMobile
{
    public partial class LoginRegisterPage : TabbedPage
    {
        public LoginRegisterPage()
        {
            InitializeComponent();

            var login = new LoginPage();
            var register = new NavigationPage(new RegisterPage());
            login.Title = "Login";
            register.Title = "Register";
            
            Children.Add(login);
            Children.Add(register);
        }
    }
}

using System;

using Xamarin.Forms;

namespace NotepadOnlineMobile
{
    public partial class LoginPage : ContentPage
    {
        private string email;
        private string password;

        public string Email
        {
            get
            {
                return email ?? "";
            }
            set
            {
                email = value?.Trim();
                OnPropertyChanged("Email");
            }
        }

        public string Password
        {
            get
            {
                return password ?? "";
            }
            set
            {
                password = value?.Trim();
                OnPropertyChanged("Password");
            }
        }

        public LoginPage()
		{
			InitializeComponent();
            InitializeLogic();
            BindingContext = this;
        }

        private async void InitializeLogic()
        {
            Settings.Storage.Load();
            var login = Settings.Storage.Get("login").ToString();
            var password = Settings.Storage.Get("password").ToString();
            var token = Settings.Storage.Get("token").ToString();

            if (login != "")
            {
                Email = login;
                Password = password;

                if (!(bool)Settings.Storage.Get("autoreg"))
                    return;

                IsBusy = true;
                var result = await DataBase.Manager.AuthorizeAsync(login, password, token);
                IsBusy = false;

                if (result != DataBase.ReturnCode.Success)
                {
                    await DisplayAlert("Error", $"An error occurred during login: {result}", "OK");
                    return;
                }

                Application.Current.MainPage = new MainPage();
            }
        }

        private async void Signin_Clicked(object sender, EventArgs e)
        {
            IsBusy = true;
            var result = await DataBase.Manager.AuthorizeAsync(Email, Password);
            IsBusy = false;
            
            if (result != DataBase.ReturnCode.Success)
            {
                await DisplayAlert("Error", $"An error occurred during login: {result}", "OK");
                return;
            }
            
            Settings.Storage.Set("login", DataBase.Manager.Login);
            Settings.Storage.Set("password", DataBase.Manager.Password);
            Settings.Storage.Set("token", DataBase.Manager.Token);

            Application.Current.MainPage = new MainPage();
        }
    }
}
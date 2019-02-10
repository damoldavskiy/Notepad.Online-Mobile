using System;

using Xamarin.Forms;

namespace NotepadOnlineMobile
{
    public partial class LoginPage : ContentPage
	{
        public LoginPage()
		{
			InitializeComponent();
            InitializeLogin();

            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void InitializeLogin()
        {
            Settings.Storage.Load();
            var login = Settings.Storage.Get("login").ToString();
            var password = Settings.Storage.Get("password").ToString();
            var token = Settings.Storage.Get("token").ToString();

            if (login != "")
            {
                emailEntry.Text = login.ToString();
                passwordEntry.Text = password.ToString();

                if (!(bool)Settings.Storage.Get("autoreg"))
                    return;

                loading.IsVisible = true;
                var result = await DataBase.Manager.AuthorizeAsync(login, password, token);
                loading.IsVisible = false;

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
            var email = emailEntry.Text?.Trim() ?? "";
            var password = passwordEntry.Text?.Trim() ?? "";

            loading.IsVisible = true;
            var result = await DataBase.Manager.AuthorizeAsync(email, password);
            loading.IsVisible = false;
            
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

        private async void Register_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }
    }
}
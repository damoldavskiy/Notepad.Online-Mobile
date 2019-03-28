using System;

using Xamarin.Forms;

namespace NotepadOnlineMobile
{
    public partial class LoginPage : ContentPage
    {
        string email;
        string password;

        public string Email
        {
            get
            {
                return email ?? "";
            }
            set
            {
                email = value?.Trim();
                OnPropertyChanged(nameof(Email));
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
                OnPropertyChanged(nameof(Password));
            }
        }

        public LoginPage()
		{
			InitializeComponent();
            BindingContext = this;
            NavigationPage.SetHasNavigationBar(this, false);
            
            Load();
        }

        async void Load()
        {
            if (Settings.Storage.Email != "")
            {
                Email = Settings.Storage.Email;
                Password = Settings.Storage.Password;

                if (!Settings.Storage.AutoLogin)
                    return;

                IsBusy = true;
                var result = await DataBase.Manager.LoginAsync(Email, Password, Settings.Storage.Token);
                IsBusy = false;

                if (result != DataBase.ReturnCode.Success)
                {
                    await DisplayAlert("Error", $"An error occurred during login: {result}", "OK");
                    return;
                }

                Application.Current.MainPage = new MainPage();
            }
        }

        async void Signin_Clicked(object sender, EventArgs e)
        {
            IsBusy = true;
            var result = await DataBase.Manager.LoginAsync(Email, Password);
            IsBusy = false;
            
            if (result != DataBase.ReturnCode.Success)
            {
                await DisplayAlert("Error", $"An error occurred during login: {result}", "OK");
                return;
            }
            
            Application.Current.MainPage = new MainPage();
        }

        async void Forgot_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new RecoveryPage());
        }
    }
}

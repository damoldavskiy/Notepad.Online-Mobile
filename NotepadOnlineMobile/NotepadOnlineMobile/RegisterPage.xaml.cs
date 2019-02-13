using System;

using Xamarin.Forms;

namespace NotepadOnlineMobile
{
    public partial class RegisterPage : ContentPage
    {
        private string email;
        private string password;
        private string confirmPassword;

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

        public string ConfirmPassword
        {
            get
            {
                return confirmPassword ?? "";
            }
            set
            {
                confirmPassword = value?.Trim();
                OnPropertyChanged("ConfirmPassword");
            }
        }

        public RegisterPage()
		{
			InitializeComponent();
            BindingContext = this;
            NavigationPage.SetHasNavigationBar(this, false);
		}

        private async void Register_Clicked(object sender, EventArgs e)
        {
            if (Password != ConfirmPassword)
            {
                await DisplayAlert("Error", "You should confirm your password by typing it to the needed box", "OK");
                return;
            }

            IsBusy = true;
            var result = await DataBase.Manager.RegisterAsync(Email, Password);
            IsBusy = false;

            if (result != DataBase.ReturnCode.Success)
            {
                await DisplayAlert("Error", $"An error occurred during creating new user: {result}", "OK");
                return;
            }

            await Navigation.PushModalAsync(new ConfirmPage());
        }
    }
}
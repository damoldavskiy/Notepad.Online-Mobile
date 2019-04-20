using System;

using Xamarin.Forms;

using static DataBase.ReturnCodeDescriptions;

namespace NotepadOnlineMobile
{
    public partial class RegisterPage : ContentPage
    {
        string email;
        string password;
        string confirmPassword;

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

        public string ConfirmPassword
        {
            get
            {
                return confirmPassword ?? "";
            }
            set
            {
                confirmPassword = value?.Trim();
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        public RegisterPage()
		{
			InitializeComponent();
            BindingContext = this;
            NavigationPage.SetHasNavigationBar(this, false);
		}

        async void Register_Clicked(object sender, EventArgs e)
        {
            if (Password != ConfirmPassword)
            {
                await DisplayAlert(Resource.Error, Resource.TypeNewPassword, Resource.Ok);
                return;
            }

            IsBusy = true;
            var result = await DataBase.Manager.RegisterAsync(Email, Password);
            IsBusy = false;

            if (result != DataBase.ReturnCode.Success)
            {
                await DisplayAlert(Resource.Error, Resource.RegistrationError + " " + result.GetDescription(), Resource.Ok);
                return;
            }

            await Navigation.PushModalAsync(new ConfirmPage());
        }
    }
}

using System;

using Xamarin.Forms;

namespace NotepadOnlineMobile
{
    public partial class RecoveryPage : ContentPage
    {
        private string email;
        private string newPassword;
        private string confirmNewPassword;

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

        public string NewPassword
        {
            get
            {
                return newPassword ?? "";
            }
            set
            {
                newPassword = value?.Trim();
                OnPropertyChanged("NewPassword");
            }
        }

        public string ConfirmNewPassword
        {
            get
            {
                return confirmNewPassword ?? "";
            }
            set
            {
                confirmNewPassword = value?.Trim();
                OnPropertyChanged("ConfirmNewPassword");
            }
        }

        public RecoveryPage()
		{
			InitializeComponent();
            BindingContext = this;
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (NewPassword != ConfirmNewPassword)
            {
                await DisplayAlert("Error", "You should confirm new password by typing it to the needed box", "OK");
                return;
            }

            IsBusy = true;
            var result = await DataBase.Manager.RecoveryAsync(Email, NewPassword);
            IsBusy = false;

            if (result != DataBase.ReturnCode.Success)
            {
                await DisplayAlert("Error", $"An error occurred during recovering password: {result}", "OK");
                return;
            }

            await Navigation.PushModalAsync(new ConfirmRecoveryPage());
        }
    }
}
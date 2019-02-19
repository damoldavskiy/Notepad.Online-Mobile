using System;

using Xamarin.Forms;

namespace NotepadOnlineMobile
{
    public partial class ChangePasswordPage : ContentPage
    {
        string password;
        string newPassword;
        string confirmNewPassword;

        public string Password
        {
            get
            {
                return password ?? "";
            }
            set
            {
                password = value.Trim();
                OnPropertyChanged(nameof(Password));
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
                OnPropertyChanged(nameof(NewPassword));
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
                OnPropertyChanged(nameof(ConfirmNewPassword));
            }
        }

        public ChangePasswordPage()
		{
			InitializeComponent();
            BindingContext = this;
        }

        async void Change_Clicked(object sender, EventArgs e)
        {
            if (NewPassword != ConfirmNewPassword)
            {
                await DisplayAlert("Error", "You should confirm new password by typing it to the needed box", "OK");
                return;
            }

            IsBusy = true;
            var result = await DataBase.Manager.ChangePasswordAsync(NewPassword);
            IsBusy = false;

            if (result != DataBase.ReturnCode.Success)
            {
                await DisplayAlert("Error", $"An error occurred during changing password: {result}", "OK");
                return;
            }

            Settings.Storage.Password = DataBase.Manager.Password;

            await DisplayAlert("Success", "Password changed successfuly", "OK");
        }
    }
}

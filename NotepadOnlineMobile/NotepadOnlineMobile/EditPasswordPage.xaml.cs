using System;

using Xamarin.Forms;

namespace NotepadOnlineMobile
{
    public partial class EditPasswordPage : ContentPage
    {
        private string oldPassword;
        private string newPassword;
        private string confirmNewPassword;

        public string OldPassword
        {
            get
            {
                return oldPassword ?? "";
            }
            set
            {
                oldPassword = value.Trim();
                OnPropertyChanged("OldPassword");
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

        public EditPasswordPage()
		{
			InitializeComponent();
            BindingContext = this;
        }

        private async void Change_Clicked(object sender, EventArgs e)
        {
            if (NewPassword != ConfirmNewPassword)
            {
                await DisplayAlert("Error", "You should confirm new password by typing it to the needed box", "OK");
                return;
            }

            IsBusy = true;
            //var result = await DataBase.Manager.EditPasswordAsync(DataBase.Manager.Login, OldPassword, newPassword);
            var result = DataBase.ReturnCode.NoConnection;
            IsBusy = false;

            if (result != DataBase.ReturnCode.Success)
            {
                await DisplayAlert("Error", $"An error occurred during changing password: {result}", "OK");
                return;
            }

            Settings.Storage.Set("password", NewPassword);

            await DisplayAlert("Success", "Password changed successfuly", "OK");
        }
    }
}
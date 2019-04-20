using System;

using Xamarin.Forms;

using static DataBase.ReturnCodeDescriptions;

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
            if (Password != DataBase.Manager.Password)
            {
                await DisplayAlert(Resource.Error, Resource.TypeCurrentPassword, Resource.Ok);
                return;
            }

            if (NewPassword != ConfirmNewPassword)
            {
                await DisplayAlert(Resource.Error, Resource.TypeNewPassword, Resource.Ok);
                return;
            }

            IsBusy = true;
            var result = await DataBase.Manager.ChangePasswordAsync(NewPassword);
            IsBusy = false;

            if (result != DataBase.ReturnCode.Success)
            {
                await DisplayAlert(Resource.Error, Resource.ChangingPasswordError + " " + result.GetDescription(), Resource.Ok);
                return;
            }

            Settings.Storage.Password = DataBase.Manager.Password;

            await DisplayAlert(Resource.Success, Resource.PasswordChanged, Resource.Ok);
        }
    }
}

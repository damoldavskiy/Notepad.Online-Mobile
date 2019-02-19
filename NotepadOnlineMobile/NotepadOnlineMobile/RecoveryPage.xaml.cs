using System;

using Xamarin.Forms;

namespace NotepadOnlineMobile
{
    public partial class RecoveryPage : ContentPage
    {
        string email;
        string newPassword;
        string confirmNewPassword;

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

        public RecoveryPage()
		{
			InitializeComponent();
            BindingContext = this;
		}

        async void Button_Clicked(object sender, EventArgs e)
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

using System;

using Xamarin.Forms;

namespace NotepadOnlineMobile
{
    public partial class RegisterPage : ContentPage
	{
		public RegisterPage()
		{
			InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
		}

        private async void Register_Clicked(object sender, EventArgs e)
        {
            var login = emailEntry.Text?.Trim() ?? "";
            var password = passwordEntry.Text?.Trim() ?? "";
            var passwordConfirm = passwordConfirmEntry.Text?.Trim() ?? "";

            if (password != passwordConfirm)
            {
                await DisplayAlert("Error", "You should confirm your password by typing it to the needed box", "OK");
                return;
            }

            loading.IsVisible = true;
            var result = await DataBase.Manager.RegisterAsync(login, password);
            loading.IsVisible = false;

            if (result != DataBase.ReturnCode.Success)
            {
                await DisplayAlert("Error", $"An error occurred during creating new user: {result}", "OK");
                return;
            }

            await Navigation.PushModalAsync(new ConfirmPage());
        }
    }
}
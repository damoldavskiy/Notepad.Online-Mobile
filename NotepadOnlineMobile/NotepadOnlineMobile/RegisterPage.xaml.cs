using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotepadOnlineMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPage : ContentPage
	{
		public RegisterPage()
		{
			InitializeComponent();
		}

        private async void Register_Clicked(object sender, EventArgs e)
        {
            var login = entryLogin.Text?.Trim() ?? "";
            var password = entryPassword.Text?.Trim() ?? "";
            var confirmPassword = entryConfirmPassword.Text?.Trim() ?? "";

            if (password != confirmPassword)
            {
                await DisplayAlert("Error", "You should confirm your password by typing it to the needed box", "OK");
                return;
            }

            loading.IsVisible = true;
            var result = await DataBase.Manager.RegisterAsync(login, password);
            loading.IsVisible = false;

            if (result != DataBase.ReturnCode.Success)
            {
                await DisplayAlert("Error", $"An error occurred while creating new user: {result}", "OK");
                return;
            }

            await Navigation.PushModalAsync(new ConfirmPage());
        }
    }
}
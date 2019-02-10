using System;

using Xamarin.Forms;

namespace NotepadOnlineMobile
{
    public partial class ConfirmPage : ContentPage
	{
		public ConfirmPage()
		{
			InitializeComponent();
		}

        private async void Submit_Clicked(object sender, EventArgs e)
        {
            var code = CodeEntry.Text?.Trim() ?? "";

            loading.IsVisible = true;
            var result = await DataBase.Manager.ConfirmAsync(code);
            loading.IsVisible = false;

            if (result != DataBase.ReturnCode.Success)
            {
                await DisplayAlert("Error", $"An error occurred during confirming registration: {result}", "OK");
                return;
            }

            Settings.Storage.Set("login", DataBase.Manager.Login);
            Settings.Storage.Set("password", DataBase.Manager.Password);
            Settings.Storage.Set("token", DataBase.Manager.Token);

            Application.Current.MainPage = new MainPage();
        }
    }
}
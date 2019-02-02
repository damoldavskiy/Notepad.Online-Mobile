using System;

using NotepadOnlineMobile.Settings;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotepadOnlineMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
        event EventHandler PageLoaded;

		public LoginPage()
		{
			InitializeComponent();

            PageLoaded += LoginPage_PageLoaded; ;
            PageLoaded(this, EventArgs.Empty);
        }

        private async void LoginPage_PageLoaded(object sender, EventArgs e)
        {
            Storage.Load();

            if (Storage.Get("login").ToString() != "")
            {
                entryLogin.Text = Storage.Get("login").ToString();
                entryPassword.Text = Storage.Get("password").ToString();

                if (!(bool)Storage.Get("autoreg"))
                    return;

                loading.IsVisible = true;
                var result = await DataBase.Manager.AuthorizeAsync(Storage.Get("login").ToString(), Storage.Get("password").ToString(), Storage.Get("token").ToString());
                loading.IsVisible = false;

                if (result != DataBase.ReturnCode.Success)
                {
                    await DisplayAlert("Error", $"An error occurred while authorizing: {result}", "OK");
                    return;
                }

                Application.Current.MainPage = new MainPage();
            }
        }

        private async void Authorize_Clicked(object sender, EventArgs e)
        {
            var login = entryLogin.Text?.Trim() ?? "";
            var password = entryPassword.Text?.Trim() ?? "";

            loading.IsVisible = true;
            var result = await DataBase.Manager.AuthorizeAsync(login, password);
            loading.IsVisible = false;

            if (result != DataBase.ReturnCode.Success)
            {
                await DisplayAlert("Error", $"An error occurred while autorizing: {result}", "OK");
                return;
            }

            Storage.Set("login", DataBase.Manager.Login);
            Storage.Set("password", DataBase.Manager.Password);
            Storage.Set("token", DataBase.Manager.Token);

            Application.Current.MainPage = new MainPage();
        }

        private async void Register_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }
    }
}
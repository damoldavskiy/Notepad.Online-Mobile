using System;

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
            var properties = Application.Current.Properties;

            if (properties.ContainsKey("login") && properties.ContainsKey("password") && properties.ContainsKey("token") && properties.ContainsKey("autoreg") && properties.ContainsKey("askdel"))
            {
                entryLogin.Text = properties["login"].ToString();
                entryPassword.Text = properties["password"].ToString();

                if (!(bool)properties["autoreg"])
                    return;

                loading.IsVisible = true;
                var result = await DataBase.Manager.AuthorizeAsync(properties["login"].ToString(), properties["password"].ToString(), properties["token"].ToString());
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

            var properties = Application.Current.Properties;
            properties["login"] = DataBase.Manager.Login;
            properties["password"] = DataBase.Manager.Password;
            properties["token"] = DataBase.Manager.Token;
            properties["autoreg"] = properties.ContainsKey("autoreg") ? properties["autoreg"] : true;
            properties["askdel"] = properties.ContainsKey("askdel") ? properties["askdel"] : false;

            Application.Current.MainPage = new MainPage();
        }

        private async void Register_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }
    }
}
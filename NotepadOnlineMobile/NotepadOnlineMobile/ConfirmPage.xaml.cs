using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotepadOnlineMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConfirmPage : ContentPage
	{
		public ConfirmPage()
		{
			InitializeComponent();
		}

        private async void Submit_Clicked(object sender, EventArgs e)
        {
            var code = entryCode.Text?.Trim() ?? "";

            loading.IsVisible = true;
            var result = await DataBase.Manager.ConfirmAsync(code);
            loading.IsVisible = false;

            if (result != DataBase.ReturnCode.Success)
            {
                await DisplayAlert("Error", $"An error occurred while confirming registration: {result}", "OK");
                return;
            }

            var properties = Application.Current.Properties;
            properties["login"] = DataBase.Manager.Login;
            properties["password"] = DataBase.Manager.Password;
            properties["token"] = DataBase.Manager.Token;

            Application.Current.MainPage = new MainPage();
        }
    }
}
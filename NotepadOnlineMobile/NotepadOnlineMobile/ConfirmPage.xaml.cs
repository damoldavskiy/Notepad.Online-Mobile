using System;

using Xamarin.Forms;

namespace NotepadOnlineMobile
{
    public partial class ConfirmPage : ContentPage
	{
        private string code;

        public string Code
        {
            get
            {
                return code ?? "";
            }
            set
            {
                code = value?.Trim();
                OnPropertyChanged("Code");
            }
        }

        public ConfirmPage()
		{
			InitializeComponent();
            BindingContext = this;
		}

        private async void Submit_Clicked(object sender, EventArgs e)
        {
            IsBusy = true;
            var result = await DataBase.Manager.ConfirmAsync(code);
            IsBusy = false;

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
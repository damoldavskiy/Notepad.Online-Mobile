using System;

using Xamarin.Forms;

namespace NotepadOnlineMobile
{
    public partial class ConfirmRecoveryPage : ContentPage
	{
        string code;

        public string Code
        {
            get
            {
                return code ?? "";
            }
            set
            {
                code = value?.Trim();
                OnPropertyChanged(nameof(Code));
            }
        }

        public ConfirmRecoveryPage()
		{
			InitializeComponent();
            BindingContext = this;
		}

        async void Submit_Clicked(object sender, EventArgs e)
        {
            IsBusy = true;
            var result = await DataBase.Manager.ConfirmRecoveryAsync(code);
            IsBusy = false;

            if (result != DataBase.ReturnCode.Success)
            {
                await DisplayAlert("Error", $"An error occurred during confirming registration: {result}", "OK");
                return;
            }

            Application.Current.MainPage = new MainPage();
        }
    }
}

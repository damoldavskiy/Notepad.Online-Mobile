using System;

using Xamarin.Forms;

using static DataBase.ReturnCodeDescriptions;

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
                await DisplayAlert(Resource.Error, Resource.RecoveryConfirmationError + " " + result.GetDescription(), Resource.Ok);
                return;
            }

            Settings.Storage.Email = DataBase.Manager.Email;
            Settings.Storage.Password = DataBase.Manager.Password;
            Settings.Storage.Token = DataBase.Manager.Token;

            Application.Current.MainPage = new MainPage();
        }
    }
}

﻿using System;

using Xamarin.Forms;

namespace NotepadOnlineMobile
{
    public partial class ConfirmRecoveryPage : ContentPage
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

        public ConfirmRecoveryPage()
		{
			InitializeComponent();
            BindingContext = this;
		}

        private async void Submit_Clicked(object sender, EventArgs e)
        {
            IsBusy = true;
            var result = await DataBase.Manager.ConfirmRecoveryAsync(code);
            IsBusy = false;

            if (result != DataBase.ReturnCode.Success)
            {
                await DisplayAlert("Error", $"An error occurred during confirming registration: {result}", "OK");
                return;
            }

            Settings.Storage.Email = DataBase.Manager.Email;
            Settings.Storage.Password = DataBase.Manager.Password;
            Settings.Storage.Token = DataBase.Manager.Token;

            Application.Current.MainPage = new MainPage();
        }
    }
}
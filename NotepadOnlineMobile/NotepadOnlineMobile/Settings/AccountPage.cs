using Xamarin.Forms;

namespace NotepadOnlineMobile.Settings
{
    class AccountPage : AbstractPage
    {
        public AccountPage() : base(Resource.Account)
        {
            Items.Add(new SettingsItem
            {
                Header = Resource.AutoLogin,
                Value = Storage.AutoLogin ? Resource.Enabled : Resource.Disabled,
                ValueVisible = true,
                SwitcherVisible = true,
                SwitcherToggled = Storage.AutoLogin,
                Action = (item) =>
                {
                    if (Storage.AutoLogin)
                    {
                        Storage.AutoLogin = false;
                        item.Value = Resource.Disabled;
                        item.SwitcherToggled = false;
                    }
                    else
                    {
                        Storage.AutoLogin = true;
                        item.Value = Resource.Enabled;
                        item.SwitcherToggled = true;
                    }
                }
            });

            Items.Add(new SettingsItem
            {
                Header = Resource.ChangePassword,
                Action = (item) =>
                {
                    var page = new ChangePasswordPage
                    {
                        Title = Resource.ChangePassword
                    };
                    Navigation.PushAsync(page);
                }
            });

            Items.Add(new SettingsItem
            {
                Header = Resource.Log_out,
                Action = (item) =>
                {
                    Storage.Email = null;
                    Storage.Password = null;
                    Storage.Token = null;
                    DataBase.Manager.Logout();
                    Application.Current.MainPage = new LoginRegisterPage();
                }
            });
        }
    }
}

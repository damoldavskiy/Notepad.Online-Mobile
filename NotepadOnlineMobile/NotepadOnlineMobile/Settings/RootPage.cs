namespace NotepadOnlineMobile.Settings
{
    class RootPage : AbstractPage
    {
        public RootPage() : base(Resource.Settings)
        {
            Items.Add(new SettingsItem
            {
                Header = Resource.Account,
                Action = (item) => Navigation.PushAsync(new AccountPage())
            });

            Items.Add(new SettingsItem
            {
                Header = Resource.Editor,
                Action = (item) => Navigation.PushAsync(new EditorPage())
            });

            Items.Add(new SettingsItem
            {
                Header = Resource.Files,
                Action = (item) => Navigation.PushAsync(new FilesPage())
            });
        }
    }
}

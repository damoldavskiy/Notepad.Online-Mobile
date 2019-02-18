namespace NotepadOnlineMobile.Settings
{
    class RootPage : AbstractPage
    {
        public RootPage() : base("Settings")
        {
            Items.Add(new SettingsItem()
            {
                Header = "Account",
                Action = (item) => Navigation.PushAsync(new AccountPage())
            });

            Items.Add(new SettingsItem()
            {
                Header = "Editor",
                Action = (item) => Navigation.PushAsync(new EditorPage())
            });

            Items.Add(new SettingsItem()
            {
                Header = "Files",
                Action = (item) => Navigation.PushAsync(new FilesPage())
            });
        }
    }
}

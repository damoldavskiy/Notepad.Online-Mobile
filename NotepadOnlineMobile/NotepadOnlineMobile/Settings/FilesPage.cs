namespace NotepadOnlineMobile.Settings
{
    class FilesPage : AbstractPage
    {
        public FilesPage() : base("Files")
        {
            Items.Add(new SettingsItem()
            {
                Header = "Ask before delete",
                Value = (bool)Storage.Get("askdel") ? "Enabled" : "Disabled",
                Action = (item) =>
                {
                    if ((bool)Storage.Get("askdel"))
                    {
                        Storage.Set("askdel", false);
                        item.Value = "Disabled";
                    }
                    else
                    {
                        Storage.Set("askdel", true);
                        item.Value = "Enabled";
                    }
                }
            });

            Items.Add(new SettingsItem()
            {
                Header = "Description type",
                Value = (bool)Storage.Get("keywords") ? "Key words" : "First characters",
                Action = (item) =>
                {
                    if ((bool)Storage.Get("keywords"))
                    {
                        Storage.Set("keywords", false);
                        item.Value = "First characters";
                    }
                    else
                    {
                        Storage.Set("keywords", true);
                        item.Value = "Key words";
                    }
                }
            });
        }
    }
}

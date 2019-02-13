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
                ValueVisible = true,
                SwitcherVisible = true,
                SwitcherToggled = (bool)Storage.Get("askdel"),
                Action = (item) =>
                {
                    if ((bool)Storage.Get("askdel"))
                    {
                        Storage.Set("askdel", false);
                        item.Value = "Disabled";
                        item.SwitcherToggled = false;
                    }
                    else
                    {
                        Storage.Set("askdel", true);
                        item.Value = "Enabled";
                        item.SwitcherToggled = true;
                    }
                }
            });

            Items.Add(new SettingsItem()
            {
                Header = "Description type",
                Value = (bool)Storage.Get("keywords") ? "Key words" : "First characters",
                ValueVisible = true,
                Action = async (item) =>
                {
                    var r = await DisplayActionSheet("Description type", "Cancel", null, "Key words", "First characters");

                    if (r == "First characters")
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

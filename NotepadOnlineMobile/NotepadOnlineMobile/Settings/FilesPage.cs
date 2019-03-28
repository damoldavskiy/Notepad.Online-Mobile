namespace NotepadOnlineMobile.Settings
{
    class FilesPage : AbstractPage
    {
        public FilesPage() : base("Files")
        {
            Items.Add(new SettingsItem
            {
                Header = "Ask before delete",
                Value = Storage.AskDelete ? "Enabled" : "Disabled",
                ValueVisible = true,
                SwitcherVisible = true,
                SwitcherToggled = Storage.AskDelete,
                Action = (item) =>
                {
                    if (Storage.AskDelete)
                    {
                        Storage.AskDelete = false;
                        item.Value = "Disabled";
                        item.SwitcherToggled = false;
                    }
                    else
                    {
                        Storage.AskDelete = true;
                        item.Value = "Enabled";
                        item.SwitcherToggled = true;
                    }
                }
            });

            Items.Add(new SettingsItem
            {
                Header = "Preload files",
                Value = Storage.Preload ? "Files load on start" : "Files load on tap",
                ValueVisible = true,
                SwitcherVisible = true,
                SwitcherToggled = Storage.Preload,
                Action = (item) =>
                {
                    if (Storage.Preload)
                    {
                        Storage.Preload = false;
                        item.Value = "Files load on tap";
                        item.SwitcherToggled = false;
                    }
                    else
                    {
                        Storage.Preload = true;
                        item.Value = "Files load on start";
                        item.SwitcherToggled = true;
                    }
                }
            });

            Items.Add(new SettingsItem
            {
                Header = "Description type",
                Value = Storage.UseKeyWords ? "Key words" : "First characters",
                ValueVisible = true,
                Action = async (item) =>
                {
                    var result = await DisplayActionSheet("Description type", "Cancel", null, "Key words", "First characters");

                    if (result == "First characters")
                    {
                        Storage.UseKeyWords = false;
                        item.Value = "First characters";
                    }
                    else if (result == "Key words")
                    {
                        Storage.UseKeyWords = true;
                        item.Value = "Key words";
                    }
                }
            });
        }
    }
}

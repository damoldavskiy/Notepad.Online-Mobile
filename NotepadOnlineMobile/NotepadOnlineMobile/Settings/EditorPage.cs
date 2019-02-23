namespace NotepadOnlineMobile.Settings
{
    class EditorPage : AbstractPage
    {
        public EditorPage() : base("Editor")
        {
            Items.Add(new SettingsItem()
            {
                Header = "Font size",
                Value = "Current size: " + Storage.FontSize,
                ValueVisible = true,
                Action = async (item) =>
                {
                    var result = await DisplayActionSheet("Font size", "Cancel", null, "12", "14", "16", "18", "20");
                    
                    int size;
                    if (int.TryParse(result, out size))
                    {
                        Storage.FontSize = size;
                        item.Value = "Current size: " + result;
                    }
                }
            });

            Items.Add(new SettingsItem()
            {
                Header = "Theme",
                Value = Storage.Theme,
                ValueVisible = true,
                Action = async (item) =>
                {
                    var result = await DisplayActionSheet("Theme", "Cancel", null, "Light", "Dark");

                    if (result == "Light" && Storage.Theme != "Light")
                    {
                        Storage.Theme = "Light";
                        Themes.Manager.Update();
                    }
                    else if (result == "Dark" && Storage.Theme != "Dark")
                    {
                        Storage.Theme = "Dark";
                        Themes.Manager.Update();
                    }
                }
            });
        }
    }
}

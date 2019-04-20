namespace NotepadOnlineMobile.Settings
{
    class FilesPage : AbstractPage
    {
        public FilesPage() : base(Resource.Files)
        {
            Items.Add(new SettingsItem
            {
                Header = Resource.AskBeforeDelete,
                Value = Storage.AskDelete ? Resource.Enabled : Resource.Disabled,
                ValueVisible = true,
                SwitcherVisible = true,
                SwitcherToggled = Storage.AskDelete,
                Action = (item) =>
                {
                    if (Storage.AskDelete)
                    {
                        Storage.AskDelete = false;
                        item.Value = Resource.Disabled;
                        item.SwitcherToggled = false;
                    }
                    else
                    {
                        Storage.AskDelete = true;
                        item.Value = Resource.Enabled;
                        item.SwitcherToggled = true;
                    }
                }
            });

            Items.Add(new SettingsItem
            {
                Header = Resource.PreloadFiles,
                Value = Storage.Preload ? Resource.FilesLoadOnStart : Resource.FilesLoadOnTap,
                ValueVisible = true,
                SwitcherVisible = true,
                SwitcherToggled = Storage.Preload,
                Action = (item) =>
                {
                    if (Storage.Preload)
                    {
                        Storage.Preload = false;
                        item.Value = Resource.FilesLoadOnTap;
                        item.SwitcherToggled = false;
                    }
                    else
                    {
                        Storage.Preload = true;
                        item.Value = Resource.FilesLoadOnStart;
                        item.SwitcherToggled = true;
                    }
                }
            });

            Items.Add(new SettingsItem
            {
                Header = Resource.DescriptionType,
                Value = Storage.UseKeyWords ? Resource.KeyWords : Resource.FirstCharacters,
                ValueVisible = true,
                Action = async (item) =>
                {
                    var result = await DisplayActionSheet(Resource.DescriptionType, Resource.Cancel, null, Resource.KeyWords, Resource.FirstCharacters);

                    if (result == Resource.FirstCharacters)
                    {
                        Storage.UseKeyWords = false;
                        item.Value = Resource.FirstCharacters;
                    }
                    else if (result == Resource.KeyWords)
                    {
                        Storage.UseKeyWords = true;
                        item.Value = Resource.KeyWords;
                    }
                }
            });
        }
    }
}

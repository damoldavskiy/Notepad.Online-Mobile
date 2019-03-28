using System.Linq;

namespace NotepadOnlineMobile.Settings
{
    class EditorPage : AbstractPage
    {
        public EditorPage() : base("Editor")
        {
            Items.Add(new SettingsItem
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

            Items.Add(new SettingsItem
            {
                Header = "Font family",
                Value = "Current family: " + Fonts.Current.Where(font => font.Family == Storage.FontFamily).First().Name,
                ValueVisible = true,
                Action = async (item) =>
                {
                    var fonts = Fonts.Current.Select(font => font.Name).ToArray();
                    var result = await DisplayActionSheet("Font family", "Cancel", null, fonts);
                    
                    if (fonts.Contains(result))
                    {
                        Storage.FontFamily = Fonts.Current.Where(font => font.Name == result).First().Family;
                        item.Value = "Current size: " + result;
                    }
                }
            });
        }
    }
}

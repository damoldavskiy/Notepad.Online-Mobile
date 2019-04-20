using System.Linq;

namespace NotepadOnlineMobile.Settings
{
    class EditorPage : AbstractPage
    {
        public EditorPage() : base(Resource.Editor)
        {
            Items.Add(new SettingsItem
            {
                Header = Resource.FontSize,
                Value = Resource.CurrentSize + ": " + Storage.FontSize,
                ValueVisible = true,
                Action = async (item) =>
                {
                    var result = await DisplayActionSheet(Resource.FontSize, Resource.Cancel, null, "12", "14", "16", "18", "20");

                    if (int.TryParse(result, out int size))
                    {
                        Storage.FontSize = size;
                        item.Value = Resource.CurrentSize + ": " + Storage.FontSize;
                    }
                }
            });

            Items.Add(new SettingsItem
            {
                Header = Resource.FontFamily,
                Value = Resource.CurrentFamily + ": " + Fonts.Current.Where(font => font.Family == Storage.FontFamily).First().Name,
                ValueVisible = true,
                Action = async (item) =>
                {
                    var fonts = Fonts.Current.Select(font => font.Name).ToArray();
                    var result = await DisplayActionSheet(Resource.FontFamily, Resource.Cancel, null, fonts);
                    
                    if (fonts.Contains(result))
                    {
                        Storage.FontFamily = Fonts.Current.Where(font => font.Name == result).First().Family;
                        item.Value = Resource.CurrentSize + ": " + result;
                    }
                }
            });
        }
    }
}

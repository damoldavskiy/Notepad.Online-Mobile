using Xamarin.Forms;

namespace NotepadOnlineMobile.Settings
{
    public static class Storage
    {
        public static void Load()
        {
            SetDef("login", "");
            SetDef("password", "");
            SetDef("token", "");
            SetDef("autoreg", true);
            SetDef("askdel", false);
            SetDef("keywords", true);
        }

        public static object Get(string property)
        {
            return Application.Current.Properties[property];
        }

        public static void Set(string property, object value)
        {
            Application.Current.Properties[property] = value;
        }

        private static void SetDef(string property, object value)
        {
            var props = Application.Current.Properties;
            props[property] = props.ContainsKey(property) ? props[property] : value;
        }
    }
}

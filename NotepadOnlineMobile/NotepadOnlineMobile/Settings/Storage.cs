using Xamarin.Forms;

namespace NotepadOnlineMobile.Settings
{
    public static class Storage
    {
        public static string Email
        {
            get { return Get(nameof(Email)).ToString(); }
            set { Set(nameof(Email), value); }
        }

        public static string Password
        {
            get { return Get(nameof(Password)).ToString(); }
            set { Set(nameof(Password), value); }
        }

        public static string Token
        {
            get { return Get(nameof(Token)).ToString(); }
            set { Set(nameof(Token), value); }
        }

        public static bool AutoLogin
        {
            get { return (bool)Get(nameof(AutoLogin)); }
            set { Set(nameof(AutoLogin), value); }
        }

        public static bool AskDelete
        {
            get { return (bool)Get(nameof(AskDelete)); }
            set { Set(nameof(AskDelete), value); }
        }

        public static bool Preload
        {
            get { return (bool)Get(nameof(Preload)); }
            set { Set(nameof(Preload), value); }
        }

        public static bool UseKeyWords
        {
            get { return (bool)Get(nameof(UseKeyWords)); }
            set { Set(nameof(UseKeyWords), value); }
        }

        public static int FontSize
        {
            get { return (int)Get(nameof(FontSize)); }
            set { Set(nameof(FontSize), value); }
        }

        public static string Theme
        {
            get { return (string)Get(nameof(Theme)); }
            set { Set(nameof(Theme), value); }
        }

        public static void Initialize()
        {
            var initialized = true;

            foreach (var property in typeof(Storage).GetProperties())
                if (!Application.Current.Properties.ContainsKey(property.Name))
                    initialized = false;

            if (initialized)
                return;

            Email = "";
            Password = "";
            Token = "";
            AutoLogin = true;
            AskDelete = false;
            UseKeyWords = true;
            Preload = true;
            FontSize = 18;
            Theme = "Light";
        }

        static object Get(string property)
        {
            return App.Current.Properties[property];
        }

        static void Set(string property, object value)
        {
            App.Current.Properties[property] = value;
        }
    }
}

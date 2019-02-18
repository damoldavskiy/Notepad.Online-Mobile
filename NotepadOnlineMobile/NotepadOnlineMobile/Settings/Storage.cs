using Xamarin.Forms;

namespace NotepadOnlineMobile.Settings
{
    public static class Storage
    {
        public static string Email
        {
            get { return Get("Email").ToString(); }
            set { Set("Email", value); }
        }

        public static string Password
        {
            get { return Get("Password").ToString(); }
            set { Set("Password", value); }
        }

        public static string Token
        {
            get { return Get("Token").ToString(); }
            set { Set("Token", value); }
        }

        public static bool AutoLogin
        {
            get { return (bool)Get("AutoLogin"); }
            set { Set("AutoLogin", value); }
        }

        public static bool AskDelete
        {
            get { return (bool)Get("AskDelete"); }
            set { Set("AskDelete", value); }
        }

        public static bool Preload
        {
            get { return (bool)Get("Preload"); }
            set { Set("Preload", value); }
        }

        public static bool UseKeyWords
        {
            get { return (bool)Get("UseKeyWords"); }
            set { Set("UseKeyWords", value); }
        }

        public static void Initialize()
        {
            var initialized = true;
            foreach (var key in new[] { "Email", "Password", "Token", "AutoLogin", "AskDelete", "Preload", "UseKeyWords" })
                if (!Application.Current.Properties.ContainsKey(key))
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
        }

        static object Get(string property)
        {
            return Application.Current.Properties[property];
        }

        static void Set(string property, object value)
        {
            Application.Current.Properties[property] = value;
        }
    }
}

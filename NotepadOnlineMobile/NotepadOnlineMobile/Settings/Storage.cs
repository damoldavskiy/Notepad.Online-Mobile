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

        public static bool UseKeyWords
        {
            get { return (bool)Get("UseKeyWords"); }
            set { Set("UseKeyWords", value); }
        }

        public static void Initialize()
        {
            if (Application.Current.Properties.ContainsKey("Email"))
                return;

            Email = "";
            Password = "";
            Token = "";
            AutoLogin = true;
            AskDelete = false;
            UseKeyWords = true;
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

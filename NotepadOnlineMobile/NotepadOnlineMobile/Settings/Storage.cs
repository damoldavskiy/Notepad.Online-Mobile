using System.Collections.Generic;
using Xamarin.Forms;

namespace NotepadOnlineMobile.Settings
{
    public static class Storage
    {
        public static string Email
        {
            get { return Get(nameof(Email)).ToString(); }
            set { Set(nameof(Email), value ?? Defaults[nameof(Email)]); }
        }

        public static string Password
        {
            get { return Get(nameof(Password)).ToString(); }
            set { Set(nameof(Password), value ?? Defaults[nameof(Password)]); }
        }

        public static string Token
        {
            get { return Get(nameof(Token)).ToString(); }
            set { Set(nameof(Token), value ?? Defaults[nameof(Token)]); }
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

        public static Dictionary<string, object> Defaults { get; } = new Dictionary<string, object>
        {
            { nameof(Email), "" },
            { nameof(Password), "" },
            { nameof(Token), "" },
            { nameof(AutoLogin), true },
            { nameof(AskDelete), false },
            { nameof(UseKeyWords), true },
            { nameof(Preload), true },
            { nameof(FontSize), 18 }
        };

        public static void Initialize()
        {
            foreach (var property in typeof(Storage).GetProperties())
                if (property.Name != nameof(Defaults) && !Application.Current.Properties.ContainsKey(property.Name))
                    Set(property.Name, Defaults[property.Name]);
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

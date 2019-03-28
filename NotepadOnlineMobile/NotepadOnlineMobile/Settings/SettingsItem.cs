using System;
using System.ComponentModel;

namespace NotepadOnlineMobile.Settings
{
    public class SettingsItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        string header;
        string value;
        bool valueVisible;
        bool switcherVisible;
        bool switcherToggled;
        Action<SettingsItem> action;

        public string Header
        {
            get
            { return header; }
            set
            {
                header = value;
                OnPropertyChanged(nameof(Header));
            }
        }

        public string Value
        {
            get
            { return value; }
            set
            {
                this.value = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        public bool ValueVisible
        {
            get
            { return valueVisible; }
            set
            {
                valueVisible = value;
                OnPropertyChanged(nameof(ValueVisible));
            }
        }

        public bool SwitcherVisible
        {
            get
            { return switcherVisible; }
            set
            {
                switcherVisible = value;
                OnPropertyChanged(nameof(SwitcherVisible));
            }
        }

        public bool SwitcherToggled
        {
            get
            { return switcherToggled; }
            set
            {
                switcherToggled = value;
                OnPropertyChanged(nameof(SwitcherToggled));
            }
        }

        public Action<SettingsItem> Action
        {
            get
            { return action; }
            set
            {
                action = value;
                OnPropertyChanged(nameof(Action));
            }
        }

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

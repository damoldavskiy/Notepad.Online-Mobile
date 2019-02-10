using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

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
                OnPropertyChanged("Header");
            }
        }

        public string Value
        {
            get
            { return value; }
            set
            {
                this.value = value;
                OnPropertyChanged("Value");
            }
        }

        public bool ValueVisible
        {
            get
            { return valueVisible; }
            set
            {
                valueVisible = value;
                OnPropertyChanged("ValueVisible");
            }
        }

        public bool SwitcherVisible
        {
            get
            { return switcherVisible; }
            set
            {
                switcherVisible = value;
                OnPropertyChanged("SwitcherVisible");
            }
        }

        public bool SwitcherToggled
        {
            get
            { return switcherToggled; }
            set
            {
                switcherToggled = value;
                OnPropertyChanged("SwitcherToggled");
            }
        }

        public Action<SettingsItem> Action
        {
            get
            { return action; }
            set
            {
                action = value;
                OnPropertyChanged("Action");
            }
        }

        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

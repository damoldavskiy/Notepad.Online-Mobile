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
        Action<SettingsItem> action;

        public string Header
        {
            get
            { return header; }
            set
            {
                OnPropertyChanged("Header");
                header = value;
            }
        }

        public string Value
        {
            get
            { return value; }
            set
            {
                OnPropertyChanged("Value");
                this.value = value;
            }
        }

        public Action<SettingsItem> Action
        {
            get
            { return action; }
            set
            {
                OnPropertyChanged("Action");
                action = value;
            }
        }

        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

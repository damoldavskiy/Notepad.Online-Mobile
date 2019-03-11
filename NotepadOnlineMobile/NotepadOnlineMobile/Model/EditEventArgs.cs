using System;

namespace NotepadOnlineMobile
{
    public class EditEventArgs : EventArgs
    {
        public string Name { get; }
        public string NewDescription { get; }
        public string NewText { get; }

        public EditEventArgs(string name, string newDescription, string newText)
        {
            Name = name;
            NewDescription = newDescription;
            NewText = newText;
        }
    }
}

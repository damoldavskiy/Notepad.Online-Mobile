using System;

namespace NotepadOnlineMobile
{
    public class DeleteEventArgs : EventArgs
    {
        public string Name { get; }

        public DeleteEventArgs(string name)
        {
            Name = name;
        }
    }
}

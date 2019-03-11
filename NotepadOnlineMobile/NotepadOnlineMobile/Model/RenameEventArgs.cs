using System;

namespace NotepadOnlineMobile
{
    public class RenameEventArgs : EventArgs
    {
        public string OldName { get; }
        public string NewName { get; }

        public RenameEventArgs(string oldName, string newName)
        {
            OldName = oldName;
            NewName = newName;
        }
    }
}

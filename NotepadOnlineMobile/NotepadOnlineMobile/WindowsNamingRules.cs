using System;
using System.Collections.Generic;
using System.Text;

namespace NotepadOnlineMobile
{
    static class WindowsNamingRules
    {
        public static bool IsNameCorrect(string name)
        {
            if (name.StartsWith(" ") || name.EndsWith(" ") || name.EndsWith("."))
                return false;

            foreach (var letter in @"\/:*?""<>|+")
                if (name.Contains(letter.ToString()))
                    return false;

            return true;
        }
    }
}

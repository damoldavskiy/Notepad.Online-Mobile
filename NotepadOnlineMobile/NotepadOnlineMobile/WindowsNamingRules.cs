namespace NotepadOnlineMobile
{
    public static class WindowsNamingRules
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

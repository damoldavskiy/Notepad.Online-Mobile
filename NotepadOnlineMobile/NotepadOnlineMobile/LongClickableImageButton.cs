using System;

using Xamarin.Forms;

namespace NotepadOnlineMobile
{
    public class LongClickableImageButton : ImageButton
	{
        public event EventHandler LongClicked;

        public LongClickableImageButton ()
		{ }

        public void OnLongClicked(object s, EventArgs e)
        {
            LongClicked?.Invoke(this, e);
        }
    }
}

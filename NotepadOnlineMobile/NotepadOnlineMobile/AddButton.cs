using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace NotepadOnlineMobile
{
	public class AddButton : View
	{
        public event EventHandler Click;
        public event EventHandler LongClick;

        public AddButton ()
		{ }

        public void OnClick(object s, EventArgs e)
        {
            Click?.Invoke(this, e);
        }

        public void OnLongClick(object s, EventArgs e)
        {
            LongClick?.Invoke(this, e);
        }
    }
}
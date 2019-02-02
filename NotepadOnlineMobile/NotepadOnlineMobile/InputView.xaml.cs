using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotepadOnlineMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InputView : ContentView
	{
        public Entry Field;
        public Button Submit;

		public InputView ()
		{
			InitializeComponent ();

            Field = field;
            Submit = submit;
		}
	}
}
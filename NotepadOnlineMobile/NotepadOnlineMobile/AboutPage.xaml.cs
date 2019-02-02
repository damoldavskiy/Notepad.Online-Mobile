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
	public partial class AboutPage : ContentPage
	{
		public AboutPage()
		{
			InitializeComponent();

            content.Text = "Notepad.Online Mobile\nVersion: Alpha\n\nHave a question? Contact with developer: party_50@mail.ru";

        }
	}
}
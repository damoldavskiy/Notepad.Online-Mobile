using Xamarin.Forms;

namespace NotepadOnlineMobile
{
	public partial class AboutPage : ContentPage
	{
		public AboutPage()
		{
			InitializeComponent();

            content.Text = Resource.Info;
        }
	}
}

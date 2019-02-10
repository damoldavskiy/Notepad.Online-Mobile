using Xamarin.Forms;

namespace NotepadOnlineMobile
{
    public partial class InputView : ContentView
	{
        public Entry Field;
        public Button Submit;

        public InputView()
		{
			InitializeComponent();

            Field = field;
            Submit = submit;
		}

        private void Cancel_Clicked(object sender, System.EventArgs e)
        {
            IsVisible = false;
            field.Text = "";
        }
    }
}
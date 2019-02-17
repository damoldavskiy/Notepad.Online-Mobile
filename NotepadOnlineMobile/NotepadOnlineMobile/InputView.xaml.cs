using System;

using Xamarin.Forms;

namespace NotepadOnlineMobile
{
    public partial class InputView : ContentView
	{
        private string text;

        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                OnPropertyChanged(nameof(Text));
            }
        }
        
        public event EventHandler SubmitClicked;

        public InputView()
		{
			InitializeComponent();
            BindingContext = this;
            IsVisible = false;
        }

        private void Submit_Clicked(object sender, EventArgs e)
        {
            SubmitClicked?.Invoke(this, EventArgs.Empty);
        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
            IsVisible = false;
            Text = "";
        }
    }
}
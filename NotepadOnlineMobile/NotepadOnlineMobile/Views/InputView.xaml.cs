using System;
using System.Threading.Tasks;

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
            Opacity = 0;
        }

        public async Task Show()
        {
            IsVisible = true;
            await this.FadeTo(1);
        }

        public async Task Hide()
        {
            await this.FadeTo(0);
            IsVisible = false;
        }

        private void Submit_Clicked(object sender, EventArgs e)
        {
            SubmitClicked?.Invoke(this, EventArgs.Empty);
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Hide();
            Text = "";
        }
    }
}
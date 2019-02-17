using Xamarin.Forms;

namespace NotepadOnlineMobile
{
    public partial class LoadingView : ContentView
	{
        public LoadingView()
		{
			InitializeComponent();
            PropertyChanged += LoadingView_PropertyChanged;
		}

        private void LoadingView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IsVisible))
            {
                if (IsVisible)
                    this.FadeTo(1);
                else
                    Opacity = 0;
            }
        }
    }
}

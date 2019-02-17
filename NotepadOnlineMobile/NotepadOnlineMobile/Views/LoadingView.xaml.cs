using System;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace NotepadOnlineMobile
{
    public partial class LoadingView : ContentView
	{
        public static BindableProperty ShowedProperty = BindableProperty.Create("Showed", typeof(bool), typeof(LoadingView), false, propertyChanged: OnShowedPropertyChanged, propertyChanging: OnShowedPropertyChanging);

        public bool Showed
        {
            get
            {
                return (bool)GetValue(ShowedProperty);
            }
            set
            {
                SetValue(ShowedProperty, value);
            }
        }

        public LoadingView()
		{
			InitializeComponent();
            IsVisible = false;
            Opacity = 0;
		}

        private static async void OnShowedPropertyChanging(BindableObject bindable, object oldValue, object newValue)
        {
            var view = (LoadingView)bindable;

            if (!(bool)newValue)
            {
                await view.FadeTo(0);
                view.IsVisible = false;
            }
        }

        private static async void OnShowedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = (LoadingView)bindable;

            if ((bool)newValue)
            {
                view.IsVisible = true;
                await view.FadeTo(1);
            }
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
    }
}

using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace NotepadOnlineMobile
{
    public static class Fonts
    {
        static readonly FontFamily[] androidFonts = new[]
        {
            new FontFamily("Roboto Regular", "sans-serif"),
            new FontFamily("Roboto Light", "sans-serif-light"),
            new FontFamily("Roboto Medium", "sans-serif-medium"),
            new FontFamily("Roboto Condensed", "sans-serif-condensed"),
            new FontFamily("Roboto Condensed Light", "sans-serif-condensed-light"),
            new FontFamily("Noto Serif", "serif"),
            new FontFamily("Droid Sans Mono", "monospace"),
        };

        public static IEnumerable<FontFamily> Current
        {
            get
            {
                switch (Device.RuntimePlatform)
                {
                    case Device.Android:
                        return androidFonts;
                    default:
                        throw new Exception("Fonts for device are not specified");
                }
            }
        }
    }
}

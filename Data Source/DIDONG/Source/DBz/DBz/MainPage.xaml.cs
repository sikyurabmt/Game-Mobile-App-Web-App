using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Media.Playback;

using Windows.UI.Core;

namespace DBz
{
    public sealed partial class MainPage : Page
    {
        static String[] tracks = { "ms-appx:///Assets/Media/Ring01.wma", 
                                   "ms-appx:///Assets/Media/Ring02.wma",
                                   "ms-appx:///Assets/Media/Ring03.wma"};
        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            StartTrackAt(1);
            BackgroundMediaPlayer.Current.CurrentStateChanged += MediaPlayerStateChanged;
        }

        private async void MediaPlayerStateChanged(MediaPlayer sender, object args)
        {
            // Dispatch to UI thread, this event is called from a background thread
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                BackgroundMediaPlayer.SendMessageToBackground(new ValueSet
                        {
                            {"Title", "aaaaaaaa"},
                            {"Artist", "bbbbbb"},
                        });
            });
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }
        private void StartTrackAt(int id)
        {
            string source = tracks[id];
            BackgroundMediaPlayer.Current.SetUriSource(new Uri(source));
            tblTitle.Text = source;
            BackgroundMediaPlayer.SendMessageToBackground(new ValueSet
                        {
                            {"Title", "Drops of H2O"},
                            {"Artist", "J.Lang"},
                        });
        }
    }
}

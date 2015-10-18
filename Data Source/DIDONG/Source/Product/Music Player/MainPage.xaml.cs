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

using Windows.Media;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.Storage.FileProperties;
using System.Threading.Tasks;
using Windows.Phone.UI.Input;
using Windows.UI.Core;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace Music_Player
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        MusicManager musicManager = new MusicManager();
        DispatcherTimer playTimer;

        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            musicManager.initList();
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;

            playTimer = new DispatcherTimer();
            playTimer.Interval = TimeSpan.FromSeconds(1); //one second
            playTimer.Tick += new EventHandler<object>(playTimer_Tick);
            playTimer.Start();
        }

        public void playTimer_Tick(object sender, object e)
        {
            if (BackgroundMediaPlayer.Current.CurrentState == MediaPlayerState.Playing)
            {
                progressBar.Value = BackgroundMediaPlayer.Current.Position.TotalSeconds;
                try
                {
                    tblNowTime.Text = String.Format(@"{0:hh\:mm\:ss}",
                                       BackgroundMediaPlayer.Current.Position).Remove(8);
                }
                catch
                {
                    tblNowTime.Text = String.Format(@"{0:hh\:mm\:ss}",
                                       BackgroundMediaPlayer.Current.Position);
                }
            }
            if (BackgroundMediaPlayer.Current.NaturalDuration.TotalSeconds == BackgroundMediaPlayer.Current.Position.TotalSeconds)
            {
                musicManager.getNextNumber();
                startMusic_getMusicProperties(MusicManager.musicList[MusicManager.STT]);
            }
        }
        void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame != null && rootFrame.CanGoBack)
            {
                e.Handled = true;
                rootFrame.GoBack();
            }
            else
            {
                Application.Current.Exit();
            }
        }

        public async void startMusic_getMusicProperties(StorageFile file)
        {
            var audioFile = await musicManager.folder.GetFileAsync(file.Name);
            musicManager.musicProperties = await audioFile.Properties.GetMusicPropertiesAsync();
            var stream = await audioFile.OpenAsync(Windows.Storage.FileAccessMode.Read);
            //mediaShow.SetSource(stream, audioFile.ContentType);
            //mediaShow.Play();
            BackgroundMediaPlayer.Current.SetStreamSource(stream);
            BackgroundMediaPlayer.Current.Play();
            MusicManager.state = MusicManager.MediaState.PLAY;
            tblTitle.Text = musicManager.musicProperties.Title;
            tblArtist.Text = musicManager.musicProperties.Artist;
            tblTotalTime.Text = String.Format(@"{0:hh\:mm\:ss}", musicManager.musicProperties.Duration);
            progressBar.Maximum = musicManager.musicProperties.Duration.TotalSeconds;
            btPP.Icon = new SymbolIcon(Symbol.Pause);
            btPP.Label = "Pause";
        }

        private void pauseMusic()
        {
            // mediaShow.Pause();
            BackgroundMediaPlayer.Current.Pause();
            MusicManager.state = MusicManager.MediaState.PAUSE;
            tblArtist.Text = "PAUSE";
        }

        private void playMusic()
        {
            //mediaShow.Play();
            BackgroundMediaPlayer.Current.Play();
            MusicManager.state = MusicManager.MediaState.PLAY;
            tblArtist.Text = musicManager.musicProperties.Artist;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //tblNowTime.Text = BackgroundMediaPlayer.Current.Position.Seconds.ToString();
        }

        private void btNext_Click(object sender, RoutedEventArgs e)
        {
            musicManager.getNextNumber();
            startMusic_getMusicProperties(MusicManager.musicList[MusicManager.STT]);
        }

        private void btPrev_Click(object sender, RoutedEventArgs e)
        {
            musicManager.getPrevNumber();
            startMusic_getMusicProperties(MusicManager.musicList[MusicManager.STT]);
        }

        private void btbarList_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PortraitList));
        }

        private void btPP_Click(object sender, RoutedEventArgs e)
        {
            switch (MusicManager.state)
            {
                case MusicManager.MediaState.PLAY:
                    pauseMusic();
                    btPP.Icon = new SymbolIcon(Symbol.Play);
                    btPP.Label = "Play";
                    break;
                case MusicManager.MediaState.PAUSE:
                    playMusic();
                    btPP.Icon = new SymbolIcon(Symbol.Pause);
                    btPP.Label = "Pause";
                    break;
                case MusicManager.MediaState.STOP:
                    startMusic_getMusicProperties(MusicManager.musicList[MusicManager.STT]);
                    break;
                default:
                    break;
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            switch (MusicManager.nof)
            {
                case MusicManager.NumOfLoad.FIRST:
                    break;
                case MusicManager.NumOfLoad.SECOND:
                    startMusic_getMusicProperties(MusicManager.musicList[MusicManager.STT]);
                    break;
            }
        }

        private void btbarRepeat_Click(object sender, RoutedEventArgs e)
        {
            switch (MusicManager.rp)
            {
                case MusicManager.Repeat.ONE:
                    MusicManager.rp = MusicManager.Repeat.NO;
                    btbarRepeat.Icon = new SymbolIcon(Symbol.RepeatAll);
                    btbarRepeat.Label = "No";
                    break;
                case MusicManager.Repeat.ALL:
                    MusicManager.rp = MusicManager.Repeat.ONE;
                    btbarRepeat.Icon = new SymbolIcon(Symbol.RepeatOne);
                    btbarRepeat.Label = "One";
                    break;
                case MusicManager.Repeat.NO:
                    MusicManager.rp = MusicManager.Repeat.ALL;
                    btbarRepeat.Icon = new SymbolIcon(Symbol.Sync);
                    btbarRepeat.Label = "All";
                    break;
            }
        }

        private void btbarShuffle_Click(object sender, RoutedEventArgs e)
        {
            switch (MusicManager.pb)
            {
                case MusicManager.Playback.ORDER:
                    MusicManager.pb = MusicManager.Playback.RANDOM;
                    btbarShuffle.Icon = new SymbolIcon(Symbol.Shuffle);
                    break;
                case MusicManager.Playback.RANDOM:
                    MusicManager.pb = MusicManager.Playback.ORDER;
                    btbarShuffle.Icon = new SymbolIcon(Symbol.ShowBcc);
                    break;
            }
        }

        private void progressBar_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {

        }
    }
}
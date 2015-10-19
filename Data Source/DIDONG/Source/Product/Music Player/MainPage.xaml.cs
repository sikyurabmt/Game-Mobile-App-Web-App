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
                tblNowTime.Text = String.Format(@"{0:hh\:mm\:ss}",
                                       BackgroundMediaPlayer.Current.Position);
            }
            if (BackgroundMediaPlayer.Current.NaturalDuration.TotalSeconds == BackgroundMediaPlayer.Current.Position.TotalSeconds)
            {
                if (MusicManager.nof == MusicManager.NumOfLoad.SECOND)
                {
                    musicManager.getNextNumber();
                    startMusic_getMusicProperties(MusicManager.musicList[MusicManager.STT]);
                }
                else
                {
                }
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
            tblAlbum.Text = musicManager.musicProperties.Album;
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
        }

        private void playMusic()
        {
            //mediaShow.Play();
            BackgroundMediaPlayer.Current.Play();
            MusicManager.state = MusicManager.MediaState.PLAY;
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

        private void btList_Click(object sender, RoutedEventArgs e)
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
                    MusicManager.nof = MusicManager.NumOfLoad.SECOND;
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

        private void btRepeat_Click(object sender, RoutedEventArgs e)
        {
            switch (MusicManager.rp)
            {
                case MusicManager.Repeat.ONE:
                    MusicManager.rp = MusicManager.Repeat.NO;
                    btRepeat.Icon = new SymbolIcon(Symbol.RepeatAll);
                    btRepeat.Foreground = new SolidColorBrush(Windows.UI.Colors.Gray);
                    //btRepeat.Label = "No";
                    break;
                case MusicManager.Repeat.ALL:
                    MusicManager.rp = MusicManager.Repeat.ONE;
                    btRepeat.Icon = new SymbolIcon(Symbol.RepeatOne);
                    btRepeat.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
                    //btRepeat.Label = "One";
                    break;
                case MusicManager.Repeat.NO:
                    MusicManager.rp = MusicManager.Repeat.ALL;
                    btRepeat.Icon = new SymbolIcon(Symbol.RepeatAll);
                    btRepeat.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
                    //btRepeat.Label = "All";
                    break;
            }
        }

        private void btShuffle_Click(object sender, RoutedEventArgs e)
        {
            switch (MusicManager.pb)
            {
                case MusicManager.Playback.ORDER:
                    MusicManager.pb = MusicManager.Playback.RANDOM;
                    btShuffle.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
                    break;
                case MusicManager.Playback.RANDOM:
                    MusicManager.pb = MusicManager.Playback.ORDER;
                    btShuffle.Foreground = new SolidColorBrush(Windows.UI.Colors.Gray);
                    break;
            }
        }

        private void progressBar_Tapped(object sender, TappedRoutedEventArgs e)
        {
            BackgroundMediaPlayer.Current.Position = new TimeSpan(0, 0, (int)progressBar.Value);
        }
    }
}
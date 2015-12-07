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


namespace Vez_Music_Player
{
    public sealed partial class MainPage : Page
    {
        MusicManager musicManager = new MusicManager();
        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            musicManager.initValue();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }


        private void ShowInfo()
        {
            tblTitle.Text = MusicManager.musicProperties.Title;
            tblArtist.Text = MusicManager.musicProperties.Artist;
            tblAlbum.Text = MusicManager.musicProperties.Album;
            tblTotalTime.Text = String.Format(@"{0:hh\:mm\:ss}",
                MusicManager.musicProperties.Duration);
            progressBar.Maximum = MusicManager.musicProperties.Duration.TotalSeconds;
        }
        private void startMusic(int STT)
        {
            musicManager.getProperties_accessStream(STT);
            BackgroundMediaPlayer.Current.SetStreamSource(MusicManager.stream);
            ShowInfo();
            MusicManager.state = MusicManager.MediaState.PLAY;
            btPlay.Icon = new SymbolIcon(Symbol.Pause);
            btPlay.Label = "Pause";
        }

        private void playMusic()
        {
            BackgroundMediaPlayer.Current.Play();
            MusicManager.state = MusicManager.MediaState.PLAY;
        }

        private void pauseMusic()
        {
            BackgroundMediaPlayer.Current.Pause();
            MusicManager.state = MusicManager.MediaState.PAUSE;
        }

        private void btPrev_Click(object sender, RoutedEventArgs e)
        {
            musicManager.getPrevNumber();
            startMusic(MusicManager.STT);
        }

        private void btPlay_Click(object sender, RoutedEventArgs e)
        {
            switch (MusicManager.state)
            {
                case MusicManager.MediaState.PLAY:
                    pauseMusic();
                    btPlay.Icon = new SymbolIcon(Symbol.Play);
                    btPlay.Label = "Play";
                    break;
                case MusicManager.MediaState.PAUSE:
                    playMusic();
                    btPlay.Icon = new SymbolIcon(Symbol.Pause);
                    btPlay.Label = "Pause";
                    break;
                case MusicManager.MediaState.STOP:
                    startMusic(MusicManager.STT);
                    break;
                default:
                    break;
            }
        }

        private void btNext_Click(object sender, RoutedEventArgs e)
        {
            musicManager.getNextNumber();
            startMusic(MusicManager.STT);
        }

        private void btbarShuffle_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btbarRepeat_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btShuffle_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btRepeat_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btList_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ExtraPage));
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            switch (MusicManager.nof)
            {
                case MusicManager.NumOfLoad.FIRST:
                    break;
                case MusicManager.NumOfLoad.SECOND:
                    startMusic(MusicManager.STT);
                    break;
            }
        }
    }
}

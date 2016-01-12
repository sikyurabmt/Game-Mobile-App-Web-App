using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Xna.Framework.Media;
using System.Collections.ObjectModel;
using Ahihi_DBz;

namespace PocketSphinxWindowsPhoneDemo
{
    public partial class ListSongPage : PhoneApplicationPage
    {
        MediaLibrary _MediaLibrary = new MediaLibrary();
        SongCollection _Song;
        ObservableCollection<ArrSong> _SourceSong { get; set; }
        ObservableCollection<ArrSong> _SourceArtist { get; set; }
        UIElement uiElement;
        String ArrAlbum, ArrArtist;
        Record record;
        public ListSongPage()
        {
            InitializeComponent();
            _SourceSong = new ObservableCollection<ArrSong>();
            _SourceArtist = new ObservableCollection<ArrSong>();
            _Song = _MediaLibrary.Songs;
            GroupSong();
            GroupAlbum();
            GroupArtist();
            record = new Record(this);
            record.isAvailable = true;
        }

        void GroupSong()
        {
            if (_SourceSong != null)
                _SourceSong.Clear();
            for (int i = 0; i < _Song.Count; i++)
            {
                ArrSong add = new ArrSong(_Song[i].Name.ToString(), _Song[i].Artist.ToString(), _Song[i].Album.ToString());
                _SourceSong.Add(add);
            }

            _SourceArtist = _SourceSong;
            for (int i = 0; i < _SourceArtist.Count - 1; i++)
            {
                for (int j = i + 1; j < _SourceArtist.Count; j++)
                {
                    if (_SourceArtist[i].Artist.ToString() == _SourceArtist[j].Artist.ToString())
                    {
                        _SourceArtist.Remove(_SourceArtist[i]);
                    }
                }
            }

            List<AlphaKeyGroup<ArrSong>> DataSource = AlphaKeyGroup<ArrSong>.CreateGroups(_SourceSong,
                System.Threading.Thread.CurrentThread.CurrentUICulture,
                (ArrSong s) => { return s.Song; }, true);

            AddrSong.ItemsSource = DataSource;
        }

        void GroupAlbum()
        {
            List<AlphaKeyGroup<ArrSong>> DataSource = AlphaKeyGroup<ArrSong>.CreateGroups(_SourceSong,
              System.Threading.Thread.CurrentThread.CurrentUICulture,
              (ArrSong s) => { return s.Album; }, true);

            AddrAlbum.ItemsSource = DataSource;
        }

        void GroupArtist()
        {
            List<AlphaKeyGroup<ArrSong>> DataSource = AlphaKeyGroup<ArrSong>.CreateGroups(_SourceArtist,
              System.Threading.Thread.CurrentThread.CurrentUICulture,
              (ArrSong s) => { return s.Artist; }, true);

            AddrArtist.ItemsSource = DataSource;
        }

        private void Song_Tapped(object sender, System.Windows.Input.GestureEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            int index = -1;
            foreach (var number in _MediaLibrary.Songs)
            {
                index++;
                if (number.Name.Contains(tb.Text))
                {
                    MediaPlayer.Play(number);
                    break;
                }
            }
            string uri = string.Format("/MainPage.xaml?index={0}", index);
            NavigationService.Navigate(new Uri(uri, UriKind.Relative));
            record.StopNativeRecorder();
            record.StopSpeechRecognizerProcessing();
        }

        private void Artist_Tapped(object sender, System.Windows.Input.GestureEventArgs e)
        {
            StackPanel st = (StackPanel)sender;
            int index = -1;
            Point point = e.GetPosition(uiElement); // lay duoc vi tri user tapped
            Double x = point.X;
            String artist = "";
            string song = "";
            foreach (var child in st.Children)
            {
                if (child.GetType().ToString() == "System.Windows.Controls.StackPanel")
                {
                    StackPanel st1 = (StackPanel)child;
                    foreach (var child1 in st1.Children)
                    {
                        if (child1.GetType().ToString() == "System.Windows.Controls.TextBlock")
                        {
                            TextBlock textblock = (TextBlock)child1;
                            if (textblock.Name == "tbArtist")
                            {
                                artist = textblock.Text;
                                for (int i = 0; i < _Song.Count; i++)
                                {
                                    if (_Song[i].Artist.ToString() == artist)
                                        ArrArtist += i.ToString() + "-";
                                }
                            }
                        }
                        if (child1.GetType().ToString() == "System.Windows.Controls.TextBox")
                        {
                            TextBox song1 = (TextBox)child1;
                            if (song1.Name.ToString() == "Song")
                                song = song1.Text;
                        }
                    }
                }
            }
            ArrArtist = ArrArtist.Substring(0, ArrArtist.Length - 1);
            if (x < 180)
            {
                foreach (var number in _MediaLibrary.Songs)
                {
                    index++;
                    if (number.Name.Contains(song))
                    {
                        MediaPlayer.Play(number);
                        break;
                    }
                }
                string uri = string.Format("/MainPage.xaml?ArrArtist={0}&&index={1}", ArrArtist, index);// sang trang song group theo artist
                NavigationService.Navigate(new Uri(uri, UriKind.Relative));

            }
            else
            {
                String kind = "artist";
                string uri = string.Format("/PlaylistPage.xaml?artist={0}&&kind={1}&&ArrArtist={2}", artist, kind, ArrArtist);// sang trang songs group theo artist
                NavigationService.Navigate(new Uri(uri, UriKind.Relative));

            }
            record.StopNativeRecorder();
            record.StopSpeechRecognizerProcessing();
        }

        private void Album_Tapped(object sender, System.Windows.Input.GestureEventArgs e)
        {
            StackPanel st = (StackPanel)sender;
            int index = -1;
            Point point = e.GetPosition(uiElement); // lay duoc vi tri user tapped
            Double x = point.X;
            String album = "";
            String song = "";

            foreach (var child in st.Children)
            {
                if (child.GetType().ToString() == "System.Windows.Controls.StackPanel")
                {
                    StackPanel st1 = (StackPanel)child;
                    foreach (var child1 in st1.Children)
                    {
                        if (child1.GetType().ToString() == "System.Windows.Controls.TextBlock")
                        {
                            TextBlock textblock = (TextBlock)child1;
                            if (textblock.Name == "tbAlbum")
                            {
                                album = textblock.Text;
                                for (int i = 0; i < _Song.Count; i++)
                                {
                                    if (_Song[i].Album.ToString() == album)
                                        ArrAlbum += i.ToString() + "-";
                                }
                            }

                        }

                        if (child1.GetType().ToString() == "System.Windows.Controls.TextBox")
                        {
                            TextBox song1 = (TextBox)child1;
                            song = song1.Text;
                        }
                    }
                }

            }
            ArrAlbum = ArrAlbum.Substring(0, ArrAlbum.Length - 1);
            if (x < 180)
            {
                foreach (var number in _MediaLibrary.Songs)
                {
                    index++;
                    if (number.Name.Contains(song))
                    {

                        MediaPlayer.Play(number);
                        break;
                    }
                }
                string uri = string.Format("/MainPage.xaml?ArrAlbum={0}&&index={1}", ArrAlbum, index);// sang trang song group theo album
                NavigationService.Navigate(new Uri(uri, UriKind.Relative));

            }
            else
            {
                string kind = "album";
                string uri = string.Format("/PlaylistPage.xaml?album={0}&&kind={1}&&ArrAlbum={2}", album, kind, ArrAlbum);// sang trang song group theo album
                NavigationService.Navigate(new Uri(uri, UriKind.Relative));

            }
            record.StopNativeRecorder();
            record.StopSpeechRecognizerProcessing();
        }

        private async void listSong_Loaded(object sender, RoutedEventArgs e)
        {
            await record.InitialzeSpeechRecognizer();
            record.InitializeAudioRecorder();

            //// Start processes
            record.StartSpeechRecognizerProcessing();
            record.StartNativeRecorder();
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {

            e.Cancel = true;

            record.StopNativeRecorder();
            record.StopSpeechRecognizerProcessing();
            NavigationService.GoBack();
        }
    }
    public class ArrSong
    {
        public string Song { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }

        public ArrSong(string song, string artist, string album)
        {
            this.Song = song;
            this.Album = album;
            this.Artist = artist;
        }
    }
}
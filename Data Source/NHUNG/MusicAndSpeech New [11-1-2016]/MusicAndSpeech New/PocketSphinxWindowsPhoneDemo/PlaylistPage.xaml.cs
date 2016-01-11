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
    public partial class PlaylistPage : PhoneApplicationPage
    {
        MediaLibrary _MediaLibrary = new MediaLibrary();
        SongCollection _Songs;
        ObservableCollection<ArrSongGrouped> _SourceSongGrouped { get; set; }
        String album, artist, ArrAlbum, ArrArtist;
        String kind;
        public PlaylistPage()
        {
            InitializeComponent();
            _SourceSongGrouped = new ObservableCollection<ArrSongGrouped>();
            _Songs = _MediaLibrary.Songs;
        }


        void GroupSong_Album()
        {
            if (_SourceSongGrouped != null)
                _SourceSongGrouped.Clear();
            for (int i = 0; i < _Songs.Count; i++)
            {
                if (_Songs[i].Album.ToString() == album)
                {
                    ArrSongGrouped add = new ArrSongGrouped(_Songs[i].Name.ToString(), _Songs[i].Artist.ToString(), _Songs[i].Album.ToString());
                    _SourceSongGrouped.Add(add);
                }

            }

            List<AlphaKeyGroup<ArrSongGrouped>> DataSource = AlphaKeyGroup<ArrSongGrouped>.CreateGroups(_SourceSongGrouped,
                System.Threading.Thread.CurrentThread.CurrentUICulture,
                (ArrSongGrouped s) => { return s.Song; }, true);

            AddrSong1.ItemsSource = DataSource;
        }

        void GroupSong_Artist()
        {
            if (_SourceSongGrouped != null)
                _SourceSongGrouped.Clear();
            for (int i = 0; i < _Songs.Count; i++)
            {
                if (_Songs[i].Artist.ToString() == artist)
                {
                    ArrSongGrouped add = new ArrSongGrouped(_Songs[i].Name.ToString(), _Songs[i].Artist.ToString(), _Songs[i].Album.ToString());
                    _SourceSongGrouped.Add(add);
                }

            }

            List<AlphaKeyGroup<ArrSongGrouped>> DataSource = AlphaKeyGroup<ArrSongGrouped>.CreateGroups(_SourceSongGrouped,
                System.Threading.Thread.CurrentThread.CurrentUICulture,
                (ArrSongGrouped s) => { return s.Song; }, true);

            AddrSong1.ItemsSource = DataSource;
        }

        private void tapped_SongTB(object sender, System.Windows.Input.GestureEventArgs e)
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
            string uri = string.Format("/MainPage.xaml?ArrAlbum={0}&&ArrArtist={1}&&index={2}", ArrAlbum, ArrArtist, index);
            NavigationService.Navigate(new Uri(uri, UriKind.Relative));

        }


        #region Page Load
        private void Song_loaded(object sender, RoutedEventArgs e)
        {

            if (NavigationContext.QueryString.TryGetValue("album", out album))
            {
                album = string.Format("{0}", album);

            }

            if (NavigationContext.QueryString.TryGetValue("artist", out artist))
            {
                artist = string.Format("{0}", artist);
            }

            if (NavigationContext.QueryString.TryGetValue("kind", out kind))
            {
                kind = string.Format("{0}", kind);
            }

            if (NavigationContext.QueryString.TryGetValue("ArrAlbum", out ArrAlbum))
            {
                ArrAlbum = string.Format("{0}", ArrAlbum);
            }

            if (NavigationContext.QueryString.TryGetValue("ArrArtist", out ArrArtist))
            {
                ArrArtist = string.Format("{0}", ArrArtist);
            }
            if (kind == "album")
                GroupSong_Album();
            else
                if (kind == "artist")
                    GroupSong_Artist();
        }
        #endregion

    }
    public class ArrSongGrouped
    {
        public string Song { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }

        public ArrSongGrouped(string song, string artist, string album)
        {
            this.Song = song;
            this.Album = album;
            this.Artist = artist;
        }
    }
}
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
        MediaLibrary library = new MediaLibrary();
        SongCollection songs;
        ObservableCollection<AddSong1> source { get; set; }
        String album, artist, ArrAlbum, ArrArtist;
        String kind;
        public PlaylistPage()
        {
            InitializeComponent();
            source = new ObservableCollection<AddSong1>();
            songs = library.Songs;
        }


        void GroupSong_Album()
        {
            if (source != null)
                source.Clear();
            for (int i = 0; i < songs.Count; i++)
            {
                if (songs[i].Album.ToString() == album)
                {
                    AddSong1 add = new AddSong1(songs[i].Name.ToString(), songs[i].Artist.ToString(), songs[i].Album.ToString());
                    source.Add(add);
                }

            }

            List<AlphaKeyGroup<AddSong1>> DataSource = AlphaKeyGroup<AddSong1>.CreateGroups(source,
                System.Threading.Thread.CurrentThread.CurrentUICulture,
                (AddSong1 s) => { return s.Song; }, true);

            AddrSong1.ItemsSource = DataSource;
        }

        void GroupSong_Artist()
        {
            if (source != null)
                source.Clear();
            for (int i = 0; i < songs.Count; i++)
            {
                if (songs[i].Artist.ToString() == artist)
                {
                    AddSong1 add = new AddSong1(songs[i].Name.ToString(), songs[i].Artist.ToString(), songs[i].Album.ToString());
                    source.Add(add);
                }

            }

            List<AlphaKeyGroup<AddSong1>> DataSource = AlphaKeyGroup<AddSong1>.CreateGroups(source,
                System.Threading.Thread.CurrentThread.CurrentUICulture,
                (AddSong1 s) => { return s.Song; }, true);

            AddrSong1.ItemsSource = DataSource;
        }

        private void tapped_SongTB(object sender, System.Windows.Input.GestureEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            int index = -1;

            foreach (var number in library.Songs)
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
    public class AddSong1
    {
        public string Song { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }

        public AddSong1(string song, string artist, string album)
        {
            this.Song = song;
            this.Album = album;
            this.Artist = artist;
        }
    }
}
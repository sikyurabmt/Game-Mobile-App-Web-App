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

namespace Ahihi_DBz
{
    public partial class ListSong : PhoneApplicationPage
    {
        MediaLibrary library = new MediaLibrary();
        SongCollection songs;
        ObservableCollection<AddSong> source { get; set; }
        UIElement uiElement; 
        String ArrAlbum, ArrArtist;
        public ListSong()
        {
            InitializeComponent();
            source =  new ObservableCollection<AddSong>();
            songs = library.Songs;
            GroupSong();
            GroupAlbum();
            GroupArtist();
        }

         void GroupSong()
         {
             if (source != null)
                 source.Clear();
            for (int i = 0; i < songs.Count; i++)
            {
                AddSong add = new AddSong(songs[i].Name.ToString(), songs[i].Artist.ToString(), songs[i].Album.ToString());
                source.Add(add);
            }

            List<AlphaKeyGroup<AddSong>> DataSource = AlphaKeyGroup<AddSong>.CreateGroups(source,
                System.Threading.Thread.CurrentThread.CurrentUICulture,
                (AddSong s) => { return s.Song; }, true);

            AddrSong.ItemsSource = DataSource;
        }

         void GroupAlbum()
        {
            List<AlphaKeyGroup<AddSong>> DataSource = AlphaKeyGroup<AddSong>.CreateGroups(source,
              System.Threading.Thread.CurrentThread.CurrentUICulture,
              (AddSong s) => { return s.Album; }, true);

            AddrArtist.ItemsSource = DataSource;
        }

         void GroupArtist()
         {
             List<AlphaKeyGroup<AddSong>> DataSource = AlphaKeyGroup<AddSong>.CreateGroups(source,
               System.Threading.Thread.CurrentThread.CurrentUICulture,
               (AddSong s) => { return s.Artist; }, true);

             AddrAlbum.ItemsSource = DataSource;
         }

         private void Song_Tapped(object sender, System.Windows.Input.GestureEventArgs e)
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
             string uri = string.Format("/MainPage.xaml?index={0}",index);
             NavigationService.Navigate(new Uri(uri, UriKind.Relative));
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
                                 for (int i = 0; i < songs.Count; i++)
                                 {
                                     if (songs[i].Artist.ToString() == artist)
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
                 foreach (var number in library.Songs)
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
                 string uri = string.Format("/Songs.xaml?artist={0}&&kind={1}&&ArrArtist={2}", artist, kind, ArrArtist);// sang trang songs group theo artist
                 NavigationService.Navigate(new Uri(uri, UriKind.Relative));

             }
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
                                 for (int i = 0; i < songs.Count; i++)
                                 {
                                     if (songs[i].Album.ToString() == album)
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
                 foreach (var number in library.Songs)
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
                 string uri = string.Format("/Songs.xaml?album={0}&&kind={1}&&ArrAlbum={2}", album, kind, ArrAlbum);// sang trang song group theo album
                 NavigationService.Navigate(new Uri(uri, UriKind.Relative));

             }
         }

         private void AlbumTB_Tapped(object sender, System.Windows.Input.GestureEventArgs e)
         {
             TextBlock tb = (TextBlock)sender;
             try
             {
                 foreach (var number in library.Songs)
                 {
                     if (number.Name.Contains(tb.TextDecorations.ToString()))
                     {
                         MediaPlayer.Play(number);
                     }
                 }
             }
             catch (Exception)
             {
                 NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
             }
         }
    }
    public class AddSong
    {
        public string Song { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }

        public AddSong(string song, string artist, string album)
        {
            this.Song = song;
            this.Album = album;
            this.Artist = artist;
        }
    }
}
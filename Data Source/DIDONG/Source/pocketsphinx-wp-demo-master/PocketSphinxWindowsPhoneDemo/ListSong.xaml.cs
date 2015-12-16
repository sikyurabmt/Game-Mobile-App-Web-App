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
using Windows.Devices.Geolocation;
using PocketSphinxWindowsPhoneDemo;

namespace Ahihi_DBz
{
    public partial class ListSong : PhoneApplicationPage
    {
        MediaLibrary library = new MediaLibrary();
        SongCollection songs;
       // AlbumCollection album;
       // List<AddSong> source;
        ObservableCollection<AddSong> source { get; set; }
        public ListSong()
        {
            InitializeComponent();
            //List<AddSong> source = new List<AddSong>();
            source =  new ObservableCollection<AddSong>();
            songs = library.Songs;
            GroupSong();
            GroupAlbum();
            GroupArtist();
        }

         void GroupSong()
         {  
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

         private void tapped_albumTB(object sender, System.Windows.Input.GestureEventArgs e)
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
             catch(Exception)
             {
                 NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
             }
          
             
         }

         private void tapped_SongTB(object sender, System.Windows.Input.GestureEventArgs e)
         {
             TextBlock tb = (TextBlock)sender;

             foreach (var number in library.Songs)
             {
                 if (number.Name.Contains(tb.Text))
                 {
                     MediaPlayer.Play(number);
                 }
             }
             NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
         }

         private void sp_tapped(object sender, System.Windows.Input.GestureEventArgs e)
         {
             StackPanel st = (StackPanel)sender;

             foreach (var child in st.Children)
             {
                 if (child.GetType().ToString() == "System.Windows.Controls.StackPanel")
                 {
                     StackPanel st1 = (StackPanel)child;
                     foreach(var child1 in st1.Children)
                     {
                         if(child1.GetType().ToString() == "System.Windows.Controls.TextBox")
                         {
                             TextBox textbox = (TextBox)child1;
                             foreach (var number in library.Songs)
                             {
                                 if (number.Name.Contains(textbox.Text))
                                 {
                                     MediaPlayer.Play(number);
                                 }
                             }
                             break;
                         }
                     }
                    
                     NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                 }
             }
         }

        #region 
         protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
         {
             if (App.Geolocator == null)
             {
                 App.Geolocator = new Geolocator();
                 App.Geolocator.DesiredAccuracy = PositionAccuracy.High;
                 App.Geolocator.MovementThreshold = 100; // The units are meters.
                 App.Geolocator.PositionChanged += geolocator_PositionChanged;
             }
         }

         protected override void OnRemovedFromJournal(System.Windows.Navigation.JournalEntryRemovedEventArgs e)
         {
             App.Geolocator.PositionChanged -= geolocator_PositionChanged;
             App.Geolocator = null;
         }

         void geolocator_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
         {

             if (!App.RunningInBackground)
             {
                 MediaPlayer.Play(songs,0);
             }
             else
             {
                 Microsoft.Phone.Shell.ShellToast toast = new Microsoft.Phone.Shell.ShellToast();
                 toast.Content = args.Position.Coordinate.Latitude.ToString("0.00");
                 toast.Title = "Location: ";
                 toast.NavigationUri = new Uri("/Page2.xaml", UriKind.Relative);
                 toast.Show();

             }
         }
        #endregion Test Background
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PocketSphinxWindowsPhoneDemo.Resources;
using System.Diagnostics;
using PocketSphinxRntComp;
using PocketSphinxWindowsPhoneDemo.Recorder;
using System.Threading.Tasks;
using File_Manager;
using System.Windows.Threading;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Xna.Framework.Media;

namespace PocketSphinxWindowsPhoneDemo
{
    /// <summary>
    /// PocketSphinx implementation for Windows Phone
    /// pure code; no MVVM and all in 1 code behind file
    /// 
    /// Created by Toine de Boer, Enbyin (NL)
    /// </summary>
    public partial class MainPage : PhoneApplicationPage
    {
        SettingManager st = new SettingManager();
        DispatcherTimer playTimer;
        String ArrAlbum, ArrArtist;
        String[] ArrAlbumIndex, ArrArtistIndex;
        int[] AlbumIndex, ArtistIndex;
        String indexNavigate;
        Record2 record2;
        public MainPage()
        {
            InitializeComponent();
            st.FileReader();
            record2 = new Record2(this, 1);
            SetProperties();
            LoadSettingProperties();
            CheckAvailable();
            playTimer = new DispatcherTimer();
            playTimer.Interval = TimeSpan.FromSeconds(1); //one second
            playTimer.Tick += new EventHandler(playTimer_Tick);
            playTimer.Start();
        }

        private void playTimer_Tick(object sender, object e)
        {
            if (Record2.mm.IsPlaying() == true)
            {
                progressBar.Value = Record2.mm.GetNowSecondsOfSong();
                tblNowTime.Text = String.Format(@"{0:hh\:mm\:ss}", Record2.mm.GetNowTimeSpanOfSong());
            }
            if (Record2.mm.GetNowSecondsOfSong() >= Record2.mm.GetTotalSecondsOfSong() - 0.5)
            {
                Record2.mm.AutoNext();
                SetProperties();
            }

            if (Record2.indexPage == 1)
                SetProperties();

            StateMessageBlock.Text = Record2.strFinal;
        }

        private void LoadSettingProperties()
        {
            switch (SettingManager._Color)
            {
                case SettingManager.Color.BLUE:
                    tblTitle.Foreground = tblArtist.Foreground = tblAlbum.Foreground = tblNowTime.Foreground = tblTotalTime.Foreground = new SolidColorBrush(Colors.Blue);
                    //tblTitle.Foreground = new SolidColorBrush(Color.FromArgb(158, 203, 211, 100));
                    //tblArtist.Foreground = new SolidColorBrush(Color.FromArgb(189, 186, 247, 100));
                    //tblAlbum.Foreground = new SolidColorBrush(Color.FromArgb(189, 186, 247, 100));
                    break;
                case SettingManager.Color.RED:
                    tblTitle.Foreground = tblArtist.Foreground = tblAlbum.Foreground = tblNowTime.Foreground = tblTotalTime.Foreground = new SolidColorBrush(Colors.Red);
                    //tblTitle.Foreground = new SolidColorBrush(Color.FromArgb(250, 50, 50, 100));
                    //tblArtist.Foreground = new SolidColorBrush(Color.FromArgb(247, 130, 130, 100));
                    //tblAlbum.Foreground = new SolidColorBrush(Color.FromArgb(247, 130, 130, 100));
                    break;
                case SettingManager.Color.YELLOW:
                    tblTitle.Foreground = tblArtist.Foreground = tblAlbum.Foreground = tblNowTime.Foreground = tblTotalTime.Foreground = new SolidColorBrush(Colors.Yellow);
                    //tblTitle.Foreground = new SolidColorBrush(Color.FromArgb(230, 234, 20, 100));
                    //tblArtist.Foreground = new SolidColorBrush(Color.FromArgb(190, 190, 80, 100));
                    //tblAlbum.Foreground = new SolidColorBrush(Color.FromArgb(190, 190, 80, 100));
                    break;
                default:
                    break;
            }

            ImageBrush background;
            switch (SettingManager._Theme)
            {
                case SettingManager.Theme.WINTER:
                    background = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri("/Images/winter.jpg", UriKind.Relative)),
                        Stretch = Stretch.UniformToFill
                    };
                    LayoutRoot.Background = background;
                    break;
                case SettingManager.Theme.SPRING:
                    background = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri("/Images/spring.jpg", UriKind.Relative)),
                        Stretch = Stretch.UniformToFill
                    };
                    LayoutRoot.Background = background;
                    break;
                default:
                    break;
            }
        }

        private void CheckAvailable()
        {
            if (!Record2.mm.SongCollectionIsAvailable())
            {
                MessageBox.Show("Couldn't find your library music!");
                Application.Current.Terminate();
            }
        }

        private void SetProperties()
        {
            Record2.mm.FileWriter(Record2.mm.GetIndexOfNowPlay());
            tblTitle.Text = Record2.mm.GetTitle();
            tblArtist.Text = Record2.mm.GetArtist();
            tblAlbum.Text = Record2.mm.GetAlbum();
            tblTotalTime.Text = String.Format(@"{0:hh\:mm\:ss}", Record2.mm.GetTotalTimeSpanOfSong());
            progressBar.Maximum = Record2.mm.GetTotalSecondsOfSong();
        }

        private void SetDefault()
        {
            tblNowTime.Text = "00:00:00";
            progressBar.Value = 0;
            ManagerButton();
        }

        private void ManagerButton()
        {
            if (Record2.mm.IsPlaying())
            {
                (ApplicationBar.Buttons[1] as ApplicationBarIconButton).Text = "pause";
                ((ApplicationBar.Buttons[1] as ApplicationBarIconButton) as ApplicationBarIconButton).IconUri = new Uri("/Images/pause.png", UriKind.Relative);
            }
            if (Record2.mm.IsPaused())
            {
                (ApplicationBar.Buttons[1] as ApplicationBarIconButton).Text = "play";
                (ApplicationBar.Buttons[1] as ApplicationBarIconButton).IconUri = new Uri("/Images/play.png", UriKind.Relative);
            }
            if (Record2.mm.IsStopped())
            {
                (ApplicationBar.Buttons[1] as ApplicationBarIconButton).Text = "play";
                (ApplicationBar.Buttons[1] as ApplicationBarIconButton).IconUri = new Uri("/Images/play.png", UriKind.Relative);
            }
        }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            if (NavigationContext.QueryString.TryGetValue("ArrAlbum", out ArrAlbum))
            {
                ArrAlbum = string.Format("{0}", ArrAlbum);
                if (ArrAlbum != "")
                {
                    ArrAlbumIndex = ArrAlbum.Split('-');
                    AlbumIndex = new int[ArrAlbumIndex.Length];

                    for (int i = 0; i < ArrAlbumIndex.Length; i++)
                    {
                        int index = int.Parse((ArrAlbumIndex[i]).ToString());
                        AlbumIndex[i] = index;
                    }
                    Record2.mm.isGroup = true;
                    Record2.mm.Arr = new int[AlbumIndex.Length];
                    Record2.mm.Arr = AlbumIndex;
                }
            }

            if (NavigationContext.QueryString.TryGetValue("ArrArtist", out ArrArtist))
            {

                ArrArtist = string.Format("{0}", ArrArtist);

                if (ArrArtist != "")
                {
                    ArrArtistIndex = ArrArtist.Split('-');
                    ArtistIndex = new int[ArrArtistIndex.Length];
                    for (int i = 0; i < ArrArtistIndex.Length; i++)
                    {
                        int index1 = int.Parse((ArrArtistIndex[i]).ToString());
                        ArtistIndex[i] = index1;
                    }
                    Record2.mm.isGroup = true;
                    Record2.mm.Arr = new int[ArtistIndex.Length];
                    Record2.mm.Arr = ArtistIndex;
                }
            }


            if (NavigationContext.QueryString.TryGetValue("index", out indexNavigate))
            {
                indexNavigate = string.Format("{0}", indexNavigate);
                MusicManager._NowPlay = Convert.ToInt32(indexNavigate);
            }
            SetProperties();
        }

        private void appbar_previous_click(object sender, EventArgs e)
        {
            Record2.PreviousProcess();
        }

        private void appbar_play_click(object sender, EventArgs e)
        {
            Record2.PlayOrPauseProcess();
            ManagerButton();
        }

        private void appbar_stop_click(object sender, EventArgs e)
        {
            Record2.StopProcess();
            SetDefault();
        }

        private void appbar_next_click(object sender, EventArgs e)
        {
            Record2.NextProcess();
        }

        private void appbar_list_click(object sender, EventArgs e)
        {
            Record2.ListProcess();
        }

        private void appbar_option_click(object sender, EventArgs e)
        {
            Record2.SettingProcess();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            Record2.InitAll();
            StateMessageBlock.Text = "Ready!!!";
        }
    }
}
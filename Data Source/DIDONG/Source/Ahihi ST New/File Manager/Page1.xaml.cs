using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

using System.Windows.Threading;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace File_Manager
{
    public partial class Page1 : PhoneApplicationPage
    {
        MusicManager mm = new MusicManager();
        SettingManager st = new SettingManager();
        DispatcherTimer playTimer;
        public Page1()
        {
            InitializeComponent();

            st.FileReader();

            CheckAvailable();
            SetProperties();
            LoadSettingProperties();

            playTimer = new DispatcherTimer();
            playTimer.Interval = TimeSpan.FromSeconds(1); //one second
            playTimer.Tick += new EventHandler(playTimer_Tick);
            playTimer.Start();
        }

        private void playTimer_Tick(object sender, object e)
        {
            if (mm.IsPlaying() == true)
            {
                progressBar.Value = mm.GetNowSecondsOfSong();
                tblNowTime.Text = String.Format(@"{0:hh\:mm\:ss}", mm.GetNowTimeSpanOfSong());
            }
            if (mm.GetNowSecondsOfSong() == mm.GetTotalSecondsOfSong())
            {
                mm.AutoNext();
                SetProperties();
            }
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
            if (!mm.SongCollectionIsAvailable())
            {
                MessageBox.Show("Couldn't find your library music!");
                Application.Current.Terminate();
            }
        }

        private void SetProperties()
        {
            tblTitle.Text = mm.GetTitle();
            tblArtist.Text = mm.GetArtist();
            tblAlbum.Text = mm.GetAlbum();
            tblTotalTime.Text = String.Format(@"{0:hh\:mm\:ss}", mm.GetTotalTimeSpanOfSong());
            progressBar.Maximum = mm.GetTotalSecondsOfSong();
        }

        private void SetDefault()
        {
            tblNowTime.Text = "00:00:00";
            progressBar.Value = 0;
            ManagerButton();
        }

        private void ManagerButton()
        {
            if (mm.IsPlaying())
            {
                (ApplicationBar.Buttons[1] as ApplicationBarIconButton).Text = "pause";
                ((ApplicationBar.Buttons[1] as ApplicationBarIconButton) as ApplicationBarIconButton).IconUri = new Uri("/Images/pause.png", UriKind.Relative);
            }
            if (mm.IsPaused())
            {
                (ApplicationBar.Buttons[1] as ApplicationBarIconButton).Text = "play";
                (ApplicationBar.Buttons[1] as ApplicationBarIconButton).IconUri = new Uri("/Images/play.png", UriKind.Relative);
            }
            if (mm.IsStopped())
            {
                (ApplicationBar.Buttons[1] as ApplicationBarIconButton).Text = "play";
                (ApplicationBar.Buttons[1] as ApplicationBarIconButton).IconUri = new Uri("/Images/play.png", UriKind.Relative);
            }
        }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            if (mm.IsPlaying())
            {
                SetProperties();
            }
        }

        private void appbar_previous_click(object sender, EventArgs e)
        {
            mm.PlayPrevious();
            SetProperties();
        }

        private void appbar_play_click(object sender, EventArgs e)
        {
            mm.PlayOrPause();
            ManagerButton();
        }

        private void appbar_stop_click(object sender, EventArgs e)
        {
            mm.Stop();
            SetDefault();
        }

        private void appbar_next_click(object sender, EventArgs e)
        {
            mm.PlayNext();
            SetProperties();
        }

        private void appbar_list_click(object sender, EventArgs e)
        {
            
        }

        private void appbar_option_click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/SettingPage.xaml", UriKind.Relative));
        }
    }
}
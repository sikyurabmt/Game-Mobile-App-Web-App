using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Ahihi_DBz.Resources;

namespace Ahihi_DBz
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void appbar_previous_click(object sender, EventArgs e)
        {

        }

        private void appbar_play_click(object sender, EventArgs e)
        {

        }

        private void appbar_stop_click(object sender, EventArgs e)
        {

        }

        private void appbar_next_click(object sender, EventArgs e)
        {

        }

        private void appbar_list_click(object sender, EventArgs e)
        {
            NavigationService.Navigate( new Uri("/ListSong.xaml", UriKind.Relative));
        }

        private void appbar_option_click(object sender, EventArgs e)
        {

        }

        
    }
}
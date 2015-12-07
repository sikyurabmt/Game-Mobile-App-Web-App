﻿using System;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Vez_Music_Player
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ExtraPage : Page
    {
        MusicManager musicManager = new MusicManager();
        public ExtraPage()
        {
            this.InitializeComponent();
            getListByName();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void getListByName()
        {
            foreach (var item in MusicManager.musicList)
            {
                lbList.Items.Add(item.Name);
            }
        }

        private void lbList_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MusicManager.STT = Convert.ToInt32(lbList.SelectedIndex);
            MusicManager.nof = MusicManager.NumOfLoad.SECOND;
            Frame.GoBack();
        }
    }
}

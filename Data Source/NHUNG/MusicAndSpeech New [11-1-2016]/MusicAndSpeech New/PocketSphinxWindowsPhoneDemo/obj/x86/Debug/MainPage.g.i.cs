﻿#pragma checksum "C:\Users\Hang BY\Desktop\MusicAndSpeech New\MusicAndSpeech New\PocketSphinxWindowsPhoneDemo\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "0D0632A43000102DFF8CF5F898703859"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace PocketSphinxWindowsPhoneDemo {
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.TextBlock tblTitle;
        
        internal System.Windows.Controls.TextBlock tblArtist;
        
        internal System.Windows.Controls.TextBlock tblAlbum;
        
        internal System.Windows.Controls.Slider progressBar;
        
        internal System.Windows.Controls.TextBlock tblNowTime;
        
        internal System.Windows.Controls.TextBlock tblTotalTime;
        
        internal System.Windows.Controls.TextBlock StateMessageBlock;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/PocketSphinxWindowsPhoneDemo;component/MainPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.tblTitle = ((System.Windows.Controls.TextBlock)(this.FindName("tblTitle")));
            this.tblArtist = ((System.Windows.Controls.TextBlock)(this.FindName("tblArtist")));
            this.tblAlbum = ((System.Windows.Controls.TextBlock)(this.FindName("tblAlbum")));
            this.progressBar = ((System.Windows.Controls.Slider)(this.FindName("progressBar")));
            this.tblNowTime = ((System.Windows.Controls.TextBlock)(this.FindName("tblNowTime")));
            this.tblTotalTime = ((System.Windows.Controls.TextBlock)(this.FindName("tblTotalTime")));
            this.StateMessageBlock = ((System.Windows.Controls.TextBlock)(this.FindName("StateMessageBlock")));
        }
    }
}


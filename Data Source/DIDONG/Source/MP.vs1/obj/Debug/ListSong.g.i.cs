﻿#pragma checksum "C:\Users\Hang BY\Desktop\Nhúng\Ahihi DBz\ListSong.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "41DA6B2C6299D6EEDA8C58F5A38DA146"
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


namespace Ahihi_DBz {
    
    
    public partial class ListSong : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal Microsoft.Phone.Controls.LongListSelector AddrSong;
        
        internal Microsoft.Phone.Controls.LongListSelector AddrAlbum;
        
        internal Microsoft.Phone.Controls.LongListSelector AddrArtist;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/Ahihi%20DBz;component/ListSong.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.AddrSong = ((Microsoft.Phone.Controls.LongListSelector)(this.FindName("AddrSong")));
            this.AddrAlbum = ((Microsoft.Phone.Controls.LongListSelector)(this.FindName("AddrAlbum")));
            this.AddrArtist = ((Microsoft.Phone.Controls.LongListSelector)(this.FindName("AddrArtist")));
        }
    }
}


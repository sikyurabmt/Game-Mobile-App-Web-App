using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using FlipTile.Resources;

namespace FlipTile
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

        private void btUpdated_Click(object sender, RoutedEventArgs e)
        {
            /*
            Uri mp = new Uri("/MainPage.xaml?", UriKind.Relative);// Navigate to the page for modifying Application Tile properties.
            ShellTile PinnedTile = ShellTile.ActiveTiles.First();

            FlipTileData TileData = new FlipTileData
            {
                Title = "Flip Tile",

                Count = 10,
                
                SmallBackgroundImage = new Uri("/Assets/Tiles/Image1.png", UriKind.Relative),
                BackgroundImage = new Uri("/Assets/Tiles/Image2.png", UriKind.Relative),
                BackBackgroundImage = new Uri("/Assets/Tiles/Image3.png", UriKind.Relative),

                WideBackgroundImage = new Uri("/Assets/Tiles/LargeImage1.png", UriKind.Relative),
                WideBackBackgroundImage = new Uri("/Assets/Tiles/LargeImage2.png", UriKind.Relative),

                BackTitle = "Flip Tile", // title when it flip
                BackContent = "Flip Tile", // content when it flip
                WideBackContent = "Seminar" // content of WideBackground
            };

            PinnedTile.Update(TileData);
            ShellTile.Create(mp, TileData, true);
            */
            FlipTileData flipTile = new FlipTileData
            {
                Title = "Flip Tile",
                BackTitle = "SE114.G13", // title when it flip
                BackContent = "KTPM2013", // content when it flip
                WideBackContent = "Nhập môn phần mềm và hệ thống nhúng", // content of WideBackground
                Count = 8,

                SmallBackgroundImage = new Uri("/Assets/Tiles/Image1.png", UriKind.Relative),
                BackgroundImage = new Uri("/Assets/Tiles/Image2.png", UriKind.Relative),
                BackBackgroundImage = new Uri("/Assets/Tiles/Image3.png", UriKind.Relative),
                WideBackgroundImage = new Uri("/Assets/Tiles/LargeImage1.png", UriKind.Relative),
                WideBackBackgroundImage = new Uri("/Assets/Tiles/LargeImage2.png", UriKind.Relative)
            };

            string uri = string.Concat("/MainPage.xaml?", "id=flip");
            ShellTile shellTile = checkTile(uri);

            if (shellTile == null)
            {
                ShellTile.Create(new Uri(uri, UriKind.Relative), flipTile, true);
            }
            else
            {
                shellTile.Update(flipTile);
            }
        }

        private ShellTile checkTile(string uri)
        {
            ShellTile shellTile = ShellTile.ActiveTiles.FirstOrDefault(tile => tile.NavigationUri.ToString().Contains(uri));
            return shellTile;
        }
    }
}
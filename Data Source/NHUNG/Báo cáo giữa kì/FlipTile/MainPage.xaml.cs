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

        private void bt_Updated(object sender, RoutedEventArgs e)
        {
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
          //  ShellTile.Create(mp, TileData, true);
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}
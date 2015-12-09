using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using CycleTile.Resources;

namespace CycleTile
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

        private void bt_Update(object sender, RoutedEventArgs e)
        {
            ShellTile ShellTile = ShellTile.ActiveTiles.First();
            Uri mp = new Uri("/MainPage.xaml?", UriKind.Relative);
            CycleTileData TileData = new CycleTileData
            {
                Title = "Cycle Tile",
                Count = 10,
                SmallBackgroundImage = new Uri("/Assets/Tiles/Image1.png", UriKind.Relative),//Gets and sets the front-side background image for the small Tile size
                CycleImages = new Uri[]
                     {
                         new Uri("/Assets/Tiles/Image1.png",UriKind.Relative),
                         new Uri("/Assets/Tiles/Image2.png",UriKind.Relative),
                          new Uri("/Assets/Tiles/Image3.png",UriKind.Relative),
                     }
            };
            ShellTile.Update(TileData);
            ShellTile.Create(mp, TileData, true);
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
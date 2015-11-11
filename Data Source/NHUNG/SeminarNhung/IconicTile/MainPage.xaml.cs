using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using IconicTile.Resources;
using System.Windows.Media;

namespace IconicTile
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

            this.NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative)); //// Navigate to the page for modifying Application Tile properties.
            Uri mp = new Uri("/MainPage.xaml?", UriKind.Relative);
            string strDay = DateTime.Today.ToString();


            IconicTileData TileData = new IconicTileData()
            {
                Title = "Iconic Tile",
                Count = 10,
                WideContent1 = strDay,
                WideContent2 = "a",
                WideContent3 = "b",
                SmallIconImage = new Uri("Assets/Tiles/CalendarIcon.png", UriKind.Relative),//Gets or sets the icon image for the small Tile size
                //IconImage = new Uri("Assets/Tiles/CalendarIcon.png", UriKind.Relative),//Gets or sets the icon image for the medium and large Tile sizes
               // BackgroundColor = Color.FromArgb(255, 255, 255, 255),
            };


            //ShellTile ShellTile = ShellTile.ActiveTiles.First();
            ShellTile ShellTile = ShellTile.ActiveTiles.FirstOrDefault();
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
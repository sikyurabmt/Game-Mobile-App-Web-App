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
            /*
            this.NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative)); // Navigate to the page for modifying Application Tile properties.
            Uri mp = new Uri("/MainPage.xaml?", UriKind.Relative);
            string strDay = DateTime.Today.ToString().Substring(0,10);


            IconicTileData TileData = new IconicTileData()
            {
                Title = "Iconic Tile",
                Count = 10,
                WideContent1 = strDay,
                WideContent2 = "WideContent2",
                WideContent3 = "WideContent3",
                SmallIconImage = new Uri("Assets/Tiles/Image1.png", UriKind.Relative),//Gets or sets the icon image for the small Tile size
                //IconImage = new Uri("Assets/Tiles/CalendarIcon.png", UriKind.Relative),//Gets or sets the icon image for the medium and large Tile sizes
               // BackgroundColor = Color.FromArgb(255, 255, 255, 255),
            };


            ShellTile ShellTile = ShellTile.ActiveTiles.First();
            ShellTile.Update(TileData);
            ShellTile.Create(mp, TileData, true);
             */
            string strDay = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
            string strTime = string.Format("{0:hh:mm:ss}", DateTime.Now);

            IconicTileData iconicTile = new IconicTileData()
            {
                Title = "Iconic Tile",
                Count = 99,
                WideContent1 = strDay,
                WideContent2 = strTime,
                WideContent3 = "Hello World",
                SmallIconImage = new Uri("Assets/Tiles/Iconic.png", UriKind.Relative),
                IconImage = new Uri("Assets/Tiles/Iconic.png", UriKind.Relative)
            };

            string uri = string.Concat("/MainPage.xaml?", "id=iconic");
            ShellTile shellTile = checkTile(uri);


            if (shellTile == null)
            {
                ShellTile.Create(new Uri(uri, UriKind.Relative), iconicTile, true);
            }
            else
            {
                shellTile.Update(iconicTile);
            }
        }

        private ShellTile checkTile(string uri)
        {
            ShellTile shellTile = ShellTile.ActiveTiles.FirstOrDefault(tile => tile.NavigationUri.ToString().Contains(uri));
            return shellTile;
        }
    }
}
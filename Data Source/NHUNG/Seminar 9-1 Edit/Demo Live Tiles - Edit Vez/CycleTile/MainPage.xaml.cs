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

        private void btUpdated_Click(object sender, RoutedEventArgs e)
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
                        new Uri("/Assets/Tiles/Image4.png",UriKind.Relative),
                        new Uri("/Assets/Tiles/Image5.png",UriKind.Relative)
                     }
            };
            ShellTile.Update(TileData);
            ShellTile.Create(mp, TileData, true);
            /*
            CycleTileData cycleTile = new CycleTileData
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

            string uri = string.Concat("/MainPage.xaml?", "id=cycle");
            ShellTile shellTile = checkTile(uri);

            if (shellTile == null)
            {
                ShellTile.Create(new Uri(uri, UriKind.Relative), cycleTile, true);
            }
            else
            {
                shellTile.Update(cycleTile);
            }
            */
        }

        private ShellTile checkTile(string uri)
        {
            ShellTile shellTile = ShellTile.ActiveTiles.FirstOrDefault(tile => tile.NavigationUri.ToString().Contains(uri));
            return shellTile;
        }
    }
}
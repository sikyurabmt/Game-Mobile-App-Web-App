using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Calendar.Resources;


namespace Calendar
{
    public partial class MainPage : PhoneApplicationPage
    {
        #region Constructor
        DateTime calendarDate;
        string result = "";
        public MainPage()
        {
            InitializeComponent();
            calendarDate = DateTime.Today;
            Initialize_Calendar(calendarDate);

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }
        #endregion

        #region Init Calendar
        void Initialize_Calendar(DateTime date)
        {
            CalendarHeader.Text = date.ToString("MMMM yyyy");
            date = new DateTime(date.Year, date.Month, 1);
            int dayOfWeek = (int)date.DayOfWeek + 1;
            int daysOfMonth = DateTime.DaysInMonth(date.Year, date.Month);
            int i = 1;
            foreach (var o1 in Calendar.Children)
            {
                foreach (var o2 in (o1 as StackPanel).Children)
                {
                    var o3 = (o2 as Grid).Children[0] as TextBlock;
                    if (i >= dayOfWeek && i < (daysOfMonth + dayOfWeek))
                        o3.Text = (i - dayOfWeek + 1).ToString();
                    else
                        o3.Text = "";
                    i++;
                }
            }
        }
        private void previousMonth(object sender, RoutedEventArgs e)
        {
            calendarDate = calendarDate.AddMonths(-1);
            Initialize_Calendar(calendarDate);
        }

        private void nextMonth(object sender, RoutedEventArgs e)
        {
            calendarDate = calendarDate.AddMonths(1);
            Initialize_Calendar(calendarDate);
        }
        #endregion

        #region Event Click
        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            string date = DateTime.Now.Date.ToString().Substring(0,10);
            string uri = string.Format("/MyEvent.xaml?date={0}", date);
            NavigationService.Navigate(new Uri(uri, UriKind.Relative));
        }
        #endregion

        #region Load and Update Tile
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
           
           if(NavigationContext.QueryString.TryGetValue("result", out result))
           {
               result = string.Format("{0}", result);
           }

            ShellTile PinnedTile = ShellTile.ActiveTiles.First();

            FlipTileData TileData = new FlipTileData
            {
                Title = result,

                Count = 10,

                SmallBackgroundImage = new Uri("/Assets/Tiles/SmallCalendarIcon.png", UriKind.Relative),
                BackgroundImage = new Uri("/Assets/Tiles/BackGroundImage.png", UriKind.Relative),
                BackBackgroundImage = new Uri("/Assets/Tiles/BackGroundImage.png", UriKind.Relative),

                WideBackgroundImage = new Uri("/Assets/Tiles/LargeBackgroundImage.png", UriKind.Relative),
                WideBackBackgroundImage = new Uri("/Assets/Tiles/LargeBackgroundImage.png", UriKind.Relative),

                BackTitle = result, // title when it flip
                BackContent = result, // content when it flip
                WideBackContent = "Seminar Flip Tile" // content of WideBackground
            };

            PinnedTile.Update(TileData);
        }
        #endregion
    }
}
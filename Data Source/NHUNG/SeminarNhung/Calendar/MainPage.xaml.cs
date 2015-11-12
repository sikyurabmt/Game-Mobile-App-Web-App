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
        // Constructor
        DateTime calendarDate;
        List<EventData> list = new List<EventData>();

        public MainPage()
        {
            InitializeComponent();
            calendarDate = DateTime.Today;
            Initialize_Calendar(calendarDate);


           // var k = PhoneApplicationService.Current.State["List"];
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

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

        private void tap1(object sender, System.Windows.Input.GestureEventArgs e)
        {
           
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
           
            NavigationService.Navigate(new Uri("/MyEvent.xaml", UriKind.Relative));
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            string subject, local, date;
           if(NavigationContext.QueryString.TryGetValue("subject", out subject))
           {
               subject = string.Format("subject: {0}", subject);
           }

            if(NavigationContext.QueryString.TryGetValue("local", out local))
            {
                local = string.Format("\tLocal: {0}", local);
            }

            if(NavigationContext.QueryString.TryGetValue("date", out date))
            {
                date = string.Format("\tDate: {0}", date);
            }

            tb_test.Text = subject + local + date;

            ShellTile PinnedTile = ShellTile.ActiveTiles.First();

            FlipTileData TileData = new FlipTileData
            {
                Title = "Flip Tile",

                Count = 10,

                SmallBackgroundImage = new Uri("/Assets/Tiles/SmallCalendarIcon.png", UriKind.Relative),
                BackgroundImage = new Uri("/Assets/Tiles/BackGroundImage.png", UriKind.Relative),
                BackBackgroundImage = new Uri("/Assets/Tiles/BackGroundImage.png", UriKind.Relative),

                WideBackgroundImage = new Uri("/Assets/Tiles/LargeBackgroundImage.png", UriKind.Relative),
                WideBackBackgroundImage = new Uri("/Assets/Tiles/LargeBackgroundImage.png", UriKind.Relative),

                BackTitle = tb_test.Text, // title when it flip
                BackContent =  tb_test.Text, // content when it flip
                WideBackContent = "Seminar Flip Tile" // content of WideBackground
            };

            PinnedTile.Update(TileData);
        }
    }
}
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
       
        public MainPage()
        {
            InitializeComponent();
            calendarDate = DateTime.Today;
            Initialize_Calendar(calendarDate);

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

        

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            //var obj = PhoneApplicationService.Current.State["List"];

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

     
     
    }

   
}
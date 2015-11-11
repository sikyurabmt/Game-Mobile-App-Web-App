using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Calendar
{
    public partial class MyEvent : PhoneApplicationPage
    {
        List<EventData> List;
        Navigate navigate;
        public MyEvent()
        {
            InitializeComponent();
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            List = new List<EventData>();
            EventData data = new EventData();
            Uri a = new Uri("/MainPage.xaml", UriKind.Relative);
           // navigate = new Navigate(this.NavigationService,a,  List);
            data.strSubject = txtSubject.Text;
            data.strLocal = txtLocal.Text;
            data.date = DateTime.Today;
            List.Add(data);
            PhoneApplicationService.Current.State["List"] = List;
           // NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            //NavigationService.Navigate(new Uri("/MainPage.xaml?object1=" + List, UriKind.Relative));
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
           
        }
        public class EventData
        {
            public string strSubject { get; set; }
            public string strLocal { get; set; }
            public DateTime date { get; set; }

        }
    }
}
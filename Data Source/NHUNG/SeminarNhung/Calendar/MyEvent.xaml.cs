using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Collections.ObjectModel;

namespace Calendar
{
    public partial class MyEvent : PhoneApplicationPage
    {
        public List<EventData> List;
        public ObservableCollection<ListDate> listdate { get; set; }
        public ObservableCollection<ListYear> listyear { get; set; }
        public ObservableCollection<ListMonth> listmonth { get; set; }

        private int date, month,  year;
        DateTime dateResult;
        public MyEvent()
        {
            InitializeComponent();
            month = DateTime.Now.Month;
            year = DateTime.Now.Year;
            date = DateTime.Now.Day;
            InitListYear();
            InitListMonth();
            InitlistDate();
           
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            List = new List<EventData>();
            EventData data = new EventData();
         
            data.strSubject = txtSubject.Text;
            data.strLocal = txtLocal.Text;
            data.date = DateTime.Today;
            string strdate = data.date.ToString();
            List.Add(data);
            string uri = string.Format("/MainPage.xaml?subject={0}&local={1}&date={2}" ,data.strSubject,data.strLocal,strdate);
            NavigationService.Navigate(new Uri(uri, UriKind.Relative));
        }
        void InitlistDate()
        {
            listdate = new ObservableCollection<ListDate>();
            
            int maxsodate = 31;
            switch (month)
            {
                case 4:
                case 6:
                case 9:
                case 11: maxsodate = 30; break;
            }

            if (month == 2)
            {
               
                if (NamNhuan(year))
                    maxsodate = 28;
                else
                    maxsodate = 29;
            }
            for (int i = 1; i < maxsodate + 1; i++)
            {
                string strdate = Convert.ToString(i);
                listdate.Add(new ListDate { strDate = strdate });
            }
            listDate.ItemsSource = listdate;
           
        }

        void InitListYear()
        {
            listyear = new ObservableCollection<ListYear>();
            int yearhientai = DateTime.Now.Year;
            year = yearhientai;
            for (int i = yearhientai; i < yearhientai + 5; i++)
            {
                string stryear = Convert.ToString(i);
                listyear.Add(new ListYear { strYear = stryear });
            }
            listYear.ItemsSource = listyear;
        }

        void InitListMonth()
        {
            listmonth = new ObservableCollection<ListMonth>();
            for (int i = 1; i < 13; i++)
            {
                string strmonth = Convert.ToString(i);
                listmonth.Add(new ListMonth { strMonth = strmonth });
            }
            listMonth.ItemsSource = listmonth;
        }

        public class ListDate
        {
            public string strDate { get; set; }
        }
        public class ListMonth
        {
            public string strMonth{get;set;}
        }

        public class ListYear
        {
            public string strYear{get;set;}
        }
       
        private void listDate_Tapped(object sender, System.Windows.Input.GestureEventArgs e)
        {
            date = Convert.ToInt32(listDate.SelectedIndex + 1);
        }

        private void listDate_Loaded(object sender, RoutedEventArgs e)
        {
            //InitlistDate();
        }

        bool NamNhuan(int year)
        {
            if (year % 4 != 0) // khong
                return false;
            else
                if (year % 400 == 0) // co
                    return true;
                else
                    if (year % 100 == 0) // khong
                        return false;
                    else
                        return true;
        }
        private void listDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           // InitlistDate();
             date = Convert.ToInt32(listDate.SelectedIndex  + 1);
        }

        private void listMonth_Loaded(object sender, RoutedEventArgs e)
        {
            month = Convert.ToInt32(listMonth.SelectedIndex + 1);
            //dateResult = new DateTime(date, month, year);
        }

        private void listYear_Loaded(object sender, RoutedEventArgs e)
        {
            year = Convert.ToInt32(listYear.SelectedIndex + DateTime.Now.Year + 1);
        }

        private void listMonth_Tapped(object sender, System.Windows.Input.GestureEventArgs e)
        {
            month = Convert.ToInt32(listMonth.SelectedIndex+1);
            InitlistDate();
        }

        private void listYear_Tapped(object sender, System.Windows.Input.GestureEventArgs e)
        {
             year = Convert.ToInt32(listYear.SelectedIndex+ DateTime.Now.Year+1);
            InitlistDate();
        }

        private void listMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            month = Convert.ToInt32(listMonth.SelectedIndex + 1);
        }

        private void listYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            year = Convert.ToInt32(listYear.SelectedIndex + DateTime.Now.Year + 1);
        }

       

      
    }
}
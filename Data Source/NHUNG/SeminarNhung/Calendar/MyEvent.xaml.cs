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
using System.Threading.Tasks;
using Windows.Storage;
using System.IO;

namespace Calendar
{
    public partial class MyEvent : PhoneApplicationPage
    {

        #region Declare
        public ObservableCollection<ListDate> listdate { get; set; }
        public ObservableCollection<ListYear> listyear { get; set; }
        public ObservableCollection<ListMonth> listmonth { get; set; }

        private int date, month,  year;
        string sdate, result;
        #endregion

        #region Contructor
        public MyEvent()
        {
            InitializeComponent();
            month = DateTime.Now.Month;
            year = DateTime.Now.Year;
            date = DateTime.Now.Day;
            InitListYear();
            InitListMonth();
            InitlistDate();
            result = "";
        }
        #endregion

        #region write, read file
        async Task WriteText()
        {
            string content = "";
            //Thư mục mặc định của ứng dụng
            StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
            //Tạo thư mục mới ở trong thư mục trên, nếu có rồi thì chỉ mở thư mục
            var dataFolder = await local.CreateFolderAsync("CalendarFolder",
                CreationCollisionOption.OpenIfExists);
            //Mở file CalendarDataFile.txt trong thư mục trên
            //try
            //{
            //    var fileReader = await dataFolder.OpenStreamForReadAsync("CalendarDataFile.txt");

            //    //Gán hết tất cả kí tự trong file trên vào content
            //    using (StreamReader streamReader = new StreamReader(fileReader))
            //    {
            //        content = streamReader.ReadToEnd();
            //    }
            //}
            //catch (Exception)
            //{

            //    throw;
            //}
            //Nối content với chuỗi vừa nhập
            content += "#object@" + txtSubject.Text + "@" + txtLocal.Text + "@3#endobject";
            byte[] fileBytes = System.Text.Encoding.UTF8.GetBytes(content.ToCharArray());
            //Tạo lại file ghi
            var fileWriter = await dataFolder.CreateFileAsync("CalendarDataFile.txt",
                CreationCollisionOption.ReplaceExisting);
            //Ghi tất cả kí tự cũ + vừa nhập vào file
            using (var s = await fileWriter.OpenStreamForWriteAsync())
            {
                s.Write(fileBytes, 0, fileBytes.Length);
            }
            await ReadText();
        }

        async Task ReadText()
        {
            StorageFolder localFD = Windows.Storage.ApplicationData.Current.LocalFolder;
            if (localFD != null)
            {
                var dataFolder = await localFD.GetFolderAsync("CalendarFolder");
                var file = await dataFolder.OpenStreamForReadAsync("CalendarDataFile.txt");
                using (StreamReader streamReader = new StreamReader(file))
                {
                    result = streamReader.ReadToEnd();
                }
            }

            string copy = result;
            string end = "#endobject";

            int dateLocal = copy.LastIndexOf(sdate);
            int endLocal = copy.LastIndexOf(end);

            string remove = copy.Remove(endLocal - 1, copy.Length - endLocal + 1);
            string jec, local;

            int ilocal = remove.LastIndexOf("@");
            local = remove.Substring(ilocal + 1);
            remove = remove.Substring(0, ilocal);

            int iobject = remove.LastIndexOf("@");
            jec = remove.Substring(iobject + 1);
            remove = remove.Substring(0, iobject);

            result = jec + local;
        }

        #endregion

        #region Save Click
        async private void btSave_Click(object sender, RoutedEventArgs e)
        {
            await WriteText();

            string uri = string.Format("/MainPage.xaml?result={0}", result);

            NavigationService.Navigate(new Uri(uri, UriKind.Relative));
        }
        #endregion

        #region Init Lists
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
               
                if (CheckLapYear(year))
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

        bool CheckLapYear(int year)
        {
            if (year % 4 != 0)
                return false;
            else
                if (year % 400 == 0)
                    return true;
                else
                    if (year % 100 == 0)
                        return false;
                    else
                        return true;
        }
        #endregion

        #region Class
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
        #endregion

        #region Event of listDate, listMonth, listYear
        private void listDate_Tapped(object sender, System.Windows.Input.GestureEventArgs e)
        {
            date = Convert.ToInt32(listDate.SelectedIndex + 1);
        }
        private void listDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
             date = Convert.ToInt32(listDate.SelectedIndex  + 1);
        }

        private void listMonth_Loaded(object sender, RoutedEventArgs e)
        {
            month = Convert.ToInt32(listMonth.SelectedIndex + 1);
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
        #endregion

        #region Load Page
        private void MyEvent_Loaded(object sender, RoutedEventArgs e)
        {
            if (NavigationContext.QueryString.TryGetValue("date", out sdate))
            {
                sdate = string.Format("{0}", sdate);
            }
        }
        #endregion
    }
}
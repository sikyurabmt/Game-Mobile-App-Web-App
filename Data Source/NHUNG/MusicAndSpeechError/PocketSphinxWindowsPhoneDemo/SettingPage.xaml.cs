using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using File_Manager;
using System.Windows.Media.Imaging;

namespace PocketSphinxWindowsPhoneDemo
{
    public partial class SettingPage : PhoneApplicationPage
    {
        private string _ImagePath;

        private string sPlayback, sRepeat, sTheme, sColor;

        SettingManager _SettingManager = new SettingManager();

        Record record;
      
        public SettingPage()
        {
            InitializeComponent();
            _SettingManager.FileReader();
            SetDefaultRadioButton();
            record = new Record(this);
            record.isAvailable = true;
        }
        private void LoadImage(string ImagePath)
        {
            if (ImagePath != "")
            {
                try
                {
                    image_theme.Stretch = System.Windows.Media.Stretch.UniformToFill;
                    image_theme.Source = new BitmapImage(new Uri(ImagePath, UriKind.Relative));
                }
                catch (Exception e)
                {
                    //Say something
                }
            }
        }
        private void SetDefaultRadioButton()
        {
            //Playback
            switch (PlayManager._Playback)
            {
                case PlayManager.Playback.ORDER:
                    rbtn_playback_order.IsChecked = true;
                    break;
                case PlayManager.Playback.RANDOM:
                    rbtn_playback_random.IsChecked = true;
                    break;
                default:
                    break;
            }
            //Repeat
            switch (PlayManager._Repeat)
            {
                case PlayManager.Repeat.ALL:
                    rbtn_repeat_all.IsChecked = true;
                    break;
                case PlayManager.Repeat.NO:
                    rbtn_repeat_no.IsChecked = true;
                    break;
                case PlayManager.Repeat.ONE:
                    rbtn_repeat_one.IsChecked = true;
                    break;
                default:
                    break;
            }
            //Theme
            switch (SettingManager._Theme)
            {
                case SettingManager.Theme.WINTER:
                    rbtn_theme_winter.IsChecked = true;
                    _ImagePath = "Images/winter.jpg";
                    break;
                case SettingManager.Theme.SPRING:
                    rbtn_theme_spring.IsChecked = true;
                    _ImagePath = "Images/spring.jpg";
                    break;
                default:
                    break;
            }
            //Color
            switch (SettingManager._Color)
            {
                case SettingManager.Color.BLUE:
                    rbtn_color_blue.IsChecked = true;
                    break;
                case SettingManager.Color.RED:
                    rbtn_color_red.IsChecked = true;
                    break;
                case SettingManager.Color.YELLOW:
                    rbtn_color_yellow.IsChecked = true;
                    break;
                default:
                    break;
            }
            //Load Image
            LoadImage(_ImagePath);
        }
        private void rbtn_theme_winter_Checked(object sender, RoutedEventArgs e)
        {
            _ImagePath = "Images/winter.jpg";
            LoadImage(_ImagePath);
        }
        private void rbtn_theme_spring_Checked(object sender, RoutedEventArgs e)
        {
            _ImagePath = "Images/spring.jpg";
            LoadImage(_ImagePath);
        }
        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            //Playback
            if (rbtn_playback_order.IsChecked == true)
            {
                sPlayback = "ORDER";
            }
            if (rbtn_playback_random.IsChecked == true)
            {
                sPlayback = "RANDOM";
            }
            //Repeat
            if (rbtn_repeat_all.IsChecked == true)
            {
                sRepeat = "ALL";
            }
            if (rbtn_repeat_no.IsChecked == true)
            {
                sRepeat = "NO";
            }
            if (rbtn_repeat_one.IsChecked == true)
            {
                sRepeat = "ONE";
            }
            //Theme
            if (rbtn_theme_winter.IsChecked == true)
            {
                sTheme = "WINTER";
            }
            if (rbtn_theme_spring.IsChecked == true)
            {
                sTheme = "SPRING";
            }
            //Color
            if (rbtn_color_blue.IsChecked == true)
            {
                sColor = "BLUE";
            }
            if (rbtn_color_red.IsChecked == true)
            {
                sColor = "RED";
            }
            if (rbtn_color_yellow.IsChecked == true)
            {
                sColor = "YELLOW";
            }
            //Write file
            _SettingManager.FileWriter(sPlayback, sRepeat, sTheme, sColor);
            record.StopNativeRecorder();
            record.StopSpeechRecognizerProcessing();
            NavigationService.GoBack();
        }
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new Uri("/Page1.xaml", UriKind.Relative));
            record.StopNativeRecorder();
            record.StopSpeechRecognizerProcessing();
            NavigationService.GoBack();
        }

        private async void Setting_Loaded(object sender, RoutedEventArgs e)
        {
            await record.InitialzeSpeechRecognizer();
            record.InitializeAudioRecorder();

            //// Start processes
            record.StartSpeechRecognizerProcessing();
            record.StartNativeRecorder();
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {

            e.Cancel = true;

            record.StopNativeRecorder();
            record.StopSpeechRecognizerProcessing();
            NavigationService.GoBack();
        }
    }
}
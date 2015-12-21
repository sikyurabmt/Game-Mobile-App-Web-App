using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PocketSphinxWindowsPhoneDemo.Resources;
using System.Diagnostics;
using PocketSphinxRntComp;
using PocketSphinxWindowsPhoneDemo.Recorder;
using System.Threading.Tasks;
using File_Manager;
using System.Windows.Threading;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PocketSphinxWindowsPhoneDemo
{
    /// <summary>
    /// PocketSphinx implementation for Windows Phone
    /// pure code; no MVVM and all in 1 code behind file
    /// 
    /// Created by Toine de Boer, Enbyin (NL)
    /// </summary>
    public partial class MainPage : PhoneApplicationPage
    {
        MusicManager mm = new MusicManager();
        SettingManager st = new SettingManager();
        DispatcherTimer playTimer;
        public MainPage()
        {
            InitializeComponent();

            st.FileReader();

            CheckAvailable();
            SetProperties();
            LoadSettingProperties();

            playTimer = new DispatcherTimer();
            playTimer.Interval = TimeSpan.FromSeconds(1); //one second
            playTimer.Tick += new EventHandler(playTimer_Tick);
            playTimer.Start();
        }

        private void playTimer_Tick(object sender, object e)
        {
            if (mm.IsPlaying() == true)
            {
                progressBar.Value = mm.GetNowSecondsOfSong();
                tblNowTime.Text = String.Format(@"{0:hh\:mm\:ss}", mm.GetNowTimeSpanOfSong());
            }
            if (mm.GetNowSecondsOfSong() == mm.GetTotalSecondsOfSong())
            {
                mm.AutoNext();
                SetProperties();
            }
        }

        private void LoadSettingProperties()
        {
            switch (SettingManager._Color)
            {
                case SettingManager.Color.BLUE:
                    tblTitle.Foreground = tblArtist.Foreground = tblAlbum.Foreground = tblNowTime.Foreground = tblTotalTime.Foreground = new SolidColorBrush(Colors.Blue);
                    //tblTitle.Foreground = new SolidColorBrush(Color.FromArgb(158, 203, 211, 100));
                    //tblArtist.Foreground = new SolidColorBrush(Color.FromArgb(189, 186, 247, 100));
                    //tblAlbum.Foreground = new SolidColorBrush(Color.FromArgb(189, 186, 247, 100));
                    break;
                case SettingManager.Color.RED:
                    tblTitle.Foreground = tblArtist.Foreground = tblAlbum.Foreground = tblNowTime.Foreground = tblTotalTime.Foreground = new SolidColorBrush(Colors.Red);
                    //tblTitle.Foreground = new SolidColorBrush(Color.FromArgb(250, 50, 50, 100));
                    //tblArtist.Foreground = new SolidColorBrush(Color.FromArgb(247, 130, 130, 100));
                    //tblAlbum.Foreground = new SolidColorBrush(Color.FromArgb(247, 130, 130, 100));
                    break;
                case SettingManager.Color.YELLOW:
                    tblTitle.Foreground = tblArtist.Foreground = tblAlbum.Foreground = tblNowTime.Foreground = tblTotalTime.Foreground = new SolidColorBrush(Colors.Yellow);
                    //tblTitle.Foreground = new SolidColorBrush(Color.FromArgb(230, 234, 20, 100));
                    //tblArtist.Foreground = new SolidColorBrush(Color.FromArgb(190, 190, 80, 100));
                    //tblAlbum.Foreground = new SolidColorBrush(Color.FromArgb(190, 190, 80, 100));
                    break;
                default:
                    break;
            }

            ImageBrush background;
            switch (SettingManager._Theme)
            {
                case SettingManager.Theme.WINTER:
                    background = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri("/Images/winter.jpg", UriKind.Relative)),
                        Stretch = Stretch.UniformToFill
                    };
                    LayoutRoot.Background = background;
                    break;
                case SettingManager.Theme.SPRING:
                    background = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri("/Images/spring.jpg", UriKind.Relative)),
                        Stretch = Stretch.UniformToFill
                    };
                    LayoutRoot.Background = background;
                    break;
                default:
                    break;
            }
        }
     
        private void CheckAvailable()
        {
            if (!mm.SongCollectionIsAvailable())
            {
                MessageBox.Show("Couldn't find your library music!");
                Application.Current.Terminate();
            }
        }

        private void SetProperties()
        {
            tblTitle.Text = mm.GetTitle();
            tblArtist.Text = mm.GetArtist();
            tblAlbum.Text = mm.GetAlbum();
            tblTotalTime.Text = String.Format(@"{0:hh\:mm\:ss}", mm.GetTotalTimeSpanOfSong());
            progressBar.Maximum = mm.GetTotalSecondsOfSong();
        }

        private void SetDefault()
        {
            tblNowTime.Text = "00:00:00";
            progressBar.Value = 0;
            ManagerButton();
        }

        private void ManagerButton()
        {
            if (mm.IsPlaying())
            {
                (ApplicationBar.Buttons[1] as ApplicationBarIconButton).Text = "pause";
                ((ApplicationBar.Buttons[1] as ApplicationBarIconButton) as ApplicationBarIconButton).IconUri = new Uri("/Images/pause.png", UriKind.Relative);
            }
            if (mm.IsPaused())
            {
                (ApplicationBar.Buttons[1] as ApplicationBarIconButton).Text = "play";
                (ApplicationBar.Buttons[1] as ApplicationBarIconButton).IconUri = new Uri("/Images/play.png", UriKind.Relative);
            }
            if (mm.IsStopped())
            {
                (ApplicationBar.Buttons[1] as ApplicationBarIconButton).Text = "play";
                (ApplicationBar.Buttons[1] as ApplicationBarIconButton).IconUri = new Uri("/Images/play.png", UriKind.Relative);
            }
        }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            if (mm.IsPlaying())
            {
                SetProperties();
            }
        }

        private void PreviousProcess()
        {
            mm.PlayPrevious();
            SetProperties();
        }

        private void NextProcess()
        {
            mm.PlayNext();
            SetProperties();
        }

        private void PlayOrPauseProcess()
        {
            mm.PlayOrPause();
            ManagerButton();
        }

        private void StopProcess()
        {
            mm.Stop();
            SetDefault();
        }

        private void SettingProcess()
        {
            NavigationService.Navigate(new Uri("/SettingPage.xaml", UriKind.Relative));
        }

        private void appbar_previous_click(object sender, EventArgs e)
        {
            PreviousProcess();
        }

        private void appbar_play_click(object sender, EventArgs e)
        {
            PlayOrPauseProcess();
        }

        private void appbar_stop_click(object sender, EventArgs e)
        {
            StopProcess();
        }

        private void appbar_next_click(object sender, EventArgs e)
        {
            NextProcess();
        }

        private void appbar_list_click(object sender, EventArgs e)
        {
            
        }

        private void appbar_option_click(object sender, EventArgs e)
        {
            SettingProcess();
        }



        //
        //  NHAN
        //  DIEN
        //  GIONG
        //  NOI
        //



        private const string WakeupText = "something new";

        private string[] DigitValues = new string[] { "play", "pause", "stop", "next", "previous", "setting", "list"};

        private RecognizerMode _mode = RecognizerMode.Wakeup;
        private RecognizerMode Mode
        {
            get 
            { 
                return _mode; 
            }
            set 
            {
                if(_mode != value)
                {
                    _mode = value;
                    SetRecognizerMode(_mode);
                }
            }
        }

        private SpeechRecognizer speechRecognizer;

        private WasapiAudioRecorder audioRecorder;

        private enum RecognizerMode { Wakeup, Digits};

        private async void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            // Initializing
            await InitialzeSpeechRecognizer();
            InitializeAudioRecorder();

            // Start processes
            StartSpeechRecognizerProcessing();
            StartNativeRecorder();

            StateMessageBlock.Text = "ready for use";
        }

        private void FindMatchToToggle(string recognizedText)
        {
            bool matchFound = false; 

            switch (Mode)
            {
                case RecognizerMode.Wakeup:
                    matchFound = (recognizedText == WakeupText);
                    break;
                case RecognizerMode.Digits:
                    var recognizedWords = recognizedText.Split(' ');
                    foreach (var word in recognizedWords)
                    {
                        if (DigitValues.Contains(word.ToLower()))
                        {
                            recognizedText = word;
                            matchFound = true;
                        }
                    }                    
                    break;
            }

            if (matchFound)
            {
                ToggleSearch();
            }
        }

        private void ToggleSearch()
        {
            switch(Mode)
            {
                case RecognizerMode.Wakeup:
                    Mode = RecognizerMode.Digits;
                    break;
                case RecognizerMode.Digits:
                    Mode = RecognizerMode.Wakeup;
                    break;
            }
        }

        private void SetRecognizerMode(RecognizerMode mode)
        {
            string result = string.Empty;
            speechRecognizer.StopProcessing();
            Debug.WriteLine(result);
            result = speechRecognizer.SetSearch(mode.ToString());
            Debug.WriteLine(result);
            speechRecognizer.StartProcessing();
            Debug.WriteLine(result);
        }

        private async Task InitialzeSpeechRecognizer()
        {
            List<string> initResults = new List<string>();

            try
            {
                AudioContainer.SphinxSpeechRecognizer = new SpeechRecognizer();
                speechRecognizer = AudioContainer.SphinxSpeechRecognizer;

                speechRecognizer.resultFound += speechRecognizer_resultFound;
                speechRecognizer.resultFinalizedBySilence += speechRecognizer_resultFinalizedBySilence;

                // Load Async
                await Task.Run(() =>
                {
                    var initResult = speechRecognizer.Initialize("\\Assets\\models\\hmm\\en-us-semi", "\\Assets\\models\\dict\\cmu07a.dic");
                    initResults.Add(initResult);
                    initResult = speechRecognizer.AddKeyphraseSearch(RecognizerMode.Wakeup.ToString(), WakeupText);
                    initResults.Add(initResult);
                    initResult = speechRecognizer.AddGrammarSearch(RecognizerMode.Digits.ToString(), "\\Assets\\models\\grammar\\digits.gram");
                    initResults.Add(initResult);
                    initResult = speechRecognizer.AddNgramSearch("forecast", "\\Assets\\models\\lm\\weather.dmp");
                    initResults.Add(initResult);
                });

                SetRecognizerMode(Mode);
            }
            catch (Exception ex)
            {
                var initResult = ex.Message;                
                initResults.Add(initResult);
            }

            foreach (var result in initResults)
            {
                Debug.WriteLine(result);
            }
        }

        private void StartSpeechRecognizerProcessing()
        {
            string result = string.Empty;

            try
            {
                if(speechRecognizer.IsProcessing())
                {
                    result = "PocketSphinx already started";
                }
                else
                {
                    result = speechRecognizer.StartProcessing();
                }                
            }
            catch
            {
                result = "Starting PocketSphinx processing failed";
            }

            Debug.WriteLine(result);
        }

        private void StopSpeechRecognizerProcessing()
        {
            string result = string.Empty;

            try
            {
                result = speechRecognizer.StopProcessing();
            }
            catch
            {
                result = "Stopping PocketSphinx processing failed";
            }

            Debug.WriteLine(result);
        }

        void speechRecognizer_resultFinalizedBySilence(string finalResult)
        {
            Debug.WriteLine("final result found: {0}", finalResult);
            FindMatchToToggle(finalResult);
        }

        void speechRecognizer_resultFound(string result)
        {
            Debug.WriteLine("result found: {0}", result);
            StateMessageBlock.Text = string.Format("found: {0}", result);
            switch (result)
            {
                case "play":
                    PlayOrPauseProcess();
                    break;
                case "pause":
                    PlayOrPauseProcess();
                    break;
                case "stop":
                    StopProcess();
                    break;
                case "next":
                    NextProcess();
                    break;
                case "previous":
                    PreviousProcess();
                    break;
                case "setting":
                    //SettingProcess();
                    break;
                case "list":
                    break;
                default:
                    break;
            }
        }

        private void InitializeAudioRecorder()
        {
            AudioContainer.AudioRecorder = new WasapiAudioRecorder();
            audioRecorder = AudioContainer.AudioRecorder;
            audioRecorder.BufferReady += audioRecorder_BufferReady;

            Debug.WriteLine("AudioRecorder Initialized");
        }

        private void StartNativeRecorder()
        {
            audioRecorder.StartRecording();
        }

        private void StopNativeRecorder()
        {
            audioRecorder.StopRecording();
        }

        void audioRecorder_BufferReady(object sender, AudioDataEventArgs e)
        {
            int registerResult = 0;
            try
            {
                registerResult = speechRecognizer.RegisterAudioBytes(e.Data);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                StopNativeRecorder();
                StopSpeechRecognizerProcessing();
                StateMessageBlock.Text = "all stoped because of error";
            }
        }
    }
}
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
using Windows.Devices.Geolocation;
using Microsoft.Xna.Framework.Media;

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

        #region Constant values

        private const string WakeupText = "something new";

        private string[] DigitValues = new string[] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

        private string[] MenuValues = new string[] { "digits", "forecast" };

        #endregion

        #region Properties

        private RecognizerMode _mode = RecognizerMode.Wakeup;
        private RecognizerMode Mode
        {
            get { return _mode; }
            set
            {
                if (_mode != value)
                {
                    _mode = value;
                    SetRecognizerMode(_mode);
                }
            }
        }

        #endregion

        #region Fields

        private SpeechRecognizer speechRecognizer;

        private WasapiAudioRecorder audioRecorder;

        private enum RecognizerMode { Wakeup, Digits, Menu };

        #endregion

        #region Constructor & Loaded event

        public MainPage()
        {
            InitializeComponent();
            songs = a.Songs;
        }

        private async void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            // Initializing
            await InitialzeSpeechRecognizer();
            InitializeAudioRecorder();

            // Start processes
            StartSpeechRecognizerProcessing();
            StartNativeRecorder();
        }

        #endregion

        #region Business Logic Methods

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
                case RecognizerMode.Menu:
                    matchFound = (MenuValues.Contains(recognizedText.ToLower()));
                    break;
            }

            if (matchFound)
            {
                //MainMessageBlock.Text = recognizedText;
                //ToggleSearch();
                NavigationService.Navigate(new Uri("/ListSong.xaml", UriKind.Relative));
            }
        }

        private void ToggleSearch()
        {
            switch (Mode)
            {
                case RecognizerMode.Wakeup:
                    Mode = RecognizerMode.Digits;
                    break;
                case RecognizerMode.Digits:
                    Mode = RecognizerMode.Menu;
                    break;
                case RecognizerMode.Menu:
                    Mode = RecognizerMode.Digits;
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

        #endregion

        #region SpeechRecognizer Methods (PocketSphinx)

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
                    initResult = speechRecognizer.AddGrammarSearch(RecognizerMode.Menu.ToString(), "\\Assets\\models\\grammar\\menu.gram");
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
                if (speechRecognizer.IsProcessing())
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
            tblStateMessageBlock.Text = string.Format("found: {0}", result);
        }

        #endregion

        #region Recording Methods

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
                tblStateMessageBlock.Text = "all stoped because of error";
            }

            // incoming raw sound
            //Debug.WriteLine("{0} bytes of raw audio recieved, {1} frames processed at PocketSphinx", e.Data.Length, registerResult);
        }

        #endregion

        #region

        MediaLibrary a = new MediaLibrary();
        SongCollection songs;
        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (App.Geolocator == null)
            {
                App.Geolocator = new Geolocator();
                App.Geolocator.DesiredAccuracy = PositionAccuracy.High;
                App.Geolocator.MovementThreshold = 100; // The units are meters.
                App.Geolocator.PositionChanged += geolocator_PositionChanged;
            }
        }

        protected override void OnRemovedFromJournal(System.Windows.Navigation.JournalEntryRemovedEventArgs e)
        {
            App.Geolocator.PositionChanged -= geolocator_PositionChanged;
            App.Geolocator = null;
        }

        void geolocator_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {

            if (App.RunningInBackground)
            {
                MediaPlayer.Play(songs, 0);
            }
            else
            {
                Microsoft.Phone.Shell.ShellToast toast = new Microsoft.Phone.Shell.ShellToast();
                toast.Content = args.Position.Coordinate.Latitude.ToString("0.00");
                toast.Title = "Location: ";
                toast.NavigationUri = new Uri("/Page2.xaml", UriKind.Relative);
                toast.Show();

            }
        }
        #endregion Test background

        private void appbar_previous_click(object sender, EventArgs e)
        {

        }

        private void appbar_play_click(object sender, EventArgs e)
        {

        }

        private void appbar_stop_click(object sender, EventArgs e)
        {

        }

        private void appbar_next_click(object sender, EventArgs e)
        {

        }

        private void appbar_list_click(object sender, EventArgs e)
        {

        }

        private void appbar_option_click(object sender, EventArgs e)
        {

        }
    }
}
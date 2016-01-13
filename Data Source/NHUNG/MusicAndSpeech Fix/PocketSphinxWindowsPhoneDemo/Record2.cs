using File_Manager;
using PocketSphinxRntComp;
using PocketSphinxWindowsPhoneDemo.Recorder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace PocketSphinxWindowsPhoneDemo
{
    public class Record2
    {
        public Record2(Page main, int index)
         {
             page = main;
             mm  = new MusicManager();
             indexPage = index;
         }

        public static MusicManager mm;
        public static Page page;

        public static bool isOtherPage = false;
        public static bool isAvailable = false; // truong hop dang o list doc lai list se bi stop luon
        public static int indexPage; // 1: MainPage, 2: ListSongPage, 3: PlayListPage, 4 : SettingPage;
        public static string strFinal = "";

        public static async void InitAll()
        {
            _mode = RecognizerMode.Wakeup;

            await InitialzeSpeechRecognizer();
            InitializeAudioRecorder();

            StartSpeechRecognizerProcessing();
            StartNativeRecorder();
        }


        private static string WakeupText = "something new";

        private static string[] SpeechValues = new string[] { "play", "pause", "stop", "next", "previous", "go option", "go list", "exit please", "close please", "cancel please", "back please" };

        public static RecognizerMode _mode = RecognizerMode.Wakeup;
        public static RecognizerMode Mode
        {
            get
            {
                return _mode;
            }
            set
            {
                if (_mode != value)
                {
                    _mode = value;
                    SetRecognizerMode(_mode);
                }
            }
        }
        public static SpeechRecognizer speechRecognizer;

        public static WasapiAudioRecorder audioRecorder;

        public enum RecognizerMode { Wakeup, Digits};

        public static void SetRecognizerMode(RecognizerMode mode)
        {
            string result = string.Empty;
            speechRecognizer.StopProcessing();
            Debug.WriteLine(result);
            result = speechRecognizer.SetSearch(mode.ToString());
            Debug.WriteLine(result);
            speechRecognizer.StartProcessing();
            Debug.WriteLine(result);
        }

        public static async Task InitialzeSpeechRecognizer()
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
        static void speechRecognizer_resultFound(string result)
        {
            Debug.WriteLine("result found: {0}", result);
            strFinal = result;
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
                case "go list":
                    ListProcess();
                    break;
                case "go option":
                    SettingProcess();
                    break;
                case "cancel please":
                    if (indexPage == 4)
                    {
                        BackProcess();
                    }
                    break;
                case "back please":
                    if (indexPage != 1)
                    {
                        BackProcess();
                    }
                    break;
                case "close please":
                    CloseApplication();
                    break;
                case "exit please":
                    ExitApplication();
                    break;
            }
        }

        static void speechRecognizer_resultFinalizedBySilence(string finalResult)
        {
            Debug.WriteLine("final result found: {0}", finalResult);
            FindMatchToToggle(finalResult);

        }
        private static void FindMatchToToggle(string recognizedText)
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
                        if (SpeechValues.Contains(word.ToLower()))
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
        public static void ToggleSearch()
        {
            switch (Mode)
            {
                case RecognizerMode.Wakeup:
                    Mode = RecognizerMode.Digits;
                    break;
                case RecognizerMode.Digits:
                    Mode = RecognizerMode.Wakeup;
                    break;
            }
        }
        public static void StartSpeechRecognizerProcessing()
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

        public static void StopSpeechRecognizerProcessing()
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
        public static void InitializeAudioRecorder()
        {
            AudioContainer.AudioRecorder = new WasapiAudioRecorder();
            audioRecorder = AudioContainer.AudioRecorder;
            audioRecorder.BufferReady += audioRecorder_BufferReady;

            Debug.WriteLine("AudioRecorder Initialized");
        }

        public static void StartNativeRecorder()
        {
            audioRecorder.StartRecording();
        }

        public static void StopNativeRecorder()
        {
            audioRecorder.StopRecording();
        }

        static void audioRecorder_BufferReady(object sender, AudioDataEventArgs e)
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
            }
        }

        public static void StartAll()
        {
            _mode = RecognizerMode.Wakeup;
            StartSpeechRecognizerProcessing();
            StartNativeRecorder();
        }

        public static void StopAll()
        {
            StopNativeRecorder();
            StopSpeechRecognizerProcessing();
        }

        public static void ListProcess()
        {
            StopAll();
            page.NavigationService.Navigate(new Uri("/ListSongPage.xaml", UriKind.RelativeOrAbsolute));
        }

        public static void SettingProcess()
        {
            StopAll();
            page.NavigationService.Navigate(new Uri("/SettingPage.xaml", UriKind.Relative));
        }

        public static void BackProcess()
        {
            StopAll();
            page.NavigationService.GoBack();
        }

        public static void PreviousProcess()
        {
            mm.PlayPrevious();
        }

        public static void NextProcess()
        {
            mm.PlayNext();
        }

        public static void PlayOrPauseProcess()
        {
            mm.PlayOrPause();

        }

        public static void StopProcess()
        {
            mm.Stop();
        }

        public static void ExitApplication()
        {
            StopNativeRecorder();
            StopSpeechRecognizerProcessing();
            Application.Current.Terminate();
        }

        public static void CloseApplication()
        {
            StopNativeRecorder();
            StopSpeechRecognizerProcessing();
            StopProcess();
            Application.Current.Terminate();
        }
    }
}

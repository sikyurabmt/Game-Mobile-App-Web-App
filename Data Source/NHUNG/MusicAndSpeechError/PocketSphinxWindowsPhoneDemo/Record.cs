using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PocketSphinxRntComp;
using PocketSphinxWindowsPhoneDemo.Recorder;
using System.Diagnostics;
using File_Manager;
using System.Windows.Navigation;
using System.Windows.Controls;
using PocketSphinxWindowsPhoneDemo;

namespace PocketSphinxWindowsPhoneDemo
{

    
     class Record
    {
         Page page;
         MusicManager mmRecord;
         public static bool isOtherPage = false;
         public bool isAvailable = false; // truong hop dang o list doc lai list se bi stop luon
         
        //
        //  NHAN
        //  DIEN
        //  GIONG
        //  NOI
        //
         public Record(Page main)
         {
             page = main;
             mmRecord  = new MusicManager();
             
         }

         public Record()
         {
             MainPage main = new MainPage();
             mmRecord = new MusicManager();
         }
       

        public const string WakeupText = "go to home";

        public string[] DigitValues = new string[] { "play", "pause", "stop", "next", "previous", "setting", "list" };

        public RecognizerMode _mode = RecognizerMode.Wakeup;
        public RecognizerMode Mode
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

        public SpeechRecognizer speechRecognizer;

        public WasapiAudioRecorder audioRecorder;

        public enum RecognizerMode { Wakeup, Digits };

       

        public void FindMatchToToggle(string recognizedText)
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

        public void ToggleSearch()
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

        public void SetRecognizerMode(RecognizerMode mode)
        {
            string result = string.Empty;
            speechRecognizer.StopProcessing();
            Debug.WriteLine(result);
            result = speechRecognizer.SetSearch(mode.ToString());
            Debug.WriteLine(result);
            speechRecognizer.StartProcessing();
            Debug.WriteLine(result);
        }

        public async Task InitialzeSpeechRecognizer()
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

        public void StartSpeechRecognizerProcessing()
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

        public void StopSpeechRecognizerProcessing()
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

        private void ListProcess()
        {
            page.NavigationService.Navigate(new Uri("/ListSongPage.xaml", UriKind.RelativeOrAbsolute));
            StopNativeRecorder();
            StopSpeechRecognizerProcessing();
        }

        private void SettingProcess()
        {
            page.NavigationService.Navigate(new Uri("/SettingPage.xaml", UriKind.Relative));
            StopNativeRecorder();
            StopSpeechRecognizerProcessing();
        }
        private void Main()
        {
            page.NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.RelativeOrAbsolute));
            if (isOtherPage == true)
            {
                StopNativeRecorder();
                StopSpeechRecognizerProcessing();
            }
        }

        private void PreviousProcess()
        {
            mmRecord.PlayPrevious();

            if (page.NavigationService.Navigate(new Uri("/MainPage.xaml?Refresh=true", UriKind.Relative)))
            {
                StopNativeRecorder();
                StopSpeechRecognizerProcessing();
            }
        }

        private void NextProcess()
        {
            mmRecord.PlayNext();
            mmRecord.FileWriter(MusicManager._NowPlay);
        }

        private void PlayOrPauseProcess()
        {
            mmRecord.PlayOrPause();
        }

        private void StopProcess()
        {
            mmRecord.Stop();
        }
        void speechRecognizer_resultFound(string result)
        {
            
            Debug.WriteLine("result found: {0}", result);
            //StateMessageBlock.Text = string.Format("found: {0}", result);
            switch (result)
            {
                case "play":
                    PlayOrPauseProcess();
                    break;
                case "pause":
                    PlayOrPauseProcess();
                    break;
                case "stop":
                    Main(); // chuyen ve ham main
                    //StopProcess();
                    break;
                case "next":
                    NextProcess();
                    break;
                case "previous":
                    PreviousProcess();
                    break;
                case "list":
                    if (isAvailable == false) // truong hop dang o list, ma noi list tiep, no se stop han, vi khong navigate lai, nen can kiem tra
                    ListProcess();
                    break;
                //case "option":
                //    SettingProcess();
                //    break;
                default:
                    break;
            }
        }

        public void InitializeAudioRecorder()
        {
            AudioContainer.AudioRecorder = new WasapiAudioRecorder();
            audioRecorder = AudioContainer.AudioRecorder;
            audioRecorder.BufferReady += audioRecorder_BufferReady;

            Debug.WriteLine("AudioRecorder Initialized");
        }

        public void StartNativeRecorder()
        {
            audioRecorder.StartRecording();
        }

        public void StopNativeRecorder()
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
              //StateMessageBlock.Text = "all stoped because of error";
            }
        }
    }
}

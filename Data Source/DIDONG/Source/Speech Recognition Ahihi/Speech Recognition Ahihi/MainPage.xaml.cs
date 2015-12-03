using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Windows.Media.SpeechRecognition;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.ApplicationModel.Activation;//IActivatedEventArgs, ActivationKind
using Windows.Media.SpeechSynthesis;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace Speech_Recognition_Ahihi
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        SpeechRecognizer speechRecog = new SpeechRecognizer();

        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        private void btnTTS_Click(object sender, RoutedEventArgs e)
        {
            SpeakText(audioPlayer, txtInfo.Text);
        }

        private async void SpeakText(MediaElement audioPlayer, string TTS)
        {
            SpeechSynthesizer ttssynthesizer = new SpeechSynthesizer();

            //Set the Voice/Speaker
            using (var Speaker = new SpeechSynthesizer())
            {
                Speaker.Voice = (SpeechSynthesizer.AllVoices.First(x => x.Gender == VoiceGender.Male));
                ttssynthesizer.Voice = Speaker.Voice;
            }

            SpeechSynthesisStream ttsStream = await ttssynthesizer.SynthesizeTextToStreamAsync(TTS);

            audioPlayer.SetSource(ttsStream, "");
        }

        private async void btnSTT_Click(object sender, RoutedEventArgs e)
        {
            // Compile the dictation grammar
            await speechRecog.CompileConstraintsAsync();

            // Start Recognition
            SpeechRecognitionResult speechRecognitionResult = await this.speechRecog.RecognizeWithUIAsync();

            // Show Output
            var sttDialog = new Windows.UI.Popups.MessageDialog(speechRecognitionResult.Text, "Heard you said...");
            await sttDialog.ShowAsync();
        }
    }
}

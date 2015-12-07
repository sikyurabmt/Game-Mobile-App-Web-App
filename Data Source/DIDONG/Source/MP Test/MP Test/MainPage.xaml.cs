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
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.Media.Playback;
    

namespace MP_Test
{
    public sealed partial class MainPage : Page
    {
        public StorageFolder folder = KnownFolders.MusicLibrary;
        public static List<StorageFile> musicList = new List<StorageFile>();
        private MediaPlayer mediaPlayer;
        static String[] tracks = { "ms-appx:///Assets/Media/Ring01.wma", 
                                   "ms-appx:///Assets/Media/Ring02.wma",
                                   "ms-appx:///Assets/Media/Ring03.wma"};
        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            initValue();
            mediaPlayer = BackgroundMediaPlayer.Current;
        }
        public async void initValue()
        {
            musicList.Clear();
            await getFiles(musicList, folder);
        }
        public async Task getFiles(List<StorageFile> list, StorageFolder parent)
        {
            foreach (var item in await parent.GetFilesAsync())
            {
                list.Add(item);
            }
            foreach (var item in await parent.GetFoldersAsync())
                await getFiles(list, item);
        }

        private void StartTrackAt(int i)
        {
            BackgroundMediaPlayer.Current.SetFileSource(musicList[i]); 
            
        }
        private void StartTrackAt2(int id)
        {
            string source = tracks[id];
            mediaPlayer.AutoPlay = false;
            mediaPlayer.SetUriSource(new Uri(source));
            //BackgroundMediaPlayer.Current.SetUriSource(new Uri(source));
            mediaPlayer.Play();
            tblTitle.Text = source;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StartTrackAt2(2);
        }
    }
}

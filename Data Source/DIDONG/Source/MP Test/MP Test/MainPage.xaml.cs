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
        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            initValue();
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
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StartTrackAt(0);
        }
    }
}

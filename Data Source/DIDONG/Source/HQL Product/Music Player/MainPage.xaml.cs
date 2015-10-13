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

using Windows.Media;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.Activation;
using Windows.Storage.AccessCache;
using Windows.Storage.Streams;
using Windows.Storage.FileProperties;
using System.Threading.Tasks;
using Windows.Storage.Search;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace Music_Player
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        CoreApplicationView view;

        public readonly List<StorageFile> list = new List<StorageFile>();

        public MainPage()
        {
            view = CoreApplication.GetCurrentView();
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }
        public void getFolder()
        {
            FileOpenPicker filePicker = new FileOpenPicker();
            filePicker.SuggestedStartLocation = PickerLocationId.MusicLibrary;
            filePicker.ViewMode = PickerViewMode.List;
            filePicker.FileTypeFilter.Clear();
            filePicker.FileTypeFilter.Add(".mp3");
            filePicker.PickSingleFileAndContinue();
            view.Activated += viewActivated;
            //FolderPicker folderPicker = new FolderPicker();
            //folderPicker.ViewMode = PickerViewMode.Thumbnail;
            //folderPicker.FileTypeFilter.Add(".mp3");
            //folderPicker.PickFolderAndContinue();
        }
        private async void viewActivated(CoreApplicationView sender, IActivatedEventArgs args1)
        {
            //FolderPickerContinuationEventArgs args = args1 as FolderPickerContinuationEventArgs;
            //if (args!=null)
            //{
            //    StorageFolder folder = args.Folder;
            //    lvShow.Items.Add(folder.Name);
            //}
            FileOpenPickerContinuationEventArgs args = args1 as FileOpenPickerContinuationEventArgs;

            if (args != null)
            {
                if (args.Files.Count == 0) return;
                view.Activated -= viewActivated;
                StorageFile storageFile = args.Files[0];
                if (storageFile.Name.EndsWith("mp3"))
                {
                    var fileStream = await storageFile.OpenAsync(Windows.Storage.FileAccessMode.Read);
                    mediaShow.SetSource(fileStream, storageFile.ContentType);
                }
                //lvShow.Items.Add(storageFile.Path);
                //string a = storageFile.Path;
                //string b = storageFile.Name;
                //sourcePath = a.Substring(0, a.Length - b.Length);
                //lvShow.Items.Add(sourcePath);
                //var installFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
                //var resourcesFolder = await installFolder.GetFolderAsync(sourcePath);
                //foreach (var item in await resourcesFolder.GetFilesAsync())
                //{
                //    list.Add(item);
                //    lvShow.Items.Add(item.Name);
                //}
            }
        }

        private async Task RetriveFilesInFolders(List<StorageFile> list, StorageFolder parent)
        {
            foreach (var item in await parent.GetFilesAsync())
            {
                list.Add(item);
            }
            foreach (var item in await parent.GetFoldersAsync()) await RetriveFilesInFolders(list, item);
        }
        public async void getList(List<StorageFile> list) 
        {
            StorageFolder folder = KnownFolders.MusicLibrary;
            List<StorageFile> listOfFiles = new List<StorageFile>();
            await RetriveFilesInFolders(listOfFiles, folder);
            // as a result of above code I have a List of 5 files that are in Music Library
            //List<IStorageItem> filesFolders = (await folder.GetItemsAsync()).ToList();
            //List<StorageFile> items = (await folder.GetFilesAsync(CommonFileQuery.OrderByName)).ToList();
            lvShow.Items.Add(listOfFiles.Count);
            Random rNum = new Random();
            int n = rNum.Next(0, listOfFiles.Count-1);
            var audioFile = await folder.GetFileAsync(listOfFiles[n].Name);
            MusicProperties musicProperties = await audioFile.Properties.GetMusicPropertiesAsync();
            lvShow.Items.Clear();
            lvShow.Items.Add(musicProperties.Title);
            lvShow.Items.Add(musicProperties.Artist);
            var stream = await audioFile.OpenAsync(Windows.Storage.FileAccessMode.Read);
            mediaShow.SetSource(stream, audioFile.ContentType);
            mediaShow.Play();
        }

        public async void getList(List<StorageFile> list, int a)
        {
            var installFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            var resourcesFolder = await installFolder.GetFolderAsync("Resources");
            foreach (var item in await resourcesFolder.GetFilesAsync())
            {
                list.Add(item);
                lvShow.Items.Add(item.Name);
            }
            Random rNum = new Random();
            int n = rNum.Next(0, 2);
            var audioFile = await resourcesFolder.GetFileAsync(list[n].Name);
            MusicProperties musicProperties = await audioFile.Properties.GetMusicPropertiesAsync();
            lvShow.Items.Add(list[n].Name);
            lvShow.Items.Add(musicProperties.Album);
            var stream = await audioFile.OpenAsync(Windows.Storage.FileAccessMode.Read);
            mediaShow.SetSource(stream, audioFile.ContentType);
            mediaShow.Play();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //lvShow.Items.Add();
            //mediaShow.Play();
            getList(list);
            //getFolder();
        }
    }
}

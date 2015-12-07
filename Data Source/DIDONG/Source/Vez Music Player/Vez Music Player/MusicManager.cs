using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.Storage;
using Windows.Storage.Streams;
using Windows.Storage.FileProperties;

namespace Vez_Music_Player
{
    public class MusicManager
    {
        public StorageFolder folder = KnownFolders.MusicLibrary;
        public static List<StorageFile> musicList = new List<StorageFile>();
        public static int STT;
        public static MusicProperties musicProperties;
        public static Windows.Storage.Streams.IRandomAccessStream stream;
        public static MediaState state;
        public static NumOfLoad nof;
        public static Playback pb;
        public static Repeat rp;

        public enum MediaState
        {
            PLAY,
            PAUSE,
            STOP
        }
        public enum Playback
        {
            ORDER,
            RANDOM
        }

        public enum Repeat
        {
            ONE,
            ALL,
            NO
        }

        public enum NumOfLoad
        {
            FIRST,
            SECOND
        }
        public async void initValue()
        {
            musicList.Clear();
            await getFiles(musicList, folder);
            STT = 0;
            musicProperties = null;
            stream = null;
            state = MediaState.STOP;
            pb = Playback.ORDER;
            rp = Repeat.ALL;
            nof = NumOfLoad.FIRST;
        }

        public async Task getFiles(List<StorageFile> list, StorageFolder parent)
        {   //get all files of type folder search use Recursion
            foreach (var item in await parent.GetFilesAsync())
            {
                list.Add(item);
            }
            foreach (var item in await parent.GetFoldersAsync())
                await getFiles(list, item);
        }

        public async void getProperties_accessStream(int nOfSTT)
        {
            var audioFile = await folder.GetFileAsync(musicList[nOfSTT].Name);
            musicProperties = await audioFile.Properties.GetMusicPropertiesAsync();
            stream = await audioFile.OpenAsync(Windows.Storage.FileAccessMode.Read);
        }
        public void getNextNumber()
        {
            if (pb == Playback.RANDOM)
            {
                Random rNum = new Random();
                STT = rNum.Next(0, MusicManager.musicList.Count - 1);
                return;
            }
            if (pb == Playback.ORDER || rp == Repeat.ALL)
            {
                if (STT == musicList.Count - 1)
                {
                    STT = 0;
                }
                else
                {
                    STT++;
                }
                return;
            }
            if (rp == Repeat.ONE)
            {
                return;
            }
        }

        public void getPrevNumber()
        {
            if (pb == Playback.ORDER)
            {
                if (STT == 0)
                {
                    STT = musicList.Count - 1;
                }
                else
                {
                    STT--;
                }
                return;
            }
            if (pb == Playback.RANDOM)
            {
                Random rNum = new Random();
                STT = rNum.Next(0, MusicManager.musicList.Count - 1);
                return;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.Storage;
using Windows.Storage.FileProperties;

namespace Music_Player
{
    public class MusicManager
    {
        public StorageFolder folder = KnownFolders.MusicLibrary;   //type folder
        public static List<StorageFile> musicList = new List<StorageFile>();
        public MusicProperties musicProperties;
        public static int STT = 0;
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
        public enum NumOfLoad
        {
            FIRST,
            SECOND
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

        public async void initList()
        {
            musicList.Clear();
            await getFiles(musicList, folder);
            state = MediaState.STOP;
            nof = NumOfLoad.FIRST;
            pb = Playback.ORDER;
            rp = Repeat.ALL;
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

        public void getNextNumber()
        {
            if (rp == Repeat.ONE)
            {
                return;
            }
            if (pb == Playback.RANDOM)
            {
                Random rNum = new Random();
                STT = rNum.Next(0, MusicManager.musicList.Count - 1);
                return;
            }
            if (pb == Playback.ORDER && (rp == Repeat.ALL || rp ==Repeat.NO))
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

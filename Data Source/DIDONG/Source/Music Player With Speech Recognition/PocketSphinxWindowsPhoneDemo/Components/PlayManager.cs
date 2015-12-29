using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager
{
    public class PlayManager
    {
        public PlayManager()
        {

        }

        public static Playback _Playback;
        public static Repeat _Repeat;

        public enum Playback
        {
            ORDER,
            RANDOM
        }
        public enum Repeat
        {
            ALL,
            NO,
            ONE
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager
{
    public class SettingManager
    {
        private string _FilePath = "Files/setup.txt";
        //PlayManager _PlayManager = new PlayManager();
        public static Theme _Theme;
        public static Color _Color;
        private string sPlayback, sRepeat, sTheme, sColor;
        

        public enum Theme
        {
            WINTER,
            SPRING
        }

        public enum Color
        {
            BLUE,
            RED,
            YELLOW
        }

        //Constructor
        public SettingManager() 
        {

        }

        //Set init value from FileReader
        private void SetValue() 
        {
            //Playback
            switch (sPlayback)
            {
                case "ORDER":
                    PlayManager._Playback = PlayManager.Playback.ORDER;
                    break;
                case "RANDOM":
                    PlayManager._Playback = PlayManager.Playback.RANDOM;
                    break;
                default:
                    break;
            }
            //Repeat
            switch (sRepeat)
            {
                case "ALL":
                    PlayManager._Repeat = PlayManager.Repeat.ALL;
                    break;
                case "NO":
                    PlayManager._Repeat = PlayManager.Repeat.NO;
                    break;
                case "ONE":
                    PlayManager._Repeat = PlayManager.Repeat.ONE;
                    break;
                default:
                    break;
            }
            //Theme
            switch (sTheme)
            {
                case "WINTER":
                    _Theme = SettingManager.Theme.WINTER;
                    break;
                case "SPRING":
                    _Theme = SettingManager.Theme.SPRING;
                    break;
                default:
                    break;
            }
            //Color
            switch (sColor)
            {
                case "BLUE":
                    _Color = SettingManager.Color.BLUE; 
                    break;
                case "RED":
                    _Color = SettingManager.Color.RED;
                    break;
                case "YELLOW":
                    _Color = SettingManager.Color.YELLOW;
                    break;
                default:
                    break;
            }
        }

        public void FileReader()
        {
            //Content File
            string Content = "";
            //Get content from file
            using (StreamReader reader = new StreamReader(_FilePath))
            {
                Content = reader.ReadToEnd();
            }
            //Check content
            if (Content!="")
            {
                string ContentCopy = Content;
                //Get value Color
                int valueToChar = ContentCopy.LastIndexOf("-");
                sColor = ContentCopy.Substring(valueToChar + 1);
                ContentCopy = ContentCopy.Substring(0, valueToChar);
                //Get value Theme
                valueToChar = ContentCopy.LastIndexOf("-");
                sTheme = ContentCopy.Substring(valueToChar + 1);
                ContentCopy = ContentCopy.Substring(0, valueToChar);
                //Get value Repeat
                valueToChar = ContentCopy.LastIndexOf("-");
                sRepeat = ContentCopy.Substring(valueToChar + 1);
                ContentCopy = ContentCopy.Substring(0, valueToChar);
                //Get value Playback
                sPlayback = ContentCopy;
            }
            //
            SetValue();
        }

        public void FileWriter(string Playback, string Repeat, string Theme, string Color)
        {
            //Concat string
            string Content = Playback + "-" + Repeat + "-" + Theme + "-" + Color;
            //Write content to file
            using (StreamWriter writer = new StreamWriter(_FilePath))
            {
                writer.Write(Content);
            }
            SetValue();
        }
    }
}

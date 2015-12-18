using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Media;


namespace File_Manager
{
    public class MusicManager
    {
        public static int _NowPlay;
        private MediaLibrary _MediaLibrary = new MediaLibrary();
        private SongCollection _SongCollection;
        private int _MaxSong;
        public MusicManager() 
        {
            _SongCollection = _MediaLibrary.Songs;
            _MaxSong = _SongCollection.Count;
            _NowPlay = 0;
        }

        private int Random(int Max)
        {
            if (Max > 0)
            {
                Random _Random = new Random();
                return _Random.Next(0, Max);
            }
            return -1;
        }

        private int GetAutoNextSong()
        {
            int NextPlay = -1;
            switch (PlayManager._Repeat)
            {
                case PlayManager.Repeat.ALL:
                    switch (PlayManager._Playback)
                    {
                        case PlayManager.Playback.ORDER:
                            if (_NowPlay == _MaxSong - 1)
                            {
                                NextPlay = 0;
                            }
                            else
                            {
                                NextPlay = _NowPlay + 1;
                            }
                            break;
                        case PlayManager.Playback.RANDOM:
                            NextPlay = Random(_MaxSong - 1);
                            break;
                        default:
                            break;
                    }
                    break;
                case PlayManager.Repeat.NO:
                    switch (PlayManager._Playback)
                    {
                        case PlayManager.Playback.ORDER:
                            if (_NowPlay == _MaxSong - 1)
                            {
                                NextPlay = -1;
                            }
                            else
                            {
                                NextPlay = _NowPlay + 1;
                            }
                            break;
                        case PlayManager.Playback.RANDOM:
                            NextPlay = Random(_MaxSong - 1);
                            break;
                        default:
                            break;
                    }
                    break;
                case PlayManager.Repeat.ONE:
                    NextPlay = _NowPlay;
                    break;
                default:
                    break;
            }
            _NowPlay = NextPlay;
            return NextPlay;
        }

        private int GetNextSong()
        {
            int NextPlay = -1;
            switch (PlayManager._Playback)
            {
                case PlayManager.Playback.ORDER:
                    if (_NowPlay == _MaxSong - 1)
                    {
                        NextPlay = 0;
                    }
                    else
                    {
                        NextPlay = _NowPlay + 1;
                    }
                    break;
                case PlayManager.Playback.RANDOM:
                    NextPlay = Random(_MaxSong - 1);
                    break;
                default:
                    break;
            }
            _NowPlay = NextPlay;
            return NextPlay;
        }

        private int GetPreviousSong()
        {
            int PreviousPlay = -1;
            switch (PlayManager._Playback)
            {
                case PlayManager.Playback.ORDER:
                    if (_NowPlay == 0)
                    {
                        PreviousPlay = _MaxSong - 1;
                    }
                    else
                    {
                        PreviousPlay = _NowPlay - 1;
                    }
                    break;
                case PlayManager.Playback.RANDOM:
                    PreviousPlay = Random(_MaxSong - 1);
                    break;
                default:
                    break;
            }
            _NowPlay = PreviousPlay;
            return PreviousPlay;
        }

        public void Play(int Number)
        {
            MediaPlayer.Play(_SongCollection, Number);
        }

        public void AutoNext()
        {
            if (_NowPlay != -1)
            {
                Play(GetAutoNextSong());
            }
            else
            {
                Stop();
                //if (IsPlaying())
                //{
                //    Stop();
                //}
            }
        }

        public void PlayNext()
        {
            if (MediaPlayer.State == MediaState.Playing)
            {
                Play(GetNextSong());
            }
            else
            {
                GetNextSong();
            }
        }

        public void PlayPrevious()
        {
            if (MediaPlayer.State == MediaState.Playing)
            {
                Play(GetPreviousSong());
            }
            else
            {
                GetPreviousSong();
            }
        }

        public void PlayOrPause()
        {
            switch (MediaPlayer.State)
            {
                case MediaState.Paused:
                    MediaPlayer.Resume();
                    break;
                case MediaState.Playing:
                    MediaPlayer.Pause();
                    break;
                case MediaState.Stopped:
                    Play(_NowPlay);
                    break;
                default:
                    break;
            }
        }

        public void Stop()
        {
            MediaPlayer.Stop();
        }

        public string GetTitle()
        {
            if (_NowPlay!=-1)
            {
                return _SongCollection[_NowPlay].Name;
            }
            return "";
        }

        public string GetArtist()
        {
            if (_NowPlay != -1)
            {
                return _SongCollection[_NowPlay].Artist.Name;
            }
            return "";
        }

        public string GetAlbum()
        {
            if (_NowPlay != -1)
            {
                return _SongCollection[_NowPlay].Album.Name;
            }
            return "";
        }

        public bool IsPlaying()
        {
            if (MediaPlayer.State == MediaState.Playing)
            {
                return true;
            }
            return false;
        }

        public bool IsPaused()
        {
            if (MediaPlayer.State == MediaState.Paused)
            {
                return true;
            }
            return false;
        }

        public bool IsStopped()
        {
            if (MediaPlayer.State == MediaState.Stopped)
            {
                return true;
            }
            return false;
        }

        public double GetTotalSecondsOfSong()
        {
            if (_NowPlay != -1)
            {
                return _SongCollection[_NowPlay].Duration.TotalSeconds;
            }
            return -1;
        }

        public double GetNowSecondsOfSong()
        {
            if (_NowPlay != -1)
            {
                return MediaPlayer.PlayPosition.TotalSeconds;
            }
            return -1;
        }

        public TimeSpan GetTotalTimeSpanOfSong()
        {
            return _SongCollection[_NowPlay].Duration;
        }

        public TimeSpan GetNowTimeSpanOfSong()
        {
            return MediaPlayer.PlayPosition;
        }

        public bool SongCollectionIsAvailable()
        {
            if (_SongCollection.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
}

using System;
using Windows.ApplicationModel.Background;
using Windows.Media;
using Windows.Media.Playback;

namespace BackgroundAudioComponent
{
    public sealed class BackgroundAudioTask : IBackgroundTask
    {
        private SystemMediaTransportControls systemmediatransportcontrol;
        private BackgroundTaskDeferral deferral;

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            systemmediatransportcontrol = SystemMediaTransportControls.GetForCurrentView();
            systemmediatransportcontrol.ButtonPressed += SystemControlsButtonPressed;
            systemmediatransportcontrol.IsEnabled = true;
            systemmediatransportcontrol.IsPauseEnabled = true;
            systemmediatransportcontrol.IsStopEnabled = true;
            systemmediatransportcontrol.IsPlayEnabled = true;

            BackgroundMediaPlayer.Current.CurrentStateChanged -= BackgroundMediaPlayerCurrentStateChanged;
            BackgroundMediaPlayer.MessageReceivedFromForeground -= BackgroundMediaPlayerOnMessageReceivedFromForeground;
            BackgroundMediaPlayer.Current.CurrentStateChanged += BackgroundMediaPlayerCurrentStateChanged;
            BackgroundMediaPlayer.MessageReceivedFromForeground += BackgroundMediaPlayerOnMessageReceivedFromForeground;

            deferral = taskInstance.GetDeferral();
        }

        private void BackgroundMediaPlayerOnMessageReceivedFromForeground(object sender, MediaPlayerDataReceivedEventArgs e)
        {
            systemmediatransportcontrol.DisplayUpdater.Type = MediaPlaybackType.Music;
            systemmediatransportcontrol.DisplayUpdater.MusicProperties.Title = e.Data["Title"].ToString();
            systemmediatransportcontrol.DisplayUpdater.MusicProperties.Artist = e.Data["Artist"].ToString();
            systemmediatransportcontrol.DisplayUpdater.Update();
        }

        private void BackgroundMediaPlayerCurrentStateChanged(MediaPlayer sender, object args)
        {
            if (sender.CurrentState == MediaPlayerState.Playing)
            {
                systemmediatransportcontrol.PlaybackStatus = MediaPlaybackStatus.Playing;
            }
            else if (sender.CurrentState == MediaPlayerState.Paused)
            {
                systemmediatransportcontrol.PlaybackStatus = MediaPlaybackStatus.Paused;
            }
        }

        private static void SystemControlsButtonPressed(SystemMediaTransportControls sender, SystemMediaTransportControlsButtonPressedEventArgs args)
        {
            switch (args.Button)
            {
                case SystemMediaTransportControlsButton.Play:
                    BackgroundMediaPlayer.Current.Play();
                    break;
                case SystemMediaTransportControlsButton.Pause:
                    BackgroundMediaPlayer.Current.Pause();
                    break;
                case SystemMediaTransportControlsButton.Stop:
                    BackgroundMediaPlayer.Current.Pause();
                    BackgroundMediaPlayer.Current.Position = TimeSpan.FromSeconds(0);
                    break;
            }
        }
    }
}

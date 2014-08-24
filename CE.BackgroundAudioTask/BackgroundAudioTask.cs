using System;
using System.Diagnostics;
using Windows.ApplicationModel.Background;
using Windows.Foundation.Collections;
using Windows.Media;
using Windows.Media.Playback;

namespace CE.BackgroundAudioTask
{
    public sealed class BackgroundAudioTask : IBackgroundTask
    {
        private BackgroundTaskDeferral deferral;
        private SystemMediaTransportControls systemmediatransportcontrol;

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            systemmediatransportcontrol = SystemMediaTransportControls.GetForCurrentView();
            systemmediatransportcontrol.ButtonPressed += systemmediatransportcontrol_ButtonPressed;
            systemmediatransportcontrol.IsEnabled = true;

            BackgroundMediaPlayer.MessageReceivedFromForeground += MessageReceivedFromForeground;
            BackgroundMediaPlayer.Current.CurrentStateChanged += BackgroundMediaPlayerCurrentStateChanged;

            // Associate a cancellation and completed handlers with the background task.
            taskInstance.Canceled += OnCanceled;
            taskInstance.Task.Completed += Taskcompleted;

            deferral = taskInstance.GetDeferral();
        }

        void systemmediatransportcontrol_ButtonPressed(SystemMediaTransportControls sender, SystemMediaTransportControlsButtonPressedEventArgs args)
        {
            switch (args.Button)
            {
                case SystemMediaTransportControlsButton.Play:
                    BackgroundMediaPlayer.Current.Play();
                    break;
                case SystemMediaTransportControlsButton.Pause:
                    BackgroundMediaPlayer.Current.Pause();
                    break;
            }
        }

        private void Taskcompleted(BackgroundTaskRegistration sender, BackgroundTaskCompletedEventArgs args)
        {
            Debug.WriteLine("MyBackgroundAudioTask " + sender.TaskId + " Completed...");
            deferral.Complete();
        }

        private void OnCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            systemmediatransportcontrol.ButtonPressed -= systemmediatransportcontrol_ButtonPressed;
            BackgroundMediaPlayer.Shutdown();
            deferral.Complete();
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

        private void MessageReceivedFromForeground(object sender, MediaPlayerDataReceivedEventArgs e)
        {
            ValueSet valueSet = e.Data;
            foreach (string key in valueSet.Keys)
            {
                switch (key)
                {
                    case "Play":
                        Debug.WriteLine("Starting Playback");
                        string[] fileInfo = (string[])valueSet[key];
                        Play(fileInfo);
                        break;
                    case "SetPosition":
                        Debug.WriteLine("Set position:");
                        //
                        var timespanValue = e.Data[key].ToString();
                        Debug.WriteLine(timespanValue);

                        var position = TimeSpan.ParseExact(timespanValue, "c", null);
                        BackgroundMediaPlayer.Current.Position = position;
                        break;

                }
            }
        }

        private void Play(string[] toPlay)
        {
            MediaPlayer mediaPlayer = BackgroundMediaPlayer.Current;
            mediaPlayer.AutoPlay = true;
            //mediaPlayer.IsLoopingEnabled = true;
            mediaPlayer.SetUriSource(new Uri(toPlay[1]));

            //Update the universal volume control
            systemmediatransportcontrol.IsPauseEnabled = true;
            systemmediatransportcontrol.IsPlayEnabled = true;
            systemmediatransportcontrol.DisplayUpdater.Type = MediaPlaybackType.Music;
            systemmediatransportcontrol.DisplayUpdater.MusicProperties.Title = toPlay[0];
            systemmediatransportcontrol.DisplayUpdater.Update();
        }

    }
}

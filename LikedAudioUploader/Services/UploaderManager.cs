using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using VkNet;
using VkNet.Enums;
using VkNet.Model.Attachments;
using VkNet.Model.RequestParams;
using Windows.UI;
using Windows.UI.Notifications;
using VkNet.Exception;
using LikedAudioUploader.Services;
using LikedAudioUploader.Classes;

namespace LikedAudioUploader.Services
{
    public class UploadManager
    {
        public static VkApi Api { get; private set; }

        static UploadManager()
        {
            Api = new VkApi();
            Api.Authorize(AuthorizationManager.Instance.AccessToken);
        }
        public void UploadAudio(LocalAudio a)
        {
            var trackTitle = a.Artist + " - " + a.Title;
            try
            {
                NotificationManager.ShowNotification(trackTitle, "Upload stated");
                AudioEditParams saveParams = UploadAudio(a.FileName);
                Api.Audio.Edit(saveParams);
                NotificationManager.ShowNotification(trackTitle, "Audio Uploaded!");
            }
            catch (VkApiException)
            {
                NotificationManager.ShowNotification("Failed to upload.\nVK error.", trackTitle);
            }
            catch(WebException)
            {
                NotificationManager.ShowNotification("Failed to upload.\nProblems with connection.", trackTitle);
            }
        }

        private static AudioEditParams UploadAudio(string p)
        {
            string jsonResponse = UploadFile(p, Api.Audio.GetUploadServer().AbsoluteUri);
            Audio audio = Api.Audio.Save(jsonResponse);

            return new AudioEditParams
            {
                Artist = audio.Artist,
                AudioId = audio.Id.Value,
                NoSearch = true,
                Title = audio.Title,
                OwnerId = audio.OwnerId.Value,
                GenreId = AudioGenre.Rock,
                Text = "Uploaded by Liked Audio Uploader \n @alikhil"
            };
        }

        private static string UploadFile(string fileName, string url)
        {
            var wClient = new  MyWebClient();
            byte[] answer = wClient.UploadFile(url, "POST", fileName);
            string result = Encoding.ASCII.GetString(answer);
            return result;
        }
    }
}

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
        public void UploadAudio(LocalAudio a, Action onUpload, Action<string> onFail)
        {
            try
            {
                AudioEditParams saveParams = UploadAudio(a.FileName);
                Api.Audio.Edit(saveParams);
                onUpload();
            }
            catch (VkApiException ex)
            {
                onFail("VK API error: " + ex.Message);
            }
            catch(WebException ex)
            {
                onFail("Uploading error: " + ex.Message);
            }
        }

        private static AudioEditParams UploadAudio(string p)
        {
            ServicePointManager.DefaultConnectionLimit = 1000;
            ServicePointManager.SetTcpKeepAlive(false, 0, 0);
            Debug.WriteLine("Uploading " + p);
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
            WebClient wClient = new WebClient();
            byte[] answer = wClient.UploadFile(url, "POST", fileName);
            string result = Encoding.ASCII.GetString(answer);
            return result;
        }
    }
}

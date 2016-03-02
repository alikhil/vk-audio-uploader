using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model.Attachments;
using VkNet.Utils;
using VkNet.Exception;
using VkNet.Model.RequestParams;
using VkNet.Enums;

namespace AudioUploader
{
    class Program
    {
        static VkApi Api = new VkApi();

        static void Main(string[] args)
        {
            
            if (args.Length == 0)
            {
                Console.WriteLine("There are no arguments!");
                return;
            }
            #if DEBUG
            foreach (var s in args)
                Console.WriteLine(s);
            #endif
            string accessToken, user, password, path;
            var dictionary = ParseArgs(args);

            dictionary.TryGetValue("u", out user);
            dictionary.TryGetValue("a", out accessToken);
            dictionary.TryGetValue("p", out password);
            dictionary.TryGetValue("P", out path);

            var authParams = new ApiAuthParams
            {
                ApplicationId = Constants.AppId,
                Login = user,
                Password = password,
                Settings = Constants.AppSettings
            };
            if (!string.IsNullOrEmpty(accessToken))
                Api.Authorize(accessToken);
            if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(password))
                Api.Authorize(authParams);
            if (Api.IsAuthorized)
            {
                Console.WriteLine("Authorized");
                UploadAudiosOnPath(path);
            }
            Console.WriteLine("Finish");
        }

        private static void UploadAudiosOnPath(string path)
        {
            var paths = path.Split(';');
            foreach (var p in paths)
            {
                UploadAllInPath(p);
            }
        }

        private static void UploadAllInPath(string p)
        {
            try
            {
                if (File.Exists(p) && p.EndsWith(".mp3"))
                {
                    UploadAndSaveFile(p);
                }
                else if (Directory.Exists(p))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(p);
                    foreach (var file in directoryInfo.EnumerateFiles())
                        if(file.Extension.Equals(".mp3"))
                        UploadAndSaveFile(file.FullName);

                    foreach (var dir in directoryInfo.EnumerateDirectories())
                        UploadAllInPath(dir.FullName);
                }
            }
            catch (VkApiException e)
            {
                Console.WriteLine("Can not upload " + p);
                Debug.WriteLine(e.Message);
            }
        }

        private static void UploadAndSaveFile(string p)
        {
            Console.WriteLine("Uploading " + p);
            string jsonResponse = UploadFile(p, Api.Audio.GetUploadServer().AbsoluteUri);
            Audio audio = Api.Audio.Save(jsonResponse);

            var editParams = new AudioEditParams
            {
                Artist = audio.Artist,
                AudioId = audio.Id.Value,
                NoSearch = true,
                Title = audio.Title,
                OwnerId = audio.OwnerId.Value,
                GenreId = AudioGenre.Rock,
                Text = "Uploaded by Liked Audio Uploader \n @alikhil"
            };

            Api.Audio.Edit(editParams);
            Console.WriteLine(p + " Uploaded");
        }

        private static string UploadFile(string fileName, string url)
        {
            WebClient wClient = new WebClient();

            byte[] answer = wClient.UploadFile(url, "POST", fileName);
            string result = Encoding.ASCII.GetString(answer);
            return result;
        }

        private static Dictionary<string,string> ParseArgs(string[] args)
        {
            if (args.Length % 2 != 0)
                throw new ArgumentException("Invalid input");
            var dict = new Dictionary<string, string>();

            for (int i = 0; i < args.Length; i++)
                if (args[i].StartsWith("-"))
                    dict[args[i].Substring(1)] = args[i + 1];
            return dict;
        }
    }
}

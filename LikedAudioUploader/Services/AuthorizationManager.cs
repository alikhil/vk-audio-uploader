using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet;

namespace LikedAudioUploader.Services
{
    class AuthorizationManager
    {
        private static AuthorizationManager instance;
        private static string fileName = "appdata.txt";
        public string AccessToken { get; private set; }
        private AuthorizationManager()
        {

        }
        public static AuthorizationManager Instance
        {
            get
            {
                instance = instance ?? new AuthorizationManager();
                return instance;
            }
        }

        public async Task<bool> IsAuthorized()
        {
            var token = await ReadDataFromStorage();
            if (token == null)
                return false;
            var api = new VkApi();
            api.Authorize(token);
            AccessToken = token;
            return api.IsAuthorized;
        }

        public async Task Authorize()
        {
            var authorizeForm = new AuthorizationForm();
            authorizeForm.ShowDialog();
            if (authorizeForm.AccessToken != null)
            {
                AccessToken = authorizeForm.AccessToken;
                WriteDataToStorage(AccessToken);
            }
            await Task.FromResult(0);
        }

        private async Task<string> ReadDataFromStorage()
        {
            var isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User |
                IsolatedStorageScope.Assembly |
                IsolatedStorageScope.Domain, null, null);
            if (isoStore.FileExists(fileName))
            {
                using (var isoStream = new IsolatedStorageFileStream(fileName, FileMode.Open, isoStore))
                {
                    using (var reader = new StreamReader(isoStream))
                    {
                        return await reader.ReadToEndAsync();
                    }
                }
            }
            return null;
        }

        private async void WriteDataToStorage(string data)
        {
            var isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User |
                IsolatedStorageScope.Assembly |
                IsolatedStorageScope.Domain, null, null);

           
            using (var isoStream = new IsolatedStorageFileStream(fileName, FileMode.OpenOrCreate, isoStore))
            {
                using (var writer = new StreamWriter(isoStream))
                {
                    await writer.WriteAsync(data);
                }
            }
            
        }
    }
}

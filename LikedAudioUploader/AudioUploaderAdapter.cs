using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using Windows.UI;
using Windows.UI.Notifications;

namespace LikedAudioUploader
{
    public class AudioUploaderAdapter
    {
        public void UploadAudio(Audio a, Action onUpload)
        {
            Process proc = new Process();
            ProcessStartInfo process = new ProcessStartInfo("AudioUploader.exe");
        
#if !DEBUG
            process.WindowStyle = ProcessWindowStyle.Hidden;
            process.CreateNoWindow = true;
#endif
            process.Arguments = String.Format("-a {0} -P \"{1}\"", Authorization.Instance.AccessToken , a.FileName);
            Process.Start(process).WaitForExit();
            onUpload();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LikedAudioUploader
{
    public class AudioProvider
    {
        public LocalAudio GetNowPlaying()
        {
            var foobars = Process.GetProcessesByName("foobar2000");
            if (foobars.Length > 0)
            {
                var windowTitle = foobars[0].MainWindowTitle;
                if (windowTitle.Contains("=>"))
                {
                    var data = windowTitle.Split(new string[] { "=>" }, StringSplitOptions.None);
                    if (data.Length != 3)
                        throw new FoobarNotConfiguredException();
                    var pathData = data[2].Split(new string[] { "&&" }, StringSplitOptions.None);
                    return new LocalAudio(data[0], data[1], pathData[0]);
                }
                else
                    throw new FoobarNotConfiguredException();
            }
            else
                throw new Exception("Foobar is not running now. \nStart it.");
        }
    }
}

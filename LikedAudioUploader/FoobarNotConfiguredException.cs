using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LikedAudioUploader
{
    class FoobarNotConfiguredException : Exception
    {
        public FoobarNotConfiguredException() 
            : base("Configure foobar to show on title %artist% => %title% => %path%")
        {
            
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LikedAudioUploader.Classes
{
    class FoobarNotConfiguredException : Exception
    {
        public FoobarNotConfiguredException() 
            : base("Configure foobar to show on title '[%album artist% ] => [%title%] => [%path%]&&'")
        {
            
        }
    }
}

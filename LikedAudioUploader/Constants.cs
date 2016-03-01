﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet;
using VkNet.Enums.Filters;

namespace LikedAudioUploader
{
    public static class Constants
    {
        public static ulong AppId = 31337;

        public static Settings AppSettings = Settings.Audio | Settings.Offline;
    }
}

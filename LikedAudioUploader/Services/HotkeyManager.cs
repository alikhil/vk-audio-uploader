using LikedAudioUploader.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LikedAudioUploader.Services
{
    public class HotkeyManager : IDisposable
    {
        
        private int Iterator = 0;

        private List<Hotkey> hotkeys;

        private HotkeyManager()
        {
            hotkeys = new List<Hotkey>();
        }

        private static HotkeyManager instance;
        public static HotkeyManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new HotkeyManager();
                return instance;
            }
        }

        public void AddHotKey(KeyModifier mod, Keys key, Action onPress, IntPtr handle)
        {
            var hotkey = new Hotkey(++Iterator, handle, onPress, mod, key);
            hotkeys.Add(hotkey);;
            hotkey.Register();
        }

        public void OnWndProc(Message m)
        {
            if (m.Msg == 0x0312)
            {
                Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);                 
                KeyModifier modifier = (KeyModifier)((int)m.LParam & 0xFFFF);
                int id = m.WParam.ToInt32();

                hotkeys.Find(h => h.Id == id).OnPress();
            }
        }

        public void Dispose()
        {
            foreach (var key in hotkeys)
                key.Dispose();
        }
    }
    public enum KeyModifier
    {
        None = 0,
        Alt = 1,
        Control = 2,
        Shift = 4,
        WinKey = 8
    }
}

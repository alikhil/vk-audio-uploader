using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LikedAudioUploader.Classes
{
    public class Hotkey : IDisposable
    {
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        public int Id { get; set; }

        public KeyModifier Modifier { get; set; }

        public Keys Key { get; set; }

        public IntPtr Handle { get; set; }

        public Action OnPress { get; set; }

        public Hotkey(int id, IntPtr handle, Action onPress, KeyModifier mod, Keys key)
        {
            Id = id;
            Handle = handle;
            OnPress = onPress;
            Modifier = mod;
            Key = key;
        }

        public void Register()
        {
            RegisterHotKey(Handle, Id, (int)Modifier, Key.GetHashCode());
        }

        public void Unregister()
        {
            UnregisterHotKey(Handle, Id);
        }

        public void Dispose()
        {
            Unregister();
        }
    }
}

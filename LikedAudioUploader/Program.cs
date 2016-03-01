using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LikedAudioUploader
{
    static class Program
    {
        /// <summary>
        /// 
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            bool created = false;
            var mutex = new Mutex(true, "LikedAudioUploader", out created);
            if (!created)
                MessageBox.Show("Audio Uploader is running now", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
           else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
        }
        
    }
}

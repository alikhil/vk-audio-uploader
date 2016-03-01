using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Collections;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using System.IO.IsolatedStorage;
using System.Threading;

namespace LikedAudioUploader
{
    public partial class MainForm : Form, IDisposable
    {
        private const String APP_ID = "LikedAudioUploder";
        public MainForm()
        {
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            this.Visible = false;
            InitializeComponent();
            HotkeyManager.Instance.AddHotKey(KeyModifier.None, Keys.F7, OnPressHotkey, this.Handle);
            this.FormClosed += Form1_FormClosed;
            Task.Run(() => Authorize());
        }

        private async void Authorize()
        {
            var authorized = await Authorization.Instance.IsAuthorized();
            if (!authorized)
                await Authorization.Instance.Authorize();
        }

        private void OnPressHotkey()
        {
            var uploader = new AudioUploaderAdapter();
            var provider = new AudioProvider();
            try
            {
                var audio = provider.GetNowPlaying();
                if (audio != null)
                {
                    Thread t = new Thread(() =>
                    {
                        uploader.UploadAudio(audio, () => ShowToast(audio));
                    });
                    t.Start();
                }
            }
            catch(FoobarNotConfiguredException ef)
            {
                MessageBox.Show(ef.Message, "Error");
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "Unknown error");
            }
           
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dispose();
        }


        public new void Dispose()
        {
            HotkeyManager.Instance.Dispose();
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            HotkeyManager.Instance.OnWndProc(m);
        }

        private static void ShowToast(Audio a)
        {
            var toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText03);

            // Fill in the text elements
            var stringElements = toastXml.GetElementsByTagName("text");
            stringElements[0].AppendChild(toastXml.CreateTextNode(a.Artist + " - " + a.Title));
            stringElements[1].AppendChild(toastXml.CreateTextNode("File uploaded!"));

            // Specify the absolute path to an image
            // Create the toast and attach event listeners
            ToastNotification toast = new ToastNotification(toastXml);

            // Show the toast. Be sure to specify the AppUserModelId on your application's shortcut!
            ToastNotificationManager.CreateToastNotifier(APP_ID).Show(toast);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}

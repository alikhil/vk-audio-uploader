﻿using System;
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
using LikedAudioUploader.Services;
using LikedAudioUploader.Providers;
using LikedAudioUploader.Classes;

namespace LikedAudioUploader
{
    public partial class MainForm : Form, IDisposable
    {
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
            var authorized = await AuthorizationManager.Instance.IsAuthorized();
            if (!authorized)
                await AuthorizationManager.Instance.Authorize();
        }

        private void OnPressHotkey()
        {
            var uploader = new UploadManager();
            var provider = new AudioProvider();
            try
            {
                var audio = provider.GetNowPlaying();
                if (audio != null)
                {
                    Thread t = new Thread(() =>
                    {
                        uploader.UploadAudio(audio);
                    });
                    t.Start();
                }
                
            }
            catch(FoobarNotConfiguredException ef)
            {
                MessageBox.Show(ef.Message, "Foobar not confugured...");
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "Something went wrong...");
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;

namespace LikedAudioUploader.Services
{
    public static class NotificationManager
    {
        private const string APP_ID = "LikedAudioUploder";

        public static void ShowNotification(string text, string caption)
        {
            
            var toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText03);

            var stringElements = toastXml.GetElementsByTagName("text");
            stringElements[0].AppendChild(toastXml.CreateTextNode(caption));
            stringElements[1].AppendChild(toastXml.CreateTextNode(text));

            ToastNotification toast = new ToastNotification(toastXml);

            ToastNotificationManager.CreateToastNotifier(APP_ID).Show(toast);
        }

    }
}

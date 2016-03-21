using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VkNet;

namespace LikedAudioUploader
{
    public partial class AuthorizationForm : Form
    {
        public string AccessToken { get; private set; }
        public AuthorizationForm()
        {
            InitializeComponent();
        }

        private async void AuthorizeBtn_Click(object sender, EventArgs e)
        {
            VkApi api = new VkApi();
            var p = new ApiAuthParams
            {
                Login = LoginTBox.Text,
                Password = PasswordTBox.Text,
                Settings = Constants.AppSettings,
                ApplicationId = Constants.AppId
            };
            try
            { 
                await api.AuthorizeAsync(p);
            }
            catch(Exception er)
            {

            }
            if (!api.IsAuthorized)
                StatusLabel.Text = "Invalid login or password";
            else
            {
                MessageBox.Show("Authorization success", "Status", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                AccessToken = api.AccessToken;
                Close();
            }
        }
    }
}

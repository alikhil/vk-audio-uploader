namespace LikedAudioUploader
{
    partial class AuthorizationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.AuthorizeBtn = new System.Windows.Forms.Button();
            this.LoginTBox = new System.Windows.Forms.TextBox();
            this.PasswordTBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // AuthorizeBtn
            // 
            this.AuthorizeBtn.Location = new System.Drawing.Point(65, 99);
            this.AuthorizeBtn.Name = "AuthorizeBtn";
            this.AuthorizeBtn.Size = new System.Drawing.Size(93, 27);
            this.AuthorizeBtn.TabIndex = 0;
            this.AuthorizeBtn.Text = "Login";
            this.AuthorizeBtn.UseVisualStyleBackColor = true;
            this.AuthorizeBtn.Click += new System.EventHandler(this.AuthorizeBtn_Click);
            // 
            // LoginTBox
            // 
            this.LoginTBox.Location = new System.Drawing.Point(23, 31);
            this.LoginTBox.Name = "LoginTBox";
            this.LoginTBox.Size = new System.Drawing.Size(180, 20);
            this.LoginTBox.TabIndex = 1;
            this.LoginTBox.Text = "batmanaw@mail.ru";
            // 
            // PasswordTBox
            // 
            this.PasswordTBox.Location = new System.Drawing.Point(23, 73);
            this.PasswordTBox.Name = "PasswordTBox";
            this.PasswordTBox.PasswordChar = '*';
            this.PasswordTBox.Size = new System.Drawing.Size(180, 20);
            this.PasswordTBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Login";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password";
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Location = new System.Drawing.Point(29, 129);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(0, 13);
            this.StatusLabel.TabIndex = 2;
            // 
            // AuthorizationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(233, 150);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PasswordTBox);
            this.Controls.Add(this.LoginTBox);
            this.Controls.Add(this.AuthorizeBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "AuthorizationForm";
            this.Text = "AuthorizationForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AuthorizeBtn;
        private System.Windows.Forms.TextBox LoginTBox;
        private System.Windows.Forms.TextBox PasswordTBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label StatusLabel;
    }
}
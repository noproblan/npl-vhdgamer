namespace VhdGamer.LegacyGui
{
    partial class LobbyForm
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
            this.channelUsersListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nicknameTextBox = new System.Windows.Forms.TextBox();
            this.saveNicknameButton = new System.Windows.Forms.Button();
            this.messageTextBox = new System.Windows.Forms.TextBox();
            this.sendMessageButton = new System.Windows.Forms.Button();
            this.tabControlChat = new System.Windows.Forms.TabControl();
            this.label2 = new System.Windows.Forms.Label();
            this.gamesListBox = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.usersComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // channelUsersListBox
            // 
            this.channelUsersListBox.FormattingEnabled = true;
            this.channelUsersListBox.Location = new System.Drawing.Point(537, 64);
            this.channelUsersListBox.Name = "channelUsersListBox";
            this.channelUsersListBox.Size = new System.Drawing.Size(150, 251);
            this.channelUsersListBox.TabIndex = 0;
            this.channelUsersListBox.SelectedIndexChanged += new System.EventHandler(this.usersListBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(537, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "My Nickname:";
            // 
            // nicknameTextBox
            // 
            this.nicknameTextBox.Location = new System.Drawing.Point(537, 25);
            this.nicknameTextBox.Name = "nicknameTextBox";
            this.nicknameTextBox.Size = new System.Drawing.Size(150, 20);
            this.nicknameTextBox.TabIndex = 3;
            // 
            // saveNicknameButton
            // 
            this.saveNicknameButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveNicknameButton.Location = new System.Drawing.Point(633, 5);
            this.saveNicknameButton.Name = "saveNicknameButton";
            this.saveNicknameButton.Size = new System.Drawing.Size(54, 20);
            this.saveNicknameButton.TabIndex = 4;
            this.saveNicknameButton.Text = "Save";
            this.saveNicknameButton.UseVisualStyleBackColor = true;
            this.saveNicknameButton.Click += new System.EventHandler(this.saveNicknameButton_Click);
            // 
            // messageTextBox
            // 
            this.messageTextBox.Location = new System.Drawing.Point(168, 295);
            this.messageTextBox.Name = "messageTextBox";
            this.messageTextBox.Size = new System.Drawing.Size(291, 20);
            this.messageTextBox.TabIndex = 6;
            // 
            // sendMessageButton
            // 
            this.sendMessageButton.Location = new System.Drawing.Point(465, 294);
            this.sendMessageButton.Name = "sendMessageButton";
            this.sendMessageButton.Size = new System.Drawing.Size(66, 21);
            this.sendMessageButton.TabIndex = 7;
            this.sendMessageButton.Text = "Send";
            this.sendMessageButton.UseVisualStyleBackColor = true;
            this.sendMessageButton.Click += new System.EventHandler(this.sendMessageButton_Click);
            // 
            // tabControlChat
            // 
            this.tabControlChat.Location = new System.Drawing.Point(168, 3);
            this.tabControlChat.Name = "tabControlChat";
            this.tabControlChat.SelectedIndex = 0;
            this.tabControlChat.Size = new System.Drawing.Size(363, 286);
            this.tabControlChat.TabIndex = 8;
            this.tabControlChat.SelectedIndexChanged += new System.EventHandler(this.tabControlChat_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Games in Library of:";
            // 
            // gamesListBox
            // 
            this.gamesListBox.FormattingEnabled = true;
            this.gamesListBox.Location = new System.Drawing.Point(12, 51);
            this.gamesListBox.Name = "gamesListBox";
            this.gamesListBox.Size = new System.Drawing.Size(150, 264);
            this.gamesListBox.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(537, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Users in Channel:";
            // 
            // usersComboBox
            // 
            this.usersComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.usersComboBox.FormattingEnabled = true;
            this.usersComboBox.Location = new System.Drawing.Point(12, 25);
            this.usersComboBox.MaxDropDownItems = 32;
            this.usersComboBox.Name = "usersComboBox";
            this.usersComboBox.Size = new System.Drawing.Size(150, 21);
            this.usersComboBox.Sorted = true;
            this.usersComboBox.TabIndex = 12;
            this.usersComboBox.SelectedIndexChanged += new System.EventHandler(this.usersComboBox_SelectedIndexChanged);
            // 
            // LobbyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 325);
            this.Controls.Add(this.usersComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.gamesListBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tabControlChat);
            this.Controls.Add(this.sendMessageButton);
            this.Controls.Add(this.messageTextBox);
            this.Controls.Add(this.saveNicknameButton);
            this.Controls.Add(this.nicknameTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.channelUsersListBox);
            this.Name = "LobbyForm";
            this.Text = "LAN Internal Chat (EXPERIMENTAL)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LobbyForm_FormClosing);
            this.Load += new System.EventHandler(this.LobbyForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox channelUsersListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nicknameTextBox;
        private System.Windows.Forms.Button saveNicknameButton;
        private System.Windows.Forms.TextBox messageTextBox;
        private System.Windows.Forms.Button sendMessageButton;
        private System.Windows.Forms.TabControl tabControlChat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox gamesListBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox usersComboBox;
    }
}
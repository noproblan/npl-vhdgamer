namespace VhdGamer.LegacyGui
{
    partial class DeleterForm
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
            this.gameList = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.deleteButton = new System.Windows.Forms.Button();
            this.cancelDeletionButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // gameList
            // 
            this.gameList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gameList.CheckOnClick = true;
            this.gameList.FormattingEnabled = true;
            this.gameList.Location = new System.Drawing.Point(12, 25);
            this.gameList.Name = "gameList";
            this.gameList.Size = new System.Drawing.Size(347, 334);
            this.gameList.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Games On Hard Disk:";
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(12, 365);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(134, 23);
            this.deleteButton.TabIndex = 9;
            this.deleteButton.Text = "Delete Selected";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // cancelDeletionButton
            // 
            this.cancelDeletionButton.Location = new System.Drawing.Point(217, 365);
            this.cancelDeletionButton.Name = "cancelDeletionButton";
            this.cancelDeletionButton.Size = new System.Drawing.Size(142, 23);
            this.cancelDeletionButton.TabIndex = 10;
            this.cancelDeletionButton.Text = "Exit";
            this.cancelDeletionButton.UseVisualStyleBackColor = true;
            this.cancelDeletionButton.Click += new System.EventHandler(this.cancelDeletionButton_Click);
            // 
            // DeleterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 394);
            this.Controls.Add(this.cancelDeletionButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gameList);
            this.Name = "DeleterForm";
            this.Text = "Clean Up";
            this.Load += new System.EventHandler(this.DeleterForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox gameList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button cancelDeletionButton;
    }
}
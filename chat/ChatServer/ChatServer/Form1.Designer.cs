namespace ChatServer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtChat = new System.Windows.Forms.TextBox();
            this.btnServer = new System.Windows.Forms.Button();
            this.lbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtChat
            // 
            this.txtChat.Location = new System.Drawing.Point(12, 12);
            this.txtChat.Multiline = true;
            this.txtChat.Name = "txtChat";
            this.txtChat.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtChat.Size = new System.Drawing.Size(486, 434);
            this.txtChat.TabIndex = 0;
            // 
            // btnServer
            // 
            this.btnServer.Location = new System.Drawing.Point(572, 391);
            this.btnServer.Name = "btnServer";
            this.btnServer.Size = new System.Drawing.Size(187, 55);
            this.btnServer.TabIndex = 1;
            this.btnServer.Text = "Server Open";
            this.btnServer.UseVisualStyleBackColor = true;
            this.btnServer.Click += new System.EventHandler(this.btnServer_Click);
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Location = new System.Drawing.Point(635, 15);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(61, 15);
            this.lbl.TabIndex = 2;
            this.lbl.Tag = "Stop";
            this.lbl.Text = "Server Off";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 488);
            this.Controls.Add(this.lbl);
            this.Controls.Add(this.btnServer);
            this.Controls.Add(this.txtChat);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox txtChat;
        private Button btnServer;
        private Label lbl;
    }
}
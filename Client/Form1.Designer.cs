namespace Client
{
    partial class Form1
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
            this.send_msg_btn = new System.Windows.Forms.Button();
            this.message_box = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.connect_ip_box = new System.Windows.Forms.TextBox();
            this.connect_ip_btn = new System.Windows.Forms.Button();
            this.uname_lbl = new System.Windows.Forms.Label();
            this.user_online = new System.Windows.Forms.Label();
            this.set_username_box = new System.Windows.Forms.TextBox();
            this.chat_list_box = new System.Windows.Forms.TextBox();
            this.message_lbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.port_connect_box = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Theme_Btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // send_msg_btn
            // 
            this.send_msg_btn.Location = new System.Drawing.Point(435, 415);
            this.send_msg_btn.Name = "send_msg_btn";
            this.send_msg_btn.Size = new System.Drawing.Size(75, 23);
            this.send_msg_btn.TabIndex = 0;
            this.send_msg_btn.Text = "Send";
            this.send_msg_btn.UseVisualStyleBackColor = true;
            this.send_msg_btn.Click += new System.EventHandler(this.Send_msg_btn_Click);
            // 
            // message_box
            // 
            this.message_box.Location = new System.Drawing.Point(59, 418);
            this.message_box.Name = "message_box";
            this.message_box.Size = new System.Drawing.Size(370, 20);
            this.message_box.TabIndex = 1;
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.SystemColors.Window;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 64);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(155, 342);
            this.listBox1.TabIndex = 3;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.ListBox1_SelectedIndexChanged);
            // 
            // connect_ip_box
            // 
            this.connect_ip_box.Location = new System.Drawing.Point(328, 4);
            this.connect_ip_box.Name = "connect_ip_box";
            this.connect_ip_box.Size = new System.Drawing.Size(97, 20);
            this.connect_ip_box.TabIndex = 4;
            // 
            // connect_ip_btn
            // 
            this.connect_ip_btn.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.connect_ip_btn.Location = new System.Drawing.Point(435, 2);
            this.connect_ip_btn.Name = "connect_ip_btn";
            this.connect_ip_btn.Size = new System.Drawing.Size(75, 23);
            this.connect_ip_btn.TabIndex = 5;
            this.connect_ip_btn.Text = "Connect";
            this.connect_ip_btn.UseVisualStyleBackColor = true;
            this.connect_ip_btn.Click += Connect_ip_btn_Click;
            // 
            // uname_lbl
            // 
            this.uname_lbl.AutoSize = true;
            this.uname_lbl.Location = new System.Drawing.Point(9, 7);
            this.uname_lbl.Name = "uname_lbl";
            this.uname_lbl.Size = new System.Drawing.Size(58, 13);
            this.uname_lbl.TabIndex = 6;
            this.uname_lbl.Text = "Username:";
            // 
            // user_online
            // 
            this.user_online.AutoSize = true;
            this.user_online.Location = new System.Drawing.Point(9, 45);
            this.user_online.Name = "user_online";
            this.user_online.Size = new System.Drawing.Size(63, 13);
            this.user_online.TabIndex = 7;
            this.user_online.Text = "User online:";
            // 
            // set_username_box
            // 
            this.set_username_box.Location = new System.Drawing.Point(69, 4);
            this.set_username_box.Name = "set_username_box";
            this.set_username_box.Size = new System.Drawing.Size(98, 20);
            this.set_username_box.TabIndex = 8;
            // 
            // chat_list_box
            // 
            this.chat_list_box.Location = new System.Drawing.Point(173, 64);
            this.chat_list_box.Multiline = true;
            this.chat_list_box.Name = "chat_list_box";
            this.chat_list_box.Size = new System.Drawing.Size(337, 342);
            this.chat_list_box.TabIndex = 9;
            // 
            // message_lbl
            // 
            this.message_lbl.AutoSize = true;
            this.message_lbl.Location = new System.Drawing.Point(9, 421);
            this.message_lbl.Name = "message_lbl";
            this.message_lbl.Size = new System.Drawing.Size(50, 13);
            this.message_lbl.TabIndex = 10;
            this.message_lbl.Text = "Message";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(331, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Chat";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(177, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Port:";
            // 
            // port_connect_box
            // 
            this.port_connect_box.Location = new System.Drawing.Point(205, 4);
            this.port_connect_box.Name = "port_connect_box";
            this.port_connect_box.Size = new System.Drawing.Size(97, 20);
            this.port_connect_box.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(307, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "IP:";
            // 
            // Theme_Btn
            // 
            this.Theme_Btn.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Theme_Btn.Location = new System.Drawing.Point(435, 41);
            this.Theme_Btn.Name = "Theme_Btn";
            this.Theme_Btn.Size = new System.Drawing.Size(75, 23);
            this.Theme_Btn.TabIndex = 15;
            this.Theme_Btn.Text = "Theme";
            this.Theme_Btn.UseVisualStyleBackColor = true;
            this.Theme_Btn.Click += new System.EventHandler(this.Theme_Btn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 450);
            this.Controls.Add(this.Theme_Btn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.port_connect_box);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.message_lbl);
            this.Controls.Add(this.chat_list_box);
            this.Controls.Add(this.set_username_box);
            this.Controls.Add(this.user_online);
            this.Controls.Add(this.uname_lbl);
            this.Controls.Add(this.connect_ip_btn);
            this.Controls.Add(this.connect_ip_box);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.message_box);
            this.Controls.Add(this.send_msg_btn);
            this.Name = "Form1";
            this.Text = "Cultivation chat";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button send_msg_btn;
        private System.Windows.Forms.TextBox message_box;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox connect_ip_box;
        private System.Windows.Forms.Button connect_ip_btn;
        private System.Windows.Forms.Label uname_lbl;
        private System.Windows.Forms.Label user_online;
        private System.Windows.Forms.TextBox set_username_box;
        private System.Windows.Forms.TextBox chat_list_box;
        private System.Windows.Forms.Label message_lbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox port_connect_box;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Theme_Btn;
    }
}
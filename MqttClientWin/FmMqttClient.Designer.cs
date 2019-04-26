namespace MqttClientWin
{
    partial class FmMqttClient
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtSubTopic = new System.Windows.Forms.TextBox();
            this.txtReceiveMessage = new System.Windows.Forms.TextBox();
            this.txtPubTopic = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSendMessage = new System.Windows.Forms.TextBox();
            this.BtnPublish = new System.Windows.Forms.Button();
            this.btnSubscribe = new System.Windows.Forms.Button();
            this.connect = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.UserName = new System.Windows.Forms.Label();
            this.Pwd = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "订阅主题";
            // 
            // txtSubTopic
            // 
            this.txtSubTopic.Location = new System.Drawing.Point(90, 56);
            this.txtSubTopic.Name = "txtSubTopic";
            this.txtSubTopic.Size = new System.Drawing.Size(283, 21);
            this.txtSubTopic.TabIndex = 1;
            this.txtSubTopic.Text = "position";
            // 
            // txtReceiveMessage
            // 
            this.txtReceiveMessage.Location = new System.Drawing.Point(35, 102);
            this.txtReceiveMessage.Multiline = true;
            this.txtReceiveMessage.Name = "txtReceiveMessage";
            this.txtReceiveMessage.Size = new System.Drawing.Size(425, 167);
            this.txtReceiveMessage.TabIndex = 2;
            // 
            // txtPubTopic
            // 
            this.txtPubTopic.Location = new System.Drawing.Point(92, 291);
            this.txtPubTopic.Name = "txtPubTopic";
            this.txtPubTopic.Size = new System.Drawing.Size(368, 21);
            this.txtPubTopic.TabIndex = 4;
            this.txtPubTopic.Text = "manipulated";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 295);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "发布主题";
            // 
            // txtSendMessage
            // 
            this.txtSendMessage.Location = new System.Drawing.Point(35, 331);
            this.txtSendMessage.Multiline = true;
            this.txtSendMessage.Name = "txtSendMessage";
            this.txtSendMessage.Size = new System.Drawing.Size(425, 77);
            this.txtSendMessage.TabIndex = 5;
            // 
            // BtnPublish
            // 
            this.BtnPublish.Location = new System.Drawing.Point(182, 414);
            this.BtnPublish.Name = "BtnPublish";
            this.BtnPublish.Size = new System.Drawing.Size(95, 36);
            this.BtnPublish.TabIndex = 6;
            this.BtnPublish.Text = "发布";
            this.BtnPublish.UseVisualStyleBackColor = true;
            this.BtnPublish.Click += new System.EventHandler(this.BtnPublish_Click);
            // 
            // btnSubscribe
            // 
            this.btnSubscribe.Location = new System.Drawing.Point(398, 54);
            this.btnSubscribe.Name = "btnSubscribe";
            this.btnSubscribe.Size = new System.Drawing.Size(60, 23);
            this.btnSubscribe.TabIndex = 7;
            this.btnSubscribe.Text = "订阅";
            this.btnSubscribe.UseVisualStyleBackColor = true;
            this.btnSubscribe.Click += new System.EventHandler(this.BtnSubscribe_ClickAsync);
            // 
            // connect
            // 
            this.connect.AccessibleRole = System.Windows.Forms.AccessibleRole.SplitButton;
            this.connect.Location = new System.Drawing.Point(398, 11);
            this.connect.Name = "connect";
            this.connect.Size = new System.Drawing.Size(60, 23);
            this.connect.TabIndex = 8;
            this.connect.Text = "连接";
            this.connect.UseVisualStyleBackColor = true;
            this.connect.Click += new System.EventHandler(this.connect_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(90, 13);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(114, 21);
            this.textBox1.TabIndex = 9;
            this.textBox1.Text = "u001";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(261, 13);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(112, 21);
            this.textBox2.TabIndex = 10;
            this.textBox2.Text = "p001";
            // 
            // UserName
            // 
            this.UserName.AutoSize = true;
            this.UserName.Location = new System.Drawing.Point(31, 18);
            this.UserName.Name = "UserName";
            this.UserName.Size = new System.Drawing.Size(53, 12);
            this.UserName.TabIndex = 11;
            this.UserName.Text = "UserName";
            // 
            // Pwd
            // 
            this.Pwd.AutoSize = true;
            this.Pwd.Location = new System.Drawing.Point(225, 16);
            this.Pwd.Name = "Pwd";
            this.Pwd.Size = new System.Drawing.Size(23, 12);
            this.Pwd.TabIndex = 12;
            this.Pwd.Text = "Pwd";
            // 
            // FmMqttClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 469);
            this.Controls.Add(this.Pwd);
            this.Controls.Add(this.UserName);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.connect);
            this.Controls.Add(this.btnSubscribe);
            this.Controls.Add(this.BtnPublish);
            this.Controls.Add(this.txtSendMessage);
            this.Controls.Add(this.txtPubTopic);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtReceiveMessage);
            this.Controls.Add(this.txtSubTopic);
            this.Controls.Add(this.label1);
            this.Name = "FmMqttClient";
            this.Text = "MQTT订阅及发布客户端";
            this.Load += new System.EventHandler(this.FmMqttClient_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSubTopic;
        private System.Windows.Forms.TextBox txtReceiveMessage;
        private System.Windows.Forms.TextBox txtPubTopic;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSendMessage;
        private System.Windows.Forms.Button BtnPublish;
        private System.Windows.Forms.Button btnSubscribe;
        private System.Windows.Forms.Button connect;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label UserName;
        private System.Windows.Forms.Label Pwd;
    }
}


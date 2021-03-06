﻿namespace test
{
    partial class ImportUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportUI));
            this.btPost = new System.Windows.Forms.Button();
            this.tbIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cb1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbCorp = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.tbSender = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbAccount = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btPost
            // 
            this.btPost.Location = new System.Drawing.Point(245, 204);
            this.btPost.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btPost.Name = "btPost";
            this.btPost.Size = new System.Drawing.Size(100, 29);
            this.btPost.TabIndex = 0;
            this.btPost.Text = "导入";
            this.btPost.UseVisualStyleBackColor = true;
            this.btPost.Click += new System.EventHandler(this.btPost_Click);
            // 
            // tbIP
            // 
            this.tbIP.Location = new System.Drawing.Point(103, 11);
            this.tbIP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbIP.Name = "tbIP";
            this.tbIP.Size = new System.Drawing.Size(184, 25);
            this.tbIP.TabIndex = 1;
            this.tbIP.Text = "192.168.18.8:5367";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "NC服务器IP";
            // 
            // cb1
            // 
            this.cb1.FormattingEnabled = true;
            this.cb1.Items.AddRange(new object[] {
            "材料出库单",
            "其他出库单",
            "库存盘点单"});
            this.cb1.Location = new System.Drawing.Point(103, 58);
            this.cb1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cb1.Name = "cb1";
            this.cb1.Size = new System.Drawing.Size(184, 23);
            this.cb1.TabIndex = 3;
            this.cb1.Text = "材料出库单";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 61);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "单据类型";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 101);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "公司编码";
            // 
            // tbCorp
            // 
            this.tbCorp.Location = new System.Drawing.Point(103, 101);
            this.tbCorp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbCorp.Name = "tbCorp";
            this.tbCorp.Size = new System.Drawing.Size(184, 25);
            this.tbCorp.TabIndex = 1;
            this.tbCorp.Text = "03";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 170);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 15);
            this.label4.TabIndex = 2;
            this.label4.Text = "文件路径";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(103, 166);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(184, 25);
            this.textBox2.TabIndex = 1;
            // 
            // tbSender
            // 
            this.tbSender.Location = new System.Drawing.Point(103, 132);
            this.tbSender.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbSender.Name = "tbSender";
            this.tbSender.Size = new System.Drawing.Size(184, 25);
            this.tbSender.TabIndex = 1;
            this.tbSender.Text = "222";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 138);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 15);
            this.label5.TabIndex = 2;
            this.label5.Text = "发送方";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 210);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 15);
            this.label6.TabIndex = 4;
            this.label6.Text = "账套编码";
            // 
            // tbAccount
            // 
            this.tbAccount.Location = new System.Drawing.Point(103, 204);
            this.tbAccount.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbAccount.Name = "tbAccount";
            this.tbAccount.Size = new System.Drawing.Size(123, 25);
            this.tbAccount.TabIndex = 1;
            this.tbAccount.Text = "5367";
            // 
            // ImportUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 241);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cb1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.tbAccount);
            this.Controls.Add(this.tbSender);
            this.Controls.Add(this.tbCorp);
            this.Controls.Add(this.tbIP);
            this.Controls.Add(this.btPost);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ImportUI";
            this.Text = "导入工具";
            this.Load += new System.EventHandler(this.ImportUI_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btPost;
        private System.Windows.Forms.TextBox tbIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbCorp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox tbSender;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbAccount;
    }
}
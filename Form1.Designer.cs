namespace AutoEqlink
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
            this.btnStart = new System.Windows.Forms.Button();
            this.btnCapture = new System.Windows.Forms.Button();
            this.txtRB2 = new System.Windows.Forms.TextBox();
            this.txtRC2 = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtTotalHorse = new System.Windows.Forms.TextBox();
            this.lbl2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblActivity = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTotalList = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkLDPlayer3 = new System.Windows.Forms.CheckBox();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(584, 372);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(130, 35);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Run Screenshot";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnCapture
            // 
            this.btnCapture.Location = new System.Drawing.Point(86, 372);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(118, 35);
            this.btnCapture.TabIndex = 1;
            this.btnCapture.Text = "Capture Data";
            this.btnCapture.UseVisualStyleBackColor = true;
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // txtRB2
            // 
            this.txtRB2.Location = new System.Drawing.Point(274, 17);
            this.txtRB2.Name = "txtRB2";
            this.txtRB2.Size = new System.Drawing.Size(100, 20);
            this.txtRB2.TabIndex = 34;
            this.txtRB2.Text = "MAL";
            // 
            // txtRC2
            // 
            this.txtRC2.Location = new System.Drawing.Point(92, 17);
            this.txtRC2.Name = "txtRC2";
            this.txtRC2.Size = new System.Drawing.Size(100, 20);
            this.txtRC2.TabIndex = 36;
            this.txtRC2.Text = "MC";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtTotalHorse);
            this.groupBox2.Controls.Add(this.lbl2);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtRB2);
            this.groupBox2.Controls.Add(this.txtRC2);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(511, 44);
            this.groupBox2.TabIndex = 41;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tab HM";
            // 
            // txtTotalHorse
            // 
            this.txtTotalHorse.Location = new System.Drawing.Point(454, 17);
            this.txtTotalHorse.Name = "txtTotalHorse";
            this.txtTotalHorse.Size = new System.Drawing.Size(43, 20);
            this.txtTotalHorse.TabIndex = 41;
            this.txtTotalHorse.Text = "12";
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Location = new System.Drawing.Point(454, 22);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(13, 13);
            this.lbl2.TabIndex = 40;
            this.lbl2.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(380, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 39;
            this.label6.Text = "Total Horse :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 13);
            this.label7.TabIndex = 37;
            this.label7.Text = "Race Country :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(198, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 13);
            this.label8.TabIndex = 35;
            this.label8.Text = "Race Board :";
            // 
            // lblActivity
            // 
            this.lblActivity.AutoSize = true;
            this.lblActivity.Location = new System.Drawing.Point(634, 257);
            this.lblActivity.Name = "lblActivity";
            this.lblActivity.Size = new System.Drawing.Size(86, 13);
            this.lblActivity.TabIndex = 42;
            this.lblActivity.Text = "screen shot data";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(581, 257);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 43;
            this.label2.Text = "Activity :";
            // 
            // lblTotalList
            // 
            this.lblTotalList.AutoSize = true;
            this.lblTotalList.Location = new System.Drawing.Point(137, 270);
            this.lblTotalList.Name = "lblTotalList";
            this.lblTotalList.Size = new System.Drawing.Size(67, 13);
            this.lblTotalList.TabIndex = 44;
            this.lblTotalList.Text = "capture data";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(73, 270);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 45;
            this.label1.Text = "Activity :";
            // 
            // chkLDPlayer3
            // 
            this.chkLDPlayer3.AutoSize = true;
            this.chkLDPlayer3.Location = new System.Drawing.Point(584, 156);
            this.chkLDPlayer3.Name = "chkLDPlayer3";
            this.chkLDPlayer3.Size = new System.Drawing.Size(81, 17);
            this.chkLDPlayer3.TabIndex = 46;
            this.chkLDPlayer3.Text = "LD Player 3";
            this.chkLDPlayer3.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.chkLDPlayer3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTotalList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblActivity);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCapture);
            this.Controls.Add(this.btnStart);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnCapture;
        private System.Windows.Forms.TextBox txtRB2;
        private System.Windows.Forms.TextBox txtRC2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtTotalHorse;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblActivity;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTotalList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkLDPlayer3;
    }
}


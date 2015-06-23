namespace MissionPlanner.Keyboard
{
    partial class KeyboardSetup
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
            this.components = new System.ComponentModel.Container();
            this.progressBarRoll = new MissionPlanner.Controls.HorizontalProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.progressBarPitch = new MissionPlanner.Controls.HorizontalProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.BUT_enable = new MissionPlanner.Controls.MyButton();
            this.label3 = new System.Windows.Forms.Label();
            this.progressBarThrottle = new MissionPlanner.Controls.HorizontalProgressBar();
            this.progressBarYaw = new MissionPlanner.Controls.HorizontalProgressBar();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBarRoll
            // 
            this.progressBarRoll.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.progressBarRoll.Label = null;
            this.progressBarRoll.Location = new System.Drawing.Point(55, 12);
            this.progressBarRoll.Maximum = 2000;
            this.progressBarRoll.maxline = 0;
            this.progressBarRoll.Minimum = 1000;
            this.progressBarRoll.minline = 0;
            this.progressBarRoll.Name = "progressBarRoll";
            this.progressBarRoll.Size = new System.Drawing.Size(100, 23);
            this.progressBarRoll.TabIndex = 6;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(10, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Roll";
            // 
            // progressBarPitch
            // 
            this.progressBarPitch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.progressBarPitch.Label = null;
            this.progressBarPitch.Location = new System.Drawing.Point(55, 41);
            this.progressBarPitch.Maximum = 2000;
            this.progressBarPitch.maxline = 0;
            this.progressBarPitch.Minimum = 1000;
            this.progressBarPitch.minline = 0;
            this.progressBarPitch.Name = "progressBarPitch";
            this.progressBarPitch.Size = new System.Drawing.Size(100, 23);
            this.progressBarPitch.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(10, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Pitch";
            // 
            // BUT_enable
            // 
            this.BUT_enable.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.BUT_enable.Location = new System.Drawing.Point(181, 12);
            this.BUT_enable.Name = "BUT_enable";
            this.BUT_enable.Size = new System.Drawing.Size(75, 23);
            this.BUT_enable.TabIndex = 23;
            this.BUT_enable.Text = "Enable";
            this.BUT_enable.UseVisualStyleBackColor = true;
            this.BUT_enable.Click += new System.EventHandler(this.BUT_enable_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(8, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "Throttle";
            // 
            // progressBarThrottle
            // 
            this.progressBarThrottle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.progressBarThrottle.Label = null;
            this.progressBarThrottle.Location = new System.Drawing.Point(55, 70);
            this.progressBarThrottle.Maximum = 2000;
            this.progressBarThrottle.maxline = 0;
            this.progressBarThrottle.minline = 0;
            this.progressBarThrottle.Name = "progressBarThrottle";
            this.progressBarThrottle.Size = new System.Drawing.Size(100, 23);
            this.progressBarThrottle.TabIndex = 25;
            // 
            // progressBarYaw
            // 
            this.progressBarYaw.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.progressBarYaw.Label = null;
            this.progressBarYaw.Location = new System.Drawing.Point(55, 99);
            this.progressBarYaw.Maximum = 2000;
            this.progressBarYaw.maxline = 0;
            this.progressBarYaw.minline = 0;
            this.progressBarYaw.Name = "progressBarYaw";
            this.progressBarYaw.Size = new System.Drawing.Size(100, 23);
            this.progressBarYaw.TabIndex = 26;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(10, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 27;
            this.label4.Text = "Yaw";
            // 
            // KeyboardSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.progressBarYaw);
            this.Controls.Add(this.progressBarThrottle);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.BUT_enable);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.progressBarPitch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBarRoll);
            this.Name = "KeyboardSetup";
            this.Text = "Keyboard";
            this.Load += new System.EventHandler(this.Keyboard_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.HorizontalProgressBar progressBarRoll;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Controls.MyButton BUT_enable;
        private Controls.HorizontalProgressBar progressBarPitch;
        private System.Windows.Forms.Label label3;
        private Controls.HorizontalProgressBar progressBarThrottle;
        private Controls.HorizontalProgressBar progressBarYaw;
        private System.Windows.Forms.Label label4;
    }
}
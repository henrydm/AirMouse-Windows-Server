namespace AirMouse
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.labelMobY = new System.Windows.Forms.Label();
            this.progressBarRight = new System.Windows.Forms.ProgressBar();
            this.progressBarLeft = new System.Windows.Forms.ProgressBar();
            this.progressBarUp = new System.Windows.Forms.ProgressBar();
            this.progressBarDown = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 261);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(447, 278);
            this.textBox1.TabIndex = 8;
            // 
            // labelMobY
            // 
            this.labelMobY.AutoSize = true;
            this.labelMobY.Location = new System.Drawing.Point(27, 235);
            this.labelMobY.Name = "labelMobY";
            this.labelMobY.Size = new System.Drawing.Size(35, 13);
            this.labelMobY.TabIndex = 7;
            this.labelMobY.Text = "label2";
            // 
            // progressBarRight
            // 
            this.progressBarRight.Location = new System.Drawing.Point(129, 129);
            this.progressBarRight.Maximum = 1000;
            this.progressBarRight.Name = "progressBarRight";
            this.progressBarRight.Size = new System.Drawing.Size(201, 23);
            this.progressBarRight.TabIndex = 9;
            // 
            // progressBarLeft
            // 
            this.progressBarLeft.Location = new System.Drawing.Point(129, 100);
            this.progressBarLeft.Maximum = 1000;
            this.progressBarLeft.Name = "progressBarLeft";
            this.progressBarLeft.Size = new System.Drawing.Size(201, 23);
            this.progressBarLeft.TabIndex = 10;
            // 
            // progressBarUp
            // 
            this.progressBarUp.Location = new System.Drawing.Point(129, 42);
            this.progressBarUp.Maximum = 1000;
            this.progressBarUp.Name = "progressBarUp";
            this.progressBarUp.Size = new System.Drawing.Size(201, 23);
            this.progressBarUp.TabIndex = 11;
            // 
            // progressBarDown
            // 
            this.progressBarDown.Location = new System.Drawing.Point(129, 71);
            this.progressBarDown.Maximum = 1000;
            this.progressBarDown.Name = "progressBarDown";
            this.progressBarDown.Size = new System.Drawing.Size(201, 23);
            this.progressBarDown.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(81, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Up";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(81, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Down";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(81, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Left";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(81, 134);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Right";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 541);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBarDown);
            this.Controls.Add(this.progressBarUp);
            this.Controls.Add(this.progressBarLeft);
            this.Controls.Add(this.progressBarRight);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.labelMobY);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label labelMobY;
        private System.Windows.Forms.ProgressBar progressBarRight;
        private System.Windows.Forms.ProgressBar progressBarLeft;
        private System.Windows.Forms.ProgressBar progressBarUp;
        private System.Windows.Forms.ProgressBar progressBarDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}


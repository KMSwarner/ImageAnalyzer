namespace ImageAnalyzer
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
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblMostIndexedColor = new System.Windows.Forms.Label();
            this.txtMostIndexedColor = new System.Windows.Forms.TextBox();
            this.lblTimesIndexed = new System.Windows.Forms.Label();
            this.txtTimesIndexed = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(39, 38);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Load Image";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(551, 38);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(200, 150);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // lblMostIndexedColor
            // 
            this.lblMostIndexedColor.AutoSize = true;
            this.lblMostIndexedColor.Location = new System.Drawing.Point(39, 98);
            this.lblMostIndexedColor.Name = "lblMostIndexedColor";
            this.lblMostIndexedColor.Size = new System.Drawing.Size(101, 13);
            this.lblMostIndexedColor.TabIndex = 2;
            this.lblMostIndexedColor.Text = "Most Indexed Color:";
            // 
            // txtMostIndexedColor
            // 
            this.txtMostIndexedColor.Location = new System.Drawing.Point(146, 95);
            this.txtMostIndexedColor.Name = "txtMostIndexedColor";
            this.txtMostIndexedColor.Size = new System.Drawing.Size(100, 20);
            this.txtMostIndexedColor.TabIndex = 3;
            // 
            // lblTimesIndexed
            // 
            this.lblTimesIndexed.AutoSize = true;
            this.lblTimesIndexed.Location = new System.Drawing.Point(42, 128);
            this.lblTimesIndexed.Name = "lblTimesIndexed";
            this.lblTimesIndexed.Size = new System.Drawing.Size(79, 13);
            this.lblTimesIndexed.TabIndex = 4;
            this.lblTimesIndexed.Text = "Times Indexed:";
            // 
            // txtTimesIndexed
            // 
            this.txtTimesIndexed.Location = new System.Drawing.Point(146, 128);
            this.txtTimesIndexed.Name = "lblTimesIndexed";
            this.txtTimesIndexed.Size = new System.Drawing.Size(100, 20);
            this.txtTimesIndexed.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtTimesIndexed);
            this.Controls.Add(this.lblTimesIndexed);
            this.Controls.Add(this.txtMostIndexedColor);
            this.Controls.Add(this.lblMostIndexedColor);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblMostIndexedColor;
        private System.Windows.Forms.TextBox txtMostIndexedColor;
        private System.Windows.Forms.Label lblTimesIndexed;
        private System.Windows.Forms.Label txtTimesIndexed;
    }
}


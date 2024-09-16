namespace EventsDelagatesDemo
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
            btnOpen = new Button();
            btnExit = new Button();
            btnManipulate = new Button();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // btnOpen
            // 
            btnOpen.Location = new Point(12, 12);
            btnOpen.Name = "btnOpen";
            btnOpen.Size = new Size(141, 29);
            btnOpen.TabIndex = 0;
            btnOpen.Text = "Image File Open";
            btnOpen.UseVisualStyleBackColor = true;
            btnOpen.Click += btnOpen_Click;
            // 
            // btnExit
            // 
            btnExit.Location = new Point(1169, 518);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(94, 29);
            btnExit.TabIndex = 1;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // btnManipulate
            // 
            btnManipulate.Location = new Point(530, 518);
            btnManipulate.Name = "btnManipulate";
            btnManipulate.Size = new Size(154, 29);
            btnManipulate.TabIndex = 2;
            btnManipulate.Text = "Manipulate Image";
            btnManipulate.UseVisualStyleBackColor = true;
            btnManipulate.Click += btnManipulate_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(834, 518);
            label1.Name = "label1";
            label1.Size = new Size(50, 20);
            label1.TabIndex = 3;
            label1.Text = "label1";
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(108, 47);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(576, 421);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BorderStyle = BorderStyle.FixedSingle;
            pictureBox2.Location = new Point(724, 47);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(576, 421);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 5;
            pictureBox2.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1376, 582);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(label1);
            Controls.Add(btnManipulate);
            Controls.Add(btnExit);
            Controls.Add(btnOpen);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnOpen;
        private Button btnExit;
        private Button btnManipulate;
        private Label label1;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
    }
}

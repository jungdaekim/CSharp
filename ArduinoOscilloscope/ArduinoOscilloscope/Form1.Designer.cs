namespace ArduinoOscilloscope
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
            components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            txtCom = new TextBox();
            btnOpenPort = new Button();
            btnClosePort = new Button();
            txtSamples = new TextBox();
            txtVref = new TextBox();
            chkHighSpeed = new CheckBox();
            btnTrigger = new Button();
            txtSerialData = new TextBox();
            rdoDCCoupling = new RadioButton();
            radioButton2 = new RadioButton();
            chkDebug = new CheckBox();
            btnSingle = new Button();
            chkMarkers = new CheckBox();
            trkVerticalScale = new VScrollBar();
            trkScanTime = new HScrollBar();
            groupBox1 = new GroupBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            groupBox2 = new GroupBox();
            lblScanTime = new Label();
            lblVerticalScale = new Label();
            chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            timer1 = new System.Windows.Forms.Timer(components);
            btnExit = new Button();
            lblFreq = new Label();
            lblAvgVolts = new Label();
            lblMaxVolts = new Label();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chart1).BeginInit();
            SuspendLayout();
            // 
            // txtCom
            // 
            txtCom.Location = new Point(64, 47);
            txtCom.Margin = new Padding(4);
            txtCom.Name = "txtCom";
            txtCom.Size = new Size(44, 27);
            txtCom.TabIndex = 0;
            // 
            // btnOpenPort
            // 
            btnOpenPort.Location = new Point(7, 85);
            btnOpenPort.Margin = new Padding(4);
            btnOpenPort.Name = "btnOpenPort";
            btnOpenPort.Size = new Size(117, 31);
            btnOpenPort.TabIndex = 1;
            btnOpenPort.Text = "Open Port";
            btnOpenPort.UseVisualStyleBackColor = true;
            btnOpenPort.Click += btnOpenPort_Click;
            // 
            // btnClosePort
            // 
            btnClosePort.Location = new Point(8, 124);
            btnClosePort.Margin = new Padding(4);
            btnClosePort.Name = "btnClosePort";
            btnClosePort.Size = new Size(116, 31);
            btnClosePort.TabIndex = 2;
            btnClosePort.Text = "Close Port";
            btnClosePort.UseVisualStyleBackColor = true;
            btnClosePort.Click += btnClosePort_Click;
            // 
            // txtSamples
            // 
            txtSamples.Location = new Point(72, 283);
            txtSamples.Margin = new Padding(4);
            txtSamples.Name = "txtSamples";
            txtSamples.Size = new Size(127, 27);
            txtSamples.TabIndex = 3;
            // 
            // txtVref
            // 
            txtVref.Location = new Point(78, 371);
            txtVref.Margin = new Padding(4);
            txtVref.Name = "txtVref";
            txtVref.Size = new Size(127, 27);
            txtVref.TabIndex = 4;
            // 
            // chkHighSpeed
            // 
            chkHighSpeed.AutoSize = true;
            chkHighSpeed.Location = new Point(28, 452);
            chkHighSpeed.Margin = new Padding(4);
            chkHighSpeed.Name = "chkHighSpeed";
            chkHighSpeed.Size = new Size(111, 24);
            chkHighSpeed.TabIndex = 5;
            chkHighSpeed.Text = "High Speed";
            chkHighSpeed.UseVisualStyleBackColor = true;
            // 
            // btnTrigger
            // 
            btnTrigger.Location = new Point(15, 509);
            btnTrigger.Margin = new Padding(4);
            btnTrigger.Name = "btnTrigger";
            btnTrigger.Size = new Size(150, 49);
            btnTrigger.TabIndex = 6;
            btnTrigger.Text = "Start";
            btnTrigger.UseVisualStyleBackColor = true;
            btnTrigger.Click += btnTrigger_Click;
            // 
            // txtSerialData
            // 
            txtSerialData.Location = new Point(244, 440);
            txtSerialData.Margin = new Padding(4);
            txtSerialData.Multiline = true;
            txtSerialData.Name = "txtSerialData";
            txtSerialData.ScrollBars = ScrollBars.Vertical;
            txtSerialData.Size = new Size(360, 125);
            txtSerialData.TabIndex = 7;
            // 
            // rdoDCCoupling
            // 
            rdoDCCoupling.AutoSize = true;
            rdoDCCoupling.Location = new Point(28, 31);
            rdoDCCoupling.Margin = new Padding(4);
            rdoDCCoupling.Name = "rdoDCCoupling";
            rdoDCCoupling.Size = new Size(51, 24);
            rdoDCCoupling.TabIndex = 8;
            rdoDCCoupling.TabStop = true;
            rdoDCCoupling.Text = "DC";
            rdoDCCoupling.UseVisualStyleBackColor = true;
            rdoDCCoupling.CheckedChanged += rdoDCCoupling_CheckedChanged;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(28, 69);
            radioButton2.Margin = new Padding(4);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(50, 24);
            radioButton2.TabIndex = 9;
            radioButton2.TabStop = true;
            radioButton2.Text = "AC";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.CheckedChanged += radioButton2_CheckedChanged;
            // 
            // chkDebug
            // 
            chkDebug.AutoSize = true;
            chkDebug.Location = new Point(964, 452);
            chkDebug.Margin = new Padding(4);
            chkDebug.Name = "chkDebug";
            chkDebug.Size = new Size(77, 24);
            chkDebug.TabIndex = 10;
            chkDebug.Text = "Debug";
            chkDebug.UseVisualStyleBackColor = true;
            // 
            // btnSingle
            // 
            btnSingle.Location = new Point(1072, 11);
            btnSingle.Margin = new Padding(4);
            btnSingle.Name = "btnSingle";
            btnSingle.Size = new Size(96, 31);
            btnSingle.TabIndex = 11;
            btnSingle.Text = "Single";
            btnSingle.UseVisualStyleBackColor = true;
            btnSingle.Click += btnSingle_Click;
            // 
            // chkMarkers
            // 
            chkMarkers.AutoSize = true;
            chkMarkers.Location = new Point(926, 16);
            chkMarkers.Margin = new Padding(4);
            chkMarkers.Name = "chkMarkers";
            chkMarkers.Size = new Size(85, 24);
            chkMarkers.TabIndex = 12;
            chkMarkers.Text = "Markers";
            chkMarkers.UseVisualStyleBackColor = true;
            // 
            // trkVerticalScale
            // 
            trkVerticalScale.Location = new Point(268, 79);
            trkVerticalScale.Minimum = 1;
            trkVerticalScale.Name = "trkVerticalScale";
            trkVerticalScale.Size = new Size(27, 214);
            trkVerticalScale.TabIndex = 13;
            trkVerticalScale.Value = 1;
            trkVerticalScale.Scroll += trkVerticalScale_Scroll;
            // 
            // trkScanTime
            // 
            trkScanTime.Location = new Point(494, 371);
            trkScanTime.Maximum = 20;
            trkScanTime.Minimum = 1;
            trkScanTime.Name = "trkScanTime";
            trkScanTime.Size = new Size(352, 23);
            trkScanTime.TabIndex = 14;
            trkScanTime.Value = 1;
            trkScanTime.Scroll += trkScanTime_Scroll;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(txtCom);
            groupBox1.Controls.Add(btnOpenPort);
            groupBox1.Controls.Add(btnClosePort);
            groupBox1.Location = new Point(15, 16);
            groupBox1.Margin = new Padding(4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4);
            groupBox1.Size = new Size(150, 177);
            groupBox1.TabIndex = 15;
            groupBox1.TabStop = false;
            groupBox1.Text = "COM Port";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(8, 51);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(48, 20);
            label1.TabIndex = 3;
            label1.Text = "COM:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(22, 248);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(129, 20);
            label2.TabIndex = 16;
            label2.Text = "Samples per Scan";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(26, 332);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(100, 20);
            label3.TabIndex = 17;
            label3.Text = "Arduino Vref:";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(rdoDCCoupling);
            groupBox2.Controls.Add(radioButton2);
            groupBox2.Location = new Point(688, 452);
            groupBox2.Margin = new Padding(4);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4);
            groupBox2.Size = new Size(180, 113);
            groupBox2.TabIndex = 18;
            groupBox2.TabStop = false;
            groupBox2.Text = "Coupling";
            // 
            // lblScanTime
            // 
            lblScanTime.AutoSize = true;
            lblScanTime.Location = new Point(618, 412);
            lblScanTime.Margin = new Padding(4, 0, 4, 0);
            lblScanTime.Name = "lblScanTime";
            lblScanTime.Size = new Size(50, 20);
            lblScanTime.TabIndex = 19;
            lblScanTime.Text = "label4";
            // 
            // lblVerticalScale
            // 
            lblVerticalScale.AutoSize = true;
            lblVerticalScale.Location = new Point(245, 332);
            lblVerticalScale.Margin = new Padding(4, 0, 4, 0);
            lblVerticalScale.Name = "lblVerticalScale";
            lblVerticalScale.Size = new Size(50, 20);
            lblVerticalScale.TabIndex = 20;
            lblVerticalScale.Text = "label5";
            // 
            // chart1
            // 
            chart1.BackColor = Color.Silver;
            chart1.BorderlineColor = Color.IndianRed;
            chartArea1.Name = "ChartArea1";
            chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chart1.Legends.Add(legend1);
            chart1.Location = new Point(316, 49);
            chart1.Margin = new Padding(4);
            chart1.Name = "chart1";
            chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Grayscale;
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chart1.Series.Add(series1);
            chart1.Size = new Size(1116, 303);
            chart1.TabIndex = 21;
            chart1.Text = "chart1";
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // btnExit
            // 
            btnExit.Location = new Point(964, 496);
            btnExit.Margin = new Padding(4);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(194, 49);
            btnExit.TabIndex = 22;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // lblFreq
            // 
            lblFreq.AutoSize = true;
            lblFreq.ForeColor = SystemColors.ControlLightLight;
            lblFreq.Location = new Point(475, 25);
            lblFreq.Name = "lblFreq";
            lblFreq.Size = new Size(50, 20);
            lblFreq.TabIndex = 23;
            lblFreq.Text = "label4";
            // 
            // lblAvgVolts
            // 
            lblAvgVolts.AutoSize = true;
            lblAvgVolts.ForeColor = SystemColors.ControlLightLight;
            lblAvgVolts.Location = new Point(633, 25);
            lblAvgVolts.Name = "lblAvgVolts";
            lblAvgVolts.Size = new Size(50, 20);
            lblAvgVolts.TabIndex = 24;
            lblAvgVolts.Text = "label4";
            // 
            // lblMaxVolts
            // 
            lblMaxVolts.AutoSize = true;
            lblMaxVolts.ForeColor = SystemColors.ControlLightLight;
            lblMaxVolts.Location = new Point(766, 25);
            lblMaxVolts.Name = "lblMaxVolts";
            lblMaxVolts.Size = new Size(89, 20);
            lblMaxVolts.TabIndex = 25;
            lblMaxVolts.Text = "lblMaxVolts";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1553, 600);
            Controls.Add(lblMaxVolts);
            Controls.Add(lblAvgVolts);
            Controls.Add(lblFreq);
            Controls.Add(btnExit);
            Controls.Add(chart1);
            Controls.Add(lblVerticalScale);
            Controls.Add(lblScanTime);
            Controls.Add(groupBox2);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(groupBox1);
            Controls.Add(trkScanTime);
            Controls.Add(trkVerticalScale);
            Controls.Add(chkMarkers);
            Controls.Add(btnSingle);
            Controls.Add(chkDebug);
            Controls.Add(txtSerialData);
            Controls.Add(btnTrigger);
            Controls.Add(chkHighSpeed);
            Controls.Add(txtVref);
            Controls.Add(txtSamples);
            Margin = new Padding(4);
            Name = "Form1";
            Text = "Form1";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)chart1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtCom;
        private Button btnOpenPort;
        private Button btnClosePort;
        private TextBox txtSamples;
        private TextBox txtVref;
        private CheckBox chkHighSpeed;
        private Button btnTrigger;
        private TextBox txtSerialData;
        private RadioButton rdoDCCoupling;
        private RadioButton radioButton2;
        private CheckBox chkDebug;
        private Button btnSingle;
        private CheckBox chkMarkers;
        private VScrollBar trkVerticalScale;
        private HScrollBar trkScanTime;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private GroupBox groupBox2;
        private Label lblScanTime;
        private Label lblVerticalScale;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Timer timer1;
        private Button btnExit;
        private Label lblFreq;
        private Label lblAvgVolts;
        private Label lblMaxVolts;
    }
}

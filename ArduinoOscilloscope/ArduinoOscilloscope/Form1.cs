using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;       // for colors used in the UI controls
using System.Linq;
using System.Windows.Forms;
using System.Management;   // for COM port descrptions
using System.IO.Ports;
using System.IO;           // for writing CSV file
using System.Windows.Forms.DataVisualization.Charting;
using System.Diagnostics;  // For Stopwatch


namespace ArduinoOscilloscope
{
    #region DOCS
    //GENERAL

    // This app sends a simple string request such as "B100" to an Arduino, which requests a "Burst"
    // of samples over a scan time of 100 mSec. It also sends another string request (eq., "S500")
    // which configures the Arduino to gather 500 samples per burst. If the request is "S500", the
    // Arduino sketch code, sets its "numBurstSamples" variable=500.
    // If the request if "B100", it calculates the per-sample delay necessary to space the 500 samples
    // over a burst duration of 100 mSec, then does an analogRead() on the A0 analog pin, stores
    // those 500, 0-1023 samples in an array, then does a Serial.println() to send those samples over the COM.

    // NOTES

    // DYNAMIC BUFFER TIME: Adds a "safety buffer" of time between scans. Takes elapsed time
    // to grab & plot (which depends on number of samples and sample rate) and adds
    // fixed buffer to that, rather than a fixed buffer time independent of samples and sample rate.

    // - HIGH SPEED MODE: the "High Speed" mode assumes Arduino has prescaler set to allow
    // only 0.017 mSec for an analogRead(), therby greatly increasing the sample rate.
    // The adjustable "burstDurationmSec" is a function of the number of samples entered by the user,
    // and the analogRead() time per sample:
    //    burstDurationmSec = numSamples * mSecPerSample * (horizontal trackbar setting)
    // Since the horizontal trackbar range is 1-20, this makes the burstDurationmSec in
    // multiples of the minimum possible duration, which is limited by the seconds/sample rate.
    // 

    // - BAUD RATE: The default configuration in both the Arduino and this app is to use a
    // baud rate of 2,000,000.

    // - TIME VALUES: The Arduino does not gather or send any time values associated with each
    // sample. Instead it just sends the sample values (0-1023), and this app generates the
    // sample time (seconds per sample) values based on knowledge of the total scan/burst time
    // and the number of samples. This saves a lot of SRAM space on the Arduino
    // (which is limited to 2048 bytes) so that a larger number of samples can be used and
    // stored on the Arduino.

    // - BANDWIDTH: the default setting time to do an "analogRead()" on the Arduino is listed 
    // as 0.1 mSec. However, in practice it's slightly longer (maybe 0.11 mSec). Therefore,
    // since you can only do 1 sample in about 0.0001 seconds, the sample rate is less than
    // 10kHz (1/0.0001). Which means the bandwidth due to Nyquist is less than 5kHz. This
    // limits the minimum scan/burst time to greater than numSamples *(0.1 mSec required
    // per sample). So for 500 samples, the minimum scan/burst time is > 50 mSec, and for 
    // 200 samples it's > 20 mSec.

    // -SAMPLE & SMAPLE RATES: A default of 500 samples per scan is used for Nano (2k SRAM),
    // though this can probably be increased if necessary. Also, since the Mega has 8k SRAM,
    // the samples per scan can be increased dramatically. Since each 0-1023 sample is stored
    // as a 2-byte (16 bit) int, 500 samples requires only about 1,000 bytes of the 2048 byte
    // Nano and Uno SRAM capacity. Fewer samples allows for a shorter scan/burst duration,
    // and more samples allows for a longer scan/burst duration. However, since you're always
    // limited byt eh > 0.1mSec required sample time, you'll never get more than 10,000 samples
    // per second scan/burst time, the scan rate is only 500 samples/sec., and the bandwidth
    // is < 250 Hz.

    // THIS APP

    // This app uses System.Management to access not only the COM ports available, but also their
    // descriptions, and that info is printed in the info text box upon startup. The user enters
    // the desired COM port number in a text box, pressed the "Open Port" buffon, and this app
    // opens the port. The user then enters the total desired scan time per burst, as well as the 
    // total number of desired samples per burst, and the Arduino Vref value (the value it uses to
    // represent a sample range of 0-1023).

    // NOTE: the users needs to first measure the Vref value used by the Arduino by connecting
    // a multimeter and reading the Arduino "5V" pin and entering that value in the "Vref"
    // textbox.

    // When the Trigger/Start button is pushed, the app sends an "Sxxx" command to tell the Arduino
    // how many samples to grab per burst. It then sends the "Byyy" command to define the total
    // mSec per scan. It also sets up a System.Timer, with an interval that dynamically accounts
    // for the total time to perform the request, read, and plot (ie, ReadArduinoBurst method),
    // plus a fixed buffer time. Since the ReadArduinoBurst() elapsed time will vary depending
    // on numSamples and Arduino read speed, the total timer interval will be dyamically changing.
    // The timer event handler then sends continuing "Byyy" requests for burst data to the Arduino.

    // The event handler also calls the "ReadArduinoBurst" method to read the data
    // (which looks like "1023\r\r" and place it in a List<string> called "rawInputStringList".
    // It then calls methods in the class called "InputDataProcessor". This class generates
    // the zero-based mSec values for each sample, which it calculates with the following formula:
    //          Time step=(Total scan time per burst)/(number of samples)
    // It also converts the 0-1023 sample values to voltage values using the user-specified Vref
    // value. It stores the resulting time and voltage values in a doule [,] called
    // "ProcessedDoubleArray". Finally "ReadArduinoBurst" calls "PlotDataBurst" to plot the
    // array values on a seprate thread.

    #endregion

    #region TO DO

    // HIGH
    //   ISSUES/BUGS:

    // LOW
    //   FEATURES:
    // - Add second channel?
    // - Add FFT?
    #endregion

    public partial class Form1 : Form
    {
        #region INITIALIZING

        // Configure COM port and Arduino
        public SerialPort ArduinoPort;
        public double vref; // The Arduino Vref, used to determine the voltage equivalent of 1023 samples
        public int baudRate = 2000000;
        public int numSamples = 500;
        public int scanTimeBuffermSec = 50;

        //Configure scope
        public double yAxisMax;
        public bool dcCoupling;
        public bool isSingleClicked = false;

        //Burst variables
        public bool endOfData = false; // Used in readArduinoBurst to detect end of data
        public int burstDurationmSec;  // User-specified mSec per scan/burst
        public List<string> rawInputStringList = new List<string>(); // used in ReadArduinoBurst to gather
        public double mSecPerSample = 0.1;

        //Debug variables
        public bool dbug = false;

        //Debugging stopwatches
        Stopwatch stopwatchArduinoBurst = new Stopwatch();

        public InputDataProcessor processor;

        // Plot varialbes to hold data for plotting
        List<double> xVals = new List<double> { 10.0 };
        List<double> yVals = new List<double> { 0.0 };

        #endregion


        public Form1()
        {
            InitializeComponent();

            UISetup();

            ChartSetup();

            PortUISetup();
        }

        #region METHODS
        private void UISetup()
        {
            // Populate UI text boxes with initial values
            txtCom.Text = "3";
            txtVref.Text = "5.2";
            txtSamples.Text = "500";
            rdoDCCoupling.Checked = true; // Initialize radio button with DC coupling selected

            // Horizontal Scan Time trackbar
            trkScanTime.Value = 10;  // It's a 1-20 trackbar, set it in the middle
            lblScanTime.Text = Convert.ToString(0.05 * Convert.ToDouble(trkScanTime.Value)) + " Sec.";

            // Vertical volts scale trackbar
            trkVerticalScale.Value = 10; // It's a 1~10 trackbar, set it at max (1x or 5V)
            lblVerticalScale.Text = "Scale X " + Convert.ToString(0.1 * Convert.ToDouble(trkVerticalScale.Value)) + "  V";
            // Pre-configure some UI buttons to be disabled and invisible
            // until they are relevant.
            btnTrigger.Enabled = false;
            //btnTrigger.Visible = false;
            btnClosePort.Enabled = false;
            //btnClosePort.Visible = false;
        }
        private void ChartSetup()
        {
            //Configure the chart
            chart1.Series["Series1"].ChartType = SeriesChartType.Line;
            //Change data line width to 2
            chart1.Series["Series1"].BorderWidth = 2;
            chart1.Series["Series1"].Color = Color.Yellow;
            chart1.ChartAreas["ChartArea1"].AxisX.Minimum = 0;
            chart1.ChartAreas["ChartArea1"].AxisY.Minimum = -5.0;
            chart1.ChartAreas["ChartArea1"].AxisY.Interval = 1.0;
            chart1.ChartAreas["ChartArea1"].AxisY.Maximum = yAxisMax;
            chart1.ChartAreas["ChartArea1"].AxisX.RoundAxisValues();
            chart1.ChartAreas["ChartArea1"].AxisX.Title = "mSec";
            chart1.ChartAreas["ChartArea1"].AxisX.TitleForeColor = Color.Black;
            chart1.ChartAreas["ChartArea1"].AxisX.LineColor = Color.Gray;
            chart1.ChartAreas["ChartArea1"].AxisY.LineColor = Color.Gray;
            chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.Gray;
            chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineWidth = 1;
            chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineWidth = 1;
            chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.Gray;
            chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.ForeColor = Color.Black;
            chart1.ChartAreas["ChartArea1"].AxisY.LabelStyle.ForeColor = Color.Black;
            chart1.ChartAreas["ChartArea1"].BackColor = Color.Black;
            chart1.Legends.Clear();

            // Pre-populate chart with random numbers to show on opening
            chart1.Series["Series1"].Points.AddXY(0.0, 0.0);
            chart1.Series["Series1"].Points.DataBindXY(xVals, yVals);
        }

        private void PortUISetup()
        {
            // Find active serial ports and display
            txtSerialData.AppendText("The following active COM ports are found:" +
                Environment.NewLine);
            List<string> portlist = GetPorts();
            foreach (string s in portlist)
            {
                txtSerialData.AppendText(s + Environment.NewLine);
            }
        }

        private List<string> GetPorts()
        {
            // This searches all the properties of the Plug and Play devices (using "Win32_PnPEntity");
            // The "caption" is the text description of the COM port object.
            using (var searcher = new ManagementObjectSearcher("SELECT * FROM " +
                "Win32_PnPEntity WHERE Caption like '%(COM%'"))
            {
                // This gets the simple port names, such as "COM4"
                string[] portnames = SerialPort.GetPortNames();

                // This gets the caption/description of the found ports
                var ports = searcher.Get().Cast<ManagementBaseObject>().ToList().Select(p => p["Caption"].ToString());
                List<string> portList = portnames.Select(n => n + " - " + ports.FirstOrDefault(s => s.Contains(n))).ToList();

                return portList;
            }
        }

        private void OpenPort()
        {
            // This is called by the "Open Port" button
            ArduinoPort = new SerialPort();
            ArduinoPort.BaudRate = baudRate;
            ArduinoPort.PortName = "COM" + txtCom.Text;

            try
            {
                ArduinoPort.Open();
            }
            catch(Exception)
            {
                MessageBox.Show("You need to plug in the device first!");
                return;
            }
            if(ArduinoPort.IsOpen)
            {
                btnOpenPort.Text = "PORT_OPEN";
                //btnOpenPort.ForeColor = Color.Red;
                //btnOpenPort.BackColor = Color.LightYellow;

                btnOpenPort.Enabled = false;
                btnClosePort.Enabled = true;
                btnClosePort.Visible = true;
                btnTrigger.Enabled = true;
                btnTrigger.Visible = true;
            }
        }

        private void ReadArduinoBurst()
        {
            int stringsReadCount = 1;

            while(endOfData==false)
            {
                try
                {
                    string inputString = ArduinoPort.ReadLine();

                    if(inputString.TrimEnd('\r', '\n')=="END")
                    {
                        endOfData = true;
                    }
                    else
                    {
                        rawInputStringList.Add(inputString);
                    }
                }
                catch
                {
                    MessageBox.Show("Couldn't read data from Arduino");
                }
                if (!endOfData) { stringsReadCount++;  }
            }

            // Use "InputDataprocessor" to process the data and fill the final time, volts
            // values for each burst into a double [,] called "processedDoubleArray"
            processor = new InputDataProcessor(rawInputStringList);
            ProcessDataBurst();
        }

        private void ProcessDataBurst()
        {
            // Convert the raw input string array from the Ardino into
            // an array of integer called "inputIntArray[]"
            processor.ParseCSVBurst();

            // Calculate how many mSec per sample, bsed on user-specified burst duration 
            // (in mSec) and user-specified number of samples per scan
            double sampleIntervalmSec =
                Convert.ToDouble(burstDurationmSec) / Convert.ToDouble(numSamples) - 0.0025;

            // This method is called by the Plot button click event to generate
            // time values for each sample
            processor.ZeroTimesBurst(sampleIntervalmSec);
            processor.ScaleValuesBurst(1023, vref);

            // If AC coupling is selected, fill the "processedDoubleArray" with values
            // that have the average of the burst subtracted.
            if(dcCoupling==false)
            {
                processor.GetACCoupled();
            }

            // Calculate the max voltage value in each burst and display it in a label on the chart
            double maxVolts = processor.GetMax();
            lblMaxVolts.BringToFront();
            lblMaxVolts.BackColor = Color.Black;
            lblMaxVolts.Text = string.Format("Max. Volts={0:0.00}", maxVolts);

            double avgVolts = processor.GetAverage();
            lblAvgVolts.BringToFront();
            lblAvgVolts.BackColor = Color.Black;
            lblAvgVolts.Text = string.Format("Avg. Volts={0:0.00}", avgVolts);

            double freq = processor.GetFreq();
            lblFreq.BringToFront();
            lblFreq.BackColor = Color.Black;
            lblFreq.Text = string.Format("Freq.={0:0} Hz", freq);

            PlotDataBurst(processor.processedDoubleArray.Length / 2);

            // Reset stuff to allow another burst to be read
            endOfData = false;
            rawInputStringList.Clear();

        }

        private void PlotDataBurst(int numSamples)
        {
            xVals.Clear();
            yVals.Clear();

            for(int i=0;i<numSamples;i++)
            {
                xVals.Add(processor.processedDoubleArray[i, 0]);
                yVals.Add(processor.processedDoubleArray[i, 1]);
            }

            chart1.Invoke(new MethodInvoker(
                delegate
                {
                    chart1.Series["Series1"].Points.DataBindXY(xVals, yVals);
                }
                ));
        }

        #endregion

        #region EVENT HANDLERS
        // Open COM Port
        private void btnOpenPort_Click(object sender, EventArgs e)
        {
            OpenPort();
        }
        // Trigger to start scan and initialize refresh timer
        private void btnTrigger_Click(object sender, EventArgs e)
        {
            numSamples = Convert.ToInt32(txtSamples.Text);

            if(chkHighSpeed.Checked )
            {
                mSecPerSample = 0.02;
            }
            else
            {
                mSecPerSample = 0.1;
            }
            // This sends a command to tell the Arduino how many samples to get per burst
            ArduinoPort.WriteLine("S" + numSamples.ToString());

            //Configure X axis max values. The trackbar sets a range that is multiplies
            // of the minimum possible scan time (based on the time it takes to do an
            // analogRead() on the set number of samples).
            lblScanTime.Text = Convert.ToString(Convert.ToInt32(numSamples * mSecPerSample *
                Convert.ToDouble(trkScanTime.Value))) + " mSec.";

            // Configure Y axis max values
            yAxisMax = 0.5 * Convert.ToDouble(trkVerticalScale.Value);
            lblVerticalScale.Text = "Scale X " + 0.2 * yAxisMax;
            chart1.ChartAreas["ChartArea1"].AxisY.Maximum = yAxisMax;

            //This sends a "B" command to the Arduino to have it send back a burst of data,
            // and specifies the duration of the burst.
            burstDurationmSec = Convert.ToInt32(numSamples * mSecPerSample *
                Convert.ToDouble(trkScanTime.Value));
            vref=Convert.ToDouble(txtVref.Text);

            ArduinoPort.WriteLine("B"+burstDurationmSec.ToString());

            ReadArduinoBurst();

            timer1.Enabled = true;
            // This sets the timer interval to the desired scan time plus a fixed
            // mSec delay.
            timer1.Interval = burstDurationmSec + scanTimeBuffermSec;
        }

        // Configure scope settings after scan starts
        // Set horizontal and vertical scales
        private void trkScanTime_Scroll(object sender, ScrollEventArgs e)
        {
            // This trackbar goes from 1-20. The minimum value should be:
            // (numSamples)*(analogRead() time per sample)
            // numSamples is obtained from txtSamples, and is available when scan is started

            burstDurationmSec=Convert.ToInt32(numSamples*mSecPerSample*
                Convert.ToDouble(trkScanTime.Value));
            timer1.Interval = burstDurationmSec + scanTimeBuffermSec;
        }

        private void trkVerticalScale_Scroll(object sender, ScrollEventArgs e)
        {
            yAxisMax = Convert.ToDouble(trkVerticalScale.Value);
        }
        //Determine if AC or DC coupling is to be used
        private void rdoDCCoupling_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }
        //Refresh data and plot
        private void timer1_Tick(object sender, EventArgs e)
        {
            if(chkMarkers.Checked)
            {
                chart1.Series["Series1"].MarkerStyle = MarkerStyle.Circle;
                chart1.Series["Series1"].MarkerColor = Color.Red;
            }
            else
            {
                chart1.Series["Series1"].MarkerStyle = MarkerStyle.None;
            }

            // Sets the chart max. & min. voltage range. Since the vertical scale trackbar
            // is set to a range of 1-10, the "0.5" gives a max of +5V range. The minimum
            // is set to the minus of that
            yAxisMax=0.5*Convert.ToDouble(trkVerticalScale.Value);
            lblVerticalScale.Text = "Scale X " + 0.2 * yAxisMax;
            chart1.ChartAreas["ChartArea1"].AxisY.Maximum = yAxisMax;
            chart1.ChartAreas["ChartArea1"].AxisY.Minimum = -yAxisMax;

            // set the horizontal time range trackbar text
            lblScanTime.Text = Convert.ToString(Convert.ToInt32(numSamples*mSecPerSample *
                Convert.ToDouble(trkScanTime.Value))) + " mSec.";

            ArduinoPort.WriteLine("B" + burstDurationmSec.ToString());

            stopwatchArduinoBurst.Restart();
            ReadArduinoBurst();
            stopwatchArduinoBurst.Stop();

            var burstTime = stopwatchArduinoBurst.ElapsedMilliseconds;

            // This dynamically sets the timer interval based on the measured, elapsed time
            // to perform the ReadArduinoBurst method, which grab the burst and plots it.
            timer1.Interval = Convert.ToInt16(burstTime) + scanTimeBuffermSec;

            if(chkDebug.Checked==true)
            {
                txtSerialData.Invoke(new MethodInvoker(
                    delegate
                    {
                        txtSerialData.Text = "Total scan + plot time=" +
                             burstTime + " mSec" + Environment.NewLine +
                             "Total buffer time=" + scanTimeBuffermSec + " mSec" +
                             Environment.NewLine + "Timer interval=" + timer1.Interval + " mSec";
                    }
                    ));
            }
        }

        // Toggle Single Shot
        private void btnSingle_Click(object sender, EventArgs e)
        {
            isSingleClicked = !isSingleClicked;

            if(isSingleClicked)
            {
                timer1.Enabled = false;
                btnSingle.BackColor = Color.Yellow;
                //File.WriteAllLines(@"C:\test.csv", processor.GetCSVOutput);
            }
            else
            {
                timer1.Enabled= true;
                btnSingle.BackColor = Color.White;
            }
        }

        //End
        private void btnClosePort_Click(object sender, EventArgs e)
        {
            btnClosePort.Enabled= false;
            btnOpenPort.Text = "Open Port";
            btnOpenPort.Enabled = true;
            btnTrigger.Enabled = false;
            ArduinoPort.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion


    }
}

using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;

namespace EmguCVTest
{
    public partial class Form1 : Form
    {
        bool streamVideo = false;
        static int cameraIdx = 0;

        double fpsOld = 30.0;
        double fps;
        DateTime last, now;
        TimeSpan ts;

        VideoCapture capture = new VideoCapture(cameraIdx);
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGrab_Click(object sender, EventArgs e)
        {
            Mat frame = new Mat();
            // capture = new VideoCapture(0);

            bool readSuccess = capture.Read(frame);
            if (readSuccess)
            {
                pictureBox1.Image = frame.ToBitmap();
                frame.Dispose();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void btnStart_Click(object sender, EventArgs e)
        {
            streamVideo = true;

            GetCameraProperties();
            GetAvailableBackends();

            // Grab Event
            capture.ImageGrabbed += Capture_ImageGrabbed;
            capture.Start();
            timer1.Enabled = true;
        }

        private void Capture_ImageGrabbed(object sender, EventArgs e)
        {
            var frameSize = new System.Drawing.Size(1920, 1080);
            now = DateTime.Now;
            if (streamVideo)
            {
                Mat frame = new Mat();
                capture.Retrieve(frame);
                CvInvoke.Resize(frame, frame, frameSize);
                pictureBox1.Image = frame.ToBitmap();
                ts = now - last;
                frame.Dispose();
            }

            last = now;
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            streamVideo = false;
        }

        public void GetCameraProperties()
        {
            foreach (var prop in Enum.GetValues(typeof(CapProp)))
            {
                double propVal = capture.Get((CapProp)prop);

                if (propVal != -1)
                {
                    textBox1.AppendText(prop.ToString() + ": " +
                        propVal.ToString() + "\r\n");
                }
            }
            textBox1.AppendText("\r\n");
        }

        public void GetAvailableBackends()
        {
            Backend[] backends = CvInvoke.WriterBackends;

            foreach (Backend backEnd in backends)
            {
                textBox1.AppendText(backEnd.Name + " (" + backEnd.ID + ")\r\n");
            }
        }

        public void CalculateFPS()
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(ts.TotalMilliseconds > 0.0)
            {
                double fpsNew=1/(0.001*ts.TotalMilliseconds);
                fps = 0.9 * fpsOld + 0.1 * fpsNew;
                label2.Text = fps.ToString("F0") + " FPS";
                fpsOld = fps;
            }
        }


    }
}

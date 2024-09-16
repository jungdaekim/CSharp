using static EventsDelagatesDemo.ImageManipulation;

namespace EventsDelagatesDemo
{
    public partial class Form1 : Form
    {
        Bitmap newFile;

        ImageManipulation modifyRGB = new ImageManipulation();
        FileOperations getFile = new FileOperations();

        public Form1()
        {
            InitializeComponent();

            modifyRGB.ImageFinished += OnImageFinished;
        }

        public void DisplayImage(Bitmap b, int window)
        {
            if(window==1)
            {
                pictureBox1.Image = b;
            }
            else if(window==2)
            {
                pictureBox2.Image = b;
            }
            else
            {
                pictureBox1.Image = b;
                pictureBox2.Image= b;   
            }
            btnManipulate.Enabled = true;
        }
        private void btnOpen_Click(object sender, EventArgs e)
        {
            newFile = getFile.OpenFile();

            DisplayImage(newFile, 3);
        }

        private void OnImageFinished(object sender, ImageEventArgs e)
        {
            DisplayImage(e.bmp, 2);
            label1.Text = "Event Handler Received";
        }
        private void btnManipulate_Click(object sender, EventArgs e)
        {
            modifyRGB.Manipulate(newFile);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

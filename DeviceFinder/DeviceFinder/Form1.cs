using System.Management;

namespace DeviceFinder
{
    public partial class Form1 : Form
    {
        List<string> deviceNames = new List<string>();
        string deviceProperty = "Caption";
        public Form1()
        {
            InitializeComponent();
        }

        #region Methods
        public List<string> GetAllDevices()
        {
            List<string> devicenames=new List<string>();

            using (var searcher = new ManagementObjectSearcher("SELECT * FROM " +
                "Win32_PnPEntity"))
            {
                foreach(var device in searcher.Get())
                {
                    if (device[deviceProperty]!=null)
                    {
                       devicenames.Add(device[deviceProperty].ToString());
                    }
                }
            }

            return devicenames;
        }
        #endregion

        #region Event Handlers
        private void btnAll_Click(object sender, EventArgs e)
        {
            deviceNames = GetAllDevices();
            deviceNames.Sort();
            foreach(string device in deviceNames)
            {
                textBox1.AppendText(device + "\r\n");
            }
            textBox1.AppendText("\r\n" + "No. of devices = " + deviceNames.Count.ToString() + "\r\n");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion


    }
}

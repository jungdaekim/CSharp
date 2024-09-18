
using System.IO.Ports;
using System.Management;

namespace HelloComPorts
{
    public partial class Form1 : Form
    {
        public List<string> portnames = new List<string>();
        public Form1()
        {
            InitializeComponent();

            portnames = GetPorts();
            foreach (string port in portnames)
            {
                textBox1.AppendText(port + "\r\n");
            }
        }

        public List<string> GetPorts()
        {
            // This searches all the properties of the Plug and Play devices
            // using "Win32_PnPEntity". The "caption" is the text description of
            // the COM port object.

            // Search all Plug and Play entities where the caption hasa ("COM" plus
            // any number of leading & lagging characters
            using (var searcher = new ManagementObjectSearcher("SELECT * FROM " +
                "Win32_PnPEntity WHERE Caption like '%(COM%'"))
            {
                // This gets the simple port names, such as "COM4"
                string[] portnames = SerialPort.GetPortNames();

                // This gets the caption/description of the found ports
                var ports = searcher.Get().Cast<ManagementBaseObject>().ToList().Select(p => p["Caption"].ToString());
                List<string> portList=portnames.Select(n=>n+" - "+ports.FirstOrDefault(searcher=>searcher.Contains(n))).ToList();
            
                return portList;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

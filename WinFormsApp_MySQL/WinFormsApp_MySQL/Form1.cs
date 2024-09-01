using MySql.Data.MySqlClient;
using System.Data;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace WinFormsApp_MySQL
{
    public partial class Form1 : Form
    {
        #region -> �Ϲݺ�������

        private string _HostName = "";    //서버연결방식 도메인
        private string _ServerName = "";  //서버연결방식 아이피
        private string _CONNECT = "";    //도메인인지 아이피인지 여부
        private string _ID = "";              //로그인아이디
        private string _PWD = "";          //패스워드
        private string _PORT = "";          //포트
        private string _DATABASE = "";   //데이터베이스명

        private bool isTested = false;     //저장전 연결여부 테스트 여부

        private string Path = "";  //Mysql 서버정보가 저장될 xml경로

        #endregion
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text == "")
            {
                MessageBox.Show("DataBase Server를 입력해 주십시오..");
                this.textBox1.Focus();
                return;
            }

            if (this.textBox2.Text == "")
            {
                MessageBox.Show("Port번호를 입력해 주십시오.");
                this.textBox2.Focus();
                return;
            }

            if (this.textBox3.Text == "")
            {
                MessageBox.Show("Database를 입력해 주십시오.");
                this.textBox3.Focus();
                return;
            }

            if (this.textBox4.Text == "")
            {
                MessageBox.Show("ID를 입력해 주십시오..");
                this.textBox4.Focus();
                return;
            }

            if (this.textBox5.Text == "")
            {
                MessageBox.Show("Password를 입력해 주십시오..");
                this.textBox5.Focus();
                return;
            }

            _HostName = this.textBox1.Text;
            _ServerName = this.textBox1.Text;
            _PORT = this.textBox2.Text;
            _DATABASE = this.textBox3.Text;
            _ID = this.textBox4.Text;
            _PWD = this.textBox5.Text;

            this.DBConnectTest(_HostName, _PORT, _DATABASE, _ID, _PWD);
        }

        //DBConnectTest �޼ҵ�
        private void DBConnectTest(string hostname, string port, string database, string id, string pwd)
        {
            DataSet dsAll = new DataSet();
            DataSet ds = new DataSet();

            //연결을 아이피로 할것인지 도메인으로 할것인지 여부를 체크하여 서버네임을 가져온다.        
            if (this.checkBox1.Checked == true)
            {
                _ServerName = hostname;
            }
            else
            {
                IPHostEntry hostEntry = Dns.GetHostEntry(hostname);
                foreach (IPAddress ip in hostEntry.AddressList)
                {
                    Console.WriteLine(ip);
                    _ServerName = ip.ToString();
                }
            }

            _PORT = port;
            _DATABASE = database;
            _ID = id;
            _PWD = pwd;

            StringBuilder _strArg = new StringBuilder("");
            _strArg.Append("Server = ");           // SQL
            _strArg.Append(_ServerName);        // 서버
            _strArg.Append(";Port = ");
            _strArg.Append(_PORT);                 // 포트
            _strArg.Append(";Database = ");
            _strArg.Append(_DATABASE);          // 데이터베이스
            _strArg.Append(";Uid = ");
            _strArg.Append(_ID);                     // ID
            _strArg.Append(";Pwd = ");
            _strArg.Append(_PWD);                 // PWD
            _strArg.Append(";");
            MySqlConnection conn = new MySqlConnection(_strArg.ToString());

            try
            {
                conn.Open();
                MessageBox.Show("DB connection is possible.. DB 접속이 가능합니다.");

                /////MES 데이터베이스에서 데이터를 가져온다.
                //MySqlCommand cmd = conn.CreateCommand();
                //string sql = "SELECT * FROM 테이블명";
                //cmd.CommandText = sql;
                //MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                //adp.Fill(ds, "t_category");

                //테스트 여부를 true로 변경한다.
                isTested = true;
            }
            catch (Exception Ex)
            {
                conn.Close();
                MessageBox.Show("DB connection is impossible..DB 접속이 불가능합니다..");
                isTested = false;
            }
            finally
            {
                conn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isTested)
                {
                    MessageBox.Show("Execute DB link test first..데이터베이스 연결테스트를 먼저 실행해주세요..");
                    return;
                }

                if (this.checkBox1.Checked == true)
                {
                    _CONNECT = "1";
                }

            FileStream fs = new FileStream(Path, System.IO.FileMode.Create);
            XmlTextWriter XTW = new XmlTextWriter(fs, System.Text.Encoding.Unicode);
            XTW.Formatting = System.Xml.Formatting.Indented;        //들여쓰기

            XTW.WriteStartDocument();
            XTW.WriteStartElement("Config");

            XTW.WriteElementString("Server", _HostName);
            XTW.WriteElementString("CONNECT", _CONNECT);
            XTW.WriteElementString("PORT", _PORT);
            XTW.WriteElementString("DATABASE", _DATABASE);
            XTW.WriteElementString("ID", _ID);
            XTW.WriteElementString("PWD", _PWD);
            XTW.WriteEndElement();
            XTW.WriteEndDocument();

            XTW.Close();

            MessageBox.Show("데이터베이스 정보가 저장되었습니다..");
             }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());  
            }
}
    }
}

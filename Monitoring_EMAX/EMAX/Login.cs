using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;
using System.Security.Cryptography;
using System.Drawing.Text;
using System.IO;
using EMAX_Monitoring.Properties;
using System.Net;
using System.Reflection;
using System.Diagnostics;
using System.Xml;
using System.Configuration;
using System.Threading;
using DevExpress.XtraEditors;

namespace EMAX_Monitoring
{
    public partial class Login : DevExpress.XtraEditors.XtraForm
    {
        public static string log_no { get; private set; }
        ReturnStruct ret = new ReturnStruct();
        
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            try
            {
                txtId.Text = Settings.Default["Login_ID"].ToString();
                chkID.Checked = Settings.Default.chk_id;

                string sFTPString = Configurations.GetConfig("FTPstring");

                string[] FTP = sFTPString.Split(';');
                string sURL = FTP[0].Substring(0, FTP[0].IndexOf(":"));

                //string sVersion = Get_Version("Version", "Version.txt", Application.StartupPath + @"\");
                //string sServerVersion = Get_Version("ServerVersion", "Version.txt", "http://" + sURL + "/EMAX_Monitoring/");
                //label_Version.Text = "Version : " + sVersion + "\n ServerVersion : " + sServerVersion;

                if (txtId.Text != "")
                {
                    this.Activate();
                    txtPw.Select();
                }
                else
                {
                    txtId.Select();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private string Get_Version(string Kind , string Text_File, string Path)
        {
            try
            {
                string Version = (Kind == "Version") ? VersionCheck.getServerVersion(Text_File, Path) : VersionCheck.getServerVersion(Text_File, Path);

                return Version;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return null;
            }
        }

        public string SHA256Hash(string data)
        {

            SHA256 sha = new SHA256Managed();
            byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(data));
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }
            return stringBuilder.ToString();
        }
        //복호화

        private void btn_Login_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtId.Text.Trim() == "")
                {
                    XtraMessageBox.Show("아이디 입력해주세요.");
                    return;
                }
                if (txtPw.Text.Trim() == "")
                {
                    XtraMessageBox.Show("비밀번호 입력해주세요.");
                    return;
                }

                bool Login_bool = Login_info();

                if (Login_bool == true)
                {
                    Insert_Login_Info();

                    if(chkID.Checked == true)
                    {
                        Settings.Default["Login_ID"] = txtId.Text;
                        Settings.Default["chk_id"] = true;
                    }
                    else
                    {
                        Settings.Default["Login_ID"] = "";
                        Settings.Default["chk_id"] = false;
                    }
                    Settings.Default.Save();

                    SplashScreenManager.ShowForm(this.FindForm(), typeof(BaseWaitForm), true, true, false);
                    SplashScreenManager.Default.SetWaitFormCaption("메뉴");
                    SplashScreenManager.Default.SetWaitFormDescription("메뉴 조회 중...");

                    this.Hide();

                    //Form form = new MainForm();
                    Form form = new Monitoring();
                    form.AutoScaleMode = AutoScaleMode.Dpi;
                    form.WindowState = FormWindowState.Maximized;
                    form.Show();

                    SplashScreenManager.CloseForm(false);

                }
                else
                {
                    XtraMessageBox.Show("아이디 또는 비밀번호 확인 바랍니다.");
                    txtPw.Text = "";
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        public bool Login_info(string id)
        {
            txtId.Text = id;
            return Login_info();
        }

        public bool Login_info()
        {
            try
            {
                SqlParam sp = new SqlParam("sp_ComLogin");
                sp.AddParam("ID", txtId.Text);
                sp.AddParam("PW", SHA256Hash(txtPw.Text));

                ret = DbHelp.Proc_Search(sp);

                if(ret.ReturnChk == 0)
                {
                    if (ret.ReturnDataSet.Tables[0].Rows.Count == 0)
                        return false;

                    GlobalValue.sUserID = ret.ReturnDataSet.Tables[0].Rows[0]["User_Code"].ToString();
                    GlobalValue.sUserNm = ret.ReturnDataSet.Tables[0].Rows[0]["User_Name"].ToString();
                }
                else
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        // 로그인 정보 Insert
        public void Insert_Login_Info()
        {
            string my_ip = Get_MyIP();
            string my_ex_ip = Get_Ext_Ip();

            SqlParam sp = new SqlParam("sp_Log");
            sp.AddParam("Kind", "I");
            sp.AddParam("Login_IP", my_ip);
            sp.AddParam("External_IP", my_ex_ip);
            sp.AddParam("Login_Host_Name", Dns.GetHostName());
            sp.AddParam("User_Code", GlobalValue.sUserID);

            ReturnStruct temp_ret = new ReturnStruct();
            temp_ret = DbHelp.Proc_Save(sp);
            if (temp_ret.ReturnChk != 0)
            {
                XtraMessageBox.Show(temp_ret.ReturnMessage);
                return;
            }

            log_no = temp_ret.ReturnDataSet.Tables[0].Rows[0][0].NullString();
        }

        public string Get_MyIP()
        {
            IPHostEntry host = Dns.GetHostByName(Dns.GetHostName());
            string myip = host.AddressList[0].ToString();
            return myip;
        }

        public string Get_Ext_Ip()
        {
            try
            {
                string ext_ip = new WebClient().DownloadString("http://ipinfo.io/ip").Trim();
                return ext_ip;
            }
            catch (Exception)
            {
                return "";
            }
        }
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            try
            {
                Application.Exit();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private Point mousePoint;
        private void SplashScreen1_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                Location = new Point(this.Left - (mousePoint.X - e.X),
                    this.Top - (mousePoint.Y - e.Y));
            }
        }

      

        private void SplashScreen1_MouseDown(object sender, MouseEventArgs e)
        {
            mousePoint = new Point(e.X, e.Y);
        }

        private void txtPw_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btn_Login_Click(sender, e);
            }
        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {
            panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            e.Graphics.DrawRectangle(new Pen(Color.FromArgb(100, 53, 74, 107), 10), panelControl1.ClientRectangle);
        }


        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void btn_Config_Click(object sender, EventArgs e)
        {
            Change_Config config = new Change_Config();
            config.Show();
        }
    }
}
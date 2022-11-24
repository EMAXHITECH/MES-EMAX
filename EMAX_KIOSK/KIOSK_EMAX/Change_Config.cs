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
using KIOSK_EMAX.Properties;
using System.Net;
using System.Reflection;
using System.Diagnostics;
using System.Xml;
using System.Configuration;
using System.Threading;
using DevExpress.XtraEditors;
using System.Data.SqlClient;

namespace KIOSK_EMAX
{
    public partial class Change_Config : DevExpress.XtraEditors.XtraForm
    {
        private static SqlConnection conn = null;

        public Change_Config()
        {
            InitializeComponent();
        }
        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void btn_Select_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(BaseWaitForm), true, true, false);
            SplashScreenManager.Default.SetWaitFormCaption("주소 변경");
            SplashScreenManager.Default.SetWaitFormDescription("변경 중...");
            if (xtraTabControl_Config.SelectedTabPageIndex == 0)
            {
                try
                {
                    string sDBConnString = "Server=" + txt_IP.Text + "," + txt_Port.Text + ";Integrated Security=false;Initial ";
                    sDBConnString += "Catalog=" + txt_DB.Text + ";";
                    sDBConnString += "User ID=" + txt_ID.Text + ";";
                    sDBConnString += "Password=" + txt_PW.Text;

                    conn = new SqlConnection(sDBConnString);

                    conn.Open();

                    conn.Close();
                    SplashScreenManager.CloseForm(false);

                    Configurations.SetConfig("DBConnstring", sDBConnString);

                    DbHelp.Clear();
                    XtraMessageBox.Show("데이터베이스 접속 주소가 변경되었습니다");
                }
                catch (Exception ex)
                {
                    SplashScreenManager.CloseForm(false);
                    XtraMessageBox.Show("데이터베이스 접속 정보가 잘못 되었습니다");
                    return;
                }
            }
            else
            {
                string sFTPstring = txt_FTPIP.Text + ":" + txt_FTPPort.Text;
                sFTPstring += ";" + txt_FTPID.Text + ";" + txt_FTPPW.Text;

                try
                {
                    FtpWebRequest Ftpreq = (FtpWebRequest)WebRequest.Create("ftp://" + txt_FTPIP.Text + ":" + txt_FTPPort.Text);
                    Ftpreq.Credentials = new NetworkCredential(txt_FTPID.Text, txt_FTPPW.Text);
                    Ftpreq.Method = WebRequestMethods.Ftp.ListDirectory;

                    Ftpreq.GetResponse();

                    SplashScreenManager.CloseForm(false);

                    Configurations.SetConfig("FTPstring", sFTPstring);

                    DbHelp.Clear();
                    XtraMessageBox.Show("파일업로드 접속 주소가 변경되었습니다");
                }
                catch(Exception ex)
                {
                    SplashScreenManager.CloseForm(false);
                    XtraMessageBox.Show("파일업로드 접속 정보가 잘못 되었습니다");
                    return;
                }   
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Change_Config_Load(object sender, EventArgs e)
        {
            //데이터베이스 접속 주소
            string sDBConnString = Configurations.GetConfig("DBConnstring");

            if (!string.IsNullOrWhiteSpace(sDBConnString))
            {
                string[] DBConn = sDBConnString.Split(';');

                string sUrl = DBConn[0].Substring(DBConn[0].IndexOf("=") + 1);
                string sIP = sUrl.Substring(0, sUrl.IndexOf(","));
                string sPort = sUrl.Substring(sUrl.IndexOf(",") + 1);

                string sDB = DBConn[2].Substring(DBConn[2].IndexOf("=") + 1);
                string sID = DBConn[3].Substring(DBConn[3].IndexOf("=") + 1);
                string sPW = DBConn[4].Substring(DBConn[4].IndexOf("=") + 1);

                txt_IP.Text = sIP;
                txt_Port.Text = sPort;
                txt_ID.Text = sID;
                txt_PW.Text = sPW;
                txt_DB.Text = sDB;
            }

            txt_IP.Focus();

            //FTP 접속 주소

            //string sFTPString = Configurations.GetConfig("FTPstring");

            //string[] FTP = sFTPString.Split(';');
            //string sFTPIP = FTP[0].Substring(0, FTP[0].IndexOf(":"));
            //string sFTPPort = FTP[0].Substring(FTP[0].IndexOf(":") + 1);

            //txt_FTPIP.Text = sFTPIP;
            //txt_FTPPort.Text = sFTPPort;
            //txt_FTPID.Text = FTP[1];
            //txt_FTPPW.Text = FTP[2];
        }
    }
}
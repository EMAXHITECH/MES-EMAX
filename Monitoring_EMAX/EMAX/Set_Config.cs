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
using System.Data.SqlClient;

namespace EMAX_Monitoring
{
    public partial class Set_Config : DevExpress.XtraEditors.XtraForm
    {
        private static SqlConnection conn = null;

        private CheckEdit[] Check_Mon;

        public Set_Config()
        {
            InitializeComponent();
        }
        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btn_Select_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(BaseWaitForm), true, true, false);
            SplashScreenManager.Default.SetWaitFormCaption("모니터링 환경설정");
            SplashScreenManager.Default.SetWaitFormDescription("설정중...");

            try
            {
                string sMonitoring = txt_Timer.Text + ";";
                for (int i = 0; i < Check_Mon.Length; i++)
                    sMonitoring += Check_Mon[i].EditValue.ToString() + ";";

                Configurations.SetConfig("Monitoring", sMonitoring);

                SplashScreenManager.CloseForm(false);
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Change_Config_Load(object sender, EventArgs e)
        {
            Check_Mon = new CheckEdit[] { check_1, check_2, check_3, check_4, check_5 };

            try
            {
                SqlParam sp = new SqlParam("sp_Monitoring_Set");
                sp.AddParam("Kind", "S");

                ReturnStruct ret = DbHelp.Proc_Search(sp);

                if(ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                DataTable dt_M = ret.ReturnDataSet.Tables[0];

                for (int i = dt_M.Rows.Count; i < Check_Mon.Length; i++)
                {
                    Check_Mon[i].Visible = false;
                }

                for(int i = 0; i < dt_M.Rows.Count; i++)
                {
                    Check_Mon[i].Text = dt_M.Rows[i]["Monitoring"].NullString();
                }
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }

            string sMonitoring = Configurations.GetConfig("Monitoring");

            if (!string.IsNullOrWhiteSpace(sMonitoring))
            {
                string[] sMon = sMonitoring.Split(';');

                txt_Timer.Text = sMon[0];
                for (int i = 0; i < Check_Mon.Length; i++)
                    Check_Mon[i].EditValue = sMon[i + 1].ToString();
            }
            else
            {
                txt_Timer.Text = "60";
                for (int i = 0; i < Check_Mon.Length; i++)
                    Check_Mon[i].Checked = false;
            }
        }

        private void btn_IP_Click(object sender, EventArgs e)
        {
            Change_Config config = new Change_Config();
            if(config.ShowDialog() == DialogResult.OK)
            {
                //XtraMessageBox.Show("IP가 재설정되어서 프로그램을 재실행합니다");
                btn_Select_Click(null, null);
            }
        }
    }
}
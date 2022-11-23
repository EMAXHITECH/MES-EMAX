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
    public partial class Change_PW : DevExpress.XtraEditors.XtraForm
    {
        public string sID = "";
        ReturnStruct ret = new ReturnStruct();
        
        public Change_PW()
        {
            InitializeComponent();
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


        private void Change_PW_Load(object sender, EventArgs e)
        {
            txtId.Text = sID;
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void btn_Select_Click(object sender, EventArgs e)
        {
            try
            {
                if(txt_ChgPw.Text != txt_ChgPwChk.Text)
                {
                    XtraMessageBox.Show("변경할 비밀번호가 동일하지 않습니다");
                    return;
                }

                SqlParam sp = new SqlParam("sp_Pw_Change");
                sp.AddParam("ID", sID);
                sp.AddParam("PW", SHA256Hash(txtPw.Text));
                sp.AddParam("ChgPW", SHA256Hash(txt_ChgPw.Text));

                ret = DbHelp.Proc_Save(sp);

                if(ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                XtraMessageBox.Show("비밀번호 변경이 완료되었습니다. 재로그인해 주세요");

                this.DialogResult = DialogResult.OK;

                this.Close();
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }
    }
}
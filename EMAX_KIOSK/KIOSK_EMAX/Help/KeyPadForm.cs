using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace KIOSK_EMAX
{
    public partial class KeyPadForm : DevExpress.XtraEditors.XtraForm
    {
        public string sNum = "";
        bool bFast = true;
        public KeyPadForm(int Num_Length = 0)
        {
            InitializeComponent();

            txt_Num.Properties.MaskSettings.MaskExpression = "n" + Num_Length.NumString();
        }


        private void KeyPadForm_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(sNum))
                sNum = "0";

            txt_Num.Text = sNum;
        }

        private void btn_Click(object sender, EventArgs e)
        {

            SimpleButton btn = sender as SimpleButton;

            string sbtn = btn.Text;

            if (bFast == true)
            {
                sNum = "0";
                bFast = false;
            }

            if (sNum == "0")
            {
                sNum = string.Format("{0:0.#}",  sbtn); 
            }
            else
            {
                sNum = string.Format("{0:0.#}", sNum + sbtn);
            }

            txt_Num.Text = sNum;
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btn_Del_Click(object sender, EventArgs e)
        {
            if (sNum == "0")
                return;

            if (sNum.Length == 1)
            {
                sNum = "0";
            }
            else
            {
                sNum = sNum.Substring(0, sNum.Length - 1);
            }

            txt_Num.Text = sNum;
        }
    }
}
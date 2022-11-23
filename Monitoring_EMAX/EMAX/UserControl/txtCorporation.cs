using EMAX_Monitoring;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevExpress.XtraEditors
{
    public partial class CorporationNumTextEdit : TextEdit, IMaskTextEdit
    {
        public string UnMaskText
        {
            set
            {
                Text = getBizNumFomatter(value);
            }
            get
            {
                return Text.Replace("-", "");
            }
        }

        public CorporationNumTextEdit()
        {
            this.Validating += (s, e) =>
            {

            };
            this.EditValue = "";
            // this.Leave += new System.EventHandler(this.txtBusinessNum_Leave);
            this.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.Properties.Mask.EditMask = "(\\(\\d\\d\\d\\) )?\\d{1,4} \\d{1,2}-\\d{1,6} \\d{1,1}";
            this.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.Properties.MaxLength = 16;
            this.UnMaskText = "";
        }

        public bool CheckValue(out string message)
        {
            if (!CheckValue())
            {
                message = "유효하지않은 법인번호입니다.";
                return false;
            }

            message = null;
            return true;
        }

        public bool CheckValue()
        {
            return true;//CommonMethod.checkBusinessNumber(Text);
        }

        public static string getBizNumFomatter(string num)
        {
            var formatNum = "";
            if (num == null) return formatNum;
            num = num.Replace("-", "").Replace(" ", "");
            if (num.Length == 10)
            {
                formatNum = num.Substring(0, 3) + "-" + num.Substring(3, 2) + "-" + num.Substring(5);
            }

            return formatNum;
        }

        // 법인 번호 체크로 변경 필요
        private void txtCorporationNum_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.Text))
            {
                ReturnStruct ret = new ReturnStruct();
                DataTable table = new DataTable();
                SqlParam sp = new SqlParam("sp_Business_No_Check");
                sp.AddParam("Company_No", this.Text.Replace("-", ""));

                ret = DbHelp.Proc_Search(sp);
                if (ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                table = ret.ReturnDataSet.Tables[0];

                if (table == null || table.Rows.Count < 1)
                    return;

                //if (txtCustCode.Text == lst[0].CUST_CODE) return;

                if (table.Rows.Count > 0)
                {
                    XtraMessageBox.Show("동일한 사업자번호가 있습니다.");
                    this.Focus();
                    this.SelectAll();
                    return;
                }
            }
        }
    }

    public interface IControlEx2
    {
        bool CheckValue();
        bool CheckValue(out string message);
    }

    public interface IMaskTextEdit2 : IControlEx
    {
        string UnMaskText { set; get; }
    }
}
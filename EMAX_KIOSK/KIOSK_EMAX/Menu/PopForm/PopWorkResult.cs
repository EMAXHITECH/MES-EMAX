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
using DevExpress.XtraEditors.Controls;

namespace KIOSK_EMAX
{
    public partial class PopWorkResult : DevExpress.XtraEditors.XtraForm
    {
        private decimal dWork_Qty = 0;
        public decimal dQty = 0, dBad_Qty = 0;
        public string sOver = "N";

        public DataRow dr = null;

        public PopWorkResult()
        {
            InitializeComponent();
        }

        private void PopWorkResult_Load(object sender, EventArgs e)
        {
            if (dr == null)
            {
                this.DialogResult = DialogResult.No;
                this.Close();
            }
            else
            {
                txt_ItemCode.Text = dr["Item_Code"].ToString();
                txt_ItemName.Text = dr["Item_Name"].ToString();
                txt_SSize.Text = dr["SSize"].ToString();
                txt_ProcessName.Text = dr["Process_Name"].ToString();

                txt_Qty.EditValueChanged -= txt_Qty_EditValueChanged;
                txt_Qty.Text = dr["Qty"].NumString();
                txt_Qty.EditValueChanged += txt_Qty_EditValueChanged;
                txt_BadQty.Text = "0";

                dWork_Qty = decimal.Parse(dr["Qty"].NumString());
            }
        }

        private void txt_Qty_EditValueChanged(object sender, EventArgs e)
        {
            dQty = decimal.Parse(txt_Qty.Text);

            if(dWork_Qty < dQty)
            {
                //XtraMessageBox.Show("지시 수량보다 더 많이 등록할 수 없습니다");
                if (XtraMessageBox.Show("실적수량이 지시수량보다 많습니다. 등록하시겠습니까?", "실적", MessageBoxButtons.YesNo) == DialogResult.No)
                    dQty = dWork_Qty;
                else
                    sOver = "Y";
            }

            txt_Qty.EditValueChanged -= txt_Qty_EditValueChanged;
            txt_Qty.Text = dQty.ToString();
            txt_Qty.EditValueChanged += txt_Qty_EditValueChanged;
        }

        private void txt_Qty_Click(object sender, EventArgs e)
        {
            KeyPadForm keyPad = new KeyPadForm();
            keyPad.StartPosition = FormStartPosition.CenterParent;
            keyPad.sNum = txt_Qty.Text;
            if (keyPad.ShowDialog() == DialogResult.OK)
                txt_Qty.Text = keyPad.sNum;
        }

        private void txt_BadQty_Click(object sender, EventArgs e)
        {
            KeyPadForm keyPad = new KeyPadForm();
            keyPad.StartPosition = FormStartPosition.CenterParent;
            keyPad.sNum = txt_BadQty.Text;
            if (keyPad.ShowDialog() == DialogResult.OK)
                txt_BadQty.Text = keyPad.sNum;
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("수정한 정보가 있습니다. 저장하시겠습니까?", "저장", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                dQty = decimal.Parse(txt_Qty.Text);
                dBad_Qty = decimal.Parse(txt_BadQty.Text);
                this.DialogResult = DialogResult.Yes;
            }
            else
                this.DialogResult = DialogResult.No;

            this.Close();
        }
    }
}
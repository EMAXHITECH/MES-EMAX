using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace MES
{
    public partial class regEquipReq : BaseReg
    {
        ReturnStruct ret = new ReturnStruct();
        public regEquipReq()
        {
            InitializeComponent();
        }

        private void regEquipReq_Load(object sender, EventArgs e)
        {
            dt_ReqDate.Focus();
        }

        private void btn_Select_Click(object sender, EventArgs e)
        {
            PopHelpForm HelpForm = new PopHelpForm("EquipReq", "sp_Help_EquipReq", "", "N");
            HelpForm.sNotReturn = "Y";
            if (HelpForm.ShowDialog() == DialogResult.OK)
            {
                Search_Data(HelpForm.sRtCode);
            }
        }

        private void btn_Insert_Click(object sender, EventArgs e)
        {
            Save_Data();
            Search_Data();
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt_ReqNo.Text))
            {
                SqlParam sp = new SqlParam("sp_regEquipReq");
                sp.AddParam("Kind", "D");
                sp.AddParam("Req_No", txt_ReqNo.Text);

                ret = DbHelp.Proc_Save(sp);

                if (ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }
                btn_Delete.sCHK = "Y";
            }
            DbHelp.Clear_Panel(panel_H);
            DbHelp.Clear_Panel(panelControl3);
            txt_ReqNo.Enabled = true;
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            Save_Data();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            if (btn_Close.Result_Update == DialogResult.Yes)
            {
                btn_Save_Click(null, null);
            }

            MainForm.MainTab.TabPages.Remove(MainForm.MainTab.SelectedTabPage);
        }

        private void Search_Data(string code = "")
        {
            SqlParam sp = new SqlParam("sp_regEquipReq");
            sp.AddParam("Kind", "S");
            sp.AddParam("Req_No", code);

            ret = DbHelp.Proc_Search(sp);

            if(ret.ReturnChk != 0)
            {
                XtraMessageBox.Show(ret.ReturnMessage);
                return;
            }

            if(!string.IsNullOrWhiteSpace(code))
            {
                DataRow row = DbHelp.Fill_Table(ret.ReturnDataSet.Tables[0]).Rows[0];

                txt_ReqNo.Text = row["Req_No"].NullString();
                dt_ReqDate.Text = row["Req_Date"].NullString();
                txt_EquipCode.Text = row["Equip_Code"].NullString();
                txt_Custom.Text = row["Custom_Code"].NullString();
                dt_Repair.Text = row["Repair_Date"].NullString();
                txt_RepairAmt.EditValue = Convert.ToDecimal(row["Repair_Amt"].NumString());
                txt_ReqMemo.Text = row["Req_Memo"].NullString();

                txt_ReqNo.Enabled = false;
            }
            else
            {
                DbHelp.Clear_Panel(panel_H);
                DbHelp.Clear_Panel(panelControl3);
                txt_ReqNo.Enabled = true;
            }
        }

        private void Save_Data()
        {
            if (Data_Check())
            {
                SqlParam sp = new SqlParam("sp_regEquipReq");
                sp.AddParam("Kind", "I");

                sp.AddParam("Req_No", txt_ReqNo.Text);
                sp.AddParam("Req_Date", dt_ReqDate.Text.Replace("-", ""));
                sp.AddParam("Equip_Code", txt_EquipCode.Text);
                sp.AddParam("Req_Memo", txt_ReqMemo.Text);
                sp.AddParam("Repair_Date", dt_Repair.Text.Replace("-", ""));
                sp.AddParam("Custom_Code", txt_Custom.Text);
                sp.AddParam("Repair_Amt", txt_RepairAmt.EditValue.NumString());

                ret = DbHelp.Proc_Save(sp);

                if (ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }
                btn_Save.sCHK = "Y";
            }
        }

        private bool Data_Check()
        {
            if (string.IsNullOrWhiteSpace(txt_ReqNo.Text))
            {
                XtraMessageBox.Show("의뢰번호를 입력해주시길 바랍니다.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(dt_ReqDate.Text))
            {
                XtraMessageBox.Show("의뢰일자를 입력해주시길 바랍니다.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txt_EquipName.Text))
            {
                XtraMessageBox.Show("설비정보를 입력해주시길 바랍니다.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txt_CustomNM.Text))
            {
                XtraMessageBox.Show("거래처를 입력해주시길 바랍니다.");
                return false;
            }
            return true;
        }

        #region 상속 함수

        protected override void btnSelect()
        {
            btn_Select.PerformClick();
        }

        protected override void btnInsert()
        {
            btn_Insert.PerformClick();
        }

        protected override void btnClose()
        {
            btn_Close.PerformClick();
        }

        protected override void btnSave()
        {
            btn_Save.PerformClick();
        }

        protected override void btnDelete()
        {
            btn_Delete.PerformClick();
        }

        #endregion

        #region 코드 검색
        private void txt_Custom_EditValueChanged(object sender, EventArgs e)
        {
            txt_CustomNM.Text = PopHelpForm.Return_Help("sp_Help_Custom", txt_Custom.Text);
        }

        private void txt_Custom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txt_Custom_Properties_ButtonClick(sender, null);
        }

        private void txt_Custom_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_CustomNM.Text))
            {
                PopHelpForm Helpform = new PopHelpForm("Custom", "sp_Help_Custom", txt_Custom.Text, "N");
                if (Helpform.ShowDialog() == DialogResult.OK)
                {
                    txt_Custom.Text = Helpform.sRtCode;
                    txt_CustomNM.Text = Helpform.sRtCodeNm;
                }
            }
        }

        private void txt_EquipCode_EditValueChanged(object sender, EventArgs e)
        {
            txt_EquipName.Text = PopHelpForm.Return_Help("sp_Help_Equip", txt_EquipCode.Text);
        }

        private void txt_EquipCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txt_EquipCode_Properties_ButtonClick(sender, null);
        }

        private void txt_EquipCode_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            PopHelpForm HelpForm = new PopHelpForm("Equip", "sp_Help_Equip", txt_EquipCode.Text, "N");
            HelpForm.sNotReturn = "Y";
            if (HelpForm.ShowDialog() == DialogResult.OK)
            {
                if(HelpForm.drReturn != null && HelpForm.drReturn.Count() > 0)
                {
                    txt_EquipCode.Text = HelpForm.drReturn[0][0].NullString();
                    txt_EquipName.Text = HelpForm.drReturn[0][1].NullString();
                }
                else if(!string.IsNullOrWhiteSpace(HelpForm.sRtCode))
                {
                    txt_EquipCode.Text = HelpForm.sRtCode;
                    txt_EquipName.Text = HelpForm.sRtCodeNm;
                }
            }
        }
        #endregion
    }
}

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
using DevExpress.XtraEditors.Repository;
using System.IO;
using System.Net;

namespace MES
{
    public partial class regEquip : BaseReg
    {
        ReturnStruct ret = new ReturnStruct();
        DataSet ds = new DataSet();

        public regEquip()
        {
            InitializeComponent();
        }

        private void regEquip_Load(object sender, EventArgs e)
        {
            pictureEdit_Equip.Properties.ContextMenuStrip = new ContextMenuStrip();

            Grid_Set();

            Search_Data();
        }

        private void Grid_Set()
        {
            DbHelp.GridSet(Grid_EQSet, View_EQSet, "IP_Addr", "IP 주소", "100", false, true, true, true);
            DbHelp.GridSet(Grid_EQSet, View_EQSet, "IP_Info", "주소 정보", "100", false, true, true, true);
            DbHelp.GridSet(Grid_EQSet, View_EQSet, "Port", "Port", "100", false, true, true, true);
            DbHelp.GridSet(Grid_EQSet, View_EQSet, "Device", "Device No", "100", false, false, false, true);
            DbHelp.GridSet(Grid_EQSet, View_EQSet, "Device_Name", "Device 설명", "100", false, true, true, true);
            DbHelp.GridSet(Grid_EQSet, View_EQSet, "Process_Name", "공정", "100", false, false, false, true);
            DbHelp.GridSet(Grid_EQSet, View_EQSet, "Process_Code", "공정", "100", false, false, false, true);

            DbHelp.GridColumn_Help(View_EQSet, "Process_Name", "Y");
            RepositoryItemButtonEdit button_Help = (RepositoryItemButtonEdit)View_EQSet.Columns["Process_Name"].ColumnEdit;
            button_Help.Buttons[0].Click += new EventHandler(Grid_Help);
            View_EQSet.Columns["Process_Name"].ColumnEdit = button_Help;

            //DbHelp.GridColumn_NumSet(View_EQSet, "Port", 0);

            Grid_EQSet.DeleteRowEventHandler += new EventHandler(Delete_Row);

            View_EQSet.OptionsView.ShowAutoFilterRow = false;
            Grid_EQSet.AddRowYN = true;
            Grid_EQSet.PopMenuChk = true;
            Grid_EQSet.MouseWheelChk = true;
        }

        private void View_EQSet_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "Process_Name")
            {
                DataRow Code = PopHelpForm.Return_Help_Row("sp_Help_General_Code", e.Value.NullString(), "30030");

                View_EQSet.SetRowCellValue(e.RowHandle, "Process_Code", (Code == null) ? "" : Code[0].NullString());
            }
        }

        private void Grid_EQSet_EditorKeyPress(object sender, KeyPressEventArgs e)
        {
            if (View_EQSet.FocusedColumn.FieldName.Contains("Process") && e.KeyChar == 13)
                Grid_Help(sender, null);
        }

        private void Grid_Help(object sender, EventArgs e)
        {
            PopHelpForm popHelp = new PopHelpForm("General", "sp_Help_General", "30030", View_EQSet.GetFocusedRowCellValue("Process_Name").NullString(), "N");
            if (popHelp.ShowDialog() == DialogResult.OK)
            {
                View_EQSet.SetFocusedRowCellValue("Process_Code", popHelp.sRtCode);
                View_EQSet.SetFocusedRowCellValue("Process_Name", popHelp.sRtCodeNm);
            }
        }

        private void Delete_Row(object sender, EventArgs e)
        {

        }

        #region 함수

        //그리드 품목 조회
        private void Search_Data()
        {
            try
            {
                SqlParam sp = new SqlParam("sp_regEquip");
                sp.AddParam("Kind", "S");
                sp.AddParam("Equip_Code", txt_EquipCode.Text);

                ret = DbHelp.Proc_Search(sp);

                if (ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                DataRow Row = (ret.ReturnDataSet.Tables[0].Rows.Count > 0) ? ret.ReturnDataSet.Tables[0].Rows[0] : null;

                if (Row != null)
                {
                    txt_EquipNM.Text = Row["Equip_Name"].NullString();
                    txt_Kind.Text = Row["Equip_Part"].NullString();
                    txt_KindNM.Text = Row["Equip_PartNM"].NullString();
                    txt_Custom.Text = Row["Custom_Code"].NullString();
                    txt_CustomNM.Text = Row["Short_Name"].NullString();
                    dt_Make.Text = Row["Make_Date"].NullString();
                    txt_Maker.Text = Row["Custom_Code1"].NullString();
                    txt_MakerNM.Text = Row["Short_Name1"].NullString();
                    txt_Make_No.Text = Row["Make_No"].NullString();
                    txt_location.Text = Row["Equip_Pos"].NullString();
                    txt_LocationNM.Text = Row["Equip_PosNM"].NullString();
                    dt_Quit.Text = Row["Dis_Date"].NullString();
                    Memo_Equip.Text = Row["M_Memo"].NullString();

                    txt_RegDate.Text = Row["Reg_Date"].NullString();
                    txt_RegUser.Text = Row["Reg_User"].NullString();

                    txt_UpDate.Text = Row["Up_Date"].NullString();
                    txt_UpUser.Text = Row["Up_User"].NullString();

                    txt_EquipCode.Enabled = false;
                }
                else
                {
                    DbHelp.Clear_Panel(panel_H);
                    DbHelp.Clear_Panel(panel_M);

                    pictureEdit_Equip.Image = null;
                    pictureEdit_Equip.Refresh();

                    dt_Make.Text = "";
                    dt_Quit.Text = "";

                    txt_EquipCode.Enabled = true;
                }

                Grid_EQSet.DataSource = ret.ReturnDataSet.Tables[1];
                Grid_EQSet.RefreshDataSource();
                View_EQSet.BestFitColumns();

                ForMat.sBasic_Set(this.Name, txt_EquipCode);

                btn_Insert.sUpdate = "N";
                btn_Close.sUpdate = "N";

                if (txt_EquipCode.Enabled)
                    txt_EquipCode.Focus();
                else
                    txt_EquipNM.Focus();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }



        #endregion

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

        protected override void btnExcel()
        {
            btn_Excel.PerformClick();
        }
        #endregion

        #region 버튼 이벤트
        private void btn_Select_Click(object sender, EventArgs e)
        {
            PopHelpForm HelpForm = new PopHelpForm("Equip", "sp_Help_Equip", "", "N");
            HelpForm.sLevelYN = "Y";
            HelpForm.sNotReturn = "Y";
            btn_Select.clsWait.CloseWait();

            if (HelpForm.ShowDialog() == DialogResult.OK)
            {
                txt_EquipCode.Text = HelpForm.sRtCode;

                btn_Select.clsWait.ShowWait(this.FindForm());
                Search_Data();
                btn_Select.clsWait.CloseWait();
            }
        }

        private void btn_Insert_Click(object sender, EventArgs e)
        {
            if (btn_Insert.Result_Update == DialogResult.Yes)
            {
                if (!Save_Data())
                    return;
            }

            txt_EquipCode.Text = "";
            Search_Data();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (!Save_Data())
                return;

            btn_Save.sCHK = "Y";
            Search_Data();
        }

        private bool Save_Data()
        {
            try
            {
                if ((!txt_EquipCode.Enabled || DbHelp.Check_Val(txt_EquipCode)) && DbHelp.Check_Val(txt_EquipNM, txt_KindNM, txt_CustomNM))
                {
                    string sDevice = "", sIP_Addr = "", sIP_Info = "", sDevice_Name = "", sProcess_Code = "", sPort = "";

                    for(int i = 0; i < View_EQSet.RowCount; i++)
                    {
                        sDevice += View_EQSet.GetRowCellValue(i, "Device").ToString() + "_/";
                        sIP_Addr += View_EQSet.GetRowCellValue(i, "IP_Addr").ToString() + "_/";
                        sIP_Info += View_EQSet.GetRowCellValue(i, "IP_Info").ToString() + "_/";
                        sDevice_Name += View_EQSet.GetRowCellValue(i, "Device_Name").ToString() + "_/";
                        sProcess_Code += View_EQSet.GetRowCellValue(i, "Process_Code").ToString() + "_/";
                        sPort += View_EQSet.GetRowCellValue(i, "Port").NumString() + "_/";
                    }

                    SqlParam sp = new SqlParam("sp_regEquip");
                    sp.AddParam("Kind", "I");
                    sp.AddParam("Equip_Code", txt_EquipCode.Text);
                    sp.AddParam("Equip_Name", txt_EquipNM.Text);
                    sp.AddParam("Equip_Part", txt_Kind.Text);
                    sp.AddParam("Custom_Code", txt_Custom.Text);
                    sp.AddParam("Make_Date", dt_Make.Text.Replace("-", ""));
                    if (!string.IsNullOrWhiteSpace(txt_MakerNM.Text))
                        sp.AddParam("Custom_Code1", txt_Maker.Text);
                    sp.AddParam("Make_No", txt_Make_No.Text);
                    if (!string.IsNullOrWhiteSpace(txt_LocationNM.Text))
                        sp.AddParam("Equip_Pos", txt_location.Text);
                    sp.AddParam("Dis_Date", dt_Quit.Text.Replace("-", ""));
                    sp.AddParam("M_Memo", Memo_Equip.Text);
                    sp.AddParam("Reg_User", GlobalValue.sUserID);

                    sp.AddParam("IP_Addr", sIP_Addr);
                    sp.AddParam("IP_Info", sIP_Info);
                    sp.AddParam("Device", sDevice);
                    sp.AddParam("Device_Name", sDevice_Name);
                    sp.AddParam("Process_Code", sProcess_Code);
                    sp.AddParam("Port", sPort);

                    ret = DbHelp.Proc_Save(sp);

                    if (ret.ReturnChk != 0)
                    {
                        XtraMessageBox.Show(ret.ReturnMessage);
                        return false;
                    }

                    txt_EquipCode.Text = ret.ReturnDataSet.Tables[0].Rows[0][0].NullString();

                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }


        private void btn_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                SqlParam sp = new SqlParam("sp_regEquip");
                sp.AddParam("Kind", "D");
                sp.AddParam("Equip_Code", txt_EquipCode.Text);

                ret = DbHelp.Proc_Save(sp);

                if (ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                txt_EquipCode.Text = "";
                Search_Data();

                btn_Delete.sCHK = "Y";
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        private void btn_Excel_Click(object sender, EventArgs e)
        {
            //FileIF.Excel_Down(gc_Equip, this.Name);
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            if (btn_Close.Result_Update == DialogResult.Yes)
            {
                if (!Save_Data())
                    return;
            }

            MainForm.MainTab.TabPages.Remove(MainForm.MainTab.SelectedTabPage);
        }
        #endregion

        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txt_Properties_ButtonClick(sender, null);
        }

        private void txt_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            string GM_Code = "", Proc = "", Search_Set = "";
            ButtonEdit Button_Edit = (ButtonEdit)sender;
            TextEdit Name = null;

            if (Button_Edit == txt_Kind || Button_Edit == txt_location)
            {
                Search_Set = "General";
                Proc = "sp_Help_General";

                if (Button_Edit == txt_Kind)
                {
                    GM_Code = "50010";
                    Name = txt_KindNM;
                }
                else
                {
                    GM_Code = "50020";
                    Name = txt_LocationNM;
                }
            }
            else if (Button_Edit == txt_Custom || Button_Edit == txt_Maker)
            {
                Search_Set = "Custom";
                Proc = "sp_Help_Custom_Code";

                if (Button_Edit == txt_Custom)
                    Name = txt_CustomNM;
                else
                    Name = txt_MakerNM;
            }

            PopHelpForm Help_Form;

            if (!string.IsNullOrWhiteSpace(GM_Code))
                Help_Form = new PopHelpForm(Search_Set, Proc, GM_Code, Button_Edit.Text, "N");
            else
                Help_Form = new PopHelpForm(Search_Set, Proc, Button_Edit.Text, "N");

            if (Help_Form.ShowDialog() == DialogResult.OK)
            {
                Button_Edit.Text = Help_Form.sRtCode;
                Name.Text = Help_Form.sRtCodeNm;
            }
        }

        private void txt_EditValueChanged(object sender, EventArgs e)
        {
            string GM_Code = "", Proc = "";
            ButtonEdit Button_Edit = (ButtonEdit)sender;
            TextEdit Name = null;

            if (Button_Edit == txt_Kind || Button_Edit == txt_location)
            {
                Proc = "sp_Help_General";

                if (Button_Edit == txt_Kind)
                {
                    GM_Code = "50010";
                    Name = txt_KindNM;
                }
                else
                {
                    GM_Code = "50020";
                    Name = txt_LocationNM;
                }
            }
            else if (Button_Edit == txt_Custom || Button_Edit == txt_Maker)
            {
                Proc = "sp_Help_Custom_Code";

                if (Button_Edit == txt_Custom)
                    Name = txt_CustomNM;
                else
                    Name = txt_MakerNM;
            }

            if (!string.IsNullOrWhiteSpace(GM_Code))
                Name.Text = PopHelpForm.Return_Help(Proc, Button_Edit.Text, GM_Code);
            else
                Name.Text = PopHelpForm.Return_Help(Proc, Button_Edit.Text);
        }

        private void pictureEdit_Equip_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Image_Help.Image_Right_Click(sender, e);
            }
        }


    }
}

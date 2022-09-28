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


            Search_Data();
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

                if (!string.IsNullOrWhiteSpace(txt_EquipCode.Text))
                {
                    DataTable Table = FileIF.Get_File_List(this.Name);

                    DataRow[] Rows = (Table == null) ? null : Table.Select("File_Name LIKE '" + txt_EquipCode.Text + "%'");

                    if (Rows != null && Rows.Count() > 0)
                    {
                        string Path = this.Name + "/" + Rows[0]["File_Name"].NullString();
                        pictureEdit_Equip.Image = FileIF.FTP_Download_Image(Path);
                        pictureEdit_Equip.ToolTip = Path;
                    }
                    else
                    {
                        pictureEdit_Equip.Image = null;
                        pictureEdit_Equip.ToolTip = "";
                    }

                    pictureEdit_Equip.Refresh();
                }

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

                    ret = DbHelp.Proc_Save(sp);

                    if (ret.ReturnChk != 0)
                    {
                        XtraMessageBox.Show(ret.ReturnMessage);
                        return false;
                    }

                    txt_EquipCode.Text = ret.ReturnDataSet.Tables[0].Rows[0][0].NullString();

                    if (pictureEdit_Equip.Image != null)
                    {
                        if (!pictureEdit_Equip.ToolTip.Contains("ftp"))
                        {
                            string File_Name = pictureEdit_Equip.ToolTip.Substring(pictureEdit_Equip.ToolTip.LastIndexOf("\\") + 1, pictureEdit_Equip.ToolTip.Length - pictureEdit_Equip.ToolTip.LastIndexOf("\\") - 1);

                            //FileIF.FTP_Directory(GlobalValue.Basic_URL + this.Name);

                            //FileIF.FTP_Upload_File(pictureEdit_Equip.ToolTip, GlobalValue.Basic_URL + this.Name + "/" + txt_EquipCode.Text + File_Name.Substring(File_Name.LastIndexOf("."), File_Name.Length - File_Name.LastIndexOf(".")));
                        }
                    }
                    else if (pictureEdit_Equip.Image == null)
                    {
                        FileIF.FTP_Delete_File(pictureEdit_Equip.ToolTip);
                    }

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

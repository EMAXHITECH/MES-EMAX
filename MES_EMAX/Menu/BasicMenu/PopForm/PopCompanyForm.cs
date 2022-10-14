using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MES
{
    public partial class PopCompanyForm : BaseForm
    {
        ReturnStruct ret = new ReturnStruct();

        public PopCompanyForm(string Company_Code = "")
        {
            InitializeComponent();

            pictureEdit_Logo.Properties.ContextMenuStrip = new ContextMenuStrip();
            pictureEdit_Sign.Properties.ContextMenuStrip = new ContextMenuStrip();

            txt_CompanyCode.Text = Company_Code;
            Search_Data();
        }

        private void PopCompanyForm_Load(object sender, EventArgs e)
        {

        }

        private void Search_Data()
        {
            try
            {
                SqlParam sp = new SqlParam("sp_Company");
                sp.AddParam("Kind", "S");
                sp.AddParam("Company_Code", txt_CompanyCode.Text);

                ret = DbHelp.Proc_Search(sp);

                if (ret.ReturnDataSet.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ret.ReturnDataSet.Tables[0].Rows[0];

                    foreach (DataColumn col in row.Table.Columns)
                    {
                        if (row[col].GetType().Name.Contains("Null"))
                        {
                            if (!(col.ToString().Contains("Img") || col.ToString().Contains("Date")))
                            {
                                row[col] = "";
                            }
                        }   
                    }

                    txt_CompanyCode.Text = row["Company_Code"].NullString();
                    txt_CompanyName.Text = row["Company_Name"].NullString();
                    txt_Owner.Text = row["Owner"].NullString();
                    txt_ShortNM.Text = row["Short_Name"].NullString();
                    chk_Main.EditValue = row["Def_Ck"].NullString();
                    txt_CompanyNo.Text = row["Company_No"].NullString();
                    txt_OfficeNo.Text = row["Office_No"].NullString();
                    txt_UpTai.Text = row["UpTai"].NullString();
                    txt_UpJong.Text = row["UpJong"].NullString();
                    txt_TelNo.Text = row["Tel_No"].NullString();
                    txt_FaxNo.Text = row["Fax_No"].NullString();
                    txt_EMail.Text = row["E_Mail"].NullString();
                    txt_HomePage.Text = row["Home_Page"].NullString();
                    txt_BillAddr1.Text = row["Bill_Addr1"].NullString();
                    txt_BillAddr2.Text = row["Bill_Addr2"].NullString();
                    txt_RegDate.Text = row["Reg_Date"].NullString();
                    txt_RegUser.Text = row["Reg_User_Name"].NullString();
                    txt_UpDate.Text = row["Up_Date"].NullString();
                    txt_UpUser.Text = row["Up_User_Name"].NullString();

                    if (!row["Logo_Img"].GetType().Name.Contains("Null") && ((byte[])row["logo_img"]).Count() != 0)
                    {
                        MemoryStream ms = new MemoryStream((byte[])row["logo_img"]);
                        pictureEdit_Logo.Image = Image.FromStream(ms);
                    }

                    if (!row["Sign_Img"].GetType().Name.Contains("Null") && ((byte[])row["Sign_Img"]).Count() != 0)
                    {
                        MemoryStream ms = new MemoryStream((byte[])row["Sign_Img"]);
                        string str = ms.ToString();
                        pictureEdit_Sign.Image = Image.FromStream(ms);
                    }
                }

                if (!string.IsNullOrWhiteSpace(txt_CompanyCode.Text))
                {
                    if (!string.IsNullOrWhiteSpace(txt_CompanyName.Text))
                        txt_CompanyCode.ReadOnly = true;

                    txt_CompanyName.Focus();
                }
                else
                {
                    txt_CompanyCode.ReadOnly = false;
                    txt_CompanyCode.Focus();
                }

                btn_Insert.sUpdate = "N";
                btn_Close.sUpdate = "N";
            }
            catch (Exception)
            {

            }
        }

        private void chk_Main_CheckedChanged(object sender, EventArgs e)
        {
            Check_Changed();
        }

        private void chk_Main_EditValueChanged(object sender, EventArgs e)
        {
            Check_Changed();
        }

        private void Check_Changed()
        {
            SqlParam sp = new SqlParam("sp_Company");
            sp.AddParam("Kind", "C");
            sp.AddParam("Company_Code", txt_CompanyCode.Text);

            ret = DbHelp.Proc_Search(sp);

            if (ret.ReturnChk != 0)
            {
                XtraMessageBox.Show(ret.ReturnMessage);
                return;
            }

            if (Convert.ToInt32(ret.ReturnDataSet.Tables[0].Rows[0][0].NumString()) > 0)
                chk_Main.Checked = false;
        }

        private void btn_Insert_Click(object sender, EventArgs e)
        {
            if (btn_Insert.Result_Update == DialogResult.Yes)
            {
                if (!Save_Data())
                    return;
            }

            DbHelp.Clear_Panel(panelControl3);
            DbHelp.Clear_Panel(panel_H);

            txt_CompanyNo.Text = "";
            txt_OfficeNo.Text = "";

            Search_Data();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (Save_Data())
            {
                btn_Save.sCHK = "Y";
                Search_Data();
            }
        }

        private bool Check_Values()
        {
            if (string.IsNullOrWhiteSpace(txt_CompanyCode.Text))
            {
                XtraMessageBox.Show("회사 코드는 필수값입니다.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txt_CompanyName.Text))
            {
                XtraMessageBox.Show("회사명은 필수값입니다.");
                return false;
            }

            return true;
        }

        private bool Save_Data()
        {
            if (Check_Values())
            {
                SqlParam sp = new SqlParam("sp_Company");
                sp.AddParam("Kind", "I");

                sp.AddParam("Company_Code", txt_CompanyCode.Text);
                sp.AddParam("Company_Name", txt_CompanyName.Text);
                sp.AddParam("Company_No", txt_CompanyNo.Text);
                sp.AddParam("Office_No", txt_OfficeNo.Text);
                sp.AddParam("Owner", txt_Owner.Text);
                sp.AddParam("UpTai", txt_UpTai.Text);
                sp.AddParam("UpJong", txt_UpJong.Text);
                sp.AddParam("Tel_No", txt_TelNo.Text);
                sp.AddParam("Fax_No", txt_FaxNo.Text);
                sp.AddParam("E_Mail", txt_EMail.Text);
                sp.AddParam("Home_Page", txt_HomePage.Text);
                sp.AddParam("Bill_Addr1", txt_BillAddr1.Text);
                sp.AddParam("Bill_Addr2", txt_BillAddr2.Text);
                sp.AddParam("Use_Ck", "Y");

                if (pictureEdit_Logo.Image != null)
                    sp.AddParam("Logo_Img", Image_Help.ImageToByte(pictureEdit_Logo.Image));
                if (pictureEdit_Sign.Image != null)
                    sp.AddParam("Sign_Img", Image_Help.ImageToByte(pictureEdit_Sign.Image));

                sp.AddParam("Short_Name", txt_ShortNM.Text);
                sp.AddParam("Def_Ck", chk_Main.EditValue);

                sp.AddParam("Reg_User", GlobalValue.sUserID);
                sp.AddParam("Up_User", GlobalValue.sUserID);

                ret = DbHelp.Proc_Search(sp);

                if (ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return false;
                }

                return true;
            }
            else
                return true;
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            SqlParam sp = new SqlParam("sp_Company");
            sp.AddParam("Kind", "D");
            sp.AddParam("Company_Code", txt_CompanyCode.Text);

            ret = DbHelp.Proc_Save(sp);

            if (ret.ReturnChk != 0)
            {
                XtraMessageBox.Show(ret.ReturnMessage);
            }

            btn_Delete.sCHK = "Y";

            DbHelp.Clear_Panel(panelControl3);
            DbHelp.Clear_Panel(panel_H);

            txt_CompanyNo.Text = "";
            txt_OfficeNo.Text = "";

            Search_Data();
            txt_CompanyCode.Focus();
        }

        

        #region 상속 함수

        protected override void btnClose()
        {
            btn_Close.PerformClick();
        }

        protected override void btnDelete()
        {
            btn_Delete.PerformClick();
        }

        protected override void btnInsert()
        {
            btn_Insert.PerformClick();
        }

        protected override void btnSave()
        {
            btn_Save.PerformClick();
        }

        protected override void Control_TextChange(object sender, EventArgs e)
        {
            base.Control_TextChange(sender, e);

            btn_Insert.sUpdate = "Y";
            btn_Close.sUpdate = "Y";
        }

        #endregion

        private void btn_Close_Click(object sender, EventArgs e)
        {
            if (btn_Close.Result_Update == DialogResult.Yes)
            {
                if (!Save_Data())
                    return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void txt_CompanyCode_Leave(object sender, EventArgs e)
        {
            Search_Data();
        }


        private void pictureEdit_Logo_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Image_Help.Image_Right_Click(sender, e);
            }
        }

        private void pictureEdit_Sign_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Image_Help.Image_Right_Click(sender, e);
            }
        }
    }
}
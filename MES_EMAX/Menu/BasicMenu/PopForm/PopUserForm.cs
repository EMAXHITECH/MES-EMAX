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
using System.Security.Cryptography;
using System.IO;
using System.Text.RegularExpressions;
using DevExpress.Utils;

namespace MES
{
    public partial class PopUserForm : BaseForm
    {
        ReturnStruct ret = new ReturnStruct();
        DataSet ds = new DataSet();

        private string sUpdate = "N", sUser_Code = "";
        private string sPWD = "";
        private string MenuNm = "regUser";

        public PopUserForm()
        {
            InitializeComponent();
        }

        public PopUserForm(string sUser_Code) : this()
        {
            this.sUser_Code = sUser_Code;
            this.sUpdate = "Y";
        }

        private void PopUserForm_Load(object sender, EventArgs e)
        {
            label_Name.Text = ForMat.Return_FormNM(MenuNm);
            //기존 메뉴 안뜨도록 설정
            pictureEdit_Sign.Properties.ContextMenuStrip = new ContextMenuStrip();

            if(sUpdate == "Y")
            {
                txt_UserCode.Text = sUser_Code;

                Search_User();
            }
            else
            {
                Clear_Form(null);
            }

            txt_UserCode.Focus();
        }

        private void Set_Default()
        {
            string[] codes = DbHelp.Set_Default("10010");

            if (codes != null)
            {
                txt_UMESos.Text = codes[0];
                txt_UMESosNM.Text = codes[1];
            }
        }

        private void Search_User()
        {
            try
            {
                SqlParam sp = new SqlParam("sp_regUser");
                sp.AddParam("Kind", "S");
                sp.AddParam("User_Code", sUser_Code);
                sp.AddParam("Search_R", "Y");

                ret = DbHelp.Proc_Search(sp);

                if (ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                if (ret.ReturnDataSet.Tables[0].Rows.Count > 0)
                {
                    Clear_Form(ret.ReturnDataSet.Tables[0]);
                    txt_UserCode.BackColor = Color.LightGray;
                    txt_UserCode.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        private void Clear_Form(DataTable dt)
        {
            if(dt != null)
            {
                DataRow dr = dt.Rows[0]; //DataRow 써서 작업하기
                txt_UserCode.Text = dt.Rows[0]["User_Code"].ToString();
                txt_UserName.Text = dt.Rows[0]["User_Name"].ToString();
                txt_UMESos.Text = dt.Rows[0]["User_Pos"].ToString();
                txt_UMESosNM.Text = dt.Rows[0]["User_PosNM"].ToString();
                txt_DeptCode.Text = dt.Rows[0]["Dept_Code"].ToString();
                txt_DeptNM.Text = dt.Rows[0]["Dept_Name"].ToString();
                dt_InDate.Text = dt.Rows[0]["In_Date"].ToString();
                txt_TelNo.Text = dt.Rows[0]["Tel_No"].ToString();
                txt_FaxNo.Text = dt.Rows[0]["Fax_No"].ToString();
                txt_MobileNo.Text = dt.Rows[0]["Mobile_No"].ToString();
                txt_EMail.Text = dt.Rows[0]["E_Mail"].ToString();
                check_Use.EditValue = dt.Rows[0]["Use_Ck"].ToString();
                sPWD = dt.Rows[0]["PWD"].ToString();
                txt_PWD.Text = "1234";

                if(dt.Rows[0]["Sign_Img"].ToString() != "")
                {
                    MemoryStream ms = new MemoryStream((byte[])dt.Rows[0]["Sign_Img"]);
                    pictureEdit_Sign.Image = Image.FromStream(ms);
                }
            }
            else
            {
                txt_UserCode.Text = "";
                txt_UserName.Text = "";
                txt_UMESos.Text = "";
                txt_UMESosNM.Text = "";
                txt_DeptCode.Text = "";
                txt_DeptNM.Text = "";
                dt_InDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
                txt_TelNo.Text = "";
                txt_FaxNo.Text = "";
                txt_MobileNo.Text = "";
                txt_EMail.Text = "";
                txt_PWD.Text = "";
                check_Use.EditValue = "Y";
                pictureEdit_Sign.Image = null;
                Set_Default();

                txt_UserCode.BackColor = Color.White;
                txt_UserCode.Enabled = true;
                txt_UserCode.Focus();
            }

            Change_Edit();
        }

        private void Change_Edit()
        {
            btn_Insert.sUpdate = "N";
            btn_Close.sUpdate = "N";
        }

        protected override void Control_TextChange(object sender, EventArgs e)
        {
            base.Control_TextChange(sender, e);

            btn_Insert.sUpdate = "Y";
            btn_Close.sUpdate = "Y";
        }

        private void txt_DeptCode_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {            
            if(string.IsNullOrWhiteSpace(txt_DeptNM.Text))
            {
                PopHelpForm HelpForm = new PopHelpForm("Dept", "sp_Help_Dept", txt_DeptCode.Text, "N");

                if (HelpForm.ShowDialog() == DialogResult.OK)
                {
                    txt_DeptCode.Text = HelpForm.sRtCode;
                    txt_DeptNM.Text = HelpForm.sRtCodeNm;
                }
            }            
        }

        private void txt_DeptCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                txt_DeptCode_Properties_ButtonClick(sender, null);
            }
        }

        private void txt_DeptCode_EditValueChanged(object sender, EventArgs e)
        {
            txt_DeptNM.Text = PopHelpForm.Return_Help("sp_Help_Dept", txt_DeptCode.Text);
        }

        private void txt_UMESos_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_UMESosNM.Text))
            {
                PopHelpForm HelpForm = new PopHelpForm("General", "sp_Help_General", "10010", txt_UMESos.Text, "N");

                if (HelpForm.ShowDialog() == DialogResult.OK)
                {
                    txt_UMESos.Text = HelpForm.sRtCode;
                    txt_UMESosNM.Text = HelpForm.sRtCodeNm;
                }
            }
        }

        private void txt_UMESos_EditValueChanged(object sender, EventArgs e)
        {
            txt_UMESosNM.Text = PopHelpForm.Return_Help("sp_Help_General", txt_UMESos.Text, "10010");
        }

        private void txt_UMESos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                txt_UMESos_Properties_ButtonClick(sender, null);
            }
        }

        private void txt_UserCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                sUser_Code = txt_UserCode.Text;
                Search_User();
            }
        }

        private void pictureEdit_Sign_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Image_Help.Image_Right_Click(sender, e);
            }
        }

        private string SHA256Hash(string data)
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

        private void btn_Insert_Click(object sender, EventArgs e)
        {
            if (btn_Insert.Result_Update == DialogResult.Yes)
            {
                btn_Save.sUpdate = "Y";
                btn_Save_Click(null, null);
            }

            Clear_Form(null);
            sUpdate = "N";
            sUser_Code = "";
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (!Check_Err())
                return;

            try

            {
                SqlParam sp = new SqlParam("sp_regUser");
                sp.AddParam("Kind", sUpdate == "N" ? "I" : "U");
                sp.AddParam("User_Code", txt_UserCode.Text);
                sp.AddParam("User_Name", txt_UserName.Text);
                sp.AddParam("Dept_Code", txt_DeptCode.Text);
                sp.AddParam("User_Pos", txt_UMESos.Text);
                sp.AddParam("In_Date", dt_InDate.DateTime.ToString("yyyyMMdd"));
                sp.AddParam("Pwd", sUpdate == "Y" ? (txt_PWD.Text == "1234" ? sPWD : SHA256Hash(txt_PWD.Text)) : SHA256Hash(txt_PWD.Text));// 비밀번호 변경시 새 암호화 처리
                sp.AddParam("Tel_No", txt_TelNo.Text);
                sp.AddParam("Fax_No", txt_FaxNo.Text);
                sp.AddParam("Mobile_No", txt_MobileNo.Text);
                sp.AddParam("E_Mail", txt_EMail.Text);
                sp.AddParam("Use_Ck", check_Use.EditValue);
                if (pictureEdit_Sign.Image != null)
                    sp.AddParam("Sign_Img", Image_Help.ImageToByte(pictureEdit_Sign.Image));

                ret = DbHelp.Proc_Save(sp);

                if (ret.ReturnChk == 0)
                {
                    btn_Save.sCHK = "Y";
                    txt_UserCode.Enabled = false;
                    sUpdate = "Y";
                    //값 변경 여부
                    Change_Edit();
                }
                else
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                SqlParam sp = new SqlParam("sp_regUser");
                sp.AddParam("Kind", "D");
                sp.AddParam("User_Code", txt_UserCode.Text);

                ret = DbHelp.Proc_Save(sp);

                if (ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }
                else
                {
                    btn_Delete.sCHK = "Y";
                    Clear_Form(null);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private bool Check_Err()
        {
            if(txt_UserCode.Text == "")
            {
                XtraMessageBox.Show("사원코드는 필수 입력 값입니다");
                return false;
            }

            if(txt_UserName.Text == "")
            {
                XtraMessageBox.Show("사원명은 필수 입력 값입니다");
                return false;
            }

            if(txt_DeptNM.Text == "")
            {
                XtraMessageBox.Show("부서는 필수 입력 값입니다");
                return false;
            }

            if(txt_PWD.Text == "")
            {
                XtraMessageBox.Show("비밀번호는 필수 입력 값입니다");
                return false;
            }

            if(txt_UMESos.Text != "" && txt_UMESosNM.Text == "")
            {
                XtraMessageBox.Show("직책 코드가 잘못 입력되어있습니다");
                return false;
            }

            //if(txt_PWD.Text.Length < 8)
            //{
            //    XtraMessageBox.Show("비밀번호는 8자리 이상으로 입력하셔야합니다.");
            //    return false;
            //}

            //if (!txt_PWD.Text.Any(char.IsUpper))
            //{
            //    XtraMessageBox.Show("비밀번호는 영문 대문자, 숫자, 특수문자 1개를 반드시 포함해야합니다");
            //    return false;
            //}

            //if(!txt_PWD.Text.Any(char.IsDigit))
            //{
            //    XtraMessageBox.Show("비밀번호는 영문 대문자, 숫자, 특수문자 1개를 반드시 포함해야합니다");
            //    return false;
            //}

            //string str = @"[~!@\#$%^&*\()\=+|\\/:;?""<>']";
            //Regex rex = new Regex(str);

            //if (!rex.IsMatch(txt_PWD.Text))
            //{
            //    XtraMessageBox.Show("비밀번호는 영문 대문자, 숫자, 특수문자 1개를 반드시 포함해야합니다");
            //    return false;
            //}

            return true;
        }

        //단축키
        protected override void btnClose()
        {
            btn_Close.PerformClick();
        }

        protected override void btnInsert()
        {
            btn_Insert.PerformClick();
        }

        protected override void btnSave()
        {
            btn_Save.PerformClick();
        }

        private void txt_PWD_Enter(object sender, EventArgs e)
        {
            //toolTipController1.SetTitle(txt_PWD, "비밀번호 조건");
            //toolTipController1.SetToolTip(txt_PWD, "영문 대문자, 숫자, 특수문자 1개를 반드시 포함\n길이 8자리 이상");
            //txt_PWD.ToolTipController = toolTipController1;
        }

        protected override void btnDelete()
        {
            btn_Delete.PerformClick();
        }
    }
}

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
    public partial class PopDeptForm : BaseForm
    {
        public string Dept_Code { get; set; }
        public bool Edit = false;
        private string sUpdate = "N";
        ReturnStruct ret = new ReturnStruct();
        DataSet ds = new DataSet();

        public PopDeptForm()
        {
            InitializeComponent();
        }

        private void PopDeptForm_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Dept_Code))
            {
                Check_DeptCode(Dept_Code);
                txt_DeptCode.Enabled = false;
            }

            txt_DeptCode.Focus();
        }

        #region 텍스트 이벤트
        private void txt_DeptCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                Check_DeptCode(txt_DeptCode.Text);
                if (!string.IsNullOrWhiteSpace(txt_DeptName.Text))
                {
                    txt_DeptCode.Enabled = false;
                    txt_DeptName.Focus();
                }
            }
        }

        private void txt_DeptParent_EditValueChanged(object sender, EventArgs e)
        {
            txt_DeptParentNM.Text = PopHelpForm.Return_Help("sp_Help_Dept", txt_DeptParent.Text);
        }

        private void txt_DeptParent_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txt_DeptParent_Properties_ButtonClick(sender, null);
            }
        }

        private void txt_DeptParent_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_DeptParentNM.Text))
            {
                PopHelpForm HelpForm = new PopHelpForm("Dept", "sp_Help_Dept", txt_DeptParent.Text, "N");
                if (HelpForm.ShowDialog() == DialogResult.OK)
                {
                    txt_DeptParent.Text = HelpForm.sRtCode;
                    txt_DeptParentNM.Text = HelpForm.sRtCodeNm;
                }
            }
        }

        private void txt_CustomCode_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_CustomName.Text))
            {
                PopHelpForm HelpForm = new PopHelpForm("Custom", "sp_Help_Custom_Param", txt_CustomCode.Text, "N");
                HelpForm.Set_Value("", "500", "", "", "");
                if (HelpForm.ShowDialog() == DialogResult.OK)
                {
                    txt_CustomCode.Text = HelpForm.sRtCode;
                }
            }
        }

        private void txt_CustomCode_EditValueChanged(object sender, EventArgs e)
        {
            txt_CustomName.Text = PopHelpForm.Return_Help("sp_Help_Custom_Param", txt_CustomCode.Text);
        }

        private void txt_CustomCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                txt_CustomCode_Properties_ButtonClick(sender, null);
            }
        }

        protected override void Control_TextChange(object sender, EventArgs e)
        {
            base.Control_TextChange(sender, e);

            btn_Insert.sUpdate = "Y";
            btn_Close.sUpdate = "Y";
        }
        #endregion

        #region 버튼 이벤트
        protected override void btnInsert()
        {
            btn_Insert.PerformClick();
        }
        private void btn_Insert_Click(object sender, EventArgs e)
        {
            if (btn_Insert.Result_Update == DialogResult.Yes)
            {
                btn_Save.sUpdate = "Y";
                btn_Save_Click(null, null);
            }
            DbHelp.Clear_Panel(panel_H);
            DbHelp.Clear_Panel(panelControl3);

            btn_Save.sCHK = "Y";
            btn_Insert.sUpdate = "N";
            btn_Close.sUpdate = "N";
            sUpdate = "N";
            Dept_Code = "";

            txt_DeptCode.Enabled = true;
            Edit = false;
        }

        protected override void btnSave()
        {
            btn_Save.PerformClick();
        }
        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (Check_Overlap() && !Edit)
            {
                XtraMessageBox.Show("해당 부서 데이터는 이미 존재합니다.");
                return;
            }
            if (!string.IsNullOrWhiteSpace(txt_DeptParent.Text) && string.IsNullOrWhiteSpace(txt_DeptParentNM.Text))
            {
                XtraMessageBox.Show("존재하지 않는 부서코드는 상위부서로 등록이 불가능합니다.");
                txt_DeptParent.Focus();
                return;
            }

            SqlParam sp = new SqlParam("sp_regDept");

            if (Check_Values())
            {
                if (string.IsNullOrWhiteSpace(Dept_Code) && !Edit)
                {
                    sp.AddParam("Kind", "I");
                    sp.AddParam("Dept_Code", txt_DeptCode.Text);
                    sp.AddParam("Dept_Name", txt_DeptName.Text);
                    sp.AddParam("Dept_Parent", txt_DeptParent.Text);
                    sp.AddParam("Use_Ck", check_Use.EditValue.ToString());
                    sp.AddParam("Custom_Code", txt_CustomCode.Text);
                }
                else if (Edit)
                {
                    sp.AddParam("Kind", "U");

                    sp.AddParam("Dept_Code", txt_DeptCode.Text);
                    sp.AddParam("Dept_Name", txt_DeptName.Text);
                    sp.AddParam("Dept_Parent", txt_DeptParent.Text);
                    sp.AddParam("Custom_Code", txt_CustomCode.Text);
                    sp.AddParam("Use_Ck", check_Use.EditValue.ToString());
                }
                else
                    return;

                ret = DbHelp.Proc_Save(sp);

                if (ret.ReturnChk == 0)
                {
                    btn_Save.sCHK = "Y";
                    btn_Insert.sUpdate = "N";
                    btn_Close.sUpdate = "N";
                    sUpdate = "Y";

                    txt_DeptCode.Enabled = false;
                    Edit = true;
                }
                else
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }
            }

            //btn_Save.sCHK = "Y";
        }

        protected override void btnDelete()
        {
            btn_Delete.PerformClick();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlParam sp = new SqlParam("sp_regDept");
            sp.AddParam("Kind", "D");
            sp.AddParam("Dept_Code", txt_DeptCode.Text);
            ret = DbHelp.Proc_Save(sp);

            if (ret.ReturnChk == 0)
            {
                DbHelp.Clear_Panel(panel_H);
                DbHelp.Clear_Panel(panelControl3);

                btn_Save.sCHK = "Y";
                btn_Delete.sCHK = "Y";
                btn_Insert.sUpdate = "N";
                btn_Close.sUpdate = "N";
                sUpdate = "Y";
                
                txt_DeptCode.Enabled = true;
                Edit = true;
            }
            else
            {
                XtraMessageBox.Show(ret.ReturnMessage);
                return;
            }
        }

        protected override void btnClose()
        {
            btn_Close.PerformClick();
        }
        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        #endregion

        #region 기타 메소드
        private void Check_DeptCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                DbHelp.Clear_Panel(panelControl3);
                Edit = false;
                return;
            }

            SqlParam sp = new SqlParam("sp_regDept");
            sp.AddParam("Kind", "S");
            sp.AddParam("Dept_Code", code);

            ret = DbHelp.Proc_Search(sp);
            ds = ret.ReturnDataSet;

            if (ds != null && ds.Tables.Count == 1)
            {
                DataTable Table = DbHelp.Fill_Table(ds.Tables[0]);

                if (Table.Rows.Count > 0)
                {
                    DataRow row = Table.Rows[0];
                    txt_DeptCode.Text = row["Dept_Code"].ToString();
                    txt_DeptName.Text = row["Dept_Name"].ToString();
                    txt_DeptParent.Text = row["Dept_Parent"].ToString();
                    txt_DeptParentNM.Text = row["Dept_Parent_Name"].ToString();
                    check_Use.EditValue = row["Use_Ck"].ToString();
                    txt_CustomCode.Text = row["Custom_Code"].ToString();

                    btn_Insert.sUpdate = "N";
                    btn_Close.sUpdate = "N";
                    Edit = true;
                }
                else
                {
                    // DbHelp.Clear_Panel(panelControl3);
                    btn_Insert.sUpdate = "Y";
                    btn_Close.sUpdate = "Y";
                    Edit = false;
                }
            }
        }

        private bool Check_Values()
        {
            if (string.IsNullOrWhiteSpace(txt_DeptCode.Text))
            {
                XtraMessageBox.Show("부서 코드를 입력해주시길 바랍니다.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txt_DeptName.Text))
            {
                XtraMessageBox.Show("부서명을 입력해주시길 바랍니다.");
                return false;
            }
            return true;
        }

        private bool Check_Overlap()
        {
            SqlParam sp = new SqlParam("sp_regDept");
            sp.AddParam("Kind", "C");
            sp.AddParam("Dept_Name", txt_DeptName.Text);

            ret = DbHelp.Proc_Search(sp);
            string check = ret.ReturnDataSet.Tables[0].Rows[0][0].ToString();

            if (Convert.ToInt32(check) > 0)
                return true;
            else return false;
        }



        #endregion

       
    }
}

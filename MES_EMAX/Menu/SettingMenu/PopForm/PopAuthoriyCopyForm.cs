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

namespace MES
{
    public partial class PopAuthoriyCopyForm : BaseForm
    {
        private ReturnStruct ret = new ReturnStruct();
        public string sUser_Code, sUser_Name;
        public PopAuthoriyCopyForm()
        {
            InitializeComponent();
        }

        private void PopAuthoriyCopyForm_Load(object sender, EventArgs e)
        {
            Grid_Set();
            Search_Data();

            txt_UserName.Text = sUser_Name;
        }
        
        private void Grid_Set()
        {
            DbHelp.GridSet(gc_User, gv_User, "Check_Box", "Check", "50", false, true, true);
            DbHelp.GridSet(gc_User, gv_User, "User_Code", "사원코드", "100", false, false, true);
            DbHelp.GridSet(gc_User, gv_User, "User_Name", "사원명", "100", false, false, true);
            DbHelp.GridSet(gc_User, gv_User, "Dept_Name", "부서", "100", false, false, true);
            DbHelp.GridSet(gc_User, gv_User, "Pos_Name", "직급", "100", false, false, true);

            DbHelp.GridColumn_CheckBox(gv_User, "Check_Box");
        }

        private void Search_Data()
        {
            SqlParam sp = new SqlParam("sp_regAuthority");
            sp.AddParam("Kind", "S");
            sp.AddParam("Part", "C");
            sp.AddParam("User_Code", sUser_Code);

            ReturnStruct ret = DbHelp.Proc_Search(sp);

            if (ret.ReturnChk != 0)
            {
                XtraMessageBox.Show(ret.ReturnMessage);
                return;
            }

            gc_User.DataSource = ret.ReturnDataSet.Tables[0];
            gc_User.RefreshDataSource();
            gv_User.BestFitColumns();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                string sCopyUser = "";

                for(int i = 0; i < gv_User.RowCount; i++)
                {
                    if(gv_User.GetRowCellValue(i, "Check_Box").ToString() == "Y")
                    {
                        sCopyUser += gv_User.GetRowCellValue(i, "User_Code").ToString() + "_/";
                    }
                }

                SqlParam sp = new SqlParam("sp_regAuthority");
                sp.AddParam("Kind", "P");
                sp.AddParam("User_Code", sUser_Code);
                sp.AddParam("CopyUser", sCopyUser);

                ret = DbHelp.Proc_Save(sp);

                if(ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }

            this.DialogResult = DialogResult.OK;
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void btnSave()
        {
            btn_Save.PerformClick();
        }
    }
}
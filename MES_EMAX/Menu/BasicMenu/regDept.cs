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
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraEditors.Repository;

namespace MES
{
    public partial class regDept : BaseReg
    {
        private RepositoryItemTextEdit txt_edit = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
        ReturnStruct ret = new ReturnStruct();
        DataSet ds = new DataSet();

        public regDept()
        {
            InitializeComponent();
        }

        private void regDept_Load(object sender, EventArgs e)
        {
            Grid_Set();
            Search_Info();
        }

        private void Grid_Set()
        {
            DbHelp.GridSet(GridDept, ViewDept, "Dept_Code", "부서코드", "80", false, false, true);
            DbHelp.GridSet(GridDept, ViewDept, "Dept_Name", "부서명", "100", false, false, true);
            DbHelp.GridSet(GridDept, ViewDept, "Dept_Parent_Name", "상위부서", "100", false, false, true);
            DbHelp.GridSet(GridDept, ViewDept, "Dept_Parent", "상위부서코드", "80", false, false, false);
            DbHelp.GridSet(GridDept, ViewDept, "Custom_Name", "작업처", "100", false, false, true);
            DbHelp.GridSet(GridDept, ViewDept, "Use_Ck", "사용유무", "70", false, false, true);

            DbHelp.GridColumn_CheckBox(ViewDept, "Use_Ck");

            GridDept.PopMenuChk = false;
            GridDept.MouseWheelChk = false;
        }

        #region 버튼 이벤트
        protected override void btnSelect()
        {
            btn_Select.PerformClick();
        }

        private void btn_Select_Click(object sender, EventArgs e)
        {
            Search_Info();
        }

        protected override void btnInsert()
        {
            btn_Insert.PerformClick();
        }
        private void btn_Insert_Click(object sender, EventArgs e)
        {
            PopDeptForm PopForm = new PopDeptForm();
            PopForm.StartPosition = FormStartPosition.CenterScreen;
            if (PopForm.ShowDialog() == DialogResult.OK)
                btn_Select_Click(null, null);
        }

        protected override void btnDelete()
        {
            btn_Delete.PerformClick();
        }
        private void btn_Delete_Click(object sender, EventArgs e)
        {
            SqlParam sp = new SqlParam("sp_regDept");
            sp.AddParam("Kind", "D");
            sp.AddParam("Dept_Code", ViewDept.GetFocusedRowCellValue("Dept_Code").ToString());

            ret = DbHelp.Proc_Save(sp);

            if(ret.ReturnChk != 0)
            {
                XtraMessageBox.Show(ret.ReturnMessage);
                return;
            }

            btn_Delete.sCHK = "Y";
            btn_Select_Click(null, null);
        }

        protected override void btnClose()
        {
            btn_Close.PerformClick();
        }
        private void btn_Close_Click(object sender, EventArgs e)
        {
            MainForm.MainTab.TabPages.Remove(MainForm.MainTab.SelectedTabPage);
        }
        #endregion

        private void Search_Info()
        {
            SqlParam sp = new SqlParam("sp_regDept");
            sp.AddParam("Kind", "S");
            sp.AddParam("Dept_Name", "");

            ret = DbHelp.Proc_Search(sp);
            ds = ret.ReturnDataSet;
            DbHelp.Fill_Table(ds.Tables[0]);
            
            DataTable Table = ds.Tables[0];
  
            GridDept.DataSource = Table;
            GridDept.RefreshDataSource();
        }

        private void ViewDept_DoubleClick(object sender, EventArgs e)
        {
            if (ViewDept.FocusedRowHandle >= 0)
            {
                string code = ViewDept.GetFocusedRowCellValue("Dept_Code").ToString();

                PopDeptForm PopForm = new PopDeptForm();
                PopForm.Dept_Code = code;
                PopForm.StartPosition = FormStartPosition.CenterScreen;
                if (PopForm.ShowDialog() == DialogResult.OK)
                    btn_Select_Click(null, null);
            }
        }

        private void btn_Excel_Click(object sender, EventArgs e)
        {
            FileIF.Excel_Down(GridDept, "부서등록");
        }
    }
}

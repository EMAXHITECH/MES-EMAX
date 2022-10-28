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
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraTreeList;

namespace MES
{
    public partial class rptWorkPlan : BaseReg
    {
        ReturnStruct ret = new ReturnStruct();
        DataSet ds = new DataSet();

        public rptWorkPlan()
        {
            InitializeComponent();
        }

        private void rptWorkPlan_Load(object sender, EventArgs e)
        {           
            //버튼 클릭 이벤트 선언
            this.btn_Select.Click += btn_Select_Click;
            this.btn_Excel.Click += btn_Excel_Click;
            this.btn_Close.Click += btn_Close_Click;
            Grid_Set();
             
            btn_Select_Click(null, null);
        }

        private void Grid_Set()
        {
            // Quot_M 그리드
            DbHelp.GridSet(gc_WorkPlan, gv_WorkPlan, "Plan_Date", "계획일자", "120", false, false, true);
            DbHelp.GridSet(gc_WorkPlan, gv_WorkPlan, "Plan_No", "계획번호", "125", false, false, true);
            DbHelp.GridSet(gc_WorkPlan, gv_WorkPlan, "Company_Name", "사업장", "125", false, false, true);
            DbHelp.GridSet(gc_WorkPlan, gv_WorkPlan, "Form_Name", "계획구분", "125", false, false, true);
            DbHelp.GridSet(gc_WorkPlan, gv_WorkPlan, "User_Name", "담당자", "125", false, false, true);
            
            DbHelp.No_ReadOnly(gv_WorkPlan);
            gc_WorkPlan.PopMenuChk = false;
            gc_WorkPlan.MouseWheelChk = false;

            //BOM
            SetUp_Tree.Tree_Columns_Set(tree_Plan, "계획일자", "Plan_Date", 110, true);
            SetUp_Tree.Tree_Columns_Set(tree_Plan, "계획번호", "Plan_No", 120, true);
            SetUp_Tree.Tree_Columns_Set(tree_Plan, "사업장", "Company_Name", 100, true);
            SetUp_Tree.Tree_Columns_Set(tree_Plan, "담당자", "User_Name", 100, true);
            SetUp_Tree.Tree_Columns_Set(tree_Plan, "자품목코드", "SItem_Code", 150, true);
            SetUp_Tree.Tree_Columns_Set(tree_Plan, "품목명", "Item_Name", 200, true);
            SetUp_Tree.Tree_Columns_Set(tree_Plan, "규격", "Ssize", 200, true);
            SetUp_Tree.Tree_Columns_Set(tree_Plan, "주문 수량", "Order_Qty", 80, true);
            SetUp_Tree.Tree_Columns_Set(tree_Plan, "계획 수량", "Plan_Qty", 80, true);
            SetUp_Tree.Tree_Columns_Set(tree_Plan, "리드 타임", "Lead_Time", 80, true);
            SetUp_Tree.Tree_Columns_Set(tree_Plan, "작업 완료 예정일", "End_Date", 110, true);
            SetUp_Tree.Tree_Columns_Set(tree_Plan, "작업 시작일", "Start_Date", 110, true);
            SetUp_Tree.Tree_Columns_Set(tree_Plan, "MCode", "MCode", 0, false);
            SetUp_Tree.Tree_Columns_Set(tree_Plan, "SCode", "SCode", 0, false);
            SetUp_Tree.Tree_Columns_Set(tree_Plan, "Item_BPart", "Item_BPart", 0, false);
            SetUp_Tree.Tree_Columns_Set(tree_Plan, "외주 체크", "Out_Ck", 80, true); //추가 

            SetUp_Tree.Tree_Columns_Key(tree_Plan, "SCode", "MCode");

            SetUp_Tree.Tree_Column_Check(tree_Plan, "Out_Ck");

            //일자
            SetUp_Tree.Tree_Column_Date(tree_Plan, "Start_Date");
            SetUp_Tree.Tree_Column_Date(tree_Plan, "End_Date");

            //숫자 자릿수 설정
            SetUp_Tree.Tree_Column_NumSet(tree_Plan, "Order_Qty", 0);
            SetUp_Tree.Tree_Column_NumSet(tree_Plan, "Plan_Qty", 0);
            SetUp_Tree.Tree_Column_NumSet(tree_Plan, "Lead_Time", 0);

            //이미지 설정
            tree_Plan.ImageIndexFieldName = "Order_No";
            tree_Plan.StateImageList = imageCollection_BOM;
            tree_Plan.OptionsView.RowImagesShowMode = RowImagesShowMode.InIndent;
        }

        private void btn_Select_Click(object sender, EventArgs e)
        {
            Search_Data();
        }

        private void btn_Excel_Click(object sender, EventArgs e)
        {
            FileIF.Excel_Down(gc_WorkPlan, this.Name);
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            MainForm.MainTab.TabPages.Remove(MainForm.MainTab.SelectedTabPage);
        }

        private void Search_Data()
        {
            try
            {
                SqlParam sp = new SqlParam("sp_rptWorkPlan");
                sp.AddParam("Kind", "S");
                sp.AddParam("Search_D", "H");
                sp.AddParam("FDATE", dt_From.Text == "" ? "" : dt_From.DateTime.ToString("yyyyMMdd"));
                sp.AddParam("TDATE", dt_To.Text == "" ? DateTime.MaxValue.ToString("yyyyMMdd") : dt_To.DateTime.ToString("yyyyMMdd"));

                ret = DbHelp.Proc_Search(sp);
                if (ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                tree_Plan.DataSource = ret.ReturnDataSet.Tables[0];
                tree_Plan.ExpandAll();

                //gc_WorkPlan.DataSource = DbHelp.Fill_Table(ret.ReturnDataSet.Tables[0]);
                //gv_WorkPlan.BestFitColumns();

                //if (gv_WorkPlan.RowCount == 0)
                //    tree_Plan.DataSource = null;
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

      
        #region 버튼 상속
        protected override void btnSelect()
        {
            btn_Select.PerformClick();
        }

        protected override void btnSave()
        {
            btn_Save.PerformClick();
        }

        protected override void btnExcel()
        {
            btn_Excel.PerformClick();
        }

        protected override void btnClose()
        {
            btn_Close.PerformClick();
        }
        #endregion

        private void tree_Plan_GetStateImage(object sender, GetStateImageEventArgs e)
        {
            if (e.Node["Item_BPart"].NullString() == "100")
                e.NodeImageIndex = 0;
            else if (e.Node["Item_BPart"].NullString() == "200")
                e.NodeImageIndex = 1;
            else
                e.NodeImageIndex = 2;
        }

        private void gv_WorkPlan_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0)
                return;

            string sPlan_No = gv_WorkPlan.GetRowCellValue(e.FocusedRowHandle, "Plan_No").ToString();

            try
            {
                SqlParam sp = new SqlParam("sp_rptWorkPlan");
                sp.AddParam("Kind", "S");
                sp.AddParam("Search_D", "D");
                sp.AddParam("Plan_No", sPlan_No);

                ret = DbHelp.Proc_Search(sp);
                if (ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                tree_Plan.DataSource = DbHelp.Fill_Table(ret.ReturnDataSet.Tables[0]);
                tree_Plan.ExpandAll();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }
    }
}

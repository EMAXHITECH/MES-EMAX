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
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;

namespace MES
{
    public partial class rptFBOM : BaseReg
    {
        ReturnStruct ret = new ReturnStruct();
        DataSet ds = new DataSet();
        DataTable dt_Tree = new DataTable();

        private string sAddNewRow = "N";

        public rptFBOM()
        {
            InitializeComponent();
        }

        private void rptFBOM_Load(object sender, EventArgs e)
        {
            Grid_Set();

            Search();

            //treeList_Bom.ClearNodes();
            //Create_Columns();

            //treeList_Bom.ImageIndexFieldName = "Item_Code";
            //treeList_Bom.StateImageList = imageCollection1;
        }

        private void Grid_Set()
        {
            DbHelp.GridSet(gc_Item, gv_Item, "Item_Code", "품목코드", "120", false, false, true, false);
            DbHelp.GridSet(gc_Item, gv_Item, "Item_Name", "품목명", "150", false, false, true, false);
            DbHelp.GridSet(gc_Item, gv_Item, "SSize", "규격", "120", false, false, true, false);
            DbHelp.GridSet(gc_Item, gv_Item, "Item_BPart", "품목구분", "120", false, false, true, false);

            gc_Item.AddRowYN = false;
            gc_Item.PopMenuChk = false;
            gc_Item.MouseWheelChk = false;

            SetUp_Tree.Tree_Columns_Set(treeList_Bom, "품목코드", "Item_Code", 200, true);
            SetUp_Tree.Tree_Columns_Set(treeList_Bom, "품목명", "Item_Name", 150, true);
            SetUp_Tree.Tree_Columns_Set(treeList_Bom, "규격", "Ssize", 150, true);
            SetUp_Tree.Tree_Columns_Set(treeList_Bom, "품목구분", "Item_BPart", 80, true);
            SetUp_Tree.Tree_Columns_Set(treeList_Bom, "수량", "Qty", 80, true);
            SetUp_Tree.Tree_Columns_Set(treeList_Bom, "Loss", "Loss_Per", 80, true);
            SetUp_Tree.Tree_Columns_Set(treeList_Bom, "실소요량", "Real_Qty", 80, true);
            SetUp_Tree.Tree_Columns_Set(treeList_Bom, "자재구분코드", "BPart_Code", 120, false);
            SetUp_Tree.Tree_Columns_Set(treeList_Bom, "모코드", "MCode", 0, false);
            SetUp_Tree.Tree_Columns_Set(treeList_Bom, "자코드", "SCode", 0, false);

            SetUp_Tree.Tree_Columns_Key(treeList_Bom, "SCode", "MCode");

            SetUp_Tree.Tree_Column_NumSet(treeList_Bom, "Qty", ForMat.SetDecimal(this.Name, "Qty1"));
            SetUp_Tree.Tree_Column_NumSet(treeList_Bom, "Loss_Per", ForMat.SetDecimal(this.Name, "Qty1"));
            SetUp_Tree.Tree_Column_NumSet(treeList_Bom, "Real_Qty", ForMat.SetDecimal(this.Name, "Qty1"));

            treeList_Bom.ImageIndexFieldName = "Item_Code";
            treeList_Bom.StateImageList = imageCollection1;

            treeList_Bom.OptionsBehavior.ReadOnly = true;
            treeList_Bom.OptionsBehavior.Editable = false;

            treeList_Bom.OptionsView.RowImagesShowMode = DevExpress.XtraTreeList.RowImagesShowMode.InIndent;
        }

        #region 함수

        //품목 정보 조회
        private void Item_Search_D(int iRow)
        {
            try
            {
                SqlParam sp = new SqlParam("sp_rptFBOM");
                sp.AddParam("Kind", "S");
                sp.AddParam("Search_D", "SD");
                sp.AddParam("Item_Code", gv_Item.GetRowCellValue(iRow, "Item_Code").ToString());

                ret = DbHelp.Proc_Search(sp);

                if(ret.ReturnChk == 0)
                {
                    treeList_Bom.DataSource = ret.ReturnDataSet.Tables[0];
                    treeList_Bom.ExpandAll();
                }
                else
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
        }

        private void Search()
        {
            try
            {
                SqlParam sp = new SqlParam("sp_rptFBOM");
                sp.AddParam("Kind", "S");
                sp.AddParam("Search_D", "SH");

                ret = DbHelp.Proc_Search(sp);

                if (ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                gc_Item.DataSource = ret.ReturnDataSet.Tables[0];

                gv_Item.BestFitColumns();

                Item_Search_D(0);
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

        protected override void btnClose()
        {
            btn_Close.PerformClick();
        }

        protected override void btnExcel()
        {
            btn_Excel.PerformClick();
        }
        #endregion

        #region 버튼 이벤트
        private void btn_Select_Click(object sender, EventArgs e)
        {
            Search();
        }
    

        private void btn_Excel_Click(object sender, EventArgs e)
        {
            
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            MainForm.MainTab.TabPages.Remove(MainForm.MainTab.SelectedTabPage);
        }
        #endregion

        private void treeList_Bom_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e)
        {
            if (e.Node["BPart_Code"].NullString() == "100")
                e.NodeImageIndex = 0;
            else if (e.Node["BPart_Code"].NullString() == "200")
                e.NodeImageIndex = 1;
            else
                e.NodeImageIndex = 2;
        }

        private void gv_Item_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0)
                return;

            Item_Search_D(e.FocusedRowHandle);
        }
    }
}

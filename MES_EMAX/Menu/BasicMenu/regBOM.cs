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
using DevExpress.Utils;

namespace MES
{
    public partial class regBOM : BaseReg
    {
        ReturnStruct ret = new ReturnStruct();
        DataSet ds = new DataSet();
        DataTable dt_Tree = new DataTable();

        private string sAddNewRow = "N";

        public regBOM()
        {
            InitializeComponent();
        }

        private void regBOM_Load(object sender, EventArgs e)
        {
            Grid_Set();

            Search();

            //treeList_Bom.ClearNodes();
            //Create_Columns();
        }

        private void Grid_Set()
        {
            DbHelp.GridSet(gc_Item, gv_Item, "Item_Code", "품목코드", "120", false, false, true, false);
            DbHelp.GridSet(gc_Item, gv_Item, "Item_Name", "품목명", "150", false, false, true, false);
            DbHelp.GridSet(gc_Item, gv_Item, "SSize", "규격", "120", false, false, true, false);
            DbHelp.GridSet(gc_Item, gv_Item, "Item_BPart", "품목구분", "120", false, false, true, false);
            DbHelp.GridSet(gc_Item, gv_Item, "Bom_Chk", "등록여부", "900", false, false, true, false);

            gc_Item.AddRowYN = false;
            gc_Item.PopMenuChk = false;
            gc_Item.MouseWheelChk = false;

            gc_BOM.AddRowYN = true;
            DbHelp.GridSet(gc_BOM, gv_BOM, "Sort_No", "No", "", false, false, false, true);
            DbHelp.GridSet(gc_BOM, gv_BOM, "SItem_Code", "품목코드", "120", false, true, true, true);
            DbHelp.GridSet(gc_BOM, gv_BOM, "Item_Name", "품목명", "150", false, false, true, true);
            DbHelp.GridSet(gc_BOM, gv_BOM, "SSize", "규격", "150", false, false, true, true);
            DbHelp.GridSet(gc_BOM, gv_BOM, "Item_BPart", "품목구분", "80", false, false, true, true);
            DbHelp.GridSet(gc_BOM, gv_BOM, "Ccolor", "색상", "80", false, false, false, true);
            DbHelp.GridSet(gc_BOM, gv_BOM, "Qty", "수량", "80", true, true, true, true);
            DbHelp.GridSet(gc_BOM, gv_BOM, "Loss_Per", "Loss", "80", true, true, true, true);
            DbHelp.GridSet(gc_BOM, gv_BOM, "Real_Qty", "실소요량", "80", true, true, true, true);
            DbHelp.GridSet(gc_BOM, gv_BOM, "Bom_Bigo", "비고", "150", false, true, true, true);
            DbHelp.GridSet(gc_BOM, gv_BOM, "Use_Ck", "사용여부", "70", false, true, true, true);

            DbHelp.GridColumn_NumSet(gv_BOM, "Qty", ForMat.SetDecimal(this.Name, "Qty1"));
            DbHelp.GridColumn_NumSet(gv_BOM, "Loss_Per", ForMat.SetDecimal(this.Name, "Qty1"));
            DbHelp.GridColumn_NumSet(gv_BOM, "Real_Qty", ForMat.SetDecimal(this.Name, "Qty1"));

            DbHelp.GridColumn_CheckBox(gv_BOM, "Use_Ck");

            DbHelp.GridColumn_Help(gv_BOM, "SItem_Code", "Y");
            RepositoryItemButtonEdit button_Help_M1 = (RepositoryItemButtonEdit)gv_BOM.Columns["SItem_Code"].ColumnEdit;
            button_Help_M1.Buttons[0].Click += new EventHandler(grid_Item_Help);
            gv_BOM.Columns["SItem_Code"].ColumnEdit = button_Help_M1;

            gv_BOM.OptionsView.ShowAutoFilterRow = false;

            gc_BOM.DeleteRowEventHandler += new EventHandler(Delete_D);

            //SetUp_Tree.Tree_Columns_Set(treeList_Bom, "품목코드", "Item_Code", 120, true);
            //SetUp_Tree.Tree_Columns_Set(treeList_Bom, "거래처품번", "CItem_Code", 120, true);
            //SetUp_Tree.Tree_Columns_Set(treeList_Bom, "품목명", "Item_Name", 120, true);
            //SetUp_Tree.Tree_Columns_Set(treeList_Bom, "품목구분", "Item_BPart", 80, true);
            //SetUp_Tree.Tree_Columns_Set(treeList_Bom, "규격", "SSize", 100, true);
            //SetUp_Tree.Tree_Columns_Set(treeList_Bom, "수량", "Qty", 80, true);
            //SetUp_Tree.Tree_Columns_Set(treeList_Bom, "자재구분코드", "BPart_Code", 120, false);
            //SetUp_Tree.Tree_Columns_Set(treeList_Bom, "모코드", "MCode", 0, false);
            //SetUp_Tree.Tree_Columns_Set(treeList_Bom, "자코드", "SCode", 0, false);

            //SetUp_Tree.Tree_Columns_Key(treeList_Bom, "SCode", "MCode");

            //treeList_Bom.ImageIndexFieldName = "Item_Code";
            //treeList_Bom.StateImageList = imageCollection1;

            //treeList_Bom.OptionsBehavior.ReadOnly = true;
            //treeList_Bom.OptionsBehavior.Editable = false;

            //treeList_Bom.OptionsView.RowImagesShowMode = DevExpress.XtraTreeList.RowImagesShowMode.InIndent;
        }

        #region 헬프창
        private void grid_Item_Help(object sender, EventArgs e)
        {
            int iRow = gv_BOM.GetFocusedDataSourceRowIndex();

            //string sMutiCHK = string.IsNullOrWhiteSpace(txt_RegUser.Text) ? "Y" : "N";

            if (string.IsNullOrWhiteSpace(gv_BOM.GetRowCellValue(iRow, "Item_Name").ToString()))
            {
                PopHelpForm Help_Form = new PopHelpForm("Item", "sp_Help_Item_Bom", gv_BOM.GetRowCellValue(iRow, "SItem_Code").ToString(), "Y"); //sMutiCHK
                Help_Form.sNotReturn = "Y";
                //Help_Form.Set_Value("'002', '003'", "", "", "", "");
                if (Help_Form.ShowDialog() == DialogResult.OK)
                {
                    foreach (DataRow row in Help_Form.drReturn)
                    {
                        gv_BOM.SetRowCellValue(iRow, "SItem_Code", row["Item_Code"].ToString());
                        if (!string.IsNullOrWhiteSpace(gv_BOM.GetRowCellValue(iRow, "SItem_Code").ToString()))
                        {
                            gv_BOM.SetRowCellValue(iRow, "Item_Name", row["Item_Name"].ToString());
                            gv_BOM.SetRowCellValue(iRow, "Item_BPart", row["Item_BPart"].ToString());
                            gv_BOM.SetRowCellValue(iRow, "Ssize", row["SSize"].ToString());
                            gv_BOM.SetRowCellValue(iRow, "Ccolor", row["Ccolor"].ToString());

                            iRow++;
                            if (iRow == gv_BOM.RowCount)
                                gv_BOM.AddNewRow();
                            else if (sAddNewRow == "Y")
                                DbHelp.NewRow_Add(gc_BOM, iRow);


                            gv_BOM.UpdateCurrentRow();
                        }

                        //Item_Search(iRow);
                    }

                    if(iRow == gv_BOM.RowCount)
                        gv_BOM.DeleteRow(iRow);
                    else if(sAddNewRow == "Y")
                        gv_BOM.DeleteRow(iRow);
                }
            }
        }

        #endregion

        #region 함수

        private void Search()
        {
            try
            {
                SqlParam sp = new SqlParam("sp_regBOM");
                sp.AddParam("Kind", "S");
                sp.AddParam("Search_D", "SH");

                ret = DbHelp.Proc_Search(sp);

                if(ret.ReturnChk != 0)
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

        //그리드 품목 조회
        private void Item_Search(int iRow)
        {
            try
            {
                SqlParam sp = new SqlParam("sp_regBOM");
                sp.AddParam("Kind", "S");
                sp.AddParam("Search_D", "SI");
                sp.AddParam("Item_Code", gv_BOM.GetRowCellValue(iRow, "SItem_Code").ToString());

                ret = DbHelp.Proc_Search(sp);

                if (ret.ReturnChk == 0)
                {
                    DataTable dt = DbHelp.Fill_Table(ret.ReturnDataSet.Tables[0]);
                    DataRow dr = dt.Rows[0];

                    gv_BOM.SetRowCellValue(iRow, "Ssize", dr["Ssize"].ToString());
                    gv_BOM.SetRowCellValue(iRow, "Ccolor", dr["Ccolor"].ToString());
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
        //품목 정보 조회
        private void Item_Search_D(int iRow)
        {
            try
            {
                SqlParam sp = new SqlParam("sp_regBOM");
                sp.AddParam("Kind", "S");
                sp.AddParam("Search_D", "SD");
                sp.AddParam("Item_Code", gv_Item.GetRowCellValue(iRow, "Item_Code").ToString());

                ret = DbHelp.Proc_Search(sp);

                if(ret.ReturnChk == 0)
                {
                    //txt_Ssize.Text = ret.ReturnDataSet.Tables[0].Rows[0]["Ssize"].ToString();
                    //txt_ItemCodeNM.Text = ret.ReturnDataSet.Tables[0].Rows[0]["ItemNM"].ToString();
                    //txt_CItem_Code.Text = ret.ReturnDataSet.Tables[0].Rows[0]["CItem_Code"].ToString();

                    //txt_RegDate.Text = ret.ReturnDataSet.Tables[0].Rows[0]["Reg_Date"].ToString();
                    //txt_RegUser.Text = ret.ReturnDataSet.Tables[0].Rows[0]["Reg_User"].ToString();
                    //txt_UpDate.Text = ret.ReturnDataSet.Tables[0].Rows[0]["Up_Date"].ToString();
                    //txt_UpUser.Text = ret.ReturnDataSet.Tables[0].Rows[0]["Up_User"].ToString();

                    gc_BOM.DataSource = ret.ReturnDataSet.Tables[1];

                    //treeList_Bom.ClearNodes();
                    //dt_Tree = ret.ReturnDataSet.Tables[2];
                    //if(dt_Tree.Rows.Count > 0)
                    //    Make_Root(dt_Tree.Select("MItem_Code = ''").CopyToDataTable(), "MItem_Code", "Item_Code"); //Make_Root(dt_Tree.Select("MItem_Code = '" + txt_ItemCode.Text + "'").CopyToDataTable(), "MItem_Code", "Item_Code");
                    //treeList_Bom.DataSource = ret.ReturnDataSet.Tables[2];

                    //treeList_Bom.ExpandAll();
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

        //에러 체크
        private bool Check_Err()
        {
            return true;
        }

        //삭제 D
        private void Delete_D(object sender, EventArgs e)
        {
            int iRow = gv_BOM.GetFocusedDataSourceRowIndex();

            try
            {
                SqlParam sp = new SqlParam("sp_regBOM");
                sp.AddParam("Kind", "D");
                sp.AddParam("Delete_D", "D");
                sp.AddParam("Item_Code", gv_Item.GetRowCellValue(gv_Item.FocusedRowHandle, "Item_Code").ToString());
                sp.AddParam("SItem_Code", gv_BOM.GetRowCellValue(iRow, "SItem_Code").ToString());
                sp.AddParam("Reg_User", GlobalValue.sUserID);

                ret = DbHelp.Proc_Save(sp);

                if(ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                Item_Search_D(gv_Item.FocusedRowHandle);

            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        //private void Insert_Row(int iRow)
        //{
        //    DataTable dt = gc_BOM.DataSource as DataTable;

        //    DataRow dr;

        //    dr = dt.NewRow();

        //    dt.Rows.InsertAt(dr, iRow);

        //    gc_BOM.DataSource = dt;
        //}

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

        protected override void btnCopy()
        {
            btn_Copy.PerformClick();
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

        #region 그리드 이벤트
        private void gv_BOM_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            if(e.Column.FieldName == "SItem_Code")
            {
                if (!string.IsNullOrWhiteSpace(e.Value.ToString()) && (gc_BOM.DataSource as DataTable).Select("SItem_Code = '" + e.Value.ToString() + "'").Length > 1)
                {
                    XtraMessageBox.Show("동일한 품목이 이미 등록이 되어 있습니다");
                    gv_BOM.SetRowCellValue(e.RowHandle, "SItem_Code", "");
                    return;
                }

                string sItem_Name = PopHelpForm.Return_Help("sp_Help_Item_Bom", gv_BOM.GetRowCellValue(e.RowHandle, "SItem_Code").ToString(), "", "'002', '003'");
                gv_BOM.SetRowCellValue(e.RowHandle, "Item_Name", sItem_Name);

                if (!string.IsNullOrWhiteSpace(sItem_Name))
                {
                    Item_Search(e.RowHandle);
                }
            }
            else if(e.Column.FieldName == "Qty")
            {
                decimal dQty = decimal.Parse(e.Value.NumString());
                decimal dLoos = decimal.Parse(gv_BOM.GetRowCellValue(e.RowHandle, "Loss_Per").NumString());

                decimal dReal = dQty * (100 + dLoos) / 100;

                gv_BOM.SetRowCellValue(e.RowHandle, "Real_Qty", dReal.NumString());
            }
            else if(e.Column.FieldName == "Loss_Per")
            {
                decimal dLoos = decimal.Parse(e.Value.NumString());
                decimal dQty = decimal.Parse(gv_BOM.GetRowCellValue(e.RowHandle, "Qty").NumString());

                decimal dReal = dQty * (100 + dLoos) / 100;

                gv_BOM.SetRowCellValue(e.RowHandle, "Real_Qty", dReal.NumString());
            }

            btn_Close.sUpdate = "Y";
            btn_Copy.sUpdate = "Y";
        }

        private void gc_BOM_EditorKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (gv_BOM.FocusedColumn == gv_BOM.Columns["SItem_Code"])
                {
                    grid_Item_Help(null, null);
                }
            }
        }

        #endregion

        #region 버튼 이벤트
        private void btn_Select_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void btn_Insert_Click(object sender, EventArgs e)
        {
            gv_BOM.AddNewRow();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (gv_BOM.RowCount < 1)
                return;

            if (!Check_Err())
                return;

            try
            {
                string sSItem_Code = "", sQty = "", sLoss = "", sReal_Qty = "", sPrice = "", sBigo = "";
                
                for(int i = 0; i < gv_BOM.RowCount; i++)
                {
                    if (!string.IsNullOrWhiteSpace(gv_BOM.GetRowCellValue(i, "Item_Name").ToString()))
                    {
                        sSItem_Code += gv_BOM.GetRowCellValue(i, "SItem_Code").ToString() + "_/";
                        sQty += gv_BOM.GetRowCellValue(i, "Qty").NumString() + "_/";
                        sLoss += gv_BOM.GetRowCellValue(i, "Loss_Per").NumString() + "_/";
                        sReal_Qty += gv_BOM.GetRowCellValue(i, "Real_Qty").NumString() + "_/";
                        sPrice += gv_BOM.GetRowCellValue(i, "P_Price").NumString() + "_/";
                        sBigo += gv_BOM.GetRowCellValue(i, "Bom_Bigo").ToString() + "_/";
                    }
                }

                SqlParam sp = new SqlParam("sp_regBOM");
                sp.AddParam("Kind", "I");
                sp.AddParam("Item_Code", gv_Item.GetRowCellValue(gv_Item.FocusedRowHandle, "Item_Code").ToString());
                sp.AddParam("SItem_Code", sSItem_Code);
                sp.AddParam("Qty", sQty);
                sp.AddParam("Loss", sLoss);
                sp.AddParam("Real_Qty", sReal_Qty);
                sp.AddParam("P_Price", sPrice);
                sp.AddParam("Bom_Bigo", sBigo);
                sp.AddParam("Reg_User", GlobalValue.sUserID);

                ret = DbHelp.Proc_Save(sp);
                
                if(ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                Item_Search_D(gv_Item.FocusedRowHandle);

                btn_Save.sCHK = "Y";

                btn_Close.sUpdate = "N";
                btn_Copy.sUpdate = "N";

            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                SqlParam sp = new SqlParam("sp_regBOM");
                sp.AddParam("Kind", "D");
                sp.AddParam("Delete_D", "H");
                sp.AddParam("Item_Code", gv_Item.GetRowCellValue(gv_Item.FocusedRowHandle, "Item_Code").ToString());

                ret = DbHelp.Proc_Save(sp);

                if(ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                Item_Search_D(gv_Item.FocusedRowHandle);

                btn_Delete.sCHK = "Y";
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        private void btn_Excel_Click(object sender, EventArgs e)
        {
            FileIF.Excel_Down(gc_BOM, "BOM 등록");
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            if (btn_Close.Result_Update == DialogResult.Yes)
            {
                if (!Check_Err())
                    return;
                btn_Save_Click(null, null);
            }

            MainForm.MainTab.TabPages.Remove(MainForm.MainTab.SelectedTabPage);
        }
        #endregion

        private void gc_BOM_NewRowAdd(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            sAddNewRow = "Y";
        }

        //private void treeList_Bom_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e)
        //{
        //    if (e.Node["BPart_Code"].NullString() == "001")
        //        e.NodeImageIndex = 0;
        //    else if (e.Node["BPart_Code"].NullString() == "002")
        //        e.NodeImageIndex = 1;
        //    else
        //        e.NodeImageIndex = 2;
        //}

        private void btn_Copy_Click(object sender, EventArgs e)
        {
            if (btn_Copy.Result_Update == DialogResult.Yes)
            {
                if (!Check_Err())
                    return;
                btn_Save_Click(null, null);
            }

            if (gv_BOM.RowCount > 0)
            {
                PopBOMCopyForm Form = new PopBOMCopyForm(gc_BOM.DataSource as DataTable);
                Form.sItem_Code = gv_Item.GetRowCellValue(gv_Item.FocusedRowHandle, "Item_Code").ToString();
                Form.sItem_Name = gv_Item.GetRowCellValue(gv_Item.FocusedRowHandle, "Item_Name").ToString();
                Form.StartPosition = FormStartPosition.CenterScreen;
                if (Form.ShowDialog() == DialogResult.OK)
                {
                    btn_Copy.sCHK = "Y";
                }
            }
        }

        private void gv_Item_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0)
                return;

            Item_Search_D(e.FocusedRowHandle);
        }
    }
}

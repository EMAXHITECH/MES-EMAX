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
using DevExpress.XtraGrid.Views.Grid;
using System.IO;

namespace MES
{
    public partial class regItem : BaseReg
    {
        ReturnStruct ret = new ReturnStruct();
        DataSet ds = new DataSet();

        public regItem()
        {
            InitializeComponent();
        }

        private void regItem_Load(object sender, EventArgs e)
        {
            Grid_Set();
            Search_Data();

            //btn_Save.Text = "I/F";
        }

        private void Grid_Set()
        {
            DbHelp.GridSet(gc_Item, gv_Item, "Item_Code", "품목코드", "120", false, false, true);
            DbHelp.GridSet(gc_Item, gv_Item, "Item_BPartNM", "품목구분", "100", false, false, true);
            DbHelp.GridSet(gc_Item, gv_Item, "Item_PartNM", "품목그룹", "100", false, false, true);
            DbHelp.GridSet(gc_Item, gv_Item, "Item_Name", "품목명", "120", false, false, true);
            DbHelp.GridSet(gc_Item, gv_Item, "Ssize", "규격", "150", false, false, true);
            DbHelp.GridSet(gc_Item, gv_Item, "Q_UnitNM", "수량단위", "80", false, false, true);
            DbHelp.GridSet(gc_Item, gv_Item, "Supply_MethodNM", "조달방범", "80", false, false, true);
            DbHelp.GridSet(gc_Item, gv_Item, "Po_MethodNM", "구매형태", "80", false, false, true);
            DbHelp.GridSet(gc_Item, gv_Item, "Stock_Qty", "안전재고", "80", false, false, true);
            DbHelp.GridSet(gc_Item, gv_Item, "Qc_KindNM", "검사유형", "100", false, false, true);
            DbHelp.GridSet(gc_Item, gv_Item, "Qc_PartNM", "검사구분", "100", false, false, true);
            DbHelp.GridSet(gc_Item, gv_Item, "Qc_PartCode", "검사구분", "100", false, false, false);
            DbHelp.GridSet(gc_Item, gv_Item, "Lead_Time", "리드타임 ", "100", false, false, true);
            DbHelp.GridSet(gc_Item, gv_Item, "Use_Ck", "사용유무", "80", false, false, true);
            DbHelp.GridSet(gc_Item, gv_Item, "Item_Image", "이미지", "100", false, false, true);
            DbHelp.GridSet(gc_Item, gv_Item, "Image_URL", "이미지", "100", false, false, false);
            DbHelp.GridSet(gc_Item, gv_Item, "Reg_Date", "등록일자", "100", false, false, true);
            DbHelp.GridSet(gc_Item, gv_Item, "Reg_User_Name", "등록자", "100", false, false, true);
            DbHelp.GridSet(gc_Item, gv_Item, "Up_Date", "수정일자", "100", false, false, true);
            DbHelp.GridSet(gc_Item, gv_Item, "Up_User_Name", "수정자", "100", false, false, true);
            gc_Item.PopMenuChk = false;

            DbHelp.GridColumn_CheckBox(gv_Item, "Use_Ck");
            DbHelp.GridColumn_NumSet(gv_Item, "Stock_Qty", ForMat.SetDecimal(this.Name, "Qty1"));
            DbHelp.GridColumn_NumSet(gv_Item, "Lead_Time", ForMat.SetDecimal(this.Name, "Price1"));

            //FileIF.Set_URL();

            RepositoryItemPictureEdit item_Image = new RepositoryItemPictureEdit();
            item_Image.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            item_Image.NullText = " ";

            gv_Item.Columns["Item_Image"].ColumnEdit = item_Image;
        }

        #region 버튼 이벤트
        private void btn_Select_Click(object sender, EventArgs e)
        {
            Search_Data();
        }

        private void btn_Insert_Click(object sender, EventArgs e)
        {
            PopItemForm PopForm = new PopItemForm();
            PopForm.StartPosition = FormStartPosition.CenterScreen;

            if (PopForm.ShowDialog() == DialogResult.OK)
                btn_Select_Click(null, null);
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if(gv_Item.RowCount > 0 && gv_Item.FocusedRowHandle >= 0)
            {
                string code = gv_Item.GetFocusedRowCellValue("Item_Code").ToString();

                SqlParam sp = new SqlParam("sp_regItem");
                sp.AddParam("Kind", "D");
                sp.AddParam("Item_Code", code);

                ret = DbHelp.Proc_Save(sp);

                if(ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                btn_Delete.sCHK = "Y";
                btn_Select_Click(null, null);
            }
        }

        private void btn_Excel_Click(object sender, EventArgs e)
        {
            FileIF.Excel_Down(gc_Item, this.Name);
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            MainForm.MainTab.TabPages.Remove(MainForm.MainTab.SelectedTabPage);
        }

        #endregion

        #region 버튼 상속
        protected override void btnSelect()
        {
            btn_Select.PerformClick();
        }

        protected override void btnInsert()
        {
            btn_Insert.PerformClick();
        }

        protected override void btnDelete()
        {
            btn_Delete.PerformClick();
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

        #region 그리드 이벤트
        private void gv_Item_DoubleClick(object sender, EventArgs e)
        {
            if (gv_Item.RowCount > 0 && gv_Item.FocusedRowHandle >= 0)
            {
                string code = gv_Item.GetFocusedRowCellValue("Item_Code").ToString();

                PopItemForm PopForm = new PopItemForm();
                PopForm.Item_Code = code;
                PopForm.StartPosition = FormStartPosition.CenterScreen;

                if (PopForm.ShowDialog() == DialogResult.OK)
                    btn_Select_Click(null, null);
            }
        }
        #endregion

        #region 기타 이벤트 및 메소드
        private void Search_Data()
        {
            SqlParam sp = new SqlParam("sp_regItem");
            sp.AddParam("Kind", "S");

            ret = DbHelp.Proc_Search(sp);


            if (ret.ReturnChk != 0)
            {
                XtraMessageBox.Show(ret.ReturnMessage);
                return;
            }

            ds = ret.ReturnDataSet;

            gc_Item.DataSource = ds.Tables[0];
            gc_Item.RefreshDataSource();
            gv_Item.BestFitColumns();

            Search_Image();
        }

        private void Search_Image()
        {
            for (int i = 0; i < gv_Item.RowCount; i++)
            {
                if (!string.IsNullOrWhiteSpace(gv_Item.GetRowCellValue(i, "Image_URL").NullString()))
                {
                    gv_Item.SetRowCellValue(i, "Item_Image", (byte[])gv_Item.GetRowCellValue(i, "Image_URL"));
                }
                else
                {
                    gv_Item.SetRowCellValue(i, "Item_Image", null); // Image_Help.ImageToByte(item_image));
                }
            }

            gv_Item.UpdateCurrentRow();
        }

        #endregion
    }
}

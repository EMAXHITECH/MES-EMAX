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

namespace MES
{
    public partial class rptEquip : BaseReg
    {
        ReturnStruct ret = new ReturnStruct();
        DataSet ds = new DataSet();

        public rptEquip()
        {
            InitializeComponent();
        }

        private void rptEquip_Load(object sender, EventArgs e)
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
            DbHelp.GridSet(GridMaster, ViewMaster, "Equip_Code", "설비코드", "100", false, false, true);
            DbHelp.GridSet(GridMaster, ViewMaster, "Equip_Name", "설비명", "120", false, false, true);
            DbHelp.GridSet(GridMaster, ViewMaster, "Equip_PartNM", "설비구분", "120", false, false, true);
            DbHelp.GridSet(GridMaster, ViewMaster, "Short_Name", "구입처", "120", false, false, true);
            DbHelp.GridSet(GridMaster, ViewMaster, "Make_Date", "제작일자", "120", false, false, true);
            DbHelp.GridSet(GridMaster, ViewMaster, "Short_Name1", "제작업체", "120", false, false, true);
            DbHelp.GridSet(GridMaster, ViewMaster, "Make_No", "제작번호", "120", false, false, true);
            DbHelp.GridSet(GridMaster, ViewMaster, "Equip_PosNM", "설비위치", "120", false, false, true);
            DbHelp.GridSet(GridMaster, ViewMaster, "Dis_Date", "폐기일자", "80", true, false, true);
            DbHelp.GridSet(GridMaster, ViewMaster, "Eq_Image", "이미지", "120", false, false, true);
            DbHelp.GridSet(GridMaster, ViewMaster, "M_Memo", "메모", "120", false, false, true);

            RepositoryItemPictureEdit pictureEdit = new RepositoryItemPictureEdit();
            pictureEdit.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            pictureEdit.NullText = " ";
            ViewMaster.Columns["Eq_Image"].ColumnEdit = pictureEdit;
            ViewMaster.Columns["Eq_Image"].OptionsColumn.FixedWidth = true;
            ViewMaster.Columns["Eq_Image"].Width = 70;
            ViewMaster.RowHeight = 50;

            RepositoryItemMemoEdit Memo_Spec = new RepositoryItemMemoEdit();
            ViewMaster.Columns["M_Memo"].ColumnEdit = Memo_Spec;

            //ViewMaster.OptionsView.RowAutoHeight = true;// MemoEdit 때문에 행 높이 자동 설정

            DbHelp.No_ReadOnly(ViewMaster);

            GridMaster.MouseWheelChk = false;
            GridMaster.PopMenuChk = false;
        }

        private void btn_Select_Click(object sender, EventArgs e)
        {
            Search_Data();
        }

        private void btn_Excel_Click(object sender, EventArgs e)
        {
            FileIF.Excel_Down(GridMaster, this.Name);
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            MainForm.MainTab.TabPages.Remove(MainForm.MainTab.SelectedTabPage);
        }

        private void Search_Data()
        {
            SqlParam sp = new SqlParam("sp_rptEquip");
            sp.AddParam("Kind", "M");
            ret = DbHelp.Proc_Search(sp);

            if (ret.ReturnChk != 0)
            {
                XtraMessageBox.Show(ret.ReturnMessage);
                return;
            }
            GridMaster.DataSource = DbHelp.Fill_Table(ret.ReturnDataSet.Tables[0]);
            GridMaster.RefreshDataSource();
            ViewMaster.BestFitColumns();

            //ViewMaster_FocusedRowChanged(gv_EquipS, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, 0));
            Set_Images();
        }

        private void Set_Images()
        {
            //for (int i = 0; i < ViewMaster.RowCount; i++)
            //{
            //    DataTable Table = FileIF.Get_File_List(GlobalValue.Basic_URL + "regEquip");

            //    DataRow[] Rows = (Table == null) ? null : Table.Select("File_Name LIKE '" + ViewMaster.GetRowCellValue(i, "Equip_Code").NullString() + "%'");
            //    if (Rows != null && Rows.Count() > 0)
            //    {
            //        string Path = GlobalValue.Basic_URL + "regEquip/" + Rows[0]["File_Name"].NullString();
            //        Image item_image = null;

            //        if (!string.IsNullOrWhiteSpace(Path))
            //        {
            //            item_image = FileIF.FTP_Download_Image(Path);
            //            ViewMaster.SetRowCellValue(i, "Eq_Image", Image_Help.ImageToByte(item_image));
            //        }
            //        else
            //        {
            //            ViewMaster.SetRowCellValue(i, "Eq_Image", null);
            //        }
            //    }
            //}
        }

        #region 버튼 상속
        protected override void btnSelect()
        {
            btn_Select.PerformClick();
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

        private void ViewMaster_DoubleClick(object sender, EventArgs e)
        {
            //int iRow = ViewMaster.FocusedRowHandle;

            //if (iRow < 0)
            //    return;
            //try
            //{
            //    //byte[] sImage = (byte[])ViewMaster.GetRowCellValue(iRow, "Eq_Image");

            //    //if (sImage == null)
            //    //    return;

            //    //PopEquipImageForm Img_Form = new PopEquipImageForm(sImage);
            //    //Img_Form.StartPosition = FormStartPosition.CenterParent;
            //    //Img_Form.ShowDialog();
            //}
            //catch (Exception)
            //{

            //}
        }

        private void ViewMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }


    }
}

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
    public partial class PopBOMCopyForm : BaseForm
    {
        DataTable BOM = new DataTable();
        public string sItem_Code, sItem_Name;
        public PopBOMCopyForm()
        {
            InitializeComponent();
        }

        public PopBOMCopyForm(DataTable Items)
        {
            InitializeComponent();
            if (Items != null)
                BOM = Items.Copy();
        }

        private void PopBOMCopyForm_Load(object sender, EventArgs e)
        {
            Grid_Set();
            Search_Data();

            txt_ItemCode.Text = sItem_Code;
            txt_ItemName.Text = sItem_Name;
        }
        
        private void Grid_Set()
        {
            DbHelp.GridSet(Grid_Items, View_Items, "Check_Box", "Check", "50", false, true, true);
            DbHelp.GridSet(Grid_Items, View_Items, "Bom_Chk", "등록여부", "100", false, false, true); 
            DbHelp.GridSet(Grid_Items, View_Items, "Item_Code", "품목코드", "100", false, false, true);
            DbHelp.GridSet(Grid_Items, View_Items, "Item_Name", "품목명", "100", false, false, true);
            DbHelp.GridSet(Grid_Items, View_Items, "Ssize", "규격", "100", false, false, true);
            DbHelp.GridSet(Grid_Items, View_Items, "Q_UnitNM", "단위", "100", false, false, true);
            DbHelp.GridSet(Grid_Items, View_Items, "BPart", "품목구분", "100", false, false, true);
            DbHelp.GridSet(Grid_Items, View_Items, "Part", "품목그룹", "100", false, false, true); 

            DbHelp.GridColumn_CheckBox(View_Items, "Check_Box");
        }

        private void Search_Data()
        {
            SqlParam sp = new SqlParam("sp_regBOM");
            sp.AddParam("Kind", "S");
            sp.AddParam("Search_D", "SL");

            ReturnStruct ret = DbHelp.Proc_Search(sp);

            if (ret.ReturnChk != 0)
            {
                XtraMessageBox.Show(ret.ReturnMessage);
                return;
            }

            Grid_Items.DataSource = ret.ReturnDataSet.Tables[0];
            Grid_Items.RefreshDataSource();
            View_Items.BestFitColumns();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            DataRow[] Rows = (Grid_Items.DataSource as DataTable).Select("Check_Box = 'Y'");

            DataRow BOM_Row = DbHelp.Summary_Data_2(BOM, "SItem_Code", new string[] { "Sort_No", "SItem_Code", "Qty", "Loss_Per", "Real_Qty", "P_Price", "Bom_Bigo" });

            foreach (DataRow Row in Rows)
            {
                SqlParam sp = new SqlParam("sp_regBOM");
                sp = new SqlParam("sp_regBOM");
                sp.AddParam("Kind", "D");
                sp.AddParam("Delete_D", "H");
                sp.AddParam("Item_Code", Row["Item_Code"].NullString());

                ReturnStruct ret_1 = DbHelp.Proc_Save(sp);
                if (ret_1.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret_1.ReturnMessage);
                    return;
                }

                sp = new SqlParam("sp_regBOM");

                sp.AddParam("Kind", "I");
                sp.AddParam("Item_Code", Row["Item_Code"].NullString());
                sp.AddParam("SItem_Code", BOM_Row["SItem_Code"].NullString());
                sp.AddParam("Qty", BOM_Row["Qty"].NullString());
                sp.AddParam("Loss", BOM_Row["Loss_Per"].NullString());
                sp.AddParam("Real_Qty", BOM_Row["Real_Qty"].NullString());
                sp.AddParam("P_Price", BOM_Row["P_Price"].NullString());
                sp.AddParam("Bom_Bigo", BOM_Row["Bom_Bigo"].NullString());
                sp.AddParam("Reg_User", GlobalValue.sUserID);

                ReturnStruct ret_2 = DbHelp.Proc_Save(sp);
                if (ret_2.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret_2.ReturnMessage);
                    return;
                }
            }

            btn_Save.sCHK = "Y";
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
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
    public partial class PopRoutCopyForm : BaseForm
    {
        private ReturnStruct ret = new ReturnStruct();
        public string sItem_Code, sItem_Name;
        public PopRoutCopyForm()
        {
            InitializeComponent();
        }

        private void PopRoutCopyForm_Load(object sender, EventArgs e)
        {
            Grid_Set();
            Search_Data();

            txt_ItemCode.Text = sItem_Code;
            txt_ItemName.Text = sItem_Name;
        }
        
        private void Grid_Set()
        {
            DbHelp.GridSet(Grid_Items, View_Items, "Check_Box", "Check", "50", false, true, true);
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
            SqlParam sp = new SqlParam("sp_regRouting");
            sp.AddParam("Kind", "S");
            sp.AddParam("Search_D", "SI");
            sp.AddParam("Item_Code", sItem_Code);

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
            try
            {
                string sCopyItem = "";

                for (int i = 0; i < View_Items.RowCount; i++)
                {
                    if (View_Items.GetRowCellValue(i, "Check_Box").ToString() == "Y")
                    {
                        sCopyItem += View_Items.GetRowCellValue(i, "Item_Code").ToString() + "_/";
                    }
                }

                SqlParam sp = new SqlParam("sp_regRouting");
                sp.AddParam("Kind", "C");
                sp.AddParam("Item_Code", sItem_Code);
                sp.AddParam("CopyItem", sCopyItem); 

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
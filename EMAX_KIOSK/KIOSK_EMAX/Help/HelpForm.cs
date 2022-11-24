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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace KIOSK_EMAX
{
    public partial class HelpForm : DevExpress.XtraEditors.XtraForm
    {
        ReturnStruct ret = new ReturnStruct();
        private string sHelpGB = "", sProc = "";
        bool Multi_Select = false;
        string[] Params = new string[5];

        public DataRow Row_Return;
        public DataTable Table_Return;

        public HelpForm(string sHelpGB, string sProc, bool Multi_Select = false, params string[] Parameters)
        {
            InitializeComponent();

            this.sHelpGB = sHelpGB;
            this.sProc = sProc;
            this.Multi_Select = Multi_Select;

            int n = 0;

            foreach (string Param in Parameters)
            {
                if (n < 5)
                    Params[n] = Param;
                else if (n >= 5)
                    break;
                n++;
            }
        }

        private void HelpForm_Load(object sender, EventArgs e)
        {
            if (sProc == "")
            {
                this.Close();
                return;
            }

            Grid_Set();
        }

        private void Grid_Set()
        {
            try
            {
                SqlParam sp = new SqlParam("sp_HelpMenu");
                sp.AddParam("HelpGB", sHelpGB);

                ret = DbHelp.Proc_Search(sp);

                DataTable dt;

                if (ret.ReturnChk == 0)
                {
                    label_Name.Text = ret.ReturnDataSet.Tables[0].Rows[0]["Help_Name"].ToString();
                    dt = ret.ReturnDataSet.Tables[1];

                    if (dt.Rows.Count < 1)
                    {
                        XtraMessageBox.Show("폼에 지정된 칼럼이 없습니다");
                        this.DialogResult = DialogResult.Cancel;
                        this.Close();
                        return;
                    }
                    else
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DbHelp.GridSet(gc_Help, gv_Help, dt.Rows[i]["Column_Code"].ToString(), dt.Rows[i]["Column_Name"].ToString(), dt.Rows[i]["Size"].ToString(), false, false, dt.Rows[i]["Use_Chk"].ToString() == "Y" ? true : false);
                            if (dt.Rows[i]["Num"].ToString() != "")
                            {
                                DbHelp.GridColumn_NumSet(gv_Help, dt.Rows[i]["Column_Code"].ToString(), int.Parse(dt.Rows[i]["Num"].NumString()));
                            }
                        }
                    }
                }
                else
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    return;
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }

            Search();
        }

        private void Search()
        {
            try
            {
                SqlParam sp = new SqlParam(sProc);
                for (int i = 0; i < Params.Length; i++)
                {
                    if (Params[i] != null)
                        sp.AddParam("value" + (i + 1).NumString(), Params[i]);
                }

                ret = DbHelp.Proc_Search(sp);

                if (ret.ReturnChk == 0)
                {
                    if (Multi_Select)       // 체크박스 컬럼 추가
                    {
                        DbHelp.GridSet(gc_Help, gv_Help, ret.ReturnDataSet.Tables[0].Columns[0].ColumnName, "선택", "80", false, true, true);
                        DbHelp.GridColumn_CheckBox(gv_Help, ret.ReturnDataSet.Tables[0].Columns[0].ColumnName);

                        gv_Help.Columns[ret.ReturnDataSet.Tables[0].Columns[0].ColumnName].VisibleIndex = 0;
                        gv_Help.Columns[ret.ReturnDataSet.Tables[0].Columns[0].ColumnName].AbsoluteIndex = 0;
                        gv_Help.Columns[0].OptionsColumn.AllowEdit = true;
                        gv_Help.Columns[0].OptionsColumn.ReadOnly = false;
                    }
                    //else
                    //    gv_Help.Columns[0].Visible = false;

                    gc_Help.DataSource = ret.ReturnDataSet.Tables[0];
                    gv_Help.BestFitColumns();
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

        private void btn_Select_Click(object sender, EventArgs e)
        {
            int iRow = gv_Help.FocusedRowHandle;

            if (iRow < 0)
                return;

            Row_Return = gv_Help.GetDataRow(iRow);

            Table_Return = Row_Return.Table.Clone();

            if (Multi_Select) // 체크박스 존재시 체크된 것만
            {
                DataRow[] Checked_Rows = (gc_Help.DataSource as DataTable).Select(gv_Help.Columns[0].FieldName + " = 'Y'");

                if (Checked_Rows != null)
                {
                    foreach (DataRow Checked in Checked_Rows)
                    {
                        Table_Return.ImportRow(Checked);
                    }
                }
            }
            else
                Table_Return.ImportRow(Row_Return);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void gv_Help_DoubleClick(object sender, EventArgs e)
        {
            btn_Select_Click(sender, null);
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 체크박스 옆 컬럼 클릭시 체크값 변경
        private void gv_Help_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (Multi_Select && e.Column.VisibleIndex == 1)
            {
                string Reverse = (gv_Help.GetRowCellValue(e.RowHandle, gv_Help.Columns[0].FieldName).NullString() == "Y") ? "N" : "Y";
                Color Row_BackColor = (gv_Help.GetRowCellValue(e.RowHandle, gv_Help.Columns[0].FieldName).NullString() == "Y") ? Color.FromArgb(255, 255, 255) : Color.FromArgb(152, 251, 152);

                gv_Help.SetRowCellValue(e.RowHandle, gv_Help.Columns[0].FieldName, Reverse);

                // gv_Help.Appearance.FocusedRow.BackColor = Row_BackColor;
                // gv_Help.Appearance.FocusedRow.BackColor2 = Row_BackColor;
                // gv_Help.Appearance.FocusedRow.ForeColor = Color.FromArgb(0, 0, 0);
            }
        }
    }
}
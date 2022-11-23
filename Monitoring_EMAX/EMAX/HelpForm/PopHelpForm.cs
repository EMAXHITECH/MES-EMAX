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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Controls;

namespace EMAX_Monitoring
{
    public partial class PopHelpForm : XtraForm
    {
        public DataRow Single_Result;
        ReturnStruct ret = new ReturnStruct();
        DataSet ds = new DataSet();

        public DataRow[] drReturn;
        private DataTable temp = new DataTable();

        private string sHelpGB = "", sProc = "";
        private string sValue1 = "", sValue2 = "", sValue3 = "", sValue4 = "", sValue5 = "";
        private string sMultiYN = "N";

        public string sBasic = "", sGMCODE = "";

        public string sLevelYN = "N";

        public string sRtCode = "", sRtCodeNm = "";

        public string sNotReturn = "N"; //하나라도 값이 바로 리턴 안되도록 처리

        public PopHelpForm(string sHelpGB, string sProc, string sBasic, string sMultiYN = "N")
        {
            InitializeComponent();

            this.sHelpGB = sHelpGB;
            this.sProc = sProc;
            this.sBasic = sBasic;
            this.sMultiYN = sMultiYN;
        }

        public PopHelpForm(string sHelpGB, string sProc, string sGMCODE, string sBasic, string sMultiYN = "N") : this(sHelpGB, sProc, sBasic, sMultiYN)
        {
            this.sGMCODE = sGMCODE;
        }

        private void gv_Help_DoubleClick(object sender, EventArgs e)
        {
            if (gv_Help.RowCount < 1)
                return;
            if (gv_Help.FocusedRowHandle < 0 || gv_Help.GetSelectedRows().Count() == 0) return;
            int iRow = gv_Help.GetSelectedRows()[0];

            if (iRow < 0)
                return;

            if (gc_Help.MultiSelectChk)
            {
                drReturn = new DataRow[1];
                drReturn[0] = gv_Help.GetDataRow(iRow);                        
            }
            else
            {
                DataRow row = temp.Rows[temp.Rows.IndexOf(gv_Help.GetFocusedDataRow())];

                sRtCode = row[0].ToString();
                sRtCodeNm = row[1].ToString();

                drReturn = new DataRow[1];
                drReturn[0] = row;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void gv_Help_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                if (gv_Help.RowCount < 1)
                    return;

                if (gc_Help.MultiSelectChk)
                {
                    if(gv_Help.SelectedRowsCount == 0)
                    {
                        gv_Help.SelectRow(0);
                    }

                    drReturn = new DataRow[gv_Help.SelectedRowsCount];
                    int j = 0;
                    for (int i = 0; i < gv_Help.RowCount; i++)
                    {
                        if (gv_Help.IsRowSelected(i))
                        {
                            drReturn[j] = gv_Help.GetDataRow(i);
                            j++;
                        }
                    }
                }
                else
                {
                    int iSelectRow = gv_Help.GetSelectedRows()[0]; //무조건 하나 선택

                    DataRow row = temp.Rows[temp.Rows.IndexOf(gv_Help.GetFocusedDataRow())];
                    sRtCode = row[0].ToString();
                    sRtCodeNm = row[1].ToString();

                    drReturn = new DataRow[1];
                    drReturn[0] = row;
                    //sRtCode = gv_Help.GetRowCellValue(iSelectRow, gv_Help.Columns[0].FieldName).ToString();
                    //sRtCodeNm = gv_Help.GetRowCellValue(iSelectRow, gv_Help.Columns[1].FieldName).ToString();
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Tab)
                return true;

            if (!gc_Help.MultiSelectChk)
                return base.ProcessCmdKey(ref msg, keyData);

            Keys key = keyData & ~(Keys.Control);

            switch (key)
            {
                case Keys.A:
                    if((keyData & Keys.Control) != 0)
                    {
                        gv_Help.SelectAll();
                    }
                    break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void PopHelpForm_Load(object sender, EventArgs e)
        {
            gc_Help.CellFocus = false;

            if(sProc == "")
            {
                this.Close();
                return;
            }

            //txt_All.Text = sBasic;

            if(sMultiYN == "Y")
            {
                gc_Help.MultiSelectChk = true;
            }

            Grid_Set();

            if(gv_Help.RowCount < 1 && sNotReturn == "N")
            {
                XtraMessageBox.Show("조건에 해당하는 정보가 없습니다");
                sBasic = "";
                btn_Search_Click(null, null);
                //this.DialogResult = DialogResult.No;
                //this.Close();
            }
        }

        private void PopHelpForm_Shown(object sender, EventArgs e)
        {
            FindControl find = gv_Help.GridControl.Controls.Find("FindControlCore", true)[0] as FindControl;
            find.FindEdit.Text = sBasic;
            find.FindEdit.Focus();

            find.FindEdit.KeyPress += new KeyPressEventHandler(find_edit_KeyPress);
        }

        private void find_edit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                if (gv_Help.RowCount > 0)
                    gv_Help.Focus();
            }
        }

        public void Set_Value(string sValue1, string sValue2, string sValue3, string sValue4, string sValue5)
        {
            this.sValue1 = sValue1;
            this.sValue2 = sValue2;
            this.sValue3 = sValue3;
            this.sValue4 = sValue4;
            this.sValue5 = sValue5;
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
                        MessageBox.Show("폼에 지정된 칼럼이 없습니다");
                        this.DialogResult = DialogResult.Cancel;
                        this.Close();
                        return;
                    }
                    else
                    {
                        for(int i = 0; i < dt.Rows.Count; i++)
                        {
                            string Column_Code = dt.Rows[i]["Column_Code"].NullString();
                            string Column_Name = dt.Rows[i]["Column_Name"].NullString();
                            bool Use_Chk = dt.Rows[i]["Use_Chk"].ToString() == "Y" ? true : false;
                            int Size = Convert.ToInt32(dt.Rows[i]["Size"].NumString());
                            int Number = (string.IsNullOrWhiteSpace(dt.Rows[i]["Num"].NullString())) ? -1 : int.Parse(dt.Rows[i]["Num"].NumString());

                            DbHelp.GridSet(gc_Help, gv_Help, Column_Code, Column_Name, Size, Number, false, Use_Chk, false);
                            //if(dt.Rows[i]["Num"].NullString() != "")
                            //{
                            //    DbHelp.GridColumn_NumSet(gv_Help, dt.Rows[i]["Column_Code"].ToString(), int.Parse(dt.Rows[i]["Num"].NumString()));
                            //}
                        }
                    }
                }
                else
                {
                    MessageBox.Show(ret.ReturnMessage);
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    return;
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }

            btn_Search_Click(null, null);
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            try
            {
                SqlParam sp = new SqlParam(sProc);
                sp.AddParam("Basic", sBasic); //txt_All.Text
                sp.AddParam("Value1", sValue1);
                sp.AddParam("Value2", sValue2);
                sp.AddParam("Value3", sValue3);
                sp.AddParam("Value4", sValue4);
                sp.AddParam("Value5", sValue5);
                if (sGMCODE != "") //공통코드 처리
                    sp.AddParam("GMCODE", sGMCODE);
                if (sLevelYN == "Y")
                    sp.AddParam("User_Code", GlobalValue.sUserID);

                ret = DbHelp.Proc_Search(sp);

                if(ret.ReturnChk == 0)
                {
                    if (sNotReturn == "N")
                    {
                        //하나밖에 없을 경우 바로 보내기
                        if (ret.ReturnDataSet.Tables[0].Rows.Count == 1)
                        {
                            Single_Result = ret.ReturnDataSet.Tables[0].Rows[0];
                            sRtCode = ret.ReturnDataSet.Tables[0].Rows[0][0].ToString();
                            sRtCodeNm = ret.ReturnDataSet.Tables[0].Rows[0][1].ToString();

                            drReturn = new DataRow[1];
                            drReturn[0] = Single_Result;

                            this.DialogResult = DialogResult.OK;
                            this.Close();

                            gc_Help.DataSource = ret.ReturnDataSet.Tables[0];
                            temp = DbHelp.Fill_Table(ret.ReturnDataSet.Tables[0]);
                            return;
                        }
                    }

                    gc_Help.DataSource = ret.ReturnDataSet.Tables[0];
                    temp = DbHelp.Fill_Table(ret.ReturnDataSet.Tables[0]);
                }
                else
                {
                    MessageBox.Show(ret.ReturnMessage);
                    return;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void btn_Select_Click(object sender, EventArgs e)
        {
            if (gc_Help.MultiSelectChk)
            {
                drReturn = new DataRow[gv_Help.SelectedRowsCount];
                int j = 0;
                for(int i = 0; i < gv_Help.RowCount; i++)
                {
                    if (gv_Help.IsRowSelected(i))
                    {
                        drReturn[j] = gv_Help.GetDataRow(i);
                        j++;
                    }
                }
            }
            else
            {
                int iSelectRow = gv_Help.GetSelectedRows()[0]; //무조건 하나 선택

                DataRow row = temp.Rows[temp.Rows.IndexOf(gv_Help.GetFocusedDataRow())];
                sRtCode = row[0].ToString();
                sRtCodeNm = row[1].ToString();
                //sRtCode = gv_Help.GetRowCellValue(iSelectRow, gv_Help.Columns[0].FieldName).ToString();
                //sRtCodeNm = gv_Help.GetRowCellValue(iSelectRow, gv_Help.Columns[1].FieldName).ToString();
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public static string Return_Help(string sProc, string sCode, string sGMCODE = "", string sValue1 = "")
        {
            ReturnStruct ret_R = new ReturnStruct();

            try
            {
                SqlParam sp = new SqlParam(sProc);
                sp.AddParam("GB", "2");
                sp.AddParam("Basic", sCode);
                sp.AddParam("Value1", sValue1);
                if (sGMCODE != "") //공통코드 처리
                    sp.AddParam("GMCODE", sGMCODE);

                ret_R = DbHelp.Proc_Search(sp);
                if (ret_R.ReturnChk == 0)
                {
                    if (ret_R.ReturnDataSet.Tables[0].Rows.Count == 0)
                        return "";

                    return ret_R.ReturnDataSet.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    return "";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }
        }

        public static DataRow Return_Help_Row(string sProc, string sCode, string sGMCODE = "", string sValue1 = "")
        {
            ReturnStruct ret_R = new ReturnStruct();

            try
            {
                SqlParam sp = new SqlParam(sProc);
                sp.AddParam("GB", "2");
                sp.AddParam("Basic", sCode);
                sp.AddParam("Value1", sValue1);
                if (sGMCODE != "") //공통코드 처리
                    sp.AddParam("GMCODE", sGMCODE);

                ret_R = DbHelp.Proc_Search(sp);
                if (ret_R.ReturnChk == 0)
                {
                    if (ret_R.ReturnDataSet.Tables[0].Rows.Count != 1)
                        return null;

                    return ret_R.ReturnDataSet.Tables[0].Rows[0];
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
    }
}

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

namespace MES
{
    public partial class PopHelpForm : XtraForm
    {
        public DataRow Single_Result;
        ReturnStruct ret = new ReturnStruct();
        DataSet ds = new DataSet();

        public DataRow[] drReturn;
        private DataTable temp = new DataTable();
        public DataTable Selected = new DataTable();        // 선택된 로우의 순서 기억 - 해당 순서대로 해당 테이블에 저장

        private string sHelpGB = "", sProc = "";
        private string sValue1 = "", sValue2 = "", sValue3 = "", sValue4 = "", sValue5 = "";
        private string sMultiYN = "N";

        public string sBasic = "", sGMCODE = "";

        public string sLevelYN = "N";

        public string sRtCode = "", sRtCodeNm = "";

        public string sNotReturn = "N"; //하나라도 값이 바로 리턴 안되도록 처리

        //중복 체크 변수
        private string sRep_Ck = "N";
        private string sCheck_Columns = "";

        private string[] sKey_Columns;

        public DataTable dt_Check = null;
        private DataTable dt_Value = null;

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

        private void PopHelpForm_Load(object sender, EventArgs e)
        {
            gc_Help.CellFocus = false;

            if (sProc == "")
            {
                this.Close();
                return;
            }

            if (sMultiYN == "Y")
            {
                gc_Help.MultiSelectChk = true;
            }

            Grid_Set();

            if (gv_Help.RowCount < 1 && sNotReturn == "N")
            {
                XtraMessageBox.Show("조건에 해당하는 정보가 없습니다");
                sBasic = "";
                btn_Search_Click(null, null);
            }
        }

        private void PopHelpForm_Shown(object sender, EventArgs e)
        {
            FindControl Find = gv_Help.GridControl.Controls.Find("FindControlCore", true)[0] as FindControl;
            Find.FindEdit.Text = sBasic;
            Find.FindEdit.Focus();

            Find.FindEdit.KeyPress += new KeyPressEventHandler(Find_Edit_KeyPress);
        }

        private void Find_Edit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
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
                    sRep_Ck = ret.ReturnDataSet.Tables[0].Rows[0]["Rep_Ck"].ToString();

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
                        string sColumns = "";
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DbHelp.GridSet(gc_Help, gv_Help, dt.Rows[i]["Column_Code"].ToString(), dt.Rows[i]["Column_Name"].ToString(), dt.Rows[i]["Size"].ToString(), false, false, dt.Rows[i]["Use_Chk"].ToString() == "Y" ? true : false);
                            if (dt.Rows[i]["Num"].ToString() != "")
                            {
                                DbHelp.GridColumn_NumSet(gv_Help, dt.Rows[i]["Column_Code"].ToString(), int.Parse(dt.Rows[i]["Num"].NumString()));
                                //값 체크할 칼럼
                                if (dt.Rows[i]["Key_Ck"].ToString() == "Y")
                                    sCheck_Columns = dt.Rows[i]["Column_Code"].ToString();
                            }
                            else if(dt.Rows[i]["Key_Ck"].ToString() == "Y")
                            {
                                sColumns += dt.Rows[i]["Column_Code"].ToString() + "/";
                            }
                        }

                        //Key 칼럼
                        if (!string.IsNullOrWhiteSpace(sColumns))
                        {
                            sColumns = sColumns.Substring(0, sColumns.Length - 1);
                            sKey_Columns = sColumns.Split('/');
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
            catch (Exception ex)
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

                if (ret.ReturnChk == 0)
                {
                    if (sNotReturn == "N")
                    {
                        //하나밖에 없을 경우 바로 보내기
                        if (ret.ReturnDataSet.Tables[0].Rows.Count == 1)
                        {
                            Single_Result = ret.ReturnDataSet.Tables[0].Rows[0];
                            sRtCode = ret.ReturnDataSet.Tables[0].Rows[0][0].ToString();
                            sRtCodeNm = ret.ReturnDataSet.Tables[0].Rows[0][1].ToString();
                            this.DialogResult = DialogResult.OK;
                            this.Close();

                            gc_Help.DataSource = ret.ReturnDataSet.Tables[0];
                            temp = DbHelp.Fill_Table(ret.ReturnDataSet.Tables[0]);
                            return;
                        }
                    }

                    DataTable Table = ret.ReturnDataSet.Tables[0].Copy();
                    // Table에 클러스터 인덱스 추가 => PK 지정  => 컬럼 위치는 맨 뒤로

                    DataColumn Col = new DataColumn("ID", typeof(int));

                    Table.Columns.Add(Col);

                    int Index = 0;

                    foreach (DataRow Row in Table.Rows)
                    {
                        Row.SetField(Col, ++Index);
                    }

                    gc_Help.DataSource = Table;
                    temp = DbHelp.Fill_Table(Table);

                    Selected = Table.Clone();

                    DataColumn[] cols = new DataColumn[1] { Selected.Columns[Selected.Columns.Count - 1] };  // 클러스터 인덱스 -> PK로 지정
                    Selected.PrimaryKey = cols;

                    if (dt_Check != null)
                    {
                        Columns_Key();
                        Key_Check();
                    }
                }
                else
                {
                    MessageBox.Show(ret.ReturnMessage);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
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

                Selected.ImportRow(row);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btn_Select_Click(object sender, EventArgs e)
        {
            if (gc_Help.MultiSelectChk)
            {
                //drReturn = new DataRow[gv_Help.SelectedRowsCount];
                //int j = 0;
                //for (int i = 0; i < gv_Help.RowCount; i++)
                //{
                //    if (gv_Help.IsRowSelected(i))
                //    {
                //        drReturn[j] = gv_Help.GetDataRow(i);
                //        j++;
                //    }
                //}

                Keep_Selected_Order();
            }
            else
            {
                int iSelectRow = gv_Help.GetSelectedRows()[0]; //무조건 하나 선택

                DataRow row = temp.Rows[temp.Rows.IndexOf(gv_Help.GetFocusedDataRow())];
                sRtCode = row[0].ToString();
                sRtCodeNm = row[1].ToString();

                Selected.ImportRow(row);
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

                    //drReturn = new DataRow[gv_Help.SelectedRowsCount];
                    //int j = 0;
                    //for (int i = 0; i < gv_Help.RowCount; i++)
                    //{
                    //    if (gv_Help.IsRowSelected(i))
                    //    {
                    //        drReturn[j] = gv_Help.GetDataRow(i);
                    //        j++;
                    //    }
                    //}

                    Keep_Selected_Order();
                }
                else
                {
                    int iSelectRow = gv_Help.GetSelectedRows()[0]; //무조건 하나 선택

                    DataRow row = temp.Rows[temp.Rows.IndexOf(gv_Help.GetFocusedDataRow())];
                    sRtCode = row[0].ToString();
                    sRtCodeNm = row[1].ToString();

                    Selected.ImportRow(row);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void Keep_Selected_Order()
        {
            if (drReturn != null)
                drReturn = null;

            drReturn = new DataRow[Selected.Rows.Count];

            for (int i = 0; i < Selected.Rows.Count; i++)
            {
                drReturn[i] = Selected.Rows[i];
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

        

        // 선택된 순서 유지
        private void gv_Help_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            if (e.ControllerRow < 0 && gv_Help.FocusedRowHandle < 0)
                return;

            if (e.Action == CollectionChangeAction.Add)
                Selected.ImportRow(gv_Help.GetDataRow(e.ControllerRow));
            else if (e.Action == CollectionChangeAction.Remove)// Select 해제될 때 Row 삭제
            {
                string Where = Selected.PrimaryKey[0] + " = " + gv_Help.GetDataRow(e.ControllerRow)["ID"].NullString();

                DataRow[] Exists = Selected.Select(Where);

                if (Exists != null && Exists.Count() == 1)
                {
                    Selected.Rows.Remove(Exists[0]);
                }
            }
            else if (e.Action == CollectionChangeAction.Refresh)
            {
                Selected.Clear();

                foreach (int Index in gv_Help.GetSelectedRows())
                {
                    Selected.ImportRow(gv_Help.GetDataRow(Index));
                }
            }
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
                sp.AddParam("GB", "3"); //Row 형식
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

        //중복 체크 함수
        #region 중복 체크 함수
        //해당 테이블 Key 칼럼 설정
        private void Columns_Key()
        {
            if (gv_Help.RowCount > 0)
                dt_Value = (gc_Help.DataSource as DataTable);

            if(dt_Value != null)
            {
                DataColumn[] Columns_Key = new DataColumn[sKey_Columns.Length];

                for(int i = 0; i < sKey_Columns.Length; i++)
                {
                    DataColumn Column = dt_Value.Columns[sKey_Columns[i]];
                    Columns_Key[i] = Column;
                }

                dt_Value.PrimaryKey = Columns_Key;
            }
        }

        private void Key_Check()
        {
            try
            {
                DataRow row_Check;
                string sKey_Value = "";
                //값 체크
                if (sRep_Ck == "Y")
                {
                    for (int i = 0; i < dt_Check.Rows.Count; i++)
                    {
                        row_Check = dt_Check.Rows[i];
                        Type type;
                        for (int j = 0; j < sKey_Columns.Length; j++)
                        {
                            type = dt_Value.Columns[sKey_Columns[j].ToString()].DataType;
                            if(type == typeof(int))
                                sKey_Value += row_Check[sKey_Columns[j].ToString()].NumString() + ",";
                            else
                                sKey_Value += row_Check[sKey_Columns[j].ToString()].ToString() + ",";
                        }

                        DataRow dr_Value = dt_Value.Rows.Find(sKey_Value.Substring(0, sKey_Value.Length - 1).Split(','));

                        if (dr_Value != null)
                        {
                            dr_Value[sCheck_Columns] = decimal.Parse(dr_Value[sCheck_Columns].NumString()) - decimal.Parse(row_Check[sCheck_Columns].NumString());

                            if (decimal.Parse(dr_Value[sCheck_Columns].NumString()) <= 0)
                                dt_Value.Rows.Remove(dr_Value);
                        }

                        sKey_Value = "";
                    }
                }
                else // 중복 없애기
                {
                    for (int i = 0; i < dt_Check.Rows.Count; i++)
                    {
                        row_Check = dt_Check.Rows[i];
                        Type type;
                        for (int j = 0; j < sKey_Columns.Length; j++)
                        {
                            type = dt_Value.Columns[sKey_Columns[j].ToString()].DataType;
                            if(type == typeof(int))
                                sKey_Value += row_Check[sKey_Columns[j].ToString()].NumString() + ",";
                            else
                                sKey_Value += row_Check[sKey_Columns[j].ToString()].ToString() + ",";
                        }

                        //중복 데이터 삭제
                        DataRow dr_Delete = dt_Value.Rows.Find(sKey_Value.Substring(0, sKey_Value.Length - 1).Split(','));
                        if(dr_Delete != null)
                            dt_Value.Rows.Remove(dr_Delete);

                        sKey_Value = "";
                    }
                }
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        #endregion
    }
}

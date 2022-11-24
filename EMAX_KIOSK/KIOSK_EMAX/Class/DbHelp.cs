using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;
using System.Drawing;
using DevExpress.XtraEditors.Controls;

namespace KIOSK_EMAX
{
    public class DbHelp
    {

        //커넥션 객체
        private static SqlConnection conn = null;

        public static string DBConnString { get; private set; }
        public static bool bDBConnCheck = false;
        private static int errorBoxCount = 0;
        /// <summary>
        /// 생성자
        /// </summary>
        public DbHelp() { }


        public static SqlConnection DBConn
        {
            get
            {
                if (!ConnectToDB())
                {
                    return null;
                }
                return conn;
            }
        }

        /// <summary>
        /// Database 접속 시도
        /// </summary>
        /// <returns></returns>
        public static bool ConnectToDB()
        {
            if (conn == null)
            {
                //StreamReader SR = new StreamReader(Application.StartupPath + @"\SqlMap.config");
                //string sConfig = SR.ReadToEnd();
                //sConfig.IndexOf("connectionString");
                //sConfig = sConfig.Substring(sConfig.IndexOf("connectionString"), 160);
                //sConfig = sConfig.Substring(sConfig.IndexOf(";") + 1);
                //sConfig = sConfig.Substring(0, sConfig.IndexOf("/>") - 1);
                //DBConnString = sConfig;

                //App.config 이용(21.07.22.)
                DBConnString = Configurations.GetConfig("DBConnstring");

                //서버명, 초기 DB명, 인증 방법
                conn = new SqlConnection(DBConnString);
            }
            try
            {
                if (!IsDBConnected)
                {
                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        bDBConnCheck = true;
                    }
                    else
                    {
                        bDBConnCheck = false;
                    }
                }
            }
            catch (SqlException e)
            {
                errorBoxCount++;
                if (errorBoxCount == 1)
                {
                    XtraMessageBox.Show(e.Message, "DBHelper - ConnectToDB()");
                }
                return false;
            }
            return true;
        }


        /// <summary>
        /// Database Open 여부 확인
        /// </summary>
        public static bool IsDBConnected
        {
            get
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    return false;
                }
                return true;
            }
        }

        /// <summary>
        /// Database 해제
        /// </summary>
        public static void Close()
        {
            if (IsDBConnected)
                DBConn.Close();
        }

        public static void Clear()
        {
            if (conn != null)
                conn = null;
        }


        public static DataTable Adapter(StringBuilder Query)
        {
            try
            {
                if (Query.Length <= 0 || Query == null)
                    return null;
                else
                {
                    DataTable table = new DataTable();
                    ConnectToDB();

                    var adapter = new SqlDataAdapter(Query.ToString(), DBConn);
                    adapter.Fill(table);

                    return table;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static void Command(StringBuilder Query)
        {
            try
            {
                if (Query.Length <= 0 || Query == null)
                    return;
                else
                {
                    ConnectToDB();
                    SqlCommand comm = new SqlCommand(Query.ToString(), DBConn);
                    comm.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {

            }
        }

        //파리미터 반환
        public static string Pr(string Parameter)
        {
            Parameter = "'" + Parameter + "'";
            return Parameter;
        }

        //파리미터 반환
        public static Int32 PrInt(string Parameter)
        {
            int Rint = 0;

            if (Parameter == null)
            {
                return Rint;
            }
            if (Parameter.Trim().Length != 0)
            {
                Rint = Convert.ToInt32(Parameter.Replace(",", ""));
            }
            return Rint;
        }

        //체크박스 파리미터 반환
        public static string PrChk(CheckEdit Parameter)
        {
            string sParameter = "N";
            if (Parameter.Checked)
            {
                sParameter = "Y";
            }
            sParameter = "'" + sParameter + "'";
            return sParameter;
        }

        //오프젝트 체크박스 파라미터 변환
        public static bool PrChkObj(Object Parameter)
        {
            bool bParameter = true;
            if (Convert.ToString(Parameter) == "N")
            {
                bParameter = false;
            }
            return bParameter;

        }

        /// <summary>
        /// SEQDay 생성
        /// </summary>
        /// <param name="sDateTime"> SEQ생성 시간 </param>
        /// <param name="sTable"> 생성테이블명 </param>
        /// <param name="sDateColumn"> 기준 Date 컬럼</param>
        /// <returns></returns>
        public static string SeqDay(string sDateTime, string sTable, string sDateColumn)
        {
            StringBuilder Sb = new StringBuilder();
            Sb.Append(" (SELECT " + sDateTime + " + ");
            Sb.Append("   REPLICATE(0, 4 - LEN(COUNT(*)))  + CAST(COUNT(*) AS VARCHAR)");
            Sb.Append("  FROM " + sTable);
            Sb.Append(" WHERE REPLACE(CONVERT(DATE, " + sDateColumn + "), '-', '') = REPLACE(CONVERT(DATE, GETDATE()), '-', '')) ");
            return Sb.ToString();
        }

        public static string Simple_Seq(int i)
        {
            if(i >= 0 && i < 100)
            {
                if (i < 10)
                {
                    return "00" + i.ToString();
                }
                else
                {
                    return "0" + i.ToString();
                }
            }
            else if(i >= 100)
            {
                return i.ToString();
            }
            else
            {
                return (i * -1).ToString();
            }
        }

        //조회 Transaction 미사용
        public static ReturnStruct Proc_Search(SqlParam sp)
        {
            ReturnStruct ret = new ReturnStruct();
            DataSet ds = new DataSet();

            try
            {
                SqlCommand cmd = new SqlCommand(sp.sProcName, DBConn);
                cmd.CommandTimeout = 60;
                cmd.CommandType = CommandType.StoredProcedure;

                foreach(SqlParameter sqlParameter in sp.SetSqlParameter())
                {
                    cmd.Parameters.Add(sqlParameter);
                }

                SqlDataAdapter sqladp = new SqlDataAdapter(cmd);

                sqladp.Fill(ds);
                ret.ReturnChk = 0;
                ret.ReturnDataSet = ds;
                ret.ReturnMessage = "";
            }
            catch(Exception ex)
            {
                ret.ReturnChk = -999999;
                ret.ReturnMessage = ex.Message;
            }
            finally
            {
                Close();
            }

            return ret;
        }

        //저장, 수정 Transaction 사용
        public static ReturnStruct Proc_Save(SqlParam sp)
        {
            ReturnStruct ret = new ReturnStruct();
            DataSet ds = new DataSet();
            SqlTransaction trans = null;

            try
            {
                trans = DBConn.BeginTransaction();

                SqlCommand cmd = new SqlCommand(sp.sProcName, DBConn);
                cmd.CommandTimeout = 3600;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = trans;

                foreach(SqlParameter sqlParameter in sp.SetSqlParameter())
                {
                    cmd.Parameters.Add(sqlParameter);
                }

                SqlDataAdapter sqladp = new SqlDataAdapter(cmd);

                sqladp.Fill(ds);
                ret.ReturnChk = 0;
                ret.ReturnDataSet = ds;
                ret.ReturnMessage = "";

                trans.Commit();

            }
            catch(Exception ex)
            {
                ret.ReturnChk = -999999;
                ret.ReturnMessage = ex.Message;

                if(trans != null)
                {
                    trans.Rollback();
                }
            }
            finally
            {
                Close();
            }

            return ret;
        }

        public static GridView GridSet(GridControl Grid, GridView View, string sColumn, string sCaption, string sWidth, bool bNumeric, bool bEdit, bool bVisible, bool bEditForm=false)
        {
            GridColumn column = View.Columns.AddVisible(sColumn, sCaption);
            View.Columns.Add(column);
            if (!bVisible)
            {
                column.Visible = false;
            }

            if (sWidth.Trim().Length != 0)
            {
                if (sWidth == "0")
                {
                    column.BestFit();
                }
                else
                {
                    View.Columns[sColumn].Width = Convert.ToInt32(sWidth);
                }
            }
            else
            {
                column.BestFit();
            }
            if (bNumeric)
            {
                View.Columns[sColumn].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                View.Columns[sColumn].DisplayFormat.FormatString = "{0:n0}";
            }

            if (!bEdit)
            {
                if (bEditForm)
                {
                    View.Columns[sColumn].AppearanceCell.BackColor = Color.LightGray;
                }
                column.OptionsColumn.AllowEdit = false;
                column.OptionsColumn.ReadOnly = true;
            }
            else
            {
                column.OptionsColumn.AllowEdit = true;
                column.OptionsColumn.ReadOnly = false;
            }

            //옵션
            //View.OptionsView.AllowCellMerge = false;
            //View.OptionsView.ShowAutoFilterRow = true;
            View.OptionsView.ShowGroupPanel = false;
            //View.OptionsView.BestFitMode = GridBestFitMode.Full;
            View.OptionsView.ColumnAutoWidth = false;
            //View.OptionsView.GroupDrawMode = false;




            Grid.UseEmbeddedNavigator = true;
            ControlNavigator navigator = Grid.EmbeddedNavigator;
            navigator.TextStringFormat = "Record {0} / {1}";
            navigator.Buttons.BeginUpdate();

            navigator.Buttons.Edit.Visible = false;
            navigator.Buttons.EndEdit.Visible = false;
            navigator.Buttons.Append.Visible = false;
            navigator.Buttons.Remove.Visible = false;
            navigator.Buttons.CancelEdit.Visible = false;
            navigator.Buttons.EndUpdate();

            return View;
        }
        
        public static void GridColumn_Data(GridView View, string sColumn)
        {
            RepositoryItemDateEdit edit_Date = new RepositoryItemDateEdit();
            edit_Date.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            edit_Date.Mask.EditMask = "yyyy-MM-dd";
            edit_Date.Mask.UseMaskAsDisplayFormat = true;
            edit_Date.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            edit_Date.EditFormat.FormatString = "yyyy-MM-dd";

            View.Columns[sColumn].ColumnEdit = edit_Date;
        }

        public static void GridColumn_NumSet(GridView View, string sColumn, int iDecimal)
        {
            RepositoryItemTextEdit NumEdit = new RepositoryItemTextEdit();
            NumEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            NumEdit.Mask.EditMask = "n" + iDecimal.ToString();
            NumEdit.Mask.UseMaskAsDisplayFormat = true;
            NumEdit.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            NumEdit.EditFormat.FormatString = "n" + iDecimal.ToString();

            View.Columns[sColumn].ColumnEdit = NumEdit;

            //if (View.Columns[sColumn].DisplayFormat.FormatType == DevExpress.Utils.FormatType.Numeric)
            //{
            //    View.Columns[sColumn].DisplayFormat.FormatString = "n" + iDecimal.ToString();
            //}
        }

        public static void GridColumn_CheckBox(GridView View, string sColumn)
        {
            RepositoryItemCheckEdit edit_Chk = new RepositoryItemCheckEdit();
            edit_Chk.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            edit_Chk.CheckBoxOptions.Style = CheckBoxStyle.SvgCheckBox1;
            edit_Chk.CheckBoxOptions.SvgImageSize = new Size(30, 30);
            edit_Chk.ValueChecked = "Y";
            edit_Chk.ValueUnchecked = "N";

            View.Columns[sColumn].ColumnEdit = edit_Chk;
        }

        public static void GridColumn_Help(GridView View, string sColumn, string sRequiredCHK)
        {
            RepositoryItemButtonEdit button_Help = new RepositoryItemButtonEdit();
            button_Help.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Search;

            View.Columns[sColumn].ColumnEdit = button_Help;

            //헬프창 명 받아오는 칼럼 수정 불가능하도록 처리
            int iColmun = View.Columns[sColumn].AbsoluteIndex + 1;
            View.Columns[iColmun].OptionsColumn.ReadOnly = true;
            View.Columns[sColumn].AppearanceCell.BackColor =  Color.FromArgb(252, 247, 182);

            if (sRequiredCHK == "Y")
            {
                View.Columns[sColumn].AppearanceHeader.ForeColor = Color.FromArgb(44, 85, 152);
                View.Columns[sColumn].AppearanceHeader.Font = new System.Drawing.Font("맑은 고딕", 20F, FontStyle.Bold);
                string col_name = View.Columns[sColumn].Caption;
                View.Columns[sColumn].Caption = "* " + col_name;
            }
        }

        // 뷰, 적용 컬럼, 룩업 데이터테이블, 셀값 컬럼, 셀 뷰 컬럼, 룩업 테이블 필드명, 룩업 테이블 필드 캡션
        public static void GridColumn_LookUp(GridView View, string Edit_Column , DataTable Table, string Value_Col, string Display_Col, string[] Display_Fields, string[] Display_Caps)
        {
            if (Table != null && Table.Rows.Count > 0)
            {
                RepositoryItemLookUpEdit LookUp = new RepositoryItemLookUpEdit();
                LookUp.NullText = "";
                LookUp.ValueMember = Value_Col;

                for (int i = 0; i < Display_Fields.Count(); i++)
                {
                    if (Table.Columns.Contains(Display_Fields[i]))
                    {
                        string Caption = (Display_Caps.Count() >= i) ? Display_Caps[i] : "";

                        LookUp.Columns.Add(new LookUpColumnInfo(Display_Fields[i], Caption));
                        LookUp.DisplayMember = Display_Col;
                    }
                }

                LookUp.DataSource = Table;
                LookUp.BestFit();

                View.Columns[Edit_Column].ColumnEdit = LookUp;
            }
            
        }

        public static DataTable Fill_Table(DataTable Table)
        {
            Type type;
            foreach (DataRow row in Table.Rows)
            {
                foreach (DataColumn col in Table.Columns)
                {
                    if (row[col] == DBNull.Value)
                    {
                        type = row[col].GetType();

                        if (type == typeof(string))
                        {
                            row[col] = "";
                        }
                        else if (type == typeof(int) || type == typeof(decimal))
                        {
                            row[col] = 0;
                        }
                        else if (type == typeof(DateTime))
                        {
                            row[col] = DateTime.Now;
                        }
                    }
                }
            }

            return Table;
        }

        public static void Clear_Panel(PanelControl panel)
        {
            Type type;
            foreach(Control control in panel.Controls)
            {
                type = control.GetType();
                if(type == typeof(TextEdit))
                {
                    control.Text = "";
                }
                else if(type == typeof(DateEdit))
                {
                    control.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
                else if(type == typeof(ButtonEdit))
                {
                    control.Text = "";
                }
                else if(type == typeof(PictureEdit))
                {
                    (control as PictureEdit).Image = null;
                }
                else if(type == typeof(CheckEdit))
                {
                    (control as CheckEdit).EditValue = "Y";
                }
                else if(type == typeof(MemoEdit))
                {
                    control.Text = "";
                }
            }
        }

        public static DataTable Return_DT(GridView gridView)
        {
            DataTable dt = new DataTable();

            for(int i = 0; i < gridView.Columns.Count; i++)
            {
                if (gridView.Columns[i].ColumnEdit != null)
                {
                    if(gridView.Columns[i].ColumnEdit.GetType() == typeof(RepositoryItemTextEdit) && gridView.Columns[i].ColumnEdit.EditFormat.FormatType == DevExpress.Utils.FormatType.Numeric)
                    {
                        dt.Columns.Add(gridView.Columns[i].FieldName, typeof(decimal));
                    }
                    else if(gridView.Columns[i].ColumnEdit.GetType() == typeof(RepositoryItemDateEdit))
                    {
                        dt.Columns.Add(gridView.Columns[i].FieldName, typeof(DateTime));
                    }
                    else //if(gridView.Columns[i].ColumnEdit.GetType() == typeof(RepositoryItemMemoEdit))
                    {
                        dt.Columns.Add(gridView.Columns[i].FieldName, typeof(string));
                    }
                }
                else
                {
                    dt.Columns.Add(gridView.Columns[i].FieldName, typeof(string));
                    //dt.Columns.Add(gridView.Columns[i].FieldName, gridView.Columns[i].ColumnType);
                }
            }

            return dt;
        }

        public static DataRow Summary_Data(GridView view, string necessity, params string[] columns)// view, 필수값, 컬럼들
        {
            try
            {
                DataTable Table = new DataTable();

                foreach (string col in columns)
                {
                    Table.Columns.Add(col);
                }
                Table.Rows.Add();
                DataRow Row = Table.Rows[0];
                

                foreach (string col in columns)
                {
                    string sum = "";
                    Type Data_Type = (view.GridControl.DataSource as DataTable).Columns[col].DataType;

                    for (int i = 0; i < view.RowCount; i++)
                    {
                        if (string.IsNullOrWhiteSpace(view.GetRowCellValue(i, necessity).NullString())) // 필수 컬럼의 값 존재여부 확인
                            continue;

                        if (Data_Type == typeof(int) || Data_Type == typeof(double) || Data_Type == typeof(decimal) || Data_Type == typeof(long) || Data_Type == typeof(float) || Data_Type == typeof(short))
                            sum += view.GetRowCellValue(i, col).NumString() + "_/";
                        else
                            sum += view.GetRowCellValue(i, col).NullString() + "_/";
                    }

                    Row[col] = sum;
                }

                return Row;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString());
                return null;
            }
        }

        public static DataRow Summary_Data_2(DataTable data, string necessity, params string[] columns) // DataTable, 필수값, 컬럼들
        {
            try
            {
                DataTable Table = new DataTable();

                foreach (string col in columns)
                {
                    Table.Columns.Add(col);
                }
                Table.Rows.Add();
                DataRow Row = Table.Rows[0];


                foreach (string col in columns)
                {
                    string sum = "";
                    Type Data_Type = data.Columns[col].DataType;

                    foreach (DataRow dr in data.Rows)
                    {
                        // 필수값 체크가 존재하는 경우
                        if (!string.IsNullOrWhiteSpace(necessity))
                            // 필수값이 빈 경우 패스
                            if (string.IsNullOrWhiteSpace(dr[necessity].NullString()))
                                continue;

                        if (Data_Type == typeof(int) || Data_Type == typeof(double) || Data_Type == typeof(decimal) || Data_Type == typeof(long) || Data_Type == typeof(float) || Data_Type == typeof(short))
                            sum += dr[col].NumString() + "_/";
                        else
                            sum += dr[col].NullString() + "_/";
                    }

                    Row[col] = sum;
                }

                return Row;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString());
                return null;
            }
        }

        // 일반 현황 풋터
        public static void Footer_Set(GridView view, string mask_len, string[] columns) // 그리드뷰, 소숫점 길이, Sum할 컬럼들
        {
            view.OptionsView.ShowFooter = true;

            foreach (string col in columns)
            {
                GridColumnSummaryItem summaryItem = new GridColumnSummaryItem();
                summaryItem.FieldName = col;
                summaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                summaryItem.DisplayFormat = "{0:n" + mask_len + "}";

                if (view.Columns[col].Summary.Count == 0)
                    view.Columns[col].Summary.Add(summaryItem);
            }
        }

        // 집계 풋터
        public static void Group_Footer_Set(GridView view, string mask_len)
        {
            view.OptionsView.ShowFooter = true;

            foreach (GridColumn col in view.Columns)
            {
                if (col.Caption.Contains("월") || col.FieldName == "Total")
                {
                    GridColumnSummaryItem summaryItem = new GridColumnSummaryItem();
                    summaryItem.FieldName = col.FieldName;
                    summaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                    summaryItem.DisplayFormat = "{0:n" + mask_len + "}";

                    if (view.Columns[col.FieldName].Summary.Count == 0)
                        view.Columns[col.FieldName].Summary.Add(summaryItem);
                }
            }
        }

        public static void No_ReadOnly(GridView view)
        {
            foreach (GridColumn col in view.Columns)
            {
                if(col.FieldName.Contains("No"))
                {
                    col.OptionsColumn.AllowEdit = true;
                    col.OptionsColumn.ReadOnly = true;
                }
            }
        }

        public static string[] Set_Default(string GM_Code)
        {
            SqlParam sp = new SqlParam("sp_General_Default");
            sp.AddParam("GM_Code", GM_Code);

            ReturnStruct temp_ret = DbHelp.Proc_Search(sp);

            if (temp_ret.ReturnDataSet.Tables[0].Rows.Count > 0)
            {
                DataRow row = temp_ret.ReturnDataSet.Tables[0].Rows[0];
                string gs_code = row["GS_Code"].NullString();
                string gs_name = row["GS_Name"].NullString();

                return new string[] { gs_code, gs_name };
            }
            else
                return null;
        }
    }
}

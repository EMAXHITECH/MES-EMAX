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
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Scrolling;

namespace KIOSK_EMAX
{
    public partial class regKioWorkResult : DevExpress.XtraEditors.XtraForm
    {
        ReturnStruct ret = new ReturnStruct();
        Timer timer_Search = new Timer();

        public regKioWorkResult()
        {
            InitializeComponent();
        }

        private void regKioWorkResult_Load(object sender, EventArgs e)
        {
            Grid_Set();

            LookUp_Set();

            timer_Search.Tick += Timer_Search;
            timer_Search.Interval = 5000;
            timer_Search.Start();
        }

        private void Grid_Set()
        {
            DbHelp.GridSet(gc_Sheet, gv_Sheet, "WIdx_No", "지시키", "", false, false, false);
            DbHelp.GridSet(gc_Sheet, gv_Sheet, "Work_Sort", "지시Sort", "", false, false, false);
            DbHelp.GridSet(gc_Sheet, gv_Sheet, "Work_No", "지시번호", "200", false, false, true);
            DbHelp.GridSet(gc_Sheet, gv_Sheet, "Work_Date", "지시일자", "200", false, false, true);
            DbHelp.GridSet(gc_Sheet, gv_Sheet, "Company_Name", "사업장", "200", false, false, false);
            DbHelp.GridSet(gc_Sheet, gv_Sheet, "Item_Code", "품목코드", "200", false, false, true);
            DbHelp.GridSet(gc_Sheet, gv_Sheet, "Item_Name", "품목명", "200", false, false, true);
            DbHelp.GridSet(gc_Sheet, gv_Sheet, "SSize", "규격", "200", false, false, true);
            DbHelp.GridSet(gc_Sheet, gv_Sheet, "Process_Code", "공정코드", "80", false, false, false);
            DbHelp.GridSet(gc_Sheet, gv_Sheet, "Process_Name", "공정", "80", false, false, true);
            DbHelp.GridSet(gc_Sheet, gv_Sheet, "Qty", "수량", "80", false, false, true);
            DbHelp.GridSet(gc_Sheet, gv_Sheet, "Custom_Name", "작업처", "80", false, false, true);
            DbHelp.GridSet(gc_Sheet, gv_Sheet, "Result_No", "작업실적키", "", false, false, false);
            DbHelp.GridSet(gc_Sheet, gv_Sheet, "Result_Sort", "작업실적", "", false, false, false);
            DbHelp.GridSet(gc_Sheet, gv_Sheet, "Work_Sts", "작업상태", "", false, false, false);
            DbHelp.GridSet(gc_Sheet, gv_Sheet, "MatOut_Ck", "자재출고", "80", false, false, true);

            DbHelp.GridColumn_NumSet(gv_Sheet, "Qty", ForMat.SetDecimal("regWork", "Qty1"));

            gc_Sheet.AddRowYN = false;
            gc_Sheet.PopMenuChk = false;  
            gc_Sheet.MouseWheelChk = false;

            //사용자
            DbHelp.GridSet(gc_User, gv_User, "Check", "선택", "80", false, true, true);
            DbHelp.GridSet(gc_User, gv_User, "User_Name", "작업자", "150", false, false, true);
            DbHelp.GridSet(gc_User, gv_User, "User_Code", "작업자", "150", false, false, false);

            DbHelp.GridColumn_CheckBox(gv_User, "Check");

            gc_User.AddRowYN = false;
            gc_User.MouseWheelChk = false;
            gc_User.PopMenuChk = false;
        }

        private void LookUp_Set()
        {
            try
            {
                SqlParam sp = new SqlParam("sp_KIOSK_WorkResult");
                sp.AddParam("Kind", "S");
                sp.AddParam("Search_D", "C");

                ret = DbHelp.Proc_Search(sp);

                if (ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                lookUp_Custom.Properties.ValueMember = "CODE";
                lookUp_Custom.Properties.DisplayMember = "NAME";
                lookUp_Custom.Properties.Columns.Add(new LookUpColumnInfo("NAME", ""));
                lookUp_Custom.Properties.DataSource = ret.ReturnDataSet.Tables[0];

                lookUp_Custom.EditValue = ret.ReturnDataSet.Tables[0].Rows[0]["CODE"].NullString();
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        #region 내부 함수
        private void Timer_Search(object sender, EventArgs e)
        {
            int iFocus_Row = gv_Sheet.FocusedRowHandle;
            gv_Sheet.FocusedRowChanged -= new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gv_Sheet_FocusedRowChanged);
            Search_Data();
            gv_Sheet.FocusedRowHandle = iFocus_Row;
            gv_Sheet.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gv_Sheet_FocusedRowChanged);
            gv_Sheet.UnselectRow(0);
            gv_Sheet.SelectRow(iFocus_Row);
            //gv_Sheet_FocusedRowChanged(gv_Sheet, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, iFocus_Row));

        }

        private void Search_Data()
        {
            //timer_Search.Stop();
            try
            {
                SqlParam sp = new SqlParam("sp_KIOSK_WorkResult");
                sp.AddParam("Kind", "S");
                sp.AddParam("Search_D", "H");
                sp.AddParam("Custom_Code", lookUp_Custom.EditValue.ToString());

                ret = DbHelp.Proc_Search(sp);

                if(ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                gc_Sheet.DataSource = ret.ReturnDataSet.Tables[0];

                if (gv_Sheet.RowCount == 0)
                    gc_User.DataSource = null;

                gv_Sheet.BestFitColumns();
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }

            //timer_Search.Start();
        }

        private void Check_Button_State(DataRow Row)
        {
            if (Row == null)
                return;

            string Work_Status = Row["Work_Sts"].NullString();

            btn_Add.Visible = false;
            btn_Del.Visible = false;

            if (Work_Status == "S")
            {
                btn_Start.Text = "일시정지";
                btn_Start.ImageOptions.SvgImage = Properties.Resources.pause1;
                btn_End.Enabled = true;
            }
            else
            {
                btn_Start.Text = "작업시작";
                btn_Start.ImageOptions.SvgImage = Properties.Resources.gettingstarted;
                btn_End.Enabled = false;

                if (string.IsNullOrWhiteSpace(Work_Status))
                {
                    btn_Add.Visible = true;
                    btn_Del.Visible = true;
                }
            }
        }

        #endregion

        #region 버튼 이벤트
        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (gv_Sheet.RowCount == 0)
                return;

            int iRow = gv_User.RowCount;

            timer_Search.Stop();
            HelpForm Help_Form = new HelpForm("User", "sp_Help_KioUser", true, new string[] { lookUp_Custom.EditValue.ToString() });
            if(Help_Form.ShowDialog() == DialogResult.OK)
            {
                foreach(DataRow row in Help_Form.Table_Return.Rows)
                {
                    gv_User.AddNewRow();
                    gv_User.UpdateCurrentRow();
                    if ((gc_User.DataSource as DataTable).Select("User_Code = '" + row["User_Code"].ToString() + "'").Length == 0)
                    {
                        gv_User.SetRowCellValue(iRow, "User_Code", row["User_Code"].ToString());
                        gv_User.SetRowCellValue(iRow, "User_Name", row["User_Name"].ToString());
                        iRow++;
                    }
                }
            }
            timer_Search.Start();
        }

        private void btn_Del_Click(object sender, EventArgs e)
        {
            if (gv_User.RowCount < 1)
                return;

            int iRow = gv_User.FocusedRowHandle;

            timer_Search.Stop();
            if (XtraMessageBox.Show("작업자를 삭제하시겠습니까?", "삭제", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                gv_User.DeleteRow(iRow);
                gv_User.UpdateCurrentRow();
            }
            timer_Search.Start();
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            if (gv_Sheet.RowCount < 1)
                return;

            if(gv_User.RowCount < 1)
            {
                XtraMessageBox.Show("작업자를 등록해 주세요");
                return;
            }

            timer_Search.Stop();

            string sMsg = "";

            DataRow dr = gv_Sheet.GetDataRow(gv_Sheet.FocusedRowHandle);

            if (btn_Start.Text == "일시정지")
            {
                sMsg = "작업을 일시정지 하시겠습니까?";
            }
            else
            {
                if (string.IsNullOrWhiteSpace(dr["Work_Sts"].ToString()))
                    sMsg = "작업을 시작하시겠습니까?";
                else
                    sMsg = "작업을 재시작하시겠습니까?";
            }

            if (XtraMessageBox.Show(sMsg, "작업 정보", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                timer_Search.Start();
                return;
            }

            //작업자
            string sUser_Code = "";
            if (string.IsNullOrWhiteSpace(dr["Work_Sts"].ToString()))
            {
                for (int i = 0; i < gv_User.RowCount; i++)
                {
                    sUser_Code += gv_User.GetRowCellValue(i, "User_Code").ToString() + "_/";
                }
            }

            try
            {
                SqlParam sp = new SqlParam("sp_KIOSK_WorkResult");
                sp.AddParam("Kind", "I");
                sp.AddParam("Insert_D", "S"); //작업시작 및 중지
                sp.AddParam("WIdx_No", dr["WIdx_No"].ToString());
                sp.AddParam("Work_Sort", dr["Work_Sort"].ToString());
                sp.AddParam("Result_No", dr["Result_No"].ToString());
                sp.AddParam("Result_Sort", dr["Result_Sort"].ToString());
                sp.AddParam("UserCode", sUser_Code);
                sp.AddParam("Form_Name", "regWorkResult");

                ret = DbHelp.Proc_Save(sp);

                if(ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                dr["Result_No"] = ret.ReturnDataSet.Tables[0].Rows[0]["Result_No"].ToString();
                dr["Result_Sort"] = ret.ReturnDataSet.Tables[0].Rows[0]["Result_Sort"].ToString();
                dr["Work_Sts"] = ret.ReturnDataSet.Tables[0].Rows[0]["Work_Sts"].ToString();

                gv_Sheet_FocusedRowChanged(gv_Sheet, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, gv_Sheet.FocusedRowHandle));
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }

            timer_Search.Start();
        }

        private void btn_End_Click(object sender, EventArgs e)
        {
            if (gv_Sheet.RowCount < 1)
                return;

            timer_Search.Stop();

            if (XtraMessageBox.Show("작업을 종료하시겠습니까?", "작업 정보", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                timer_Search.Start();
                return;
            }

            int iRow = gv_Sheet.FocusedRowHandle;
            DataRow dr_Result = gv_Sheet.GetDataRow(iRow);

            decimal dQty = 0, dBad_Qty = 0;

            PopWorkResult result_Form = new PopWorkResult();
            result_Form.dr = dr_Result;
            result_Form.StartPosition = FormStartPosition.CenterParent;
            if (result_Form.ShowDialog() == DialogResult.No)
                return;

            dQty = result_Form.dQty;
            dBad_Qty = result_Form.dBad_Qty;
            string sOver = result_Form.sOver;

            try
            {
                SqlParam sp = new SqlParam("sp_KIOSK_WorkResult");
                sp.AddParam("Kind", "I");
                sp.AddParam("Insert_D", "E");
                sp.AddParam("Result_No", dr_Result["Result_No"].ToString());
                sp.AddParam("Result_Sort", dr_Result["Result_Sort"].ToString());
                sp.AddParam("WIdx_No", dr_Result["WIdx_No"].ToString());
                sp.AddParam("Work_Sort", dr_Result["Work_Sort"].ToString());
                sp.AddParam("Qty", dQty.ToString());
                sp.AddParam("Bad_Qty", dBad_Qty.ToString());
                //sp.AddParam("Over_Ck", sOver);

                ret = DbHelp.Proc_Save(sp);

                if(ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                Search_Data();
                if (iRow == gv_Sheet.RowCount)
                    iRow--;

                gv_Sheet_FocusedRowChanged(gv_Sheet, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, iRow));
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }

            timer_Search.Start();
        }

        #endregion


        #region 콤보박스

        private void lookUp_Custom_EditValueChanged(object sender, EventArgs e)
        {
            timer_Search.Stop();
            Search_Data();
            timer_Search.Start();
        }

        #endregion

        #region 그리드 이벤트
        private void gv_Sheet_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0)
                return;

            DataRow dr = gv_Sheet.GetDataRow(e.FocusedRowHandle);

            Check_Button_State(dr);

            try
            {
                SqlParam sp = new SqlParam("sp_KIOSK_WorkResult");
                sp.AddParam("Kind", "S");
                sp.AddParam("Search_D", "U");
                sp.AddParam("WIdx_No", dr["WIdx_No"].ToString());
                sp.AddParam("Result_No", dr["Result_No"].ToString());

                ret = DbHelp.Proc_Search(sp);

                if(ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                gc_User.DataSource = ret.ReturnDataSet.Tables[0];
                gv_User.BestFitColumns();
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        private void gv_Sheet_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            DataRow Row = gv_Sheet.GetDataRow(e.RowHandle);

            if (Row["Work_Sts"].NullString().Contains("S")) // 작업중
                e.Appearance.BackColor = Color.FromArgb(117, 255, 0);
            else if (Row["Work_Sts"].NullString().Contains("L")) //비가동
                e.Appearance.BackColor = Color.FromArgb(255, 105, 97);
            else
                e.Appearance.BackColor = Color.Transparent;
        }
        #endregion

        private void gc_Sheet_Load(object sender, EventArgs e)
        {
            foreach (Control control in gc_Sheet.Controls)
            {
                //control.Font = new Font("맑은 고딕", 15F);                
                if (control.GetType() == typeof(HCrkScrollBar))
                {
                    control.MinimumSize = new Size(0, 250);
                    control.Height = 250;
                }
                else if (control.GetType() == typeof(VCrkScrollBar))
                {
                    Point point = new Point(control.Location.X - 100, control.Location.Y);
                    //control.Left = 30;
                    control.Location = point;
                    control.MinimumSize = new Size(50, 0);
                    control.Width = 50;
                }
            }
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            timer_Search.Stop();

            int iRow = gv_Sheet.FocusedRowHandle;

            if(iRow < 0)
            {
                XtraMessageBox.Show("품목을 선택하세요");
                timer_Search.Start();
                return;
            }

            string sItem_Code = gv_Sheet.GetRowCellValue(iRow, "Item_Code").ToString();

            if (string.IsNullOrWhiteSpace(sItem_Code))
            {
                timer_Search.Start();
                return;
            }
        }
    }
}
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
using System.Diagnostics;

namespace EMAX_Monitoring
{
    public partial class Monitoring : DevExpress.XtraEditors.XtraForm
    {
        string[] Config;
        string[] ViewCheck;
        int Notice_Count = 0;

        ReturnStruct ret = new ReturnStruct();
        int Index_Start = 1;
        int Show_Rows = 5;
        bool Turn_Panel;
        PanelControl[] Panels;
        int Panel_Index;
        int iView_Check = 0;

        public Monitoring()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void Monitoring_Load(object sender, EventArgs e)
        {
            Config = Configurations.GetConfig("Monitoring").Split(';');
            //Panels = this.Controls.OfType<PanelControl>().OrderBy(x => x.Location.Y).ToArray();
            Panels = new PanelControl[] { Panel_WR, Panel_Sales, Panel_Process };

            string sViewChk = "";

            for(int i = 1; i < 6; i++)
            {
                if (Config[i] == "Y")
                    sViewChk += i.ToString() + ",";
            }

            if (string.IsNullOrWhiteSpace(sViewChk))
            {
                XtraMessageBox.Show("설정한 모니터링이 없습니다.");
                Label_Title.Text = "설정한 모니터링이 없습니다.";
            }
            else
            {
                sViewChk = sViewChk.Substring(0, sViewChk.Length - 1);

                ViewCheck = sViewChk.Split(',');

                Grid_Set();
                Turn_Panel = true;
                Panel_Index = 0; //처음은 0으로 설정
                Rotate_Panel(null, null);
            }

            Notice_Show(null, null);
        }
        
        private void Grid_Set()
        {
            gc_Work.Grid_TP = GridControlEx.Grid_Type.Report;

            DbHelp.GridSet(gc_Work, gv_Work, "Work_Date", "지시일자", 170);
            DbHelp.GridSet(gc_Work, gv_Work, "Key_No", "작지번호", 150);
            DbHelp.GridSet(gc_Work, gv_Work, "Item_Code", "품목코드", 150);
            DbHelp.GridSet(gc_Work, gv_Work, "Item_Name", "품명", 200);
            DbHelp.GridSet(gc_Work, gv_Work, "SSize", "규격", 200);
            DbHelp.GridSet(gc_Work, gv_Work, "Work_Qty", "지시수량", 100, ForMat.SetDecimal("regWork", "Qty1"));
            DbHelp.GridSet(gc_Work, gv_Work, "Result_Qty", "작업수량", 100, ForMat.SetDecimal("regWork", "Qty1"));
            DbHelp.GridSet(gc_Work, gv_Work, "Custom_Name", "작업처", 120);
            DbHelp.GridSet(gc_Work, gv_Work, "Process", "작업상태", 120);

            gv_Work.OptionsCustomization.AllowFilter = false;
            gv_Work.OptionsCustomization.AllowSort = false;
            gv_Work.OptionsView.ShowAutoFilterRow = false;
            gv_Work.OptionsView.RowAutoHeight = false;
            gv_Work.RowHeight = 125;

            gc_Work.AddRowYN = false;
            gc_Work.PopMenuChk = false;
            gc_Work.MouseWheelChk = false;

            //주문대비 납품현황
            gc_Sales.Grid_TP = GridControlEx.Grid_Type.Report;

            DbHelp.GridSet(gc_Sales, gv_Sales, "Delivery", "Delivery", 170);
            DbHelp.GridSet(gc_Sales, gv_Sales, "PI-No", "Pi_No", 120);
            DbHelp.GridSet(gc_Sales, gv_Sales, "Order_Date", "Order-Date", 170);
            DbHelp.GridSet(gc_Sales, gv_Sales, "Company_Name", "사업장", 120);
            DbHelp.GridSet(gc_Sales, gv_Sales, "Custom_Name", "Buyer", 120);
            DbHelp.GridSet(gc_Sales, gv_Sales, "Item_Code", "품목코드", 150);
            DbHelp.GridSet(gc_Sales, gv_Sales, "Item_Name", "품명", 200);
            DbHelp.GridSet(gc_Sales, gv_Sales, "SSize", "규격", 200);
            DbHelp.GridSet(gc_Sales, gv_Sales, "User_Name", "담당자", 150);
            DbHelp.GridSet(gc_Sales, gv_Sales, "Book_Date", "Booking-Date", 170);
            DbHelp.GridSet(gc_Sales, gv_Sales, "Order_Qty", "Order 수량", 100, ForMat.SetDecimal("regWork", "Qty1"));
            DbHelp.GridSet(gc_Sales, gv_Sales, "Sales_Qty", "선적 수량", 100, ForMat.SetDecimal("regWork", "Qty1"));
            DbHelp.GridSet(gc_Sales, gv_Sales, "Not_Qty", "미출고 수량", 100, ForMat.SetDecimal("regWork", "Qty1"));
            DbHelp.GridSet(gc_Sales, gv_Sales, "S_Price", "단가", 100, ForMat.SetDecimal("regWork", "Price1"));
            DbHelp.GridSet(gc_Sales, gv_Sales, "Amt", "미출고 금액", 150, ForMat.SetDecimal("regWork", "Amt1"));
            DbHelp.GridSet(gc_Sales, gv_Sales, "State", "상태", 120);

            gv_Sales.OptionsCustomization.AllowFilter = false;
            gv_Sales.OptionsCustomization.AllowSort = false;
            gv_Sales.OptionsView.ShowAutoFilterRow = false;
            gv_Sales.OptionsView.RowAutoHeight = false;
            gv_Sales.RowHeight = 125;

            gc_Sales.AddRowYN = false;
            gc_Sales.PopMenuChk = false;
            gc_Sales.MouseWheelChk = false;

            //공정별 진척현황
            gc_Process.Grid_TP = GridControlEx.Grid_Type.Report;

            DbHelp.GridSet(gc_Process, gv_Process, "Process_Name", "공정명", 150);
            DbHelp.GridSet(gc_Process, gv_Process, "Custom_Name", "작업처", 150);
            DbHelp.GridSet(gc_Process, gv_Process, "TResult_Per", "작업률(%)", 150);
            DbHelp.GridSet(gc_Process, gv_Process, "Work_Date", "지시일자", 170);
            DbHelp.GridSet(gc_Process, gv_Process, "Key_No", "작지번호", 150);
            DbHelp.GridSet(gc_Process, gv_Process, "Item_Code", "품목코드", 150);
            DbHelp.GridSet(gc_Process, gv_Process, "Item_Name", "품목명", 200);
            DbHelp.GridSet(gc_Process, gv_Process, "SSize", "규격", 200);
            DbHelp.GridSet(gc_Process, gv_Process, "Work_Qty", "지시 수량", 100, ForMat.SetDecimal("regWork", "Qty1"));
            DbHelp.GridSet(gc_Process, gv_Process, "Result_Qty", "작업수량", 100, ForMat.SetDecimal("regWork", "Qty1"));
            DbHelp.GridSet(gc_Process, gv_Process, "Result_Per", "작업률(%)", 120);

            gv_Process.OptionsCustomization.AllowFilter = false;
            gv_Process.OptionsCustomization.AllowSort = false;
            gv_Process.OptionsView.ShowAutoFilterRow = false;
            gv_Process.OptionsView.RowAutoHeight = false;
            gv_Process.RowHeight = 125;

            gc_Process.AddRowYN = false;
            gc_Process.PopMenuChk = false;
            gc_Process.MouseWheelChk = false;

            Show_Rows = 5;

            int Time_Gap = Convert.ToInt32(Config[0]);

            Timer T = new Timer();
            T.Tick += Rotate_Panel;
            T.Interval = 1000 * Time_Gap;
            T.Start();

            Timer Notice = new Timer();
            Notice.Tick += Notice_Show;
            Notice.Interval = 1000 * 30;
            Notice.Start();
        }

        private void Rotate_Panel(object sender, EventArgs e)
        {
            if (ViewCheck.Length > 1 || Panel_Index == 0)
            {
                if (Turn_Panel)
                {
                    if (Panel_Index > 0)//처음에는 무시하기
                    {
                        Panels[Panel_Index - 1].Dock = DockStyle.None;  // 현재 현황 판넬 내리기
                        Panels[Panel_Index - 1].Visible = false;

                        if (iView_Check == ViewCheck.Length - 1)
                            iView_Check = 0;
                        else
                            iView_Check++;
                    }

                    Panel_Index = int.Parse(ViewCheck[iView_Check]);

                    Panels[Panel_Index - 1].Visible = true;
                    Panels[Panel_Index - 1].Dock = DockStyle.Fill;

                    Set_Title();
                    Search_Data();
                }
                else
                    Search_Data();
            }
            else
                Search_Data();
        }

        private void Set_Title()
        {
            SqlParam sp = new SqlParam("sp_Monitoring_Set");
            sp.AddParam("Kind", "S");

            ReturnStruct temp_ret = DbHelp.Proc_Search(sp);

            if (temp_ret.ReturnChk != 0)
            {
                XtraMessageBox.Show(temp_ret.ReturnMessage);
                return;
            }

            DataTable Titles = temp_ret.ReturnDataSet.Tables[0];

            Label_Title.Text = "    " + Titles.Rows[Panel_Index - 1]["Monitoring"].NullString();
        }

        private void Search_Data()
        {
            SqlParam sp = new SqlParam("sp_Monitoring");
            sp.AddParam("Kind", "S");
            sp.AddParam("Search_D", Panel_Index);
            sp.AddParam("Index_S", Index_Start.NumString());
            sp.AddParam("Row_Count", Show_Rows.NumString());
            
            ret = DbHelp.Proc_Search(sp);

            if (ret.ReturnChk != 0)
            {
                XtraMessageBox.Show(ret.ReturnMessage);
                return;
            }

            if (Panel_Index == 1)
            {
                gc_Work.DataSource = ret.ReturnDataSet.Tables[0];
                gc_Work.RefreshDataSource();
            }
            else if(Panel_Index == 2)
            {
                gc_Sales.DataSource = ret.ReturnDataSet.Tables[0];
                gc_Sales.RefreshDataSource();
            }
            else if(Panel_Index == 3)
            {
                gc_Process.DataSource = ret.ReturnDataSet.Tables[0];
                gc_Process.RefreshDataSource();
            }

            int Row_Count = Convert.ToInt32(ret.ReturnDataSet.Tables[1].Rows[0][0].NumString());

            if (Row_Count < (Index_Start + Show_Rows))
            {
                Index_Start = 1;
                Turn_Panel = true;
            }
            else
            {
                Index_Start += Show_Rows;
                Turn_Panel = false;
            }
        }

        private void Notice_Show(object sender, EventArgs e)
        {
            string part_name = string.Empty;
            ReturnStruct temp_ret = new ReturnStruct();

            SqlParam sp = new SqlParam("sp_MainForm_Notice");
            sp.AddParam("User_Code", "Monitoring");
            temp_ret = DbHelp.Proc_Search(sp);

            if (temp_ret.ReturnChk != 0)
            {
                return;
            }
            DataTable notice = DbHelp.Fill_Table(temp_ret.ReturnDataSet.Tables[0]);

            if (Notice_Count == notice.Rows.Count)
                Notice_Count = 0;

            if (notice.Rows.Count > 0)
            {
                if (notice.Rows[Notice_Count]["Notice_Part"].NullString() == "010")
                {
                    part_name = "(긴급) ";
                    Label_Notice.ForeColor = Color.Red;
                }
                else
                {
                    part_name = "";
                    Label_Notice.ForeColor = Color.White;
                }

                Label_Notice.Text = part_name + notice.Rows[Notice_Count]["Notice_Title"].NullString() + "      (" + notice.Rows[Notice_Count]["Notice_STime"].NullString().Substring(0, 10) + " ~ " + notice.Rows[Notice_Count]["Notice_ETime"].NullString().Substring(0, 10) + ")";
                Label_Notice.ToolTip = notice.Rows[Notice_Count]["Notice_No"].NullString();

                Notice_Count++;
            }
            else
            {
                Label_Notice.Text = "";
                Label_Notice.ToolTip = "";
            }
        }

        private void Monitoring_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void View_Work_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "Process")
            {
                if (e.Value.NullString() == "작업중")
                {
                    e.DisplayText = "→";
                }
                else if (e.Value.NullString() == "작업중지")
                {
                    e.DisplayText = "■";
                }
                else if (e.Value.NullString() == "작업완료")
                {
                    e.DisplayText = "●";
                }
            }
        }

        private void View_Work_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            string FieldName = e.Column.FieldName;

            if (FieldName == "Process")
            {
                string Value = gc_Work.Get_Cell_Data("Process", e.RowHandle);

                if (Value == "작업중")
                {
                    e.Appearance.ForeColor = Color.FromArgb(117, 255, 0);
                    e.Appearance.Font = new Font("나눔바른고딕", 30F, FontStyle.Bold);
                }
                else if (Value == "작업중지")
                {
                    e.Appearance.ForeColor = Color.Red;
                    e.Appearance.Font = new Font("나눔바른고딕", 30F, FontStyle.Bold);
                }
                else if (Value == "작업완료")
                {
                    e.Appearance.ForeColor = Color.FromArgb(44, 85, 152);
                    e.Appearance.Font = new Font("나눔바른고딕", 28F, FontStyle.Bold);
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.F1)
            {
                Set_Config set_Config = new Set_Config();
                if(set_Config.ShowDialog() == DialogResult.OK)
                {
                    XtraMessageBox.Show("설정이 변경되어 프로그램을 재실행합니다.");

                    string sPath = Application.ExecutablePath;

                    Process.Start(sPath);
                    //System.Threading.Thread.Sleep(1000);
                    Application.Exit();
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
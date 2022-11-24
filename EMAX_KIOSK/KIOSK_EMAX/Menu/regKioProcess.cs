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

namespace KIOSK_EMAX
{
    public partial class regKioProcess : DevExpress.XtraEditors.XtraForm
    {
        ReturnStruct ret = new ReturnStruct();

        public regKioProcess()
        {
            InitializeComponent();
        }

        private void regKioProcess_Load(object sender, EventArgs e)
        {
            Grid_Set();

            dt_Result.DateTime = DateTime.Today;
        }

        private void Grid_Set()
        {
            DbHelp.GridSet(gc_Process, gv_Process, "Process_Name", "공정명", "100", false, false, true);
            DbHelp.GridSet(gc_Process, gv_Process, "Custom_Name", "작업처", "80", false, false, true);
            DbHelp.GridSet(gc_Process, gv_Process, "TResult_Per", "작업률(%)", "100", false, false, false);
            DbHelp.GridSet(gc_Process, gv_Process, "Work_Date", "지시일자", "200", false, false, true);
            DbHelp.GridSet(gc_Process, gv_Process, "Key_No", "작지번호", "200", false, false, true);
            DbHelp.GridSet(gc_Process, gv_Process, "Item_Code", "품목코드", "200", false, false, true);
            DbHelp.GridSet(gc_Process, gv_Process, "Item_Name", "품목명", "200", false, false, false);
            DbHelp.GridSet(gc_Process, gv_Process, "SSize", "규격", "150", false, false, true);
            DbHelp.GridSet(gc_Process, gv_Process, "Work_Qty", "지시수량", "80", false, false, true);
            DbHelp.GridSet(gc_Process, gv_Process, "Result_Qty", "작업수량", "80", false, false, true);
            DbHelp.GridSet(gc_Process, gv_Process, "Result_Per", "작업률(%)", "100", false, false, true);

            DbHelp.GridColumn_NumSet(gv_Process, "Work_Qty", ForMat.SetDecimal("regWork", "Qty1"));
            DbHelp.GridColumn_NumSet(gv_Process, "Result_Qty", ForMat.SetDecimal("regWork", "Qty1"));

            gc_Process.AddRowYN = false;
            gc_Process.PopMenuChk = false;
            gc_Process.MouseWheelChk = false;

        }


        #region 내부 함수
        private void Search_Data()
        {
            try
            {
                SqlParam sp = new SqlParam("sp_KIOSK_Process");
                sp.AddParam("Kind", "S");
                sp.AddParam("Result_Date", dt_Result.DateTime.ToString("yyyyMMdd"));

                ret = DbHelp.Proc_Search(sp);

                if(ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                gc_Process.DataSource = ret.ReturnDataSet.Tables[0];
                gv_Process.BestFitColumns();
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        #endregion

        #region 버튼 이벤트

        private void btn_Search_Click(object sender, EventArgs e)
        {
            Search_Data();
        }

        #endregion


    }
}
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
    public partial class rptPLC : BaseReg
    {
        ReturnStruct ret = new ReturnStruct();
        DataSet ds = new DataSet();

        public rptPLC()
        {
            InitializeComponent();
        }

        private void rptPLC_Load(object sender, EventArgs e)
        {
            //버튼 클릭 이벤트 선언
            this.btn_Select.Click += btn_Select_Click;
            this.btn_Excel.Click += btn_Excel_Click;
            this.btn_Close.Click += btn_Close_Click;
            Grid_Set();

            dt_F.DateTime = DateTime.Today.AddDays(-1);
            dt_T.DateTime = DateTime.Today;

            btn_Select_Click(null, null); 
        }

        private void Grid_Set()
        {
            DbHelp.GridSet(gc_Equip, gv_Equip, "Date", "일자", "150", false, false, true, true);
            DbHelp.GridSet(gc_Equip, gv_Equip, "Equip_IP", "IP", "150", false, false, true, true);
            DbHelp.GridSet(gc_Equip, gv_Equip, "Port", "Port", "100", false, false, true, true);
            DbHelp.GridSet(gc_Equip, gv_Equip, "Addr", "주소값", "120", false, false, true, true);
            DbHelp.GridSet(gc_Equip, gv_Equip, "DEVICE", "DEVICE_ID", "150", false, false, true, true);
            DbHelp.GridSet(gc_Equip, gv_Equip, "Device_Name", "구분", "150", false, false, true, true);
            DbHelp.GridSet(gc_Equip, gv_Equip, "PLC", "PLC 데이터", "150", false, false, true, true);

            gv_Equip.OptionsView.ShowAutoFilterRow = false;

            //PLC 데이터
            DbHelp.GridSet(gc_PLC, gv_PLC, "Date", "일자", "150", false, false, true, true);
            DbHelp.GridSet(gc_PLC, gv_PLC, "DEVICE", "DEVICE", "150", false, false, true, true);
            DbHelp.GridSet(gc_PLC, gv_PLC, "PARAM1", "값1", "100", false, false, true, true);
            DbHelp.GridSet(gc_PLC, gv_PLC, "PARAM2", "값2", "120", false, false, true, true);

            gv_PLC.OptionsView.ShowAutoFilterRow = false;
        }

        private void btn_Select_Click(object sender, EventArgs e)
        {
            Search_Data();
        }

        private void btn_Excel_Click(object sender, EventArgs e)
        {
            if(gc_Equip.Focused)
                FileIF.Excel_Down(gc_Equip, this.Name);
            else
                FileIF.Excel_Down(gc_PLC, this.Name);
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            MainForm.MainTab.TabPages.Remove(MainForm.MainTab.SelectedTabPage);
        }

        private void Search_Data()
        {
            SqlParam sp = new SqlParam("sp_PLC");
            sp.AddParam("Kind", "D");
            sp.AddParam("Fdate", dt_F.DateTime.ToString("yyyyMMdd"));
            sp.AddParam("Tdate", dt_T.DateTime.ToString("yyyyMMdd"));
            ret = DbHelp.Proc_Search(sp);

            if (ret.ReturnChk != 0)
            {
                XtraMessageBox.Show(ret.ReturnMessage);
                return;
            }

            gc_Equip.DataSource = ret.ReturnDataSet.Tables[0];
            gv_Equip.BestFitColumns();

            gc_PLC.DataSource = ret.ReturnDataSet.Tables[1];
            gv_PLC.BestFitColumns();
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

    }
}

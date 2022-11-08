using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace PLC_EMAX
{
    public partial class PLC_Data : DevExpress.XtraEditors.XtraForm
    {
        ReturnStruct ret = new ReturnStruct();
        DataSet ds = new DataSet();

        public string sEquip_Code = "";

        public PLC_Data()
        {
            InitializeComponent();
        }

        private void PLC_Data_Load(object sender, EventArgs e)
        {
            Grid_Set();

            dt_T.EditValueChanged -= dt_T_EditValueChanged;
            dt_T.DateTime = DateTime.Today;
            dt_F.DateTime = DateTime.Today.AddMonths(-1);
            dt_T.EditValueChanged += dt_T_EditValueChanged;
        }

        private void Grid_Set()
        {
            DbHelp.GridSet(gc_PLC, gv_PLC, "Equip_IP", "IP", "150", false, false, true, true);
            DbHelp.GridSet(gc_PLC, gv_PLC, "Port", "Port", "100", false, false, true, true);
            DbHelp.GridSet(gc_PLC, gv_PLC, "Addr", "주소값", "120", false, false, true, true);
            DbHelp.GridSet(gc_PLC, gv_PLC, "Device_Name", "구분", "150", false, false, true, true);
            DbHelp.GridSet(gc_PLC, gv_PLC, "PLC", "PLC 데이터", "150", false, false, true, true);

            gv_PLC.OptionsView.ShowAutoFilterRow = false;
        }

        private void Search()
        {
            try
            {
                Stopwatch stopwatch = new Stopwatch();

                SqlParam sp = new SqlParam("sp_PLC");
                sp.AddParam("Kind", "A");
                sp.AddParam("Equip_Code", sEquip_Code);
                sp.AddParam("FDate", dt_F.DateTime.ToString("yyyyMMdd"));
                sp.AddParam("TDate", dt_T.DateTime.ToString("yyyyMMdd"));
                sp.AddParam("TOP", txt_Top.EditValue.NumString());

                stopwatch.Start();

                ret = DbHelp.Proc_Search(sp);

                stopwatch.Stop();

                if (ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                decimal dTime = stopwatch.ElapsedMilliseconds / (decimal)1000;

                txt_Time.EditValue = dTime;  

                gc_PLC.DataSource = ret.ReturnDataSet.Tables[0];
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        private void dt_F_EditValueChanged(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(dt_F.Text))
            {
                dt_F.DateTime = dt_T.DateTime.AddMonths(-1);
            }

            Search();
        }

        private void dt_T_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(dt_T.Text))
            {
                dt_T.DateTime = dt_F.DateTime.AddMonths(1);
            }

            Search();
        }

        private void txt_Top_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                Search();
            }
        }
    }
}
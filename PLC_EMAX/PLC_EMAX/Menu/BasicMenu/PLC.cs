using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Windows.Forms;

namespace PLC_EMAX
{
    public partial class PLC : DevExpress.XtraEditors.XtraForm
    {
        ReturnStruct ret = new ReturnStruct();
        DataSet ds = new DataSet();

        Timer timer_PLC = new Timer();

        public PLC()
        {
            InitializeComponent();
        }

        private void PLC_Load(object sender, EventArgs e)
        {
            Grid_Set();

            timer_PLC.Tick += Timer_Search;
            timer_PLC.Interval = 5000;
            timer_PLC.Start();

            Search();
        }

        private void Grid_Set()
        {
            DbHelp.GridSet(gc_Equip, gv_Equip, "Equip_Code", "설비코드", "120", false, true, true, true);
            DbHelp.GridSet(gc_Equip, gv_Equip, "Equip_Name", "설비명", "150", false, false, true, true);
            DbHelp.GridSet(gc_Equip, gv_Equip, "PLC_Ck", "PLC 통신 여부", "100", false, false, true, true);

            gv_Equip.OptionsView.ShowAutoFilterRow = false;

            DbHelp.GridSet(gc_PLC, gv_PLC, "Equip_IP", "IP", "150", false, false, true, true);
            DbHelp.GridSet(gc_PLC, gv_PLC, "Port", "Port", "100", false, false, true, true);
            DbHelp.GridSet(gc_PLC, gv_PLC, "Addr", "주소값", "120", false, true, true, true);
            DbHelp.GridSet(gc_PLC, gv_PLC, "Device_Name", "구분", "150", false, false, true, true);
            DbHelp.GridSet(gc_PLC, gv_PLC, "PLC", "PLC 데이터", "150", false, false, true, true);

            gv_PLC.OptionsView.ShowAutoFilterRow = false;
        }
        private void Timer_Search(object sender, EventArgs e)
        {
            if(gv_Equip.RowCount > 0)
                gv_Equip_FocusedRowChanged(gv_Equip, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, gv_Equip.FocusedRowHandle));
        }

        private void Search()
        {
            try
            {
                timer_PLC.Stop();
                SqlParam sp = new SqlParam("sp_PLC");
                sp.AddParam("Kind", "S");

                ret = DbHelp.Proc_Search(sp);

                if(ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                gc_Equip.DataSource = ret.ReturnDataSet.Tables[0];

                timer_PLC.Start();
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        private void Search_D(string sEquip_Code)
        {
            try
            {
                SqlParam sp = new SqlParam("sp_PLC");
                sp.AddParam("Kind", "P");
                sp.AddParam("Equip_Code", sEquip_Code);

                ret = DbHelp.Proc_Search(sp);

                if (ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                gc_PLC.DataSource = ret.ReturnDataSet.Tables[0];
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        private void gv_Equip_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0)
                return;

            if (gv_Equip.GetRowCellValue(e.FocusedRowHandle, "PLC_Ck").ToString() == "미통신")
                btn_Data.Visible = false;
            else
                btn_Data.Visible = true;

            Search_D(gv_Equip.GetRowCellValue(e.FocusedRowHandle, "Equip_Code").ToString());
        }

        private void btn_Read_Click(object sender, EventArgs e)
        {
            try
            {
                SqlParam sp = new SqlParam("sp_PLC");
                sp.AddParam("Kind", "R");
                sp.AddParam("IP", txt_IP.Text);
                sp.AddParam("Port", txt_Port.Text);
                sp.AddParam("Addr", txt_Addr.Text);

                ret = DbHelp.Proc_Search(sp);

                if (ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                if(ret.ReturnDataSet.Tables[0].Rows.Count > 0)
                {
                    txt_PLC.Text = ret.ReturnDataSet.Tables[0].Rows[0]["PLC"].NullString();
                    XtraMessageBox.Show("PLC 데이터 읽기 성공");
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        private void btn_Write_Click(object sender, EventArgs e)
        {
            try
            {
                SqlParam sp = new SqlParam("sp_PLC");
                sp.AddParam("Kind", "W");
                sp.AddParam("IP", txt_IP.Text);
                sp.AddParam("Port", txt_Port.Text);
                sp.AddParam("Addr", txt_Addr.Text);
                sp.AddParam("PLC", txt_PLC.Text);

                ret = DbHelp.Proc_Save(sp);

                if (ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                if(ret.ReturnDataSet.Tables[0].Rows[0]["CHK"].NullString() == "O")
                    XtraMessageBox.Show("PLC 데이터 쓰기 성공");
                else
                    XtraMessageBox.Show("PLC 데이터 쓰기 실패");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        private void PLC_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer_PLC.Stop();
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void btn_Data_Click(object sender, EventArgs e)
        {
            if (gv_Equip.RowCount < 0)
                return;

            PLC_Data Data_Pop = new PLC_Data();
            Data_Pop.Text = "설비 : '" + gv_Equip.GetRowCellValue(gv_Equip.FocusedRowHandle, "Equip_Name").NullString() + "' 데이터 누적 조회";
            Data_Pop.sEquip_Code = gv_Equip.GetRowCellValue(gv_Equip.FocusedRowHandle, "Equip_Code").NullString();
            Data_Pop.ShowDialog();
        }
    }
}
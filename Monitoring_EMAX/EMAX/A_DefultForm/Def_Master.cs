using DevExpress.XtraEditors.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace SERP
{
    public partial class Def_Master : CommonUserControl
    {
        //로그
        Log.Log_text log = new Log.Log_text();
        //데이터 테이블
        DataTable Dt_R = new DataTable();
        //메세지
        public delegate void SendMessageHandler(string message);
        public event SendMessageHandler SendMessage;


        public Def_Master()
        {
            InitializeComponent();
        }

        private void Def_Master_Load(object sender, EventArgs e)
        {
            GridSet();
            dtStart.DateTime = DateTime.Now.AddDays(-14);
            dtEnd.DateTime = DateTime.Now;
            
        }

        #region 메소드
        private void GridSet()
        {
            ViewMaster.OptionsBehavior.ReadOnly = true;
            ViewMaster.OptionsBehavior.Editable = false;
            ViewMaster.OptionsView.ShowGroupPanel = false;

            ViewMasterSub.OptionsBehavior.ReadOnly = true;
            ViewMasterSub.OptionsBehavior.Editable = false;
            ViewMasterSub.OptionsView.ShowGroupPanel = false;

            ViewDetail.OptionsBehavior.ReadOnly = true;
            ViewDetail.OptionsBehavior.Editable = false;
            ViewDetail.OptionsView.ShowGroupPanel = false;
        }

        #endregion

        #region 입력사원 / 업체 선택
        //private void txtStaffName_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        //{
        //    try
        //    {
        //        if (e.Button.Kind == ButtonPredefines.Search)
        //        {
        //            frmSearchStaffs frm = new frmSearchStaffs();
        //            frm.SetSearchString = txtStaffName.Text;
        //            frm.SendRst += new frmSearchStaffs.SendResult(frmStaff_SendResult);
        //            frm.ShowDialog();
        //        }
        //        else
        //        {
        //            txtStaffName.Text = "";
        //            txtStaffName.ToolTip = "";
        //            //lookDept.EditValue = null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        log.Log(ex.ToString());
        //    }


        //}
        //protected override void frmStaff_SendResult(StaffModel model)
        //{
        //    try
        //    {
        //        //lookDept.EditValue = model.DEPT_CODE;
        //        txtStaffName.ToolTip = model.STAFF_CODE;
        //        txtStaffName.Text = model.STAFF_NAME;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        log.Log(ex.ToString());
        //    }

        //}

        //private void txtCustName_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        //{
        //    try
        //    {
        //        if (e.Button.Kind == ButtonPredefines.Search)
        //        {
        //            frmSearchCustomers frm = new frmSearchCustomers();
        //            frm.HideCheckboxField = false;
        //            frm.SetSearchString = txtCustName.Text;
        //            frm.SetCustKind = CommonBase.CUST_KIND.PURCHASE;
        //            //frm.SendRst += new frmSearchCustomers.SendResult(frmCust_SendRst);
        //            frm.SendRsts += new frmSearchCustomers.SendResults(frmCust_SendRst); //modified on 2020.06
        //            frm.StartPosition = FormStartPosition.CenterScreen; frm.ShowDialog();
        //        }
        //        else
        //        {
        //            txtCustName.Text = ""; txtCustName.ToolTip = "";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        log.Log(ex.ToString());
        //    }

        //}
        //void frmCust_SendRst(List<CustomerModel> models)
        //{
        //    try
        //    {
        //        string names = null;
        //        string codes = null;
        //        foreach (CustomerModel model in models)
        //        {
        //            names += "'" + model.CUST_NAME1 + "',";
        //            codes += "'" + model.CUST_CODE + "',";
        //        }
        //        txtCustName.ToolTip = codes;
        //        txtCustName.Text = names;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        log.Log(ex.ToString());
        //    }

        //}

        private void txtCustName_EditValueChanged(object sender, EventArgs e)
        {
        }
        #endregion

        #region 버튼이벤트
        private void btnSearch_Click(object sender, EventArgs e)
        {
            Qr_SelectMaster();
            Qr_SelectMasterSub();
            Qr_SelectDetail();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //frmEnteringDetailJH frm = new frmEnteringDetailJH("");
            //frm.SendMessage += RcvMessage;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            //if (ViewMasterSub.RowCount == 0)
            //    return;

            //frmEnteringDetailJH frm = new frmEnteringDetailJH(Convert.ToString(ViewMasterSub.GetFocusedRowCellValue("PE_NUM")));
            //frm.SendMessage += RcvMessage;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.ShowDialog();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (ViewMaster.RowCount > 0)
            {
                DataRow dr = ViewMasterSub.GetFocusedDataRow();
                if (dr["PO_IN_OK"].ToString() == "Y")
                {
                    MessageBox.Show("입고 삭제가 불가능 합니다");
                    return;
                }


                string str1 = dr["PE_NUM"].ToString();
                string str2 = dr["PO_NUM"].ToString();
                Qr_DeleteMaster(str1, str2);
                MessageBox.Show("삭제 되었습니다");
                btnSearch_Click(null, null);
            }
        }
        #endregion

        #region 쿼리
        private void Qr_SelectMaster()
        {
            try
            {
                Dt_R = new DataTable();
                DbHelp.ConnectToDB();
                StringBuilder Sb = new StringBuilder();
                Sb.Append(" SELECT PROJECT_NO, PROJECT_NAME ");
                Sb.Append(" FROM PurchaseEntering_Info ");
                Sb.Append("  WHERE INSERT_DATE BETWEEN " + DbHelp.Pr(dtStart.DateTime.ToString("yyyyMMdd")) + " AND " + DbHelp.Pr(dtEnd.DateTime.AddDays(1).ToString("yyyyMMdd")));
                Sb.Append(" GROUP BY PROJECT_NO, PROJECT_NAME ");

                var sqlAdapter = new SqlDataAdapter(Sb.ToString(), DbHelp.DBConn);
                sqlAdapter.Fill(Dt_R);
                GridMaster.DataSource = Dt_R;
                GridMaster.RefreshDataSource();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                log.Log(ex.ToString());
            }
        }

        private void Qr_SelectMasterSub()
        {
            try
            {
                Dt_R = new DataTable();
                DbHelp.ConnectToDB();
                StringBuilder Sb = new StringBuilder();
                Sb.Append(" SELECT ");
                Sb.Append(" PE_NUM, CONVERT(DATETIME, INSERT_DATE, 110) AS INSERT_DATE,  ");
                Sb.Append(" STAFF_CODE, STAFF_NAME, DEPT_CODE, DEPT_NAME, ");
                Sb.Append(" CUST_CODE, CUST_NAME, REQ_TYPE,  ");
                Sb.Append(" CASE WHEN REQ_TYPE ='1'  THEN '자재' WHEN REQ_TYPE ='2'  THEN '일반'  WHEN REQ_TYPE ='3'  THEN '외주'  WHEN REQ_TYPE ='4'  THEN '운반' END AS REQ_TYPE_NAME,   ");
                Sb.Append(" SUPPLY_AMT, VAT_AMT, TOT_AMT, PAY_AMT, CASE WHEN IN_CONFIRM ='Y' THEN '입고'ELSE '미입고' END AS IN_CONFIRM,  ");
                Sb.Append(" PROJECT_NO, PROJECT_NAME, PROJECT_ITM_CODE, PROJECT_ITM_NAME,  ");
                Sb.Append(" PO_NUM, REMARK, PAY_DATE ");
                Sb.Append(" FROM PurchaseEntering_Info ");
                Sb.Append(" WHERE PROJECT_NO = " + DbHelp.Pr(Convert.ToString(ViewMaster.GetFocusedRowCellValue("PROJECT_NO"))));

                var sqlAdapter = new SqlDataAdapter(Sb.ToString(), DbHelp.DBConn);
                sqlAdapter.Fill(Dt_R);
                GridMasterSub.DataSource = Dt_R;
                GridMasterSub.RefreshDataSource();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                log.Log(ex.ToString());
            }
        }

        private void Qr_SelectDetail()
        {
            try
            {
                Dt_R = new DataTable();
                DbHelp.ConnectToDB();
                StringBuilder Sb = new StringBuilder();
                Sb.Append(" SELECT ");
                Sb.Append(" ITM_CODE, ITM_NAME, ITM_STANDARD, PURPOSE, QUALITY,  ");
                Sb.Append(" QTY, IN_QTY, PRICE, SUPPLY_AMT, VAT_AMT, TOT_AMT,  PAY_AMT,");
                Sb.Append(" UNIT, WEIGHT, REMARK, CUST_CODE, CUST_NAME,  ");
                Sb.Append(" IN_DATE, UPDATE_DATE, INSERT_DATE ");
                Sb.Append(" FROM PurchaseEntering_Items ");
                Sb.Append(" WHERE PE_NUM = " + DbHelp.Pr(Convert.ToString(ViewMasterSub.GetFocusedRowCellValue("PE_NUM"))));
                Sb.Append(" ORDER BY PEI_SERIAL ");

                var sqlAdapter = new SqlDataAdapter(Sb.ToString(), DbHelp.DBConn);
                sqlAdapter.Fill(Dt_R);
                GridDetail.DataSource = Dt_R;
                GridDetail.RefreshDataSource();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                log.Log(ex.ToString());
            }
        }

        private void Qr_DeleteMaster(string PE_Num, string PO_Num)
        {
            try
            {
                DbHelp.ConnectToDB();
                StringBuilder Sb = new StringBuilder();
                Sb.Append(" DELETE FROM PurchaseEntering_Info ");
                Sb.Append(" WHERE PE_NUM = " + DbHelp.Pr(PE_Num));
                Sb.Append("; ");
                Sb.Append(" DELETE FROM PurchaseEntering_Items ");
                Sb.Append(" WHERE PE_NUM = " + DbHelp.Pr(PE_Num));
                Sb.Append("; ");
                Sb.Append(" UPDATE PurchaseOrder_Info ");
                Sb.Append(" SET PO_IN_OK = 'N' ");
                Sb.Append(" WHERE PO_NUM = " + DbHelp.Pr(PO_Num));
                SqlCommand com = new SqlCommand(Sb.ToString(), DbHelp.DBConn);
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                log.Log(ex.ToString());
            }
        }

        #endregion
        public void RcvMessage(string str)
        {
            try
            {
                if (str.Length != 0)
                {
                    string[] StrList = str.Split('|');

                    if (StrList.Length == 10)
                    {
                        if (StrList[0] == "frmMaterialPop")
                        {

                        }
                    }

                    if (str == "frmEnteringDetailJH")
                    {
                        btnSearch_Click(null, null);
                    }
                    else if(str == "frmEnteringDetailJHUpdate")
                    {
                        Qr_SelectMasterSub();
                        Qr_SelectDetail();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                log.Log(ex.ToString());
            }
        }

        private void ViewMasterSub_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void ViewMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Qr_SelectMasterSub();
            Qr_SelectDetail();
        }

        private void ViewMasterSub_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Qr_SelectDetail();
        }
    }
}

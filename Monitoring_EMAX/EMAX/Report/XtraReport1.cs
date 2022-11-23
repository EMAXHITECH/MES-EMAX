using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace EMAX_Monitoring
{
    public partial class XtraReport1 : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReport1(string ls_pi_num, string ls_pii_serial)
        {
            InitializeComponent();

            DbHelp.ConnectToDB();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은 고딕", 10);

            Print_Initialize();
            Get_DataPrint(ls_pi_num, ls_pii_serial);
        }

        private void Print_Initialize()
        {
            Boolean lb_chk = true;

            lblLotNO.Visible = lb_chk;
            lblMakeDate.Visible = lb_chk;
            lblCustomer.Visible = lb_chk;
            lblMaterial.Visible = lb_chk;
            lblStandard.Visible = lb_chk;
            lblWeightQty.Visible = lb_chk;
            lblCheck.Visible = lb_chk;
            lblCertification.Visible = lb_chk;

            txtLotNO.Text = "";
            txtMakeDate.Text = "";
            txtCustomer.Text = "";
            txtMaterial1.Text = "";
            txtMaterial2.Text = "";
            txtStandard1.Text = "";
            txtStandard2.Text = "";
            txtWeight.Text = "";
            txtQty.Text = "";
            txtCheck.Text = "";
            txtCertification.Text = "";
            xrBarCode1.Text = "";    // 바코드 
        }

        //
        private void Get_DataPrint(string ls_pi_num, string ls_pii_serial)
        {
            StringBuilder SB;      //StringBuilder
            SqlConnection sqlConn = new SqlConnection(DbHelp.DBConnString);
            SqlCommand sqlComm;

            string ls_gettype;

            SB = new StringBuilder();
            SB.Append("SELECT a.PR_NUM, a.PRI_SERIAL, a.ITM_CODE, c.ITM_NAME, a.ITM_STANDARD, b.PR_REG_DATE ");
            SB.Append("     , a.PG_UNIT, a.PRI_QTY, a.SOI_BQTY, a.BAD_CODE, a.WORK_CODE, d.CUST_NAME1       ");
            SB.Append("  FROM ProducResult_Items A                                                          ");
            SB.Append("       LEFT OUTER JOIN ProducResult_Info B ON A.PR_NUM = A.PR_NUM                    ");
            SB.Append("       LEFT OUTER JOIN Item_info         C ON A.ITM_CODE = C.ITM_CODE                ");
            SB.Append("       LEFT OUTER JOIN Customer_Info     D ON B.CUST_CODE = D.CUST_CODE              ");
            SB.Append(" WHERE A.PR_NUM = '" + ls_pi_num + "' AND A.PRI_SERIAL = '" + ls_pii_serial + "'     ");

            if (sqlConn.State == 0) sqlConn.Open();
            sqlComm = new SqlCommand(SB.ToString(), sqlConn);
            sqlComm.ExecuteNonQuery();

            SqlDataReader reader = sqlComm.ExecuteReader();

            string[] ls_val = new string[20];

            if (!reader.HasRows) return;

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    ls_gettype = reader.GetDataTypeName(i);

                    if (!reader.IsDBNull(i))
                    {
                        switch (ls_gettype)
                        {
                            case "nvarchar":
                            case "nchar":
                            case "varchar":
                            case "char":
                                ls_val[i] = reader.GetString(i);
                                break;

                            case "bigint":
                                ls_val[i] = reader.GetInt64(i).ToString();
                                break;

                            case "int":
                                ls_val[i] = reader.GetInt32(i).ToString();
                                break;

                            case "decimal":
                                ls_val[i] = reader.GetDecimal(i).ToString();
                                break;

                            case "datetime":
                            case "smalldatetime":
                                ls_val[i] = reader.GetDateTime(i).ToString();
                                break;

                            default:
                                break;
                        }
                    }
                }

                txtLotNO.Text = "";                         // LOT번호 
                txtMakeDate.Text = LeftA(ls_val[5], 4) +"." + MidA(ls_val[5], 5, 2) + "." + RightA(ls_val[5], 2);  // 제조일자
                txtCustomer.Text = ls_val[11];              // 거래처
                txtMaterial1.Text = "C1100";                // 재질1
                txtMaterial2.Text = "BD-1/2H";              // 재질2
                txtStandard1.Text = ls_val[4].Replace(".0","");              // 규격1
                txtStandard2.Text = RightA(ls_val[4], 5);   // 규격2
                txtWeight.Text = ls_val[8];                 // 중량
                txtQty.Text = ls_val[7];                    // 수량
                txtCheck.Text = "";                         // 검사
                txtCertification.Text = "한국표준협회";      // 인증기관
                //xrBarCode1.Text = ls_val[0].Substring(2, 4) + Convert.ToInt16(ls_val[0].Substring(6, 3)).ToString() + Convert.ToInt16(ls_val[1]).ToString(); /*ls_val[0] + ls_val[1]*/ // 바코드 
                xrBarCode1.Text = ls_val[0] + Convert.ToInt16(ls_val[1]).ToString(); /*ls_val[0] + ls_val[1]*/ // 바코드 
            }
        }

        // 문자열 왼쪽 처음부터 지정된 문자열값 리턴(VBScript Left기능)
        private string LeftA(string target, int length)
        {
            if (length <= target.Length)
            {
                return target.Substring(0, length);
            }
            return target;
        }

        // 지정된 위치이후 모든 문자열 리턴 (VBScript Mid기능)
        private string MidA(string target, int start)
        {
            if (start <= target.Length)
            {
                return target.Substring(start - 1);
            }
            return string.Empty;
        }

        // 문자열이 지정된 위치에서 지정된 길이만큼까지의 문자열 리턴 (VBScript Mid기능)
        private string MidA(string target, int start, int length)
        {
            if (start <= target.Length)
            {
                if (start + length - 1 <= target.Length)
                {
                    return target.Substring(start - 1, length);
                }
                return target.Substring(start - 1);
            }
            return string.Empty;
        }

        // 문자열 오른쪽 처음부터 지정된 문자열값 리턴(VBScript Right기능)
        private string RightA(string target, int length)
        {
            if (length <= target.Length)
            {
                return target.Substring(target.Length - length);
            }
            return target;
        }
    }
}

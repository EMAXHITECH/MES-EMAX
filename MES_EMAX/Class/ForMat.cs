using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MES
{
    public static class ForMat
    {
        private static ReturnStruct ret;

        public static string NumString(this object result)
        {
            if (result == null)
                return "0";
            else
            {
                if (result.ToString() == "")
                    return "0";
                else
                {
                    if (decimal.TryParse(result.ToString(), out decimal d))
                        return result.ToString();

                    string[] str_lst = Regex.Split(result.ToString(), @"\D");
                    return string.Concat(str_lst);

                    //Regex pattern = new Regex(@"[0-9]*\.*[0-9]+");
                    // Match match = pattern.Match(result.ToString());

                    // return match.Value;
                }
            }
        }

        public static string NullString(this object result)
        {
            if (result == null || result.ToString() == "")
                return "";
            else
                return result.ToString();
        }

        public static decimal ToDecimal(this object result)
        {
            return Convert.ToDecimal(result.NumString());
        }

        public static int ToInt(this object result)
        {
            return Convert.ToInt32(result.NumString());
        }

        public static int SetDecimal(string sMenu, string sColumn)
        {
            try
            {
                SqlParam sp = new SqlParam("sp_ForMat_Decimal");
                sp.AddParam("MENU_SCODE", sMenu);

                ret = DbHelp.Proc_Search(sp);

                if (ret.ReturnChk == 0)
                {
                    if (ret.ReturnDataSet.Tables[0].Rows.Count > 0)
                    {
                        return int.Parse(ret.ReturnDataSet.Tables[0].Rows[0][sColumn].ToString());
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static void sBasic_Set(string sFormNm, TextEdit txt_Key)
        {
            try
            {
                SqlParam sp = new SqlParam("sp_BasicSet");
                sp.AddParam("FormNM", sFormNm);

                ret = DbHelp.Proc_Search(sp);
                if (ret.ReturnChk == 0)
                {
                    if (ret.ReturnDataSet.Tables[0].Rows.Count > 0)
                    {
                        if(ret.ReturnDataSet.Tables[0].Rows[0]["Ref_Code"].ToString() == "0")
                        {
                            txt_Key.Enabled = true;
                            txt_Key.BackColor = Color.White;
                        }
                        else
                        {
                            txt_Key.Enabled = false;
                            txt_Key.BackColor = Color.LightGray;
                        }
                    }
                    else
                    {
                        txt_Key.Enabled = true;
                        txt_Key.BackColor = Color.White;
                        txt_Key.Focus();
                    }
                }
                else
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        public static string Return_FormNM(string sForm)
        {
            string sFormNM = "";

            try
            {
                SqlParam sp = new SqlParam("sp_Form_Name");
                sp.AddParam("FormName", sForm);

                ret = DbHelp.Proc_Search(sp);

                if(ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return "";
                }

                sFormNM = ret.ReturnDataSet.Tables[0].Rows[0]["FormName"].ToString();
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return "";
            }

            return sFormNM;
        }

        public static DateTime GetFirstDay(this DateTime dt)
        {
            return dt.AddDays(1 - dt.Day);
        }

        public static DateTime GetFirstWeekend(this DateTime dt)
        {
            while (dt.DayOfWeek != DayOfWeek.Sunday)
            {
                dt = dt.AddDays(1);
            }

            return dt;
        }

        public static string Return_NumKr(long Number)
        {
            string[] NumberChar = new string[] { "", "일", "이", "삼", "사", "오", "육", "칠", "팔", "구" };
            string[] LevelChar = new string[] { "", "십", "백", "천" };
            string[] DecimalChar = new string[] { "", "만", "억", "조", "경" };

            string strValue = string.Format("{0}", Number);
            string NumKr = string.Empty;
            bool UseDecimal = false;

            if (Number == 0)
                return "영";
            
            for(int i = 0; i < strValue.Length; i++)
            {
                int Level = strValue.Length - i;
                if(strValue.Substring(i, 1) != "0")
                {
                    UseDecimal = true;
                    if(((Level - 1) % 4) == 0)
                    {
                        if (DecimalChar[(Level - 1) / 4] != string.Empty && strValue.Substring(i, 1) == "1")
                            NumKr = NumKr + DecimalChar[(Level - 1) / 4];
                        else
                            NumKr = NumKr + NumberChar[int.Parse(strValue.Substring(i, 1))] + DecimalChar[(Level - 1) / 4];

                        UseDecimal = false;
                    }
                    else
                    {
                        if (strValue.Substring(i, 1) == "1")
                            NumKr = NumKr + LevelChar[(Level - 1) % 4];
                        else
                            NumKr = NumKr + NumberChar[int.Parse(strValue.Substring(i, 1))] + LevelChar[(Level - 1) % 4];
                    }
                }
                else
                {
                    if((Level % 4 == 0) && UseDecimal)
                    {
                        NumKr = NumKr + DecimalChar[(Level / 4)];
                        UseDecimal = false;
                    }
                }
            }

            return NumKr;
        }

        public static string Return_SumQty(GridControlEx gc_Sum, string sColumn_Gubun, string sColumn_Sum, int iDeciaml)
        {
            string sReturn = "";

            DataTable dt_sum = gc_Sum.DataSource as DataTable;

            DataTable dt_Return = new DataTable();
            dt_Return.Columns.Add("Name", typeof(string));
            dt_Return.Columns.Add("Qty", typeof(decimal));

            for (int i = 0; i < dt_sum.Rows.Count; i++)
            {
                DataRow dr;
                if (dt_Return.Select("Name = '" + dt_sum.Rows[i][sColumn_Gubun].ToString() + "'").Length > 0)
                {
                    dr = dt_Return.Select("Name = '" + dt_sum.Rows[i][sColumn_Gubun].ToString() + "'")[0];
                    dr.BeginEdit();
                    dr["Qty"] = decimal.Parse(dr[sColumn_Sum].NumString()) + decimal.Parse(dt_sum.Rows[i][sColumn_Sum].NumString());
                    dr.EndEdit();
                }
                else
                {
                    dr = dt_Return.NewRow();
                    dr["Name"] = dt_sum.Rows[i][sColumn_Gubun].ToString();
                    dr["Qty"] = decimal.Parse(dt_sum.Rows[i][sColumn_Sum].NumString());
                    dt_Return.Rows.Add(dr);
                }
            }

            for (int i = 0; i < dt_Return.Rows.Count; i++)
            {
                sReturn += string.Format("{0:n" + iDeciaml.ToString() + "}", dt_Return.Rows[i]["Qty"]) + " " + dt_Return.Rows[i]["Name"].ToString() + ", ";
            }

            if(sReturn.Length > 0)
                sReturn = sReturn.Substring(0, sReturn.Length - 2);

            return sReturn;
        }
    }
}

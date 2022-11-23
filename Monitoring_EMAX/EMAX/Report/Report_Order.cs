using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace EMAX_Monitoring
{
    public partial class Report_Order : DevExpress.XtraReports.UI.XtraReport
    {
        private float[] Order_Width = new float[7] { 300F, 170F, 120F, 120F, 120F, 140F, 130F };
        private float[] SItem_Width = new float[9] { 300F, 130F, 90F, 90F, 90F, 100F, 100F, 100F, 100F };

        public Report_Order()
        {
            InitializeComponent();
        }

        public Report_Order(string Order_No)
        {
            InitializeComponent();

            SqlParam sp = new SqlParam("sp_regOrder");
            sp.AddParam("Kind", "P");
            sp.AddParam("Order_No", Order_No);

            ReturnStruct ret = DbHelp.Proc_Search(sp);

            if (ret.ReturnChk == 0)
            {
                DataSet Data = ret.ReturnDataSet;

                DataRow Title = Data.Tables[0].Rows[0];
                DataTable Order = Data.Tables[1];
                DataTable SItem = Data.Tables[2];

                decimal Order_Sum = Convert.ToDecimal(Order.Compute("SUM(Amt)", ""));
                decimal SItem_Sum = Convert.ToDecimal(SItem.Compute("SUM(Amt)", ""));

                string str_Title = Title["Order_Date"].NullString() + " (" + Title["Custom_Name"].NullString() + ") " + Title["Project_Title"].NullString();

                Label_Title.Text = str_Title;
                Label_Order_No.Text = "수주번호 : " + Title["Order_No"].NullString();

                if (!string.IsNullOrWhiteSpace(Title["COrder_No"].NullString()))
                    Label_Order_No.Text += " (" + Title["COrder_No"].NullString() + ") ";

                XRTable Order_T = Make_Table(Order, "Order", Order_Sum);
                XRTable SItem_T = Make_Table(SItem, "SItem", SItem_Sum, Order_T);

                Detail.Controls.Add(SItem_T);
                ReportPrintTool print = new ReportPrintTool(this);
                this.ShowPrintMarginsWarning = false;

                print.ShowPreview();
            }
        }

        private XRTable Make_Table(DataTable Table, string Kind, decimal Sum, XRTable Order_XR = null)
        {
            XRTable XR_Table;
            float[] Width = (Kind == "Order") ? Order_Width : SItem_Width;

            if (Order_XR == null)
                XR_Table = XRTable_Set();
            else
                XR_Table = Order_XR;

            XR_Table.BeginInit();

            XRTableRow Head_Blank = new XRTableRow();
            XRTableCell Head_Cell = Add_Cell("  ", 850F, Color.White, 10F, true);
            Head_Cell.BorderColor = Color.White;
            Head_Blank.Cells.Add(Head_Cell);
            XR_Table.Rows.Add(Head_Blank);

            XR_Table.Rows.Add(Row_Header(Kind));

            foreach (DataRow Row in Table.Rows)
            {
                XR_Table.Rows.Add(Row_DataAdd(Row, Kind));
            }

            Tail_Row(XR_Table, Table, Sum, Kind);

            XR_Table.EndInit();

            return XR_Table;
        }

        private XRTable XRTable_Set()
        {
            XRTable Table = new XRTable();

            Table.LocationF = new PointF(34.5F, 0F);
            Table.WidthF = 1100F;
            Table.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);

            return Table;
        }

        private XRTableRow Row_Header(string Kind)
        {
            XRTableRow XRRow = new XRTableRow();

            if (Kind == "Order")
            {
                XRRow.Cells.Add(Add_Cell("품명", Order_Width[0], Color.SkyBlue, 10F, true));
                XRRow.Cells.Add(Add_Cell("규격", Order_Width[1], Color.SkyBlue, 10F, true));
                XRRow.Cells.Add(Add_Cell("거래처품번", Order_Width[2], Color.SkyBlue, 10F, true));
                XRRow.Cells.Add(Add_Cell("수량", Order_Width[3], Color.SkyBlue, 10F, true));
                XRRow.Cells.Add(Add_Cell("단가", Order_Width[4], Color.SkyBlue, 10F, true));
                XRRow.Cells.Add(Add_Cell("금액", Order_Width[5], Color.SkyBlue, 10F, true));
                XRRow.Cells.Add(Add_Cell("납기", Order_Width[6], Color.SkyBlue, 10F, true));
            }
            else if (Kind == "SItem")
            {
                XRRow.Cells.Add(Add_Cell("자재명", SItem_Width[0], Color.SkyBlue, 10F, true));
                XRRow.Cells.Add(Add_Cell("규격", SItem_Width[1], Color.SkyBlue, 10F, true));
                XRRow.Cells.Add(Add_Cell("총소요량", SItem_Width[2], Color.SkyBlue, 10F, true));
                XRRow.Cells.Add(Add_Cell("재고수량", SItem_Width[3], Color.SkyBlue, 10F, true));
                XRRow.Cells.Add(Add_Cell("필요수량", SItem_Width[4], Color.SkyBlue, 10F, true));
                XRRow.Cells.Add(Add_Cell("단가", SItem_Width[5], Color.SkyBlue, 10F, true));
                XRRow.Cells.Add(Add_Cell("금액", SItem_Width[6], Color.SkyBlue, 10F, true));
                XRRow.Cells.Add(Add_Cell("거래처", SItem_Width[7], Color.SkyBlue, 10F, true));
                XRRow.Cells.Add(Add_Cell("비고", SItem_Width[8], Color.SkyBlue, 10F, true));
            }

            return XRRow;
        }

        private XRTableRow Row_DataAdd(DataRow Row, string Kind)
        {
            XRTableRow Table_Row = new XRTableRow();
            int Column_Index = 0;

            float[] Column_Width = (Kind == "Order") ? Order_Width : SItem_Width;

            foreach (DataColumn Column in Row.Table.Columns)
            {
                if (Row[Column].GetType() == typeof(decimal) && Column.ColumnName.Contains("_Qty"))
                    Table_Row.Cells.Add(Add_Cell(Row[Column], Column_Width[Column_Index], Color.White, 9F, false, "1", 3));  // 수량, 금액부분 => 우측 정렬 (자재 쪽 수량 -> 소수점 3자리)
                else if (Row[Column].GetType() == typeof(decimal) && !Column.ColumnName.Contains("_Qty"))
                    Table_Row.Cells.Add(Add_Cell(Row[Column], Column_Width[Column_Index], Color.White, 9F, false, "1"));     // 수량, 금액부분 => 우측 정렬
                else if (Column.ColumnName == "Ssize" || Column.ColumnName == "Order_Bigo")
                    Table_Row.Cells.Add(Add_Cell(string.Format("{0:n0}", Row[Column]), Column_Width[Column_Index], Color.White, 9F, false, "2"));     // 규격, 비고 => 좌측 정렬
                else
                    Table_Row.Cells.Add(Add_Cell(string.Format("{0:n0}", Row[Column]), Column_Width[Column_Index], Color.White, 9F, false));          // 나머지 => 중앙 정렬

                Column_Index++;
            }

            return Table_Row;
        }

        private XRTableCell Add_Cell(object Caption, float Width, Color BackColor, float Font_Size, bool Title, string Alignment = "0", int Point_Length = 0)
        {
            XRTableCell cell = new XRTableCell();

            cell.BackColor = BackColor;
            cell.BorderColor = Color.Black;
            cell.Borders = (BackColor == Color.SkyBlue || !Title) ? DevExpress.XtraPrinting.BorderSide.All : DevExpress.XtraPrinting.BorderSide.None;
            cell.Font = new Font("맑은 고딕", Font_Size, Title ? FontStyle.Bold : FontStyle.Regular);      // 제목인 경우 폰트 굵게
            if (Caption.GetType() == typeof(decimal))
                cell.Text = String.Format("{0:N" + Point_Length.NumString() + "}", Caption);
            else
                cell.Text = Caption.NullString();
            cell.WidthF = Width;
            cell.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2);
            cell.Multiline = true;

            if (Alignment == "1")
                cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            else if (Alignment == "2")
                cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            else
                cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

            return cell;
        }

        private void Tail_Row(XRTable XR_Table, DataTable Data, decimal Sum, string Kind)
        {
            XRTableRow Tail = new XRTableRow();
            float[] Width = (Kind == "Order") ? Order_Width : SItem_Width;
            float Sum_Location = -300F;
            float Right_Blank = 0;

            Tail.Cells.Add(Add_Cell("합계  :", Width[0], Color.AntiqueWhite, 10F, true));

            for (int i = 0; i < Data.Columns.Count; i++)
            {
                if (Data.Columns[i].ColumnName == "Amt")
                {
                    for (int j = i + 1; j < Data.Columns.Count; j++)
                    {
                        Right_Blank += Width[j];
                    }

                    Tail.Cells.Add(Add_Cell(" ", Sum_Location, Color.AntiqueWhite, 10F, true, "2"));
                    Tail.Cells.Add(Add_Cell(Sum, (XR_Table.WidthF - Sum_Location - Width[0] - Right_Blank), Color.AntiqueWhite, 10F, true, "1"));
                    Tail.Cells.Add(Add_Cell(" ", Right_Blank, Color.AntiqueWhite, 10F, true, "2"));

                    break;
                }

                Sum_Location += Width[i];
            }

            XR_Table.Rows.Add(Tail);
        }
    }
}

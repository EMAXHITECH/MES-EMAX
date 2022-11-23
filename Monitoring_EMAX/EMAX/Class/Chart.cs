using DevExpress.Utils;
using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EMAX_Monitoring
{
    public static class Chart
    {
        public static ChartControl chart_ctrl = new ChartControl();
        public static string d_length = string.Empty;
        public static string txt_ptrn = string.Empty;

        public static ChartControl Get_Chart(ChartControl chart, ViewType view_type, DataTable data, string arg, string val, int deciaml_length, string custom_text_ptrn)
        {
            d_length = deciaml_length.NumString();
            txt_ptrn = custom_text_ptrn;

            chart_ctrl = chart;
            chart_ctrl.Series.Clear();

            DataTable table = DbHelp.Fill_Table(data);
            table.DefaultView.Sort = val + " DESC";

            Series sr = new Series("S", view_type);
            sr.DataSource = table;
            sr.ArgumentDataMember = arg;
            sr.ValueDataMembers.AddRange(new string[] { val });
            sr.Label.TextPattern = "{VP:P2} {A}   {V:N" + deciaml_length.NumString() + "}"+ custom_text_ptrn;
            sr.LegendTextPattern = "{VP:P0} {A}";

            chart_ctrl.Series.Add(sr);
            chart_ctrl.ToolTipEnabled = DefaultBoolean.True;
            chart_ctrl.MouseMove += new System.Windows.Forms.MouseEventHandler(Mouse_Over);

            return chart;
        }

        public static void Mouse_Over(object sender, MouseEventArgs e)
        {
            chart_ctrl.Series[0].ToolTipPointPattern = "{VP:P2} {A}   {V:N" + d_length + "}" + txt_ptrn;
            chart_ctrl.Series[0].ToolTipSeriesPattern = "{VP:P2} {A}   {V:N"+ d_length + "}" + txt_ptrn;
            chart_ctrl.Series[0].CrosshairLabelPattern = "{VP:P2} {A}   {V:N"+ d_length + "}" + txt_ptrn;
        }
    }
}

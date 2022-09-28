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
using System.Diagnostics;
using DevExpress.DataProcessing;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraBars;
using DevExpress.DashboardCommon.Native;
using DevExpress.XtraEditors.Controls;
using System.Data.SqlClient;

namespace MES
{
    public class CommonUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        //protected bool IsArrowNavi = false;
        public bool IsArrowNavi = true; //modified on 2020.07.15

        public event TabClosingEventHandler TabClosing;
        public void FireFormClosing(TabClosingEventArgs e)
        {
            TabClosing?.Invoke(this, e);
        }



        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (DesignMode) return;

            var components = this.GetType().GetField("components", System.Reflection.BindingFlags.Instance|System.Reflection.BindingFlags.NonPublic)?.GetValue(this) as IContainer;
        }
        
    }
}

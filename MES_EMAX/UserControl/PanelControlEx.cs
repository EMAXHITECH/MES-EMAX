using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MES
{
    public partial class PanelControlEx : PanelControl
    {
        public PanelControlEx()
        {
            InitializeComponent();
        }

        public PanelControlEx(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;

                cp.ExStyle |= 0x00000020; //WS_EX_TRANSPARENT

                return cp;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {

        }

        protected override void OnResize(EventArgs eventargs)
        {
            if (Parent == null)
                return;

            Rectangle rc = new Rectangle(Location, Size);
            Parent.Invalidate(rc, true);
        }
    }
}

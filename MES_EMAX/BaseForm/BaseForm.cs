using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace MES
{
    public partial class BaseForm : DevExpress.XtraEditors.XtraForm
    {
        Timer time = new Timer();

        public BaseForm()
        {
            InitializeComponent();
        }

        private void BaseForm_Load(object sender, EventArgs e)
        {
            Control[] controls = GetAllControls(this);

            foreach (Control control in controls)
            {
                if (control.GetType() != typeof(MemoEdit) && control.GetType() != typeof(GridControlEx) && control.Controls.Count == 0)
                    control.KeyDown += Control_KeyDown;

                if (control.GetType() == typeof(DateEdit))
                    (control as DateEdit).EditValueChanged += new EventHandler(Control_TextChange);
                else if (control.GetType() != typeof(SimpleButtonEx))
                    control.TextChanged += new EventHandler(Control_TextChange);
            }
        }

        protected virtual void Control_TextChange(object sender, EventArgs e) {}

        protected void Control_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
                e.Handled = true;
            }
        }

        private void BaseForm_KeyDown(object sender, KeyEventArgs e)
        {
            //Type type = ((XtraForm)sender).ActiveControl.Parent.GetType();
            //if (e.KeyCode == Keys.Enter && type != typeof(MemoEdit))
            //{
            //    SendKeys.Send("{TAB}");
            //    e.Handled = true;
            //}
        }

        private Control[] GetAllControls(Control containerCT)
        {
            List<Control> allControls = new List<Control>();

            foreach (Control control in containerCT.Controls)
            {
                allControls.Add(control);

                if (control.Controls.Count > 0 && control.GetType() != typeof(MemoEdit))
                {
                    allControls.AddRange(GetAllControls(control));
                }
            }

            return allControls.ToArray();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F2) //추가
            {
                btnInsert();
            }
            else if (keyData == Keys.F5) //저장
            {
                btnSave();
            }
            else if (keyData == Keys.F9) //삭제
            {
                btnDelete();
            }
            else if (keyData == Keys.F7) //종료
            {
                btnClose();
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected virtual void btnInsert() { }
        protected virtual void btnSave() { }
        protected virtual void btnDelete() { }
        protected virtual void btnClose() { }
    }
}
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

namespace MES
{
    public partial class BaseUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        public BaseUserControl()
        {
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.F2) //추가
            {
                btnInsert();
            }
            else if(keyData == Keys.F3) //조회
            {
                btnSelect();
            }
            else if(keyData == Keys.F5) //저장
            {
                btnSave();
            }
            else if(keyData == Keys.F6) // 엑셀
            {
                btnExcel();
            }
            else if(keyData == Keys.F9) //삭제
            {
                btnDelete();
            }
            else if(keyData == Keys.F7) //종료
            {
                btnClose();
            }
            else if(keyData == Keys.F8) //프린트
            {
                btnPrint();
            }
            else if(keyData == Keys.F10) //복사
            {
                btnCopy();
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected virtual void btnSelect(){ }
        protected virtual void btnInsert() { }
        protected virtual void btnSave() { }
        protected virtual void btnExcel() { }
        protected virtual void btnDelete() { }
        protected virtual void btnClose() { }
        protected virtual void btnPrint() { }
        protected virtual void btnCopy() { }

        private void BaseUserControl_Load(object sender, EventArgs e)
        {
            this.Focus();

            foreach(Control control_M in this.Controls)
            {
                foreach(Control control in control_M.Controls)
                {
                    if(control.GetType() != typeof(MemoEdit))
                        control.KeyDown += Control_KeyDown;

                    if(control.GetType() == typeof(DateEdit))
                        (control as DateEdit).EditValueChanged += new EventHandler(Control_TextChange);
                    else if(control.GetType() != typeof(SimpleButtonEx))
                        control.TextChanged += new EventHandler(Control_TextChange);
                }
            }
        }

        protected virtual void Control_TextChange(object sender, EventArgs e) { }

        private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
                e.Handled = true;
            }
            
        }
    }
}

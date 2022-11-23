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

namespace EMAX_Monitoring
{
    public partial class BaseReg : DevExpress.XtraEditors.XtraUserControl
    {
        ReturnStruct ret = new ReturnStruct();

        public BaseReg()
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

        protected void BaseUserControl_Load(object sender, EventArgs e)
        {
            this.Focus();

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

            //foreach (Control control_M in this.Controls)
            //{
            //    foreach (Control control in control_M.Controls)
            //    {
            //        if (control.GetType() != typeof(MemoEdit) && control.GetType() != typeof(GridControlEx))
            //            control.KeyDown += Control_KeyDown;

            //        if (control.GetType() == typeof(DateEdit))
            //            (control as DateEdit).EditValueChanged += new EventHandler(Control_TextChange);
            //        else if (control.GetType() != typeof(SimpleButtonEx))
            //            control.TextChanged += new EventHandler(Control_TextChange);
            //    }
            //}
            if (this.Name.Trim().Length > 3)
            {
   
                if (this.Name.Substring(0, 3) == "rpt")
                {
                    panButton.Size = new Size(239, 36);
                    btn_Excel.Location = new Point(84, 1);
                    btn_Select.Location = new Point(6, 1);

                    btn_Insert.Visible = false;
                    btn_Save.Visible = false;
                    btn_Delete.Visible = false;
                    btn_Print.Visible = false;
                }
                else if (this.Name.Substring(0, 3) == "reg")
                {
                    SqlParam sp = new SqlParam("sp_Menu_Button");
                    sp.AddParam("USER_CODE", GlobalValue.sUserID);
                    sp.AddParam("FORM_NAME", this.Name.Trim());
                    ret = DbHelp.Proc_Search(sp);

                    if (ret.ReturnChk != 0)
                    {
                        XtraMessageBox.Show(ret.ReturnMessage);
                        return;
                    }

                     DataTable Dt =  DbHelp.Fill_Table(ret.ReturnDataSet.Tables[0]);
                    if (Dt == null) return;
                    if (Dt.Rows.Count == 0) return;

                    string sSe = Convert.ToString(Dt.Rows[0]["SE_CK"]);
                    string sDt = Convert.ToString(Dt.Rows[0]["DT_CK"]);
                    string sPt = Convert.ToString(Dt.Rows[0]["PT_CK"]);

                    //조회
                    if (sSe == "0" && sDt == "0" && sPt == "0")
                    {
                        btn_Print.Visible = false;
                        btn_Delete.Visible = false;
                        btn_Select.Visible = false;

                        panButton.Size = new Size(315, 36);

                        btn_Excel.Location = new Point(160, 1);
                        btn_Save.Location = new Point(82, 1);
                        btn_Insert.Location = new Point(4, 1);
                    }
                    else if (sSe == "0" && sDt == "0" && sPt == "1")
                    {
                        btn_Delete.Visible = false;
                        btn_Select.Visible = false;

                        panButton.Size = new Size(392, 36);

                        btn_Save.Location = new Point(81, 1);
                        btn_Insert.Location = new Point(3, 1);
                    }
                    else if (sSe == "0" && sDt == "1" && sPt == "0")
                    {
                        btn_Print.Visible = false;
                        btn_Select.Visible = false;

                        panButton.Size = new Size(392, 36);

                        btn_Excel.Location = new Point(237, 1);
                        btn_Delete.Location = new Point(159, 1);
                        btn_Save.Location = new Point(81, 1);
                        btn_Insert.Location = new Point(3, 1);
                    }
                    else if (sSe == "1" && sDt == "0" && sPt == "0")
                    {
                        btn_Print.Visible = false;
                        btn_Delete.Visible = false;

                        panButton.Size = new Size(392, 36);

                        btn_Excel.Location = new Point(237, 1);
                        btn_Save.Location = new Point(159, 1);
                        btn_Insert.Location = new Point(81, 1);
                        btn_Select.Location = new Point(3, 1);
                    }
                    else if (sSe == "1" && sDt == "1" && sPt == "0")
                    {
                        btn_Print.Visible = false;

                        panButton.Size = new Size(471, 36);

                        btn_Select.Location = new Point(3, 1);
                        btn_Insert.Location = new Point(82, 1);
                        btn_Save.Location = new Point(160, 1);
                        btn_Delete.Location = new Point(238, 1);
                        btn_Excel.Location = new Point(316, 1);
                    }
                    else if (sSe == "0" && sDt == "1" && sPt == "1")
                    {
                        btn_Select.Visible = false;

                        panButton.Size = new Size(471, 36);
                    }
                    else if (sSe == "1" && sDt == "0" && sPt == "1")
                    {
                        btn_Delete.Visible = false;

                        panButton.Size = new Size(471, 36);

                        btn_Select.Location = new Point(3, 1);
                        btn_Insert.Location = new Point(81, 1);
                        btn_Save.Location = new Point(160, 1);
                    }
                }
            }

        }

        //전체 컨트롤 가지고 오기
        private Control[] GetAllControls(Control containerCT)
        {
            List<Control> allControls = new List<Control>();

            foreach(Control control in containerCT.Controls)
            {
                allControls.Add(control);

                if(control.Controls.Count > 0 && control.GetType() != typeof(MemoEdit))
                {
                    allControls.AddRange(GetAllControls(control));
                }
            }

            return allControls.ToArray();
        }


        protected void Menu_Jump(string Jump_Form, string Menu_Name)
        {
           //MainForm.Menu_Jump(Jump_Form, Menu_Name);
        }


        protected virtual void Control_TextChange(object sender, EventArgs e) { }

        protected void Control_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
                e.Handled = true;
            }
            
        }
    }
}

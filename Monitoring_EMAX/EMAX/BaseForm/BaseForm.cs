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

namespace EMAX_Monitoring
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

                //control_M.TextChanged += new EventHandler(Control_TextChange);
                //foreach(Control control_S in control_M.Controls)
                //{
                //    control_S.TextChanged += new EventHandler(Control_TextChange);
                //}
            }

            btn_File.Location = new Point(panelControlEx1.Size.Width - btn_File.Size.Width - 3, 4);

            File_Check(null, null);
            //time.Tick += File_Check;
            //time.Interval = 1000;
            //time.Start();
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

        private void btn_File_Click(object sender, EventArgs e)
        {
            try
            {
                PanelControl[] Panels = this.Controls.OfType<PanelControl>().OrderBy(x => x.Location.Y).ToArray();

                if (Panels != null && Panels.Length > 1)
                {
                    TextEdit[] Text_Edits = Panels[1].Controls.OfType<TextEdit>().OrderBy(x => x.Location.X).ToArray();

                    if (Text_Edits != null && Text_Edits.Length > 0)
                    {
                        TextEdit PK_Text = Text_Edits[0];

                        if (string.IsNullOrWhiteSpace(PK_Text.Text))
                        {
                            XtraMessageBox.Show("파일 업로드 전에 저장을 진행해주시길 바랍니다.");
                            return;
                        }

                    }
                }
            }
            catch (Exception)
            {
                XtraMessageBox.Show("정규 등록 페이지가 아닙니다.");
            }
        }

        private void File_Check(object sender, EventArgs e)
        {
            try
            {
                FileIF.Set_URL();
                PanelControl[] Panels = this.Controls.OfType<PanelControl>().OrderBy(x => x.Location.Y).ToArray();

                if (Panels != null && Panels.Length > 1)
                {
                    TextEdit[] Text_Edits = Panels[1].Controls.OfType<TextEdit>().OrderBy(x => x.Location.X).ToArray();

                    if (Text_Edits != null && Text_Edits.Length > 0)
                    {
                        TextEdit PK_Text = Panels[1].Controls.OfType<TextEdit>().OrderBy(x => x.Location.X).ToArray()[0];

                        string Path = Configurations.GetConfig("DBConnstring").Split('=')[3];
                        Path = Path.Substring(0, Path.IndexOf(";"));
                        Path = FileIF.FTP_URL + "/" + Path + "/" + this.Name + "/" + PK_Text.Text;

                        DataTable Files = FileIF.Get_File_List(Path);

                        if (Files == null || Files.Rows.Count == 0)
                        {
                            btn_File.Appearance.BackColor = Color.White;
                            return;
                        }

                        if (Files.Rows.Count > 0)
                        {
                            if (Files.Select("File_Size > 0").Count() > 0)
                            {
                                btn_File.ResetBackColor();
                                btn_File.Appearance.BackColor = Color.ForestGreen;
                            }
                            else
                            {
                                btn_File.Appearance.BackColor = Color.White;
                            }
                        }
                    }
                }

                        
            }
            catch (Exception)
            {
                btn_File.Appearance.BackColor = Color.White;
            }
        }
    }
}
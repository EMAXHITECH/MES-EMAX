using DevExpress.XtraBars;
using DevExpress.XtraBars.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Log;
using System.Diagnostics;
using System.Reflection;
using System.Drawing.Text;
using DevExpress.Utils.Drawing.Helpers;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Docking2010.Customization;
using DevExpress.XtraTab.ViewInfo;
using DevExpress.XtraTab;
using System.Linq;
using System.Data.SqlClient;
using System.Net;
using DevExpress.XtraEditors.Repository;
using System.Windows.Media;
using Color = System.Drawing.Color;
using DevExpress.Utils;

namespace SERP
{
    public partial class MainForm : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        bool bFlyPop = false;
        int notice_count = 0;
        Log.Log_text log = new Log_text();
        static PrivateFontCollection privateFonts = new PrivateFontCollection();
        List<UserControl> Form_ = new List<UserControl>();
        ReturnStruct ret = new ReturnStruct();
        public static UserControl User_Ctrl;
        public static PanelControl PK_Panel;
        public static TextEdit PK_Text;
        public static SimpleButton File_Button;
        public static string Menu_SCode;
        public static string New_PK;

        string sUID = "admin";
        string[] sKEY = new string[15];
        string[] sMAIN_TEXT = new string[15];
        string sMgNo = string.Empty;
        string sMgNoSo = string.Empty;
        public static string USER_ID, USER_NAME, DEPT_CODE, DEPT_NAME;

        string sKey_No = "", sForm_Name = "", sMenuName = "";

        public static XtraTabControl MainTab;

        public static SimpleButton btn_Message;

        public static Timer time_message = new Timer();
        private Timer time = new Timer();

        private MediaPlayer mediaPlayer = new MediaPlayer();

        private bool PanelMouse = false;
        private Point startPointPanel, endPointPanel;

        public MainForm()
        {
            InitializeComponent();

            OptionsAdaptiveLayout.AdaptiveLayout = true;
            OptionsAdaptiveLayout.InlineModeThreshold = 650;
            OptionsAdaptiveLayout.OverlayModeThreshold = 450;
            MainTab = xtraTabControl1;
        }

        private void MainForm_New_Load(object sender, EventArgs e)
        {
            fluentDesignFormControl1.Manager.AllowCustomization = false;
            acc_MenuList.Clear();

            string a = "";

            try
            {
                sUID = GlobalValue.sUserID;
                txtStaffName.Caption = GlobalValue.sUserNm;

                Win32SubclasserException.Allow = false;
                acc_MenuList.AllowDrop = true;
                this.Visible = true;
                AssemblyName assemblyName = Assembly.GetExecutingAssembly().GetName();
                acc_MenuList.Images = imgList;
                acc_MenuList.BeginUpdate();
                Menu_Set();
                acc_MenuList.EndUpdate();

                Get_Login_Time();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                log.Log(this.Name + " / " + a + "//" + ex.Message);
            }
        }

        // 로그인 시간 띄우기
        private void Get_Login_Time()
        {
            SqlParam sp = new SqlParam("sp_Log");
            sp.AddParam("Kind", "S");
            sp.AddParam("Log_No", Login.log_no);
            sp.AddParam("In_From", DateTime.MinValue.ToString("yyyyMMdd"));
            sp.AddParam("In_To", DateTime.MaxValue.ToString("yyyyMMdd"));
            ReturnStruct temp_ret = DbHelp.Proc_Search(sp);
            if (temp_ret.ReturnChk != 0)
            {
                XtraMessageBox.Show(temp_ret.ReturnMessage);
                return;
            }
            string login_time = temp_ret.ReturnDataSet.Tables[0].Rows[0]["In_Time"].NullString();

            string mederium = (Convert.ToInt32(login_time.Substring(0, 2)) / 12 == 1) ? "오후 " : "오전 ";
            string hour = (Convert.ToInt32(login_time.Substring(0, 2)) % 12).ToString().PadLeft(2, '0');
            string minute = login_time.Substring(3, 2);
            string second = login_time.Substring(6, 2);

            txt_Login_Time.Text = mederium + hour + "시 " + minute + "분 " + second + "초";
        }

        void cls()
        {
            this.MainForm_New_FormClosing(null, null);
        }

        private void Menu_Set()
        {
            DataTable dt, dt_sub;
            DataRow[] dr, dr_sub;
            AccordionControlElement AccMenuSub1 = new AccordionControlElement();

            try
            {
                SqlParam sp = new SqlParam("sp_Menu_List");
                sp.AddParam("User_Code", sUID);

                ret = DbHelp.Proc_Search(sp);

                if (ret.ReturnChk == 0)
                {
                    dt = ret.ReturnDataSet.Tables[0];
                    dt_sub = ret.ReturnDataSet.Tables[1];

                    if (dt.Rows.Count == 0)
                    {
                        XtraMessageBox.Show("작업자에 설정된 메뉴가 없습니다");
                        return;
                    }

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        AccordionControlElement AccMainMenu = new AccordionControlElement();
                        AccMainMenu = GetElement(dt.Rows[i]["Menu_MCode"].ToString(), ElementStyle.Group, dt.Rows[i]["Menu_MName"].ToString());
                        this.acc_MenuList.Elements.Add(AccMainMenu);

                        dr = dt_sub.Select("Menu_MCode = '" + dt.Rows[i]["Menu_MCode"].ToString() + "'");

                        foreach (DataRow drsub1 in dr)
                        {
                            AccordionControlElement AccMenuSub2;

                            if (drsub1["Form_Name"].ToString() == "")
                            {
                                AccMenuSub1 = GetElement(drsub1["Form_Name"].ToString(), ElementStyle.Group, drsub1["Menu_SName"].ToString());
                                AccMainMenu.Elements.Add(AccMenuSub1);
                            }
                            else
                            {
                                AccMenuSub2 = new AccordionControlElement();
                                AccMenuSub2 = GetElement(drsub1["Form_Name"].ToString(), ElementStyle.Item, drsub1["Menu_SName"].ToString());
                                AccMenuSub1.Elements.Add(AccMenuSub2);
                            }
                        }
                    }
                }
                else
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        // AccordionControlElement 정보 입력
        private AccordionControlElement GetElement(string name, ElementStyle style, string text, bool expanded = true)
        {
            AccordionControlElement element = new AccordionControlElement
            {
                Name = name,        // 명칭
                Style = style,      // 스타일
                Text = text,        // 텍스트
                Expanded = false    // 확장여부
            };
            return element;
        }

        // DEBUG 모드 프로그램 실행시 프로그램명 보여주기 
        public static void sbTabPageAdd(UserControl r_frm, string ControlName)
        {
            try
            {
                int iExist = -1;

                for (int ix = 0; ix <= MainTab.Controls.Count - 1; ix++)
                {
#if DEBUG
                    if (MainTab.Controls[ix].Text == ControlName.Substring(ControlName.IndexOf("ː") + 1) + "(" + r_frm.Name + ")")
#else
                    if (MainTab.Controls[ix].Text == ControlName.Substring(ControlName.IndexOf("ː") + 1))
#endif
                    {
                        iExist = ix;

                        break;
                    }
                }

                if (iExist < 0)
                {
                    MainTab.TabPages.Add(ControlName);
#if DEBUG
                    MainTab.TabPages[MainTab.TabPages.Count - 1].Text = ControlName.Substring(ControlName.IndexOf("ː") + 1) + "(" + r_frm.Name + ")";
#else
                    MainTab.TabPages[MainTab.TabPages.Count - 1].Text = ControlName.Substring(ControlName.IndexOf("ː") + 1);
#endif

                    //Name으로 Form ID 가지고 있기
                    MainTab.TabPages[MainTab.TabPages.Count - 1].Name = r_frm.Name;

                    iExist = MainTab.TabPages.Count - 1;
                    MainTab.TabPages[MainTab.TabPages.Count - 1].Controls.Add(r_frm);
                }

                MainTab.SelectedTabPageIndex = iExist;  //.SelectedIndex = iExist;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message + "// " + ex.StackTrace);
            }
        }

        public void accordionControl1_ElementClick(object sender, ElementClickEventArgs e)
        {
            try
            {
                string sNAME = e.Element.Name;
                string sTEXT = e.Element.Text;
                int TreeLevel = e.Element.Level;    // 0: 대분류, 1: 중분류, 2: 소분류

                if (!string.IsNullOrWhiteSpace(sNAME) && TreeLevel == 2 && sTEXT != "사용자관리" && sTEXT != "프로그램관리")
                {
                    Type type = Type.GetType("SERP." + sNAME);

                    if (type != null)
                    {
                        UserControl form = (UserControl)Activator.CreateInstance(type, null, null);
                        form.Dock = DockStyle.Fill;
                        form.BackColor = Color.FromArgb(226, 238, 247);
                        //form.AutoScaleMode = AutoScaleMode.Dpi;
                        form.AutoScaleMode = AutoScaleMode.None;

                        sbTabPageAdd(form, sTEXT.Replace("ㆍ", ""));

                        Insert_Log_Detail(sTEXT);
                    }
                    else
                    {
                        XtraMessageBox.Show("해당 메뉴 폼이 없습니다");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }

        }

        private void Form_Load(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public static Font GetBasicFont()
        {
            string FolderName = @Application.StartupPath + @"\Font\";
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(FolderName);
            foreach (System.IO.FileInfo File in di.GetFiles())
            {
                if (File.Extension.ToLower().CompareTo(".ttf") == 0)
                {
                    string FileNameOnly = File.Name.Substring(0, File.Name.Length - 4);
                    string FullFileName = File.FullName;
                    privateFonts.AddFontFile(File.FullName);
                }
            }
            Font font9 = new Font(privateFonts.Families[1], 9F);
            return font9;
        }

        //
        int oldSideSize;
        private void btn_Size_DoubleClick(object sender, EventArgs e)
        {

            if (acc_MenuList.Visible == true)
            {
                acc_MenuList.Visible = false;
                oldSideSize = sidePanel3.Width;
                sidePanel3.Width = 6;
            }
            else
            {
                acc_MenuList.Visible = true;
                sidePanel3.Width = oldSideSize;
            }
        }

        private void btn_Logout_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("로그아웃을 하시겠습니까?", "로그아웃", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            time_message.Stop();
            time.Stop();
            this.Hide();

            Insert_Log_Out_Info();

            GlobalValue.sUserID = "";
            GlobalValue.sUserNm = "";

            Login form = new Login();
            form.ShowDialog();
        }

        private void xtraTabControl1_CloseButtonClick_1(object sender, EventArgs e)
        {
            ClosePageButtonEventArgs arg = e as ClosePageButtonEventArgs;
            XtraTabPage a = arg.Page as XtraTabPage;
            CloseTabPage(a);
        }

        private void btn_Cal_Click(object sender, EventArgs e)
        {
            try
            {
                Process Process = new Process();
                Process.EnableRaisingEvents = true;

                if (!Environment.Is64BitProcess)
                {
                    Process.StartInfo.FileName = @"C:\Windows\sysnative\" + "calc.exe";
                    Process.Start();
                }
                else
                {
                    Process.StartInfo.FileName = @"C:\WINDOWS\system32\" + "calc.exe";
                    Process.Start();
                }


                OperatingSystem os = Environment.OSVersion;
                Version v = os.Version;

                if (5 == v.Major && v.Minor > 0)
                {
                    //windows xp
                }

                else if (6 == v.Major && v.Minor == 0)
                {
                    //windows vista
                }

                else if (6 == v.Major && v.Minor == 1)
                {
                    //Windows 7
                }
                else if (6 == v.Major && v.Minor == 2)
                {
                    //Windows 8 , 10
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                log.Log(this.Name + " / " + ex.Message);
            }
        }

        private void btn_Cap_Click(object sender, EventArgs e)
        {
            try
            {

                Process snippingToolProcess = new Process();
                snippingToolProcess.EnableRaisingEvents = true;
                if (!Environment.Is64BitProcess)
                {
                    snippingToolProcess.StartInfo.FileName = "C:\\Windows\\sysnative\\SnippingTool.exe";
                    snippingToolProcess.Start();
                }
                else
                {
                    snippingToolProcess.StartInfo.FileName = "C:\\Windows\\system32\\SnippingTool.exe";
                    snippingToolProcess.Start();
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                log.Log(this.Name + " / " + ex.Message);
            }
        }

        private void btn_Min_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void xtraTabControl1_CustomHeaderButtonClick(object sender, DevExpress.XtraTab.ViewInfo.CustomHeaderButtonEventArgs e)
        {
            int index = xtraTabControl1.SelectedTabPageIndex;
            int Count = xtraTabControl1.TabPages.Count - 1;

            switch (e.Button.Kind.ToString())
            {
                case ("Left"):
                    xtraTabControl1.SelectedTabPageIndex = index - 1;
                    break;
                case ("Right"):
                    xtraTabControl1.SelectedTabPageIndex = index + 1;
                    break;
                case ("Close"):
                    if (xtraTabControl1.TabPages.Count == 0)
                    {
                        return;
                    }
                    else if (xtraTabControl1.TabPages.Count == 1)
                    {
                        CloseAllTabPage();
                    }
                    else if (XtraMessageBox.Show("현재 열린 모든 화면을 닫으시겠습니까?", "확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        CloseAllTabPage();
                    }
                    break;
            }
        }

        private void accordionControl1_MouseMove(object sender, MouseEventArgs e)
        {
            AccordionControl ac = sender as AccordionControl;
            ac.Tag = null;

            if (ac.Capture)
            {
                AccordionControlHitInfo hi = ac.CalcHitInfo(e.Location);
                ac.Tag = new object();

                if (hi.ItemInfo != null && hi.ItemInfo.Element.Style == ElementStyle.Item)
                {
                    ac.DoDragDrop(hi.ItemInfo.Element, DragDropEffects.Move);
                }

                (ac.GetViewInfo() as AccordionControlViewInfo).PressedInfo = AccordionControlHitInfo.Empty;
            }
        }

        private void accordionControl1_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(typeof(AccordionControlElement)) != null)
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void accordionControl1_DragDrop(object sender, DragEventArgs e)
        {
            AccordionControlElement dragElement = e.Data.GetData(typeof(AccordionControlElement)) as AccordionControlElement;
            AccordionControl ac = sender as AccordionControl;

            if (dragElement == null || ac.Tag != null)
            {
                return;
            }

            AccordionControlHitInfo hi = ac.CalcHitInfo(ac.PointToClient(new Point(e.X, e.Y)));
            if (hi.ItemInfo != null)
            {
                AccordionControlElement element = hi.ItemInfo.Element;
                if (element.OwnerElement != null)
                {
                    element.OwnerElement.Elements.Add(dragElement);
                }
                else
                {
                    ac.Elements.Add(dragElement);
                }
            }
        }

        private void acc_MenuList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int a = e.KeyValue;

            }
        }



        protected bool CloseTabPage(XtraTabPage page, TabClosingEventArgs e = null)
        {
            if (e == null) e = new TabClosingEventArgs(CloseReason.UserClosing, false);

            e.TabControl = xtraTabControl1;
            e.TabPage = page;

            foreach (var view in page.Controls.OfType<CommonUserControl>())
            {
                view.FireFormClosing(e);
                if (e.Cancel) break;
            }

            bool closed = !e.Cancel;
            if (closed)
            {
                xtraTabControl1.TabPages.Remove(page);
            }

            return closed;
        }

        protected bool CloseAllTabPage(CloseReason reason = CloseReason.UserClosing)
        {
            var e = new TabClosingEventArgs(reason, false);
            e.MultipleTab = true;

            int Count = xtraTabControl1.TabPages.Count - 1;
            for (int i = Count; i >= 0; i--)
            {
                var page = xtraTabControl1.TabPages[i];
                if (!CloseTabPage(page, e)) return false;
            }

            return true;
        }

        private void MainForm_New_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FlyoutMessageBox.Show("프로그램을 종료하시겠습니까?", "종료", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1) == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                if (xtraTabControl1.TabPages.Count > 0)
                {
                    if (!CloseAllTabPage(e.CloseReason))
                    {
                        e.Cancel = true;
                        return;
                    }
                }

                Insert_Log_Out_Info();

                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
        }

        // 프로그램 사용 로그 기록
        public void Insert_Log_Detail(string menu_name)
        {
            SqlParam sp = new SqlParam("sp_Log");
            sp.AddParam("Kind", "C");
            sp.AddParam("Log_No", Login.log_no);
            sp.AddParam("Excuted_Menu", menu_name);

            ReturnStruct temp_ret = DbHelp.Proc_Save(sp);
            if (temp_ret.ReturnChk != 0)
            {
                XtraMessageBox.Show(temp_ret.ReturnMessage);
                return;
            }
        }

        // 로그아웃 정보 입력
        public void Insert_Log_Out_Info()
        {
            SqlParam sp = new SqlParam("sp_Log");
            sp.AddParam("Kind", "O");
            sp.AddParam("Log_No", Login.log_no);

            ReturnStruct temp_ret = new ReturnStruct();
            temp_ret = DbHelp.Proc_Save(sp);
            if (temp_ret.ReturnChk != 0)
            {
                XtraMessageBox.Show(temp_ret.ReturnMessage);
                return;
            }
        }

        // 로그아웃 클릭
        private void Logout_Click(object sender, ItemClickEventArgs e)
        {
            btn_Logout_Click(null, null);
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            btn_Cal_Click(null, null);
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            btn_Cap_Click(null, null);
        }



        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.W))
            {
                if (xtraTabControl1.TabPages.Count > 0)
                {
                    int index = xtraTabControl1.SelectedTabPageIndex;
                    var page = xtraTabControl1.TabPages[index];

                    if (CloseTabPage(page))
                    {
                        xtraTabControl1.SelectedTabPageIndex = xtraTabControl1.TabPages.Count - 1;
                    }

                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }


        private void sidePanel3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btn_Size_DoubleClick(null, null);
        }

        //
        bool drag;
        int x;
        int sideSize;

        private void btn_ChangePW_ItemClick(object sender, ItemClickEventArgs e)
        {
            Change_PW change_PW = new Change_PW();
            change_PW.sID = GlobalValue.sUserID;

            if (change_PW.ShowDialog() == DialogResult.No)
                return;

            this.Hide();

            Insert_Log_Out_Info();

            GlobalValue.sUserID = "";
            GlobalValue.sUserNm = "";

            Login form = new Login();
            form.ShowDialog();
        }

        private void btn_Capture_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Process snippingToolProcess = new Process();
                snippingToolProcess.EnableRaisingEvents = true;
                if (!Environment.Is64BitProcess)
                {
                    snippingToolProcess.StartInfo.FileName = "C:\\Windows\\sysnative\\SnippingTool.exe";
                    snippingToolProcess.Start();
                }
                else
                {
                    snippingToolProcess.StartInfo.FileName = "C:\\Windows\\system32\\SnippingTool.exe";
                    snippingToolProcess.Start();
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                log.Log(this.Name + " / " + ex.Message);
            }
        }

        private void btn_Calculator_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Process Process = new Process();
                Process.EnableRaisingEvents = true;

                if (!Environment.Is64BitProcess)
                {
                    Process.StartInfo.FileName = @"C:\Windows\sysnative\" + "calc.exe";
                    Process.Start();
                }
                else
                {
                    Process.StartInfo.FileName = @"C:\WINDOWS\system32\" + "calc.exe";
                    Process.Start();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                log.Log(this.Name + " / " + ex.Message);
            }
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            if (e.PrevPage != null)
            {
                if (e.Page != null)
                {
                    e.PrevPage.Appearance.Header.Font = e.Page.Appearance.Header.Font;
                    e.PrevPage.Appearance.Header.ForeColor = e.Page.Appearance.Header.ForeColor;

                    if (e.Page.Name == "rptMainScreen")
                    {
                        if (time_message.Enabled)
                            time_message.Stop();
                    }
                    else
                    {
                        if (!time_message.Enabled)
                            time_message.Start();
                    }
                }
            }

            if (e.Page != null)
            {
                e.Page.Appearance.Header.Font = new Font("나눔바른고딕", 11F, FontStyle.Bold);
                e.Page.Appearance.Header.ForeColor = Color.DarkRed;
            }
        }

        private void btn_File_Click(object sender, EventArgs e)
        {
            if (xtraTabControl1.TabPages.Count <= 0)
                return;

            UserControl user_ctrl = (UserControl)xtraTabControl1.SelectedTabPage.Controls[0];

            try
            {
                PanelControl PK_Panel = user_ctrl.Controls.OfType<PanelControl>().OrderBy(x => x.Location.Y).ToArray()[1];
                TextEdit PK_Text = PK_Panel.Controls.OfType<TextEdit>().OrderBy(x => x.Location.X).ToArray()[0];

                if (string.IsNullOrWhiteSpace(PK_Text.Text))
                {
                    XtraMessageBox.Show("파일 업로드 전에 저장을 진행해주시길 바랍니다.");
                    return;
                }

                FileForm Form = new FileForm(user_ctrl.Name, PK_Text.Text);
                Form.StartPosition = FormStartPosition.CenterScreen;
                Form.ShowDialog();
            }
            catch (Exception)
            {
                XtraMessageBox.Show("정규 등록 페이지가 아닙니다.");
            }
        }
        private void sidePanel3_MouseDown(object sender, MouseEventArgs e)
        {
            drag = true;
            x = e.X;
            sideSize = sidePanel3.Width;
        }

        private void sidePanel3_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;
        }


        private void sidePanel3_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag)
            {
                sidePanel3.Width = sideSize + (e.X - x);
            }

        }

        public static void Menu_Jump(string Jump_Form, string Menu_Name, string sSearch_Key)
        {
            string sNAME = Jump_Form;

            int iExist = MainTab.Controls.IndexOfKey(sNAME);

            if (iExist > 0)
            {
                MainTab.SelectedTabPageIndex = iExist;
                MainTab.TabPages.Remove(MainTab.SelectedTabPage);
            }

            if (!string.IsNullOrWhiteSpace(Jump_Form))
            {
                Type type = Type.GetType("SERP." + sNAME);

                if (type != null)
                {
                    BaseReg form = (BaseReg)Activator.CreateInstance(type, null, null);
                    form.Dock = DockStyle.Fill;
                    form.BackColor = Color.FromArgb(226, 238, 247);
                    form.AutoScaleMode = AutoScaleMode.None;

                    sbTabPageAdd(form, Menu_Name);
                    form.Search_Key(sSearch_Key);
                }
                else
                {
                    XtraMessageBox.Show("해당 메뉴 폼이 없습니다");
                    return;
                }
            }

        }
    }

    public delegate void TabClosingEventHandler(object sender, TabClosingEventArgs e);
    public class TabClosingEventArgs : CancelEventArgs
    {
        public TabClosingEventArgs() { }
        public TabClosingEventArgs(CloseReason closeReason, bool cancel) : base(cancel)
        {
            CloseReason = closeReason;
        }

        public CloseReason CloseReason { private set; get; }

        public DialogResult CloseSaveResult { set; get; }

        public bool MultipleTab { set; get; }

        public XtraTabControl TabControl { set; get; }

        public XtraTabPage TabPage { set; get; }

        public void SelectTab()
        {
            TabControl.SelectedTabPage = TabPage;
        }
    }
}
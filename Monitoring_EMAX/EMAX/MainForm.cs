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

        public MainForm()
        {
            InitializeComponent();

            OptionsAdaptiveLayout.AdaptiveLayout = true;
            OptionsAdaptiveLayout.InlineModeThreshold = 650;
            OptionsAdaptiveLayout.OverlayModeThreshold = 450;
            MainTab = xtraTabControl1;
            File_Button = btn_File;
            btn_Message = btnMessage;
        }

        private void MainForm_New_Load(object sender, EventArgs e)
        {
            Notice_Show(null, null);
            Message_Rcv(null, null);

            time.Tick += Notice_Show;
            time.Interval = 1000 * 5;
            time.Start();
            
            time_message.Interval = 3000;
            time_message.Tick += Message_Rcv;

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

                GridSet();

                Screen_MainForm();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                log.Log(this.Name + " / " + a + "//" + ex.Message);
            }
        }

        #region 플라이 팝

        private void GridSet()
        {
            DbHelp.GridSet(GridMaster, ViewMaster, "Chk", "선택", "45", false, true, true);
            DbHelp.GridSet(GridMaster, ViewMaster, "User_Code", "사용자코드", "120", false, false, false);
            DbHelp.GridSet(GridMaster, ViewMaster, "User_Name", "사용자", "100", false, false, true);
            DbHelp.GridSet(GridMaster, ViewMaster, "Dept_Name", "부서", "100", false, false, true);


            RepositoryItemCheckEdit repositoryCheckEdit = GridMaster.RepositoryItems.Add("CheckEdit") as RepositoryItemCheckEdit;
            repositoryCheckEdit.ValueChecked = "Y";
            repositoryCheckEdit.ValueUnchecked = "N";
            ViewMaster.Columns["Chk"].ColumnEdit = repositoryCheckEdit;
            ViewMaster.OptionsSelection.MultiSelect = true;

            Qr_sp_Help_User();
        }

        private void Qr_sp_Help_User()
        {
            ret = new ReturnStruct();
            SqlParam sp = new SqlParam("sp_Help_User");
            sp.AddParam("GB", "3");
            ret = DbHelp.Proc_Search(sp);
            DataTable dt = DbHelp.Fill_Table(ret.ReturnDataSet.Tables[0]);
            GridMaster.DataSource = ret.ReturnDataSet.Tables[0];
        }
        private void btnMessage_Click(object sender, EventArgs e)
        {
            if (!bFlyPop)
            {
                flyoutPanel1.ShowPopup();
                xtraTabControl2.SelectedTabPageIndex = 0;
                MemoSend.Text = "";
                if (string.IsNullOrWhiteSpace(btnMessage.ToolTip))
                    Qr_FastMessage("02");
                else
                    Qr_RcvMessage_MgNo("02", btnMessage.ToolTip);
                bFlyPop = true;
            }
            else
            {
                flyoutPanel1.HidePopup();
                bFlyPop = false;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Qr_BackMessage("01");
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Qr_NextMessage("01");
        }
        private void btn_SoNext_Click(object sender, EventArgs e)
        {
            Qr_NextMessage("02");
        }

        private void btn_SoBack_Click(object sender, EventArgs e)
        {
            Qr_BackMessage("02");
        }
        private void btnRcvClose_Click(object sender, EventArgs e)
        {
            flyoutPanel1.HidePopup();
            bFlyPop = false;
        }

        private void btn_SoSend_Click(object sender, EventArgs e)
        {

        }

        private void btn_SoClose_Click(object sender, EventArgs e)
        {

        }

        private void btn_SoRcv_Click(object sender, EventArgs e)
        {
            flyoutPanel3.HidePopup();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            bool chk = false;
            for (int i = 0; i < ViewMaster.RowCount; i++)
            {
                if (Convert.ToString(ViewMaster.GetRowCellValue(i, "Chk")) == "Y")
                {
                    chk = true;
                }
            }

            if (!chk)
            {
                XtraMessageBox.Show("사원을 선택해 주십시요");
                return;
            }

            if (MemoSend.Text.Trim().Length == 0)
            {
                XtraMessageBox.Show("메세지를 입력해 주십시요");
                return;
            }

            Qr_SendMessage("02");
            MemoSend.Text = "";
            for (int i = 0; i < ViewMaster.RowCount; i++)
            {
                ViewMaster.UnselectRow(i);
            }

            XtraMessageBox.Show("메세지 전송 완료");
        }


        private void btnSendClose_Click(object sender, EventArgs e)
        {
            flyoutPanel1.HidePopup();
            bFlyPop = false;
        }

        //전송
        private void Qr_SendMessage(string sType)
        {
            ret = new ReturnStruct();
            for (int i = 0; i < ViewMaster.RowCount; i++)
            {
                if (Convert.ToString(ViewMaster.GetRowCellValue(i, "Chk")) == "Y")
                {
                    SqlParam sp = new SqlParam("sp_regMessage");
                    sp.AddParam("Status", "1");
                     sp.AddParam("Rcv_MgNo", "");
                    sp.AddParam("Send_User_Code", GlobalValue.sUserID);
                    sp.AddParam("Rcv_User_Code", Convert.ToString(ViewMaster.GetRowCellValue(i, "User_Code")));
                    sp.AddParam("Send_Message", MemoSend.Text);
                    sp.AddParam("Type_Message", sType);
                    ret = DbHelp.Proc_Search(sp);
                }
            }
        }

        //수신
        private void Qr_RcvMessage(string sType)
        {
            ret = new ReturnStruct();
            SqlParam sp = new SqlParam("sp_regMessage");
            sp.AddParam("Status", "2");
            sp.AddParam("Rcv_MgNo", "");
            sp.AddParam("Send_User_Code", GlobalValue.sUserID);
            sp.AddParam("Rcv_User_Code","");
            sp.AddParam("Send_Message","");
            sp.AddParam("Type_Message", sType);
            ret = DbHelp.Proc_Search(sp);
            if (ret.ReturnDataSet == null)
                return;

            DataTable dt = DbHelp.Fill_Table(ret.ReturnDataSet.Tables[0]);
            if (dt != null)
            {
                if (dt.Rows.Count != 0)
                {
                    if (sType == "01")
                    {
                        flyoutPanel1.ShowPopup();
                        xtraTabControl2.SelectedTabPageIndex = 0;
                        MemoSend.Text = "";

                        bFlyPop = true;
                        sMgNoSo = Convert.ToString(dt.Rows[0]["Mg_No"]);
                        lblSendName.Text = Convert.ToString(dt.Rows[0]["User_Name"]);
                        lblRcvTime.Text = "받은시간 : " + Convert.ToString(dt.Rows[0]["Mg_Send_Date"]);
                        MemoRcv.Text = Convert.ToString(dt.Rows[0]["Mg_Message"]);
                        Qr_sp_Help_User();
                    }
                    else if (sType == "02")
                    {
                        flyoutPanel3.ShowPopup();
                        xtraTabControl4.SelectedTabPageIndex = 0;
                        sMgNoSo = Convert.ToString(dt.Rows[0]["Mg_No"]);
                        memo_SoRcv.Text = "";
                        memo_SoRcv.Text = "받은시간 : " + Convert.ToString(dt.Rows[0]["Mg_Send_Date"]);
                        memo_SoRcv.Text += Environment.NewLine + Environment.NewLine + Convert.ToString(dt.Rows[0]["Mg_Message"]);
                        Qr_sp_Help_User();
                    }

                    sForm_Name = dt.Rows[0]["Form_Name"].NullString();
                    sKey_No = dt.Rows[0]["Key_No"].NullString();
                    sMenuName = dt.Rows[0]["Menu_SName"].NullString();
                    btn_conf_Visible();
                }
            }
        }

        //지정된 메세지
        private void Qr_RcvMessage_MgNo(string sType, string sMg_No)
        {
            ret = new ReturnStruct();
            SqlParam sp = new SqlParam("sp_regMessage");
            sp.AddParam("Status", "6");
            sp.AddParam("Rcv_MgNo", sMg_No);
            sp.AddParam("Send_User_Code", GlobalValue.sUserID);
            sp.AddParam("Rcv_User_Code", "");
            sp.AddParam("Send_Message", "");
            sp.AddParam("Type_Message", sType);
            ret = DbHelp.Proc_Search(sp);
            if (ret.ReturnDataSet == null)
                return;

            DataTable dt = DbHelp.Fill_Table(ret.ReturnDataSet.Tables[0]);
            if (dt != null)
            {
                if (dt.Rows.Count != 0)
                {
                    if (dt.Rows.Count != 0)
                    {
                        flyoutPanel1.ShowPopup();
                        xtraTabControl2.SelectedTabPageIndex = 0;
                        MemoSend.Text = "";

                        bFlyPop = true;
                        sMgNoSo = Convert.ToString(dt.Rows[0]["Mg_No"]);
                        lblSendName.Text = Convert.ToString(dt.Rows[0]["User_Name"]);
                        lblRcvTime.Text = "받은시간 : " + Convert.ToString(dt.Rows[0]["Mg_Send_Date"]);
                        MemoRcv.Text = Convert.ToString(dt.Rows[0]["Mg_Message"]);
                        Qr_sp_Help_User();
                        sForm_Name = dt.Rows[0]["Form_Name"].NullString();
                        sKey_No = dt.Rows[0]["Key_No"].NullString();
                        sMenuName = dt.Rows[0]["Menu_SName"].NullString();
                        btn_conf_Visible();
                    }
                }
            }

            btnMessage.ToolTip = "";
        }

        //최신 메세지
        private void Qr_FastMessage(string sType)
        {
            ret = new ReturnStruct();
            SqlParam sp = new SqlParam("sp_regMessage");
            sp.AddParam("Status", "3");
            sp.AddParam("Rcv_MgNo", "");
            sp.AddParam("Send_User_Code", GlobalValue.sUserID);
            sp.AddParam("Rcv_User_Code", "");
            sp.AddParam("Send_Message", "");
            sp.AddParam("Type_Message", sType);
            ret = DbHelp.Proc_Search(sp);
            DataTable dt = DbHelp.Fill_Table(ret.ReturnDataSet.Tables[0]);
            if (dt != null)
            {
                if (dt.Rows.Count != 0)
                {
                    flyoutPanel1.ShowPopup();
                    xtraTabControl2.SelectedTabPageIndex = 0;
                    MemoSend.Text = "";

                    bFlyPop = true;
                    sMgNoSo = Convert.ToString(dt.Rows[0]["Mg_No"]);
                    lblSendName.Text = Convert.ToString(dt.Rows[0]["User_Name"]);
                    lblRcvTime.Text = "받은시간 : " + Convert.ToString(dt.Rows[0]["Mg_Send_Date"]);
                    MemoRcv.Text = Convert.ToString(dt.Rows[0]["Mg_Message"]);
                    Qr_sp_Help_User();
                    sForm_Name = dt.Rows[0]["Form_Name"].NullString();
                    sKey_No = dt.Rows[0]["Key_No"].NullString();
                    sMenuName = dt.Rows[0]["Menu_SName"].NullString();
                    btn_conf_Visible();
                }
            }
        }

        //이전 메세지
        private void Qr_BackMessage(string sType)
        {
            ret = new ReturnStruct();
            SqlParam sp = new SqlParam("sp_regMessage");
            sp.AddParam("Status", "4");
            //if (sType == "01")
            //{
            //    sp.AddParam("Rcv_MgNo", sMgNo);
            //}
            //else
            //{
            sp.AddParam("Rcv_MgNo", sMgNoSo);
            //}
            sp.AddParam("Send_User_Code", GlobalValue.sUserID);
            sp.AddParam("Rcv_User_Code", "");
            sp.AddParam("Send_Message", "");
            sp.AddParam("Type_Message", "02");//sType
            ret = DbHelp.Proc_Search(sp);
            DataTable dt = DbHelp.Fill_Table(ret.ReturnDataSet.Tables[0]);
            if (dt != null)
            {
                if (dt.Rows.Count != 0)
                {
                    if (sType == "01")
                    {
                        flyoutPanel1.ShowPopup();
                        xtraTabControl2.SelectedTabPageIndex = 0;
                        MemoSend.Text = "";

                        bFlyPop = true;
                        sMgNoSo = Convert.ToString(dt.Rows[0]["Mg_No"]);
                        lblSendName.Text = Convert.ToString(dt.Rows[0]["User_Name"]);
                        lblRcvTime.Text = "받은시간 : " + Convert.ToString(dt.Rows[0]["Mg_Send_Date"]);
                        MemoRcv.Text = Convert.ToString(dt.Rows[0]["Mg_Message"]);
                        Qr_sp_Help_User();
                    }
                    else if (sType == "02")
                    {
                        sMgNoSo = Convert.ToString(dt.Rows[0]["Mg_No"]);
                        memo_SoRcv.Text = "";
                        memo_SoRcv.Text = "받은시간 : " + Convert.ToString(dt.Rows[0]["Mg_Send_Date"]);
                        memo_SoRcv.Text += Environment.NewLine + Environment.NewLine + Convert.ToString(dt.Rows[0]["Mg_Message"]);
                        Qr_sp_Help_User();
                    }

                    sForm_Name = dt.Rows[0]["Form_Name"].NullString();
                    sKey_No = dt.Rows[0]["Key_No"].NullString();
                    sMenuName = dt.Rows[0]["Menu_SName"].NullString();
                    btn_conf_Visible();
                }
            }
        }

        //다음 메세지
        private void Qr_NextMessage(string sType)
        {
            ret = new ReturnStruct();
            SqlParam sp = new SqlParam("sp_regMessage");
            sp.AddParam("Status", "5");
            //if (sType == "01")
            //{
            //    sp.AddParam("Rcv_MgNo", sMgNo);
            //}
            //else
            //{
            sp.AddParam("Rcv_MgNo", sMgNoSo);
            //}
            
            sp.AddParam("Send_User_Code", GlobalValue.sUserID);
            sp.AddParam("Rcv_User_Code", "");
            sp.AddParam("Send_Message", "");
            sp.AddParam("Type_Message", "02"); //sType
            ret = DbHelp.Proc_Search(sp);
            DataTable dt = DbHelp.Fill_Table(ret.ReturnDataSet.Tables[0]);
            if (dt != null)
            {
                if (dt.Rows.Count != 0)
                {
                    if (sType == "01")
                    {
                        flyoutPanel1.ShowPopup();
                        xtraTabControl2.SelectedTabPageIndex = 0;
                        MemoSend.Text = "";

                        bFlyPop = true;
                        sMgNoSo = Convert.ToString(dt.Rows[0]["Mg_No"]);
                        lblSendName.Text = Convert.ToString(dt.Rows[0]["User_Name"]);
                        lblRcvTime.Text = "받은시간 : " + Convert.ToString(dt.Rows[0]["Mg_Send_Date"]);
                        MemoRcv.Text = Convert.ToString(dt.Rows[0]["Mg_Message"]);
                        Qr_sp_Help_User();
                    }
                    else if (sType == "02")
                    {
                        sMgNoSo = Convert.ToString(dt.Rows[0]["Mg_No"]);
                        memo_SoRcv.Text = "";
                        memo_SoRcv.Text = "받은시간 : " + Convert.ToString(dt.Rows[0]["Mg_Send_Date"]);
                        memo_SoRcv.Text += Environment.NewLine + Environment.NewLine + Convert.ToString(dt.Rows[0]["Mg_Message"]);
                        Qr_sp_Help_User();
                    }
                    sForm_Name = dt.Rows[0]["Form_Name"].NullString();
                    sKey_No = dt.Rows[0]["Key_No"].NullString();
                    sMenuName = dt.Rows[0]["Menu_SName"].NullString();
                    btn_conf_Visible();
                }
            }
        }
        #endregion

        //메인 스크린 띄우기
        private void Screen_MainForm()
        {
            Type type = Type.GetType("SERP.rptMainScreen");

            if (type != null)
            {
                UserControl form = (UserControl)Activator.CreateInstance(type, null, null);
                form.Dock = DockStyle.Fill;
                form.BackColor = Color.FromArgb(226, 238, 247);
                //form.AutoScaleMode = AutoScaleMode.Dpi;
                form.AutoScaleMode = AutoScaleMode.None;

                sbTabPageAdd(form, "공지/메세지".Replace("ㆍ", ""));

                Insert_Log_Detail("공지/메세지");
            }
            else
            {
                XtraMessageBox.Show("해당 메뉴 폼이 없습니다");
                return;
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

        // 하단 공지사항 제목 띄우기
        private void Notice_Show(object sender, EventArgs e)
        {
            string part_name = string.Empty;
            ReturnStruct temp_ret = new ReturnStruct();

            SqlParam sp = new SqlParam("sp_MainForm_Notice");
            sp.AddParam("User_Code", GlobalValue.sUserID);
            temp_ret = DbHelp.Proc_Search(sp);

            if (temp_ret.ReturnChk != 0)
            {
                return;
            }
            DataTable notice = DbHelp.Fill_Table(temp_ret.ReturnDataSet.Tables[0]);

            if (notice_count == notice.Rows.Count)
                notice_count = 0;

            if (notice.Rows.Count > 0)
            {
                if (notice.Rows[notice_count]["Notice_Part"].NullString() == "010")
                {
                    part_name = "(긴급) ";
                    txt_Notice.ForeColor = Color.Red;
                }
                else
                {
                    part_name = "";
                    txt_Notice.ForeColor = Color.Black;
                }

                txt_Notice.Text = part_name + notice.Rows[notice_count]["Notice_Title"].NullString() + "      (" + notice.Rows[notice_count]["Notice_STime"].NullString().Substring(0, 10) + " ~ " + notice.Rows[notice_count]["Notice_ETime"].NullString().Substring(0, 10) + ")";
                txt_Notice.ToolTip = notice.Rows[notice_count]["Notice_No"].NullString();

                notice_count++;
            }

            ////메신저 수신
            //Qr_RcvMessage("01");
            //Qr_RcvMessage("02");
        }

        private void Message_Rcv(object sender, EventArgs e)
        {
            //메신저 수신
            //Qr_RcvMessage("01");
            Qr_RcvMessage("02");
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

                if(ret.ReturnChk == 0)
                {
                    dt = ret.ReturnDataSet.Tables[0];
                    dt_sub = ret.ReturnDataSet.Tables[1];

                    if(dt.Rows.Count == 0)
                    {
                        XtraMessageBox.Show("작업자에 설정된 메뉴가 없습니다");
                        return;
                    }

                    for(int i = 0; i < dt.Rows.Count; i++)
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
        public static void Menu_Jump(string Jump_Form, string Menu_Name)
        {
            string sNAME = Jump_Form;

            if (!string.IsNullOrWhiteSpace(Jump_Form))
            {
                Type type = Type.GetType("SERP." + sNAME);

                if (type != null)
                {
                    UserControl form = (UserControl)Activator.CreateInstance(type, null, null);
                    form.Dock = DockStyle.Fill;
                    form.BackColor = Color.FromArgb(226, 238, 247);
                    form.AutoScaleMode = AutoScaleMode.None;

                    sbTabPageAdd(form, Menu_Name);
                }
                else
                {
                    XtraMessageBox.Show("해당 메뉴 폼이 없습니다");
                    return;
                }
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
                else if (sNAME != null & sNAME != "" && TreeLevel == 1)
                {
                    if (sNAME.IndexOf("Kiosk") > -1)
                    {
                        Type type = Type.GetType("SERP." + sNAME);
                        if (type != null)
                        {
                            XtraForm form = (XtraForm)Activator.CreateInstance(type, null, null);
                            form.Dock = DockStyle.Fill;
                            form.BackColor = Color.FromArgb(226, 238, 247);
                            //form.AutoScaleMode = AutoScaleMode.Dpi;
                            form.AutoScaleMode = AutoScaleMode.None;
                            form.WindowState = FormWindowState.Maximized;
                            form.Show();
                        }
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

        // 공지사항 더블 클릭 => 공지사항 내용 보여주기
        private void txt_Notice_DoubleClick(object sender, EventArgs e)
        {
            PopNoticeForm pop = new PopNoticeForm(txt_Notice.ToolTip);
            pop.StartPosition = FormStartPosition.CenterScreen;
            pop.ShowDialog();
        }

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

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            flyoutPanel3.ShowPopup();
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
                        if(time_message.Enabled)
                            time_message.Stop();
                    }
                    else
                    {
                        if(!time_message.Enabled)
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

        private void btn_conf_Visible()
        {
            if (string.IsNullOrWhiteSpace(sKey_No))
            {
                btn_Conf.Visible = false;
                btn_SoConf.Visible = false;
            }
            else
            {
                btn_Conf.Visible = true;
                btn_SoConf.Visible = true;
            }
        }

        private void btn_Confirm_Click(object sender, EventArgs e)
        {
            try
            {
                UserControl user_ctrl = (UserControl)xtraTabControl1.SelectedTabPage.Controls[0];

                PanelControl PK_Panel = user_ctrl.Controls.OfType<PanelControl>().OrderBy(x => x.Location.Y).ToArray()[1];
                TextEdit PK_Text = PK_Panel.Controls.OfType<TextEdit>().OrderBy(x => x.Location.X).ToArray()[0];

                if (string.IsNullOrWhiteSpace(PK_Text.Text))
                {
                    XtraMessageBox.Show("컨펌 등록 전에 저장을 진행해주시길 바랍니다.");
                    return;
                }

                Conf_Form Form = new Conf_Form(user_ctrl.Name, PK_Text.Text, GlobalValue.sUserID);
                Form.StartPosition = FormStartPosition.CenterScreen;
                Form.ShowDialog();
            }
            catch (Exception)
            {

            }
        }

        private void btn_Conf_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(sKey_No))
                return;

            int iExist = -1;
            for (int ix = 0; ix <= MainTab.Controls.Count - 1; ix++)
            {
#if DEBUG
                if (MainTab.Controls[ix].Text == sMenuName.Substring(sMenuName.IndexOf("ː") + 1) + "(" + sForm_Name + ")")
#else
                    if (MainForm.MainTab.Controls[ix].Text == sMenuName.Substring(sMenuName.IndexOf("ː") + 1))
#endif
                {
                    iExist = ix;

                    break;
                }

            }
            if (iExist > 0)
            {
                MainTab.SelectedTabPageIndex = iExist;
                MainTab.TabPages.Remove(MainTab.SelectedTabPage);
            }

            if (bFlyPop)
            {
                flyoutPanel1.HidePopup();
                bFlyPop = false;
            }
            else
            {
                flyoutPanel3.HidePopup();
            }

            Menu_Jump(sForm_Name, sMenuName);
            if (sForm_Name == "regOrderTr")
                regOrderTr.OrderTr_Select(sKey_No);
            else if (sForm_Name == "regSalePlanTr")
                regSalePlanTr.PlanTr_Select(sKey_No);
            else if (sForm_Name == "regQuotTr")
                regQuotTr.QuotTr_Select(sKey_No);
        }

        public static void Check_File_Exists()
        {
            try
            {
                FileIF.Set_URL();
                User_Ctrl = (UserControl)MainTab.SelectedTabPage.Controls[0];
                PK_Panel = User_Ctrl.Controls.OfType<PanelControl>().OrderBy(x => x.Location.Y).ToArray()[1];
                PK_Text = PK_Panel.Controls.OfType<TextEdit>().OrderBy(x => x.Location.X).ToArray()[0];

                string Path = Configurations.GetConfig("DBConnstring").Split('=')[3];
                Path = Path.Substring(0, Path.IndexOf(";"));
                Path = FileIF.FTP_URL + "/" + Path + "/" + User_Ctrl.Name + "/" + PK_Text.Text;

                DataTable Files = FileIF.Get_File_List(Path);

                if (Files == null || Files.Rows.Count == 0)
                {
                    File_Button.Appearance.BackColor = Color.White;
                    return;
                }

                if (Files.Rows.Count > 0)
                {
                    if (Files.Select("File_Size > 0").Count() > 0)
                    {
                        File_Button.ResetBackColor();
                        File_Button.Appearance.BackColor = Color.ForestGreen;
                    }
                    else
                    {
                        File_Button.Appearance.BackColor = Color.White;
                    }
                }
            }
            catch (Exception)
            {
                File_Button.Appearance.BackColor = Color.White;
            }
        }

        public static void Save_Conf()
        {
            try
            {
                User_Ctrl = (UserControl)MainTab.SelectedTabPage.Controls[0];
                PK_Panel = User_Ctrl.Controls.OfType<PanelControl>().OrderBy(x => x.Location.Y).ToArray()[1];
                PK_Text = PK_Panel.Controls.OfType<TextEdit>().OrderBy(x => x.Location.X).ToArray()[0];

                if (User_Ctrl.Name == Conf_Form.Form_NM && PK_Text.Text == Conf_Form.PK)
                {

                }
            }
            catch (Exception)
            {

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

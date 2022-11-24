using DevExpress.Utils.Svg;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Windows.Forms;

namespace KIOSK_EMAX
{
    public partial class MainMenu : DevExpress.XtraEditors.XtraForm
    {
        ReturnStruct ret = new ReturnStruct();

        public MainMenu()
        {
            InitializeComponent();
        }

        private void SetMenu()
        {
            try
            {
                SqlParam sp = new SqlParam("sp_Pop_Menu");

                ret = DbHelp.Proc_Search(sp);

                if(ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                DataTable dt = ret.ReturnDataSet.Tables[0];

                if(dt.Rows.Count > 0)
                {
                    for(int i = 0; i < dt.Rows.Count; i++)
                    {
                        RibbonPageGroup menu_ribbonPG = new RibbonPageGroup("                 ");
                        menu_ribbonPG.ItemsLayout = RibbonPageGroupItemsLayout.OneRow;
                        menu_ribbonPG.AllowTextClipping = false;
                        menu_ribbonPG.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
                        ribbonPage1.Groups.Add(menu_ribbonPG);

                        BarButtonItem menu_btn = new BarButtonItem();
                        menu_btn.Caption = dt.Rows[i]["Menu_SName"].NullString();
                        menu_btn.Name = dt.Rows[i]["Form_Name"].NullString();
                        menu_btn.RibbonStyle = RibbonItemStyles.Large;
                        menu_btn.ItemAppearance.Normal.Font = new Font("맑은 고딕", 15, FontStyle.Bold);

                        ResourceManager rm = Properties.Resources.ResourceManager;
                        menu_btn.ImageOptions.SvgImage = (SvgImage)rm.GetObject(dt.Rows[i]["Btn_Img"].ToString());

                        menu_btn.ItemClick += new ItemClickEventHandler(Menu_Item_Click);
                        menu_ribbonPG.ItemLinks.Add(menu_btn);
                    }
                }

                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                string nameSpace = string.Format(assembly.EntryPoint.DeclaringType.Namespace + ".");

                XtraForm openForm = assembly.CreateInstance(nameSpace + dt.Rows[0]["Form_Name"].NullString(), true) as XtraForm;

                Label_Title.Text = dt.Rows[0]["Menu_SName"].NullString();

                openForm.Text = dt.Rows[0]["Menu_SName"].NullString();
                openForm.MdiParent = this;
                openForm.Dock = DockStyle.Fill;
                openForm.FormBorderStyle = FormBorderStyle.None;
                openForm.Show();
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }             

        private void MainMenu_Load(object sender, EventArgs e)
        {
            ribbon_Menu.DrawGroupCaptions = DevExpress.Utils.DefaultBoolean.False;
            ribbon_Menu.AllowMdiChildButtons = false;

            this.Text = "현장 프로그램";

            this.IsMdiContainer = true;

            SetMenu();

            try
            {
                RibbonPageGroup menu_ribbonPG = new RibbonPageGroup("                 ");
                menu_ribbonPG.ItemsLayout = RibbonPageGroupItemsLayout.OneRow;
                menu_ribbonPG.AllowTextClipping = false;
                menu_ribbonPG.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
                ribbonPage1.Groups.Add(menu_ribbonPG);

                BarButtonItem menu_btn = new BarButtonItem();
                menu_btn.Caption = "닫기";
                menu_btn.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
                menu_btn.ItemAppearance.Normal.Font = new Font("맑은 고딕", 15, FontStyle.Bold);

                menu_btn.ImageOptions.SvgImage = Properties.Resources.cancel;

                menu_btn.ItemClick += new ItemClickEventHandler(Menu_Item_Click);

                menu_ribbonPG.ItemLinks.Add(menu_btn);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        private void Menu_Item_Click(object sender, ItemClickEventArgs e)
        {
            if (e.Item.Caption == "닫기")
            {
                try
                {
                    if (this.ActiveMdiChild == null)
                    {
                        this.Close();
                    }
                    else
                    {
                        this.ActiveMdiChild.Close();

                        if (this.ActiveMdiChild != null)
                            Label_Title.Text = this.ActiveMdiChild.Text;
                        else
                            Label_Title.Text = "";
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message);
                    return;
                }

                return;
            }

            try
            {
                foreach (XtraForm childForm in this.MdiChildren)
                {
                    if ((childForm as XtraForm).Text == e.Item.Caption)
                    {
                        Label_Title.Text = e.Item.Caption;
                        childForm.Activate();
                        return;
                    }
                }

                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                string nameSpace = string.Format(assembly.EntryPoint.DeclaringType.Namespace + ".");

                XtraForm openForm = assembly.CreateInstance(nameSpace + e.Item.Name.ToString(), true) as XtraForm;

                Label_Title.Text = e.Item.Caption;

                openForm.Text = e.Item.Caption;
                openForm.MdiParent = this;
                openForm.Dock = DockStyle.Fill;
                openForm.FormBorderStyle = FormBorderStyle.None;
                openForm.Show();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        //환결설정(IP) : F1 단축키 사용
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.F1)
            {
                Change_Config config = new Change_Config();
                if(config.ShowDialog() == DialogResult.OK)
                {
                    XtraMessageBox.Show("설정이 변경되어 프로그램을 재실행합니다");

                    string sPath = Application.ExecutablePath;

                    Process.Start(sPath);

                    Application.Exit();
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}

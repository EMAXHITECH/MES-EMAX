namespace KIOSK_EMAX
{
    partial class MainMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            this.ribbon_Menu = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.skinDropDownButtonItem1 = new DevExpress.XtraBars.SkinDropDownButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.Panel_Title = new DevExpress.XtraEditors.PanelControl();
            this.Label_Title = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon_Menu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Panel_Title)).BeginInit();
            this.Panel_Title.SuspendLayout();
            this.SuspendLayout();
            // 
            // ribbon_Menu
            // 
            this.ribbon_Menu.ColorScheme = DevExpress.XtraBars.Ribbon.RibbonControlColorScheme.Blue;
            this.ribbon_Menu.ExpandCollapseItem.Id = 0;
            this.ribbon_Menu.Font = new System.Drawing.Font("맑은 고딕", 25F, System.Drawing.FontStyle.Bold);
            this.ribbon_Menu.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon_Menu.ExpandCollapseItem,
            this.ribbon_Menu.SearchEditItem,
            this.barButtonItem1,
            this.barButtonItem2,
            this.skinDropDownButtonItem1});
            this.ribbon_Menu.Location = new System.Drawing.Point(0, 0);
            this.ribbon_Menu.Margin = new System.Windows.Forms.Padding(5);
            this.ribbon_Menu.MaxItemId = 5;
            this.ribbon_Menu.Name = "ribbon_Menu";
            this.ribbon_Menu.OptionsMenuMinWidth = 519;
            this.ribbon_Menu.OptionsStubGlyphs.Font = new System.Drawing.Font("맑은 고딕", 25F, System.Drawing.FontStyle.Bold);
            this.ribbon_Menu.OptionsStubGlyphs.UseFont = true;
            this.ribbon_Menu.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbon_Menu.PopupShowMode = DevExpress.XtraBars.PopupShowMode.Classic;
            this.ribbon_Menu.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbon_Menu.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbon_Menu.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            this.ribbon_Menu.ShowToolbarCustomizeItem = false;
            this.ribbon_Menu.Size = new System.Drawing.Size(1280, 141);
            this.ribbon_Menu.Toolbar.ShowCustomizeItem = false;
            this.ribbon_Menu.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "barButtonItem1";
            this.barButtonItem1.Id = 1;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "barButtonItem2";
            this.barButtonItem2.Id = 2;
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // skinDropDownButtonItem1
            // 
            this.skinDropDownButtonItem1.Id = 4;
            this.skinDropDownButtonItem1.Name = "skinDropDownButtonItem1";
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "ribbonPage1";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.skinDropDownButtonItem1);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "ribbonPageGroup1";
            // 
            // Panel_Title
            // 
            this.Panel_Title.ContentImage = global::KIOSK_EMAX.Properties.Resources.work1;
            this.Panel_Title.Controls.Add(this.Label_Title);
            this.Panel_Title.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_Title.Location = new System.Drawing.Point(0, 141);
            this.Panel_Title.Margin = new System.Windows.Forms.Padding(5);
            this.Panel_Title.Name = "Panel_Title";
            this.Panel_Title.Size = new System.Drawing.Size(1280, 73);
            this.Panel_Title.TabIndex = 1;
            // 
            // Label_Title
            // 
            this.Label_Title.Appearance.Font = new System.Drawing.Font("맑은 고딕", 23F, System.Drawing.FontStyle.Bold);
            this.Label_Title.Appearance.ForeColor = System.Drawing.Color.White;
            this.Label_Title.Appearance.Options.UseFont = true;
            this.Label_Title.Appearance.Options.UseForeColor = true;
            this.Label_Title.Location = new System.Drawing.Point(28, 14);
            this.Label_Title.Name = "Label_Title";
            this.Label_Title.Size = new System.Drawing.Size(93, 42);
            this.Label_Title.TabIndex = 0;
            this.Label_Title.Text = "메뉴명";
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 994);
            this.Controls.Add(this.Panel_Title);
            this.Controls.Add(this.ribbon_Menu);
            this.IconOptions.Image = ((System.Drawing.Image)(resources.GetObject("MainMenu.IconOptions.Image")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximumSize = new System.Drawing.Size(1280, 1024);
            this.Name = "MainMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainMenu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon_Menu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Panel_Title)).EndInit();
            this.Panel_Title.ResumeLayout(false);
            this.Panel_Title.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon_Menu;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.SkinDropDownButtonItem skinDropDownButtonItem1;
        private DevExpress.XtraEditors.PanelControl Panel_Title;
        private DevExpress.XtraEditors.LabelControl Label_Title;
    }
}


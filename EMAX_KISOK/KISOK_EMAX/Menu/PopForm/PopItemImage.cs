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
using DevExpress.XtraEditors.Controls;
using CefSharp;
using CefSharp.WinForms;

namespace KISOK_EMAX
{
    public partial class PopItemImage : DevExpress.XtraEditors.XtraForm
    {
        public string sItem_Code = "";

        private ChromiumWebBrowser item_Image = null;

        public PopItemImage()
        {
            InitializeComponent();
        }

        private void PopItemImage_Load(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(sItem_Code))
            {
                this.Close();
                return;
            }

            string sUrl = "https://plm.hangilsv.com/MES/ItemsDoc?searchValue=";

            sUrl += sItem_Code;

            try
            {
                item_Image = new ChromiumWebBrowser(sUrl);
                item_Image.BrowserSettings.AcceptLanguageList = "ko-KR";
                this.Controls.Add(item_Image);
                item_Image.Dock = DockStyle.Fill;
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }

        }
    }
}
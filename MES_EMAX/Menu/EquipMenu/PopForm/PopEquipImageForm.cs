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
using System.IO;

namespace MES
{
    public partial class PopEquipImageForm : BaseForm
    {
        byte[] sImage = null;

        public PopEquipImageForm(byte[] sImage)
        {
            InitializeComponent();
            this.sImage = sImage;
        }

        private void PopEquipImageForm_Load(object sender, EventArgs e)
        {
            MemoryStream ms = new MemoryStream((byte[])sImage);
            pictureEdit1.Image = Image.FromStream(ms);
        }
    }
}

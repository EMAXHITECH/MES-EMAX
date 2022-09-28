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
    public partial class Exit : DevExpress.XtraEditors.XtraForm
    {
        public delegate void TextEventHandler(bool sendstring);
        //이벤트 생성
        public event TextEventHandler FormSendEvent;

        bool Exit_YN = false;
        public Exit()
        {
            InitializeComponent();
        }

        public string dd(string aa)
        {
            //this.ShowDialog();

            

            return "ad";
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            try
            {
                Application.Exit();
 
            }
            catch (Exception ex)
            {

                
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            Exit_YN = true;

            FormSendEvent(Exit_YN);
        }

        private void btn_Cancel_Click_1(object sender, EventArgs e)
        {
            Exit_YN = false;

            FormSendEvent(Exit_YN);

            this.Close();

        }

    }
}
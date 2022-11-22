using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PLC_EMAX
{
    public class clsCommon
    {
        public void ShowWait(Form showForm, string sCaption = "", string sDescription = "데이터 처리중 입니다. \n잠시만 기다려주세요.")
        {
            SplashScreenManager.CloseForm(false);

            SplashScreenManager.ShowForm(showForm, typeof(BaseWaitForm), true, true, false);
            SplashScreenManager.Default.SetWaitFormCaption(sCaption);
            SplashScreenManager.Default.SetWaitFormDescription(sDescription);
            //Thread.Sleep(500);
        }

        public void CloseWait()
        {
            SplashScreenManager.CloseForm(false);
        }
    }
}

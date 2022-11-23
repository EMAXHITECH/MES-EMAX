using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EMAX_Monitoring
{
   public class SimpleButtonEx : SimpleButton
    {
        public enum Button_GB
        {
            Search = 0,
            Add = 1,
            Delete = 2,
            Save = 3,
            Excel = 4,
            Print = 5,
            Exit = 6,
            Copy = 7
        }

        public clsCommon clsWait = new clsCommon();

        public Button_GB button_GB { get; set; }
        //업데이트 정보 표시
        public string sUpdate { get; set; } = "N";

        //사용 완료 여부 확인(Y 일경우 완료 알림 창 표시)
        public string sCHK { get; set; } = "N";

        //조회시 사용
        public string sSearch { get; set; } = "Y";

        public DialogResult Result_Update { get; set; }

        protected override void OnCreateControl()
        {
            this.Appearance.BackColor = Color.White;
            this.Size = new Size(74, 34);

            if(button_GB == Button_GB.Search)
            {
                this.Text = "조회";
                this.ImageOptions.SvgImage = Properties.Resources.marqueezoom;
            }
            else if (button_GB == Button_GB.Add)
            {
                this.Text = "추가";
                this.ImageOptions.SvgImage = Properties.Resources.actions_addcircled;
            }
            else if (button_GB == Button_GB.Delete)
            {
                this.Text = "삭제";
                this.ImageOptions.SvgImage = Properties.Resources.actions_deletecircled;
            }
            else if (button_GB == Button_GB.Save)
            {
                this.Text = "저장";
                this.ImageOptions.SvgImage = Properties.Resources.saveas;
            }
            else if (button_GB == Button_GB.Excel)
            {
                this.Text = "엑셀";
                this.ImageOptions.SvgImage = Properties.Resources.exporttoxls;
            }
            else if (button_GB == Button_GB.Print)
            {
                this.Text = "출력";
                this.ImageOptions.SvgImage = Properties.Resources.print;
            }
            else if (button_GB == Button_GB.Exit)
            {
                this.Text = "닫기";
                this.ImageOptions.SvgImage = Properties.Resources.del;
            }
            else if(button_GB == Button_GB.Copy)
            {
                this.Text = "복사";
                this.ImageOptions.SvgImage = Properties.Resources.copy;
            }
        }

        protected override void OnClick(EventArgs e)
        {
            switch (button_GB)
            {
                case Button_GB.Delete:
                    if (XtraMessageBox.Show("삭제하시겠습니까?", "삭제", MessageBoxButtons.YesNo) == DialogResult.No)
                        return;
                    break;
                case Button_GB.Save:
                    if (sUpdate == "N")
                    {
                        if (XtraMessageBox.Show("저장하시겠습니까?", "저장", MessageBoxButtons.YesNo) == DialogResult.No)
                            return;
                    }
                    sUpdate = "N";

                    break;
                case Button_GB.Exit:
                    if(sUpdate == "Y")
                    {
                        Result_Update = XtraMessageBox.Show("수정한 정보가 있습니다. 저장하시겠습니까?", "닫기(저장)", MessageBoxButtons.YesNoCancel);

                        if (Result_Update == DialogResult.Cancel)
                            return;
                    }
                    else
                    {
                        if (XtraMessageBox.Show("창을 닫으시겠습니까?", "닫기", MessageBoxButtons.YesNo) == DialogResult.No)
                            return;
                    }
                    break;
                case Button_GB.Print:
                    if (XtraMessageBox.Show("출력하시겠습니까?", "출력", MessageBoxButtons.YesNo) == DialogResult.No)
                        return;
                    break;
                case Button_GB.Excel:
                    if (XtraMessageBox.Show("엑셀 저장 하시겠습니까?", "엑셀", MessageBoxButtons.YesNo) == DialogResult.No)
                        return;
                    break;
                case Button_GB.Add:
                    if (sUpdate == "Y")
                    {
                        Result_Update = XtraMessageBox.Show("수정한 정보가 있습니다. 저장하시겠습니까?", "추가(저장)", MessageBoxButtons.YesNoCancel);

                        if (Result_Update == DialogResult.Cancel)
                            return;
                    }
                    else
                    {
                        if (XtraMessageBox.Show("추가하시겠습니까?", "추가", MessageBoxButtons.YesNo) == DialogResult.No)
                            return;
                    }
                    break;
                case Button_GB.Copy:
                    if (sUpdate == "Y")
                    {
                        Result_Update = XtraMessageBox.Show("수정한 정보가 있습니다. 저장하시겠습니까?", "복사(저장)", MessageBoxButtons.YesNoCancel);

                        if (Result_Update == DialogResult.Cancel)
                            return;
                    }
                    else
                    {
                        if (XtraMessageBox.Show("복사하시겠습니까?", "복사", MessageBoxButtons.YesNo) == DialogResult.No)
                            return;
                    }
                    
                    break;
                case Button_GB.Search:
                    clsWait.ShowWait(this.FindForm());
                    break;
            }

            base.OnClick(e);

            if (sCHK == "Y")
            {
                switch (button_GB)
                {
                    case Button_GB.Delete:
                        XtraMessageBox.Show("삭제되었습니다");
                        sCHK = "N";
                        break;
                    case Button_GB.Save:
                        XtraMessageBox.Show("저장되었습니다");
                        sCHK = "N";
                        break;
                    case Button_GB.Copy:
                        XtraMessageBox.Show("복사되었습니다");
                        sCHK = "N";
                        break;
                }
            }

            if (button_GB == Button_GB.Search)
            {
                clsWait.CloseWait();
            }

            //if (button_GB == Button_GB.Search || button_GB == Button_GB.Add || button_GB == Button_GB.Delete)
            //    MainForm.Check_File_Exists();
        }
    }
}

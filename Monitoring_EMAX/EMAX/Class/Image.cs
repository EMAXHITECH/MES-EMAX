using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EMAX_Monitoring
{
    class Image_Help
    {
        public static void Image_Right_Click(object sender, MouseEventArgs e)
        {
            PictureEdit ctl = (PictureEdit)sender;
            //PictureBox ctl = (PictureBox)sender;

            Point pos = new Point(e.X, e.Y);

            ContextMenu menu = new ContextMenu();
            MenuItem m1 = new MenuItem();
            MenuItem m2 = new MenuItem();

            m1.Text = "이미지 추가 / 변경";
            m2.Text = "이미지 삭제";

            // PictureBox 에 이미지 삽입
            m1.Click += (senders, es) => { EventAddImage(ctl); };
            // PictureBox 의 이미지를 초기 이미지로 복구
            m2.Click += (senders, es) => { ((PictureEdit)ctl).Image = ctl.Image = null; }; //.InitialImage

            menu.MenuItems.Add(m1);
            menu.MenuItems.Add(m2);

            menu.Show(ctl, pos);
        }

        // 이미지 파일 경로 반환
        public static string GetImageFileName()
        {
            OpenFileDialog frm = new OpenFileDialog();
            frm.Filter = "Image files (*.jpg,*.gif,*.png)|*.jpg;*.gif;*.png";
            string filePath = null;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                filePath = frm.FileName;
            }

            return filePath;
        }

        // PictureBox에 이미지 삽입
        public static void EventAddImage(PictureEdit ctl)
        {
            string name = GetImageFileName();

            if (!string.IsNullOrWhiteSpace(name))
            {
                Image img = Image.FromFile(name);
                ctl.Image = img;
                ctl.ToolTip = name;
            }
        }

        // 이미지 바이트 배열 형태로 반환 => 이미지 형태 저장할 때 사용
        public static byte[] ImageToByte(Image img)
        {
            if (img != null)
            {
                var stream = new MRUMemoryStream();
                img.Save(stream, ImageFormat.Gif);
                byte[] imgbyte = stream.ToArray();
                return imgbyte;
            }
            else return null;
        }
    }
}

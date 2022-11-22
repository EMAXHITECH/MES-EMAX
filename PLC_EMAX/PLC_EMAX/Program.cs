using System;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using DevExpress.XtraEditors;
using DevExpress.Utils.Svg;
using System.Drawing;
using System.Drawing.Text;
using System.Threading;

namespace PLC_EMAX
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        /// 

        static PrivateFontCollection privateFonts = new PrivateFontCollection();

        [STAThread]
        static void Main()
        {
            bool Process_Chk = Font_Copy_Paste();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ////DevExpress 보너스 스킨 등록(활성화) 
            BonusSkins.Register();
            OfficeSkins.Register();
            SkinManager.EnableFormSkins();
            SkinManager.EnableMdiFormSkins();
            WindowsFormsSettings.ForceAPIPaint();
            WindowsFormsSettings.SetDPIAware();
            WindowsFormsSettings.EnableFormSkins();
            WindowsFormsSettings.DefaultLookAndFeel.SetSkinStyle("The Bezier");
            SetSkinPlatte();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font(GetBasicFont(), FontStyle.Regular);
            //WindowsFormsSettings.DefaultFont = new System.Drawing.Font(GetBasicFont(), FontStyle.Regular);
            //WindowsFormsSettings.DefaultFont = new Font(GetBasicFont(), FontStyle.Regular);
            WindowsFormsSettings.AllowAutoScale = DevExpress.Utils.DefaultBoolean.Default;
            WindowsFormsSettings.CustomizationFormSnapMode = DevExpress.Utils.Controls.SnapMode.OwnerControl;
            //WindowsFormsSettings.ScrollUIMode = ScrollUIMode.Touch;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (Process_Chk == true)
            {
                Application.Restart();
            }
            else
            {
                Application.Run(new PLC());
            }
            //Application.Run(new Cal());
            //Application.Run(new MainForm());
        }
        static float GetDefalutSize()
        {
            return 9.75F;
        }
        static void SetSkinPlatte()
        {
            SvgPalette svgPalette = new SvgPalette();
            svgPalette.Colors.Add(new SvgColor("Paint", Color.FromArgb(226, 238, 247)));
            svgPalette.Colors.Add(new SvgColor("Paint High", Color.FromArgb(255, 255, 255)));
            svgPalette.Colors.Add(new SvgColor("Paint Shadow", Color.FromArgb(222, 222, 222)));
            svgPalette.Colors.Add(new SvgColor("Brush", Color.FromArgb(80, 80, 80)));
            svgPalette.Colors.Add(new SvgColor("Brush Light", Color.FromArgb(150, 150, 150)));
            svgPalette.Colors.Add(new SvgColor("Brush High", Color.FromArgb(80, 80, 80)));
            svgPalette.Colors.Add(new SvgColor("Brush Major", Color.FromArgb(186, 197, 221)));
            svgPalette.Colors.Add(new SvgColor("Brush Minor", Color.FromArgb(210, 210, 210)));
            svgPalette.Colors.Add(new SvgColor("Accent Paint", Color.FromArgb(23, 107, 209)));
            svgPalette.Colors.Add(new SvgColor("Accent Paint Light", Color.FromArgb(191, 224, 255)));
            svgPalette.Colors.Add(new SvgColor("Accent Brush", Color.FromArgb(255, 255, 255)));
            svgPalette.Colors.Add(new SvgColor("Accent Brush Light", Color.FromArgb(81, 148, 224)));
            svgPalette.Colors.Add(new SvgColor("Key Paint", Color.FromArgb(245, 249, 252)));
            svgPalette.Colors.Add(new SvgColor("Key Brush", Color.FromArgb(255,255,255)));
            svgPalette.Colors.Add(new SvgColor("Key Brush Light", Color.FromArgb(150, 150, 150)));
            svgPalette.Colors.Add(new SvgColor("Red", Color.FromArgb(226, 54, 66)));
            svgPalette.Colors.Add(new SvgColor("Green", Color.FromArgb(60, 146, 92)));
            svgPalette.Colors.Add(new SvgColor("Blue", Color.FromArgb(58, 116, 194)));
            svgPalette.Colors.Add(new SvgColor("Yellow", Color.FromArgb(252, 169, 10)));
            svgPalette.Colors.Add(new SvgColor("Black", Color.FromArgb(122, 122, 122)));
            svgPalette.Colors.Add(new SvgColor("Gray", Color.FromArgb(190, 190, 190)));
            svgPalette.Colors.Add(new SvgColor("White", Color.FromArgb(255, 255, 255)));

            var commonSkin = CommonSkins.GetSkin(UserLookAndFeel.Default);
            commonSkin.SvgPalettes[Skin.DefaultSkinPaletteName].CustomPalette = svgPalette;
            commonSkin.CustomSvgPalettes.Add(new SvgPaletteKey(commonSkin.CustomSvgPalettes.Count, "Custom Palette"), svgPalette);

            SkinElement element = SkinManager.GetSkinElement(SkinProductId.Grid, DevExpress.LookAndFeel.UserLookAndFeel.Default, "Border");
            element.Border.All = Color.FromArgb(186, 197, 221);
            LookAndFeelHelper.ForceDefaultLookAndFeelChanged();
        }

        public static Font GetBasicFont()
        {
            PrivateFontCollection privateFonts = new PrivateFontCollection();
            privateFonts.AddFontFile("NanumBarunGothic.ttf");
            Font font9 = new Font(privateFonts.Families[0], 10F);
            return font9;
        }
        //최초실행시 나눔바른고딕 폰트 없을경우 C\Windows\Font -> 여기에 설치
        // 2020.06.30 전재휘
        static bool Font_Copy_Paste()
        {
            bool Chk = true;
            InstalledFontCollection collection = new InstalledFontCollection();

            try
            {
                foreach (FontFamily fontFamily in collection.Families)
                {
                    if (fontFamily.Name.IndexOf("나눔바른고딕") > -1)
                    {
                        Chk = false;
                        break;
                    }
                }

                if (Chk == true)
                {
                    string StartPath = @Application.StartupPath + @"\Font";
                    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                    proc.EnableRaisingEvents = false;
                    proc.StartInfo.FileName = StartPath + @"\Font_Install.exe";
                    proc.Start();
                }

                return Chk;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return Chk;
            }
        
            //}
        }
    }
}

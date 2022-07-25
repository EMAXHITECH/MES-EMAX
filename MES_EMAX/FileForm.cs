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
using System.IO;
using System.Net;

namespace MES
{
    public partial class FileForm : DevExpress.XtraEditors.XtraForm
    {
        public static string PK;
        public static string Base_URL;
        public static string Form_Directory;
        public static string Detail_Directory;
        public static GridControlEx Main_Grid;

        public FileForm(string Form_Name, string Form_PK)
        {
            InitializeComponent();

            Main_Grid = Grid_File;

            PK = Form_PK;
            Form_Directory = Form_Name;
            Detail_Directory = Form_Directory + "/" + PK;
        }

        private void FileForm_Load(object sender, EventArgs e)
        {
            FileIF.Set_URL();

            string DB = Configurations.GetConfig("DBConnstring").Split('=')[3];
            DB = DB.Substring(0, DB.IndexOf(";"));
            Base_URL = FileIF.FTP_URL + "/" + DB + "/";

            Detail_Directory = Base_URL + Detail_Directory;
            Form_Directory = Base_URL + Form_Directory;


            Grid_Set();
            Search_Data();
        }

        private void Grid_Set()
        {
            DbHelp.GridSet(Grid_File, View_File, "File_Name", "파일명", "300", false, false, true, true);
            DbHelp.GridSet(Grid_File, View_File, "File_Size", "파일크기 (Byte)", "130", false, false, true, true);
            DbHelp.GridSet(Grid_File, View_File, "Upload_Date", "업로드 일자", "130", false, false, true, true);

            DbHelp.GridColumn_NumSet(View_File, "File_Size", ForMat.SetDecimal(this.Name, "Qty1"));

            SqlParam sp = new SqlParam("sp_File_Transfer");
            ReturnStruct ret = DbHelp.Proc_Search(sp);

            if (ret.ReturnChk != 0)
            {
                XtraMessageBox.Show(ret.ReturnMessage);
                return;
            }

            Grid_File.DataSource = ret.ReturnDataSet.Tables[0];
            Grid_File.AddRowYN = false;
            Grid_File.PopMenuChk = false;
            Grid_File.MouseWheelChk = false;
        }

        public static void Search_Data()
        {
            DataTable Files = FileIF.Get_File_List(Detail_Directory);

            if (Files == null)
                return;

            Main_Grid.DataSource = Files;
        }

        private void btn_Upload_Click(object sender, EventArgs e)
        {
            XtraOpenFileDialog FileDialog = new XtraOpenFileDialog();
            FileDialog.StartPosition = FormStartPosition.CenterScreen;
            FileDialog.InitialDirectory = @"C:\";

            if (FileDialog.ShowDialog() == DialogResult.OK)
            {
                string File_Name = Path.GetFileName(FileDialog.FileName);

                FileIF.FTP_Directory(Base_URL);
                FileIF.FTP_Directory(Form_Directory);
                FileIF.FTP_Directory(Detail_Directory);

                FileIF.FTP_Upload_File(FileDialog.FileName, Detail_Directory + "/" + File_Name);    // FTP 경로 + Form_Name + PK 번호 + 파일명
            }

            Search_Data();
        }

        private void btn_Download_Click(object sender, EventArgs e)
        {
            XtraFolderBrowserDialog Browser = new XtraFolderBrowserDialog();
            Browser.StartPosition = FormStartPosition.CenterScreen;
            if (Browser.ShowDialog() == DialogResult.OK)
            {
                bool Auto_Open = (View_File.GetSelectedRows().Count() == 1) ? true : false;

                foreach (int Index in View_File.GetSelectedRows())
                {
                    string File_Name = View_File.GetRowCellValue(Index, "File_Name").ToString();

                    FileIF.FTP_Download_File(Detail_Directory + "/" + File_Name, Browser.SelectedPath + "/" + File_Name, Auto_Open);
                }

                XtraMessageBox.Show("선택한 파일이 다운로드 되었습니다.");
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            foreach (int Index in View_File.GetSelectedRows())
            {
                string File_Name = View_File.GetRowCellValue(Index, "File_Name").ToString();

                FileIF.FTP_Delete_File(Detail_Directory + "/" + File_Name);
            }

            Search_Data();
            XtraMessageBox.Show("선택한 파일이 삭제 되었습니다.");
        }
    }
}
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EMAX_Monitoring
{
    public class FileIF
    {
        private static string sFileName = "";

        public static string ID { get; set; }
        public static string PW { get; set; }

        public static string FTP_URL { get; set; }

        public static void Excel_Down(GridControlEx gridControl, string sFormNM, int File_No = 0)
        {
            try
            {
                if (gridControl.MainView.RowCount < 0)
                    return;

                File_Open(sFormNM, File_No);

                if (sFileName != "" && sFileName != null)
                {
                    string sFileExtension = new FileInfo(sFileName).Extension;

                    switch (sFileExtension)
                    {
                        case ".xls":
                            XlsExportOptionsEx fileXls = new XlsExportOptionsEx();
                            fileXls.ExportMode = XlsExportMode.SingleFile;
                            fileXls.ExportType = DevExpress.Export.ExportType.DataAware;
                            gridControl.ExportToXls(sFileName, fileXls);
                            break;

                        case ".xlsx":
                            XlsxExportOptionsEx fileXlsx = new XlsxExportOptionsEx();
                            fileXlsx.ExportMode = XlsxExportMode.SingleFile;
                            fileXlsx.ExportType = DevExpress.Export.ExportType.DataAware;
                            gridControl.ExportToXlsx(sFileName, fileXlsx);
                            break;
                    }

                    if (System.IO.File.Exists(sFileName))
                    {
                        System.Diagnostics.Process.Start(sFileName);
                    }
                    else
                    {
                        XtraMessageBox.Show("해당 파일을 찾을 수가 없습니다 / " + sFileName);
                        return;
                    }
                }
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        private static void File_Open(string Form_Name, int File_No)
        {
            string File_Name = "";

            SqlParam sp = new SqlParam("sp_Excel_Title");
            sp.AddParam("Form_Name", Form_Name);

            ReturnStruct temp_ret = new ReturnStruct();
            temp_ret = DbHelp.Proc_Search(sp);

            if (temp_ret.ReturnChk == 0)
            {
                File_Name = temp_ret.ReturnDataSet.Tables[0].Rows[0][0].NullString();

                if (File_No > 0)
                    File_Name = File_Name + "_" + File_No.NumString();
            }

            XtraSaveFileDialog fileOpen = new XtraSaveFileDialog();
            fileOpen.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            fileOpen.FileName = File_Name + "_" + DateTime.Now.ToString("yyyyMMdd");
            fileOpen.Filter = "Excel Files(*.xls, *.xlsx, *.xlt)|*.xls;*.xlsx;*.xlt";

            if(fileOpen.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                sFileName = fileOpen.FileName;
            }
        }


        //FTP 업로드
        public static bool FTP_Upload_File(string SourcePath, string TargetURI)
        {
            try
            {
                FtpWebRequest Ftpreq = (FtpWebRequest)WebRequest.Create(TargetURI);
                Ftpreq.Credentials = new NetworkCredential(ID, PW);
                Ftpreq.Method = WebRequestMethods.Ftp.UploadFile;

                FileStream Source_Stream = new FileStream(SourcePath, FileMode.Open, FileAccess.Read);
                Stream Target_Stream = Ftpreq.GetRequestStream();

                byte[] buffer = new byte[2048];

                while (true)
                {
                    int byte_Count = Source_Stream.Read(buffer, 0, buffer.Length);

                    if (byte_Count == 0)
                    {
                        break;
                    }

                    Target_Stream.Write(buffer, 0, byte_Count);
                }

                Target_Stream.Close();
                Source_Stream.Close();

                Ftpreq = null;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        //파일 다운로드(FTP)
        public static bool FTP_Download_File(string SourceURI, string TargetPath, bool AutoOpen = true)
        {
            try
            {
                FtpWebRequest Ftpreq = (FtpWebRequest)WebRequest.Create(SourceURI);
                Ftpreq.Credentials = new NetworkCredential(ID, PW);
                Ftpreq.Method = WebRequestMethods.Ftp.DownloadFile;

                FtpWebResponse Ftprsp = (FtpWebResponse)Ftpreq.GetResponse();


                FileStream Target_Stream = new FileStream(TargetPath, FileMode.Create, FileAccess.Write);
                Stream Source_Stream = Ftprsp.GetResponseStream();

                byte[] buffer = new byte[2048];

                while (true)
                {
                    int byte_Count = Source_Stream.Read(buffer, 0, buffer.Length);

                    if (byte_Count == 0)
                    {
                        break;
                    }

                    Target_Stream.Write(buffer, 0, byte_Count);
                }

                Target_Stream.Close();
                Source_Stream.Close();

                Ftpreq = null;

                if (AutoOpen)
                    Process.Start(TargetPath);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        public static Image FTP_Download_Image(string SourceURI)
        {
            Image image = null;
            try
            {
                FtpWebRequest Ftpreq = (FtpWebRequest)WebRequest.Create(SourceURI);
                Ftpreq.Credentials = new NetworkCredential(ID, PW);
                Ftpreq.Method = WebRequestMethods.Ftp.DownloadFile;

                FtpWebResponse Ftprsp = (FtpWebResponse)Ftpreq.GetResponse();

                Stream Source_Stream = Ftprsp.GetResponseStream();

                image = Image.FromStream(Source_Stream);
               
                Source_Stream.Close();

                Ftpreq = null;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return image;
            }

            return image;
        }

        public static bool FTP_Delete_File(string URI)
        {
            try
            {
                FtpWebRequest ftp = WebRequest.Create(URI) as FtpWebRequest;
                ftp.Credentials = new NetworkCredential(ID, PW);
                ftp.Method = WebRequestMethods.Ftp.DeleteFile;
                FtpWebResponse response = ftp.GetResponse() as FtpWebResponse;
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        //파일 폴더 생성
        private static bool IsDirectory(string sDirectory)
        {
            try
            {
                FtpWebRequest Ftpreq = (FtpWebRequest)WebRequest.Create(sDirectory);
                Ftpreq.Credentials = new NetworkCredential(ID, PW);
                Ftpreq.Method = WebRequestMethods.Ftp.ListDirectory;

                using (Ftpreq.GetResponse())
                {
                    return true;
                }
            }
            catch (WebException)
            {
                return false;
            }
        }

        public static bool FTP_Directory(string sDirectory)
        {
            if (IsDirectory(sDirectory))
                return true;
            try
            {
                FtpWebRequest Ftpreq = (FtpWebRequest)WebRequest.Create(sDirectory);
                Ftpreq.Credentials = new NetworkCredential(ID, PW);
                Ftpreq.Method = WebRequestMethods.Ftp.MakeDirectory;

                FtpWebResponse Ftprsp = (FtpWebResponse)Ftpreq.GetResponse();
                Ftprsp = null;

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public static DataTable Get_File_List (string Target_URI)
        {
            if (IsDirectory(Target_URI))
            {
                DataTable File_Info = new DataTable();
                File_Info.Columns.Add("File_Name");
                File_Info.Columns.Add("File_Size", typeof(long));
                File_Info.Columns.Add("Upload_Date");

                string Directory_Data = Get_File_Info(Target_URI, WebRequestMethods.Ftp.ListDirectory);

                string[] Files = Directory_Data.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);


                foreach (string File in Files)
                {
                    try
                    {
                        string Size = Get_File_Info(Target_URI + "/" + File, WebRequestMethods.Ftp.GetFileSize);
                        string Update = Get_File_Info(Target_URI + "/" + File, WebRequestMethods.Ftp.GetDateTimestamp);

                        File_Info.Rows.Add(File, Convert.ToInt64(Size), Update);
                    }
                    catch (Exception)
                    {
                        File_Info.Rows.Add(File, -1, "");
                    }
                }

                return File_Info;
            }
            else
                return null;
        }

        private static string Get_File_Info(string Target_URI, string Request_Method)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(Target_URI);

            if (Request_Method == WebRequestMethods.Ftp.ListDirectory)
                request.Timeout = 3000;
            request.Method = Request_Method;
            request.Credentials = new NetworkCredential(ID, PW);

            try
            {
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                if (Request_Method == WebRequestMethods.Ftp.ListDirectory)
                    return (new StreamReader(response.GetResponseStream())).ReadToEnd();
                else if (Request_Method == WebRequestMethods.Ftp.GetFileSize)
                    return response.ContentLength.NumString();
                else if (Request_Method == WebRequestMethods.Ftp.GetDateTimestamp)
                    return response.LastModified.ToString("yyyy-MM-dd");
                else
                    return null;
            }
            catch (Exception)
            {
                if (Request_Method == WebRequestMethods.Ftp.GetFileSize)
                    return (-1).NumString();
                else if (Request_Method == WebRequestMethods.Ftp.GetDateTimestamp)
                    return "";
                else
                    return null;
            }
        }

        public static void Set_URL()
        {
            string sFTPstring = Configurations.GetConfig("FTPstring");
            string[] sFTP = sFTPstring.Split(';');

            FTP_URL = "ftp://" + sFTP[0] + "/";
            ID = sFTP[1];
            PW = sFTP[2];
            //FTP_URL = "ftp://192.168.0.60:21/";
            //ID = "administrator";
            //PW = "Root1234";
        }
    }
}

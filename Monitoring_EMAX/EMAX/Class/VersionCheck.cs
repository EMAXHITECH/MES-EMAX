using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EMAX_Monitoring
{
    class VersionCheck
    {
        public static string getVersion(string versionFile, string resourceDownloadFolder)
        {
            string sVersion = "";
            string sLine = "";

            StreamReader SR = new StreamReader(resourceDownloadFolder + versionFile);

            int iLine = 0;
            string[] sFile = new string[2];


            while((sLine = SR.ReadLine()) != null)
            {
                sFile[iLine] = sLine;
                iLine++;
            }

            sVersion = sFile[1].Split('|')[1];

            return sVersion;
        }

        public static string getServerVersion(string versionFile, string URL)
        {
            string sServerVersion = "";
            string sLine = "";

            WebClient client = new WebClient();
            Stream sr = client.OpenRead(URL + versionFile);
            StreamReader SR = new StreamReader(sr);

            int iLine = 0;
            string[] sFile = new string[2];


            while ((sLine = SR.ReadLine()) != null)
            {
                sFile[iLine] = sLine;
                iLine++;
            }

            sServerVersion = sFile[1].Split('|')[1];

            return sServerVersion;
        }
    }
}

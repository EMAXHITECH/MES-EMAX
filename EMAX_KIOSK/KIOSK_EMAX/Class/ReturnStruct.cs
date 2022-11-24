using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIOSK_EMAX
{
    public struct ReturnStruct
    {
        public int ReturnChk
        {
            set;
            get;
        }

        public DataSet ReturnDataSet
        {
            set;
            get;
        }

        public string ReturnMessage
        {
            set;
            get;
        }
    }
}

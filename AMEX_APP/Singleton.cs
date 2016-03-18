using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AMEX_APP
{
    public sealed class Singleton
    {
        #region "Declaration"

        private static SqlDataAccess oSqlDataAccess = null;

        #endregion

        #region "Construtor"
        private Singleton() { }
        #endregion

        #region "Property"

        public static SqlDataAccess objSqlDataAccess
        {
            get
            {
                if (oSqlDataAccess == null)
                {
                    oSqlDataAccess = new SqlDataAccess();
                }
                return oSqlDataAccess;
            }
        }        

        public static void traceService(string content)
        {
            try
            {
                string strFile = @"\WMS_Service_" + System.DateTime.Now.ToString("yyyyMMdd") + ".txt";
                string strPath = System.Windows.Forms.Application.StartupPath.ToString() + strFile;
                if (!File.Exists(strPath))
                {
                    File.Create(strPath);
                }
                FileStream fs = new FileStream(strPath, FileMode.Append, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.BaseStream.Seek(0, SeekOrigin.End);
                sw.WriteLine(content);
                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {
                //throw;
            }
        }

        #endregion
    }
}

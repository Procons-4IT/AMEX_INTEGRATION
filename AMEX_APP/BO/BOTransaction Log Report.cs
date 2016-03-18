using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BAL;

namespace AMEX_APP
{
    public class BOTransation_Log_Report : IBusinessLogic<DOTransaction_Log_Report>
    {
        const string SELECT = "AMEX_GetJE_Data_s_New_14032015";

        #region IBusinessLogic<Transaction log Report> Members
        public DataSet List(String Filedate)
        {
            DataSet _retVal = null;
            SqlDataAccess oDataWrapper = null;
            string strInterface = System.Configuration.ConfigurationManager.AppSettings["InterDB"].ToString();
            try
            {
                oDataWrapper = new SqlDataAccess();
                SqlParameter[] oParameter = new SqlParameter[1];
                oParameter[0] = new SqlParameter("@Instance", SqlDbType.VarChar);
                oParameter[0].Value = Filedate;
                _retVal = oDataWrapper.ExecuteDataSet(SELECT, oParameter, strInterface);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                oDataWrapper = null;
            }
            return _retVal;
        }
        #endregion
    }
}

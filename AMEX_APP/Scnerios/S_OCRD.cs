using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Armada_Sync;
using System.Collections;

namespace Armada_App
{
    public partial class S_OCRD : Form
    {
        private const string TRANSACTIONLOG = "[Armada_Service_M_S_OCRD_s]";
        private DataTable oDtTransLog = null;
        private string sQuery = string.Empty;

        public S_OCRD()
        {
            InitializeComponent();
        }

        private void S_OCRD_Load(object sender, EventArgs e)
        {
            try
            {
                UXUTIL.clsUtilities.setAllControlsThemes(this);
                this.WindowState = FormWindowState.Maximized;
                LoadAll();
            }
            catch (Exception ex)
            {
                TransLog.traceService(" Error : " + ex.Message);
            }
        }

        private void LoadAll()
        {
            try
            {
                string strInterface = System.Configuration.ConfigurationManager.AppSettings["InterDB"].ToString();
                DataTable oDS_OCRD = null;
                string str_M_S_OCRD = "Exec [Armada_Service_M_S_OCRD_s]";
                oDS_OCRD = Armada_Sync.Singleton.objSqlDataAccess.ExecuteReader(str_M_S_OCRD, strInterface);
                dgv_S_OCRD.DataSource = oDS_OCRD;
            }
            catch (Exception ex)
            {
                TransLog.traceService(" Error : " + ex.Message);
            }
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            try
            {
                string[] strValues = new string[4];
                Hashtable htCCDet = new Hashtable();
                string strInterface = System.Configuration.ConfigurationManager.AppSettings["InterDB"].ToString();
                string strMainDB = System.Configuration.ConfigurationManager.AppSettings["MainDB"].ToString();
                oDtTransLog = Armada_Sync.Singleton.objSqlDataAccess.ExecuteReader(TRANSACTIONLOG, strInterface);
                if (oDtTransLog != null && oDtTransLog.Rows.Count > 0)
                {
                    foreach (DataRow dr in oDtTransLog.Rows)
                    {
                        try
                        {
                            foreach (SAPbobsCOM.Company item in TransLog.objCompany)
                            {
                                TransLog.traceService(" Transaction Type : " + dr["Scenario"].ToString());
                                TransLog.traceService(" Transaction Key : " + dr["Key"].ToString());
                                TransLog.traceService("Company DB : " + item.CompanyDB);
                                if (item.Connected)
                                {
                                    string strCountry = item.GetCompanyService().GetAdminInfo().Country;
                                    if (dr["Country"].ToString() == strCountry)
                                    {
                                        if (strMainDB != item.CompanyDB)
                                        {
                                            Singleton.obj_S_OCRD.Sync((dr["Key"].ToString()), TransType.A, item, strInterface, "", strValues, htCCDet);
                                        }
                                    }
                                    else
                                    {
                                        TransLog.traceService(" Error : Company Not Connected.");
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            TransLog.traceService(" Error : " + ex.Message);
                        }
                    }
                }
                MessageBox.Show("Manual Sync Completed...");
                LoadAll();
            }
            catch (Exception ex)
            {
                TransLog.traceService("Error Message : " + ex.Message);
                TransLog.traceService(" Error Source : " + ex.Source);
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            S_OCRD.ActiveForm.Close();
        }
    }
}

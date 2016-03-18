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
    public partial class S_ORIN : Form
    {
        private const string TRANSACTIONLOG = "Armada_Service_M_S_ORIN_s";
        private DataTable oDtTransLog = null;
        private string sQuery = string.Empty;
        private DataTable oCompDT = null;
        private string strCompany = string.Empty;
        private string strWareHouse = string.Empty;
        private SAPbobsCOM.Company oGetCompany = null;

        public S_ORIN()
        {
            InitializeComponent();
        }

        private void S_ORIN_Load(object sender, EventArgs e)
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
                DataTable oDS_M_S_ORIN = null;
                string str_M_S_ORIN = "Exec Armada_Service_M_S_ORIN_s";
                oDS_M_S_ORIN = Armada_Sync.Singleton.objSqlDataAccess.ExecuteReader(str_M_S_ORIN, strInterface);
                dgv_S_ORIN.DataSource = oDS_M_S_ORIN; 
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
                string strInterface = System.Configuration.ConfigurationManager.AppSettings["InterDB"].ToString();
                string strMainDB = System.Configuration.ConfigurationManager.AppSettings["MainDB"].ToString();
                oDtTransLog = Armada_Sync.Singleton.objSqlDataAccess.ExecuteReader(TRANSACTIONLOG, strInterface);
                if (oDtTransLog != null && oDtTransLog.Rows.Count > 0)
                {                   
                    foreach (DataRow dr in oDtTransLog.Rows)
                    {
                        try
                        {
                            Hashtable htCCDet = new Hashtable();
                            string[] strValues = new string[4];

                            sQuery = " Select T1.U_COMPANY,T0.U_WAREHOUSE,T1.U_OUTACCT From  ";
                            sQuery += " [@Z_INBOUNDMAPPINGC] T0 JOIN [@Z_INBOUNDMAPPING] T1 On T0.Code = T1.Code ";
                            sQuery += " Where T0.U_SHOPID = '" + dr["ShopID"].ToString() + "'";
                            oCompDT = Armada_Sync.Singleton.objSqlDataAccess.ExecuteReader(sQuery, strMainDB);
                            if (oCompDT != null)
                            {
                                if (oCompDT.Rows.Count > 0)
                                {
                                    strCompany = oCompDT.Rows[0][0].ToString();
                                    strWareHouse = oCompDT.Rows[0][1].ToString();
                                    oGetCompany = TransLog.GetCompany(strCompany);
                                    strValues[0] = oCompDT.Rows[0][2].ToString();
                                }
                            }
                            
                            TransLog.traceService(" Transaction Type : " + dr["Scenario"].ToString());
                            TransLog.traceService(" Transaction Key : " + dr["Key"].ToString());
                            if (oGetCompany != null)
                            {
                                TransLog.traceService("Company DB : " + oGetCompany.CompanyDB);
                                if (oGetCompany.Connected)
                                {
                                    Singleton.obj_S_ORIN.Sync((dr["Key"].ToString()), TransType.A, oGetCompany, strInterface, strWareHouse, strValues, htCCDet);
                                }
                                else
                                {
                                    TransLog.traceService(" Error : Company Not Connected.");
                                }
                            }
                            else
                                TransLog.traceService(" Error : Company Not Found.");
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
            S_ORIN.ActiveForm.Close();
        }
    }
}

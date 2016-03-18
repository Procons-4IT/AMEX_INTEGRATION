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
    public partial class S_OINV : Form
    {
        private const string TRANSACTIONLOG = "Armada_Service_M_S_OINV_s";
        private DataTable oDtTransLog = null;
        private string sQuery = string.Empty;
        private DataTable oCompDT = null;
        private string strCompany = string.Empty;
        private string strWareHouse = string.Empty;
        private SAPbobsCOM.Company oGetCompany = null;
        private DataTable oCCDT = null;

        public S_OINV()
        {
            InitializeComponent();
        }

        private void S_OINV_Load(object sender, EventArgs e)
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
                DataTable oDS_M_S_OINV = null;
                string str_M_S_OINV = "Exec Armada_Service_M_S_OINV_s";
                oDS_M_S_OINV = Armada_Sync.Singleton.objSqlDataAccess.ExecuteReader(str_M_S_OINV, strInterface);
                dgv_S_OINV.DataSource = oDS_M_S_OINV;
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

                            sQuery = " Select T1.U_COMPANY,T0.U_WAREHOUSE From  ";
                            sQuery += " [@Z_INBOUNDMAPPINGC] T0 JOIN [@Z_INBOUNDMAPPING] T1 On T0.Code = T1.Code ";
                            sQuery += " Where T0.U_SHOPID = '" + dr["ShopID"].ToString() + "'";
                            oCompDT = Armada_Sync.Singleton.objSqlDataAccess.ExecuteReader(sQuery, strMainDB);
                            if (oCompDT != null)
                            {
                                if (oCompDT.Rows.Count > 0)
                                {
                                    strCompany = oCompDT.Rows[0]["U_COMPANY"].ToString();
                                    strWareHouse = oCompDT.Rows[0]["U_WAREHOUSE"].ToString();
                                    oGetCompany = TransLog.GetCompany(strCompany);

                                    sQuery = " Select T0.U_SCREDITCARD,T0.U_CREDITCARD,T0.U_CARDNUMBER,T0.U_CARDVALID,T0.U_PAYMENTMETHOD From  ";
                                    sQuery += " [@Z_INBOUNDMAPPINGC1] T0 JOIN [@Z_INBOUNDMAPPING] T1 On T0.Code = T1.Code ";
                                    sQuery += " Where T1.U_COMPANY = '" + strCompany + "'";
                                    oCCDT = Armada_Sync.Singleton.objSqlDataAccess.ExecuteReader(sQuery, strMainDB);
                                    if (oCCDT != null)
                                    {
                                        if (oCCDT.Rows.Count > 0)
                                        {
                                            foreach (DataRow dr1 in oCCDT.Rows)
                                            {
                                                htCCDet.Add(dr1["U_SCREDITCARD"], dr1);
                                            }
                                        }
                                    }
                                }
                            }

                            TransLog.traceService(" Transaction Type : " + dr["Scenario"].ToString());
                            TransLog.traceService(" Transaction Key : " + dr["Key"].ToString());
                            if (oGetCompany != null)
                            {
                                TransLog.traceService("Company DB : " + oGetCompany.CompanyDB);
                                if (oGetCompany.Connected)
                                {
                                    Singleton.obj_S_OINV.Sync((dr["Key"].ToString()), TransType.A, oGetCompany, strInterface, strWareHouse, strValues, htCCDet);
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
            S_OINV.ActiveForm.Close();
        }
    }
}

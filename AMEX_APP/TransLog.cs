using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.IO;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;

namespace AMEX_APP
{
    public partial class TransLog : Form
    {
        #region "Declarations"

        public static Hashtable oCompanyht = null;
        public string sQuery = string.Empty;
        public static SAPbobsCOM.Company[] objCompany = null;
        BackgroundWorker m_oWorker;
        BackgroundWorker i_oWorker;
        BackgroundWorker p_oWorker;
        private object _locker = new object();
        private object _locker1 = new object();
        private object _locker2 = new object();
        private SqlDataAdapter sqlAdap;
        private DataSet Ds;
        private SqlCommand sqlCmd;
        private SAPbobsCOM.Recordset oRecordSet;

        #endregion

        #region "Constructor"

        public TransLog()
        {
            try
            {
                InitializeComponent();

                m_oWorker = new BackgroundWorker();
                m_oWorker.DoWork += new DoWorkEventHandler(m_oWorker_DoWork);
                m_oWorker.ProgressChanged += new ProgressChangedEventHandler
                        (m_oWorker_ProgressChanged);
                m_oWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler
                        (m_oWorker_RunWorkerCompleted);
                m_oWorker.WorkerReportsProgress = true;
                m_oWorker.WorkerSupportsCancellation = true;

                i_oWorker = new BackgroundWorker();
                i_oWorker.DoWork += new DoWorkEventHandler(i_oWorker_DoWork);
                i_oWorker.ProgressChanged += new ProgressChangedEventHandler
                        (i_oWorker_ProgressChanged);
                i_oWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler
                        (i_oWorker_RunWorkerCompleted);
                i_oWorker.WorkerReportsProgress = true;
                i_oWorker.WorkerSupportsCancellation = true;

                p_oWorker = new BackgroundWorker();
                p_oWorker.DoWork += new DoWorkEventHandler(p_oWorker_DoWork);
                p_oWorker.ProgressChanged += new ProgressChangedEventHandler
                        (p_oWorker_ProgressChanged);
                p_oWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler
                        (p_oWorker_RunWorkerCompleted);
                p_oWorker.WorkerReportsProgress = true;
                p_oWorker.WorkerSupportsCancellation = true;

                TransLog.CheckForIllegalCrossThreadCalls = false;

            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region "BG"

        void m_oWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            StatusLabel.Text = "Successfully Logged Into SAP Business One";
        }

        void m_oWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBar1.Value = e.ProgressPercentage;
            //StatusLabel.Text = "Companys Connected Successfully...!";            
        }

        void m_oWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //ConnectAllSapCompany();
                if (1 == 1)
                {
                    ConnectAllSapCompany();
                    System.Threading.Thread.Sleep(5000);
                    string strInterface = System.Configuration.ConfigurationManager.AppSettings["InterDB"].ToString();
                    SAPbobsCOM.Company oCompany = GetCompany(strInterface, txtInstance.Text.ToString());
                    if (oCompany != null)
                    {
                        if (oCompany.Connected)
                        {
                            //DataSet ds = BindTxnDetails(Convert.ToDateTime("2016-02-01"));

                            DataSet ds = null;
                            string strPathR = GetFilePath_I("JE");
                            //string strFile = getFile(strPathR, Convert.ToDateTime("2016-02-01").ToString("ddMMyyyy"));
                            string strFile = getFile(strPathR, System.DateTime.Now.AddDays(-1).ToString("ddMMyyyy"), "Y");

                            if (string.IsNullOrEmpty(strFile))
                            {
                                traceService("No File Found to Process...", txtInstance.Text.ToString());
                            }
                            else
                            {
                                string strPath = @"" + strPathR + "\\" + strFile;


                                txtFileName.Text = strFile;

                                FileInfo info = new FileInfo(strPath);
                                //string strFile = info.Name;
                                string strFileDate = strFile.Substring(7, 8);

                                traceService("Processing Date : " + System.DateTime.Now.ToString("yyyyMMdd"), txtInstance.Text.ToString());
                                traceService("Processing Path : " + strPath, txtInstance.Text.ToString());
                                traceService("Processing File : " + strFile, txtInstance.Text.ToString());

                                oRecordSet = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                                sQuery = "Select T1.FileDate From Z_AMEX_H T1 Where T1.FileDate ='" + strFileDate + "' And FileName = '" + info.Name + "'";
                                sQuery += " AND IsRnSeq = 'Y' ";
                                sQuery += " AND IsRecCount = 'Y' ";
                                sQuery += " AND IsTrnAmtMat = 'Y' ";
                                sQuery += " AND IsBilAmtMat = 'Y' ";
                                sQuery += " AND IsAcctValid = 'Y' ";
                                sQuery += " AND IsCurValid = 'Y' ";
                                sQuery += " AND IsDim2Valid = 'Y' ";
                                sQuery += " AND IsDim3Valid = 'Y' ";
                                sQuery += " AND IsExValid = 'Y' ";
                                oRecordSet.DoQuery(sQuery);
                                if (!oRecordSet.EoF)
                                {
                                    traceService("Processing File Already Processed...", txtInstance.Text.ToString());
                                }
                                else
                                {
                                    if (true)
                                    {
                                        if (GetTextData_AMEX(strPath, txtInstance.Text.ToString(), strFile, strFileDate, info.Name))
                                        {
                                            BOTransation_Log_Report oBOTransation_Log_Report = null;
                                            oBOTransation_Log_Report = new BOTransation_Log_Report();
                                            ds = oBOTransation_Log_Report.List(txtInstance.Text.ToString());
                                        }

                                        System.Threading.Thread.Sleep(5000);

                                        if (ds != null && ds.Tables.Count > 0)
                                        {
                                            DataTable oHeader = ds.Tables[0];
                                            DataTable oDetails_K = ds.Tables[1];
                                            DataTable oDetails = ds.Tables[2];
                                            bool blnValid = true;
                                            if (oHeader.Rows.Count > 0)
                                            {
                                                if (oHeader.Rows[0]["IsRnSeq"].ToString() == "N")
                                                {
                                                    blnValid = false;
                                                    traceService("Validation File Sequence Failed : " + strFile, txtInstance.Text.ToString());
                                                }

                                                if (oHeader.Rows[0]["IsRecCount"].ToString() == "N")
                                                {
                                                    blnValid = false;
                                                    traceService("Validation File Record Count Failed : " + strFile, txtInstance.Text.ToString());
                                                }

                                                if (oHeader.Rows[0]["IsTrnAmtMat"].ToString() == "N")
                                                {
                                                    blnValid = false;
                                                    traceService("Validation File Transaction Amount Not Matches With Trailer Record  : " + strFile, txtInstance.Text.ToString());
                                                }

                                                if (oHeader.Rows[0]["IsBilAmtMat"].ToString() == "N")
                                                {
                                                    blnValid = false;
                                                    traceService("Validation File Billed Amount Not Matches With Trailer Record  : " + strFile, txtInstance.Text.ToString());
                                                }

                                                if (oHeader.Rows[0]["IsAcctValid"].ToString() == "N")
                                                {
                                                    blnValid = false;
                                                    traceService("Validation File Few Accounts Not Matches With Account Tables  : " + strFile, txtInstance.Text.ToString());
                                                }

                                                if (oHeader.Rows[0]["IsCurValid"].ToString() == "N")
                                                {
                                                    blnValid = false;
                                                    traceService("Validation File Currency Not Matches With Currency Tables  : " + strFile, txtInstance.Text.ToString());
                                                }

                                                if (oHeader.Rows[0]["IsDim2Valid"].ToString() == "N")
                                                {
                                                    blnValid = false;
                                                    traceService("Validation File Dimension 2 Not Matches With Dimension Tables  : " + strFile, txtInstance.Text.ToString());
                                                }

                                                if (oHeader.Rows[0]["IsDim3Valid"].ToString() == "N")
                                                {
                                                    blnValid = false;
                                                    traceService("Validation File Dimension 2 Not Matches With Dimension Tables  : " + strFile, txtInstance.Text.ToString());
                                                }

                                                if (oHeader.Rows[0]["IsExValid"].ToString() == "N")
                                                {
                                                    blnValid = false;
                                                    traceService("Validation File Exchange not Defined few Currency  : " + strFile, txtInstance.Text.ToString());
                                                }

                                                if (oHeader.Rows[0]["Series"].ToString() == "")
                                                {
                                                    blnValid = false;
                                                    traceService("Series Not Defined", txtInstance.Text.ToString());
                                                }
                                            }

                                            if (blnValid)
                                            {
                                                toolStripProgressBar1.Minimum = 0;
                                                toolStripProgressBar1.Maximum = 100;
                                                if (addJournal_Entry(txtInstance.Text, oDetails_K, oDetails, oHeader.Rows[0]["Series"].ToString(), oHeader.Rows[0]["DSAcct"].ToString(), oHeader.Rows[0]["CSAcct"].ToString()))
                                                {
                                                    MoveToFolder("JE", txtFileName.Text, "S");
                                                }
                                                else
                                                {
                                                    MoveToFolder("JE", txtFileName.Text, "F");
                                                }
                                            }
                                            else
                                            {
                                                MoveToFolder("JE", txtFileName.Text, "F");
                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }
                }
                m_oWorker.CancelAsync();
            }
            catch (Exception ex)
            {
                traceService(ex.StackTrace.ToString(), txtInstance.Text.ToString());
                traceService(ex.Message.ToString(), txtInstance.Text.ToString());
            }
            finally
            {
                traceService("Processing Completed : " + System.DateTime.Now.ToLongDateString() + " " + System.DateTime.Now.ToLongTimeString(), txtInstance.Text.ToString());
                toolStripProgressBar1.Value = 0;
                this.Close();
            }
        }

        void i_oWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            StatusLabel.Text = "Data Loaded Successfully....!";
        }

        void i_oWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBar1.Value = e.ProgressPercentage;
            StatusLabel.Text = "Processing......" + toolStripProgressBar1.Value.ToString() + "%";
        }

        void i_oWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                DataSet ds = BindTxnDetails(System.DateTime.Now);
                //DgvTxnLogParent.DataSource = ds.Tables[1];
            }
            catch (Exception ex)
            {
                traceService(ex.StackTrace.ToString(), txtInstance.Text.ToString());
                traceService(ex.Message.ToString(), txtInstance.Text.ToString());
            }
        }

        void p_oWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            StatusLabel.Text = "Data Loaded Successfully....!";
        }

        void p_oWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBar1.Value = e.ProgressPercentage;
            StatusLabel.Text = "Processing......" + toolStripProgressBar1.Value.ToString() + "%";
        }

        void p_oWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //DataTable oDetails = ((DataTable)DgvTxnLogParent.DataSource);
                //if (addJournal_Entry(txtInstance.Text, oDetails, ""))
                //{
                //    MoveToFolder("JE", txtFileName.Text);
                //}
            }
            catch (Exception ex)
            {
                traceService(ex.StackTrace.ToString(), txtInstance.Text.ToString());
                traceService(ex.Message.ToString(), txtInstance.Text.ToString());
            }
        }

        #endregion

        #region "Menus"

        private void logOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TransLog.ActiveForm.Close();
        }

        #endregion

        #region "Events"

        private void TransLog_Load(object sender, EventArgs e)
        {
            try
            {
                //if (1 == 2)
                //{
                //UXUTIL.clsUtilities.setAllControlsThemes(this);
                //this.WindowState = FormWindowState.Maximized;
                //companyStatusLabel.Text = "Connecting with SAP Company!!";
                //BinddataScenario("JE");
                //m_oWorker.RunWorkerAsync();
                //}
                string strLogTime = System.DateTime.Now.ToString("yyyyMMddhhmmss");
                txtInstance.Text = strLogTime;
                traceService("Processing Started : " + System.DateTime.Now.ToLongDateString() + " " + System.DateTime.Now.ToLongTimeString(), txtInstance.Text.ToString());
                m_oWorker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void oTimer_Tick(object sender, EventArgs e)
        {
            timeStatusLabel.Text = Convert.ToString(DateTime.Now) + "                                                                                                                                                           ";
        }

        private void oLogTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                //bindTrnDetailsAuto();
                //setRowColorBasedonStatus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void TransLog_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                this.ShowInTaskbar = false;
            }
        }

        private void BtnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                //i_oWorker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void DgvTxnLogParent_Sorted(object sender, EventArgs e)
        {
            //setRowColorBasedonStatus();
        }

        private void niTaskBar_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Maximized;
            this.ShowInTaskbar = true;
            //setRowColorBasedonStatus();
        }

        private void niTaskBar_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Maximized;
            this.ShowInTaskbar = true;
            //setRowColorBasedonStatus();
        }

        private void SystemtryShow_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Maximized;
            this.ShowInTaskbar = true;
            //setRowColorBasedonStatus();
        }

        private void SystemtryExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TransLog_FormClosing(object sender, FormClosingEventArgs e)
        {
            //disConnectCompany();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            p_oWorker.RunWorkerAsync();
        }

        #endregion

        #region"Functions"

        private void MoveToFolder(string strType, string FName, string strMoveType)
        {
            try
            {
                string OpenFolder = null;
                string SuccessFolder = null;
                string FailedFolder = null;

                OpenFolder = GetOpenPath(strType);
                SuccessFolder = GetSuccessPath(strType);
                FailedFolder = GetFailedPath(strType);

                string strFileName = OpenFolder + "\\" + FName;
                if (File.Exists(strFileName) == true)
                {
                    if (strMoveType == "S")
                    {
                        System.IO.File.Move(strFileName, SuccessFolder + "\\" + FName);
                    }
                    else if (strMoveType == "F")
                    {
                        System.IO.File.Move(strFileName, FailedFolder + "\\" + FName);
                    }
                }
            }
            catch (Exception ex)
            {
                traceService(ex.StackTrace.ToString(), txtInstance.Text.ToString());
                traceService(ex.Message.ToString(), txtInstance.Text.ToString());
            }
        }

        public string GetOpenPath(string Type)
        {
            string _retVal = null;
            try
            {
                string strInterface = System.Configuration.ConfigurationManager.AppSettings["MainDB"].ToString();
                SAPbobsCOM.Company oCompany = GetCompany(strInterface, txtInstance.Text.ToString());
                oRecordSet = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oRecordSet.DoQuery("Select T1.U_OPath From [@Z_AMX_IBND] T1 Where T1.U_Type='" + Type + "'");
                if (!oRecordSet.EoF)
                {
                    _retVal = oRecordSet.Fields.Item(0).Value.ToString();
                }
                else
                {
                    throw new Exception("");
                }
            }
            catch (Exception ex)
            {
                traceService(ex.StackTrace.ToString(), txtInstance.Text.ToString());
                traceService(ex.Message.ToString(), txtInstance.Text.ToString());
            }
            return _retVal;
        }

        public string GetSuccessPath(string Type)
        {
            string _retVal = null;
            try
            {
                string strInterface = System.Configuration.ConfigurationManager.AppSettings["MainDB"].ToString();
                SAPbobsCOM.Company oCompany = GetCompany(strInterface, txtInstance.Text.ToString());
                oRecordSet = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oRecordSet.DoQuery("Select T1.U_SPath From [@Z_AMX_IBND] T1 Where T1.U_Type='" + Type + "'");
                if (!oRecordSet.EoF)
                {
                    _retVal = oRecordSet.Fields.Item(0).Value.ToString();
                }
                else
                {
                    throw new Exception("");
                }
            }
            catch (Exception ex)
            {
                traceService(ex.StackTrace.ToString(), txtInstance.Text.ToString());
                traceService(ex.Message.ToString(), txtInstance.Text.ToString());
            }
            return _retVal;
        }

        public string GetFailedPath(string Type)
        {
            string _retVal = null;
            try
            {
                string strInterface = System.Configuration.ConfigurationManager.AppSettings["MainDB"].ToString();
                SAPbobsCOM.Company oCompany = GetCompany(strInterface, txtInstance.Text.ToString());
                oRecordSet = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oRecordSet.DoQuery("Select T1.U_FPath From [@Z_AMX_IBND] T1 Where T1.U_Type='" + Type + "'");
                if (!oRecordSet.EoF)
                {
                    _retVal = oRecordSet.Fields.Item(0).Value.ToString();
                }
                else
                {
                    throw new Exception("");
                }
            }
            catch (Exception ex)
            {
                traceService(ex.StackTrace.ToString(), txtInstance.Text.ToString());
                traceService(ex.Message.ToString(), txtInstance.Text.ToString());
            }
            return _retVal;
        }

        public string GetLoggerPath(string Type)
        {
            string _retVal = null;
            try
            {
                string strInterface = System.Configuration.ConfigurationManager.AppSettings["MainDB"].ToString();
                SAPbobsCOM.Company oCompany = GetCompany(strInterface, txtInstance.Text.ToString());
                oRecordSet = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oRecordSet.DoQuery("Select T1.U_LPath From [@Z_AMX_IBND] T1 Where T1.U_Type='" + Type + "'");
                if (!oRecordSet.EoF)
                {
                    _retVal = oRecordSet.Fields.Item(0).Value.ToString();
                }
                else
                {
                    throw new Exception("");
                }
            }
            catch (Exception ex)
            {
                traceService(ex.StackTrace.ToString(), txtInstance.Text.ToString());
                traceService(ex.Message.ToString(), txtInstance.Text.ToString());
            }
            return _retVal;
        }

        public string GetFilePath_I(string Type)
        {
            string _retVal = null;
            try
            {
                string strInterface = System.Configuration.ConfigurationManager.AppSettings["MainDB"].ToString();
                SAPbobsCOM.Company oCompany = GetCompany(strInterface, txtInstance.Text.ToString());
                SAPbobsCOM.Recordset oRecordSet = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oRecordSet.DoQuery("Select T1.U_OPath From [@Z_AMX_IBND] T1 Where T1.U_Type= '" + Type + "'");
                if (!oRecordSet.EoF)
                {
                    _retVal = oRecordSet.Fields.Item(0).Value.ToString();
                }
                else
                {
                    throw new Exception("");
                }
            }
            catch (Exception ex)
            {
                traceService(ex.StackTrace.ToString(), txtInstance.Text.ToString());
                traceService(ex.Message.ToString(), txtInstance.Text.ToString());
            }
            return _retVal;
        }

        public string getFile(string strPath, string strDate, string strFirst)
        {
            string _retVal = null;
            try
            {
                foreach (System.IO.FileInfo file in new System.IO.DirectoryInfo(strPath.ToString()).GetFiles("*.TXT"))
                {
                    if (strFirst == "Y")
                    {
                        _retVal = file.Name;
                        break;
                    }
                    else
                    {
                        if (file.Name.Contains(strDate))
                        {
                            _retVal = file.Name;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                traceService(ex.StackTrace.ToString(), txtInstance.Text.ToString());
                traceService(ex.Message.ToString(), txtInstance.Text.ToString());
            }
            return _retVal;
        }

        public void BinddataScenario(String val)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = Scenario();
            CmbScenario.DataSource = bs;
            CmbScenario.DisplayMember = "Text";
            CmbScenario.ValueMember = "ID";
            CmbScenario.SelectedValue = val;
        }

        private DataTable Scenario()
        {
            DataTable dt = new DataTable();
            try
            {
                dt.Columns.Add("ID", typeof(string));
                dt.Columns.Add("Text", typeof(string));
                DataRow dr = dt.NewRow();
                dr["ID"] = 0;
                dr["Text"] = "-ALL-";
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr["ID"] = "JE";
                dr["Text"] = "Journal Entry";
                dt.Rows.Add(dr);
            }
            catch (Exception ex)
            {
                traceService(ex.StackTrace.ToString(), txtInstance.Text.ToString());
                traceService(ex.Message.ToString(), txtInstance.Text.ToString());
            }

            return dt;
        }

        private DataSet BindTxnDetails(DateTime dtLoadDate)
        {
            DataSet ds = null;
            try
            {
                string strPathR = GetFilePath_I("JE");
                string strFile = getFile(strPathR, dtLoadDate.ToString("ddMMyyyy"), "N");
                string strPath = @"" + strPathR + "\\" + strFile;
                string strLogTime = System.DateTime.Now.ToString("yyyyMMddhhmmss");
                txtInstance.Text = strLogTime;
                txtFileName.Text = strFile;
                FileInfo info = new FileInfo(strPath);
                //string strFile = info.Name;
                string strFileDate = strFile.Substring(7, 8);

                if (GetTextData_AMEX(strPath, strLogTime, strFile, strFileDate, info.Name))
                {
                    BOTransation_Log_Report oBOTransation_Log_Report = null;
                    oBOTransation_Log_Report = new BOTransation_Log_Report();
                    ds = oBOTransation_Log_Report.List(strLogTime);
                }
            }
            catch (Exception ex)
            {
                traceService(ex.StackTrace.ToString(), txtInstance.Text.ToString());
                traceService(ex.Message.ToString(), txtInstance.Text.ToString());
            }

            return ds;
        }

        //private void bindTrnDetailsAuto()
        //{
        //    string strInterface = System.Configuration.ConfigurationManager.AppSettings["MainDB"].ToString();
        //    DataTable oDtRefresh = null;
        //    string strQuery = "Exec Armada_Service_TxnLogReportRefresh_S";
        //    oDtRefresh = Singleton.objSqlDataAccess.ExecuteReader(strQuery, strInterface);
        //    DgvTxnLogParent.DataSource = oDtRefresh;
        //}

        private void setRowColorBasedonStatus()
        {
            try
            {
                // string[] strValues = new string[4];
                //Image[] image = new Image[3];
                //image[0] = Armada_App.Properties.Resources.Yes1;
                //image[1] = Armada_App.Properties.Resources.Error1;
                //image[2] = Armada_App.Properties.Resources.Create1;

                int intRow = 0;
                toolStripProgressBar1.Maximum = DgvTxnLogParent.RowCount;
                foreach (DataGridViewRow row in DgvTxnLogParent.Rows)
                {
                    string RowType = row.Cells["Status"].Value.ToString();
                    //System.Threading.Thread.Sleep(1000);
                    if (RowType == "Success")
                    {
                        //row.Cells["Image"].Value = image[0];
                        row.DefaultCellStyle.ForeColor = Color.Green;
                        //Image image = Armada_App.Properties.Resources.Yes1;                        
                    }
                    else if (RowType == "Failed")
                    {
                        // row.Cells["Image"].Value = image[1];
                        row.DefaultCellStyle.ForeColor = Color.Red;
                        //Image image = Armada_App.Properties.Resources.Error1;                        
                    }
                    else if (RowType == "Open")
                    {
                        //row.Cells["Image"].Value = image[2];
                        row.DefaultCellStyle.ForeColor = Color.SteelBlue;
                        //Image image = Armada_App.Properties.Resources.Create1;                        
                    }

                    //if (intRow == 100)
                    //{
                    //    intRow = 1;
                    //    toolStripProgressBar1.Value = 0;
                    //}

                    toolStripProgressBar1.Value = intRow;
                    intRow += 1;
                }
                // toolStripProgressBar1.Value = 0;
                //Application.DoEvents();
            }
            catch (Exception ex)
            {
                traceService(ex.StackTrace.ToString(), txtInstance.Text.ToString());
                traceService(ex.Message.ToString(), txtInstance.Text.ToString());
            }
            finally
            {
                toolStripProgressBar1.Value = 0;
            }
        }

        public bool GetTextData_AMEX(string strPath, string strLogTime, string strFile, string strFileDate, string strfName)
        {
            bool _retVal = false;
            DataTable _oDt_H = new DataTable();
            DataTable _oDt_D = new DataTable();
            DataTable _oDt_T = new DataTable();

            try
            {
                _oDt_H.TableName = "HD";
                _oDt_H.Columns.Add("RecNum", typeof(string)).Caption = "1-6-6";
                _oDt_H.Columns.Add("FileMode", typeof(string)).Caption = "7-9-3";
                _oDt_H.Columns.Add("PrmDate", typeof(string)).Caption = "10-17-8";
                _oDt_H.Columns.Add("PrmBatchno", typeof(string)).Caption = "18-32-15";
                _oDt_H.Columns.Add("Filler1", typeof(string)).Caption = "33-397-365";

                _oDt_D.TableName = "DE";
                _oDt_D.Columns.Add("RecNum", typeof(string)).Caption = "1-6-6";
                _oDt_D.Columns.Add("PrmDate", typeof(string)).Caption = "7-14-8";
                _oDt_D.Columns.Add("PrmTrxnAmt", typeof(string)).Caption = "15-33-19";
                _oDt_D.Columns.Add("PrmTrxnCur", typeof(string)).Caption = "34-36-3";
                _oDt_D.Columns.Add("PrmTxnDate", typeof(string)).Caption = "37-44-8";
                _oDt_D.Columns.Add("PrmBillAmt", typeof(string)).Caption = "45-63-19";
                _oDt_D.Columns.Add("PrmBillCur", typeof(string)).Caption = "64-66-3";
                _oDt_D.Columns.Add("PrmPostDate", typeof(string)).Caption = "67-74-8";
                _oDt_D.Columns.Add("PrmLoadDate", typeof(string)).Caption = "75-82-8";
                _oDt_D.Columns.Add("GLAccNumDB", typeof(string)).Caption = "83-107-25";
                _oDt_D.Columns.Add("GLAccNumCR", typeof(string)).Caption = "108-132-25";
                _oDt_D.Columns.Add("PrmTrxnName", typeof(string)).Caption = "133-172-40";
                _oDt_D.Columns.Add("PrmTrxnStatus", typeof(string)).Caption = "173-176-4";
                _oDt_D.Columns.Add("PrmEntSerno", typeof(string)).Caption = "177-191-15";
                _oDt_D.Columns.Add("PrmEntity", typeof(string)).Caption = "192-192-1";
                _oDt_D.Columns.Add("PrmARN", typeof(string)).Caption = "193-215-23";
                _oDt_D.Columns.Add("PrmRRN", typeof(string)).Caption = "216-227-12";
                _oDt_D.Columns.Add("PrmRsnCd", typeof(string)).Caption = "228-231-4";
                _oDt_D.Columns.Add("PrmTxnSerno", typeof(string)).Caption = "232-246-15";
                _oDt_D.Columns.Add("AmexTID", typeof(string)).Caption = "247-261-15";
                _oDt_D.Columns.Add("PrmPrdSerno", typeof(string)).Caption = "262-267-6";
                _oDt_D.Columns.Add("PrmPrdCode", typeof(string)).Caption = "268-269-2";
                _oDt_D.Columns.Add("PrmPrdType", typeof(string)).Caption = "270-272-3";
                _oDt_D.Columns.Add("PrmEntName", typeof(string)).Caption = "273-312-40";
                _oDt_D.Columns.Add("PrmEntAcNo", typeof(string)).Caption = "313-332-20";
                _oDt_D.Columns.Add("PrmBAName", typeof(string)).Caption = "333-372-40";
                _oDt_D.Columns.Add("PrmIBAN", typeof(string)).Caption = "373-397-25";
                _oDt_D.Columns.Add("Sequence", typeof(string)).Caption = "Sequence";

                _oDt_T.TableName = "TR";
                _oDt_T.Columns.Add("RecNum", typeof(string)).Caption = "1-6-6";
                _oDt_T.Columns.Add("Filler1", typeof(string)).Caption = "7-14-8";
                _oDt_T.Columns.Add("TrxnAmtHash", typeof(string)).Caption = "15-33-19";
                _oDt_T.Columns.Add("Filler2", typeof(string)).Caption = "34-44-11";
                _oDt_T.Columns.Add("PostAmtHash", typeof(string)).Caption = "45-63-19";
                _oDt_T.Columns.Add("Filler3", typeof(string)).Caption = "64-397-334";

                string strSerial = strFile.Substring(16, 5);

                if (strPath.Length > 0)
                {
                    string[] txtRows = null;
                    DataRow oDr = default(DataRow);
                    txtRows = System.IO.File.ReadAllLines(strPath);
                    int intCompleted = txtRows.Length - 1;
                    int intRow = 0;
                    traceService("Total No of records : " + (txtRows.Length - 2).ToString(), txtInstance.Text.ToString());
                    foreach (string txtrow in txtRows)
                    {
                        if (intRow == 0)
                        {
                            StatusLabel.Text = "Loading Header...";
                            lblRec.Text = "Loading Header...";

                            oDr = _oDt_H.NewRow();
                            for (int index = 0; index <= _oDt_H.Columns.Count - 1; index++)
                            {
                                string[] strParam = _oDt_H.Columns[index].Caption.Split('-');
                                oDr[index] = txtrow.Substring(Convert.ToInt32(strParam[0]) - 1, Convert.ToInt32(strParam[2])).Replace("'", "''");
                            }
                            _oDt_H.Rows.Add(oDr);
                        }
                        else if (intRow > 0 & intRow < txtRows.Length - 1)
                        {
                            oDr = _oDt_D.NewRow();
                            for (int index = 0; index <= _oDt_D.Columns.Count - 1; index++)
                            {
                                StatusLabel.Text = "Loading Detail...";
                                lblRec.Text = "Loading Detail...";

                                if (index == (intCompleted / 10))
                                {
                                    toolStripProgressBar1.PerformStep();
                                }

                                if (_oDt_D.Columns[index].Caption == "Sequence")
                                {
                                    oDr[index] = intRow.ToString();
                                }
                                else
                                {
                                    string[] strParam = _oDt_D.Columns[index].Caption.Split('-');
                                    oDr[index] = txtrow.Substring(Convert.ToInt32(strParam[0]) - 1, Convert.ToInt32(strParam[2])).Replace("'", "''");
                                }
                            }
                            _oDt_D.Rows.Add(oDr);
                        }
                        else if (intRow == txtRows.Length - 1)
                        {
                            StatusLabel.Text = "Loading Trailer...";
                            lblRec.Text = "Loading Trailer...";

                            oDr = _oDt_T.NewRow();
                            for (int index = 0; index <= _oDt_T.Columns.Count - 1; index++)
                            {
                                string[] strParam = _oDt_T.Columns[index].Caption.Split('-');
                                oDr[index] = txtrow.Substring(Convert.ToInt32(strParam[0]) - 1, Convert.ToInt32(strParam[2])).Replace("'", "''");
                            }
                            _oDt_T.Rows.Add(oDr);
                        }
                        intRow = intRow + 1;
                    }
                }

                try
                {
                    //Header
                    StatusLabel.Text = "Importing Header...";
                    lblRec.Text = "Importing Header...";
                    string strInterface = System.Configuration.ConfigurationManager.AppSettings["InterDB"].ToString();
                    string strDtXML_H = getXMLstring(_oDt_H);
                    string strQuery = "Exec [Insert_AMEX_H] '" + strFileDate + "','" + strSerial + "','" + strDtXML_H + "','" + strLogTime + "','" + strfName + "'";
                    Singleton.objSqlDataAccess.ExecuteNonQuery(strQuery, strInterface);

                    //Details
                    Int32 totalRows = _oDt_D.Rows.Count;
                    int intLoop = 10;
                    Int32 breakrec = totalRows / intLoop;
                    int skipRecord = 0;
                    //int totalPushed = 0;
                    int totalBal = _oDt_D.Rows.Count;
                    toolStripProgressBar1.Minimum = 0;
                    toolStripProgressBar1.Maximum = 100;

                    StatusLabel.Text = "Importing Detail...";
                    lblRec.Text = "Importing Detail...";
                    for (int index = 0; index <= intLoop; index++)
                    {
                        toolStripProgressBar1.PerformStep();

                        if (totalBal > 0)
                        {
                            if (totalBal > breakrec)
                            {
                                dynamic pushSplitDT = _oDt_D.AsEnumerable().Skip(skipRecord).Take(breakrec).CopyToDataTable();
                                pushSplitDT.TableName = "DE";
                                string strDtXML_D = getXMLstring(pushSplitDT);
                                strQuery = "Exec [Insert_AMEX_D] '" + strFileDate + "','" + strSerial + "','" + strDtXML_D + "','" + strLogTime + "'";
                                Singleton.objSqlDataAccess.ExecuteNonQuery(strQuery, strInterface);
                                skipRecord += breakrec;
                                //totalPushed += breakrec;
                                totalBal -= breakrec;
                                System.Threading.Thread.Sleep(1000);
                            }
                            else if (totalBal > 0 && totalBal <= breakrec)
                            {
                                dynamic pushSplitDT = _oDt_D.AsEnumerable().Skip(skipRecord).Take(totalBal).CopyToDataTable();
                                pushSplitDT.TableName = "DE";
                                string strDtXML_D = getXMLstring(pushSplitDT);
                                strQuery = "Exec [Insert_AMEX_D] '" + strFileDate + "','" + strSerial + "','" + strDtXML_D + "','" + strLogTime + "'";
                                Singleton.objSqlDataAccess.ExecuteNonQuery(strQuery, strInterface);
                                skipRecord += breakrec;
                                //totalPushed += breakrec;
                                totalBal = 0;
                                System.Threading.Thread.Sleep(1000);
                            }
                        }
                    }


                    //Trailer
                    StatusLabel.Text = "Importing Trailer...";
                    lblRec.Text = "Importing Trailer...";
                    string strDtXML_T = getXMLstring(_oDt_T);
                    strQuery = "Exec [Insert_AMEX_T] '" + strFileDate + "','" + strSerial + "','" + strDtXML_T + "','" + strLogTime + "'";
                    Singleton.objSqlDataAccess.ExecuteNonQuery(strQuery, strInterface);

                    _retVal = true;
                }
                catch (Exception ex)
                {
                    traceService(ex.StackTrace.ToString(), txtInstance.Text.ToString());
                    traceService(ex.Message.ToString(), txtInstance.Text.ToString());
                }
                finally
                {
                    toolStripProgressBar1.Value = 0;
                }
            }
            catch (Exception ex)
            {
                traceService(ex.StackTrace.ToString(), txtInstance.Text.ToString());
                traceService(ex.Message.ToString(), txtInstance.Text.ToString());
            }
            finally
            {
                toolStripProgressBar1.Value = 0;
            }
            return _retVal;
        }

        public string getXMLstring(System.Data.DataTable oDt)
        {
            string _retVal = string.Empty;
            try
            {
                System.IO.StringWriter sr = new System.IO.StringWriter();
                oDt.WriteXml(sr, false);
                _retVal = sr.ToString();
            }
            catch (Exception ex)
            {
                traceService(ex.StackTrace.ToString(), txtInstance.Text.ToString());
                traceService(ex.Message.ToString(), txtInstance.Text.ToString());
            }
            return _retVal;
        }

        private bool addJournal_Entry(string strInstance, DataTable oDetails_K, DataTable oDetails_All, string strSeries, string strDSusAcct, string strCSusAcct)
        {
            bool _retVal = false;
            bool _retVal_M = true;

            try
            {
                Int32 intStatus = 0;
                string strQuery = string.Empty;
                double dblAmount = 0;
                string strMainDB = System.Configuration.ConfigurationManager.AppSettings["MainDB"].ToString();
                string strInterface = System.Configuration.ConfigurationManager.AppSettings["InterDB"].ToString();
                SAPbobsCOM.Company oCompany = GetCompany(strMainDB, txtInstance.Text.ToString());

                SAPbobsCOM.Recordset oRecordSet = null;
                SAPbobsCOM.JournalEntries oJE = null;

                oRecordSet = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                SAPbobsCOM.Recordset oRecordSet_A = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

                string strLocalC = oCompany.GetCompanyService().GetAdminInfo().LocalCurrency;

                int intCount = 0;
                int intCompleted = oDetails_K.Rows.Count;
                int intSuccessRecords = 0;
                int intSuspenseRecords = 0;

                foreach (DataRow dr_k in oDetails_K.Rows)
                {
                    bool blnBulk = false;

                    intCount += 1;
                    if (intCount == (intCompleted / 10))
                    {
                        toolStripProgressBar1.PerformStep();
                        intCount = 0;
                    }

                    DataView dv = new DataView();
                    dv = oDetails_All.DefaultView;
                    DataTable dt = dv.Table;
                    dt.DefaultView.RowFilter = "Key = '" + dr_k["Key"].ToString() + "'";
                    DataTable oDetails = dt.DefaultView.ToTable();

                    oJE = (SAPbobsCOM.JournalEntries)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oJournalEntries);

                    oJE.Series = Convert.ToInt16(strSeries);
                    oJE.Memo = "Amex Journal Import";
                    oJE.UserFields.Fields.Item("U_Import").Value = "Y";

                    if (oDetails != null)
                    {
                        DataRow dr1 = oDetails.Rows[0];

                        traceService("************************************************************", txtInstance.Text.ToString());
                        StatusLabel.Text = "Processing Data : " + dr1["Key"].ToString();
                        lblRec.Text = "Processing Data : " + dr1["Key"].ToString();

                        string strKey, strDA, strCA, strDim2, strDim3, strCurrency, strRefDate1, strRecNum;

                        strKey = dr1["Key"].ToString();
                        strDA = dr1["DA"].ToString();
                        strCA = dr1["CA"].ToString();
                        strDim2 = dr1["Dim2"].ToString();
                        strDim3 = dr1["Dim3"].ToString();
                        strCurrency = dr1["Currency"].ToString();
                        strRefDate1 = dr1["RefDate"].ToString();
                        strRecNum = dr1["RecNum"].ToString();

                        traceService("Processing Data : " + dr1["Key"].ToString(), txtInstance.Text.ToString());
                        traceService("Processing PostingType : " + dr1["PostingType"].ToString(), txtInstance.Text.ToString());
                        traceService("Processing Debit Acct : " + dr1["DA"].ToString(), txtInstance.Text.ToString());
                        traceService("Processing Credit Acct : " + dr1["CA"].ToString(), txtInstance.Text.ToString());
                        traceService("Processing Dimension 2 : " + dr1["Dim2"].ToString(), txtInstance.Text.ToString());
                        traceService("Processing Dimension 3 : " + dr1["Dim3"].ToString(), txtInstance.Text.ToString());
                        traceService("Processing Currency : " + dr1["Currency"].ToString(), txtInstance.Text.ToString());
                        traceService("Processing RefDate : " + dr1["RefDate"].ToString(), txtInstance.Text.ToString());

                        //UDF's
                        if (dr1["Bulk"].ToString() == "0")
                        {
                            DateTime dtRefDate;
                            string strRefDate = dr1["RefDate"].ToString();
                            if (strRefDate != string.Empty)
                            {
                                dtRefDate = Convert.ToDateTime(strRefDate.Substring(4, 4) + "-" + strRefDate.Substring(2, 2) + "-" + strRefDate.Substring(0, 2));
                                oJE.ReferenceDate = dtRefDate;
                                oJE.TaxDate = dtRefDate;
                                oJE.DueDate = dtRefDate;
                            }

                            //oJE.Memo = dr1["Memo"].ToString();
                            //oJE.Reference = dr1["Ref1"].ToString();
                            //oJE.Reference2 = dr1["Ref2"].ToString();
                            //oJE.Reference3 = dr1["Ref3"].ToString();

                            //oJE.UserFields.Fields.Item("U_PRRN").Value = dr1["U_PRRN"].ToString();
                            //oJE.UserFields.Fields.Item("U_PRC").Value = dr1["U_PRC"].ToString();
                            //oJE.UserFields.Fields.Item("U_PPS").Value = dr1["U_PPS"].ToString();
                            //oJE.UserFields.Fields.Item("U_PEN").Value = dr1["U_PEN"].ToString();
                            //oJE.UserFields.Fields.Item("U_PBANP").Value = dr1["U_PBANP"].ToString();
                            //oJE.UserFields.Fields.Item("U_PBAN").Value = dr1["U_PBAN"].ToString();
                            //oJE.UserFields.Fields.Item("U_PIBAN").Value = dr1["U_PIBAN"].ToString();
                            //oJE.UserFields.Fields.Item("U_PTA").Value = dr1["U_PTA"].ToString();
                            //oJE.UserFields.Fields.Item("U_PTC").Value = dr1["U_PTC"].ToString();
                            //oJE.UserFields.Fields.Item("U_PTS").Value = dr1["U_PTS"].ToString();
                            //oJE.UserFields.Fields.Item("U_PE").Value = dr1["U_PE"].ToString();

                            //DateTime dtPTD;
                            //string strPTD = dr1["U_PTD"].ToString();
                            //if (strPTD != string.Empty)
                            //{
                            //    dtPTD = Convert.ToDateTime(strPTD.Substring(4, 4) + "-" + strPTD.Substring(2, 2) + "-" + strPTD.Substring(0, 2));
                            //    oJE.UserFields.Fields.Item("U_PTD").Value = dtPTD;
                            //}
                        }
                        else
                        {
                            blnBulk = true;
                            DateTime dtRefDate;
                            string strRefDate = dr1["RefDate"].ToString();
                            if (strRefDate != string.Empty)
                            {
                                dtRefDate = Convert.ToDateTime(strRefDate.Substring(4, 4) + "-" + strRefDate.Substring(2, 2) + "-" + strRefDate.Substring(0, 2));
                                oJE.ReferenceDate = dtRefDate;
                                oJE.TaxDate = dtRefDate;
                                oJE.DueDate = dtRefDate;
                                oJE.Reference = dtRefDate.ToString("yyyyMMdd");
                            }
                        }

                        int intRow = 0;

                        foreach (DataRow dr in oDetails.Rows)
                        {
                            if (dr["PostingType"].ToString() == "BL" || dr["PostingType"].ToString() == "ID")
                            {
                                dblAmount = Convert.ToDouble(dr["Amount"].ToString());
                                oJE.Lines.SetCurrentLine(intRow);                               

                                string strQuery_A = "Select AcctCode From OACT Where AcctCode = '" + dr["DA"].ToString().Trim() + "'";
                                oRecordSet_A.DoQuery(strQuery_A);
                                if (oRecordSet_A.EoF)
                                {
                                    oJE.Lines.AccountCode = strDSusAcct;
                                    intSuspenseRecords += 1;
                                }
                                else
                                {
                                    oJE.Lines.AccountCode = dr["DA"].ToString();
                                }
                                if (dr["Currency"].ToString() == strLocalC)
                                {
                                    oJE.Lines.Debit = dblAmount;
                                }
                                else
                                {
                                    oJE.Lines.FCDebit = dblAmount;
                                    oJE.Lines.FCCurrency = dr["Currency"].ToString();
                                }
                                oJE.Lines.CostingCode2 = dr["Dim2"].ToString();
                                oJE.Lines.CostingCode3 = dr["Dim3"].ToString();

                                if (dr["PostingType"].ToString() == "BL")
                                {
                                    DateTime dtRefDate;
                                    string strRefDate = dr["RefDate"].ToString();
                                    if (strRefDate != string.Empty)
                                    {
                                        dtRefDate = Convert.ToDateTime(strRefDate.Substring(4, 4) + "-" + strRefDate.Substring(2, 2) + "-" + strRefDate.Substring(0, 2));
                                        oJE.Lines.Reference1 = dtRefDate.ToString("yyyyMMdd");
                                    }
                                }
                                else
                                {
                                    oJE.Lines.LineMemo = dr["Memo"].ToString();
                                    oJE.Lines.Reference1 = dr["Ref1"].ToString();
                                    oJE.Lines.Reference2 = dr["Ref2"].ToString();
                                    oJE.Lines.AdditionalReference = dr["Ref3"].ToString();

                                    oJE.Lines.UserFields.Fields.Item("U_PRRN").Value = dr["U_PRRN"].ToString();
                                    oJE.Lines.UserFields.Fields.Item("U_PRC").Value = dr["U_PRC"].ToString();
                                    oJE.Lines.UserFields.Fields.Item("U_PPS").Value = dr["U_PPS"].ToString();
                                    oJE.Lines.UserFields.Fields.Item("U_PEN").Value = dr["U_PEN"].ToString();
                                    oJE.Lines.UserFields.Fields.Item("U_PBANP").Value = dr["U_PBANP"].ToString();
                                    oJE.Lines.UserFields.Fields.Item("U_PBAN").Value = dr["U_PBAN"].ToString();
                                    oJE.Lines.UserFields.Fields.Item("U_PIBAN").Value = dr["U_PIBAN"].ToString();
                                    oJE.Lines.UserFields.Fields.Item("U_PTA").Value = dr["U_PTA"].ToString();
                                    oJE.Lines.UserFields.Fields.Item("U_PTC").Value = dr["U_PTC"].ToString();
                                    oJE.Lines.UserFields.Fields.Item("U_PTS").Value = dr["U_PTS"].ToString();
                                    oJE.Lines.UserFields.Fields.Item("U_PE").Value = dr["U_PE"].ToString();

                                    DateTime dtPTD;
                                    string strPTD = dr["U_PTD"].ToString();
                                    if (strPTD != string.Empty)
                                    {
                                        dtPTD = Convert.ToDateTime(strPTD.Substring(4, 4) + "-" + strPTD.Substring(2, 2) + "-" + strPTD.Substring(0, 2));
                                        oJE.Lines.UserFields.Fields.Item("U_PTD").Value = dtPTD;
                                    }
                                }

                                oJE.Lines.Add();
                                oJE.Lines.SetCurrentLine(1);

                                strQuery_A = "Select AcctCode From OACT Where AcctCode = '" + dr["CA"].ToString().Trim() + "'";
                                oRecordSet_A.DoQuery(strQuery_A);
                                if (oRecordSet_A.EoF)
                                {
                                    oJE.Lines.AccountCode = strCSusAcct;
                                    intSuspenseRecords += 1;
                                }
                                else
                                {
                                    oJE.Lines.AccountCode = dr["CA"].ToString();
                                }
                                if (dr["Currency"].ToString() == strLocalC)
                                {
                                    oJE.Lines.Credit = dblAmount;
                                }
                                else
                                {
                                    oJE.Lines.FCCredit = dblAmount;
                                    oJE.Lines.FCCurrency = dr["Currency"].ToString();
                                }
                                oJE.Lines.CostingCode2 = dr["Dim2"].ToString();
                                oJE.Lines.CostingCode3 = dr["Dim3"].ToString();

                                if (dr["PostingType"].ToString() == "BL")
                                {
                                    DateTime dtRefDate;
                                    string strRefDate = dr["RefDate"].ToString();
                                    if (strRefDate != string.Empty)
                                    {
                                        dtRefDate = Convert.ToDateTime(strRefDate.Substring(4, 4) + "-" + strRefDate.Substring(2, 2) + "-" + strRefDate.Substring(0, 2));
                                        oJE.Lines.Reference1 = dtRefDate.ToString("yyyyMMdd");
                                    }
                                }
                                else
                                {
                                    oJE.Lines.LineMemo = dr["Memo"].ToString();
                                    oJE.Lines.Reference1 = dr["Ref1"].ToString();
                                    oJE.Lines.Reference2 = dr["Ref2"].ToString();
                                    oJE.Lines.AdditionalReference = dr["Ref3"].ToString();

                                    oJE.Lines.UserFields.Fields.Item("U_PRRN").Value = dr["U_PRRN"].ToString();
                                    oJE.Lines.UserFields.Fields.Item("U_PRC").Value = dr["U_PRC"].ToString();
                                    oJE.Lines.UserFields.Fields.Item("U_PPS").Value = dr["U_PPS"].ToString();
                                    oJE.Lines.UserFields.Fields.Item("U_PEN").Value = dr["U_PEN"].ToString();
                                    oJE.Lines.UserFields.Fields.Item("U_PBANP").Value = dr["U_PBANP"].ToString();
                                    oJE.Lines.UserFields.Fields.Item("U_PBAN").Value = dr["U_PBAN"].ToString();
                                    oJE.Lines.UserFields.Fields.Item("U_PIBAN").Value = dr["U_PIBAN"].ToString();
                                    oJE.Lines.UserFields.Fields.Item("U_PTA").Value = dr["U_PTA"].ToString();
                                    oJE.Lines.UserFields.Fields.Item("U_PTC").Value = dr["U_PTC"].ToString();
                                    oJE.Lines.UserFields.Fields.Item("U_PTS").Value = dr["U_PTS"].ToString();
                                    oJE.Lines.UserFields.Fields.Item("U_PE").Value = dr["U_PE"].ToString();

                                    DateTime dtPTD;
                                    string strPTD = dr["U_PTD"].ToString();
                                    if (strPTD != string.Empty)
                                    {
                                        dtPTD = Convert.ToDateTime(strPTD.Substring(4, 4) + "-" + strPTD.Substring(2, 2) + "-" + strPTD.Substring(0, 2));
                                        oJE.Lines.UserFields.Fields.Item("U_PTD").Value = dtPTD;
                                    }
                                }

                                intRow += 1;
                            }
                            else if (dr["PostingType"].ToString() == "DB" || dr["PostingType"].ToString() == "CB")
                            {
                                dblAmount = Convert.ToDouble(dr["Amount"].ToString());
                                oJE.Lines.SetCurrentLine(intRow);

                                if (dr["Type"].ToString() == "0")
                                {
                                    string strQuery_A = "Select AcctCode From OACT Where AcctCode = '" + dr["DA"].ToString().Trim() + "'";
                                    oRecordSet_A.DoQuery(strQuery_A);
                                    if (oRecordSet_A.EoF)
                                    {
                                        oJE.Lines.AccountCode = strDSusAcct;
                                        intSuspenseRecords += 1;
                                    }
                                    else
                                    {
                                        oJE.Lines.AccountCode = dr["DA"].ToString();
                                    }

                                    if (dr["Currency"].ToString() == strLocalC)
                                    {
                                        oJE.Lines.Debit = dblAmount;
                                    }
                                    else
                                    {
                                        oJE.Lines.FCDebit = dblAmount;
                                        oJE.Lines.FCCurrency = dr["Currency"].ToString();
                                    }                                    
                                }
                                else if (dr["Type"].ToString() == "1")
                                {
                                    string strQuery_A = "Select AcctCode From OACT Where AcctCode = '" + dr["CA"].ToString().Trim() + "'";
                                    oRecordSet_A.DoQuery(strQuery_A);
                                    if (oRecordSet_A.EoF)
                                    {
                                        oJE.Lines.AccountCode = strCSusAcct;
                                        intSuspenseRecords += 1;
                                    }
                                    else
                                    {
                                        oJE.Lines.AccountCode = dr["CA"].ToString();
                                    }

                                    if (dr["Currency"].ToString() == strLocalC)
                                    {
                                        oJE.Lines.Credit = dblAmount;
                                    }
                                    else
                                    {
                                        oJE.Lines.FCCredit = dblAmount;
                                        oJE.Lines.FCCurrency = dr["Currency"].ToString();
                                    }                                   
                                }

                                oJE.Lines.CostingCode2 = dr["Dim2"].ToString();
                                oJE.Lines.CostingCode3 = dr["Dim3"].ToString();

                                if (dr["IsCon"].ToString() == "1")
                                {
                                    DateTime dtRefDate;
                                    string strRefDate = dr["RefDate"].ToString();
                                    if (strRefDate != string.Empty)
                                    {
                                        dtRefDate = Convert.ToDateTime(strRefDate.Substring(4, 4) + "-" + strRefDate.Substring(2, 2) + "-" + strRefDate.Substring(0, 2));
                                        oJE.Lines.Reference1 = dtRefDate.ToString("yyyyMMdd");
                                    }
                                }
                                else
                                {
                                    oJE.Lines.LineMemo = dr["Memo"].ToString();
                                    oJE.Lines.Reference1 = dr["Ref1"].ToString();
                                    oJE.Lines.Reference2 = dr["Ref2"].ToString();
                                    oJE.Lines.AdditionalReference = dr["Ref3"].ToString();

                                    oJE.Lines.UserFields.Fields.Item("U_PRRN").Value = dr["U_PRRN"].ToString();
                                    oJE.Lines.UserFields.Fields.Item("U_PRC").Value = dr["U_PRC"].ToString();
                                    oJE.Lines.UserFields.Fields.Item("U_PPS").Value = dr["U_PPS"].ToString();
                                    oJE.Lines.UserFields.Fields.Item("U_PEN").Value = dr["U_PEN"].ToString();
                                    oJE.Lines.UserFields.Fields.Item("U_PBANP").Value = dr["U_PBANP"].ToString();
                                    oJE.Lines.UserFields.Fields.Item("U_PBAN").Value = dr["U_PBAN"].ToString();
                                    oJE.Lines.UserFields.Fields.Item("U_PIBAN").Value = dr["U_PIBAN"].ToString();
                                    oJE.Lines.UserFields.Fields.Item("U_PTA").Value = dr["U_PTA"].ToString();
                                    oJE.Lines.UserFields.Fields.Item("U_PTC").Value = dr["U_PTC"].ToString();
                                    oJE.Lines.UserFields.Fields.Item("U_PTS").Value = dr["U_PTS"].ToString();
                                    oJE.Lines.UserFields.Fields.Item("U_PE").Value = dr["U_PE"].ToString();

                                    DateTime dtPTD;
                                    string strPTD = dr["U_PTD"].ToString();
                                    if (strPTD != string.Empty)
                                    {
                                        dtPTD = Convert.ToDateTime(strPTD.Substring(4, 4) + "-" + strPTD.Substring(2, 2) + "-" + strPTD.Substring(0, 2));
                                        oJE.Lines.UserFields.Fields.Item("U_PTD").Value = dtPTD;
                                    }
                                }

                                oJE.Lines.Add();
                                intRow += 1;
                            }
                        }

                        intStatus = oJE.Add();
                        if (intStatus != 0)
                        {
                            traceService("Processing Error Code : " + oCompany.GetLastErrorCode().ToString(), txtInstance.Text.ToString());
                            traceService("Processing Error : " + oCompany.GetLastErrorDescription(), txtInstance.Text.ToString());
                            _retVal = false;
                            _retVal_M = false;
                        }
                        else
                        {
                            traceService("Processing Status : Success ", txtInstance.Text.ToString());
                            _retVal = true;
                        }

                        if (_retVal)
                        {
                            if (blnBulk)
                            {
                                sQuery = "Exec AMEX_GetJERec_Data_s '" + txtInstance.Text + "','" + strDA.Trim() + "','" + strCA.Trim() + "','" + strDim2.Trim() + "','" + strDim3.Trim() + "','" + strCurrency.Trim() + "','" + strRefDate1 + "'";
                                DataSet oDS = Singleton.objSqlDataAccess.ExecuteDataSet(sQuery, strInterface);
                                string[] array = oDS.Tables[0]
                                                 .AsEnumerable()
                                                 .Select(row => row.Field<string>("RecNum"))
                                                 .ToArray();

                                var dt_R = new DataTable("JEREF");
                                dt_R.Columns.Add("RecNum", typeof(string));
                                dt_R.Columns.Add("Instance", typeof(string));
                                dt_R.Columns.Add("JEREF", typeof(string));
                                foreach (string item in array)
                                {
                                    DataRow dr3 = dt_R.NewRow();
                                    dr3[0] = item.ToString();
                                    dr3[1] = txtInstance.Text;
                                    dr3[2] = oCompany.GetNewObjectKey();
                                    dt_R.Rows.Add(dr3);
                                }

                                string strDtXML_R = getXMLstring(dt_R);
                                strQuery = "Exec [UPDATE_AMEX_R] '" + strDtXML_R + "'";
                                Singleton.objSqlDataAccess.ExecuteNonQuery(strQuery, strInterface);
                                intSuccessRecords += array.Length;
                            }
                            else
                            {
                                string[] array = new string[1];
                                array[0] = strRecNum;//dr["RecNum"].ToString();

                                var dt_R = new DataTable("JEREF");
                                dt_R.Columns.Add("RecNum", typeof(string));
                                dt_R.Columns.Add("Instance", typeof(string));
                                dt_R.Columns.Add("JEREF", typeof(string));

                                foreach (string item in array)
                                {
                                    DataRow dr2 = dt_R.NewRow();
                                    dr2[0] = item.ToString();
                                    dr2[1] = txtInstance.Text;
                                    dr2[2] = oCompany.GetNewObjectKey();
                                    dt_R.Rows.Add(dr2);
                                }
                                string strDtXML_R = getXMLstring(dt_R);
                                strQuery = "Exec [UPDATE_AMEX_R] '" + strDtXML_R + "'";
                                Singleton.objSqlDataAccess.ExecuteNonQuery(strQuery, strInterface);
                                intSuccessRecords += 1;
                            }
                        }
                    }
                }

                traceService("****************************************************", txtInstance.Text.ToString());
                traceService("Total records posted successfully : " + intSuccessRecords.ToString(), txtInstance.Text.ToString());
                traceService("Total no of records posted to Suspense(Both Debit & Credit) : " + intSuspenseRecords.ToString(), txtInstance.Text.ToString());
                traceService("****************************************************", txtInstance.Text.ToString());

                return _retVal_M;
            }
            catch (Exception ex)
            {
                traceService(ex.Message, txtInstance.Text.ToString());
            }
            return _retVal_M;
        }

        public void ConnectAllSapCompany()
        {
            try
            {
                SAPbobsCOM.Company oCompany = null;
                oCompanyht = new Hashtable();

                string strMaiDB = System.Configuration.ConfigurationManager.AppSettings["MainDB"].ToString();
                string DBServer = System.Configuration.ConfigurationManager.AppSettings["SAPServer"].ToString();
                string ServerType = System.Configuration.ConfigurationManager.AppSettings["DbServerType"].ToString();
                string DBUserName = System.Configuration.ConfigurationManager.AppSettings["DbUserName"].ToString();
                string DBPwd = System.Configuration.ConfigurationManager.AppSettings["DbPassword"].ToString();
                string LicenseServer = System.Configuration.ConfigurationManager.AppSettings["SAPlicense"].ToString();
                string SAPUserName = System.Configuration.ConfigurationManager.AppSettings["SAPUserName"].ToString();
                string SAPPwd = System.Configuration.ConfigurationManager.AppSettings["SAPPassword"].ToString();

                sQuery = " SELECT '" + strMaiDB + "' U_COMPANY,'" + SAPUserName + "' U_SAPUSERNAME,'" + SAPPwd + "' U_SAPPASSWORD  ";
                string ConnectionString = String.Format(System.Configuration.ConfigurationManager.AppSettings["Logger"].ToString(), strMaiDB);
                DataSet oDS = Singleton.objSqlDataAccess.ExecuteDataSet(sQuery, strMaiDB);
                if (oDS != null)
                {
                    DataTable oDT_C = oDS.Tables[0];
                    int intCompany = oDT_C.Rows.Count;
                    objCompany = new SAPbobsCOM.Company[intCompany];
                    int intCompCount = 0;

                    foreach (DataRow dr_company in oDT_C.Rows)
                    {
                        StatusLabel.Text = "Connecting Sap Company : " + dr_company["U_COMPANY"].ToString();
                        lblRec.Text = "Connecting Sap Company : " + dr_company["U_COMPANY"].ToString();

                        oCompany = new SAPbobsCOM.Company();
                        oCompany.Server = DBServer;
                        switch (ServerType)
                        {
                            case "2008":
                                oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2008;
                                break;
                            case "2012":
                                oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2012;
                                break;
                            case "2014":
                                oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2014;
                                break;
                            default:
                                break;
                        }
                        oCompany.DbUserName = DBUserName;
                        oCompany.DbPassword = DBPwd;
                        oCompany.CompanyDB = dr_company["U_COMPANY"].ToString();
                        oCompany.UserName = dr_company["U_SAPUSERNAME"].ToString();
                        oCompany.Password = dr_company["U_SAPPASSWORD"].ToString();
                        oCompany.UseTrusted = false;

                        if (oCompany.Connect() != 0)
                        {
                            objCompany[intCompCount] = oCompany;
                            oCompanyht.Add(dr_company["U_COMPANY"].ToString(), oCompany);
                            traceService("Processing Company : " + oCompany.CompanyDB, txtInstance.Text.ToString());
                            traceService("Processing Company Connection Error Code : " + oCompany.GetLastErrorDescription(), txtInstance.Text.ToString());
                        }
                        else
                        {
                            objCompany[intCompCount] = oCompany;
                            oCompanyht.Add(dr_company["U_COMPANY"].ToString(), oCompany);
                            traceService("Processing Company : " + oCompany.CompanyDB, txtInstance.Text.ToString());
                            traceService("Processing Company Connected", txtInstance.Text.ToString());
                        }
                        intCompCount += 1;
                    }
                }
            }
            catch (Exception ex)
            {
                traceService(ex.StackTrace.ToString(), txtInstance.Text.ToString());
                traceService(ex.Message.ToString(), txtInstance.Text.ToString());
                throw;
            }
        }

        public static SAPbobsCOM.Company GetCompany(string strCompany, string strInstance)
        {
            SAPbobsCOM.Company _retVal = null;
            try
            {
                bool blnCompanyExist = false;

                foreach (string key in oCompanyht.Keys)
                {
                    if (key.ToString() == strCompany)
                    {
                        _retVal = (SAPbobsCOM.Company)oCompanyht[key];
                        //traceService("Get Company");
                        blnCompanyExist = true;
                        break;
                    }
                }

                if (!blnCompanyExist)
                {
                    throw new Exception("Company Not Found...");
                }
            }
            catch (Exception ex)
            {
                traceService(ex.Message, strInstance);
            }
            return _retVal;
        }

        public void disConnectCompany()
        {
            try
            {
                SAPbobsCOM.Company _retVal = null;
                foreach (string key in oCompanyht.Keys)
                {
                    _retVal = (SAPbobsCOM.Company)oCompanyht[key];
                    if (_retVal != null)
                    {
                        if (_retVal.Connected)
                        {
                            _retVal.Disconnect();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                traceService(ex.Message, txtInstance.Text.ToString());
            }
        }

        public static void traceService(string content, string strInstance)
        {
            try
            {
                string strFile = @"\AMEX_Service_" + strInstance + ".txt";
                string strPath = System.Windows.Forms.Application.StartupPath.ToString() + strFile;
                if (!File.Exists(strPath))
                {
                    FileStream fs = new FileStream(strPath, FileMode.Create, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.BaseStream.Seek(0, SeekOrigin.End);
                    sw.WriteLine(content);
                    sw.Flush();
                    sw.Close();
                }
                else
                {
                    FileStream fs = new FileStream(strPath, FileMode.Append, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.BaseStream.Seek(0, SeekOrigin.End);
                    sw.WriteLine(content);
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                //throw;
            }
        }

        #endregion
    }
}


//public static string FormatedSpace(string val, int fixedLen)
//{
//    int len = 0;
//    string retVal = string.Empty;

//    try
//    {
//        len = val.Length;
//        retVal = val;
//        for (int cnt = 0; cnt < fixedLen - len - 1; cnt++)
//        {
//            retVal = retVal + " ";
//        }
//    }
//    catch (Exception ex)
//    {
//        traceService(ex.Message);
//    }
//    return retVal;
//}

//private void CreateExceptionLog(string strLPath, string content)
//{
//    StreamWriter swExceptionLog = null;
//    try
//    {
//        string strFile = @"\AMEX_Logger_" + System.DateTime.Now.ToString("yyyyMMdd") + ".txt";
//        string strPath = strLPath + strFile;
//        swExceptionLog = new StreamWriter(strPath, true);
//        if (!File.Exists(strLPath))
//        {
//            FileStream fs = new FileStream(strPath, FileMode.Create, FileAccess.Write);
//            StreamWriter sw = new StreamWriter(fs);
//            sw.BaseStream.Seek(0, SeekOrigin.End);
//            sw.WriteLine(content);
//            sw.Flush();
//            sw.Close();
//        }
//        else
//        {
//            FileStream fs = new FileStream(strPath, FileMode.Append, FileAccess.Write);
//            StreamWriter sw = new StreamWriter(fs);
//            sw.BaseStream.Seek(0, SeekOrigin.End);
//            sw.WriteLine(content);
//            sw.Flush();
//            sw.Close();
//        }
//    }
//    catch (Exception ex)
//    {
//        traceService(ex.Message);
//    }
//}

//Bulk Update
//var dt_R = new DataTable("JEREF");
//dt_R.Columns.Add("RecNum", typeof(string));
//dt_R.Columns.Add("Instance", typeof(string));
//dt_R.Columns.Add("JEREF", typeof(string));

//foreach (DictionaryEntry entry in oHT)
//{
//    string[] arry = (string[])entry.Key;
//    foreach (string item in arry)
//    {
//        DataRow dr = dt_R.NewRow();
//        dr[0] = item.ToString();
//        dr[1] = txtInstance.Text;
//        dr[2] = entry.Value;
//        dt_R.Rows.Add(dr);
//    }
//}

//Int32 totalRows = dt_R.Rows.Count;
//int intLoop = 10;
//Int32 breakrec = totalRows / intLoop;
//int skipRecord = 0;
//for (int index = 0; index <= intLoop; index++)
//{
//    dynamic pushSplitDT = dt_R.AsEnumerable().Skip(skipRecord).Take(breakrec).CopyToDataTable();
//    pushSplitDT.TableName = "JEREF";
//    string strDtXML_R = getXMLstring(pushSplitDT);
//    strQuery = "Exec [UPDATE_AMEX_R] '" + strDtXML_R + "'";
//    Singleton.objSqlDataAccess.ExecuteNonQuery(strQuery, strInterface);
//    skipRecord += breakrec;
//}
//Bulk Update

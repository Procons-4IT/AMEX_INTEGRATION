Imports System.Data.SqlClient
Imports System.IO


Public Class Form1

    Private oCompany As SAPbobsCOM.Company
    Private oRecordSet As SAPbobsCOM.Recordset
    Private sqlAdap As SqlDataAdapter
    Private Ds As DataSet
    Private sqlCmd As SqlCommand

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Try
            'connectLocalCompany()
            Dim strPath As String = "C:\Users\LENOVO\Desktop\Amex\SAP-GL#01022016-07776.txt"
            GetTextData_AMEX(strPath)
        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show(ex.Message)
        Finally
            If Not IsNothing(oCompany) Then
                If oCompany.Connected Then
                    oCompany.Disconnect()
                End If
            End If
        End Try
    End Sub

    Public Function GetTextData_AMEX(ByVal strPath As String) As Boolean
        Dim _retVal As Boolean = False
        Dim _oDt_H As New DataTable
        Dim _oDt_D As New DataTable
        Dim _oDt_T As New DataTable
        Dim strLogTime As String = System.DateTime.Now.ToString("yyyyMMddhhmmss")

        Try

            _oDt_H.TableName = "HD"
            _oDt_H.Columns.Add("RecNum", GetType(String)).Caption = "1-6-6"
            _oDt_H.Columns.Add("FileMode", GetType(String)).Caption = "7-9-3"
            _oDt_H.Columns.Add("PrmDate", GetType(String)).Caption = "10-17-8"
            _oDt_H.Columns.Add("PrmBatchno", GetType(String)).Caption = "18-32-15"
            _oDt_H.Columns.Add("Filler1", GetType(String)).Caption = "33-397-365"

            _oDt_D.TableName = "DE"
            _oDt_D.Columns.Add("RecNum", GetType(String)).Caption = "1-6-6"
            _oDt_D.Columns.Add("PrmDate", GetType(String)).Caption = "7-14-8"
            _oDt_D.Columns.Add("PrmTrxnAmt", GetType(String)).Caption = "15-33-19"
            _oDt_D.Columns.Add("PrmTrxnCur", GetType(String)).Caption = "34-36-3"
            _oDt_D.Columns.Add("PrmTxnDate", GetType(String)).Caption = "37-44-8"
            _oDt_D.Columns.Add("PrmBillAmt", GetType(String)).Caption = "45-63-19"
            _oDt_D.Columns.Add("PrmBillCur", GetType(String)).Caption = "64-66-3"
            _oDt_D.Columns.Add("PrmPostDate", GetType(String)).Caption = "67-74-8"
            _oDt_D.Columns.Add("PrmLoadDate", GetType(String)).Caption = "75-82-8"
            _oDt_D.Columns.Add("GLAccNumDB", GetType(String)).Caption = "83-107-25"
            _oDt_D.Columns.Add("GLAccNumCR", GetType(String)).Caption = "108-132-25"
            _oDt_D.Columns.Add("PrmTrxnName", GetType(String)).Caption = "133-172-40"
            _oDt_D.Columns.Add("PrmTrxnStatus", GetType(String)).Caption = "173-176-4"
            _oDt_D.Columns.Add("PrmEntSerno", GetType(String)).Caption = "177-191-15"
            _oDt_D.Columns.Add("PrmEntity", GetType(String)).Caption = "192-192-1"
            _oDt_D.Columns.Add("PrmARN", GetType(String)).Caption = "193-215-23"
            _oDt_D.Columns.Add("PrmRRN ", GetType(String)).Caption = "216-227-12"
            _oDt_D.Columns.Add("PrmRsnCd ", GetType(String)).Caption = "228-231-4"
            _oDt_D.Columns.Add("PrmTxnSerno ", GetType(String)).Caption = "232-246-15"
            _oDt_D.Columns.Add("AmexTID", GetType(String)).Caption = "247-261-15"
            _oDt_D.Columns.Add("PrmPrdSerno", GetType(String)).Caption = "262-267-6"
            _oDt_D.Columns.Add("PrmPrdCode", GetType(String)).Caption = "268-269-2"
            _oDt_D.Columns.Add("PrmPrdType", GetType(String)).Caption = "270-272-3"
            _oDt_D.Columns.Add("PrmEntName", GetType(String)).Caption = "273-312-40"
            _oDt_D.Columns.Add("PrmEntAcNo", GetType(String)).Caption = "313-332-20"
            _oDt_D.Columns.Add("PrmBAName", GetType(String)).Caption = "333-372-40"
            _oDt_D.Columns.Add("PrmIBAN", GetType(String)).Caption = "373-397-25"
            _oDt_D.Columns.Add("Sequence", GetType(String)).Caption = "Sequence"

            _oDt_T.TableName = "TR"
            _oDt_T.Columns.Add("RecNum", GetType(String)).Caption = "1-6-6"
            _oDt_T.Columns.Add("Filler1", GetType(String)).Caption = "7-14-8"
            _oDt_T.Columns.Add("TrxnAmtHash", GetType(String)).Caption = "15-33-19"
            _oDt_T.Columns.Add("Filler2", GetType(String)).Caption = "34-44-11"
            _oDt_T.Columns.Add("PostAmtHash", GetType(String)).Caption = "45-63-19"
            _oDt_T.Columns.Add("Filler3", GetType(String)).Caption = "64-397-334"

            Dim info As New FileInfo(strPath)
            Dim strFile As String = info.Name
            Dim strFileDate As String = strFile.Substring(7, 8)
            Dim strSerial As String = strFile.Substring(16, 5)
            If strPath.Length > 0 Then
                Dim txtRows() As String
                Dim oDr As DataRow
                txtRows = System.IO.File.ReadAllLines(strPath)
                Dim intRow As Integer = 0
                For Each txtrow As String In txtRows
                    If intRow = 0 Then
                        oDr = _oDt_H.NewRow()
                        For index As Integer = 0 To _oDt_H.Columns.Count - 1
                            Dim strParam() As String = _oDt_H.Columns.Item(index).Caption.Split("-")
                            oDr(index) = txtrow.Substring(CInt(strParam(0)) - 1, CInt(strParam(2))).Replace("'", "''")
                        Next
                        _oDt_H.Rows.Add(oDr)
                    ElseIf intRow > 0 And intRow < txtRows.Length - 1 Then
                        oDr = _oDt_D.NewRow()
                        For index As Integer = 0 To _oDt_D.Columns.Count - 1
                            If _oDt_D.Columns.Item(index).Caption = "Sequence" Then
                                oDr(index) = intRow.ToString()
                            Else
                                Dim strParam() As String = _oDt_D.Columns.Item(index).Caption.Split("-")
                                oDr(index) = txtrow.Substring(CInt(strParam(0)) - 1, CInt(strParam(2))).Replace("'", "''")
                            End If
                        Next
                        _oDt_D.Rows.Add(oDr)
                    ElseIf intRow = txtRows.Length - 1 Then
                        oDr = _oDt_T.NewRow()
                        For index As Integer = 0 To _oDt_T.Columns.Count - 1
                            Dim strParam() As String = _oDt_T.Columns.Item(index).Caption.Split("-")
                            oDr(index) = txtrow.Substring(CInt(strParam(0)) - 1, CInt(strParam(2))).Replace("'", "''")
                        Next
                        _oDt_T.Rows.Add(oDr)
                    End If
                    intRow = intRow + 1
                Next
            End If

            Dim ConnectionString As String = "Server=LENOVO-PC;database=AMEX;uid=sa;pwd=sap2008;Connection Timeout=300"
            Dim myConnection As SqlConnection = New SqlConnection(ConnectionString)
            myConnection.Open()
            Try

                'Header
                Dim strDtXML_H As String = getXMLstring(_oDt_H)
                Dim strQuery As String = "Exec [Insert_AMEX_H] '" + strFileDate + "','" + strSerial + "','" + strDtXML_H + "','" + strLogTime + "','" + info.Name + "'"
                sqlCmd = New SqlCommand(strQuery, myConnection)
                sqlCmd.ExecuteNonQuery()

                'Details
                Dim totalRows = _oDt_D.Rows.Count
                Dim intLoop As Integer = 10
                Dim breakrec = totalRows / intLoop
                Dim skipRecord As Integer = 0
                For index As Integer = 0 To intLoop
                    Dim pushSplitDT = _oDt_D.AsEnumerable().Skip(skipRecord).Take(breakrec).CopyToDataTable()
                    pushSplitDT.TableName = "DE"
                    Dim strDtXML_D As String = getXMLstring(pushSplitDT)
                    strQuery = "Exec [Insert_AMEX_D] '" + strFileDate + "','" + strSerial + "','" + strDtXML_D + "','" + strLogTime + "'"
                    sqlCmd = New SqlCommand(strQuery, myConnection)
                    sqlCmd.ExecuteNonQuery()
                    skipRecord += breakrec
                Next

                'Trailer
                Dim strDtXML_T As String = getXMLstring(_oDt_T)
                strQuery = "Exec [Insert_AMEX_T] '" + strFileDate + "','" + strSerial + "','" + strDtXML_T + "','" + strLogTime + "'"
                sqlCmd = New SqlCommand(strQuery, myConnection)
                sqlCmd.ExecuteNonQuery()

            Catch ex As Exception
                myConnection.Close()
            Finally
                myConnection.Close()
            End Try

        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show(ex.Message)
        End Try

    End Function

    Public Function getXMLstring(ByVal oDt As System.Data.DataTable) As String
        Dim _retVal As String = String.Empty
        Try
            Dim sr As New System.IO.StringWriter()
            oDt.WriteXml(sr, False)
            _retVal = sr.ToString()
            Return _retVal
        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show(ex.Message)
        End Try
    End Function

    Private Function connectLocalCompany() As Boolean
        Try

            oCompany = New SAPbobsCOM.Company
            oCompany.Server = "LENOVO-PC"
            oCompany.language = SAPbobsCOM.BoSuppLangs.ln_English
            'If strLocalServertype = "2005" Then
            '    oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2005
            'ElseIf strLocalServertype = "2008" Then
            oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2008
            'ElseIf strLocalServertype = "2012" Then
            '    oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2012

            'End If

            oCompany.LicenseServer = "LENOVO-PC" ' "Compaq-PC:30000"
            oCompany.DbUserName = "sa"
            oCompany.DbPassword = "sap2008"
            oCompany.CompanyDB = "SBODemoIn"
            oCompany.UserName = "manager"
            oCompany.Password = "1234"

            If oCompany.Connected = True Then

            Else
                If oCompany.Connect <> 0 Then
                    'MsgBox("Connection to SAP B1 failed. Error Description :" & oCompany.GetLastErrorDescription)
                    Throw New Exception(oCompany.GetLastErrorDescription())
                Else

                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
        Return True
    End Function

End Class

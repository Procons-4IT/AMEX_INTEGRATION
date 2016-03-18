Public NotInheritable Class clsTable

#Region "Private Functions"
    '*************************************************************************************************************
    'Type               : Private Function
    'Name               : AddTables
    'Parameter          : 
    'Return Value       : none
    'Author             : Manu
    'Created Dt         : 
    'Last Modified By   : 
    'Modified Dt        : 
    'Purpose            : Generic Function for adding all Tables in DB. This function shall be called by 
    '                     public functions to create a table
    '**************************************************************************************************************
    Private Sub AddTables(ByVal strTab As String, ByVal strDesc As String, ByVal nType As SAPbobsCOM.BoUTBTableType)
        Dim oUserTablesMD As SAPbobsCOM.UserTablesMD
        Try

            oUserTablesMD = oApplication.Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserTables)
            'Adding Table
            If Not oUserTablesMD.GetByKey(strTab) Then
                oUserTablesMD.TableName = strTab
                oUserTablesMD.TableDescription = strDesc
                oUserTablesMD.TableType = nType
                If oUserTablesMD.Add <> 0 Then
                    Throw New Exception(oApplication.Company.GetLastErrorDescription)
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oUserTablesMD)
            oUserTablesMD = Nothing
            GC.WaitForPendingFinalizers()
            GC.Collect()
        End Try
    End Sub

    '*************************************************************************************************************
    'Type               : Private Function
    'Name               : AddFields
    'Parameter          : SstrTab As String,strCol As String,
    '                     strDesc As String,nType As Integer,i,nEditSize,nSubType As Integer
    'Return Value       : none
    'Author             : Manu
    'Created Dt         : 
    'Last Modified By   : 
    'Modified Dt        : 
    'Purpose            : Generic Function for adding all Fields in DB Tables. This function shall be called by 
    '                     public functions to create a Field
    '**************************************************************************************************************
    Private Sub AddFields(ByVal strTab As String, _
                            ByVal strCol As String, _
                                ByVal strDesc As String, _
                                    ByVal nType As SAPbobsCOM.BoFieldTypes, _
                                        Optional ByVal i As Integer = 0, _
                                            Optional ByVal nEditSize As Integer = 10, _
                                                Optional ByVal nSubType As SAPbobsCOM.BoFldSubTypes = 0, _
                                                    Optional ByVal Mandatory As SAPbobsCOM.BoYesNoEnum = SAPbobsCOM.BoYesNoEnum.tNO)
        Dim oUserFieldMD As SAPbobsCOM.UserFieldsMD
        Try

            If Not (strTab = "OPDN" Or strTab = "ORPD" Or strTab = "ORPC" Or strTab = "OIQR" Or strTab = "OJDT" Or strTab = "JDT1" Or strTab = "OACT" Or strTab = "OCRN") Then
                strTab = "@" + strTab
            End If

            If Not IsColumnExists(strTab, strCol) Then
                oUserFieldMD = oApplication.Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields)

                oUserFieldMD.Description = strDesc
                oUserFieldMD.Name = strCol
                oUserFieldMD.Type = nType
                oUserFieldMD.SubType = nSubType
                oUserFieldMD.TableName = strTab
                oUserFieldMD.EditSize = nEditSize
                oUserFieldMD.Mandatory = Mandatory
                If oUserFieldMD.Add <> 0 Then
                    Throw New Exception(oApplication.Company.GetLastErrorDescription)
                End If

                System.Runtime.InteropServices.Marshal.ReleaseComObject(oUserFieldMD)

            End If

        Catch ex As Exception
            Throw ex
        Finally
            oUserFieldMD = Nothing
            GC.WaitForPendingFinalizers()
            GC.Collect()
        End Try
    End Sub

    '*************************************************************************************************************
    'Type               : Private Function
    'Name               : AddFields
    'Parameter          : SstrTab As String,strCol As String,
    '                     strDesc As String,nType As Integer,i,nEditSize,nSubType As Integer
    'Return Value       : none
    'Author             : Manu
    'Created Dt         : 
    'Last Modified By   : 
    'Modified Dt        : 
    'Purpose            : Generic Function for adding all Fields in DB Tables. This function shall be called by 
    '                     public functions to create a Field
    '**************************************************************************************************************
    Public Sub addField(ByVal TableName As String, ByVal ColumnName As String, ByVal ColDescription As String, ByVal FieldType As SAPbobsCOM.BoFieldTypes, ByVal Size As Integer, ByVal SubType As SAPbobsCOM.BoFldSubTypes, ByVal ValidValues As String, ByVal ValidDescriptions As String, ByVal SetValidValue As String)
        Dim intLoop As Integer
        Dim strValue, strDesc As Array
        Dim objUserFieldMD As SAPbobsCOM.UserFieldsMD
        Try

            strValue = ValidValues.Split(Convert.ToChar(","))
            strDesc = ValidDescriptions.Split(Convert.ToChar(","))
            If (strValue.GetLength(0) <> strDesc.GetLength(0)) Then
                Throw New Exception("Invalid Valid Values")
            End If


            If (Not IsColumnExists(TableName, ColumnName)) Then
                objUserFieldMD = oApplication.Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields)
                objUserFieldMD.TableName = TableName
                objUserFieldMD.Name = ColumnName
                objUserFieldMD.Description = ColDescription
                objUserFieldMD.Type = FieldType
                If (FieldType <> SAPbobsCOM.BoFieldTypes.db_Numeric) Then
                    objUserFieldMD.Size = Size
                Else
                    objUserFieldMD.EditSize = Size
                End If
                objUserFieldMD.SubType = SubType
                objUserFieldMD.DefaultValue = SetValidValue
                For intLoop = 0 To strValue.GetLength(0) - 1
                    objUserFieldMD.ValidValues.Value = strValue(intLoop)
                    objUserFieldMD.ValidValues.Description = strDesc(intLoop)
                    objUserFieldMD.ValidValues.Add()
                Next
                If (objUserFieldMD.Add() <> 0) Then
                    MsgBox(oApplication.Company.GetLastErrorDescription)
                End If
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objUserFieldMD)
            Else
            End If

        Catch ex As Exception
            MsgBox(ex.Message)

        Finally
            objUserFieldMD = Nothing
            GC.WaitForPendingFinalizers()
            GC.Collect()

        End Try


    End Sub

    '*************************************************************************************************************
    'Type               : Private Function
    'Name               : IsColumnExists
    'Parameter          : ByVal Table As String, ByVal Column As String
    'Return Value       : Boolean
    'Author             : Manu
    'Created Dt         : 
    'Last Modified By   : 
    'Modified Dt        : 
    'Purpose            : Function to check if the Column already exists in Table
    '**************************************************************************************************************
    Private Function IsColumnExists(ByVal Table As String, ByVal Column As String) As Boolean
        Dim oRecordSet As SAPbobsCOM.Recordset

        Try
            strSQL = "SELECT COUNT(*) FROM CUFD WHERE ""TableID"" = '" & Table & "' AND ""AliasID"" = '" & Column & "'"
            oRecordSet = oApplication.Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oRecordSet.DoQuery(strSQL)

            If oRecordSet.Fields.Item(0).Value = 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw ex
        Finally
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecordSet)
            oRecordSet = Nothing
            GC.Collect()
        End Try
    End Function

    Private Sub AddKey(ByVal strTab As String, ByVal strColumn As String, ByVal strKey As String, ByVal i As Integer)
        Dim oUserKeysMD As SAPbobsCOM.UserKeysMD

        Try
            '// The meta-data object must be initialized with a
            '// regular UserKeys object
            oUserKeysMD = oApplication.Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserKeys)

            If Not oUserKeysMD.GetByKey("@" & strTab, i) Then

                '// Set the table name and the key name
                oUserKeysMD.TableName = strTab
                oUserKeysMD.KeyName = strKey

                '// Set the column's alias
                oUserKeysMD.Elements.ColumnAlias = strColumn
                oUserKeysMD.Elements.Add()
                oUserKeysMD.Elements.ColumnAlias = "RentFac"

                '// Determine whether the key is unique or not
                oUserKeysMD.Unique = SAPbobsCOM.BoYesNoEnum.tYES

                '// Add the key
                If oUserKeysMD.Add <> 0 Then
                    Throw New Exception(oApplication.Company.GetLastErrorDescription)
                End If

            End If

        Catch ex As Exception
            Throw ex

        Finally
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oUserKeysMD)
            oUserKeysMD = Nothing
            GC.Collect()
            GC.WaitForPendingFinalizers()
        End Try

    End Sub

    '********************************************************************
    'Type		            :   Function    
    'Name               	:	AddUDO
    'Parameter          	:   
    'Return Value       	:	Boolean
    'Author             	:	
    'Created Date       	:	
    'Last Modified By	    :	
    'Modified Date        	:	
    'Purpose             	:	To Add a UDO for Transaction Tables
    '********************************************************************
    Private Sub AddUDO(ByVal strUDO As String, ByVal strDesc As String, ByVal strTable As String, _
                                Optional ByVal sFind1 As String = "", Optional ByVal sFind2 As String = "", _
                                        Optional ByVal strChildTbl As String = "", Optional ByVal nObjectType As SAPbobsCOM.BoUDOObjType = SAPbobsCOM.BoUDOObjType.boud_Document)

        Dim oUserObjectMD As SAPbobsCOM.UserObjectsMD
        Try
            oUserObjectMD = oApplication.Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserObjectsMD)
            If oUserObjectMD.GetByKey(strUDO) = 0 Then
                oUserObjectMD.CanCancel = SAPbobsCOM.BoYesNoEnum.tYES
                oUserObjectMD.CanClose = SAPbobsCOM.BoYesNoEnum.tYES
                oUserObjectMD.CanCreateDefaultForm = SAPbobsCOM.BoYesNoEnum.tNO
                oUserObjectMD.CanDelete = SAPbobsCOM.BoYesNoEnum.tNO
                oUserObjectMD.CanFind = SAPbobsCOM.BoYesNoEnum.tYES

                If sFind1 <> "" And sFind2 <> "" Then
                    oUserObjectMD.FindColumns.ColumnAlias = sFind1
                    oUserObjectMD.FindColumns.Add()
                    oUserObjectMD.FindColumns.SetCurrentLine(1)
                    oUserObjectMD.FindColumns.ColumnAlias = sFind2
                    oUserObjectMD.FindColumns.Add()
                End If

                oUserObjectMD.CanLog = SAPbobsCOM.BoYesNoEnum.tNO
                oUserObjectMD.LogTableName = ""
                oUserObjectMD.CanYearTransfer = SAPbobsCOM.BoYesNoEnum.tNO
                oUserObjectMD.ExtensionName = ""

                If strChildTbl <> "" Then
                    oUserObjectMD.ChildTables.TableName = strChildTbl
                End If

                oUserObjectMD.ManageSeries = SAPbobsCOM.BoYesNoEnum.tNO
                oUserObjectMD.Code = strUDO
                oUserObjectMD.Name = strDesc
                oUserObjectMD.ObjectType = nObjectType
                oUserObjectMD.TableName = strTable

                If oUserObjectMD.Add() <> 0 Then
                    Throw New Exception(oApplication.Company.GetLastErrorDescription)
                End If
            End If

        Catch ex As Exception
            Throw ex

        Finally
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oUserObjectMD)
            oUserObjectMD = Nothing
            GC.WaitForPendingFinalizers()
            GC.Collect()
        End Try

    End Sub

#End Region

#Region "Public Functions"
    '*************************************************************************************************************
    'Type               : Public Function
    'Name               : CreateTables
    'Parameter          : 
    'Return Value       : none
    'Author             : Manu
    'Created Dt         : 
    'Last Modified By   : 
    'Modified Dt        : 
    'Purpose            : Creating Tables by calling the AddTables & AddFields Functions
    '**************************************************************************************************************
    Public Sub CreateTables()
        Try

            oApplication.SBO_Application.StatusBar.SetText("Initializing Database...", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning)
            oApplication.Company.StartTransaction()

            '---- User Defined Fields           
            addField("OJDT", "Import", "Import", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_Address, "Y,N", "Yes,No", "N")
            AddFields("OJDT", "PRRN", "Prime RRN", SAPbobsCOM.BoFieldTypes.db_Alpha, , 12) 'UDF1 
            AddFields("OJDT", "PRC", "Prime reason code", SAPbobsCOM.BoFieldTypes.db_Alpha, , 4) 'UDF2 
            'AddFields("OJDT", "PTSN", "Prm Trxn Serial Number", SAPbobsCOM.BoFieldTypes.db_Alpha, , 15) 'UDF3 
            'AddFields("OJDT", "ATID", "AEGNS Trxn ID (TID)", SAPbobsCOM.BoFieldTypes.db_Alpha, , 15) 'UDF4 
            AddFields("OJDT", "PPS", "Prime product serno", SAPbobsCOM.BoFieldTypes.db_Alpha, , 6) 'UDF5  
            AddFields("OJDT", "PEN", "Prime Entity Name", SAPbobsCOM.BoFieldTypes.db_Alpha, , 40) 'UDF6  
            AddFields("OJDT", "PBANP", "Bank act no in Primeq", SAPbobsCOM.BoFieldTypes.db_Alpha, , 20) 'UDF7  
            AddFields("OJDT", "PBAN", "Prime Bank AC Name", SAPbobsCOM.BoFieldTypes.db_Alpha, , 40) 'UDF8  
            AddFields("OJDT", "PIBAN", "Prime IBAN account", SAPbobsCOM.BoFieldTypes.db_Alpha, , 25) 'UDF9  
            AddFields("OJDT", "PTA", "Prime Trxn Amt", SAPbobsCOM.BoFieldTypes.db_Alpha, , 25) 'UDF10  
            AddFields("OJDT", "PTC", "Trxn Numeric currency", SAPbobsCOM.BoFieldTypes.db_Alpha, , 25) 'UDF11  
            addField("OJDT", "PTD", "Trxn Date", SAPbobsCOM.BoFieldTypes.db_Date, 10, SAPbobsCOM.BoFldSubTypes.st_None, "", "", "") 'UDF12 

            AddFields("OJDT", "PTS", "Prime trxn Status", SAPbobsCOM.BoFieldTypes.db_Alpha, , 15) 'UDF3(1) 
            AddFields("OJDT", "PE", "Prime Entity", SAPbobsCOM.BoFieldTypes.db_Alpha, , 40) 'UDF4(1)  

            '---- User Defined Fields-----JDT1
            AddFields("JDT1", "PRRN", "Prime RRN", SAPbobsCOM.BoFieldTypes.db_Alpha, , 12) 'UDF1 
            AddFields("JDT1", "PRC", "Prime reason code", SAPbobsCOM.BoFieldTypes.db_Alpha, , 4) 'UDF2    
            AddFields("JDT1", "PTS", "Prime trxn Status", SAPbobsCOM.BoFieldTypes.db_Alpha, , 15) 'UDF3(1) 
            AddFields("JDT1", "PE", "Prime Entity", SAPbobsCOM.BoFieldTypes.db_Alpha, , 40) 'UDF4(1)  
            AddFields("JDT1", "PPS", "Prime product serno", SAPbobsCOM.BoFieldTypes.db_Alpha, , 6) 'UDF5  
            AddFields("JDT1", "PEN", "Prime Entity Name", SAPbobsCOM.BoFieldTypes.db_Alpha, , 40) 'UDF6  
            AddFields("JDT1", "PBANP", "Bank act no in Primeq", SAPbobsCOM.BoFieldTypes.db_Alpha, , 20) 'UDF7  
            AddFields("JDT1", "PBAN", "Prime Bank AC Name", SAPbobsCOM.BoFieldTypes.db_Alpha, , 40) 'UDF8  
            AddFields("JDT1", "PIBAN", "Prime IBAN account", SAPbobsCOM.BoFieldTypes.db_Alpha, , 25) 'UDF9  
            AddFields("JDT1", "PTA", "Prime Trxn Amt", SAPbobsCOM.BoFieldTypes.db_Alpha, , 25) 'UDF10  
            AddFields("JDT1", "PTC", "Trxn Numeric currency", SAPbobsCOM.BoFieldTypes.db_Alpha, , 25) 'UDF11  
            addField("JDT1", "PTD", "Trxn Date", SAPbobsCOM.BoFieldTypes.db_Date, 10, SAPbobsCOM.BoFldSubTypes.st_None, "", "", "") 'UDF12 

            'OACT(Accounts)
            addField("OACT", "IsBulk", "Is Bulk", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_Address, "Y,N", "Yes,No", "N")

            'OCRN(Currency)
            AddFields("OCRN", "ISO", "Int Code(Num)", SAPbobsCOM.BoFieldTypes.db_Alpha, , 4)

            'JE In Bound Table
            AddTables("Z_AMX_IBND", "In Bound", SAPbobsCOM.BoUTBTableType.bott_MasterData)
            AddTables("Z_AMX_IBD1", "In Bound Lines", SAPbobsCOM.BoUTBTableType.bott_MasterDataLines)

            AddFields("Z_AMX_IBND", "Type", "Type", SAPbobsCOM.BoFieldTypes.db_Alpha, , 50)
            AddFields("Z_AMX_IBND", "OPath", "Open Path", SAPbobsCOM.BoFieldTypes.db_Alpha, , 100)
            AddFields("Z_AMX_IBND", "SPath", "Success Path", SAPbobsCOM.BoFieldTypes.db_Alpha, , 100)
            AddFields("Z_AMX_IBND", "FPath", "Failed Path", SAPbobsCOM.BoFieldTypes.db_Alpha, , 100)
            AddFields("Z_AMX_IBND", "LPath", "Log Path", SAPbobsCOM.BoFieldTypes.db_Alpha, , 100)
            AddFields("Z_AMX_IBND", "Series", "Series", SAPbobsCOM.BoFieldTypes.db_Alpha, , 10)
            AddFields("Z_AMX_IBND", "DSUSACCT", "Debit Sus ACCT", SAPbobsCOM.BoFieldTypes.db_Alpha, , 30)
            AddFields("Z_AMX_IBND", "CSUSACCT", "Credit Sus ACCT", SAPbobsCOM.BoFieldTypes.db_Alpha, , 30)

            'Log Table
            AddTables("Z_AMX_IBND_Log", "InBound Log", SAPbobsCOM.BoUTBTableType.bott_NoObject)

            'InBound Log Table
            AddFields("Z_AMX_IBND_Log", "Type", "Type", SAPbobsCOM.BoFieldTypes.db_Alpha, , 50)
            addField("@Z_AMX_IBND_Log", "FileDate", "File Date", SAPbobsCOM.BoFieldTypes.db_Date, 10, SAPbobsCOM.BoFldSubTypes.st_None, "", "", "")
            AddFields("Z_AMX_IBND_Log", "RecNum", "Reference Num", SAPbobsCOM.BoFieldTypes.db_Alpha, , 20)
            AddFields("Z_AMX_IBND_Log", "SAPRef", "SAP Reference No", SAPbobsCOM.BoFieldTypes.db_Alpha, , 10)
            
            '---- User Defined Object
            CreateUDO()

            If oApplication.Company.InTransaction() Then
                oApplication.Company.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit)
            End If
            oApplication.SBO_Application.StatusBar.SetText("Database creation completed...", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning)

        Catch ex As Exception
            If oApplication.Company.InTransaction() Then
                oApplication.Company.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack)
            End If
            Throw ex
        Finally
            GC.Collect()
            GC.WaitForPendingFinalizers()
        End Try
    End Sub

    Public Sub CreateUDO()
        Try
            'Add UDO
            AddUDO("Z_AMX_IBND", "In Bound", "Z_AMX_IBND", "Code", "U_Type", "", SAPbobsCOM.BoUDOObjType.boud_MasterData)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    
#End Region

End Class

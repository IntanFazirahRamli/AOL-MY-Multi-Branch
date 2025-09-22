Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Drawing


Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Collections.Generic
Imports System.Web.UI.WebControls
Imports System.Web

' Include this namespace if it is not already there

Imports System.Globalization
Imports System.Threading

Partial Class AccountIDRelocation
    Inherits System.Web.UI.Page


    Public rcno As String

    'Public Sub MakeMeNull()
    '    lblMessage.Text = ""
    '    lblAlert.Text = ""
    '    txtMode.Text = ""
    '    txtCOACode.Text = ""
    '    txtDescription.Text = ""

    '    txtArea.Text = ""
    '    txtFunction.Text = ""
    '    txtOrganisation.Text = ""
    '    txtServiceType.Text = ""
    '    txtCostCenter.Text = ""

    '    txtRcno.Text = ""
    '    'txtCreatedBy.Text = ""
    '    txtCreatedOn.Text = ""
    '    ' txtLastModifiedBy.Text = ""
    '    txtLastModifiedOn.Text = ""
    '    ddlCompanyGrp.SelectedIndex = 0
    '    ddlGLType.SelectedIndex = 0
    'End Sub

    'Private Sub EnableControls()
    '    btnSave.Enabled = False
    '    btnSave.ForeColor = System.Drawing.Color.Gray
    '    btnCancel.Enabled = False
    '    btnCancel.ForeColor = System.Drawing.Color.Gray

    '    btnADD.Enabled = True
    '    btnADD.ForeColor = System.Drawing.Color.Black

    '    btnEdit.Enabled = False
    '    btnEdit.ForeColor = System.Drawing.Color.Gray
    '    btnDelete.Enabled = False
    '    btnDelete.ForeColor = System.Drawing.Color.Gray

    '    btnQuit.Enabled = True
    '    btnQuit.ForeColor = System.Drawing.Color.Black
    '    btnPrint.Enabled = True
    '    btnPrint.ForeColor = System.Drawing.Color.Black
    '    txtCOACode.Enabled = False
    '    txtDescription.Enabled = False
    '    txtArea.Enabled = False
    '    txtFunction.Enabled = False
    '    txtOrganisation.Enabled = False
    '    txtServiceType.Enabled = False
    '    txtCostCenter.Enabled = False
    '    ddlCompanyGrp.Enabled = False
    '    ddlGLType.Enabled = False

    '    AccessControl()
    'End Sub

    'Private Sub DisableControls()
    '    btnSave.Enabled = True
    '    btnSave.ForeColor = System.Drawing.Color.Black
    '    btnCancel.Enabled = True
    '    btnCancel.ForeColor = System.Drawing.Color.Black

    '    btnADD.Enabled = False
    '    btnADD.ForeColor = System.Drawing.Color.Gray

    '    btnEdit.Enabled = False
    '    btnEdit.ForeColor = System.Drawing.Color.Gray

    '    btnDelete.Enabled = False
    '    btnDelete.ForeColor = System.Drawing.Color.Gray

    '    btnQuit.Enabled = False
    '    btnQuit.ForeColor = System.Drawing.Color.Gray

    '    btnPrint.Enabled = False
    '    btnPrint.ForeColor = System.Drawing.Color.Gray

    '    txtCOACode.Enabled = True
    '    txtDescription.Enabled = True
    '    txtArea.Enabled = True
    '    txtFunction.Enabled = True
    '    txtOrganisation.Enabled = True
    '    txtServiceType.Enabled = True
    '    txtCostCenter.Enabled = True
    '    ddlCompanyGrp.Enabled = True
    '    ddlGLType.Enabled = True

    '    AccessControl()
    'End Sub

   

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Dim UserID As String = Convert.ToString(Session("UserID"))
                txtCreatedBy.Text = UserID
                txtLastModifiedBy.Text = UserID

                txtContactTypeRelocate.Text = Session("ContactTypetoRelocate")
                txtServiceNameRelocate.Text = Session("ServiceNametoRelocate")
                txtLocationIDRelocate.Text = Session("LocationIDtoRelocate")
                txtCreatedOn.Attributes.Add("readonly", "readonly")

                If txtContactTypeRelocate.Text = "CORPORATE" Then
                    txtAccountType.Text = "COMPANY"
                Else
                    txtAccountType.Text = "PERSON"
                End If

                txtFrom.Text = Session("relocatefrom")
                txtRcnoContact.Text = Session("rcno")

                Session.Remove("relocatefrom")
                Session.Remove("rcno")

                txtUpdated.Text = "N"
                PopulateGrid()

                'SqlDSContractDetail.SelectCommand = "SELECT distinct(b.ContractNo), b.LocationId,  a.ContractGroup, a.ServiceAddress from tblContract a, tblServiceRecord b where a.ContractNo = b.ContractNo and LocationId =@LocationId order by b.ContractNo"
                'SqlDSContractDetail.DataBind()

                'SqlDSService.SelectCommand = "SELECT ContractNo, RecordNo, LocationId, SchServiceDate, Rcno from tblServiceRecord where LocationId = @LocationId order by ContractNo, RecordNo"
                'SqlDSService.DataBind()

                'SqlDSInvoice.SelectCommand = "SELECT b.SubCode, b.CostCode, b.RefType, b.InvoiceNumber, b.LocationId, a.DocType, a.SalesDate from tblSales a, tblSalesDetail b where a.InvoiceNumber = b.InvoiceNumber and b.LocationId = @LocationId and a.DocType = 'ARIN' order by b.CostCode, a.InvoiceNumber"
                'SqlDSInvoice.DataBind()

                'SqlDSReceipts.SelectCommand = "SELECT b.RefType, a.ReceiptNumber, a.ReceiptFrom, a.Appliedbase, a.ReceiptDate from tblrecv a, tblrecvdet b where a.ReceiptNumber = b.ReceiptNumber and b.RefType in (SELECT InvoiceNumber from tblSalesDetail where LocationId = @LocationId) order by a.ReceiptNumber, b.RefType"
                'SqlDSReceipts.DataBind()

                'SqlDSCNDN.SelectCommand = "SELECT b.SubCode, b.InvoiceNumber, b.LocationId, a.DocType, b.RefType, a.SalesDate, b.SourceInvoice from tblSales a, tblSalesDetail b where a.InvoiceNumber = b.InvoiceNumber and ((a.DocType = 'ARCN') or (a.DocType = 'ARDN')) and  b.LocationId = @LocationId "
                'SqlDSCNDN.DataBind()

                'SqlDSJournals.SelectCommand = "SELECT b.RefType, b.VoucherNumber, b.CreditBase, a.JournalDate from tbljrnv a, tbljrnvdet b where a.VoucherNumber= b.VoucherNumber and b.RefType in (SELECT InvoiceNumber from tblSalesDetail where LocationId = @LocationId) order by a.VoucherNumber, b.RefType"
                'SqlDSJournals.DataBind()
            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("RELOCATE - " + Session("UserID"), "Page_Load", ex.Message.ToString, "")
            Exit Sub
        End Try

    End Sub

    Private Sub PopulateGrid()
        Try
            SqlDSContractDetail.SelectCommand = "SELECT distinct(b.ContractNo), b.LocationId,  a.ContractGroup, a.ServiceAddress from tblContract a, tblServiceRecord b where a.ContractNo = b.ContractNo and LocationId ='" & txtLocationIDRelocate.Text & "' order by b.ContractNo"
            SqlDSContractDetail.DataBind()

            SqlDSService.SelectCommand = "SELECT ContractNo, RecordNo, LocationId, SchServiceDate, Rcno from tblServiceRecord where LocationId = '" & txtLocationIDRelocate.Text & "' order by ContractNo, RecordNo"
            SqlDSService.DataBind()

            SqlDSInvoice.SelectCommand = "SELECT b.SubCode, b.CostCode, b.RefType, b.InvoiceNumber, b.LocationId, a.DocType, a.SalesDate from tblSales a, tblSalesDetail b where a.InvoiceNumber = b.InvoiceNumber and b.LocationId = '" & txtLocationIDRelocate.Text & "' and a.DocType = 'ARIN' order by b.CostCode, a.InvoiceNumber"
            SqlDSInvoice.DataBind()

            SqlDSReceipts.SelectCommand = "SELECT b.RefType, a.ReceiptNumber, a.ReceiptFrom, a.Appliedbase, a.ReceiptDate from tblrecv a, tblrecvdet b where a.ReceiptNumber = b.ReceiptNumber and b.RefType in (SELECT InvoiceNumber from tblSalesDetail where LocationId = '" & txtLocationIDRelocate.Text & "') order by a.ReceiptNumber, b.RefType"
            SqlDSReceipts.DataBind()

            SqlDSCNDN.SelectCommand = "SELECT b.SubCode, b.InvoiceNumber, b.LocationId, a.DocType, b.RefType, a.SalesDate, b.SourceInvoice from tblSales a, tblSalesDetail b where a.InvoiceNumber = b.InvoiceNumber and ((a.DocType = 'ARCN') or (a.DocType = 'ARDN')) and  b.LocationId = '" & txtLocationIDRelocate.Text & "'"
            SqlDSCNDN.DataBind()

            SqlDSJournals.SelectCommand = "SELECT b.RefType, b.VoucherNumber, b.CreditBase, a.JournalDate from tbljrnv a, tbljrnvdet b where a.VoucherNumber= b.VoucherNumber and b.RefType in (SELECT InvoiceNumber from tblSalesDetail where LocationId = '" & txtLocationIDRelocate.Text & "') order by a.VoucherNumber, b.RefType"
            SqlDSJournals.DataBind()
        Catch ex As Exception
            InsertIntoTblWebEventLog("RELOCATE - " + Session("UserID"), "PopulateGrid", ex.Message.ToString, txtLocationIDRelocate.Text & " to " & txtLocationIDRelocateNew.Text)
            Exit Sub
        End Try
    End Sub
    Private Sub AccessControl()
        Try
            ' '''''''''''''''''''Access Control 
            'If Session("SecGroupAuthority") <> "ADMINISTRATOR" Then
            '    Dim conn As MySqlConnection = New MySqlConnection()
            '    Dim command As MySqlCommand = New MySqlCommand

            '    conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            '    conn.Open()

            '    command.CommandType = CommandType.Text
            '    'command.CommandText = "SELECT x0209,  x0209Add, x0209Edit, x0209Delete, x0209Print FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"
            '    command.CommandText = "SELECT x0209,  x0209Add, x0209Edit, x0209Delete FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"

            '    command.Connection = conn

            '    Dim dr As MySqlDataReader = command.ExecuteReader()
            '    Dim dt As New DataTable
            '    dt.Load(dr)
            '    conn.Close()

            '    If dt.Rows.Count > 0 Then
            '        If Not IsDBNull(dt.Rows(0)("x0209")) Then
            '            If String.IsNullOrEmpty(Convert.ToBoolean(dt.Rows(0)("x0209"))) = False Then
            '                If Convert.ToBoolean(dt.Rows(0)("x0209")) = False Then
            '                    Response.Redirect("Home.aspx")
            '                End If
            '            End If
            '        End If

            '        If Not IsDBNull(dt.Rows(0)("x0209Add")) Then
            '            If String.IsNullOrEmpty(dt.Rows(0)("x0209Add")) = False Then
            '                Me.btnADD.Enabled = dt.Rows(0)("x0209Add").ToString()
            '            End If
            '        End If

            '        If txtMode.Text = "View" Then
            '            If Not IsDBNull(dt.Rows(0)("x0209Edit")) Then
            '                If String.IsNullOrEmpty(dt.Rows(0)("x0209Edit")) = False Then
            '                    Me.btnEdit.Enabled = dt.Rows(0)("x0209Edit").ToString()
            '                End If
            '            End If

            '            If Not IsDBNull(dt.Rows(0)("x0209Delete")) Then
            '                If String.IsNullOrEmpty(dt.Rows(0)("x0209Delete")) = False Then
            '                    Me.btnDelete.Enabled = dt.Rows(0)("x0209Delete").ToString()
            '                End If
            '            End If
            '        Else
            '            Me.btnEdit.Enabled = False
            '            Me.btnDelete.Enabled = False
            '        End If

            '        If btnADD.Enabled = True Then
            '            btnADD.ForeColor = System.Drawing.Color.Black
            '        Else
            '            btnADD.ForeColor = System.Drawing.Color.Gray
            '        End If


            '        If btnEdit.Enabled = True Then
            '            btnEdit.ForeColor = System.Drawing.Color.Black
            '        Else
            '            btnEdit.ForeColor = System.Drawing.Color.Gray
            '        End If

            '        If btnDelete.Enabled = True Then
            '            btnDelete.ForeColor = System.Drawing.Color.Black
            '        Else
            '            btnDelete.ForeColor = System.Drawing.Color.Gray
            '        End If


            '        If btnPrint.Enabled = True Then
            '            btnPrint.ForeColor = System.Drawing.Color.Black
            '        Else
            '            btnPrint.ForeColor = System.Drawing.Color.Gray
            '        End If


            '    End If
            'End If

            ' '''''''''''''''''''Access Control 
        Catch ex As Exception
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
            lblAlert.Text = ex.Message
        End Try
    End Sub

    Public Sub PopulateDropDownList(ByVal query As String, ByVal textField As String, ByVal valueField As String, ByVal ddl As DropDownList)
        Dim con As MySqlConnection = New MySqlConnection()

        con.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        'Using con As New MySqlConnection(constr)
        Using cmd As New MySqlCommand(query)
            cmd.CommandType = CommandType.Text
            cmd.Connection = con
            con.Open()
            ddl.DataSource = cmd.ExecuteReader()
            ddl.DataTextField = textField.Trim()
            ddl.DataValueField = valueField.Trim()
            ddl.DataBind()
            con.Close()
        End Using
        'End Using
    End Sub



    Protected Sub btnQuit_Click(sender As Object, e As EventArgs) Handles btnQuit.Click
        Try
            'Me.ClientScript.RegisterClientScriptBlock(Me.[GetType](), "Close", "window.close()", True)
            'Response.Redirect("Home.aspx")
            'If Session("servicefrom") = "contactC" Or Session("relocatefrom") = "relocateC" Then
            If txtFrom.Text = "relocateC" Then
                Session("relocatefrom") = "relocateC"
                Session("rcno") = txtRcnoContact.Text
                Response.Redirect("Company.aspx")
            ElseIf txtFrom.Text = "relocateP" Then
                Session("relocatefrom") = "relocateP"
                Session("rcno") = txtRcnoContact.Text
                Response.Redirect("Person.aspx")
            End If
        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString
            InsertIntoTblWebEventLog("RELOCATE - " + Session("UserID"), "btnQuit_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub


    Protected Sub btnSaveRelocate_Click(sender As Object, e As EventArgs) Handles btnSaveRelocate.Click

        Try
            lblAlert.Text = ""

            If txtUpdated.Text = "Y" Then
                Exit Sub

            End If
            'UpdateRelocate()

            UpdateRelocateNew()

        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString
            InsertIntoTblWebEventLog("RELOCATE - " + Session("UserID"), "btnSaveRelocate_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub


    Private Sub UpdateRelocateNew()

        Dim ExecutingModule As String
        ExecutingModule = ""
        Try


            If String.IsNullOrEmpty(txtAccountIDRelocate.Text.Trim) = True Then
                lblAlert.Text = "Existing Account ID cannot be Emplty"
                txtAccountIDRelocate.Focus()
                Exit Sub
            End If

            If txtLocationIDRelocate.Text.Trim = txtLocationIDRelocateNew.Text.Trim Then
                lblAlert.Text = "Existing Location ID and New Location IDs are same"
                Exit Sub
            End If


            generateLocationIdRelocate()

            Dim command As MySqlCommand = New MySqlCommand
            command.CommandType = CommandType.Text

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            If conn.State = ConnectionState.Open Then
                conn.Close()
                conn.Dispose()
            End If
            conn.Open()

            Dim commandUpdateRelocate As MySqlCommand = New MySqlCommand

            commandUpdateRelocate.CommandType = CommandType.StoredProcedure
            commandUpdateRelocate.CommandText = "UpdateRelocateNew"

            commandUpdateRelocate.Parameters.Clear()
            commandUpdateRelocate.Parameters.AddWithValue("@pr_LocationId", txtLocationIDRelocate.Text.Trim)
            commandUpdateRelocate.Parameters.AddWithValue("@pr_LocationIdNew", txtLocationIDRelocateNew.Text.Trim)
            commandUpdateRelocate.Parameters.AddWithValue("@pr_AccountId", txtAccountIDRelocate.Text.Trim)
            commandUpdateRelocate.Parameters.AddWithValue("@pr_ContactType", txtAccountType.Text.Trim)

            'commandUpdateRelocate.Parameters.AddWithValue("@pr_ContractNo", "")
            'commandUpdateRelocate.Parameters.AddWithValue("@pr_ServiceRecordNo", "")
            'commandUpdateRelocate.Parameters.AddWithValue("@pr_InvoiceNumber", "")
            commandUpdateRelocate.Parameters.AddWithValue("@pr_LocationNo", txtLocationNo.Text)

            If txtContactTypeRelocate.Text = "CORPORATE" And txtAccountType.Text = "COMPANY" Then
                commandUpdateRelocate.Parameters.AddWithValue("@pr_Module", "CompLoc")
            ElseIf txtContactTypeRelocate.Text = "RESIDENTIAL" And txtAccountType.Text = "PERSON" Then
                commandUpdateRelocate.Parameters.AddWithValue("@pr_Module", "PersLoc")
            ElseIf txtContactTypeRelocate.Text = "CORPORATE" And txtAccountType.Text = "PERSON" Then
                commandUpdateRelocate.Parameters.AddWithValue("@pr_Module", "CorpToRes")
            ElseIf txtContactTypeRelocate.Text = "RESIDENTIAL" And txtAccountType.Text = "COMPANY" Then
                commandUpdateRelocate.Parameters.AddWithValue("@pr_Module", "ResToCorp")
            End If

            commandUpdateRelocate.Parameters.AddWithValue("@pr_CreatedBy", txtCreatedBy.Text)
            commandUpdateRelocate.Connection = conn
            commandUpdateRelocate.ExecuteScalar()


            lblMessage.Text = "Account Relocation Complete"

            mdlCompleteConfirm.Show()


            conn.Close()
            conn.Dispose()
            command.Dispose()

            commandUpdateRelocate.Dispose()


            PopulateGrid()
            txtUpdated.Text = "Y"
        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString
            InsertIntoTblWebEventLog("RELOCATE - " + Session("UserID"), "updateRelocate", ex.Message.ToString, ExecutingModule)
            Exit Sub
        End Try
    End Sub


    Private Sub UpdateRelocate()

        Dim ExecutingModule As String
        ExecutingModule = ""
        Try


            If String.IsNullOrEmpty(txtAccountIDRelocate.Text.Trim) = True Then
                lblAlert.Text = "Existing Account ID cannot be Emplty"
                txtAccountIDRelocate.Focus()
                Exit Sub
            End If

            If txtLocationIDRelocate.Text.Trim = txtLocationIDRelocateNew.Text.Trim Then
                lblAlert.Text = "Existing Location ID and New Location IDs are same"
                Exit Sub
            End If

            Dim command As MySqlCommand = New MySqlCommand
            command.CommandType = CommandType.Text

            Dim commandUpdateRelocate As MySqlCommand = New MySqlCommand

            commandUpdateRelocate.CommandType = CommandType.StoredProcedure
            commandUpdateRelocate.CommandText = "UpdateRelocate"

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            If conn.State = ConnectionState.Open Then
                conn.Close()
                conn.Dispose()
            End If
            conn.Open()

            InsertIntoTblWebEventLog("GenerateLocationIdRelocate : " + Session("UserID"), "btnSaveRelocate_Click", "", ExecutingModule)

            generateLocationIdRelocate()

            Dim qry As String = ""
            Dim qry1 As String = ""

            'ExecutingModule = "generateLocationIdRelocate"

            'If txtContactTypeRelocate.Text = "CORPORATE" And txtAccountType.Text = "COMPANY" Then
            '    ExecutingModule = "CORPORATE"
            '    qry = "Update tblCompanyLocation set AccountId = @AccountId, LocationId= @LocationIdNew, LocationNo=@LocationNo  "
            '    qry = qry + " where LocationID =@LocationID; "

            '    command.CommandText = qry
            '    command.Parameters.Clear()
            '    command.Parameters.AddWithValue("@LocationId", txtLocationIDRelocate.Text)
            '    command.Parameters.AddWithValue("@LocationIdNew", txtLocationIDRelocateNew.Text)
            '    command.Parameters.AddWithValue("@AccountId", txtAccountIDRelocate.Text)
            '    command.Parameters.AddWithValue("@LocationNo", txtLocationNo.Text)
            '    command.Connection = conn
            '    command.ExecuteNonQuery()
            '    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CHNGACCT", txtLocationIDRelocate.Text, "CORP", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")), 0, 0, 0, txtAccountIDRelocate.Text, "", txtLocationIDRelocateNew.Text)

            'ElseIf txtContactTypeRelocate.Text = "RESIDENTIAL" And txtAccountType.Text = "PERSON" Then
            '    ExecutingModule = "RESIDENTIAL"
            '    qry = "Update tblPersonLocation set AccountId = @AccountId, LocationId= @LocationIdNew, LocationNo=@LocationNo  "
            '    qry = qry + " where LocationID =@LocationID; "

            '    command.CommandText = qry
            '    command.Parameters.Clear()
            '    command.Parameters.AddWithValue("@LocationId", txtLocationIDRelocate.Text)
            '    command.Parameters.AddWithValue("@LocationIdNew", txtLocationIDRelocateNew.Text)
            '    command.Parameters.AddWithValue("@AccountId", txtAccountIDRelocate.Text)
            '    command.Parameters.AddWithValue("@LocationNo", txtLocationNo.Text)
            '    command.Connection = conn
            '    command.ExecuteNonQuery()
            '    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CHNGACCT", txtLocationIDRelocate.Text, "PERS", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")), 0, 0, 0, txtAccountIDRelocate.Text, "", txtLocationIDRelocateNew.Text)

            'End If




            'If txtContactTypeRelocate.Text = "CORPORATE" And txtAccountType.Text = "PERSON" Then

            '    qry = "INSERT INTO tblpersonlocation "
            '    qry = qry + " (PersonID,Location,BranchID,Description,ContactPerson,Address1,Telephone,Mobile,Email,CreatedBy,"
            '    qry = qry + " CreatedOn,LastModifiedBy,LastModifiedOn,AddBlock,AddNos,AddFloor,AddUnit,AddBuilding,AddStreet,AddCity,"
            '    qry = qry + " AddState,AddCountry,AddPostal,LocateGrp,Fax,AccountID,LocationID,LocationPrefix,LocationNo,ServiceName,"
            '    qry = qry + " Contact1Position,Telephone2,ContactPerson2,Contact2Position,Contact2Tel,Contact2Fax,Contact2Tel2,Contact2Mobile,Contact2Email,ServiceAddress,"
            '    qry = qry + " ServiceLocationGroup,BillingNameSvc,BillAddressSvc,BillStreetSvc,BillBuildingSvc,BillCitySvc,BillStateSvc,BillCountrySvc,BillPostalSvc,BillContact1Svc,"
            '    qry = qry + " BillPosition1Svc,BillTelephone1Svc,BillFax1Svc,Billtelephone12Svc,BillMobile1Svc,BillEmail1Svc,BillContact2Svc,BillPosition2Svc,BillTelephone2Svc,BillFax2Svc,"
            '    qry = qry + " Billtelephone22Svc,BillMobile2Svc,BillEmail2Svc,InChargeIDSvc,ARTERMSvc,SalesManSvc,SendServiceReportTo1,SendServiceReportTo2,ContractGroup,AccountNo,"
            '    qry = qry + " Comments,PersonGroupD,InActiveD,Industry,MarketSegmentID,DefaultInvoiceFormat,ServiceEmailCC)"
            '    qry = qry + " SELECT "
            '    qry = qry + " CompanyID,Location,BranchID,Description,ContactPerson,Address1,Telephone,Mobile,Email,CreatedBy,"
            '    qry = qry + " CreatedOn,LastModifiedBy,LastModifiedOn,AddBlock,AddNos,AddFloor,AddUnit,AddBuilding,AddStreet,AddCity,"
            '    qry = qry + " AddState, AddCountry, AddPostal, LocateGrp, Fax, '" & txtAccountIDRelocate.Text & "', '" & txtLocationIDRelocateNew.Text & "',  LocationPrefix, '" & txtLocationNo.Text & "', ServiceName,"
            '    qry = qry + " Contact1Position, Telephone2, ContactPerson2, Contact2Position, Contact2Tel, Contact2Fax, Contact2Tel2, Contact2Mobile, Contact2Email, ServiceAddress,"
            '    qry = qry + " ServiceLocationGroup, BillingNameSvc, BillAddressSvc, BillStreetSvc, BillBuildingSvc, BillCitySvc, BillStateSvc, BillCountrySvc, BillPostalSvc, BillContact1Svc,"
            '    qry = qry + " BillPosition1Svc, BillTelephone1Svc, BillFax1Svc, Billtelephone12Svc, BillMobile1Svc, BillEmail1Svc, BillContact2Svc, BillPosition2Svc, BillTelephone2Svc, BillFax2Svc,"
            '    qry = qry + " Billtelephone22Svc,BillMobile2Svc,BillEmail2Svc,InChargeIDSvc,ARTERMSvc,SalesManSvc,SendServiceReportTo1,SendServiceReportTo2,ContractGroup,AccountNo,"
            '    qry = qry + " Comments, CompanyGroupD, InActiveD, Industry, MarketSegmentID, DefaultInvoiceFormat, ServiceEmailCC"
            '    qry = qry + " From tblCompanyLocation"
            '    qry = qry + " where LocationID = '" & txtLocationIDRelocate.Text & "'"


            '    command.CommandText = qry
            '    command.Parameters.Clear()

            '    command.Connection = conn
            '    command.ExecuteNonQuery()


            '    ''''''''''''''''''''''''''''''
            '    qry1 = "DELETE from tblCompanyLocation "
            '    qry1 = qry1 + " where LocationID = '" & txtLocationIDRelocate.Text & "'"

            '    command.CommandText = qry1
            '    command.Parameters.Clear()

            '    command.Connection = conn
            '    command.ExecuteNonQuery()
            '    '''''''''''''''''''''''''''''''''

            '    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CHNGACCT", txtLocationIDRelocate.Text, "CORP", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")), 0, 0, 0, txtAccountIDRelocate.Text, "", txtLocationIDRelocateNew.Text)

            'ElseIf txtContactTypeRelocate.Text = "RESIDENTIAL" And txtAccountType.Text = "COMPANY" Then

            '    qry = "INSERT INTO tblCompanylocation "
            '    qry = qry + " (CompanyID,Location,BranchID,Description,ContactPerson,Address1,Telephone,Mobile,Email,CreatedBy,"
            '    qry = qry + " CreatedOn,LastModifiedBy,LastModifiedOn,AddBlock,AddNos,AddFloor,AddUnit,AddBuilding,AddStreet,AddCity,"
            '    qry = qry + " AddState,AddCountry,AddPostal,LocateGrp,Fax,AccountID,LocationID,LocationPrefix,LocationNo,ServiceName,"
            '    qry = qry + " Contact1Position,Telephone2,ContactPerson2,Contact2Position,Contact2Tel,Contact2Fax,Contact2Tel2,Contact2Mobile,Contact2Email,ServiceAddress,"
            '    qry = qry + " ServiceLocationGroup,BillingNameSvc,BillAddressSvc,BillStreetSvc,BillBuildingSvc,BillCitySvc,BillStateSvc,BillCountrySvc,BillPostalSvc,BillContact1Svc,"
            '    qry = qry + " BillPosition1Svc,BillTelephone1Svc,BillFax1Svc,Billtelephone12Svc,BillMobile1Svc,BillEmail1Svc,BillContact2Svc,BillPosition2Svc,BillTelephone2Svc,BillFax2Svc,"
            '    qry = qry + " Billtelephone22Svc,BillMobile2Svc,BillEmail2Svc,InChargeIDSvc,ARTERMSvc,SalesManSvc,SendServiceReportTo1,SendServiceReportTo2,ContractGroup,AccountNo,"
            '    qry = qry + " Comments,CompanyGroupD,InActiveD,Industry,MarketSegmentID,DefaultInvoiceFormat,ServiceEmailCC)"
            '    qry = qry + " SELECT "
            '    qry = qry + " PersonID,Location,BranchID,Description,ContactPerson,Address1,Telephone,Mobile,Email,CreatedBy,"
            '    qry = qry + " CreatedOn,LastModifiedBy,LastModifiedOn,AddBlock,AddNos,AddFloor,AddUnit,AddBuilding,AddStreet,AddCity,"
            '    qry = qry + " AddState, AddCountry, AddPostal, LocateGrp, Fax, '" & txtAccountIDRelocate.Text & "', '" & txtLocationIDRelocateNew.Text & "',  LocationPrefix, '" & txtLocationNo.Text & "', ServiceName,"
            '    qry = qry + " Contact1Position, Telephone2, ContactPerson2, Contact2Position, Contact2Tel, Contact2Fax, Contact2Tel2, Contact2Mobile, Contact2Email, ServiceAddress,"
            '    qry = qry + " ServiceLocationGroup, BillingNameSvc, BillAddressSvc, BillStreetSvc, BillBuildingSvc, BillCitySvc, BillStateSvc, BillCountrySvc, BillPostalSvc, BillContact1Svc,"
            '    qry = qry + " BillPosition1Svc, BillTelephone1Svc, BillFax1Svc, Billtelephone12Svc, BillMobile1Svc, BillEmail1Svc, BillContact2Svc, BillPosition2Svc, BillTelephone2Svc, BillFax2Svc,"
            '    qry = qry + " Billtelephone22Svc,BillMobile2Svc,BillEmail2Svc,InChargeIDSvc,ARTERMSvc,SalesManSvc,SendServiceReportTo1,SendServiceReportTo2,ContractGroup,AccountNo,"
            '    qry = qry + " Comments, PersonGroupD, InActiveD, Industry, MarketSegmentID, DefaultInvoiceFormat, ServiceEmailCC"
            '    qry = qry + " From tblPersonLocation"
            '    qry = qry + " where LocationID = '" & txtLocationIDRelocate.Text & "'"


            '    command.CommandText = qry
            '    command.Parameters.Clear()

            '    command.Connection = conn
            '    command.ExecuteNonQuery()

            '    ''''''''''''''''''''''''''''''
            '    qry1 = "DELETE from tblPersonLocation "
            '    qry1 = qry1 + " where LocationID = '" & txtLocationIDRelocate.Text & "'"

            '    command.CommandText = qry1
            '    command.Parameters.Clear()

            '    command.Connection = conn
            '    command.ExecuteNonQuery()
            '    '''''''''''''''''''''''''''''''''
            '    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CHNGACCT", txtLocationIDRelocate.Text, "PERS", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")), 0, 0, 0, txtAccountIDRelocate.Text, "", txtLocationIDRelocateNew.Text)

            'End If


            ''''''''''''''''''''''''

            InsertIntoTblWebEventLog("Contract : " + Session("UserID"), "btnSaveRelocate_Click", "", ExecutingModule)

            Dim gvbRecords, gvbService, gvbInvoice, i, j, k, totalrecords As Long
            gvbRecords = 0
            totalrecords = 0
            gvbService = 0
            gvbInvoice = 0

            gvbRecords = grvContract.Rows.Count()

            For j = 0 To gvbRecords - 1

                Dim TextBoxchkSelect As CheckBox = CType(grvContract.Rows(j).Cells(0).FindControl("chkSelectRecContractB"), CheckBox)

                If TextBoxchkSelect.Checked = True Then

                    ExecutingModule = "CONTRACT"

                    Dim TextBoxContractNoGVB As TextBox = CType(grvContract.Rows(j).Cells(0).FindControl("txtContractNoGVB"), TextBox)
                    Dim TextBoxLocationIdGVB As TextBox = CType(grvContract.Rows(j).Cells(0).FindControl("txtLocationIdGVB"), TextBox)
                    Dim TextBoxRcnoCotractGVB As TextBox = CType(grvContract.Rows(j).Cells(0).FindControl("txtRcnoGVB"), TextBox)

                    'qry = "Update tblContract set AccountId = @AccountId, ContactType=@ContactType  "
                    'qry = qry + " where ContractNo = '" & TextBoxContractNoGVB.Text & "'"

                    'command.CommandText = qry
                    'command.Parameters.Clear()
                    'command.Parameters.AddWithValue("@LocationId", txtLocationIDRelocate.Text)
                    'command.Parameters.AddWithValue("@LocationIdNew", txtLocationIDRelocateNew.Text)
                    'command.Parameters.AddWithValue("@AccountId", txtAccountIDRelocate.Text)
                    'command.Parameters.AddWithValue("@ContactType", txtAccountType.Text)
                    'command.Connection = conn
                    'command.ExecuteNonQuery()

                    'Start : SP

                    commandUpdateRelocate.Parameters.Clear()
                    commandUpdateRelocate.Parameters.AddWithValue("@pr_LocationId", txtLocationIDRelocate.Text.Trim)
                    commandUpdateRelocate.Parameters.AddWithValue("@pr_LocationIdNew", txtLocationIDRelocateNew.Text.Trim)
                    commandUpdateRelocate.Parameters.AddWithValue("@pr_AccountId", txtAccountIDRelocate.Text.Trim)
                    commandUpdateRelocate.Parameters.AddWithValue("@pr_ContactType", txtAccountType.Text.Trim)

                    commandUpdateRelocate.Parameters.AddWithValue("@pr_ContractNo", TextBoxContractNoGVB.Text.Trim)
                    commandUpdateRelocate.Parameters.AddWithValue("@pr_ServiceRecordNo", "")
                    commandUpdateRelocate.Parameters.AddWithValue("@pr_InvoiceNumber", "")
                    commandUpdateRelocate.Parameters.AddWithValue("@pr_LocationNo", 0)
                    commandUpdateRelocate.Parameters.AddWithValue("@pr_Module", "Contract")

                    commandUpdateRelocate.Connection = conn
                    commandUpdateRelocate.ExecuteScalar()

                    'End : SP

                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CHNGACCT", txtLocationIDRelocate.Text, "CONTSERV", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")), 0, 0, 0, txtAccountIDRelocate.Text, TextBoxContractNoGVB.Text, txtLocationIDRelocateNew.Text)

                    '-------------------------
                    ExecutingModule = "CONTRACTDET"
                    'qry = "Update tblContractdet set AccountId = @AccountId, LocationId= @LocationIdNew  "
                    'qry = qry + " where ContractNo = @ContractNo and LocationID =@LocationID; "

                    'command.CommandText = qry
                    'command.Parameters.Clear()
                    'command.Parameters.AddWithValue("@LocationId", txtLocationIDRelocate.Text)
                    'command.Parameters.AddWithValue("@LocationIdNew", txtLocationIDRelocateNew.Text)
                    'command.Parameters.AddWithValue("@AccountId", txtAccountIDRelocate.Text)
                    'command.Parameters.AddWithValue("@ContractNo", TextBoxContractNoGVB.Text)
                    'command.Connection = conn
                    'command.ExecuteNonQuery()


                    'Start : SP

                    commandUpdateRelocate.Parameters.Clear()
                    commandUpdateRelocate.Parameters.AddWithValue("@pr_LocationId", txtLocationIDRelocate.Text.Trim)
                    commandUpdateRelocate.Parameters.AddWithValue("@pr_LocationIdNew", txtLocationIDRelocateNew.Text.Trim)
                    commandUpdateRelocate.Parameters.AddWithValue("@pr_AccountId", txtAccountIDRelocate.Text.Trim)
                    commandUpdateRelocate.Parameters.AddWithValue("@pr_ContactType", "")

                    commandUpdateRelocate.Parameters.AddWithValue("@pr_ContractNo", TextBoxContractNoGVB.Text.Trim)
                    commandUpdateRelocate.Parameters.AddWithValue("@pr_ServiceRecordNo", "")
                    commandUpdateRelocate.Parameters.AddWithValue("@pr_InvoiceNumber", "")
                    commandUpdateRelocate.Parameters.AddWithValue("@pr_LocationNo", 0)
                    commandUpdateRelocate.Parameters.AddWithValue("@pr_Module", "ContractDet")

                    commandUpdateRelocate.Connection = conn
                    commandUpdateRelocate.ExecuteScalar()

                    'End : SP

                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CHNGACCT", txtLocationIDRelocate.Text, "CONTSERVDET", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")), 0, 0, 0, txtAccountIDRelocate.Text, TextBoxContractNoGVB.Text, txtLocationIDRelocateNew.Text)

                    '''''''''''''''''''''''''''
                    ExecutingModule = "SERVICE"
                    gvbService = grvService.Rows.Count()
                    For i = 0 To gvbService - 1
                        Dim TextBoxServiceRecordNoGVB As TextBox = CType(grvService.Rows(i).Cells(0).FindControl("txtRecordNoGVBS"), TextBox)
                        Dim TextBoxContractNoGVBS As TextBox = CType(grvService.Rows(i).Cells(0).FindControl("txtContractNoGVBS"), TextBox)
                        Dim TextBoxLocationIdGVBS As TextBox = CType(grvService.Rows(i).Cells(0).FindControl("txtLocationIdGVBS"), TextBox)

                        If TextBoxContractNoGVBS.Text.Trim = TextBoxContractNoGVB.Text.Trim And TextBoxLocationIdGVBS.Text.Trim = txtLocationIDRelocate.Text.Trim Then
                            'qry = "Update tblServiceRecord set AccountId = @AccountId, LocationId= @LocationIdNew, ContactType=@ContactType  "
                            'qry = qry + " where RecordNo = @ServiceRecordNo; "
                            'command.CommandText = qry
                            'command.Parameters.Clear()

                            'command.Parameters.AddWithValue("@AccountId", txtAccountIDRelocate.Text)
                            'command.Parameters.AddWithValue("@LocationIdNew", txtLocationIDRelocateNew.Text)

                            'command.Parameters.AddWithValue("@ServiceRecordNo", TextBoxServiceRecordNoGVB.Text)
                            'command.Parameters.AddWithValue("@ContactType", txtAccountType.Text)

                            'command.Connection = conn
                            'command.ExecuteNonQuery()


                            'Start : SP

                            commandUpdateRelocate.Parameters.Clear()
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_LocationId", "")
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_LocationIdNew", txtLocationIDRelocateNew.Text.Trim)
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_AccountId", txtAccountIDRelocate.Text.Trim)
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_ContactType", txtAccountType.Text)

                            commandUpdateRelocate.Parameters.AddWithValue("@pr_ContractNo", "")
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_ServiceRecordNo", TextBoxServiceRecordNoGVB.Text)
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_InvoiceNumber", "")
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_LocationNo", 0)
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_Module", "Service")

                            commandUpdateRelocate.Connection = conn
                            commandUpdateRelocate.ExecuteScalar()

                            'End : SP

                            CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CHNGACCT", txtLocationIDRelocate.Text, "SERV", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")), 0, 0, 0, txtAccountIDRelocate.Text, TextBoxServiceRecordNoGVB.Text, txtLocationIDRelocateNew.Text)

                        End If
                    Next

                    '---------------------------------------

                    gvbInvoice = grvInvoice.Rows.Count()
                    For k = 0 To gvbInvoice - 1
                        Dim TextBoxServiceRecordNoInvoiceGVB As TextBox = CType(grvInvoice.Rows(k).Cells(0).FindControl("txtRecordNoGVBS"), TextBox)
                        Dim TextBoxContractNoGVBI As TextBox = CType(grvInvoice.Rows(k).Cells(0).FindControl("txtContractNoGVBI"), TextBox)
                        Dim TextBoxLocationIdGVBI As TextBox = CType(grvInvoice.Rows(k).Cells(0).FindControl("txtLocationIdGVBI"), TextBox)
                        Dim TextBoxInvoiceNoGVBI As TextBox = CType(grvInvoice.Rows(k).Cells(0).FindControl("txtInvoiveNumberGVBI"), TextBox)

                        ExecutingModule = "SALESDETAIL"
                        If TextBoxContractNoGVBI.Text.Trim = TextBoxContractNoGVB.Text.Trim And TextBoxLocationIdGVBI.Text.Trim = txtLocationIDRelocate.Text.Trim Then
                            'qry = "Update tblSalesDetail set  LocationId= @LocationIdNew  "
                            'qry = qry + " where LocationID =@LocationID; "

                            'command.CommandText = qry
                            'command.Parameters.Clear()
                            'command.Parameters.AddWithValue("@LocationId", txtLocationIDRelocate.Text)
                            'command.Parameters.AddWithValue("@LocationIdNew", txtLocationIDRelocateNew.Text)

                            'command.Connection = conn
                            'command.ExecuteNonQuery()

                            'Start : SP

                            commandUpdateRelocate.Parameters.Clear()
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_LocationId", txtLocationIDRelocate.Text)
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_LocationIdNew", txtLocationIDRelocateNew.Text.Trim)
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_AccountId", "")
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_ContactType", "")

                            commandUpdateRelocate.Parameters.AddWithValue("@pr_ContractNo", "")
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_ServiceRecordNo", "")
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_InvoiceNumber", "")
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_LocationNo", 0)
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_Module", "InvoiceDet")

                            commandUpdateRelocate.Connection = conn
                            commandUpdateRelocate.ExecuteScalar()

                            'End : SP

                            CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CHNGACCT", txtLocationIDRelocate.Text, "INVOICEDET", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")), 0, 0, 0, txtAccountIDRelocate.Text, TextBoxInvoiceNoGVBI.Text, txtLocationIDRelocateNew.Text)

                            ExecutingModule = "SALES"
                            'qry = "Update tblSales set  AccountId= @AccountId, ContactType=@ContactType  "
                            'qry = qry + " where InvoiceNumber =@InvoiceNumber; "

                            'command.CommandText = qry
                            'command.Parameters.Clear()

                            'command.Parameters.AddWithValue("@InvoiceNumber", TextBoxInvoiceNoGVBI.Text)
                            'command.Parameters.AddWithValue("@AccountId", txtAccountIDRelocate.Text)
                            'command.Parameters.AddWithValue("@ContactType", txtAccountType.Text)
                            'command.Connection = conn
                            'command.ExecuteNonQuery()

                            'Start : SP

                            commandUpdateRelocate.Parameters.Clear()
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_LocationId", "")
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_LocationIdNew", "")
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_AccountId", txtAccountIDRelocate.Text)
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_ContactType", txtAccountType.Text)

                            commandUpdateRelocate.Parameters.AddWithValue("@pr_ContractNo", "")
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_ServiceRecordNo", "")
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_InvoiceNumber", TextBoxInvoiceNoGVBI.Text)
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_LocationNo", 0)
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_Module", "Invoice")

                            commandUpdateRelocate.Connection = conn
                            commandUpdateRelocate.ExecuteScalar()

                            'End : SP

                            CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CHNGACCT", txtLocationIDRelocate.Text, "INVOICE", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")), 0, 0, 0, txtAccountIDRelocate.Text, TextBoxInvoiceNoGVBI.Text, txtLocationIDRelocateNew.Text)

                            ''''''''''''''''''''''''''''''''
                            qry = "Update tblSales set  AccountId= @AccountId,  ContactType=@ContactType   "
                            qry = qry + " where InvoiceNumber in (Select InvoiceNumber from tblSalesDetail where SourceInvoice = @InvoiceNumber); "

                            'command.CommandText = qry
                            'command.Parameters.Clear()

                            'command.Parameters.AddWithValue("@InvoiceNumber", TextBoxInvoiceNoGVBI.Text)
                            'command.Parameters.AddWithValue("@AccountId", txtAccountIDRelocate.Text)
                            'command.Parameters.AddWithValue("@ContactType", txtAccountType.Text)
                            'command.Connection = conn
                            'command.ExecuteNonQuery()

                            'Start : SP

                            commandUpdateRelocate.Parameters.Clear()
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_LocationId", "")
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_LocationIdNew", "")
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_AccountId", txtAccountIDRelocate.Text)
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_ContactType", txtAccountType.Text)

                            commandUpdateRelocate.Parameters.AddWithValue("@pr_ContractNo", "")
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_ServiceRecordNo", "")
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_InvoiceNumber", TextBoxInvoiceNoGVBI.Text)
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_LocationNo", 0)
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_Module", "CNDN")

                            commandUpdateRelocate.Connection = conn
                            commandUpdateRelocate.ExecuteScalar()

                            'End : SP

                            CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CHNGACCT", txtLocationIDRelocate.Text, "CNDN", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")), 0, 0, 0, txtAccountIDRelocate.Text, TextBoxInvoiceNoGVBI.Text, txtLocationIDRelocateNew.Text)

                            '''''''''''''''''''''''''''''''''''''''
                            ExecutingModule = "RECV"
                            'qry = "Update tblRecv, tblRecvDet set tblRecv.AccountId = @AccountId,  tblRecv.ContactType=@ContactType   "
                            'qry = qry + " where tblRecv.ReceiptNumber = tblRecvDet.ReceiptNumber "
                            'qry = qry + " and tblRecvDet.RefType =@InvoiceNumber; "

                            'command.CommandText = qry
                            'command.Parameters.Clear()

                            'command.Parameters.AddWithValue("@InvoiceNumber", TextBoxInvoiceNoGVBI.Text)
                            'command.Parameters.AddWithValue("@AccountId", txtAccountIDRelocate.Text)
                            'command.Parameters.AddWithValue("@ContactType", txtAccountType.Text)
                            'command.Connection = conn
                            'command.ExecuteNonQuery()

                            'Start : SP

                            commandUpdateRelocate.Parameters.Clear()
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_LocationId", "")
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_LocationIdNew", "")
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_AccountId", txtAccountIDRelocate.Text)
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_ContactType", txtAccountType.Text)

                            commandUpdateRelocate.Parameters.AddWithValue("@pr_ContractNo", "")
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_ServiceRecordNo", "")
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_InvoiceNumber", TextBoxInvoiceNoGVBI.Text)
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_LocationNo", 0)
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_Module", "Receipt")

                            commandUpdateRelocate.Connection = conn
                            commandUpdateRelocate.ExecuteScalar()

                            'End : SP

                            CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CHNGACCT", txtLocationIDRelocate.Text, "RECEIPT", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")), 0, 0, 0, txtAccountIDRelocate.Text, TextBoxInvoiceNoGVBI.Text, txtLocationIDRelocateNew.Text)

                            'qry = "Update tblRecv, tblRecvDet set tblRecvDet.AccountId = @AccountId,  tblRecv.ContactType=@ContactType   "
                            'qry = qry + " where tblRecv.ReceiptNumber = tblRecvDet.ReceiptNumber "
                            'qry = qry + " and tblRecvDet.RefType =@InvoiceNumber; "

                            'command.CommandText = qry
                            'command.Parameters.Clear()

                            'command.Parameters.AddWithValue("@InvoiceNumber", TextBoxInvoiceNoGVBI.Text)
                            'command.Parameters.AddWithValue("@AccountId", txtAccountIDRelocate.Text)
                            'command.Parameters.AddWithValue("@ContactType", txtAccountType.Text)
                            'command.Connection = conn
                            'command.ExecuteNonQuery()

                            'Start : SP

                            commandUpdateRelocate.Parameters.Clear()
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_LocationId", "")
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_LocationIdNew", "")
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_AccountId", txtAccountIDRelocate.Text)
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_ContactType", txtAccountType.Text)

                            commandUpdateRelocate.Parameters.AddWithValue("@pr_ContractNo", "")
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_ServiceRecordNo", "")
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_InvoiceNumber", TextBoxInvoiceNoGVBI.Text)
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_LocationNo", 0)
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_Module", "ReceiptDet")

                            commandUpdateRelocate.Connection = conn
                            commandUpdateRelocate.ExecuteScalar()

                            'End : SP

                            CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CHNGACCT", txtLocationIDRelocate.Text, "RECEIPTDET", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")), 0, 0, 0, txtAccountIDRelocate.Text, TextBoxInvoiceNoGVBI.Text, txtLocationIDRelocateNew.Text)

                            '=======================
                            '=============================

                            ExecutingModule = "JOURNAL DET"
                            'qry = "Update tblJrnvdet set AccountId = @AccountId, LocationId= @LocationIdNew,  ContactType=@ContactType   "
                            'qry = qry + " where RefType =@InvoiceNumber; "

                            'command.CommandText = qry
                            'command.Parameters.Clear()

                            ''command.Parameters.AddWithValue("@LocationId", txtLocationIDRelocate.Text)
                            'command.Parameters.AddWithValue("@LocationIdNew", txtLocationIDRelocateNew.Text)
                            'command.Parameters.AddWithValue("@InvoiceNumber", TextBoxInvoiceNoGVBI.Text)
                            'command.Parameters.AddWithValue("@AccountId", txtAccountIDRelocate.Text)
                            'command.Parameters.AddWithValue("@ContactType", txtAccountType.Text)
                            'command.Connection = conn
                            'command.ExecuteNonQuery()


                            'Start : SP

                            commandUpdateRelocate.Parameters.Clear()
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_LocationId", txtLocationIDRelocate.Text)
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_LocationIdNew", txtLocationIDRelocateNew.Text)
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_AccountId", txtAccountIDRelocate.Text)
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_ContactType", txtAccountType.Text)

                            commandUpdateRelocate.Parameters.AddWithValue("@pr_ContractNo", "")
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_ServiceRecordNo", "")
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_InvoiceNumber", TextBoxInvoiceNoGVBI.Text)
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_LocationNo", 0)
                            commandUpdateRelocate.Parameters.AddWithValue("@pr_Module", "Journal")

                            commandUpdateRelocate.Connection = conn
                            commandUpdateRelocate.ExecuteScalar()

                            'End : SP

                            CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CHNGACCT", txtLocationIDRelocate.Text, "ADJNOTE", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")), 0, 0, 0, txtAccountIDRelocate.Text, TextBoxInvoiceNoGVBI.Text, txtLocationIDRelocateNew.Text)

                        End If
                    Next

                    '---------------------------------------

                    totalrecords = totalrecords + 1
                End If
            Next

            lblMessage.Text = "Total Contracts Updated :" & totalrecords

            mdlCompleteConfirm.Show()

            'grvContract.DataBind()
            'grvService.DataBind()
            'grvInvoice.DataBind()

            'grvReceipts.DataBind()
            'grvCNDN.DataBind()
            'grvJournals.DataBind()

            ''''''''''''''''
            'Start : 05.01.2020

            ExecutingModule = "generateLocationIdRelocate"

            InsertIntoTblWebEventLog("Customer : " + Session("UserID"), "btnSaveRelocate_Click", "", ExecutingModule)


            If txtContactTypeRelocate.Text = "CORPORATE" And txtAccountType.Text = "COMPANY" Then
                ExecutingModule = "CORPORATE"
                'qry = "Update tblCompanyLocation set AccountId = @AccountId, LocationId= @LocationIdNew, LocationNo=@LocationNo  "
                'qry = qry + " where LocationID =@LocationID; "

                'command.CommandText = qry
                'command.Parameters.Clear()
                'command.Parameters.AddWithValue("@LocationId", txtLocationIDRelocate.Text)
                'command.Parameters.AddWithValue("@LocationIdNew", txtLocationIDRelocateNew.Text)
                'command.Parameters.AddWithValue("@AccountId", txtAccountIDRelocate.Text)
                'command.Parameters.AddWithValue("@LocationNo", txtLocationNo.Text)
                'command.Connection = conn
                'command.ExecuteNonQuery()


                'Start : SP

                commandUpdateRelocate.Parameters.Clear()
                commandUpdateRelocate.Parameters.AddWithValue("@pr_LocationId", txtLocationIDRelocate.Text)
                commandUpdateRelocate.Parameters.AddWithValue("@pr_LocationIdNew", txtLocationIDRelocateNew.Text)
                commandUpdateRelocate.Parameters.AddWithValue("@pr_AccountId", txtAccountIDRelocate.Text)
                commandUpdateRelocate.Parameters.AddWithValue("@pr_ContactType", "")

                commandUpdateRelocate.Parameters.AddWithValue("@pr_ContractNo", "")
                commandUpdateRelocate.Parameters.AddWithValue("@pr_ServiceRecordNo", "")
                commandUpdateRelocate.Parameters.AddWithValue("@pr_InvoiceNumber", "")
                commandUpdateRelocate.Parameters.AddWithValue("@pr_LocationNo", txtLocationNo.Text)
                commandUpdateRelocate.Parameters.AddWithValue("@pr_Module", "CompLoc")

                commandUpdateRelocate.Connection = conn
                commandUpdateRelocate.ExecuteScalar()

                'End : SP

                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CHNGACCT", txtLocationIDRelocate.Text, "CORP", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")), 0, 0, 0, txtAccountIDRelocate.Text, "" & txtLocationIDRelocate.Text & " (" & txtServiceNameRelocate.Text & ") - " & txtContactTypeRelocate.Text & " has been relocated to " & txtLocationIDRelocateNew.Text & " (" & txtBillingNameRelocate.Text & ") - " & txtAccountType.Text, txtLocationIDRelocateNew.Text)

            ElseIf txtContactTypeRelocate.Text = "RESIDENTIAL" And txtAccountType.Text = "PERSON" Then
                ExecutingModule = "RESIDENTIAL"
                'qry = "Update tblPersonLocation set AccountId = @AccountId, LocationId= @LocationIdNew, LocationNo=@LocationNo  "
                'qry = qry + " where LocationID =@LocationID; "

                'command.CommandText = qry
                'command.Parameters.Clear()
                'command.Parameters.AddWithValue("@LocationId", txtLocationIDRelocate.Text)
                'command.Parameters.AddWithValue("@LocationIdNew", txtLocationIDRelocateNew.Text)
                'command.Parameters.AddWithValue("@AccountId", txtAccountIDRelocate.Text)
                'command.Parameters.AddWithValue("@LocationNo", txtLocationNo.Text)
                'command.Connection = conn
                'command.ExecuteNonQuery()

                'Start : SP

                commandUpdateRelocate.Parameters.Clear()
                commandUpdateRelocate.Parameters.AddWithValue("@pr_LocationId", txtLocationIDRelocate.Text)
                commandUpdateRelocate.Parameters.AddWithValue("@pr_LocationIdNew", txtLocationIDRelocateNew.Text)
                commandUpdateRelocate.Parameters.AddWithValue("@pr_AccountId", txtAccountIDRelocate.Text)
                commandUpdateRelocate.Parameters.AddWithValue("@pr_ContactType", "")

                commandUpdateRelocate.Parameters.AddWithValue("@pr_ContractNo", "")
                commandUpdateRelocate.Parameters.AddWithValue("@pr_ServiceRecordNo", "")
                commandUpdateRelocate.Parameters.AddWithValue("@pr_InvoiceNumber", "")
                commandUpdateRelocate.Parameters.AddWithValue("@pr_LocationNo", txtLocationNo.Text)
                commandUpdateRelocate.Parameters.AddWithValue("@pr_Module", "PersLoc")

                commandUpdateRelocate.Connection = conn
                commandUpdateRelocate.ExecuteScalar()

                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CHNGACCT", txtLocationIDRelocate.Text, "PERS", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")), 0, 0, 0, txtAccountIDRelocate.Text, "" & txtLocationIDRelocate.Text & " (" & txtServiceNameRelocate.Text & ") - " & txtContactTypeRelocate.Text & " has been relocated to " & txtLocationIDRelocateNew.Text & " (" & txtBillingNameRelocate.Text & ") - " & txtAccountType.Text, txtLocationIDRelocateNew.Text)

                'End : SP

            End If


            If txtContactTypeRelocate.Text = "CORPORATE" And txtAccountType.Text = "PERSON" Then

                'qry = "INSERT INTO tblpersonlocation "
                'qry = qry + " (PersonID,Location,BranchID,Description,ContactPerson,Address1,Telephone,Mobile,Email,CreatedBy,"
                'qry = qry + " CreatedOn,LastModifiedBy,LastModifiedOn,AddBlock,AddNos,AddFloor,AddUnit,AddBuilding,AddStreet,AddCity,"
                'qry = qry + " AddState,AddCountry,AddPostal,LocateGrp,Fax,AccountID,LocationID,LocationPrefix,LocationNo,ServiceName,"
                'qry = qry + " Contact1Position,Telephone2,ContactPerson2,Contact2Position,Contact2Tel,Contact2Fax,Contact2Tel2,Contact2Mobile,Contact2Email,ServiceAddress,"
                'qry = qry + " ServiceLocationGroup,BillingNameSvc,BillAddressSvc,BillStreetSvc,BillBuildingSvc,BillCitySvc,BillStateSvc,BillCountrySvc,BillPostalSvc,BillContact1Svc,"
                'qry = qry + " BillPosition1Svc,BillTelephone1Svc,BillFax1Svc,Billtelephone12Svc,BillMobile1Svc,BillEmail1Svc,BillContact2Svc,BillPosition2Svc,BillTelephone2Svc,BillFax2Svc,"
                'qry = qry + " Billtelephone22Svc,BillMobile2Svc,BillEmail2Svc,InChargeIDSvc,ARTERMSvc,SalesManSvc,SendServiceReportTo1,SendServiceReportTo2,ContractGroup,AccountNo,"
                'qry = qry + " Comments,PersonGroupD,InActiveD,Industry,MarketSegmentID,DefaultInvoiceFormat,ServiceEmailCC)"
                'qry = qry + " SELECT "
                'qry = qry + " CompanyID,Location,BranchID,Description,ContactPerson,Address1,Telephone,Mobile,Email,CreatedBy,"
                'qry = qry + " CreatedOn,LastModifiedBy,LastModifiedOn,AddBlock,AddNos,AddFloor,AddUnit,AddBuilding,AddStreet,AddCity,"
                'qry = qry + " AddState, AddCountry, AddPostal, LocateGrp, Fax, '" & txtAccountIDRelocate.Text & "', '" & txtLocationIDRelocateNew.Text & "',  LocationPrefix, '" & txtLocationNo.Text & "', ServiceName,"
                'qry = qry + " Contact1Position, Telephone2, ContactPerson2, Contact2Position, Contact2Tel, Contact2Fax, Contact2Tel2, Contact2Mobile, Contact2Email, ServiceAddress,"
                'qry = qry + " ServiceLocationGroup, BillingNameSvc, BillAddressSvc, BillStreetSvc, BillBuildingSvc, BillCitySvc, BillStateSvc, BillCountrySvc, BillPostalSvc, BillContact1Svc,"
                'qry = qry + " BillPosition1Svc, BillTelephone1Svc, BillFax1Svc, Billtelephone12Svc, BillMobile1Svc, BillEmail1Svc, BillContact2Svc, BillPosition2Svc, BillTelephone2Svc, BillFax2Svc,"
                'qry = qry + " Billtelephone22Svc,BillMobile2Svc,BillEmail2Svc,InChargeIDSvc,ARTERMSvc,SalesManSvc,SendServiceReportTo1,SendServiceReportTo2,ContractGroup,AccountNo,"
                'qry = qry + " Comments, CompanyGroupD, InActiveD, Industry, MarketSegmentID, DefaultInvoiceFormat, ServiceEmailCC"
                'qry = qry + " From tblCompanyLocation"
                'qry = qry + " where LocationID = '" & txtLocationIDRelocate.Text & "'"


                'command.CommandText = qry
                'command.Parameters.Clear()

                'command.Connection = conn
                'command.ExecuteNonQuery()

                'qry1 = "DELETE from tblCompanyLocation "
                'qry1 = qry1 + " where LocationID = '" & txtLocationIDRelocate.Text & "'"

                'command.CommandText = qry1
                'command.Parameters.Clear()

                'command.Connection = conn
                'command.ExecuteNonQuery()
                '''''''''''''''''''''''''''''''''

                'Start : SP

                commandUpdateRelocate.Parameters.Clear()
                commandUpdateRelocate.Parameters.AddWithValue("@pr_LocationId", txtLocationIDRelocate.Text)
                commandUpdateRelocate.Parameters.AddWithValue("@pr_LocationIdNew", txtLocationIDRelocateNew.Text)
                commandUpdateRelocate.Parameters.AddWithValue("@pr_AccountId", txtAccountIDRelocate.Text)
                commandUpdateRelocate.Parameters.AddWithValue("@pr_ContactType", "")

                commandUpdateRelocate.Parameters.AddWithValue("@pr_ContractNo", "")
                commandUpdateRelocate.Parameters.AddWithValue("@pr_ServiceRecordNo", "")
                commandUpdateRelocate.Parameters.AddWithValue("@pr_InvoiceNumber", "")
                commandUpdateRelocate.Parameters.AddWithValue("@pr_LocationNo", txtLocationNo.Text)
                commandUpdateRelocate.Parameters.AddWithValue("@pr_Module", "CorpToRes")

                commandUpdateRelocate.Connection = conn
                commandUpdateRelocate.ExecuteScalar()

                'End : SP
                'CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CHNGACCT", txtLocationIDRelocate.Text, "PERS", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")), 0, 0, 0, txtAccountIDRelocate.Text, "" & txtLocationIDRelocate.Text & " (" & txtServiceNameRelocate.Text & ") - " & txtContactTypeRelocate.Text & " has been relocated to " & txtLocationIDRelocateNew.Text & " (" & txtBillingNameRelocate.Text & ") - " & txtAccountType.Text, txtLocationIDRelocateNew.Text)



                ''''''''''''''''''''''''''''''


                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CHNGACCT", txtLocationIDRelocate.Text, "CORP", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")), 0, 0, 0, txtAccountIDRelocate.Text, "" & txtLocationIDRelocate.Text & " (" & txtServiceNameRelocate.Text & ") - " & txtContactTypeRelocate.Text & " has been relocated to " & txtLocationIDRelocateNew.Text & " (" & txtBillingNameRelocate.Text & ") - " & txtAccountType.Text, txtLocationIDRelocateNew.Text)

            ElseIf txtContactTypeRelocate.Text = "RESIDENTIAL" And txtAccountType.Text = "COMPANY" Then

                'qry = "INSERT INTO tblCompanylocation "
                'qry = qry + " (CompanyID,Location,BranchID,Description,ContactPerson,Address1,Telephone,Mobile,Email,CreatedBy,"
                'qry = qry + " CreatedOn,LastModifiedBy,LastModifiedOn,AddBlock,AddNos,AddFloor,AddUnit,AddBuilding,AddStreet,AddCity,"
                'qry = qry + " AddState,AddCountry,AddPostal,LocateGrp,Fax,AccountID,LocationID,LocationPrefix,LocationNo,ServiceName,"
                'qry = qry + " Contact1Position,Telephone2,ContactPerson2,Contact2Position,Contact2Tel,Contact2Fax,Contact2Tel2,Contact2Mobile,Contact2Email,ServiceAddress,"
                'qry = qry + " ServiceLocationGroup,BillingNameSvc,BillAddressSvc,BillStreetSvc,BillBuildingSvc,BillCitySvc,BillStateSvc,BillCountrySvc,BillPostalSvc,BillContact1Svc,"
                'qry = qry + " BillPosition1Svc,BillTelephone1Svc,BillFax1Svc,Billtelephone12Svc,BillMobile1Svc,BillEmail1Svc,BillContact2Svc,BillPosition2Svc,BillTelephone2Svc,BillFax2Svc,"
                'qry = qry + " Billtelephone22Svc,BillMobile2Svc,BillEmail2Svc,InChargeIDSvc,ARTERMSvc,SalesManSvc,SendServiceReportTo1,SendServiceReportTo2,ContractGroup,AccountNo,"
                'qry = qry + " Comments,CompanyGroupD,InActiveD,Industry,MarketSegmentID,DefaultInvoiceFormat,ServiceEmailCC)"
                'qry = qry + " SELECT "
                'qry = qry + " PersonID,Location,BranchID,Description,ContactPerson,Address1,Telephone,Mobile,Email,CreatedBy,"
                'qry = qry + " CreatedOn,LastModifiedBy,LastModifiedOn,AddBlock,AddNos,AddFloor,AddUnit,AddBuilding,AddStreet,AddCity,"
                'qry = qry + " AddState, AddCountry, AddPostal, LocateGrp, Fax, '" & txtAccountIDRelocate.Text & "', '" & txtLocationIDRelocateNew.Text & "',  LocationPrefix, '" & txtLocationNo.Text & "', ServiceName,"
                'qry = qry + " Contact1Position, Telephone2, ContactPerson2, Contact2Position, Contact2Tel, Contact2Fax, Contact2Tel2, Contact2Mobile, Contact2Email, ServiceAddress,"
                'qry = qry + " ServiceLocationGroup, BillingNameSvc, BillAddressSvc, BillStreetSvc, BillBuildingSvc, BillCitySvc, BillStateSvc, BillCountrySvc, BillPostalSvc, BillContact1Svc,"
                'qry = qry + " BillPosition1Svc, BillTelephone1Svc, BillFax1Svc, Billtelephone12Svc, BillMobile1Svc, BillEmail1Svc, BillContact2Svc, BillPosition2Svc, BillTelephone2Svc, BillFax2Svc,"
                'qry = qry + " Billtelephone22Svc,BillMobile2Svc,BillEmail2Svc,InChargeIDSvc,ARTERMSvc,SalesManSvc,SendServiceReportTo1,SendServiceReportTo2,ContractGroup,AccountNo,"
                'qry = qry + " Comments, PersonGroupD, InActiveD, Industry, MarketSegmentID, DefaultInvoiceFormat, ServiceEmailCC"
                'qry = qry + " From tblPersonLocation"
                'qry = qry + " where LocationID = '" & txtLocationIDRelocate.Text & "'"


                'command.CommandText = qry
                'command.Parameters.Clear()

                'command.Connection = conn
                'command.ExecuteNonQuery()

                ' ''''''''''''''''''''''''''''''
                'qry1 = "DELETE from tblPersonLocation "
                'qry1 = qry1 + " where LocationID = '" & txtLocationIDRelocate.Text & "'"

                'command.CommandText = qry1
                'command.Parameters.Clear()

                'command.Connection = conn
                'command.ExecuteNonQuery()


                'Start : SP

                commandUpdateRelocate.Parameters.Clear()
                commandUpdateRelocate.Parameters.AddWithValue("@pr_LocationId", txtLocationIDRelocate.Text)
                commandUpdateRelocate.Parameters.AddWithValue("@pr_LocationIdNew", txtLocationIDRelocateNew.Text)
                commandUpdateRelocate.Parameters.AddWithValue("@pr_AccountId", txtAccountIDRelocate.Text)
                commandUpdateRelocate.Parameters.AddWithValue("@pr_ContactType", "")

                commandUpdateRelocate.Parameters.AddWithValue("@pr_ContractNo", "")
                commandUpdateRelocate.Parameters.AddWithValue("@pr_ServiceRecordNo", "")
                commandUpdateRelocate.Parameters.AddWithValue("@pr_InvoiceNumber", "")
                commandUpdateRelocate.Parameters.AddWithValue("@pr_LocationNo", txtLocationNo.Text)
                commandUpdateRelocate.Parameters.AddWithValue("@pr_Module", "ResToCorp")

                commandUpdateRelocate.Connection = conn
                commandUpdateRelocate.ExecuteScalar()

                'End : SP
                '''''''''''''''''''''''''''''''''
                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CHNGACCT", txtLocationIDRelocate.Text, "PERS", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")), 0, 0, 0, txtAccountIDRelocate.Text, "" & txtLocationIDRelocate.Text & " (" & txtServiceNameRelocate.Text & ") - " & txtContactTypeRelocate.Text & " has been relocated to " & txtLocationIDRelocateNew.Text & " (" & txtBillingNameRelocate.Text & ") - " & txtAccountType.Text, txtLocationIDRelocateNew.Text)

            End If

            'end: 05.01.2020


            'qry = "Update tblCompanyLocation set AccountId = @AccountId, LocationId= @LocationIdNew  "
            'qry = qry + " where LocationID =@LocationID; "

            'command.CommandText = qry
            'command.Parameters.Clear()
            'command.Parameters.AddWithValue("@LocationId", txtLocationIDRelocate.Text)
            'command.Parameters.AddWithValue("@LocationIdNew", txtLocationIDRelocateNew.Text)
            'command.Parameters.AddWithValue("@AccountId", txtAccountIDRelocate.Text)

            'command.Connection = conn
            'command.ExecuteNonQuery()

            ' '''''''''''''''''''''''''''''''''''''''''''''''

            'qry = "Update tblContractdet set AccountId = @AccountId, LocationId= @LocationIdNew  "
            'qry = qry + " where LocationID =@LocationID; "

            'command.CommandText = qry
            'command.Parameters.Clear()
            'command.Parameters.AddWithValue("@LocationId", txtLocationIDRelocate.Text)
            'command.Parameters.AddWithValue("@LocationIdNew", txtLocationIDRelocateNew.Text)
            'command.Parameters.AddWithValue("@AccountId", txtAccountIDRelocate.Text)

            'command.Connection = conn
            'command.ExecuteNonQuery()


            ' '''''''''''''''''''''''''''''''''''''''''''''''

            'qry = "Update tblServiceRecord set AccountId = @AccountId, LocationId= @LocationIdNew  "
            'qry = qry + " where LocationID =@LocationID; "

            'command.CommandText = qry
            'command.Parameters.Clear()
            'command.Parameters.AddWithValue("@LocationId", txtLocationIDRelocate.Text)
            'command.Parameters.AddWithValue("@LocationIdNew", txtLocationIDRelocateNew.Text)
            'command.Parameters.AddWithValue("@AccountId", txtAccountIDRelocate.Text)

            'command.Connection = conn
            'command.ExecuteNonQuery()

            ' '''''''''''''''''''''''''''''''''''''''''''''''

            'qry = "Update tblSalesDetail set AccountId = @AccountId, LocationId= @LocationIdNew  "
            'qry = qry + " where LocationID =@LocationID; "

            'command.CommandText = qry
            'command.Parameters.Clear()
            'command.Parameters.AddWithValue("@LocationId", txtLocationIDRelocate.Text)
            'command.Parameters.AddWithValue("@LocationIdNew", txtLocationIDRelocateNew.Text)
            ''command.Parameters.AddWithValue("@AccountId", txtAccountIDRelocate.Text)

            'command.Connection = conn
            'command.ExecuteNonQuery()

            ' '''''''''''''''''''''''''''''''''''''''''''''''

            'qry = "Update tblRecvDet set AccountId = @AccountId, LocationId= @LocationIdNew  "
            'qry = qry + " where LocationID =@LocationID; "

            'command.CommandText = qry
            'command.Parameters.Clear()
            'command.Parameters.AddWithValue("@LocationId", txtLocationIDRelocate.Text)
            'command.Parameters.AddWithValue("@LocationIdNew", txtLocationIDRelocateNew.Text)
            ''command.Parameters.AddWithValue("@AccountId", txtAccountIDRelocate.Text)

            'command.Connection = conn
            'command.ExecuteNonQuery()

            ' '''''''''''''''''''''''''''''''''''''''''''''''

            'qry = "Update tblJrnvdet set AccountId = @AccountId, LocationId= @LocationId  "
            'qry = qry + " where LocationID =@LocationID; "

            'command.CommandText = qry
            'command.Parameters.Clear()

            'command.Parameters.AddWithValue("@LocationId", txtLocationIDRelocate.Text)
            'command.Parameters.AddWithValue("@LocationIdNew", txtLocationIDRelocateNew.Text)
            'command.Parameters.AddWithValue("@AccountId", txtAccountIDRelocate.Text)

            'command.Connection = conn
            'command.ExecuteNonQuery()

            ''''''''''''''''''''''''''''''''''''''
            conn.Close()
            conn.Dispose()
            command.Dispose()

            commandUpdateRelocate.Dispose()


            PopulateGrid()
            txtUpdated.Text = "Y"
        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString
            InsertIntoTblWebEventLog("RELOCATE - " + Session("UserID"), "updateRelocate", ex.Message.ToString, ExecutingModule)
            Exit Sub
        End Try
    End Sub
    Public Sub InsertIntoTblWebEventLog(LoginID As String, events As String, errorMsg As String, ID As String)
        Try
            Dim conn As New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

            Dim insCmds As New MySqlCommand()
            insCmds.CommandType = CommandType.Text

            Dim insQuery As String = "Insert into tblWebEventLog(LoginId, Event, Error,ID, CreatedOn)"
            insQuery += " values(@LoginId,@Event,@Error,@ID,@CreatedOn);"

            insCmds.CommandText = insQuery

            insCmds.Parameters.Clear()
            insCmds.Parameters.AddWithValue("@LoginId", LoginID)
            insCmds.Parameters.AddWithValue("@Event", events)
            insCmds.Parameters.AddWithValue("@Error", errorMsg)
            insCmds.Parameters.AddWithValue("@ID", ID)
            insCmds.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))

            conn.Open()
            insCmds.Connection = conn
            insCmds.ExecuteNonQuery()
            conn.Close()
            conn.Dispose()
            insCmds.Dispose()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub generateLocationIdRelocate()
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        If conn.State = ConnectionState.Open Then
            conn.Close()
            conn.Dispose()
        End If
        conn.Open()

        Dim command1 As MySqlCommand = New MySqlCommand

        If txtAccountType.Text = "COMPANY" Then
            command1.CommandText = "SELECT locationid,locationprefix,locationno FROM tblcompanylocation where accountid=" & txtAccountIDRelocate.Text.Trim & " order by locationno desc;"
        Else
            command1.CommandText = "SELECT locationid,locationprefix,locationno FROM tblPersonlocation where accountid=" & txtAccountIDRelocate.Text.Trim & " order by locationno desc;"
        End If

        command1.Connection = conn
        Dim dr As MySqlDataReader = command1.ExecuteReader()
        Dim dt As New System.Data.DataTable
        dt.Load(dr)
        If dt.Rows.Count > 0 Then
            Dim lastnum As Int64 = Convert.ToInt64(dt.Rows(0)("locationno"))
            lastnum = lastnum + 1

            txtLocationIDRelocateNew.Text = txtAccountIDRelocate.Text + "-" + lastnum.ToString("D4")
            txtLocationNo.Text = lastnum
            txtLocationPrefix.Text = ""
        Else
            txtLocationIDRelocateNew.Text = txtAccountIDRelocate.Text + "-0001"
            txtLocationPrefix.Text = ""
            txtLocationNo.Text = "1"
        End If

        conn.Close()
        conn.Dispose()

        command1.Dispose()
    End Sub

    Protected Sub btnClient1_Click(sender As Object, e As ImageClickEventArgs) Handles btnClient1.Click
        Try
            lblAlert.Text = ""

            'txtSearch.Text = "CustomerSearch"

            If String.IsNullOrEmpty(txtAccountIDRelocate.Text.Trim) = False Then
                txtPopUpClient.Text = ""

                txtPopUpClient.Text = txtAccountIDRelocate.Text
                txtPopupClientSearch.Text = txtPopUpClient.Text

                'If txtContactTypeRelocate.Text = "CORPORATE" Then
                '    SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.AccountId,  A.Name, A.ID From tblCompany A where (upper(A.Name) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper.Trim + "%"" or A.accountid like ""%" + txtPopupClientSearch.Text.Trim + "%"") order by AccountID, Name"
                'ElseIf txtContactTypeRelocate.Text = "RESIDENTIAL" Then
                '    SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,   A.AccountId,  A.Name, A.ID From tblPerson A where (upper(A.Name) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper.Trim + "%"" or A.accountid like ""%" + txtPopupClientSearch.Text.Trim + "%"") order by AccountID, Name"
                'End If


                If txtAccountType.Text = "COMPANY" Then
                    SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.AccountId,  A.Name, A.ID From tblCompany A where (upper(A.Name) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper.Trim + "%"" or A.accountid like ""%" + txtPopupClientSearch.Text.Trim + "%"") order by A.AccountID, A.Name"
                ElseIf txtAccountType.Text = "PERSON" Then
                    SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,   A.AccountId,  A.Name, A.ID From tblPerson A where (upper(A.Name) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper.Trim + "%"" or A.accountid like ""%" + txtPopupClientSearch.Text.Trim + "%"") order by A.AccountID, A.Name"
                End If
                'End If
                'SELECT B.CompanyID as ID,A.name,A.accountid,A.contactperson, b.LocationId, b.Address1 as ServiceAddress1  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and  A.AccountID like '" + txtPpclient.Text.Trim + "%' OR B.CompanyID like '%" + txtPpclient.Text.Trim + "%' OR A.NAME Like '%" + txtPpclient.Text.Trim + "%' OR B.LocationID Like '%" + txtPpclient.Text.Trim + "%' union (SELECT D.PersonID as ID,C.name,C.accountid,C.contactperson, D.LocationId, D.Address1 as ServiceAddress1 From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') AND C.AccountID like '" + txtPpclient.Text.Trim + "%' OR D.PersonID  like '%" + txtPpclient.Text.Trim + "%' OR C.NAME Like '%" + txtPpclient.Text.Trim + "%' OR D.LocationID Like '%" + txtPpclient.Text.Trim + "%') ORDER BY AccountId, LocationId

                SqlDSClient.DataBind()
                gvClient.DataBind()
                'updPanelInvoice.Update()
            Else
                txtPopUpClient.Text = ""

                'If txtContactTypeRelocate.Text = "CORPORATE" Then
                '    SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.AccountId,  A.Name, A.ID From tblCompany A where (upper(A.Name) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper.Trim + "%"" or A.accountid like ""%" + txtPopupClientSearch.Text.Trim + "%"") order by AccountID, Name"
                'ElseIf txtContactTypeRelocate.Text = "RESIDENTIAL" Then
                '    SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  A.AccountId,  A.Name, A.ID From tblPerson A where (upper(A.Name) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper.Trim + "%"" or A.accountid like ""%" + txtPopupClientSearch.Text.Trim + "%"") order by AccountID, Name"
                'End If


                If txtAccountType.Text = "COMPANY" Then
                    SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.AccountId,  A.Name, A.ID From tblCompany A where (upper(A.Name) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper.Trim + "%"" or A.accountid like ""%" + txtPopupClientSearch.Text.Trim + "%"") order by A.AccountID, A.Name"
                ElseIf txtAccountType.Text = "PERSON" Then
                    SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  A.AccountId,  A.Name, A.ID From tblPerson A where (upper(A.Name) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper.Trim + "%"" or A.accountid like ""%" + txtPopupClientSearch.Text.Trim + "%"") order by A.AccountID, A.Name"
                End If

                SqlDSClient.DataBind()
                gvClient.DataBind()
                'updPanelInvoice.Update()
            End If

            'txtCustomerSearch.Text = SqlDSClient.SelectCommand
            mdlPopUpClient.Show()
        Catch ex As Exception

            InsertIntoTblWebEventLog("RELOCATE - " + Session("UserID"), "btnClient1_Click", ex.Message.ToString, "")
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
        End Try
    End Sub

    Protected Sub txtPopUpClient_TextChanged(sender As Object, e As EventArgs) Handles txtPopUpClient.TextChanged
        'If txtPopUpClient.Text.Trim = "" Then
        '    MessageBox.Message.Alert(Page, "Please enter client name", "str")
        '    Exit Sub
        'End If

        Try


            'If txtContactTypeRelocate.Text = "CORPORATE" Then
            '    SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.AccountId,  A.Name, A.ID From tblCompany A  where (upper(A.Name) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%""  or A.accountid like """ + txtPopUpClient.Text + "%"" )  order by AccountID,  Name "

            'Else
            '    SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,   A.AccountId,  A.Name, A.ID From tblPerson A  where (upper(A.Name) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%""  or A.accountid like """ + txtPopUpClient.Text + "%"" )  order by AccountID,  Name"

            'End If


            If txtAccountType.Text = "COMPANY" Then
                SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.AccountId,  A.Name, A.ID From tblCompany A  where (upper(A.Name) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%""  or A.accountid like """ + txtPopUpClient.Text + "%"" )  order by AccountID,  Name "
            Else
                SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,   A.AccountId,  A.Name, A.ID From tblPerson A  where (upper(A.Name) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%""  or A.accountid like """ + txtPopUpClient.Text + "%"" )  order by AccountID,  Name"
            End If


            SqlDSClient.DataBind()
            gvClient.DataBind()
            mdlPopUpClient.Show()
            'txtIsPopup.Text = "Client"


        Catch ex As Exception
            'Dim exstr As String
            'exstr = ex.Message
            'lblAlert.Text = exstr

            InsertIntoTblWebEventLog("RELOCATE - " + Session("UserID"), "txtPopUpClient_TextChanged", ex.Message.ToString, "")
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
        End Try
    End Sub

    Protected Sub gvClient_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvClient.SelectedIndexChanged
        Try
            lblAlert.Text = ""
          
            If (gvClient.SelectedRow.Cells(2).Text = "&nbsp;") Then
                txtAccountIDRelocate.Text = ""
            Else
                txtAccountIDRelocate.Text = gvClient.SelectedRow.Cells(2).Text.Trim
            End If

            If (gvClient.SelectedRow.Cells(4).Text = "&nbsp;") Then
                txtBillingNameRelocate.Text = ""
            Else
                txtBillingNameRelocate.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(4).Text.Trim)
            End If


            'gvLocation.DataBind()
            mdlPopUpClient.Hide()
        Catch ex As Exception
            'Dim exstr As String
            'exstr = ex.Message
            'lblAlert.Text = exstr

            InsertIntoTblWebEventLog("RELOCATE - " + Session("UserID"), "gvClient_SelectedIndexChanged", ex.Message.ToString, "")
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
        End Try
    End Sub

    Protected Sub OnSelectedIndexChangedgClient(sender As Object, e As EventArgs) Handles gvClient.SelectedIndexChanged
        For Each row As GridViewRow In gvClient.Rows
            If row.RowIndex = gvClient.SelectedIndex Then
                row.BackColor = ColorTranslator.FromHtml("#738A9C")
                row.ToolTip = String.Empty
            Else
                row.BackColor = ColorTranslator.FromHtml("#E4E4E4")
                row.ToolTip = "Click to select this row."
            End If
        Next
    End Sub

    Protected Sub gvClient_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvClient.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='silver';this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#E4E4E4';")
            'e.Row.Attributes.Add("onmousedown", "this.style.backgroundColor='#738A9C';")
            'e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#738A9C';")

            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvClient, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

   
    Protected Sub gvClient_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvClient.PageIndexChanging
        Try
            gvClient.PageIndex = e.NewPageIndex

            'If txtClientFrom.Text = "ImportService" Then
            '    SqlDSClient.SelectCommand = txtImportService.Text
            'End If

            'If txtSearch.Text = "CustomerSearch" Then
            'SqlDSClient.SelectCommand = txtCustomerSearch.Text
            'End If
            SqlDSClient.DataBind()
            'If txtSearch.Text = "InvoiceSearch" Then
            '    SqlDSClient.SelectCommand = txtInvoiceSearch.Text
            'End If


            SqlDSClient.DataBind()
            gvClient.DataBind()
            mdlPopUpClient.Show()
        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString
            InsertIntoTblWebEventLog("RELOCATE - " + Session("UserID"), "gvClient_PageIndexChanging", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub btnPopUpClientReset_Click(sender As Object, e As ImageClickEventArgs) Handles btnPopUpClientReset.Click
        Try

            txtPopUpClient.Text = "Search Here for AccountID or Client Name or Contact Person"

            SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc,  B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') UNION  SELECT 'PERSON' as AccountType, C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') order by ServiceName"
          

            SqlDSClient.DataBind()
            gvClient.DataBind()
            mdlPopUpClient.Show()


        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString
            InsertIntoTblWebEventLog("RELOCATE - " + Session("UserID"), "btnPopUpClientReset_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub txtAccountType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtAccountType.SelectedIndexChanged
        Try
            txtAccountIDRelocate.Text = ""
            txtBillingNameRelocate.Text = ""
        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString
            InsertIntoTblWebEventLog("RELOCATE - " + Session("UserID"), "btnQuit_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

   
End Class

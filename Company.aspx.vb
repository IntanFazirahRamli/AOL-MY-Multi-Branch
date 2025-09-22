Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Collections.Generic
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Threading
'Imports Microsoft.Office.Interop.Word
'Imports Microsoft.Office.Interop.Excel

Imports Microsoft.VisualBasic
Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Drawing
Imports System.Reflection
Imports System.Globalization

Imports System.Web.Services
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Security.Cryptography
Imports System.Web.Script.Serialization
Imports System.Security.Cryptography.X509Certificates
Imports System.Threading.Tasks
Imports EInvoicing.EInvoice
Imports Microsoft.IdentityModel.Clients.ActiveDirectory
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Serialization

Imports NPOI.HSSF.UserModel
Imports NPOI.SS.UserModel
Imports NPOI.SS.Util
Imports NPOI.XSSF.UserModel

'Imports System.Web.UI.HtmlControls.HtmlIframe

Partial Class Company
    Inherits System.Web.UI.Page
    'End Class

    Public rcno As String
    Public svcrcno As String
    Private Shared gScheduler, gSalesman, gIncharge As String
    Public isInPage As Boolean = False
    Public Message As String = String.Empty
    ' To store the Error or Message
    Private Word As Microsoft.Office.Interop.Word.ApplicationClass
    ' The Interop Object for Word
    Private Excel As Microsoft.Office.Interop.Excel.ApplicationClass
    ' The Interop Object for Excel
    Private Unknown As Object = Type.Missing
    ' For passing Empty values
    Public Enum StatusType
        SUCCESS
        FAILED
    End Enum
    ' To Specify Success or Failure Types
    Public Status As StatusType
    Shared random As New Random()

    'Public Function IsValidEmailAddress(email As String) As Boolean
    '    Try
    '        IsValidEmailAddress = False
    '        'Dim emailChecked = New System.Net.Mail.MailAddress(email)
    '        'Return True

    '        IsValidEmailAddress = Regex.IsMatch(email.Trim(), "\A(?:[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?\.)+[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?)\Z");

    '        If (IsValidEmailAddress) Then
    '            Return True
    '        End If
    '        'Then
    '    Catch

    '        Return False
    '    End Try
    'End Function


    Private Sub FindCountry(source As String)

        Dim sqlstr As String
        sqlstr = ""

        If source = "office" Then
            sqlstr = "SELECT Country FROM tblCity where City = '" & ddlOffCity.Text & "'"
        ElseIf source = "billing" Then
            sqlstr = "SELECT Country FROM tblCity where City = '" & ddlBillCity.Text & "'"
        ElseIf source = "service" Then
            sqlstr = "SELECT Country FROM tblCity where City = '" & ddlCity.Text & "'"
        ElseIf source = "servicebilling" Then
            sqlstr = "SELECT Country FROM tblCity where City = '" & ddlBillCitySvc.Text & "'"
        End If

        Try

            Dim command As MySqlCommand = New MySqlCommand

            Dim conn As MySqlConnection = New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            command.CommandType = CommandType.Text
            command.CommandText = sqlstr
            command.Connection = conn

            Dim dr As MySqlDataReader = command.ExecuteReader()
            Dim dt As New System.Data.DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then

                If source = "office" Then
                    ddlOffCountry.Text = dt.Rows(0)("Country").ToString()

                ElseIf source = "billing" Then
                    ddlBillCountry.Text = dt.Rows(0)("Country").ToString()
                ElseIf source = "service" Then
                    ddlCountry.Text = dt.Rows(0)("Country").ToString()
                ElseIf source = "servicebilling" Then
                    ddlBillCountrySvc.Text = dt.Rows(0)("Country").ToString()
                End If

            End If
            dt.Clear()
            dt.Dispose()
            dr.Close()

            conn.Close()
            conn.Dispose()


        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "FUNCTION FINDCOUNTRY", ex.Message.ToString, sqlstr)

        End Try
    End Sub

    Public Sub MakeMeNull()
        Try
            lblMessage.Text = ""
            lblAlert.Text = ""
            txtMode.Text = ""
            lblAutoEmailInv.Text = "-"
            lblAutoEmailSOA.Text = "-"

            txtAccountID.Text = ""
            ddlStatus.SelectedIndex = 0
            'ddlStatus.SelectedItem.Text = "O"
            ddlCompanyGrp.SelectedIndex = 0
            ddlContractGrp.SelectedIndex = 0

            txtNameE.Text = ""
            txtNameO.Text = ""
            txtRegNo.Text = ""
            txtGSTRegNo.Text = ""
            txtWebsite.Text = ""
            txtStartDate.Text = ""
            ddlIndustry.SelectedIndex = 0
            ddlSalesMan.SelectedIndex = 0
            ddlIncharge.SelectedIndex = 0
            txtComments.Text = ""
            ddlTerms.SelectedIndex = 0
            ddlCurrency.SelectedIndex = 0
            'ddlCurrency.SelectedValue = "SGD"
            txtOffAddress1.Text = ""
            txtOffStreet.Text = ""
            txtOffBuilding.Text = ""
            ddlOffCity.SelectedIndex = 0
            ddlOffCountry.SelectedIndex = 0
            ddlOffState.SelectedIndex = 0
            txtOffPostal.Text = ""

            txtOffContactPerson.Text = ""
            txtOffPosition.Text = ""
            txtOffContactNo.Text = ""
            txtOffFax.Text = ""
            txtOffContact2.Text = ""
            txtOffEmail.Text = ""
            txtOffMobile.Text = ""

            txtOffCont1Name.Text = ""
            txtOffCont1Position.Text = ""
            txtOffCont1Tel.Text = ""
            txtOffCont1Fax.Text = ""
            txtOffCont1Tel2.Text = ""
            txtOffCont1Mobile.Text = ""
            txtOffCont1Email.Text = ""

            txtBillingName.Text = ""
            txtBillAddress.Text = ""
            txtBillStreet.Text = ""
            txtBillBuilding.Text = ""
            ddlBillCity.SelectedIndex = 0
            ddlBillState.SelectedIndex = 0
            ddlBillCountry.SelectedIndex = 0
            txtBillPostal.Text = ""

            txtBillCP1Contact.Text = ""
            txtBillCP1Position.Text = ""
            txtBillCP1Tel.Text = ""
            txtBillCP1Fax.Text = ""
            txtBillCP1Tel2.Text = ""
            txtBillCP1Mobile.Text = ""
            txtBillCP1Fax.Text = ""
            txtBillCP1Email.Text = ""

            txtBillCP2Contact.Text = ""
            txtBillCP2Position.Text = ""
            txtBillCP2Tel.Text = ""
            txtBillCP2Fax.Text = ""
            txtBillCP2Tel2.Text = ""
            txtBillCP2Mobile.Text = ""
            txtBillCP2Fax.Text = ""
            txtBillCP2Email.Text = ""

            chkOffAddr.Checked = False
            chkInactive.Checked = False

            rdbBillingSettings.SelectedIndex = 0

            txtRcno.Text = ""

            txtClientID.Text = ""
            txtAccountCode.Text = ""
            'ddlTerms.SelectedValue = "-1"
            ddlTerms.SelectedIndex = 0
            'txtCreatedOn.Text = ""
            'txtLocation.Text = ""
            chkSendStatementSOA.Checked = True
            chkSendStatementInv.Checked = False
            chkAutoEmailInvoice.Checked = False
            chkAutoEmailStatement.Checked = False
            chkRequireEBilling.Checked = False
            ddlDefaultInvoiceFormat.SelectedIndex = 0
            txtBillingOptionRemarks.Text = ""

            chkEmailNotifySchedule.Checked = False
            chkEmailNotifyJobProgress.Checked = False
            chkPhotosMandatory.Checked = False
            chkDisplayTimeInTimeOut.Checked = False

            ddlCurrencyEdit.SelectedIndex = 0
            ddlDefaultInvoiceFormatEdit.SelectedIndex = 0
            ddlTermsEdit.SelectedIndex = 0
            chkAutoEmailInvoiceEdit.Checked = False
            chkAutoEmailStatementEdit.Checked = False
            chkSendStatementInvEdit.Checked = False
            chkSendStatementSOAEdit.Checked = False
            txtBillingOptionRemarksEdit.Text = ""
            chkRequireEBillingEdit.Checked = False

            btnSpecificLocation.Text = "SPECIFIC LOCATION"
            txtCreditLimit.Text = "0.00"
            lblFileUploadCount.Text = "File Upload"

            lblCurrentVal.Text = ""
            lbl1To30Val.Text = ""
            lbl31To60Val.Text = ""
            lbl61To90Val.Text = ""
            lbl91To180Val.Text = ""
            lblMoreThan180Val.Text = ""
            lblTotalVal.Text = ""
            ddlLocation.SelectedIndex = 0

            chkSmartCustomer.Checked = False
            'updPanelContract1.Update()
            txtTINOld.Text = ""
            txtTIN.Text = ""
            txtSST.Text = ""

        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "FUNCTION MAKEMENULL", ex.Message.ToString, "")
        End Try
    End Sub

    Private Sub EnableControls()
        Try
            btnSave.Enabled = False
            btnSave.ForeColor = System.Drawing.Color.Gray
            btnCancel.Enabled = False
            btnCancel.ForeColor = System.Drawing.Color.Gray

            btnADD.Enabled = True
            btnADD.ForeColor = System.Drawing.Color.Black
            btnCopyAdd.Enabled = False
            btnCopyAdd.ForeColor = System.Drawing.Color.Gray
            btnDelete.Enabled = False
            btnDelete.ForeColor = System.Drawing.Color.Gray
            btnQuit.Enabled = True
            btnQuit.ForeColor = System.Drawing.Color.Black
            btnPrint.Enabled = True
            btnPrint.ForeColor = System.Drawing.Color.Black

            btnContract.Enabled = False
            btnContract.ForeColor = System.Drawing.Color.Gray
            btnTransactions.Enabled = False
            btnTransactions.ForeColor = System.Drawing.Color.Gray
            btnChangeStatus.Enabled = False
            btnChangeStatus.ForeColor = System.Drawing.Color.Gray

            btnSearchTIN.Enabled = False
            btnSearchTIN.ForeColor = System.Drawing.Color.Gray


            btnFilter.Enabled = True
            btnFilter.ForeColor = System.Drawing.Color.Black

            btnUpdateServiceContact.Enabled = False
            btnUpdateServiceContact.ForeColor = System.Drawing.Color.Gray


            'btnTransfersSvc.Enabled = True
            'btnTransfersSvc.ForeColor = System.Drawing.Color.Black

            'btnSvcAdd.Enabled = True
            'btnSvcCancel.Enabled = True
            'btnSvcEdit.Enabled = True
            'btnSvcSave.Enabled = True
            'btnSvcDelete.Enabled = True

            'btnSvcAdd.ForeColor = System.Drawing.Color.Black
            'btnSvcCancel.ForeColor = System.Drawing.Color.Black
            'btnSvcEdit.ForeColor = System.Drawing.Color.Black
            'btnSvcSave.ForeColor = System.Drawing.Color.Black
            'btnSvcDelete.ForeColor = System.Drawing.Color.Black
            'btnContract.Enabled = False
            'btnContract.ForeColor = System.Drawing.Color.Black

            txtAccountID.Enabled = False
            ddlStatus.Enabled = False
            ddlCompanyGrp.Enabled = False

            txtNameE.Enabled = False
            txtNameO.Enabled = False
            txtRegNo.Enabled = False
            txtGSTRegNo.Enabled = False
            txtWebsite.Enabled = False
            txtStartDate.Enabled = False
            ddlIndustry.Enabled = False
            ddlSalesMan.Enabled = False
            ddlIncharge.Enabled = False
            txtComments.Enabled = False
            ddlTerms.Enabled = False
            ddlCurrency.Enabled = False
            txtOffAddress1.Enabled = False
            txtOffStreet.Enabled = False
            txtOffBuilding.Enabled = False
            ddlOffCity.Enabled = False
            ddlOffCountry.Enabled = False
            ddlOffState.Enabled = False
            txtOffPostal.Enabled = False

            txtOffContactPerson.Enabled = False
            txtOffPosition.Enabled = False
            txtOffMobile.Enabled = False
            txtOffContactNo.Enabled = False
            txtOffFax.Enabled = False
            txtOffContact2.Enabled = False
            txtOffEmail.Enabled = False
            txtOffMobile.Enabled = False

            txtOffCont1Name.Enabled = False
            txtOffCont1Position.Enabled = False
            txtOffCont1Tel.Enabled = False
            txtOffCont1Fax.Enabled = False
            txtOffCont1Tel2.Enabled = False
            txtOffCont1Mobile.Enabled = False
            txtOffCont1Email.Enabled = False

            txtBillingName.Enabled = False
            txtBillAddress.Enabled = False
            txtBillStreet.Enabled = False
            txtBillBuilding.Enabled = False
            ddlBillCity.Enabled = False
            ddlBillState.Enabled = False
            ddlBillCountry.Enabled = False
            txtBillPostal.Enabled = False

            txtBillCP1Contact.Enabled = False
            txtBillCP1Position.Enabled = False
            txtBillCP1Tel.Enabled = False
            txtBillCP1Fax.Enabled = False
            txtBillCP1Tel2.Enabled = False
            txtBillCP1Mobile.Enabled = False
            txtBillCP1Fax.Enabled = False
            txtBillCP1Email.Enabled = False

            txtBillCP2Contact.Enabled = False
            txtBillCP2Position.Enabled = False
            txtBillCP2Tel.Enabled = False
            txtBillCP2Fax.Enabled = False
            txtBillCP2Tel2.Enabled = False
            txtBillCP2Mobile.Enabled = False
            txtBillCP2Fax.Enabled = False
            txtBillCP2Email.Enabled = False

            chkOffAddr.Enabled = False
            chkInactive.Enabled = False

            rdbBillingSettings.Enabled = False

            Label24.Visible = False
            btnUpdateBilling.Visible = False
            chkSendStatementInv.Enabled = False
            chkSendStatementSOA.Enabled = False
            ddlLocation.Enabled = False
            ddlDefaultInvoiceFormat.Enabled = False
            btnEditSendStatement.Visible = True
            chkAutoEmailInvoice.Enabled = False
            chkAutoEmailStatement.Enabled = False
            txtBillingOptionRemarks.Enabled = False
            chkRequireEBilling.Enabled = False

            chkEmailNotifySchedule.Enabled = False
            chkEmailNotifyJobProgress.Enabled = False
            chkPhotosMandatory.Enabled = False
            chkDisplayTimeInTimeOut.Enabled = False

            txtCreditLimit.Enabled = False
            chkSmartCustomer.Enabled = False
            chkStatusLoc.Enabled = False

            txtTIN.Enabled = False
            txtSST.Enabled = False

            'ddlCurrencyEdit.Enabled = False
            'ddlDefaultInvoiceFormatEdit.Enabled = False
            'ddlTermsEdit.Enabled = False
            'chkAutoEmailInvoiceEdit.Enabled = False
            'chkSendStatementEdit.Enabled = False

            ' '''''''''''''''''''Access Control 
            'Dim conn As MySqlConnection = New MySqlConnection()
            'Dim command As MySqlCommand = New MySqlCommand

            'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            'conn.Open()

            'command.CommandType = CommandType.Text
            'command.CommandText = "SELECT x2412, x2413, x0302, x0302Add, x0302Edit, x0302EditAcct, x0302Delete, x0302Trans, x0302Notes, x0302Change FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"
            'command.Connection = conn

            'Dim dr As MySqlDataReader = command.ExecuteReader()
            'Dim dt As New System.Data.DataTable
            'dt.Load(dr)

            'If dt.Rows.Count > 0 Then

            '    If String.IsNullOrEmpty(dt.Rows(0)("x0302")) = False Then
            '        If dt.Rows(0)("x0302").ToString() = False Then
            '            Response.Redirect("Home.aspx")
            '        End If
            '    End If

            '    If String.IsNullOrEmpty(dt.Rows(0)("x2412")) = False Then
            '        If Convert.ToBoolean(dt.Rows(0)("x2412")) = False Then
            '            btnContract.Enabled = dt.Rows(0)("x2412").ToString()
            '        End If
            '    End If

            '    If String.IsNullOrEmpty(dt.Rows(0)("x2412")) = False Then
            '        If Convert.ToBoolean(dt.Rows(0)("x2412")) = False Then
            '            btnSvcContract.Enabled = dt.Rows(0)("x2412").ToString()
            '        End If
            '    End If


            '    If String.IsNullOrEmpty(dt.Rows(0)("x2413")) = False Then
            '        If Convert.ToBoolean(dt.Rows(0)("x2413")) = False Then
            '            btnSvcService.Enabled = dt.Rows(0)("x2413").ToString()
            '        End If
            '    End If


            '    If String.IsNullOrEmpty(dt.Rows(0)("x0302Add")) = False Then
            '        btnADD.Enabled = dt.Rows(0)("x0302Add").ToString()
            '    End If

            '    If String.IsNullOrEmpty(dt.Rows(0)("x0302Edit")) = False Then
            '        btnCopyAdd.Enabled = dt.Rows(0)("x0302Edit").ToString()
            '    End If

            '    If String.IsNullOrEmpty(dt.Rows(0)("x0302Delete")) = False Then
            '        btnDelete.Enabled = dt.Rows(0)("x0302Delete").ToString()
            '    End If

            '    'If String.IsNullOrEmpty(dt.Rows(0)("x2412")) = False Then
            '    '    btnContract.Enabled = dt.Rows(0)("x2412").ToString()
            '    'End If


            '    If btnADD.Enabled = True Then
            '        btnADD.ForeColor = System.Drawing.Color.Black
            '    Else
            '        btnADD.ForeColor = System.Drawing.Color.Gray
            '    End If


            '    If btnCopyAdd.Enabled = True Then
            '        btnCopyAdd.ForeColor = System.Drawing.Color.Black
            '    Else
            '        btnCopyAdd.ForeColor = System.Drawing.Color.Gray
            '    End If

            '    If btnDelete.Enabled = True Then
            '        btnDelete.ForeColor = System.Drawing.Color.Black
            '    Else
            '        btnDelete.ForeColor = System.Drawing.Color.Gray
            '    End If


            '    If btnContract.Enabled = True Then
            '        btnContract.ForeColor = System.Drawing.Color.Black
            '    Else
            '        btnContract.ForeColor = System.Drawing.Color.Gray
            '    End If

            '    If btnSvcContract.Enabled = True Then
            '        btnSvcContract.ForeColor = System.Drawing.Color.Black
            '    Else
            '        btnSvcContract.ForeColor = System.Drawing.Color.Gray
            '    End If

            '    If btnSvcService.Enabled = True Then
            '        btnSvcService.ForeColor = System.Drawing.Color.Black
            '    Else
            '        btnSvcService.ForeColor = System.Drawing.Color.Gray
            '    End If


            '    ''If btnPrint.Enabled = True Then
            '    ''    btnPrint.ForeColor = System.Drawing.Color.Black
            '    ''Else
            '    ''    btnPrint.ForeColor = System.Drawing.Color.Gray
            '    ''End If
            '    ''btnApprModeOnOff.Visible = vpSec2413ApprMode
            'End If

            ' '''''''''''''''''''Access Control 

        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "FUNCTION ENABLECONTROLS", ex.Message.ToString, "")
        End Try
    End Sub

    Private Sub DisableControls()
        Try
            btnSave.Enabled = True
            btnSave.ForeColor = System.Drawing.Color.Black
            btnCancel.Enabled = True
            btnCancel.ForeColor = System.Drawing.Color.Black

            btnADD.Enabled = False
            btnADD.ForeColor = System.Drawing.Color.Gray

            btnCopyAdd.Enabled = False
            btnCopyAdd.ForeColor = System.Drawing.Color.Gray

            btnDelete.Enabled = False
            btnDelete.ForeColor = System.Drawing.Color.Gray

            btnQuit.Enabled = False
            btnQuit.ForeColor = System.Drawing.Color.Gray

            btnPrint.Enabled = False
            btnPrint.ForeColor = System.Drawing.Color.Gray
            btnTransactions.Enabled = False
            btnTransactions.ForeColor = System.Drawing.Color.Gray
            btnChangeStatus.Enabled = False
            btnChangeStatus.ForeColor = System.Drawing.Color.Gray

            btnSearchTIN.Enabled = False
            btnSearchTIN.ForeColor = System.Drawing.Color.Gray

            btnUpdateServiceContact.Enabled = False
            btnUpdateServiceContact.ForeColor = System.Drawing.Color.Gray

            btnSvcAdd.Enabled = False
            btnSvcCancel.Enabled = False
            btnSvcEdit.Enabled = False
            btnSvcCopy.Enabled = False
            btnSvcSave.Enabled = False
            btnSvcDelete.Enabled = False

            btnSvcAdd.ForeColor = System.Drawing.Color.Gray
            btnSvcCancel.ForeColor = System.Drawing.Color.Gray
            btnSvcEdit.ForeColor = System.Drawing.Color.Gray
            btnSvcCopy.ForeColor = System.Drawing.Color.Gray
            btnSvcSave.ForeColor = System.Drawing.Color.Gray
            btnSvcDelete.ForeColor = System.Drawing.Color.Gray

            btnSvcContract.Enabled = False
            btnSvcContract.ForeColor = System.Drawing.Color.Gray

            btnSvcService.Enabled = False
            btnSvcService.ForeColor = System.Drawing.Color.Gray

            btnContract.Enabled = False
            btnContract.ForeColor = System.Drawing.Color.Gray

            btnFilter.Enabled = False
            btnFilter.ForeColor = System.Drawing.Color.Gray

            btnTransfersSvc.Enabled = False
            btnTransfersSvc.ForeColor = System.Drawing.Color.Gray

            btnSpecificLocation.Enabled = False
            btnSpecificLocation.ForeColor = System.Drawing.Color.Gray


            txtAccountID.Enabled = True
            ddlStatus.Enabled = True
            ddlCompanyGrp.Enabled = True

            txtNameE.Enabled = True
            txtNameO.Enabled = True
            txtRegNo.Enabled = True
            txtGSTRegNo.Enabled = True
            txtWebsite.Enabled = True
            txtStartDate.Enabled = True
            ddlIndustry.Enabled = True
            ddlSalesMan.Enabled = True
            ddlIncharge.Enabled = True
            txtComments.Enabled = True
            ddlTerms.Enabled = True
            ddlCurrency.Enabled = True
            txtOffAddress1.Enabled = True
            txtOffStreet.Enabled = True
            txtOffBuilding.Enabled = True
            ddlOffCity.Enabled = True
            ddlOffCountry.Enabled = True
            ddlOffState.Enabled = True
            txtOffPostal.Enabled = True

            txtOffContactPerson.Enabled = True
            txtOffPosition.Enabled = True
            txtOffMobile.Enabled = True
            txtOffContactNo.Enabled = True
            txtOffFax.Enabled = True
            txtOffContact2.Enabled = True
            txtOffEmail.Enabled = True
            txtOffMobile.Enabled = True

            txtOffCont1Name.Enabled = True
            txtOffCont1Position.Enabled = True
            txtOffCont1Tel.Enabled = True
            txtOffCont1Fax.Enabled = True
            txtOffCont1Tel2.Enabled = True
            txtOffCont1Mobile.Enabled = True
            txtOffCont1Email.Enabled = True

            txtBillingName.Enabled = True
            txtBillAddress.Enabled = True
            txtBillStreet.Enabled = True
            txtBillBuilding.Enabled = True
            ddlBillCity.Enabled = True
            ddlBillState.Enabled = True
            ddlBillCountry.Enabled = True
            txtBillPostal.Enabled = True

            txtBillCP1Contact.Enabled = True
            txtBillCP1Position.Enabled = True
            txtBillCP1Tel.Enabled = True
            txtBillCP1Fax.Enabled = True
            txtBillCP1Tel2.Enabled = True
            txtBillCP1Mobile.Enabled = True
            txtBillCP1Fax.Enabled = True
            txtBillCP1Email.Enabled = True

            txtBillCP2Contact.Enabled = True
            txtBillCP2Position.Enabled = True
            txtBillCP2Tel.Enabled = True
            txtBillCP2Fax.Enabled = True
            txtBillCP2Tel2.Enabled = True
            txtBillCP2Mobile.Enabled = True
            txtBillCP2Fax.Enabled = True
            txtBillCP2Email.Enabled = True
            ddlDefaultInvoiceFormat.Enabled = True
            txtBillingOptionRemarks.Enabled = True

            chkOffAddr.Enabled = True
            '   chkInactive.Enabled = True

            rdbBillingSettings.Enabled = True

            btnUpdateBilling.Visible = False
            chkSendStatementInv.Enabled = True
            chkSendStatementSOA.Enabled = True
            ddlLocation.Enabled = True

            btnEditSendStatement.Visible = False
            chkAutoEmailInvoice.Enabled = True
            chkAutoEmailStatement.Enabled = True
            chkRequireEBilling.Enabled = True

            chkEmailNotifySchedule.Enabled = True
            chkEmailNotifyJobProgress.Enabled = True
            chkPhotosMandatory.Enabled = True
            chkDisplayTimeInTimeOut.Enabled = True

            txtCreditLimit.Enabled = True
            chkSmartCustomer.Enabled = True

            chkStatusLoc.Enabled = False


            txtTIN.Enabled = True
            txtSST.Enabled = True

            'ddlCurrencyEdit.Enabled = True
            'ddlDefaultInvoiceFormatEdit.Enabled = True
            'ddlTermsEdit.Enabled = True
            'chkAutoEmailInvoiceEdit.Enabled = True
            'chkSendStatementEdit.Enabled = True
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "FUNCTION DISABLECONTROLS", ex.Message.ToString, "")
        End Try
    End Sub

    Protected Sub GridView1_DataBound(sender As Object, e As EventArgs) Handles GridView1.DataBound
        'If Session("contractfrom") = "clients" Then
        '    'If String.IsNullOrEmpty(Session("servicefrom")) = False Then
        '    If (isInPage = False) Then
        '        GridView1.PageIndex = GridView1.PageIndex + 1
        '        GridView1.DataBind()
        '    End If
        '    'Session.Remove("contractfrom")
        'End If


    End Sub

    Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        Try
            GridView1.PageIndex = e.NewPageIndex
            SqlDataSource1.SelectCommand = txt.Text
            SqlDataSource1.DataBind()
            GridView1.DataBind()
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "GridView1_PageIndexChanging", ex.Message.ToString, "")
        End Try
    End Sub

    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        'If e.Row.RowType = DataControlRowType.DataRow Then
        '    If String.IsNullOrEmpty(txtSelectedRow.Text) = True Then
        '        'txtComments.Text = e.Row.Cells(4).Text
        '        If txtRcno.Text = e.Row.Cells(4).Text Then
        '            isInPage = True
        '            txtSelectedRow.Text = e.Row.RowIndex
        '        End If

        '    End If
        'End If

    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        'If txtMode.Text = "EDIT" Then
        '    lblAlert.Text = "CANNOT SELECT RECORD IN EDIT MODE. SAVE OR CANCEL TO PROCEED"
        '    Return
        'End If
        Try
            If txtMode.Text = "NEW" Or txtMode.Text = "EDIT" Then
                lblAlert.Text = "CANNOT SELECT RECORD IN ADD/EDIT MODE. SAVE OR CANCEL TO PROCEED"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                Return
            End If

            Dim editindex As Integer

            If (Session("servicefrom")) = "contactC" Or Session("relocatefrom") = "relocateC" Then

                MakeMeNull()
                MakeSvcNull()
                txtRcno.Text = Session("rcno")
                ddlCompanyGrp.SelectedIndex = -1
                ddlIndustry.SelectedIndex = -1
                ddlSalesMan.SelectedIndex = -1
                ddlIncharge.SelectedIndex = -1
                ddlTerms.SelectedIndex = -1
                ddlOffCity.SelectedIndex = -1
                ddlOffCountry.SelectedIndex = -1
                ddlOffState.SelectedIndex = -1
                ddlBillCity.SelectedIndex = -1
                ddlBillCountry.SelectedIndex = -1
                ddlBillState.SelectedIndex = -1
                ddlTerms.SelectedIndex = -1
                ddlCurrency.SelectedIndex = -1

                ddlCurrencyEdit.SelectedIndex = -1
                ddlDefaultInvoiceFormatEdit.SelectedIndex = -1
                ddlTermsEdit.SelectedIndex = -1
              

                'Session.Remove("servicefrom")
            ElseIf (Session("contractfrom")) = "clients" Then

                MakeMeNull()
                MakeSvcNull()
                txtRcno.Text = Session("rcno")

                ddlCompanyGrp.SelectedIndex = -1
                ddlIndustry.SelectedIndex = -1
                ddlSalesMan.SelectedIndex = -1
                ddlIncharge.SelectedIndex = -1
                ddlTerms.SelectedIndex = -1
                ddlCurrency.SelectedIndex = -1

                ddlOffCity.SelectedIndex = -1
                ddlOffCountry.SelectedIndex = -1
                ddlOffState.SelectedIndex = -1
                ddlBillCity.SelectedIndex = -1
                ddlBillCountry.SelectedIndex = -1
                ddlBillState.SelectedIndex = -1

                ddlCurrencyEdit.SelectedIndex = -1
                ddlDefaultInvoiceFormatEdit.SelectedIndex = 1 -
                ddlTermsEdit.SelectedIndex = -1
                
            ElseIf (Session("customerfrom")) = "Corporate" Then
                Session.Remove("customerfrom")
                MakeMeNull()
                MakeSvcNull()
                txtRcno.Text = Session("rcno")

                ddlCompanyGrp.SelectedIndex = -1
                ddlIndustry.SelectedIndex = -1
                ddlSalesMan.SelectedIndex = -1
                ddlIncharge.SelectedIndex = -1
                ddlTerms.SelectedIndex = -1
                ddlCurrency.SelectedIndex = -1

                ddlOffCity.SelectedIndex = -1
                ddlOffCountry.SelectedIndex = -1
                ddlOffState.SelectedIndex = -1
                ddlBillCity.SelectedIndex = -1
                ddlBillCountry.SelectedIndex = -1
                ddlBillState.SelectedIndex = -1

                ddlCurrencyEdit.SelectedIndex = -1
                ddlDefaultInvoiceFormatEdit.SelectedIndex = -1
                ddlTermsEdit.SelectedIndex = -1
             
                ' Session.Remove("contractfrom")
                'Dim editindex As Integer = GridView1.SelectedIndex
                'rcno = DirectCast(GridView1.Rows(editindex).FindControl("Label1"), Label).Text
                'txtRcno.Text = rcno.ToString()
            Else

                MakeMeNull()
                MakeSvcNull()
                'EnableControls()
                MakeNotesNull()
                MakeCPNull()

                btnCopyAdd.Enabled = True
                btnCopyAdd.ForeColor = System.Drawing.Color.Black
                btnDelete.Enabled = True
                btnDelete.ForeColor = System.Drawing.Color.Black

                btnTransactions.Enabled = True
                btnTransactions.ForeColor = System.Drawing.Color.Black
                btnChangeStatus.Enabled = True
                btnChangeStatus.ForeColor = System.Drawing.Color.Black

                btnFilter.Enabled = True
                btnFilter.ForeColor = System.Drawing.Color.Black

                btnUpdateServiceContact.Enabled = True
                btnUpdateServiceContact.ForeColor = System.Drawing.Color.Black

                editindex = GridView1.SelectedIndex
                rcno = DirectCast(GridView1.Rows(editindex).FindControl("Label1"), Label).Text
                txtRcno.Text = rcno.ToString()

                btnUpdateBilling.Visible = True
            End If
        
            PopulateRecord()
            RetrieveAutoEmailInfo()

            GridView1.SelectedIndex = editindex

            If (Session("contractfrom")) = "clients" Or Session("servicefrom") = "contactC" Then

                SqlDataSource1.SelectCommand = txt.Text
                SqlDataSource1.DataBind()
                'GridView1.AllowPaging = False
                GridView1.DataBind()


                'GridView1.DataSource = txt.Text
                'GridView1.DataBind()

                'Session("gridsqlCompany") = txt.Text
                'Dim pi As Integer = Session("gridview1companyPI")
                'Dim ri As Integer = Session("gridview1companyRI")
                GridView1.PageIndex = Session("gridview1companyPI")
                GridView1.SelectedIndex = Session("gridview1companyRI")

                Session.Remove("gridview1companyPI")
                Session.Remove("gridview1companyRI")
                'Session("gridview1personPI") = GridView1.PageIndex
                'Session("gridview1personRI") = GridView1.SelectedIndex
            End If

            'If chkInactive.Checked = True Then
            '    'btnChangeStatus.Enabled = False
            '    'btnChangeStatus.ForeColor = System.Drawing.Color.Gray

            '    btnUpdateServiceContact.Enabled = False
            '    btnUpdateServiceContact.ForeColor = System.Drawing.Color.Gray
            'End If

            ''Session("contractfrom") = "clients"
            'If String.IsNullOrEmpty(Session("contractfrom")) = False Then
            '    GridView1.SelectedIndex = Convert.ToInt32(txtSelectedRow.Text)
            '    Session("contractfrom") = ""
            'End If

            'If String.IsNullOrEmpty(Session("servicefrom")) = False Then
            'GridView1.SelectedIndex = Convert.ToInt32(txtSelectedRow.Text)
            'End If
            'Try

            '    System.Threading.Thread.Sleep(1000)
            '    'Label1.Visible = True
            '    Dim conn As MySqlConnection = New MySqlConnection()

            '    conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            '    conn.Open()
            '    Dim command1 As MySqlCommand = New MySqlCommand

            '    command1.CommandType = CommandType.Text

            '    command1.CommandText = "SELECT *,UPPER(Salesman),UPPER(LocateGrp),UPPER(Industry) FROM tblcompany where rcno=" & Convert.ToInt32(txtRcno.Text)
            '    command1.Connection = conn

            '    Dim dr As MySqlDataReader = command1.ExecuteReader()
            '    Dim dt As New System.Data.DataTable
            '    dt.Load(dr)

            '    If dt.Rows.Count > 0 Then

            '        txtAccountID.Text = dt.Rows(0)("AccountID").ToString
            '        txtAccountIDtab2.Text = txtAccountID.Text
            '        lblAccountID.Text = txtAccountID.Text

            '        lblAccountID2.Text = txtAccountID.Text

            '        txtAccountIDSelected.Text = txtAccountID.Text

            '        If dt.Rows(0)("Status").ToString <> "" Then
            '            ddlStatus.Text = dt.Rows(0)("Status").ToString
            '        End If
            '        If dt.Rows(0)("Inactive").ToString = "1" Then
            '            chkInactive.Checked = True
            '        Else
            '            chkInactive.Checked = False
            '        End If
            '        If dt.Rows(0)("CompanyGroup").ToString <> "" Then
            '            ddlCompanyGrp.Text = dt.Rows(0)("CompanyGroup").ToString
            '        End If
            '        txtNameE.Text = dt.Rows(0)("Name").ToString
            '        lblName.Text = txtNameE.Text
            '        lblName2.Text = txtNameE.Text
            '        txtNameO.Text = dt.Rows(0)("Name2").ToString
            '        txtRegNo.Text = dt.Rows(0)("RocNos").ToString
            '        txtGSTRegNo.Text = dt.Rows(0)("GstNos").ToString

            '        txtWebsite.Text = dt.Rows(0)("Website").ToString
            '        If dt.Rows(0)("StartDate").ToString = DBNull.Value.ToString Then
            '        Else

            '            txtStartDate.Text = Convert.ToDateTime(dt.Rows(0)("StartDate")).ToString("dd/MM/yyyy")
            '        End If

            '        If dt.Rows(0)("UPPER(Industry)").ToString <> "" Then

            '            gSalesman = dt.Rows(0)("Industry").ToString.ToUpper()

            '            If ddlIndustry.Items.FindByValue(gSalesman) Is Nothing Then
            '                ddlIndustry.Items.Add(gSalesman)
            '                ddlIndustry.Text = gSalesman
            '            Else
            '                ddlIndustry.Text = dt.Rows(0)("Industry").ToString.Trim().ToUpper()
            '            End If
            '        End If

            '        'If dt.Rows(0)("UPPER(Industry)").ToString <> "" Then
            '        '    ddlIndustry.Text = dt.Rows(0)("UPPER(Industry)").ToString
            '        'End If

            '        'If String.IsNullOrEmpty(dt.Rows(0)("UPPER(Industry)").ToString) = False Then
            '        '    ddlIndustry.Text = dt.Rows(0)("UPPER(Industry)").ToString
            '        'End If
            '        'If dt.Rows(0)("UPPER(Salesman)").ToString <> "" Then
            '        '    ddlSalesMan.Text = dt.Rows(0)("UPPER(Salesman)").ToString
            '        'End If

            '        If dt.Rows(0)("UPPER(Salesman)").ToString <> "" Then

            '            gSalesman = dt.Rows(0)("SalesMan").ToString.ToUpper()

            '            If ddlSalesMan.Items.FindByValue(gSalesman) Is Nothing Then
            '                ddlSalesMan.Items.Add(gSalesman)
            '                ddlSalesMan.Text = gSalesman
            '            Else
            '                ddlSalesMan.Text = dt.Rows(0)("SalesMan").ToString.Trim().ToUpper()
            '            End If
            '        End If


            '        'If dt.Rows(0)("Inchargeid").ToString <> "" Then
            '        '    ddlIncharge.Text = dt.Rows(0)("Inchargeid").ToString
            '        'End If


            '        If dt.Rows(0)("Inchargeid").ToString <> "" Then
            '            'Dim gSalesman As String

            '            gSalesman = dt.Rows(0)("Inchargeid").ToString.ToUpper()

            '            If ddlIncharge.Items.FindByValue(gSalesman) Is Nothing Then
            '                ddlIncharge.Items.Add(gSalesman)
            '                ddlIncharge.Text = gSalesman
            '            Else
            '                ddlIncharge.Text = dt.Rows(0)("Inchargeid").ToString.Trim().ToUpper()
            '            End If
            '        End If


            '        If dt.Rows(0)("ArTerm").ToString <> "" Then

            '            gSalesman = dt.Rows(0)("ArTerm").ToString.ToUpper()

            '            If ddlTerms.Items.FindByValue(gSalesman) Is Nothing Then
            '                ddlTerms.Items.Add(gSalesman)
            '                ddlTerms.Text = gSalesman
            '            Else
            '                ddlTerms.Text = dt.Rows(0)("ArTerm").ToString.Trim().ToUpper()
            '            End If
            '        End If

            '        If dt.Rows(0)("ArCurrency").ToString <> "" Then
            '            'Dim gSalesman As String

            '            gSalesman = dt.Rows(0)("ArCurrency").ToString.ToUpper()

            '            If ddlCurrency.Items.FindByValue(gSalesman) Is Nothing Then
            '                ddlCurrency.Items.Add(gSalesman)
            '                ddlCurrency.Text = gSalesman
            '            Else
            '                ddlCurrency.Text = dt.Rows(0)("ArCurrency").ToString.Trim().ToUpper()
            '            End If
            '        End If
            '        txtCommentsSvc.Text = dt.Rows(0)("Comments").ToString
            '        txtOffAddress1.Text = dt.Rows(0)("Address1").ToString
            '        txtOffStreet.Text = dt.Rows(0)("AddStreet").ToString


            '        txtOffBuilding.Text = dt.Rows(0)("AddBuilding").ToString
            '        If dt.Rows(0)("AddCity").ToString <> "" Then
            '            ddlOffCity.Text = dt.Rows(0)("AddCity").ToString
            '        End If

            '        If dt.Rows(0)("AddCountry").ToString <> "" Then
            '            If Server.HtmlDecode(dt.Rows(0)("AddCountry")).ToString = "S'pore" Or Server.HtmlDecode(dt.Rows(0)("AddCountry")).ToString = "S'PORE" Then
            '                ddlOffCountry.Text = "SINGAPORE"
            '            Else
            '                ddlOffCountry.Text = dt.Rows(0)("AddCountry").ToString
            '            End If
            '        End If
            '        If dt.Rows(0)("AddState").ToString <> "" Then
            '            ddlOffState.Text = dt.Rows(0)("AddState").ToString
            '        End If

            '        txtOffPostal.Text = dt.Rows(0)("AddPostal").ToString

            '        txtOffContactPerson.Text = dt.Rows(0)("ContactPerson").ToString
            '        txtOffPosition.Text = dt.Rows(0)("OffContactPosition").ToString
            '        txtOffMobile.Text = dt.Rows(0)("Mobile").ToString

            '        txtOffContactNo.Text = dt.Rows(0)("Telephone").ToString
            '        txtOffFax.Text = dt.Rows(0)("Fax").ToString
            '        txtOffContact2.Text = dt.Rows(0)("Telephone2").ToString
            '        txtOffEmail.Text = dt.Rows(0)("Email").ToString

            '        txtOffCont1Name.Text = dt.Rows(0)("OffContact1").ToString
            '        txtOffCont1Position.Text = dt.Rows(0)("OffContact1Position").ToString
            '        txtOffCont1Tel.Text = dt.Rows(0)("OffContact1Tel").ToString
            '        txtOffCont1Fax.Text = dt.Rows(0)("OffContact1Fax").ToString
            '        txtOffCont1Tel2.Text = dt.Rows(0)("OffContact1Tel2").ToString
            '        txtOffCont1Mobile.Text = dt.Rows(0)("OffContact1Mobile").ToString
            '        txtOffCont1Email.Text = dt.Rows(0)("ContactPersonEmail").ToString

            '        If dt.Rows(0)("BillingAddress").ToString = "1" Then
            '            chkOffAddr.Checked = True
            '        Else
            '            chkOffAddr.Checked = False
            '        End If

            '        txtBillingName.Text = dt.Rows(0)("BillingName").ToString

            '        txtBillAddress.Text = dt.Rows(0)("BillAddress1").ToString
            '        txtBillStreet.Text = dt.Rows(0)("BillStreet").ToString
            '        txtBillBuilding.Text = dt.Rows(0)("BillBuilding").ToString
            '        If dt.Rows(0)("BillCity").ToString <> "" Then
            '            ddlBillCity.Text = dt.Rows(0)("BillCity").ToString
            '        End If

            '        If dt.Rows(0)("BillCountry").ToString <> "" Then
            '            If Server.HtmlDecode(dt.Rows(0)("BillCountry")).ToString = "S'pore" Or Server.HtmlDecode(dt.Rows(0)("AddCountry")).ToString = "S'PORE" Then
            '                ddlBillCountry.Text = "SINGAPORE"
            '            Else
            '                ddlBillCountry.Text = dt.Rows(0)("BillCountry").ToString
            '            End If
            '        End If

            '        If dt.Rows(0)("BillState").ToString <> "" Then
            '            ddlBillState.Text = dt.Rows(0)("BillState").ToString
            '        End If
            '        txtBillPostal.Text = dt.Rows(0)("BillPostal").ToString

            '        txtBillCP1Contact.Text = dt.Rows(0)("BillContactPerson").ToString
            '        txtBillCP1Position.Text = dt.Rows(0)("BillContact1Position").ToString
            '        txtBillCP1Tel.Text = dt.Rows(0)("BillTelephone").ToString
            '        txtBillCP1Fax.Text = dt.Rows(0)("BillFax").ToString
            '        txtBillCP1Tel2.Text = dt.Rows(0)("BillTelephone2").ToString
            '        txtBillCP1Mobile.Text = dt.Rows(0)("BillMobile").ToString
            '        txtBillCP1Email.Text = dt.Rows(0)("BillContact1Email").ToString

            '        txtBillCP2Contact.Text = dt.Rows(0)("BillContact2").ToString
            '        txtBillCP2Position.Text = dt.Rows(0)("BillContact2Position").ToString
            '        txtBillCP2Tel.Text = dt.Rows(0)("BillContact2Tel").ToString
            '        txtBillCP2Fax.Text = dt.Rows(0)("BillContact2Fax").ToString
            '        txtBillCP2Tel2.Text = dt.Rows(0)("BillContact2Tel2").ToString
            '        txtBillCP2Mobile.Text = dt.Rows(0)("BillContact2Mobile").ToString
            '        txtBillCP2Email.Text = dt.Rows(0)("BillContact2Email").ToString

            '        If String.IsNullOrEmpty(dt.Rows(0)("BillingSettings").ToString) = False Then
            '            If dt.Rows(0)("BillingSettings").ToString = "AccountID" Then
            '                rdbBillingSettings.SelectedIndex = 0
            '            ElseIf dt.Rows(0)("BillingSettings").ToString = "LocationID" Then
            '                rdbBillingSettings.SelectedIndex = 1
            '            ElseIf dt.Rows(0)("BillingSettings").ToString = "ContractNo" Then
            '                rdbBillingSettings.SelectedIndex = 2
            '            ElseIf dt.Rows(0)("BillingSettings").ToString = "ServiceLocationCode" Then
            '                rdbBillingSettings.SelectedIndex = 3
            '            End If

            '        End If

            '        'If String.IsNullOrEmpty(dt.Rows(0)("BillingSettings").ToString) = False Then
            '        '    rdbBillingSettings.SelectedItem.Text = dt.Rows(0)("BillingSettings").ToString
            '        'End If

            '        'Tab panel - service location

            '        txtClientID.Text = dt.Rows(0)("ID").ToString
            '        txtAccountCode.Text = dt.Rows(0)("AccountNo").ToString

            '        If String.IsNullOrEmpty(txtAccountID.Text) Then
            '            SqlDataSource2.SelectCommand = "SELECT * FROM tblcompanylocation where accountid is null and companyid = '" & txtClientID.Text & "'"
            '        Else
            '            SqlDataSource2.SelectCommand = "SELECT * FROM tblcompanylocation where accountid = " & txtAccountID.Text
            '        End If

            '        SqlDataSource2.DataBind()
            '        GridView2.DataBind()

            '        txtDetail.Text = SqlDataSource2.SelectCommand

            '        SqlDSNotesMaster.SelectCommand = "select * from tblnotes where keyfield = '" + txtAccountID.Text + "'"
            '        SqlDSNotesMaster.DataBind()
            '        gvNotesMaster.DataBind()

            '        'View Uploaded files

            '        Dim myDir As New DirectoryInfo(Server.MapPath("~/Uploads/Customer/"))
            '        Dim filesInDir As FileInfo() = myDir.GetFiles((Convert.ToString(txtAccountID.Text + "_")) + "*.*")
            '        Dim files As List(Of ListItem) = New List(Of ListItem)

            '        For Each foundFile As FileInfo In filesInDir
            '            Dim fullName As String = foundFile.FullName
            '            files.Add(New ListItem(foundFile.Name))
            '        Next
            '        'Dim filePaths() As String = Directory.GetFiles(Server.MapPath("~/Uploads/") + txtAccountID.Text + "_*")
            '        'For Each filePath As String In filePaths
            '        '    files.Add(New ListItem(Path.GetFileName(filePath), filePath))
            '        'Next
            '        gvUpload.DataSource = files
            '        gvUpload.DataBind()
            '    End If
            '    conn.Close()

            'Catch ex As Exception
            '    MessageBox.Message.Alert(Page, "Error!!! " + ex.Message.ToString, "str")

            'End Try

            'btnDelete.Enabled = True
            'btnDelete.ForeColor = System.Drawing.Color.Black
            'btnQuit.Enabled = True
            'btnQuit.ForeColor = System.Drawing.Color.Black


            'btnCopyAdd.Enabled = True
            'btnCopyAdd.ForeColor = System.Drawing.Color.Black


            'txtAccountID.Enabled = False
            'btnTransactions.Enabled = True
            'btnTransactions.ForeColor = System.Drawing.Color.Black


            'btnChangeStatus.Enabled = True
            'btnChangeStatus.ForeColor = System.Drawing.Color.Black

            'btnSvcAdd.Enabled = True
            'btnSvcCancel.Enabled = False
            'btnSvcEdit.Enabled = False
            'btnSvcSave.Enabled = False
            'btnSvcDelete.Enabled = False

            'btnSvcAdd.ForeColor = System.Drawing.Color.Black
            'btnSvcCancel.ForeColor = System.Drawing.Color.Gray
            'btnSvcEdit.ForeColor = System.Drawing.Color.Gray
            'btnSvcSave.ForeColor = System.Drawing.Color.Gray
            'btnSvcDelete.ForeColor = System.Drawing.Color.Gray
            'btnContract.Enabled = True
            'btnContract.ForeColor = System.Drawing.Color.Black

            'btnSvcContract.Enabled = False
            'btnSvcContract.ForeColor = System.Drawing.Color.Gray
            'btnSvcService.Enabled = False
            'btnSvcService.ForeColor = System.Drawing.Color.Gray

            'AccessControl()
            'tb1.ActiveTabIndex = 0

            'If chkInactive.Checked = True Then
            '    btnCopyAdd.Enabled = False
            '    btnCopyAdd.ForeColor = System.Drawing.Color.Gray
            '    btnSvcAdd.Enabled = False
            '    btnSvcAdd.ForeColor = System.Drawing.Color.Gray
            'End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "GridView1_SelectedIndexChanged", ex.Message.ToString, txtAccountID.Text)
        End Try
    End Sub

    Private Sub RetrieveAutoEmailInfo()
        Try
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()
            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text
            Dim qry As String = "SELECT A.createdon FROM tblautoemaileventlog A INNER JOIN tblsales B on b.InvoiceNumber=A.ID "
            qry = qry + "and A.Event = 'EmailInvoice - 14' and AccountID='" & txtAccountID.Text & "' ORDER BY a.RCNO DESC limit 1;"
            command1.CommandText = qry
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New System.Data.DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then
                lblAutoEmailInv.Text = "Last Sent : " & dt.Rows(0)("CreatedOn").ToString
            Else
                lblAutoEmailInv.Text = "-"
            End If

            dt.Clear()
            dt.Dispose()
            dr.Close()
            command1.Dispose()

            Dim command2 As MySqlCommand = New MySqlCommand

            command2.CommandType = CommandType.Text
            Dim qry1 As String = "SELECT EmailSentDate FROM tblsoalog where Message='SOA - Sent' and AccountID='" & txtAccountID.Text.Trim & "' ORDER BY RCNO DESC limit 1;"
            command2.CommandText = qry1
            command2.Connection = conn

            Dim dr2 As MySqlDataReader = command2.ExecuteReader()
            Dim dt2 As New System.Data.DataTable
            dt2.Load(dr2)

            If dt2.Rows.Count > 0 Then
                lblAutoEmailSOA.Text = "Last Sent : " & dt2.Rows(0)("EmailSentDate").ToString
            Else
                lblAutoEmailSOA.Text = "-"
            End If

            dt2.Clear()
            dt2.Dispose()
            dr2.Close()
            command2.Dispose()

            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "RetrieveAutoEmailInfo", ex.Message.ToString, txtAccountID.Text)
        End Try

    End Sub
    Private Sub PopulateRecord()
        Try

            'System.Threading.Thread.Sleep(1000)
            'Label1.Visible = True
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()
            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.CommandText = "SELECT *,UPPER(Salesman),UPPER(LocateGrp),UPPER(Industry) FROM tblcompany where rcno=" & Convert.ToInt32(txtRcno.Text)
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New System.Data.DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then

                txtAccountID.Text = dt.Rows(0)("AccountID").ToString
                txtAccountIDtab2.Text = txtAccountID.Text
                lblAccountID.Text = txtAccountID.Text
                lblAccountID2.Text = txtAccountID.Text
                txtAccountIDSelected.Text = txtAccountID.Text

                If dt.Rows(0)("Status").ToString <> "" Then
                    ddlStatus.Text = dt.Rows(0)("Status").ToString
                End If
                If dt.Rows(0)("Inactive").ToString = "1" Then
                    chkInactive.Checked = True
                Else
                    chkInactive.Checked = False
                End If
                If dt.Rows(0)("CompanyGroup").ToString <> "" Then
                    ddlCompanyGrp.Text = dt.Rows(0)("CompanyGroup").ToString
                End If
                txtNameE.Text = dt.Rows(0)("Name").ToString
                lblName.Text = txtNameE.Text
                lblName2.Text = txtNameE.Text
                txtNameO.Text = dt.Rows(0)("Name2").ToString
                txtRegNo.Text = dt.Rows(0)("RocNos").ToString
                txtGSTRegNo.Text = dt.Rows(0)("GstNos").ToString
                txtWebsite.Text = dt.Rows(0)("Website").ToString

                If dt.Rows(0)("StartDate").ToString = DBNull.Value.ToString Then
                Else
                    txtStartDate.Text = Convert.ToDateTime(dt.Rows(0)("StartDate")).ToString("dd/MM/yyyy")
                End If

                If dt.Rows(0)("UPPER(Industry)").ToString <> "" Then

                    gSalesman = dt.Rows(0)("Industry").ToString.ToUpper()

                    If ddlIndustry.Items.FindByValue(gSalesman) Is Nothing Then
                        ddlIndustry.Items.Add(gSalesman)
                        ddlIndustry.Text = gSalesman
                    Else
                        ddlIndustry.Text = dt.Rows(0)("Industry").ToString.Trim().ToUpper()
                    End If
                End If

                'If dt.Rows(0)("UPPER(Industry)").ToString <> "" Then
                '    ddlIndustry.Text = dt.Rows(0)("UPPER(Industry)").ToString
                'End If

                'If String.IsNullOrEmpty(dt.Rows(0)("UPPER(Industry)").ToString) = False Then
                '    ddlIndustry.Text = dt.Rows(0)("UPPER(Industry)").ToString
                'End If
                'If dt.Rows(0)("UPPER(Salesman)").ToString <> "" Then
                '    ddlSalesMan.Text = dt.Rows(0)("UPPER(Salesman)").ToString
                'End If

                If dt.Rows(0)("UPPER(Salesman)").ToString <> "" Then

                    gSalesman = dt.Rows(0)("SalesMan").ToString.ToUpper()

                    If ddlSalesMan.Items.FindByValue(gSalesman) Is Nothing Then
                        ddlSalesMan.Items.Add(gSalesman)
                        ddlSalesMan.Text = gSalesman
                    Else
                        ddlSalesMan.Text = dt.Rows(0)("SalesMan").ToString.Trim().ToUpper()
                    End If
                End If


                'If dt.Rows(0)("Inchargeid").ToString <> "" Then
                '    ddlIncharge.Text = dt.Rows(0)("Inchargeid").ToString
                'End If


                If dt.Rows(0)("Inchargeid").ToString <> "" Then
                    'Dim gSalesman As String

                    gSalesman = dt.Rows(0)("Inchargeid").ToString.ToUpper()

                    If ddlIncharge.Items.FindByValue(gSalesman) Is Nothing Then
                        ddlIncharge.Items.Add(gSalesman)
                        ddlIncharge.Text = gSalesman
                    Else
                        ddlIncharge.Text = dt.Rows(0)("Inchargeid").ToString.Trim().ToUpper()
                    End If
                End If


                If dt.Rows(0)("ArTerm").ToString <> "" Then

                    gSalesman = dt.Rows(0)("ArTerm").ToString.ToUpper()

                    If ddlTerms.Items.FindByValue(gSalesman) Is Nothing Then
                        ddlTerms.Items.Add(gSalesman)
                        ddlTerms.Text = gSalesman
                    Else
                        ddlTerms.Text = dt.Rows(0)("ArTerm").ToString.Trim().ToUpper()
                    End If
                End If

                If dt.Rows(0)("ArCurrency").ToString <> "" Then
                    'Dim gSalesman As String

                    gSalesman = dt.Rows(0)("ArCurrency").ToString.ToUpper()

                    If ddlCurrency.Items.FindByValue(gSalesman) Is Nothing Then
                        ddlCurrency.Items.Add(gSalesman)
                        ddlCurrency.Text = gSalesman
                    Else
                        ddlCurrency.Text = dt.Rows(0)("ArCurrency").ToString.Trim().ToUpper()
                    End If
                End If
                txtComments.Text = dt.Rows(0)("Comments").ToString
                txtOffAddress1.Text = dt.Rows(0)("Address1").ToString
                txtOffStreet.Text = dt.Rows(0)("AddStreet").ToString


                txtOffBuilding.Text = dt.Rows(0)("AddBuilding").ToString
                If String.IsNullOrEmpty(dt.Rows(0)("AddCity").ToString) = False Then
                    ddlOffCity.Text = dt.Rows(0)("AddCity").ToString
                End If

                If String.IsNullOrEmpty(dt.Rows(0)("AddCountry").ToString) = False Then
                    If Server.HtmlDecode(dt.Rows(0)("AddCountry")).ToString = "S'pore" Or Server.HtmlDecode(dt.Rows(0)("AddCountry")).ToString = "S'PORE" Then
                        ddlOffCountry.Text = "SINGAPORE"
                    Else
                        ddlOffCountry.Text = dt.Rows(0)("AddCountry").ToString.Trim

                    End If
                End If
                If String.IsNullOrEmpty(dt.Rows(0)("AddState").ToString) = False Then
                    ddlOffState.Text = dt.Rows(0)("AddState").ToString
                End If

                txtOffPostal.Text = dt.Rows(0)("AddPostal").ToString

                txtOffContactPerson.Text = dt.Rows(0)("ContactPerson").ToString
                txtOffPosition.Text = dt.Rows(0)("OffContactPosition").ToString
                txtOffMobile.Text = dt.Rows(0)("Mobile").ToString

                txtOffContactNo.Text = dt.Rows(0)("Telephone").ToString
                txtOffFax.Text = dt.Rows(0)("Fax").ToString
                txtOffContact2.Text = dt.Rows(0)("Telephone2").ToString
                txtOffEmail.Text = dt.Rows(0)("Email").ToString

                txtOffCont1Name.Text = dt.Rows(0)("OffContact1").ToString
                txtOffCont1Position.Text = dt.Rows(0)("OffContact1Position").ToString
                txtOffCont1Tel.Text = dt.Rows(0)("OffContact1Tel").ToString
                txtOffCont1Fax.Text = dt.Rows(0)("OffContact1Fax").ToString
                txtOffCont1Tel2.Text = dt.Rows(0)("OffContact1Tel2").ToString
                txtOffCont1Mobile.Text = dt.Rows(0)("OffContact1Mobile").ToString
                txtOffCont1Email.Text = dt.Rows(0)("ContactPersonEmail").ToString

                If dt.Rows(0)("BillingAddress").ToString = "1" Then
                    chkOffAddr.Checked = True
                Else
                    chkOffAddr.Checked = False
                End If

                txtBillingName.Text = dt.Rows(0)("BillingName").ToString

                txtBillAddress.Text = dt.Rows(0)("BillAddress1").ToString
                txtBillStreet.Text = dt.Rows(0)("BillStreet").ToString
                txtBillBuilding.Text = dt.Rows(0)("BillBuilding").ToString
                If String.IsNullOrEmpty(dt.Rows(0)("BillCity").ToString) = False Then
                    ddlBillCity.Text = dt.Rows(0)("BillCity").ToString
                End If

                If String.IsNullOrEmpty(dt.Rows(0)("BillCountry").ToString) = False Then
                    If Server.HtmlDecode(dt.Rows(0)("BillCountry")).ToString = "S'pore" Or Server.HtmlDecode(dt.Rows(0)("AddCountry")).ToString = "S'PORE" Then
                        ddlBillCountry.Text = "SINGAPORE"
                    Else
                        ddlBillCountry.Text = dt.Rows(0)("BillCountry").ToString
                    End If
                End If

                If String.IsNullOrEmpty(dt.Rows(0)("BillState").ToString) = False Then
                    ddlBillState.Text = dt.Rows(0)("BillState").ToString
                End If
                txtBillPostal.Text = dt.Rows(0)("BillPostal").ToString

                txtBillCP1Contact.Text = dt.Rows(0)("BillContactPerson").ToString
                txtBillCP1Position.Text = dt.Rows(0)("BillContact1Position").ToString
                txtBillCP1Tel.Text = dt.Rows(0)("BillTelephone").ToString
                txtBillCP1Fax.Text = dt.Rows(0)("BillFax").ToString
                txtBillCP1Tel2.Text = dt.Rows(0)("BillTelephone2").ToString
                txtBillCP1Mobile.Text = dt.Rows(0)("BillMobile").ToString
                txtBillCP1Email.Text = dt.Rows(0)("BillContact1Email").ToString

                txtBillCP2Contact.Text = dt.Rows(0)("BillContact2").ToString
                txtBillCP2Position.Text = dt.Rows(0)("BillContact2Position").ToString
                txtBillCP2Tel.Text = dt.Rows(0)("BillContact2Tel").ToString
                txtBillCP2Fax.Text = dt.Rows(0)("BillContact2Fax").ToString
                txtBillCP2Tel2.Text = dt.Rows(0)("BillContact2Tel2").ToString
                txtBillCP2Mobile.Text = dt.Rows(0)("BillContact2Mobile").ToString
                txtBillCP2Email.Text = dt.Rows(0)("BillContact2Email").ToString


                If String.IsNullOrEmpty(dt.Rows(0)("BillingSettings").ToString) = False Then
                    If dt.Rows(0)("BillingSettings").ToString = "AccountID" Then
                        rdbBillingSettings.SelectedIndex = 0
                    ElseIf dt.Rows(0)("BillingSettings").ToString = "LocationID" Then
                        rdbBillingSettings.SelectedIndex = 1
                    ElseIf dt.Rows(0)("BillingSettings").ToString = "ContractNo" Then
                        rdbBillingSettings.SelectedIndex = 2
                    ElseIf dt.Rows(0)("BillingSettings").ToString = "ServiceLocationCode" Then
                        rdbBillingSettings.SelectedIndex = 3
                    End If

                End If

                txtBillingOptionRemarks.Text = dt.Rows(0)("BillingOptionRemarks").ToString

                'If String.IsNullOrEmpty(dt.Rows(0)("BillingSettings").ToString) = False Then
                '    rdbBillingSettings.SelectedItem.Text = dt.Rows(0)("BillingSettings").ToString
                'End If

                'Tab panel - service location

                txtClientID.Text = dt.Rows(0)("ID").ToString
                txtAccountCode.Text = dt.Rows(0)("AccountNo").ToString

                chkSendStatementSOA.Checked = dt.Rows(0)("SendStatement").ToString
                If dt.Rows(0)("HardCopyInvoice").ToString = "" Then
                    chkSendStatementInv.Checked = False
                Else
                    chkSendStatementInv.Checked = dt.Rows(0)("HardCopyInvoice").ToString

                End If

                chkAutoEmailInvoice.Checked = dt.Rows(0)("AutoEmailInvoice").ToString
                chkAutoEmailStatement.Checked = dt.Rows(0)("AutoEmailSOA").ToString

                If dt.Rows(0)("RequireEBilling").ToString = "" Then
                    chkRequireEBilling.Checked = False
                Else
                    chkRequireEBilling.Checked = dt.Rows(0)("RequireEBilling").ToString

                End If
                If dt.Rows(0)("EmailNotificationOfSchedule").ToString = "" Then
                    chkEmailNotifySchedule.Checked = False
                Else
                    chkEmailNotifySchedule.Checked = dt.Rows(0)("EmailNotificationOfSchedule").ToString

                End If
                If dt.Rows(0)("EmailNotificationOfJobProgress").ToString = "" Then
                    chkEmailNotifyJobProgress.Checked = False
                Else
                    chkEmailNotifyJobProgress.Checked = dt.Rows(0)("EmailNotificationOfJobProgress").ToString

                End If
                If dt.Rows(0)("MandatoryServiceReportPhotos").ToString = "" Then
                    chkPhotosMandatory.Checked = False
                Else
                    chkPhotosMandatory.Checked = dt.Rows(0)("MandatoryServiceReportPhotos").ToString

                End If
                If dt.Rows(0)("DisplayTimeInTimeOutInServiceReport").ToString = "" Then
                    chkDisplayTimeInTimeOut.Checked = False
                Else
                    chkDisplayTimeInTimeOut.Checked = dt.Rows(0)("DisplayTimeInTimeOutInServiceReport").ToString

                End If
                'chkSmartCustomer.Checked = dt.Rows(0)("SmartCustomer").ToString

                'If chkSendStatement.Checked = False Then
                '    btnOSInvoiceStatement.Disabled = True
                'Else
                '    btnOSInvoiceStatement.Disabled = False
                'End If

                If String.IsNullOrEmpty(dt.Rows(0)("Location").ToString) = True Then
                    ddlLocation.SelectedIndex = 0
                Else
                    ddlLocation.Text = dt.Rows(0)("Location").ToString
                End If


                txtAccountIDEdit.Text = txtAccountID.Text
                txtNameEdit.Text = txtNameE.Text
                chkSendStatementSOAEdit.Checked = chkSendStatementSOA.Checked
                chkSendStatementInvEdit.Checked = chkSendStatementInv.Checked
                If String.IsNullOrEmpty(dt.Rows(0)("DefaultInvoiceFormat").ToString) = True Then
                    ddlDefaultInvoiceFormat.SelectedIndex = 0
                Else
                    ddlDefaultInvoiceFormat.Text = dt.Rows(0)("DefaultInvoiceFormat").ToString
                End If
                txtCreditLimit.Text = dt.Rows(0)("ARLimit").ToString


                ddlCurrencyEdit.Text = ddlCurrency.Text
                ddlTermsEdit.Text = ddlTerms.Text
                chkAutoEmailInvoiceEdit.Checked = chkAutoEmailInvoice.Checked
                ddlDefaultInvoiceFormatEdit.Text = ddlDefaultInvoiceFormat.Text
                chkSendStatementSOAEdit.Checked = chkSendStatementSOA.Checked
                chkSendStatementInvEdit.Checked = chkSendStatementInv.Checked
                chkRequireEBillingEdit.Checked = chkRequireEBilling.Checked

                chkAutoEmailStatementEdit.Checked = chkAutoEmailStatement.Checked
                txtBillingOptionRemarksEdit.Text = txtBillingOptionRemarks.Text

                txtTIN.Text = dt.Rows(0)("TaxIdentificationNo").ToString
                txtTINOld.Text = dt.Rows(0)("TaxIdentificationNo").ToString

                txtSST.Text = dt.Rows(0)("SalesTaxRegistrationNo").ToString

                'ddlLocation.Text = dt.Rows(0)("Location").ToString

                'If dt.Rows(0)("Inactive").ToString = "1" Then
                '    chkInactive.Checked = True
                'Else
                '    chkInactive.Checked = False
                'End If

                If txtDisplayRecordsLocationwise.Text = "N" Then
                    If String.IsNullOrEmpty(txtAccountID.Text) Then
                        SqlDataSource2.SelectCommand = "SELECT * FROM tblcompanylocation where accountid is null and companyid = '" & txtClientID.Text & "'"
                    Else
                        SqlDataSource2.SelectCommand = "SELECT * FROM tblcompanylocation where accountid = '" & txtAccountID.Text & "' order by LocationNo"
                    End If
                    btnSearchTIN.Visible = False
                   
                End If


                If txtDisplayRecordsLocationwise.Text = "Y" Then
                    If String.IsNullOrEmpty(txtAccountID.Text) Then
                        SqlDataSource2.SelectCommand = "SELECT * FROM tblcompanylocation where accountid is null and companyid = '" & txtClientID.Text & "'"
                    Else
                        SqlDataSource2.SelectCommand = "SELECT * FROM tblcompanylocation where accountid = '" & txtAccountID.Text & "' and location in (Select LocationID  from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') order by LocationNo"
                    End If

                    btnSearchTIN.Visible = True
                End If

                SqlDataSource2.DataBind()
                GridView2.DataBind()

                'lblServiceLocationCount.Text = "Service Location [" & GridView2.Rows.Count & "]"

                txtDetail.Text = SqlDataSource2.SelectCommand

                SqlDSNotesMaster.SelectCommand = "select * from tblnotes where keyfield = '" + txtAccountID.Text + "'"
                SqlDSNotesMaster.DataBind()
                gvNotesMaster.DataBind()

                '      'View Uploaded files

                '      Dim myDir As New DirectoryInfo(Server.MapPath("~/Uploads/Customer/"))
                '      Dim filesInDir As FileInfo() = myDir.GetFiles((Convert.ToString(txtAccountID.Text + "_")) + "*.*")
                '      Dim files As List(Of ListItem) = New List(Of ListItem)

                '      For Each foundFile As FileInfo In filesInDir
                'Dim fullName As String = foundFile.FullName
                '      files.Add(New ListItem(foundFile.Name))
                '      Next
                '      'Dim filePaths() As String = Directory.GetFiles(Server.MapPath("~/Uploads/") + txtAccountID.Text + "_*")
                '      'For Each filePath As String In filePaths
                '      '    files.Add(New ListItem(Path.GetFileName(filePath), filePath))
                '      'Next
                '      gvUpload.DataSource = files
                '      gvUpload.DataBind()

                SqlDSUpload.SelectCommand = "select * from tblfileupload where fileref = '" + txtAccountID.Text + "'"
                gvUpload.DataSourceID = "SqlDSUpload"
                gvUpload.DataBind()

                lblFileUploadCount.Text = "File Upload [" & gvUpload.Rows.Count & "]"

                '''''''''''' Ageing
                lblCurrentVal.Text = ""
                lbl1To30Val.Text = ""
                lbl31To60Val.Text = ""
                lbl61To90Val.Text = ""
                lbl91To180Val.Text = ""
                lblMoreThan180Val.Text = ""
                lblTotalVal.Text = ""

                'Dim IsLock As String
                'IsLock = ""

                Dim connAgeing As MySqlConnection = New MySqlConnection()

                connAgeing.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                connAgeing.Open()

                Dim commandAgeing As MySqlCommand = New MySqlCommand

                commandAgeing.CommandType = CommandType.Text
                commandAgeing.CommandText = "SELECT sum(Current) as Current, sum(1To10) as 1To10, sum(11To30) as 11To30, sum(31To60) as 31To60, sum(61To90) as 61To90, sum(91To150) as 91To150,sum(151To180) as 151To180, sum(GreaterThan180) as GreaterThan180 FROM tbwcompanybal where AccountId='" & txtAccountID.Text & "'"
                commandAgeing.Connection = connAgeing

                Dim drAgeing As MySqlDataReader = commandAgeing.ExecuteReader()
                Dim dtAgeing As New DataTable
                dtAgeing.Load(drAgeing)


                If dtAgeing.Rows.Count > 0 Then

                    If Convert.IsDBNull(dtAgeing.Rows(0)("Current")) = True Then
                        lblCurrentVal.Text = "0.00"
                    Else
                        lblCurrentVal.Text = dtAgeing.Rows(0)("Current").ToString
                    End If



                    If Convert.IsDBNull(dtAgeing.Rows(0)("1To10")) = True And Convert.IsDBNull(dtAgeing.Rows(0)("11To30")) = True Then
                        lbl1To30Val.Text = "0.00"
                    Else
                        lbl1To30Val.Text = Convert.ToDecimal((dtAgeing.Rows(0)("1To10").ToString)) + Convert.ToDecimal(dtAgeing.Rows(0)("11To30").ToString)
                    End If



                    'lbl1To30Val.Text = Convert.ToDecimal((dtAgeing.Rows(0)("1To10").ToString)) + Convert.ToDecimal(dtAgeing.Rows(0)("11To30").ToString)
                    If Convert.IsDBNull(dtAgeing.Rows(0)("31To60")) = True Then
                        lbl31To60Val.Text = "0.00"
                    Else
                        lbl31To60Val.Text = dtAgeing.Rows(0)("31To60").ToString
                    End If

                    If Convert.IsDBNull(dtAgeing.Rows(0)("61To90")) = True Then
                        lbl61To90Val.Text = "0.00"
                    Else
                        lbl61To90Val.Text = dtAgeing.Rows(0)("61To90").ToString
                    End If




                    If Convert.IsDBNull(dtAgeing.Rows(0)("91To150")) = True And Convert.IsDBNull(dtAgeing.Rows(0)("151To180")) = True Then
                        lbl91To180Val.Text = "0.00"
                    Else
                        lbl91To180Val.Text = Convert.ToDecimal(dtAgeing.Rows(0)("91To150").ToString) + Convert.ToDecimal(dtAgeing.Rows(0)("151To180").ToString)

                    End If

                    If Convert.IsDBNull(dtAgeing.Rows(0)("GreaterThan180")) = True Then
                        lblMoreThan180Val.Text = "0.00"
                    Else
                        lblMoreThan180Val.Text = dtAgeing.Rows(0)("GreaterThan180").ToString
                    End If

                    lblTotalVal.Text = Convert.ToDecimal(lblCurrentVal.Text) + Convert.ToDecimal(lbl1To30Val.Text) + Convert.ToDecimal(lbl31To60Val.Text) + Convert.ToDecimal(lbl61To90Val.Text) + Convert.ToDecimal(lbl91To180Val.Text) + Convert.ToDecimal(lblMoreThan180Val.Text)

                End If

                connAgeing.Close()
                connAgeing.Dispose()
                commandAgeing.Dispose()
                dtAgeing.Dispose()


                '''''''''''' Ageing
            End If
            conn.Close()
            conn.Dispose()

            dt.Dispose()
            dr.Close()
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "GRIDVIEW ROW SELECTION - POPULATERECORD", ex.Message.ToString, txtAccountID.Text)

        End Try

        btnDelete.Enabled = True
        btnDelete.ForeColor = System.Drawing.Color.Black
        btnQuit.Enabled = True
        btnQuit.ForeColor = System.Drawing.Color.Black


        btnCopyAdd.Enabled = True
        btnCopyAdd.ForeColor = System.Drawing.Color.Black


        txtAccountID.Enabled = False
        btnTransactions.Enabled = True
        btnTransactions.ForeColor = System.Drawing.Color.Black


        btnChangeStatus.Enabled = True
        btnChangeStatus.ForeColor = System.Drawing.Color.Black

        btnSvcAdd.Enabled = True
        btnSvcCancel.Enabled = False
        btnSvcEdit.Enabled = False
        btnSvcCopy.Enabled = False
        btnSvcSave.Enabled = False
        btnSvcDelete.Enabled = False

        btnSvcAdd.ForeColor = System.Drawing.Color.Black
        btnSvcCancel.ForeColor = System.Drawing.Color.Gray
        btnSvcEdit.ForeColor = System.Drawing.Color.Gray
        btnSvcCopy.ForeColor = System.Drawing.Color.Gray
        btnSvcSave.ForeColor = System.Drawing.Color.Gray
        btnSvcDelete.ForeColor = System.Drawing.Color.Gray
        btnContract.Enabled = True
        btnContract.ForeColor = System.Drawing.Color.Black

        btnSvcContract.Enabled = False
        btnSvcContract.ForeColor = System.Drawing.Color.Gray
        btnSvcService.Enabled = False
        btnSvcService.ForeColor = System.Drawing.Color.Gray

        btnTransactionsSvc.Enabled = False
        btnTransactionsSvc.ForeColor = System.Drawing.Color.Gray

        btnSpecificLocation.Enabled = False
        btnSpecificLocation.ForeColor = System.Drawing.Color.Gray

        btnTransfersSvc.Enabled = False
        btnTransfersSvc.ForeColor = System.Drawing.Color.Gray

        btnUpdateServiceContact.Enabled = True
        btnUpdateServiceContact.ForeColor = System.Drawing.Color.Black

        AccessControl()
        tb1.ActiveTabIndex = 0

        GridView1.SelectedIndex = 0

        If chkInactive.Checked = True Then
            btnCopyAdd.Enabled = False
            btnCopyAdd.ForeColor = System.Drawing.Color.Gray
            btnSvcAdd.Enabled = False
            btnSvcAdd.ForeColor = System.Drawing.Color.Gray

            btnUpdateServiceContact.Enabled = False
            btnUpdateServiceContact.ForeColor = System.Drawing.Color.Gray

            btnUpdateBilling.Enabled = False
            btnUpdateBilling.ForeColor = System.Drawing.Color.Gray

            'btnChangeStatus.Enabled = False
            'btnChangeStatus.ForeColor = System.Drawing.Color.Gray
            btnChangeStatusLoc.Enabled = False
            btnChangeStatusLoc.ForeColor = System.Drawing.Color.Gray

            btnSpecificLocation.Enabled = False
            btnSpecificLocation.ForeColor = System.Drawing.Color.Gray
            btnTransfersSvc.Enabled = False
            btnTransfersSvc.ForeColor = System.Drawing.Color.Gray


            btnEditContractGroup.Visible = False
            btnEditSalesman.Visible = False
            btnEditSendStatement.Visible = False
        End If
    End Sub
    Protected Sub btnADD_Click(sender As Object, e As EventArgs) Handles btnADD.Click
        DisableControls()
        MakeMeNull()
        txtMode.Text = "NEW"
        lblMessage.Text = "ACTION: ADD RECORD"
        ddlCompanyGrp.Focus()
        ddlStatus.Enabled = False
        'txtStartDate.Text = DateAndTime.Now.ToShortDateString
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "currentdatetimestartdate();", True)
        txtNameE.Focus()
        btnADD.Enabled = False
        txtCreatedOn.Text = ""
        tb1.ActiveTabIndex = 0

        'btnSave.Enabled = True
        'btnSave.ForeColor = System.Drawing.Color.Black
        'btnCancel.Enabled = True
        'btnCancel.ForeColor = System.Drawing.Color.Black

        txtSearchCust.Enabled = False
        btnGoCust.Enabled = False
        btnResetSearch.Enabled = False
        btnSearchTIN.Enabled = True
        btnSearchTIN.ForeColor = System.Drawing.Color.Black

        'chkAutoEmailInvoice.Checked = True
        'chkAutoEmailStatement.Checked = True
        'chkSendStatementInv.Checked = True
        'chkSendStatementSOA.Checked = True

        '''''''''''''''''Contact Module setup
        Try


            Dim conn As MySqlConnection = New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim CommandContactsSetup As MySqlCommand = New MySqlCommand
            CommandContactsSetup.CommandType = CommandType.Text
            CommandContactsSetup.CommandText = "SELECT * FROM tblContactSetup"
            CommandContactsSetup.Connection = conn

            Dim drContactsSetup As MySqlDataReader = CommandContactsSetup.ExecuteReader()
            Dim dtContactsSetup As New System.Data.DataTable
            dtContactsSetup.Load(drContactsSetup)

            If dtContactsSetup.Rows.Count > 0 Then

                ddlTerms.Text = dtContactsSetup.Rows(0)("CompARTerms").ToString()
                ddlTermsSvc.Text = dtContactsSetup.Rows(0)("CompARTerms").ToString()

                If dtContactsSetup.Rows(0)("CompROCNosBlank").ToString() = True Then
                    Label24.Visible = True
                End If

                If dtContactsSetup.Rows(0)("CompCurrency").ToString = "" Then
                    ddlCurrency.SelectedIndex = 0
                Else
                    ddlCurrency.Text = dtContactsSetup.Rows(0)("CompCurrency").ToString
                End If

                If dtContactsSetup.Rows(0)("CompDefaultAutoEmailInvoice").ToString() = True Then
                    chkAutoEmailInvoice.Checked = True
                End If
                If dtContactsSetup.Rows(0)("CompDefaultAutoEmailSOA").ToString() = True Then
                    chkAutoEmailStatement.Checked = True
                End If
                If dtContactsSetup.Rows(0)("CompHardCopyInvoice").ToString() = True Then
                    chkSendStatementInv.Checked = True
                End If
                If dtContactsSetup.Rows(0)("CompHardCopySOA").ToString() = True Then
                    chkSendStatementSOA.Checked = True
                End If
            End If

            ''''''''''''''''''''''''''''''''''''''''
            conn.Close()
            conn.Dispose()
            If txtDisplayTimeInTimeOutServiceRecord.Text = "1" Then
                chkDisplayTimeInTimeOut.Checked = True
            End If



        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "BTNADD_CLICK", ex.Message.ToString, "")

        End Try
    End Sub

    Private Sub AccessControl()
        '''''''''''''''''''Access Control 
        'If Session("SecGroupAuthority") <> "ADMINISTRATOR" Then
        Try

            Dim command As MySqlCommand = New MySqlCommand

            Dim conn As MySqlConnection = New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            command.CommandType = CommandType.Text
            command.CommandText = "SELECT x2412, x2413, x0302, x0302Add, x0302Edit, x0302EditAcct, x0302Delete, x0302Trans, x0302Notes, x0302Change,x0302EditBilling, x0302CompanySpecificLocation, x0302EditContractGroup, x0302ChangeAccount, x0302CompanyUpdateServiceContact FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"
            command.Connection = conn

            Dim dr As MySqlDataReader = command.ExecuteReader()
            Dim dt As New System.Data.DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then

                If Convert.ToBoolean(dt.Rows(0)("x0302")) = False Then
                    If dt.Rows(0)("x0302").ToString() = False Then
                        Response.Redirect("Home.aspx")
                    End If
                End If


                If Convert.ToBoolean(dt.Rows(0)("x2412")) = False Then
                    btnContract.Enabled = dt.Rows(0)("x2412").ToString()
                    btnSvcContract.Enabled = dt.Rows(0)("x2412").ToString()
                End If


                'If (dt.Rows(0)("x2412")) = False Then
                '    If Convert.ToBoolean(dt.Rows(0)("x2412")) = False Then
                '        btnSvcContract.Enabled = dt.Rows(0)("x2412").ToString()
                '    End If
                'End If



                If Convert.ToBoolean(dt.Rows(0)("x2413")) = False Then
                    btnSvcService.Enabled = dt.Rows(0)("x2413").ToString()
                End If



                If (dt.Rows(0)("x0302Add")) = False Then
                    btnADD.Enabled = dt.Rows(0)("x0302Add").ToString()
                End If

                If (dt.Rows(0)("x0302Edit")) = False Then
                    btnCopyAdd.Enabled = dt.Rows(0)("x0302Edit").ToString()
                End If

                If (dt.Rows(0)("x0302Delete")) = False Then
                    btnDelete.Enabled = dt.Rows(0)("x0302Delete").ToString()
                End If

                'If String.IsNullOrEmpty(dt.Rows(0)("x2412")) = False Then
                '    btnContract.Enabled = dt.Rows(0)("x2412").ToString()
                'End If
                If (dt.Rows(0)("x0302EditBilling")) = False Then
                    btnUpdateBilling.Enabled = dt.Rows(0)("x0302EditBilling").ToString()
                End If

                If String.IsNullOrEmpty(dt.Rows(0)("x0302ChangeAccount")) = False Then
                    btnTransfersSvc.Enabled = dt.Rows(0)("x0302ChangeAccount").ToString()
                End If

                If String.IsNullOrEmpty(dt.Rows(0)("x0302CompanySpecificLocation")) = False Then
                    btnSpecificLocation.Enabled = dt.Rows(0)("x0302CompanySpecificLocation").ToString()
                End If

                If String.IsNullOrEmpty(dt.Rows(0)("x0302CompanyUpdateServiceContact")) = False Then
                    btnUpdateServiceContact.Enabled = dt.Rows(0)("x0302CompanyUpdateServiceContact").ToString()
                End If

                If chkInactive.Checked = False Then
                    If Convert.ToBoolean(dt.Rows(0)("x0302EditContractGroup")) = False Then
                        btnEditContractGroup.Visible = dt.Rows(0)("x0302EditContractGroup").ToString()
                    Else
                        btnEditContractGroup.Visible = dt.Rows(0)("x0302EditContractGroup").ToString()
                    End If
                End If


                'If Convert.ToBoolean(dt.Rows(0)("x0302EditContractGroup")) = False Then
                '    If dt.Rows(0)("x0302EditContractGroup").ToString() = False Then
                '        btnEditContractGroup.Visible = dt.Rows(0)("x0302EditContractGroup").ToString()
                '    End If
                'End If


                If btnADD.Enabled = True Then
                    btnADD.ForeColor = System.Drawing.Color.Black
                Else
                    btnADD.ForeColor = System.Drawing.Color.Gray
                End If


                If btnCopyAdd.Enabled = True Then
                    btnCopyAdd.ForeColor = System.Drawing.Color.Black
                Else
                    btnCopyAdd.ForeColor = System.Drawing.Color.Gray
                End If

                If btnDelete.Enabled = True Then
                    btnDelete.ForeColor = System.Drawing.Color.Black
                Else
                    btnDelete.ForeColor = System.Drawing.Color.Gray
                End If


                If btnContract.Enabled = True Then
                    btnContract.ForeColor = System.Drawing.Color.Black
                Else
                    btnContract.ForeColor = System.Drawing.Color.Gray
                End If

                If btnSvcContract.Enabled = True Then
                    btnSvcContract.ForeColor = System.Drawing.Color.Black
                Else
                    btnSvcContract.ForeColor = System.Drawing.Color.Gray
                End If

                If btnSvcService.Enabled = True Then
                    btnSvcService.ForeColor = System.Drawing.Color.Black
                Else
                    btnSvcService.ForeColor = System.Drawing.Color.Gray
                End If

                If btnUpdateBilling.Enabled = True Then
                    btnUpdateBilling.ForeColor = System.Drawing.Color.Black
                Else
                    btnUpdateBilling.ForeColor = System.Drawing.Color.Gray
                End If

                If chkInactive.Checked = False Then
                    If btnTransfersSvc.Enabled = True Then
                        btnTransfersSvc.ForeColor = System.Drawing.Color.Black
                    Else
                        btnTransfersSvc.ForeColor = System.Drawing.Color.Gray
                    End If
                End If

                If chkInactive.Checked = False Then
                    If btnUpdateServiceContact.Enabled = True Then
                        btnUpdateServiceContact.ForeColor = System.Drawing.Color.Black
                    Else
                        btnUpdateServiceContact.ForeColor = System.Drawing.Color.Gray
                    End If
                End If

                ''If btnPrint.Enabled = True Then
                ''    btnPrint.ForeColor = System.Drawing.Color.Black
                ''Else
                ''    btnPrint.ForeColor = System.Drawing.Color.Gray
                ''End If
                ''btnApprModeOnOff.Visible = vpSec2413ApprMode
            End If
            conn.Close()
            conn.Dispose()
            command.Dispose()
            dt.Dispose()
            dr.Close()
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "FUNCTION ACCESSCONTROL", ex.Message.ToString, Session("SecGroupAuthority"))
        End Try
        'End If

        '''''''''''''''''''Access Control 

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not IsPostBack Then
            MakeMeNull()
            EnableControls()

            MakeSvcNull()
            EnableSvcControls()

            MakeNotesNull()
            EnableNotesControls()

            MakeCPNull()
            EnableCPControls()

            AccessControl()

            'ddlLocation.Attributes.Add("disabled", "true")

            Dim UserID As String = Convert.ToString(Session("UserID"))
            txtCreatedBy.Text = UserID
            txtLastModifiedBy.Text = UserID
            txtCreatedOn.Attributes.Add("readonly", "readonly")
            txtGroupAuthority.Text = Session("SecGroupAuthority")
            'FindLocation()
            Dim Query As String
            Query = "SELECT LocationID, Location FROM tblGroupAccessLocation where GroupAccess = '" & txtGroupAuthority.Text.ToUpper & "' order by LocationID"
            PopulateDropDownList(Query, "LocationID", "LocationID", ddlLocation)
            PopulateDropDownList(Query, "LocationID", "LocationID", ddlBranch)
            PopulateDropDownList(Query, "LocationID", "LocationID", ddlBranchSearch)

            Query = "SELECT companygroup FROM tblcompanygroup order by companygroup"
            PopulateDropDownList(Query, "companygroup", "companygroup", ddlCompanyGrp)
            PopulateDropDownList(Query, "companygroup", "companygroup", ddlCompanyGrpD)

            Query = "SELECT locationgroup FROM tbllocationgroup order by locationgroup"
            PopulateDropDownList(Query, "locationgroup", "locationgroup", ddlLocateGrp)


            Query = "SELECT StaffId FROM tblstaff where roles= 'SALES MAN' and status ='O' ORDER BY STAFFID"
            PopulateDropDownList(Query, "StaffId", "StaffId", ddlSalesMan)
            PopulateDropDownList(Query, "StaffId", "StaffId", ddlSalesManSvc)
            PopulateDropDownList(Query, "StaffId", "StaffId", ddlSearchSalesman)
            PopulateDropDownList(Query, "StaffId", "StaffId", ddlNewSalesman)


            'Query = "SELECT StaffId FROM tblstaff where roles= 'SALES MAN' and status ='O' ORDER BY STAFFID"
            ''Query = "SELECT StaffId FROM tblstaff where roles= 'SALES MAN'  ORDER BY STAFFID"
            ''PopulateDropDownList(Query, "StaffId", "StaffId", ddlSalesMan)
            'PopulateDropDownList(Query, "StaffId", "StaffId", ddlSalesManSvc)
            ''PopulateDropDownList(Query, "StaffId", "StaffId", ddlSearchSalesman)
            'PopulateDropDownList(Query, "StaffId", "StaffId", ddlNewSalesman)

            'PopulateStaff()

            Query = "SELECT industry FROM tblindustry ORDER BY industry"
            PopulateDropDownList(Query, "industry", "industry", ddlIndustry)
            PopulateDropDownList(Query, "industry", "industry", ddlIndustrysvc)
            PopulateDropDownList(Query, "industry", "industry", ddlSearchIndustry)

            'Query = "SELECT distinct (inchargeId) FROM tblteam where Status <> 'N' ORDER BY inchargeID"
            'PopulateDropDownList(Query, "inchargeId", "inchargeId", ddlIncharge)
            'PopulateDropDownList(Query, "inchargeId", "inchargeId", ddlInchargeSvc)

            Query = "SELECT distinct (StaffID) FROM tblStaff where Roles='TECHNICAL' and Status = 'O' ORDER BY StaffID"
            PopulateDropDownList(Query, "StaffID", "StaffID", ddlIncharge)
            PopulateDropDownList(Query, "StaffID", "StaffID", ddlInchargeSvc)

            Query = "SELECT City FROM tblcity ORDER BY City"
            PopulateDropDownList(Query, "City", "City", ddlCity)
            PopulateDropDownList(Query, "City", "City", ddlOffCity)
            PopulateDropDownList(Query, "City", "City", ddlBillCity)
            PopulateDropDownList(Query, "City", "City", ddlBillCitySvc)
            PopulateDropDownList(Query, "City", "City", ddlEditBillCity)

            Query = "SELECT Country FROM tblcountry  ORDER BY Country"
            PopulateDropDownList(Query, "Country", "Country", ddlCountry)
            PopulateDropDownList(Query, "Country", "Country", ddlOffCountry)
            PopulateDropDownList(Query, "Country", "Country", ddlBillCountry)
            PopulateDropDownList(Query, "Country", "Country", ddlBillCountrySvc)
            PopulateDropDownList(Query, "Country", "Country", ddlEditBillCountry)

            Query = "SELECT Terms,TermsDay FROM tblterms ORDER BY termsday,terms"
            PopulateDropDownList(Query, "Terms", "Terms", ddlTerms)
            PopulateDropDownList(Query, "Terms", "Terms", ddlTermsSvc)
            PopulateDropDownList(Query, "Terms", "Terms", ddlTermsEdit)

            IsDisplayRecordsLocationwise()
            ContactModuleSetup()


            btnUpdateServiceContact.Enabled = False
            btnUpdateServiceContact.ForeColor = System.Drawing.Color.Gray
            'Exit Sub
            ''   ---------------------------------------------
            ''Dim splashscreen = New 
            'Dim splashScreen = New SplashForm()
            'splashScreen.Show(splashScreen.lblStatus.Text = "Loading Clients...")
            '' On the splash screen now go and show loading messages
            'splashScreen.lblStatus.Refresh()

            '' Do the specific loading here for the status set above
            ''Dim clientList = _repository.LoadClients()

            '' Continue doing this above until you're done

            '' Close the splash screen
            'splashScreen.Close()

            '' ---------------------------------------------

            txtDDLText.Text = "-1"
            btnTop.Attributes.Add("onclick", "javascript:scroll(0,0);return false;")
            btnTopDetail.Attributes.Add("onclick", "javascript:scroll(0,0);return false;")

            Try



                'Query = "Select Terms, TermsDay from tblTerms order by Termsday,Terms"
                'PopulateDropDownList(Query, "Terms", "Terms", ddlTerms)
                'PopulateDropDownList(Query, "Terms", "Terms", ddlTermsSvc)


                'Query = "Select ContractGroup from tblcontractgroup"
                'Query = "Select concat(COACode, ' - ', Description) as CCode from tblcontractgroup order by ContractGroup"
                Query = "SELECT CONCAT(ContractGroup, ' : ', GroupDescription) AS ContractGroup FROM tblcontractgroup order by ContractGroup"

                PopulateDropDownList(Query, "ContractGroup", "ContractGroup", ddlContractGrp)

                PopulateDropDownList(Query, "ContractGroup", "ContractGroup", ddlContractGroupEdit)


                If Session("servicefrom") = "contactC" Or Session("relocatefrom") = "relocateC" Then
                    txt.Text = Session("gridsql")
                    SqlDataSource1.SelectCommand = txt.Text
                    SqlDataSource1.DataBind()
                    GridView1.DataSourceID = "SqlDataSource1"

                    If String.IsNullOrEmpty(Session("rcno")) = False Then
                        txtRcno.Text = Session("rcno")
                        GridView1_SelectedIndexChanged(New Object(), New EventArgs)
                    End If

                    txtDetail.Text = Session("gridsqlCompanyDetail")
                    SqlDataSource2.SelectCommand = txtDetail.Text
                    SqlDataSource2.DataBind()
                    GridView2.DataSourceID = "SqlDataSource2"

                    If String.IsNullOrEmpty(Session("rcnoDetail")) = False Then
                        'txtRcno.Text = Session("rcno")
                        GridView2_SelectedIndexChanged(New Object(), New EventArgs)
                    End If


                    Session.Remove("servicefrom")
                    Session.Remove("rcno")
                    Session.Remove("accountid")
                    Session.Remove("locationid")

                    Session.Remove("gridsqlCompanyDetail")
                    Session.Remove("rcnoDetail")
                    Session.Remove("relocatefrom")

                ElseIf Session("contractfrom") = "clients" Then

                    ddlCompanyGrp.SelectedIndex = -1
                    ddlIndustry.SelectedIndex = -1
                    ddlSalesMan.SelectedIndex = -1
                    ddlIncharge.SelectedIndex = -1
                    ddlTerms.SelectedIndex = -1
                    ddlCurrency.SelectedIndex = -1

                    ddlOffCity.SelectedIndex = -1
                    ddlOffCountry.SelectedIndex = -1
                    ddlOffState.SelectedIndex = -1
                    ddlBillCity.SelectedIndex = -1
                    ddlBillCountry.SelectedIndex = -1
                    ddlBillState.SelectedIndex = -1

                    ddlBillCity.SelectedIndex = -1
                    ddlBillCountry.SelectedIndex = -1
                    ddlBillState.SelectedIndex = -1

                    ddlCurrencyEdit.SelectedIndex = -1
                    ddlDefaultInvoiceFormatEdit.SelectedIndex = -1
                    ddlTermsEdit.SelectedIndex = -1

                    txt.Text = Session("gridsqlCompany")

                    If String.IsNullOrEmpty(Session("rcno")) = False Then
                        'txtRcno.Text = Session("rcno")
                        GridView1_SelectedIndexChanged(New Object(), New EventArgs)
                    End If

                    txtDetail.Text = Session("gridsqlCompanyDetail")
                    SqlDataSource2.SelectCommand = txtDetail.Text
                    SqlDataSource2.DataBind()
                    GridView2.DataSourceID = "SqlDataSource2"

                    If String.IsNullOrEmpty(Session("rcnoDetail")) = False Then
                        GridView2_SelectedIndexChanged(New Object(), New EventArgs)
                    End If

                    Session.Remove("contractfrom")
                    Session.Remove("locationid")
                    Session.Remove("rcno")
                    Session.Remove("accountid")


                    Session.Remove("gridsqlCompanyDetail")
                    Session.Remove("rcnoDetail")

                    ''''''''''''''''''''
                ElseIf Session("customerfrom") = "Corporate" And Session("armodule") = "armodule" Then

                    ddlCompanyGrp.SelectedIndex = -1
                    ddlIndustry.SelectedIndex = -1
                    ddlSalesMan.SelectedIndex = -1
                    ddlIncharge.SelectedIndex = -1
                    ddlTerms.SelectedIndex = -1
                    ddlCurrency.SelectedIndex = -1

                    ddlOffCity.SelectedIndex = -1
                    ddlOffCountry.SelectedIndex = -1
                    ddlOffState.SelectedIndex = -1
                    ddlBillCity.SelectedIndex = -1
                    ddlBillCountry.SelectedIndex = -1
                    ddlBillState.SelectedIndex = -1


                    ddlBillCity.SelectedIndex = -1
                    ddlBillCountry.SelectedIndex = -1
                    ddlBillState.SelectedIndex = -1

                    ddlCurrencyEdit.SelectedIndex = -1
                    ddlDefaultInvoiceFormatEdit.SelectedIndex = -1
                    ddlTermsEdit.SelectedIndex = -1


                    txt.Text = Session("gridsqlCompany")
                    SqlDataSource1.SelectCommand = txt.Text
                    SqlDataSource1.DataBind()
                    GridView1.DataSourceID = "SqlDataSource1"


                    If String.IsNullOrEmpty(Session("rcno")) = False Then
                        'txtRcno.Text = Session("rcno")
                        GridView1_SelectedIndexChanged(New Object(), New EventArgs)
                    End If


                    txtDetail.Text = Session("gridsqlCompanyDetail")
                    SqlDataSource2.SelectCommand = txtDetail.Text
                    SqlDataSource2.DataBind()
                    GridView2.DataSourceID = "SqlDataSource2"

                    If String.IsNullOrEmpty(Session("rcnoDetail")) = False Then
                        GridView2_SelectedIndexChanged(New Object(), New EventArgs)
                    End If

                    Session.Remove("customerfrom")
                    Session.Remove("rcno")
                    Session.Remove("accountid")
                    Session.Remove("locationid")

                    Session.Remove("gridsqlCompanyDetail")
                    Session.Remove("rcnoDetail")
                    Session.Remove("armodule")

                    If Session("contracttype") = "CORPORATESVC" Then
                        Session.Remove("contracttype")
                        tb1.ActiveTabIndex = 1
                        'Session.Add("locationselectedsvc", txtLocationIDSelectedsVC.Text)
                        txtLocationIDSelectedsVC.Text = Session("locationselectedsvc")
                        'tb1_ActiveTabChanged(sender, e)
                        btnTransactionsSvc_Click(sender, e)
                    Else
                        Session.Remove("contracttype")
                        btnTransactions_Click(sender, e)
                    End If

                    '''''''''''''''''''''
                Else

                    'txt.Text = " SELECT distinct a.Rcno, a.AccountId, a.InActive, a.ID, a.Name, a.ARCurrency, a.Location, b.Bal, a.Telephone, a.Fax, a.Address1, a.AddPOstal, a.BillAddress1, a.BillPostal, a.ContactPerson ,a.ARTerm, a.Industry,  a.LocateGrp, a.CompanyGroup, a.AccountNo, a.Salesman, a.AddStreet, a.AddBuilding, A.AddCity, A.AddState, A.AddCountry, a.BillStreet, a.BillBuilding, A.BillCity, A.BillState, A.BillCountry,  a.CreatedBy, a.CreatedOn, a.LastModifiedBy, a.LastModifiedOn, a.AutoEmailInvoice, a.AutoEmailSOA, a.UnsubscribeAutoEmailDate  "
                    'txt.Text = txt.Text + " FROM tblcompany a left join companybal b on a.Accountid = b.Accountid left join tblcompanyLocation c on a.Accountid = c.Accountid where a.Inactive=0 "

                    'If txtDisplayRecordsLocationwise.Text = "Y" Then
                    '    'txt.Text = " SELECT distinct a.Rcno, a.AccountId, a.InActive, a.ID, a.Name, a.ARCurrency, a.Location, b.Bal, a.Telephone, a.Fax, a.Address1, a.AddPOstal, a.BillAddress1, a.BillPostal, a.ContactPerson ,a.ARTerm, a.Industry,  a.LocateGrp, a.CompanyGroup, a.AccountNo, a.Salesman, a.AddStreet, a.AddBuilding, A.AddCity, A.AddState, A.AddCountry, a.BillStreet, a.BillBuilding, A.BillCity, A.BillState, A.BillCountry,  a.CreatedBy, a.CreatedOn, a.LastModifiedBy, a.LastModifiedOn FROM tblcompany a left join companybal b on a.Accountid = b.Accountid left join tblcompanyLocation c on a.Accountid = c.Accountid where a.Inactive=0 "
                    '    txt.Text = txt.Text + " and a.location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "')"
                    'End If

                    ''If txtDisplayRecordsLocationwise.Text = "N" Then
                    ''    txt.Text = " SELECT * FROM tblcompany a left join CustomerBal b on a.Accountid = b.Accountid where a.Inactive=0 "
                    ''End If

                    ''If txtDisplayRecordsLocationwise.Text = "Y" Then
                    ''    txt.Text = txt.Text & " and location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "')"
                    ''End If

                    'txt.Text = txt.Text & " ORDER BY a.rcno DESC, a.Name limit 100"

                    'SqlDataSource1.SelectCommand = txt.Text
                    'SqlDataSource1.DataBind()
                    'GridView1.DataSourceID = "SqlDataSource1"

                End If


                txtAccountID.Attributes.Add("readonly", "readonly")

                tb1.ActiveTabIndex = 0
                ModalPopupExtender1.Hide()


                If Session("SecGroupAuthority") <> "ADMINISTRATOR" Then
                    tb1.Tabs(4).Visible = False
                End If

                txtCutOffDate.Text = Convert.ToDateTime(Now).ToString("dd/MM/yyyy")
                Session("cutoffoscustomer") = txtCutOffDate.Text

                If txtDisplayRecordsLocationwise.Text = "N" Then
                    GridViewContractESM.Columns(1).ControlStyle.CssClass = "dummybutton"
                    GridViewContractESM.Columns(1).HeaderStyle.CssClass = "dummybutton"
                    GridViewContractESM.Columns(1).ItemStyle.CssClass = "dummybutton"

                    GridViewInvoiceESM.Columns(1).ControlStyle.CssClass = "dummybutton"
                    GridViewInvoiceESM.Columns(1).HeaderStyle.CssClass = "dummybutton"
                    GridViewInvoiceESM.Columns(1).ItemStyle.CssClass = "dummybutton"
                End If
                'Convert.ToDateTime(dt.Rows(0)("StartDate")).ToString("dd/MM/yyyy")
            Catch ex As Exception
                InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "PAGE LOAD - NOT POSTBACK", ex.Message.ToString, txtAccountID.Text)
            End Try

        Else
            'If txtTransactionSelected.Text = "Y" Then
            '    Session.Add("customerfrom", "Corporate")
            '    Session.Add("AccountID", txtAccountID.Text)
            '    Session("contracttype") = "CORPORATE"
            '    Session("companygroup") = ddlCompanyGrp.Text
            '    Session("accountid") = txtAccountID.Text.Trim
            '    Session("custname") = txtNameE.Text
            '    txtTransactionSelected.Text = "N"
            'Else
            Session.Remove("customerfrom")
            'End If

        End If
        txtBlock.Attributes.Add("onchange", "UpdateBillingDetails()")
        txtNo.Attributes.Add("onchange", "UpdateBillingDetails()")
        txtFloor.Attributes.Add("onchange", "UpdateBillingDetails()")
        txtUnit.Attributes.Add("onchange", "UpdateBillingDetails()")
        txtAddress.Attributes.Add("onchange", "UpdateBillingDetails()")
        txtStreet.Attributes.Add("onchange", "UpdateBillingDetails()")
        txtBuilding.Attributes.Add("onchange", "UpdateBillingDetails()")
        ddlCity.Attributes.Add("onchange", "UpdateBillingDetails()")
        ddlState.Attributes.Add("onchange", "UpdateBillingDetails()")
        ddlCountry.Attributes.Add("onchange", "UpdateBillingDetails()")
        txtPostal.Attributes.Add("onchange", "UpdateBillingDetails()")
        'txtTelephone.Attributes.Add("onchange", "UpdateBillingDetails()")
        'txtFax.Attributes.Add("onchange", "UpdateBillingDetails()")
        'txtTelephone2.Attributes.Add("onchange", "UpdateBillingDetails()")
        'txtMobile.Attributes.Add("onchange", "UpdateBillingDetails()")
        'chkOffAddr.Attributes.Add("onchange", "UpdateBillingDetails()")

        txtSearch.Attributes.Add("onblur", "WaterMark(this, event);")
        txtSearch.Attributes.Add("onfocus", "WaterMark(this, event);")
        txtMarketSegmentIDsvc.Attributes.Add("readonly", "readonly")

        ddlOffCity.Attributes.Add("onchange", "getCountry()")
        ddlBillCity.Attributes.Add("onchange", "getCountry()")
        ddlCity.Attributes.Add("onchange", "getCountry()")
        ddlBillCitySvc.Attributes.Add("onchange", "getCountry()")

        txtOldSalesman.Attributes.Add("readonly", "readonly")
        txtLocationIDEditSalesman.Attributes.Add("readonly", "readonly")
        txtCustomerNameEditSalesman.Attributes.Add("readonly", "readonly")
        txtServiceAddressEditSalesman.Attributes.Add("readonly", "readonly")

        'txtAccountIDCP.Attributes.Add("readonly", "readonly")
        CheckTab()

        'ClientScript.RegisterStartupScript(Page.GetType, "str", "TabChanged(sender,e);", True)
    End Sub

    Public Sub PopulateDropDownList(ByVal query As String, ByVal textField As String, ByVal valueField As String, ByVal ddl As DropDownList)
        Try

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
                con.Dispose()
            End Using
            'End Using
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "PopulateDropDownList", ex.Message.ToString, query + " , " + TryCast(ddl, DropDownList).Text + " , " + " , " + textField + " , " + valueField)
        End Try
    End Sub

    Private Sub CheckTab()
        If ConfigurationManager.AppSettings("DomainName").ToString() = "PEST-PRO" Then
            TabPanel5.Visible = False
        Else
            TabPanel5.Visible = True

        End If
        'If tb1.ActiveTabIndex = 1 Then
        '    GridView1.CssClass = "dummybutton"
        '    btnADD.CssClass = "dummybutton"
        '    btnCopyAdd.CssClass = "dummybutton"
        '    btnDelete.CssClass = "dummybutton"
        '    btnContract.CssClass = "dummybutton"
        '    btnQuit.CssClass = "dummybutton"
        '    btnFilter.CssClass = "dummybutton"
        '    btnResetSearch.CssClass = "dummybutton"
        '    txtSearchCust.CssClass = "dummybutton"
        'ElseIf tb1.ActiveTabIndex = 2 Then
        '    GridView1.CssClass = "dummybutton"
        '    btnADD.CssClass = "dummybutton"
        '    btnCopyAdd.CssClass = "dummybutton"
        '    btnDelete.CssClass = "dummybutton"
        '    btnContract.CssClass = "dummybutton"
        '    btnQuit.CssClass = "dummybutton"
        '    btnFilter.CssClass = "dummybutton"
        '    btnResetSearch.CssClass = "dummybutton"
        '    txtSearchCust.CssClass = "dummybutton"
        'ElseIf tb1.ActiveTabIndex = 0 Then

        '    GridView1.CssClass = "visiblebutton"
        '    btnADD.CssClass = "visiblebutton"
        '    btnCopyAdd.CssClass = "visiblebutton"
        '    btnDelete.CssClass = "visiblebutton"
        '    btnContract.CssClass = "visiblebutton"
        '    btnQuit.CssClass = "visiblebutton"
        '    btnFilter.CssClass = "visiblebutton"
        '    btnResetSearch.CssClass = "visiblebutton"
        '    txtSearchCust.CssClass = "visiblebutton"
        'End If
    End Sub

    'Private Sub GenerateAlphabets()
    '    Dim alphabets As New List(Of ListItem)()
    '    Dim alphabet As New ListItem()
    '    alphabet.Value = "A"
    '    alphabet.Selected = alphabet.Value.Equals(ViewState("CurrentAlphabet"))
    '    alphabets.Add(alphabet)
    '    For i As Integer = 66 To 90
    '        alphabet = New ListItem()
    '        alphabet.Value = [Char].ConvertFromUtf32(i)
    '        alphabet.Selected = alphabet.Value.Equals(ViewState("CurrentAlphabet"))
    '        alphabets.Add(alphabet)
    '    Next
    '    ' MessageBox.Message.Alert(Page, alphabets.Count.ToString, "str")
    '    rptAlphabets.DataSource = alphabets
    '    rptAlphabets.DataBind()
    'End Sub


    Private Function ValidateTINEditCheckOS() As Boolean
        If txtTINOld.Text <> "" Or String.IsNullOrEmpty(txtTINOld.Text) = False Then
            If txtTIN.Text = "" Or String.IsNullOrEmpty(txtTIN.Text) Then

                Try
                    Dim conn As MySqlConnection = New MySqlConnection()

                    conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                    conn.Open()


                    Dim Command As MySqlCommand = New MySqlCommand

                    Command.CommandType = CommandType.Text
                    Dim qry As String = ""

                    qry = "SELECT * FROM tblsales where BalanceBase>0 and EI='Y';"

                    Command.CommandText = qry
                    Command.Connection = conn

                    Dim dr As MySqlDataReader = Command.ExecuteReader()
                    Dim dt As New DataTable
                    dt.Load(dr)

                    If dt.Rows.Count > 0 Then
                        conn.close()
                        conn.dispose()

                        Return True
                    Else
                        conn.close()
                        conn.dispose()

                        Return False
                    End If
                Catch ex As Exception
                    InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "ValidateTINEditCheckOS", ex.Message.ToString, txtAccountID.Text)

                End Try
            Else
                Return False
            End If

        Else
            Return False

        End If
    End Function

    Private Sub TINEditUpdateSales()
        Try
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()


            Dim Command As MySqlCommand = New MySqlCommand

            Command.CommandType = CommandType.Text
            Dim qry As String = ""

            qry = "SELECT CalendarPeriod FROM tblPERIOD where ARLock<>'Y' and ARLockE<>'Y'"

            Command.CommandText = qry
            Command.Connection = conn

            Dim dr As MySqlDataReader = Command.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then
                For i As Int16 = 0 To dt.rows.count - 1

                    Dim commandUpdateInvoice As MySqlCommand = New MySqlCommand
                    commandUpdateInvoice.CommandType = CommandType.Text
                    Dim sqlUpdateInvoice As String = "update tblsales set TaxIdentificationNo=@TIN,EI='Y' where AccountID=@AccountID and EI='N' and GLPeriod=@period"
                    InsertIntoTblWebEventLog("INVOICE", "UPDATETININSALES", sqlUpdateInvoice, txtCreatedBy.Text)

                    commandUpdateInvoice.CommandText = sqlUpdateInvoice
                    commandUpdateInvoice.Parameters.Clear()
                    commandUpdateInvoice.Parameters.AddwithValue("@TIN", txtTIN.Text)
                    commandUpdateInvoice.Parameters.AddwithValue("@Period", dt.Rows(i)("CalendarPeriod"))
                    commandUpdateInvoice.Parameters.AddwithValue("@AccountID", txtAccountID.Text)
                    InsertIntoTblWebEventLog("INVOICE", "UPDATETININSALES", txtAccountID.Text, txtCreatedBy.Text)

                    commandUpdateInvoice.Connection = conn
                    commandUpdateInvoice.ExecuteNonQuery()
                    commandUpdateInvoice.Dispose()

                Next

            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "TINEditUpdateSales", ex.Message.ToString, txtAccountID.Text)

        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        'If ddlCompanyGrp.Text = "-1" Then
        '    ' MessageBox.Message.Alert(Page, "Company Group cannot be blank!!!", "str")
        '    lblAlert.Text = "COMPANY GROUP CANNOT BE BLANK"
        '    txtCreatedOn.Text = ""
        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        '    Return
        'End If

        If txtDisplayRecordsLocationwise.Text = "Y" Then
            If ddlLocation.SelectedIndex = 0 Then
                lblAlert.Text = "MASTER BRANCH CANNOT BE BLANK"
                ddlLocation.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                Exit Sub
            End If
        End If

        If ddlOffCity.SelectedIndex = 0 Then
            lblAlert.Text = "CITY CANNOT BE BLANK"
            ddlOffCity.Focus()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            Exit Sub
        End If

        If String.IsNullOrEmpty(txtNameE.Text.Trim) = True Then
            ' MessageBox.Message.Alert(Page, "Name cannot be blank!!!", "str")
            lblAlert.Text = "NAME CANNOT BE BLANK"
            txtCreatedOn.Text = ""
            txtNameE.Focus()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            Return
        End If

        If Label24.Visible = True And String.IsNullOrEmpty(txtRegNo.Text.Trim) = True Then
            lblAlert.Text = "COMPANY REGISTRATION NO. CANNOT BE BLANK"
            txtCreatedOn.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            Return
        End If

        If ddlIndustry.Text = "-1" Then
            ' MessageBox.Message.Alert(Page, "Company Group cannot be blank!!!", "str")
            lblAlert.Text = "INDUSTRY CANNOT BE BLANK"
            txtCreatedOn.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            Return
        End If

        'If ddlSalesMan.Text = "-1" Then
        '    ' MessageBox.Message.Alert(Page, "Company Group cannot be blank!!!", "str")
        '    lblAlert.Text = "SALESMAN CANNOT BE BLANK"
        '    txtCreatedOn.Text = ""
        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        '    Return

        'End If

        'If txtBillingName.Text = "" Then
        If String.IsNullOrEmpty(txtBillingName.Text.Trim) = True Then
            lblAlert.Text = "BILLING NAME CANNOT BE BLANK"
            txtCreatedOn.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            txtBillingName.Focus()
            Return
        End If


        If String.IsNullOrEmpty(txtBillAddress.Text.Trim) = True Then
            lblAlert.Text = "BILL STREET ADDRESS1 CANNOT BE BLANK"
            txtCreatedOn.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            txtBillAddress.Focus()
            Return
        End If

        'If txtBillCP1Contact.Text = "" Then
        If String.IsNullOrEmpty(txtBillCP1Contact.Text.Trim) = True Then
            lblAlert.Text = "BILLING CONTACT PERSON 1 CANNOT BE BLANK"
            txtCreatedOn.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            txtBillCP1Contact.Focus()
            Return
        End If

        If ddlTerms.Text = "-1" Then
            ' MessageBox.Message.Alert(Page, "Company Group cannot be blank!!!", "str")
            lblAlert.Text = "TERMS CANNOT BE BLANK"
            txtCreatedOn.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            Return
        End If


        If ddlCurrency.Text = "-1" Then
            ' MessageBox.Message.Alert(Page, "Company Group cannot be blank!!!", "str")
            lblAlert.Text = "CURRENCY CANNOT BE BLANK"
            txtCreatedOn.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            Return
        End If


        If ddlOffCountry.SelectedIndex = 0 Then
            lblAlert.Text = "COUNTRY (OFFICE ADDRESS) CANNOT BE BLANK"
            txtCreatedOn.Text = ""
            ddlOffCountry.Focus()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            Exit Sub
        End If



        If ddlBillCountry.SelectedIndex = 0 Then
            lblAlert.Text = "COUNTRY (BILL ADDRESS) CANNOT BE BLANK"
            txtCreatedOn.Text = ""
            ddlBillCountry.Focus()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            Exit Sub
        End If

        If String.IsNullOrEmpty(txtCreditLimit.Text.Trim) = True Then
            txtCreditLimit.Text = "0.00"
        End If

        If txtDisplayRecordsLocationwise.Text = "Y" Then
            If ddlLocation.SelectedIndex = 0 Then
                lblAlert.Text = "LOCATION CANNOT BE BLANK"
                ddlLocation.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                Exit Sub
            End If
        End If

        If chkSendStatementInv.Checked = False And chkAutoEmailInvoice.Checked = False Then
            If String.IsNullOrEmpty(txtBillingOptionRemarks.Text) Then
                lblAlert.Text = "BILLING REMARKS CANNOT BE BLANK"
                txtCreatedOn.Text = ""
                txtBillingOptionRemarks.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                Exit Sub
            End If
        End If

        'Postal Code Validation


        If txtPostalValidate.Text.ToUpper = "TRUE" Then
            If String.IsNullOrEmpty(txtOffPostal.Text) Then
                lblAlert.Text = "OFFICE POSTAL CANNOT BE BLANK"
                txtOffPostal.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                Exit Sub
            End If

            If String.IsNullOrEmpty(txtBillPostal.Text) Then
                lblAlert.Text = "BILLING ADDRESS POSTAL CANNOT BE BLANK"
                txtBillPostal.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                Exit Sub
            End If

        End If
        'MessageBox.Message.Alert(Page, "Val1", "str")
        If Validation() = False Then
            txtCreatedOn.Text = ""
            Return
        End If

        'If IsValidEmailAddress(txtOffCont1Email.Text) = False Then
        '    lblAlert.Text = "PLEASE ENTER VALID EMAIL"
        '    Return
        'End If


        If txtMode.Text = "NEW" Then
            Try

                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                If ValidateSave(conn) = False Then
                    '  txtCreatedOn.Text = ""
                    '   Return

                End If

                'Dim CustName As String = IgnoredWords(conn, txtNameE.Text.ToUpper)

                'Dim command1 As MySqlCommand = New MySqlCommand

                'command1.CommandType = CommandType.Text

                'command1.CommandText = "SELECT Name FROM tblcompany where Name like '%" & CustName & "%'"
                'command1.Parameters.AddWithValue("@name", CustName)
                'command1.Connection = conn

                'Dim dr As MySqlDataReader = command1.ExecuteReader()
                'Dim dt As New System.Data.DataTable
                'dt.Load(dr)

                'If dt.Rows.Count > 0 Then
                '    For i As Int32 = 0 To dt.Rows.Count - 1
                '        If CustName = IgnoredWords(conn, dt.Rows(i)("Name").ToString.ToUpper) Then
                '            lblAlert.Text = "Customer Name already exists"
                '            txtCreatedOn.Text = ""
                '            Exit Sub
                '        End If
                '    Next

                'End If


                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                Dim qry As String = "INSERT INTO tblcompany(Id,Name,AddBlock,AddNos,AddFloor,AddUnit,AddBuilding,AddStreet,AddPostal,AddState,AddCity,AddCountry,Telephone,Fax,BillingAddress,"
                qry = qry + "BillBlock,BillNos,BillFloor,BillUnit,BillBuilding,BillStreet,BillPostal,BillState,BillCity,BillCountry,ContactPerson,Comments,RocNos,RocRegDate,AuthorizedCapital,"
                qry = qry + "PaidupCapital,CompanyType,Industry,FinanceCompanyId,FinanceCompany,ArLimit,ApLimit,SalesLimit,PurchaseLimit,ApCurrency,ArCurrency,SendStatement,GstRegistered,GstNos,Status,"
                qry = qry + "Address1,BillAddress1,ContactPersonEmail,Website,Source,ARBal,APBal,Sales,Purchase,LocateGRP,SalesGRP,Dealer,LoginID,Email,Password,WebLevel,ARTERM,APTERM,PriceGroup,InChargeID,"
                qry = qry + "Age0,Age30,Age60,Age90,Age120,SalesMan,StopSalesYN,StopPurchYN,SpecCode,ArWarning,StartDate,LicenseNumber,LicenseInfo,SalesGST,ArMethod,ApMethod,ProductM1,ProductM2,ProductM3,ProductM4,"
                qry = qry + "ProductF1,ProductF2,ProductF3,ProductF4,RentalTerm,CompanyGroup,Donor,Member,MemberType,MemberID,GIROID,DateJoin,DateExpired,DateTerminate,TemplateNo,ARLedger,ARSubLedger,APLedger,APSubLedger,"
                qry = qry + "SrcCompID,DiscountPct,PreferredCustYN,ChkGstInclusive,Reason,Boardmember,BoardDesignation,period,Intriducer,Organization,chkLetterIndemnity,LetterIndemnitySignedBy,LeterDate,CreatedBy,CreatedOn,"
                qry = qry + "LastModifiedBy,LastModifiedOn,BillTelephone,BillFax,Name2,WebLoginID,WebLoginPassWord,WebAccessLevel,WebOneTimePassWord,BillContactPerson,WebGroupDealer,WebDisable,WebID,OTPMobile,OTPYN,OTPGenerateDate,"
                qry = qry + "HideInStock,OverdueDaysLimit,OverdueDaysLimitActive,OverdueDaysWarning,OverdueDaysWarningActive,chkAR,DueDaysStopFreq,SubCompanyNo,SourceCompany,chkSendServiceReport,Telephone2,BillTelephone2,Mobile,BillMobile,"
                qry = qry + "SoPriceGroup,POPrefix,PONumber,LastStatus,OverdueMonthWarning,OverDueMonthLimit,AccountNo,FlowFrom,FlowTo,InActive,ShippingTerm,InterCompany,AutoEmailServ,ReportFormatServ,WebUploadDate,IsCustomer,IsSupplier,PaxBased,"
                qry = qry + "BillMonthly,DiscType,ARPDFFromat,EmailConsolidate,AccountID,OffContact1Position,OffContact1Tel,OffContact1Fax,OffContact1Tel2,OffContact1Mobile,BillContact1Position,BillContact2,BillContact2Position,"
                qry = qry + "BillContact1Email,BillContact2Email,BillContact2Tel,BillContact2Fax,BillContact2Tel2,BillContact2Mobile,OffContact1,OffContactPosition,BillingSettings,BillingName,TermsDay,Location, DefaultInvoiceFormat, AutoEmailInvoice,AutoEmailSOA,HardCopyInvoice,EmailNotificationOfSchedule,EmailNotificationOfJobProgress,MandatoryServiceReportPhotos,DisplayTimeInTimeOutInServiceReport,BillingOptionRemarks,RequireEBilling, TaxIdentificationNo, SalesTaxRegistrationNo)VALUES(@Id,@Name,@AddBlock,@AddNos,@AddFloor,@AddUnit,@AddBuilding,@AddStreet,@AddPostal,@AddState,@AddCity,@AddCountry,@Telephone,@Fax,@BillingAddress,@BillBlock,@BillNos,"
                qry = qry + "@BillFloor,@BillUnit,@BillBuilding,@BillStreet,@BillPostal,@BillState,@BillCity,@BillCountry,@ContactPerson,@Comments,@RocNos,@RocRegDate,@AuthorizedCapital,@PaidupCapital,@CompanyType,@Industry,@FinanceCompanyId,@FinanceCompany,"
                qry = qry + "@ArLimit,@ApLimit,@SalesLimit,@PurchaseLimit,@ApCurrency,@ArCurrency,@SendStatement,@GstRegistered,@GstNos,@Status,@Address1,@BillAddress1,@ContactPersonEmail,@Website,@Source,@ARBal,@APBal,@Sales,@Purchase,@LocateGRP,@SalesGRP,@Dealer,"
                qry = qry + "@LoginID,@Email,@Password,@WebLevel,@ARTERM,@APTERM,@PriceGroup,@InChargeID,@Age0,@Age30,@Age60,@Age90,@Age120,@SalesMan,@StopSalesYN,@StopPurchYN,@SpecCode,@ArWarning,@StartDate,@LicenseNumber,@LicenseInfo,@SalesGST,@ArMethod,@ApMethod,"
                qry = qry + "@ProductM1,@ProductM2,@ProductM3,@ProductM4,@ProductF1,@ProductF2,@ProductF3,@ProductF4,@RentalTerm,@CompanyGroup,@Donor,@Member,@MemberType,@MemberID,@GIROID,@DateJoin,@DateExpired,@DateTerminate,@TemplateNo,@ARLedger,@ARSubLedger,"
                qry = qry + "@APLedger,@APSubLedger,@SrcCompID,@DiscountPct,@PreferredCustYN,@ChkGstInclusive,@Reason,@Boardmember,@BoardDesignation,@period,@Intriducer,@Organization,@chkLetterIndemnity,@LetterIndemnitySignedBy,@LeterDate,@CreatedBy,@CreatedOn,"
                qry = qry + "@LastModifiedBy,@LastModifiedOn,@BillTelephone,@BillFax,@Name2,@WebLoginID,@WebLoginPassWord,@WebAccessLevel,@WebOneTimePassWord,@BillContactPerson,@WebGroupDealer,@WebDisable,@WebID,@OTPMobile,@OTPYN,@OTPGenerateDate,@HideInStock,"
                qry = qry + "@OverdueDaysLimit,@OverdueDaysLimitActive,@OverdueDaysWarning,@OverdueDaysWarningActive,@chkAR,@DueDaysStopFreq,@SubCompanyNo,@SourceCompany,@chkSendServiceReport,@Telephone2,@BillTelephone2,@Mobile,@BillMobile,@SoPriceGroup,@POPrefix,"
                qry = qry + "@PONumber,@LastStatus,@OverdueMonthWarning,@OverDueMonthLimit,@AccountNo,@FlowFrom,@FlowTo,@InActive,@ShippingTerm,@InterCompany,@AutoEmailServ,@ReportFormatServ,@WebUploadDate,@IsCustomer,@IsSupplier,@PaxBased,@BillMonthly,@DiscType,@ARPDFFromat,@EmailConsolidate"
                qry = qry + ",@AccountID,@OffContact1Position,@OffContact1Tel,@OffContact1Fax,@OffContact1Tel2,@OffContact1Mobile,@BillContact1Position,@BillContact2,@BillContact2Position,@BillContact1Email,@BillContact2Email,@BillContact2Tel,@BillContact2Fax,@BillContact2Tel2,@BillContact2Mobile,@OffContact1,@OffContactPosition,@BillingSettings,@BillingName,@TermsDay,@Location, @DefaultInvoiceFormat, @AutoEmailInvoice,@AutoEmailSOA,@HardCopyInvoice,@EmailNotificationOfSchedule,@EmailNotificationOfJobProgress,@MandatoryServiceReportPhotos,@DisplayTimeInTimeOutInServiceReport,@BillingOptionRemarks,@RequireEBilling,@TaxIdentificationNo, @SalesTaxRegistrationNo);"


                command.CommandText = qry
                command.Parameters.Clear()

                command.Parameters.AddWithValue("@Id", "")
                command.Parameters.AddWithValue("@Name", txtNameE.Text.ToUpper)
                command.Parameters.AddWithValue("@AddBlock", "")
                command.Parameters.AddWithValue("@AddNos", "")
                command.Parameters.AddWithValue("@AddFloor", "")
                command.Parameters.AddWithValue("@AddUnit", "")
                command.Parameters.AddWithValue("@AddBuilding", txtOffBuilding.Text.ToUpper)
                command.Parameters.AddWithValue("@AddStreet", txtOffStreet.Text.ToUpper)
                command.Parameters.AddWithValue("@AddPostal", txtOffPostal.Text.ToUpper)
                If ddlOffState.Text = txtDDLText.Text Then
                    command.Parameters.AddWithValue("@AddState", "")
                Else
                    command.Parameters.AddWithValue("@AddState", ddlOffState.Text.ToUpper)
                End If

                If ddlOffCity.Text = txtDDLText.Text Then
                    command.Parameters.AddWithValue("@AddCity", "")
                Else
                    command.Parameters.AddWithValue("@AddCity", ddlOffCity.Text.ToUpper)
                End If
                If ddlOffCountry.Text = txtDDLText.Text Then
                    command.Parameters.AddWithValue("@AddCountry", "")
                Else
                    command.Parameters.AddWithValue("@AddCountry", ddlOffCountry.Text.ToUpper)
                End If
                command.Parameters.AddWithValue("@Telephone", txtOffContactNo.Text.ToUpper)
                command.Parameters.AddWithValue("@Fax", txtOffFax.Text.ToUpper)
                If chkOffAddr.Checked = True Then
                    command.Parameters.AddWithValue("@BillingAddress", 1)

                Else
                    command.Parameters.AddWithValue("@BillingAddress", 0)

                End If
                command.Parameters.AddWithValue("@BillBlock", "")
                command.Parameters.AddWithValue("@BillNos", "")
                command.Parameters.AddWithValue("@BillFloor", "")
                command.Parameters.AddWithValue("@BillUnit", "")
                command.Parameters.AddWithValue("@BillBuilding", txtBillBuilding.Text.ToUpper)
                command.Parameters.AddWithValue("@BillStreet", txtBillStreet.Text.ToUpper)
                command.Parameters.AddWithValue("@BillPostal", txtBillPostal.Text.ToUpper)
                If ddlBillState.Text = txtDDLText.Text Then
                    command.Parameters.AddWithValue("@BillState", "")
                Else
                    command.Parameters.AddWithValue("@BillState", ddlBillState.Text.ToUpper)
                End If
                If ddlBillCity.Text = txtDDLText.Text Then
                    command.Parameters.AddWithValue("@BillCity", "")
                Else
                    command.Parameters.AddWithValue("@BillCity", ddlBillCity.Text.ToUpper)
                End If
                If ddlBillCountry.Text = txtDDLText.Text Then
                    command.Parameters.AddWithValue("@BillCountry", "")
                Else
                    command.Parameters.AddWithValue("@BillCountry", ddlBillCountry.Text.ToUpper)
                End If

                command.Parameters.AddWithValue("@ContactPerson", txtOffContactPerson.Text.ToUpper)

                command.Parameters.AddWithValue("@Comments", txtComments.Text.ToUpper)
                command.Parameters.AddWithValue("@RocNos", txtRegNo.Text.ToUpper)
                command.Parameters.AddWithValue("@RocRegDate", DBNull.Value)

                command.Parameters.AddWithValue("@AuthorizedCapital", 0)
                command.Parameters.AddWithValue("@PaidupCapital", 0)
                command.Parameters.AddWithValue("@CompanyType", "")
                If ddlIndustry.Text = txtDDLText.Text Then
                    command.Parameters.AddWithValue("@Industry", "")
                Else
                    command.Parameters.AddWithValue("@Industry", ddlIndustry.Text.ToUpper)
                End If

                command.Parameters.AddWithValue("@FinanceCompanyId", "")
                command.Parameters.AddWithValue("@FinanceCompany", "")
                command.Parameters.AddWithValue("@ArLimit", txtCreditLimit.Text)
                command.Parameters.AddWithValue("@ApLimit", 0)
                command.Parameters.AddWithValue("@SalesLimit", 0)
                command.Parameters.AddWithValue("@PurchaseLimit", 0)
                command.Parameters.AddWithValue("@ApCurrency", "")
                command.Parameters.AddWithValue("@ArCurrency", ddlCurrency.SelectedItem.Text.ToUpper)

                command.Parameters.AddWithValue("@SendStatement", chkSendStatementSOA.Checked)
                command.Parameters.AddWithValue("@HardCopyInvoice", chkSendStatementInv.Checked)

                command.Parameters.AddWithValue("@EmailNotificationOfSchedule", chkEmailNotifySchedule.Checked)
                command.Parameters.AddWithValue("@EmailNotificationOfJobProgress", chkEmailNotifyJobProgress.Checked)
                command.Parameters.AddWithValue("@MandatoryServiceReportPhotos", chkPhotosMandatory.Checked)
                command.Parameters.AddWithValue("@DisplayTimeInTimeOutInServiceReport", chkDisplayTimeInTimeOut.Checked)

                command.Parameters.AddWithValue("@GstRegistered", 0)
                command.Parameters.AddWithValue("@GstNos", txtGSTRegNo.Text.ToUpper)
                command.Parameters.AddWithValue("@Status", ddlStatus.Text.ToUpper)
                command.Parameters.AddWithValue("@Address1", txtOffAddress1.Text.ToUpper)
                command.Parameters.AddWithValue("@BillAddress1", txtBillAddress.Text.ToUpper)
                command.Parameters.AddWithValue("@ContactPersonEmail", txtOffCont1Email.Text.ToUpper)
                command.Parameters.AddWithValue("@Website", txtWebsite.Text.ToUpper)
                command.Parameters.AddWithValue("@Source", "")
                command.Parameters.AddWithValue("@ARBal", 0)
                command.Parameters.AddWithValue("@APBal", 0)
                command.Parameters.AddWithValue("@Sales", 0)
                command.Parameters.AddWithValue("@Purchase", 0)
                command.Parameters.AddWithValue("@LocateGRP", "")


                command.Parameters.AddWithValue("@SalesGRP", "")

                command.Parameters.AddWithValue("@Dealer", "")
                command.Parameters.AddWithValue("@LoginID", "")
                command.Parameters.AddWithValue("@Email", txtOffEmail.Text.ToUpper)
                command.Parameters.AddWithValue("@Password", "")
                command.Parameters.AddWithValue("@WebLevel", "")
                command.Parameters.AddWithValue("@ARTERM", ddlTerms.SelectedItem.Text.ToUpper)
                command.Parameters.AddWithValue("@APTERM", "")
                command.Parameters.AddWithValue("@PriceGroup", "")
                If ddlIncharge.Text = txtDDLText.Text Then
                    command.Parameters.AddWithValue("@InChargeID", "")

                Else
                    command.Parameters.AddWithValue("@InChargeID", ddlIncharge.Text.ToUpper)

                End If

                command.Parameters.AddWithValue("@Age0", 0)
                command.Parameters.AddWithValue("@Age30", 0)
                command.Parameters.AddWithValue("@Age60", 0)
                command.Parameters.AddWithValue("@Age90", 0)
                command.Parameters.AddWithValue("@Age120", 0)
                If ddlSalesMan.Text = txtDDLText.Text Then
                    command.Parameters.AddWithValue("@SalesMan", "")
                Else
                    command.Parameters.AddWithValue("@SalesMan", ddlSalesMan.Text.ToUpper)
                End If
                command.Parameters.AddWithValue("@StopSalesYN", "")
                command.Parameters.AddWithValue("@StopPurchYN", "")
                command.Parameters.AddWithValue("@SpecCode", txtSpecCode.Text.ToUpper)
                command.Parameters.AddWithValue("@ArWarning", 0)
                If txtStartDate.Text = "" Then
                    command.Parameters.AddWithValue("@StartDate", DBNull.Value)
                Else
                    command.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(txtStartDate.Text).ToString("yyyy-MM-dd"))
                End If
                command.Parameters.AddWithValue("@LicenseNumber", 0)
                command.Parameters.AddWithValue("@LicenseInfo", "")
                command.Parameters.AddWithValue("@SalesGST", "")
                command.Parameters.AddWithValue("@ArMethod", "")
                command.Parameters.AddWithValue("@ApMethod", "")
                command.Parameters.AddWithValue("@ProductM1", "")
                command.Parameters.AddWithValue("@ProductM2", "")
                command.Parameters.AddWithValue("@ProductM3", "")
                command.Parameters.AddWithValue("@ProductM4", "")
                command.Parameters.AddWithValue("@ProductF1", "")
                command.Parameters.AddWithValue("@ProductF2", "")
                command.Parameters.AddWithValue("@ProductF3", "")
                command.Parameters.AddWithValue("@ProductF4", "")
                command.Parameters.AddWithValue("@RentalTerm", "")
                If ddlCompanyGrp.Text = txtDDLText.Text Then
                    command.Parameters.AddWithValue("@CompanyGroup", "")
                Else
                    command.Parameters.AddWithValue("@CompanyGroup", ddlCompanyGrp.Text.ToUpper)
                End If

                command.Parameters.AddWithValue("@Donor", 0)
                command.Parameters.AddWithValue("@Member", 0)
                command.Parameters.AddWithValue("@MemberType", "")
                command.Parameters.AddWithValue("@MemberID", "")
                command.Parameters.AddWithValue("@GIROID", "")
                command.Parameters.AddWithValue("@DateJoin", DBNull.Value)
                command.Parameters.AddWithValue("@DateExpired", DBNull.Value)
                command.Parameters.AddWithValue("@DateTerminate", DBNull.Value)
                command.Parameters.AddWithValue("@TemplateNo", "")
                command.Parameters.AddWithValue("@ARLedger", "")
                command.Parameters.AddWithValue("@ARSubLedger", "")
                command.Parameters.AddWithValue("@APLedger", "")
                command.Parameters.AddWithValue("@APSubLedger", "")

                command.Parameters.AddWithValue("@SrcCompID", "")
                command.Parameters.AddWithValue("@DiscountPct", 0)
                command.Parameters.AddWithValue("@PreferredCustYN", "")
                command.Parameters.AddWithValue("@ChkGstInclusive", "")
                command.Parameters.AddWithValue("@Reason", "")
                command.Parameters.AddWithValue("@Boardmember", "")
                command.Parameters.AddWithValue("@BoardDesignation", "")
                command.Parameters.AddWithValue("@period", "")
                command.Parameters.AddWithValue("@Intriducer", "")
                command.Parameters.AddWithValue("@Organization", "")
                command.Parameters.AddWithValue("@chkLetterIndemnity", 0)
                command.Parameters.AddWithValue("@LetterIndemnitySignedBy", "")
                command.Parameters.AddWithValue("@LeterDate", DBNull.Value)
                command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                command.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))

                command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))

                command.Parameters.AddWithValue("@BillTelephone", txtBillCP1Tel.Text.ToUpper)
                command.Parameters.AddWithValue("@BillFax", txtBillCP1Fax.Text.ToUpper)
                command.Parameters.AddWithValue("@Name2", txtNameO.Text.ToUpper)
                command.Parameters.AddWithValue("@WebLoginID", "")
                command.Parameters.AddWithValue("@WebLoginPassWord", "")
                command.Parameters.AddWithValue("@WebAccessLevel", 0)
                command.Parameters.AddWithValue("@WebOneTimePassWord", "")

                command.Parameters.AddWithValue("@BillContactPerson", txtBillCP1Contact.Text.ToUpper)
                command.Parameters.AddWithValue("@WebGroupDealer", 0)
                command.Parameters.AddWithValue("@WebDisable", 0)
                command.Parameters.AddWithValue("@WebID", "")
                command.Parameters.AddWithValue("@OTPMobile", "")
                command.Parameters.AddWithValue("@OTPYN", 0)
                command.Parameters.AddWithValue("@OTPGenerateDate", DBNull.Value)
                command.Parameters.AddWithValue("@HideInStock", 0)
                command.Parameters.AddWithValue("@OverdueDaysLimit", 0)
                command.Parameters.AddWithValue("@OverdueDaysLimitActive", 0)
                command.Parameters.AddWithValue("@OverdueDaysWarning", 0)
                command.Parameters.AddWithValue("@OverdueDaysWarningActive", 0)
                command.Parameters.AddWithValue("@chkAR", 0)
                command.Parameters.AddWithValue("@DueDaysStopFreq", "")
                command.Parameters.AddWithValue("@SubCompanyNo", "")
                command.Parameters.AddWithValue("@SourceCompany", "")
                command.Parameters.AddWithValue("@chkSendServiceReport", 0)
                command.Parameters.AddWithValue("@Telephone2", txtOffContact2.Text.ToUpper)
                command.Parameters.AddWithValue("@BillTelephone2", txtBillCP1Tel2.Text.ToUpper)
                command.Parameters.AddWithValue("@Mobile", txtOffMobile.Text.ToUpper)
                command.Parameters.AddWithValue("@BillMobile", txtBillCP1Mobile.Text.ToUpper)
                command.Parameters.AddWithValue("@SoPriceGroup", "")
                command.Parameters.AddWithValue("@POPrefix", "")
                command.Parameters.AddWithValue("@PONumber", 0)
                command.Parameters.AddWithValue("@LastStatus", "")
                command.Parameters.AddWithValue("@OverdueMonthWarning", 0)
                command.Parameters.AddWithValue("@OverDueMonthLimit", 0)
                command.Parameters.AddWithValue("@AccountNo", "")
                command.Parameters.AddWithValue("@FlowFrom", "")
                command.Parameters.AddWithValue("@FlowTo", "")
                If chkInactive.Checked = True Then
                    command.Parameters.AddWithValue("@InActive", 1)

                Else
                    command.Parameters.AddWithValue("@InActive", 0)

                End If
                command.Parameters.AddWithValue("@ShippingTerm", "")
                command.Parameters.AddWithValue("@InterCompany", 0)
                command.Parameters.AddWithValue("@AutoEmailServ", 0)
                command.Parameters.AddWithValue("@ReportFormatServ", "")
                command.Parameters.AddWithValue("@WebUploadDate", DBNull.Value)
                command.Parameters.AddWithValue("@IsCustomer", 0)

                command.Parameters.AddWithValue("@IsSupplier", 0)

                command.Parameters.AddWithValue("@PaxBased", 0)
                command.Parameters.AddWithValue("@BillMonthly", 0)
                command.Parameters.AddWithValue("@DiscType", "")
                command.Parameters.AddWithValue("@ARPDFFromat", "")
                command.Parameters.AddWithValue("@EmailConsolidate", 0)
                command.Parameters.AddWithValue("@OffContact1Position", txtOffCont1Position.Text.ToUpper)
                command.Parameters.AddWithValue("@OffContact1Tel", txtOffCont1Tel.Text.ToUpper)
                command.Parameters.AddWithValue("@OffContact1Fax", txtOffCont1Fax.Text.ToUpper)
                command.Parameters.AddWithValue("@OffContact1Tel2", txtOffCont1Tel2.Text.ToUpper)
                command.Parameters.AddWithValue("@OffContact1Mobile", txtOffCont1Mobile.Text.ToUpper)
                command.Parameters.AddWithValue("@BillContact1Position", txtBillCP1Position.Text.ToUpper)
                command.Parameters.AddWithValue("@BillContact2", txtBillCP2Contact.Text.ToUpper)
                command.Parameters.AddWithValue("@BillContact2Position", txtBillCP2Position.Text.ToUpper)
                command.Parameters.AddWithValue("@BillContact1Email", txtBillCP1Email.Text.ToUpper)
                command.Parameters.AddWithValue("@BillContact2Email", txtBillCP2Email.Text.ToUpper)
                command.Parameters.AddWithValue("@BillContact2Tel", txtBillCP2Tel.Text.ToUpper)
                command.Parameters.AddWithValue("@BillContact2Fax", txtBillCP2Fax.Text.ToUpper)
                command.Parameters.AddWithValue("@BillContact2Tel2", txtBillCP2Tel2.Text.ToUpper)
                command.Parameters.AddWithValue("@BillContact2Mobile", txtBillCP2Mobile.Text.ToUpper)
                command.Parameters.AddWithValue("@OffContact1", txtOffCont1Name.Text.ToUpper)
                command.Parameters.AddWithValue("@OffContactPosition", txtOffPosition.Text.ToUpper)
                command.Parameters.AddWithValue("@BillingSettings", rdbBillingSettings.SelectedValue.ToString)
                command.Parameters.AddWithValue("@BillingName", txtBillingName.Text.ToUpper)
                'command.Parameters.AddWithValue("@Terms", ddlTerms.SelectedItem.Text)
                command.Parameters.AddWithValue("@TermsDay", 0)

                If ddlLocation.SelectedIndex = 0 Then
                    command.Parameters.AddWithValue("@Location", "")
                Else
                    command.Parameters.AddWithValue("@Location", ddlLocation.Text.ToUpper)
                End If

                If ddlDefaultInvoiceFormat.SelectedIndex = 0 Then
                    command.Parameters.AddWithValue("@DefaultInvoiceFormat", "")
                Else
                    command.Parameters.AddWithValue("@DefaultInvoiceFormat", ddlDefaultInvoiceFormat.Text)
                End If
                command.Parameters.AddWithValue("@BillingOptionRemarks", txtBillingOptionRemarks.Text.ToUpper)

                command.Parameters.AddWithValue("@AutoEmailInvoice", chkAutoEmailInvoice.Checked)
                command.Parameters.AddWithValue("@AutoEmailSOA", chkAutoEmailStatement.Checked)
                command.Parameters.AddWithValue("@RequireEBilling", chkRequireEBilling.Checked)

                command.Parameters.AddWithValue("@TaxIdentificationNo", txtTIN.Text.ToUpper)
                command.Parameters.AddWithValue("@SalesTaxRegistrationNo", txtSST.Text.ToUpper)

                GenerateAccountNo()
                command.Parameters.AddWithValue("@AccountID", txtAccountID.Text)
                command.Connection = conn
                command.ExecuteNonQuery()
                InsertContactMaster(conn)
                '  MessageBox.Message.Alert(Page, "Record added successfully!!!", "str")
                lblMessage.Text = "ADD: RECORD SUCCESSFULLY ADDED"
                lblAlert.Text = ""
                txtTINOld.Text = txtTIN.Text

                txtRcno.Text = command.LastInsertedId
                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CORP", txtAccountID.Text, "ADD", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")), 0, 0, 0, txtAccountID.Text, "", txtRcno.Text)

                lblAccountID.Text = txtAccountID.Text
                txtAccountIDtab2.Text = txtAccountID.Text
                lblAccountID2.Text = txtAccountID.Text
                txtAccountIDSelected.Text = txtAccountID.Text

                lblName.Text = txtNameE.Text
                lblName2.Text = txtNameE.Text
                conn.Close()

            Catch ex As Exception
                InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "ADD SAVE", ex.Message.ToString, txtAccountID.Text)
            End Try
            EnableControls()

        ElseIf txtMode.Text = "EDIT" Then
            If txtRcno.Text = "" Then
                ' MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
                lblAlert.Text = "SELECT RECORD TO EDIT"
                Return

            End If

            If ValidateTINEditCheckOS() = True Then
                lblAlert.tEXT = "TIN cannot be deleted as there are Outstanding Invoices"
                Return
            End If

           
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command2 As MySqlCommand = New MySqlCommand

                command2.CommandType = CommandType.Text

                command2.CommandText = "SELECT * FROM tblcompany where accountid=@id and rcno<>" & Convert.ToInt32(txtRcno.Text)
                command2.Parameters.AddWithValue("@id", txtAccountID.Text)
                command2.Connection = conn

                Dim dr1 As MySqlDataReader = command2.ExecuteReader()
                Dim dt1 As New System.Data.DataTable
                dt1.Load(dr1)

                If dt1.Rows.Count > 0 Then

                    '  MessageBox.Message.Alert(Page, "Account ID already exists!!!", "str")
                    lblAlert.Text = "Account ID already exists!!!"
                Else

                    Dim command1 As MySqlCommand = New MySqlCommand

                    command1.CommandType = CommandType.Text

                    command1.CommandText = "SELECT * FROM tblcompany where rcno=" & Convert.ToInt32(txtRcno.Text)
                    command1.Connection = conn

                    Dim dr As MySqlDataReader = command1.ExecuteReader()
                    Dim dt As New System.Data.DataTable
                    dt.Load(dr)

                    If dt.Rows.Count > 0 Then

                        Dim command As MySqlCommand = New MySqlCommand

                        command.CommandType = CommandType.Text
                        Dim qry As String = "UPDATE tblcompany SET Name = @Name,AddBuilding = @AddBuilding,AddStreet = @AddStreet,AddPostal = @AddPostal,AddState = @AddState,AddCity = @AddCity,AddCountry = @AddCountry,Telephone = @Telephone,Fax = @Fax,BillingAddress = @BillingAddress,BillBuilding = @BillBuilding,BillStreet = @BillStreet,BillPostal = @BillPostal,BillState = @BillState,BillCity = @BillCity,BillCountry = @BillCountry,ContactPerson = @ContactPerson,Comments = @Comments,RocNos = @RocNos,Industry = @Industry,Status = @Status,Address1 = @Address1,BillAddress1 = @BillAddress1,ContactPersonEmail = @ContactPersonEmail,Website = @Website,LocateGRP = @LocateGRP,InChargeID = @InChargeID,SalesMan = @SalesMan,StartDate = @StartDate,CompanyGroup = @CompanyGroup,LastModifiedBy = @LastModifiedBy,LastModifiedOn = @LastModifiedOn,BillTelephone = @BillTelephone,BillFax = @BillFax,Name2 = @Name2,BillContactPerson = @BillContactPerson,Telephone2 = @Telephone2,BillTelephone2 = @BillTelephone2,Mobile = @Mobile,BillMobile = @BillMobile,Inactive=@Inactive,OffContact1Position=@OffContact1Position,OffContact1Tel=@OffContact1Tel,OffContact1Fax=@OffContact1Fax,OffContact1Tel2=@OffContact1Tel2,OffContact1Mobile=@OffContact1Mobile,BillContact1Position=@BillContact1Position,BillContact2=@BillContact2,BillContact2Position=@BillContact2Position,BillContact1Email=@BillContact1Email,BillContact2Email=@BillContact2Email,BillContact2Tel=@BillContact2Tel,BillContact2Fax=@BillContact2Fax,BillContact2Tel2=@BillContact2Tel2,BillContact2Mobile=@BillContact2Mobile,Email=@Email,OffContact1=@OffContact1,OffContactPosition=@OffContactPosition,gstnos=@gstnos,BillingSettings=@BillingSettings,BillingName=@BillingName,ArTerm=@Terms,TermsDay=@TermsDay, ArCurrency =@Currency,  SendStatement =@SendStatement, Location=@Location, DefaultInvoiceFormat=@DefaultInvoiceFormat, AutoEmailInvoice= @AutoEmailInvoice, AutoEmailSOA= @AutoEmailSOA, ArLimit=@ArLimit, HardCopyInvoice=@HardCopyInvoice,EmailNotificationOfSchedule=@EmailNotificationOfSchedule,EmailNotificationOfJobProgress=@EmailNotificationOfJobProgress,MandatoryServiceReportPhotos=@MandatoryServiceReportPhotos,DisplayTimeInTimeOutInServiceReport=@DisplayTimeInTimeOutInServiceReport,BillingOptionRemarks=@BillingOptionRemarks,RequireEBilling=@RequireEBilling, TaxIdentificationNo =@TaxIdentificationNo, SalesTaxRegistrationNo =@SalesTaxRegistrationNo WHERE  rcno=" & Convert.ToInt32(txtRcno.Text)

                        command.CommandText = qry
                        command.Parameters.Clear()

                        command.Parameters.AddWithValue("@Id", "")
                        command.Parameters.AddWithValue("@Name", txtNameE.Text.ToUpper)
                        command.Parameters.AddWithValue("@AddBlock", "")
                        command.Parameters.AddWithValue("@AddNos", "")
                        command.Parameters.AddWithValue("@AddFloor", "")
                        command.Parameters.AddWithValue("@AddUnit", "")
                        command.Parameters.AddWithValue("@AddBuilding", txtOffBuilding.Text.ToUpper)
                        command.Parameters.AddWithValue("@AddStreet", txtOffStreet.Text.ToUpper)
                        command.Parameters.AddWithValue("@AddPostal", txtOffPostal.Text.ToUpper)
                        If ddlOffState.Text = txtDDLText.Text Then
                            command.Parameters.AddWithValue("@AddState", "")
                        Else
                            command.Parameters.AddWithValue("@AddState", ddlOffState.Text.ToUpper)
                        End If

                        If ddlOffCity.Text = txtDDLText.Text Then
                            command.Parameters.AddWithValue("@AddCity", "")
                        Else
                            command.Parameters.AddWithValue("@AddCity", ddlOffCity.Text.ToUpper)
                        End If
                        If ddlOffCountry.Text = txtDDLText.Text Then
                            command.Parameters.AddWithValue("@AddCountry", "")
                        Else
                            command.Parameters.AddWithValue("@AddCountry", ddlOffCountry.Text.ToUpper)
                        End If
                        command.Parameters.AddWithValue("@Telephone", txtOffContactNo.Text.ToUpper)
                        command.Parameters.AddWithValue("@Fax", txtOffFax.Text)
                        If chkOffAddr.Checked = True Then
                            command.Parameters.AddWithValue("@BillingAddress", 1)

                        Else
                            command.Parameters.AddWithValue("@BillingAddress", 0)

                        End If
                        command.Parameters.AddWithValue("@BillBlock", "")
                        command.Parameters.AddWithValue("@BillNos", "")
                        command.Parameters.AddWithValue("@BillFloor", "")
                        command.Parameters.AddWithValue("@BillUnit", "")
                        command.Parameters.AddWithValue("@BillBuilding", txtBillBuilding.Text.ToUpper)
                        command.Parameters.AddWithValue("@BillStreet", txtBillStreet.Text.ToUpper)
                        command.Parameters.AddWithValue("@BillPostal", txtBillPostal.Text.ToUpper)
                        If ddlBillState.Text = txtDDLText.Text Then
                            command.Parameters.AddWithValue("@BillState", "")
                        Else
                            command.Parameters.AddWithValue("@BillState", ddlBillState.Text.ToUpper)
                        End If
                        If ddlBillCity.Text = txtDDLText.Text Then
                            command.Parameters.AddWithValue("@BillCity", "")
                        Else
                            command.Parameters.AddWithValue("@BillCity", ddlBillCity.Text.ToUpper)
                        End If
                        If ddlBillCountry.Text = txtDDLText.Text Then
                            command.Parameters.AddWithValue("@BillCountry", "")
                        Else
                            command.Parameters.AddWithValue("@BillCountry", ddlBillCountry.Text.ToUpper)
                        End If

                        command.Parameters.AddWithValue("@ContactPerson", txtOffContactPerson.Text.ToUpper)

                        command.Parameters.AddWithValue("@Comments", txtComments.Text.ToUpper)
                        command.Parameters.AddWithValue("@RocNos", txtRegNo.Text.ToUpper)
                        command.Parameters.AddWithValue("@RocRegDate", DBNull.Value)

                        command.Parameters.AddWithValue("@AuthorizedCapital", 0)
                        command.Parameters.AddWithValue("@PaidupCapital", 0)
                        command.Parameters.AddWithValue("@CompanyType", "")
                        If ddlIndustry.Text = txtDDLText.Text Then
                            command.Parameters.AddWithValue("@Industry", "")
                        Else
                            command.Parameters.AddWithValue("@Industry", ddlIndustry.Text.ToUpper)
                        End If

                        command.Parameters.AddWithValue("@FinanceCompanyId", "")
                        command.Parameters.AddWithValue("@FinanceCompany", "")
                        command.Parameters.AddWithValue("@ArLimit", txtCreditLimit.Text)
                        command.Parameters.AddWithValue("@ApLimit", 0)
                        command.Parameters.AddWithValue("@SalesLimit", 0)
                        command.Parameters.AddWithValue("@PurchaseLimit", 0)
                        command.Parameters.AddWithValue("@ApCurrency", "")
                        'command.Parameters.AddWithValue("@ArCurrency", ddlCurrency.SelectedItem.Text)
                        command.Parameters.AddWithValue("@SendStatement", chkSendStatementSOA.Checked)
                        command.Parameters.AddWithValue("@HardCopyInvoice", chkSendStatementInv.Checked)

                        command.Parameters.AddWithValue("@EmailNotificationOfSchedule", chkEmailNotifySchedule.Checked)
                        command.Parameters.AddWithValue("@EmailNotificationOfJobProgress", chkEmailNotifyJobProgress.Checked)
                        command.Parameters.AddWithValue("@MandatoryServiceReportPhotos", chkPhotosMandatory.Checked)
                        command.Parameters.AddWithValue("@DisplayTimeInTimeOutInServiceReport", chkDisplayTimeInTimeOut.Checked)

                        command.Parameters.AddWithValue("@GstRegistered", 0)
                        command.Parameters.AddWithValue("@GstNos", txtGSTRegNo.Text.ToUpper)
                        command.Parameters.AddWithValue("@Status", ddlStatus.Text.ToUpper)
                        command.Parameters.AddWithValue("@Address1", txtOffAddress1.Text.ToUpper)
                        command.Parameters.AddWithValue("@BillAddress1", txtBillAddress.Text.ToUpper)
                        command.Parameters.AddWithValue("@ContactPersonEmail", txtOffCont1Email.Text.ToUpper)
                        command.Parameters.AddWithValue("@Website", txtWebsite.Text.ToUpper)
                        command.Parameters.AddWithValue("@Source", "")
                        command.Parameters.AddWithValue("@ARBal", 0)
                        command.Parameters.AddWithValue("@APBal", 0)
                        command.Parameters.AddWithValue("@Sales", 0)
                        command.Parameters.AddWithValue("@Purchase", 0)
                        command.Parameters.AddWithValue("@LocateGRP", "")

                        command.Parameters.AddWithValue("@SalesGRP", "")

                        command.Parameters.AddWithValue("@Dealer", "")
                        command.Parameters.AddWithValue("@LoginID", "")
                        command.Parameters.AddWithValue("@Email", txtOffEmail.Text.ToUpper)
                        command.Parameters.AddWithValue("@Password", "")
                        command.Parameters.AddWithValue("@WebLevel", "")
                        'command.Parameters.AddWithValue("@ArTERM", ddlTerms.SelectedItem.Text)
                        command.Parameters.AddWithValue("@APTERM", "")
                        command.Parameters.AddWithValue("@PriceGroup", "")
                        If ddlIncharge.Text = txtDDLText.Text Then
                            command.Parameters.AddWithValue("@InChargeID", "")

                        Else
                            command.Parameters.AddWithValue("@InChargeID", ddlIncharge.Text.ToUpper)

                        End If

                        command.Parameters.AddWithValue("@Age0", 0)
                        command.Parameters.AddWithValue("@Age30", 0)
                        command.Parameters.AddWithValue("@Age60", 0)
                        command.Parameters.AddWithValue("@Age90", 0)
                        command.Parameters.AddWithValue("@Age120", 0)
                        If ddlSalesMan.Text = txtDDLText.Text Then
                            command.Parameters.AddWithValue("@SalesMan", "")
                        Else
                            command.Parameters.AddWithValue("@SalesMan", ddlSalesMan.Text.ToUpper)
                        End If
                        command.Parameters.AddWithValue("@StopSalesYN", "")
                        command.Parameters.AddWithValue("@StopPurchYN", "")
                        command.Parameters.AddWithValue("@SpecCode", txtSpecCode.Text.ToUpper)
                        command.Parameters.AddWithValue("@ArWarning", 0)
                        If txtStartDate.Text = "" Then
                            command.Parameters.AddWithValue("@StartDate", DBNull.Value)
                        Else
                            command.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(txtStartDate.Text).ToString("yyyy-MM-dd"))
                        End If
                        command.Parameters.AddWithValue("@LicenseNumber", 0)
                        command.Parameters.AddWithValue("@LicenseInfo", "")
                        command.Parameters.AddWithValue("@SalesGST", "")
                        command.Parameters.AddWithValue("@ArMethod", "")
                        command.Parameters.AddWithValue("@ApMethod", "")
                        command.Parameters.AddWithValue("@ProductM1", "")
                        command.Parameters.AddWithValue("@ProductM2", "")
                        command.Parameters.AddWithValue("@ProductM3", "")
                        command.Parameters.AddWithValue("@ProductM4", "")
                        command.Parameters.AddWithValue("@ProductF1", "")
                        command.Parameters.AddWithValue("@ProductF2", "")
                        command.Parameters.AddWithValue("@ProductF3", "")
                        command.Parameters.AddWithValue("@ProductF4", "")
                        command.Parameters.AddWithValue("@RentalTerm", "")
                        If ddlCompanyGrp.Text = txtDDLText.Text Then
                            command.Parameters.AddWithValue("@CompanyGroup", "")
                        Else
                            command.Parameters.AddWithValue("@CompanyGroup", ddlCompanyGrp.Text.ToUpper)
                        End If

                        command.Parameters.AddWithValue("@Donor", 0)
                        command.Parameters.AddWithValue("@Member", 0)
                        command.Parameters.AddWithValue("@MemberType", "")
                        command.Parameters.AddWithValue("@MemberID", "")
                        command.Parameters.AddWithValue("@GIROID", "")
                        command.Parameters.AddWithValue("@DateJoin", DBNull.Value)
                        command.Parameters.AddWithValue("@DateExpired", DBNull.Value)
                        command.Parameters.AddWithValue("@DateTerminate", DBNull.Value)
                        command.Parameters.AddWithValue("@TemplateNo", "")
                        command.Parameters.AddWithValue("@ARLedger", "")
                        command.Parameters.AddWithValue("@ARSubLedger", "")
                        command.Parameters.AddWithValue("@APLedger", "")
                        command.Parameters.AddWithValue("@APSubLedger", "")
                        command.Parameters.AddWithValue("@SrcCompID", "")
                        command.Parameters.AddWithValue("@DiscountPct", 0)
                        command.Parameters.AddWithValue("@PreferredCustYN", "")
                        command.Parameters.AddWithValue("@ChkGstInclusive", "")
                        command.Parameters.AddWithValue("@Reason", "")
                        command.Parameters.AddWithValue("@Boardmember", "")
                        command.Parameters.AddWithValue("@BoardDesignation", "")
                        command.Parameters.AddWithValue("@period", "")
                        command.Parameters.AddWithValue("@Intriducer", "")
                        command.Parameters.AddWithValue("@Organization", "")
                        command.Parameters.AddWithValue("@chkLetterIndemnity", 0)
                        command.Parameters.AddWithValue("@LetterIndemnitySignedBy", "")
                        command.Parameters.AddWithValue("@LeterDate", DBNull.Value)
                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))

                        command.Parameters.AddWithValue("@BillTelephone", txtBillCP1Tel.Text.ToUpper)
                        command.Parameters.AddWithValue("@BillFax", txtBillCP1Fax.Text.ToUpper)
                        command.Parameters.AddWithValue("@Name2", txtNameO.Text.ToUpper)
                        command.Parameters.AddWithValue("@WebLoginID", "")
                        command.Parameters.AddWithValue("@WebLoginPassWord", "")
                        command.Parameters.AddWithValue("@WebAccessLevel", 0)
                        command.Parameters.AddWithValue("@WebOneTimePassWord", "")

                        command.Parameters.AddWithValue("@BillContactPerson", txtBillCP1Contact.Text.ToUpper)
                        command.Parameters.AddWithValue("@WebGroupDealer", 0)
                        command.Parameters.AddWithValue("@WebDisable", 0)
                        command.Parameters.AddWithValue("@WebID", "")
                        command.Parameters.AddWithValue("@OTPMobile", "")
                        command.Parameters.AddWithValue("@OTPYN", 0)
                        command.Parameters.AddWithValue("@OTPGenerateDate", DBNull.Value)
                        command.Parameters.AddWithValue("@HideInStock", 0)
                        command.Parameters.AddWithValue("@OverdueDaysLimit", 0)
                        command.Parameters.AddWithValue("@OverdueDaysLimitActive", 0)
                        command.Parameters.AddWithValue("@OverdueDaysWarning", 0)
                        command.Parameters.AddWithValue("@OverdueDaysWarningActive", 0)
                        command.Parameters.AddWithValue("@chkAR", 0)
                        command.Parameters.AddWithValue("@DueDaysStopFreq", "")
                        command.Parameters.AddWithValue("@SubCompanyNo", "")
                        command.Parameters.AddWithValue("@SourceCompany", "")
                        command.Parameters.AddWithValue("@chkSendServiceReport", 0)
                        command.Parameters.AddWithValue("@Telephone2", txtOffContact2.Text.ToUpper)
                        command.Parameters.AddWithValue("@BillTelephone2", txtBillCP1Tel2.Text.ToUpper)
                        command.Parameters.AddWithValue("@Mobile", txtOffMobile.Text.ToUpper)
                        command.Parameters.AddWithValue("@BillMobile", txtBillCP1Mobile.Text.ToUpper)
                        command.Parameters.AddWithValue("@SoPriceGroup", "")
                        command.Parameters.AddWithValue("@POPrefix", "")
                        command.Parameters.AddWithValue("@PONumber", 0)
                        command.Parameters.AddWithValue("@LastStatus", "")
                        command.Parameters.AddWithValue("@OverdueMonthWarning", 0)
                        command.Parameters.AddWithValue("@OverDueMonthLimit", 0)
                        command.Parameters.AddWithValue("@AccountNo", "")
                        command.Parameters.AddWithValue("@FlowFrom", "")
                        command.Parameters.AddWithValue("@FlowTo", "")
                        If chkInactive.Checked = True Then
                            command.Parameters.AddWithValue("@InActive", 1)

                        Else
                            command.Parameters.AddWithValue("@InActive", 0)

                        End If
                        command.Parameters.AddWithValue("@ShippingTerm", "")
                        command.Parameters.AddWithValue("@InterCompany", 0)
                        command.Parameters.AddWithValue("@AutoEmailServ", 0)
                        command.Parameters.AddWithValue("@ReportFormatServ", "")
                        command.Parameters.AddWithValue("@WebUploadDate", DBNull.Value)
                        command.Parameters.AddWithValue("@IsCustomer", 0)

                        command.Parameters.AddWithValue("@IsSupplier", 0)

                        command.Parameters.AddWithValue("@PaxBased", 0)
                        command.Parameters.AddWithValue("@BillMonthly", 0)
                        command.Parameters.AddWithValue("@DiscType", "")
                        command.Parameters.AddWithValue("@ARPDFFromat", "")
                        command.Parameters.AddWithValue("@EmailConsolidate", 0)
                        command.Parameters.AddWithValue("@OffContact1Position", txtOffCont1Position.Text.ToUpper)
                        command.Parameters.AddWithValue("@OffContact1Tel", txtOffCont1Tel.Text.ToUpper)
                        command.Parameters.AddWithValue("@OffContact1Fax", txtOffCont1Fax.Text.ToUpper)
                        command.Parameters.AddWithValue("@OffContact1Tel2", txtOffCont1Tel2.Text.ToUpper)
                        command.Parameters.AddWithValue("@OffContact1Mobile", txtOffCont1Mobile.Text.ToUpper)
                        command.Parameters.AddWithValue("@BillContact1Position", txtBillCP1Position.Text.ToUpper)
                        command.Parameters.AddWithValue("@BillContact2", txtBillCP2Contact.Text.ToUpper)
                        command.Parameters.AddWithValue("@BillContact2Position", txtBillCP2Position.Text.ToUpper)
                        command.Parameters.AddWithValue("@BillContact1Email", txtBillCP1Email.Text.ToUpper)
                        command.Parameters.AddWithValue("@BillContact2Email", txtBillCP2Email.Text.ToUpper)
                        command.Parameters.AddWithValue("@BillContact2Tel", txtBillCP2Tel.Text.ToUpper)
                        command.Parameters.AddWithValue("@BillContact2Fax", txtBillCP2Fax.Text.ToUpper)
                        command.Parameters.AddWithValue("@BillContact2Tel2", txtBillCP2Tel2.Text.ToUpper)
                        command.Parameters.AddWithValue("@BillContact2Mobile", txtBillCP2Mobile.Text.ToUpper)

                        command.Parameters.AddWithValue("@OffContact1", txtOffCont1Name.Text.ToUpper)
                        command.Parameters.AddWithValue("@OffContactPosition", txtOffPosition.Text.ToUpper)
                        command.Parameters.AddWithValue("@BillingSettings", rdbBillingSettings.SelectedValue.ToString)
                        command.Parameters.AddWithValue("@BillingName", txtBillingName.Text.ToUpper)
                        command.Parameters.AddWithValue("@Terms", ddlTerms.SelectedItem.Text.ToUpper)

                        'command.Parameters.AddWithValue("@TermsDay", ddlTerms.SelectedValue.ToString)
                        command.Parameters.AddWithValue("@TermsDay", 0)
                        command.Parameters.AddWithValue("@Currency", ddlCurrency.SelectedItem.Text.ToUpper)
                        'command.Parameters.AddWithValue("@Location", ddlLocation.Text.ToUpper)

                        If ddlLocation.SelectedIndex = 0 Then
                            command.Parameters.AddWithValue("@Location", "")
                        Else
                            command.Parameters.AddWithValue("@Location", ddlLocation.Text.ToUpper)
                        End If

                        If ddlDefaultInvoiceFormat.SelectedIndex = 0 Then
                            command.Parameters.AddWithValue("@DefaultInvoiceFormat", "")
                        Else
                            command.Parameters.AddWithValue("@DefaultInvoiceFormat", ddlDefaultInvoiceFormat.Text)
                        End If
                        command.Parameters.AddWithValue("@BillingOptionRemarks", txtBillingOptionRemarks.Text.ToUpper)

                        command.Parameters.AddWithValue("@AutoEmailInvoice", chkAutoEmailInvoice.Checked)
                        command.Parameters.AddWithValue("@AutoEmailSOA", chkAutoEmailStatement.Checked)
                        command.Parameters.AddWithValue("@RequireEBilling", chkRequireEBilling.Checked)

                        command.Parameters.AddWithValue("@TaxIdentificationNo", txtTIN.Text.ToUpper)
                        command.Parameters.AddWithValue("@SalesTaxRegistrationNo", txtSST.Text.ToUpper)


                        command.Connection = conn

                        command.ExecuteNonQuery()

                        UpdateContactMaster(conn)

                        '  MessageBox.Message.Alert(Page, "Record updated successfully!!!", "str")
                        lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED"
                        lblAlert.Text = ""

                        If String.IsNullOrEmpty(txtTINOld.Text) Then
                            If String.IsNullOrEmpty(txtTIN.Text) = False Then
                                TINEditUpdateSales()

                            End If
                        End If

                        txtTINOld.Text = txtTIN.Text
                        CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CORP", txtAccountID.Text, "EDIT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")), 0, 0, 0, txtAccountID.Text, "", txtRcno.Text)

                    End If
                End If

                conn.Close()
                conn.Dispose()
                command2.Dispose()
                dt1.Dispose()
                dr1.Close()

            Catch ex As Exception
                InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "EDIT SAVE", ex.Message.ToString, txtAccountID.Text)
            End Try
            EnableControls()

        End If


        'txt.Text = "SELECT * FROM tblcompany WHERE  Inactive=0 order by rcno desc limit 100;"
        'SqlDataSource1.SelectCommand = "SELECT * FROM tblcompany WHERE  Inactive=0 order by rcno desc limit 100;"
        SqlDataSource1.SelectCommand = txt.Text
        SqlDataSource1.DataBind()
        GridView1.DataBind()


        btnCopyAdd.Enabled = True
        btnCopyAdd.ForeColor = System.Drawing.Color.Black

        btnSearchTIN.Enabled = False
        btnSearchTIN.ForeColor = System.Drawing.Color.Gray


        '     GridView1.DataSourceID = "SqlDataSource1"
        '   MakeMeNull()
        txtMode.Text = ""
        txtSvcMode.Text = ""

        EnableSvcControls()

        txtSearchCust.Enabled = True
        btnGoCust.Enabled = True
        btnResetSearch.Enabled = True

        'InsertNewLog()

        'btnSave.Enabled = False
        'btnSave.ForeColor = System.Drawing.Color.Gray
        'btnCancel.Enabled = False
        'btnCancel.ForeColor = System.Drawing.Color.Gray

    End Sub

    'Private Sub InsertNewLog()

    '    Try

    '        ''
    '        Dim conn As MySqlConnection = New MySqlConnection()
    '        Dim command As MySqlCommand = New MySqlCommand

    '        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    '        conn.Open()

    '        command.CommandType = CommandType.Text
    '        command.CommandText = "SELECT EnableLogforCustomer FROM tblservicerecordmastersetup where rcno=1"
    '        command.Connection = conn

    '        Dim dr As MySqlDataReader = command.ExecuteReader()
    '        Dim dt As New DataTable
    '        dt.Load(dr)

    '        If dt.Rows.Count > 0 Then
    '            'If Convert.ToBoolean(dt.Rows(0)("EnableLogforCustomer")) = False Then
    '            If dt.Rows(0)("EnableLogforCustomer").ToString = "1" Then


    '                ' Start: Insert NEW Log table
    '                Dim connLog As MySqlConnection = New MySqlConnection()

    '                connLog.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataLogConnectionString").ConnectionString
    '                If connLog.State = ConnectionState.Open Then
    '                    connLog.Close()
    '                    connLog.Dispose()
    '                End If
    '                connLog.Open()

    '                Dim commandInsertLog As MySqlCommand = New MySqlCommand
    '                commandInsertLog.CommandType = CommandType.StoredProcedure
    '                commandInsertLog.CommandText = "InsertLog_sitadatadb"

    '                commandInsertLog.Parameters.Clear()

    '                commandInsertLog.Parameters.AddWithValue("@pr_ModuleType", "Corporate")
    '                commandInsertLog.Parameters.AddWithValue("@pr_KeyValue", txtAccountID.Text.Trim)

    '                commandInsertLog.Connection = connLog
    '                commandInsertLog.ExecuteScalar()

    '                connLog.Close()
    '                commandInsertLog.Dispose()

    '                ''''

    '            End If
    '            'End If

    '        End If
    '        conn.Close()
    '        conn.Dispose()
    '        command.Dispose()
    '        dt.Dispose()
    '        dr.Close()

    '        ''
    '        ' End: Insert NEW Log table
    '    Catch ex As Exception
    '        lblAlert.Text = ex.Message.ToString
    '        InsertIntoTblWebEventLog("COMPANY - " + Session("UserID"), "FUNCTION InsertNewLog", ex.Message.ToString, txtAccountID.Text)
    '        'MessageBox.Message.Alert(Page, ex.Message.ToString, "str")
    '    End Try
    'End Sub

    'Private Sub InsertNewLogDetail()

    '    Try

    '        ' Start: Insert NEW Log table

    '        ''
    '        Dim conn As MySqlConnection = New MySqlConnection()
    '        Dim command As MySqlCommand = New MySqlCommand

    '        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    '        conn.Open()

    '        command.CommandType = CommandType.Text
    '        command.CommandText = "SELECT EnableLogforCustomer FROM tblservicerecordmastersetup where rcno=1"
    '        command.Connection = conn

    '        Dim dr As MySqlDataReader = command.ExecuteReader()
    '        Dim dt As New DataTable
    '        dt.Load(dr)

    '        If dt.Rows.Count > 0 Then
    '            'If Convert.ToBoolean(dt.Rows(0)("EnableLogforCustomer")) = False Then
    '            If dt.Rows(0)("EnableLogforCustomer").ToString = "1" Then

    '                Dim connLog As MySqlConnection = New MySqlConnection()

    '                connLog.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataLogConnectionString").ConnectionString
    '                If connLog.State = ConnectionState.Open Then
    '                    connLog.Close()
    '                    connLog.Dispose()
    '                End If
    '                connLog.Open()

    '                Dim commandInsertLog As MySqlCommand = New MySqlCommand
    '                commandInsertLog.CommandType = CommandType.StoredProcedure
    '                commandInsertLog.CommandText = "InsertLogDetail_sitadatadb"

    '                commandInsertLog.Parameters.Clear()

    '                commandInsertLog.Parameters.AddWithValue("@pr_ModuleType", "Corporate")
    '                commandInsertLog.Parameters.AddWithValue("@pr_KeyValue", txtAccountID.Text.Trim)
    '                commandInsertLog.Parameters.AddWithValue("@pr_KeyValueDetail", txtLocationID.Text.Trim)

    '                commandInsertLog.Connection = connLog
    '                commandInsertLog.ExecuteScalar()

    '                connLog.Close()
    '                commandInsertLog.Dispose()

    '                ''''

    '            End If
    '            'End If

    '        End If
    '        conn.Close()
    '        conn.Dispose()
    '        command.Dispose()
    '        dt.Dispose()
    '        dr.Close()
    '        ' End: Insert NEW Log table
    '    Catch ex As Exception
    '        lblAlert.Text = ex.Message.ToString
    '        InsertIntoTblWebEventLog("COMPANY - " + Session("UserID"), "FUNCTION InsertLogDetail", ex.Message.ToString, txtAccountID.Text)
    '        'MessageBox.Message.Alert(Page, ex.Message.ToString, "str")
    '    End Try
    'End Sub
    Protected Sub btnQuit_Click(sender As Object, e As EventArgs) Handles btnQuit.Click
        Response.Redirect("Home.aspx")

    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        lblMessage.Text = ""
        If txtRcno.Text = "" Then
            ' MessageBox.Message.Alert(Page, "Select a record to delete!!!", "str")
            lblAlert.Text = "SELECT RECORD TO DELETE"
            Return

        End If
        lblMessage.Text = "ACTION: DELETE RECORD"

        Dim confirmValue As String = Request.Form("confirm_value")
        If confirmValue = "Yes" Then


            ' MessageBox.Message.Alert(Page, txtRcno.Text, "str")
            System.Threading.Thread.Sleep(5000)
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                command1.CommandText = "SELECT * FROM tblcompany where rcno=" & Convert.ToInt32(txtRcno.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New System.Data.DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    '   Dim qry As String = "delete from tblcompany where rcno=" & Convert.ToInt32(txtRcno.Text)
                    Dim qry As String = "UPDATE tblcompany SET Inactive=1,Status = 'T' where rcno=" & Convert.ToInt32(txtRcno.Text)
                    command.CommandText = qry

                    command.Connection = conn

                    command.ExecuteNonQuery()

                    'DeleteContactMaster(conn)

                    '  MessageBox.Message.Alert(Page, "Record updated successfully!!!", "str")
                    lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CORP", txtAccountID.Text, "DELETE", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtAccountID.Text, "", txtRcno.Text)

                End If
                conn.Close()
                conn.Dispose()
                command1.Dispose()
            Catch ex As Exception
                InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "DELETE", ex.Message.ToString, txtAccountID.Text)
            End Try
            EnableControls()


            GridView1.DataSourceID = "SqlDataSource1"
            MakeMeNull()
            lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"
            ddlStatus.SelectedIndex = 0
        End If

    End Sub


    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            MakeMeNull()
            EnableControls()

            '   txt.Text = "select * from tblasset where rcno<>0;"
            'SqlDataSource1.SelectCommand = "SELECT * FROM tblcompany WHERE  Inactive=0 order by AccountId desc limit 100;"
            SqlDataSource1.DataBind()
            GridView1.DataBind()

            txtSearchCust.Enabled = True
            btnGoCust.Enabled = True
            btnResetSearch.Enabled = True
            btnSearchTIN.Enabled = False
            btnSearchTIN.ForeColor = System.Drawing.Color.Gray

        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "btnCancel_Click", ex.Message.ToString, "")
        End Try
    End Sub

    Protected Sub chkSameAddr_CheckedChanged(sender As Object, e As EventArgs) Handles chkSameAddr.CheckedChanged
        If String.IsNullOrEmpty(txtPostal.Text.Trim) = False Then
            txtPostal_TextChanged(sender, e)
        End If

    End Sub

    Protected Sub btnCopyAdd_Click(sender As Object, e As EventArgs) Handles btnCopyAdd.Click
        Try
            'txtRcno.Text = ""
            'txtID.Text = ""
            'txtNameE.Text = ""
            'txtAcctCode.Text = ""
            'DisableControls()

            'txtMode.Text = "NEW"
            'txtID.Focus()
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "currentdatetimestartdate();", True)
            lblMessage.Text = ""
            If txtRcno.Text = "" Then
                '  MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
                lblAlert.Text = "SELECT RECORD TO EDIT"
                Return
            End If
            DisableControls()
            txtMode.Text = "EDIT"
            lblMessage.Text = "ACTION: EDIT RECORD"
            tb1.ActiveTabIndex = 0
            txtCreatedOn.Text = ""
            txtSearchCust.Enabled = False
            btnGoCust.Enabled = False
            btnResetSearch.Enabled = False

            btnSearchTIN.Enabled = True
            btnSearchTIN.ForeColor = System.Drawing.Color.Black


            'btnSave.Enabled = True
            'btnSave.ForeColor = System.Drawing.Color.Black
            'btnCancel.Enabled = True
            'btnCancel.ForeColor = System.Drawing.Color.Black
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "btnCopyAdd_Click", ex.Message.ToString, "")
        End Try
    End Sub


    Private Sub InsertContactMaster(conn As MySqlConnection)
        Try

            Dim command As MySqlCommand = New MySqlCommand

            command.CommandType = CommandType.Text
            Dim qry As String = "INSERT INTO tblcontactmaster(ContType,ContID,ContName,ContRegID,ContRegDate,ContGSTYN,ContGSTNO,ContPerson,ContLocationGroup,ContSalesGroup,ContGroup,ContSales,ContInCharge,ContTel,ContFax,ContHP,ContEmail,ContAddBlock,ContAddNos,ContAddFloor,ContAddUnit,ContAddress1,ContAddStreet,ContAddBuilding,ContAddPostal,ContAddState,ContAddCity,ContAddCountry,ContBillTel,ContBILLFax,ContBILLHP,ContBILLEmail,ContBILLBlock,ContBILLNos,ContBILLFloor,ContBILLUnit,ContBILLAddress1,ContBILLStreet,ContBILLBuilding,ContBILLPostal,ContBILLState,ContBILLCity,ContBILLCountry,CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn,Dealer,Name2,WebLoginID,WebLoginPassWord,WebAccessLevel,WebOneTimePassWord,WebLevel,DiscountPct,LoginID,Password,PriceGroup,SubCompanyNo,SalesGRP,LocateGRP,Telephone2,BillTelephone2,Mobile,BillMobile,SoPriceGroup,AccountNo,InActive,ContARTerm,ContAPTerm,ContRentalTerm,ContShippingTerm,ContApCurrency,ContArCurrency,Industry,InterCompany,CreateDeviceID,CreateSource,FlowFrom,FlowTo,EditSource,DeleteStatus,LastEditDevice,AutoEmailServ,ReportFormatServ,Email,WebUploadDate,IsCustomer,IsSupplier,AccountID,OffContact1Position,OffContact1Tel,OffContact1Fax,OffContact1Tel2,OffContact1Mobile,BillContact1Position,BillContact1Email,BillContact2,BillContact2Position,BillContact2Tel,BillContact2Fax,BillContact2Tel2,BillContact2Mobile,BillContact2Email,OffContact1,OffContactPosition) VALUES(@ContType,@ContID,@ContName,@ContRegID,@ContRegDate,@ContGSTYN,@ContGSTNO,@ContPerson,@ContLocationGroup,@ContSalesGroup,@ContGroup,@ContSales,@ContInCharge,@ContTel,@ContFax,@ContHP,@ContEmail,@ContAddBlock,@ContAddNos,@ContAddFloor,@ContAddUnit,@ContAddress1,@ContAddStreet,@ContAddBuilding,@ContAddPostal,@ContAddState,@ContAddCity,@ContAddCountry,@ContBillTel,@ContBILLFax,@ContBILLHP,@ContBILLEmail,@ContBILLBlock,@ContBILLNos,@ContBILLFloor,@ContBILLUnit,@ContBILLAddress1,@ContBILLStreet,@ContBILLBuilding,@ContBILLPostal,@ContBILLState,@ContBILLCity,@ContBILLCountry,@CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn,@Dealer,@Name2,@WebLoginID,@WebLoginPassWord,@WebAccessLevel,@WebOneTimePassWord,@WebLevel,@DiscountPct,@LoginID,@Password,@PriceGroup,@SubCompanyNo,@SalesGRP,@LocateGRP,@Telephone2,@BillTelephone2,@Mobile,@BillMobile,@SoPriceGroup,@AccountNo,@InActive,@ContARTerm,@ContAPTerm,@ContRentalTerm,@ContShippingTerm,@ContApCurrency,@ContArCurrency,@Industry,@InterCompany,@CreateDeviceID,@CreateSource,@FlowFrom,@FlowTo,@EditSource,@DeleteStatus,@LastEditDevice,@AutoEmailServ,@ReportFormatServ,@Email,@WebUploadDate,@IsCustomer,@IsSupplier,@AccountID,@OffContact1Position,@OffContact1Tel,@OffContact1Fax,@OffContact1Tel2,@OffContact1Mobile,@BillContact1Position,@BillContact1Email,@BillContact2,@BillContact2Position,@BillContact2Tel,@BillContact2Fax,@BillContact2Tel2,@BillContact2Mobile,@BillContact2Email,@OffContact1,@OffContactPosition);"
            command.CommandText = qry
            command.Parameters.Clear()

            command.Parameters.AddWithValue("@ContType", "COMPANY")
            command.Parameters.AddWithValue("@ContID", "")
            command.Parameters.AddWithValue("@ContName", txtNameE.Text)
            command.Parameters.AddWithValue("@ContRegID", "")

            command.Parameters.AddWithValue("@contRegDate", DBNull.Value)

            command.Parameters.AddWithValue("@ContGSTYN", 0)
            command.Parameters.AddWithValue("@ContGSTNO", txtGSTRegNo.Text)

            command.Parameters.AddWithValue("@ContPerson", txtOffContactPerson.Text)

            command.Parameters.AddWithValue("@ContLocationGroup", "")

            '  If ddlSalesGrp.Text = txtDDLText.Text Then
            command.Parameters.AddWithValue("@ContSalesGroup", "")
            'Else
            'command.Parameters.AddWithValue("@ContSalesGroup", ddlSalesGrp.Text)
            'End If
            If ddlCompanyGrp.Text = txtDDLText.Text Then
                command.Parameters.AddWithValue("@ContGroup", "")
            Else
                command.Parameters.AddWithValue("@ContGroup", ddlCompanyGrp.Text)
            End If
            If ddlSalesMan.Text = txtDDLText.Text Then
                command.Parameters.AddWithValue("@ContSales", "")
            Else
                command.Parameters.AddWithValue("@ContSales", ddlSalesMan.Text)
            End If
            If ddlIncharge.Text = txtDDLText.Text Then
                command.Parameters.AddWithValue("@ContInCharge", "")
            Else
                command.Parameters.AddWithValue("@ContInCharge", ddlIncharge.Text)
            End If

            command.Parameters.AddWithValue("@ContTel", txtOffContactNo.Text)
            command.Parameters.AddWithValue("@ContFax", txtOffFax.Text)
            command.Parameters.AddWithValue("@ContHP", txtOffMobile.Text)
            command.Parameters.AddWithValue("@ContEmail", txtOffCont1Email.Text)
            command.Parameters.AddWithValue("@ContAddBlock", "")
            command.Parameters.AddWithValue("@ContAddNos", "")
            command.Parameters.AddWithValue("@ContAddFloor", "")
            command.Parameters.AddWithValue("@ContAddUnit", "")
            command.Parameters.AddWithValue("@ContAddress1", txtOffAddress1.Text)
            command.Parameters.AddWithValue("@ContAddStreet", txtOffStreet.Text)
            command.Parameters.AddWithValue("@ContAddBuilding", txtOffBuilding.Text)
            command.Parameters.AddWithValue("@ContAddPostal", txtOffPostal.Text)
            command.Parameters.AddWithValue("@ContAddState", ddlOffState.Text)
            If ddlOffCity.Text = txtDDLText.Text Then
                command.Parameters.AddWithValue("@ContAddCity", "")
            Else
                command.Parameters.AddWithValue("@ContAddCity", ddlOffCity.Text)
            End If
            If ddlOffCountry.Text = txtDDLText.Text Then
                command.Parameters.AddWithValue("@ContAddCountry", "")
            Else
                command.Parameters.AddWithValue("@ContAddCountry", ddlOffCountry.Text)
            End If
            command.Parameters.AddWithValue("@ContBillTel", txtBillCP1Tel.Text)
            command.Parameters.AddWithValue("@ContBILLFax", txtBillCP1Fax.Text)
            command.Parameters.AddWithValue("@ContBILLHP", txtBillCP1Mobile.Text)
            command.Parameters.AddWithValue("@ContBILLEmail", txtBillCP1Email.Text)
            command.Parameters.AddWithValue("@ContBILLBlock", "")
            command.Parameters.AddWithValue("@ContBILLNos", "")
            command.Parameters.AddWithValue("@ContBILLFloor", "")
            command.Parameters.AddWithValue("@ContBILLUnit", "")
            command.Parameters.AddWithValue("@ContBILLAddress1", txtBillAddress.Text)
            command.Parameters.AddWithValue("@ContBILLStreet", txtBillStreet.Text)
            command.Parameters.AddWithValue("@ContBILLBuilding", txtBillBuilding.Text)
            command.Parameters.AddWithValue("@ContBILLPostal", txtBillPostal.Text)
            command.Parameters.AddWithValue("@ContBILLState", ddlBillState.Text)
            If ddlBillCity.Text = txtDDLText.Text Then
                command.Parameters.AddWithValue("@ContBILLCity", "")
            Else
                command.Parameters.AddWithValue("@ContBILLCity", ddlBillCity.Text)
            End If
            If ddlBillCountry.Text = txtDDLText.Text Then
                command.Parameters.AddWithValue("@ContBILLCountry", "")
            Else
                command.Parameters.AddWithValue("@ContBILLCountry", ddlBillCountry.Text)
            End If
            command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
            command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
            command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
            command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

            command.Parameters.AddWithValue("@Dealer", "")
            command.Parameters.AddWithValue("@Name2", txtNameO.Text)
            command.Parameters.AddWithValue("@WebLoginID", "")
            command.Parameters.AddWithValue("@WebLoginPassWord", "")
            command.Parameters.AddWithValue("@WebAccessLevel", 0)
            command.Parameters.AddWithValue("@WebOneTimePassWord", "")
            command.Parameters.AddWithValue("@WebLevel", "")
            command.Parameters.AddWithValue("@DiscountPct", 0)
            command.Parameters.AddWithValue("@LoginID", "")
            command.Parameters.AddWithValue("@Password", "")
            command.Parameters.AddWithValue("@PriceGroup", "")
            command.Parameters.AddWithValue("@SubCompanyNo", "")
            command.Parameters.AddWithValue("@SalesGRP", "")
            command.Parameters.AddWithValue("@LocateGRP", "")
            command.Parameters.AddWithValue("@Telephone2", txtOffContact2.Text)
            command.Parameters.AddWithValue("@BillTelephone2", txtBillCP1Tel2.Text)
            command.Parameters.AddWithValue("@Mobile", txtOffCont1Mobile.Text)
            command.Parameters.AddWithValue("@BillMobile", txtBillCP1Mobile.Text)
            command.Parameters.AddWithValue("@SoPriceGroup", "")
            command.Parameters.AddWithValue("@AccountNo", "")
            If chkInactive.Checked = True Then
                command.Parameters.AddWithValue("@InActive", 1)

            Else
                command.Parameters.AddWithValue("@InActive", 0)

            End If
            command.Parameters.AddWithValue("@ContARTerm", "")
            command.Parameters.AddWithValue("@ContAPTerm", "")
            command.Parameters.AddWithValue("@ContRentalTerm", "")
            command.Parameters.AddWithValue("@ContShippingTerm", "")
            command.Parameters.AddWithValue("@ContApCurrency", "")
            command.Parameters.AddWithValue("@ContArCurrency", "")
            If ddlIndustry.Text = txtDDLText.Text Then
                command.Parameters.AddWithValue("@Industry", "")
            Else
                command.Parameters.AddWithValue("@Industry", ddlIndustry.Text)
            End If

            command.Parameters.AddWithValue("@InterCompany", 0)
            command.Parameters.AddWithValue("@CreateDeviceID", "")
            command.Parameters.AddWithValue("@CreateSource", "")
            command.Parameters.AddWithValue("@FlowFrom", "")
            command.Parameters.AddWithValue("@FlowTo", "")
            command.Parameters.AddWithValue("@EditSource", "")
            command.Parameters.AddWithValue("@DeleteStatus", "")
            command.Parameters.AddWithValue("@LastEditDevice", "")
            command.Parameters.AddWithValue("@AutoEmailServ", 0)
            command.Parameters.AddWithValue("@ReportFormatServ", "")
            command.Parameters.AddWithValue("@Email", txtOffEmail.Text)
            command.Parameters.AddWithValue("@WebUploadDate", DBNull.Value)

            command.Parameters.AddWithValue("@IsCustomer", 0)

            command.Parameters.AddWithValue("@IsSupplier", 0)
            command.Parameters.AddWithValue("@AccountID", txtAccountID.Text)
            command.Parameters.AddWithValue("@OffContact1Position", txtOffCont1Position.Text)
            command.Parameters.AddWithValue("@OffContact1Tel", txtOffCont1Tel.Text)
            command.Parameters.AddWithValue("@OffContact1Fax", txtOffCont1Fax.Text)
            command.Parameters.AddWithValue("@OffContact1Tel2", txtOffCont1Tel2.Text)
            command.Parameters.AddWithValue("@OffContact1Mobile", txtOffCont1Mobile.Text)
            command.Parameters.AddWithValue("@BillContact1Position", txtBillCP1Position.Text)
            command.Parameters.AddWithValue("@BillContact2", txtBillCP2Contact.Text)
            command.Parameters.AddWithValue("@BillContact2Position", txtBillCP2Position.Text)
            command.Parameters.AddWithValue("@BillContact1Email", txtBillCP1Email.Text)
            command.Parameters.AddWithValue("@BillContact2Email", txtBillCP2Email.Text)
            command.Parameters.AddWithValue("@BillContact2Tel", txtBillCP2Tel.Text)
            command.Parameters.AddWithValue("@BillContact2Fax", txtBillCP2Fax.Text)
            command.Parameters.AddWithValue("@BillContact2Tel2", txtBillCP2Tel2.Text)
            command.Parameters.AddWithValue("@BillContact2Mobile", txtBillCP2Mobile.Text)
            command.Parameters.AddWithValue("@OffContact1", txtOffCont1Name.Text)
            command.Parameters.AddWithValue("@OffContactPosition", txtOffPosition.Text)
            ' command.Parameters.AddWithValue("@OffContactMobile", txtOffMobile.Text)
            command.Parameters.AddWithValue("@BillContactPerson", txtBillCP1Contact.Text)

            command.Connection = conn

            command.ExecuteNonQuery()


        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "INSERT CONTACT MASTER", ex.Message.ToString, txtAccountID.Text)
        End Try

    End Sub

    Private Sub UpdateContactMaster(conn As MySqlConnection)
        Try

            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            If String.IsNullOrEmpty(txtAccountID.Text) Then
                command1.CommandText = "SELECT * FROM tblcontactmaster where ContRegID=@id and conttype='COMPANY'"
                command1.Parameters.AddWithValue("@id", txtClientID.Text)
            Else
                command1.CommandText = "SELECT * FROM tblcontactmaster where ACCOUNTID=@id and conttype='COMPANY'"
                command1.Parameters.AddWithValue("@id", txtAccountID.Text)
            End If

            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New System.Data.DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then

                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                command.Parameters.Clear()
                Dim qry As String
                If String.IsNullOrEmpty(txtAccountID.Text) Then
                    qry = "UPDATE tblcontactmaster SET ContName = @ContName,ContRegID = @ContRegID,ContRegDate = @ContRegDate,ContGSTYN = @ContGSTYN,ContGSTNO = @ContGSTNO,ContPerson = @ContPerson,ContLocationGroup = @ContLocationGroup,ContSalesGroup = @ContSalesGroup,ContGroup = @ContGroup,ContSales = @ContSales,ContInCharge = @ContInCharge,ContTel = @ContTel,ContFax = @ContFax,ContHP = @ContHP,ContEmail = @ContEmail,ContAddBlock = @ContAddBlock,ContAddNos = @ContAddNos,ContAddFloor = @ContAddFloor,ContAddUnit = @ContAddUnit,ContAddress1 = @ContAddress1,ContAddStreet = @ContAddStreet,ContAddBuilding = @ContAddBuilding,ContAddPostal = @ContAddPostal,ContAddState = @ContAddState,ContAddCity = @ContAddCity,ContAddCountry = @ContAddCountry,ContBillTel = @ContBillTel,ContBILLFax = @ContBILLFax,ContBILLHP = @ContBILLHP,ContBILLEmail = @ContBILLEmail,ContBILLBlock = @ContBILLBlock,ContBILLNos = @ContBILLNos,ContBILLFloor = @ContBILLFloor,ContBILLUnit = @ContBILLUnit,ContBILLAddress1 = @ContBILLAddress1,ContBILLStreet = @ContBILLStreet,ContBILLBuilding = @ContBILLBuilding,ContBILLPostal = @ContBILLPostal,ContBILLState = @ContBILLState,ContBILLCity = @ContBILLCity,ContBILLCountry = @ContBILLCountry,LastModifiedBy = @LastModifiedBy,LastModifiedOn = @LastModifiedOn,Dealer = @Dealer,Name2 = @Name2,WebLoginID = @WebLoginID,WebLoginPassWord = @WebLoginPassWord,WebAccessLevel = @WebAccessLevel,WebOneTimePassWord = @WebOneTimePassWord,WebLevel = @WebLevel,DiscountPct = @DiscountPct,LoginID = @LoginID,Password = @Password,PriceGroup = @PriceGroup,SubCompanyNo = @SubCompanyNo,SalesGRP = @SalesGRP,LocateGRP = @LocateGRP,Telephone2 = @Telephone2,BillTelephone2 = @BillTelephone2,Mobile = @Mobile,BillMobile = @BillMobile,SoPriceGroup = @SoPriceGroup,AccountNo = @AccountNo,InActive = @InActive,ContARTerm = @ContARTerm,ContAPTerm = @ContAPTerm,ContRentalTerm = @ContRentalTerm,ContShippingTerm = @ContShippingTerm,ContApCurrency = @ContApCurrency,ContArCurrency = @ContArCurrency,Industry = @Industry,InterCompany = @InterCompany,CreateDeviceID = @CreateDeviceID,CreateSource = @CreateSource,FlowFrom = @FlowFrom,FlowTo = @FlowTo,EditSource = @EditSource,DeleteStatus = @DeleteStatus,LastEditDevice = @LastEditDevice,AutoEmailServ = @AutoEmailServ,ReportFormatServ = @ReportFormatServ,Email = @Email,WebUploadDate = @WebUploadDate,IsCustomer = @IsCustomer,IsSupplier = @IsSupplier,AccountID = @AccountID,OffContact1Position = @OffContact1Position,OffContact1Tel = @OffContact1Tel,OffContact1Fax = @OffContact1Fax,OffContact1Tel2 = @OffContact1Tel2,OffContact1Mobile = @OffContact1Mobile,BillContact1Position = @BillContact1Position,BillContact1Email = @BillContact1Email,BillContact2 = @BillContact2,BillContact2Position = @BillContact2Position,BillContact2Tel = @BillContact2Tel,BillContact2Fax = @BillContact2Fax,BillContact2Tel2 = @BillContact2Tel2,BillContact2Mobile = @BillContact2Mobile,BillContact2Email = @BillContact2Email,OffContact1=@OffContact1,OffContact1=@OffContact1 WHERE id =@id and conttype='COMPANY';"
                    command.Parameters.AddWithValue("@id", txtClientID.Text)
                Else
                    qry = "UPDATE tblcontactmaster SET ContName = @ContName,ContRegID = @ContRegID,ContRegDate = @ContRegDate,ContGSTYN = @ContGSTYN,ContGSTNO = @ContGSTNO,ContPerson = @ContPerson,ContLocationGroup = @ContLocationGroup,ContSalesGroup = @ContSalesGroup,ContGroup = @ContGroup,ContSales = @ContSales,ContInCharge = @ContInCharge,ContTel = @ContTel,ContFax = @ContFax,ContHP = @ContHP,ContEmail = @ContEmail,ContAddBlock = @ContAddBlock,ContAddNos = @ContAddNos,ContAddFloor = @ContAddFloor,ContAddUnit = @ContAddUnit,ContAddress1 = @ContAddress1,ContAddStreet = @ContAddStreet,ContAddBuilding = @ContAddBuilding,ContAddPostal = @ContAddPostal,ContAddState = @ContAddState,ContAddCity = @ContAddCity,ContAddCountry = @ContAddCountry,ContBillTel = @ContBillTel,ContBILLFax = @ContBILLFax,ContBILLHP = @ContBILLHP,ContBILLEmail = @ContBILLEmail,ContBILLBlock = @ContBILLBlock,ContBILLNos = @ContBILLNos,ContBILLFloor = @ContBILLFloor,ContBILLUnit = @ContBILLUnit,ContBILLAddress1 = @ContBILLAddress1,ContBILLStreet = @ContBILLStreet,ContBILLBuilding = @ContBILLBuilding,ContBILLPostal = @ContBILLPostal,ContBILLState = @ContBILLState,ContBILLCity = @ContBILLCity,ContBILLCountry = @ContBILLCountry,LastModifiedBy = @LastModifiedBy,LastModifiedOn = @LastModifiedOn,Dealer = @Dealer,Name2 = @Name2,WebLoginID = @WebLoginID,WebLoginPassWord = @WebLoginPassWord,WebAccessLevel = @WebAccessLevel,WebOneTimePassWord = @WebOneTimePassWord,WebLevel = @WebLevel,DiscountPct = @DiscountPct,LoginID = @LoginID,Password = @Password,PriceGroup = @PriceGroup,SubCompanyNo = @SubCompanyNo,SalesGRP = @SalesGRP,LocateGRP = @LocateGRP,Telephone2 = @Telephone2,BillTelephone2 = @BillTelephone2,Mobile = @Mobile,BillMobile = @BillMobile,SoPriceGroup = @SoPriceGroup,AccountNo = @AccountNo,InActive = @InActive,ContARTerm = @ContARTerm,ContAPTerm = @ContAPTerm,ContRentalTerm = @ContRentalTerm,ContShippingTerm = @ContShippingTerm,ContApCurrency = @ContApCurrency,ContArCurrency = @ContArCurrency,Industry = @Industry,InterCompany = @InterCompany,CreateDeviceID = @CreateDeviceID,CreateSource = @CreateSource,FlowFrom = @FlowFrom,FlowTo = @FlowTo,EditSource = @EditSource,DeleteStatus = @DeleteStatus,LastEditDevice = @LastEditDevice,AutoEmailServ = @AutoEmailServ,ReportFormatServ = @ReportFormatServ,Email = @Email,WebUploadDate = @WebUploadDate,IsCustomer = @IsCustomer,IsSupplier = @IsSupplier,AccountID = @AccountID,OffContact1Position = @OffContact1Position,OffContact1Tel = @OffContact1Tel,OffContact1Fax = @OffContact1Fax,OffContact1Tel2 = @OffContact1Tel2,OffContact1Mobile = @OffContact1Mobile,BillContact1Position = @BillContact1Position,BillContact1Email = @BillContact1Email,BillContact2 = @BillContact2,BillContact2Position = @BillContact2Position,BillContact2Tel = @BillContact2Tel,BillContact2Fax = @BillContact2Fax,BillContact2Tel2 = @BillContact2Tel2,BillContact2Mobile = @BillContact2Mobile,BillContact2Email = @BillContact2Email,OffContact1=@OffContact1,OffContact1=@OffContact1 WHERE accountid =@id and conttype='COMPANY';"
                    command.Parameters.AddWithValue("@id", txtAccountID.Text)
                End If

                command.CommandText = qry

                command.Parameters.AddWithValue("@ContType", "COMPANY")
                command.Parameters.AddWithValue("@ContID", "")
                command.Parameters.AddWithValue("@ContName", txtNameE.Text)
                command.Parameters.AddWithValue("@ContRegID", "")

                command.Parameters.AddWithValue("@contRegDate", DBNull.Value)

                command.Parameters.AddWithValue("@ContGSTYN", 0)
                command.Parameters.AddWithValue("@ContGSTNO", txtGSTRegNo.Text)

                command.Parameters.AddWithValue("@ContPerson", txtOffContactPerson.Text)

                command.Parameters.AddWithValue("@ContLocationGroup", "")

                '  If ddlSalesGrp.Text = txtDDLText.Text Then
                command.Parameters.AddWithValue("@ContSalesGroup", "")
                'Else
                'command.Parameters.AddWithValue("@ContSalesGroup", ddlSalesGrp.Text)
                'End If
                If ddlCompanyGrp.Text = txtDDLText.Text Then
                    command.Parameters.AddWithValue("@ContGroup", "")
                Else
                    command.Parameters.AddWithValue("@ContGroup", ddlCompanyGrp.Text)
                End If
                If ddlSalesMan.Text = txtDDLText.Text Then
                    command.Parameters.AddWithValue("@ContSales", "")
                Else
                    command.Parameters.AddWithValue("@ContSales", ddlSalesMan.Text)
                End If
                If ddlIncharge.Text = txtDDLText.Text Then
                    command.Parameters.AddWithValue("@ContInCharge", "")
                Else
                    command.Parameters.AddWithValue("@ContInCharge", ddlIncharge.Text)
                End If

                command.Parameters.AddWithValue("@ContTel", txtOffContactNo.Text)
                command.Parameters.AddWithValue("@ContFax", txtOffFax.Text)
                command.Parameters.AddWithValue("@ContHP", txtOffMobile.Text)
                command.Parameters.AddWithValue("@ContEmail", txtOffCont1Email.Text)
                command.Parameters.AddWithValue("@ContAddBlock", "")
                command.Parameters.AddWithValue("@ContAddNos", "")
                command.Parameters.AddWithValue("@ContAddFloor", "")
                command.Parameters.AddWithValue("@ContAddUnit", "")
                command.Parameters.AddWithValue("@ContAddress1", txtOffAddress1.Text)
                command.Parameters.AddWithValue("@ContAddStreet", txtOffStreet.Text)
                command.Parameters.AddWithValue("@ContAddBuilding", txtOffBuilding.Text)
                command.Parameters.AddWithValue("@ContAddPostal", txtOffPostal.Text)
                command.Parameters.AddWithValue("@ContAddState", ddlOffState.Text)
                If ddlOffCity.Text = txtDDLText.Text Then
                    command.Parameters.AddWithValue("@ContAddCity", "")
                Else
                    command.Parameters.AddWithValue("@ContAddCity", ddlOffCity.Text)
                End If
                If ddlOffCountry.Text = txtDDLText.Text Then
                    command.Parameters.AddWithValue("@ContAddCountry", "")
                Else
                    command.Parameters.AddWithValue("@ContAddCountry", ddlOffCountry.Text)
                End If
                command.Parameters.AddWithValue("@ContBillTel", txtBillCP1Tel.Text)
                command.Parameters.AddWithValue("@ContBILLFax", txtBillCP1Fax.Text)
                command.Parameters.AddWithValue("@ContBILLHP", txtBillCP1Mobile.Text)
                command.Parameters.AddWithValue("@ContBILLEmail", txtBillCP1Email.Text)
                command.Parameters.AddWithValue("@ContBILLBlock", "")
                command.Parameters.AddWithValue("@ContBILLNos", "")
                command.Parameters.AddWithValue("@ContBILLFloor", "")
                command.Parameters.AddWithValue("@ContBILLUnit", "")
                command.Parameters.AddWithValue("@ContBILLAddress1", txtBillAddress.Text)
                command.Parameters.AddWithValue("@ContBILLStreet", txtBillStreet.Text)
                command.Parameters.AddWithValue("@ContBILLBuilding", txtBillBuilding.Text)
                command.Parameters.AddWithValue("@ContBILLPostal", txtBillPostal.Text)
                command.Parameters.AddWithValue("@ContBILLState", ddlBillState.Text)
                If ddlBillCity.Text = txtDDLText.Text Then
                    command.Parameters.AddWithValue("@ContBILLCity", "")
                Else
                    command.Parameters.AddWithValue("@ContBILLCity", ddlBillCity.Text)
                End If
                If ddlBillCountry.Text = txtDDLText.Text Then
                    command.Parameters.AddWithValue("@ContBILLCountry", "")
                Else
                    command.Parameters.AddWithValue("@ContBILLCountry", ddlBillCountry.Text)
                End If
                command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                command.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))

                command.Parameters.AddWithValue("@Dealer", "")
                command.Parameters.AddWithValue("@Name2", txtNameO.Text)
                command.Parameters.AddWithValue("@WebLoginID", "")
                command.Parameters.AddWithValue("@WebLoginPassWord", "")
                command.Parameters.AddWithValue("@WebAccessLevel", 0)
                command.Parameters.AddWithValue("@WebOneTimePassWord", "")
                command.Parameters.AddWithValue("@WebLevel", "")
                command.Parameters.AddWithValue("@DiscountPct", 0)
                command.Parameters.AddWithValue("@LoginID", "")
                command.Parameters.AddWithValue("@Password", "")
                command.Parameters.AddWithValue("@PriceGroup", "")
                command.Parameters.AddWithValue("@SubCompanyNo", "")
                command.Parameters.AddWithValue("@SalesGRP", "")
                command.Parameters.AddWithValue("@LocateGRP", "")
                command.Parameters.AddWithValue("@Telephone2", txtOffContact2.Text)
                command.Parameters.AddWithValue("@BillTelephone2", txtBillCP1Tel2.Text)
                command.Parameters.AddWithValue("@Mobile", txtOffCont1Mobile.Text)
                command.Parameters.AddWithValue("@BillMobile", txtBillCP1Mobile.Text)
                command.Parameters.AddWithValue("@SoPriceGroup", "")
                command.Parameters.AddWithValue("@AccountNo", "")
                If chkInactive.Checked = True Then
                    command.Parameters.AddWithValue("@InActive", 1)

                Else
                    command.Parameters.AddWithValue("@InActive", 0)

                End If
                command.Parameters.AddWithValue("@ContARTerm", "")
                command.Parameters.AddWithValue("@ContAPTerm", "")
                command.Parameters.AddWithValue("@ContRentalTerm", "")
                command.Parameters.AddWithValue("@ContShippingTerm", "")
                command.Parameters.AddWithValue("@ContApCurrency", "")
                command.Parameters.AddWithValue("@ContArCurrency", "")
                If ddlIndustry.Text = txtDDLText.Text Then
                    command.Parameters.AddWithValue("@Industry", "")
                Else
                    command.Parameters.AddWithValue("@Industry", ddlIndustry.Text)
                End If

                command.Parameters.AddWithValue("@InterCompany", 0)
                command.Parameters.AddWithValue("@CreateDeviceID", "")
                command.Parameters.AddWithValue("@CreateSource", "")
                command.Parameters.AddWithValue("@FlowFrom", "")
                command.Parameters.AddWithValue("@FlowTo", "")
                command.Parameters.AddWithValue("@EditSource", "")
                command.Parameters.AddWithValue("@DeleteStatus", "")
                command.Parameters.AddWithValue("@LastEditDevice", "")
                command.Parameters.AddWithValue("@AutoEmailServ", 0)
                command.Parameters.AddWithValue("@ReportFormatServ", "")
                command.Parameters.AddWithValue("@Email", txtOffEmail.Text)
                command.Parameters.AddWithValue("@WebUploadDate", DBNull.Value)

                command.Parameters.AddWithValue("@IsCustomer", 0)

                command.Parameters.AddWithValue("@IsSupplier", 0)
                command.Parameters.AddWithValue("@AccountID", txtAccountID.Text)
                command.Parameters.AddWithValue("@OffContact1Position", txtOffCont1Position.Text)
                command.Parameters.AddWithValue("@OffContact1Tel", txtOffCont1Tel.Text)
                command.Parameters.AddWithValue("@OffContact1Fax", txtOffCont1Fax.Text)
                command.Parameters.AddWithValue("@OffContact1Tel2", txtOffCont1Tel2.Text)
                command.Parameters.AddWithValue("@OffContact1Mobile", txtOffCont1Mobile.Text)
                command.Parameters.AddWithValue("@BillContact1Position", txtBillCP1Position.Text)
                command.Parameters.AddWithValue("@BillContact2", txtBillCP2Contact.Text)
                command.Parameters.AddWithValue("@BillContact2Position", txtBillCP2Position.Text)
                command.Parameters.AddWithValue("@BillContact1Email", txtBillCP1Email.Text)
                command.Parameters.AddWithValue("@BillContact2Email", txtBillCP2Email.Text)
                command.Parameters.AddWithValue("@BillContact2Tel", txtBillCP2Tel.Text)
                command.Parameters.AddWithValue("@BillContact2Fax", txtBillCP2Fax.Text)
                command.Parameters.AddWithValue("@BillContact2Tel2", txtBillCP2Tel2.Text)
                command.Parameters.AddWithValue("@BillContact2Mobile", txtBillCP2Mobile.Text)
                command.Parameters.AddWithValue("@OffContact1", txtOffCont1Name.Text)
                command.Parameters.AddWithValue("@OffContactPosition", txtOffPosition.Text)
                ' command.Parameters.AddWithValue("@OffContactMobile", txtOffMobile.Text)
                command.Parameters.AddWithValue("@BillContactPerson", txtBillCP1Contact.Text)
                command.Connection = conn

                command.ExecuteNonQuery()

            End If

        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "UPDATE CONTACT MASTER", ex.Message.ToString, txtAccountID.Text)
        End Try
    End Sub

    Private Sub DeleteContactMaster(conn As MySqlConnection)
        Try

            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            If String.IsNullOrEmpty(txtAccountID.Text) Then
                command1.CommandText = "SELECT * FROM tblcontactmaster where contid=@id and conttype='COMPANY'"
                command1.Parameters.AddWithValue("@id", txtClientID.Text)
            Else
                command1.CommandText = "SELECT * FROM tblcontactmaster where accountid=@id and conttype='COMPANY'"
                command1.Parameters.AddWithValue("@id", txtAccountID.Text)
            End If

            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New System.Data.DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then

                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                '  Dim qry As String = "delete from tblcontactmaster where contid=@id and conttype='COMPANY'"
                Dim qry As String
                If String.IsNullOrEmpty(txtAccountID.Text) Then
                    qry = "update tblcontactmaster set Inactive=1 where contid=@id and conttype='COMPANY'"
                    command.Parameters.AddWithValue("@id", txtClientID.Text)
                Else
                    qry = "update tblcontactmaster set Inactive=1 where accountid=@id and conttype='COMPANY'"
                    command.Parameters.AddWithValue("@id", txtAccountID.Text)
                End If



                command.CommandText = qry

                command.Connection = conn

                command.ExecuteNonQuery()


            End If


        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "DELETE CONTACT MASTER", ex.Message.ToString, txtAccountID.Text)
        End Try
    End Sub

    Private Function Validation() As Boolean
        Dim d As Date


        If String.IsNullOrEmpty(txtStartDate.Text) = False Then
            If Date.TryParseExact(txtStartDate.Text, "dd/MM/yyyy", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, d) Then
                txtStartDate.Text = d.ToShortDateString
            Else
                ' MessageBox.Message.Alert(Page, "Start Date is invalid", "str")
                lblAlert.Text = "INVALID START DATE"
                Return False
                Exit Function
            End If
        End If
        Return True
    End Function

    Private Function IgnoredWords(conn As MySqlConnection, name As String) As String
        Dim command1 As MySqlCommand = New MySqlCommand

        command1.CommandType = CommandType.Text

        command1.CommandText = "SELECT * FROM tblcustomerignoredwords where fieldtype='Name'"
        command1.Parameters.AddWithValue("@name", name)
        command1.Connection = conn

        Dim dr As MySqlDataReader = command1.ExecuteReader()
        Dim dt As New System.Data.DataTable
        dt.Load(dr)

        If dt.Rows.Count > 0 Then
            For i As Int16 = 0 To dt.Rows.Count - 1
                name = name.Replace(dt.Rows(i)("IgnoredWord").ToString.ToUpper, " ")

            Next
        End If

        Return name.Trim

    End Function


    Private Function AddrIgnoredWords(conn As MySqlConnection, addr As String) As String
        Dim command1 As MySqlCommand = New MySqlCommand

        command1.CommandType = CommandType.Text

        command1.CommandText = "SELECT * FROM tblcustomerignoredwords where fieldtype='ADDR'"
        command1.Parameters.AddWithValue("@name", addr)
        command1.Connection = conn

        Dim dr As MySqlDataReader = command1.ExecuteReader()
        Dim dt As New System.Data.DataTable
        dt.Load(dr)

        If dt.Rows.Count > 0 Then
            For i As Int16 = 0 To dt.Rows.Count - 1
                addr = addr.Replace(dt.Rows(i)("IgnoredWord").ToString.ToUpper, " ")

            Next
        End If

        Return addr.Trim

    End Function

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        'MessageBox.Message.Alert(Page, ddlSearchStatus.Text + " " + txtDDLText.Text, "str")
        Dim qry As String
        Try
            'Return

            'If txtDisplayRecordsLocationwise.Text = "N" Then
            qry = "Select distinct tblcompany.Rcno, tblcompany.AccountId, tblcompany.InActive, tblcompany.ID, tblcompany.Name, tblcompany.ARCurrency, tblcompany.Location, companybal.Bal, tblcompany.Telephone, tblcompany.Fax, tblcompany.Address1, tblcompany.AddPOstal, tblcompany.BillAddress1, tblcompany.BillPostal, tblcompany.ContactPerson ,tblcompany.ARTerm, tblcompany.Industry,  tblcompany.LocateGrp, tblcompany.CompanyGroup, tblcompany.AccountNo, tblcompany.Salesman, tblcompany.AddStreet, tblcompany.AddBuilding, tblcompany.AddCity, tblcompany.AddState, tblcompany.AddCountry, tblcompany.BillStreet, tblcompany.BillBuilding, tblcompany.BillCity, tblcompany.BillState, tblcompany.BillCountry,  tblcompany.CreatedBy, tblcompany.CreatedOn, tblcompany.LastModifiedBy, tblcompany.LastModifiedOn, tblcompany.AutoEmailInvoice,tblcompany.AutoEmailSOA, tblcompany.UnSubscribeAutoEmailDate, tblCompany.TaxIdentificationNo, tblCompany.SalesTaxRegistrationNo  "
            qry = qry + " FROM tblcompany  left join companybal  on tblcompany.Accountid = companybal.Accountid left join tblcompanyLocation  on tblcompany.Accountid = tblcompanyLocation.Accountid where 1=1 "

            If txtDisplayRecordsLocationwise.Text = "Y" Then
                If ddlBranchSearch.SelectedIndex = 0 Then
                    ddlBranch.SelectedIndex = 0
                Else
                    ddlBranch.Text = ddlBranchSearch.Text
                    qry = qry + " and tblcompany.location = '" + ddlBranchSearch.Text + "'"

                End If
            End If

            If String.IsNullOrEmpty(txtSearchID.Text) = False Then
                txtAccountID.Text = txtSearchID.Text
                qry = qry + " and tblcompany.accountid like '%" + txtSearchID.Text + "%'"

            End If
            If String.IsNullOrEmpty(ddlSearchStatus.Text) = False Then
                If ddlSearchStatus.Text <> txtDDLText.Text Then
                    ddlStatus.Text = ddlSearchStatus.Text
                    qry = qry + " and tblcompany.status = '" + ddlSearchStatus.Text + "'"
                End If
            End If
            If String.IsNullOrEmpty(txtSearchCompany.Text) = False Then
                txtNameE.Text = txtSearchCompany.Text
                qry = qry + " and tblcompany.name like ""%" + (txtSearchCompany.Text) + "%"""
            End If
            If String.IsNullOrEmpty(txtSearchAddress.Text) = False Then

                qry = qry + " and (tblcompany.address1 like ""%" + txtSearchAddress.Text + "%"""
                qry = qry + " or tblcompany.addbuilding like '%" + txtSearchAddress.Text + "%'"
                qry = qry + " or tblcompany.addstreet like '%" + txtSearchAddress.Text + "%')"
            End If
            If String.IsNullOrEmpty(txtSearchBillingAddress.Text) = False Then

                qry = qry + " and (tblcompany.billaddress1 like ""%" + txtSearchBillingAddress.Text + "%"""
                qry = qry + " or tblcompany.billbuilding like '%" + txtSearchBillingAddress.Text + "%'"
                qry = qry + " or tblcompany.billstreet like '%" + txtSearchBillingAddress.Text + "%')"
            End If
            If String.IsNullOrEmpty(txtSearchPostal.Text) = False Then
                txtPostal.Text = txtSearchPostal.Text
                qry = qry + " and (tblcompany.addpostal like '" + txtSearchPostal.Text + "%'"
                qry = qry + " or tblcompany.billpostal like '" + txtSearchPostal.Text + "%')"
            End If
            If String.IsNullOrEmpty(txtSearchContact.Text) = False Then
                qry = qry + " and (tblcompany.contactperson  like '%" + txtSearchContact.Text + "%'"
                qry = qry + " or tblcompany.offcontact1  like '%" + txtSearchContact.Text + "%'"
                qry = qry + " or tblcompany.BillingName  like '%" + txtSearchContact.Text + "%'"

                qry = qry + " or tblcompany.BillcontactPerson  like '%" + txtSearchContact.Text + "%'"
                qry = qry + " or tblcompany.Billcontact2  like '%" + txtSearchContact.Text + "%')"


            End If
            If String.IsNullOrEmpty(ddlSearchSalesman.Text) = False Then
                If ddlSearchSalesman.Text <> txtDDLText.Text Then
                    qry = qry + " and tblcompany.salesman  = '" + ddlSearchSalesman.Text + "'"
                End If
            End If
            If String.IsNullOrEmpty(ddlSearchIndustry.Text) = False Then
                If ddlSearchIndustry.Text <> txtDDLText.Text Then
                    qry = qry + " and tblcompany.industry  = '" + ddlSearchIndustry.Text + "'"
                End If
            End If
            If String.IsNullOrEmpty(txtSearchContactNo.Text) = False Then

                qry = qry + " and (tblcompany.mobile  like '%" + txtSearchContactNo.Text + "%' or tblcompany.telephone like '%" + txtSearchContactNo.Text + "%'"
                qry = qry + " or tblcompany.telephone2  like '%" + txtSearchContactNo.Text + "%'"
                qry = qry + " or tblcompany.offcontact1Tel  like '%" + txtSearchContactNo.Text + "%'"
                qry = qry + " or tblcompany.offcontact1Tel2  like '%" + txtSearchContactNo.Text + "%'"
                qry = qry + " or tblcompany.offcontact1Mobile  like '%" + txtSearchContactNo.Text + "%'"
                qry = qry + " or tblcompany.BillTelephone  like '%" + txtSearchContactNo.Text + "%'"
                qry = qry + " or tblcompany.BillTelephone2  like '%" + txtSearchContactNo.Text + "%'"
                qry = qry + " or tblcompany.BillMobile  like '%" + txtSearchContactNo.Text + "%'"
                qry = qry + " or tblcompany.Billcontact2Tel  like '%" + txtSearchContactNo.Text + "%'"
                qry = qry + " or tblcompany.Billcontact2Tel2  like '%" + txtSearchContactNo.Text + "%'"
                qry = qry + " or tblcompany.Billcontact2Mobile  like '%" + txtSearchContactNo.Text + "%')"

            End If
            If chkSearchInactive.Checked = True Then
                qry = qry + " and tblcompany.Inactive=1"
            End If
            If String.IsNullOrEmpty(txtSearchServiceAddress.Text) = False Then
                qry = qry + " and tblcompany.accountid in (select accountid from tblcompanylocation where rcno <> 0"
                qry = qry + " and (tblcompanylocation.address1 like ""%" + txtSearchServiceAddress.Text + "%"""
                qry = qry + " or tblcompanylocation.addbuilding like '%" + txtSearchServiceAddress.Text + "%'"
                qry = qry + " or tblcompanylocation.addpostal like '" + txtSearchServiceAddress.Text + "%'"
                qry = qry + " or tblcompanylocation.addstreet like '%" + txtSearchServiceAddress.Text + "%'))"
            End If
            'End If



            'If txtDisplayRecordsLocationwise.Text = "Y" Then
            '    qry = " SELECT distinct tblcompany.Rcno, tblcompany.AccountId, tblcompany.InActive, tblcompany.ID, tblcompany.Name, tblcompany.ARCurrency, tblcompany.Location, companybal.Bal, tblcompany.Telephone, tblcompany.Fax, tblcompany.Address1, tblcompany.AddPOstal, tblcompany.BillAddress1, tblcompany.BillPostal, tblcompany.ContactPerson ,tblcompany.ARTerm, tblcompany.Industry,  tblcompany.LocateGrp, tblcompany.CompanyGroup, tblcompany.AccountNo, tblcompany.Salesman, tblcompany.AddStreet, tblcompany.AddBuilding, tblcompany.AddCity, tblcompany.AddState, tblcompany.AddCountry, tblcompany.BillStreet, tblcompany.BillBuilding, tblcompany.BillCity, tblcompany.BillState, tblcompany.BillCountry,  tblcompany.CreatedBy, tblcompany.CreatedOn, tblcompany.LastModifiedBy, tblcompany.LastModifiedOn FROM tblcompany  left join companybal  on tblcompany.Accountid = companybal.Accountid left join tblcompanyLocation  on tblcompany.Accountid = tblcompanyLocation.Accountid where tblcompany.Inactive=0 "
            '    qry = qry + " and tblcompanyLocation.location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "')"

            '    'qry = "Select * from tblcompany left join companybal  on tblcompany.Accountid = companybal.Accountid where 1=1 "
            '    If String.IsNullOrEmpty(txtSearchID.Text) = False Then
            '        txtAccountID.Text = txtSearchID.Text
            '        qry = qry + " and tblcompany.accountid like '%" + txtSearchID.Text + "%'"

            '    End If
            '    If String.IsNullOrEmpty(ddlSearchStatus.Text) = False Then
            '        If ddlSearchStatus.Text <> txtDDLText.Text Then
            '            ddlStatus.Text = ddlSearchStatus.Text
            '            qry = qry + " and tblcompany.status = '" + ddlSearchStatus.Text + "'"
            '        End If
            '    End If
            '    If String.IsNullOrEmpty(txtSearchCompany.Text) = False Then
            '        txtNameE.Text = txtSearchCompany.Text
            '        qry = qry + " and tblcompany.name like ""%" + (txtSearchCompany.Text) + "%"""
            '    End If
            '    If String.IsNullOrEmpty(txtSearchAddress.Text) = False Then

            '        qry = qry + " and (tblcompany.address1 like ""%" + txtSearchAddress.Text + "%"""
            '        qry = qry + " or tblcompany.addbuilding like '%" + txtSearchAddress.Text + "%'"
            '        qry = qry + " or tblcompany.addstreet like '%" + txtSearchAddress.Text + "%')"
            '    End If
            '    If String.IsNullOrEmpty(txtSearchBillingAddress.Text) = False Then

            '        qry = qry + " and (tblcompany.billaddress1 like ""%" + txtSearchBillingAddress.Text + "%"""
            '        qry = qry + " or tblcompany.billbuilding like '%" + txtSearchBillingAddress.Text + "%'"
            '        qry = qry + " or tblcompany.billstreet like '%" + txtSearchBillingAddress.Text + "%')"
            '    End If
            '    If String.IsNullOrEmpty(txtSearchPostal.Text) = False Then
            '        txtPostal.Text = txtSearchPostal.Text
            '        qry = qry + " and (tblcompany.addpostal like '" + txtSearchPostal.Text + "%'"
            '        qry = qry + " or tblcompany.billpostal like '" + txtSearchPostal.Text + "%')"
            '    End If
            '    If String.IsNullOrEmpty(txtSearchContact.Text) = False Then
            '        qry = qry + " and (tblcompany.contactperson  like '%" + txtSearchContact.Text + "%'"
            '        qry = qry + " or tblcompany.offcontact1  like '%" + txtSearchContact.Text + "%'"
            '        qry = qry + " or tblcompany.BillingName  like '%" + txtSearchContact.Text + "%'"

            '        qry = qry + " or tblcompany.BillcontactPerson  like '%" + txtSearchContact.Text + "%'"
            '        qry = qry + " or tblcompany.Billcontact2  like '%" + txtSearchContact.Text + "%')"


            '    End If
            '    If String.IsNullOrEmpty(ddlSearchSalesman.Text) = False Then
            '        If ddlSearchSalesman.Text <> txtDDLText.Text Then
            '            qry = qry + " and tblcompany.salesman  = '" + ddlSearchSalesman.Text + "'"
            '        End If
            '    End If
            '    If String.IsNullOrEmpty(ddlSearchIndustry.Text) = False Then
            '        If ddlSearchIndustry.Text <> txtDDLText.Text Then
            '            qry = qry + " and tblcompany.industry  = '" + ddlSearchIndustry.Text + "'"
            '        End If
            '    End If
            '    If String.IsNullOrEmpty(txtSearchContactNo.Text) = False Then

            '        qry = qry + " and (tblcompany.mobile  like '%" + txtSearchContactNo.Text + "%' or telephone like '%" + txtSearchContactNo.Text + "%'"
            '        qry = qry + " or tblcompany.telephone2  like '%" + txtSearchContactNo.Text + "%'"
            '        qry = qry + " or tblcompany.offcontact1Tel  like '%" + txtSearchContactNo.Text + "%'"
            '        qry = qry + " or tblcompany.offcontact1Tel2  like '%" + txtSearchContactNo.Text + "%'"
            '        qry = qry + " or tblcompany.offcontact1Mobile  like '%" + txtSearchContactNo.Text + "%'"
            '        qry = qry + " or tblcompany.BillTelephone  like '%" + txtSearchContactNo.Text + "%'"
            '        qry = qry + " or tblcompany.BillTelephone2  like '%" + txtSearchContactNo.Text + "%'"
            '        qry = qry + " or tblcompany.BillMobile  like '%" + txtSearchContactNo.Text + "%'"
            '        qry = qry + " or tblcompany.Billcontact2Tel  like '%" + txtSearchContactNo.Text + "%'"
            '        qry = qry + " or tblcompany.Billcontact2Tel2  like '%" + txtSearchContactNo.Text + "%'"
            '        qry = qry + " or tblcompany.Billcontact2Mobile  like '%" + txtSearchContactNo.Text + "%')"

            '    End If
            '    If chkSearchInactive.Checked = True Then
            '        qry = qry + " and tblcompany.Inactive=1"
            '    End If
            '    If String.IsNullOrEmpty(txtSearchServiceAddress.Text) = False Then
            '        qry = qry + " and tblcompany.accountid in (select accountid from tblcompanylocation where rcno <> 0"
            '        qry = qry + " and (tblcompanylocation.address1 like ""%" + txtSearchServiceAddress.Text + "%"""
            '        qry = qry + " or tblcompanylocation.addbuilding like '%" + txtSearchServiceAddress.Text + "%'"
            '        qry = qry + " or tblcompanylocation.addpostal like '" + txtSearchServiceAddress.Text + "%'"
            '        qry = qry + " or tblcompanylocation.addstreet like '%" + txtSearchServiceAddress.Text + "%'))"
            '    End If
            'End If

            If txtDisplayRecordsLocationwise.Text = "Y" Then
                'qry = qry + " and Location = '" & ddlLocation.Text.Trim & "'"

                qry = qry + " and tblcompanyLocation.location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "')"

            End If



            qry = qry + " order by tblcompany.rcno desc,tblcompany.name;"
            txt.Text = qry

            MakeMeNull()
            SqlDataSource1.SelectCommand = qry
            SqlDataSource1.DataBind()
            GridView1.DataBind()
            txtSearchID.Text = ""
            txtSearchCompany.Text = ""
            txtSearchAddress.Text = ""
            txtSearchContactNo.Text = ""
            txtSearchContact.Text = ""
            txtSearchPostal.Text = ""
            ddlSearchSalesman.ClearSelection()
            '   txtSearchContact.Text = ""
            ddlSearchIndustry.ClearSelection()
            ddlSearchStatus.ClearSelection()
            txtSearchBillingAddress.Text = ""


            txtSearchCust.Text = txtSearchServiceAddress.Text
            txtSearch.Text = txtSearchServiceAddress.Text

            txtSearchServiceAddress.Text = ""
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "BTNSEARCH_CLICK", ex.Message.ToString, qry)
        End Try
    End Sub

    'Protected Sub GridView2_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView2.PageIndexChanging
    '    GridView2.PageIndex = e.NewPageIndex
    '    SqlDataSource2.SelectCommand = "Select distinct name,id from tblcompanystaff where rcno <>0 and name like '" + ViewState("CurrentAlphabet") + "%'"
    '    SqlDataSource2.DataBind()
    '    GridView2.DataBind()
    '    ModalPopupExtender2.Show()
    '    'If txtModal.Text = "SearchContact" Then
    '    '    ModalPopupExtender1.Show()
    '    '    ModalPopupExtender2.Show()
    '    'End If

    'End Sub

    'Protected Sub Alphabet_Click(sender As Object, e As EventArgs)

    '    Dim lnkAlphabet As LinkButton = DirectCast(sender, LinkButton)
    '    ViewState("CurrentAlphabet") = lnkAlphabet.Text
    '    Me.GenerateAlphabets()
    '    GridView2.PageIndex = 0
    '    SqlDataSource2.SelectCommand = "Select distinct name,id from tblcompanystaff where rcno <>0 and name like '" + lnkAlphabet.Text + "%'"
    '    SqlDataSource2.DataBind()
    '    GridView2.DataBind()
    '    ModalPopupExtender2.Show()
    '    'If txtModal.Text = "SearchContact" Then
    '    '    ModalPopupExtender1.Show()
    '    '    ModalPopupExtender2.Show()
    '    'End If

    'End Sub

    'Protected Sub GridView2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView2.SelectedIndexChanged
    '    '  MessageBox.Message.Alert(Page, txtModal.Text, "str")
    '    If txtModal.Text = "Contact" Then
    '        txtContact.Text = GridView2.SelectedRow.Cells(2).Text
    '    ElseIf txtModal.Text = "BillContact" Then
    '        txtBillContact.Text = GridView2.SelectedRow.Cells(2).Text
    '    ElseIf txtModal.Text = "SearchContact" Then
    '        txtSearchContact.Text = GridView2.SelectedRow.Cells(2).Text
    '        ModalPopupExtender1.Show()
    '    End If

    '    txtSearchName.Text = ""
    'End Sub


    ''Protected Sub btnContact_Click(sender As Object, e As ImageClickEventArgs) Handles btnContact.Click
    ''    ModalPopupExtender2.TargetControlID = "btnContact"
    ''    txtModal.Text = "Contact"
    ''    ModalPopupExtender2.Show()
    ''End Sub

    'Protected Sub btnSearchName_Click(sender As Object, e As ImageClickEventArgs) Handles btnSearchName.Click
    '    If String.IsNullOrEmpty(txtSearchName.Text) = False Then

    '        SqlDataSource2.SelectCommand = "Select distinct name,id from tblcompanystaff where rcno <>0 and name like '%" + txtSearchName.Text + "%'"
    '        SqlDataSource2.DataBind()
    '        GridView2.DataBind()
    '    Else
    '        MessageBox.Message.Alert(Page, "Enter text to search", "str")

    '    End If

    '    ModalPopupExtender2.Show()
    'End Sub

    'Protected Sub btnBillContact_Click(sender As Object, e As ImageClickEventArgs) Handles btnBillContact.Click
    '    ModalPopupExtender2.TargetControlID = "btnBillContact"
    '    txtModal.Text = "BillContact"
    '    ModalPopupExtender2.Show()
    'End Sub

    'Protected Sub btnSearchContact_Click(sender As Object, e As ImageClickEventArgs) Handles btnSearchContact.Click
    '    ModalPopupExtender2.TargetControlID = "btnSearchContact"
    '    txtModal.Text = "SearchContact"
    '    ModalPopupExtender2.Show()
    'End Sub


    Protected Sub btnContract_Click(sender As Object, e As EventArgs) Handles btnContract.Click
        Try
            lblMessage.Text = ""
            If txtRcno.Text = "" Then
                '  MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
                lblAlert.Text = "SELECT RECORD TO OPEN CONTRACT"
                Exit Sub
            End If
            Session.Remove("locationid")
            Session.Remove("sevaddress")

            Session("contractfrom") = "clients"
            Session("contracttype") = "CORPORATE"
            Session("companygroup") = ddlCompanyGrp.Text
            'Session("clientid") = txtID.Text
            Session("accountid") = txtAccountID.Text.Trim

            Session("custname") = txtNameE.Text


            'Session("contactperson") = txtContact.Text
            'Session("conpermobile") = txtMobile.Text
            'Session("acctcode") = txtAcctCode.Text
            'Session("telephone") = txtTelephone.Text
            'Session("fax") = txtFax.Text
            Session("postal") = txtOffPostal.Text
            Session("sevaddress") = txtAddress.Text
            Session("locategrp") = ddlLocateGrp.Text
            Session("salesman") = ddlSalesMan.Text


            Session("offaddress1") = txtOffAddress1.Text
            Session("offstreet") = txtOffStreet.Text
            Session("offbuilding") = txtOffBuilding.Text



            If (ddlOffCity.Text.Trim) = "-1" Then
                Session("offcity") = ""
            Else
                Session("offcity") = ddlBillCity.Text
            End If
            'Session("offcity") = ddlOffCity.Text
            Session("offpostal") = txtOffPostal.Text

            Session("billaddress1") = txtBillAddress.Text
            Session("billstreet") = txtBillStreet.Text
            Session("billbuilding") = txtBillBuilding.Text

            If (ddlBillCity.Text.Trim) = "-1" Then
                Session("billcity") = ""
            Else
                Session("billcity") = ddlBillCity.Text
            End If

            If (ddlBillCountry.Text.Trim) = "-1" Then
                Session("billcountry") = ""
            Else
                Session("billcountry") = ddlBillCountry.Text
            End If

            'Session("billcity") = ddlBillCity.Text
            Session("billpostal") = txtBillPostal.Text

            Session("industry") = ddlIndustry.Text

            'If String.IsNullOrEmpty(Session("contractfrom")) = False Then
            '    Session("clientid") = txtID.Text

            'End If

            Session("gridsqlCompany") = txt.Text
            Session("rcno") = txtRcno.Text

            Session("gridsqlCompanyDetail") = txtDetail.Text
            Session("rcnoDetail") = txtSvcRcno.Text
            Session("inactive") = chkInactive.Checked
            'Session("location") = ddlLocation.Text

            'If ddlLocation.SelectedIndex = 0 Then
            Session("location") = ""
            'Else
            'Session("location") = ddlLocation.Text
            'End If
            Response.Redirect("contract.aspx", False)
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "btnContract_Click", ex.Message.ToString, "")
        End Try
    End Sub

    Protected Sub txtSearchPostal_TextChanged(sender As Object, e As EventArgs) Handles txtSearchPostal.TextChanged

    End Sub

    Protected Sub txtPostal_TextChanged(sender As Object, e As EventArgs) Handles txtPostal.TextChanged
        If txtPostal.Text.Length > 0 Then
            'MessageBox.Message.Alert(Page, "hi", "str")
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()
                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                command1.CommandText = "SELECT * FROM tblpostaltolocation where postalbeginwith=@postalbeginwith"
                If txtPostal.Text.Length >= 2 Then
                    command1.Parameters.AddWithValue("@postalbeginwith", txtPostal.Text.Substring(0, 2))
                ElseIf txtPostal.Text.Length = 1 Then
                    command1.Parameters.AddWithValue("@postalbeginwith", "0" + txtPostal.Text)

                End If
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New System.Data.DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then
                    If dt.Rows(0)("LocateGRP").ToString <> "" Then
                        ddlLocateGrp.Text = dt.Rows(0)("LocateGRP").ToString
                    End If
                End If

                conn.Close()
                conn.Dispose()
                command1.Dispose()
                dt.Dispose()
                dr.Close()

            Catch ex As Exception
                InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "txtPostal_TextChanged", ex.Message.ToString, txtAccountID.Text + ", " + txtPostal.Text)
            End Try
        End If

    End Sub



    Protected Sub GenerateAccountNo()

        Try
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()
            Dim command1 As MySqlCommand = New MySqlCommand
            Dim command As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.CommandText = "SELECT Period01 FROM tbldoccontrol where Prefix = 'Contact';"
            '  command1.Parameters.AddWithValue("@code", txtAcctCode.Text)
            command1.Connection = conn
            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New System.Data.DataTable
            dt.Load(dr)
            If dt.Rows.Count > 0 Then
                Dim lastnum As Int64 = Convert.ToInt64(dt.Rows(0)("Period01"))
                lastnum = lastnum + 1

                command.CommandType = CommandType.Text

                command.CommandText = "UPDATE tbldoccontrol set Period01=@lastnum where Prefix = 'Contact';"
                command.Parameters.Clear()

                command.Parameters.AddWithValue("@lastnum", lastnum)

                command.Connection = conn

                command.ExecuteNonQuery()
                txtAccountID.Text = lastnum

            Else
                command.CommandType = CommandType.Text

                command.CommandText = "insert into tbldoccontrol(Prefix,Period01) values ('Contact',10000001);"
                command.Connection = conn

                command.ExecuteNonQuery()

                txtAccountID.Text = "10000001"

            End If

            conn.Close()
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "GenerateAccountNo", ex.Message.ToString, txtAccountID.Text)
        End Try
    End Sub

    Protected Sub GenerateLocationID()

        Try
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()
            Dim command1 As MySqlCommand = New MySqlCommand
            Dim command As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            If String.IsNullOrEmpty(txtAccountID.Text) Then
                command1.CommandText = "SELECT locationid,locationprefix,locationno FROM tblcompanylocation where companyid='" & txtClientID.Text & "' order by locationno desc;"
                '  command1.Parameters.AddWithValue("@code", txtAcctCode.Text)
                command1.Connection = conn
                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New System.Data.DataTable
                dt.Load(dr)
                If dt.Rows.Count > 0 Then
                    Dim lastnum As Int64 = Convert.ToInt64(dt.Rows(0)("locationno"))
                    lastnum = lastnum + 1
                    'Dim length As String = "D3"
                    Dim length As String = "D4"
                    'txtLocationID.Text = txtClientID.Text + "-" + dt.Rows(0)("locationprefix").ToString + lastnum.ToString("D4")
                    txtLocationID.Text = txtClientID.Text + "-" + lastnum.ToString("D4")
                    txtLocatonNo.Text = lastnum
                    'txtLocationPrefix.Text = "L"
                    txtLocationPrefix.Text = ""
                Else
                    'txtLocationID.Text = txtClientID.Text + "-L001"
                    txtLocationID.Text = txtClientID.Text + "-0001"
                    'txtLocationPrefix.Text = "L"
                    txtLocationPrefix.Text = ""
                    txtLocatonNo.Text = "1"
                End If
            Else
                command1.CommandText = "SELECT locationid,locationprefix,locationno FROM tblcompanylocation where accountid=" & txtAccountID.Text & " order by locationno desc;"
                '  command1.Parameters.AddWithValue("@code", txtAcctCode.Text)
                command1.Connection = conn
                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New System.Data.DataTable
                dt.Load(dr)
                If dt.Rows.Count > 0 Then
                    Dim lastnum As Int64 = Convert.ToInt64(dt.Rows(0)("locationno"))
                    lastnum = lastnum + 1

                    'txtLocationID.Text = txtAccountID.Text + "-" + dt.Rows(0)("locationprefix").ToString + lastnum.ToString("D4")
                    txtLocationID.Text = txtAccountID.Text + "-" + lastnum.ToString("D4")
                    txtLocatonNo.Text = lastnum
                    'txtLocationPrefix.Text = "L"
                    txtLocationPrefix.Text = ""
                Else
                    'txtLocationID.Text = txtAccountID.Text + "-L001"
                    txtLocationID.Text = txtAccountID.Text + "-0001"
                    'txtLocationPrefix.Text = "L"
                    txtLocationPrefix.Text = ""
                    txtLocatonNo.Text = "1"
                End If
            End If

            conn.Close()
            conn.Dispose()
            command.Dispose()
            command1.Dispose()
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "GenerateLocationID", ex.Message.ToString, txtAccountID.Text)
        End Try
    End Sub






    'Protected Sub btnok_Click(sender As Object, e As EventArgs) Handles btnok.Click

    'If String.IsNullOrEmpty(TextBox3.Text) = False Then
    '    '  MessageBox.Message.Alert(Page, TextBox3.Text, "str")
    '    Dim index As Integer = TextBox3.Text.IndexOf("-")
    '    If index = 0 Then
    '        txtID.Text = txtAcctCode.Text + "-01"

    '    Else
    '        Dim code As Integer = Convert.ToInt64(TextBox3.Text.Substring(index + 1))
    '        Dim count As String = "D" + TextBox3.Text.Substring(index + 1).Length.ToString
    '        txtID.Text = txtAcctCode.Text + "-" + (code + 1).ToString(count)

    '    End If


    'End If
    'ModalPopupExtender2.Hide()

    'End Sub

    'Protected Sub btnNo_Click(sender As Object, e As EventArgs) Handles btnNo.Click
    '    MessageBox.Message.Alert(Page, "Enter Account Code", "str")
    '    'txtAcctCode.Text = ""
    '    'txtAcctCode.Focus()

    '    ModalPopupExtender2.Hide()

    'End Sub



    Public Sub MakeSvcNull()
        lblMessage.Text = ""
        lblAlert.Text = ""
        chkSameAddr.Checked = False
        txtSvcMode.Text = ""
        txtLocationID.Text = ""
        txtServiceName.Text = ""
        txtDescription.Text = ""
        txtAddress.Text = ""
        txtStreet.Text = ""
        txtBuilding.Text = ""
        ddlCity.SelectedIndex = 0
        ddlState.SelectedIndex = 0
        ddlCountry.SelectedIndex = 0
        txtPostal.Text = ""
        ddlLocateGrp.SelectedIndex = 0

        txtSvcCP1Contact.Text = ""
        txtSvcCP1Position.Text = ""
        txtSvcCP1Telephone.Text = ""
        txtSvcCP1Fax.Text = ""
        txtSvcCP1Telephone2.Text = ""
        txtSvcCP1Mobile.Text = ""
        txtSvcCP1Email.Text = ""

        txtSvcCP2Contact.Text = ""
        txtSvcCP2Position.Text = ""
        txtSvcCP2Telephone.Text = ""
        txtSvcCP2Tel2.Text = ""
        txtSvcCP2Fax.Text = ""
        txtSvcCP2Mobile.Text = ""
        txtSvcCP2Email.Text = ""

        txtSvcRcno.Text = ""
        txtServiceLocationGroup.Text = ""

        txtBillingNameSvc.Text = ""
        txtBillAddressSvc.Text = ""
        txtBillStreetSvc.Text = ""
        txtBillBuildingSvc.Text = ""
        ddlBillCitySvc.SelectedIndex = 0
        ddlBillStateSvc.SelectedIndex = 0
        ddlBillCountrySvc.SelectedIndex = 0
        txtBillPostalSvc.Text = ""
        txtBillContact1Svc.Text = ""
        txtBillPosition1Svc.Text = ""
        txtBillTelephone1Svc.Text = ""
        txtBillFax1Svc.Text = ""
        txtBilltelephone12Svc.Text = ""
        txtBillMobile1Svc.Text = ""
        txtBillEmail1Svc.Text = ""
        txtBillContact2Svc.Text = ""
        txtBillPosition2Svc.Text = ""
        txtBillTelephone2Svc.Text = ""
        txtBillFax2Svc.Text = ""
        txtBilltelephone22Svc.Text = ""
        txtBillMobile2Svc.Text = ""
        txtBillEmail2Svc.Text = ""

        ddlSalesManSvc.SelectedIndex = 0
        ddlInchargeSvc.SelectedIndex = 0
        ddlTermsSvc.SelectedIndex = 0

        chkServiceReportSendTo1.Checked = False
        chkSvcPhotosMandatory.Checked = False
        ddlIndustrysvc.SelectedIndex = 0
        txtMarketSegmentIDsvc.Text = ""

        ddlContractGrp.SelectedIndex = 0

        txtCommentsSvc.Text = ""

        ddlCompanyGrpD.SelectedIndex = 0
        chkInactiveD.Checked = False

        txtSvcEmailCC.Text = ""
        ddlSvcDefaultInvoiceFormat.SelectedIndex = 0
        ddlBranch.SelectedIndex = 0
        btnSpecificLocation.Text = "SPECIFIC LOCATION"
        chkSendEmailNotificationOnly.Checked = False
        chkSmartCustomer.Checked = False

        txtBusinessHoursStart.Text = ""
        txtBusinessHoursEnd.Text = ""
        chkExcludePIRDatainBusinessHours.Checked = False
        txtSiteName.Text = ""
        txtServiceZone.Text = ""
        txtServiceArea.Text = ""
        ddlContractGroup.ClearSelection()
    End Sub

    Private Sub EnableSvcControls()
        btnSvcSave.Enabled = False
        btnSvcSave.ForeColor = System.Drawing.Color.Gray
        btnSvcCancel.Enabled = False
        btnSvcCancel.ForeColor = System.Drawing.Color.Gray

        btnSvcAdd.Enabled = True
        btnSvcAdd.ForeColor = System.Drawing.Color.Black
        'btnSvcEdit.Enabled = True
        'btnSvcEdit.ForeColor = System.Drawing.Color.Black
        'btnSvcDelete.Enabled = True
        'btnSvcDelete.ForeColor = System.Drawing.Color.Black

        btnChangeStatus.Enabled = False
        btnChangeStatus.ForeColor = System.Drawing.Color.Gray

        'btnEditContractGroup.Visible = False

        chkSameAddr.Enabled = False
        txtLocationID.Enabled = False
        txtServiceName.Enabled = False
        txtDescription.Enabled = False
        txtAddress.Enabled = False
        txtStreet.Enabled = False
        txtBuilding.Enabled = False
        ddlCity.Enabled = False
        ddlState.Enabled = False
        ddlCountry.Enabled = False
        txtPostal.Enabled = False
        ddlLocateGrp.Enabled = False

        txtSvcCP1Contact.Enabled = False
        txtSvcCP1Position.Enabled = False
        txtSvcCP1Telephone.Enabled = False
        txtSvcCP1Fax.Enabled = False
        txtSvcCP1Telephone2.Enabled = False
        txtSvcCP1Mobile.Enabled = False
        txtSvcCP1Email.Enabled = False

        txtSvcCP2Contact.Enabled = False
        txtSvcCP2Position.Enabled = False
        txtSvcCP2Telephone.Enabled = False
        txtSvcCP2Tel2.Enabled = False
        txtSvcCP2Fax.Enabled = False
        txtSvcCP2Mobile.Enabled = False
        txtSvcCP2Email.Enabled = False
        txtServiceLocationGroup.Enabled = False


        txtBillingNameSvc.Enabled = False
        txtBillAddressSvc.Enabled = False
        txtBillStreetSvc.Enabled = False
        txtBillBuildingSvc.Enabled = False
        ddlBillCitySvc.Enabled = False
        ddlBillStateSvc.Enabled = False
        ddlBillCountrySvc.Enabled = False
        txtBillPostalSvc.Enabled = False
        txtBillContact1Svc.Enabled = False
        txtBillPosition1Svc.Enabled = False
        txtBillTelephone1Svc.Enabled = False
        txtBillFax1Svc.Enabled = False
        txtBilltelephone12Svc.Enabled = False
        txtBillMobile1Svc.Enabled = False
        txtBillEmail1Svc.Enabled = False
        txtBillContact2Svc.Enabled = False
        txtBillPosition2Svc.Enabled = False
        txtBillTelephone2Svc.Enabled = False
        txtBillFax2Svc.Enabled = False
        txtBilltelephone22Svc.Enabled = False
        txtBillMobile2Svc.Enabled = False
        txtBillEmail2Svc.Enabled = False

        ddlSalesManSvc.Enabled = False
        ddlInchargeSvc.Enabled = False
        ddlTermsSvc.Enabled = False

        chkServiceReportSendTo1.Enabled = False
        chkServiceReportSendTo2.Enabled = False
        chkSvcPhotosMandatory.Enabled = False

        chkMainBillingInfo.Enabled = False
        ddlIndustrysvc.Enabled = False
        txtMarketSegmentIDsvc.Enabled = False

        ddlContractGrp.Enabled = False

        txtCommentsSvc.Enabled = False

        ddlCompanyGrpD.Enabled = False
        chkInactiveD.Enabled = False

        txtSvcEmailCC.Enabled = False
        ddlSvcDefaultInvoiceFormat.Enabled = False
        ddlBranch.Enabled = False
        chkSendEmailNotificationOnly.Enabled = False
        chkSmartCustomer.Enabled = False

        txtBusinessHoursStart.Enabled = False
        txtBusinessHoursEnd.Enabled = False
        chkExcludePIRDatainBusinessHours.Enabled = False

        txtSiteName.Enabled = False
        txtServiceZone.Enabled = False
        txtServiceArea.Enabled = False
    End Sub

    Private Sub DisableSvcControls()
        btnSvcSave.Enabled = True
        btnSvcSave.ForeColor = System.Drawing.Color.Black
        btnSvcCancel.Enabled = True
        btnSvcCancel.ForeColor = System.Drawing.Color.Black

        btnSvcAdd.Enabled = False
        btnSvcAdd.ForeColor = System.Drawing.Color.Gray

        btnSvcEdit.Enabled = False
        btnSvcEdit.ForeColor = System.Drawing.Color.Gray

        btnSvcCopy.Enabled = False
        btnSvcCopy.ForeColor = System.Drawing.Color.Gray

        btnSvcDelete.Enabled = False
        btnSvcDelete.ForeColor = System.Drawing.Color.Gray

        btnSvcContract.Enabled = False
        btnSvcContract.ForeColor = System.Drawing.Color.Gray

        btnSvcService.Enabled = False
        btnSvcService.ForeColor = System.Drawing.Color.Gray

        btnTransactionsSvc.Enabled = False
        btnTransactionsSvc.ForeColor = System.Drawing.Color.Gray

        btnTransfersSvc.Enabled = False
        btnTransfersSvc.ForeColor = System.Drawing.Color.Gray


        btnSpecificLocation.Enabled = False
        btnSpecificLocation.ForeColor = System.Drawing.Color.Gray



        chkSameAddr.Enabled = True
        txtLocationID.Enabled = True
        txtServiceName.Enabled = True
        txtDescription.Enabled = True
        txtAddress.Enabled = True
        txtStreet.Enabled = True
        txtBuilding.Enabled = True
        ddlCity.Enabled = True
        ddlState.Enabled = True
        ddlCountry.Enabled = True
        txtPostal.Enabled = True
        ddlLocateGrp.Enabled = True

        txtSvcCP1Contact.Enabled = True
        txtSvcCP1Position.Enabled = True
        txtSvcCP1Telephone.Enabled = True
        txtSvcCP1Fax.Enabled = True
        txtSvcCP1Telephone2.Enabled = True
        txtSvcCP1Mobile.Enabled = True
        txtSvcCP1Email.Enabled = True

        txtSvcCP2Contact.Enabled = True
        txtSvcCP2Position.Enabled = True
        txtSvcCP2Telephone.Enabled = True
        txtSvcCP2Tel2.Enabled = True
        txtSvcCP2Fax.Enabled = True
        txtSvcCP2Mobile.Enabled = True
        txtSvcCP2Email.Enabled = True
        txtServiceLocationGroup.Enabled = True


        txtBillingNameSvc.Enabled = True
        txtBillAddressSvc.Enabled = True
        txtBillStreetSvc.Enabled = True
        txtBillBuildingSvc.Enabled = True
        ddlBillCitySvc.Enabled = True
        ddlBillStateSvc.Enabled = True
        ddlBillCountrySvc.Enabled = True
        txtBillPostalSvc.Enabled = True
        txtBillContact1Svc.Enabled = True
        txtBillPosition1Svc.Enabled = True
        txtBillTelephone1Svc.Enabled = True
        txtBillFax1Svc.Enabled = True
        txtBilltelephone12Svc.Enabled = True
        txtBillMobile1Svc.Enabled = True
        txtBillEmail1Svc.Enabled = True
        txtBillContact2Svc.Enabled = True
        txtBillPosition2Svc.Enabled = True
        txtBillTelephone2Svc.Enabled = True
        txtBillFax2Svc.Enabled = True
        txtBilltelephone22Svc.Enabled = True
        txtBillMobile2Svc.Enabled = True
        txtBillEmail2Svc.Enabled = True

        ddlSalesManSvc.Enabled = True
        ddlInchargeSvc.Enabled = True
        ddlTermsSvc.Enabled = True

        chkServiceReportSendTo1.Enabled = True
        chkServiceReportSendTo2.Enabled = True
        chkSvcPhotosMandatory.Enabled = True

        chkMainBillingInfo.Enabled = True
        ddlIndustrysvc.Enabled = True
        'txtMarketSegmentIDsvc.Enabled = True

        ddlContractGrp.Enabled = True

        txtCommentsSvc.Enabled = True

        ddlCompanyGrpD.Enabled = True
        chkInactiveD.Enabled = True

        txtSvcEmailCC.Enabled = True
        ddlSvcDefaultInvoiceFormat.Enabled = True
        ddlBranch.Enabled = True
        chkSendEmailNotificationOnly.Enabled = True

        chkSmartCustomer.Enabled = True

        txtBusinessHoursStart.Enabled = True
        txtBusinessHoursEnd.Enabled = True
        chkExcludePIRDatainBusinessHours.Enabled = True

        txtSiteName.Enabled = True

        btnSvcService.Text = "SERVICE"
        btnSvcContract.Text = "CONTRACT"

        txtServiceZone.Enabled = True
        txtServiceArea.Enabled = True

    End Sub

    Protected Sub GridView2_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView2.PageIndexChanging
        Try
            GridView2.PageIndex = e.NewPageIndex

            SqlDataSource2.SelectCommand = txtDetail.Text
            SqlDataSource2.DataBind()
            GridView2.DataBind()

        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "GridView2_PageIndexChanging", ex.Message.ToString, "")
        End Try
    End Sub

    Protected Sub GridView2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView2.SelectedIndexChanged

        '''''''''''''''''''''''''''''
        If (Session("servicefrom")) = "contactC" Then

            MakeSvcNull()
            txtSvcRcno.Text = Session("rcnoDetail")


            'ddlCity.SelectedIndex = -1
            'ddlState.SelectedIndex = -1
            'ddlCountry.SelectedIndex = -1
            'ddlLocateGrp.SelectedIndex = -1

            'ddlBillCitySvc.SelectedIndex = -1
            'ddlBillStateSvc.SelectedIndex = -1
            'ddlBillCountrySvc.SelectedIndex = -1

            'ddlSalesManSvc.SelectedIndex = -1
            'ddlInchargeSvc.SelectedIndex = -1
            'ddlTermsSvc.SelectedIndex = -1
            ddlContractGrp.SelectedIndex = -1
            ddlCity.SelectedIndex = -1
            ddlCountry.SelectedIndex = -1
            ddlState.SelectedIndex = -1
            ddlLocateGrp.SelectedIndex = -1
            ddlIndustrysvc.SelectedIndex = -1

            ddlBillCitySvc.SelectedIndex = -1
            ddlBillCountrySvc.SelectedIndex = -1
            ddlBillStateSvc.SelectedIndex = -1

            ddlSalesManSvc.SelectedIndex = -1
            ddlInchargeSvc.SelectedIndex = -1
            ddlTermsSvc.SelectedIndex = -1
            ddlCompanyGrpD.SelectedIndex = -1
            ddlContractGroupEdit.SelectedIndex = -1
            'ddlContractGrp.SelectedIndex = -1

            'MakeMeNull()
            'MakeSvcNull()
            'txtRcno.Text = Session("rcno")
            'ddlCompanyGrp.SelectedIndex = -1
            'ddlIndustry.SelectedIndex = -1
            'ddlSalesMan.SelectedIndex = -1
            'ddlIncharge.SelectedIndex = -1
            'ddlTerms.SelectedIndex = -1
            'ddlOffCity.SelectedIndex = -1
            'ddlOffCountry.SelectedIndex = -1
            'ddlOffState.SelectedIndex = -1
            'ddlBillCity.SelectedIndex = -1
            'ddlBillCountry.SelectedIndex = -1
            'ddlBillState.SelectedIndex = -1
            Session.Remove("servicefrom")
        ElseIf (Session("contractfrom")) = "clients" Then

            'MakeMeNull()
            'MakeSvcNull()
            MakeSvcNull()
            txtSvcRcno.Text = Session("rcnoDetail")

            MakeSvcNull()
            txtSvcRcno.Text = Session("rcnoDetail")

            ddlCity.SelectedIndex = -1
            ddlState.SelectedIndex = -1
            ddlCountry.SelectedIndex = -1
            ddlLocateGrp.SelectedIndex = -1

            ddlCompanyGrp.SelectedIndex = -1
            ddlIndustry.SelectedIndex = -1
            ddlSalesMan.SelectedIndex = -1
            ddlIncharge.SelectedIndex = -1
            ddlTerms.SelectedIndex = -1
            ddlCurrency.SelectedIndex = -1
            ddlOffCity.SelectedIndex = -1
            ddlOffCountry.SelectedIndex = -1
            ddlOffState.SelectedIndex = -1

            ddlCurrencyEdit.SelectedIndex = -1
            ddlDefaultInvoiceFormatEdit.SelectedIndex = -1
            ddlTermsEdit.SelectedIndex = -1


            'ddlBillCitySvc.SelectedIndex = -1
            'ddlBillStateSvc.SelectedIndex = -1
            'ddlBillCountrySvc.SelectedIndex = -1

            ddlCity.SelectedIndex = -1
            ddlCountry.SelectedIndex = -1
            ddlState.SelectedIndex = -1
            ddlLocateGrp.SelectedIndex = -1
            ddlIndustrysvc.SelectedIndex = -1

            ddlBillCitySvc.SelectedIndex = -1
            ddlBillCountrySvc.SelectedIndex = -1
            ddlBillStateSvc.SelectedIndex = -1

            ddlSalesManSvc.SelectedIndex = -1
            ddlInchargeSvc.SelectedIndex = -1
            ddlTermsSvc.SelectedIndex = -1
            ddlContractGrp.SelectedIndex = -1
            ddlCompanyGrpD.SelectedIndex = -1
            ddlBranch.SelectedIndex = -1
            ddlContractGroupEdit.SelectedIndex = -1

            Session.Remove("contractfrom")
            'Dim editindex As Integer = GridView1.SelectedIndex
            'rcno = DirectCast(GridView1.Rows(editindex).FindControl("Label1"), Label).Text
            'txtRcno.Text = rcno.ToString()
        Else



            Dim editindex As Integer = GridView2.SelectedIndex
            'svcrcno = DirectCast(GridView2.Rows(editindex).FindControl("Label1"), Label).Text
            'txtSvcRcno.Text = svcrcno.ToString()


            'Dim editindex As Integer

            'If String.IsNullOrEmpty(txtSelectedIndex.Text) = False Then
            '    editindex = txtSelectedIndex.Text
            'Else
            '    'editindex = GridView2.SelectedIndex
            '    svcrcno = DirectCast(GridView2.Rows(editindex).FindControl("Label1"), Label).Text
            '    txtSvcRcno.Text = svcrcno.ToString()
            'End If

            If String.IsNullOrEmpty(txtSelectedIndex.Text) = False Then
                editindex = txtSelectedIndex.Text
                svcrcno = 0
                txtSvcRcno.Text = svcrcno.ToString()
            Else
                editindex = GridView2.SelectedIndex
            End If

            If editindex < 0 Then
                btnSvcAdd.Enabled = True
                btnSvcAdd.ForeColor = System.Drawing.Color.Black
                Exit Sub

                'btnSvcEdit.Enabled = True
                'btnSvcDelete.Enabled = True
            End If

            MakeSvcNull()
            svcrcno = DirectCast(GridView2.Rows(editindex).FindControl("Label1"), Label).Text
            txtSvcRcno.Text = svcrcno.ToString()
        End If


        '''''''''''''''''''''''''''''''


        'MakeSvcNull()

        'Dim editindex As Integer = GridView2.SelectedIndex
        'svcrcno = DirectCast(GridView2.Rows(editindex).FindControl("Label1"), Label).Text
        'txtSvcRcno.Text = svcrcno.ToString()
        Try
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()
            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.CommandText = "SELECT * FROM tblcompanylocation where rcno=" & Convert.ToInt32(txtSvcRcno.Text)
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New System.Data.DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then
                txtAccountID.Text = dt.Rows(0)("AccountID").ToString
                txtAccountIDtab2.Text = txtAccountID.Text
                lblAccountID.Text = txtAccountID.Text
                txtLocationID.Text = dt.Rows(0)("locationID").ToString

                txtAccountIdSelectedSvc.Text = txtAccountID.Text
                txtLocationIDSelectedsVC.Text = txtLocationID.Text

                txtServiceName.Text = dt.Rows(0)("ServiceName").ToString
                txtDescription.Text = dt.Rows(0)("Description").ToString
                txtAddress.Text = dt.Rows(0)("Address1").ToString
                txtStreet.Text = dt.Rows(0)("AddStreet").ToString
                txtBuilding.Text = dt.Rows(0)("AddBuilding").ToString

                txtClientID.Text = dt.Rows(0)("CompanyID").ToString
                If dt.Rows(0)("AddCity").ToString <> "" Then
                    ddlCity.Text = dt.Rows(0)("AddCity").ToString
                End If

                'txtStreet.Text = dt.Rows(0)("AddCountry").ToString

                If dt.Rows(0)("AddCountry").ToString <> "" Then
                    If Server.HtmlDecode(dt.Rows(0)("AddCountry")).ToString = "S'pore" Or Server.HtmlDecode(dt.Rows(0)("AddCountry")).ToString = "S'PORE" Then
                        ddlCountry.Text = "SINGAPORE"
                    Else
                        ddlCountry.Text = dt.Rows(0)("AddCountry").ToString
                    End If
                End If
                If dt.Rows(0)("AddState").ToString <> "" Then
                    ddlState.Text = dt.Rows(0)("AddState").ToString
                End If
                txtPostal.Text = dt.Rows(0)("AddPostal").ToString
                If dt.Rows(0)("LocateGrp").ToString <> "" Then
                    ddlLocateGrp.Text = dt.Rows(0)("LocateGrp").ToString
                End If
                If dt.Rows(0)("ServiceAddress").ToString = "1" Then
                    chkSameAddr.Checked = True
                Else
                    chkSameAddr.Checked = False
                End If
                txtLocationPrefix.Text = dt.Rows(0)("LocationPrefix").ToString
                txtLocatonNo.Text = dt.Rows(0)("LocationNo").ToString

                txtSvcCP1Contact.Text = dt.Rows(0)("ContactPerson").ToString
                txtSvcCP1Position.Text = dt.Rows(0)("Contact1Position").ToString
                txtSvcCP1Telephone.Text = dt.Rows(0)("Telephone").ToString
                txtSvcCP1Fax.Text = dt.Rows(0)("Fax").ToString
                txtSvcCP1Telephone2.Text = dt.Rows(0)("Telephone2").ToString
                txtSvcCP1Mobile.Text = dt.Rows(0)("Mobile").ToString
                txtSvcCP1Email.Text = dt.Rows(0)("Email").ToString

                txtSvcCP2Contact.Text = dt.Rows(0)("ContactPerson2").ToString
                txtSvcCP2Position.Text = dt.Rows(0)("Contact2Position").ToString
                txtSvcCP2Telephone.Text = dt.Rows(0)("Contact2Tel").ToString
                txtSvcCP2Fax.Text = dt.Rows(0)("Contact2Fax").ToString
                txtSvcCP2Tel2.Text = dt.Rows(0)("Contact2Tel2").ToString
                txtSvcCP2Mobile.Text = dt.Rows(0)("Contact2Mobile").ToString
                txtSvcCP2Email.Text = dt.Rows(0)("Contact2Email").ToString
                txtServiceLocationGroup.Text = dt.Rows(0)("ServiceLocationGroup").ToString


                btnSvcContract.Enabled = True
                btnSvcContract.ForeColor = System.Drawing.Color.Black
                btnSvcService.Enabled = True
                btnSvcService.ForeColor = System.Drawing.Color.Black
                If chkInactive.Checked = False Then
                    btnTransfersSvc.Enabled = True
                    btnTransfersSvc.ForeColor = System.Drawing.Color.Black

                    btnSpecificLocation.Enabled = True
                    btnSpecificLocation.ForeColor = System.Drawing.Color.Black
                End If

                txtBillingNameSvc.Text = dt.Rows(0)("BillingNameSvc").ToString
                txtBillAddressSvc.Text = dt.Rows(0)("BillAddressSvc").ToString
                txtBillStreetSvc.Text = dt.Rows(0)("BillStreetSvc").ToString
                txtBillBuildingSvc.Text = dt.Rows(0)("BillBuildingSvc").ToString

                Dim gCity As String
                If dt.Rows(0)("BillCitySvc").ToString <> "" Then

                    gCity = dt.Rows(0)("BillCitySvc").ToString.ToUpper()

                    If ddlBillCitySvc.Items.FindByValue(gCity) Is Nothing Then
                        ddlBillCitySvc.Items.Add(gCity)
                        ddlBillCitySvc.Text = gCity
                    Else
                        ddlBillCitySvc.Text = dt.Rows(0)("BillCitySvc").ToString.Trim().ToUpper()
                    End If
                End If

                Dim gState As String
                If dt.Rows(0)("BillStateSvc").ToString <> "" Then
                    gState = dt.Rows(0)("BillStateSvc").ToString.ToUpper()

                    If ddlBillStateSvc.Items.FindByValue(gState) Is Nothing Then
                        ddlBillStateSvc.Items.Add(gState)
                        ddlBillStateSvc.Text = gState
                    Else
                        ddlBillStateSvc.Text = dt.Rows(0)("BillStateSvc").ToString.Trim().ToUpper()
                    End If
                End If

                Dim gCountry As String
                If dt.Rows(0)("BillCountrySvc").ToString <> "" Then


                    gCountry = dt.Rows(0)("BillCountrySvc").ToString.ToUpper()

                    If ddlBillCountrySvc.Items.FindByValue(gCountry) Is Nothing Then
                        ddlBillCountrySvc.Items.Add(gCountry)
                        ddlBillCountrySvc.Text = gCountry
                    Else
                        ddlBillCountrySvc.Text = dt.Rows(0)("BillCountrySvc").ToString.Trim().ToUpper()
                    End If
                End If


                'If dt.Rows(0)("BillCitySvc").ToString <> "" Then
                '    ddlBillCitySvc.Text = dt.Rows(0)("BillCitySvc").ToString
                'End If
                'If dt.Rows(0)("BillCitySvc").ToString <> "" Then
                '    ddlBillCitySvc.Text = dt.Rows(0)("BillCitySvc").ToString
                'End If

                'If dt.Rows(0)("BillStateSvc").ToString <> "" Then
                '    ddlBillStateSvc.Text = dt.Rows(0)("BillStateSvc").ToString
                'End If

                'If dt.Rows(0)("BillCountrySvc").ToString <> "" Then
                '    ddlBillCountrySvc.Text = dt.Rows(0)("BillCountrySvc").ToString
                'End If
                txtBillPostalSvc.Text = dt.Rows(0)("BillPostalSvc").ToString
                txtBillContact1Svc.Text = dt.Rows(0)("BillContact1Svc").ToString
                txtBillPosition1Svc.Text = dt.Rows(0)("BillPosition1Svc").ToString
                txtBillTelephone1Svc.Text = dt.Rows(0)("BillTelephone1Svc").ToString
                txtBillFax1Svc.Text = dt.Rows(0)("BillFax1Svc").ToString
                txtBilltelephone12Svc.Text = dt.Rows(0)("Billtelephone12Svc").ToString
                txtBillMobile1Svc.Text = dt.Rows(0)("BillMobile1Svc").ToString
                txtBillEmail1Svc.Text = dt.Rows(0)("BillEmail1Svc").ToString
                txtBillContact2Svc.Text = dt.Rows(0)("BillContact2Svc").ToString
                txtBillPosition2Svc.Text = dt.Rows(0)("BillPosition2Svc").ToString
                txtBillTelephone2Svc.Text = dt.Rows(0)("BillTelephone2Svc").ToString
                txtBillFax2Svc.Text = dt.Rows(0)("BillFax2Svc").ToString
                txtBilltelephone22Svc.Text = dt.Rows(0)("Billtelephone22Svc").ToString
                txtBillMobile2Svc.Text = dt.Rows(0)("BillMobile2Svc").ToString
                txtBillEmail2Svc.Text = dt.Rows(0)("BillEmail2Svc").ToString

                'If String.IsNullOrEmpty(dt.Rows(0)("SalesManSvc").ToString) = True Then
                '    ddlSalesManSvc.SelectedIndex = 0
                'Else
                '    ddlSalesManSvc.Text = dt.Rows(0)("SalesManSvc").ToString
                'End If

                If String.IsNullOrEmpty(dt.Rows(0)("SalesManSvc").ToString) = True Then
                    ddlSalesManSvc.SelectedIndex = 0
                Else
                    gSalesman = dt.Rows(0)("SalesManSvc").ToString.ToUpper()

                    If ddlSalesManSvc.Items.FindByValue(gSalesman) Is Nothing Then
                        ddlSalesManSvc.Items.Add(gSalesman)
                        ddlSalesManSvc.Text = gSalesman
                    Else
                        ddlSalesManSvc.Text = dt.Rows(0)("SalesManSvc").ToString.Trim().ToUpper()
                    End If
                End If


                'If String.IsNullOrEmpty(dt.Rows(0)("InchargeIdSvc").ToString) = True Then
                '    ddlInchargeSvc.SelectedIndex = 0
                'Else
                '    ddlInchargeSvc.Text = dt.Rows(0)("InchargeIdSvc").ToString
                'End If


                If String.IsNullOrEmpty(dt.Rows(0)("InchargeIdSvc").ToString) = True Then
                    ddlInchargeSvc.SelectedIndex = 0
                Else
                    gIncharge = dt.Rows(0)("InchargeIdSvc").ToString.ToUpper()

                    If ddlInchargeSvc.Items.FindByValue(gIncharge) Is Nothing Then
                        ddlInchargeSvc.Items.Add(gIncharge)
                        ddlInchargeSvc.Text = gIncharge
                    Else
                        ddlInchargeSvc.Text = dt.Rows(0)("InchargeIdSvc").ToString.Trim().ToUpper()
                    End If
                End If

                'If String.IsNullOrEmpty(dt.Rows(0)("ArTermSvc").ToString) = True Then
                '    ddlTermsSvc.SelectedIndex = 0
                'Else
                '    ddlTermsSvc.Text = dt.Rows(0)("ArTermSvc").ToString
                'End If


                If String.IsNullOrEmpty(dt.Rows(0)("ArTermSvc").ToString) = True Then
                    ddlTermsSvc.SelectedIndex = 0
                Else
                    gIncharge = dt.Rows(0)("ArTermSvc").ToString.ToUpper()

                    If ddlTermsSvc.Items.FindByValue(gIncharge) Is Nothing Then
                        ddlTermsSvc.Items.Add(gIncharge)
                        ddlTermsSvc.Text = gIncharge
                    Else
                        ddlTermsSvc.Text = dt.Rows(0)("ArTermSvc").ToString.Trim().ToUpper()
                    End If
                End If


                If (dt.Rows(0)("SendServiceReportTo1").ToString) = "Y" Then
                    chkServiceReportSendTo1.Checked = True
                Else
                    chkServiceReportSendTo1.Checked = False
                End If

                If (dt.Rows(0)("SendServiceReportTo2").ToString) = "Y" Then
                    chkServiceReportSendTo2.Checked = True
                Else
                    chkServiceReportSendTo2.Checked = False
                End If

                If dt.Rows(0)("MandatoryServiceReportPhotos").ToString = "" Then
                    chkSvcPhotosMandatory.Checked = False
                Else
                    chkSvcPhotosMandatory.Checked = dt.Rows(0)("MandatoryServiceReportPhotos").ToString

                End If

                'If (dt.Rows(0)("MandatoryServiceReportPhotos").ToString) = "Y" Then
                '    chkSvcPhotosMandatory.Checked = True
                'Else
                '    chkSvcPhotosMandatory.Checked = False
                'End If
                'If String.IsNullOrEmpty(dt.Rows(0)("Industry").ToString) = True Then
                '    ddlIndustrysvc.SelectedIndex = 0
                'Else
                '    ddlIndustrysvc.Text = dt.Rows(0)("Industry").ToString
                'End If


                If String.IsNullOrEmpty(dt.Rows(0)("Industry").ToString) = True Then
                    ddlInchargeSvc.SelectedIndex = 0
                Else
                    gIncharge = dt.Rows(0)("Industry").ToString.ToUpper()

                    If ddlIndustrysvc.Items.FindByValue(gIncharge) Is Nothing Then
                        ddlIndustrysvc.Items.Add(gIncharge)
                        ddlIndustrysvc.Text = gIncharge
                    Else
                        ddlIndustrysvc.Text = dt.Rows(0)("Industry").ToString.Trim().ToUpper()
                    End If
                End If

                txtMarketSegmentIDsvc.Text = dt.Rows(0)("MarketSegmentID").ToString()

                If String.IsNullOrEmpty(dt.Rows(0)("ContractGroup").ToString.Trim) = False Then

                    'Start:Retrive Contract Group Description
                    Dim commandContractGrp As MySqlCommand = New MySqlCommand

                    commandContractGrp.CommandType = CommandType.Text
                    commandContractGrp.CommandText = "SELECT GroupDescription FROM tblContractGroup where ContractGroup ='" & dt.Rows(0)("ContractGroup").ToString & "'"
                    commandContractGrp.Connection = conn

                    Dim drContractGrp As MySqlDataReader = commandContractGrp.ExecuteReader()
                    Dim dtContractGrp As New DataTable
                    dtContractGrp.Load(drContractGrp)

                    If dtContractGrp.Rows.Count > 0 Then
                        ddlContractGrp.Text = dt.Rows(0)("ContractGroup").ToString & " : " & dtContractGrp.Rows(0)("GroupDescription").ToString
                        ddlContractGroupEdit.Text = dt.Rows(0)("ContractGroup").ToString & " : " & dtContractGrp.Rows(0)("GroupDescription").ToString
                    End If

                    'End:Retrieve Contract Group Description

                    'ddlContractGrp.Text = dt.Rows(0)("ContractGroup").ToString
                    'ddlContractGroupEdit.Text = dt.Rows(0)("ContractGroup").ToString

                    'Query = "SELECT CONCAT(ContractGroup, ' : ', GroupDescription) AS ContractGroup FROM tblcontractgroup order by ContractGroup"
                Else
                    ddlContractGrp.SelectedIndex = 0
                    ddlContractGroupEdit.SelectedIndex = 0
                End If

                ' txtServiceLocationGroup.Text = dt.Rows(0)("AccountNo").ToString()

                txtCommentsSvc.Text = dt.Rows(0)("Comments").ToString()

                If String.IsNullOrEmpty(dt.Rows(0)("CompanyGroupD").ToString.Trim) = False Then
                    ddlCompanyGrpD.Text = dt.Rows(0)("CompanyGroupD").ToString
                Else
                    ddlCompanyGrpD.SelectedIndex = 0
                End If

                'ddlCompanyGrpD.Text = dt.Rows(0)("CompanyGrpD").ToString()
                chkInactiveD.Checked = dt.Rows(0)("InactiveD").ToString()

                chkStatusLoc.Checked = dt.Rows(0)("Status").ToString()

                txtSvcEmailCC.Text = dt.Rows(0)("ServiceEmailCC").ToString()

                chkSendEmailNotificationOnly.Checked = dt.Rows(0)("EmailServiceNotificationOnly").ToString()

                chkSmartCustomer.Checked = dt.Rows(0)("SmartCustomer").ToString()

                txtBusinessHoursStart.Text = dt.Rows(0)("BusinessHoursStart").ToString()
                txtBusinessHoursEnd.Text = dt.Rows(0)("BusinessHoursEnd").ToString()
                chkExcludePIRDatainBusinessHours.Checked = dt.Rows(0)("ExcludePIRDataDuringBusinessHours").ToString()


                If String.IsNullOrEmpty(dt.Rows(0)("DefaultInvoiceFormat").ToString.Trim) = False Then
                    ddlSvcDefaultInvoiceFormat.Text = dt.Rows(0)("DefaultInvoiceFormat").ToString
                Else
                    ddlSvcDefaultInvoiceFormat.SelectedIndex = 0
                End If

                If String.IsNullOrEmpty(dt.Rows(0)("Location").ToString.Trim) = False Then
                    ddlBranch.Text = dt.Rows(0)("Location").ToString
                Else
                    ddlBranch.SelectedIndex = 0
                End If

                ''''''''''''''''''''''''''''''''''''''''
                'Start:Retrive Service Records
                Dim commandService As MySqlCommand = New MySqlCommand

                commandService.CommandType = CommandType.Text
                commandService.CommandText = "SELECT count(SpecificlocationName) as totSpecificlocation FROM tblcompanylocationspecificlocation where LocationId ='" & txtLocationID.Text & "'"
                commandService.Connection = conn

                Dim drService As MySqlDataReader = commandService.ExecuteReader()
                Dim dtService As New DataTable
                dtService.Load(drService)

                If dtService.Rows.Count > 0 Then
                    btnSpecificLocation.Text = "SPECIFIC LOCATION [" + Val(dtService.Rows(0)("totSpecificlocation").ToString).ToString + "]"

                End If

                If Session("SecGroupAuthority") = "ADMINISTRATOR" Then
                    btnEditContractGroup.Visible = True
                Else
                    AccessControl()
                End If

                'End:Retrieve Service Records
                ''''''''''''''''''''''''''

                '' Hide btnEditContractGroup if record exists in Contract for the Contract Goup and Location
                Dim command2 As MySqlCommand = New MySqlCommand

                Dim conn2 As MySqlConnection = New MySqlConnection()
                conn2.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn2.Open()

                command2.CommandType = CommandType.Text
                command2.CommandText = "SELECT a.ContractNo FROM tblContract a, tblContractDet b where a.ContractNo = b.ContractNo and a.ContractGroup = '" & ddlContractGrp.Text & "' and b.LocationID = '" & txtLocationID.Text & "' limit 1"
                command2.Connection = conn2

                Dim dr2 As MySqlDataReader = command2.ExecuteReader()
                Dim dt2 As New System.Data.DataTable
                dt2.Load(dr2)

                If dt2.Rows.Count > 0 Then
                    btnEditContractGroup.Visible = False
                End If


                txtOldSalesman.Text = dt.Rows(0)("SalesManSvc").ToString.Trim().ToUpper()
                txtLocationIDEditSalesman.Text = dt.Rows(0)("LocationID").ToString.Trim().ToUpper()
                txtCustomerNameEditSalesman.Text = dt.Rows(0)("ServiceName").ToString.Trim().ToUpper()
                txtServiceAddressEditSalesman.Text = dt.Rows(0)("Address1").ToString.Trim().ToUpper()

                txtSiteName.Text = dt.Rows(0)("SiteName").ToString.Trim().ToUpper()

                If String.IsNullOrEmpty(dt.Rows(0)("AddStreet").ToString.Trim().ToUpper()) = False Then
                    txtServiceAddressEditSalesman.Text = txtServiceAddressEditSalesman.Text & ", " & (dt.Rows(0)("AddStreet").ToString.Trim().ToUpper())
                End If

                If String.IsNullOrEmpty(dt.Rows(0)("AddBuilding").ToString.Trim().ToUpper()) = False Then
                    txtServiceAddressEditSalesman.Text = txtServiceAddressEditSalesman.Text & ", " & (dt.Rows(0)("AddBuilding").ToString.Trim().ToUpper())
                End If

                If String.IsNullOrEmpty(dt.Rows(0)("AddCity").ToString.Trim().ToUpper()) = False Then
                    txtServiceAddressEditSalesman.Text = txtServiceAddressEditSalesman.Text & ", " & (dt.Rows(0)("AddCity").ToString.Trim().ToUpper())
                End If

                If String.IsNullOrEmpty(dt.Rows(0)("AddState").ToString.Trim().ToUpper()) = False Then
                    txtServiceAddressEditSalesman.Text = txtServiceAddressEditSalesman.Text & ", " & (dt.Rows(0)("AddState").ToString.Trim().ToUpper())
                End If

                If String.IsNullOrEmpty(dt.Rows(0)("AddCountry").ToString.Trim().ToUpper()) = False Then
                    txtServiceAddressEditSalesman.Text = txtServiceAddressEditSalesman.Text & ", " & (dt.Rows(0)("AddCountry").ToString.Trim().ToUpper())
                End If

                If String.IsNullOrEmpty(dt.Rows(0)("AddPostal").ToString.Trim().ToUpper()) = False Then
                    txtServiceAddressEditSalesman.Text = txtServiceAddressEditSalesman.Text & ", " & (dt.Rows(0)("AddPostal").ToString.Trim().ToUpper())
                End If

                txtServiceZone.Text = dt.Rows(0)("ServiceZone").ToString
                txtServiceArea.Text = dt.Rows(0)("ServiceArea").ToString
                conn2.Close()
                dt2.Dispose()
                dr2.Close()
                '' Hide btnEditContractGroup if record exists in Contract for the Contract Goup and Location



                'Start:Retrive Service Records
                Dim commandServiceLocation As MySqlCommand = New MySqlCommand

                commandServiceLocation.CommandType = CommandType.Text
                commandServiceLocation.CommandText = "SELECT count(RecordNo) as totServiceLocationID FROM tblserviceRecord where LocationId ='" & txtLocationID.Text & "' and Status in ('O','P','H')"
                commandServiceLocation.Connection = conn

                Dim drServiceLocation As MySqlDataReader = commandServiceLocation.ExecuteReader()
                Dim dtServiceLocation As New DataTable
                dtServiceLocation.Load(drServiceLocation)

                If dtServiceLocation.Rows.Count > 0 Then
                    btnSvcService.Text = "SERVICE [" + Val(dtServiceLocation.Rows(0)("totServiceLocationID").ToString).ToString + "]"

                End If


                'End:Retrieve Service Records


                'Start:Retrive Contract Records
                Dim commandContractLocation As MySqlCommand = New MySqlCommand

                commandContractLocation.CommandType = CommandType.Text
                commandContractLocation.CommandText = "SELECT count(distinct(ContractNo)) as totContractLocationID FROM tblContractDet where LocationId ='" & txtLocationID.Text & "'"
                commandContractLocation.Connection = conn

                Dim drContractLocation As MySqlDataReader = commandContractLocation.ExecuteReader()
                Dim dtContractLocation As New DataTable
                dtContractLocation.Load(drContractLocation)

                If dtContractLocation.Rows.Count > 0 Then
                    btnSvcContract.Text = "CONTRACT [" + Val(dtContractLocation.Rows(0)("totContractLocationID").ToString).ToString + "]"

                End If

                'End:Retrieve Contract Records

                ''23.02.21

                'Dim conn As MySqlConnection = New MySqlConnection()

                'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                'conn.Open()

                lblNextScheduledService.Text = ""
                lblLastServiceDone.Text = ""

                Dim commandMaxDateService As MySqlCommand = New MySqlCommand

                commandMaxDateService.CommandType = CommandType.Text
                commandMaxDateService.CommandText = "SELECT max(ServiceDate) as MaxServiceDate from tblServiceRecord where LocationId= '" & txtLocationID.Text & "' and Status ='P'"
                commandMaxDateService.Connection = conn

                Dim drMaxDateService As MySqlDataReader = commandMaxDateService.ExecuteReader()
                Dim dtMaxDateService As New DataTable
                dtMaxDateService.Load(drMaxDateService)

                'If IsDate(Convert.ToDateTime(dtMaxDateService.Rows(0)("MaxServiceDate")).ToString("yyyy-MM-dd")) = True Then
                If IsDBNull((dtMaxDateService.Rows(0)("MaxServiceDate"))) = False Then

                    lblLastServiceDone.Text = Convert.ToDateTime(dtMaxDateService.Rows(0)("MaxServiceDate")).ToString("dd/MM/yyyy")

                End If


                Dim commandMinDateService As MySqlCommand = New MySqlCommand

                commandMinDateService.CommandType = CommandType.Text
                commandMinDateService.CommandText = "SELECT min(ServiceDate) as MinServiceDate from tblServiceRecord where LocationId= '" & txtLocationID.Text & "' and Status ='O' and ServiceDate >= '" & DateTime.Now.ToString("yyyy-MM-dd") & "'"
                commandMinDateService.Connection = conn

                Dim drMinDateService As MySqlDataReader = commandMinDateService.ExecuteReader()
                Dim dtMinDateService As New DataTable
                dtMinDateService.Load(drMinDateService)

                'If IsDate(Convert.ToDateTime(dtMaxDateService.Rows(0)("MaxServiceDate")).ToString("yyyy-MM-dd")) = True Then
                If IsDBNull((dtMinDateService.Rows(0)("MinServiceDate"))) = False Then

                    lblNextScheduledService.Text = Convert.ToDateTime(dtMinDateService.Rows(0)("MinServiceDate")).ToString("dd/MM/yyyy")

                End If
                'conn.Close()
                'conn.Dispose()
                commandMaxDateService.Dispose()
                dtMaxDateService.Dispose()
                dtMaxDateService.Dispose()

                commandMinDateService.Dispose()
                dtMinDateService.Dispose()
                dtMinDateService.Dispose()
                ''23.02.21
            End If

            conn.Close()
            conn.Dispose()
            command1.Dispose()
            dt.Dispose()
            dr.Close()

        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "GRIDVIEW2_SELECTEDINDEXCHANGED", ex.Message.ToString, txtSvcRcno.Text)
        End Try

        AddrConcat()

        '   txtSvcMode.Text = "EDIT"
        '    DisableSvcControls()
        EnableSvcControls()



        btnSvcContract.Enabled = True
        btnSvcService.Enabled = True
        btnSvcEdit.Enabled = True
        btnSvcCopy.Enabled = True
        btnSvcDelete.Enabled = True


        btnSvcContract.ForeColor = System.Drawing.Color.Black
        btnSvcService.ForeColor = System.Drawing.Color.Black
        btnSvcEdit.ForeColor = System.Drawing.Color.Black
        btnSvcCopy.ForeColor = System.Drawing.Color.Black
        btnSvcDelete.ForeColor = System.Drawing.Color.Black

        If chkInactive.Checked = False Then
            btnTransfersSvc.Enabled = True
            btnTransfersSvc.ForeColor = System.Drawing.Color.Black
        End If

        'btnTransactionsSvc.Enabled = True
        'btnTransactionsSvc.ForeColor = System.Drawing.Color.Black

        If chkSmartCustomer.Checked = True Then
            btnTransfersSvc.Enabled = False
            btnTransfersSvc.ForeColor = System.Drawing.Color.Gray
        End If
        If chkInactive.Checked = False Then
            btnSpecificLocation.Enabled = True
            btnSpecificLocation.ForeColor = System.Drawing.Color.Black
        End If

        If chkInactive.Checked = True Then

            btnSvcAdd.Enabled = False
            btnSvcAdd.ForeColor = System.Drawing.Color.Gray
            btnSvcEdit.Enabled = False
            btnSvcEdit.ForeColor = System.Drawing.Color.Gray
            btnSvcCopy.Enabled = False
            btnSvcCopy.ForeColor = System.Drawing.Color.Gray

            btnSvcDelete.Enabled = False
            btnSvcDelete.ForeColor = System.Drawing.Color.Gray

            btnChangeStatusLoc.Enabled = False
            btnChangeStatusLoc.ForeColor = System.Drawing.Color.Gray

            btnSpecificLocation.Enabled = False
            btnSpecificLocation.ForeColor = System.Drawing.Color.Gray
            btnTransfersSvc.Enabled = False
            btnTransfersSvc.ForeColor = System.Drawing.Color.Gray

            btnEditContractGroup.Visible = False
            btnEditSalesman.Visible = False
        End If

        AccessControl()

        If chkSmartCustomer.Checked = True Then
            btnTransfersSvc.Enabled = False
            btnTransfersSvc.ForeColor = System.Drawing.Color.Gray
        End If

        'btnSvcDelete.Enabled = True
        'btnSvcDelete.ForeColor = System.Drawing.Color.Black
        'btnSvcEdit.Enabled = True
        'btnSvcEdit.ForeColor = System.Drawing.Color.Black

    End Sub


    Protected Sub FindMarketSegmentID()
        Try


            Dim connBillingDetails As MySqlConnection = New MySqlConnection()

            connBillingDetails.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            connBillingDetails.Open()

            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text
            command1.CommandText = "SELECT marketsegmentid FROM tblindustry where industry= """ & ddlIndustrysvc.Text & """"
            command1.Connection = connBillingDetails

            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New System.Data.DataTable
            dt.Load(dr)


            If dt.Rows.Count > 0 Then
                txtMarketSegmentIDsvc.Text = dt.Rows(0)("marketsegmentid").ToString
            End If
            connBillingDetails.Close()
            connBillingDetails.Dispose()
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "FindMarketSegmentID", ex.Message.ToString, ddlIndustrysvc.Text)
        End Try
    End Sub
    Protected Sub btnSvcAdd_Click(sender As Object, e As EventArgs) Handles btnSvcAdd.Click
        Try
            DisableSvcControls()

            MakeSvcNull()

            txtClientID.Text = ""
            txtAccountCode.Text = ""
            txtCreatedOn.Text = ""
            lblMessage.Text = "ACTION: ADD SERVICE LOCATION"

            btnADD.Enabled = False
            btnADD.ForeColor = System.Drawing.Color.Gray
            btnCopyAdd.Enabled = False
            btnCopyAdd.ForeColor = System.Drawing.Color.Gray
            btnDelete.Enabled = False
            btnDelete.ForeColor = System.Drawing.Color.Gray

            btnFilter.Enabled = False
            btnFilter.ForeColor = System.Drawing.Color.Gray
            btnContract.Enabled = False
            btnContract.ForeColor = System.Drawing.Color.Gray
            btnTransactions.Enabled = False
            btnTransactions.ForeColor = System.Drawing.Color.Gray
            btnChangeStatus.Enabled = False
            btnChangeStatus.ForeColor = System.Drawing.Color.Gray

            btnQuit.Enabled = False
            btnQuit.ForeColor = System.Drawing.Color.Gray
            btnSvcAdd.Enabled = False
            btnSvcAdd.ForeColor = System.Drawing.Color.Gray

            btnSvcContract.Enabled = False
            btnSvcService.Enabled = False

            btnSvcContract.ForeColor = System.Drawing.Color.Gray
            btnSvcService.ForeColor = System.Drawing.Color.Gray

            btnTransactionsSvc.Enabled = False
            btnTransactionsSvc.ForeColor = System.Drawing.Color.Gray

            btnSpecificLocation.Enabled = False
            btnSpecificLocation.ForeColor = System.Drawing.Color.Gray

            btnTransfersSvc.Enabled = False
            btnTransfersSvc.ForeColor = System.Drawing.Color.Gray

            ddlContractGroup.Enabled = False
       
            'ddlInchargeSvc.Text = ddlIncharge.Text
            ddlTermsSvc.Text = ddlTerms.Text
            ddlIndustrysvc.Text = ddlIndustry.Text


            'ddlSalesManSvc.Text = ddlSalesMan.Text
            txtBillingNameSvc.Text = txtBillingName.Text

            btnEditContractGroup.Visible = False

            FindMarketSegmentID()

            txtSvcMode.Text = "NEW"
            txtServiceName.Text = txtNameE.Text
            'chkServiceReportSendTo1.Checked = True
            'chkServiceReportSendTo2.Checked = True

            chkServiceReportSendTo1.Checked = False
            chkServiceReportSendTo2.Checked = False
            chkSvcPhotosMandatory.Checked = False

            txtServiceName.Focus()
            'btnSvcSave.Enabled = True
            'btnSvcSave.ForeColor = System.Drawing.Color.Black
            'btnSvcCancel.Enabled = True
            'btnSvcCancel.ForeColor = System.Drawing.Color.Black

            ddlSalesManSvc.SelectedIndex = -1
            ddlSalesManSvc.Items.Clear()
            ddlSalesManSvc.Items.Add("--SELECT--")
            Dim Query As String
            Query = "SELECT StaffId FROM tblstaff where roles= 'SALES MAN' and  Status ='O' ORDER BY STAFFID"
            'Query = "SELECT StaffId FROM tblstaff where roles= 'SALES MAN'  ORDER BY STAFFID"
            'PopulateDropDownList(Query, "StaffId", "StaffId", ddlSalesMan)
            PopulateDropDownList(Query, "StaffId", "StaffId", ddlSalesManSvc)
            'PopulateDropDownList(Query, "StaffId", "StaffId", ddlSearchSalesman)
            chkStatusLoc.Checked = True

        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "btnSvcAdd_Click", ex.Message.ToString, txtAccountID.Text)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Protected Sub btnSvcSave_Click(sender As Object, e As EventArgs) Handles btnSvcSave.Click
        lblAlert.Text = ""

        If chkInactiveD.Checked = True Then

            Dim connIsLocationIDInActive As MySqlConnection = New MySqlConnection()

            connIsLocationIDInActive.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            connIsLocationIDInActive.Open()

            Dim commandIsLocationIDInActive As MySqlCommand = New MySqlCommand
            commandIsLocationIDInActive.CommandType = CommandType.Text

            'If ddlContactType.Text = "COMPANY" Then
            commandIsLocationIDInActive.CommandText = "SELECT a.ContractNo from tblContract a, tblContractDet b where a.ContractNo = b.ContractNo and ((a.Status = 'O') or (a.Status ='H')) and b.LocationID ='" & txtLocationID.Text.Trim & "' limit 1"
            'ElseIf ddlContactType.Text = "PERSON" Then
            '    commandIsLocationIDInActive.CommandText = "SELECT count(LocationID) as CountAccountId from tblPersonLocation where LocationID ='" & lblAccountIdContactLocation1.Text.Trim & "' and InActiveD=True"
            'End If

            commandIsLocationIDInActive.Connection = connIsLocationIDInActive

            Dim drIsAccountId As MySqlDataReader = commandIsLocationIDInActive.ExecuteReader()
            Dim dtIsAccountId As New DataTable
            dtIsAccountId.Load(drIsAccountId)

            If dtIsAccountId.Rows.Count > 0 Then
                If String.IsNullOrEmpty(dtIsAccountId.Rows(0)("ContractNo").ToString) = False Then
                    commandIsLocationIDInActive.Dispose()
                    connIsLocationIDInActive.Close()
                    dtIsAccountId.Dispose()
                    lblAlert.Text = "There are ACTIVE Contracts under this Location ID. To proceed in making this Location Inactive, Terminate All Active Contracts under this Location ID " & txtLocationID.Text.Trim
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                    Exit Sub
                End If


            End If
            commandIsLocationIDInActive.Dispose()
            connIsLocationIDInActive.Close()
            dtIsAccountId.Dispose()


        End If

        If ddlCompanyGrpD.Text = "-1" Then
            ' MessageBox.Message.Alert(Page, "Company Group cannot be blank!!!", "str")
            lblAlert.Text = "COMPANY GROUP CANNOT BE BLANK"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            Return
        End If


        If txtServiceName.Text = "" Then
            lblAlert.Text = "SERVICE NAME CANNOT BE BLANK"
            'txtCreatedOn.Text = ""
            txtServiceName.Focus()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            Exit Sub
        End If


        If ddlContractGrp.SelectedIndex = 0 Then
            ' MessageBox.Message.Alert(Page, "Service Name cannot be blank!!!", "str")
            lblAlert.Text = "CONTRACT GROUP CANNOT BE BLANK"
            'txtCreatedOn.Text = ""
            ddlContractGrp.Focus()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            ' Exit Sub
            Return
        End If

        If txtAddress.Text = "" Then
            lblAlert.Text = "STREET ADDRESS 1 (SERVICE ADDRESS) CANNOT BE BLANK"
            'txtCreatedOn.Text = ""
            txtAddress.Focus()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            Exit Sub

        End If

        'If txtBuilding.Text = "" Then
        '    lblAlert.Text = "BUILDING/UNIT NO. (SERVICE ADDRESS) CANNOT BE BLANK"
        '    txtBuilding.Focus()
        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)

        '    Exit Sub

        'End If

        'If ddlCity.SelectedIndex = 0 Then
        '    lblAlert.Text = "CITY (SERVICE ADDRESS) CANNOT BE BLANK"
        '    ddlCity.Focus()
        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        '    Exit Sub
        'End If

        If ddlCountry.SelectedIndex = 0 Then
            lblAlert.Text = "COUNTRY (SERVICE ADDRESS) CANNOT BE BLANK"
            'txtCreatedOn.Text = ""
            ddlCountry.Focus()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            Exit Sub
        End If


        If ddlBillCountrySvc.SelectedIndex = 0 Then
            lblAlert.Text = "COUNTRY (BILL ADDRESS) CANNOT BE BLANK"
            'txtCreatedOn.Text = ""
            ddlBillCountrySvc.Focus()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            Exit Sub
        End If
        'If txtPostal.Text = "" Then
        '    lblAlert.Text = "POSTAL (SERVICE ADDRESS) CANNOT BE BLANK"
        '    txtPostal.Focus()
        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        '    Exit Sub
        'End If

        If ddlCity.SelectedIndex = 0 Then
            lblAlert.Text = "CITY CANNOT BE BLANK"
            'txtCreatedOn.Text = ""
            ddlCity.Focus()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            Exit Sub
        End If

        If ddlIndustrysvc.SelectedIndex = 0 Then
            lblAlert.Text = "INDUSTRY CANNOT BE BLANK"
            'txtCreatedOn.Text = ""
            ddlIndustrysvc.Focus()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            Exit Sub
        End If

        If txtSvcCP1Contact.Text = "" Then
            lblAlert.Text = "CONTACT PERSON-1 CANNOT BE BLANK"
            'txtCreatedOn.Text = ""
            txtServiceName.Focus()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            Exit Sub
        End If
        If txtBillingNameSvc.Text = "" Then
            lblAlert.Text = "BILLING NAME CANNOT BE BLANK"
            'txtCreatedOn.Text = ""
            txtBillingNameSvc.Focus()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            Return
        End If

        If txtBillContact1Svc.Text = "" Then
            lblAlert.Text = "BILLING CONTACT PERSON 1 CANNOT BE BLANK"
            'txtCreatedOn.Text = ""
            txtBillContact1Svc.Focus()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            Return
        End If

        If txtDisplayRecordsLocationwise.Text = "Y" Then
            If ddlBranch.SelectedIndex = 0 Then
                lblAlert.Text = "BRANCH CANNOT BE BLANK"
                ddlBranch.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                Exit Sub
            End If
        End If


        If ddlSalesManSvc.SelectedIndex = 0 Then
            lblAlert.Text = "SALESMAN CANNOT BE BLANK"
            ddlSalesManSvc.Focus()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            Return
        End If

        If ddlTermsSvc.SelectedIndex = 0 Then
            lblAlert.Text = "TERMS CANNOT BE BLANK"
            ddlTermsSvc.Focus()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            Return
        End If

        If Len(txtSvcCP2Email.Text) > 100 Then
            lblAlert.Text = "SERVICE CONTACT EMAIL2 SHOULD BE LESS THAN 100 CHARACTERS"
            txtSvcCP2Email.Focus()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            Return
        End If

        'Postal Code Validation


        If txtPostalValidate.Text.ToUpper = "TRUE" Then

            If String.IsNullOrEmpty(txtBillPostalSvc.Text) Then
                lblAlert.Text = "SERVICE LOCATION - BILLING ADDRESS POSTAL CANNOT BE BLANK"
                txtBillPostalSvc.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                Exit Sub
            End If
        End If

        'If txtPostal.Text = "" Then
        '    MessageBox.Message.Alert(Page, "Postal Code cannot be blank!!!", "str")
        '    txtPostal.Focus()
        '    Return

        'End If
        'If txtAddress.Text = "" Then
        '    MessageBox.Message.Alert(Page, "Service address cannot be blank!!!", "str")
        '    txtAddress.Focus()
        '    Return

        'End If

        Dim lLocation As String

        If ddlBranch.SelectedIndex = 0 Then
            lLocation = ""
        Else
            lLocation = ddlBranch.Text
        End If


        Dim hyphenpos As Integer
        hyphenpos = 0
        hyphenpos = (ddlContractGrp.Text.IndexOf(":"))
        Dim lContractGroup As String

        lContractGroup = Left(ddlContractGrp.Text.Trim, (hyphenpos - 1))

        If txtSvcMode.Text = "NEW" Then
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                If txtDisplayRecordsLocationwise.Text = "Y" Then
                    command1.CommandText = "SELECT * FROM tblcompanylocation where accountid=@id and Location=@Location"
                    command1.Parameters.AddWithValue("@id", txtAccountID.Text)
                    command1.Parameters.AddWithValue("@Location", ddlBranch.Text.Trim)
                Else
                    command1.CommandText = "SELECT * FROM tblcompanylocation where accountid=@id"
                    command1.Parameters.AddWithValue("@id", txtAccountID.Text)
                End If

                'command1.CommandText = "SELECT * FROM tblcompanylocation where accountid=@id"
                'command1.Parameters.AddWithValue("@id", txtAccountID.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New System.Data.DataTable
                dt.Load(dr)

                Dim addr As String = txtAddress.Text.Trim + " " + txtStreet.Text.Trim + " " + txtBuilding.Text.Trim
                Dim dataaddr As String
                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1

                        dataaddr = dt.Rows(i)("Address1").ToString + " " + dt.Rows(i)("AddStreet").ToString + " " + dt.Rows(i)("AddBuilding").ToString

                        '  InsertIntoTblWebEventLog("COMPLOC1", "SVCSAVE", addr, dataaddr)

                        If txtDisplayRecordsLocationwise.Text = "Y" Then
                            If addr = dataaddr And dt.Rows(i)("ContractGroup").ToString.Trim = ddlContractGrp.Text.Trim And dt.Rows(i)("CompanyGroupD").ToString.Trim = ddlCompanyGrpD.Text.Trim And dt.Rows(i)("Location").ToString.Trim = ddlBranch.Text.Trim Then
                                '  MessageBox.Message.Alert(Page, "Address already exists!!!", "str")
                                'lblAlert.Text = "ADDRESS ALREADY EXISTS FOR THIS CONTRACT GROUP AND COMPANY GROUP"
                                lblAlert.Text = "THIS ADDRESS ALREADY EXISTS FOR THIS BRANCH, CONTRACT GROUP AND COMPANY GROUP"

                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                                Exit Sub
                            End If
                        Else
                            If addr = dataaddr And dt.Rows(i)("ContractGroup").ToString.Trim = ddlContractGrp.Text.Trim And dt.Rows(i)("CompanyGroupD").ToString.Trim = ddlCompanyGrpD.Text.Trim Then
                                ' MessageBox.Message.Alert(Page, "Address already exists!!!", "str")
                                'lblAlert.Text = "ADDRESS ALREADY EXISTS FOR THIS CONTRACT GROUP AND COMPANY GROUP"
                                lblAlert.Text = "THIS ADDRESS ALREADY EXISTS FOR THIS CONTRACT GROUP, AND COMPANY GROUP"
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                                Exit Sub
                            End If
                        End If

                    Next

                    Dim CustAddr As String = AddrIgnoredWords(conn, addr.ToUpper)
                    CustAddr = CustAddr.Replace("  ", " ")
                    Dim dataaddr1 As String
                    For i As Integer = 0 To dt.Rows.Count - 1
                        dataaddr = dt.Rows(i)("Address1").ToString + " " + dt.Rows(i)("AddStreet").ToString + " " + dt.Rows(i)("AddBuilding").ToString
                        dataaddr = dataaddr.Replace("  ", " ")
                        dataaddr1 = AddrIgnoredWords(conn, dataaddr.ToUpper)

                        dataaddr1 = dataaddr1.Replace("  ", " ")
                        InsertIntoTblWebEventLog("COMPLOC2", "SVCSAVE", CustAddr, dataaddr1)
                        'InsertIntoTblWebEventLog("COMPLOC2", "SVCSAVE", dt.Rows(i)("ContractGroup").ToString.Trim, ddlContractGrp.Text.Trim)
                        'InsertIntoTblWebEventLog("COMPLOC2", "SVCSAVE", dt.Rows(i)("CompanyGroupD").ToString.Trim, ddlCompanyGrpD.Text.Trim)

                        If txtDisplayRecordsLocationwise.Text = "Y" Then

                            If CustAddr = dataaddr1 And dt.Rows(i)("ContractGroup").ToString.Trim = ddlContractGrp.Text.Trim And dt.Rows(i)("CompanyGroupD").ToString.Trim = ddlCompanyGrpD.Text.Trim And dt.Rows(i)("Location").ToString.Trim = ddlBranch.Text.Trim Then
                                lblAlert.Text = "THIS ADDRESS ALREADY EXISTS FOR THIS BRANCH, CONTRACT GROUP AND COMPANY GROUP"

                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                                Exit Sub
                            End If


                        Else
                            If CustAddr = dataaddr1 And dt.Rows(i)("ContractGroup").ToString.Trim = ddlContractGrp.Text.Trim And dt.Rows(i)("CompanyGroupD").ToString.Trim = ddlCompanyGrpD.Text.Trim Then
                                ' MessageBox.Message.Alert(Page, "Address already exists!!!", "str")
                                'lblAlert.Text = "ADDRESS ALREADY EXISTS FOR THIS CONTRACT GROUP AND COMPANY GROUP"
                                lblAlert.Text = "THIS ADDRESS ALREADY EXISTS FOR THIS CONTRACT GROUP AND COMPANY GROUP"
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                                Exit Sub
                            End If
                        End If

                    Next
                End If


                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                'Dim qry As String = "INSERT INTO tblcompanylocation(CompanyID,Location,BranchID,Description,ContactPerson,Address1,Telephone,Mobile,Email,CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn,AddBlock,AddNos,AddFloor,AddUnit,AddBuilding,AddStreet,AddCity,AddState,AddCountry,AddPostal,LocateGrp,Fax,AccountID,LocationID,LocationPrefix,LocationNo,ServiceName,Contact1Position,Telephone2,ContactPerson2,Contact2Position,Contact2Tel,Contact2Fax,Contact2Tel2,Contact2Mobile,Contact2Email,ServiceAddress, ServiceLocationGroup,BillingNameSvc,BillAddressSvc,BillStreetSvc,BillBuildingSvc,BillCitySvc,BillStateSvc,BillCountrySvc,BillPostalSvc,BillContact1Svc,BillPosition1Svc,BillTelephone1Svc,BillFax1Svc,Billtelephone12Svc,BillMobile1Svc,BillEmail1Svc,BillContact2Svc,BillPosition2Svc,BillTelephone2Svc,BillFax2Svc,Billtelephone22Svc,BillMobile2Svc,BillEmail2Svc, InChargeIdSvc, ArTermSvc, SalesmanSvc, SendServiceReportTo1, SendServiceReportTo2, Industry, MarketSegmentId, ContractGroup, Comments)VALUES(@CompanyID,@Location,@BranchID,@Description,@ContactPerson,@Address1,@Telephone,@Mobile,@Email,@CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn,@AddBlock,@AddNos,@AddFloor,@AddUnit,@AddBuilding,@AddStreet,@AddCity,@AddState,@AddCountry,@AddPostal,@LocateGrp,@Fax,@AccountID,@LocationID,@LocationPrefix,@LocationNo,@ServiceName,@Contact1Position,@Tel2,@ContactPerson2,@Contact2Position,@Contact2Tel,@Contact2Fax,@Contact2Tel2,@Contact2Mobile,@Contact2Email,@ServiceAddress, @ServiceLocationGroup,@BillingNameSvc,@BillAddressSvc,@BillStreetSvc,@BillBuildingSvc,@BillCitySvc,@BillStateSvc,@BillCountrySvc,@BillPostalSvc,@BillContact1Svc,@BillPosition1Svc,@BillTelephone1Svc,@BillFax1Svc,@Billtelephone12Svc,@BillMobile1Svc,@BillEmail1Svc,@BillContact2Svc,@BillPosition2Svc,@BillTelephone2Svc,@BillFax2Svc,@Billtelephone22Svc,@BillMobile2Svc,@BillEmail2Svc, @InChargeIdSvc, @ArTermSvc, @SalesmanSvc, @SendServiceReportTo1, @SendServiceReportTo2, @Industry, @MarketSegmentId, @ContractGroup, @Comments);"
                Dim qry As String = "INSERT INTO tblcompanylocation(CompanyID,Location,BranchID,Description,ContactPerson,Address1,Telephone,Mobile,Email,CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn,AddBlock,AddNos,AddFloor,AddUnit,AddBuilding,AddStreet,AddCity,AddState,AddCountry,AddPostal,LocateGrp,Fax,AccountID,LocationID,LocationPrefix,LocationNo,ServiceName,Contact1Position,Telephone2,ContactPerson2,Contact2Position,Contact2Tel,Contact2Fax,Contact2Tel2,Contact2Mobile,Contact2Email,ServiceAddress, ServiceLocationGroup,BillingNameSvc,BillAddressSvc,BillStreetSvc,BillBuildingSvc,BillCitySvc,BillStateSvc,BillCountrySvc,BillPostalSvc,BillContact1Svc,BillPosition1Svc,BillTelephone1Svc,BillFax1Svc,Billtelephone12Svc,BillMobile1Svc,BillEmail1Svc,BillContact2Svc,BillPosition2Svc,BillTelephone2Svc,BillFax2Svc,Billtelephone22Svc,BillMobile2Svc,BillEmail2Svc, InChargeIdSvc, ArTermSvc, SalesmanSvc, SendServiceReportTo1, SendServiceReportTo2, Industry, MarketSegmentId, ContractGroup, Comments, CompanyGroupD, InActiveD, DefaultInvoiceFormat, ServiceEmailCC, EmailServiceNotificationOnly, SmartCustomer, BusinessHoursStart, BusinessHoursEnd, ExcludePIRDataDuringBusinessHours,MandatoryServiceReportPhotos, siteName, ServiceZone,ServiceArea)VALUES(@CompanyID,@Location,@BranchID,@Description,@ContactPerson,@Address1,@Telephone,@Mobile,@Email,@CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn,@AddBlock,@AddNos,@AddFloor,@AddUnit,@AddBuilding,@AddStreet,@AddCity,@AddState,@AddCountry,@AddPostal,@LocateGrp,@Fax,@AccountID,@LocationID,@LocationPrefix,@LocationNo,@ServiceName,@Contact1Position,@Tel2,@ContactPerson2,@Contact2Position,@Contact2Tel,@Contact2Fax,@Contact2Tel2,@Contact2Mobile,@Contact2Email,@ServiceAddress, @ServiceLocationGroup,@BillingNameSvc,@BillAddressSvc,@BillStreetSvc,@BillBuildingSvc,@BillCitySvc,@BillStateSvc,@BillCountrySvc,@BillPostalSvc,@BillContact1Svc,@BillPosition1Svc,@BillTelephone1Svc,@BillFax1Svc,@Billtelephone12Svc,@BillMobile1Svc,@BillEmail1Svc,@BillContact2Svc,@BillPosition2Svc,@BillTelephone2Svc,@BillFax2Svc,@Billtelephone22Svc,@BillMobile2Svc,@BillEmail2Svc, @InChargeIdSvc, @ArTermSvc, @SalesmanSvc, @SendServiceReportTo1, @SendServiceReportTo2, @Industry, @MarketSegmentId, @ContractGroup, @Comments, @CompanyGroupD, @InActiveD,  @DefaultInvoiceFormat, @ServiceEmailCC, @EmailServiceNotificationOnly, @SmartCustomer, @BusinessHoursStart, @BusinessHoursEnd, @ExcludePIRDataDuringBusinessHours,@MandatoryServiceReportPhotos, @siteName, @ServiceZone,@ServiceArea);"


                command.CommandText = qry
                command.Parameters.Clear()

                command.Parameters.AddWithValue("@CompanyID", txtClientID.Text.ToUpper)
                If ddlBranch.SelectedIndex = 0 Then
                    command.Parameters.AddWithValue("@Location", "")
                Else
                    command.Parameters.AddWithValue("@Location", ddlBranch.Text)
                End If
                command.Parameters.AddWithValue("@BranchID", "")
                command.Parameters.AddWithValue("@Description", txtDescription.Text.ToUpper)
                command.Parameters.AddWithValue("@ContactPerson", txtSvcCP1Contact.Text.ToUpper)
                command.Parameters.AddWithValue("@Address1", txtAddress.Text.ToUpper)
                command.Parameters.AddWithValue("@Telephone", txtSvcCP1Telephone.Text.ToUpper)
                command.Parameters.AddWithValue("@Mobile", txtSvcCP1Mobile.Text.ToUpper)
                command.Parameters.AddWithValue("@Email", txtSvcCP1Email.Text.ToUpper)

                command.Parameters.AddWithValue("@AddBlock", "")
                command.Parameters.AddWithValue("@AddNos", "")
                command.Parameters.AddWithValue("@AddFloor", "")
                command.Parameters.AddWithValue("@AddUnit", "")
                command.Parameters.AddWithValue("@AddBuilding", txtBuilding.Text.ToUpper)
                command.Parameters.AddWithValue("@AddStreet", txtStreet.Text.ToUpper)
                If ddlState.Text = txtDDLText.Text Then
                    command.Parameters.AddWithValue("@AddState", "")
                Else
                    command.Parameters.AddWithValue("@AddState", ddlState.Text.ToUpper)
                End If

                If ddlCity.Text = txtDDLText.Text Then
                    command.Parameters.AddWithValue("@AddCity", "")
                Else
                    command.Parameters.AddWithValue("@AddCity", ddlCity.Text.ToUpper)
                End If
                If ddlCountry.Text = txtDDLText.Text Then
                    command.Parameters.AddWithValue("@AddCountry", "")
                Else
                    command.Parameters.AddWithValue("@AddCountry", ddlCountry.Text.ToUpper)
                End If
                command.Parameters.AddWithValue("@AddPostal", txtPostal.Text.ToUpper)
                If ddlLocateGrp.Text = txtDDLText.Text Then
                    command.Parameters.AddWithValue("@LocateGRP", "")

                Else
                    command.Parameters.AddWithValue("@LocateGRP", ddlLocateGrp.Text.ToUpper)

                End If
                command.Parameters.AddWithValue("@Fax", txtSvcCP1Fax.Text.ToUpper)
                command.Parameters.AddWithValue("@AccountID", txtAccountID.Text.ToUpper)
                GenerateLocationID()

                command.Parameters.AddWithValue("@LocationID", txtLocationID.Text.ToUpper)
                command.Parameters.AddWithValue("@LocationPrefix", txtLocationPrefix.Text.ToUpper)
                command.Parameters.AddWithValue("@LocationNo", txtLocatonNo.Text.ToUpper)
                command.Parameters.AddWithValue("@ServiceName", txtServiceName.Text.ToUpper)
                command.Parameters.AddWithValue("@Contact1Position", txtSvcCP1Position.Text.ToUpper)
                command.Parameters.AddWithValue("@Tel2", txtSvcCP1Telephone2.Text.ToUpper)
                command.Parameters.AddWithValue("@ContactPerson2", txtSvcCP2Contact.Text.ToUpper)
                command.Parameters.AddWithValue("@Contact2Position", txtSvcCP2Position.Text.ToUpper)
                command.Parameters.AddWithValue("@Contact2Tel", txtSvcCP2Telephone.Text.ToUpper)
                command.Parameters.AddWithValue("@Contact2Fax", txtSvcCP2Fax.Text.ToUpper)
                command.Parameters.AddWithValue("@Contact2Tel2", txtSvcCP2Tel2.Text.ToUpper)
                command.Parameters.AddWithValue("@Contact2Mobile", txtSvcCP2Mobile.Text.ToUpper)
                command.Parameters.AddWithValue("@Contact2Email", txtSvcCP2Email.Text.ToUpper)
                command.Parameters.AddWithValue("@ServiceLocationGroup", txtServiceLocationGroup.Text.ToUpper)
                If chkSameAddr.Checked = True Then
                    command.Parameters.AddWithValue("@ServiceAddress", 1)

                Else
                    command.Parameters.AddWithValue("@ServiceAddress", 0)

                End If

                command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                command.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))

                command.Parameters.AddWithValue("@BillingNameSvc", txtBillingNameSvc.Text.ToUpper)
                command.Parameters.AddWithValue("@BillAddressSvc", txtBillAddressSvc.Text.ToUpper)
                command.Parameters.AddWithValue("@BillStreetSvc", txtBillStreetSvc.Text.ToUpper)
                command.Parameters.AddWithValue("@BillBuildingSvc", txtBillBuildingSvc.Text.ToUpper)
                If ddlBillCitySvc.Text = txtDDLText.Text Then
                    command.Parameters.AddWithValue("@BillCitySvc", "")
                Else
                    command.Parameters.AddWithValue("@BillCitySvc", ddlBillCitySvc.Text.ToUpper)
                End If
                If ddlBillStateSvc.Text = txtDDLText.Text Then
                    command.Parameters.AddWithValue("@BillStateSvc", "")
                Else
                    command.Parameters.AddWithValue("@BillStateSvc", ddlBillStateSvc.Text.ToUpper)
                End If
                If ddlBillCountrySvc.Text = txtDDLText.Text Then
                    command.Parameters.AddWithValue("@BillCountrySvc", "")
                Else
                    command.Parameters.AddWithValue("@BillCountrySvc", ddlBillCountrySvc.Text.ToUpper)
                End If
                command.Parameters.AddWithValue("@BillPostalSvc", txtBillPostalSvc.Text.ToUpper)
                command.Parameters.AddWithValue("@BillContact1Svc", txtBillContact1Svc.Text.ToUpper)
                command.Parameters.AddWithValue("@BillPosition1Svc", txtBillPosition1Svc.Text.ToUpper)
                command.Parameters.AddWithValue("@BillTelephone1Svc", txtBillTelephone1Svc.Text.ToUpper)
                command.Parameters.AddWithValue("@BillFax1Svc", txtBillFax1Svc.Text.ToUpper)
                command.Parameters.AddWithValue("@Billtelephone12Svc", txtBilltelephone12Svc.Text.ToUpper)
                command.Parameters.AddWithValue("@BillMobile1Svc", txtBillMobile1Svc.Text.ToUpper)
                command.Parameters.AddWithValue("@BillEmail1Svc", txtBillEmail1Svc.Text.ToUpper)
                command.Parameters.AddWithValue("@BillContact2Svc", txtBillContact2Svc.Text.ToUpper)
                command.Parameters.AddWithValue("@BillPosition2Svc", txtBillPosition2Svc.Text.ToUpper)
                command.Parameters.AddWithValue("@BillTelephone2Svc", txtBillTelephone2Svc.Text.ToUpper)
                command.Parameters.AddWithValue("@BillFax2Svc", txtBillFax2Svc.Text.ToUpper)
                command.Parameters.AddWithValue("@Billtelephone22Svc", txtBilltelephone22Svc.Text.ToUpper)
                command.Parameters.AddWithValue("@BillMobile2Svc", txtBillMobile2Svc.Text.ToUpper)
                command.Parameters.AddWithValue("@BillEmail2Svc", txtBillEmail2Svc.Text.ToUpper)


                If ddlSalesManSvc.SelectedIndex = 0 Then
                    command.Parameters.AddWithValue("@SalesManSvc", "")
                Else
                    command.Parameters.AddWithValue("@SalesManSvc", ddlSalesManSvc.Text.ToUpper)
                End If

                If ddlInchargeSvc.SelectedIndex = 0 Then
                    command.Parameters.AddWithValue("@InchargeIDSvc", "")
                Else
                    command.Parameters.AddWithValue("@InchargeIDSvc", ddlInchargeSvc.Text.ToUpper)
                End If

                If ddlTermsSvc.SelectedIndex = 0 Then
                    command.Parameters.AddWithValue("@ARTermSvc", "")
                Else
                    command.Parameters.AddWithValue("@ARTermSvc", ddlTermsSvc.Text.ToUpper)
                End If


                'command.Parameters.AddWithValue("@SalesManSvc", ddlSalesManSvc.Text.ToUpper)
                'command.Parameters.AddWithValue("@InchargeIDSvc", ddlInchargeSvc.Text.ToUpper)
                'command.Parameters.AddWithValue("@ARTermSvc", ddlTermsSvc.Text.ToUpper)

                If chkServiceReportSendTo1.Checked = True Then
                    command.Parameters.AddWithValue("@SendServiceReportTo1", "Y")
                Else
                    command.Parameters.AddWithValue("@SendServiceReportTo1", "N")
                End If

                If chkServiceReportSendTo2.Checked = True Then
                    command.Parameters.AddWithValue("@SendServiceReportTo2", "Y")
                Else
                    command.Parameters.AddWithValue("@SendServiceReportTo2", "N")
                End If
                command.Parameters.AddWithValue("@MandatoryServiceReportPhotos", chkSvcPhotosMandatory.Checked)

                'If chkSvcPhotosMandatory.Checked = True Then
                '    command.Parameters.AddWithValue("@MandatoryServiceReportPhotos", "Y")
                'Else
                '    command.Parameters.AddWithValue("@MandatoryServiceReportPhotos", "N")
                'End If

                command.Parameters.AddWithValue("@Industry", ddlIndustrysvc.Text.ToUpper)
                command.Parameters.AddWithValue("@MarketSegmentId", txtMarketSegmentIDsvc.Text.ToUpper)

                If ddlContractGrp.SelectedIndex = 0 Then
                    command.Parameters.AddWithValue("@ContractGroup", "")
                Else
                    'command.Parameters.AddWithValue("@ContractGroup", ddlContractGrp.SelectedValue.ToString)
                    command.Parameters.AddWithValue("@ContractGroup", lContractGroup.Trim)

                End If
                command.Parameters.AddWithValue("@Comments", txtCommentsSvc.Text.ToUpper)

                If ddlCompanyGrpD.SelectedIndex = 0 Then
                    command.Parameters.AddWithValue("@CompanyGroupD", "")
                Else
                    command.Parameters.AddWithValue("@CompanyGroupD", ddlCompanyGrpD.SelectedValue.ToString)
                End If
                'command.Parameters.AddWithValue("@CompanyGroupD", ddlCompanyGrpD.Text.ToUpper)
                command.Parameters.AddWithValue("@InActiveD", False)

                command.Parameters.AddWithValue("@ServiceEmailCC", txtSvcEmailCC.Text.ToUpper)
                command.Parameters.AddWithValue("@EmailServiceNotificationOnly", chkSendEmailNotificationOnly.Checked)


                If ddlSvcDefaultInvoiceFormat.SelectedIndex = 0 Then
                    command.Parameters.AddWithValue("@DefaultInvoiceFormat", "")
                Else
                    command.Parameters.AddWithValue("@DefaultInvoiceFormat", ddlSvcDefaultInvoiceFormat.Text)
                End If

                command.Parameters.AddWithValue("@SmartCustomer", chkSmartCustomer.Checked)

                command.Parameters.AddWithValue("@BusinessHoursStart", txtBusinessHoursStart.Text.ToUpper)
                command.Parameters.AddWithValue("@BusinessHoursEnd", txtBusinessHoursEnd.Text.ToUpper)
                command.Parameters.AddWithValue("@ExcludePIRDataDuringBusinessHours", chkExcludePIRDatainBusinessHours.Checked)

                command.Parameters.AddWithValue("@SiteName", txtSiteName.Text.ToUpper)
                command.Parameters.AddWithValue("@ServiceZone", txtServiceZone.Text.ToUpper)
                command.Parameters.AddWithValue("@ServiceArea", txtServiceArea.Text.ToUpper)


                'ddlSalesManSvc.SelectedIndex = 0
                'ddlInchargeSvc.SelectedIndex = 0
                'ddlTermsSvc.SelectedIndex = 0

                'chkServiceReportSendTo1.Checked = False
                'chkServiceReportSendTo2

                command.Connection = conn

                command.ExecuteNonQuery()
                txtSvcRcno.Text = command.LastInsertedId

             

                '   MessageBox.Message.Alert(Page, "Record added successfully!!!", "str")
                lblMessage.Text = "ADD: RECORD SUCCESSFULLY ADDED"

                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CORP", txtLocationID.Text, "ADD", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")), 0, 0, 0, txtAccountID.Text, "", txtRcno.Text)


                lblAlert.Text = ""
                conn.Close()
                conn.Dispose()
                command.Dispose()
                command1.Dispose()
                dt.Dispose()
                dr.Close()

            Catch ex As Exception
                InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "SVC ADD SAVE", ex.Message.ToString, txtAccountID.Text + " " + txtLocationID.Text)
            End Try
            EnableSvcControls()

        ElseIf txtSvcMode.Text = "EDIT" Then
            If txtSvcRcno.Text = "" Then
                '  MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
                lblAlert.Text = "SELECT RECORD TO EDIT"
                Return

            End If
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                'Dim ind As String
                'ind = txtcountry.Text
                'ind = ind.Replace("'", "\\'")

                Dim command2 As MySqlCommand = New MySqlCommand

                command2.CommandType = CommandType.Text

                If txtDisplayRecordsLocationwise.Text = "Y" Then
                    command2.CommandText = "SELECT * FROM tblcompanylocation where accountid=@id and Location=@Location and rcno<>" & txtSvcRcno.Text
                    command2.Parameters.AddWithValue("@id", txtAccountID.Text)
                    command2.Parameters.AddWithValue("@Location", ddlBranch.Text.Trim)
                Else
                    command2.CommandText = "SELECT * FROM tblcompanylocation where accountid=@id and rcno<>" & txtSvcRcno.Text
                    command2.Parameters.AddWithValue("@id", txtAccountID.Text)
                End If

                'command2.CommandText = "SELECT * FROM tblcompanylocation where accountid=@id and rcno<>" & txtSvcRcno.Text
                'command2.Parameters.AddWithValue("@id", txtAccountID.Text)
                command2.Connection = conn

                Dim dr As MySqlDataReader = command2.ExecuteReader()
                Dim dt As New System.Data.DataTable
                dt.Load(dr)

                Dim addr As String = txtAddress.Text.Trim + " " + txtStreet.Text.Trim + " " + txtBuilding.Text.Trim
                Dim dataaddr As String
                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1

                        dataaddr = dt.Rows(i)("Address1").ToString + " " + dt.Rows(i)("AddStreet").ToString + " " + dt.Rows(i)("AddBuilding").ToString

                        If txtDisplayRecordsLocationwise.Text = "Y" Then
                            If addr = dataaddr And dt.Rows(i)("ContractGroup").ToString.Trim = ddlContractGrp.Text.Trim And dt.Rows(i)("CompanyGroupD").ToString.Trim = ddlCompanyGrpD.Text.Trim And dt.Rows(i)("Location").ToString.Trim = ddlBranch.Text.Trim Then
                                '  MessageBox.Message.Alert(Page, "Address already exists!!!", "str")
                                'lblAlert.Text = "ADDRESS ALREADY EXISTS FOR THIS CONTRACT GROUP AND COMPANY GROUP"
                                lblAlert.Text = "THIS ADDRESS ALREADY EXISTS FOR THIS BRANCH, CONTRACT GROUP AND COMPANY GROUP"

                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                                Exit Sub
                            End If
                        Else
                            If addr = dataaddr And dt.Rows(i)("ContractGroup").ToString.Trim = ddlContractGrp.Text.Trim And dt.Rows(i)("CompanyGroupD").ToString.Trim = ddlCompanyGrpD.Text.Trim Then
                                '  MessageBox.Message.Alert(Page, "Address already exists!!!", "str")
                                'lblAlert.Text = "ADDRESS ALREADY EXISTS FOR THIS CONTRACT GROUP AND COMPANY GROUP"
                                lblAlert.Text = "THIS ADDRESS ALREADY EXISTS FOR THIS CONTRACT GROUP AND COMPANY GROUP"

                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                                Exit Sub
                            End If
                        End If


                    Next

                End If

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                command1.CommandText = "SELECT * FROM tblcompanylocation where rcno=" & Convert.ToInt32(txtSvcRcno.Text)
                command1.Connection = conn

                Dim dr1 As MySqlDataReader = command1.ExecuteReader()
                Dim dt1 As New System.Data.DataTable
                dt1.Load(dr1)

                If dt1.Rows.Count > 0 Then

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String

                    'qry = "UPDATE tblcompanylocation SET CompanyID = @CompanyID, Description = @Description,ContactPerson = @ContactPerson,Address1 = @Address1,Telephone = @Telephone,Mobile = @Mobile,Email = @Email, LastModifiedBy = @LastModifiedBy,LastModifiedOn = @LastModifiedOn, AddBuilding = @AddBuilding,AddStreet = @AddStreet,AddCity = @AddCity,AddState = @AddState,AddCountry = @AddCountry,AddPostal = @AddPostal,LocateGrp = @LocateGrp,Fax = @Fax,ServiceName = @ServiceName,Contact1Position = @Contact1Position,Telephone2 = @Tel2,ContactPerson2 = @ContactPerson2,Contact2Position = @Contact2Position,Contact2Tel = @Contact2Tel,Contact2Fax = @Contact2Fax,Contact2Tel2 = @Contact2Tel2,Contact2Mobile = @Contact2Mobile,Contact2Email = @Contact2Email,ServiceAddress=@ServiceAddress,  ServiceLocationGroup= @ServiceLocationGroup,BillingNameSvc = @BillingNameSvc,BillAddressSvc = @BillAddressSvc,BillStreetSvc = @BillStreetSvc,BillBuildingSvc = @BillBuildingSvc,BillCitySvc = @BillCitySvc,BillStateSvc = @BillStateSvc,BillCountrySvc = @BillCountrySvc,BillPostalSvc = @BillPostalSvc,BillContact1Svc = @BillContact1Svc,BillPosition1Svc = @BillPosition1Svc,BillTelephone1Svc = @BillTelephone1Svc,BillFax1Svc = @BillFax1Svc,Billtelephone12Svc = @Billtelephone12Svc,BillMobile1Svc = @BillMobile1Svc,BillEmail1Svc = @BillEmail1Svc,BillContact2Svc = @BillContact2Svc,BillPosition2Svc = @BillPosition2Svc,BillTelephone2Svc = @BillTelephone2Svc,BillFax2Svc = @BillFax2Svc,Billtelephone22Svc = @Billtelephone22Svc,BillMobile2Svc = @BillMobile2Svc,BillEmail2Svc = @BillEmail2Svc, InChargeIdSvc=@InChargeIdSvc, ArTermSvc=@ArTermSvc, SalesmanSvc=@SalesmanSvc, SendServiceReportTo1=@SendServiceReportTo1, SendServiceReportTo2=@SendServiceReportTo2, Industry = @Industry, MarketSegmentId=@MarketsegmentID, ContractGroup = @ContractGroup, Comments=@Comments where rcno=" & Convert.ToInt32(txtSvcRcno.Text)
                    qry = "UPDATE tblcompanylocation SET CompanyID = @CompanyID, Description = @Description,ContactPerson = @ContactPerson,Address1 = @Address1,Telephone = @Telephone,Mobile = @Mobile,Email = @Email, LastModifiedBy = @LastModifiedBy,LastModifiedOn = @LastModifiedOn, AddBuilding = @AddBuilding,AddStreet = @AddStreet,AddCity = @AddCity,AddState = @AddState,AddCountry = @AddCountry,AddPostal = @AddPostal,LocateGrp = @LocateGrp,Fax = @Fax,ServiceName = @ServiceName,Contact1Position = @Contact1Position,Telephone2 = @Tel2,ContactPerson2 = @ContactPerson2,Contact2Position = @Contact2Position,Contact2Tel = @Contact2Tel,Contact2Fax = @Contact2Fax,Contact2Tel2 = @Contact2Tel2,Contact2Mobile = @Contact2Mobile,Contact2Email = @Contact2Email,ServiceAddress=@ServiceAddress,  ServiceLocationGroup= @ServiceLocationGroup,BillingNameSvc = @BillingNameSvc,BillAddressSvc = @BillAddressSvc,BillStreetSvc = @BillStreetSvc,BillBuildingSvc = @BillBuildingSvc,BillCitySvc = @BillCitySvc,BillStateSvc = @BillStateSvc,BillCountrySvc = @BillCountrySvc,BillPostalSvc = @BillPostalSvc,BillContact1Svc = @BillContact1Svc,BillPosition1Svc = @BillPosition1Svc,BillTelephone1Svc = @BillTelephone1Svc,BillFax1Svc = @BillFax1Svc,Billtelephone12Svc = @Billtelephone12Svc,BillMobile1Svc = @BillMobile1Svc,BillEmail1Svc = @BillEmail1Svc,BillContact2Svc = @BillContact2Svc,BillPosition2Svc = @BillPosition2Svc,BillTelephone2Svc = @BillTelephone2Svc,BillFax2Svc = @BillFax2Svc,Billtelephone22Svc = @Billtelephone22Svc,BillMobile2Svc = @BillMobile2Svc,BillEmail2Svc = @BillEmail2Svc, InChargeIdSvc=@InChargeIdSvc, ArTermSvc=@ArTermSvc, SalesmanSvc=@SalesmanSvc, SendServiceReportTo1=@SendServiceReportTo1, SendServiceReportTo2=@SendServiceReportTo2, Industry = @Industry, MarketSegmentId=@MarketsegmentID, ContractGroup = @ContractGroup, Comments=@Comments, CompanyGroupD=@CompanyGroupD, InActiveD=@InActiveD,  DefaultInvoiceFormat=@DefaultInvoiceFormat, ServiceEmailCC=@ServiceEmailCC, Location=@Location, EmailServiceNotificationOnly =@EmailServiceNotificationOnly, SmartCustomer= @SmartCustomer, BusinessHoursStart =@BusinessHoursStart, BusinessHoursEnd=@BusinessHoursEnd, ExcludePIRDataDuringBusinessHours=@ExcludePIRDataDuringBusinessHours,MandatoryServiceReportPhotos=@MandatoryServiceReportPhotos, siteName= @siteName,  ServiceZone = @ServiceZone,ServiceArea=@ServiceArea where rcno=" & Convert.ToInt32(txtSvcRcno.Text)

                    command.CommandText = qry
                    command.Parameters.Clear()

                    command.Parameters.AddWithValue("@CompanyID", txtClientID.Text.ToUpper)
                    command.Parameters.AddWithValue("@Description", txtDescription.Text.ToUpper)
                    command.Parameters.AddWithValue("@ContactPerson", txtSvcCP1Contact.Text.ToUpper)
                    command.Parameters.AddWithValue("@Address1", txtAddress.Text.ToUpper)
                    command.Parameters.AddWithValue("@Telephone", txtSvcCP1Telephone.Text.ToUpper)
                    command.Parameters.AddWithValue("@Mobile", txtSvcCP1Mobile.Text.ToUpper)
                    command.Parameters.AddWithValue("@Email", txtSvcCP1Email.Text.ToUpper)

                    command.Parameters.AddWithValue("@AddBuilding", txtBuilding.Text.ToUpper)
                    command.Parameters.AddWithValue("@AddStreet", txtStreet.Text.ToUpper)
                    If ddlState.Text = txtDDLText.Text Then
                        command.Parameters.AddWithValue("@AddState", "")
                    Else
                        command.Parameters.AddWithValue("@AddState", ddlState.Text.ToUpper)
                    End If

                    If ddlCity.Text = txtDDLText.Text Then
                        command.Parameters.AddWithValue("@AddCity", "")
                    Else
                        command.Parameters.AddWithValue("@AddCity", ddlCity.Text.ToUpper)
                    End If
                    If ddlCountry.Text = txtDDLText.Text Then
                        command.Parameters.AddWithValue("@AddCountry", "")
                    Else
                        command.Parameters.AddWithValue("@AddCountry", ddlCountry.Text.ToUpper)
                    End If
                    command.Parameters.AddWithValue("@AddPostal", txtPostal.Text.ToUpper)
                    If ddlLocateGrp.Text = txtDDLText.Text Then
                        command.Parameters.AddWithValue("@LocateGRP", "")

                    Else
                        command.Parameters.AddWithValue("@LocateGRP", ddlLocateGrp.Text.ToUpper)

                    End If
                    command.Parameters.AddWithValue("@Fax", txtSvcCP1Fax.Text.ToUpper)
                    command.Parameters.AddWithValue("@AccountID", txtAccountID.Text.ToUpper)
                    command.Parameters.AddWithValue("@LocationPrefix", txtLocationPrefix.Text.ToUpper)
                    command.Parameters.AddWithValue("@LocationNo", Convert.ToInt16(txtLocatonNo.Text.ToUpper))
                    command.Parameters.AddWithValue("@ServiceName", txtServiceName.Text.ToUpper)
                    command.Parameters.AddWithValue("@Contact1Position", txtSvcCP1Position.Text.ToUpper)
                    command.Parameters.AddWithValue("@Tel2", txtSvcCP1Telephone2.Text.ToUpper)
                    command.Parameters.AddWithValue("@ContactPerson2", txtSvcCP2Contact.Text.ToUpper)
                    command.Parameters.AddWithValue("@Contact2Position", txtSvcCP2Position.Text.ToUpper)
                    command.Parameters.AddWithValue("@Contact2Tel", txtSvcCP2Telephone.Text.ToUpper)
                    command.Parameters.AddWithValue("@Contact2Fax", txtSvcCP2Fax.Text.ToUpper)
                    command.Parameters.AddWithValue("@Contact2Tel2", txtSvcCP2Tel2.Text.ToUpper)
                    command.Parameters.AddWithValue("@Contact2Mobile", txtSvcCP2Mobile.Text.ToUpper)
                    command.Parameters.AddWithValue("@Contact2Email", txtSvcCP2Email.Text.ToUpper)
                    command.Parameters.AddWithValue("@ServiceLocationGroup", txtServiceLocationGroup.Text.ToUpper)
                    If chkSameAddr.Checked = True Then
                        command.Parameters.AddWithValue("@ServiceAddress", 1)

                    Else
                        command.Parameters.AddWithValue("@ServiceAddress", 0)

                    End If


                    command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                    command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))

                    command.Parameters.AddWithValue("@BillingNameSvc", txtBillingNameSvc.Text.ToUpper)
                    command.Parameters.AddWithValue("@BillAddressSvc", txtBillAddressSvc.Text.ToUpper)
                    command.Parameters.AddWithValue("@BillStreetSvc", txtBillStreetSvc.Text.ToUpper)
                    command.Parameters.AddWithValue("@BillBuildingSvc", txtBillBuildingSvc.Text.ToUpper)
                    If ddlBillCitySvc.Text = txtDDLText.Text Then
                        command.Parameters.AddWithValue("@BillCitySvc", "")
                    Else
                        command.Parameters.AddWithValue("@BillCitySvc", ddlBillCitySvc.Text.ToUpper)
                    End If
                    If ddlBillStateSvc.Text = txtDDLText.Text Then
                        command.Parameters.AddWithValue("@BillStateSvc", "")
                    Else
                        command.Parameters.AddWithValue("@BillStateSvc", ddlBillStateSvc.Text.ToUpper)
                    End If
                    If ddlBillCountrySvc.Text = txtDDLText.Text Then
                        command.Parameters.AddWithValue("@BillCountrySvc", "")
                    Else
                        command.Parameters.AddWithValue("@BillCountrySvc", ddlBillCountrySvc.Text.ToUpper)
                    End If
                    command.Parameters.AddWithValue("@BillPostalSvc", txtBillPostalSvc.Text.ToUpper)
                    command.Parameters.AddWithValue("@BillContact1Svc", txtBillContact1Svc.Text.ToUpper)
                    command.Parameters.AddWithValue("@BillPosition1Svc", txtBillPosition1Svc.Text.ToUpper)
                    command.Parameters.AddWithValue("@BillTelephone1Svc", txtBillTelephone1Svc.Text.ToUpper)
                    command.Parameters.AddWithValue("@BillFax1Svc", txtBillFax1Svc.Text.ToUpper)
                    command.Parameters.AddWithValue("@Billtelephone12Svc", txtBilltelephone12Svc.Text.ToUpper)
                    command.Parameters.AddWithValue("@BillMobile1Svc", txtBillMobile1Svc.Text.ToUpper)
                    command.Parameters.AddWithValue("@BillEmail1Svc", txtBillEmail1Svc.Text.ToUpper)
                    command.Parameters.AddWithValue("@BillContact2Svc", txtBillContact2Svc.Text.ToUpper)
                    command.Parameters.AddWithValue("@BillPosition2Svc", txtBillPosition2Svc.Text.ToUpper)
                    command.Parameters.AddWithValue("@BillTelephone2Svc", txtBillTelephone2Svc.Text.ToUpper)
                    command.Parameters.AddWithValue("@BillFax2Svc", txtBillFax2Svc.Text.ToUpper)
                    command.Parameters.AddWithValue("@Billtelephone22Svc", txtBilltelephone22Svc.Text.ToUpper)
                    command.Parameters.AddWithValue("@BillMobile2Svc", txtBillMobile2Svc.Text.ToUpper)
                    command.Parameters.AddWithValue("@BillEmail2Svc", txtBillEmail2Svc.Text.ToUpper)

                    command.Parameters.AddWithValue("@SalesManSvc", ddlSalesManSvc.Text.ToUpper)
                    command.Parameters.AddWithValue("@InchargeIDSvc", ddlInchargeSvc.Text.ToUpper)
                    command.Parameters.AddWithValue("@ARTermSvc", ddlTermsSvc.Text.ToUpper)

                    If chkServiceReportSendTo1.Checked = True Then
                        command.Parameters.AddWithValue("@SendServiceReportTo1", "Y")
                    Else
                        command.Parameters.AddWithValue("@SendServiceReportTo1", "N")
                    End If

                    If chkServiceReportSendTo2.Checked = True Then
                        command.Parameters.AddWithValue("@SendServiceReportTo2", "Y")
                    Else
                        command.Parameters.AddWithValue("@SendServiceReportTo2", "N")
                    End If
                    command.Parameters.AddWithValue("@MandatoryServiceReportPhotos", chkSvcPhotosMandatory.Checked)

                    'If chkSvcPhotosMandatory.Checked = True Then
                    '    command.Parameters.AddWithValue("@MandatoryServiceReportPhotos", "Y")
                    'Else
                    '    command.Parameters.AddWithValue("@MandatoryServiceReportPhotos", "N")
                    'End If

                    command.Parameters.AddWithValue("@Industry", ddlIndustrysvc.Text.ToUpper)
                    command.Parameters.AddWithValue("@MarketSegmentId", txtMarketSegmentIDsvc.Text.ToUpper)

                    If ddlContractGrp.SelectedIndex = 0 Then
                        command.Parameters.AddWithValue("@ContractGroup", "")
                    Else
                        'command.Parameters.AddWithValue("@ContractGroup", ddlContractGrp.SelectedValue.ToString)
                        command.Parameters.AddWithValue("@ContractGroup", lContractGroup.Trim)
                    End If

                    command.Parameters.AddWithValue("@Comments", txtCommentsSvc.Text.ToUpper)

                    If ddlCompanyGrpD.SelectedIndex = 0 Then
                        command.Parameters.AddWithValue("@CompanyGroupD", "")
                    Else
                        command.Parameters.AddWithValue("@CompanyGroupD", ddlCompanyGrpD.SelectedValue.ToString)
                    End If

                    'command.Parameters.AddWithValue("@CompanyGroupD", ddlCompanyGrpD.Text.ToUpper)
                    'command.Parameters.AddWithValue("@InActiveD", False)
                    command.Parameters.AddWithValue("@InActiveD", chkInactiveD.Checked)

                    command.Parameters.AddWithValue("@ServiceEmailCC", txtSvcEmailCC.Text.ToUpper)
                    command.Parameters.AddWithValue("@EmailServiceNotificationOnly", chkSendEmailNotificationOnly.Checked)

                    If ddlSvcDefaultInvoiceFormat.SelectedIndex = 0 Then
                        command.Parameters.AddWithValue("@DefaultInvoiceFormat", "")
                    Else
                        command.Parameters.AddWithValue("@DefaultInvoiceFormat", ddlSvcDefaultInvoiceFormat.Text)
                    End If

                    If ddlBranch.SelectedIndex = 0 Then
                        command.Parameters.AddWithValue("@Location", "")
                    Else
                        command.Parameters.AddWithValue("@Location", ddlBranch.Text)
                    End If

                    command.Parameters.AddWithValue("@SmartCustomer", chkSmartCustomer.Checked)

                    command.Parameters.AddWithValue("@BusinessHoursStart", txtBusinessHoursStart.Text.ToUpper)
                    command.Parameters.AddWithValue("@BusinessHoursEnd", txtBusinessHoursEnd.Text.ToUpper)
                    command.Parameters.AddWithValue("@ExcludePIRDataDuringBusinessHours", chkExcludePIRDatainBusinessHours.Checked)

                    command.Parameters.AddWithValue("@SiteName", txtSiteName.Text.ToUpper)
                    command.Parameters.AddWithValue("@ServiceZone", txtServiceZone.Text.ToUpper)
                    command.Parameters.AddWithValue("@ServiceArea", txtServiceArea.Text.ToUpper)
                    command.Connection = conn
                    command.ExecuteNonQuery()

                    '''''''''''''UPDATE CUSTOMER PORTAL USER ACCESS LOCATION INFO'''''''''''''''

                    Dim commandPortal As MySqlCommand = New MySqlCommand

                    commandPortal.CommandType = CommandType.Text
                    commandPortal.CommandText = "UPDATE tblcustomerportaluseraccesslocation SET ServiceName=@ServiceName,ServiceZone=@ServiceZone,ServiceArea=@ServiceArea,ContractGroup=@ContractGroup,Address1=@Address1,AddBuilding=@AddBuilding,AddStreet=@AddStreet,AddCity=@AddCity,AddCountry=@AddCountry,AddState=@AddState,AddPostal=@AddPostal where locationid='" & txtLocationID.Text & "'"
                    commandPortal.Connection = conn

                    commandPortal.Parameters.Clear()
                    commandPortal.Parameters.AddWithValue("@ServiceZone", txtServiceZone.Text.ToUpper)
                    commandPortal.Parameters.AddWithValue("@ServiceArea", txtServiceArea.Text.ToUpper)

                    If ddlContractGrp.SelectedIndex = 0 Then
                        commandPortal.Parameters.AddWithValue("@ContractGroup", "")
                    Else
                         commandPortal.Parameters.AddWithValue("@ContractGroup", lContractGroup.Trim)
                    End If

                    commandPortal.Parameters.AddWithValue("@ServiceName", txtServiceName.Text.ToUpper)
                    commandPortal.Parameters.AddWithValue("@AddBuilding", txtBuilding.Text.ToUpper)
                    commandPortal.Parameters.AddWithValue("@AddStreet", txtStreet.Text.ToUpper)

                    If ddlState.Text = txtDDLText.Text Then
                        commandPortal.Parameters.AddWithValue("@AddState", "")
                    Else
                        commandPortal.Parameters.AddWithValue("@AddState", ddlState.Text.ToUpper)
                    End If

                    If ddlCity.Text = txtDDLText.Text Then
                        commandPortal.Parameters.AddWithValue("@AddCity", "")
                    Else
                        commandPortal.Parameters.AddWithValue("@AddCity", ddlCity.Text.ToUpper)
                    End If
                    If ddlCountry.Text = txtDDLText.Text Then
                        commandPortal.Parameters.AddWithValue("@AddCountry", "")
                    Else
                        commandPortal.Parameters.AddWithValue("@AddCountry", ddlCountry.Text.ToUpper)
                    End If
                    commandPortal.Parameters.AddWithValue("@AddPostal", txtPostal.Text.ToUpper)
                    commandPortal.Parameters.AddWithValue("@Address1", txtAddress.Text.ToUpper)

                    commandPortal.ExecuteNonQuery()


                    '''''''''''''''''' START: UPDATE SERVICE LOCATION '''''''''''''''''''''''''''''''''

                    ''''''''''''''' Start :Update tblContractDet '''''''''''''''''''''''''''''''
                    Dim lAddressContract As String = ""
                    Dim laddress1 As String = ""
                    Dim lIsOpenContarctExist As String = ""
                    Dim cmdIsOpenContarctExist As MySqlCommand = New MySqlCommand

                    cmdIsOpenContarctExist.CommandType = CommandType.Text

                    cmdIsOpenContarctExist.CommandText = "SELECT ContractNo, LocationId FROM tblContractDet where LocationId=@LocationId and ContractNo in (Select ContractNo from tblContract where Status = 'O' order by ContractNo)"
                    cmdIsOpenContarctExist.Parameters.AddWithValue("@LocationId", txtLocationID.Text)
                    cmdIsOpenContarctExist.Connection = conn

                    Dim drIsOpenContarctExist As MySqlDataReader = cmdIsOpenContarctExist.ExecuteReader()
                    Dim dtIsOpenContarctExist As New System.Data.DataTable
                    dtIsOpenContarctExist.Load(drIsOpenContarctExist)


                    If dtIsOpenContarctExist.Rows.Count > 0 Then
                        'lAddressContract = ""
                        lIsOpenContarctExist = ""
                        laddress1 = ""

                        If String.IsNullOrEmpty(txtAddress.Text) = False Then
                            laddress1 = txtAddress.Text.ToUpper
                        End If

                        If String.IsNullOrEmpty(txtStreet.Text) = False Then
                            laddress1 = laddress1 & ", " & txtStreet.Text.ToUpper
                        End If

                        If String.IsNullOrEmpty(txtBuilding.Text) = False Then
                            laddress1 = laddress1 & ", " & txtBuilding.Text.ToUpper
                        End If

                        'If ddlState.Text = txtDDLText.Text Then
                        '    commandSvc.Parameters.AddWithValue("@AddState", "")
                        'Else
                        '    commandSvc.Parameters.AddWithValue("@AddState", ddlState.Text.ToUpper)
                        'End If

                        If ddlCity.SelectedIndex > 0 Then
                            laddress1 = laddress1 & ", " & ddlCity.Text.ToUpper
                        End If


                        'If ddlCountry.SelectedIndex > 0 Then
                        '    laddress1 = laddress1 & ", " & ddlCity.Text.ToUpper
                        'End If

                        If String.IsNullOrEmpty(txtPostal.Text) = False Then
                            laddress1 = laddress1 & ", " & txtPostal.Text.ToUpper
                        End If
                        lIsOpenContarctExist = dtIsOpenContarctExist.Rows(0)("ContractNo").ToString

                        For x As Integer = 0 To dtIsOpenContarctExist.Rows.Count - 1

                            '''''''''''''''''''''''''''''''''''''''''
                            'Dim lIsOpenContarctExistNew As String

                            'lIsOpenContarctExistNew = dtIsOpenContarctExist.Rows(0)("ContractNo").ToString

                            If lIsOpenContarctExist <> dtIsOpenContarctExist.Rows(x)("ContractNo").ToString Then
                                'If lIsOpenContarctExist <> lIsOpenContarctExistNew Then
                                'Dim commandUpdContractServiceAddress As MySqlCommand = New MySqlCommand
                                'commandUpdContractServiceAddress.CommandType = CommandType.Text
                                'commandUpdContractServiceAddress.CommandText = "Update tblContract set CustName = """ & txtServiceName.Text.ToUpper & """, ServiceAddress = '" & lAddressContract.Trim & "'  where ContractNo = '" & lIsOpenContarctExist.Trim & "'"
                                'commandUpdContractServiceAddress.Connection = conn
                                'commandUpdContractServiceAddress.ExecuteNonQuery()

                                'commandUpdContractServiceAddress.Dispose()

                                UpdateContractHeader(lIsOpenContarctExist.Trim)

                                'lAddressContract = ""
                                lIsOpenContarctExist = ""
                                lIsOpenContarctExist = dtIsOpenContarctExist.Rows(x)("ContractNo").ToString
                            End If


                            ''''''''''''''''''''''''''''''''''''
                            '''''''''''''''''''''''''''''''''''''''''''''
                            Dim cmdLoopContarctDet As MySqlCommand = New MySqlCommand

                            cmdLoopContarctDet.CommandType = CommandType.Text

                            cmdLoopContarctDet.CommandText = "SELECT AccountId, ContractNo, LocationId,Address1, Location FROM tblContractDet where ContractNo = @ContractNo  order by AccountID, LocationID"
                            cmdLoopContarctDet.Parameters.AddWithValue("@ContractNo", lIsOpenContarctExist)
                            cmdLoopContarctDet.Connection = conn

                            Dim drLoopContarctDet As MySqlDataReader = cmdLoopContarctDet.ExecuteReader()
                            Dim dtLoopContarctDet As New System.Data.DataTable
                            dtLoopContarctDet.Load(drLoopContarctDet)

                            If dtLoopContarctDet.Rows.Count > 0 Then

                                '''''''''''''''''''''''''''''''''

                                For y As Integer = 0 To dtLoopContarctDet.Rows.Count - 1
                                    'For Each row As DataRow In dtLoopContarctDet.Rows()
                                    'txtComments.Text = row("LocationID")

                                    If dtLoopContarctDet.Rows(y)("AccountId").ToString = txtAccountID.Text And dtLoopContarctDet.Rows(y)("LocationID").ToString = txtLocationID.Text Then
                                        Dim commandContDet As MySqlCommand = New MySqlCommand

                                        commandContDet.CommandType = CommandType.Text
                                        Dim qryContDet As String
                                        qryContDet = ""

                                        '''''''''''''''''
                                        qryContDet = "Update tblContractDet SET  "
                                        qryContDet = qryContDet + "ContactPerson = @ContactPerson, "
                                        qryContDet = qryContDet + "ServiceName = @ServiceName, "

                                        qryContDet = qryContDet + "Telephone = @Telephone, "
                                        qryContDet = qryContDet + "Mobile = @Mobile, "
                                        qryContDet = qryContDet + "Email = @Email, "
                                        qryContDet = qryContDet + "LocateGrp = @LocateGrp, "
                                        qryContDet = qryContDet + "Fax = @Fax, "

                                        qryContDet = qryContDet + "Contact1Position = @Contact1Position, "
                                        qryContDet = qryContDet + "ContactPerson2 = @ContactPerson2, "
                                        qryContDet = qryContDet + "Contact2Position = @Contact2Position, "
                                        qryContDet = qryContDet + "Contact2Tel = @Contact2Tel, "
                                        qryContDet = qryContDet + "Contact2Fax = @Contact2Fax, "
                                        qryContDet = qryContDet + "Contact2Tel2 = @Contact2Tel2, "
                                        qryContDet = qryContDet + "Contact2Mobile = @Contact2Mobile, "
                                        qryContDet = qryContDet + "Contact2Email = @Contact2Email, "
                                        qryContDet = qryContDet + "Telephone2 = @Telephone2, "
                                        qryContDet = qryContDet + "Address1 = @Address1, "
                                        qryContDet = qryContDet + "Location = @Location, "
                                        qryContDet = qryContDet + "LastModifiedBy = @LastModifiedBy, "
                                        qryContDet = qryContDet + "LastModifiedOn = @LastModifiedOn "
                                        qryContDet = qryContDet + "where "
                                        qryContDet = qryContDet + "AccountID	=	@AccountID	and "
                                        qryContDet = qryContDet + "LocationID	=	@LocationID "

                                        '''''''''''''''''
                                        commandContDet.CommandText = qryContDet
                                        commandContDet.Parameters.Clear()

                                        commandContDet.Parameters.AddWithValue("@ContactPerson", txtSvcCP1Contact.Text.ToUpper)
                                        commandContDet.Parameters.AddWithValue("@ServiceName", txtServiceName.Text.ToUpper)

                                        commandContDet.Parameters.AddWithValue("@Telephone", txtSvcCP1Telephone.Text.ToUpper)
                                        commandContDet.Parameters.AddWithValue("@Mobile", txtSvcCP1Mobile.Text.ToUpper)
                                        commandContDet.Parameters.AddWithValue("@Email", txtSvcCP1Email.Text.ToUpper)

                                        If ddlLocateGrp.Text = txtDDLText.Text Then
                                            commandContDet.Parameters.AddWithValue("@LocateGRP", "")
                                        Else
                                            commandContDet.Parameters.AddWithValue("@LocateGRP", ddlLocateGrp.Text.ToUpper)
                                        End If
                                        commandContDet.Parameters.AddWithValue("@Fax", txtSvcCP1Fax.Text.ToUpper)

                                        commandContDet.Parameters.AddWithValue("@Contact1Position", txtSvcCP1Position.Text.ToUpper)
                                        commandContDet.Parameters.AddWithValue("@ContactPerson2", txtSvcCP2Contact.Text.ToUpper)
                                        commandContDet.Parameters.AddWithValue("@Contact2Position", txtSvcCP2Position.Text.ToUpper)
                                        commandContDet.Parameters.AddWithValue("@Contact2Tel", txtSvcCP2Telephone.Text.ToUpper)
                                        commandContDet.Parameters.AddWithValue("@Contact2Fax", txtSvcCP2Fax.Text.ToUpper)
                                        commandContDet.Parameters.AddWithValue("@Contact2Tel2", txtSvcCP2Tel2.Text.ToUpper)
                                        commandContDet.Parameters.AddWithValue("@Contact2Mobile", txtSvcCP2Mobile.Text.ToUpper)
                                        commandContDet.Parameters.AddWithValue("@Contact2Email", txtSvcCP2Email.Text.ToUpper)
                                        commandContDet.Parameters.AddWithValue("@Telephone2", txtSvcCP1Telephone2.Text.ToUpper)
                                        commandContDet.Parameters.AddWithValue("@Address1", laddress1)
                                        commandContDet.Parameters.AddWithValue("@Location", lLocation)

                                        commandContDet.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                                        commandContDet.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                                        commandContDet.Parameters.AddWithValue("@AccountID", txtAccountID.Text.ToUpper)
                                        commandContDet.Parameters.AddWithValue("@LocationID", txtLocationID.Text.ToUpper)

                                        commandContDet.Connection = conn
                                        commandContDet.ExecuteNonQuery()

                                        commandContDet.Dispose()
                                        'lAddressContract = lAddressContract & laddress1 & ";  "

                                    Else
                                        'lAddressContract = lAddressContract & dtLoopContarctDet.Rows(y)("Address1").ToString & ";  "

                                    End If

                                    ''''''''''''''''''''''''''''''
                                Next
                            End If

                            ''''''''''''''''''''''''''''''''''''''''''''''''
                        Next x
                    End If

                    UpdateContractHeader(lIsOpenContarctExist.Trim)

                    'If String.IsNullOrEmpty(lIsOpenContarctExist) = False Then
                    '    Dim commandUpdContractServiceAddress1 As MySqlCommand = New MySqlCommand
                    '    commandUpdContractServiceAddress1.CommandType = CommandType.Text
                    '    commandUpdContractServiceAddress1.CommandText = "Update tblContract set CustName = """ & txtServiceName.Text.ToUpper & """, ServiceAddress = '" & lAddressContract.Trim & "'  where ContractNo = '" & lIsOpenContarctExist.Trim & "'"
                    '    commandUpdContractServiceAddress1.Connection = conn
                    '    commandUpdContractServiceAddress1.ExecuteNonQuery()

                    '    commandUpdContractServiceAddress1.Dispose()
                    'End If

                    '''''''''''''' End :Update tblContractDet '''''''''''''''''''''''''''''''


                    ''''''''''''''' Start :Update tblServiceRecord '''''''''''''''''''''''''''''''


                    Dim qrySvc As String
                    qrySvc = ""

                    '''''''''''''''''
                    'qrySvc = "Update tblServiceRecord SET  "
                    'qrySvc = qrySvc + "ContactPersonID = @ContactPerson, "
                    'qrySvc = qrySvc + "CustAddress1 = @Address1, "
                    'qrySvc = qrySvc + "Address1 = @Address1, "
                    'qrySvc = qrySvc + "Contact1Tel = @Telephone, "
                    'qrySvc = qrySvc + "ContactPersonMobile = @Mobile, "
                    'qrySvc = qrySvc + "Email = @Email, "
                    'qrySvc = qrySvc + "AddBuilding = @AddBuilding, "
                    'qrySvc = qrySvc + "AddStreet = @AddStreet, "
                    'qrySvc = qrySvc + "AddCity = @AddCity, "
                    'qrySvc = qrySvc + "AddState = @AddState, "
                    'qrySvc = qrySvc + "AddCountry = @AddCountry, "
                    'qrySvc = qrySvc + "AddPostal = @AddPostal, "
                    'qrySvc = qrySvc + "LocateGrp = @LocateGrp, "
                    'qrySvc = qrySvc + "Contact1Fax = @Fax, "
                    'qrySvc = qrySvc + "ServiceName = @ServiceName, "
                    'qrySvc = qrySvc + "CustName = @ServiceName, "
                    'qrySvc = qrySvc + "Contact1Position = @Contact1Position, "
                    'qrySvc = qrySvc + "ContactPerson2 = @ContactPerson2, "
                    'qrySvc = qrySvc + "Contact2Position = @Contact2Position, "
                    'qrySvc = qrySvc + "Contact2Tel = @Contact2Tel, "
                    'qrySvc = qrySvc + "Contact2Fax = @Contact2Fax, "
                    'qrySvc = qrySvc + "Contact2Tel2 = @Contact2Tel2, "
                    'qrySvc = qrySvc + "Contact2Mobile = @Contact2Mobile, "
                    'qrySvc = qrySvc + "Contact2Email = @Contact2Email, "
                    'qrySvc = qrySvc + "Contact1Tel2 = @Telephone2, "
                    'qrySvc = qrySvc + "OtherEmail = @OtherEmail, "
                    'qrySvc = qrySvc + "LastModifiedBy = @LastModifiedBy, "
                    'qrySvc = qrySvc + "LastModifiedOn = @LastModifiedOn "
                    'qrySvc = qrySvc + "where "
                    'qrySvc = qrySvc + "AccountID	=	@AccountID	and "
                    'qrySvc = qrySvc + "LocationID	=	@LocationID	and "
                    'qrySvc = qrySvc + "Status = 'O'"


                    '''''''''''''''''


                    Dim commandSvc As MySqlCommand = New MySqlCommand

                    'commandSvc.CommandType = CommandType.Text
                    'commandSvc.CommandText = qrySvc

                    commandSvc.CommandType = CommandType.StoredProcedure
                    commandSvc.CommandText = "UpdateTblServiceRecordFromCustomerMaster"

                    commandSvc.Parameters.Clear()

                    commandSvc.Parameters.AddWithValue("@pr_ContactPerson", txtSvcCP1Contact.Text.ToUpper)
                    commandSvc.Parameters.AddWithValue("@pr_Address1", txtAddress.Text.ToUpper)
                    commandSvc.Parameters.AddWithValue("@pr_CustAddress1", txtAddress.Text.ToUpper)
                    commandSvc.Parameters.AddWithValue("@pr_Telephone", txtSvcCP1Telephone.Text.ToUpper)
                    commandSvc.Parameters.AddWithValue("@pr_Mobile", txtSvcCP1Mobile.Text.ToUpper)
                    commandSvc.Parameters.AddWithValue("@pr_Email", txtSvcCP1Email.Text.ToUpper)

                    commandSvc.Parameters.AddWithValue("@pr_AddBuilding", txtBuilding.Text.ToUpper)
                    commandSvc.Parameters.AddWithValue("@pr_AddStreet", txtStreet.Text.ToUpper)
                    If ddlState.Text = txtDDLText.Text Then
                        commandSvc.Parameters.AddWithValue("@pr_AddState", "")
                    Else
                        commandSvc.Parameters.AddWithValue("@pr_AddState", ddlState.Text.ToUpper)
                    End If

                    If ddlCity.Text = txtDDLText.Text Then
                        commandSvc.Parameters.AddWithValue("@pr_AddCity", "")
                    Else
                        commandSvc.Parameters.AddWithValue("@pr_AddCity", ddlCity.Text.ToUpper)
                    End If
                    If ddlCountry.Text = txtDDLText.Text Then
                        commandSvc.Parameters.AddWithValue("@pr_AddCountry", "")
                    Else
                        commandSvc.Parameters.AddWithValue("@pr_AddCountry", ddlCountry.Text.ToUpper)
                    End If
                    commandSvc.Parameters.AddWithValue("@pr_AddPostal", txtPostal.Text.ToUpper)
                    If ddlLocateGrp.Text = txtDDLText.Text Then
                        commandSvc.Parameters.AddWithValue("@pr_LocateGRP", "")
                    Else
                        commandSvc.Parameters.AddWithValue("@pr_LocateGRP", ddlLocateGrp.Text.ToUpper)
                    End If
                    commandSvc.Parameters.AddWithValue("@pr_Fax", txtSvcCP1Fax.Text.ToUpper)
                    commandSvc.Parameters.AddWithValue("@pr_ServiceName", txtServiceName.Text.ToUpper)
                    commandSvc.Parameters.AddWithValue("@pr_Contact1Position", txtSvcCP1Position.Text.ToUpper)
                    commandSvc.Parameters.AddWithValue("@pr_ContactPerson2", txtSvcCP2Contact.Text.ToUpper)
                    commandSvc.Parameters.AddWithValue("@pr_Contact2Position", txtSvcCP2Position.Text.ToUpper)
                    commandSvc.Parameters.AddWithValue("@pr_Contact2Tel", txtSvcCP2Telephone.Text.ToUpper)
                    commandSvc.Parameters.AddWithValue("@pr_Contact2Fax", txtSvcCP2Fax.Text.ToUpper)
                    commandSvc.Parameters.AddWithValue("@pr_Contact2Tel2", txtSvcCP2Tel2.Text.ToUpper)
                    commandSvc.Parameters.AddWithValue("@pr_Contact2Mobile", txtSvcCP2Mobile.Text.ToUpper)
                    commandSvc.Parameters.AddWithValue("@pr_Contact2Email", txtSvcCP2Email.Text.ToUpper)
                    commandSvc.Parameters.AddWithValue("@pr_Telephone2", txtSvcCP1Telephone2.Text.ToUpper)

                    Dim lOtherEmail As String
                    lOtherEmail = ""

                    'If lSendServiceReportTo1Loc = "Y" Then
                    If chkServiceReportSendTo1.Checked = True Then
                        lOtherEmail = txtBillEmail1Svc.Text.Trim
                    End If

                    'End If


                    'If String.IsNullOrEmpty(lOtherEmail.Trim) = False Then
                    '    lOtherEmail = lOtherEmail.Trim() & ";" & txtBillEmail2Svc.Text.Trim()
                    'Else
                    '    lOtherEmail = txtBillEmail2Svc.Text.Trim()
                    'End If

                    If chkServiceReportSendTo2.Checked = True Then
                        If String.IsNullOrEmpty(txtBillEmail2Svc.Text.Trim()) = False Then
                            If String.IsNullOrEmpty(lOtherEmail.Trim) = True Then
                                lOtherEmail = txtBillEmail2Svc.Text.ToUpper.Trim()
                            Else
                                lOtherEmail = lOtherEmail.Trim() & ";" & txtBillEmail2Svc.Text.ToUpper.Trim()
                            End If
                        End If
                    End If


                    If String.IsNullOrEmpty(txtSvcEmailCC.Text.Trim()) = False Then
                        If String.IsNullOrEmpty(lOtherEmail) = True Then
                            lOtherEmail = txtSvcEmailCC.Text.ToUpper.Trim()
                        Else
                            lOtherEmail = lOtherEmail.Trim() & ";" & txtSvcEmailCC.Text.ToUpper.Trim()
                        End If
                    End If

                    'End If
                    commandSvc.Parameters.AddWithValue("@pr_OtherEmail", Left(lOtherEmail.ToUpper.Trim, 500))
                    commandSvc.Parameters.AddWithValue("@pr_LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                    commandSvc.Parameters.AddWithValue("@pr_LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                    commandSvc.Parameters.AddWithValue("@pr_AccountID", txtAccountID.Text.ToUpper)
                    commandSvc.Parameters.AddWithValue("@pr_LocationID", txtLocationID.Text.ToUpper)
                    commandSvc.Parameters.AddWithValue("@pr_Location", lLocation.ToUpper)
                    commandSvc.Connection = conn
                    commandSvc.ExecuteScalar()

                    commandSvc.Dispose()

                    '''''''''''''' End :Update tblServiceRecord '''''''''''''''''''''''''''''''

                    '' ''''''''''''''' Start :Update tblInvoice '''''''''''''''''''''''''''''''

                    'Dim commandSales As MySqlCommand = New MySqlCommand

                    'commandSales.CommandType = CommandType.Text
                    'Dim qrySales As String
                    'qrySales = ""

                    ' '''''''''''''''''
                    'qrySales = "Update tblSales SET  "
                    'qrySales = qrySales + "CustAttention = @ContactPerson, "
                    'qrySales = qrySales + "CustAddress1 = @Address1, "
                    'qrySales = qrySales + "CustAddBuilding = @AddBuilding, "
                    'qrySales = qrySales + "CustAddStreet = @AddStreet, "
                    'qrySales = qrySales + "CustAddCountry = @AddCountry, "
                    'qrySales = qrySales + "CustAddPostal = @AddPostal, "
                    'qrySales = qrySales + "ContactPersonMobile = @Mobile, "
                    'qrySales = qrySales + "LastModifiedBy = @LastModifiedBy, "
                    'qrySales = qrySales + "LastModifiedOn = @LastModifiedOn "
                    'qrySales = qrySales + "where "
                    'qrySales = qrySales + "AccountID	=	@AccountID	and "
                    'qrySales = qrySales + "CommpanyGroup	=	@CommpanyGroup	and "
                    'qrySales = qrySales + "PostStatus = 'O'"

                    ' '''''''''''''''''
                    'commandSales.CommandText = qrySales
                    'commandSales.Parameters.Clear()

                    'commandSales.Parameters.AddWithValue("@ContactPerson", txtSvcCP1Contact.Text.ToUpper)
                    'commandSales.Parameters.AddWithValue("@Address1", txtAddress.Text.ToUpper)
                    'commandSales.Parameters.AddWithValue("@AddBuilding", txtBuilding.Text.ToUpper)
                    'commandSales.Parameters.AddWithValue("@AddStreet", txtStreet.Text.ToUpper)
                    ''If ddlState.Text = txtDDLText.Text Then
                    ''    commandSales.Parameters.AddWithValue("@AddState", "")
                    ''Else
                    ''    commandSales.Parameters.AddWithValue("@AddState", ddlState.Text.ToUpper)
                    ''End If

                    ''If ddlCity.Text = txtDDLText.Text Then
                    ''    commandSales.Parameters.AddWithValue("@AddCity", "")
                    ''Else
                    ''    commandSales.Parameters.AddWithValue("@AddCity", ddlCity.Text.ToUpper)
                    ''End If
                    'If ddlCountry.Text = txtDDLText.Text Then
                    '    commandSales.Parameters.AddWithValue("@AddCountry", "")
                    'Else
                    '    commandSales.Parameters.AddWithValue("@AddCountry", ddlCountry.Text.ToUpper)
                    'End If
                    'commandSales.Parameters.AddWithValue("@AddPostal", txtPostal.Text.ToUpper)

                    'commandSales.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                    'commandSales.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                    'commandSales.Parameters.AddWithValue("@AccountID", txtAccountID.Text.ToUpper)
                    'commandSales.Parameters.AddWithValue("@CommpanyGroup", ddlCompanyGrpD.Text.ToUpper)
                    'commandSales.Connection = conn
                    'commandSales.ExecuteNonQuery()

                    'commandSales.Dispose()

                    ' '''''''''''''''' End : Update tblInvoice '''''''''''''''''''''''''''''''

                    '''''''''''''''''' END:UPDATE SERVICE LOCATION '''''''''''''''''''''''''''''''''

                    lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED"
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CORP", txtLocationID.Text, "EDIT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")), 0, 0, 0, txtAccountID.Text, "", txtRcno.Text)

                    lblAlert.Text = ""
                End If

                conn.Close()
                conn.Dispose()
                command1.Dispose()
                command2.Dispose()
                dt.Dispose()
                dt1.Dispose()
                dr.Close()

            Catch ex As Exception
                InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "SVC EDIT SAVE", ex.Message.ToString, txtAccountID.Text + " " + txtLocationID.Text)
            End Try
            EnableSvcControls()
        End If

        If txtDisplayRecordsLocationwise.Text = "N" Then
            If String.IsNullOrEmpty(txtAccountID.Text) Then
                SqlDataSource2.SelectCommand = "SELECT * FROM tblcompanylocation where companyid = '" & txtClientID.Text & "'"
            Else
                SqlDataSource2.SelectCommand = "SELECT * FROM tblcompanylocation where accountid = '" & txtAccountID.Text & "'"
            End If
        End If


        If txtDisplayRecordsLocationwise.Text = "Y" Then
            If String.IsNullOrEmpty(txtAccountID.Text) Then
                SqlDataSource2.SelectCommand = "SELECT * FROM tblcompanylocation where personid = '" & txtClientID.Text & "'"
            Else
                SqlDataSource2.SelectCommand = "SELECT * FROM tblcompanylocation where accountid = '" & txtAccountID.Text & "' and location in (Select LocationID  from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "')"
            End If
        End If

        SqlDataSource2.DataBind()
        GridView2.DataBind()
        ' MakeSvcNull()
        txtSvcMode.Text = ""


        '''''''''''''''''''''''''

        SqlDataSource1.SelectCommand = txt.Text
        SqlDataSource1.DataBind()
        GridView1.DataBind()

        '''''''''''''''''''''''

        btnADD.Enabled = True
        btnADD.ForeColor = System.Drawing.Color.Black
        'btnCopyAdd.Enabled = True
        'btnCopyAdd.ForeColor = System.Drawing.Color.Black
        'btnDelete.Enabled = True
        'btnDelete.ForeColor = System.Drawing.Color.Black
        btnTransactions.Enabled = True
        btnTransactions.ForeColor = System.Drawing.Color.Black
        btnChangeStatus.Enabled = True
        btnChangeStatus.ForeColor = System.Drawing.Color.Black

        btnQuit.Enabled = True
        btnQuit.ForeColor = System.Drawing.Color.Black


        btnSvcEdit.Enabled = True
        btnSvcEdit.ForeColor = System.Drawing.Color.Black

        btnSvcCopy.Enabled = True
        btnSvcCopy.ForeColor = System.Drawing.Color.Black


        btnSvcDelete.Enabled = True
        btnSvcDelete.ForeColor = System.Drawing.Color.Black

        btnSvcContract.Enabled = True
        btnSvcContract.ForeColor = System.Drawing.Color.Black

        btnSvcService.Enabled = True
        btnSvcService.ForeColor = System.Drawing.Color.Black

        btnTransfersSvc.Enabled = True
        btnTransfersSvc.ForeColor = System.Drawing.Color.Black

        btnSpecificLocation.Enabled = True
        btnSpecificLocation.ForeColor = System.Drawing.Color.Black

        ddlContractGroup.Enabled = True
    
        AccessControl()
        AddrConcat()
        'InsertNewLogDetail()

        'btnSvcSave.Enabled = False
        'btnSvcSave.ForeColor = System.Drawing.Color.Gray
        'btnSvcCancel.Enabled = False
        'btnSvcCancel.ForeColor = System.Drawing.Color.Gray

    End Sub


    Private Sub UpdateContractHeader(lContractNo As String)
        Try
            Dim conn1 As MySqlConnection = New MySqlConnection()

            conn1.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn1.Open()


            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text
            command1.CommandText = "SELECT LocationId, Address1 FROM tblcontractdet where ContractNo ='" & lContractNo & "' order by LocationId"
            command1.Connection = conn1

            Dim drservicecontractDet As MySqlDataReader = command1.ExecuteReader()
            Dim dtservicecontractDet As New DataTable
            dtservicecontractDet.Load(drservicecontractDet)


            Dim lServiceAddressCons As String = "", lLocationId As String = ""

            lLocationId = ""

            lServiceAddressCons = ""

            For i = 0 To dtservicecontractDet.Rows.Count - 1

                If lLocationId <> dtservicecontractDet.Rows(i)("LocationId").ToString() Then

                    If i = 0 Then
                        lServiceAddressCons = lServiceAddressCons & dtservicecontractDet.Rows(i)("Address1").ToString()
                    Else
                        lServiceAddressCons = lServiceAddressCons & ";  " & vbNewLine & dtservicecontractDet.Rows(i)("Address1").ToString()
                    End If
                    lLocationId = dtservicecontractDet.Rows(i)("LocationId").ToString()
                End If

            Next i

            Dim lBillAddressCons As String
            lBillAddressCons = ""

            If String.IsNullOrEmpty(txtBillAddressSvc.Text.Trim) = False Then
                lBillAddressCons = txtBillAddressSvc.Text.Trim.ToUpper
            End If

            If String.IsNullOrEmpty(txtBillStreetSvc.Text.Trim) = False Then
                lBillAddressCons = lBillAddressCons.Trim + ", " + txtBillStreetSvc.Text.Trim.ToUpper
            End If

            If String.IsNullOrEmpty(txtBillBuildingSvc.Text.Trim) = False Then
                lBillAddressCons = lBillAddressCons.Trim + ", " + txtBillBuildingSvc.Text.Trim.ToUpper
            End If

            If ddlBillCountrySvc.SelectedIndex > 0 Then
                lBillAddressCons = lBillAddressCons.Trim + ", " + ddlBillCountrySvc.Text.Trim.ToUpper
            End If

            If String.IsNullOrEmpty(txtBillPostalSvc.Text.Trim) = False Then
                lBillAddressCons = lBillAddressCons.Trim + ", " + txtBillPostalSvc.Text.Trim
            End If

            Dim lLocation As String

            If ddlBranch.SelectedIndex = 0 Then
                lLocation = ""
            Else
                lLocation = ddlBranch.Text
            End If
            Dim commandUpdContractServiceAddress1 As MySqlCommand = New MySqlCommand
            commandUpdContractServiceAddress1.CommandType = CommandType.Text
            'commandUpdContractServiceAddress1.CommandText = "Update tblContract set CustName = """ & txtServiceName.Text.ToUpper & """, ServiceAddress = """ & lServiceAddressCons & """  where ContractNo = '" & lContractNo.Trim & "'"
            'commandUpdContractServiceAddress1.CommandText = "Update tblContract set Location = '" & lLocation & "',   CustName = """ & txtServiceName.Text.ToUpper & """, ServiceAddress = """ & lServiceAddressCons & """, BillAddress1 = """ & txtBillAddressSvc.Text.ToUpper & ", " & txtBillStreetSvc.Text.ToUpper & ", " & txtBillBuildingSvc.Text.ToUpper & ", " & ddlBillCountrySvc.Text.ToUpper & ", " & txtBillPostalSvc.Text & """  where ContractNo = '" & lContractNo.Trim & "'"
            commandUpdContractServiceAddress1.CommandText = "Update tblContract set Location = '" & lLocation & "',   CustName = """ & txtServiceName.Text.ToUpper & """, ServiceAddress = """ & lServiceAddressCons & """, CustAddr = """ & lBillAddressCons.Trim & """ , BillAddress1 = """ & lBillAddressCons.Trim & """  where ContractNo = '" & lContractNo.Trim & "'"

            commandUpdContractServiceAddress1.Connection = conn1
            commandUpdContractServiceAddress1.ExecuteNonQuery()

            commandUpdContractServiceAddress1.Dispose()
            conn1.Close()


            conn1.Close()
            conn1.Dispose()
            'conn.Close()
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("COMPANY - " + Session("UserID"), "FUNCTION UpdateContractHeader", ex.Message.ToString, txtLocationID.Text)
            Exit Sub

        End Try
    End Sub

    Protected Sub btnSvcEdit_Click(sender As Object, e As EventArgs) Handles btnSvcEdit.Click
        Try
            lblMessage.Text = ""
            If txtSvcRcno.Text = "" Then
                ' MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
                lblAlert.Text = "SELECT RECORD TO EDIT"
                Return

            End If
            DisableSvcControls()
            txtSvcMode.Text = "EDIT"
            lblMessage.Text = "ACTION: EDIT SERVICE LOCATION"
            txtCreatedOn.Text = ""
            btnSvcContract.Enabled = False
            btnSvcService.Enabled = False

            btnSvcContract.ForeColor = System.Drawing.Color.Gray
            btnSvcService.ForeColor = System.Drawing.Color.Gray

            btnTransfersSvc.Enabled = False
            btnTransfersSvc.ForeColor = System.Drawing.Color.Gray

            btnSpecificLocation.Enabled = False
            btnSpecificLocation.ForeColor = System.Drawing.Color.Gray

            'ddlContractGrp.Enabled = False
            btnEditContractGroup.Visible = False
            ddlContractGroup.Enabled = False
        
            AccessControl()
            ''''''''''''''''''''''''''''''
            Dim sqlstr As String
            sqlstr = ""

            sqlstr = "SELECT ContractNo FROM tblContractDet where LocationID = '" & txtLocationID.Text & "' limit 1"

            Dim command As MySqlCommand = New MySqlCommand

            Dim conn As MySqlConnection = New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            command.CommandType = CommandType.Text
            command.CommandText = sqlstr
            command.Connection = conn

            Dim dr As MySqlDataReader = command.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then
                ddlContractGrp.Enabled = False
            End If
            conn.Close()
            conn.Dispose()
            command.Dispose()
            dt.Dispose()
            dr.Close()

            ''''''''''''''''''''''''''''''

            'btnSvcSave.Enabled = True
            'btnSvcSave.ForeColor = System.Drawing.Color.Black
            'btnSvcCancel.Enabled = True
            'btnSvcCancel.ForeColor = System.Drawing.Color.Black
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "btnSvcEdit_Click", ex.Message.ToString, "")
        End Try
    End Sub
    Private Sub AddrConcat()
        Try
            txtSvcAddr.Text = txtAddress.Text.Trim + " " + txtStreet.Text.Trim + " " + txtBuilding.Text.Trim
            If ddlCity.Text <> "-1" Then
                txtSvcAddr.Text = txtSvcAddr.Text + " " + ddlCity.Text
            End If
            If ddlState.Text <> "-1" Then
                txtSvcAddr.Text = txtSvcAddr.Text + " " + ddlState.Text
            End If
            If ddlCountry.Text <> "-1" Then
                txtSvcAddr.Text = txtSvcAddr.Text + " " + ddlCountry.Text
            End If
            txtSvcAddr.Text = txtSvcAddr.Text + " " + txtPostal.Text.Trim
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "AddrConcat", ex.Message.ToString, "")
        End Try
    End Sub
    Protected Sub btnSvcCancel_Click(sender As Object, e As EventArgs) Handles btnSvcCancel.Click
        Try
            MakeSvcNull()
            EnableSvcControls()
            btnSvcEdit.Enabled = False
            btnSvcEdit.ForeColor = System.Drawing.Color.Gray
            btnSvcCopy.Enabled = False
            btnSvcCopy.ForeColor = System.Drawing.Color.Gray
            btnSvcDelete.Enabled = False
            btnSvcDelete.ForeColor = System.Drawing.Color.Gray

            btnADD.Enabled = True
            btnADD.ForeColor = System.Drawing.Color.Black
            btnCopyAdd.Enabled = True
            btnCopyAdd.ForeColor = System.Drawing.Color.Black
            'btnDelete.Enabled = True
            'btnDelete.ForeColor = System.Drawing.Color.Black

            btnFilter.Enabled = True
            btnFilter.ForeColor = System.Drawing.Color.Black
            btnContract.Enabled = True
            btnContract.ForeColor = System.Drawing.Color.Black
            btnTransactions.Enabled = True
            btnTransactions.ForeColor = System.Drawing.Color.Black
            btnChangeStatus.Enabled = True
            btnChangeStatus.ForeColor = System.Drawing.Color.Black

            btnQuit.Enabled = True
            btnQuit.ForeColor = System.Drawing.Color.Black

        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "btnSvcCancel_Click", ex.Message.ToString, "")
        End Try
    End Sub

    Protected Sub btnSvcDelete_Click(sender As Object, e As EventArgs) Handles btnSvcDelete.Click
        lblMessage.Text = ""
        If txtSvcRcno.Text = "" Then
            ' MessageBox.Message.Alert(Page, "Select a record to delete!!!", "str")
            lblAlert.Text = "SELECT RECORD TO DELETE"
            Return
        End If
        lblMessage.Text = "ACTION: DELETE SERVICE LOCATION"

        Dim confirmValue As String = Request.Form("confirm_value")
        'If confirmValue = "Yes" Then
        If Right(confirmValue, 3) = "Yes" Then
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()



                '''''''''''''''''''''''''''
                Dim commandIsExistContract As MySqlCommand = New MySqlCommand
                commandIsExistContract.CommandType = CommandType.Text
                commandIsExistContract.CommandText = "SELECT LocationID FROM tblcontractDet where LocationID ='" & txtLocationID.Text & "' limit 1"
                commandIsExistContract.Connection = conn

                Dim drIsExistContract As MySqlDataReader = commandIsExistContract.ExecuteReader()
                Dim dtIsExistContract As New System.Data.DataTable
                dtIsExistContract.Load(drIsExistContract)

                If dtIsExistContract.Rows.Count > 0 Then
                    lblAlert.Text = "LOCATION ID EXISTS IN CONTRACT.. CANNOT BE DELETED"
                    commandIsExistContract.Dispose()
                    dtIsExistContract.Dispose()
                    drIsExistContract.Close()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                    Exit Sub
                End If

                commandIsExistContract.Dispose()
                dtIsExistContract.Dispose()
                drIsExistContract.Close()
                ''''''''''''''''''''''''''''

                '''''''''''''''''''''''''''
                Dim commandIsExistService As MySqlCommand = New MySqlCommand
                commandIsExistService.CommandType = CommandType.Text
                commandIsExistService.CommandText = "SELECT LocationID FROM tblServiceRecord where LocationID ='" & txtLocationID.Text & "' limit 1"
                commandIsExistService.Connection = conn

                Dim drIsExistService As MySqlDataReader = commandIsExistService.ExecuteReader()
                Dim dtIsExistService As New System.Data.DataTable
                dtIsExistService.Load(drIsExistService)

                If dtIsExistService.Rows.Count > 0 Then
                    lblAlert.Text = "LOCATION ID EXISTS IN SERVICE.. CANNOT BE DELETED"
                    commandIsExistService.Dispose()
                    dtIsExistService.Dispose()
                    drIsExistService.Close()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                    Exit Sub
                End If

                commandIsExistService.Dispose()
                dtIsExistService.Dispose()
                drIsExistService.Close()
                ''''''''''''''''''''''''''''

                Dim command1 As MySqlCommand = New MySqlCommand
                command1.CommandType = CommandType.Text
                command1.CommandText = "SELECT * FROM tblcompanylocation where rcno=" & Convert.ToInt32(txtSvcRcno.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New System.Data.DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "delete from tblcompanylocation where rcno=" & Convert.ToInt32(txtSvcRcno.Text)

                    command.CommandText = qry
                    command.Connection = conn
                    command.ExecuteNonQuery()

                    '  MessageBox.Message.Alert(Page, "Record deleted successfully!!!", "str")
                    lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CORP", txtLocationID.Text, "DELETE", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")), 0, 0, 0, txtAccountID.Text, "", txtRcno.Text)

                    command.Dispose()
                End If
                conn.Close()
                conn.Dispose()
                command1.Dispose()
                dt.Dispose()
                dr.Close()

            Catch ex As Exception
                InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "SVC DELETE", ex.Message.ToString, txtSvcRcno.Text)
            End Try
            EnableSvcControls()

            If String.IsNullOrEmpty(txtAccountID.Text) Then
                SqlDataSource2.SelectCommand = "SELECT * FROM tblcompanylocation where companyid = " & txtClientID.Text
            Else
                SqlDataSource2.SelectCommand = "SELECT * FROM tblcompanylocation where accountid = " & txtAccountID.Text
            End If

            SqlDataSource2.DataBind()
            GridView2.DataBind()
            MakeSvcNull()
            lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"
        End If

    End Sub

    Protected Sub btnFilter_Click(sender As Object, e As EventArgs) Handles btnFilter.Click
        ModalPopupExtender1.Show()
        ' md1.Show()


    End Sub

    Protected Sub btnSvcSearch_Click(sender As Object, e As ImageClickEventArgs) Handles btnSvcSearch.Click
        '    Dim qry As String
        '    qry = "select * from tblcompany where id='E127-1221';" 'accountid ='10000006'" 'in (select accountid from tblcompanylocation where rcno <> 0"

        'If String.IsNullOrEmpty(txtSearch.Text) = False Then

        '    qry = qry + " and (address1 like '%" + txtSearch.Text + "%'"
        '    qry = qry + " or addbuilding like '%" + txtSearch.Text + "%'"
        '    qry = qry + " or addstreet like '%" + txtSearch.Text + "%'"
        '    qry = qry + " or addpostal like '" + txtSearch.Text + "%'))"
        'End If
        '    qry = qry + " order by createdon desc,name;"

        ' txt.Text = qry

        '  MakeMeNull()
        '   SqlDataSource1.SelectCommand = qry
        ' SqlDataSource1.DataBind()
        '  GridView1.DataSourceID = "SqlDataSource1"
        '  GridView1.DataBind()
        '  tb1.ActiveTabIndex = 0

        Dim qry As String
        Try
            qry = "SELECT * FROM tblcompanylocation where accountid='" & txtAccountIDtab2.Text & "'"

            If String.IsNullOrEmpty(txtSearch.Text) = False Then
                qry = qry + " and (locationid='" & txtSearch.Text & "'"
                ' or address1='" & txtSearch.Text & "' or addstreet='" & txtSearch.Text & "' or addbuilding='" & txtSearch.Text & "'or addpostal='" & txtSearch.Text & "';"
                qry = qry + " or description like '%" + txtSearch.Text + "%'"
                qry = qry + " or address1 like '%" + txtSearch.Text + "%'"
                qry = qry + " or addbuilding like '%" + txtSearch.Text + "%'"
                qry = qry + " or addstreet like '%" + txtSearch.Text + "%'"
                qry = qry + " or addpostal like '" + txtSearch.Text + "%'"
                qry = qry + " or billemail1svc like '%" + txtSearch.Text + "%'"
                qry = qry + " or billemail2svc like '%" + txtSearch.Text + "%')"

            End If

            MakeSvcNull()
            SqlDataSource2.SelectCommand = qry
            SqlDataSource2.DataBind()
            GridView2.DataBind()
            GridView2.SelectedIndex = 0
            GridView2_SelectedIndexChanged(sender, e)

            txtSearch.Text = "Search here"
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "BTNSVCSEARCH_CLICK", ex.Message.ToString, qry)
        End Try
    End Sub


    Protected Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        btnGoSvc_Click(sender, e)

        'txtSearchText.Text = txtSearch.Text

        'Dim qry As String
        'Try

        '    If txtDisplayRecordsLocationwise.Text = "N" Then
        '        qry = "SELECT * FROM tblcompanylocation where accountid='" & txtAccountIDtab2.Text & "'"

        '        If String.IsNullOrEmpty(txtSearch.Text) = False Then
        '            qry = qry + " and (locationid like ""%" & txtSearch.Text & "%"""
        '            ' or address1='" & txtSearch.Text & "' or addstreet='" & txtSearch.Text & "' or addbuilding='" & txtSearch.Text & "'or addpostal='" & txtSearch.Text & "';"
        '            qry = qry + " or companyid like ""%" + txtSearch.Text + "%"""
        '            qry = qry + " or servicename like ""%" + txtSearch.Text + "%"""
        '            qry = qry + " or billingnamesvc like ""%" + txtSearch.Text + "%"""
        '            qry = qry + " or ContractGroup like ""%" + txtSearch.Text + "%"""
        '            qry = qry + " or ContactPerson like ""%" + txtSearch.Text + "%"""
        '            qry = qry + " or BillContact1Svc like ""%" + txtSearch.Text + "%"""
        '            qry = qry + " or comments like ""%" + txtSearch.Text + "%"""
        '            qry = qry + " or description like ""%" + txtSearch.Text + "%"""
        '            qry = qry + " or address1 like ""%" + txtSearch.Text + "%"""
        '            qry = qry + " or addbuilding like ""%" + txtSearch.Text + "%"""
        '            qry = qry + " or addstreet like ""%" + txtSearch.Text + "%"""
        '            qry = qry + " or addpostal like """ + txtSearch.Text + "%"""
        '            qry = qry + " or billemail1svc like ""%" + txtSearch.Text + "%"""
        '            qry = qry + " or billemail2svc like ""%" + txtSearch.Text + "%"")"

        '        End If
        '    End If

        '    If txtDisplayRecordsLocationwise.Text = "Y" Then
        '        qry = "SELECT * FROM tblcompanylocation where accountid='" & txtAccountIDtab2.Text & "' and Location in  (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "')"

        '        If String.IsNullOrEmpty(txtSearch.Text) = False Then
        '            qry = qry + " and (locationid like ""%" & txtSearch.Text & "%"""
        '            ' or address1='" & txtSearch.Text & "' or addstreet='" & txtSearch.Text & "' or addbuilding='" & txtSearch.Text & "'or addpostal='" & txtSearch.Text & "';"
        '            qry = qry + " or companyid like ""%" + txtSearch.Text + "%"""
        '            qry = qry + " or servicename like ""%" + txtSearch.Text + "%"""
        '            qry = qry + " or billingnamesvc like ""%" + txtSearch.Text + "%"""
        '            qry = qry + " or ContractGroup like ""%" + txtSearch.Text + "%"""
        '            qry = qry + " or ContactPerson like ""%" + txtSearch.Text + "%"""
        '            qry = qry + " or BillContact1Svc like ""%" + txtSearch.Text + "%"""
        '            qry = qry + " or comments like ""%" + txtSearch.Text + "%"""
        '            qry = qry + " or description like ""%" + txtSearch.Text + "%"""
        '            qry = qry + " or address1 like ""%" + txtSearch.Text + "%"""
        '            qry = qry + " or addbuilding like ""%" + txtSearch.Text + "%"""
        '            qry = qry + " or addstreet like ""%" + txtSearch.Text + "%"""
        '            qry = qry + " or addpostal like """ + txtSearch.Text + "%"""
        '            qry = qry + " or billemail1svc like ""%" + txtSearch.Text + "%"""
        '            qry = qry + " or billemail2svc like ""%" + txtSearch.Text + "%"")"

        '        End If
        '    End If

        '    MakeSvcNull()
        '    SqlDataSource2.SelectCommand = qry
        '    SqlDataSource2.DataBind()
        '    GridView2.DataBind()
        '    lblMessage.Text = "SEARCH CRITERIA : " + txtSearch.Text + " <br/>NUMBER OF RECORDS FOUND : " + GridView2.Rows.Count.ToString

        '    'txtSearch.Text = "Search Here for Location Address, Postal Code or Description"
        '    txtDetail.Text = qry
        'Catch ex As Exception
        '    InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "txtSearch_TextChanged", ex.Message.ToString, qry)
        'End Try
    End Sub


    Protected Sub btnResetSearch_Click(sender As Object, e As ImageClickEventArgs) Handles btnResetSearch.Click
        MakeMeNull()
        EnableControls()
        txtSearchCust.Text = ""
        txtSearchCustText.Text = ""

        txtSearch.Text = ""
        txtSearchText.Text = ""
        Try
            txt.Text = "SELECT * FROM tblcompany a  left join CustomerBal b on a.Accountid = b.Accountid where  a.Inactive=0 order by a.rcno desc, a.Name limit 100;"
            SqlDataSource1.SelectCommand = txt.Text
            SqlDataSource1.DataBind()
            GridView1.DataBind()
            lblMessage.Text = ""
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "btnResetSearch_Click", ex.Message.ToString, txt.Text)
        End Try
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As ImageClickEventArgs) Handles btnReset.Click
        Try
            txtSearch.Text = ""
            txtSearchText.Text = ""
            If String.IsNullOrEmpty(txtAccountID.Text) Then
                SqlDataSource2.SelectCommand = "SELECT * FROM tblcompanylocation where accountid is null and companyid = '" & txtClientID.Text & "'"
            Else
                SqlDataSource2.SelectCommand = "SELECT * FROM tblcompanylocation where accountid = " & txtAccountID.Text
            End If

            SqlDataSource2.DataBind()
            GridView2.DataBind()
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "btnReset_Click", ex.Message.ToString, SqlDataSource2.SelectCommand.ToString)
        End Try
    End Sub

    Protected Sub txtSearchCust_TextChanged(sender As Object, e As EventArgs) Handles txtSearchCust.TextChanged
        Dim qry As String
        Try

            btnGoCust_Click(sender, e)
            '''''''''''
            '    'Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            '    'Dim conn As MySqlConnection = New MySqlConnection(constr)
            '    'conn.Open()
            '    'Dim command As MySqlCommand = New MySqlCommand
            '    'command.CommandType = CommandType.StoredProcedure

            '    'command.CommandText = "SaveTbwARBal"
            '    'command.Parameters.Clear()

            '    'Dim lcutoffdate As String

            '    ''lcutoffdate = "01/01/1990"

            '    'lcutoffdate = DateTime.Now.ToString("yyyy-MM-dd", New System.Globalization.CultureInfo("en-GB"))
            '    'lcutoffdate = "01/" & Format(Month(Convert.ToDateTime(lcutoffdate)) - 2, "00") & "/" & Year(Convert.ToDateTime(lcutoffdate))

            '    'command.Parameters.AddWithValue("@pr_CutOffdate", Convert.ToDateTime(lcutoffdate).ToString("yyyy-MM-dd"))
            '    'command.Connection = conn
            '    'command.ExecuteScalar()
            '    'conn.Close()
            '    'conn.Dispose()
            '    'command.Dispose()

            '    '''''''''''''''

            '    txtSearchCustText.Text = txtSearchCust.Text


            '    'If txtDisplayRecordsLocationwise.Text = "Y" Then
            '    '    qry = " SELECT distinct tblcompany.Rcno, tblcompany.AccountId, tblcompany.InActive, tblcompany.ID, tblcompany.Name, tblcompany.ARCurrency, tblcompany.Location, companybal.Bal, tblcompany.Telephone, tblcompany.Fax, tblcompany.Address1, tblcompany.AddPOstal, tblcompany.BillAddress1, tblcompany.BillPostal, tblcompany.ContactPerson ,tblcompany.ARTerm, tblcompany.Industry,  tblcompany.LocateGrp, tblcompany.CompanyGroup, tblcompany.AccountNo, tblcompany.Salesman, tblcompany.AddStreet, tblcompany.AddBuilding, tblcompany.AddCity, tblcompany.AddState, tblcompany.AddCountry, tblcompany.BillStreet, tblcompany.BillBuilding, tblcompany.BillCity, tblcompany.BillState, tblcompany.BillCountry,  tblcompany.CreatedBy, tblcompany.CreatedOn, tblcompany.LastModifiedBy, tblcompany.LastModifiedOn FROM tblcompany  left join companybal  on tblcompany.Accountid = companybal.Accountid left join tblcompanyLocation  on tblcompany.Accountid = tblcompanyLocation.Accountid where tblcompany.Inactive=0 "
            '    '    qry = qry + " and tblcompanyLocation.location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "')"

            '    '    If String.IsNullOrEmpty(txtSearchCust.Text.Trim) = False Then
            '    '        qry = qry + " and (tblcompany.accountid like ""%" & txtSearchCust.Text.Trim & "%"""
            '    '        qry = qry + " or tblcompany.id like ""%" + txtSearchCust.Text.Trim + "%"""

            '    '        qry = qry + " or tblcompany.address1 like ""%" + txtSearchCust.Text.Trim + "%"""
            '    '        qry = qry + " or tblcompany.addbuilding like ""%" + txtSearchCust.Text.Trim + "%"""
            '    '        qry = qry + " or tblcompany.addstreet like ""%" + txtSearchCust.Text.Trim + "%"""
            '    '        qry = qry + " or tblcompany.billingname like ""%" + txtSearchCust.Text.Trim + "%"""
            '    '        qry = qry + " or tblcompany.billaddress1 like ""%" + txtSearchCust.Text.Trim + "%"""
            '    '        qry = qry + " or tblcompany.billbuilding like ""%" + txtSearchCust.Text.Trim + "%"""
            '    '        qry = qry + " or tblcompany.billstreet like ""%" + txtSearchCust.Text.Trim + "%"""
            '    '        qry = qry + " or tblcompany.addpostal like """ + txtSearchCust.Text.Trim + "%"""
            '    '        qry = qry + " or tblcompany.billpostal like """ + txtSearchCust.Text.Trim + "%"""
            '    '        qry = qry + " or tblcompany.name like ""%" + txtSearchCust.Text.Trim + "%"""
            '    '        qry = qry + " or tblcompany.rocnos like ""%" + txtSearchCust.Text.Trim + "%"""
            '    '        'qry = qry + " or tblcompany.accountid in (select accountid from tblcompanylocation  where 1=1 "
            '    '        'qry = qry + " and (tblcompanylocation.address1 like ""%" + txtSearchCust.Text.Trim + "%"""

            '    '        qry = qry + " or (tblcompanylocation.address1 like ""%" + txtSearchCust.Text.Trim + "%"""
            '    '        qry = qry + " or tblcompanylocation.servicename like ""%" + txtSearchCust.Text.Trim + "%"""

            '    '        qry = qry + " or tblcompanylocation.companyid like ""%" + txtSearchCust.Text.Trim + "%"""
            '    '        qry = qry + " or tblcompanylocation.addbuilding like ""%" + txtSearchCust.Text.Trim + "%"""
            '    '        qry = qry + " or tblcompanylocation.addstreet like ""%" + txtSearchCust.Text.Trim + "%"""
            '    '        qry = qry + " or tblcompanylocation.addpostal like """ + txtSearchCust.Text.Trim + "%""))"

            '    '    End If
            '    'End If

            '    'If txtDisplayRecordsLocationwise.Text = "N" Then
            '    qry = "select  tblcompany.Rcno, tblcompany.AccountId, tblcompany.InActive, tblcompany.ID, tblcompany.Name, tblcompany.ARCurrency, tblcompany.Location, CustomerBal.Bal, tblcompany.Telephone, tblcompany.Fax, tblcompany.Address1, tblcompany.AddPOstal, tblcompany.BillAddress1, tblcompany.BillPostal, tblcompany.ContactPerson ,tblcompany.ARTerm, tblcompany.Industry,  tblcompany.LocateGrp, tblcompany.CompanyGroup, tblcompany.AccountNo, tblcompany.Salesman, tblcompany.AddStreet, tblcompany.AddBuilding, tblcompany.AddCity, tblcompany.AddState, tblcompany.AddCountry, tblcompany.BillStreet, tblcompany.BillBuilding, tblcompany.BillCity, tblcompany.BillState, tblcompany.BillCountry,  tblcompany.CreatedBy, tblcompany.CreatedOn, tblcompany.LastModifiedBy, tblcompany.LastModifiedOn,tblcompany.AutoEmailInvoice,tblcompany.AutoEmailSOA,tblcompany.UnsubscribeAutoEmailDate from tblcompany left join CustomerBal  on tblcompany.Accountid = CustomerBal.Accountid where 1=1 "

            '    If String.IsNullOrEmpty(txtSearchCust.Text.Trim) = False Then
            '        qry = qry + " and tblcompany.accountid like ""%" & txtSearchCust.Text.Trim & "%"""
            '        qry = qry + " or tblcompany.id like ""%" + txtSearchCust.Text.Trim + "%"""

            '        qry = qry + " or tblcompany.address1 like ""%" + txtSearchCust.Text.Trim + "%"""
            '        qry = qry + " or tblcompany.addbuilding like ""%" + txtSearchCust.Text.Trim + "%"""
            '        qry = qry + " or tblcompany.addstreet like ""%" + txtSearchCust.Text.Trim + "%"""
            '        qry = qry + " or tblcompany.billingname like ""%" + txtSearchCust.Text.Trim + "%"""
            '        qry = qry + " or tblcompany.billaddress1 like ""%" + txtSearchCust.Text.Trim + "%"""
            '        qry = qry + " or tblcompany.billbuilding like ""%" + txtSearchCust.Text.Trim + "%"""
            '        qry = qry + " or tblcompany.billstreet like ""%" + txtSearchCust.Text.Trim + "%"""
            '        qry = qry + " or tblcompany.addpostal like """ + txtSearchCust.Text.Trim + "%"""
            '        qry = qry + " or tblcompany.billpostal like """ + txtSearchCust.Text.Trim + "%"""
            '        qry = qry + " or tblcompany.name like ""%" + txtSearchCust.Text.Trim + "%"""
            '        qry = qry + " or tblcompany.rocnos like ""%" + txtSearchCust.Text.Trim + "%"""
            '        qry = qry + " or tblcompany.accountid in (select accountid from tblcompanylocation  where 1=1 "
            '        qry = qry + " and (tblcompanylocation.address1 like ""%" + txtSearchCust.Text.Trim + "%"""

            '        qry = qry + " or (tblcompanylocation.address1 like ""%" + txtSearchCust.Text.Trim + "%"""
            '        qry = qry + " or tblcompanylocation.servicename like ""%" + txtSearchCust.Text.Trim + "%"""

            '        qry = qry + " or tblcompanylocation.companyid like ""%" + txtSearchCust.Text.Trim + "%"""
            '        qry = qry + " or tblcompanylocation.addbuilding like ""%" + txtSearchCust.Text.Trim + "%"""
            '        qry = qry + " or tblcompanylocation.addstreet like ""%" + txtSearchCust.Text.Trim + "%"""
            '        qry = qry + " or tblcompanylocation.addpostal like """ + txtSearchCust.Text.Trim + "%"")))"

            '    End If
            '    'End If





            '    'If String.IsNullOrEmpty(txtSearchCust.Text.Trim) = False Then
            '    '    qry = qry + " and (tblcompany.accountid like ""%" & txtSearchCust.Text.Trim & "%"""
            '    '    qry = qry + " or tblcompany.id like ""%" + txtSearchCust.Text.Trim + "%"""

            '    '    qry = qry + " or tblcompany.address1 like ""%" + txtSearchCust.Text.Trim + "%"""
            '    '    qry = qry + " or tblcompany.addbuilding like ""%" + txtSearchCust.Text.Trim + "%"""
            '    '    qry = qry + " or tblcompany.addstreet like ""%" + txtSearchCust.Text.Trim + "%"""
            '    '    qry = qry + " or tblcompany.billingname like ""%" + txtSearchCust.Text.Trim + "%"""
            '    '    qry = qry + " or tblcompany.billaddress1 like ""%" + txtSearchCust.Text.Trim + "%"""
            '    '    qry = qry + " or tblcompany.billbuilding like ""%" + txtSearchCust.Text.Trim + "%"""
            '    '    qry = qry + " or tblcompany.billstreet like ""%" + txtSearchCust.Text.Trim + "%"""
            '    '    qry = qry + " or tblcompany.addpostal like """ + txtSearchCust.Text.Trim + "%"""
            '    '    qry = qry + " or tblcompany.billpostal like """ + txtSearchCust.Text.Trim + "%"""
            '    '    qry = qry + " or tblcompany.name like ""%" + txtSearchCust.Text.Trim + "%"""
            '    '    qry = qry + " or tblcompany.rocnos like ""%" + txtSearchCust.Text.Trim + "%"""
            '    '    'qry = qry + " or tblcompany.accountid in (select accountid from tblcompanylocation  where 1=1 "
            '    '    'qry = qry + " and (tblcompanylocation.address1 like ""%" + txtSearchCust.Text.Trim + "%"""

            '    '    qry = qry + " or (tblcompanylocation.address1 like ""%" + txtSearchCust.Text.Trim + "%"""
            '    '    qry = qry + " or tblcompanylocation.servicename like ""%" + txtSearchCust.Text.Trim + "%"""

            '    '    qry = qry + " or tblcompanylocation.companyid like ""%" + txtSearchCust.Text.Trim + "%"""
            '    '    qry = qry + " or tblcompanylocation.addbuilding like ""%" + txtSearchCust.Text.Trim + "%"""
            '    '    qry = qry + " or tblcompanylocation.addstreet like ""%" + txtSearchCust.Text.Trim + "%"""
            '    '    qry = qry + " or tblcompanylocation.addpostal like """ + txtSearchCust.Text.Trim + "%""))"

            '    'End If

            '    'If txtDisplayRecordsLocationwise.Text = "Y" Then
            '    '    'qry = qry + " and Location = '" & txtLocation.Text.Trim & "'"
            '    '    qry = qry & " and location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "')"

            '    'End If

            '    qry = qry + " order by tblcompany.rcno desc,tblcompany.name;"
            '    txt.Text = qry
            '    MakeMeNull()
            '    SqlDataSource1.SelectCommand = qry
            '    SqlDataSource1.DataBind()
            '    GridView1.AllowPaging = False
            '    GridView1.DataBind()
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "btnSearchCust_TextChanged", ex.Message.ToString, qry)
        End Try



        ''lblMessage.Text = "SEARCH CRITERIA : " + txtSearchCust.Text + " <br/>NUMBER OF RECORDS FOUND : " + GridView1.Rows.Count

        ''txtSearchCust.Text = "Search Here"

        'GridView1.AllowPaging = True

        'tb1.ActiveTabIndex = 0
        'txtSearch.Text = txtSearchCust.Text

        ' '''''''''''

        'If GridView1.Rows.Count > 0 Then
        '    txtMode.Text = "View"

        '    txtRcno.Text = GridView1.Rows(0).Cells(4).Text

        '    PopulateRecord()

        '    'GridView1_SelectedIndexChanged(sender, e)
        '    'If String.IsNullOrEmpty(txtRcnoSelected.Text.Trim) = False Then
        '    '    If txtRcnoSelected.Text > 0 Then
        '    '        txtRcno.Text = txtRcnoSelected.Text
        '    '        txtRcnoSelected.Text = 0
        '    '    Else
        '    '        txtRcno.Text = GridView1.Rows(0).Cells(1).Text
        '    '    End If
        '    'Else
        '    '    txtRcno.Text = GridView1.Rows(0).Cells(1).Text
        '    'End If

        '    'txtRcno.Text = GridView1.Rows(0).Cells(1).Text

        '    'PopulateRecord()
        '    ''UpdatePanel2.Update()

        '    'updPanelSave.Update()
        '    'UpdatePanel3.Update()

        '    'GridView1_SelectedIndexChanged(sender, e)
        'Else
        '    MakeMeNull()
        '    'MakeMeNullBillingDetails()
        'End If

        'btnGoSvc_Click(sender, e)

        'lblMessage.Text = "SEARCH CRITERIA : " + txtSearchCust.Text + " <br/>NUMBER OF RECORDS FOUND : " + GridView1.Rows.Count.ToString
        ''''''''''''''''''''''

    End Sub

    Protected Sub UploadFile(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpload.Click
        Try
            lblMessage.Text = ""
            lblAlert.Text = ""
            If String.IsNullOrEmpty(txtAccountID.Text) Then
                lblAlert.Text = "SELECT ACCOUNT TO UPLOAD FILE"
                Return

            End If

            If String.IsNullOrEmpty(txtFileDescription.Text) Then
                lblAlert.Text = "ENTER FILE DESCRIPTION TO UPLOAD FILE"
                Exit Sub

            End If

            If FileUpload1.HasFile Then

                Dim fileName As String = Path.GetFileName(FileUpload1.PostedFile.FileName)
                Dim ext As String = Path.GetExtension(fileName)

                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()


                If ext = ".DOC" Or ext = ".doc" Or ext = ".DOCX" Or ext = ".docx" Or ext = ".xls" Or ext = ".xlsx" Or ext = ".XLS" Or ext = ".XLSX" Or ext = ".CSV" Or ext = ".csv" Or ext = ".ppt" Or ext = ".PPT" Or ext = ".pptx" Or ext = ".PPTX" Or ext = ".PDF" Or ext = ".pdf" Or ext = ".txt" Or ext = ".TXT" Or ext = ".jpg" Or ext = ".jpeg" Or ext = ".png" Or ext = ".bmp" Or ext = ".JPG" Or ext = ".JPEG" Or ext = ".PNG" Or ext = ".BMP" Then

                    If File.Exists(Server.MapPath("~/Uploads/Customer/") + txtAccountID.Text + "_" + fileName) Then

                        Dim command1 As MySqlCommand = New MySqlCommand

                        command1.CommandType = CommandType.Text

                        command1.CommandText = "SELECT * FROM tblFILEUPLOAD where filenamelink=@filenamelink"
                        command1.Parameters.AddWithValue("@filenamelink", txtAccountID.Text + "_" + fileName)
                        command1.Connection = conn

                        Dim dr As MySqlDataReader = command1.ExecuteReader()
                        Dim dt As New DataTable
                        dt.Load(dr)

                        If dt.Rows.Count > 0 Then

                            '  MessageBox.Message.Alert(Page, "Record already exists!!!", "str")
                            lblAlert.Text = "FILE ALREADY EXISTS"
                            conn.Close()

                            Exit Sub
                        End If
                    Else
                        Dim command1 As MySqlCommand = New MySqlCommand

                        command1.CommandType = CommandType.Text

                        command1.CommandText = "SELECT * FROM tblFILEUPLOAD where filenamelink=@filenamelink"
                        command1.Parameters.AddWithValue("@filenamelink", txtAccountID.Text + "_" + fileName)
                        command1.Connection = conn

                        Dim dr As MySqlDataReader = command1.ExecuteReader()
                        Dim dt As New DataTable
                        dt.Load(dr)

                        If dt.Rows.Count > 0 Then

                            Dim command2 As MySqlCommand = New MySqlCommand

                            command2.CommandType = CommandType.Text

                            command2.CommandText = "delete from fileupload where filenamelink='" + txtAccountID.Text + "_" + fileName + "'"

                            command2.Connection = conn

                            command2.ExecuteNonQuery()
                        End If
                    End If
                    FileUpload1.PostedFile.SaveAs((Server.MapPath("~/Uploads/Customer/") + txtAccountID.Text + "_" + fileName))

                    'Dim myDir As New DirectoryInfo(Server.MapPath("~/Uploads/Customer/"))
                    'Dim filesInDir As FileInfo() = myDir.GetFiles((Convert.ToString(txtAccountID.Text + "_")) + "*.*")
                    'Dim files As List(Of ListItem) = New List(Of ListItem)

                    'For Each foundFile As FileInfo In filesInDir
                    '    Dim fullName As String = foundFile.FullName
                    '    files.Add(New ListItem(foundFile.Name))
                    'Next
                    ''Dim filePaths() As String = Directory.GetFiles(Server.MapPath("~/Uploads/") + txtAccountID.Text + "_*")
                    ''For Each filePath As String In filePaths
                    ''    files.Add(New ListItem(Path.GetFileName(filePath), filePath))
                    ''Next

                    'ADD FILE UPLOAD INFORMATION TO DATABASE INORDER TO STORE FILES WITH DESCRIPTION - 20170930



                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "INSERT INTO tblfileupload(FileGroup,FileRef,FileName,FileDescription,FileType,FileNameLink,CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn)"
                    qry = qry + "VALUES(@FileGroup,@FileRef,@FileName,@FileDescription,@FileType,@FileNameLink,@CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn);"


                    command.CommandText = qry
                    command.Parameters.Clear()

                    If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                        command.Parameters.AddWithValue("@FileGroup", "CUSTOMER")
                        command.Parameters.AddWithValue("@FileRef", txtAccountID.Text)
                        command.Parameters.AddWithValue("@FileName", fileName.ToUpper)
                        command.Parameters.AddWithValue("@FileDescription", txtFileDescription.Text.ToUpper)
                        command.Parameters.AddWithValue("@FileType", ext.ToUpper)
                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))

                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@FileNameLink", txtAccountID.Text + "_" + fileName.ToUpper)

                    ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                        command.Parameters.AddWithValue("@FileGroup", "CUSTOMER")
                        command.Parameters.AddWithValue("@FileRef", txtAccountID.Text)
                        command.Parameters.AddWithValue("@FileName", fileName)
                        command.Parameters.AddWithValue("@FileDescription", txtFileDescription.Text)
                        command.Parameters.AddWithValue("@FileType", ext.ToUpper)
                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                        command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@FileNameLink", txtAccountID.Text + "_" + fileName)

                    End If


                    command.Connection = conn

                    command.ExecuteNonQuery()
                    conn.Close()
                    conn.Dispose()
                    command.Dispose()

                    SqlDSUpload.SelectCommand = "select * from tblfileupload where fileref = '" + txtAccountID.Text + "'"
                    gvUpload.DataSourceID = "SqlDSUpload"
                    gvUpload.DataBind()
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "FILEUPLOAD", txtAccountID.Text, "ADD", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", txtAccountID.Text + "_" + fileName, txtAccountID.Text)

                    '    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "FILEUPLOAD", txtAccountID.Text, "ADD", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtAccountID.Text + "_" + fileName)

                    txtFileDescription.Text = ""

                    lblMessage.Text = "FILE UPLOADED"
                    lblFileUploadCount.Text = "File Upload [" & gvUpload.Rows.Count & "]"

                Else
                    lblAlert.Text = "FILE FORMAT NOT ALLOWED TO UPLOAD"
                    Return
                End If
            Else
                lblAlert.Text = "SELECT FILE TO UPLOAD"
            End If
            '  Response.Redirect(Request.Url.AbsoluteUri)
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "Upload File", ex.Message.ToString, txtAccountID.Text + "-" + FileUpload1.PostedFile.FileName)
        End Try
    End Sub

    'Protected Sub btnEditFileDesc_Click(sender As Object, e As EventArgs)
    '    Dim btn1 As ImageButton = DirectCast(sender, ImageButton)

    '    Dim xrow1 As GridViewRow = CType(btn1.NamingContainer, GridViewRow)
    '    Dim rowindex1 As Integer = xrow1.RowIndex



    '    Dim rcnofile = DirectCast(GridView1.Rows(rowindex1).FindControl("Label1"), Label).Text
    '    txtfilercno.Text = rcnofile.ToString

    '    lblMessage.Text = ""
    '    lblAlert.Text = ""

    '    txtEditFileName.Text = GridView1.Rows(rowindex1).Cells(0).Text
    '    txtEditFileDescription.Text = GridView1.Rows(rowindex1).Cells(1).Text

    '    mdlPopupEditFileDesc.Show()


    'End Sub


    Protected Sub DownloadFile(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim filePath As String = CType(sender, LinkButton).CommandArgument
            filePath = Server.MapPath("~/Uploads/Customer/") + filePath
            Response.ContentType = ContentType
            Response.AppendHeader("Content-Disposition", ("attachment; filename=" + Path.GetFileName(filePath)))
            Response.WriteFile(filePath)
            Response.End()
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "DownloadFile", ex.Message.ToString, "")
        End Try
    End Sub

    Protected Sub DeleteFile(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim filePath As String = CType(sender, LinkButton).CommandArgument
            txtFileLink.Text = filePath

            filePath = Server.MapPath("~/Uploads/Customer/") + filePath

            txtDeleteUploadedFile.Text = filePath
            iframeid.Attributes.Add("src", "about:blank")

            lblQuery.Text = "Are you sure to DELETE the File? <br><br> File Name : " + txtFileLink.Text

            mdlPopupDeleteUploadedFile.Show()

            'File.Delete(filePath)
            ''  Response.Redirect(Request.Url.AbsoluteUri)
            'lblMessage.Text = "FILE DELETED"
            'Dim myDir As New DirectoryInfo(Server.MapPath("~/Uploads/"))
            'Dim filesInDir As FileInfo() = myDir.GetFiles((Convert.ToString(txtAccountID.Text + "_")) + "*.*")
            'Dim files As List(Of ListItem) = New List(Of ListItem)

            'For Each foundFile As FileInfo In filesInDir
            '    Dim fullName As String = foundFile.FullName
            '    files.Add(New ListItem(foundFile.Name))
            'Next
            ''Dim filePaths() As String = Directory.GetFiles(Server.MapPath("~/Uploads/") + txtAccountID.Text + "_*")
            ''For Each filePath As String In filePaths
            ''    files.Add(New ListItem(Path.GetFileName(filePath), filePath))
            ''Next
            'gvUpload.DataSource = files
            'gvUpload.DataBind()
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "DeleteFile", ex.Message.ToString, "")
        End Try
    End Sub

    Private Function CheckforSalesmanLocation() As Boolean
        Try
            Dim IsSalesman As Boolean
            IsSalesman = True
            Dim IsLock As String
            IsLock = ""

            Dim connLocation As MySqlConnection = New MySqlConnection()

            connLocation.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            connLocation.Open()

            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text
            command1.CommandText = "SELECT StaffId FROM tblstaff where StaffId='" & ddlSalesManSvc.Text.ToUpper & "' and Status ='O'"
            command1.Connection = connLocation

            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)


            If dt.Rows.Count = 0 Then
                'ddlLocation.Text = dt.Rows(0)("LocationID").ToString
                IsSalesman = False
            End If

            connLocation.Close()
            connLocation.Dispose()
            command1.Dispose()
            dt.Dispose()
            Return IsSalesman
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "CheckforSalesmanLocation", ex.Message.ToString, "")
        End Try
    End Function

    Protected Sub btnSvcContract_Click(sender As Object, e As EventArgs) Handles btnSvcContract.Click
        lblMessage.Text = ""
        If txtRcno.Text = "" Then
            '  MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
            lblAlert.Text = "SELECT RECORD TO OPEN CONTRACT"
            Exit Sub
        End If

        If ddlCompanyGrpD.SelectedIndex = 0 Then
            '  MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
            lblAlert.Text = "ENTER COMPANY GROUP TO OPEN CONTRACT"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            Exit Sub
        End If


        If CheckforSalesmanLocation() = False Then
            lblAlert.Text = "PLEASE SELECT ACTIVE SALESMAN"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            Exit Sub
        End If


        Session("companygroup") = ddlCompanyGrpD.Text
        'Session("companygroup") = ddlCompanyGrp.Text
        Session("contractfrom") = "clients"
        Session("contracttype") = "CORPORATE"

        'Session("clientid") = txtID.Text
        Session("accountid") = txtAccountID.Text.Trim

        '12.11.17
        'Session("custname") = txtNameE.Text
        Session("custname") = txtServiceName.Text
        '12.11.17

        'Session("contactperson") = txtContact.Text
        'Session("conpermobile") = txtMobile.Text
        'Session("acctcode") = txtAcctCode.Text
        'Session("telephone") = txtTelephone.Text
        'Session("fax") = txtFax.Text
        Session("postal") = txtOffPostal.Text

        Dim serviceaddress As String
        serviceaddress = ""

        If String.IsNullOrEmpty(txtAddress.Text.Trim) = False Then
            serviceaddress = txtAddress.Text
        End If

        If String.IsNullOrEmpty(txtStreet.Text.Trim) = False Then
            serviceaddress = serviceaddress + " " + txtStreet.Text
        End If
        If String.IsNullOrEmpty(txtBuilding.Text.Trim) = False Then
            serviceaddress = serviceaddress + " " + txtBuilding.Text
        End If
        If ddlCity.SelectedIndex > 0 Then
            serviceaddress = serviceaddress + " " + ddlCity.Text
        End If
        If String.IsNullOrEmpty(txtPostal.Text.Trim) = False Then
            serviceaddress = serviceaddress + " " + txtPostal.Text
        End If


        'Session("sevaddress") = txtAddress.Text
        Session("sevaddress") = serviceaddress
        Session("locategrp") = ddlLocateGrp.Text

        If ddlSalesManSvc.SelectedIndex = 0 Then
            Session("salesman") = ddlSalesMan.Text
        Else
            Session("salesman") = ddlSalesManSvc.Text
        End If

        Session("offaddress1") = txtOffAddress1.Text
        Session("offstreet") = txtOffStreet.Text
        Session("offbuilding") = txtOffBuilding.Text

        If (ddlOffCity.Text.Trim) = "-1" Then
            Session("offcity") = ""
        Else
            Session("offcity") = ddlBillCity.Text
        End If

        'Session("offcity") = ddlOffCity.Text
        Session("offpostal") = txtOffPostal.Text



        'Session("billaddress1") = txtBillAddress.Text
        'Session("billstreet") = txtBillStreet.Text
        'Session("billbuilding") = txtBillBuilding.Text

        'If (ddlBillCity.Text.Trim) = "-1" Then
        '    Session("billcity") = ""
        'Else
        '    Session("billcity") = ddlBillCity.Text
        'End If

        'If (ddlBillCity.Text.Trim) = "-1" Then
        '    Session("billcountry") = ""
        'Else
        '    Session("billcountry") = ddlBillCountry.Text
        'End If
        ''Session("billcity") = ddlBillCity.Text
        'Session("billpostal") = txtBillPostal.Text



        ''''''''''''''''''''''''''''

        Session("billaddress1") = txtBillAddressSvc.Text
        Session("billstreet") = txtBillStreetSvc.Text
        Session("billbuilding") = txtBillBuildingSvc.Text

        If (ddlBillCity.Text.Trim) = "-1" Then
            Session("billcity") = ""
        Else
            Session("billcity") = ddlBillCitySvc.Text
        End If

        If (ddlBillCountrySvc.Text.Trim) = "-1" Then
            Session("billcountry") = ""
        Else
            Session("billcountry") = ddlBillCountrySvc.Text
        End If
        'Session("billcity") = ddlBillCity.Text
        Session("billpostal") = txtBillPostalSvc.Text

        '''''''''''''''''''''''''''''


        Session("locationid") = txtLocationID.Text
        'If String.IsNullOrEmpty(Session("contractfrom")) = False Then
        '    Session("clientid") = txtID.Text

        'End If
        Session("gridsqlCompany") = txt.Text
        Session("rcno") = txtRcno.Text

        Session("gridsqlCompanyDetail") = txtDetail.Text
        Session("rcnoDetail") = txtSvcRcno.Text
        Session("industry") = ddlIndustrysvc.Text
        Session("marketsegmentidsvc") = txtMarketSegmentIDsvc.Text

        If (ddlContractGrp.Text.Trim) = "-1" Then
            Session("contractgroup") = ""
        Else
            Dim hyphenpos As Integer
            hyphenpos = 0
            hyphenpos = (ddlContractGrp.Text.IndexOf(":"))
            Dim lContractGroup As String

            lContractGroup = Left(ddlContractGrp.Text.Trim, (hyphenpos - 1))
            Session("contractgroup") = lContractGroup.Trim
            'Session("contractgroup") = ddlContractGrp.Text
        End If

        Session("inactive") = chkInactive.Checked

        'If ddlLocation.SelectedIndex = 0 Then
        '    Session("location") = ""
        'Else
        '    Session("location") = ddlLocation.Text
        'End If

        If ddlBranch.SelectedIndex = 0 Then
            Session("location") = ""
        Else
            Session("location") = ddlBranch.Text
        End If

        Session("gridview1companyPI") = GridView1.PageIndex
        Session("gridview1companyRI") = GridView1.SelectedIndex

        Response.Redirect("contract.aspx")

        'Server.Transfer("contract.aspx")
    End Sub

    Protected Sub btnSvcService_Click(sender As Object, e As EventArgs) Handles btnSvcService.Click
        lblAlert.Text = ""
        If String.IsNullOrEmpty(txtLocationID.Text) = True Then
            lblAlert.Text = "Please Select a Service Location then press SERVICE button to Proceed"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            Exit Sub
        End If

        Session("servicefrom") = "contactC"

        If String.IsNullOrEmpty(txtLocationID.Text) = False Then
            Session("locationid") = txtLocationID.Text
            'Session("accountid") = txtAccountID.Text
            'End If

            'If String.IsNullOrEmpty(txtContractNo.Text) = False Then
            'Session("contractno") = txtContractNo.Text
            'txt.Text = SQLDSContract.SelectCommand
            Session("gridsql") = txt.Text
            Session("rcno") = txtRcno.Text
            Session("AccountId") = txtAccountID.Text
            Session("CustName") = txtNameE.Text
            Session("ContactType") = "CORPORATE"
            Session("CompanyGroup") = ddlCompanyGrp.Text
            '  Session("ContractGroup") = ddlContractGrp.Text

            Session("Scheduler") = Session("StaffID")

            Session("gridsqlCompanyDetail") = txtDetail.Text
            Session("rcnoDetail") = txtSvcRcno.Text
            Session("inactive") = chkInactive.Checked
            Session("location") = ddlLocation.Text
            'Session("Team") = txtTeam.Text
            'Session("InCharge") = txtTeamIncharge.Text
            'Session("ServiceBy") = txtServiceBy.Text
            'Session("ScheduleType") = ddlScheduleType.Text

            Session("gridview1companyPI") = GridView1.PageIndex
            Session("gridview1companyRI") = GridView1.SelectedIndex

            '''''''''''''''''''''''''''''
        End If

        Response.Redirect("Service.aspx")
    End Sub

    Protected Sub ddlOffCity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlOffCity.SelectedIndexChanged
        Try
            If ddlOffCity.SelectedIndex > 0 Then
                FindCountry("office")
            End If
            'UpdatePanel1.Update()
            ddlOffCity.Focus()
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "ddlOffCity_SelectedIndexChanged", ex.Message.ToString, "")
        End Try
    End Sub

    Protected Sub ddlBillCity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBillCity.SelectedIndexChanged
        If ddlBillCity.SelectedIndex > 0 Then
            FindCountry("billing")
        End If
        'UpdatePanel1.Update()
    End Sub

    Protected Sub ddlCity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCity.SelectedIndexChanged
        If ddlCity.SelectedIndex > 0 Then
            FindCountry("service")
        End If
    End Sub

    Protected Sub ddlBillCitySvc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBillCitySvc.SelectedIndexChanged
        If ddlBillCitySvc.SelectedIndex > 0 Then
            FindCountry("servicebilling")
        End If
    End Sub

    Protected Sub txtNameE_TextChanged(sender As Object, e As EventArgs) Handles txtNameE.TextChanged
        Try
            GridView3.DataSource = ""
            GridView3.DataBind()

            Dim command As MySqlCommand = New MySqlCommand
            Dim sqlstr As String
            Dim conn As MySqlConnection = New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            If txtDisplayRecordsLocationwise.Text = "Y" Then
                sqlstr = "Select Name from tblCompany where Name = """ & txtNameE.Text.Trim.ToUpper & """ and location = '" & ddlLocation.Text.Trim & "'"
            Else
                sqlstr = "Select Name from tblCompany where Name = """ & txtNameE.Text.Trim.ToUpper & """"
            End If

            command.CommandType = CommandType.Text
            command.CommandText = sqlstr
            command.Connection = conn

            Dim dr As MySqlDataReader = command.ExecuteReader()
            Dim dt As New System.Data.DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then

                'Dim message As String = "alert('Customer Already Exists..')"

                'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)
                ''MessageBox.Message.Alert(Page, "Company Already Exists..", "str")
                '  lblAlert.Text = "Customer Name already exists"
                GridView3.DataSource = dt
                GridView3.DataBind()
                mdlPopupCustExists.Show()
            End If


            Dim CustName As String = IgnoredWords(conn, txtNameE.Text.ToUpper)

            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text
            If txtDisplayRecordsLocationwise.Text = "Y" Then
                command1.CommandText = "SELECT Location as Branch,AccountID,Name,concat(Address1,AddStreet,AddBuilding) as Address FROM tblcompany where Name like '%" & CustName & "%'"

            Else
                command1.CommandText = "SELECT AccountID,Name,concat(Address1,AddStreet,AddBuilding) as Address FROM tblcompany where Name like '%" & CustName & "%'"

            End If
            command1.Parameters.AddWithValue("@name", CustName)
            command1.Connection = conn

            Dim dr1 As MySqlDataReader = command1.ExecuteReader()
            Dim dt1 As New System.Data.DataTable
            dt1.Load(dr1)

            If dt1.Rows.Count > 0 Then
                For i As Int32 = 0 To dt1.Rows.Count - 1
                    If CustName = IgnoredWords(conn, dt1.Rows(i)("Name").ToString.ToUpper) Then
                        ' lblAlert.Text = "Customer Name already exists"
                        GridView3.DataSource = dt1
                        GridView3.DataBind()
                        mdlPopupCustExists.Show()
                        ' txtCreatedOn.Text = ""
                        Exit Sub
                    End If
                Next

            End If
            conn.Close()
            conn.Dispose()
            command.Dispose()
            dt.Dispose()
            dr.Close()

            txtBillingName.Text = txtNameE.Text.Trim
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "txtNameE_TextChanged", ex.Message.ToString, txtNameE.Text)
        End Try
    End Sub





    Protected Sub ddlIndustrysvc_TextChanged(sender As Object, e As EventArgs) Handles ddlIndustrysvc.TextChanged
        FindMarketSegmentID()
    End Sub

    Protected Sub btnGoCust_Click(sender As Object, e As EventArgs) Handles btnGoCust.Click

        txtSearchCustText.Text = txtSearchCust.Text
        Dim qry As String
        Try

            'qry = "select * from tblcompany left join companybal  on tblcompany.Accountid = companybal.Accountid where 1=1 "

            If txtDisplayRecordsLocationwise.Text = "Y" Then
                qry = " SELECT distinct tblcompany.Rcno, tblcompany.AccountId, tblcompany.InActive, tblcompany.ID, tblcompany.Name, tblcompany.ARCurrency, tblcompany.Location, companybal.Bal, tblcompany.Telephone, tblcompany.Fax, tblcompany.Address1, tblcompany.AddPOstal, tblcompany.BillAddress1, tblcompany.BillPostal, tblcompany.ContactPerson ,tblcompany.ARTerm, tblcompany.Industry,  tblcompany.LocateGrp, tblcompany.CompanyGroup, tblcompany.AccountNo, tblcompany.Salesman, tblcompany.AddStreet, tblcompany.AddBuilding, tblcompany.AddCity, tblcompany.AddState, tblcompany.AddCountry, tblcompany.BillStreet, tblcompany.BillBuilding, tblcompany.BillCity, tblcompany.BillState, tblcompany.BillCountry,  tblcompany.CreatedBy, tblcompany.CreatedOn, tblcompany.LastModifiedBy, tblcompany.LastModifiedOn,tblcompany.AutoEmailInvoice,tblcompany.AutoEmailSOA,tblcompany.UnsubscribeAutoEmailDate, tblCompany.TaxIdentificationNo, tblCompany.SalesTaxRegistrationNo  FROM tblcompany  left join companybal  on tblcompany.Accountid = companybal.Accountid left join tblcompanyLocation  on tblcompany.Accountid = tblcompanyLocation.Accountid where tblcompany.Inactive=0 "
                qry = qry + " and tblcompany.location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "')"

                If txtSearchCust.Text <> "Search Here" Then
                    qry = qry + " and (tblcompany.accountid like ""%" & txtSearchCust.Text.Trim & "%"""
                    qry = qry + " or tblcompany.id like ""%" + txtSearchCust.Text.Trim + "%"""
                    qry = qry + " or tblcompany.address1 like ""%" + txtSearchCust.Text.Trim + "%"""
                    qry = qry + " or tblcompany.addbuilding like ""%" + txtSearchCust.Text.Trim + "%"""
                    qry = qry + " or tblcompany.addstreet like ""%" + txtSearchCust.Text.Trim + "%"""
                    qry = qry + " or tblcompany.billingname like ""%" + txtSearchCust.Text.Trim + "%"""
                    qry = qry + " or tblcompany.billaddress1 like ""%" + txtSearchCust.Text.Trim + "%"""
                    qry = qry + " or tblcompany.billbuilding like ""%" + txtSearchCust.Text.Trim + "%"""
                    qry = qry + " or tblcompany.billstreet like ""%" + txtSearchCust.Text.Trim + "%"""
                    qry = qry + " or tblcompany.addpostal like """ + txtSearchCust.Text.Trim + "%"""
                    qry = qry + " or tblcompany.billpostal like """ + txtSearchCust.Text.Trim + "%"""
                    qry = qry + " or tblcompany.name like ""%" + txtSearchCust.Text.Trim + "%"""
                    qry = qry + " or tblcompany.rocnos like ""%" + txtSearchCust.Text.Trim + "%"""
                    qry = qry + " or tblcompany.Telephone like ""%" + txtSearchCust.Text.Trim + "%"""
                    qry = qry + " or tblcompany.Name2 like ""%" + txtSearchCust.Text.Trim + "%"""

                    'qry = qry + " or tblcompany.accountid in (select accountid from tblcompanylocation  where 1=1 "
                    'qry = qry + " and (tblcompanylocation.address1 like ""%" + txtSearchCust.Text + "%"""

                    qry = qry + " or (tblcompanylocation.address1 like ""%" + txtSearchCust.Text.Trim + "%"""
                    qry = qry + " or tblcompanylocation.servicename like ""%" + txtSearchCust.Text.Trim + "%"""

                    qry = qry + " or tblcompanylocation.companyid like ""%" + txtSearchCust.Text.Trim + "%"""
                    qry = qry + " or tblcompanylocation.addbuilding like ""%" + txtSearchCust.Text.Trim + "%"""
                    qry = qry + " or tblcompanylocation.addstreet like ""%" + txtSearchCust.Text.Trim + "%"""
                    qry = qry + " or tblcompanylocation.addpostal like """ + txtSearchCust.Text.Trim + "%""))"
                End If

            End If

            If txtDisplayRecordsLocationwise.Text = "N" Then
                qry = "select  tblcompany.Rcno, tblcompany.AccountId, tblcompany.InActive, tblcompany.ID, tblcompany.Name, tblcompany.ARCurrency, tblcompany.Location, CustomerBal.Bal, tblcompany.Telephone, tblcompany.Fax, tblcompany.Address1, tblcompany.AddPOstal, tblcompany.BillAddress1, tblcompany.BillPostal, tblcompany.ContactPerson ,tblcompany.ARTerm, tblcompany.Industry,  tblcompany.LocateGrp, tblcompany.CompanyGroup, tblcompany.AccountNo, tblcompany.Salesman, tblcompany.AddStreet, tblcompany.AddBuilding, tblcompany.AddCity, tblcompany.AddState, tblcompany.AddCountry, tblcompany.BillStreet, tblcompany.BillBuilding, tblcompany.BillCity, tblcompany.BillState, tblcompany.BillCountry,  tblcompany.CreatedBy, tblcompany.CreatedOn, tblcompany.LastModifiedBy, tblcompany.LastModifiedOn,tblcompany.AutoEmailInvoice,tblcompany.AutoEmailSOA,tblcompany.UnsubscribeAutoEmailDate, tblCompany.TaxIdentificationNo, tblCompany.SalesTaxRegistrationNo from tblcompany left join CustomerBal  on tblcompany.Accountid = CustomerBal.Accountid where 1=1 "

                If txtSearchCust.Text <> "Search Here" Then
                    qry = qry + " and tblcompany.accountid like ""%" & txtSearchCust.Text.Trim & "%"""
                    qry = qry + " or tblcompany.id like ""%" + txtSearchCust.Text.Trim + "%"""
                    qry = qry + " or tblcompany.address1 like ""%" + txtSearchCust.Text.Trim + "%"""
                    qry = qry + " or tblcompany.addbuilding like ""%" + txtSearchCust.Text.Trim + "%"""
                    qry = qry + " or tblcompany.addstreet like ""%" + txtSearchCust.Text.Trim + "%"""
                    qry = qry + " or tblcompany.billingname like ""%" + txtSearchCust.Text.Trim + "%"""
                    qry = qry + " or tblcompany.billaddress1 like ""%" + txtSearchCust.Text.Trim + "%"""
                    qry = qry + " or tblcompany.billbuilding like ""%" + txtSearchCust.Text.Trim + "%"""
                    qry = qry + " or tblcompany.billstreet like ""%" + txtSearchCust.Text.Trim + "%"""
                    qry = qry + " or tblcompany.addpostal like """ + txtSearchCust.Text.Trim + "%"""
                    qry = qry + " or tblcompany.billpostal like """ + txtSearchCust.Text.Trim + "%"""
                    qry = qry + " or tblcompany.name like ""%" + txtSearchCust.Text.Trim + "%"""
                    qry = qry + " or tblcompany.rocnos like ""%" + txtSearchCust.Text.Trim + "%"""
                    qry = qry + " or tblcompany.Telephone like ""%" + txtSearchCust.Text.Trim + "%"""
                    qry = qry + " or tblcompany.accountid in (select accountid from tblcompanylocation  where 1=1 "
                    qry = qry + " and (tblcompanylocation.address1 like ""%" + txtSearchCust.Text.Trim + "%"""

                    qry = qry + " or (tblcompanylocation.address1 like ""%" + txtSearchCust.Text.Trim + "%"""
                    qry = qry + " or tblcompanylocation.servicename like ""%" + txtSearchCust.Text.Trim + "%"""

                    qry = qry + " or tblcompanylocation.companyid like ""%" + txtSearchCust.Text.Trim + "%"""
                    qry = qry + " or tblcompanylocation.addbuilding like ""%" + txtSearchCust.Text.Trim + "%"""
                    qry = qry + " or tblcompanylocation.addstreet like ""%" + txtSearchCust.Text.Trim + "%"""
                    qry = qry + " or tblcompanylocation.addpostal like """ + txtSearchCust.Text.Trim + "%"")))"


                End If
            End If

            'If txtSearchCust.Text <> "Search Here" Then
            '    qry = qry + " and (tblcompany.accountid like ""%" & txtSearchCust.Text & "%"""
            '    qry = qry + " or tblcompany.id like ""%" + txtSearchCust.Text + "%"""
            '    qry = qry + " or tblcompany.address1 like ""%" + txtSearchCust.Text + "%"""
            '    qry = qry + " or tblcompany.addbuilding like ""%" + txtSearchCust.Text + "%"""
            '    qry = qry + " or tblcompany.addstreet like ""%" + txtSearchCust.Text + "%"""
            '    qry = qry + " or tblcompany.billingname like ""%" + txtSearchCust.Text + "%"""
            '    qry = qry + " or tblcompany.billaddress1 like ""%" + txtSearchCust.Text + "%"""
            '    qry = qry + " or tblcompany.billbuilding like ""%" + txtSearchCust.Text + "%"""
            '    qry = qry + " or tblcompany.billstreet like ""%" + txtSearchCust.Text + "%"""
            '    qry = qry + " or tblcompany.addpostal like """ + txtSearchCust.Text + "%"""
            '    qry = qry + " or tblcompany.billpostal like """ + txtSearchCust.Text + "%"""
            '    qry = qry + " or tblcompany.name like ""%" + txtSearchCust.Text + "%"""
            '    qry = qry + " or tblcompany.rocnos like ""%" + txtSearchCust.Text + "%"""

            '    'qry = qry + " or tblcompany.accountid in (select accountid from tblcompanylocation  where 1=1 "
            '    'qry = qry + " and (tblcompanylocation.address1 like ""%" + txtSearchCust.Text + "%"""

            '    qry = qry + " or (tblcompanylocation.address1 like ""%" + txtSearchCust.Text + "%"""
            '    qry = qry + " or tblcompanylocation.servicename like ""%" + txtSearchCust.Text + "%"""

            '    qry = qry + " or tblcompanylocation.companyid like ""%" + txtSearchCust.Text + "%"""
            '    qry = qry + " or tblcompanylocation.addbuilding like ""%" + txtSearchCust.Text + "%"""
            '    qry = qry + " or tblcompanylocation.addstreet like ""%" + txtSearchCust.Text + "%"""
            '    qry = qry + " or tblcompanylocation.addpostal like """ + txtSearchCust.Text + "%""))"


            'End If

            'If txtDisplayRecordsLocationwise.Text = "Y" Then
            '    'qry = qry + " and Location = '" & txtLocation.Text.Trim & "'"
            '    qry = qry & " and location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "')"

            'End If
            qry = qry + " order by tblcompany.rcno desc,tblcompany.name;"

            txt.Text = qry
            MakeMeNull()
            SqlDataSource1.SelectCommand = qry
            SqlDataSource1.DataBind()
            GridView1.AllowPaging = False
            GridView1.DataBind()
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "btnGoCust_Click", ex.Message.ToString, qry)
        End Try

        lblMessage.Text = "SEARCH CRITERIA : " + txtSearchCust.Text + " <br/>NUMBER OF RECORDS FOUND : " + GridView1.Rows.Count.ToString

        'txtSearchCust.Text = "Search Here"

        GridView1.AllowPaging = True

        tb1.ActiveTabIndex = 0
        txtSearch.Text = txtSearchCust.Text

        '''''''''''

        If GridView1.Rows.Count > 0 Then
            txtMode.Text = "View"

            txtRcno.Text = GridView1.Rows(0).Cells(6).Text

            PopulateRecord()

            RetrieveAutoEmailInfo()
        Else
            MakeMeNull()
            'MakeMeNullBillingDetails()
        End If

        btnGoSvc_Click(sender, e)
        ''''''''''''''''''''''

        'txtSearchCustText.Text = txtSearchCust.Text
        'Dim qry As String
        ''qry = "select * from tblcompany where tblcompany.rcno <> 0"

        'If txtSearchCust.Text <> "Search Here" Then
        '    If String.IsNullOrEmpty(txtSearchCust.Text) = False Then
        '        qry = qry + " and (tblcompany.accountid like '" & txtSearchCust.Text & "%'"
        '        qry = qry + " or tblcompany.address1 like '%" + txtSearchCust.Text + "%'"
        '        qry = qry + " or tblcompany.addbuilding like '%" + txtSearchCust.Text + "%'"
        '        qry = qry + " or tblcompany.addstreet like '%" + txtSearchCust.Text + "%'"
        '        qry = qry + " or tblcompany.billingname like '%" + txtSearchCust.Text + "%'"
        '        qry = qry + " or tblcompany.billaddress1 like '%" + txtSearchCust.Text + "%'"
        '        qry = qry + " or tblcompany.billbuilding like '%" + txtSearchCust.Text + "%'"
        '        qry = qry + " or tblcompany.billstreet like '%" + txtSearchCust.Text + "%'"
        '        qry = qry + " or tblcompany.addpostal like '" + txtSearchCust.Text + "%'"
        '        qry = qry + " or tblcompany.billpostal like '" + txtSearchCust.Text + "%'"
        '        qry = qry + " or tblcompany.name like '%" + txtSearchCust.Text + "%'"
        '        qry = qry + " or tblcompany.rocnos like '%" + txtSearchCust.Text + "%'"
        '        qry = qry + " or tblcompany.companygroup like '%" + txtSearchCust.Text + "%'"
        '        qry = qry + " or tblcompany.accountid in (select accountid from tblcompanylocation where rcno<>0"

        '        qry = qry + " and (tblcompanylocation.address1 like '%" + txtSearchCust.Text + "%'"
        '        qry = qry + " or tblcompanylocation.addbuilding like '%" + txtSearchCust.Text + "%'"
        '        qry = qry + " or tblcompanylocation.addstreet like '%" + txtSearchCust.Text + "%'"
        '        qry = qry + " or tblcompanylocation.addpostal like '" + txtSearchCust.Text + "%')))"

        '    End If
        '    qry = qry + " order by tblcompany.rcno desc,tblcompany.name;"



        '    txt.Text = qry
        '    MakeMeNull()
        '    SqlDataSource1.SelectCommand = qry
        '    SqlDataSource1.DataBind()
        '    GridView1.AllowPaging = False
        '    GridView1.DataBind()


        '    lblMessage.Text = "SEARCH CRITERIA : " + txtSearchCust.Text + " <br/>NUMBER OF RECORDS FOUND : " + GridView1.Rows.Count.ToString


        '    tb1.ActiveTabIndex = 0
        '    GridView1.AllowPaging = True
        'Else
        '    txtSearchCust.Text = "Search Here"
        'End If

    End Sub



    Protected Sub btnGoSvc_Click(sender As Object, e As EventArgs) Handles btnGoSvc.Click
        txtSearchText.Text = txtSearch.Text


        Dim qry As String
        qry = ""
        Dim qry1 As String
        qry1 = ""
        Dim qry2 As String
        qry2 = ""

        Dim lContractGroupSelection As String
        lContractGroupSelection = ""
        'Try
        'Contract Group

        Dim YrStrList2 As List(Of [String]) = New List(Of String)()

        For Each item As ListItem In ddlContractGroup.Items
            If item.Selected Then

                'YrStrList2.Add("""" + item.Value + """")
                'qry1 = qry1 + " and ("
                qry1 = qry1 + "  ContractGroup like ""%" + item.Value + "%"" or "
                'qry1 = qry1 + ")"
            End If
        Next

        If String.IsNullOrEmpty(qry1) = False Then
            qry2 = " and (" + qry1

            If Right(qry2, 3) = "or " Then
                qry2 = qry2.Remove(qry2.Length - 3)
                'qry2 = qry2.Substring(qry2.Length - 3)
            End If
            qry2 = qry2 + ")"
        End If

        'Dim lContractGroupSelection As String
        'lContractGroupSelection = ""

        'If YrStrList2.Count > 0 Then

        '    Dim YrStr As [String] = [String].Join(",", YrStrList2.ToArray)
        '    lContractGroupSelection = YrStr

        '    ''  selFormula = selFormula + " and {tblservicerecorddet1.ServiceID} in [" + YrStr + "]"
        '    'selFormula = selFormula + " and {tblcontract1.contractgroup} in [" + YrStr + "]"
        '    'If selection = "" Then
        '    '    selection = "ContractGroup : " + YrStr
        '    'Else
        '    '    selection = selection + ", ContractGroup : " + YrStr
        '    'End If
        '    'qry = qry + " and tblcontract.ContractGroup in (" + YrStr + ")"
        'End If

        'Contract Group 


        'If txtSearch.Text <> "Search Here for Location Address, Postal Code or Description" Then
        Try

            If txtSearch.Text = "Search Here" Then
                txtSearch.Text = ""
            End If

            If txtSearch.Text = "Search Here for Location Address, Postal Code or Description" Then
                txtSearch.Text = ""
            End If

            'If ((txtSearch.Text = "" And qry1 = "")) Then
            'qry = "SELECT * FROM tblcompanylocation where accountid='" & txtAccountIDtab2.Text & "'"

            'End If

            'If txtSearchCust.Text <> "Search Here" Then
            'If String.IsNullOrEmpty(txtSearch.Text) = False Or qry1 <> "" Then

            If txtDisplayRecordsLocationwise.Text = "N" Then
                qry = "SELECT * FROM tblcompanylocation where accountid='" & txtAccountIDtab2.Text & "'"

                'If String.IsNullOrEmpty(txtSearch.Text) = False Then
                qry = qry + " and (locationid like ""%" & txtSearch.Text & "%"""
                ' or address1='" & txtSearch.Text & "' or addstreet='" & txtSearch.Text & "' or addbuilding='" & txtSearch.Text & "'or addpostal='" & txtSearch.Text & "';"
                qry = qry + " or companyid like ""%" + txtSearch.Text + "%"""
                qry = qry + " or servicename like ""%" + txtSearch.Text + "%"""
                qry = qry + " or billingnamesvc like ""%" + txtSearch.Text + "%"""
                'qry = qry + " or ContractGroup like ""%" + txtSearch.Text + "%"""
                'qry = qry + " or ContractGroup like ""%" + lContractGroupSelection + "%"""

                If String.IsNullOrEmpty(qry2) = True Then
                    qry = qry + " or ContractGroup like ""%" + txtSearch.Text + "%"""
                    'Else
                    '    qry = qry + qry1
                End If

                qry = qry + " or ContactPerson like ""%" + txtSearch.Text + "%"""
                qry = qry + " or BillContact1Svc like ""%" + txtSearch.Text + "%"""
                qry = qry + " or Comments like ""%" + txtSearch.Text + "%"""
                qry = qry + " or description like ""%" + txtSearch.Text + "%"""
                qry = qry + " or address1 like ""%" + txtSearch.Text + "%"""
                qry = qry + " or addbuilding like ""%" + txtSearch.Text + "%"""
                qry = qry + " or addstreet like ""%" + txtSearch.Text + "%"""
                qry = qry + " or addpostal like """ + txtSearch.Text + "%"""
                qry = qry + " or billemail1svc like ""%" + txtSearch.Text + "%"""
                qry = qry + " or billemail2svc like ""%" + txtSearch.Text + "%"")"

                If String.IsNullOrEmpty(qry2) = False Then
                    '    qry = qry + " or ContractGroup like ""%" + txtSearch.Text + "%"""
                    'Else
                    qry = qry + qry2
                End If
            End If
            'End If

            If txtDisplayRecordsLocationwise.Text = "Y" Then
                qry = "SELECT * FROM tblcompanylocation where accountid='" & txtAccountIDtab2.Text & "' and Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "')"

                If String.IsNullOrEmpty(txtSearch.Text) = False Then
                    qry = qry + " and (locationid like ""%" & txtSearch.Text & "%"""
                    ' or address1='" & txtSearch.Text & "' or addstreet='" & txtSearch.Text & "' or addbuilding='" & txtSearch.Text & "'or addpostal='" & txtSearch.Text & "';"
                    qry = qry + " or companyid like ""%" + txtSearch.Text + "%"""
                    qry = qry + " or servicename like ""%" + txtSearch.Text + "%"""
                    qry = qry + " or billingnamesvc like ""%" + txtSearch.Text + "%"""
                    qry = qry + " or ContractGroup like ""%" + txtSearch.Text + "%"""
                    qry = qry + " or ContactPerson like ""%" + txtSearch.Text + "%"""
                    qry = qry + " or BillContact1Svc like ""%" + txtSearch.Text + "%"""
                    qry = qry + " or Comments like ""%" + txtSearch.Text + "%"""
                    qry = qry + " or description like ""%" + txtSearch.Text + "%"""
                    qry = qry + " or address1 like ""%" + txtSearch.Text + "%"""
                    qry = qry + " or addbuilding like ""%" + txtSearch.Text + "%"""
                    qry = qry + " or addstreet like ""%" + txtSearch.Text + "%"""
                    qry = qry + " or addpostal like """ + txtSearch.Text + "%"""
                    qry = qry + " or billemail1svc like ""%" + txtSearch.Text + "%"""
                    qry = qry + " or billemail2svc like ""%" + txtSearch.Text + "%"")"

                End If
            End If
            'End If
            'End If

            MakeSvcNull()
            SqlDataSource2.SelectCommand = qry
            SqlDataSource2.DataBind()
            GridView2.DataBind()
            GridView2.SelectedIndex = 0
            GridView2_SelectedIndexChanged(sender, e)

            lblMessage.Text = "SEARCH CRITERIA : " + txtSearch.Text + " <br/>NUMBER OF RECORDS FOUND : " + GridView2.Rows.Count.ToString


            txtDetail.Text = qry
            'txtSearch.Text = "Search Here for Location Address, Postal Code or Description"
            If String.IsNullOrEmpty(txtSearch.Text) = True Then
                txtSearch.Text = "Search Here for Location Address, Postal Code or Description"
            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "btnGoCust_Click", ex.Message.ToString, qry)
        End Try

        'txtSearchText.Text = txtSearch.Text

        'If txtSearch.Text <> "Search Here for Location Address, Postal Code or Description" Then
        '    Dim qry As String
        '    Try

        '        If txtDisplayRecordsLocationwise.Text = "N" Then
        '            qry = "SELECT * FROM tblcompanylocation where accountid='" & txtAccountIDtab2.Text & "'"

        '            If String.IsNullOrEmpty(txtSearch.Text) = False Then
        '                qry = qry + " and (locationid like ""%" & txtSearch.Text & "%"""
        '                ' or address1='" & txtSearch.Text & "' or addstreet='" & txtSearch.Text & "' or addbuilding='" & txtSearch.Text & "'or addpostal='" & txtSearch.Text & "';"
        '                qry = qry + " or companyid like ""%" + txtSearch.Text + "%"""
        '                qry = qry + " or servicename like ""%" + txtSearch.Text + "%"""
        '                qry = qry + " or billingnamesvc like ""%" + txtSearch.Text + "%"""
        '                qry = qry + " or ContractGroup like ""%" + txtSearch.Text + "%"""
        '                qry = qry + " or ContactPerson like ""%" + txtSearch.Text + "%"""
        '                qry = qry + " or BillContact1Svc like ""%" + txtSearch.Text + "%"""
        '                qry = qry + " or Comments like ""%" + txtSearch.Text + "%"""
        '                qry = qry + " or description like ""%" + txtSearch.Text + "%"""
        '                qry = qry + " or address1 like ""%" + txtSearch.Text + "%"""
        '                qry = qry + " or addbuilding like ""%" + txtSearch.Text + "%"""
        '                qry = qry + " or addstreet like ""%" + txtSearch.Text + "%"""
        '                qry = qry + " or addpostal like """ + txtSearch.Text + "%"""
        '                qry = qry + " or billemail1svc like ""%" + txtSearch.Text + "%"""
        '                qry = qry + " or billemail2svc like ""%" + txtSearch.Text + "%"")"

        '            End If
        '        End If

        '        If txtDisplayRecordsLocationwise.Text = "Y" Then
        '            qry = "SELECT * FROM tblcompanylocation where accountid='" & txtAccountIDtab2.Text & "' and Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "')"

        '            If String.IsNullOrEmpty(txtSearch.Text) = False Then
        '                qry = qry + " and (locationid like ""%" & txtSearch.Text & "%"""
        '                ' or address1='" & txtSearch.Text & "' or addstreet='" & txtSearch.Text & "' or addbuilding='" & txtSearch.Text & "'or addpostal='" & txtSearch.Text & "';"
        '                qry = qry + " or companyid like ""%" + txtSearch.Text + "%"""
        '                qry = qry + " or servicename like ""%" + txtSearch.Text + "%"""
        '                qry = qry + " or billingnamesvc like ""%" + txtSearch.Text + "%"""
        '                qry = qry + " or ContractGroup like ""%" + txtSearch.Text + "%"""
        '                qry = qry + " or ContactPerson like ""%" + txtSearch.Text + "%"""
        '                qry = qry + " or BillContact1Svc like ""%" + txtSearch.Text + "%"""
        '                qry = qry + " or Comments like ""%" + txtSearch.Text + "%"""
        '                qry = qry + " or description like ""%" + txtSearch.Text + "%"""
        '                qry = qry + " or address1 like ""%" + txtSearch.Text + "%"""
        '                qry = qry + " or addbuilding like ""%" + txtSearch.Text + "%"""
        '                qry = qry + " or addstreet like ""%" + txtSearch.Text + "%"""
        '                qry = qry + " or addpostal like """ + txtSearch.Text + "%"""
        '                qry = qry + " or billemail1svc like ""%" + txtSearch.Text + "%"""
        '                qry = qry + " or billemail2svc like ""%" + txtSearch.Text + "%"")"

        '            End If
        '        End If

        '        MakeSvcNull()
        '        SqlDataSource2.SelectCommand = qry
        '        SqlDataSource2.DataBind()
        '        GridView2.DataBind()
        '        lblMessage.Text = "SEARCH CRITERIA : " + txtSearch.Text + " <br/>NUMBER OF RECORDS FOUND : " + GridView2.Rows.Count.ToString


        '        txtDetail.Text = qry
        '    Catch ex As Exception
        '        InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "btnGoCust_Click", ex.Message.ToString, qry)
        '    End Try
        'Else
        '    txtSearch.Text = "Search Here for Location Address, Postal Code or Description"
        'End If

    End Sub

    Protected Sub PreviewFile(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim filePath As String = CType(sender, LinkButton).CommandArgument
            Dim ext As String = Path.GetExtension(filePath)
            filePath = "Uploads/Customer/" + filePath
            ext = ext.ToLower

            '  filePath = Server.MapPath("~/Uploads/") + filePath
            '    frmWord.Attributes["src"] = http://localhost/MyApp/resume.doc;
            ' iframeid.Attributes.Add("src", Server.HtmlDecode("D:\1_CWBInfotech\A_Sitapest\Program\Sitapest\Uploads\10000145_photo (1).JPG"))
            If ext = ".doc" Or ext = ".docx" Or ext = ".xls" Or ext = ".xlsx" Then
                Dim strFilePath As String = Server.MapPath("Uploads\Customer\")
                Dim strFile As String = CType(sender, LinkButton).CommandArgument
                Dim File As String() = strFile.Split("."c)
                Dim strExtension As String = ext
                Dim strUrl As String = "http://" + Request.Url.Authority + "/WordinIFrame/ConvertedLocation/"

                Dim Filename As String = strFilePath + strFile.Split("."c)(0) & Convert.ToString(".html")

                If System.IO.File.Exists(Filename) Then
                    System.IO.File.Delete(Filename)
                End If

                If ext = ".doc" Or ext = ".docx" Then
                    ConvertHTMLFromWord(strFilePath & strFile, strFilePath + "A" + strFile.Split("."c)(0) & Convert.ToString(".html"))

                ElseIf ext = ".xls" Or ext = ".xlsx" Then
                    ConvertHtmlFromExcel(strFilePath + strFile, strFilePath + "A" + strFile.Split("."c)(0) + ".html")
                End If

                iframeid.Attributes("src") = "Uploads/Customer/A" + strFile.Split("."c)(0) & Convert.ToString(".html")

            Else
                iframeid.Attributes.Add("src", filePath)

            End If
            '  iframeid.Attributes.Add("src", "https://docs.google.com/viewer?url={D:/1_CWBInfotech/A_Sitapest/Program/Sitapest/Uploads/10000145_ActualVsForecast_Format1.pdf?pid=explorer&efh=false&a=v&chrome=false&embedded=true")
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "PreviewFile", ex.Message.ToString, "")
        End Try
    End Sub

    Public Sub ConvertHTMLFromWord(Source As Object, Target As Object)
        If Word Is Nothing Then
            ' Check for the prior instance of the OfficeWord Object
            Word = New Microsoft.Office.Interop.Word.ApplicationClass()
        End If

        Try
            ' To suppress window display the following code will help
            Word.Visible = False
            Word.Application.Visible = False
            Word.WindowState = Microsoft.Office.Interop.Word.WdWindowState.wdWindowStateMinimize



            Word.Documents.Open(Source, Unknown, Unknown, Unknown, Unknown, Unknown, _
                Unknown, Unknown, Unknown, Unknown, Unknown, Unknown, _
                Unknown, Unknown, Unknown, Unknown)

            Dim format As Object = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatHTML

            Word.ActiveDocument.SaveAs(Target, format, Unknown, Unknown, Unknown, Unknown, _
                Unknown, Unknown, Unknown, Unknown, Unknown, Unknown, _
                Unknown, Unknown, Unknown, Unknown)

            Status = StatusType.SUCCESS
            Message = Status.ToString()
        Catch e As Exception
            Message = "Error :" + e.Message.ToString().Trim()
        Finally
            If Word IsNot Nothing Then
                Word.Documents.Close(Unknown, Unknown, Unknown)
                Word.Quit(Unknown, Unknown, Unknown)
            End If
        End Try
    End Sub

    Public Sub ConvertHtmlFromExcel(Source As String, Target As String)
        If Excel Is Nothing Then
            Excel = New Microsoft.Office.Interop.Excel.ApplicationClass()
        End If

        Try
            Excel.Visible = False
            Excel.Application.Visible = False
            Excel.WindowState = Microsoft.Office.Interop.Excel.XlWindowState.xlMinimized

            Excel.Workbooks.Open(Source, Unknown, Unknown, Unknown, Unknown, Unknown, _
                Unknown, Unknown, Unknown, Unknown, Unknown, Unknown, _
                Unknown, Unknown, Unknown)

            Dim format As Object = Microsoft.Office.Interop.Excel.XlFileFormat.xlHtml

            Excel.Workbooks(1).SaveAs(Target, format, Unknown, Unknown, Unknown, Unknown, _
                Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Unknown, Unknown, Unknown, Unknown, Unknown)

            Status = StatusType.SUCCESS

            Message = Status.ToString()
        Catch e As Exception
            Message = "Error :" + e.Message.ToString().Trim()
        Finally
            If Excel IsNot Nothing Then
                Excel.Workbooks.Close()
                Excel.Quit()
            End If
        End Try
    End Sub

    Protected Sub btnTransactions_Click(sender As Object, e As EventArgs) Handles btnTransactions.Click
        Session.Add("AccountID", txtAccountID.Text)
        Session.Add("customerfrom", "Corporate")


        Session("contracttype") = "CORPORATE"
        Session("AccountTypeSOA") = "COMPANY"
        Session("companygroup") = ddlCompanyGrp.Text
        Session("accountid") = txtAccountID.Text.Trim
        Session("custname") = txtNameE.Text
        Session("postal") = txtOffPostal.Text
        Session("sevaddress") = txtAddress.Text
        Session("locategrp") = ddlLocateGrp.Text
        Session("salesman") = ddlSalesMan.Text


        Session("offaddress1") = txtOffAddress1.Text
        Session("offstreet") = txtOffStreet.Text
        Session("offbuilding") = txtOffBuilding.Text



        If (ddlOffCity.Text.Trim) = "-1" Then
            Session("offcity") = ""
        Else
            Session("offcity") = ddlBillCity.Text
        End If
        'Session("offcity") = ddlOffCity.Text
        Session("offpostal") = txtOffPostal.Text

        Session("billaddress1") = txtBillAddress.Text
        Session("billstreet") = txtBillStreet.Text
        Session("billbuilding") = txtBillBuilding.Text

        If (ddlBillCity.Text.Trim) = "-1" Then
            Session("billcity") = ""
        Else
            Session("billcity") = ddlBillCity.Text
        End If

        'Session("billcity") = ddlBillCity.Text
        Session("billpostal") = txtBillPostal.Text

        Session("industry") = ddlIndustry.Text

        'If String.IsNullOrEmpty(Session("contractfrom")) = False Then
        '    Session("clientid") = txtID.Text

        'End If

        Session("gridsqlCompany") = txt.Text
        Session("rcno") = txtRcno.Text

        Session("gridsqlCompanyDetail") = txtDetail.Text
        Session("rcnoDetail") = txtSvcRcno.Text

        ddlFilter.Text = "ALL TRANSACTIONS"


        ddlFilter_SelectedIndexChanged(sender, e)
        If grdViewInvoiceDetails.Rows.Count = 0 Then
            lblAlertTransactions.Text = "THERE ARE NO TRANSACTIONS IN THIS ACCOUNT"
            ddlFilter.Visible = False
        Else
            lblAlertTransactions.Text = ""
            ddlFilter.Visible = True

            ddlFilter.Text = "SALES INVOICE (OUTSTANDING)"
            ddlFilter_SelectedIndexChanged(sender, e)
        End If
        '   UpdateTransactions()
        lblTotal.Text = ""
        lblCurDate.Text = "As at " + Convert.ToString(Session("SysDate"))

        'Session.Add("PrintDate", Convert.ToDateTime(txtCutOffDate.ToString("yyyy-MM-dd")))


        ModalPopupInvoice.Show()

    End Sub


    'Protected Sub btnViewTransSummary_Click(sender As Object, e As EventArgs) Handles btnViewTransSummary.Click
    '    Dim selFormula As String
    '    selFormula = "{tblar1.rcno} <> 0 and {tblar1.GLType}='DEBTOR'"
    '    selFormula = selFormula + " and {tblar1.AccountID} = '" + txtAccountIDSelected.Text + "'"
    '    Session.Add("selFormula", selFormula)
    '    Session.Add("AccountID", txtAccountID.Text)

    '    Response.Redirect("RV_TransactionSummary.aspx")

    'End Sub

    Private Sub UpdateTransactions()

        Dim qry As String = ""
        Dim qryTotal As String = ""

        If ddlFilter.SelectedValue.ToString = "ALL TRANSACTIONS" Then
            qry = "(SELECT salesdate as VoucherDate,'INVOICE' as Type,invoicenumber as VoucherNumber,AppliedBase as Amount FROM tblsales WHERE poststatus='P' and doctype='ARIN' AND (AccountId = '" + txtAccountID.Text + "')) union (select RECEIPTDATE as VoucherDate,'RECEIPTS' as Type,receiptnumber as VoucherNumber,BaseAmount as Amount from tblrecv WHERE poststatus='P' AND (AccountId = '" + txtAccountID.Text + "')) union (SELECT salesdate as VoucherDate,'CN' as Type,invoicenumber as VoucherNumber,AppliedBase as Amount FROM tblsales WHERE poststatus='P' and doctype='ARCN' AND (AccountId = '" + txtAccountID.Text + "')) union (SELECT salesdate as VoucherDate,'DN' as Type,invoicenumber as VoucherNumber,AppliedBase as Amount FROM tblsales WHERE poststatus='P' and doctype='ARDN' AND (AccountId = '" + txtAccountID.Text + "')) union all (SELECT tbljrnv.JournalDate as VoucherDate,'JOURNAL' as Type,tbljrnvdet.VoucherNumber,-tbljrnvdet.cREDITBase as Amount from tbljrnvdet left outer join tbljrnv on tbljrnvdet.vouchernumber=tbljrnv.vouchernumber where PostStatus='P' AND (AccountId = '" + txtAccountID.Text + "')) ORDER BY VoucherDate desc"
            qryTotal = "SELECT ifnull(Sum(AppliedBase),0) as totalamountinvoice FROM tblsales WHERE poststatus='P' and doctype='ARIN' AND (AccountId = '" + txtAccountID.Text + "') union select ifnull(Sum(BaseAmount),0) as totalamountreceipt from tblrecv WHERE poststatus='P' AND (AccountId = '" + txtAccountID.Text + "') union SELECT ifnull(Sum(AppliedBase),0) as totalamountCN FROM tblsales WHERE poststatus='P' and doctype='ARCN' AND (AccountId = '" + txtAccountID.Text + "') union SELECT ifnull(Sum(AppliedBase),0) as totalamountDN FROM tblsales WHERE poststatus='P' and doctype='ARDN' AND (AccountId = '" + txtAccountID.Text + "')"
        ElseIf ddlFilter.SelectedValue.ToString = "SALES INVOICE" Then
            qry = "SELECT salesdate as VoucherDate,'INVOICE' as Type,invoicenumber as VoucherNumber,AppliedBase as Amount FROM tblsales WHERE poststatus='P' and doctype='ARIN' AND (AccountId = '" + txtAccountID.Text + "') ORDER BY VoucherDate desc"
            qryTotal = "SELECT ifnull(Sum(AppliedBase),0) as totalamount FROM tblsales WHERE poststatus='P' and doctype='ARIN' AND (AccountId = '" + txtAccountID.Text + "')"
            'ElseIf ddlFilter.SelectedValue.ToString = "SALES INVOICE (OUTSTANDING)" Then
            '    qry = "SELECT salesdate as VoucherDate,'INVOICE' as Type,invoicenumber as VoucherNumber,BalanceBase as Amount FROM tblsales WHERE poststatus='P' and doctype='ARIN' AND (AccountId = '" + txtAccountID.Text + "') and balancebase<>0 ORDER BY VoucherDate desc"
            '    qryTotal = "SELECT ifnull(Sum(BalanceBase),0) as totalamount FROM tblsales WHERE poststatus='P' and doctype='ARIN' AND (AccountId = '" + txtAccountID.Text + "') and balancebase<>0"
        ElseIf ddlFilter.SelectedValue.ToString = "SALES INVOICE (OUTSTANDING)" Then
            qry = "SELECT salesdate as VoucherDate,'INVOICE' as Type,invoicenumber as VoucherNumber,BalanceBase as Amount FROM tblsales WHERE poststatus='P' and doctype='ARIN' AND (AccountId = '" + txtAccountID.Text + "') and balancebase<>0 ORDER BY VoucherDate desc"
            qryTotal = "SELECT ifnull(Sum(BalanceBase),0) as totalamount FROM tblsales WHERE poststatus='P' AND (AccountId = '" + txtAccountID.Text + "') and balancebase<>0"

        ElseIf ddlFilter.SelectedValue.ToString = "RECEIPT" Then
            qry = "select RECEIPTDATE as VoucherDate,'RECEIPTS' as Type,receiptnumber as VoucherNumber,BaseAmount as Amount from tblrecv WHERE poststatus='P' AND (AccountId = '" + txtAccountID.Text + "') ORDER BY VoucherDate desc"
            qryTotal = "select ifnull(Sum(BaseAmount),0) as totalamount from tblrecv WHERE poststatus='P' AND (AccountId = '" + txtAccountID.Text + "')"
        ElseIf ddlFilter.SelectedValue.ToString = "CREDIT NOTE" Then
            qry = "SELECT salesdate as VoucherDate,'CN' as Type,invoicenumber as VoucherNumber,AppliedBase as Amount FROM tblsales WHERE poststatus='P' and doctype='ARCN' AND (AccountId = '" + txtAccountID.Text + "') ORDER BY VoucherDate desc"
            qryTotal = "SELECT ifnull(Sum(AppliedBase),0) as totalamount FROM tblsales WHERE poststatus='P' and doctype='ARCN' AND (AccountId = '" + txtAccountID.Text + "')"
        ElseIf ddlFilter.SelectedValue.ToString = "DEBIT NOTE" Then
            qry = "SELECT salesdate as VoucherDate,'DN' as Type,invoicenumber as VoucherNumber,AppliedBase as Amount FROM tblsales WHERE poststatus='P' and doctype='ARDN' AND (AccountId = '" + txtAccountID.Text + "') ORDER BY VoucherDate desc"
            qryTotal = "SELECT ifnull(Sum(AppliedBase),0) as totalamount FROM tblsales WHERE poststatus='P' and doctype='ARDN' AND (AccountId = '" + txtAccountID.Text + "')"
        ElseIf ddlFilter.SelectedValue.ToString = "ADJUSTMENT" Then
            qry = "SELECT tbljrnv.JournalDate as VoucherDate,'JOURNAL' as Type,tbljrnvdet.VoucherNumber,-tbljrnvdet.cREDITBase as Amount from tbljrnvdet left outer join tbljrnv on tbljrnvdet.vouchernumber=tbljrnv.vouchernumber where PostStatus='P' AND (AccountId = '" + txtAccountID.Text + "')"
            qryTotal = "SELECT ifnull(Sum(-tbljrnvdet.cREDITBase),0) as totalamount from tbljrnvdet left outer join tbljrnv on tbljrnvdet.vouchernumber=tbljrnv.vouchernumber where PostStatus='P' AND (AccountId = '" + txtAccountID.Text + "')"

        End If

        SqlDSInvoiceDetails.SelectCommand = qry
        SqlDSInvoiceDetails.DataBind()
        grdViewInvoiceDetails.DataBind()


        If ddlFilter.SelectedValue.ToString = "ALL TRANSACTIONS" Then
            'If dt.Columns.Contains("totalamountinvoice") Then
            '    tot = tot + dt.Rows(0)("totalamountinvoice")
            'ElseIf dt.Columns.Contains("totalamountreceipt") Then
            '    tot = tot + dt.Rows(0)("totalamountreceipt")
            'ElseIf dt.Columns.Contains("totalamountCN") Then
            '    tot = tot + dt.Rows(0)("totalamountCN")
            'ElseIf dt.Columns.Contains("totalamountDN") Then
            '    tot = tot + dt.Rows(0)("totalamountDN")
            'End If

            lblTotal.Text = ""

        Else
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                command1.CommandText = qryTotal
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New System.Data.DataTable
                dt.Load(dr)
                Dim tot As Decimal = 0

                If dt.Rows.Count > 0 Then
                    tot = dt.Rows(0)("totalamount")
                    lblTotal.Text = "Total    : " + tot.ToString("N2")
                End If
                conn.Close()
                conn.Dispose()
                command1.Dispose()
                dt.Dispose()
                dr.Close()

            Catch ex As Exception
                InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "UpdateTransactions", ex.Message.ToString, qryTotal)
            End Try
        End If


    End Sub

    Protected Sub grdViewInvoiceDetails_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdViewInvoiceDetails.PageIndexChanging
        grdViewInvoiceDetails.PageIndex = e.NewPageIndex
        'SqlDSInvoiceDetails.SelectCommand = "SELECT salesdate as VoucherDate,'INVOICE' as Type,invoicenumber as VoucherNumber,AppliedBase as Amount FROM tblsales WHERE poststatus='P' and doctype='ARIN' AND (AccountId = '" + txtAccountIDSelected.Text + "') union select RECEIPTDATE as VoucherDate,'RECEIPTS' as Type,receiptnumber as VoucherNumber,BaseAmount as Amount from tblrecv WHERE poststatus='P' AND (AccountId = '" + txtAccountIDSelected.Text + "') union SELECT salesdate as VoucherDate,'CN' as Type,invoicenumber as VoucherNumber,AppliedBase as Amount FROM tblsales WHERE poststatus='P' and doctype='ARCN' AND (AccountId = '" + txtAccountIDSelected.Text + "') union SELECT salesdate as VoucherDate,'DN' as Type,invoicenumber as VoucherNumber,AppliedBase as Amount FROM tblsales WHERE poststatus='P' and doctype='ARDN' AND (AccountId = '" + txtAccountIDSelected.Text + "') ORDER BY VoucherDate"
        'SqlDSInvoiceDetails.DataBind()
        'grdViewInvoiceDetails.DataBind()

        UpdateTransactions()

        Session.Add("customerfrom", "Corporate")
        ModalPopupInvoice.Show()

    End Sub

    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='silver';this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#E4E4E4';")
            'e.Row.Attributes.Add("onmousedown", "this.style.backgroundColor='#738A9C';")
            'e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#738A9C';")

            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."

            If txtDisplayRecordsLocationwise.Text = "N" Then
                e.Row.Cells(11).Visible = False
                GridView1.HeaderRow.Cells(11).Visible = False
            Else
                e.Row.Cells(11).Visible = True
                GridView1.HeaderRow.Cells(11).Visible = True
            End If
        End If
    End Sub

    Protected Sub OnSelectedIndexChanged(sender As Object, e As EventArgs)

        'For Each row As GridViewRow In GridView1.Rows
        '    If row.RowIndex = GridView2.SelectedIndex Then
        '        row.BackColor = ColorTranslator.FromHtml("#00ccff")
        '        row.ToolTip = String.Empty
        '    Else
        '        row.BackColor = ColorTranslator.FromHtml("#ffffff")
        '        row.ToolTip = "Click to select this row."
        '    End If

        '    If String.IsNullOrEmpty(txtSelectedIndex.Text) = True Then
        '        If row.RowIndex = GridView1.SelectedIndex Then
        '            row.BackColor = ColorTranslator.FromHtml("#00ccff")
        '            row.ToolTip = String.Empty
        '        Else
        '            If row.RowIndex Mod 2 = 0 Then
        '                row.BackColor = ColorTranslator.FromHtml("#EFF3FB")
        '                row.ToolTip = "Click to select this row."
        '            Else
        '                row.BackColor = ColorTranslator.FromHtml("#ffffff")
        '                row.ToolTip = "Click to select this row."
        '            End If
        '        End If


        '    Else
        '        If Convert.ToInt32(txtSelectedIndex.Text) >= 0 Then
        '            If row.RowIndex = txtSelectedIndex.Text Then
        '                row.BackColor = ColorTranslator.FromHtml("#00ccff")
        '                row.ToolTip = String.Empty
        '                txtSelectedIndex.Text = ""
        '            Else
        '                row.BackColor = ColorTranslator.FromHtml("#ffffff")
        '                row.ToolTip = "Click to select this row."
        '            End If

        '        End If

        '    End If
        'Next

        For Each row As GridViewRow In GridView1.Rows
            If row.RowIndex = GridView1.SelectedIndex Then
                row.BackColor = ColorTranslator.FromHtml("#AEE4FF")
                row.ToolTip = String.Empty
            Else
                If row.RowIndex Mod 2 = 0 Then
                    row.BackColor = ColorTranslator.FromHtml("#EFF3FB")
                    row.ToolTip = "Click to select this row."
                Else
                    row.BackColor = ColorTranslator.FromHtml("#ffffff")
                    row.ToolTip = "Click to select this row."
                End If
            End If
        Next
    End Sub

    Protected Sub OnRowDataBound2(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='silver';this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#E4E4E4';")
            'e.Row.Attributes.Add("onmousedown", "this.style.backgroundColor='#738A9C';")
            'e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#738A9C';")

            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridView2, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Protected Sub OnSelectedIndexChanged2(sender As Object, e As EventArgs) Handles GridView2.SelectedIndexChanged
        For Each row As GridViewRow In GridView2.Rows
            If row.RowIndex = GridView2.SelectedIndex Then
                row.BackColor = ColorTranslator.FromHtml("#00ccff")
                row.ToolTip = String.Empty
            Else
                row.BackColor = ColorTranslator.FromHtml("#ffffff")
                row.ToolTip = "Click to select this row."
            End If

            If String.IsNullOrEmpty(txtSelectedIndex.Text) = True Then
                If row.RowIndex = GridView2.SelectedIndex Then
                    row.BackColor = ColorTranslator.FromHtml("#00ccff")
                    row.ToolTip = String.Empty
                Else
                    If row.RowIndex Mod 2 = 0 Then
                        row.BackColor = ColorTranslator.FromHtml("#EFF3FB")
                        row.ToolTip = "Click to select this row."
                    Else
                        row.BackColor = ColorTranslator.FromHtml("#ffffff")
                        row.ToolTip = "Click to select this row."
                    End If
                End If


            Else
                If Convert.ToInt32(txtSelectedIndex.Text) >= 0 Then
                    If row.RowIndex = txtSelectedIndex.Text Then
                        row.BackColor = ColorTranslator.FromHtml("#00ccff")
                        row.ToolTip = String.Empty
                        txtSelectedIndex.Text = ""
                    Else
                        row.BackColor = ColorTranslator.FromHtml("#ffffff")
                        row.ToolTip = "Click to select this row."
                    End If

                    'If row.RowIndex = txtSelectedIndex.Text Then
                    '    row.BackColor = ColorTranslator.FromHtml("#00ccff")
                    '    row.ToolTip = String.Empty
                    'Else
                    '    If row.RowIndex Mod 2 = 0 Then
                    '        row.BackColor = ColorTranslator.FromHtml("#EFF3FB")
                    '        row.ToolTip = "Click to select this row."
                    '    Else
                    '        row.BackColor = ColorTranslator.FromHtml("#ffffff")
                    '        row.ToolTip = "Click to select this row."
                    '    End If
                    'End If
                End If

            End If
        Next
    End Sub

    Protected Sub tb1_ActiveTabChanged(sender As Object, e As EventArgs) Handles tb1.ActiveTabChanged
        Try
            If tb1.ActiveTabIndex <> 0 Then
                If txtMode.Text = "NEW" Or txtMode.Text = "EDIT" Then
                    lblAlert.Text = "Cannot switch tabs in Add or Edit Mode.. Save or Cancel the record to Proceed"
                    tb1.ActiveTabIndex = 0
                    Exit Sub
                End If
            End If
            If tb1.ActiveTabIndex <> 1 Then
                If txtSvcMode.Text = "NEW" Or txtSvcMode.Text = "EDIT" Then
                    lblAlert.Text = "Cannot switch tabs in Add or Edit Mode.. Save or Cancel the record to Proceed"
                    tb1.ActiveTabIndex = 1
                    Exit Sub
                End If
            End If
            If tb1.ActiveTabIndex <> 3 Then
                If txtNotesMode.Text = "NEW" Or txtNotesMode.Text = "EDIT" Then
                    lblAlert.Text = "Cannot switch tabs in Add or Edit Mode.. Save or Cancel the record to Proceed"
                    tb1.ActiveTabIndex = 3
                    Exit Sub
                End If
            End If


            If tb1.ActiveTabIndex = 1 Then
                If GridView2.Rows.Count = 0 Then
                    txtSelectedIndex.Text = "-1"
                Else
                    txtSelectedIndex.Text = "0"
                End If

                GridView2_SelectedIndexChanged(New Object(), New EventArgs)
                OnSelectedIndexChanged2(New Object(), New EventArgs)
            ElseIf tb1.ActiveTabIndex = 2 Then


                iframeid.Attributes.Add("src", "about:blank")

                'View Uploaded files

                'Dim myDir As New DirectoryInfo(Server.MapPath("~/Uploads/Customer/"))
                'Dim filesInDir As FileInfo() = myDir.GetFiles((Convert.ToString(txtAccountID.Text + "_")) + "*.*")
                'Dim files As List(Of ListItem) = New List(Of ListItem)

                'For Each foundFile As FileInfo In filesInDir
                '    Dim fullName As String = foundFile.FullName
                '    files.Add(New ListItem(foundFile.Name))
                'Next


                'gvUpload.DataSource = files
                'gvUpload.DataBind()

                SqlDSUpload.SelectCommand = "select * from tblfileupload where fileref = '" + txtAccountID.Text + "'"
                gvUpload.DataSourceID = "SqlDSUpload"
                gvUpload.DataBind()

                lblFileUploadCount.Text = "File Upload [" & gvUpload.Rows.Count & "]"
                'updPanelContract1.Update()

            ElseIf tb1.ActiveTabIndex = 3 Then
                lblNotesKeyField.Text = txtAccountID.Text
                lblNotesStaffID.Text = txtCreatedBy.Text
            ElseIf tb1.ActiveTabIndex = 4 Then
                txtAccountIDCP.Text = txtAccountID.Text

                If String.IsNullOrEmpty(txtAccountID.Text) = True Then
                    lblAlert.Text = "Please Select an Account ID"
                    tb1.ActiveTabIndex = 0
                    Exit Sub
                End If

                SqlDSCP.SelectCommand = "SELECT * FROM tblCompanyCustomerAccess where AccountID = '" & txtAccountID.Text & "'"
                gvCP.DataSourceID = "SqlDSCP"
                gvCP.DataBind()

            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "tb1_ActiveTabChanged", ex.Message.ToString, "")
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Protected Sub btnUpdateBilling_Click(sender As Object, e As EventArgs) Handles btnUpdateBilling.Click
        Try
            If txtRcno.Text = "" Then
                ' MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
                lblAlert.Text = "SELECT RECORD TO EDIT"
                '   mdlPopupEditBilling.Show()
                Return

            End If

            txtEditBillingName.Text = txtBillingName.Text
            txtEditBillAddress.Text = txtBillAddress.Text
            txtEditBillStreet.Text = txtBillStreet.Text
            txtEditBillBuilding.Text = txtBillBuilding.Text
            ddlEditBillCity.SelectedIndex = ddlBillCity.SelectedIndex
            ddlEditBillState.SelectedIndex = ddlBillState.SelectedIndex
            ddlEditBillCountry.SelectedIndex = ddlBillCountry.SelectedIndex
            txtEditBillPostal.Text = txtBillPostal.Text

            txtEditBillCP1Contact.Text = txtBillCP1Contact.Text
            txtEditBillCP1Position.Text = txtBillCP1Position.Text
            txtEditBillCP1Tel.Text = txtBillCP1Tel.Text
            txtEditBillCP1Fax.Text = txtBillCP1Fax.Text
            txtEditBillCP1Tel2.Text = txtBillCP1Tel2.Text
            txtEditBillCP1Mobile.Text = txtBillCP1Mobile.Text
            txtEditBillCP1Fax.Text = txtBillCP1Fax.Text
            txtEditBillCP1Email.Text = txtBillCP1Email.Text

            txtEditBillCP2Contact.Text = txtBillCP2Contact.Text
            txtEditBillCP2Position.Text = txtBillCP2Position.Text
            txtEditBillCP2Tel.Text = txtBillCP2Tel.Text
            txtEditBillCP2Fax.Text = txtBillCP2Fax.Text
            txtEditBillCP2Tel2.Text = txtBillCP2Tel2.Text
            txtEditBillCP2Mobile.Text = txtBillCP2Mobile.Text
            txtEditBillCP2Fax.Text = txtBillCP2Fax.Text
            txtEditBillCP2Email.Text = txtBillCP2Email.Text
            chkUpdateBillingAll.Checked = False

            mdlPopupEditBilling.Show()
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "btnUpdateBilling_Click", ex.Message.ToString, "")
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)

        End Try
    End Sub

    Protected Sub btnEditBillingSave_Click(sender As Object, e As EventArgs) Handles btnEditBillingSave.Click
        If String.IsNullOrEmpty(txtEditBillingName.Text.Trim) = True Then
            lblAlert.Text = "BILLING NAME CANNOT BE BLANK"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            txtEditBillingName.Focus()
            mdlPopupEditBilling.Show()

            Return
        End If


        If String.IsNullOrEmpty(txtEditBillAddress.Text.Trim) = True Then
            lblAlert.Text = "BILL STREET ADDRESS1 CANNOT BE BLANK"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            txtEditBillAddress.Focus()
            mdlPopupEditBilling.Show()


            Return
        End If

        'If txtEditBillCP1Contact.Text = "" Then
        If String.IsNullOrEmpty(txtEditBillCP1Contact.Text.Trim) = True Then
            lblAlert.Text = "BILLING CONTACT PERSON 1 CANNOT BE BLANK"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            txtEditBillCP1Contact.Focus()
            mdlPopupEditBilling.Show()


            Return
        End If


        If txtRcno.Text = "" Then
            ' MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
            lblAlert.Text = "SELECT RECORD TO EDIT"
            mdlPopupEditBilling.Show()
            Return

        End If
        Try
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()


            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.CommandText = "SELECT * FROM tblcompany where rcno=" & Convert.ToInt32(txtRcno.Text)
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New System.Data.DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then

                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                Dim qry As String = "UPDATE tblcompany SET BillBuilding = @BillBuilding,BillStreet = @BillStreet,BillPostal = @BillPostal,BillState = @BillState,BillCity = @BillCity,BillCountry = @BillCountry,"
                qry = qry + "BillAddress1 = @BillAddress1,LastModifiedBy = @LastModifiedBy,LastModifiedOn = @LastModifiedOn,BillTelephone = @BillTelephone,BillFax = @BillFax,"
                '  qry = qry + "BillAddress1 = @BillAddress1,BillTelephone = @BillTelephone,BillFax = @BillFax,"

                qry = qry + "BillContactPerson = @BillContactPerson,BillTelephone2 = @BillTelephone2,BillMobile = @BillMobile,BillContact1Position=@BillContact1Position,BillContact2=@BillContact2,"
                qry = qry + "BillContact2Position=@BillContact2Position,BillContact1Email=@BillContact1Email,BillContact2Email=@BillContact2Email,BillContact2Tel=@BillContact2Tel,BillContact2Fax=@BillContact2Fax,"
                qry = qry + "BillContact2Tel2=@BillContact2Tel2,BillContact2Mobile=@BillContact2Mobile,BillingName=@BillingName WHERE  rcno=" & Convert.ToInt32(txtRcno.Text)

                command.CommandText = qry
                command.Parameters.Clear()

                If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                    command.Parameters.AddWithValue("@BillBuilding", txtEditBillBuilding.Text.ToUpper)
                    command.Parameters.AddWithValue("@BillStreet", txtEditBillStreet.Text.ToUpper)
                    command.Parameters.AddWithValue("@BillPostal", txtEditBillPostal.Text.ToUpper)
                    If ddlEditBillState.Text = txtDDLText.Text Then
                        command.Parameters.AddWithValue("@BillState", "")
                    Else
                        command.Parameters.AddWithValue("@BillState", ddlEditBillState.Text.ToUpper)
                    End If
                    If ddlEditBillCity.Text = txtDDLText.Text Then
                        command.Parameters.AddWithValue("@BillCity", "")
                    Else
                        command.Parameters.AddWithValue("@BillCity", ddlEditBillCity.Text.ToUpper)
                    End If
                    If ddlEditBillCountry.Text = txtDDLText.Text Then
                        command.Parameters.AddWithValue("@BillCountry", "")
                    Else
                        command.Parameters.AddWithValue("@BillCountry", ddlEditBillCountry.Text.ToUpper)
                    End If


                    command.Parameters.AddWithValue("@BillAddress1", txtEditBillAddress.Text.ToUpper)


                    command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                    command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                    command.Parameters.AddWithValue("@BillTelephone", txtEditBillCP1Tel.Text.ToUpper)
                    command.Parameters.AddWithValue("@BillFax", txtEditBillCP1Fax.Text.ToUpper)

                    command.Parameters.AddWithValue("@BillContactPerson", txtEditBillCP1Contact.Text.ToUpper)

                    command.Parameters.AddWithValue("@BillTelephone2", txtEditBillCP1Tel2.Text.ToUpper)
                    command.Parameters.AddWithValue("@BillMobile", txtEditBillCP1Mobile.Text.ToUpper)

                    command.Parameters.AddWithValue("@BillContact1Position", txtEditBillCP1Position.Text.ToUpper)
                    command.Parameters.AddWithValue("@BillContact2", txtEditBillCP2Contact.Text.ToUpper)
                    command.Parameters.AddWithValue("@BillContact2Position", txtEditBillCP2Position.Text.ToUpper)
                    command.Parameters.AddWithValue("@BillContact1Email", txtEditBillCP1Email.Text.ToUpper)
                    command.Parameters.AddWithValue("@BillContact2Email", txtEditBillCP2Email.Text.ToUpper)
                    command.Parameters.AddWithValue("@BillContact2Tel", txtEditBillCP2Tel.Text.ToUpper)
                    command.Parameters.AddWithValue("@BillContact2Fax", txtEditBillCP2Fax.Text.ToUpper)
                    command.Parameters.AddWithValue("@BillContact2Tel2", txtEditBillCP2Tel2.Text.ToUpper)
                    command.Parameters.AddWithValue("@BillContact2Mobile", txtEditBillCP2Mobile.Text.ToUpper)

                    command.Parameters.AddWithValue("@BillingName", txtEditBillingName.Text.ToUpper)

                ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                    command.Parameters.AddWithValue("@BillBuilding", txtEditBillBuilding.Text)
                    command.Parameters.AddWithValue("@BillStreet", txtEditBillStreet.Text)
                    command.Parameters.AddWithValue("@BillPostal", txtEditBillPostal.Text)
                    If ddlEditBillState.Text = txtDDLText.Text Then
                        command.Parameters.AddWithValue("@BillState", "")
                    Else
                        command.Parameters.AddWithValue("@BillState", ddlEditBillState.Text)
                    End If
                    If ddlEditBillCity.Text = txtDDLText.Text Then
                        command.Parameters.AddWithValue("@BillCity", "")
                    Else
                        command.Parameters.AddWithValue("@BillCity", ddlEditBillCity.Text)
                    End If
                    If ddlEditBillCountry.Text = txtDDLText.Text Then
                        command.Parameters.AddWithValue("@BillCountry", "")
                    Else
                        command.Parameters.AddWithValue("@BillCountry", ddlEditBillCountry.Text)
                    End If


                    command.Parameters.AddWithValue("@BillAddress1", txtEditBillAddress.Text)


                    command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                    command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                    command.Parameters.AddWithValue("@BillTelephone", txtEditBillCP1Tel.Text)
                    command.Parameters.AddWithValue("@BillFax", txtEditBillCP1Fax.Text)

                    command.Parameters.AddWithValue("@BillContactPerson", txtEditBillCP1Contact.Text)

                    command.Parameters.AddWithValue("@BillTelephone2", txtEditBillCP1Tel2.Text)
                    command.Parameters.AddWithValue("@BillMobile", txtEditBillCP1Mobile.Text)

                    command.Parameters.AddWithValue("@BillContact1Position", txtEditBillCP1Position.Text)
                    command.Parameters.AddWithValue("@BillContact2", txtEditBillCP2Contact.Text)
                    command.Parameters.AddWithValue("@BillContact2Position", txtEditBillCP2Position.Text)
                    command.Parameters.AddWithValue("@BillContact1Email", txtEditBillCP1Email.Text)
                    command.Parameters.AddWithValue("@BillContact2Email", txtEditBillCP2Email.Text)
                    command.Parameters.AddWithValue("@BillContact2Tel", txtEditBillCP2Tel.Text)
                    command.Parameters.AddWithValue("@BillContact2Fax", txtEditBillCP2Fax.Text)
                    command.Parameters.AddWithValue("@BillContact2Tel2", txtEditBillCP2Tel2.Text)
                    command.Parameters.AddWithValue("@BillContact2Mobile", txtEditBillCP2Mobile.Text)

                    command.Parameters.AddWithValue("@BillingName", txtEditBillingName.Text)
                End If

                command.Connection = conn

                command.ExecuteNonQuery()

                If chkUpdateBillingAll.Checked = True Then
                    UpdateSvcLocation(conn)
                End If

                'Update Corporate Page
                txtBillingName.Text = txtEditBillingName.Text
                txtBillAddress.Text = txtEditBillAddress.Text
                txtBillStreet.Text = txtEditBillStreet.Text
                txtBillBuilding.Text = txtEditBillBuilding.Text
                ddlBillCity.SelectedIndex = ddlEditBillCity.SelectedIndex
                ddlBillState.SelectedIndex = ddlEditBillState.SelectedIndex
                ddlBillCountry.SelectedIndex = ddlEditBillCountry.SelectedIndex
                txtBillPostal.Text = txtEditBillPostal.Text

                txtBillCP1Contact.Text = txtEditBillCP1Contact.Text
                txtBillCP1Position.Text = txtEditBillCP1Position.Text
                txtBillCP1Tel.Text = txtEditBillCP1Tel.Text
                txtBillCP1Fax.Text = txtEditBillCP1Fax.Text
                txtBillCP1Tel2.Text = txtEditBillCP1Tel2.Text
                txtBillCP1Mobile.Text = txtEditBillCP1Mobile.Text
                txtBillCP1Fax.Text = txtEditBillCP1Fax.Text
                txtBillCP1Email.Text = txtEditBillCP1Email.Text

                txtBillCP2Contact.Text = txtEditBillCP2Contact.Text
                txtBillCP2Position.Text = txtEditBillCP2Position.Text
                txtBillCP2Tel.Text = txtEditBillCP2Tel.Text
                txtBillCP2Fax.Text = txtEditBillCP2Fax.Text
                txtBillCP2Tel2.Text = txtEditBillCP2Tel2.Text
                txtBillCP2Mobile.Text = txtEditBillCP2Mobile.Text
                txtBillCP2Fax.Text = txtEditBillCP2Fax.Text
                txtBillCP2Email.Text = txtEditBillCP2Email.Text


                '  MessageBox.Message.Alert(Page, "Record updated successfully!!!", "str")
                lblMessage.Text = "EDIT: BILLING INFO SUCCESSFULLY UPDATED"
                lblAlert.Text = ""
                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CORP", txtAccountID.Text, "EDITBILLING", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtAccountID.Text, "", txtRcno.Text)

            End If

            conn.Close()
            conn.Dispose()
            command1.Dispose()
            dt.Dispose()
            dr.Close()

        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "btnEditBillingSave", ex.Message.ToString, txtAccountID.Text)
        End Try
        '   EnableControls()


        'txt.Text = "SELECT * FROM tblcompany WHERE  Inactive=0 order by rcno desc limit 100;"
        'SqlDataSource1.SelectCommand = "SELECT * FROM tblcompany WHERE  Inactive=0 order by rcno desc limit 100;"
        SqlDataSource1.SelectCommand = txt.Text
        SqlDataSource1.DataBind()
        GridView1.DataBind()
    End Sub

    Private Sub UpdateSvcLocation(conn As MySqlConnection)
        Try
            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.CommandText = "SELECT * FROM tblcompanylocation where accountid='" & txtAccountID.Text & "'"
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New System.Data.DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then
                For i As Int16 = 0 To dt.Rows.Count - 1

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "UPDATE tblcompanylocation SET BillingNameSvc = @BillingNameSvc,BillAddressSvc = @BillAddressSvc,BillStreetSvc = @BillStreetSvc,BillBuildingSvc = @BillBuildingSvc,BillCitySvc = @BillCitySvc,BillStateSvc = @BillStateSvc,BillCountrySvc = @BillCountrySvc,BillPostalSvc = @BillPostalSvc,BillContact1Svc = @BillContact1Svc,BillPosition1Svc = @BillPosition1Svc,BillTelephone1Svc = @BillTelephone1Svc,BillFax1Svc = @BillFax1Svc,Billtelephone12Svc = @Billtelephone12Svc,BillMobile1Svc = @BillMobile1Svc,BillEmail1Svc = @BillEmail1Svc,BillContact2Svc = @BillContact2Svc,BillPosition2Svc = @BillPosition2Svc,BillTelephone2Svc = @BillTelephone2Svc,BillFax2Svc = @BillFax2Svc,Billtelephone22Svc = @Billtelephone22Svc,BillMobile2Svc = @BillMobile2Svc,BillEmail2Svc = @BillEmail2Svc where accountid='" & dt.Rows(i)("Accountid").ToString & "'"
                    command.CommandText = qry
                    command.Parameters.Clear()

                    If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then
                        command.Parameters.AddWithValue("@BillingNameSvc", txtEditBillingName.Text.ToUpper)
                        command.Parameters.AddWithValue("@BillAddressSvc", txtEditBillAddress.Text.ToUpper)
                        command.Parameters.AddWithValue("@BillStreetSvc", txtEditBillStreet.Text.ToUpper)
                        command.Parameters.AddWithValue("@BillBuildingSvc", txtEditBillBuilding.Text.ToUpper)
                        If ddlEditBillCity.Text = txtDDLText.Text Then
                            command.Parameters.AddWithValue("@BillCitySvc", "")
                        Else
                            command.Parameters.AddWithValue("@BillCitySvc", ddlEditBillCity.Text.ToUpper)
                        End If
                        If ddlEditBillState.Text = txtDDLText.Text Then
                            command.Parameters.AddWithValue("@BillStateSvc", "")
                        Else
                            command.Parameters.AddWithValue("@BillStateSvc", ddlEditBillState.Text.ToUpper)
                        End If
                        If ddlEditBillCountry.Text = txtDDLText.Text Then
                            command.Parameters.AddWithValue("@BillCountrySvc", "")
                        Else
                            command.Parameters.AddWithValue("@BillCountrySvc", ddlEditBillCountry.Text.ToUpper)
                        End If
                        command.Parameters.AddWithValue("@BillPostalSvc", txtEditBillPostal.Text.ToUpper)
                        command.Parameters.AddWithValue("@BillContact1Svc", txtEditBillCP1Contact.Text.ToUpper)
                        command.Parameters.AddWithValue("@BillPosition1Svc", txtEditBillCP1Position.Text.ToUpper)
                        command.Parameters.AddWithValue("@BillTelephone1Svc", txtEditBillCP1Tel.Text.ToUpper)
                        command.Parameters.AddWithValue("@BillFax1Svc", txtEditBillCP1Fax.Text.ToUpper)
                        command.Parameters.AddWithValue("@Billtelephone12Svc", txtEditBillCP1Tel2.Text.ToUpper)
                        command.Parameters.AddWithValue("@BillMobile1Svc", txtEditBillCP1Mobile.Text.ToUpper)
                        command.Parameters.AddWithValue("@BillEmail1Svc", txtEditBillCP1Email.Text.ToUpper)
                        command.Parameters.AddWithValue("@BillContact2Svc", txtEditBillCP2Contact.Text.ToUpper)
                        command.Parameters.AddWithValue("@BillPosition2Svc", txtEditBillCP2Position.Text.ToUpper)
                        command.Parameters.AddWithValue("@BillTelephone2Svc", txtEditBillCP2Tel.Text.ToUpper)
                        command.Parameters.AddWithValue("@BillFax2Svc", txtEditBillCP2Fax.Text.ToUpper)
                        command.Parameters.AddWithValue("@Billtelephone22Svc", txtEditBillCP2Tel2.Text.ToUpper)
                        command.Parameters.AddWithValue("@BillMobile2Svc", txtEditBillCP2Mobile.Text.ToUpper)
                        command.Parameters.AddWithValue("@BillEmail2Svc", txtEditBillCP2Email.Text.ToUpper)

                    ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then
                        command.Parameters.AddWithValue("@BillingNameSvc", txtEditBillingName.Text)
                        command.Parameters.AddWithValue("@BillAddressSvc", txtEditBillAddress.Text)
                        command.Parameters.AddWithValue("@BillStreetSvc", txtEditBillStreet.Text)
                        command.Parameters.AddWithValue("@BillBuildingSvc", txtEditBillBuilding.Text)
                        If ddlEditBillCity.Text = txtDDLText.Text Then
                            command.Parameters.AddWithValue("@BillCitySvc", "")
                        Else
                            command.Parameters.AddWithValue("@BillCitySvc", ddlEditBillCity.Text)
                        End If
                        If ddlEditBillState.Text = txtDDLText.Text Then
                            command.Parameters.AddWithValue("@BillStateSvc", "")
                        Else
                            command.Parameters.AddWithValue("@BillStateSvc", ddlEditBillState.Text)
                        End If
                        If ddlEditBillCountry.Text = txtDDLText.Text Then
                            command.Parameters.AddWithValue("@BillCountrySvc", "")
                        Else
                            command.Parameters.AddWithValue("@BillCountrySvc", ddlEditBillCountry.Text)
                        End If
                        command.Parameters.AddWithValue("@BillPostalSvc", txtEditBillPostal.Text)
                        command.Parameters.AddWithValue("@BillContact1Svc", txtEditBillCP1Contact.Text)
                        command.Parameters.AddWithValue("@BillPosition1Svc", txtEditBillCP1Position.Text)
                        command.Parameters.AddWithValue("@BillTelephone1Svc", txtEditBillCP1Tel.Text)
                        command.Parameters.AddWithValue("@BillFax1Svc", txtEditBillCP1Fax.Text)
                        command.Parameters.AddWithValue("@Billtelephone12Svc", txtEditBillCP1Tel2.Text)
                        command.Parameters.AddWithValue("@BillMobile1Svc", txtEditBillCP1Mobile.Text)
                        command.Parameters.AddWithValue("@BillEmail1Svc", txtEditBillCP1Email.Text)
                        command.Parameters.AddWithValue("@BillContact2Svc", txtEditBillCP2Contact.Text)
                        command.Parameters.AddWithValue("@BillPosition2Svc", txtEditBillCP2Position.Text)
                        command.Parameters.AddWithValue("@BillTelephone2Svc", txtEditBillCP2Tel.Text)
                        command.Parameters.AddWithValue("@BillFax2Svc", txtEditBillCP2Fax.Text)
                        command.Parameters.AddWithValue("@Billtelephone22Svc", txtEditBillCP2Tel2.Text)
                        command.Parameters.AddWithValue("@BillMobile2Svc", txtEditBillCP2Mobile.Text)
                        command.Parameters.AddWithValue("@BillEmail2Svc", txtEditBillCP2Email.Text)
                    End If
                    command.Connection = conn

                    command.ExecuteNonQuery()

                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CORPSVCLOC", txtAccountID.Text, "EDITBILLING", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtLocationID.Text, "", dt.Rows(i)("Rcno"))

                Next

                SqlDataSource2.SelectCommand = "SELECT * FROM tblcompanylocation where accountid = '" & txtAccountID.Text & "'"
                SqlDataSource2.DataBind()
                GridView2.DataBind()

            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "UpdateSvcLocation", ex.Message.ToString, txtAccountID.Text)
        End Try
    End Sub

    Protected Sub btnChangeStatus_Click(sender As Object, e As EventArgs) Handles btnChangeStatus.Click
        'lblStatusAccountID.Text = txtAccountID.Text
        If chkInactive.Checked = True Then
            '  lblOldStatus.Text = "Inactive"
            lblStatusMessage.Text = "<u>INACTIVE CUSTOMER</u>" + "<br><br><br>" + "Account ID : " + txtAccountID.Text + ", Name : " + txtNameE.Text + "<br><br><br>" + "Are you sure to make the Account 'ACTIVE'?"
        Else
            ' lblOldStatus.Text = "Active"
            lblStatusMessage.Text = "<u>ACTIVE CUSTOMER</u>" + "<br><br><br>" + "Account ID : " + txtAccountID.Text + ", Name : " + txtNameE.Text + "<br><br><br>" + "Are you sure to make the Account 'INACTIVE'?"
        End If
        txtStatusRemarks.Text = ""
        lblAlertStatus.Text = ""
        mdlPopupStatus.Show()
    End Sub

    'Protected Sub btnUpdateStatus_Click(sender As Object, e As EventArgs) Handles btnUpdateStatus.Click
    '    If ddlNewStatus.Text = txtDDLText.Text Then
    '        lblAlertStatus.Text = "SELECT NEW STATUS"
    '        mdlPopupStatus.Show()

    '        Return

    '    End If
    '    If ddlNewStatus.Text = lblOldStatus.Text Then
    '        lblAlertStatus.Text = "STATUS ALREADY UPDATED"
    '        mdlPopupStatus.Show()

    '        Return
    '    End If
    '    'If ddlNewStatus.Text = txtPostStatus.Text Then
    '    '    lblAlertStatus.Text = "STATUS ALREADY UPDATED"
    '    '    mdlPopupStatus.Show()

    '    '    Return
    '    'End If

    '    Try
    '        Dim conn As MySqlConnection = New MySqlConnection()

    '        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    '        conn.Open()

    '        Dim command As MySqlCommand = New MySqlCommand

    '        command.CommandType = CommandType.Text

    '        If ddlNewStatus.SelectedIndex = 1 Then
    '            command.CommandText = "UPDATE tblCompany SET InActive= 0 where rcno=" & Convert.ToInt32(txtRcno.Text)
    '        ElseIf ddlNewStatus.SelectedIndex = 2 Then
    '            command.CommandText = "UPDATE tblCompany SET InActive= 1 where rcno=" & Convert.ToInt32(txtRcno.Text)
    '        End If

    '        'command.CommandText = "UPDATE tblCompany SET InActive='" + ddlNewStatus.SelectedValue + "' where rcno=" & Convert.ToInt32(txtRcno.Text)
    '        command.Connection = conn
    '        command.ExecuteNonQuery()

    '        '   UpdateContractActSvcDate(conn)

    '        conn.Close()
    '        'ddlStatus.Text = ddlNewStatus.Text
    '        ddlNewStatus.SelectedIndex = 0

    '        lblMessage.Text = "ACTION: STATUS UPDATED"
    '        CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CHST", txtAccountID.Text, "CHST", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtAccountID.Text, "", txtRcno.Text)
    '        'txtPostStatus.Text = ddlNewStatus.SelectedValue

    '        SqlDataSource1.SelectCommand = txt.Text
    '        SqlDataSource1.DataBind()
    '        GridView1.DataBind()

    '        If ddlNewStatus.SelectedIndex = 1 Then
    '            chkInactive.Checked = False
    '        ElseIf ddlNewStatus.SelectedIndex = 2 Then
    '            chkInactive.Checked = True
    '        End If
    '        'GridView1.DataSourceID = "SqlDataSource1"
    '        mdlPopupStatus.Hide()
    '    Catch ex As Exception
    '        MessageBox.Message.Alert(Page, ex.ToString, "str")
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
    '    End Try
    'End Sub

    Protected Sub btnConfirmYes_Click(sender As Object, e As EventArgs) Handles btnConfirmYes.Click
        If String.IsNullOrEmpty(txtStatusRemarks.Text) Then
            lblAlertStatus.Text = "ENTER REMARKS TO UPDATE STATUS"
            mdlPopupStatus.Show()
            Exit Sub

        End If

        Try
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim command As MySqlCommand = New MySqlCommand

            command.CommandType = CommandType.Text

            If chkInactive.Checked = True Then

                command.CommandText = "UPDATE tblCompany SET InActive= 0 where rcno=" & Convert.ToInt32(txtRcno.Text)
                chkInactive.Checked = False

            ElseIf chkInactive.Checked = False Then

                command.CommandText = "UPDATE tblCompany SET InActive= 1 where rcno=" & Convert.ToInt32(txtRcno.Text)
                chkInactive.Checked = True

            End If

            'command.CommandText = "UPDATE tblCompany SET InActive='" + ddlNewStatus.SelectedValue + "' where rcno=" & Convert.ToInt32(txtRcno.Text)
            command.Connection = conn
            command.ExecuteNonQuery()

            '   UpdateContractActSvcDate(conn)

            conn.Close()
            conn.Dispose()
            command.Dispose()

            'ddlStatus.Text = ddlNewStatus.Text
            '  ddlNewStatus.SelectedIndex = 0

            lblMessage.Text = "ACTION: STATUS UPDATED"

            CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CHST", txtAccountID.Text, "CHST", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")), 0, 0, 0, txtAccountID.Text, txtStatusRemarks.Text.ToUpper, txtRcno.Text)
            'txtPostStatus.Text = ddlNewStatus.SelectedValue

            SqlDataSource1.SelectCommand = txt.Text
            SqlDataSource1.DataBind()
            GridView1.DataBind()

            'InsertNewLog()


            mdlPopupStatus.Hide()
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "CHST - BTNCONFIRMYES_CLICK", ex.Message.ToString, txtAccountID.Text)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)

        End Try

    End Sub

    Protected Sub ddlFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFilter.SelectedIndexChanged
        Try
            UpdateTransactions()

            Session.Add("customerfrom", "Corporate")

            'If grdViewInvoiceDetails.Rows.Count = 0 Then
            '    lblAlertTransactions.Text = "THERE ARE NO TRANSACTIONS IN THIS ACCOUNT"
            '    ddlFilter.Visible = False
            'Else
            '    lblAlertTransactions.Text = ""
            '    ddlFilter.Visible = True

            'End If
            ModalPopupInvoice.Show()

        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "ddlFilter_SelectedIndexChanged", ex.Message.ToString, "")
        End Try
    End Sub

    Dim total As Decimal

    Protected Sub grdViewInvoiceDetails_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdViewInvoiceDetails.RowDataBound
        ''    Dim total As Decimal = Sum(Function(row) row.Field(Of Decimal)("Amount"))
        ''    Dim total As Decimal = WebControls.DataControlField.grdViewInvoiceDetails.Columns("Amount").SummaryItem.SummaryValue
        ' If e.Row.RowType = DataControlRowType.DataRow Then
        '    Dim price As Decimal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"))
        '    total = total + price
        '    '  Session.Add("Total", total.ToString("N2"))

        'End If

        'If e.Row.RowType = DataControlRowType.Footer Then
        '    e.Row.Cells(2).Text = "Total"
        '    e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
        '    e.Row.Cells(3).Text = total 'Session("Total").ToString()
        '    e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
        'End If


    End Sub

    Private Sub DisableNotesControls()

        btnSaveNotesMaster.Enabled = True
        btnSaveNotesMaster.ForeColor = System.Drawing.Color.Black
        btnCancelNotesMaster.Enabled = True
        btnCancelNotesMaster.ForeColor = System.Drawing.Color.Black

        btnAddNotesMaster.Enabled = False
        btnAddNotesMaster.ForeColor = System.Drawing.Color.Gray

        btnEditNotesMaster.Enabled = False
        btnEditNotesMaster.ForeColor = System.Drawing.Color.Gray

        btnDeleteNotesMaster.Enabled = False
        btnDeleteNotesMaster.ForeColor = System.Drawing.Color.Gray

        txtNotes.Enabled = True

    End Sub


    Private Sub EnableNotesControls()
        btnSaveNotesMaster.Enabled = False
        btnSaveNotesMaster.ForeColor = System.Drawing.Color.Gray
        btnCancelNotesMaster.Enabled = False
        btnCancelNotesMaster.ForeColor = System.Drawing.Color.Gray

        btnAddNotesMaster.Enabled = True
        btnAddNotesMaster.ForeColor = System.Drawing.Color.Black
        btnEditNotesMaster.Enabled = False
        btnEditNotesMaster.ForeColor = System.Drawing.Color.Gray
        btnDeleteNotesMaster.Enabled = False
        btnDeleteNotesMaster.ForeColor = System.Drawing.Color.Gray

        txtNotes.Enabled = False

    End Sub

    Public Sub MakeNotesNull()
        txtNotesMode.Text = ""
        txtNotes.Text = ""
        txtNotesRcNo.Text = ""
    End Sub


    Public NotesRcno As String

    Protected Sub gvNotesMaster_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvNotesMaster.PageIndexChanging
        gvNotesMaster.PageIndex = e.NewPageIndex

        SqlDSNotesMaster.SelectCommand = "SELECT * From tblnotes where rcno <>0 and keyfield='" + txtAccountID.Text + "'"


        SqlDSNotesMaster.DataBind()
        gvNotesMaster.DataBind()

    End Sub

    Protected Sub gvNotesMaster_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvNotesMaster.SelectedIndexChanged
        Try
            MakeNotesNull()
            '  txtTechMode.Text = "Edit"
            Dim editindex As Integer = gvNotesMaster.SelectedIndex
            rcno = DirectCast(gvNotesMaster.Rows(editindex).FindControl("Label1"), Label).Text
            txtNotesRcNo.Text = rcno.ToString()
            txtNotes.Text = gvNotesMaster.SelectedRow.Cells(2).Text
            EnableNotesControls()

            btnEditNotesMaster.Enabled = True
            btnEditNotesMaster.ForeColor = System.Drawing.Color.Black
            btnDeleteNotesMaster.Enabled = True
            btnDeleteNotesMaster.ForeColor = System.Drawing.Color.Black


        Catch ex As Exception
            MessageBox.Message.Alert(Page, ex.ToString, "str")
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Protected Sub btnCancelNotesMaster_Click(sender As Object, e As EventArgs) Handles btnCancelNotesMaster.Click
        MakeNotesNull()
        EnableNotesControls()
        txtNotesMode.Text = ""
    End Sub

    Protected Sub btnAddNotesMaster_Click(sender As Object, e As EventArgs) Handles btnAddNotesMaster.Click
        DisableNotesControls()

        MakeNotesNull()
        lblMessage.Text = "ACTION: ADD NOTES"


        txtNotesMode.Text = "Add"
        txtNotes.Focus()

    End Sub

    Protected Sub btnSaveNotesMaster_Click(sender As Object, e As EventArgs) Handles btnSaveNotesMaster.Click
        If String.IsNullOrEmpty(txtNotes.Text) Then
            ' MessageBox.Message.Alert(Page, "Select Staff to proceed!!", "str")
            lblAlert.Text = "ENTER NOTES"
            Return
        End If

        If txtNotesMode.Text = "Add" Then
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                command1.CommandText = "SELECT * FROM tblNOTES where KEYFIELD=@recordno and notes=@notes"
                command1.Parameters.AddWithValue("@recordno", lblNotesKeyField.Text)
                command1.Parameters.AddWithValue("@notes", txtNotes.Text)

                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    '    MessageBox.Message.Alert(Page, "Selected Staff already assigned for this service!!!", "str")
                    lblAlert.Text = "NOTES ALREADY EXISTS"

                Else

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "INSERT INTO tblnotes(KeyModule,SubKeyModule,KeyField,SubKeyField,StaffID,CreatedOn,ContactType,CustCode,CustName,ContactPerson,Notes,Internal,Printable,CreatedBy,LastModifiedBy,LastModifiedOn)VALUES(@KeyModule,@SubKeyModule,@KeyField,@SubKeyField,@StaffID,@CreatedOn,@ContactType,@CustCode,@CustName,@ContactPerson,@Notes,@Internal,@Printable,@CreatedBy,@LastModifiedBy,@LastModifiedOn);"
                    command.CommandText = qry
                    command.Parameters.Clear()
                    If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then
                        command.Parameters.AddWithValue("@KeyModule", "CORPORATE")
                        command.Parameters.AddWithValue("@SubKeyModule", DBNull.Value.ToString)
                        command.Parameters.AddWithValue("@KeyField", lblNotesKeyField.Text.ToUpper)
                        command.Parameters.AddWithValue("@SubKeyField", DBNull.Value.ToString)
                        command.Parameters.AddWithValue("@StaffID", lblNotesStaffID.Text.ToUpper)
                        command.Parameters.AddWithValue("@ContactType", "CORPORATE")
                        command.Parameters.AddWithValue("@CustCode", txtAccountID.Text)
                        command.Parameters.AddWithValue("@CustName", txtServiceName.Text)
                        command.Parameters.AddWithValue("@ContactPerson", txtSvcCP1Contact.Text.ToUpper)
                        command.Parameters.AddWithValue("@Notes", txtNotes.Text.ToUpper)
                        command.Parameters.AddWithValue("@Internal", 0)
                        command.Parameters.AddWithValue("@Printable", 0)
                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))



                    ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then
                        command.Parameters.AddWithValue("@KeyModule", "CORPORATE")
                        command.Parameters.AddWithValue("@SubKeyModule", DBNull.Value.ToString)
                        command.Parameters.AddWithValue("@KeyField", lblNotesKeyField.Text)
                        command.Parameters.AddWithValue("@SubKeyField", DBNull.Value.ToString)
                        command.Parameters.AddWithValue("@StaffID", lblNotesStaffID.Text)
                        command.Parameters.AddWithValue("@ContactType", "CORPORATE")
                        command.Parameters.AddWithValue("@CustCode", txtAccountID.Text)
                        command.Parameters.AddWithValue("@CustName", txtServiceName.Text)
                        command.Parameters.AddWithValue("@ContactPerson", txtSvcCP1Contact.Text)
                        command.Parameters.AddWithValue("@Notes", txtNotes.Text)
                        command.Parameters.AddWithValue("@Internal", 0)
                        command.Parameters.AddWithValue("@Printable", 0)
                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                        command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))


                    End If


                    command.Connection = conn

                    command.ExecuteNonQuery()
                    txtNotesRcNo.Text = command.LastInsertedId

                    '   MessageBox.Message.Alert(Page, "Record added successfully!!!", "str")
                    lblMessage.Text = "ADD: NOTES SUCCESSFULLY ADDED"
                    lblAlert.Text = ""

                End If
                conn.Close()

            Catch ex As Exception
                InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "NOTES ADD SAVE", ex.Message.ToString, txtAccountID.Text)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            End Try
            EnableNotesControls()

        ElseIf txtNotesMode.Text = "Edit" Then
            If txtNotesRcNo.Text = "" Then
                '   MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
                lblAlert.Text = "SELECT RECORD TO EDIT"

                Return

            End If
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command2 As MySqlCommand = New MySqlCommand

                command2.CommandType = CommandType.Text
                command2.CommandText = "SELECT * FROM tblNOTES where KEYFIELD=@recordno and NOTES=@notes and rcno<>" & Convert.ToInt32(txtNotesRcNo.Text)
                command2.Parameters.AddWithValue("@recordno", lblNotesKeyField.Text)
                command2.Parameters.AddWithValue("@notes", txtNotes.Text)

                command2.Connection = conn

                Dim dr1 As MySqlDataReader = command2.ExecuteReader()
                Dim dt1 As New DataTable
                dt1.Load(dr1)

                If dt1.Rows.Count > 0 Then

                    lblAlert.Text = "NOTES ALREADY EXISTS"



                Else

                    Dim command1 As MySqlCommand = New MySqlCommand

                    command1.CommandType = CommandType.Text

                    command1.CommandText = "SELECT * FROM tblnotes where rcno=" & Convert.ToInt32(txtNotesRcNo.Text)
                    command1.Connection = conn

                    Dim dr As MySqlDataReader = command1.ExecuteReader()
                    Dim dt As New DataTable
                    dt.Load(dr)

                    If dt.Rows.Count > 0 Then

                        Dim command As MySqlCommand = New MySqlCommand

                        command.CommandType = CommandType.Text
                        Dim qry As String = "UPDATE tblnotes SET notes=@notes,StaffID = @StaffID,LastModifiedBy = @LastModifiedBy,LastModifiedOn = @LastModifiedOn WHERE  rcno=" & Convert.ToInt32(txtNotesRcNo.Text)

                        command.CommandText = qry
                        command.Parameters.Clear()

                        If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then
                            command.Parameters.AddWithValue("@notes", txtNotes.Text.ToUpper)

                            command.Parameters.AddWithValue("@StaffID", lblNotesStaffID.Text.ToUpper)
                            command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                            command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                        ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then
                            command.Parameters.AddWithValue("@notes", txtNotes.Text)

                            command.Parameters.AddWithValue("@StaffID", lblNotesStaffID.Text)
                            command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                            command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        End If

                        command.Connection = conn

                        command.ExecuteNonQuery()

                        '  MessageBox.Message.Alert(Page, "Record updated successfully!!!", "str")
                        lblMessage.Text = "EDIT: NOTES SUCCESSFULLY UPDATED"
                        lblAlert.Text = ""
                    End If
                End If


                txtNotesMode.Text = ""

                conn.Close()
                conn.Dispose()
                command2.Dispose()
                dt1.Dispose()
                dr1.Close()

            Catch ex As Exception
                InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "NOTES EDIT SAVE", ex.Message.ToString, txtAccountID.Text)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            End Try
            EnableNotesControls()

        End If
        SqlDSNotesMaster.SelectCommand = "select * from tblnotes where keyfield = '" + txtAccountID.Text + "'"
        SqlDSNotesMaster.DataBind()
        gvNotesMaster.DataBind()

        txtNotesMode.Text = ""

    End Sub

    Protected Sub btnEditNotesMaster_Click(sender As Object, e As EventArgs) Handles btnEditNotesMaster.Click
        lblAlert.Text = ""
        lblMessage.Text = ""
        If txtNotesRcNo.Text = "" Then
            ' MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
            lblAlert.Text = "SELECT RECORD TO EDIT"
            Return

        End If
        If ddlStatus.Text = "O" Then
            DisableNotesControls()
            txtNotesMode.Text = "Edit"
            lblMessage.Text = "ACTION: EDIT NOTES"
        Else
            lblMessage.Text = "ONLY OPEN RECORDS CAN BE EDITED"
        End If

    End Sub

    Protected Sub btnDeleteNotesMaster_Click(sender As Object, e As EventArgs) Handles btnDeleteNotesMaster.Click
        lblMessage.Text = ""
        If txtNotesRcNo.Text = "" Then
            ' MessageBox.Message.Alert(Page, "Select a record to delete!!!", "str")
            lblAlert.Text = "SELECT RECORD TO DELETE"
            Return
        End If
        lblMessage.Text = "ACTION: DELETE NOTES"

        Dim confirmValue As String = Request.Form("confirm_value")
        If Right(confirmValue, 3) = "Yes" Then
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                command1.CommandText = "SELECT * FROM tblNOTES where rcno=" & Convert.ToInt32(txtNotesRcNo.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "delete from tblNOTES where rcno=" & Convert.ToInt32(txtNotesRcNo.Text)

                    command.CommandText = qry

                    command.Connection = conn

                    command.ExecuteNonQuery()

                    '  MessageBox.Message.Alert(Page, "Record deleted successfully!!!", "str")
                    lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"

                End If
                conn.Close()
                conn.Dispose()
                command1.Dispose()
                dt.Dispose()
                dr.Close()

            Catch ex As Exception
                InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "NOTES DELETE", ex.Message.ToString, txtAccountID.Text)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            End Try
            EnableNotesControls()

            SqlDSNotesMaster.SelectCommand = "select * from tblnotes where keyfield = '" + txtAccountID.Text + "'"
            SqlDSNotesMaster.DataBind()
            gvNotesMaster.DataBind()
            MakeNotesNull()
            lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"

        End If
    End Sub

    Protected Sub OnRowDataBoundgNotes(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='silver';this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#E4E4E4';")
            'e.Row.Attributes.Add("onmousedown", "this.style.backgroundColor='#738A9C';")
            'e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#738A9C';")

            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvNotesMaster, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Protected Sub OnRowDataBoundTransferFiles(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='silver';this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#E4E4E4';")
            'e.Row.Attributes.Add("onmousedown", "this.style.backgroundColor='#738A9C';")
            'e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#738A9C';")

            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvTransferFiles, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Protected Sub OnSelectedIndexChangedgNotes(sender As Object, e As EventArgs) Handles gvNotesMaster.SelectedIndexChanged
        For Each row As GridViewRow In gvNotesMaster.Rows
            'If row.RowIndex = gvNotesMaster.SelectedIndex Then
            '    row.BackColor = ColorTranslator.FromHtml("#738A9C")
            '    row.ToolTip = String.Empty
            'Else
            '    row.BackColor = ColorTranslator.FromHtml("#E4E4E4")
            '    row.ToolTip = "Click to select this row."
            'End If

            If row.RowIndex = gvNotesMaster.SelectedIndex Then
                row.BackColor = ColorTranslator.FromHtml("#00ccff")
                row.ToolTip = String.Empty
            Else
                If row.RowIndex Mod 2 = 0 Then
                    row.BackColor = ColorTranslator.FromHtml("#EFF3FB")
                    row.ToolTip = "Click to select this row."
                Else
                    row.BackColor = ColorTranslator.FromHtml("#ffffff")
                    row.ToolTip = "Click to select this row."
                End If
            End If
        Next
    End Sub

    Protected Sub OnSelectedIndexChangedTransferFile(sender As Object, e As EventArgs) Handles gvTransferFiles.SelectedIndexChanged
        For Each row As GridViewRow In gvNotesMaster.Rows
            'If row.RowIndex = gvNotesMaster.SelectedIndex Then
            '    row.BackColor = ColorTranslator.FromHtml("#738A9C")
            '    row.ToolTip = String.Empty
            'Else
            '    row.BackColor = ColorTranslator.FromHtml("#E4E4E4")
            '    row.ToolTip = "Click to select this row."
            'End If

            If row.RowIndex = gvTransferFiles.SelectedIndex Then
                row.BackColor = ColorTranslator.FromHtml("#00ccff")
                row.ToolTip = String.Empty
            Else
                If row.RowIndex Mod 2 = 0 Then
                    row.BackColor = ColorTranslator.FromHtml("#EFF3FB")
                    row.ToolTip = "Click to select this row."
                Else
                    row.BackColor = ColorTranslator.FromHtml("#ffffff")
                    row.ToolTip = "Click to select this row."
                End If
            End If
        Next
    End Sub

    Protected Sub OnSelectedIndexChangedgCP(sender As Object, e As EventArgs) Handles gvCP.SelectedIndexChanged
        For Each row As GridViewRow In gvCP.Rows
            'If row.RowIndex = gvNotesMaster.SelectedIndex Then
            '    row.BackColor = ColorTranslator.FromHtml("#738A9C")
            '    row.ToolTip = String.Empty
            'Else
            '    row.BackColor = ColorTranslator.FromHtml("#E4E4E4")
            '    row.ToolTip = "Click to select this row."
            'End If

            If row.RowIndex = gvCP.SelectedIndex Then
                row.BackColor = ColorTranslator.FromHtml("#00ccff")
                row.ToolTip = String.Empty
            Else
                If row.RowIndex Mod 2 = 0 Then
                    row.BackColor = ColorTranslator.FromHtml("#EFF3FB")
                    row.ToolTip = "Click to select this row."
                Else
                    row.BackColor = ColorTranslator.FromHtml("#ffffff")
                    row.ToolTip = "Click to select this row."
                End If
            End If
        Next
    End Sub

    Protected Sub btnConfirmDelete_Click(sender As Object, e As EventArgs) Handles btnConfirmDelete.Click
        Dim deletefilepath1 As String = Server.MapPath("~/Uploads/Customer/DeletedFiles/") + txtFileLink.Text
        Dim deletefilepath As String = Server.MapPath("~/Uploads/Customer/DeletedFiles/") + Path.GetFileNameWithoutExtension(deletefilepath1) + "_" + DateTime.Now.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("ssmmhh") + Path.GetExtension(deletefilepath1)
        File.Move(txtDeleteUploadedFile.Text, deletefilepath)


        'File.Delete(txtDeleteUploadedFile.Text)
        '  Response.Redirect(Request.Url.AbsoluteUri)
        Try
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()
            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.CommandText = "SELECT * FROM tblfileupload where filenamelink='" + txtFileLink.Text + "'"
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then

                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                Dim qry As String = "delete from tblfileupload where filenamelink='" + txtFileLink.Text + "'"

                command.CommandText = qry

                command.Connection = conn

                command.ExecuteNonQuery()
                command.Dispose()

                '  MessageBox.Message.Alert(Page, "Record deleted successfully!!!", "str")
                'CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "FILEUPLOADDELETE", txtFileLink.Text, "DELETE", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtFileLink.Text)

            End If
            conn.Close()
            conn.Dispose()
            command1.Dispose()
            dt.Dispose()
            dr.Close()
            lblMessage.Text = "FILE DELETED"

            SqlDSUpload.SelectCommand = "select * from tblfileupload where fileref = '" + txtAccountID.Text + "'"
            gvUpload.DataSourceID = "SqlDSUpload"
            gvUpload.DataBind()

            'Dim myDir As New DirectoryInfo(Server.MapPath("~/Uploads/Customer/"))
            'Dim filesInDir As FileInfo() = myDir.GetFiles((Convert.ToString(txtAccountID.Text + "_")) + "*.*")
            'Dim files As List(Of ListItem) = New List(Of ListItem)

            'For Each foundFile As FileInfo In filesInDir
            '    Dim fullName As String = foundFile.FullName
            '    files.Add(New ListItem(foundFile.Name))
            'Next
            ''Dim filePaths() As String = Directory.GetFiles(Server.MapPath("~/Uploads/") + txtAccountID.Text + "_*")
            ''For Each filePath As String In filePaths
            ''    files.Add(New ListItem(Path.GetFileName(filePath), filePath))
            ''Next
            'gvUpload.DataSource = files
            'gvUpload.DataBind()
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "FILE DELETE", ex.Message.ToString, txtAccountID.Text + " " + txtFileLink.Text)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Protected Sub ddlView_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlView.SelectedIndexChanged
        GridView1.PageSize = Convert.ToInt16(ddlView.SelectedItem.Text)
        SqlDataSource1.SelectCommand = txt.Text
        SqlDataSource1.DataBind()
        GridView1.DataBind()
    End Sub

    Protected Sub ddlView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlView1.SelectedIndexChanged
        GridView2.PageSize = Convert.ToInt16(ddlView1.SelectedItem.Text)

        SqlDataSource2.SelectCommand = txtDetail.Text
        SqlDataSource2.DataBind()
        GridView2.DataBind()

    End Sub

    Protected Sub btnTransactionsSvc_Click(sender As Object, e As EventArgs) Handles btnTransactionsSvc.Click
        Try
            Session.Add("AccountID", txtAccountID.Text)
            Session.Add("customerfrom", "Corporate")


            Session("contracttype") = "CORPORATESVC"
            Session("companygroup") = ddlCompanyGrp.Text
            Session("accountid") = txtAccountID.Text.Trim
            Session("custname") = txtNameE.Text
            Session("postal") = txtOffPostal.Text
            Session("sevaddress") = txtAddress.Text
            Session("locategrp") = ddlLocateGrp.Text
            Session("salesman") = ddlSalesMan.Text

            Session.Add("locationselectedsvc", txtLocationIDSelectedsVC.Text)

            Session("offaddress1") = txtOffAddress1.Text
            Session("offstreet") = txtOffStreet.Text
            Session("offbuilding") = txtOffBuilding.Text



            If (ddlOffCity.Text.Trim) = "-1" Then
                Session("offcity") = ""
            Else
                Session("offcity") = ddlBillCity.Text
            End If
            'Session("offcity") = ddlOffCity.Text
            Session("offpostal") = txtOffPostal.Text

            Session("billaddress1") = txtBillAddress.Text
            Session("billstreet") = txtBillStreet.Text
            Session("billbuilding") = txtBillBuilding.Text

            If (ddlBillCity.Text.Trim) = "-1" Then
                Session("billcity") = ""
            Else
                Session("billcity") = ddlBillCity.Text
            End If

            'Session("billcity") = ddlBillCity.Text
            Session("billpostal") = txtBillPostal.Text

            Session("industry") = ddlIndustry.Text

            'If String.IsNullOrEmpty(Session("contractfrom")) = False Then
            '    Session("clientid") = txtID.Text

            'End If

            Session("gridsqlCompany") = txt.Text
            Session("rcno") = txtRcno.Text

            Session("gridsqlCompanyDetail") = txtDetail.Text
            Session("rcnoDetail") = txtSvcRcno.Text

            txtAccountIdSelectedSvc.Text = txtAccountID.Text

            ddlFilterSvc.Text = "ALL TRANSACTIONS"
            If grdViewInvoiceDetailsSvc.Rows.Count = 0 Then
                lblAlertTransactionsSvc.Text = "THERE ARE NO TRANSACTIONS IN THIS ACCOUNT & LOCATION"
                ddlFilterSvc.Visible = False
            Else
                lblAlertTransactionsSvc.Text = ""
                ddlFilterSvc.Visible = True

            End If
            '   UpdateTransactions()
            lblTotalSvc.Text = ""
            ModalPopupInvoiceSvc.Show()
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "btnTransactionsSvc_Click", ex.Message.ToString, "")
        End Try
    End Sub

    Protected Sub ddlFilterSvc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFilterSvc.SelectedIndexChanged
        Try
            UpdateTransactionsSvc()

            Session.Add("customerfrom", "Corporate")

            'If grdViewInvoiceDetails.Rows.Count = 0 Then
            '    lblAlertTransactions.Text = "THERE ARE NO TRANSACTIONS IN THIS ACCOUNT"
            '    ddlFilter.Visible = False
            'Else
            '    lblAlertTransactions.Text = ""
            '    ddlFilter.Visible = True

            'End If
            ModalPopupInvoiceSvc.Show()
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "ddlFilterSvc_SelectedIndexChanged", ex.Message.ToString, "")
        End Try
    End Sub



    Private Sub UpdateTransactionsSvc()
        Dim qry As String = ""
        Dim qryTotal As String = ""

        Try

            If ddlFilterSvc.SelectedValue.ToString = "ALL TRANSACTIONS" Then
                qry = "(SELECT tblsales.salesdate as VoucherDate,'INVOICE' as Type, tblsales.invoicenumber as VoucherNumber, tblsalesdetail.AppliedBase as Amount FROM tblsales, tblsalesdetail WHERE tblsales.poststatus='P' and tblsales.doctype='ARIN' AND tblsales.Invoicenumber=tblsalesDetail.Invoicenumber and (tblsales.AccountId = '" + txtAccountIdSelectedSvc.Text + "') AND (tblsalesdetail.LocationId = '" + txtLocationIDSelectedsVC.Text + "')) UNION (select tblrecv.RECEIPTDATE as VoucherDate,'RECEIPTS' as Type,tblrecv.receiptnumber as VoucherNumber,tblrecv.AppliedBase as Amount  from tblrecv, tblrecvdet, tblsalesdetail WHERE tblsalesdetail.Invoicenumber = tblRecvDet.RefType  and tblrecv.Receiptnumber = tblRecvDet.Receiptnumber and  tblrecv.poststatus='P'  AND (tblrecv.AccountId = '" + txtAccountIDSelected.Text + "') AND (tblsalesdetail.LocationId = '" + txtLocationIDSelectedsVC.Text + "')) union  (SELECT tblsales.salesdate as VoucherDate,'CN' as Type, tblsales.invoicenumber as VoucherNumber, tblsalesdetail.AppliedBase as Amount FROM tblsales, tblsalesdetail WHERE tblsales.poststatus='P' and tblsales.doctype='ARCN'  AND tblsales.Invoicenumber = tblsalesDetail.Invoicenumber and  (tblsales.AccountId = '" + txtAccountIdSelectedSvc.Text + "') AND (tblsalesdetail.LocationId = '" + txtLocationIDSelectedsVC.Text + "')) UNION (SELECT tblsales.salesdate as VoucherDate,'DN' as Type, tblsales.invoicenumber as VoucherNumber, tblsalesdetail.AppliedBase as Amount FROM tblsales, tblsalesdetail WHERE tblsales.poststatus='P' and tblsales.doctype='ARDN' AND tblsales.Invoicenumber = tblsalesDetail.Invoicenumber and  (tblsales.AccountId = '" + txtAccountIdSelectedSvc.Text + "') AND (tblsalesdetail.LocationId = '" + txtLocationIDSelectedsVC.Text + "')) ORDER BY VoucherDate desc "

                qryTotal = "SELECT ifnull(Sum(AppliedBase),0) as totalamountinvoice FROM tblsales WHERE poststatus='P' and doctype='ARIN' AND AccountId = '" + txtAccountIdSelectedSvc.Text + "' AND tblsalesdetail.LocationId = '" + txtLocationIDSelectedsVC.Text + "' union select ifnull(Sum(BaseAmount),0) as totalamountreceipt from tblrecv WHERE poststatus='P' AND (AccountId = '" + txtAccountID.Text + "') AND (tblsalesdetail.LocationId = '" + txtLocationIDSelectedsVC.Text + "') union SELECT ifnull(Sum(AppliedBase),0) as totalamountCN FROM tblsales WHERE poststatus='P' and doctype='ARCN' AND (AccountId = '" + txtAccountID.Text + "') AND (tblsalesdetail.LocationId = '" + txtLocationIDSelectedsVC.Text + "') union SELECT ifnull(Sum(AppliedBase),0) as totalamountDN FROM tblsales WHERE poststatus='P' and doctype='ARDN' AND (AccountId = '" + txtAccountID.Text + "') AND (tblsalesdetail.LocationId = '" + txtLocationIDSelectedsVC.Text + "')"
            ElseIf ddlFilterSvc.SelectedValue.ToString = "SALES INVOICE" Then
                qry = "SELECT tblsales.salesdate as VoucherDate,'INVOICE' as Type, tblsales.invoicenumber as VoucherNumber, tblsalesdetail.AppliedBase as Amount FROM tblsales, tblsalesdetail WHERE tblsales.poststatus='P' and tblsales.doctype='ARIN' AND tblsales.Invoicenumber=tblsalesDetail.Invoicenumber and (tblsales.AccountId = '" + txtAccountIdSelectedSvc.Text + "') AND (tblsalesdetail.LocationId = '" + txtLocationIDSelectedsVC.Text + "') ORDER BY VoucherDate desc"

                'qry = "SELECT salesdate as VoucherDate,'INVOICE' as Type,invoicenumber as VoucherNumber,AppliedBase as Amount FROM tblsales WHERE poststatus='P' and doctype='ARIN' AND (AccountId = '" + txtAccountID.Text + "') ORDER BY VoucherDate"
                qryTotal = "SELECT ifnull(Sum(tblsalesdetail.AppliedBase),0) as totalamount From tblsales, tblsalesdetail WHERE tblsales.poststatus='P' and tblsales.doctype='ARIN' AND tblsales.Invoicenumber=tblsalesDetail.Invoicenumber and (tblsales.AccountId = '" + txtAccountIdSelectedSvc.Text + "') AND (tblsalesdetail.LocationId = '" + txtLocationIDSelectedsVC.Text + "') ORDER BY VoucherDate desc"


            ElseIf ddlFilterSvc.SelectedValue.ToString = "SALES INVOICE (OUTSTANDING)" Then
                qry = "(SELECT tblsales.salesdate as VoucherDate,'INVOICE' as Type, tblsales.invoicenumber as VoucherNumber, tblsalesdetail.AppliedBase as Amount FROM tblsales, tblsalesdetail WHERE tblsales.poststatus='P' and tblsales.doctype='ARIN' AND tblsales.Invoicenumber=tblsalesDetail.Invoicenumber and (tblsales.AccountId = '" + txtAccountIdSelectedSvc.Text + "') AND (tblsalesdetail.LocationId = '" + txtLocationIDSelectedsVC.Text + "')) UNION (select tblrecv.RECEIPTDATE as VoucherDate,'RECEIPTS' as Type,tblrecv.receiptnumber as VoucherNumber,tblrecvdet.AppliedBase as Amount  from tblrecv, tblrecvdet, tblsalesdetail WHERE tblsalesdetail.Invoicenumber = tblRecvDet.RefType  and tblrecv.Receiptnumber = tblRecvDet.Receiptnumber and  tblrecv.poststatus='P'  AND (tblrecv.AccountId = '" + txtAccountIDSelected.Text + "') AND (tblsalesdetail.LocationId = '" + txtLocationIDSelectedsVC.Text + "')) union  (SELECT tblsales.salesdate as VoucherDate,'CN' as Type, tblsales.invoicenumber as VoucherNumber, tblsalesdetail.AppliedBase as Amount FROM tblsales, tblsalesdetail WHERE tblsales.poststatus='P' and tblsales.doctype='ARCN'  AND tblsales.Invoicenumber = tblsalesDetail.Invoicenumber and  (tblsales.AccountId = '" + txtAccountIdSelectedSvc.Text + "') AND (tblsalesdetail.LocationId = '" + txtLocationIDSelectedsVC.Text + "')) UNION (SELECT tblsales.salesdate as VoucherDate,'DN' as Type, tblsales.invoicenumber as VoucherNumber, tblsalesdetail.AppliedBase as Amount FROM tblsales, tblsalesdetail WHERE tblsales.poststatus='P' and tblsales.doctype='ARDN' AND tblsales.Invoicenumber = tblsalesDetail.Invoicenumber and  (tblsales.AccountId = '" + txtAccountIdSelectedSvc.Text + "') AND (tblsalesdetail.LocationId = '" + txtLocationIDSelectedsVC.Text + "')) ORDER BY VoucherDate desc"

                'qry = "SELECT salesdate as VoucherDate,'INVOICE' as Type,invoicenumber as VoucherNumber,BalanceBase as Amount FROM tblsales WHERE poststatus='P' and doctype='ARIN' AND (AccountId = '" + txtAccountID.Text + "') and balancebase<>0 ORDER BY VoucherDate"
                qryTotal = "SELECT ifnull(Sum(BalanceBase),0) as totalamount FROM tblsales WHERE poststatus='P' and doctype='ARIN' AND (AccountId = '" + txtAccountID.Text + "') and balancebase<>0"
            ElseIf ddlFilterSvc.SelectedValue.ToString = "RECEIPT" Then
                qry = "select tblrecv.RECEIPTDATE as VoucherDate,'RECEIPTS' as Type,tblrecv.receiptnumber as VoucherNumber,tblrecvDet.AppliedBase as Amount  from tblrecv, tblrecvdet, tblsalesdetail WHERE tblsalesdetail.Invoicenumber = tblRecvDet.RefType  and tblrecv.Receiptnumber = tblRecvDet.Receiptnumber and  tblrecv.poststatus='P'  AND (tblrecv.AccountId = '" + txtAccountIDSelected.Text + "') AND (tblsalesdetail.LocationId = '" + txtLocationIDSelectedsVC.Text + "')  ORDER BY VoucherDate desc "

                'qry = "select RECEIPTDATE as VoucherDate,'RECEIPTS' as Type,receiptnumber as VoucherNumber,BaseAmount as Amount from tblrecv WHERE poststatus='P' AND (AccountId = '" + txtAccountID.Text + "') ORDER BY VoucherDate"
                qryTotal = "select ifnull(Sum(tblrecvDet.AppliedBase),0) as totalamount from tblrecv, tblrecvdet, tblsalesdetail WHERE tblsalesdetail.Invoicenumber = tblRecvDet.RefType  and tblrecv.Receiptnumber = tblRecvDet.Receiptnumber and  tblrecv.poststatus='P'  AND (tblrecv.AccountId = '" + txtAccountIDSelected.Text + "') AND (tblsalesdetail.LocationId = '" + txtLocationIDSelectedsVC.Text + "')"
            ElseIf ddlFilterSvc.SelectedValue.ToString = "CREDIT NOTE" Then
                qry = "SELECT tblsales.salesdate as VoucherDate,'CN' as Type, tblsales.invoicenumber as VoucherNumber, tblsalesdetail.AppliedBase as Amount FROM tblsales, tblsalesdetail WHERE tblsales.poststatus='P' and tblsales.doctype='ARCN'  AND tblsales.Invoicenumber = tblsalesDetail.Invoicenumber and  (tblsales.AccountId = '" + txtAccountIdSelectedSvc.Text + "') AND (tblsalesdetail.LocationId = '" + txtLocationIDSelectedsVC.Text + "') ORDER BY VoucherDate desc"

                'qry = "SELECT salesdate as VoucherDate,'CN' as Type,invoicenumber as VoucherNumber,AppliedBase as Amount FROM tblsales WHERE poststatus='P' and doctype='ARCN' AND (AccountId = '" + txtAccountID.Text + "') ORDER BY VoucherDate"
                qryTotal = "SELECT ifnull(Sum(tblsalesdetail.AppliedBase),0) as totalamount From tblsales, tblsalesdetail WHERE tblsales.poststatus='P' and tblsales.doctype='ARCN' AND tblsales.Invoicenumber=tblsalesDetail.Invoicenumber and (tblsales.AccountId = '" + txtAccountIdSelectedSvc.Text + "') AND (tblsalesdetail.LocationId = '" + txtLocationIDSelectedsVC.Text + "') order by voucherdate desc"
            ElseIf ddlFilterSvc.SelectedValue.ToString = "DEBIT NOTE" Then
                qry = " SELECT tblsales.salesdate as VoucherDate,'DN' as Type, tblsales.invoicenumber as VoucherNumber, tblsalesdetail.AppliedBase as Amount FROM tblsales, tblsalesdetail WHERE tblsales.poststatus='P' and tblsales.doctype='ARDN' AND tblsales.Invoicenumber = tblsalesDetail.Invoicenumber and  (tblsales.AccountId = '" + txtAccountIdSelectedSvc.Text + "') AND (tblsalesdetail.LocationId = '" + txtLocationIDSelectedsVC.Text + "') ORDER BY VoucherDate "


                'qry = "SELECT salesdate as VoucherDate,'DN' as Type,invoicenumber as VoucherNumber,AppliedBase as Amount FROM tblsales WHERE poststatus='P' and doctype='ARDN' AND (AccountId = '" + txtAccountID.Text + "') ORDER BY VoucherDate"
                qryTotal = "SELECT ifnull(Sum(tblsalesdetail.AppliedBase),0) as totalamount From tblsales, tblsalesdetail WHERE tblsales.poststatus='P' and tblsales.doctype='ARDN' AND tblsales.Invoicenumber=tblsalesDetail.Invoicenumber and (tblsales.AccountId = '" + txtAccountIdSelectedSvc.Text + "') AND (tblsalesdetail.LocationId = '" + txtLocationIDSelectedsVC.Text + "') order by voucherdate desc"
            ElseIf ddlFilterSvc.SelectedValue.ToString = "ADJUSTMENT" Then
                '  qry = "SELECT salesdate as VoucherDate,'INVOICE' as Type,invoicenumber as VoucherNumber,AppliedBase as Amount FROM tblsales WHERE poststatus='P' and doctype='ARIN' AND (AccountId = '" + txtAccountID.Text + "')"

            End If

            SqlDSInvoiceDetailsSvc.SelectCommand = qry
            SqlDSInvoiceDetailsSvc.DataBind()
            grdViewInvoiceDetailsSvc.DataBind()


            If ddlFilterSvc.SelectedValue.ToString = "ALL TRANSACTIONS" Then
                'If dt.Columns.Contains("totalamountinvoice") Then
                '    tot = tot + dt.Rows(0)("totalamountinvoice")
                'ElseIf dt.Columns.Contains("totalamountreceipt") Then
                '    tot = tot + dt.Rows(0)("totalamountreceipt")
                'ElseIf dt.Columns.Contains("totalamountCN") Then
                '    tot = tot + dt.Rows(0)("totalamountCN")
                'ElseIf dt.Columns.Contains("totalamountDN") Then
                '    tot = tot + dt.Rows(0)("totalamountDN")
                'End If

                lblTotal.Text = ""

            Else

                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                command1.CommandText = qryTotal
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New System.Data.DataTable
                dt.Load(dr)
                Dim tot As Decimal = 0

                If dt.Rows.Count > 0 Then
                    tot = dt.Rows(0)("totalamount")
                    lblTotalSvc.Text = "Total    : " + tot.ToString("N2")
                End If
                conn.Close()
                conn.Dispose()
                command1.Dispose()
                dt.Dispose()
                dr.Close()

            End If

            'If ddlFilter.SelectedValue.ToString = "ALL TRANSACTIONS" Then
            '    qry = "SELECT salesdate as VoucherDate,'INVOICE' as Type,invoicenumber as VoucherNumber,AppliedBase as Amount FROM tblsales WHERE poststatus='P' and doctype='ARIN' AND (AccountId = '" + txtAccountID.Text + "') union select RECEIPTDATE as VoucherDate,'RECEIPTS' as Type,receiptnumber as VoucherNumber,BaseAmount as Amount from tblrecv WHERE poststatus='P' AND (AccountId = '" + txtAccountID.Text + "') union SELECT salesdate as VoucherDate,'CN' as Type,invoicenumber as VoucherNumber,AppliedBase as Amount FROM tblsales WHERE poststatus='P' and doctype='ARCN' AND (AccountId = '" + txtAccountID.Text + "') union SELECT salesdate as VoucherDate,'DN' as Type,invoicenumber as VoucherNumber,AppliedBase as Amount FROM tblsales WHERE poststatus='P' and doctype='ARDN' AND (AccountId = '" + txtAccountID.Text + "') ORDER BY VoucherDate"
            '    qryTotal = "SELECT ifnull(Sum(AppliedBase),0) as totalamountinvoice FROM tblsales WHERE poststatus='P' and doctype='ARIN' AND (AccountId = '" + txtAccountID.Text + "') union select ifnull(Sum(BaseAmount),0) as totalamountreceipt from tblrecv WHERE poststatus='P' AND (AccountId = '" + txtAccountID.Text + "') union SELECT ifnull(Sum(AppliedBase),0) as totalamountCN FROM tblsales WHERE poststatus='P' and doctype='ARCN' AND (AccountId = '" + txtAccountID.Text + "') union SELECT ifnull(Sum(AppliedBase),0) as totalamountDN FROM tblsales WHERE poststatus='P' and doctype='ARDN' AND (AccountId = '" + txtAccountID.Text + "')"
            'ElseIf ddlFilter.SelectedValue.ToString = "SALES INVOICE" Then
            '    qry = "SELECT salesdate as VoucherDate,'INVOICE' as Type,invoicenumber as VoucherNumber,AppliedBase as Amount FROM tblsales WHERE poststatus='P' and doctype='ARIN' AND (AccountId = '" + txtAccountID.Text + "') ORDER BY VoucherDate"
            '    qryTotal = "SELECT ifnull(Sum(AppliedBase),0) as totalamount FROM tblsales WHERE poststatus='P' and doctype='ARIN' AND (AccountId = '" + txtAccountID.Text + "')"
            'ElseIf ddlFilter.SelectedValue.ToString = "SALES INVOICE (OUTSTANDING)" Then
            '    qry = "SELECT salesdate as VoucherDate,'INVOICE' as Type,invoicenumber as VoucherNumber,BalanceBase as Amount FROM tblsales WHERE poststatus='P' and doctype='ARIN' AND (AccountId = '" + txtAccountID.Text + "') and balancebase<>0 ORDER BY VoucherDate"
            '    qryTotal = "SELECT ifnull(Sum(BalanceBase),0) as totalamount FROM tblsales WHERE poststatus='P' and doctype='ARIN' AND (AccountId = '" + txtAccountID.Text + "') and balancebase<>0"
            'ElseIf ddlFilter.SelectedValue.ToString = "RECEIPT" Then
            '    qry = "select RECEIPTDATE as VoucherDate,'RECEIPTS' as Type,receiptnumber as VoucherNumber,BaseAmount as Amount from tblrecv WHERE poststatus='P' AND (AccountId = '" + txtAccountID.Text + "') ORDER BY VoucherDate"
            '    qryTotal = "select ifnull(Sum(BaseAmount),0) as totalamount from tblrecv WHERE poststatus='P' AND (AccountId = '" + txtAccountID.Text + "')"
            'ElseIf ddlFilter.SelectedValue.ToString = "CREDIT NOTE" Then
            '    qry = "SELECT salesdate as VoucherDate,'CN' as Type,invoicenumber as VoucherNumber,AppliedBase as Amount FROM tblsales WHERE poststatus='P' and doctype='ARCN' AND (AccountId = '" + txtAccountID.Text + "') ORDER BY VoucherDate"
            '    qryTotal = "SELECT ifnull(Sum(AppliedBase),0) as totalamount FROM tblsales WHERE poststatus='P' and doctype='ARCN' AND (AccountId = '" + txtAccountID.Text + "')"
            'ElseIf ddlFilter.SelectedValue.ToString = "DEBIT NOTE" Then
            '    qry = "SELECT salesdate as VoucherDate,'DN' as Type,invoicenumber as VoucherNumber,AppliedBase as Amount FROM tblsales WHERE poststatus='P' and doctype='ARDN' AND (AccountId = '" + txtAccountID.Text + "') ORDER BY VoucherDate"
            '    qryTotal = "SELECT ifnull(Sum(AppliedBase),0) as totalamount FROM tblsales WHERE poststatus='P' and doctype='ARDN' AND (AccountId = '" + txtAccountID.Text + "')"
            'ElseIf ddlFilter.SelectedValue.ToString = "ADJUSTMENT" Then
            '    '  qry = "SELECT salesdate as VoucherDate,'INVOICE' as Type,invoicenumber as VoucherNumber,AppliedBase as Amount FROM tblsales WHERE poststatus='P' and doctype='ARIN' AND (AccountId = '" + txtAccountID.Text + "')"

            'End If

            'SqlDSInvoiceDetails.SelectCommand = qry
            'SqlDSInvoiceDetails.DataBind()
            'grdViewInvoiceDetails.DataBind()


            'If ddlFilter.SelectedValue.ToString = "ALL TRANSACTIONS" Then
            '    'If dt.Columns.Contains("totalamountinvoice") Then
            '    '    tot = tot + dt.Rows(0)("totalamountinvoice")
            '    'ElseIf dt.Columns.Contains("totalamountreceipt") Then
            '    '    tot = tot + dt.Rows(0)("totalamountreceipt")
            '    'ElseIf dt.Columns.Contains("totalamountCN") Then
            '    '    tot = tot + dt.Rows(0)("totalamountCN")
            '    'ElseIf dt.Columns.Contains("totalamountDN") Then
            '    '    tot = tot + dt.Rows(0)("totalamountDN")
            '    'End If

            '    lblTotal.Text = ""

            'Else

            '    Dim conn As MySqlConnection = New MySqlConnection()

            '    conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            '    conn.Open()

            '    Dim command1 As MySqlCommand = New MySqlCommand

            '    command1.CommandType = CommandType.Text

            '    command1.CommandText = qryTotal
            '    command1.Connection = conn

            '    Dim dr As MySqlDataReader = command1.ExecuteReader()
            '    Dim dt As New System.Data.DataTable
            '    dt.Load(dr)
            '    Dim tot As Decimal = 0

            '    If dt.Rows.Count > 0 Then
            '        tot = dt.Rows(0)("totalamount")
            '        lblTotal.Text = "Total    : " + tot.ToString("N2")
            '    End If
            '    conn.Close()
            'End If

        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "updateTransactionsSvc", ex.Message.ToString, qryTotal)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Protected Sub GridView1_Sorting(sender As Object, e As GridViewSortEventArgs) Handles GridView1.Sorting
        SqlDataSource1.SelectCommand = txt.Text
        SqlDataSource1.DataBind()
        GridView1.DataBind()

    End Sub

    'Protected Sub btnEditFileDescSave_Click(sender As Object, e As EventArgs) Handles btnEditFileDescSave.Click

    '    Try

    '        Dim conn As MySqlConnection = New MySqlConnection()

    '        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    '        conn.Open()

    '        Dim command As MySqlCommand = New MySqlCommand

    '        command.CommandType = CommandType.Text

    '        command.CommandText = "UPDATE tblfileupload SET FileDescription=@FileDescription where rcno=" & Convert.ToInt32(txtfilercno.Text)

    '        command.Parameters.Clear()

    '        If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

    '            command.Parameters.AddWithValue("@FileDescription", txtEditFileDescription.Text.ToUpper)

    '        ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

    '            command.Parameters.AddWithValue("@FileDescription", txtEditFileDescription.Text)

    '        End If


    '        command.Connection = conn

    '        command.ExecuteNonQuery()



    '        conn.Close()

    '        SqlDSUpload.SelectCommand = "select * from tblfileupload where fileref = '" + txtAccountID.Text + "'"
    '        gvUpload.DataSourceID = "SqlDSUpload"
    '        gvUpload.DataBind()

    '        lblMessage.Text = "FILE DESCRIPTION UPDATED"

    '    Catch ex As Exception
    '        MessageBox.Message.Alert(Page, ex.ToString, "str")
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
    '    End Try
    'End Sub

    Protected Sub grdViewInvoiceDetails_Sorting(sender As Object, e As GridViewSortEventArgs) Handles grdViewInvoiceDetails.Sorting

        UpdateTransactions()

        ModalPopupInvoice.Show()

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
            insCmds.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))

            conn.Open()
            insCmds.Connection = conn
            insCmds.ExecuteNonQuery()
            conn.Close()
            conn.Dispose()
            insCmds.Dispose()

            '  lblAlert.Text = errorMsg
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY.ASPX" + txtCreatedBy.Text, "InsertIntoTblWebEventLog", ex.Message.ToString, "ERROR")
        End Try
    End Sub

    Protected Sub btnSvcCopy_Click(sender As Object, e As EventArgs) Handles btnSvcCopy.Click
        txtSvcRcno.Text = ""
        txtLocationID.Text = ""
        txtAddress.Text = ""
        lblMessage.Text = "ACTION: COPY SERVICE LOCATION"

        btnADD.Enabled = False
        btnADD.ForeColor = System.Drawing.Color.Gray
        btnCopyAdd.Enabled = False
        btnCopyAdd.ForeColor = System.Drawing.Color.Gray
        btnDelete.Enabled = False
        btnDelete.ForeColor = System.Drawing.Color.Gray

        btnFilter.Enabled = False
        btnFilter.ForeColor = System.Drawing.Color.Gray
        btnContract.Enabled = False
        btnContract.ForeColor = System.Drawing.Color.Gray
        btnTransactions.Enabled = False
        btnTransactions.ForeColor = System.Drawing.Color.Gray
        btnChangeStatus.Enabled = False
        btnChangeStatus.ForeColor = System.Drawing.Color.Gray


        btnQuit.Enabled = False
        btnQuit.ForeColor = System.Drawing.Color.Gray
        btnSvcAdd.Enabled = False
        btnSvcAdd.ForeColor = System.Drawing.Color.Gray

        btnSvcContract.Enabled = False
        btnSvcService.Enabled = False

        btnSvcContract.ForeColor = System.Drawing.Color.Gray
        btnSvcService.ForeColor = System.Drawing.Color.Gray

        btnTransfersSvc.Enabled = False
        btnTransfersSvc.ForeColor = System.Drawing.Color.Gray

        btnSpecificLocation.Enabled = False
        btnSpecificLocation.ForeColor = System.Drawing.Color.Gray

        'ddlInchargeSvc.Text = ddlIncharge.Text
        'ddlTermsSvc.Text = ddlTerms.Text
        'ddlIndustrysvc.Text = ddlIndustry.Text
        'ddlSalesManSvc.Text = ddlSalesMan.Text

        'FindMarketSegmentID()

        DisableSvcControls()
        txtSvcMode.Text = "NEW"

    End Sub


    Public Sub FindLocation()
        Try
            Dim IsLock As String
            IsLock = ""

            Dim connLocation As MySqlConnection = New MySqlConnection()

            connLocation.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            connLocation.Open()

            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text
            command1.CommandText = "SELECT LocationID, Location FROM tblstaff where StaffId='" & txtCreatedBy.Text.ToUpper & "'"
            command1.Connection = connLocation

            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)


            If dt.Rows.Count > 0 Then
                ddlLocation.Text = dt.Rows(0)("LocationID").ToString
            End If

            connLocation.Close()
            connLocation.Dispose()
            command1.Dispose()
            dt.Dispose()
        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "FindLocation", ex.Message.ToString, "")
        End Try
    End Sub


    Private Sub ContactModuleSetup()
        Try
            '''''''''''''''''Contact Module setup

            Dim conn As MySqlConnection = New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim CommandContactsSetup As MySqlCommand = New MySqlCommand
            CommandContactsSetup.CommandType = CommandType.Text
            CommandContactsSetup.CommandText = "SELECT * FROM tblContactSetup"
            CommandContactsSetup.Connection = conn

            Dim drContactsSetup As MySqlDataReader = CommandContactsSetup.ExecuteReader()
            Dim dtContactsSetup As New System.Data.DataTable
            dtContactsSetup.Load(drContactsSetup)

            If dtContactsSetup.Rows.Count > 0 Then

                If dtContactsSetup.Rows(0)("PostalValidate").ToString() = True Then
                    txtPostalValidate.Text = "TRUE"
                    Label70.Visible = True
                    Label71.Visible = True
                    Label72.Visible = True
                Else
                    txtPostalValidate.Text = "FALSE"
                    Label70.Visible = False
                    Label71.Visible = False
                    Label72.Visible = False
                End If
            End If
            CommandContactsSetup.Dispose()
            dtContactsSetup.Clear()
            dtContactsSetup.Dispose()
            drContactsSetup.Close()

            ''''''''''''''''''''''''''''''''''''''''
            conn.Close()
            conn.Dispose()
        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "ContactModuleSetup", ex.Message.ToString, "")
        End Try
    End Sub

    Public Sub IsDisplayRecordsLocationwise()
        Try
            Dim IsLock As String
            IsLock = ""

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim commandServiceRecordMasterSetup As MySqlCommand = New MySqlCommand
            commandServiceRecordMasterSetup.CommandType = CommandType.Text
            commandServiceRecordMasterSetup.CommandText = "SELECT showSConScreenLoad, ServiceContractMaxRec,DisplayRecordsLocationWise,DisplayTimeInTimeOutInServiceReport FROM tblservicerecordmastersetup"
            commandServiceRecordMasterSetup.Connection = conn

            Dim drServiceRecordMasterSetup As MySqlDataReader = commandServiceRecordMasterSetup.ExecuteReader()
            Dim dtServiceRecordMasterSetup As New DataTable
            dtServiceRecordMasterSetup.Load(drServiceRecordMasterSetup)

            'txtLimit.Text = dtServiceRecordMasterSetup.Rows(0)("ServiceContractMaxRec")
            'txtDisplayRecordsLocationwise.Text = dtServiceRecordMasterSetup.Rows(0)("DisplayRecordsLocationWise").ToString


            If dtServiceRecordMasterSetup.Rows.Count > 0 Then
                txtDisplayRecordsLocationwise.Text = dtServiceRecordMasterSetup.Rows(0)("DisplayRecordsLocationWise").ToString
                txtDisplayTimeInTimeOutServiceRecord.Text = dtServiceRecordMasterSetup.Rows(0)("DisplayTimeInTimeOutInServiceReport").ToString

            End If

            If txtDisplayRecordsLocationwise.Text = "Y" Then
                lblBranch1.Visible = True
                Label64.Visible = True
                ddlBranch.Visible = True
                BranchSearch.Visible = True

                Label67.Visible = True
                Label66.Visible = True
                ddlLocation.Visible = True
            Else
                lblBranch1.Visible = False
                Label64.Visible = False
                ddlBranch.Visible = False
                BranchSearch.Visible = False

                Label67.Visible = False
                Label66.Visible = False
                ddlLocation.Visible = False
            End If
            conn.Close()
            conn.Dispose()
            commandServiceRecordMasterSetup.Dispose()
            dtServiceRecordMasterSetup.Dispose()
        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "IsDisplayRecordsLocationwise", ex.Message.ToString, "")
        End Try
    End Sub

    Protected Sub btnTransfersSvc_Click(sender As Object, e As EventArgs) Handles btnTransfersSvc.Click
        Try

            '''''''''''''''''''''''''''
            Session("relocatefrom") = "relocateC"
            Session("gridsql") = txt.Text
            Session("rcno") = txtRcno.Text
            Session("gridsqlCompanyDetail") = txtDetail.Text
            Session("rcnoDetail") = txtSvcRcno.Text

            'Session("inactive") = chkInactive.Checked

            '''''''''''''''''''''''''''''
            'End If

            ''''''''''''''
            Session("LocationIDtoRelocate") = txtLocationID.Text
            Session("ServiceNametoRelocate") = txtServiceName.Text
            Session("ContactTypetoRelocate") = "CORPORATE"

            Response.Redirect("AccountIDRelocation.aspx")
        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "btnTransfersSvc_Click", ex.Message.ToString, "")
        End Try
    End Sub

    Protected Sub btnSaveRelocate_Click(sender As Object, e As EventArgs) Handles btnSaveRelocate.Click
        Try
            Dim command As MySqlCommand = New MySqlCommand
            command.CommandType = CommandType.Text
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            If conn.State = ConnectionState.Open Then
                conn.Close()
                conn.Dispose()
            End If
            conn.Open()

            generateLocationIdRelocate()


            Dim qry As String = ""

            qry = "Update tblCompanyLocation set AccountId = @AccountId, LocationId= @LocationIdNew  "
            qry = qry + " where LocationID =@LocationID; "

            command.CommandText = qry
            command.Parameters.Clear()
            command.Parameters.AddWithValue("@LocationId", txtLocationIDRelocate.Text)
            command.Parameters.AddWithValue("@LocationIdNew", txtLocationIDRelocateNew.Text)
            command.Parameters.AddWithValue("@AccountId", txtAccountIDRelocate.Text)

            command.Connection = conn
            command.ExecuteNonQuery()

            '''''''''''''''''''''''''''''''''''''''''''''''

            qry = "Update tblContractdet set AccountId = @AccountId, LocationId= @LocationIdNew  "
            qry = qry + " where LocationID =@LocationID; "

            command.CommandText = qry
            command.Parameters.Clear()
            command.Parameters.AddWithValue("@LocationId", txtLocationIDRelocate.Text)
            command.Parameters.AddWithValue("@LocationIdNew", txtLocationIDRelocateNew.Text)
            command.Parameters.AddWithValue("@AccountId", txtAccountIDRelocate.Text)

            command.Connection = conn
            command.ExecuteNonQuery()


            '''''''''''''''''''''''''''''''''''''''''''''''

            qry = "Update tblServiceRecord set AccountId = @AccountId, LocationId= @LocationIdNew  "
            qry = qry + " where LocationID =@LocationID; "

            command.CommandText = qry
            command.Parameters.Clear()
            command.Parameters.AddWithValue("@LocationId", txtLocationIDRelocate.Text)
            command.Parameters.AddWithValue("@LocationIdNew", txtLocationIDRelocateNew.Text)
            command.Parameters.AddWithValue("@AccountId", txtAccountIDRelocate.Text)

            command.Connection = conn
            command.ExecuteNonQuery()

            '''''''''''''''''''''''''''''''''''''''''''''''

            qry = "Update tblSalesDetail set AccountId = @AccountId, LocationId= @LocationIdNew  "
            qry = qry + " where LocationID =@LocationID; "

            command.CommandText = qry
            command.Parameters.Clear()
            command.Parameters.AddWithValue("@LocationId", txtLocationIDRelocate.Text)
            command.Parameters.AddWithValue("@LocationIdNew", txtLocationIDRelocateNew.Text)
            'command.Parameters.AddWithValue("@AccountId", txtAccountIDRelocate.Text)

            command.Connection = conn
            command.ExecuteNonQuery()

            '''''''''''''''''''''''''''''''''''''''''''''''

            qry = "Update tblRecvDet set AccountId = @AccountId, LocationId= @LocationIdNew  "
            qry = qry + " where LocationID =@LocationID; "

            command.CommandText = qry
            command.Parameters.Clear()
            command.Parameters.AddWithValue("@LocationId", txtLocationIDRelocate.Text)
            command.Parameters.AddWithValue("@LocationIdNew", txtLocationIDRelocateNew.Text)
            'command.Parameters.AddWithValue("@AccountId", txtAccountIDRelocate.Text)

            command.Connection = conn
            command.ExecuteNonQuery()

            '''''''''''''''''''''''''''''''''''''''''''''''

            qry = "Update tblJrnvdet set AccountId = @AccountId, LocationId= @LocationId  "
            qry = qry + " where LocationID =@LocationID; "

            command.CommandText = qry
            command.Parameters.Clear()

            command.Parameters.AddWithValue("@LocationId", txtLocationIDRelocate.Text)
            command.Parameters.AddWithValue("@LocationIdNew", txtLocationIDRelocateNew.Text)
            command.Parameters.AddWithValue("@AccountId", txtAccountIDRelocate.Text)

            command.Connection = conn
            command.ExecuteNonQuery()

            ''''''''''''''''''''''''''''''''''''''
            conn.Close()
            conn.Dispose()
            command.Dispose()
        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "btnSaveRelocate_Click", ex.Message.ToString, "")
        End Try
    End Sub

    Private Sub generateLocationIdRelocate()
        Try
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            If conn.State = ConnectionState.Open Then
                conn.Close()
                conn.Dispose()
            End If
            conn.Open()

            Dim command1 As MySqlCommand = New MySqlCommand
            command1.CommandText = "SELECT locationid,locationprefix,locationno FROM tblcompanylocation where accountid=" & txtAccountIDRelocate.Text.Trim & " order by locationno desc;"

            command1.Connection = conn
            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New System.Data.DataTable
            dt.Load(dr)
            If dt.Rows.Count > 0 Then
                Dim lastnum As Int64 = Convert.ToInt64(dt.Rows(0)("locationno"))
                lastnum = lastnum + 1

                'txtLocationID.Text = txtAccountID.Text + "-" + dt.Rows(0)("locationprefix").ToString + lastnum.ToString("D4")
                txtLocationIDRelocateNew.Text = txtAccountIDRelocate.Text + "-" + lastnum.ToString("D4")
                txtLocatonNo.Text = lastnum
                'txtLocationPrefix.Text = "L"
                txtLocationPrefix.Text = ""
            Else
                'txtLocationID.Text = txtAccountID.Text + "-L001"
                txtLocationIDRelocateNew.Text = txtAccountIDRelocate.Text + "-0001"
                'txtLocationPrefix.Text = "L"
                txtLocationPrefix.Text = ""
                txtLocatonNo.Text = "1"
            End If

            conn.Close()
            conn.Dispose()

            command1.Dispose()
        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "generateLocationIdRelocate", ex.Message.ToString, "")
        End Try
    End Sub

    Protected Sub txtAccountIDRelocate_TextChanged(sender As Object, e As EventArgs) Handles txtAccountIDRelocate.TextChanged
        Try
            lblAlertRelocate.Text = ""
            txtBillingNameRelocate.Text = ""

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            If conn.State = ConnectionState.Open Then
                conn.Close()
                conn.Dispose()
            End If
            conn.Open()

            Dim command1 As MySqlCommand = New MySqlCommand
            command1.CommandText = "SELECT BillingName FROM tblcompany where accountid= '" & txtAccountIDRelocate.Text.Trim & "'"

            command1.Connection = conn
            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New System.Data.DataTable
            dt.Load(dr)
            If dt.Rows.Count > 0 Then

                txtBillingNameRelocate.Text = dt.Rows(0)("BillingName").ToString()

            Else
                txtBillingNameRelocate.Text = ""

                lblAlertRelocate.Text = "INVALID ACCOUNT ID"
                'mdlPopupRelocate.Show()
            End If

            conn.Close()
            conn.Dispose()

            command1.Dispose()
            mdlPopupRelocate.Show()
        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "txtAccountIDRelocate_TextChanged", ex.Message.ToString, "")
        End Try
    End Sub

    Protected Sub btnEditSendStatement_Click(sender As Object, e As ImageClickEventArgs) Handles btnEditSendStatement.Click
        lblMessage.Text = ""
        lblAlert.Text = ""

        If txtRcno.Text = "" Then
            ' MessageBox.Message.Alert(Page, "Select a record to delete!!!", "str")
            lblAlert.Text = "SELECT RECORD TO EDIT SEND STATEMENT"
            Return

        End If
        mdlPopupEditSendStatement.Show()
    End Sub


    Protected Sub btnSaveSendStatement_Click(sender As Object, e As EventArgs) Handles btnSaveSendStatement.Click
        Try
            If chkAutoEmailInvoiceEdit.Checked = False And chkSendStatementInvEdit.Checked = False Then
                If String.IsNullOrEmpty(txtBillingOptionRemarksEdit.Text) Then
                    lblAlertSendStatement.Text = "REMARKS CANNOT BE BLANK"

                    mdlPopupEditSendStatement.Show()
                    Exit Sub


                End If
            End If
            lblAlertSendStatement.Text = ""

            Dim qry As String
            qry = ""
            Dim conn As MySqlConnection = New MySqlConnection()
            Dim command As MySqlCommand = New MySqlCommand
            command.CommandType = CommandType.Text

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            qry = "Update tblCompany set    "
            qry = qry + " ArCurrency = @Currency, ARTerm=@ARTerm, AutoEmailInvoice = @AutoEmailInvoice,AutoEmailSOA=@AutoEmailSOA, DefaultInvoiceFormat= @DefaultInvoiceFormat, SendStatement = @SendStatement,HardCopyInvoice=@HardCopyInvoice,BillingOptionRemarks=@BillingOptionRemarks,RequireEBilling=@RequireEBilling,LastModifiedBy = @LastModifiedBy, LastModifiedOn=@LastModifiedOn "
            qry = qry + " where Rcno = @Rcno;"

            command.CommandText = qry
            command.Parameters.Clear()
            command.Parameters.AddWithValue("@Rcno", txtRcno.Text)

            command.Parameters.AddWithValue("@Currency", ddlCurrencyEdit.Text)
            command.Parameters.AddWithValue("@ARTerm", ddlTermsEdit.Text)
            command.Parameters.AddWithValue("@AutoEmailInvoice", chkAutoEmailInvoiceEdit.Checked)
            command.Parameters.AddWithValue("@AutoEmailSOA", chkAutoEmailStatementEdit.Checked)
            command.Parameters.AddWithValue("@DefaultInvoiceFormat", ddlDefaultInvoiceFormatEdit.Text)
            command.Parameters.AddWithValue("@SendStatement", chkSendStatementSOAEdit.Checked)
            command.Parameters.AddWithValue("@HardCopyInvoice", chkSendStatementInvEdit.Checked)
            command.Parameters.AddWithValue("@BillingOptionRemarks", txtBillingOptionRemarksEdit.Text)
            command.Parameters.AddWithValue("@RequireEBilling", chkRequireEBillingEdit.Checked)

            command.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
            command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
            command.Connection = conn
            command.ExecuteNonQuery()

            conn.Close()
            conn.Dispose()
            command.Dispose()

            'SqlDataSource1.SelectCommand = txt.Text
            GridView1.DataSourceID = "SqlDataSource1"
            GridView1.DataBind()

            ddlCurrency.Text = ddlCurrencyEdit.Text
            ddlTerms.Text = ddlTermsEdit.Text
            chkAutoEmailInvoice.Checked = chkAutoEmailInvoiceEdit.Checked
            chkAutoEmailStatement.Checked = chkAutoEmailStatementEdit.Checked
            ddlDefaultInvoiceFormat.Text = ddlDefaultInvoiceFormatEdit.Text
            chkSendStatementInv.Checked = chkSendStatementInvEdit.Checked
            chkSendStatementSOA.Checked = chkSendStatementSOAEdit.Checked
            txtBillingOptionRemarks.Text = txtBillingOptionRemarksEdit.Text
            chkRequireEBilling.Checked = chkRequireEBillingEdit.Checked

            'CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CORPORATE", txtAccountID.Text, "EDITSENDSTATEMENT", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtAccountID.Text, "", txtRcno.Text)
            CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CORP", txtAccountID.Text, "EDITSENDSTATEMENT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")), 0, 0, 0, txtAccountID.Text, "", txtRcno.Text)

        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "btnSaveSendStatement_Click", ex.Message.ToString, txtAccountID.Text)
        End Try
    End Sub


    Protected Sub txtSearchContactNo_TextChanged(sender As Object, e As EventArgs) Handles txtSearchContactNo.TextChanged

    End Sub

    Protected Sub btnAddSpecificLocation_Click(sender As Object, e As EventArgs) Handles btnAddSpecificLocation.Click
        DisableSpecificLocationControls()

        MakeSpecificLocationNull()
        'lblMessage.Text = "ACTION: ADD SPECIFIC LOCATION"
        txtCreatedOn.Text = ""
        txtSpecificLocationMode.Text = "Add"
        txtSpecificLocation.Focus()
        mdlPopupSpecificLocaion.Show()
    End Sub

    Protected Sub btnEditSpecificLocation_Click(sender As Object, e As EventArgs) Handles btnEditSpecificLocation.Click
        lblAlert.Text = ""
        lblMessage.Text = ""
        If txtSpecificLocationRcNo.Text = "" Then
            ' MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
            lblAlert.Text = "SELECT RECORD TO EDIT"
            Return

        End If
        If ddlStatus.Text = "O" Then
            DisableSpecificLocationControls()
            txtSpecificLocationMode.Text = "Edit"
            lblMessage.Text = "ACTION: EDIT NOTES"
            txtCreatedOn.Text = ""
            mdlPopupSpecificLocaion.Show()
        Else
            lblMessage.Text = "ONLY OPEN RECORDS CAN BE EDITED"
        End If
    End Sub

    Protected Sub btnDeleteSpecificLocation_Click(sender As Object, e As EventArgs) Handles btnDeleteSpecificLocation.Click
        lblMessage.Text = ""
        If txtSpecificLocationRcNo.Text = "" Then
            ' MessageBox.Message.Alert(Page, "Select a record to delete!!!", "str")
            lblAlert.Text = "SELECT RECORD TO DELETE"
            Return
        End If
        lblMessage.Text = "ACTION: DELETE SPECIFIC LOCATION"

        Dim confirmValue As String = Request.Form("confirm_value")
        If Right(confirmValue, 3) = "Yes" Then
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                command1.CommandText = "SELECT * FROM tblcompanylocationspecificlocation where rcno=" & Convert.ToInt32(txtSpecificLocationRcNo.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "delete from tblcompanylocationspecificlocation where rcno=" & Convert.ToInt32(txtSpecificLocationRcNo.Text)

                    command.CommandText = qry

                    command.Connection = conn

                    command.ExecuteNonQuery()

                    '  MessageBox.Message.Alert(Page, "Record deleted successfully!!!", "str")
                    lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"

                End If
                conn.Close()
                conn.Dispose()
                command1.Dispose()
                dt.Dispose()
                dr.Close()

            Catch ex As Exception
                InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "SPECIFIC LOCATION DELETE", ex.Message.ToString, txtAccountID.Text)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            End Try

            EnableSpecificLocationControls()
            SqlDSSpecificLocation.SelectCommand = "select * from tblcompanylocationspecificlocation where LocationID = '" + txtLocationIDSpecificLocation.Text + "'"
            SqlDSSpecificLocation.DataBind()
            gvSpecificLocation.DataBind()
            MakeSpecificLocationNull()
            lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"
            mdlPopupSpecificLocaion.Show()
        End If
    End Sub

    Protected Sub btnSaveSpecificLocaion_Click(sender As Object, e As EventArgs) Handles btnSaveSpecificLocaion.Click
        If String.IsNullOrEmpty(txtSpecificLocation.Text) Then
            ' MessageBox.Message.Alert(Page, "Select Staff to proceed!!", "str")
            lblAlert.Text = "ENTER SPECIFIC LOCATION"
            Return
        End If

        If txtSpecificLocationMode.Text = "Add" Then
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                command1.CommandText = "SELECT * FROM tblcompanylocationspecificlocation where LocationId=@LocationId and SpecificLocationName=@SpecificLocation"
                command1.Parameters.AddWithValue("@LocationId", txtLocationIDSpecificLocation.Text)
                command1.Parameters.AddWithValue("@SpecificLocation", txtSpecificLocation.Text)

                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    '    MessageBox.Message.Alert(Page, "Selected Staff already assigned for this service!!!", "str")
                    lblAlert.Text = "SPECIFIC LOCATION ALREADY EXISTS"

                Else

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "INSERT INTO tblcompanylocationspecificlocation(AccountID, LocationID, SpecificLocationName, Zone, CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn)VALUES(@AccountID, @LocationID,@SpecificLocation,@Zone, @CreatedOn,@CreatedBy,@LastModifiedBy,@LastModifiedOn);"
                    command.CommandText = qry
                    command.Parameters.Clear()
                    If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then
                        command.Parameters.AddWithValue("@AccountID", txtAccountIDSpecificLocation.Text.ToUpper)
                        command.Parameters.AddWithValue("@LocationID", txtLocationIDSpecificLocation.Text.ToUpper)

                        command.Parameters.AddWithValue("@SpecificLocation", txtSpecificLocation.Text.ToUpper)
                        command.Parameters.AddWithValue("@Zone", txtZone.Text.ToUpper)
                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))



                    ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then
                        command.Parameters.AddWithValue("@AccountID", txtAccountIDSpecificLocation.Text)
                        command.Parameters.AddWithValue("@LocationID", txtLocationIDSpecificLocation.Text)
                        command.Parameters.AddWithValue("@SpecificLocation", txtSpecificLocation.Text)
                        command.Parameters.AddWithValue("@Zone", txtZone.Text)
                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                        command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))


                    End If


                    command.Connection = conn

                    command.ExecuteNonQuery()
                    txtSpecificLocationRcNo.Text = command.LastInsertedId

                    '   MessageBox.Message.Alert(Page, "Record added successfully!!!", "str")
                    lblMessage.Text = "ADD: SPECIFIC LOCATION SUCCESSFULLY ADDED"
                    lblAlert.Text = ""

                End If
                conn.Close()
                mdlPopupSpecificLocaion.Show()
            Catch ex As Exception
                InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "SPECIFIC LOCATION ADD SAVE", ex.Message.ToString, txtAccountID.Text)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            End Try

            EnableSpecificLocationControls()
        ElseIf txtSpecificLocationMode.Text = "Edit" Then
            If txtSpecificLocationRcNo.Text = "" Then
                '   MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
                lblAlert.Text = "SELECT RECORD TO EDIT"

                Return

            End If
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command2 As MySqlCommand = New MySqlCommand

                command2.CommandType = CommandType.Text
                command2.CommandText = "SELECT * FROM tblcompanylocationspecificlocation where SpecificLocationName=@SpecificLocaionName and LocationID=@LocationID and rcno<>" & Convert.ToInt32(txtSpecificLocationRcNo.Text)
                command2.Parameters.AddWithValue("@LocationID", txtLocationIDSpecificLocation.Text)
                command2.Parameters.AddWithValue("@SpecificLocaionName", txtSpecificLocation.Text)
                command2.Connection = conn

                Dim dr1 As MySqlDataReader = command2.ExecuteReader()
                Dim dt1 As New DataTable
                dt1.Load(dr1)

                If dt1.Rows.Count > 0 Then

                    lblAlert.Text = "SPECIFIC LOCATION ALREADY EXISTS"



                Else

                    Dim command1 As MySqlCommand = New MySqlCommand

                    command1.CommandType = CommandType.Text

                    command1.CommandText = "SELECT * FROM tblcompanylocationspecificlocation where rcno=" & Convert.ToInt32(txtSpecificLocationRcNo.Text)
                    command1.Connection = conn

                    Dim dr As MySqlDataReader = command1.ExecuteReader()
                    Dim dt As New DataTable
                    dt.Load(dr)

                    If dt.Rows.Count > 0 Then

                        Dim command As MySqlCommand = New MySqlCommand

                        command.CommandType = CommandType.Text
                        Dim qry As String = "UPDATE tblcompanylocationspecificlocation SET SpecificLocationName=@SpecificLocaion, Zone=@Zone, LastModifiedBy = @LastModifiedBy,LastModifiedOn = @LastModifiedOn WHERE  rcno=" & Convert.ToInt32(txtSpecificLocationRcNo.Text)

                        command.CommandText = qry
                        command.Parameters.Clear()

                        If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then
                            command.Parameters.AddWithValue("@SpecificLocaion", txtSpecificLocation.Text.ToUpper)
                            command.Parameters.AddWithValue("@Zone", txtZone.Text.ToUpper)
                            command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                            command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                        ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then
                            command.Parameters.AddWithValue("@SpecificLocaion", txtSpecificLocation.Text)
                            command.Parameters.AddWithValue("@Zone", txtZone.Text)
                            command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                            command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        End If

                        command.Connection = conn

                        command.ExecuteNonQuery()

                        '  MessageBox.Message.Alert(Page, "Record updated successfully!!!", "str")
                        lblMessage.Text = "EDIT: SPECIFIC LOCATION SUCCESSFULLY UPDATED"
                        lblAlert.Text = ""
                    End If
                End If


                txtSpecificLocationMode.Text = ""

                conn.Close()
                conn.Dispose()
                command2.Dispose()
                dt1.Dispose()
                dr1.Close()
                mdlPopupSpecificLocaion.Show()
            Catch ex As Exception
                InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "SPECIFIC LOCATION EDIT SAVE", ex.Message.ToString, txtAccountID.Text)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            End Try
            EnableSpecificLocationControls()

        End If
        SqlDSSpecificLocation.SelectCommand = "select * from tblcompanylocationspecificlocation where LocationID = '" + txtLocationIDSpecificLocation.Text + "'"
        SqlDSSpecificLocation.DataBind()
        gvSpecificLocation.DataBind()

        txtSpecificLocationMode.Text = ""
    End Sub

    Public Sub MakeSpecificLocationNull()
        txtSpecificLocationMode.Text = ""
        txtSpecificLocation.Text = ""
        txtSpecificLocationRcNo.Text = ""
        txtZone.Text = ""

    End Sub

    Private Sub DisableSpecificLocationControls()

        btnSaveSpecificLocaion.Enabled = True
        btnSaveSpecificLocaion.ForeColor = System.Drawing.Color.Black
        btnSpecificLocationCancel.Enabled = True
        btnSpecificLocationCancel.ForeColor = System.Drawing.Color.Black

        btnAddSpecificLocation.Enabled = False
        btnAddSpecificLocation.ForeColor = System.Drawing.Color.Gray

        btnEditSpecificLocation.Enabled = False
        btnEditSpecificLocation.ForeColor = System.Drawing.Color.Gray

        btnDeleteSpecificLocation.Enabled = False
        btnDeleteSpecificLocation.ForeColor = System.Drawing.Color.Gray

        txtSpecificLocation.Enabled = True
        txtZone.Enabled = True
    End Sub


    Private Sub EnableSpecificLocationControls()

        btnSaveSpecificLocaion.Enabled = False
        btnSaveSpecificLocaion.ForeColor = System.Drawing.Color.Gray
        btnSpecificLocationCancel.Enabled = False
        btnSpecificLocationCancel.ForeColor = System.Drawing.Color.Gray

        btnAddSpecificLocation.Enabled = True
        btnAddSpecificLocation.ForeColor = System.Drawing.Color.Black

        btnEditSpecificLocation.Enabled = False
        btnEditSpecificLocation.ForeColor = System.Drawing.Color.Gray

        btnDeleteSpecificLocation.Enabled = False
        btnDeleteSpecificLocation.ForeColor = System.Drawing.Color.Gray

        txtSpecificLocation.Enabled = False
        txtZone.Enabled = False


    End Sub
    'Public NotesRcno As String

    Protected Sub btnSpecificLocation_Click(sender As Object, e As EventArgs) Handles btnSpecificLocation.Click
        If txtRcno.Text = "" Then
            ' MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
            lblAlert.Text = "SELECT RECORD TO EDIT"
            '   mdlPopupEditBilling.Show()
            Return

        End If

        If txtSvcRcno.Text = "" Then
            ' MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
            lblAlert.Text = "SELECT RECORD TO ENER SPECIFIC LOCATION"
            '   mdlPopupEditBilling.Show()
            Return

        End If

        txtLocationIDSpecificLocation.Text = txtLocationID.Text
        txtAccountIDSpecificLocation.Text = txtAccountID.Text
        'DisableSpecificLocationControls()
        SqlDSSpecificLocation.SelectCommand = "select * from tblcompanylocationspecificlocation where LocationID = '" + txtLocationIDSpecificLocation.Text + "' order by SpecificlocationName"
        SqlDSSpecificLocation.DataBind()
        gvSpecificLocation.DataBind()
        mdlPopupSpecificLocaion.Show()

    End Sub


    Protected Sub OnRowDataBoundgSpecificLocation(sender As Object, e As GridViewRowEventArgs) Handles gvSpecificLocation.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='silver';this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#E4E4E4';")
            'e.Row.Attributes.Add("onmousedown", "this.style.backgroundColor='#738A9C';")
            'e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#738A9C';")

            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvSpecificLocation, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub


    Protected Sub OnSelectedIndexChangedgSpecificLocation(sender As Object, e As EventArgs) Handles gvSpecificLocation.SelectedIndexChanged
        For Each row As GridViewRow In gvSpecificLocation.Rows
            'If row.RowIndex = gvNotesMaster.SelectedIndex Then
            '    row.BackColor = ColorTranslator.FromHtml("#738A9C")
            '    row.ToolTip = String.Empty
            'Else
            '    row.BackColor = ColorTranslator.FromHtml("#E4E4E4")
            '    row.ToolTip = "Click to select this row."
            'End If

            If row.RowIndex = gvSpecificLocation.SelectedIndex Then
                row.BackColor = ColorTranslator.FromHtml("#00ccff")
                row.ToolTip = String.Empty
            Else
                If row.RowIndex Mod 2 = 0 Then
                    row.BackColor = ColorTranslator.FromHtml("#EFF3FB")
                    row.ToolTip = "Click to select this row."
                Else
                    row.BackColor = ColorTranslator.FromHtml("#ffffff")
                    row.ToolTip = "Click to select this row."
                End If
            End If
        Next
    End Sub



    Protected Sub gvSpecificLocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvSpecificLocation.SelectedIndexChanged
        Try
            MakeSpecificLocationNull()
            '  txtTechMode.Text = "Edit"
            Dim editindex As Integer = gvSpecificLocation.SelectedIndex
            rcno = DirectCast(gvSpecificLocation.Rows(editindex).FindControl("Label63"), Label).Text
            txtSpecificLocationRcNo.Text = rcno.ToString()
            txtSpecificLocation.Text = Server.HtmlDecode(gvSpecificLocation.SelectedRow.Cells(1).Text)
            txtZone.Text = Server.HtmlDecode(gvSpecificLocation.SelectedRow.Cells(2).Text)
            EnableSpecificLocationControls()

            btnEditSpecificLocation.Enabled = True
            btnEditSpecificLocation.ForeColor = System.Drawing.Color.Black
            btnDeleteSpecificLocation.Enabled = True
            btnDeleteSpecificLocation.ForeColor = System.Drawing.Color.Black

            mdlPopupSpecificLocaion.Show()
        Catch ex As Exception
            MessageBox.Message.Alert(Page, ex.Message.ToString, "str")
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Protected Sub btnSpecificLocaionCancel_Click(sender As Object, e As EventArgs)

    End Sub

    Protected Sub btnSpecificLocationCancel_Click(sender As Object, e As EventArgs) Handles btnSpecificLocationCancel.Click
        MakeSpecificLocationNull()
        EnableSpecificLocationControls()
        txtSpecificLocation.Text = ""
        mdlPopupSpecificLocaion.Show()
    End Sub


    '' Client Access

    Protected Sub OnRowDataBoundgCP(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='silver';this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#E4E4E4';")
            'e.Row.Attributes.Add("onmousedown", "this.style.backgroundColor='#738A9C';")
            'e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#738A9C';")

            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvCP, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Public Sub MakeCPNull()
        'txtAccountIDCP.Text = ""
        'txtStatusCP.Text = ""
        chkStatusCP.Checked = True
        txtNameCP.Text = ""
        txtEmailCP.Text = ""
        txtUserIDCP.Text = ""
        txtPwdCP.Text = ""
        chkChangePasswordonNextLogin.Checked = False
    End Sub


    Private Sub DisableCPControls()

        btnSaveCP.Enabled = True
        btnSaveCP.ForeColor = System.Drawing.Color.Black
        btnCancelCP.Enabled = True
        btnCancelCP.ForeColor = System.Drawing.Color.Black

        btnAddCP.Enabled = False
        btnAddCP.ForeColor = System.Drawing.Color.Gray

        btnEditCP.Enabled = False
        btnEditCP.ForeColor = System.Drawing.Color.Gray

        btnDeleteCP.Enabled = False
        btnDeleteCP.ForeColor = System.Drawing.Color.Gray

        'txtStatusCP.Enabled = True
        chkStatusCP.Enabled = True
        txtNameCP.Enabled = True
        txtEmailCP.Enabled = True
        txtUserIDCP.Enabled = True
        txtPwdCP.Enabled = True
        chkChangePasswordonNextLogin.Enabled = True
        'txtNotes.Enabled = True

    End Sub

    Private Sub EnableCPControls()

        btnSaveCP.Enabled = False
        btnSaveCP.ForeColor = System.Drawing.Color.Gray
        btnCancelCP.Enabled = False
        btnCancelCP.ForeColor = System.Drawing.Color.Gray

        btnAddCP.Enabled = True
        btnAddCP.ForeColor = System.Drawing.Color.Black
        btnEditCP.Enabled = False
        btnEditCP.ForeColor = System.Drawing.Color.Gray
        btnDeleteCP.Enabled = False
        btnDeleteCP.ForeColor = System.Drawing.Color.Gray

        txtAccountIDCP.Enabled = False
        'txtStatusCP.Enabled = False
        chkStatusCP.Enabled = False
        txtNameCP.Enabled = False
        txtEmailCP.Enabled = False
        txtUserIDCP.Enabled = False
        txtPwdCP.Enabled = False
        chkChangePasswordonNextLogin.Enabled = False

    End Sub

    Protected Sub btnAddCP_Click(sender As Object, e As EventArgs) Handles btnAddCP.Click
        DisableCPControls()

        MakeCPNull()
        lblMessage.Text = "ACTION: ADD CLIENT ACCESS"
        txtCreatedOn.Text = ""
        txtCPMode.Text = "Add"
        chkStatusCP.Enabled = False
        txtAccountIDCP.Focus()
    End Sub

    Protected Sub btnEditCP_Click(sender As Object, e As EventArgs) Handles btnEditCP.Click
        Try
            lblAlert.Text = ""
            lblMessage.Text = ""
            txtCreatedOn.Text = ""
            If txtCPRcno.Text = "" Then
                ' MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
                lblAlert.Text = "SELECT RECORD TO EDIT"
                Return

            End If
            If ddlStatus.Text = "O" Then
                DisableCPControls()
                txtCPMode.Text = "Edit"
                lblMessage.Text = "ACTION: EDIT CLIENT ACCESS"
            Else
                lblMessage.Text = "ONLY OPEN RECORDS CAN BE EDITED"
            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "btnEditCP_Click", ex.Message.ToString, txtAccountID.Text)
        End Try
    End Sub

    Protected Sub btnDeleteCP_Click(sender As Object, e As EventArgs) Handles btnDeleteCP.Click
        lblMessage.Text = ""
        If txtCPRcno.Text = "" Then
            ' MessageBox.Message.Alert(Page, "Select a record to delete!!!", "str")
            lblAlert.Text = "SELECT RECORD TO DELETE"
            Return
        End If
        lblMessage.Text = "ACTION: DELETE CLIENT ACCESS"

        Dim confirmValue As String = Request.Form("confirm_value")
        If Right(confirmValue, 3) = "Yes" Then
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                'Dim command1 As MySqlCommand = New MySqlCommand

                'command1.CommandType = CommandType.Text

                'command1.CommandText = "SELECT * FROM tblcompanycustomeraccess where rcno=" & Convert.ToInt32(txtNotesRcNo.Text)
                'command1.Connection = conn

                'Dim dr As MySqlDataReader = command1.ExecuteReader()
                'Dim dt As New DataTable
                'dt.Load(dr)

                'If dt.Rows.Count > 0 Then

                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                Dim qry As String = "delete from tblcompanycustomeraccess where rcno=" & Convert.ToInt32(txtCPRcno.Text)

                command.CommandText = qry
                command.Connection = conn
                command.ExecuteNonQuery()

                '  MessageBox.Message.Alert(Page, "Record deleted successfully!!!", "str")
                lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"

                'End If
                conn.Close()
                conn.Dispose()
                'command1.Dispose()
                'dt.Dispose()
                'dr.Close()

            Catch ex As Exception
                InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "btnDeleteCP_Click", ex.Message.ToString, txtAccountID.Text)
            End Try
            EnableCPControls()

            SqlDSCP.SelectCommand = "select * from tblcompanycustomeraccess where Accountid = '" + txtAccountID.Text + "'"
            SqlDSCP.DataBind()
            gvCP.DataBind()
            MakeCPNull()
            lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"

        End If
    End Sub

    Protected Sub btnSaveCP_Click(sender As Object, e As EventArgs) Handles btnSaveCP.Click
        If String.IsNullOrEmpty(txtNameCP.Text) Then
            ' MessageBox.Message.Alert(Page, "Select Staff to proceed!!", "str")
            lblAlert.Text = "ENTER NAME"
            Return
        End If

        If txtCPMode.Text = "Add" Then
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                'Dim command1 As MySqlCommand = New MySqlCommand

                'command1.CommandType = CommandType.Text

                'command1.CommandText = "SELECT * FROM tblNOTES where KEYFIELD=@recordno and notes=@notes"
                'command1.Parameters.AddWithValue("@recordno", lblNotesKeyField.Text)
                'command1.Parameters.AddWithValue("@notes", txtNotes.Text)

                'command1.Connection = conn

                'Dim dr As MySqlDataReader = command1.ExecuteReader()
                'Dim dt As New DataTable
                'dt.Load(dr)

                'If dt.Rows.Count > 0 Then

                '    '    MessageBox.Message.Alert(Page, "Selected Staff already assigned for this service!!!", "str")
                '    lblAlert.Text = "NOTES ALREADY EXISTS"

                'Else

                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                Dim qry As String = "INSERT INTO tblcompanycustomeraccess(AccountID, Status, Name, EmailAddress, Password, UserID,  ChangepasswordOnNextLogin, CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn)VALUES(@AccountID, @Status, @Name, @EmailAddress, @Password, @UserID,  @ChangepasswordOnNextLogin,@CreatedOn,@CreatedBy,@LastModifiedBy,@LastModifiedOn);"
                command.CommandText = qry
                command.Parameters.Clear()
                If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then
                    command.Parameters.AddWithValue("@AccountID", txtAccountIDCP.Text)

                    command.Parameters.AddWithValue("@Status", chkStatusCP.Checked)
                    command.Parameters.AddWithValue("@Name", txtNameCP.Text.ToUpper)
                    command.Parameters.AddWithValue("@EmailAddress", txtEmailCP.Text.ToUpper)
                    command.Parameters.AddWithValue("@Password", txtPwdCP.Text)
                    command.Parameters.AddWithValue("@UserID", txtUserIDCP.Text.ToUpper)

                    command.Parameters.AddWithValue("@ChangepasswordOnNextLogin", chkChangePasswordonNextLogin.Checked)

                    command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                    command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                    command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then
                    command.Parameters.AddWithValue("@AccountID", txtAccountIDCP.Text)
                    command.Parameters.AddWithValue("@Status", chkStatusCP.Checked)
                    command.Parameters.AddWithValue("@Name", txtNameCP.Text.ToUpper)
                    command.Parameters.AddWithValue("@EmailAddress", txtEmailCP.Text.ToUpper)
                    command.Parameters.AddWithValue("@Password", txtPwdCP.Text)
                    command.Parameters.AddWithValue("@UserID", txtUserIDCP.Text.ToUpper)
                    command.Parameters.AddWithValue("@ChangepasswordOnNextLogin", chkChangePasswordonNextLogin.Checked)
                    command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                    command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                    command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                End If


                command.Connection = conn

                command.ExecuteNonQuery()
                txtCPRcno.Text = command.LastInsertedId

                '   MessageBox.Message.Alert(Page, "Record added successfully!!!", "str")
                lblMessage.Text = "ADD: CLIENT ACCESS RECORD SUCCESSFULLY ADDED"
                lblAlert.Text = ""

                'End If
                conn.Close()
                conn.Dispose()
                'command1.Dispose()
                'dt.Dispose()
                'dr.Close()

            Catch ex As Exception
                InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "btnSaveCP_Click", ex.Message.ToString, txtAccountID.Text)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            End Try
            EnableCPControls()

        ElseIf txtCPMode.Text = "Edit" Then
            If txtCPRcno.Text = "" Then
                '   MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
                lblAlert.Text = "SELECT RECORD TO EDIT"

                Return

            End If
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                'Dim command2 As MySqlCommand = New MySqlCommand

                'command2.CommandType = CommandType.Text
                'command2.CommandText = "SELECT * FROM tblNOTES where KEYFIELD=@recordno and NOTES=@notes and rcno<>" & Convert.ToInt32(txtNotesRcNo.Text)
                'command2.Parameters.AddWithValue("@recordno", lblNotesKeyField.Text)
                'command2.Parameters.AddWithValue("@notes", txtNotes.Text)

                'command2.Connection = conn

                'Dim dr1 As MySqlDataReader = command2.ExecuteReader()
                'Dim dt1 As New DataTable
                'dt1.Load(dr1)

                'If dt1.Rows.Count > 0 Then

                '    lblAlert.Text = "NOTES ALREADY EXISTS"
                'Else

                'Dim command1 As MySqlCommand = New MySqlCommand

                'command1.CommandType = CommandType.Text

                'command1.CommandText = "SELECT * FROM tblnotes where rcno=" & Convert.ToInt32(txtNotesRcNo.Text)
                'command1.Connection = conn

                'Dim dr As MySqlDataReader = command1.ExecuteReader()
                'Dim dt As New DataTable
                'dt.Load(dr)

                'If dt.Rows.Count > 0 Then

                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                Dim qry As String = "UPDATE tblcompanycustomeraccess SET AccountID=@AccountID, Name=@Name, Status = @Status, EmailAddress=@EmailAddress,Password = @Password, UserID=@UserID, ChangepasswordOnNextLogin=@ChangepasswordOnNextLogin, LastModifiedBy = @LastModifiedBy,LastModifiedOn = @LastModifiedOn WHERE  rcno=" & Convert.ToInt32(txtCPRcno.Text)

                command.CommandText = qry
                command.Parameters.Clear()

                If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then
                    command.Parameters.AddWithValue("@AccountID", txtAccountIDCP.Text.ToUpper)
                    command.Parameters.AddWithValue("@Name", txtNameCP.Text.ToUpper)
                    command.Parameters.AddWithValue("@Status", chkStatusCP.Checked)
                    command.Parameters.AddWithValue("@EmailAddress", txtEmailCP.Text.ToUpper)
                    command.Parameters.AddWithValue("@Password", txtPwdCP.Text)
                    command.Parameters.AddWithValue("@UserID", txtUserIDCP.Text.ToUpper)
                    command.Parameters.AddWithValue("@ChangepasswordOnNextLogin", chkChangePasswordonNextLogin.Checked)
                    command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                    command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then
                    command.Parameters.AddWithValue("@AccountID", txtAccountIDCP.Text)
                    command.Parameters.AddWithValue("@Name", txtNameCP.Text)
                    command.Parameters.AddWithValue("@Status", chkStatusCP.Checked)
                    command.Parameters.AddWithValue("@EmailAddress", txtEmailCP.Text)
                    command.Parameters.AddWithValue("@Password", txtPwdCP.Text)
                    command.Parameters.AddWithValue("@UserID", txtUserIDCP.Text)
                    command.Parameters.AddWithValue("@ChangepasswordOnNextLogin", chkChangePasswordonNextLogin.Checked)

                    command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                    command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                End If

                command.Connection = conn

                command.ExecuteNonQuery()

                '  MessageBox.Message.Alert(Page, "Record updated successfully!!!", "str")
                lblMessage.Text = "EDIT: NOTES SUCCESSFULLY UPDATED"
                lblAlert.Text = ""
                'End If
                'End If


                txtCPMode.Text = ""

                conn.Close()
                conn.Dispose()
                'command2.Dispose()
                'dt1.Dispose()
                'dr1.Close()

            Catch ex As Exception
                InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "btnSaveCP_Click", ex.Message.ToString, txtAccountID.Text)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            End Try

            EnableCPControls()
        End If
        SqlDSCP.SelectCommand = "select * from tblcompanycustomeraccess where AccountId = '" + txtAccountID.Text + "'"
        SqlDSCP.DataBind()
        gvCP.DataBind()

        txtCPMode.Text = ""
    End Sub

    Protected Sub btnCancelCP_Click(sender As Object, e As EventArgs) Handles btnCancelCP.Click
        MakeCPNull()
        EnableCPControls()
        txtCPMode.Text = ""
    End Sub

    Protected Sub gvCP_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvCP.PageIndexChanging
        'gvNotesMaster.PageIndex = e.NewPageIndex

        'SqlDSNotesMaster.SelectCommand = "SELECT * From tblnotes where rcno <>0 and keyfield='" + txtAccountID.Text + "'"


        'SqlDSNotesMaster.DataBind()
        'gvNotesMaster.DataBind()
    End Sub

    Protected Sub gvCP_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvCP.SelectedIndexChanged
        Try
            MakeCPNull()
            '  txtTechMode.Text = "Edit"
            Dim editindex As Integer = gvCP.SelectedIndex
            rcno = DirectCast(gvCP.Rows(editindex).FindControl("Label1"), Label).Text
            txtCPRcno.Text = rcno.ToString()
            chkStatusCP.Checked = gvCP.SelectedRow.Cells(5).Text
            'txtStatusCP.Text = gvCP.SelectedRow.Cells(2).Text
            txtNameCP.Text = gvCP.SelectedRow.Cells(2).Text
            txtUserIDCP.Text = gvCP.SelectedRow.Cells(4).Text
            txtEmailCP.Text = gvCP.SelectedRow.Cells(3).Text
            'txtStatusCP.Text = gvCP.SelectedRow.Cells(2).Text

            EnableCPControls()

            btnEditCP.Enabled = True
            btnEditCP.ForeColor = System.Drawing.Color.Black
            btnDeleteCP.Enabled = True
            btnDeleteCP.ForeColor = System.Drawing.Color.Black


        Catch ex As Exception
            MessageBox.Message.Alert(Page, ex.ToString, "str")
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Protected Sub btnContractGroupEditSave_Click(sender As Object, e As EventArgs) Handles btnContractGroupEditSave.Click
        If ddlContractGroupEdit.Text = ddlContractGrp.Text Then
            lblAlertContractGroup.Text = "NO CHANGES MADE"
            mdlPopupContractGroup.Show()
            Return

        End If

        Try
            Dim hyphenpos As Integer
            hyphenpos = 0
            hyphenpos = (ddlContractGroupEdit.Text.IndexOf(":"))
            Dim lContractGroup As String

            lContractGroup = Left(ddlContractGroupEdit.Text.Trim, (hyphenpos - 1))

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim command As MySqlCommand = New MySqlCommand

            command.CommandType = CommandType.Text
            command.CommandText = "UPDATE tblCompanyLocation SET ContractGroup = @ContractGroup, LastModifiedBy = @LastModifiedBy, LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(txtSvcRcno.Text)
            command.Parameters.Clear()

            command.Parameters.AddWithValue("@ContractGroup", lContractGroup)
            command.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
            command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

            command.Connection = conn

            command.ExecuteNonQuery()
            conn.Close()
            conn.Dispose()
            command.Dispose()
            ddlContractGrp.Text = ddlContractGroupEdit.Text

            'SqlDataSource1.SelectCommand = txt.Text
            'SqlDataSource1.DataBind()
            'GridView1.DataBind()
            GridView1.DataSourceID = "SqlDataSource1"
            GridView1.DataBind()

            SqlDataSource2.SelectCommand = txtDetail.Text
            SqlDataSource2.DataBind()
            GridView2.DataSourceID = "SqlDataSource2"

            CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "COMPANY", txtLocationID.Text, "EDITCONTRACTGROUP", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtAccountID.Text, "", txtRcno.Text)

            'InsertNewLogDetail()

            'GridView1.DataSourceID = "SqlDataSource1"
            mdlPopupContractGroup.Hide()
            'mdlPopupNotes.Hide()
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + Session("UserID"), "btnContractGroupEditSave_Click", ex.Message.ToString, txtLocationID.Text)
            Exit Sub
        End Try
    End Sub

    Protected Sub btnEditContractGroup_Click(sender As Object, e As ImageClickEventArgs) Handles btnEditContractGroup.Click
        txtCreatedOn.Text = ""
        mdlPopupContractGroup.Show()
    End Sub

    Protected Sub txtCutOffDate_TextChanged(sender As Object, e As EventArgs) Handles txtCutOffDate.TextChanged
        Session("cutoffoscustomer") = txtCutOffDate.Text
        ModalPopupInvoice.Show()
    End Sub

    Protected Sub btnEditHistory_Click(sender As Object, e As EventArgs)
        Try


            If txtMode.Text = "Add" Or txtMode.Text = "Edit" Or txtMode.Text = "Copy" Then
                lblAlert.Text = "RECORD IS IN ADD/EDIT MODE, CLICK SAVE OR CANCEL TO VIEW HISTORY"
                Return
            End If

            lblMessage.Text = ""
            'lblAlertSchDate.Text = ""
            lblAlert.Text = ""

            Dim btn1 As Button = DirectCast(sender, Button)

            Dim xrow1 As GridViewRow = CType(btn1.NamingContainer, GridViewRow)
            Dim rowindex1 As Integer = xrow1.RowIndex

            'MakeMeNull()
            Dim lblidRcno As String = TryCast(GridView1.Rows(rowindex1).FindControl("Label1"), Label).Text

            txtRcno.Text = lblidRcno
            'RetrieveData()

            GridView1.SelectedIndex = rowindex1

            'Dim lblStatus As String = GridView1.Rows(rowindex1).Cells(2).Text
            Dim strRecordNo As String = GridView1.Rows(rowindex1).Cells(2).Text
            'txtRcno.Text = ""

            'rcno = DirectCast(GridView1.Rows(rowindex1).FindControl("Label1"), Label).Text
            'txtRcno.Text = rcno.ToString()

            lblMessage.Text = ""
            'lblAlertSchDate.Text = ""
            lblAlert.Text = ""
            'txtGridIndex.Text = rowindex1.ToString

            sqlDSViewEditHistory.SelectCommand = "Select * from tblEventlog where  DocRef = '" & strRecordNo & "' order by logdate desc"
            sqlDSViewEditHistory.DataBind()

            grdViewEditHistory.DataSourceID = "sqlDSViewEditHistory"
            grdViewEditHistory.DataBind()

            mdlViewEditHistory.Show()


        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + Session("UserID"), "btnEditHistory_Click", ex.Message.ToString, "")

        End Try

    End Sub

    Protected Sub btnUpdateServiceContact_Click(sender As Object, e As EventArgs) Handles btnUpdateServiceContact.Click
        Try
            SqlDataSource3.SelectCommand = "Select Rcno, ContractGroup, LocationID, Address1, AddPOstal from tblCompanyLocation where AccountId = '" & txtAccountID.Text & "' Order by LocationID"

            grvServiceRecDetails.DataSourceID = "SqlDataSource3"
            grvServiceRecDetails.DataBind()
            lblAlertContactServiceUpdate.Text = ""
            txtConfirmationCode.Text = ""

            chkDefaultContactInfo.Checked = False


            txtSvcCP1ContactUpdateContactInformation.Text = ""
            txtSvcCP1PositionUpdateContactInformation.Text = ""
            txtSvcCP1TelephoneUpdateContactInformation.Text = ""
            txtSvcCP1FaxUpdateContactInformation.Text = ""
            txtSvcCP1Telephone2UpdateContactInformation.Text = ""
            txtSvcCP1MobileUpdateContactInformation.Text = ""
            txtSvcCP1EmailUpdateContactInformation.Text = ""
            txtSvcCP2ContactUpdateContactInformation.Text = ""
            txtSvcCP2PositionUpdateContactInformation.Text = ""
            txtSvcCP2TelephoneUpdateContactInformation.Text = ""
            txtSvcCP2FaxUpdateContactInformation.Text = ""
            txtSvcCP2Tel2UpdateContactInformation.Text = ""
            txtSvcCP2MobileUpdateContactInformation.Text = ""
            txtSvcCP2EmailUpdateContactInformation.Text = ""
            txtSvcEmailCCUpdateContactInformation.Text = ""


            mdlUpdateServiceContact.Show()
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + Session("UserID"), "btnEditHistory_Click", ex.Message.ToString, "")
            lblAlert.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            txtConfirmationCode.Text = ""
            lblAlertContactServiceUpdate.Text = ""
            lblAlert.Text = ""
            lblAlertContactServiceUpdate.Text = ""


            If txtSvcCP1ContactUpdateContactInformation.Text = "" Then
                lblAlertContactServiceUpdate.Text = "CONTACT PERSON-1 CANNOT BE BLANK"
                'txtCreatedOn.Text = ""
                txtSvcCP1ContactUpdateContactInformation.Focus()
                mdlUpdateServiceContact.Show()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                Exit Sub
            End If


            If Len(txtSvcCP2EmailUpdateContactInformation.Text) > 100 Then
                lblAlertContactServiceUpdate.Text = "SERVICE CONTACT EMAIL2 SHOULD BE LESS THAN 100 CHARACTERS"
                txtSvcCP2EmailUpdateContactInformation.Focus()
                mdlUpdateServiceContact.Show()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                Return
            End If


            Dim TotRecsSelected, RecsProcessed As Long
            TotRecsSelected = 0
            RecsProcessed = 0
            For Each row As GridViewRow In grvServiceRecDetails.Rows
                Dim chkbox As CheckBox = row.FindControl("chkGrid")

                If chkbox.Checked = True Then
                    TotRecsSelected = 1
                    GoTo IsRecords
                End If
            Next

            If TotRecsSelected = 0 Then
                lblAlertContactServiceUpdate.Text = "NO RECORD IS SELECTED"
                mdlUpdateServiceContact.Show()
                Exit Sub
            End If
IsRecords:


            If grvServiceRecDetails.Rows.Count = 0 Then
                lblAlertContactServiceUpdate.Text = "NO RECORD TO PROCESS"
                mdlUpdateServiceContact.Show()
                'txtProcessed.Text = "N"
                Exit Sub
            End If


            lblRandom.Text = random.Next(100000, 900000)

            'updPanelMassChange1.Update()
            'lblLine4EditAgreeValueSave.Text = txtRandom.Text
            'If chkUpdateServiceRecords.Checked = False Then
            mdlWarning.Show()
            'Else
            'ProcessUpdate()
            'End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + Session("UserID"), "btnEditHistory_Click", ex.Message.ToString, "")
            lblAlert.Text = ex.Message.ToString
        End Try
    End Sub

    Private Sub ProcessUpdate()
        Try
            '            lblAlert.Text = ""
            '            lblAlertContactServiceUpdate.Text = ""

            '            Dim TotRecsSelected, RecsProcessed As Long
            '            TotRecsSelected = 0
            '            RecsProcessed = 0
            '            For Each row As GridViewRow In grvServiceRecDetails.Rows
            '                Dim chkbox As CheckBox = row.FindControl("chkGrid")

            '                If chkbox.Checked = True Then
            '                    TotRecsSelected = 1
            '                    GoTo IsRecords
            '                End If
            '            Next

            '            If TotRecsSelected = 0 Then
            '                lblAlertContactServiceUpdate.Text = "NO RECORD IS SELECTED"
            '                mdlUpdateServiceContact.Show()
            '                Exit Sub
            '            End If
            'IsRecords:


            '            If grvServiceRecDetails.Rows.Count = 0 Then
            '                lblAlertContactServiceUpdate.Text = "NO RECORD TO PROCESS"
            '                mdlUpdateServiceContact.Show()
            '                'txtProcessed.Text = "N"
            '                Exit Sub
            '            End If

            Dim lLocation As String

            If ddlBranch.SelectedIndex = 0 Then
                lLocation = ""
            Else
                lLocation = ddlBranch.Text
            End If
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()
            '''''''''''''''''' START: UPDATE SERVICE LOCATION '''''''''''''''''''''''''''''''''

            ''''''''''''''' Start :Update tblContractDet '''''''''''''''''''''''''''''''

            Dim totrecsUpdated As Integer
            totrecsUpdated = 0

            For rowIndex As Integer = 0 To grvServiceRecDetails.Rows.Count - 1

                Dim TextBoxchkSelect As CheckBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("chkGrid"), CheckBox)

                If TextBoxchkSelect.Checked = True Then
                    Dim lblLocationID1 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtLocationIdGV"), TextBox)


                    'Start : Update Company Location

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String

                    'qry = "UPDATE tblcompanylocation SET CompanyID = @CompanyID, Description = @Description,ContactPerson = @ContactPerson,Address1 = @Address1,Telephone = @Telephone,Mobile = @Mobile,Email = @Email, LastModifiedBy = @LastModifiedBy,LastModifiedOn = @LastModifiedOn, AddBuilding = @AddBuilding,AddStreet = @AddStreet,AddCity = @AddCity,AddState = @AddState,AddCountry = @AddCountry,AddPostal = @AddPostal,LocateGrp = @LocateGrp,Fax = @Fax,ServiceName = @ServiceName,Contact1Position = @Contact1Position,Telephone2 = @Tel2,ContactPerson2 = @ContactPerson2,Contact2Position = @Contact2Position,Contact2Tel = @Contact2Tel,Contact2Fax = @Contact2Fax,Contact2Tel2 = @Contact2Tel2,Contact2Mobile = @Contact2Mobile,Contact2Email = @Contact2Email,ServiceAddress=@ServiceAddress,  ServiceLocationGroup= @ServiceLocationGroup,BillingNameSvc = @BillingNameSvc,BillAddressSvc = @BillAddressSvc,BillStreetSvc = @BillStreetSvc,BillBuildingSvc = @BillBuildingSvc,BillCitySvc = @BillCitySvc,BillStateSvc = @BillStateSvc,BillCountrySvc = @BillCountrySvc,BillPostalSvc = @BillPostalSvc,BillContact1Svc = @BillContact1Svc,BillPosition1Svc = @BillPosition1Svc,BillTelephone1Svc = @BillTelephone1Svc,BillFax1Svc = @BillFax1Svc,Billtelephone12Svc = @Billtelephone12Svc,BillMobile1Svc = @BillMobile1Svc,BillEmail1Svc = @BillEmail1Svc,BillContact2Svc = @BillContact2Svc,BillPosition2Svc = @BillPosition2Svc,BillTelephone2Svc = @BillTelephone2Svc,BillFax2Svc = @BillFax2Svc,Billtelephone22Svc = @Billtelephone22Svc,BillMobile2Svc = @BillMobile2Svc,BillEmail2Svc = @BillEmail2Svc, InChargeIdSvc=@InChargeIdSvc, ArTermSvc=@ArTermSvc, SalesmanSvc=@SalesmanSvc, SendServiceReportTo1=@SendServiceReportTo1, SendServiceReportTo2=@SendServiceReportTo2, Industry = @Industry, MarketSegmentId=@MarketsegmentID, ContractGroup = @ContractGroup, Comments=@Comments where rcno=" & Convert.ToInt32(txtSvcRcno.Text)
                    qry = "UPDATE tblcompanylocation SET ContactPerson = @ContactPerson,Telephone = @Telephone,Mobile = @Mobile,Email = @Email, Fax = @Fax, Contact1Position = @Contact1Position,Telephone2 = @Tel2,ContactPerson2 = @ContactPerson2,Contact2Position = @Contact2Position,Contact2Tel = @Contact2Tel,Contact2Fax = @Contact2Fax,Contact2Tel2 = @Contact2Tel2,Contact2Mobile = @Contact2Mobile,Contact2Email = @Contact2Email, ServiceEmailCC=@ServiceEmailCC where Accountid= '" & txtAccountID.Text.Trim & "' and LocationID = '" & lblLocationID1.Text & "'"

                    command.CommandText = qry
                    command.Parameters.Clear()

                    command.Parameters.AddWithValue("@ContactPerson", txtSvcCP1ContactUpdateContactInformation.Text.ToUpper)
                    command.Parameters.AddWithValue("@Telephone", txtSvcCP1TelephoneUpdateContactInformation.Text.ToUpper)
                    command.Parameters.AddWithValue("@Mobile", txtSvcCP1MobileUpdateContactInformation.Text.ToUpper)
                    command.Parameters.AddWithValue("@Email", txtSvcCP1EmailUpdateContactInformation.Text.ToUpper)
                    command.Parameters.AddWithValue("@Fax", txtSvcCP1FaxUpdateContactInformation.Text.ToUpper)
                    command.Parameters.AddWithValue("@Contact1Position", txtSvcCP1PositionUpdateContactInformation.Text.ToUpper)
                    command.Parameters.AddWithValue("@Tel2", txtSvcCP1Telephone2UpdateContactInformation.Text.ToUpper)
                    command.Parameters.AddWithValue("@ContactPerson2", txtSvcCP2ContactUpdateContactInformation.Text.ToUpper)
                    command.Parameters.AddWithValue("@Contact2Position", txtSvcCP2PositionUpdateContactInformation.Text.ToUpper)
                    command.Parameters.AddWithValue("@Contact2Tel", txtSvcCP2TelephoneUpdateContactInformation.Text.ToUpper)
                    command.Parameters.AddWithValue("@Contact2Fax", txtSvcCP2FaxUpdateContactInformation.Text.ToUpper)
                    command.Parameters.AddWithValue("@Contact2Tel2", txtSvcCP2Tel2UpdateContactInformation.Text.ToUpper)
                    command.Parameters.AddWithValue("@Contact2Mobile", txtSvcCP2MobileUpdateContactInformation.Text.ToUpper)
                    command.Parameters.AddWithValue("@Contact2Email", txtSvcCP2EmailUpdateContactInformation.Text.ToUpper)
                    command.Parameters.AddWithValue("@ServiceEmailCC", txtSvcEmailCCUpdateContactInformation.Text.ToUpper)
                    command.Connection = conn
                    command.ExecuteNonQuery()


                    'end : Update Company Location


                    Dim ttxtSvcCP1Contact, ttxtAddress, ttxtSvcCP1Telephone, ttxtSvcCP1Mobile, ttxtSvcCP1Email, ttxtBuilding, ttxtStreet, tddlState As String
                    Dim tddlCity, tddlCountry, ttxtPostal, tddlLocateGrp, ttxtSvcCP1Fax, ttxtLocationPrefix, ttxtLocatonNo, ttxtServiceName As String
                    Dim ttxtSvcCP1Position, ttxtSvcCP1Telephone2, ttxtSvcCP2Contact, ttxtSvcCP2Position, ttxtSvcCP2Telephone, ttxtSvcCP2Fax, ttxtSvcCP2Tel2, ttxtSvcCP2Mobile As String
                    Dim ttxtSvcCP2Email, ttxtServiceLocationGroup As String
                    Dim ttxtBillingNameSvc, ttxtBillAddressSvc, ttxtBillStreetSvc, ttxtBillBuildingSvc, tddlBillCitySvc, tddlBillStateSvc As String
                    Dim tddlBillCountrySvc, ttxtBillPostalSvc, ttxtBillContact1Svc, ttxtBillPosition1Svc, ttxtBillTelephone1Svc, ttxtBillFax1Svc As String
                    Dim ttxtBilltelephone12Svc, ttxtBillMobile1Svc, ttxtBillEmail1Svc, ttxtBillContact2Svc, ttxtBillPosition2Svc, ttxtBillTelephone2Svc As String
                    Dim ttxtBillFax2Svc, ttxtBilltelephone22Svc, ttxtBillMobile2Svc, ttxtBillEmail2Svc, tddlSalesManSvc, tddlInchargeSvc As String
                    Dim tddlTermsSvc, tddlIndustrysvc, ttxtMarketSegmentIDsvc, tddlContractGrp, tddlCompanyGrpD, ttxtSvcEmailCC, ttxtSendServiceReportTo1, ttxtSendServiceReportTo2 As String

                    ttxtSvcCP1Contact = ""
                    ttxtAddress = ""
                    ttxtSvcCP1Telephone = ""
                    ttxtSvcCP1Mobile = ""
                    ttxtSvcCP1Email = ""
                    ttxtBuilding = ""
                    ttxtStreet = ""
                    tddlState = ""
                    tddlCity = ""
                    tddlCountry = ""
                    ttxtPostal = ""
                    tddlLocateGrp = ""
                    ttxtSvcCP1Fax = ""
                    ttxtLocationPrefix = ""
                    ttxtLocatonNo = ""
                    ttxtServiceName = ""
                    ttxtSvcCP1Position = ""
                    ttxtSvcCP1Telephone2 = ""
                    ttxtSvcCP2Contact = ""
                    ttxtSvcCP2Position = ""
                    ttxtSvcCP2Telephone = ""
                    ttxtSvcCP2Fax = ""
                    ttxtSvcCP2Tel2 = ""
                    ttxtSvcCP2Mobile = ""
                    ttxtSvcCP2Email = ""
                    ttxtServiceLocationGroup = ""

                    ttxtBillingNameSvc = ""
                    ttxtBillAddressSvc = ""
                    ttxtBillStreetSvc = ""
                    ttxtBillBuildingSvc = ""
                    tddlBillCitySvc = ""
                    tddlBillStateSvc = ""
                    tddlBillCountrySvc = ""
                    ttxtBillPostalSvc = ""
                    ttxtBillContact1Svc = ""
                    ttxtBillPosition1Svc = ""
                    ttxtBillTelephone1Svc = ""
                    ttxtBillFax1Svc = ""

                    ttxtBilltelephone12Svc = ""
                    ttxtBillMobile1Svc = ""
                    ttxtBillEmail1Svc = ""
                    ttxtBillContact2Svc = ""
                    ttxtBillPosition2Svc = ""
                    ttxtBillTelephone2Svc = ""

                    ttxtBillFax2Svc = ""
                    ttxtBilltelephone22Svc = ""
                    ttxtBillMobile2Svc = ""
                    ttxtBillEmail2Svc = ""
                    tddlSalesManSvc = ""
                    tddlInchargeSvc = ""

                    tddlTermsSvc = ""
                    tddlIndustrysvc = ""
                    ttxtMarketSegmentIDsvc = ""
                    tddlContractGrp = ""
                    tddlCompanyGrpD = ""
                    ttxtSvcEmailCC = ""
                    ttxtSendServiceReportTo1 = ""
                    ttxtSendServiceReportTo2 = ""
                    '''

                    Dim cmdServiceLocationDetails As MySqlCommand = New MySqlCommand
                    cmdServiceLocationDetails.CommandType = CommandType.Text

                    Dim lsql As String
                    lsql = ""

                    lsql = "SELECT ContactPerson, Address1, Telephone, Mobile, Email, AddBuilding, AddStreet, AddState, AddCity, AddCountry, AddPostal,"
                    lsql = lsql + " LocateGrp, Fax, LocationPrefix, LocationNo, ServiceName, Contact1Position, Telephone2, ContactPerson2,"
                    lsql = lsql + " Contact2Position, Contact2Tel, Contact2Fax, Contact2Tel2, Contact2Mobile, Contact2Email, ServiceLocationGroup,"
                    lsql = lsql + " BillingNameSvc, BillAddressSvc, BillStreetSvc, BillBuildingSvc, BillCitySvc, BillStateSvc, BillCountrySvc, BillPostalSvc,"
                    lsql = lsql + " BillContact1Svc, BillPosition1Svc, BillTelephone1Svc, BillFax1Svc, Billtelephone12Svc, BillMobile1Svc, BillEmail1Svc,"
                    lsql = lsql + " BillContact2Svc, BillPosition2Svc, BillTelephone2Svc, BillFax2Svc, Billtelephone22Svc, BillMobile2Svc, BillEmail2Svc,"
                    lsql = lsql + " SalesManSvc, InchargeIDSvc, ARTermSvc, Industry, MarketSegmentID, ContractGroup, CompanyGroupD, ServiceEmailCC, "
                    lsql = lsql + " SendServiceReportTo1, SendServiceReportTo2 "
                    lsql = lsql + "From tblCompanyLocation "
                    lsql = lsql + "where AccountID = '" & txtAccountID.Text.Trim & "' and LocationID = '" & lblLocationID1.Text.Trim & "'"


                    cmdServiceLocationDetails.CommandText = lsql
                    'cmdServiceLocationDetails.Parameters.AddWithValue("@AccountID", txtAccountID.Text)
                    'cmdServiceLocationDetails.Parameters.AddWithValue("@LocationId", lblLocationID1.Text)
                    cmdServiceLocationDetails.Connection = conn

                    Dim drServiceLocationDetails As MySqlDataReader = cmdServiceLocationDetails.ExecuteReader()
                    Dim dtServiceLocationDetails As New System.Data.DataTable
                    dtServiceLocationDetails.Load(drServiceLocationDetails)


                    If dtServiceLocationDetails.Rows.Count > 0 Then
                        ttxtSvcCP1Contact = dtServiceLocationDetails.Rows(0)("ContactPerson").ToString.Trim
                        ttxtAddress = dtServiceLocationDetails.Rows(0)("Address1").ToString.Trim
                        ttxtSvcCP1Telephone = dtServiceLocationDetails.Rows(0)("Telephone").ToString.Trim
                        ttxtSvcCP1Mobile = dtServiceLocationDetails.Rows(0)("Mobile").ToString.Trim
                        ttxtSvcCP1Email = dtServiceLocationDetails.Rows(0)("Email").ToString
                        ttxtBuilding = dtServiceLocationDetails.Rows(0)("AddBuilding").ToString.Trim
                        ttxtStreet = dtServiceLocationDetails.Rows(0)("AddStreet").ToString.Trim
                        tddlState = dtServiceLocationDetails.Rows(0)("AddState").ToString.Trim
                        tddlCity = dtServiceLocationDetails.Rows(0)("AddCity").ToString.Trim
                        tddlCountry = dtServiceLocationDetails.Rows(0)("AddCountry").ToString.Trim
                        ttxtPostal = dtServiceLocationDetails.Rows(0)("AddPostal").ToString.Trim
                        tddlLocateGrp = dtServiceLocationDetails.Rows(0)("LocateGRP").ToString.Trim
                        ttxtSvcCP1Fax = dtServiceLocationDetails.Rows(0)("Fax").ToString.Trim
                        ttxtLocationPrefix = dtServiceLocationDetails.Rows(0)("LocationPrefix").ToString.Trim
                        ttxtLocatonNo = dtServiceLocationDetails.Rows(0)("LocationNo").ToString.Trim
                        ttxtServiceName = dtServiceLocationDetails.Rows(0)("ServiceName").ToString.Trim
                        ttxtSvcCP1Position = dtServiceLocationDetails.Rows(0)("Contact1Position").ToString.Trim
                        ttxtSvcCP1Telephone2 = dtServiceLocationDetails.Rows(0)("Telephone2").ToString.Trim
                        ttxtSvcCP2Contact = dtServiceLocationDetails.Rows(0)("ContactPerson2").ToString.Trim
                        ttxtSvcCP2Position = dtServiceLocationDetails.Rows(0)("Contact2Position").ToString.Trim
                        ttxtSvcCP2Telephone = dtServiceLocationDetails.Rows(0)("Contact2Tel").ToString.Trim
                        ttxtSvcCP2Fax = dtServiceLocationDetails.Rows(0)("Contact2Fax").ToString.Trim
                        ttxtSvcCP2Tel2 = dtServiceLocationDetails.Rows(0)("Contact2Tel2").ToString.Trim
                        ttxtSvcCP2Mobile = dtServiceLocationDetails.Rows(0)("Contact2Mobile").ToString.Trim
                        ttxtSvcCP2Email = dtServiceLocationDetails.Rows(0)("Contact2Email").ToString.Trim
                        ttxtServiceLocationGroup = dtServiceLocationDetails.Rows(0)("ServiceLocationGroup").ToString.Trim

                        ttxtBillingNameSvc = dtServiceLocationDetails.Rows(0)("BillingNameSvc").ToString
                        ttxtBillAddressSvc = dtServiceLocationDetails.Rows(0)("BillAddressSvc").ToString
                        ttxtBillStreetSvc = dtServiceLocationDetails.Rows(0)("BillStreetSvc").ToString
                        ttxtBillBuildingSvc = dtServiceLocationDetails.Rows(0)("BillBuildingSvc").ToString
                        tddlBillCitySvc = dtServiceLocationDetails.Rows(0)("BillCitySvc").ToString
                        tddlBillStateSvc = dtServiceLocationDetails.Rows(0)("BillStateSvc").ToString
                        tddlBillCountrySvc = dtServiceLocationDetails.Rows(0)("BillCountrySvc").ToString
                        ttxtBillPostalSvc = dtServiceLocationDetails.Rows(0)("BillPostalSvc").ToString
                        ttxtBillContact1Svc = dtServiceLocationDetails.Rows(0)("BillContact1Svc").ToString
                        ttxtBillPosition1Svc = dtServiceLocationDetails.Rows(0)("BillPosition1Svc").ToString
                        ttxtBillTelephone1Svc = dtServiceLocationDetails.Rows(0)("BillTelephone1Svc").ToString
                        ttxtBillFax1Svc = dtServiceLocationDetails.Rows(0)("BillFax1Svc").ToString

                        ttxtBilltelephone12Svc = dtServiceLocationDetails.Rows(0)("Billtelephone12Svc").ToString
                        ttxtBillMobile1Svc = dtServiceLocationDetails.Rows(0)("BillMobile1Svc").ToString
                        ttxtBillEmail1Svc = dtServiceLocationDetails.Rows(0)("BillEmail1Svc").ToString
                        ttxtBillContact2Svc = dtServiceLocationDetails.Rows(0)("BillContact2Svc").ToString
                        ttxtBillPosition2Svc = dtServiceLocationDetails.Rows(0)("BillPosition2Svc").ToString
                        ttxtBillTelephone2Svc = dtServiceLocationDetails.Rows(0)("BillTelephone2Svc").ToString

                        ttxtBillFax2Svc = dtServiceLocationDetails.Rows(0)("BillFax2Svc").ToString.Trim
                        ttxtBilltelephone22Svc = dtServiceLocationDetails.Rows(0)("Billtelephone22Svc").ToString.Trim
                        ttxtBillMobile2Svc = dtServiceLocationDetails.Rows(0)("BillMobile2Svc").ToString.Trim
                        ttxtBillEmail2Svc = dtServiceLocationDetails.Rows(0)("BillEmail2Svc").ToString.Trim
                        tddlSalesManSvc = dtServiceLocationDetails.Rows(0)("SalesManSvc").ToString.Trim
                        tddlInchargeSvc = dtServiceLocationDetails.Rows(0)("InchargeIDSvc").ToString.Trim

                        tddlTermsSvc = dtServiceLocationDetails.Rows(0)("ARTermSvc").ToString.Trim
                        tddlIndustrysvc = dtServiceLocationDetails.Rows(0)("Industry").ToString.Trim
                        ttxtMarketSegmentIDsvc = dtServiceLocationDetails.Rows(0)("MarketSegmentId").ToString.Trim
                        tddlContractGrp = dtServiceLocationDetails.Rows(0)("ContractGroup").ToString.Trim
                        tddlCompanyGrpD = dtServiceLocationDetails.Rows(0)("CompanyGroupD").ToString.Trim
                        ttxtSvcEmailCC = dtServiceLocationDetails.Rows(0)("ServiceEmailCC").ToString.Trim
                        ttxtSendServiceReportTo1 = dtServiceLocationDetails.Rows(0)("SendServiceReportTo1").ToString.Trim
                        ttxtSendServiceReportTo2 = dtServiceLocationDetails.Rows(0)("SendServiceReportTo2").ToString.Trim
                    End If


                    ''
                    Dim lAddressContract As String = ""
                    Dim laddress1 As String = ""
                    Dim lIsOpenContarctExist As String = ""
                    Dim cmdIsOpenContarctExist As MySqlCommand = New MySqlCommand

                    cmdIsOpenContarctExist.CommandType = CommandType.Text

                    cmdIsOpenContarctExist.CommandText = "SELECT ContractNo, LocationId FROM tblContractDet where AccountId =@AccountID and  LocationId=@LocationId and ContractNo in (Select ContractNo from tblContract where Status = 'O' order by ContractNo)"
                    cmdIsOpenContarctExist.Parameters.AddWithValue("@AccountID", txtAccountID.Text)
                    cmdIsOpenContarctExist.Parameters.AddWithValue("@LocationId", lblLocationID1.Text)
                    cmdIsOpenContarctExist.Connection = conn

                    Dim drIsOpenContarctExist As MySqlDataReader = cmdIsOpenContarctExist.ExecuteReader()
                    Dim dtIsOpenContarctExist As New System.Data.DataTable
                    dtIsOpenContarctExist.Load(drIsOpenContarctExist)


                    If dtIsOpenContarctExist.Rows.Count > 0 Then
                        'lAddressContract = ""
                        lIsOpenContarctExist = ""
                        laddress1 = ""

                        If String.IsNullOrEmpty(ttxtAddress) = False Then
                            laddress1 = ttxtAddress.ToUpper
                        End If

                        If String.IsNullOrEmpty(ttxtStreet) = False Then
                            laddress1 = laddress1 & ", " & ttxtStreet.ToUpper
                        End If

                        If String.IsNullOrEmpty(ttxtBuilding) = False Then
                            laddress1 = laddress1 & ", " & ttxtBuilding.ToUpper
                        End If

                        'If ddlState.Text = txtDDLText.Text Then
                        '    commandSvc.Parameters.AddWithValue("@AddState", "")
                        'Else
                        '    commandSvc.Parameters.AddWithValue("@AddState", ddlState.Text.ToUpper)
                        'End If


                        If String.IsNullOrEmpty(tddlCity) = False Then
                            laddress1 = laddress1 & ", " & tddlCity.ToUpper
                        End If

                        'If ddlCountry.SelectedIndex > 0 Then
                        '    laddress1 = laddress1 & ", " & ddlCity.Text.ToUpper
                        'End If

                        If String.IsNullOrEmpty(ttxtPostal) = False Then
                            laddress1 = laddress1 & ", " & ttxtPostal.ToUpper
                        End If
                        lIsOpenContarctExist = dtIsOpenContarctExist.Rows(0)("ContractNo").ToString

                        For x As Integer = 0 To dtIsOpenContarctExist.Rows.Count - 1

                            '''''''''''''''''''''''''''''''''''''''''
                            'Dim lIsOpenContarctExistNew As String

                            'lIsOpenContarctExistNew = dtIsOpenContarctExist.Rows(0)("ContractNo").ToString

                            If lIsOpenContarctExist <> dtIsOpenContarctExist.Rows(x)("ContractNo").ToString Then
                                'If lIsOpenContarctExist <> lIsOpenContarctExistNew Then
                                'Dim commandUpdContractServiceAddress As MySqlCommand = New MySqlCommand
                                'commandUpdContractServiceAddress.CommandType = CommandType.Text
                                'commandUpdContractServiceAddress.CommandText = "Update tblContract set CustName = """ & txtServiceName.Text.ToUpper & """, ServiceAddress = '" & lAddressContract.Trim & "'  where ContractNo = '" & lIsOpenContarctExist.Trim & "'"
                                'commandUpdContractServiceAddress.Connection = conn
                                'commandUpdContractServiceAddress.ExecuteNonQuery()

                                'commandUpdContractServiceAddress.Dispose()

                                'UpdateContractHeader(lIsOpenContarctExist.Trim)
                                'UpdateContractHeaderProcessData(lIsOpenContarctExist.Trim, ttxtBillAddressSvc, ttxtBillStreetSvc, ttxtBillBuildingSvc, tddlBillCitySvc, tddlBillStateSvc, tddlBillCountrySvc, ttxtBillPostalSvc, ttxtServiceName)
                                'lAddressContract = ""
                                lIsOpenContarctExist = ""
                                lIsOpenContarctExist = dtIsOpenContarctExist.Rows(x)("ContractNo").ToString
                            End If


                            ''''''''''''''''''''''''''''''''''''
                            '''''''''''''''''''''''''''''''''''''''''''''
                            Dim cmdLoopContarctDet As MySqlCommand = New MySqlCommand

                            cmdLoopContarctDet.CommandType = CommandType.Text

                            cmdLoopContarctDet.CommandText = "SELECT AccountId, ContractNo, LocationId,Address1, Location FROM tblContractDet where ContractNo = @ContractNo  order by AccountID, LocationID"
                            cmdLoopContarctDet.Parameters.AddWithValue("@ContractNo", lIsOpenContarctExist)
                            cmdLoopContarctDet.Connection = conn

                            Dim drLoopContarctDet As MySqlDataReader = cmdLoopContarctDet.ExecuteReader()
                            Dim dtLoopContarctDet As New System.Data.DataTable
                            dtLoopContarctDet.Load(drLoopContarctDet)

                            If dtLoopContarctDet.Rows.Count > 0 Then

                                '''''''''''''''''''''''''''''''''
                                For y As Integer = 0 To dtLoopContarctDet.Rows.Count - 1
                                    'For Each row As DataRow In dtLoopContarctDet.Rows()
                                    'txtComments.Text = row("LocationID")

                                    If dtLoopContarctDet.Rows(y)("AccountId").ToString = txtAccountID.Text And dtLoopContarctDet.Rows(y)("LocationID").ToString = lblLocationID1.Text Then
                                        Dim commandContDet As MySqlCommand = New MySqlCommand

                                        commandContDet.CommandType = CommandType.Text
                                        Dim qryContDet As String
                                        qryContDet = ""

                                        '''''''''''''''''
                                        qryContDet = "Update tblContractDet SET  "
                                        qryContDet = qryContDet + "ContactPerson = @ContactPerson, "
                                        'qryContDet = qryContDet + "ServiceName = @ServiceName, "

                                        qryContDet = qryContDet + "Telephone = @Telephone, "
                                        qryContDet = qryContDet + "Mobile = @Mobile, "
                                        qryContDet = qryContDet + "Email = @Email, "
                                        'qryContDet = qryContDet + "LocateGrp = @LocateGrp, "
                                        qryContDet = qryContDet + "Fax = @Fax, "

                                        qryContDet = qryContDet + "Contact1Position = @Contact1Position, "
                                        qryContDet = qryContDet + "ContactPerson2 = @ContactPerson2, "
                                        qryContDet = qryContDet + "Contact2Position = @Contact2Position, "
                                        qryContDet = qryContDet + "Contact2Tel = @Contact2Tel, "
                                        qryContDet = qryContDet + "Contact2Fax = @Contact2Fax, "
                                        qryContDet = qryContDet + "Contact2Tel2 = @Contact2Tel2, "
                                        qryContDet = qryContDet + "Contact2Mobile = @Contact2Mobile, "
                                        qryContDet = qryContDet + "Contact2Email = @Contact2Email, "
                                        qryContDet = qryContDet + "Telephone2 = @Telephone2, "
                                        'qryContDet = qryContDet + "Address1 = @Address1, "
                                        'qryContDet = qryContDet + "Location = @Location, "
                                        qryContDet = qryContDet + "LastModifiedBy = @LastModifiedBy, "
                                        qryContDet = qryContDet + "LastModifiedOn = @LastModifiedOn "
                                        qryContDet = qryContDet + "where "
                                        qryContDet = qryContDet + "AccountID	=	@AccountID	and "
                                        qryContDet = qryContDet + "LocationID	=	@LocationID "

                                        '''''''''''''''''
                                        commandContDet.CommandText = qryContDet
                                        commandContDet.Parameters.Clear()

                                        commandContDet.Parameters.AddWithValue("@ContactPerson", ttxtSvcCP1Contact.ToUpper)
                                        'commandContDet.Parameters.AddWithValue("@ServiceName", ttxtServiceName.ToUpper)

                                        commandContDet.Parameters.AddWithValue("@Telephone", ttxtSvcCP1Telephone.ToUpper)
                                        commandContDet.Parameters.AddWithValue("@Mobile", ttxtSvcCP1Mobile.ToUpper)
                                        commandContDet.Parameters.AddWithValue("@Email", ttxtSvcCP1Email.ToUpper)

                                        'If tddlLocateGrp.Text = txtDDLText.Text Then
                                        '    commandContDet.Parameters.AddWithValue("@LocateGRP", "")
                                        'Else
                                        'commandContDet.Parameters.AddWithValue("@LocateGRP", tddlLocateGrp.ToUpper)
                                        'End If

                                        'If ddlLocateGrp.Text = txtDDLText.Text Then
                                        '    commandContDet.Parameters.AddWithValue("@LocateGRP", "")
                                        'Else
                                        '    commandContDet.Parameters.AddWithValue("@LocateGRP", tddlLocateGrp.ToUpper)
                                        'End If
                                        commandContDet.Parameters.AddWithValue("@Fax", ttxtSvcCP1Fax.ToUpper)

                                        commandContDet.Parameters.AddWithValue("@Contact1Position", ttxtSvcCP1Position.ToUpper)
                                        commandContDet.Parameters.AddWithValue("@ContactPerson2", ttxtSvcCP2Contact.ToUpper)
                                        commandContDet.Parameters.AddWithValue("@Contact2Position", ttxtSvcCP2Position.ToUpper)
                                        commandContDet.Parameters.AddWithValue("@Contact2Tel", ttxtSvcCP2Telephone.ToUpper)
                                        commandContDet.Parameters.AddWithValue("@Contact2Fax", ttxtSvcCP2Fax.ToUpper)
                                        commandContDet.Parameters.AddWithValue("@Contact2Tel2", ttxtSvcCP2Tel2.ToUpper)
                                        commandContDet.Parameters.AddWithValue("@Contact2Mobile", ttxtSvcCP2Mobile.ToUpper)
                                        commandContDet.Parameters.AddWithValue("@Contact2Email", ttxtSvcCP2Email.ToUpper)
                                        commandContDet.Parameters.AddWithValue("@Telephone2", ttxtSvcCP1Telephone2.ToUpper)
                                        'commandContDet.Parameters.AddWithValue("@Address1", laddress1)
                                        'commandContDet.Parameters.AddWithValue("@Location", lLocation)

                                        commandContDet.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                                        commandContDet.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                                        commandContDet.Parameters.AddWithValue("@AccountID", txtAccountID.Text.ToUpper)
                                        commandContDet.Parameters.AddWithValue("@LocationID", lblLocationID1.Text.ToUpper)

                                        commandContDet.Connection = conn
                                        commandContDet.ExecuteNonQuery()

                                        commandContDet.Dispose()
                                        'lAddressContract = lAddressContract & laddress1 & ";  "

                                    Else
                                        'lAddressContract = lAddressContract & dtLoopContarctDet.Rows(y)("Address1").ToString & ";  "

                                    End If

                                    ''''''''''''''''''''''''''''''
                                Next
                            End If

                            ''''''''''''''''''''''''''''''''''''''''''''''''
                        Next x
                    End If

                    'UpdateContractHeader(lIsOpenContarctExist.Trim)
                    'UpdateContractHeaderProcessData(lIsOpenContarctExist.Trim, ttxtBillAddressSvc, ttxtBillStreetSvc, ttxtBillBuildingSvc, tddlBillCitySvc, tddlBillStateSvc, tddlBillCountrySvc, ttxtBillPostalSvc, ttxtServiceName)

                    '''''''''''''' End :Update tblContractDet '''''''''''''''''''''''''''''''

                    ''''''''''''''' Start :Update tblServiceRecord '''''''''''''''''''''''''''''''

                    Dim qrySvc As String
                    qrySvc = ""

                    Dim commandSvc As MySqlCommand = New MySqlCommand

                    'commandSvc.CommandType = CommandType.Text
                    'commandSvc.CommandText = qrySvc

                    commandSvc.CommandType = CommandType.StoredProcedure
                    commandSvc.CommandText = "UpdateContactInfoFromCustomerMaster"

                    commandSvc.Parameters.Clear()

                    commandSvc.Parameters.AddWithValue("@pr_ContactPerson", ttxtSvcCP1Contact.ToUpper)
                    'commandSvc.Parameters.AddWithValue("@pr_Address1", ttxtAddress.ToUpper)
                    'commandSvc.Parameters.AddWithValue("@pr_CustAddress1", ttxtAddress.ToUpper)
                    commandSvc.Parameters.AddWithValue("@pr_Telephone", ttxtSvcCP1Telephone.ToUpper)
                    commandSvc.Parameters.AddWithValue("@pr_Mobile", ttxtSvcCP1Mobile.ToUpper)
                    commandSvc.Parameters.AddWithValue("@pr_Email", ttxtSvcCP1Email.ToUpper)

                    'commandSvc.Parameters.AddWithValue("@pr_AddBuilding", ttxtBuilding.ToUpper)
                    'commandSvc.Parameters.AddWithValue("@pr_AddStreet", ttxtStreet.ToUpper)
                    'If ddlState.Text = txtDDLText.Text Then
                    '    commandSvc.Parameters.AddWithValue("@pr_AddState", "")
                    'Else
                    '    commandSvc.Parameters.AddWithValue("@pr_AddState", tddlState.ToUpper)
                    'End If

                    'If ddlCity.Text = txtDDLText.Text Then
                    '    commandSvc.Parameters.AddWithValue("@pr_AddCity", "")
                    'Else
                    '    commandSvc.Parameters.AddWithValue("@pr_AddCity", tddlCity.ToUpper)
                    'End If
                    'If ddlCountry.Text = txtDDLText.Text Then
                    '    commandSvc.Parameters.AddWithValue("@pr_AddCountry", "")
                    'Else
                    '    commandSvc.Parameters.AddWithValue("@pr_AddCountry", tddlCountry.ToUpper)
                    'End If
                    'commandSvc.Parameters.AddWithValue("@pr_AddPostal", ttxtPostal.ToUpper)
                    'If ddlLocateGrp.Text = txtDDLText.Text Then
                    '    commandSvc.Parameters.AddWithValue("@pr_LocateGRP", "")
                    'Else
                    '    commandSvc.Parameters.AddWithValue("@pr_LocateGRP", tddlLocateGrp.ToUpper)
                    'End If
                    commandSvc.Parameters.AddWithValue("@pr_Fax", ttxtSvcCP1Fax.ToUpper)
                    'commandSvc.Parameters.AddWithValue("@pr_ServiceName", ttxtServiceName.ToUpper)
                    commandSvc.Parameters.AddWithValue("@pr_Contact1Position", ttxtSvcCP1Position.ToUpper)
                    commandSvc.Parameters.AddWithValue("@pr_ContactPerson2", ttxtSvcCP2Contact.ToUpper)
                    commandSvc.Parameters.AddWithValue("@pr_Contact2Position", ttxtSvcCP2Position.ToUpper)
                    commandSvc.Parameters.AddWithValue("@pr_Contact2Tel", ttxtSvcCP2Telephone.ToUpper)
                    commandSvc.Parameters.AddWithValue("@pr_Contact2Fax", ttxtSvcCP2Fax.ToUpper)
                    commandSvc.Parameters.AddWithValue("@pr_Contact2Tel2", ttxtSvcCP2Tel2.ToUpper)
                    commandSvc.Parameters.AddWithValue("@pr_Contact2Mobile", ttxtSvcCP2Mobile.ToUpper)
                    commandSvc.Parameters.AddWithValue("@pr_Contact2Email", ttxtSvcCP2Email.ToUpper)
                    commandSvc.Parameters.AddWithValue("@pr_Telephone2", ttxtSvcCP1Telephone2.ToUpper)

                    Dim lOtherEmail As String
                    lOtherEmail = ""

                    'If lSendServiceReportTo1Loc = "Y" Then
                    If ttxtSendServiceReportTo1 = "Y" Then
                        lOtherEmail = ttxtBillEmail1Svc.Trim
                    End If

                    'End If


                    If String.IsNullOrEmpty(lOtherEmail.Trim) = False Then
                        lOtherEmail = lOtherEmail.Trim() & ";" & txtBillEmail2Svc.Text.Trim()
                    Else
                        lOtherEmail = txtBillEmail2Svc.Text.Trim()
                    End If

                    If ttxtSendServiceReportTo2 = "Y" Then
                        If String.IsNullOrEmpty(ttxtBillEmail2Svc.Trim()) = False Then
                            If String.IsNullOrEmpty(lOtherEmail.Trim) = True Then
                                lOtherEmail = ttxtBillEmail2Svc.ToUpper.Trim()
                            Else
                                lOtherEmail = lOtherEmail.Trim() & ";" & ttxtBillEmail2Svc.ToUpper.Trim()
                            End If
                        End If
                    End If


                    If String.IsNullOrEmpty(ttxtSvcEmailCC.Trim()) = False Then
                        If String.IsNullOrEmpty(lOtherEmail) = True Then
                            lOtherEmail = ttxtSvcEmailCC.ToUpper.Trim()
                        Else
                            lOtherEmail = lOtherEmail.Trim() & ";" & ttxtSvcEmailCC.ToUpper.Trim()
                        End If
                    End If

                    'End If
                    commandSvc.Parameters.AddWithValue("@pr_OtherEmail", Left(lOtherEmail.ToUpper.Trim, 500))
                    commandSvc.Parameters.AddWithValue("@pr_LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                    commandSvc.Parameters.AddWithValue("@pr_LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                    commandSvc.Parameters.AddWithValue("@pr_AccountID", txtAccountID.Text.ToUpper)
                    commandSvc.Parameters.AddWithValue("@pr_LocationID", lblLocationID1.Text.ToUpper)
                    commandSvc.Parameters.AddWithValue("@pr_Location", lLocation.ToUpper)
                    commandSvc.Connection = conn
                    commandSvc.ExecuteScalar()

                    commandSvc.Dispose()
                    totrecsUpdated = totrecsUpdated + 1
                End If

            Next


            '''''''''''''' End :Update tblServiceRecord '''''''''''''''''''''''''''''''

            '' ''''''''''''''' Start :Update tblInvoice '''''''''''''''''''''''''''''''

            'Dim commandSales As MySqlCommand = New MySqlCommand

            'commandSales.CommandType = CommandType.Text
            'Dim qrySales As String
            'qrySales = ""

            ' '''''''''''''''''
            'qrySales = "Update tblSales SET  "
            'qrySales = qrySales + "CustAttention = @ContactPerson, "
            'qrySales = qrySales + "CustAddress1 = @Address1, "
            'qrySales = qrySales + "CustAddBuilding = @AddBuilding, "
            'qrySales = qrySales + "CustAddStreet = @AddStreet, "
            'qrySales = qrySales + "CustAddCountry = @AddCountry, "
            'qrySales = qrySales + "CustAddPostal = @AddPostal, "
            'qrySales = qrySales + "ContactPersonMobile = @Mobile, "
            'qrySales = qrySales + "LastModifiedBy = @LastModifiedBy, "
            'qrySales = qrySales + "LastModifiedOn = @LastModifiedOn "
            'qrySales = qrySales + "where "
            'qrySales = qrySales + "AccountID	=	@AccountID	and "
            'qrySales = qrySales + "CommpanyGroup	=	@CommpanyGroup	and "
            'qrySales = qrySales + "PostStatus = 'O'"

            ' '''''''''''''''''
            'commandSales.CommandText = qrySales
            'commandSales.Parameters.Clear()

            'commandSales.Parameters.AddWithValue("@ContactPerson", txtSvcCP1Contact.Text.ToUpper)
            'commandSales.Parameters.AddWithValue("@Address1", txtAddress.Text.ToUpper)
            'commandSales.Parameters.AddWithValue("@AddBuilding", txtBuilding.Text.ToUpper)
            'commandSales.Parameters.AddWithValue("@AddStreet", txtStreet.Text.ToUpper)
            ''If ddlState.Text = txtDDLText.Text Then
            ''    commandSales.Parameters.AddWithValue("@AddState", "")
            ''Else
            ''    commandSales.Parameters.AddWithValue("@AddState", ddlState.Text.ToUpper)
            ''End If

            ''If ddlCity.Text = txtDDLText.Text Then
            ''    commandSales.Parameters.AddWithValue("@AddCity", "")
            ''Else
            ''    commandSales.Parameters.AddWithValue("@AddCity", ddlCity.Text.ToUpper)
            ''End If
            'If ddlCountry.Text = txtDDLText.Text Then
            '    commandSales.Parameters.AddWithValue("@AddCountry", "")
            'Else
            '    commandSales.Parameters.AddWithValue("@AddCountry", ddlCountry.Text.ToUpper)
            'End If
            'commandSales.Parameters.AddWithValue("@AddPostal", txtPostal.Text.ToUpper)

            'commandSales.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
            'commandSales.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))
            'commandSales.Parameters.AddWithValue("@AccountID", txtAccountID.Text.ToUpper)
            'commandSales.Parameters.AddWithValue("@CommpanyGroup", ddlCompanyGrpD.Text.ToUpper)
            'commandSales.Connection = conn
            'commandSales.ExecuteNonQuery()

            'commandSales.Dispose()

            ' '''''''''''''''' End : Update tblInvoice '''''''''''''''''''''''''''''''

            '''''''''''''''''' END:UPDATE SERVICE LOCATION '''''''''''''''''''''''''''''''''

            lblMessage.Text = "TOTAL RECORDS UPDATED : " & totrecsUpdated
            CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CORP", txtLocationID.Text, "EDIT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")), 0, 0, 0, txtAccountID.Text, "", txtRcno.Text)

            lblAlert.Text = ""
            txtConfirmationCode.Text = ""

            'End If

            conn.Close()
            conn.Dispose()

            'commandSvc.Dispose()
            'cmdIsOpenContarctExist.Dispose()

            'command1.Dispose()
            'command2.Dispose()
            'dt.Dispose()
            'dt1.Dispose()
            'dr.Close()
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + Session("UserID"), "ProcessUpdate", ex.Message.ToString, "")
            lblAlert.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnEditServiceContactSaveYes_Click(sender As Object, e As EventArgs) Handles btnEditServiceContactSaveYes.Click
        Try
            lblWarningAlert.Text = ""

            If String.IsNullOrEmpty(txtConfirmationCode.Text.Trim) = True Then
                lblWarningAlert.Text = "Please Enter Confirmation Code"
                mdlWarning.Show()
                Exit Sub
            End If

            If txtConfirmationCode.Text.Trim <> lblRandom.Text.Trim Then
                lblWarningAlert.Text = "Confirmation Code does not match"
                mdlWarning.Show()
                Exit Sub
            End If

            ProcessUpdate()
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + Session("UserID"), "btnEditServiceContactSaveYes_Click", ex.Message.ToString, "")
            lblAlert.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnEditServiceContactSaveNo_Click(sender As Object, e As EventArgs) Handles btnEditServiceContactSaveNo.Click
        txtConfirmationCode.Text = ""
    End Sub

    Protected Sub chkAll_CheckedChanged(sender As Object, e As EventArgs)
        'Select All Check Boxes
        Try
            lblAlert.Text = ""
            lblMessage.Text = ""
            'UpdatePanel1.Update()

            For Each row As GridViewRow In grvServiceRecDetails.Rows
                Dim chkSelectCtrl As CheckBox = DirectCast(row.FindControl("chkGrid"), CheckBox)
                If chkSelectCtrl.Checked = False Then
                    chkSelectCtrl.Checked = True
                Else
                    chkSelectCtrl.Checked = False
                End If
            Next
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + Session("UserID"), "chkAll_CheckedChanged", ex.Message.ToString, "")
            lblAlert.Text = ex.Message.ToString
        End Try
    End Sub


    Private Sub UpdateContractHeaderProcessData(lContractNo As String, ttxtBillAddressSvc As String, ttxtBillStreetSvc As String, ttxtBillBuildingSvc As String, tddlBillCitySvc As String, tddlBillStateSvc As String, tddlBillCountrySvc As String, ttxtBillPostalSvc As String, ttxtServiceName As String)
        Try
            Dim conn1 As MySqlConnection = New MySqlConnection()

            conn1.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn1.Open()


            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text
            command1.CommandText = "SELECT LocationId, Address1 FROM tblcontractdet where ContractNo ='" & lContractNo & "' order by LocationId"
            command1.Connection = conn1

            Dim drservicecontractDet As MySqlDataReader = command1.ExecuteReader()
            Dim dtservicecontractDet As New DataTable
            dtservicecontractDet.Load(drservicecontractDet)


            Dim lServiceAddressCons As String = "", lLocationId As String = ""

            lLocationId = ""

            lServiceAddressCons = ""

            For i = 0 To dtservicecontractDet.Rows.Count - 1

                If lLocationId <> dtservicecontractDet.Rows(i)("LocationId").ToString() Then

                    If i = 0 Then
                        lServiceAddressCons = lServiceAddressCons & dtservicecontractDet.Rows(i)("Address1").ToString()
                    Else
                        lServiceAddressCons = lServiceAddressCons & ";  " & vbNewLine & dtservicecontractDet.Rows(i)("Address1").ToString()
                    End If
                    lLocationId = dtservicecontractDet.Rows(i)("LocationId").ToString()
                End If

            Next i

            Dim lBillAddressCons As String
            lBillAddressCons = ""

            If String.IsNullOrEmpty(ttxtBillAddressSvc.Trim) = False Then
                lBillAddressCons = ttxtBillAddressSvc.Trim.ToUpper
            End If

            If String.IsNullOrEmpty(ttxtBillStreetSvc.Trim) = False Then
                lBillAddressCons = lBillAddressCons.Trim + ", " + ttxtBillStreetSvc.Trim.ToUpper
            End If

            If String.IsNullOrEmpty(ttxtBillBuildingSvc.Trim) = False Then
                lBillAddressCons = lBillAddressCons.Trim + ", " + ttxtBillBuildingSvc.Trim.ToUpper
            End If


            If String.IsNullOrEmpty(tddlBillCountrySvc.Trim) = False Then
                lBillAddressCons = lBillAddressCons.Trim + ", " + tddlBillCountrySvc.Trim.ToUpper
            End If

            If String.IsNullOrEmpty(ttxtBillPostalSvc.Trim) = False Then
                lBillAddressCons = lBillAddressCons.Trim + ", " + ttxtBillPostalSvc.Trim
            End If

            Dim lLocation As String

            If ddlBranch.SelectedIndex = 0 Then
                lLocation = ""
            Else
                lLocation = ddlBranch.Text
            End If
            Dim commandUpdContractServiceAddress1 As MySqlCommand = New MySqlCommand
            commandUpdContractServiceAddress1.CommandType = CommandType.Text
            'commandUpdContractServiceAddress1.CommandText = "Update tblContract set CustName = """ & txtServiceName.Text.ToUpper & """, ServiceAddress = """ & lServiceAddressCons & """  where ContractNo = '" & lContractNo.Trim & "'"
            'commandUpdContractServiceAddress1.CommandText = "Update tblContract set Location = '" & lLocation & "',   CustName = """ & txtServiceName.Text.ToUpper & """, ServiceAddress = """ & lServiceAddressCons & """, BillAddress1 = """ & txtBillAddressSvc.Text.ToUpper & ", " & txtBillStreetSvc.Text.ToUpper & ", " & txtBillBuildingSvc.Text.ToUpper & ", " & ddlBillCountrySvc.Text.ToUpper & ", " & txtBillPostalSvc.Text & """  where ContractNo = '" & lContractNo.Trim & "'"
            commandUpdContractServiceAddress1.CommandText = "Update tblContract set Location = '" & lLocation & "',   CustName = """ & ttxtServiceName.ToUpper & """, ServiceAddress = """ & lServiceAddressCons & """, CustAddr = """ & lBillAddressCons.Trim & """ , BillAddress1 = """ & lBillAddressCons.Trim & """  where ContractNo = '" & lContractNo.Trim & "'"

            commandUpdContractServiceAddress1.Connection = conn1
            commandUpdContractServiceAddress1.ExecuteNonQuery()

            commandUpdContractServiceAddress1.Dispose()
            conn1.Close()


            conn1.Close()
            conn1.Dispose()
            'conn.Close()
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("COMPANY - " + Session("UserID"), "FUNCTION UpdateContractHeader", ex.Message.ToString, txtLocationID.Text)
            Exit Sub

        End Try
    End Sub

    Protected Sub chkDefaultContactInfo_CheckedChanged(sender As Object, e As EventArgs) Handles chkDefaultContactInfo.CheckedChanged
        txtSvcCP1ContactUpdateContactInformation.Text = txtOffContactPerson.Text
        txtSvcCP1PositionUpdateContactInformation.Text = txtOffPosition.Text
        txtSvcCP1TelephoneUpdateContactInformation.Text = txtOffContactNo.Text
        txtSvcCP1FaxUpdateContactInformation.Text = txtOffFax.Text
        txtSvcCP1Telephone2UpdateContactInformation.Text = txtOffContact2.Text
        txtSvcCP1MobileUpdateContactInformation.Text = txtOffMobile.Text
        txtSvcCP1EmailUpdateContactInformation.Text = txtOffEmail.Text
        txtSvcCP2ContactUpdateContactInformation.Text = txtOffCont1Name.Text
        txtSvcCP2PositionUpdateContactInformation.Text = txtOffCont1Position.Text
        txtSvcCP2TelephoneUpdateContactInformation.Text = txtOffCont1Tel.Text
        txtSvcCP2FaxUpdateContactInformation.Text = txtOffCont1Fax.Text
        txtSvcCP2Tel2UpdateContactInformation.Text = txtOffCont1Tel2.Text
        txtSvcCP2MobileUpdateContactInformation.Text = txtOffCont1Mobile.Text
        txtSvcCP2EmailUpdateContactInformation.Text = txtOffCont1Email.Text
        mdlUpdateServiceContact.Show()
        'txtSvcEmailCCUpdateContactInformation


    End Sub

    Protected Sub ddlLocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLocation.SelectedIndexChanged


        Try
            If String.IsNullOrEmpty(txtNameE.Text) = False Then

                Dim command As MySqlCommand = New MySqlCommand
                Dim sqlstr As String
                Dim conn As MySqlConnection = New MySqlConnection()
                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                If txtDisplayRecordsLocationwise.Text = "Y" Then
                    sqlstr = "Select Name from tblCompany where Name = """ & txtNameE.Text.Trim & """ and location = '" & ddlLocation.Text.Trim & "'"
                Else
                    sqlstr = "Select Name from tblCompany where Name = """ & txtNameE.Text.Trim & """"
                End If

                command.CommandType = CommandType.Text
                command.CommandText = sqlstr
                command.Connection = conn

                Dim dr As MySqlDataReader = command.ExecuteReader()
                Dim dt As New System.Data.DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    Dim message As String = "alert('Customer Already Exists..')"

                    ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)
                    'MessageBox.Message.Alert(Page, "Company Already Exists..", "str")

                End If
                conn.Close()
                conn.Dispose()
                command.Dispose()
                dt.Dispose()
                dr.Close()


            End If
            'txtBillingName.Text = txtNameE.Text.Trim
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "ddlLocation_SelectedIndexChanged", ex.Message.ToString, txtNameE.Text)
        End Try


    End Sub

    Protected Sub btnEditSalesman_Click(sender As Object, e As ImageClickEventArgs) Handles btnEditSalesman.Click
        mdlImportServices.Show()
    End Sub

    Protected Sub btnShowRecords_Click(sender As Object, e As EventArgs) Handles btnShowRecords.Click
        'Try
        '    lblAlertEditSalesman.Text = ""
        '    If String.IsNullOrEmpty(txtEffectiveDate.Text) = True Then

        '        lblAlertEditSalesman.Text = "Please Enter 'Effective Date'"
        '        txtEffectiveDate.Focus()
        '        mdlImportServices.Show()
        '        Exit Sub
        '    End If

        '    If ddlNewSalesman.SelectedIndex = 0 Then
        '        lblAlertEditSalesman.Text = "Please Select 'New Salesman'"
        '        ddlNewSalesman.Focus()
        '        mdlImportServices.Show()
        '        Exit Sub
        '    End If

        '    Dim str1 As String = ""
        '    Dim str2 As String = ""

        '    str1 = "`tbweditsalesmancontract_" & Session("UserID") & "`"
        '    str2 = "`tbweditsalesmaninvoice_" & Session("UserID") & "`"

        '    str1 = str1.Replace(".", "")
        '    str2 = str2.Replace(".", "")

        '    '''

        '    Dim conn As MySqlConnection = New MySqlConnection()

        '    conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        '    conn.Open()
        '    Dim commandSalesman As MySqlCommand = New MySqlCommand

        '    'commandSvc.CommandType = CommandType.Text
        '    'commandSvc.CommandText = qrySvc

        '    commandSalesman.CommandType = CommandType.StoredProcedure
        '    commandSalesman.CommandText = "GetCustomersPageWise"

        '    commandSalesman.Parameters.Clear()

        '    commandSalesman.Parameters.AddWithValue("pr_PageIndex", 1)
        '    commandSalesman.Parameters.AddWithValue("pr_PageSize", 20)
        '    commandSalesman.Parameters.AddWithValue("pr_Salesman", txtOldSalesman.Text.Trim)
        '    commandSalesman.Parameters.AddWithValue("pr_UserId", txtCreatedBy.Text.Trim)
        '    'commandSalesman.Parameters.AddWithValue("pr_UserId", txtCreatedBy.Text.Trim)
        '    commandSalesman.Parameters.AddWithValue("pr_tbweditsalesmancontract", str1)
        '    commandSalesman.Parameters.AddWithValue("pr_tbweditsalesmaninvoice", str2)
        '    commandSalesman.Parameters.AddWithValue("pr_EffectiveDate", Convert.ToDateTime(txtEffectiveDate.Text).ToString("yyyy-MM-dd"))
        '    commandSalesman.Parameters.Add("pr_PageCount", SqlDbType.Int, 4).Direction = ParameterDirection.Output


        '    commandSalesman.Connection = conn
        '    commandSalesman.ExecuteScalar()

        '    commandSalesman.Dispose()

        '    '''

        '    Dim sqlstrC, sqlstrI As String
        '    sqlstrC = "Select Rcno, RcnoContract,  Location, ContractNo, Salesman, StartDate, EndDate, Duration, AgreeValue from " & str1 & " order by StartDate, ContractNo limit 50"

        '    SqlDSContratESM.SelectCommand = sqlstrC
        '    SqlDSContratESM.DataBind()

        '    GridViewContractESM.DataSourceID = "SqlDSContratESM"
        '    GridViewContractESM.DataBind()

        '    'GridViewContractESM.DataSource = GetImagesPageWise(1, 10, txtOldSalesman.Text.Trim, txtCreatedBy.Text)
        '    'GridViewContractESM.DataBind()

        '    'sqlstrI = "Select Rcno, Location, InvoiceNumber, StaffCode, SalesDate, Terms, DueDate from tbweditsalesmaninvoice where UserId ='" & txtCreatedBy.Text.Trim & "'"
        '    sqlstrI = "Select Rcno, RcnoInvoice, Location, InvoiceNumber, StaffCode, SalesDate, Terms, DueDate from " & str2 & "  order by SalesDate, InvoiceNumber limit 50"

        '    SqlDSInvoiceESM.SelectCommand = sqlstrI
        '    SqlDSInvoiceESM.DataBind()

        '    GridViewInvoiceESM.DataSourceID = "SqlDSInvoiceESM"
        '    GridViewInvoiceESM.DataBind()

        '    Label42.Text = "CONTRACTS TO BE UPDATED : " & GridViewContractESM.Rows.Count.ToString
        '    Label41.Text = "INVOICES TO BE UPDATED : " & GridViewInvoiceESM.Rows.Count.ToString

        '    'GridViewInvoiceESM.DataSource = GetImagesPageWise(1, 10, txtOldSalesman.Text.Trim, txtCreatedBy.Text)
        '    'GridViewInvoiceESM.DataBind()

        '    conn.Close()
        '    conn.Dispose()
        '    mdlImportServices.Show()
        'Catch ex As Exception
        '    InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "btnShowRecords_Click", ex.Message.ToString, txtNameE.Text)
        '    lblAlertEditSalesman.Text = ex.Message.ToString
        '    mdlImportServices.Show()
        'End Try

        Try
            lblAlertEditSalesman.Text = ""
            If String.IsNullOrEmpty(txtEffectiveDate.Text) = True Then

                lblAlertEditSalesman.Text = "Please Enter 'Effective Date'"
                txtEffectiveDate.Focus()
                mdlImportServices.Show()
                Exit Sub
            End If

            If ddlNewSalesman.SelectedIndex = 0 Then
                lblAlertEditSalesman.Text = "Please Select 'New Salesman'"
                ddlNewSalesman.Focus()
                mdlImportServices.Show()
                Exit Sub
            End If

            Dim str1 As String = ""
            Dim str2 As String = ""

            str1 = "`tbweditsalesmancontract_" & Session("UserID") & "`"
            str2 = "`tbweditsalesmaninvoice_" & Session("UserID") & "`"

            str1 = str1.Replace(".", "")
            str2 = str2.Replace(".", "")

            '''

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()
            Dim commandSalesman As MySqlCommand = New MySqlCommand

            'commandSvc.CommandType = CommandType.Text
            'commandSvc.CommandText = qrySvc

            commandSalesman.CommandType = CommandType.StoredProcedure
            commandSalesman.CommandText = "GetCustomersPageWise"

            commandSalesman.Parameters.Clear()

            commandSalesman.Parameters.AddWithValue("pr_PageIndex", 1)
            commandSalesman.Parameters.AddWithValue("pr_PageSize", 20)
            commandSalesman.Parameters.AddWithValue("pr_LocationId", txtLocationIDEditSalesman.Text.Trim)
            commandSalesman.Parameters.AddWithValue("pr_Salesman", txtOldSalesman.Text.Trim)
            commandSalesman.Parameters.AddWithValue("pr_UserId", txtCreatedBy.Text.Trim)
            'commandSalesman.Parameters.AddWithValue("pr_UserId", txtCreatedBy.Text.Trim)
            commandSalesman.Parameters.AddWithValue("pr_tbweditsalesmancontract", str1)
            commandSalesman.Parameters.AddWithValue("pr_tbweditsalesmaninvoice", str2)
            commandSalesman.Parameters.AddWithValue("pr_EffectiveDate", Convert.ToDateTime(txtEffectiveDate.Text).ToString("yyyy-MM-dd"))
            commandSalesman.Parameters.Add("pr_PageCount", SqlDbType.Int, 4).Direction = ParameterDirection.Output


            commandSalesman.Connection = conn
            commandSalesman.ExecuteScalar()

            commandSalesman.Dispose()

            '''

            Dim sqlstrC, sqlstrI As String
            sqlstrC = "Select Rcno, RcnoContract,  Location, ContractNo, Salesman, StartDate, EndDate, Duration, AgreeValue, ActualEnd from " & str1 & " order by StartDate, ContractNo limit 50"

            SqlDSContratESM.SelectCommand = sqlstrC
            SqlDSContratESM.DataBind()

            GridViewContractESM.DataSourceID = "SqlDSContratESM"
            GridViewContractESM.DataBind()

            'GridViewContractESM.DataSource = GetImagesPageWise(1, 10, txtOldSalesman.Text.Trim, txtCreatedBy.Text)
            'GridViewContractESM.DataBind()

            'sqlstrI = "Select Rcno, Location, InvoiceNumber, StaffCode, SalesDate, Terms, DueDate from tbweditsalesmaninvoice where UserId ='" & txtCreatedBy.Text.Trim & "'"
            sqlstrI = "Select Rcno, RcnoInvoice, Location, InvoiceNumber, StaffCode, SalesDate, Terms, DueDate from " & str2 & "  order by SalesDate, InvoiceNumber limit 50"

            SqlDSInvoiceESM.SelectCommand = sqlstrI
            SqlDSInvoiceESM.DataBind()

            GridViewInvoiceESM.DataSourceID = "SqlDSInvoiceESM"
            GridViewInvoiceESM.DataBind()

            Label42.Text = "CONTRACTS TO BE UPDATED : " & GridViewContractESM.Rows.Count.ToString
            Label41.Text = "INVOICES TO BE UPDATED : " & GridViewInvoiceESM.Rows.Count.ToString

            'GridViewInvoiceESM.DataSource = GetImagesPageWise(1, 10, txtOldSalesman.Text.Trim, txtCreatedBy.Text)
            'GridViewInvoiceESM.DataBind()

            conn.Close()
            conn.Dispose()
            mdlImportServices.Show()
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "btnShowRecords_Click", ex.Message.ToString, txtNameE.Text)
            lblAlertEditSalesman.Text = ex.Message.ToString
            mdlImportServices.Show()
        End Try
    End Sub


    'Public Shared Function GetImagesPageWise(ByVal pageIndex As Integer, ByVal pageSize As Integer, ByVal oldsalesman As String, ByVal userid As String) As DataSet

    '    Dim conString As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

    '    Using con As New MySqlConnection(conString)
    '        Using cmd As New MySqlCommand("GetCustomersPageWise")
    '            cmd.CommandType = CommandType.StoredProcedure
    '            cmd.Parameters.AddWithValue("pr_PageIndex", pageIndex)
    '            cmd.Parameters.AddWithValue("pr_PageSize", pageSize)
    '            cmd.Parameters.AddWithValue("pr_Salesman", oldsalesman)
    '            cmd.Parameters.AddWithValue("pr_UserId", userid)
    '            cmd.Parameters.Add("pr_PageCount", SqlDbType.Int, 4).Direction = ParameterDirection.Output
    '            Using sda As New MySqlDataAdapter()
    '                cmd.Connection = con
    '                sda.SelectCommand = cmd
    '                Using ds As New DataSet()
    '                    sda.Fill(ds, "customer")
    '                    Dim dt As New DataTable("PageCount")
    '                    dt.Columns.Add("PageCount")
    '                    dt.Rows.Add()
    '                    dt.Rows(0)(0) = cmd.Parameters("pr_PageCount").Value
    '                    ds.Tables.Add(dt)
    '                    Return ds
    '                End Using
    '            End Using
    '        End Using
    '    End Using
    'End Function

    '<WebMethod>
    'Public Shared Function GetImages(ByVal pageIndex As Integer, ByVal oldsalesman As String, ByVal userid As String) As String
    '    'Added to similate delay so that we see the loader working
    '    'Must be removed when moving to production
    '    'System.Threading.Thread.Sleep(2000)

    '    Return GetImagesPageWise(pageIndex, 10, oldsalesman, userid).GetXml()
    'End Function


    'Public Shared Function GetImagesPageWiseInvoice(ByVal pageIndex As Integer, ByVal pageSize As Integer, ByVal oldsalesman As String, ByVal userid As String) As DataSet

    '    Dim conString As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

    '    Using con As New MySqlConnection(conString)
    '        Using cmd As New MySqlCommand("GetCustomersPageWise")
    '            cmd.CommandType = CommandType.StoredProcedure
    '            cmd.Parameters.AddWithValue("pr_PageIndex", pageIndex)
    '            cmd.Parameters.AddWithValue("pr_PageSize", pageSize)
    '            cmd.Parameters.AddWithValue("pr_Salesman", oldsalesman)
    '            cmd.Parameters.AddWithValue("pr_UserId", userid)
    '            cmd.Parameters.Add("pr_PageCount", SqlDbType.Int, 4).Direction = ParameterDirection.Output
    '            Using sda As New MySqlDataAdapter()
    '                cmd.Connection = con
    '                sda.SelectCommand = cmd
    '                Using ds As New DataSet()
    '                    sda.Fill(ds, "customer")
    '                    Dim dt As New DataTable("PageCount")
    '                    dt.Columns.Add("PageCount")
    '                    dt.Rows.Add()
    '                    dt.Rows(0)(0) = cmd.Parameters("pr_PageCount").Value
    '                    ds.Tables.Add(dt)
    '                    Return ds
    '                End Using
    '            End Using
    '        End Using
    '    End Using
    'End Function

    '<WebMethod>
    'Public Shared Function GetImagesInvoice(ByVal pageIndex As Integer, ByVal oldsalesman As String, ByVal userid As String) As String
    '    'Added to similate delay so that we see the loader working
    '    'Must be removed when moving to production
    '    'System.Threading.Thread.Sleep(2000)

    '    Return GetImagesPageWise(pageIndex, 10, oldsalesman, userid).GetXml()
    'End Function

    Protected Sub btnUpdateSalesman_Click(sender As Object, e As EventArgs) Handles btnUpdateSalesman.Click

        Try

            lblAlertEditSalesman.Text = ""


            Dim totalRowsC, totalRowsI As Long
            totalRowsC = 0
            totalRowsC = 0

            For rowIndex1 As Integer = 0 To GridViewContractESM.Rows.Count - 1
                Dim TextBoxchkSelectC As CheckBox = CType(GridViewContractESM.Rows(rowIndex1).Cells(0).FindControl("chkSelectContractESMGV"), CheckBox)
                If (TextBoxchkSelectC.Checked = True) Then
                    totalRowsC = totalRowsC + 1
                    GoTo insertRec2
                End If
            Next rowIndex1
            'End If


            If totalRowsC = 0 Then
                'mdlImportServices.Show()

                For rowIndex1 As Integer = 0 To GridViewInvoiceESM.Rows.Count - 1
                    Dim TextBoxchkSelectI As CheckBox = CType(GridViewInvoiceESM.Rows(rowIndex1).Cells(0).FindControl("chkSelectInvoiceESMGV"), CheckBox)
                    If (TextBoxchkSelectI.Checked = True) Then
                        totalRowsI = totalRowsI + 1
                        GoTo insertRec2
                    End If
                Next rowIndex1

                lblAlertEditSalesman.Text = "PLEASE SELECT A RECORD"
                ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", Message, True)
                mdlImportServices.Show()
                Exit Sub
            End If



insertRec2:
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            'If conn.State = ConnectionState.Open Then
            '    conn.Close()
            'End If
            conn.Open()
            'Dim sql As String
            'sql = ""

            Dim rowselected As Integer
            'rowselected = 0

            rowselected = 0

            rowselected = GridViewContractESM.Rows.Count - 1 '26.10.17

            For rowIndex As Integer = 0 To GridViewContractESM.Rows.Count - 1
                Dim TextBoxchkSelect As CheckBox = CType(GridViewContractESM.Rows(rowIndex).Cells(0).FindControl("chkSelectContractESMGV"), CheckBox)

                If (TextBoxchkSelect.Checked = True) Then

                    Dim qry As String
                    qry = ""

                    Dim command As MySqlCommand = New MySqlCommand
                    command.CommandType = CommandType.Text

                    rowselected = rowselected + 1

                    'Dim lblid1 As TextBox = CType(GridViewContractESM.Rows(rowIndex).Cells(0).FindControl("txtRcnoContractCESMGV"), TextBox)
                    'qry = "UPDATE tblcontract SET Salesman =@Salesman where rcno=" & Convert.ToInt32(lblid1.Text)

                    Dim lblid1 As TextBox = CType(GridViewContractESM.Rows(rowIndex).Cells(0).FindControl("txtContractNoCESMGV"), TextBox)
                    qry = "UPDATE tblcontract SET Salesman =@Salesman where ContractNo= '" & lblid1.Text.Trim & "'"

                    command.CommandText = qry
                    command.Parameters.Clear()

                    command.Parameters.AddWithValue("@Salesman", ddlNewSalesman.Text.ToUpper)

                    command.Connection = conn
                    command.ExecuteNonQuery()

                    command.Dispose()

                    'CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "SERVCONT", lblid1.Text.Trim, "EDITSALESMAN", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 0, 0, 0, txtLocationID.Text, "", txtOldSalesman.Text.Trim & " - " & ddlNewSalesman.Text.Trim)
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "SERVCONT", lblid1.Text, "EDITSALESMAN", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 0, 0, 0, txtAccountID.Text, txtOldSalesman.Text + " - " + ddlNewSalesman.Text.ToUpper, txtSvcRcno.Text)

                End If
                '11.10


nextrec:
            Next rowIndex
            'End If

            'Start: Invoice

            rowselected = 0

            rowselected = GridViewInvoiceESM.Rows.Count - 1

            For rowIndexI As Integer = 0 To GridViewInvoiceESM.Rows.Count - 1
                Dim TextBoxchkSelectI As CheckBox = CType(GridViewInvoiceESM.Rows(rowIndexI).Cells(0).FindControl("chkSelectInvoiceESMGV"), CheckBox)

                If (TextBoxchkSelectI.Checked = True) Then

                    Dim qry As String
                    qry = ""

                    Dim command As MySqlCommand = New MySqlCommand
                    command.CommandType = CommandType.Text

                    rowselected = rowselected + 1

                    'Dim lblidI As TextBox = CType(GridViewInvoiceESM.Rows(rowIndexI).Cells(0).FindControl("txtRcnoSalesIESMGV"), TextBox)
                    'qry = "UPDATE tblSales SET StaffCode =@Salesman, SET Salesman =@Salesman where rcno=" & Convert.ToInt32(lblidI.Text)

                    Dim lblidI As TextBox = CType(GridViewInvoiceESM.Rows(rowIndexI).Cells(0).FindControl("txtInvoiceNoIESMGV"), TextBox)
                    qry = "UPDATE tblSales SET StaffCode =@Salesman,  Salesman =@Salesman where InvoiceNumber= '" & lblidI.Text.Trim & "'"

                    command.CommandText = qry
                    command.Parameters.Clear()

                    command.Parameters.AddWithValue("@Salesman", ddlNewSalesman.Text.ToUpper)

                    command.Connection = conn
                    command.ExecuteNonQuery()

                    command.Dispose()

                    'CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "INVOICE", lblidI.Text.Trim, "EDITSALESMAN", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 0, 0, 0, txtLocationID.Text, "", txtOldSalesman.Text.Trim & " - " & ddlNewSalesman.Text.Trim)
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "INVOICE", lblidI.Text, "EDITSALESMAN", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 0, 0, 0, txtAccountID.Text, txtOldSalesman.Text + " - " + ddlNewSalesman.Text.ToUpper, txtSvcRcno.Text)

                End If
                '11.10



            Next rowIndexI

            'End: Invoice

            Dim qry1 As String
            qry1 = "UPDATE tblcompanyLocation SET Salesmansvc =@Salesman where LocationId= '" & txtLocationID.Text & "'"

            Dim command1 As MySqlCommand = New MySqlCommand
            command1.CommandType = CommandType.Text

            command1.CommandText = qry1
            command1.Parameters.Clear()

            command1.Parameters.AddWithValue("@Salesman", ddlNewSalesman.Text.ToUpper)

            command1.Connection = conn
            command1.ExecuteNonQuery()

            command1.Dispose()
            conn.Close()
            conn.Dispose()

            ddlSalesManSvc.Text = ddlNewSalesman.Text.ToUpper
            btnShowRecords_Click(sender, e)

            Label42.Text = "CONTRACTS TO BE UPDATED"
            Label41.Text = "INVOICES TO BE UPDATED"

            txtOldSalesman.Text = ddlNewSalesman.Text.Trim
            ddlNewSalesman.SelectedIndex = 0
            txtEffectiveDate.Text = ""

            mdlImportServices.Hide()

        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "btnUpdateSalesman_Click", ex.Message.ToString, txtNameE.Text)
            lblAlertEditSalesman.Text = ex.Message.ToString
            mdlImportServices.Show()
        End Try
    End Sub

    Protected Sub SqlDSNotesMaster_Selected(sender As Object, e As SqlDataSourceStatusEventArgs) Handles SqlDSNotesMaster.Selected
        If e.AffectedRows >= 0 Then

            lblNotesCount.Text = "Notes [" & e.AffectedRows.ToString & "]"
        Else
            lblNotesCount.Text = "Notes"
        End If
    End Sub

    Protected Sub SqlDataSource2_Selected(sender As Object, e As SqlDataSourceStatusEventArgs) Handles SqlDataSource2.Selected
        If e.AffectedRows >= 0 Then

            lblServiceLocationCount.Text = "Service Location [" & e.AffectedRows.ToString & "]"
        Else
            lblServiceLocationCount.Text = "Service Location"
        End If
    End Sub

    Protected Sub txtOffAddress1_TextChanged(sender As Object, e As EventArgs) Handles txtOffAddress1.TextChanged
        Try
            GridView3.DataSource = ""
            GridView3.DataBind()

            Dim command As MySqlCommand = New MySqlCommand
            Dim sqlstr As String
            Dim conn As MySqlConnection = New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()
            If txtDisplayRecordsLocationwise.Text = "Y" Then
                sqlstr = "Select Location as Branch,AccountID,Name,concat(Address1,AddStreet,AddBuilding) as Address from tblCompany where Address1 = """ & txtOffAddress1.Text.Trim & """"

            Else
                sqlstr = "Select AccountID,Name,concat(Address1,AddStreet,AddBuilding) as Address from tblCompany where Address1 = """ & txtOffAddress1.Text.Trim & """"

            End If
        
            command.CommandType = CommandType.Text
            command.CommandText = sqlstr
            command.Connection = conn

            Dim dr As MySqlDataReader = command.ExecuteReader()
            Dim dt As New System.Data.DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then

                'Dim message As String = "alert('Customer Already Exists..')"

                'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)
                ''MessageBox.Message.Alert(Page, "Company Already Exists..", "str")
                '  lblAlert.Text = "Customer Name already exists"
                GridView3.DataSource = dt
                GridView3.DataBind()
                mdlPopupCustExists.Show()

            End If

            command.Dispose()
            dt.Dispose()
            dr.Close()

            'Dim CustName As String = IgnoredWords(conn, txtNameE.Text.ToUpper)

            'Dim command1 As MySqlCommand = New MySqlCommand

            'command1.CommandType = CommandType.Text

            'command1.CommandText = "SELECT AccountID,Name,Address1 as Address FROM tblcompany where Name like '%" & CustName & "%'"
            'command1.Parameters.AddWithValue("@name", CustName)
            'command1.Connection = conn

            'Dim dr1 As MySqlDataReader = command1.ExecuteReader()
            'Dim dt1 As New System.Data.DataTable
            'dt1.Load(dr1)

            'If dt1.Rows.Count > 0 Then
            '    For i As Int32 = 0 To dt1.Rows.Count - 1
            '        If CustName = IgnoredWords(conn, dt1.Rows(i)("Name").ToString.ToUpper) Then
            '            '  lblAlert.Text = "Customer Name already exists"
            '            GridView3.DataSource = dt1
            '            GridView3.DataBind()
            '            mdlPopupCustExists.Show()
            '            txtCreatedOn.Text = ""
            '            Exit Sub
            '        End If
            '    Next

            'End If
            conn.Close()
            conn.Dispose()

            '  txtBillingName.Text = txtNameE.Text.Trim
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "txtOffAddress1_TextChanged", ex.Message.ToString, txtNameE.Text)
        End Try
    End Sub

    Private Function ValidateSave(conn As MySqlConnection) As Boolean
        Try
            GridView3.DataSource = ""
            GridView3.DataBind()

            Dim command As MySqlCommand = New MySqlCommand
            Dim sqlstr As String
            'Dim conn As MySqlConnection = New MySqlConnection()
            'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            'conn.Open()

            If txtDisplayRecordsLocationwise.Text = "Y" Then
                sqlstr = "Select Name from tblCompany where Name = """ & txtNameE.Text.Trim.ToUpper & """ and location = '" & ddlLocation.Text.Trim & "'"
            Else
                sqlstr = "Select Name from tblCompany where Name = """ & txtNameE.Text.Trim.ToUpper & """"
            End If

            command.CommandType = CommandType.Text
            command.CommandText = sqlstr
            command.Connection = conn

            Dim dr As MySqlDataReader = command.ExecuteReader()
            Dim dt As New System.Data.DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then

                'Dim message As String = "alert('Customer Already Exists..')"

                'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)
                ''MessageBox.Message.Alert(Page, "Company Already Exists..", "str")
                lblAlert.Text = "Customer Name already exists"
                Return False

                'GridView3.DataSource = dt
                'GridView3.DataBind()
                'mdlPopupCustExists.Show()
            End If

            command.Dispose()
            dt.Dispose()
            dr.Close()

            Dim CustName As String = IgnoredWords(conn, txtNameE.Text.ToUpper)

            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            If txtDisplayRecordsLocationwise.Text = "Y" Then
                command1.CommandText = "SELECT Location as Branch,AccountID,Name,concat(Address1,AddStreet,AddBuilding) as Address FROM tblcompany where Name like '%" & CustName & "%'"

            Else
                command1.CommandText = "SELECT AccountID,Name,concat(Address1,AddStreet,AddBuilding) as Address FROM tblcompany where Name like '%" & CustName & "%'"

            End If
             '   command1.Parameters.AddWithValue("@name", CustName)
            command1.Connection = conn

            Dim dr1 As MySqlDataReader = command1.ExecuteReader()
            Dim dt1 As New System.Data.DataTable
            dt1.Load(dr1)

            If dt1.Rows.Count > 0 Then
                For i As Int32 = 0 To dt1.Rows.Count - 1
                    If CustName = IgnoredWords(conn, dt1.Rows(i)("Name").ToString.ToUpper) Then
                        lblAlert.Text = "Customer Name already exists"
                        'GridView3.DataSource = dt1
                        'GridView3.DataBind()
                        'mdlPopupCustExists.Show()
                        ' txtCreatedOn.Text = ""
                        Return False

                    End If
                Next

            End If

            GridView3.DataSource = ""
            GridView3.DataBind()

            Dim command2 As MySqlCommand = New MySqlCommand
            Dim sqlstr2 As String

            If txtDisplayRecordsLocationwise.Text = "Y" Then
                sqlstr2 = "Select Location as Branch,AccountID,Name,concat(Address1,AddStreet,AddBuilding) from tblCompany where Address1 = """ & txtOffAddress1.Text.Trim & """"

            Else
                sqlstr2 = "Select AccountID,Name,concat(Address1,AddStreet,AddBuilding) from tblCompany where Address1 = """ & txtOffAddress1.Text.Trim & """"

            End If
           
            command2.CommandType = CommandType.Text
            command2.CommandText = sqlstr2
            command2.Connection = conn

            Dim dr2 As MySqlDataReader = command2.ExecuteReader()
            Dim dt2 As New System.Data.DataTable
            dt2.Load(dr2)

            If dt2.Rows.Count > 0 Then

                'Dim message As String = "alert('Customer Already Exists..')"

                'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)
                ''MessageBox.Message.Alert(Page, "Company Already Exists..", "str")
                lblAlert.Text = "Office Address already exists"
                Return False

            End If

            command2.Dispose()
            dt2.Dispose()
            dr2.Close()
            'conn.Close()
            'conn.Dispose()

            Return True

        Catch ex As Exception

            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "ValidateSave()", ex.Message.ToString, txtNameE.Text)
            Return False

        End Try
    End Function

    Private Sub PopulateStaff()
        'Dim Query As String
        'If txtSvcMode.Text = "NEW" Then

        '    'Dim Query As String
        '    Query = "SELECT StaffId FROM tblstaff where roles= 'SALES MAN' and  Status ='O' ORDER BY STAFFID"
        '    'PopulateDropDownList(Query, "StaffId", "StaffId", ddlSalesMan)
        '    PopulateDropDownList(Query, "StaffId", "StaffId", ddlSalesManSvc)

        '    'Query = "SELECT StaffId FROM tblstaff where roles= 'SALES MAN'  ORDER BY STAFFID"
        '    'PopulateDropDownList(Query, "StaffId", "StaffId", ddlSalesMan)
        '    'PopulateDropDownList(Query, "StaffId", "StaffId", ddlSalesManSvc)
        '    'PopulateDropDownList(Query, "StaffId", "StaffId", ddlSearchSalesman)
        '    'PopulateDropDownList(Query, "StaffId", "StaffId", ddlNewSalesman)
        'Else
        '    Query = "SELECT StaffId FROM tblstaff where roles= 'SALES MAN'  ORDER BY STAFFID"
        '    'PopulateDropDownList(Query, "StaffId", "StaffId", ddlSalesMan)
        '    PopulateDropDownList(Query, "StaffId", "StaffId", ddlSalesManSvc)
        'End If

    End Sub

    Protected Sub btnEmailSOA_Click(sender As Object, e As EventArgs) Handles btnEmailSOA.Click
        If String.IsNullOrEmpty(txtBillCP1Email.Text) = False And String.IsNullOrEmpty(txtBillCP2Email.Text) = True Then
            txtClientEmailAddr.Text = txtBillCP1Email.Text
        ElseIf String.IsNullOrEmpty(txtBillCP1Email.Text) = True And String.IsNullOrEmpty(txtBillCP2Email.Text) = False Then
            txtClientEmailAddr.Text = txtBillCP2Email.Text
        ElseIf String.IsNullOrEmpty(txtBillCP1Email.Text) = False And String.IsNullOrEmpty(txtBillCP2Email.Text) = False Then
            txtClientEmailAddr.Text = txtBillCP1Email.Text & ";" & txtBillCP2Email.Text
        ElseIf String.IsNullOrEmpty(txtBillCP1Email.Text) = True And String.IsNullOrEmpty(txtBillCP2Email.Text) = True Then
            txtClientEmailAddr.Text = ""
        End If

        Dim conn As MySqlConnection = New MySqlConnection()
        Dim command As MySqlCommand = New MySqlCommand

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        command.CommandType = CommandType.Text
        command.CommandText = "SELECT EmailPerson FROM tblstaff where SecWebLoginID = @userid;"
        command.Parameters.AddWithValue("@userid", Session("UserID"))
        command.Connection = conn

        Dim dr As MySqlDataReader = command.ExecuteReader()
        Dim dt As New DataTable
        dt.Load(dr)

        If dt.Rows.Count > 0 Then
            If String.IsNullOrEmpty(dt.Rows(0)("EmailPerson").ToString()) = False Then
                txtInternalEmailAddr.Text = dt.Rows(0)("EmailPerson").ToString()
            End If
        End If

        command.Dispose()
        dt.Clear()
        dr.Close()
        dt.Dispose()

        mdlPopupEmailSOA.Show()
    End Sub


    'Protected Sub rdbClientEmailSOA_CheckedChanged(sender As Object, e As EventArgs) Handles rdbClientEmailSOA.CheckedChanged
    '    txtClientEmailAddr.Enabled = True
    '    rdbInternalSOA.Checked = False
    '    txtInternalEmailAddr.Enabled = False

    'End Sub


    ''Protected Sub rdbInternalEmailSOA_CheckedChanged(sender As Object, e As EventArgs) Handles rdbInternalEmailSOA.CheckedChanged
    ''    txtInternalEmailAddr.Enabled = True
    ''End Sub

    'Protected Sub rdbInternalSOA_CheckedChanged(sender As Object, e As EventArgs) Handles rdbInternalSOA.CheckedChanged
    '    txtInternalEmailAddr.Enabled = True
    '    rdbClientEmailSOA.Checked = False
    '    txtClientEmailAddr.Enabled = False

    'End Sub

    Protected Sub rdbEmailSOA_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rdbEmailSOA.SelectedIndexChanged
        If rdbEmailSOA.SelectedValue = "1" Then
            txtClientEmailAddr.Enabled = True
            txtInternalEmailAddr.Enabled = False
        ElseIf rdbEmailSOA.SelectedValue = "2" Then
            txtClientEmailAddr.Enabled = False
            txtInternalEmailAddr.Enabled = True
        End If
        mdlPopupEmailSOA.Show()

    End Sub

    Protected Sub btnSendEmailSOA_Click(sender As Object, e As EventArgs) Handles btnSendEmailSOA.Click
        Dim conn As New MySqlConnection()
        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        Dim command As MySqlCommand = New MySqlCommand

        command.CommandType = CommandType.Text
        Dim qry1 As String = "INSERT INTO tblautoemailreport(AccountIDFrom,AccountIDTo,DocumentType,CreatedBy,CreatedOn,"
        qry1 = qry1 + "Module,Generated,BatchNo,FileType,Selection,Selformula,RetryCount,DomainName,Distribution,ContractGroup,"
        qry1 = qry1 + "Branch,CutOffDate,"
        qry1 = qry1 + "InvoiceType,Location,PeriodFrom,PeriodTo,InvDateFrom,InvDateTo,DueDateFrom,DueDateTo,LedgerCodeFrom,"
        qry1 = qry1 + "LedgerCodeTo,Incharge,SalesMan,AccountType,AccountName,CompanyGroup,LocateGrp,Terms,GLStatus,"
        qry1 = qry1 + "ReportFormat,IncludeCompanyInfo,IncludeRecInfo,PrintDate,IncludeUnPaidBal)"
        qry1 = qry1 + "VALUES(@AccountIDFrom,@AccountIDTo,@DocumentType,@CreatedBy,@CreatedOn,@Module,@Generated,"
        qry1 = qry1 + "@BatchNo,@FileType,@Selection,@Selformula,@RetryCount,@DomainName,@Distribution,@ContractGroup,@Branch,"
        qry1 = qry1 + "@CutOffDate,@InvoiceType,@Location,@PeriodFrom,@PeriodTo,@InvDateFrom,@InvDateTo,@DueDateFrom,"
        qry1 = qry1 + "@DueDateTo,@LedgerCodeFrom,@LedgerCodeTo,@Incharge,@SalesMan,@AccountType,@AccountName,@CompanyGroup,"
        qry1 = qry1 + "@LocateGrp,@Terms,@GLStatus,@ReportFormat,@IncludeCompanyInfo,@IncludeRecInfo,@PrintDate,"
        qry1 = qry1 + "@IncludeUnPaidBal);"

        command.CommandText = qry1
        command.Parameters.Clear()

        command.Parameters.AddWithValue("@AccountIDFrom", Convert.ToString(Session("AccountID")))
        command.Parameters.AddWithValue("@AccountIDTo", Convert.ToString(Session("AccountID")))
        command.Parameters.AddWithValue("@DocumentType", "SOA")
       

        command.Parameters.AddWithValue("@Module", "CUSTSOA")
        command.Parameters.AddWithValue("@Generated", 0)
        command.Parameters.AddWithValue("@BatchNo", Session("UserID").ToString + " " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))

        command.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
        command.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))


        command.Parameters.AddWithValue("@FileType", "PDF")

        If rdbEmailSOA.SelectedValue = "1" Then
            If String.IsNullOrEmpty(txtClientEmailAddr.Text) Then
                lblAlertEmailSOA.Text = "Email Address Empty"
                mdlPopupEmailSOA.Show()
                Return

            Else
                command.Parameters.AddWithValue("@Selection", txtClientEmailAddr.Text)

            End If
           
        ElseIf rdbEmailSOA.SelectedValue = "2" Then
            If String.IsNullOrEmpty(txtInternalEmailAddr.Text) Then
                lblAlertEmailSOA.Text = "Email Address Empty"
                mdlPopupEmailSOA.Show()
                Return

            Else
                command.Parameters.AddWithValue("@Selection", txtInternalEmailAddr.Text)

            End If
           
        End If


            command.Parameters.AddWithValue("@SelFormula", Session("SelFormula"))
            command.Parameters.AddWithValue("@RetryCount", 0)

            command.Parameters.AddWithValue("@DomainName", ConfigurationManager.AppSettings("DomainName").ToString())
            command.Parameters.AddWithValue("@Distribution", True)

            If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                command.Parameters.AddWithValue("@Branch", Convert.ToString(Session("Branch")))

                command.Parameters.AddWithValue("@Location", "")

            Else
                command.Parameters.AddWithValue("@Branch", "")
                command.Parameters.AddWithValue("@Location", "")
            End If

            command.Parameters.AddWithValue("@InvoiceType", "")


            command.Parameters.AddWithValue("@PeriodFrom", "")

            command.Parameters.AddWithValue("@PeriodTo", "")

            command.Parameters.AddWithValue("@InvDateFrom", "")

            command.Parameters.AddWithValue("@InvDateTo", "")

            command.Parameters.AddWithValue("@DueDateFrom", "")

            command.Parameters.AddWithValue("@DueDateTo", "")

            command.Parameters.AddWithValue("@LedgerCodeFrom", "")

            command.Parameters.AddWithValue("@LedgerCodeTo", "")

            command.Parameters.AddWithValue("@Incharge", "")

            command.Parameters.AddWithValue("@SalesMan", "")

            command.Parameters.AddWithValue("@AccountType", "COMPANY")

            command.Parameters.AddWithValue("@AccountName", Convert.ToString(Session("CustName")))

            command.Parameters.AddWithValue("@CompanyGroup", "")

            command.Parameters.AddWithValue("@LocateGrp", "")

            command.Parameters.AddWithValue("@Terms", "")

            command.Parameters.AddWithValue("@GLStatus", "")

            command.Parameters.AddWithValue("@PrintDate", "")

            command.Parameters.AddWithValue("@IncludeUnpaidBal", False)


            If String.IsNullOrEmpty(Convert.ToString(Session("cutoffoscustomer"))) Then
                command.Parameters.AddWithValue("@CutOffDate", "")
            Else

                Dim d As DateTime
                If Date.TryParseExact(Convert.ToString(Session("cutoffoscustomer")), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

                Else

                End If
                command.Parameters.AddWithValue("@CutOffDate", d.ToString("yyyy-MM-dd"))
            End If


            command.Parameters.AddWithValue("@IncludeCompanyInfo", False)

            command.Parameters.AddWithValue("@IncludeRecInfo", False)


            command.Parameters.AddWithValue("@ContractGroup", "")

            command.Parameters.AddWithValue("@ReportFormat", "CUST - SOA")

            command.Connection = conn

            command.ExecuteNonQuery()

            command.Dispose()
            conn.Close()
            conn.Dispose()



            mdlPopupMsg.Show()
    End Sub

    Protected Sub btnImportData_Click(sender As Object, e As EventArgs) Handles btnImportData.Click
        mdlImportData.Show()

    End Sub

    Protected Function InsertCorporateData(dt As DataTable, conn As MySqlConnection) As Boolean
        Dim Success As Int32 = 0
        Dim Failure As Int32 = 0
        Dim FailureString As String = ""

        Dim dtLog As New DataTable

        Try

            Dim qry As String = "INSERT INTO tblcompany(Id,Name,AddBlock,AddNos,AddFloor,AddUnit,AddBuilding,AddStreet,AddPostal,AddState,AddCity,AddCountry,Telephone,Fax,BillingAddress,"
            qry = qry + "BillBlock,BillNos,BillFloor,BillUnit,BillBuilding,BillStreet,BillPostal,BillState,BillCity,BillCountry,ContactPerson,Comments,RocNos,RocRegDate,AuthorizedCapital,"
            qry = qry + "PaidupCapital,CompanyType,Industry,FinanceCompanyId,FinanceCompany,ArLimit,ApLimit,SalesLimit,PurchaseLimit,ApCurrency,ArCurrency,SendStatement,GstRegistered,GstNos,Status,"
            qry = qry + "Address1,BillAddress1,ContactPersonEmail,Website,Source,ARBal,APBal,Sales,Purchase,LocateGRP,SalesGRP,Dealer,LoginID,Email,Password,WebLevel,ARTERM,APTERM,PriceGroup,InChargeID,"
            qry = qry + "Age0,Age30,Age60,Age90,Age120,SalesMan,StopSalesYN,StopPurchYN,SpecCode,ArWarning,StartDate,LicenseNumber,LicenseInfo,SalesGST,ArMethod,ApMethod,ProductM1,ProductM2,ProductM3,ProductM4,"
            qry = qry + "ProductF1,ProductF2,ProductF3,ProductF4,RentalTerm,CompanyGroup,Donor,Member,MemberType,MemberID,GIROID,DateJoin,DateExpired,DateTerminate,TemplateNo,ARLedger,ARSubLedger,APLedger,APSubLedger,"
            qry = qry + "SrcCompID,DiscountPct,PreferredCustYN,ChkGstInclusive,Reason,Boardmember,BoardDesignation,period,Intriducer,Organization,chkLetterIndemnity,LetterIndemnitySignedBy,LeterDate,CreatedBy,CreatedOn,"
            qry = qry + "LastModifiedBy,LastModifiedOn,BillTelephone,BillFax,Name2,WebLoginID,WebLoginPassWord,WebAccessLevel,WebOneTimePassWord,BillContactPerson,WebGroupDealer,WebDisable,WebID,OTPMobile,OTPYN,OTPGenerateDate,"
            qry = qry + "HideInStock,OverdueDaysLimit,OverdueDaysLimitActive,OverdueDaysWarning,OverdueDaysWarningActive,chkAR,DueDaysStopFreq,SubCompanyNo,SourceCompany,chkSendServiceReport,Telephone2,BillTelephone2,Mobile,BillMobile,"
            qry = qry + "SoPriceGroup,POPrefix,PONumber,LastStatus,OverdueMonthWarning,OverDueMonthLimit,AccountNo,FlowFrom,FlowTo,InActive,ShippingTerm,InterCompany,AutoEmailServ,ReportFormatServ,WebUploadDate,IsCustomer,IsSupplier,PaxBased,"
            qry = qry + "BillMonthly,DiscType,ARPDFFromat,EmailConsolidate,AccountID,OffContact1Position,OffContact1Tel,OffContact1Fax,OffContact1Tel2,OffContact1Mobile,BillContact1Position,BillContact2,BillContact2Position,"
            qry = qry + "BillContact1Email,BillContact2Email,BillContact2Tel,BillContact2Fax,BillContact2Tel2,BillContact2Mobile,OffContact1,OffContactPosition,BillingSettings,BillingName,TermsDay,Location, DefaultInvoiceFormat, AutoEmailInvoice,AutoEmailSOA)VALUES(@Id,@Name,@AddBlock,@AddNos,@AddFloor,@AddUnit,@AddBuilding,@AddStreet,@AddPostal,@AddState,@AddCity,@AddCountry,@Telephone,@Fax,@BillingAddress,@BillBlock,@BillNos,"
            qry = qry + "@BillFloor,@BillUnit,@BillBuilding,@BillStreet,@BillPostal,@BillState,@BillCity,@BillCountry,@ContactPerson,@Comments,@RocNos,@RocRegDate,@AuthorizedCapital,@PaidupCapital,@CompanyType,@Industry,@FinanceCompanyId,@FinanceCompany,"
            qry = qry + "@ArLimit,@ApLimit,@SalesLimit,@PurchaseLimit,@ApCurrency,@ArCurrency,@SendStatement,@GstRegistered,@GstNos,@Status,@Address1,@BillAddress1,@ContactPersonEmail,@Website,@Source,@ARBal,@APBal,@Sales,@Purchase,@LocateGRP,@SalesGRP,@Dealer,"
            qry = qry + "@LoginID,@Email,@Password,@WebLevel,@ARTERM,@APTERM,@PriceGroup,@InChargeID,@Age0,@Age30,@Age60,@Age90,@Age120,@SalesMan,@StopSalesYN,@StopPurchYN,@SpecCode,@ArWarning,@StartDate,@LicenseNumber,@LicenseInfo,@SalesGST,@ArMethod,@ApMethod,"
            qry = qry + "@ProductM1,@ProductM2,@ProductM3,@ProductM4,@ProductF1,@ProductF2,@ProductF3,@ProductF4,@RentalTerm,@CompanyGroup,@Donor,@Member,@MemberType,@MemberID,@GIROID,@DateJoin,@DateExpired,@DateTerminate,@TemplateNo,@ARLedger,@ARSubLedger,"
            qry = qry + "@APLedger,@APSubLedger,@SrcCompID,@DiscountPct,@PreferredCustYN,@ChkGstInclusive,@Reason,@Boardmember,@BoardDesignation,@period,@Intriducer,@Organization,@chkLetterIndemnity,@LetterIndemnitySignedBy,@LeterDate,@CreatedBy,@CreatedOn,"
            qry = qry + "@LastModifiedBy,@LastModifiedOn,@BillTelephone,@BillFax,@Name2,@WebLoginID,@WebLoginPassWord,@WebAccessLevel,@WebOneTimePassWord,@BillContactPerson,@WebGroupDealer,@WebDisable,@WebID,@OTPMobile,@OTPYN,@OTPGenerateDate,@HideInStock,"
            qry = qry + "@OverdueDaysLimit,@OverdueDaysLimitActive,@OverdueDaysWarning,@OverdueDaysWarningActive,@chkAR,@DueDaysStopFreq,@SubCompanyNo,@SourceCompany,@chkSendServiceReport,@Telephone2,@BillTelephone2,@Mobile,@BillMobile,@SoPriceGroup,@POPrefix,"
            qry = qry + "@PONumber,@LastStatus,@OverdueMonthWarning,@OverDueMonthLimit,@AccountNo,@FlowFrom,@FlowTo,@InActive,@ShippingTerm,@InterCompany,@AutoEmailServ,@ReportFormatServ,@WebUploadDate,@IsCustomer,@IsSupplier,@PaxBased,@BillMonthly,@DiscType,@ARPDFFromat,@EmailConsolidate"
            qry = qry + ",@AccountID,@OffContact1Position,@OffContact1Tel,@OffContact1Fax,@OffContact1Tel2,@OffContact1Mobile,@BillContact1Position,@BillContact2,@BillContact2Position,@BillContact1Email,@BillContact2Email,@BillContact2Tel,@BillContact2Fax,@BillContact2Tel2,@BillContact2Mobile,@OffContact1,@OffContactPosition,@BillingSettings,@BillingName,@TermsDay,@Location, @DefaultInvoiceFormat, @AutoEmailInvoice,@AutoEmailSOA);"

            Dim AccountID As String = ""
            Dim Exists As Boolean = True


            InsertIntoTblWebEventLog("COMPANY-EXCEL", "InsertData", dt.Rows.Count.ToString, "")

            Dim drow As DataRow
            Dim dc1 As DataColumn = New DataColumn("AccountID", GetType(String))
            Dim dc2 As DataColumn = New DataColumn("Name", GetType(String))
            Dim dc3 As DataColumn = New DataColumn("Status", GetType(String))
            Dim dc4 As DataColumn = New DataColumn("Remarks", GetType(String))
            dtLog.Columns.Add(dc1)
            dtLog.Columns.Add(dc2)
            dtLog.Columns.Add(dc3)
            dtLog.Columns.Add(dc4)

            For Each r As DataRow In dt.Rows

                drow = dtLog.NewRow()

                If IsDBNull(r("AccountID")) Then
                    Failure = Failure + 1
                    FailureString = FailureString + " " + AccountID + "(ACCOUNTID_BLANK)"
                    drow("Status") = "Failed"
                    drow("Remarks") = "ACCOUNTID_BLANK"
                    dtLog.Rows.Add(drow)
                    Continue For
                Else
                    InsertIntoTblWebEventLog("COMPANY-EXCEL", "InsertData1", dt.Rows.Count.ToString, r("AccountID"))

                    AccountID = r("AccountID")
                    drow("AccountID") = AccountID

                End If

                Dim command2 As MySqlCommand = New MySqlCommand

                command2.CommandType = CommandType.Text

                command2.CommandText = "SELECT * FROM tblcompany where accountid=@id"
                command2.Parameters.AddWithValue("@id", AccountID)
                command2.Connection = conn

                Dim dr1 As MySqlDataReader = command2.ExecuteReader()
                Dim dt1 As New System.Data.DataTable
                dt1.Load(dr1)

                If dt1.Rows.Count > 0 Then

                    '  MessageBox.Message.Alert(Page, "Account ID already exists!!!", "str")
                    ' lblAlert.Text = "Account ID already exists!!!"
                    Failure = Failure + 1
                    FailureString = FailureString + " " + AccountID + "(DUPLICATE)"
                    drow("Status") = "Failed"
                    drow("Remarks") = "ACCOUNTID_DUPLICATE"
                    If IsDBNull(r("Name")) = False Then
                        drow("Name") = r("Name")
                    End If
                    dtLog.Rows.Add(drow)

                Else
                    'Check for dropdownlist values, if it exists

                    If IsDBNull(r("Industry")) = False Then
                        If CheckIndustryExists(r("Industry"), conn) = False Then

                            Failure = Failure + 1
                            FailureString = FailureString + " " + AccountID + "(INDUSTRY DOES NOT EXIST)"
                            drow("Status") = "Failed"
                            drow("Remarks") = "INDUSTRY DOES NOT EXIST"
                            If IsDBNull(r("Name")) = False Then
                                drow("Name") = r("Name")
                            End If
                            dtLog.Rows.Add(drow)
                            Continue For
                        End If
                    End If

                    If IsDBNull(r("AddCity")) = False Then
                        If CheckCityExists(r("AddCity"), conn) = False Then

                            Failure = Failure + 1
                            FailureString = FailureString + " " + AccountID + "(CITY DOES NOT EXIST)"
                            drow("Status") = "Failed"
                            drow("Remarks") = "CITY DOES NOT EXIST"
                            If IsDBNull(r("Name")) = False Then
                                drow("Name") = r("Name")
                            End If
                            dtLog.Rows.Add(drow)
                            Continue For
                        End If
                    End If

                    If IsDBNull(r("AddState")) = False Then
                        If CheckStateExists(r("AddState"), conn) = False Then
                            Failure = Failure + 1
                            FailureString = FailureString + " " + AccountID + "(STATE DOES NOT EXIST)"
                            drow("Status") = "Failed"
                            drow("Remarks") = "STATE DOES NOT EXIST"
                            If IsDBNull(r("Name")) = False Then
                                drow("Name") = r("Name")
                            End If
                            dtLog.Rows.Add(drow)
                            Continue For
                        End If
                    End If

                    If IsDBNull(r("AddCountry")) = False Then
                        If CheckCountryExists(r("AddCountry"), conn) = False Then
                            Failure = Failure + 1
                            FailureString = FailureString + " " + AccountID + "(COUNTRY DOES NOT EXIST)"
                            drow("Status") = "Failed"
                            drow("Remarks") = "COUNTRY DOES NOT EXIST"
                            If IsDBNull(r("Name")) = False Then
                                drow("Name") = r("Name")
                            End If
                            dtLog.Rows.Add(drow)
                            Continue For
                        End If
                    End If

                    If IsDBNull(r("BillCity")) = False Then
                        If CheckCityExists(r("BillCity"), conn) = False Then

                            Failure = Failure + 1
                            FailureString = FailureString + " " + AccountID + "(CITY DOES NOT EXIST)"
                            drow("Status") = "Failed"
                            drow("Remarks") = "CITY DOES NOT EXIST"
                            If IsDBNull(r("Name")) = False Then
                                drow("Name") = r("Name")
                            End If
                            dtLog.Rows.Add(drow)
                            Continue For
                        End If
                    End If

                    If IsDBNull(r("BillState")) = False Then
                        If CheckStateExists(r("BillState"), conn) = False Then
                            Failure = Failure + 1
                            FailureString = FailureString + " " + AccountID + "(STATE DOES NOT EXIST)"
                            drow("Status") = "Failed"
                            drow("Remarks") = "STATE DOES NOT EXIST"
                            If IsDBNull(r("Name")) = False Then
                                drow("Name") = r("Name")
                            End If
                            dtLog.Rows.Add(drow)
                            Continue For
                        End If
                    End If

                    If IsDBNull(r("BillCountry")) = False Then
                        If CheckCountryExists(r("BillCountry"), conn) = False Then
                            Failure = Failure + 1
                            FailureString = FailureString + " " + AccountID + "(COUNTRY DOES NOT EXIST)"
                            drow("Status") = "Failed"
                            drow("Remarks") = "COUNTRY DOES NOT EXIST"
                            If IsDBNull(r("Name")) = False Then
                                drow("Name") = r("Name")
                            End If
                            dtLog.Rows.Add(drow)
                            Continue For
                        End If
                    End If

                    If IsDBNull(r("CreditTerms")) = False Then
                        If CheckTermsExists(r("CreditTerms"), conn) = False Then
                            Failure = Failure + 1
                            FailureString = FailureString + " " + AccountID + "(CREDIT TERMS DOES NOT EXIST)"
                            drow("Status") = "Failed"
                            drow("Remarks") = "CREDIT TERMS DOES NOT EXIST"
                            If IsDBNull(r("Name")) = False Then
                                drow("Name") = r("Name")
                            End If
                            dtLog.Rows.Add(drow)
                            Continue For
                        End If
                    End If

                    If IsDBNull(r("ARCurrency")) = False Then
                        If CheckCurrencyExists(r("ARCurrency"), conn) = False Then
                            Failure = Failure + 1
                            FailureString = FailureString + " " + AccountID + "(CURRENCY DOES NOT EXIST)"
                            drow("Status") = "Failed"
                            drow("Remarks") = "CURRENCY DOES NOT EXIST"
                            If IsDBNull(r("Name")) = False Then
                                drow("Name") = r("Name")
                            End If
                            dtLog.Rows.Add(drow)
                            Continue For
                        End If
                    End If

                    If IsDBNull(r("DefaultInvoiceFormat")) = False Then
                        If CheckInvoiceFormatExists(r("DefaultInvoiceFormat"), conn) = False Then
                            Failure = Failure + 1
                            FailureString = FailureString + " " + AccountID + "(INVOICE FORMAT DOES NOT EXIST)"
                            drow("Status") = "Failed"
                            drow("Remarks") = "INVOICE FORMAT DOES NOT EXIST"
                            If IsDBNull(r("Name")) = False Then
                                drow("Name") = r("Name")
                            End If
                            dtLog.Rows.Add(drow)
                            Continue For
                        End If

                    End If


                    Dim cmd As MySqlCommand = conn.CreateCommand()
                    '  Dim cmd As MySqlCommand = New MySqlCommand

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = qry
                    cmd.Parameters.Clear()

                    If IsDBNull(r("AccountID")) Then
                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(ACCOUNTID_BLANK)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "ACCOUNTID_BLANK"
                        If IsDBNull(r("Name")) = False Then
                            drow("Name") = r("Name")
                        End If
                        dtLog.Rows.Add(drow)

                        Continue For

                    Else
                        cmd.Parameters.AddWithValue("@AccountID", r("AccountID").ToString.ToUpper)
                    End If

                    If IsDBNull(r("Name")) Then
                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(NAME_BLANK)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "NAME_BLANK"
                        dtLog.Rows.Add(drow)
                        Continue For
                    Else
                        drow("Name") = r("Name")
                        cmd.Parameters.AddWithValue("@Name", r("Name").ToString.ToUpper)
                    End If

                    '  If IsDBNull(r("OldName")) Then
                    If IsDBNull(r("OldName")) Then
                        cmd.Parameters.AddWithValue("@Name2", "")
                    Else
                        cmd.Parameters.AddWithValue("@Name2", r("OldName").ToString.ToUpper)
                    End If
                    cmd.Parameters.AddWithValue("@Status", "O")
                    cmd.Parameters.AddWithValue("@InActive", 0)

                    cmd.Parameters.AddWithValue("@CompanyGroup", "")
                    'If IsDBNull(r("CompanyGroup")) Then
                    '    Failure = Failure + 1
                    '    FailureString = FailureString + " " + AccountID + "(COMPANYGROUP_BLANK)"
                    '    drow("Status") = "Failed"
                    '    drow("Remarks") = "COMPANYGROUP_BLANK"
                    '    dtLog.Rows.Add(drow)
                    '    Continue For
                    'Else
                    '    cmd.Parameters.AddWithValue("@CompanyGroup", r("CompanyGroup"))
                    'End If
                    If IsDBNull(r("RegistrationNo")) Then
                        cmd.Parameters.AddWithValue("@RocNos", "")
                    Else
                        cmd.Parameters.AddWithValue("@RocNos", r("RegistrationNo").ToString.ToUpper)
                    End If
                    If IsDBNull(r("GstNos")) Then
                        cmd.Parameters.AddWithValue("@GstNos", "")
                    Else
                        cmd.Parameters.AddWithValue("@GstNos", r("GstNos").ToString.ToUpper)
                    End If

                    If IsDBNull(r("Website")) Then
                        cmd.Parameters.AddWithValue("@Website", "")
                    Else
                        cmd.Parameters.AddWithValue("@Website", r("Website").ToString.ToUpper)
                    End If
                    If IsDBNull(r("CustomerSince")) Then
                        cmd.Parameters.AddWithValue("@StartDate", DBNull.Value)
                    Else
                        cmd.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(r("CustomerSince")).ToString("yyyy-MM-dd"))
                    End If

                    If IsDBNull(r("Industry")) Then
                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(Industry_BLANK)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "INDUSTRY_BLANK"
                        dtLog.Rows.Add(drow)
                        Continue For
                    Else
                        cmd.Parameters.AddWithValue("@Industry", r("Industry").ToString.ToUpper)

                    End If
                    cmd.Parameters.AddWithValue("@SalesMan", "")
                    cmd.Parameters.AddWithValue("@InChargeID", "")

                    'If IsDBNull(r("SalesMan")) Then
                    '    cmd.Parameters.AddWithValue("@SalesMan", "")
                    'Else
                    '    cmd.Parameters.AddWithValue("@SalesMan", r("SalesMan"))
                    'End If
                    'If IsDBNull(r("InChargeID")) Then
                    '    cmd.Parameters.AddWithValue("@InChargeID", "")
                    'Else
                    '    cmd.Parameters.AddWithValue("@InChargeID", r("InChargeID"))
                    'End If
                    If IsDBNull(r("Comments")) Then
                        cmd.Parameters.AddWithValue("@Comments", "")
                    Else
                        cmd.Parameters.AddWithValue("@Comments", r("Comments").ToString.ToUpper)
                    End If
                    If IsDBNull(r("Address1")) Then
                        cmd.Parameters.AddWithValue("@Address1", "")
                    Else
                        cmd.Parameters.AddWithValue("@Address1", r("Address1").ToString.ToUpper)
                    End If


                    If IsDBNull(r("AddStreet")) Then
                        cmd.Parameters.AddWithValue("@AddStreet", "")
                    Else
                        cmd.Parameters.AddWithValue("@AddStreet", r("AddStreet").ToString.ToUpper)
                    End If
                    If IsDBNull(r("AddBuilding")) Then
                        cmd.Parameters.AddWithValue("@AddBuilding", "")
                    Else
                        cmd.Parameters.AddWithValue("@AddBuilding", r("AddBuilding").ToString.ToUpper)
                    End If
                    If IsDBNull(r("AddCity")) Then
                        cmd.Parameters.AddWithValue("@AddCity", "")
                    Else
                        cmd.Parameters.AddWithValue("@AddCity", r("AddCity").ToString.ToUpper)
                    End If

                    If IsDBNull(r("AddState")) Then

                        cmd.Parameters.AddWithValue("@AddState", "")
                    Else
                        cmd.Parameters.AddWithValue("@AddState", r("AddState").ToString.ToUpper)

                    End If

                    If IsDBNull(r("AddCountry")) Then
                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(OFFICEADDRESS-COUNTRY_BLANK)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "OFFICEADDRESS-COUNTRY_BLANK"
                        dtLog.Rows.Add(drow)
                        Continue For
                    Else
                        cmd.Parameters.AddWithValue("@AddCountry", r("AddCountry").ToString.ToUpper)
                    End If

                    If IsDBNull(r("AddPostal")) Then
                        cmd.Parameters.AddWithValue("@AddPostal", "")
                    Else
                        cmd.Parameters.AddWithValue("@AddPostal", r("AddPostal").ToString.ToUpper)
                    End If
                    If IsDBNull(r("ContactPerson")) Then
                        cmd.Parameters.AddWithValue("@ContactPerson", "")
                    Else
                        cmd.Parameters.AddWithValue("@ContactPerson", r("ContactPerson").ToString.ToUpper)
                    End If
                    If IsDBNull(r("OffContactPosition")) Then
                        cmd.Parameters.AddWithValue("@OffContactPosition", "")
                    Else
                        cmd.Parameters.AddWithValue("@OffContactPosition", r("OffContactPosition").ToString.ToUpper)
                    End If
                    If IsDBNull(r("Telephone")) Then
                        cmd.Parameters.AddWithValue("@Telephone", "")
                    Else
                        cmd.Parameters.AddWithValue("@Telephone", r("Telephone").ToString.ToUpper)
                    End If
                    If IsDBNull(r("Fax")) Then
                        cmd.Parameters.AddWithValue("@Fax", "")
                    Else
                        cmd.Parameters.AddWithValue("@Fax", r("Fax").ToString.ToUpper)
                    End If
                    If IsDBNull(r("Telephone2")) Then
                        cmd.Parameters.AddWithValue("@Telephone2", "")
                    Else
                        cmd.Parameters.AddWithValue("@Telephone2", r("Telephone2").ToString.ToUpper)
                    End If
                    If IsDBNull(r("Mobile")) Then
                        cmd.Parameters.AddWithValue("@Mobile", "")
                    Else
                        cmd.Parameters.AddWithValue("@Mobile", r("Mobile").ToString.ToUpper)
                    End If


                    If IsDBNull(r("Email")) Then
                        cmd.Parameters.AddWithValue("@Email", "")
                    Else
                        cmd.Parameters.AddWithValue("@Email", r("Email").ToString.ToUpper)
                    End If
                    If IsDBNull(r("OffContact1")) Then
                        cmd.Parameters.AddWithValue("@OffContact1", "")
                    Else
                        cmd.Parameters.AddWithValue("@OffContact1", r("OffContact1").ToString.ToUpper)
                    End If
                    If IsDBNull(r("OffContact1Position")) Then
                        cmd.Parameters.AddWithValue("@OffContact1Position", "")
                    Else
                        cmd.Parameters.AddWithValue("@OffContact1Position", r("OffContact1Position").ToString.ToUpper)
                    End If
                    If IsDBNull(r("OffContact1Tel")) Then
                        cmd.Parameters.AddWithValue("@OffContact1Tel", "")
                    Else
                        cmd.Parameters.AddWithValue("@OffContact1Tel", r("OffContact1Tel").ToString.ToUpper)
                    End If
                    If IsDBNull(r("OffContact1Fax")) Then
                        cmd.Parameters.AddWithValue("@OffContact1Fax", "")
                    Else
                        cmd.Parameters.AddWithValue("@OffContact1Fax", r("OffContact1Fax").ToString.ToUpper)
                    End If
                    If IsDBNull(r("OffContact1Tel2")) Then
                        cmd.Parameters.AddWithValue("@OffContact1Tel2", "")
                    Else
                        cmd.Parameters.AddWithValue("@OffContact1Tel2", r("OffContact1Tel2").ToString.ToUpper)
                    End If

                    If IsDBNull(r("OffContact1Mobile")) Then
                        cmd.Parameters.AddWithValue("@OffContact1Mobile", "")
                    Else
                        cmd.Parameters.AddWithValue("@OffContact1Mobile", r("OffContact1Mobile").ToString.ToUpper)
                    End If
                    If IsDBNull(r("ContactPersonEmail")) Then
                        cmd.Parameters.AddWithValue("@ContactPersonEmail", "")
                    Else
                        cmd.Parameters.AddWithValue("@ContactPersonEmail", r("ContactPersonEmail").ToString.ToUpper)
                    End If
                    If IsDBNull(r("BillingAddress")) Then
                        cmd.Parameters.AddWithValue("@BillingAddress", 0)
                    ElseIf r("BillingAddress").ToString.ToUpper = "TRUE" Then
                        cmd.Parameters.AddWithValue("@BillingAddress", 1)
                    ElseIf r("BillingAddress").ToString.ToUpper = "FALSE" Then
                        cmd.Parameters.AddWithValue("@BillingAddress", 0)
                    End If

                    If IsDBNull(r("BillingName")) Then
                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(BILLINGNAME_BLANK)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "BILLINGNAME_BLANK"
                        dtLog.Rows.Add(drow)
                        Continue For
                    Else
                        cmd.Parameters.AddWithValue("@BillingName", r("BillingName").ToString.ToUpper)
                    End If

                    'If Billing Address is same as Office Address - set to True, then copy all Office Contact Info to Billing Contact Info
                    Dim CheckBillingAddress As Boolean = False
                    If IsDBNull(r("BillingAddress")) Then
                        CheckBillingAddress = False
                    Else
                        If r("BillingAddress").ToString.ToUpper = "TRUE" Then
                            CheckBillingAddress = True
                        Else
                            CheckBillingAddress = False
                        End If
                    End If

                    If CheckBillingAddress = True Then
                        If IsDBNull(r("BillAddress1")) Then
                            If IsDBNull(r("Address1")) Then
                                cmd.Parameters.AddWithValue("@BillAddress1", "")
                            Else
                                cmd.Parameters.AddWithValue("@BillAddress1", r("Address1").ToString.ToUpper)
                            End If
                        Else
                            cmd.Parameters.AddWithValue("@BillAddress1", r("BillAddress1").ToString.ToUpper)
                        End If
                        If IsDBNull(r("Billbuilding")) Then
                            If IsDBNull(r("AddBuilding")) Then
                                cmd.Parameters.AddWithValue("@BillBuilding", "")
                            Else
                                cmd.Parameters.AddWithValue("@BillBuilding", r("AddBuilding").ToString.ToUpper)
                            End If
                        Else
                            cmd.Parameters.AddWithValue("@Billbuilding", r("Billbuilding").ToString.ToUpper)
                        End If
                        If IsDBNull(r("BillStreet")) Then
                            If IsDBNull(r("AddStreet")) Then
                                cmd.Parameters.AddWithValue("@BillStreet", "")
                            Else
                                cmd.Parameters.AddWithValue("@BillStreet", r("AddStreet").ToString.ToUpper)
                            End If
                        Else
                            cmd.Parameters.AddWithValue("@BillStreet", r("BillStreet").ToString.ToUpper)
                        End If

                        If IsDBNull(r("BillCity")) Then
                            If IsDBNull(r("AddCity")) Then
                                cmd.Parameters.AddWithValue("@BillCity", "")
                            Else
                                cmd.Parameters.AddWithValue("@BillCity", r("AddCity").ToString.ToUpper)
                            End If
                        Else
                            cmd.Parameters.AddWithValue("@BillCity", r("BillCity").ToString.ToUpper)
                        End If
                        If IsDBNull(r("BillState")) Then
                            If IsDBNull(r("AddState")) Then
                                cmd.Parameters.AddWithValue("@BillState", "")
                            Else
                                cmd.Parameters.AddWithValue("@BillState", r("AddState").ToString.ToUpper)
                            End If
                        Else
                            cmd.Parameters.AddWithValue("@BillState", r("BillState").ToString.ToUpper)
                        End If

                        If IsDBNull(r("BillCountry")) Then
                            If IsDBNull(r("AddCountry")) Then
                                cmd.Parameters.AddWithValue("@BillCountry", "")
                            Else
                                cmd.Parameters.AddWithValue("@BillCountry", r("AddCountry").ToString.ToUpper)
                            End If
                        Else
                            cmd.Parameters.AddWithValue("@BillCountry", r("BillCountry").ToString.ToUpper)
                        End If

                        If IsDBNull(r("BillPostal")) Then
                            If IsDBNull(r("AddPostal")) Then
                                cmd.Parameters.AddWithValue("@BillPostal", "")
                            Else
                                cmd.Parameters.AddWithValue("@BillPostal", r("AddPostal").ToString.ToUpper)
                            End If
                        Else
                            cmd.Parameters.AddWithValue("@BillPostal", r("BillPostal").ToString.ToUpper)
                        End If
                        If IsDBNull(r("BillContactPerson")) Then

                            If IsDBNull(r("ContactPerson")) Then
                                Failure = Failure + 1
                                FailureString = FailureString + " " + AccountID + "(BILLING-CONTACTPERSON_BLANK)"
                                drow("Status") = "Failed"
                                drow("Remarks") = "BILLING-CONTACTPERSON_BLANK"
                                dtLog.Rows.Add(drow)
                                Continue For
                            Else
                                cmd.Parameters.AddWithValue("@BillContactPerson", r("ContactPerson").ToString.ToUpper)
                            End If

                        Else
                            cmd.Parameters.AddWithValue("@BillContactPerson", r("BillContactPerson").ToString.ToUpper)
                        End If
                        If IsDBNull(r("BillContact1Position")) Then
                            If IsDBNull(r("OffContactPosition")) Then
                                cmd.Parameters.AddWithValue("@BillContact1Position", "")
                            Else
                                cmd.Parameters.AddWithValue("@BillContact1Position", r("OffContactPosition").ToString.ToUpper)
                            End If
                        Else
                            cmd.Parameters.AddWithValue("@BillContact1Position", r("BillContact1Position").ToString.ToUpper)
                        End If



                        If IsDBNull(r("BillTelephone")) Then
                            If IsDBNull(r("Telephone")) Then
                                cmd.Parameters.AddWithValue("@BillTelephone", "")
                            Else
                                cmd.Parameters.AddWithValue("@BillTelephone", r("Telephone").ToString.ToUpper)
                            End If
                        Else
                            cmd.Parameters.AddWithValue("@BillTelephone", r("BillTelephone").ToString.ToUpper)
                        End If

                        If IsDBNull(r("BillFax")) Then
                            If IsDBNull(r("Fax")) Then
                                cmd.Parameters.AddWithValue("@BillFax", "")
                            Else
                                cmd.Parameters.AddWithValue("@BillFax", r("Fax").ToString.ToUpper)
                            End If
                        Else
                            cmd.Parameters.AddWithValue("@BillFax", r("BillFax").ToString.ToUpper)
                        End If

                        If IsDBNull(r("BillTelephone2")) Then
                            If IsDBNull(r("Telephone2")) Then
                                cmd.Parameters.AddWithValue("@BillTelephone2", "")
                            Else
                                cmd.Parameters.AddWithValue("@BillTelephone2", r("Telephone2").ToString.ToUpper)
                            End If
                        Else
                            cmd.Parameters.AddWithValue("@BillTelephone2", r("BillTelephone2").ToString.ToUpper)
                        End If
                        If IsDBNull(r("BillMobile")) Then
                            If IsDBNull(r("Mobile")) Then
                                cmd.Parameters.AddWithValue("@BillMobile", "")
                            Else
                                cmd.Parameters.AddWithValue("@BillMobile", r("Mobile").ToString.ToUpper)
                            End If
                        Else
                            cmd.Parameters.AddWithValue("@BillMobile", r("BillMobile").ToString.ToUpper)
                        End If
                        If IsDBNull(r("BillContact1Email")) Then
                            If IsDBNull(r("Email")) Then
                                cmd.Parameters.AddWithValue("@BillContact1Email", "")
                            Else
                                cmd.Parameters.AddWithValue("@BillContact1Email", r("Email").ToString.ToUpper)
                            End If
                        Else
                            cmd.Parameters.AddWithValue("@BillContact1Email", r("BillContact1Email").ToString.ToUpper)
                        End If


                        If IsDBNull(r("BillContact2")) Then
                            If IsDBNull(r("OffContact1")) Then
                                cmd.Parameters.AddWithValue("@BillContact2", "")
                            Else
                                cmd.Parameters.AddWithValue("@BillContact2", r("OffContact1").ToString.ToUpper)
                            End If
                        Else
                            cmd.Parameters.AddWithValue("@BillContact2", r("BillContact2").ToString.ToUpper)
                        End If

                        If IsDBNull(r("BillContact2Position")) Then
                            If IsDBNull(r("OffContact1Position")) Then
                                cmd.Parameters.AddWithValue("@BillContact2Position", "")
                            Else
                                cmd.Parameters.AddWithValue("@BillContact2Position", r("OffContact1Position").ToString.ToUpper)
                            End If
                        Else
                            cmd.Parameters.AddWithValue("@BillContact2Position", r("BillContact2Position").ToString.ToUpper)
                        End If

                        If IsDBNull(r("BillContact2Tel")) Then
                            If IsDBNull(r("OffContact1Tel")) Then
                                cmd.Parameters.AddWithValue("@BillContact2Tel", "")
                            Else
                                cmd.Parameters.AddWithValue("@BillContact2Tel", r("OffContact1Tel").ToString.ToUpper)
                            End If
                        Else
                            cmd.Parameters.AddWithValue("@BillContact2Tel", r("BillContact2Tel").ToString.ToUpper)
                        End If

                        If IsDBNull(r("BillContact2Fax")) Then
                            If IsDBNull(r("OffContact1Fax")) Then
                                cmd.Parameters.AddWithValue("@BillContact2Fax", "")
                            Else
                                cmd.Parameters.AddWithValue("@BillContact2Fax", r("OffContact1Fax").ToString.ToUpper)
                            End If
                        Else
                            cmd.Parameters.AddWithValue("@BillContact2Fax", r("BillContact2Fax").ToString.ToUpper)
                        End If
                        If IsDBNull(r("BillContact2Tel2")) Then
                            If IsDBNull(r("OffContact1Tel2")) Then
                                cmd.Parameters.AddWithValue("@BillContact2Tel2", "")
                            Else
                                cmd.Parameters.AddWithValue("@BillContact2Tel2", r("OffContact1Tel2").ToString.ToUpper)
                            End If
                        Else
                            cmd.Parameters.AddWithValue("@BillContact2Tel2", r("BillContact2Tel2").ToString.ToUpper)
                        End If

                        If IsDBNull(r("BillContact2Mobile")) Then
                            If IsDBNull(r("OffContact1Mobile")) Then
                                cmd.Parameters.AddWithValue("@BillContact2Mobile", "")
                            Else
                                cmd.Parameters.AddWithValue("@BillContact2Mobile", r("OffContact1Mobile").ToString.ToUpper)
                            End If
                        Else
                            cmd.Parameters.AddWithValue("@BillContact2Mobile", r("BillContact2Mobile").ToString.ToUpper)
                        End If
                        If IsDBNull(r("BillContact2Email")) Then
                            If IsDBNull(r("ContactPersonEmail")) Then
                                cmd.Parameters.AddWithValue("@BillContact2Email", "")
                            Else
                                cmd.Parameters.AddWithValue("@BillContact2Email", r("ContactPersonEmail").ToString.ToUpper)
                            End If
                        Else
                            cmd.Parameters.AddWithValue("@BillContact2Email", r("BillContact2Email").ToString.ToUpper)
                        End If
                    Else
                        If IsDBNull(r("BillAddress1")) Then
                            Failure = Failure + 1
                            FailureString = FailureString + " " + AccountID + "(BILLINGADDRESS1_BLANK)"
                            drow("Status") = "Failed"
                            drow("Remarks") = "BILLINGADDRESS1_BLANK"
                            dtLog.Rows.Add(drow)
                            Continue For
                        Else
                            cmd.Parameters.AddWithValue("@BillAddress1", r("BillAddress1").ToString.ToUpper)
                        End If
                        If IsDBNull(r("Billbuilding")) Then
                            cmd.Parameters.AddWithValue("@Billbuilding", "")
                        Else
                            cmd.Parameters.AddWithValue("@Billbuilding", r("Billbuilding").ToString.ToUpper)
                        End If
                        If IsDBNull(r("BillStreet")) Then
                            cmd.Parameters.AddWithValue("@BillStreet", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillStreet", r("BillStreet").ToString.ToUpper)
                        End If

                        If IsDBNull(r("BillCity")) Then
                            cmd.Parameters.AddWithValue("@BillCity", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillCity", r("BillCity").ToString.ToUpper)
                        End If
                        If IsDBNull(r("BillState")) Then
                            cmd.Parameters.AddWithValue("@BillState", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillState", r("BillState").ToString.ToUpper)
                        End If
                        If IsDBNull(r("BillCountry")) Then
                            Failure = Failure + 1
                            FailureString = FailureString + " " + AccountID + "(BILLINGADDRESS-COUNTRY_BLANK)"
                            drow("Status") = "Failed"
                            drow("Remarks") = "BILLINGADDRESS-COUNTRY_BLANK"
                            dtLog.Rows.Add(drow)
                            Continue For
                        Else
                            cmd.Parameters.AddWithValue("@BillCountry", r("BillCountry").ToString.ToUpper)
                        End If
                        If IsDBNull(r("BillPostal")) Then
                            cmd.Parameters.AddWithValue("@BillPostal", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillPostal", r("BillPostal").ToString.ToUpper)
                        End If
                        If IsDBNull(r("BillContactPerson")) Then
                            Failure = Failure + 1
                            FailureString = FailureString + " " + AccountID + "(BILLING-CONTACTPERSON_BLANK)"
                            drow("Status") = "Failed"
                            drow("Remarks") = "BILLING-CONTACTPERSON_BLANK"
                            dtLog.Rows.Add(drow)
                            Continue For
                        Else
                            cmd.Parameters.AddWithValue("@BillContactPerson", r("BillContactPerson").ToString.ToUpper)
                        End If
                        If IsDBNull(r("BillContact1Position")) Then
                            cmd.Parameters.AddWithValue("@BillContact1Position", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillContact1Position", r("BillContact1Position").ToString.ToUpper)
                        End If

                        If IsDBNull(r("BillTelephone")) Then
                            cmd.Parameters.AddWithValue("@BillTelephone", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillTelephone", r("BillTelephone").ToString.ToUpper)
                        End If

                        If IsDBNull(r("BillFax")) Then
                            cmd.Parameters.AddWithValue("@BillFax", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillFax", r("BillFax").ToString.ToUpper)
                        End If

                        If IsDBNull(r("BillTelephone2")) Then
                            cmd.Parameters.AddWithValue("@BillTelephone2", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillTelephone2", r("BillTelephone2").ToString.ToUpper)
                        End If
                        If IsDBNull(r("BillMobile")) Then
                            cmd.Parameters.AddWithValue("@BillMobile", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillMobile", r("BillMobile").ToString.ToUpper)
                        End If
                        If IsDBNull(r("BillContact1Email")) Then
                            cmd.Parameters.AddWithValue("@BillContact1Email", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillContact1Email", r("BillContact1Email").ToString.ToUpper)
                        End If
                        If IsDBNull(r("BillContact2")) Then
                            cmd.Parameters.AddWithValue("@BillContact2", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillContact2", r("BillContact2").ToString.ToUpper)
                        End If

                        If IsDBNull(r("BillContact2Position")) Then
                            cmd.Parameters.AddWithValue("@BillContact2Position", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillContact2Position", r("BillContact2Position").ToString.ToUpper)
                        End If
                        If IsDBNull(r("BillContact2Tel")) Then
                            cmd.Parameters.AddWithValue("@BillContact2Tel", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillContact2Tel", r("BillContact2Tel").ToString.ToUpper)
                        End If
                        If IsDBNull(r("BillContact2Fax")) Then
                            cmd.Parameters.AddWithValue("@BillContact2Fax", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillContact2Fax", r("BillContact2Fax").ToString.ToUpper)
                        End If
                        If IsDBNull(r("BillContact2Tel2")) Then
                            cmd.Parameters.AddWithValue("@BillContact2Tel2", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillContact2Tel2", r("BillContact2Tel2").ToString.ToUpper)
                        End If
                        If IsDBNull(r("BillContact2Mobile")) Then
                            cmd.Parameters.AddWithValue("@BillContact2Mobile", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillContact2Mobile", r("BillContact2Mobile").ToString.ToUpper)
                        End If
                        If IsDBNull(r("BillContact2Email")) Then
                            cmd.Parameters.AddWithValue("@BillContact2Email", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillContact2Email", r("BillContact2Email").ToString.ToUpper)
                        End If
                    End If


                    If IsDBNull(r("CreditLimit")) Then
                        cmd.Parameters.AddWithValue("@ArLimit", 0.0)
                    Else
                        cmd.Parameters.AddWithValue("@ArLimit", r("CreditLimit"))
                    End If
                    If IsDBNull(r("CreditTerms")) Then
                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(CREDITTERMS_BLANK)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "CREDITTERMS_BLANK"
                        dtLog.Rows.Add(drow)
                        Continue For
                    Else
                        cmd.Parameters.AddWithValue("@ARTERM", r("CreditTerms").ToString.ToUpper)
                    End If
                    If IsDBNull(r("ARCurrency")) Then
                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(CURRENCY_BLANK)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "CURRENCY_BLANK"
                        dtLog.Rows.Add(drow)
                        Continue For
                    Else
                        cmd.Parameters.AddWithValue("@ARCurrency", r("ARCurrency").ToString.ToUpper)
                    End If
                    If IsDBNull(r("SendStatement")) Then
                        cmd.Parameters.AddWithValue("@SendStatement", 0)
                    ElseIf r("SendStatement").ToString.ToUpper = "TRUE" Then
                        cmd.Parameters.AddWithValue("@SendStatement", 1)
                    ElseIf r("SendStatement").ToString.ToUpper = "FALSE" Then
                        cmd.Parameters.AddWithValue("@SendStatement", 0)
                    End If

                    If IsDBNull(r("AutoEmailInvoice")) Then
                        cmd.Parameters.AddWithValue("@AutoEmailInvoice", 0)
                    ElseIf r("AutoEmailInvoice").ToString.ToUpper = "TRUE" Then
                        cmd.Parameters.AddWithValue("@AutoEmailInvoice", 1)
                    ElseIf r("AutoEmailInvoice").ToString.ToUpper = "FALSE" Then
                        cmd.Parameters.AddWithValue("@AutoEmailInvoice", 0)
                    End If

                    If IsDBNull(r("AutoEmailSOA")) Then
                        cmd.Parameters.AddWithValue("@AutoEmailSOA", 0)
                    ElseIf r("AutoEmailSOA").ToString.ToUpper = "TRUE" Then
                        cmd.Parameters.AddWithValue("@AutoEmailSOA", 1)
                    ElseIf r("AutoEmailSOA").ToString.ToUpper = "FALSE" Then
                        cmd.Parameters.AddWithValue("@AutoEmailSOA", 0)
                    End If
                    If IsDBNull(r("DefaultInvoiceFormat")) Then
                        cmd.Parameters.AddWithValue("@DefaultInvoiceFormat", "")
                    Else
                        cmd.Parameters.AddWithValue("@DefaultInvoiceFormat", r("DefaultInvoiceFormat"))
                    End If
                    If IsDBNull(r("Location")) Then
                        cmd.Parameters.AddWithValue("@Location", "")
                    Else
                        cmd.Parameters.AddWithValue("@Location", r("Location").ToString.ToUpper)
                    End If
                    cmd.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text + "_IMPORT")
                    cmd.Parameters.AddWithValue("@LastModifiedBy", txtCreatedBy.Text + "_IMPORT")


                    'If IsDBNull(r("CreatedBy")) Then
                    '    cmd.Parameters.AddWithValue("@CreatedBy", "EXCELIMPORT")

                    'Else
                    '    cmd.Parameters.AddWithValue("@CreatedBy", r("CreatedBy"))

                    'End If

                    cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))

                    'If IsDBNull(r("CreatedBy")) Then
                    '    cmd.Parameters.AddWithValue("@LastModifiedBy", "EXCELIMPORT")

                    'Else
                    '    cmd.Parameters.AddWithValue("@LastModifiedBy", r("CreatedBy"))

                    'End If
                    cmd.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                    cmd.Parameters.AddWithValue("@BillingSettings", "AccountID")



                    cmd.Parameters.AddWithValue("@Id", "")

                    cmd.Parameters.AddWithValue("@AddBlock", "")
                    cmd.Parameters.AddWithValue("@AddNos", "")
                    cmd.Parameters.AddWithValue("@AddFloor", "")
                    cmd.Parameters.AddWithValue("@AddUnit", "")

                    cmd.Parameters.AddWithValue("@BillBlock", "")
                    cmd.Parameters.AddWithValue("@BillNos", "")
                    cmd.Parameters.AddWithValue("@BillFloor", "")
                    cmd.Parameters.AddWithValue("@BillUnit", "")

                    cmd.Parameters.AddWithValue("@RocRegDate", DBNull.Value)

                    cmd.Parameters.AddWithValue("@AuthorizedCapital", 0)
                    cmd.Parameters.AddWithValue("@PaidupCapital", 0)
                    cmd.Parameters.AddWithValue("@CompanyType", "")


                    cmd.Parameters.AddWithValue("@FinanceCompanyId", "")
                    cmd.Parameters.AddWithValue("@FinanceCompany", "")

                    cmd.Parameters.AddWithValue("@ApLimit", 0)
                    cmd.Parameters.AddWithValue("@SalesLimit", 0)
                    cmd.Parameters.AddWithValue("@PurchaseLimit", 0)
                    cmd.Parameters.AddWithValue("@ApCurrency", "")
                    cmd.Parameters.AddWithValue("@GstRegistered", 0)


                    cmd.Parameters.AddWithValue("@Source", "")
                    cmd.Parameters.AddWithValue("@ARBal", 0)
                    cmd.Parameters.AddWithValue("@APBal", 0)
                    cmd.Parameters.AddWithValue("@Sales", 0)
                    cmd.Parameters.AddWithValue("@Purchase", 0)
                    cmd.Parameters.AddWithValue("@LocateGRP", "")


                    cmd.Parameters.AddWithValue("@SalesGRP", "")

                    cmd.Parameters.AddWithValue("@Dealer", "")
                    cmd.Parameters.AddWithValue("@LoginID", "")

                    cmd.Parameters.AddWithValue("@Password", "")
                    cmd.Parameters.AddWithValue("@WebLevel", "")

                    cmd.Parameters.AddWithValue("@APTERM", "")
                    cmd.Parameters.AddWithValue("@PriceGroup", "")


                    cmd.Parameters.AddWithValue("@Age0", 0)
                    cmd.Parameters.AddWithValue("@Age30", 0)
                    cmd.Parameters.AddWithValue("@Age60", 0)
                    cmd.Parameters.AddWithValue("@Age90", 0)
                    cmd.Parameters.AddWithValue("@Age120", 0)

                    cmd.Parameters.AddWithValue("@StopSalesYN", "")
                    cmd.Parameters.AddWithValue("@StopPurchYN", "")
                    cmd.Parameters.AddWithValue("@SpecCode", "")
                    cmd.Parameters.AddWithValue("@ArWarning", 0)

                    cmd.Parameters.AddWithValue("@LicenseNumber", 0)
                    cmd.Parameters.AddWithValue("@LicenseInfo", "")
                    cmd.Parameters.AddWithValue("@SalesGST", "")
                    cmd.Parameters.AddWithValue("@ArMethod", "")
                    cmd.Parameters.AddWithValue("@ApMethod", "")
                    cmd.Parameters.AddWithValue("@ProductM1", "")
                    cmd.Parameters.AddWithValue("@ProductM2", "")
                    cmd.Parameters.AddWithValue("@ProductM3", "")
                    cmd.Parameters.AddWithValue("@ProductM4", "")
                    cmd.Parameters.AddWithValue("@ProductF1", "")
                    cmd.Parameters.AddWithValue("@ProductF2", "")
                    cmd.Parameters.AddWithValue("@ProductF3", "")
                    cmd.Parameters.AddWithValue("@ProductF4", "")
                    cmd.Parameters.AddWithValue("@RentalTerm", "")


                    cmd.Parameters.AddWithValue("@Donor", 0)
                    cmd.Parameters.AddWithValue("@Member", 0)
                    cmd.Parameters.AddWithValue("@MemberType", "")
                    cmd.Parameters.AddWithValue("@MemberID", "")
                    cmd.Parameters.AddWithValue("@GIROID", "")
                    cmd.Parameters.AddWithValue("@DateJoin", DBNull.Value)
                    cmd.Parameters.AddWithValue("@DateExpired", DBNull.Value)
                    cmd.Parameters.AddWithValue("@DateTerminate", DBNull.Value)
                    cmd.Parameters.AddWithValue("@TemplateNo", "")
                    cmd.Parameters.AddWithValue("@ARLedger", "")
                    cmd.Parameters.AddWithValue("@ARSubLedger", "")
                    cmd.Parameters.AddWithValue("@APLedger", "")
                    cmd.Parameters.AddWithValue("@APSubLedger", "")

                    cmd.Parameters.AddWithValue("@SrcCompID", "")
                    cmd.Parameters.AddWithValue("@DiscountPct", 0)
                    cmd.Parameters.AddWithValue("@PreferredCustYN", "")
                    cmd.Parameters.AddWithValue("@ChkGstInclusive", "")
                    cmd.Parameters.AddWithValue("@Reason", "")
                    cmd.Parameters.AddWithValue("@Boardmember", "")
                    cmd.Parameters.AddWithValue("@BoardDesignation", "")
                    cmd.Parameters.AddWithValue("@period", "")
                    cmd.Parameters.AddWithValue("@Intriducer", "")
                    cmd.Parameters.AddWithValue("@Organization", "")
                    cmd.Parameters.AddWithValue("@chkLetterIndemnity", 0)
                    cmd.Parameters.AddWithValue("@LetterIndemnitySignedBy", "")
                    cmd.Parameters.AddWithValue("@LeterDate", DBNull.Value)

                    cmd.Parameters.AddWithValue("@WebLoginID", "")
                    cmd.Parameters.AddWithValue("@WebLoginPassWord", "")
                    cmd.Parameters.AddWithValue("@WebAccessLevel", 0)
                    cmd.Parameters.AddWithValue("@WebOneTimePassWord", "")


                    cmd.Parameters.AddWithValue("@WebGroupDealer", 0)
                    cmd.Parameters.AddWithValue("@WebDisable", 0)
                    cmd.Parameters.AddWithValue("@WebID", "")
                    cmd.Parameters.AddWithValue("@OTPMobile", "")
                    cmd.Parameters.AddWithValue("@OTPYN", 0)
                    cmd.Parameters.AddWithValue("@OTPGenerateDate", DBNull.Value)
                    cmd.Parameters.AddWithValue("@HideInStock", 0)
                    cmd.Parameters.AddWithValue("@OverdueDaysLimit", 0)
                    cmd.Parameters.AddWithValue("@OverdueDaysLimitActive", 0)
                    cmd.Parameters.AddWithValue("@OverdueDaysWarning", 0)
                    cmd.Parameters.AddWithValue("@OverdueDaysWarningActive", 0)
                    cmd.Parameters.AddWithValue("@chkAR", 0)
                    cmd.Parameters.AddWithValue("@DueDaysStopFreq", "")
                    cmd.Parameters.AddWithValue("@SubCompanyNo", "")
                    cmd.Parameters.AddWithValue("@SourceCompany", "")
                    cmd.Parameters.AddWithValue("@chkSendServiceReport", 0)

                    cmd.Parameters.AddWithValue("@SoPriceGroup", "")
                    cmd.Parameters.AddWithValue("@POPrefix", "")
                    cmd.Parameters.AddWithValue("@PONumber", 0)
                    cmd.Parameters.AddWithValue("@LastStatus", "")
                    cmd.Parameters.AddWithValue("@OverdueMonthWarning", 0)
                    cmd.Parameters.AddWithValue("@OverDueMonthLimit", 0)
                    cmd.Parameters.AddWithValue("@AccountNo", "")
                    cmd.Parameters.AddWithValue("@FlowFrom", "")
                    cmd.Parameters.AddWithValue("@FlowTo", "")

                    cmd.Parameters.AddWithValue("@ShippingTerm", "")
                    cmd.Parameters.AddWithValue("@InterCompany", 0)
                    cmd.Parameters.AddWithValue("@AutoEmailServ", 0)
                    cmd.Parameters.AddWithValue("@ReportFormatServ", "")
                    cmd.Parameters.AddWithValue("@WebUploadDate", DBNull.Value)
                    cmd.Parameters.AddWithValue("@IsCustomer", 0)

                    cmd.Parameters.AddWithValue("@IsSupplier", 0)

                    cmd.Parameters.AddWithValue("@PaxBased", 0)
                    cmd.Parameters.AddWithValue("@BillMonthly", 0)
                    cmd.Parameters.AddWithValue("@DiscType", "")
                    cmd.Parameters.AddWithValue("@ARPDFFromat", "")
                    cmd.Parameters.AddWithValue("@EmailConsolidate", 0)
                    cmd.Parameters.AddWithValue("@TermsDay", 0)


                    'cmd.Parameters.AddWithValue("@SmartCustomer", chkSmartCustomer.Checked)

                    cmd.Connection = conn

                    cmd.ExecuteNonQuery()
                    cmd.Dispose()

                    Success = Success + 1
                    drow("Status") = "Success"
                    drow("Remarks") = ""
                    dtLog.Rows.Add(drow)
                End If
                ' InsertIntoTblWebEventLog("InserData2", Success.ToString, Failure.ToString)

            Next

            txtSuccessCount.Text = Success.ToString
            txtFailureCount.Text = Failure.ToString
            txtFailureString.Text = FailureString

            GridView1.DataSource = dtLog
            GridView1.DataBind()

            dt.Clear()


            Return True
        Catch ex As Exception
            txtSuccessCount.Text = Success.ToString
            txtFailureCount.Text = Failure.ToString
            txtFailureString.Text = FailureString
            lblAlert.Text = ex.Message.ToString


            InsertIntoTblWebEventLog("COMPANY-EXCEL", "InsertCorporateData", ex.Message.ToString, txtCreatedBy.Text)

            Return False

        End Try
    End Function

    Private Function Excel_To_DataTable(ByVal pRutaArchivo As String, ByVal pHojaIndex As Integer) As DataTable
        Dim Tabla As DataTable = Nothing

        Try

            If System.IO.File.Exists(pRutaArchivo) Then
                Dim workbook As IWorkbook = Nothing
                Dim worksheet As ISheet = Nothing
                Dim first_sheet_name As String = ""

                Using FS As FileStream = New FileStream(pRutaArchivo, FileMode.Open, FileAccess.Read)
                    workbook = WorkbookFactory.Create(FS)
                    worksheet = workbook.GetSheetAt(pHojaIndex)
                    first_sheet_name = worksheet.SheetName

                    '   If worksheet.SheetName.ToUpper = rdbModule.SelectedValue.ToString.ToUpper Then
                    '  InsertIntoTblWebEventLog("TestExcel0", worksheet.SheetName.ToUpper + worksheet.LastRowNum.ToString + "Aa", "")


                    If worksheet.LastRowNum > 2 Then

                        Tabla = New DataTable(first_sheet_name)
                        Tabla.Rows.Clear()
                        Tabla.Columns.Clear()

                        For rowIndex As Integer = 0 To worksheet.LastRowNum

                            Dim NewReg As DataRow = Nothing
                            Dim row As IRow = worksheet.GetRow(rowIndex)
                            Dim row2 As IRow = Nothing
                            Dim row3 As IRow = Nothing

                            If rowIndex = 0 Then
                                row2 = worksheet.GetRow(rowIndex + 2)
                                row3 = worksheet.GetRow(rowIndex + 3)
                            End If
                            '    InsertIntoTblWebEventLog("TestExcel1", rowIndex.ToString, worksheet.LastRowNum.ToString)

                            If row IsNot Nothing Then
                                If rowIndex > 1 Then NewReg = Tabla.NewRow()
                                Dim colIndex As Integer = 0

                                For Each cell As ICell In row.Cells
                                    Dim valorCell As Object = Nothing
                                    Dim cellType As String = ""
                                    Dim cellType2 As String() = New String(1) {}
                                    If rowIndex = 1 Then

                                    ElseIf rowIndex = 0 Then

                                        For i As Integer = 0 To 2 - 1
                                            Dim cell2 As ICell = Nothing

                                            If i = 0 Then
                                                cell2 = row2.GetCell(cell.ColumnIndex)
                                            Else
                                                cell2 = row3.GetCell(cell.ColumnIndex)
                                            End If
                                            '   InsertIntoTblWebEventLog("TestExcel5", cell.ColumnIndex.ToString, i.ToString)

                                            If cell2 IsNot Nothing Then

                                                Select Case cell2.CellType
                                                    Case NPOI.SS.UserModel.CellType.Blank
                                                        'cellType2(i) = "System.String"
                                                    Case NPOI.SS.UserModel.CellType.Boolean
                                                        cellType2(i) = "System.Boolean"
                                                    Case NPOI.SS.UserModel.CellType.String
                                                        cellType2(i) = "System.String"
                                                    Case NPOI.SS.UserModel.CellType.Numeric

                                                        If HSSFDateUtil.IsCellDateFormatted(cell2) Then
                                                            cellType2(i) = "System.DateTime"
                                                        Else
                                                            cellType2(i) = "System.Double"
                                                        End If

                                                    Case NPOI.SS.UserModel.CellType.Formula
                                                        Dim continuar As Boolean = True

                                                        Select Case cell2.CachedFormulaResultType
                                                            Case NPOI.SS.UserModel.CellType.Boolean
                                                                cellType2(i) = "System.Boolean"
                                                            Case NPOI.SS.UserModel.CellType.String
                                                                cellType2(i) = "System.String"
                                                            Case NPOI.SS.UserModel.CellType.Numeric

                                                                If HSSFDateUtil.IsCellDateFormatted(cell2) Then
                                                                    cellType2(i) = "System.DateTime"
                                                                Else

                                                                    Try

                                                                        If cell2.CellFormula = "TRUE()" Then
                                                                            cellType2(i) = "System.Boolean"
                                                                            continuar = False
                                                                        End If

                                                                        If continuar AndAlso cell2.CellFormula = "FALSE()" Then
                                                                            cellType2(i) = "System.Boolean"
                                                                            continuar = False
                                                                        End If

                                                                        If continuar Then
                                                                            cellType2(i) = "System.Double"
                                                                            continuar = False
                                                                        End If

                                                                    Catch
                                                                    End Try
                                                                End If
                                                        End Select

                                                    Case Else
                                                        cellType2(i) = "System.String"
                                                End Select
                                            Else
                                                cellType2(i) = "System.String"
                                            End If
                                            '  InsertIntoTblWebEventLog("TestExcel4", "", cellType2(i).ToString)

                                        Next

                                        If cellType2(0) = cellType2(1) Then
                                            cellType = cellType2(0)
                                        Else
                                            If cellType2(0) Is Nothing Then cellType = cellType2(1)
                                            If cellType2(1) Is Nothing Then cellType = cellType2(0)
                                            If cellType = "" Then cellType = "System.String"
                                        End If

                                        Dim colName As String = "Column_{0}"

                                        Try
                                            colName = cell.StringCellValue
                                        Catch
                                            colName = String.Format(colName, colIndex)
                                        End Try
                                        ' InsertIntoTblWebEventLog("TestExcel2", colName, colIndex)


                                        For Each col As DataColumn In Tabla.Columns
                                            If col.ColumnName = colName Then colName = String.Format("{0}_{1}", colName, colIndex)
                                        Next
                                        '   InsertIntoTblWebEventLog("TestExcel3", colName, cellType)

                                        Dim codigo As DataColumn = New DataColumn(colName, System.Type.[GetType](cellType))
                                        Tabla.Columns.Add(codigo)
                                        colIndex += 1
                                    Else

                                        Select Case cell.CellType
                                            Case NPOI.SS.UserModel.CellType.Blank
                                                valorCell = DBNull.Value
                                            Case NPOI.SS.UserModel.CellType.Boolean
                                                'Select Case cell.BooleanCellValue
                                                '    Case True
                                                valorCell = cell.BooleanCellValue
                                                '    Case False
                                                '        valorCell = cell.BooleanCellValue
                                                '    Case Else
                                                '        valorCell = False

                                                'End Select

                                            Case NPOI.SS.UserModel.CellType.String
                                                valorCell = cell.StringCellValue
                                            Case NPOI.SS.UserModel.CellType.Numeric

                                                If HSSFDateUtil.IsCellDateFormatted(cell) Then
                                                    valorCell = cell.DateCellValue
                                                Else
                                                    valorCell = cell.NumericCellValue
                                                End If

                                            Case NPOI.SS.UserModel.CellType.Formula

                                                Select Case cell.CachedFormulaResultType
                                                    Case NPOI.SS.UserModel.CellType.Blank
                                                        valorCell = DBNull.Value
                                                    Case NPOI.SS.UserModel.CellType.String
                                                        valorCell = cell.StringCellValue
                                                    Case NPOI.SS.UserModel.CellType.Boolean
                                                        valorCell = cell.BooleanCellValue
                                                    Case NPOI.SS.UserModel.CellType.Numeric

                                                        If HSSFDateUtil.IsCellDateFormatted(cell) Then
                                                            valorCell = cell.DateCellValue
                                                        Else
                                                            valorCell = cell.NumericCellValue
                                                        End If
                                                End Select

                                            Case Else
                                                valorCell = cell.StringCellValue
                                        End Select
                                        '   InsertIntoTblWebEventLog("TestExcel6", cell.ColumnIndex.ToString + " " + cell.CellType.ToString, valorCell.ToString)

                                        'If cell.CellType.ToString = "Boolean" Then
                                        '    If valorCell.ToString = "TRUE" Or valorCell.ToString = "FALSE" Then

                                        '    Else
                                        '        Continue For

                                        '    End If
                                        'End If

                                        If cell.ColumnIndex <= Tabla.Columns.Count - 1 Then NewReg(cell.ColumnIndex) = valorCell
                                    End If
                                Next
                            End If

                            If rowIndex > 1 Then Tabla.Rows.Add(NewReg)
                        Next


                        Tabla.AcceptChanges()
                    Else
                        lblMessage.Text = "Please import more than one Account details to proceed."
                    End If
                    'Else
                    'lblMessage.Text = "Sheet Name does not match with the selected template."
                    'End If

                End Using
            Else
                Throw New Exception("ERROR 404")
            End If

        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString

            InsertIntoTblWebEventLog("COMPANY-EXCEL", "Excel_To_DataTable", ex.Message.ToString, txtCreatedBy.Text)
            Throw ex

        End Try

        Return Tabla
    End Function

    Private Function CheckIndustryExists(Industry As String, conn As MySqlConnection) As Boolean
        Dim cmd As MySqlCommand = New MySqlCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT EXISTS(SELECT Industry FROM tblIndustry where Industry =@Industry)"
        cmd.Connection = conn
        cmd.Parameters.Clear()
        cmd.Parameters.AddWithValue("@Industry", Industry)
        Return cmd.ExecuteScalar
    End Function

    Private Function CheckCityExists(City As String, conn As MySqlConnection) As Boolean
        Dim cmd As MySqlCommand = New MySqlCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT EXISTS(SELECT city FROM tblcity where city =@city)"
        cmd.Connection = conn
        cmd.Parameters.Clear()
        cmd.Parameters.AddWithValue("@city", City)
        Return cmd.ExecuteScalar
    End Function

    Private Function CheckStateExists(State As String, conn As MySqlConnection) As Boolean
        Dim cmd As MySqlCommand = New MySqlCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT EXISTS(SELECT State FROM tblState where State =@State)"
        cmd.Connection = conn
        cmd.Parameters.Clear()
        cmd.Parameters.AddWithValue("@State", State)
        Return cmd.ExecuteScalar
    End Function

    Private Function CheckCountryExists(Country As String, conn As MySqlConnection) As Boolean
        Dim cmd As MySqlCommand = New MySqlCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT EXISTS(SELECT Country FROM tblCountry where Country =@Country)"
        cmd.Connection = conn
        cmd.Parameters.Clear()
        cmd.Parameters.AddWithValue("@Country", Country)
        Return cmd.ExecuteScalar
    End Function

    Private Function CheckTermsExists(Terms As String, conn As MySqlConnection) As Boolean
        Dim cmd As MySqlCommand = New MySqlCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT EXISTS(SELECT Terms FROM tblTerms where Terms =@Terms)"
        cmd.Connection = conn
        cmd.Parameters.Clear()
        cmd.Parameters.AddWithValue("@Terms", Terms)
        Return cmd.ExecuteScalar
    End Function

    Private Function CheckCurrencyExists(Currency As String, conn As MySqlConnection) As Boolean
        Dim cmd As MySqlCommand = New MySqlCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT EXISTS(SELECT Currency FROM tblCurrency where Currency =@Currency)"
        cmd.Connection = conn
        cmd.Parameters.Clear()
        cmd.Parameters.AddWithValue("@Currency", Currency)
        Return cmd.ExecuteScalar
    End Function

    Private Function CheckCompanyGroupExists(CompanyGroup As String, conn As MySqlConnection) As Boolean
        Dim cmd As MySqlCommand = New MySqlCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT EXISTS(SELECT CompanyGroup FROM tblCompanyGroup where CompanyGroup =@CompanyGroup)"
        cmd.Connection = conn
        cmd.Parameters.Clear()
        cmd.Parameters.AddWithValue("@CompanyGroup", CompanyGroup)
        Return cmd.ExecuteScalar
    End Function

    Private Function CheckContractGroupExists(ContractGroup As String, conn As MySqlConnection) As Boolean
        Dim cmd As MySqlCommand = New MySqlCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT EXISTS(SELECT ContractGroup FROM tblContractGroup where ContractGroup =@ContractGroup)"
        cmd.Connection = conn
        cmd.Parameters.Clear()
        cmd.Parameters.AddWithValue("@ContractGroup", ContractGroup)
        Return cmd.ExecuteScalar
    End Function

    Private Function CheckLocationGroupExists(LocationGroup As String, conn As MySqlConnection) As Boolean
        Dim cmd As MySqlCommand = New MySqlCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT EXISTS(SELECT LocationGroup FROM tblLocationGroup where LocationGroup =@LocationGroup)"
        cmd.Connection = conn
        cmd.Parameters.Clear()
        cmd.Parameters.AddWithValue("@LocationGroup", LocationGroup)
        Return cmd.ExecuteScalar
    End Function

    Private Function CheckInchargeIDExists(InchargeID As String, conn As MySqlConnection) As Boolean
        Dim cmd As MySqlCommand = New MySqlCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT EXISTS(SELECT distinct (StaffID) FROM tblStaff where Roles='TECHNICAL' and Status = 'O' AND STAFFID=@InchargeID)"
        cmd.Connection = conn
        cmd.Parameters.Clear()
        cmd.Parameters.AddWithValue("@InchargeID", InchargeID)
        Return cmd.ExecuteScalar
    End Function

    Private Function CheckSalesmanExists(Salesman As String, conn As MySqlConnection) As Boolean
        Dim cmd As MySqlCommand = New MySqlCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT EXISTS(SELECT distinct (StaffID) FROM tblStaff where Roles='SALES MAN' and Status = 'O' AND STAFFID=@Salesman)"
        cmd.Connection = conn
        cmd.Parameters.Clear()
        cmd.Parameters.AddWithValue("@Salesman", Salesman)
        Return cmd.ExecuteScalar
    End Function

    Private Function CheckInvoiceFormatExists(InvoiceFormat As String, conn As MySqlConnection) As Boolean
        Select Case InvoiceFormat
            Case "Format1"
                Return True
            Case "Format2"
                Return True
            Case "Format3"
                Return True
            Case "Format4"
                Return True
            Case "Format5"
                Return True
            Case "Format6"
                Return True
            Case "Format7"
                Return True
            Case "Format8"
                Return True
            Case Else
                Return False
        End Select

    End Function

    Private Function CheckNationalityExists(InvoiceFormat As String, conn As MySqlConnection) As Boolean
        Select Case InvoiceFormat
            Case "SINGAPOREAN"
                Return True
            Case "MALAYSIAN"
                Return True
            Case "INDIAN"
                Return True
            Case "INDONESIAN"
                Return True
            Case "CHINESE"
                Return True
            Case "PHILIPPINE"
                Return True
            Case "SOUTH KOREAN"
                Return True
            Case "OTHERS"
                Return True
            Case Else
                Return False
        End Select

    End Function

    Private Function CheckCorporateLocationDuplicate(conn As MySqlConnection, AccountID As String, addr As String, ContractGroup As String, CompanyGroup As String) As Boolean

        Dim commandLoc As MySqlCommand = New MySqlCommand

        commandLoc.CommandType = CommandType.Text

        commandLoc.CommandText = "SELECT * FROM tblcompanylocation where accountid=@id"
        commandLoc.Parameters.AddWithValue("@id", AccountID)
        commandLoc.Connection = conn

        Dim drLoc As MySqlDataReader = commandLoc.ExecuteReader()
        Dim dtLoc As New System.Data.DataTable
        dtLoc.Load(drLoc)

        '   Dim addr As String = r("Address1").ToString.Trim + " " + r("AddStreet").ToString.Trim + " " + r("AddBuilding").ToString.Trim
        Dim dataaddr As String
        If dtLoc.Rows.Count > 0 Then
            For i As Integer = 0 To dtLoc.Rows.Count - 1

                dataaddr = dtLoc.Rows(i)("Address1").ToString.Trim + " " + dtLoc.Rows(i)("AddStreet").ToString.Trim + " " + dtLoc.Rows(i)("AddBuilding").ToString.Trim

                If addr = dataaddr And dtLoc.Rows(i)("ContractGroup").ToString.Trim = ContractGroup And dtLoc.Rows(i)("CompanyGroupD").ToString.Trim = CompanyGroup Then
                    Return False
                End If
            Next

        End If
        Return True
    End Function

    Private Function CheckResidentialLocationDuplicate(conn As MySqlConnection, AccountID As String, addr As String, ContractGroup As String, PersonGroup As String) As Boolean

        Dim commandLoc As MySqlCommand = New MySqlCommand

        commandLoc.CommandType = CommandType.Text

        commandLoc.CommandText = "SELECT * FROM tblpersonlocation where accountid=@id"
        commandLoc.Parameters.AddWithValue("@id", AccountID)
        commandLoc.Connection = conn

        Dim drLoc As MySqlDataReader = commandLoc.ExecuteReader()
        Dim dtLoc As New System.Data.DataTable
        dtLoc.Load(drLoc)

        '   Dim addr As String = r("Address1").ToString.Trim + " " + r("AddStreet").ToString.Trim + " " + r("AddBuilding").ToString.Trim
        Dim dataaddr As String
        If dtLoc.Rows.Count > 0 Then
            For i As Integer = 0 To dtLoc.Rows.Count - 1

                dataaddr = dtLoc.Rows(i)("Address1").ToString.Trim + " " + dtLoc.Rows(i)("AddStreet").ToString.Trim + " " + dtLoc.Rows(i)("AddBuilding").ToString.Trim

                If addr = dataaddr And dtLoc.Rows(i)("ContractGroup").ToString.Trim = ContractGroup And dtLoc.Rows(i)("PersonGroupD").ToString.Trim = PersonGroup Then
                    Return False
                End If
            Next

        End If
        Return True
    End Function

    Protected Function GenerateLocationID(AccountID As String, conn As MySqlConnection) As Int32
        Dim lastnum As Int32 = 0

        Try
            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.CommandText = "SELECT locationid,locationprefix,locationno FROM tblpersonlocation where accountid='" & AccountID & "' order by locationno desc;"
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New System.Data.DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then
                lastnum = Convert.ToInt64(dt.Rows(0)("locationno"))
                lastnum = lastnum + 1
                Return lastnum
            Else
                lastnum = 1
                Return lastnum
            End If

        Catch ex As Exception
            Return lastnum

            InsertIntoTblWebEventLog("COMPANY", "GenerateLocationID", ex.Message.ToString, AccountID)

        End Try
    End Function

    Protected Sub btnExcelUpload_Click(sender As Object, e As EventArgs) Handles btnExcelUpload.Click
        lblAlert.Text = ""
        lblMessage.Text = ""

        Dim ofilename As String = ""
        Dim sfilename As String = ""

        ofilename = Path.GetFileName(FileUpload1.PostedFile.FileName)
        Dim ext As String = Path.GetExtension(ofilename)
        sfilename = ofilename.Split("."c)(0)

        Dim folderPath As String = Server.MapPath("~/Uploads/Excel/")
        If Not Directory.Exists(folderPath) Then
            Directory.CreateDirectory(folderPath)
        End If
        Dim fileName As String = folderPath + sfilename + "_" + DateTime.Now.ToString("yyyyMMddhhmm") + "_" + txtCreatedBy.Text + ext

        If System.IO.File.Exists(fileName) Then

            System.IO.File.Delete(fileName)
        End If
        'Save the File to the Directory (Folder).
        FileUpload1.PostedFile.SaveAs(fileName)

        txtWorkBookName.Text = sfilename
        Dim file As FileStream = New FileStream(fileName, FileMode.Open, FileAccess.Read)
        Dim dropdownworkbook = New XSSFWorkbook(file)
        file.Close()
        file.Dispose()

        Dim dt As New DataTable
        dt = Excel_To_DataTable(fileName, 0)
        ' Response.Write(dt.Rows.Count.ToString)
        If dt Is Nothing Then
            lblAlert.Text = "DATA NOT IMPORTED"
        Else
            Dim res As Boolean = True


            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()
            Dim count As String = dt.Rows.Count.ToString

            'If rdbModule.SelectedValue.ToString = "Corporate" Then
            InsertCorporateData(dt, conn)
            '    ElseIf rdbModule.SelectedValue.ToString = "Residential" Then
            '    InsertResidentialData(dt, conn)
            '    ElseIf rdbModule.SelectedValue.ToString = "CorporateLocation" Then
            '    InsertCorporateLocationData(dt, conn)
            '    ElseIf rdbModule.SelectedValue.ToString = "ResidentialLocation" Then
            '    InsertResidentialLocationData(dt, conn)
            'End If
            lblMessage.Text = "Total Records Imported : " + count + "<br>" + " Success : " + txtSuccessCount.Text + ", Failure : " + txtFailureCount.Text '+ " Failed AccountID : " + txtFailureString.Text
            If GridView1.Rows.Count > 0 Then
                '  btnImportExportToExcel.Visible = True

            End If


            conn.Close()
            conn.Dispose()

        End If

        'dt.Clear()
        'dt.Dispose()
        ' Session("Filename") = fileName
    End Sub

    Protected Sub btnCorporateTemplate_Click(sender As Object, e As ImageClickEventArgs) Handles btnCorporateTemplate.Click

    End Sub

    Protected Sub btnUpdateStatusLoc_Click(sender As Object, e As EventArgs) Handles btnUpdateStatusLoc.Click
        If String.IsNullOrEmpty(txtStatusLocReason.Text) Then
            lblAlertStatusLoc.Text = "ENTER REASON FOR CHANGE OF STATUS"
            mdlPopupStatusLoc.Show()
            Return

        End If

        If chkStatusLoc.Checked = True Then
            If ddlNewStatus.SelectedValue = 1 Then
                lblAlertStatusLoc.Text = "CHOOSE A DIFFERENT STATUS"
                mdlPopupStatusLoc.Show()
                Return
            End If
        Else
            If ddlNewStatus.SelectedValue = 0 Then
                lblAlertStatusLoc.Text = "CHOOSE A DIFFERENT STATUS"
                mdlPopupStatusLoc.Show()
                Return
            End If
        End If

        Try
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim command As MySqlCommand = New MySqlCommand

            command.CommandType = CommandType.Text

            '   If chkStatusLoc.Checked = True Then
            If ddlNewStatus.SelectedValue = 0 Then
                command.CommandText = "UPDATE tblcompanylocation SET Status= 0,StatusReason=@Reason where rcno=" & Convert.ToInt32(txtSvcRcno.Text)
                command.Parameters.Clear()
                command.Parameters.AddWithValue("@Reason", txtStatusLocReason.Text)

                '  ElseIf chkStatusLoc.Checked = False Then
            ElseIf ddlNewStatus.SelectedValue = 1 Then
                command.CommandText = "UPDATE tblcompanyLOCATION SET Status= 1,StatusReason=@Reason where rcno=" & Convert.ToInt32(txtSvcRcno.Text)
                command.Parameters.Clear()
                command.Parameters.AddWithValue("@Reason", txtStatusLocReason.Text)

            End If

            command.Connection = conn
            command.ExecuteNonQuery()

            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            '     If chkStatusLoc.Checked = True Then
            If ddlNewStatus.SelectedValue = 0 Then
                command1.CommandText = "UPDATE tblcustomerportaluseraccesslocation SET Status= 0 where locationid='" & txtLocationID.Text & "'"
            ElseIf ddlNewStatus.SelectedValue = 1 Then
                '   ElseIf chkStatusLoc.Checked = False Then

                command1.CommandText = "UPDATE tblcustomerportaluseraccesslocation SET Status= 1 where locationid='" & txtLocationID.Text & "'"

            End If

            command1.Connection = conn
            command1.ExecuteNonQuery()

            conn.Close()
            conn.Dispose()
            command.Dispose()
            'ddlStatus.Text = ddlNewStatus.Text
            '  ddlNewStatus.SelectedIndex = 0

            lblMessage.Text = "ACTION: STATUS UPDATED"

            CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CHST", txtLocationID.Text, "CHST", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")), 0, 0, 0, txtLocationID.Text, txtStatusRemarks.Text.ToUpper, txtSvcRcno.Text)
            'txtPostStatus.Text = ddlNewStatus.SelectedValue

            '   SqlDataSource2.SelectCommand = txt.Text
            If ddlNewStatus.Text = "Active" Then
                chkStatusLoc.Checked = True
            Else
                chkStatusLoc.Checked = False
            End If

            If txtDisplayRecordsLocationwise.Text = "Y" Then
                If String.IsNullOrEmpty(txtAccountID.Text) Then
                    SqlDataSource2.SelectCommand = "SELECT * FROM tblcompanylocation where personid = '" & txtClientID.Text & "'"
                Else
                    SqlDataSource2.SelectCommand = "SELECT * FROM tblcompanylocation where accountid = '" & txtAccountID.Text & "' and location in (Select LocationID  from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "')"
                End If
            End If

            SqlDataSource2.DataBind()
            GridView2.DataBind()
            'InsertNewLog()

            mdlPopupStatusLoc.Hide()
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "btnConfirmYes_Click", ex.Message.ToString, txtLocationID.Text)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Protected Sub btnChangeStatusLoc_Click(sender As Object, e As EventArgs) Handles btnChangeStatusLoc.Click
        lblStatusLocationID.Text = txtLocationID.Text
        If chkStatusLoc.Checked Then
            ddlNewStatus.SelectedValue = 1
        Else
            ddlNewStatus.SelectedValue = 0
        End If
        txtStatusLocReason.Text = ""
        lblAlertStatusLoc.Text = ""

        mdlPopupStatusLoc.Show()

    End Sub

    Protected Sub btnEmailReminder_Click(sender As Object, e As EventArgs) Handles btnEmailReminder.Click
        txtAccountIDReminder.Text = txtAccountID.Text
        txtCustNameReminder.Text = txtNameE.Text
        txtCutOffDateReminder.Text = Convert.ToDateTime(Now).ToString("dd/MM/yyyy")
      
        Dim connAgeing As MySqlConnection = New MySqlConnection()

        connAgeing.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        connAgeing.Open()

        Dim commandAgeing As MySqlCommand = New MySqlCommand

        commandAgeing.CommandType = CommandType.Text
        commandAgeing.CommandText = "SELECT sum(UnPaidBalance) as Balance FROM tbwcompanybal where AccountId='" & txtAccountID.Text & "'"
        commandAgeing.Connection = connAgeing

        Dim drAgeing As MySqlDataReader = commandAgeing.ExecuteReader()
        Dim dtAgeing As New DataTable
        dtAgeing.Load(drAgeing)


        If dtAgeing.Rows.Count > 0 Then

            If Convert.IsDBNull(dtAgeing.Rows(0)("Balance")) = True Then
                txtAmtReminder.Text = "0.00"
            Else
                txtAmtReminder.Text = dtAgeing.Rows(0)("Balance").ToString
            End If

        End If

        connAgeing.Close()
        connAgeing.Dispose()
        commandAgeing.Dispose()
        dtAgeing.Dispose()

        Session.Add("ReminderAccountID", txtAccountID.Text)
        Session.Add("ReminderAmt", txtAmtReminder.Text)

        mdlPopupReminder.Show()

    End Sub

    Protected Sub txtAmtReminder_TextChanged(sender As Object, e As EventArgs) Handles txtAmtReminder.TextChanged
        Session.Add("ReminderAmt", txtAmtReminder.Text)

    End Sub
  
    Protected Sub gvTransferFiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvTransferFiles.SelectedIndexChanged
        Dim editindex As Integer
        editindex = gvTransferFiles.SelectedIndex
        rcno = DirectCast(gvTransferFiles.Rows(editindex).FindControl("Label1"), Label).Text
        txtTransferRcno.Text = rcno.ToString()

        Dim filePath As String = CType(sender, LinkButton).CommandArgument
        txtTransferFileName.Text = filePath


    End Sub


    Protected Sub btnTransferFiles_Click(sender As Object, e As EventArgs) Handles btnTransferFiles.Click
        Try
            lblMessage.Text = ""
            lblAlert.Text = ""
            If String.IsNullOrEmpty(txtAccountID.Text) Then
                lblAlert.Text = "SELECT ACCOUNT TO UPLOAD FILE"
                Return
            End If
            SqlDSTransferUpload.SelectCommand = "select * from tblfileupload where fileref = '" + txtAccountID.Text + "'"
            gvTransferFiles.DataSourceID = "SqlDSTransferUpload"
            gvTransferFiles.DataBind()


            txtTransferAccountFrom.Text = txtAccountID.Text
            mdlTransferFiles.Show()

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btnPopUpClientSearch_Click(sender As Object, e As EventArgs)
        mdlPopupTIN.Show()

    End Sub

    Protected Sub gvClientSearch_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvClientSearch.Sorting
        Dim qry As String = ""
        qry = "SELECT AccountID,Name,ROCNos,TaxIdentificationNo From tblCOMPANY where rcno <>0 "

        If String.IsNullOrEmpty(txtNameE.Text) = False Then
            qry = qry + " and Name like '%" & txtNameE.Text & "%'"
        End If
        If String.IsNullOrEmpty(txtRegNo.Text) = False Then
            qry = qry + " and RocNos = '" & txtRegNo.Text & "'"
        End If
        qry = qry + " order by Name;"
        SqlDSClientSearch.SelectCommand = qry
        SqlDSClientSearch.DataBind()
        gvClientSearch.DataBind()
        mdlPopupTIN.Show()
    End Sub

    Protected Sub gvClientSearch_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvClientSearch.PageIndexChanging
        Try
            If txtPpClientSearch.Text.Trim = "" Then

                SqlDSClientSearch.SelectCommand = "SELECT AccountID,Name,ROCNos,TaxIdentificationNo From tblCOMPANY where rcno <>0 order by TaxIdentificationNo"
                'SqlDSClientSearch.SelectCommand = "SELECT id,name,accountid,contactperson FROM tblcompany union SELECT id,name,accountid,contactperson FROM tblperson"
                '  SqlDSClientSearch.SelectCommand = "SELECT B.CompanyID as ID,A.name,A.accountid,A.contactperson, b.LocationId, b.Address1 as ServiceAddress1, B.ContractGroup, B.serviceName  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '')  union SELECT D.PersonID as ID,C.name,C.accountid,C.contactperson, D.LocationId, D.Address1 as ServiceAddress1 From tblperson C, tblPersonLocation D  ORDER BY AccountId, LocationId"
                '  SqlDSClientSearch.SelectCommand = "SELECT B.Companyid as ID,A.name,A.accountid,A.contactperson, b.LocationId, b.Address1 as ServiceAddress1, B.ContractGroup, B.serviceName  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '')  union SELECT D.Personid as ID,C.name,C.accountid,C.contactperson, D.LocationId, D.Address1 as ServiceAddress1, D.ContractGroup, D.serviceName From tblperson C  Left join tblPersonLocation D on C.Accountid = D.Accountid  where (C.Accountid is not null and D.Accountid <> '')   ORDER BY AccountId, LocationId"


            Else
                '     SqlDSClientSearch.SelectCommand = "SELECT distinct * From tblContactMaster where rcno <>0 and (ContID like '" + txtPpclient.Text.Trim + "%' OR ACCOUNTID Like '%" + txtPpclient.Text.Trim + "%') order by contname"
                'SqlDSClientSearch.SelectCommand = "(SELECT id,name,accountid,contactperson From tblCompany where AccountID like '" + txtPpclient.Text.Trim + "%' OR ID Like '%" + txtPpclient.Text.Trim + "%') union (SELECT id,name,accountid,contactperson From tblperson where AccountID like '" + txtPpclient.Text.Trim + "%' OR ID Like '%" + txtPpclient.Text.Trim + "%')"
                '    SqlDSClientSearch.SelectCommand = "SELECT B.CompanyID as ID,A.name,A.accountid,A.contactperson, b.LocationId, b.Address1 as ServiceAddress1,b.contractgroup, B.serviceName  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and  A.AccountID like '" + txtPpclient.Text.Trim + "%' OR B.CompanyID Like '%" + txtPpclient.Text.Trim + "%' OR A.NAME Like '%" + txtPpclient.Text.Trim + "%' OR B.LocationID Like '%" + txtPpclient.Text.Trim + "%' union (SELECT D.PersonID as ID,C.name,C.accountid,C.contactperson, D.LocationId, D.Address1 as ServiceAddress1 From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') AND C.AccountID like '%" + txtPpclient.Text.Trim + "%' OR D.PersonID Like '%" + txtPpclient.Text.Trim + "%' OR C.NAME Like '%" + txtPpclient.Text.Trim + "%' OR D.LocationID Like '%" + txtPpclient.Text.Trim + "%') ORDER BY AccountId, LocationId"
                '   SqlDSClientSearch.SelectCommand = "SELECT B.Companyid as ID,A.name,A.accountid,A.contactperson, b.LocationId, b.Address1 as ServiceAddress1, B.ContractGroup, B.serviceName  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and  A.AccountID like '" + txtPpclient.Text.Trim + "%' OR B.Companyid Like '%" + txtPpclient.Text.Trim + "%' OR A.NAME Like '%" + txtPpclient.Text.Trim + "%' OR B.LocationID Like '%" + txtPpclient.Text.Trim + "%' union (SELECT D.Personid as ID,C.name,C.accountid,C.contactperson, D.LocationId, D.Address1 as ServiceAddress1, D.ContractGroup, D.serviceName From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') AND C.AccountID like '" + txtPpclient.Text.Trim + "%' OR D.Personid Like '%" + txtPpclient.Text.Trim + "%' OR C.NAME Like '%" + txtPpclient.Text.Trim + "%' OR D.LocationID Like '%" + txtPpclient.Text.Trim + "%') ORDER BY AccountId, LocationId"
                '  SqlDSClientSearch.SelectCommand = "SELECT AccountID,Name,ROCNos,TaxIdentificationNo From tblCOMPANY where rcno <>0 and TaxIdentificationNo like '%" & txtPpclient.Text.Trim & "%' order by TaxIdentificationNo"
                Dim qry As String = ""
                qry = "SELECT AccountID,Name,ROCNos,TaxIdentificationNo From tblCOMPANY where rcno <>0 "

                If String.IsNullOrEmpty(txtNameE.Text) = False Then
                    qry = qry + " and Name like '%" & txtNameE.Text & "%'"
                End If
                If String.IsNullOrEmpty(txtRegNo.Text) = False Then
                    qry = qry + " and RocNos = '" & txtRegNo.Text & "'"
                End If
                qry = qry + " order by Name;"
                SqlDSClientSearch.SelectCommand = qry

            End If


            gvClientSearch.PageIndex = e.NewPageIndex

            mdlPopupTIN.Show()

        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY", "gvClientSearch_PageIndexChanging", ex.Message.ToString, txtCreatedBy.Text)
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Protected Sub gvClientSearch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvClientSearch.SelectedIndexChanged
        Try

            'If (gvClientSearch.SelectedRow.Cells(1).Text = "&nbsp;") Then
            '    txtNameE.Text = ""
            'Else
            '    txtNameE.Text = Server.HtmlDecode(gvClientSearch.SelectedRow.Cells(1).Text.Trim)
            'End If


            'If (gvClientSearch.SelectedRow.Cells(2).Text = "&nbsp;") Then
            '    txtRegNo.Text = ""
            'Else
            '    txtRegNo.Text = gvClientSearch.SelectedRow.Cells(2).Text.Trim
            'End If

            'If (gvClientSearch.SelectedRow.Cells(3).Text = "&nbsp;") Then
            '    txtTIN.Text = ""
            'Else
            '    txtTIN.Text = gvClientSearch.SelectedRow.Cells(3).Text.Trim
            ' End If

            Dim tin As String = SearchTin(gvClientSearch.SelectedRow.Cells(1).Text, "BRN", gvClientSearch.SelectedRow.Cells(2).Text)
            txtTIN.Text = tin

            mdlPopupTIN.Hide()

        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY", "gvClientSearch_SelectedIndexChanged", ex.Message.ToString, gvClientSearch.SelectedRow.Cells(2).Text.Trim)
        End Try
    End Sub

    Protected Sub btnppClientReset_Click(sender As Object, e As ImageClickEventArgs) Handles btnppClientReset.Click
        Try
            txtPpclient.Text = "Search Here"
            txtPpClientSearch.Text = ""
            '  SqlDSClientSearch.SelectCommand = "SELECT distinct * From tblcontactmaster where rcno <>0 order by contname"
            'SqlDSClientSearch.SelectCommand = "SELECT id,name,accountid,contactperson FROM tblcompany union SELECT id,name,accountid,contactperson FROM tblperson"
            '  SqlDSClientSearch.SelectCommand = "SELECT B.CompanyID as ID,A.name,A.accountid,A.contactperson, b.LocationId, b.Address1 as ServiceAddress1, B.ContractGroup, B.ServiceName  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '')  union SELECT D.Personid as ID,C.name,C.accountid,C.contactperson, D.LocationId, D.Address1 as ServiceAddress1, D.ContractGroup, D.ServiceName From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where (C.Accountid is not null and C.Accountid <> '') and  (D.Accountid is not null and D.Accountid <> '')  ORDER BY AccountId, LocationId"
            SqlDSClientSearch.SelectCommand = "SELECT AccountID,Name,ROCNos,TaxIdentificationNo From tblCOMPANY where rcno <>0 order by TaxIdentificationNo"


            SqlDSClientSearch.DataBind()
            gvClientSearch.DataBind()
            mdlPopupTIN.Show()
            'txtIsPopUp.Text = "ClientSearch"
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY", "btnppClientReset_Click", ex.Message.ToString, txtCreatedBy.Text)
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Protected Sub txtPpclient_TextChanged(sender As Object, e As EventArgs) Handles txtPpclient.TextChanged
        Try
            If txtPpclient.Text.Trim = "" Then
                MessageBox.Message.Alert(Page, "Please enter search text", "str")
            Else
                txtPpClientSearch.Text = txtPpclient.Text
                ' SqlDSClientSearch.SelectCommand = "(SELECT id,name,accountid,contactperson From tblCompany where AccountID like '" + txtPpclient.Text.Trim + "%' OR ID Like '%" + txtPpclient.Text.Trim + "%' or name like '" + txtPpclient.Text.Trim + "%') union (SELECT id,name,accountid,contactperson From tblperson where AccountID like '" + txtPpclient.Text.Trim + "%' OR ID Like '%" + txtPpclient.Text.Trim + "%' or name like '" + txtPpclient.Text.Trim + "%')"
                '    SqlDSClientSearch.SelectCommand = "SELECT AccountID,Name,ROCNos,TaxIdentificationNo From tblCOMPANY where rcno <>0 and TaxIdentificationNo like '%" & txtPpclient.Text.Trim & "%' order by TaxIdentificationNo"


                'If txtClientModal.Text = "ID" Then
                '    '   SqlDSClientSearch.SelectCommand = "SELECT distinct * From tblContactMaster where rcno <>0 and (ContID like '" + txtPpclient.Text.Trim + "%' OR ACCOUNTID Like '%" + txtPpclient.Text.Trim + "%') order by contname"
                '    SqlDSClientSearch.SelectCommand = "(SELECT id,name,accountid,contactperson From tblCompany where AccountID like '" + txtPpclient.Text.Trim + "%' OR ID Like '%" + txtPpclient.Text.Trim + "%') union (SELECT id,name,accountid,contactperson From tblperson where AccountID like '" + txtPpclient.Text.Trim + "%' OR ID Like '%" + txtPpclient.Text.Trim + "%')"

                'Else
                '    '   SqlDSClientSearch.SelectCommand = "SELECT distinct * From tblContactMaster where rcno <>0 and ContName like '" + txtPpclient.Text.Trim + "%' order by contname"
                '    SqlDSClientSearch.SelectCommand = "(SELECT id,name,accountid,contactperson From tblCompany where name like '" + txtPpclient.Text.Trim + "%') union (SELECT id,name,accountid,contactperson From tblperson where Name like '" + txtPpclient.Text.Trim + "%')"

                'End If

                Dim qry As String = ""
                qry = "SELECT AccountID,Name,ROCNos,TaxIdentificationNo From tblCOMPANY where rcno <>0 "

                If String.IsNullOrEmpty(txtNameE.Text) = False Then
                    qry = qry + " and Name like '%" & txtNameE.Text & "%'"
                End If
                If String.IsNullOrEmpty(txtRegNo.Text) = False Then
                    qry = qry + " and RocNos = '" & txtRegNo.Text & "'"
                End If
                qry = qry + " order by Name;"
                SqlDSClientSearch.SelectCommand = qry

                SqlDSClientSearch.DataBind()
                gvClientSearch.DataBind()

            End If

            txtPpclient.Text = "Search Here"
            mdlPopupTIN.Show()
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY", "txtPpclient_TextChanged", ex.Message.ToString, txtCreatedBy.Text)
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Protected Sub OnRowDataBoundgClientSearch(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='silver';this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#E4E4E4';")
            'e.Row.Attributes.Add("onmousedown", "this.style.backgroundColor='#738A9C';")
            'e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#738A9C';")

            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvClientSearch, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Protected Sub OnSelectedIndexChangedgClientSearch(sender As Object, e As EventArgs) Handles gvClientSearch.SelectedIndexChanged
        For Each row As GridViewRow In gvClientSearch.Rows
            If row.RowIndex = gvClientSearch.SelectedIndex Then
                row.BackColor = ColorTranslator.FromHtml("#738A9C")
                row.ToolTip = String.Empty
            Else
                row.BackColor = ColorTranslator.FromHtml("#E4E4E4")
                row.ToolTip = "Click to select this row."
            End If
        Next
    End Sub

    Private AccessToken As String ' = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ijk2RjNBNjU2OEFEQzY0MzZDNjVBNDg1MUQ5REM0NTlFQTlCM0I1NTRSUzI1NiIsIng1dCI6Imx2T21Wb3JjWkRiR1draFIyZHhGbnFtenRWUSIsInR5cCI6ImF0K2p3dCJ9.eyJpc3MiOiJodHRwczovL3ByZXByb2QtaWRlbnRpdHkubXlpbnZvaXMuaGFzaWwuZ292Lm15IiwibmJmIjoxNzM1Mjk5OTM4LCJpYXQiOjE3MzUyOTk5MzgsImV4cCI6MTczNTMwMzUzOCwiYXVkIjpbIkludm9pY2luZ0FQSSIsImh0dHBzOi8vcHJlcHJvZC1pZGVudGl0eS5teWludm9pcy5oYXNpbC5nb3YubXkvcmVzb3VyY2VzIl0sInNjb3BlIjpbIkludm9pY2luZ0FQSSJdLCJjbGllbnRfaWQiOiIwZmNhYTU3Yy04YTJkLTQxOTQtODU3Mi0xOTM4YzJjY2M2MjgiLCJJc1RheFJlcHJlcyI6IjEiLCJJc0ludGVybWVkaWFyeSI6IjAiLCJJbnRlcm1lZElkIjoiMCIsIkludGVybWVkVElOIjoiIiwiSW50ZXJtZWRST0IiOiIiLCJJbnRlcm1lZEVuZm9yY2VkIjoiMiIsIm5hbWUiOiJDMTE1MzI2NTIwOTA6MGZjYWE1N2MtOGEyZC00MTk0LTg1NzItMTkzOGMyY2NjNjI4IiwiU1NJZCI6IjZkMjNiOGNhLTFiODgtZmExNy1kZTU2LTZlYTJiYTFmZmIwMCIsInByZWZlcnJlZF91c2VybmFtZSI6IkFPTCBCZXRhIiwiVGF4SWQiOiIzNDYwMSIsIlRheHBheWVyVElOIjoiQzExNTMyNjUyMDkwIiwiVGF4VGluIjoiQzExNTMyNjUyMDkwIiwiUHJvZklkIjoiNDMyMjciLCJJc1RheEFkbWluIjoiMCIsIklzU3lzdGVtIjoiMSJ9.nLz_PadiyijqN9KW4qyybga4YRZDAwXiJhodK8rDiHPg1AvzwO17zw9Ularr69I0cKmUk5AVDncbp9VbilwZAXRgy34rKyvvg09RZH73Y9k5NfrC7N6ztk91eXepO7duDiPdAEIWIAjHGBa282rt5R3Do-nYiiskkfqoLZPjLcAuEBUysOSuj0MuzUGbzfqfF0kiJX6ORnTTjB8EpcFtl5r9Za0MPKR27XRCaHlPCwdfi_akQ3YSTC7yYjEQ14UFuWivDLDzBIJLANAvABKM2ijhNijJl2aatxCyz5kMg139EPwokr1rr2ZONneerrBXVy5yQ4si9YW9ZSj-ygRCSQ"

    Private Async Function GetToken() As Task
        Get_API_Token().Wait()
    End Function

    Private Function Get_API_Token() As Task
        Dim clientId As String = ConfigurationManager.AppSettings.[Get]("apiEInvoiceclientId")
        Dim clientSecret As String = ConfigurationManager.AppSettings.[Get]("apiEInvoiceclientSecret")
        Dim grant_type As String = ConfigurationManager.AppSettings.[Get]("apiEInvoicegrant_type")
        Dim scope As String = ConfigurationManager.AppSettings.[Get]("apiEInvoicescope")
        Dim tokenURL As String = ConfigurationManager.AppSettings.[Get]("apiEInvoiceToken")

        Return Task.Run(Async Function()

                            Dim token As String = ""
                            Using client = New HttpClient()
                                Try

                                    Dim data = {
                                             New KeyValuePair(Of String, String)("client_id", clientId),
                                             New KeyValuePair(Of String, String)("client_secret", clientSecret),
                                             New KeyValuePair(Of String, String)("grant_type", grant_type),
                                             New KeyValuePair(Of String, String)("scope", scope)
                                         }

                                    Dim apiGetToken As String = tokenURL
                                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
                                    Dim response As HttpResponseMessage = client.PostAsync(apiGetToken, New FormUrlEncodedContent(data)).Result
                                    Dim responseString = response.Content.ReadAsStringAsync().Result
                                    If response.IsSuccessStatusCode Then
                                        Dim jObject = Newtonsoft.Json.Linq.JObject.Parse(responseString)
                                        AccessToken = jObject("access_token").ToString()
                                    End If
                                Catch ex As Exception

                                    Dim test As String = ""

                                End Try

                            End Using
                        End Function)

    End Function

    Protected Function SearchTin(TaxPayerName As String, idType As String, idvalue As String) As String
        Dim token = GetToken()
        Dim tokenno As String = ""
        tokenno = AccessToken
        InsertIntoTblWebEventLog("INVOICE2", "ACCESSTOKEN", AccessToken, Session("UserID").ToString)


        'Dim tokenno As String = ""
        'tokenno = AccessToken
        ''  tokenno = GetToken()
        'InsertIntoTblWebEventLog("TIN1", "ACCESSTOKEN", AccessToken, Session("UserID").ToString)

        ''   Dim values As String = "tin?idType=" + idType + "&idValue= " + idvalue + "&taxpayername= " + TaxPayerName
        ''  Dim apiSearchTinURL = ConfigurationManager.AppSettings.[Get]("apiSearchTin")
        'Dim apiSearchTinURL = "https://preprod-api.myinvois.hasil.gov.my/api/v1.0/taxpayer/search"
        'Dim values As String = "tin?idType=" + idType + "&idValue=" + idvalue

        'Using client = New HttpClient()
        '    If Not String.IsNullOrWhiteSpace(tokenno) Then

        '        client.DefaultRequestHeaders.Authorization = New Headers.AuthenticationHeaderValue("Bearer", tokenno)
        '        Dim apiSearchTinurlPath As String = apiSearchTinURL & "/" & values
        '        InsertIntoTblWebEventLog("TIN1", "SEARCHTIN", apiSearchTinurlPath, Session("UserID").ToString)

        '        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
        '        Dim response As HttpResponseMessage = client.GetAsync(apiSearchTinurlPath).Result
        '        Dim responseString = response.Content.ReadAsStringAsync().Result

        '        If response.IsSuccessStatusCode Then
        '            Return True
        '        End If

        '    End If
        'End Using

        'Return False

        Dim tin As String
        'Dim tokenno As String = ""
        'tokenno = AccessToken
        '  tokenno = GetToken()
        InsertIntoTblWebEventLog("TIN2", idvalue, idType, Session("UserID").ToString)

        Dim values As String = "tin?idType=" + idType + "&idValue=" + idvalue
        '  Dim apiSearchTinURL = ConfigurationManager.AppSettings.[Get]("apiSearchTin")
        Dim apiSearchTinURL = "https://preprod-api.myinvois.hasil.gov.my/api/v1.0/taxpayer/search"
        Using client = New HttpClient()
            If Not String.IsNullOrWhiteSpace(tokenno) Then

                client.DefaultRequestHeaders.Authorization = New Headers.AuthenticationHeaderValue("Bearer", tokenno)
                Dim apiSearchTinurlPath As String = apiSearchTinURL & "/" & values
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
                Dim response As HttpResponseMessage = client.GetAsync(apiSearchTinurlPath).Result
                Dim responseString = response.Content.ReadAsStringAsync().Result
                InsertIntoTblWebEventLog("TIN3", "RESPONSE", response.IsSuccessStatusCode.ToString, Session("UserID").ToString)

                If response.IsSuccessStatusCode Then
                    'Return True
                    Dim jObject = Newtonsoft.Json.Linq.JObject.Parse(responseString)
                    tin = jObject("tin").ToString()
                End If

            End If
        End Using

        Return tin
    End Function

    Protected Sub btnSearchTIN_Click(sender As Object, e As EventArgs)

        Dim tin As String = SearchTin(txtNameE.Text, "BRN", txtRegNo.Text)
        '   txtTIN.Text = tin
        txtPpClientSearch.Text = txtTIN.Text
        txtPpclient.Text = txtTIN.Text

        Dim qry As String = ""
        qry = "SELECT AccountID,Name,ROCNos,TaxIdentificationNo From tblCOMPANY where rcno <>0 "

        If String.IsNullOrEmpty(txtNameE.Text) = False Then
            qry = qry + " and Name like '%" & txtNameE.Text & "%'"
        End If
        If String.IsNullOrEmpty(txtRegNo.Text) = False Then
            qry = qry + " and RocNos = '" & txtRegNo.Text & "'"
        End If
        qry = qry + " order by Name;"
        SqlDSClientSearch.SelectCommand = qry

        SqlDSClientSearch.DataBind()
        gvClientSearch.DataBind()

        ' Dim tin = SearchTin("", "BRN", "199901007530")

        '    InsertIntoTblWebEventLog("COMPANY", "SEARCH TIN", tin, DateTime.Now.ToString)

        'If txtPpClientSearch.Text.Trim = "" Then

        '    SqlDSClientSearch.SelectCommand = "SELECT AccountID,Name,ROCNos,TaxIdentificationNo From tblCOMPANY where rcno <>0 order by TaxIdentificationNo"
        'Else
        '    SqlDSClientSearch.SelectCommand = "SELECT AccountID,Name,ROCNos,TaxIdentificationNo From tblCOMPANY where rcno <>0 and TaxIdentificationNo like '%" & txtPpclient.Text.Trim & "%' order by TaxIdentificationNo"

        'End If

        'SqlDSClientSearch.DataBind()
        'gvClientSearch.DataBind()

        mdlPopupTIN.Show()

    End Sub
End Class

Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Collections.Generic
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Globalization
Imports System.Drawing

Partial Class Person
    Inherits System.Web.UI.Page

    Public rcno As String
    Public svcrcno As String
    Private Shared gScheduler, gSalesman, gIncharge As String

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

    Public Sub MakeMeNull()
        lblMessage.Text = ""
        lblAlert.Text = ""
        txtMode.Text = ""
        txtAccountID.Text = ""
        'ddlStatus.SelectedItem.Text = "O"
        ddlStatus.SelectedIndex = 0
        ddlCompanyGrp.SelectedIndex = 0


        txtNameE.Text = ""
        txtNameO.Text = ""

        ddlSalute.SelectedIndex = 0
        ddlSex.SelectedIndex = 0
        ddlIC.SelectedIndex = 0
        txtNRIC.Text = ""
        ddlNationality.SelectedIndex = 0

        txtStartDate.Text = ""
        ddlSalesMan.SelectedIndex = 0
        ddlIncharge.SelectedIndex = 0
        txtComments.Text = ""

        txtOffAddress1.Text = ""
        txtOffStreet.Text = ""
        txtOffBuilding.Text = ""
        ddlOffCity.SelectedIndex = 0
        ddlOffCountry.SelectedIndex = 0
        ddlOffState.SelectedIndex = 0
        txtOffPostal.Text = ""

        txtOffContactPerson.Text = ""
        txtOffEmail.Text = ""
        txtOffContactNo.Text = ""
        txtOffFax.Text = ""
        txtOffContact2.Text = ""
        txtOffMobile.Text = ""

        txtOffCont1Name.Text = ""
        txtOffCont1Tel.Text = ""
        txtOffCont1Fax.Text = ""
        txtOffCont1Tel2.Text = ""
        txtOffCont1Mobile.Text = ""
        txtOffCont1Email.Text = ""

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
        ddlDefaultInvoiceFormat.SelectedIndex = 0
        txtBillingOptionRemarks.Text = ""

        txtRcno.Text = ""

        txtClientID.Text = ""
        txtAccountCode.Text = ""

        txtBillingName.Text = ""
        ddlTerms.SelectedIndex = 0
        'ddlCurrency.SelectedValue = "SGD"
        ddlCurrency.SelectedIndex = 0
        txtCreatedOn.Text = ""

        'txtLocation.Text = ""
        chkSendStatementSOA.Checked = True
        chkSendStatementInv.Checked = False
        chkAutoEmailInvoice.Checked = False
        chkAutoEmailStatement.Checked = False
        chkRequireEBilling.Checked = False

        chkEmailNotifySchedule.Checked = False
        chkEmailNotifyJobProgress.Checked = False
        chkPhotosMandatory.Checked = False
        chkDisplayTimeInTimeOut.Checked = False

        ddlCurrencyEdit.SelectedIndex = 0
        ddlDefaultInvoiceFormatEdit.SelectedIndex = 0
        ddlTermsEdit.SelectedIndex = 0
        chkAutoEmailInvoiceEdit.Checked = False
        chkAutoEmailStatementEdit.Checked = False
        txtBillingOptionRemarksEdit.Text = ""
        chkRequireEBillingEdit.Checked = False

        chkSendStatementSOAEdit.Checked = False
        chkSendStatementInvEdit.Checked = False
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

        txtTIN.Text = ""
        txtSST.Text = ""
        'chkSmartCustomer.Checked = False
    End Sub


    Private Sub FindCountry(source As String)
        Try
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
            conn.Close()
            conn.Dispose()
            command.Dispose()
            dt.Dispose()
            dr.Close()
        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "FindCountry", ex.Message.ToString, txtAccountID.Text)
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
            'btnCopyAdd.Enabled = True
            'btnCopyAdd.ForeColor = System.Drawing.Color.Black
            'btnDelete.Enabled = True
            'btnDelete.ForeColor = System.Drawing.Color.Black
            btnQuit.Enabled = True
            btnQuit.ForeColor = System.Drawing.Color.Black

            btnCopyAdd.Enabled = False
            btnCopyAdd.ForeColor = System.Drawing.Color.Gray
            btnDelete.Enabled = False
            btnDelete.ForeColor = System.Drawing.Color.Gray

            btnChangeStatus.Enabled = False
            btnChangeStatus.ForeColor = System.Drawing.Color.Gray
            'btnPrint.Enabled = True
            'btnPrint.ForeColor = System.Drawing.Color.Black

            btnChangeStatus.Enabled = False
            btnChangeStatus.ForeColor = System.Drawing.Color.Gray

            btnContract.Enabled = False
            btnContract.ForeColor = System.Drawing.Color.Gray

            btnTransactions.Enabled = False
            btnTransactions.ForeColor = System.Drawing.Color.Gray
            btnChangeStatus.Enabled = False
            btnChangeStatus.ForeColor = System.Drawing.Color.Gray

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
            ddlSalute.Enabled = False
            ddlSex.Enabled = False
            ddlIC.Enabled = False
            txtNRIC.Enabled = False
            ddlNationality.Enabled = False
            txtStartDate.Enabled = False
            ddlSalesMan.Enabled = False
            ddlIncharge.Enabled = False
            txtComments.Enabled = False

            txtOffAddress1.Enabled = False
            txtOffStreet.Enabled = False
            txtOffBuilding.Enabled = False
            ddlOffCity.Enabled = False
            ddlOffCountry.Enabled = False
            ddlOffState.Enabled = False
            txtOffPostal.Enabled = False

            txtOffContactPerson.Enabled = False
            txtOffMobile.Enabled = False
            txtOffContactNo.Enabled = False
            txtOffFax.Enabled = False
            txtOffContact2.Enabled = False
            txtOffEmail.Enabled = False

            txtOffCont1Name.Enabled = False
            txtOffCont1Tel.Enabled = False
            txtOffCont1Fax.Enabled = False
            txtOffCont1Tel2.Enabled = False
            txtOffCont1Mobile.Enabled = False
            txtOffCont1Email.Enabled = False

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

            txtBillingName.Enabled = False
            ddlTerms.Enabled = False
            ddlCurrency.Enabled = False

            Label30.Visible = False
            btnUpdateBilling.Visible = False
            chkSendStatementSOA.Enabled = False
            chkSendStatementInv.Enabled = False

            chkEmailNotifySchedule.Enabled = False
            chkEmailNotifyJobProgress.Enabled = False
            chkPhotosMandatory.Enabled = False
            chkDisplayTimeInTimeOut.Enabled = False

            ddlLocation.Enabled = False
            ddlDefaultInvoiceFormat.Enabled = False
            txtBillingOptionRemarks.Enabled = False

            btnEditSendStatement.Visible = True
            chkAutoEmailInvoice.Enabled = False
            chkAutoEmailStatement.Enabled = False
            txtCreditLimit.Enabled = False
            chkRequireEBilling.Enabled = False

            txtTIN.Enabled = False
            txtSST.Enabled = False

            'chkSmartCustomer.Enabled = False


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
            'command.CommandText = "SELECT x2412, x2413, x0303, x0303Add, x0303Edit,  x0303Delete, x0303Trans FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"
            'command.Connection = conn

            'Dim dr As MySqlDataReader = command.ExecuteReader()
            'Dim dt As New DataTable
            'dt.Load(dr)

            'If dt.Rows.Count > 0 Then

            '    If String.IsNullOrEmpty(dt.Rows(0)("x0303")) = False Then
            '        If dt.Rows(0)("x0303").ToString() = False Then
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

            '    If String.IsNullOrEmpty(dt.Rows(0)("x0303Add")) = False Then
            '        btnADD.Enabled = dt.Rows(0)("x0303Add").ToString()
            '    End If

            '    If String.IsNullOrEmpty(dt.Rows(0)("x0303Edit")) = False Then
            '        btnCopyAdd.Enabled = dt.Rows(0)("x0303Edit").ToString()
            '    End If

            '    If String.IsNullOrEmpty(dt.Rows(0)("x0303Delete")) = False Then
            '        btnDelete.Enabled = dt.Rows(0)("x0303Delete").ToString()
            '    End If




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
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "EnableControls", ex.Message.ToString, txtAccountID.Text)
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

            btnTransactions.Enabled = False
            btnTransactions.ForeColor = System.Drawing.Color.Gray

            btnChangeStatus.Enabled = False
            btnChangeStatus.ForeColor = System.Drawing.Color.Gray

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
            'btnContract.Enabled = True
            'btnContract.ForeColor = System.Drawing.Color.Black

            btnContract.Enabled = False
            btnContract.ForeColor = System.Drawing.Color.Gray

            btnFilter.Enabled = False
            btnFilter.ForeColor = System.Drawing.Color.Gray

            btnTransfersSvc.Enabled = False
            btnTransfersSvc.ForeColor = System.Drawing.Color.Gray


            btnSpecificLocation.Enabled = False
            btnSpecificLocation.ForeColor = System.Drawing.Color.Gray

            btnUpdateServiceContact.Enabled = False
            btnUpdateServiceContact.ForeColor = System.Drawing.Color.Gray

            txtAccountID.Enabled = True
            ddlStatus.Enabled = True
            ddlCompanyGrp.Enabled = True

            txtNameE.Enabled = True
            txtNameO.Enabled = True
            ddlSalute.Enabled = True
            ddlSex.Enabled = True
            ddlIC.Enabled = True
            txtNRIC.Enabled = True
            ddlNationality.Enabled = True
            txtStartDate.Enabled = True
            ddlSalesMan.Enabled = True
            ddlIncharge.Enabled = True
            txtComments.Enabled = True

            txtOffAddress1.Enabled = True
            txtOffStreet.Enabled = True
            txtOffBuilding.Enabled = True
            ddlOffCity.Enabled = True
            ddlOffCountry.Enabled = True
            ddlOffState.Enabled = True
            txtOffPostal.Enabled = True

            txtOffContactPerson.Enabled = True
            txtOffMobile.Enabled = True
            txtOffContactNo.Enabled = True
            txtOffFax.Enabled = True
            txtOffContact2.Enabled = True
            txtOffEmail.Enabled = True

            txtOffCont1Name.Enabled = True
            txtOffCont1Tel.Enabled = True
            txtOffCont1Fax.Enabled = True
            txtOffCont1Tel2.Enabled = True
            txtOffCont1Mobile.Enabled = True
            txtOffCont1Email.Enabled = True

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

            chkOffAddr.Enabled = True
            '     chkInactive.Enabled = True
            rdbBillingSettings.Enabled = True

            txtBillingName.Enabled = True
            ddlTerms.Enabled = True
            ddlCurrency.Enabled = True

            ddlDefaultInvoiceFormat.Enabled = True
            txtBillingOptionRemarks.Enabled = True

            btnEditSendStatement.Visible = False

            btnUpdateBilling.Visible = False
            chkSendStatementSOA.Enabled = True
            chkSendStatementInv.Enabled = True

            chkEmailNotifySchedule.Enabled = True
            chkEmailNotifyJobProgress.Enabled = True
            chkPhotosMandatory.Enabled = True
            chkDisplayTimeInTimeOut.Enabled = True

            ddlLocation.Enabled = True
            btnEditSendStatement.Visible = False
            ddlCurrency.Enabled = True
            chkAutoEmailInvoice.Enabled = True
            chkAutoEmailStatement.Enabled = True
            txtCreditLimit.Enabled = True
            chkRequireEBilling.Enabled = True

            txtTIN.Enabled = True
            txtSST.Enabled = True

            'chkSmartCustomer.Enabled = False


            'ddlCurrencyEdit.Enabled = True
            'ddlDefaultInvoiceFormatEdit.Enabled = True
            'ddlTermsEdit.Enabled = True
            'chkAutoEmailInvoiceEdit.Enabled = True
            'chkSendStatementEdit.Enabled = True

        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "DisableControls", ex.Message.ToString, txtAccountID.Text)
        End Try
    End Sub

    Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        Try
            GridView1.PageIndex = e.NewPageIndex
            SqlDataSource1.SelectCommand = txt.Text
            SqlDataSource1.DataBind()
            GridView1.DataBind()
        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "GridView1_PageIndexChanging", ex.Message.ToString, txtAccountID.Text)
        End Try
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
            'MakeMeNull()
            'MakeSvcNull()

            Dim editindex As Integer
            'rcno = DirectCast(GridView1.Rows(editindex).FindControl("Label1"), Label).Text
            'txtRcno.Text = rcno.ToString()


            If (Session("servicefrom")) = "contactP" Then

                MakeMeNull()
                MakeSvcNull()
                txtRcno.Text = Session("rcno")
                ddlCompanyGrp.SelectedIndex = -1
                'ddlIndustry.SelectedIndex = -1
                ddlSalesMan.SelectedIndex = -1
                ddlIncharge.SelectedIndex = -1
                ddlTerms.SelectedIndex = -1
                ddlOffCity.SelectedIndex = -1
                ddlOffCountry.SelectedIndex = -1
                ddlOffState.SelectedIndex = -1
                ddlBillCity.SelectedIndex = -1
                ddlBillCountry.SelectedIndex = -1
                ddlBillState.SelectedIndex = -1
                'Session.Remove("servicefrom")
            ElseIf (Session("contractfrom")) = "clients" Then

                MakeMeNull()
                MakeSvcNull()
                txtRcno.Text = Session("rcno")

                ddlCompanyGrp.SelectedIndex = -1
                'ddlIndustry.SelectedIndex = -1
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


                'Session.Remove("contractfrom")
                'Dim editindex As Integer = GridView1.SelectedIndex
                'rcno = DirectCast(GridView1.Rows(editindex).FindControl("Label1"), Label).Text
                'txtRcno.Text = rcno.ToString()
            ElseIf (Session("customerfrom")) = "Residential" Then
                Session.Remove("customerfrom")
                MakeMeNull()
                MakeSvcNull()
                txtRcno.Text = Session("rcno")

                ddlCompanyGrp.SelectedIndex = -1
                'ddlIndustry.SelectedIndex = -1
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
                'Session.Remove("contractfrom")
                'Dim editindex As Integer = GridView1.SelectedIndex
                'rcno = DirectCast(GridView1.Rows(editindex).FindControl("Label1"), Label).Text
                'txtRcno.Text = rcno.ToString()
            Else

                MakeMeNull()
                MakeSvcNull()
                MakeNotesNull()

                EnableControls()

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

            If (Session("contractfrom")) = "clients" Or Session("servicefrom") = "contactP" Then
                'Dim pi As Integer = Session("gridview1personPI")
                'Dim ri As Integer = Session("gridview1personRI")
                GridView1.PageIndex = Session("gridview1personPI")
                GridView1.SelectedIndex = Session("gridview1personRI")

                Session.Remove("gridview1personPI")
                Session.Remove("gridview1personRI")
                'Session("gridview1personPI") = GridView1.PageIndex
                'Session("gridview1personRI") = GridView1.SelectedIndex
            End If

        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "GridView1_SelectedIndexChanged", ex.Message.ToString, txtAccountID.Text)

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
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "RetrieveAutoEmailInfo", ex.Message.ToString, txtAccountID.Text)
        End Try

    End Sub

    Private Sub PopulateRecord()
        Try
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()
            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.CommandText = "SELECT *,UPPER(LocateGrp),UPPER(Salesman) FROM tblperson where rcno=" & Convert.ToInt32(txtRcno.Text)
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New DataTable
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
                If dt.Rows(0)("PersonGroup").ToString <> "" Then
                    ddlCompanyGrp.Text = dt.Rows(0)("PersonGroup").ToString
                End If
                'If dt.Rows(0)("Salutation").ToString <> "" Then
                '    ddlSalute.Text = dt.Rows(0)("Salutation").ToString
                'End If
                '  txtID.Text = dt.Rows(0)("ID").ToString
                txtNameE.Text = dt.Rows(0)("Name").ToString
                lblName.Text = txtNameE.Text
                lblName2.Text = txtNameE.Text

                txtNameO.Text = dt.Rows(0)("Name2").ToString
                If dt.Rows(0)("Sex").ToString <> "" And String.IsNullOrEmpty(dt.Rows(0)("Sex").ToString) = False Then
                    'If dt.Rows(0)("Sex").ToString <> "" Then
                    ddlSex.Text = dt.Rows(0)("Sex").ToString
                End If
                If dt.Rows(0)("Nationality").ToString <> "" Then
                    ddlNationality.Text = dt.Rows(0)("Nationality").ToString
                End If
                If dt.Rows(0)("Inactive").ToString = "1" Then
                    chkInactive.Checked = True
                Else
                    chkInactive.Checked = False
                End If

                If dt.Rows(0)("ICType").ToString <> "" And String.IsNullOrEmpty(dt.Rows(0)("ICType").ToString) = False Then
                    ddlIC.Text = dt.Rows(0)("ICType").ToString
                End If


                txtNRIC.Text = dt.Rows(0)("Nric").ToString
                If dt.Rows(0)("DateJoin").ToString = DBNull.Value.ToString Then
                Else

                    txtStartDate.Text = Convert.ToDateTime(dt.Rows(0)("DateJoin")).ToString("dd/MM/yyyy")
                End If

                'If dt.Rows(0)("UPPER(LocateGrp)").ToString <> "" Then
                '    ddlLocateGrp.Text = dt.Rows(0)("UPPER(LocateGrp)").ToString
                'End If



                'If dt.Rows(0)("UPPER(Salesman)").ToString <> "" Then
                '    ddlSalesMan.Text = dt.Rows(0)("UPPER(Salesman)").ToString
                'End If
             
                If dt.Rows(0)("UPPER(Salesman)").ToString <> "" Then
                    'Dim gSalesman As String

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

                txtComments.Text = dt.Rows(0)("Comments").ToString

                '  txtAcctCode.Text = dt.Rows(0)("AccountNo").ToString

                'If dt.Rows(0)("IsCustomer").ToString = "1" Then
                '    chkCustomer.Checked = True
                'Else
                '    chkCustomer.Checked = False
                'End If
                'If dt.Rows(0)("IsSupplier").ToString = "1" Then
                '    chkSupplier.Checked = True
                'Else
                '    chkSupplier.Checked = False
                'End If
                txtOffAddress1.Text = dt.Rows(0)("Address1").ToString
                txtOffStreet.Text = dt.Rows(0)("AddStreet").ToString
                txtOffBuilding.Text = dt.Rows(0)("AddBuilding").ToString
                If dt.Rows(0)("AddCity").ToString <> "" Then
                    ddlOffCity.Text = dt.Rows(0)("AddCity").ToString
                End If


                If dt.Rows(0)("AddCountry").ToString <> "" Then
                    If Server.HtmlDecode(dt.Rows(0)("AddCountry")).ToString = "S'pore" Or Server.HtmlDecode(dt.Rows(0)("AddCountry")).ToString = "S'PORE" Then
                        ddlOffCountry.Text = "SINGAPORE"
                    Else
                        ddlOffCountry.Text = dt.Rows(0)("AddCountry").ToString
                    End If
                End If
                If dt.Rows(0)("AddState").ToString <> "" Then
                    ddlOffState.Text = dt.Rows(0)("AddState").ToString
                End If

                txtOffPostal.Text = dt.Rows(0)("AddPostal").ToString

                txtOffContactPerson.Text = dt.Rows(0)("ContactPerson").ToString
                txtOffMobile.Text = dt.Rows(0)("TelMobile").ToString

                txtOffContactNo.Text = dt.Rows(0)("TelHome").ToString
                txtOffFax.Text = dt.Rows(0)("TelFax").ToString
                txtOffContact2.Text = dt.Rows(0)("TelPager").ToString
                txtOffEmail.Text = dt.Rows(0)("Email").ToString

                txtOffCont1Name.Text = dt.Rows(0)("ContactPerson2").ToString
                txtOffCont1Tel.Text = dt.Rows(0)("ResCP2Tel").ToString
                txtOffCont1Fax.Text = dt.Rows(0)("ResCP2Fax").ToString
                txtOffCont1Tel2.Text = dt.Rows(0)("ResCP2Tel2").ToString
                txtOffCont1Mobile.Text = dt.Rows(0)("ResCP2Mobile").ToString
                txtOffCont1Email.Text = dt.Rows(0)("ResCP2Email").ToString

                If dt.Rows(0)("BillingAddress").ToString = "1" Then
                    chkOffAddr.Checked = True
                Else
                    chkOffAddr.Checked = False
                End If

                txtBillAddress.Text = dt.Rows(0)("BillAddress1").ToString
                txtBillStreet.Text = dt.Rows(0)("BillStreet").ToString
                txtBillBuilding.Text = dt.Rows(0)("BillBuilding").ToString
                If dt.Rows(0)("BillCity").ToString <> "" Then
                    ddlBillCity.Text = dt.Rows(0)("BillCity").ToString
                End If

                If dt.Rows(0)("BillCountry").ToString <> "" Then
                    If Server.HtmlDecode(dt.Rows(0)("BillCountry")).ToString = "S'pore" Or Server.HtmlDecode(dt.Rows(0)("AddCountry")).ToString = "S'PORE" Then
                        ddlBillCountry.Text = "SINGAPORE"
                    Else
                        ddlBillCountry.Text = dt.Rows(0)("BillCountry").ToString
                    End If
                End If
            
                If dt.Rows(0)("BillState").ToString <> "" Then
                    ddlBillState.Text = dt.Rows(0)("BillState").ToString
                End If
                txtBillPostal.Text = dt.Rows(0)("BillPostal").ToString

                txtBillCP1Contact.Text = dt.Rows(0)("BillContactPerson").ToString
                txtBillCP1Position.Text = dt.Rows(0)("BillContact1Position").ToString
                txtBillCP1Tel.Text = dt.Rows(0)("BillTelHome").ToString
                txtBillCP1Fax.Text = dt.Rows(0)("BillTelFax").ToString
                txtBillCP1Tel2.Text = dt.Rows(0)("BillTelPager").ToString
                txtBillCP1Mobile.Text = dt.Rows(0)("BillTelMobile").ToString
                txtBillCP1Email.Text = dt.Rows(0)("BillEmail").ToString

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

                If dt.Rows(0)("ArTerm").ToString <> "" Then
                    'Dim gSalesman As String

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
                    'If dt.Rows(0)("ArCurrency").ToString = "S$" Then

                    '    ddlCurrency.Text = "SGD"

                    'Else
                    If ddlCurrency.Items.FindByValue(gSalesman) Is Nothing Then
                        ddlCurrency.Items.Add(gSalesman)
                        '    ddlCurrency.Text = gSalesman
                        ddlCurrency.SelectedValue = gSalesman

                    Else
                        ddlCurrency.Text = dt.Rows(0)("ArCurrency").ToString.Trim().ToUpper()
                    End If
                End If


                    'If String.IsNullOrEmpty(dt.Rows(0)("BillingSettings").ToString) = False Then
                    '    rdbBillingSettings.SelectedValue = dt.Rows(0)("BillingSettings").ToString
                    'End If

                    'If dt.Rows(0)("ArTerm").ToString <> "" Then
                    '    ddlTerms.Text = dt.Rows(0)("ArTerm").ToString
                    'End If
                    txtBillingName.Text = dt.Rows(0)("BillingName").ToString

                    'If dt.Rows(0)("ArCurrency").ToString <> "" Then
                    '    ddlCurrency.Text = dt.Rows(0)("ArCurrency").ToString
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
                'If chkSendStatement.Checked = False Then
                '    btnOSInvoiceStatement.Disabled = True
                'Else
                '    btnOSInvoiceStatement.Disabled = False
                'End If

                'ddlLocation.Text = dt.Rows(0)("Location").ToString

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
                chkAutoEmailStatementEdit.Checked = chkAutoEmailStatement.Checked
                ddlDefaultInvoiceFormatEdit.Text = ddlDefaultInvoiceFormat.Text
                chkSendStatementSOAEdit.Checked = chkSendStatementSOA.Checked
                chkSendStatementInvEdit.Checked = chkSendStatementInv.Checked
                chkRequireEBillingEdit.Checked = chkRequireEBilling.Checked

                txtTIN.Text = dt.Rows(0)("TaxIdentificationNo").ToString
                txtSST.Text = dt.Rows(0)("SalesTaxRegistrationNo").ToString

                'chkSmartCustomer.Checked = dt.Rows(0)("SmartCustomer").ToString

                If txtDisplayRecordsLocationwise.Text = "N" Then
                    If String.IsNullOrEmpty(txtAccountID.Text) Then
                        SqlDataSource2.SelectCommand = "SELECT * FROM tblpersonlocation where accountid is null and personid = '" & txtClientID.Text & "'"
                    Else
                        SqlDataSource2.SelectCommand = "SELECT * FROM tblpersonlocation where accountid =  '" & txtAccountID.Text & "' order by LocationNo"
                    End If
                End If

                If txtDisplayRecordsLocationwise.Text = "Y" Then
                    If String.IsNullOrEmpty(txtAccountID.Text) Then
                        SqlDataSource2.SelectCommand = "SELECT * FROM tblpersonlocation where accountid is null and personid = '" & txtClientID.Text & "'"
                    Else
                        SqlDataSource2.SelectCommand = "SELECT * FROM tblpersonlocation where accountid =  '" & txtAccountID.Text & "' and location in (Select LocationID  from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') order by LocationNo"
                    End If
                End If

                SqlDataSource2.DataBind()
                GridView2.DataBind()

                txtDetail.Text = SqlDataSource2.SelectCommand
                lblServiceLocationCount.Text = "Service Location [" & GridView2.Rows.Count & "]"

                SqlDSNotesMaster.SelectCommand = "select * from tblnotes where keyfield = '" + txtAccountID.Text + "'"
                SqlDSNotesMaster.DataBind()
                gvNotesMaster.DataBind()


                ''View Uploaded files

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


                    'lblCurrentVal.Text = dtAgeing.Rows(0)("Current").ToString
                    'lbl1To30Val.Text = Convert.ToDecimal(dtAgeing.Rows(0)("1To10").ToString) + Convert.ToDecimal(dtAgeing.Rows(0)("11To30").ToString)
                    'lbl31To60Val.Text = dtAgeing.Rows(0)("31To60").ToString
                    'lbl61To90Val.Text = dtAgeing.Rows(0)("61To90").ToString
                    'lbl91To180Val.Text = Convert.ToDecimal(dtAgeing.Rows(0)("91To150").ToString) + Convert.ToDecimal(dtAgeing.Rows(0)("151To180").ToString)
                    'lblMoreThan180Val.Text = dtAgeing.Rows(0)("GreaterThan180").ToString

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

            tb1.ActiveTabIndex = 0
        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "PopulateRecord", ex.Message.ToString, txtAccountID.Text)
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

        btnTransfersSvc.Enabled = False
        btnTransfersSvc.ForeColor = System.Drawing.Color.Gray

        btnUpdateServiceContact.Enabled = True
        btnUpdateServiceContact.ForeColor = System.Drawing.Color.Black

        AccessControl()
        tb1.ActiveTabIndex = 0
        GridView1.SelectedIndex = 0

        'btnSvcContract.Enabled = False
        'btnSvcContract.ForeColor = System.Drawing.Color.Gray
        'btnSvcService.Enabled = False
        'btnSvcService.ForeColor = System.Drawing.Color.Gray

       

       


     
        If chkInactive.Checked = True Then
            btnCopyAdd.Enabled = False
            btnCopyAdd.ForeColor = System.Drawing.Color.Gray
            btnSvcAdd.Enabled = False
            btnSvcAdd.ForeColor = System.Drawing.Color.Gray
        End If
    End Sub


    Protected Sub btnADD_Click(sender As Object, e As EventArgs) Handles btnADD.Click
        Try
            DisableControls()
            MakeMeNull()
            txtMode.Text = "NEW"
            lblMessage.Text = "ACTION: ADD RECORD"
            ddlCompanyGrp.Focus()
            ddlStatus.Enabled = False
            'txtStartDate.Text = DateAndTime.Now.ToShortDateString
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "currentdatetimestartdate();", True)

            btnADD.Enabled = False
            tb1.ActiveTabIndex = 0

            txtSearchCust.Enabled = False
            btnGoCust.Enabled = False
            btnResetSearch.Enabled = False

            'chkAutoEmailInvoice.Checked = True
            'chkAutoEmailStatement.Checked = True
            'chkSendStatementInv.Checked = True
            'chkSendStatementSOA.Checked = True

            '''''''''''''''''Contact Module setup

            Dim conn As MySqlConnection = New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim CommandContactsSetup As MySqlCommand = New MySqlCommand
            CommandContactsSetup.CommandType = CommandType.Text
            CommandContactsSetup.CommandText = "SELECT * FROM tblContactSetup"
            CommandContactsSetup.Connection = conn

            Dim drContactsSetup As MySqlDataReader = CommandContactsSetup.ExecuteReader()
            Dim dtContactsSetup As New DataTable
            dtContactsSetup.Load(drContactsSetup)

            If dtContactsSetup.Rows.Count > 0 Then

                ddlTerms.Text = dtContactsSetup.Rows(0)("PersARTerms").ToString()
                ddlTermsSvc.Text = dtContactsSetup.Rows(0)("PersARTerms").ToString()

                If dtContactsSetup.Rows(0)("PersNRICBlank").ToString() = True Then
                    Label30.Visible = True
                End If

                If dtContactsSetup.Rows(0)("PersCurrency").ToString = "" Then
                    ddlCurrency.SelectedIndex = 0
                Else
                    ddlCurrency.Text = dtContactsSetup.Rows(0)("PersCurrency").ToString
                End If


                If dtContactsSetup.Rows(0)("PersIndustry").ToString = "" Then
                    ddlIndustrysvc.SelectedIndex = 0
                Else
                    ddlIndustrysvc.Text = dtContactsSetup.Rows(0)("PersIndustry").ToString
                End If

                If dtContactsSetup.Rows(0)("PersMarketSegmentID").ToString = "" Then
                    txtMarketSegmentIDsvc.Text = ""
                Else
                    txtMarketSegmentIDsvc.Text = dtContactsSetup.Rows(0)("PersMarketSegmentID").ToString
                End If

                If dtContactsSetup.Rows(0)("PersDefaultAutoEmailInvoice").ToString() = True Then
                    chkAutoEmailInvoice.Checked = True
                End If
                If dtContactsSetup.Rows(0)("PersDefaultAutoEmailSOA").ToString() = True Then
                    chkAutoEmailStatement.Checked = True
                End If
                If dtContactsSetup.Rows(0)("PersHardCopyInvoice").ToString() = True Then
                    chkSendStatementInv.Checked = True
                End If
                If dtContactsSetup.Rows(0)("PersHardCopySOA").ToString() = True Then
                    chkSendStatementSOA.Checked = True
                End If
                'chkAutoEmailInvoice.Checked = dtContactsSetup.Rows(0)("PersDefaultAutoEmailInvoice").ToString
            End If

            conn.Close()
            conn.Dispose()
            If txtDisplayTimeInTimeOutServiceRecord.Text = "1" Then
                chkDisplayTimeInTimeOut.Checked = True
            End If
            ''''''''''''''''''''''''''''''''''''''''
        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "btnADD_Click", ex.Message.ToString, txtAccountID.Text)
        End Try
    End Sub

    Private Sub AccessControl()
        Try
            '''''''''''''''''''Access Control 
            'If Session("SecGroupAuthority") <> "ADMINISTRATOR" Then
            Dim conn As MySqlConnection = New MySqlConnection()
            Dim command As MySqlCommand = New MySqlCommand

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            command.CommandType = CommandType.Text
            command.CommandText = "SELECT x2412, x2413, x0303, x0303Add, x0303Edit,  x0303Delete, x0303Trans,X0303EditBilling, x0303PersonSpecificLocation, x0303EditContractGroup, x0303ChangeAccount, x0303PersonUpdateServiceContact FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"
            command.Connection = conn

            Dim dr As MySqlDataReader = command.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then

                If String.IsNullOrEmpty(dt.Rows(0)("x0303")) = False Then
                    If dt.Rows(0)("x0303").ToString() = False Then
                        Response.Redirect("Home.aspx")
                    End If
                End If

                If Convert.ToBoolean(dt.Rows(0)("x2412")) = False Then
                    btnContract.Enabled = dt.Rows(0)("x2412").ToString()
                    btnSvcContract.Enabled = dt.Rows(0)("x2412").ToString()
                End If

                If Convert.ToBoolean(dt.Rows(0)("x2413")) = False Then
                    btnSvcService.Enabled = dt.Rows(0)("x2413").ToString()
                End If


                If String.IsNullOrEmpty(dt.Rows(0)("x0303Add")) = False Then
                    btnADD.Enabled = dt.Rows(0)("x0303Add").ToString()
                End If

                If String.IsNullOrEmpty(dt.Rows(0)("x0303Edit")) = False Then
                    btnCopyAdd.Enabled = dt.Rows(0)("x0303Edit").ToString()
                End If

                If String.IsNullOrEmpty(dt.Rows(0)("x0303Delete")) = False Then
                    btnDelete.Enabled = dt.Rows(0)("x0303Delete").ToString()
                End If

                If String.IsNullOrEmpty(dt.Rows(0)("x0303EditBilling")) = False Then
                    btnUpdateBilling.Enabled = dt.Rows(0)("x0303EditBilling").ToString()
                End If

                If String.IsNullOrEmpty(dt.Rows(0)("x0303ChangeAccount")) = False Then
                    btnTransfersSvc.Enabled = dt.Rows(0)("x0303ChangeAccount").ToString()
                End If

                If String.IsNullOrEmpty(dt.Rows(0)("x0303PersonSpecificLocation")) = False Then
                    btnSpecificLocation.Enabled = dt.Rows(0)("x0303PersonSpecificLocation").ToString()
                End If

                If Convert.ToBoolean(dt.Rows(0)("x0303EditContractGroup")) = False Then
                    btnEditContractGroup.Visible = dt.Rows(0)("x0303EditContractGroup").ToString()
                Else
                    btnEditContractGroup.Visible = dt.Rows(0)("x0303EditContractGroup").ToString()
                End If

                If String.IsNullOrEmpty(dt.Rows(0)("x0303PersonUpdateServiceContact")) = False Then
                    btnUpdateServiceContact.Enabled = dt.Rows(0)("x0303PersonUpdateServiceContact").ToString()
                End If

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

                If btnTransfersSvc.Enabled = True Then
                    btnTransfersSvc.ForeColor = System.Drawing.Color.Black
                Else
                    btnTransfersSvc.ForeColor = System.Drawing.Color.Gray
                End If

                If btnUpdateServiceContact.Enabled = True Then
                    btnUpdateServiceContact.ForeColor = System.Drawing.Color.Black
                Else
                    btnUpdateServiceContact.ForeColor = System.Drawing.Color.Gray
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

            'End If

        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "AccessControl", ex.Message.ToString, txtAccountID.Text)
        End Try
        '''''''''''''''''''Access Control 
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                tb1.Tabs(4).Visible = False
                MakeMeNull()
                EnableControls()

                MakeSvcNull()
                EnableSvcControls()

                MakeNotesNull()
                EnableNotesControls()

                MakeCPNull()
                EnableCPControls()

                AccessControl()

                txtCreatedOn.Attributes.Add("readonly", "readonly")
                txtGroupAuthority.Text = Session("SecGroupAuthority")
                'ddlLocation.Attributes.Add("disabled", "true")

                Dim UserID As String = Convert.ToString(Session("UserID"))
                txtCreatedBy.Text = UserID
                txtLastModifiedBy.Text = UserID

                'FindLocation()

                Dim Query As String
                Query = "SELECT LocationID, Location FROM tblGroupAccessLocation where GroupAccess = '" & txtGroupAuthority.Text.ToUpper & "'"
                PopulateDropDownList(Query, "LocationID", "LocationID", ddlLocation)
                PopulateDropDownList(Query, "LocationID", "LocationID", ddlBranch)
                PopulateDropDownList(Query, "LocationID", "LocationID", ddlBranchSearch)

                'SELECT companygroup FROM tblcompanygroup order by companygroup
                'SELECT locationgroup FROM tbllocationgroup order by locationgroup
                'SELECT City FROM tblcity WHERE (Rcno <> 0) ORDER BY City
                'SELECT Country FROM tblcountry WHERE (Rcno <> 0) ORDER BY Country
                'SELECT distinct (inchargeId) FROM tblteam where Status <> 'N' ORDER BY inchargeID
                'SELECT UPPER(industry) FROM tblindustry ORDER BY industry
                'SELECT StaffId FROM tblstaff where roles= 'SALES MAN' ORDER BY STAFFID
                'SELECT Terms,TermsDay FROM tblterms ORDER BY termsday,terms


                Query = "SELECT companygroup FROM tblcompanygroup order by companygroup"
                PopulateDropDownList(Query, "companygroup", "companygroup", ddlCompanyGrp)
                PopulateDropDownList(Query, "companygroup", "companygroup", ddlPersonGrpD)

                Query = "SELECT locationgroup FROM tbllocationgroup order by locationgroup"
                PopulateDropDownList(Query, "locationgroup", "locationgroup", ddlLocateGrp)

                Query = "SELECT StaffId FROM tblstaff where roles= 'SALES MAN' ORDER BY STAFFID"
                PopulateDropDownList(Query, "StaffId", "StaffId", ddlSalesMan)
                PopulateDropDownList(Query, "StaffId", "StaffId", ddlSalesManSvc)
                PopulateDropDownList(Query, "StaffId", "StaffId", ddlSearchSalesman)

                Query = "SELECT industry FROM tblindustry ORDER BY industry"
                'PopulateDropDownList(Query, "industry", "industry", ddlIndustry)
                PopulateDropDownList(Query, "industry", "industry", ddlIndustrysvc)

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

                txtDDLText.Text = "-1"

                btnTop.Attributes.Add("onclick", "javascript:scroll(0,0);return false;")
                btnTopDetail.Attributes.Add("onclick", "javascript:scroll(0,0);return false;")

                If Session("servicefrom") = "contactP" Then
                    txt.Text = Session("gridsql")
                    SqlDataSource1.SelectCommand = txt.Text
                    SqlDataSource1.DataBind()
                    GridView1.DataSourceID = "SqlDataSource1"


                    If String.IsNullOrEmpty(Session("rcno")) = False Then
                        txtRcno.Text = Session("rcno")
                        GridView1_SelectedIndexChanged(New Object(), New EventArgs)
                    End If

                    txtDetail.Text = Session("gridsqlPersonDetail")
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

                    Session.Remove("gridsqlPersonDetail")
                    Session.Remove("rcnoDetail")
                    ddlCurrency.SelectedIndex = -1

                    ddlCurrencyEdit.SelectedIndex = -1
                    ddlDefaultInvoiceFormatEdit.SelectedIndex = -1
                    ddlTermsEdit.SelectedIndex = -1

                ElseIf Session("contractfrom") = "clients" Then
                    txt.Text = Session("gridsqlPerson")
                    SqlDataSource1.SelectCommand = txt.Text
                    SqlDataSource1.DataBind()
                    GridView1.DataSourceID = "SqlDataSource1"

                    If String.IsNullOrEmpty(Session("rcno")) = False Then
                        'txtRcno.Text = Session("rcno")
                        GridView1_SelectedIndexChanged(New Object(), New EventArgs)
                    End If


                    txtDetail.Text = Session("gridsqlPersonDetail")
                    SqlDataSource2.SelectCommand = txtDetail.Text
                    SqlDataSource2.DataBind()
                    GridView2.DataSourceID = "SqlDataSource2"

                    If String.IsNullOrEmpty(Session("rcnoDetail")) = False Then
                        'txtRcno.Text = Session("rcno")
                        GridView2_SelectedIndexChanged(New Object(), New EventArgs)
                    End If

                    Session.Remove("contractfrom")
                    Session.Remove("rcno")
                    Session.Remove("accountid")
                    Session.Remove("locationid")

                    Session.Remove("gridsqlPersonDetail")
                    Session.Remove("rcnoDetail")

                    ''''''''''''''''''
                ElseIf Session("customerfrom") = "Residential" And Session("armodule") = "armodule" Then

                    ddlCompanyGrp.SelectedIndex = -1
                    'ddlIndustry.SelectedIndex = -1
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
                    btnTransactions_Click(sender, e)

                    ''''''''''''''''''''
                Else
                    'txt.Text = " SELECT a.Rcno, a.AccountId, a.InActive, a.ID, a.Name, a.ARCurrency, a.Location, b.Bal, a.TelMobile, a.TelFax, a.Address1, a.AddPOstal, a.BillAddress1, a.BillPostal, a.NRIC, a.ICType, a.Nationality, a.Sex, a.LocateGrp, a.PersonGroup, a.AccountNo, a.Salesman, a.AddStreet, a.AddBuilding, a.AddCity, a.AddState, a.AddCountry, a.BillStreet, a.BillBuilding, a.BillCity, a.BillState, a.BillCountry,  a.CreatedBy, a.CreatedOn, a.LastModifiedBy, a.LastModifiedOn,a.AutoEmailInvoice,a.AutoEmailSOA,a.UnsubscribeAutoEmailDate  "
                    'txt.Text = txt.Text + "  FROM tblperson a left join personbal b on a.Accountid = b.Accountid where a.Inactive=0 "


                    'If txtDisplayRecordsLocationwise.Text = "Y" Then
                    '    'txt.Text = txt.Text & " and Location = '" & txtLocation.Text & "'"
                    '    txt.Text = txt.Text & " and a.location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "')"

                    'End If


                    ''If txtDisplayRecordsLocationwise.Text = "Y" Then
                    ''    txt.Text = " SELECT distinct a.Rcno, a.AccountId, a.InActive, a.ID, a.Name, a.ARCurrency, a.Location, b.Bal, a.TelMobile, a.TelFax, a.Address1, a.AddPOstal, a.BillAddress1, a.BillPostal, a.NRIC, a.ICType, a.Nationality, a.Sex, a.LocateGrp, a.PersonGroup, a.AccountNo, a.Salesman, a.AddStreet, a.AddBuilding, A.AddCity, A.AddState, A.AddCountry, a.BillStreet, a.BillBuilding, A.BillCity, A.BillState, A.BillCountry,  a.CreatedBy, a.CreatedOn, a.LastModifiedBy, a.LastModifiedOn  FROM tblperson a left join personbal b on a.Accountid = b.Accountid left join tblPersonLocation c on a.Accountid = c.Accountid where a.Inactive=0 "
                    ''    'txt.Text = " SELECT * FROM tblperson a left join personbal b on a.Accountid = b.Accountid left join tblPersonLocation c on a.Accountid = c.Accountid where a.Inactive=0 "

                    ''    txt.Text = txt.Text + " and c.location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "')"
                    ''End If

                    ''If txtDisplayRecordsLocationwise.Text = "N" Then
                    ''txt.Text = " SELECT * FROM tblperson a left join CustomerBal b on a.Accountid = b.Accountid where a.Inactive=0 "
                    ''End If

                    'txt.Text = txt.Text & " ORDER BY a.rcno DESC, a.Name limit 100"

                    'SqlDataSource1.SelectCommand = txt.Text
                    'SqlDataSource1.DataBind()
                    'GridView1.DataSourceID = "SqlDataSource1"

                End If

                'txt.Text = "SELECT * FROM tblperson WHERE (Rcno <> 0) and Inactive=0 order by name limit 100"


                tb1.ActiveTabIndex = 0
                ModalPopupExtender1.Hide()

                'Dim Query As String

                'Query = "Select Terms, TermsDay from tblTerms order by Termsday,Terms"
                'PopulateDropDownList(Query, "Terms", "Terms", ddlTerms)
                'PopulateDropDownList(Query, "Terms", "Terms", ddlTermsSvc)


                Query = "SELECT CONCAT(ContractGroup, ' : ', GroupDescription) AS ContractGroup FROM tblcontractgroup order by ContractGroup"

                'Query = "Select ContractGroup from tblcontractgroup"
                PopulateDropDownList(Query, "ContractGroup", "ContractGroup", ddlContractGrp)
                PopulateDropDownList(Query, "ContractGroup", "ContractGroup", ddlContractGroupEdit)

                If Session("SecGroupAuthority") <> "ADMINISTRATOR" Then
                    tb1.Tabs(4).Visible = False
                End If

                txtCutOffDate.Text = Convert.ToDateTime(Now).ToString("dd/MM/yyyy")
                Session("cutoffoscustomer") = txtCutOffDate.Text
            Else
                Session.Remove("customerfrom")
            End If



            txtSearch.Attributes.Add("onblur", "WaterMark(this, event);")
            txtSearch.Attributes.Add("onfocus", "WaterMark(this, event);")

            ddlOffCity.Attributes.Add("onchange", "getCountry()")
            ddlBillCity.Attributes.Add("onchange", "getCountry()")
            ddlCity.Attributes.Add("onchange", "getCountry()")
            ddlBillCitySvc.Attributes.Add("onchange", "getCountry()")
            CheckTab()

            txtCreatedOn.ForeColor = txtCreatedOn.BackColor
        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "Page_load", ex.Message.ToString, txtAccountID.Text)
        End Try
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
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "PopulateDropDownList", ex.Message.ToString, txtAccountID.Text)
        End Try
    End Sub


    Private Sub CheckTab()
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


    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            'If txtNameE.Text = "" Then

            If txtDisplayRecordsLocationwise.Text = "Y" Then
                If ddlLocation.SelectedIndex = 0 Then
                    lblAlert.Text = "MASTER BRANCH CANNOT BE BLANK"
                    ddlLocation.Focus()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                    Exit Sub
                End If
            End If


            If String.IsNullOrEmpty(txtNameE.Text.Trim) = True Then
                ' MessageBox.Message.Alert(Page, "Name cannot be blank!!!", "str")
                lblAlert.Text = "NAME CANNOT BE BLANK"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                txtNameE.Focus()
                Return
            End If

            'If ddlCompanyGrp.Text = "-1" Then
            '    ' MessageBox.Message.Alert(Page, "Company Group cannot be blank!!!", "str")
            '    lblAlert.Text = "PERSON GROUP CANNOT BE BLANK"
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            '    Return

            'End If

            If Label30.Visible = True And String.IsNullOrEmpty(txtNRIC.Text.Trim) = True Then
                lblAlert.Text = "NRIC CANNOT BE BLANK"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                Return
            End If

            'If ddlSalesMan.Text = "-1" Then
            '    ' MessageBox.Message.Alert(Page, "Company Group cannot be blank!!!", "str")
            '    lblAlert.Text = "SALESMAN CANNOT BE BLANK"
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            '    Return

            'End If

            'If txtBillingName.Text = "" Then
            If String.IsNullOrEmpty(txtBillingName.Text.Trim) = True Then
                lblAlert.Text = "BILLING NAME CANNOT BE BLANK"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                txtBillingName.Focus()
                Return
            End If


            If String.IsNullOrEmpty(txtBillAddress.Text.Trim) = True Then
                lblAlert.Text = "BILL STREET ADDRESS1 CANNOT BE BLANK"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                txtBillAddress.Focus()
                Return
            End If

            'If txtBillCP1Contact.Text = "" Then
            If String.IsNullOrEmpty(txtBillCP1Contact.Text.Trim) = True Then
                lblAlert.Text = "BILLING CONTACT PERSON 1 CANNOT BE BLANK"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                txtBillCP1Contact.Focus()
                Return
            End If

            If ddlTerms.SelectedIndex = 0 Then
                ' MessageBox.Message.Alert(Page, "Company Group cannot be blank!!!", "str")
                lblAlert.Text = "TERMS CANNOT BE BLANK"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                Return

            End If

            If ddlCurrency.SelectedIndex = 0 Then
                ' MessageBox.Message.Alert(Page, "Company Group cannot be blank!!!", "str")
                lblAlert.Text = "CURRENCY CANNOT BE BLANK"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                Return
            End If


            If ddlOffCountry.SelectedIndex = 0 Then
                lblAlert.Text = "COUNTRY (OFFICE ADDRESS) CANNOT BE BLANK"
                ddlOffCountry.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                Exit Sub
            End If

            If ddlOffCity.SelectedIndex = 0 Then
                lblAlert.Text = "CITY CANNOT BE BLANK"
                ddlOffCity.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                Exit Sub
            End If

            If ddlBillCountry.SelectedIndex = 0 Then
                lblAlert.Text = "COUNTRY (BILL ADDRESS) CANNOT BE BLANK"
                ddlBillCountry.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                Exit Sub
            End If

            If String.IsNullOrEmpty(txtCreditLimit.Text.Trim) = True Then
                txtCreditLimit.Text = "0.00"
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

            If Validation() = False Then
                Return
            End If
            If txtMode.Text = "NEW" Then
                '  Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                If ValidateSave(conn) = False Then
                    'txtCreatedOn.Text = ""
                    'Return

                End If

                'Dim CustName As String = IgnoredWords(conn, txtNameE.Text.ToUpper)

                'Dim command1 As MySqlCommand = New MySqlCommand

                'command1.CommandType = CommandType.Text

                'command1.CommandText = "SELECT Name FROM tblperson where Name like '%" & CustName & "%'"
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

                'Dim command1 As MySqlCommand = New MySqlCommand

                'command1.CommandType = CommandType.Text

                'command1.CommandText = "SELECT * FROM tblperson where id=@id"
                'command1.Parameters.AddWithValue("@id", txtID.Text)
                'command1.Connection = conn

                'Dim dr As MySqlDataReader = command1.ExecuteReader()
                'Dim dt As New DataTable
                'dt.Load(dr)

                'If dt.Rows.Count > 0 Then

                '    MessageBox.Message.Alert(Page, "Record already exists!!!", "str")

                'Else

                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                Dim qry As String = "INSERT INTO tblperson(Id,Salutation,Name,Nric,CountryBirth,DateBirth,Citizenship,Race,Sex,MartialStatus,AddBlock,AddNos,AddFloor,AddUnit,AddBuilding,AddStreet,AddPostal,AddCity,AddState,AddCountry,TelHome,TelMobile,TelPager,TelFax,BillingAddress,BillBlock,BillNos,BillFloor,BillUnit,BillBuilding,BillStreet,"
                qry = qry + "BillPostal,BillCity,BillState,BillCountry,Company,ComBlock,ComNos,ComFloor,ComUnit,ComBuilding,ComStreet,ComPostal,ComCity,ComState,ComCountry,ComTel,ComFax,Location,Department,Profession,Appointment,Email,TelDirect,TelExtension,Comments,FinanceCompanyId,FinanceCompany,ArLimit,ApLimit,SalesLimit,PurchaseLimit,ApCurrency,ArCurrency,SendStatement,Status,Address1,"
                qry = qry + "BillAddress1,ComAddress1,Source,LocateGRP,SalesGRP,PriceGroup,StopSalesYN,StopPurchYN,SpecCode,ArWarning,Name2,Language,RefNo1,RefNo2,ICType,WorkPass,PersonGroup,Patient,Donor,Member,Religion,Occupation,MemberType,DateJoin,DateExpired,DateTerminate,GIROID,MemberID,Proposer,TemplateNo,ARLedger,ARSubLedger,APLedger,APSubLedger,CreatedBy,CreatedOn,"
                qry = qry + "LastModifiedBy,LastModifiedOn,Reason,Education,Boardmember,BoardDesignation,period,Intriducer,Organization,DriveLicNo,DriveLicExp,DriveLicCountry,PassportNo,DriveLicIssueDate,WebLoginID,WebLoginPassWord,WebAccessLevel,WebOneTimePassWord,chkLetterIndemnity,ARTERM,APTERM,"
                qry = qry + "SalesGST,ChkGstInclusive,RentalTerm,ArMethod,ApMethod,Dealer,WebLevel,DiscountPct,LoginID,Password,Functions,BillContactPerson,Remarks,DrivingSince,WorkPassExpDt,AcupunctureStatement,CardPrinted,Note,InChargeID,DealerYN,WebDisableYN,OTPYN,WebID,SubCompanyNo,"
                qry = qry + "SourceCompany,chkSendServiceReport,SoPriceGroup,AccountNo,FlowFrom,FlowTo,Salesman,InActive,AutoEmailServ,ReportFormatServ,WebUploadDate,DriveLicEffDt,BillTelHome,BillTelFax,BillTelMobile,BillTelPager,BillEmail,IsCustomer,IsSupplier,WebCreateDeviceID,WebCreateSource,WebFlowFrom,WebFlowTo,WebEditSource,WebDeleteStatus,WebLastEditDevice,"
                qry = qry + "AccountID,Nationality,ContactPerson,ResCP2Tel,ResCP2Fax,ResCP2Tel2,ResCP2Mobile,ResCP2Email,ContactPerson2,BillContact1Position,BillContact2,BillContact2Position,BillContact2Email,BillContact2Tel,BillContact2Fax,BillContact2Tel2,BillContact2Mobile,BillingSettings,BillingName,TermsDay, DefaultInvoiceFormat, AutoEmailInvoice, AutoEmailSOA,HardCopyInvoice,EmailNotificationOfSchedule,EmailNotificationOfJobProgress,MandatoryServiceReportPhotos,DisplayTimeInTimeOutInServiceReport,BillingOptionRemarks,RequireEBilling, TaxIdentificationNo, SalesTaxRegistrationNo)"
                qry = qry + "VALUES(@Id,@Salutation,@Name,@Nric,@CountryBirth,@DateBirth,@Citizenship,@Race,@Sex,@MartialStatus,@AddBlock,@AddNos,@AddFloor,@AddUnit,@AddBuilding,@AddStreet,@AddPostal,@AddCity,@AddState,@AddCountry,@TelHome,@TelMobile,@TelPager,@TelFax,@BillingAddress,"
                qry = qry + "@BillBlock,@BillNos,@BillFloor,@BillUnit,@BillBuilding,@BillStreet,@BillPostal,@BillCity,@BillState,@BillCountry,@Company,@ComBlock,@ComNos,@ComFloor,@ComUnit,@ComBuilding,@ComStreet,@ComPostal,@ComCity,@ComState,@ComCountry,@ComTel,@ComFax,@Location,"
                qry = qry + "@Department,@Profession,@Appointment,@Email,@TelDirect,@TelExtension,@Comments,@FinanceCompanyId,@FinanceCompany,@ArLimit,@ApLimit,@SalesLimit,@PurchaseLimit,@ApCurrency,@ArCurrency,@SendStatement,@Status,@Address1,@BillAddress1,@ComAddress1,@Source,"
                qry = qry + "@LocateGRP,@SalesGRP,@PriceGroup,@StopSalesYN,@StopPurchYN,@SpecCode,@ArWarning,@Name2,@Language,@RefNo1,@RefNo2,@ICType,@WorkPass,@PersonGroup,@Patient,@Donor,@Member,@Religion,@Occupation,@MemberType,@DateJoin,@DateExpired,@DateTerminate,"
                qry = qry + "@GIROID,@MemberID,@Proposer,@TemplateNo,@ARLedger,@ARSubLedger,@APLedger,@APSubLedger,@CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn,@Reason,@Education,@Boardmember,@BoardDesignation,@period,@Intriducer,@Organization,@DriveLicNo,@DriveLicExp,"
                qry = qry + "@DriveLicCountry,@PassportNo,@DriveLicIssueDate,@WebLoginID,@WebLoginPassWord,@WebAccessLevel,@WebOneTimePassWord,@chkLetterIndemnity,@ARTERM,@APTERM,@SalesGST,@ChkGstInclusive,@RentalTerm,@ArMethod,@ApMethod,@Dealer,@WebLevel,@DiscountPct,@LoginID,"
                qry = qry + "@Password,@Functions,@BillContactPerson,@Remarks,@DrivingSince,@WorkPassExpDt,@AcupunctureStatement,@CardPrinted,@Note,@InChargeID,@DealerYN,@WebDisableYN,@OTPYN,@WebID,@SubCompanyNo,@SourceCompany,@chkSendServiceReport,@SoPriceGroup,@AccountNo,"
                qry = qry + "@FlowFrom,@FlowTo,@Salesman,@InActive,@AutoEmailServ,@ReportFormatServ,@WebUploadDate,@DriveLicEffDt,@BillTelHome,@BillTelFax,@BillTelMobile,@BillTelPager,@BillEmail,@IsCustomer,@IsSupplier,@WebCreateDeviceID,@WebCreateSource,@WebFlowFrom,@WebFlowTo,@WebEditSource,@WebDeleteStatus,@WebLastEditDevice,"
                qry = qry + "@AccountID,@Nationality,@ContactPerson,@ResCP2Tel,@ResCP2Fax,@ResCP2Tel2,@ResCP2Mobile,@ResCP2Email,@ContactPerson2,@BillContact1Position,@BillContact2,@BillContact2Position,@BillContact2Email,@BillContact2Tel,@BillContact2Fax,@BillContact2Tel2,@BillContact2Mobile,@BillingSettings,@BillingName,@TermsDay, @DefaultInvoiceFormat, @AutoEmailInvoice, @AutoEmailSOA,@HardCopyInvoice,@EmailNotificationOfSchedule,@EmailNotificationOfJobProgress,@MandatoryServiceReportPhotos,@DisplayTimeInTimeOutInServiceReport,@BillingOptionRemarks,@RequireEBilling,  @TaxIdentificationNo, @SalesTaxRegistrationNo);"
                command.CommandText = qry
                command.Parameters.Clear()

                command.Parameters.AddWithValue("@Id", "")
                If ddlSalute.Text = txtDDLText.Text Then
                    command.Parameters.AddWithValue("@Salutation", "")
                Else
                    command.Parameters.AddWithValue("@Salutation", ddlSalute.Text)
                End If

                command.Parameters.AddWithValue("@Name", txtNameE.Text.ToUpper)
                command.Parameters.AddWithValue("@Nric", txtNRIC.Text.ToUpper)
                command.Parameters.AddWithValue("@CountryBirth", "")
                command.Parameters.AddWithValue("@DateBirth", DBNull.Value)
                command.Parameters.AddWithValue("@Citizenship", "")
                command.Parameters.AddWithValue("@Race", "")
                If ddlSex.Text = txtDDLText.Text Then
                    command.Parameters.AddWithValue("@Sex", "")
                Else
                    command.Parameters.AddWithValue("@Sex", ddlSex.Text.ToUpper)
                End If
                command.Parameters.AddWithValue("@Email", txtOffEmail.Text.ToUpper)
                command.Parameters.AddWithValue("@MartialStatus", "")
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

                command.Parameters.AddWithValue("@TelHome", txtOffContactNo.Text.ToUpper)
                command.Parameters.AddWithValue("@TelFax", txtOffFax.Text.ToUpper)
                command.Parameters.AddWithValue("@TelPager", txtOffContact2.Text.ToUpper)
                command.Parameters.AddWithValue("@TelMobile", txtOffMobile.Text.ToUpper)

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
                command.Parameters.AddWithValue("@Company", "")
                command.Parameters.AddWithValue("@ComBlock", "")
                command.Parameters.AddWithValue("@ComNos", "")
                command.Parameters.AddWithValue("@ComFloor", "")
                command.Parameters.AddWithValue("@ComUnit", "")
                command.Parameters.AddWithValue("@ComBuilding", "")
                command.Parameters.AddWithValue("@ComStreet", "")
                command.Parameters.AddWithValue("@ComPostal", "")
                command.Parameters.AddWithValue("@ComCity", "")
                command.Parameters.AddWithValue("@ComState", "")
                command.Parameters.AddWithValue("@ComCountry", "")
                command.Parameters.AddWithValue("@ComTel", "")
                command.Parameters.AddWithValue("@ComFax", "")

                If ddlLocation.SelectedIndex = 0 Then
                    command.Parameters.AddWithValue("@Location", "")
                Else
                    command.Parameters.AddWithValue("@Location", ddlLocation.Text)
                End If

                'command.Parameters.AddWithValue("@Location", ddlLocation.Text)
                command.Parameters.AddWithValue("@Department", "")
                command.Parameters.AddWithValue("@Profession", "")
                command.Parameters.AddWithValue("@Appointment", "")
                command.Parameters.AddWithValue("@TelDirect", "")
                command.Parameters.AddWithValue("@TelExtension", "")
                command.Parameters.AddWithValue("@Comments", txtComments.Text.ToUpper)
                command.Parameters.AddWithValue("@FinanceCompanyId", "")
                command.Parameters.AddWithValue("@FinanceCompany", "")
                command.Parameters.AddWithValue("@ArLimit", txtCreditLimit.Text)
                command.Parameters.AddWithValue("@ApLimit", 0)
                command.Parameters.AddWithValue("@SalesLimit", 0)
                command.Parameters.AddWithValue("@PurchaseLimit", 0)
                command.Parameters.AddWithValue("@ApCurrency", 0)
                command.Parameters.AddWithValue("@ArCurrency", ddlCurrency.SelectedItem.Text.ToUpper)
                command.Parameters.AddWithValue("@SendStatement", chkSendStatementSOA.Checked)
                command.Parameters.AddWithValue("@HardCopyInvoice", chkSendStatementInv.Checked)

                command.Parameters.AddWithValue("@EmailNotificationOfSchedule", chkEmailNotifySchedule.Checked)
                command.Parameters.AddWithValue("@EmailNotificationOfJobProgress", chkEmailNotifyJobProgress.Checked)
                command.Parameters.AddWithValue("@MandatoryServiceReportPhotos", chkPhotosMandatory.Checked)
                command.Parameters.AddWithValue("@DisplayTimeInTimeOutInServiceReport", chkDisplayTimeInTimeOut.Checked)

                command.Parameters.AddWithValue("@Status", ddlStatus.Text.ToUpper)

                command.Parameters.AddWithValue("@Address1", txtOffAddress1.Text.ToUpper)
                command.Parameters.AddWithValue("@BillAddress1", txtBillAddress.Text.ToUpper)
                command.Parameters.AddWithValue("@ComAddress1", "")
                command.Parameters.AddWithValue("@Source", "")
                command.Parameters.AddWithValue("@LocateGRP", "")
                command.Parameters.AddWithValue("@SalesGRP", "")

                command.Parameters.AddWithValue("@PriceGroup", "")

                command.Parameters.AddWithValue("@StopSalesYN", "")
                command.Parameters.AddWithValue("@StopPurchYN", "")
                command.Parameters.AddWithValue("@SpecCode", "")
                command.Parameters.AddWithValue("@ArWarning", 0)
                command.Parameters.AddWithValue("@Name2", txtNameO.Text.ToUpper)
                command.Parameters.AddWithValue("@Language", "")
                command.Parameters.AddWithValue("@RefNo1", "")
                command.Parameters.AddWithValue("@RefNo2", "")
                If ddlIC.Text = txtDDLText.Text Then
                    command.Parameters.AddWithValue("@ICType", "")
                Else
                    command.Parameters.AddWithValue("@ICType", ddlIC.Text.ToUpper)
                End If
                command.Parameters.AddWithValue("@WorkPass", "")
                If ddlCompanyGrp.Text = txtDDLText.Text Then
                    command.Parameters.AddWithValue("@PersonGroup", "")
                Else
                    command.Parameters.AddWithValue("@PersonGroup", ddlCompanyGrp.Text.ToUpper)
                End If

                command.Parameters.AddWithValue("@Patient", 0)
                command.Parameters.AddWithValue("@Donor", 0)
                command.Parameters.AddWithValue("@Member", 0)
                command.Parameters.AddWithValue("@Religion", "")
                command.Parameters.AddWithValue("@Occupation", "")
                command.Parameters.AddWithValue("@MemberType", "")
                If txtStartDate.Text = "" Then
                    command.Parameters.AddWithValue("@DateJoin", DBNull.Value)
                Else
                    command.Parameters.AddWithValue("@DateJoin", Convert.ToDateTime(txtStartDate.Text).ToString("yyyy-MM-dd"))
                End If
                command.Parameters.AddWithValue("@DateExpired", DBNull.Value)
                command.Parameters.AddWithValue("@DateTerminate", DBNull.Value)
                command.Parameters.AddWithValue("@GIROID", "")
                command.Parameters.AddWithValue("@MemberID", "")
                command.Parameters.AddWithValue("@Proposer", "")
                command.Parameters.AddWithValue("@TemplateNo", "")
                command.Parameters.AddWithValue("@ARLedger", "")
                command.Parameters.AddWithValue("@ARSubLedger", "")
                command.Parameters.AddWithValue("@APLedger", "")
                command.Parameters.AddWithValue("@APSubLedger", "")
                command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))

                command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                command.Parameters.AddWithValue("@Reason", "")
                command.Parameters.AddWithValue("@Education", "")
                command.Parameters.AddWithValue("@Boardmember", "")
                command.Parameters.AddWithValue("@BoardDesignation", "")
                command.Parameters.AddWithValue("@period", "")
                command.Parameters.AddWithValue("@Intriducer", "")
                command.Parameters.AddWithValue("@Organization", "")
                command.Parameters.AddWithValue("@DriveLicNo", "")
                command.Parameters.AddWithValue("@DriveLicExp", DBNull.Value)
                command.Parameters.AddWithValue("@DriveLicCountry", "")
                command.Parameters.AddWithValue("@PassportNo", "")
                command.Parameters.AddWithValue("@DriveLicIssueDate", DBNull.Value)
                command.Parameters.AddWithValue("@WebLoginID", "")
                command.Parameters.AddWithValue("@WebLoginPassWord", "")
                command.Parameters.AddWithValue("@WebAccessLevel", 0)
                command.Parameters.AddWithValue("@WebOneTimePassWord", "")
                command.Parameters.AddWithValue("@chkLetterIndemnity", 0)
                command.Parameters.AddWithValue("@ARTERM", ddlTerms.Text.ToUpper)
                command.Parameters.AddWithValue("@APTERM", "")
                command.Parameters.AddWithValue("@SalesGST", "")
                command.Parameters.AddWithValue("@ChkGstInclusive", "")
                command.Parameters.AddWithValue("@RentalTerm", "")
                command.Parameters.AddWithValue("@ArMethod", "")
                command.Parameters.AddWithValue("@ApMethod", "")
                command.Parameters.AddWithValue("@Dealer", "")
                command.Parameters.AddWithValue("@WebLevel", "")
                command.Parameters.AddWithValue("@DiscountPct", 0)
                command.Parameters.AddWithValue("@LoginID", "")
                command.Parameters.AddWithValue("@Password", "")
                command.Parameters.AddWithValue("@Functions", "")
                command.Parameters.AddWithValue("@BillContactPerson", txtBillCP1Contact.Text.ToUpper)

                command.Parameters.AddWithValue("@Remarks", "")
                command.Parameters.AddWithValue("@DrivingSince", DBNull.Value)
                command.Parameters.AddWithValue("@WorkPassExpDt", DBNull.Value)
                command.Parameters.AddWithValue("@AcupunctureStatement", 0)
                command.Parameters.AddWithValue("@CardPrinted", 0)
                command.Parameters.AddWithValue("@Note", "")
                If ddlIncharge.Text = txtDDLText.Text Then
                    command.Parameters.AddWithValue("@InChargeID", "")

                Else
                    command.Parameters.AddWithValue("@InChargeID", ddlIncharge.Text.ToUpper)

                End If

                command.Parameters.AddWithValue("@DealerYN", 0)
                command.Parameters.AddWithValue("@WebDisableYN", 0)
                command.Parameters.AddWithValue("@OTPYN", 0)
                command.Parameters.AddWithValue("@WebID", "")
                command.Parameters.AddWithValue("@SubCompanyNo", "")
                command.Parameters.AddWithValue("@SourceCompany", "")
                command.Parameters.AddWithValue("@chkSendServiceReport", 0)
                command.Parameters.AddWithValue("@SoPriceGroup", "")
                command.Parameters.AddWithValue("@AccountNo", "")
                command.Parameters.AddWithValue("@FlowFrom", "")
                command.Parameters.AddWithValue("@FlowTo", "")

                If ddlSalesMan.Text = txtDDLText.Text Then
                    command.Parameters.AddWithValue("@SalesMan", "")
                Else
                    command.Parameters.AddWithValue("@SalesMan", ddlSalesMan.Text.ToUpper)
                End If

                command.Parameters.AddWithValue("@InActive", 0)

                command.Parameters.AddWithValue("@AutoEmailServ", 0)
                command.Parameters.AddWithValue("@ReportFormatServ", "")
                command.Parameters.AddWithValue("@WebUploadDate", DBNull.Value)
                command.Parameters.AddWithValue("@DriveLicEffDt", DBNull.Value)
                command.Parameters.AddWithValue("@BillTelHome", txtBillCP1Tel.Text.ToUpper)
                command.Parameters.AddWithValue("@BillTelFax", txtBillCP1Fax.Text.ToUpper)
                command.Parameters.AddWithValue("@BillTelPager", txtBillCP1Tel2.Text.ToUpper)
                command.Parameters.AddWithValue("@BillTelMobile", txtBillCP1Mobile.Text.ToUpper)
                command.Parameters.AddWithValue("@BillEmail", txtBillCP1Email.Text.ToUpper)

                command.Parameters.AddWithValue("@IsCustomer", 0)

                command.Parameters.AddWithValue("@IsSupplier", 0)

                command.Parameters.AddWithValue("@WebCreateDeviceID", "")
                command.Parameters.AddWithValue("@WebCreateSource", "")
                command.Parameters.AddWithValue("@WebFlowFrom", "")
                command.Parameters.AddWithValue("@WebFlowTo", "")
                command.Parameters.AddWithValue("@WebEditSource", "")
                command.Parameters.AddWithValue("@WebDeleteStatus", "")
                command.Parameters.AddWithValue("@WebLastEditDevice", "")

                If ddlNationality.Text = txtDDLText.Text Then
                    command.Parameters.AddWithValue("@Nationality", "")
                Else
                    command.Parameters.AddWithValue("@Nationality", ddlNationality.Text.ToUpper)
                End If

                command.Parameters.AddWithValue("@ContactPerson", txtOffContactPerson.Text.ToUpper)
                command.Parameters.AddWithValue("@ResCP2Tel", txtOffCont1Tel.Text.ToUpper)
                command.Parameters.AddWithValue("@ResCP2Fax", txtOffCont1Fax.Text.ToUpper)
                command.Parameters.AddWithValue("@ResCP2Tel2", txtOffCont1Tel2.Text.ToUpper)
                command.Parameters.AddWithValue("@ResCP2Mobile", txtOffCont1Mobile.Text.ToUpper)
                command.Parameters.AddWithValue("@ResCP2Email", txtOffCont1Email.Text.ToUpper)
                command.Parameters.AddWithValue("@ContactPerson2", txtOffCont1Name.Text.ToUpper)
                command.Parameters.AddWithValue("@BillContact1Position", txtBillCP1Position.Text.ToUpper)
                command.Parameters.AddWithValue("@BillContact2", txtBillCP2Contact.Text.ToUpper)
                command.Parameters.AddWithValue("@BillContact2Position", txtBillCP2Position.Text.ToUpper)
                command.Parameters.AddWithValue("@BillContact2Email", txtBillCP2Email.Text.ToUpper)
                command.Parameters.AddWithValue("@BillContact2Tel", txtBillCP2Tel.Text.ToUpper)
                command.Parameters.AddWithValue("@BillContact2Fax", txtBillCP2Fax.Text.ToUpper)
                command.Parameters.AddWithValue("@BillContact2Tel2", txtBillCP2Tel2.Text.ToUpper)
                command.Parameters.AddWithValue("@BillContact2Mobile", txtBillCP2Mobile.Text.ToUpper)
                command.Parameters.AddWithValue("@BillingSettings", rdbBillingSettings.SelectedValue.ToString)
                command.Parameters.AddWithValue("@BillingName", txtBillingName.Text.ToUpper)
                'command.Parameters.AddWithValue("@Terms", ddlTerms.SelectedItem.Text)
                command.Parameters.AddWithValue("@TermsDay", 0)
                command.Parameters.AddWithValue("@BillingOptionRemarks", txtBillingOptionRemarks.Text.ToUpper)

                If ddlDefaultInvoiceFormat.SelectedIndex = 0 Then
                    command.Parameters.AddWithValue("@DefaultInvoiceFormat", "")
                Else
                    command.Parameters.AddWithValue("@DefaultInvoiceFormat", ddlDefaultInvoiceFormat.Text)
                End If

                command.Parameters.AddWithValue("@AutoEmailInvoice", chkAutoEmailInvoice.Checked)
                command.Parameters.AddWithValue("@AutoEmailSOA", chkAutoEmailStatement.Checked)
                command.Parameters.AddWithValue("@RequireEBilling", chkRequireEBilling.Checked)

                command.Parameters.AddWithValue("@TaxIdentificationNo", txtTIN.Text.ToUpper)
                command.Parameters.AddWithValue("@SalesTaxRegistrationNo", txtSST.Text.ToUpper)

                'command.Parameters.AddWithValue("@Location", txtLocation.Text)

                'If ddlLocation.SelectedIndex = 0 Then
                '    command.Parameters.AddWithValue("@Location", "")
                'Else
                '    command.Parameters.AddWithValue("@Location", ddlLocation.Text)
                'End If
                GenerateAccountNo()
                command.Parameters.AddWithValue("@AccountID", txtAccountID.Text)
                command.Connection = conn
                command.ExecuteNonQuery()
                InsertContactMaster(conn)
                lblMessage.Text = "ADD: RECORD SUCCESSFULLY ADDED"
                lblAlert.Text = ""
                txtRcno.Text = command.LastInsertedId
                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "PERS", txtAccountID.Text, "ADD", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtAccountID.Text, "", txtRcno.Text)

                lblAccountID.Text = txtAccountID.Text
                txtAccountIDtab2.Text = txtAccountID.Text
                lblName.Text = txtNameE.Text
                lblAccountID2.Text = txtAccountID.Text
                lblName2.Text = txtNameE.Text
                txtAccountIDSelected.Text = txtAccountID.Text

                conn.Close()
                conn.Dispose()

                'Catch ex As Exception
                '    MessageBox.Message.Alert(Page, "Error!!!" + ex.Message.ToString, "str")
                'End Try
                EnableControls()

            ElseIf txtMode.Text = "EDIT" Then
                If txtRcno.Text = "" Then
                    MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
                    Return

                End If
                '    Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command2 As MySqlCommand = New MySqlCommand

                command2.CommandType = CommandType.Text

                command2.CommandText = "SELECT * FROM tblperson where accountid=@id and rcno<>" & Convert.ToInt32(txtRcno.Text)
                command2.Parameters.AddWithValue("@id", txtAccountID.Text)
                command2.Connection = conn

                Dim dr1 As MySqlDataReader = command2.ExecuteReader()
                Dim dt1 As New DataTable
                dt1.Load(dr1)

                If dt1.Rows.Count > 0 Then

                    MessageBox.Message.Alert(Page, "Account ID already exists!!!", "str")

                Else

                    Dim command1 As MySqlCommand = New MySqlCommand

                    command1.CommandType = CommandType.Text

                    command1.CommandText = "SELECT * FROM tblperson where rcno=" & Convert.ToInt32(txtRcno.Text)
                    command1.Connection = conn

                    Dim dr As MySqlDataReader = command1.ExecuteReader()
                    Dim dt As New DataTable
                    dt.Load(dr)

                    If dt.Rows.Count > 0 Then

                        Dim command As MySqlCommand = New MySqlCommand

                        command.CommandType = CommandType.Text
                        Dim qry As String = "UPDATE tblperson SET Id = @Id,Salutation = @Salutation,Name = @Name,Nric = @Nric,Sex = @Sex,Email=@Email,AddBlock = @AddBlock,AddNos = @AddNos,AddFloor = @AddFloor,AddUnit = @AddUnit,AddBuilding = @AddBuilding,AddStreet = @AddStreet,AddPostal = @AddPostal,AddCity = @AddCity,AddState = @AddState,AddCountry = @AddCountry,TelHome = @TelHome,TelMobile = @TelMobile,TelPager = @TelPager,TelFax = @TelFax,BillingAddress = @BillingAddress,BillBlock = @BillBlock,BillNos = @BillNos,BillFloor = @BillFloor,BillUnit = @BillUnit,BillBuilding = @BillBuilding,BillStreet = @BillStreet,BillPostal = @BillPostal,BillCity = @BillCity,BillState = @BillState,BillCountry = @BillCountry,Comments = @Comments,Address1 = @Address1,BillAddress1 = @BillAddress1,LocateGRP = @LocateGRP,SalesGRP = @SalesGRP,Name2 = @Name2,ICType = @ICType,PersonGroup = @PersonGroup,LastModifiedBy = @LastModifiedBy,LastModifiedOn = @LastModifiedOn,Salesman = @Salesman,BillTelHome = @BillTelHome,BillTelFax = @BillTelFax,BillTelMobile = @BillTelMobile,BillTelPager = @BillTelPager,BillEmail = @BillEmail,BillContactPerson=@BillContactPerson,IsCustomer = @IsCustomer,IsSupplier = @IsSupplier, Accountno= @Accountno, status=@Status,Inactive=@Inactive,Nationality = @Nationality,ContactPerson = @ContactPerson,ResCP2Tel = @ResCP2Tel,ResCP2Fax = @ResCP2Fax,ResCP2Tel2 = @ResCP2Tel2,ResCP2Mobile = @ResCP2Mobile,ResCP2Email = @ResCP2Email,ContactPerson2 = @ContactPerson2,BillContact1Position = @BillContact1Position,BillContact2 = @BillContact2,BillContact2Position = @BillContact2Position,BillContact2Tel = @BillContact2Tel,BillContact2Fax = @BillContact2Fax,BillContact2Tel2 = @BillContact2Tel2,BillContact2Mobile = @BillContact2Mobile,BillContact2Email = @BillContact2Email,BillingSettings=@BillingSettings,BillingName=@BillingName,ArTerm=@Terms,TermsDay=@TermsDay, ArCurrency =@Currency, SendStatement =@SendStatement, DefaultInvoiceFormat=@DefaultInvoiceFormat, AutoEmailInvoice=@AutoEmailInvoice, AutoEmailSOA=@AutoEmailSOA, ArLimit=@ArLimit,HardCopyInvoice=@HardCopyInvoice,EmailNotificationOfSchedule=@EmailNotificationOfSchedule,EmailNotificationOfJobProgress=@EmailNotificationOfJobProgress,MandatoryServiceReportPhotos=@MandatoryServiceReportPhotos,DisplayTimeInTimeOutInServiceReport=@DisplayTimeInTimeOutInServiceReport,BillingOptionRemarks=@BillingOptionRemarks,RequireEBilling=@RequireEBilling,Location=@Location, TaxIdentificationNo =@TaxIdentificationNo, SalesTaxRegistrationNo =@SalesTaxRegistrationNo WHERE  rcno=" & Convert.ToInt32(txtRcno.Text)

                        command.CommandText = qry
                        command.Parameters.Clear()

                        command.Parameters.AddWithValue("@Id", "")
                        If ddlSalute.Text = txtDDLText.Text Then
                            command.Parameters.AddWithValue("@Salutation", "")
                        Else
                            command.Parameters.AddWithValue("@Salutation", ddlSalute.Text.ToUpper)
                        End If

                        command.Parameters.AddWithValue("@Name", txtNameE.Text.ToUpper)
                        command.Parameters.AddWithValue("@Nric", txtNRIC.Text.ToUpper)
                        command.Parameters.AddWithValue("@CountryBirth", "")
                        command.Parameters.AddWithValue("@DateBirth", DBNull.Value)
                        command.Parameters.AddWithValue("@Citizenship", "")
                        command.Parameters.AddWithValue("@Race", "")
                        If ddlSex.Text = txtDDLText.Text Then
                            command.Parameters.AddWithValue("@Sex", "")
                        Else
                            command.Parameters.AddWithValue("@Sex", ddlSex.Text.ToUpper)
                        End If
                        command.Parameters.AddWithValue("@Email", txtOffEmail.Text.ToUpper)
                        command.Parameters.AddWithValue("@MartialStatus", "")
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

                        command.Parameters.AddWithValue("@TelHome", txtOffContactNo.Text.ToUpper)
                        command.Parameters.AddWithValue("@TelFax", txtOffFax.Text.ToUpper)
                        command.Parameters.AddWithValue("@TelPager", txtOffContact2.Text.ToUpper)
                        command.Parameters.AddWithValue("@TelMobile", txtOffMobile.Text.ToUpper)

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
                        command.Parameters.AddWithValue("@Company", "")
                        command.Parameters.AddWithValue("@ComBlock", "")
                        command.Parameters.AddWithValue("@ComNos", "")
                        command.Parameters.AddWithValue("@ComFloor", "")
                        command.Parameters.AddWithValue("@ComUnit", "")
                        command.Parameters.AddWithValue("@ComBuilding", "")
                        command.Parameters.AddWithValue("@ComStreet", "")
                        command.Parameters.AddWithValue("@ComPostal", "")
                        command.Parameters.AddWithValue("@ComCity", "")
                        command.Parameters.AddWithValue("@ComState", "")
                        command.Parameters.AddWithValue("@ComCountry", "")
                        command.Parameters.AddWithValue("@ComTel", "")
                        command.Parameters.AddWithValue("@ComFax", "")

                        If ddlLocation.SelectedIndex = 0 Then
                            command.Parameters.AddWithValue("@Location", "")
                        Else
                            command.Parameters.AddWithValue("@Location", ddlLocation.Text)
                        End If

                        'command.Parameters.AddWithValue("@Location", ddlLocation.Text)
                        command.Parameters.AddWithValue("@Department", "")
                        command.Parameters.AddWithValue("@Profession", "")
                        command.Parameters.AddWithValue("@Appointment", "")
                        command.Parameters.AddWithValue("@TelDirect", "")
                        command.Parameters.AddWithValue("@TelExtension", "")
                        command.Parameters.AddWithValue("@Comments", txtComments.Text.ToUpper)
                        command.Parameters.AddWithValue("@FinanceCompanyId", "")
                        command.Parameters.AddWithValue("@FinanceCompany", "")
                        command.Parameters.AddWithValue("@ArLimit", txtCreditLimit.Text)
                        command.Parameters.AddWithValue("@ApLimit", 0)
                        command.Parameters.AddWithValue("@SalesLimit", 0)
                        command.Parameters.AddWithValue("@PurchaseLimit", 0)
                        command.Parameters.AddWithValue("@ApCurrency", 0)
                        'command.Parameters.AddWithValue("@ArCurrency", ddlCurrency.SelectedItem.Text)
                        command.Parameters.AddWithValue("@SendStatement", chkSendStatementSOA.Checked)
                        command.Parameters.AddWithValue("@HardCopyInvoice", chkSendStatementInv.Checked)

                        command.Parameters.AddWithValue("@EmailNotificationOfSchedule", chkEmailNotifySchedule.Checked)
                        command.Parameters.AddWithValue("@EmailNotificationOfJobProgress", chkEmailNotifyJobProgress.Checked)
                        command.Parameters.AddWithValue("@MandatoryServiceReportPhotos", chkPhotosMandatory.Checked)
                        command.Parameters.AddWithValue("@DisplayTimeInTimeOutInServiceReport", chkDisplayTimeInTimeOut.Checked)

                        command.Parameters.AddWithValue("@Status", ddlStatus.Text)

                        command.Parameters.AddWithValue("@Address1", txtOffAddress1.Text.ToUpper)
                        command.Parameters.AddWithValue("@BillAddress1", txtBillAddress.Text.ToUpper)
                        command.Parameters.AddWithValue("@ComAddress1", "")
                        command.Parameters.AddWithValue("@Source", "")
                        command.Parameters.AddWithValue("@LocateGRP", "")
                        command.Parameters.AddWithValue("@SalesGRP", "")

                        command.Parameters.AddWithValue("@PriceGroup", "")

                        command.Parameters.AddWithValue("@StopSalesYN", "")
                        command.Parameters.AddWithValue("@StopPurchYN", "")
                        command.Parameters.AddWithValue("@SpecCode", "")
                        command.Parameters.AddWithValue("@ArWarning", 0)
                        command.Parameters.AddWithValue("@Name2", txtNameO.Text.ToUpper)
                        command.Parameters.AddWithValue("@Language", "")
                        command.Parameters.AddWithValue("@RefNo1", "")
                        command.Parameters.AddWithValue("@RefNo2", "")
                        If ddlIC.Text = txtDDLText.Text Then
                            command.Parameters.AddWithValue("@ICType", "")
                        Else
                            command.Parameters.AddWithValue("@ICType", ddlIC.Text.ToUpper)
                        End If
                        command.Parameters.AddWithValue("@WorkPass", "")
                        If ddlCompanyGrp.Text = txtDDLText.Text Then
                            command.Parameters.AddWithValue("@PersonGroup", "")
                        Else
                            command.Parameters.AddWithValue("@PersonGroup", ddlCompanyGrp.Text.ToUpper)
                        End If

                        command.Parameters.AddWithValue("@Patient", 0)
                        command.Parameters.AddWithValue("@Donor", 0)
                        command.Parameters.AddWithValue("@Member", 0)
                        command.Parameters.AddWithValue("@Religion", "")
                        command.Parameters.AddWithValue("@Occupation", "")
                        command.Parameters.AddWithValue("@MemberType", "")
                        If txtStartDate.Text = "" Then
                            command.Parameters.AddWithValue("@DateJoin", DBNull.Value)
                        Else
                            command.Parameters.AddWithValue("@DateJoin", Convert.ToDateTime(txtStartDate.Text).ToString("yyyy-MM-dd"))
                        End If
                        command.Parameters.AddWithValue("@DateExpired", DBNull.Value)
                        command.Parameters.AddWithValue("@DateTerminate", DBNull.Value)
                        command.Parameters.AddWithValue("@GIROID", "")
                        command.Parameters.AddWithValue("@MemberID", "")
                        command.Parameters.AddWithValue("@Proposer", "")
                        command.Parameters.AddWithValue("@TemplateNo", "")
                        command.Parameters.AddWithValue("@ARLedger", "")
                        command.Parameters.AddWithValue("@ARSubLedger", "")
                        command.Parameters.AddWithValue("@APLedger", "")
                        command.Parameters.AddWithValue("@APSubLedger", "")
                        'command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                        'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))

                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                        command.Parameters.AddWithValue("@Reason", "")
                        command.Parameters.AddWithValue("@Education", "")
                        command.Parameters.AddWithValue("@Boardmember", "")
                        command.Parameters.AddWithValue("@BoardDesignation", "")
                        command.Parameters.AddWithValue("@period", "")
                        command.Parameters.AddWithValue("@Intriducer", "")
                        command.Parameters.AddWithValue("@Organization", "")
                        command.Parameters.AddWithValue("@DriveLicNo", "")
                        command.Parameters.AddWithValue("@DriveLicExp", DBNull.Value)
                        command.Parameters.AddWithValue("@DriveLicCountry", "")
                        command.Parameters.AddWithValue("@PassportNo", "")
                        command.Parameters.AddWithValue("@DriveLicIssueDate", DBNull.Value)
                        command.Parameters.AddWithValue("@WebLoginID", "")
                        command.Parameters.AddWithValue("@WebLoginPassWord", "")
                        command.Parameters.AddWithValue("@WebAccessLevel", 0)
                        command.Parameters.AddWithValue("@WebOneTimePassWord", "")
                        command.Parameters.AddWithValue("@chkLetterIndemnity", 0)
                        'command.Parameters.AddWithValue("@ARTERM", "")
                        command.Parameters.AddWithValue("@APTERM", "")
                        command.Parameters.AddWithValue("@SalesGST", "")
                        command.Parameters.AddWithValue("@ChkGstInclusive", "")
                        command.Parameters.AddWithValue("@RentalTerm", "")
                        command.Parameters.AddWithValue("@ArMethod", "")
                        command.Parameters.AddWithValue("@ApMethod", "")
                        command.Parameters.AddWithValue("@Dealer", "")
                        command.Parameters.AddWithValue("@WebLevel", "")
                        command.Parameters.AddWithValue("@DiscountPct", 0)
                        command.Parameters.AddWithValue("@LoginID", "")
                        command.Parameters.AddWithValue("@Password", "")
                        command.Parameters.AddWithValue("@Functions", "")
                        command.Parameters.AddWithValue("@BillContactPerson", txtBillCP1Contact.Text.ToUpper)

                        command.Parameters.AddWithValue("@Remarks", "")
                        command.Parameters.AddWithValue("@DrivingSince", DBNull.Value)
                        command.Parameters.AddWithValue("@WorkPassExpDt", DBNull.Value)
                        command.Parameters.AddWithValue("@AcupunctureStatement", 0)
                        command.Parameters.AddWithValue("@CardPrinted", 0)
                        command.Parameters.AddWithValue("@Note", "")
                        If ddlIncharge.Text = txtDDLText.Text Then
                            command.Parameters.AddWithValue("@InChargeID", "")

                        Else
                            command.Parameters.AddWithValue("@InChargeID", ddlIncharge.Text)

                        End If

                        command.Parameters.AddWithValue("@DealerYN", 0)
                        command.Parameters.AddWithValue("@WebDisableYN", 0)
                        command.Parameters.AddWithValue("@OTPYN", 0)
                        command.Parameters.AddWithValue("@WebID", "")
                        command.Parameters.AddWithValue("@SubCompanyNo", "")
                        command.Parameters.AddWithValue("@SourceCompany", "")
                        command.Parameters.AddWithValue("@chkSendServiceReport", 0)
                        command.Parameters.AddWithValue("@SoPriceGroup", "")
                        command.Parameters.AddWithValue("@AccountNo", "")
                        command.Parameters.AddWithValue("@FlowFrom", "")
                        command.Parameters.AddWithValue("@FlowTo", "")

                        If ddlSalesMan.Text = txtDDLText.Text Then
                            command.Parameters.AddWithValue("@SalesMan", "")
                        Else
                            command.Parameters.AddWithValue("@SalesMan", ddlSalesMan.Text)
                        End If

                        command.Parameters.AddWithValue("@InActive", 0)

                        command.Parameters.AddWithValue("@AutoEmailServ", 0)
                        command.Parameters.AddWithValue("@ReportFormatServ", "")
                        command.Parameters.AddWithValue("@WebUploadDate", DBNull.Value)
                        command.Parameters.AddWithValue("@DriveLicEffDt", DBNull.Value)
                        command.Parameters.AddWithValue("@BillTelHome", txtBillCP1Tel.Text.ToUpper)
                        command.Parameters.AddWithValue("@BillTelFax", txtBillCP1Fax.Text.ToUpper)
                        command.Parameters.AddWithValue("@BillTelPager", txtBillCP1Tel2.Text.ToUpper)
                        command.Parameters.AddWithValue("@BillTelMobile", txtBillCP1Mobile.Text.ToUpper)
                        command.Parameters.AddWithValue("@BillEmail", txtBillCP1Email.Text.ToUpper)

                        command.Parameters.AddWithValue("@IsCustomer", 0)

                        command.Parameters.AddWithValue("@IsSupplier", 0)

                        command.Parameters.AddWithValue("@WebCreateDeviceID", "")
                        command.Parameters.AddWithValue("@WebCreateSource", "")
                        command.Parameters.AddWithValue("@WebFlowFrom", "")
                        command.Parameters.AddWithValue("@WebFlowTo", "")
                        command.Parameters.AddWithValue("@WebEditSource", "")
                        command.Parameters.AddWithValue("@WebDeleteStatus", "")
                        command.Parameters.AddWithValue("@WebLastEditDevice", "")

                        If ddlNationality.Text = txtDDLText.Text Then
                            command.Parameters.AddWithValue("@Nationality", "")
                        Else
                            command.Parameters.AddWithValue("@Nationality", ddlNationality.Text)
                        End If

                        command.Parameters.AddWithValue("@ContactPerson", txtOffContactPerson.Text.ToUpper)
                        command.Parameters.AddWithValue("@ResCP2Tel", txtOffCont1Tel.Text.ToUpper)
                        command.Parameters.AddWithValue("@ResCP2Fax", txtOffCont1Fax.Text.ToUpper)
                        command.Parameters.AddWithValue("@ResCP2Tel2", txtOffCont1Tel2.Text.ToUpper)
                        command.Parameters.AddWithValue("@ResCP2Mobile", txtOffCont1Mobile.Text.ToUpper)
                        command.Parameters.AddWithValue("@ResCP2Email", txtOffCont1Email.Text.ToUpper)
                        command.Parameters.AddWithValue("@ContactPerson2", txtOffCont1Name.Text.ToUpper)
                        command.Parameters.AddWithValue("@BillContact1Position", txtBillCP1Position.Text.ToUpper)
                        command.Parameters.AddWithValue("@BillContact2", txtBillCP2Contact.Text.ToUpper)
                        command.Parameters.AddWithValue("@BillContact2Position", txtBillCP2Position.Text)
                        command.Parameters.AddWithValue("@BillContact2Email", txtBillCP2Email.Text)
                        command.Parameters.AddWithValue("@BillContact2Tel", txtBillCP2Tel.Text.ToUpper)
                        command.Parameters.AddWithValue("@BillContact2Fax", txtBillCP2Fax.Text.ToUpper)
                        command.Parameters.AddWithValue("@BillContact2Tel2", txtBillCP2Tel2.Text.ToUpper)
                        command.Parameters.AddWithValue("@BillContact2Mobile", txtBillCP2Mobile.Text.ToUpper)
                        command.Parameters.AddWithValue("@BillingSettings", rdbBillingSettings.SelectedValue.ToString)
                        command.Parameters.AddWithValue("@BillingName", txtBillingName.Text.ToUpper)
                        command.Parameters.AddWithValue("@Terms", ddlTerms.Text.ToUpper)
                        command.Parameters.AddWithValue("@TermsDay", 0)
                        'command.Parameters.AddWithValue("@TermsDay", ddlTerms.SelectedValue.ToString)
                        command.Parameters.AddWithValue("@Currency", ddlCurrency.SelectedItem.Text.ToUpper)

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
                        command.Dispose()
                        lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED"
                        lblAlert.Text = ""
                        CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "PERS", txtAccountID.Text, "EDIT", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtAccountID.Text, "", txtRcno.Text)

                    End If
                End If

                conn.Close()
                conn.Dispose()
                command2.Dispose()

                'Catch ex As Exception

                '    MessageBox.Message.Alert(Page, "Error!!! " + ex.ToString, "str")
                'End Try
                EnableControls()

            End If
            'txt.Text = "SELECT * FROM tblperson WHERE  Inactive=0 order by createdon desc limit 100;"
            'SqlDataSource1.SelectCommand = "SELECT * FROM tblperson WHERE  Inactive=0 order by createdon desc limit 100;"

            SqlDataSource1.SelectCommand = txt.Text
            SqlDataSource1.DataBind()
            GridView1.DataBind()

            btnCopyAdd.Enabled = True
            btnCopyAdd.ForeColor = System.Drawing.Color.Gray

            '     GridView1.DataSourceID = "SqlDataSource1"
            '   MakeMeNull()
            txtMode.Text = ""
            txtSvcMode.Text = ""

            EnableSvcControls()

            txtSearchCust.Enabled = True
            btnGoCust.Enabled = True
            btnResetSearch.Enabled = True

            'InsertNewLog()
        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "btnSave_Click", ex.Message.ToString, txtAccountID.Text)
        End Try
    End Sub


    'Private Sub InsertNewLog()

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
    '                commandInsertLog.CommandText = "InsertLog_sitadatadb"

    '                commandInsertLog.Parameters.Clear()

    '                commandInsertLog.Parameters.AddWithValue("@pr_ModuleType", "Residential")
    '                commandInsertLog.Parameters.AddWithValue("@pr_KeyValue", txtAccountID.Text.Trim)

    '                commandInsertLog.Connection = connLog
    '                commandInsertLog.ExecuteScalar()

    '                connLog.Close()
    '                commandInsertLog.Dispose()

    '                ' End: Insert NEW Log table

    '                ''''

    '            End If
    '            'End If

    '        End If
    '        conn.Close()
    '        conn.Dispose()
    '        command.Dispose()
    '        dt.Dispose()
    '        dr.Close()
    '    Catch ex As Exception
    '        lblAlert.Text = ex.Message.ToString
    '        InsertIntoTblWebEventLog("PERSON - " + Session("UserID"), "FUNCTION InsertNewLog", ex.Message.ToString, txtAccountID.Text)
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

    '                commandInsertLog.Parameters.AddWithValue("@pr_ModuleType", "Residential")
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

            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                command1.CommandText = "SELECT * FROM tblperson where rcno=" & Convert.ToInt32(txtRcno.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    ' Dim qry As String = "delete from tblperson where rcno=" & Convert.ToInt32(txtRcno.Text)
                    Dim qry As String = "UPDATE tblperson SET Inactive=1,Status='T' where rcno=" & Convert.ToInt32(txtRcno.Text)

                    command.CommandText = qry

                    command.Connection = conn

                    command.ExecuteNonQuery()

                    DeleteContactMaster(conn)

                    lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "PERS", txtAccountID.Text, "DELETE", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtAccountID.Text, "", txtRcno.Text)

                End If
                conn.Close()
                conn.Dispose()
                command1.Dispose()
                dt.Dispose()
                dr.Close()


                EnableControls()


                GridView1.DataSourceID = "SqlDataSource1"
                MakeMeNull()
                lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"
                ddlStatus.SelectedIndex = 0
            Catch ex As Exception
                InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "btnDelete_Click", ex.Message.ToString, txtAccountID.Text)
            End Try
        End If

    End Sub

    Protected Sub chkSameAddr_CheckedChanged(sender As Object, e As EventArgs) Handles chkSameAddr.CheckedChanged
        If String.IsNullOrEmpty(txtPostal.Text.Trim) = False Then
            txtPostal_TextChanged(sender, e)
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
        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "btnCancel_Click", ex.Message.ToString, txtAccountID.Text)
        End Try
    End Sub

    Protected Sub btnCopyAdd_Click(sender As Object, e As EventArgs) Handles btnCopyAdd.Click
        Try
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

            txtSearchCust.Enabled = False
            btnGoCust.Enabled = False
            btnResetSearch.Enabled = False
        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "btnCopyAdd_Click", ex.Message.ToString, txtAccountID.Text)
        End Try
    End Sub


    Private Sub InsertContactMaster(conn As MySqlConnection)
        Try

            Dim command As MySqlCommand = New MySqlCommand

            command.CommandType = CommandType.Text
            Dim qry As String = "INSERT INTO tblcontactmaster(ContType,ContID,ContName,ContRegID,ContRegDate,ContGSTYN,ContGSTNO,ContPerson,ContLocationGroup,ContSalesGroup,ContGroup,ContSales,ContInCharge,ContTel,ContFax,ContHP,ContEmail,ContAddBlock,ContAddNos,ContAddFloor,ContAddUnit,ContAddress1,ContAddStreet,ContAddBuilding,ContAddPostal,ContAddState,ContAddCity,ContAddCountry,ContBillTel,ContBILLFax,ContBILLHP,ContBILLEmail,ContBILLBlock,ContBILLNos,ContBILLFloor,ContBILLUnit,ContBILLAddress1,ContBILLStreet,ContBILLBuilding,ContBILLPostal,ContBILLState,ContBILLCity,ContBILLCountry,CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn,Dealer,Name2,WebLoginID,WebLoginPassWord,WebAccessLevel,WebOneTimePassWord,WebLevel,DiscountPct,LoginID,Password,PriceGroup,SubCompanyNo,SalesGRP,LocateGRP,Telephone2,BillTelephone2,Mobile,BillMobile,SoPriceGroup,AccountNo,InActive,ContARTerm,ContAPTerm,ContRentalTerm,ContShippingTerm,ContApCurrency,ContArCurrency,Industry,InterCompany,CreateDeviceID,CreateSource,FlowFrom,FlowTo,EditSource,DeleteStatus,LastEditDevice,AutoEmailServ,ReportFormatServ,Email,WebUploadDate,IsCustomer,IsSupplier,AccountID,OffContact1Position,OffContact1Tel,OffContact1Fax,OffContact1Tel2,OffContact1Mobile,BillContact1Position,BillContact1Email,BillContact2,BillContact2Position,BillContact2Tel,BillContact2Fax,BillContact2Tel2,BillContact2Mobile,BillContact2Email,OffContact1,OffContactPosition) VALUES(@ContType,@ContID,@ContName,@ContRegID,@ContRegDate,@ContGSTYN,@ContGSTNO,@ContPerson,@ContLocationGroup,@ContSalesGroup,@ContGroup,@ContSales,@ContInCharge,@ContTel,@ContFax,@ContHP,@ContEmail,@ContAddBlock,@ContAddNos,@ContAddFloor,@ContAddUnit,@ContAddress1,@ContAddStreet,@ContAddBuilding,@ContAddPostal,@ContAddState,@ContAddCity,@ContAddCountry,@ContBillTel,@ContBILLFax,@ContBILLHP,@ContBILLEmail,@ContBILLBlock,@ContBILLNos,@ContBILLFloor,@ContBILLUnit,@ContBILLAddress1,@ContBILLStreet,@ContBILLBuilding,@ContBILLPostal,@ContBILLState,@ContBILLCity,@ContBILLCountry,@CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn,@Dealer,@Name2,@WebLoginID,@WebLoginPassWord,@WebAccessLevel,@WebOneTimePassWord,@WebLevel,@DiscountPct,@LoginID,@Password,@PriceGroup,@SubCompanyNo,@SalesGRP,@LocateGRP,@Telephone2,@BillTelephone2,@Mobile,@BillMobile,@SoPriceGroup,@AccountNo,@InActive,@ContARTerm,@ContAPTerm,@ContRentalTerm,@ContShippingTerm,@ContApCurrency,@ContArCurrency,@Industry,@InterCompany,@CreateDeviceID,@CreateSource,@FlowFrom,@FlowTo,@EditSource,@DeleteStatus,@LastEditDevice,@AutoEmailServ,@ReportFormatServ,@Email,@WebUploadDate,@IsCustomer,@IsSupplier,@AccountID,@OffContact1Position,@OffContact1Tel,@OffContact1Fax,@OffContact1Tel2,@OffContact1Mobile,@BillContact1Position,@BillContact1Email,@BillContact2,@BillContact2Position,@BillContact2Tel,@BillContact2Fax,@BillContact2Tel2,@BillContact2Mobile,@BillContact2Email,@OffContact1,@OffContactPosition);"
            command.CommandText = qry
            command.Parameters.Clear()

            command.Parameters.AddWithValue("@ContType", "PERSON")
            command.Parameters.AddWithValue("@ContID", "")
            command.Parameters.AddWithValue("@ContName", txtNameE.Text)
            command.Parameters.AddWithValue("@ContRegID", "")

            command.Parameters.AddWithValue("@contRegDate", DBNull.Value)

            command.Parameters.AddWithValue("@ContGSTYN", 0)
            command.Parameters.AddWithValue("@ContGSTNO", "")

            command.Parameters.AddWithValue("@ContPerson", txtOffContactPerson.Text)

            command.Parameters.AddWithValue("@ContLocationGroup", "")

            command.Parameters.AddWithValue("@ContSalesGroup", "")

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
            command.Parameters.AddWithValue("@ContApCurrency", 0)
            command.Parameters.AddWithValue("@ContArCurrency", 0)

            command.Parameters.AddWithValue("@Industry", "")

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
            If chkCustomer.Checked = True Then
                command.Parameters.AddWithValue("@IsCustomer", 1)

            Else
                command.Parameters.AddWithValue("@IsCustomer", 0)

            End If
            If chkSupplier.Checked = True Then
                command.Parameters.AddWithValue("@IsSupplier", 1)

            Else
                command.Parameters.AddWithValue("@IsSupplier", 0)

            End If
            command.Parameters.AddWithValue("@AccountID", txtAccountID.Text)
            command.Parameters.AddWithValue("@OffContact1Position", "")
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
            command.Parameters.AddWithValue("@OffContactPosition", "")
            '  command.Parameters.AddWithValue("@OffContactMobile", txtOffMobile.Text)
            command.Parameters.AddWithValue("@BillContactPerson", txtBillCP1Contact.Text)


            command.Connection = conn

            command.ExecuteNonQuery()


        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "InsertContactMaster", ex.Message.ToString, txtAccountID.Text)
        End Try

    End Sub

    Private Sub UpdateContactMaster(conn As MySqlConnection)
        Try

            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            If String.IsNullOrEmpty(txtAccountID.Text) Then
                command1.CommandText = "SELECT * FROM tblcontactmaster where ContRegID=@id and conttype='PERSON'"
                command1.Parameters.AddWithValue("@id", txtClientID.Text)
            Else
                command1.CommandText = "SELECT * FROM tblcontactmaster where ACCOUNTID=@id and conttype='PERSON'"
                command1.Parameters.AddWithValue("@id", txtAccountID.Text)
            End If

            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then

                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                command.Parameters.Clear()
                Dim qry As String
                If String.IsNullOrEmpty(txtAccountID.Text) Then
                    qry = "UPDATE tblcontactmaster SET ContName = @ContName,ContRegID = @ContRegID,ContRegDate = @ContRegDate,ContGSTYN = @ContGSTYN,ContGSTNO = @ContGSTNO,ContPerson = @ContPerson,ContLocationGroup = @ContLocationGroup,ContSalesGroup = @ContSalesGroup,ContGroup = @ContGroup,ContSales = @ContSales,ContInCharge = @ContInCharge,ContTel = @ContTel,ContFax = @ContFax,ContHP = @ContHP,ContEmail = @ContEmail,ContAddBlock = @ContAddBlock,ContAddNos = @ContAddNos,ContAddFloor = @ContAddFloor,ContAddUnit = @ContAddUnit,ContAddress1 = @ContAddress1,ContAddStreet = @ContAddStreet,ContAddBuilding = @ContAddBuilding,ContAddPostal = @ContAddPostal,ContAddState = @ContAddState,ContAddCity = @ContAddCity,ContAddCountry = @ContAddCountry,ContBillTel = @ContBillTel,ContBILLFax = @ContBILLFax,ContBILLHP = @ContBILLHP,ContBILLEmail = @ContBILLEmail,ContBILLBlock = @ContBILLBlock,ContBILLNos = @ContBILLNos,ContBILLFloor = @ContBILLFloor,ContBILLUnit = @ContBILLUnit,ContBILLAddress1 = @ContBILLAddress1,ContBILLStreet = @ContBILLStreet,ContBILLBuilding = @ContBILLBuilding,ContBILLPostal = @ContBILLPostal,ContBILLState = @ContBILLState,ContBILLCity = @ContBILLCity,ContBILLCountry = @ContBILLCountry,LastModifiedBy = @LastModifiedBy,LastModifiedOn = @LastModifiedOn,Dealer = @Dealer,Name2 = @Name2,WebLoginID = @WebLoginID,WebLoginPassWord = @WebLoginPassWord,WebAccessLevel = @WebAccessLevel,WebOneTimePassWord = @WebOneTimePassWord,WebLevel = @WebLevel,DiscountPct = @DiscountPct,LoginID = @LoginID,Password = @Password,PriceGroup = @PriceGroup,SubCompanyNo = @SubCompanyNo,SalesGRP = @SalesGRP,LocateGRP = @LocateGRP,Telephone2 = @Telephone2,BillTelephone2 = @BillTelephone2,Mobile = @Mobile,BillMobile = @BillMobile,SoPriceGroup = @SoPriceGroup,AccountNo = @AccountNo,InActive = @InActive,ContARTerm = @ContARTerm,ContAPTerm = @ContAPTerm,ContRentalTerm = @ContRentalTerm,ContShippingTerm = @ContShippingTerm,ContApCurrency = @ContApCurrency,ContArCurrency = @ContArCurrency,Industry = @Industry,InterCompany = @InterCompany,CreateDeviceID = @CreateDeviceID,CreateSource = @CreateSource,FlowFrom = @FlowFrom,FlowTo = @FlowTo,EditSource = @EditSource,DeleteStatus = @DeleteStatus,LastEditDevice = @LastEditDevice,AutoEmailServ = @AutoEmailServ,ReportFormatServ = @ReportFormatServ,Email = @Email,WebUploadDate = @WebUploadDate,IsCustomer = @IsCustomer,IsSupplier = @IsSupplier,AccountID = @AccountID,OffContact1Position = @OffContact1Position,OffContact1Tel = @OffContact1Tel,OffContact1Fax = @OffContact1Fax,OffContact1Tel2 = @OffContact1Tel2,OffContact1Mobile = @OffContact1Mobile,BillContact1Position = @BillContact1Position,BillContact1Email = @BillContact1Email,BillContact2 = @BillContact2,BillContact2Position = @BillContact2Position,BillContact2Tel = @BillContact2Tel,BillContact2Fax = @BillContact2Fax,BillContact2Tel2 = @BillContact2Tel2,BillContact2Mobile = @BillContact2Mobile,BillContact2Email = @BillContact2Email,OffContact1=@OffContact1,OffContact1=@OffContact1 WHERE contid =@id and conttype='PERSON';"
                    command.Parameters.AddWithValue("@id", txtClientID.Text)
                Else
                    qry = "UPDATE tblcontactmaster SET ContName = @ContName,ContRegID = @ContRegID,ContRegDate = @ContRegDate,ContGSTYN = @ContGSTYN,ContGSTNO = @ContGSTNO,ContPerson = @ContPerson,ContLocationGroup = @ContLocationGroup,ContSalesGroup = @ContSalesGroup,ContGroup = @ContGroup,ContSales = @ContSales,ContInCharge = @ContInCharge,ContTel = @ContTel,ContFax = @ContFax,ContHP = @ContHP,ContEmail = @ContEmail,ContAddBlock = @ContAddBlock,ContAddNos = @ContAddNos,ContAddFloor = @ContAddFloor,ContAddUnit = @ContAddUnit,ContAddress1 = @ContAddress1,ContAddStreet = @ContAddStreet,ContAddBuilding = @ContAddBuilding,ContAddPostal = @ContAddPostal,ContAddState = @ContAddState,ContAddCity = @ContAddCity,ContAddCountry = @ContAddCountry,ContBillTel = @ContBillTel,ContBILLFax = @ContBILLFax,ContBILLHP = @ContBILLHP,ContBILLEmail = @ContBILLEmail,ContBILLBlock = @ContBILLBlock,ContBILLNos = @ContBILLNos,ContBILLFloor = @ContBILLFloor,ContBILLUnit = @ContBILLUnit,ContBILLAddress1 = @ContBILLAddress1,ContBILLStreet = @ContBILLStreet,ContBILLBuilding = @ContBILLBuilding,ContBILLPostal = @ContBILLPostal,ContBILLState = @ContBILLState,ContBILLCity = @ContBILLCity,ContBILLCountry = @ContBILLCountry,LastModifiedBy = @LastModifiedBy,LastModifiedOn = @LastModifiedOn,Dealer = @Dealer,Name2 = @Name2,WebLoginID = @WebLoginID,WebLoginPassWord = @WebLoginPassWord,WebAccessLevel = @WebAccessLevel,WebOneTimePassWord = @WebOneTimePassWord,WebLevel = @WebLevel,DiscountPct = @DiscountPct,LoginID = @LoginID,Password = @Password,PriceGroup = @PriceGroup,SubCompanyNo = @SubCompanyNo,SalesGRP = @SalesGRP,LocateGRP = @LocateGRP,Telephone2 = @Telephone2,BillTelephone2 = @BillTelephone2,Mobile = @Mobile,BillMobile = @BillMobile,SoPriceGroup = @SoPriceGroup,AccountNo = @AccountNo,InActive = @InActive,ContARTerm = @ContARTerm,ContAPTerm = @ContAPTerm,ContRentalTerm = @ContRentalTerm,ContShippingTerm = @ContShippingTerm,ContApCurrency = @ContApCurrency,ContArCurrency = @ContArCurrency,Industry = @Industry,InterCompany = @InterCompany,CreateDeviceID = @CreateDeviceID,CreateSource = @CreateSource,FlowFrom = @FlowFrom,FlowTo = @FlowTo,EditSource = @EditSource,DeleteStatus = @DeleteStatus,LastEditDevice = @LastEditDevice,AutoEmailServ = @AutoEmailServ,ReportFormatServ = @ReportFormatServ,Email = @Email,WebUploadDate = @WebUploadDate,IsCustomer = @IsCustomer,IsSupplier = @IsSupplier,AccountID = @AccountID,OffContact1Position = @OffContact1Position,OffContact1Tel = @OffContact1Tel,OffContact1Fax = @OffContact1Fax,OffContact1Tel2 = @OffContact1Tel2,OffContact1Mobile = @OffContact1Mobile,BillContact1Position = @BillContact1Position,BillContact1Email = @BillContact1Email,BillContact2 = @BillContact2,BillContact2Position = @BillContact2Position,BillContact2Tel = @BillContact2Tel,BillContact2Fax = @BillContact2Fax,BillContact2Tel2 = @BillContact2Tel2,BillContact2Mobile = @BillContact2Mobile,BillContact2Email = @BillContact2Email,OffContact1=@OffContact1,OffContact1=@OffContact1 WHERE accountid =@id and conttype='PERSON';"
                    command.Parameters.AddWithValue("@id", txtAccountID.Text)
                End If

                command.CommandText = qry

                command.Parameters.AddWithValue("@ContID", "")
                command.Parameters.AddWithValue("@ContName", txtNameE.Text)
                command.Parameters.AddWithValue("@ContRegID", "")

                command.Parameters.AddWithValue("@contRegDate", DBNull.Value)

                command.Parameters.AddWithValue("@ContGSTYN", 0)
                command.Parameters.AddWithValue("@ContGSTNO", "")

                command.Parameters.AddWithValue("@ContPerson", txtOffContactPerson.Text)

                command.Parameters.AddWithValue("@ContLocationGroup", "")

                command.Parameters.AddWithValue("@ContSalesGroup", "")

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
                command.Parameters.AddWithValue("@ContApCurrency", 0)
                command.Parameters.AddWithValue("@ContArCurrency", 0)

                command.Parameters.AddWithValue("@Industry", "")

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
                If chkCustomer.Checked = True Then
                    command.Parameters.AddWithValue("@IsCustomer", 1)

                Else
                    command.Parameters.AddWithValue("@IsCustomer", 0)

                End If
                If chkSupplier.Checked = True Then
                    command.Parameters.AddWithValue("@IsSupplier", 1)

                Else
                    command.Parameters.AddWithValue("@IsSupplier", 0)

                End If
                command.Parameters.AddWithValue("@AccountID", txtAccountID.Text)
                command.Parameters.AddWithValue("@OffContact1Position", "")
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
                command.Parameters.AddWithValue("@OffContactPosition", "")
                '  command.Parameters.AddWithValue("@OffContactMobile", txtOffMobile.Text)
                command.Parameters.AddWithValue("@BillContactPerson", txtBillCP1Contact.Text)


                command.Connection = conn

                command.ExecuteNonQuery()

            End If

        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "UpdateContactMaster", ex.Message.ToString, txtAccountID.Text)
        End Try
    End Sub

    Private Sub DeleteContactMaster(conn As MySqlConnection)
        Try

            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            If String.IsNullOrEmpty(txtAccountID.Text) Then
                command1.CommandText = "SELECT * FROM tblcontactmaster where ContRegID=@id and conttype='PERSON'"
                command1.Parameters.AddWithValue("@id", txtClientID.Text)
            Else
                command1.CommandText = "SELECT * FROM tblcontactmaster where ACCOUNTID=@id and conttype='PERSON'"
                command1.Parameters.AddWithValue("@id", txtAccountID.Text)
            End If
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then

                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                Dim qry As String
                If String.IsNullOrEmpty(txtAccountID.Text) Then
                    qry = "update tblcontactmaster set Inactive=1 where contid=@id and conttype='PERSON'"
                    command.Parameters.AddWithValue("@id", txtClientID.Text)
                Else
                    qry = "update tblcontactmaster set Inactive=1 where accountid=@id and conttype='PERSON'"
                    command.Parameters.AddWithValue("@id", txtAccountID.Text)
                End If

                command.CommandText = qry

                command.Connection = conn

                command.ExecuteNonQuery()


            End If


        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "DeleteContactMaster", ex.Message.ToString, txtAccountID.Text)
        End Try
    End Sub


    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            Dim qry As String

            'If txtDisplayRecordsLocationwise.Text = "N" Then
            qry = "SELECT  tblperson.Rcno, tblperson.AccountId, tblperson.InActive, tblperson.ID, tblperson.Name, tblperson.ARCurrency, tblperson.Location, personbal.Bal, tblperson.TelMobile, tblperson.TelFax, tblperson.Address1, tblperson.AddPOstal, tblperson.BillAddress1, tblperson.BillPostal, tblperson.NRIC, tblperson.ICType, tblperson.Nationality, tblperson.Sex, tblperson.LocateGrp, tblperson.PersonGroup, tblperson.AccountNo, tblperson.Salesman, tblperson.AddStreet, tblperson.AddBuilding, tblperson.AddCity, tblperson.AddState, tblperson.AddCountry, tblperson.BillStreet, tblperson.BillBuilding, tblperson.BillCity, tblperson.BillState, tblperson.BillCountry,  tblperson.CreatedBy, tblperson.CreatedOn, tblperson.LastModifiedBy, tblperson.LastModifiedOn, tblperson.AutoEmailInvoice, tblperson.AutoEmailSOA, tblperson.UnSubscribeAutoEmailDate  "
            qry = qry + " FROM tblperson  left join personbal  on tblperson.Accountid = personbal.Accountid left join tblpersonLocation  on tblperson.Accountid = tblpersonLocation.Accountid where 1=1 "

            If txtDisplayRecordsLocationwise.Text = "Y" Then
                If ddlBranchSearch.SelectedIndex = 0 Then
                    ddlBranch.SelectedIndex = 0
                Else
                    ddlBranch.Text = ddlBranchSearch.Text
                    qry = qry + " and tblperson.location = '" + ddlBranchSearch.Text + "'"

                End If
            End If

            If String.IsNullOrEmpty(txtSearchID.Text) = False Then
                txtAccountID.Text = txtSearchID.Text
                qry = qry + " and tblperson.accountid like '%" + txtSearchID.Text + "%'"

            End If
            If String.IsNullOrEmpty(ddlSearchStatus.Text) = False Then
                If ddlSearchStatus.Text <> txtDDLText.Text Then
                    ddlStatus.Text = ddlSearchStatus.Text
                    qry = qry + " and tblperson.status = '" + ddlSearchStatus.Text + "'"
                End If
            End If
            If String.IsNullOrEmpty(txtSearchCompany.Text) = False Then
                txtNameE.Text = txtSearchCompany.Text
                qry = qry + " and tblperson.name like '%" + txtSearchCompany.Text + "%'"
            End If
            If String.IsNullOrEmpty(txtSearchAddress.Text) = False Then

                qry = qry + " and (tblperson.address1 like '%" + txtSearchAddress.Text + "%'"
                qry = qry + " or tblperson.addbuilding like '%" + txtSearchAddress.Text + "%'"
                qry = qry + " or tblperson.addstreet like '%" + txtSearchAddress.Text + "%')"
            End If
            If String.IsNullOrEmpty(txtSearchBillingAddress.Text) = False Then

                qry = qry + " and (tblperson.billaddress1 like ""%" + txtSearchBillingAddress.Text + "%"""
                qry = qry + " or tblperson.billbuilding like '%" + txtSearchBillingAddress.Text + "%'"
                qry = qry + " or tblperson.billstreet like '%" + txtSearchBillingAddress.Text + "%')"
            End If
            If String.IsNullOrEmpty(txtSearchPostal.Text) = False Then
                txtPostal.Text = txtSearchPostal.Text
                qry = qry + " and (tblperson.addpostal like '" + txtSearchPostal.Text + "%'"
                qry = qry + " or tblperson.billpostal like '" + txtSearchPostal.Text + "%')"
            End If
            If String.IsNullOrEmpty(txtSearchContact.Text) = False Then
                qry = qry + " and (tblperson.contactperson  like '%" + txtSearchContact.Text + "%'"
                qry = qry + " or tblperson.contactperson2  like '%" + txtSearchContact.Text + "%'"
                qry = qry + " or tblperson.BillcontactPerson  like '%" + txtSearchContact.Text + "%'"
                qry = qry + " or tblperson.BillingName  like '%" + txtSearchContact.Text + "%'"

                qry = qry + " or tblperson.Billcontact2  like '%" + txtSearchContact.Text + "%')"


            End If
            If String.IsNullOrEmpty(ddlSearchSalesman.Text) = False Then
                If ddlSearchSalesman.Text <> txtDDLText.Text Then
                    qry = qry + " and tblperson.salesman  = '" + ddlSearchSalesman.Text + "'"
                End If
            End If

            If String.IsNullOrEmpty(txtSearchContactNo.Text) = False Then

                qry = qry + " and (tblperson.telmobile  like '%" + txtSearchContactNo.Text + "%' or tblperson.telhome like '%" + txtSearchContactNo.Text + "%'"
                qry = qry + " or tblperson.telpager  like '%" + txtSearchContactNo.Text + "%'"
                qry = qry + " or tblperson.rescp2Tel  like '%" + txtSearchContactNo.Text + "%'"
                qry = qry + " or tblperson.rescp2Tel2  like '%" + txtSearchContactNo.Text + "%'"
                qry = qry + " or tblperson.rescp2Mobile  like '%" + txtSearchContactNo.Text + "%'"
                qry = qry + " or tblperson.BillTelhome  like '%" + txtSearchContactNo.Text + "%'"
                qry = qry + " or tblperson.BillTelpager  like '%" + txtSearchContactNo.Text + "%'"
                qry = qry + " or tblperson.BilltelMobile  like '%" + txtSearchContactNo.Text + "%'"
                qry = qry + " or tblperson.Billcontact2Tel  like '%" + txtSearchContactNo.Text + "%'"
                qry = qry + " or tblperson.Billcontact2Tel2  like '%" + txtSearchContactNo.Text + "%'"
                qry = qry + " or tblperson.Billcontact2Mobile  like '%" + txtSearchContactNo.Text + "%')"

            End If
            If chkSearchInactive.Checked = True Then
                qry = qry + " and tblperson.Inactive=1"
            End If
            If String.IsNullOrEmpty(txtSearchServiceAddress.Text) = False Then
                qry = qry + " and tblperson.accountid in (select accountid from tblpersonlocation where rcno <> 0"
                qry = qry + " and (tblpersonlocation.address1 like ""%" + txtSearchServiceAddress.Text + "%"""
                qry = qry + " or tblpersonlocation.addbuilding like '%" + txtSearchServiceAddress.Text + "%'"
                qry = qry + " or tblpersonlocation.addpostal like '" + txtSearchServiceAddress.Text + "%'"
                qry = qry + " or tblpersonlocation.addstreet like '%" + txtSearchServiceAddress.Text + "%'))"
            End If
            'End If




            'If txtDisplayRecordsLocationwise.Text = "Y" Then
            '    'qry = "SELECT * FROM tblperson   left join Personbal on tblperson.Accountid = Personbal.Accountid  where 1=1 "

            '    qry = " SELECT distinct tblperson.Rcno, tblperson.AccountId, tblperson.InActive, tblperson.ID, tblperson.Name, tblperson.ARCurrency, tblperson.Location, PersonBal.Bal, tblperson.TelMobile, tblperson.TelFax, tblperson.Address1, tblperson.AddPOstal, tblperson.BillAddress1, tblperson.BillPostal, tblperson.NRIC, tblperson.ICType, tblperson.Nationality, tblperson.Sex, tblperson.LocateGrp, tblperson.PersonGroup, tblperson.AccountNo, tblperson.Salesman, tblperson.AddStreet, tblperson.AddBuilding, tblperson.AddCity, tblperson.AddState, tblperson.AddCountry, tblperson.BillStreet, tblperson.BillBuilding, tblperson.BillCity, tblperson.BillState, tblperson.BillCountry,  tblperson.CreatedBy, tblperson.CreatedOn, tblperson.LastModifiedBy, tblperson.LastModifiedOn FROM tblperson  left join personbal  on tblperson.Accountid = Personbal.Accountid left join tblPersonLocation  on tblperson.Accountid = tblPersonLocation.Accountid where tblPerson.Inactive=0 "
            '    qry = qry + " and tblPersonLocation.location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "')"


            '    If String.IsNullOrEmpty(txtSearchID.Text) = False Then
            '        txtAccountID.Text = txtSearchID.Text
            '        qry = qry + " and tblperson.accountid like '%" + txtSearchID.Text + "%'"

            '    End If
            '    If String.IsNullOrEmpty(ddlSearchStatus.Text) = False Then
            '        If ddlSearchStatus.Text <> txtDDLText.Text Then
            '            ddlStatus.Text = ddlSearchStatus.Text
            '            qry = qry + " and tblperson.status = '" + ddlSearchStatus.Text + "'"
            '        End If
            '    End If
            '    If String.IsNullOrEmpty(txtSearchCompany.Text) = False Then
            '        txtNameE.Text = txtSearchCompany.Text
            '        qry = qry + " and tblperson.name like '%" + txtSearchCompany.Text + "%'"
            '    End If
            '    If String.IsNullOrEmpty(txtSearchAddress.Text) = False Then

            '        qry = qry + " and (tblperson.address1 like '%" + txtSearchAddress.Text + "%'"
            '        qry = qry + " or tblperson.addbuilding like '%" + txtSearchAddress.Text + "%'"
            '        qry = qry + " or tblperson.addstreet like '%" + txtSearchAddress.Text + "%')"
            '    End If
            '    If String.IsNullOrEmpty(txtSearchBillingAddress.Text) = False Then

            '        qry = qry + " and (tblperson.billaddress1 like ""%" + txtSearchBillingAddress.Text + "%"""
            '        qry = qry + " or tblperson.billbuilding like '%" + txtSearchBillingAddress.Text + "%'"
            '        qry = qry + " or tblperson.billstreet like '%" + txtSearchBillingAddress.Text + "%')"
            '    End If
            '    If String.IsNullOrEmpty(txtSearchPostal.Text) = False Then
            '        txtPostal.Text = txtSearchPostal.Text
            '        qry = qry + " and (tblperson.addpostal like '" + txtSearchPostal.Text + "%'"
            '        qry = qry + " or tblperson.billpostal like '" + txtSearchPostal.Text + "%')"
            '    End If
            '    If String.IsNullOrEmpty(txtSearchContact.Text) = False Then
            '        qry = qry + " and (tblperson.contactperson  like '%" + txtSearchContact.Text + "%'"
            '        qry = qry + " or tblperson.contactperson2  like '%" + txtSearchContact.Text + "%'"
            '        qry = qry + " or tblperson.BillcontactPerson  like '%" + txtSearchContact.Text + "%'"
            '        qry = qry + " or tblperson.BillingName  like '%" + txtSearchContact.Text + "%'"

            '        qry = qry + " or tblperson.Billcontact2  like '%" + txtSearchContact.Text + "%')"


            '    End If
            '    If String.IsNullOrEmpty(ddlSearchSalesman.Text) = False Then
            '        If ddlSearchSalesman.Text <> txtDDLText.Text Then
            '            qry = qry + " and tblperson.salesman  = '" + ddlSearchSalesman.Text + "'"
            '        End If
            '    End If

            '    If String.IsNullOrEmpty(txtSearchContactNo.Text) = False Then

            '        qry = qry + " and (tblperson.telmobile  like '%" + txtSearchContactNo.Text + "%' or tblperson.telhome like '%" + txtSearchContactNo.Text + "%'"
            '        qry = qry + " or tblperson.telpager  like '%" + txtSearchContactNo.Text + "%'"
            '        qry = qry + " or tblperson.rescp2Tel  like '%" + txtSearchContactNo.Text + "%'"
            '        qry = qry + " or tblperson.rescp2Tel2  like '%" + txtSearchContactNo.Text + "%'"
            '        qry = qry + " or tblperson.rescp2Mobile  like '%" + txtSearchContactNo.Text + "%'"
            '        qry = qry + " or tblperson.BillTelhome  like '%" + txtSearchContactNo.Text + "%'"
            '        qry = qry + " or tblperson.BillTelpager  like '%" + txtSearchContactNo.Text + "%'"
            '        qry = qry + " or tblperson.BilltelMobile  like '%" + txtSearchContactNo.Text + "%'"
            '        qry = qry + " or tblperson.Billcontact2Tel  like '%" + txtSearchContactNo.Text + "%'"
            '        qry = qry + " or tblperson.Billcontact2Tel2  like '%" + txtSearchContactNo.Text + "%'"
            '        qry = qry + " or tblperson.Billcontact2Mobile  like '%" + txtSearchContactNo.Text + "%')"

            '    End If
            '    If chkSearchInactive.Checked = True Then
            '        qry = qry + " and tblperson.Inactive=1"
            '    End If
            '    If String.IsNullOrEmpty(txtSearchServiceAddress.Text) = False Then
            '        qry = qry + " and tblperson.accountid in (select accountid from tblpersonlocation where rcno <> 0"
            '        qry = qry + " and (tblpersonlocation.address1 like ""%" + txtSearchServiceAddress.Text + "%"""
            '        qry = qry + " or tblpersonlocation.addbuilding like '%" + txtSearchServiceAddress.Text + "%'"
            '        qry = qry + " or tblpersonlocation.addpostal like '" + txtSearchServiceAddress.Text + "%'"
            '        qry = qry + " or tblpersonlocation.addstreet like '%" + txtSearchServiceAddress.Text + "%'))"
            '    End If
            'End If


            'If txtDisplayRecordsLocationwise.Text = "Y" Then
            '    qry = qry + " and Location = '" & ddlLocation.Text.Trim & "'"
            'End If

            If txtDisplayRecordsLocationwise.Text = "Y" Then
                'qry = qry + " and Location = '" & ddlLocation.Text.Trim & "'"

                qry = qry + " and tblpersonlocation.location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "')"

            End If

            qry = qry + " order by tblperson.rcno desc,tblperson.name;"
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

            ddlSearchStatus.ClearSelection()
            txtSearchBillingAddress.Text = ""

            txtSearchCust.Text = txtSearchServiceAddress.Text
            txtSearch.Text = txtSearchServiceAddress.Text

            txtSearchServiceAddress.Text = ""
        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "btnSearch_Click", ex.Message.ToString, txtAccountID.Text)
        End Try
    End Sub

    Private Function Validation() As Boolean
        Dim d As Date


        If String.IsNullOrEmpty(txtStartDate.Text) = False Then
            If Date.TryParseExact(txtStartDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then
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
            'Session("sevaddress") = serviceaddress
            Session("contractfrom") = "clients"
            Session("contracttype") = "PERSON"
            Session("companygroup") = ddlCompanyGrp.Text
            Session("clientid") = txtID.Text
            Session("accountid") = txtAccountID.Text.Trim

            Session("custname") = txtNameE.Text

            'Session("contactperson") = txtBillContact.Text
            'Session("conpermobile") = txtMobile.Text
            'Session("acctcode") = txtAcctCode.Text
            'Session("telephone") = txtTelephone.Text
            'Session("fax") = txtFax.Text
            Session("postal") = txtPostal.Text
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


            If String.IsNullOrEmpty(Session("contractfrom")) = False Then
                Session("clientid") = txtID.Text
            End If
            Session("gridsqlPerson") = txt.Text
            Session("rcno") = txtRcno.Text

            Session("gridsqlPersonDetail") = txtDetail.Text
            Session("rcnoDetail") = txtSvcRcno.Text
            Session("inactive") = chkInactive.Checked

            'If ddlLocation.SelectedIndex = 0 Then
            Session("location") = ""
            'Else
            'Session("location") = ddlLocation.Text
            'End If
            'Session("location") = ddlLocation.Text
            Response.Redirect("contract.aspx", False)
        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "btnContract_Click", ex.Message.ToString, txtAccountID.Text)
        End Try
    End Sub

    Protected Sub txtPostal_TextChanged(sender As Object, e As EventArgs) Handles txtPostal.TextChanged
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
            Dim dt As New DataTable
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
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "txtPostal_TextChanged", ex.Message.ToString, txtAccountID.Text)
        End Try
    End Sub



    Protected Sub txtAcctCode_TextChanged(sender As Object, e As EventArgs) Handles txtAcctCode.TextChanged
        Try
            If String.IsNullOrEmpty(txtAcctCode.Text) = False Then
                txtAcctCode.Text = txtAcctCode.Text.ToUpper

                GenerateID()
            End If

        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "txtAcctCode_TextChanged", ex.Message.ToString, txtAccountID.Text)
        End Try
    End Sub

    Protected Sub GenerateID()

        Try
            'Dim conn As MySqlConnection = New MySqlConnection()

            'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            'conn.Open()
            'Dim command1 As MySqlCommand = New MySqlCommand

            'command1.CommandType = CommandType.Text

            'command1.CommandText = "SELECT * FROM tblcompany where ID LIKE '" & txtAcctCode.Text & "%' order by id desc"
            ''  command1.Parameters.AddWithValue("@code", txtAcctCode.Text)
            'command1.Connection = conn
            ''   MessageBox.Message.Alert(Page, command1.CommandText.ToString, "str")
            'Dim dr As MySqlDataReader = command1.ExecuteReader()
            'Dim dt As New DataTable
            'dt.Load(dr)
            'If dt.Rows.Count > 0 Then
            '    TextBox3.Text = dt.Rows(0)("ID").ToString
            '    ModalPopupExtender2.Show()

            'Else
            '    txtID.Text = txtAcctCode.Text + "-01"

            'End If


        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "GenerateID", ex.Message.ToString, txtAccountID.Text)
        End Try
    End Sub



    'Protected Sub btnok_Click(sender As Object, e As EventArgs) Handles btnok.Click

    '    If String.IsNullOrEmpty(TextBox3.Text) = False Then
    '        '  MessageBox.Message.Alert(Page, TextBox3.Text, "str")
    '        Dim index As Integer = TextBox3.Text.IndexOf("-")
    '        If index = 0 Then
    '            txtID.Text = txtAcctCode.Text + "-01"

    '        Else
    '            Dim code As Integer = Convert.ToInt64(TextBox3.Text.Substring(index + 1))
    '            Dim count As String = "D" + TextBox3.Text.Substring(index + 1).Length.ToString
    '            txtID.Text = txtAcctCode.Text + "-" + (code + 1).ToString(count)

    '        End If


    '    End If
    'ModalPopupExtender2.Hide()

    'End Sub

    'Protected Sub btnNo_Click(sender As Object, e As EventArgs) Handles btnNo.Click
    '    MessageBox.Message.Alert(Page, "Enter Account Code", "str")
    '    txtAcctCode.Text = ""
    '    txtAcctCode.Focus()

    '    ModalPopupExtender2.Hide()

    'End Sub



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
            Dim dt As New DataTable
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
            conn.Dispose()
            command1.Dispose()
            command.Dispose()
            dt.Dispose()
            dr.Close()
        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "GenerateAccountNo", ex.Message.ToString, txtAccountID.Text)
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
                command1.CommandText = "SELECT locationid,locationprefix,locationno FROM tblpersonlocation where personid='" & txtClientID.Text & "' order by locationno desc;"
                '  command1.Parameters.AddWithValue("@code", txtAcctCode.Text)
                command1.Connection = conn
                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
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
                    'txtLocationPrefix.Text = "L"
                    'txtLocatonNo.Text = "1"

                    txtLocationID.Text = txtClientID.Text + "-0001"
                    txtLocationPrefix.Text = ""
                    txtLocatonNo.Text = "1"
                End If
            Else
                command1.CommandText = "SELECT locationid,locationprefix,locationno FROM tblpersonlocation where accountid=" & txtAccountID.Text & " order by locationno desc;"
                '  command1.Parameters.AddWithValue("@code", txtAcctCode.Text)
                command1.Connection = conn
                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
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
                    'txtLocationPrefix.Text = "L"
                    'txtLocatonNo.Text = "1"

                    txtLocationID.Text = txtAccountID.Text + "-0001"
                    txtLocationPrefix.Text = ""
                    txtLocatonNo.Text = "1"
                End If
            End If

            conn.Close()
            conn.Dispose()
            command1.Dispose()



        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "GenerateLocationID", ex.Message.ToString, txtAccountID.Text)
        End Try
    End Sub

    Public Sub MakeSvcNull()
        Try
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
            txtCommentsSvc.Text = ""
            ddlSalesManSvc.SelectedIndex = 0
            ddlInchargeSvc.SelectedIndex = 0
            ddlTermsSvc.SelectedIndex = 0

            chkServiceReportSendTo1.Checked = False
            chkServiceReportSendTo2.Checked = False

            chkSvcPhotosMandatory.Checked = False

            ddlContractGrp.SelectedIndex = 0


            ddlPersonGrpD.SelectedIndex = 0
            chkInactiveD.Checked = False
            'ddlIndustrysvc.SelectedIndex = 0
            'txtMarketSegmentIDsvc.Text = ""
            txtSvcEmailCC.Text = ""
            ddlSvcDefaultInvoiceFormat.SelectedIndex = 0
            ddlBranch.SelectedIndex = 0
            btnSpecificLocation.Text = "SPECIFIC LOCATION"
            chkSendEmailNotificationOnly.Checked = False
            chkSmartCustomer.Checked = False
            txtSiteName.Text = ""

            'chkSmartCustomer.Checked = False

            'ddlSalesManSvc.Items.Clear()
            'ddlSalesManSvc.Items.Add("--SELECT--")
            'Dim Query As String
            'Query = "SELECT StaffId FROM tblstaff where roles= 'SALES MAN' and  Status ='O' ORDER BY STAFFID"
            ''PopulateDropDownList(Query, "StaffId", "StaffId", ddlSalesMan)
            'PopulateDropDownList(Query, "StaffId", "StaffId", ddlSalesManSvc)
            ''PopulateDropDownList(Query, "StaffId", "StaffId", ddlSearchSalesman)
        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "MakeSvcNull", ex.Message.ToString, txtAccountID.Text)
        End Try
    End Sub

    Private Sub EnableSvcControls()
        Try
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

            ddlContractGrp.Enabled = False
            chkMainBillingInfo.Enabled = False
            txtCommentsSvc.Enabled = False

            ddlPersonGrpD.Enabled = False
            chkInactiveD.Enabled = False

            ddlIndustrysvc.Enabled = False
            txtMarketSegmentIDsvc.Enabled = False

            txtSvcEmailCC.Enabled = False
            ddlSvcDefaultInvoiceFormat.Enabled = False
            ddlBranch.Enabled = False
            chkSendEmailNotificationOnly.Enabled = False

            chkSmartCustomer.Enabled = False
            txtSiteName.Enabled = False
        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "EnableSvcControls", ex.Message.ToString, txtAccountID.Text)
        End Try
    End Sub

    Private Sub DisableSvcControls()
        Try
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


            btnSpecificLocation.Enabled = False
            btnSpecificLocation.ForeColor = System.Drawing.Color.Gray

            btnTransfersSvc.Enabled = False
            btnTransfersSvc.ForeColor = System.Drawing.Color.Gray


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

            ddlContractGrp.Enabled = True
            chkMainBillingInfo.Enabled = True
            txtCommentsSvc.Enabled = True

            ddlPersonGrpD.Enabled = True
            chkInactiveD.Enabled = True

            ddlIndustrysvc.Enabled = True

            txtSvcEmailCC.Enabled = True
            ddlSvcDefaultInvoiceFormat.Enabled = True
            ddlBranch.Enabled = True
            chkSendEmailNotificationOnly.Enabled = True
            chkSmartCustomer.Enabled = True
            txtSiteName.Enabled = True

            btnSvcService.Text = "SERVICE"
            btnSvcContract.Text = "CONTRACT"

            'chkSmartCustomer.Enabled = True

            'txtMarketSegmentIDsvc.Enabled = True
        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "DisableSvcControls", ex.Message.ToString, txtAccountID.Text)
        End Try
    End Sub

    Protected Sub GridView2_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView2.PageIndexChanging
        GridView2.PageIndex = e.NewPageIndex

        SqlDataSource2.SelectCommand = txtDetail.Text
        SqlDataSource2.DataBind()
        GridView2.DataBind()

    End Sub

    Protected Sub GridView2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView2.SelectedIndexChanged
        Try
            If (Session("servicefrom")) = "contactP" Then
                MakeSvcNull()
                txtSvcRcno.Text = Session("rcnoDetail")

                'ddlCity.SelectedIndex = -1
                'ddlState.SelectedIndex = -1
                'ddlCountry.SelectedIndex = -1
                'ddlLocateGrp.SelectedIndex = -1
                'ddlBillCitySvc.SelectedIndex = -1
                'ddlBillStateSvc.SelectedIndex = -1
                'ddlBillCountrySvc.SelectedIndex = -1
                'ddlContractGrp.SelectedIndex = -1


                ddlContractGrp.SelectedIndex = -1
                ddlCity.SelectedIndex = -1
                ddlCountry.SelectedIndex = -1
                ddlState.SelectedIndex = -1
                ddlLocateGrp.SelectedIndex = -1
                'ddlIndustrysvc.SelectedIndex = -1

                ddlBillCitySvc.SelectedIndex = -1
                ddlBillCountrySvc.SelectedIndex = -1
                ddlBillStateSvc.SelectedIndex = -1

                ddlSalesManSvc.SelectedIndex = -1
                ddlInchargeSvc.SelectedIndex = -1
                ddlTermsSvc.SelectedIndex = -1
                ddlPersonGrpD.SelectedIndex = -1
                ddlIndustrysvc.SelectedIndex = -1
                ddlBranch.SelectedIndex = -1
                ddlContractGroupEdit.SelectedIndex = -1
                'ddlIndustrysvc.SelectedIndex = -1
                'ddlPersonGrpD.SelectedIndex = -1
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

                ddlCity.SelectedIndex = -1
                ddlState.SelectedIndex = -1
                ddlCountry.SelectedIndex = -1
                ddlLocateGrp.SelectedIndex = -1

                'ddlBillCitySvc.SelectedIndex = -1
                'ddlBillStateSvc.SelectedIndex = -1
                'ddlBillCountrySvc.SelectedIndex = -1

                ddlCity.SelectedIndex = -1
                ddlCountry.SelectedIndex = -1
                ddlState.SelectedIndex = -1
                ddlLocateGrp.SelectedIndex = -1

                ddlBillCitySvc.SelectedIndex = -1
                ddlBillCountrySvc.SelectedIndex = -1
                ddlBillStateSvc.SelectedIndex = -1

                ddlSalesManSvc.SelectedIndex = -1
                ddlInchargeSvc.SelectedIndex = -1
                ddlTermsSvc.SelectedIndex = -1
                ddlContractGrp.SelectedIndex = -1

                ddlIndustrysvc.SelectedIndex = -1
                ddlPersonGrpD.SelectedIndex = -1
                ddlContractGroupEdit.SelectedIndex = -1

                'ddlIncharge.SelectedIndex = -1
                'ddlTerms.SelectedIndex = -1
                'ddlCurrency.SelectedIndex = -1
                'ddlOffCity.SelectedIndex = -1
                'ddlOffCountry.SelectedIndex = -1
                'ddlOffState.SelectedIndex = -1
                'ddlBillCity.SelectedIndex = -1
                'ddlBillCountry.SelectedIndex = -1
                'ddlBillState.SelectedIndex = -1
                Session.Remove("contractfrom")
                'Dim editindex As Integer = GridView1.SelectedIndex
                'rcno = DirectCast(GridView1.Rows(editindex).FindControl("Label1"), Label).Text
                'txtRcno.Text = rcno.ToString()
            Else

                ''MakeMeNull()
                ''MakeSvcNull()
                'MakeSvcNull()
                'Dim editindex As Integer = GridView2.SelectedIndex
                'svcrcno = DirectCast(GridView2.Rows(editindex).FindControl("Label1"), Label).Text
                'txtSvcRcno.Text = svcrcno.ToString()

                ''''''''''''''''''''''''
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

                ''''''''''''''''''''''''''
            End If

            'MakeSvcNull()

            'Dim editindex As Integer = GridView2.SelectedIndex
            'svcrcno = DirectCast(GridView2.Rows(editindex).FindControl("Label1"), Label).Text
            'txtSvcRcno.Text = svcrcno.ToString()

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()
            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.CommandText = "SELECT * FROM tblpersonlocation where rcno=" & Convert.ToInt32(txtSvcRcno.Text)
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then
                txtAccountID.Text = dt.Rows(0)("AccountID").ToString
                txtAccountIDtab2.Text = txtAccountID.Text
                lblAccountID.Text = txtAccountID.Text
                txtLocationID.Text = dt.Rows(0)("locationID").ToString
                txtServiceName.Text = dt.Rows(0)("ServiceName").ToString
                txtDescription.Text = dt.Rows(0)("Description").ToString
                txtAddress.Text = dt.Rows(0)("Address1").ToString
                txtStreet.Text = dt.Rows(0)("AddStreet").ToString
                txtBuilding.Text = dt.Rows(0)("AddBuilding").ToString

                txtClientID.Text = dt.Rows(0)("PersonID").ToString

                If dt.Rows(0)("AddCity").ToString <> "" Then
                    ddlCity.Text = dt.Rows(0)("AddCity").ToString
                End If

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

                txtBillingNameSvc.Text = dt.Rows(0)("BillingNameSvc").ToString
                txtBillAddressSvc.Text = dt.Rows(0)("BillAddressSvc").ToString
                txtBillStreetSvc.Text = dt.Rows(0)("BillStreetSvc").ToString
                txtBillBuildingSvc.Text = dt.Rows(0)("BillBuildingSvc").ToString

                Dim gCity As String
                If dt.Rows(0)("BillCitySvc").ToString <> "" Then


                    gCity = dt.Rows(0)("BillCitySvc").ToString.ToUpper()

                    If ddlBillCitySvc.Items.FindByValue(gScheduler) Is Nothing Then
                        ddlBillCitySvc.Items.Add(gCity)
                        ddlBillCitySvc.Text = gCity
                    Else
                        ddlBillCitySvc.Text = dt.Rows(0)("BillCitySvc").ToString.Trim().ToUpper()
                    End If
                End If

                Dim gState As String
                If dt.Rows(0)("BillStateSvc").ToString <> "" Then
                    gState = dt.Rows(0)("BillStateSvc").ToString.ToUpper()

                    If ddlBillStateSvc.Items.FindByValue(gScheduler) Is Nothing Then
                        ''If String.IsNullOrEmpty(ddlScheduler.Items.FindByValue(gScheduler) = True Then
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
                        ''If String.IsNullOrEmpty(ddlScheduler.Items.FindByValue(gScheduler) = True Then
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

                If String.IsNullOrEmpty(dt.Rows(0)("ArTermSvc").ToString) = True Then
                    ddlTermsSvc.SelectedIndex = 0
                Else
                    ddlTermsSvc.Text = dt.Rows(0)("ArTermSvc").ToString
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
                Else
                    ddlContractGrp.SelectedIndex = 0
                    ddlContractGroupEdit.SelectedIndex = 0
                End If

                '  txtServiceLocationGroup.Text = dt.Rows(0)("AccountNo").ToString()
                txtCommentsSvc.Text = dt.Rows(0)("Comments").ToString()

                If String.IsNullOrEmpty(dt.Rows(0)("PersonGroupD").ToString.Trim) = False Then
                    ddlPersonGrpD.Text = dt.Rows(0)("PersonGroupD").ToString
                Else
                    ddlPersonGrpD.SelectedIndex = 0
                End If
                'ddlPersonGrpD.Text = dt.Rows(0)("PersonGrpD").ToString()
                chkInactiveD.Checked = dt.Rows(0)("InactiveD").ToString()

                If String.IsNullOrEmpty(dt.Rows(0)("Industry").ToString.Trim) = False Then
                    ddlIndustrysvc.Text = dt.Rows(0)("Industry").ToString
                Else
                    ddlIndustrysvc.SelectedIndex = 0
                End If
                'ddlIndustrysvc.Text = dt.Rows(0)("PersonGrpD").ToString()
                txtMarketSegmentIDsvc.Text = dt.Rows(0)("MarketSegmentID").ToString()


                txtSvcEmailCC.Text = dt.Rows(0)("ServiceEmailCC").ToString()

                chkSendEmailNotificationOnly.Checked = dt.Rows(0)("EmailServiceNotificationOnly").ToString()

                chkSmartCustomer.Checked = dt.Rows(0)("SmartCustomer").ToString()
                txtSiteName.Text = dt.Rows(0)("SiteName").ToString()

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

                btnSvcContract.Enabled = True
                btnSvcContract.ForeColor = System.Drawing.Color.Black
                btnSvcService.Enabled = True
                btnSvcService.ForeColor = System.Drawing.Color.Black


                btnSpecificLocation.Enabled = True
                btnSpecificLocation.ForeColor = System.Drawing.Color.Black

                btnTransfersSvc.Enabled = True
                btnTransfersSvc.ForeColor = System.Drawing.Color.Black

                ''''''''''''''''''''''''''''''''''''''''
                'Start:Retrive Service Records
                Dim commandService As MySqlCommand = New MySqlCommand

                commandService.CommandType = CommandType.Text
                commandService.CommandText = "SELECT count(SpecificlocationName) as totSpecificlocation FROM tblPersonlocationspecificlocation where LocationId ='" & txtLocationID.Text & "'"
                commandService.Connection = conn

                Dim drService As MySqlDataReader = commandService.ExecuteReader()
                Dim dtService As New DataTable
                dtService.Load(drService)

                If dtService.Rows.Count > 0 Then
                    btnSpecificLocation.Text = "SPECIFIC LOCATION [" + Val(dtService.Rows(0)("totSpecificlocation").ToString).ToString + "]"

                End If

                'If Session("SecGroupAuthority") = "ADMINISTRATOR" Then
                '    btnEditContractGroup.Visible = True
                'Else
                '    AccessControl()
                'End If

                'AccessControl()
                'End:Retrieve Service Records


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

                conn2.Close()
                dt2.Dispose()
                dr2.Close()
                '' Hide btnEditContractGroup if record exists in Contract for the Contract Goup and Location


                'Start:Retrive Service Records
                Dim commandServiceLocation As MySqlCommand = New MySqlCommand

                commandServiceLocation.CommandType = CommandType.Text
                commandServiceLocation.CommandText = "SELECT count(RecordNo) as totServiceLocationID FROM tblserviceRecord where LocationId ='" & txtLocationID.Text & "' AND STATUS IN ('O','P','H')"
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
            End If

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
            commandMinDateService.CommandText = "SELECT min(ServiceDate) as MinServiceDate from tblServiceRecord where LocationId= '" & txtLocationID.Text & "' and Status ='O'  and ServiceDate >= '" & DateTime.Now.ToString("yyyy-MM-dd") & "'"
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

            conn.Close()
            conn.Dispose()
            command1.Dispose()
            dt.Dispose()
            dr.Close()

            AddrConcat()

            '   txtSvcMode.Text = "EDIT"
            '    DisableSvcControls()
            EnableSvcControls()
            'ddlContractGrp.Enabled = False

            'btnSvcDelete.Enabled = True
            'btnSvcDelete.ForeColor = System.Drawing.Color.Black
            'btnSvcEdit.Enabled = True
            'btnSvcEdit.ForeColor = System.Drawing.Color.Black

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

            btnTransfersSvc.Enabled = True
            btnTransfersSvc.ForeColor = System.Drawing.Color.Black

            btnSpecificLocation.Enabled = True
            btnSpecificLocation.ForeColor = System.Drawing.Color.Black




            If chkInactive.Checked = True Then
                btnSvcAdd.Enabled = False
                btnSvcAdd.ForeColor = System.Drawing.Color.Gray
                btnSvcEdit.Enabled = False
                btnSvcEdit.ForeColor = System.Drawing.Color.Gray
                btnSvcCopy.Enabled = False
                btnSvcCopy.ForeColor = System.Drawing.Color.Gray


            End If

            AccessControl()

            If chkSmartCustomer.Checked = True Then
                btnTransfersSvc.Enabled = False
                btnTransfersSvc.ForeColor = System.Drawing.Color.Gray
            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "GridView2_SelectedIndexChanged", ex.Message.ToString, txtAccountID.Text)
        End Try
    End Sub

    Protected Sub btnSvcAdd_Click(sender As Object, e As EventArgs) Handles btnSvcAdd.Click
        Try
            DisableSvcControls()

            MakeSvcNull()

            txtClientID.Text = ""
            txtAccountCode.Text = ""

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


            txtSvcMode.Text = "NEW"
            txtServiceName.Text = txtNameE.Text
            'chkServiceReportSendTo1.Checked = True
            'chkServiceReportSendTo2.Checked = True

            chkServiceReportSendTo1.Checked = False
            chkServiceReportSendTo2.Checked = False
            chkSvcPhotosMandatory.Checked = False

            'ddlInchargeSvc.Text = ddlIncharge.Text
            ddlTermsSvc.Text = ddlTerms.Text
            'ddlSalesManSvc.Text = ddlSalesMan.Text
            txtBillingNameSvc.Text = txtBillingName.Text

            btnEditContractGroup.Visible = False

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
            'PopulateDropDownList(Query, "StaffId", "StaffId", ddlSalesMan)
            PopulateDropDownList(Query, "StaffId", "StaffId", ddlSalesManSvc)
            'PopulateDropDownList(Query, "StaffId", "StaffId", ddlSearchSalesman)

        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "btnSvcAdd_Click", ex.Message.ToString, txtAccountID.Text)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
        'Catch ex As Exception
        '    InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "btnSvcAdd_Click", ex.Message.ToString, txtAccountID.Text)
        'End Try
    End Sub

    Protected Sub btnSvcSave_Click(sender As Object, e As EventArgs) Handles btnSvcSave.Click
        lblAlert.Text = ""
        Try

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

            If ddlPersonGrpD.Text = "-1" Then
                ' MessageBox.Message.Alert(Page, "Company Group cannot be blank!!!", "str")
                lblAlert.Text = "PERSON GROUP CANNOT BE BLANK"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                Return

            End If

            If txtServiceName.Text = "" Then
                ' MessageBox.Message.Alert(Page, "Service Name cannot be blank!!!", "str")
                lblAlert.Text = "SERVICE NAME CANNOT BE BLANK"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                txtServiceName.Focus()
                Exit Sub

            End If

            If ddlIndustrysvc.SelectedIndex = 0 Then
                lblAlert.Text = "INDUSTRY CANNOT BE BLANK"
                'txtCreatedOn.Text = ""
                ddlIndustrysvc.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                Exit Sub
            End If

            If ddlContractGrp.SelectedIndex = 0 Then
                ' MessageBox.Message.Alert(Page, "Service Name cannot be blank!!!", "str")
                lblAlert.Text = "CONTRACT GROUP CANNOT BE BLANK"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                ddlContractGrp.Focus()
                Exit Sub

            End If

            If txtAddress.Text = "" Then
                lblAlert.Text = "STREET ADDRESS 1 (SERVICE ADDRESS) CANNOT BE BLANK"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                txtAddress.Focus()
                Exit Sub

            End If

            'If txtBuilding.Text = "" Then
            '    lblAlert.Text = "BUILDING/UNIT NO. (SERVICE ADDRESS) CANNOT BE BLANK"
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            '    txtBuilding.Focus()
            '    Exit Sub

            'End If

            'If ddlCity.SelectedIndex = 0 Then
            '    lblAlert.Text = "CITY (SERVICE ADDRESS ) CANNOT BE BLANK"
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            '    ddlCity.Focus()
            '    Exit Sub

            'End If

            If ddlCountry.SelectedIndex = 0 Then
                lblAlert.Text = "COUNTRY (SERVICE ADDRESS) CANNOT BE BLANK"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                ddlCountry.Focus()
                Exit Sub

            End If

            If ddlCity.SelectedIndex = 0 Then
                lblAlert.Text = "CITY CANNOT BE BLANK"
                'txtCreatedOn.Text = ""
                ddlCity.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                Exit Sub
            End If

            If ddlBillCountrySvc.SelectedIndex = 0 Then
                lblAlert.Text = "COUNTRY (BILL ADDRESS) CANNOT BE BLANK"
                ddlBillCountrySvc.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                Exit Sub
            End If

            'If txtPostal.Text = "" Then
            '    lblAlert.Text = "POSTAL (SERVICE ADDRESS) CANNOT BE BLANK"
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            '    txtPostal.Focus()
            '    Exit Sub

            'End If

            If txtSvcCP1Contact.Text = "" Then
                ' MessageBox.Message.Alert(Page, "Service Name cannot be blank!!!", "str")
                lblAlert.Text = "CONTACT PERSON-1 CANNOT BE BLANK"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                txtServiceName.Focus()
                Exit Sub

            End If
            If txtBillingNameSvc.Text = "" Then
                lblAlert.Text = "BILLING NAME CANNOT BE BLANK"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                txtBillingNameSvc.Focus()
                Return
            End If

            If txtBillContact1Svc.Text = "" Then
                lblAlert.Text = "BILLING CONTACT PERSON 1 CANNOT BE BLANK"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                txtBillContact1Svc.Focus()
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

                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                'command1.CommandText = "SELECT * FROM tblpersonlocation where accountid=@id"
                'command1.Parameters.AddWithValue("@id", txtAccountID.Text)

                If txtDisplayRecordsLocationwise.Text = "Y" Then
                    command1.CommandText = "SELECT * FROM tblpersonlocation where accountid=@id and Location=@Location"
                    command1.Parameters.AddWithValue("@id", txtAccountID.Text)
                    command1.Parameters.AddWithValue("@Location", ddlBranch.Text.Trim)
                Else
                    command1.CommandText = "SELECT * FROM tblpersonlocation where accountid=@id"
                    command1.Parameters.AddWithValue("@id", txtAccountID.Text)
                End If

                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                Dim addr As String = txtAddress.Text.Trim + " " + txtStreet.Text.Trim + " " + txtBuilding.Text.Trim
                Dim dataaddr As String
                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1

                        dataaddr = dt.Rows(i)("Address1").ToString + " " + dt.Rows(i)("AddStreet").ToString + " " + dt.Rows(i)("AddBuilding").ToString

                        'If addr = dataaddr And dt.Rows(i)("ContractGroup").ToString.Trim = ddlContractGrp.Text.Trim And dt.Rows(i)("PersonGroupD").ToString.Trim = ddlPersonGrpD.Text.Trim Then
                        '    '  MessageBox.Message.Alert(Page, "Address already exists!!!", "str")
                        '    lblAlert.Text = "ADDRESS ALREADY EXISTS FOR THIS CONTRACT GROUP AND COMPANY GROUP"
                        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                        '    Exit Sub
                        'End If

                        If txtDisplayRecordsLocationwise.Text = "Y" Then
                            If addr = dataaddr And dt.Rows(i)("ContractGroup").ToString.Trim = ddlContractGrp.Text.Trim And dt.Rows(i)("PersonGroupD").ToString.Trim = ddlPersonGrpD.Text.Trim And dt.Rows(i)("Location").ToString.Trim = ddlBranch.Text.Trim Then
                                '  MessageBox.Message.Alert(Page, "Address already exists!!!", "str")
                                'lblAlert.Text = "ADDRESS ALREADY EXISTS FOR THIS CONTRACT GROUP AND COMPANY GROUP"
                                lblAlert.Text = "THIS ADDRESS ALREADY EXISTS FOR THIS BRANCH, CONTRACT GROUP AND COMPANY GROUP"

                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                                Exit Sub
                            End If
                        Else
                            If addr = dataaddr And dt.Rows(i)("ContractGroup").ToString.Trim = ddlContractGrp.Text.Trim And dt.Rows(i)("PersonGroupD").ToString.Trim = ddlPersonGrpD.Text.Trim Then
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
                        '  InsertIntoTblWebEventLog("COMPLOC2", "SVCSAVE", CustAddr, dataaddr1)
                        'If CustAddr = dataaddr1 And dt.Rows(i)("ContractGroup").ToString.Trim = ddlContractGrp.Text.Trim And dt.Rows(i)("PersonGroupD").ToString.Trim = ddlPersonGrpD.Text.Trim Then
                        '    ' MessageBox.Message.Alert(Page, "Address already exists!!!", "str")
                        '    lblAlert.Text = "ADDRESS ALREADY EXISTS FOR THIS CONTRACT GROUP AND COMPANY GROUP"
                        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                        '    Exit Sub
                        'End If

                        If txtDisplayRecordsLocationwise.Text = "Y" Then

                            If CustAddr = dataaddr1 And dt.Rows(i)("ContractGroup").ToString.Trim = ddlContractGrp.Text.Trim And dt.Rows(i)("PersonGroupD").ToString.Trim = ddlPersonGrpD.Text.Trim And dt.Rows(i)("Location").ToString.Trim = ddlBranch.Text.Trim Then
                                lblAlert.Text = "THIS ADDRESS ALREADY EXISTS FOR THIS BRANCH, CONTRACT GROUP AND COMPANY GROUP"
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                                Exit Sub
                            End If

                        Else
                            If CustAddr = dataaddr1 And dt.Rows(i)("ContractGroup").ToString.Trim = ddlContractGrp.Text.Trim And dt.Rows(i)("PersonGroupD").ToString.Trim = ddlPersonGrpD.Text.Trim Then
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
                'Dim qry As String = "INSERT INTO tblpersonlocation(personid,Location,BranchID,Description,ContactPerson,Address1,Telephone,Mobile,Email,CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn,AddBlock,AddNos,AddFloor,AddUnit,AddBuilding,AddStreet,AddCity,AddState,AddCountry,AddPostal,LocateGrp,Fax,AccountID,LocationID,LocationPrefix,LocationNo,ServiceName,Contact1Position,Telephone2,ContactPerson2,Contact2Position,Contact2Tel,Contact2Fax,Contact2Tel2,Contact2Mobile,Contact2Email,ServiceAddress, ServiceLocationGroup,BillingNameSvc,BillAddressSvc,BillStreetSvc,BillBuildingSvc,BillCitySvc,BillStateSvc,BillCountrySvc,BillPostalSvc,BillContact1Svc,BillPosition1Svc,BillTelephone1Svc,BillFax1Svc,Billtelephone12Svc,BillMobile1Svc,BillEmail1Svc,BillContact2Svc,BillPosition2Svc,BillTelephone2Svc,BillFax2Svc,Billtelephone22Svc,BillMobile2Svc,BillEmail2Svc, InChargeIdSvc, ArTermSvc, SalesmanSvc, SendServiceReportTo1, SendServiceReportTo2, ContractGroup, Comments)VALUES(@personid,@Location,@BranchID,@Description,@ContactPerson,@Address1,@Telephone,@Mobile,@Email,@CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn,@AddBlock,@AddNos,@AddFloor,@AddUnit,@AddBuilding,@AddStreet,@AddCity,@AddState,@AddCountry,@AddPostal,@LocateGrp,@Fax,@AccountID,@LocationID,@LocationPrefix,@LocationNo,@ServiceName,@Contact1Position,@Tel2,@ContactPerson2,@Contact2Position,@Contact2Tel,@Contact2Fax,@Contact2Tel2,@Contact2Mobile,@Contact2Email,@ServiceAddress, @ServiceLocationGroup,@BillingNameSvc,@BillAddressSvc,@BillStreetSvc,@BillBuildingSvc,@BillCitySvc,@BillStateSvc,@BillCountrySvc,@BillPostalSvc,@BillContact1Svc,@BillPosition1Svc,@BillTelephone1Svc,@BillFax1Svc,@Billtelephone12Svc,@BillMobile1Svc,@BillEmail1Svc,@BillContact2Svc,@BillPosition2Svc,@BillTelephone2Svc,@BillFax2Svc,@Billtelephone22Svc,@BillMobile2Svc,@BillEmail2Svc, @InChargeIdSvc, @ArTermSvc, @SalesmanSvc, @SendServiceReportTo1, @SendServiceReportTo2, @ContractGroup, @Comments);"
                Dim qry As String = "INSERT INTO tblpersonlocation(personid,Location,BranchID,Description,ContactPerson,Address1,Telephone,Mobile,Email,CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn,AddBlock,AddNos,AddFloor,AddUnit,AddBuilding,AddStreet,AddCity,AddState,AddCountry,AddPostal,LocateGrp,Fax,AccountID,LocationID,LocationPrefix,LocationNo,ServiceName,Contact1Position,Telephone2,ContactPerson2,Contact2Position,Contact2Tel,Contact2Fax,Contact2Tel2,Contact2Mobile,Contact2Email,ServiceAddress, ServiceLocationGroup,BillingNameSvc,BillAddressSvc,BillStreetSvc,BillBuildingSvc,BillCitySvc,BillStateSvc,BillCountrySvc,BillPostalSvc,BillContact1Svc,BillPosition1Svc,BillTelephone1Svc,BillFax1Svc,Billtelephone12Svc,BillMobile1Svc,BillEmail1Svc,BillContact2Svc,BillPosition2Svc,BillTelephone2Svc,BillFax2Svc,Billtelephone22Svc,BillMobile2Svc,BillEmail2Svc, InChargeIdSvc, ArTermSvc, SalesmanSvc, SendServiceReportTo1, SendServiceReportTo2, ContractGroup, Comments, PersonGroupD, InActiveD, Industry, MarketSegmentId, DefaultInvoiceFormat, ServiceEmailCC, EmailServiceNotificationOnly, SmartCustomer,MandatoryServiceReportPhotos, siteName)VALUES(@personid,@Location,@BranchID,@Description,@ContactPerson,@Address1,@Telephone,@Mobile,@Email,@CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn,@AddBlock,@AddNos,@AddFloor,@AddUnit,@AddBuilding,@AddStreet,@AddCity,@AddState,@AddCountry,@AddPostal,@LocateGrp,@Fax,@AccountID,@LocationID,@LocationPrefix,@LocationNo,@ServiceName,@Contact1Position,@Tel2,@ContactPerson2,@Contact2Position,@Contact2Tel,@Contact2Fax,@Contact2Tel2,@Contact2Mobile,@Contact2Email,@ServiceAddress, @ServiceLocationGroup,@BillingNameSvc,@BillAddressSvc,@BillStreetSvc,@BillBuildingSvc,@BillCitySvc,@BillStateSvc,@BillCountrySvc,@BillPostalSvc,@BillContact1Svc,@BillPosition1Svc,@BillTelephone1Svc,@BillFax1Svc,@Billtelephone12Svc,@BillMobile1Svc,@BillEmail1Svc,@BillContact2Svc,@BillPosition2Svc,@BillTelephone2Svc,@BillFax2Svc,@Billtelephone22Svc,@BillMobile2Svc,@BillEmail2Svc, @InChargeIdSvc, @ArTermSvc, @SalesmanSvc, @SendServiceReportTo1, @SendServiceReportTo2, @ContractGroup, @Comments, @PersonGroupD, @InActiveD,  @Industry, @MarketSegmentId,  @DefaultInvoiceFormat, @ServiceEmailCC, @EmailServiceNotificationOnly, @SmartCustomer,@MandatoryServiceReportPhotos, @siteName);"

                command.CommandText = qry
                command.Parameters.Clear()

                command.Parameters.AddWithValue("@personid", txtClientID.Text.ToUpper)
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
                command.Parameters.AddWithValue("@AddPostal", txtPostal.Text)
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

                command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text.ToUpper))
                command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text.ToUpper))

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

                'command.Parameters.AddWithValue("@SalesManSvc", ddlSalesManSvc.Text.ToUpper)
                'command.Parameters.AddWithValue("@InchargeIDSvc", ddlInchargeSvc.Text.ToUpper)
                'command.Parameters.AddWithValue("@ARTermSvc", ddlTermsSvc.Text.ToUpper)


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

                If ddlContractGrp.SelectedIndex = 0 Then
                    command.Parameters.AddWithValue("@ContractGroup", "")
                Else
                    'command.Parameters.AddWithValue("@ContractGroup", ddlContractGrp.SelectedValue.ToString)
                    command.Parameters.AddWithValue("@ContractGroup", lContractGroup.Trim)
                End If

                command.Parameters.AddWithValue("@Comments", txtCommentsSvc.Text.ToUpper)


                If ddlPersonGrpD.SelectedIndex = 0 Then
                    command.Parameters.AddWithValue("@PersonGroupD", "")
                Else
                    command.Parameters.AddWithValue("@PersonGroupD", ddlPersonGrpD.SelectedValue.ToString)
                End If


                'command.Parameters.AddWithValue("@PersonGroupD", ddlPersonGrpD.Text.ToUpper)
                command.Parameters.AddWithValue("@InActiveD", False)

                If ddlIndustrysvc.SelectedIndex = 0 Then
                    command.Parameters.AddWithValue("@Industry", "")
                Else
                    command.Parameters.AddWithValue("@Industry", ddlIndustrysvc.SelectedValue.ToString)
                End If
                command.Parameters.AddWithValue("@MarketSegmentId", txtMarketSegmentIDsvc.Text.ToUpper)

                command.Parameters.AddWithValue("@ServiceEmailCC", txtSvcEmailCC.Text.ToUpper)
                command.Parameters.AddWithValue("@EmailServiceNotificationOnly", chkSendEmailNotificationOnly.Checked)

                If ddlSvcDefaultInvoiceFormat.SelectedIndex = 0 Then
                    command.Parameters.AddWithValue("@DefaultInvoiceFormat", "")
                Else
                    command.Parameters.AddWithValue("@DefaultInvoiceFormat", ddlSvcDefaultInvoiceFormat.Text)
                End If

                command.Parameters.AddWithValue("@SmartCustomer", chkSmartCustomer.Checked)
                command.Parameters.AddWithValue("@SiteName", txtSiteName.Text.ToUpper)

                command.Connection = conn

                command.ExecuteNonQuery()
                txtSvcRcno.Text = command.LastInsertedId

                '   MessageBox.Message.Alert(Page, "Record added successfully!!!", "str")
                lblMessage.Text = "ADD: RECORD SUCCESSFULLY ADDED"
                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "PERS", txtLocationID.Text, "ADD", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")), 0, 0, 0, txtAccountID.Text, "", txtRcno.Text)

                lblAlert.Text = ""
                conn.Close()
                conn.Dispose()
                dt.Dispose()
                dr.Close()

                EnableSvcControls()

            ElseIf txtSvcMode.Text = "EDIT" Then
                If txtSvcRcno.Text = "" Then
                    '  MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
                    lblAlert.Text = "SELECT RECORD TO EDIT"
                    Return

                End If
                'Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                'Dim ind As String
                'ind = txtcountry.Text
                'ind = ind.Replace("'", "\\'")

                Dim command2 As MySqlCommand = New MySqlCommand

                command2.CommandType = CommandType.Text

                'command2.CommandText = "SELECT * FROM tblpersonlocation where accountid=@id and rcno<>" & txtSvcRcno.Text
                'command2.Parameters.AddWithValue("@id", txtAccountID.Text)

                If txtDisplayRecordsLocationwise.Text = "Y" Then
                    command2.CommandText = "SELECT * FROM tblcompanylocation where accountid=@id and Location=@Location and rcno<>" & txtSvcRcno.Text
                    command2.Parameters.AddWithValue("@id", txtAccountID.Text)
                    command2.Parameters.AddWithValue("@Location", ddlBranch.Text.Trim)
                Else
                    command2.CommandText = "SELECT * FROM tblcompanylocation where accountid=@id and rcno<>" & txtSvcRcno.Text
                    command2.Parameters.AddWithValue("@id", txtAccountID.Text)
                End If

                command2.Connection = conn

                Dim dr As MySqlDataReader = command2.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                Dim addr As String = txtAddress.Text.Trim + " " + txtStreet.Text.Trim + " " + txtBuilding.Text.Trim
                Dim dataaddr As String
                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1

                        dataaddr = dt.Rows(i)("Address1").ToString + " " + dt.Rows(i)("AddStreet").ToString + " " + dt.Rows(i)("AddBuilding").ToString

                        'If addr = dataaddr And dt.Rows(i)("ContractGroup").ToString.Trim = ddlContractGrp.Text.Trim And dt.Rows(i)("PersonGroupD").ToString.Trim = ddlPersonGrpD.Text.Trim Then
                        '    '  MessageBox.Message.Alert(Page, "Address already exists!!!", "str")
                        '    lblAlert.Text = "ADDRESS ALREADY EXISTS FOR THIS CONTRACT GROUP AND COMPANY GROUP"
                        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                        '    Exit Sub
                        'End If

                        If txtDisplayRecordsLocationwise.Text = "Y" Then
                            If addr = dataaddr And dt.Rows(i)("ContractGroup").ToString.Trim = ddlContractGrp.Text.Trim And dt.Rows(i)("PersonGroupD").ToString.Trim = ddlPersonGrpD.Text.Trim And dt.Rows(i)("Location").ToString.Trim = ddlBranch.Text.Trim Then
                                '  MessageBox.Message.Alert(Page, "Address already exists!!!", "str")
                                'lblAlert.Text = "ADDRESS ALREADY EXISTS FOR THIS CONTRACT GROUP AND COMPANY GROUP"
                                lblAlert.Text = "THIS ADDRESS ALREADY EXISTS FOR THIS BRANCH, CONTRACT GROUP AND COMPANY GROUP"

                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                                Exit Sub
                            End If
                        Else
                            If addr = dataaddr And dt.Rows(i)("ContractGroup").ToString.Trim = ddlContractGrp.Text.Trim And dt.Rows(i)("PersonGroupD").ToString.Trim = ddlPersonGrpD.Text.Trim Then
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

                command1.CommandText = "SELECT * FROM tblpersonlocation where rcno=" & Convert.ToInt32(txtSvcRcno.Text)
                command1.Connection = conn

                Dim dr1 As MySqlDataReader = command1.ExecuteReader()
                Dim dt1 As New DataTable
                dt1.Load(dr1)

                If dt1.Rows.Count > 0 Then

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String

                    'qry = "UPDATE tblpersonlocation SET personid = @personid, Description = @Description,ContactPerson = @ContactPerson,Address1 = @Address1,Telephone = @Telephone,Mobile = @Mobile,Email = @Email, LastModifiedBy = @LastModifiedBy,LastModifiedOn = @LastModifiedOn, AddBuilding = @AddBuilding,AddStreet = @AddStreet,AddCity = @AddCity,AddState = @AddState,AddCountry = @AddCountry,AddPostal = @AddPostal,LocateGrp = @LocateGrp,Fax = @Fax,ServiceName = @ServiceName,Contact1Position = @Contact1Position,Telephone2 = @Tel2,ContactPerson2 = @ContactPerson2,Contact2Position = @Contact2Position,Contact2Tel = @Contact2Tel,Contact2Fax = @Contact2Fax,Contact2Tel2 = @Contact2Tel2,Contact2Mobile = @Contact2Mobile,Contact2Email = @Contact2Email,ServiceAddress=@ServiceAddress, ServiceLocationGroup= @ServiceLocationGroup,BillingNameSvc = @BillingNameSvc,BillAddressSvc = @BillAddressSvc,BillStreetSvc = @BillStreetSvc,BillBuildingSvc = @BillBuildingSvc,BillCitySvc = @BillCitySvc,BillStateSvc = @BillStateSvc,BillCountrySvc = @BillCountrySvc,BillPostalSvc = @BillPostalSvc,BillContact1Svc = @BillContact1Svc,BillPosition1Svc = @BillPosition1Svc,BillTelephone1Svc = @BillTelephone1Svc,BillFax1Svc = @BillFax1Svc,Billtelephone12Svc = @Billtelephone12Svc,BillMobile1Svc = @BillMobile1Svc,BillEmail1Svc = @BillEmail1Svc,BillContact2Svc = @BillContact2Svc,BillPosition2Svc = @BillPosition2Svc,BillTelephone2Svc = @BillTelephone2Svc,BillFax2Svc = @BillFax2Svc,Billtelephone22Svc = @Billtelephone22Svc,BillMobile2Svc = @BillMobile2Svc,BillEmail2Svc = @BillEmail2Svc, InChargeIdSvc=@InChargeIdSvc, ArTermSvc=@ArTermSvc, SalesmanSvc=@SalesmanSvc, SendServiceReportTo1=@SendServiceReportTo1, SendServiceReportTo2=@SendServiceReportTo2,ContractGroup = @ContractGroup, Comments=@Comments where rcno=" & Convert.ToInt32(txtSvcRcno.Text)
                    qry = "UPDATE tblpersonlocation SET personid = @personid, Description = @Description,ContactPerson = @ContactPerson,Address1 = @Address1,Telephone = @Telephone,Mobile = @Mobile,Email = @Email, LastModifiedBy = @LastModifiedBy,LastModifiedOn = @LastModifiedOn, AddBuilding = @AddBuilding,AddStreet = @AddStreet,AddCity = @AddCity,AddState = @AddState,AddCountry = @AddCountry,AddPostal = @AddPostal,LocateGrp = @LocateGrp,Fax = @Fax,ServiceName = @ServiceName,Contact1Position = @Contact1Position,Telephone2 = @Tel2,ContactPerson2 = @ContactPerson2,Contact2Position = @Contact2Position,Contact2Tel = @Contact2Tel,Contact2Fax = @Contact2Fax,Contact2Tel2 = @Contact2Tel2,Contact2Mobile = @Contact2Mobile,Contact2Email = @Contact2Email,ServiceAddress=@ServiceAddress, ServiceLocationGroup= @ServiceLocationGroup,BillingNameSvc = @BillingNameSvc,BillAddressSvc = @BillAddressSvc,BillStreetSvc = @BillStreetSvc,BillBuildingSvc = @BillBuildingSvc,BillCitySvc = @BillCitySvc,BillStateSvc = @BillStateSvc,BillCountrySvc = @BillCountrySvc,BillPostalSvc = @BillPostalSvc,BillContact1Svc = @BillContact1Svc,BillPosition1Svc = @BillPosition1Svc,BillTelephone1Svc = @BillTelephone1Svc,BillFax1Svc = @BillFax1Svc,Billtelephone12Svc = @Billtelephone12Svc,BillMobile1Svc = @BillMobile1Svc,BillEmail1Svc = @BillEmail1Svc,BillContact2Svc = @BillContact2Svc,BillPosition2Svc = @BillPosition2Svc,BillTelephone2Svc = @BillTelephone2Svc,BillFax2Svc = @BillFax2Svc,Billtelephone22Svc = @Billtelephone22Svc,BillMobile2Svc = @BillMobile2Svc,BillEmail2Svc = @BillEmail2Svc, InChargeIdSvc=@InChargeIdSvc, ArTermSvc=@ArTermSvc, SalesmanSvc=@SalesmanSvc, SendServiceReportTo1=@SendServiceReportTo1, SendServiceReportTo2=@SendServiceReportTo2,ContractGroup = @ContractGroup, Comments=@Comments, PersonGroupD=@PersonGroupD, InActiveD=@InActiveD,  Industry=@Industry, MarketSegmentId=@MarketSegmentId, DefaultInvoiceFormat=@DefaultInvoiceFormat, ServiceEmailCC=@ServiceEmailCC, Location=@Location, EmailServiceNotificationOnly =@EmailServiceNotificationOnly, SmartCustomer=@SmartCustomer,MandatoryServiceReportPhotos=@MandatoryServiceReportPhotos, siteName= @siteName where rcno=" & Convert.ToInt32(txtSvcRcno.Text)

                    command.CommandText = qry
                    command.Parameters.Clear()

                    command.Parameters.AddWithValue("@personid", txtClientID.Text.ToUpper)
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
                        command.Parameters.AddWithValue("@AddCity", ddlCity.Text)
                    End If
                    If ddlCountry.Text = txtDDLText.Text Then
                        command.Parameters.AddWithValue("@AddCountry", "")
                    Else
                        command.Parameters.AddWithValue("@AddCountry", ddlCountry.Text)
                    End If
                    command.Parameters.AddWithValue("@AddPostal", txtPostal.Text)
                    If ddlLocateGrp.Text = txtDDLText.Text Then
                        command.Parameters.AddWithValue("@LocateGRP", "")

                    Else
                        command.Parameters.AddWithValue("@LocateGRP", ddlLocateGrp.Text)

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
                    command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text.ToUpper))

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

                    If ddlContractGrp.SelectedIndex = 0 Then
                        command.Parameters.AddWithValue("@ContractGroup", "")
                    Else
                        'command.Parameters.AddWithValue("@ContractGroup", ddlContractGrp.SelectedValue.ToString)
                        command.Parameters.AddWithValue("@ContractGroup", lContractGroup.Trim)
                    End If

                    command.Parameters.AddWithValue("@Comments", txtCommentsSvc.Text.ToUpper)

                    'command.Parameters.AddWithValue("@PersonGroupD", ddlPersonGrpD.Text.ToUpper)
                    If ddlPersonGrpD.SelectedIndex = 0 Then
                        command.Parameters.AddWithValue("@PersonGroupD", "")
                    Else
                        command.Parameters.AddWithValue("@PersonGroupD", ddlPersonGrpD.SelectedValue.ToString)
                    End If


                    'command.Parameters.AddWithValue("@InActiveD", False)
                    command.Parameters.AddWithValue("@InActiveD", chkInactiveD.Checked)

                    If ddlIndustrysvc.SelectedIndex = 0 Then
                        command.Parameters.AddWithValue("@Industry", "")
                    Else
                        command.Parameters.AddWithValue("@Industry", ddlIndustrysvc.SelectedValue.ToString)
                    End If
                    command.Parameters.AddWithValue("@MarketSegmentId", txtMarketSegmentIDsvc.Text.ToUpper)

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
                    command.Parameters.AddWithValue("@SiteName", txtSiteName.Text.ToUpper)
                    command.Connection = conn
                    command.ExecuteNonQuery()



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


                        lAddressContract = ""

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

                                UpdateContractHeader(lIsOpenContarctExist.Trim)

                                'If lIsOpenContarctExist <> lIsOpenContarctExistNew Then
                                'Dim commandUpdContractServiceAddress As MySqlCommand = New MySqlCommand
                                'commandUpdContractServiceAddress.CommandType = CommandType.Text
                                'commandUpdContractServiceAddress.CommandText = "Update tblContract set CustName = """ & txtServiceName.Text.ToUpper & """, ServiceAddress = '" & lAddressContract.Trim & "'  where ContractNo = '" & lIsOpenContarctExist.Trim & "'"
                                'commandUpdContractServiceAddress.Connection = conn
                                'commandUpdContractServiceAddress.ExecuteNonQuery()

                                'commandUpdContractServiceAddress.Dispose()
                                'lAddressContract = ""
                                lIsOpenContarctExist = ""
                                lIsOpenContarctExist = dtIsOpenContarctExist.Rows(x)("ContractNo").ToString
                            End If


                            ''''''''''''''''''''''''''''''''''''
                            '''''''''''''''''''''''''''''''''''''''''''''
                            Dim cmdLoopContarctDet As MySqlCommand = New MySqlCommand

                            cmdLoopContarctDet.CommandType = CommandType.Text

                            cmdLoopContarctDet.CommandText = "SELECT AccountId, ContractNo, LocationId,Address1 FROM tblContractDet where ContractNo = '" & lIsOpenContarctExist & "' order by LocationID"
                            cmdLoopContarctDet.Parameters.AddWithValue("@LocationId", txtLocationID.Text)
                            cmdLoopContarctDet.Connection = conn

                            Dim drLoopContarctDet As MySqlDataReader = cmdLoopContarctDet.ExecuteReader()
                            Dim dtLoopContarctDet As New System.Data.DataTable
                            dtLoopContarctDet.Load(drLoopContarctDet)

                            If dtLoopContarctDet.Rows.Count > 0 Then

                                '''''''''''''''''''''''''''''''''

                                For y As Integer = 0 To dtLoopContarctDet.Rows.Count - 1

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

                    'Dim commandSvc As MySqlCommand = New MySqlCommand

                    'commandSvc.CommandType = CommandType.Text
                    'Dim qrySvc As String
                    'qrySvc = ""

                    ' '''''''''''''''''
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
                    'commandSvc.CommandText = qrySvc
                    'commandSvc.Parameters.Clear()

                    'commandSvc.Parameters.AddWithValue("@ContactPerson", txtSvcCP1Contact.Text.ToUpper)
                    'commandSvc.Parameters.AddWithValue("@Address1", txtAddress.Text.ToUpper)
                    'commandSvc.Parameters.AddWithValue("@Telephone", txtSvcCP1Telephone.Text.ToUpper)
                    'commandSvc.Parameters.AddWithValue("@Mobile", txtSvcCP1Mobile.Text.ToUpper)
                    'commandSvc.Parameters.AddWithValue("@Email", txtSvcCP1Email.Text.ToUpper)

                    'commandSvc.Parameters.AddWithValue("@AddBuilding", txtBuilding.Text.ToUpper)
                    'commandSvc.Parameters.AddWithValue("@AddStreet", txtStreet.Text.ToUpper)
                    'If ddlState.Text = txtDDLText.Text Then
                    '    commandSvc.Parameters.AddWithValue("@AddState", "")
                    'Else
                    '    commandSvc.Parameters.AddWithValue("@AddState", ddlState.Text.ToUpper)
                    'End If

                    'If ddlCity.Text = txtDDLText.Text Then
                    '    commandSvc.Parameters.AddWithValue("@AddCity", "")
                    'Else
                    '    commandSvc.Parameters.AddWithValue("@AddCity", ddlCity.Text.ToUpper)
                    'End If
                    'If ddlCountry.Text = txtDDLText.Text Then
                    '    commandSvc.Parameters.AddWithValue("@AddCountry", "")
                    'Else
                    '    commandSvc.Parameters.AddWithValue("@AddCountry", ddlCountry.Text.ToUpper)
                    'End If
                    'commandSvc.Parameters.AddWithValue("@AddPostal", txtPostal.Text.ToUpper)
                    'If ddlLocateGrp.Text = txtDDLText.Text Then
                    '    commandSvc.Parameters.AddWithValue("@LocateGRP", "")
                    'Else
                    '    commandSvc.Parameters.AddWithValue("@LocateGRP", ddlLocateGrp.Text.ToUpper)
                    'End If
                    'commandSvc.Parameters.AddWithValue("@Fax", txtSvcCP1Fax.Text.ToUpper)
                    'commandSvc.Parameters.AddWithValue("@ServiceName", txtServiceName.Text.ToUpper)
                    'commandSvc.Parameters.AddWithValue("@Contact1Position", txtSvcCP1Position.Text.ToUpper)
                    'commandSvc.Parameters.AddWithValue("@ContactPerson2", txtSvcCP2Contact.Text.ToUpper)
                    'commandSvc.Parameters.AddWithValue("@Contact2Position", txtSvcCP2Position.Text.ToUpper)
                    'commandSvc.Parameters.AddWithValue("@Contact2Tel", txtSvcCP2Telephone.Text.ToUpper)
                    'commandSvc.Parameters.AddWithValue("@Contact2Fax", txtSvcCP2Fax.Text.ToUpper)
                    'commandSvc.Parameters.AddWithValue("@Contact2Tel2", txtSvcCP2Tel2.Text.ToUpper)
                    'commandSvc.Parameters.AddWithValue("@Contact2Mobile", txtSvcCP2Mobile.Text.ToUpper)
                    'commandSvc.Parameters.AddWithValue("@Contact2Email", txtSvcCP2Email.Text.ToUpper)
                    'commandSvc.Parameters.AddWithValue("@Telephone2", txtSvcCP1Telephone2.Text.ToUpper)

                    'Dim lOtherEmail As String
                    'lOtherEmail = ""

                    ''If lSendServiceReportTo1Loc = "Y" Then
                    'lOtherEmail = txtBillEmail1Svc.Text.Trim
                    ''End If

                    ''If lSendServiceReportTo2Loc = "Y" Then
                    'If String.IsNullOrEmpty(lOtherEmail.Trim) = False Then
                    '    lOtherEmail = lOtherEmail.Trim() & "; " & txtBillEmail2Svc.Text.Trim()
                    'Else
                    '    lOtherEmail = txtBillEmail2Svc.Text.Trim()
                    'End If
                    ''End If
                    'commandSvc.Parameters.AddWithValue("@OtherEmail", lOtherEmail.ToUpper)
                    'commandSvc.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                    'commandSvc.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                    'commandSvc.Parameters.AddWithValue("@AccountID", txtAccountID.Text.ToUpper)
                    'commandSvc.Parameters.AddWithValue("@LocationID", txtLocationID.Text.ToUpper)

                    'commandSvc.Connection = conn
                    'commandSvc.ExecuteNonQuery()

                    'commandSvc.Dispose()




                    Dim commandSvc As MySqlCommand = New MySqlCommand

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

                    If chkServiceReportSendTo1.Checked = True Then
                        lOtherEmail = txtBillEmail1Svc.Text.Trim
                    End If


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

                    '''''''''''''''''' END: UPDATE SERVICE LOCATION '''''''''''''''''''''''''''''''''



                    lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED"
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "PERS", txtLocationID.Text, "EDIT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")), 0, 0, 0, txtAccountID.Text, "", txtRcno.Text)

                    lblAlert.Text = ""
                End If


                conn.Close()
                conn.Dispose()
                command1.Dispose()
                command2.Dispose()
                dt.Dispose()
                dt1.Dispose()
                dr.Close()
                dr1.Close()

                'Catch ex As Exception
                '    MessageBox.Message.Alert(Page, ex.ToString, "str")
                'End Try
                EnableSvcControls()
            End If

            If txtDisplayRecordsLocationwise.Text = "N" Then
                If String.IsNullOrEmpty(txtAccountID.Text) Then
                    SqlDataSource2.SelectCommand = "SELECT * FROM tblpersonlocation where personid = '" & txtClientID.Text & "'"
                Else
                    SqlDataSource2.SelectCommand = "SELECT * FROM tblpersonlocation where accountid = '" & txtAccountID.Text & "'"
                End If
            End If

            If txtDisplayRecordsLocationwise.Text = "Y" Then
                If String.IsNullOrEmpty(txtAccountID.Text) Then
                    SqlDataSource2.SelectCommand = "SELECT * FROM tblpersonlocation where personid = '" & txtClientID.Text & "'"
                Else
                    SqlDataSource2.SelectCommand = "SELECT * FROM tblpersonlocation where accountid = '" & txtAccountID.Text & "' and location in (Select LocationID  from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "')"
                End If
            End If

            SqlDataSource2.DataBind()
            GridView2.DataBind()
            ' MakeSvcNull()
            txtSvcMode.Text = ""
            AddrConcat()


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
            btnQuit.Enabled = True
            btnQuit.ForeColor = System.Drawing.Color.Black
            btnTransactions.Enabled = True
            btnTransactions.ForeColor = System.Drawing.Color.Black
            btnChangeStatus.Enabled = True
            btnChangeStatus.ForeColor = System.Drawing.Color.Black

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

            btnSpecificLocation.Enabled = True
            btnSpecificLocation.ForeColor = System.Drawing.Color.Black

            btnTransfersSvc.Enabled = True
            btnTransfersSvc.ForeColor = System.Drawing.Color.Black

            AccessControl()

            'InsertNewLogDetail()

            'btnSvcSave.Enabled = False
            'btnSvcSave.ForeColor = System.Drawing.Color.Gray
            'btnSvcCancel.Enabled = False
            'btnSvcCancel.ForeColor = System.Drawing.Color.Gray
        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "btnSvcSave_Click", ex.Message.ToString, txtAccountID.Text)
        End Try
    End Sub


    Private Sub UpdateContractHeader(lContractNo As String)
        Try
            Dim conn1 As MySqlConnection = New MySqlConnection()

            conn1.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn1.Open()

            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text
            command1.CommandText = "SELECT * FROM tblcontractdet where ContractNo ='" & lContractNo & "' order by LocationId"
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
            'commandUpdContractServiceAddress1.CommandText = "Update tblContract set Location = '" & lLocation & "',   CustName = """ & txtServiceName.Text.ToUpper & """, ServiceAddress = """ & lServiceAddressCons & """, BillAddress1 = """ & txtBillAddressSvc.Text.ToUpper & ", " & txtBillStreetSvc.Text.ToUpper & ", " & txtBillBuildingSvc.Text.ToUpper & ", " & ddlBillCitySvc.Text.ToUpper & ", " & txtBillPostalSvc.Text & """  where ContractNo = '" & lContractNo.Trim & "'"
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
            exstr = ex.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("PERSON - " + Session("UserID"), "FUNCTION UpdateContractHeader", ex.Message.ToString, txtLocationID.Text)
            Exit Sub

        End Try
    End Sub
    Protected Sub btnSvcEdit_Click(sender As Object, e As EventArgs) Handles btnSvcEdit.Click
        lblMessage.Text = ""
        If txtSvcRcno.Text = "" Then
            ' MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
            lblAlert.Text = "SELECT RECORD TO EDIT"
            Return

        End If
        DisableSvcControls()
        txtSvcMode.Text = "EDIT"
        lblMessage.Text = "ACTION: EDIT SERVICE LOCATION"

        btnSvcContract.Enabled = False
        btnSvcService.Enabled = False

        btnSvcContract.ForeColor = System.Drawing.Color.Gray
        btnSvcService.ForeColor = System.Drawing.Color.Gray

        btnTransfersSvc.Enabled = False
        btnTransfersSvc.ForeColor = System.Drawing.Color.Gray

        btnSpecificLocation.Enabled = False
        btnSpecificLocation.ForeColor = System.Drawing.Color.Gray

        'btnTransfersSvc.Enabled = False
        'btnTransfersSvc.ForeColor = System.Drawing.Color.Gray

        'ddlContractGrp.Enabled = False
        btnEditContractGroup.Visible = False


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

    End Sub

    Protected Sub btnSvcCancel_Click(sender As Object, e As EventArgs) Handles btnSvcCancel.Click
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
        btnDelete.Enabled = True
        btnDelete.ForeColor = System.Drawing.Color.Black

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

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                command1.CommandText = "SELECT * FROM tblpersonlocation where rcno=" & Convert.ToInt32(txtSvcRcno.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "delete from tblpersonlocation where rcno=" & Convert.ToInt32(txtSvcRcno.Text)

                    command.CommandText = qry

                    command.Connection = conn

                    command.ExecuteNonQuery()

                    '  MessageBox.Message.Alert(Page, "Record deleted successfully!!!", "str")
                    lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "PERS", txtLocationID.Text, "DELETE", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")), 0, 0, 0, txtAccountID.Text, "", txtRcno.Text)

                End If
                conn.Close()
                conn.Dispose()

                command1.Dispose()
                dt.Dispose()
                dr.Close()

            Catch ex As Exception
                InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "btnSvcDelete_Click", ex.Message.ToString, txtAccountID.Text)
            End Try
            EnableSvcControls()

            If String.IsNullOrEmpty(txtAccountID.Text) Then
                SqlDataSource2.SelectCommand = "SELECT * FROM tblpersonlocation where personid = " & txtClientID.Text
            Else
                SqlDataSource2.SelectCommand = "SELECT * FROM tblpersonlocation where accountid = " & txtAccountID.Text
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
        Try
            '    Dim qry As String
            '    qry = "select * from tblcompany where id='E127-1221';" 'accountid ='10000006'" 'in (select accountid from tblpersonlocation where rcno <> 0"

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
            qry = "SELECT * FROM tblpersonlocation where accountid='" & txtAccountIDtab2.Text & "'"

            If String.IsNullOrEmpty(txtSearch.Text) = False Then
                qry = qry + " and (locationid='" & txtSearch.Text & "'"
                ' or address1='" & txtSearch.Text & "' or addstreet='" & txtSearch.Text & "' or addbuilding='" & txtSearch.Text & "'or addpostal='" & txtSearch.Text & "';"
                qry = qry + " or description like '%" + txtSearch.Text + "%'"
                qry = qry + " or address1 like '%" + txtSearch.Text + "%'"
                qry = qry + " or addbuilding like '%" + txtSearch.Text + "%'"
                qry = qry + " or addstreet like '%" + txtSearch.Text + "%'"
                qry = qry + " or addpostal like '" + txtSearch.Text + "%')"
            End If

            MakeSvcNull()
            SqlDataSource2.SelectCommand = qry
            SqlDataSource2.DataBind()
            GridView2.DataBind()
            txtSearch.Text = "Search here"
        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "btnSvcSearch_Click", ex.Message.ToString, txtAccountID.Text)
        End Try
    End Sub


    Private Sub AddrConcat()
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

    End Sub

    Protected Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Try
            txtSearchText.Text = txtSearch.Text

            Dim qry As String
            qry = "SELECT * FROM tblpersonlocation where accountid='" & txtAccountIDtab2.Text & "'"

            If String.IsNullOrEmpty(txtSearch.Text) = False Then
                qry = qry + " and (locationid like ""%" & txtSearch.Text & "%"""
                ' or address1='" & txtSearch.Text & "' or addstreet='" & txtSearch.Text & "' or addbuilding='" & txtSearch.Text & "'or addpostal='" & txtSearch.Text & "';"
                qry = qry + " or personid like ""%" + txtSearch.Text + "%"""
                qry = qry + " or servicename like ""%" + txtSearch.Text + "%"""
                qry = qry + " or billingnamesvc like ""%" + txtSearch.Text + "%"""
                qry = qry + " or ContractGroup like ""%" + txtSearch.Text + "%"""
                qry = qry + " or ContactPerson like ""%" + txtSearch.Text + "%"""
                qry = qry + " or BillContact1Svc like ""%" + txtSearch.Text + "%"""
                qry = qry + " or comments like ""%" + txtSearch.Text + "%"""
                qry = qry + " or description like ""%" + txtSearch.Text + "%"""
                qry = qry + " or address1 like ""%" + txtSearch.Text + "%"""
                qry = qry + " or addbuilding like ""%" + txtSearch.Text + "%"""
                qry = qry + " or addstreet like ""%" + txtSearch.Text + "%"""
                qry = qry + " or addpostal like """ + txtSearch.Text + "%"")"
            End If

            MakeSvcNull()
            SqlDataSource2.SelectCommand = qry
            SqlDataSource2.DataBind()
            GridView2.DataBind()
            lblMessage.Text = "SEARCH CRITERIA : " + txtSearch.Text + " <br/>NUMBER OF RECORDS FOUND : " + GridView2.Rows.Count.ToString

            'txtSearch.Text = "Search Here for Location Address, Postal Code or Description"
            txtDetail.Text = qry
        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "txtSearch_TextChanged", ex.Message.ToString, txtAccountID.Text)
        End Try
    End Sub

    Protected Sub txtOffCont1Fax_TextChanged(sender As Object, e As EventArgs) Handles txtOffCont1Fax.TextChanged

    End Sub


    Protected Sub btnReset_Click(sender As Object, e As ImageClickEventArgs) Handles btnReset.Click
        Try
            txtSearch.Text = ""
            txtSearchText.Text = ""
            If String.IsNullOrEmpty(txtAccountID.Text) Then
                SqlDataSource2.SelectCommand = "SELECT * FROM tblpersonlocation where accountid is null and personid = '" & txtClientID.Text & "'"
            Else
                SqlDataSource2.SelectCommand = "SELECT * FROM tblpersonlocation where accountid = " & txtAccountID.Text
            End If

            SqlDataSource2.DataBind()
            GridView2.DataBind()
        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "btnReset_Click", ex.Message.ToString, txtAccountID.Text)
        End Try
    End Sub

    Protected Sub btnResetSearch_Click(sender As Object, e As ImageClickEventArgs) Handles btnResetSearch.Click
        Try
            MakeMeNull()
            EnableControls()
            txtSearchCust.Text = ""
            txtSearchCustText.Text = ""

            txt.Text = "SELECT * FROM tblperson a left join CustomerBal b on a.Accountid = b.Accountid where a.Inactive=0 ORDER BY a.rcno DESC, a.Name limit 100;"
            SqlDataSource1.SelectCommand = txt.Text
            SqlDataSource1.DataBind()
            GridView1.DataBind()
            lblMessage.Text = ""
        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "btnResetSearch_Click", ex.Message.ToString, txtAccountID.Text)
        End Try
    End Sub

    Protected Sub txtSearchCust_TextChanged(sender As Object, e As EventArgs) Handles txtSearchCust.TextChanged
        Try
            'txtSearchCustText.Text = txtSearchCust.Text

            btnGoCust_Click(sender, e)

            'Dim qry As String
            ''txt.Text = " SELECT distinct a.Rcno, a.AccountId, a.InActive, a.ID, a.Name, a.ARCurrency, a.Location, b.Bal, a.TelMobile, a.TelFax, a.Address1, a.AddPOstal, a.BillAddress1, a.BillPostal, a.NRIC, a.ICType, a.Nationality, a.Sex, a.LocateGrp, a.PersonGroup, a.AccountNo, a.Salesman, a.AddStreet, a.AddBuilding, A.AddCity, A.AddState, A.AddCountry, a.BillStreet, a.BillBuilding, A.BillCity, A.BillState, A.BillCountry,  a.CreatedBy, a.CreatedOn, a.LastModifiedBy, a.LastModifiedOn  FROM tblperson a left join personbal b on a.Accountid = b.Accountid left join tblPersonLocation c on a.Accountid = c.Accountid where a.Inactive=0 "

            ''If txtDisplayRecordsLocationwise.Text = "N" Then
            'qry = "select  tblperson.Rcno, tblperson.AccountId, tblperson.InActive, tblperson.ID, tblperson.Name, tblperson.ARCurrency, tblperson.Location, CustomerBal.Bal, tblperson.TelMobile, tblperson.TelFax, tblperson.Address1, tblperson.AddPOstal, tblperson.BillAddress1, tblperson.BillPostal, tblperson.NRIC, tblperson.ICType, tblperson.Nationality, tblperson.Sex, tblperson.LocateGrp, tblperson.PersonGroup, tblperson.AccountNo, tblperson.Salesman, tblperson.AddStreet, tblperson.AddBuilding, tblperson.AddCity, tblperson.AddState, tblperson.AddCountry, tblperson.BillStreet, tblperson.BillBuilding, tblperson.BillCity, tblperson.BillState, tblperson.BillCountry,  tblperson.CreatedBy, tblperson.CreatedOn, tblperson.LastModifiedBy, tblperson.LastModifiedOn,tblperson.AutoEmailInvoice,tblperson.AutoEmailSOA,tblperson.UnsubscribeAutoEmailDate from tblperson left join CustomerBal  on tblperson.Accountid = CustomerBal.Accountid where 1=1 "

            'If String.IsNullOrEmpty(txtSearchCust.Text) = False Then
            '    qry = qry + " and (tblperson.accountid like ""%" & txtSearchCust.Text & "%"""
            '    qry = qry + " or tblperson.id like ""%" + txtSearchCust.Text + "%"""

            '    qry = qry + " or tblperson.address1 like ""%" + txtSearchCust.Text + "%"""
            '    qry = qry + " or tblperson.addbuilding like ""%" + txtSearchCust.Text + "%"""
            '    qry = qry + " or tblperson.addstreet like ""%" + txtSearchCust.Text + "%"""
            '    qry = qry + " or tblperson.billaddress1 like ""%" + txtSearchCust.Text + "%"""
            '    qry = qry + " or tblperson.billbuilding like ""%" + txtSearchCust.Text + "%"""
            '    qry = qry + " or tblperson.billstreet like ""%" + txtSearchCust.Text + "%"""
            '    qry = qry + " or tblperson.addpostal like """ + txtSearchCust.Text + "%"""
            '    qry = qry + " or tblperson.billpostal like """ + txtSearchCust.Text + "%"""
            '    qry = qry + " or tblperson.name like ""%" + txtSearchCust.Text + "%"""
            '    'qry = qry + " or tblperson.persongroup like ""%" + txtSearchCust.Text + "%"""
            '    qry = qry + " or tblperson.salesman  like ""%" + txtSearchCust.Text + "%"""
            '    qry = qry + " or tblperson.telmobile  like ""%" + txtSearchCust.Text + "%"" or tblperson.telhome like ""%" + txtSearchCust.Text + "%"""
            '    qry = qry + " or tblperson.telpager  like ""%" + txtSearchCust.Text + "%"""
            '    qry = qry + " or tblperson.rescp2Tel  like ""%" + txtSearchCust.Text + "%"""
            '    qry = qry + " or tblperson.rescp2Tel2  like ""%" + txtSearchCust.Text + "%"""
            '    qry = qry + " or tblperson.rescp2Mobile  like ""%" + txtSearchCust.Text + "%"""
            '    qry = qry + " or tblperson.BillTelhome  like ""%" + txtSearchCust.Text + "%"""
            '    qry = qry + " or tblperson.BillTelpager  like ""%" + txtSearchCust.Text + "%"""
            '    qry = qry + " or tblperson.BilltelMobile  like ""%" + txtSearchCust.Text + "%"""
            '    qry = qry + " or tblperson.Billcontact2Tel  like ""%" + txtSearchCust.Text + "%"""
            '    qry = qry + " or tblperson.Billcontact2Tel2  like ""%" + txtSearchCust.Text + "%"""
            '    qry = qry + " or tblperson.Billcontact2Mobile  like ""%" + txtSearchCust.Text + "%"""
            '    qry = qry + " or tblperson.BillingName  like ""%" + txtSearchCust.Text + "%"""
            '    qry = qry + " or tblperson.locategrp  like ""%" + txtSearchCust.Text + "%"""

            '    qry = qry + " or tblperson.accountid in (select accountid from tblpersonlocation where rcno<>0"

            '    qry = qry + " and (tblpersonlocation.address1 like ""%" + txtSearchCust.Text + "%"""
            '    qry = qry + " or tblpersonlocation.servicename like ""%" + txtSearchCust.Text + "%"""

            '    qry = qry + " or tblpersonlocation.addbuilding like ""%" + txtSearchCust.Text + "%"""
            '    qry = qry + " or tblpersonlocation.addstreet like ""%" + txtSearchCust.Text + "%"""
            '    qry = qry + " or tblpersonlocation.addpostal like """ + txtSearchCust.Text + "%"")))"

            'End If
            ''End If

            ''If txtDisplayRecordsLocationwise.Text = "Y" Then
            ''    'qry = qry + " and Location = '" & txtLocation.Text.Trim & "'"
            ''    qry = qry & " and location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "')"

            ''End If


            ' '''''''''''''''''''''''''''''''''

            ''If txtDisplayRecordsLocationwise.Text = "Y" Then
            ''    qry = " SELECT  tblperson.Rcno, tblperson.AccountId, tblperson.InActive, tblperson.ID, tblperson.Name, tblperson.ARCurrency, tblperson.Location, PersonBal.Bal, tblperson.TelMobile, tblperson.TelFax, tblperson.Address1, tblperson.AddPOstal, tblperson.BillAddress1, tblperson.BillPostal, tblperson.NRIC, tblperson.ICType, tblperson.Nationality, tblperson.Sex, tblperson.LocateGrp, tblperson.PersonGroup, tblperson.AccountNo, tblperson.Salesman, tblperson.AddStreet, tblperson.AddBuilding, tblperson.AddCity, tblperson.AddState, tblperson.AddCountry, tblperson.BillStreet, tblperson.BillBuilding, tblperson.BillCity, tblperson.BillState, tblperson.BillCountry,  tblperson.CreatedBy, tblperson.CreatedOn, tblperson.LastModifiedBy, tblperson.LastModifiedOn FROM tblperson  left join Personbal  on tblperson.Accountid = Personbal.Accountid left join tblPersonLocation  on tblPerson.Accountid = tblPersonLocation.Accountid where tblPerson.Inactive=0 "
            ''    qry = qry + " and tblPersonLocation.location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "')"

            ''    If String.IsNullOrEmpty(txtSearchCust.Text) = False Then
            ''        qry = qry + " and (tblperson.accountid like ""%" & txtSearchCust.Text & "%"""
            ''        qry = qry + " or tblperson.id like ""%" + txtSearchCust.Text + "%"""

            ''        qry = qry + " or tblperson.address1 like ""%" + txtSearchCust.Text + "%"""
            ''        qry = qry + " or tblperson.addbuilding like ""%" + txtSearchCust.Text + "%"""
            ''        qry = qry + " or tblperson.addstreet like ""%" + txtSearchCust.Text + "%"""
            ''        qry = qry + " or tblperson.billaddress1 like ""%" + txtSearchCust.Text + "%"""
            ''        qry = qry + " or tblperson.billbuilding like ""%" + txtSearchCust.Text + "%"""
            ''        qry = qry + " or tblperson.billstreet like ""%" + txtSearchCust.Text + "%"""
            ''        qry = qry + " or tblperson.addpostal like """ + txtSearchCust.Text + "%"""
            ''        qry = qry + " or tblperson.billpostal like """ + txtSearchCust.Text + "%"""
            ''        qry = qry + " or tblperson.name like ""%" + txtSearchCust.Text + "%"""
            ''        'qry = qry + " or tblperson.persongroup like ""%" + txtSearchCust.Text + "%"""
            ''        qry = qry + " or tblperson.salesman  like ""%" + txtSearchCust.Text + "%"""
            ''        qry = qry + " or tblperson.telmobile  like ""%" + txtSearchCust.Text + "%"" or tblperson.telhome like ""%" + txtSearchCust.Text + "%"""
            ''        qry = qry + " or tblperson.telpager  like ""%" + txtSearchCust.Text + "%"""
            ''        qry = qry + " or tblperson.rescp2Tel  like ""%" + txtSearchCust.Text + "%"""
            ''        qry = qry + " or tblperson.rescp2Tel2  like ""%" + txtSearchCust.Text + "%"""
            ''        qry = qry + " or tblperson.rescp2Mobile  like ""%" + txtSearchCust.Text + "%"""
            ''        qry = qry + " or tblperson.BillTelhome  like ""%" + txtSearchCust.Text + "%"""
            ''        qry = qry + " or tblperson.BillTelpager  like ""%" + txtSearchCust.Text + "%"""
            ''        qry = qry + " or tblperson.BilltelMobile  like ""%" + txtSearchCust.Text + "%"""
            ''        qry = qry + " or tblperson.Billcontact2Tel  like ""%" + txtSearchCust.Text + "%"""
            ''        qry = qry + " or tblperson.Billcontact2Tel2  like ""%" + txtSearchCust.Text + "%"""
            ''        qry = qry + " or tblperson.Billcontact2Mobile  like ""%" + txtSearchCust.Text + "%"""
            ''        qry = qry + " or tblperson.BillingName  like ""%" + txtSearchCust.Text + "%"""
            ''        qry = qry + " or tblperson.locategrp  like ""%" + txtSearchCust.Text + "%"""

            ''        'qry = qry + " or tblperson.accountid in (select accountid from tblpersonlocation where rcno<>0"

            ''        qry = qry + " and (tblpersonlocation.address1 like ""%" + txtSearchCust.Text + "%"""
            ''        qry = qry + " or tblpersonlocation.servicename like ""%" + txtSearchCust.Text + "%"""

            ''        qry = qry + " or tblpersonlocation.addbuilding like ""%" + txtSearchCust.Text + "%"""
            ''        qry = qry + " or tblpersonlocation.addstreet like ""%" + txtSearchCust.Text + "%"""
            ''        qry = qry + " or tblpersonlocation.addpostal like """ + txtSearchCust.Text + "%""))"

            ''    End If
            ''End If



            ' ''''''''''''''''''''''''''''



            'qry = qry + " order by tblperson.rcno desc,tblperson.name;"
            'txt.Text = qry
            'MakeMeNull()
            'SqlDataSource1.SelectCommand = qry
            'SqlDataSource1.DataBind()
            'GridView1.AllowPaging = False
            'GridView1.DataBind()


            ''lblMessage.Text = "SEARCH CRITERIA : " + txtSearchCust.Text + " <br/>NUMBER OF RECORDS FOUND : " + GridView1.Rows.Count.ToString

            ''txtSearchCust.Text = "Search Here"

            'GridView1.AllowPaging = True
            'tb1.ActiveTabIndex = 0
            'txtSearch.Text = txtSearchCust.Text

            ' '''''''''''

            'If GridView1.Rows.Count > 0 Then
            '    txtMode.Text = "View"

            '    txtRcno.Text = GridView1.Rows(0).Cells(4).Text

            '    PopulateRecord()


            'Else
            '    MakeMeNull()
            '    'MakeMeNullBillingDetails()
            'End If

            'btnGoSvc_Click(sender, e)

            lblMessage.Text = "SEARCH CRITERIA : " + txtSearchCust.Text + " <br/>NUMBER OF RECORDS FOUND : " + GridView1.Rows.Count.ToString
            ''''''''''''''''''''''
        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "txtSearchCust_TextChanged", ex.Message.ToString, txtAccountID.Text)
        End Try
    End Sub

    Protected Sub UploadFile(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpload.Click
        Try
            lblMessage.Text = ""
            lblAlert.Text = ""
            If String.IsNullOrEmpty(txtAccountID.Text) Then
                lblAlert.Text = "SELECT ACCOUNT TO UPLOAD FILE"
                Exit Sub

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
                            conn.Dispose()

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
                            command2.Dispose()
                        End If
                        command1.Dispose()
                        dt.Dispose()
                        dr.Close()
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
                        command.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))

                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                        command.Parameters.AddWithValue("@FileNameLink", txtAccountID.Text + "_" + fileName.ToUpper)

                    ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                        command.Parameters.AddWithValue("@FileGroup", "CUSTOMER")
                        command.Parameters.AddWithValue("@FileRef", txtAccountID.Text)
                        command.Parameters.AddWithValue("@FileName", fileName)
                        command.Parameters.AddWithValue("@FileDescription", txtFileDescription.Text)
                        command.Parameters.AddWithValue("@FileType", ext.ToUpper)
                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                        command.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                        command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
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
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "UploadFile", ex.Message.ToString, txtAccountID.Text)
        End Try
    End Sub


    Protected Sub DownloadFile(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim filePath As String = CType(sender, LinkButton).CommandArgument
            filePath = Server.MapPath("~/Uploads/Customer/") + filePath
            Response.ContentType = ContentType
            Response.AppendHeader("Content-Disposition", ("attachment; filename=" + Path.GetFileName(filePath)))
            Response.WriteFile(filePath)
            Response.End()
        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "DownloadFile", ex.Message.ToString, txtAccountID.Text)
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
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "DeleteFile", ex.Message.ToString, txtAccountID.Text)
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
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "CheckforSalesmanLocation", ex.Message.ToString, "")
        End Try
    End Function

    Protected Sub btnSvcContract_Click(sender As Object, e As EventArgs) Handles btnSvcContract.Click
        Try
            lblMessage.Text = ""
            If txtRcno.Text = "" Then
                '  MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
                lblAlert.Text = "SELECT RECORD TO OPEN CONTRACT"
                Exit Sub
            End If

            If ddlPersonGrpD.SelectedIndex = 0 Then
                '  MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
                lblAlert.Text = "ENTER PERSON GROUP TO OPEN CONTRACT"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                Exit Sub
            End If

            If ddlIndustrysvc.SelectedIndex = 0 Then
                '  MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
                lblAlert.Text = "SELECT INDUSTRY TO OPEN CONTRACT"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                Exit Sub
            End If

            If CheckforSalesmanLocation() = False Then
                lblAlert.Text = "PLEASE SELECT ACTIVE SALESMAN"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                Exit Sub
            End If

            Session("companygroup") = ddlPersonGrpD.Text
            'Session("companygroup") = ddlCompanyGrp.Text

            Session("contractfrom") = "clients"
            Session("contracttype") = "PERSON"

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

            'Session("sevaddress") = txtAddress.Text
            Session("locategrp") = ddlLocateGrp.Text

            If ddlSalesManSvc.SelectedIndex = 0 Then
                Session("salesman") = ddlSalesMan.Text
            Else
                Session("salesman") = ddlSalesManSvc.Text
            End If
            'Session("salesman") = ddlSalesMan.Text

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

            'If (ddlBillCountry.Text.Trim) = "-1" Then
            '    Session("billcity") = ""
            'Else
            '    Session("billcity") = ddlBillCountry.Text
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
            'Session("contractgroup") = ddlContractGrp.Text

            Session("gridsqlPerson") = txt.Text
            Session("rcno") = txtRcno.Text

            Session("gridsqlPersonDetail") = txtDetail.Text
            Session("rcnoDetail") = txtSvcRcno.Text

            'If (ddlContractGrp.Text.Trim) = "-1" Then
            '    Session("contractgroup") = ""
            'Else
            '    Session("contractgroup") = ddlContractGrp.Text
            'End If
            Session("industry") = ddlIndustrysvc.Text
            Session("marketsegmentidsvc") = txtMarketSegmentIDsvc.Text
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

            Session("gridview1personPI") = GridView1.PageIndex
            Session("gridview1personRI") = GridView1.SelectedIndex

            'Session("location") = ddlLocation.Text
            Response.Redirect("contract.aspx", False)
        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "btnSvcContract_Click", ex.Message.ToString, txtAccountID.Text)
        End Try
    End Sub

    Protected Sub btnSvcService_Click(sender As Object, e As EventArgs) Handles btnSvcService.Click
        lblAlert.Text = ""
        If String.IsNullOrEmpty(txtLocationID.Text) = True Then
            lblAlert.Text = "Please Select a Service Location then press SERVICE button to Proceed"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            Exit Sub
        End If

        Try
            Session("servicefrom") = "contactP"

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
                Session("ContactType") = "RESIDENTIAL"
                Session("CompanyGroup") = ddlCompanyGrp.Text
                '    Session("ContractGroup") = ddlContractGrp.Text

                Session("Scheduler") = Session("StaffID")

                Session("gridsqlPersonDetail") = txtDetail.Text
                Session("rcnoDetail") = txtSvcRcno.Text
                Session("inactive") = chkInactive.Checked
                Session("location") = ddlLocation.Text
                'Session("Team") = txtTeam.Text
                'Session("InCharge") = txtTeamIncharge.Text
                'Session("ServiceBy") = txtServiceBy.Text
                'Session("ScheduleType") = ddlScheduleType.Text

                Session("gridview1personPI") = GridView1.PageIndex
                Session("gridview1personRI") = GridView1.SelectedIndex
                '''''''''''''''''''''''''''''
            End If

            Response.Redirect("Service.aspx")
        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "btnSvcService_Click", ex.Message.ToString, txtAccountID.Text)
        End Try
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

    Protected Sub ddlOffCity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlOffCity.SelectedIndexChanged
        If ddlOffCity.SelectedIndex > 0 Then
            FindCountry("office")
        End If
    End Sub

    Protected Sub ddlBillCity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBillCity.SelectedIndexChanged
        If ddlBillCity.SelectedIndex > 0 Then
            FindCountry("billing")
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
                sqlstr = "Select Name from tblperson where Name = """ & txtNameE.Text.Trim.ToUpper & """ and location = '" & ddlLocation.Text.Trim & "'"
            Else
                sqlstr = "Select Name from tblperson where Name = """ & txtNameE.Text.Trim.ToUpper & """"
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

            command1.CommandText = "SELECT AccountID,Name,concat(Address1,AddStreet,AddBuilding) as Address FROM tblPERSON where Name like '%" & CustName & "%'"
            command1.Parameters.AddWithValue("@name", CustName)
            command1.Connection = conn

            Dim dr1 As MySqlDataReader = command1.ExecuteReader()
            Dim dt1 As New System.Data.DataTable
            dt1.Load(dr1)

            If dt1.Rows.Count > 0 Then
                For i As Int32 = 0 To dt1.Rows.Count - 1

                    ' InsertIntoTblWebEventLog("CustName", "txtNameE_TextChanged", CustName, IgnoredWords(conn, dt1.Rows(i)("Name").ToString.ToUpper))
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
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "txtNameE_TextChanged", ex.Message.ToString, txtNameE.Text)
        End Try
    End Sub


    Protected Sub btnGoCust_Click(sender As Object, e As EventArgs) Handles btnGoCust.Click

        Dim qry As String

        'If txtDisplayRecordsLocationwise.Text = "N" Then
        qry = "select  tblperson.Rcno, tblperson.AccountId, tblperson.InActive, tblperson.ID, tblperson.Name, tblperson.ARCurrency, tblperson.Location, CustomerBal.Bal, tblperson.TelMobile, tblperson.TelFax, tblperson.Address1, tblperson.AddPOstal, tblperson.BillAddress1, tblperson.BillPostal, tblperson.NRIC, tblperson.ICType, tblperson.Nationality, tblperson.Sex, tblperson.LocateGrp, tblperson.PersonGroup, tblperson.AccountNo, tblperson.Salesman, tblperson.AddStreet, tblperson.AddBuilding, tblperson.AddCity, tblperson.AddState, tblperson.AddCountry, tblperson.BillStreet, tblperson.BillBuilding, tblperson.BillCity, tblperson.BillState, tblperson.BillCountry,  tblperson.CreatedBy, tblperson.CreatedOn, tblperson.LastModifiedBy, tblperson.LastModifiedOn,tblperson.AutoEmailInvoice,tblperson.AutoEmailSOA,tblperson.UnsubscribeAutoEmailDate from tblperson left join CustomerBal  on tblperson.Accountid = CustomerBal.Accountid where 1=1 "

        If txtSearchCust.Text <> "Search Here" Then
            qry = qry + " and (tblperson.accountid like ""%" & txtSearchCust.Text.Trim & "%"""
            qry = qry + " or tblperson.id like ""%" + txtSearchCust.Text.Trim + "%"""

            qry = qry + " or tblperson.address1 like ""%" + txtSearchCust.Text.Trim + "%"""
            qry = qry + " or tblperson.addbuilding like ""%" + txtSearchCust.Text.Trim + "%"""
            qry = qry + " or tblperson.addstreet like ""%" + txtSearchCust.Text.Trim + "%"""
            qry = qry + " or tblperson.billaddress1 like ""%" + txtSearchCust.Text.Trim + "%"""
            qry = qry + " or tblperson.billbuilding like ""%" + txtSearchCust.Text.Trim + "%"""
            qry = qry + " or tblperson.billstreet like ""%" + txtSearchCust.Text.Trim + "%"""
            qry = qry + " or tblperson.addpostal like """ + txtSearchCust.Text.Trim + "%"""
            qry = qry + " or tblperson.billpostal like """ + txtSearchCust.Text.Trim + "%"""
            qry = qry + " or tblperson.name like ""%" + txtSearchCust.Text.Trim + "%"""
            'qry = qry + " or tblperson.persongroup like ""%" + txtSearchCust.Text + "%"""
            qry = qry + " or tblperson.salesman  like ""%" + txtSearchCust.Text.Trim + "%"""
            qry = qry + " or tblperson.telmobile  like ""%" + txtSearchCust.Text.Trim + "%"" or tblperson.telhome like ""%" + txtSearchCust.Text + "%"""
            qry = qry + " or tblperson.telpager  like ""%" + txtSearchCust.Text.Trim + "%"""
            qry = qry + " or tblperson.rescp2Tel  like ""%" + txtSearchCust.Text.Trim + "%"""
            qry = qry + " or tblperson.rescp2Tel2  like ""%" + txtSearchCust.Text.Trim + "%"""
            qry = qry + " or tblperson.rescp2Mobile  like ""%" + txtSearchCust.Text.Trim + "%"""
            qry = qry + " or tblperson.BillTelhome  like ""%" + txtSearchCust.Text.Trim + "%"""
            qry = qry + " or tblperson.BillTelpager  like ""%" + txtSearchCust.Text.Trim + "%"""
            qry = qry + " or tblperson.BilltelMobile  like ""%" + txtSearchCust.Text.Trim + "%"""
            qry = qry + " or tblperson.Billcontact2Tel  like ""%" + txtSearchCust.Text.Trim + "%"""
            qry = qry + " or tblperson.Billcontact2Tel2  like ""%" + txtSearchCust.Text.Trim + "%"""
            qry = qry + " or tblperson.Billcontact2Mobile  like ""%" + txtSearchCust.Text.Trim + "%"""
            qry = qry + " or tblperson.BillingName  like ""%" + txtSearchCust.Text.Trim + "%"""
            qry = qry + " or tblperson.locategrp  like ""%" + txtSearchCust.Text.Trim + "%"""
            qry = qry + " or tblperson.Name2 like ""%" + txtSearchCust.Text.Trim + "%"""
            qry = qry + " or tblperson.accountid in (select accountid from tblpersonlocation where rcno<>0"

            qry = qry + " and (tblpersonlocation.address1 like ""%" + txtSearchCust.Text.Trim + "%"""
            qry = qry + " or tblpersonlocation.servicename like ""%" + txtSearchCust.Text.Trim + "%"""

            qry = qry + " or tblpersonlocation.addbuilding like ""%" + txtSearchCust.Text.Trim + "%"""
            qry = qry + " or tblpersonlocation.addstreet like ""%" + txtSearchCust.Text.Trim + "%"""
            qry = qry + " or tblpersonlocation.addpostal like """ + txtSearchCust.Text.Trim + "%"")))"

        End If
        'End If

        'If txtDisplayRecordsLocationwise.Text = "Y" Then
        '    'qry = qry + " and Location = '" & txtLocation.Text.Trim & "'"
        '    qry = qry & " and location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "')"

        'End If


        If txtDisplayRecordsLocationwise.Text = "Y" Then
            qry = " SELECT distinct  tblperson.Rcno, tblperson.AccountId, tblperson.InActive, tblperson.ID, tblperson.Name, tblperson.ARCurrency, tblperson.Location, PersonBal.Bal, tblperson.TelMobile, tblperson.TelFax, tblperson.Address1, tblperson.AddPOstal, tblperson.BillAddress1, tblperson.BillPostal, tblperson.NRIC, tblperson.ICType, tblperson.Nationality, tblperson.Sex, tblperson.LocateGrp, tblperson.PersonGroup, tblperson.AccountNo, tblperson.Salesman, tblperson.AddStreet, tblperson.AddBuilding, tblperson.AddCity, tblperson.AddState, tblperson.AddCountry, tblperson.BillStreet, tblperson.BillBuilding, tblperson.BillCity, tblperson.BillState, tblperson.BillCountry,  tblperson.CreatedBy, tblperson.CreatedOn, tblperson.LastModifiedBy, tblperson.LastModifiedOn,tblperson.AutoEmailInvoice,tblperson.AutoEmailSOA,tblperson.UnsubscribeAutoEmailDate FROM tblperson  left join Personbal  on tblperson.Accountid = Personbal.Accountid left join tblPersonLocation  on tblPerson.Accountid = tblPersonLocation.Accountid where tblPerson.Inactive=0 "
            qry = qry + " and tblPerson.location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "')"


            If txtSearchCust.Text <> "Search Here" Then
                qry = qry + " and (tblperson.accountid like ""%" & txtSearchCust.Text.Trim & "%"""
                qry = qry + " or tblperson.id like ""%" + txtSearchCust.Text.Trim + "%"""

                qry = qry + " or tblperson.address1 like ""%" + txtSearchCust.Text.Trim + "%"""
                qry = qry + " or tblperson.addbuilding like ""%" + txtSearchCust.Text.Trim + "%"""
                qry = qry + " or tblperson.addstreet like ""%" + txtSearchCust.Text.Trim + "%"""
                qry = qry + " or tblperson.billaddress1 like ""%" + txtSearchCust.Text.Trim + "%"""
                qry = qry + " or tblperson.billbuilding like ""%" + txtSearchCust.Text + "%"""
                qry = qry + " or tblperson.billstreet like ""%" + txtSearchCust.Text + "%"""
                qry = qry + " or tblperson.addpostal like """ + txtSearchCust.Text.Trim + "%"""
                qry = qry + " or tblperson.billpostal like """ + txtSearchCust.Text.Trim + "%"""
                qry = qry + " or tblperson.name like ""%" + txtSearchCust.Text.Trim + "%"""
                'qry = qry + " or tblperson.persongroup like ""%" + txtSearchCust.Text + "%"""
                qry = qry + " or tblperson.salesman  like ""%" + txtSearchCust.Text.Trim + "%"""
                qry = qry + " or tblperson.telmobile  like ""%" + txtSearchCust.Text.Trim + "%"" or tblperson.telhome like ""%" + txtSearchCust.Text.Trim + "%"""
                qry = qry + " or tblperson.telpager  like ""%" + txtSearchCust.Text.Trim + "%"""
                qry = qry + " or tblperson.rescp2Tel  like ""%" + txtSearchCust.Text.Trim + "%"""
                qry = qry + " or tblperson.rescp2Tel2  like ""%" + txtSearchCust.Text.Trim + "%"""
                qry = qry + " or tblperson.rescp2Mobile  like ""%" + txtSearchCust.Text.Trim + "%"""
                qry = qry + " or tblperson.BillTelhome  like ""%" + txtSearchCust.Text.Trim + "%"""
                qry = qry + " or tblperson.BillTelpager  like ""%" + txtSearchCust.Text.Trim + "%"""
                qry = qry + " or tblperson.BilltelMobile  like ""%" + txtSearchCust.Text.Trim + "%"""
                qry = qry + " or tblperson.Billcontact2Tel  like ""%" + txtSearchCust.Text.Trim + "%"""
                qry = qry + " or tblperson.Billcontact2Tel2  like ""%" + txtSearchCust.Text.Trim + "%"""
                qry = qry + " or tblperson.Billcontact2Mobile  like ""%" + txtSearchCust.Text.Trim + "%"""
                qry = qry + " or tblperson.BillingName  like ""%" + txtSearchCust.Text.Trim + "%"""
                qry = qry + " or tblperson.locategrp  like ""%" + txtSearchCust.Text.Trim + "%"""

                'qry = qry + " or tblperson.accountid in (select accountid from tblpersonlocation where rcno<>0"

                qry = qry + " and (tblpersonlocation.address1 like ""%" + txtSearchCust.Text.Trim + "%"""
                qry = qry + " or tblpersonlocation.servicename like ""%" + txtSearchCust.Text.Trim + "%"""

                qry = qry + " or tblpersonlocation.addbuilding like ""%" + txtSearchCust.Text.Trim + "%"""
                qry = qry + " or tblpersonlocation.addstreet like ""%" + txtSearchCust.Text.Trim + "%"""
                qry = qry + " or tblpersonlocation.addpostal like """ + txtSearchCust.Text.Trim + "%""))"

            End If
        End If
        qry = qry + " order by tblperson.rcno desc,tblperson.name;"
        txt.Text = qry
        MakeMeNull()
        SqlDataSource1.SelectCommand = qry
        SqlDataSource1.DataBind()
        GridView1.AllowPaging = False
        GridView1.DataBind()


        lblMessage.Text = "SEARCH CRITERIA : " + txtSearchCust.Text + " <br/>NUMBER OF RECORDS FOUND : " + GridView1.Rows.Count.ToString

        'txtSearchCust.Text = "Search Here"

        GridView1.AllowPaging = True
        tb1.ActiveTabIndex = 0
        txtSearch.Text = txtSearchCust.Text


        '''''''''''

        If GridView1.Rows.Count > 0 Then
            txtMode.Text = "View"

            txtRcno.Text = GridView1.Rows(0).Cells(4).Text

            PopulateRecord()
            RetrieveAutoEmailInfo()

        Else
            MakeMeNull()
            'MakeMeNullBillingDetails()
        End If

        btnGoSvc_Click(sender, e)
        ''''''''''''''''''''''


        'Dim qry As String
        'qry = "select * from tblperson where 1=1 "

        'If txtSearchCust.Text <> "Search Here" Then
        '    If String.IsNullOrEmpty(txtSearchCust.Text) = False Then
        '        qry = qry + " and (tblperson.accountid like '" & txtSearchCust.Text & "%'"
        '        qry = qry + " or tblperson.address1 like '%" + txtSearchCust.Text + "%'"
        '        qry = qry + " or tblperson.addbuilding like '%" + txtSearchCust.Text + "%'"
        '        qry = qry + " or tblperson.addstreet like '%" + txtSearchCust.Text + "%'"
        '        qry = qry + " or tblperson.billaddress1 like '%" + txtSearchCust.Text + "%'"
        '        qry = qry + " or tblperson.billbuilding like '%" + txtSearchCust.Text + "%'"
        '        qry = qry + " or tblperson.billstreet like '%" + txtSearchCust.Text + "%'"
        '        qry = qry + " or tblperson.addpostal like '" + txtSearchCust.Text + "%'"
        '        qry = qry + " or tblperson.billpostal like '" + txtSearchCust.Text + "%'"
        '        qry = qry + " or tblperson.name like '%" + txtSearchCust.Text + "%'"
        '        qry = qry + " or tblperson.persongroup like '%" + txtSearchCust.Text + "%'"
        '        qry = qry + " or tblperson.salesman  like '%" + txtSearchCust.Text + "%'"
        '        qry = qry + " or tblperson.telmobile  like '%" + txtSearchCust.Text + "%' or tblperson.telhome like '%" + txtSearchCust.Text + "%'"
        '        qry = qry + " or tblperson.telpager  like '%" + txtSearchCust.Text + "%'"
        '        qry = qry + " or tblperson.rescp2Tel  like '%" + txtSearchCust.Text + "%'"
        '        qry = qry + " or tblperson.rescp2Tel2  like '%" + txtSearchCust.Text + "%'"
        '        qry = qry + " or tblperson.rescp2Mobile  like '%" + txtSearchCust.Text + "%'"
        '        qry = qry + " or tblperson.BillTelhome  like '%" + txtSearchCust.Text + "%'"
        '        qry = qry + " or tblperson.BillTelpager  like '%" + txtSearchCust.Text + "%'"
        '        qry = qry + " or tblperson.BilltelMobile  like '%" + txtSearchCust.Text + "%'"
        '        qry = qry + " or tblperson.Billcontact2Tel  like '%" + txtSearchCust.Text + "%'"
        '        qry = qry + " or tblperson.Billcontact2Tel2  like '%" + txtSearchCust.Text + "%'"
        '        qry = qry + " or tblperson.Billcontact2Mobile  like '%" + txtSearchCust.Text + "%'"
        '        qry = qry + " or tblperson.BillingName  like '%" + txtSearchCust.Text + "%'"
        '        qry = qry + " or tblperson.locategrp  like '%" + txtSearchCust.Text + "%'"

        '        qry = qry + " or tblperson.accountid in (select accountid from tblpersonlocation where rcno<>0"

        '        qry = qry + " and (tblpersonlocation.address1 like '%" + txtSearchCust.Text + "%'"
        '        qry = qry + " or tblpersonlocation.addbuilding like '%" + txtSearchCust.Text + "%'"
        '        qry = qry + " or tblpersonlocation.addstreet like '%" + txtSearchCust.Text + "%'"
        '        qry = qry + " or tblpersonlocation.addpostal like '" + txtSearchCust.Text + "%')))"

        '    End If
        '    qry = qry + " order by tblperson.rcno desc,tblperson.name;"
        '    txt.Text = qry
        '    MakeMeNull()
        '    SqlDataSource1.SelectCommand = qry
        '    SqlDataSource1.DataBind()
        '    GridView1.AllowPaging = False
        '    GridView1.DataBind()


        '    lblMessage.Text = "SEARCH CRITERIA : " + txtSearchCust.Text + " <br/>NUMBER OF RECORDS FOUND : " + GridView1.Rows.Count.ToString
        '    GridView1.AllowPaging = True
        '    tb1.ActiveTabIndex = 0
        'Else
        '    Exit Sub
        'End If

        'txtSearchCust.Text = "Search Here"


    End Sub



    Protected Sub btnGoSvc_Click(sender As Object, e As EventArgs) Handles btnGoSvc.Click
        txtSearchText.Text = txtSearch.Text

        If txtSearch.Text <> "Search Here for Location Address, Postal Code or Description" Then
            Dim qry As String
            qry = "SELECT * FROM tblpersonlocation where accountid='" & txtAccountIDtab2.Text & "'"

            If String.IsNullOrEmpty(txtSearch.Text) = False Then
                qry = qry + " and (locationid like ""%" & txtSearch.Text & "%"""
                ' or address1='" & txtSearch.Text & "' or addstreet='" & txtSearch.Text & "' or addbuilding='" & txtSearch.Text & "'or addpostal='" & txtSearch.Text & "';"
                qry = qry + " or personid like ""%" + txtSearch.Text + "%"""
                qry = qry + " or servicename like ""%" + txtSearch.Text + "%"""
                qry = qry + " or billingnamesvc like ""%" + txtSearch.Text + "%"""
                qry = qry + " or ContractGroup like ""%" + txtSearch.Text + "%"""
                qry = qry + " or ContactPerson like ""%" + txtSearch.Text + "%"""
                qry = qry + " or BillContact1Svc like ""%" + txtSearch.Text + "%"""
                qry = qry + " or comments like ""%" + txtSearch.Text + "%"""
                qry = qry + " or description like ""%" + txtSearch.Text + "%"""
                qry = qry + " or address1 like ""%" + txtSearch.Text + "%"""
                qry = qry + " or addbuilding like ""%" + txtSearch.Text + "%"""
                qry = qry + " or addstreet like ""%" + txtSearch.Text + "%"""
                qry = qry + " or addpostal like """ + txtSearch.Text + "%"")"
            End If

            MakeSvcNull()
            SqlDataSource2.SelectCommand = qry
            SqlDataSource2.DataBind()
            GridView2.DataBind()
            lblMessage.Text = "SEARCH CRITERIA : " + txtSearch.Text + " <br/>NUMBER OF RECORDS FOUND : " + GridView2.Rows.Count.ToString
            txtDetail.Text = qry
        Else

            txtSearch.Text = "Search Here for Location Address, Postal Code or Description"

        End If

    End Sub

    Protected Sub PreviewFile(ByVal sender As Object, ByVal e As EventArgs)
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
        Try
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
            Catch ex As Exception
                InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "grdViewInvoiceDetails_PageIndexChanging", ex.Message.ToString, txtAccountID.Text)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            End Try
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

        Session.Add("customerfrom", "Residential")


        'Session("contractfrom") = "clients"
        Session("contracttype") = "PERSON"
        Session("AccountTypeSOA") = "PERSON"
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

        'Session("billcity") = ddlBillCity.Text
        Session("billpostal") = txtBillPostal.Text

        'Session("industry") = ddlIndustry.Text

        'If String.IsNullOrEmpty(Session("contractfrom")) = False Then
        '    Session("clientid") = txtID.Text

        'End If

        Session("gridsqlCompany") = txt.Text
        Session("rcno") = txtRcno.Text

        Session("gridsqlCompanyDetail") = txtDetail.Text
        Session("rcnoDetail") = txtSvcRcno.Text

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
        'UpdateTransactions()
        lblTotal.Text = ""

        lblCurDate.Text = "As at " + Convert.ToString(Session("SysDate"))
        ModalPopupInvoice.Show()

    End Sub

    Protected Sub grdViewInvoiceDetails_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdViewInvoiceDetails.PageIndexChanging
        Try
            grdViewInvoiceDetails.PageIndex = e.NewPageIndex
            'SqlDSInvoiceDetails.SelectCommand = "SELECT salesdate as VoucherDate,'INVOICE' as Type,invoicenumber as VoucherNumber,AppliedBase as Amount FROM tblsales WHERE poststatus='P' and doctype='ARIN' AND (AccountId = '" + txtAccountIDSelected.Text + "') union select RECEIPTDATE as VoucherDate,'RECEIPTS' as Type,receiptnumber as VoucherNumber,BaseAmount as Amount from tblrecv WHERE poststatus='P' AND (AccountId = '" + txtAccountIDSelected.Text + "') union SELECT salesdate as VoucherDate,'CN' as Type,invoicenumber as VoucherNumber,AppliedBase as Amount FROM tblsales WHERE poststatus='P' and doctype='ARCN' AND (AccountId = '" + txtAccountIDSelected.Text + "') union SELECT salesdate as VoucherDate,'DN' as Type,invoicenumber as VoucherNumber,AppliedBase as Amount FROM tblsales WHERE poststatus='P' and doctype='ARDN' AND (AccountId = '" + txtAccountIDSelected.Text + "') ORDER BY VoucherDate"
            'SqlDSInvoiceDetails.DataBind()
            'grdViewInvoiceDetails.DataBind()

            UpdateTransactions()

            Session.Add("customerfrom", "Residential")
            ModalPopupInvoice.Show()
        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "grdViewInvoiceDetails_PageIndexChanging", ex.Message.ToString, txtAccountID.Text)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
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
                e.Row.Cells(8).Visible = False
                GridView1.HeaderRow.Cells(8).Visible = False
            Else
                e.Row.Cells(8).Visible = True
                GridView1.HeaderRow.Cells(8).Visible = True
            End If
        End If
    End Sub


    Protected Sub OnSelectedIndexChanged(sender As Object, e As EventArgs)
        For Each row As GridViewRow In GridView1.Rows
            'If row.RowIndex = GridView1.SelectedIndex Then
            '    row.BackColor = ColorTranslator.FromHtml("#738A9C")
            '    row.ToolTip = String.Empty
            'Else
            '    row.BackColor = ColorTranslator.FromHtml("#E4E4E4")
            '    row.ToolTip = "Click to select this row."
            'End If

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

    Protected Sub OnSelectedIndexChanged2(sender As Object, e As EventArgs)

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
        'For Each row As GridViewRow In GridView2.Rows
        '    'If row.RowIndex = GridView2.SelectedIndex Then
        '    '    row.BackColor = ColorTranslator.FromHtml("#738A9C")
        '    '    row.ToolTip = String.Empty
        '    'Else
        '    '    row.BackColor = ColorTranslator.FromHtml("#E4E4E4")
        '    '    row.ToolTip = "Click to select this row."
        '    'End If

        '    If String.IsNullOrEmpty(txtSelectedIndex.Text) = True Then
        '        'If row.RowIndex = GridView2.SelectedIndex Then
        '        '    row.BackColor = ColorTranslator.FromHtml("#738A9C")
        '        '    row.ToolTip = String.Empty
        '        'Else
        '        '    row.BackColor = ColorTranslator.FromHtml("#E4E4E4")
        '        '    row.ToolTip = "Click to select this row."
        '        'End If

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
        '            'If row.RowIndex = txtSelectedIndex.Text Then
        '            '    row.BackColor = ColorTranslator.FromHtml("#738A9C")
        '            '    row.ToolTip = String.Empty
        '            '    txtSelectedIndex.Text = ""
        '            'Else
        '            '    row.BackColor = ColorTranslator.FromHtml("#E4E4E4")
        '            '    row.ToolTip = "Click to select this row."
        '            'End If

        '            If row.RowIndex = txtSelectedIndex.Text Then
        '                row.BackColor = ColorTranslator.FromHtml("#00ccff")
        '                row.ToolTip = String.Empty
        '            Else
        '                If row.RowIndex Mod 2 = 0 Then
        '                    row.BackColor = ColorTranslator.FromHtml("#EFF3FB")
        '                    row.ToolTip = "Click to select this row."
        '                Else
        '                    row.BackColor = ColorTranslator.FromHtml("#ffffff")
        '                    row.ToolTip = "Click to select this row."
        '                End If
        '            End If
        '        End If

        '    End If

        'Next
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

                SqlDSCP.SelectCommand = "SELECT * FROM tblPersonCustomerAccess where AccountID = '" & txtAccountID.Text & "'"
                gvCP.DataSourceID = "SqlDSCP"
                gvCP.DataBind()

            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "tb1_ActiveTabChanged", ex.Message.ToString, txtAccountID.Text)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Protected Sub btnUpdateBilling_Click(sender As Object, e As EventArgs) Handles btnUpdateBilling.Click
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

    End Sub

    Protected Sub btnEditBillingSave_Click(sender As Object, e As EventArgs) Handles btnEditBillingSave.Click
        Try
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
            '    Try
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()


            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text
            command1.CommandText = "SELECT * FROM tblperson where rcno=" & Convert.ToInt32(txtRcno.Text)
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New System.Data.DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then

                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                Dim qry As String = "UPDATE tblperson SET BillBuilding = @BillBuilding,BillStreet = @BillStreet,BillPostal = @BillPostal,BillState = @BillState,BillCity = @BillCity,BillCountry = @BillCountry,"
                qry = qry + "BillAddress1 = @BillAddress1,LastModifiedBy = @LastModifiedBy,LastModifiedOn = @LastModifiedOn,BillTelHome = @BillTelephone,BillTelFax = @BillFax,"
                '  qry = qry + "BillAddress1 = @BillAddress1,BillTelephone = @BillTelephone,BillFax = @BillFax,"

                qry = qry + "BillContactPerson = @BillContactPerson,BillTelPager = @BillTelephone2,BillTelMobile = @BillMobile,BillContact1Position=@BillContact1Position,BillContact2=@BillContact2,"
                qry = qry + "BillContact2Position=@BillContact2Position,BillEmail=@BillContact1Email,BillContact2Email=@BillContact2Email,BillContact2Tel=@BillContact2Tel,BillContact2Fax=@BillContact2Fax,"
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
                    command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
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
                    command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))

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
                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "RESI", txtAccountID.Text, "EDITBILLING", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtAccountID.Text, "", txtRcno.Text)

            End If

            conn.Close()
            conn.Dispose()
            command1.Dispose()
            dt.Dispose()
            dr.Close()
            'Catch ex As Exception

            '    MessageBox.Message.Alert(Page, "Error!!! " + ex.ToString, "str")
            'End Try
            '   EnableControls()


            'txt.Text = "SELECT * FROM tblcompany WHERE  Inactive=0 order by rcno desc limit 100;"
            'SqlDataSource1.SelectCommand = "SELECT * FROM tblcompany WHERE  Inactive=0 order by rcno desc limit 100;"
            SqlDataSource1.SelectCommand = txt.Text
            SqlDataSource1.DataBind()
            GridView1.DataBind()
        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "btnEditBillingSave_Click", ex.Message.ToString, txtAccountID.Text)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Private Sub UpdateSvcLocation(conn As MySqlConnection)
        Try
            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.CommandText = "SELECT * FROM tblpersonlocation where accountid='" & txtAccountID.Text & "'"
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New System.Data.DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then
                For i As Int16 = 0 To dt.Rows.Count - 1

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "UPDATE tblpersonlocation SET BillingNameSvc = @BillingNameSvc,BillAddressSvc = @BillAddressSvc,BillStreetSvc = @BillStreetSvc,BillBuildingSvc = @BillBuildingSvc,BillCitySvc = @BillCitySvc,BillStateSvc = @BillStateSvc,BillCountrySvc = @BillCountrySvc,BillPostalSvc = @BillPostalSvc,BillContact1Svc = @BillContact1Svc,BillPosition1Svc = @BillPosition1Svc,BillTelephone1Svc = @BillTelephone1Svc,BillFax1Svc = @BillFax1Svc,Billtelephone12Svc = @Billtelephone12Svc,BillMobile1Svc = @BillMobile1Svc,BillEmail1Svc = @BillEmail1Svc,BillContact2Svc = @BillContact2Svc,BillPosition2Svc = @BillPosition2Svc,BillTelephone2Svc = @BillTelephone2Svc,BillFax2Svc = @BillFax2Svc,Billtelephone22Svc = @Billtelephone22Svc,BillMobile2Svc = @BillMobile2Svc,BillEmail2Svc = @BillEmail2Svc where accountid='" & dt.Rows(i)("Accountid").ToString & "'"
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
                    command.Dispose()

                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "RESISVCLOC", txtAccountID.Text, "EDITBILLING", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtLocationID.Text, "", dt.Rows(i)("Rcno"))

                Next

                SqlDataSource2.SelectCommand = "SELECT * FROM tblPERSONlocation where accountid = '" & txtAccountID.Text & "'"
                SqlDataSource2.DataBind()
                GridView2.DataBind()
            End If
            command1.Dispose()
            dt.Dispose()
            dr.Close()
        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "UpdateSvcLocation", ex.Message.ToString, txtAccountID.Text)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Protected Sub btnChangeStatus_Click(sender As Object, e As EventArgs) Handles btnChangeStatus.Click
        Try
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
        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "btnChangeStatus_Click", ex.Message.ToString, txtAccountID.Text)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

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

                command.CommandText = "UPDATE tblperson SET InActive= 0 where rcno=" & Convert.ToInt32(txtRcno.Text)
                chkInactive.Checked = False

            ElseIf chkInactive.Checked = False Then

                command.CommandText = "UPDATE tblperson SET InActive= 1 where rcno=" & Convert.ToInt32(txtRcno.Text)
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
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "btnConfirmYes_Click", ex.Message.ToString, txtAccountID.Text)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    'Protected Sub btnUpdateStatus_Click(sender As Object, e As EventArgs) Handles btnUpdateStatus.Click
    '    If ddlNewStatus.Text = txtDDLText.Text Then
    '        lblAlertStatus.Text = "SELECT NEW STATUS"
    '        mdlPopupStatus.Show()

    '        Return

    '    End If
    '    lblMessageStatus.Text = lblOldStatus.Text + " " + ddlNewStatus.Text

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
    '            command.CommandText = "UPDATE tblPerson SET InActive= 0 where rcno=" & Convert.ToInt32(txtRcno.Text)
    '        ElseIf ddlNewStatus.SelectedIndex = 2 Then
    '            command.CommandText = "UPDATE tblPerson SET InActive= 1 where rcno=" & Convert.ToInt32(txtRcno.Text)
    '        End If

    '        'command.CommandText = "UPDATE tblPerson SET InActive='" + ddlNewStatus.SelectedValue + "' where rcno=" & Convert.ToInt32(txtRcno.Text)
    '        command.Connection = conn
    '        command.ExecuteNonQuery()

    '        '   UpdateContractActSvcDate(conn)

    '        conn.Close()
    '        'ddlStatus.Text = ddlNewStatus.Text
    '        ddlNewStatus.SelectedIndex = 0

    '        lblMessage.Text = "ACTION: STATUS UPDATED"
    '        CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CHST", txtAccountID.Text, "CHST", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtAccountID.Text, "", txtRcno.Text)
    '        'txtPostStatus.Text = ddlNewStatus.SelectedValue
    '        If ddlNewStatus.SelectedIndex = 1 Then
    '            chkInactive.Checked = False
    '        ElseIf ddlNewStatus.SelectedIndex = 2 Then
    '            chkInactive.Checked = True
    '        End If
    '        SqlDataSource1.SelectCommand = txt.Text
    '        SqlDataSource1.DataBind()
    '        GridView1.DataBind()

    '        'GridView1.DataSourceID = "SqlDataSource1"
    '        mdlPopupStatus.Hide()
    '    Catch ex As Exception
    '        MessageBox.Message.Alert(Page, ex.ToString, "str")
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
    '    End Try
    'End Sub

    Protected Sub ddlFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFilter.SelectedIndexChanged
        Try
            Dim qry As String = ""

            If ddlFilter.SelectedValue.ToString = "ALL TRANSACTIONS" Then
                qry = "(SELECT salesdate as VoucherDate,'INVOICE' as Type,invoicenumber as VoucherNumber,AppliedBase as Amount FROM tblsales WHERE poststatus='P' and doctype='ARIN' AND (AccountId = '" + txtAccountID.Text + "')) union (select RECEIPTDATE as VoucherDate,'RECEIPTS' as Type,receiptnumber as VoucherNumber,BaseAmount as Amount from tblrecv WHERE poststatus='P' AND (AccountId = '" + txtAccountID.Text + "')) union (SELECT salesdate as VoucherDate,'CN' as Type,invoicenumber as VoucherNumber,AppliedBase as Amount FROM tblsales WHERE poststatus='P' and doctype='ARCN' AND (AccountId = '" + txtAccountID.Text + "')) union (SELECT salesdate as VoucherDate,'DN' as Type,invoicenumber as VoucherNumber,AppliedBase as Amount FROM tblsales WHERE poststatus='P' and doctype='ARDN' AND (AccountId = '" + txtAccountID.Text + "')) ORDER BY VoucherDate desc"
            ElseIf ddlFilter.SelectedValue.ToString = "SALES INVOICE" Then
                qry = "SELECT salesdate as VoucherDate,'INVOICE' as Type,invoicenumber as VoucherNumber,AppliedBase as Amount FROM tblsales WHERE poststatus='P' and doctype='ARIN' AND (AccountId = '" + txtAccountID.Text + "') ORDER BY VoucherDate desc"
            ElseIf ddlFilter.SelectedValue.ToString = "SALES INVOICE (OUTSTANDING)" Then
                qry = "SELECT salesdate as VoucherDate,'INVOICE' as Type,invoicenumber as VoucherNumber,AppliedBase as Amount FROM tblsales WHERE poststatus='P' and doctype='ARIN' AND (AccountId = '" + txtAccountID.Text + "') and balancebase<>0 ORDER BY VoucherDate desc"
            ElseIf ddlFilter.SelectedValue.ToString = "RECEIPT" Then
                qry = "select RECEIPTDATE as VoucherDate,'RECEIPTS' as Type,receiptnumber as VoucherNumber,BaseAmount as Amount from tblrecv WHERE poststatus='P' AND (AccountId = '" + txtAccountID.Text + "') ORDER BY VoucherDate desc"
            ElseIf ddlFilter.SelectedValue.ToString = "CREDIT NOTE" Then
                qry = "SELECT salesdate as VoucherDate,'CN' as Type,invoicenumber as VoucherNumber,AppliedBase as Amount FROM tblsales WHERE poststatus='P' and doctype='ARCN' AND (AccountId = '" + txtAccountID.Text + "') ORDER BY VoucherDate desc"
            ElseIf ddlFilter.SelectedValue.ToString = "DEBIT NOTE" Then
                qry = "SELECT salesdate as VoucherDate,'DN' as Type,invoicenumber as VoucherNumber,AppliedBase as Amount FROM tblsales WHERE poststatus='P' and doctype='ARDN' AND (AccountId = '" + txtAccountID.Text + "') ORDER BY VoucherDate desc"
            ElseIf ddlFilter.SelectedValue.ToString = "ADJUSTMENT" Then
                '  qry = "SELECT salesdate as VoucherDate,'INVOICE' as Type,invoicenumber as VoucherNumber,AppliedBase as Amount FROM tblsales WHERE poststatus='P' and doctype='ARIN' AND (AccountId = '" + txtAccountID.Text + "')"

            End If

            SqlDSInvoiceDetails.SelectCommand = qry
            SqlDSInvoiceDetails.DataBind()
            grdViewInvoiceDetails.DataBind()

            UpdateTransactions()
            Session.Add("customerfrom", "Residential")
            ModalPopupInvoice.Show()

        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "ddlFilter_SelectedIndexChanged", ex.Message.ToString, txtAccountID.Text)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Private Sub UpdateTransactions()
        Try
            Dim qry As String = ""
            Dim qryTotal As String = ""

            If ddlFilter.SelectedValue.ToString = "ALL TRANSACTIONS" Then
                qry = "(SELECT salesdate as VoucherDate,'INVOICE' as Type,invoicenumber as VoucherNumber,AppliedBase as Amount FROM tblsales WHERE poststatus='P' and doctype='ARIN' AND (AccountId = '" + txtAccountID.Text + "')) union (select RECEIPTDATE as VoucherDate,'RECEIPTS' as Type,receiptnumber as VoucherNumber,BaseAmount as Amount from tblrecv WHERE poststatus='P' AND (AccountId = '" + txtAccountID.Text + "')) union (SELECT salesdate as VoucherDate,'CN' as Type,invoicenumber as VoucherNumber,AppliedBase as Amount FROM tblsales WHERE poststatus='P' and doctype='ARCN' AND (AccountId = '" + txtAccountID.Text + "')) union (SELECT salesdate as VoucherDate,'DN' as Type,invoicenumber as VoucherNumber,AppliedBase as Amount FROM tblsales WHERE poststatus='P' and doctype='ARDN' AND (AccountId = '" + txtAccountID.Text + "')) union all (SELECT tbljrnv.JournalDate as VoucherDate,'JOURNAL' as Type,tbljrnvdet.VoucherNumber,-tbljrnvdet.cREDITBase as Amount from tbljrnvdet left outer join tbljrnv on tbljrnvdet.vouchernumber=tbljrnv.vouchernumber where PostStatus='P' AND (AccountId = '" + txtAccountID.Text + "')) ORDER BY VoucherDate desc"
                qryTotal = "SELECT ifnull(Sum(AppliedBase),0) as totalamountinvoice FROM tblsales WHERE poststatus='P' and doctype='ARIN' AND (AccountId = '" + txtAccountID.Text + "') union select ifnull(Sum(BaseAmount),0) as totalamountreceipt from tblrecv WHERE poststatus='P' AND (AccountId = '" + txtAccountID.Text + "') union SELECT ifnull(Sum(AppliedBase),0) as totalamountCN FROM tblsales WHERE poststatus='P' and doctype='ARCN' AND (AccountId = '" + txtAccountID.Text + "') union SELECT ifnull(Sum(AppliedBase),0) as totalamountDN FROM tblsales WHERE poststatus='P' and doctype='ARDN' AND (AccountId = '" + txtAccountID.Text + "')"
            ElseIf ddlFilter.SelectedValue.ToString = "SALES INVOICE" Then
                qry = "SELECT salesdate as VoucherDate,'INVOICE' as Type,invoicenumber as VoucherNumber,AppliedBase as Amount FROM tblsales WHERE poststatus='P' and doctype='ARIN' AND (AccountId = '" + txtAccountID.Text + "') ORDER BY VoucherDate desc"
                qryTotal = "SELECT ifnull(Sum(AppliedBase),0) as totalamount FROM tblsales WHERE poststatus='P' and doctype='ARIN' AND (AccountId = '" + txtAccountID.Text + "')"
            ElseIf ddlFilter.SelectedValue.ToString = "SALES INVOICE (OUTSTANDING)" Then
                qry = "SELECT salesdate as VoucherDate,'INVOICE' as Type,invoicenumber as VoucherNumber,BalanceBase as Amount FROM tblsales WHERE poststatus='P' and doctype='ARIN' AND (AccountId = '" + txtAccountID.Text + "') and balancebase<>0 ORDER BY VoucherDate desc"
                qryTotal = "SELECT ifnull(Sum(BalanceBase),0) as totalamount FROM tblsales WHERE poststatus='P' and doctype='ARIN' AND (AccountId = '" + txtAccountID.Text + "') and balancebase<>0"
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
            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "UpdateTransactions", ex.Message.ToString, txtAccountID.Text)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try

    End Sub
    Protected Sub grdViewInvoiceDetails_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdViewInvoiceDetails.RowDataBound
        ''    Dim total As Decimal = Sum(Function(row) row.Field(Of Decimal)("Amount"))
        ''    Dim total As Decimal = WebControls.DataControlField.grdViewInvoiceDetails.Columns("Amount").SummaryItem.SummaryValue
        'If e.Row.RowType = DataControlRowType.DataRow Then
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
        Try
            gvNotesMaster.PageIndex = e.NewPageIndex

            SqlDSNotesMaster.SelectCommand = "SELECT * From tblnotes where rcno <>0 and keyfield='" + txtAccountID.Text + "'"


            SqlDSNotesMaster.DataBind()
            gvNotesMaster.DataBind()
        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "gvNotesMaster_PageIndexChanging", ex.Message.ToString, txtAccountID.Text)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
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
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "gvNotesMaster_SelectedIndexChanged", ex.Message.ToString, txtAccountID.Text)
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
        lblmessage.Text = "ACTION: ADD NOTES"


        txtNotesMode.Text = "Add"
        txtNotes.Focus()

    End Sub

    Protected Sub btnSaveNotesMaster_Click(sender As Object, e As EventArgs) Handles btnSaveNotesMaster.Click
        If String.IsNullOrEmpty(txtNotes.Text) Then
            ' MessageBox.Message.Alert(Page, "Select Staff to proceed!!", "str")
            lblalert.Text = "ENTER NOTES"
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
                    lblalert.Text = "NOTES ALREADY EXISTS"

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
                        command.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))



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
                        command.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                        command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))


                    End If


                    command.Connection = conn

                    command.ExecuteNonQuery()
                    txtNotesRcNo.Text = command.LastInsertedId

                    '   MessageBox.Message.Alert(Page, "Record added successfully!!!", "str")
                    lblmessage.Text = "ADD: NOTES SUCCESSFULLY ADDED"
                    lblalert.Text = ""

                End If
                conn.Close()
                conn.Dispose()
                command1.Dispose()
                dt.Dispose()
                dr.Close()

            Catch ex As Exception
                InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "btnSaveNotesMaster_Click", ex.Message.ToString, txtAccountID.Text)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            End Try
            EnableNotesControls()

        ElseIf txtNOTESMode.Text = "Edit" Then
            If txtNotesRcNo.Text = "" Then
                '   MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
                lblalert.Text = "SELECT RECORD TO EDIT"

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

                    lblalert.Text = "NOTES ALREADY EXISTS"



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
                            command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))

                        ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then
                            command.Parameters.AddWithValue("@notes", txtNotes.Text)

                            command.Parameters.AddWithValue("@StaffID", lblNotesStaffID.Text)
                            command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                            command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                        End If

                        command.Connection = conn

                        command.ExecuteNonQuery()

                        '  MessageBox.Message.Alert(Page, "Record updated successfully!!!", "str")
                        lblmessage.Text = "EDIT: NOTES SUCCESSFULLY UPDATED"
                        lblalert.Text = ""
                    End If
                End If


                txtNotesMode.Text = ""

                conn.Close()
                conn.Dispose()
                command2.Dispose()
                dt1.Dispose()
                dr1.Close()

            Catch ex As Exception
                InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "btnSaveNotesMaster_Click", ex.Message.ToString, txtAccountID.Text)
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
        Try
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
        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "btnEditNotesMaster_Click", ex.Message.ToString, txtAccountID.Text)
        End Try
    End Sub

    Protected Sub btnDeleteNotesMaster_Click(sender As Object, e As EventArgs) Handles btnDeleteNotesMaster.Click
        lblmessage.Text = ""
        If txtNotesRcNo.Text = "" Then
            ' MessageBox.Message.Alert(Page, "Select a record to delete!!!", "str")
            lblalert.Text = "SELECT RECORD TO DELETE"
            Return
        End If
        lblmessage.Text = "ACTION: DELETE NOTES"

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
                    lblmessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"

                End If
                conn.Close()
                conn.Dispose()
                command1.Dispose()
                dt.Dispose()
                dr.Close()

            Catch ex As Exception
                InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "btnDeleteNotesMaster_Click", ex.Message.ToString, txtAccountID.Text)
            End Try
            EnableNotesControls()

            SqlDSNotesMaster.SelectCommand = "select * from tblnotes where keyfield = '" + txtAccountID.Text + "'"
            SqlDSNotesMaster.DataBind()
            gvNotesMaster.DataBind()
            MakeNotesNull()
            lblmessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"

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

    Protected Sub OnSelectedIndexChangedgNotes(sender As Object, e As EventArgs)
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

    Protected Sub btnConfirmDelete_Click(sender As Object, e As EventArgs) Handles btnConfirmDelete.Click
        Try
            Dim deletefilepath1 As String = Server.MapPath("~/Uploads/Customer/DeletedFiles/") + txtFileLink.Text
            Dim deletefilepath As String = Server.MapPath("~/Uploads/Customer/DeletedFiles/") + Path.GetFileNameWithoutExtension(deletefilepath1) + "_" + DateTime.Now.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("ssmmhh") + Path.GetExtension(deletefilepath1)
            File.Move(txtDeleteUploadedFile.Text, deletefilepath)

            '  File.Delete(txtDeleteUploadedFile.Text)
            '  Response.Redirect(Request.Url.AbsoluteUri)

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
                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "FILEUPLOADDELETE", txtFileLink.Text, "DELETE", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtFileLink.Text)
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


            'lblMessage.Text = "FILE DELETED"
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
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "btnConfirmDelete_Click", ex.Message.ToString, txtAccountID.Text)
        End Try
    End Sub

    Protected Sub ddlView_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlView.SelectedIndexChanged
        Try
            GridView1.PageSize = Convert.ToInt16(ddlView.SelectedItem.Text)
            SqlDataSource1.SelectCommand = txt.Text
            SqlDataSource1.DataBind()
            GridView1.DataBind()
        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "ddlView_SelectedIndexChanged", ex.Message.ToString, txtAccountID.Text)
        End Try
    End Sub

    Protected Sub ddlView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlView1.SelectedIndexChanged
        GridView2.PageSize = Convert.ToInt16(ddlView1.SelectedItem.Text)

        SqlDataSource2.SelectCommand = txtDetail.Text
        SqlDataSource2.DataBind()
        GridView2.DataBind()

    End Sub

    Protected Sub txtSelectedIndex_TextChanged(sender As Object, e As EventArgs) Handles txtSelectedIndex.TextChanged

    End Sub

    Protected Sub GridView1_Sorting(sender As Object, e As GridViewSortEventArgs) Handles GridView1.Sorting
        SqlDataSource1.SelectCommand = txt.Text
        SqlDataSource1.DataBind()
        GridView1.DataBind()

    End Sub

    Protected Sub grdViewInvoiceDetails_Sorting(sender As Object, e As GridViewSortEventArgs) Handles grdViewInvoiceDetails.Sorting
        UpdateTransactions()

        ModalPopupInvoice.Show()

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


        btnSpecificLocation.Enabled = False
        btnSpecificLocation.ForeColor = System.Drawing.Color.Gray

        btnTransfersSvc.Enabled = False
        btnTransfersSvc.ForeColor = System.Drawing.Color.Gray
        'ddlInchargeSvc.Text = ddlIncharge.Text
        'ddlTermsSvc.Text = ddlTerms.Text
        'ddlIndustrysvc.Text = ddlIndustry.Text
        'ddlSalesManSvc.Text = ddlSalesMan.Text

        'FindMarketSegmentID()

        DisableSvcControls()
        txtSvcMode.Text = "NEW"

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

            lblAlert.Text = errorMsg

        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON" + txtCreatedBy.Text, "InsertIntoTblWebEventLog", ex.Message.ToString, "ERROR")
        End Try
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
            dr.Close()
        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "FindLocation", ex.Message.ToString, txtAccountID.Text)
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
                    Label66.Visible = True
                    Label67.Visible = True
                    Label68.Visible = True
                Else
                    txtPostalValidate.Text = "FALSE"
                    Label66.Visible = False
                    Label67.Visible = False
                    Label68.Visible = False
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


            If dtServiceRecordMasterSetup.Rows.Count > 0 Then
                txtDisplayRecordsLocationwise.Text = dtServiceRecordMasterSetup.Rows(0)("DisplayRecordsLocationWise").ToString
                txtDisplayTimeInTimeOutServiceRecord.Text = dtServiceRecordMasterSetup.Rows(0)("DisplayTimeInTimeOutInServiceReport").ToString

            End If

            If txtDisplayRecordsLocationwise.Text = "Y" Then
                lblBranch1.Visible = True
                Label20.Visible = True
                ddlBranch.Visible = True
                BranchSearch.Visible = True

                Label65.Visible = True
                Label22.Visible = True
                ddlLocation.Visible = True
            Else
                lblBranch1.Visible = False
                Label20.Visible = False
                ddlBranch.Visible = False
                BranchSearch.Visible = False

                Label65.Visible = False
                Label22.Visible = False
                ddlLocation.Visible = False
            End If

            conn.Close()
            conn.Dispose()
            commandServiceRecordMasterSetup.Dispose()
            dtServiceRecordMasterSetup.Dispose()
        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "IsDisplayRecordsLocationwise", ex.Message.ToString, txtAccountID.Text)
        End Try

    End Sub

    Protected Sub ddlIndustrysvc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlIndustrysvc.SelectedIndexChanged
        FindMarketSegmentID()
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
            command1.Dispose()
            dt.Dispose()
            dr.Close()
        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "FindMarketSegmentID", ex.Message.ToString, ddlIndustrysvc.Text)
        End Try
    End Sub

    Protected Sub btnTransfersSvc_Click(sender As Object, e As EventArgs) Handles btnTransfersSvc.Click
        Try
            Session("relocatefrom") = "relocateP"

            Session("gridsql") = txt.Text
            Session("rcno") = txtRcno.Text

            Session("gridsqlCompanyDetail") = txtDetail.Text
            Session("rcnoDetail") = txtSvcRcno.Text
            'Session("inactive") = chkInactive.Checked

            Session("LocationIDtoRelocate") = txtLocationID.Text
            Session("ServiceNametoRelocate") = txtServiceName.Text
            Session("ContactTypetoRelocate") = "RESIDENTIAL"

            Response.Redirect("AccountIDRelocation.aspx")
        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "btnTransfersSvc_Click", ex.Message.ToString, txtAccountID.Text)
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

            qry = "Update tblPerson set    "
            qry = qry + " ArCurrency = @Currency, ARTerm=@ARTerm, AutoEmailInvoice = @AutoEmailInvoice,AutoEmailSOA = @AutoEmailSOA, DefaultInvoiceFormat= @DefaultInvoiceFormat, SendStatement = @SendStatement,HardCopyInvoice=@HardCopyInvoice,BillingOptionRemarks=@BillingOptionRemarks,RequireEBilling=@RequireEBilling,LastModifiedBy = @LastModifiedBy, LastModifiedOn=@LastModifiedOn "
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
            chkSendStatementSOA.Checked = chkSendStatementSOAEdit.Checked
            chkSendStatementInv.Checked = chkSendStatementInvEdit.Checked
            txtBillingOptionRemarks.Text = txtBillingOptionRemarksEdit.Text
            chkRequireEBilling.Checked = chkRequireEBillingEdit.Checked

            'CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CORPORATE", txtAccountID.Text, "EDITSENDSTATEMENT", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtAccountID.Text, "", txtRcno.Text)
            CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "PERS", txtAccountID.Text, "EDITSENDSTATEMENT", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")), 0, 0, 0, txtAccountID.Text, "", txtRcno.Text)

        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "btnSaveSendStatement_Click", ex.Message.ToString, txtAccountID.Text)
        End Try
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

                command1.CommandText = "SELECT * FROM tblPersonlocationspecificlocation where rcno=" & Convert.ToInt32(txtSpecificLocationRcNo.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "delete from tblPersonlocationspecificlocation where rcno=" & Convert.ToInt32(txtSpecificLocationRcNo.Text)

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
                InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "SPECIFIC LOCATION DELETE", ex.Message.ToString, txtAccountID.Text)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            End Try

            EnableSpecificLocationControls()
            SqlDSSpecificLocation.SelectCommand = "select * from tblPersonlocationspecificlocation where LocationID = '" + txtLocationIDSpecificLocation.Text + "'"
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

                command1.CommandText = "SELECT * FROM tblPersonlocationspecificlocation where LocationId=@LocationId and SpecificLocationName=@SpecificLocation"
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
                    Dim qry As String = "INSERT INTO tblPersonlocationspecificlocation(AccountID, LocationID, SpecificLocationName, Zone, CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn)VALUES(@AccountID, @LocationID,@SpecificLocation, @Zone, @CreatedOn,@CreatedBy,@LastModifiedBy,@LastModifiedOn);"
                    command.CommandText = qry
                    command.Parameters.Clear()
                    If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then
                        command.Parameters.AddWithValue("@AccountID", txtAccountIDSpecificLocation.Text.ToUpper)
                        command.Parameters.AddWithValue("@LocationID", txtLocationIDSpecificLocation.Text.ToUpper)

                        command.Parameters.AddWithValue("@SpecificLocation", txtSpecificLocation.Text.ToUpper)
                        command.Parameters.AddWithValue("@Zone", txtZone.Text.ToUpper)
                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))



                    ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then
                        command.Parameters.AddWithValue("@AccountID", txtAccountIDSpecificLocation.Text)
                        command.Parameters.AddWithValue("@LocationID", txtLocationIDSpecificLocation.Text)
                        command.Parameters.AddWithValue("@SpecificLocation", txtSpecificLocation.Text)
                        command.Parameters.AddWithValue("@Zone", txtZone.Text)
                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                        command.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                        command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))


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
                InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "SPECIFIC LOCATION ADD SAVE", ex.Message.ToString, txtAccountID.Text)
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
                command2.CommandText = "SELECT * FROM tblPersonlocationSpecificlocation where SpecificLocationName=@SpecificLocaionName and LocationID=@LocationID and rcno<>" & Convert.ToInt32(txtSpecificLocationRcNo.Text)
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

                    command1.CommandText = "SELECT * FROM tblPersonLocationspecificlocation where rcno=" & Convert.ToInt32(txtSpecificLocationRcNo.Text)
                    command1.Connection = conn

                    Dim dr As MySqlDataReader = command1.ExecuteReader()
                    Dim dt As New DataTable
                    dt.Load(dr)

                    If dt.Rows.Count > 0 Then

                        Dim command As MySqlCommand = New MySqlCommand

                        command.CommandType = CommandType.Text
                        Dim qry As String = "UPDATE tblPersonlocationspecificlocation SET SpecificLocationName=@SpecificLocaion,Zone=@Zone, LastModifiedBy = @LastModifiedBy,LastModifiedOn = @LastModifiedOn WHERE  rcno=" & Convert.ToInt32(txtSpecificLocationRcNo.Text)

                        command.CommandText = qry
                        command.Parameters.Clear()

                        If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then
                            command.Parameters.AddWithValue("@SpecificLocaion", txtSpecificLocation.Text.ToUpper)
                            command.Parameters.AddWithValue("@Zone", txtZone.Text.ToUpper)
                            command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                            command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))

                        ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then
                            command.Parameters.AddWithValue("@SpecificLocaion", txtSpecificLocation.Text)
                            command.Parameters.AddWithValue("@Zone", txtZone.Text)
                            command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                            command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
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
                InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "SPECIFIC LOCATION EDIT SAVE", ex.Message.ToString, txtAccountID.Text)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            End Try
            EnableSpecificLocationControls()

        End If
        SqlDSSpecificLocation.SelectCommand = "select * from tblPersonlocationspecificlocation where LocationID = '" + txtLocationIDSpecificLocation.Text + "'"
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
            txtZone.Text = Server.HtmlDecode(gvSpecificLocation.SelectedRow.Cells(3).Text)
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

    Protected Sub btnSpecificLocationCancel_Click(sender As Object, e As EventArgs) Handles btnSpecificLocationCancel.Click
        MakeSpecificLocationNull()
        EnableSpecificLocationControls()
        txtSpecificLocation.Text = ""
        mdlPopupSpecificLocaion.Show()
    End Sub

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
        SqlDSSpecificLocation.SelectCommand = "select * from tblPersonlocationspecificlocation where LocationID = '" + txtLocationIDSpecificLocation.Text + "' order by SpecificlocationName"
        SqlDSSpecificLocation.DataBind()
        gvSpecificLocation.DataBind()
        mdlPopupSpecificLocaion.Show()
    End Sub


    ''' Client Access

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
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "btnEditCP_Click", ex.Message.ToString, txtAccountID.Text)
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
                Dim qry As String = "delete from tblPersoncustomeraccess where rcno=" & Convert.ToInt32(txtCPRcno.Text)

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
                InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "btnDeleteCP_Click", ex.Message.ToString, txtAccountID.Text)
            End Try
            EnableCPControls()

            SqlDSCP.SelectCommand = "select * from tblPersoncustomeraccess where Accountid = '" + txtAccountID.Text + "'"
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
                Dim qry As String = "INSERT INTO tblPersoncustomeraccess(AccountID, Status, Name, EmailAddress, Password, UserID,  ChangepasswordOnNextLogin, CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn)VALUES(@AccountID, @Status, @Name, @EmailAddress, @Password, @UserID,  @ChangepasswordOnNextLogin,@CreatedOn,@CreatedBy,@LastModifiedBy,@LastModifiedOn);"
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
                    command.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                    command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                    command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))

                ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then
                    command.Parameters.AddWithValue("@AccountID", txtAccountIDCP.Text)
                    command.Parameters.AddWithValue("@Status", chkStatusCP.Checked)
                    command.Parameters.AddWithValue("@Name", txtNameCP.Text.ToUpper)
                    command.Parameters.AddWithValue("@EmailAddress", txtEmailCP.Text.ToUpper)
                    command.Parameters.AddWithValue("@Password", txtPwdCP.Text)
                    command.Parameters.AddWithValue("@UserID", txtUserIDCP.Text.ToUpper)
                    command.Parameters.AddWithValue("@ChangepasswordOnNextLogin", chkChangePasswordonNextLogin.Checked)
                    command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                    command.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                    command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                    command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))

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
                InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "btnSaveCP_Click", ex.Message.ToString, txtAccountID.Text)
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
                Dim qry As String = "UPDATE tblPersoncustomeraccess SET AccountID=@AccountID, Name=@Name, Status = @Status, EmailAddress=@EmailAddress,Password = @Password, UserID=@UserID, ChangepasswordOnNextLogin=@ChangepasswordOnNextLogin, LastModifiedBy = @LastModifiedBy,LastModifiedOn = @LastModifiedOn WHERE  rcno=" & Convert.ToInt32(txtCPRcno.Text)

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
                    command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))

                ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then
                    command.Parameters.AddWithValue("@AccountID", txtAccountIDCP.Text)
                    command.Parameters.AddWithValue("@Name", txtNameCP.Text)
                    command.Parameters.AddWithValue("@Status", chkStatusCP.Checked)
                    command.Parameters.AddWithValue("@EmailAddress", txtEmailCP.Text)
                    command.Parameters.AddWithValue("@Password", txtPwdCP.Text)
                    command.Parameters.AddWithValue("@UserID", txtUserIDCP.Text)
                    command.Parameters.AddWithValue("@ChangepasswordOnNextLogin", chkChangePasswordonNextLogin.Checked)

                    command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                    command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
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
                InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "btnSaveCP_Click", ex.Message.ToString, txtAccountID.Text)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            End Try

            EnableCPControls()
        End If
        SqlDSCP.SelectCommand = "select * from tblPersoncustomeraccess where AccountId = '" + txtAccountID.Text + "'"
        SqlDSCP.DataBind()
        gvCP.DataBind()

        txtCPMode.Text = ""
    End Sub

    Protected Sub btnCancelCP_Click(sender As Object, e As EventArgs) Handles btnCancelCP.Click
        MakeCPNull()
        EnableCPControls()
        txtCPMode.Text = ""
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

    Protected Sub btnEditContractGroup_Click(sender As Object, e As ImageClickEventArgs) Handles btnEditContractGroup.Click
        txtCreatedOn.Text = ""
        mdlPopupContractGroup.Show()
    End Sub

    Protected Sub btnContractGroupEditSave_Click(sender As Object, e As EventArgs) Handles btnContractGroupEditSave.Click
        If ddlContractGroupEdit.Text = ddlContractGrp.Text Then
            lblAlertContractGroup.Text = "NO CHANGES MADE"
            mdlPopupContractGroup.Show()
            Return

        End If

        Try

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim command As MySqlCommand = New MySqlCommand

            command.CommandType = CommandType.Text
            command.CommandText = "UPDATE tblPersonLocation SET ContractGroup = @ContractGroup, LastModifiedBy = @LastModifiedBy, LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(txtSvcRcno.Text)
            command.Parameters.Clear()

            command.Parameters.AddWithValue("@ContractGroup", ddlContractGroupEdit.Text)
            command.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
            command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
            command.Connection = conn

            command.ExecuteNonQuery()
            conn.Close()
            conn.Dispose()
            command.Dispose()
            ddlContractGrp.Text = ddlContractGroupEdit.Text

            GridView1.DataSourceID = "SqlDataSource1"
            GridView1.DataBind()

            SqlDataSource2.SelectCommand = txtDetail.Text
            SqlDataSource2.DataBind()
            GridView2.DataSourceID = "SqlDataSource2"

            CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "PERSON", txtLocationID.Text, "EDITCONTRACTGROUP", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtAccountID.Text, "", txtRcno.Text)

            'GridView1.DataSourceID = "SqlDataSource1"
            mdlPopupContractGroup.Hide()
            'mdlPopupNotes.Hide()
        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON - " + Session("UserID"), "btnContractGroupEditSave_Click", ex.Message.ToString, txtLocationID.Text)
            Exit Sub
        End Try
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
            InsertIntoTblWebEventLog("PERSON - " + Session("UserID"), "btnEditHistory_Click", ex.Message.ToString, "")

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
                    qry = "UPDATE tblPersonlocation SET ContactPerson = @ContactPerson,Telephone = @Telephone,Mobile = @Mobile,Email = @Email, Fax = @Fax, Contact1Position = @Contact1Position,Telephone2 = @Tel2,ContactPerson2 = @ContactPerson2,Contact2Position = @Contact2Position,Contact2Tel = @Contact2Tel,Contact2Fax = @Contact2Fax,Contact2Tel2 = @Contact2Tel2,Contact2Mobile = @Contact2Mobile,Contact2Email = @Contact2Email, ServiceEmailCC=@ServiceEmailCC where Accountid= '" & txtAccountID.Text.Trim & "' and LocationID = '" & lblLocationID1.Text & "'"

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
                    lsql = lsql + " SalesManSvc, InchargeIDSvc, ARTermSvc, Industry, MarketSegmentID, ContractGroup, PersonGroupD, ServiceEmailCC, "
                    lsql = lsql + " SendServiceReportTo1, SendServiceReportTo2 "
                    lsql = lsql + "From tblPersonLocation "
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
                        tddlCompanyGrpD = dtServiceLocationDetails.Rows(0)("PersonGroupD").ToString.Trim
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
            InsertIntoTblWebEventLog("PERSON - " + Session("UserID"), "ProcessUpdate", ex.Message.ToString, "")
            lblAlert.Text = ex.Message.ToString
        End Try



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
            InsertIntoTblWebEventLog("PERSON - " + Session("UserID"), "chkAll_CheckedChanged", ex.Message.ToString, "")
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
            InsertIntoTblWebEventLog("PERSON - " + Session("UserID"), "FUNCTION UpdateContractHeader", ex.Message.ToString, txtLocationID.Text)
            Exit Sub

        End Try
    End Sub

    Protected Sub chkDefaultContactInfo_CheckedChanged(sender As Object, e As EventArgs) Handles chkDefaultContactInfo.CheckedChanged
        txtSvcCP1ContactUpdateContactInformation.Text = txtOffContactPerson.Text
        'txtSvcCP1PositionUpdateContactInformation.Text = txtOffPosition.Text
        txtSvcCP1TelephoneUpdateContactInformation.Text = txtOffContactNo.Text
        txtSvcCP1FaxUpdateContactInformation.Text = txtOffFax.Text
        txtSvcCP1Telephone2UpdateContactInformation.Text = txtOffContact2.Text
        txtSvcCP1MobileUpdateContactInformation.Text = txtOffMobile.Text
        txtSvcCP1EmailUpdateContactInformation.Text = txtOffEmail.Text
        txtSvcCP2ContactUpdateContactInformation.Text = txtOffCont1Name.Text
        'txtSvcCP2PositionUpdateContactInformation.Text = txtOffCont1Position.Text
        txtSvcCP2TelephoneUpdateContactInformation.Text = txtOffCont1Tel.Text
        txtSvcCP2FaxUpdateContactInformation.Text = txtOffCont1Fax.Text
        txtSvcCP2Tel2UpdateContactInformation.Text = txtOffCont1Tel2.Text
        txtSvcCP2MobileUpdateContactInformation.Text = txtOffCont1Mobile.Text
        txtSvcCP2EmailUpdateContactInformation.Text = txtOffCont1Email.Text
        mdlUpdateServiceContact.Show()
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
            InsertIntoTblWebEventLog("PERSON - " + Session("UserID"), "btnEditServiceContactSaveYes_Click", ex.Message.ToString, "")
            lblAlert.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnEditServiceContactSaveNo_Click(sender As Object, e As EventArgs) Handles btnEditServiceContactSaveNo.Click
        txtConfirmationCode.Text = ""
    End Sub

    Protected Sub btnUpdateServiceContact_Click(sender As Object, e As EventArgs) Handles btnUpdateServiceContact.Click
        Try
            SqlDataSource3.SelectCommand = "Select Rcno, ContractGroup, LocationID, Address1, AddPOstal from tblPersonLocation where AccountId = '" & txtAccountID.Text & "' Order by LocationID"

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
            InsertIntoTblWebEventLog("PERSON - " + Session("UserID"), "btnEditHistory_Click", ex.Message.ToString, "")
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


            lblRandom.Text = Random.Next(100000, 900000)

            'updPanelMassChange1.Update()
            'lblLine4EditAgreeValueSave.Text = txtRandom.Text
            'If chkUpdateServiceRecords.Checked = False Then
            mdlWarning.Show()
            'Else
            'ProcessUpdate()
            'End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("PERSON - " + Session("UserID"), "btnEditHistory_Click", ex.Message.ToString, "")
            lblAlert.Text = ex.Message.ToString
        End Try
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
                    sqlstr = "Select Name from tblPerson where Name = """ & txtNameE.Text.Trim & """ and location = '" & ddlLocation.Text.Trim & "'"
                Else
                    sqlstr = "Select Name from tblPerson where Name = """ & txtNameE.Text.Trim & """"
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
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "ddlLocation_SelectedIndexChanged", ex.Message.ToString, txtNameE.Text)
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

            sqlstr = "Select AccountID,Name,concat(Address1,AddStreet,AddBuilding) as Address from tblperson where Address1 = """ & txtOffAddress1.Text.Trim & """"

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
            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "txtOffAddress1_TextChanged", ex.Message.ToString, txtNameE.Text)
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
                sqlstr = "Select Name from tblperson where Name = """ & txtNameE.Text.Trim.ToUpper & """ and location = '" & ddlLocation.Text.Trim & "'"
            Else
                sqlstr = "Select Name from tblperson where Name = """ & txtNameE.Text.Trim.ToUpper & """"
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

            command1.CommandText = "SELECT AccountID,Name,concat(Address1,AddStreet,AddBuilding) as Address as Address FROM tblperson where Name like '%" & CustName & "%'"
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

            sqlstr2 = "Select AccountID,Name,concat(Address1,AddStreet,AddBuilding) as Address from tblPERSON where Address1 = """ & txtOffAddress1.Text.Trim & """"

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

            InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "ValidateSave()", ex.Message.ToString, txtNameE.Text)
            Return False

        End Try
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

        command.Parameters.AddWithValue("@AccountType", "PERSON")

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
End Class

Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Threading

Public Class ContractDetail
    Inherits System.Web.UI.Page
    ' Dim cls As New clsDBAccess()
    Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    Dim client As String
    Public rcno As String
    'CHECK ALL THE INPUT FIELDS ARE VALIDATED AND ALL INPUT PARAMETERS ARE GIVEN IN INSERT COMMAND

    Public TotDetailRecords As Integer
    Dim gFreqMTD As String
    Dim gSeq As String
    Dim gServiceDate As Date
    Dim gdayofWeek As String
    Dim gServiceDatestr As String

    Dim rowdeleted As Boolean
    Dim RowNumber As Integer
    Dim RowIndexSch As Integer

    Dim gDOW As String
    Dim gDayNo As Integer
    Dim gMonthNo As Integer
    Dim gWeekNo As Integer
    Dim mode As String

    'Dim gServiceDateArr(366) As Date
    'Dim gServiceDatestrArr(366) As String
    'Dim gdayofWeekArr(366) As String

    Public HasDuplicateTarget As Boolean

    Private Shared prevPage As String = String.Empty


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Dim Query As String

        'Restrict users manual date entries
        txtContractNo.Attributes.Add("readonly", "readonly")
        txtContractDate.Attributes.Add("readonly", "readonly")
        txtContactType.Attributes.Add("readonly", "readonly")
        txtCustName.Attributes.Add("readonly", "readonly")

        txtContact.Attributes.Add("readonly", "readonly")
        txtFrequencyDesc.Attributes.Add("readonly", "readonly")
        txtServiceDesc.Attributes.Add("readonly", "readonly")
  
        txtServStart.Attributes.Add("readonly", "readonly")
        txtServEnd.Attributes.Add("readonly", "readonly")

        btnServiceSchedule.Attributes.Add("onclick", "btn_disable()")
        btnServiceSchedule.Attributes.Add("onclick", ClientScript.GetPostBackEventReference(btnServiceSchedule, "") + ";;this.disabled = true")

        btnSave.Attributes.Add("onclick", "btn_disable1()")
        btnSave.Attributes.Add("onclick", ClientScript.GetPostBackEventReference(btnSave, "") + ";;this.disabled = true")


        If Not Page.IsPostBack Then
            ''mdlPopUpContact.Hide()
            'prevPage = Request.UrlReferrer.ToString()
            Session("contractdetailfrom") = ""
            txtContractNo.Text = Session("contractno")
            txtContractDate.Text = Session("contractdate")
            txtContactType.Text = Session("contracttype")
            txtClient.Text = Session("client")
            txtCustName.Text = Session("custname")

            txtContact.Text = Session("contact")
            txtServStart.Text = Session("servstart")
            txtServEnd.Text = Session("servend")
            txtAgreedValue.Text = Session("agreedvalue")
            txtDiscAmt.Text = Session("discamt")
            txtStatus.Text = Session("status")
            txtBillingFreq.Text = Session("billingfreq")
            txtBillingAmount.Text = Session("billingamount")


            If Session("GS") = "P" Then
                btnServiceSchedule.Enabled = False
            End If

            'Session.Remove("contractdate")
            'Session.Remove("contracttype")
            'Session.Remove("client")
            'Session.Remove("custname")
            'Session.Remove("contact")
            'Session.Remove("servstart")
            'Session.Remove("servend")
            'Session.Remove("agreedvalue")
            'Session.Remove("discamt")
            'Session.Remove("status")
          
            MakeMeNull()
            'EnableControls()

            'FirstGridViewRowSchedule()

            'Dim TextBoxTargetDesc As DropDownList = CType(grvTargetDetails.Rows(0).Cells(0).FindControl("ddlTargetDescGV"), DropDownList)
            'Query = "Select targetId, descrip1 from tblTarget"

            'PopulateDropDownList(Query, "descrip1", "descrip1", TextBoxTargetDesc)
            TotDetailRecords = 0
            txtMode.Text = "New"
            rowdeleted = False
            txtTotalRecords.Text = GridView1.Rows.Count

            'If Convert.ToInt32(txtTotalRecords.Text) > 0 Then
            '    btnServiceSchedule.Enabled = True
            'Else
            '    btnServiceSchedule.Enabled = False
            'End If
        End If
    End Sub

    Public Sub ddlContactType_SelectedIndexChanged(sender As Object, e As EventArgs)
        ''ddlClient.Items.Clear()
        ''ddlContactPerson.Items.Clear()
        ''ddlClient.Items.Add("--Select--")
        ''ddlContactPerson.Items.Add("--Select--")
        'Try
        '    If ddlContactType.SelectedValue.ToUpper = "COMPANY" Then
        '        Dim query As String = "select ContID,ContName from tblContactMaster where upper(ContType)='COMPANY'"
        '        ' PopulateDropDownList(query, "ContID", "ContID", ddlClient)
        '        PopulateDropDownList(query, "ContName", "ContName", ddlContactPerson)
        '    ElseIf ddlContactType.SelectedValue.ToUpper = "PERSON" Then
        '        Dim query As String = "select ContID, ContName from tblContactMaster where upper(ContType)='PERSON'"
        '        ' PopulateDropDownList(query, "ContID", "ContID", ddlClient)
        '        PopulateDropDownList(query, "ContName", "ContName", ddlContactPerson)
        '    Else
        '        MessageBox.Message.Alert(Page, "Please select Contact Type", "str")
        '        ' ddlClient.Items.Clear()
        '        ddlContactPerson.Items.Clear()
        '        '  ddlClient.Items.Add("--Select--")
        '        ddlContactPerson.Items.Add("--Select--")
        '    End If
        'Catch ex As Exception
        '    MessageBox.Message.Alert(Page, ex.Message.ToString, "str")
        'End Try
    End Sub

    Protected Sub btnADD_Click(sender As Object, e As EventArgs) Handles btnADD.Click
        If mode = "saveNew" Then
            MessageBox.Message.Alert(Page, "Record Added successfully!!!", "str")
        ElseIf mode = "saveEdit" Then
            MessageBox.Message.Alert(Page, "Record Updated successfully!!!", "str")
        
        End If

        MakeMeNull()
        'DisableControls()
        txtMode.Text = "New"
        txtContractNo.Focus()
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try

            'If txtFrequency.Text.Trim = "" Then
            '    ''MessageBox.Message.Alert(Page, "Frequency cannot be blank!!!", "str")
            '    Dim message As String = "alert('Frequency cannot be blank!!!')"
            '    ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)
            '    Return
            'End If
            'If txtServiceId.Text.Trim = "" Then
            '    ''MessageBox.Message.Alert(Page, "Service Id cannot be blank!!!", "str")
            '    Dim message As String = "alert('Service Id cannot be blank!!!')"
            '    ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)
            '    Return
            'End If

            'If IsNumeric(txtValuePerService.Text) = False Then
            '    ''MessageBox.Message.Alert(Page, "Frequency cannot be blank!!!", "str")
            '    Dim message As String = "alert('Value can be numeric only!!!')"
            '    ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)
            '    Return
            'End If

            'If txtDuplicateTarget.Text = "Y" Then
            '    Dim message As String = "alert('Duplicate Target/Pests has been selected!!!')"
            '    ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)

            '    Exit Sub
            'End If

            'System.Threading.Thread.Sleep(5000)

            If txtMode.Text = "New" Then
                Try
                    Dim conn As MySqlConnection = New MySqlConnection(constr)
                    conn.Open()

                    Dim command As MySqlCommand = New MySqlCommand
                    command.CommandType = CommandType.Text

                    Dim qry As String = "INSERT INTO tblContractdet(ContractNo, Frequency, Value, NoService, Location, BranchId,"
                    qry = qry + "NoOfSvcInterval, NoOfInterval, ServiceId, ServiceDesc, FrequencyDesc, TargetDesc, "
                    qry = qry + "ContactPerson, ServiceNotes, CreatedBy, CreatedOn,"
                    qry = qry + "LastModifiedBy, LastModifiedOn)"  'total 77 fields

                    qry = qry + " VALUES(@ContractNo, @Frequency, @Value, @NoService, @Location, @BranchId, @NoOfSvcInterval, @NoOfInterval, @ServiceId,"
                    qry = qry + "@ServiceDesc, @FrequencyDesc, @TargetDesc,  @ContactPerson, @ServiceNotes,"
                    qry = qry + "@CreatedBy, @CreatedOn, @LastModifiedBy, @LastModifiedOn)"


                    command.CommandText = qry
                    command.Parameters.Clear()

                    command.Parameters.AddWithValue("@ContractNo", txtContractNo.Text.Trim)
                    command.Parameters.AddWithValue("@Frequency", txtFrequency.Text.Trim)

                    If String.IsNullOrEmpty(txtValuePerService.Text) = True Or txtValuePerService.Text = "" Then
                        command.Parameters.AddWithValue("@Value", 0.0)
                    Else
                        command.Parameters.AddWithValue("@Value", Convert.ToDecimal(txtValuePerService.Text))
                    End If

                    command.Parameters.AddWithValue("@NoService", txtNoService.Text)

                    command.Parameters.AddWithValue("@Location", txtNoofSvcInterval.Text.Trim)
                    command.Parameters.AddWithValue("@BranchId", txtNoofInterval.Text.Trim)
                    command.Parameters.AddWithValue("@NoOfSvcInterval", txtNoofSvcInterval.Text.Trim)
                    command.Parameters.AddWithValue("@NoOfInterval", txtNoofInterval.Text.Trim)

                    command.Parameters.AddWithValue("@ServiceId", txtServiceId.Text.Trim)
                    command.Parameters.AddWithValue("@ServiceDesc", txtServiceDesc.Text.Trim)
                    command.Parameters.AddWithValue("@FrequencyDesc", txtFrequencyDesc.Text.Trim)
                    If String.IsNullOrEmpty(txtTargetDesc.Text) = False Then
                        command.Parameters.AddWithValue("@TargetDesc", IIf(Len(txtTargetDesc.Text) >= 100, Left(txtTargetDesc.Text, 99), Left(txtTargetDesc.Text.Trim, (Len(txtTargetDesc.Text) - 1))))
                    Else
                        command.Parameters.AddWithValue("@TargetDesc", txtTargetDesc.Text.Trim)

                    End If

                    command.Parameters.AddWithValue("@ContactPerson", txtContact.Text.Trim)
                    command.Parameters.AddWithValue("@ServiceNotes", txtServiceNotes.Text.Trim)
                    command.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                    command.Parameters.AddWithValue("@CreatedOn", DateAndTime.Now)
                    command.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                    command.Parameters.AddWithValue("@LastModifiedOn", DateAndTime.Now)

                    command.Connection = conn
                    command.ExecuteNonQuery()
                    'MessageBox.Message.Alert(Page, "Record added successfully!!!", "str")
                    ''Exit Sub


                    Dim sqlLastId As String
                    sqlLastId = "SELECT last_insert_id() from tblcontractdet"


                    'Dim conn As MySqlConnection = New MySqlConnection(constr)
                    'conn.Open()

                    Dim command1 As MySqlCommand = New MySqlCommand
                    command1.CommandType = CommandType.Text

                    command1.CommandText = sqlLastId
                    command1.Parameters.Clear()

                    'SqlCommand cmd = new SqlCommand(sql, conn);
                    'conn.Open();
                    command1.Connection = conn
                    txtSourceSQLID.Text = command1.ExecuteScalar()


                    Dim command2 As MySqlCommand = New MySqlCommand
                    command2.CommandType = CommandType.Text
                    command2.CommandText = "Update tblcontractdet set OrigCode = " & Convert.ToInt32(txtSourceSQLID.Text) & " where rcno = " & Convert.ToInt32(txtSourceSQLID.Text)
                    command2.Connection = conn
                    command2.ExecuteNonQuery()


                    'Start:Target
                    SetRowData()

                    Dim tableAdd As DataTable = TryCast(ViewState("CurrentTable"), DataTable)

                    If tableAdd IsNot Nothing Then

                        For rowIndex As Integer = 0 To tableAdd.Rows.Count - 1
                            'string txSpareId = row.ItemArray[0] as string;
                            Dim TextBoxTargetDesc As DropDownList = CType(grvTargetDetails.Rows(rowIndex).Cells(0).FindControl("ddlTargetDescGV"), DropDownList)

                            If (String.IsNullOrEmpty(TextBoxTargetDesc.Text) = False) And (TextBoxTargetDesc.Text <> "0") And (TextBoxTargetDesc.Text <> "-1") Then

                                Dim TextBoxTargtId As TextBox = CType(grvTargetDetails.Rows(rowIndex).Cells(1).FindControl("txtTargtIdGV"), TextBox)

                                'Dim conn As MySqlConnection = New MySqlConnection(constr)
                                'conn.Open()

                                Dim commandTarget As MySqlCommand = New MySqlCommand
                                commandTarget.CommandType = CommandType.Text

                                Dim qryTarget As String = "INSERT INTO tblcontractservingtarget(ContractNo, ServiceId, SourceSQLId,"
                                qryTarget = qryTarget + " TargetId, TargetDesc,  CreatedBy, CreatedOn,"
                                qryTarget = qryTarget + " LastModifiedBy, LastModifiedOn)"

                                qryTarget = qryTarget + " VALUES(@ContractNo, @ServiceId, @SourceSQLId,  @TargetId, @TargetDesc,"
                                qryTarget = qryTarget + "@CreatedBy, @CreatedOn, @LastModifiedBy, @LastModifiedOn)"


                                commandTarget.CommandText = qryTarget
                                commandTarget.Parameters.Clear()

                                commandTarget.Parameters.AddWithValue("@ContractNo", txtContractNo.Text.Trim)
                                commandTarget.Parameters.AddWithValue("@ServiceId", txtServiceId.Text.Trim)
                                commandTarget.Parameters.AddWithValue("@SourceSQLId", txtSourceSQLID.Text)
                                commandTarget.Parameters.AddWithValue("@TargetId", TextBoxTargtId.Text.Trim)
                                commandTarget.Parameters.AddWithValue("@TargetDesc", TextBoxTargetDesc.Text.Trim)

                                commandTarget.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                                commandTarget.Parameters.AddWithValue("@CreatedOn", DateAndTime.Now)
                                commandTarget.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                                commandTarget.Parameters.AddWithValue("@LastModifiedOn", DateAndTime.Now)

                                commandTarget.Connection = conn
                                commandTarget.ExecuteNonQuery()
                                'MessageBox.Message.Alert(Page, "Record added successfully!!!", "str")
                                'End If
                                'conn.Close()


                            End If
                        Next rowIndex
                    End If

                    'End:Target

                    ''
                    'SetRowDataFreq()

                    Dim tableFreq As DataTable = TryCast(ViewState("CurrentTableFreq"), DataTable)

                    If tableFreq IsNot Nothing Then

                        For rowIndexFreq As Integer = 0 To tableFreq.Rows.Count - 1
                            Dim TextBoxSeqNo As TextBox = CType(grvFreqDetails.Rows(rowIndexFreq).Cells(0).FindControl("txtSeqNoGV"), TextBox)

                            If (String.IsNullOrEmpty(TextBoxSeqNo.Text) = False) Then

                                Dim TextBoxFreqMTD As DropDownList = CType(grvFreqDetails.Rows(rowIndexFreq).Cells(1).FindControl("ddlFreqMTDGV"), DropDownList)
                                Dim TextBoxMonthNo As TextBox = CType(grvFreqDetails.Rows(rowIndexFreq).Cells(2).FindControl("txtMonthNoGV"), TextBox)
                                Dim TextBoxDayNo As TextBox = CType(grvFreqDetails.Rows(rowIndexFreq).Cells(3).FindControl("txtDayNoGV"), TextBox)
                                Dim TextBoxWeekNo As TextBox = CType(grvFreqDetails.Rows(rowIndexFreq).Cells(4).FindControl("txtWeekNoGV"), TextBox)
                                Dim TextBoxWeekDOW As DropDownList = CType(grvFreqDetails.Rows(rowIndexFreq).Cells(5).FindControl("ddlWeekDOWGV"), DropDownList)
                                Dim TextBoxLocation As DropDownList = CType(grvFreqDetails.Rows(rowIndexFreq).Cells(6).FindControl("ddlLocationGV"), DropDownList)
                                Dim TextBoxBranchID As DropDownList = CType(grvFreqDetails.Rows(rowIndexFreq).Cells(7).FindControl("ddlBranchIDGV"), DropDownList)
                                Dim TextBoxContractNo As TextBox = CType(grvFreqDetails.Rows(rowIndexFreq).Cells(8).FindControl("txtContractNoGVF"), TextBox)
                                Dim TextBoxSourceSQLID As TextBox = CType(grvFreqDetails.Rows(rowIndexFreq).Cells(9).FindControl("txtSourceSQLIDGVF"), TextBox)
                                Dim TextBoxRcno As TextBox = CType(grvFreqDetails.Rows(rowIndexFreq).Cells(10).FindControl("txtRcnoGVF"), TextBox)


                                Dim commandFreq As MySqlCommand = New MySqlCommand
                                commandFreq.CommandType = CommandType.Text

                                Dim qryFreq As String = "INSERT INTO tblservicecontractfrequency(ContractNo, SeqNo, FreqMTD, DayNo, WeekNo, WeekDow, MonthNo, Location, BranchId,SourceSQLId,"
                                qryFreq = qryFreq + " CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn)"

                                qryFreq = qryFreq + " VALUES(@ContractNo, @SeqNo, @FreqMTD, @DayNo, @WeekNo, @WeekDow, @MonthNo, @Location, @BranchId, @SourceSQLId,"
                                qryFreq = qryFreq + "@CreatedBy, @CreatedOn, @LastModifiedBy, @LastModifiedOn)"

                                commandFreq.CommandText = qryFreq
                                commandFreq.Parameters.Clear()

                                commandFreq.Parameters.AddWithValue("@ContractNo", txtContractNo.Text.Trim)
                                commandFreq.Parameters.AddWithValue("@FreqMTD", TextBoxFreqMTD.Text.Trim)
                                commandFreq.Parameters.AddWithValue("@SourceSQLId", txtSourceSQLID.Text)
                                commandFreq.Parameters.AddWithValue("@SeqNo", TextBoxSeqNo.Text.Trim)

                                If String.IsNullOrEmpty(TextBoxDayNo.Text.Trim) = True Then
                                    commandFreq.Parameters.AddWithValue("@DayNo", 0)
                                Else
                                    commandFreq.Parameters.AddWithValue("@DayNo", TextBoxDayNo.Text.Trim)
                                End If

                                If String.IsNullOrEmpty(TextBoxWeekNo.Text.Trim) = True Then
                                    commandFreq.Parameters.AddWithValue("@WeekNo", "")
                                Else
                                    commandFreq.Parameters.AddWithValue("@WeekNo", TextBoxWeekNo.Text.Trim)
                                End If

                                If String.IsNullOrEmpty(TextBoxMonthNo.Text.Trim) = True Then
                                    commandFreq.Parameters.AddWithValue("@MonthNo", 0)
                                Else
                                    commandFreq.Parameters.AddWithValue("@MonthNo", TextBoxMonthNo.Text.Trim)
                                End If


                                'commandFreq.Parameters.AddWithValue("@WeekNo", TextBoxWeekNo.Text.Trim)
                                commandFreq.Parameters.AddWithValue("@WeekDow", TextBoxWeekDOW.Text.Trim)
                                'commandFreq.Parameters.AddWithValue("@MonthNo", TextBoxMonthNo.Text.Trim)
                                commandFreq.Parameters.AddWithValue("@Location", TextBoxLocation.Text.Trim)
                                commandFreq.Parameters.AddWithValue("@BranchId", TextBoxBranchID.Text.Trim)

                                commandFreq.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                                commandFreq.Parameters.AddWithValue("@CreatedOn", DateAndTime.Now)
                                commandFreq.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                                commandFreq.Parameters.AddWithValue("@LastModifiedOn", DateAndTime.Now)

                                commandFreq.Connection = conn
                                commandFreq.ExecuteNonQuery()
                                'MessageBox.Message.Alert(Page, "Record added successfully!!!", "str")
                                'End If
                                'conn.Close()

                            End If
                        Next rowIndexFreq
                    End If

                    conn.Close()
                    'MessageBox.Message.Alert(Page, "Record added successfully!!!", "str")
                    Dim message As String = "alert('Record added successfully!!!')"
                    ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)
                    GridView1.DataSourceID = "SqlDSContractDet"

                Catch ex As Exception
                    Dim exstr As String
                    exstr = ex.ToString
                    MessageBox.Message.Alert(Page, ex.ToString, "str")
                End Try

                'EnableControls()
            ElseIf txtMode.Text = "Edit" Then
                Try
                    Dim conn As MySqlConnection = New MySqlConnection(constr)
                    conn.Open()

                    Dim command As MySqlCommand = New MySqlCommand
                    command.CommandType = CommandType.Text

                    Dim qry As String = "UPDATE tblContractdet SET  Frequency = @Frequency, Value = @Value, NoService =@NoService, "
                    qry = qry + "Location = @Location, BranchId = @BranchId, NoOfSvcInterval =@NoOfSvcInterval,"
                    qry = qry + "NoOfInterval = @NoOfInterval, ServiceId = @ServiceId, ServiceDesc =@ServiceDesc,"
                    qry = qry + "FrequencyDesc = @FrequencyDesc, ContactPerson = @ContactPerson, ServiceNotes =@ServiceNotes,"
                    qry = qry + "FrequencyDesc = @FrequencyDesc, TargetDesc = @TargetDesc, ContactPerson = @ContactPerson, ServiceNotes =@ServiceNotes,"
                    qry = qry + "LastModifiedBy = @LastModifiedBy,LastModifiedOn = @LastModifiedOn where rcno = @rcno"

                    command.CommandText = qry
                    command.Parameters.Clear()

                    command.Parameters.AddWithValue("@rcno", txtRcno.Text.Trim)
                    command.Parameters.AddWithValue("@ContractNo", txtContractNo.Text.Trim)
                    command.Parameters.AddWithValue("@Frequency", txtFrequency.Text.Trim)
                    command.Parameters.AddWithValue("@Value", Convert.ToDecimal(txtValuePerService.Text))
                    command.Parameters.AddWithValue("@NoService", txtNoService.Text)

                    command.Parameters.AddWithValue("@Location", txtNoofSvcInterval.Text.Trim)
                    command.Parameters.AddWithValue("@BranchId", txtNoofInterval.Text.Trim)
                    command.Parameters.AddWithValue("@NoOfSvcInterval", txtNoofSvcInterval.Text.Trim)
                    command.Parameters.AddWithValue("@NoOfInterval", txtNoofInterval.Text.Trim)

                    command.Parameters.AddWithValue("@ServiceId", txtServiceId.Text.Trim)
                    command.Parameters.AddWithValue("@ServiceDesc", txtServiceDesc.Text.Trim)
                    command.Parameters.AddWithValue("@FrequencyDesc", txtFrequencyDesc.Text.Trim)
                    If String.IsNullOrEmpty(txtTargetDesc.Text) = False Then
                        command.Parameters.AddWithValue("@TargetDesc", IIf(Len(txtTargetDesc.Text) >= 100, Left(txtTargetDesc.Text, 99), Left(txtTargetDesc.Text.Trim, (Len(txtTargetDesc.Text) - 1))))
                    Else
                        command.Parameters.AddWithValue("@TargetDesc", txtTargetDesc.Text.Trim)

                    End If
                    command.Parameters.AddWithValue("@ContactPerson", txtContact.Text.Trim)
                    command.Parameters.AddWithValue("@ServiceNotes", txtServiceNotes.Text.Trim)
                    'command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                    'command.Parameters.AddWithValue("@CreatedOn", DateAndTime.Now)
                    command.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                    command.Parameters.AddWithValue("@LastModifiedOn", DateAndTime.Now)

                    command.Connection = conn
                    command.ExecuteNonQuery()
                    'MessageBox.Message.Alert(Page, "Record updated successfully!!!", "str")
                    Dim message As String = "alert('Record updated successfully!!!')"
                    ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)
                    'End If
                    conn.Close()

                Catch ex As Exception
                    Dim exstr As String
                    exstr = ex.ToString
                    'MessageBox.Message.Alert(Page, ex.ToString, "str
                    ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", ex.ToString, True)
                End Try

                'EnableControls()




                ''
                SetRowData()



                Dim table As DataTable = TryCast(ViewState("CurrentTable"), DataTable)

                If table IsNot Nothing Then

                    For rowIndex As Integer = 0 To table.Rows.Count - 1
                        'string txSpareId = row.ItemArray[0] as string;
                        Dim TextBoxTargetDesc As DropDownList = CType(grvTargetDetails.Rows(rowIndex).Cells(0).FindControl("ddlTargetDescGV"), DropDownList)

                        If (String.IsNullOrEmpty(TextBoxTargetDesc.Text) = False) And (TextBoxTargetDesc.Text <> "0") And (TextBoxTargetDesc.Text <> "-1") Then


                            'New
                            Dim TextBoxRcno As TextBox = CType(grvTargetDetails.Rows(rowIndex).Cells(0).FindControl("txtRcnoGV"), TextBox)

                            If (String.IsNullOrEmpty(TextBoxRcno.Text) = True) Or (TextBoxRcno.Text = "0") Then

                                Dim TextBoxTargtId As TextBox = CType(grvTargetDetails.Rows(rowIndex).Cells(1).FindControl("txtTargtIdGV"), TextBox)

                                Dim conn As MySqlConnection = New MySqlConnection(constr)
                                conn.Open()

                                Dim command As MySqlCommand = New MySqlCommand
                                command.CommandType = CommandType.Text

                                Dim qry As String = "INSERT INTO tblcontractservingtarget(ContractNo, ServiceId, SourceSQLId,"
                                qry = qry + " TargetId, TargetDesc,  CreatedBy, CreatedOn,"
                                qry = qry + " LastModifiedBy, LastModifiedOn)"

                                qry = qry + " VALUES(@ContractNo, @ServiceId, @SourceSQLId,  @TargetId, @TargetDesc,"
                                qry = qry + "@CreatedBy, @CreatedOn, @LastModifiedBy, @LastModifiedOn)"


                                command.CommandText = qry
                                command.Parameters.Clear()

                                command.Parameters.AddWithValue("@ContractNo", txtContractNo.Text.Trim)
                                command.Parameters.AddWithValue("@ServiceId", txtServiceId.Text.Trim)
                                command.Parameters.AddWithValue("@SourceSQLId", txtSourceSQLID.Text)
                                command.Parameters.AddWithValue("@TargetId", TextBoxTargtId.Text.Trim)
                                command.Parameters.AddWithValue("@TargetDesc", TextBoxTargetDesc.Text.Trim)

                                command.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                                command.Parameters.AddWithValue("@CreatedOn", DateAndTime.Now)
                                command.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                                command.Parameters.AddWithValue("@LastModifiedOn", DateAndTime.Now)

                                command.Connection = conn
                                command.ExecuteNonQuery()
                                'MessageBox.Message.Alert(Page, "Record added successfully!!!", "str")
                                'End If
                                conn.Close()

                            Else
                                Dim TextBoxTargtId As TextBox = CType(grvTargetDetails.Rows(rowIndex).Cells(1).FindControl("txtTargtIdGV"), TextBox)

                                Dim conn As MySqlConnection = New MySqlConnection(constr)
                                conn.Open()

                                Dim command As MySqlCommand = New MySqlCommand
                                command.CommandType = CommandType.Text

                                Dim qry As String = "Update tblcontractservingtarget set TargetId = @TargetId, TargetDesc = @TargetDesc,  ServiceId = @ServiceId,"
                                qry = qry + " LastModifiedBy = @LastModifiedBy , LastModifiedOn = @LastModifiedOn where Rcno = @Rcno"


                                command.CommandText = qry
                                command.Parameters.Clear()

                                command.Parameters.AddWithValue("@ServiceId", txtServiceId.Text.Trim)
                                command.Parameters.AddWithValue("@TargetId", TextBoxTargtId.Text.Trim)
                                command.Parameters.AddWithValue("@TargetDesc", TextBoxTargetDesc.Text.Trim)
                                command.Parameters.AddWithValue("@Rcno", TextBoxRcno.Text.Trim)
                                command.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                                command.Parameters.AddWithValue("@LastModifiedOn", DateAndTime.Now)

                                command.Connection = conn
                                command.ExecuteNonQuery()
                                'MessageBox.Message.Alert(Page, "Record added successfully!!!", "str")
                                'End If
                                conn.Close()
                            End If
                        End If

                    Next rowIndex
                End If


                ' ''


                ' ''
                'SetRowDataFreq()


                Dim conndel As MySqlConnection = New MySqlConnection(constr)
                conndel.Open()

                If String.IsNullOrEmpty(txtRcno.Text) = False And (txtRcno.Text) <> "0" Then
                    'Delete
                    Dim commandFreqDel As MySqlCommand = New MySqlCommand
                    commandFreqDel.CommandType = CommandType.Text

                    Dim qrydel As String = "DELETE from tblservicecontractfrequency where SourceSQLId = @SourceSQLId"

                    commandFreqDel.CommandText = qrydel
                    commandFreqDel.Parameters.Clear()

                    commandFreqDel.Parameters.AddWithValue("@SourceSQLId", Convert.ToInt32(txtRcno.Text))

                    commandFreqDel.Connection = conndel
                    commandFreqDel.ExecuteNonQuery()

                    conndel.Close()
                End If
                'Delete


                Dim tableFreqEdit As DataTable = TryCast(ViewState("CurrentTableFreq"), DataTable)

                If tableFreqEdit IsNot Nothing Then

                    For rowIndexFreq As Integer = 0 To tableFreqEdit.Rows.Count - 1
                        'string txSpareId = row.ItemArray[0] as string;
                        Dim TextBoxSeqNo As TextBox = CType(grvFreqDetails.Rows(rowIndexFreq).Cells(0).FindControl("txtSeqNoGV"), TextBox)

                        If (String.IsNullOrEmpty(TextBoxSeqNo.Text) = False) Then

                            Dim TextBoxFreqMTD As DropDownList = CType(grvFreqDetails.Rows(rowIndexFreq).Cells(1).FindControl("ddlFreqMTDGV"), DropDownList)
                            Dim TextBoxMonthNo As TextBox = CType(grvFreqDetails.Rows(rowIndexFreq).Cells(2).FindControl("txtMonthNoGV"), TextBox)
                            Dim TextBoxDayNo As TextBox = CType(grvFreqDetails.Rows(rowIndexFreq).Cells(3).FindControl("txtDayNoGV"), TextBox)
                            Dim TextBoxWeekNo As TextBox = CType(grvFreqDetails.Rows(rowIndexFreq).Cells(4).FindControl("txtWeekNoGV"), TextBox)
                            Dim TextBoxWeekDOW As DropDownList = CType(grvFreqDetails.Rows(rowIndexFreq).Cells(5).FindControl("ddlWeekDOWGV"), DropDownList)
                            Dim TextBoxLocation As DropDownList = CType(grvFreqDetails.Rows(rowIndexFreq).Cells(6).FindControl("ddlLocationGV"), DropDownList)
                            Dim TextBoxBranchID As DropDownList = CType(grvFreqDetails.Rows(rowIndexFreq).Cells(7).FindControl("ddlBranchIDGV"), DropDownList)
                            Dim TextBoxContractNo As TextBox = CType(grvFreqDetails.Rows(rowIndexFreq).Cells(8).FindControl("txtContractNoGVF"), TextBox)
                            Dim TextBoxSourceSQLID As TextBox = CType(grvFreqDetails.Rows(rowIndexFreq).Cells(9).FindControl("txtSourceSQLIDGVF"), TextBox)
                            Dim TextBoxRcno As TextBox = CType(grvFreqDetails.Rows(rowIndexFreq).Cells(10).FindControl("txtRcnoGVF"), TextBox)



                            Dim conn As MySqlConnection = New MySqlConnection(constr)
                            conn.Open()


                            Dim commandFreq As MySqlCommand = New MySqlCommand
                            commandFreq.CommandType = CommandType.Text

                            Dim qry As String = "INSERT INTO tblservicecontractfrequency(ContractNo, SeqNo, FreqMTD, DayNo, WeekNo, WeekDow, MonthNo, Location, BranchId,SourceSQLId,"
                            qry = qry + " CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn)"

                            qry = qry + " VALUES(@ContractNo, @SeqNo, @FreqMTD, @DayNo, @WeekNo, @WeekDow, @MonthNo, @Location, @BranchId, @SourceSQLId,"
                            qry = qry + "@CreatedBy, @CreatedOn, @LastModifiedBy, @LastModifiedOn)"


                            commandFreq.CommandText = qry
                            commandFreq.Parameters.Clear()

                            commandFreq.Parameters.AddWithValue("@ContractNo", txtContractNo.Text.Trim)
                            commandFreq.Parameters.AddWithValue("@FreqMTD", TextBoxFreqMTD.Text.Trim)
                            commandFreq.Parameters.AddWithValue("@SourceSQLId", txtSourceSQLID.Text)
                            commandFreq.Parameters.AddWithValue("@SeqNo", TextBoxSeqNo.Text.Trim)

                            If String.IsNullOrEmpty(TextBoxDayNo.Text.Trim) = True Then
                                commandFreq.Parameters.AddWithValue("@DayNo", 0)
                            Else
                                commandFreq.Parameters.AddWithValue("@DayNo", TextBoxDayNo.Text.Trim)
                            End If

                            'If String.IsNullOrEmpty(TextBoxDayNo.Text.Trim) = True Then
                            '    commandFreq.Parameters.AddWithValue("@WeekNo", 0)
                            'Else
                            '    commandFreq.Parameters.AddWithValue("@WeekNo", TextBoxWeekNo.Text.Trim)
                            'End If

                            If String.IsNullOrEmpty(TextBoxMonthNo.Text.Trim) = True Then
                                commandFreq.Parameters.AddWithValue("@MonthNo", 0)
                            Else
                                commandFreq.Parameters.AddWithValue("@MonthNo", TextBoxMonthNo.Text.Trim)
                            End If


                            commandFreq.Parameters.AddWithValue("@WeekNo", TextBoxWeekNo.Text.Trim)
                            commandFreq.Parameters.AddWithValue("@WeekDow", TextBoxWeekDOW.Text.Trim)
                            'commandFreq.Parameters.AddWithValue("@MonthNo", TextBoxMonthNo.Text.Trim)
                            commandFreq.Parameters.AddWithValue("@Location", TextBoxLocation.Text.Trim)
                            commandFreq.Parameters.AddWithValue("@BranchId", TextBoxBranchID.Text.Trim)

                            commandFreq.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                            commandFreq.Parameters.AddWithValue("@CreatedOn", DateAndTime.Now)
                            commandFreq.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                            commandFreq.Parameters.AddWithValue("@LastModifiedOn", DateAndTime.Now)

                            commandFreq.Connection = conn
                            commandFreq.ExecuteNonQuery()

                            'End If
                            'conn.Close()


                        End If
                    Next rowIndexFreq
                End If
                MessageBox.Message.Alert(Page, "Record Updated successfully!!!", "str")
                GridView1.DataSourceID = "SqlDSContractDet"
            End If

            UpdateContractHeader()
            mode = "save" & txtMode.Text
            btnADD_Click(sender, e)
            txtTotalRecords.Text = GridView1.Rows.Count
            'If Convert.ToInt32(txtTotalRecords.Text) > 0 Then
            '    btnServiceSchedule.Enabled = True
            'Else
            '    btnServiceSchedule.Enabled = False
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UpdateContractHeader()
        'Dim conn1 As MySqlConnection = New MySqlConnection(constr)
        'conn1.Open()

        'Dim command1 As MySqlCommand = New MySqlCommand

        'command1.CommandType = CommandType.Text

        'command1.CommandText = "SELECT * FROM tblcontractdet where ContractNo ='" & txtContractNo.Text & "'"
        'command1.Connection = conn1

        'Dim drservicecontractDet As MySqlDataReader = command1.ExecuteReader()
        'Dim dtservicecontractDet As New DataTable
        'dtservicecontractDet.Load(drservicecontractDet)

        'Dim notes As String = "", totalValue, totalNoService, maxNoService, perServiceValue, totalServiceAmt, discAmt, agreeValue As Decimal
        'totalValue = 0
        'totalNoService = 0
        'perServiceValue = 0
        'maxNoService = 0
        'totalServiceAmt = 0

        'For i = 0 To dtservicecontractDet.Rows.Count - 1
        '    totalValue = totalValue + Val(dtservicecontractDet.Rows(i)("Value").ToString())
        '    totalNoService = totalNoService + Val(dtservicecontractDet.Rows(i)("NoService").ToString())
        '    totalServiceAmt = totalServiceAmt + CDec(Val(dtservicecontractDet.Rows(i)("Value").ToString())) * CDec(Val(dtservicecontractDet.Rows(i)("NoService").ToString()))
        '    If Val(dtservicecontractDet.Rows(i)("NoService").ToString()) > maxNoService Then maxNoService = Val(dtservicecontractDet.Rows(i)("NoService").ToString())

        '    'totalServiceAmt = totalServiceAmt + CDec(Val(GetDsValue(ds, "Value", i))) * CDec(Val(GetDsValue(ds, "NoService", i)))
        '    'If Val(GetDsValue(ds, "NoService", i)) > maxNoService Then maxNoService = Val(GetDsValue(ds, "NoService", i))

        '    'totalValue = totalValue + Val(GetDsValue(ds, "Value", i))
        '    'totalNoService = totalNoService + Val(GetDsValue(ds, "NoService", i))
        '    'totalServiceAmt = totalServiceAmt + CDec(Val(GetDsValue(ds, "Value", i))) * CDec(Val(GetDsValue(ds, "NoService", i)))
        '    'If Val(GetDsValue(ds, "NoService", i)) > maxNoService Then maxNoService = Val(GetDsValue(ds, "NoService", i))
        'Next i

        ''If maxNoService <> 0 Then perServiceValue = Math.Round(totalServiceAmt / maxNoService, 2)

        'If totalNoService > 0 Then
        '    perServiceValue = Math.Round(totalServiceAmt / totalNoService, 2)
        'End If

        'discAmt = Val(txtDiscAmt.Text)
        'agreeValue = totalServiceAmt - discAmt
        'agreeValue = Math.Round(agreeValue, 2)
        'totalNoService = Math.Round(totalNoService, 0)
        'totalServiceAmt = Math.Round(totalServiceAmt, 2)



        ''Update
        'Dim commandUpdateDetail As MySqlCommand = New MySqlCommand

        'commandUpdateDetail.CommandType = CommandType.StoredProcedure
        'commandUpdateDetail.CommandText = "UpdateContractFromContractDetail"
        'commandUpdateDetail.Parameters.Clear()

        'commandUpdateDetail.Parameters.AddWithValue("@pr_ContractNo", txtContractNo.Text.Trim)
        'commandUpdateDetail.Parameters.AddWithValue("@pr_totalNoService", totalNoService)
        'commandUpdateDetail.Parameters.AddWithValue("@pr_totalServiceAmt", totalServiceAmt)
        'commandUpdateDetail.Parameters.AddWithValue("@pr_perServiceValue", perServiceValue)
        'commandUpdateDetail.Parameters.AddWithValue("@pr_agreeValue", agreeValue)
        'commandUpdateDetail.Parameters.AddWithValue("@pr_agreeValueContract", Convert.ToDecimal(txtAgreedValue.Text))
        'commandUpdateDetail.Connection = conn1
        'commandUpdateDetail.ExecuteScalar()
        'conn1.Close()

        ''Update
        ''cls00Connection.UpdateData("update m24Contract set ServiceNo=" & totalNoService & " where (ServiceNo is null or ServiceNo=0)  and ContractNo='" & cls00Regional.String_Filter(pContractNo) & "'")
        ''cls00Connection.UpdateData("update m24Contract set ServiceAmt=" & totalServiceAmt & " where (ServiceAmt is null or ServiceAmt=0)  and ContractNo='" & cls00Regional.String_Filter(pContractNo) & "'")
        ''cls00Connection.UpdateData("update m24Contract set ContractValue=" & totalServiceAmt & " where (ContractValue is null or ContractValue=0)  and ContractNo='" & cls00Regional.String_Filter(pContractNo) & "'")
        ''cls00Connection.UpdateData("update m24Contract set PerServiceValue=" & perServiceValue & " where (PerServiceValue is null or PerServiceValue=0)  and ContractNo='" & cls00Regional.String_Filter(pContractNo) & "'")
        ''cls00Connection.UpdateData("update m24Contract set AgreeValue=" & agreeValue & " where  ContractNo='" & cls00Regional.String_Filter(pContractNo) & "' and (AgreeValue is null or AgreeValue =0)")

        ''dsTemp = cls00Connection.getDataSet("select sum(NoService) as TotalNoService from m24ContractDet where ContractNo=N'" & cls00Regional.String_Filter(pContractNo) & "'")
        ''If RowCount(dsTemp) > 0 Then
        ''    cls00Connection.UpdateData("update m24Contract set ServiceNo=" & Val(GetDsValue(dsTemp, "TotalNoService")) & "   where ContractNo=N'" & cls00Regional.String_Filter(pContractNo) & "'")
        ''End If


        ''conn.Close()
    End Sub

    Public Sub MakeMeNull()
        txtNoofSvcInterval.Text = 1
        txtFrequency.Text = ""
        txtFrequencyDesc.Text = ""
        txtServiceId.Text = ""
        txtServiceDesc.Text = ""
        txtRecordDeleted.Text = "N"
        'txtContactPerson.Text = ""
        'txtContactPersonName.Text = ""
        txtServiceNotes.Text = ""
        txtNoofInterval.Text = "0"
        txtNoService.Text = "0"
        txtSourceSQLID.Text = "0"
        txtValue.Text = "0.00"
        txtValuePerService.Text = "0.00"
        txtDuplicateTarget.Text = "N"

        txtMonthByWhichMonth.Text = ""
        txtDOWByWhichWeek.Text = ""

        FirstGridViewRowTarget()
        FirstGridViewRowFreq()
        Dim Query As String
        Dim TextBoxTargetDesc As DropDownList = CType(grvTargetDetails.Rows(0).Cells(0).FindControl("ddlTargetDescGV"), DropDownList)
        Query = "Select * from tblTarget"

        grvFreqDetails.Enabled = False
        grvTargetDetails.Enabled = False
        PopulateDropDownList(Query, "descrip1", "descrip1", TextBoxTargetDesc)
    End Sub

    Public Sub PopulateDropDownList(ByVal query As String, ByVal textField As String, ByVal valueField As String, ByVal ddl As DropDownList)
        Using con As New MySqlConnection(constr)
            Using cmd As New MySqlCommand(query)
                cmd.CommandType = CommandType.Text
                cmd.Connection = con
                con.Open()
                ddl.DataSource = cmd.ExecuteReader()
                ddl.DataTextField = textField
                ddl.DataValueField = valueField
                ddl.DataBind()
                con.Close()
            End Using
        End Using
    End Sub



    Protected Sub gvClient_SelectedIndexChanged(sender As Object, e As EventArgs)

        'If (gvClient.SelectedRow.Cells(2).Text = "&nbsp") Then
        '    txtClient.Text = ""
        'Else
        '    txtClient.Text = gvClient.SelectedRow.Cells(2).Text.Trim
        'End If

        'If (gvClient.SelectedRow.Cells(3).Text = "&nbsp") Then
        '    txtCustName.Text = ""
        'Else
        '    txtCustName.Text = gvClient.SelectedRow.Cells(3).Text.Trim
        'End If

        'If (gvClient.SelectedRow.Cells(4).Text = "&nbsp") Then
        '    txtContactPerson.Text = ""
        'Else
        '    txtContactPerson.Text = gvClient.SelectedRow.Cells(4).Text.Trim
        'End If

        'If (gvClient.SelectedRow.Cells(5).Text = "&nbsp") Then
        '    txtTelephone.Text = ""
        'Else
        '    txtTelephone.Text = gvClient.SelectedRow.Cells(5).Text.Trim
        'End If
        'Dim addr1 As String = ""
        'Dim addr2 As String = ""
        'Dim addr3 As String = ""

        'If (gvClient.SelectedRow.Cells(6).Text = "&nbsp") Then
        '    addr1 = ""
        'Else
        '    addr1 = gvClient.SelectedRow.Cells(6).Text.Trim
        'End If

        'If (gvClient.SelectedRow.Cells(7).Text = "&nbsp") Then
        '    addr2 = ""
        'ElseIf (gvClient.SelectedRow.Cells(7).Text.Contains("&#39")) Then 'sometimes the value is S'pore where single quotes asc value is &#39
        '    addr2 = gvClient.SelectedRow.Cells(7).Text.Trim.Replace("&#39", "'")
        'Else
        '    addr2 = gvClient.SelectedRow.Cells(7).Text.Trim
        'End If

        'If (gvClient.SelectedRow.Cells(8).Text = "&nbsp") Then
        '    addr3 = ""
        'Else
        '    addr3 = gvClient.SelectedRow.Cells(8).Text.Trim
        'End If


        'txtServAddress.Text = addr1 + " " + addr2 + " " + addr3

        'If (gvClient.SelectedRow.Cells(9).Text = "&nbsp") Then
        '    txtFax.Text = ""
        'Else
        '    txtFax.Text = gvClient.SelectedRow.Cells(9).Text.Trim
        'End If

        'If (gvClient.SelectedRow.Cells(10).Text = "&nbsp") Then
        '    txtConPerMobile.Text = ""
        'Else
        '    txtConPerMobile.Text = gvClient.SelectedRow.Cells(10).Text.Trim
        'End If

        'If (gvClient.SelectedRow.Cells(11).Text = "&nbsp") Then
        '    txtPostal.Text = ""
        'Else
        '    txtPostal.Text = gvClient.SelectedRow.Cells(11).Text.Trim
        'End If

        ''also scheduler and salesman from tblstaff should match selected client from tblcontactmaster
    End Sub

    Private Sub DisableButton(disena As Boolean)
        btnSave.Enabled = disena
        btnADD.Enabled = disena
        btnDelete.Enabled = disena
        btnServiceSchedule.Enabled = disena
        btnBack.Enabled =disena
            btnQuit.Enabled = disena
    End Sub
    Protected Sub btnServiceSchedule_Click(sender As Object, e As EventArgs) Handles btnServiceSchedule.Click
        Try

            If GridView1.Rows.Count = 0 Then
                'DisableButton(True)
                Dim message As String = "alert('Please Enter Contract Detail..')"
                ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)
                Exit Sub
            End If

            Label1.Visible = True
            'Label1.Text = "Name :"

            'DisableButton(False)

            Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            Dim conn As MySqlConnection = New MySqlConnection(constr)
            conn.Open()

            Dim cmdContractDet As MySqlCommand = New MySqlCommand

            cmdContractDet.CommandType = CommandType.Text

            cmdContractDet.CommandText = "SELECT * FROM tblcontractdet where ContractNo ='" & txtContractNo.Text & "'"
            cmdContractDet.Connection = conn

            Dim drservicecontractDet As MySqlDataReader = cmdContractDet.ExecuteReader()
            Dim dtservicecontractDet As New DataTable
            dtservicecontractDet.Load(drservicecontractDet)


            'If dtservicecontractDet.Rows.Count = 0 Then
            '    'MessageBox.Message.Alert(Page, "Please Enter Contract Detail..", "str")
            '    Dim message As String = "alert('Please Enter Contract Detail..')"
            '    ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)
            '    Exit Sub
            'End If

            ''''
            For i = 0 To dtservicecontractDet.Rows.Count - 1

                '''''''''''
                Dim cmdFreq As MySqlCommand = New MySqlCommand
                cmdFreq.CommandType = CommandType.Text

                cmdFreq.CommandText = "SELECT * FROM tblservicecontractFrequency where SourceSQLID= " & dtservicecontractDet.Rows(i)("OrigCode").ToString
                cmdFreq.Connection = conn

                Dim drtblservicecontractFrequency As MySqlDataReader = cmdFreq.ExecuteReader()
                Dim dttblservicecontractFrequency As New DataTable
                dttblservicecontractFrequency.Load(drtblservicecontractFrequency)

                'noOfInterval = Val(dtservicecontractDet.Rows(i)("NoofInterval").ToString())
                'gSQLFreq = dttblservicecontractFrequency.Rows(0)("Rcno").ToString()

                ''''''''''''
                'dsFreq = cls00Connection.getDataSet("select * from  m24servicecontractFrequency where SourceSQLID=" & GetDsValue(dsDet, "code", i))
                If dttblservicecontractFrequency.Rows.Count <= 0 Then Continue For

                If dtservicecontractDet.Rows(i)("Frequency").ToString() = "WEEKLY" Or dtservicecontractDet.Rows(i)("Frequency").ToString() = "TWICE-WEEKLY" Or dtservicecontractDet.Rows(i)("Frequency").ToString() = "THRICE-WEEKLY" Or dtservicecontractDet.Rows(i)("Frequency").ToString() = "FOUR-TIMES-WEEKLY" Or dtservicecontractDet.Rows(i)("Frequency").ToString() = "FIVE-TIMES-WEEKLY" Or dtservicecontractDet.Rows(i)("Frequency").ToString() = "SIX-TIMES-WEEKLY" Then

                    For j = 0 To dttblservicecontractFrequency.Rows.Count - 1
                        If dttblservicecontractFrequency.Rows(j)("WEEKDOW").ToString() = "" Or dttblservicecontractFrequency.Rows(j)("WEEKDOW").ToString() = "-1" Or String.IsNullOrEmpty(dttblservicecontractFrequency.Rows(j)("WEEKDOW").ToString()) = True Then
                            'MsgBox("Please setup Frequency (WeekDOW) for " & dtservicecontractDet.Rows(i)("Frequency").ToString())

                            'Dim message As String = "alert('Please Enter Contract Detail..')"
                            'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)

                            Dim message As String = "alert('Please setup Frequency (WeekDOW) for " & dtservicecontractDet.Rows(i)("Frequency").ToString() & " ')"
                            ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)
                            Exit Sub
                        End If
                    Next

                End If

                If dtservicecontractDet.Rows(i)("Frequency").ToString() = "BI-WEEKLY" Or dtservicecontractDet.Rows(i)("Frequency").ToString() = "TRI-WEEKLY" Or dtservicecontractDet.Rows(i)("Frequency").ToString() = "FORTNIGHTLY" Then
                    For j = 0 To dttblservicecontractFrequency.Rows.Count - 1
                        If dttblservicecontractFrequency.Rows(j)("WeekDOW").ToString() = "" Or Val(dttblservicecontractFrequency.Rows(j)("WeekNo").ToString()) = 0 Then
                            ''MsgBox("Please setup Frequency (WeekDOW and WeekNo) for " & dtservicecontractDet.Rows(i)("Frequency").ToString())
                            Dim message As String = "alert('Please setup Frequency (WeekDOW and WeekNo) for " & dtservicecontractDet.Rows(i)("Frequency").ToString() & " ')"
                            ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)
                            Exit Sub
                        End If
                    Next

                End If

                If dtservicecontractDet.Rows(i)("Frequency").ToString() = "MONTHLY" Then
                    For j = 0 To dttblservicecontractFrequency.Rows.Count - 1
                        If (dttblservicecontractFrequency.Rows(j)("WeekDOW").ToString() = "-1" Or Val(dttblservicecontractFrequency.Rows(j)("WeekNo").ToString()) = 0) And Val(dttblservicecontractFrequency.Rows(j)("Dayno").ToString()) = 0 Then
                            'MsgBox("Please setup Frequency ((WeekDOW and WeekNo) / MonthNo) for " & dtservicecontractDet.Rows(i)("Frequency").ToString())
                            Dim message As String = "alert('Please setup Frequency ((WeekDOW and WeekNo) / MonthNo) for " & dtservicecontractDet.Rows(i)("Frequency").ToString() & " ')"
                            ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)
                            Exit Sub
                        End If
                    Next

                End If

                If dtservicecontractDet.Rows(i)("Frequency").ToString() = "BI-MONTHLY" Or dtservicecontractDet.Rows(i)("Frequency").ToString() = "QUARTERLY" Or dtservicecontractDet.Rows(i)("Frequency").ToString() = "HALF-ANNUALLY" Or dtservicecontractDet.Rows(i)("Frequency").ToString() = "ANNUALLY" Or dtservicecontractDet.Rows(i)("Frequency").ToString() = "BI-ANNUALLY" Then
                    For j = 0 To dttblservicecontractFrequency.Rows.Count - 1
                        '(GetDsValue(dsFreq, "WeekDOW", j).Trim = "" Or Val(GetDsValue(dsFreq, "Weekno", j)) = 0) And (Val(GetDsValue(dsFreq, "DayNo", j)) = 0 Or Val(GetDsValue(dsFreq, "MonthNo", j)) = 0) Then
                        If (dttblservicecontractFrequency.Rows(j)("WeekDOW").ToString() = "" Or Val(dttblservicecontractFrequency.Rows(j)("WeekNo").ToString()) = 0) And (Val(dttblservicecontractFrequency.Rows(j)("MonthNo").ToString()) = 0 Or Val(dttblservicecontractFrequency.Rows(j)("DayNo").ToString()) = 0) Then
                            'MsgBox("Please setup Frequency ((WeekDOW and WeekNo) / (DayNo and MonthNo) for " & dtservicecontractDet.Rows(i)("Frequency").ToString())
                            Dim message As String = "alert('Please setup Frequency ((WeekDOW and WeekNo) / (DayNo and MonthNo)) for " & dtservicecontractDet.Rows(i)("Frequency").ToString() & " ')"
                            ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)
                            Exit Sub
                        End If
                    Next

                End If


            Next

            Session("serviceschedulefrom") = "contractdetail"

            If String.IsNullOrEmpty(txtContractNo.Text) = False Then
                Session("contractno") = txtContractNo.Text
                Session("contractdate") = txtContractDate.Text
                Session("contracttype") = txtContactType.Text
                Session("client") = txtClient.Text
                Session("custname") = txtCustName.Text

                Session("contact") = txtContact.Text
                Session("servstart") = txtServStart.Text
                Session("servend") = txtServEnd.Text
                Session("billingfreq") = txtBillingFreq.Text

                Session("billingamount") = txtBillingAmount.Text
                Session("agreevalue") = txtAgreedValue.Text

            End If


            System.Threading.Thread.Sleep(3000)
            Response.Redirect("ServiceSchedule.aspx")
            DisableButton(True)

        Catch ex As Exception
            DisableButton(True)

            Dim exstr As String
            exstr = ex.ToString
            Dim message As String = "alert('" + exstr + "')"
            ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)

            'MessageBox.Message.Alert(Page, ex.ToString, "str")
        End Try
    End Sub

    Private Sub fGetInterval()
        lblInterval.Text = ""
        Select Case txtFrequency.Text.ToUpper
            Case "DAILY"
                lblInterval.Text = "Days"
            Case "WEEKLY", "TWICE-WEEKLY"
                lblInterval.Text = "Wks"
            Case "BI-WEEKLY", "FORTNIGHTLY"
                lblInterval.Text = "Bi-wks"
            Case "TRI-WEEKLY"
                lblInterval.Text = "Tri-wks"
            Case "MONTHLY", "THRICE-MONTHLY"
                lblInterval.Text = "Mths"
            Case "BI-MONTHLY"
                lblInterval.Text = "Bi-mths"
            Case "QUARTERLY"
                lblInterval.Text = "Quarterly"
            Case "HALF-ANNUALLY"
                lblInterval.Text = "Half-Annually"
            Case "ANNUALLY"
                lblInterval.Text = "Annually"
            Case "BI-ANNUALLY"
                lblInterval.Text = "Bi-Annually"
        End Select
    End Sub
   

    Private Function pWeekNumber(ByVal pDate As Date) As Integer
        pWeekNumber = 0
        Dim temp As String = Math.Round((pDate.Day - 1) / 7, 1)
        If temp.Contains(".") Then temp = temp.Substring(0, temp.IndexOf("."))
        pWeekNumber = 1 + temp
    End Function

    Protected Sub gvService_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvService.SelectedIndexChanged
        If gvService.SelectedRow.Cells(1).Text = "&nbsp" Then
            txtServiceId.Text = " "
        Else
            txtServiceId.Text = gvService.SelectedRow.Cells(1).Text
        End If

        If gvService.SelectedRow.Cells(2).Text = "&nbsp" Then
            txtServiceDesc.Text = " "
        Else
            txtServiceDesc.Text = gvService.SelectedRow.Cells(2).Text
        End If

        'If gvTeam.SelectedRow.Cells(3).Text = "&nbsp" Then
        '    txtTeamIncharge.Text = " "
        'Else
        '    txtTeamIncharge.Text = gvTeam.SelectedRow.Cells(3).Text
        'End If
    End Sub

   

    'Grid View



    Private Sub FirstGridViewRowTarget()
        Try
            Dim dt As New DataTable()
            Dim dr As DataRow = Nothing
            'dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));

            dt.Columns.Add(New DataColumn("TargetDesc", GetType(String)))
            dt.Columns.Add(New DataColumn("TargetId", GetType(String)))
            dt.Columns.Add(New DataColumn("ContractNo", GetType(String)))

            dt.Columns.Add(New DataColumn("ServiceID", GetType(String)))
            dt.Columns.Add(New DataColumn("SourceSQLID", GetType(String)))
            dt.Columns.Add(New DataColumn("Rcno", GetType(String)))

            dr = dt.NewRow()

            dr("TargetDesc") = String.Empty
            dr("TargetId") = String.Empty
            dr("ContractNo") = String.Empty

            dr("ServiceID") = String.Empty
            dr("SourceSQLID") = 0
            dr("Rcno") = 0
            dt.Rows.Add(dr)

            ViewState("CurrentTable") = dt

            grvTargetDetails.DataSource = dt
            grvTargetDetails.DataBind()

            Dim btnAdd As Button = CType(grvTargetDetails.FooterRow.Cells(1).FindControl("btnAddDetail"), Button)
            Page.Form.DefaultFocus = btnAdd.ClientID

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewRow()
        Try
            Dim rowIndex As Integer = 0
            Dim Query As String

            If ViewState("CurrentTable") IsNot Nothing Then
                Dim dtCurrentTable As DataTable = CType(ViewState("CurrentTable"), DataTable)
                Dim drCurrentRow As DataRow = Nothing
                If dtCurrentTable.Rows.Count > 0 Then
                    For i As Integer = 1 To dtCurrentTable.Rows.Count

                        Dim TextBoxTargetDesc As DropDownList = CType(grvTargetDetails.Rows(rowIndex).Cells(0).FindControl("ddlTargetDescGV"), DropDownList)
                        Dim TextBoxTargtId As TextBox = CType(grvTargetDetails.Rows(rowIndex).Cells(1).FindControl("txtTargtIdGV"), TextBox)
                        Dim TextBoxContractNo As TextBox = CType(grvTargetDetails.Rows(rowIndex).Cells(2).FindControl("txtContractNoGV"), TextBox)
                        Dim TextBoxServiceID As TextBox = CType(grvTargetDetails.Rows(rowIndex).Cells(3).FindControl("txtServiceIDGV"), TextBox)
                        Dim TextBoxSourceSQLID As TextBox = CType(grvTargetDetails.Rows(rowIndex).Cells(4).FindControl("txtSourceSQLIDGV"), TextBox)
                        Dim TextBoxRcno As TextBox = CType(grvTargetDetails.Rows(rowIndex).Cells(5).FindControl("txtRcnoGV"), TextBox)
                        drCurrentRow = dtCurrentTable.NewRow()

                        drCurrentRow("TargetID") = ""
                        drCurrentRow("ServiceID") = txtServiceId.Text
                        drCurrentRow("ContractNo") = txtContact.Text
                        drCurrentRow("Rcno") = 0

                        dtCurrentTable.Rows(i - 1)("TargetDesc") = TextBoxTargetDesc.SelectedValue
                        dtCurrentTable.Rows(i - 1)("TargetId") = TextBoxTargtId.Text
                        dtCurrentTable.Rows(i - 1)("ContractNo") = TextBoxContractNo.Text
                        dtCurrentTable.Rows(i - 1)("ServiceID") = TextBoxServiceID.Text
                        dtCurrentTable.Rows(i - 1)("SourceSQLID") = TextBoxSourceSQLID.Text
                        dtCurrentTable.Rows(i - 1)("Rcno") = TextBoxRcno.Text
                        rowIndex += 1

                    Next i
                    dtCurrentTable.Rows.Add(drCurrentRow)
                    ViewState("CurrentTable") = dtCurrentTable

                    grvTargetDetails.DataSource = dtCurrentTable
                    grvTargetDetails.DataBind()

                    Dim rowIndex2 As Integer = 0
                    Dim j As Integer = 1
                    Do While j <= (rowIndex)

                        Dim TextBoxTargetDesc1 As DropDownList = CType(grvTargetDetails.Rows(rowIndex2).Cells(0).FindControl("ddlTargetDescGV"), DropDownList)
                        Query = "Select * from tblTarget"
                        PopulateDropDownList(Query, "descrip1", "descrip1", TextBoxTargetDesc1)

                        'Dim i2 As Integer = objCommon.PopulateDropDown(CType(grvTargetDetails.Rows(rowIndex2).Cells(0).FindControl("ddlSpareIdGV"), DropDownList), "Select SpareId, SpareDesc from spare where VATRate > 0.00 and  CompId=" & Convert.ToString(HttpContext.Current.Session("CompId")) & " and BranchId=" & Convert.ToString(HttpContext.Current.Session("BranchID")) & " order by SpareDesc", "SpareDesc", "SpareId")
                        rowIndex2 += 1
                        j += 1
                    Loop

                    Dim TextBoxTargetDesc2 As DropDownList = CType(grvTargetDetails.Rows(rowIndex).Cells(0).FindControl("ddlTargetDescGV"), DropDownList)
                    Query = "Select TargetId, descrip1 from tblTarget"
                    PopulateDropDownList(Query, "descrip1", "descrip1", TextBoxTargetDesc2)
                    'Dim i5 As Integer = objCommon.PopulateDropDown(CType(grvTargetDetails.Rows(rowIndex).Cells(0).FindControl("ddlSpareIdGV"), DropDownList), "Select * from Spare where  VATRate > 0.00 and  CompId=" & Convert.ToString(HttpContext.Current.Session("CompId")) & " and BranchId=" & Convert.ToString(HttpContext.Current.Session("BranchID")) & " order by SpareDesc", "SpareDesc", "SpareId")


                End If
            Else
                Response.Write("ViewState is null")
            End If
            SetPreviousData()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub AddNewRowWithDetailRec()
        Try
            Dim rowIndex As Integer = 0
            Dim Query As String
            If ViewState("CurrentTable") IsNot Nothing Then
                Dim dtCurrentTable As DataTable = CType(ViewState("CurrentTable"), DataTable)
                Dim drCurrentRow As DataRow = Nothing
                If TotDetailRecords > 0 Then
                    For i As Integer = 1 To (dtCurrentTable.Rows.Count)


                        Dim TextBoxTargetDesc As DropDownList = CType(grvTargetDetails.Rows(rowIndex).Cells(0).FindControl("ddlTargetDescGV"), DropDownList)
                        Dim TextBoxTargtId As TextBox = CType(grvTargetDetails.Rows(rowIndex).Cells(1).FindControl("txtTargtIdGV"), TextBox)
                        Dim TextBoxContractNo As TextBox = CType(grvTargetDetails.Rows(rowIndex).Cells(2).FindControl("txtContractNoGV"), TextBox)
                        Dim TextBoxServiceID As TextBox = CType(grvTargetDetails.Rows(rowIndex).Cells(3).FindControl("txtServiceIDGV"), TextBox)
                        Dim TextBoxSourceSQLID As TextBox = CType(grvTargetDetails.Rows(rowIndex).Cells(4).FindControl("txtSourceSQLIDGV"), TextBox)
                        Dim TextBoxRcno As TextBox = CType(grvTargetDetails.Rows(rowIndex).Cells(5).FindControl("txtRcnoGV"), TextBox)
                        drCurrentRow = dtCurrentTable.NewRow()

                        drCurrentRow("TargetID") = ""
                        drCurrentRow("ServiceID") = txtServiceId.Text
                        drCurrentRow("ContractNo") = txtContact.Text
                        drCurrentRow("Rcno") = 0

                        dtCurrentTable.Rows(i - 1)("TargetDesc") = TextBoxTargetDesc.SelectedValue
                        dtCurrentTable.Rows(i - 1)("TargetId") = TextBoxTargtId.Text
                        dtCurrentTable.Rows(i - 1)("ContractNo") = TextBoxContractNo.Text
                        dtCurrentTable.Rows(i - 1)("ServiceID") = TextBoxServiceID.Text
                        dtCurrentTable.Rows(i - 1)("SourceSQLID") = TextBoxSourceSQLID.Text
                        dtCurrentTable.Rows(i - 1)("Rcno") = TextBoxRcno.Text
                        rowIndex += 1

                    Next i
                    dtCurrentTable.Rows.Add(drCurrentRow)
                    ViewState("CurrentTable") = dtCurrentTable

                    grvTargetDetails.DataSource = dtCurrentTable
                    grvTargetDetails.DataBind()


                    Dim rowIndex1 As Integer = 0

                    For j As Integer = 1 To (dtCurrentTable.Rows.Count)

                        Dim TextBoxTargetDesc1 As DropDownList = CType(grvTargetDetails.Rows(rowIndex1).Cells(0).FindControl("ddlTargetDescGV"), DropDownList)
                        Query = "Select * from tblTarget"
                        PopulateDropDownList(Query, "descrip1", "descrip1", TextBoxTargetDesc1)
                        'Dim i2 As Integer = objCommon.PopulateDropDown(CType(grvTargetDetails.Rows(rowIndex1).Cells(0).FindControl("ddlSpareIdGV"), DropDownList), "Select SpareId, SpareDesc from spare where  VATRate > 0.00 and  CompId=" & Convert.ToString(HttpContext.Current.Session("CompId")) & " and  BranchId=" & Convert.ToString(HttpContext.Current.Session("BranchID")) & "order by SpareDesc", "SpareDesc", "SpareId")

                        rowIndex1 += 1
                    Next j

                    Dim TextBoxTargetDesc2 As DropDownList = CType(grvTargetDetails.Rows(rowIndex).Cells(0).FindControl("ddlTargetDescGV"), DropDownList)
                    Query = "Select TargetId, descrip1 from tblTarget"
                    PopulateDropDownList(Query, "descrip1", "descrip1", TextBoxTargetDesc2)
                    'Dim i7 As Integer = objCommon.PopulateDropDown(CType(grvTargetDetails.Rows(rowIndex).Cells(1).FindControl("ddlSpareIdGV"), DropDownList), "Select * from Spare where  VATRate > 0.00 and   CompId=" & Convert.ToString(HttpContext.Current.Session("CompId")) & " and BranchId=" & Convert.ToString(HttpContext.Current.Session("BranchID")) & "order by SpareDesc", "SpareDesc", "SpareId")

                End If


            Else
                Response.Write("ViewState is null")
            End If
            SetPreviousData()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SetPreviousData()
        Try
            Dim rowIndex As Integer = 0

            Dim Query As String
            txtTargetDesc.Text = ""
            If ViewState("CurrentTable") IsNot Nothing Then
                Dim dt As DataTable = CType(ViewState("CurrentTable"), DataTable)
                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1

                        Dim TextBoxTargetDesc As DropDownList = CType(grvTargetDetails.Rows(rowIndex).Cells(0).FindControl("ddlTargetDescGV"), DropDownList)
                        Dim TextBoxTargtId As TextBox = CType(grvTargetDetails.Rows(rowIndex).Cells(1).FindControl("txtTargtIdGV"), TextBox)
                        Dim TextBoxContractNo As TextBox = CType(grvTargetDetails.Rows(rowIndex).Cells(2).FindControl("txtContractNoGV"), TextBox)
                        Dim TextBoxServiceID As TextBox = CType(grvTargetDetails.Rows(rowIndex).Cells(3).FindControl("txtServiceIDGV"), TextBox)
                        Dim TextBoxSourceSQLID As TextBox = CType(grvTargetDetails.Rows(rowIndex).Cells(4).FindControl("txtSourceSQLIDGV"), TextBox)
                        Dim TextBoxRcno As TextBox = CType(grvTargetDetails.Rows(rowIndex).Cells(5).FindControl("txtRcnoGV"), TextBox)

                        TextBoxTargetDesc.Text = dt.Rows(i)("TargetDesc").ToString()
                        TextBoxTargtId.Text = dt.Rows(i)("TargetId").ToString()
                        TextBoxContractNo.Text = dt.Rows(i)("ContractNo").ToString()
                        TextBoxServiceID.Text = dt.Rows(i)("ServiceID").ToString()
                        TextBoxSourceSQLID.Text = dt.Rows(i)("SourceSQLID").ToString()
                        TextBoxRcno.Text = dt.Rows(i)("Rcno").ToString()

                        'If (TextBoxRcno.Text <> "0") And (TextBoxRcno.Text <> "") And (String.IsNullOrEmpty(TextBoxRcno.Text) = False) Then
                        '    Dim TextBoxTargetDesc2 As DropDownList = CType(grvTargetDetails.Rows(rowIndex).Cells(1).FindControl("ddlTargetDescGV"), DropDownList)
                        '    Query = "Select * from tblTarget"
                        '    PopulateDropDownList(Query, "descrip1", "descrip1", TextBoxTargetDesc2)
                        'End If
                        
                        If (TextBoxTargtId.Text <> "0") And (TextBoxTargtId.Text <> "") And (String.IsNullOrEmpty(TextBoxTargtId.Text) = False) Then
                            Dim TextBoxTargetDesc2 As DropDownList = CType(grvTargetDetails.Rows(rowIndex).Cells(1).FindControl("ddlTargetDescGV"), DropDownList)
                            Query = "Select TargetId, descrip1 from tblTarget"
                            PopulateDropDownList(Query, "descrip1", "descrip1", TextBoxTargetDesc2)
                        End If

                        'If (TextBoxCatId.Text <> "0") AndAlso (TextBoxCatId.Text <> "") AndAlso (String.IsNullOrEmpty(TextBoxCatId.Text) = False) Then
                        '    Dim i1 As Integer = objCommon.PopulateDropDown(CType(grvTargetDetails.Rows(rowIndex).Cells(1).FindControl("ddlSpareIdGV"), DropDownList), "Select * from Spare where  VATRate > 0.00 and  CompId=" & Convert.ToString(HttpContext.Current.Session("CompId")) & " and BranchId=" & Convert.ToString(HttpContext.Current.Session("BranchID")) & " order by SpareDesc", "SpareDesc", "SpareId")
                        'End If


                        'txtTargetDesc.Text = txtTargetDesc.Text & dt.Rows(i)("TargetDesc").ToString()

                        If (String.IsNullOrEmpty(TextBoxTargetDesc.Text) = False) And (TextBoxTargetDesc.Text <> "0") And (TextBoxTargetDesc.Text <> "-1") Then

                            If dt.Rows.Count > 0 Then
                                If i < dt.Rows.Count - 2 Then
                                    txtTargetDesc.Text = txtTargetDesc.Text & dt.Rows(i)("TargetDesc").ToString() & ", "
                                Else
                                    txtTargetDesc.Text = txtTargetDesc.Text & dt.Rows(i)("TargetDesc").ToString()
                                End If
                                ''
                            End If
                        End If
                        rowIndex += 1
                    Next i
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SetRowData()
        Dim rowIndex As Integer = 0
        Try
            If ViewState("CurrentTable") IsNot Nothing Then
                Dim dtCurrentTable As DataTable = CType(ViewState("CurrentTable"), DataTable)
                Dim drCurrentRow As DataRow = Nothing
                If dtCurrentTable.Rows.Count > 0 Then
                    For i As Integer = 1 To dtCurrentTable.Rows.Count


                        Dim TextBoxTargetDesc As DropDownList = CType(grvTargetDetails.Rows(rowIndex).Cells(0).FindControl("ddlTargetDescGV"), DropDownList)
                        Dim TextBoxTargtId As TextBox = CType(grvTargetDetails.Rows(rowIndex).Cells(1).FindControl("txtTargtIdGV"), TextBox)
                        Dim TextBoxContractNo As TextBox = CType(grvTargetDetails.Rows(rowIndex).Cells(2).FindControl("txtContractNoGV"), TextBox)
                        Dim TextBoxServiceID As TextBox = CType(grvTargetDetails.Rows(rowIndex).Cells(3).FindControl("txtServiceIDGV"), TextBox)
                        Dim TextBoxSourceSQLID As TextBox = CType(grvTargetDetails.Rows(rowIndex).Cells(4).FindControl("txtSourceSQLIDGV"), TextBox)
                        Dim TextBoxRcno As TextBox = CType(grvTargetDetails.Rows(rowIndex).Cells(5).FindControl("txtRcnoGV"), TextBox)

                        drCurrentRow = dtCurrentTable.NewRow()

                        dtCurrentTable.Rows(i - 1)("TargetDesc") = TextBoxTargetDesc.Text
                        dtCurrentTable.Rows(i - 1)("TargetId") = TextBoxTargtId.Text
                        dtCurrentTable.Rows(i - 1)("ContractNo") = TextBoxContractNo.Text
                        dtCurrentTable.Rows(i - 1)("ServiceID") = TextBoxServiceID.Text
                        dtCurrentTable.Rows(i - 1)("SourceSQLID") = TextBoxSourceSQLID.Text
                        dtCurrentTable.Rows(i - 1)("Rcno") = TextBoxRcno.Text

                        rowIndex += 1
                    Next i

                    ViewState("CurrentTable") = dtCurrentTable


                End If
            Else
                Response.Write("ViewState is null")
            End If
            SetPreviousData()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Function HighlightDuplicate(ByVal gridview As GridView) As Boolean
        HasDuplicateTarget = False
        For currentRow As Integer = 0 To gridview.Rows.Count - 2
            Dim rowToCompare As GridViewRow = gridview.Rows(currentRow)

            For otherRow As Integer = currentRow + 1 To gridview.Rows.Count - 1
                Dim row As GridViewRow = gridview.Rows(otherRow)
                Dim duplicateRow As Boolean = False
                Dim TextBoxTargetDescGV As DropDownList = CType(grvTargetDetails.Rows(currentRow).Cells(0).FindControl("ddlTargetDescGV"), DropDownList)
                Dim TextBoxTargetDescGV1 As DropDownList = CType(grvTargetDetails.Rows(otherRow).Cells(0).FindControl("ddlTargetDescGV"), DropDownList)

                If ((TextBoxTargetDescGV.Text) = (TextBoxTargetDescGV1.Text)) Then

                    duplicateRow = True

                    TextBoxTargetDescGV.BackColor = Drawing.Color.LightCoral
                    TextBoxTargetDescGV1.BackColor = Drawing.Color.LightCoral

                    'TextBoxTargetDescGV.Font.Bold = True
                    'TextBoxTargetDescGV1.Font.Bold = True
                    'TextBoxTargetDescGV.BackColor = Drawing.Color.Purple
                    'TextBoxTargetDescGV1.BackColor = Drawing.Color.Purple

                    
                    'Dim custVal As New CustomValidator()
                    'custVal.IsValid = False
                    'custVal.ErrorMessage = ""
                    'custVal.ErrorMessage = "Selected Target Already Exists"
                    'custVal.EnableClientScript = True

                    'custVal.ValidationGroup = "VGroup"
                    'Me.Page.Form.Controls.Add(custVal)

                    HasDuplicateTarget = True
                    Return HasDuplicateTarget


                Else
                    duplicateRow = False
                    HasDuplicateTarget = False
                End If

            Next otherRow
        Next currentRow

        Return HasDuplicateTarget
    End Function



    Protected Sub ddlTargetDescGV_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try

            'If grvTargetDetails.PageSize >= 10 Then
            '    grvTargetDetails.PageSize = TotDetailRecords + 1
            '    'TotDetailRecordsForPaging = TotDetailRecordsForPaging + 1
            'End If



            Dim lTargetDesciption As String

            Dim ddl1 As DropDownList = DirectCast(sender, DropDownList)

            Dim xrow1 As GridViewRow = CType(ddl1.NamingContainer, GridViewRow)
            Dim lblid1 As DropDownList = CType(xrow1.FindControl("ddlTargetDescGV"), DropDownList)
            Dim lblid2 As TextBox = CType(xrow1.FindControl("txtTargtIdGV"), TextBox)


            lTargetDesciption = lblid1.Text

            Dim rowindex1 As Integer = xrow1.RowIndex

            'Get Targt Id

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()
            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.CommandText = "SELECT TargetId FROM tblTarget where descrip1= '" & lTargetDesciption & "'"
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then
                lblid2.Text = dt.Rows(0)("TargetId").ToString
            End If
            'HasDuplicateTarget = False
            'Dim HasDuplicate As Boolean
            HasDuplicateTarget = HighlightDuplicate(grvTargetDetails)

            txtDuplicateTarget.Text = "N"
            txtRecordAdded.Text = "N"

            If HasDuplicateTarget = True Then
                txtDuplicateTarget.Text = "Y"
                Dim message As String = "alert('Duplicate Target/Pests has been selected!!!')"
                ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)

                Exit Sub
            End If


            If rowindex1 = grvTargetDetails.Rows.Count - 1 Then
                btnAddDetail_Click(sender, e)
                txtRecordAdded.Text = "Y"
            End If
            'txtTargetDesc.Text = txtTargetDesc.Text & lblid1.Text & ", "

        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Protected Sub grvTargetDetails_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs) Handles grvTargetDetails.RowDeleting
        Try

            If txtRecordDeleted.Text = "Y" Then
                txtRecordDeleted.Text = "N"
                Exit Sub
            End If

            Dim confirmValue As String
            confirmValue = ""

            confirmValue = Request.Form("confirm_value")
            If Right(confirmValue, 3) = "Yes" Then

                txtRecordDeleted.Text = "N"
                Dim Query As String
                SetRowData()

                Dim dt As DataTable = CType(ViewState("CurrentTable"), DataTable)
                Dim drCurrentRow As DataRow = Nothing
                Dim rowIndex As Integer = Convert.ToInt32(e.RowIndex)



                Dim TextBoxRcno As TextBox = CType(grvTargetDetails.Rows(rowIndex).Cells(5).FindControl("txtRcnoGV"), TextBox)
                If (String.IsNullOrEmpty(TextBoxRcno.Text) = False) Then
                    If (Convert.ToInt32(TextBoxRcno.Text) > 0) Then

                        Dim conn As MySqlConnection = New MySqlConnection(constr)
                        conn.Open()

                        Dim commandUpdGS As MySqlCommand = New MySqlCommand
                        commandUpdGS.CommandType = CommandType.Text
                        commandUpdGS.CommandText = "Delete from tblcontractservingtarget where rcno = " & TextBoxRcno.Text
                        commandUpdGS.Connection = conn
                        commandUpdGS.ExecuteNonQuery()

                        conn.Close()
                    End If
                End If

                If dt.Rows.Count > 1 Then
                    dt.Rows.Remove(dt.Rows(rowIndex))
                    drCurrentRow = dt.NewRow()
                    ViewState("CurrentTable") = dt
                    grvTargetDetails.DataSource = dt
                    grvTargetDetails.DataBind()

                    SetPreviousData()

                    Dim TextBoxTargetDesc2 As DropDownList = CType(grvTargetDetails.Rows(dt.Rows.Count - 1).Cells(1).FindControl("ddlTargetDescGV"), DropDownList)
                    Query = "Select * from tblTarget"
                    PopulateDropDownList(Query, "descrip1", "descrip1", TextBoxTargetDesc2)


                    ''''''''''''''''''''''''''
                    HasDuplicateTarget = HighlightDuplicate(grvTargetDetails)

                    txtDuplicateTarget.Text = "N"

                    If HasDuplicateTarget = True Then
                        txtDuplicateTarget.Text = "Y"
                        Dim message As String = "alert('Duplicate Target/Pests has been selected!!!')"
                        ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)

                        Exit Sub
                    End If


                    'Dim ddl1 As DropDownList = DirectCast(sender, DropDownList)

                    'Dim xrow1 As GridViewRow = CType(ddl1.NamingContainer, GridViewRow)
                    'Dim rowindex1 As Integer = xrow1.RowIndex

                    If txtRecordAdded.Text = "N" Then
                        btnAddDetail_Click(sender, e)
                    End If

                    txtRecordDeleted.Text = "Y"

                    UpdateTargetDescription()
                    ''''''''''''''''''''''
                    'Dim i6 As Integer = objCommon.PopulateDropDown(CType(grvTargetDetails.Rows(dt.Rows.Count - 1).Cells(1).FindControl("ddlSpareIdGV"), DropDownList), "Select * from spare where  VATRate > 0.00 and  CompId=" & Convert.ToString(HttpContext.Current.Session("CompId")) & "  and BranchId=" & Convert.ToString(HttpContext.Current.Session("BranchID")), "SpareDesc", "SpareId")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UpdateTargetDescription()
        Dim conn As MySqlConnection = New MySqlConnection(constr)
        conn.Open()

        Dim command1 As MySqlCommand = New MySqlCommand
        command1.CommandType = CommandType.Text
        command1.CommandText = "Update tblcontractdet set TargetDesc = '" & txtTargetDesc.Text & "' where rcno = " & Convert.ToInt32(txtRcno.Text)
        command1.Connection = conn
        command1.ExecuteNonQuery()

        conn.Close()

    End Sub

    Protected Sub grvTargetDetails_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Try

            If e.Row.RowType = DataControlRowType.DataRow Then

                ' Delete

                For Each cell As DataControlFieldCell In e.Row.Cells
                    ' check all cells in one row
                    For Each control As Control In cell.Controls

                        Dim button As ImageButton = TryCast(control, ImageButton)
                        If button IsNot Nothing AndAlso button.CommandName = "Delete" Then
                            'button.OnClientClick = "if (!confirm('Are you sure to DELETE this record?')) return;"
                            button.OnClientClick = "Confirm()"
                        End If
                    Next control
                Next cell

            End If



        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Protected Sub grvTargetDetails_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        Try
            grvTargetDetails.PageIndex = e.NewPageIndex
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Protected Sub btnAddDetail_Click(ByVal sender As Object, ByVal e As EventArgs)
        'AddNewRowWithDetailRec()
        If TotDetailRecords > 0 Then
            AddNewRowWithDetailRec()
        Else
            AddNewRow()
        End If
    End Sub





    Private Sub FirstGridViewRowFreq()
        Try
            Dim dtFreq As New DataTable()
            Dim drFreq As DataRow = Nothing
            'dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));

            dtFreq.Columns.Add(New DataColumn("SeqNo", GetType(String)))
            dtFreq.Columns.Add(New DataColumn("FreqMTD", GetType(String)))
            dtFreq.Columns.Add(New DataColumn("MonthNo", GetType(String)))
            dtFreq.Columns.Add(New DataColumn("DayNo", GetType(String)))
            dtFreq.Columns.Add(New DataColumn("WeekNo", GetType(String)))
            dtFreq.Columns.Add(New DataColumn("WeekDOW", GetType(String)))
            dtFreq.Columns.Add(New DataColumn("Location", GetType(String)))
            dtFreq.Columns.Add(New DataColumn("BranchID", GetType(String)))

            dtFreq.Columns.Add(New DataColumn("ContractNoF", GetType(String)))
            dtFreq.Columns.Add(New DataColumn("SourceSQLIDF", GetType(String)))
            dtFreq.Columns.Add(New DataColumn("RcnoF", GetType(String)))

            drFreq = dtFreq.NewRow()

            drFreq("SeqNo") = "001"
            drFreq("FreqMTD") = String.Empty
            drFreq("MonthNo") = String.Empty
            drFreq("DayNo") = String.Empty
            drFreq("WeekNo") = String.Empty
            drFreq("WeekDOW") = String.Empty
            drFreq("Location") = String.Empty
            drFreq("BranchID") = String.Empty

            drFreq("ContractNoF") = String.Empty
            drFreq("SourceSQLIDF") = 0
            drFreq("RcnoF") = 0
            dtFreq.Rows.Add(drFreq)

            ViewState("CurrentTableFreq") = dtFreq

            grvFreqDetails.DataSource = dtFreq
            grvFreqDetails.DataBind()

            'Dim btnAdd As Button = CType(grvTargetDetails.FooterRow.Cells(1).FindControl("btnAddDetail"), Button)
            'Page.Form.DefaultFocus = btnAdd.ClientID

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewRowFreq()
        Try
            Dim rowIndex As Integer = 0
            'Dim Query As String

            If ViewState("CurrentTableFreq") IsNot Nothing Then
                Dim dtCurrentTable As DataTable = CType(ViewState("CurrentTableFreq"), DataTable)
                Dim drCurrentRow As DataRow = Nothing
                If dtCurrentTable.Rows.Count > 0 Then
                    For i As Integer = 1 To dtCurrentTable.Rows.Count

                        Dim TextBoxSeqNo As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(0).FindControl("txtSeqNoGV"), TextBox)
                        Dim TextBoxFreqMTD As DropDownList = CType(grvFreqDetails.Rows(rowIndex).Cells(1).FindControl("ddlFreqMTDGV"), DropDownList)
                        Dim TextBoxMonthNo As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(2).FindControl("txtMonthNoGV"), TextBox)
                        Dim TextBoxDayNo As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(3).FindControl("txtDayNoGV"), TextBox)
                        Dim TextBoxWeekNo As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(4).FindControl("txtWeekNoGV"), TextBox)
                        Dim TextBoxWeekDOW As DropDownList = CType(grvFreqDetails.Rows(rowIndex).Cells(5).FindControl("ddlWeekDOWGV"), DropDownList)
                        Dim TextBoxLocation As DropDownList = CType(grvFreqDetails.Rows(rowIndex).Cells(6).FindControl("ddlLocationGV"), DropDownList)
                        Dim TextBoxBranchID As DropDownList = CType(grvFreqDetails.Rows(rowIndex).Cells(7).FindControl("ddlBranchIDGV"), DropDownList)

                        Dim TextBoxContractNo As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(8).FindControl("txtContractNoGVF"), TextBox)

                        Dim TextBoxSourceSQLID As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(9).FindControl("txtSourceSQLIDGVF"), TextBox)
                        Dim TextBoxRcno As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(10).FindControl("txtRcnoGVF"), TextBox)
                        drCurrentRow = dtCurrentTable.NewRow()

                        'drCurrentRow("SeqNo") = ""
                        drCurrentRow("SeqNo") = gSeq
                        'drCurrentRow("FreqMTD") = gFreqMTD
                        drCurrentRow("ContractNoF") = txtContact.Text
                        drCurrentRow("RcnoF") = 0

                        dtCurrentTable.Rows(i - 1)("SeqNo") = TextBoxSeqNo.Text
                        dtCurrentTable.Rows(i - 1)("FreqMTD") = TextBoxFreqMTD.SelectedValue
                        dtCurrentTable.Rows(i - 1)("MonthNo") = TextBoxMonthNo.Text
                        dtCurrentTable.Rows(i - 1)("DayNo") = TextBoxDayNo.Text
                        dtCurrentTable.Rows(i - 1)("WeekNo") = TextBoxWeekNo.Text
                        dtCurrentTable.Rows(i - 1)("WeekDOW") = TextBoxWeekDOW.SelectedValue
                        dtCurrentTable.Rows(i - 1)("Location") = TextBoxLocation.Text
                        dtCurrentTable.Rows(i - 1)("BranchID") = TextBoxBranchID.Text
                        dtCurrentTable.Rows(i - 1)("ContractNoF") = TextBoxContractNo.Text
                        dtCurrentTable.Rows(i - 1)("SourceSQLIDF") = TextBoxSourceSQLID.Text
                        dtCurrentTable.Rows(i - 1)("RcnoF") = TextBoxRcno.Text
                        rowIndex += 1

                    Next i
                    dtCurrentTable.Rows.Add(drCurrentRow)
                    ViewState("CurrentTableFreq") = dtCurrentTable

                    grvFreqDetails.DataSource = dtCurrentTable
                    grvFreqDetails.DataBind()



                    If txtFrequencyDesc.Text = "DAILY" Then
                        Dim TextBoxFreqMTD As DropDownList = CType(grvFreqDetails.Rows(0).Cells(0).FindControl("ddlFreqMTDGV"), DropDownList)
                        Dim TextBoxWeekDOW As DropDownList = CType(grvFreqDetails.Rows(0).Cells(0).FindControl("ddlWeekDOWGV"), DropDownList)
                        TextBoxFreqMTD.Items.Clear()
                        TextBoxFreqMTD.Items.Add("DAY")

                        TextBoxWeekDOW.Enabled = False
                    ElseIf txtFrequencyDesc.Text = "MONTHLY" Or txtFrequencyDesc.Text = "TWICE-MONTHLY" Or txtFrequencyDesc.Text = "THRICE-MONTHLY" Or txtFrequencyDesc.Text = "FOUR-TIMES-MONTHLY" Then
                        Dim TextBoxFreqMTD As DropDownList = CType(grvFreqDetails.Rows(0).Cells(0).FindControl("ddlFreqMTDGV"), DropDownList)
                        TextBoxFreqMTD.Items.Clear()
                        TextBoxFreqMTD.Items.Add("DOW")
                        TextBoxFreqMTD.Items.Add("DATE")

                        Dim TextBoxWeekNo As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(4).FindControl("txtWeekNoGV"), TextBox)
                        TextBoxWeekNo.Enabled = True

                        TextBoxWeekNo.Text = pWeekNumber(txtServStart.Text)

                        Dim TextBoxWeekDOW As DropDownList = CType(grvFreqDetails.Rows(rowIndex).Cells(5).FindControl("ddlWeekDOWGV"), DropDownList)
                        TextBoxWeekDOW.Text = CDate(txtServStart.Text).DayOfWeek.ToString()
                    ElseIf txtFrequencyDesc.Text = "WEEKLY" Then
                        Dim TextBoxWeekDOW As DropDownList = CType(grvFreqDetails.Rows(0).Cells(0).FindControl("ddlWeekDOWGV"), DropDownList)
                        TextBoxWeekDOW.Text = gDOW

                    ElseIf txtFrequencyDesc.Text = "BI-MONTHLY" Or txtFrequencyDesc.Text = "QUARTERLY" Or txtFrequencyDesc.Text = "HALF-ANNUALLY" Or txtFrequencyDesc.Text = "ANNUALLY" Or txtFrequencyDesc.Text = "BI-ANNUALLY" Then
                        Dim TextBoxSeqNo As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(0).FindControl("txtSeqNoGV"), TextBox)
                        TextBoxSeqNo.Text = gSeq
                        Dim TextBoxFreqMTD As DropDownList = CType(grvFreqDetails.Rows(0).Cells(0).FindControl("ddlFreqMTDGV"), DropDownList)
                        TextBoxFreqMTD.Items.Clear()
                        TextBoxFreqMTD.Items.Add("DOW")
                        TextBoxFreqMTD.Items.Add("MONTH")
                        Dim TextBoxWeekNo As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(6).FindControl("txtWeekNoGV"), TextBox)
                        TextBoxWeekNo.Enabled = True
                    ElseIf txtFrequencyDesc.Text = "BI-WEEKLY" Or txtFrequencyDesc.Text = "TRI-WEEKLY" Or txtFrequencyDesc.Text = "FORTNIGHTLY" Then
                        Dim TextBoxWeekDOW As DropDownList = CType(grvFreqDetails.Rows(0).Cells(0).FindControl("ddlWeekDOWGV"), DropDownList)
                        TextBoxWeekDOW.Text = gDOW

                        Dim TextBoxWeekNo As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(6).FindControl("txtWeekNoGV"), TextBox)
                        TextBoxWeekNo.Enabled = True
                    ElseIf txtFrequencyDesc.Text = "TWICE-WEEKLY" Or txtFrequencyDesc.Text = "THRICE-WEEKLY" Or txtFrequencyDesc.Text = "FOUR-TIMES-WEEKLY" Or txtFrequencyDesc.Text = "FIVE-TIMES-WEEKLY" Or txtFrequencyDesc.Text = "SIX-TIMES-WEEKLY" Then
                        Dim TextBoxFreqMTD As DropDownList = CType(grvFreqDetails.Rows(0).Cells(0).FindControl("ddlFreqMTDGV"), DropDownList)
                        TextBoxFreqMTD.Items.Clear()
                        TextBoxFreqMTD.Items.Add("DOW")
                        TextBoxFreqMTD.Enabled = False

                        Dim TextBoxWeekNo As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(6).FindControl("txtWeekNoGV"), TextBox)
                        TextBoxWeekNo.Enabled = False

                        Dim TextBoxWeekDOW As DropDownList = CType(grvFreqDetails.Rows(0).Cells(0).FindControl("ddlWeekDOWGV"), DropDownList)
                        TextBoxWeekDOW.Text = gDOW

                    Else
                        Dim TextBoxFreqMTD As DropDownList = CType(grvFreqDetails.Rows(0).Cells(0).FindControl("ddlFreqMTDGV"), DropDownList)
                        Dim TextBoxSeqNo As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(0).FindControl("txtSeqNoGV"), TextBox)
                        TextBoxSeqNo.Text = gSeq
                        TextBoxFreqMTD.Items.Clear()
                        TextBoxFreqMTD.Items.Add(gFreqMTD)
                    End If
                    '    'Dim i2 As Integer = objCommon.PopulateDropDown(CType(grvTargetDetails.Rows(rowIndex2).Cells(0).FindControl("ddlSpareIdGV"), DropDownList), "Select SpareId, SpareDesc from spare where VATRate > 0.00 and  CompId=" & Convert.ToString(HttpContext.Current.Session("CompId")) & " and BranchId=" & Convert.ToString(HttpContext.Current.Session("BranchID")) & " order by SpareDesc", "SpareDesc", "SpareId")
                    '    rowIndex2 += 1
                    '    j += 1
                    'Loop

                    'Dim TextBoxTargetDesc2 As DropDownList = CType(grvFreqDetails.Rows(rowIndex).Cells(0).FindControl("ddlTargetDescGV"), DropDownList)
                    'Query = "Select * from tblTarget"
                    'PopulateDropDownList(Query, "descrip1", "descrip1", TextBoxTargetDesc2)
                    ''Dim i5 As Integer = objCommon.PopulateDropDown(CType(grvTargetDetails.Rows(rowIndex).Cells(0).FindControl("ddlSpareIdGV"), DropDownList), "Select * from Spare where  VATRate > 0.00 and  CompId=" & Convert.ToString(HttpContext.Current.Session("CompId")) & " and BranchId=" & Convert.ToString(HttpContext.Current.Session("BranchID")) & " order by SpareDesc", "SpareDesc", "SpareId")


                End If
            Else
                Response.Write("ViewState is null")
            End If
            SetPreviousDataFreq()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub AddNewRowWithDetailRecFreq()
        Try
            Dim rowIndex As Integer = 0
            'Dim Query As String
            If ViewState("CurrentTableFreq") IsNot Nothing Then
                Dim dtCurrentTable As DataTable = CType(ViewState("CurrentTableFreq"), DataTable)
                Dim drCurrentRow As DataRow = Nothing
                If TotDetailRecords > 0 Then
                    For i As Integer = 1 To (dtCurrentTable.Rows.Count)


                        Dim TextBoxSeqNo As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(0).FindControl("txtSeqNoGV"), TextBox)
                        Dim TextBoxFreqMTD As DropDownList = CType(grvFreqDetails.Rows(rowIndex).Cells(1).FindControl("ddlFreqMTDGV"), DropDownList)
                        Dim TextBoxMonthNo As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(2).FindControl("txtMonthNoGV"), TextBox)
                        Dim TextBoxDayNo As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(3).FindControl("txtDayNoGV"), TextBox)
                        Dim TextBoxWeekNo As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(4).FindControl("txtWeekNoGV"), TextBox)
                        Dim TextBoxWeekDOW As DropDownList = CType(grvFreqDetails.Rows(rowIndex).Cells(5).FindControl("ddlWeekDOWGV"), DropDownList)
                        Dim TextBoxLocation As DropDownList = CType(grvFreqDetails.Rows(rowIndex).Cells(6).FindControl("ddlLocationGV"), DropDownList)
                        Dim TextBoxBranchID As DropDownList = CType(grvFreqDetails.Rows(rowIndex).Cells(7).FindControl("ddlBranchIDGV"), DropDownList)
                        Dim TextBoxContractNo As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(8).FindControl("txtContractNoGVF"), TextBox)
                        Dim TextBoxSourceSQLID As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(9).FindControl("txtSourceSQLIDGVF"), TextBox)
                        Dim TextBoxRcno As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(10).FindControl("txtRcnoGVF"), TextBox)
                        drCurrentRow = dtCurrentTable.NewRow()

                        'drCurrentRow("TargetID") = ""
                        'drCurrentRow("ServiceID") = txtServiceId.Text
                        drCurrentRow("ContractNoF") = txtContact.Text
                        drCurrentRow("RcnoF") = 0

                        dtCurrentTable.Rows(i - 1)("SeqNo") = TextBoxSeqNo.Text
                        dtCurrentTable.Rows(i - 1)("FreqMTD") = TextBoxFreqMTD.SelectedValue
                        dtCurrentTable.Rows(i - 1)("MonthNo") = TextBoxMonthNo.Text
                        dtCurrentTable.Rows(i - 1)("DayNo") = TextBoxDayNo.Text
                        dtCurrentTable.Rows(i - 1)("WeekNo") = TextBoxWeekNo.Text
                        dtCurrentTable.Rows(i - 1)("WeekDOW") = TextBoxWeekDOW.SelectedValue
                        dtCurrentTable.Rows(i - 1)("Location") = TextBoxLocation.Text
                        dtCurrentTable.Rows(i - 1)("BranchID") = TextBoxBranchID.Text
                        dtCurrentTable.Rows(i - 1)("ContractNoF") = TextBoxContractNo.Text
                        dtCurrentTable.Rows(i - 1)("SourceSQLIDF") = TextBoxSourceSQLID.Text
                        dtCurrentTable.Rows(i - 1)("RcnoF") = TextBoxRcno.Text
                        rowIndex += 1

                    Next i
                    dtCurrentTable.Rows.Add(drCurrentRow)
                    ViewState("CurrentTableFreq") = dtCurrentTable

                    grvFreqDetails.DataSource = dtCurrentTable
                    grvFreqDetails.DataBind()


                    If txtFrequencyDesc.Text = "DAILY" Then
                        'Dim TextBoxSeqNo As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(0).FindControl("txtSeqNoGV"), TextBox)
                        'TextBoxSeqNo.Text = gSeq
                        Dim TextBoxFreqMTD As DropDownList = CType(grvFreqDetails.Rows(0).Cells(0).FindControl("ddlFreqMTDGV"), DropDownList)
                        TextBoxFreqMTD.Items.Clear()
                        TextBoxFreqMTD.Items.Add("DAY")
                    ElseIf txtFrequencyDesc.Text = "MONTHLY" Or txtFrequencyDesc.Text = "TWICE-MONTHLY" Or txtFrequencyDesc.Text = "THRICE-MONTHLY" Or txtFrequencyDesc.Text = "FOUR-TIMES-MONTHLY" Then
                        Dim TextBoxFreqMTD As DropDownList = CType(grvFreqDetails.Rows(0).Cells(0).FindControl("ddlFreqMTDGV"), DropDownList)
                        'TextBoxFreqMTD.Items.Clear()
                        TextBoxFreqMTD.Items.Add("DOW")
                        TextBoxFreqMTD.Items.Add("DATE")

                        Dim TextBoxWeekNo As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(4).FindControl("txtWeekNoGV"), TextBox)
                        TextBoxWeekNo.Enabled = True

                        TextBoxWeekNo.Text = pWeekNumber(txtServStart.Text)

                        Dim TextBoxWeekDOW As DropDownList = CType(grvFreqDetails.Rows(rowIndex).Cells(5).FindControl("ddlWeekDOWGV"), DropDownList)
                        TextBoxWeekDOW.Text = CDate(txtServStart.Text).DayOfWeek.ToString()


                    ElseIf txtFrequencyDesc.Text = "BI-MONTHLY" Or txtFrequencyDesc.Text = "QUARTERLY" Or txtFrequencyDesc.Text = "HALF-ANNUALLY" Or txtFrequencyDesc.Text = "ANNUALLY" Or txtFrequencyDesc.Text = "BI-ANNUALLY" Then
                        Dim TextBoxFreqMTD As DropDownList = CType(grvFreqDetails.Rows(0).Cells(0).FindControl("ddlFreqMTDGV"), DropDownList)
                        'TextBoxFreqMTD.Items.Clear()
                        TextBoxFreqMTD.Items.Add("DOW")
                        TextBoxFreqMTD.Items.Add("MONTH")
                        Dim TextBoxWeekNo As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(6).FindControl("txtWeekNoGV"), TextBox)
                        TextBoxWeekNo.Enabled = True
                    ElseIf txtFrequencyDesc.Text = "BI-WEEKLY" Or txtFrequencyDesc.Text = "TRI-WEEKLY" Or txtFrequencyDesc.Text = "FORTNIGHTLY" Then
                        Dim TextBoxWeekNo As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(6).FindControl("txtWeekNoGV"), TextBox)
                        TextBoxWeekNo.Enabled = True

                    ElseIf txtFrequencyDesc.Text = "TWICE-WEEKLY" Or txtFrequencyDesc.Text = "THRICE-WEEKLY" Or txtFrequencyDesc.Text = "FOUR-TIMES-WEEKLY" Or txtFrequencyDesc.Text = "FIVE-TIMES-WEEKLY" Or txtFrequencyDesc.Text = "SIX-TIMES-WEEKLY" Then
                        Dim TextBoxFreqMTD As DropDownList = CType(grvFreqDetails.Rows(0).Cells(0).FindControl("ddlFreqMTDGV"), DropDownList)
                        TextBoxFreqMTD.Items.Clear()
                        TextBoxFreqMTD.Items.Add("DOW")
                        TextBoxFreqMTD.Enabled = False

                        Dim TextBoxWeekNo As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(6).FindControl("txtWeekNoGV"), TextBox)
                        TextBoxWeekNo.Enabled = False
                    Else
                        Dim TextBoxFreqMTD As DropDownList = CType(grvFreqDetails.Rows(0).Cells(0).FindControl("ddlFreqMTDGV"), DropDownList)
                        'TextBoxFreqMTD.Items.Clear()
                        TextBoxFreqMTD.Items.Add(gFreqMTD)
                    End If
                    'Dim rowIndex1 As Integer = 0

                    'For j As Integer = 1 To (dtCurrentTable.Rows.Count)

                    '    Dim TextBoxTargetDesc1 As DropDownList = CType(grvFreqDetails.Rows(rowIndex1).Cells(0).FindControl("ddlTargetDescGV"), DropDownList)
                    '    Query = "Select * from tblTarget"
                    '    PopulateDropDownList(Query, "descrip1", "descrip1", TextBoxTargetDesc1)
                    '    'Dim i2 As Integer = objCommon.PopulateDropDown(CType(grvTargetDetails.Rows(rowIndex1).Cells(0).FindControl("ddlSpareIdGV"), DropDownList), "Select SpareId, SpareDesc from spare where  VATRate > 0.00 and  CompId=" & Convert.ToString(HttpContext.Current.Session("CompId")) & " and  BranchId=" & Convert.ToString(HttpContext.Current.Session("BranchID")) & "order by SpareDesc", "SpareDesc", "SpareId")

                    '    rowIndex1 += 1
                    'Next j

                    'Dim TextBoxTargetDesc2 As DropDownList = CType(grvFreqDetails.Rows(rowIndex).Cells(0).FindControl("ddlTargetDescGV"), DropDownList)
                    'Query = "Select * from tblTarget"
                    'PopulateDropDownList(Query, "descrip1", "descrip1", TextBoxTargetDesc2)
                    ''Dim i7 As Integer = objCommon.PopulateDropDown(CType(grvTargetDetails.Rows(rowIndex).Cells(1).FindControl("ddlSpareIdGV"), DropDownList), "Select * from Spare where  VATRate > 0.00 and   CompId=" & Convert.ToString(HttpContext.Current.Session("CompId")) & " and BranchId=" & Convert.ToString(HttpContext.Current.Session("BranchID")) & "order by SpareDesc", "SpareDesc", "SpareId")

                End If


            Else
                Response.Write("ViewState is null")
            End If
            SetPreviousDataFreq()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SetPreviousDataFreq()
        Try
            Dim rowIndex As Integer = 0

            'Dim Query As String
            If ViewState("CurrentTableFreq") IsNot Nothing Then
                Dim dt As DataTable = CType(ViewState("CurrentTableFreq"), DataTable)
                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1

                        Dim TextBoxSeqNo As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(0).FindControl("txtSeqNoGV"), TextBox)
                        Dim TextBoxFreqMTD As DropDownList = CType(grvFreqDetails.Rows(rowIndex).Cells(1).FindControl("ddlFreqMTDGV"), DropDownList)
                        Dim TextBoxMonthNo As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(2).FindControl("txtMonthNoGV"), TextBox)
                        Dim TextBoxDayNo As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(3).FindControl("txtDayNoGV"), TextBox)
                        Dim TextBoxWeekNo As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(4).FindControl("txtWeekNoGV"), TextBox)
                        Dim TextBoxWeekDOW As DropDownList = CType(grvFreqDetails.Rows(rowIndex).Cells(5).FindControl("ddlWeekDOWGV"), DropDownList)
                        Dim TextBoxLocation As DropDownList = CType(grvFreqDetails.Rows(rowIndex).Cells(6).FindControl("ddlLocationGV"), DropDownList)
                        Dim TextBoxBranchID As DropDownList = CType(grvFreqDetails.Rows(rowIndex).Cells(7).FindControl("ddlBranchIDGV"), DropDownList)
                        Dim TextBoxContractNo As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(8).FindControl("txtContractNoGVF"), TextBox)
                        Dim TextBoxSourceSQLID As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(9).FindControl("txtSourceSQLIDGVF"), TextBox)
                        Dim TextBoxRcno As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(10).FindControl("txtRcnoGVF"), TextBox)

                        TextBoxSeqNo.Text = dt.Rows(i)("SeqNo").ToString()
                        TextBoxFreqMTD.Text = dt.Rows(i)("FreqMTD").ToString()
                        TextBoxMonthNo.Text = dt.Rows(i)("MonthNo").ToString()
                        TextBoxDayNo.Text = dt.Rows(i)("DayNo").ToString()
                        TextBoxWeekNo.Text = dt.Rows(i)("WeekNo").ToString()
                        TextBoxLocation.Text = dt.Rows(i)("Location").ToString()
                        TextBoxBranchID.Text = dt.Rows(i)("BranchID").ToString()
                        TextBoxWeekDOW.Text = dt.Rows(i)("WeekDOW").ToString()

                        TextBoxContractNo.Text = dt.Rows(i)("ContractNoF").ToString()
                        TextBoxSourceSQLID.Text = dt.Rows(i)("SourceSQLIDF").ToString()
                        TextBoxRcno.Text = dt.Rows(i)("RcnoF").ToString()


                        If txtFrequencyDesc.Text = "DAILY" Then
                            TextBoxFreqMTD.Items.Clear()
                            TextBoxFreqMTD.Items.Add("DAY")
                            TextBoxWeekDOW.Enabled = False

                        ElseIf txtFrequencyDesc.Text = "MONTHLY" Or txtFrequencyDesc.Text = "TWICE-MONTHLY" Or txtFrequencyDesc.Text = "THRICE-MONTHLY" Or txtFrequencyDesc.Text = "FOUR-TIMES-MONTHLY" Then
                            TextBoxFreqMTD.Items.Clear()
                            TextBoxFreqMTD.Items.Add("DOW")
                            TextBoxFreqMTD.Items.Add("DATE")
                            TextBoxWeekNo.Enabled = True

                            TextBoxWeekNo.Text = pWeekNumber(txtServStart.Text)
                            TextBoxWeekDOW.Text = CDate(txtServStart.Text).DayOfWeek.ToString()

                        ElseIf txtFrequencyDesc.Text = "BI-MONTHLY" Or txtFrequencyDesc.Text = "QUARTERLY" Or txtFrequencyDesc.Text = "HALF-ANNUALLY" Or txtFrequencyDesc.Text = "ANNUALLY" Or txtFrequencyDesc.Text = "BI-ANNUALLY" Then
                            'Dim TextBoxSeqNo As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(0).FindControl("txtSeqNoGV"), TextBox)
                            TextBoxSeqNo.Text = "001"
                            'Dim TextBoxFreqMTD As DropDownList = CType(grvFreqDetails.Rows(0).Cells(0).FindControl("ddlFreqMTDGV"), DropDownList)
                            TextBoxFreqMTD.Items.Clear()
                            TextBoxFreqMTD.Items.Add("DOW")
                            TextBoxFreqMTD.Items.Add("MONTH")
                            TextBoxWeekNo.Enabled = True
                            TextBoxWeekNo.Text = pWeekNumber(txtServStart.Text)
                            TextBoxWeekDOW.Text = CDate(txtServStart.Text).DayOfWeek.ToString()
                        ElseIf txtFrequencyDesc.Text = "WEEKLY" Then
                            TextBoxFreqMTD.Items.Clear()
                            TextBoxFreqMTD.Items.Add("DOW")
                            TextBoxWeekDOW.Text = gDOW
                        ElseIf txtFrequencyDesc.Text = "BI-WEEKLY" Or txtFrequencyDesc.Text = "TRI-WEEKLY" Or txtFrequencyDesc.Text = "FORTNIGHTLY" Then
                            TextBoxWeekNo.Enabled = True

                            TextBoxFreqMTD.Items.Clear()
                            TextBoxFreqMTD.Items.Add("DOW")
                            TextBoxWeekNo.Text = pWeekNumber(txtServStart.Text)
                            TextBoxWeekDOW.Text = CDate(txtServStart.Text).DayOfWeek.ToString()

                        ElseIf txtFrequencyDesc.Text = "TWICE-WEEKLY" Or txtFrequencyDesc.Text = "THRICE-WEEKLY" Or txtFrequencyDesc.Text = "FOUR-TIMES-WEEKLY" Or txtFrequencyDesc.Text = "FIVE-TIMES-WEEKLY" Or txtFrequencyDesc.Text = "SIX-TIMES-WEEKLY" Then
                            TextBoxFreqMTD.Items.Clear()
                            TextBoxFreqMTD.Items.Add("DOW")
                            TextBoxWeekNo.Enabled = False
                            TextBoxFreqMTD.Enabled = False
                            TextBoxWeekDOW.Text = gDOW

                            If TextBoxSeqNo.Text = "001" Then
                                TextBoxWeekDOW.Enabled = False
                            End If
                        Else
                            TextBoxFreqMTD.Items.Clear()
                            TextBoxFreqMTD.Items.Add("DOW")
                        End If

                        'If (TextBoxTargtId.Text <> "0") And (TextBoxTargtId.Text <> "") And (String.IsNullOrEmpty(TextBoxTargtId.Text) = False) Then
                        '    Dim TextBoxTargetDesc2 As DropDownList = CType(grvFreqDetails.Rows(rowIndex).Cells(1).FindControl("ddlTargetDescGV"), DropDownList)
                        '    Query = "Select * from tblTarget"
                        '    PopulateDropDownList(Query, "descrip1", "descrip1", TextBoxTargetDesc2)
                        'End If


                        rowIndex += 1
                    Next i
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SetRowDataFreq()
        Dim rowIndex As Integer = 0
        Try
            If ViewState("CurrentTableFreq") IsNot Nothing Then
                Dim dtCurrentTable As DataTable = CType(ViewState("CurrentTableFreq"), DataTable)
                Dim drCurrentRow As DataRow = Nothing
                If dtCurrentTable.Rows.Count > 0 Then
                    For i As Integer = 1 To dtCurrentTable.Rows.Count


                        Dim TextBoxSeqNo As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(0).FindControl("txtSeqNoGV"), TextBox)
                        Dim TextBoxFreqMTD As DropDownList = CType(grvFreqDetails.Rows(rowIndex).Cells(1).FindControl("ddlFreqMTDGV"), DropDownList)
                        Dim TextBoxMonthNo As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(2).FindControl("txtMonthNoGV"), TextBox)
                        Dim TextBoxDayNo As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(3).FindControl("txtDayNoGV"), TextBox)
                        Dim TextBoxWeekNo As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(4).FindControl("txtWeekNoGV"), TextBox)
                        Dim TextBoxWeekDOW As DropDownList = CType(grvFreqDetails.Rows(rowIndex).Cells(5).FindControl("ddlWeekDOWGV"), DropDownList)
                        Dim TextBoxLocation As DropDownList = CType(grvFreqDetails.Rows(rowIndex).Cells(6).FindControl("ddlLocationGV"), DropDownList)
                        Dim TextBoxBranchID As DropDownList = CType(grvFreqDetails.Rows(rowIndex).Cells(7).FindControl("ddlBranchIDGV"), DropDownList)
                        Dim TextBoxContractNo As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(8).FindControl("txtContractNoGVF"), TextBox)
                        Dim TextBoxSourceSQLID As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(9).FindControl("txtSourceSQLIDGVF"), TextBox)
                        Dim TextBoxRcno As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(10).FindControl("txtRcnoGVF"), TextBox)

                        drCurrentRow = dtCurrentTable.NewRow()

                        dtCurrentTable.Rows(i - 1)("SeqNo") = TextBoxSeqNo.Text
                        dtCurrentTable.Rows(i - 1)("FreqMTD") = TextBoxFreqMTD.Text
                        dtCurrentTable.Rows(i - 1)("MonthNo") = TextBoxMonthNo.Text
                        dtCurrentTable.Rows(i - 1)("DayNo") = TextBoxDayNo.Text
                        dtCurrentTable.Rows(i - 1)("WeekNo") = TextBoxWeekNo.Text
                        dtCurrentTable.Rows(i - 1)("WeekDOW") = TextBoxWeekDOW.Text
                        dtCurrentTable.Rows(i - 1)("Location") = TextBoxLocation.Text
                        dtCurrentTable.Rows(i - 1)("BranchID") = TextBoxBranchID.Text

                        dtCurrentTable.Rows(i - 1)("ContractNoF") = TextBoxContractNo.Text
                        dtCurrentTable.Rows(i - 1)("SourceSQLIDF") = TextBoxSourceSQLID.Text
                        dtCurrentTable.Rows(i - 1)("RcnoF") = TextBoxRcno.Text

                        rowIndex += 1
                    Next i

                    ViewState("CurrentTableFreq") = dtCurrentTable


                End If
            Else
                Response.Write("ViewState is null")
            End If
            SetPreviousDataFreq()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    '''''''''''''''''''''''''''''''''

    Private Sub AddNewRowFreqEdit()
        Try
            Dim rowIndex As Integer = 0
            'Dim Query As String

            If ViewState("CurrentTableFreq") IsNot Nothing Then
                Dim dtCurrentTable As DataTable = CType(ViewState("CurrentTableFreq"), DataTable)
                Dim drCurrentRow As DataRow = Nothing
                If dtCurrentTable.Rows.Count > 0 Then
                    For i As Integer = 1 To dtCurrentTable.Rows.Count

                        Dim TextBoxSeqNo As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(0).FindControl("txtSeqNoGV"), TextBox)
                        Dim TextBoxFreqMTD As DropDownList = CType(grvFreqDetails.Rows(rowIndex).Cells(1).FindControl("ddlFreqMTDGV"), DropDownList)
                        Dim TextBoxMonthNo As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(2).FindControl("txtMonthNoGV"), TextBox)
                        Dim TextBoxDayNo As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(3).FindControl("txtDayNoGV"), TextBox)
                        Dim TextBoxWeekNo As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(4).FindControl("txtWeekNoGV"), TextBox)
                        Dim TextBoxWeekDOW As DropDownList = CType(grvFreqDetails.Rows(rowIndex).Cells(5).FindControl("ddlWeekDOWGV"), DropDownList)
                        Dim TextBoxLocation As DropDownList = CType(grvFreqDetails.Rows(rowIndex).Cells(6).FindControl("ddlLocationGV"), DropDownList)
                        Dim TextBoxBranchID As DropDownList = CType(grvFreqDetails.Rows(rowIndex).Cells(7).FindControl("ddlBranchIDGV"), DropDownList)

                        Dim TextBoxContractNo As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(8).FindControl("txtContractNoGVF"), TextBox)

                        Dim TextBoxSourceSQLID As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(9).FindControl("txtSourceSQLIDGVF"), TextBox)
                        Dim TextBoxRcno As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(10).FindControl("txtRcnoGVF"), TextBox)
                        drCurrentRow = dtCurrentTable.NewRow()


                        drCurrentRow("SeqNo") = gSeq
                        'drCurrentRow("FreqMTD") = gFreqMTD
                        drCurrentRow("ContractNoF") = txtContact.Text
                        drCurrentRow("RcnoF") = 0

                        dtCurrentTable.Rows(i - 1)("SeqNo") = TextBoxSeqNo.Text
                        dtCurrentTable.Rows(i - 1)("FreqMTD") = TextBoxFreqMTD.SelectedValue
                        dtCurrentTable.Rows(i - 1)("MonthNo") = TextBoxMonthNo.Text
                        dtCurrentTable.Rows(i - 1)("DayNo") = TextBoxDayNo.Text
                        dtCurrentTable.Rows(i - 1)("WeekNo") = TextBoxWeekNo.Text
                        dtCurrentTable.Rows(i - 1)("WeekDOW") = TextBoxWeekDOW.SelectedValue
                        dtCurrentTable.Rows(i - 1)("Location") = TextBoxLocation.Text
                        dtCurrentTable.Rows(i - 1)("BranchID") = TextBoxBranchID.Text
                        dtCurrentTable.Rows(i - 1)("ContractNoF") = TextBoxContractNo.Text
                        dtCurrentTable.Rows(i - 1)("SourceSQLIDF") = TextBoxSourceSQLID.Text
                        dtCurrentTable.Rows(i - 1)("RcnoF") = TextBoxRcno.Text
                        rowIndex += 1


                    Next i
                    dtCurrentTable.Rows.Add(drCurrentRow)
                    ViewState("CurrentTableFreq") = dtCurrentTable

                    grvFreqDetails.DataSource = dtCurrentTable
                    grvFreqDetails.DataBind()


                End If
            Else
                Response.Write("ViewState is null")
            End If
            SetPreviousDataFreqEdit()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub




    Private Sub SetPreviousDataFreqEdit()
        Try
            Dim rowIndex As Integer = 0

            'Dim Query As String
            If ViewState("CurrentTableFreq") IsNot Nothing Then
                Dim dt As DataTable = CType(ViewState("CurrentTableFreq"), DataTable)
                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1

                        Dim TextBoxSeqNo As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(0).FindControl("txtSeqNoGV"), TextBox)
                        Dim TextBoxFreqMTD As DropDownList = CType(grvFreqDetails.Rows(rowIndex).Cells(1).FindControl("ddlFreqMTDGV"), DropDownList)
                        Dim TextBoxMonthNo As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(2).FindControl("txtMonthNoGV"), TextBox)
                        Dim TextBoxDayNo As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(3).FindControl("txtDayNoGV"), TextBox)
                        Dim TextBoxWeekNo As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(4).FindControl("txtWeekNoGV"), TextBox)
                        Dim TextBoxWeekDOW As DropDownList = CType(grvFreqDetails.Rows(rowIndex).Cells(5).FindControl("ddlWeekDOWGV"), DropDownList)
                        Dim TextBoxLocation As DropDownList = CType(grvFreqDetails.Rows(rowIndex).Cells(6).FindControl("ddlLocationGV"), DropDownList)
                        Dim TextBoxBranchID As DropDownList = CType(grvFreqDetails.Rows(rowIndex).Cells(7).FindControl("ddlBranchIDGV"), DropDownList)
                        Dim TextBoxContractNo As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(8).FindControl("txtContractNoGVF"), TextBox)
                        Dim TextBoxSourceSQLID As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(9).FindControl("txtSourceSQLIDGVF"), TextBox)
                        Dim TextBoxRcno As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(10).FindControl("txtRcnoGVF"), TextBox)

                        TextBoxSeqNo.Text = dt.Rows(i)("SeqNo").ToString()
                        TextBoxFreqMTD.Text = dt.Rows(i)("FreqMTD").ToString()
                        TextBoxMonthNo.Text = dt.Rows(i)("MonthNo").ToString()
                        TextBoxDayNo.Text = dt.Rows(i)("DayNo").ToString()
                        TextBoxWeekNo.Text = dt.Rows(i)("WeekNo").ToString()
                        TextBoxLocation.Text = dt.Rows(i)("Location").ToString()
                        TextBoxBranchID.Text = dt.Rows(i)("BranchID").ToString()
                        TextBoxWeekDOW.Text = dt.Rows(i)("WeekDOW").ToString()

                        TextBoxContractNo.Text = dt.Rows(i)("ContractNoF").ToString()
                        TextBoxSourceSQLID.Text = dt.Rows(i)("SourceSQLIDF").ToString()
                        TextBoxRcno.Text = dt.Rows(i)("RcnoF").ToString()


                        If TextBoxFreqMTD.Text = "DATE" Then
                            TextBoxMonthNo.Enabled = False
                            TextBoxDayNo.Enabled = True
                            TextBoxWeekNo.Enabled = False
                            TextBoxWeekDOW.Enabled = False

                        ElseIf TextBoxFreqMTD.Text = "DOW" Then
                            TextBoxMonthNo.Enabled = False
                            TextBoxDayNo.Enabled = False
                            TextBoxWeekNo.Enabled = True
                            TextBoxWeekDOW.Enabled = True

                            If txtFrequencyDesc.Text = "TWICE-WEEKLY" Or txtFrequencyDesc.Text = "THRICE-WEEKLY" Or txtFrequencyDesc.Text = "FOUR-TIMES-WEEKLY" Or txtFrequencyDesc.Text = "FIVE-TIMES-WEEKLY" Or txtFrequencyDesc.Text = "SIX-TIMES-WEEKLY" Then
                                TextBoxWeekNo.Enabled = False
                                TextBoxFreqMTD.Enabled = False
                            End If


                        ElseIf TextBoxFreqMTD.Text = "MONTH" Then
                            TextBoxMonthNo.Enabled = True
                            TextBoxDayNo.Enabled = True
                            TextBoxWeekNo.Enabled = False
                            TextBoxWeekDOW.Enabled = False

                        End If


                        rowIndex += 1

                        If txtFrequencyDesc.Text = "DAILY" Then
                            TextBoxFreqMTD.Items.Clear()
                            TextBoxFreqMTD.Items.Add("DAY")
                            TextBoxWeekDOW.Enabled = False

                        ElseIf txtFrequencyDesc.Text = "MONTHLY" Or txtFrequencyDesc.Text = "TWICE-MONTHLY" Or txtFrequencyDesc.Text = "THRICE-MONTHLY" Or txtFrequencyDesc.Text = "FOUR-TIMES-MONTHLY" Then
                            ''TextBoxFreqMTD.Items.Clear()
                            TextBoxFreqMTD.Items.Remove("DAY")
                            TextBoxFreqMTD.Items.Remove("MONTH")



                            If String.IsNullOrEmpty(TextBoxFreqMTD.Items.FindByValue("DOW").ToString) = True Then
                                TextBoxFreqMTD.Items.Add("DOW")
                            End If
                            If String.IsNullOrEmpty(TextBoxFreqMTD.Items.FindByValue("DATE").ToString) = True Then
                                TextBoxFreqMTD.Items.Add("DATE")
                            End If

                            'TextBoxFreqMTD.Items.Add("DATE")
                            'TextBoxWeekNo.Enabled = True

                            'TextBoxWeekNo.Text = pWeekNumber(txtServStart.Text)
                            'TextBoxWeekDOW.Text = CDate(txtServStart.Text).DayOfWeek.ToString()

                        ElseIf txtFrequencyDesc.Text = "BI-MONTHLY" Or txtFrequencyDesc.Text = "QUARTERLY" Or txtFrequencyDesc.Text = "HALF-ANNUALLY" Or txtFrequencyDesc.Text = "ANNUALLY" Or txtFrequencyDesc.Text = "BI-ANNUALLY" Then
                            TextBoxFreqMTD.Items.Remove("DAY")
                            TextBoxFreqMTD.Items.Remove("DATE")

                            If String.IsNullOrEmpty(TextBoxFreqMTD.Items.FindByValue("DOW").ToString) = True Then
                                TextBoxFreqMTD.Items.Add("DOW")
                            End If
                            If String.IsNullOrEmpty(TextBoxFreqMTD.Items.FindByValue("MONTH").ToString) = True Then
                                TextBoxFreqMTD.Items.Add("MONTH")
                            End If

                            'TextBoxWeekNo.Enabled = True
                            'TextBoxWeekNo.Text = pWeekNumber(txtServStart.Text)
                            'TextBoxWeekDOW.Text = CDate(txtServStart.Text).DayOfWeek.ToString()
                        ElseIf txtFrequencyDesc.Text = "WEEKLY" Then
                            TextBoxFreqMTD.Items.Clear()
                            TextBoxFreqMTD.Items.Add("DOW")

                        ElseIf txtFrequencyDesc.Text = "BI-WEEKLY" Or txtFrequencyDesc.Text = "TRI-WEEKLY" Or txtFrequencyDesc.Text = "FORTNIGHTLY" Then
                            TextBoxWeekNo.Enabled = True
                        ElseIf txtFrequencyDesc.Text = "TWICE-WEEKLY" Or txtFrequencyDesc.Text = "THRICE-WEEKLY" Or txtFrequencyDesc.Text = "FOUR-TIMES-WEEKLY" Or txtFrequencyDesc.Text = "FIVE-TIMES-WEEKLY" Or txtFrequencyDesc.Text = "SIX-TIMES-WEEKLY" Then
                            TextBoxFreqMTD.Items.Clear()
                            TextBoxFreqMTD.Items.Add("DOW")

                        Else
                            TextBoxFreqMTD.Items.Clear()
                            TextBoxFreqMTD.Items.Add("DOW")
                        End If
                    Next i


                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''''''''''''''''''''''''''''''


    '''
    Public Function HighlightDuplicateFreq(ByVal gridview As GridView) As Boolean
        Dim HasDuplicateFreq As Boolean = False
        For currentRow As Integer = 0 To gridview.Rows.Count - 2
            Dim rowToCompare As GridViewRow = gridview.Rows(currentRow)

            For otherRow As Integer = currentRow + 1 To gridview.Rows.Count - 1
                Dim row As GridViewRow = gridview.Rows(otherRow)
                Dim duplicateRow As Boolean = False

                Dim TextBoxdWeekDOWGVGV As DropDownList = CType(grvFreqDetails.Rows(currentRow).Cells(0).FindControl("ddlWeekDOWGV"), DropDownList)
                Dim TextBoxdWeekDOWGVGV1 As DropDownList = CType(grvFreqDetails.Rows(otherRow).Cells(0).FindControl("ddlWeekDOWGV"), DropDownList)

                If ((TextBoxdWeekDOWGVGV.Text) = (TextBoxdWeekDOWGVGV1.Text)) Then

                    duplicateRow = True

                    TextBoxdWeekDOWGVGV.BackColor = Drawing.Color.Purple
                    TextBoxdWeekDOWGVGV1.BackColor = Drawing.Color.Purple

                    TextBoxdWeekDOWGVGV.BackColor = Drawing.Color.Purple
                    TextBoxdWeekDOWGVGV1.BackColor = Drawing.Color.Purple

                    Dim custVal As New CustomValidator()
                    custVal.IsValid = False
                    custVal.ErrorMessage = ""
                    custVal.ErrorMessage = "Week Day Already Exists"
                    custVal.EnableClientScript = True

                    custVal.ValidationGroup = "VGroup"
                    Me.Page.Form.Controls.Add(custVal)

                    HasDuplicateFreq = True
                    Return HasDuplicateFreq


                Else
                    duplicateRow = False
                    HasDuplicateFreq = False
                End If

            Next otherRow
        Next currentRow

        Return HasDuplicateFreq
    End Function


    Protected Sub grvFreqDetails_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs) Handles grvFreqDetails.RowDeleting
        Try

            Dim Query As String
            SetRowDataFreq()

            Dim dt As DataTable = CType(ViewState("CurrentTableFreq"), DataTable)
            Dim drCurrentRow As DataRow = Nothing
            Dim rowIndex As Integer = Convert.ToInt32(e.RowIndex)


            If ViewState("CurrentTableFreq") IsNot Nothing Then
                'Dim dt As DataTable = CType(ViewState("CurrentTable"), DataTable)
                'Dim drCurrentRow As DataRow = Nothing
                'Dim rowIndex As Integer = Convert.ToInt32(e.RowIndex)
                ''
                'Dim CatIdGV As TextBox = CType(grvTargetDetails.Rows(rowIndex).Cells(13).FindControl("ddlCatIdGV"), TextBox)
                'Dim SpareIdGV As DropDownList = CType(grvTargetDetails.Rows(rowIndex).Cells(0).FindControl("ddlSpareIdGV"), DropDownList)
                'Dim TransIdDetailGV As TextBox = CType(grvTargetDetails.Rows(rowIndex).Cells(9).FindControl("txtTransIdDetailGV"), TextBox)

                'If Convert.ToInt32(TransIdDetailGV.Text) > 0 Then
                '    'DropDownList SpareIdGV = (DropDownList)grvTargetDetails.Rows[rowIndex].Cells[0].FindControl("ddlSpareIdGV");
                '    Dim SpareSerialNoGV As TextBox = CType(grvTargetDetails.Rows(rowIndex).Cells(2).FindControl("ddlSpareSerialNoGV"), TextBox)
                '    Dim QuantityGV As TextBox = CType(grvTargetDetails.Rows(rowIndex).Cells(1).FindControl("txtQuantityGV"), TextBox)
                '    Dim TotalGV As TextBox = CType(grvTargetDetails.Rows(rowIndex).Cells(7).FindControl("txtTotalGV"), TextBox)

                '    'Delete

                '    objBL.CompId = Convert.ToInt32(HttpContext.Current.Session("CompId"))
                '    objBL.BranchId = Convert.ToInt32(HttpContext.Current.Session("BranchId"))
                '    objBL.AccessBy = Convert.ToInt32(HttpContext.Current.Session("UserId"))
                '    objBL.TransId = Convert.ToInt32(txtParentId.Text)
                '    objBL.TransIdDetail = Convert.ToInt32(TransIdDetailGV.Text)
                '    objBL.CatId = (CatIdGV.Text)
                '    objBL.SpareId = Convert.ToInt32(SpareIdGV.Text)

                '    Dim OutputParam As Integer = -10

                '    'objBL.SpareId = Convert.ToInt32(txtSpareId.Text);
                '    If SpareSerialNoGV.Text = "---Select---" Then
                '        objBL.SpareSerialNo = ""
                '    Else
                '        objBL.SpareSerialNo = (SpareSerialNoGV.Text)
                '    End If


                '    objBL.Qty = Convert.ToDecimal(QuantityGV.Text)


                '    Dim i As Boolean = objBL.DeleteSparePurchDetail(OutputParam)
                '    'If i Then
                '    '    If OutputParam = 1 Then
                '    '        ShowMsg(objConstant.GsMsgDelete)
                '    '    ElseIf OutputParam = 2 Then
                '    '        ShowMsg(objConstant.GsMsgNotDeleted)
                '    '    End If
                '    'End If

            End If


            'If SpareIdGV.Text = "0" Then
            '    Return
            'End If

            If dt.Rows.Count > 1 Then
                dt.Rows.Remove(dt.Rows(rowIndex))
                drCurrentRow = dt.NewRow()
                ViewState("CurrentTableFreq") = dt
                grvFreqDetails.DataSource = dt
                grvFreqDetails.DataBind()

                SetPreviousDataFreq()

                Dim TextBoxTargetDesc2 As DropDownList = CType(grvTargetDetails.Rows(dt.Rows.Count - 1).Cells(1).FindControl("ddlTargetDescGV"), DropDownList)
                Query = "Select * from tblTarget"
                PopulateDropDownList(Query, "descrip1", "descrip1", TextBoxTargetDesc2)
                'Dim i6 As Integer = objCommon.PopulateDropDown(CType(grvTargetDetails.Rows(dt.Rows.Count - 1).Cells(1).FindControl("ddlSpareIdGV"), DropDownList), "Select * from spare where  VATRate > 0.00 and  CompId=" & Convert.ToString(HttpContext.Current.Session("CompId")) & "  and BranchId=" & Convert.ToString(HttpContext.Current.Session("BranchID")), "SpareDesc", "SpareId")
            End If
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Protected Sub grvFreqDetails_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Try

            If e.Row.RowType = DataControlRowType.DataRow Then

                ' Delete

                'For Each cell As DataControlFieldCell In e.Row.Cells
                '    ' check all cells in one row
                '    For Each control As Control In cell.Controls

                '        Dim button As ImageButton = TryCast(control, ImageButton)
                '        If button IsNot Nothing AndAlso button.CommandName = "Delete" Then
                '            ' Add delete confirmation
                '            button.OnClientClick = "if (!confirm('Are you sure to DELETE this record?')) return;"
                '        End If
                '    Next control
                'Next cell

            End If



        Catch ex As Exception
            Throw ex
        End Try
    End Sub



    'Grid View


    Protected Sub grvTargetDetails_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grvTargetDetails.SelectedIndexChanged

    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        MakeMeNull()

        grvFreqDetails.Enabled = True
        grvTargetDetails.Enabled = True
        Dim editindex As Integer = GridView1.SelectedIndex
        rcno = DirectCast(GridView1.Rows(editindex).FindControl("Label1"), Label).Text
        txtRcno.Text = rcno.ToString()

        '' '''

        Try
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim command1 As MySqlCommand = New MySqlCommand
            command1.CommandType = CommandType.Text
            command1.CommandText = "SELECT * FROM tblcontractDet where rcno=" & Convert.ToInt32(txtRcno.Text)
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then

                If dt.Rows(0)("Frequency").ToString <> "" Then : txtFrequency.Text = dt.Rows(0)("Frequency").ToString : End If
                If dt.Rows(0)("FrequencyDesc").ToString <> "" Then : txtFrequencyDesc.Text = dt.Rows(0)("FrequencyDesc").ToString : End If
                If dt.Rows(0)("ServiceID").ToString <> "" Then : txtServiceId.Text = dt.Rows(0)("ServiceID").ToString : End If
                If dt.Rows(0)("ServiceDesc").ToString <> "" Then : txtServiceDesc.Text = dt.Rows(0)("ServiceDesc").ToString : End If
                If dt.Rows(0)("NoService").ToString <> "" Then : txtNoService.Text = dt.Rows(0)("NoService").ToString : End If
                If dt.Rows(0)("NoofSvcInterval").ToString <> "" Then : txtNoofSvcInterval.Text = dt.Rows(0)("NoofSvcInterval").ToString : End If
                If dt.Rows(0)("NoofInterval").ToString <> "" Then : txtNoofInterval.Text = dt.Rows(0)("NoofInterval").ToString : End If
                If dt.Rows(0)("ServiceNotes").ToString <> "" Then : txtServiceNotes.Text = dt.Rows(0)("ServiceNotes").ToString : End If

                If dt.Rows(0)("Value").ToString <> "" Then : txtValuePerService.Text = dt.Rows(0)("Value").ToString : End If

                'If dt.Rows(0)("ContactPerson").ToString <> "" Then : txtContactPerson.Text = dt.Rows(0)("ContactPerson").ToString : End If
                If dt.Rows(0)("OrigCode").ToString <> "" Then : txtSourceSQLID.Text = dt.Rows(0)("OrigCode").ToString : End If
                If dt.Rows(0)("OrigCode").ToString <> "" Then : txtOrigCode.Text = dt.Rows(0)("OrigCode").ToString : End If
                'If dt.Rows(0)("Duration").ToString <> "" Then : txtDuration.Text = dt.Rows(0)("Duration").ToString : End If

            End If



            'Start: Frequency

            ''''''''''''''
            Dim commandByWhich As MySqlCommand = New MySqlCommand
            commandByWhich.CommandType = CommandType.Text
            commandByWhich.CommandText = "SELECT * FROM tblservicefrequency where Frequency= '" & txtFrequency.Text & "'"
            commandByWhich.Connection = conn

            Dim drByWhich As MySqlDataReader = commandByWhich.ExecuteReader()
            Dim dtByWhich As New DataTable
            dtByWhich.Load(drByWhich)

            If dtByWhich.Rows.Count > 0 Then

                If dtByWhich.Rows(0)("MonthByWhichMonth").ToString <> "" Then : txtMonthByWhichMonth.Text = dtByWhich.Rows(0)("MonthByWhichMonth").ToString : End If
                If dtByWhich.Rows(0)("DOWByWhichWeek").ToString <> "" Then : txtDOWByWhichWeek.Text = dtByWhich.Rows(0)("DOWByWhichWeek").ToString : End If

            End If


            '''''''''''''''''''




            SetRowDataFreq()


            Dim dtFreq1 As DataTable = CType(ViewState("CurrentTableFreq"), DataTable)
            Dim drCurrentRowFreq As DataRow = Nothing

            For i As Integer = 0 To grvFreqDetails.Rows.Count - 1
                dtFreq1.Rows.Remove(dtFreq1.Rows(0))
                drCurrentRowFreq = dtFreq1.NewRow()
                ViewState("CurrentTableFreq") = dtFreq1
                grvFreqDetails.DataSource = dtFreq1
                grvFreqDetails.DataBind()

                SetPreviousDataFreq()

            Next i


            FirstGridViewRowFreq()

            Dim cmdContratFreq As MySqlCommand = New MySqlCommand
            cmdContratFreq.CommandType = CommandType.Text
            cmdContratFreq.CommandText = "SELECT * FROM tblServicecontractFrequency where SourceSQLID=" & Convert.ToInt32(txtOrigCode.Text)
            cmdContratFreq.Connection = conn

            Dim drFreq As MySqlDataReader = cmdContratFreq.ExecuteReader()
            Dim dtFreq As New DataTable
            dtFreq.Load(drFreq)

            'Enable

            'Enable

            If txtNoofSvcInterval.Text = "1" Then

                '''
                'If txtFrequencyDesc.Text = "DAILY" Then
                '    TextBoxFreqMtd.Items.Clear()
                '    TextBoxFreqMtd.Items.Add("DATE")
                'ElseIf txtFrequencyDesc.Text = "MONTHLY" Or txtFrequencyDesc.Text = "THRICE-MONTHLY" Then
                '    TextBoxFreqMtd.Items.Clear()
                '    TextBoxFreqMtd.Items.Add("DOW")
                '    TextBoxFreqMtd.Items.Add("DATE")

                'ElseIf txtFrequencyDesc.Text = "WEEKLY" Then
                '    TextBoxFreqMtd.Items.Clear()
                '    TextBoxFreqMtd.Items.Add("DOW")
                'ElseIf txtFrequencyDesc.Text = "BI-MONTHLY" Or txtFrequencyDesc.Text = "QUARTERLY" Or txtFrequencyDesc.Text = "HALF-ANNUALLY" Or txtFrequencyDesc.Text = "ANNUALLY" Or txtFrequencyDesc.Text = "BI-ANNUALLY" Then
                '    TextBoxFreqMtd.Items.Clear()
                '    TextBoxFreqMtd.Items.Add("DOW")
                '    TextBoxFreqMtd.Items.Add("MONTH")
                'Else
                '    TextBoxFreqMtd.Items.Clear()
                '    TextBoxFreqMtd.Items.Add("DOW")
                'End If

                '''


                Dim TextBoxSeqNo As TextBox = CType(grvFreqDetails.Rows(0).Cells(0).FindControl("txtSeqNoGV"), TextBox)
                TextBoxSeqNo.Text = Convert.ToString(dtFreq.Rows(0)("SeqNo"))

                'MessageBox.Message.Alert(Page, Convert.ToString(dtFreq.Rows(0)("FreqMtd")), "str")
                Dim TextBoxFreqMtd As DropDownList = CType(grvFreqDetails.Rows(0).Cells(0).FindControl("ddlFreqMTDGV"), DropDownList)

                If txtFrequencyDesc.Text = "DAILY" Then
                    TextBoxFreqMtd.Items.Clear()
                    TextBoxFreqMtd.Items.Add("DAY")
                ElseIf txtFrequencyDesc.Text = "MONTHLY" Or txtFrequencyDesc.Text = "THRICE-MONTHLY" Then
                    TextBoxFreqMtd.Items.Clear()
                    TextBoxFreqMtd.Items.Add("DOW")
                    TextBoxFreqMtd.Items.Add("DATE")

                ElseIf txtFrequencyDesc.Text = "WEEKLY" Then
                    TextBoxFreqMtd.Items.Clear()
                    TextBoxFreqMtd.Items.Add("DOW")
                ElseIf txtFrequencyDesc.Text = "BI-MONTHLY" Or txtFrequencyDesc.Text = "QUARTERLY" Or txtFrequencyDesc.Text = "HALF-ANNUALLY" Or txtFrequencyDesc.Text = "ANNUALLY" Or txtFrequencyDesc.Text = "BI-ANNUALLY" Then
                    TextBoxFreqMtd.Items.Clear()
                    TextBoxFreqMtd.Items.Add("DOW")
                    TextBoxFreqMtd.Items.Add("MONTH")
                Else
                    TextBoxFreqMtd.Items.Clear()
                    TextBoxFreqMtd.Items.Add("DOW")
                End If

                TextBoxFreqMtd.Text = Convert.ToString(dtFreq.Rows(0)("FreqMtd").ToString.Trim())

                Dim TextBoxDayNo As TextBox = CType(grvFreqDetails.Rows(0).Cells(0).FindControl("txtDayNoGV"), TextBox)
                TextBoxDayNo.Text = Convert.ToString(dtFreq.Rows(0)("DayNo"))

                Dim TextBoxWeekNo As TextBox = CType(grvFreqDetails.Rows(0).Cells(0).FindControl("txtWeekNoGV"), TextBox)
                TextBoxWeekNo.Text = Convert.ToString(dtFreq.Rows(0)("WeekNo"))

                Dim TextBoxMonthNo As TextBox = CType(grvFreqDetails.Rows(0).Cells(0).FindControl("txtMonthNoGV"), TextBox)
                TextBoxMonthNo.Text = Convert.ToString(dtFreq.Rows(0)("MonthNo"))

                Dim TextBoxWeekDOW As DropDownList = CType(grvFreqDetails.Rows(0).Cells(0).FindControl("ddlWeekDOWGV"), DropDownList)
                TextBoxWeekDOW.Text = Convert.ToString(dtFreq.Rows(0)("WeekDOW"))

                Dim TextBoxLocation As DropDownList = CType(grvFreqDetails.Rows(0).Cells(0).FindControl("ddlLocationGV"), DropDownList)
                TextBoxLocation.Text = Convert.ToString(dtFreq.Rows(0)("Location"))

                Dim TextBoxBranchID As DropDownList = CType(grvFreqDetails.Rows(0).Cells(0).FindControl("ddlBranchIDGV"), DropDownList)
                TextBoxBranchID.Text = Convert.ToString(dtFreq.Rows(0)("BranchID"))

                Dim TextBoxContractNo As TextBox = CType(grvFreqDetails.Rows(0).Cells(0).FindControl("txtContractNoGVF"), TextBox)
                TextBoxContractNo.Text = Convert.ToString(dtFreq.Rows(0)("ContractNo"))

                Dim TextBoxSourceSQLID As TextBox = CType(grvFreqDetails.Rows(0).Cells(0).FindControl("txtSourceSQLIDGVF"), TextBox)
                TextBoxSourceSQLID.Text = Convert.ToString(dtFreq.Rows(0)("SourceSQLID"))

                Dim TextBoxRcno As TextBox = CType(grvFreqDetails.Rows(0).Cells(0).FindControl("txtRcnoGVF"), TextBox)
                TextBoxRcno.Text = Convert.ToString(dtFreq.Rows(0)("Rcno"))


                '''
                'If txtFrequencyDesc.Text = "DAILY" Then
                '    TextBoxFreqMtd.Items.Clear()
                '    TextBoxFreqMtd.Items.Add("DATE")
                'ElseIf txtFrequencyDesc.Text = "MONTHLY" Or txtFrequencyDesc.Text = "THRICE-MONTHLY" Then
                '    TextBoxFreqMtd.Items.Clear()
                '    TextBoxFreqMtd.Items.Add("DOW")
                '    TextBoxFreqMtd.Items.Add("DATE")

                'ElseIf txtFrequencyDesc.Text = "WEEKLY" Then
                '    TextBoxFreqMtd.Items.Clear()
                '    TextBoxFreqMtd.Items.Add("DOW")
                'ElseIf txtFrequencyDesc.Text = "BI-MONTHLY" Or txtFrequencyDesc.Text = "QUARTERLY" Or txtFrequencyDesc.Text = "HALF-ANNUALLY" Or txtFrequencyDesc.Text = "ANNUALLY" Or txtFrequencyDesc.Text = "BI-ANNUALLY" Then
                '    TextBoxFreqMtd.Items.Clear()
                '    TextBoxFreqMTD.Items.Add("DOW")
                '    TextBoxFreqMTD.Items.Add("MONTH")
                'Else
                '    TextBoxFreqMtd.Items.Clear()
                '    TextBoxFreqMtd.Items.Add("DOW")
                'End If


                '''

                If txtFrequency.Text = "DAILY" Or txtFrequency.Text = "WEEKLY" Or txtFrequency.Text = "Bi-Weekly" Or txtFrequency.Text = "Tri-Weekly" Or txtFrequency.Text = "Fortnightly" Or txtFrequency.Text = "Twice-Weekly" Then
                    TextBoxFreqMtd.Enabled = False
                End If


                If TextBoxFreqMtd.Text = "DAY" Then
                    TextBoxMonthNo.Enabled = False
                    TextBoxDayNo.Enabled = False
                    TextBoxWeekNo.Enabled = False
                    TextBoxWeekDOW.Enabled = False


                ElseIf TextBoxFreqMtd.Text = "DATE" Then
                    TextBoxMonthNo.Enabled = False
                    TextBoxDayNo.Enabled = True
                    TextBoxWeekNo.Enabled = False
                    TextBoxWeekDOW.Enabled = False

                ElseIf TextBoxFreqMtd.Text = "DOW" Then
                    TextBoxMonthNo.Enabled = False
                    TextBoxDayNo.Enabled = False
                    TextBoxWeekNo.Enabled = True
                    TextBoxWeekDOW.Enabled = True


                    If txtFrequency.Text = "WEEKLY" Or txtFrequency.Text = "Thrice-Weekly" Or txtFrequency.Text = "Twice-Weekly" Or txtFrequency.Text = "Four-Times-Weekly" Or txtFrequency.Text = "Five-Times-Weekly" Or txtFrequency.Text = "Six-Times-Weekly" Then
                        TextBoxWeekNo.Enabled = False
                    End If

                ElseIf TextBoxFreqMtd.Text = "MONTH" Then
                    TextBoxMonthNo.Enabled = True
                    TextBoxDayNo.Enabled = True
                    TextBoxWeekNo.Enabled = False
                    TextBoxWeekDOW.Enabled = False

                End If

            ElseIf Convert.ToInt32(txtNoofSvcInterval.Text) > 1 Then
                Dim rowIndex = 0
                rowIndex = Convert.ToInt32(txtNoofSvcInterval.Text)

                For i As Integer = 0 To rowIndex
                    If (i < (rowIndex - 1)) Then
                        AddNewRowFreqEdit()
                    End If

                    Dim TextBoxSeqNo As TextBox = CType(grvFreqDetails.Rows(i).Cells(0).FindControl("txtSeqNoGV"), TextBox)
                    TextBoxSeqNo.Text = Convert.ToString(dtFreq.Rows(i)("SeqNo"))

                    'MessageBox.Message.Alert(Page, Convert.ToString(dtFreq.Rows(i)("FreqMtd")), "str")
                    Dim TextBoxFreqMtd As DropDownList = CType(grvFreqDetails.Rows(i).Cells(0).FindControl("ddlFreqMTDGV"), DropDownList)


                    'sen
                    If txtFrequencyDesc.Text = "DAILY" Then
                        'Dim TextBoxFreqMTD As DropDownList = CType(grvFreqDetails.Rows(0).Cells(0).FindControl("ddlFreqMTDGV"), DropDownList)
                        TextBoxFreqMtd.Items.Clear()
                        TextBoxFreqMtd.Items.Add("DAY")


                    ElseIf txtFrequencyDesc.Text = "MONTHLY" Or txtFrequencyDesc.Text = "TWICE-MONTHLY" Or txtFrequencyDesc.Text = "THRICE-MONTHLY" Or txtFrequencyDesc.Text = "FOUR-TIMES-MONTHLY" Then
                        'Dim TextBoxFreqMTD As DropDownList = CType(grvFreqDetails.Rows(0).Cells(0).FindControl("ddlFreqMTDGV"), DropDownList)
                        TextBoxFreqMtd.Items.Clear()
                        TextBoxFreqMtd.Items.Add("DOW")
                        TextBoxFreqMtd.Items.Add("DATE")

                        'Dim TextBoxWeekNo As TextBox = CType(grvFreqDetails.Rows(rowIndex).Cells(4).FindControl("txtWeekNoGV"), TextBox)
                        'TextBoxWeekNo.Enabled = True

                        'TextBoxWeekNo.Text = pWeekNumber(txtServStart.Text)


                    ElseIf txtFrequencyDesc.Text = "WEEKLY" Then

                        'TextBoxWeekDOW.Text = gDOW

                    ElseIf txtFrequencyDesc.Text = "BI-MONTHLY" Or txtFrequencyDesc.Text = "QUARTERLY" Or txtFrequencyDesc.Text = "HALF-ANNUALLY" Or txtFrequencyDesc.Text = "ANNUALLY" Or txtFrequencyDesc.Text = "BI-ANNUALLY" Then
                        ''Dim TextBoxFreqMTD As DropDownList = CType(grvFreqDetails.Rows(0).Cells(0).FindControl("ddlFreqMTDGV"), DropDownList)
                        TextBoxFreqMtd.Items.Clear()
                        TextBoxFreqMtd.Items.Add("DOW")
                        TextBoxFreqMtd.Items.Add("MONTH")

                    ElseIf txtFrequencyDesc.Text = "BI-WEEKLY" Or txtFrequencyDesc.Text = "TRI-WEEKLY" Or txtFrequencyDesc.Text = "FORTNIGHTLY" Then

                    ElseIf txtFrequencyDesc.Text = "TWICE-WEEKLY" Or txtFrequencyDesc.Text = "THRICE-WEEKLY" Or txtFrequencyDesc.Text = "FOUR-TIMES-WEEKLY" Or txtFrequencyDesc.Text = "FIVE-TIMES-WEEKLY" Or txtFrequencyDesc.Text = "SIX-TIMES-WEEKLY" Then
                        'Dim TextBoxFreqMTD As DropDownList = CType(grvFreqDetails.Rows(0).Cells(0).FindControl("ddlFreqMTDGV"), DropDownList)
                        TextBoxFreqMtd.Items.Clear()
                        TextBoxFreqMtd.Items.Add("DOW")
                        TextBoxFreqMtd.Enabled = False

                    Else
                        'Dim TextBoxFreqMTD As DropDownList = CType(grvFreqDetails.Rows(0).Cells(0).FindControl("ddlFreqMTDGV"), DropDownList)
                        'TextBoxFreqMTD.Items.Clear()
                        'TextBoxFreqMTD.Items.Add(gFreqMTD)
                    End If
                    'sen

                    TextBoxFreqMtd.Text = Convert.ToString(dtFreq.Rows(i)("FreqMtd").ToString.Trim())

                    Dim TextBoxDayNo As TextBox = CType(grvFreqDetails.Rows(i).Cells(0).FindControl("txtDayNoGV"), TextBox)
                    TextBoxDayNo.Text = Convert.ToString(dtFreq.Rows(i)("DayNo"))

                    Dim TextBoxWeekNo As TextBox = CType(grvFreqDetails.Rows(i).Cells(0).FindControl("txtWeekNoGV"), TextBox)
                    TextBoxWeekNo.Text = Convert.ToString(dtFreq.Rows(i)("WeekNo"))

                    Dim TextBoxMonthNo As TextBox = CType(grvFreqDetails.Rows(i).Cells(0).FindControl("txtMonthNoGV"), TextBox)
                    TextBoxMonthNo.Text = Convert.ToString(dtFreq.Rows(i)("MonthNo"))

                    'MessageBox.Message.Alert(Page, Convert.ToString(dtFreq.Rows(i)("WeekDOW")), "str")
                    Dim TextBoxWeekDOW As DropDownList = CType(grvFreqDetails.Rows(i).Cells(0).FindControl("ddlWeekDOWGV"), DropDownList)
                    TextBoxWeekDOW.Text = Convert.ToString(dtFreq.Rows(i)("WeekDOW"))

                    Dim TextBoxLocation As DropDownList = CType(grvFreqDetails.Rows(i).Cells(0).FindControl("ddlLocationGV"), DropDownList)
                    TextBoxLocation.Text = Convert.ToString(dtFreq.Rows(i)("Location"))

                    Dim TextBoxBranchID As DropDownList = CType(grvFreqDetails.Rows(i).Cells(0).FindControl("ddlBranchIDGV"), DropDownList)
                    TextBoxBranchID.Text = Convert.ToString(dtFreq.Rows(i)("BranchID"))

                    Dim TextBoxContractNo As TextBox = CType(grvFreqDetails.Rows(i).Cells(0).FindControl("txtContractNoGVF"), TextBox)
                    TextBoxContractNo.Text = Convert.ToString(dtFreq.Rows(i)("ContractNo"))

                    Dim TextBoxSourceSQLID As TextBox = CType(grvFreqDetails.Rows(i).Cells(0).FindControl("txtSourceSQLIDGVF"), TextBox)
                    TextBoxSourceSQLID.Text = Convert.ToString(dtFreq.Rows(i)("SourceSQLID"))

                    Dim TextBoxRcno As TextBox = CType(grvFreqDetails.Rows(i).Cells(0).FindControl("txtRcnoGVF"), TextBox)
                    TextBoxRcno.Text = Convert.ToString(dtFreq.Rows(i)("Rcno"))




                    If TextBoxFreqMtd.Text = "DATE" Then
                        TextBoxMonthNo.Enabled = False
                        TextBoxDayNo.Enabled = True
                        TextBoxWeekNo.Enabled = False
                        TextBoxWeekDOW.Enabled = False

                    ElseIf TextBoxFreqMtd.Text = "DOW" Then
                        TextBoxMonthNo.Enabled = False
                        TextBoxDayNo.Enabled = False
                        TextBoxWeekNo.Enabled = True
                        TextBoxWeekDOW.Enabled = True

                        If txtFrequencyDesc.Text = "TWICE-WEEKLY" Or txtFrequencyDesc.Text = "THRICE-WEEKLY" Or txtFrequencyDesc.Text = "FOUR-TIMES-WEEKLY" Or txtFrequencyDesc.Text = "FIVE-TIMES-WEEKLY" Or txtFrequencyDesc.Text = "SIX-TIMES-WEEKLY" Then

                            TextBoxWeekNo.Enabled = False
                        End If


                    ElseIf TextBoxFreqMtd.Text = "MONTH" Then
                        TextBoxMonthNo.Enabled = True
                        TextBoxDayNo.Enabled = True
                        TextBoxWeekNo.Enabled = False
                        TextBoxWeekDOW.Enabled = False

                    End If

                Next i


                'AddNewRowFreq()
                'SetPreviousDataFreq()
            Else
                FirstGridViewRowFreq()
            End If


            'Delete last row

            'SetRowDataFreq()

            Dim dtFreq2 As DataTable = CType(ViewState("CurrentTableFreq"), DataTable)
            Dim drCurrentRowFreq2 As DataRow = Nothing

            If dtFreq2.Rows.Count > 1 Then
                dtFreq2.Rows.Remove(dtFreq2.Rows(dtFreq2.Rows.Count - 1))
                drCurrentRowFreq2 = dtFreq2.NewRow()
                ViewState("CurrentTableFreq") = dtFreq2
                grvFreqDetails.DataSource = dtFreq2
                grvFreqDetails.DataBind()

                'SetPreviousDataFreq()

            End If
            'End: Frequency


            'Start: Target
            SetRowData()


            Dim dtScdrTarget1 As DataTable = CType(ViewState("CurrentTable"), DataTable)
            Dim drCurrentRowTarget1 As DataRow = Nothing

            For i As Integer = 0 To grvTargetDetails.Rows.Count - 1
                dtScdrTarget1.Rows.Remove(dtScdrTarget1.Rows(0))
                drCurrentRowTarget1 = dtScdrTarget1.NewRow()
                ViewState("CurrentTable") = dtScdrTarget1
                grvTargetDetails.DataSource = dtScdrTarget1
                grvTargetDetails.DataBind()

                SetPreviousData()

            Next i


            FirstGridViewRowTarget()

            Dim cmdContratTarget As MySqlCommand = New MySqlCommand
            cmdContratTarget.CommandType = CommandType.Text
            cmdContratTarget.CommandText = "SELECT * FROM tblcontractServingTarget where SourceSQLID=" & Convert.ToInt32(txtOrigCode.Text)
            cmdContratTarget.Connection = conn

            Dim drTarget As MySqlDataReader = cmdContratTarget.ExecuteReader()
            Dim dtTarget As New DataTable
            dtTarget.Load(drTarget)

            Dim TotDetailRecords = dtTarget.Rows.Count
            If dtTarget.Rows.Count > 0 Then


                Dim rowIndex = 0

                For Each row As DataRow In dtTarget.Rows
                    If (TotDetailRecords > (rowIndex + 1)) Then

                        AddNewRow()
                    End If

                    Dim TextBoxTargetDesc As DropDownList = CType(grvTargetDetails.Rows(rowIndex).Cells(0).FindControl("ddlTargetDescGV"), DropDownList)
                    TextBoxTargetDesc.Text = Convert.ToString(dtTarget.Rows(rowIndex)("TargetDesc"))

                    Dim TextBoxTargtId As TextBox = CType(grvTargetDetails.Rows(rowIndex).Cells(0).FindControl("txtTargtIdGV"), TextBox)
                    TextBoxTargtId.Text = Convert.ToString(dtTarget.Rows(rowIndex)("TargetID"))

                    Dim TextBoxContractNo As TextBox = CType(grvTargetDetails.Rows(rowIndex).Cells(0).FindControl("txtContractNoGV"), TextBox)
                    TextBoxContractNo.Text = Convert.ToString(dtTarget.Rows(rowIndex)("ContractNo"))

                    Dim TextBoxServiceID As TextBox = CType(grvTargetDetails.Rows(rowIndex).Cells(0).FindControl("txtServiceIDGV"), TextBox)
                    TextBoxServiceID.Text = Convert.ToString(dtTarget.Rows(rowIndex)("ServiceID"))

                    Dim TextBoxSourceSQLID As TextBox = CType(grvTargetDetails.Rows(rowIndex).Cells(0).FindControl("txtSourceSQLIDGV"), TextBox)
                    TextBoxSourceSQLID.Text = Convert.ToString(dtTarget.Rows(rowIndex)("SourceSQLID"))

                    Dim TextBoxRcno As TextBox = CType(grvTargetDetails.Rows(rowIndex).Cells(0).FindControl("txtRcnoGV"), TextBox)
                    TextBoxRcno.Text = Convert.ToString(dtTarget.Rows(rowIndex)("Rcno"))


                    Dim Query As String
                    Dim TextBoxTargetDesc2 As DropDownList = CType(grvTargetDetails.Rows(rowIndex).Cells(0).FindControl("ddlTargetDescGV"), DropDownList)
                    Query = "Select * from tblTarget"
                    PopulateDropDownList(Query, "descrip1", "descrip1", TextBoxTargetDesc2)
                    rowIndex += 1

                Next row

                AddNewRow()
                SetPreviousData()

            Else
                FirstGridViewRowTarget()
                Dim Query As String
                Dim TextBoxTargetDesc As DropDownList = CType(grvTargetDetails.Rows(0).Cells(0).FindControl("ddlTargetDescGV"), DropDownList)
                Query = "Select * from tblTarget"

                PopulateDropDownList(Query, "descrip1", "descrip1", TextBoxTargetDesc)
            End If

            'End: Target

            conn.Close()

        Catch ex As Exception
            MessageBox.Message.Alert(Page, "Error!!! " + ex.Message.ToString, "str")
        End Try
        'txtMode.Text = "Edit"
        'DisableControls()
        'btnDelete.Enabled = True
        'btnDelete.ForeColor = System.Drawing.Color.Black
        'btnQuit.Enabled = True
        'btnQuit.ForeColor = System.Drawing.Color.Black
        'btnCopyAdd.Enabled = True
        'btnCopyAdd.ForeColor = System.Drawing.Color.Black
        'txtID.Enabled = False


        txtMode.Text = "Edit"
        'DisableControls()
        btnDelete.Enabled = True
        btnDelete.ForeColor = System.Drawing.Color.Black
        btnQuit.Enabled = True
        btnQuit.ForeColor = System.Drawing.Color.Black
    End Sub


    Protected Sub gvTeam_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvTeam.SelectedIndexChanged


        If gvTeam.SelectedRow.Cells(1).Text = "&nbsp" Then
            txtFrequency.Text = " "
        Else
            txtFrequency.Text = gvTeam.SelectedRow.Cells(1).Text
        End If


        If gvTeam.SelectedRow.Cells(1).Text = "&nbsp" Then
            txtFrequencyDesc.Text = " "
        Else
            txtFrequencyDesc.Text = gvTeam.SelectedRow.Cells(1).Text
        End If

        txtMonthByWhichMonth.Text = gvTeam.SelectedRow.Cells(2).Text
        txtDOWByWhichWeek.Text = gvTeam.SelectedRow.Cells(3).Text

        grvFreqDetails.Enabled = True
        grvTargetDetails.Enabled = True

        If txtFrequencyDesc.Text = "TWICE-WEEKLY" Or txtFrequencyDesc.Text = "TWICE-MONTHLY" Then
            txtNoofSvcInterval.Text = 2

        ElseIf txtFrequencyDesc.Text = "THRICE-MONTHLY" Or txtFrequencyDesc.Text = "THRICE-WEEKLY" Then
            txtNoofSvcInterval.Text = 3
        ElseIf txtFrequencyDesc.Text = "TRI-WEEKLY" Then
            txtNoofSvcInterval.Text = 3
        ElseIf txtFrequencyDesc.Text = "FOUR-TIMES-WEEKLY" Or txtFrequencyDesc.Text = "FOUR-TIMES-MONTHLY" Then
            txtNoofSvcInterval.Text = 4
        ElseIf txtFrequencyDesc.Text = "FIVE-TIMES-WEEKLY" Or txtFrequencyDesc.Text = "FIVE-TIMES-MONTHLY" Then
            txtNoofSvcInterval.Text = 5
        ElseIf txtFrequencyDesc.Text = "SIX-TIMES-WEEKLY" Or txtFrequencyDesc.Text = "SIX-TIMES-MONTHLY" Then
            txtNoofSvcInterval.Text = 6
        Else
            txtNoofSvcInterval.Text = 1
        End If



        lblInterval.Text = ""
        Select Case txtFrequency.Text.ToUpper
            Case "DAILY"
                lblInterval.Text = "Days"
            Case "WEEKLY", "TWICE-WEEKLY"
                lblInterval.Text = "Wks"
            Case "BI-WEEKLY", "FORTNIGHTLY"
                lblInterval.Text = "Bi-wks"
            Case "TRI-WEEKLY"
                lblInterval.Text = "Tri-wks"
            Case "MONTHLY", "THRICE-MONTHLY"
                lblInterval.Text = "Mths"
            Case "BI-MONTHLY"
                lblInterval.Text = "Bi-mths"
            Case "QUARTERLY"
                lblInterval.Text = "Quarterly"
            Case "HALF-ANNUALLY"
                lblInterval.Text = "Half-Annually"
            Case "ANNUALLY"
                lblInterval.Text = "Annually"
            Case "BI-ANNUALLY"
                lblInterval.Text = "Bi-Annually"
        End Select


        Dim lInterval As Integer
        lInterval = txtNoofSvcInterval.Text


        Dim interval As Integer
        Dim endDate As Date
        If txtFrequencyDesc.Text = "MONTHLY" Then

            Dim monthno As Integer = DateDiff(DateInterval.Month, CDate(txtServStart.Text), CDate(txtServEnd.Text))

            If monthno > 0 Then
                endDate = CDate(txtServStart.Text).AddMonths(monthno).AddDays(-1)
                If Date.Compare(CDate(txtServEnd.Text), endDate) > 0 Then
                    monthno = monthno + 1
                End If
            End If
            txtNoofInterval.Text = monthno
            If txtNoofSvcInterval.Text.Trim = "" Then txtNoofSvcInterval.Text = "1"


        ElseIf txtFrequencyDesc.Text = "DAILY" Then
            txtNoofInterval.Text = DateDiff(DateInterval.Day, CDate(txtServStart.Text), CDate(txtServEnd.Text)) + 1
            If txtNoofSvcInterval.Text.Trim = "" Then txtNoofSvcInterval.Text = "1"
        ElseIf txtFrequencyDesc.Text = "WEEKLY" Then
            interval = DateDiff(DateInterval.Weekday, CDate(txtServStart.Text), CDate(txtServEnd.Text).AddDays(1))
            If interval > 0 Then
                endDate = CDate(txtServStart.Text).AddDays(7 * interval).AddDays(-1)
                If Date.Compare(CDate(txtServEnd.Text), endDate) > 0 Then
                    interval = interval + 1
                End If
            End If
            txtNoofInterval.Text = interval
            If txtNoofSvcInterval.Text.Trim = "" Then txtNoofSvcInterval.Text = "1"
        ElseIf txtFrequencyDesc.Text = "BI-WEEKLY" Or txtFrequencyDesc.Text = "FORTNIGHTLY" Then
            Dim weekNo As Integer = DateDiff(DateInterval.Weekday, CDate(txtServStart.Text), CDate(txtServEnd.Text).AddDays(1))
            If weekNo > 0 Then
                endDate = CDate(txtServStart.Text).AddDays(7 * weekNo).AddDays(-1)
                If Date.Compare(CDate(txtServEnd.Text), endDate) > 0 Then
                    weekNo = weekNo + 1
                End If
            End If
            Dim tempInt As Decimal = Math.Round(weekNo / 2, 2)
            If tempInt.ToString.Contains(".") Then tempInt = tempInt + 1
            txtNoofInterval.Text = tempInt
            If txtNoofInterval.Text.Contains(".") Then
                txtNoofInterval.Text = txtNoofInterval.Text.Substring(0, txtNoofInterval.Text.IndexOf("."))
            End If
            If txtNoofSvcInterval.Text.Trim = "" Then txtNoofSvcInterval.Text = "1"
        ElseIf txtFrequencyDesc.Text = "TWICE-WEEKLY" Then
            interval = DateDiff(DateInterval.Weekday, CDate(txtServStart.Text), CDate(txtServEnd.Text).AddDays(1))
            If interval > 0 Then
                endDate = CDate(txtServStart.Text).AddDays(7 * interval).AddDays(-1)
                If Date.Compare(CDate(txtServEnd.Text), endDate) > 0 Then
                    interval = interval + 1
                End If
            End If
            txtNoofInterval.Text = interval

            If txtNoofSvcInterval.Text.Trim = "" Then txtNoofSvcInterval.Text = "2"

        ElseIf txtFrequencyDesc.Text = "TRI-WEEKLY" Then
            Dim weekNo As Integer = DateDiff(DateInterval.Weekday, CDate(txtServStart.Text), CDate(txtServEnd.Text).AddDays(1))
            If weekNo > 0 Then
                endDate = CDate(txtServStart.Text).AddDays(7 * weekNo).AddDays(-1)
                If Date.Compare(CDate(txtServEnd.Text), endDate) > 0 Then
                    weekNo = weekNo + 1
                End If
            End If

            Dim tempInt As Decimal = Math.Round(weekNo / 3, 2)
            If tempInt.ToString.Contains(".") Then tempInt = tempInt + 1
            txtNoofInterval.Text = tempInt
            If txtNoofInterval.Text.Contains(".") Then
                txtNoofInterval.Text = txtNoofInterval.Text.Substring(0, txtNoofInterval.Text.IndexOf("."))
            End If
            If txtNoofSvcInterval.Text.Trim = "" Then txtNoofSvcInterval.Text = "3"

            'Dim weekNo As Integer = DateDiff(DateInterval.Weekday, CDate(txtServStart.Text), CDate(txtServEnd.Text).AddDays(1))
            'If weekNo > 0 Then
            '    endDate = CDate(mskServiceStartDate.Text).AddDays(7 * weekNo).AddDays(-1)
            '    If Date.Compare(CDate(txtServEnd.Text), endDate) > 0 Then
            '        weekNo = weekNo + 1
            '    End If
            'End If

            'Dim tempInt As Decimal = Math.Round(weekNo / 3, 2)
            'If tempInt.ToString.Contains(".") Then tempInt = tempInt + 1
            'txtNoofInterval.Text = tempInt
            'If txtNoofInterval.Text.Contains(".") Then
            '    txtNoofInterval.Text = txtNoofInterval.Text.Substring(0, txtNoofInterval.Text.IndexOf("."))
            'End If
            'If txtNoofSvcInterval.Text.Trim = "" Then txtNoofSvcInterval.Text = "3"
        ElseIf txtFrequencyDesc.Text = "THRICE-WEEKLY" Then
            interval = DateDiff(DateInterval.Weekday, CDate(txtServStart.Text), CDate(txtServEnd.Text).AddDays(1))
            If interval > 0 Then
                endDate = CDate(txtServStart.Text).AddDays(7 * interval).AddDays(-1)
                If Date.Compare(CDate(txtServEnd.Text), endDate) > 0 Then
                    interval = interval + 1
                End If
            End If
            txtNoofInterval.Text = interval


            If txtNoofSvcInterval.Text.Trim = "" Then txtNoofSvcInterval.Text = "3"
        ElseIf txtFrequencyDesc.Text = "FOUR-TIMES-WEEKLY" Then
            interval = DateDiff(DateInterval.Weekday, CDate(txtServStart.Text), CDate(txtServEnd.Text).AddDays(1))
            If interval > 0 Then
                endDate = CDate(txtServStart.Text).AddDays(7 * interval).AddDays(-1)
                If Date.Compare(CDate(txtServEnd.Text), endDate) > 0 Then
                    interval = interval + 1
                End If
            End If
            txtNoofInterval.Text = interval


            If txtNoofSvcInterval.Text.Trim = "" Then txtNoofSvcInterval.Text = "4"
        ElseIf txtFrequencyDesc.Text = "FIVE-TIMES-WEEKLY" Then
            interval = DateDiff(DateInterval.Weekday, CDate(txtServStart.Text), CDate(txtServEnd.Text).AddDays(1))
            If interval > 0 Then
                endDate = CDate(txtServStart.Text).AddDays(7 * interval).AddDays(-1)
                If Date.Compare(CDate(txtServEnd.Text), endDate) > 0 Then
                    interval = interval + 1
                End If
            End If
            txtNoofInterval.Text = interval


            If txtNoofSvcInterval.Text.Trim = "" Then txtNoofSvcInterval.Text = "5"

        ElseIf txtFrequencyDesc.Text = "SIX-TIMES-WEEKLY" Then
            interval = DateDiff(DateInterval.Weekday, CDate(txtServStart.Text), CDate(txtServEnd.Text).AddDays(1))
            If interval > 0 Then
                endDate = CDate(txtServStart.Text).AddDays(7 * interval).AddDays(-1)
                If Date.Compare(CDate(txtServEnd.Text), endDate) > 0 Then
                    interval = interval + 1
                End If
            End If
            txtNoofInterval.Text = interval


            If txtNoofSvcInterval.Text.Trim = "" Then txtNoofSvcInterval.Text = "6"
        ElseIf txtFrequencyDesc.Text = "BI-MONTHLY" Then
            Dim monthNo As Integer = DateDiff(DateInterval.Month, CDate(txtServStart.Text), CDate(txtServEnd.Text))

            'If monthNo > 0 Then
            '    endDate = CDate(mskServiceStartDate.Text).AddMonths(monthNo).AddDays(-1)
            '    If Date.Compare(endDate, CDate(mskServiceEndDate.Text)) > 0 Then
            '        monthNo = monthNo - 1
            '    End If
            'End If
            'endDate = CDate(mskServiceStartDate.Text).AddMonths(monthNo + 1).AddDays(-1)
            'If Date.Compare(endDate, CDate(mskServiceEndDate.Text)) = 0 Then
            '    monthNo = monthNo + 1
            'End If
            If monthNo > 0 Then
                endDate = CDate(txtServStart.Text).AddMonths(monthNo).AddDays(-1)
                If Date.Compare(CDate(txtServEnd.Text), endDate) > 0 Then
                    monthNo = monthNo + 1
                End If
            End If

            txtNoofInterval.Text = Math.Round(monthNo / 2, 2)
            If txtNoofInterval.Text.Contains(".") Then
                txtNoofInterval.Text = txtNoofInterval.Text.Substring(0, txtNoofInterval.Text.IndexOf("."))
            End If
            If txtNoofSvcInterval.Text.Trim = "" Then txtNoofSvcInterval.Text = "1"

        ElseIf txtFrequencyDesc.Text = "TWICE-MONTHLY" Then
            Dim monthno As Integer = DateDiff(DateInterval.Month, CDate(txtServStart.Text), CDate(txtServEnd.Text))

            'If monthno > 0 Then
            '    endDate = CDate(mskServiceStartDate.Text).AddMonths(monthno).AddDays(-1)
            '    If Date.Compare(endDate, CDate(mskServiceEndDate.Text)) > 0 Then
            '        monthno = monthno - 1
            '    End If
            'End If
            'endDate = CDate(mskServiceStartDate.Text).AddMonths(monthno + 1).AddDays(-1)
            'If Date.Compare(endDate, CDate(mskServiceEndDate.Text)) = 0 Then
            '    monthno = monthno + 1
            'End If
            If monthno > 0 Then
                endDate = CDate(txtServStart.Text).AddMonths(monthno).AddDays(-1)
                If Date.Compare(CDate(txtServEnd.Text), endDate) > 0 Then
                    monthno = monthno + 1
                End If
            End If
            txtNoofInterval.Text = monthno

            If txtNoofSvcInterval.Text.Trim = "" Then txtNoofSvcInterval.Text = "2"
        ElseIf txtFrequencyDesc.Text = "THRICE-MONTHLY" Then
            Dim monthno As Integer = DateDiff(DateInterval.Month, CDate(txtServStart.Text), CDate(txtServEnd.Text))

            'If monthno > 0 Then
            '    endDate = CDate(mskServiceStartDate.Text).AddMonths(monthno).AddDays(-1)
            '    If Date.Compare(endDate, CDate(mskServiceEndDate.Text)) > 0 Then
            '        monthno = monthno - 1
            '    End If
            'End If
            'endDate = CDate(mskServiceStartDate.Text).AddMonths(monthno + 1).AddDays(-1)
            'If Date.Compare(endDate, CDate(mskServiceEndDate.Text)) = 0 Then
            '    monthno = monthno + 1
            'End If
            If monthno > 0 Then
                endDate = CDate(txtServStart.Text).AddMonths(monthno).AddDays(-1)
                If Date.Compare(CDate(txtServEnd.Text), endDate) > 0 Then
                    monthno = monthno + 1
                End If
            End If
            txtNoofInterval.Text = monthno

            If txtNoofSvcInterval.Text.Trim = "" Then txtNoofSvcInterval.Text = "3"

        ElseIf txtFrequencyDesc.Text = "FOUR-TIMES-MONTHLY" Then
            Dim monthno As Integer = DateDiff(DateInterval.Month, CDate(txtServStart.Text), CDate(txtServEnd.Text))

            'If monthno > 0 Then
            '    endDate = CDate(mskServiceStartDate.Text).AddMonths(monthno).AddDays(-1)
            '    If Date.Compare(endDate, CDate(mskServiceEndDate.Text)) > 0 Then
            '        monthno = monthno - 1
            '    End If
            'End If
            'endDate = CDate(mskServiceStartDate.Text).AddMonths(monthno + 1).AddDays(-1)
            'If Date.Compare(endDate, CDate(mskServiceEndDate.Text)) = 0 Then
            '    monthno = monthno + 1
            'End If
            If monthno > 0 Then
                endDate = CDate(txtServStart.Text).AddMonths(monthno).AddDays(-1)
                If Date.Compare(CDate(txtServEnd.Text), endDate) > 0 Then
                    monthno = monthno + 1
                End If
            End If
            txtNoofInterval.Text = monthno

            If txtNoofSvcInterval.Text.Trim = "" Then txtNoofSvcInterval.Text = "4"
        ElseIf txtFrequencyDesc.Text = "QUARTERLY" Then
            Dim monthNo As Integer = DateDiff(DateInterval.Month, CDate(txtServStart.Text), CDate(txtServEnd.Text))

            'If monthNo > 0 Then
            '    endDate = CDate(mskServiceStartDate.Text).AddMonths(monthNo).AddDays(-1)
            '    If Date.Compare(endDate, CDate(mskServiceEndDate.Text)) > 0 Then
            '        monthNo = monthNo - 1
            '    End If
            'End If
            'endDate = CDate(mskServiceStartDate.Text).AddMonths(monthNo + 1).AddDays(-1)
            'If Date.Compare(endDate, CDate(mskServiceEndDate.Text)) = 0 Then
            '    monthNo = monthNo + 1
            'End If
            If monthNo > 0 Then
                endDate = CDate(txtServStart.Text).AddMonths(monthNo).AddDays(-1)
                If Date.Compare(CDate(txtServEnd.Text), endDate) > 0 Then
                    monthNo = monthNo + 1
                End If
            End If
            txtNoofInterval.Text = Math.Round(monthNo / 3, 2)
            If txtNoofInterval.Text.Contains(".") Then
                txtNoofInterval.Text = txtNoofInterval.Text.Substring(0, txtNoofInterval.Text.IndexOf("."))
            End If
            If txtNoofSvcInterval.Text.Trim = "" Then txtNoofSvcInterval.Text = "1"
        ElseIf txtFrequencyDesc.Text = "HALF-ANNUALLY" Then
            Dim monthNo As Integer = DateDiff(DateInterval.Month, CDate(txtServStart.Text), CDate(txtServEnd.Text))

            'If monthNo > 0 Then
            '    endDate = CDate(mskServiceStartDate.Text).AddMonths(monthNo).AddDays(-1)
            '    If Date.Compare(endDate, CDate(mskServiceEndDate.Text)) > 0 Then
            '        monthNo = monthNo - 1
            '    End If
            'End If
            'endDate = CDate(mskServiceStartDate.Text).AddMonths(monthNo + 1).AddDays(-1)
            'If Date.Compare(endDate, CDate(mskServiceEndDate.Text)) = 0 Then
            '    monthNo = monthNo + 1
            'End If
            If monthNo > 0 Then
                endDate = CDate(txtServStart.Text).AddMonths(monthNo).AddDays(-1)
                If Date.Compare(CDate(txtServEnd.Text), endDate) > 0 Then
                    monthNo = monthNo + 1
                End If
            End If
            txtNoofInterval.Text = Math.Round((monthNo / 6), 2)
            If txtNoofInterval.Text.Contains(".") Then
                txtNoofInterval.Text = txtNoofInterval.Text.Substring(0, txtNoofInterval.Text.IndexOf("."))
            End If
            If txtNoofSvcInterval.Text.Trim = "" Then txtNoofSvcInterval.Text = "1"
        ElseIf txtFrequencyDesc.Text = "ANNUALLY" Then
            Dim yearNo As Integer = DateDiff(DateInterval.Year, CDate(txtServStart.Text), CDate(txtServEnd.Text))

            'If yearNo > 0 Then
            '    endDate = CDate(mskServiceStartDate.Text).AddYears(yearNo).AddDays(-1)
            '    If Date.Compare(endDate, CDate(mskServiceEndDate.Text)) > 0 Then
            '        yearNo = yearNo - 1
            '    End If
            'End If
            'endDate = CDate(mskServiceStartDate.Text).AddYears(yearNo + 1).AddDays(-1)
            'If Date.Compare(endDate, CDate(mskServiceEndDate.Text)) = 0 Then
            '    yearNo = yearNo + 1
            'End If

            If yearNo > 0 Then
                endDate = CDate(txtServStart.Text).AddYears(yearNo).AddDays(-1)
                If Date.Compare(CDate(txtServEnd.Text), endDate) > 0 Then
                    yearNo = yearNo + 1
                End If
            End If

            txtNoofInterval.Text = yearNo
            If txtNoofSvcInterval.Text.Trim = "" Then txtNoofSvcInterval.Text = "1"
        ElseIf txtFrequencyDesc.Text = "BI-ANNUALLY" Then
            Dim yearNo As Integer = DateDiff(DateInterval.Year, CDate(txtServStart.Text), CDate(txtServEnd.Text))

            'If yearNo > 0 Then
            '    endDate = CDate(mskServiceStartDate.Text).AddYears(yearNo).AddDays(-1)
            '    If Date.Compare(endDate, CDate(mskServiceEndDate.Text)) > 0 Then
            '        yearNo = yearNo - 1
            '    End If
            'End If
            'endDate = CDate(mskServiceStartDate.Text).AddYears(yearNo + 1).AddDays(-1)
            'If Date.Compare(endDate, CDate(mskServiceEndDate.Text)) = 0 Then
            '    yearNo = yearNo + 1
            'End If
            If yearNo > 0 Then
                endDate = CDate(txtServStart.Text).AddYears(yearNo).AddDays(-1)
                If Date.Compare(CDate(txtServEnd.Text), endDate) > 0 Then
                    yearNo = yearNo + 1
                End If
            End If

            txtNoofInterval.Text = Math.Round(yearNo / 2, 2)
            If txtNoofInterval.Text.Contains(".") Then
                txtNoofInterval.Text = txtNoofInterval.Text.Substring(0, txtNoofInterval.Text.IndexOf("."))
            End If
            If txtNoofSvcInterval.Text.Trim = "" Then txtNoofSvcInterval.Text = "1"
        Else
            txtNoofInterval.Text = 1
            If txtNoofSvcInterval.Text.Trim = "" Then txtNoofSvcInterval.Text = "1"


        End If
        fGetInterval()
        txtNoService.Text = CDec((txtNoofSvcInterval.Text)) * CDec((txtNoofInterval.Text))
        'lblTotal.Text = (Math.Round(CDec((txtValue.Text)) * CDec((txtNoService.Text)), 4))

        '''''''''''''''''''''''''''''''''''''''''
        If Convert.ToInt64(txtNoofInterval.Text) > 0 Then
            txtValuePerService.Text = (Convert.ToDecimal(txtAgreedValue.Text) / Convert.ToInt64(txtNoofInterval.Text)).ToString("N2")
        Else
            Dim message As String = "alert('This Frequency CANNOT be selected within this Date Range!!!')"
            ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)
            Exit Sub

        End If


        '''''''''''''''''''''''''''''''''''''''''
        'Start:Frequency

        ' 'Delete first row

        SetRowDataFreq()

        Dim dtFreq As DataTable = CType(ViewState("CurrentTableFreq"), DataTable)
        Dim drCurrentRowFreq As DataRow = Nothing

        For i As Integer = 0 To grvFreqDetails.Rows.Count - 1
            dtFreq.Rows.Remove(dtFreq.Rows(0))
            drCurrentRowFreq = dtFreq.NewRow()
            ViewState("CurrentTableFreq") = dtFreq
            grvFreqDetails.DataSource = dtFreq
            grvFreqDetails.DataBind()

            SetPreviousDataFreq()
        Next i

        FirstGridViewRowFreq()


        Dim TextBoxSeqNo As TextBox = CType(grvFreqDetails.Rows(0).Cells(0).FindControl("txtSeqNoGV"), TextBox)
        Dim TextBoxFreqMTD As DropDownList = CType(grvFreqDetails.Rows(0).Cells(1).FindControl("ddlFreqMTDGV"), DropDownList)
        Dim TextBoxMonthNo As TextBox = CType(grvFreqDetails.Rows(0).Cells(2).FindControl("txtMonthNoGV"), TextBox)
        Dim TextBoxDayNo As TextBox = CType(grvFreqDetails.Rows(0).Cells(3).FindControl("txtDayNoGV"), TextBox)
        Dim TextBoxWeekNo As TextBox = CType(grvFreqDetails.Rows(0).Cells(4).FindControl("txtWeekNoGV"), TextBox)
        Dim TextBoxWeekDOW As DropDownList = CType(grvFreqDetails.Rows(0).Cells(5).FindControl("ddlWeekDOWGV"), DropDownList)
        Dim TextBoxLocation As DropDownList = CType(grvFreqDetails.Rows(0).Cells(6).FindControl("ddlLocationGV"), DropDownList)
        Dim TextBoxBranchID As DropDownList = CType(grvFreqDetails.Rows(0).Cells(7).FindControl("ddlBranchIDGV"), DropDownList)
        Dim TextBoxContractNo As TextBox = CType(grvFreqDetails.Rows(0).Cells(8).FindControl("txtContractNoGVF"), TextBox)
        Dim TextBoxSourceSQLID As TextBox = CType(grvFreqDetails.Rows(0).Cells(9).FindControl("txtSourceSQLIDGVF"), TextBox)
        Dim TextBoxRcno As TextBox = CType(grvFreqDetails.Rows(0).Cells(10).FindControl("txtRcnoGVF"), TextBox)

        If txtFrequencyDesc.Text = "DAILY" Then
            gSeq = "001"

            gFreqMTD = "DAY"
            AddNewRowFreq()
            TextBoxFreqMTD.Enabled = False
            TextBoxMonthNo.Enabled = False
            TextBoxDayNo.Enabled = False
            TextBoxWeekNo.Enabled = False
            TextBoxWeekDOW.Enabled = False
            TextBoxLocation.Enabled = False
            TextBoxBranchID.Enabled = False
            TextBoxContractNo.Enabled = False
            TextBoxSourceSQLID.Enabled = False
        ElseIf txtFrequencyDesc.Text = "WEEKLY" Then
            gFreqMTD = "DOW"
            gSeq = "001"
            gDOW = CDate(txtServStart.Text).DayOfWeek.ToString
            AddNewRowFreq()
            TextBoxFreqMTD.Enabled = False
            TextBoxMonthNo.Enabled = False
            TextBoxDayNo.Enabled = False
            TextBoxWeekNo.Enabled = False
            'TextBoxWeekDOW.Enabled = False
            'TextBoxLocation.Enabled = False
            'TextBoxBranchID.Enabled = False
            TextBoxContractNo.Enabled = False
            TextBoxSourceSQLID.Enabled = False
        ElseIf txtFrequencyDesc.Text = "BI-WEEKLY" Or txtFrequencyDesc.Text = "TRI-WEEKLY" Or txtFrequencyDesc.Text = "FORTNIGHTLY" Then
            'TextBoxSeqNo.Text = "001"
            gSeq = "001"
            gFreqMTD = "DOW"
            TextBoxWeekNo.Enabled = True
            For i As Integer = 0 To lInterval - 1
                gSeq = "00" + (i + 1).ToString()
                AddNewRowFreq()
            Next i
            'AddNewRowFreq()
            TextBoxFreqMTD.Enabled = False
            TextBoxMonthNo.Enabled = False
            TextBoxDayNo.Enabled = False

            TextBoxWeekDOW.Enabled = False
            'TextBoxLocation.Enabled = False
            'TextBoxBranchID.Enabled = False
            TextBoxContractNo.Enabled = False
            TextBoxSourceSQLID.Enabled = False
        ElseIf txtFrequencyDesc.Text = "TWICE-WEEKLY" Or txtFrequencyDesc.Text = "THRICE-WEEKLY" Or txtFrequencyDesc.Text = "FOUR-TIMES-WEEKLY" Or txtFrequencyDesc.Text = "FIVE-TIMES-WEEKLY" Or txtFrequencyDesc.Text = "SIX-TIMES-WEEKLY" Then
            'TextBoxSeqNo.Text = "001"
            gSeq = "001"
            gFreqMTD = "DOW"

            TextBoxFreqMTD.Enabled = False
            TextBoxWeekNo.Enabled = False
            For i As Integer = 0 To lInterval - 1
                gSeq = "00" + (i + 1).ToString()
                If i = 0 Then
                    gDOW = CDate(txtServStart.Text).DayOfWeek.ToString
                Else
                    gDOW = ""
                End If
                AddNewRowFreq()
            Next i
            'AddNewRowFreq()
            TextBoxFreqMTD.Enabled = False
            TextBoxMonthNo.Enabled = False
            TextBoxDayNo.Enabled = False

            TextBoxWeekDOW.Enabled = False
            'TextBoxLocation.Enabled = False
            'TextBoxBranchID.Enabled = False
            TextBoxContractNo.Enabled = False
            TextBoxSourceSQLID.Enabled = False
        ElseIf txtFrequencyDesc.Text = "MONTHLY" Or txtFrequencyDesc.Text = "TWICE-MONTHLY" Or txtFrequencyDesc.Text = "THRICE-MONTHLY" Or txtFrequencyDesc.Text = "FOUR-TIMES-MONTHLY" Then
            'TextBoxSeqNo.Text = "001"
            'gFreqMTD = "DOW"
            TextBoxWeekNo.Enabled = True
            For i As Integer = 0 To lInterval - 1
                gSeq = "00" + (i + 1).ToString()
                AddNewRowFreq()

                TextBoxFreqMTD.Enabled = False
                TextBoxMonthNo.Enabled = False
                TextBoxDayNo.Enabled = False

                '
            Next i
            'AddNewRowFreq()
            'TextBoxFreqMTD.Enabled = False
            'TextBoxMonthNo.Enabled = False
            'TextBoxDayNo.Enabled = False
            'TextBoxWeekNo.Enabled = True
            'TextBoxWeekDOW.Enabled = False
            'TextBoxLocation.Enabled = False
            'TextBoxBranchID.Enabled = False
            TextBoxContractNo.Enabled = False
            TextBoxSourceSQLID.Enabled = False
        ElseIf txtFrequencyDesc.Text = "BI-MONTHLY" Or txtFrequencyDesc.Text = "QUARTERLY" Or txtFrequencyDesc.Text = "HALF-ANNUALLY" Or txtFrequencyDesc.Text = "ANNUALLY" Or txtFrequencyDesc.Text = "BI-ANNUALLY" Then
            gSeq = "001"
            TextBoxWeekNo.Enabled = True
            AddNewRowFreq()
            TextBoxFreqMTD.Enabled = False
            TextBoxMonthNo.Enabled = False
            TextBoxDayNo.Enabled = False

            'TextBoxWeekDOW.Enabled = False
            'TextBoxLocation.Enabled = False
            'TextBoxBranchID.Enabled = False
            TextBoxContractNo.Enabled = False
            TextBoxSourceSQLID.Enabled = False
            'For i As Integer = 0 To lInterval
            '    If i = 0 Then
            '        AddNewRow()
            '    Else
            '        AddNewRowWithDetailRecFreq()

            '    End If

            'Next i

        ElseIf txtFrequencyDesc.Text = "ADHOC" Or txtFrequencyDesc.Text = "ONE-TIME" Then
            gSeq = "001"

            gFreqMTD = "DAY"
            AddNewRowFreq()
            TextBoxFreqMTD.Enabled = False
            TextBoxMonthNo.Enabled = False
            TextBoxDayNo.Enabled = False
            TextBoxWeekNo.Enabled = False
            TextBoxWeekDOW.Enabled = False
            TextBoxLocation.Enabled = False
            TextBoxBranchID.Enabled = False
            TextBoxContractNo.Enabled = False
            TextBoxSourceSQLID.Enabled = False

        End If

        'Delete last row

        SetRowDataFreq()

        Dim dtFreq2 As DataTable = CType(ViewState("CurrentTableFreq"), DataTable)
        Dim drCurrentRowFreq2 As DataRow = Nothing

        If dtFreq2.Rows.Count > 1 Then
            dtFreq2.Rows.Remove(dtFreq2.Rows(0))
            drCurrentRowFreq2 = dtFreq2.NewRow()
            ViewState("CurrentTableFreq") = dtFreq2
            grvFreqDetails.DataSource = dtFreq2
            grvFreqDetails.DataBind()

            SetPreviousDataFreq()

        End If

        '''''''''''''''''End:Frequency


    End Sub

    Protected Sub ddlFreqMTDGV_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try

            If grvTargetDetails.PageSize >= 10 Then
                grvTargetDetails.PageSize = TotDetailRecords + 1
                'TotDetailRecordsForPaging = TotDetailRecordsForPaging + 1
            End If

            'Dim lTargetDesciption As String
            SetRowData()
            Dim ddl1 As DropDownList = DirectCast(sender, DropDownList)

            Dim xrow1 As GridViewRow = CType(ddl1.NamingContainer, GridViewRow)
            Dim lblid1 As DropDownList = CType(xrow1.FindControl("ddlFreqMTDGV"), DropDownList)
            Dim lblid2 As TextBox = CType(xrow1.FindControl("txtMonthNoGV"), TextBox)
            Dim lblid3 As TextBox = CType(xrow1.FindControl("txtDayNoGV"), TextBox)
            Dim lblid4 As TextBox = CType(xrow1.FindControl("txtWeekNoGV"), TextBox)
            Dim lblid5 As DropDownList = CType(xrow1.FindControl("ddlWeekDOWGV"), DropDownList)

            Dim rowindex1 As Integer
            rowindex1 = xrow1.RowIndex

            'Dim TextBoxFreqMTD As DropDownList = CType(grvFreqDetails.Rows(rowindex1).Cells(1).FindControl("ddlFreqMTDGV"), DropDownList)
            Dim TextBoxMonthNo As TextBox = CType(grvFreqDetails.Rows(rowindex1).Cells(2).FindControl("txtMonthNoGV"), TextBox)
            Dim TextBoxDayNo As TextBox = CType(grvFreqDetails.Rows(rowindex1).Cells(3).FindControl("txtDayNoGV"), TextBox)
            Dim TextBoxWeekNo As TextBox = CType(grvFreqDetails.Rows(rowindex1).Cells(4).FindControl("txtWeekNoGV"), TextBox)
            Dim TextBoxWeekDOW As DropDownList = CType(grvFreqDetails.Rows(rowindex1).Cells(5).FindControl("ddlWeekDOWGV"), DropDownList)
            'Dim TextBoxLocation As DropDownList = CType(grvFreqDetails.Rows(rowindex1).Cells(6).FindControl("ddlLocationGV"), DropDownList)
            'Dim TextBoxBranchID As DropDownList = CType(grvFreqDetails.Rows(rowindex1).Cells(7).FindControl("ddlBranchIDGV"), DropDownList)
            'Dim TextBoxContractNo As TextBox = CType(grvFreqDetails.Rows(rowindex1).Cells(8).FindControl("txtContractNoGVF"), TextBox)
            'Dim TextBoxSourceSQLID As TextBox = CType(grvFreqDetails.Rows(rowindex1).Cells(9).FindControl("txtSourceSQLIDGVF"), TextBox)
            'Dim TextBoxRcno As TextBox = CType(grvFreqDetails.Rows(rowindex1).Cells(10).FindControl("txtRcnoGVF"), TextBox)

            'lblid2.Text = ""
            'lblid3.Text = ""
            'lblid4.Text = ""
            'lblid5.SelectedIndex = 0

            If lblid1.Text = "DATE" Then
                lblid2.Enabled = False
                lblid3.Enabled = True
                lblid4.Enabled = False
                lblid5.Enabled = False
                lblid2.Text = ""
                lblid3.Text = Day(CDate(Me.txtServStart.Text))
                lblid4.Text = ""
                lblid5.SelectedIndex = 0

            ElseIf lblid1.Text = "DOW" Then
                lblid2.Enabled = False
                lblid3.Enabled = False
                lblid4.Enabled = True
                lblid5.Enabled = True

                lblid2.Text = ""
                lblid3.Text = ""
                lblid4.Text = pWeekNumber(txtServStart.Text)
                lblid5.Text = CDate(txtServStart.Text).DayOfWeek.ToString()

            ElseIf lblid1.Text = "MONTH" Then
                lblid2.Enabled = True
                lblid3.Enabled = True
                lblid4.Enabled = False
                lblid5.Enabled = False

                TextBoxMonthNo.Text = "1"
                TextBoxDayNo.Text = Day(CDate(Me.txtServStart.Text))
                TextBoxWeekNo.Text = ""
                TextBoxWeekDOW.SelectedIndex = 0
                'lblid2.Text = ""
                'lblid3.Text = Day(CDate(Me.txtServStart.Text))
                'lblid4.Text = ""
                'lblid5.SelectedIndex = 0

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub ddlWeekDOWGV_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try

            If grvTargetDetails.PageSize >= 10 Then
                grvTargetDetails.PageSize = TotDetailRecords + 1
                'TotDetailRecordsForPaging = TotDetailRecordsForPaging + 1
            End If

            'Dim lTargetDesciption As String
            SetRowData()
            Dim ddl1 As DropDownList = DirectCast(sender, DropDownList)

            Dim xrow1 As GridViewRow = CType(ddl1.NamingContainer, GridViewRow)
            Dim lblid1 As DropDownList = CType(xrow1.FindControl("ddlFreqMTDGV"), DropDownList)
            'Dim lblid2 As TextBox = CType(xrow1.FindControl("txtMonthNoGV"), TextBox)
            'Dim lblid3 As TextBox = CType(xrow1.FindControl("txtDayNoGV"), TextBox)
            'Dim lblid4 As TextBox = CType(xrow1.FindControl("txtWeekNoGV"), TextBox)
            Dim lblid5 As DropDownList = CType(xrow1.FindControl("ddlWeekDOWGV"), DropDownList)

            Dim rowindex1 As Integer
            rowindex1 = xrow1.RowIndex

            'Dim TextBoxFreqMTD As DropDownList = CType(grvFreqDetails.Rows(rowindex1).Cells(1).FindControl("ddlFreqMTDGV"), DropDownList)
            Dim TextBoxMonthNo As TextBox = CType(grvFreqDetails.Rows(rowindex1).Cells(2).FindControl("txtMonthNoGV"), TextBox)
            Dim TextBoxDayNo As TextBox = CType(grvFreqDetails.Rows(rowindex1).Cells(3).FindControl("txtDayNoGV"), TextBox)
            Dim TextBoxWeekNo As TextBox = CType(grvFreqDetails.Rows(rowindex1).Cells(4).FindControl("txtWeekNoGV"), TextBox)
            Dim TextBoxWeekDOW As DropDownList = CType(grvFreqDetails.Rows(rowindex1).Cells(5).FindControl("ddlWeekDOWGV"), DropDownList)
            'Dim TextBoxLocation As DropDownList = CType(grvFreqDetails.Rows(rowindex1).Cells(6).FindControl("ddlLocationGV"), DropDownList)
            'Dim TextBoxBranchID As DropDownList = CType(grvFreqDetails.Rows(rowindex1).Cells(7).FindControl("ddlBranchIDGV"), DropDownList)
            'Dim TextBoxContractNo As TextBox = CType(grvFreqDetails.Rows(rowindex1).Cells(8).FindControl("txtContractNoGVF"), TextBox)
            'Dim TextBoxSourceSQLID As TextBox = CType(grvFreqDetails.Rows(rowindex1).Cells(9).FindControl("txtSourceSQLIDGVF"), TextBox)
            'Dim TextBoxRcno As TextBox = CType(grvFreqDetails.Rows(rowindex1).Cells(10).FindControl("txtRcnoGVF"), TextBox)

            'lblid2.Text = ""
            'lblid3.Text = ""
            'lblid4.Text = ""
            'lblid5.SelectedIndex = 0

            If lblid1.Text = "DOW" Then
                If lblid5.SelectedIndex = "0" Then
                    lblid5.Text = CDate(txtServStart.Text).DayOfWeek.ToString()
                    MsgBox("Invalid DOW for the Frequency.. ")
                End If


            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Protected Sub txtWeekNoGV_TextChanged(sender As Object, e As EventArgs)
        Dim txt1 As TextBox = DirectCast(sender, TextBox)

        Dim xrow1 As GridViewRow = CType(txt1.NamingContainer, GridViewRow)
        Dim lblid1 As TextBox = CType(xrow1.FindControl("txtWeekNoGV"), TextBox)

        If Val(lblid1.Text) <= 0 Then
            MessageBox.Message.Alert(Page, "Week cannot be less than 0", "str")
            lblid1.Text = "1"

        End If


        If txtFrequency.Text = "BI-MONTHLY" Or txtFrequency.Text = "QUARTERLY" Or txtFrequency.Text = "HALF-ANNUALLY" Or txtFrequency.Text = "ANNUALLY" Or txtFrequency.Text = "BI-ANNUALLY" Then
            Dim maxWeekNo As String = (txtDOWByWhichWeek.Text)
            maxWeekNo = maxWeekNo.Substring(0, maxWeekNo.IndexOf(","))

            If Val(lblid1.Text) > Val(maxWeekNo) Then
                MsgBox("Week No cannot be more than " & Val(maxWeekNo))
                lblid1.Text = pWeekNumber(txtServStart.Text)
                Exit Sub
            End If
        End If


        If txtFrequency.Text = "BI-WEEKLY" Or txtFrequency.Text = "TRI-WEEKLY" Or txtFrequency.Text = "MONTHLY" Or txtFrequency.Text = "FORTNIGHTLY" Or txtFrequency.Text = "THRICE-MONTHLY" Or txtFrequency.Text = "TWICE-MONTHLY" Or txtFrequency.Text = "FOUR-TIMES-MONTHLY" Then
            Dim maxWeekNo As Integer = Val(txtDOWByWhichWeek.Text)
            If Val(lblid1.Text) > maxWeekNo Then
                If txtFrequency.Text.ToUpper = "MONTHLY" Then
                    If Val(lblid1.Text) > 5 Then
                        MsgBox("Week No cannot be more than 5")
                        lblid1.Text = pWeekNumber(txtServStart.Text)
                        Exit Sub
                    End If
                Else
                    MsgBox("Week No can not be bigger than " & maxWeekNo)
                    lblid1.Text = pWeekNumber(txtServStart.Text)
                    Exit Sub
                End If

            End If
        End If
    End Sub

    Protected Sub txtMonthNoGV_TextChanged(sender As Object, e As EventArgs)
        Dim txt1 As TextBox = DirectCast(sender, TextBox)

        Dim xrow1 As GridViewRow = CType(txt1.NamingContainer, GridViewRow)
        Dim lblid1 As TextBox = CType(xrow1.FindControl("txtMonthNoGV"), TextBox)


        If Val(lblid1.Text) <= 0 Then
            MessageBox.Message.Alert(Page, "Month cannot be less than 0", "str")
            lblid1.Text = "1"

        End If

        Dim maxMonthNo As Integer = Val(txtMonthByWhichMonth.Text)
        If Val(lblid1.Text) > maxMonthNo Then
            MsgBox("Month No cannot be more than " & maxMonthNo)
            lblid1.Text = "1"
            Exit Sub
        End If


    End Sub

    Protected Sub txtDayNoGV_TextChanged(sender As Object, e As EventArgs)
        Dim txt1 As TextBox = DirectCast(sender, TextBox)

        Dim xrow1 As GridViewRow = CType(txt1.NamingContainer, GridViewRow)
        Dim lblid1 As TextBox = CType(xrow1.FindControl("txtDayNoGV"), TextBox)

        If Val(lblid1.Text) <= 0 Or Val(lblid1.Text) > 31 Then
            MessageBox.Message.Alert(Page, "Day cannot be less than 0 and more than 31", "str")
            lblid1.Text = Day(CDate(Me.txtServStart.Text))

        End If
    End Sub



    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If txtStatus.Text = "P" Then
                Dim message1 As String = "alert('Contract has already been POSTED.. Cannot be DELETED!!!')"
                ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message1, True)

                Exit Sub
            End If

            Dim confirmValue As String
            confirmValue = ""

            confirmValue = Request.Form("confirm_value")
            If Right(confirmValue, 3) = "Yes" Then
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command2 As MySqlCommand = New MySqlCommand
                command2.CommandType = CommandType.Text

                Dim qry2 As String = "DELETE from tblContractDet where Rcno= @Rcno "

                command2.CommandText = qry2
                command2.Parameters.Clear()

                command2.Parameters.AddWithValue("@Rcno", txtRcno.Text)
                command2.Connection = conn
                command2.ExecuteNonQuery()



                Dim command3 As MySqlCommand = New MySqlCommand
                command3.CommandType = CommandType.Text

                Dim qry3 As String = "DELETE from tblContractServingTarget where SourceSQLId= @OrigCode "

                command3.CommandText = qry3
                command3.Parameters.Clear()

                command3.Parameters.AddWithValue("@OrigCode", txtOrigCode.Text)
                command3.Connection = conn
                command3.ExecuteNonQuery()


                Dim command4 As MySqlCommand = New MySqlCommand
                command4.CommandType = CommandType.Text

                Dim qry4 As String = "DELETE from tblServiceContractFrequency where SourceSQLId= @OrigCode "

                command4.CommandText = qry4
                command4.Parameters.Clear()

                command4.Parameters.AddWithValue("@OrigCode", txtOrigCode.Text)
                command4.Connection = conn
                command4.ExecuteNonQuery()

                conn.Close()

                UpdateContractHeader()

                Dim message As String = "alert('Contract Detail is deleted Successfully!!!')"
                ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)

                btnADD_Click(sender, e)
                GridView1.DataBind()
            End If
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            Dim message As String = "alert('" + exstr + "!!!')"
            ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)


        End Try
    End Sub



    Protected Sub btnQuit_Click(sender As Object, e As EventArgs) Handles btnQuit.Click
        Response.Redirect("Home.aspx")
    End Sub



    Protected Sub txtValue_TextChanged(sender As Object, e As EventArgs) Handles txtValue.TextChanged

    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        'Session("contractno") = txtContractNo.Text
        Session("contractdetailfrom") = "contract"
        Response.Redirect("Contract.aspx")
        'Response.Redirect(prevPage)
    End Sub


    Protected Sub txtRecordAdded_TextChanged(sender As Object, e As EventArgs) Handles txtRecordAdded.TextChanged

    End Sub

    Protected Sub txtRecordDeleted_TextChanged(sender As Object, e As EventArgs) Handles txtRecordDeleted.TextChanged

    End Sub

    Protected Sub txtBillingAmount_TextChanged(sender As Object, e As EventArgs) Handles txtBillingAmount.TextChanged

    End Sub
End Class




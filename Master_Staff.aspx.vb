

Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.IO
Imports System.Drawing


Partial Class Master_Staff
    Inherits System.Web.UI.Page

    Public rcno As String
    Public Leavercno As String
    Public Sub MakeMeNull()
        lblMessage.Text = ""
        lblAlert.Text = ""

        txtMode.Text = ""
        txtStaffID.Text = ""
        txtNRIC.Text = ""
        txtDept.Text = ""
        ddlSalute.ClearSelection()
        txtName.Text = ""
        txtInterfaceLang.Text = ""
        txtPrefName.Text = ""
        Image2.ImageUrl = ""
        txtDateJoined.Text = ""
        txtDateLeft.Text = ""
        txtProfession.Text = ""
        txtAppt.Text = ""
        txtEmail.Text = ""
        txtTel.Text = ""
        txtMobile.Text = ""
        ddlMaritalStatus.SelectedIndex = 0
        ddlStatus.SelectedIndex = 0
        txtComments.Text = ""
        txtDOB.Text = ""
        txtNationality.Text = ""
        txtCitizenship.Text = ""
        ddlSystemUser.ClearSelection()
        txtLocationId.SelectedIndex = 0
        txtLocationName.Text = ""
        txtWHBranch.Text = ""
        txtDeptSubLdgr.Text = ""
        txtStaffGroup.Text = ""
        ddlType.ClearSelection()
        txtPassType.Text = ""
        txtPayrollID.Text = ""
        txtPassportNo.Text = ""
        txtPassportExpiry.Text = ""
        txtWPEPNo.Text = ""
        txtWPEPexpiry.Text = ""

        txtResBlock.Text = ""
        txtResNo.Text = ""
        txtResFloor.Text = ""
        txtResUnit.Text = ""
        txtResAddr.Text = ""
        txtResBuilding.Text = ""
        txtResStreet.Text = ""
        ddlResCity.ClearSelection()
        ddlResState.ClearSelection()
        ddlResCountry.ClearSelection()
        txtResPostal.Text = ""
        txtResTel.Text = ""
        txtResMobile.Text = ""
        txtResEmail.Text = ""

        txtGroupAuthority.Text = "GUEST"
        txtWebLoginID.Text = ""
        txtWebLoginPassword.Text = ""
        chkSystemUser.Checked = False
        txtRcno.Text = ""
        'txtCreatedBy.Text = ""
        txtCreatedOn.Text = ""
        ' txtLastModifiedBy.Text = ""
        txtLastModifiedOn.Text = ""

        txtMobileLoginID.Text = ""
        txtMobileLoginPassword.Text = ""

        txtWebLoginID1.Text = ""
        txtWebLoginPassword1.Text = ""
        txtMobileLoginID1.Text = ""
        txtMobileLoginPassword1.Text = ""

        chkManualTimeIn.Checked = False
        chkManualTimeOut.Checked = False

        lblStaffID1.Text = ""
        lblStaffName1.Text = ""
        lblStaffID.Text = ""
        lblStaffName.Text = ""

        lblStaffIDStatus.Text = ""
        lblOldStatus.Text = ""

        ddlNewStatus.SelectedIndex = 0
        ddlRoles.SelectedIndex = 0
        GridView2.DataBind()

    End Sub

    Private Sub EnableLeaveControls()
        btnLeaveSave.Enabled = True
        btnLeaveSave.ForeColor = System.Drawing.Color.Black
        btnSvcCancel.Enabled = True
        btnSvcCancel.ForeColor = System.Drawing.Color.Black

        btnSvcAdd.Enabled = False
        btnSvcAdd.ForeColor = System.Drawing.Color.Gray

        btnSvcEdit.Enabled = False
        btnSvcEdit.ForeColor = System.Drawing.Color.Gray

        btnSvcDelete.Enabled = False
        btnSvcDelete.ForeColor = System.Drawing.Color.Gray

        txtApplicationDate.Enabled = True
        txtDateFrom.Enabled = True
        txtDateTo.Enabled = True
        ddlLeaveType.Enabled = True
        txtTotalDays.Enabled = True
    End Sub

    Private Sub DisableLeaveControls()



        btnLeaveSave.Enabled = False
        btnLeaveSave.ForeColor = System.Drawing.Color.Gray
        btnSvcCancel.Enabled = False
        btnSvcCancel.ForeColor = System.Drawing.Color.Gray

        btnSvcAdd.Enabled = True
        btnSvcAdd.ForeColor = System.Drawing.Color.Black
        btnSvcEdit.Enabled = False
        btnSvcEdit.ForeColor = System.Drawing.Color.Gray
        btnSvcDelete.Enabled = False
        btnSvcDelete.ForeColor = System.Drawing.Color.Gray

        txtApplicationDate.Enabled = False
        txtDateFrom.Enabled = False
        txtDateTo.Enabled = False
        ddlLeaveType.Enabled = False
        txtTotalDays.Enabled = False
    End Sub

    Private Sub MakeLeaveNull()
        txtApplicationDate.Text = ""
        txtDateFrom.Text = ""
        txtDateTo.Text = ""
        txtLeaveDescription.Text = ""
        ddlLeaveType.SelectedIndex = 0
        txtTotalDays.Text = 0
    End Sub

    Private Sub EnableControls()
        btnSave.Enabled = False
        btnSave.ForeColor = System.Drawing.Color.Gray
        btnCancel.Enabled = False
        btnCancel.ForeColor = System.Drawing.Color.Gray

        btnADD.Enabled = True
        btnADD.ForeColor = System.Drawing.Color.Black

        btnDelete.Enabled = False
        btnDelete.ForeColor = System.Drawing.Color.Gray

        btnEdit.Enabled = False
        btnEdit.ForeColor = System.Drawing.Color.Gray

        btnQuit.Enabled = True
        btnQuit.ForeColor = System.Drawing.Color.Black
        btnPrint.Enabled = True
        btnPrint.ForeColor = System.Drawing.Color.Black

        btnPrintLeave.Enabled = False
        btnPrintLeave.ForeColor = System.Drawing.Color.Gray

        btnStatus.Enabled = False
        btnStatus.ForeColor = System.Drawing.Color.Gray

        txtStaffID.Enabled = False
        txtNRIC.Enabled = False
        txtDept.Enabled = False
        ddlSalute.Enabled = False
        txtName.Enabled = False
        txtInterfaceLang.Enabled = False
        txtPrefName.Enabled = False

        FileUpload1.Enabled = False

        txtDateJoined.Enabled = False
        txtDateLeft.Enabled = False
        txtProfession.Enabled = False
        txtAppt.Enabled = False
        txtEmail.Enabled = False
        txtTel.Enabled = False
        txtMobile.Enabled = False
        ddlMaritalStatus.Enabled = False
        ddlStatus.Enabled = False
        txtComments.Enabled = False
        txtDOB.Enabled = False
        txtNationality.Enabled = False
        txtCitizenship.Enabled = False
        ddlSystemUser.Enabled = False
        txtLocationID.Enabled = False
        txtLocationName.Enabled = False
        txtWHBranch.Enabled = False
        txtDeptSubLdgr.Enabled = False
        txtStaffGroup.Enabled = False
        ddlType.Enabled = False
        txtPassType.Enabled = False
        txtPayrollID.Enabled = False
        txtPassportNo.Enabled = False
        txtPassportExpiry.Enabled = False
        txtWPEPNo.Enabled = False
        txtWPEPexpiry.Enabled = False

        txtResBlock.Enabled = False
        txtResNo.Enabled = False
        txtResFloor.Enabled = False
        txtResUnit.Enabled = False
        txtResAddr.Enabled = False
        txtResBuilding.Enabled = False
        txtResStreet.Enabled = False
        ddlResCity.Enabled = False
        ddlResState.Enabled = False
        ddlResCountry.Enabled = False
        txtResPostal.Enabled = False
        txtResTel.Enabled = False
        txtResMobile.Enabled = False
        txtResEmail.Enabled = False
        ddlRoles.Enabled = False
        txtDateLeftStatus.Enabled = False
        GridView1.Enabled = True
        AccessControl()

    End Sub

    Private Sub DisableControls()
        btnSave.Enabled = True
        btnSave.ForeColor = System.Drawing.Color.Black
        btnCancel.Enabled = True
        btnCancel.ForeColor = System.Drawing.Color.Black
        btnADD.Enabled = False
        btnADD.ForeColor = System.Drawing.Color.Gray

        btnEdit.Enabled = False
        btnEdit.ForeColor = System.Drawing.Color.Gray

        btnDelete.Enabled = False
        btnDelete.ForeColor = System.Drawing.Color.Gray

        btnQuit.Enabled = False
        btnQuit.ForeColor = System.Drawing.Color.Gray



        btnPrint.Enabled = False
        btnPrint.ForeColor = System.Drawing.Color.Gray

        btnPrintLeave.Enabled = False
        btnPrintLeave.ForeColor = System.Drawing.Color.Gray

        btnFilter.Enabled = False
        btnFilter.ForeColor = System.Drawing.Color.Gray

        btnStatus.Enabled = False
        btnStatus.ForeColor = System.Drawing.Color.Gray

        FileUpload1.Enabled = True

        txtStaffID.Enabled = True
        txtNRIC.Enabled = True
        txtDept.Enabled = True
        ddlSalute.Enabled = True
        txtName.Enabled = True
        txtInterfaceLang.Enabled = True
        txtPrefName.Enabled = True
        txtDateJoined.Enabled = True
        txtDateLeft.Enabled = True
        txtProfession.Enabled = True
        txtAppt.Enabled = True
        txtEmail.Enabled = True
        txtTel.Enabled = True
        txtMobile.Enabled = True
        ddlMaritalStatus.Enabled = True
        If txtMode.Text = "New" Then
            ddlStatus.Enabled = False
        Else
            ddlStatus.Enabled = False
        End If

        txtComments.Enabled = True
        txtDOB.Enabled = True
        txtNationality.Enabled = True
        txtCitizenship.Enabled = True
        ddlSystemUser.Enabled = True
        txtLocationID.Enabled = True
        txtLocationName.Enabled = True
        txtWHBranch.Enabled = True
        txtDeptSubLdgr.Enabled = True
        txtStaffGroup.Enabled = True
        ddlType.Enabled = True
        txtPassType.Enabled = True
        txtPayrollID.Enabled = True
        txtPassportNo.Enabled = True
        txtPassportExpiry.Enabled = True
        txtWPEPNo.Enabled = True
        txtWPEPexpiry.Enabled = True

        txtResBlock.Enabled = True
        txtResNo.Enabled = True
        txtResFloor.Enabled = True
        txtResUnit.Enabled = True
        txtResAddr.Enabled = True
        txtResBuilding.Enabled = True
        txtResStreet.Enabled = True
        ddlResCity.Enabled = True
        ddlResState.Enabled = True
        ddlResCountry.Enabled = True
        txtResPostal.Enabled = True
        txtResTel.Enabled = True
        txtResMobile.Enabled = True
        txtResEmail.Enabled = True

        ddlRoles.Enabled = True

        'AccessControl()
    End Sub

    Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        GridView1.PageIndex = e.NewPageIndex
        SqlDataSource1.SelectCommand = txt.Text
        SqlDataSource1.DataBind()
        GridView1.DataBind()
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        If txtMode.Text = "Edit" Then
            lblAlert.Text = "CANNOT SELECT RECORD IN EDIT MODE. SAVE OR CANCEL TO PROCEED"
            Return
        End If
        'EnableControls()
        MakeMeNull()
        Dim editindex As Integer = GridView1.SelectedIndex
        rcno = DirectCast(GridView1.Rows(editindex).FindControl("Label1"), Label).Text
        txtRcno.Text = rcno.ToString()

        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()
        Dim command1 As MySqlCommand = New MySqlCommand

        command1.CommandType = CommandType.Text

        command1.CommandText = "SELECT * FROM tblstaff where rcno=" & Convert.ToInt32(txtRcno.Text)
        command1.Connection = conn

        Dim dr As MySqlDataReader = command1.ExecuteReader()

        ' dr.Read()
        Dim dt As New DataTable
        dt.Load(dr)

        '  Dim imgg As Byte() = DirectCast(dr("Photo"), Byte())

        'If IsNothing(imgg) Then
        '    Image2.ImageUrl = ""
        'Else
        '    Image2.ImageUrl = Convert.ToString("data:image/jpg;base64,") + Convert.ToBase64String(imgg)
        '    Dim Buffer As Byte() = DirectCast(dr("Photo"), Byte())
        '    Dim stream1 As New System.IO.MemoryStream(Buffer, True)
        '    stream1.Write(Buffer, 0, Buffer.Length)
        '    Dim m_bitmap As System.Drawing.Bitmap = DirectCast(System.Drawing.Bitmap.FromStream(stream1, True), System.Drawing.Bitmap)
        '    Response.ContentType = "Image/jpeg"
        '    m_bitmap.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg)
        'End If


        If dt.Rows.Count > 0 Then
            txtStaffID.Text = dt.Rows(0)("StaffId").ToString
            txtNRIC.Text = dt.Rows(0)("Nric").ToString
            txtDept.Text = dt.Rows(0)("Department").ToString
            If dt.Rows(0)("Salute").ToString <> "" Then
                ddlSalute.Text = dt.Rows(0)("Salute").ToString

            End If
            txtName.Text = dt.Rows(0)("Name").ToString
            txtInterfaceLang.Text = dt.Rows(0)("InterfaceLanguage").ToString
            txtPrefName.Text = dt.Rows(0)("PreferredName").ToString
            If dt.Rows(0)("DateJoin").ToString = DBNull.Value.ToString Then
            Else
                txtDateJoined.Text = Convert.ToDateTime(dt.Rows(0)("DateJoin")).ToString("dd/MM/yyyy")
            End If
            If dt.Rows(0)("DateLeft").ToString = DBNull.Value.ToString Then
            Else
                txtDateLeft.Text = Convert.ToDateTime(dt.Rows(0)("DateLeft")).ToString("dd/MM/yyyy")
            End If

            txtDateLeftStatus.Text = txtDateLeft.Text

            txtProfession.Text = dt.Rows(0)("Profession").ToString
            txtAppt.Text = dt.Rows(0)("Appointment").ToString
            txtEmail.Text = dt.Rows(0)("EmailPerson").ToString
            txtTel.Text = dt.Rows(0)("TelHome").ToString
            txtMobile.Text = dt.Rows(0)("TelMobile").ToString

            'If String.IsNullOrEmpty(dt.Rows(0)("MartialStatus").ToString) = True Then
            '    ddlMaritalStatus.SelectedIndex = 0
            'ElseIf dt.Rows(0)("MartialStatus").ToString <> "MARRIED" Then
            'Else
            '    ddlMaritalStatus.Text = dt.Rows(0)("MartialStatus").ToString
            'End If


            If dt.Rows(0)("MartialStatus").ToString = "MARRIED" Or dt.Rows(0)("MartialStatus").ToString = "DIVORCED" Or dt.Rows(0)("MartialStatus").ToString = "SINGLE" Or dt.Rows(0)("MartialStatus").ToString = "SEPARATED" Then
                ddlMaritalStatus.SelectedIndex = 0
            Else
                ddlMaritalStatus.SelectedIndex = 0
            End If

            If String.IsNullOrEmpty(dt.Rows(0)("Status").ToString) = True Then
                ddlStatus.SelectedIndex = 0
            Else
                ddlStatus.Text = dt.Rows(0)("Status").ToString
            End If

            txtComments.Text = dt.Rows(0)("Comments").ToString
            If dt.Rows(0)("DateOfBirth").ToString = DBNull.Value.ToString Then
            Else
                txtDOB.Text = Convert.ToDateTime(dt.Rows(0)("DateOfBirth")).ToString("dd/MM/yyyy")
            End If
            txtNationality.Text = dt.Rows(0)("Nationality").ToString
            txtCitizenship.Text = dt.Rows(0)("Citizenship").ToString
            ddlSystemUser.Text = dt.Rows(0)("SystemUser").ToString

            If String.IsNullOrEmpty(dt.Rows(0)("LocationID").ToString) = True Then
                txtLocationId.SelectedIndex = 0
            Else
                txtLocationId.Text = dt.Rows(0)("LocationID").ToString
            End If

            'txtLocationID.Text = dt.Rows(0)("LocationID").ToString
            txtLocationName.Text = dt.Rows(0)("Location").ToString
            txtWHBranch.Text = dt.Rows(0)("WHLocation").ToString
            txtDeptSubLdgr.Text = dt.Rows(0)("DeptSubLedger").ToString
            txtStaffGroup.Text = dt.Rows(0)("EmpGroup").ToString
            If dt.Rows(0)("Type").ToString <> "" Then
                ddlType.Text = dt.Rows(0)("Type").ToString
            End If
            txtPassType.Text = dt.Rows(0)("PassType").ToString
            txtPayrollID.Text = dt.Rows(0)("PayrollID").ToString
            txtPassportNo.Text = dt.Rows(0)("PPNo").ToString
            If dt.Rows(0)("Passport_Expiry").ToString = DBNull.Value.ToString Then
            Else
                txtPassportExpiry.Text = Convert.ToDateTime(dt.Rows(0)("Passport_Expiry")).ToString("dd/MM/yyyy")
            End If
            txtWPEPNo.Text = dt.Rows(0)("WP_EP_NO").ToString
            If dt.Rows(0)("WP_EP_Expiry").ToString = DBNull.Value.ToString Then
            Else
                txtWPEPexpiry.Text = Convert.ToDateTime(dt.Rows(0)("WP_EP_Expiry")).ToString("dd/MM/yyyy")
            End If

            txtResBlock.Text = dt.Rows(0)("HomeBlock").ToString
            txtResNo.Text = dt.Rows(0)("HomeNos").ToString
            txtResFloor.Text = dt.Rows(0)("HomeFloor").ToString
            txtResUnit.Text = dt.Rows(0)("HomeUnit").ToString
            txtResAddr.Text = dt.Rows(0)("HomeAddress1").ToString
            txtResBuilding.Text = dt.Rows(0)("HomeBuilding").ToString
            txtResStreet.Text = dt.Rows(0)("HomeStreet").ToString
            If dt.Rows(0)("HomeCity").ToString <> "" Then
                ddlResCity.Text = dt.Rows(0)("HomeCity").ToString
            End If
            If dt.Rows(0)("HomeState").ToString <> "" Then
                ddlResState.Text = dt.Rows(0)("HomeState").ToString
            End If
            If dt.Rows(0)("HomeCountry").ToString <> "" Then
                ddlResCountry.Text = dt.Rows(0)("HomeCountry").ToString
            End If

            If String.IsNullOrEmpty(dt.Rows(0)("Roles").ToString) = True Then
                ddlRoles.SelectedIndex = 0
            Else

                If ddlRoles.Items.FindByValue(dt.Rows(0)("Roles").ToString) Is Nothing Then
                    ddlRoles.SelectedIndex = 0
                Else
                    ddlRoles.Text = dt.Rows(0)("Roles").ToString.Trim()
                End If

            End If
            txtResPostal.Text = dt.Rows(0)("HomePostal").ToString
            txtResTel.Text = dt.Rows(0)("HomeTel").ToString
            txtResMobile.Text = dt.Rows(0)("HomeMobile").ToString
            txtResEmail.Text = dt.Rows(0)("HomeEmail").ToString

            If IsDBNull(dt.Rows(0)("Photo")) = False Then
                Dim bytes As Byte() = DirectCast(dt.Rows(0)("Photo"), Byte())
                Image2.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(bytes)
            End If

            If String.IsNullOrEmpty(dt.Rows(0)("SecGroupAuthority").ToString) = True Then
                txtGroupAuthority.SelectedIndex = 0
            Else
                txtGroupAuthority.Text = dt.Rows(0)("SecGroupAuthority").ToString
            End If

            txtWebLoginID.Text = dt.Rows(0)("SecWebLoginID").ToString
            txtWebLoginID1.Text = dt.Rows(0)("SecWebLoginID").ToString

            txtMobileLoginID.Text = dt.Rows(0)("SecMobileLoginID").ToString
            txtMobileLoginID1.Text = dt.Rows(0)("SecMobileLoginID").ToString

            chkManualTimeIn.Checked = dt.Rows(0)("MobileAppManualTimeIn").ToString
            chkManualTimeOut.Checked = dt.Rows(0)("MobileAppManualTimeOut").ToString

            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            'Password decryption
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            If IsDBNull(dt.Rows(0)("SecWebPassword")) = False Then
                Dim data As Byte() = Convert.FromBase64String(dt.Rows(0)("SecWebPassword").ToString)
                Dim decodedString As String = System.Text.Encoding.UTF8.GetString(data)

                txtWebLoginPassword.Text = decodedString
                txtWebLoginPassword1.Text = decodedString
            End If

            If IsDBNull(dt.Rows(0)("SecMobilePassword")) = False Then
                Dim data1 As Byte() = Convert.FromBase64String(dt.Rows(0)("SecMobilePassword").ToString)
                Dim decodedString1 As String = System.Text.Encoding.UTF8.GetString(data1)

                txtMobileLoginPassword.Text = decodedString1
                txtMobileLoginPassword1.Text = decodedString1
            End If

            If dt.Rows(0)("SystemUser").ToString = "Y" Then
                chkSystemUser.Checked = True
            Else
                chkSystemUser.Checked = False
            End If


            lblStaffID.Text = txtStaffID.Text
            lblStaffName.Text = txtName.Text
            lblStaffID1.Text = txtStaffID.Text
            lblStaffName1.Text = txtName.Text
            txtWebLoginID.Text = txtStaffID.Text
            txtMobileLoginID.Text = txtStaffID.Text

        End If
        conn.Close()

        txtMode.Text = "View"

        'Catch ex As Exception
        '    MessageBox.Message.Alert(Page, "Error!!! " + ex.Message.ToString, "str")
        'End Try
        'txtMode.Text = "Edit"
        ' DisableControls()
        btnEdit.Enabled = True
        btnEdit.ForeColor = System.Drawing.Color.Black
        btnDelete.Enabled = True
        btnDelete.ForeColor = System.Drawing.Color.Black
        btnQuit.Enabled = True
        btnQuit.ForeColor = System.Drawing.Color.Black
        btnPrintLeave.Enabled = True
        btnPrintLeave.ForeColor = System.Drawing.Color.Black
        btnStatus.Enabled = True
        btnStatus.ForeColor = System.Drawing.Color.Black

        If CheckIfExists() = True Then
            txtExists.Text = "True"
        Else
            txtExists.Text = "False"
        End If

        txtGroupAuthority.Enabled = False
        chkSystemUser.Enabled = False
        txtWebLoginID.Enabled = False
        txtWebLoginPassword.Enabled = False


        txtMobileLoginID.Enabled = False
        txtMobileLoginPassword.Enabled = False

        chkManualTimeIn.Enabled = False
        chkManualTimeOut.Enabled = False

        btnSaveUser.Enabled = False
        btnCancelUser.Enabled = False

        btnEditUser.Enabled = True

        'EnableControls()
        'If Session("SecGroupAuthority") <> "ADMINISTRATOR" Then
        AccessControl()
        'End If

        tb1.ActiveTabIndex = 0
    End Sub

    Protected Sub btnADD_Click(sender As Object, e As EventArgs) Handles btnADD.Click
        MakeMeNull()
        txtMode.Text = "New"
        DisableControls()
        lblAlert.Text = ""

        lblMessage.Text = "ACTION: ADD RECORD"
        txtStaffID.Focus()
        txtExists.Text = "False"
        tb1.ActiveTabIndex = 0
        GridView1.Enabled = False

        btnADD.Enabled = False
        btnADD.ForeColor = System.Drawing.Color.Gray

        txtSearchStaff.Enabled = False
        btnGo.Enabled = False

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

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        txtCreatedOn.Attributes.Add("readonly", "readonly")
        'txtTotalDays.Attributes.Add("readonly", "readonly")
        btnTop.Attributes.Add("onclick", "javascript:scroll(0,0);return false;")

        If Not IsPostBack Then
            MakeMeNull()
            EnableControls()
            'DisableControls()
            DisableLeaveControls()
            txtDDLText.Text = "-1"
            txt.Text = "SELECT * FROM tblstaff  order by name"
            Dim UserID As String = Convert.ToString(Session("UserID"))
            txtCreatedBy.Text = UserID
            txtLastModifiedBy.Text = UserID
            tb1.ActiveTabIndex = 0

            'txtWebLoginPassword.Attributes.["type"] = "password";
            txtWebLoginPassword.Attributes.Add("type", "password")
            txtMobileLoginPassword.Attributes.Add("type", "password")

            txtWebLoginID.Attributes.Add("readonly", "readonly")

            txtLocationName.Attributes.Add("readonly", "readonly")

            Dim query As String

            query = "Select GroupAccess from tblGroupAccess order by GroupAccess"
            PopulateDropDownList(query, "GroupAccess", "GroupAccess", txtGroupAuthority)


            query = "Select LocationID from tblgroupaccesslocation where GroupAccess = '" & Session("SecGroupAuthority") & "'"
            PopulateDropDownList(query, "LocationID", "LocationID", txtLocationId)




            ''''


            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            If conn.State = ConnectionState.Open Then
                conn.Close()
                conn.Dispose()
            End If
            conn.Open()
            Dim commandServiceRecordMasterSetup As MySqlCommand = New MySqlCommand
            commandServiceRecordMasterSetup.CommandType = CommandType.Text
            commandServiceRecordMasterSetup.CommandText = "SELECT ShowInvoiceOnScreenLoad, InvoiceRecordMaxRec,DisplayRecordsLocationWise, PostInvoice, InvoiceOnlyEditableByCreator, DefaultTaxCode FROM tblservicerecordmastersetup"
            commandServiceRecordMasterSetup.Connection = conn

            Dim drServiceRecordMasterSetup As MySqlDataReader = commandServiceRecordMasterSetup.ExecuteReader()
            Dim dtServiceRecordMasterSetup As New DataTable
            dtServiceRecordMasterSetup.Load(drServiceRecordMasterSetup)

            'txtLimit.Text = dtServiceRecordMasterSetup.Rows(0)("InvoiceRecordMaxRec")
            txtDisplayRecordsLocationwise.Text = dtServiceRecordMasterSetup.Rows(0)("DisplayRecordsLocationWise").ToString
            'txtPostUponSave.Text = dtServiceRecordMasterSetup.Rows(0)("PostInvoice").ToString
            'txtOnlyEditableByCreator.Text = dtServiceRecordMasterSetup.Rows(0)("InvoiceOnlyEditableByCreator").ToString
            'txtDefaultTaxCode.Text = dtServiceRecordMasterSetup.Rows(0)("DefaultTaxCode").ToString

            ''''''''''''''''''''''''''''''''''''''''''

            Dim sql As String
            sql = ""

            If txtDisplayRecordsLocationwise.Text = "Y" Then
                SqlDataSource1.SelectCommand = "SELECT * FROM tblstaff WHERE Status = 'O' and ((LocationID ='') or (Locationid is null) or locationid in (Select LocationID from tblgroupaccesslocation where GroupAccess ='" & Session("SecGroupAuthority") & "')) order by name"
            Else
                SqlDataSource1.SelectCommand = "SELECT * FROM tblstaff WHERE Status = 'O' order by name"
            End If

            SqlDataSource1.DataBind()
            GridView1.DataSourceID = "SqlDataSource1"
            GridView1.DataBind()

            conn.Close()
            commandServiceRecordMasterSetup.Dispose()
            drServiceRecordMasterSetup.Close()
            dtServiceRecordMasterSetup.Dispose()



            '''
            'If UserID = "admin" Or UserID = "ADMIN" Then
            '    txtWebLoginPassword.TextMode = TextBoxMode.SingleLine
            '    txtMobileLoginPassword.TextMode = TextBoxMode.SingleLine
            'Else
            '    txtWebLoginPassword.TextMode = TextBoxMode.Password
            '    txtMobileLoginPassword.TextMode = TextBoxMode.Password
            'End If
        Else
            'If Not String.IsNullOrEmpty(txtWebLoginPassword.Text.Trim()) Then
            '    txtWebLoginPassword.Attributes.Add("value", txtWebLoginPassword.Text)
            'End If

        End If
        CheckTab()

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
            command.CommandText = "SELECT x0304,  x0304Add, x0304Edit, x0304Delete, x0304ChSt, X0304GroupAuthority FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"

            command.Connection = conn

            Dim dr As MySqlDataReader = command.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)
            conn.Close()


            Dim command1 As MySqlCommand = New MySqlCommand
            conn.Open()
            command1.CommandType = CommandType.Text
            command1.CommandText = "SELECT x0304Print FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"
            command1.Connection = conn
            Dim dr1 As MySqlDataReader = command1.ExecuteReader()
            Dim dt1 As New DataTable
            dt1.Load(dr1)
            conn.Close()

            If (dt1.Rows.Count > 0) Then
                If Not IsDBNull(dt1.Rows(0)("x0304Print")) Then
                    If String.IsNullOrEmpty(dt1.Rows(0)("x0304Print")) = False Then
                        Me.btnPrint.Enabled = dt1.Rows(0)("x0304Print").ToString()
                    End If
                End If
            End If

            If dt.Rows.Count > 0 Then
                If Not IsDBNull(dt.Rows(0)("x0304")) Then
                    If String.IsNullOrEmpty(Convert.ToBoolean(dt.Rows(0)("x0304"))) = False Then
                        If Convert.ToBoolean(dt.Rows(0)("x0304")) = False Then
                            Response.Redirect("Home.aspx")
                        End If
                    End If
                End If

                If Not IsDBNull(dt.Rows(0)("x0304Add")) Then
                    If String.IsNullOrEmpty(dt.Rows(0)("x0304Add")) = False Then
                        Me.btnADD.Enabled = dt.Rows(0)("x0304Add").ToString()
                    End If
                End If

                If Not IsDBNull(dt.Rows(0)("x0304ChSt")) Then
                    If String.IsNullOrEmpty(dt.Rows(0)("x0304ChSt")) = False Then
                        Me.btnStatus.Enabled = dt.Rows(0)("x0304ChSt").ToString()
                    End If
                End If


                If Not IsDBNull(dt.Rows(0)("X0304GroupAuthority")) Then
                    If String.IsNullOrEmpty(dt.Rows(0)("X0304GroupAuthority")) = False Then
                        If dt.Rows(0)("X0304GroupAuthority").ToString = False Then
                            tb1.Tabs(1).Visible = False
                        End If

                    End If
                End If



                If txtMode.Text = "View" Then
                    If Not IsDBNull(dt.Rows(0)("x0304Edit")) Then
                        If String.IsNullOrEmpty(dt.Rows(0)("x0304Edit")) = False Then
                            Me.btnEdit.Enabled = dt.Rows(0)("x0304Edit").ToString()
                        End If
                    End If

                    If Not IsDBNull(dt.Rows(0)("x0304Delete")) Then
                        If String.IsNullOrEmpty(dt.Rows(0)("x0304Delete")) = False Then
                            Me.btnDelete.Enabled = dt.Rows(0)("x0304Delete").ToString()
                        End If
                    End If
                Else
                    Me.btnEdit.Enabled = False
                    Me.btnDelete.Enabled = False
                End If

                If btnADD.Enabled = True Then
                    btnADD.ForeColor = System.Drawing.Color.Black
                Else
                    btnADD.ForeColor = System.Drawing.Color.Gray
                End If


                If btnEdit.Enabled = True Then
                    btnEdit.ForeColor = System.Drawing.Color.Black
                Else
                    btnEdit.ForeColor = System.Drawing.Color.Gray
                End If

                If btnDelete.Enabled = True Then
                    btnDelete.ForeColor = System.Drawing.Color.Black
                Else
                    btnDelete.ForeColor = System.Drawing.Color.Gray
                End If


                If btnPrint.Enabled = True Then
                    btnPrint.ForeColor = System.Drawing.Color.Black
                Else
                    btnPrint.ForeColor = System.Drawing.Color.Gray
                End If


                If btnStatus.Enabled = True Then
                    btnStatus.ForeColor = System.Drawing.Color.Black
                Else
                    btnStatus.ForeColor = System.Drawing.Color.Gray
                End If

            End If


            conn.Dispose()
            command.Dispose()
            command1.Dispose()
            dt1.Dispose()
            dr.Close()
            dr1.Close()

            'End If

            '''''''''''''''''''Access Control 
        Catch ex As Exception
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
            lblAlert.Text = ex.Message
        End Try
    End Sub


    Private Function Validation() As Boolean
        Dim t As DateTime
        Dim d As Double
        Dim dt As Date
        If String.IsNullOrEmpty(txtDateJoined.Text) = False Then
            If Date.TryParseExact(txtDateJoined.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, dt) Then
                txtDateJoined.Text = dt.ToShortDateString

            Else
                ' MessageBox.Message.Alert(Page, "Date Joined is invalid", "str")
                lblAlert.Text = "DATE JOINED IS INVALID"
                Return False
                Exit Function
            End If
        End If
        If String.IsNullOrEmpty(txtDateLeft.Text) = False Then
            If Date.TryParseExact(txtDateLeft.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, dt) Then
                txtDateLeft.Text = dt.ToShortDateString

            Else
                ' MessageBox.Message.Alert(Page, "Date Left is invalid", "str")
                lblAlert.Text = "DATE LEFT IS INVALID"
                Return False
                Exit Function
            End If
        End If
        If String.IsNullOrEmpty(txtDOB.Text) = False Then
            If Date.TryParseExact(txtDOB.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, dt) Then
                txtDOB.Text = dt.ToShortDateString

            Else
                ' MessageBox.Message.Alert(Page, "DOB is invalid", "str")
                lblAlert.Text = "DOB IS INVALID"
                Return False
                Exit Function
            End If
        End If
        If String.IsNullOrEmpty(txtPassportExpiry.Text) = False Then
            If Date.TryParseExact(txtPassportExpiry.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, dt) Then
                txtPassportExpiry.Text = dt.ToShortDateString

            Else
                '  MessageBox.Message.Alert(Page, "Passport Expiry Date is invalid", "str")
                lblAlert.Text = "PASSPORT EXPIRY DATE IN INVALID"
                Return False
                Exit Function
            End If
        End If
        If String.IsNullOrEmpty(txtWPEPexpiry.Text) = False Then
            If Date.TryParseExact(txtWPEPexpiry.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, dt) Then
                txtWPEPexpiry.Text = dt.ToShortDateString

            Else
                '  MessageBox.Message.Alert(Page, "WP/EP Expiry Date is invalid", "str")
                lblAlert.Text = "WP/EP EXPIRY DATE IS INVALID"
                Return False
                Exit Function
            End If
        End If
        Return True
    End Function

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtStaffID.Text = "" Then
            '  MessageBox.Message.Alert(Page, "StaffId cannot be blank!!!", "str")
            lblAlert.Text = "STAFF ID CANNOT BE BLANK"
            txtStaffID.Focus()
            Return
        End If
        If txtName.Text = "" Then
            '  MessageBox.Message.Alert(Page, "Name cannot be blank!!!", "str")
            lblAlert.Text = "NAME CANNOT BE BLANK"
            txtName.Focus()
            Return
        End If

        'If String.IsNullOrEmpty(txtLocationName.Text.Trim) = True Then
        '    lblAlert.Text = "LOCATION CANNOT BE BLANK"
        '    txtName.Focus()
        '    Return
        'End If


        If Validation() = False Then
            Return
        End If

        If ddlSystemUser.Text = "Y" Then
            If String.IsNullOrEmpty(txtEmail.Text.Trim) = True Then
                lblAlert.Text = "EMAIL CANNOT BE BLANK FOR SYSTEM USER"
                txtEmail.Focus()
                Return
            End If
        End If

        If txtMode.Text = "New" Then
            '  Try
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.CommandText = "SELECT * FROM tblstaff where staffid=@staffid"
            command1.Parameters.AddWithValue("@staffid", txtStaffID.Text)
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then

                '   MessageBox.Message.Alert(Page, "Record already exists!!!", "str")
                lblAlert.Text = "RECORD ALREADY EXISTS"
                txtStaffID.Focus()
                Exit Sub
            Else

                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                Dim qry As String = "INSERT INTO tblstaff(StaffId,Salute,Name,Nric,CountryOfBirth,DateOfBirth,Citizenship,Race,Sex,MartialStatus,AddBlock,AddNos,AddFloor,AddUnit,AddBuilding,AddStreet,AddPostal,AddCity,AddState,AddCountry,TelHome,TelMobile,TelPager,Location,Department,Profession,Appointment,DirectLine,Extension, "
                qry = qry + "EmailPerson,EmailCompany,DateJoin,DateLeft,Comments,SecGroupAuthority,SecLevel,SecLoginId,SecPassword,SecExpiryDate,Address1,SecChangePassword,SecDisablePassword,Payroll_Coy,EmpGroup,Trade,WP_EP_NO,FIN_NO,DateArrival,PP_Expiry,WP_EP_Expiry,MonthLevy,AgentCode,PPNo, "
                qry = qry + "PPType,PPLocation,Nationality,SystemUser,HLEVEL,HDEPT,WP_EP_ApplyDt,PassPort_Expiry,Sec_Bond_No,Sec_Bond_Expiry,Soc_Cert_No,Soc_Cert_Expiry,InterfaceLanguage,Language1,Language2,Language3,Language4,Language5,Language6,WebPassword,CostCenter,SalaryType,BasicPay,Share, "
                qry = qry + "DaysPerWeek,SDLyn,PayBasis,ComputeMethod,PayMethod,BankCode,BranchCode,AccountNo,CPFgroup,CPFsub,FWLcode,CPFno,FundCode,FundByEmployer,CompanyBank,CompanyCPF,CreatedOn,DateConfirm,DailyLevy,Type,HoursPerDay,OTyn,DailyBasic,HourlyBasic,OT1_5,OT2,WorkTimeGRP, "
                qry = qry + "TimeCardNo,PayrollID,PassType,ALEntitlement,TimeAllowanceYN,TimeAllowanceStart,TimeAllowanceEnd,TimeCardReport,HomeBlock,HomeNos,HomeFloor,HomeUnit,HomeBuilding,HomeStreet,HomePostal,HomeCity,HomeState,HomeCountry,HomeAddress1,HomeTel,HomeMobile,HomePager, "
                qry = qry + "HomeEmail,Status,Meal,Transport,Shift,RoomNo,ShiftCode,LocationID,DeptSubLedger,IR8SIndicator,POSBranch,JobType_Auth_Users,WHLocation,LastModifiedOn,LastModifiedBy,ZNSecPassword,PayCompute,EffectPeriod,CPFYN,WorkEnqScreen,StaffPic,CreatedBy,SecMobileLoginID, "
                qry = qry + "SecMobilePassword,SecTabletMobileNo,SecWebLoginID,SecWebPassword,ProjectCode,HRSecurityLevel,SecGoogleEmail,SecGooglePassword,SecGoogleTaskEvent,SecGoogleJobReqDate,SecGoogleServDate,SlsmanYN,WebDisableYN,OTPYN,WebID,EmployerTaxShare,EmployeeTaxShare,Passcode, "
                qry = qry + "LeaveGroup,AnnualLeaveIncrement,MaxBringForward,MaxLeaveEntitlement,EvenDistribution,CustomizedDistribution,Month1,Month2,Month3,Month4,Month5,Month6,Month7,Month8,Month9,Month10,Month11,Month12,SubCompanyNo,SourceCompany,Photo,VerifyBySMS,Singature, "
                qry = qry + "CalendarColor,PreferredName,SalesGroup,CompanyGroup,Roles,SecGroupAuthorityTablet,SecLoginComments,WebUploadDate,PRIssueDate,UploadDate,IncentiveRate,RptDepartment)VALUES(@StaffId,@Salute,@Name,@Nric,@CountryOfBirth,@DateOfBirth,@Citizenship,@Race,@Sex, "
                qry = qry + "@MartialStatus,@AddBlock,@AddNos,@AddFloor,@AddUnit,@AddBuilding,@AddStreet,@AddPostal,@AddCity,@AddState,@AddCountry,@TelHome,@TelMobile,@TelPager,@Location,@Department,@Profession,@Appointment,@DirectLine,@Extension,@EmailPerson,@EmailCompany, "
                qry = qry + "@DateJoin,@DateLeft,@Comments,@SecGroupAuthority,@SecLevel,@SecLoginId,@SecPassword,@SecExpiryDate,@Address1,@SecChangePassword,@SecDisablePassword,@Payroll_Coy,@EmpGroup,@Trade,@WP_EP_NO,@FIN_NO,@DateArrival,@PP_Expiry,@WP_EP_Expiry,@MonthLevy, "
                qry = qry + "@AgentCode,@PPNo,@PPType,@PPLocation,@Nationality,@SystemUser,@HLEVEL,@HDEPT,@WP_EP_ApplyDt,@PassPort_Expiry,@Sec_Bond_No,@Sec_Bond_Expiry,@Soc_Cert_No,@Soc_Cert_Expiry,@InterfaceLanguage,@Language1,@Language2,@Language3,@Language4,@Language5, "
                qry = qry + "@Language6,@WebPassword,@CostCenter,@SalaryType,@BasicPay,@Share,@DaysPerWeek,@SDLyn,@PayBasis,@ComputeMethod,@PayMethod,@BankCode,@BranchCode,@AccountNo,@CPFgroup,@CPFsub,@FWLcode,@CPFno,@FundCode,@FundByEmployer,@CompanyBank, "
                qry = qry + "@CompanyCPF,@CreatedOn,@DateConfirm,@DailyLevy,@Type,@HoursPerDay,@OTyn,@DailyBasic,@HourlyBasic,@OT1_5,@OT2,@WorkTimeGRP,@TimeCardNo,@PayrollID,@PassType,@ALEntitlement,@TimeAllowanceYN,@TimeAllowanceStart,@TimeAllowanceEnd,@TimeCardReport, "
                qry = qry + "@HomeBlock,@HomeNos,@HomeFloor,@HomeUnit,@HomeBuilding,@HomeStreet,@HomePostal,@HomeCity,@HomeState,@HomeCountry,@HomeAddress1,@HomeTel,@HomeMobile,@HomePager,@HomeEmail,@Status,@Meal,@Transport,@Shift,@RoomNo,@ShiftCode,@LocationID, "
                qry = qry + "@DeptSubLedger,@IR8SIndicator,@POSBranch,@JobType_Auth_Users,@WHLocation,@LastModifiedOn,@LastModifiedBy,@ZNSecPassword,@PayCompute,@EffectPeriod,@CPFYN,@WorkEnqScreen,@StaffPic,@CreatedBy,@SecMobileLoginID,@SecMobilePassword,@SecTabletMobileNo,@SecWebLoginID,@SecWebPassword,@ProjectCode,@HRSecurityLevel,@SecGoogleEmail, "
                qry = qry + "@SecGooglePassword,@SecGoogleTaskEvent,@SecGoogleJobReqDate,@SecGoogleServDate,@SlsmanYN,@WebDisableYN,@OTPYN,@WebID,@EmployerTaxShare,@EmployeeTaxShare,@Passcode,@LeaveGroup,@AnnualLeaveIncrement,@MaxBringForward,@MaxLeaveEntitlement,@EvenDistribution,@CustomizedDistribution,@Month1,@Month2,@Month3,@Month4,@Month5,@Month6, "
                qry = qry + "@Month7,@Month8,@Month9,@Month10,@Month11,@Month12,@SubCompanyNo,@SourceCompany,@Photo,@VerifyBySMS,@Singature,@CalendarColor,@PreferredName,@SalesGroup,@CompanyGroup,@Roles,@SecGroupAuthorityTablet,@SecLoginComments,@WebUploadDate,@PRIssueDate,@UploadDate,@IncentiveRate,@RptDepartment);"

                command.CommandText = qry
                command.Parameters.Clear()

                If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                    command.Parameters.AddWithValue("@StaffId", txtStaffID.Text.ToUpper)
                    If ddlSalute.Text = txtDDLText.Text Then
                        command.Parameters.AddWithValue("@Salute", "")
                    Else
                        command.Parameters.AddWithValue("@Salute", ddlSalute.Text.ToUpper)
                    End If

                    command.Parameters.AddWithValue("@Name", txtName.Text.ToUpper)
                    command.Parameters.AddWithValue("@Nric", txtNRIC.Text.ToUpper)
                    command.Parameters.AddWithValue("@CountryOfBirth", "")
                    If String.IsNullOrEmpty(txtDOB.Text) Then
                        command.Parameters.AddWithValue("@DateOfBirth", DBNull.Value)
                    Else
                        command.Parameters.AddWithValue("@DateOfBirth", Convert.ToDateTime(txtDOB.Text).ToString("yyyy-MM-dd"))

                    End If
                    command.Parameters.AddWithValue("@Citizenship", txtCitizenship.Text.ToUpper)
                    command.Parameters.AddWithValue("@Race", "")
                    command.Parameters.AddWithValue("@Sex", "")

                    If ddlMaritalStatus.SelectedIndex = 0 Then
                        command.Parameters.AddWithValue("@MartialStatus", "")
                    Else
                        command.Parameters.AddWithValue("@MartialStatus", ddlMaritalStatus.Text.ToUpper)
                    End If

                    'command.Parameters.AddWithValue("@MartialStatus", ddlMaritalStatus.Text.ToUpper)
                    command.Parameters.AddWithValue("@AddBlock", "")
                    command.Parameters.AddWithValue("@AddNos", "")
                    command.Parameters.AddWithValue("@AddFloor", "")
                    command.Parameters.AddWithValue("@AddUnit", "")
                    command.Parameters.AddWithValue("@AddBuilding", "")
                    command.Parameters.AddWithValue("@AddStreet", "")
                    command.Parameters.AddWithValue("@AddPostal", "")
                    command.Parameters.AddWithValue("@AddCity", "")
                    command.Parameters.AddWithValue("@AddState", "")
                    command.Parameters.AddWithValue("@AddCountry", "")
                    command.Parameters.AddWithValue("@TelHome", txtTel.Text.ToUpper)
                    command.Parameters.AddWithValue("@TelMobile", txtMobile.Text.ToUpper)
                    command.Parameters.AddWithValue("@TelPager", "")
                    command.Parameters.AddWithValue("@Location", txtLocationName.Text.ToUpper)
                    command.Parameters.AddWithValue("@Department", txtDept.Text.ToUpper)
                    command.Parameters.AddWithValue("@Profession", txtProfession.Text.ToUpper)
                    command.Parameters.AddWithValue("@Appointment", txtAppt.Text.ToUpper)
                    command.Parameters.AddWithValue("@DirectLine", "")
                    command.Parameters.AddWithValue("@Extension", "")
                    command.Parameters.AddWithValue("@EmailPerson", txtEmail.Text.ToUpper)
                    command.Parameters.AddWithValue("@EmailCompany", "")
                    If String.IsNullOrEmpty(txtDateJoined.Text) Then
                        command.Parameters.AddWithValue("@DateJoin", DBNull.Value)
                    Else
                        command.Parameters.AddWithValue("@DateJoin", Convert.ToDateTime(txtDateJoined.Text).ToString("yyyy-MM-dd"))
                    End If
                    If String.IsNullOrEmpty(txtDateLeft.Text) Then
                        command.Parameters.AddWithValue("@DateLeft", DBNull.Value)
                    Else
                        command.Parameters.AddWithValue("@DateLeft", Convert.ToDateTime(txtDateLeft.Text).ToString("yyyy-MM-dd"))

                    End If
                    command.Parameters.AddWithValue("@Comments", txtComments.Text.ToUpper)
                    command.Parameters.AddWithValue("@SecGroupAuthority", "")
                    command.Parameters.AddWithValue("@SecLevel", 0)
                    command.Parameters.AddWithValue("@SecLoginId", "")
                    command.Parameters.AddWithValue("@SecPassword", "")
                    command.Parameters.AddWithValue("@SecExpiryDate", DBNull.Value)
                    command.Parameters.AddWithValue("@Address1", "")
                    command.Parameters.AddWithValue("@SecChangePassword", 0)
                    command.Parameters.AddWithValue("@SecDisablePassword", 0)
                    command.Parameters.AddWithValue("@Payroll_Coy", "")
                    command.Parameters.AddWithValue("@EmpGroup", txtStaffGroup.Text.ToUpper)
                    command.Parameters.AddWithValue("@Trade", "")
                    command.Parameters.AddWithValue("@WP_EP_NO", txtWPEPNo.Text.ToUpper)
                    command.Parameters.AddWithValue("@FIN_NO", "")
                    command.Parameters.AddWithValue("@DateArrival", DBNull.Value)
                    command.Parameters.AddWithValue("@PP_Expiry", DBNull.Value)
                    If String.IsNullOrEmpty(txtWPEPexpiry.Text) Then
                        command.Parameters.AddWithValue("@WP_EP_Expiry", DBNull.Value)
                    Else
                        command.Parameters.AddWithValue("@WP_EP_Expiry", Convert.ToDateTime(txtWPEPexpiry.Text).ToString("yyyy-MM-dd"))

                    End If
                    command.Parameters.AddWithValue("@MonthLevy", 0)
                    command.Parameters.AddWithValue("@AgentCode", "")
                    command.Parameters.AddWithValue("@PPNo", txtPassportNo.Text.ToUpper)
                    command.Parameters.AddWithValue("@PPType", "")
                    command.Parameters.AddWithValue("@PPLocation", "")
                    command.Parameters.AddWithValue("@Nationality", txtNationality.Text.ToUpper)
                    If ddlSystemUser.Text = txtDDLText.Text Then
                        command.Parameters.AddWithValue("@SystemUser", "")
                    Else
                        command.Parameters.AddWithValue("@SystemUser", ddlSystemUser.Text.ToUpper)
                    End If

                    command.Parameters.AddWithValue("@HLEVEL", "")
                    command.Parameters.AddWithValue("@HDEPT", "")
                    command.Parameters.AddWithValue("@WP_EP_ApplyDt", DBNull.Value)
                    If String.IsNullOrEmpty(txtPassportExpiry.Text) Then
                        command.Parameters.AddWithValue("@PassPort_Expiry", DBNull.Value)
                    Else
                        command.Parameters.AddWithValue("@PassPort_Expiry", Convert.ToDateTime(txtPassportExpiry.Text).ToString("yyyy-MM-dd"))

                    End If
                    command.Parameters.AddWithValue("@Sec_Bond_No", "")
                    command.Parameters.AddWithValue("@Sec_Bond_Expiry", DBNull.Value)
                    command.Parameters.AddWithValue("@Soc_Cert_No", "")
                    command.Parameters.AddWithValue("@Soc_Cert_Expiry", DBNull.Value)
                    command.Parameters.AddWithValue("@InterfaceLanguage", txtInterfaceLang.Text.ToUpper)
                    command.Parameters.AddWithValue("@Language1", "")
                    command.Parameters.AddWithValue("@Language2", "")
                    command.Parameters.AddWithValue("@Language3", "")
                    command.Parameters.AddWithValue("@Language4", "")
                    command.Parameters.AddWithValue("@Language5", "")
                    command.Parameters.AddWithValue("@Language6", "")
                    command.Parameters.AddWithValue("@WebPassword", "")
                    command.Parameters.AddWithValue("@CostCenter", "")
                    command.Parameters.AddWithValue("@SalaryType", "")
                    command.Parameters.AddWithValue("@BasicPay", 0)
                    command.Parameters.AddWithValue("@Share", 0)
                    command.Parameters.AddWithValue("@DaysPerWeek", 0)
                    command.Parameters.AddWithValue("@SDLyn", "")
                    command.Parameters.AddWithValue("@PayBasis", "")
                    command.Parameters.AddWithValue("@ComputeMethod", "")
                    command.Parameters.AddWithValue("@PayMethod", "")
                    command.Parameters.AddWithValue("@BankCode", "")
                    command.Parameters.AddWithValue("@BranchCode", "")
                    command.Parameters.AddWithValue("@AccountNo", "")
                    command.Parameters.AddWithValue("@CPFgroup", "")
                    command.Parameters.AddWithValue("@CPFsub", "")
                    command.Parameters.AddWithValue("@FWLcode", "")
                    command.Parameters.AddWithValue("@CPFno", "")
                    command.Parameters.AddWithValue("@FundCode", "")
                    command.Parameters.AddWithValue("@FundByEmployer", 0)
                    command.Parameters.AddWithValue("@CompanyBank", "")
                    command.Parameters.AddWithValue("@CompanyCPF", "")
                    command.Parameters.AddWithValue("@DateConfirm", DBNull.Value)
                    command.Parameters.AddWithValue("@DailyLevy", 0)
                    If ddlType.Text = txtDDLText.Text Then
                        command.Parameters.AddWithValue("@Type", "")
                    Else
                        command.Parameters.AddWithValue("@Type", ddlType.Text.ToUpper)
                    End If

                    command.Parameters.AddWithValue("@HoursPerDay", 0)
                    command.Parameters.AddWithValue("@OTyn", "")
                    command.Parameters.AddWithValue("@DailyBasic", 0)
                    command.Parameters.AddWithValue("@HourlyBasic", 0)
                    command.Parameters.AddWithValue("@OT1_5", 0)
                    command.Parameters.AddWithValue("@OT2", 0)
                    command.Parameters.AddWithValue("@WorkTimeGRP", "")
                    command.Parameters.AddWithValue("@TimeCardNo", "")
                    command.Parameters.AddWithValue("@PayrollID", txtPayrollID.Text.ToUpper)
                    command.Parameters.AddWithValue("@PassType", txtPassType.Text.ToUpper)
                    command.Parameters.AddWithValue("@ALEntitlement", 0)
                    command.Parameters.AddWithValue("@TimeAllowanceYN", "")
                    command.Parameters.AddWithValue("@TimeAllowanceStart", 0)
                    command.Parameters.AddWithValue("@TimeAllowanceEnd", 0)
                    command.Parameters.AddWithValue("@TimeCardReport", 0)
                    command.Parameters.AddWithValue("@HomeBlock", txtResBlock.Text.ToUpper)
                    command.Parameters.AddWithValue("@HomeNos", txtResNo.Text.ToUpper)
                    command.Parameters.AddWithValue("@HomeFloor", txtResFloor.Text.ToUpper)
                    command.Parameters.AddWithValue("@HomeUnit", txtResUnit.Text.ToUpper)
                    command.Parameters.AddWithValue("@HomeBuilding", txtResBuilding.Text.ToUpper)
                    command.Parameters.AddWithValue("@HomeStreet", txtResStreet.Text.ToUpper)
                    command.Parameters.AddWithValue("@HomePostal", txtResPostal.Text.ToUpper)
                    If ddlResCity.Text = txtDDLText.Text Then
                        command.Parameters.AddWithValue("@HomeCity", "")
                    Else
                        command.Parameters.AddWithValue("@HomeCity", ddlResCity.Text.ToUpper)
                    End If
                    If ddlResState.Text = txtDDLText.Text Then
                        command.Parameters.AddWithValue("@HomeState", "")
                    Else
                        command.Parameters.AddWithValue("@HomeState", ddlResState.Text.ToUpper)
                    End If
                    If ddlResCountry.Text = txtDDLText.Text Then
                        command.Parameters.AddWithValue("@HomeCountry", "")
                    Else
                        command.Parameters.AddWithValue("@HomeCountry", ddlResCountry.Text.ToUpper)
                    End If
                    command.Parameters.AddWithValue("@HomeAddress1", txtResAddr.Text.ToUpper)
                    command.Parameters.AddWithValue("@HomeTel", txtResTel.Text.ToUpper)
                    command.Parameters.AddWithValue("@HomeMobile", txtResMobile.Text.ToUpper)
                    command.Parameters.AddWithValue("@HomePager", "")
                    command.Parameters.AddWithValue("@HomeEmail", txtResEmail.Text.ToUpper)
                    command.Parameters.AddWithValue("@Status", ddlStatus.Text.ToUpper)
                    command.Parameters.AddWithValue("@Meal", 0)
                    command.Parameters.AddWithValue("@Transport", 0)
                    command.Parameters.AddWithValue("@Shift", 0)
                    command.Parameters.AddWithValue("@RoomNo", "")
                    command.Parameters.AddWithValue("@ShiftCode", "")
                    command.Parameters.AddWithValue("@LocationID", txtLocationID.Text.ToUpper)
                    command.Parameters.AddWithValue("@DeptSubLedger", txtDeptSubLdgr.Text.ToUpper)
                    command.Parameters.AddWithValue("@IR8SIndicator", 0)
                    command.Parameters.AddWithValue("@POSBranch", "")
                    command.Parameters.AddWithValue("@JobType_Auth_Users", "")
                    command.Parameters.AddWithValue("@WHLocation", txtWHBranch.Text.ToUpper)
                    command.Parameters.AddWithValue("@ZNSecPassword", "")
                    command.Parameters.AddWithValue("@PayCompute", "")
                    command.Parameters.AddWithValue("@EffectPeriod", "")
                    command.Parameters.AddWithValue("@CPFYN", "")
                    command.Parameters.AddWithValue("@WorkEnqScreen", "")
                    command.Parameters.AddWithValue("@StaffPic", "")
                    command.Parameters.AddWithValue("@SecMobileLoginID", "")
                    command.Parameters.AddWithValue("@SecMobilePassword", "")
                    command.Parameters.AddWithValue("@SecTabletMobileNo", "")
                    command.Parameters.AddWithValue("@SecWebLoginID", "")
                    command.Parameters.AddWithValue("@SecWebPassword", "")
                    command.Parameters.AddWithValue("@ProjectCode", "")
                    command.Parameters.AddWithValue("@HRSecurityLevel", "")
                    command.Parameters.AddWithValue("@SecGoogleEmail", "")
                    command.Parameters.AddWithValue("@SecGooglePassword", "")
                    command.Parameters.AddWithValue("@SecGoogleTaskEvent", 0)
                    command.Parameters.AddWithValue("@SecGoogleJobReqDate", 0)
                    command.Parameters.AddWithValue("@SecGoogleServDate", 0)
                    command.Parameters.AddWithValue("@SlsmanYN", 0)
                    command.Parameters.AddWithValue("@WebDisableYN", 0)
                    command.Parameters.AddWithValue("@OTPYN", 0)
                    command.Parameters.AddWithValue("@WebID", 0)
                    command.Parameters.AddWithValue("@EmployerTaxShare", 0)
                    command.Parameters.AddWithValue("@EmployeeTaxShare", 0)
                    command.Parameters.AddWithValue("@Passcode", "")
                    command.Parameters.AddWithValue("@LeaveGroup", "")
                    command.Parameters.AddWithValue("@AnnualLeaveIncrement", 0)
                    command.Parameters.AddWithValue("@MaxBringForward", 0)
                    command.Parameters.AddWithValue("@MaxLeaveEntitlement", 0)
                    command.Parameters.AddWithValue("@EvenDistribution", 0)
                    command.Parameters.AddWithValue("@CustomizedDistribution", 0)
                    command.Parameters.AddWithValue("@Month1", 0)
                    command.Parameters.AddWithValue("@Month2", 0)
                    command.Parameters.AddWithValue("@Month3", 0)
                    command.Parameters.AddWithValue("@Month4", 0)
                    command.Parameters.AddWithValue("@Month5", 0)
                    command.Parameters.AddWithValue("@Month6", 0)
                    command.Parameters.AddWithValue("@Month7", 0)
                    command.Parameters.AddWithValue("@Month8", 0)
                    command.Parameters.AddWithValue("@Month9", 0)
                    command.Parameters.AddWithValue("@Month10", 0)
                    command.Parameters.AddWithValue("@Month11", 0)
                    command.Parameters.AddWithValue("@Month12", 0)
                    command.Parameters.AddWithValue("@SubCompanyNo", "")
                    command.Parameters.AddWithValue("@SourceCompany", "")

                    'Upload image
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    If FileUpload1.HasFile Then

                        'getting length of uploaded file
                        Dim length As Integer = FileUpload1.PostedFile.ContentLength
                        '     MessageBox.Message.Alert(Page, length.ToString, "str")
                        'create a byte array to store the binary image data
                        Dim imgbyte As Byte() = New Byte(length - 1) {}
                        'store the currently selected file in memeory
                        Dim img As HttpPostedFile = FileUpload1.PostedFile
                        'set the binary data
                        img.InputStream.Read(imgbyte, 0, length)
                        Dim bytes As Byte() = FileUpload1.FileBytes

                        Image2.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(bytes)


                        command.Parameters.AddWithValue("@Photo", imgbyte)
                    Else
                        command.Parameters.AddWithValue("@Photo", "")

                    End If

                    command.Parameters.AddWithValue("@VerifyBySMS", 0)
                    command.Parameters.AddWithValue("@Singature", "")
                    command.Parameters.AddWithValue("@CalendarColor", "")
                    command.Parameters.AddWithValue("@PreferredName", txtPrefName.Text.ToUpper)
                    command.Parameters.AddWithValue("@SalesGroup", "")
                    command.Parameters.AddWithValue("@CompanyGroup", "")
                    If ddlRoles.SelectedIndex = 0 Then
                        command.Parameters.AddWithValue("@Roles", "")
                    Else
                        command.Parameters.AddWithValue("@Roles", ddlRoles.Text.ToUpper)
                    End If
                    command.Parameters.AddWithValue("@SecGroupAuthorityTablet", "")
                    command.Parameters.AddWithValue("@SecLoginComments", "")
                    command.Parameters.AddWithValue("@WebUploadDate", DBNull.Value)
                    command.Parameters.AddWithValue("@PRIssueDate", DBNull.Value)
                    command.Parameters.AddWithValue("@UploadDate", DBNull.Value)
                    command.Parameters.AddWithValue("@IncentiveRate", 0)
                    command.Parameters.AddWithValue("@RptDepartment", "")
                    command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                    command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                    command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                    command.Parameters.AddWithValue("@StaffId", txtStaffID.Text.ToUpper)
                    If ddlSalute.Text = txtDDLText.Text Then
                        command.Parameters.AddWithValue("@Salute", "")
                    Else
                        command.Parameters.AddWithValue("@Salute", ddlSalute.Text)
                    End If

                    command.Parameters.AddWithValue("@Name", txtName.Text.ToUpper)
                    command.Parameters.AddWithValue("@Nric", txtNRIC.Text)
                    command.Parameters.AddWithValue("@CountryOfBirth", "")
                    If String.IsNullOrEmpty(txtDOB.Text) Then
                        command.Parameters.AddWithValue("@DateOfBirth", DBNull.Value)
                    Else
                        command.Parameters.AddWithValue("@DateOfBirth", Convert.ToDateTime(txtDOB.Text).ToString("yyyy-MM-dd"))

                    End If
                    command.Parameters.AddWithValue("@Citizenship", txtCitizenship.Text)
                    command.Parameters.AddWithValue("@Race", "")
                    command.Parameters.AddWithValue("@Sex", "")
                    command.Parameters.AddWithValue("@MartialStatus", ddlMaritalStatus.Text)
                    command.Parameters.AddWithValue("@AddBlock", "")
                    command.Parameters.AddWithValue("@AddNos", "")
                    command.Parameters.AddWithValue("@AddFloor", "")
                    command.Parameters.AddWithValue("@AddUnit", "")
                    command.Parameters.AddWithValue("@AddBuilding", "")
                    command.Parameters.AddWithValue("@AddStreet", "")
                    command.Parameters.AddWithValue("@AddPostal", "")
                    command.Parameters.AddWithValue("@AddCity", "")
                    command.Parameters.AddWithValue("@AddState", "")
                    command.Parameters.AddWithValue("@AddCountry", "")
                    command.Parameters.AddWithValue("@TelHome", txtTel.Text)
                    command.Parameters.AddWithValue("@TelMobile", txtMobile.Text)
                    command.Parameters.AddWithValue("@TelPager", "")
                    command.Parameters.AddWithValue("@Location", txtLocationName.Text)
                    command.Parameters.AddWithValue("@Department", txtDept.Text)
                    command.Parameters.AddWithValue("@Profession", txtProfession.Text)
                    command.Parameters.AddWithValue("@Appointment", txtAppt.Text)
                    command.Parameters.AddWithValue("@DirectLine", "")
                    command.Parameters.AddWithValue("@Extension", "")
                    command.Parameters.AddWithValue("@EmailPerson", txtEmail.Text)
                    command.Parameters.AddWithValue("@EmailCompany", "")
                    If String.IsNullOrEmpty(txtDateJoined.Text) Then
                        command.Parameters.AddWithValue("@DateJoin", DBNull.Value)
                    Else
                        command.Parameters.AddWithValue("@DateJoin", Convert.ToDateTime(txtDateJoined.Text).ToString("yyyy-MM-dd"))
                    End If
                    If String.IsNullOrEmpty(txtDateLeft.Text) Then
                        command.Parameters.AddWithValue("@DateLeft", DBNull.Value)
                    Else
                        command.Parameters.AddWithValue("@DateLeft", Convert.ToDateTime(txtDateLeft.Text).ToString("yyyy-MM-dd"))

                    End If
                    command.Parameters.AddWithValue("@Comments", txtComments.Text)
                    command.Parameters.AddWithValue("@SecGroupAuthority", "")
                    command.Parameters.AddWithValue("@SecLevel", 0)
                    command.Parameters.AddWithValue("@SecLoginId", "")
                    command.Parameters.AddWithValue("@SecPassword", "")
                    command.Parameters.AddWithValue("@SecExpiryDate", DBNull.Value)
                    command.Parameters.AddWithValue("@Address1", "")
                    command.Parameters.AddWithValue("@SecChangePassword", 0)
                    command.Parameters.AddWithValue("@SecDisablePassword", 0)
                    command.Parameters.AddWithValue("@Payroll_Coy", "")
                    command.Parameters.AddWithValue("@EmpGroup", txtStaffGroup.Text)
                    command.Parameters.AddWithValue("@Trade", "")
                    command.Parameters.AddWithValue("@WP_EP_NO", txtWPEPNo.Text)
                    command.Parameters.AddWithValue("@FIN_NO", "")
                    command.Parameters.AddWithValue("@DateArrival", DBNull.Value)
                    command.Parameters.AddWithValue("@PP_Expiry", DBNull.Value)
                    If String.IsNullOrEmpty(txtWPEPexpiry.Text) Then
                        command.Parameters.AddWithValue("@WP_EP_Expiry", DBNull.Value)
                    Else
                        command.Parameters.AddWithValue("@WP_EP_Expiry", Convert.ToDateTime(txtWPEPexpiry.Text).ToString("yyyy-MM-dd"))

                    End If
                    command.Parameters.AddWithValue("@MonthLevy", 0)
                    command.Parameters.AddWithValue("@AgentCode", "")
                    command.Parameters.AddWithValue("@PPNo", txtPassportNo.Text)
                    command.Parameters.AddWithValue("@PPType", "")
                    command.Parameters.AddWithValue("@PPLocation", "")
                    command.Parameters.AddWithValue("@Nationality", txtNationality.Text)
                    If ddlSystemUser.Text = txtDDLText.Text Then
                        command.Parameters.AddWithValue("@SystemUser", "")
                    Else
                        command.Parameters.AddWithValue("@SystemUser", ddlSystemUser.Text)
                    End If

                    command.Parameters.AddWithValue("@HLEVEL", "")
                    command.Parameters.AddWithValue("@HDEPT", "")
                    command.Parameters.AddWithValue("@WP_EP_ApplyDt", DBNull.Value)
                    If String.IsNullOrEmpty(txtPassportExpiry.Text) Then
                        command.Parameters.AddWithValue("@PassPort_Expiry", DBNull.Value)
                    Else
                        command.Parameters.AddWithValue("@PassPort_Expiry", Convert.ToDateTime(txtPassportExpiry.Text).ToString("yyyy-MM-dd"))

                    End If
                    command.Parameters.AddWithValue("@Sec_Bond_No", "")
                    command.Parameters.AddWithValue("@Sec_Bond_Expiry", DBNull.Value)
                    command.Parameters.AddWithValue("@Soc_Cert_No", "")
                    command.Parameters.AddWithValue("@Soc_Cert_Expiry", DBNull.Value)
                    command.Parameters.AddWithValue("@InterfaceLanguage", txtInterfaceLang.Text)
                    command.Parameters.AddWithValue("@Language1", "")
                    command.Parameters.AddWithValue("@Language2", "")
                    command.Parameters.AddWithValue("@Language3", "")
                    command.Parameters.AddWithValue("@Language4", "")
                    command.Parameters.AddWithValue("@Language5", "")
                    command.Parameters.AddWithValue("@Language6", "")
                    command.Parameters.AddWithValue("@WebPassword", "")
                    command.Parameters.AddWithValue("@CostCenter", "")
                    command.Parameters.AddWithValue("@SalaryType", "")
                    command.Parameters.AddWithValue("@BasicPay", 0)
                    command.Parameters.AddWithValue("@Share", 0)
                    command.Parameters.AddWithValue("@DaysPerWeek", 0)
                    command.Parameters.AddWithValue("@SDLyn", "")
                    command.Parameters.AddWithValue("@PayBasis", "")
                    command.Parameters.AddWithValue("@ComputeMethod", "")
                    command.Parameters.AddWithValue("@PayMethod", "")
                    command.Parameters.AddWithValue("@BankCode", "")
                    command.Parameters.AddWithValue("@BranchCode", "")
                    command.Parameters.AddWithValue("@AccountNo", "")
                    command.Parameters.AddWithValue("@CPFgroup", "")
                    command.Parameters.AddWithValue("@CPFsub", "")
                    command.Parameters.AddWithValue("@FWLcode", "")
                    command.Parameters.AddWithValue("@CPFno", "")
                    command.Parameters.AddWithValue("@FundCode", "")
                    command.Parameters.AddWithValue("@FundByEmployer", 0)
                    command.Parameters.AddWithValue("@CompanyBank", "")
                    command.Parameters.AddWithValue("@CompanyCPF", "")
                    command.Parameters.AddWithValue("@DateConfirm", DBNull.Value)
                    command.Parameters.AddWithValue("@DailyLevy", 0)
                    If ddlType.Text = txtDDLText.Text Then
                        command.Parameters.AddWithValue("@Type", "")
                    Else
                        command.Parameters.AddWithValue("@Type", ddlType.Text)
                    End If

                    command.Parameters.AddWithValue("@HoursPerDay", 0)
                    command.Parameters.AddWithValue("@OTyn", "")
                    command.Parameters.AddWithValue("@DailyBasic", 0)
                    command.Parameters.AddWithValue("@HourlyBasic", 0)
                    command.Parameters.AddWithValue("@OT1_5", 0)
                    command.Parameters.AddWithValue("@OT2", 0)
                    command.Parameters.AddWithValue("@WorkTimeGRP", "")
                    command.Parameters.AddWithValue("@TimeCardNo", "")
                    command.Parameters.AddWithValue("@PayrollID", txtPayrollID.Text)
                    command.Parameters.AddWithValue("@PassType", txtPassType.Text)
                    command.Parameters.AddWithValue("@ALEntitlement", 0)
                    command.Parameters.AddWithValue("@TimeAllowanceYN", "")
                    command.Parameters.AddWithValue("@TimeAllowanceStart", 0)
                    command.Parameters.AddWithValue("@TimeAllowanceEnd", 0)
                    command.Parameters.AddWithValue("@TimeCardReport", 0)
                    command.Parameters.AddWithValue("@HomeBlock", txtResBlock.Text)
                    command.Parameters.AddWithValue("@HomeNos", txtResNo.Text)
                    command.Parameters.AddWithValue("@HomeFloor", txtResFloor.Text)
                    command.Parameters.AddWithValue("@HomeUnit", txtResUnit.Text)
                    command.Parameters.AddWithValue("@HomeBuilding", txtResBuilding.Text)
                    command.Parameters.AddWithValue("@HomeStreet", txtResStreet.Text)
                    command.Parameters.AddWithValue("@HomePostal", txtResPostal.Text)
                    If ddlResCity.Text = txtDDLText.Text Then
                        command.Parameters.AddWithValue("@HomeCity", "")
                    Else
                        command.Parameters.AddWithValue("@HomeCity", ddlResCity.Text)
                    End If
                    If ddlResState.Text = txtDDLText.Text Then
                        command.Parameters.AddWithValue("@HomeState", "")
                    Else
                        command.Parameters.AddWithValue("@HomeState", ddlResState.Text)
                    End If
                    If ddlResCountry.Text = txtDDLText.Text Then
                        command.Parameters.AddWithValue("@HomeCountry", "")
                    Else
                        command.Parameters.AddWithValue("@HomeCountry", ddlResCountry.Text)
                    End If
                    command.Parameters.AddWithValue("@HomeAddress1", txtResAddr.Text)
                    command.Parameters.AddWithValue("@HomeTel", txtResTel.Text)
                    command.Parameters.AddWithValue("@HomeMobile", txtResMobile.Text)
                    command.Parameters.AddWithValue("@HomePager", "")
                    command.Parameters.AddWithValue("@HomeEmail", txtResEmail.Text)
                    command.Parameters.AddWithValue("@Status", ddlStatus.Text)
                    command.Parameters.AddWithValue("@Meal", 0)
                    command.Parameters.AddWithValue("@Transport", 0)
                    command.Parameters.AddWithValue("@Shift", 0)
                    command.Parameters.AddWithValue("@RoomNo", "")
                    command.Parameters.AddWithValue("@ShiftCode", "")
                    command.Parameters.AddWithValue("@LocationID", txtLocationID.Text)
                    command.Parameters.AddWithValue("@DeptSubLedger", txtDeptSubLdgr.Text)
                    command.Parameters.AddWithValue("@IR8SIndicator", 0)
                    command.Parameters.AddWithValue("@POSBranch", "")
                    command.Parameters.AddWithValue("@JobType_Auth_Users", "")
                    command.Parameters.AddWithValue("@WHLocation", txtWHBranch.Text)
                    command.Parameters.AddWithValue("@ZNSecPassword", "")
                    command.Parameters.AddWithValue("@PayCompute", "")
                    command.Parameters.AddWithValue("@EffectPeriod", "")
                    command.Parameters.AddWithValue("@CPFYN", "")
                    command.Parameters.AddWithValue("@WorkEnqScreen", "")
                    command.Parameters.AddWithValue("@StaffPic", "")
                    command.Parameters.AddWithValue("@SecMobileLoginID", "")
                    command.Parameters.AddWithValue("@SecMobilePassword", "")
                    command.Parameters.AddWithValue("@SecTabletMobileNo", "")
                    command.Parameters.AddWithValue("@SecWebLoginID", "")
                    command.Parameters.AddWithValue("@SecWebPassword", "")
                    command.Parameters.AddWithValue("@ProjectCode", "")
                    command.Parameters.AddWithValue("@HRSecurityLevel", "")
                    command.Parameters.AddWithValue("@SecGoogleEmail", "")
                    command.Parameters.AddWithValue("@SecGooglePassword", "")
                    command.Parameters.AddWithValue("@SecGoogleTaskEvent", 0)
                    command.Parameters.AddWithValue("@SecGoogleJobReqDate", 0)
                    command.Parameters.AddWithValue("@SecGoogleServDate", 0)
                    command.Parameters.AddWithValue("@SlsmanYN", 0)
                    command.Parameters.AddWithValue("@WebDisableYN", 0)
                    command.Parameters.AddWithValue("@OTPYN", 0)
                    command.Parameters.AddWithValue("@WebID", 0)
                    command.Parameters.AddWithValue("@EmployerTaxShare", 0)
                    command.Parameters.AddWithValue("@EmployeeTaxShare", 0)
                    command.Parameters.AddWithValue("@Passcode", "")
                    command.Parameters.AddWithValue("@LeaveGroup", "")
                    command.Parameters.AddWithValue("@AnnualLeaveIncrement", 0)
                    command.Parameters.AddWithValue("@MaxBringForward", 0)
                    command.Parameters.AddWithValue("@MaxLeaveEntitlement", 0)
                    command.Parameters.AddWithValue("@EvenDistribution", 0)
                    command.Parameters.AddWithValue("@CustomizedDistribution", 0)
                    command.Parameters.AddWithValue("@Month1", 0)
                    command.Parameters.AddWithValue("@Month2", 0)
                    command.Parameters.AddWithValue("@Month3", 0)
                    command.Parameters.AddWithValue("@Month4", 0)
                    command.Parameters.AddWithValue("@Month5", 0)
                    command.Parameters.AddWithValue("@Month6", 0)
                    command.Parameters.AddWithValue("@Month7", 0)
                    command.Parameters.AddWithValue("@Month8", 0)
                    command.Parameters.AddWithValue("@Month9", 0)
                    command.Parameters.AddWithValue("@Month10", 0)
                    command.Parameters.AddWithValue("@Month11", 0)
                    command.Parameters.AddWithValue("@Month12", 0)
                    command.Parameters.AddWithValue("@SubCompanyNo", "")
                    command.Parameters.AddWithValue("@SourceCompany", "")

                    'Upload image
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    If FileUpload1.HasFile Then

                        'getting length of uploaded file
                        Dim length As Integer = FileUpload1.PostedFile.ContentLength
                        '     MessageBox.Message.Alert(Page, length.ToString, "str")
                        'create a byte array to store the binary image data
                        Dim imgbyte As Byte() = New Byte(length - 1) {}
                        'store the currently selected file in memeory
                        Dim img As HttpPostedFile = FileUpload1.PostedFile
                        'set the binary data
                        img.InputStream.Read(imgbyte, 0, length)
                        Dim bytes As Byte() = FileUpload1.FileBytes

                        Image2.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(bytes)


                        command.Parameters.AddWithValue("@Photo", imgbyte)
                    Else
                        command.Parameters.AddWithValue("@Photo", "")

                    End If

                    command.Parameters.AddWithValue("@VerifyBySMS", 0)
                    command.Parameters.AddWithValue("@Singature", "")
                    command.Parameters.AddWithValue("@CalendarColor", "")
                    command.Parameters.AddWithValue("@PreferredName", txtPrefName.Text)
                    command.Parameters.AddWithValue("@SalesGroup", "")
                    command.Parameters.AddWithValue("@CompanyGroup", "")
                    If ddlRoles.SelectedIndex = 0 Then
                        command.Parameters.AddWithValue("@Roles", "")
                    Else
                        command.Parameters.AddWithValue("@Roles", ddlRoles.Text.ToUpper)
                    End If
                    command.Parameters.AddWithValue("@SecGroupAuthorityTablet", "")
                    command.Parameters.AddWithValue("@SecLoginComments", "")
                    command.Parameters.AddWithValue("@WebUploadDate", DBNull.Value)
                    command.Parameters.AddWithValue("@PRIssueDate", DBNull.Value)
                    command.Parameters.AddWithValue("@UploadDate", DBNull.Value)
                    command.Parameters.AddWithValue("@IncentiveRate", 0)
                    command.Parameters.AddWithValue("@RptDepartment", "")
                    command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                    command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                    command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                End If
                command.Connection = conn

                command.ExecuteNonQuery()
                txtRcno.Text = command.LastInsertedId

                '  MessageBox.Message.Alert(Page, "Record added successfully!!!", "str")
                lblMessage.Text = "ADD: RECORD SUCCESSFULLY ADDED"
                lblAlert.Text = ""

                lblStaffID.Text = txtStaffID.Text
                lblStaffName.Text = txtName.Text
                lblStaffID1.Text = txtStaffID.Text
                lblStaffName1.Text = txtName.Text
                txtWebLoginID.Text = txtStaffID.Text
                txtMobileLoginID.Text = txtStaffID.Text
            End If
            conn.Close()
            If txtMode.Text = "NEW" Then
                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "STAFF", txtStaffID.Text, "ADD", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
            Else
                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "STAFF", txtStaffID.Text, "EDIT", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
            End If
            'Catch ex As Exception
            '    MessageBox.Message.Alert(Page, ex.ToString, "str")
            'End Try
            EnableControls()
            txtMode.Text = ""
            txt.Text = "SELECT * FROM tblstaff WHERE (Rcno <> 0) order by rcno desc"
            txtSearchStaff.Text = "Search Here"

        ElseIf txtMode.Text = "Edit" Then
            If txtRcno.Text = "" Then
                ' MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
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

                command2.CommandText = "SELECT * FROM tblstaff where staffid=@staffid and rcno<>" & Convert.ToInt32(txtRcno.Text)
                command2.Parameters.AddWithValue("@staffid", txtStaffID.Text)
                command2.Connection = conn

                Dim dr1 As MySqlDataReader = command2.ExecuteReader()
                Dim dt1 As New DataTable
                dt1.Load(dr1)

                If dt1.Rows.Count > 0 Then

                    ' MessageBox.Message.Alert(Page, "Staff ID already exists!!!", "str")
                    lblAlert.Text = "STAFF ID ALREADY EXISTS"
                    txtStaffID.Focus()
                    Exit Sub

                Else

                    Dim command1 As MySqlCommand = New MySqlCommand

                    command1.CommandType = CommandType.Text

                    command1.CommandText = "SELECT * FROM tblstaff where rcno=" & Convert.ToInt32(txtRcno.Text)
                    command1.Connection = conn

                    Dim dr As MySqlDataReader = command1.ExecuteReader()
                    Dim dt As New DataTable
                    dt.Load(dr)

                    If dt.Rows.Count > 0 Then

                        Dim command As MySqlCommand = New MySqlCommand

                        command.CommandType = CommandType.Text
                        Dim qry As String

                        ' If txtExists.Text = "True" Then
                        qry = "UPDATE tblstaff SET Salute = @Salute,Name = @Name,Nric = @Nric,DateOfBirth = @DateOfBirth,Citizenship = @Citizenship,MartialStatus = @MartialStatus,TelHome = @TelHome,TelMobile = @TelMobile, "

                        'Else
                        '    qry = "UPDATE tblstaff SET StaffId = @StaffId,Salute = @Salute,Name = @Name,Nric = @Nric,DateOfBirth = @DateOfBirth,Citizenship = @Citizenship,MartialStatus = @MartialStatus,TelHome = @TelHome,TelMobile = @TelMobile, "

                        'End If
                        qry = qry + "Location = @Location,Department = @Department,Profession = @Profession,Appointment = @Appointment,EmailPerson = @EmailPerson,DateJoin = @DateJoin,DateLeft = @DateLeft,Comments = @Comments,EmpGroup = @EmpGroup, "
                        qry = qry + "WP_EP_NO = @WP_EP_NO,WP_EP_Expiry = @WP_EP_Expiry,Nationality = @Nationality,SystemUser = @SystemUser,PassPort_Expiry = @PassPort_Expiry,PayrollID = @PayrollID,PassType = @PassType,HomeBlock = @HomeBlock,HomeNos = @HomeNos,HomeFloor = @HomeFloor, "
                        qry = qry + "HomeUnit = @HomeUnit,HomeBuilding = @HomeBuilding,HomeStreet = @HomeStreet,HomePostal = @HomePostal,HomeCity = @HomeCity,HomeState = @HomeState,HomeCountry = @HomeCountry, "
                        qry = qry + "HomeAddress1 = @HomeAddress1,HomeTel = @HomeTel,HomeMobile = @HomeMobile,HomeEmail = @HomeEmail,LocationID = @LocationID,DeptSubLedger = @DeptSubLedger,WHLocation = @WHLocation,Roles=@Roles, "
                        If FileUpload1.HasFile Then
                            qry = qry + "Photo=@Photo, "
                        End If
                        qry = qry + "LastModifiedOn = @LastModifiedOn,LastModifiedBy = @LastModifiedBy where rcno=" & Convert.ToInt32(txtRcno.Text)

                        command.CommandText = qry
                        command.Parameters.Clear()

                        If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                            command.Parameters.AddWithValue("@StaffId", txtStaffID.Text.ToUpper)
                            If ddlSalute.Text = txtDDLText.Text Then
                                command.Parameters.AddWithValue("@Salute", "")
                            Else
                                command.Parameters.AddWithValue("@Salute", ddlSalute.Text.ToUpper)
                            End If

                            command.Parameters.AddWithValue("@Name", txtName.Text.ToUpper)
                            command.Parameters.AddWithValue("@Nric", txtNRIC.Text.ToUpper)
                            command.Parameters.AddWithValue("@CountryOfBirth", "")
                            If String.IsNullOrEmpty(txtDOB.Text) Then
                                command.Parameters.AddWithValue("@DateOfBirth", DBNull.Value)
                            Else
                                command.Parameters.AddWithValue("@DateOfBirth", Convert.ToDateTime(txtDOB.Text).ToString("yyyy-MM-dd"))

                            End If
                            command.Parameters.AddWithValue("@Citizenship", txtCitizenship.Text.ToUpper)
                            command.Parameters.AddWithValue("@Race", "")
                            command.Parameters.AddWithValue("@Sex", "")

                            If ddlMaritalStatus.SelectedIndex = 0 Then
                                command.Parameters.AddWithValue("@MartialStatus", "")
                            Else
                                command.Parameters.AddWithValue("@MartialStatus", ddlMaritalStatus.Text.ToUpper)
                            End If

                            'command.Parameters.AddWithValue("@MartialStatus", ddlMaritalStatus.Text.ToUpper)
                            command.Parameters.AddWithValue("@AddBlock", "")
                            command.Parameters.AddWithValue("@AddNos", "")
                            command.Parameters.AddWithValue("@AddFloor", "")
                            command.Parameters.AddWithValue("@AddUnit", "")
                            command.Parameters.AddWithValue("@AddBuilding", "")
                            command.Parameters.AddWithValue("@AddStreet", "")
                            command.Parameters.AddWithValue("@AddPostal", "")
                            command.Parameters.AddWithValue("@AddCity", "")
                            command.Parameters.AddWithValue("@AddState", "")
                            command.Parameters.AddWithValue("@AddCountry", "")
                            command.Parameters.AddWithValue("@TelHome", txtTel.Text.ToUpper)
                            command.Parameters.AddWithValue("@TelMobile", txtMobile.Text.ToUpper)
                            command.Parameters.AddWithValue("@TelPager", "")
                            command.Parameters.AddWithValue("@Location", txtLocationName.Text.ToUpper)
                            command.Parameters.AddWithValue("@Department", txtDept.Text.ToUpper)
                            command.Parameters.AddWithValue("@Profession", txtProfession.Text.ToUpper)
                            command.Parameters.AddWithValue("@Appointment", txtAppt.Text.ToUpper)
                            command.Parameters.AddWithValue("@DirectLine", "")
                            command.Parameters.AddWithValue("@Extension", "")
                            command.Parameters.AddWithValue("@EmailPerson", txtEmail.Text.ToUpper)
                            command.Parameters.AddWithValue("@EmailCompany", "")
                            If String.IsNullOrEmpty(txtDateJoined.Text) Then
                                command.Parameters.AddWithValue("@DateJoin", DBNull.Value)
                            Else
                                command.Parameters.AddWithValue("@DateJoin", Convert.ToDateTime(txtDateJoined.Text).ToString("yyyy-MM-dd"))
                            End If
                            If String.IsNullOrEmpty(txtDateLeft.Text) Then
                                command.Parameters.AddWithValue("@DateLeft", DBNull.Value)
                            Else
                                command.Parameters.AddWithValue("@DateLeft", Convert.ToDateTime(txtDateLeft.Text).ToString("yyyy-MM-dd"))

                            End If
                            command.Parameters.AddWithValue("@Comments", txtComments.Text.ToUpper)
                            command.Parameters.AddWithValue("@SecGroupAuthority", "")
                            command.Parameters.AddWithValue("@SecLevel", 0)
                            command.Parameters.AddWithValue("@SecLoginId", "")
                            command.Parameters.AddWithValue("@SecPassword", "")
                            command.Parameters.AddWithValue("@SecExpiryDate", DBNull.Value)
                            command.Parameters.AddWithValue("@Address1", "")
                            command.Parameters.AddWithValue("@SecChangePassword", 0)
                            command.Parameters.AddWithValue("@SecDisablePassword", 0)
                            command.Parameters.AddWithValue("@Payroll_Coy", "")
                            command.Parameters.AddWithValue("@EmpGroup", txtStaffGroup.Text.ToUpper)
                            command.Parameters.AddWithValue("@Trade", "")
                            command.Parameters.AddWithValue("@WP_EP_NO", txtWPEPNo.Text.ToUpper)
                            command.Parameters.AddWithValue("@FIN_NO", "")
                            command.Parameters.AddWithValue("@DateArrival", DBNull.Value)
                            command.Parameters.AddWithValue("@PP_Expiry", DBNull.Value)
                            If String.IsNullOrEmpty(txtWPEPexpiry.Text) Then
                                command.Parameters.AddWithValue("@WP_EP_Expiry", DBNull.Value)
                            Else
                                command.Parameters.AddWithValue("@WP_EP_Expiry", Convert.ToDateTime(txtWPEPexpiry.Text).ToString("yyyy-MM-dd"))

                            End If
                            command.Parameters.AddWithValue("@MonthLevy", 0)
                            command.Parameters.AddWithValue("@AgentCode", "")
                            command.Parameters.AddWithValue("@PPNo", txtPassportNo.Text.ToUpper)
                            command.Parameters.AddWithValue("@PPType", "")
                            command.Parameters.AddWithValue("@PPLocation", "")
                            command.Parameters.AddWithValue("@Nationality", txtNationality.Text.ToUpper)
                            If ddlSystemUser.Text = txtDDLText.Text Then
                                command.Parameters.AddWithValue("@SystemUser", "")
                            Else
                                command.Parameters.AddWithValue("@SystemUser", ddlSystemUser.Text.ToUpper)
                            End If

                            command.Parameters.AddWithValue("@HLEVEL", "")
                            command.Parameters.AddWithValue("@HDEPT", "")
                            command.Parameters.AddWithValue("@WP_EP_ApplyDt", DBNull.Value)
                            If String.IsNullOrEmpty(txtPassportExpiry.Text) Then
                                command.Parameters.AddWithValue("@PassPort_Expiry", DBNull.Value)
                            Else
                                command.Parameters.AddWithValue("@PassPort_Expiry", Convert.ToDateTime(txtPassportExpiry.Text).ToString("yyyy-MM-dd"))

                            End If
                            command.Parameters.AddWithValue("@Sec_Bond_No", "")
                            command.Parameters.AddWithValue("@Sec_Bond_Expiry", DBNull.Value)
                            command.Parameters.AddWithValue("@Soc_Cert_No", "")
                            command.Parameters.AddWithValue("@Soc_Cert_Expiry", DBNull.Value)
                            command.Parameters.AddWithValue("@InterfaceLanguage", txtInterfaceLang.Text.ToUpper)
                            command.Parameters.AddWithValue("@Language1", "")
                            command.Parameters.AddWithValue("@Language2", "")
                            command.Parameters.AddWithValue("@Language3", "")
                            command.Parameters.AddWithValue("@Language4", "")
                            command.Parameters.AddWithValue("@Language5", "")
                            command.Parameters.AddWithValue("@Language6", "")
                            command.Parameters.AddWithValue("@WebPassword", "")
                            command.Parameters.AddWithValue("@CostCenter", "")
                            command.Parameters.AddWithValue("@SalaryType", "")
                            command.Parameters.AddWithValue("@BasicPay", 0)
                            command.Parameters.AddWithValue("@Share", 0)
                            command.Parameters.AddWithValue("@DaysPerWeek", 0)
                            command.Parameters.AddWithValue("@SDLyn", "")
                            command.Parameters.AddWithValue("@PayBasis", "")
                            command.Parameters.AddWithValue("@ComputeMethod", "")
                            command.Parameters.AddWithValue("@PayMethod", "")
                            command.Parameters.AddWithValue("@BankCode", "")
                            command.Parameters.AddWithValue("@BranchCode", "")
                            command.Parameters.AddWithValue("@AccountNo", "")
                            command.Parameters.AddWithValue("@CPFgroup", "")
                            command.Parameters.AddWithValue("@CPFsub", "")
                            command.Parameters.AddWithValue("@FWLcode", "")
                            command.Parameters.AddWithValue("@CPFno", "")
                            command.Parameters.AddWithValue("@FundCode", "")
                            command.Parameters.AddWithValue("@FundByEmployer", 0)
                            command.Parameters.AddWithValue("@CompanyBank", "")
                            command.Parameters.AddWithValue("@CompanyCPF", "")
                            command.Parameters.AddWithValue("@DateConfirm", DBNull.Value)
                            command.Parameters.AddWithValue("@DailyLevy", 0)
                            If ddlType.Text = txtDDLText.Text Then
                                command.Parameters.AddWithValue("@Type", "")
                            Else
                                command.Parameters.AddWithValue("@Type", ddlType.Text.ToUpper)
                            End If

                            command.Parameters.AddWithValue("@HoursPerDay", 0)
                            command.Parameters.AddWithValue("@OTyn", "")
                            command.Parameters.AddWithValue("@DailyBasic", 0)
                            command.Parameters.AddWithValue("@HourlyBasic", 0)
                            command.Parameters.AddWithValue("@OT1_5", 0)
                            command.Parameters.AddWithValue("@OT2", 0)
                            command.Parameters.AddWithValue("@WorkTimeGRP", "")
                            command.Parameters.AddWithValue("@TimeCardNo", "")
                            command.Parameters.AddWithValue("@PayrollID", txtPayrollID.Text.ToUpper)
                            command.Parameters.AddWithValue("@PassType", txtPassType.Text.ToUpper)
                            command.Parameters.AddWithValue("@ALEntitlement", 0)
                            command.Parameters.AddWithValue("@TimeAllowanceYN", "")
                            command.Parameters.AddWithValue("@TimeAllowanceStart", 0)
                            command.Parameters.AddWithValue("@TimeAllowanceEnd", 0)
                            command.Parameters.AddWithValue("@TimeCardReport", 0)
                            command.Parameters.AddWithValue("@HomeBlock", txtResBlock.Text.ToUpper)
                            command.Parameters.AddWithValue("@HomeNos", txtResNo.Text.ToUpper)
                            command.Parameters.AddWithValue("@HomeFloor", txtResFloor.Text.ToUpper)
                            command.Parameters.AddWithValue("@HomeUnit", txtResUnit.Text.ToUpper)
                            command.Parameters.AddWithValue("@HomeBuilding", txtResBuilding.Text.ToUpper)
                            command.Parameters.AddWithValue("@HomeStreet", txtResStreet.Text.ToUpper)
                            command.Parameters.AddWithValue("@HomePostal", txtResPostal.Text.ToUpper)
                            If ddlResCity.Text = txtDDLText.Text Then
                                command.Parameters.AddWithValue("@HomeCity", "")
                            Else
                                command.Parameters.AddWithValue("@HomeCity", ddlResCity.Text.ToUpper)
                            End If
                            If ddlResState.Text = txtDDLText.Text Then
                                command.Parameters.AddWithValue("@HomeState", "")
                            Else
                                command.Parameters.AddWithValue("@HomeState", ddlResState.Text.ToUpper)
                            End If
                            If ddlResCountry.Text = txtDDLText.Text Then
                                command.Parameters.AddWithValue("@HomeCountry", "")
                            Else
                                command.Parameters.AddWithValue("@HomeCountry", ddlResCountry.Text.ToUpper)
                            End If
                            command.Parameters.AddWithValue("@HomeAddress1", txtResAddr.Text.ToUpper)
                            command.Parameters.AddWithValue("@HomeTel", txtResTel.Text.ToUpper)
                            command.Parameters.AddWithValue("@HomeMobile", txtResMobile.Text.ToUpper)
                            command.Parameters.AddWithValue("@HomePager", "")
                            command.Parameters.AddWithValue("@HomeEmail", txtResEmail.Text.ToUpper)
                            command.Parameters.AddWithValue("@Status", ddlStatus.Text.ToUpper)
                            command.Parameters.AddWithValue("@Meal", 0)
                            command.Parameters.AddWithValue("@Transport", 0)
                            command.Parameters.AddWithValue("@Shift", 0)
                            command.Parameters.AddWithValue("@RoomNo", "")
                            command.Parameters.AddWithValue("@ShiftCode", "")
                            command.Parameters.AddWithValue("@LocationID", txtLocationID.Text.ToUpper)
                            command.Parameters.AddWithValue("@DeptSubLedger", txtDeptSubLdgr.Text.ToUpper)
                            command.Parameters.AddWithValue("@IR8SIndicator", 0)
                            command.Parameters.AddWithValue("@POSBranch", "")
                            command.Parameters.AddWithValue("@JobType_Auth_Users", "")
                            command.Parameters.AddWithValue("@WHLocation", txtWHBranch.Text.ToUpper)
                            command.Parameters.AddWithValue("@ZNSecPassword", "")
                            command.Parameters.AddWithValue("@PayCompute", "")
                            command.Parameters.AddWithValue("@EffectPeriod", "")
                            command.Parameters.AddWithValue("@CPFYN", "")
                            command.Parameters.AddWithValue("@WorkEnqScreen", "")
                            command.Parameters.AddWithValue("@StaffPic", "")
                            command.Parameters.AddWithValue("@SecMobileLoginID", "")
                            command.Parameters.AddWithValue("@SecMobilePassword", "")
                            command.Parameters.AddWithValue("@SecTabletMobileNo", "")
                            command.Parameters.AddWithValue("@SecWebLoginID", "")
                            command.Parameters.AddWithValue("@SecWebPassword", "")
                            command.Parameters.AddWithValue("@ProjectCode", "")
                            command.Parameters.AddWithValue("@HRSecurityLevel", "")
                            command.Parameters.AddWithValue("@SecGoogleEmail", "")
                            command.Parameters.AddWithValue("@SecGooglePassword", "")
                            command.Parameters.AddWithValue("@SecGoogleTaskEvent", 0)
                            command.Parameters.AddWithValue("@SecGoogleJobReqDate", 0)
                            command.Parameters.AddWithValue("@SecGoogleServDate", 0)
                            command.Parameters.AddWithValue("@SlsmanYN", 0)
                            command.Parameters.AddWithValue("@WebDisableYN", 0)
                            command.Parameters.AddWithValue("@OTPYN", 0)
                            command.Parameters.AddWithValue("@WebID", 0)
                            command.Parameters.AddWithValue("@EmployerTaxShare", 0)
                            command.Parameters.AddWithValue("@EmployeeTaxShare", 0)
                            command.Parameters.AddWithValue("@Passcode", "")
                            command.Parameters.AddWithValue("@LeaveGroup", "")
                            command.Parameters.AddWithValue("@AnnualLeaveIncrement", 0)
                            command.Parameters.AddWithValue("@MaxBringForward", 0)
                            command.Parameters.AddWithValue("@MaxLeaveEntitlement", 0)
                            command.Parameters.AddWithValue("@EvenDistribution", 0)
                            command.Parameters.AddWithValue("@CustomizedDistribution", 0)
                            command.Parameters.AddWithValue("@Month1", 0)
                            command.Parameters.AddWithValue("@Month2", 0)
                            command.Parameters.AddWithValue("@Month3", 0)
                            command.Parameters.AddWithValue("@Month4", 0)
                            command.Parameters.AddWithValue("@Month5", 0)
                            command.Parameters.AddWithValue("@Month6", 0)
                            command.Parameters.AddWithValue("@Month7", 0)
                            command.Parameters.AddWithValue("@Month8", 0)
                            command.Parameters.AddWithValue("@Month9", 0)
                            command.Parameters.AddWithValue("@Month10", 0)
                            command.Parameters.AddWithValue("@Month11", 0)
                            command.Parameters.AddWithValue("@Month12", 0)
                            command.Parameters.AddWithValue("@SubCompanyNo", "")
                            command.Parameters.AddWithValue("@SourceCompany", "")

                            'Upload image
                            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                            If FileUpload1.HasFile Then

                                'getting length of uploaded file
                                Dim length As Integer = FileUpload1.PostedFile.ContentLength
                                '     MessageBox.Message.Alert(Page, length.ToString, "str")
                                'create a byte array to store the binary image data
                                Dim imgbyte As Byte() = New Byte(length - 1) {}
                                'store the currently selected file in memeory
                                Dim img As HttpPostedFile = FileUpload1.PostedFile
                                'set the binary data
                                img.InputStream.Read(imgbyte, 0, length)
                                Dim bytes As Byte() = FileUpload1.FileBytes

                                Image2.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(bytes)


                                command.Parameters.AddWithValue("@Photo", imgbyte)
                            Else
                                command.Parameters.AddWithValue("@Photo", "")

                            End If

                            command.Parameters.AddWithValue("@VerifyBySMS", 0)
                            command.Parameters.AddWithValue("@Singature", "")
                            command.Parameters.AddWithValue("@CalendarColor", "")
                            command.Parameters.AddWithValue("@PreferredName", txtPrefName.Text.ToUpper)
                            command.Parameters.AddWithValue("@SalesGroup", "")
                            command.Parameters.AddWithValue("@CompanyGroup", "")
                            If ddlRoles.SelectedIndex = 0 Then
                                command.Parameters.AddWithValue("@Roles", "")
                            Else
                                command.Parameters.AddWithValue("@Roles", ddlRoles.Text.ToUpper)
                            End If
                            command.Parameters.AddWithValue("@SecGroupAuthorityTablet", "")
                            command.Parameters.AddWithValue("@SecLoginComments", "")
                            command.Parameters.AddWithValue("@WebUploadDate", DBNull.Value)
                            command.Parameters.AddWithValue("@PRIssueDate", DBNull.Value)
                            command.Parameters.AddWithValue("@UploadDate", DBNull.Value)
                            command.Parameters.AddWithValue("@IncentiveRate", 0)
                            command.Parameters.AddWithValue("@RptDepartment", "")
                            command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                            command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                            command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                            command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                        ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                            command.Parameters.AddWithValue("@StaffId", txtStaffID.Text.ToUpper)
                            If ddlSalute.Text = txtDDLText.Text Then
                                command.Parameters.AddWithValue("@Salute", "")
                            Else
                                command.Parameters.AddWithValue("@Salute", ddlSalute.Text)
                            End If

                            command.Parameters.AddWithValue("@Name", txtName.Text.ToUpper)
                            command.Parameters.AddWithValue("@Nric", txtNRIC.Text)
                            command.Parameters.AddWithValue("@CountryOfBirth", "")
                            If String.IsNullOrEmpty(txtDOB.Text) Then
                                command.Parameters.AddWithValue("@DateOfBirth", DBNull.Value)
                            Else
                                command.Parameters.AddWithValue("@DateOfBirth", Convert.ToDateTime(txtDOB.Text).ToString("yyyy-MM-dd"))

                            End If
                            command.Parameters.AddWithValue("@Citizenship", txtCitizenship.Text)
                            command.Parameters.AddWithValue("@Race", "")
                            command.Parameters.AddWithValue("@Sex", "")
                            command.Parameters.AddWithValue("@MartialStatus", ddlMaritalStatus.Text)
                            command.Parameters.AddWithValue("@AddBlock", "")
                            command.Parameters.AddWithValue("@AddNos", "")
                            command.Parameters.AddWithValue("@AddFloor", "")
                            command.Parameters.AddWithValue("@AddUnit", "")
                            command.Parameters.AddWithValue("@AddBuilding", "")
                            command.Parameters.AddWithValue("@AddStreet", "")
                            command.Parameters.AddWithValue("@AddPostal", "")
                            command.Parameters.AddWithValue("@AddCity", "")
                            command.Parameters.AddWithValue("@AddState", "")
                            command.Parameters.AddWithValue("@AddCountry", "")
                            command.Parameters.AddWithValue("@TelHome", txtTel.Text)
                            command.Parameters.AddWithValue("@TelMobile", txtMobile.Text)
                            command.Parameters.AddWithValue("@TelPager", "")
                            command.Parameters.AddWithValue("@Location", txtLocationName.Text)
                            command.Parameters.AddWithValue("@Department", txtDept.Text)
                            command.Parameters.AddWithValue("@Profession", txtProfession.Text)
                            command.Parameters.AddWithValue("@Appointment", txtAppt.Text)
                            command.Parameters.AddWithValue("@DirectLine", "")
                            command.Parameters.AddWithValue("@Extension", "")
                            command.Parameters.AddWithValue("@EmailPerson", txtEmail.Text)
                            command.Parameters.AddWithValue("@EmailCompany", "")
                            If String.IsNullOrEmpty(txtDateJoined.Text) Then
                                command.Parameters.AddWithValue("@DateJoin", DBNull.Value)
                            Else
                                command.Parameters.AddWithValue("@DateJoin", Convert.ToDateTime(txtDateJoined.Text).ToString("yyyy-MM-dd"))
                            End If
                            If String.IsNullOrEmpty(txtDateLeft.Text) Then
                                command.Parameters.AddWithValue("@DateLeft", DBNull.Value)
                            Else
                                command.Parameters.AddWithValue("@DateLeft", Convert.ToDateTime(txtDateLeft.Text).ToString("yyyy-MM-dd"))

                            End If
                            command.Parameters.AddWithValue("@Comments", txtComments.Text)
                            command.Parameters.AddWithValue("@SecGroupAuthority", "")
                            command.Parameters.AddWithValue("@SecLevel", 0)
                            command.Parameters.AddWithValue("@SecLoginId", "")
                            command.Parameters.AddWithValue("@SecPassword", "")
                            command.Parameters.AddWithValue("@SecExpiryDate", DBNull.Value)
                            command.Parameters.AddWithValue("@Address1", "")
                            command.Parameters.AddWithValue("@SecChangePassword", 0)
                            command.Parameters.AddWithValue("@SecDisablePassword", 0)
                            command.Parameters.AddWithValue("@Payroll_Coy", "")
                            command.Parameters.AddWithValue("@EmpGroup", txtStaffGroup.Text)
                            command.Parameters.AddWithValue("@Trade", "")
                            command.Parameters.AddWithValue("@WP_EP_NO", txtWPEPNo.Text)
                            command.Parameters.AddWithValue("@FIN_NO", "")
                            command.Parameters.AddWithValue("@DateArrival", DBNull.Value)
                            command.Parameters.AddWithValue("@PP_Expiry", DBNull.Value)
                            If String.IsNullOrEmpty(txtWPEPexpiry.Text) Then
                                command.Parameters.AddWithValue("@WP_EP_Expiry", DBNull.Value)
                            Else
                                command.Parameters.AddWithValue("@WP_EP_Expiry", Convert.ToDateTime(txtWPEPexpiry.Text).ToString("yyyy-MM-dd"))

                            End If
                            command.Parameters.AddWithValue("@MonthLevy", 0)
                            command.Parameters.AddWithValue("@AgentCode", "")
                            command.Parameters.AddWithValue("@PPNo", txtPassportNo.Text)
                            command.Parameters.AddWithValue("@PPType", "")
                            command.Parameters.AddWithValue("@PPLocation", "")
                            command.Parameters.AddWithValue("@Nationality", txtNationality.Text)
                            If ddlSystemUser.Text = txtDDLText.Text Then
                                command.Parameters.AddWithValue("@SystemUser", "")
                            Else
                                command.Parameters.AddWithValue("@SystemUser", ddlSystemUser.Text)
                            End If

                            command.Parameters.AddWithValue("@HLEVEL", "")
                            command.Parameters.AddWithValue("@HDEPT", "")
                            command.Parameters.AddWithValue("@WP_EP_ApplyDt", DBNull.Value)
                            If String.IsNullOrEmpty(txtPassportExpiry.Text) Then
                                command.Parameters.AddWithValue("@PassPort_Expiry", DBNull.Value)
                            Else
                                command.Parameters.AddWithValue("@PassPort_Expiry", Convert.ToDateTime(txtPassportExpiry.Text).ToString("yyyy-MM-dd"))

                            End If
                            command.Parameters.AddWithValue("@Sec_Bond_No", "")
                            command.Parameters.AddWithValue("@Sec_Bond_Expiry", DBNull.Value)
                            command.Parameters.AddWithValue("@Soc_Cert_No", "")
                            command.Parameters.AddWithValue("@Soc_Cert_Expiry", DBNull.Value)
                            command.Parameters.AddWithValue("@InterfaceLanguage", txtInterfaceLang.Text)
                            command.Parameters.AddWithValue("@Language1", "")
                            command.Parameters.AddWithValue("@Language2", "")
                            command.Parameters.AddWithValue("@Language3", "")
                            command.Parameters.AddWithValue("@Language4", "")
                            command.Parameters.AddWithValue("@Language5", "")
                            command.Parameters.AddWithValue("@Language6", "")
                            command.Parameters.AddWithValue("@WebPassword", "")
                            command.Parameters.AddWithValue("@CostCenter", "")
                            command.Parameters.AddWithValue("@SalaryType", "")
                            command.Parameters.AddWithValue("@BasicPay", 0)
                            command.Parameters.AddWithValue("@Share", 0)
                            command.Parameters.AddWithValue("@DaysPerWeek", 0)
                            command.Parameters.AddWithValue("@SDLyn", "")
                            command.Parameters.AddWithValue("@PayBasis", "")
                            command.Parameters.AddWithValue("@ComputeMethod", "")
                            command.Parameters.AddWithValue("@PayMethod", "")
                            command.Parameters.AddWithValue("@BankCode", "")
                            command.Parameters.AddWithValue("@BranchCode", "")
                            command.Parameters.AddWithValue("@AccountNo", "")
                            command.Parameters.AddWithValue("@CPFgroup", "")
                            command.Parameters.AddWithValue("@CPFsub", "")
                            command.Parameters.AddWithValue("@FWLcode", "")
                            command.Parameters.AddWithValue("@CPFno", "")
                            command.Parameters.AddWithValue("@FundCode", "")
                            command.Parameters.AddWithValue("@FundByEmployer", 0)
                            command.Parameters.AddWithValue("@CompanyBank", "")
                            command.Parameters.AddWithValue("@CompanyCPF", "")
                            command.Parameters.AddWithValue("@DateConfirm", DBNull.Value)
                            command.Parameters.AddWithValue("@DailyLevy", 0)
                            If ddlType.Text = txtDDLText.Text Then
                                command.Parameters.AddWithValue("@Type", "")
                            Else
                                command.Parameters.AddWithValue("@Type", ddlType.Text)
                            End If

                            command.Parameters.AddWithValue("@HoursPerDay", 0)
                            command.Parameters.AddWithValue("@OTyn", "")
                            command.Parameters.AddWithValue("@DailyBasic", 0)
                            command.Parameters.AddWithValue("@HourlyBasic", 0)
                            command.Parameters.AddWithValue("@OT1_5", 0)
                            command.Parameters.AddWithValue("@OT2", 0)
                            command.Parameters.AddWithValue("@WorkTimeGRP", "")
                            command.Parameters.AddWithValue("@TimeCardNo", "")
                            command.Parameters.AddWithValue("@PayrollID", txtPayrollID.Text)
                            command.Parameters.AddWithValue("@PassType", txtPassType.Text)
                            command.Parameters.AddWithValue("@ALEntitlement", 0)
                            command.Parameters.AddWithValue("@TimeAllowanceYN", "")
                            command.Parameters.AddWithValue("@TimeAllowanceStart", 0)
                            command.Parameters.AddWithValue("@TimeAllowanceEnd", 0)
                            command.Parameters.AddWithValue("@TimeCardReport", 0)
                            command.Parameters.AddWithValue("@HomeBlock", txtResBlock.Text)
                            command.Parameters.AddWithValue("@HomeNos", txtResNo.Text)
                            command.Parameters.AddWithValue("@HomeFloor", txtResFloor.Text)
                            command.Parameters.AddWithValue("@HomeUnit", txtResUnit.Text)
                            command.Parameters.AddWithValue("@HomeBuilding", txtResBuilding.Text)
                            command.Parameters.AddWithValue("@HomeStreet", txtResStreet.Text)
                            command.Parameters.AddWithValue("@HomePostal", txtResPostal.Text)
                            If ddlResCity.Text = txtDDLText.Text Then
                                command.Parameters.AddWithValue("@HomeCity", "")
                            Else
                                command.Parameters.AddWithValue("@HomeCity", ddlResCity.Text)
                            End If
                            If ddlResState.Text = txtDDLText.Text Then
                                command.Parameters.AddWithValue("@HomeState", "")
                            Else
                                command.Parameters.AddWithValue("@HomeState", ddlResState.Text)
                            End If
                            If ddlResCountry.Text = txtDDLText.Text Then
                                command.Parameters.AddWithValue("@HomeCountry", "")
                            Else
                                command.Parameters.AddWithValue("@HomeCountry", ddlResCountry.Text)
                            End If
                            command.Parameters.AddWithValue("@HomeAddress1", txtResAddr.Text)
                            command.Parameters.AddWithValue("@HomeTel", txtResTel.Text)
                            command.Parameters.AddWithValue("@HomeMobile", txtResMobile.Text)
                            command.Parameters.AddWithValue("@HomePager", "")
                            command.Parameters.AddWithValue("@HomeEmail", txtResEmail.Text)
                            command.Parameters.AddWithValue("@Status", ddlStatus.Text)
                            command.Parameters.AddWithValue("@Meal", 0)
                            command.Parameters.AddWithValue("@Transport", 0)
                            command.Parameters.AddWithValue("@Shift", 0)
                            command.Parameters.AddWithValue("@RoomNo", "")
                            command.Parameters.AddWithValue("@ShiftCode", "")
                            command.Parameters.AddWithValue("@LocationID", txtLocationID.Text)
                            command.Parameters.AddWithValue("@DeptSubLedger", txtDeptSubLdgr.Text)
                            command.Parameters.AddWithValue("@IR8SIndicator", 0)
                            command.Parameters.AddWithValue("@POSBranch", "")
                            command.Parameters.AddWithValue("@JobType_Auth_Users", "")
                            command.Parameters.AddWithValue("@WHLocation", txtWHBranch.Text)
                            command.Parameters.AddWithValue("@ZNSecPassword", "")
                            command.Parameters.AddWithValue("@PayCompute", "")
                            command.Parameters.AddWithValue("@EffectPeriod", "")
                            command.Parameters.AddWithValue("@CPFYN", "")
                            command.Parameters.AddWithValue("@WorkEnqScreen", "")
                            command.Parameters.AddWithValue("@StaffPic", "")
                            command.Parameters.AddWithValue("@SecMobileLoginID", "")
                            command.Parameters.AddWithValue("@SecMobilePassword", "")
                            command.Parameters.AddWithValue("@SecTabletMobileNo", "")
                            command.Parameters.AddWithValue("@SecWebLoginID", "")
                            command.Parameters.AddWithValue("@SecWebPassword", "")
                            command.Parameters.AddWithValue("@ProjectCode", "")
                            command.Parameters.AddWithValue("@HRSecurityLevel", "")
                            command.Parameters.AddWithValue("@SecGoogleEmail", "")
                            command.Parameters.AddWithValue("@SecGooglePassword", "")
                            command.Parameters.AddWithValue("@SecGoogleTaskEvent", 0)
                            command.Parameters.AddWithValue("@SecGoogleJobReqDate", 0)
                            command.Parameters.AddWithValue("@SecGoogleServDate", 0)
                            command.Parameters.AddWithValue("@SlsmanYN", 0)
                            command.Parameters.AddWithValue("@WebDisableYN", 0)
                            command.Parameters.AddWithValue("@OTPYN", 0)
                            command.Parameters.AddWithValue("@WebID", 0)
                            command.Parameters.AddWithValue("@EmployerTaxShare", 0)
                            command.Parameters.AddWithValue("@EmployeeTaxShare", 0)
                            command.Parameters.AddWithValue("@Passcode", "")
                            command.Parameters.AddWithValue("@LeaveGroup", "")
                            command.Parameters.AddWithValue("@AnnualLeaveIncrement", 0)
                            command.Parameters.AddWithValue("@MaxBringForward", 0)
                            command.Parameters.AddWithValue("@MaxLeaveEntitlement", 0)
                            command.Parameters.AddWithValue("@EvenDistribution", 0)
                            command.Parameters.AddWithValue("@CustomizedDistribution", 0)
                            command.Parameters.AddWithValue("@Month1", 0)
                            command.Parameters.AddWithValue("@Month2", 0)
                            command.Parameters.AddWithValue("@Month3", 0)
                            command.Parameters.AddWithValue("@Month4", 0)
                            command.Parameters.AddWithValue("@Month5", 0)
                            command.Parameters.AddWithValue("@Month6", 0)
                            command.Parameters.AddWithValue("@Month7", 0)
                            command.Parameters.AddWithValue("@Month8", 0)
                            command.Parameters.AddWithValue("@Month9", 0)
                            command.Parameters.AddWithValue("@Month10", 0)
                            command.Parameters.AddWithValue("@Month11", 0)
                            command.Parameters.AddWithValue("@Month12", 0)
                            command.Parameters.AddWithValue("@SubCompanyNo", "")
                            command.Parameters.AddWithValue("@SourceCompany", "")

                            'Upload image
                            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                            If FileUpload1.HasFile Then

                                'getting length of uploaded file
                                Dim length As Integer = FileUpload1.PostedFile.ContentLength
                                '     MessageBox.Message.Alert(Page, length.ToString, "str")
                                'create a byte array to store the binary image data
                                Dim imgbyte As Byte() = New Byte(length - 1) {}
                                'store the currently selected file in memeory
                                Dim img As HttpPostedFile = FileUpload1.PostedFile
                                'set the binary data
                                img.InputStream.Read(imgbyte, 0, length)
                                Dim bytes As Byte() = FileUpload1.FileBytes

                                Image2.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(bytes)


                                command.Parameters.AddWithValue("@Photo", imgbyte)
                            Else
                                command.Parameters.AddWithValue("@Photo", "")

                            End If

                            command.Parameters.AddWithValue("@VerifyBySMS", 0)
                            command.Parameters.AddWithValue("@Singature", "")
                            command.Parameters.AddWithValue("@CalendarColor", "")
                            command.Parameters.AddWithValue("@PreferredName", txtPrefName.Text)
                            command.Parameters.AddWithValue("@SalesGroup", "")
                            command.Parameters.AddWithValue("@CompanyGroup", "")
                            If ddlRoles.SelectedIndex = 0 Then
                                command.Parameters.AddWithValue("@Roles", "")
                            Else
                                command.Parameters.AddWithValue("@Roles", ddlRoles.Text.ToUpper)
                            End If
                            command.Parameters.AddWithValue("@SecGroupAuthorityTablet", "")
                            command.Parameters.AddWithValue("@SecLoginComments", "")
                            command.Parameters.AddWithValue("@WebUploadDate", DBNull.Value)
                            command.Parameters.AddWithValue("@PRIssueDate", DBNull.Value)
                            command.Parameters.AddWithValue("@UploadDate", DBNull.Value)
                            command.Parameters.AddWithValue("@IncentiveRate", 0)
                            command.Parameters.AddWithValue("@RptDepartment", "")
                            command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                            command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                            command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                            command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                        End If


                        command.Connection = conn

                        command.ExecuteNonQuery()

                        'If txtExists.Text = "True" Then
                        '    ' MessageBox.Message.Alert(Page, "Record updated successfully!!! Record is in use, so City cannot be updated!!!", "str")
                        '    lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED. RECORD IS IN USE, SO STAFF ID CANNOT BE UPDATED"
                        '    lblAlert.Text = ""
                        'Else
                        ' MessageBox.Message.Alert(Page, "Record updated successfully!!!", "str")
                        lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED"
                        lblAlert.Text = ""
                        '  End If

                    End If
                End If

                conn.Close()

            Catch ex As Exception
                MessageBox.Message.Alert(Page, ex.ToString, "str")
            End Try
            EnableControls()
            txtMode.Text = ""
        End If
        SqlDataSource1.SelectCommand = txt.Text
        SqlDataSource1.DataBind()
        GridView1.DataBind()
        '   GridView1.DataSourceID = "SqlDataSource1"
        '  MakeMeNull()
        If CheckIfExists() = True Then
            txtExists.Text = "True"
        Else
            txtExists.Text = "False"
        End If

        txtSearchStaff.Enabled = True
        btnGo.Enabled = True

    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        MakeMeNull()
        EnableControls()
        'DisableControls()
        txtSearchStaff.Enabled = True
        btnGo.Enabled = True
    End Sub


    Protected Sub btnQuit_Click(sender As Object, e As EventArgs) Handles btnQuit.Click
        'Me.ClientScript.RegisterClientScriptBlock(Me.[GetType](), "Close", "window.close()", True)
        Response.Redirect("Home.aspx")

    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        lblMessage.Text = ""
        If txtRcno.Text = "" Then
            '  MessageBox.Message.Alert(Page, "Select a record to delete!!!", "str")
            lblAlert.Text = "SELECT RECORD TO DELETE"
            Return

        End If
        lblMessage.Text = "ACTION: DELETE RECORD"

        Dim confirmValue As String = Request.Form("confirm_value")
        If confirmValue = "Yes" Then



            '   MessageBox.Message.Confirm(Page, "Do you want to delete the selected record?", "str", vbYesNo)
            If txtExists.Text = "True" Then
                '  MessageBox.Message.Alert(Page, "Record is in use, cannot be deleted!!!", "str")
                lblAlert.Text = "RECORD IS IN USE, CANNOT BE DELETED"
                Return
            End If
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                command1.CommandText = "SELECT * FROM tblstaff where rcno=" & Convert.ToInt32(txtRcno.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "delete from tblstaff where rcno=" & Convert.ToInt32(txtRcno.Text)

                    command.CommandText = qry

                    command.Connection = conn

                    command.ExecuteNonQuery()

                    '  MessageBox.Message.Alert(Page, "Record deleted successfully!!!", "str")
                    lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "STAFF", txtStaffID.Text, "DELETE", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
                End If
                conn.Close()

            Catch ex As Exception
                MessageBox.Message.Alert(Page, ex.ToString, "str")
            End Try
            EnableControls()


            GridView1.DataSourceID = "SqlDataSource1"
            MakeMeNull()
            lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"
        End If

    End Sub

    Protected Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If txtRcno.Text = "" Then
            ' MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
            lblAlert.Text = "SELECT RECORD TO EDIT"
            Return

        End If
        DisableControls()
        txtMode.Text = "Edit"
        lblMessage.Text = "ACTION: EDIT RECORD"
        txtStaffID.Enabled = False
        '   ddlStatus.Enabled = False

        txtSearchStaff.Enabled = False
        btnGo.Enabled = False
    End Sub

    Private Function CheckIfExists() As Boolean
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        Dim command1 As MySqlCommand = New MySqlCommand

        command1.CommandType = CommandType.Text

        command1.CommandText = "SELECT salesman FROM tblcompany where salesman is not null and salesman=@data limit 1"
        command1.Parameters.AddWithValue("@data", txtStaffID.Text)
        command1.Connection = conn

        Dim dr1 As MySqlDataReader = command1.ExecuteReader()
        Dim dt1 As New DataTable
        dt1.Load(dr1)

        If dt1.Rows.Count > 0 Then
            Return True
        End If

        Dim command2 As MySqlCommand = New MySqlCommand

        command2.CommandType = CommandType.Text

        command2.CommandText = "SELECT salesman FROM tblperson where salesman is not null and salesman=@data limit 1"
        command2.Parameters.AddWithValue("@data", txtStaffID.Text)
        command2.Connection = conn

        Dim dr2 As MySqlDataReader = command2.ExecuteReader()
        Dim dt2 As New DataTable
        dt2.Load(dr2)

        If dt2.Rows.Count > 0 Then
            Return True
        End If

        Dim command3 As MySqlCommand = New MySqlCommand

        command3.CommandType = CommandType.Text

        command3.CommandText = "SELECT scheduler,salesman  FROM tblcontract where (scheduler is not null and salesman is not null) and  scheduler=@data or salesman=@data limit 1"
        command3.Parameters.AddWithValue("@data", txtStaffID.Text)
        command3.Connection = conn

        Dim dr3 As MySqlDataReader = command3.ExecuteReader()
        Dim dt3 As New DataTable
        dt3.Load(dr3)

        If dt3.Rows.Count > 0 Then
            Return True
        End If

        Dim command4 As MySqlCommand = New MySqlCommand

        command4.CommandType = CommandType.Text

        command4.CommandText = "SELECT scheduler FROM tblservicerecord where scheduler is not null and scheduler=@data limit 1"
        command4.Parameters.AddWithValue("@data", txtStaffID.Text)
        command4.Connection = conn

        Dim dr4 As MySqlDataReader = command4.ExecuteReader()
        Dim dt4 As New DataTable
        dt4.Load(dr4)

        If dt4.Rows.Count > 0 Then
            Return True
        End If

        Dim command5 As MySqlCommand = New MySqlCommand

        command5.CommandType = CommandType.Text

        command5.CommandText = "SELECT staffid FROM tblservicerecordstaff where staffid is not null and staffid=@data limit 1"
        command5.Parameters.AddWithValue("@data", txtStaffID.Text)
        command5.Connection = conn

        Dim dr5 As MySqlDataReader = command5.ExecuteReader()
        Dim dt5 As New DataTable
        dt5.Load(dr5)

        If dt5.Rows.Count > 0 Then
            Return True
        End If
        conn.Close()

        Return False
    End Function

    Protected Sub txtStaffID_TextChanged(sender As Object, e As EventArgs) Handles txtStaffID.TextChanged
        txtStaffID.Text = txtStaffID.Text.ToUpper

    End Sub

    Protected Sub txtName_TextChanged(sender As Object, e As EventArgs) Handles txtName.TextChanged
        txtName.Text = txtName.Text.ToUpper

    End Sub


    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click

        Dim qry As String
        qry = "select * from tblstaff where rcno <> 0"
        If String.IsNullOrEmpty(txtSearchStaffID.Text) = False Then

            qry = qry + " and staffid like '%" + txtSearchStaffID.Text + "%'"
        End If

        If String.IsNullOrEmpty(txtSearchName.Text) = False Then

            qry = qry + " and name like '%" + txtSearchName.Text + "%'"
        End If

        qry = qry + " order by createdon desc,name;"
        txt.Text = qry
        MakeMeNull()
        SqlDataSource1.SelectCommand = qry
        SqlDataSource1.DataBind()
        GridView1.DataBind()
        txtSearchStaffID.Text = ""
        txtSearchName.Text = ""


    End Sub

    'Public Function InsertUpdateData(ByVal cmd As MySqlCommand) As Boolean
    '    Dim con As MySqlConnection = New MySqlConnection()

    '    con.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

    '    cmd.CommandType = CommandType.Text
    '    cmd.Connection = con
    '    Try
    '        con.Open()
    '        cmd.ExecuteNonQuery()
    '        Return True
    '    Catch ex As Exception
    '        Response.Write(ex.Message)
    '        Return False
    '    Finally
    '        con.Close()
    '        con.Dispose()
    '    End Try
    'End Function

    'Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As EventArgs)
    '    ' Read the file and convert it to Byte Array
    '    Dim filePath As String = FileUpload1.PostedFile.FileName
    '    Dim filename As String = System.IO.Path.GetFileName(filePath)
    '    Dim ext As String = System.IO.Path.GetExtension(filename)
    '    Dim contenttype As String = String.Empty
    '    MessageBox.Message.Alert(Page, ext, "str")
    '    ext = ext.ToLower

    '    'Set the contenttype based on File Extension
    '    Select Case ext
    '        Case ".doc"
    '            contenttype = "application/vnd.ms-word"
    '            Exit Select
    '        Case ".docx"
    '            contenttype = "application/vnd.ms-word"
    '            Exit Select
    '        Case ".xls"
    '            contenttype = "application/vnd.ms-excel"
    '            Exit Select
    '        Case ".xlsx"
    '            contenttype = "application/vnd.ms-excel"
    '            Exit Select
    '        Case ".jpg"
    '            contenttype = "image/jpg"
    '            Exit Select
    '        Case ".png"
    '            contenttype = "image/png"
    '            Exit Select
    '        Case ".gif"
    '            contenttype = "image/gif"
    '            Exit Select
    '        Case ".pdf"
    '            contenttype = "application/pdf"
    '            Exit Select
    '    End Select
    '    If contenttype <> String.Empty Then
    '        Dim fs As Stream = FileUpload1.PostedFile.InputStream
    '        Dim br As New BinaryReader(fs)
    '        Dim bytes As Byte() = br.ReadBytes(fs.Length)

    '        'insert the file into database
    '        Dim con As MySqlConnection = New MySqlConnection()

    '        con.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    '        con.Open()
    '        Dim strQuery As String = "update tblstaff set Photo=@Data where staffid=@staff;"
    '        Dim cmd As New MySqlCommand(strQuery)

    '        cmd.CommandText = strQuery
    '        cmd.Parameters.Clear()


    '        cmd.Parameters.AddWithValue("@Data", bytes)
    '        cmd.Parameters.AddWithValue("@STAFF", txtStaffID.Text)


    '        cmd.Connection = con

    '        '  InsertUpdateData(cmd)       


    '        cmd.ExecuteNonQuery()
    '        con.Close()

    '        lblMessage.ForeColor = System.Drawing.Color.Green
    '        lblMessage.Text = "File Uploaded Successfully"
    '    Else
    '        lblMessage.ForeColor = System.Drawing.Color.Red
    '        lblMessage.Text = "File format not recognised." _
    '        & " Upload Image/Word/PDF/Excel formats"
    '    End If
    'End Sub



    Protected Sub btnEditUser_Click(sender As Object, e As EventArgs) Handles btnEditUser.Click
        txtGroupAuthority.Enabled = True
        chkSystemUser.Enabled = True
        txtWebLoginID.Enabled = True
        txtWebLoginPassword.Enabled = True

        txtMobileLoginID.Enabled = True
        txtMobileLoginPassword.Enabled = True

        chkManualTimeIn.Enabled = True
        chkManualTimeOut.Enabled = True


        btnSaveUser.Enabled = True
        btnCancelUser.Enabled = True

        btnEditUser.Enabled = False
        lblMessage.Text = "ACTION: EDIT USER CREDENTIALS"
    End Sub

    Protected Sub btnCancelUser_Click(sender As Object, e As EventArgs) Handles btnCancelUser.Click
        lblMessage.Text = ""
        lblAlert.Text = ""
        txtGroupAuthority.Text = "GUEST"
        txtGroupAuthority.Enabled = False

        chkSystemUser.Checked = False
        txtWebLoginID.Text = txtWebLoginID1.Text
        txtWebLoginPassword.Text = txtWebLoginPassword1.Text

        chkSystemUser.Enabled = False
        txtWebLoginID.Enabled = False
        txtWebLoginPassword.Enabled = False


        txtMobileLoginID.Text = txtMobileLoginID1.Text
        txtMobileLoginPassword.Text = txtMobileLoginPassword1.Text

        txtMobileLoginID.Enabled = False
        txtMobileLoginPassword.Enabled = False

        chkManualTimeIn.Enabled = False
        chkManualTimeOut.Enabled = False

        btnSaveUser.Enabled = False
        btnCancelUser.Enabled = False

        btnEditUser.Enabled = True

    End Sub


    Protected Sub btnSaveUser_Click(sender As Object, e As EventArgs) Handles btnSaveUser.Click

        lblAlert.Text = ""

        If String.IsNullOrEmpty(txtRcno.Text) Then
            lblAlert.Text = "SELECT RECORD TO PROCEED"
            Return
        End If
        If String.IsNullOrEmpty(txtGroupAuthority.Text) Then
            lblAlert.Text = "GROUP AUTHORITY CANNOT BE BLANK"
            Return
        End If
        If chkSystemUser.Checked = True Then
            If String.IsNullOrEmpty(txtWebLoginID.Text) Then
                lblAlert.Text = "LOGIN ID CANNOT BE BLANK FOR SYSTEM USER"
                Return
            End If
            If String.IsNullOrEmpty(txtWebLoginPassword.Text) Then
                lblAlert.Text = "PASSWORD CANNOT BE BLANK FOR SYSTEM USER"
                Return
            End If
        End If
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        If String.IsNullOrEmpty(txtWebLoginID.Text) = False Then
            Dim command2 As MySqlCommand = New MySqlCommand

            command2.CommandType = CommandType.Text

            command2.CommandText = "SELECT * FROM tblstaff where SecWebLoginID=@SecWebLoginID and rcno<>" & Convert.ToInt32(txtRcno.Text)
            command2.Parameters.AddWithValue("@SecWebLoginID", txtWebLoginID.Text)
            command2.Connection = conn

            Dim dr1 As MySqlDataReader = command2.ExecuteReader()
            Dim dt1 As New DataTable
            dt1.Load(dr1)

            If dt1.Rows.Count > 0 Then

                '  MessageBox.Message.Alert(Page, "Team ID already exists!!!", "str")
                lblAlert.Text = "WebLoginID ALREADY EXISTS"
                txtWebLoginID.Focus()
                Exit Sub
            End If
        End If

        If String.IsNullOrEmpty(txtMobileLoginID.Text) = False Then
            Dim command3 As MySqlCommand = New MySqlCommand

            command3.CommandType = CommandType.Text

            command3.CommandText = "SELECT * FROM tblstaff where SecMobileLoginID=@SecMobileLoginID and rcno<>" & Convert.ToInt32(txtRcno.Text)
            command3.Parameters.AddWithValue("@SecMobileLoginID", txtMobileLoginID.Text)
            command3.Connection = conn

            Dim dr3 As MySqlDataReader = command3.ExecuteReader()
            Dim dt3 As New DataTable
            dt3.Load(dr3)

            If dt3.Rows.Count > 0 Then

                '  MessageBox.Message.Alert(Page, "Team ID already exists!!!", "str")
                lblAlert.Text = "MOBILE LOGINID ALREADY EXISTS"
                txtMobileLoginID.Focus()
                Exit Sub
            End If
        End If

        Dim command As MySqlCommand = New MySqlCommand

        command.CommandType = CommandType.Text

        command.CommandText = "UPDATE tblsTAFF SET SecGroupAuthority=@SecGroupAuthority,SystemUser=@SystemUser,SecWebLoginID=@SecWebLoginID,SecWebPassword=@SecWebPassword, SecMobileLoginID=@SecMobileLoginID,SecMobilePassword=@SecMobilePassword, MobileAppManualTimeIn=@MobileAppManualTimeIn, MobileAppManualTimeOut =@MobileAppManualTimeOut, LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(txtRcno.Text)
        command.Parameters.Clear()


        If txtGroupAuthority.SelectedIndex = 0 Then
            command.Parameters.AddWithValue("@SecGroupAuthority", "")
        Else
            command.Parameters.AddWithValue("@SecGroupAuthority", txtGroupAuthority.Text)
        End If


        If chkSystemUser.Checked Then
            command.Parameters.AddWithValue("@SystemUser", "Y")
        Else
            command.Parameters.AddWithValue("@SystemUser", "N")
        End If
        command.Parameters.AddWithValue("@SecWebLoginID", txtWebLoginID.Text)

        command.Parameters.AddWithValue("@SecMobileLoginID", txtMobileLoginID.Text)

        command.Parameters.AddWithValue("@MobileAppManualTimeIn", chkManualTimeIn.Checked)
        command.Parameters.AddWithValue("@MobileAppManualTimeOut", chkManualTimeOut.Checked)

        '''''''''''''''''''''''''''''''''
        'Password encryption
        '''''''''''''''''''''''''''''''''''
        Dim NameEncodein As Byte() = New Byte(txtWebLoginPassword.Text.Length - 1) {}
        NameEncodein = System.Text.Encoding.UTF8.GetBytes(txtWebLoginPassword.Text)
        Dim EcodedName As String = Convert.ToBase64String(NameEncodein)

        command.Parameters.AddWithValue("@SecWebPassword", EcodedName)


        Dim NameEncodein1 As Byte() = New Byte(txtMobileLoginPassword.Text.Length - 1) {}
        NameEncodein1 = System.Text.Encoding.UTF8.GetBytes(txtMobileLoginPassword.Text)
        Dim EcodedName1 As String = Convert.ToBase64String(NameEncodein1)

        command.Parameters.AddWithValue("@SecMobilePassword", EcodedName1)
        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

        command.Connection = conn

        command.ExecuteNonQuery()
        conn.Close()
        CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "STAFF", txtStaffID.Text, "CREDENTIALS", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)


        lblMessage.Text = "ACTION: USER CREDENTIALS UPDATED"

        txtWebLoginID1.Text = txtWebLoginID.Text
        txtWebLoginPassword1.Text = txtWebLoginPassword.Text
        txtMobileLoginID1.Text = txtMobileLoginID.Text
        txtMobileLoginPassword1.Text = txtMobileLoginPassword.Text

        txtGroupAuthority.Enabled = False
        chkSystemUser.Enabled = False
        txtWebLoginID.Enabled = False
        txtWebLoginPassword.Enabled = False


        txtMobileLoginID.Enabled = False
        txtMobileLoginPassword.Enabled = False

        btnSaveUser.Enabled = False
        btnCancelUser.Enabled = False

        btnEditUser.Enabled = True

        chkManualTimeIn.Enabled = False
        chkManualTimeOut.Enabled = False

        GridView1.DataBind()

    End Sub

    Private Sub CheckTab()
        'If tb1.ActiveTabIndex = 1 Or tb1.ActiveTabIndex = 2 Then
        '    GridView1.CssClass = "dummybutton"
        '    btnADD.CssClass = "dummybutton"
        '    btnEdit.CssClass = "dummybutton"
        '    btnDelete.CssClass = "dummybutton"
        '    btnQuit.CssClass = "dummybutton"
        '    btnFilter.CssClass = "dummybutton"
        '    btnPrint.CssClass = "dummybutton"
        'ElseIf tb1.ActiveTabIndex = 0 Then

        '    GridView1.CssClass = "visiblebutton"
        '    btnADD.CssClass = "visiblebutton"
        '    btnEdit.CssClass = "visiblebutton"
        '    btnDelete.CssClass = "visiblebutton"
        '    btnQuit.CssClass = "visiblebutton"
        '    btnFilter.CssClass = "visiblebutton"
        '    btnPrint.CssClass = "visiblebutton"

        'End If
    End Sub

    Protected Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Response.Redirect("RV_MasterStaff.aspx")
    End Sub

    Protected Sub btnSvcSave_Click(sender As Object, e As EventArgs)

    End Sub

    Protected Sub btnLeaveSave_Click(sender As Object, e As EventArgs) Handles btnLeaveSave.Click

        lblAlert.Text = ""

        If txtApplicationDate.Text = "" Then
            lblAlert.Text = "PLEASE ENTER APPLICATION DATE"
            ddlLeaveType.Focus()
            Exit Sub
        End If

        If ddlLeaveType.SelectedIndex = 0 Then
            lblAlert.Text = "PLEASE SELECT LEAVE TYPE"
            ddlLeaveType.Focus()
            Exit Sub
        End If
        If txtDateFrom.Text = "" Then
            ' MessageBox.Message.Alert(Page, "Service Name cannot be blank!!!", "str")
            lblAlert.Text = "PLEASE ENTER DATE FROM"
            txtDateFrom.Focus()
            Exit Sub

        End If
        If txtDateTo.Text = "" Then
            ' MessageBox.Message.Alert(Page, "Service Name cannot be blank!!!", "str")
            lblAlert.Text = "PLEASE ENTER DATE TO"
            txtDateTo.Focus()
            Exit Sub

        End If

        Dim dt As Date
        If Date.TryParseExact(txtApplicationDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, dt) Then
            txtApplicationDate.Text = dt.ToShortDateString
        Else
            lblAlert.Text = "APPLICATION DATE IS INVALID"
            Exit Sub
        End If

        If Date.TryParseExact(txtDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, dt) Then
            txtDateFrom.Text = dt.ToShortDateString
        Else
            lblAlert.Text = "DATE FROM IS INVALID"
            Exit Sub
        End If

        If Date.TryParseExact(txtDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, dt) Then
            txtDateTo.Text = dt.ToShortDateString
        Else
            lblAlert.Text = "DATE TO IS INVALID"
            Exit Sub
        End If


        If Val(txtTotalDays.Text) < 0 Then
            lblAlert.Text = "PLEASE ENTER VALID DATES"
            txtTotalDays.Text = "0"
            txtDateFrom.Focus()
            Exit Sub
        End If


        If txtSvcMode.Text = "NEW" Then
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()


                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                Dim qry As String = "INSERT INTO tblLeave(ApplicationID,ApplicationDate,StaffID,StaffName,LeaveType, LeaveDescription, Date1,Date2, DeductDays, CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn)VALUES(@ApplicationID,@ApplicationDate,@StaffID,@StaffName,@LeaveType, @LeaveDescription, @Date1,@Date2, @DeductDays, @CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn);"

                command.CommandText = qry
                command.Parameters.Clear()

                If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                    command.Parameters.AddWithValue("@ApplicationID", lblStaffID.Text.ToUpper)
                    command.Parameters.AddWithValue("@ApplicationDate", Convert.ToDateTime(txtApplicationDate.Text).ToString("yyyy-MM-dd"))
                    command.Parameters.AddWithValue("@StaffID", lblStaffID.Text.ToUpper)
                    command.Parameters.AddWithValue("@StaffName", lblStaffName.Text.ToUpper)
                    command.Parameters.AddWithValue("@LeaveType", Left(ddlLeaveType.Text, 3).ToUpper)
                    command.Parameters.AddWithValue("@LeaveDescription", Right(ddlLeaveType.Text, Len(ddlLeaveType.Text) - Len(Left(ddlLeaveType.Text, 4))).ToUpper)
                    command.Parameters.AddWithValue("@Date1", Convert.ToDateTime(txtDateFrom.Text).ToString("yyyy-MM-dd"))
                    command.Parameters.AddWithValue("@Date2", Convert.ToDateTime(txtDateTo.Text).ToString("yyyy-MM-dd"))
                    command.Parameters.AddWithValue("@DeductDays", txtTotalDays.Text.ToUpper)
                    command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                    command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                    command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                    command.Parameters.AddWithValue("@ApplicationID", lblStaffID.Text)
                    command.Parameters.AddWithValue("@ApplicationDate", Convert.ToDateTime(txtApplicationDate.Text).ToString("yyyy-MM-dd"))
                    command.Parameters.AddWithValue("@StaffID", lblStaffID.Text)
                    command.Parameters.AddWithValue("@StaffName", lblStaffName.Text)
                    command.Parameters.AddWithValue("@LeaveType", Left(ddlLeaveType.Text, 3))
                    command.Parameters.AddWithValue("@LeaveDescription", Right(ddlLeaveType.Text, Len(ddlLeaveType.Text) - Len(Left(ddlLeaveType.Text, 4))))
                    command.Parameters.AddWithValue("@Date1", Convert.ToDateTime(txtDateFrom.Text).ToString("yyyy-MM-dd"))
                    command.Parameters.AddWithValue("@Date2", Convert.ToDateTime(txtDateTo.Text).ToString("yyyy-MM-dd"))
                    command.Parameters.AddWithValue("@DeductDays", txtTotalDays.Text)
                    command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                    command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                    command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                End If


                command.Connection = conn

                command.ExecuteNonQuery()
                txtSvcRcno.Text = command.LastInsertedId

                '   MessageBox.Message.Alert(Page, "Record added successfully!!!", "str")
                lblMessage.Text = "ADD: LEAVE RECORD SUCCESSFULLY ADDED"
                lblAlert.Text = ""
                conn.Close()

            Catch ex As Exception
                MessageBox.Message.Alert(Page, ex.ToString, "str")
            End Try
            'EnableSvcControls()

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

            Dim command1 As MySqlCommand = New MySqlCommand
            command1.CommandType = CommandType.Text
            command1.CommandText = "SELECT * FROM tblLeave where rcno=" & Convert.ToInt32(txtSvcRcno.Text)
            command1.Connection = conn

            Dim dr1 As MySqlDataReader = command1.ExecuteReader()
            Dim dt1 As New DataTable
            dt1.Load(dr1)

            If dt1.Rows.Count > 0 Then

                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                Dim qry As String

                qry = "UPDATE tblLeave SET ApplicationID = @ApplicationID, ApplicationDate = @ApplicationDate,LeaveType = @LeaveType, LeaveDescription =@LeaveDescription, Date1 = @Date1,Date2 = @Date2, DeductDays=@DeductDays, LastModifiedBy = @LastModifiedBy,LastModifiedOn = @LastModifiedOn where rcno=" & Convert.ToInt32(txtSvcRcno.Text)

                command.CommandText = qry
                command.Parameters.Clear()

                If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                    command.Parameters.AddWithValue("@ApplicationID", lblStaffID.Text.ToUpper)
                    command.Parameters.AddWithValue("@ApplicationDate", Convert.ToDateTime(txtApplicationDate.Text).ToString("yyyy-MM-dd"))
                    command.Parameters.AddWithValue("@LeaveType", Left(ddlLeaveType.Text, 3).ToUpper)
                    command.Parameters.AddWithValue("@LeaveDescription", Right(ddlLeaveType.Text, Len(ddlLeaveType.Text) - Len(Left(ddlLeaveType.Text, 4))).ToUpper)
                    command.Parameters.AddWithValue("@Date1", Convert.ToDateTime(txtDateFrom.Text).ToString("yyyy-MM-dd"))
                    command.Parameters.AddWithValue("@Date2", Convert.ToDateTime(txtDateTo.Text).ToString("yyyy-MM-dd"))
                    command.Parameters.AddWithValue("@DeductDays", txtTotalDays.Text.ToUpper)
                    command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                    command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                    command.Parameters.AddWithValue("@ApplicationID", lblStaffID.Text)
                    command.Parameters.AddWithValue("@ApplicationDate", Convert.ToDateTime(txtApplicationDate.Text).ToString("yyyy-MM-dd"))
                    command.Parameters.AddWithValue("@LeaveType", Left(ddlLeaveType.Text, 3))
                    command.Parameters.AddWithValue("@LeaveDescription", Right(ddlLeaveType.Text, Len(ddlLeaveType.Text) - Len(Left(ddlLeaveType.Text, 4))))
                    command.Parameters.AddWithValue("@Date1", Convert.ToDateTime(txtDateFrom.Text).ToString("yyyy-MM-dd"))
                    command.Parameters.AddWithValue("@Date2", Convert.ToDateTime(txtDateTo.Text).ToString("yyyy-MM-dd"))
                    command.Parameters.AddWithValue("@DeductDays", txtTotalDays.Text)
                    command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                    command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                End If

                command.Connection = conn

                command.ExecuteNonQuery()


                '   MessageBox.Message.Alert(Page, "Record updated successfully!!!", "str")
                lblMessage.Text = "EDIT: LEAVE RECORD SUCCESSFULLY UPDATED"
                lblAlert.Text = ""
            End If

            conn.Close()

            'Catch ex As Exception
            '    MessageBox.Message.Alert(Page, ex.ToString, "str")
            'End Try
            'EnableSvcControls()
        End If

        If String.IsNullOrEmpty(txtStaffID.Text) Then
            SqlDataSource2.SelectCommand = "SELECT * FROM tblLeave where StaffID = '" & txtStaffID.Text & "'"
        Else
            SqlDataSource2.SelectCommand = "SELECT * FROM tblLeave where StaffID = '" & txtStaffID.Text & "'"
        End If

        SqlDataSource2.DataBind()
        GridView2.DataBind()
        ' MakeSvcNull()
        txtSvcMode.Text = ""
        DisableLeaveControls()

        'btnSvcAdd.Enabled = True
        'btnSvcAdd.ForeColor = System.Drawing.Color.Black
        ''btnCopyAdd.Enabled = True
        ''btnCopyAdd.ForeColor = System.Drawing.Color.Black
        'btnDelete.Enabled = True
        'btnDelete.ForeColor = System.Drawing.Color.Black

        btnQuit.Enabled = True
        btnQuit.ForeColor = System.Drawing.Color.Black
        'AddrConcat()

        'btnSvcSave.Enabled = False
        'btnSvcSave.ForeColor = System.Drawing.Color.Gray
        'btnSvcCancel.Enabled = False
        'btnSvcCancel.ForeColor = System.Drawing.Color.Gray
    End Sub

    Protected Sub GridView2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView2.SelectedIndexChanged
        MakeLeaveNull()
        'EnableLeaveControls()
        Dim editindex As Integer = GridView2.SelectedIndex
        Leavercno = DirectCast(GridView2.Rows(editindex).FindControl("Label1"), Label).Text
        txtSvcRcno.Text = Leavercno.ToString()

        txtSvcMode.Text = "EDIT"
        lblMessage.Text = "ACTION: EDIT LEAVE"
        If (GridView2.SelectedRow.Cells(2).Text = "&nbsp;") Then
            txtApplicationDate.Text = ""
        Else
            txtApplicationDate.Text = GridView2.SelectedRow.Cells(2).Text.Trim
        End If

        If (GridView2.SelectedRow.Cells(3).Text = "&nbsp;") Then
            txtDateFrom.Text = ""
        Else
            txtDateFrom.Text = GridView2.SelectedRow.Cells(3).Text.Trim
        End If

        If (GridView2.SelectedRow.Cells(4).Text = "&nbsp;") Then
            txtDateTo.Text = ""
        Else
            txtDateTo.Text = GridView2.SelectedRow.Cells(4).Text.Trim
        End If

        If (GridView2.SelectedRow.Cells(4).Text = "&nbsp;") Then
            txtDateTo.Text = ""
        Else
            txtDateTo.Text = GridView2.SelectedRow.Cells(4).Text.Trim
        End If

        If (GridView2.SelectedRow.Cells(5).Text = "&nbsp;") Then
            ddlLeaveType.Text = ""
        Else
            ddlLeaveType.Text = GridView2.SelectedRow.Cells(5).Text.Trim + "-" + GridView2.SelectedRow.Cells(6).Text.Trim
        End If

        If (GridView2.SelectedRow.Cells(6).Text = "&nbsp;") Then
            txtLeaveDescription.Text = ""
        Else
            txtLeaveDescription.Text = GridView2.SelectedRow.Cells(6).Text.Trim
        End If

        If (GridView2.SelectedRow.Cells(7).Text = "&nbsp;") Then
            txtTotalDays.Text = ""
        Else
            txtTotalDays.Text = GridView2.SelectedRow.Cells(7).Text.Trim
        End If

        btnSvcEdit.Enabled = True
        btnSvcEdit.ForeColor = System.Drawing.Color.Black

        btnSvcDelete.Enabled = True
        btnSvcDelete.ForeColor = System.Drawing.Color.Black

    End Sub

    Protected Sub btnSvcAdd_Click(sender As Object, e As EventArgs) Handles btnSvcAdd.Click
        txtSvcMode.Text = "NEW"
        lblMessage.Text = "ACTION: ADD LEAVE"
        EnableLeaveControls()
        MakeLeaveNull()
    End Sub

    Protected Sub btnSvcCancel_Click(sender As Object, e As EventArgs) Handles btnSvcCancel.Click
        DisableLeaveControls()
    End Sub

    Protected Sub ddlLeaveType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLeaveType.SelectedIndexChanged

    End Sub

    Protected Sub btnSvcDelete_Click(sender As Object, e As EventArgs) Handles btnSvcDelete.Click
        lblMessage.Text = ""
        If txtSvcRcno.Text = "" Then
            ' MessageBox.Message.Alert(Page, "Select a record to delete!!!", "str")
            lblAlert.Text = "SELECT RECORD TO DELETE"
            Return
        End If
        lblMessage.Text = "ACTION: DELETE LEAVE"

        Dim confirmValue As String = Request.Form("confirm_value")
        If confirmValue = "Yes" Then
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()



                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                Dim qry As String = "delete from tblleave where rcno=" & Convert.ToInt32(txtSvcRcno.Text)

                command.CommandText = qry

                command.Connection = conn

                command.ExecuteNonQuery()

                '  MessageBox.Message.Alert(Page, "Record deleted successfully!!!", "str")
                lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"

                'End If
                conn.Close()

            Catch ex As Exception
                MessageBox.Message.Alert(Page, ex.ToString, "str")
            End Try

            EnableLeaveControls()
            'If String.IsNullOrEmpty(lblStaffID.Text) Then
            '    SqlDataSource2.SelectCommand = "SELECT * FROM tblleave where rcno = " & lblStaffID.Text
            'Else
            SqlDataSource2.SelectCommand = "SELECT * FROM tblleave where StaffID = '" & lblStaffID.Text & "'"
            'End If

            SqlDataSource2.DataBind()
            GridView2.DataBind()
            MakeLeaveNull()
            EnableLeaveControls()
            'MakeSvcNull()
            lblMessage.Text = "DELETE: LEAVE RECORD SUCCESSFULLY DELETED"
        End If

    End Sub

    Protected Sub btnSvcEdit_Click(sender As Object, e As EventArgs) Handles btnSvcEdit.Click
        EnableLeaveControls()
    End Sub


    Protected Sub btnPrintLeaveRpt_Click(sender As Object, e As EventArgs) Handles btnPrintLeaveRpt.Click
        Dim dt As Date
        If String.IsNullOrEmpty(txtLeavePrintDateFrom.Text) = False Then
            If Date.TryParseExact(txtLeavePrintDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, dt) Then
                txtDateFrom.Text = dt.ToShortDateString

            Else
                MessageBox.Message.Alert(Page, "Date From is invalid", "str")

                Return
                Exit Sub

            End If
        End If
        Dim dt1 As Date
        If String.IsNullOrEmpty(txtLeavePrintDateTo.Text) = False Then
            If Date.TryParseExact(txtLeavePrintDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, dt1) Then
                txtDateTo.Text = dt1.ToShortDateString

            Else
                MessageBox.Message.Alert(Page, "Date To is invalid", "str")

                Return
                Exit Sub

            End If
        End If
        Dim selFormula As String
        selFormula = "{tblleave1.rcno}<>0"
        If String.IsNullOrEmpty(txtLeavePrintStaffId.Text) Then
            selFormula = "and {tblleave1.StaffID} LIKE '*" + txtLeavePrintStaffId.Text + "*'"
        End If
        If String.IsNullOrEmpty(txtLeavePrintDateFrom.Text) = False Then
            selFormula = selFormula + "{tblleave1.date1} >=" + "#" + dt.ToString("MM-dd-yyyy") + "#"

        End If
        If String.IsNullOrEmpty(txtLeavePrintDateTo.Text) = False Then
            selFormula = selFormula + "{tblleave1.date2} <=" + "#" + dt1.ToString("MM-dd-yyyy") + "#"

        End If
        Response.Redirect("RV_MasterStaffLeave.aspx")
    End Sub

    Protected Sub btnResetSearch_Click(sender As Object, e As ImageClickEventArgs) Handles btnResetSearch.Click
        MakeMeNull()
        EnableControls()

        txt.Text = "SELECT * FROM tblstaff WHERE (Rcno <> 0) order by name"
        SqlDataSource1.SelectCommand = "SELECT * FROM tblstaff WHERE (Rcno <> 0) order by name"
        SqlDataSource1.DataBind()
        GridView1.DataBind()
        lblMessage.Text = ""
    End Sub

    Protected Sub txtSearchStaff_TextChanged(sender As Object, e As EventArgs) Handles txtSearchStaff.TextChanged
        txtSearchStaffText.Text = txtSearchStaff.Text
        Dim qry As String
        qry = "select * from tblstaff where rcno <> 0"
        If String.IsNullOrEmpty(txtSearchStaffText.Text) = False Then

            qry = qry + " and (staffid like '%" + txtSearchStaffText.Text + "%'"

            qry = qry + " or name like '%" + txtSearchStaffText.Text + "%')"
        End If

        qry = qry + " order by rcno desc,name;"
        txt.Text = qry
        MakeMeNull()
        SqlDataSource1.SelectCommand = qry
        SqlDataSource1.DataBind()
        GridView1.DataBind()

        lblMessage.Text = "SEARCH CRITERIA : " + txtSearchText.Text + " <br/>NUMBER OF RECORDS FOUND : " + GridView1.Rows.Count.ToString

        txtSearchStaff.Text = "Search Here"

        '      

        If txtMode.Text = "Edit" Then
            lblAlert.Text = "CANNOT SELECT RECORD IN EDIT MODE. SAVE OR CANCEL TO PROCEED"
            Return
        End If

        'MakeMeNull()
        'Dim editindex As Integer = 0
        'rcno = DirectCast(GridView1.Rows(editindex).FindControl("Label1"), Label).Text
        'txtRcno.Text = rcno.ToString()


        If Convert.ToInt32(GridView1.Rows.Count.ToString) > 0 Then

            'btnQuickSearch_Click(sender, e)

            'Dim MyTi As TextInfo = New CultureInfo("en-US", False).TextInfo
            'MakeMeNull()
            'MakeMeNullBillingDetails()

            If GridView1.Rows.Count > 0 Then
                txtMode.Text = "View"
                txtRcno.Text = GridView1.Rows(0).Cells(1).Text
                PopulateRecord()
                'UpdatePanel2.Update()

                'updPanelSave.Update()
                'UpdatePanel3.Update()

                'GridView1_SelectedIndexChanged(sender, e)
            End If
        End If
        txtSearchStaff.Text = txtSearchStaffText.Text

        'Dim conn As MySqlConnection = New MySqlConnection()

        'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        'conn.Open()
        'Dim command1 As MySqlCommand = New MySqlCommand

        'command1.CommandType = CommandType.Text

        'command1.CommandText = "SELECT * FROM tblstaff where rcno=" & Convert.ToInt32(txtRcno.Text)
        'command1.Connection = conn

        'Dim dr As MySqlDataReader = command1.ExecuteReader()


        'Dim dt As New DataTable
        'dt.Load(dr)



        'If dt.Rows.Count > 0 Then
        '    txtStaffID.Text = dt.Rows(0)("StaffId").ToString
        '    txtNRIC.Text = dt.Rows(0)("Nric").ToString
        '    txtDept.Text = dt.Rows(0)("Department").ToString
        '    If dt.Rows(0)("Salute").ToString <> "" Then
        '        ddlSalute.Text = dt.Rows(0)("Salute").ToString

        '    End If
        '    txtName.Text = dt.Rows(0)("Name").ToString
        '    txtInterfaceLang.Text = dt.Rows(0)("InterfaceLanguage").ToString
        '    txtPrefName.Text = dt.Rows(0)("PreferredName").ToString
        '    If dt.Rows(0)("DateJoin").ToString = DBNull.Value.ToString Then
        '    Else
        '        txtDateJoined.Text = Convert.ToDateTime(dt.Rows(0)("DateJoin")).ToString("dd/MM/yyyy")
        '    End If
        '    If dt.Rows(0)("DateLeft").ToString = DBNull.Value.ToString Then
        '    Else
        '        txtDateLeft.Text = Convert.ToDateTime(dt.Rows(0)("DateLeft")).ToString("dd/MM/yyyy")
        '    End If
        '    txtProfession.Text = dt.Rows(0)("Profession").ToString
        '    txtAppt.Text = dt.Rows(0)("Appointment").ToString
        '    txtEmail.Text = dt.Rows(0)("EmailPerson").ToString
        '    txtTel.Text = dt.Rows(0)("TelHome").ToString
        '    txtMobile.Text = dt.Rows(0)("TelMobile").ToString

        '    'If String.IsNullOrEmpty(dt.Rows(0)("MartialStatus").ToString) = True Then
        '    '    ddlMaritalStatus.SelectedIndex = 0
        '    'ElseIf dt.Rows(0)("MartialStatus").ToString <> "MARRIED" Then
        '    'Else
        '    '    ddlMaritalStatus.Text = dt.Rows(0)("MartialStatus").ToString
        '    'End If


        '    If dt.Rows(0)("MartialStatus").ToString = "MARRIED" Or dt.Rows(0)("MartialStatus").ToString = "DIVORCED" Or dt.Rows(0)("MartialStatus").ToString = "SINGLE" Or dt.Rows(0)("MartialStatus").ToString = "SEPARATED" Then
        '        ddlMaritalStatus.SelectedIndex = 0
        '    Else
        '        ddlMaritalStatus.SelectedIndex = 0
        '    End If

        '    If String.IsNullOrEmpty(dt.Rows(0)("Status").ToString) = True Then
        '        ddlStatus.SelectedIndex = 0
        '    Else
        '        ddlStatus.Text = dt.Rows(0)("Status").ToString
        '    End If

        '    txtComments.Text = dt.Rows(0)("Comments").ToString
        '    If dt.Rows(0)("DateOfBirth").ToString = DBNull.Value.ToString Then
        '    Else
        '        txtDOB.Text = Convert.ToDateTime(dt.Rows(0)("DateOfBirth")).ToString("dd/MM/yyyy")
        '    End If
        '    txtNationality.Text = dt.Rows(0)("Nationality").ToString
        '    txtCitizenship.Text = dt.Rows(0)("Citizenship").ToString
        '    ddlSystemUser.Text = dt.Rows(0)("SystemUser").ToString
        '    txtLocationID.Text = dt.Rows(0)("LocationID").ToString
        '    txtLocationName.Text = dt.Rows(0)("Location").ToString
        '    txtWHBranch.Text = dt.Rows(0)("WHLocation").ToString
        '    txtDeptSubLdgr.Text = dt.Rows(0)("DeptSubLedger").ToString
        '    txtStaffGroup.Text = dt.Rows(0)("EmpGroup").ToString
        '    If dt.Rows(0)("Type").ToString <> "" Then
        '        ddlType.Text = dt.Rows(0)("Type").ToString
        '    End If
        '    txtPassType.Text = dt.Rows(0)("PassType").ToString
        '    txtPayrollID.Text = dt.Rows(0)("PayrollID").ToString
        '    txtPassportNo.Text = dt.Rows(0)("PPNo").ToString
        '    If dt.Rows(0)("Passport_Expiry").ToString = DBNull.Value.ToString Then
        '    Else
        '        txtPassportExpiry.Text = Convert.ToDateTime(dt.Rows(0)("Passport_Expiry")).ToString("dd/MM/yyyy")
        '    End If
        '    txtWPEPNo.Text = dt.Rows(0)("WP_EP_NO").ToString
        '    If dt.Rows(0)("WP_EP_Expiry").ToString = DBNull.Value.ToString Then
        '    Else
        '        txtWPEPexpiry.Text = Convert.ToDateTime(dt.Rows(0)("WP_EP_Expiry")).ToString("dd/MM/yyyy")
        '    End If

        '    txtResBlock.Text = dt.Rows(0)("HomeBlock").ToString
        '    txtResNo.Text = dt.Rows(0)("HomeNos").ToString
        '    txtResFloor.Text = dt.Rows(0)("HomeFloor").ToString
        '    txtResUnit.Text = dt.Rows(0)("HomeUnit").ToString
        '    txtResAddr.Text = dt.Rows(0)("HomeAddress1").ToString
        '    txtResBuilding.Text = dt.Rows(0)("HomeBuilding").ToString
        '    txtResStreet.Text = dt.Rows(0)("HomeStreet").ToString
        '    If dt.Rows(0)("HomeCity").ToString <> "" Then
        '        ddlResCity.Text = dt.Rows(0)("HomeCity").ToString
        '    End If
        '    If dt.Rows(0)("HomeState").ToString <> "" Then
        '        ddlResState.Text = dt.Rows(0)("HomeState").ToString
        '    End If
        '    If dt.Rows(0)("HomeCountry").ToString <> "" Then
        '        ddlResCountry.Text = dt.Rows(0)("HomeCountry").ToString
        '    End If


        '    txtResPostal.Text = dt.Rows(0)("HomePostal").ToString
        '    txtResTel.Text = dt.Rows(0)("HomeTel").ToString
        '    txtResMobile.Text = dt.Rows(0)("HomeMobile").ToString
        '    txtResEmail.Text = dt.Rows(0)("HomeEmail").ToString

        '    If IsDBNull(dt.Rows(0)("Photo")) = False Then
        '        Dim bytes As Byte() = DirectCast(dt.Rows(0)("Photo"), Byte())
        '        Image2.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(bytes)
        '    End If

        '    If String.IsNullOrEmpty(dt.Rows(0)("SecGroupAuthority").ToString) = True Then
        '        txtGroupAuthority.SelectedIndex = 0
        '    Else
        '        txtGroupAuthority.Text = dt.Rows(0)("SecGroupAuthority").ToString
        '    End If

        '    txtWebLoginID.Text = dt.Rows(0)("SecWebLoginID").ToString
        '    txtWebLoginID1.Text = dt.Rows(0)("SecWebLoginID").ToString

        '    txtMobileLoginID.Text = dt.Rows(0)("SecMobileLoginID").ToString
        '    txtMobileLoginID1.Text = dt.Rows(0)("SecMobileLoginID").ToString

        '    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '    'Password decryption
        '    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        '    If IsDBNull(dt.Rows(0)("SecWebPassword")) = False Then
        '        Dim data As Byte() = Convert.FromBase64String(dt.Rows(0)("SecWebPassword").ToString)
        '        Dim decodedString As String = System.Text.Encoding.UTF8.GetString(data)

        '        txtWebLoginPassword.Text = decodedString
        '        txtWebLoginPassword1.Text = decodedString
        '    End If

        '    If IsDBNull(dt.Rows(0)("SecMobilePassword")) = False Then
        '        Dim data1 As Byte() = Convert.FromBase64String(dt.Rows(0)("SecMobilePassword").ToString)
        '        Dim decodedString1 As String = System.Text.Encoding.UTF8.GetString(data1)

        '        txtMobileLoginPassword.Text = decodedString1
        '        txtMobileLoginPassword1.Text = decodedString1
        '    End If

        '    If dt.Rows(0)("SystemUser").ToString = "Y" Then
        '        chkSystemUser.Checked = True
        '    Else
        '        chkSystemUser.Checked = False
        '    End If


        '    lblStaffID.Text = txtStaffID.Text
        '    lblStaffName.Text = txtName.Text
        '    lblStaffID1.Text = txtStaffID.Text
        '    lblStaffName1.Text = txtName.Text

        'End If
        'conn.Close()

        'txtMode.Text = "View"

        ''Catch ex As Exception
        ''    MessageBox.Message.Alert(Page, "Error!!! " + ex.Message.ToString, "str")
        ''End Try
        ''txtMode.Text = "Edit"
        '' DisableControls()
        'btnEdit.Enabled = True
        'btnEdit.ForeColor = System.Drawing.Color.Black
        'btnDelete.Enabled = True
        'btnDelete.ForeColor = System.Drawing.Color.Black
        'btnQuit.Enabled = True
        'btnQuit.ForeColor = System.Drawing.Color.Black
        'btnPrintLeave.Enabled = True
        'btnPrintLeave.ForeColor = System.Drawing.Color.Black
        'btnStatus.Enabled = True
        'btnStatus.ForeColor = System.Drawing.Color.Black

        'If CheckIfExists() = True Then
        '    txtExists.Text = "True"
        'Else
        '    txtExists.Text = "False"
        'End If

        'txtGroupAuthority.Enabled = False
        'chkSystemUser.Enabled = False
        'txtWebLoginID.Enabled = False
        'txtWebLoginPassword.Enabled = False


        'txtMobileLoginID.Enabled = False
        'txtMobileLoginPassword.Enabled = False

        'btnSaveUser.Enabled = False
        'btnCancelUser.Enabled = False

        'btnEditUser.Enabled = True

        ''EnableControls()
        'If Session("SecGroupAuthority") <> "ADMINISTRATOR" Then
        '    AccessControl()
        'End If

        'tb1.ActiveTabIndex = 0
    End Sub


    Private Sub PopulateRecord()
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()
        Dim command1 As MySqlCommand = New MySqlCommand

        command1.CommandType = CommandType.Text

        command1.CommandText = "SELECT * FROM tblstaff where rcno=" & Convert.ToInt32(txtRcno.Text)
        command1.Connection = conn

        Dim dr As MySqlDataReader = command1.ExecuteReader()


        Dim dt As New DataTable
        dt.Load(dr)



        If dt.Rows.Count > 0 Then
            txtStaffID.Text = dt.Rows(0)("StaffId").ToString
            txtNRIC.Text = dt.Rows(0)("Nric").ToString
            txtDept.Text = dt.Rows(0)("Department").ToString
            If dt.Rows(0)("Salute").ToString <> "" Then
                ddlSalute.Text = dt.Rows(0)("Salute").ToString

            End If
            txtName.Text = dt.Rows(0)("Name").ToString
            txtInterfaceLang.Text = dt.Rows(0)("InterfaceLanguage").ToString
            txtPrefName.Text = dt.Rows(0)("PreferredName").ToString
            If dt.Rows(0)("DateJoin").ToString = DBNull.Value.ToString Then
            Else
                txtDateJoined.Text = Convert.ToDateTime(dt.Rows(0)("DateJoin")).ToString("dd/MM/yyyy")
            End If
            If dt.Rows(0)("DateLeft").ToString = DBNull.Value.ToString Then
            Else
                txtDateLeft.Text = Convert.ToDateTime(dt.Rows(0)("DateLeft")).ToString("dd/MM/yyyy")
            End If
            txtProfession.Text = dt.Rows(0)("Profession").ToString
            txtAppt.Text = dt.Rows(0)("Appointment").ToString
            txtEmail.Text = dt.Rows(0)("EmailPerson").ToString
            txtTel.Text = dt.Rows(0)("TelHome").ToString
            txtMobile.Text = dt.Rows(0)("TelMobile").ToString

            'If String.IsNullOrEmpty(dt.Rows(0)("MartialStatus").ToString) = True Then
            '    ddlMaritalStatus.SelectedIndex = 0
            'ElseIf dt.Rows(0)("MartialStatus").ToString <> "MARRIED" Then
            'Else
            '    ddlMaritalStatus.Text = dt.Rows(0)("MartialStatus").ToString
            'End If


            If dt.Rows(0)("MartialStatus").ToString = "MARRIED" Or dt.Rows(0)("MartialStatus").ToString = "DIVORCED" Or dt.Rows(0)("MartialStatus").ToString = "SINGLE" Or dt.Rows(0)("MartialStatus").ToString = "SEPARATED" Then
                ddlMaritalStatus.SelectedIndex = 0
            Else
                ddlMaritalStatus.SelectedIndex = 0
            End If

            If String.IsNullOrEmpty(dt.Rows(0)("Status").ToString) = True Then
                ddlStatus.SelectedIndex = 0
            Else
                ddlStatus.Text = dt.Rows(0)("Status").ToString
            End If

            If String.IsNullOrEmpty(dt.Rows(0)("Roles").ToString) = True Then
                ddlRoles.SelectedIndex = 0
            Else

                If ddlRoles.Items.FindByValue(dt.Rows(0)("Roles").ToString) Is Nothing Then
                    ddlRoles.SelectedIndex = 0
                Else
                    ddlRoles.Text = dt.Rows(0)("Roles").ToString.Trim()
                End If

            End If

            txtComments.Text = dt.Rows(0)("Comments").ToString
            If dt.Rows(0)("DateOfBirth").ToString = DBNull.Value.ToString Then
            Else
                txtDOB.Text = Convert.ToDateTime(dt.Rows(0)("DateOfBirth")).ToString("dd/MM/yyyy")
            End If
            txtNationality.Text = dt.Rows(0)("Nationality").ToString
            txtCitizenship.Text = dt.Rows(0)("Citizenship").ToString
            ddlSystemUser.Text = dt.Rows(0)("SystemUser").ToString

            If String.IsNullOrEmpty(dt.Rows(0)("LocationID").ToString) = True Then
                txtLocationId.SelectedIndex = 0
            Else
                txtLocationId.Text = dt.Rows(0)("LocationID").ToString
            End If

            txtLocationName.Text = dt.Rows(0)("Location").ToString
            txtWHBranch.Text = dt.Rows(0)("WHLocation").ToString
            txtDeptSubLdgr.Text = dt.Rows(0)("DeptSubLedger").ToString
            txtStaffGroup.Text = dt.Rows(0)("EmpGroup").ToString
            If dt.Rows(0)("Type").ToString <> "" Then
                ddlType.Text = dt.Rows(0)("Type").ToString
            End If
            txtPassType.Text = dt.Rows(0)("PassType").ToString
            txtPayrollID.Text = dt.Rows(0)("PayrollID").ToString
            txtPassportNo.Text = dt.Rows(0)("PPNo").ToString
            If dt.Rows(0)("Passport_Expiry").ToString = DBNull.Value.ToString Then
            Else
                txtPassportExpiry.Text = Convert.ToDateTime(dt.Rows(0)("Passport_Expiry")).ToString("dd/MM/yyyy")
            End If
            txtWPEPNo.Text = dt.Rows(0)("WP_EP_NO").ToString
            If dt.Rows(0)("WP_EP_Expiry").ToString = DBNull.Value.ToString Then
            Else
                txtWPEPexpiry.Text = Convert.ToDateTime(dt.Rows(0)("WP_EP_Expiry")).ToString("dd/MM/yyyy")
            End If

            txtResBlock.Text = dt.Rows(0)("HomeBlock").ToString
            txtResNo.Text = dt.Rows(0)("HomeNos").ToString
            txtResFloor.Text = dt.Rows(0)("HomeFloor").ToString
            txtResUnit.Text = dt.Rows(0)("HomeUnit").ToString
            txtResAddr.Text = dt.Rows(0)("HomeAddress1").ToString
            txtResBuilding.Text = dt.Rows(0)("HomeBuilding").ToString
            txtResStreet.Text = dt.Rows(0)("HomeStreet").ToString
            If dt.Rows(0)("HomeCity").ToString <> "" Then
                ddlResCity.Text = dt.Rows(0)("HomeCity").ToString
            End If
            If dt.Rows(0)("HomeState").ToString <> "" Then
                ddlResState.Text = dt.Rows(0)("HomeState").ToString
            End If
            If dt.Rows(0)("HomeCountry").ToString <> "" Then
                ddlResCountry.Text = dt.Rows(0)("HomeCountry").ToString
            End If


            txtResPostal.Text = dt.Rows(0)("HomePostal").ToString
            txtResTel.Text = dt.Rows(0)("HomeTel").ToString
            txtResMobile.Text = dt.Rows(0)("HomeMobile").ToString
            txtResEmail.Text = dt.Rows(0)("HomeEmail").ToString

            If IsDBNull(dt.Rows(0)("Photo")) = False Then
                Dim bytes As Byte() = DirectCast(dt.Rows(0)("Photo"), Byte())
                Image2.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(bytes)
            End If

            'txtName.Text = dt.Rows(0)("SecGroupAuthority").ToString
            If String.IsNullOrEmpty(dt.Rows(0)("SecGroupAuthority").ToString) = True Then
                txtGroupAuthority.SelectedIndex = 0
            Else
                txtGroupAuthority.Text = dt.Rows(0)("SecGroupAuthority").ToString
            End If

            txtWebLoginID.Text = dt.Rows(0)("SecWebLoginID").ToString
            txtWebLoginID1.Text = dt.Rows(0)("SecWebLoginID").ToString

            txtMobileLoginID.Text = dt.Rows(0)("SecMobileLoginID").ToString
            txtMobileLoginID1.Text = dt.Rows(0)("SecMobileLoginID").ToString

            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            'Password decryption
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            If IsDBNull(dt.Rows(0)("SecWebPassword")) = False Then
                Dim data As Byte() = Convert.FromBase64String(dt.Rows(0)("SecWebPassword").ToString)
                Dim decodedString As String = System.Text.Encoding.UTF8.GetString(data)

                txtWebLoginPassword.Text = decodedString
                txtWebLoginPassword1.Text = decodedString
            End If

            If IsDBNull(dt.Rows(0)("SecMobilePassword")) = False Then
                Dim data1 As Byte() = Convert.FromBase64String(dt.Rows(0)("SecMobilePassword").ToString)
                Dim decodedString1 As String = System.Text.Encoding.UTF8.GetString(data1)

                txtMobileLoginPassword.Text = decodedString1
                txtMobileLoginPassword1.Text = decodedString1
            End If

            If dt.Rows(0)("SystemUser").ToString = "Y" Then
                chkSystemUser.Checked = True
            Else
                chkSystemUser.Checked = False
            End If


            lblStaffID.Text = txtStaffID.Text
            lblStaffName.Text = txtName.Text
            lblStaffID1.Text = txtStaffID.Text
            lblStaffName1.Text = txtName.Text
            txtWebLoginID.Text = txtStaffID.Text
            txtMobileLoginID.Text = txtStaffID.Text

        End If
        conn.Close()

        txtMode.Text = "View"

        'Catch ex As Exception
        '    MessageBox.Message.Alert(Page, "Error!!! " + ex.Message.ToString, "str")
        'End Try
        'txtMode.Text = "Edit"
        ' DisableControls()
        btnEdit.Enabled = True
        btnEdit.ForeColor = System.Drawing.Color.Black
        btnDelete.Enabled = True
        btnDelete.ForeColor = System.Drawing.Color.Black
        btnQuit.Enabled = True
        btnQuit.ForeColor = System.Drawing.Color.Black
        btnPrintLeave.Enabled = True
        btnPrintLeave.ForeColor = System.Drawing.Color.Black
        btnStatus.Enabled = True
        btnStatus.ForeColor = System.Drawing.Color.Black

        If CheckIfExists() = True Then
            txtExists.Text = "True"
        Else
            txtExists.Text = "False"
        End If

        txtGroupAuthority.Enabled = False
        chkSystemUser.Enabled = False
        txtWebLoginID.Enabled = False
        txtWebLoginPassword.Enabled = False


        txtMobileLoginID.Enabled = False
        txtMobileLoginPassword.Enabled = False

        btnSaveUser.Enabled = False
        btnCancelUser.Enabled = False

        btnEditUser.Enabled = True

        'EnableControls()
        'If Session("SecGroupAuthority") <> "ADMINISTRATOR" Then
        AccessControl()
        'End If

        GridView1.SelectedIndex = 0

        tb1.ActiveTabIndex = 0
    End Sub


    Protected Sub btnGo_Click(sender As Object, e As ImageClickEventArgs) Handles btnGo.Click
        txtSearchStaffText.Text = txtSearchStaff.Text

        Dim qry As String
        qry = "select * from tblstaff where rcno <> 0"
        If String.IsNullOrEmpty(txtSearchStaffText.Text) = False Then

            qry = qry + " and (staffid like '%" + txtSearchStaffText.Text + "%'"

            qry = qry + " or name like '%" + txtSearchStaffText.Text + "%')"
        End If

        qry = qry + " order by rcno desc,name;"


        txt.Text = qry
        MakeMeNull()
        SqlDataSource1.SelectCommand = qry
        SqlDataSource1.DataBind()
        GridView1.DataBind()

        lblMessage.Text = "SEARCH CRITERIA : " + txtSearchText.Text + " <br/>NUMBER OF RECORDS FOUND : " + GridView1.Rows.Count.ToString

        txtSearchStaff.Text = "Search Here"


        If txtMode.Text = "Edit" Then
            lblAlert.Text = "CANNOT SELECT RECORD IN EDIT MODE. SAVE OR CANCEL TO PROCEED"
            Return
        End If

        'MakeMeNull()
        'Dim editindex As Integer = 0
        'rcno = DirectCast(GridView1.Rows(editindex).FindControl("Label1"), Label).Text
        'txtRcno.Text = rcno.ToString()


        If Convert.ToInt32(GridView1.Rows.Count.ToString) > 0 Then

            'btnQuickSearch_Click(sender, e)

            'Dim MyTi As TextInfo = New CultureInfo("en-US", False).TextInfo
            'MakeMeNull()
            'MakeMeNullBillingDetails()

            If GridView1.Rows.Count > 0 Then
                txtMode.Text = "View"
                txtRcno.Text = GridView1.Rows(0).Cells(1).Text
                PopulateRecord()
                'UpdatePanel2.Update()

                'updPanelSave.Update()
                'UpdatePanel3.Update()

                'GridView1_SelectedIndexChanged(sender, e)
            End If
        End If

        txtSearchStaff.Text = txtSearchStaffText.Text

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
        End If
    End Sub

    Protected Sub OnSelectedIndexChanged(sender As Object, e As EventArgs)
        For Each row As GridViewRow In GridView1.Rows
            If row.RowIndex = GridView1.SelectedIndex Then
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

    Protected Sub btnStatus_Click(sender As Object, e As EventArgs) Handles btnStatus.Click
        lblMessage.Text = ""
        If txtRcno.Text = "" Then
            ' MessageBox.Message.Alert(Page, "Select a record to delete!!!", "str")
            lblAlert.Text = "SELECT RECORD TO UPDATE STATUS"
            Return

        End If
        lblStaffIDStatus.Text = txtStaffID.Text
        lblOldStatus.Text = ddlStatus.Text
        ddlNewStatus.SelectedValue = ddlStatus.SelectedValue

        lblAlertStatus.Text = ""
        mdlPopupStatus.Show()
    End Sub



    Protected Sub btnUpdateStatus_Click(sender As Object, e As EventArgs) Handles btnUpdateStatus.Click
        If ddlNewStatus.Text = txtDDLText.Text Then
            lblAlertStatus.Text = "SELECT NEW STATUS"
            mdlPopupStatus.Show()

            Return

        End If
        If ddlNewStatus.Text = lblOldStatus.Text Then
            lblAlertStatus.Text = "STATUS ALREADY UPDATED"
            mdlPopupStatus.Show()

            Return
        End If
        Try
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim command As MySqlCommand = New MySqlCommand

            command.CommandType = CommandType.Text
            If lblOldStatus.Text = "D" Then
                command.CommandText = "UPDATE tblstaff SET STATUS='" + ddlNewStatus.SelectedValue + "',loginattempt=0,statusRemarks='', DateLeft=@dateleft, LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(txtRcno.Text)
            Else

                command.CommandText = "UPDATE tblstaff SET STATUS='" + ddlNewStatus.SelectedValue + "',  DateLeft=@dateleft, LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(txtRcno.Text)

            End If

            command.Connection = conn
            command.Parameters.Clear()

            command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
            command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

            If txtDateLeftStatus.Text = "" Then
                command.Parameters.AddWithValue("@Dateleft", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@Dateleft", Convert.ToDateTime(txtDateLeftStatus.Text).ToString("yyyy-MM-dd"))
            End If

            command.ExecuteNonQuery()

            '   UpdateContractActSvcDate(conn)

            'SASI - 20200819 - If a staff is made inactive, the corresponding team where the staff is an in-charge should also made inactive

            If ddlNewStatus.Text <> "O" Then
                Dim commandTeam As MySqlCommand = New MySqlCommand

                commandTeam.CommandType = CommandType.Text
                commandTeam.CommandText = "UPDATE tblTeam SET STATUS='N' where InchargeID='" + lblStaffIDStatus.Text + "'"

                commandTeam.Connection = conn

                commandTeam.ExecuteNonQuery()

            End If


            conn.Close()
            GridView1.DataBind()

            ddlStatus.Text = ddlNewStatus.Text
            ddlNewStatus.SelectedIndex = 0
            txtDateLeft.Text = txtDateLeftStatus.Text
            lblMessage.Text = "ACTION: STATUS UPDATED"
            CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CHST-STAFF", txtStaffID.Text, "CHST-STAFF", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtStaffID.Text, "", txtRcno.Text)

            SqlDataSource1.SelectCommand = txt.Text
            SqlDataSource1.DataBind()
            GridView1.DataBind()

            'GridView1.DataSourceID = "SqlDataSource1"
            mdlPopupStatus.Hide()
        Catch ex As Exception
            MessageBox.Message.Alert(Page, ex.ToString, "str")
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try

    End Sub

    Protected Sub ddlView_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlView.SelectedIndexChanged
        '   SQLDSInvoice.SelectCommand = txt.Text

        SqlDataSource1.SelectCommand = txt.Text
        SqlDataSource1.DataBind()
        GridView1.PageSize = Convert.ToInt16(ddlView.SelectedItem.Text)
        GridView1.DataBind()
    End Sub

    Protected Sub txtLocationId_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtLocationId.SelectedIndexChanged


        Dim IsAccountId As Boolean
        IsAccountId = False

        Dim connIsAccountId As MySqlConnection = New MySqlConnection()

        connIsAccountId.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        connIsAccountId.Open()

        Dim commandIsAccountId As MySqlCommand = New MySqlCommand
        commandIsAccountId.CommandType = CommandType.Text

        commandIsAccountId.CommandText = "Select LocationID, Location from tbllocation where LocationID ='" & txtLocationId.Text & "'"

        commandIsAccountId.Connection = connIsAccountId

        Dim drIsAccountId As MySqlDataReader = commandIsAccountId.ExecuteReader()
        Dim dtIsAccountId As New DataTable
        dtIsAccountId.Load(drIsAccountId)

        If dtIsAccountId.Rows.Count > 0 Then
            txtLocationName.Text = dtIsAccountId.Rows(0)("Location").ToString
        End If
        commandIsAccountId.Dispose()
        connIsAccountId.Close()
        dtIsAccountId.Dispose()

    End Sub

    Protected Sub ddlNewStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNewStatus.SelectedIndexChanged
        If Left(ddlNewStatus.Text, 1) = "R" Or Left(ddlNewStatus.Text, 1) = "T" Then
            txtDateLeftStatus.Enabled = True
        Else
            txtDateLeftStatus.Enabled = False
            txtDateLeftStatus.Text = ""

        End If
        mdlPopupStatus.Show()
    End Sub

    Protected Sub btnEncrypt_Click(sender As Object, e As EventArgs) Handles btnEncrypt.Click
        Dim conn As MySqlConnection = New MySqlConnection()
        Dim commandE1 As MySqlCommand = New MySqlCommand

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        commandE1.CommandType = CommandType.Text
        commandE1.CommandText = "select StaffID, SecMobileLoginId, SecWEBLoginID,SecMobilePassword, SecWEBPassword  from tblStaff where  Status = 'O'"
        commandE1.Connection = conn

        Dim drservice As MySqlDataReader = commandE1.ExecuteReader()
        Dim dtservice As New DataTable
        dtservice.Load(drservice)

        If dtservice.Rows.Count > 0 Then
            'ChangeStatus()


            For x As Integer = 0 To dtservice.Rows.Count - 1

                ' '''''''''''''''''''''''''
                'Dim commandIsBilled As MySqlCommand = New MySqlCommand

                'commandIsBilled.CommandType = CommandType.Text
                'commandIsBilled.CommandText = "select count(RecordNo) as isBilled from tblServiceRecord where RecordNo = '" & dtservice.Rows(x)("RecordNo").ToString & "' and ((BillNo ='') or (Billno is Null))"
                'commandIsBilled.Connection = conn

                'Dim drIsBilled As MySqlDataReader = commandIsBilled.ExecuteReader()
                'Dim dtIsBilled As New DataTable
                'dtIsBilled.Load(drIsBilled)

                'If dtIsBilled.Rows(0)("isBilled") = 0 Then
                '    CountBilledServiceRecords = CountBilledServiceRecords + 1
                '    commandIsBilled.Dispose()
                '    dtIsBilled.Dispose()
                '    GoTo nextrecE
                'End If

                'commandIsBilled.Dispose()
                'dtIsBilled.Dispose()
                ' '''''''''''''''''''''''''''

                Dim lMobileLoginId1, lSecWEBLoginID1 As String
                lMobileLoginId1 = ""
                lSecWEBLoginID1 = ""

                lMobileLoginId1 = dtservice.Rows(x)("SecMobileLoginId").ToString
                lSecWEBLoginID1 = dtservice.Rows(x)("SecWEBLoginID").ToString

                Dim lMobileLoginId, lSecWEBLoginID As String
                lMobileLoginId = ""
                lSecWEBLoginID = ""


                lMobileLoginId1 = lMobileLoginId1.ToLower
                lSecWEBLoginID1 = lSecWEBLoginID1.ToLower

                'Dim NameEncodein(dtservice.Rows(x)("SecMobileLoginId").ToString.Length - 1) As Byte
                'NameEncodein = System.Text.Encoding.UTF8.GetBytes(dtservice.Rows(x)("SecMobileLoginId").ToString)
                'Dim EcodedName As String = Convert.ToBase64String(NameEncodein)
                'lMobileLoginId = EcodedName

                'Dim NameEncodein2(dtservice.Rows(x)("SecWEBLoginID").ToString.Length - 1) As Byte
                'NameEncodein2 = System.Text.Encoding.UTF8.GetBytes(dtservice.Rows(x)("SecWEBLoginID").ToString)
                'Dim EcodedName2 As String = Convert.ToBase64String(NameEncodein2)
                'lSecWEBLoginID = EcodedName2

                Dim NameEncodein(lMobileLoginId1.ToString.Length - 1) As Byte
                NameEncodein = System.Text.Encoding.UTF8.GetBytes(lMobileLoginId1.ToString)
                Dim EcodedName As String = Convert.ToBase64String(NameEncodein)
                lMobileLoginId = EcodedName

                Dim NameEncodein2(lSecWEBLoginID1.ToString.Length - 1) As Byte
                NameEncodein2 = System.Text.Encoding.UTF8.GetBytes(lSecWEBLoginID1.ToString)
                Dim EcodedName2 As String = Convert.ToBase64String(NameEncodein2)
                lSecWEBLoginID = EcodedName2

                Dim commandE2 As MySqlCommand = New MySqlCommand
                commandE2.CommandType = CommandType.Text
                'commandE2.CommandText = "update  tblServiceRecord set Status='V' where (Status='O' or Status='' or Status is null)  and SchServiceDate>'" & Convert.ToDateTime(txtActualEndChSt.Text).ToString("yyyy-MM-dd") & "' and recordNo in (select recordNo from tblServiceRecordDet where ContractNo ='" & txtContractNo.Text & "')"
                commandE2.CommandText = "update  tblStaff set SecMobileLoginEncrypt='" & lMobileLoginId & "', SecWebLoginEncrypt='" & lSecWEBLoginID & "' where StaffID = '" & dtservice.Rows(x)("StaffID").ToString & "'"

                commandE2.Connection = conn
                commandE2.ExecuteNonQuery()
                commandE2.Dispose()
nextrecE:
            Next
        End If
    End Sub

    Protected Sub GridView1_Sorting(sender As Object, e As GridViewSortEventArgs) Handles GridView1.Sorting
        SqlDataSource1.SelectCommand = txt.Text
        SqlDataSource1.DataBind()
        GridView1.DataBind()
    End Sub

    Protected Sub txtGroupAuthority_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtGroupAuthority.SelectedIndexChanged
        If String.IsNullOrEmpty(txtEmail.Text) Then
            lblAlert.Text = "ENTER EMAIL ID IN STAFF INFORMATION TO PROCEED"
            Return
        End If
    End Sub

    Protected Sub txtWebLoginPassword_TextChanged(sender As Object, e As EventArgs) Handles txtWebLoginPassword.TextChanged
        If String.IsNullOrEmpty(txtEmail.Text) Then
            lblAlert.Text = "ENTER EMAIL ID IN STAFF INFORMATION TO PROCEED"
            Return
        End If
    End Sub
End Class


Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports EASendMail
Imports System.Drawing.Drawing2D
Imports System.Configuration
Imports System.Collections.Generic
Imports System.Drawing
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Data.Odbc
Imports AjaxControlToolkit
Imports NPOI.HSSF.UserModel
Imports NPOI.SS.UserModel
Imports NPOI.SS.Util
Imports NPOI.XSSF.UserModel

Partial Class AssetModule
    Inherits System.Web.UI.Page

    Public rcno As String
    Public Message As String = String.Empty


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


    Public Sub MakeMeNull()
        lblMsgMovement.Text = ""
        lblAlertMovement.Text = ""

        lblMessage.Text = ""
        lblAlert.Text = ""
        txtMode.Text = ""
        ddlBranch.SelectedIndex = 0
        ddlStatus.SelectedIndex = 0

        txtAssetNo.Text = ""
        txtSerialNo.Text = ""
        txtIMEI.Text = ""
        ddlGroup.SelectedIndex = 0
        ddlClass.SelectedIndex = 0
        ddlBrand.SelectedIndex = 0
        ddlModel.SelectedIndex = 0
        ddlColor.SelectedIndex = 0

        txtAssetRegNo.Text = ""
        txtDescription.Text = ""
        txtPurchaseDate.Text = ""
        ddlSupplier.SelectedIndex = 0
        txtPONo.Text = ""
        txtPurchasePrice.Text = ""

        txtWarranty.Text = ""
        ddlDurationType.SelectedIndex = 0
        txtWarrantyEnd.Text = ""
        txtNotes.Text = ""
        Image2.ImageUrl = ""
        Image2.Visible = False

        'txtAssetClass.Text = ""
        'txtAssetGroup.Text = ""

        'txtAssetCode.Text = ""
        'txtAssetMake.Text = ""
        'txtModel.Text = ""
        'txtMfgYear.Text = ""

        'txtCapacity.Text = ""
        'txtCapacityUnit.Text = ""
        'txtRegDate.Text = ""
        ''txtType.Text = ""

        'txtLastSvc.Text = ""
        'txtNextSvc.Text = ""
        'txtPriceCode.Text = ""
        'txtOurRef.Text = ""

        'txtEstDateIn.Text = ""
        'txtDateOut.Text = ""
        'txtRefDate.Text = ""
        'txtReference.Text = ""

        'txtGoogleEmail.Text = ""
        'txtGPSLabel.Text = ""
        ''
        'txtOPStatus.Text = ""


        'txtTechSpecs.Text = ""
        'txtRemarks.Text = ""

        'ddlInchargeID.SelectedIndex = 0
        'txtStatus.SelectedIndex = 0

        txtRcno.Text = ""
        'txtCreatedBy.Text = ""
        txtCreatedOn.Text = ""
        ' txtLastModifiedBy.Text = ""
        txtLastModifiedOn.Text = ""
        '  txtLocationId.SelectedIndex = 0

        btnStatus.Visible = False
        btnStatus.Text = ""

        '  btnMovement.Visible = False

    End Sub

    Private Sub EnableControls()
        btnSave.Enabled = False
        btnSave.ForeColor = System.Drawing.Color.Gray
        btnCancel.Enabled = False
        btnCancel.ForeColor = System.Drawing.Color.Gray

        btnAdd.Enabled = True
        btnAdd.ForeColor = System.Drawing.Color.Black

        btnEdit.Enabled = False
        btnEdit.ForeColor = System.Drawing.Color.Gray
        btnDelete.Enabled = False
        btnDelete.ForeColor = System.Drawing.Color.Gray

        btnQuit.Enabled = True
        btnQuit.ForeColor = System.Drawing.Color.Black
        btnPrint.Enabled = True
        btnPrint.ForeColor = System.Drawing.Color.Black
        btnFilter.Enabled = True
        btnFilter.ForeColor = System.Drawing.Color.Black

        btnStatus.Enabled = True
        btnStatus.ForeColor = System.Drawing.Color.Black
        'btnMovement.Enabled = True
        'btnMovement.ForeColor = System.Drawing.Color.Black

        ddlBranch.Enabled = False
        ddlStatus.Enabled = False

        txtAssetNo.Enabled = False
        txtSerialNo.Enabled = False
        txtIMEI.Enabled = False

        ddlGroup.Enabled = False
        ddlClass.Enabled = False
        ddlBrand.Enabled = False
        ddlModel.Enabled = False
        ddlColor.Enabled = False

        txtAssetRegNo.Enabled = False
        txtDescription.Enabled = False
        txtPurchaseDate.Enabled = False
        ddlSupplier.Enabled = False
        txtPONo.Enabled = False
        txtPurchasePrice.Enabled = False

        txtWarranty.Enabled = False
        ddlDurationType.Enabled = False
        txtWarrantyEnd.Enabled = False
        txtNotes.Enabled = False


        'txtAssetNo.Enabled = False
        'txtAssetRegNo.Enabled = False
        'txtAssetClass.Enabled = False
        'txtAssetGroup.Enabled = False

        'txtAssetCode.Enabled = False
        'txtAssetMake.Enabled = False
        'txtModel.Enabled = False
        'txtMfgYear.Enabled = False

        'txtCapacity.Enabled = False
        'txtCapacityUnit.Enabled = False
        'txtRegDate.Enabled = False
        'txtType.Enabled = False

        'txtLastSvc.Enabled = False
        'txtNextSvc.Enabled = False
        'txtPriceCode.Enabled = False
        'txtOurRef.Enabled = False

        'txtEstDateIn.Enabled = False
        'txtDateOut.Enabled = False
        'txtRefDate.Enabled = False
        'txtReference.Enabled = False

        'txtGoogleEmail.Enabled = False
        'txtGPSLabel.Enabled = False
        'txtStatus.Enabled = False
        'txtOPStatus.Enabled = False

        'txtDescription.Enabled = False
        'txtTechSpecs.Enabled = False
        'txtRemarks.Enabled = False

        'ddlInchargeID.Enabled = False
        'txtLocationId.Enabled = False
        AccessControl()
    End Sub

    Private Sub DisableControls()
        btnSave.Enabled = True
        btnSave.ForeColor = System.Drawing.Color.Black
        btnCancel.Enabled = True
        btnCancel.ForeColor = System.Drawing.Color.Black

        btnAdd.Enabled = False
        btnAdd.ForeColor = System.Drawing.Color.Gray

        btnEdit.Enabled = False
        btnEdit.ForeColor = System.Drawing.Color.Gray

        btnDelete.Enabled = False
        btnDelete.ForeColor = System.Drawing.Color.Gray

        btnQuit.Enabled = False
        btnQuit.ForeColor = System.Drawing.Color.Gray

        btnPrint.Enabled = False
        btnPrint.ForeColor = System.Drawing.Color.Gray

        btnFilter.Enabled = False
        btnFilter.ForeColor = System.Drawing.Color.Gray

        btnStatus.Enabled = False
        btnStatus.ForeColor = System.Drawing.Color.Gray
        btnMovement.Enabled = False
        btnMovement.ForeColor = System.Drawing.Color.Gray


        ddlBranch.Enabled = True
        '  ddlStatus.Enabled = True

        txtAssetNo.Enabled = True
        txtSerialNo.Enabled = True
        txtIMEI.Enabled = True

        ddlGroup.Enabled = True
        ddlClass.Enabled = True
        ddlBrand.Enabled = True
        ddlModel.Enabled = True
        ddlColor.Enabled = True

        txtAssetRegNo.Enabled = True
        txtDescription.Enabled = True
        txtPurchaseDate.Enabled = True
        ddlSupplier.Enabled = True
        txtPONo.Enabled = True
        txtPurchasePrice.Enabled = True

        txtWarranty.Enabled = True
        ddlDurationType.Enabled = True
        txtWarrantyEnd.Enabled = True
        txtNotes.Enabled = True

        'txtAssetNo.Enabled = True
        'txtAssetRegNo.Enabled = True
        'txtAssetClass.Enabled = True
        'txtAssetGroup.Enabled = True

        'txtAssetCode.Enabled = True
        'txtAssetMake.Enabled = True
        'txtModel.Enabled = True
        'txtMfgYear.Enabled = True

        'txtCapacity.Enabled = True
        'txtCapacityUnit.Enabled = True
        'txtRegDate.Enabled = True
        'txtType.Enabled = True

        'txtLastSvc.Enabled = True
        'txtNextSvc.Enabled = True
        'txtPriceCode.Enabled = True
        'txtOurRef.Enabled = True

        'txtEstDateIn.Enabled = True
        'txtDateOut.Enabled = True
        'txtRefDate.Enabled = True
        'txtReference.Enabled = True

        'txtGoogleEmail.Enabled = True
        'txtGPSLabel.Enabled = True
        'txtStatus.Enabled = True
        'txtOPStatus.Enabled = True

        'txtDescription.Enabled = True
        'txtTechSpecs.Enabled = True
        'txtRemarks.Enabled = True

        'txtRcno.Enabled = True
        'txtCreatedBy.Enabled = True
        'txtCreatedOn.Enabled = True
        'txtLastModifiedBy.Enabled = True
        'txtLastModifiedOn.Enabled = True

        'ddlInchargeID.Enabled = True
        'txtLocationId.Enabled = True
        AccessControl()
    End Sub

    Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        Try
            GridView1.PageIndex = e.NewPageIndex
            SqlDataSource1.SelectCommand = txt.Text
            SqlDataSource1.DataBind()
            GridView1.DataBind()
        Catch ex As Exception
            InsertIntoTblWebEventLog("GridView1_PageIndexChanging", ex.Message.ToString, txtCreatedBy.Text)
        End Try
    End Sub

   

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        Try
            If txtMode.Text = "Edit" Then
                lblAlert.Text = "CANNOT SELECT RECORD IN EDIT MODE. SAVE OR CANCEL TO PROCEED"
                Return
            End If
            'EnableControls()
            MakeMeNull()
            '    InsertIntoTblWebEventLog("Gridview1_SelectedIndexChanged", GridView1.SelectedIndex.ToString, "1")

            Dim editindex As Integer = GridView1.SelectedIndex
            rcno = DirectCast(GridView1.Rows(editindex).FindControl("Label1"), Label).Text
            txtRcno.Text = rcno.ToString()

            PopulateRecord(rcno.ToString)

            ReloadGridEmpty()

            txtMode.Text = "View"

            'txtMode.Text = "Edit"
            '  DisableControls()
            btnEdit.Enabled = True
            btnEdit.ForeColor = System.Drawing.Color.Black
            btnDelete.Enabled = True
            btnDelete.ForeColor = System.Drawing.Color.Black
            btnQuit.Enabled = True
            btnQuit.ForeColor = System.Drawing.Color.Black

            'EnableControls()
            tb1.ActiveTabIndex = 0

            If Session("SecGroupAuthority") <> "ADMINISTRATOR" Then
                AccessControl()
            End If
            '   InsertIntoTblWebEventLog("Gridview1_SelectedIndexChanged", GridView1.SelectedIndex.ToString, "6")
        Catch ex As Exception
            InsertIntoTblWebEventLog("Gridview1_SelectedIndexChanged", ex.Message.ToString, txtAssetNo.Text)
            lblMessage.Text = ex.Message.ToString

        End Try

    End Sub

    Private Sub PopulateRecord(rcno As String)
        txtRcno.Text = rcno
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()
        Dim command1 As MySqlCommand = New MySqlCommand

        command1.CommandType = CommandType.Text

        command1.CommandText = "SELECT * FROM tblasset where rcno=" & Convert.ToInt32(txtRcno.Text)
        command1.Connection = conn

        Dim dr As MySqlDataReader = command1.ExecuteReader()
        InsertIntoTblWebEventLog("Gridview1_SelectedIndexChanged", GridView1.SelectedIndex.ToString, rcno)
        '    Return

        ' dr.Read()
        Dim dt As New DataTable
        dt.Load(dr)
        If dt.Rows.Count > 0 Then

            If txtDisplayRecordsLocationwise.Text = "Y" Then
                ddlBranch.SelectedValue = dt.Rows(0)("Location").ToString
            End If
         
            If dt.Rows(0)("Status").ToString = "1" Or dt.Rows(0)("Status").ToString = "O" Then
                ddlStatus.SelectedIndex = 1
                'ddlStatus.SelectedValue = "1"

                btnStatus.Visible = True
                btnStatus.Text = "CHECKOUT"
            Else
                ddlStatus.SelectedIndex = 2
                ' ddlStatus.SelectedValue = "0"
                btnStatus.Visible = True
                btnStatus.Text = "CHECKIN"
            End If
            ddlStatus.SelectedItem.Text = dt.Rows(0)("StatusDesc")

            ' btnMovement.Visible = True
            InsertIntoTblWebEventLog("Gridview1_SelectedIndexChanged", GridView1.SelectedIndex.ToString, dt.Rows(0)("Status").ToString)

            txtAssetNo.Text = dt.Rows(0)("AssetNo").ToString
            txtSerialNo.Text = dt.Rows(0)("SerialNo").ToString
            txtIMEI.Text = dt.Rows(0)("IMEI").ToString

            If String.IsNullOrEmpty(dt.Rows(0)("AssetGrp").ToString) = False Then
                ddlGroup.Text = dt.Rows(0)("AssetGrp").ToString
            End If
         
          
            If String.IsNullOrEmpty(dt.Rows(0)("AssetClass").ToString) = False Then
                ddlClass.Text = dt.Rows(0)("AssetClass").ToString
            Else
                ddlClass.SelectedIndex = 0

            End If
        
            If String.IsNullOrEmpty(dt.Rows(0)("Make").ToString) = False Then
                ddlBrand.Text = dt.Rows(0)("Make").ToString
            End If
            ddlModel.Items.Clear()
            ddlModel.Items.Add("--SELECT--")
            ' ddlModel.SelectedIndex = "-1"

            PopulateDropDownList("SELECT AssetModel FROM tblassetModel WHERE ASSETBRAND='" & ddlBrand.Text & "' ORDER BY AssetModel", "AssetModel", "AssetModel", ddlModel)


            If String.IsNullOrEmpty(dt.Rows(0)("Model").ToString) = False Then
                '   lblAlert.Text = dt.Rows(0)("Model").ToString
                ddlModel.SelectedItem.Text = dt.Rows(0)("Model").ToString
            End If
            If String.IsNullOrEmpty(dt.Rows(0)("Color").ToString) = False Then
                ddlColor.Text = dt.Rows(0)("Color").ToString
            End If
            '     Return

            txtAssetRegNo.Text = dt.Rows(0)("AssetRegNo").ToString
            txtDescription.Text = dt.Rows(0)("Descrip").ToString
            If dt.Rows(0)("PurchDt").ToString = DBNull.Value.ToString Then
            Else
                txtPurchaseDate.Text = Convert.ToDateTime(dt.Rows(0)("PurchDt")).ToString("dd/MM/yyyy")
            End If
      
            If String.IsNullOrEmpty(dt.Rows(0)("SupplierName").ToString) = False And String.IsNullOrEmpty(dt.Rows(0)("SupplierCode").ToString) = False Then
                ddlSupplier.SelectedItem.Text = dt.Rows(0)("SupplierCode").ToString + " - " + dt.Rows(0)("SupplierName").ToString
            Else
                ddlSupplier.SelectedItem.Text = ""
            End If

            txtPONo.Text = dt.Rows(0)("PurchRef").ToString
            txtPurchasePrice.Text = dt.Rows(0)("PurchVal").ToString

            txtWarranty.Text = dt.Rows(0)("Warranty").ToString
            If String.IsNullOrEmpty(dt.Rows(0)("WarrantyMS").ToString) = False Then
                ddlDurationType.Text = dt.Rows(0)("WarrantyMS").ToString
            End If
            If dt.Rows(0)("WarrantyEnd").ToString = DBNull.Value.ToString Then
            Else
                txtWarrantyEnd.Text = Convert.ToDateTime(dt.Rows(0)("WarrantyEnd")).ToString("dd/MM/yyyy")
            End If

            txtNotes.Text = dt.Rows(0)("Notes").ToString
            '  InsertIntoTblWebEventLog("Gridview1_SelectedIndexChanged", GridView1.SelectedIndex.ToString, "4")

            Try
                Dim command2 As MySqlCommand = New MySqlCommand

                command2.CommandType = CommandType.Text


                command2.CommandText = "SELECT * FROM tblassetphotos where assetno='" + txtAssetNo.Text + "' and PrimaryPhoto=1"
                command2.Connection = conn

                Dim dr2 As MySqlDataReader = command2.ExecuteReader()
                Dim dt2 As New DataTable
                dt2.Load(dr2)

                If dt2.Rows.Count > 0 Then
                    Image2.Visible = True

                    If IsDBNull(dt2.Rows(0)("Photo")) = False Then
                        Dim bytes As Byte() = DirectCast(dt2.Rows(0)("Photo"), Byte())
                        Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
                        Image2.ImageUrl = "data:image/png;base64," + base64String
                    End If
                End If
                dr2.Close()
                dt2.Dispose()
                command2.Dispose()
                BindMovement()
                BindFileGrid("0")
                CountMovement(conn)
                iframeid.Attributes.Add("src", "about:blank")
                '    InsertIntoTblWebEventLog("Gridview1_SelectedIndexChanged", GridView1.SelectedIndex.ToString, "5")

            Catch ex As Exception
                InsertIntoTblWebEventLog("PopulateRecord", ex.Message.ToString, txtAssetNo.Text)
                lblMessage.Text = ex.Message.ToString

            End Try


        End If

        command1.Dispose()
        conn.Close()
        conn.Dispose()
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

    Protected Sub btnADD_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        MakeMeNull()
        lblAlert.Text = ""
        ddlSupplier.Items.Clear()

        'SqlDSSupplier.DataBind()

        'ddlSupplier.DataSourceID = "SqlDSSupplier"

        ddlSupplier.Items.Add("--SELECT--")
        ddlSupplier.DataBind()
        ddlSupplier.SelectedIndex = 0


        '   txtStatus.Text = "O"

        txtMode.Text = "New"
        DisableControls()
        lblMessage.Text = "ACTION: ADD RECORD"
        ddlStatus.SelectedValue = "1"
        ddlStatus.Focus()


    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        txtCreatedOn.Attributes.Add("readonly", "readonly")
        '  Page.Form.Enctype = "multipart/form-data"
        '  ReloadGrid()

        If Not IsPostBack Then
            '  MakeMeNull()
            EnableControls()
            Dim UserID As String = Convert.ToString(Session("UserID"))
            txtCreatedBy.Text = UserID
            txtLastModifiedBy.Text = UserID
            tb1.ActiveTabIndex = 0
            AccessControl()

            IsDisplayRecordsLocationwise()
            Dim query As String
            If txtDisplayRecordsLocationwise.Text = "Y" Then

                query = "Select LocationID from tbllocation"
                PopulateDropDownList(query, "LocationID", "LocationID", ddlBranch)
                'PopulateDropDownList("SELECT AssetGroup FROM tblassetGROUP ORDER BY AssetGroup", "AssetGroup", "AssetGroup", ddlGroup)
                'PopulateDropDownList("SELECT AssetClass FROM tblassetClass ORDER BY AssetClass", "AssetClass", "AssetClass", ddlClass)
                'PopulateDropDownList("SELECT AssetBrand FROM tblassetBrand ORDER BY AssetBrand", "AssetBrand", "AssetBrand", ddlBrand)
                'PopulateDropDownList("SELECT AssetModel FROM tblassetModel ORDER BY AssetModel", "AssetModel", "AssetModel", ddlModel)
                'PopulateDropDownList("SELECT AssetColor FROM tblassetColor ORDER BY AssetColor", "AssetColor", "AssetColor", ddlColor)

                '       PopulateDropDownList("SELECT AssetBrand FROM tblassetBrand ORDER BY AssetBrand", "AssetBrand", "AssetBrand", ddlBrand)

                '    PopulateDropDownList("SELECT Status,Available FROM tblassetstatus ORDER BY Status", "Status", "Available", ddlStatus)
            End If

            '  If Session("SecGroupAuthority") <> "ADMINISTRATOR" Then
            query = "SELECT AssetGroup FROM tblassetGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"
            'Else
            '    Query = "SELECT AssetGroup FROM tblassetGROUP ORDER BY AssetGroup"
            'End If

            PopulateDropDownList(query, "AssetGroup", "AssetGroup", ddlGroup)

            btnStatus.Visible = False
            '     btnMovement.Visible = False

            txt.Text = "SELECT * FROM TBLASSET WHERE RCNO<>0 "
            txt.Text = txt.Text + " and AssetGrp in (select AssetGroup from tblassetgroupaccess where GroupAccess = '" & Session("SecGroupAuthority") & "') ORDER BY RCNO desc"

            SqlDataSource1.SelectCommand = txt.Text
            SqlDataSource1.DataBind()
            GridView1.DataBind()

            If GridView1.Rows.Count > 0 Then
                txtRcno.Text = DirectCast(GridView1.Rows(0).FindControl("Label1"), Label).Text

                If String.IsNullOrEmpty(txtRcno.Text) = False Then

                    PopulateRecord(txtRcno.Text)
                End If

                GridView1.SelectedIndex = 0

            End If


        End If

        '   InsertIntoTblWebEventLog("Page_Load", GridView1.SelectedIndex.ToString, "0")

    End Sub

    Private Sub IsDisplayRecordsLocationwise()
        Try
            Dim IsLock As String
            IsLock = ""

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim commandServiceRecordMasterSetup As MySqlCommand = New MySqlCommand
            commandServiceRecordMasterSetup.CommandType = CommandType.Text
            commandServiceRecordMasterSetup.CommandText = "SELECT DisplayRecordsLocationWise FROM tblservicerecordmastersetup"
            commandServiceRecordMasterSetup.Connection = conn

            Dim drServiceRecordMasterSetup As MySqlDataReader = commandServiceRecordMasterSetup.ExecuteReader()
            Dim dtServiceRecordMasterSetup As New DataTable
            dtServiceRecordMasterSetup.Load(drServiceRecordMasterSetup)

            'txtLimit.Text = dtServiceRecordMasterSetup.Rows(0)("ServiceContractMaxRec")
            'txtDisplayRecordsLocationwise.Text = dtServiceRecordMasterSetup.Rows(0)("DisplayRecordsLocationWise").ToString


            If dtServiceRecordMasterSetup.Rows.Count > 0 Then
                txtDisplayRecordsLocationwise.Text = dtServiceRecordMasterSetup.Rows(0)("DisplayRecordsLocationWise").ToString
            End If

            If txtDisplayRecordsLocationwise.Text = "Y" Then
                BranchRow.Visible = True
            Else
                BranchRow.Visible = False
            End If
            conn.Close()
            conn.Dispose()
            commandServiceRecordMasterSetup.Dispose()
            dtServiceRecordMasterSetup.Dispose()

        Catch ex As Exception
            '  InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "IsDisplayRecordsLocationwise", ex.Message.ToString, "")
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

    Private Sub AccessControl()
        'Return

        Try
            '''''''''''''''''''Access Control 
            If Session("SecGroupAuthority") <> "ADMINISTRATOR" Then
                Dim conn As MySqlConnection = New MySqlConnection()
                Dim command As MySqlCommand = New MySqlCommand

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                command.CommandType = CommandType.Text
                command.CommandText = "SELECT x1301AssetView,  x1301AssetAdd, x1301AssetEdit, x1301AssetDelete,x1301AssetPrint,x1301AssetMovement,x1301AssetImport FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"
                command.Connection = conn

                Dim dr As MySqlDataReader = command.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)
                conn.Close()

                If dt.Rows.Count > 0 Then
                    If Not IsDBNull(dt.Rows(0)("x1301AssetView")) Then
                        If String.IsNullOrEmpty(Convert.ToBoolean(dt.Rows(0)("x1301AssetView"))) = False Then
                            If Convert.ToBoolean(dt.Rows(0)("x1301AssetView")) = False Then
                                Response.Redirect("Home.aspx")
                            End If
                        End If
                    End If

                    If Not IsDBNull(dt.Rows(0)("x1301AssetAdd")) Then
                        If String.IsNullOrEmpty(dt.Rows(0)("x1301AssetAdd")) = False Then
                            Me.btnAdd.Enabled = dt.Rows(0)("x1301AssetAdd").ToString()
                        End If
                    End If

                    If Not IsDBNull(dt.Rows(0)("x1301AssetPrint")) Then
                        If String.IsNullOrEmpty(dt.Rows(0)("x1301AssetPrint")) = False Then
                            Me.btnPrint.Enabled = dt.Rows(0)("x1301AssetPrint").ToString()
                        End If
                    End If

                    If txtMode.Text = "View" Then
                        If Not IsDBNull(dt.Rows(0)("x1301AssetEdit")) Then
                            If String.IsNullOrEmpty(dt.Rows(0)("x1301AssetEdit")) = False Then
                                Me.btnEdit.Enabled = dt.Rows(0)("x1301AssetEdit").ToString()
                            End If
                        End If

                        If Not IsDBNull(dt.Rows(0)("x1301AssetDelete")) Then
                            If String.IsNullOrEmpty(dt.Rows(0)("x1301AssetDelete")) = False Then
                                Me.btnDelete.Enabled = dt.Rows(0)("x1301AssetDelete").ToString()
                            End If
                        End If

                        If Not IsDBNull(dt.Rows(0)("x1301AssetMovement")) Then
                            If String.IsNullOrEmpty(dt.Rows(0)("x1301AssetMovement")) = False Then
                                Me.btnStatus.Enabled = dt.Rows(0)("x1301AssetMovement").ToString()
                            End If
                        End If
                        If Not IsDBNull(dt.Rows(0)("x1301AssetImport")) Then
                            If String.IsNullOrEmpty(dt.Rows(0)("x1301AssetImport")) = False Then
                                Me.btnImportFromExcel.Enabled = dt.Rows(0)("x1301AssetImport").ToString()
                            End If
                        End If
                    Else
                        Me.btnEdit.Enabled = False
                        Me.btnDelete.Enabled = False
                        Me.btnStatus.Enabled = False
                        Me.btnImportFromExcel.Enabled = False
                    End If

                    If btnAdd.Enabled = True Then
                        btnAdd.ForeColor = System.Drawing.Color.Black
                    Else
                        btnAdd.ForeColor = System.Drawing.Color.Gray
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

                    If btnImportFromExcel.Enabled = True Then
                        btnImportFromExcel.ForeColor = System.Drawing.Color.Black
                    Else
                        btnImportFromExcel.ForeColor = System.Drawing.Color.Gray
                    End If
                End If

                command.Dispose()
                conn.Close()
                conn.Dispose()
            End If

            '''''''''''''''''''Access Control 
        Catch ex As Exception
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
            lblAlert.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        '  lblAlert.Text = ddlStatus.SelectedItem.Value.ToString + " " + ddlStatus.SelectedValue.ToString
        '  Return

        If txtDisplayRecordsLocationwise.Text = "Y" Then
            If ddlBranch.Text = "-1" Then
                lblAlert.Text = "LOCATION CANNOT BE BLANK"
                Return
            End If
        End If
        'If txtAssetNo.Text = "" Then
        '    '  MessageBox.Message.Alert(Page, "Asset No cannot be blank!!!", "str")
        '    lblAlert.Text = "ASSET NO CANNOT BE BLANK"
        '    Return

        'End If
        If String.IsNullOrEmpty(txtWarranty.Text) = False Then

            Dim d As Double
            If Double.TryParse(txtWarranty.Text, d) = False Then
                '   MessageBox.Message.Alert(Page, "Allocated time is invalid!!! Enter time in mins!!", "str")
                lblAlert.Text = "WARRANTY SHOULD BE IN NUMBERS"
                Exit Sub
            End If

        End If
        If String.IsNullOrEmpty(ddlGroup.Text) = False Then
            If ddlGroup.Text = "-1" Then
                lblAlert.Text = "ASSET GROUP CANNOT BE BLANK"
                Return
            End If
        End If

        If String.IsNullOrEmpty(txtPurchaseDate.Text) Then
            lblAlert.Text = "PURCHASE DATE CANNOT BE BLANK"
            Return
        End If

        WarrantyCalculation()
        'If DateValidation() = False Then
        '    Return
        'End If
        If txtMode.Text = "New" Then
            ' Try
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.CommandText = "SELECT * FROM tblasset where assetno=@ind"
            command1.Parameters.AddWithValue("@ind", txtAssetNo.Text)
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then

                ''  MessageBox.Message.Alert(Page, "Record already exists!!!", "str")
                lblAlert.Text = "RECORD ALREADY EXISTS"
                txtAssetNo.Focus()
                Exit Sub
            Else

                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                Dim qry As String = "INSERT INTO tblasset(AssetNo,AssetGrp,AssetClass,AssetRegNo,Status,OpStatus,Descrip,DefAddr,Depmtd,PurfmCType,"
                qry = qry + "PurfmCCode,PurfmInvNo,PurchRef,PurchDt,PurchVal,ObbkVal,ObbkDate,CurrAddr,InCharge,CurrCont,Phone1,Phone2,EstDateIn,"
                qry = qry + "Notes,ActDateIn,LastMoveId,Project,DateOut,Purpose,AssetCode,Make,Model,Year,Capacity,EngineNo,Value,Remarks,PurfmCName,"
                qry = qry + "ReferenceOur,SvcFreq,NextSvcDate,LastSvcDate,SvcBy,DWM,SoldToCoID,SoldToCoName,SoldVal,SoldRef,SoldDate,DisposedRef,"
                qry = qry + "DisposedDate,AuthorBy,Desc1,AltNo,MfgYear,CustCode,CustName,Reference,RefDate,PriceCode,PROJECTCODE,PROJECTNAME,Cost,"
                qry = qry + "SupplierCode,SupplierName,LocCode,ValueDate,SoldBy,MarketValue,ScrapToCoID,ScrapToCoName,ScrapVal,ScrapRef,ScrapDate,"
                qry = qry + "ScrapBy,EngineBrand,EngineModel,ArNo,DueDate,TestNo,TestRemarks,CertType,RentalYN,InChargeID,SelfOwnYN,PurchBy,CostOther,"
                qry = qry + "IncomeOther,DeprDur,DeprMonthly,DeprEnd,EstLife,DeprOps,CostBillPct,PurchOVal,PurchExRate,PurchCurr,RoadTaxExpiry,CoeExpiry,"
                qry = qry + "InspectDate,VpcExpiry,VpcNo,PaymentType,MarketCost,CostDate,GroupID,GroupName,SaleableYN,FinCoID,FinCoName,FinDtFrom,"
                qry = qry + "FinDtTo,GltypeSales,GltypePurchase,LedgercodeSales,LedgercodePurchase,ContactType,TechnicalSpecs,type,CapacityUnitMS,"
                qry = qry + "RegDate,AgmtNo,LoanAmt,NoInst,IntRate,TermCharges,FirstInst,LastInst,MthlyInst,SubLedgercodeSales,SubLedgercodePurchase,"
                qry = qry + "GPSLabel,UploadDate,DelGoogleCalendar,GoogleEmail,GooglePassword,ColourCodeHtml,ColourCodeRGB, CreatedBy,CreatedOn,"
                qry = qry + "LastModifiedBy,LastModifiedOn,Location,SerialNo,Color,Warranty,WarrantyMS,WarrantyEnd,IMEI,StatusDesc)"
                qry = qry + "VALUES(@AssetNo,@AssetGrp,@AssetClass,@AssetRegNo,@Status,@OpStatus,@Descrip,@DefAddr,@Depmtd,@PurfmCType,@PurfmCCode,@PurfmInvNo,@PurchRef,@PurchDt,"
                qry = qry + "@PurchVal,@ObbkVal,@ObbkDate,@CurrAddr,@InCharge,@CurrCont,@Phone1,@Phone2,@EstDateIn,@Notes,@ActDateIn,@LastMoveId,@Project,@DateOut,@Purpose,"
                qry = qry + "@AssetCode,@Make,@Model,@Year,@Capacity,@EngineNo,@Value,@Remarks,@PurfmCName,@ReferenceOur,@SvcFreq,@NextSvcDate,@LastSvcDate,@SvcBy,@DWM,"
                qry = qry + "@SoldToCoID,@SoldToCoName,@SoldVal,@SoldRef,@SoldDate,@DisposedRef,@DisposedDate,@AuthorBy,@Desc1,@AltNo,@MfgYear,@CustCode,@CustName,@Reference,"
                qry = qry + "@RefDate,@PriceCode,@PROJECTCODE,@PROJECTNAME,@Cost,@SupplierCode,@SupplierName,@LocCode,@ValueDate,@SoldBy,@MarketValue,@ScrapToCoID,@ScrapToCoName,"
                qry = qry + "@ScrapVal,@ScrapRef,@ScrapDate,@ScrapBy,@EngineBrand,@EngineModel,@ArNo,@DueDate,@TestNo,@TestRemarks,@CertType,@RentalYN,@InChargeID,@SelfOwnYN,"
                qry = qry + "@PurchBy,@CostOther,@IncomeOther,@DeprDur,@DeprMonthly,@DeprEnd,@EstLife,@DeprOps,@CostBillPct,@PurchOVal,@PurchExRate,@PurchCurr,@RoadTaxExpiry,"
                qry = qry + "@CoeExpiry,@InspectDate,@VpcExpiry,@VpcNo,@PaymentType,@MarketCost,@CostDate,@GroupID,@GroupName,@SaleableYN,@FinCoID,@FinCoName,@FinDtFrom,@FinDtTo,"
                qry = qry + "@GltypeSales,@GltypePurchase,@LedgercodeSales,@LedgercodePurchase,@ContactType,@TechnicalSpecs,@type,@CapacityUnitMS,@RegDate,@AgmtNo,@LoanAmt,"
                qry = qry + "@NoInst,@IntRate,@TermCharges,@FirstInst,@LastInst,@MthlyInst,@SubLedgercodeSales,@SubLedgercodePurchase,@GPSLabel,@UploadDate,@DelGoogleCalendar,"
                qry = qry + "@GoogleEmail,@GooglePassword,@ColourCodeHtml,@ColourCodeRGB,@CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn,@Location,@SerialNo,@Color,@Warranty,@WarrantyMS,@WarrantyEnd,@IMEI,@StatusDesc);"


                command.CommandText = qry
                command.Parameters.Clear()

                If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then
                    If txtDisplayRecordsLocationwise.Text = "Y" Then
                        command.Parameters.AddWithValue("@Location", ddlBranch.Text.ToUpper)
                    Else
                        command.Parameters.AddWithValue("@Location", "")
                    End If

                    command.Parameters.AddWithValue("@Status", ddlStatus.SelectedItem.Value.ToString)
                    command.Parameters.AddWithValue("@StatusDesc", ddlStatus.SelectedItem.Text.ToString)
                    command.Parameters.AddWithValue("@OpStatus", "")

                    command.Parameters.AddWithValue("@SerialNo", txtSerialNo.Text.ToUpper)
                    command.Parameters.AddWithValue("@IMEI", txtIMEI.Text.ToUpper)

                    If ddlGroup.Text = "-1" Then
                        command.Parameters.AddWithValue("@AssetGrp", "")
                    Else
                        command.Parameters.AddWithValue("@AssetGrp", ddlGroup.Text.ToUpper)
                    End If
                    If ddlClass.Text = "-1" Then
                        command.Parameters.AddWithValue("@AssetClass", "")
                    Else
                        command.Parameters.AddWithValue("@AssetClass", ddlClass.Text.ToUpper)
                    End If
                    If ddlBrand.Text = "-1" Then
                        command.Parameters.AddWithValue("@Make", "")
                    Else
                        command.Parameters.AddWithValue("@Make", ddlBrand.Text.ToUpper)
                    End If

                    If ddlModel.Text = "-1" Then
                        command.Parameters.AddWithValue("@Model", "")
                    Else
                        command.Parameters.AddWithValue("@Model", ddlModel.Text.ToUpper)
                    End If
                    If ddlColor.Text = "-1" Then
                        command.Parameters.AddWithValue("@Color", "")
                    Else
                        command.Parameters.AddWithValue("@Color", ddlColor.Text.ToUpper)
                    End If
                    command.Parameters.AddWithValue("@AssetRegNo", txtAssetRegNo.Text.ToUpper)

                    command.Parameters.AddWithValue("@Descrip", txtDescription.Text.ToUpper)
                    If ddlSupplier.SelectedIndex = 0 Then
                        command.Parameters.AddWithValue("@SupplierCode", "")
                        command.Parameters.AddWithValue("@SupplierName", "")
                    Else
                        Dim suppliercode As String = ddlSupplier.Text.Substring(0, ddlSupplier.Text.IndexOf("-"))
                        Dim suppliername As String = ddlSupplier.Text.Substring(ddlSupplier.Text.IndexOf("-") + 1)

                        command.Parameters.AddWithValue("@SupplierCode", suppliercode)
                        command.Parameters.AddWithValue("@SupplierName", suppliername)
                    End If

                    If String.IsNullOrEmpty(txtPurchaseDate.Text) = True Then
                        command.Parameters.AddWithValue("@PurchDt", DBNull.Value)
                    Else
                        command.Parameters.AddWithValue("@PurchDt", Convert.ToDateTime(txtPurchaseDate.Text))
                    End If

                    command.Parameters.AddWithValue("@PurchRef", txtPONo.Text)
                    If String.IsNullOrEmpty(txtPurchasePrice.Text) = False Then
                        command.Parameters.AddWithValue("@PurchVal", Convert.ToDecimal(txtPurchasePrice.Text))
                    Else
                        command.Parameters.AddWithValue("@PurchVal", 0)
                    End If
                    If String.IsNullOrEmpty(txtWarranty.Text) = False Then
                        command.Parameters.AddWithValue("@Warranty", Convert.ToInt32(txtWarranty.Text))
                    Else
                        command.Parameters.AddWithValue("@Warranty", 0)
                    End If
                    If ddlDurationType.Text = "-1" Then
                        command.Parameters.AddWithValue("@WarrantyMS", "")
                    Else
                        command.Parameters.AddWithValue("@WarrantyMS", ddlDurationType.Text.ToUpper)
                    End If

                    If String.IsNullOrEmpty(txtWarrantyEnd.Text) = True Then
                        command.Parameters.AddWithValue("@WarrantyEnd", DBNull.Value)
                    Else
                        command.Parameters.AddWithValue("@WarrantyEnd", Convert.ToDateTime(txtWarrantyEnd.Text))
                    End If
                    command.Parameters.AddWithValue("@Notes", txtNotes.Text.ToUpper)

                ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then
                    If txtDisplayRecordsLocationwise.Text = "Y" Then
                        command.Parameters.AddWithValue("@Location", ddlBranch.Text)
                    Else
                        command.Parameters.AddWithValue("@Location", "")
                    End If
                    command.Parameters.AddWithValue("@Status", ddlStatus.SelectedItem.Value.ToString)
                    command.Parameters.AddWithValue("@StatusDesc", ddlStatus.SelectedItem.Text.ToString)
                    command.Parameters.AddWithValue("@OpStatus", "")

                    '    command.Parameters.AddWithValue("@AssetNo", txtAssetNo.Text)
                    command.Parameters.AddWithValue("@SerialNo", txtSerialNo.Text)
                    command.Parameters.AddWithValue("@IMEI", txtIMEI.Text)

                    If ddlGroup.Text = "-1" Then
                        command.Parameters.AddWithValue("@AssetGrp", "")
                    Else
                        command.Parameters.AddWithValue("@AssetGrp", ddlGroup.Text)
                    End If
                    If ddlGroup.Text = "-1" Then
                        command.Parameters.AddWithValue("@AssetClass", "")
                    Else
                        command.Parameters.AddWithValue("@AssetClass", ddlClass.Text)
                    End If
                    If ddlBrand.Text = "-1" Then
                        command.Parameters.AddWithValue("@Make", "")
                    Else
                        command.Parameters.AddWithValue("@Make", ddlBrand.Text)
                    End If

                    If ddlModel.Text = "-1" Then
                        command.Parameters.AddWithValue("@Model", "")
                    Else
                        command.Parameters.AddWithValue("@Model", ddlModel.Text)
                    End If
                    If ddlColor.Text = "-1" Then
                        command.Parameters.AddWithValue("@Color", "")
                    Else
                        command.Parameters.AddWithValue("@Color", ddlColor.Text)
                    End If
                    command.Parameters.AddWithValue("@AssetRegNo", txtAssetRegNo.Text)

                    command.Parameters.AddWithValue("@Descrip", txtDescription.Text)

                    If ddlSupplier.SelectedIndex = 0 Then
                        command.Parameters.AddWithValue("@SupplierCode", "")
                        command.Parameters.AddWithValue("@SupplierName", "")
                    Else
                        Dim suppliercode As String = ddlSupplier.SelectedItem.Text.Substring(0, ddlSupplier.SelectedItem.Text.IndexOf("-"))
                        Dim suppliername As String = ddlSupplier.SelectedItem.Text.Substring(ddlSupplier.SelectedItem.Text.IndexOf("-") + 1)

                        command.Parameters.AddWithValue("@SupplierCode", suppliercode)
                        command.Parameters.AddWithValue("@SupplierName", suppliername)
                    End If

                    If String.IsNullOrEmpty(txtPurchaseDate.Text) = True Then
                        command.Parameters.AddWithValue("@PurchDt", DBNull.Value)
                    Else
                        command.Parameters.AddWithValue("@PurchDt", Convert.ToDateTime(txtPurchaseDate.Text))
                    End If

                    command.Parameters.AddWithValue("@PurchRef", txtPONo.Text)
                    If String.IsNullOrEmpty(txtPurchasePrice.Text) = False Then
                        command.Parameters.AddWithValue("@PurchVal", Convert.ToDecimal(txtPurchasePrice.Text))
                    Else
                        command.Parameters.AddWithValue("@PurchVal", 0)
                    End If
                    If String.IsNullOrEmpty(txtWarranty.Text) = False Then
                        command.Parameters.AddWithValue("@Warranty", Convert.ToInt32(txtWarranty.Text))
                    Else
                        command.Parameters.AddWithValue("@Warranty", 0)
                    End If
                    If ddlDurationType.Text = "-1" Then
                        command.Parameters.AddWithValue("@WarrantyMS", "")
                    Else
                        command.Parameters.AddWithValue("@WarrantyMS", ddlDurationType.Text)
                    End If

                    If String.IsNullOrEmpty(txtWarrantyEnd.Text) = True Then
                        command.Parameters.AddWithValue("@WarrantyEnd", DBNull.Value)
                    Else
                        command.Parameters.AddWithValue("@WarrantyEnd", Convert.ToDateTime(txtWarrantyEnd.Text))
                    End If
                    command.Parameters.AddWithValue("@Notes", txtNotes.Text)

                End If

                command.Parameters.AddWithValue("@DefAddr", "")
                command.Parameters.AddWithValue("@Depmtd", "")
                command.Parameters.AddWithValue("@PurfmCType", "")
                command.Parameters.AddWithValue("@PurfmCCode", "")
                command.Parameters.AddWithValue("@PurfmInvNo", "")

                command.Parameters.AddWithValue("@ObbkVal", 0)
                command.Parameters.AddWithValue("@ObbkDate", DBNull.Value)
                command.Parameters.AddWithValue("@CurrAddr", "")
                command.Parameters.AddWithValue("@InCharge", "")
                command.Parameters.AddWithValue("@CurrCont", "")
                command.Parameters.AddWithValue("@Phone1", "")
                command.Parameters.AddWithValue("@Phone2", "")
                'If txtEstDateIn.Text = "" Then
                '    command.Parameters.AddWithValue("@EstDateIn", DBNull.Value)
                'Else
                '    command.Parameters.AddWithValue("@EstDateIn", Convert.ToDateTime(txtEstDateIn.Text))
                'End If
                command.Parameters.AddWithValue("@EstDateIn", DBNull.Value)

                command.Parameters.AddWithValue("@ActDateIn", DBNull.Value)
                command.Parameters.AddWithValue("@LastMoveId", "")
                command.Parameters.AddWithValue("@Project", "")
                'If txtDateOut.Text = "" Then
                '    command.Parameters.AddWithValue("@DateOut", DBNull.Value)
                'Else
                '    command.Parameters.AddWithValue("@DateOut", Convert.ToDateTime(txtDateOut.Text))
                'End If
                command.Parameters.AddWithValue("@DateOut", DBNull.Value)
                command.Parameters.AddWithValue("@Purpose", "")
                command.Parameters.AddWithValue("@AssetCode", "")
                command.Parameters.AddWithValue("@Year", "")
                command.Parameters.AddWithValue("@Capacity", "")
                command.Parameters.AddWithValue("@EngineNo", "")
                command.Parameters.AddWithValue("@Value", 0)
                command.Parameters.AddWithValue("@Remarks", "")
                command.Parameters.AddWithValue("@PurfmCName", "")
                command.Parameters.AddWithValue("@ReferenceOur", "")
                command.Parameters.AddWithValue("@SvcFreq", 0)
                'If txtNextSvc.Text = "" Then
                '    command.Parameters.AddWithValue("@NextSvcDate", DBNull.Value)
                'Else
                '    command.Parameters.AddWithValue("@NextSvcDate", Convert.ToDateTime(txtNextSvc.Text))
                'End If
                'If txtLastSvc.Text = "" Then
                '    command.Parameters.AddWithValue("@LastSvcDate", DBNull.Value)
                'Else
                '    command.Parameters.AddWithValue("@LastSvcDate", Convert.ToDateTime(txtLastSvc.Text))
                'End If
                command.Parameters.AddWithValue("@NextSvcDate", DBNull.Value)
                command.Parameters.AddWithValue("@LastSvcDate", DBNull.Value)
                command.Parameters.AddWithValue("@SvcBy", "")
                command.Parameters.AddWithValue("@DWM", "")
                command.Parameters.AddWithValue("@SoldToCoID", "")
                command.Parameters.AddWithValue("@SoldToCoName", "")
                command.Parameters.AddWithValue("@SoldVal", 0)
                command.Parameters.AddWithValue("@SoldRef", "")
                command.Parameters.AddWithValue("@SoldDate", DBNull.Value)
                command.Parameters.AddWithValue("@DisposedRef", "")
                command.Parameters.AddWithValue("@DisposedDate", DBNull.Value)
                command.Parameters.AddWithValue("@AuthorBy", "")
                command.Parameters.AddWithValue("@Desc1", "")
                command.Parameters.AddWithValue("@AltNo", "")
                command.Parameters.AddWithValue("@MfgYear", "")
                command.Parameters.AddWithValue("@CustCode", "")
                command.Parameters.AddWithValue("@CustName", "")
                command.Parameters.AddWithValue("@Reference", "")
                'If txtRefDate.Text = "" Then
                '    command.Parameters.AddWithValue("@RefDate", DBNull.Value)
                'Else
                '    command.Parameters.AddWithValue("@RefDate", Convert.ToDateTime(txtRefDate.Text))
                'End If
                command.Parameters.AddWithValue("@RefDate", DBNull.Value)
                command.Parameters.AddWithValue("@PriceCode", "")
                command.Parameters.AddWithValue("@PROJECTCODE", "")
                command.Parameters.AddWithValue("@PROJECTNAME", "")
                command.Parameters.AddWithValue("@Cost", 0)

                command.Parameters.AddWithValue("@LocCode", "")
                command.Parameters.AddWithValue("@ValueDate", DBNull.Value)
                command.Parameters.AddWithValue("@SoldBy", "")
                command.Parameters.AddWithValue("@MarketValue", 0)
                command.Parameters.AddWithValue("@ScrapToCoID", "")
                command.Parameters.AddWithValue("@ScrapToCoName", "")
                command.Parameters.AddWithValue("@ScrapVal", 0)
                command.Parameters.AddWithValue("@ScrapRef", "")
                command.Parameters.AddWithValue("@ScrapDate", DBNull.Value)
                command.Parameters.AddWithValue("@ScrapBy", "")
                command.Parameters.AddWithValue("@EngineBrand", "")
                command.Parameters.AddWithValue("@EngineModel", "")
                command.Parameters.AddWithValue("@ArNo", "")
                command.Parameters.AddWithValue("@DueDate", DBNull.Value)
                command.Parameters.AddWithValue("@TestNo", "")
                command.Parameters.AddWithValue("@TestRemarks", "")
                command.Parameters.AddWithValue("@CertType", "")
                command.Parameters.AddWithValue("@RentalYN", "")
                'If ddlInchargeID.Text = "-1" Then
                '    command.Parameters.AddWithValue("@InChargeID", "")
                'Else
                '    command.Parameters.AddWithValue("@InChargeID", ddlInchargeID.Text.ToUpper)
                'End If
                command.Parameters.AddWithValue("@InChargeID", "")
                command.Parameters.AddWithValue("@SelfOwnYN", "")
                command.Parameters.AddWithValue("@PurchBy", "")
                command.Parameters.AddWithValue("@CostOther", 0)
                command.Parameters.AddWithValue("@IncomeOther", 0)
                command.Parameters.AddWithValue("@DeprDur", 0)
                command.Parameters.AddWithValue("@DeprMonthly", 0)
                command.Parameters.AddWithValue("@DeprEnd", DBNull.Value)
                command.Parameters.AddWithValue("@EstLife", 0)
                command.Parameters.AddWithValue("@DeprOps", 0)
                command.Parameters.AddWithValue("@CostBillPct", 0)
                command.Parameters.AddWithValue("@PurchOVal", 0)
                command.Parameters.AddWithValue("@PurchExRate", 0)
                command.Parameters.AddWithValue("@PurchCurr", "")
                command.Parameters.AddWithValue("@RoadTaxExpiry", DBNull.Value)
                command.Parameters.AddWithValue("@CoeExpiry", DBNull.Value)
                command.Parameters.AddWithValue("@InspectDate", DBNull.Value)
                command.Parameters.AddWithValue("@VpcExpiry", DBNull.Value)
                command.Parameters.AddWithValue("@VpcNo", "")
                command.Parameters.AddWithValue("@PaymentType", "")
                command.Parameters.AddWithValue("@MarketCost", 0)
                command.Parameters.AddWithValue("@CostDate", DBNull.Value)
                command.Parameters.AddWithValue("@GroupID", "")
                command.Parameters.AddWithValue("@GroupName", "")
                command.Parameters.AddWithValue("@SaleableYN", "")
                command.Parameters.AddWithValue("@FinCoID", "")
                command.Parameters.AddWithValue("@FinCoName", "")
                command.Parameters.AddWithValue("@FinDtFrom", DBNull.Value)
                command.Parameters.AddWithValue("@FinDtTo", DBNull.Value)
                command.Parameters.AddWithValue("@GltypeSales", "")
                command.Parameters.AddWithValue("@GltypePurchase", "")
                command.Parameters.AddWithValue("@LedgercodeSales", "")
                command.Parameters.AddWithValue("@LedgercodePurchase", "")
                command.Parameters.AddWithValue("@ContactType", "")
                command.Parameters.AddWithValue("@TechnicalSpecs", "")
                command.Parameters.AddWithValue("@type", "")
                command.Parameters.AddWithValue("@CapacityUnitMS", "")
                command.Parameters.AddWithValue("@RegDate", DBNull.Value)
                'If txtRegDate.Text = "" Then
                '    command.Parameters.AddWithValue("@RegDate", DBNull.Value)
                'Else
                '    command.Parameters.AddWithValue("@RegDate", Convert.ToDateTime(txtRegDate.Text))
                'End If
                command.Parameters.AddWithValue("@AgmtNo", "")
                command.Parameters.AddWithValue("@LoanAmt", 0)
                command.Parameters.AddWithValue("@NoInst", 0)
                command.Parameters.AddWithValue("@IntRate", 0)
                command.Parameters.AddWithValue("@TermCharges", 0)
                command.Parameters.AddWithValue("@FirstInst", 0)
                command.Parameters.AddWithValue("@LastInst", 0)
                command.Parameters.AddWithValue("@MthlyInst", 0)
                command.Parameters.AddWithValue("@SubLedgercodeSales", "")
                command.Parameters.AddWithValue("@SubLedgercodePurchase", "")
                command.Parameters.AddWithValue("@GPSLabel", "")
                command.Parameters.AddWithValue("@UploadDate", DBNull.Value)
                command.Parameters.AddWithValue("@DelGoogleCalendar", 0)
                command.Parameters.AddWithValue("@GoogleEmail", "")
                command.Parameters.AddWithValue("@GooglePassword", "")
                command.Parameters.AddWithValue("@ColourCodeHtml", "")
                command.Parameters.AddWithValue("@ColourCodeRGB", "")


                command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                GenerateAssetNo()
                command.Parameters.AddWithValue("@AssetNo", txtAssetNo.Text.ToUpper)
                If txtAssetNo.Text = "" Then
                    '  MessageBox.Message.Alert(Page, "Asset No cannot be blank!!!", "str")
                    lblAlert.Text = "ASSET NO CANNOT BE BLANK"
                    Return

                End If

                command.Connection = conn

                command.ExecuteNonQuery()

                txtRcno.Text = command.LastInsertedId

                '  MessageBox.Message.Alert(Page, "Record added successfully!!!", "str")
                lblMessage.Text = "ADD: RECORD SUCCESSFULLY ADDED"
                lblAlert.Text = ""
            End If
            conn.Close()
            If txtMode.Text = "New" Then
                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "ASSET", txtAssetNo.Text, "ADD", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
            Else
                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "ASSET", txtAssetNo.Text, "EDIT", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
            End If
            'Catch ex As Exception
            '    MessageBox.Message.Alert(Page, ex.ToString, "str")
            '    InsertIntoTblWebEventLog("btnSave_Click", ex.Message.ToString, "New " + txtAssetNo.Text)
            'End Try
            EnableControls()
            btnStatus.Visible = True
            btnStatus.Text = "CHECKOUT"
            '     btnMovement.Visible = True

            txtMode.Text = ""
            SqlDataSource1.SelectCommand = "select * from tblasset where rcno<>0 ORDER BY rcno DESC;"
            SqlDataSource1.DataBind()
            GridView1.DataBind()
            GridView1.SelectedIndex = 0

        ElseIf txtMode.Text = "Edit" Then
            If txtRcno.Text = "" Then
                ' MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
                lblAlert.Text = "SELECT RECORD TO EDIT"
                Return

            End If
            '   Try
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()


            Dim command2 As MySqlCommand = New MySqlCommand

            command2.CommandType = CommandType.Text

            command2.CommandText = "SELECT * FROM tblasset where assetno=@ind and rcno<>" & Convert.ToInt32(txtRcno.Text)
            command2.Parameters.AddWithValue("@ind", txtAssetNo.Text)
            command2.Connection = conn

            Dim dr1 As MySqlDataReader = command2.ExecuteReader()
            Dim dt1 As New DataTable
            dt1.Load(dr1)

            If dt1.Rows.Count > 0 Then

                ' MessageBox.Message.Alert(Page, "AssetNo already exists!!!", "str")
                lblAlert.Text = "ASSET NO ALREADY EXISTS"
                txtAssetNo.Focus()
                Exit Sub
            Else

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                command1.CommandText = "SELECT * FROM tblasset where rcno=" & Convert.ToInt32(txtRcno.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "UPDATE tblasset SET Location=@Location,AssetNo = @AssetNo,SerialNo=@SerialNo,AssetGrp = @AssetGrp,AssetClass = @AssetClass,Make = @Make,Model = @Model,Color=@Color,"
                    qry = qry + "AssetRegNo = @AssetRegNo,Descrip = @Descrip,PurchDt=@PurchDt,SupplierCode=@SupplierCode,SupplierName=@SupplierName,PurchVal=@PurchVal,"
                    qry = qry + "PurchRef=@PurchRef,Warranty=@Warranty,WarrantyMS=@WarrantyMS,WarrantyEnd=@WarrantyEnd,Notes=@Notes,IMEI=@IMEI,LastModifiedBy = @LastModifiedBy,LastModifiedOn = @LastModifiedOn WHERE Rcno = " & Convert.ToInt32(txtRcno.Text)

                    command.CommandText = qry
                    command.Parameters.Clear()

                    If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then
                        If txtDisplayRecordsLocationwise.Text = "Y" Then
                            command.Parameters.AddWithValue("@Location", ddlBranch.Text.ToUpper)
                        Else
                            command.Parameters.AddWithValue("@Location", "")
                        End If
                        'command.Parameters.AddWithValue("@Status", txtStatus.Text.ToUpper)
                        'command.Parameters.AddWithValue("@OpStatus", "1")

                        command.Parameters.AddWithValue("@AssetNo", txtAssetNo.Text.ToUpper)
                        command.Parameters.AddWithValue("@SerialNo", txtSerialNo.Text.ToUpper)
                        command.Parameters.AddWithValue("@IMEI", txtIMEI.Text.ToUpper)

                        If ddlGroup.Text = "-1" Then
                            command.Parameters.AddWithValue("@AssetGrp", "")
                        Else
                            command.Parameters.AddWithValue("@AssetGrp", ddlGroup.Text.ToUpper)
                        End If
                        If ddlGroup.Text = "-1" Then
                            command.Parameters.AddWithValue("@AssetClass", "")
                        Else
                            command.Parameters.AddWithValue("@AssetClass", ddlClass.Text.ToUpper)
                        End If
                        If ddlBrand.Text = "-1" Then
                            command.Parameters.AddWithValue("@Make", "")
                        Else
                            command.Parameters.AddWithValue("@Make", ddlBrand.Text.ToUpper)
                        End If

                        If ddlModel.Text = "-1" Then
                            command.Parameters.AddWithValue("@Model", "")
                        Else
                            command.Parameters.AddWithValue("@Model", ddlModel.Text.ToUpper)
                        End If
                        If ddlColor.Text = "-1" Then
                            command.Parameters.AddWithValue("@Color", "")
                        Else
                            command.Parameters.AddWithValue("@Color", ddlColor.Text.ToUpper)
                        End If
                        command.Parameters.AddWithValue("@AssetRegNo", txtAssetRegNo.Text.ToUpper)

                        command.Parameters.AddWithValue("@Descrip", txtDescription.Text.ToUpper)
                        '  If ddlSupplier.SelectedIndex = 0 Then
                        If ddlSupplier.SelectedItem.Text = "" Then
                            command.Parameters.AddWithValue("@SupplierCode", "")
                            command.Parameters.AddWithValue("@SupplierName", "")
                        Else
                            Dim suppliercode As String = ddlSupplier.SelectedItem.Text.Substring(0, ddlSupplier.SelectedItem.Text.IndexOf("-"))
                            Dim suppliername As String = ddlSupplier.SelectedItem.Text.Substring(ddlSupplier.SelectedItem.Text.IndexOf("-") + 1)

                            command.Parameters.AddWithValue("@SupplierCode", suppliercode)
                            command.Parameters.AddWithValue("@SupplierName", suppliername)
                        End If

                        If String.IsNullOrEmpty(txtPurchaseDate.Text) = True Then
                            command.Parameters.AddWithValue("@PurchDt", DBNull.Value)
                        Else
                            command.Parameters.AddWithValue("@PurchDt", Convert.ToDateTime(txtPurchaseDate.Text))
                        End If

                        command.Parameters.AddWithValue("@PurchRef", txtPONo.Text)
                        If String.IsNullOrEmpty(txtPurchasePrice.Text) = False Then
                            command.Parameters.AddWithValue("@PurchVal", Convert.ToDecimal(txtPurchasePrice.Text))
                        Else
                            command.Parameters.AddWithValue("@PurchVal", 0)
                        End If
                        If String.IsNullOrEmpty(txtWarranty.Text) = False Then
                            command.Parameters.AddWithValue("@Warranty", Convert.ToInt32(txtWarranty.Text))
                        Else
                            command.Parameters.AddWithValue("@Warranty", 0)
                        End If
                        If ddlDurationType.Text = "-1" Then
                            command.Parameters.AddWithValue("@WarrantyMS", "")
                        Else
                            command.Parameters.AddWithValue("@WarrantyMS", ddlDurationType.Text.ToUpper)
                        End If
                        If String.IsNullOrEmpty(txtWarrantyEnd.Text) = True Then
                            command.Parameters.AddWithValue("@WarrantyEnd", DBNull.Value)
                        Else
                            command.Parameters.AddWithValue("@WarrantyEnd", Convert.ToDateTime(txtWarrantyEnd.Text))
                        End If
                        command.Parameters.AddWithValue("@Notes", txtNotes.Text.ToUpper)

                    ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then
                        If txtDisplayRecordsLocationwise.Text = "Y" Then
                            command.Parameters.AddWithValue("@Location", ddlBranch.Text)
                        Else
                            command.Parameters.AddWithValue("@Location", "")
                        End If
                        'command.Parameters.AddWithValue("@Status", txtStatus.Text)
                        'command.Parameters.AddWithValue("@OpStatus", "1")

                        command.Parameters.AddWithValue("@AssetNo", txtAssetNo.Text)
                        command.Parameters.AddWithValue("@SerialNo", txtSerialNo.Text)
                        command.Parameters.AddWithValue("@IMEI", txtIMEI.Text)

                        If ddlGroup.Text = "-1" Then
                            command.Parameters.AddWithValue("@AssetGrp", "")
                        Else
                            command.Parameters.AddWithValue("@AssetGrp", ddlGroup.Text)
                        End If
                        If ddlGroup.Text = "-1" Then
                            command.Parameters.AddWithValue("@AssetClass", "")
                        Else
                            command.Parameters.AddWithValue("@AssetClass", ddlClass.Text)
                        End If
                        If ddlBrand.Text = "-1" Then
                            command.Parameters.AddWithValue("@Make", "")
                        Else
                            command.Parameters.AddWithValue("@Make", ddlBrand.Text)
                        End If

                        If ddlModel.Text = "-1" Then
                            command.Parameters.AddWithValue("@Model", "")
                        Else
                            command.Parameters.AddWithValue("@Model", ddlModel.Text)
                        End If
                        If ddlColor.Text = "-1" Then
                            command.Parameters.AddWithValue("@Color", "")
                        Else
                            command.Parameters.AddWithValue("@Color", ddlColor.Text)
                        End If
                        command.Parameters.AddWithValue("@AssetRegNo", txtAssetRegNo.Text)

                        command.Parameters.AddWithValue("@Descrip", txtDescription.Text)
                        If ddlSupplier.SelectedIndex = 0 Then
                            command.Parameters.AddWithValue("@SupplierCode", "")
                            command.Parameters.AddWithValue("@SupplierName", "")
                        Else
                            Dim suppliercode As String = ddlSupplier.Text.Substring(0, ddlSupplier.Text.IndexOf("-"))
                            Dim suppliername As String = ddlSupplier.Text.Substring(ddlSupplier.Text.IndexOf("-") + 1)

                            command.Parameters.AddWithValue("@SupplierCode", suppliercode)
                            command.Parameters.AddWithValue("@SupplierName", suppliername)
                        End If

                        If String.IsNullOrEmpty(txtPurchaseDate.Text) = True Then
                            command.Parameters.AddWithValue("@PurchDt", DBNull.Value)
                        Else
                            command.Parameters.AddWithValue("@PurchDt", Convert.ToDateTime(txtPurchaseDate.Text))
                        End If

                        command.Parameters.AddWithValue("@PurchRef", txtPONo.Text)
                        If String.IsNullOrEmpty(txtPurchasePrice.Text) = False Then
                            command.Parameters.AddWithValue("@PurchVal", Convert.ToDecimal(txtPurchasePrice.Text))
                        Else
                            command.Parameters.AddWithValue("@PurchVal", 0)
                        End If
                        If String.IsNullOrEmpty(txtWarranty.Text) = False Then
                            command.Parameters.AddWithValue("@Warranty", Convert.ToInt32(txtWarranty.Text))
                        Else
                            command.Parameters.AddWithValue("@Warranty", 0)
                        End If
                        If ddlDurationType.Text = "-1" Then
                            command.Parameters.AddWithValue("@WarrantyMS", "")
                        Else
                            command.Parameters.AddWithValue("@WarrantyMS", ddlDurationType.Text)
                        End If
                        If String.IsNullOrEmpty(txtWarrantyEnd.Text) = True Then
                            command.Parameters.AddWithValue("@WarrantyEnd", DBNull.Value)
                        Else
                            command.Parameters.AddWithValue("@WarrantyEnd", Convert.ToDateTime(txtWarrantyEnd.Text))
                        End If
                        command.Parameters.AddWithValue("@Notes", txtNotes.Text)

                    End If
                    command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                    command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                    command.Connection = conn

                    command.ExecuteNonQuery()



                    '  MessageBox.Message.Alert(Page, "Record updated successfully!!!", "str")
                    lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED"
                    lblAlert.Text = ""

                End If


            End If
            conn.Close()

            'Catch ex As Exception
            '    MessageBox.Message.Alert(Page, ex.ToString, "str")
            '    InsertIntoTblWebEventLog("btnSave_Click", ex.Message.ToString, "Edit " + txtAssetNo.Text)
            'End Try
            EnableControls()
            txtMode.Text = ""
            SqlDataSource1.SelectCommand = txt.Text

            SqlDataSource1.DataBind()
            GridView1.DataBind()
        End If




            ' GridView1.DataSourceID = "SqlDataSource1"
            ' MakeMeNull()
            'ddlInchargeID.SelectedIndex = 0
            'txtStatus.SelectedIndex = 0


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
    End Sub



    Protected Sub btnQuit_Click(sender As Object, e As EventArgs) Handles btnQuit.Click
        'MessageBox.Message.Alert(Page, "hi", "str")
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

                command1.CommandText = "SELECT * FROM tblasset where rcno=" & Convert.ToInt32(txtRcno.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "delete from tblasset where rcno=" & Convert.ToInt32(txtRcno.Text)

                    command.CommandText = qry

                    command.Connection = conn

                    command.ExecuteNonQuery()

                    '  MessageBox.Message.Alert(Page, "Record deleted successfully!!!", "str")
                    lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "ASSET", txtAssetNo.Text, "DELETE", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
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

    Protected Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Response.Redirect("RV_MasterVehicle.aspx")
    End Sub

    Private Function DateValidation() As Boolean
        Dim d As Date
        If String.IsNullOrEmpty(txtPurchaseDate.Text) = False Then
            If Date.TryParseExact(txtPurchaseDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then
                txtRegDate.Text = d.ToShortDateString

            Else
                '  MessageBox.Message.Alert(Page, "Reg Date is invalid", "str")
                lblAlert.Text = "PURCHASE DATE IS INVALID"
                Return False
                Exit Function
            End If
        End If
        If String.IsNullOrEmpty(txtWarrantyEnd.Text) = False Then
            If Date.TryParseExact(txtWarrantyEnd.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then
                txtRefDate.Text = d.ToShortDateString

            Else
                ' MessageBox.Message.Alert(Page, "Ref Date is invalid", "str")
                lblAlert.Text = "WARRANTY DATE IS INVALID"
                Return False
                Exit Function
            End If
        End If
      

        Return True
    End Function

    Protected Sub btnFilter_Click(sender As Object, e As EventArgs) Handles btnFilter.Click
        ' pup.ShowPopupWindow()
        txtSearchAssetNo.Focus()
        txtSearchAssetNo.Text = ""
        '  txtSearchAssetRegNo.Text = ""
        txtSearchSerialNo.Text = ""
        ddlSearchStatus.SelectedIndex = 0
        txtSearchNotes.Text = ""
        txtSearchDescription.Text = ""
        txtSearchIMEI.Text = ""
        txtSearchPONO.Text = ""
        txtSearchCurrentUser.Text = ""
        ModalPopupExtender1.Show()

    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            Dim qry As String
            qry = "select * from tblasset where 1=1 "
            If String.IsNullOrEmpty(txtSearchAssetNo.Text) = False Then
                txtAssetNo.Text = txtSearchAssetNo.Text

                qry = qry + " and assetno like '%" + txtSearchAssetNo.Text + "%'"
            End If
            If String.IsNullOrEmpty(txtSearchSerialNo.Text) = False Then
                txtSerialNo.Text = txtSearchSerialNo.Text
                qry = qry + " and serialno like '%" + txtSearchSerialNo.Text + "%'"
            End If
            If String.IsNullOrEmpty(txtSearchIMEI.Text) = False Then
                txtIMEI.Text = txtSearchIMEI.Text
                qry = qry + " and IMEI like '%" + txtSearchIMEI.Text + "%'"
            End If
            If String.IsNullOrEmpty(txtSearchPONO.Text) = False Then
                txtPONo.Text = txtSearchPONO.Text
                qry = qry + " and PurchaseRef like '%" + txtSearchPONO.Text + "%'"
            End If
            If String.IsNullOrEmpty(txtSearchCurrentUser.Text) = False Then
                '  txtAssetRegNo.Text = txtSearchCurrentUser.Text
                qry = qry + " and finconame like '%" + txtSearchCurrentUser.Text + "%'"
            End If
            If String.IsNullOrEmpty(txtSearchDescription.Text) = False Then
                txtDescription.Text = txtSearchDescription.Text
                qry = qry + " and Descrip like '%" + txtSearchDescription.Text + "%'"
            End If
            If String.IsNullOrEmpty(txtSearchNotes.Text) = False Then
                txtNotes.Text = txtSearchNotes.Text
                qry = qry + " and Notes like '%" + txtSearchNotes.Text + "%'"
            End If
            If ddlSearchStatus.Text <> "-1" Then
                ' txtStatus.Text = ddlSearchStatus.Text
                qry = qry + " and status = '" + ddlSearchStatus.SelectedValue.ToString + "'"
            End If
      
            qry = qry + " and AssetGrp in (select AssetGroup from tblassetgroupaccess where GroupAccess = '" & Session("SecGroupAuthority") & "')"

            qry = qry + " order by rcno desc;"
            InsertIntoTblWebEventLog("Search", qry, "")
            txt.Text = qry
            SqlDataSource1.SelectCommand = qry
            SqlDataSource1.DataBind()
            GridView1.DataBind()
            txtSearchAssetNo.Text = ""
            'txtSearchAssetRegNo.Text = ""
            ddlSearchStatus.SelectedIndex = 0
            txtSearchSerialNo.Text = ""
            'UpdatePanel1.Update()
        Catch ex As Exception
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
            lblAlert.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub MyCloseWindow()

    End Sub

    Protected Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click


    End Sub

    Protected Sub btnResetSearch_Click(sender As Object, e As ImageClickEventArgs) Handles btnResetSearch.Click
        MakeMeNull()
        EnableControls()
        'ddlInchargeID.SelectedIndex = 0
        'txtStatus.SelectedIndex = 0
        txt.Text = "select * from tblasset where rcno<>0"
        txt.Text = txt.Text + " and AssetGrp in (select AssetGroup from tblassetgroupaccess where GroupAccess = '" & Session("SecGroupAuthority") & "') ORDER BY RCNO desc"

        SqlDataSource1.SelectCommand = txt.Text
        SqlDataSource1.DataBind()
        GridView1.DataBind()
    End Sub



    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            MakeMeNull()
            EnableControls()
        Catch ex As Exception
            MessageBox.Message.Alert(Page, "Error!!!", "str")
        End Try
    End Sub

    Public Sub InsertIntoTblWebEventLog(events As String, errorMsg As String, ID As String)
        Try
            Dim conn As New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

            Dim insCmds As New MySqlCommand()
            insCmds.CommandType = CommandType.Text

            Dim insQuery As String = "Insert into tblwebEventLog(LoginId, Event, Error,ID, CreatedOn)"
            insQuery += " values(@LoginId,@Event,@Error,@ID,@CreatedOn);"

            insCmds.CommandText = insQuery

            insCmds.Parameters.Clear()
            insCmds.Parameters.AddWithValue("@LoginId", "AssetModule" + " - " + txtCreatedBy.Text)
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

        Catch ex As Exception
            InsertIntoTblWebEventLog("InsertIntoTblWebEventLog", ex.Message.ToString, txtAssetNo.Text)
            Exit Sub
        End Try
    End Sub

    Public Sub InsertIntoTbwFileUpload(filename As String, path1 As String)
        Try
            filename = filename.Replace(".PDF", ".pdf")
            path1 = path1.Replace(".PDF", ".pdf")

            Dim conn As New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()


            Dim command As MySqlCommand = New MySqlCommand

            command.CommandType = CommandType.Text
            Dim qry As String = "INSERT INTO tbwassetfileupload(FileGroup,FileRef,FileName,FileDescription,FileType,FileNameLink, ManualReport,CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn,DocumentType)"
            qry = qry + "VALUES(@FileGroup,@FileRef,@FileName,@FileDescription,@FileType,@FileNameLink,@ManualReport,@CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn,@DocumentType);"


            command.CommandText = qry
            command.Parameters.Clear()


            command.Parameters.AddWithValue("@FileGroup", "ASSET")
            command.Parameters.AddWithValue("@FileRef", Session("AssetNo"))
            command.Parameters.AddWithValue("@FileName", filename)
            command.Parameters.AddWithValue("@FileDescription", "")
            command.Parameters.AddWithValue("@FileType", Path.GetExtension(filename))

            command.Parameters.AddWithValue("@ManualReport", "N")

            command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
            command.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))

            command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
            command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
            command.Parameters.AddWithValue("@FileNameLink", path1)
            command.Parameters.AddWithValue("@DocumentType", Session("StatusType"))



            command.Connection = conn

            command.ExecuteNonQuery()
            conn.Close()
            conn.Dispose()
            command.Dispose()

        Catch ex As Exception
            InsertIntoTblWebEventLog("InsertIntoTbwAssetFileUpload", ex.Message.ToString, Session("AssetNo"))
            Exit Sub
        End Try
    End Sub

    Public Sub DeleteTbwFileUpload()
        Try
            Dim conn As New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()


            Dim command As MySqlCommand = New MySqlCommand

            command.CommandType = CommandType.Text
            Dim qry As String = "delete from tbwassetfileupload where createdby='" + txtCreatedBy.Text + "'" 

            command.CommandText = qry
           
            command.Connection = conn

            command.ExecuteNonQuery()
            conn.Close()
            conn.Dispose()
            command.Dispose()

        Catch ex As Exception
            InsertIntoTblWebEventLog("DeleteTbwFileUpload", ex.Message.ToString, Session("AssetNo"))
            Exit Sub
        End Try
    End Sub
    Private Sub WarrantyCalculation()
        txtWarrantyEnd.Text = ""
        If String.IsNullOrEmpty(txtPurchaseDate.Text) = False And String.IsNullOrEmpty(txtWarranty.Text) = False Then
            If ddlDurationType.Text.ToUpper = "YEARS" Then
                txtWarrantyEnd.Text = Convert.ToDateTime(txtPurchaseDate.Text).AddYears(Convert.ToInt16(txtWarranty.Text)).AddDays(-1).ToString("dd/MM/yyyy")
            ElseIf ddlDurationType.Text.ToUpper = "MONTHS" Then
                txtWarrantyEnd.Text = Convert.ToDateTime(txtPurchaseDate.Text).AddMonths(Convert.ToInt16(txtWarranty.Text)).AddDays(-1).ToString("dd/MM/yyyy")
            ElseIf ddlDurationType.Text.ToUpper = "WEEKS" Then
                txtWarrantyEnd.Text = Convert.ToDateTime(txtPurchaseDate.Text).AddDays(Convert.ToInt16(txtWarranty.Text) * 7).AddDays(-1).ToString("dd/MM/yyyy")

            End If
        End If
    End Sub

    Protected Sub ddlView_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlView.SelectedIndexChanged
        '  GridView1.PageSize = Convert.ToInt16(ddlView.SelectedItem.Text)
        Try
            GridView1.PageSize = Convert.ToInt16(ddlView.SelectedItem.Text)
            SqlDataSource1.SelectCommand = txt.Text
            SqlDataSource1.DataBind()
            GridView1.DataBind()
        Catch ex As Exception
            InsertIntoTblWebEventLog("ddlView_SelectedIndexChanged", ex.Message.ToString, txtCreatedBy.Text)
        End Try
    End Sub


    Protected Sub txtSearchAsset_TextChanged(sender As Object, e As EventArgs) Handles txtSearchAsset.TextChanged
        txtSearchText.Text = txtSearchAsset.Text

        Dim qry As String
        qry = "select * from tblasset where 1=1 "
        If String.IsNullOrEmpty(txtSearchAsset.Text) = False Then
         
            qry = qry + " and (assetno like '%" + txtSearchAsset.Text + "%'"
            qry = qry + " or serialno like '%" + txtSearchAsset.Text + "%'"
            qry = qry + " or IMEI like '%" + txtSearchAsset.Text + "%'"
            qry = qry + " or PurchRef like '%" + txtSearchAsset.Text + "%'"
            qry = qry + " or Descrip like '%" + txtSearchAsset.Text + "%'"
            qry = qry + " or Notes like '%" + txtSearchAsset.Text + "%'"
            qry = qry + " or status = '" + txtSearchAsset.Text + "'"
            qry = qry + " or AssetGrp like '%" + txtSearchAsset.Text + "%'"
            qry = qry + " or AssetClass like '%" + txtSearchAsset.Text + "%'"
            qry = qry + " or Make like '%" + txtSearchAsset.Text + "%'"
            qry = qry + " or Model like '%" + txtSearchAsset.Text + "%'"
            qry = qry + " or Color like '%" + txtSearchAsset.Text + "%')"
        End If

        qry = qry + " and AssetGrp in (select AssetGroup from tblassetgroupaccess where GroupAccess = '" & Session("SecGroupAuthority") & "')"

        qry = qry + " order by rcno desc;"

       
        txt.Text = qry
        MakeMeNull()
        SqlDataSource1.SelectCommand = qry
        SqlDataSource1.DataBind()
        GridView1.DataBind()
        '  GridView1.SelectedIndex = 0

        If GridView1.Rows.Count > 0 Then
            txtRcno.Text = DirectCast(GridView1.Rows(0).FindControl("Label1"), Label).Text

            If String.IsNullOrEmpty(txtRcno.Text) = False Then

                PopulateRecord(txtRcno.Text)
            End If

            GridView1.SelectedIndex = 0

        End If

        lblMessage.Text = "SEARCH CRITERIA : " + txtSearchAsset.Text + " <br/>NUMBER OF RECORDS FOUND : " + GridView1.Rows.Count.ToString

        '   txtSearchAsset.Text = "Search Here"
    End Sub

    Protected Sub btnGoCust_Click(sender As Object, e As EventArgs) Handles btnGoCust.Click
        txtSearchText.Text = txtSearchAsset.Text
        If txtSearchAsset.Text <> "Search Here" Then

            Dim qry As String
            qry = "select * from tblasset where 1=1 "
            If String.IsNullOrEmpty(txtSearchAsset.Text) = False Then

                qry = qry + " and assetno like '%" + txtSearchAsset.Text + "%'"

            End If
            qry = qry + " and AssetGrp in (select AssetGroup from tblassetgroupaccess where GroupAccess = '" & Session("SecGroupAuthority") & "')"


            qry = qry + " order by rcno desc;"


            txt.Text = qry
            MakeMeNull()
            SqlDataSource1.SelectCommand = qry
            SqlDataSource1.DataBind()
            GridView1.DataBind()

            If GridView1.Rows.Count > 0 Then
                txtRcno.Text = DirectCast(GridView1.Rows(0).FindControl("Label1"), Label).Text

                If String.IsNullOrEmpty(txtRcno.Text) = False Then

                    PopulateRecord(txtRcno.Text)
                End If

                GridView1.SelectedIndex = 0

            End If

            lblMessage.Text = "SEARCH CRITERIA : " + txtSearchAsset.Text + " <br/>NUMBER OF RECORDS FOUND : " + GridView1.Rows.Count.ToString
        Else
            txtSearchAsset.Text = "Search Here"
        End If

    End Sub

    Protected Sub btnStatus_Click(sender As Object, e As EventArgs) Handles btnStatus.Click
        If String.IsNullOrEmpty(txtAssetNo.Text) Then
            lblAlert.Text = "ASSET NO CANNOT BE EMPTY"
            Exit Sub

        End If
        lblLastAssetDetails.Text = ""

        Session.Add("AssetNo", txtAssetNo.Text)
        Session.Add("CheckInFileExists", "False")
        'Session.Remove("ChkInFileName")
        Session.Add("CheckOutFileExists", "False")
        'Session.Remove("ChkOutFileName")
        Session.Remove("CheckOutPath")
        '    Session.Add("UploadState", "start")
        ReloadGridEmpty()
        DeleteTbwFileUpload()

        Dim temppath As String = Server.MapPath("~\Uploads\Asset\temp\") + Session("AssetNo")
          If Directory.Exists(temppath) Then
            Directory.Delete(temppath, True)
        End If

        'lblFileName1.Text = ""
        'txtFileDesc1.Text = ""
        'lblFileName2.Text = ""
        'txtFileDesc2.Text = ""
        'lblFileName3.Text = ""
        'txtFileDesc3.Text = ""
        'lblFileName4.Text = ""
        'txtFileDesc4.Text = ""
        'lblFileName5.Text = ""
        'txtFileDesc5.Text = ""
        'lblFileName1.Visible = False
        'txtFileDesc1.Visible = False
        'lblFileName2.Visible = False
        'txtFileDesc2.Visible = False
        'lblFileName3.Visible = False
        'txtFileDesc3.Visible = False
        'lblFileName4.Visible = False
        'txtFileDesc4.Visible = False
        'lblFileName5.Visible = False
        'txtFileDesc5.Visible = False

        'lblRow1.Visible = False
        'lblRow2.Visible = False
        'lblRow3.Visible = False
        'lblRow4.Visible = False
        'lblRow5.Visible = False

        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()
        Dim command3 As MySqlCommand = New MySqlCommand

        command3.CommandType = CommandType.Text


        command3.CommandText = "SELECT Incharge,MovementDate FROM tblassetmovement where assetno='" + txtAssetNo.Text + "' order by rcno desc"
        command3.Connection = conn

        Dim dr3 As MySqlDataReader = command3.ExecuteReader()
        Dim dt3 As New DataTable
        dt3.Load(dr3)

        If dt3.Rows.Count > 0 Then
            If btnStatus.Text = "CHECKOUT" Then
                lblLastAssetDetails.Text = "Last CheckedIn By : " + dt3.Rows(0)("Incharge") + "<br>Last CheckedIn Date : " + Convert.ToDateTime(dt3.Rows(0)("MovementDate")).ToString("dd/MM/yyyy")
            ElseIf btnStatus.Text = "CHECKIN" Then
                lblLastAssetDetails.Text = "Last CheckedOut By : " + dt3.Rows(0)("Incharge") + "<br>Last CheckedOut Date : " + Convert.ToDateTime(dt3.Rows(0)("MovementDate")).ToString("dd/MM/yyyy")

            End If

        End If
            dr3.Close()
            dt3.Dispose()
            command3.Dispose()
            conn.Close()
            conn.Dispose()

            If btnStatus.Text = "CHECKOUT" Then
                Session.Add("StatusType", "CHECKOUT")
                lblStatusHeading.Text = "ASSET CHECK-OUT"

                btnSaveCheckOut.Enabled = True
                btnSaveCheckOut.Text = "Checkout"
                lblAlertCheckout.Text = ""
                lblMessageCheckout.Text = ""

                txtAssetCheckOut.Text = ""
                txtDescCheckOut.Text = ""
                ddlCheckOutLocation.Text = "-1"
                ddlCheckOutStaff.Text = "-1"
                ddlCheckOutType.Text = "-1"
                txtCheckOutDate.Text = ""
                txtExpCheckInDate.Text = ""
                txtCheckOutNotes.Text = ""
                ddlCheckInType.Text = "-1"

                txtAssetCheckOut.Text = txtAssetNo.Text
                txtDescCheckOut.Text = txtDescription.Text
                lblCheckType.Text = "CheckOutType"
                lblCheckTo.Text = "CheckOutTo"
                lblCheckDate.Text = "CheckOutDate"
                expchkin.Visible = True

                ddlCheckOutType.Visible = True
                ddlCheckInType.Visible = False


                ddlCheckOutLocation.Visible = False
                ddlCheckOutStaff.Visible = True

                If txtDisplayRecordsLocationwise.Text = "Y" Then
                    ddlCheckOutType.Enabled = True
                    CheckOutLocationRow.Visible = True
                Else
                    ddlCheckOutType.SelectedValue = "STAFF"
                    ddlCheckOutType.Enabled = False
                    CheckOutLocationRow.Visible = False
                End If

                mdlPopupCheckOut.Show()
            ElseIf btnStatus.Text = "CHECKIN" Then
                Session.Add("StatusType", "CHECKIN")
                lblStatusHeading.Text = "ASSET CHECK-IN"

                btnSaveCheckOut.Enabled = True
                btnSaveCheckOut.Text = "Checkin"
                lblAlertCheckout.Text = ""
                lblMessageCheckout.Text = ""

                txtAssetCheckOut.Text = ""
                txtDescCheckOut.Text = ""
                ddlCheckOutLocation.Text = "-1"
                ddlCheckOutStaff.Text = "-1"
                ddlCheckOutType.Text = "-1"
                txtCheckOutDate.Text = ""
                txtExpCheckInDate.Text = ""
                txtCheckOutNotes.Text = ""
                ddlCheckInType.Text = "-1"

                txtAssetCheckOut.Text = txtAssetNo.Text
                txtDescCheckOut.Text = txtDescription.Text
                lblCheckType.Text = "CheckOutType"
                lblCheckTo.Text = "CheckOutTo"
                lblCheckDate.Text = "CheckInDate"
                expchkin.Visible = False

                ddlCheckOutType.Visible = False
                ddlCheckInType.Visible = True

                ddlCheckOutLocation.Visible = False
                ddlCheckOutStaff.Visible = True

                If txtDisplayRecordsLocationwise.Text = "Y" Then
                    ddlCheckInType.Enabled = True
                    CheckOutLocationRow.Visible = True
                Else
                    ddlCheckInType.SelectedValue = "STAFF"
                    ddlCheckInType.Enabled = False
                    CheckOutLocationRow.Visible = False
                End If

                mdlPopupCheckOut.Show()

            End If
    End Sub

    Protected Sub ddlCheckOutType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCheckOutType.SelectedIndexChanged
        If ddlCheckOutType.SelectedValue = "STAFF" Then
            'ddlCheckOutStaff.Visible = True
            'ddlCheckOutLocation.Visible = False
            CheckOutLocationRow.Visible = False

        ElseIf ddlCheckOutType.SelectedValue = "LOCATION" Then
            'ddlCheckOutStaff.Visible = False
            'ddlCheckOutLocation.Visible = True
            CheckOutLocationRow.Visible = True

        End If
        mdlPopupCheckOut.Show()

    End Sub

    Protected Sub btnSaveCheckOut_Click(sender As Object, e As System.EventArgs) Handles btnSaveCheckOut.Click
       
        Dim temppath As String = Server.MapPath("~\Uploads\Asset\temp\") + Session("AssetNo")
        If Directory.Exists(temppath) Then

        End If


        If Session("StatusType") = "CHECKOUT" Then
            If ddlCheckOutType.Text = "-1" Then
                lblAlertCheckout.Text = "SELECT CHECKOUT TYPE"
                Exit Sub

                mdlPopupCheckOut.Show()
                mdlPopupConfirm.Hide()
            End If

            If String.IsNullOrEmpty(txtCheckOutDate.Text) Then
                txtCheckOutDate.Text = DateTime.Now.ToString("dd/MM/yyyy")
            End If
            If txtDisplayRecordsLocationwise.Text = "Y" Then
                If ddlCheckOutLocation.Text = "-1" Then
                    lblAlertCheckout.Text = "SELECT CHECKOUT TO LOCATION"
                    mdlPopupCheckOut.Show()
                    Exit Sub
                    mdlPopupConfirm.Hide()
                End If
                If ddlCheckOutStaff.Text = "-1" Then
                    lblAlertCheckout.Text = "SELECT CHECKOUT STAFF"
                    mdlPopupCheckOut.Show()
                    Exit Sub
                    mdlPopupConfirm.Hide()
                End If

            Else
                If ddlCheckOutStaff.Text = "-1" Then
                    lblAlertCheckout.Text = "SELECT CHECKOUT STAFF"
                    mdlPopupCheckOut.Show()
                    Exit Sub

                    mdlPopupConfirm.Hide()
                End If
            End If
            '   InsertIntoTblWebEventLog("CHECKOUT", "", "")

            'If Session("CheckOutFileExists") <> "True" Then

            '    Session.Add("ChkOutFileName", "")
            'End If


            'InsertIntoTblWebEventLog("UploadFile3", chkoutFileUpload.HasFile.ToString, "")


            lblStatusMessage.Text = "Do you confirm to checkout the following? <br><br> Item : " + txtDescCheckOut.Text + "<br><br> Checkout To : " + ddlCheckOutStaff.Text

            mdlPopupConfirm.Show()
            '  mdlPopupCheckOut.Hide()

        ElseIf Session("StatusType") = "CHECKIN" Then
            If ddlCheckInType.Text = "-1" Then
                lblAlertCheckout.Text = "SELECT CHECKIN TYPE"
                mdlPopupCheckOut.Show()
                Exit Sub
            End If
            If String.IsNullOrEmpty(txtCheckOutDate.Text) Then
                txtCheckOutDate.Text = DateTime.Now.ToString("dd/MM/yyyy")
            End If
            If txtDisplayRecordsLocationwise.Text = "Y" Then
                If ddlCheckOutLocation.Text = "-1" Then
                    lblAlertCheckout.Text = "SELECT CHECKIN TO LOCATION"
                    mdlPopupCheckOut.Show()
                    Exit Sub
                End If
                If ddlCheckOutStaff.Text = "-1" Then
                    lblAlertCheckout.Text = "SELECT CHECKIN STAFF"
                    mdlPopupCheckOut.Show()
                    Exit Sub
                End If
            Else
                If ddlCheckOutStaff.Text = "-1" Then
                    lblAlertCheckout.Text = "SELECT CHECKIN STAFF"
                    mdlPopupCheckOut.Show()
                    Exit Sub
                End If
            End If

            'If Session("CheckInFileExists") <> "True" Then

            '    Session.Add("ChkInFileName", "")
            'End If

            lblStatusMessage.Text = "Do you confirm to checkin the following? <br><br> Item : " + txtDescCheckOut.Text + "<br><br> CheckIn To : " + ddlCheckOutStaff.Text

            mdlPopupConfirm.Show()
            mdlPopupCheckOut.Hide()
        End If



    End Sub

 
    Protected Function ValidateEmail(ByVal EmailId As String) As String
        Dim resEmail As String = ""
        If EmailId.Contains(","c) Then EmailId = EmailId.Replace(","c, ";"c)
        If EmailId.Contains("/"c) Then EmailId = EmailId.Replace("/"c, ";"c)
        If EmailId.Contains(":"c) Then EmailId = EmailId.Replace(":"c, ";"c)
        resEmail = EmailId.TrimEnd(";"c)
        Return resEmail
    End Function


    Private Sub SendEmailNotification(conn As MySqlConnection, ToEmail As String, subject As String, StaffName As String, content As String, rcno As String)
        'Return

        Dim oMail As New SmtpMail(ConfigurationManager.AppSettings("EmailLicense").ToString())
        Dim oSmtp As New SmtpClient()

        ToEmail = ValidateEmail(ToEmail)

        If ToEmail.Last.ToString = ";" Then
            ToEmail = ToEmail.Remove(ToEmail.Length - 1)
        End If

        If ToEmail.First.ToString = ";" Then
            ToEmail = ToEmail.Remove(0)
        End If

        Dim pattern As String
        pattern = "^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"

        If String.IsNullOrEmpty(ToEmail) = False Then
            Dim ToAddress As String() = ToEmail.Split(";"c)
            If ToAddress.Count() > 0 Then
                For i As Integer = 0 To ToAddress.Count() - 1
                    If Regex.IsMatch(ToAddress(i).ToString.Trim, pattern) Then

                    Else
                        MessageBox.Message.Alert(Page, "Enter valid 'TO' Email address" + " (" + ToAddress(i).ToString() + ")", "str")
                        Return
                    End If
                    oMail.[To].Add(New MailAddress(ToAddress(i).ToString.Trim))
                Next
            End If
        End If


        ''''Retrieve Staff Details''
        Dim commandStaff As MySqlCommand = New MySqlCommand
        commandStaff.CommandType = CommandType.Text
        commandStaff.CommandText = "SELECT AssetEmailNotification FROM tblservicerecordmastersetup where rcno=1"
         commandStaff.Connection = conn

        Dim drStaff As MySqlDataReader = commandStaff.ExecuteReader()
        Dim dtStaff As New DataTable
        dtStaff.Load(drStaff)

        If dtStaff.Rows.Count > 0 Then

            Dim CCEmail As String = dtStaff.Rows(0)("AssetEmailNotification").ToString

            If String.IsNullOrEmpty(CCEmail) = False Then
                Dim ToAddress As String() = CCEmail.Split(";"c)
                If ToAddress.Count() > 0 Then
                    For i As Integer = 0 To ToAddress.Count() - 1
                        If Regex.IsMatch(ToAddress(i).ToString.Trim, pattern) Then

                        Else
                            MessageBox.Message.Alert(Page, "Enter valid 'CC' Email address" + " (" + ToAddress(i).ToString() + ")", "str")
                            Return
                        End If
                        oMail.[Cc].Add(New MailAddress(ToAddress(i).ToString.Trim))
                    Next
                End If
            End If
        End If

        '    oMail.Cc = "Christian.Reyes@anticimex.com.sg"
        oMail.From = ConfigurationManager.AppSettings("EmailFrom").ToString()
        oMail.Subject = subject
        oMail.HtmlBody = content

        Dim command3 As MySqlCommand = New MySqlCommand

        command3.CommandType = CommandType.Text


        command3.CommandText = "select * from tblfileupload where filegroup='ASSET' AND filenamelink like '" + rcno + "\_%'"
        command3.Connection = conn

        Dim dr3 As MySqlDataReader = command3.ExecuteReader()
        Dim dt3 As New DataTable
        dt3.Load(dr3)

        If dt3.Rows.Count > 0 Then
            For i As Int16 = 0 To dt3.Rows.Count - 1
                oMail.AddAttachment(Server.MapPath("~/Uploads/Asset/") + dt3.Rows(i)("filenamelink").ToString)
            Next
        End If

        command3.Dispose()
        dt3.Dispose()
        dr3.Close()

        Dim oServer As New SmtpServer(ConfigurationManager.AppSettings("EmailSMTP").ToString())
        oServer.Port = ConfigurationManager.AppSettings("EmailPort").ToString()
        oServer.ConnectType = SmtpConnectType.ConnectDirectSSL
        oServer.User = ConfigurationManager.AppSettings("EmailFrom").ToString()
        oServer.Password = ConfigurationManager.AppSettings("EmailPassword").ToString()

        oSmtp.SendMail(oServer, oMail)
        oSmtp.Close()

    End Sub

    Private Sub GridSave(conn As MySqlConnection)
        Dim desc As String
        Dim rcno As String

        'Dim conn As MySqlConnection = New MySqlConnection()

        'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        'conn.Open()

        For Each row As GridViewRow In gvStatusFileList.Rows
            desc = DirectCast(row.FindControl("txtgrdFileDesc"), TextBox).Text
            rcno = DirectCast(row.FindControl("Label1"), Label).Text


            Dim command As MySqlCommand = New MySqlCommand

            command.CommandType = CommandType.Text
            command.Connection = conn

            Dim qry As String = "update tbwassetfileupload set filedescription=@filedescription where rcno='" + rcno + "'"

            command.CommandText = qry
            command.Parameters.Clear()

            command.Parameters.AddWithValue("@filedescription", desc)

            command.ExecuteNonQuery()

            command.Dispose()

        Next
        'conn.Close()
        'conn.Dispose()

    End Sub
    Private Sub CheckOutSave()
        Try
            Dim rcno As String = ""
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()
            GridSave(conn)

            Dim command As MySqlCommand = New MySqlCommand

            command.CommandType = CommandType.Text
            command.Connection = conn

            Dim qry As String = "INSERT INTO tblassetmovement(AssetNo,MovementType,Location,RecipientType,Incharge,MovementDate,Notes,ReturnDate,CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn)"
            qry = qry + "VALUES(@AssetNo,@MovementType,@Location,@RecipientType,@Incharge,@MovementDate,@Notes,@ReturnDate,@CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn);"

            command.CommandText = qry
            command.Parameters.Clear()

            command.Parameters.AddWithValue("@AssetNo", txtAssetCheckOut.Text)
            command.Parameters.AddWithValue("@MovementType", "CHECKOUT")
            If ddlCheckOutLocation.Text = "-1" Then
                command.Parameters.AddWithValue("@Location", "")
            Else

                command.Parameters.AddWithValue("@Location", ddlCheckOutLocation.Text)

            End If
            command.Parameters.AddWithValue("@RecipientType", ddlCheckOutType.Text)
            command.Parameters.AddWithValue("@Incharge", ddlCheckOutStaff.Text)
            If String.IsNullOrEmpty(txtCheckOutDate.Text) Then
                command.Parameters.AddWithValue("@MovementDate", DateTime.Now.ToString("dd/MM/yyyy"))
            Else
                command.Parameters.AddWithValue("@MovementDate", Convert.ToDateTime(txtCheckOutDate.Text))
            End If
            If String.IsNullOrEmpty(txtExpCheckInDate.Text) Then
                command.Parameters.AddWithValue("@ReturnDate", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@ReturnDate", Convert.ToDateTime(txtExpCheckInDate.Text))
            End If
            command.Parameters.AddWithValue("@Notes", txtCheckOutNotes.Text)
            command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
            command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
            command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
            command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
            command.ExecuteNonQuery()
            rcno = command.LastInsertedId

            command.Dispose()

            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.CommandText = "update tblasset set Status=0,StatusDesc='UNAVAILABLE',FinCoName=@finconame where assetno=@assetno"

            command1.Parameters.Clear()
            command1.Parameters.AddWithValue("@assetno", txtAssetNo.Text)
            command1.Parameters.AddWithValue("@finconame", ddlCheckOutStaff.Text)

            command1.Connection = conn

            command1.ExecuteNonQuery()

            UploadFile(rcno)

            ''''Retrieve Staff Details''
            Dim commandStaff As MySqlCommand = New MySqlCommand
            commandStaff.CommandType = CommandType.Text
            commandStaff.CommandText = "SELECT StaffID, Name, SecGroupAuthority,EmailPerson FROM tblstaff where StaffID = @userid;"
            commandStaff.Parameters.AddWithValue("@userid", ddlCheckOutStaff.Text)
            commandStaff.Connection = conn

            Dim drStaff As MySqlDataReader = commandStaff.ExecuteReader()
            Dim dtStaff As New DataTable
            dtStaff.Load(drStaff)

            If dtStaff.Rows.Count > 0 Then

                Dim ToEmail As String = dtStaff.Rows(0)("EmailPerson").ToString
                'If txtCreatedBy.Text = "SASI" Then
                '    ToEmail = "sasi.vishwa@gmail.com"
                'Else
                '    ToEmail = "Christian.Reyes@anticimex.com.sg;sasi.vishwa@gmail.com"
                'End If
                '  Dim ToEmail As String = "sasi.vishwa@gmail.com"

                Dim content As String = ""
                'content = content + "Hi " + ddlCheckOutStaff.Text + ",<br/><br/>"
                content = content + "Please find the Asset Checkout Details : <br/>"
                content = content + "<br/>CheckOut To : " + dtStaff.Rows(0)("Staffid").ToString() + " - " + dtStaff.Rows(0)("Name").ToString()
                content = content + "<br/>CheckOut By : " + txtCreatedBy.Text + " - " + Session("Name").ToString()
                content = content + "<br/>Asset No : " + txtAssetNo.Text
                content = content + "<br/>Serial No : " + txtSerialNo.Text
                If String.IsNullOrEmpty(txtCheckOutDate.Text) Then
                    content = content + "<br/>CheckOutDate :  " + DateTime.Now.ToString("dd/MM/yyyy")
                        Else
                    content = content + "<br/>CheckOutDate :  " + txtCheckOutDate.Text
                   End If
                ' content = content + "<br/>CheckOutDate :  " + txtCheckOutDate.Text
                content = content + "<br/>Description :  " + txtDescCheckOut.Text
                content = content + "<br/><br/>Thank You.<br/><br/>-AOL."

                If ToEmail = "" Then

                Else
                    SendEmailNotification(conn, ToEmail, "ASSET CHECKOUT NOTIFICATION", ddlCheckOutStaff.Text, content, rcno)

                End If
             
                '  CountMovement(conn)

                
            End If
            commandStaff.Dispose()
            dtStaff.Clear()
            dtStaff.Dispose()
            drStaff.Close()

            CountMovement(conn)
            conn.Close()
            conn.Dispose()

        

        Catch ex As Exception
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
            lblAlertCheckout.Text = ex.Message
            mdlPopupCheckOut.Show()

        End Try
    End Sub

    Private Sub CheckInSave()
        Try
            Dim rcno As String = ""
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()
            GridSave(conn)

            Dim command As MySqlCommand = New MySqlCommand

            command.CommandType = CommandType.Text
            command.Connection = conn

            Dim qry As String = "INSERT INTO tblassetmovement(AssetNo,MovementType,Location,RecipientType,Incharge,MovementDate,Notes,ReturnDate,CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn)"
            qry = qry + "VALUES(@AssetNo,@MovementType,@Location,@RecipientType,@Incharge,@MovementDate,@Notes,@ReturnDate,@CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn);"

            command.CommandText = qry
            command.Parameters.Clear()

            command.Parameters.AddWithValue("@AssetNo", txtAssetCheckOut.Text)
            command.Parameters.AddWithValue("@MovementType", "CHECKIN")
            If ddlCheckInLocation.Text = "-1" Then
                command.Parameters.AddWithValue("@Location", "")
            Else

                command.Parameters.AddWithValue("@Location", ddlCheckOutLocation.Text)

            End If
            command.Parameters.AddWithValue("@RecipientType", ddlCheckInType.Text)
            command.Parameters.AddWithValue("@Incharge", ddlCheckOutStaff.Text)
            '    command.Parameters.AddWithValue("@MovementDate", Convert.ToDateTime(txtCheckInDate.Text))
            If String.IsNullOrEmpty(txtCheckOutDate.Text) Then
                command.Parameters.AddWithValue("@MovementDate", DateTime.Now.ToString("dd/MM/yyyy"))
            Else
                command.Parameters.AddWithValue("@MovementDate", Convert.ToDateTime(txtCheckOutDate.Text))
            End If
            command.Parameters.AddWithValue("@ReturnDate", DBNull.Value)
        
            command.Parameters.AddWithValue("@Notes", txtCheckOutNotes.Text)
          
            command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
            command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
            command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
            command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
            command.ExecuteNonQuery()
            rcno = command.LastInsertedId

            command.Dispose()

            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.CommandText = "update tblasset set Status=1,StatusDesc='AVAILABLE',FinCoName='' where assetno=@assetno"

            command1.Parameters.Clear()
            command1.Parameters.AddWithValue("@assetno", txtAssetNo.Text)

            command1.Connection = conn

            command1.ExecuteNonQuery()

            UploadFile(rcno)

            ''''Retrieve Staff Details''
            Dim commandStaff As MySqlCommand = New MySqlCommand
            commandStaff.CommandType = CommandType.Text
            commandStaff.CommandText = "SELECT StaffID, Name, SecGroupAuthority,EmailPerson FROM tblstaff where StaffID = @userid;"
            commandStaff.Parameters.AddWithValue("@userid", ddlCheckOutStaff.Text)
            commandStaff.Connection = conn

            Dim drStaff As MySqlDataReader = commandStaff.ExecuteReader()
            Dim dtStaff As New DataTable
            dtStaff.Load(drStaff)

            If dtStaff.Rows.Count > 0 Then
                Dim ToEmail As String = dtStaff.Rows(0)("EmailPerson").ToString
                'If txtCreatedBy.Text = "SASI" Then
                '    ToEmail = "sasi.vishwa@gmail.com"
                'Else
                '    ToEmail = "Christian.Reyes@anticimex.com.sg;sasi.vishwa@gmail.com"
                'End If

                '  Dim ToEmail As String = "sasi.vishwa@gmail.com"
                     Dim content As String = ""
                    'content = content + "Hi " + ddlCheckInStaff.Text + ",<br/><br/>"
                    content = content + "Please find the Asset Checkin Details : <br/>"
                    content = content + "<br/>Checkin To : " + dtStaff.Rows(0)("Staffid").ToString() + " - " + dtStaff.Rows(0)("Name").ToString()
                    content = content + "<br/>Received By : " + txtCreatedBy.Text + " - " + Session("Name").ToString()

                content = content + "<br/>Asset No : " + txtAssetNo.Text
                content = content + "<br/>Serial No : " + txtSerialNo.Text
                content = content + "<br/>CheckinDate :  " + txtCheckOutDate.Text
                content = content + "<br/>Description :  " + txtDescCheckOut.Text
                    content = content + "<br/><br/>Thank You.<br/><br/>-AOL."

                    'Dim ToEmail As String = "sasi.vishwa@gmail.com"
                    'Dim content As String = "Hi " + ddlCheckInStaff.Text + ",<br/><br/>Please find the Asset Checkin Details : <br/><br/>Asset No : " + txtAssetNo.Text
                    'content = content + "<br/>CheckInDate :  " + txtCheckInDate.Text + "<br/>Description :  " + txtCheckInDesc.Text + "<br/><br/>Thank You.<br/><br/>-AOL."
                    If ToEmail = "" Then

                    Else
                    SendEmailNotification(conn, ToEmail, "ASSET CHECKIN NOTIFICATION", ddlCheckInStaff.Text, content, rcno)
                    End If

                End If
                ' CountMovement(conn)

                commandStaff.Dispose()
                dtStaff.Clear()
                dtStaff.Dispose()
                drStaff.Close()

                CountMovement(conn)

                conn.Close()
                conn.Dispose()



        Catch ex As Exception
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
            lblAlertCheckIn.Text = ex.Message
            mdlPopUpCheckIn.Show()

        End Try
    End Sub

    Protected Sub btnConfirmYes_Click(sender As Object, e As EventArgs) Handles btnConfirmYes.Click
        If Session("StatusType") = "CHECKOUT" Then
            CheckOutSave()
            '  UploadFile()

            mdlPopupConfirm.Hide()
            '  mdlPopupCheckOut.Show()

            lblMessageCheckout.Text = "ASSET CHECKED OUT SUCCESSFULLY"
            btnSaveCheckOut.Enabled = False
            btnStatus.Text = "CHECKIN"
            lblMessage.Text = "ASSET CHECKED OUT SUCCESSFULLY"
            mdlPopupCheckOut.Hide()
         

        ElseIf Session("StatusType") = "CHECKIN" Then
            CheckInSave()
            mdlPopupConfirm.Hide()
            '  mdlPopupCheckOut.Show()

            lblMessageCheckout.Text = "ASSET CHECKED IN SUCCESSFULLY"
            btnSaveCheckOut.Enabled = False
            btnStatus.Text = "CHECKOUT"
            lblMessage.Text = "ASSET CHECKED IN SUCCESSFULLY"

            mdlPopupCheckOut.Hide()
        End If
        SqlDataSource1.SelectCommand = txt.Text
        SqlDataSource1.DataBind()

        GridView1.DataBind()


    End Sub

   

    Protected Sub ddlCheckOutStaff_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCheckOutStaff.SelectedIndexChanged
        mdlPopupCheckOut.Show()

    End Sub

    Protected Sub ddlCheckOutLocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCheckOutLocation.SelectedIndexChanged
        mdlPopupCheckOut.Show()

    End Sub

    Protected Sub ddlCheckInType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCheckInType.SelectedIndexChanged
        If ddlCheckInType.SelectedValue = "STAFF" Then
            'ddlCheckOutStaff.Visible = True
            'ddlCheckOutLocation.Visible = False
            CheckInLocationRow.Visible = False

        ElseIf ddlCheckInType.SelectedValue = "LOCATION" Then
            'ddlCheckOutStaff.Visible = False
            'ddlCheckOutLocation.Visible = True
            CheckInLocationRow.Visible = True

        End If
        mdlPopUpCheckIn.Show()
    End Sub

    Protected Sub btnSaveCheckIn_Click(sender As Object, e As EventArgs) Handles btnSaveCheckIn.Click
        If ddlCheckInType.Text = "-1" Then
            lblAlertCheckIn.Text = "SELECT CHECKIN TYPE"
            mdlPopUpCheckIn.Show()
        End If
        If String.IsNullOrEmpty(txtCheckInDate.Text) Then
            txtCheckInDate.Text = DateTime.Now.ToString("dd/MM/yyyy")
        End If
        If txtDisplayRecordsLocationwise.Text = "Y" Then
            If ddlCheckInLocation.Text = "-1" Then
                lblAlertCheckIn.Text = "SELECT CHECKIN TO LOCATION"
                mdlPopUpCheckIn.Show()
            End If
            If ddlCheckInStaff.Text = "-1" Then
                lblAlertCheckIn.Text = "SELECT CHECKIN STAFF"
                mdlPopUpCheckIn.Show()
            End If
        Else
            If ddlCheckInStaff.Text = "-1" Then
                lblAlertCheckIn.Text = "SELECT CHECKIN STAFF"
                mdlPopUpCheckIn.Show()
            End If
        End If

        If Session("CheckInFileExists") <> "True" Then

            Session.Add("ChkInFileName", "")
        End If

        lblStatusMessage.Text = "Do you confirm to checkin the following? <br><br> Item : " + txtCheckInDesc.Text + "<br><br> CheckIn To : " + ddlCheckInStaff.Text

        mdlPopupConfirm.Show()
        mdlPopUpCheckIn.Hide()

    End Sub

    Protected Sub btnMovement_Click(sender As Object, e As EventArgs)
        'Try
        '    Dim row As GridViewRow = DirectCast(DirectCast(sender, Button).Parent.Parent, GridViewRow)

        '    txtMovementAssetNo.Text = DirectCast(row.FindControl("btnMovement"), Button).CommandArgument
        '    lblMovement.Text = txtMovementAssetNo.Text

        '    SqlDSMovement.SelectCommand = "select * from tblassetmovement where assetno='" & txtMovementAssetNo.Text & "'"
        '    SqlDSMovement.DataBind()
        '    grdMovement.DataBind()

        '    mdlViewMovement.Show()

        'Catch ex As Exception
        '    InsertIntoTblWebEventLog("btnMovement_Click", ex.Message.ToString, txtMovementAssetNo.Text)

        'End Try
    End Sub

    Protected Sub grdMovement_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdMovement.PageIndexChanging
        grdMovement.PageIndex = e.NewPageIndex
        BindMovement()


    End Sub

    Protected Sub grdMovement_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdMovement.RowDataBound
        If grdMovement.Rows.Count > 0 Then
            If txtDisplayRecordsLocationwise.Text = "Y" Then
                grdMovement.Columns(4).Visible = True
            Else
                grdMovement.Columns(4).Visible = False
            End If

            'If String.IsNullOrEmpty(grdMovement.Columns(7).
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='silver';this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#E4E4E4';")
            'e.Row.Attributes.Add("onmousedown", "this.style.backgroundColor='#738A9C';")
            'e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#738A9C';")

            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(grdMovement, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If

    End Sub

    Protected Sub ddlCheckInStaff_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCheckInStaff.SelectedIndexChanged
        mdlPopUpCheckIn.Show()

    End Sub

    Protected Sub ddlCheckInLocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCheckInLocation.SelectedIndexChanged
        mdlPopUpCheckIn.Show()

    End Sub

    Private Sub BindMovement()
        SqlDSMovement.SelectCommand = "select * from tblassetmovement where assetno='" & txtAssetNo.Text & "'"
        SqlDSMovement.DataBind()
        grdMovement.DataBind()

    End Sub

    Private Sub CountMovement(conn As MySqlConnection)

        'Dim conn As MySqlConnection = New MySqlConnection()

        'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        'conn.Open()

        Dim command3 As MySqlCommand = New MySqlCommand

        command3.CommandType = CommandType.Text


        command3.CommandText = "SELECT COUNT(*) AS COUNT FROM tblassetmovement where assetno='" + txtAssetNo.Text + "'"
        command3.Connection = conn

        Dim dr3 As MySqlDataReader = command3.ExecuteReader()
        Dim dt3 As New DataTable
        dt3.Load(dr3)

        If dt3.Rows.Count > 0 Then
            If dt3.Rows(0)("Count").ToString = "0" Then
                '   btnMovement.Text = "MOVEMENT"
                lblMovementCount.Text = "Movement"
            Else
                ' btnMovement.Text = "MOVEMENT" + "[" + dt3.Rows(0)("Count").ToString + "]"
                lblMovementCount.Text = "Movement" + "[" + dt3.Rows(0)("Count").ToString + "]"
            End If

        End If
        dr3.Close()
        dt3.Dispose()
        command3.Dispose()

        'conn.Close()
        'conn.Dispose()


    End Sub

    Protected Sub tb1_ActiveTabChanged(sender As Object, e As EventArgs) Handles tb1.ActiveTabChanged
        If tb1.ActiveTabIndex = 2 Then
            If String.IsNullOrEmpty(txtAssetNo.Text) Then
                lblMessage.Text = "SELECT ASSET NO TO PROCEED"
                tb1.ActiveTabIndex = 0
                Exit Sub
            Else
                lblPhotoAssetNo.Text = txtAssetNo.Text
                ' chkPrimaryPhoto.Checked = False

            End If
            BindGrid()
        End If

        If tb1.ActiveTabIndex = 1 Then
            If String.IsNullOrEmpty(txtAssetNo.Text) Then
                lblMessage.Text = "SELECT ASSET NO TO PROCEED"
                tb1.ActiveTabIndex = 0
                Exit Sub
            Else
                lblMovementAssetNo.Text = txtAssetNo.Text
                lblMovementDescription.Text = txtDescription.Text
                BindMovement()
                lblMovementRcno.Text = 0

                If grdMovement.Rows.Count > 0 Then
                    lblMovementRcno.Text = DirectCast(grdMovement.Rows(0).FindControl("Label1"), Label).Text
                    If String.IsNullOrEmpty(lblMovementRcno.Text) = False Then
                        grdMovement.SelectedIndex = 0

                        BindFileGrid(lblMovementRcno.Text)

                    End If
                    '  BindFileGrid("0")
                End If
            End If
        End If

    End Sub

    Protected Sub btnPhotoUpload_Click(sender As Object, e As EventArgs) Handles btnPhotoUpload.Click
        lblAlert.Text = ""
        Try
        
            If FileUpload2.HasFile Then

                Dim _HttpFileCollection As HttpFileCollection = Request.Files
                '   InsertIntoTblWebEventLog("btnPhotoUpload_Click", _HttpFileCollection.Count.ToString, txtRecordNoSelected.Text)
                For i As Integer = 0 To _HttpFileCollection.Count - 1
                    Dim _HttpPostedFile As HttpPostedFile = _HttpFileCollection(i)
                    If _HttpPostedFile.ContentLength > 0 Then _HttpPostedFile.SaveAs(Server.MapPath("~/Uploads/Photos/" & Path.GetFileName(_HttpPostedFile.FileName)))


                    If _HttpPostedFile.ContentLength > 200000000 Then
                        lblAlert.Text = "PHOTO UPLOAD EXCEEDS THE MAXIMUM LIMIT. FILENAME : " + _HttpPostedFile.FileName.ToString
                        Return
                    End If

                    Dim fileName As String = Path.GetFileName(_HttpPostedFile.FileName)
                    Dim ext As String = Path.GetExtension(fileName)

                    If ext = ".jpg" Or ext = ".JPG" Or ext = ".JPEG" Or ext = ".jpeg" Or ext = ".bmp" Or ext = ".BMP" Or ext = ".PNG" Or ext = ".png" Then


                        Dim st As System.IO.Stream = _HttpPostedFile.InputStream
                        Dim original As Image = Image.FromStream(st)
                        Dim memStream As MemoryStream = New MemoryStream()
                        original.Save(memStream, System.Drawing.Imaging.ImageFormat.Jpeg)
                        'If chkLargePhoto.Checked = True Then
                        '    original.Save(memStream, System.Drawing.Imaging.ImageFormat.Jpeg)
                        'Else
                        '    Dim resized As Image = ResizeImage(original, New Size(320, 240))

                        '    resized.Save(memStream, System.Drawing.Imaging.ImageFormat.Jpeg)
                        'End If

                        '  resized.Save(Server.MapPath("~/Uploads/") + Convert.ToString(Session("UserID")) + "_" + FileUpload2.FileName.ToString)

                        ' ImgConvert()
                        ' Return
                        Dim primaryphoto As String = 0

                        Dim conn As MySqlConnection = New MySqlConnection()
                        Dim cmd As MySqlCommand = New MySqlCommand

                        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                        conn.Open()

                        Dim command1 As MySqlCommand = New MySqlCommand

                        command1.CommandType = CommandType.Text
                        command1.CommandText = "SELECT * FROM tblassetphotos where assetno='" + txtAssetNo.Text + "'"
                        '    command1.CommandText = "SELECT * FROM tblassetphotos where rcno='" + rcno.ToString + "'"
                        command1.Connection = conn

                        Dim dr As MySqlDataReader = command1.ExecuteReader()
                        Dim dt As New DataTable
                        dt.Load(dr)

                        If dt.Rows.Count > 0 Then
                            primaryphoto = 0
                        Else
                            primaryphoto = 1
                        End If
                        Dim command As MySqlCommand = New MySqlCommand

                        command.CommandType = CommandType.Text

                        command.Connection = conn
                        command.CommandText = "Insert into tblassetPhotos(AssetNo,Photo,FileType,FileSize,CreatedBy,CreatedOn,PrimaryPhoto) values(@AssetNo,@Photo,@FileType,@FileSize,@CreatedBy,@CreatedOn,@PrimaryPhoto);"
                        command.Parameters.Clear()
                        command.Parameters.AddWithValue("@AssetNo", lblPhotoAssetNo.Text)


                        command.Parameters.AddWithValue("@Photo", memStream.ToArray)
                        command.Parameters.AddWithValue("@FileType", GetMimeType(original).ToString)
                        command.Parameters.AddWithValue("@FileSize", memStream.Length.ToString)
                        command.Parameters.AddWithValue("@CreatedBy", Convert.ToString(Session("UserID")))
                        command.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                        If primaryphoto = "1" Then
                            command.Parameters.AddWithValue("@PrimaryPhoto", True)
                        Else
                            command.Parameters.AddWithValue("@PrimaryPhoto", False)
                        End If
                        command.ExecuteNonQuery()
                        command.Dispose()
                        conn.Close()
                        conn.Dispose()
                        lblAlert.Text = "IMAGE UPLOADED"
                        BindGrid()

                    End If
                Next


            Else
                lblAlert.Text = "SELECT PICTURE TO UPLOAD"

            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("btnPhotoUpload_Click", ex.Message.ToString, lblPhotoAssetNo.Text)
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub


    Public Shared Function GetMimeType(ByVal i As Image) As String
        For Each codec As System.Drawing.Imaging.ImageCodecInfo In System.Drawing.Imaging.ImageCodecInfo.GetImageDecoders()
            If codec.FormatID = i.RawFormat.Guid Then Return codec.MimeType

        Next

        Return "image/unknown"
    End Function

    Public Shared Function ResizeImage(ByVal image As Image, ByVal size As Size, Optional ByVal preserveAspectRatio As Boolean = True) As Image
        Dim newWidth As Integer
        Dim newHeight As Integer
        If preserveAspectRatio Then
            Dim originalWidth As Integer = image.Width
            Dim originalHeight As Integer = image.Height
            Dim percentWidth As Single = CSng(size.Width) / CSng(originalWidth)
            Dim percentHeight As Single = CSng(size.Height) / CSng(originalHeight)
            Dim percent As Single = If(percentHeight < percentWidth, percentHeight, percentWidth)
            newWidth = CInt(originalWidth * percent)
            newHeight = CInt(originalHeight * percent)
        Else
            newWidth = size.Width
            newHeight = size.Height
        End If

        Dim newImage As Image = New Bitmap(newWidth, newHeight)

        Using graphicsHandle As Graphics = Graphics.FromImage(newImage)
            graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic
            graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight)
        End Using
        Return newImage
    End Function

    Private Sub BindGrid()
        Try
            Dim con As MySqlConnection = New MySqlConnection()

            con.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            con.Open()

            'Dim command As MySqlCommand = New MySqlCommand

            'command.CommandType = CommandType.Text

            'command.CommandText = "update tblservicephoto set LargePhoto=0 where recordno='" & lblRecordNo.Text & "' and largephoto is null"

            'command.Connection = con

            'command.ExecuteNonQuery()
            'command.Dispose()

            Dim cmd As MySqlCommand = New MySqlCommand

            cmd.CommandType = CommandType.Text

            cmd.CommandText = "SELECT * FROM tblassetphotos where assetno='" & txtAssetNo.Text & "'"
            cmd.Connection = con

            Using sda As New MySqlDataAdapter(cmd)
                Dim dt As New DataTable()
                sda.Fill(dt)
                gvImages.DataSource = dt
                gvImages.DataBind()
                dt.Clear()
                sda.Dispose()
                dt.Dispose()
            End Using

            'Display the primary photo in main tab

            Dim command2 As MySqlCommand = New MySqlCommand

            command2.CommandType = CommandType.Text


            command2.CommandText = "SELECT * FROM tblassetphotos where assetno='" + txtAssetNo.Text + "' and PrimaryPhoto=1"
            command2.Connection = con

            Dim dr2 As MySqlDataReader = command2.ExecuteReader()
            Dim dt2 As New DataTable
            dt2.Load(dr2)
        
            If dt2.Rows.Count > 0 Then
                Image2.Visible = True

                If IsDBNull(dt2.Rows(0)("Photo")) = False Then
                    Dim bytes As Byte() = DirectCast(dt2.Rows(0)("Photo"), Byte())
                    Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
                    Image2.ImageUrl = "data:image/png;base64," + base64String
                End If
            End If
            dr2.Close()
            dt2.Dispose()
            command2.Dispose()

            cmd.Dispose()
            con.Close()
            con.Dispose()

       
        Catch ex As Exception
            InsertIntoTblWebEventLog("BindGrid", ex.Message.ToString, txtAssetNo.Text)
        End Try
    End Sub

    Protected Sub OnPageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        gvImages.PageIndex = e.NewPageIndex
        Me.BindGrid()
    End Sub

    Protected Sub OnSelectedIndexChangedImg(sender As Object, e As EventArgs)
        Dim editindex As Integer = gvImages.SelectedIndex
        If editindex = 3 Then
            '   iframe1.Attributes.Add("src", DirectCast(gvImages.Rows(editindex).FindControl("ImageView"), System.Web.UI.WebControls.Image).ImageUrl)
            ImageEnlarge.ImageUrl = DirectCast(gvImages.Rows(editindex).FindControl("ImageView"), System.Web.UI.WebControls.Image).ImageUrl
            mdlPopupImage.Show()

        End If


        For Each row As GridViewRow In gvImages.Rows
            If row.RowIndex = gvImages.SelectedIndex Then
                row.BackColor = ColorTranslator.FromHtml("#738A9C")
                row.ToolTip = String.Empty
            Else
                row.BackColor = ColorTranslator.FromHtml("#E4E4E4")
                row.ToolTip = "Click to select this row."
            End If
        Next
    End Sub


    Protected Sub OnRowDataBound1(sender As Object, e As GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim bytes As Byte() = TryCast(TryCast(e.Row.DataItem, DataRowView)("Photo"), Byte())
                Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
                TryCast(e.Row.FindControl("ImageView"), System.Web.UI.WebControls.Image).ImageUrl = Convert.ToString("data:image/png;base64,") & base64String
                'TryCast(e.Row.FindControl("ImageButton1"), System.Web.UI.WebControls.ImageButton).ImageUrl = Convert.ToString("data:image/png;base64,") & base64String
                ''  TryCast(e.Row.FindControl("ImageButton1"), System.Web.UI.WebControls.ImageButton).Attributes.Add("onclick", "javascript: LoadDiv ('" & Convert.ToString("data:image/png;base64,") & base64String & "');")
                ''   iframe1.Attributes.Add("src", Convert.ToString("data:image/png;base64,") & base64String)
                'e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer';")

                'e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvImages, "Select$" & e.Row.RowIndex)
                'e.Row.ToolTip = "Click to select this row."

            End If

        Catch ex As Exception
            InsertIntoTblWebEventLog("OnRowDataBound1", ex.Message.ToString, txtAssetNo.Text)
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Protected Sub DeletePhoto(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim rcno As Int64 = CType(sender, LinkButton).CommandArgument
            txtDeletePhoto.Text = rcno

            lblDeleteConfirm.Text = "Photo"
            ' lblEvent.Text = "Confirm DELETE"

            Dim grdrow As GridViewRow = CType((CType(sender, LinkButton)).NamingContainer, GridViewRow)

            lblQuery.Text = "Are you sure to DELETE the Photo? "
            mdlPopupDeleteUploadedFile.Show()


        Catch ex As Exception
            InsertIntoTblWebEventLog("DeletePhoto", ex.Message.ToString, txtDeletePhoto.Text)
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Protected Sub btnConfirmDelete_Click(sender As Object, e As EventArgs) Handles btnConfirmDelete.Click
        If lblDeleteConfirm.Text = "Photo" Then
            DeleteUploadedPhoto(Convert.ToInt64(txtDeletePhoto.Text))
        ElseIf lblDeleteConfirm.Text = "PrimaryPhoto" Then
            UpdatePrimaryPhoto(Convert.ToInt64(lblImageRcNo.Text))
        ElseIf lblDeleteConfirm.Text = "File" Then
            DeleteUploadedfile(txtDeleteUploadedFile.Text)
        End If


    End Sub

    Private Sub DeleteUploadedPhoto(rcno As Int64)
      
        Try

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()
            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.CommandText = "SELECT * FROM tblassetphotos where rcno='" + rcno.ToString + "'"
            command1.Connection = conn
         
            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then

                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                Dim qry As String = "delete from tblassetphotos where rcno='" + rcno.ToString + "'"

                command.CommandText = qry

                command.Connection = conn

                command.ExecuteNonQuery()
                command.Dispose()

                '  MessageBox.Message.Alert(Page, "Record deleted successfully!!!", "str")
                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "DELETEASSETPHOTO", rcno, "DELETE", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtAssetNo.Text)
            End If
            command1.Dispose()

            conn.Close()
            conn.Dispose()

            lblMessage.Text = "PHOTO DELETED"

            BindGrid()



        Catch ex As Exception
            InsertIntoTblWebEventLog("DeleteUploadedPhoto", ex.Message.ToString, txtAssetNo.Text)
        End Try
    End Sub

    Private Sub UpdatePrimaryPhoto(rcno As Int64)
        Try

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            If chkPrimaryPhoto.Checked = True Then
                Dim command2 As MySqlCommand = New MySqlCommand

                command2.CommandType = CommandType.Text

                command2.CommandText = "SELECT * FROM tblassetphotos where assetno='" + txtAssetNo.Text + "'"
                command2.Connection = conn

                Dim dr2 As MySqlDataReader = command2.ExecuteReader()
                Dim dt2 As New DataTable
                dt2.Load(dr2)

                If dt2.Rows.Count > 0 Then

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text


                    command.CommandText = "Update tblassetphotos set PrimaryPhoto=0 where assetno = '" + txtAssetNo.Text + "'"

                    command.Connection = conn

                    command.ExecuteNonQuery()
                    command.Dispose()
                End If
                dr2.Close()
                dt2.Dispose()
                command2.Dispose()
          
            End If

           
            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text


            command1.CommandText = "SELECT * FROM tblassetphotos where rcno='" + rcno.ToString + "'"
            command1.Connection = conn
        
            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)
            '  InsertIntoTblWebEventLog("UpdateLargePhoto", rcno.ToString + " " + txtCheckboxValue.Text, txtRecordNoSelected.Text)

            If dt.Rows.Count > 0 Then

                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                ' Dim qry As String = "delete from tblservicephoto where rcno='" + rcno.ToString + "'"

                ' command.CommandText = qry
                If chkPrimaryPhoto.Checked = "True" Then
                    command.CommandText = "Update tblassetphotos set PrimaryPhoto=1 where rcno = '" + rcno.ToString + "'"

                Else
                    command.CommandText = "Update tblassetphotos set PrimaryPhoto=0 where rcno = '" + rcno.ToString + "'"

                End If
                command.Connection = conn

                command.ExecuteNonQuery()
                command.Dispose()

                '  MessageBox.Message.Alert(Page, "Record deleted successfully!!!", "str")
                '   CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "DELETEPHOTO", rcno, "DELETE", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtSvcRecord.Text)
            End If

            conn.Close()
            conn.Dispose()
            command1.Dispose()

            lblMessage.Text = "PRIMARY PHOTO FIELD UPDATED"

            BindGrid()



        Catch ex As Exception
            InsertIntoTblWebEventLog("UpdatePrimaryPhoto", ex.Message.ToString, txtAssetNo.Text)
        End Try

    End Sub

    Protected Sub ViewImage(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim rcno As Int64 = CType(sender, LinkButton).CommandArgument
            Dim grdrow As GridViewRow = CType((CType(sender, LinkButton)).NamingContainer, GridViewRow)
            lblImageRcNo.Text = rcno

            'Dim bytes As Byte() = TryCast(TryCast(grdrow.Cells(3).d, DataRowView)("Photo"), Byte())
            'Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
            'TryCast(e.Row.FindControl("ImageView"), System.Web.UI.WebControls.Image).ImageUrl = Convert.ToString("data:image/png;base64,") & base64String

            ImageEnlarge.ImageUrl = DirectCast(grdrow.FindControl("ImageView"), System.Web.UI.WebControls.Image).ImageUrl
            chkPrimaryPhoto.Checked = DirectCast(grdrow.FindControl("chkSelectPrimaryPhotoGV"), CheckBox).Checked

            mdlPopupImage.Show()

        Catch ex As Exception
            InsertIntoTblWebEventLog("ViewImage", ex.Message.ToString, txtAssetNo.Text)
        End Try
    End Sub

    Protected Sub gvImages_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvImages.RowCommand

    End Sub

    Protected Sub chkPrimaryPhoto_CheckedChanged(sender As Object, e As EventArgs) Handles chkPrimaryPhoto.CheckedChanged
        Try

         
            lblDeleteConfirm.Text = "PrimaryPhoto"
            ' lblEvent.Text = "Confirm DELETE"

            '  Dim grdrow As GridViewRow = CType((CType(sender, CheckBox)).NamingContainer, GridViewRow)
            If chkPrimaryPhoto.Checked = True Then
                lblQuery.Text = "Are you sure to make this as Primary Photo? "

            Else
                lblQuery.Text = "Are you sure to remove this from Primary Photo? "

            End If
             mdlPopupDeleteUploadedFile.Show()


        Catch ex As Exception
            InsertIntoTblWebEventLog("chkPrimaryPhoto_CheckedChanged", ex.Message.ToString, txtDeletePhoto.Text)
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Protected Sub btnMovement_Click1(sender As Object, e As EventArgs) Handles btnMovement.Click
        Try
          
            '   lblMovement.Text = txtAssetNo.Text + " - " + txtDescription.Text

            SqlDSMovement.SelectCommand = "select * from tblassetmovement where assetno='" & txtAssetNo.Text & "'"
            SqlDSMovement.DataBind()
            grdMovement.DataBind()

            '  mdlViewMovement.Show()

        Catch ex As Exception
            InsertIntoTblWebEventLog("btnMovement_Click", ex.Message.ToString, lblMovementAssetNo.Text)

        End Try
    End Sub

    Protected Sub ddlDurationType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDurationType.SelectedIndexChanged
        Dim d As Double
        If Double.TryParse(txtWarranty.Text, d) = False Then
            '   MessageBox.Message.Alert(Page, "Allocated time is invalid!!! Enter time in mins!!", "str")
            lblAlert.Text = "WARRANTY SHOULD BE IN NUMBERS"

            Exit Sub
        End If
        WarrantyCalculation()

    End Sub

    Protected Sub txtWarranty_TextChanged(sender As Object, e As EventArgs) Handles txtWarranty.TextChanged
        Dim d As Double
        If Double.TryParse(txtWarranty.Text, d) = False Then
            '   MessageBox.Message.Alert(Page, "Allocated time is invalid!!! Enter time in mins!!", "str")
            lblAlert.Text = "WARRANTY SHOULD BE IN NUMBERS"

            Exit Sub
        End If
        WarrantyCalculation()

    End Sub

    Private Sub GenerateAssetNo()
        Dim num1 As String
        Dim month As Integer
        Dim month1 As String

        '    Dim dtdate As Date
      
            num1 = Date.Now.Year.ToString
        num1 = "AS" + num1
            month = Convert.ToInt32(DateTime.Now.ToString("MM"))
            month1 = DateTime.Now.ToString("MM")
      
        InsertIntoTblWebEventLog("GenerateAssetNo", num1, txtCreatedBy.Text)

        Try
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.CommandText = "SELECT * FROM tbldoccontrol where Prefix= '" + num1 + "';"
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            Dim field As String = ""
            Dim lastnumber As String = ""
            If dt.Rows.Count > 0 Then
                Dim num As Integer

                Select Case month
                    Case "01"
                        num = Convert.ToInt32(dt.Rows(0)("Period01"))
                        field = "Period01"
                        lastnumber = num
                    Case "02"
                        num = Convert.ToInt32(dt.Rows(0)("Period02"))
                        field = "Period02"
                        lastnumber = num
                    Case "03"
                        num = Convert.ToInt32(dt.Rows(0)("Period03"))
                        field = "Period03"
                        lastnumber = num
                    Case "04"
                        num = Convert.ToInt32(dt.Rows(0)("Period04"))
                        field = "Period04"
                        lastnumber = num
                    Case "05"
                        num = Convert.ToInt32(dt.Rows(0)("Period05"))
                        field = "Period05"
                        lastnumber = num
                    Case "06"
                        num = Convert.ToInt32(dt.Rows(0)("Period06"))
                        field = "Period06"
                        lastnumber = num
                    Case "07"
                        num = Convert.ToInt32(dt.Rows(0)("Period07"))
                        field = "Period07"
                        lastnumber = num
                    Case "08"
                        num = Convert.ToInt32(dt.Rows(0)("Period08"))
                        field = "Period08"
                        lastnumber = num
                    Case "09"
                        num = Convert.ToInt32(dt.Rows(0)("Period09"))
                        field = "Period09"
                        lastnumber = num
                    Case "10"
                        num = Convert.ToInt32(dt.Rows(0)("Period10"))
                        field = "Period10"
                        lastnumber = num
                    Case "11"
                        num = Convert.ToInt32(dt.Rows(0)("Period11"))
                        field = "Period11"
                        lastnumber = num
                    Case "12"
                        num = Convert.ToInt32(dt.Rows(0)("Period12"))
                        field = "Period12"
                        lastnumber = num
                End Select
                Dim length As String = "D" + dt.Rows(0)("Width").ToString

                txtAssetNo.Text = num1 + month1 + "-" + (num + 1).ToString(length)

                ' txtSvcRecord.Text = dt.Rows(0)("Prefix") + DateTime.Now.ToString("MM") + dt.Rows(0)("Separator").ToString + (num + 1).ToString(length)
                '  txtSvcRecord.Text = dt.Rows(0)("Prefix") + DateTime.Now.ToString("MM") + "-" + (num + 1).ToString(length)

                'txtSvcRecord.Text = dt.Rows(0)("Prefix") + month1 + "-" + (num + 1).ToString(length)

                '   txtSvcRecord.Text = txtPrefixDocNoService.Text + dt.Rows(0)("Prefix") + month1 + "-" + (num + 1).ToString(length)
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                'Increment Autonumber
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                Dim lastnum As Integer = Convert.ToDouble(lastnumber + 1)

                Dim cmd1 As MySqlCommand = New MySqlCommand("update tbldoccontrol set " & field & " = " & lastnum & " where Prefix = '" + num1 + "';", conn)
                cmd1.ExecuteNonQuery()

                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            Else
                Dim command As New MySqlCommand

                Command.CommandType = CommandType.Text

                command.CommandText = "insert into tbldoccontrol(Prefix,GenerateMethod,Period01,Period02,Period03,Period04,Period05,Period06,Period07,Period08,Period09,Period10,Period11,Period12,width) values('" + num1 + "','M',0,0,0,0,0,0,0,0,0,0,0,0,6)"
                Command.Connection = conn

                Command.ExecuteNonQuery()

                txtAssetNo.Text = num1 + month1 + "-" + "000001"
            End If
            conn.Close()
            conn.Dispose()
            command1.Dispose()
            dt.Dispose()
            dr.Close()
        Catch ex As Exception
            InsertIntoTblWebEventLog("GenerateAssetNo", ex.Message.ToString, num1)
        End Try
    End Sub

 
    Protected Sub UploadFile(rcno As String)
        lblMessage.Text = ""
        lblAlert.Text = ""
        If String.IsNullOrEmpty(txtAssetNo.Text) Then
            lblAlert.Text = "SELECT ACCOUNT TO UPLOAD FILE"
            Return

        End If
        Dim sessionfilename As String = ""
        InsertIntoTblWebEventLog("UploadFile", Session("StatusType"), Session("CheckInFileExists") + " " + Session("CheckOutFileExists"))
        ' InsertIntoTblWebEventLog("UploadFile", Session("StatusType"), AjaxFileUpload1.ContextKeys.ToString + " " + AjaxFileUpload2.ContextKeys.ToString)

        If (Session("StatusType") = "CHECKOUT" And Session("CheckOutFileExists") <> "True") Then
            Return
        ElseIf (Session("StatusType") = "CHECKIN" And Session("CheckInFileExists") <> "True") Then
            Return
        End If

        Try
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()


            Dim commandtbw As MySqlCommand = New MySqlCommand

            commandtbw.CommandType = CommandType.Text
            Dim qry As String = "select * from tbwassetfileupload"
            qry = qry + " where date(createdon)='" + DateTime.Now.ToString("yyyyMMdd") + "' and documenttype='" + Session("StatusType") + "' and fileref='" + Session("AssetNo") + "' and createdby='" + txtCreatedBy.Text + "';"

            commandtbw.Connection = conn

            commandtbw.CommandText = qry
            Dim drtbw As MySqlDataReader = commandtbw.ExecuteReader()
            Dim dttbw As New DataTable
            dttbw.Load(drtbw)

            If dttbw.Rows.Count > 0 Then
                For i As Int16 = 0 To dttbw.Rows.Count - 1

                    Dim filename As String = rcno + "_" + dttbw.Rows(i)("FileName").ToString

                    Dim filepath As String = dttbw.Rows(i)("FileNameLink").ToString
                    Dim newfilepath As String = Server.MapPath("~/Uploads/Asset/") + filename

                    InsertIntoTblWebEventLog("UploadFile3", newfilepath, filepath)
                    InsertIntoTblWebEventLog("UploadFile4", filename, Session("AssetNo"))
                    File.Move(filepath, newfilepath)


                    Dim qry1 As String = "INSERT INTO tblfileupload(FileGroup,FileRef,FileName,FileDescription,FileType,FileNameLink, ManualReport,CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn,DocumentType)"
                    qry1 = qry1 + "VALUES(@FileGroup,@FileRef,@FileName,@FileDescription,@FileType,@FileNameLink,@ManualReport,@CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn,@DocumentType);"

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    command.CommandText = qry1
                    command.Parameters.Clear()

                    command.Parameters.AddWithValue("@FileGroup", "ASSET")
                    command.Parameters.AddWithValue("@FileRef", Session("AssetNo"))
                    command.Parameters.AddWithValue("@FileName", dttbw.Rows(i)("FileName").ToString)
                    command.Parameters.AddWithValue("@FileDescription", dttbw.Rows(i)("FileDescription").ToString)
                    command.Parameters.AddWithValue("@FileType", dttbw.Rows(i)("FileType").ToString)


                    command.Parameters.AddWithValue("@ManualReport", "N")

                    command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                    command.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))

                    command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                    command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                    command.Parameters.AddWithValue("@FileNameLink", filename.ToUpper)
                    command.Parameters.AddWithValue("@DocumentType", dttbw.Rows(i)("DocumentType").ToString)
                    command.Connection = conn

                    command.ExecuteNonQuery()

                    command.Dispose()

                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "FILEUPLOAD", txtAssetNo.Text, "ADD", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", txtAssetNo.Text + "_" + filename, txtAssetNo.Text)


                Next

                dttbw.Dispose()
                drtbw.Close()
                commandtbw.Dispose()

            End If

            If Directory.Exists(Session("CheckOutPath")) Then
                Directory.Delete(Session("CheckOutPath"))
            End If

            conn.Close()
            conn.Dispose()


            'For Each foundFile As String In My.Computer.FileSystem.GetFiles(Session("CheckOutPath") + "/")
            '    sessionfilename = Path.GetFileName(foundFile)
            '    Dim filepath As String = foundFile

            '    ' filepath = Server.MapPath("~/Uploads/Asset/temp/") + filepath
            '    '     filepath = Session("CheckOutPath") + "/" + filepath

            '    Dim filename As String = rcno + "_" + sessionfilename
            '    Dim newfilepath As String = Server.MapPath("~/Uploads/Asset/") + filename

            '    Dim ext As String = Path.GetExtension(sessionfilename)

            '    InsertIntoTblWebEventLog("UploadFile3", newfilepath, filepath)
            '    InsertIntoTblWebEventLog("UploadFile4", sessionfilename, ext)


            '    If ext = ".DOC" Or ext = ".doc" Or ext = ".DOCX" Or ext = ".docx" Or ext = ".xls" Or ext = ".xlsx" Or ext = ".XLS" Or ext = ".XLSX" Or ext = ".CSV" Or ext = ".csv" Or ext = ".ppt" Or ext = ".PPT" Or ext = ".pptx" Or ext = ".PPTX" Or ext = ".PDF" Or ext = ".pdf" Or ext = ".txt" Or ext = ".TXT" Or ext = ".jpg" Or ext = ".jpeg" Or ext = ".png" Or ext = ".bmp" Or ext = ".JPG" Or ext = ".JPEG" Or ext = ".PNG" Or ext = ".BMP" Then

            '        If File.Exists(Server.MapPath("~/Uploads/Asset/") + filename) Then

            '            Dim command1 As MySqlCommand = New MySqlCommand

            '            command1.CommandType = CommandType.Text

            '            command1.CommandText = "SELECT * FROM tblFILEUPLOAD where filenamelink=@filenamelink"
            '            command1.Parameters.AddWithValue("@filenamelink", filename)
            '            command1.Connection = conn

            '            Dim dr As MySqlDataReader = command1.ExecuteReader()
            '            Dim dt As New DataTable
            '            dt.Load(dr)

            '            If dt.Rows.Count > 0 Then

            '                '  MessageBox.Message.Alert(Page, "Record already exists!!!", "str")
            '                lblAlert.Text = "FILE ALREADY EXISTS"
            '                conn.Close()
            '                conn.Dispose()
            '                Exit Sub
            '            End If
            '        Else
            '            Dim command1 As MySqlCommand = New MySqlCommand

            '            command1.CommandType = CommandType.Text

            '            command1.CommandText = "SELECT * FROM tblFILEUPLOAD where filenamelink=@filenamelink"
            '            command1.Parameters.AddWithValue("@filenamelink", filename)
            '            command1.Connection = conn

            '            Dim dr As MySqlDataReader = command1.ExecuteReader()
            '            Dim dt As New DataTable
            '            dt.Load(dr)

            '            If dt.Rows.Count > 0 Then

            '                Dim command2 As MySqlCommand = New MySqlCommand

            '                command2.CommandType = CommandType.Text
            '                command2.CommandText = "delete from tblfileupload where filenamelink='" + filename + "'"
            '                command2.Connection = conn
            '                command2.ExecuteNonQuery()
            '            End If
            '        End If
            '        File.Move(filepath, newfilepath)


            '        '   FileUpload1.PostedFile.SaveAs((Server.MapPath("~/Uploads/Asset/") + txtAssetNo.Text + "_" + fileName))


            '        Dim command As MySqlCommand = New MySqlCommand

            '        command.CommandType = CommandType.Text
            '        Dim qry As String = "INSERT INTO tblfileupload(FileGroup,FileRef,FileName,FileDescription,FileType,FileNameLink, ManualReport,CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn,DocumentType)"
            '        qry = qry + " select FileGroup,FileRef,FileName,FileDescription,FileType,concat(" + rcno + ",""_"",FileName),ManualReport,CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn,DocumentType from tbwassetfileupload"
            '        qry = qry + " where date(createdon)='" + DateTime.Now.ToString("yyyyMMdd") + "' and documenttype='" + Session("StatusType") + "' and fileref='" + Session("AssetNo") + "' and createdby='" + txtCreatedBy.Text + "';"


            '        command.CommandText = qry
            '        command.Connection = conn

            '        command.ExecuteNonQuery()

            '        'command.Parameters.Clear()

            '        'command.Parameters.AddWithValue("@FileGroup", "ASSET")
            '        'command.Parameters.AddWithValue("@FileRef", txtAssetNo.Text)
            '        'command.Parameters.AddWithValue("@FileName", sessionfilename.ToUpper)
            '        'command.Parameters.AddWithValue("@FileDescription", Session("StatusType"))
            '        'command.Parameters.AddWithValue("@FileType", ext.ToUpper)


            '        'command.Parameters.AddWithValue("@ManualReport", "N")

            '        'command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
            '        'command.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))

            '        'command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
            '        'command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))
            '        'command.Parameters.AddWithValue("@FileNameLink", filename.ToUpper)







            '        'Dim command5 As MySqlCommand = New MySqlCommand

            '        'command5.CommandType = CommandType.Text

            '        'command5.CommandText = "update tblassetmovement set filenamelink=@filenamelink,filename=@filename where rcno=@rcno"

            '        'command5.Parameters.Clear()
            '        'command5.Parameters.AddWithValue("@rcno", rcno)
            '        'command5.Parameters.AddWithValue("@filenamelink", filename.ToUpper)
            '        'command5.Parameters.AddWithValue("@filename", sessionfilename.ToUpper)
            '        'command5.Connection = conn

            '        'command5.ExecuteNonQuery()
            '        'command5.Dispose()


            '        'SqlDSUpload.SelectCommand = "select * from tblfileupload where fileref = '" + txtSvcRecord.Text + "'"
            '        'gvUpload.DataSourceID = "SqlDSUpload"
            '        'gvUpload.DataBind()

    
            ''  lblMessage.Text = "FILE UPLOADED"
            ''     lblFileUploadCount.Text = "File Upload [" & gvUpload.Rows.Count & "]"

            '    Else
            'lblAlert.Text = "FILE FORMAT NOT ALLOWED TO UPLOAD"
            'Return
            '    End If
            'Next
          
            'Else
            '    'lblAlert.Text = "SELECT FILE TO UPLOAD"
            'End If
            '  Response.Redirect(Request.Url.AbsoluteUri)


        Catch ex As Exception
            InsertIntoTblWebEventLog("UploadFile", ex.Message.ToString, sessionfilename)
        End Try
        ' End If
        Session.Add("CheckOutFileExists", "False")
        Session.Add("CheckInFileExists", "False")
        Session.Remove("CheckOutPath")
        'Session.Remove("ChkOutFileName")
        'Session.Remove("ChkInFileName")
    End Sub

 

    Protected Sub AsyncFileUpload1_UploadedComplete(sender As Object, e As AjaxControlToolkit.AsyncFileUploadEventArgs) Handles AsyncFileUpload1.UploadedComplete
        Dim filename As String = System.IO.Path.GetFileName(AsyncFileUpload1.FileName)
        AsyncFileUpload1.PostedFile.SaveAs((Server.MapPath("~/Uploads/Asset/temp/") + txtAssetNo.Text + "_" + filename))
        If AsyncFileUpload1.HasFile Then
            Session.Add("ChkOutFileName", AsyncFileUpload1.FileName)
            lblChkOutFile.Text = "True"
        End If
     
    End Sub

    Protected Sub AsyncFileUpload2_UploadedComplete(sender As Object, e As AjaxControlToolkit.AsyncFileUploadEventArgs) Handles AsyncFileUpload2.UploadedComplete
        Dim filename As String = System.IO.Path.GetFileName(AsyncFileUpload2.FileName)
        AsyncFileUpload2.PostedFile.SaveAs((Server.MapPath("~/Uploads/Asset/temp/") + txtAssetNo.Text + "_" + filename))
        If AsyncFileUpload2.HasFile Then
            Session.Add("ChkInFileName", AsyncFileUpload2.FileName)
            lblChkInFile.Text = "True"
        End If
    End Sub

    'Protected Sub AsyncFileUpload1_UploadedFileError(sender As Object, e As AjaxControlToolkit.AsyncFileUploadEventArgs) Handles AsyncFileUpload1.UploadedFileError
    '    Dim ext As String = Path.GetExtension(AsyncFileUpload1.FileName)
    '    If ext = ".DOC" Or ext = ".doc" Or ext = ".DOCX" Or ext = ".docx" Or ext = ".xls" Or ext = ".xlsx" Or ext = ".XLS" Or ext = ".XLSX" Or ext = ".CSV" Or ext = ".csv" Or ext = ".ppt" Or ext = ".PPT" Or ext = ".pptx" Or ext = ".PPTX" Or ext = ".PDF" Or ext = ".pdf" Or ext = ".txt" Or ext = ".TXT" Or ext = ".jpg" Or ext = ".jpeg" Or ext = ".png" Or ext = ".bmp" Or ext = ".JPG" Or ext = ".JPEG" Or ext = ".PNG" Or ext = ".BMP" Then
    '    Else
    '        lblAlertCheckout.Text = "FILE FORMAT NOT ALLOWED TO UPLOAD"
    '        mdlPopupCheckOut.Show()

    '    End If

    'End Sub

    'Protected Sub AsyncFileUpload2_UploadedFileError(sender As Object, e As AjaxControlToolkit.AsyncFileUploadEventArgs) Handles AsyncFileUpload2.UploadedFileError
    '    Dim ext As String = Path.GetExtension(AsyncFileUpload2.FileName)
    '    If ext = ".DOC" Or ext = ".doc" Or ext = ".DOCX" Or ext = ".docx" Or ext = ".xls" Or ext = ".xlsx" Or ext = ".XLS" Or ext = ".XLSX" Or ext = ".CSV" Or ext = ".csv" Or ext = ".ppt" Or ext = ".PPT" Or ext = ".pptx" Or ext = ".PPTX" Or ext = ".PDF" Or ext = ".pdf" Or ext = ".txt" Or ext = ".TXT" Or ext = ".jpg" Or ext = ".jpeg" Or ext = ".png" Or ext = ".bmp" Or ext = ".JPG" Or ext = ".JPEG" Or ext = ".PNG" Or ext = ".BMP" Then
    '    Else
    '        lblAlertCheckIn.Text = "FILE FORMAT NOT ALLOWED TO UPLOAD"
    '        mdlPopUpCheckIn.Show()

    '    End If
    'End Sub


    Protected Sub PreviewFile(ByVal sender As Object, ByVal e As EventArgs)
        'Try
        '   InsertIntoTblWebEventLog("PreviewFile1", "", txtAssetNo.Text)

        'Dim directory As String = CType(sender, LinkButton).CommandArgument
        'directory = Path.Combine("Uploads/Asset/", directory)

        'Dim di As DirectoryInfo = New DirectoryInfo(directory)
        'Dim ds As System.Security.AccessControl.DirectorySecurity = di.GetAccessControl()

        'For Each rule As System.Security.AccessControl.AccessRule In ds.GetAccessRules(True, True, GetType(System.Security.Principal.NTAccount))
        '    InsertIntoTblWebEventLog("PreviewFile2", rule.IdentityReference.Value, rule.AccessControlType)
        'Next
        'Exit Sub


        iframeid.Attributes.Add("src", "about:blank")
        lblMsgMovement.Text = ""
        Dim filePath As String = CType(sender, LinkButton).CommandArgument
        InsertIntoTblWebEventLog("PreviewFile2", filePath, txtAssetNo.Text)

        If String.IsNullOrEmpty(filePath) Or filePath = "" Then
            lblMsgMovement.Text = "NO FILE TO PREVIEW"
            Return

        End If
        Dim ext As String = Path.GetExtension(filePath)
        filePath = Path.Combine("Uploads/Asset/", filePath)

        ext = ext.ToLower
        InsertIntoTblWebEventLog("PreviewFile3", ext, txtAssetNo.Text)

        If ext = ".doc" Or ext = ".docx" Or ext = ".xls" Or ext = ".xlsx" Then
            Dim strFilePath As String = Server.MapPath("Uploads\Asset\")
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

            iframeid.Attributes("src") = "Uploads/Asset/A" + strFile.Split("."c)(0) & Convert.ToString(".html")

        Else
            filePath = filePath.Replace("#", "%23")
            ' filePath = filePath.Replace(".PDF", ".pdf")
            InsertIntoTblWebEventLog("PreviewFile4", filePath, txtAssetNo.Text)

            iframeid.Attributes.Add("src", ResolveUrl(filePath))
            '  iframeid.Attributes.Add("src", "Uploads/Asset/temp/GBH8667H/GBH8667H_Revision Time Table.pdf")
        End If
        InsertIntoTblWebEventLog("PreviewFile5", filePath, txtAssetNo.Text)

        '  iframeid.Attributes.Add("src", "https://docs.google.com/viewer?url={D:/1_CWBInfotech/A_Sitapest/Program/Sitapest/Uploads/10000145_ActualVsForecast_Format1.pdf?pid=explorer&efh=false&a=v&chrome=false&embedded=true")
        'Catch ex As Exception
        '    InsertIntoTblWebEventLog("PreviewFile", ex.Message.ToString, txtAssetNo.Text)
        '    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        'End Try
    End Sub

    Protected Sub PreviewFile1(ByVal sender As Object, ByVal e As EventArgs)
      
        Dim filePath As String = CType(sender, LinkButton).CommandArgument
        '   InsertIntoTblWebEventLog("PreviewFile2", filePath, txtAssetNo.Text)

        If String.IsNullOrEmpty(filePath) Or filePath = "" Then
            lblMsgMovement.Text = "NO FILE TO PREVIEW"
            Return

        End If
        Dim ext As String = Path.GetExtension(filePath)
        filePath = Path.Combine("Uploads/Asset/", filePath)

        ext = ext.ToLower
        '    InsertIntoTblWebEventLog("PreviewFile3", ext, txtAssetNo.Text)
        Dim url As String = ""
        If ext = ".doc" Or ext = ".docx" Or ext = ".xls" Or ext = ".xlsx" Then
            Dim strFilePath As String = Server.MapPath("Uploads\Asset\")
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

            url = "Uploads/Asset/A" + strFile.Split("."c)(0) & Convert.ToString(".html")

        Else
            filePath = filePath.Replace("#", "%23")
            filePath = filePath.Replace(".PDF", ".pdf")
            '  InsertIntoTblWebEventLog("PreviewFile4", filePath, Directory.GetAccessControl(filePath).ToString)

            url = ResolveUrl(filePath)
            '  iframeid.Attributes.Add("src", "Uploads/Asset/temp/GBH8667H/GBH8667H_Revision Time Table.pdf")
        End If

        Dim s As String = "window.open('" & url + "', '_blank');"
        ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
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
            'Excel.Visible = False
            'Excel.Application.Visible = False
            'Excel.WindowState = Microsoft.Office.Interop.Excel.XlWindowState.xlMinimized

            'Excel.Workbooks.Open(Source, Unknown, Unknown, Unknown, Unknown, Unknown, _
            '    Unknown, Unknown, Unknown, Unknown, Unknown, Unknown, _
            '    Unknown, Unknown, Unknown)

            'Dim format As Object = Microsoft.Office.Interop.Excel.XlFileFormat.xlHtml

            'Excel.Workbooks(1).SaveAs(Target, format, Unknown, Unknown, Unknown, Unknown, _
            '    Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlShared, Unknown, Unknown, Unknown, Unknown, Unknown)

            'Status = StatusType.SUCCESS

            'Message = Status.ToString()

            Dim format As Object = Microsoft.Office.Interop.Excel.XlFileFormat.xlHtml

            Dim excel As New Microsoft.Office.Interop.Excel.Application
            Dim xls As Microsoft.Office.Interop.Excel.Workbook
            xls = excel.Workbooks.Open(Source)
            xls.SaveAs(Target, format)
            xls.Close()
        Catch e As Exception
            Message = "Error :" + e.Message.ToString().Trim()
        Finally
            If Excel IsNot Nothing Then
                Excel.Workbooks.Close()
                Excel.Quit()
            End If
        End Try
    End Sub

    Protected Sub DownloadFile(ByVal sender As Object, ByVal e As EventArgs)
        Try
            lblMsgMovement.Text = ""
            Dim filePath As String = CType(sender, LinkButton).CommandArgument
            If String.IsNullOrEmpty(filePath) Or filePath = "" Then
                lblMsgMovement.Text = "NO FILE TO DOWNLOAD"
                Return

            End If
            filePath = Server.MapPath("~/Uploads/Asset/") + filePath
          

            Response.ContentType = ContentType
            Response.AppendHeader("Content-Disposition", ("attachment; filename=" + Path.GetFileName(filePath)))
            Response.WriteFile(filePath)
            Response.End()
        Catch ex As Exception
            InsertIntoTblWebEventLog("DownloadFile", ex.Message.ToString, txtAssetNo.Text)
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Protected Sub EmailFile(ByVal sender As Object, ByVal e As EventArgs)
        Try
            lblMsgMovement.Text = ""
            Dim filePath As String = CType(sender, LinkButton).CommandArgument
            If String.IsNullOrEmpty(filePath) Or filePath = "" Then
                lblMsgMovement.Text = "NO FILE TO EMAIL"
                Return

            End If
            Session.Add("FileName", filePath)
            filePath = Server.MapPath("~/Uploads/Asset/") + filePath
          
            Session.Add("FilePath", filePath)
            Session.Add("AssetNo", txtAssetNo.Text)
            Response.Redirect("Email.aspx?Type=AssetFileUpload")

        Catch ex As Exception
            InsertIntoTblWebEventLog("EmailFile", ex.Message.ToString, txtAssetNo.Text)
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

  
    Protected Sub AjaxFileUpload1_UploadComplete(sender As Object, e As AjaxControlToolkit.AjaxFileUploadEventArgs) Handles AjaxFileUpload1.UploadComplete
        Try
            InsertIntoTblWebEventLog("AjaxFileUpload1_UploadComplete1", e.FileName.ToString, Session("AssetNo"))

            Dim filename As String = System.IO.Path.GetFileName(e.FileName)
            filename = Session("AssetNo") + "_" + filename
            InsertIntoTblWebEventLog("AjaxFileUpload1_UploadComplete2", filename, Session("AssetNo"))
            Dim temppath As String = Server.MapPath("~\Uploads\Asset\temp\") + Session("AssetNo")
            Dim path1 As String = Server.MapPath("~\Uploads\Asset\temp\") + Session("AssetNo") + "\" + filename
            InsertIntoTblWebEventLog("AjaxFileUpload1_UploadComplete3", path1, Session("AssetNo"))
               If Not Directory.Exists(temppath) Then
                Directory.CreateDirectory(temppath)
            End If
            AjaxFileUpload1.SaveAs(path1)

            If Session("StatusType") = "CHECKOUT" Then
                Session.Add("CheckOutFileExists", "True")
            ElseIf Session("StatusType") = "CHECKIN" Then
                Session.Add("CheckInFileExists", "True")
            End If

            InsertIntoTblWebEventLog("AjaxFileUpload1_UploadComplete", path1, Session("AssetNo"))
            Session.Add("CheckOutPath", temppath)
            'Session.Add("ChkOutFileName", filename)
            'AjaxFileUpload1.ContextKeys = "CheckOut"

            '   ReloadGrid()


        Catch ex As Exception
            lblAlertCheckout.Text = ex.Message.ToString
            mdlPopupCheckOut.Show()
            InsertIntoTblWebEventLog("AjaxFileUpload1_UploadComplete", ex.Message.ToString, Session("AssetNo"))

        End Try


    End Sub

    Public Sub SaveUpload()
        Dim temppath As String = Server.MapPath("~/Uploads/Asset/temp/") + Session("AssetNo")
        '  InsertIntoTblWebEventLog("AjaxFileUpload1_UploadCompleteAll", e.FilesUploaded.ToString, Session("AssetNo"))

        For Each foundFile As String In My.Computer.FileSystem.GetFiles(temppath + "/")
            ' InsertIntoTblWebEventLog("AjaxFileUpload1_UploadCompleteAll", Path.GetFileName(foundFile), i.ToString)
            InsertIntoTbwFileUpload(Path.GetFileName(foundFile), foundFile)
        Next

        ReloadGrid()

        mdlPopupCheckOut.Show()

    End Sub

    Protected Sub ReloadGridEmpty()
        SqlDStbwFileUpload.SelectCommand = "select * from tbwassetfileupload where rcno=0"
        SqlDStbwFileUpload.DataBind()
        gvStatusFileList.DataSourceID = "SqlDStbwFileUpload"
        gvStatusFileList.DataBind()
    End Sub

    Protected Sub ReloadGrid()
        SqlDStbwFileUpload.SelectCommand = "select * from tbwassetfileupload where date(createdon)='" + DateTime.Now.ToString("yyyyMMdd") + "' and documenttype='" + Session("StatusType") + "' and fileref='" + Session("AssetNo") + "' and createdby='" + txtCreatedBy.Text + "'"
        SqlDStbwFileUpload.DataBind()
        gvStatusFileList.DataSourceID = "SqlDStbwFileUpload"
        gvStatusFileList.DataBind()
     
        InsertIntoTblWebEventLog("ReloadGrid", gvStatusFileList.Rows.Count.ToString, Session("AssetNo"))
    End Sub

    'Protected Sub AjaxFileUpload2_UploadComplete(sender As Object, e As AjaxControlToolkit.AjaxFileUploadEventArgs) Handles AjaxFileUpload2.UploadComplete
    '    Try
    '        Dim filename As String = System.IO.Path.GetFileName(e.FileName)
    '        filename = Session("AssetNo") + "_" + filename
    '        Dim temppath As String = Server.MapPath("~\Uploads\Asset\temp\") + Session("AssetNo")
    '        Dim path1 As String = Server.MapPath("~\Uploads\Asset\temp\") + Session("AssetNo") + "\" + filename
    '         If Not Directory.Exists(temppath) Then
    '            Directory.CreateDirectory(temppath)
    '        End If
    '        AjaxFileUpload2.SaveAs(path1)

    '        If Session("StatusType") = "CHECKOUT" Then
    '            Session.Add("CheckOutFileExists", "True")
    '        ElseIf Session("StatusType") = "CHECKIN" Then
    '            Session.Add("CheckInFileExists", "True")
    '        End If
    '        InsertIntoTblWebEventLog("AjaxFileUpload2_UploadComplete", path1, Session("AssetNo"))
    '        Session.Add("CheckOutPath", temppath)
    '        'Session.Add("ChkInFileName", filename)
    '        'AjaxFileUpload2.ContextKeys = "CheckIn"
    '        ReloadGrid()

    '    Catch ex As Exception
    '        lblAlertCheckout.Text = ex.Message.ToString
    '        mdlPopupCheckOut.Show()
    '        InsertIntoTblWebEventLog("AjaxFileUpload2_UploadComplete", ex.Message.ToString, Session("AssetNo"))

    '    End Try
    'End Sub

    'Protected Sub AjaxFileUpload1_UploadCompleteAll(sender As Object, e As AjaxControlToolkit.AjaxFileUploadCompleteAllEventArgs) Handles AjaxFileUpload1.UploadCompleteAll
    '    Session.Add("CheckOutFileCount", e.FilesUploaded.ToString)
    '    InsertIntoTblWebEventLog("AjaxFileUpload1_UploadCompleteAll", e.FilesUploaded.ToString, txtAssetNo.ToString)
    'End Sub

    'Protected Sub AjaxFileUpload2_UploadCompleteAll(sender As Object, e As AjaxControlToolkit.AjaxFileUploadCompleteAllEventArgs) Handles AjaxFileUpload2.UploadCompleteAll
    '    Session.Add("CheckInFileCount", e.FilesUploaded.ToString)
    '    InsertIntoTblWebEventLog("AjaxFileUpload2_UploadCompleteAll", e.FilesUploaded.ToString, txtAssetNo.ToString)
    'End Sub

    Protected Sub AjaxFileUpload1_UploadCompleteAll(sender As Object, e As AjaxFileUploadCompleteAllEventArgs) Handles AjaxFileUpload1.UploadCompleteAll
          Dim temppath As String = Server.MapPath("~/Uploads/Asset/temp/") + Session("AssetNo")
        InsertIntoTblWebEventLog("AjaxFileUpload1_UploadCompleteAll", e.FilesUploaded.ToString, Session("AssetNo"))

        For Each foundFile As String In My.Computer.FileSystem.GetFiles(temppath + "/")
            '   InsertIntoTblWebEventLog("AjaxFileUpload1_UploadCompleteAll", Path.GetFileName(foundFile), i.ToString)
            InsertIntoTbwFileUpload(Path.GetFileName(foundFile), foundFile)
        Next




    End Sub

    'Protected Sub AjaxFileUpload1_UploadStart(sender As Object, e As AjaxFileUploadStartEventArgs) Handles AjaxFileUpload1.UploadStart
    '    InsertIntoTblWebEventLog("AjaxFileUpload2_UploadStart", "", Session("AssetNo"))

    'End Sub

    Protected Sub DeleteFile(ByVal sender As Object, ByVal e As EventArgs)
        Try

            'If txtFileDelete.Text = "0" Then
            '    lblAlert.Text = "FILE DELETION ACCESS RIGHT HAS NOT BEEN PROVIDED"
            '    'MaintainScrollPositionOnPostBack = "true"
            '    'Me.LoadPage.MaintainScrollPositionOnPostBack = True
            '    Me.MaintainScrollPositionOnPostBack = False
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            '    Me.MaintainScrollPositionOnPostBack = True
            '    Exit Sub
            'End If

            Dim filePath As String = CType(sender, LinkButton).CommandArgument
            txtFileLink.Text = filePath

            filePath = Server.MapPath("~/Uploads/Asset/") + filePath

            txtDeleteUploadedFile.Text = filePath
            iframeid.Attributes.Add("src", "about:blank")

            Dim grdrow As GridViewRow = CType((CType(sender, LinkButton)).NamingContainer, GridViewRow)
            lblDeleteConfirm.Text = "File"
            '  lblEvent.Text = "Confirm DELETE"
            lblQuery.Text = "Are you sure to DELETE the File? <br><br> File Name : " + txtFileLink.Text + "<br><br>File Description : " + grdrow.Cells(1).Text
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
            InsertIntoTblWebEventLog("DeleteFile", ex.Message.ToString, txtAssetNo.Text)
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Private Sub DeleteUploadedfile(gFilePath As String)
        'File.Delete(gFilePath)
        ''  Response.Redirect(Request.Url.AbsoluteUri)
        'lblMessage.Text = "FILE DELETED"
        'Dim myDir As New DirectoryInfo(Server.MapPath("~/Uploads/Service/"))
        ''   Dim filesInDir As FileInfo() = myDir.GetFiles((Convert.ToString(txtAccountID.Text + "_")) + "*.*")
        'Dim filesInDir As FileInfo() = myDir.GetFiles((Convert.ToString(txtSvcRecord.Text + "_")) + "*.*")
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
        Try
            ' File.Delete(gFilePath)
            Dim deletefilepath = Server.MapPath("~/Uploads/Asset/DeletedFiles/") + txtFileLink.Text
            File.Move(gFilePath, deletefilepath)
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

                '  MessageBox.Message.Alert(Page, "Record deleted successfully!!!", "str")
                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "FILEUPLOAD", txtFileLink.Text, "DELETE", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtAssetNo.Text)
            End If
            conn.Close()
            lblMessage.Text = "FILE DELETED"

            BindFileGrid(lblMovementRcno.Text)


        Catch ex As Exception
            InsertIntoTblWebEventLog("DeleteUploadedfile", ex.Message.ToString, gFilePath)
        End Try
    End Sub

   
    'Protected Sub btnRefreshGrid_Click(sender As Object, e As ImageClickEventArgs) Handles btnRefreshGrid.Click
    '    ReloadGrid()
    '    mdlPopupCheckOut.Show()

    'End Sub

    Protected Sub grdMovement_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grdMovement.SelectedIndexChanged
        Dim editindex As Integer = grdMovement.SelectedIndex
        rcno = DirectCast(grdMovement.Rows(editindex).FindControl("Label1"), Label).Text
        lblMovementRcno.Text = rcno

        If grdMovement.SelectedRow.Cells(1).Text = "&nbsp;" Then
            lblMovementType.Text = ""
        Else
            lblMovementType.Text = Server.HtmlDecode(grdMovement.SelectedRow.Cells(1).Text).ToString
        End If

        BindFileGrid(rcno)

    End Sub

    Protected Sub BindFileGrid(rcno As String)
        SqlDSUpload.SelectCommand = "select * from tblfileupload where filegroup='ASSET' AND filenamelink like '" + rcno + "\_%'"
        SqlDSUpload.DataBind()
        gvUpload.DataBind()
    End Sub

    Protected Sub btnRefreshFilesGrid_Click(sender As Object, e As EventArgs) Handles btnRefreshFilesGrid.Click
        ReloadGrid()
        mdlPopupCheckOut.Show()
    End Sub

    Protected Sub ddlBrand_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBrand.SelectedIndexChanged
        'SqlDSModel.SelectCommand = "SELECT AssetModel FROM tblassetModel where assetbrand='" & ddlBrand.Text & "' ORDER BY AssetModel"
        'SqlDSModel.DataBind()
        'ddlModel.DataBind()
     
        ddlModel.Items.Clear()
        ddlModel.Items.Add("--SELECT--")
        '   ddlGender.Items.Add("N.A")

        PopulateDropDownList("SELECT AssetModel FROM tblassetModel WHERE ASSETBRAND='" & ddlBrand.Text & "' ORDER BY AssetModel", "AssetModel", "AssetModel", ddlModel)


    End Sub

    Protected Sub btnImportFromExcel_Click(sender As Object, e As EventArgs) Handles btnImportFromExcel.Click
        lblAlertImportExcel.Text = ""
        lblMessageImportExcel.Text = ""
        If FileUpload1.HasFile Then
            FileUpload1 = Nothing

        End If


        GridView2.DataSource = ""
        GridView2.DataBind()

        mdlPopupImportExcel.Show()

    End Sub

    Protected Sub btnImportExcelUpload_Click(sender As Object, e As EventArgs) Handles btnImportExcelUpload.Click
       
        lblAlertImportExcel.Text = ""
        lblMessageImportExcel.Text = ""

        If FileUpload1.HasFile = False Then
            lblAlertImportExcel.Text = "CHOOSE FILE TO UPLOAD"
            mdlPopupImportExcel.Show()
            Exit Sub
        End If

        Dim ofilename As String = ""
        Dim sfilename As String = ""
        '   InsertIntoTblWebEventLog("btnImportExcelUpload_Click", "11", "TEST")

        ofilename = Path.GetFileName(FileUpload1.PostedFile.FileName)
        '   InsertIntoTblWebEventLog("btnImportExcelUpload_Click", "11", ofilename)

        Dim ext As String = Path.GetExtension(ofilename)
        sfilename = ofilename.Split("."c)(0)

        '   InsertIntoTblWebEventLog("btnImportExcelUpload_Click", "1", sfilename)

        Dim folderPath As String = Server.MapPath("~/Uploads/Excel/")
        If Not Directory.Exists(folderPath) Then
            Directory.CreateDirectory(folderPath)
        End If
        Dim fileName As String = folderPath + sfilename + "_" + DateTime.Now.ToString("yyyyMMddhhmm") + "_" + txtCreatedBy.Text + ext
        '   InsertIntoTblWebEventLog("btnImportExcelUpload_Click", "2", fileName)

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
            InsertAssetData(dt, conn)

            lblMessageImportExcel.Text = "Total Records Imported : " + count + "<br>" + " Success : " + txtSuccessCount.Text + ", Failure : " + txtFailureCount.Text '+ " Failed AccountID : " + txtFailureString.Text
            'If GridView1.Rows.Count > 0 Then
            '    btnExportToExcel.Visible = True

            'End If
            conn.Close()
            conn.Dispose()

            txt.Text = "SELECT * FROM TBLASSET WHERE RCNO<>0"
            txt.Text = txt.Text + " and AssetGrp in (select AssetGroup from tblassetgroupaccess where GroupAccess = '" & Session("SecGroupAuthority") & "') ORDER BY RCNO desc"

            SqlDataSource1.SelectCommand = txt.Text
            SqlDataSource1.DataBind()
            GridView1.DataBind()

            If GridView1.Rows.Count > 0 Then
                txtRcno.Text = DirectCast(GridView1.Rows(0).FindControl("Label1"), Label).Text

                If String.IsNullOrEmpty(txtRcno.Text) = False Then

                    PopulateRecord(txtRcno.Text)
                End If

                GridView1.SelectedIndex = 0

            End If

        End If
        mdlPopupImportExcel.Show()

    End Sub

    Protected Function InsertAssetData(dt As DataTable, conn As MySqlConnection) As Boolean
        Dim Success As Int32 = 0
        Dim Failure As Int32 = 0
        Dim FailureString As String = ""

        Dim dtLog As New DataTable
'  Try
            Dim qry As String = "INSERT INTO tblasset(AssetNo,AssetGrp,AssetClass,AssetRegNo,Status,OpStatus,Descrip,DefAddr,Depmtd,PurfmCType,"
            qry = qry + "PurfmCCode,PurfmInvNo,PurchRef,PurchDt,PurchVal,ObbkVal,ObbkDate,CurrAddr,InCharge,CurrCont,Phone1,Phone2,EstDateIn,"
            qry = qry + "Notes,ActDateIn,LastMoveId,Project,DateOut,Purpose,AssetCode,Make,Model,Year,Capacity,EngineNo,Value,Remarks,PurfmCName,"
            qry = qry + "ReferenceOur,SvcFreq,NextSvcDate,LastSvcDate,SvcBy,DWM,SoldToCoID,SoldToCoName,SoldVal,SoldRef,SoldDate,DisposedRef,"
            qry = qry + "DisposedDate,AuthorBy,Desc1,AltNo,MfgYear,CustCode,CustName,Reference,RefDate,PriceCode,PROJECTCODE,PROJECTNAME,Cost,"
            qry = qry + "SupplierCode,SupplierName,LocCode,ValueDate,SoldBy,MarketValue,ScrapToCoID,ScrapToCoName,ScrapVal,ScrapRef,ScrapDate,"
            qry = qry + "ScrapBy,EngineBrand,EngineModel,ArNo,DueDate,TestNo,TestRemarks,CertType,RentalYN,InChargeID,SelfOwnYN,PurchBy,CostOther,"
            qry = qry + "IncomeOther,DeprDur,DeprMonthly,DeprEnd,EstLife,DeprOps,CostBillPct,PurchOVal,PurchExRate,PurchCurr,RoadTaxExpiry,CoeExpiry,"
            qry = qry + "InspectDate,VpcExpiry,VpcNo,PaymentType,MarketCost,CostDate,GroupID,GroupName,SaleableYN,FinCoID,FinCoName,FinDtFrom,"
            qry = qry + "FinDtTo,GltypeSales,GltypePurchase,LedgercodeSales,LedgercodePurchase,ContactType,TechnicalSpecs,type,CapacityUnitMS,"
            qry = qry + "RegDate,AgmtNo,LoanAmt,NoInst,IntRate,TermCharges,FirstInst,LastInst,MthlyInst,SubLedgercodeSales,SubLedgercodePurchase,"
            qry = qry + "GPSLabel,UploadDate,DelGoogleCalendar,GoogleEmail,GooglePassword,ColourCodeHtml,ColourCodeRGB, CreatedBy,CreatedOn,"
        qry = qry + "LastModifiedBy,LastModifiedOn,Location,SerialNo,Color,Warranty,WarrantyMS,WarrantyEnd,IMEI,StatusDesc)"
            qry = qry + "VALUES(@AssetNo,@AssetGrp,@AssetClass,@AssetRegNo,@Status,@OpStatus,@Descrip,@DefAddr,@Depmtd,@PurfmCType,@PurfmCCode,@PurfmInvNo,@PurchRef,@PurchDt,"
            qry = qry + "@PurchVal,@ObbkVal,@ObbkDate,@CurrAddr,@InCharge,@CurrCont,@Phone1,@Phone2,@EstDateIn,@Notes,@ActDateIn,@LastMoveId,@Project,@DateOut,@Purpose,"
            qry = qry + "@AssetCode,@Make,@Model,@Year,@Capacity,@EngineNo,@Value,@Remarks,@PurfmCName,@ReferenceOur,@SvcFreq,@NextSvcDate,@LastSvcDate,@SvcBy,@DWM,"
            qry = qry + "@SoldToCoID,@SoldToCoName,@SoldVal,@SoldRef,@SoldDate,@DisposedRef,@DisposedDate,@AuthorBy,@Desc1,@AltNo,@MfgYear,@CustCode,@CustName,@Reference,"
            qry = qry + "@RefDate,@PriceCode,@PROJECTCODE,@PROJECTNAME,@Cost,@SupplierCode,@SupplierName,@LocCode,@ValueDate,@SoldBy,@MarketValue,@ScrapToCoID,@ScrapToCoName,"
            qry = qry + "@ScrapVal,@ScrapRef,@ScrapDate,@ScrapBy,@EngineBrand,@EngineModel,@ArNo,@DueDate,@TestNo,@TestRemarks,@CertType,@RentalYN,@InChargeID,@SelfOwnYN,"
            qry = qry + "@PurchBy,@CostOther,@IncomeOther,@DeprDur,@DeprMonthly,@DeprEnd,@EstLife,@DeprOps,@CostBillPct,@PurchOVal,@PurchExRate,@PurchCurr,@RoadTaxExpiry,"
            qry = qry + "@CoeExpiry,@InspectDate,@VpcExpiry,@VpcNo,@PaymentType,@MarketCost,@CostDate,@GroupID,@GroupName,@SaleableYN,@FinCoID,@FinCoName,@FinDtFrom,@FinDtTo,"
            qry = qry + "@GltypeSales,@GltypePurchase,@LedgercodeSales,@LedgercodePurchase,@ContactType,@TechnicalSpecs,@type,@CapacityUnitMS,@RegDate,@AgmtNo,@LoanAmt,"
            qry = qry + "@NoInst,@IntRate,@TermCharges,@FirstInst,@LastInst,@MthlyInst,@SubLedgercodeSales,@SubLedgercodePurchase,@GPSLabel,@UploadDate,@DelGoogleCalendar,"
        qry = qry + "@GoogleEmail,@GooglePassword,@ColourCodeHtml,@ColourCodeRGB,@CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn,@Location,@SerialNo,@Color,@Warranty,@WarrantyMS,@WarrantyEnd,@IMEI,@StatusDesc);"

        Dim SerialNo As String = ""
            Dim Exists As Boolean = True


            InsertIntoTblWebEventLog("InsertAssetData", dt.Rows.Count.ToString, "")

            Dim drow As DataRow
        Dim dc1 As DataColumn = New DataColumn("SerialNo", GetType(String))
            Dim dc2 As DataColumn = New DataColumn("AssetGroup", GetType(String))
            Dim dc3 As DataColumn = New DataColumn("Status", GetType(String))
            Dim dc4 As DataColumn = New DataColumn("Remarks", GetType(String))
            dtLog.Columns.Add(dc1)
            dtLog.Columns.Add(dc2)
            dtLog.Columns.Add(dc3)
            dtLog.Columns.Add(dc4)

            For Each r As DataRow In dt.Rows

                drow = dtLog.NewRow()

            If IsDBNull(r("SerialNo")) Then
                Failure = Failure + 1
                FailureString = FailureString + " " + SerialNo + "(SERIALNO_BLANK)"
                drow("Status") = "Failed"
                drow("Remarks") = "SERIALNO_BLANK"
                dtLog.Rows.Add(drow)
                Continue For
            Else
                InsertIntoTblWebEventLog("InsertData1", dt.Rows.Count.ToString, r("SerialNo"))

                SerialNo = r("SerialNo")
                drow("SerialNo") = SerialNo

            End If

            '    Dim command2 As MySqlCommand = New MySqlCommand

            '    command2.CommandType = CommandType.Text

            '    command2.CommandText = "SELECT * FROM tblasset where assetno=@id"
            '    command2.Parameters.AddWithValue("@id", AssetNo)
            '    command2.Connection = conn

            '    Dim dr1 As MySqlDataReader = command2.ExecuteReader()
            '    Dim dt1 As New System.Data.DataTable
            '    dt1.Load(dr1)

            '    If dt1.Rows.Count > 0 Then

            '        '  MessageBox.Message.Alert(Page, "Account ID already exists!!!", "str")
            '        ' lblAlert.Text = "Account ID already exists!!!"
            '        Failure = Failure + 1
            '        FailureString = FailureString + " " + AssetNo + "(DUPLICATE)"
            '        drow("Status") = "Failed"
            '        drow("Remarks") = "ASSETNO_DUPLICATE"
            '        If IsDBNull(r("AssetGroup")) = False Then
            '            drow("AssetGroup") = r("AssetGroup")
            '        End If
            '        dtLog.Rows.Add(drow)

            'Else

            InsertIntoTblWebEventLog("AssetImport1", r("SerialNo"), "")

            Dim command3 As MySqlCommand = New MySqlCommand

            command3.CommandType = CommandType.Text

            command3.CommandText = "SELECT * FROM tblasset where SerialNo=@sno AND IMEI=@imei"
            command3.Parameters.AddWithValue("@sno", r("SerialNo"))
            If IsDBNull(r("IMEI")) Then
                command3.Parameters.AddWithValue("@imei", "")
            Else
                command3.Parameters.AddWithValue("@imei", r("IMEI"))
            End If

            command3.Connection = conn

            Dim dr3 As MySqlDataReader = command3.ExecuteReader()
            Dim dt3 As New System.Data.DataTable
            dt3.Load(dr3)

            InsertIntoTblWebEventLog("AssetImport", r("SerialNo"), "")

            If dt3.Rows.Count > 0 Then

                '  MessageBox.Message.Alert(Page, "Account ID already exists!!!", "str")
                ' lblAlert.Text = "Account ID already exists!!!"
                Failure = Failure + 1
                FailureString = FailureString + " " + SerialNo + "(DUPLICATE)"
                drow("Status") = "Failed"
                drow("Remarks") = "ASSET ALREADY EXISTS"
                If IsDBNull(r("AssetGroup")) = False Then
                    drow("AssetGroup") = r("AssetGroup")
                End If
                dtLog.Rows.Add(drow)

            Else

                'Check for dropdownlist values, if it exists

                If IsDBNull(r("AssetGroup")) = False Then
                    If CheckAssetGroupExists(r("AssetGroup"), conn) = False Then

                        Failure = Failure + 1
                        FailureString = FailureString + " " + SerialNo + "(ASSETGROUP DOES NOT EXIST)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "ASSET GROUP DOES NOT EXIST"
                        'If IsDBNull(r("AssetGroup")) = False Then
                        '    drow("AssetGroup") = r("AssetGroup")
                        'End If
                        dtLog.Rows.Add(drow)
                        Continue For
                    End If
                End If

                If IsDBNull(r("AssetClass")) = False Then
                    If CheckAssetClassExists(r("AssetClass"), conn) = False Then

                        Failure = Failure + 1
                        FailureString = FailureString + " " + SerialNo + "(ASSETCLASS DOES NOT EXIST)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "ASSET CLASS DOES NOT EXIST"
                        If IsDBNull(r("AssetGroup")) = False Then
                            drow("AssetGroup") = r("AssetGroup")
                        End If
                        dtLog.Rows.Add(drow)
                        Continue For
                    End If
                End If

                If IsDBNull(r("AssetBrand")) = False Then
                    If CheckAssetBrandExists(r("AssetBrand"), conn) = False Then

                        Failure = Failure + 1
                        FailureString = FailureString + " " + SerialNo + "(ASSETBRAND DOES NOT EXIST)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "ASSET BRAND DOES NOT EXIST"
                        If IsDBNull(r("AssetGroup")) = False Then
                            drow("AssetGroup") = r("AssetGroup")
                        End If
                        dtLog.Rows.Add(drow)
                        Continue For
                    End If
                End If

                If IsDBNull(r("AssetModel")) = False Then
                    If CheckAssetModelExists(r("AssetBrand"), r("AssetModel"), conn) = False Then

                        Failure = Failure + 1
                        FailureString = FailureString + " " + SerialNo + "(ASSETMODEL DOES NOT EXIST)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "ASSET MODEL DOES NOT EXIST"
                        If IsDBNull(r("AssetGroup")) = False Then
                            drow("AssetGroup") = r("AssetGroup")
                        End If
                        dtLog.Rows.Add(drow)
                        Continue For
                    End If
                End If

                If IsDBNull(r("AssetColor")) = False Then
                    If CheckAssetColorExists(r("AssetColor"), conn) = False Then

                        Failure = Failure + 1
                        FailureString = FailureString + " " + SerialNo + "(ASSETCOLOR DOES NOT EXIST)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "ASSET COLOR DOES NOT EXIST"
                        If IsDBNull(r("AssetGroup")) = False Then
                            drow("AssetGroup") = r("AssetGroup")
                        End If
                        dtLog.Rows.Add(drow)
                        Continue For
                    End If
                End If

                If IsDBNull(r("SupplierCode")) = False Then
                    If CheckSupplierExists(r("SupplierCode"), r("SupplierName"), conn) = False Then

                        Failure = Failure + 1
                        FailureString = FailureString + " " + SerialNo + "(SUPPLIER DOES NOT EXIST)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "SUPPLIER DOES NOT EXIST"
                        If IsDBNull(r("AssetGroup")) = False Then
                            drow("AssetGroup") = r("AssetGroup")
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

                'If IsDBNull(r("AssetNo")) Then
                '    Failure = Failure + 1
                '    FailureString = FailureString + " " + SerialNo + "(ASSETNO_BLANK)"
                '    drow("Status") = "Failed"
                '    drow("Remarks") = "ASSETNO_BLANK"
                '    If IsDBNull(r("AssetGroup")) = False Then
                '        drow("AssetGroup") = r("AssetGroup")
                '    End If
                '    dtLog.Rows.Add(drow)

                '    Continue For

                'Else
                '    cmd.Parameters.AddWithValue("@AssetNo", r("AssetNo").ToString.ToUpper)
                'End If

                If IsDBNull(r("AssetGroup")) Then
                    Failure = Failure + 1
                    FailureString = FailureString + " " + SerialNo + "(ASSETGROUP_BLANK)"
                    drow("Status") = "Failed"
                    drow("Remarks") = "ASSETGROUP_BLANK"
                    dtLog.Rows.Add(drow)
                    Continue For
                Else
                    drow("AssetGroup") = r("AssetGroup")
                    cmd.Parameters.AddWithValue("@AssetGrp", r("AssetGroup").ToString.ToUpper)
                End If

                If txtDisplayRecordsLocationwise.Text = "Y" Then
                    If IsDBNull(r("Branch")) Then
                        Failure = Failure + 1
                        FailureString = FailureString + " " + SerialNo + "(BRANCH_BLANK)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "BRANCH_BLANK"
                        dtLog.Rows.Add(drow)
                        Continue For
                    Else
                        drow("Branch") = r("Branch")
                        cmd.Parameters.AddWithValue("@Location", r("Branch").ToString.ToUpper)
                    End If
                Else
                    cmd.Parameters.AddWithValue("@Location", "")
                End If


                cmd.Parameters.AddWithValue("@Status", "1")
                cmd.Parameters.AddWithValue("@StatusDesc", "AVAILABLE")
                cmd.Parameters.AddWithValue("@OpStatus", "")
                'End If
                If IsDBNull(r("SerialNo")) Then
                    cmd.Parameters.AddWithValue("@SerialNo", "")
                Else
                    cmd.Parameters.AddWithValue("@SerialNo", r("SerialNo").ToString)
                End If

                If IsDBNull(r("IMEI")) Then
                    cmd.Parameters.AddWithValue("@IMEI", "")
                Else
                    cmd.Parameters.AddWithValue("@IMEI", r("IMEI").ToString)
                End If


                If IsDBNull(r("AssetClass")) Then
                    cmd.Parameters.AddWithValue("@AssetClass", "")
                Else
                    cmd.Parameters.AddWithValue("@AssetClass", r("AssetClass").ToString)
                End If
                If IsDBNull(r("AssetBrand")) Then
                    cmd.Parameters.AddWithValue("@Make", "")
                Else
                    cmd.Parameters.AddWithValue("@Make", r("AssetBrand").ToString)
                End If
                If IsDBNull(r("AssetModel")) Then
                    cmd.Parameters.AddWithValue("@Model", "")
                Else
                    cmd.Parameters.AddWithValue("@Model", r("AssetModel").ToString)
                End If
                If IsDBNull(r("AssetColor")) Then
                    cmd.Parameters.AddWithValue("@Color", "")
                Else
                    cmd.Parameters.AddWithValue("@Color", r("AssetColor").ToString)
                End If
                If IsDBNull(r("AssetRegNo")) Then
                    cmd.Parameters.AddWithValue("@AssetRegNo", "")
                Else
                    cmd.Parameters.AddWithValue("@AssetRegNo", r("AssetRegNo").ToString)
                End If

                If IsDBNull(r("Description")) Then
                    Failure = Failure + 1
                    FailureString = FailureString + " " + SerialNo + "(DESCRIPTION_BLANK)"
                    drow("Status") = "Failed"
                    drow("Remarks") = "DESCRIPTION_BLANK"
                    dtLog.Rows.Add(drow)
                    Continue For
                    cmd.Parameters.AddWithValue("@Descrip", "")
                Else
                    cmd.Parameters.AddWithValue("@Descrip", r("Description").ToString)
                End If

                If IsDBNull(r("SupplierCode")) Then
                    cmd.Parameters.AddWithValue("@SupplierCode", "")
                Else
                    cmd.Parameters.AddWithValue("@SupplierCode", r("SupplierCode").ToString)
                End If
                If IsDBNull(r("SupplierName")) Then
                    cmd.Parameters.AddWithValue("@SupplierName", "")
                Else
                    cmd.Parameters.AddWithValue("@SupplierName", r("SupplierName").ToString)
                End If
                If IsDBNull(r("PurchaseDate")) Then
                    cmd.Parameters.AddWithValue("@PurchDt", "")
                Else
                    cmd.Parameters.AddWithValue("@PurchDt", Convert.ToDateTime(r("PurchaseDate")).ToString("yyyy-MM-dd"))
                End If

                If IsDBNull(r("PONo")) Then
                    cmd.Parameters.AddWithValue("@PurchRef", "")
                Else
                    cmd.Parameters.AddWithValue("@PurchRef", r("PONo").ToString)
                End If
                If IsDBNull(r("PurchasePrice")) Then
                    cmd.Parameters.AddWithValue("@PurchVal", 0)
                Else
                    cmd.Parameters.AddWithValue("@PurchVal", Convert.ToDecimal(r("PurchasePrice")))
                End If
                If IsDBNull(r("Warranty")) Then
                    cmd.Parameters.AddWithValue("@Warranty", 0)
                Else
                    Dim d As Double
                    If Double.TryParse(r("Warranty"), d) = False Then
                        Failure = Failure + 1
                        FailureString = FailureString + " " + SerialNo + "(WARRANTY_NOTINNUMBERS)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "WARRANTY SHOULD BE IN NUMBERS"
                        dtLog.Rows.Add(drow)
                        Continue For

                    End If
                    cmd.Parameters.AddWithValue("@Warranty", Convert.ToInt16(r("Warranty")))
                End If
                If IsDBNull(r("WarrantyMS")) Then
                    cmd.Parameters.AddWithValue("@WarrantyMS", "")
                Else
                    cmd.Parameters.AddWithValue("@WarrantyMS", r("WarrantyMS").ToString)
                End If

                If IsDBNull(r("WarrantyEndDate")) Then
                    cmd.Parameters.AddWithValue("@WarrantyEnd", "")
                Else
                    cmd.Parameters.AddWithValue("@WarrantyEnd", Convert.ToDateTime(r("WarrantyEndDate")).ToString("yyyy-MM-dd"))
                End If
                If IsDBNull(r("Notes")) Then
                    cmd.Parameters.AddWithValue("@Notes", "")
                Else
                    cmd.Parameters.AddWithValue("@Notes", r("Notes").ToString)
                End If
                cmd.Parameters.AddWithValue("@DefAddr", "")
                cmd.Parameters.AddWithValue("@Depmtd", "")
                cmd.Parameters.AddWithValue("@PurfmCType", "")
                cmd.Parameters.AddWithValue("@PurfmCCode", "")
                cmd.Parameters.AddWithValue("@PurfmInvNo", "")

                cmd.Parameters.AddWithValue("@ObbkVal", 0)
                cmd.Parameters.AddWithValue("@ObbkDate", DBNull.Value)
                cmd.Parameters.AddWithValue("@CurrAddr", "")
                cmd.Parameters.AddWithValue("@InCharge", "")
                cmd.Parameters.AddWithValue("@CurrCont", "")
                cmd.Parameters.AddWithValue("@Phone1", "")
                cmd.Parameters.AddWithValue("@Phone2", "")
                cmd.Parameters.AddWithValue("@EstDateIn", DBNull.Value)

                cmd.Parameters.AddWithValue("@ActDateIn", DBNull.Value)
                cmd.Parameters.AddWithValue("@LastMoveId", "")
                cmd.Parameters.AddWithValue("@Project", "")

                cmd.Parameters.AddWithValue("@DateOut", DBNull.Value)
                cmd.Parameters.AddWithValue("@Purpose", "")
                cmd.Parameters.AddWithValue("@AssetCode", "")
                cmd.Parameters.AddWithValue("@Year", "")
                cmd.Parameters.AddWithValue("@Capacity", "")
                cmd.Parameters.AddWithValue("@EngineNo", "")
                cmd.Parameters.AddWithValue("@Value", 0)
                cmd.Parameters.AddWithValue("@Remarks", "")
                cmd.Parameters.AddWithValue("@PurfmCName", "")
                cmd.Parameters.AddWithValue("@ReferenceOur", "")
                cmd.Parameters.AddWithValue("@SvcFreq", 0)

                cmd.Parameters.AddWithValue("@NextSvcDate", DBNull.Value)
                cmd.Parameters.AddWithValue("@LastSvcDate", DBNull.Value)
                cmd.Parameters.AddWithValue("@SvcBy", "")
                cmd.Parameters.AddWithValue("@DWM", "")
                cmd.Parameters.AddWithValue("@SoldToCoID", "")
                cmd.Parameters.AddWithValue("@SoldToCoName", "")
                cmd.Parameters.AddWithValue("@SoldVal", 0)
                cmd.Parameters.AddWithValue("@SoldRef", "")
                cmd.Parameters.AddWithValue("@SoldDate", DBNull.Value)
                cmd.Parameters.AddWithValue("@DisposedRef", "")
                cmd.Parameters.AddWithValue("@DisposedDate", DBNull.Value)
                cmd.Parameters.AddWithValue("@AuthorBy", "")
                cmd.Parameters.AddWithValue("@Desc1", "")
                cmd.Parameters.AddWithValue("@AltNo", "")
                cmd.Parameters.AddWithValue("@MfgYear", "")
                cmd.Parameters.AddWithValue("@CustCode", "")
                cmd.Parameters.AddWithValue("@CustName", "")
                cmd.Parameters.AddWithValue("@Reference", "")

                cmd.Parameters.AddWithValue("@RefDate", DBNull.Value)
                cmd.Parameters.AddWithValue("@PriceCode", "")
                cmd.Parameters.AddWithValue("@PROJECTCODE", "")
                cmd.Parameters.AddWithValue("@PROJECTName", "")
                cmd.Parameters.AddWithValue("@Cost", 0)

                cmd.Parameters.AddWithValue("@LocCode", "")
                cmd.Parameters.AddWithValue("@ValueDate", DBNull.Value)
                cmd.Parameters.AddWithValue("@SoldBy", "")
                cmd.Parameters.AddWithValue("@MarketValue", 0)
                cmd.Parameters.AddWithValue("@ScrapToCoID", "")
                cmd.Parameters.AddWithValue("@ScrapToCoName", "")
                cmd.Parameters.AddWithValue("@ScrapVal", 0)
                cmd.Parameters.AddWithValue("@ScrapRef", "")
                cmd.Parameters.AddWithValue("@ScrapDate", DBNull.Value)
                cmd.Parameters.AddWithValue("@ScrapBy", "")
                cmd.Parameters.AddWithValue("@EngineBrand", "")
                cmd.Parameters.AddWithValue("@EngineModel", "")
                cmd.Parameters.AddWithValue("@ArNo", "")
                cmd.Parameters.AddWithValue("@DueDate", DBNull.Value)
                cmd.Parameters.AddWithValue("@TestNo", "")
                cmd.Parameters.AddWithValue("@TestRemarks", "")
                cmd.Parameters.AddWithValue("@CertType", "")
                cmd.Parameters.AddWithValue("@RentalYN", "")

                cmd.Parameters.AddWithValue("@InChargeID", "")
                cmd.Parameters.AddWithValue("@SelfOwnYN", "")
                cmd.Parameters.AddWithValue("@PurchBy", "")
                cmd.Parameters.AddWithValue("@CostOther", 0)
                cmd.Parameters.AddWithValue("@IncomeOther", 0)
                cmd.Parameters.AddWithValue("@DeprDur", 0)
                cmd.Parameters.AddWithValue("@DeprMonthly", 0)
                cmd.Parameters.AddWithValue("@DeprEnd", DBNull.Value)
                cmd.Parameters.AddWithValue("@EstLife", 0)
                cmd.Parameters.AddWithValue("@DeprOps", 0)
                cmd.Parameters.AddWithValue("@CostBillPct", 0)
                cmd.Parameters.AddWithValue("@PurchOVal", 0)
                cmd.Parameters.AddWithValue("@PurchExRate", 0)
                cmd.Parameters.AddWithValue("@PurchCurr", "")
                cmd.Parameters.AddWithValue("@RoadTaxExpiry", DBNull.Value)
                cmd.Parameters.AddWithValue("@CoeExpiry", DBNull.Value)
                cmd.Parameters.AddWithValue("@InspectDate", DBNull.Value)
                cmd.Parameters.AddWithValue("@VpcExpiry", DBNull.Value)
                cmd.Parameters.AddWithValue("@VpcNo", "")
                cmd.Parameters.AddWithValue("@PaymentType", "")
                cmd.Parameters.AddWithValue("@MarketCost", 0)
                cmd.Parameters.AddWithValue("@CostDate", DBNull.Value)
                cmd.Parameters.AddWithValue("@GroupID", "")
                cmd.Parameters.AddWithValue("@GroupName", "")
                cmd.Parameters.AddWithValue("@SaleableYN", "")
                cmd.Parameters.AddWithValue("@FinCoID", "")
                cmd.Parameters.AddWithValue("@FinCoName", "")
                cmd.Parameters.AddWithValue("@FinDtFrom", DBNull.Value)
                cmd.Parameters.AddWithValue("@FinDtTo", DBNull.Value)
                cmd.Parameters.AddWithValue("@GltypeSales", "")
                cmd.Parameters.AddWithValue("@GltypePurchase", "")
                cmd.Parameters.AddWithValue("@LedgercodeSales", "")
                cmd.Parameters.AddWithValue("@LedgercodePurchase", "")
                cmd.Parameters.AddWithValue("@ContactType", "")
                cmd.Parameters.AddWithValue("@TechnicalSpecs", "")
                cmd.Parameters.AddWithValue("@type", "")
                cmd.Parameters.AddWithValue("@CapacityUnitMS", "")
                cmd.Parameters.AddWithValue("@RegDate", DBNull.Value)

                cmd.Parameters.AddWithValue("@AgmtNo", "")
                cmd.Parameters.AddWithValue("@LoanAmt", 0)
                cmd.Parameters.AddWithValue("@NoInst", 0)
                cmd.Parameters.AddWithValue("@IntRate", 0)
                cmd.Parameters.AddWithValue("@TermCharges", 0)
                cmd.Parameters.AddWithValue("@FirstInst", 0)
                cmd.Parameters.AddWithValue("@LastInst", 0)
                cmd.Parameters.AddWithValue("@MthlyInst", 0)
                cmd.Parameters.AddWithValue("@SubLedgercodeSales", "")
                cmd.Parameters.AddWithValue("@SubLedgercodePurchase", "")
                cmd.Parameters.AddWithValue("@GPSLabel", "")
                cmd.Parameters.AddWithValue("@UploadDate", DBNull.Value)
                cmd.Parameters.AddWithValue("@DelGoogleCalendar", 0)
                cmd.Parameters.AddWithValue("@GoogleEmail", "")
                cmd.Parameters.AddWithValue("@GooglePassword", "")
                cmd.Parameters.AddWithValue("@ColourCodeHtml", "")
                cmd.Parameters.AddWithValue("@ColourCodeRGB", "")


                GenerateAssetNo()
                cmd.Parameters.AddWithValue("@AssetNo", txtAssetNo.Text.ToUpper)
                If txtAssetNo.Text = "" Then
                    '  MessageBox.Message.Alert(Page, "Asset No cannot be blank!!!", "str")
                    lblAlert.Text = "ASSET NO CANNOT BE BLANK"
                    Return False

                End If

                cmd.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text + "_IMPORT")
                cmd.Parameters.AddWithValue("@LastModifiedBy", txtCreatedBy.Text + "_IMPORT")
                cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                cmd.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                cmd.Connection = conn

                cmd.ExecuteNonQuery()
                cmd.Dispose()
                txtAssetNo.Text = ""

                Success = Success + 1
                drow("Status") = "Success"
                drow("Remarks") = ""
                dtLog.Rows.Add(drow)
            End If
            ' End If

        Next
            txtSuccessCount.Text = Success.ToString
            txtFailureCount.Text = Failure.ToString
            txtFailureString.Text = FailureString

            GridView2.DataSource = dtLog
        GridView2.DataBind()

            dt.Clear()

        Return True
        'Catch ex As Exception
        '    txtSuccessCount.Text = Success.ToString
        '    txtFailureCount.Text = Failure.ToString
        '    txtFailureString.Text = FailureString
        '    lblAlertImportExcel.Text = ex.Message.ToString


        '    InsertIntoTblWebEventLog("InsertAssetData", ex.Message.ToString, txtCreatedBy.Text)

        '    Return False

        'End Try
    End Function

    Private Function CheckAssetGroupExists(AssetGroup As String, conn As MySqlConnection) As Boolean
        Dim cmd As MySqlCommand = New MySqlCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT EXISTS(SELECT AssetGroup FROM tblassetgroup where AssetGroup =@AssetGroup)"
        cmd.Connection = conn
        cmd.Parameters.Clear()
        cmd.Parameters.AddWithValue("@AssetGroup", AssetGroup)
        Return cmd.ExecuteScalar
    End Function

    Private Function CheckAssetClassExists(AssetClass As String, conn As MySqlConnection) As Boolean
        Dim cmd As MySqlCommand = New MySqlCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT EXISTS(SELECT AssetClass FROM tblassetClass where AssetClass =@AssetClass)"
        cmd.Connection = conn
        cmd.Parameters.Clear()
        cmd.Parameters.AddWithValue("@AssetClass", AssetClass)
        Return cmd.ExecuteScalar
    End Function

    Private Function CheckAssetBrandExists(AssetBrand As String, conn As MySqlConnection) As Boolean
        Dim cmd As MySqlCommand = New MySqlCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT EXISTS(SELECT AssetBrand FROM tblassetBrand where AssetBrand =@AssetBrand)"
        cmd.Connection = conn
        cmd.Parameters.Clear()
        cmd.Parameters.AddWithValue("@AssetBrand", AssetBrand)
        Return cmd.ExecuteScalar
    End Function

    Private Function CheckAssetModelExists(AssetBrand As String, AssetModel As String, conn As MySqlConnection) As Boolean
        Dim cmd As MySqlCommand = New MySqlCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT EXISTS(SELECT AssetModel FROM tblassetModel where AssetBrand=@AssetBrand AND AssetModel =@AssetModel)"
        cmd.Connection = conn
        cmd.Parameters.Clear()
        cmd.Parameters.AddWithValue("@AssetBrand", AssetBrand)
        cmd.Parameters.AddWithValue("@AssetModel", AssetModel)
        Return cmd.ExecuteScalar
    End Function

    Private Function CheckAssetColorExists(AssetColor As String, conn As MySqlConnection) As Boolean
        Dim cmd As MySqlCommand = New MySqlCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT EXISTS(SELECT AssetColor FROM tblassetColor where AssetColor =@AssetColor)"
        cmd.Connection = conn
        cmd.Parameters.Clear()
        cmd.Parameters.AddWithValue("@AssetColor", AssetColor)
        Return cmd.ExecuteScalar
    End Function

    Private Function CheckSupplierExists(SupplierID As String, Name As String, conn As MySqlConnection) As Boolean
        Dim cmd As MySqlCommand = New MySqlCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT EXISTS(SELECT SupplierID FROM tblSupplier where SupplierID =@SupplierID AND Name=@Name)"
        cmd.Connection = conn
        cmd.Parameters.Clear()
        cmd.Parameters.AddWithValue("@SupplierID", SupplierID)
        cmd.Parameters.AddWithValue("@Name", Name)
        Return cmd.ExecuteScalar
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

                    '    If worksheet.SheetName.ToUpper = rdbModule.SelectedValue.ToString.ToUpper Then
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

            InsertIntoTblWebEventLog("Excel_To_DataTable", ex.Message.ToString, txtCreatedBy.Text)
            Throw ex

        End Try

        Return Tabla
    End Function

    Protected Sub btnAssetTemplate_Click(sender As Object, e As ImageClickEventArgs) Handles btnAssetTemplate.Click
        Try
            Dim filePath As String = ""

            filePath = "Asset_ExcelTemplate.xlsx"

            filePath = Server.MapPath("~/Uploads/Excel/ExcelTemplates/") + filePath
            Response.ContentType = ContentType
            Response.AppendHeader("Content-Disposition", ("attachment; filename=" + Path.GetFileName(filePath)))
            Response.WriteFile(filePath)
            Response.End()
        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString

            InsertIntoTblWebEventLog("Template", ex.Message.ToString, "Asset")
        End Try
    End Sub

    Protected Sub btnAddFileUpload_Click(sender As Object, e As EventArgs) Handles btnAddFileUpload.Click
        Try
            lblMessage.Text = ""
            lblAlert.Text = ""
            If String.IsNullOrEmpty(lblMovementRcno.Text) Then
                lblAlert.Text = "SELECT MOVEMENT TO UPLOAD FILE"
                Exit Sub

            End If
            If String.IsNullOrEmpty(txtFileDescription.Text) Then
                lblAlert.Text = "ENTER FILE DESCRIPTION TO UPLOAD FILE"
                Exit Sub

            End If
            If FileUpload3.HasFile Then

                Dim fileName As String = lblMovementAssetNo.Text + "_" + Path.GetFileName(FileUpload3.PostedFile.FileName)
                fileName = fileName.Replace(".PDF", ".pdf")

                Dim ext As String = Path.GetExtension(fileName)

                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()


                If ext = ".DOC" Or ext = ".doc" Or ext = ".DOCX" Or ext = ".docx" Or ext = ".xls" Or ext = ".xlsx" Or ext = ".XLS" Or ext = ".XLSX" Or ext = ".CSV" Or ext = ".csv" Or ext = ".ppt" Or ext = ".PPT" Or ext = ".pptx" Or ext = ".PPTX" Or ext = ".PDF" Or ext = ".pdf" Or ext = ".txt" Or ext = ".TXT" Or ext = ".jpg" Or ext = ".jpeg" Or ext = ".png" Or ext = ".bmp" Or ext = ".JPG" Or ext = ".JPEG" Or ext = ".PNG" Or ext = ".BMP" Then

                    If File.Exists(Server.MapPath("~/Uploads/Asset/") + lblMovementRcno.Text + "_" + fileName) Then

                        Dim command1 As MySqlCommand = New MySqlCommand

                        command1.CommandType = CommandType.Text

                        command1.CommandText = "SELECT * FROM tblFILEUPLOAD where filenamelink=@filenamelink"
                        command1.Parameters.AddWithValue("@filenamelink", lblMovementRcno.Text + "_" + fileName)
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
                        command1.Parameters.AddWithValue("@filenamelink", lblMovementRcno.Text + "_" + fileName)
                        command1.Connection = conn

                        Dim dr As MySqlDataReader = command1.ExecuteReader()
                        Dim dt As New DataTable
                        dt.Load(dr)

                        If dt.Rows.Count > 0 Then

                            Dim command2 As MySqlCommand = New MySqlCommand

                            command2.CommandType = CommandType.Text

                            command2.CommandText = "delete from fileupload where filenamelink='" + lblMovementRcno.Text + "_" + fileName + "'"

                            command2.Connection = conn

                            command2.ExecuteNonQuery()
                            command2.Dispose()
                        End If
                        command1.Dispose()
                        dt.Dispose()
                        dr.Close()
                    End If
                    FileUpload1.PostedFile.SaveAs((Server.MapPath("~/Uploads/Asset/") + lblMovementRcno.Text + "_" + fileName))

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

                    command.Parameters.AddWithValue("@FileGroup", "ASSET")
                    command.Parameters.AddWithValue("@FileRef", lblMovementAssetNo.Text)
                    command.Parameters.AddWithValue("@FileName", fileName.ToUpper)
                    command.Parameters.AddWithValue("@FileDescription", txtFileDescription.Text.ToUpper)
                    command.Parameters.AddWithValue("@FileType", ext.ToUpper)

                    command.Parameters.AddWithValue("@ManualReport", "N")

                    command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                    command.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))

                    command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                    command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                    command.Parameters.AddWithValue("@FileNameLink", lblMovementRcno.Text + "_" + fileName.ToUpper)
                    command.Parameters.AddWithValue("@DocumentType", lblMovementType.Text)



                    command.Connection = conn

                    command.ExecuteNonQuery()
                    conn.Close()
                    conn.Dispose()
                    command.Dispose()

                    SqlDSUpload.SelectCommand = "select * from tblfileupload where fileref = '" + lblMovementRcno.Text + "'"
                    gvUpload.DataSourceID = "SqlDSUpload"
                    gvUpload.DataBind()

                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "FILEUPLOAD", txtAssetNo.Text, "ADD", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", lblMovementRcno.Text + "_" + fileName, txtAssetNo.Text)

                    txtFileDescription.Text = ""

                    lblMessage.Text = "FILE UPLOADED"
                    '     lblFileUploadCount.Text = "File Upload [" & gvUpload.Rows.Count & "]"
                    BindFileGrid(lblMovementRcno.Text)
                    cpnl2.Collapsed = True

                Else
                    lblAlert.Text = "FILE FORMAT NOT ALLOWED TO UPLOAD"
                    Return
                End If
            Else
                lblAlert.Text = "SELECT FILE TO UPLOAD"
            End If
            '  Response.Redirect(Request.Url.AbsoluteUri)
        Catch ex As Exception
            InsertIntoTblWebEventLog("UploadFile", ex.Message.ToString, txtAssetNo.Text)
        End Try
    End Sub


    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    End Sub


    Public Sub WriteExcelWithNPOI(ByVal dt As DataTable, ByVal extension As String)
        Dim workbook As IWorkbook

        If extension = "xlsx" Then
            workbook = New XSSFWorkbook()
        ElseIf extension = "xls" Then
            workbook = New HSSFWorkbook()
        Else
            Throw New Exception("This format is not supported")
        End If

        Dim sheet1 As ISheet = workbook.CreateSheet("Sheet1")

        'Add Selection Criteria

        Dim row1 As IRow = sheet1.CreateRow(0)
        Dim cell1 As ICell = row1.CreateCell(0)
        ' cell1.SetCellValue(Session("Selection").ToString)
        '  cell1.SetCellValue("(" + ConfigurationManager.AppSettings("DomainName").ToString() + ")" + Session("Selection").ToString)
        '  cell1.SetCellValue("Hi")

        cell1.CellStyle.WrapText = True
        Dim cra = New NPOI.SS.Util.CellRangeAddress(0, 0, 0, 8)
        sheet1.AddMergedRegion(cra)

        'Add Column Heading
        row1 = sheet1.CreateRow(1)

        Dim testeStyle As ICellStyle = workbook.CreateCellStyle()
        testeStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium
        testeStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium
        testeStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium
        testeStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium

        testeStyle.FillForegroundColor = IndexedColors.RoyalBlue.Index
        testeStyle.FillPattern = FillPattern.SolidForeground
        '  testeStyle.FillForegroundColor = IndexedColors.White.Index
        testeStyle.Alignment = HorizontalAlignment.Center

        Dim RowFont As IFont = workbook.CreateFont()
        RowFont.Color = IndexedColors.White.Index
        RowFont.IsBold = True

        testeStyle.SetFont(RowFont)

        For j As Integer = 0 To dt.Columns.Count - 1
            Dim cell As ICell = row1.CreateCell(j)
            '  InsertIntoTblWebEventLog("WriteExcel", dt.Columns(j).GetType().ToString(), dt.Columns(j).ToString())

            Dim columnName As String = dt.Columns(j).ToString()
            cell.SetCellValue(columnName)
            ' cell.Row.RowStyle.FillBackgroundColor = IndexedColors.LightBlue.Index
            cell.CellStyle = testeStyle

        Next

        'Add details
        Dim _doubleCellStyle As ICellStyle = Nothing

        If _doubleCellStyle Is Nothing Then
            _doubleCellStyle = workbook.CreateCellStyle()
            _doubleCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium
            _doubleCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium
            _doubleCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium
            _doubleCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium

            _doubleCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.00")
        End If

        Dim _intCellStyle As ICellStyle = Nothing

        If _intCellStyle Is Nothing Then
            _intCellStyle = workbook.CreateCellStyle()
            _intCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium
            _intCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium
            _intCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium
            _intCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium

            _intCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0")
        End If

        Dim dateCellStyle As ICellStyle = Nothing

        If dateCellStyle Is Nothing Then
            dateCellStyle = workbook.CreateCellStyle()
            dateCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium
            dateCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium
            dateCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium
            dateCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium

            dateCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("dd-mm-yyyy")
        End If

        Dim AllCellStyle As ICellStyle = Nothing

        AllCellStyle = workbook.CreateCellStyle()
        AllCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium
        AllCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium
        AllCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium
        AllCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium


            For i As Integer = 0 To dt.Rows.Count - 1
                Dim row As IRow = sheet1.CreateRow(i + 2)

                For j As Integer = 0 To dt.Columns.Count - 1
                    Dim cell As ICell = row.CreateCell(j)

                'If j = dt.Columns.Count - 7 Or j = dt.Columns.Count - 8 Or j = dt.Columns.Count - 9 Or j = dt.Columns.Count - 10 Or j = dt.Columns.Count - 11 Or j = dt.Columns.Count - 17 Then
                '    If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                '        Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                '        cell.SetCellValue(d)
                '    Else
                '        Dim d As Double = Convert.ToDouble("0.00")
                '        cell.SetCellValue(d)

                '    End If
                '    cell.CellStyle = _doubleCellStyle

                'ElseIf j >= 2 And j <= dt.Columns.Count - 23 Then
                '    If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                '        Dim d As Int32 = Convert.ToInt32(dt.Rows(i)(j).ToString)
                '        cell.SetCellValue(d)
                '        cell.CellStyle = _intCellStyle

                '    Else
                '        '    Dim d As Int32 = Convert.ToInt32("0")
                '        cell.SetCellValue("")
                '        cell.CellStyle = AllCellStyle
                '    End If

                'ElseIf j = dt.Columns.Count - 14 Or j = dt.Columns.Count - 16 Or j = dt.Columns.Count - 19 Or j = dt.Columns.Count - 21 Then
                '    If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                '        Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
                '        cell.SetCellValue(d)
                '        'Else
                '        '    Dim d As Double = Convert.ToDouble("0.00")
                '        '    cell.SetCellValue(d)

                '    End If
                '    cell.CellStyle = dateCellStyle
                'Else
                cell.SetCellValue(dt.Rows(i)(j).ToString)
                cell.CellStyle = AllCellStyle

                '  End If
                If i = dt.Rows.Count - 1 Then
                    sheet1.AutoSizeColumn(j)
                End If
            Next
            Next


        Using exportData = New MemoryStream()
            Response.Clear()
            workbook.Write(exportData)
            '  Dim criteria As String = "_" + txtSvcDateFrom.Text + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmss")
            '   Dim attachment As String = "attachment; filename=ManpowerProductivityTeamMemberAppointment"
            Dim attachment As String = ""
            attachment = "attachment; filename=AssetExport"

            If extension = "xlsx" Then
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Response.AddHeader("Content-Disposition", String.Format(attachment + ".xlsx"))
                Response.BinaryWrite(exportData.ToArray())
            ElseIf extension = "xls" Then
                Response.ContentType = "application/vnd.ms-excel"
                Response.AddHeader("Content-Disposition", String.Format(attachment + ".xls"))
                Response.BinaryWrite(exportData.GetBuffer())
            End If

            Response.[End]()
        End Using
    End Sub


    Private Function GetDataSet() As DataTable
        Dim dt As New DataTable()
        Dim conn As MySqlConnection = New MySqlConnection()
        Dim cmd As MySqlCommand = New MySqlCommand
        Dim qry As String = "select Status as AssetStatus,StatusDesc,AssetNo,SerialNo,IMEI,AssetGrp,AssetRegNo,AssetClass,Make,Model,Color,AssetRegNo,Descrip,FinCoName as CurrentUser,PurchDt as PurchaseDate,SupplierCode,SupplierName,PurchRef as PONo,PurchVal as PurchasePrice,Warranty,WarrantyMS,WarrantyEnd,CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn "
        Dim qry1 As String = txt.Text
        qry1 = qry1.Replace("SELECT * ", qry)

        InsertIntoTblWebEventLog("Asset Excel", qry1, qry)
        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

        Dim sda As New MySqlDataAdapter()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = qry1

        cmd.Connection = conn
        Try
            conn.Open()
            sda.SelectCommand = cmd
            sda.Fill(dt)

            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
            sda.Dispose()
            conn.Dispose()
        End Try
    End Function

    Protected Sub btnChangeStatus_Click(sender As Object, e As EventArgs) Handles btnChangeStatus.Click
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        Dim qry As String = ""

        Dim command1 As MySqlCommand = New MySqlCommand

        command1.CommandType = CommandType.Text

        If btnStatus.Text = "CHECKOUT" Then
            btnStatus.Text = "CHECKIN"
            qry = "update tblasset set Status=0,StatusDesc='UNAVAILABLE' where assetno=@assetno"
        ElseIf btnStatus.Text = "CHECKIN" Then
            btnStatus.Text = "CHECKOUT"
            qry = "update tblasset set Status=1,StatusDesc='AVAILABLE' where assetno=@assetno"

        End If
        command1.CommandText = qry

        command1.Parameters.Clear()
        command1.Parameters.AddWithValue("@assetno", txtAssetNo.Text)
        ' command1.Parameters.AddWithValue("@finconame", ddlCheckOutStaff.Text)

        command1.Connection = conn

        command1.ExecuteNonQuery()

        command1.Dispose()
        conn.Close()
        conn.Dispose()

        If btnStatus.Text = "CHECKOUT" Then
            lblAlert.Text = "'" + txtAssetNo.Text + "' has been changed to UnAvailable"
        ElseIf btnStatus.Text = "CHECKIN" Then
            lblAlert.Text = "'" + txtAssetNo.Text + "' has been changed to Available"
        End If

        SqlDataSource1.SelectCommand = txt.Text
        SqlDataSource1.DataBind()
        GridView1.DataBind()
    End Sub

    Protected Sub btnExportToExcel_Click(sender As Object, e As ImageClickEventArgs) Handles btnExportToExcel.Click

        '   Dim dt As DataTable = DirectCast(SqlDataSource1.Select(DataSourceSelectArguments.Empty), DataView).Table
        Dim dt As DataTable = GetDataSet()

        WriteExcelWithNPOI(dt, "xlsx")
        Return


        Try
            GridView1.AllowPaging = False

            Response.Clear()
            Response.Buffer = True
            Response.ClearContent()
            Response.ClearHeaders()
            Response.Charset = ""

            Dim FileName As String = "AssetExport_" & DateTime.Now & ".xls"
            Dim strwritter As StringWriter = New StringWriter()
            Dim htmltextwrtter As HtmlTextWriter = New HtmlTextWriter(strwritter)
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.ContentType = "application/vnd.ms-excel"
            ' Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.AddHeader("Content-Disposition", "attachment;filename=" & FileName)
            GridView1.GridLines = GridLines.Both
            GridView1.HeaderStyle.Font.Bold = True
            GridView1.RenderControl(htmltextwrtter)
            Response.Write(strwritter.ToString())
            Response.[End]()
            GridView1.AllowPaging = True

        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString

            InsertIntoTblWebEventLog("btnExportToExcel_Click", ex.Message.ToString, "")
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub
End Class

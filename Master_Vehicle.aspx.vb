Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data

Partial Class Master_Vehicle
    Inherits System.Web.UI.Page

    Public rcno As String

    Public Sub MakeMeNull()
        lblMessage.Text = ""
        lblAlert.Text = ""
        txtMode.Text = ""
        txtAssetNo.Text = ""
        txtAssetRegNo.Text = ""
        txtAssetClass.Text = ""
        txtAssetGroup.Text = ""

        txtAssetCode.Text = ""
        txtAssetMake.Text = ""
        txtModel.Text = ""
        txtMfgYear.Text = ""

        txtCapacity.Text = ""
        txtCapacityUnit.Text = ""
        txtRegDate.Text = ""
        'txtType.Text = ""

        txtLastSvc.Text = ""
        txtNextSvc.Text = ""
        txtPriceCode.Text = ""
        txtOurRef.Text = ""

        txtEstDateIn.Text = ""
        txtDateOut.Text = ""
        txtRefDate.Text = ""
        txtReference.Text = ""

        txtGoogleEmail.Text = ""
        txtGPSLabel.Text = ""
        'txtStatus.Text = ""
        txtOPStatus.Text = ""

        txtDescription.Text = ""
        txtTechSpecs.Text = ""
        txtRemarks.Text = ""

        ddlInchargeID.SelectedIndex = 0
        txtStatus.SelectedIndex = 0

        txtRcno.Text = ""
        'txtCreatedBy.Text = ""
        txtCreatedOn.Text = ""
        ' txtLastModifiedBy.Text = ""
        txtLastModifiedOn.Text = ""
        txtLocationId.SelectedIndex = 0


    End Sub

    Private Sub EnableControls()
        btnSave.Enabled = False
        btnSave.ForeColor = System.Drawing.Color.Gray
        btnCancel.Enabled = False
        btnCancel.ForeColor = System.Drawing.Color.Gray

        btnADD.Enabled = True
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

        txtAssetNo.Enabled = False
        txtAssetRegNo.Enabled = False
        txtAssetClass.Enabled = False
        txtAssetGroup.Enabled = False

        txtAssetCode.Enabled = False
        txtAssetMake.Enabled = False
        txtModel.Enabled = False
        txtMfgYear.Enabled = False

        txtCapacity.Enabled = False
        txtCapacityUnit.Enabled = False
        txtRegDate.Enabled = False
        txtType.Enabled = False

        txtLastSvc.Enabled = False
        txtNextSvc.Enabled = False
        txtPriceCode.Enabled = False
        txtOurRef.Enabled = False

        txtEstDateIn.Enabled = False
        txtDateOut.Enabled = False
        txtRefDate.Enabled = False
        txtReference.Enabled = False

        txtGoogleEmail.Enabled = False
        txtGPSLabel.Enabled = False
        txtStatus.Enabled = False
        txtOPStatus.Enabled = False

        txtDescription.Enabled = False
        txtTechSpecs.Enabled = False
        txtRemarks.Enabled = False

        ddlInchargeID.Enabled = False
        txtLocationId.Enabled = False
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

        txtAssetNo.Enabled = True
        txtAssetRegNo.Enabled = True
        txtAssetClass.Enabled = True
        txtAssetGroup.Enabled = True

        txtAssetCode.Enabled = True
        txtAssetMake.Enabled = True
        txtModel.Enabled = True
        txtMfgYear.Enabled = True

        txtCapacity.Enabled = True
        txtCapacityUnit.Enabled = True
        txtRegDate.Enabled = True
        txtType.Enabled = True

        txtLastSvc.Enabled = True
        txtNextSvc.Enabled = True
        txtPriceCode.Enabled = True
        txtOurRef.Enabled = True

        txtEstDateIn.Enabled = True
        txtDateOut.Enabled = True
        txtRefDate.Enabled = True
        txtReference.Enabled = True

        txtGoogleEmail.Enabled = True
        txtGPSLabel.Enabled = True
        txtStatus.Enabled = True
        txtOPStatus.Enabled = True

        txtDescription.Enabled = True
        txtTechSpecs.Enabled = True
        txtRemarks.Enabled = True

        txtRcno.Enabled = True
        txtCreatedBy.Enabled = True
        txtCreatedOn.Enabled = True
        txtLastModifiedBy.Enabled = True
        txtLastModifiedOn.Enabled = True

        ddlInchargeID.Enabled = True
        txtLocationId.Enabled = True
        AccessControl()
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

        If GridView1.SelectedRow.Cells(1).Text = "&nbsp;" Then
            txtAssetNo.Text = ""
        Else

            txtAssetNo.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(1).Text).ToString
        End If
        If GridView1.SelectedRow.Cells(2).Text = "&nbsp;" Then
            txtAssetRegNo.Text = ""
        Else
            txtAssetRegNo.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(2).Text).ToString
        End If
        If GridView1.SelectedRow.Cells(3).Text = "&nbsp;" Then
            txtAssetClass.Text = ""
        Else
            txtAssetClass.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(3).Text).ToString
        End If

        If GridView1.SelectedRow.Cells(4).Text = "&nbsp;" Then
            ddlInchargeID.Text = "-1"
        Else
            ddlInchargeID.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(4).Text).ToString
        End If
        If GridView1.SelectedRow.Cells(5).Text = "&nbsp;" Then
            txtStatus.Text = ""
        Else
            txtStatus.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(5).Text).ToString
        End If
        If GridView1.SelectedRow.Cells(6).Text = "&nbsp;" Then
            txtOPStatus.Text = ""
        Else
            txtOPStatus.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(6).Text).ToString
        End If
        If GridView1.SelectedRow.Cells(7).Text = "&nbsp;" Then
            txtDescription.Text = ""
        Else
            txtDescription.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(7).Text).ToString
        End If


        If String.IsNullOrEmpty(GridView1.SelectedRow.Cells(8).Text) = True Or (GridView1.SelectedRow.Cells(8).Text) = "&nbsp;" Then
            txtLocationId.SelectedIndex = 0
        Else
            txtLocationId.Text = GridView1.SelectedRow.Cells(8).Text
        End If
        'If GridView1.SelectedRow.Cells(3).Text = "&nbsp;" Then
        '    txtAssetGroup.Text = ""
        'Else
        '    txtAssetGroup.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(3).Text).ToString
        'End If

        'If GridView1.SelectedRow.Cells(5).Text = "&nbsp;" Then
        '    txtAssetCode.Text = ""
        'Else
        '    txtAssetCode.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(5).Text).ToString
        'End If
        'If GridView1.SelectedRow.Cells(6).Text = "&nbsp;" Then
        '    txtAssetMake.Text = ""
        'Else
        '    txtAssetMake.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(6).Text).ToString
        'End If
        'If GridView1.SelectedRow.Cells(7).Text = "&nbsp;" Then
        '    txtModel.Text = ""
        'Else
        '    txtModel.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(7).Text).ToString
        'End If
        'If GridView1.SelectedRow.Cells(8).Text = "&nbsp;" Then
        '    txtMfgYear.Text = ""
        'Else
        '    txtMfgYear.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(8).Text).ToString
        'End If
        'If GridView1.SelectedRow.Cells(9).Text = "&nbsp;" Then
        '    txtCapacity.Text = ""
        'Else
        '    txtCapacity.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(9).Text).ToString
        'End If
        'If GridView1.SelectedRow.Cells(10).Text = "&nbsp;" Then
        '    txtCapacityUnit.Text = ""
        'Else
        '    txtCapacityUnit.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(10).Text).ToString
        'End If
        'If GridView1.SelectedRow.Cells(11).Text = "&nbsp;" Then
        '    txtRegDate.Text = ""
        'Else
        '    txtRegDate.Text = Convert.ToDateTime(Server.HtmlDecode(GridView1.SelectedRow.Cells(11).Text)).ToString("dd/MM/yyyy")
        'End If
        ''If GridView1.SelectedRow.Cells(12).Text = "&nbsp;" Then
        ''    txtType.Text = ""
        ''Else
        ''    txtType.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(12).Text).ToString
        ''End If
        'If GridView1.SelectedRow.Cells(13).Text = "&nbsp;" Then
        '    txtLastSvc.Text = ""
        'Else
        '    txtLastSvc.Text = Convert.ToDateTime(Server.HtmlDecode(GridView1.SelectedRow.Cells(13).Text)).ToString("dd/MM/yyyy")
        'End If
        'If GridView1.SelectedRow.Cells(14).Text = "&nbsp;" Then
        '    txtNextSvc.Text = ""
        'Else
        '    txtNextSvc.Text = Convert.ToDateTime(Server.HtmlDecode(GridView1.SelectedRow.Cells(14).Text)).ToString("dd/MM/yyyy")
        'End If
        'If GridView1.SelectedRow.Cells(15).Text = "&nbsp;" Then
        '    txtPriceCode.Text = ""
        'Else
        '    txtPriceCode.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(15).Text).ToString
        'End If
        'If GridView1.SelectedRow.Cells(16).Text = "&nbsp;" Then
        '    txtOurRef.Text = ""
        'Else
        '    txtOurRef.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(16).Text).ToString
        'End If
        'If GridView1.SelectedRow.Cells(17).Text = "&nbsp;" Then
        '    txtDateOut.Text = ""
        'Else
        '    txtDateOut.Text = Convert.ToDateTime(Server.HtmlDecode(GridView1.SelectedRow.Cells(17).Text)).ToString("dd/MM/yyyy")
        'End If
        'If GridView1.SelectedRow.Cells(18).Text = "&nbsp;" Then
        '    txtEstDateIn.Text = ""
        'Else
        '    txtEstDateIn.Text = Convert.ToDateTime(Server.HtmlDecode(GridView1.SelectedRow.Cells(18).Text)).ToString("dd/MM/yyyy")
        'End If
        'If GridView1.SelectedRow.Cells(19).Text = "&nbsp;" Then
        '    txtRefDate.Text = ""
        'Else
        '    txtRefDate.Text = Convert.ToDateTime(Server.HtmlDecode(GridView1.SelectedRow.Cells(19).Text)).ToString("dd/MM/yyyy")
        'End If
        'If GridView1.SelectedRow.Cells(20).Text = "&nbsp;" Then
        '    txtReference.Text = ""
        'Else
        '    txtReference.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(20).Text).ToString
        'End If
        'If GridView1.SelectedRow.Cells(21).Text = "&nbsp;" Then
        '    txtGoogleEmail.Text = ""
        'Else
        '    txtGoogleEmail.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(21).Text).ToString
        'End If
        'If GridView1.SelectedRow.Cells(22).Text = "&nbsp;" Then
        '    txtGPSLabel.Text = ""
        'Else
        '    txtGPSLabel.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(22).Text).ToString
        'End If

        'If GridView1.SelectedRow.Cells(26).Text = "&nbsp;" Then
        '    txtTechSpecs.Text = ""
        'Else
        '    txtTechSpecs.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(26).Text).ToString
        'End If
        'If GridView1.SelectedRow.Cells(27).Text = "&nbsp;" Then
        '    txtRemarks.Text = ""
        'Else
        '    txtRemarks.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(27).Text).ToString
        'End If

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

        If Session("SecGroupAuthority") <> "ADMINISTRATOR" Then
            AccessControl()
        End If


    End Sub

    Protected Sub btnADD_Click(sender As Object, e As EventArgs) Handles btnADD.Click
        MakeMeNull()


        txtStatus.Text = "O"
        txtOPStatus.Text = "O"
        txtType.Text = "Vehicle"
        txtMode.Text = "New"
        DisableControls()
        lblMessage.Text = "ACTION: ADD RECORD"
        txtAssetNo.Focus()

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        txtCreatedOn.Attributes.Add("readonly", "readonly")

        If Not IsPostBack Then
            MakeMeNull()
            EnableControls()
            Dim UserID As String = Convert.ToString(Session("UserID"))
            txtCreatedBy.Text = UserID
            txtLastModifiedBy.Text = UserID

            Dim query As String
            query = "Select LocationID from tbllocation"
            PopulateDropDownList(query, "LocationID", "LocationID", txtLocationId)

            query = ""

            'Dim query As String
            query = "SELECT StaffId FROM tblstaff ORDER BY STAFFID"
            PopulateDropDownList(query, "StaffId", "StaffId", ddlSearchInchargeID)

        End If
        'txt.Text = Convert.ToString(Session("Query"))
        'If String.IsNullOrEmpty(txt.Text) = False Or txt.Text <> "" Then
        '    SqlDataSource1.SelectCommand = txt.Text
        '    SqlDataSource1.DataBind()
        '    GridView1.DataBind()
        'End If

        'btnFilter.Attributes.Add("onClick", "javascript:FilterPopup();")
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
        Try
            '''''''''''''''''''Access Control 
            If Session("SecGroupAuthority") <> "ADMINISTRATOR" Then
                Dim conn As MySqlConnection = New MySqlConnection()
                Dim command As MySqlCommand = New MySqlCommand

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                command.CommandType = CommandType.Text
                command.CommandText = "SELECT X2415,  X2415Add, X2415Edit, X2415Delete,x2415Print FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"
                command.Connection = conn

                Dim dr As MySqlDataReader = command.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)
                conn.Close()

                If dt.Rows.Count > 0 Then
                    If Not IsDBNull(dt.Rows(0)("X2415")) Then
                        If String.IsNullOrEmpty(Convert.ToBoolean(dt.Rows(0)("X2415"))) = False Then
                            If Convert.ToBoolean(dt.Rows(0)("X2415")) = False Then
                                Response.Redirect("Home.aspx")
                            End If
                        End If
                    End If

                    If Not IsDBNull(dt.Rows(0)("X2415Add")) Then
                        If String.IsNullOrEmpty(dt.Rows(0)("X2415Add")) = False Then
                            Me.btnAdd.Enabled = dt.Rows(0)("X2415Add").ToString()
                        End If
                    End If

                    If Not IsDBNull(dt.Rows(0)("X2415Print")) Then
                        If String.IsNullOrEmpty(dt.Rows(0)("X2415Print")) = False Then
                            Me.btnPrint.Enabled = dt.Rows(0)("X2415Print").ToString()
                        End If
                    End If

                    If txtMode.Text = "View" Then
                        If Not IsDBNull(dt.Rows(0)("X2415Edit")) Then
                            If String.IsNullOrEmpty(dt.Rows(0)("X2415Edit")) = False Then
                                Me.btnEdit.Enabled = dt.Rows(0)("X2415Edit").ToString()
                            End If
                        End If

                        If Not IsDBNull(dt.Rows(0)("X2415Delete")) Then
                            If String.IsNullOrEmpty(dt.Rows(0)("X2415Delete")) = False Then
                                Me.btnDelete.Enabled = dt.Rows(0)("X2415Delete").ToString()
                            End If
                        End If
                    Else
                        Me.btnEdit.Enabled = False
                        Me.btnDelete.Enabled = False
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

        If txtAssetNo.Text = "" Then
            '  MessageBox.Message.Alert(Page, "Asset No cannot be blank!!!", "str")
            lblAlert.Text = "ASSET NO CANNOT BE BLANK"
            Return

        End If
        'If DateValidation() = False Then
        '    Return
        'End If
        If txtMode.Text = "New" Then
            Try
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

                    ' MessageBox.Message.Alert(Page, "Record already exists!!!", "str")
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
                    qry = qry + "GPSLabel,UploadDate,DelGoogleCalendar,GoogleEmail,GooglePassword,ColourCodeHtml,ColourCodeRGB,Location, CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn)"
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
                    qry = qry + "@GoogleEmail,@GooglePassword,@ColourCodeHtml,@ColourCodeRGB,@Location, @CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn);"


                    command.CommandText = qry
                    command.Parameters.Clear()

                    If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                        command.Parameters.AddWithValue("@AssetNo", txtAssetNo.Text.ToUpper)
                        If txtAssetGroup.Text = "" Then
                            command.Parameters.AddWithValue("@AssetGrp", "")
                        Else
                            command.Parameters.AddWithValue("@AssetGrp", txtAssetGroup.Text.ToUpper)
                        End If

                        command.Parameters.AddWithValue("@AssetClass", txtAssetClass.Text.ToUpper)
                        command.Parameters.AddWithValue("@AssetRegNo", txtAssetRegNo.Text.ToUpper)
                        command.Parameters.AddWithValue("@Status", txtStatus.Text.ToUpper)
                        command.Parameters.AddWithValue("@OpStatus", txtOPStatus.Text.ToUpper)
                        command.Parameters.AddWithValue("@Descrip", txtDescription.Text.ToUpper)
                        command.Parameters.AddWithValue("@DefAddr", "")
                        command.Parameters.AddWithValue("@Depmtd", "")
                        command.Parameters.AddWithValue("@PurfmCType", "")
                        command.Parameters.AddWithValue("@PurfmCCode", "")
                        command.Parameters.AddWithValue("@PurfmInvNo", "")
                        command.Parameters.AddWithValue("@PurchRef", "")
                        command.Parameters.AddWithValue("@PurchDt", DBNull.Value)
                        command.Parameters.AddWithValue("@PurchVal", 0)
                        command.Parameters.AddWithValue("@ObbkVal", 0)
                        command.Parameters.AddWithValue("@ObbkDate", DBNull.Value)
                        command.Parameters.AddWithValue("@CurrAddr", "")
                        command.Parameters.AddWithValue("@InCharge", "")
                        command.Parameters.AddWithValue("@CurrCont", "")
                        command.Parameters.AddWithValue("@Phone1", "")
                        command.Parameters.AddWithValue("@Phone2", "")
                        If txtEstDateIn.Text = "" Then
                            command.Parameters.AddWithValue("@EstDateIn", DBNull.Value)
                        Else
                            command.Parameters.AddWithValue("@EstDateIn", Convert.ToDateTime(txtEstDateIn.Text))
                        End If

                        command.Parameters.AddWithValue("@Notes", "")
                        command.Parameters.AddWithValue("@ActDateIn", DBNull.Value)
                        command.Parameters.AddWithValue("@LastMoveId", "")
                        command.Parameters.AddWithValue("@Project", "")
                        If txtDateOut.Text = "" Then
                            command.Parameters.AddWithValue("@DateOut", DBNull.Value)
                        Else
                            command.Parameters.AddWithValue("@DateOut", Convert.ToDateTime(txtDateOut.Text))
                        End If
                        command.Parameters.AddWithValue("@Purpose", "")
                        command.Parameters.AddWithValue("@AssetCode", txtAssetCode.Text.ToUpper)
                        command.Parameters.AddWithValue("@Make", txtAssetMake.Text.ToUpper)
                        command.Parameters.AddWithValue("@Model", txtModel.Text.ToUpper)
                        command.Parameters.AddWithValue("@Year", "")
                        command.Parameters.AddWithValue("@Capacity", txtCapacity.Text.ToUpper)
                        command.Parameters.AddWithValue("@EngineNo", "")
                        command.Parameters.AddWithValue("@Value", 0)
                        command.Parameters.AddWithValue("@Remarks", txtRemarks.Text.ToUpper)
                        command.Parameters.AddWithValue("@PurfmCName", "")
                        command.Parameters.AddWithValue("@ReferenceOur", txtOurRef.Text.ToUpper)
                        command.Parameters.AddWithValue("@SvcFreq", 0)
                        If txtNextSvc.Text = "" Then
                            command.Parameters.AddWithValue("@NextSvcDate", DBNull.Value)
                        Else
                            command.Parameters.AddWithValue("@NextSvcDate", Convert.ToDateTime(txtNextSvc.Text))
                        End If
                        If txtLastSvc.Text = "" Then
                            command.Parameters.AddWithValue("@LastSvcDate", DBNull.Value)
                        Else
                            command.Parameters.AddWithValue("@LastSvcDate", Convert.ToDateTime(txtLastSvc.Text))
                        End If
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
                        command.Parameters.AddWithValue("@MfgYear", txtMfgYear.Text.ToUpper)
                        command.Parameters.AddWithValue("@CustCode", "")
                        command.Parameters.AddWithValue("@CustName", "")
                        command.Parameters.AddWithValue("@Reference", txtReference.Text.ToUpper)
                        If txtRefDate.Text = "" Then
                            command.Parameters.AddWithValue("@RefDate", DBNull.Value)
                        Else
                            command.Parameters.AddWithValue("@RefDate", Convert.ToDateTime(txtRefDate.Text))
                        End If
                        command.Parameters.AddWithValue("@PriceCode", txtPriceCode.Text.ToUpper)
                        command.Parameters.AddWithValue("@PROJECTCODE", "")
                        command.Parameters.AddWithValue("@PROJECTNAME", "")
                        command.Parameters.AddWithValue("@Cost", 0)
                        command.Parameters.AddWithValue("@SupplierCode", "")
                        command.Parameters.AddWithValue("@SupplierName", "")
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
                        If ddlInchargeID.Text = "-1" Then
                            command.Parameters.AddWithValue("@InChargeID", "")
                        Else
                            command.Parameters.AddWithValue("@InChargeID", ddlInchargeID.Text.ToUpper)
                        End If

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
                        command.Parameters.AddWithValue("@TechnicalSpecs", txtTechSpecs.Text.ToUpper)
                        command.Parameters.AddWithValue("@type", txtType.Text.ToUpper)
                        command.Parameters.AddWithValue("@CapacityUnitMS", txtCapacityUnit.Text.ToUpper)
                        If txtRegDate.Text = "" Then
                            command.Parameters.AddWithValue("@RegDate", DBNull.Value)
                        Else
                            command.Parameters.AddWithValue("@RegDate", Convert.ToDateTime(txtRegDate.Text))
                        End If
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
                        command.Parameters.AddWithValue("@GPSLabel", txtGPSLabel.Text.ToUpper)
                        command.Parameters.AddWithValue("@UploadDate", DBNull.Value)
                        command.Parameters.AddWithValue("@DelGoogleCalendar", 0)
                        command.Parameters.AddWithValue("@GoogleEmail", txtGoogleEmail.Text.ToUpper)
                        command.Parameters.AddWithValue("@GooglePassword", "")
                        command.Parameters.AddWithValue("@ColourCodeHtml", "")
                        command.Parameters.AddWithValue("@ColourCodeRGB", "")

                        If txtLocationId.SelectedIndex = 0 Then
                            command.Parameters.AddWithValue("@Location", "")
                        Else
                            command.Parameters.AddWithValue("@Location", txtLocationId.Text)
                        End If

                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                    ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                        command.Parameters.AddWithValue("@AssetNo", txtAssetNo.Text)
                        If txtAssetGroup.Text = "" Then
                            command.Parameters.AddWithValue("@AssetGrp", "")
                        Else
                            command.Parameters.AddWithValue("@AssetGrp", txtAssetGroup.Text)
                        End If

                        command.Parameters.AddWithValue("@AssetClass", txtAssetClass.Text)
                        command.Parameters.AddWithValue("@AssetRegNo", txtAssetRegNo.Text)
                        command.Parameters.AddWithValue("@Status", txtStatus.Text)
                        command.Parameters.AddWithValue("@OpStatus", txtOPStatus.Text)
                        command.Parameters.AddWithValue("@Descrip", txtDescription.Text)
                        command.Parameters.AddWithValue("@DefAddr", "")
                        command.Parameters.AddWithValue("@Depmtd", "")
                        command.Parameters.AddWithValue("@PurfmCType", "")
                        command.Parameters.AddWithValue("@PurfmCCode", "")
                        command.Parameters.AddWithValue("@PurfmInvNo", "")
                        command.Parameters.AddWithValue("@PurchRef", "")
                        command.Parameters.AddWithValue("@PurchDt", DBNull.Value)
                        command.Parameters.AddWithValue("@PurchVal", 0)
                        command.Parameters.AddWithValue("@ObbkVal", 0)
                        command.Parameters.AddWithValue("@ObbkDate", DBNull.Value)
                        command.Parameters.AddWithValue("@CurrAddr", "")
                        command.Parameters.AddWithValue("@InCharge", "")
                        command.Parameters.AddWithValue("@CurrCont", "")
                        command.Parameters.AddWithValue("@Phone1", "")
                        command.Parameters.AddWithValue("@Phone2", "")
                        If txtEstDateIn.Text = "" Then
                            command.Parameters.AddWithValue("@EstDateIn", DBNull.Value)
                        Else
                            command.Parameters.AddWithValue("@EstDateIn", Convert.ToDateTime(txtEstDateIn.Text))
                        End If

                        command.Parameters.AddWithValue("@Notes", "")
                        command.Parameters.AddWithValue("@ActDateIn", DBNull.Value)
                        command.Parameters.AddWithValue("@LastMoveId", "")
                        command.Parameters.AddWithValue("@Project", "")
                        If txtDateOut.Text = "" Then
                            command.Parameters.AddWithValue("@DateOut", DBNull.Value)
                        Else
                            command.Parameters.AddWithValue("@DateOut", Convert.ToDateTime(txtDateOut.Text))
                        End If
                        command.Parameters.AddWithValue("@Purpose", "")
                        command.Parameters.AddWithValue("@AssetCode", txtAssetCode.Text)
                        command.Parameters.AddWithValue("@Make", txtAssetMake.Text)
                        command.Parameters.AddWithValue("@Model", txtModel.Text)
                        command.Parameters.AddWithValue("@Year", "")
                        command.Parameters.AddWithValue("@Capacity", txtCapacity.Text)
                        command.Parameters.AddWithValue("@EngineNo", "")
                        command.Parameters.AddWithValue("@Value", 0)
                        command.Parameters.AddWithValue("@Remarks", txtRemarks.Text)
                        command.Parameters.AddWithValue("@PurfmCName", "")
                        command.Parameters.AddWithValue("@ReferenceOur", txtOurRef.Text)
                        command.Parameters.AddWithValue("@SvcFreq", 0)
                        If txtNextSvc.Text = "" Then
                            command.Parameters.AddWithValue("@NextSvcDate", DBNull.Value)
                        Else
                            command.Parameters.AddWithValue("@NextSvcDate", Convert.ToDateTime(txtNextSvc.Text))
                        End If
                        If txtLastSvc.Text = "" Then
                            command.Parameters.AddWithValue("@LastSvcDate", DBNull.Value)
                        Else
                            command.Parameters.AddWithValue("@LastSvcDate", Convert.ToDateTime(txtLastSvc.Text))
                        End If
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
                        command.Parameters.AddWithValue("@MfgYear", txtMfgYear.Text)
                        command.Parameters.AddWithValue("@CustCode", "")
                        command.Parameters.AddWithValue("@CustName", "")
                        command.Parameters.AddWithValue("@Reference", txtReference.Text)
                        If txtRefDate.Text = "" Then
                            command.Parameters.AddWithValue("@RefDate", DBNull.Value)
                        Else
                            command.Parameters.AddWithValue("@RefDate", Convert.ToDateTime(txtRefDate.Text))
                        End If
                        command.Parameters.AddWithValue("@PriceCode", txtPriceCode.Text)
                        command.Parameters.AddWithValue("@PROJECTCODE", "")
                        command.Parameters.AddWithValue("@PROJECTNAME", "")
                        command.Parameters.AddWithValue("@Cost", 0)
                        command.Parameters.AddWithValue("@SupplierCode", "")
                        command.Parameters.AddWithValue("@SupplierName", "")
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
                        If ddlInchargeID.Text = "-1" Then
                            command.Parameters.AddWithValue("@InChargeID", "")
                        Else
                            command.Parameters.AddWithValue("@InChargeID", ddlInchargeID.Text)
                        End If

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
                        command.Parameters.AddWithValue("@TechnicalSpecs", txtTechSpecs.Text)
                        command.Parameters.AddWithValue("@type", txtType.Text)
                        command.Parameters.AddWithValue("@CapacityUnitMS", txtCapacityUnit.Text)
                        If txtRegDate.Text = "" Then
                            command.Parameters.AddWithValue("@RegDate", DBNull.Value)
                        Else
                            command.Parameters.AddWithValue("@RegDate", Convert.ToDateTime(txtRegDate.Text))
                        End If
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
                        command.Parameters.AddWithValue("@GPSLabel", txtGPSLabel.Text)
                        command.Parameters.AddWithValue("@UploadDate", DBNull.Value)
                        command.Parameters.AddWithValue("@DelGoogleCalendar", 0)
                        command.Parameters.AddWithValue("@GoogleEmail", txtGoogleEmail.Text)
                        command.Parameters.AddWithValue("@GooglePassword", "")
                        command.Parameters.AddWithValue("@ColourCodeHtml", "")
                        command.Parameters.AddWithValue("@ColourCodeRGB", "")

                        If txtLocationId.SelectedIndex = 0 Then
                            command.Parameters.AddWithValue("@Location", "")
                        Else
                            command.Parameters.AddWithValue("@Location", txtLocationId.Text)
                        End If

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
                End If
                conn.Close()
                If txtMode.Text = "NEW" Then
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "VEHICLE", txtAssetNo.Text, "ADD", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
                Else
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "VEHICLE", txtAssetNo.Text, "EDIT", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
                End If
            Catch ex As Exception
                MessageBox.Message.Alert(Page, ex.ToString, "str")
            End Try
            EnableControls()
            txtMode.Text = ""
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
                        Dim qry As String = "UPDATE tblasset SET AssetNo = @AssetNo,AssetGrp = @AssetGrp,AssetClass = @AssetClass,AssetRegNo = @AssetRegNo,Status = @Status,OpStatus = @OpStatus,Descrip = @Descrip,"
                        qry = qry + "EstDateIn = @EstDateIn,DateOut = @DateOut,AssetCode = @AssetCode,Make = @Make,Model = @Model,Capacity = @Capacity,Remarks = @Remarks,ReferenceOur = @ReferenceOur,NextSvcDate = @NextSvcDate,"
                        qry = qry + "LastSvcDate = @LastSvcDate,MfgYear = @MfgYear,Reference = @Reference,RefDate = @RefDate,PriceCode = @PriceCode,TechnicalSpecs = @TechnicalSpecs,type = @type,CapacityUnitMS = @CapacityUnitMS,"
                        qry = qry + "RegDate = @RegDate,GPSLabel = @GPSLabel,GoogleEmail = @GoogleEmail, Location = @Location, LastModifiedBy = @LastModifiedBy,LastModifiedOn = @LastModifiedOn, InchargeID=@InchargeID WHERE Rcno = " & Convert.ToInt32(txtRcno.Text)

                        command.CommandText = qry
                        command.Parameters.Clear()

                        If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                            command.Parameters.AddWithValue("@AssetNo", txtAssetNo.Text.ToUpper)
                            If txtAssetGroup.Text = "" Then
                                command.Parameters.AddWithValue("@AssetGrp", "")
                            Else
                                command.Parameters.AddWithValue("@AssetGrp", txtAssetGroup.Text.ToUpper)
                            End If

                            command.Parameters.AddWithValue("@AssetClass", txtAssetClass.Text.ToUpper)
                            command.Parameters.AddWithValue("@AssetRegNo", txtAssetRegNo.Text.ToUpper)
                            command.Parameters.AddWithValue("@Status", txtStatus.Text.ToUpper)
                            command.Parameters.AddWithValue("@OpStatus", txtOPStatus.Text.ToUpper)
                            command.Parameters.AddWithValue("@Descrip", txtDescription.Text.ToUpper)
                            command.Parameters.AddWithValue("@DefAddr", "")
                            command.Parameters.AddWithValue("@Depmtd", "")
                            command.Parameters.AddWithValue("@PurfmCType", "")
                            command.Parameters.AddWithValue("@PurfmCCode", "")
                            command.Parameters.AddWithValue("@PurfmInvNo", "")
                            command.Parameters.AddWithValue("@PurchRef", "")
                            command.Parameters.AddWithValue("@PurchDt", DBNull.Value)
                            command.Parameters.AddWithValue("@PurchVal", 0)
                            command.Parameters.AddWithValue("@ObbkVal", 0)
                            command.Parameters.AddWithValue("@ObbkDate", DBNull.Value)
                            command.Parameters.AddWithValue("@CurrAddr", "")
                            command.Parameters.AddWithValue("@InCharge", "")
                            command.Parameters.AddWithValue("@CurrCont", "")
                            command.Parameters.AddWithValue("@Phone1", "")
                            command.Parameters.AddWithValue("@Phone2", "")
                            If txtEstDateIn.Text = "" Then
                                command.Parameters.AddWithValue("@EstDateIn", DBNull.Value)
                            Else
                                command.Parameters.AddWithValue("@EstDateIn", Convert.ToDateTime(txtEstDateIn.Text))
                            End If

                            command.Parameters.AddWithValue("@Notes", "")
                            command.Parameters.AddWithValue("@ActDateIn", DBNull.Value)
                            command.Parameters.AddWithValue("@LastMoveId", "")
                            command.Parameters.AddWithValue("@Project", "")
                            If txtDateOut.Text = "" Then
                                command.Parameters.AddWithValue("@DateOut", DBNull.Value)
                            Else
                                command.Parameters.AddWithValue("@DateOut", Convert.ToDateTime(txtDateOut.Text))
                            End If
                            command.Parameters.AddWithValue("@Purpose", "")
                            command.Parameters.AddWithValue("@AssetCode", txtAssetCode.Text.ToUpper)
                            command.Parameters.AddWithValue("@Make", txtAssetMake.Text.ToUpper)
                            command.Parameters.AddWithValue("@Model", txtModel.Text.ToUpper)
                            command.Parameters.AddWithValue("@Year", "")
                            command.Parameters.AddWithValue("@Capacity", txtCapacity.Text.ToUpper)
                            command.Parameters.AddWithValue("@EngineNo", "")
                            command.Parameters.AddWithValue("@Value", 0)
                            command.Parameters.AddWithValue("@Remarks", txtRemarks.Text.ToUpper)
                            command.Parameters.AddWithValue("@PurfmCName", "")
                            command.Parameters.AddWithValue("@ReferenceOur", txtOurRef.Text.ToUpper)
                            command.Parameters.AddWithValue("@SvcFreq", 0)
                            If txtNextSvc.Text = "" Then
                                command.Parameters.AddWithValue("@NextSvcDate", DBNull.Value)
                            Else
                                command.Parameters.AddWithValue("@NextSvcDate", Convert.ToDateTime(txtNextSvc.Text))
                            End If
                            If txtLastSvc.Text = "" Then
                                command.Parameters.AddWithValue("@LastSvcDate", DBNull.Value)
                            Else
                                command.Parameters.AddWithValue("@LastSvcDate", Convert.ToDateTime(txtLastSvc.Text))
                            End If
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
                            command.Parameters.AddWithValue("@MfgYear", txtMfgYear.Text.ToUpper)
                            command.Parameters.AddWithValue("@CustCode", "")
                            command.Parameters.AddWithValue("@CustName", "")
                            command.Parameters.AddWithValue("@Reference", txtReference.Text.ToUpper)
                            If txtRefDate.Text = "" Then
                                command.Parameters.AddWithValue("@RefDate", DBNull.Value)
                            Else
                                command.Parameters.AddWithValue("@RefDate", Convert.ToDateTime(txtRefDate.Text))
                            End If
                            command.Parameters.AddWithValue("@PriceCode", txtPriceCode.Text.ToUpper)
                            command.Parameters.AddWithValue("@PROJECTCODE", "")
                            command.Parameters.AddWithValue("@PROJECTNAME", "")
                            command.Parameters.AddWithValue("@Cost", 0)
                            command.Parameters.AddWithValue("@SupplierCode", "")
                            command.Parameters.AddWithValue("@SupplierName", "")
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
                            If ddlInchargeID.Text = "-1" Then
                                command.Parameters.AddWithValue("@InChargeID", "")
                            Else
                                command.Parameters.AddWithValue("@InChargeID", ddlInchargeID.Text.ToUpper)
                            End If

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
                            command.Parameters.AddWithValue("@TechnicalSpecs", txtTechSpecs.Text.ToUpper)
                            command.Parameters.AddWithValue("@type", txtType.Text.ToUpper)
                            command.Parameters.AddWithValue("@CapacityUnitMS", txtCapacityUnit.Text.ToUpper)
                            If txtRegDate.Text = "" Then
                                command.Parameters.AddWithValue("@RegDate", DBNull.Value)
                            Else
                                command.Parameters.AddWithValue("@RegDate", Convert.ToDateTime(txtRegDate.Text))
                            End If
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
                            command.Parameters.AddWithValue("@GPSLabel", txtGPSLabel.Text.ToUpper)
                            command.Parameters.AddWithValue("@UploadDate", DBNull.Value)
                            command.Parameters.AddWithValue("@DelGoogleCalendar", 0)
                            command.Parameters.AddWithValue("@GoogleEmail", txtGoogleEmail.Text.ToUpper)
                            command.Parameters.AddWithValue("@GooglePassword", "")
                            command.Parameters.AddWithValue("@ColourCodeHtml", "")
                            command.Parameters.AddWithValue("@ColourCodeRGB", "")

                            If txtLocationId.SelectedIndex = 0 Then
                                command.Parameters.AddWithValue("@Location", "")
                            Else
                                command.Parameters.AddWithValue("@Location", txtLocationId.Text)
                            End If

                            command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                            command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                            command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                            command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                        ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                            command.Parameters.AddWithValue("@AssetNo", txtAssetNo.Text)
                            If txtAssetGroup.Text = "" Then
                                command.Parameters.AddWithValue("@AssetGrp", "")
                            Else
                                command.Parameters.AddWithValue("@AssetGrp", txtAssetGroup.Text)
                            End If

                            command.Parameters.AddWithValue("@AssetClass", txtAssetClass.Text)
                            command.Parameters.AddWithValue("@AssetRegNo", txtAssetRegNo.Text)
                            command.Parameters.AddWithValue("@Status", txtStatus.Text)
                            command.Parameters.AddWithValue("@OpStatus", txtOPStatus.Text)
                            command.Parameters.AddWithValue("@Descrip", txtDescription.Text)
                            command.Parameters.AddWithValue("@DefAddr", "")
                            command.Parameters.AddWithValue("@Depmtd", "")
                            command.Parameters.AddWithValue("@PurfmCType", "")
                            command.Parameters.AddWithValue("@PurfmCCode", "")
                            command.Parameters.AddWithValue("@PurfmInvNo", "")
                            command.Parameters.AddWithValue("@PurchRef", "")
                            command.Parameters.AddWithValue("@PurchDt", DBNull.Value)
                            command.Parameters.AddWithValue("@PurchVal", 0)
                            command.Parameters.AddWithValue("@ObbkVal", 0)
                            command.Parameters.AddWithValue("@ObbkDate", DBNull.Value)
                            command.Parameters.AddWithValue("@CurrAddr", "")
                            command.Parameters.AddWithValue("@InCharge", "")
                            command.Parameters.AddWithValue("@CurrCont", "")
                            command.Parameters.AddWithValue("@Phone1", "")
                            command.Parameters.AddWithValue("@Phone2", "")
                            If txtEstDateIn.Text = "" Then
                                command.Parameters.AddWithValue("@EstDateIn", DBNull.Value)
                            Else
                                command.Parameters.AddWithValue("@EstDateIn", Convert.ToDateTime(txtEstDateIn.Text))
                            End If

                            command.Parameters.AddWithValue("@Notes", "")
                            command.Parameters.AddWithValue("@ActDateIn", DBNull.Value)
                            command.Parameters.AddWithValue("@LastMoveId", "")
                            command.Parameters.AddWithValue("@Project", "")
                            If txtDateOut.Text = "" Then
                                command.Parameters.AddWithValue("@DateOut", DBNull.Value)
                            Else
                                command.Parameters.AddWithValue("@DateOut", Convert.ToDateTime(txtDateOut.Text))
                            End If
                            command.Parameters.AddWithValue("@Purpose", "")
                            command.Parameters.AddWithValue("@AssetCode", txtAssetCode.Text)
                            command.Parameters.AddWithValue("@Make", txtAssetMake.Text)
                            command.Parameters.AddWithValue("@Model", txtModel.Text)
                            command.Parameters.AddWithValue("@Year", "")
                            command.Parameters.AddWithValue("@Capacity", txtCapacity.Text)
                            command.Parameters.AddWithValue("@EngineNo", "")
                            command.Parameters.AddWithValue("@Value", 0)
                            command.Parameters.AddWithValue("@Remarks", txtRemarks.Text)
                            command.Parameters.AddWithValue("@PurfmCName", "")
                            command.Parameters.AddWithValue("@ReferenceOur", txtOurRef.Text)
                            command.Parameters.AddWithValue("@SvcFreq", 0)
                            If txtNextSvc.Text = "" Then
                                command.Parameters.AddWithValue("@NextSvcDate", DBNull.Value)
                            Else
                                command.Parameters.AddWithValue("@NextSvcDate", Convert.ToDateTime(txtNextSvc.Text))
                            End If
                            If txtLastSvc.Text = "" Then
                                command.Parameters.AddWithValue("@LastSvcDate", DBNull.Value)
                            Else
                                command.Parameters.AddWithValue("@LastSvcDate", Convert.ToDateTime(txtLastSvc.Text))
                            End If
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
                            command.Parameters.AddWithValue("@MfgYear", txtMfgYear.Text)
                            command.Parameters.AddWithValue("@CustCode", "")
                            command.Parameters.AddWithValue("@CustName", "")
                            command.Parameters.AddWithValue("@Reference", txtReference.Text)
                            If txtRefDate.Text = "" Then
                                command.Parameters.AddWithValue("@RefDate", DBNull.Value)
                            Else
                                command.Parameters.AddWithValue("@RefDate", Convert.ToDateTime(txtRefDate.Text))
                            End If
                            command.Parameters.AddWithValue("@PriceCode", txtPriceCode.Text)
                            command.Parameters.AddWithValue("@PROJECTCODE", "")
                            command.Parameters.AddWithValue("@PROJECTNAME", "")
                            command.Parameters.AddWithValue("@Cost", 0)
                            command.Parameters.AddWithValue("@SupplierCode", "")
                            command.Parameters.AddWithValue("@SupplierName", "")
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
                            If ddlInchargeID.Text = "-1" Then
                                command.Parameters.AddWithValue("@InChargeID", "")
                            Else
                                command.Parameters.AddWithValue("@InChargeID", ddlInchargeID.Text)
                            End If

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
                            command.Parameters.AddWithValue("@TechnicalSpecs", txtTechSpecs.Text)
                            command.Parameters.AddWithValue("@type", txtType.Text)
                            command.Parameters.AddWithValue("@CapacityUnitMS", txtCapacityUnit.Text)
                            If txtRegDate.Text = "" Then
                                command.Parameters.AddWithValue("@RegDate", DBNull.Value)
                            Else
                                command.Parameters.AddWithValue("@RegDate", Convert.ToDateTime(txtRegDate.Text))
                            End If
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
                            command.Parameters.AddWithValue("@GPSLabel", txtGPSLabel.Text)
                            command.Parameters.AddWithValue("@UploadDate", DBNull.Value)
                            command.Parameters.AddWithValue("@DelGoogleCalendar", 0)
                            command.Parameters.AddWithValue("@GoogleEmail", txtGoogleEmail.Text)
                            command.Parameters.AddWithValue("@GooglePassword", "")
                            command.Parameters.AddWithValue("@ColourCodeHtml", "")
                            command.Parameters.AddWithValue("@ColourCodeRGB", "")

                            If txtLocationId.SelectedIndex = 0 Then
                                command.Parameters.AddWithValue("@Location", "")
                            Else
                                command.Parameters.AddWithValue("@Location", txtLocationId.Text)
                            End If

                            command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                            command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                            command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                            command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                        End If

                        command.Connection = conn

                        command.ExecuteNonQuery()

                        '  MessageBox.Message.Alert(Page, "Record updated successfully!!!", "str")
                        lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED"
                        lblAlert.Text = ""
                    End If
                End If

                conn.Close()

            Catch ex As Exception
                MessageBox.Message.Alert(Page, ex.ToString, "str")
            End Try
            EnableControls()
            txtMode.Text = ""
        End If

        SqlDataSource1.SelectCommand = "select * from tblasset where rcno<>0 ORDER BY CREATEDON DESC;"
        SqlDataSource1.DataBind()
        GridView1.DataBind()

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
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "VEHICLE", txtAssetNo.Text, "DELETE", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
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
        If String.IsNullOrEmpty(txtRegDate.Text) = False Then
            If Date.TryParseExact(txtRegDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then
                txtRegDate.Text = d.ToShortDateString

            Else
                '  MessageBox.Message.Alert(Page, "Reg Date is invalid", "str")
                lblAlert.Text = "REG DATE IS INVALID"
                Return False
                Exit Function
            End If
        End If
        If String.IsNullOrEmpty(txtRefDate.Text) = False Then
            If Date.TryParseExact(txtRefDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then
                txtRefDate.Text = d.ToShortDateString

            Else
                ' MessageBox.Message.Alert(Page, "Ref Date is invalid", "str")
                lblAlert.Text = "REF DATE IS INVALID"
                Return False
                Exit Function
            End If
        End If
        If String.IsNullOrEmpty(txtDateOut.Text) = False Then
            If Date.TryParseExact(txtDateOut.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then
                txtDateOut.Text = d.ToShortDateString

            Else
                '  MessageBox.Message.Alert(Page, "Date Out is invalid", "str")
                lblAlert.Text = "DATE OUT IS INVALID"
                Return False
                Exit Function
            End If
        End If
        If String.IsNullOrEmpty(txtEstDateIn.Text) = False Then
            If Date.TryParseExact(txtEstDateIn.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then
                txtEstDateIn.Text = d.ToShortDateString

            Else
                '  MessageBox.Message.Alert(Page, "Est Date In is invalid", "str")
                lblAlert.Text = "EST DATE IS INVALID"
                Return False
                Exit Function
            End If
        End If
        If String.IsNullOrEmpty(txtNextSvc.Text) = False Then
            If Date.TryParseExact(txtNextSvc.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then
                txtNextSvc.Text = d.ToShortDateString

            Else
                '  MessageBox.Message.Alert(Page, "Next Svc Date is invalid", "str")
                lblAlert.Text = "NEXT SVC DATE IS INVALID"
                Return False
                Exit Function
            End If
        End If
        If String.IsNullOrEmpty(txtLastSvc.Text) = False Then
            If Date.TryParseExact(txtLastSvc.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then
                txtLastSvc.Text = d.ToShortDateString

            Else
                ' MessageBox.Message.Alert(Page, "Last Svc Date is invalid", "str")
                lblAlert.Text = "LAST SVC DATE IS INVALID"
                Return False
                Exit Function
            End If
        End If

        Return True
    End Function

    Protected Sub btnFilter_Click(sender As Object, e As EventArgs) Handles btnFilter.Click
        ' pup.ShowPopupWindow()
        txtSearchAssetNo.Focus()

    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            Dim qry As String
            qry = "select * from tblasset where 1=1 "
            If String.IsNullOrEmpty(txtSearchAssetNo.Text) = False Then
                txtAssetNo.Text = txtSearchAssetNo.Text

                qry = qry + " and assetno like '%" + txtSearchAssetNo.Text.Trim + "%'"
            End If
            If String.IsNullOrEmpty(txtSearchAssetRegNo.Text) = False Then
                txtAssetRegNo.Text = txtSearchAssetRegNo.Text
                qry = qry + " and assetregno like '%" + txtSearchAssetRegNo.Text.Trim + "%'"
            End If

            If String.IsNullOrEmpty(ddlSearchStatus.Text) = False Then
                txtStatus.Text = ddlSearchStatus.Text
                qry = qry + " and status = '" + ddlSearchStatus.Text + "'"
            End If
            'MessageBox.Message.Alert(Page, ddlInchargeID.Text + " " + TextBox1.Text, "str")

            If ddlSearchInchargeID.SelectedIndex = 0 Or ddlSearchInchargeID.Text = "." Then
                'MessageBox.Message.Alert(Page, ddlSearchInchargeID.Text, "str")

            Else
                If String.IsNullOrEmpty(ddlSearchInchargeID.Text) = False Then
                    ddlInchargeID.Text = ddlSearchInchargeID.Text
                    qry = qry + " and InchargeID = '" + ddlSearchInchargeID.Text.Trim + "'"
                End If

            End If

            qry = qry + " order by assetno;"
            '  txt.Text = qry
            SqlDataSource1.SelectCommand = qry
            SqlDataSource1.DataBind()
            GridView1.DataBind()
            txtSearchAssetNo.Text = ""
            txtSearchAssetRegNo.Text = ""
            ddlSearchInchargeID.ClearSelection()
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
        '   txt.Text = "select * from tblasset where rcno<>0;"
        SqlDataSource1.SelectCommand = "select * from tblasset where rcno<>0;"
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
End Class

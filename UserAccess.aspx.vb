Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Drawing


Partial Class UserAccess
    Inherits System.Web.UI.Page

    Public rcno As String

    Public Sub MakeMeNull()
        lblMessage.Text = ""
        lblAlert.Text = ""
        txtGroupAuthority.Text = ""
        txtMode.Text = ""
        txtRcno.Text = ""
        txtComments.Text = ""

        ''''''
        lblGroupAuthority.Text = ""
        lblGroupAuthority3.Text = ""
        lblGroupAccess.Text = ""
        lblGroupAuthority1.Text = ""
        lblGroupAuthority4.Text = ""
        lblGroupAuthority5.Text = ""
        lblGroupAuthority6.Text = ""
        lblGroupAuthority7.Text = ""
        lblGroupAuthorityAsset.Text = ""

       
        ''''''''''
    End Sub

    Private Sub EnableControls()
        btnSave.Enabled = False
        btnSave.ForeColor = System.Drawing.Color.Gray
        btnCancel.Enabled = False
        btnCancel.ForeColor = System.Drawing.Color.Gray

        btnADD.Enabled = True
        btnADD.ForeColor = System.Drawing.Color.Black

        btnEdit.Enabled = False
        btnEdit.ForeColor = System.Drawing.Color.Gray
        btnDelete.Enabled = False
        btnDelete.ForeColor = System.Drawing.Color.Gray

        btnQuit.Enabled = True
        btnQuit.ForeColor = System.Drawing.Color.Black
        btnPrint.Enabled = True
        btnPrint.ForeColor = System.Drawing.Color.Black
        txtGroupAuthority.Enabled = False
        txtComments.Enabled = False
        ' chkAccess.Enabled = False

        txtMode.Text = ""
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

        txtGroupAuthority.Enabled = True
        txtComments.Enabled = True
        '  chkAccess.Enabled = True

    End Sub


    Private Sub EnableLocationAccessControls()
        btnSaveLocationAccess.Enabled = False
        btnSaveLocationAccess.ForeColor = System.Drawing.Color.Gray
        btnCancelLocationAccess.Enabled = False
        btnCancelLocationAccess.ForeColor = System.Drawing.Color.Gray

        btnAddAccessLocation.Enabled = True
        btnAddAccessLocation.ForeColor = System.Drawing.Color.Black

        btnEditAccessLocation.Enabled = False
        btnEditAccessLocation.ForeColor = System.Drawing.Color.Gray

        ddlLocationID.Enabled = False

        'btnDelete.Enabled = False
        'btnDelete.ForeColor = System.Drawing.Color.Gray

        'btnQuit.Enabled = True
        'btnQuit.ForeColor = System.Drawing.Color.Black
        'btnPrint.Enabled = True
        'btnPrint.ForeColor = System.Drawing.Color.Black
        'txtGroupAuthority.Enabled = False
        'txtComments.Enabled = False
        ' chkAccess.Enabled = False

        'txtMode.Text = ""
    End Sub

    Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView1.PageIndexChanging

    End Sub
    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        Try
            If txtMode.Text = "Edit" Then
                lblAlert.Text = "CANNOT SELECT RECORD IN EDIT MODE. SAVE OR CANCEL TO PROCEED"
                Return
            End If

            EnableControls()
            MakeMeNull()
            Dim editindex As Integer = GridView1.SelectedIndex
            rcno = DirectCast(GridView1.Rows(editindex).FindControl("Label1"), Label).Text
            txtRcno.Text = rcno.ToString()

            If GridView1.SelectedRow.Cells(2).Text = "&nbsp;" Then
                txtGroupAuthority.Text = ""
            Else

                txtGroupAuthority.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(2).Text).ToString
            End If
            If GridView1.SelectedRow.Cells(3).Text = "&nbsp;" Then
                txtComments.Text = ""
            Else

                txtComments.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(3).Text).ToString
            End If

            'If GridView1.SelectedRow.Cells(4).Text = "&nbsp;" Or GridView1.SelectedRow.Cells(3).Text = "0" Then
            '    chkAccess.Checked = False
            'ElseIf GridView1.SelectedRow.Cells(4).Text = "N" Then
            '    chkAccess.Checked = True
            'End If

            '   txtMode.Text = "Edit"
            ' DisableControls()
            btnEdit.Enabled = True
            btnEdit.ForeColor = System.Drawing.Color.Black
            btnDelete.Enabled = True
            btnDelete.ForeColor = System.Drawing.Color.Black
            btnQuit.Enabled = True
            btnQuit.ForeColor = System.Drawing.Color.Black

            If CheckIfExists() = True Then
                txtExists.Text = "True"
            Else
                txtExists.Text = "False"
            End If

            'SELECT staffid, name, nric, type, datejoin, dateleft, appointment, payrollid, status FROM tblstaff where secgroupauthority='ADMINISTRATOR'
            SqlDataSource2.SelectCommand = "SELECT if(status='O', 'Active','InActive') as Active, staffid, name, nric, type, datejoin, dateleft, appointment, payrollid FROM tblstaff where secgroupauthority = '" & txtGroupAuthority.Text & "' order by Active,Staffid,Name"
            SqlDataSource2.DataBind()
            GridView2.DataBind()

            'SELECT * FROM tblGroupAccessLocation
            SqlDataSource3.SelectCommand = "SELECT * FROM tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "'"
            SqlDataSource3.DataBind()
            GridView3.DataBind()


            SqlDataSource4.SelectCommand = "SELECT * FROM tblassetgroupaccess where GroupAccess = '" & txtGroupAuthority.Text & "'"
            SqlDataSource4.DataBind()
            grdAssetGroup.DataBind()
            RetrieveData()

            txtGroupAuthority.Enabled = False

            If Session("SecGroupAuthority") <> "ADMINISTRATOR" Then
                AccessControl()
            End If
        Catch ex As Exception
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
            lblAlert.Text = ex.Message

        End Try
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
                'command.CommandText = "SELECT X0125,  X0125Add, X0125Edit, X0125Delete, X0125Print FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"
                command.CommandText = "SELECT x0704,  x0704Add, x0704Edit, x0704Delete FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"

                command.Connection = conn

                Dim dr As MySqlDataReader = command.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)
                conn.Close()

                If dt.Rows.Count > 0 Then
                    If Not IsDBNull(dt.Rows(0)("x0704")) Then
                        If String.IsNullOrEmpty(Convert.ToBoolean(dt.Rows(0)("x0704"))) = False Then
                            If Convert.ToBoolean(dt.Rows(0)("x0704")) = False Then
                                Response.Redirect("Home.aspx")
                            End If
                        End If
                    End If

                    If Not IsDBNull(dt.Rows(0)("x0704Add")) Then
                        If String.IsNullOrEmpty(dt.Rows(0)("x0704Add")) = False Then
                            Me.btnADD.Enabled = dt.Rows(0)("x0704Add").ToString()
                        End If
                    End If


                    If txtMode.Text = "View" Then
                        If Not IsDBNull(dt.Rows(0)("x0704Edit")) Then
                            If String.IsNullOrEmpty(dt.Rows(0)("x0704Edit")) = False Then
                                Me.btnEdit.Enabled = dt.Rows(0)("x0704Edit").ToString()
                            End If
                        End If

                        If Not IsDBNull(dt.Rows(0)("x0704Delete")) Then
                            If String.IsNullOrEmpty(dt.Rows(0)("x0704Delete")) = False Then
                                Me.btnDelete.Enabled = dt.Rows(0)("x0704Delete").ToString()
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


                End If
            End If

            '''''''''''''''''''Access Control 
        Catch ex As Exception
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
            lblAlert.Text = ex.Message.ToString
        End Try
    End Sub

    Private Sub RetrieveData()
        lblGroupAuthority.Text = txtGroupAuthority.Text
        lblGroupAuthority1.Text = txtGroupAuthority.Text
        lblGroupAccess.Text = txtGroupAuthority.Text
        lblGroupAuthority3.Text = txtGroupAuthority.Text
        lblGroupAuthority4.Text = txtGroupAuthority.Text
        lblGroupAuthority5.Text = txtGroupAuthority.Text
        lblGroupAuthority6.Text = txtGroupAuthority.Text
        lblGroupAuthority7.Text = txtGroupAuthority.Text
        lblGroupAuthorityAsset.Text = txtGroupAuthority.Text
        lblGroupAuthority8.Text = txtGroupAuthority.Text

        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()
        Dim command1 As MySqlCommand = New MySqlCommand

        command1.CommandType = CommandType.Text

        command1.CommandText = "SELECT * FROM tblGroupAccess where GroupAccess='" & txtGroupAuthority.Text & "'"
        command1.Connection = conn

        Dim dr As MySqlDataReader = command1.ExecuteReader()
        Dim dt As New DataTable
        dt.Load(dr)

        If dt.Rows.Count > 0 Then

            'AR Modules


            '1 AR Module - Invoice

            If dt.Rows(0)("x0252").ToString = "1" Then
                chkInvoiceList.Items.FindByValue("Access").Selected = True
            Else
                chkInvoiceList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x0252Add").ToString = "1" Then
                chkInvoiceList.Items.FindByValue("Add").Selected = True
            Else
                chkInvoiceList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0252Edit").ToString = "1" Then
                chkInvoiceList.Items.FindByValue("Edit").Selected = True
            Else
                chkInvoiceList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0252Delete").ToString = "1" Then
                chkInvoiceList.Items.FindByValue("Delete").Selected = True
            Else
                chkInvoiceList.Items.FindByValue("Delete").Selected = False
            End If

            If dt.Rows(0)("x0252Print").ToString = "1" Then
                chkInvoiceList.Items.FindByValue("Print").Selected = True
            Else
                chkInvoiceList.Items.FindByValue("Print").Selected = False
            End If
            If dt.Rows(0)("x0252Post").ToString = "1" Then
                chkInvoiceList.Items.FindByValue("Post").Selected = True
            Else
                chkInvoiceList.Items.FindByValue("Post").Selected = False
            End If
            If dt.Rows(0)("x0252Reverse").ToString = "1" Then
                chkInvoiceList.Items.FindByValue("Reverse").Selected = True
            Else
                chkInvoiceList.Items.FindByValue("Reverse").Selected = False
            End If

            If dt.Rows(0)("x0252SubmitEInvoice").ToString = "1" Then
                chkInvoiceList.Items.FindByValue("SubmitEInvoice").Selected = True
            Else
                chkInvoiceList.Items.FindByValue("SubmitEInvoice").Selected = False
            End If

            If dt.Rows(0)("x0252CancelEInvoice").ToString = "1" Then
                chkInvoiceList.Items.FindByValue("CancelEInvoice").Selected = True
            Else
                chkInvoiceList.Items.FindByValue("CancelEInvoice").Selected = False
            End If

            'Consolidated
            If dt.Rows(0)("x0259").ToString = "1" Then
                ChkConsolidatedInvoiceList.Items.FindByValue("Access").Selected = True
            Else
                ChkConsolidatedInvoiceList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x0259Add").ToString = "1" Then
                ChkConsolidatedInvoiceList.Items.FindByValue("Add").Selected = True
            Else
                ChkConsolidatedInvoiceList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0259Edit").ToString = "1" Then
                ChkConsolidatedInvoiceList.Items.FindByValue("Edit").Selected = True
            Else
                ChkConsolidatedInvoiceList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0259Void").ToString = "1" Then
                ChkConsolidatedInvoiceList.Items.FindByValue("Void").Selected = True
            Else
                ChkConsolidatedInvoiceList.Items.FindByValue("Void").Selected = False
            End If

            If dt.Rows(0)("x0259Print").ToString = "1" Then
                ChkConsolidatedInvoiceList.Items.FindByValue("Print").Selected = True
            Else
                ChkConsolidatedInvoiceList.Items.FindByValue("Print").Selected = False
            End If

            If dt.Rows(0)("x0259Submit").ToString = "1" Then
                ChkConsolidatedInvoiceList.Items.FindByValue("SubmitInvoice").Selected = True
            Else
                ChkConsolidatedInvoiceList.Items.FindByValue("SubmitInvoice").Selected = False
            End If

            If dt.Rows(0)("x0259Cancel").ToString = "1" Then
                ChkConsolidatedInvoiceList.Items.FindByValue("CancelInvoice").Selected = True
            Else
                ChkConsolidatedInvoiceList.Items.FindByValue("CancelInvoice").Selected = False
            End If


            If dt.Rows(0)("x0255QReceipt").ToString = "1" Then
                chkInvoiceList.Items.FindByValue("Quick Receipt").Selected = True
            Else
                chkInvoiceList.Items.FindByValue("Quick Receipt").Selected = False
            End If

            If dt.Rows(0)("x0252MultiPrint").ToString = "1" Then
                chkInvoiceList.Items.FindByValue("Multi-Print").Selected = True
            Else
                chkInvoiceList.Items.FindByValue("Multi-Print").Selected = False
            End If



            If dt.Rows(0)("x0252EditCompanyName").ToString = "1" Then
                chkInvoiceList.Items.FindByValue("EditInvEditAccountName").Selected = True
            Else
                chkInvoiceList.Items.FindByValue("EditInvEditAccountName").Selected = False
            End If

            If dt.Rows(0)("x0252EditBillingAddress").ToString = "1" Then
                chkInvoiceList.Items.FindByValue("EditInvEditBillingDetail").Selected = True
            Else
                chkInvoiceList.Items.FindByValue("EditInvEditBillingDetail").Selected = False
            End If

            If dt.Rows(0)("x0252EditOurRef").ToString = "1" Then
                chkInvoiceList.Items.FindByValue("EditInvEditOurRef").Selected = True
            Else
                chkInvoiceList.Items.FindByValue("EditInvEditOurRef").Selected = False
            End If

            If dt.Rows(0)("x0252EditSalesman").ToString = "1" Then
                chkInvoiceList.Items.FindByValue("EditInvEditSalesman").Selected = True
            Else
                chkInvoiceList.Items.FindByValue("EditInvEditSalesman").Selected = False
            End If


            If dt.Rows(0)("x0252EditRemarks").ToString = "1" Then
                chkInvoiceList.Items.FindByValue("EditInvEditRemarks").Selected = True
            Else
                chkInvoiceList.Items.FindByValue("EditInvEditRemarks").Selected = False
            End If

            If dt.Rows(0)("x0252FileUpload").ToString = "1" Then
                chkInvoiceList.Items.FindByValue("FileUpload").Selected = True
            Else
                chkInvoiceList.Items.FindByValue("FileUpload").Selected = False
            End If



            If dt.Rows(0)("x0252PrintInvoice").ToString = "1" Then
                chkInvoiceList.Items.FindByValue("PrintInvoice").Selected = True
            Else
                chkInvoiceList.Items.FindByValue("PrintInvoice").Selected = False
            End If


            If dt.Rows(0)("x0252ExportInvoice").ToString = "1" Then
                chkInvoiceList.Items.FindByValue("ExportInvoice").Selected = True
            Else
                chkInvoiceList.Items.FindByValue("ExportInvoice").Selected = False
            End If


            If dt.Rows(0)("x0252ViewInvoice").ToString = "1" Then
                chkInvoiceList.Items.FindByValue("ViewInvoice").Selected = True
            Else
                chkInvoiceList.Items.FindByValue("ViewInvoice").Selected = False
            End If


            If dt.Rows(0)("x0252EmailInvoice").ToString = "1" Then
                chkInvoiceList.Items.FindByValue("EmailInvoice").Selected = True
            Else
                chkInvoiceList.Items.FindByValue("EmailInvoice").Selected = False
            End If

            If dt.Rows(0)("x252DeSelectContractGroup").ToString = "1" Then
                chkInvoiceList.Items.FindByValue("DeSelectContractGroup").Selected = True
            Else
                chkInvoiceList.Items.FindByValue("DeSelectContractGroup").Selected = False
            End If

            If dt.Rows(0)("x0252ViewInvFormat1").ToString = "1" Then
                chkInvoiceList.Items.FindByValue("InvoiceFormat1").Selected = True
            Else
                chkInvoiceList.Items.FindByValue("InvoiceFormat1").Selected = False
            End If

            If dt.Rows(0)("x0252ViewInvFormat2").ToString = "1" Then
                chkInvoiceList.Items.FindByValue("InvoiceFormat2").Selected = True
            Else
                chkInvoiceList.Items.FindByValue("InvoiceFormat2").Selected = False
            End If

            If dt.Rows(0)("x0252ViewInvFormat3").ToString = "1" Then
                chkInvoiceList.Items.FindByValue("InvoiceFormat3").Selected = True
            Else
                chkInvoiceList.Items.FindByValue("InvoiceFormat3").Selected = False
            End If

            If dt.Rows(0)("x0252ViewInvFormat4").ToString = "1" Then
                chkInvoiceList.Items.FindByValue("InvoiceFormat4").Selected = True
            Else
                chkInvoiceList.Items.FindByValue("InvoiceFormat4").Selected = False
            End If

            If dt.Rows(0)("x0252ViewInvFormat5").ToString = "1" Then
                chkInvoiceList.Items.FindByValue("InvoiceFormat5").Selected = True
            Else
                chkInvoiceList.Items.FindByValue("InvoiceFormat5").Selected = False
            End If

            If dt.Rows(0)("x0252ViewInvFormat6").ToString = "1" Then
                chkInvoiceList.Items.FindByValue("InvoiceFormat6").Selected = True
            Else
                chkInvoiceList.Items.FindByValue("InvoiceFormat6").Selected = False
            End If

            If dt.Rows(0)("x0252ViewInvFormat7").ToString = "1" Then
                chkInvoiceList.Items.FindByValue("InvoiceFormat7").Selected = True
            Else
                chkInvoiceList.Items.FindByValue("InvoiceFormat7").Selected = False
            End If

            If dt.Rows(0)("x0252ViewInvFormat8").ToString = "1" Then
                chkInvoiceList.Items.FindByValue("InvoiceFormat8").Selected = True
            Else
                chkInvoiceList.Items.FindByValue("InvoiceFormat8").Selected = False
            End If

            If dt.Rows(0)("x0252ViewInvFormat9").ToString = "1" Then
                chkInvoiceList.Items.FindByValue("InvoiceFormat9").Selected = True
            Else
                chkInvoiceList.Items.FindByValue("InvoiceFormat9").Selected = False
            End If

            If dt.Rows(0)("x0252ViewInvFormat10").ToString = "1" Then
                chkInvoiceList.Items.FindByValue("InvoiceFormat10").Selected = True
            Else
                chkInvoiceList.Items.FindByValue("InvoiceFormat10").Selected = False
            End If

            If dt.Rows(0)("x0252ViewInvFormat11").ToString = "1" Then
                chkInvoiceList.Items.FindByValue("InvoiceFormat11").Selected = True
            Else
                chkInvoiceList.Items.FindByValue("InvoiceFormat11").Selected = False
            End If

            If dt.Rows(0)("x0252ViewInvFormat12").ToString = "1" Then
                chkInvoiceList.Items.FindByValue("InvoiceFormat12").Selected = True
            Else
                chkInvoiceList.Items.FindByValue("InvoiceFormat12").Selected = False
            End If

            If dt.Rows(0)("x0252ViewInvFormat13").ToString = "1" Then
                chkInvoiceList.Items.FindByValue("InvoiceFormat13").Selected = True
            Else
                chkInvoiceList.Items.FindByValue("InvoiceFormat13").Selected = False
            End If

            If dt.Rows(0)("x0252ViewInvFormat14").ToString = "1" Then
                chkInvoiceList.Items.FindByValue("InvoiceFormat14").Selected = True
            Else
                chkInvoiceList.Items.FindByValue("InvoiceFormat14").Selected = False
            End If
            '2 AR Module - Batch Invoice

            If dt.Rows(0)("x0253").ToString = "1" Then
                chkBatchInvoiceList.Items.FindByValue("Access").Selected = True
            Else
                chkBatchInvoiceList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x0253Add").ToString = "1" Then
                chkBatchInvoiceList.Items.FindByValue("Add").Selected = True
            Else
                chkBatchInvoiceList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0253Edit").ToString = "1" Then
                chkBatchInvoiceList.Items.FindByValue("Edit").Selected = True
            Else
                chkBatchInvoiceList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0253Delete").ToString = "1" Then
                chkBatchInvoiceList.Items.FindByValue("Delete").Selected = True
            Else
                chkBatchInvoiceList.Items.FindByValue("Delete").Selected = False
            End If

            If dt.Rows(0)("x0253Print").ToString = "1" Then
                chkBatchInvoiceList.Items.FindByValue("Print").Selected = True
            Else
                chkBatchInvoiceList.Items.FindByValue("Print").Selected = False
            End If
            If dt.Rows(0)("x0253Post").ToString = "1" Then
                chkBatchInvoiceList.Items.FindByValue("Post").Selected = True
            Else
                chkBatchInvoiceList.Items.FindByValue("Post").Selected = False
            End If
            If dt.Rows(0)("x0253Reverse").ToString = "1" Then
                chkBatchInvoiceList.Items.FindByValue("Reverse").Selected = True
            Else
                chkBatchInvoiceList.Items.FindByValue("Reverse").Selected = False
            End If


            If dt.Rows(0)("x0253MultiPrint").ToString = "1" Then
                chkBatchInvoiceList.Items.FindByValue("Multi-Print").Selected = True
            Else
                chkBatchInvoiceList.Items.FindByValue("Multi-Print").Selected = False
            End If

            If dt.Rows(0)("x253DeSelectContractGroup").ToString = "1" Then
                chkBatchInvoiceList.Items.FindByValue("DeSelectContractGroup").Selected = True
            Else
                chkBatchInvoiceList.Items.FindByValue("DeSelectContractGroup").Selected = False
            End If


            '3 AR Module - Receipt

            If dt.Rows(0)("x0255").ToString = "1" Then
                chkReceiptList.Items.FindByValue("Access").Selected = True
            Else
                chkReceiptList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x0255Add").ToString = "1" Then
                chkReceiptList.Items.FindByValue("Add").Selected = True
            Else
                chkReceiptList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0255Edit").ToString = "1" Then
                chkReceiptList.Items.FindByValue("Edit").Selected = True
            Else
                chkReceiptList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0255Delete").ToString = "1" Then
                chkReceiptList.Items.FindByValue("Delete").Selected = True
            Else
                chkReceiptList.Items.FindByValue("Delete").Selected = False
            End If


            If dt.Rows(0)("x0255Print").ToString = "1" Then
                chkReceiptList.Items.FindByValue("Print").Selected = True
            Else
                chkReceiptList.Items.FindByValue("Print").Selected = False
            End If
            If dt.Rows(0)("x0255Post").ToString = "1" Then
                chkReceiptList.Items.FindByValue("Post").Selected = True
            Else
                chkReceiptList.Items.FindByValue("Post").Selected = False
            End If
            If dt.Rows(0)("x0255Reverse").ToString = "1" Then
                chkReceiptList.Items.FindByValue("Reverse").Selected = True
            Else
                chkReceiptList.Items.FindByValue("Reverse").Selected = False
            End If

            If dt.Rows(0)("x0255ChequeReturn").ToString = "1" Then
                chkReceiptList.Items.FindByValue("ChequeReturn").Selected = True
            Else
                chkReceiptList.Items.FindByValue("ChequeReturn").Selected = False
            End If

            If dt.Rows(0)("x0255FileUpload").ToString = "1" Then
                chkReceiptList.Items.FindByValue("FileUpload").Selected = True
            Else
                chkReceiptList.Items.FindByValue("FileUpload").Selected = False
            End If

            '4 AR Module - CN

            If dt.Rows(0)("x0256").ToString = "1" Then
                chkCreditNoteList.Items.FindByValue("Access").Selected = True
            Else
                chkCreditNoteList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x0256Add").ToString = "1" Then
                chkCreditNoteList.Items.FindByValue("Add").Selected = True
            Else
                chkCreditNoteList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0256Edit").ToString = "1" Then
                chkCreditNoteList.Items.FindByValue("Edit").Selected = True
            Else
                chkCreditNoteList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0256Delete").ToString = "1" Then
                chkCreditNoteList.Items.FindByValue("Delete").Selected = True
            Else
                chkCreditNoteList.Items.FindByValue("Delete").Selected = False
            End If


            If dt.Rows(0)("x0256Print").ToString = "1" Then
                chkCreditNoteList.Items.FindByValue("Print").Selected = True
            Else
                chkCreditNoteList.Items.FindByValue("Print").Selected = False
            End If

            If dt.Rows(0)("x0256Post").ToString = "1" Then
                chkCreditNoteList.Items.FindByValue("Post").Selected = True
            Else
                chkCreditNoteList.Items.FindByValue("Post").Selected = False
            End If

            If dt.Rows(0)("x0256Reverse").ToString = "1" Then
                chkCreditNoteList.Items.FindByValue("Reverse").Selected = True
            Else
                chkCreditNoteList.Items.FindByValue("Reverse").Selected = False
            End If

            If dt.Rows(0)("x0256SubmitECNDN").ToString = "1" Then
                chkCreditNoteList.Items.FindByValue("SubmitECNDN").Selected = True
            Else
                chkCreditNoteList.Items.FindByValue("SubmitECNDN").Selected = False
            End If

            If dt.Rows(0)("x0256CancelECNDN").ToString = "1" Then
                chkCreditNoteList.Items.FindByValue("CancelECNDN").Selected = True
            Else
                chkCreditNoteList.Items.FindByValue("CancelECNDN").Selected = False
            End If


            If dt.Rows(0)("x0256EditCompanyName").ToString = "1" Then
                chkCreditNoteList.Items.FindByValue("EditCNEditAccountName").Selected = True
            Else
                chkCreditNoteList.Items.FindByValue("EditCNEditAccountName").Selected = False
            End If

            If dt.Rows(0)("x0256EditBillingAddress").ToString = "1" Then
                chkCreditNoteList.Items.FindByValue("EditCNEditBillingDetail").Selected = True
            Else
                chkCreditNoteList.Items.FindByValue("EditCNEditBillingDetail").Selected = False
            End If

            If dt.Rows(0)("x0256FileUpload").ToString = "1" Then
                chkCreditNoteList.Items.FindByValue("FileUpload").Selected = True
            Else
                chkCreditNoteList.Items.FindByValue("FileUpload").Selected = False
            End If

            '5 AR Module - Adjustment

            If dt.Rows(0)("x0258").ToString = "1" Then
                chkAdjustmentList.Items.FindByValue("Access").Selected = True
            Else
                chkAdjustmentList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x0258Add").ToString = "1" Then
                chkAdjustmentList.Items.FindByValue("Add").Selected = True
            Else
                chkAdjustmentList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0258Edit").ToString = "1" Then
                chkAdjustmentList.Items.FindByValue("Edit").Selected = True
            Else
                chkAdjustmentList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0258Delete").ToString = "1" Then
                chkAdjustmentList.Items.FindByValue("Delete").Selected = True
            Else
                chkAdjustmentList.Items.FindByValue("Delete").Selected = False
            End If


            If dt.Rows(0)("x0258Print").ToString = "1" Then
                chkAdjustmentList.Items.FindByValue("Print").Selected = True
            Else
                chkAdjustmentList.Items.FindByValue("Print").Selected = False
            End If
            If dt.Rows(0)("x0258Post").ToString = "1" Then
                chkAdjustmentList.Items.FindByValue("Post").Selected = True
            Else
                chkAdjustmentList.Items.FindByValue("Post").Selected = False
            End If
            If dt.Rows(0)("x0258Reverse").ToString = "1" Then
                chkAdjustmentList.Items.FindByValue("Reverse").Selected = True
            Else
                chkAdjustmentList.Items.FindByValue("Reverse").Selected = False
            End If

            If dt.Rows(0)("x0258FileUpload").ToString = "1" Then
                chkAdjustmentList.Items.FindByValue("FileUpload").Selected = True
            Else
                chkAdjustmentList.Items.FindByValue("FileUpload").Selected = False
            End If

            'Setup Access


            ''''''''''''''''''''''''''''''''''''''''''''''




            ''''''''''''''''''''''''''''''''''''''''''''''''''

            '1 Setup - Billing Codes

            If dt.Rows(0)("x0251").ToString = "1" Then
                chkCompanyGroupList.Items.FindByValue("Access").Selected = True
            Else
                chkCompanyGroupList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x0251Add").ToString = "1" Then
                chkCompanyGroupList.Items.FindByValue("Add").Selected = True
            Else
                chkCompanyGroupList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0251Edit").ToString = "1" Then
                chkCompanyGroupList.Items.FindByValue("Edit").Selected = True
            Else
                chkCompanyGroupList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0251Delete").ToString = "1" Then
                chkCompanyGroupList.Items.FindByValue("Delete").Selected = True
            Else
                chkCompanyGroupList.Items.FindByValue("Delete").Selected = False
            End If


            '2. Setup - Chart Of Accounts Access

            If dt.Rows(0)("x0209").ToString = "1" Then
                chkCOAList.Items.FindByValue("Access").Selected = True
            Else
                chkCOAList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x0209Add").ToString = "1" Then
                chkCOAList.Items.FindByValue("Add").Selected = True
            Else
                chkCOAList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0209Edit").ToString = "1" Then
                chkCOAList.Items.FindByValue("Edit").Selected = True
            Else
                chkCOAList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0209Delete").ToString = "1" Then
                chkCOAList.Items.FindByValue("Delete").Selected = True
            Else
                chkCOAList.Items.FindByValue("Delete").Selected = False
            End If

            '3 Setup - City Access

            If dt.Rows(0)("x0107").ToString = "1" Then
                chkCityList.Items.FindByValue("Access").Selected = True
            Else
                chkCityList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x0107Add").ToString = "1" Then
                chkCityList.Items.FindByValue("Add").Selected = True
            Else
                chkCityList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0107Edit").ToString = "1" Then
                chkCityList.Items.FindByValue("Edit").Selected = True
            Else
                chkCityList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0107Delete").ToString = "1" Then
                chkCityList.Items.FindByValue("Delete").Selected = True
            Else
                chkCityList.Items.FindByValue("Delete").Selected = False
            End If

            '4 Setup - Companygroup Access

            If dt.Rows(0)("x0109").ToString = "1" Then
                chkCompanyGroupList.Items.FindByValue("Access").Selected = True
            Else
                chkCompanyGroupList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x0109Add").ToString = "1" Then
                chkCompanyGroupList.Items.FindByValue("Add").Selected = True
            Else
                chkCompanyGroupList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0109Edit").ToString = "1" Then
                chkCompanyGroupList.Items.FindByValue("Edit").Selected = True
            Else
                chkCompanyGroupList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0109Delete").ToString = "1" Then
                chkCompanyGroupList.Items.FindByValue("Delete").Selected = True
            Else
                chkCompanyGroupList.Items.FindByValue("Delete").Selected = False
            End If


            '5 Setup - Contract Group Category

            If dt.Rows(0)("x0151").ToString = "1" Then
                chkContractGroupCategoryList.Items.FindByValue("Access").Selected = True
            Else
                chkContractGroupCategoryList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x0151Add").ToString = "1" Then
                chkContractGroupCategoryList.Items.FindByValue("Add").Selected = True
            Else
                chkContractGroupCategoryList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0151Edit").ToString = "1" Then
                chkContractGroupCategoryList.Items.FindByValue("Edit").Selected = True
            Else
                chkContractGroupCategoryList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0151Delete").ToString = "1" Then
                chkContractGroupCategoryList.Items.FindByValue("Delete").Selected = True
            Else
                chkContractGroupCategoryList.Items.FindByValue("Delete").Selected = False
            End If

            '6 Setup - country Access

            If dt.Rows(0)("x0112").ToString = "1" Then
                chkCountryList.Items.FindByValue("Access").Selected = True
            Else
                chkCountryList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x0112Add").ToString = "1" Then
                chkCountryList.Items.FindByValue("Add").Selected = True
            Else
                chkCountryList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0112Edit").ToString = "1" Then
                chkCountryList.Items.FindByValue("Edit").Selected = True
            Else
                chkCountryList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0112Delete").ToString = "1" Then
                chkCountryList.Items.FindByValue("Delete").Selected = True
            Else
                chkCountryList.Items.FindByValue("Delete").Selected = False
            End If



            '7 Setup - Currency Access

            If dt.Rows(0)("x0113").ToString = "1" Then
                chkCurrencyList.Items.FindByValue("Access").Selected = True
            Else
                chkCurrencyList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x0113Add").ToString = "1" Then
                chkCurrencyList.Items.FindByValue("Add").Selected = True
            Else
                chkCurrencyList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0113Edit").ToString = "1" Then
                chkCurrencyList.Items.FindByValue("Edit").Selected = True
            Else
                chkCurrencyList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0113Delete").ToString = "1" Then
                chkCurrencyList.Items.FindByValue("Delete").Selected = True
            Else
                chkCurrencyList.Items.FindByValue("Delete").Selected = False
            End If


            '8 Setup - Department

            If dt.Rows(0)("x0152").ToString = "1" Then
                chkDepartmentList.Items.FindByValue("Access").Selected = True
            Else
                chkDepartmentList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x0152Add").ToString = "1" Then
                chkDepartmentList.Items.FindByValue("Add").Selected = True
            Else
                chkDepartmentList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0152Edit").ToString = "1" Then
                chkDepartmentList.Items.FindByValue("Edit").Selected = True
            Else
                chkDepartmentList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0152Delete").ToString = "1" Then
                chkDepartmentList.Items.FindByValue("Delete").Selected = True
            Else
                chkDepartmentList.Items.FindByValue("Delete").Selected = False
            End If

            '9 Setup - EmailSetup Access

            If dt.Rows(0)("x0110").ToString = "1" Then
                chkEmailSetupList.Items.FindByValue("Access").Selected = True
            Else
                chkEmailSetupList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x0110Add").ToString = "1" Then
                chkEmailSetupList.Items.FindByValue("Add").Selected = True
            Else
                chkEmailSetupList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0110Edit").ToString = "1" Then
                chkEmailSetupList.Items.FindByValue("Edit").Selected = True
            Else
                chkEmailSetupList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0110Delete").ToString = "1" Then
                chkEmailSetupList.Items.FindByValue("Delete").Selected = True
            Else
                chkEmailSetupList.Items.FindByValue("Delete").Selected = False
            End If


            '10 Setup - Event Log

            If dt.Rows(0)("x0153").ToString = "1" Then
                chkEventLogList.Items.FindByValue("Access").Selected = True
            Else
                chkEventLogList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x0153Add").ToString = "1" Then
                chkEventLogList.Items.FindByValue("Add").Selected = True
            Else
                chkEventLogList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0153Edit").ToString = "1" Then
                chkEventLogList.Items.FindByValue("Edit").Selected = True
            Else
                chkEventLogList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0153Delete").ToString = "1" Then
                chkEventLogList.Items.FindByValue("Delete").Selected = True
            Else
                chkEventLogList.Items.FindByValue("Delete").Selected = False
            End If


            '11 Setup - Industry Access

            If dt.Rows(0)("x0104").ToString = "1" Then
                chkIndustryList.Items.FindByValue("Access").Selected = True
            Else
                chkIndustryList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x0104Add").ToString = "1" Then
                chkIndustryList.Items.FindByValue("Add").Selected = True
            Else
                chkIndustryList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0104Edit").ToString = "1" Then
                chkIndustryList.Items.FindByValue("Edit").Selected = True
            Else
                chkIndustryList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0104Delete").ToString = "1" Then
                chkIndustryList.Items.FindByValue("Delete").Selected = True
            Else
                chkIndustryList.Items.FindByValue("Delete").Selected = False
            End If


            '12 Setup - Invoice Frequency Codes

            If dt.Rows(0)("x0154").ToString = "1" Then
                chkInvFreqList.Items.FindByValue("Access").Selected = True
            Else
                chkInvFreqList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x0154Add").ToString = "1" Then
                chkInvFreqList.Items.FindByValue("Add").Selected = True
            Else
                chkInvFreqList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0154Edit").ToString = "1" Then
                chkInvFreqList.Items.FindByValue("Edit").Selected = True
            Else
                chkInvFreqList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0154Delete").ToString = "1" Then
                chkInvFreqList.Items.FindByValue("Delete").Selected = True
            Else
                chkInvFreqList.Items.FindByValue("Delete").Selected = False
            End If

            '13 Setup - Holiday Access

            If dt.Rows(0)("x1721Access").ToString = "1" Then
                chkHolidayList.Items.FindByValue("Access").Selected = True
            Else
                chkHolidayList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x1721Add").ToString = "1" Then
                chkHolidayList.Items.FindByValue("Add").Selected = True
            Else
                chkHolidayList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x1721Edit").ToString = "1" Then
                chkHolidayList.Items.FindByValue("Edit").Selected = True
            Else
                chkHolidayList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x1721Delete").ToString = "1" Then
                chkHolidayList.Items.FindByValue("Delete").Selected = True
            Else
                chkHolidayList.Items.FindByValue("Delete").Selected = False
            End If

            '14 Setup - Locationgroup Access

            If dt.Rows(0)("x0118").ToString = "1" Then
                chkLocGroupList.Items.FindByValue("Access").Selected = True
            Else
                chkLocGroupList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x0118Add").ToString = "1" Then
                chkLocGroupList.Items.FindByValue("Add").Selected = True
            Else
                chkLocGroupList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0118Edit").ToString = "1" Then
                chkLocGroupList.Items.FindByValue("Edit").Selected = True
            Else
                chkLocGroupList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0118Delete").ToString = "1" Then
                chkLocGroupList.Items.FindByValue("Delete").Selected = True
            Else
                chkLocGroupList.Items.FindByValue("Delete").Selected = False
            End If

            '15 Setup - Mass Change

            If dt.Rows(0)("x0155").ToString = "1" Then
                chkMassChangeList.Items.FindByValue("Access").Selected = True
            Else
                chkMassChangeList.Items.FindByValue("Access").Selected = False
            End If
            'If dt.Rows(0)("x0155Add").ToString = "1" Then
            '    chkMassChangeList.Items.FindByValue("Add").Selected = True
            'Else
            '    chkMassChangeList.Items.FindByValue("Add").Selected = False
            'End If
            'If dt.Rows(0)("x0155Edit").ToString = "1" Then
            '    chkMassChangeList.Items.FindByValue("Edit").Selected = True
            'Else
            '    chkMassChangeList.Items.FindByValue("Edit").Selected = False
            'End If
            'If dt.Rows(0)("x0155Delete").ToString = "1" Then
            '    chkMassChangeList.Items.FindByValue("Delete").Selected = True
            'Else
            '    chkMassChangeList.Items.FindByValue("Delete").Selected = False
            'End If

            '16 Setup - Moble Device

            If dt.Rows(0)("x0156").ToString = "1" Then
                chkMobileList.Items.FindByValue("Access").Selected = True
            Else
                chkMobileList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x0156Add").ToString = "1" Then
                chkMobileList.Items.FindByValue("Add").Selected = True
            Else
                chkMobileList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0156Edit").ToString = "1" Then
                chkMobileList.Items.FindByValue("Edit").Selected = True
            Else
                chkMobileList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0156Delete").ToString = "1" Then
                chkMobileList.Items.FindByValue("Delete").Selected = True
            Else
                chkMobileList.Items.FindByValue("Delete").Selected = False
            End If

            '17 Setup - Postal To Location

            If dt.Rows(0)("x0157").ToString = "1" Then
                chkPostalList.Items.FindByValue("Access").Selected = True
            Else
                chkPostalList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x0157Add").ToString = "1" Then
                chkPostalList.Items.FindByValue("Add").Selected = True
            Else
                chkPostalList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0157Edit").ToString = "1" Then
                chkPostalList.Items.FindByValue("Edit").Selected = True
            Else
                chkPostalList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0157Delete").ToString = "1" Then
                chkPostalList.Items.FindByValue("Delete").Selected = True
            Else
                chkPostalList.Items.FindByValue("Delete").Selected = False
            End If

            '18 Setup - Schedule Type Access

            If dt.Rows(0)("x0103").ToString = "1" Then
                chkSchTypeList.Items.FindByValue("Access").Selected = True
            Else
                chkSchTypeList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x0103Add").ToString = "1" Then
                chkSchTypeList.Items.FindByValue("Add").Selected = True
            Else
                chkSchTypeList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0103Edit").ToString = "1" Then
                chkSchTypeList.Items.FindByValue("Edit").Selected = True
            Else
                chkSchTypeList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0103Delete").ToString = "1" Then
                chkSchTypeList.Items.FindByValue("Delete").Selected = True
            Else
                chkSchTypeList.Items.FindByValue("Delete").Selected = False
            End If

            '19 Setup - Service Frequency

            If dt.Rows(0)("x0158").ToString = "1" Then
                chkSvcFreqList.Items.FindByValue("Access").Selected = True
            Else
                chkSvcFreqList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x0158Add").ToString = "1" Then
                chkSvcFreqList.Items.FindByValue("Add").Selected = True
            Else
                chkSvcFreqList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0158Edit").ToString = "1" Then
                chkSvcFreqList.Items.FindByValue("Edit").Selected = True
            Else
                chkSvcFreqList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0158Delete").ToString = "1" Then
                chkSvcFreqList.Items.FindByValue("Delete").Selected = True
            Else
                chkSvcFreqList.Items.FindByValue("Delete").Selected = False
            End If

            '20 Setup - Service master

            If dt.Rows(0)("x0159").ToString = "1" Then
                chkServiceMasterList.Items.FindByValue("Access").Selected = True
            Else
                chkServiceMasterList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x0159Add").ToString = "1" Then
                chkServiceMasterList.Items.FindByValue("Add").Selected = True
            Else
                chkServiceMasterList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0159Edit").ToString = "1" Then
                chkServiceMasterList.Items.FindByValue("Edit").Selected = True
            Else
                chkServiceMasterList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0159Delete").ToString = "1" Then
                chkServiceMasterList.Items.FindByValue("Delete").Selected = True
            Else
                chkServiceMasterList.Items.FindByValue("Delete").Selected = False
            End If


            '21 UserStaff Access

            If dt.Rows(0)("X0304").ToString = "1" Then
                chkUserStaffList.Items.FindByValue("Access").Selected = True
            Else
                chkUserStaffList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("X0304Add").ToString = "1" Then
                chkUserStaffList.Items.FindByValue("Add").Selected = True
            Else
                chkUserStaffList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("X0304Edit").ToString = "1" Then
                chkUserStaffList.Items.FindByValue("Edit").Selected = True
            Else
                chkUserStaffList.Items.FindByValue("Edit").Selected = False
            End If

            If dt.Rows(0)("X0304Delete").ToString = "1" Then
                chkUserStaffList.Items.FindByValue("Delete").Selected = True
            Else
                chkUserStaffList.Items.FindByValue("Delete").Selected = False
            End If


            'If dt.Rows(0)("X0304Trans").ToString = "1" Then
            '    chkUserStaffList.Items.FindByValue("Trans").Selected = True
            'Else
            '    chkUserStaffList.Items.FindByValue("Trans").Selected = False
            'End If
            'If dt.Rows(0)("X0304Security").ToString = "1" Then
            '    chkUserStaffList.Items.FindByValue("Security").Selected = True
            'Else
            '    chkUserStaffList.Items.FindByValue("Security").Selected = False
            'End If






            '22 Setup - State Access

            If dt.Rows(0)("x0125").ToString = "1" Then
                chkStateList.Items.FindByValue("Access").Selected = True
            Else
                chkStateList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x0125Add").ToString = "1" Then
                chkStateList.Items.FindByValue("Add").Selected = True
            Else
                chkStateList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0125Edit").ToString = "1" Then
                chkStateList.Items.FindByValue("Edit").Selected = True
            Else
                chkStateList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0125Delete").ToString = "1" Then
                chkStateList.Items.FindByValue("Delete").Selected = True
            Else
                chkStateList.Items.FindByValue("Delete").Selected = False
            End If

            '23 Setup - Target

            If dt.Rows(0)("x0160").ToString = "1" Then
                chkTargetList.Items.FindByValue("Access").Selected = True
            Else
                chkTargetList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x0160Add").ToString = "1" Then
                chkTargetList.Items.FindByValue("Add").Selected = True
            Else
                chkTargetList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0160Edit").ToString = "1" Then
                chkTargetList.Items.FindByValue("Edit").Selected = True
            Else
                chkTargetList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0160Delete").ToString = "1" Then
                chkTargetList.Items.FindByValue("Delete").Selected = True
            Else
                chkTargetList.Items.FindByValue("Delete").Selected = False
            End If


            '24 Setup - TaxRate Access

            If dt.Rows(0)("x0128").ToString = "1" Then
                chkTaxRateList.Items.FindByValue("Access").Selected = True
            Else
                chkTaxRateList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x0128Add").ToString = "1" Then
                chkTaxRateList.Items.FindByValue("Add").Selected = True
            Else
                chkTaxRateList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0128Edit").ToString = "1" Then
                chkTaxRateList.Items.FindByValue("Edit").Selected = True
            Else
                chkTaxRateList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0128Delete").ToString = "1" Then
                chkTaxRateList.Items.FindByValue("Delete").Selected = True
            Else
                chkTaxRateList.Items.FindByValue("Delete").Selected = False
            End If


            '25 Setup - Team

            If dt.Rows(0)("x0161").ToString = "1" Then
                chkTeamList.Items.FindByValue("Access").Selected = True
            Else
                chkTeamList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x0161Add").ToString = "1" Then
                chkTeamList.Items.FindByValue("Add").Selected = True
            Else
                chkTeamList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0161Edit").ToString = "1" Then
                chkTeamList.Items.FindByValue("Edit").Selected = True
            Else
                chkTeamList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0161Delete").ToString = "1" Then
                chkTeamList.Items.FindByValue("Delete").Selected = True
            Else
                chkTeamList.Items.FindByValue("Delete").Selected = False
            End If

            '26 Setup - Termination Code

            If dt.Rows(0)("x0162").ToString = "1" Then
                chkTermCodeList.Items.FindByValue("Access").Selected = True
            Else
                chkTermCodeList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x0162Add").ToString = "1" Then
                chkTermCodeList.Items.FindByValue("Add").Selected = True
            Else
                chkTermCodeList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0162Edit").ToString = "1" Then
                chkTermCodeList.Items.FindByValue("Edit").Selected = True
            Else
                chkTermCodeList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0162Delete").ToString = "1" Then
                chkTermCodeList.Items.FindByValue("Delete").Selected = True
            Else
                chkTermCodeList.Items.FindByValue("Delete").Selected = False
            End If


            '27 Setup - Terms Access

            If dt.Rows(0)("x0102").ToString = "1" Then
                chkTermsList.Items.FindByValue("Access").Selected = True
            Else
                chkTermsList.Items.FindByValue("Access").Selected = False
            End If


            If dt.Rows(0)("x0102Add").ToString = "1" Then
                chkTermsList.Items.FindByValue("Add").Selected = True
            Else
                chkTermsList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0102Edit").ToString = "1" Then
                chkTermsList.Items.FindByValue("Edit").Selected = True
            Else
                chkTermsList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0102Delete").ToString = "1" Then
                chkTermsList.Items.FindByValue("Delete").Selected = True
            Else
                chkTermsList.Items.FindByValue("Delete").Selected = False
            End If


            'Setup - Target Access

            'If dt.Rows(0)("x3201").ToString = "1" Then
            '    chkTargetList.Items.FindByValue("Access").Selected = True
            'Else
            '    chkTargetList.Items.FindByValue("Access").Selected = False
            'End If
            'If dt.Rows(0)("x3201Add").ToString = "1" Then
            '    chkTargetList.Items.FindByValue("Add").Selected = True
            'Else
            '    chkTargetList.Items.FindByValue("Add").Selected = False
            'End If
            'If dt.Rows(0)("x3201Edit").ToString = "1" Then
            '    chkTargetList.Items.FindByValue("Edit").Selected = True
            'Else
            '    chkTargetList.Items.FindByValue("Edit").Selected = False
            'End If
            'If dt.Rows(0)("x3201Delete").ToString = "1" Then
            '    chkTargetList.Items.FindByValue("Delete").Selected = True
            'Else
            '    chkTargetList.Items.FindByValue("Delete").Selected = False
            'End If



            'Setup - Postal to Location Access

            'If dt.Rows(0)("x0117").ToString = "1" Then
            '    chkPostalList.Items.FindByValue("Access").Selected = True
            'Else
            '    chkPostalList.Items.FindByValue("Access").Selected = False
            'End If
            'If dt.Rows(0)("x0117Add").ToString = "1" Then
            '    chkPostalList.Items.FindByValue("Add").Selected = True
            'Else
            '    chkPostalList.Items.FindByValue("Add").Selected = False
            'End If
            'If dt.Rows(0)("x0117Edit").ToString = "1" Then
            '    chkPostalList.Items.FindByValue("Edit").Selected = True
            'Else
            '    chkPostalList.Items.FindByValue("Edit").Selected = False
            'End If
            'If dt.Rows(0)("x0117Delete").ToString = "1" Then
            '    chkPostalList.Items.FindByValue("Delete").Selected = True
            'Else
            '    chkPostalList.Items.FindByValue("Delete").Selected = False
            'End If




            'Setup - Department Access

            'If dt.Rows(0)("x0114").ToString = "1" Then
            '    chkDepartmentList.Items.FindByValue("Access").Selected = True
            'Else
            '    chkDepartmentList.Items.FindByValue("Access").Selected = False
            'End If
            'If dt.Rows(0)("x0114Add").ToString = "1" Then
            '    chkDepartmentList.Items.FindByValue("Add").Selected = True
            'Else
            '    chkDepartmentList.Items.FindByValue("Add").Selected = False
            'End If
            'If dt.Rows(0)("x0114Edit").ToString = "1" Then
            '    chkDepartmentList.Items.FindByValue("Edit").Selected = True
            'Else
            '    chkDepartmentList.Items.FindByValue("Edit").Selected = False
            'End If
            'If dt.Rows(0)("x0114Delete").ToString = "1" Then
            '    chkDepartmentList.Items.FindByValue("Delete").Selected = True
            'Else
            '    chkDepartmentList.Items.FindByValue("Delete").Selected = False
            'End If




            '28 Setup - UOM Access

            If dt.Rows(0)("X0129").ToString = "1" Then
                chkUOMList.Items.FindByValue("Access").Selected = True
            Else
                chkUOMList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("X0129Add").ToString = "1" Then
                chkUOMList.Items.FindByValue("Add").Selected = True
            Else
                chkUOMList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("X0129Edit").ToString = "1" Then
                chkUOMList.Items.FindByValue("Edit").Selected = True
            Else
                chkUOMList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("X0129Delete").ToString = "1" Then
                chkUOMList.Items.FindByValue("Delete").Selected = True
            Else
                chkUOMList.Items.FindByValue("Delete").Selected = False
            End If



            '29 Setup - User Access Access

            If dt.Rows(0)("x0704").ToString = "1" Then
                chkUserAccessList.Items.FindByValue("Access").Selected = True
            Else
                chkUserAccessList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x0704Add").ToString = "1" Then
                chkUserAccessList.Items.FindByValue("Add").Selected = True
            Else
                chkUserAccessList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0704Edit").ToString = "1" Then
                chkUserAccessList.Items.FindByValue("Edit").Selected = True
            Else
                chkUserAccessList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0704Delete").ToString = "1" Then
                chkUserAccessList.Items.FindByValue("Delete").Selected = True
            Else
                chkUserAccessList.Items.FindByValue("Delete").Selected = False
            End If

            '30 Setup - Vehicle Access

            If dt.Rows(0)("x2415").ToString = "1" Then
                chkVehicleList.Items.FindByValue("Access").Selected = True
            Else
                chkVehicleList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x2415Add").ToString = "1" Then
                chkVehicleList.Items.FindByValue("Add").Selected = True
            Else
                chkVehicleList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x2415Edit").ToString = "1" Then
                chkVehicleList.Items.FindByValue("Edit").Selected = True
            Else
                chkVehicleList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x2415Delete").ToString = "1" Then
                chkVehicleList.Items.FindByValue("Delete").Selected = True
            Else
                chkVehicleList.Items.FindByValue("Delete").Selected = False
            End If


            '31 Setup - Market Segment

            If dt.Rows(0)("x0163").ToString = "1" Then
                chkMarketSegmentList.Items.FindByValue("Access").Selected = True
            Else
                chkMarketSegmentList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x0163Add").ToString = "1" Then
                chkMarketSegmentList.Items.FindByValue("Add").Selected = True
            Else
                chkMarketSegmentList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0163Edit").ToString = "1" Then
                chkMarketSegmentList.Items.FindByValue("Edit").Selected = True
            Else
                chkMarketSegmentList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0163Delete").ToString = "1" Then
                chkMarketSegmentList.Items.FindByValue("Delete").Selected = True
            Else
                chkMarketSegmentList.Items.FindByValue("Delete").Selected = False
            End If


            '32 Setup - Service Type

            If dt.Rows(0)("x0164").ToString = "1" Then
                chkServiceTypeList.Items.FindByValue("Access").Selected = True
            Else
                chkServiceTypeList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x0164Add").ToString = "1" Then
                chkServiceTypeList.Items.FindByValue("Add").Selected = True
            Else
                chkServiceTypeList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0164Edit").ToString = "1" Then
                chkServiceTypeList.Items.FindByValue("Edit").Selected = True
            Else
                chkServiceTypeList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0164Delete").ToString = "1" Then
                chkServiceTypeList.Items.FindByValue("Delete").Selected = True
            Else
                chkServiceTypeList.Items.FindByValue("Delete").Selected = False
            End If

            '33 Setup - Service Module - NOT USED

            If dt.Rows(0)("x0165").ToString = "1" Then
                chkServiceModuleList.Items.FindByValue("Access").Selected = True
            Else
                chkServiceModuleList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x0165Add").ToString = "1" Then
                chkServiceModuleList.Items.FindByValue("Add").Selected = True
            Else
                chkServiceModuleList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0165Edit").ToString = "1" Then
                chkServiceModuleList.Items.FindByValue("Edit").Selected = True
            Else
                chkServiceModuleList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0165Delete").ToString = "1" Then
                chkServiceModuleList.Items.FindByValue("Delete").Selected = True
            Else
                chkServiceModuleList.Items.FindByValue("Delete").Selected = False
            End If

            '34 Setup - Contacts Module Setup

            If dt.Rows(0)("x0166").ToString = "1" Then
                chkContactsModuleSetup.Items.FindByValue("Access").Selected = True
            Else
                chkContactsModuleSetup.Items.FindByValue("Access").Selected = False
            End If

            If dt.Rows(0)("x0166Edit").ToString = "1" Then
                chkContactsModuleSetup.Items.FindByValue("Edit").Selected = True
            Else
                chkContactsModuleSetup.Items.FindByValue("Edit").Selected = False
            End If


            '35 Setup - Lock Service Record

            If dt.Rows(0)("x0167").ToString = "1" Then
                chkLockServiceRecord.Items.FindByValue("Access").Selected = True
            Else
                chkLockServiceRecord.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x0167Add").ToString = "1" Then
                chkLockServiceRecord.Items.FindByValue("Add").Selected = True
            Else
                chkLockServiceRecord.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0167Edit").ToString = "1" Then
                chkLockServiceRecord.Items.FindByValue("Edit").Selected = True
            Else
                chkLockServiceRecord.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0167Delete").ToString = "1" Then
                chkLockServiceRecord.Items.FindByValue("Delete").Selected = True
            Else
                chkLockServiceRecord.Items.FindByValue("Delete").Selected = False
            End If

            '36 Setup - Chemicals Setup

            If dt.Rows(0)("x0168").ToString = "1" Then
                chkChemicalsList.Items.FindByValue("Access").Selected = True
            Else
                chkChemicalsList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x0168Add").ToString = "1" Then
                chkChemicalsList.Items.FindByValue("Add").Selected = True
            Else
                chkChemicalsList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0168Edit").ToString = "1" Then
                chkChemicalsList.Items.FindByValue("Edit").Selected = True
            Else
                chkChemicalsList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0168Delete").ToString = "1" Then
                chkChemicalsList.Items.FindByValue("Delete").Selected = True
            Else
                chkChemicalsList.Items.FindByValue("Delete").Selected = False
            End If


            '37 Setup - Bank Setup

            If dt.Rows(0)("x0169").ToString = "1" Then
                chkBankList.Items.FindByValue("Access").Selected = True
            Else
                chkBankList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x0169Add").ToString = "1" Then
                chkBankList.Items.FindByValue("Add").Selected = True
            Else
                chkBankList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0169Edit").ToString = "1" Then
                chkBankList.Items.FindByValue("Edit").Selected = True
            Else
                chkBankList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0169Delete").ToString = "1" Then
                chkBankList.Items.FindByValue("Delete").Selected = True
            Else
                chkBankList.Items.FindByValue("Delete").Selected = False
            End If


            '38 Setup - Notes Template Setup

            If dt.Rows(0)("x0170").ToString = "1" Then
                chkNotesTemplateList.Items.FindByValue("Access").Selected = True
            Else
                chkNotesTemplateList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x0170Add").ToString = "1" Then
                chkNotesTemplateList.Items.FindByValue("Add").Selected = True
            Else
                chkNotesTemplateList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0170Edit").ToString = "1" Then
                chkNotesTemplateList.Items.FindByValue("Edit").Selected = True
            Else
                chkNotesTemplateList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0170Delete").ToString = "1" Then
                chkNotesTemplateList.Items.FindByValue("Delete").Selected = True
            Else
                chkNotesTemplateList.Items.FindByValue("Delete").Selected = False
            End If

            '39 Setup - Settlement Type Setup

            If dt.Rows(0)("x0171").ToString = "1" Then
                chkSettlementTypeList.Items.FindByValue("Access").Selected = True
            Else
                chkSettlementTypeList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x0171Add").ToString = "1" Then
                chkSettlementTypeList.Items.FindByValue("Add").Selected = True
            Else
                chkSettlementTypeList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0171Edit").ToString = "1" Then
                chkSettlementTypeList.Items.FindByValue("Edit").Selected = True
            Else
                chkSettlementTypeList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0171Delete").ToString = "1" Then
                chkSettlementTypeList.Items.FindByValue("Delete").Selected = True
            Else
                chkSettlementTypeList.Items.FindByValue("Delete").Selected = False
            End If

            '40 Setup - Service Action Setup

            If dt.Rows(0)("x0172").ToString = "1" Then
                chkServiceActionList.Items.FindByValue("Access").Selected = True
            Else
                chkServiceActionList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x0172Add").ToString = "1" Then
                chkServiceActionList.Items.FindByValue("Add").Selected = True
            Else
                chkServiceActionList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0172Edit").ToString = "1" Then
                chkServiceActionList.Items.FindByValue("Edit").Selected = True
            Else
                chkServiceActionList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0172Delete").ToString = "1" Then
                chkServiceActionList.Items.FindByValue("Delete").Selected = True
            Else
                chkServiceActionList.Items.FindByValue("Delete").Selected = False
            End If


            '41 Setup - Period

            If dt.Rows(0)("x0173").ToString = "1" Then
                chkPeriodList.Items.FindByValue("Access").Selected = True
            Else
                chkPeriodList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x0173Add").ToString = "1" Then
                chkPeriodList.Items.FindByValue("Add").Selected = True
            Else
                chkPeriodList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0173Edit").ToString = "1" Then
                chkPeriodList.Items.FindByValue("Edit").Selected = True
            Else
                chkPeriodList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0173Delete").ToString = "1" Then
                chkPeriodList.Items.FindByValue("Delete").Selected = True
            Else
                chkPeriodList.Items.FindByValue("Delete").Selected = False
            End If

            '42 Setup - Batch Email

            If dt.Rows(0)("x0174").ToString = "1" Then
                ChkBatchEmailList.Items.FindByValue("Access").Selected = True
            Else
                ChkBatchEmailList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x0174_SendEmail").ToString = "1" Then
                ChkBatchEmailList.Items.FindByValue("Add").Selected = True
            Else
                ChkBatchEmailList.Items.FindByValue("Add").Selected = False
            End If



            '43 Setup - Location

            If dt.Rows(0)("x0175").ToString = "1" Then
                chkLocationList.Items.FindByValue("Access").Selected = True
            Else
                chkLocationList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x0175Add").ToString = "1" Then
                chkLocationList.Items.FindByValue("Add").Selected = True
            Else
                chkLocationList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0175Edit").ToString = "1" Then
                chkLocationList.Items.FindByValue("Edit").Selected = True
            Else
                chkLocationList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0175Delete").ToString = "1" Then
                chkLocationList.Items.FindByValue("Delete").Selected = True
            Else
                chkLocationList.Items.FindByValue("Delete").Selected = False
            End If



            '44 Setup - Setup-Operation

            If dt.Rows(0)("x0176").ToString = "1" Then
                chkOpsModuleList.Items.FindByValue("Access").Selected = True
            Else
                chkOpsModuleList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x0176Add").ToString = "1" Then
                chkOpsModuleList.Items.FindByValue("Add").Selected = True
            Else
                chkOpsModuleList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0176Edit").ToString = "1" Then
                chkOpsModuleList.Items.FindByValue("Edit").Selected = True
            Else
                chkOpsModuleList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0176Delete").ToString = "1" Then
                chkOpsModuleList.Items.FindByValue("Delete").Selected = True
            Else
                chkOpsModuleList.Items.FindByValue("Delete").Selected = False
            End If


            '45 Setup - Setup-AR Module

            If dt.Rows(0)("x0177").ToString = "1" Then
                chkARModuleList.Items.FindByValue("Access").Selected = True
            Else
                chkARModuleList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x0177Add").ToString = "1" Then
                chkARModuleList.Items.FindByValue("Add").Selected = True
            Else
                chkARModuleList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0177Edit").ToString = "1" Then
                chkARModuleList.Items.FindByValue("Edit").Selected = True
            Else
                chkARModuleList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0177Delete").ToString = "1" Then
                chkARModuleList.Items.FindByValue("Delete").Selected = True
            Else
                chkARModuleList.Items.FindByValue("Delete").Selected = False
            End If


            '46 Setup - SMS Steup Module

            If dt.Rows(0)("x0178").ToString = "1" Then
                chkSMSSetupList.Items.FindByValue("Access").Selected = True
            Else
                chkSMSSetupList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x0178Add").ToString = "1" Then
                chkSMSSetupList.Items.FindByValue("Add").Selected = True
            Else
                chkSMSSetupList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0178Edit").ToString = "1" Then
                chkSMSSetupList.Items.FindByValue("Edit").Selected = True
            Else
                chkSMSSetupList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0178Delete").ToString = "1" Then
                chkSMSSetupList.Items.FindByValue("Delete").Selected = True
            Else
                chkSMSSetupList.Items.FindByValue("Delete").Selected = False
            End If


            '47 Setup - Login Log Steup Module

            If dt.Rows(0)("x0179").ToString = "1" Then
                chkLoginLog.Items.FindByValue("Access").Selected = True
            Else
                chkLoginLog.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x0179Add").ToString = "1" Then
                chkLoginLog.Items.FindByValue("Add").Selected = True
            Else
                chkLoginLog.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0179Edit").ToString = "1" Then
                chkLoginLog.Items.FindByValue("Edit").Selected = True
            Else
                chkLoginLog.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0179Delete").ToString = "1" Then
                chkLoginLog.Items.FindByValue("Delete").Selected = True
            Else
                chkLoginLog.Items.FindByValue("Delete").Selected = False
            End If


            '48 Setup - Document Type Module

            If dt.Rows(0)("x0180").ToString = "1" Then
                chkDocumentType.Items.FindByValue("Access").Selected = True
            Else
                chkDocumentType.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x0180Add").ToString = "1" Then
                chkDocumentType.Items.FindByValue("Add").Selected = True
            Else
                chkDocumentType.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0180Edit").ToString = "1" Then
                chkDocumentType.Items.FindByValue("Edit").Selected = True
            Else
                chkDocumentType.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0180Delete").ToString = "1" Then
                chkDocumentType.Items.FindByValue("Delete").Selected = True
            Else
                chkDocumentType.Items.FindByValue("Delete").Selected = False
            End If



            '49 Setup - Customer Portal

            If dt.Rows(0)("x0181").ToString = "1" Then
                chkCustmerPortal.Items.FindByValue("Access").Selected = True
            Else
                chkCustmerPortal.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x0181Add").ToString = "1" Then
                chkCustmerPortal.Items.FindByValue("Add").Selected = True
            Else
                chkCustmerPortal.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0181Edit").ToString = "1" Then
                chkCustmerPortal.Items.FindByValue("Edit").Selected = True
            Else
                chkCustmerPortal.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0181Delete").ToString = "1" Then
                chkCustmerPortal.Items.FindByValue("Delete").Selected = True
            Else
                chkCustmerPortal.Items.FindByValue("Delete").Selected = False
            End If


            '50 Setup - Stock Item

            If dt.Rows(0)("x0182").ToString = "1" Then
                chkStockItem.Items.FindByValue("Access").Selected = True
            Else
                chkStockItem.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x0182Add").ToString = "1" Then
                chkStockItem.Items.FindByValue("Add").Selected = True
            Else
                chkStockItem.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0182Edit").ToString = "1" Then
                chkStockItem.Items.FindByValue("Edit").Selected = True
            Else
                chkStockItem.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0182Delete").ToString = "1" Then
                chkStockItem.Items.FindByValue("Delete").Selected = True
            Else
                chkStockItem.Items.FindByValue("Delete").Selected = False
            End If



            '51 Device Type

            If dt.Rows(0)("x0183DeviceType").ToString = "1" Then
                chkDeviceType.Items.FindByValue("Access").Selected = True
            Else
                chkDeviceType.Items.FindByValue("Access").Selected = False
            End If

            'If dt.Rows(0)("x0182Add").ToString = "1" Then
            '    chkStockItem.Items.FindByValue("Add").Selected = True
            'Else
            '    chkStockItem.Items.FindByValue("Add").Selected = False
            'End If
            'If dt.Rows(0)("x0182Edit").ToString = "1" Then
            '    chkStockItem.Items.FindByValue("Edit").Selected = True
            'Else
            '    chkStockItem.Items.FindByValue("Edit").Selected = False
            'End If
            'If dt.Rows(0)("x0182Delete").ToString = "1" Then
            '    chkStockItem.Items.FindByValue("Delete").Selected = True
            'Else
            '    chkStockItem.Items.FindByValue("Delete").Selected = False
            'End If


            '52 Device Type

            If dt.Rows(0)("x0184deviceEventThreshold").ToString = "1" Then
                chkDeviceEventThreshold.Items.FindByValue("Access").Selected = True
            Else
                chkDeviceEventThreshold.Items.FindByValue("Access").Selected = False
            End If


            '53 Pest Master

            If dt.Rows(0)("x0185").ToString = "1" Then
                chkPestMaster.Items.FindByValue("Access").Selected = True
            Else
                chkPestMaster.Items.FindByValue("Access").Selected = False
            End If

            If dt.Rows(0)("x0185Add").ToString = "1" Then
                chkPestMaster.Items.FindByValue("Add").Selected = True
            Else
                chkPestMaster.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0185Edit").ToString = "1" Then
                chkPestMaster.Items.FindByValue("Edit").Selected = True
            Else
                chkPestMaster.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0185Delete").ToString = "1" Then
                chkPestMaster.Items.FindByValue("Delete").Selected = True
            Else
                chkPestMaster.Items.FindByValue("Delete").Selected = False
            End If

            '54 Level of Infestation

            If dt.Rows(0)("x0186").ToString = "1" Then
                chkLevelofInfestatation.Items.FindByValue("Access").Selected = True
            Else
                chkLevelofInfestatation.Items.FindByValue("Access").Selected = False
            End If

            If dt.Rows(0)("x0186Add").ToString = "1" Then
                chkLevelofInfestatation.Items.FindByValue("Add").Selected = True
            Else
                chkLevelofInfestatation.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0186Edit").ToString = "1" Then
                chkLevelofInfestatation.Items.FindByValue("Edit").Selected = True
            Else
                chkLevelofInfestatation.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0186Delete").ToString = "1" Then
                chkLevelofInfestatation.Items.FindByValue("Delete").Selected = True
            Else
                chkLevelofInfestatation.Items.FindByValue("Delete").Selected = False
            End If


            '55 Gender

            If dt.Rows(0)("x0187").ToString = "1" Then
                chkPestGender.Items.FindByValue("Access").Selected = True
            Else
                chkPestGender.Items.FindByValue("Access").Selected = False
            End If

            If dt.Rows(0)("x0187Add").ToString = "1" Then
                chkPestGender.Items.FindByValue("Add").Selected = True
            Else
                chkPestGender.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0187Edit").ToString = "1" Then
                chkPestGender.Items.FindByValue("Edit").Selected = True
            Else
                chkPestGender.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0187Delete").ToString = "1" Then
                chkPestGender.Items.FindByValue("Delete").Selected = True
            Else
                chkPestGender.Items.FindByValue("Delete").Selected = False
            End If

            '56 LifeStage

            If dt.Rows(0)("x0188").ToString = "1" Then
                chkPestLifeStage.Items.FindByValue("Access").Selected = True
            Else
                chkPestLifeStage.Items.FindByValue("Access").Selected = False
            End If


            If dt.Rows(0)("x0188Add").ToString = "1" Then
                chkPestLifeStage.Items.FindByValue("Add").Selected = True
            Else
                chkPestLifeStage.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0188Edit").ToString = "1" Then
                chkPestLifeStage.Items.FindByValue("Edit").Selected = True
            Else
                chkPestLifeStage.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0188Delete").ToString = "1" Then
                chkPestLifeStage.Items.FindByValue("Delete").Selected = True
            Else
                chkPestLifeStage.Items.FindByValue("Delete").Selected = False
            End If

            '57 Species

            If dt.Rows(0)("x0189").ToString = "1" Then
                chkPestSpecies.Items.FindByValue("Access").Selected = True
            Else
                chkPestSpecies.Items.FindByValue("Access").Selected = False
            End If

            If dt.Rows(0)("x0189Add").ToString = "1" Then
                chkPestSpecies.Items.FindByValue("Add").Selected = True
            Else
                chkPestSpecies.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0189Edit").ToString = "1" Then
                chkPestSpecies.Items.FindByValue("Edit").Selected = True
            Else
                chkPestSpecies.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0189Delete").ToString = "1" Then
                chkPestSpecies.Items.FindByValue("Delete").Selected = True
            Else
                chkPestSpecies.Items.FindByValue("Delete").Selected = False
            End If


            '58 Trap Type

            If dt.Rows(0)("x0190").ToString = "1" Then
                chkPestTrapType.Items.FindByValue("Access").Selected = True
            Else
                chkPestTrapType.Items.FindByValue("Access").Selected = False
            End If

            If dt.Rows(0)("x0190Add").ToString = "1" Then
                chkPestTrapType.Items.FindByValue("Add").Selected = True
            Else
                chkPestTrapType.Items.FindByValue("Add").Selected = False
            End If

            If dt.Rows(0)("x0190Edit").ToString = "1" Then
                chkPestTrapType.Items.FindByValue("Edit").Selected = True
            Else
                chkPestTrapType.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0190Delete").ToString = "1" Then
                chkPestTrapType.Items.FindByValue("Delete").Selected = True
            Else
                chkPestTrapType.Items.FindByValue("Delete").Selected = False
            End If


            '59 Hold Code

            If dt.Rows(0)("x0191").ToString = "1" Then
                chkHoldCodeList.Items.FindByValue("Access").Selected = True
            Else
                chkHoldCodeList.Items.FindByValue("Access").Selected = False
            End If

            If dt.Rows(0)("x0191Add").ToString = "1" Then
                chkHoldCodeList.Items.FindByValue("Add").Selected = True
            Else
                chkHoldCodeList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0191Edit").ToString = "1" Then
                chkHoldCodeList.Items.FindByValue("Edit").Selected = True
            Else
                chkHoldCodeList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0191Delete").ToString = "1" Then
                chkHoldCodeList.Items.FindByValue("Delete").Selected = True
            Else
                chkHoldCodeList.Items.FindByValue("Delete").Selected = False
            End If

            '60 Batch Contract Price Change

            If dt.Rows(0)("x0192").ToString = "1" Then
                chkBatchContractPriceChange.Items.FindByValue("Access").Selected = True
            Else
                chkBatchContractPriceChange.Items.FindByValue("Access").Selected = False
            End If

            If dt.Rows(0)("x0193SetupLogDetails").ToString = "1" Then
                chkSetupLogDetails.Items.FindByValue("Access").Selected = True
            Else
                chkSetupLogDetails.Items.FindByValue("Access").Selected = False
            End If


            '09.07.20


            '61 Setup - Company Setup

            If dt.Rows(0)("x0101").ToString = "1" Then
                chkCompanySetup.Items.FindByValue("Access").Selected = True
            Else
                chkCompanySetup.Items.FindByValue("Access").Selected = False
            End If
            'If dt.Rows(0)("x0101Add").ToString = "1" Then
            '    chkCompanySetup.Items.FindByValue("Add").Selected = True
            'Else
            '    chkCompanySetup.Items.FindByValue("Add").Selected = False
            'End If
            If dt.Rows(0)("x0101Edit").ToString = "1" Then
                chkCompanySetup.Items.FindByValue("Edit").Selected = True
            Else
                chkCompanySetup.Items.FindByValue("Edit").Selected = False
            End If
            'If dt.Rows(0)("x0101Delete").ToString = "1" Then
            '    chkCompanySetup.Items.FindByValue("Delete").Selected = True
            'Else
            '    chkCompanySetup.Items.FindByValue("Delete").Selected = False
            'End If
            '09.07.20

            'If dt.Rows(0)("x0191Add").ToString = "1" Then
            '    chkHoldCodeList.Items.FindByValue("Add").Selected = True
            'Else
            '    chkHoldCodeList.Items.FindByValue("Add").Selected = False
            'End If
            'If dt.Rows(0)("x0191Edit").ToString = "1" Then
            '    chkHoldCodeList.Items.FindByValue("Edit").Selected = True
            'Else
            '    chkHoldCodeList.Items.FindByValue("Edit").Selected = False
            'End If
            'If dt.Rows(0)("x0191Delete").ToString = "1" Then
            '    chkHoldCodeList.Items.FindByValue("Delete").Selected = True
            'Else
            '    chkHoldCodeList.Items.FindByValue("Delete").Selected = False
            'End If


            'If dt.Rows(0)("x0182Add").ToString = "1" Then
            '    chkStockItem.Items.FindByValue("Add").Selected = True
            'Else
            '    chkStockItem.Items.FindByValue("Add").Selected = False
            'End If
            'If dt.Rows(0)("x0182Edit").ToString = "1" Then
            '    chkStockItem.Items.FindByValue("Edit").Selected = True
            'Else
            '    chkStockItem.Items.FindByValue("Edit").Selected = False
            'End If
            'If dt.Rows(0)("x0182Delete").ToString = "1" Then
            '    chkStockItem.Items.FindByValue("Delete").Selected = True
            'Else
            '    chkStockItem.Items.FindByValue("Delete").Selected = False
            'End If



            '62 ContractCode

            If dt.Rows(0)("x0194").ToString = "1" Then
                chkContractCodeList.Items.FindByValue("Access").Selected = True
            Else
                chkContractCodeList.Items.FindByValue("Access").Selected = False
            End If

            If dt.Rows(0)("x0194Add").ToString = "1" Then
                chkContractCodeList.Items.FindByValue("Add").Selected = True
            Else
                chkContractCodeList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x0194Edit").ToString = "1" Then
                chkContractCodeList.Items.FindByValue("Edit").Selected = True
            Else
                chkContractCodeList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("x0194Delete").ToString = "1" Then
                chkContractCodeList.Items.FindByValue("Delete").Selected = True
            Else
                chkContractCodeList.Items.FindByValue("Delete").Selected = False
            End If
            '=====================================


            'Company Access

            If dt.Rows(0)("X0302").ToString = "1" Then
                chkCompanyList.Items.FindByValue("Access").Selected = True
            Else
                chkCompanyList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("X0302Add").ToString = "1" Then
                chkCompanyList.Items.FindByValue("Add").Selected = True
            Else
                chkCompanyList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("X0302Edit").ToString = "1" Then
                chkCompanyList.Items.FindByValue("Edit").Selected = True
            Else
                chkCompanyList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("X0302EditAcct").ToString = "1" Then
                chkCompanyList.Items.FindByValue("EditAccount").Selected = True
            Else
                chkCompanyList.Items.FindByValue("EditAccount").Selected = False
            End If
            If dt.Rows(0)("X0302Delete").ToString = "1" Then
                chkCompanyList.Items.FindByValue("Delete").Selected = True
            Else
                chkCompanyList.Items.FindByValue("Delete").Selected = False
            End If

            If dt.Rows(0)("X0302Change").ToString = "1" Then
                chkCompanyList.Items.FindByValue("ChangeStatus").Selected = True
            Else
                chkCompanyList.Items.FindByValue("ChangeStatus").Selected = False
            End If
            If dt.Rows(0)("X0302Trans").ToString = "1" Then
                chkCompanyList.Items.FindByValue("Trans").Selected = True
            Else
                chkCompanyList.Items.FindByValue("Trans").Selected = False
            End If
            If dt.Rows(0)("X0302Notes").ToString = "1" Then
                chkCompanyList.Items.FindByValue("Notes").Selected = True
            Else
                chkCompanyList.Items.FindByValue("Notes").Selected = False
            End If
            If dt.Rows(0)("X0302ViewAll").ToString = "1" Then
                chkCompanyList.Items.FindByValue("ViewAll").Selected = True
            Else
                chkCompanyList.Items.FindByValue("ViewAll").Selected = False
            End If
            If dt.Rows(0)("X0302EditBilling").ToString = "1" Then
                chkCompanyList.Items.FindByValue("EditBilling").Selected = True
            Else
                chkCompanyList.Items.FindByValue("EditBilling").Selected = False
            End If

            If dt.Rows(0)("x0302CompanySpecificLocation").ToString = "1" Then
                chkCompanyList.Items.FindByValue("SpecificLocation").Selected = True
            Else
                chkCompanyList.Items.FindByValue("SpecificLocation").Selected = False
            End If

            If dt.Rows(0)("X0302EditContractGroup").ToString = "1" Then
                chkCompanyList.Items.FindByValue("EditContractGroup").Selected = True
            Else
                chkCompanyList.Items.FindByValue("EditContractGroup").Selected = False
            End If

            If dt.Rows(0)("x0302ChangeAccount").ToString = "1" Then
                chkCompanyList.Items.FindByValue("ChangeAccount").Selected = True
            Else
                chkCompanyList.Items.FindByValue("ChangeAccount").Selected = False
            End If

            If dt.Rows(0)("x0302CompanyUpdateServiceContact").ToString = "1" Then
                chkCompanyList.Items.FindByValue("UpdateServiceContact").Selected = True
            Else
                chkCompanyList.Items.FindByValue("UpdateServiceContact").Selected = False
            End If

            'Person Access

            If dt.Rows(0)("X0303").ToString = "1" Then
                chkPersonList.Items.FindByValue("Access").Selected = True
            Else
                chkPersonList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("X0303Add").ToString = "1" Then
                chkPersonList.Items.FindByValue("Add").Selected = True
            Else
                chkPersonList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("X0303Edit").ToString = "1" Then
                chkPersonList.Items.FindByValue("Edit").Selected = True
            Else
                chkPersonList.Items.FindByValue("Edit").Selected = False
            End If

            If dt.Rows(0)("X0303Delete").ToString = "1" Then
                chkPersonList.Items.FindByValue("Delete").Selected = True
            Else
                chkPersonList.Items.FindByValue("Delete").Selected = False
            End If

            If dt.Rows(0)("X0303Trans").ToString = "1" Then
                chkPersonList.Items.FindByValue("Trans").Selected = True
            Else
                chkPersonList.Items.FindByValue("Trans").Selected = False
            End If
            If dt.Rows(0)("X0303Notes").ToString = "1" Then
                chkPersonList.Items.FindByValue("Notes").Selected = True
            Else
                chkPersonList.Items.FindByValue("Notes").Selected = False
            End If
            If dt.Rows(0)("X0303EditBilling").ToString = "1" Then
                chkPersonList.Items.FindByValue("EditBilling").Selected = True
            Else
                chkPersonList.Items.FindByValue("EditBilling").Selected = False
            End If

            If dt.Rows(0)("X0303PersonSpecificLocation").ToString = "1" Then
                chkPersonList.Items.FindByValue("SpecificLocation").Selected = True
            Else
                chkPersonList.Items.FindByValue("SpecificLocation").Selected = False
            End If

            If dt.Rows(0)("X0303EditContractGroup").ToString = "1" Then
                chkPersonList.Items.FindByValue("EditContractGroup").Selected = True
            Else
                chkPersonList.Items.FindByValue("EditContractGroup").Selected = False
            End If

            If dt.Rows(0)("x0303ChangeAccount").ToString = "1" Then
                chkPersonList.Items.FindByValue("ChangeAccount").Selected = True
            Else
                chkPersonList.Items.FindByValue("ChangeAccount").Selected = False
            End If

            If dt.Rows(0)("x0303PersonUpdateServiceContact").ToString = "1" Then
                chkPersonList.Items.FindByValue("UpdateServiceContact").Selected = True
            Else
                chkPersonList.Items.FindByValue("UpdateServiceContact").Selected = False
            End If

            'Contract Access

            If dt.Rows(0)("X2412").ToString = "1" Then
                chkContractList.Items.FindByValue("Access").Selected = True
            Else
                chkContractList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("X2412Add").ToString = "1" Then
                chkContractList.Items.FindByValue("Add").Selected = True
            Else
                chkContractList.Items.FindByValue("Add").Selected = False
            End If

            If dt.Rows(0)("X2412Copy").ToString = "1" Then
                chkContractList.Items.FindByValue("Copy").Selected = True
            Else
                chkContractList.Items.FindByValue("Copy").Selected = False
            End If

            If dt.Rows(0)("X2412Edit").ToString = "1" Then
                chkContractList.Items.FindByValue("Edit").Selected = True
            Else
                chkContractList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("X2412Delete").ToString = "1" Then
                chkContractList.Items.FindByValue("Delete").Selected = True
            Else
                chkContractList.Items.FindByValue("Delete").Selected = False
            End If
            If dt.Rows(0)("X2412Print").ToString = "1" Then
                chkContractList.Items.FindByValue("Print").Selected = True
            Else
                chkContractList.Items.FindByValue("Print").Selected = False
            End If
            If dt.Rows(0)("X2412ChSt").ToString = "1" Then
                chkContractList.Items.FindByValue("ChangeStatus").Selected = True
            Else
                chkContractList.Items.FindByValue("ChangeStatus").Selected = False
            End If
            If dt.Rows(0)("X2412Update").ToString = "1" Then
                chkContractList.Items.FindByValue("Update").Selected = True
            Else
                chkContractList.Items.FindByValue("Update").Selected = False
            End If
            If dt.Rows(0)("X2412Reverse").ToString = "1" Then
                chkContractList.Items.FindByValue("Reverse").Selected = True
            Else
                chkContractList.Items.FindByValue("Reverse").Selected = False
            End If
            If dt.Rows(0)("X2412Process").ToString = "1" Then
                chkContractList.Items.FindByValue("Process").Selected = True
            Else
                chkContractList.Items.FindByValue("Process").Selected = False
            End If
            If dt.Rows(0)("X2412Early").ToString = "1" Then
                chkContractList.Items.FindByValue("EarlyComplete").Selected = True
            Else
                chkContractList.Items.FindByValue("EarlyComplete").Selected = False
            End If
            If dt.Rows(0)("X2412Term").ToString = "1" Then
                chkContractList.Items.FindByValue("TerminationByCust").Selected = True
            Else
                chkContractList.Items.FindByValue("TerminationByCust").Selected = False
            End If
            If dt.Rows(0)("X2412Cancel").ToString = "1" Then
                chkContractList.Items.FindByValue("CancelByCust").Selected = True
            Else
                chkContractList.Items.FindByValue("CancelByCust").Selected = False
            End If
            If dt.Rows(0)("X2412Renewal").ToString = "1" Then
                chkContractList.Items.FindByValue("RenewalStatus").Selected = True
            Else
                chkContractList.Items.FindByValue("RenewalStatus").Selected = False
            End If
            If dt.Rows(0)("X2412EditPortfolioValue").ToString = "1" Then
                chkContractList.Items.FindByValue("EditPortfolioValue").Selected = True
            Else
                chkContractList.Items.FindByValue("EditPortfolioValue").Selected = False
            End If

            If dt.Rows(0)("X2412EditOurRef").ToString = "1" Then
                chkContractList.Items.FindByValue("EditOurRef").Selected = True
            Else
                chkContractList.Items.FindByValue("EditOurRef").Selected = False
            End If

            If dt.Rows(0)("X2412EditManualContractNo").ToString = "1" Then
                chkContractList.Items.FindByValue("EditManualContractNo").Selected = True
            Else
                chkContractList.Items.FindByValue("EditManualContractNo").Selected = False
            End If

            If dt.Rows(0)("X2412EditPONo").ToString = "1" Then
                chkContractList.Items.FindByValue("EditPONo").Selected = True
            Else
                chkContractList.Items.FindByValue("EditPONo").Selected = False
            End If

            If dt.Rows(0)("X2412EditNotes").ToString = "1" Then
                chkContractList.Items.FindByValue("EditNotes").Selected = True
            Else
                chkContractList.Items.FindByValue("EditNotes").Selected = False
            End If

            If dt.Rows(0)("x2412SORAccess").ToString = "1" Then
                chkContractList.Items.FindByValue("SOR").Selected = True
            Else
                chkContractList.Items.FindByValue("SOR").Selected = False
            End If

            If dt.Rows(0)("x2412SORAdd").ToString = "1" Then
                chkContractList.Items.FindByValue("SORAdd").Selected = True
            Else
                chkContractList.Items.FindByValue("SORAdd").Selected = False
            End If

            If dt.Rows(0)("x2412SOREdit").ToString = "1" Then
                chkContractList.Items.FindByValue("SOREdit").Selected = True
            Else
                chkContractList.Items.FindByValue("SOREdit").Selected = False
            End If

            If dt.Rows(0)("x2412SORDelete").ToString = "1" Then
                chkContractList.Items.FindByValue("SORDelete").Selected = True
            Else
                chkContractList.Items.FindByValue("SORDelete").Selected = False
            End If



            '''''
            If dt.Rows(0)("x2412BackDateContract").ToString = "1" Then
                chkContractList.Items.FindByValue("AddBackdateContractIndefinite").Selected = True
            Else
                chkContractList.Items.FindByValue("AddBackdateContractIndefinite").Selected = False
            End If

            If dt.Rows(0)("x2412BackDateContractTermination").ToString = "1" Then
                chkContractList.Items.FindByValue("TerminationBackdate").Selected = True
            Else
                chkContractList.Items.FindByValue("TerminationBackdate").Selected = False
            End If


            If dt.Rows(0)("x2412BackDateContractSameMonthOnly").ToString = "1" Then
                chkContractList.Items.FindByValue("AddBackdateContractSameMonthOnly").Selected = True
            Else
                chkContractList.Items.FindByValue("AddBackdateContractSameMonthOnly").Selected = False
            End If

            If dt.Rows(0)("x2412BackDateContractTerminationSameMonthOnly").ToString = "1" Then
                chkContractList.Items.FindByValue("TerminationSameMonthOnly").Selected = True
            Else
                chkContractList.Items.FindByValue("TerminationSameMonthOnly").Selected = False
            End If


            If dt.Rows(0)("x2412FutureDateContractTermination").ToString = "1" Then
                chkContractList.Items.FindByValue("TerminationFutureDate").Selected = True
            Else
                chkContractList.Items.FindByValue("TerminationFutureDate").Selected = False
            End If

            If dt.Rows(0)("x2412FileAccess").ToString = "1" Then
                chkContractList.Items.FindByValue("FileAccess").Selected = True
            Else
                chkContractList.Items.FindByValue("FileAccess").Selected = False
            End If

            If dt.Rows(0)("x2412FileUpload").ToString = "1" Then
                chkContractList.Items.FindByValue("FileUpload").Selected = True
            Else
                chkContractList.Items.FindByValue("FileUpload").Selected = False
            End If

            If dt.Rows(0)("x2412FileDelete").ToString = "1" Then
                chkContractList.Items.FindByValue("FileDelete").Selected = True
            Else
                chkContractList.Items.FindByValue("FileDelete").Selected = False
            End If

            If dt.Rows(0)("x2412EditAutoRenewal").ToString = "1" Then
                chkContractList.Items.FindByValue("EditAutoRenewal").Selected = True
            Else
                chkContractList.Items.FindByValue("EditAutoRenewal").Selected = False
            End If

            If dt.Rows(0)("x2412BillingFrequency").ToString = "1" Then
                chkContractList.Items.FindByValue("EditBillingFrequency").Selected = True
            Else
                chkContractList.Items.FindByValue("EditBillingFrequency").Selected = False
            End If

            If dt.Rows(0)("x2412RegenerateSchedule").ToString = "1" Then
                chkContractList.Items.FindByValue("RegenerateSchedule").Selected = True
            Else
                chkContractList.Items.FindByValue("RegenerateSchedule").Selected = False
            End If

            If dt.Rows(0)("x2412ExtendContractEndDate").ToString = "1" Then
                chkContractList.Items.FindByValue("ExtendEndDate").Selected = True
            Else
                chkContractList.Items.FindByValue("ExtendEndDate").Selected = False
            End If

            If dt.Rows(0)("x2412Distribution").ToString = "1" Then
                chkContractList.Items.FindByValue("Distribution").Selected = True
            Else
                chkContractList.Items.FindByValue("Distribution").Selected = False
            End If

            If dt.Rows(0)("x2412VoidContract").ToString = "1" Then
                chkContractList.Items.FindByValue("VoidContract").Selected = True
            Else
                chkContractList.Items.FindByValue("VoidContract").Selected = False
            End If

            If dt.Rows(0)("x2412WarrantyDates").ToString = "1" Then
                chkContractList.Items.FindByValue("WarrantyDates").Selected = True
            Else
                chkContractList.Items.FindByValue("WarrantyDates").Selected = False
            End If

            If dt.Rows(0)("x2412Revision").ToString = "1" Then
                chkContractList.Items.FindByValue("Revision").Selected = True
            Else
                chkContractList.Items.FindByValue("Revision").Selected = False
            End If

            If dt.Rows(0)("x2412AgreementTypeContractCode").ToString = "1" Then
                chkContractList.Items.FindByValue("AgreementTypeContractCode").Selected = True
            Else
                chkContractList.Items.FindByValue("AgreementTypeContractCode").Selected = False
            End If


         


            'If chkContractList.Items.FindByValue("WarrantyDates").Selected = True Then
            '    Command.Parameters.AddWithValue("@x2412WarrantyDates", 1)
            'Else
            '    Command.Parameters.AddWithValue("@x2412WarrantyDates", 0)
            'End If

            ''''
            'Asset Access

            If dt.Rows(0)("x1301AssetView").ToString = "1" Then
                chkAssetList.Items.FindByValue("Access").Selected = True
            Else
                chkAssetList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x1301AssetAdd").ToString = "1" Then
                chkAssetList.Items.FindByValue("Add").Selected = True
            Else
                chkAssetList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x1301AssetEdit").ToString = "1" Then
                chkAssetList.Items.FindByValue("Edit").Selected = True
            Else
                chkAssetList.Items.FindByValue("Edit").Selected = False
            End If

            If dt.Rows(0)("x1301AssetDelete").ToString = "1" Then
                chkAssetList.Items.FindByValue("Delete").Selected = True
            Else
                chkAssetList.Items.FindByValue("Delete").Selected = False
            End If

            If dt.Rows(0)("x1301AssetPrint").ToString = "1" Then
                chkAssetList.Items.FindByValue("Print").Selected = True
            Else
                chkAssetList.Items.FindByValue("Print").Selected = False
            End If
            If dt.Rows(0)("x1301AssetMovement").ToString = "1" Then
                chkAssetList.Items.FindByValue("Movement").Selected = True
            Else
                chkAssetList.Items.FindByValue("Movement").Selected = False
            End If

            'Asset Supplier Access

            If dt.Rows(0)("x1302AssetSupplierView").ToString = "1" Then
                chkAssetSupplierList.Items.FindByValue("Access").Selected = True
            Else
                chkAssetSupplierList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x1302AssetSupplierAdd").ToString = "1" Then
                chkAssetSupplierList.Items.FindByValue("Add").Selected = True
            Else
                chkAssetSupplierList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x1302AssetSupplierEdit").ToString = "1" Then
                chkAssetSupplierList.Items.FindByValue("Edit").Selected = True
            Else
                chkAssetSupplierList.Items.FindByValue("Edit").Selected = False
            End If

            If dt.Rows(0)("x1302AssetSupplierDelete").ToString = "1" Then
                chkAssetSupplierList.Items.FindByValue("Delete").Selected = True
            Else
                chkAssetSupplierList.Items.FindByValue("Delete").Selected = False
            End If

            'Asset Brand Access

            If dt.Rows(0)("x1302AssetBrandView").ToString = "1" Then
                chkAssetBrandList.Items.FindByValue("Access").Selected = True
            Else
                chkAssetBrandList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x1302AssetBrandAdd").ToString = "1" Then
                chkAssetBrandList.Items.FindByValue("Add").Selected = True
            Else
                chkAssetBrandList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x1302AssetBrandEdit").ToString = "1" Then
                chkAssetBrandList.Items.FindByValue("Edit").Selected = True
            Else
                chkAssetBrandList.Items.FindByValue("Edit").Selected = False
            End If

            If dt.Rows(0)("x1302AssetBrandDelete").ToString = "1" Then
                chkAssetBrandList.Items.FindByValue("Delete").Selected = True
            Else
                chkAssetBrandList.Items.FindByValue("Delete").Selected = False
            End If

            'Asset Class Access

            If dt.Rows(0)("x1302AssetClassView").ToString = "1" Then
                chkAssetClassList.Items.FindByValue("Access").Selected = True
            Else
                chkAssetClassList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x1302AssetClassAdd").ToString = "1" Then
                chkAssetClassList.Items.FindByValue("Add").Selected = True
            Else
                chkAssetClassList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x1302AssetClassEdit").ToString = "1" Then
                chkAssetClassList.Items.FindByValue("Edit").Selected = True
            Else
                chkAssetClassList.Items.FindByValue("Edit").Selected = False
            End If

            If dt.Rows(0)("x1302AssetClassDelete").ToString = "1" Then
                chkAssetClassList.Items.FindByValue("Delete").Selected = True
            Else
                chkAssetClassList.Items.FindByValue("Delete").Selected = False
            End If

            'Asset Color Access

            If dt.Rows(0)("x1302AssetColorView").ToString = "1" Then
                chkAssetColorList.Items.FindByValue("Access").Selected = True
            Else
                chkAssetColorList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x1302AssetColorAdd").ToString = "1" Then
                chkAssetColorList.Items.FindByValue("Add").Selected = True
            Else
                chkAssetColorList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x1302AssetColorEdit").ToString = "1" Then
                chkAssetColorList.Items.FindByValue("Edit").Selected = True
            Else
                chkAssetColorList.Items.FindByValue("Edit").Selected = False
            End If

            If dt.Rows(0)("x1302AssetColorDelete").ToString = "1" Then
                chkAssetColorList.Items.FindByValue("Delete").Selected = True
            Else
                chkAssetColorList.Items.FindByValue("Delete").Selected = False
            End If

            'Asset Group Access

            If dt.Rows(0)("x1302AssetGroupView").ToString = "1" Then
                chkAssetGroupList.Items.FindByValue("Access").Selected = True
            Else
                chkAssetGroupList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x1302AssetGroupAdd").ToString = "1" Then
                chkAssetGroupList.Items.FindByValue("Add").Selected = True
            Else
                chkAssetGroupList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x1302AssetGroupEdit").ToString = "1" Then
                chkAssetGroupList.Items.FindByValue("Edit").Selected = True
            Else
                chkAssetGroupList.Items.FindByValue("Edit").Selected = False
            End If

            If dt.Rows(0)("x1302AssetGroupDelete").ToString = "1" Then
                chkAssetGroupList.Items.FindByValue("Delete").Selected = True
            Else
                chkAssetGroupList.Items.FindByValue("Delete").Selected = False
            End If

            'Asset Model Access

            If dt.Rows(0)("x1302AssetModelView").ToString = "1" Then
                chkAssetModelList.Items.FindByValue("Access").Selected = True
            Else
                chkAssetModelList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x1302AssetModelAdd").ToString = "1" Then
                chkAssetModelList.Items.FindByValue("Add").Selected = True
            Else
                chkAssetModelList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x1302AssetModelEdit").ToString = "1" Then
                chkAssetModelList.Items.FindByValue("Edit").Selected = True
            Else
                chkAssetModelList.Items.FindByValue("Edit").Selected = False
            End If

            If dt.Rows(0)("x1302AssetModelDelete").ToString = "1" Then
                chkAssetModelList.Items.FindByValue("Delete").Selected = True
            Else
                chkAssetModelList.Items.FindByValue("Delete").Selected = False
            End If

            'Asset Status Access

            If dt.Rows(0)("x1302AssetStatusView").ToString = "1" Then
                chkAssetStatusList.Items.FindByValue("Access").Selected = True
            Else
                chkAssetStatusList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x1302AssetStatusAdd").ToString = "1" Then
                chkAssetStatusList.Items.FindByValue("Add").Selected = True
            Else
                chkAssetStatusList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x1302AssetStatusEdit").ToString = "1" Then
                chkAssetStatusList.Items.FindByValue("Edit").Selected = True
            Else
                chkAssetStatusList.Items.FindByValue("Edit").Selected = False
            End If

            If dt.Rows(0)("x1302AssetStatusDelete").ToString = "1" Then
                chkAssetStatusList.Items.FindByValue("Delete").Selected = True
            Else
                chkAssetStatusList.Items.FindByValue("Delete").Selected = False
            End If

            'Asset MovementType Access

            If dt.Rows(0)("x1302AssetMovementTypeView").ToString = "1" Then
                chkAssetMovementTypeList.Items.FindByValue("Access").Selected = True
            Else
                chkAssetMovementTypeList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("x1302AssetMovementTypeAdd").ToString = "1" Then
                chkAssetMovementTypeList.Items.FindByValue("Add").Selected = True
            Else
                chkAssetMovementTypeList.Items.FindByValue("Add").Selected = False
            End If
            If dt.Rows(0)("x1302AssetMovementTypeEdit").ToString = "1" Then
                chkAssetMovementTypeList.Items.FindByValue("Edit").Selected = True
            Else
                chkAssetMovementTypeList.Items.FindByValue("Edit").Selected = False
            End If

            If dt.Rows(0)("x1302AssetMovementTypeDelete").ToString = "1" Then
                chkAssetMovementTypeList.Items.FindByValue("Delete").Selected = True
            Else
                chkAssetMovementTypeList.Items.FindByValue("Delete").Selected = False
            End If


            'Service Access

            If dt.Rows(0)("X2413").ToString = "1" Then
                chkServiceList.Items.FindByValue("Access").Selected = True
            Else
                chkServiceList.Items.FindByValue("Access").Selected = False
            End If
            If dt.Rows(0)("X2413Add").ToString = "1" Then
                chkServiceList.Items.FindByValue("Add").Selected = True
            Else
                chkServiceList.Items.FindByValue("Add").Selected = False
            End If

            If dt.Rows(0)("X2413Copy").ToString = "1" Then
                chkServiceList.Items.FindByValue("Copy").Selected = True
            Else
                chkServiceList.Items.FindByValue("Copy").Selected = False
            End If


            If dt.Rows(0)("X2413Edit").ToString = "1" Then
                chkServiceList.Items.FindByValue("Edit").Selected = True
            Else
                chkServiceList.Items.FindByValue("Edit").Selected = False
            End If
            If dt.Rows(0)("X2413Delete").ToString = "1" Then
                chkServiceList.Items.FindByValue("Delete").Selected = True
            Else
                chkServiceList.Items.FindByValue("Delete").Selected = False
            End If
            If dt.Rows(0)("X2413Print").ToString = "1" Then
                chkServiceList.Items.FindByValue("Print").Selected = True
            Else
                chkServiceList.Items.FindByValue("Print").Selected = False
            End If
            If dt.Rows(0)("X2413ChSt").ToString = "1" Then
                chkServiceList.Items.FindByValue("ChangeStatus").Selected = True
            Else
                chkServiceList.Items.FindByValue("ChangeStatus").Selected = False
            End If
            If dt.Rows(0)("X2413Update").ToString = "1" Then
                chkServiceList.Items.FindByValue("Update").Selected = True
            Else
                chkServiceList.Items.FindByValue("Update").Selected = False
            End If
            If dt.Rows(0)("X2413Reverse").ToString = "1" Then
                chkServiceList.Items.FindByValue("Reverse").Selected = True
            Else
                chkServiceList.Items.FindByValue("Reverse").Selected = False
            End If
            If dt.Rows(0)("X2413ViewAll").ToString = "1" Then
                chkServiceList.Items.FindByValue("ViewAll").Selected = True
            Else
                chkServiceList.Items.FindByValue("ViewAll").Selected = False
            End If

            If dt.Rows(0)("x2413Recalculate").ToString = "1" Then
                chkServiceList.Items.FindByValue("ReCalculate").Selected = True
            Else
                chkServiceList.Items.FindByValue("ReCalculate").Selected = False
            End If

            If dt.Rows(0)("x2413ExportToExcel").ToString = "1" Then
                chkServiceList.Items.FindByValue("ExportToExcel").Selected = True
            Else
                chkServiceList.Items.FindByValue("ExportToExcel").Selected = False
            End If


            If dt.Rows(0)("x2413FileAccess").ToString = "1" Then
                chkServiceList.Items.FindByValue("FileAccess").Selected = True
            Else
                chkServiceList.Items.FindByValue("FileAccess").Selected = False
            End If

            If dt.Rows(0)("x2413FileUpload").ToString = "1" Then
                chkServiceList.Items.FindByValue("FileUpload").Selected = True
            Else
                chkServiceList.Items.FindByValue("FileUpload").Selected = False
            End If

            If dt.Rows(0)("x2413FileDelete").ToString = "1" Then
                chkServiceList.Items.FindByValue("FileDelete").Selected = True
            Else
                chkServiceList.Items.FindByValue("FileDelete").Selected = False
            End If

            If dt.Rows(0)("x2413PestCount").ToString = "1" Then
                chkServiceList.Items.FindByValue("PestCount").Selected = True
            Else
                chkServiceList.Items.FindByValue("PestCount").Selected = False
            End If

            If dt.Rows(0)("x2413VoidService").ToString = "1" Then
                chkServiceList.Items.FindByValue("VoidService").Selected = True
            Else
                chkServiceList.Items.FindByValue("VoidService").Selected = False
            End If

            If dt.Rows(0)("x2413EditBilledAmtBillNo").ToString = "1" Then
                chkServiceList.Items.FindByValue("EditBilledAmtBillNo").Selected = True
            Else
                chkServiceList.Items.FindByValue("EditBilledAmtBillNo").Selected = False
            End If

            'If dt.Rows(0)("x2413EditContractGroup").ToString = "1" Then
            '    chkServiceList.Items.FindByValue("EditContractGroup").Selected = True
            'Else
            '    chkServiceList.Items.FindByValue("EditContractGroup").Selected = False
            'End If
            'End If

            'Dim command2 As MySqlCommand = New MySqlCommand

            'command2.CommandType = CommandType.Text

            'command2.CommandText = "SELECT * FROM tblGroupAccess where GroupAccess='" & txtGroupAuthority.Text & "'"
            'command2.Connection = conn

            'Dim dr2 As MySqlDataReader = command2.ExecuteReader()
            'Dim dt2 As New DataTable
            'dt2.Load(dr2)

            'If dt2.Rows.Count > 0 Then
            'txtRcNo2.Text = dt2.Rows(0)("Rcno").ToString

            'Service Access

            'If dt2.Rows(0)("X2413ApprMode").ToString = "1" Then
            '    chkServiceList.Items.FindByValue("ApproveMode").Selected = True
            'Else
            '    chkServiceList.Items.FindByValue("ApproveMode").Selected = False
            'End If
            If dt.Rows(0)("X2413AssignServiceRecord").ToString = "1" Then
                chkServiceList.Items.FindByValue("AssignServiceRecord").Selected = True
            Else
                chkServiceList.Items.FindByValue("AssignServiceRecord").Selected = False
            End If

            If dt.Rows(0)("x2412EditAgreeValue").ToString = "1" Then
                chkContractList.Items.FindByValue("EditAgreeValue").Selected = True
            Else
                chkContractList.Items.FindByValue("EditAgreeValue").Selected = False
            End If

            If dt.Rows(0)("x2412EditOurRef").ToString = "1" Then
                chkContractList.Items.FindByValue("EditOurRef").Selected = True
            Else
                chkContractList.Items.FindByValue("EditOurRef").Selected = False
            End If

            If dt.Rows(0)("x2412EditManualContractNo").ToString = "1" Then
                chkContractList.Items.FindByValue("EditManualContractNo").Selected = True
            Else
                chkContractList.Items.FindByValue("EditManualContractNo").Selected = False
            End If

            If dt.Rows(0)("x2412EditPoNo").ToString = "1" Then
                chkContractList.Items.FindByValue("EditPONo").Selected = True
            Else
                chkContractList.Items.FindByValue("EditPONo").Selected = False
            End If

            If dt.Rows(0)("x2413ManualContractPOWONo").ToString = "1" Then
                chkServiceList.Items.FindByValue("ManualContratPOWONo").Selected = True
            Else
                chkServiceList.Items.FindByValue("ManualContratPOWONo").Selected = False
            End If



            'UserStaff Access

            'If dt.Rows(0)("X0304HRSecurityLevel").ToString = "1" Then
            '    chkUserStaffList.Items.FindByValue("HRSecurity").Selected = True
            'Else
            '    chkUserStaffList.Items.FindByValue("HRSecurity").Selected = False
            'End If
            'If dt.Rows(0)("X0304HRViewSameLevel").ToString = "1" Then
            '    chkUserStaffList.Items.FindByValue("HRViewSame").Selected = True
            'Else
            '    chkUserStaffList.Items.FindByValue("HRViewSame").Selected = False
            'End If
            If dt.Rows(0)("X0304GroupAuthority").ToString = "1" Then
                chkUserStaffList.Items.FindByValue("GroupAccess").Selected = True
            Else
                chkUserStaffList.Items.FindByValue("GroupAccess").Selected = False
            End If
            If dt.Rows(0)("X0304ChSt").ToString = "1" Then
                chkUserStaffList.Items.FindByValue("ChangeStatus").Selected = True
            Else
                chkUserStaffList.Items.FindByValue("ChangeStatus").Selected = False
            End If
            If dt.Rows(0)("X0304Print").ToString = "1" Then
                chkUserStaffList.Items.FindByValue("Print").Selected = True
            Else
                chkUserStaffList.Items.FindByValue("Print").Selected = False
            End If

            'Reports Access

            If dt.Rows(0)("x2427ServiceRecord").ToString = "1" Then
                chkReportsList.Items.FindByValue("ServiceRecord").Selected = True
            Else
                chkReportsList.Items.FindByValue("ServiceRecord").Selected = False
            End If
            If dt.Rows(0)("x2427ServiceContract").ToString = "1" Then
                chkReportsList.Items.FindByValue("ServiceContract").Selected = True
            Else
                chkReportsList.Items.FindByValue("ServiceContract").Selected = False
            End If
            If dt.Rows(0)("x2427Management").ToString = "1" Then
                chkReportsList.Items.FindByValue("Management").Selected = True
            Else
                chkReportsList.Items.FindByValue("Management").Selected = False
            End If
            If dt.Rows(0)("x2427Revenue").ToString = "1" Then
                chkReportsList.Items.FindByValue("Revenue").Selected = True
            Else
                chkReportsList.Items.FindByValue("Revenue").Selected = False
            End If
            If dt.Rows(0)("x2427Portfolio").ToString = "1" Then
                chkReportsList.Items.FindByValue("Portfolio").Selected = True
            Else
                chkReportsList.Items.FindByValue("Portfolio").Selected = False
            End If
            If dt.Rows(0)("x2427JobOrder").ToString = "1" Then
                chkReportsList.Items.FindByValue("JobOrder").Selected = True
            Else
                chkReportsList.Items.FindByValue("JobOrder").Selected = False
            End If
            If dt.Rows(0)("x2427Others").ToString = "1" Then
                chkReportsList.Items.FindByValue("ARReports").Selected = True
            Else
                chkReportsList.Items.FindByValue("ARReports").Selected = False
            End If

            If dt.Rows(0)("x0401ToolsAddressVerification").ToString = "1" Then
                chkToolsList.Items.FindByValue("Access").Selected = True
            Else
                chkToolsList.Items.FindByValue("Access").Selected = False
            End If

            If dt.Rows(0)("x0401ToolsFloorPlan").ToString = "1" Then
                chkFloorPlanList.Items.FindByValue("Access").Selected = True
            Else
                chkFloorPlanList.Items.FindByValue("Access").Selected = False
            End If

        End If


        conn.Close()

    End Sub


    Protected Sub btnADD_Click(sender As Object, e As EventArgs) Handles btnADD.Click
        MakeMeNull()

        DisableControls()

        txtMode.Text = "New"
        lblMessage.Text = "ACTION: ADD RECORD"
        txtGroupAuthority.Focus()

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        txtCreatedOn.Attributes.Add("readonly", "readonly")
        chkContactSelectAll.Attributes.Add("onchange", "javascript: CheckBoxListSelectContact ('" & chkContactSelectAll.ClientID & "');")
        chkSetupSelectAll.Attributes.Add("onchange", "javascript: CheckBoxListSelectSetup ('" & chkSetupSelectAll.ClientID & "');")
        chkARSelectAll.Attributes.Add("onchange", "javascript: CheckBoxListSelectAR ('" & chkARSelectAll.ClientID & "');")
        chkSvcSelectAll.Attributes.Add("onchange", "javascript: CheckBoxListSelectSvc ('" & chkSvcSelectAll.ClientID & "');")
        chkReportSelectAll.Attributes.Add("onchange", "javascript: CheckBoxListSelectReport ('" & chkReportSelectAll.ClientID & "');")
        chkAssetSelectAll.Attributes.Add("onchange", "javascript: CheckBoxListSelectAsset ('" & chkAssetSelectAll.ClientID & "');")

        If Not IsPostBack Then
            MakeMeNull()
            EnableControls()
            EnableSvcAccessControls()
            EnableContactAccessControls()
            EnableSetupAccessControls()
            EnableARAccessControls()
            EnableReportAccessControls()
            EnableLocationAccessControls()
            EnableAssetAccessControls()
            EnableAssetGroupAccessControls()

            Dim UserID As String = Convert.ToString(Session("UserID"))
            txtCreatedBy.Text = UserID
            txtLastModifiedBy.Text = UserID


            Dim query As String = ""
            query = "Select Concat(LocationID,' : ',Location) as LocationConcat from tblLocation order by Location "
            PopulateDropDownList(query, "LocationConcat", "LocationConcat", ddlLocationID)

            'Dim query As String = ""
            'query = "Select strLocation from tblLocation order by strLocation "
            'PopulateDropDownList(query, "strLocation", "strLocation", ddlLocationID)



            ''''''''''''''''''''''''''''''''''''
            Dim conn As MySqlConnection = New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim commandServiceRecordMasterSetup As MySqlCommand = New MySqlCommand
            commandServiceRecordMasterSetup.CommandType = CommandType.Text
            'commandServiceRecordMasterSetup.CommandText = "SELECT showSConScreenLoad, ServiceContractMaxRec,DisplayRecordsLocationWise, BackDateContract, BackDateContractTermination, ContractRevisionTerminationCode, PrefixDocNoContract, AutoRenewal FROM tblservicerecordmastersetup"
            commandServiceRecordMasterSetup.CommandText = "SELECT DisplayRecordsLocationWise FROM tblservicerecordmastersetup"

            commandServiceRecordMasterSetup.Connection = conn

            Dim drServiceRecordMasterSetup As MySqlDataReader = commandServiceRecordMasterSetup.ExecuteReader()
            Dim dtServiceRecordMasterSetup As New DataTable
            dtServiceRecordMasterSetup.Load(drServiceRecordMasterSetup)

            'txtLimit.Text = dtServiceRecordMasterSetup.Rows(0)("ServiceContractMaxRec")
            txtDisplayRecordsLocationwise.Text = dtServiceRecordMasterSetup.Rows(0)("DisplayRecordsLocationWise").ToString


            'txtBackDateContractSetup.Text = dtServiceRecordMasterSetup.Rows(0)("BackDateContract")
            'txtBackDateContractTerminationSetup.Text = dtServiceRecordMasterSetup.Rows(0)("BackDateContractTermination")

            'txtContractRevisionTerminationCode.Text = dtServiceRecordMasterSetup.Rows(0)("ContractRevisionTerminationCode")

            'txtPrefixDocNoContract.Text = dtServiceRecordMasterSetup.Rows(0)("PrefixDocNoContract")

            'If dtServiceRecordMasterSetup.Rows(0)("AutoRenewal") = 1 Then
            '    chkAutoRenew.Visible = True
            '    'btnAutoRenew.Visible = True
            '    txtbtnAutoRenew.Text = "Y"
            'Else
            '    chkAutoRenew.Visible = False
            '    btnAutoRenew.Visible = False
            '    txtbtnAutoRenew.Text = "N"
            'End If


            If txtDisplayRecordsLocationwise.Text = "Y" Then
                tb1.Tabs(9).Visible = True
            Else
                tb1.Tabs(9).Visible = False

            End If
            conn.Close()
            conn.Dispose()
            dtServiceRecordMasterSetup.Dispose()
            drServiceRecordMasterSetup.Close()
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            tb1.ActiveTabIndex = 0
        End If
        CheckTab()
        'If Not (ViewState("ActiveTabIndex") Is Nothing) Then

        '    tb1.ActiveTabIndex = CInt(ViewState("ActiveTabIndex"))
        'End If
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
                cmd.Dispose()
            End Using
            'End Using
        Catch ex As Exception
            InsertIntoTblWebEventLog("USERACCESS - " + Session("UserID"), "FUNCTION PopulateDropDownList", ex.Message.ToString, textField.Trim() & valueField.Trim())
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
    Private Sub CheckTab()
        '   If tb1.ActiveTabIndex = 1 Or tb1.ActiveTabIndex = 2 Or tb1.ActiveTabIndex = 3 Or tb1.ActiveTabIndex = 4 Or tb1.ActiveTabIndex = 5 Then
        If tb1.ActiveTabIndex <> 0 Then
            GridView1.CssClass = "dummybutton"
            btnQuit.CssClass = "dummybutton"
        ElseIf tb1.ActiveTabIndex = 0 Then

            GridView1.CssClass = "visiblebutton"
            btnQuit.CssClass = "visiblebutton"
        End If

    End Sub


    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtGroupAuthority.Text = "" Then
            '  MessageBox.Message.Alert(Page, "Group Authority cannot be blank!!!", "str")
            lblAlert.Text = "GROUP AUTHORITY CANNOT BE BLANK"
            Return

        End If
        If txtMode.Text = "New" Then
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                command1.CommandText = "SELECT * FROM tblGroupAccess where GroupAccess=@GroupAccess"
                command1.Parameters.AddWithValue("@GroupAccess", txtGroupAuthority.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    '  MessageBox.Message.Alert(Page, "Record already exists!!!", "str")
                    lblAlert.Text = "RECORD ALREADY EXISTS"
                    txtGroupAuthority.Focus()
                    Exit Sub
                Else

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "INSERT INTO tblGroupAccess(GroupAccess,Comments) VALUES(@GroupAccess,@Comments);"
                    '   Dim qry As String = "INSERT INTO tblGroupAccess(GroupAccess,x0301,x0302,x0302Add,x0302Edit,x0302Delete,x0302Trans,x0303,x0303Add,x0303Edit,x0303Delete,x0303Trans,x0304,x0304Add,x0304Edit,x0304Delete,x0304Trans,x0304Security,x0305,x0302EditAcct,x0302Change,x0302Notes,x0303Notes,x0302ViewAll,x0306,x2414,x2414Add,x2414Edit,x2414Delete,x2414Print,x2414Button,x2411,x2411Set1,x2411JobOrder,x2411Asset,x2411Contract,x2411Set2,x2415Print,x2415ChSt,x2415,x2415Add,x2415Edit,x2415Delete,x2412,x2412Add,x2412Edit,x2412Delete,x2412Print,x2412ChSt,x2412Update,x2412Reverse,x2412Process,x2412Early,x2412Term,x2412Cancel,x2412Renewal,x2413Print,x2413ChSt,x2413Update,x2413,x2413Add,x2413Edit,x2413Delete,x2413Reverse,x2401,x2416,x2416Add,x2416Edit,x2416Delete,x2416Print,x2416ChSt,x2416ToQuote,x2416JobOrder,x2416KIV,x2416Noted,x2416Attended,x2416Voided,x2416Improvement,x2416SMS,x2416ActionBy,x2416EditActionNotes,x2416ViewAll,x2413ViewAll,x2414ViewAll,x2417,x2417Add,x2417Edit,x2417Print,x2417ChSt,x2412ServiceEButton,Comments)VALUES(@GroupAccess,@x0301,@x0302,@x0302Add,@x0302Edit,@x0302Delete,@x0302Trans,@x0303,@x0303Add,@x0303Edit,@x0303Delete,@x0303Trans,@x0304,@x0304Add,@x0304Edit,@x0304Delete,@x0304Trans,@x0304Security,@x0305,@x0302EditAcct,@x0302Change,@x0302Notes,@x0303Notes,@x0302ViewAll,@x0306,@x2414,@x2414Add,@x2414Edit,@x2414Delete,@x2414Print,@x2414Button,@x2411,@x2411Set1,@x2411JobOrder,@x2411Asset,@x2411Contract,@x2411Set2,@x2415Print,@x2415ChSt,@x2415,@x2415Add,@x2415Edit,@x2415Delete,@x2412,@x2412Add,@x2412Edit,@x2412Delete,@x2412Print,@x2412ChSt,@x2412Update,@x2412Reverse,@x2412Process,@x2412Early,@x2412Term,@x2412Cancel,@x2412Renewal,@x2413Print,@x2413ChSt,@x2413Update,@x2413,@x2413Add,@x2413Edit,@x2413Delete,@x2413Reverse,@x2401,@x2416,@x2416Add,@x2416Edit,@x2416Delete,@x2416Print,@x2416ChSt,@x2416ToQuote,@x2416JobOrder,@x2416KIV,@x2416Noted,@x2416Attended,@x2416Voided,@x2416Improvement,@x2416SMS,@x2416ActionBy,@x2416EditActionNotes,@x2416ViewAll,@x2413ViewAll,@x2414ViewAll,@x2417,@x2417Add,@x2417Edit,@x2417Print,@x2417ChSt,@x2412ServiceEButton,@Comments);"
                    command.CommandText = qry
                    command.Parameters.Clear()

                    command.Parameters.AddWithValue("@GroupAccess", txtGroupAuthority.Text.ToUpper)
                    ''If chkAccess.Checked = True Then
                    ''    command.Parameters.AddWithValue("@x0301", 1)
                    ''Else
                    ''    command.Parameters.AddWithValue("@x0301", 0)
                    ''End If
                    'command.Parameters.AddWithValue("@x0302", 0)
                    'command.Parameters.AddWithValue("@x0302Add", 0)
                    'command.Parameters.AddWithValue("@x0302Edit", 0)
                    'command.Parameters.AddWithValue("@x0302Delete", 0)
                    'command.Parameters.AddWithValue("@x0302Trans", 0)
                    'command.Parameters.AddWithValue("@x0303", 0)
                    'command.Parameters.AddWithValue("@x0303Add", 0)
                    'command.Parameters.AddWithValue("@x0303Edit", 0)
                    'command.Parameters.AddWithValue("@x0303Delete", 0)
                    'command.Parameters.AddWithValue("@x0303Trans", 0)
                    'command.Parameters.AddWithValue("@x0304", 0)
                    'command.Parameters.AddWithValue("@x0304Add", 0)
                    'command.Parameters.AddWithValue("@x0304Edit", 0)
                    'command.Parameters.AddWithValue("@x0304Delete", 0)
                    'command.Parameters.AddWithValue("@x0304Trans", 0)
                    'command.Parameters.AddWithValue("@x0304Security", 0)
                    'command.Parameters.AddWithValue("@x0305", 0)
                    'command.Parameters.AddWithValue("@x0302EditAcct", 0)
                    'command.Parameters.AddWithValue("@x0302Change", 0)
                    'command.Parameters.AddWithValue("@x0302Notes", 0)
                    'command.Parameters.AddWithValue("@x0303Notes", 0)
                    'command.Parameters.AddWithValue("@x0302ViewAll", 0)
                    'command.Parameters.AddWithValue("@x0306", 0)
                    'command.Parameters.AddWithValue("@x2414", 0)
                    'command.Parameters.AddWithValue("@x2414Add", 0)
                    'command.Parameters.AddWithValue("@x2414Edit", 0)
                    'command.Parameters.AddWithValue("@x2414Delete", 0)
                    'command.Parameters.AddWithValue("@x2414Print", 0)
                    'command.Parameters.AddWithValue("@x2414Button", 0)
                    'command.Parameters.AddWithValue("@x2411", 0)
                    'command.Parameters.AddWithValue("@x2411Set1", 0)
                    'command.Parameters.AddWithValue("@x2411JobOrder", 0)
                    'command.Parameters.AddWithValue("@x2411Asset", 0)
                    'command.Parameters.AddWithValue("@x2411Contract", 0)
                    'command.Parameters.AddWithValue("@x2411Set2", 0)
                    'command.Parameters.AddWithValue("@x2415Print", 0)
                    'command.Parameters.AddWithValue("@x2415ChSt", 0)
                    'command.Parameters.AddWithValue("@x2415", 0)
                    'command.Parameters.AddWithValue("@x2415Add", 0)
                    'command.Parameters.AddWithValue("@x2415Edit", 0)
                    'command.Parameters.AddWithValue("@x2415Delete", 0)
                    'command.Parameters.AddWithValue("@x2412", 0)
                    'command.Parameters.AddWithValue("@x2412Add", 0)
                    'command.Parameters.AddWithValue("@x2412Edit", 0)
                    'command.Parameters.AddWithValue("@x2412Delete", 0)
                    'command.Parameters.AddWithValue("@x2412Print", 0)
                    'command.Parameters.AddWithValue("@x2412ChSt", 0)
                    'command.Parameters.AddWithValue("@x2412Update", 0)
                    'command.Parameters.AddWithValue("@x2412Reverse", 0)
                    'command.Parameters.AddWithValue("@x2412Process", 0)
                    'command.Parameters.AddWithValue("@x2412Early", 0)
                    'command.Parameters.AddWithValue("@x2412Term", 0)
                    'command.Parameters.AddWithValue("@x2412Cancel", 0)
                    'command.Parameters.AddWithValue("@x2412Renewal", 0)
                    'command.Parameters.AddWithValue("@x2413Print", 0)
                    'command.Parameters.AddWithValue("@x2413ChSt", 0)
                    'command.Parameters.AddWithValue("@x2413Update", 0)
                    'command.Parameters.AddWithValue("@x2413", 0)
                    'command.Parameters.AddWithValue("@x2413Add", 0)
                    'command.Parameters.AddWithValue("@x2413Edit", 0)
                    'command.Parameters.AddWithValue("@x2413Delete", 0)
                    'command.Parameters.AddWithValue("@x2413Reverse", 0)
                    'command.Parameters.AddWithValue("@x2401", 0)
                    'command.Parameters.AddWithValue("@x2416", 0)
                    'command.Parameters.AddWithValue("@x2416Add", 0)
                    'command.Parameters.AddWithValue("@x2416Edit", 0)
                    'command.Parameters.AddWithValue("@x2416Delete", 0)
                    'command.Parameters.AddWithValue("@x2416Print", 0)
                    'command.Parameters.AddWithValue("@x2416ChSt", 0)
                    'command.Parameters.AddWithValue("@x2416ToQuote", 0)
                    'command.Parameters.AddWithValue("@x2416JobOrder", 0)
                    'command.Parameters.AddWithValue("@x2416KIV", 0)
                    'command.Parameters.AddWithValue("@x2416Noted", 0)
                    'command.Parameters.AddWithValue("@x2416Attended", 0)
                    'command.Parameters.AddWithValue("@x2416Voided", 0)
                    'command.Parameters.AddWithValue("@x2416Improvement", 0)
                    'command.Parameters.AddWithValue("@x2416SMS", 0)
                    'command.Parameters.AddWithValue("@x2416ActionBy", 0)
                    'command.Parameters.AddWithValue("@x2416EditActionNotes", 0)
                    'command.Parameters.AddWithValue("@x2416ViewAll", 0)
                    'command.Parameters.AddWithValue("@x2413ViewAll", 0)
                    'command.Parameters.AddWithValue("@x2414ViewAll", 0)
                    'command.Parameters.AddWithValue("@x2417", 0)
                    'command.Parameters.AddWithValue("@x2417Add", 0)
                    'command.Parameters.AddWithValue("@x2417Edit", 0)
                    'command.Parameters.AddWithValue("@x2417Print", 0)
                    'command.Parameters.AddWithValue("@x2417ChSt", 0)
                    'command.Parameters.AddWithValue("@x2412ServiceEButton", 0)


                    command.Parameters.AddWithValue("@Comments", txtComments.Text)

                    command.Connection = conn

                    command.ExecuteNonQuery()
                    txtRcno.Text = command.LastInsertedId

                    Dim command2 As MySqlCommand = New MySqlCommand

                    command2.CommandType = CommandType.Text
                    Dim qry1 As String = "INSERT INTO tblGroupAccess(GroupAccess,Level) VALUES(@GroupAccess,@level);"
                    ' Dim qry1 As String = "INSERT INTO tblGroupAccess(GroupAccess,x0307,x0304ChSt,x0304Print,Level,x0304GroupAuthority,x0304HRSecurityLevel,x0304HRViewSameLevel,x0307Access,x0307Add,x0307Edit,x0307Delete,x2413ApprMode,x2414Assign,x2417PostToWeb,x2413AssignServiceRecord,x0308Access,x0304EDocumentAccess,x0304EDocumentAdd,x0304EDocumentEdit,x0304EDocumentDelete,x2414PR,x2414ReAsignEvent,x0302ShowARBalance,x0302ShowAPBalance,x0303ShowARBalance,x0303ShowAPBalance,x0303EditAcct,x2418,x2418Add,x2418Edit,x2418Delete,x2419,x2419Add,x2419Edit,x2419Delete,x2420,x2420Add,x2420Edit,x2420Delete,x2421,x2421Add,x2421Edit,x2421Delete,x2422,x2422Add,x2422Edit,x2422Delete,x2423,x2424,x2413DownloadFromWeb,x2413TabletMessage,x2413TabletMessageEdit,x2425,x2425Add,x2425Edit,x2425DetailModify,x2425ChSt,x2426,x2427JobOrder,x2427Management,x2427Others,x2427Revenue,x2427ServiceContract,x2427ServiceRecord,x2427Portfolio,x2428,x24DueDateMonitoring,x24EventMonitoring,x24ContractGroupEnquiry,x0304EDocumentConfidential,x2429,x2412EditAgreeValue,x2416Designer)VALUES(@GroupAccess,@x0307,@x0304ChSt,@x0304Print,@Level,@x0304GroupAuthority,@x0304HRSecurityLevel,@x0304HRViewSameLevel,@x0307Access,@x0307Add,@x0307Edit,@x0307Delete,@x2413ApprMode,@x2414Assign,@x2417PostToWeb,@x2413AssignServiceRecord,@x0308Access,@x0304EDocumentAccess,@x0304EDocumentAdd,@x0304EDocumentEdit,@x0304EDocumentDelete,@x2414PR,@x2414ReAsignEvent,@x0302ShowARBalance,@x0302ShowAPBalance,@x0303ShowARBalance,@x0303ShowAPBalance,@x0303EditAcct,@x2418,@x2418Add,@x2418Edit,@x2418Delete,@x2419,@x2419Add,@x2419Edit,@x2419Delete,@x2420,@x2420Add,@x2420Edit,@x2420Delete,@x2421,@x2421Add,@x2421Edit,@x2421Delete,@x2422,@x2422Add,@x2422Edit,@x2422Delete,@x2423,@x2424,@x2413DownloadFromWeb,@x2413TabletMessage,@x2413TabletMessageEdit,@x2425,@x2425Add,@x2425Edit,@x2425DetailModify,@x2425ChSt,@x2426,@x2427JobOrder,@x2427Management,@x2427Others,@x2427Revenue,@x2427ServiceContract,@x2427ServiceRecord,@x2427Portfolio,@x2428,@x24DueDateMonitoring,@x24EventMonitoring,@x24ContractGroupEnquiry,@x0304EDocumentConfidential,@x2429,@x2412EditAgreeValue,@x2416Designer);"
                    command2.CommandText = qry1
                    command2.Parameters.Clear()

                    command2.Parameters.AddWithValue("@GroupAccess", txtGroupAuthority.Text.ToUpper)
                    command2.Parameters.AddWithValue("@Level", 0)
                    ''If chkAccess.Checked = True Then
                    ''    command2.Parameters.AddWithValue("@Level", 1)
                    ''Else
                    ''    command2.Parameters.AddWithValue("@Level", 0)
                    ''End If
                    'command2.Parameters.AddWithValue("@x0307", 0)
                    'command2.Parameters.AddWithValue("@x0304ChSt", 0)
                    'command2.Parameters.AddWithValue("@x0304Print", 0)
                    'command2.Parameters.AddWithValue("@x0304GroupAuthority", 0)
                    'command2.Parameters.AddWithValue("@x0304HRSecurityLevel", 0)
                    'command2.Parameters.AddWithValue("@x0304HRViewSameLevel", 0)
                    'command2.Parameters.AddWithValue("@x0307Access", 0)
                    'command2.Parameters.AddWithValue("@x0307Add", 0)
                    'command2.Parameters.AddWithValue("@x0307Edit", 0)
                    'command2.Parameters.AddWithValue("@x0307Delete", 0)
                    'command2.Parameters.AddWithValue("@x2413ApprMode", 0)
                    'command2.Parameters.AddWithValue("@x2414Assign", 0)
                    'command2.Parameters.AddWithValue("@x2417PostToWeb", 0)
                    'command2.Parameters.AddWithValue("@x2413AssignServiceRecord", 0)
                    'command2.Parameters.AddWithValue("@x0308Access", 0)
                    'command2.Parameters.AddWithValue("@x0304EDocumentAccess", 0)
                    'command2.Parameters.AddWithValue("@x0304EDocumentAdd", 0)
                    'command2.Parameters.AddWithValue("@x0304EDocumentEdit", 0)
                    'command2.Parameters.AddWithValue("@x0304EDocumentDelete", 0)
                    'command2.Parameters.AddWithValue("@x2414PR", 0)
                    'command2.Parameters.AddWithValue("@x2414ReAsignEvent", 0)
                    'command2.Parameters.AddWithValue("@x0302ShowARBalance", 0)
                    'command2.Parameters.AddWithValue("@x0302ShowAPBalance", 0)
                    'command2.Parameters.AddWithValue("@x0303ShowARBalance", 0)
                    'command2.Parameters.AddWithValue("@x0303ShowAPBalance", 0)
                    'command2.Parameters.AddWithValue("@x0303EditAcct", 0)
                    'command2.Parameters.AddWithValue("@x2418", 0)
                    'command2.Parameters.AddWithValue("@x2418Add", 0)
                    'command2.Parameters.AddWithValue("@x2418Edit", 0)
                    'command2.Parameters.AddWithValue("@x2418Delete", 0)
                    'command2.Parameters.AddWithValue("@x2419", 0)
                    'command2.Parameters.AddWithValue("@x2419Add", 0)
                    'command2.Parameters.AddWithValue("@x2419Edit", 0)
                    'command2.Parameters.AddWithValue("@x2419Delete", 0)
                    'command2.Parameters.AddWithValue("@x2420", 0)
                    'command2.Parameters.AddWithValue("@x2420Add", 0)
                    'command2.Parameters.AddWithValue("@x2420Edit", 0)
                    'command2.Parameters.AddWithValue("@x2420Delete", 0)
                    'command2.Parameters.AddWithValue("@x2421", 0)
                    'command2.Parameters.AddWithValue("@x2421Add", 0)
                    'command2.Parameters.AddWithValue("@x2421Edit", 0)
                    'command2.Parameters.AddWithValue("@x2421Delete", 0)
                    'command2.Parameters.AddWithValue("@x2422", 0)
                    'command2.Parameters.AddWithValue("@x2422Add", 0)
                    'command2.Parameters.AddWithValue("@x2422Edit", 0)
                    'command2.Parameters.AddWithValue("@x2422Delete", 0)
                    'command2.Parameters.AddWithValue("@x2423", 0)
                    'command2.Parameters.AddWithValue("@x2424", 0)
                    'command2.Parameters.AddWithValue("@x2413DownloadFromWeb", 0)
                    'command2.Parameters.AddWithValue("@x2413TabletMessage", 0)
                    'command2.Parameters.AddWithValue("@x2413TabletMessageEdit", 0)
                    'command2.Parameters.AddWithValue("@x2425", 0)
                    'command2.Parameters.AddWithValue("@x2425Add", 0)
                    'command2.Parameters.AddWithValue("@x2425Edit", 0)
                    'command2.Parameters.AddWithValue("@x2425DetailModify", 0)
                    'command2.Parameters.AddWithValue("@x2425ChSt", 0)
                    'command2.Parameters.AddWithValue("@x2426", 0)
                    'command2.Parameters.AddWithValue("@x2427JobOrder", 0)
                    'command2.Parameters.AddWithValue("@x2427Management", 0)
                    'command2.Parameters.AddWithValue("@x2427Others", 0)
                    'command2.Parameters.AddWithValue("@x2427Revenue", 0)
                    'command2.Parameters.AddWithValue("@x2427ServiceContract", 0)
                    'command2.Parameters.AddWithValue("@x2427ServiceRecord", 0)
                    'command2.Parameters.AddWithValue("@x2427Portfolio", 0)
                    'command2.Parameters.AddWithValue("@x2428", 0)
                    'command2.Parameters.AddWithValue("@x24DueDateMonitoring", 0)
                    'command2.Parameters.AddWithValue("@x24EventMonitoring", 0)
                    'command2.Parameters.AddWithValue("@x24ContractGroupEnquiry", 0)
                    'command2.Parameters.AddWithValue("@x0304EDocumentConfidential", 0)
                    'command2.Parameters.AddWithValue("@x2429", 0)
                    'command2.Parameters.AddWithValue("@x2412EditAgreeValue", 0)
                    'command2.Parameters.AddWithValue("@x2416Designer", 0)


                    command2.Connection = conn

                    command2.ExecuteNonQuery()
                    txtRcNo2.Text = command2.LastInsertedId
                    'MessageBox.Message.Alert(Page, "Record added successfully!!!", "str")
                    lblMessage.Text = "ADD: RECORD SUCCESSFULLY ADDED"
                    lblAlert.Text = ""
                End If
                conn.Close()

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
            'If txtExists.Text = "True" Then
            '    '  MessageBox.Message.Alert(Page, "Record is in use, cannot be modified!!!", "str")
            '    lblAlert.Text = "RECORD IS IN USE, SO CANNOT BE MODIFIED"
            '    Return
            'End If
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                'Dim ind As String
                'ind = txtIndustry.Text
                'ind = ind.Replace("'", "\\'")

                Dim command2 As MySqlCommand = New MySqlCommand

                command2.CommandType = CommandType.Text

                command2.CommandText = "SELECT * FROM tblGroupAccess where GroupAccess=@GroupAccess and rcno<>" & Convert.ToInt32(txtRcno.Text)
                command2.Parameters.AddWithValue("@GroupAccess", txtGroupAuthority.Text)
                command2.Connection = conn

                Dim dr1 As MySqlDataReader = command2.ExecuteReader()
                Dim dt1 As New DataTable
                dt1.Load(dr1)

                If dt1.Rows.Count > 0 Then

                    ' MessageBox.Message.Alert(Page, "Group Authority already exists!!!", "str")
                    lblAlert.Text = "GROUP AUTHORITY ALREADY EXISTS"
                    txtGroupAuthority.Focus()
                    Exit Sub
                Else

                    Dim command1 As MySqlCommand = New MySqlCommand

                    command1.CommandType = CommandType.Text

                    command1.CommandText = "SELECT * FROM tblGroupAccess where rcno=" & Convert.ToInt32(txtRcno.Text)
                    command1.Connection = conn

                    Dim dr As MySqlDataReader = command1.ExecuteReader()
                    Dim dt As New DataTable
                    dt.Load(dr)

                    If dt.Rows.Count > 0 Then

                        Dim command As MySqlCommand = New MySqlCommand

                        command.CommandType = CommandType.Text


                        Dim qry As String = "update tblGroupAccess set Comments=@Comments where rcno=" & Convert.ToInt32(txtRcno.Text)

                        command.CommandText = qry
                        command.Parameters.Clear()

                        '  command.Parameters.AddWithValue("@GroupAccess", txtGroupAuthority.Text.ToUpper)
                        'If chkAccess.Checked = True Then
                        '    command.Parameters.AddWithValue("@Access", 1)
                        'Else
                        '    command.Parameters.AddWithValue("@Access", 0)
                        'End If
                        command.Parameters.AddWithValue("@Comments", txtComments.Text)

                        command.Connection = conn

                        command.ExecuteNonQuery()

                        Dim command3 As MySqlCommand = New MySqlCommand

                        command3.CommandType = CommandType.Text
                        Dim qry3 As String = "update tblGroupAccess set GroupAccess=@GroupAccess,Level=@Access where rcno=" & Convert.ToInt32(txtRcNo2.Text)

                        command3.CommandText = qry
                        command3.Parameters.Clear()

                        command3.Parameters.AddWithValue("@GroupAccess", txtGroupAuthority.Text.ToUpper)
                        'If chkAccess.Checked = True Then
                        '    command3.Parameters.AddWithValue("@Access", 1)
                        'Else
                        command3.Parameters.AddWithValue("@Access", 0)
                        'End If

                        command3.Connection = conn

                        command3.ExecuteNonQuery()
                        '  MessageBox.Message.Alert(Page, "Record updated successfully!!!", "str")
                        lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED"
                    End If
                End If

                conn.Close()

            Catch ex As Exception
                MessageBox.Message.Alert(Page, ex.ToString, "str")
            End Try
            EnableControls()
            txtMode.Text = ""
        End If

        GridView1.DataSourceID = "SqlDataSource1"
        '  MakeMeNull()
        If CheckIfExists() = True Then
            txtExists.Text = "True"
        Else
            txtExists.Text = "False"
        End If
    End Sub

    Protected Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If txtRcno.Text = "" Then
            '  MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
            lblAlert.Text = "SELECT RECORD TO EDIT"
            Return

        End If
        DisableControls()
        txtGroupAuthority.Enabled = False

        txtMode.Text = "Edit"
        lblMessage.Text = "ACTION: EDIT RECORD"
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        MakeMeNull()
        EnableControls()

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

            If txtExists.Text = "True" Then
                ' MessageBox.Message.Alert(Page, "Record is in use, cannot be modified!!!", "str")
                lblAlert.Text = "RECORD IS IN USE, CANNOT BE DELETED"
                Return
            End If


            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                command1.CommandText = "SELECT * FROM tblGroupAccess where rcno=" & Convert.ToInt32(txtRcno.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text

                    Dim qry As String = "delete from tblGroupAccess where rcno=" & Convert.ToInt32(txtRcno.Text)

                    command.CommandText = qry
                    command.Connection = conn

                    command.ExecuteNonQuery()

                    Dim command2 As MySqlCommand = New MySqlCommand
                    command2.CommandType = CommandType.Text
                    command2.CommandText = "delete from tblGroupAccess where rcno=" & Convert.ToInt32(txtRcNo2.Text)
                    command2.Connection = conn
                    command2.ExecuteNonQuery()

                    '  MessageBox.Message.Alert(Page, "Record deleted successfully!!!", "str")
                    lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"
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
        Response.Redirect("RV_MasterGroupAuthority.aspx")
    End Sub

    Private Function CheckIfExists() As Boolean
        'Dim conn As MySqlConnection = New MySqlConnection()

        'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        'conn.Open()

        'Dim command1 As MySqlCommand = New MySqlCommand

        'command1.CommandType = CommandType.Text

        'command1.CommandText = "SELECT * FROM tblstaff where SecGroupAuthority=@data"
        'command1.Parameters.AddWithValue("@data", txtGroupAuthority.Text)
        'command1.Connection = conn

        'Dim dr1 As MySqlDataReader = command1.ExecuteReader()
        'Dim dt1 As New DataTable
        'dt1.Load(dr1)

        'If dt1.Rows.Count > 0 Then
        '    Return True
        'End If

        'conn.Close()

        'Return False
    End Function


    Private Sub EnableSvcAccessControls()
        btnSaveSvcAccess.Enabled = False
        btnSaveSvcAccess.ForeColor = System.Drawing.Color.Gray
        btnCancelSvcAccess.Enabled = False
        btnCancelSvcAccess.ForeColor = System.Drawing.Color.Gray

        btnEditSvcAccess.Enabled = True
        btnEditSvcAccess.ForeColor = System.Drawing.Color.Black

        chkServiceList.Enabled = False
        chkContractList.Enabled = False

        chkSvcSelectAll.Enabled = False

        txtModeSvcAccess.Text = ""
    End Sub

    Private Sub DisableSvcAccessControls()
        btnSaveSvcAccess.Enabled = True
        btnSaveSvcAccess.ForeColor = System.Drawing.Color.Black
        btnCancelSvcAccess.Enabled = True
        btnCancelSvcAccess.ForeColor = System.Drawing.Color.Black

        btnEditSvcAccess.Enabled = False
        btnEditSvcAccess.ForeColor = System.Drawing.Color.Gray

        chkServiceList.Enabled = True
        chkContractList.Enabled = True

        chkSvcSelectAll.Enabled = True

    End Sub

    Protected Sub btnEditSvcAccess_Click(sender As Object, e As EventArgs) Handles btnEditSvcAccess.Click
        lblAlert.Text = ""
        If txtRcno.Text = "" Then
            ' MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
            lblAlert.Text = "SELECT GROUP AUTHORITY TO EDIT"
            Return

        End If
        DisableSvcAccessControls()
        lblMessage.Text = "ACTION: EDIT SERVICE AND CONTRACT ACCESS RECORDS"
        txtModeSvcAccess.Text = "EDIT"
    End Sub

    Protected Sub btnCancelSvcAccess_Click(sender As Object, e As EventArgs) Handles btnCancelSvcAccess.Click
        lblAlert.Text = ""
        EnableSvcAccessControls()

    End Sub

    Protected Sub btnSaveSvcAccess_Click(sender As Object, e As EventArgs) Handles btnSaveSvcAccess.Click
        lblAlert.Text = ""
        lblMessage.Text = ""
        'If txtRcno.Text = "" Or txtRcNo2.Text = "" Then
        If txtRcno.Text = "" Then
            ' MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
            lblAlert.Text = "SELECT RECORD TO EDIT"
            Return

        End If

        '     Try
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        'Dim ind As String
        'ind = txtIndustry.Text
        'ind = ind.Replace("'", "\\'")

        Dim command2 As MySqlCommand = New MySqlCommand

        command2.CommandType = CommandType.Text

        command2.CommandText = "SELECT * FROM tblGroupAccess where GroupAccess=@GroupAccess and rcno<>" & Convert.ToInt32(txtRcno.Text)
        command2.Parameters.AddWithValue("@GroupAccess", txtGroupAuthority.Text)
        command2.Connection = conn

        Dim dr1 As MySqlDataReader = command2.ExecuteReader()
        Dim dt1 As New DataTable
        dt1.Load(dr1)

        If dt1.Rows.Count > 0 Then

            ' MessageBox.Message.Alert(Page, "Group Authority already exists!!!", "str")
            lblAlert.Text = "Group Authority ALREADY EXISTS"
            txtGroupAuthority.Focus()
            Exit Sub
        Else

            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.CommandText = "SELECT * FROM tblGroupAccess where rcno=" & Convert.ToInt32(txtRcno.Text)
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then

                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                'Dim qry As String = "update tblGroupAccess set x2412EditPortfolioValue = @x2412EditPortfolioValue,x2412 = @x2412,x2412Add = @x2412Add ,x2412Copy = @x2412Copy ,x2412Edit = @x2412Edit,x2412Delete = @x2412Delete,x2412Print = @x2412Print,x2412ChSt = @x2412ChSt,x2412Update = @x2412Update,x2412Reverse = @x2412Reverse,x2412Process = @x2412Process,x2412Early = @x2412Early,x2412Term = @x2412Term,x2412Cancel = @x2412Cancel,x2412Renewal = @x2412Renewal,x2413Print = @x2413Print,x2413ChSt = @x2413ChSt,x2413Update = @x2413Update,x2413 = @x2413,x2413Add = @x2413Add,x2413Copy = @x2413Copy,x2413Edit = @x2413Edit,x2413Delete = @x2413Delete,x2413Reverse = @x2413Reverse,x2413ViewAll = @x2413ViewAll,x2413Recalculate = @x2413Recalculate, x2412EditOurRef = @x2412EditOurRef, x2412EditManualContractNo = @x2412EditManualContractNo,x2412EditPoNo = @x2412EditPoNo,x2412EditNotes = @x2412EditNotes,  x2413ApprMode=@x2413ApprMode,x2413AssignServiceRecord=@x2413AssignServiceRecord, x2412EditAgreeValue =@x2412EditAgreeValue, x2413ExportToExcel = @x2413ExportToExcel, x2412SORAccess=@x2412SORAccess, x2412SORAdd=@x2412SORAdd, x2412SOREdit=@x2412SOREdit, x2412SORDelete=@x2412SORDelete, x2412BackDateContract=@x2412BackDateContract, x2412BackDateContractTermination=@x2412BackDateContractTermination, x2412BackDateContractSameMonthOnly=@x2412BackDateContractSameMonthOnly, x2412BackDateContractTerminationSameMonthOnly=@x2412BackDateContractTerminationSameMonthOnly, x2412FileAccess=@x2412FileAccess, x2412FileUpload=@x2412FileUpload, x2412FileDelete=@x2412FileDelete, x2413FileAccess=@x2413FileAccess, x2413FileUpload=@x2413FileUpload, x2413FileDelete=@x2413FileDelete, x2412EditAutoRenewal=@x2412EditAutoRenewal, x2413PestCount=@x2413PestCount, x2412BillingFrequency=@x2412BillingFrequency, x2412FutureDateContractTermination =@x2412FutureDateContractTermination, x2412RegenerateSchedule=@x2412RegenerateSchedule, x2412ExtendContractEndDate=@x2412ExtendContractEndDate, x2412Distribution =@x2412Distribution, x2412VoidContract =@x2412VoidContract, x2413VoidService =@x2413VoidService, x2412WarrantyDates=@x2412WarrantyDates, x2413ManualContractPOWONo =@x2413ManualContractPOWONo, x2412Revision=@x2412Revision, x2412AgreementTypeContractCode=@x2412AgreementTypeContractCode where rcno=" & Convert.ToInt32(txtRcno.Text)

                Dim qry As String = "update tblGroupAccess set x2412EditPortfolioValue = @x2412EditPortfolioValue,"
                qry = qry + " x2412 = @x2412,x2412Add = @x2412Add ,x2412Copy = @x2412Copy ,x2412Edit = @x2412Edit,x2412Delete = @x2412Delete, "
                qry = qry + " x2412Print = @x2412Print,x2412ChSt = @x2412ChSt,x2412Update = @x2412Update,x2412Reverse = @x2412Reverse, "
                qry = qry + " x2412Process = @x2412Process,x2412Early = @x2412Early,x2412Term = @x2412Term,x2412Cancel = @x2412Cancel,"
                qry = qry + " x2412Renewal = @x2412Renewal,x2413Print = @x2413Print,x2413ChSt = @x2413ChSt,x2413Update = @x2413Update, "
                qry = qry + " x2413 = @x2413,x2413Add = @x2413Add,x2413Copy = @x2413Copy,x2413Edit = @x2413Edit,x2413Delete = @x2413Delete,"
                qry = qry + " x2413Reverse = @x2413Reverse,x2413ViewAll = @x2413ViewAll,x2413Recalculate = @x2413Recalculate, "
                qry = qry + " x2412EditOurRef = @x2412EditOurRef, x2412EditManualContractNo = @x2412EditManualContractNo,x2412EditPoNo = @x2412EditPoNo,"
                qry = qry + " x2412EditNotes = @x2412EditNotes,  x2413ApprMode=@x2413ApprMode,x2413AssignServiceRecord=@x2413AssignServiceRecord, "
                qry = qry + " x2412EditAgreeValue =@x2412EditAgreeValue, x2413ExportToExcel = @x2413ExportToExcel, x2412SORAccess=@x2412SORAccess, "
                qry = qry + " x2412SORAdd=@x2412SORAdd, x2412SOREdit=@x2412SOREdit, x2412SORDelete=@x2412SORDelete, x2412BackDateContract=@x2412BackDateContract, "
                qry = qry + " x2412BackDateContractTermination=@x2412BackDateContractTermination, x2412BackDateContractSameMonthOnly=@x2412BackDateContractSameMonthOnly, "
                qry = qry + " x2412BackDateContractTerminationSameMonthOnly=@x2412BackDateContractTerminationSameMonthOnly, x2412FileAccess=@x2412FileAccess, "
                qry = qry + " x2412FileUpload=@x2412FileUpload, x2412FileDelete=@x2412FileDelete, x2413FileAccess=@x2413FileAccess, x2413FileUpload=@x2413FileUpload, "
                qry = qry + " x2413FileDelete=@x2413FileDelete, x2412EditAutoRenewal=@x2412EditAutoRenewal, x2413PestCount=@x2413PestCount, x2412BillingFrequency=@x2412BillingFrequency, "
                qry = qry + " x2412FutureDateContractTermination =@x2412FutureDateContractTermination, x2412RegenerateSchedule=@x2412RegenerateSchedule, x2412ExtendContractEndDate=@x2412ExtendContractEndDate, "
                qry = qry + " x2412Distribution =@x2412Distribution, x2412VoidContract =@x2412VoidContract, x2413VoidService =@x2413VoidService, x2412WarrantyDates=@x2412WarrantyDates, "
                qry = qry + " x2413ManualContractPOWONo =@x2413ManualContractPOWONo, x2412Revision=@x2412Revision, x2413EditBilledAmtBillNo=@x2413EditBilledAmtBillNo,  x2412AgreementTypeContractCode=@x2412AgreementTypeContractCode "
                qry = qry + " where rcno=" & Convert.ToInt32(txtRcno.Text)


                command.CommandText = qry
                command.Parameters.Clear()

                '1
                If chkContractList.Items.FindByValue("EditPortfolioValue").Selected = True Then
                    command.Parameters.AddWithValue("@x2412EditPortfolioValue", 1)
                Else
                    command.Parameters.AddWithValue("@x2412EditPortfolioValue", 0)
                End If

                '2
                If chkContractList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x2412", 1)
                Else
                    command.Parameters.AddWithValue("@x2412", 0)
                End If

                '3
                If chkContractList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x2412Add", 1)
                Else
                    command.Parameters.AddWithValue("@x2412Add", 0)
                End If

                '4
                If chkContractList.Items.FindByValue("Copy").Selected = True Then
                    command.Parameters.AddWithValue("@x2412Copy", 1)
                Else
                    command.Parameters.AddWithValue("@x2412Copy", 0)
                End If

                '5
                If chkContractList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x2412Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x2412Edit", 0)
                End If

                '6
                If chkContractList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x2412Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x2412Delete", 0)
                End If

                '7
                If chkContractList.Items.FindByValue("Print").Selected = True Then
                    command.Parameters.AddWithValue("@x2412Print", 1)
                Else
                    command.Parameters.AddWithValue("@x2412Print", 0)
                End If

                '8
                If chkContractList.Items.FindByValue("ChangeStatus").Selected = True Then
                    command.Parameters.AddWithValue("@x2412ChSt", 1)
                Else
                    command.Parameters.AddWithValue("@x2412ChSt", 0)
                End If

                '9
                If chkContractList.Items.FindByValue("Update").Selected = True Then
                    command.Parameters.AddWithValue("@x2412Update", 1)
                Else
                    command.Parameters.AddWithValue("@x2412Update", 0)
                End If

                '10
                If chkContractList.Items.FindByValue("Reverse").Selected = True Then
                    command.Parameters.AddWithValue("@x2412Reverse", 1)
                Else
                    command.Parameters.AddWithValue("@x2412Reverse", 0)
                End If

                '11
                If chkContractList.Items.FindByValue("Process").Selected = True Then
                    command.Parameters.AddWithValue("@x2412Process", 1)
                Else
                    command.Parameters.AddWithValue("@x2412Process", 0)
                End If

                '12
                If chkContractList.Items.FindByValue("EarlyComplete").Selected = True Then
                    command.Parameters.AddWithValue("@x2412Early", 1)
                Else
                    command.Parameters.AddWithValue("@x2412Early", 0)
                End If

                '13
                If chkContractList.Items.FindByValue("TerminationByCust").Selected = True Then
                    command.Parameters.AddWithValue("@x2412Term", 1)
                Else
                    command.Parameters.AddWithValue("@x2412Term", 0)
                End If

                '14
                If chkContractList.Items.FindByValue("CancelByCust").Selected = True Then
                    command.Parameters.AddWithValue("@x2412Cancel", 1)
                Else
                    command.Parameters.AddWithValue("@x2412Cancel", 0)
                End If

                '15
                If chkContractList.Items.FindByValue("RenewalStatus").Selected = True Then
                    command.Parameters.AddWithValue("@x2412Renewal", 1)
                Else
                    command.Parameters.AddWithValue("@x2412Renewal", 0)
                End If






                '16
                If chkServiceList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x2413", 1)
                Else
                    command.Parameters.AddWithValue("@x2413", 0)
                End If

                '17
                If chkServiceList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x2413Add", 1)
                Else
                    command.Parameters.AddWithValue("@x2413Add", 0)
                End If

                '18
                If chkServiceList.Items.FindByValue("Copy").Selected = True Then
                    command.Parameters.AddWithValue("@x2413Copy", 1)
                Else
                    command.Parameters.AddWithValue("@x2413Copy", 0)
                End If

                '19
                If chkServiceList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x2413Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x2413Edit", 0)
                End If

                '20
                If chkServiceList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x2413Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x2413Delete", 0)
                End If

                '21
                If chkServiceList.Items.FindByValue("Print").Selected = True Then
                    command.Parameters.AddWithValue("@x2413Print", 1)
                Else
                    command.Parameters.AddWithValue("@x2413Print", 0)
                End If

                '22
                If chkServiceList.Items.FindByValue("ChangeStatus").Selected = True Then
                    command.Parameters.AddWithValue("@x2413ChSt", 1)
                Else
                    command.Parameters.AddWithValue("@x2413ChSt", 0)
                End If

                '23
                If chkServiceList.Items.FindByValue("Update").Selected = True Then
                    command.Parameters.AddWithValue("@x2413Update", 1)
                Else
                    command.Parameters.AddWithValue("@x2413Update", 0)
                End If

                '24
                If chkServiceList.Items.FindByValue("Reverse").Selected = True Then
                    command.Parameters.AddWithValue("@x2413Reverse", 1)
                Else
                    command.Parameters.AddWithValue("@x2413Reverse", 0)
                End If

                '25
                If chkServiceList.Items.FindByValue("ViewAll").Selected = True Then
                    command.Parameters.AddWithValue("@x2413ViewAll", 1)
                Else
                    command.Parameters.AddWithValue("@x2413ViewAll", 0)
                End If

                '26
                If chkServiceList.Items.FindByValue("ReCalculate").Selected = True Then
                    command.Parameters.AddWithValue("@x2413Recalculate", 1)
                Else
                    command.Parameters.AddWithValue("@x2413Recalculate", 0)
                End If


                '27
                If chkContractList.Items.FindByValue("EditOurRef").Selected = True Then
                    command.Parameters.AddWithValue("@x2412EditOurRef", 1)
                Else
                    command.Parameters.AddWithValue("@x2412EditOurRef", 0)
                End If

                '28
                If chkContractList.Items.FindByValue("EditManualContractNo").Selected = True Then
                    command.Parameters.AddWithValue("@x2412EditManualContractNo", 1)
                Else
                    command.Parameters.AddWithValue("@x2412EditManualContractNo", 0)
                End If

                '29
                If chkContractList.Items.FindByValue("EditPONo").Selected = True Then
                    command.Parameters.AddWithValue("@x2412EditPoNo", 1)
                Else
                    command.Parameters.AddWithValue("@x2412EditPoNo", 0)
                End If

                '30
                If chkContractList.Items.FindByValue("EditNotes").Selected = True Then
                    command.Parameters.AddWithValue("@x2412EditNotes", 1)
                Else
                    command.Parameters.AddWithValue("@x2412EditNotes", 0)
                End If

                '31
                If chkContractList.Items.FindByValue("SOR").Selected = True Then
                    command.Parameters.AddWithValue("@x2412SORAccess", 1)
                Else
                    command.Parameters.AddWithValue("@x2412SORAccess", 0)
                End If

                '32
                If chkContractList.Items.FindByValue("SORAdd").Selected = True Then
                    command.Parameters.AddWithValue("@x2412SORAdd", 1)
                Else
                    command.Parameters.AddWithValue("@x2412SORAdd", 0)
                End If

                '33
                If chkContractList.Items.FindByValue("SOREdit").Selected = True Then
                    command.Parameters.AddWithValue("@x2412SOREdit", 1)
                Else
                    command.Parameters.AddWithValue("@x2412SOREdit", 0)
                End If

                '34
                If chkContractList.Items.FindByValue("SORDelete").Selected = True Then
                    command.Parameters.AddWithValue("@x2412SORDelete", 1)
                Else
                    command.Parameters.AddWithValue("@x2412SORDelete", 0)
                End If


                '35
                If chkContractList.Items.FindByValue("AddBackdateContractIndefinite").Selected = True Then
                    command.Parameters.AddWithValue("@x2412BackDateContract", 1)
                Else
                    command.Parameters.AddWithValue("@x2412BackDateContract", 0)
                End If

                '36
                If chkContractList.Items.FindByValue("TerminationBackdate").Selected = True Then
                    command.Parameters.AddWithValue("@x2412BackDateContractTermination", 1)
                Else
                    command.Parameters.AddWithValue("@x2412BackDateContractTermination", 0)
                End If



                If chkContractList.Items.FindByValue("AddBackdateContractSameMonthOnly").Selected = True Then
                    command.Parameters.AddWithValue("@x2412BackDateContractSameMonthOnly", 1)
                Else
                    command.Parameters.AddWithValue("@x2412BackDateContractSameMonthOnly", 0)
                End If

                '36
                If chkContractList.Items.FindByValue("TerminationSameMonthOnly").Selected = True Then
                    command.Parameters.AddWithValue("@x2412BackDateContractTerminationSameMonthOnly", 1)
                Else
                    command.Parameters.AddWithValue("@x2412BackDateContractTerminationSameMonthOnly", 0)
                End If


                If chkContractList.Items.FindByValue("TerminationFutureDate").Selected = True Then
                    command.Parameters.AddWithValue("@x2412FutureDateContractTermination", 1)
                Else
                    command.Parameters.AddWithValue("@x2412FutureDateContractTermination", 0)
                End If

                '37
                If chkContractList.Items.FindByValue("FileAccess").Selected = True Then
                    command.Parameters.AddWithValue("@x2412FileAccess", 1)
                Else
                    command.Parameters.AddWithValue("@x2412FileAccess", 0)
                End If

                '38
                If chkContractList.Items.FindByValue("FileUpload").Selected = True Then
                    command.Parameters.AddWithValue("@x2412FileUpload", 1)
                Else
                    command.Parameters.AddWithValue("@x2412FileUpload", 0)
                End If

                '39
                If chkContractList.Items.FindByValue("FileDelete").Selected = True Then
                    command.Parameters.AddWithValue("@x2412FileDelete", 1)
                Else
                    command.Parameters.AddWithValue("@x2412FileDelete", 0)
                End If


                If chkContractList.Items.FindByValue("EditAutoRenewal").Selected = True Then
                    command.Parameters.AddWithValue("@x2412EditAutoRenewal", 1)
                Else
                    command.Parameters.AddWithValue("@x2412EditAutoRenewal", 0)
                End If


                '40
                If chkServiceList.Items.FindByValue("FileAccess").Selected = True Then
                    command.Parameters.AddWithValue("@x2413FileAccess", 1)
                Else
                    command.Parameters.AddWithValue("@x2413FileAccess", 0)
                End If

                '41
                If chkServiceList.Items.FindByValue("FileUpload").Selected = True Then
                    command.Parameters.AddWithValue("@x2413FileUpload", 1)
                Else
                    command.Parameters.AddWithValue("@x2413FileUpload", 0)
                End If

                '42
                If chkServiceList.Items.FindByValue("FileDelete").Selected = True Then
                    command.Parameters.AddWithValue("@x2413FileDelete", 1)
                Else
                    command.Parameters.AddWithValue("@x2413FileDelete", 0)
                End If


                '43
                If chkServiceList.Items.FindByValue("PestCount").Selected = True Then
                    command.Parameters.AddWithValue("@x2413PestCount", 1)
                Else
                    command.Parameters.AddWithValue("@x2413PestCount", 0)
                End If

                '44
                If chkContractList.Items.FindByValue("EditBillingFrequency").Selected = True Then
                    command.Parameters.AddWithValue("@x2412BillingFrequency", 1)
                Else
                    command.Parameters.AddWithValue("@x2412BillingFrequency", 0)
                End If


                '45

                If chkContractList.Items.FindByValue("RegenerateSchedule").Selected = True Then
                    command.Parameters.AddWithValue("@x2412RegenerateSchedule", 1)
                Else
                    command.Parameters.AddWithValue("@x2412RegenerateSchedule", 0)
                End If
                'If dt.Rows(0)("x2412RegenerateSchedule").ToString = "1" Then
                '    chkContractList.Items.FindByValue("RegenerateSchedule").Selected = True
                'Else
                '    chkContractList.Items.FindByValue("RegenerateSchedule").Selected = False
                'End If

                'If chkServiceList.Items.FindByValue("EditContractGroup").Selected = True Then
                '    command.Parameters.AddWithValue("@x2413EditContractGroup", 1)
                'Else
                '    command.Parameters.AddWithValue("@x2413EditContractGroup", 0)
                'End If


                '46

                If chkContractList.Items.FindByValue("ExtendEndDate").Selected = True Then
                    command.Parameters.AddWithValue("@x2412ExtendContractEndDate", 1)
                Else
                    command.Parameters.AddWithValue("@x2412ExtendContractEndDate", 0)
                End If


                If chkContractList.Items.FindByValue("Distribution").Selected = True Then
                    command.Parameters.AddWithValue("@x2412Distribution", 1)
                Else
                    command.Parameters.AddWithValue("@x2412Distribution", 0)
                End If

                If chkContractList.Items.FindByValue("VoidContract").Selected = True Then
                    command.Parameters.AddWithValue("@x2412VoidContract", 1)
                Else
                    command.Parameters.AddWithValue("@x2412VoidContract", 0)
                End If

                If chkContractList.Items.FindByValue("WarrantyDates").Selected = True Then
                    command.Parameters.AddWithValue("@x2412WarrantyDates", 1)
                Else
                    command.Parameters.AddWithValue("@x2412WarrantyDates", 0)
                End If

                If chkServiceList.Items.FindByValue("ManualContratPOWONo").Selected = True Then
                    command.Parameters.AddWithValue("@x2413ManualContractPOWONo", 1)
                Else
                    command.Parameters.AddWithValue("@x2413ManualContractPOWONo", 0)
                End If

                'If dt.Rows(0)("x2412Distribution").ToString = "1" Then
                '    chkContractList.Items.FindByValue("x2412Distribution").Selected = True
                'Else
                '    chkContractList.Items.FindByValue("Distribution").Selected = False
                'End If
                'command.Connection = conn

                'command.ExecuteNonQuery()

                'Dim command3 As MySqlCommand = New MySqlCommand
                'command3.CommandType = CommandType.Text
                'Dim qry3 As String = "update tblGroupAccess set x2413ApprMode=@x2413ApprMode,x2413AssignServiceRecord=@x2413AssignServiceRecord, x2412EditAgreeValue =@x2412EditAgreeValue where rcno=" & Convert.ToInt32(txtRcno.Text)

                'command3.CommandText = qry3
                'command3.Parameters.Clear()
                'command3.Parameters.AddWithValue("@x2413ApprMode", 0)

                '30
                command.Parameters.AddWithValue("@x2413ApprMode", 0)

                'If chkServiceList.Items.FindByValue("ApprMode").Selected = True Then
                '    command.Parameters.AddWithValue("@x2413ApprMode", 1)
                'Else
                '    command.Parameters.AddWithValue("@x2413ApprMode", 0)
                'End If

                '31
                If chkServiceList.Items.FindByValue("AssignServiceRecord").Selected = True Then
                    command.Parameters.AddWithValue("@x2413AssignServiceRecord", 1)
                Else
                    command.Parameters.AddWithValue("@x2413AssignServiceRecord", 0)
                End If


                '32
                If chkContractList.Items.FindByValue("EditAgreeValue").Selected = True Then
                    command.Parameters.AddWithValue("@x2412EditAgreeValue", 1)
                Else
                    command.Parameters.AddWithValue("@x2412EditAgreeValue", 0)
                End If

                '33
                If chkServiceList.Items.FindByValue("ExportToExcel").Selected = True Then
                    command.Parameters.AddWithValue("@x2413ExportToExcel", 1)
                Else
                    command.Parameters.AddWithValue("@x2413ExportToExcel", 0)
                End If

                If chkServiceList.Items.FindByValue("VoidService").Selected = True Then
                    command.Parameters.AddWithValue("@x2413VoidService", 1)
                Else
                    command.Parameters.AddWithValue("@x2413VoidService", 0)
                End If


                If chkContractList.Items.FindByValue("Revision").Selected = True Then
                    command.Parameters.AddWithValue("@x2412Revision", 1)
                Else
                    command.Parameters.AddWithValue("@x2412Revision", 0)
                End If

                If chkServiceList.Items.FindByValue("EditBilledAmtBillNo").Selected = True Then
                    command.Parameters.AddWithValue("@x2413EditBilledAmtBillNo", 1)
                Else
                    command.Parameters.AddWithValue("@x2413EditBilledAmtBillNo", 0)
                End If

                If chkContractList.Items.FindByValue("AgreementTypeContractCode").Selected = True Then
                    command.Parameters.AddWithValue("@x2412AgreementTypeContractCode", 1)
                Else
                    command.Parameters.AddWithValue("@x2412AgreementTypeContractCode", 0)
                End If

                'If chkContractList.Items.FindByValue("EditOurRef").Selected = True Then
                '    command.Parameters.AddWithValue("@x2412EditOurRef", 1)
                'Else
                '    command.Parameters.AddWithValue("@x2412EditOurRef", 0)
                'End If


                'If chkContractList.Items.FindByValue("EditManualContractNo").Selected = True Then
                '    command.Parameters.AddWithValue("@x2412EditManualContractNo", 1)
                'Else
                '    command.Parameters.AddWithValue("@x2412EditManualContractNo", 0)
                'End If

                'If chkContractList.Items.FindByValue("EditPONo").Selected = True Then
                '    command.Parameters.AddWithValue("@x2412EditPONo", 1)
                'Else
                '    command.Parameters.AddWithValue("@x2412EditPONo", 0)
                'End If

                'command3.Connection = conn
                'command3.ExecuteNonQuery()

                command.Connection = conn

                command.ExecuteNonQuery()
                lblMessage.Text = "EDIT: SERVICE AND CONTRACT ACCESS SUCCESSFULLY UPDATED"
            End If
        End If

        conn.Close()

        'Catch ex As Exception
        '    MessageBox.Message.Alert(Page, ex.ToString, "str")
        'End Try
        EnableSvcAccessControls()
        txtModeSvcAccess.Text = ""
    End Sub

    Protected Sub tb1_ActiveTabChanged(sender As Object, e As EventArgs) Handles tb1.ActiveTabChanged
        '     lblAlert.Text = tb1.ActiveTabIndex.ToString

        lblAlert.Text = ""

        ' If tb1.ActiveTabIndex = 1 Or tb1.ActiveTabIndex = 2 Or tb1.ActiveTabIndex = 3 Or tb1.ActiveTabIndex = 4 Or tb1.ActiveTabIndex = 5 Then
        If tb1.ActiveTabIndex <> 0 Then
            If txtMode.Text = "ADD" Or txtMode.Text = "EDIT" Then
                lblAlert.Text = "Cannot switch tabs in Add or Edit Mode!! Save or Cancel the record to proceed!!"
                tb1.ActiveTabIndex = 0
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)

            End If

            If String.IsNullOrEmpty(txtGroupAuthority.Text) Then
                MessageBox.Message.Alert(Page, "Select a Group Authority to proceed", "str")
                tb1.ActiveTabIndex = 0
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)

            End If
        End If

        ''  If tb1.ActiveTabIndex = 0 Or tb1.ActiveTabIndex = 1 Or tb1.ActiveTabIndex = 3 Or tb1.ActiveTabIndex = 4 Or tb1.ActiveTabIndex = 5 Then
        'If tb1.ActiveTabIndex <> 2 Then
        '    If txtModeSvcAccess.Text = "Edit" Then
        '        lblAlert.Text = "Cannot switch tabs in Add or Edit Mode!! Save or Cancel the record to proceed!!"
        '        tb1.ActiveTabIndex = 2
        '    End If

        'End If

        ' If tb1.ActiveTabIndex = 0 Or tb1.ActiveTabIndex = 1 Or tb1.ActiveTabIndex = 3 Or tb1.ActiveTabIndex = 4 Or tb1.ActiveTabIndex = 5 Or tb1.ActiveTabIndex = 6 Then
        If tb1.ActiveTabIndex <> 2 Then
            If txtModeSetupAccess.Text = "EDIT" Then
                lblAlert.Text = "Cannot switch tabs in Add or Edit Mode!! Save or Cancel the record to proceed!!"
                tb1.ActiveTabIndex = 2
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)

            End If

        End If
        '   If tb1.ActiveTabIndex = 0 Or tb1.ActiveTabIndex = 1 Or tb1.ActiveTabIndex = 2 Or tb1.ActiveTabIndex = 3 Or tb1.ActiveTabIndex = 5 Then
        If tb1.ActiveTabIndex <> 3 Then
            If txtModeContactAccess.Text = "EDIT" Then
                lblAlert.Text = "Cannot switch tabs in Add or Edit Mode!! Save or Cancel the record to proceed!!"
                tb1.ActiveTabIndex = 3
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)

            End If

        End If
        '  If tb1.ActiveTabIndex = 0 Or tb1.ActiveTabIndex = 1 Or tb1.ActiveTabIndex = 2 Or tb1.ActiveTabIndex = 3 Or tb1.ActiveTabIndex = 4 Then

        If tb1.ActiveTabIndex <> 4 Then
            If txtModeToolsAccess.Text = "EDIT" Then
                lblAlert.Text = "Cannot switch tabs in Add or Edit Mode!! Save or Cancel the record to proceed!!"
                tb1.ActiveTabIndex = 4
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)

            End If

        End If


        If tb1.ActiveTabIndex <> 5 Then
            If txtModeAssetAccess.Text = "EDIT" Then
                lblAlert.Text = "Cannot switch tabs in Add or Edit Mode!! Save or Cancel the record to proceed!!"
                tb1.ActiveTabIndex = 5
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)

            End If

        End If


        '  If tb1.ActiveTabIndex = 0 Or tb1.ActiveTabIndex = 1 Or tb1.ActiveTabIndex = 2 Or tb1.ActiveTabIndex = 3 Or tb1.ActiveTabIndex = 4 Then
        If tb1.ActiveTabIndex <> 6 Then
            If txtModeSvcAccess.Text = "EDIT" Then
                lblAlert.Text = "Cannot switch tabs in Add or Edit Mode!! Save or Cancel the record to proceed!!"
                tb1.ActiveTabIndex = 6
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)

            End If
        End If

        '  If tb1.ActiveTabIndex = 0 Or tb1.ActiveTabIndex = 1 Or tb1.ActiveTabIndex = 2 Or tb1.ActiveTabIndex = 3 Or tb1.ActiveTabIndex = 4 Then
        If tb1.ActiveTabIndex <> 7 Then
            If txtModeARAccess.Text = "EDIT" Then
                lblAlert.Text = "Cannot switch tabs in Add or Edit Mode!! Save or Cancel the record to proceed!!"
                tb1.ActiveTabIndex = 7
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)

            End If

        End If



        If tb1.ActiveTabIndex <> 8 Then
            If txtModeReportAccess.Text = "EDIT" Then
                lblAlert.Text = "Cannot switch tabs in Add or Edit Mode!! Save or Cancel the record to proceed!!"
                tb1.ActiveTabIndex = 8
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            End If
        End If

        If tb1.ActiveTabIndex <> 9 Then
            If txtModeLocationAccess.Text = "EDIT" Or txtModeLocationAccess.Text = "NEW" Then
                lblAlert.Text = "Cannot switch tabs in Add or Edit Mode!! Save or Cancel the record to proceed!!"
                tb1.ActiveTabIndex = 9
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            End If
        End If

        If tb1.ActiveTabIndex <> 10 Then
            If txtModeAssetAccess.Text = "EDIT" Then
                lblAlert.Text = "Cannot switch tabs in Add or Edit Mode!! Save or Cancel the record to proceed!!"
                tb1.ActiveTabIndex = 10
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)

            End If

        End If


        If tb1.ActiveTabIndex = 1 Then
            If String.IsNullOrEmpty(lblGroupAuthority.Text) = True Then
                lblAlert.Text = "Please Select Group Access!!"
                tb1.ActiveTabIndex = 0
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            End If
        End If

        If tb1.ActiveTabIndex = 2 Then
            If String.IsNullOrEmpty(lblGroupAuthority3.Text) = True Then
                lblAlert.Text = "Please Select Group Access!!"
                tb1.ActiveTabIndex = 0
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            End If
        End If

        If tb1.ActiveTabIndex = 3 Then
            If String.IsNullOrEmpty(lblGroupAccess.Text) = True Then
                lblAlert.Text = "Please Select Group Access!!"
                tb1.ActiveTabIndex = 0
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            End If
        End If


        If tb1.ActiveTabIndex = 4 Then
            If String.IsNullOrEmpty(lblGroupAuthority7.Text) = True Then
                lblAlert.Text = "Please Select Group Access!!"
                tb1.ActiveTabIndex = 0
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            End If
        End If

        If tb1.ActiveTabIndex = 5 Then
            If String.IsNullOrEmpty(lblGroupAuthority1.Text) = True Then
                lblAlert.Text = "Please Select Group Access!!"
                tb1.ActiveTabIndex = 0
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            End If
        End If

        If tb1.ActiveTabIndex = 6 Then
            If String.IsNullOrEmpty(lblGroupAuthority4.Text) = True Then
                lblAlert.Text = "Please Select Group Access!!"
                tb1.ActiveTabIndex = 0
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            End If
        End If


        If tb1.ActiveTabIndex = 7 Then
            If String.IsNullOrEmpty(lblGroupAuthority5.Text) = True Then
                lblAlert.Text = "Please Select Group Access!!"
                tb1.ActiveTabIndex = 0
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            End If
        End If

        If tb1.ActiveTabIndex = 8 Then
            If String.IsNullOrEmpty(lblGroupAuthority6.Text) = True Then
                lblAlert.Text = "Please Select Group Access!!"
                tb1.ActiveTabIndex = 0
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            End If
        End If


        '   If tb1.ActiveTabIndex = 1 Or tb1.ActiveTabIndex = 2 Or tb1.ActiveTabIndex = 3 Or tb1.ActiveTabIndex = 4 Or tb1.ActiveTabIndex = 5 Then
        If tb1.ActiveTabIndex <> 0 Then
            GridView1.CssClass = "dummybutton"
            btnQuit.CssClass = "dummybutton"

        Else
            GridView1.CssClass = "visiblebutton"
            btnQuit.CssClass = "visiblebutton"

        End If

        '   ViewState("ActiveTabIndex") = tb1.ActiveTabIndex

    End Sub

    Private Sub EnableContactAccessControls()
        btnSaveContactAccess.Enabled = False
        btnSaveContactAccess.ForeColor = System.Drawing.Color.Gray
        btnCancelContactAccess.Enabled = False
        btnCancelContactAccess.ForeColor = System.Drawing.Color.Gray

        btnEditContactAccess.Enabled = True
        btnEditContactAccess.ForeColor = System.Drawing.Color.Black

        chkCompanyList.Enabled = False
        chkPersonList.Enabled = False
        'chkUserStaffList.Enabled = False
        'chkReportsList.Enabled = False

        chkContactSelectAll.Enabled = False

        txtModeContactAccess.Text = ""
    End Sub

    Private Sub DisableContactAccessControls()
        btnSaveContactAccess.Enabled = True
        btnSaveContactAccess.ForeColor = System.Drawing.Color.Black
        btnCancelContactAccess.Enabled = True
        btnCancelContactAccess.ForeColor = System.Drawing.Color.Black

        btnEditContactAccess.Enabled = False
        btnEditContactAccess.ForeColor = System.Drawing.Color.Gray

        chkCompanyList.Enabled = True
        chkPersonList.Enabled = True
        'chkUserStaffList.Enabled = True
        'chkReportsList.Enabled = True

        chkContactSelectAll.Enabled = True

    End Sub

    Protected Sub btnEditContactAccess_Click(sender As Object, e As EventArgs) Handles btnEditContactAccess.Click
        lblAlert.Text = ""
        If txtRcno.Text = "" Then
            ' MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
            lblAlert.Text = "SELECT GROUP AUTHORITY TO EDIT"
            Return

        End If
        DisableContactAccessControls()
        lblMessage.Text = "ACTION: EDIT CONTACT ACCESS RECORDS"
        txtModeContactAccess.Text = "EDIT"
    End Sub

    Protected Sub btnCancelContactAccess_Click(sender As Object, e As EventArgs) Handles btnCancelContactAccess.Click
        lblAlert.Text = ""
        EnableContactAccessControls()

    End Sub

    Protected Sub btnSaveContactAccess_Click(sender As Object, e As EventArgs) Handles btnSaveContactAccess.Click
        lblMessage.Text = ""
        lblAlert.Text = ""
        'If txtRcno.Text = "" Or txtRcNo2.Text = "" Then
        If txtRcno.Text = "" Then
            ' MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
            lblAlert.Text = "SELECT RECORD TO EDIT"
            Return

        End If

        '     Try
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        'Dim ind As String
        'ind = txtIndustry.Text
        'ind = ind.Replace("'", "\\'")

        Dim command2 As MySqlCommand = New MySqlCommand

        command2.CommandType = CommandType.Text

        command2.CommandText = "SELECT * FROM tblGroupAccess where GroupAccess=@GroupAccess and rcno<>" & Convert.ToInt32(txtRcno.Text)
        command2.Parameters.AddWithValue("@GroupAccess", txtGroupAuthority.Text)
        command2.Connection = conn

        Dim dr1 As MySqlDataReader = command2.ExecuteReader()
        Dim dt1 As New DataTable
        dt1.Load(dr1)

        If dt1.Rows.Count > 0 Then

            ' MessageBox.Message.Alert(Page, "Group Authority already exists!!!", "str")
            lblAlert.Text = "Group Authority ALREADY EXISTS"
            txtGroupAuthority.Focus()
            Exit Sub
        Else

            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.CommandText = "SELECT * FROM tblGroupAccess where rcno=" & Convert.ToInt32(txtRcno.Text)
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then

                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                Dim qry As String = "update tblGroupAccess set x0302 = @x0302,x0302Add = @x0302Add,x0302Edit = @x0302Edit,x0302Delete = @x0302Delete,x0302Trans = @x0302Trans,x0303 = @x0303,x0303Add = @x0303Add,x0303Edit = @x0303Edit,x0303Delete = @x0303Delete,x0303Trans = @x0303Trans,x0302EditAcct = @x0302EditAcct,x0302Change = @x0302Change,x0302Notes = @x0302Notes,x0303Notes = @x0303Notes,x0302ViewAll = @x0302ViewAll,x0302EditBilling = @x0302EditBilling,x0303EditBilling = @x0303EditBilling, x0302CompanySpecificLocation = @x0302SpecificLocation,x0303PersonSpecificLocation = @x0303SpecificLocation, x0302EditContractGroup=@x0302EditContractGroup, x0303EditContractGroup=@x0303EditContractGroup, x0302ChangeAccount = @x0302ChangeAccount, x0303ChangeAccount = @x0303ChangeAccount, x0302CompanyUpdateServiceContact=@x0302CompanyUpdateServiceContact, x0303PersonUpdateServiceContact=@x0303PersonUpdateServiceContact  where rcno=" & Convert.ToInt32(txtRcno.Text)

                command.CommandText = qry
                command.Parameters.Clear()

                'Company Access
                If chkCompanyList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0302", 1)
                Else
                    command.Parameters.AddWithValue("@x0302", 0)
                End If
                If chkCompanyList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0302Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0302Add", 0)
                End If
                If chkCompanyList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0302Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0302Edit", 0)
                End If
                If chkCompanyList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0302Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0302Delete", 0)
                End If
                If chkCompanyList.Items.FindByValue("Trans").Selected = True Then
                    command.Parameters.AddWithValue("@x0302Trans", 1)
                Else
                    command.Parameters.AddWithValue("@x0302Trans", 0)
                End If
                If chkCompanyList.Items.FindByValue("EditAccount").Selected = True Then
                    command.Parameters.AddWithValue("@x0302EditAcct", 1)
                Else
                    command.Parameters.AddWithValue("@x0302EditAcct", 0)
                End If
                If chkCompanyList.Items.FindByValue("ChangeStatus").Selected = True Then
                    command.Parameters.AddWithValue("@x0302Change", 1)
                Else
                    command.Parameters.AddWithValue("@x0302Change", 0)
                End If
                If chkCompanyList.Items.FindByValue("Notes").Selected = True Then
                    command.Parameters.AddWithValue("@x0302Notes", 1)
                Else
                    command.Parameters.AddWithValue("@x0302Notes", 0)
                End If

                If chkCompanyList.Items.FindByValue("ViewAll").Selected = True Then
                    command.Parameters.AddWithValue("@x0302ViewAll", 1)
                Else
                    command.Parameters.AddWithValue("@x0302ViewAll", 0)
                End If
                If chkCompanyList.Items.FindByValue("EditBilling").Selected = True Then
                    command.Parameters.AddWithValue("@x0302EditBilling", 1)
                Else
                    command.Parameters.AddWithValue("@x0302EditBilling", 0)
                End If

                If chkCompanyList.Items.FindByValue("SpecificLocation").Selected = True Then
                    command.Parameters.AddWithValue("@x0302SpecificLocation", 1)
                Else
                    command.Parameters.AddWithValue("@x0302SpecificLocation", 0)
                End If

                If chkCompanyList.Items.FindByValue("EditContractGroup").Selected = True Then
                    command.Parameters.AddWithValue("@x0302EditContractGroup", 1)
                Else
                    command.Parameters.AddWithValue("@x0302EditContractGroup", 0)
                End If

                If chkCompanyList.Items.FindByValue("ChangeAccount").Selected = True Then
                    command.Parameters.AddWithValue("@x0302ChangeAccount", 1)
                Else
                    command.Parameters.AddWithValue("@x0302ChangeAccount", 0)
                End If

                If chkCompanyList.Items.FindByValue("UpdateServiceContact").Selected = True Then
                    command.Parameters.AddWithValue("@x0302CompanyUpdateServiceContact", 1)
                Else
                    command.Parameters.AddWithValue("@x0302CompanyUpdateServiceContact", 0)
                End If

                'Person Access
                If chkPersonList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0303", 1)
                Else
                    command.Parameters.AddWithValue("@x0303", 0)
                End If
                If chkPersonList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0303Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0303Add", 0)
                End If
                If chkPersonList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0303Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0303Edit", 0)
                End If
                If chkPersonList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0303Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0303Delete", 0)
                End If
                If chkPersonList.Items.FindByValue("Trans").Selected = True Then
                    command.Parameters.AddWithValue("@x0303Trans", 1)
                Else
                    command.Parameters.AddWithValue("@x0303Trans", 0)
                End If
                If chkPersonList.Items.FindByValue("Notes").Selected = True Then
                    command.Parameters.AddWithValue("@x0303Notes", 1)
                Else
                    command.Parameters.AddWithValue("@x0303Notes", 0)
                End If
                If chkPersonList.Items.FindByValue("EditBilling").Selected = True Then
                    command.Parameters.AddWithValue("@x0303EditBilling", 1)
                Else
                    command.Parameters.AddWithValue("@x0303EditBilling", 0)
                End If

                If chkPersonList.Items.FindByValue("SpecificLocation").Selected = True Then
                    command.Parameters.AddWithValue("@x0303SpecificLocation", 1)
                Else
                    command.Parameters.AddWithValue("@x0303SpecificLocation", 0)
                End If

                If chkPersonList.Items.FindByValue("EditContractGroup").Selected = True Then
                    command.Parameters.AddWithValue("@x0303EditContractGroup", 1)
                Else
                    command.Parameters.AddWithValue("@x0303EditContractGroup", 0)
                End If

                If chkPersonList.Items.FindByValue("ChangeAccount").Selected = True Then
                    command.Parameters.AddWithValue("@x0303ChangeAccount", 1)
                Else
                    command.Parameters.AddWithValue("@x0303ChangeAccount", 0)
                End If


                If chkPersonList.Items.FindByValue("UpdateServiceContact").Selected = True Then
                    command.Parameters.AddWithValue("@x0303PersonUpdateServiceContact", 1)
                Else
                    command.Parameters.AddWithValue("@x0303PersonUpdateServiceContact", 0)
                End If

                command.Connection = conn

                command.ExecuteNonQuery()


                lblMessage.Text = "EDIT: CONTACT ACCESS SUCCESSFULLY UPDATED"
            End If
        End If

        conn.Close()

        'Catch ex As Exception
        '    MessageBox.Message.Alert(Page, ex.ToString, "str")
        'End Try
        EnableContactAccessControls()
        txtModeContactAccess.Text = ""
    End Sub

    Private Sub EnableToolsAccessControls()
        btnSaveToolsAccess.Enabled = False
        btnSaveToolsAccess.ForeColor = System.Drawing.Color.Gray
        btnCancelToolsAccess.Enabled = False
        btnCancelToolsAccess.ForeColor = System.Drawing.Color.Gray

        btnEditToolsAccess.Enabled = True
        btnEditToolsAccess.ForeColor = System.Drawing.Color.Black

        chkToolsList.Enabled = False
        chkFloorPlanList.Enabled = False
        txtModeToolsAccess.Text = ""

    End Sub


    Private Sub DisablebleToolsAccessControls()
        btnSaveToolsAccess.Enabled = True
        btnSaveToolsAccess.ForeColor = System.Drawing.Color.Black
        btnCancelToolsAccess.Enabled = True
        btnCancelToolsAccess.ForeColor = System.Drawing.Color.Black

        btnEditToolsAccess.Enabled = False
        btnEditToolsAccess.ForeColor = System.Drawing.Color.Gray



        chkToolsList.Enabled = True
        chkFloorPlanList.Enabled = True

    End Sub
    Private Sub EnableSetupAccessControls()
        btnSaveSetupAccess.Enabled = False
        btnSaveSetupAccess.ForeColor = System.Drawing.Color.Gray
        btnCancelSetupAccess.Enabled = False
        btnCancelSetupAccess.ForeColor = System.Drawing.Color.Gray

        btnEditSetupAccess.Enabled = True
        btnEditSetupAccess.ForeColor = System.Drawing.Color.Black

        chkUserStaffList.Enabled = False

        chkCompanyGroupList.Enabled = False
        chkCityList.Enabled = False
        chkStateList.Enabled = False
        chkCountryList.Enabled = False

        chkSchTypeList.Enabled = False
        chkTargetList.Enabled = False
        chkSvcFreqList.Enabled = False
        chkInvFreqList.Enabled = False

        chkVehicleList.Enabled = False
        chkTeamList.Enabled = False
        chkLocGroupList.Enabled = False
        chkPostalList.Enabled = False

        chkTermCodeList.Enabled = False
        chkUserAccessList.Enabled = False
        chkContractGroupCategoryList.Enabled = False
        chkDepartmentList.Enabled = False

        chkPostalList.Enabled = False

        chkServiceMasterList.Enabled = False
        chkIndustryList.Enabled = False
        chkEmailSetupList.Enabled = False

        chkHolidayList.Enabled = False
        chkUOMList.Enabled = False
        chkCOAList.Enabled = False
        chkTaxRateList.Enabled = False

        chkBillingCodeList.Enabled = False
        chkMassChangeList.Enabled = False
        chkEventLogList.Enabled = False
        chkMobileList.Enabled = False

        chkCurrencyList.Enabled = False
        chkTermsList.Enabled = False
        chkSetupSelectAll.Enabled = False

        chkMarketSegmentList.Enabled = False
        chkServiceTypeList.Enabled = False
        chkServiceModuleList.Enabled = False

        chkContactsModuleSetup.Enabled = False
        chkLockServiceRecord.Enabled = False
        chkChemicalsList.Enabled = False

        chkBankList.Enabled = False
        chkNotesTemplateList.Enabled = False
        chkSettlementTypeList.Enabled = False
        chkServiceActionList.Enabled = False
        chkPeriodList.Enabled = False
        chkLocationList.Enabled = False
        ChkBatchEmailList.Enabled = False
        txtModeSetupAccess.Text = ""
        txtModeToolsAccess.Text = ""

        chkOpsModuleList.Enabled = False
        chkARModuleList.Enabled = False
        chkSMSSetupList.Enabled = False

        chkLoginLog.Enabled = False
        chkDocumentType.Enabled = False
        chkCustmerPortal.Enabled = False

        chkStockItem.Enabled = False

        chkDeviceType.Enabled = False
        chkDeviceEventThreshold.Enabled = False

        chkPestMaster.Enabled = False
        chkLevelofInfestatation.Enabled = False
        chkPestGender.Enabled = False
        chkPestLifeStage.Enabled = False
        chkPestSpecies.Enabled = False
        chkPestTrapType.Enabled = False
        chkHoldCodeList.Enabled = False
        chkBatchContractPriceChange.Enabled = False
        chkSetupLogDetails.Enabled = False
        chkCompanySetup.Enabled = False
        chkContractCodeList.Enabled = False
    End Sub

    Private Sub DisableSetupAccessControls()
        btnSaveSetupAccess.Enabled = True
        btnSaveSetupAccess.ForeColor = System.Drawing.Color.Black
        btnCancelSetupAccess.Enabled = True
        btnCancelSetupAccess.ForeColor = System.Drawing.Color.Black

        btnEditSetupAccess.Enabled = False
        btnEditSetupAccess.ForeColor = System.Drawing.Color.Gray

        chkUserStaffList.Enabled = True
        chkCompanyGroupList.Enabled = True
        chkCityList.Enabled = True
        chkStateList.Enabled = True
        chkCountryList.Enabled = True

        chkSchTypeList.Enabled = True
        chkTargetList.Enabled = True
        'chkSvcFreqList.Enabled = True
        chkInvFreqList.Enabled = True

        chkVehicleList.Enabled = True
        chkTeamList.Enabled = True
        chkLocGroupList.Enabled = True
        chkPostalList.Enabled = True

        chkTermCodeList.Enabled = True
        chkUserAccessList.Enabled = True
        chkContractGroupCategoryList.Enabled = True
        chkDepartmentList.Enabled = True

        chkPostalList.Enabled = True

        chkServiceMasterList.Enabled = True
        chkIndustryList.Enabled = True
        chkEmailSetupList.Enabled = True

        chkHolidayList.Enabled = True
        chkUOMList.Enabled = True
        chkCOAList.Enabled = True
        chkTaxRateList.Enabled = True

        chkBillingCodeList.Enabled = True
        chkMassChangeList.Enabled = True
        chkEventLogList.Enabled = True
        chkMobileList.Enabled = True

        chkCurrencyList.Enabled = True
        chkTermsList.Enabled = True
        chkSetupSelectAll.Enabled = True

        'block due to fields names not known

        'chkSvcFreqList.Enabled = True
        chkInvFreqList.Enabled = True

        chkTeamList.Enabled = True

        chkTermCodeList.Enabled = True
        chkServiceMasterList.Enabled = True
        chkContractGroupCategoryList.Enabled = True


        chkBillingCodeList.Enabled = True
        chkMassChangeList.Enabled = True
        chkEventLogList.Enabled = True
        chkMobileList.Enabled = True

        chkMarketSegmentList.Enabled = True
        chkServiceTypeList.Enabled = True
        chkServiceModuleList.Enabled = True

        chkContactsModuleSetup.Enabled = True
        chkLockServiceRecord.Enabled = True

        chkChemicalsList.Enabled = True
        chkBankList.Enabled = True
        chkNotesTemplateList.Enabled = True
        chkSettlementTypeList.Enabled = True
        chkServiceActionList.Enabled = True
        chkPeriodList.Enabled = True
        chkLocationList.Enabled = True
        ChkBatchEmailList.Enabled = True

        chkOpsModuleList.Enabled = True
        chkARModuleList.Enabled = True

        chkSMSSetupList.Enabled = True
        chkLoginLog.Enabled = True
        chkDocumentType.Enabled = True
        chkCustmerPortal.Enabled = True
        chkStockItem.Enabled = True

        chkDeviceType.Enabled = True
        chkDeviceEventThreshold.Enabled = True

        chkPestMaster.Enabled = True
        chkLevelofInfestatation.Enabled = True
        chkPestGender.Enabled = True
        chkPestLifeStage.Enabled = True
        chkPestSpecies.Enabled = True
        chkPestTrapType.Enabled = True
        chkHoldCodeList.Enabled = True
        chkBatchContractPriceChange.Enabled = True

        chkSetupLogDetails.Enabled = True
        chkCompanySetup.Enabled = True
        chkContractCodeList.Enabled = True
    End Sub


    Protected Sub btnEditSetupAccess_Click(sender As Object, e As EventArgs) Handles btnEditSetupAccess.Click
        lblAlert.Text = ""
        If txtRcno.Text = "" Then
            ' MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
            lblAlert.Text = "SELECT GROUP AUTHORITY TO EDIT"
            Return

        End If
        DisableSetupAccessControls()
        lblMessage.Text = "ACTION: EDIT SETUP ACCESS RECORDS"
        txtModeSetupAccess.Text = "EDIT"
    End Sub

    Protected Sub btnCancelSetupAccess_Click(sender As Object, e As EventArgs) Handles btnCancelSetupAccess.Click
        lblAlert.Text = ""
        EnableSetupAccessControls()

    End Sub

    Protected Sub btnSaveSetupAccess_Click(sender As Object, e As EventArgs) Handles btnSaveSetupAccess.Click
        lblAlert.Text = ""
        lblMessage.Text = ""
        'If txtRcno.Text = "" Or txtRcNo2.Text = "" Then
        If txtRcno.Text = "" Then
            ' MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
            lblAlert.Text = "SELECT RECORD TO EDIT"
            Return

        End If

        'Try
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        'Dim ind As String
        'ind = txtIndustry.Text
        'ind = ind.Replace("'", "\\'")
        '  lblalert.text = chkUserStaffList.Items.FindByValue("Print").selected.tostring
        '  Exit Sub

        Dim command2 As MySqlCommand = New MySqlCommand

        command2.CommandType = CommandType.Text

        command2.CommandText = "SELECT * FROM tblGroupAccess where GroupAccess=@GroupAccess and rcno<>" & Convert.ToInt32(txtRcno.Text)
        command2.Parameters.AddWithValue("@GroupAccess", txtGroupAuthority.Text)
        command2.Connection = conn

        Dim dr1 As MySqlDataReader = command2.ExecuteReader()
        Dim dt1 As New DataTable
        dt1.Load(dr1)

        If dt1.Rows.Count > 0 Then

            ' MessageBox.Message.Alert(Page, "Group Authority already exists!!!", "str")
            lblAlert.Text = "GROUP AUTHORITY ALREADY EXISTS"
            txtGroupAuthority.Focus()
            Exit Sub
        Else

            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.CommandText = "SELECT * FROM tblGroupAccess where rcno=" & Convert.ToInt32(txtRcno.Text)
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then

                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                Dim qry As String

                qry = "update tblGroupAccess set "
                qry = qry + " x0251 = @x0251, x0251Add  =@x0251Add, x0251Edit=@x0251Edit, x0251Delete=@x0251Delete, "
                qry = qry + " x0209 = @x0209, x0209Add  =@x0209Add,x0209Edit=@x0209Edit,x0209Delete=@x0209Delete, "
                qry = qry + " x0107 = @x0107, x0107Add = @x0107Add,x0107Edit = @x0107Edit,x0107Delete = @x0107Delete,"
                qry = qry + " x0109 = @x0109, x0109Add = @x0109Add,x0109Edit = @x0109Edit,x0109Delete = @x0109Delete,"
                qry = qry + " x0151 = @x0151, x0151Add = @x0151Add,x0151Edit = @x0151Edit,x0151Delete = @x0151Delete,"
                qry = qry + " x0112 = @x0112, x0112Add = @x0112Add,x0112Edit = @x0112Edit,x0112Delete = @x0112Delete,"
                qry = qry + " x0113 = @x0113, x0113Add = @x0113Add,x0113Edit = @x0113Edit,x0113Delete = @x0113Delete,"
                qry = qry + " x0152 = @x0152, x0152Add = @x0152Add,x0152Edit = @x0152Edit,x0152Delete = @x0152Delete,"
                qry = qry + " x0110 = @x0110,x0110Add = @x0110Add,x0110Edit = @x0110Edit,x0110Delete = @x0110Delete,"
                qry = qry + " x0153 = @x0153,x0153Add = @x0153Add,x0153Edit = @x0153Edit,x0153Delete = @x0153Delete,"
                qry = qry + " x0104 = @x0104,x0104Add = @x0104Add,x0104Edit = @x0104Edit,x0104Delete = @x0104Delete, "
                qry = qry + " x0154 = @x0154,x0154Add = @x0154Add,x0154Edit = @x0154Edit,x0154Delete = @x0154Delete,"
                qry = qry + " x1721Access = @x1721Access,x1721Add = @x1721Add,x1721Edit = @x1721Edit,x1721Delete = @x1721Delete,"
                qry = qry + " x0118 = @x0118,x0118Add = @x0118Add,x0118Edit = @x0118Edit,x0118Delete = @x0118Delete,"
                qry = qry + " x0155 = @x0155,x0155Add = @x0155Add,x0155Edit = @x0155Edit,x0155Delete = @x0155Delete,"
                qry = qry + " x0156 = @x0156,x0156Add = @x0156Add,x0156Edit = @x0156Edit,x0156Delete = @x0156Delete,"
                qry = qry + " x0157 = @x0157,x0157Add = @x0157Add,x0157Edit = @x0157Edit,x0157Delete = @x0157Delete,"
                qry = qry + " x0103 = @x0103,x0103Add = @x0103Add,x0103Edit = @x0103Edit,x0103Delete = @x0103Delete, "
                qry = qry + " x0158 = @x0158,x0158Add = @x0158Add,x0158Edit = @x0158Edit,x0158Delete = @x0158Delete,"
                qry = qry + " x0159 = @x0159,x0159Add = @x0159Add,x0159Edit = @x0159Edit,x0159Delete = @x0159Delete,"
                qry = qry + " x0304 = @x0304,x0304Add = @x0304Add,x0304Edit = @x0304Edit,x0304Delete = @x0304Delete, "
                qry = qry + "x0304Print=@x0304Print,x0304ChSt=@x0304ChSt,x0304GroupAuthority=@x0304GroupAuthority,"
                qry = qry + " x0125 = @x0125,x0125Add = @x0125Add,x0125Edit = @x0125Edit,x0125Delete = @x0125Delete,"
                qry = qry + " x0160 = @x0160,x0160Add = @x0160Add,x0160Edit = @x0160Edit,x0160Delete = @x0160Delete,"
                qry = qry + " x0161 = @x0161,x0161Add = @x0161Add,x0161Edit = @x0161Edit,x0161Delete = @x0161Delete,"
                qry = qry + " x0162 = @x0162,x0162Add = @x0162Add,x0162Edit = @x0162Edit,x0162Delete = @x0162Delete,"
                qry = qry + " x0128 = @x0128,x0128Add = @x0128Add,x0128Edit = @x0128Edit,x0128Delete = @x0128Delete,"
                qry = qry + " x0129 = @x0129,x0129Add = @x0129Add,x0129Edit = @x0129Edit,x0129Delete = @x0129Delete,"
                qry = qry + " x0102 = @x0102,  x0102Add=@x0102Add,  x0102Edit=@x0102Edit,  x0102Delete=@x0102Delete,"
                qry = qry + " x0704 = @x0704,x0704Add = @x0704Add,x0704Edit = @x0704Edit,x0704Delete = @x0704Delete, "
                qry = qry + " x2415 = @x2415,x2415Add = @x2415Add,x2415Edit = @x2415Edit,x2415Delete = @x2415Delete, "

                qry = qry + " x0163 = @x0163,x0163Add = @x0163Add,x0163Edit = @x0163Edit,x0163Delete = @x0163Delete,"
                qry = qry + " x0164 = @x0164,x0164Add = @x0164Add,x0164Edit = @x0164Edit,x0164Delete = @x0164Delete,"
                qry = qry + " x0165 = @x0165,x0165Add = @x0165Add,x0165Edit = @x0165Edit,x0165Delete = @x0165Delete,"

                qry = qry + " x0167 = @x0167,x0167Add = @x0167Add,x0167Edit = @x0167Edit,x0167Delete = @x0167Delete,"
                qry = qry + " x0168 = @x0168,x0168Add = @x0168Add,x0168Edit = @x0168Edit,x0168Delete = @x0168Delete,"

                qry = qry + " x0169 = @x0169,x0169Add = @x0169Add,x0169Edit = @x0169Edit,x0169Delete = @x0169Delete,"
                qry = qry + " x0170 = @x0170,x0170Add = @x0170Add,x0170Edit = @x0170Edit,x0170Delete = @x0170Delete,"
                qry = qry + " x0171 = @x0171,x0171Add = @x0171Add,x0171Edit = @x0171Edit,x0171Delete = @x0171Delete,"
                qry = qry + " x0172 = @x0172,x0172Add = @x0172Add,x0172Edit = @x0172Edit,x0172Delete = @x0172Delete,"
                qry = qry + " x0173 = @x0173,x0173Add = @x0173Add,x0173Edit = @x0173Edit,x0173Delete = @x0173Delete,"
                qry = qry + " x0174 = @x0174,x0174_SendEmail = @x0174Add,"
                qry = qry + " x0175 = @x0175,x0175Add = @x0175Add,x0175Edit = @x0175Edit,x0175Delete = @x0175Delete,"
                qry = qry + " x0166 = @x0166,x0166Edit = @x0166Edit,"
                qry = qry + " x0176 = @x0176,x0176Add = @x0176Add,x0176Edit = @x0176Edit,x0176Delete = @x0176Delete,"
                qry = qry + " x0177 = @x0177,x0177Add = @x0177Add,x0177Edit = @x0177Edit,x0177Delete = @x0177Delete,"
                qry = qry + " x0178 = @x0178,x0178Add = @x0178Add,x0178Edit = @x0178Edit,x0178Delete = @x0178Delete,"
                qry = qry + " x0179 = @x0179,x0179Add = @x0179Add,x0179Edit = @x0179Edit,x0179Delete = @x0179Delete,"
                qry = qry + " x0180 = @x0180,x0180Add = @x0180Add,x0180Edit = @x0180Edit,x0180Delete = @x0180Delete,"
                qry = qry + " x0181 = @x0181,x0181Add = @x0181Add,x0181Edit = @x0181Edit,x0181Delete = @x0181Delete,"
                qry = qry + " x0182 = @x0182,x0182Add = @x0182Add,x0182Edit = @x0182Edit,x0182Delete = @x0182Delete,"

                qry = qry + " x0183DeviceType = @x0183DeviceType,"
                qry = qry + " x0184deviceEventThreshold = @x0184deviceEventThreshold, "

                qry = qry + " x0185 = @x0185,x0185Add = @x0185Add,x0185Edit = @x0185Edit,x0185Delete = @x0185Delete,"
                qry = qry + " x0186 = @x0186,x0186Add = @x0186Add,x0186Edit = @x0186Edit,x0186Delete = @x0186Delete,"
                qry = qry + " x0187 = @x0187,x0187Add = @x0187Add,x0187Edit = @x0187Edit,x0187Delete = @x0187Delete,"
                qry = qry + " x0188 = @x0188,x0188Add = @x0188Add,x0188Edit = @x0188Edit,x0188Delete = @x0188Delete,"
                qry = qry + " x0189 = @x0189,x0189Add = @x0189Add,x0189Edit = @x0189Edit,x0189Delete = @x0189Delete,"
                qry = qry + " x0190 = @x0190,x0190Add = @x0190Add,x0190Edit = @x0190Edit,x0190Delete = @x0190Delete,"

                qry = qry + " x0191 = @x0191,x0191Add = @x0191Add,x0191Edit = @x0191Edit,x0191Delete = @x0191Delete,"
                qry = qry + " x0192 = @x0192, x0193SetupLogDetails = @x0193SetupLogDetails,"
                qry = qry + " x0101 = @x0101,x0101Edit = @x0101Edit, "
                qry = qry + " x0194 = @x0194,x0194Add = @x0194Add,x0194Edit = @x0194Edit,x0194Delete = @x0194Delete"
                qry = qry + "  where rcno = " & Convert.ToInt32(txtRcno.Text)

                command.CommandText = qry
                command.Parameters.Clear()

                'BillingCodes


                If chkBillingCodeList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0251", 1)
                Else
                    command.Parameters.AddWithValue("@x0251", 0)
                End If

                If chkBillingCodeList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0251Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0251Add", 0)
                End If

                If chkBillingCodeList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0251Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0251Edit", 0)
                End If

                If chkBillingCodeList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0251Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0251Delete", 0)
                End If

                'Setup - Chart of Accounts Access
                If chkCOAList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0209", 1)
                Else
                    command.Parameters.AddWithValue("@x0209", 0)
                End If
                If chkCOAList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0209Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0209Add", 0)
                End If
                If chkCOAList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0209Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0209Edit", 0)
                End If
                If chkCOAList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0209Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0209Delete", 0)
                End If

                'Setup - city

                If chkCityList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0107", 1)
                Else
                    command.Parameters.AddWithValue("@x0107", 0)
                End If
                If chkCityList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0107Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0107Add", 0)
                End If
                If chkCityList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0107Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0107Edit", 0)
                End If
                If chkCityList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0107Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0107Delete", 0)
                End If


                'Setup - CompanyGroup

                If chkCompanyGroupList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0109", 1)
                Else
                    command.Parameters.AddWithValue("@x0109", 0)
                End If
                If chkCompanyGroupList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0109Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0109Add", 0)
                End If
                If chkCompanyGroupList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0109Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0109Edit", 0)
                End If
                If chkCompanyGroupList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0109Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0109Delete", 0)
                End If

                'Setup - Contrat Group Category

                If chkContractGroupCategoryList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0151", 1)
                Else
                    command.Parameters.AddWithValue("@x0151", 0)
                End If
                If chkContractGroupCategoryList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0151Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0151Add", 0)
                End If
                If chkContractGroupCategoryList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0151Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0151Edit", 0)
                End If
                If chkContractGroupCategoryList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0151Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0151Delete", 0)
                End If


                'Setup - Country

                If chkCountryList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0112", 1)
                Else
                    command.Parameters.AddWithValue("@x0112", 0)
                End If
                If chkCountryList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0112Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0112Add", 0)
                End If
                If chkCountryList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0112Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0112Edit", 0)
                End If
                If chkCountryList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0112Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0112Delete", 0)
                End If

                'Setup - Currency

                If chkCurrencyList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0113", 1)
                Else
                    command.Parameters.AddWithValue("@x0113", 0)
                End If
                If chkCurrencyList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0113Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0113Add", 0)
                End If
                If chkCurrencyList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0113Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0113Edit", 0)
                End If
                If chkCurrencyList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0113Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0113Delete", 0)
                End If

                'Setup - Department

                If chkDepartmentList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0152", 1)
                Else
                    command.Parameters.AddWithValue("@x0152", 0)
                End If

                If chkDepartmentList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0152Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0152Add", 0)
                End If
                If chkDepartmentList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0152Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0152Edit", 0)
                End If
                If chkDepartmentList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0152Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0152Delete", 0)
                End If


                'Setup - EmailSetup

                If chkEmailSetupList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0110", 1)
                Else
                    command.Parameters.AddWithValue("@x0110", 0)
                End If
                If chkEmailSetupList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0110Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0110Add", 0)
                End If
                If chkEmailSetupList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0110Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0110Edit", 0)
                End If
                If chkEmailSetupList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0110Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0110Delete", 0)
                End If


                '10 Setup - Event Log

                If chkEventLogList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0153", 1)
                Else
                    command.Parameters.AddWithValue("@x0153", 0)
                End If
                If chkEventLogList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0153Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0153Add", 0)
                End If
                If chkEventLogList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0153Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0153Edit", 0)
                End If
                If chkEventLogList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0153Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0153Delete", 0)
                End If

                '11 Setup - Industry

                If chkIndustryList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0104", 1)
                Else
                    command.Parameters.AddWithValue("@x0104", 0)
                End If
                If chkIndustryList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0104Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0104Add", 0)
                End If
                If chkIndustryList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0104Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0104Edit", 0)
                End If
                If chkIndustryList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0104Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0104Delete", 0)
                End If


                '12 Setup - Invoice Frequency

                If chkInvFreqList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0154", 1)
                Else
                    command.Parameters.AddWithValue("@x0154", 0)
                End If
                If chkInvFreqList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0154Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0154Add", 0)
                End If
                If chkInvFreqList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0154Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0154Edit", 0)
                End If
                If chkInvFreqList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0154Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0154Delete", 0)
                End If


                '13 Setup - Holiday

                If chkHolidayList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x1721Access", 1)
                Else
                    command.Parameters.AddWithValue("@x1721Access", 0)
                End If
                If chkHolidayList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x1721Add", 1)
                Else
                    command.Parameters.AddWithValue("@x1721Add", 0)
                End If
                If chkHolidayList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x1721Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x1721Edit", 0)
                End If
                If chkHolidayList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x1721Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x1721Delete", 0)
                End If



                '14 Setup - Location Group

                If chkLocGroupList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0118", 1)
                Else
                    command.Parameters.AddWithValue("@x0118", 0)
                End If
                If chkLocGroupList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0118Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0118Add", 0)
                End If
                If chkLocGroupList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0118Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0118Edit", 0)
                End If
                If chkLocGroupList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0118Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0118Delete", 0)
                End If



                '15 Setup - Mass Change

                If chkMassChangeList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0155", 1)
                Else
                    command.Parameters.AddWithValue("@x0155", 0)
                End If


                command.Parameters.AddWithValue("@x0155Add", 0)
                command.Parameters.AddWithValue("@x0155Edit", 0)
                command.Parameters.AddWithValue("@x0155Delete", 0)



                'If chkMassChangeList.Items.FindByValue("Add").Selected = True Then
                '    command.Parameters.AddWithValue("@x0155Add", 1)
                'Else
                '    command.Parameters.AddWithValue("@x0155Add", 0)
                'End If
                'If chkMassChangeList.Items.FindByValue("Edit").Selected = True Then
                '    command.Parameters.AddWithValue("@x0155Edit", 1)
                'Else
                '    command.Parameters.AddWithValue("@x0155Edit", 0)
                'End If
                'If chkMassChangeList.Items.FindByValue("Delete").Selected = True Then
                '    command.Parameters.AddWithValue("@x0155Delete", 1)
                'Else
                '    command.Parameters.AddWithValue("@x0155Delete", 0)
                'End If


                '16 Setup - Mobile Device

                If chkMobileList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0156", 1)
                Else
                    command.Parameters.AddWithValue("@x0156", 0)
                End If
                If chkMobileList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0156Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0156Add", 0)
                End If
                If chkMobileList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0156Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0156Edit", 0)
                End If
                If chkMobileList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0156Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0156Delete", 0)
                End If


                '17 Setup - Invoice Frequency

                If chkPostalList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0157", 1)
                Else
                    command.Parameters.AddWithValue("@x0157", 0)
                End If
                If chkPostalList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0157Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0157Add", 0)
                End If
                If chkPostalList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0157Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0157Edit", 0)
                End If
                If chkPostalList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0157Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0157Delete", 0)
                End If



                '18 Setup - Schedule Type

                If chkSchTypeList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0103", 1)
                Else
                    command.Parameters.AddWithValue("@x0103", 0)
                End If
                If chkSchTypeList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0103Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0103Add", 0)
                End If
                If chkSchTypeList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0103Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0103Edit", 0)
                End If
                If chkSchTypeList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0103Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0103Delete", 0)
                End If

                '19 Setup - Service Frequency

                If chkSvcFreqList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0158", 1)
                Else
                    command.Parameters.AddWithValue("@x0158", 0)
                End If
                If chkSvcFreqList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0158Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0158Add", 0)
                End If
                If chkSvcFreqList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0158Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0158Edit", 0)
                End If
                If chkSvcFreqList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0158Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0158Delete", 0)
                End If

                '20 Setup - Service Master

                If chkServiceMasterList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0159", 1)
                Else
                    command.Parameters.AddWithValue("@x0159", 0)
                End If
                If chkServiceMasterList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0159Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0159Add", 0)
                End If
                If chkServiceMasterList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0159Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0159Edit", 0)
                End If
                If chkServiceMasterList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0159Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0159Delete", 0)
                End If



                '21 UserStaff Access
                If chkUserStaffList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0304", 1)
                Else
                    command.Parameters.AddWithValue("@x0304", 0)
                End If
                If chkUserStaffList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0304Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0304Add", 0)
                End If
                If chkUserStaffList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0304Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0304Edit", 0)
                End If
                If chkUserStaffList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0304Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0304Delete", 0)
                End If
                'If chkUserStaffList.Items.FindByValue("Trans").Selected = True Then
                '    command.Parameters.AddWithValue("@x0304Trans", 1)
                'Else
                '    command.Parameters.AddWithValue("@x0304Trans", 0)
                'End If
                'If chkUserStaffList.Items.FindByValue("Security").Selected = True Then
                '    command.Parameters.AddWithValue("@x0304Security", 1)
                'Else
                '    command.Parameters.AddWithValue("@x0304Security", 0)
                'End If

                'UserStaff Access
                If chkUserStaffList.Items.FindByValue("ChangeStatus").Selected = True Then
                    command.Parameters.AddWithValue("@x0304ChSt", 1)
                Else
                    command.Parameters.AddWithValue("@x0304ChSt", 0)
                End If

                If chkUserStaffList.Items.FindByValue("GroupAccess").Selected = True Then
                    command.Parameters.AddWithValue("@x0304GroupAuthority", 1)
                Else
                    command.Parameters.AddWithValue("@x0304GroupAuthority", 0)
                End If
                If chkUserStaffList.Items.FindByValue("Print").Selected = True Then
                    command.Parameters.AddWithValue("@x0304Print", 1)
                Else
                    command.Parameters.AddWithValue("@x0304Print", 0)
                End If


                '22 Setup - State

                If chkStateList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0125", 1)
                Else
                    command.Parameters.AddWithValue("@x0125", 0)
                End If
                If chkStateList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0125Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0125Add", 0)
                End If
                If chkStateList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0125Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0125Edit", 0)
                End If
                If chkStateList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0125Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0125Delete", 0)
                End If


                '23 Setup - Target

                If chkTargetList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0160", 1)
                Else
                    command.Parameters.AddWithValue("@x0160", 0)
                End If
                If chkTargetList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0160Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0160Add", 0)
                End If
                If chkTargetList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0160Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0160Edit", 0)
                End If
                If chkTargetList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0160Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0160Delete", 0)
                End If

                '24 Setup - TaxRate

                If chkTaxRateList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0128", 1)
                Else
                    command.Parameters.AddWithValue("@x0128", 0)
                End If
                If chkTaxRateList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0128Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0128Add", 0)
                End If
                If chkTaxRateList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0128Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0128Edit", 0)
                End If
                If chkTaxRateList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0128Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0128Delete", 0)
                End If


                '25 Setup - Team

                If chkTeamList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0161", 1)
                Else
                    command.Parameters.AddWithValue("@x0161", 0)
                End If
                If chkTeamList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0161Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0161Add", 0)
                End If
                If chkTeamList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0161Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0161Edit", 0)
                End If
                If chkTeamList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0161Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0161Delete", 0)
                End If


                '26 Setup - Termination Code

                If chkTermCodeList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0162", 1)
                Else
                    command.Parameters.AddWithValue("@x0162", 0)
                End If
                If chkTermCodeList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0162Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0162Add", 0)
                End If
                If chkTermCodeList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0162Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0162Edit", 0)
                End If
                If chkTermCodeList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0162Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0162Delete", 0)
                End If


                '27 Setup - Terms

                If chkTermsList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0102", 1)
                Else
                    command.Parameters.AddWithValue("@x0102", 0)
                End If

                If chkTermsList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0102Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0102Add", 0)
                End If
                If chkTermsList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0102Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0102Edit", 0)
                End If
                If chkTermsList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0102Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0102Delete", 0)
                End If


                '28 Setup - UOM

                If chkUOMList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0129", 1)
                Else
                    command.Parameters.AddWithValue("@x0129", 0)
                End If
                If chkUOMList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0129Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0129Add", 0)
                End If
                If chkUOMList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0129Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0129Edit", 0)
                End If
                If chkUOMList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0129Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0129Delete", 0)
                End If

                '29 Setup - User Access

                If chkUserAccessList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0704", 1)
                Else
                    command.Parameters.AddWithValue("@x0704", 0)
                End If
                If chkUserAccessList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0704Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0704Add", 0)
                End If
                If chkUserAccessList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0704Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0704Edit", 0)
                End If
                If chkUserAccessList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0704Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0704Delete", 0)
                End If

                '30 Setup - Vehicle Access

                If chkVehicleList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x2415", 1)
                Else
                    command.Parameters.AddWithValue("@x2415", 0)
                End If
                If chkVehicleList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x2415Add", 1)
                Else
                    command.Parameters.AddWithValue("@x2415Add", 0)
                End If
                If chkVehicleList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x2415Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x2415Edit", 0)
                End If
                If chkVehicleList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x2415Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x2415Delete", 0)
                End If



                '''''''''''''''''''''''''''''''''''''''''

                '31 Setup - Market Segment

                If chkMarketSegmentList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0163", 1)
                Else
                    command.Parameters.AddWithValue("@x0163", 0)
                End If
                If chkMarketSegmentList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0163Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0163Add", 0)
                End If
                If chkMarketSegmentList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0163Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0163Edit", 0)
                End If
                If chkMarketSegmentList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0163Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0163Delete", 0)
                End If


                '32 Setup - Service Type

                If chkServiceTypeList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0164", 1)
                Else
                    command.Parameters.AddWithValue("@x0164", 0)
                End If
                If chkServiceTypeList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0164Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0164Add", 0)
                End If
                If chkServiceTypeList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0164Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0164Edit", 0)
                End If
                If chkServiceTypeList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0164Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0164Delete", 0)
                End If



                '33 Setup - Service Module Setup - NOT USED

                If chkServiceModuleList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0165", 1)
                Else
                    command.Parameters.AddWithValue("@x0165", 0)
                End If
                If chkServiceModuleList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0165Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0165Add", 0)
                End If
                If chkServiceModuleList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0165Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0165Edit", 0)
                End If
                If chkServiceModuleList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0165Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0165Delete", 0)
                End If


                '34 Setup - Contacts Module Setup

                If chkContactsModuleSetup.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0166", 1)
                Else
                    command.Parameters.AddWithValue("@x0166", 0)
                End If

                If chkContactsModuleSetup.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0166Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0166Edit", 0)
                End If


                '35 Setup - Lock Service Record

                If chkLockServiceRecord.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0167", 1)
                Else
                    command.Parameters.AddWithValue("@x0167", 0)
                End If
                If chkLockServiceRecord.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0167Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0167Add", 0)
                End If
                If chkLockServiceRecord.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0167Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0167Edit", 0)
                End If
                If chkLockServiceRecord.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0167Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0167Delete", 0)
                End If


                '36 Setup - Chemicals Setup

                If chkChemicalsList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0168", 1)
                Else
                    command.Parameters.AddWithValue("@x0168", 0)
                End If
                If chkChemicalsList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0168Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0168Add", 0)
                End If
                If chkChemicalsList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0168Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0168Edit", 0)
                End If
                If chkChemicalsList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0168Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0168Delete", 0)
                End If



                '37 Setup - Chemicals Setup

                If chkBankList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0169", 1)
                Else
                    command.Parameters.AddWithValue("@x0169", 0)
                End If
                If chkBankList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0169Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0169Add", 0)
                End If
                If chkBankList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0169Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0169Edit", 0)
                End If
                If chkBankList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0169Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0169Delete", 0)
                End If


                '38 Setup - Notes Template

                If chkNotesTemplateList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0170", 1)
                Else
                    command.Parameters.AddWithValue("@x0170", 0)
                End If
                If chkNotesTemplateList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0170Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0170Add", 0)
                End If
                If chkNotesTemplateList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0170Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0170Edit", 0)
                End If
                If chkNotesTemplateList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0170Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0170Delete", 0)
                End If
                ''''''''''''''''''''''''''''''''''''''''''''
                ''''''''''''''''''''''''''''''''''''
                '39 Setup - Settlement Type

                If chkSettlementTypeList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0171", 1)
                Else
                    command.Parameters.AddWithValue("@x0171", 0)
                End If
                If chkSettlementTypeList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0171Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0171Add", 0)
                End If
                If chkSettlementTypeList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0171Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0171Edit", 0)
                End If
                If chkSettlementTypeList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0171Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0171Delete", 0)
                End If
                ''''''''''''''''''''''''''''''''''''''''''''
                ''''''''''''''''''''''''''''''''''''

                '40 Setup - Service Action

                If chkServiceActionList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0172", 1)
                Else
                    command.Parameters.AddWithValue("@x0172", 0)
                End If
                If chkServiceActionList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0172Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0172Add", 0)
                End If
                If chkServiceActionList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0172Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0172Edit", 0)
                End If
                If chkServiceActionList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0172Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0172Delete", 0)
                End If


                '41 Setup - Period

                If chkPeriodList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0173", 1)
                Else
                    command.Parameters.AddWithValue("@x0173", 0)
                End If
                If chkPeriodList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0173Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0173Add", 0)
                End If
                If chkPeriodList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0173Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0173Edit", 0)
                End If
                If chkPeriodList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0173Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0173Delete", 0)
                End If


                '42 Setup - Batch Email

                If ChkBatchEmailList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0174", 1)
                Else
                    command.Parameters.AddWithValue("@x0174", 0)
                End If
                If ChkBatchEmailList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0174Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0174Add", 0)
                End If


                '43 Setup - Location

                If chkLocationList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0175", 1)
                Else
                    command.Parameters.AddWithValue("@x0175", 0)
                End If
                If chkLocationList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0175Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0175Add", 0)
                End If
                If chkLocationList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0175Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0175Edit", 0)
                End If
                If chkLocationList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0175Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0175Delete", 0)
                End If


                '44 Setup - Setup-OPS

                If chkOpsModuleList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0176", 1)
                Else
                    command.Parameters.AddWithValue("@x0176", 0)
                End If
                If chkOpsModuleList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0176Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0176Add", 0)
                End If
                If chkOpsModuleList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0176Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0176Edit", 0)
                End If
                If chkOpsModuleList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0176Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0176Delete", 0)
                End If



                '45 Setup - Setup-AR Module

                If chkARModuleList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0177", 1)
                Else
                    command.Parameters.AddWithValue("@x0177", 0)
                End If
                If chkARModuleList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0177Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0177Add", 0)
                End If
                If chkARModuleList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0177Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0177Edit", 0)
                End If
                If chkARModuleList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0177Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0177Delete", 0)
                End If


                '46 Setup - SMS Setup Module

                If chkSMSSetupList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0178", 1)
                Else
                    command.Parameters.AddWithValue("@x0178", 0)
                End If
                If chkSMSSetupList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0178Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0178Add", 0)
                End If
                If chkSMSSetupList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0178Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0178Edit", 0)
                End If
                If chkSMSSetupList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0178Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0178Delete", 0)
                End If


                '47 Setup - Login Log Module

                If chkLoginLog.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0179", 1)
                Else
                    command.Parameters.AddWithValue("@x0179", 0)
                End If
                If chkLoginLog.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0179Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0179Add", 0)
                End If
                If chkLoginLog.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0179Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0179Edit", 0)
                End If
                If chkLoginLog.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0179Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0179Delete", 0)
                End If

                '48 Setup - Document Type Module

                If chkDocumentType.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0180", 1)
                Else
                    command.Parameters.AddWithValue("@x0180", 0)
                End If
                If chkDocumentType.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0180Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0180Add", 0)
                End If
                If chkDocumentType.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0180Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0180Edit", 0)
                End If
                If chkDocumentType.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0180Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0180Delete", 0)
                End If


                '49 Setup - Customer Portal

                If chkCustmerPortal.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0181", 1)
                Else
                    command.Parameters.AddWithValue("@x0181", 0)
                End If
                If chkCustmerPortal.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0181Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0181Add", 0)
                End If
                If chkCustmerPortal.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0181Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0181Edit", 0)
                End If
                If chkCustmerPortal.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0181Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0181Delete", 0)
                End If


                '50 Setup - Stock Item

                If chkStockItem.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0182", 1)
                Else
                    command.Parameters.AddWithValue("@x0182", 0)
                End If
                If chkStockItem.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0182Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0182Add", 0)
                End If
                If chkStockItem.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0182Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0182Edit", 0)
                End If
                If chkStockItem.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0182Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0182Delete", 0)
                End If



                '51 Device Type


                If chkDeviceType.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0183DeviceType", 1)
                Else
                    command.Parameters.AddWithValue("@x0183DeviceType", 0)
                End If

                'If chkStockItem.Items.FindByValue("Add").Selected = True Then
                '    command.Parameters.AddWithValue("@x0182Add", 1)
                'Else
                '    command.Parameters.AddWithValue("@x0182Add", 0)
                'End If
                'If chkStockItem.Items.FindByValue("Edit").Selected = True Then
                '    command.Parameters.AddWithValue("@x0182Edit", 1)
                'Else
                '    command.Parameters.AddWithValue("@x0182Edit", 0)
                'End If
                'If chkStockItem.Items.FindByValue("Delete").Selected = True Then
                '    command.Parameters.AddWithValue("@x0182Delete", 1)
                'Else
                '    command.Parameters.AddWithValue("@x0182Delete", 0)
                'End If




                '52' Device Event Threshold

                '50 Setup - Stock Item

                If chkDeviceEventThreshold.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0184deviceEventThreshold", 1)
                Else
                    command.Parameters.AddWithValue("@x0184deviceEventThreshold", 0)
                End If


                'If chkStockItem.Items.FindByValue("Add").Selected = True Then
                '    command.Parameters.AddWithValue("@x0182Add", 1)
                'Else
                '    command.Parameters.AddWithValue("@x0182Add", 0)
                'End If
                'If chkStockItem.Items.FindByValue("Edit").Selected = True Then
                '    command.Parameters.AddWithValue("@x0182Edit", 1)
                'Else
                '    command.Parameters.AddWithValue("@x0182Edit", 0)
                'End If
                'If chkStockItem.Items.FindByValue("Delete").Selected = True Then
                '    command.Parameters.AddWithValue("@x0182Delete", 1)
                'Else
                '    command.Parameters.AddWithValue("@x0182Delete", 0)
                'End If



                If chkPestMaster.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0185", 1)
                Else
                    command.Parameters.AddWithValue("@x0185", 0)
                End If

                If chkPestMaster.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0185Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0185Add", 0)
                End If

                If chkPestMaster.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0185Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0185Edit", 0)
                End If

                If chkPestMaster.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0185Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0185Delete", 0)
                End If



                If chkLevelofInfestatation.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0186", 1)
                Else
                    command.Parameters.AddWithValue("@x0186", 0)
                End If

                If chkLevelofInfestatation.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0186Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0186Add", 0)
                End If

                If chkLevelofInfestatation.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0186Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0186Edit", 0)
                End If

                If chkLevelofInfestatation.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0186Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0186Delete", 0)
                End If


                If chkPestGender.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0187", 1)
                Else
                    command.Parameters.AddWithValue("@x0187", 0)
                End If

                If chkPestGender.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0187Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0187Add", 0)
                End If

                If chkPestGender.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0187Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0187Edit", 0)
                End If

                If chkPestGender.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0187Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0187Delete", 0)
                End If


                If chkPestLifeStage.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0188", 1)
                Else
                    command.Parameters.AddWithValue("@x0188", 0)
                End If

                If chkPestLifeStage.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0188Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0188Add", 0)
                End If

                If chkPestLifeStage.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0188Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0188Edit", 0)
                End If

                If chkPestLifeStage.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0188Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0188Delete", 0)
                End If



                If chkPestSpecies.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0189", 1)
                Else
                    command.Parameters.AddWithValue("@x0189", 0)
                End If

                If chkPestSpecies.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0189Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0189Add", 0)
                End If

                If chkPestSpecies.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0189Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0189Edit", 0)
                End If

                If chkPestSpecies.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0189Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0189Delete", 0)
                End If



                If chkPestTrapType.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0190", 1)
                Else
                    command.Parameters.AddWithValue("@x0190", 0)
                End If

                If chkPestTrapType.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0190Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0190Add", 0)
                End If

                If chkPestTrapType.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0190Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0190Edit", 0)
                End If

                If chkPestTrapType.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0190Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0190Delete", 0)
                End If


                If chkHoldCodeList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0191", 1)
                Else
                    command.Parameters.AddWithValue("@x0191", 0)
                End If

                If chkHoldCodeList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0191Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0191Add", 0)
                End If

                If chkHoldCodeList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0191Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0191Edit", 0)
                End If

                If chkHoldCodeList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0191Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0191Delete", 0)
                End If


                If chkBatchContractPriceChange.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0192", 1)
                Else
                    command.Parameters.AddWithValue("@x0192", 0)
                End If

                If chkSetupLogDetails.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0193SetupLogDetails", 1)
                Else
                    command.Parameters.AddWithValue("@x0193SetupLogDetails", 0)
                End If



                'Setup - city

                If chkCompanySetup.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0101", 1)
                Else
                    command.Parameters.AddWithValue("@x0101", 0)
                End If


                'If chkCompanySetup.Items.FindByValue("Add").Selected = True Then
                '    command.Parameters.AddWithValue("@x0101Add", 1)
                'Else
                '    command.Parameters.AddWithValue("@x0101Add", 0)
                'End If

                If chkCompanySetup.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0101Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0101Edit", 0)
                End If


                If chkContractCodeList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0194", 1)
                Else
                    command.Parameters.AddWithValue("@x0194", 0)
                End If

                If chkContractCodeList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x0194Add", 1)
                Else
                    command.Parameters.AddWithValue("@x0194Add", 0)
                End If

                If chkContractCodeList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x0194Edit", 1)
                Else
                    command.Parameters.AddWithValue("@x0194Edit", 0)
                End If

                If chkContractCodeList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x0194Delete", 1)
                Else
                    command.Parameters.AddWithValue("@x0194Delete", 0)
                End If

                'If chkCompanySetup.Items.FindByValue("Delete").Selected = True Then
                '    command.Parameters.AddWithValue("@x0101Delete", 1)
                'Else
                '    command.Parameters.AddWithValue("@x0101Delete", 0)
                'End If
                ''''''''''''''''''''''''''''''''''''''''''''

                'setup - Terms Access

                ''Setup - Postal to Location

                'If chkPostalList.Items.FindByValue("Access").Selected = True Then
                '    command3.Parameters.AddWithValue("@x0117", 1)
                'Else
                '    command3.Parameters.AddWithValue("@x0117", 0)
                'End If
                'If chkPostalList.Items.FindByValue("Add").Selected = True Then
                '    command3.Parameters.AddWithValue("@x0117Add", 1)
                'Else
                '    command3.Parameters.AddWithValue("@x0117Add", 0)
                'End If
                'If chkPostalList.Items.FindByValue("Edit").Selected = True Then
                '    command3.Parameters.AddWithValue("@x0117Edit", 1)
                'Else
                '    command3.Parameters.AddWithValue("@x0117Edit", 0)
                'End If
                'If chkPostalList.Items.FindByValue("Delete").Selected = True Then
                '    command3.Parameters.AddWithValue("@x0117Delete", 1)
                'Else
                '    command3.Parameters.AddWithValue("@x0117Delete", 0)
                'End If

                ''Setup - EmailSetup

                'If chkEmailSetupList.Items.FindByValue("Access").Selected = True Then
                '    command3.Parameters.AddWithValue("@x0110", 1)
                'Else
                '    command3.Parameters.AddWithValue("@x0110", 0)
                'End If
                'If chkEmailSetupList.Items.FindByValue("Add").Selected = True Then
                '    command3.Parameters.AddWithValue("@x0110Add", 1)
                'Else
                '    command3.Parameters.AddWithValue("@x0110Add", 0)
                'End If
                'If chkEmailSetupList.Items.FindByValue("Edit").Selected = True Then
                '    command3.Parameters.AddWithValue("@x0110Edit", 1)
                'Else
                '    command3.Parameters.AddWithValue("@x0110Edit", 0)
                'End If
                'If chkEmailSetupList.Items.FindByValue("Delete").Selected = True Then
                '    command3.Parameters.AddWithValue("@x0110Delete", 1)
                'Else
                '    command3.Parameters.AddWithValue("@x0110Delete", 0)
                'End If

                ''Setup - Department

                'If chkDepartmentList.Items.FindByValue("Access").Selected = True Then
                '    command3.Parameters.AddWithValue("@x0114", 1)
                'Else
                '    command3.Parameters.AddWithValue("@x0114", 0)
                'End If
                'If chkDepartmentList.Items.FindByValue("Add").Selected = True Then
                '    command3.Parameters.AddWithValue("@x0114Add", 1)
                'Else
                '    command3.Parameters.AddWithValue("@x0114Add", 0)
                'End If
                'If chkDepartmentList.Items.FindByValue("Edit").Selected = True Then
                '    command3.Parameters.AddWithValue("@x0114Edit", 1)
                'Else
                '    command3.Parameters.AddWithValue("@x0114Edit", 0)
                'End If
                'If chkDepartmentList.Items.FindByValue("Delete").Selected = True Then
                '    command3.Parameters.AddWithValue("@x0114Delete", 1)
                'Else
                '    command3.Parameters.AddWithValue("@x0114Delete", 0)
                'End If

                ''''''''''''''''''''''''''''''''''''''
                command.Connection = conn

                command.ExecuteNonQuery()

                'Dim command3 As MySqlCommand = New MySqlCommand
                'command3.CommandType = CommandType.Text
                'Dim qry3 As String = "update tblGroupAccess set x0304ChSt = @x0304ChSt,x0304Print = @x0304Print,x0304GroupAuthority = @x0304GroupAuthority,   x3201 = @x3201,x3201Add = @x3201Add,x3201Edit = @x3201Edit,x3201Delete = @x3201Delete,  x0117 = @x0117,x0117Add = @x0117Add,x0117Edit = @x0117Edit,x0117Delete = @x0117Delete,  x0114 = @x0114,x0114Add = @x0114Add,x0114Edit = @x0114Edit,x0114Delete = @x0114Delete,  , where rcno=" & Convert.ToInt32(txtRcNo2.Text)

                'command3.CommandText = qry3
                'command3.Parameters.Clear()

                'command3.Connection = conn

                'command3.ExecuteNonQuery()
                '  MessageBox.Message.Alert(Page, "Record updated successfully!!!", "str")
                lblMessage.Text = "EDIT: SETUP ACCESS SUCCESSFULLY UPDATED"
            End If
        End If

        conn.Close()

        'Catch ex As Exception
        '    lblAlert.Text = ex.Message.ToString
        'End Try
        EnableSetupAccessControls()
        txtModeSetupAccess.Text = ""
    End Sub


    Private Sub EnableReportAccessControls()
        btnSaveReportAccess.Enabled = False
        btnSaveReportAccess.ForeColor = System.Drawing.Color.Gray
        btnCancelReportAccess.Enabled = False
        btnCancelReportAccess.ForeColor = System.Drawing.Color.Gray

        btnEditReportAccess.Enabled = True
        btnEditReportAccess.ForeColor = System.Drawing.Color.Black


        chkReportsList.Enabled = False

        chkReportSelectAll.Enabled = False

        txtModeReportAccess.Text = ""
    End Sub

    Private Sub DisableReportAccessControls()
        btnSaveReportAccess.Enabled = True
        btnSaveReportAccess.ForeColor = System.Drawing.Color.Black
        btnCancelReportAccess.Enabled = True
        btnCancelReportAccess.ForeColor = System.Drawing.Color.Black

        btnEditReportAccess.Enabled = False
        btnEditReportAccess.ForeColor = System.Drawing.Color.Gray

        chkReportsList.Enabled = True

        chkReportSelectAll.Enabled = True

    End Sub

    Protected Sub btnEditReportAccess_Click(sender As Object, e As EventArgs) Handles btnEditReportAccess.Click
        lblAlert.Text = ""
        If txtRcno.Text = "" Then
            ' MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
            lblAlert.Text = "SELECT GROUP AUTHORITY TO EDIT"
            Return

        End If
        DisableReportAccessControls()
        lblMessage.Text = "ACTION: EDIT REPORT ACCESS RECORDS"
        txtModeReportAccess.Text = "EDIT"
    End Sub

    Protected Sub btnCancelReportAccess_Click(sender As Object, e As EventArgs) Handles btnCancelReportAccess.Click
        EnableReportAccessControls()

    End Sub

    Protected Sub btnSaveReportAccess_Click(sender As Object, e As EventArgs) Handles btnSaveReportAccess.Click
        lblAlert.Text = ""
        If txtRcno.Text = "" Then
            ' MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
            lblAlert.Text = "SELECT RECORD TO EDIT"
            Return

        End If

        '     Try
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        'Dim ind As String
        'ind = txtIndustry.Text
        'ind = ind.Replace("'", "\\'")

        Dim command2 As MySqlCommand = New MySqlCommand

        command2.CommandType = CommandType.Text

        command2.CommandText = "SELECT * FROM tblGroupAccess where GroupAccess=@GroupAccess and rcno<>" & Convert.ToInt32(txtRcno.Text)
        command2.Parameters.AddWithValue("@GroupAccess", txtGroupAuthority.Text)
        command2.Connection = conn

        Dim dr1 As MySqlDataReader = command2.ExecuteReader()
        Dim dt1 As New DataTable
        dt1.Load(dr1)

        If dt1.Rows.Count > 0 Then

            ' MessageBox.Message.Alert(Page, "Group Authority already exists!!!", "str")
            lblAlert.Text = "GROUP AUTHORITY ALREADY EXISTS"
            txtGroupAuthority.Focus()
            Exit Sub
        Else

            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.CommandText = "SELECT * FROM tblGroupAccess where rcno=" & Convert.ToInt32(txtRcno.Text)
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then

                Dim command3 As MySqlCommand = New MySqlCommand
                command3.CommandType = CommandType.Text
                Dim qry3 As String = "update tblGroupAccess set x2427JobOrder = @x2427JobOrder,x2427Management = @x2427Management,x2427Others = @x2427Others,x2427Revenue = @x2427Revenue,x2427ServiceContract = @x2427ServiceContract,x2427ServiceRecord = @x2427ServiceRecord,x2427Portfolio = @x2427Portfolio, x2427Others = @x2427Others where rcno=" & Convert.ToInt32(txtRcno.Text)

                command3.CommandText = qry3
                command3.Parameters.Clear()


                'Reports Access
                If chkReportsList.Items.FindByValue("ServiceRecord").Selected = True Then
                    command3.Parameters.AddWithValue("@x2427ServiceRecord", 1)
                Else
                    command3.Parameters.AddWithValue("@x2427ServiceRecord", 0)
                End If
                If chkReportsList.Items.FindByValue("ServiceContract").Selected = True Then
                    command3.Parameters.AddWithValue("@x2427ServiceContract", 1)
                Else
                    command3.Parameters.AddWithValue("@x2427ServiceContract", 0)
                End If
                If chkReportsList.Items.FindByValue("Management").Selected = True Then
                    command3.Parameters.AddWithValue("@x2427Management", 1)
                Else
                    command3.Parameters.AddWithValue("@x2427Management", 0)
                End If
                If chkReportsList.Items.FindByValue("Revenue").Selected = True Then
                    command3.Parameters.AddWithValue("@x2427Revenue", 1)
                Else
                    command3.Parameters.AddWithValue("@x2427Revenue", 0)
                End If
                If chkReportsList.Items.FindByValue("Portfolio").Selected = True Then
                    command3.Parameters.AddWithValue("@x2427Portfolio", 1)
                Else
                    command3.Parameters.AddWithValue("@x2427Portfolio", 0)
                End If
                If chkReportsList.Items.FindByValue("JobOrder").Selected = True Then
                    command3.Parameters.AddWithValue("@x2427JobOrder", 1)
                Else
                    command3.Parameters.AddWithValue("@x2427JobOrder", 0)
                End If
                If chkReportsList.Items.FindByValue("ARReports").Selected = True Then
                    command3.Parameters.AddWithValue("@x2427Others", 1)
                Else
                    command3.Parameters.AddWithValue("@x2427Others", 0)
                End If
                command3.Connection = conn

                command3.ExecuteNonQuery()
                '  MessageBox.Message.Alert(Page, "Record updated successfully!!!", "str")
                lblMessage.Text = "EDIT: REPORT ACCESS SUCCESSFULLY UPDATED"
            End If
        End If

        conn.Close()

        'Catch ex As Exception
        '    MessageBox.Message.Alert(Page, ex.ToString, "str")
        'End Try
        EnableReportAccessControls()
        txtModeReportAccess.Text = ""
    End Sub


    Private Sub EnableARAccessControls()
        btnSaveARAccess.Enabled = False
        btnSaveARAccess.ForeColor = System.Drawing.Color.Gray
        btnCancelARAccess.Enabled = False
        btnCancelARAccess.ForeColor = System.Drawing.Color.Gray

        btnEditARAccess.Enabled = True
        btnEditARAccess.ForeColor = System.Drawing.Color.Black

        chkInvoiceList.Enabled = False
        chkCreditNoteList.Enabled = False
        chkReceiptList.Enabled = False
        chkAdjustmentList.Enabled = False
        chkBatchInvoiceList.Enabled = False
        chkConsolidatedInvoiceList.Enabled = False
        chkARSelectAll.Enabled = False
        chkSalesOrderList.Enabled = False
        txtModeARAccess.Text = ""
    End Sub

    Private Sub DisableARAccessControls()
        btnSaveARAccess.Enabled = True
        btnSaveARAccess.ForeColor = System.Drawing.Color.Black
        btnCancelARAccess.Enabled = True
        btnCancelARAccess.ForeColor = System.Drawing.Color.Black

        btnEditARAccess.Enabled = False
        btnEditARAccess.ForeColor = System.Drawing.Color.Gray

        chkInvoiceList.Enabled = True
        chkCreditNoteList.Enabled = True
        chkReceiptList.Enabled = True
        chkAdjustmentList.Enabled = True
        chkBatchInvoiceList.Enabled = True
        chkConsolidatedInvoiceList.Enabled = True
        chkSalesOrderList.Enabled = True
        chkARSelectAll.Enabled = True

    End Sub

    Protected Sub btnEditARAccess_Click(sender As Object, e As EventArgs) Handles btnEditARAccess.Click
        lblAlert.Text = ""
        If txtRcno.Text = "" Then
            ' MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
            lblAlert.Text = "SELECT GROUP AUTHORITY TO EDIT"
            Return

        End If
        DisableARAccessControls()
        lblMessage.Text = "ACTION: EDIT AR ACCESS RECORDS"
        txtModeARAccess.Text = "EDIT"

        'EnableARAccessControls()

    End Sub

    Protected Sub btnCancelARAccess_Click(sender As Object, e As EventArgs) Handles btnCancelARAccess.Click
        lblAlert.Text = ""
        EnableARAccessControls()

    End Sub

    Protected Sub btnSaveARAccess_Click(sender As Object, e As EventArgs) Handles btnSaveARAccess.Click


        lblAlert.Text = ""

        'If txtRcno.Text = "" Or txtRcNo2.Text = "" Then
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
            'ind = txtIndustry.Text
            'ind = ind.Replace("'", "\\'")
            InsertIntoTblWebEventLog("USERACCESS", "SAVEARACCESS1", txtRcNo.Text, txtGroupAuthority.Text)

            Dim command2 As MySqlCommand = New MySqlCommand

            command2.CommandType = CommandType.Text

            command2.CommandText = "SELECT * FROM tblGroupAccess where GroupAccess=@GroupAccess and rcno<>" & Convert.ToInt32(txtRcno.Text)
            command2.Parameters.AddWithValue("@GroupAccess", txtGroupAuthority.Text)
            command2.Connection = conn

            Dim dr1 As MySqlDataReader = command2.ExecuteReader()
            Dim dt1 As New DataTable
            dt1.Load(dr1)

            InsertIntoTblWebEventLog("USERACCESS", "SAVEARACCESS2", dt1.Rows.Count.ToString, txtGroupAuthority.Text)

            If dt1.Rows.Count > 0 Then

                ' MessageBox.Message.Alert(Page, "Group Authority already exists!!!", "str")
                lblAlert.Text = "GROUP AUTHORITY ALREADY EXISTS"
                txtGroupAuthority.Focus()
                Exit Sub
            Else

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                command1.CommandText = "SELECT * FROM tblGroupAccess where rcno=" & Convert.ToInt32(txtRcno.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                InsertIntoTblWebEventLog("USERACCESS", "SAVEARACCESS3", dt.Rows.Count.ToString, txtGroupAuthority.Text)


                If dt.Rows.Count > 0 Then
                    InsertIntoTblWebEventLog("USERACCESS", "SAVEARACCESS4", txtRcNo.Text, txtGroupAuthority.Text)

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String

                    qry = "update tblGroupAccess set"
                    qry = qry + " x0252 = @x0252, x0252Add  =@x0252Add, x0252Edit=@x0252Edit, x0252Delete=@x0252Delete,     x0252Print  =@x0252Print,  x0252Post = @x0252Post, x0252Reverse=@x0252Reverse, x0252MultiPrint  =@x0252MultiPrint, x0252EditCompanyName  =@x0252EditCompanyName, x0252EditBillingAddress  =@x0252EditBillingAddress, x0252EditOurRef  =@x0252EditOurRef, x0252EditSalesman  =@x0252EditSalesman, x0252EditRemarks  =@x0252EditRemarks,x0252FileUpload=@x0252FileUpload,"
                    qry = qry + " x0253 = @x0253, x0253Add  =@x0253Add, x0253Edit=@x0253Edit, x0253Delete=@x0253Delete,     x0253Print  =@x0253Print,  x0253Post = @x0253Post, x0253Reverse=@x0253Reverse, x0253MultiPrint  =@x0253MultiPrint, "

                    qry = qry + " x0255 = @x0255, x0255Add  =@x0255Add,x0255Edit=@x0255Edit,x0255Delete=@x0255Delete,       x0255Print  =@x0255Print,  x0255Post = @x0255Post, x0255Reverse=@x0255Reverse, x0255QReceipt = @x0255QReceipt, x0255ChequeReturn = @x0255ChequeReturn,x0255FileUpload=@x0255FileUpload,"
                    qry = qry + " x0256 = @x0256, x0256Add = @x0256Add,x0256Edit = @x0256Edit,x0256Delete = @x0256Delete,   x0256Print  =@x0256Print,  x0256Post = @x0256Post, x0256Reverse=@x0256Reverse, x0256EditCompanyName  =@x0256EditCompanyName,"
                    qry = qry + "x0256SubmitECNDN=@x0256SubmitECNDN,x0256CancelECNDN=@x0256CancelECNDN, x0256EditBillingAddress  =@x0256EditBillingAddress,x0256FileUpload=@x0256FileUpload,"
                    qry = qry + " x0258 = @x0258, x0258Add = @x0258Add,x0258Edit = @x0258Edit,x0258Delete = @x0258Delete,   x0258Print  =@x0258Print,  x0258Post = @x0258Post, x0258Reverse=@x0258Reverse,x0258FileUpload=@x0258FileUpload, "
                    qry = qry + " x0252SubmitEInvoice=@x0252SubmitEInvoice,x0252CancelEInvoice=@x0252CancelEInvoice,x0252PrintInvoice = @x0252PrintInvoice, x0252ExportInvoice = @x0252ExportInvoice, x0252ViewInvoice = @x0252ViewInvoice, x0252EmailInvoice = @x0252EmailInvoice, "

                    qry = qry + " x252DeSelectContractGroup = @x252DeSelectContractGroup, x253DeSelectContractGroup = @x253DeSelectContractGroup "
                    qry = qry + ",x0252ViewInvFormat1=@x0252ViewInvFormat1,x0252ViewInvFormat2=@x0252ViewInvFormat2,x0252ViewInvFormat3=@x0252ViewInvFormat3"
                    qry = qry + ",x0252ViewInvFormat4=@x0252ViewInvFormat4,x0252ViewInvFormat5=@x0252ViewInvFormat5,x0252ViewInvFormat6=@x0252ViewInvFormat6"
                    qry = qry + ",x0252ViewInvFormat7=@x0252ViewInvFormat7,x0252ViewInvFormat8=@x0252ViewInvFormat8,x0252ViewInvFormat9=x0252ViewInvFormat9"
                    qry = qry + ",x0252ViewInvFormat10=@x0252ViewInvFormat10,x0252ViewInvFormat11=@x0252ViewInvFormat11,x0252ViewInvFormat12=@x0252ViewInvFormat12"
                    qry = qry + ",x0252ViewInvFormat12=@x0252ViewInvFormat12,x0252ViewInvFormat13=@x0252ViewInvFormat13,x0252ViewInvFormat14=@x0252ViewInvFormat14"
                    qry = qry + ",x0259 = @x0259, x0259Add = @x0259Add,x0259Edit = @x0259Edit,x0259Void = @x0259Void,x0259Print = @x0259Print,x0259Submit = @x0259Submit,x0259Cancel = @x0259Cancel"
                       qry = qry + "  where rcno = " & Convert.ToInt32(txtRcno.Text)

                    command.CommandText = qry
                    command.Parameters.Clear()

                    '1 Invoice
                    InsertIntoTblWebEventLog("USERACCESS", "SAVEARACCESS5", qry, txtGroupAuthority.Text)



                    If chkInvoiceList.Items.FindByValue("Access").Selected = True Then
                        command.Parameters.AddWithValue("@x0252", 1)
                    Else
                        command.Parameters.AddWithValue("@x0252", 0)
                    End If

                    If chkInvoiceList.Items.FindByValue("Add").Selected = True Then
                        command.Parameters.AddWithValue("@x0252Add", 1)
                    Else
                        command.Parameters.AddWithValue("@x0252Add", 0)
                    End If

                    If chkInvoiceList.Items.FindByValue("Edit").Selected = True Then
                        command.Parameters.AddWithValue("@x0252Edit", 1)
                    Else
                        command.Parameters.AddWithValue("@x0252Edit", 0)
                    End If

                    If chkInvoiceList.Items.FindByValue("Delete").Selected = True Then
                        command.Parameters.AddWithValue("@x0252Delete", 1)
                    Else
                        command.Parameters.AddWithValue("@x0252Delete", 0)
                    End If


                    If chkInvoiceList.Items.FindByValue("Print").Selected = True Then
                        command.Parameters.AddWithValue("@x0252Print", 1)
                    Else
                        command.Parameters.AddWithValue("@x0252Print", 0)
                    End If

                    If chkInvoiceList.Items.FindByValue("Post").Selected = True Then
                        command.Parameters.AddWithValue("@x0252Post", 1)
                    Else
                        command.Parameters.AddWithValue("@x0252Post", 0)
                    End If

                    If chkInvoiceList.Items.FindByValue("Reverse").Selected = True Then
                        command.Parameters.AddWithValue("@x0252Reverse", 1)
                    Else
                        command.Parameters.AddWithValue("@x0252Reverse", 0)
                    End If

                    If chkInvoiceList.Items.FindByValue("SubmitEInvoice").Selected = True Then
                        command.Parameters.AddWithValue("@x0252SubmitEInvoice", 1)
                    Else
                        command.Parameters.AddWithValue("@x0252SubmitEInvoice", 0)
                    End If
                    If chkInvoiceList.Items.FindByValue("CancelEInvoice").Selected = True Then
                        command.Parameters.AddWithValue("@x0252CancelEInvoice", 1)
                    Else
                        command.Parameters.AddWithValue("@x0252CancelEInvoice", 0)
                    End If

                    If chkInvoiceList.Items.FindByValue("Quick Receipt").Selected = True Then
                        command.Parameters.AddWithValue("@x0255QReceipt", 1)
                    Else
                        command.Parameters.AddWithValue("@x0255QReceipt", 0)
                    End If


                    If chkInvoiceList.Items.FindByValue("Multi-Print").Selected = True Then
                        command.Parameters.AddWithValue("@x0252MultiPrint", 1)
                    Else
                        command.Parameters.AddWithValue("@x0252MultiPrint", 0)
                    End If

                    If chkInvoiceList.Items.FindByValue("EditInvEditAccountName").Selected = True Then
                        command.Parameters.AddWithValue("@x0252EditCompanyName", 1)
                    Else
                        command.Parameters.AddWithValue("@x0252EditCompanyName", 0)
                    End If

                    If chkInvoiceList.Items.FindByValue("EditInvEditBillingDetail").Selected = True Then
                        command.Parameters.AddWithValue("@x0252EditBillingAddress", 1)
                    Else
                        command.Parameters.AddWithValue("@x0252EditBillingAddress", 0)
                    End If

                    If chkInvoiceList.Items.FindByValue("EditInvEditOurRef").Selected = True Then
                        command.Parameters.AddWithValue("@x0252EditOurRef", 1)
                    Else
                        command.Parameters.AddWithValue("@x0252EditOurRef", 0)
                    End If

                    If chkInvoiceList.Items.FindByValue("EditInvEditSalesman").Selected = True Then
                        command.Parameters.AddWithValue("@x0252EditSalesman", 1)
                    Else
                        command.Parameters.AddWithValue("@x0252EditSalesman", 0)
                    End If

                    If chkInvoiceList.Items.FindByValue("EditInvEditRemarks").Selected = True Then
                        command.Parameters.AddWithValue("@x0252EditRemarks", 1)
                    Else
                        command.Parameters.AddWithValue("@x0252EditRemarks", 0)
                    End If

                    If chkInvoiceList.Items.FindByValue("FileUpload").Selected = True Then
                        command.Parameters.AddWithValue("@x0252FileUpload", 1)
                    Else
                        command.Parameters.AddWithValue("@x0252FileUpload", 0)
                    End If

                    If chkInvoiceList.Items.FindByValue("InvoiceFormat1").Selected = True Then
                        command.Parameters.AddWithValue("@x0252ViewInvFormat1", 1)
                    Else
                        command.Parameters.AddWithValue("@x0252ViewInvFormat1", 0)
                    End If

                    If chkInvoiceList.Items.FindByValue("InvoiceFormat2").Selected = True Then
                        command.Parameters.AddWithValue("@x0252ViewInvFormat2", 1)
                    Else
                        command.Parameters.AddWithValue("@x0252ViewInvFormat2", 0)
                    End If

                    If chkInvoiceList.Items.FindByValue("InvoiceFormat3").Selected = True Then
                        command.Parameters.AddWithValue("@x0252ViewInvFormat3", 1)
                    Else
                        command.Parameters.AddWithValue("@x0252ViewInvFormat3", 0)
                    End If

                    If chkInvoiceList.Items.FindByValue("InvoiceFormat4").Selected = True Then
                        command.Parameters.AddWithValue("@x0252ViewInvFormat4", 1)
                    Else
                        command.Parameters.AddWithValue("@x0252ViewInvFormat4", 0)
                    End If

                    If chkInvoiceList.Items.FindByValue("InvoiceFormat5").Selected = True Then
                        command.Parameters.AddWithValue("@x0252ViewInvFormat5", 1)
                    Else
                        command.Parameters.AddWithValue("@x0252ViewInvFormat5", 0)
                    End If


                    If chkInvoiceList.Items.FindByValue("InvoiceFormat6").Selected = True Then
                        command.Parameters.AddWithValue("@x0252ViewInvFormat6", 1)
                    Else
                        command.Parameters.AddWithValue("@x0252ViewInvFormat6", 0)
                    End If

                    If chkInvoiceList.Items.FindByValue("InvoiceFormat7").Selected = True Then
                        command.Parameters.AddWithValue("@x0252ViewInvFormat7", 1)
                    Else
                        command.Parameters.AddWithValue("@x0252ViewInvFormat7", 0)
                    End If

                    If chkInvoiceList.Items.FindByValue("InvoiceFormat8").Selected = True Then
                        command.Parameters.AddWithValue("@x0252ViewInvFormat8", 1)
                    Else
                        command.Parameters.AddWithValue("@x0252ViewInvFormat8", 0)
                    End If

                    If chkInvoiceList.Items.FindByValue("InvoiceFormat9").Selected = True Then
                        command.Parameters.AddWithValue("@x0252ViewInvFormat9", 1)
                    Else
                        command.Parameters.AddWithValue("@x0252ViewInvFormat9", 0)
                    End If

                    If chkInvoiceList.Items.FindByValue("InvoiceFormat10").Selected = True Then
                        command.Parameters.AddWithValue("@x0252ViewInvFormat10", 1)
                    Else
                        command.Parameters.AddWithValue("@x0252ViewInvFormat10", 0)
                    End If


                    If chkInvoiceList.Items.FindByValue("InvoiceFormat11").Selected = True Then
                        command.Parameters.AddWithValue("@x0252ViewInvFormat11", 1)
                    Else
                        command.Parameters.AddWithValue("@x0252ViewInvFormat11", 0)
                    End If

                    If chkInvoiceList.Items.FindByValue("InvoiceFormat12").Selected = True Then
                        command.Parameters.AddWithValue("@x0252ViewInvFormat12", 1)
                    Else
                        command.Parameters.AddWithValue("@x0252ViewInvFormat12", 0)
                    End If

                    If chkInvoiceList.Items.FindByValue("InvoiceFormat13").Selected = True Then
                        command.Parameters.AddWithValue("@x0252ViewInvFormat13", 1)
                    Else
                        command.Parameters.AddWithValue("@x0252ViewInvFormat13", 0)
                    End If

                    If chkInvoiceList.Items.FindByValue("InvoiceFormat14").Selected = True Then
                        command.Parameters.AddWithValue("@x0252ViewInvFormat14", 1)
                    Else
                        command.Parameters.AddWithValue("@x0252ViewInvFormat14", 0)
                    End If

                    ' Consolidated Invoice

                    If chkConsolidatedInvoiceList.Items.FindByValue("Access").Selected = True Then
                        command.Parameters.AddWithValue("@x0259", 1)
                    Else
                        command.Parameters.AddWithValue("@x0259", 0)
                    End If

                    If chkConsolidatedInvoiceList.Items.FindByValue("Add").Selected = True Then
                        command.Parameters.AddWithValue("@x0259Add", 1)
                    Else
                        command.Parameters.AddWithValue("@x0259Add", 0)
                    End If

                    If chkConsolidatedInvoiceList.Items.FindByValue("Edit").Selected = True Then
                        command.Parameters.AddWithValue("@x0259Edit", 1)
                    Else
                        command.Parameters.AddWithValue("@x0259Edit", 0)
                    End If

                    If chkConsolidatedInvoiceList.Items.FindByValue("Void").Selected = True Then
                        command.Parameters.AddWithValue("@x0259Void", 1)
                    Else
                        command.Parameters.AddWithValue("@x0259Void", 0)
                    End If


                    If chkConsolidatedInvoiceList.Items.FindByValue("Print").Selected = True Then
                        command.Parameters.AddWithValue("@x0259Print", 1)
                    Else
                        command.Parameters.AddWithValue("@x0259Print", 0)
                    End If

                    If chkConsolidatedInvoiceList.Items.FindByValue("SubmitInvoice").Selected = True Then
                        command.Parameters.AddWithValue("@x0259Submit", 1)
                    Else
                        command.Parameters.AddWithValue("@x0259Submit", 0)
                    End If
                    If chkConsolidatedInvoiceList.Items.FindByValue("CancelInvoice").Selected = True Then
                        command.Parameters.AddWithValue("@x0259Cancel", 1)
                    Else
                        command.Parameters.AddWithValue("@x0259Cancel", 0)
                    End If

                    '2. Batch Invoice 

                    If chkBatchInvoiceList.Items.FindByValue("Access").Selected = True Then
                        command.Parameters.AddWithValue("@x0253", 1)
                    Else
                        command.Parameters.AddWithValue("@x0253", 0)
                    End If

                    If chkBatchInvoiceList.Items.FindByValue("Add").Selected = True Then
                        command.Parameters.AddWithValue("@x0253Add", 1)
                    Else
                        command.Parameters.AddWithValue("@x0253Add", 0)
                    End If

                    If chkBatchInvoiceList.Items.FindByValue("Edit").Selected = True Then
                        command.Parameters.AddWithValue("@x0253Edit", 1)
                    Else
                        command.Parameters.AddWithValue("@x0253Edit", 0)
                    End If

                    If chkBatchInvoiceList.Items.FindByValue("Delete").Selected = True Then
                        command.Parameters.AddWithValue("@x0253Delete", 1)
                    Else
                        command.Parameters.AddWithValue("@x0253Delete", 0)
                    End If


                    If chkBatchInvoiceList.Items.FindByValue("Print").Selected = True Then
                        command.Parameters.AddWithValue("@x0253Print", 1)
                    Else
                        command.Parameters.AddWithValue("@x0253Print", 0)
                    End If

                    If chkBatchInvoiceList.Items.FindByValue("Post").Selected = True Then
                        command.Parameters.AddWithValue("@x0253Post", 1)
                    Else
                        command.Parameters.AddWithValue("@x0253Post", 0)
                    End If

                    If chkBatchInvoiceList.Items.FindByValue("Reverse").Selected = True Then
                        command.Parameters.AddWithValue("@x0253Reverse", 1)
                    Else
                        command.Parameters.AddWithValue("@x0253Reverse", 0)
                    End If


                    If chkBatchInvoiceList.Items.FindByValue("Multi-Print").Selected = True Then
                        command.Parameters.AddWithValue("@x0253MultiPrint", 1)
                    Else
                        command.Parameters.AddWithValue("@x0253MultiPrint", 0)
                    End If


                    '3 Receipt

                    If chkReceiptList.Items.FindByValue("Access").Selected = True Then
                        command.Parameters.AddWithValue("@x0255", 1)
                    Else
                        command.Parameters.AddWithValue("@x0255", 0)
                    End If
                    If chkReceiptList.Items.FindByValue("Add").Selected = True Then
                        command.Parameters.AddWithValue("@x0255Add", 1)
                    Else
                        command.Parameters.AddWithValue("@x0255Add", 0)
                    End If
                    If chkReceiptList.Items.FindByValue("Edit").Selected = True Then
                        command.Parameters.AddWithValue("@x0255Edit", 1)
                    Else
                        command.Parameters.AddWithValue("@x0255Edit", 0)
                    End If
                    If chkReceiptList.Items.FindByValue("Delete").Selected = True Then
                        command.Parameters.AddWithValue("@x0255Delete", 1)
                    Else
                        command.Parameters.AddWithValue("@x0255Delete", 0)
                    End If


                    If chkReceiptList.Items.FindByValue("Print").Selected = True Then
                        command.Parameters.AddWithValue("@x0255Print", 1)
                    Else
                        command.Parameters.AddWithValue("@x0255Print", 0)
                    End If

                    If chkReceiptList.Items.FindByValue("Post").Selected = True Then
                        command.Parameters.AddWithValue("@x0255Post", 1)
                    Else
                        command.Parameters.AddWithValue("@x0255Post", 0)
                    End If

                    If chkReceiptList.Items.FindByValue("Reverse").Selected = True Then
                        command.Parameters.AddWithValue("@x0255Reverse", 1)
                    Else
                        command.Parameters.AddWithValue("@x0255Reverse", 0)
                    End If

                    If chkReceiptList.Items.FindByValue("ChequeReturn").Selected = True Then
                        command.Parameters.AddWithValue("@x0255ChequeReturn", 1)
                    Else
                        command.Parameters.AddWithValue("@x0255ChequeReturn", 0)
                    End If

                    If chkReceiptList.Items.FindByValue("FileUpload").Selected = True Then
                        command.Parameters.AddWithValue("@x0255FileUpload", 1)
                    Else
                        command.Parameters.AddWithValue("@x0255FileUpload", 0)
                    End If

                    '4 CN
                    If chkCreditNoteList.Items.FindByValue("Access").Selected = True Then
                        command.Parameters.AddWithValue("@x0256", 1)
                    Else
                        command.Parameters.AddWithValue("@x0256", 0)
                    End If
                    If chkCreditNoteList.Items.FindByValue("Add").Selected = True Then
                        command.Parameters.AddWithValue("@x0256Add", 1)
                    Else
                        command.Parameters.AddWithValue("@x0256Add", 0)
                    End If
                    If chkCreditNoteList.Items.FindByValue("Edit").Selected = True Then
                        command.Parameters.AddWithValue("@x0256Edit", 1)
                    Else
                        command.Parameters.AddWithValue("@x0256Edit", 0)
                    End If
                    If chkCreditNoteList.Items.FindByValue("Delete").Selected = True Then
                        command.Parameters.AddWithValue("@x0256Delete", 1)
                    Else
                        command.Parameters.AddWithValue("@x0256Delete", 0)
                    End If


                    If chkCreditNoteList.Items.FindByValue("Print").Selected = True Then
                        command.Parameters.AddWithValue("@x0256Print", 1)
                    Else
                        command.Parameters.AddWithValue("@x0256Print", 0)
                    End If

                    If chkCreditNoteList.Items.FindByValue("Post").Selected = True Then
                        command.Parameters.AddWithValue("@x0256Post", 1)
                    Else
                        command.Parameters.AddWithValue("@x0256Post", 0)
                    End If

                    If chkCreditNoteList.Items.FindByValue("Reverse").Selected = True Then
                        command.Parameters.AddWithValue("@x0256Reverse", 1)
                    Else
                        command.Parameters.AddWithValue("@x0256Reverse", 0)
                    End If

                    If chkCreditNoteList.Items.FindByValue("SubmitECNDN").Selected = True Then
                        command.Parameters.AddWithValue("@x0256SubmitECNDN", 1)
                    Else
                        command.Parameters.AddWithValue("@x0256SubmitECNDN", 0)
                    End If
                    If chkCreditNoteList.Items.FindByValue("CancelECNDN").Selected = True Then
                        command.Parameters.AddWithValue("@x0256CancelECNDN", 1)
                    Else
                        command.Parameters.AddWithValue("@x0256CancelECNDN", 0)
                    End If

                    If chkCreditNoteList.Items.FindByValue("EditCNEditAccountName").Selected = True Then
                        command.Parameters.AddWithValue("@x0256EditCompanyName", 1)
                    Else
                        command.Parameters.AddWithValue("@x0256EditCompanyName", 0)
                    End If

                    If chkCreditNoteList.Items.FindByValue("EditCNEditBillingDetail").Selected = True Then
                        command.Parameters.AddWithValue("@x0256EditBillingAddress", 1)
                    Else
                        command.Parameters.AddWithValue("@x0256EditBillingAddress", 0)
                    End If

                    If chkCreditNoteList.Items.FindByValue("FileUpload").Selected = True Then
                        command.Parameters.AddWithValue("@x0256FileUpload", 1)
                    Else
                        command.Parameters.AddWithValue("@x0256FileUpload", 0)
                    End If

                    '5 Adjustment

                    If chkAdjustmentList.Items.FindByValue("Access").Selected = True Then
                        command.Parameters.AddWithValue("@x0258", 1)
                    Else
                        command.Parameters.AddWithValue("@x0258", 0)
                    End If
                    If chkAdjustmentList.Items.FindByValue("Add").Selected = True Then
                        command.Parameters.AddWithValue("@x0258Add", 1)
                    Else
                        command.Parameters.AddWithValue("@x0258Add", 0)
                    End If
                    If chkAdjustmentList.Items.FindByValue("Edit").Selected = True Then
                        command.Parameters.AddWithValue("@x0258Edit", 1)
                    Else
                        command.Parameters.AddWithValue("@x0258Edit", 0)
                    End If
                    If chkAdjustmentList.Items.FindByValue("Delete").Selected = True Then
                        command.Parameters.AddWithValue("@x0258Delete", 1)
                    Else
                        command.Parameters.AddWithValue("@x0258Delete", 0)
                    End If


                    If chkAdjustmentList.Items.FindByValue("Print").Selected = True Then
                        command.Parameters.AddWithValue("@x0258Print", 1)
                    Else
                        command.Parameters.AddWithValue("@x0258Print", 0)
                    End If

                    If chkAdjustmentList.Items.FindByValue("Post").Selected = True Then
                        command.Parameters.AddWithValue("@x0258Post", 1)
                    Else
                        command.Parameters.AddWithValue("@x0258Post", 0)
                    End If

                    If chkAdjustmentList.Items.FindByValue("Reverse").Selected = True Then
                        command.Parameters.AddWithValue("@x0258Reverse", 1)
                    Else
                        command.Parameters.AddWithValue("@x0258Reverse", 0)
                    End If

                    If chkAdjustmentList.Items.FindByValue("FileUpload").Selected = True Then
                        command.Parameters.AddWithValue("@x0258FileUpload", 1)
                    Else
                        command.Parameters.AddWithValue("@x0258FileUpload", 0)
                    End If

                    ' x0252PrintInvoice , x0252ExportInvoice, x0252PrintInvoice, x0252EmailInvoice 

                    If chkInvoiceList.Items.FindByValue("PrintInvoice").Selected = True Then
                        command.Parameters.AddWithValue("@x0252PrintInvoice", 1)
                    Else
                        command.Parameters.AddWithValue("@x0252PrintInvoice", 0)
                    End If

                    If chkInvoiceList.Items.FindByValue("ExportInvoice").Selected = True Then
                        command.Parameters.AddWithValue("@x0252ExportInvoice", 1)
                    Else
                        command.Parameters.AddWithValue("@x0252ExportInvoice", 0)
                    End If

                    If chkInvoiceList.Items.FindByValue("ViewInvoice").Selected = True Then
                        command.Parameters.AddWithValue("@x0252ViewInvoice", 1)
                    Else
                        command.Parameters.AddWithValue("@x0252ViewInvoice", 0)
                    End If

                    If chkInvoiceList.Items.FindByValue("EmailInvoice").Selected = True Then
                        command.Parameters.AddWithValue("@x0252EmailInvoice", 1)
                    Else
                        command.Parameters.AddWithValue("@x0252EmailInvoice", 0)
                    End If


                    ''
                    If chkInvoiceList.Items.FindByValue("DeSelectContractGroup").Selected = True Then
                        command.Parameters.AddWithValue("@x252DeSelectContractGroup", 1)
                    Else
                        command.Parameters.AddWithValue("@x252DeSelectContractGroup", 0)
                    End If

                    If chkBatchInvoiceList.Items.FindByValue("DeSelectContractGroup").Selected = True Then
                        command.Parameters.AddWithValue("@x253DeSelectContractGroup", 1)
                    Else
                        command.Parameters.AddWithValue("@x253DeSelectContractGroup", 0)
                    End If
                    ''
                    ''''''''''''''''''''''''''''''''''''''
                    command.Connection = conn

                    command.ExecuteNonQuery()
                    lblMessage.Text = "EDIT: AR MODULE ACCESS SUCCESSFULLY UPDATED"
                End If
            End If

            conn.Close()
            InsertIntoTblWebEventLog("USERACCESS", "SAVEARACCESS5", "CLOSE", txtGroupAuthority.Text)


        Catch ex As Exception
            MessageBox.Message.Alert(Page, ex.ToString, "str")
            InsertIntoTblWebEventLog("USERACCESS", "SAVEARACCESS3", ex.Message.ToString, txtGroupAuthority.Text)


        End Try
        'EnableSetupAccessControls()
        'txtModeSetupAccess.Text = ""


        EnableARAccessControls()
        txtModeARAccess.Text = ""
    End Sub

    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
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

    Protected Sub OnSelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        For Each row As GridViewRow In GridView1.Rows
            If row.RowIndex = GridView1.SelectedIndex Then
                row.BackColor = ColorTranslator.FromHtml("#738A9C")
                row.ToolTip = String.Empty
            Else
                row.BackColor = ColorTranslator.FromHtml("#E4E4E4")
                row.ToolTip = "Click to select this row."
            End If
        Next
    End Sub

    Protected Sub GridView3_PageIndexChanged(sender As Object, e As EventArgs) Handles GridView3.PageIndexChanged

    End Sub

    Protected Sub GridView3_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView3.PageIndexChanging
        GridView3.PageIndex = e.NewPageIndex
        SqlDataSource3.SelectCommand = "SELECT * FROM tblGroupAccessLocation where GroupAccess = '" & lblGroupAuthority6.Text & "'"
        SqlDataSource3.DataBind()
        GridView3.DataBind()

    End Sub


    Protected Sub OnRowDataBound3(sender As Object, e As GridViewRowEventArgs) Handles GridView3.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='silver';this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#E4E4E4';")
            'e.Row.Attributes.Add("onmousedown", "this.style.backgroundColor='#738A9C';")
            'e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#738A9C';")

            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridView3, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Protected Sub OnSelectedIndexChanged3(sender As Object, e As EventArgs)
        For Each row As GridViewRow In GridView3.Rows
            If row.RowIndex = GridView3.SelectedIndex Then
                row.BackColor = ColorTranslator.FromHtml("#738A9C")
                row.ToolTip = String.Empty
            Else
                row.BackColor = ColorTranslator.FromHtml("#E4E4E4")
                row.ToolTip = "Click to select this row."
            End If
        Next
    End Sub
    Protected Sub ddlLocationID_TextChanged(sender As Object, e As EventArgs) Handles ddlLocationID.TextChanged

        Dim hyphenpos As Integer
        hyphenpos = 0
        hyphenpos = (ddlLocationID.Text.IndexOf(":"))

        txtLocationDescription.Text = Left(ddlLocationID.Text, (hyphenpos - 1))
        txtLocationDescription.Text = Right(ddlLocationID.Text, Len(ddlLocationID.Text) - (Len(txtLocationDescription.Text) + 3))
        'txtLocationDescription.Text = Left(ddlLocationID.Text, (hyphenpos - 1))

        'txtLocationDescription.Text = ddlLocationID.Text
    End Sub

    Protected Sub btnAddAccessLocation_Click(sender As Object, e As EventArgs) Handles btnAddAccessLocation.Click

        Dim query As String = ""
        ddlLocationID.Items.Clear()
        ddlLocationID.Items.Add("--SELECT--")

        ' lblAlert.Text = lblGroupAuthority6.Text
        Dim conn As MySqlConnection = New MySqlConnection()
        Dim command As MySqlCommand = New MySqlCommand

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        command.CommandType = CommandType.Text
        command.CommandText = "Select Concat(LocationID,' : ',Location) as LocationConcat from tblLocation where locationID not in (select locationid from tblgroupaccesslocation where groupaccess= '" & lblGroupAuthority6.Text & "') order by Location "

        command.Connection = conn

        Dim dr As MySqlDataReader = command.ExecuteReader()
        Dim dt As New DataTable
        dt.Load(dr)

        If dt.Rows.Count > 0 Then
            query = "Select Concat(LocationID,' : ',Location) as LocationConcat from tblLocation where locationID not in (select locationid from tblgroupaccesslocation where groupaccess= '" & lblGroupAuthority6.Text & "') order by Location "
            ' lblAlert.Text = "1 " + lblGroupAuthority6.Text
            PopulateDropDownList(query, "LocationConcat", "LocationConcat", ddlLocationID)
            ddlLocationID.SelectedIndex = 0
        Else

        End If
      
        '   lblAlert.Text = "2 " + lblGroupAuthority6.Text

        txtLocationDescription.Text = ""
        ddlLocationID.Enabled = True

        btnAddAccessLocation.Enabled = False
        btnAddAccessLocation.ForeColor = System.Drawing.Color.Gray

        btnEditAccessLocation.Enabled = False
        btnEditAccessLocation.ForeColor = System.Drawing.Color.Gray

        btnSaveLocationAccess.Enabled = True
        btnSaveLocationAccess.ForeColor = System.Drawing.Color.Black

        btnCancelLocationAccess.Enabled = True
        btnCancelLocationAccess.ForeColor = System.Drawing.Color.Black

        txtModeLocationAccess.Text = "NEW"
        lblMessage.Text = "ACTION: ADD RECORD"
    End Sub

    Protected Sub btnEditAccessLocation_Click(sender As Object, e As EventArgs) Handles btnEditAccessLocation.Click
        ddlLocationID.Enabled = True

        btnAddAccessLocation.Enabled = False
        btnAddAccessLocation.ForeColor = System.Drawing.Color.Gray

        btnEditAccessLocation.Enabled = False
        btnEditAccessLocation.ForeColor = System.Drawing.Color.Gray

        btnSaveLocationAccess.Enabled = True
        btnSaveLocationAccess.ForeColor = System.Drawing.Color.Black

        btnCancelLocationAccess.Enabled = True
        btnCancelLocationAccess.ForeColor = System.Drawing.Color.Black

        lblMessage.Text = "ACTION: EDIT RECORD"
        txtModeLocationAccess.Text = "EDIT"
    End Sub

    Protected Sub btnSaveLocationAccess_Click(sender As Object, e As EventArgs) Handles btnSaveLocationAccess.Click
        If ddlLocationID.Text = "--SELECT--" Or ddlLocationID.Text = "" Then
            ' MessageBox.Message.Alert(Page, "City cannot be blank!!!", "str")
            lblAlert.Text = "LOCATION CANNOT BE BLANK"
            Return

        End If

        Dim hyphenpos As Integer
        hyphenpos = 0
        hyphenpos = (ddlLocationID.Text.IndexOf(":"))

        Dim locationid As String = ""
        locationid = Left(ddlLocationID.Text, (hyphenpos - 1))

        If txtModeLocationAccess.Text = "NEW" Then
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                command1.CommandText = "SELECT * FROM tblgroupaccesslocation where locationid=@lLocationID and GroupAccess = @Groupaccess "
                command1.Parameters.AddWithValue("@lLocationID", locationid)
                command1.Parameters.AddWithValue("@Groupaccess", lblGroupAuthority6.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    ' MessageBox.Message.Alert(Page, "Record already exists!!!", "str")
                    lblAlert.Text = "RECORD ALREADY EXISTS"
                    ddlLocationID.Focus()
                    Exit Sub
                Else

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "INSERT INTO tblgroupaccesslocation(groupaccess,location, locationID, CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn)"
                    qry = qry + "VALUES(@groupaccess,@location,@locationID, @CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn);"

                    command.CommandText = qry
                    command.Parameters.Clear()

                    If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                        command.Parameters.AddWithValue("@groupaccess", lblGroupAuthority6.Text.ToUpper)
                        command.Parameters.AddWithValue("@location", txtLocationDescription.Text.ToUpper)

                        If ddlLocationID.SelectedIndex = 0 Then
                            command.Parameters.AddWithValue("@locationID", "")
                        Else
                            command.Parameters.AddWithValue("@locationID", locationid.ToUpper)
                        End If

                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        'command.Parameters.AddWithValue("@countrycode", 0)


                    ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                        command.Parameters.AddWithValue("@groupaccess", lblGroupAuthority6.Text.ToUpper)
                        command.Parameters.AddWithValue("@location", txtLocationDescription.Text.ToUpper)

                        If ddlLocationID.SelectedIndex = 0 Then
                            command.Parameters.AddWithValue("@locationID", "")
                        Else
                            command.Parameters.AddWithValue("@locationID", locationid)
                        End If
                        'command.Parameters.AddWithValue("@country", ddlCountry.Text)
                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                        command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))


                    End If


                    command.Connection = conn

                    command.ExecuteNonQuery()
                    txtRcnoLocationAccess.Text = command.LastInsertedId

                    '  MessageBox.Message.Alert(Page, "Record added successfully!!!", "str")
                    lblMessage.Text = "ADD: RECORD SUCCESSFULLY ADDED"
                    lblAlert.Text = ""

                End If
                conn.Close()
                conn.Dispose()
                command1.Dispose()
                dt.Dispose()
                ddlLocationID.Enabled = False


            Catch ex As Exception
                MessageBox.Message.Alert(Page, "Error!!!" + ex.Message.ToString, "str")
            End Try
            EnableControls()
            txtModeLocationAccess.Text = ""
        ElseIf txtModeLocationAccess.Text = "EDIT" Then
            If txtRcno.Text = "" Then
                '  MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
                lblAlert.Text = "SELECT RECORD TO EDIT"
                Return
            End If

            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command2 As MySqlCommand = New MySqlCommand

                command2.CommandType = CommandType.Text

                command2.CommandText = "SELECT * FROM tblgroupaccesslocation where locationid=@lLocationID and GroupAccess = @Groupaccess and rcno<>" & Convert.ToInt32(txtRcnoLocationAccess.Text)
                command2.Parameters.AddWithValue("@lLocationID", locationid)
                command2.Parameters.AddWithValue("@Groupaccess", lblGroupAuthority6.Text)
                command2.Connection = conn

                Dim dr1 As MySqlDataReader = command2.ExecuteReader()
                Dim dt1 As New DataTable
                dt1.Load(dr1)

                If dt1.Rows.Count > 0 Then

                    '  MessageBox.Message.Alert(Page, "city already exists!!!", "str")
                    lblAlert.Text = "LOCATION ALREADY EXISTS"
                    ddlLocationID.Focus()
                    Exit Sub
                Else

                    Dim command1 As MySqlCommand = New MySqlCommand

                    command1.CommandType = CommandType.Text

                    command1.CommandText = "SELECT * FROM tblgroupaccesslocation where rcno=" & Convert.ToInt32(txtRcnoLocationAccess.Text)
                    command1.Connection = conn

                    Dim dr As MySqlDataReader = command1.ExecuteReader()
                    Dim dt As New DataTable
                    dt.Load(dr)

                    If dt.Rows.Count > 0 Then

                        Dim command As MySqlCommand = New MySqlCommand

                        command.CommandType = CommandType.Text
                        Dim qry As String
                        If txtExists.Text = "True" Then
                            'qry = "update tblgroupaccesslocation set location=@location,LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(txtRcnoLocationAccess.Text)

                        Else
                            qry = "update tblgroupaccesslocation set groupaccess=@groupaccess,location=@location,locationId=@locationId, LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(txtRcnoLocationAccess.Text)

                        End If

                        command.CommandText = qry
                        command.Parameters.Clear()

                        If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                            command.Parameters.AddWithValue("@groupaccess", lblGroupAuthority6.Text.ToUpper)
                            command.Parameters.AddWithValue("@location", txtLocationDescription.Text.ToUpper)

                            If ddlLocationID.SelectedIndex = 0 Then
                                command.Parameters.AddWithValue("@locationID", "")
                            Else
                                command.Parameters.AddWithValue("@locationID", locationid.ToUpper)
                            End If

                            'command.Parameters.AddWithValue("@country", ddlCountry.Text.ToUpper)
                            command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                            command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                        ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                            command.Parameters.AddWithValue("@groupaccess", lblGroupAuthority6.Text.ToUpper)
                            command.Parameters.AddWithValue("@location", txtLocationDescription.Text.ToUpper)

                            If ddlLocationID.SelectedIndex = 0 Then
                                command.Parameters.AddWithValue("@locationID", "")
                            Else
                                command.Parameters.AddWithValue("@locationID", locationid)
                            End If
                            'command.Parameters.AddWithValue("@country", ddlCountry.Text)
                            command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                            command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                        End If

                        command.Connection = conn

                        command.ExecuteNonQuery()

                        If txtExists.Text = "True" Then
                            ' MessageBox.Message.Alert(Page, "Record updated successfully!!! Record is in use, so City cannot be updated!!!", "str")
                            lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED. RECORD IS IN USE, SO CITY CANNOT BE UPDATED"
                            lblAlert.Text = ""
                        Else
                            ' MessageBox.Message.Alert(Page, "Record updated successfully!!!", "str")
                            lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED"
                            lblAlert.Text = ""
                        End If
                    End If
                End If

                conn.Close()
                If txtMode.Text = "NEW" Then
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "USERACCESS", ddlLocationID.Text, "ADD", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
                Else
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "USERACCESS", ddlLocationID.Text, "EDIT", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
                End If
            Catch ex As Exception
                MessageBox.Message.Alert(Page, "Error!!!", "str")
            End Try
            'EnableControls()
            txtModeLocationAccess.Text = ""

        End If

        SqlDataSource3.SelectCommand = "SELECT * FROM tblGroupAccessLocation where GroupAccess = '" & lblGroupAuthority6.Text & "'"
        SqlDataSource3.DataBind()
        GridView3.DataBind()

        EnableLocationAccessControls()
        'btnSaveLocationAccess.Enabled = False
        'btnAddAccessLocation.ForeColor = System.Drawing.Color.Gray

        'btnCancelLocationAccess.Enabled = False
        'btnAddAccessLocation.ForeColor = System.Drawing.Color.Gray


        '   MakeMeNull()
        If CheckIfExists() = True Then
            txtExists.Text = "True"
        Else
            txtExists.Text = "False"
        End If
    End Sub

    Protected Sub btnCancelLocationAccess_Click(sender As Object, e As EventArgs) Handles btnCancelLocationAccess.Click
        btnAddAccessLocation.Enabled = True
        btnAddAccessLocation.ForeColor = System.Drawing.Color.Black

        'btnAddAccessLocation.Enabled = True
        'btnAddAccessLocation.ForeColor = System.Drawing.Color.Gray

        btnSaveLocationAccess.Enabled = False
        btnSaveLocationAccess.ForeColor = System.Drawing.Color.Gray

        btnCancelLocationAccess.Enabled = False
        btnCancelLocationAccess.ForeColor = System.Drawing.Color.Gray
        ddlLocationID.Enabled = False
        'btnAddAccessLocation.Enabled = True

        txtModeLocationAccess.Text = ""

    End Sub

    Protected Sub GridView3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView3.SelectedIndexChanged
        If txtMode.Text = "Edit" Then
            lblAlert.Text = "CANNOT SELECT RECORD IN EDIT MODE. SAVE OR CANCEL TO PROCEED"
            Return
        End If


        'EnableControls()
        'MakeMeNull()


        'Dim editindex As Integer = GridView3.SelectedIndex
        'rcno = DirectCast(GridView1.Rows(editindex).FindControl("Label1"), Label).Text
        'txtRcno.Text = rcno.ToString()

        Dim concatstr As String = ""

        'Query = "Select Concat(LocationID,' : ',Location) as LocationConcat from tblLocation order by Location "
        'PopulateDropDownList(Query, "LocationConcat", "LocationConcat", ddlLocationID)

        If GridView3.SelectedRow.Cells(2).Text = "&nbsp;" Then
            ddlLocationID.SelectedIndex = 0
        Else
            'concatstr = Server.HtmlDecode(GridView3.SelectedRow.Cells(2).Text).ToString.Trim & " : " & Server.HtmlDecode(GridView3.SelectedRow.Cells(3).Text).ToString.Trim
            ddlLocationID.Text = Server.HtmlDecode(GridView3.SelectedRow.Cells(2).Text).ToString.Trim & " : " & Server.HtmlDecode(GridView3.SelectedRow.Cells(3).Text).ToString.Trim
        End If


        If GridView3.SelectedRow.Cells(1).Text = "&nbsp;" Then
            txtRcnoLocationAccess.Text = "0"
        Else
            txtRcnoLocationAccess.Text = Server.HtmlDecode(GridView3.SelectedRow.Cells(1).Text).ToString
        End If


        If GridView3.SelectedRow.Cells(3).Text = "&nbsp;" Then
            txtLocationDescription.Text = ""
        Else
            txtLocationDescription.Text = Server.HtmlDecode(GridView3.SelectedRow.Cells(3).Text).ToString
        End If

        btnEditAccessLocation.Enabled = True
        btnEditAccessLocation.ForeColor = System.Drawing.Color.Black
        'btnDelete.Enabled = True
        'btnDelete.ForeColor = System.Drawing.Color.Black
        'btnQuit.Enabled = True
        'btnQuit.ForeColor = System.Drawing.Color.Black

        'If CheckIfExists() = True Then
        '    txtExists.Text = "True"
        'Else
        '    txtExists.Text = "False"
        'End If

        'SqlDataSource2.SelectCommand = "SELECT * FROM tblstaff where secgroupauthority = '" & txtGroupAuthority.Text & "'"
        'SqlDataSource2.DataBind()
        'GridView2.DataBind()


        'SqlDataSource3.SelectCommand = "SELECT * FROM tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "'"
        'SqlDataSource3.DataBind()
        'GridView3.DataBind()


        'RetrieveData()

        'txtGroupAuthority.Enabled = False

        'If Session("SecGroupAuthority") <> "ADMINISTRATOR" Then
        '    AccessControl()
        'End If
    End Sub

    Protected Sub btnDeleteAccessLocation_Click(sender As Object, e As EventArgs) Handles btnDeleteAccessLocation.Click
        lblMessage.Text = ""
        If txtRcnoLocationAccess.Text = "" Then
            '  MessageBox.Message.Alert(Page, "Select a record to delete!!!", "str")
            lblAlert.Text = "SELECT RECORD TO DELETE"
            Return

        End If
        lblMessage.Text = "ACTION: DELETE RECORD"

        Dim confirmValue As String = Request.Form("confirm_value")
        If confirmValue = "Yes" Then

            'If txtExists.Text = "True" Then
            '    ' MessageBox.Message.Alert(Page, "Record is in use, cannot be modified!!!", "str")
            '    lblAlert.Text = "RECORD IS IN USE, CANNOT BE DELETED"
            '    Return
            'End If


            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                'Dim command1 As MySqlCommand = New MySqlCommand

                'command1.CommandType = CommandType.Text

                'command1.CommandText = "SELECT * FROM tblGroupAccess where rcno=" & Convert.ToInt32(txtRcno.Text)
                'command1.Connection = conn

                'Dim dr As MySqlDataReader = command1.ExecuteReader()
                'Dim dt As New DataTable
                'dt.Load(dr)

                'If dt.Rows.Count > 0 Then

                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text

                Dim qry As String = "delete from tblGroupAccessLocation where rcno=" & Convert.ToInt32(txtRcnoLocationAccess.Text)

                command.CommandText = qry
                command.Connection = conn

                command.ExecuteNonQuery()

                'Dim command2 As MySqlCommand = New MySqlCommand
                'command2.CommandType = CommandType.Text
                'command2.CommandText = "delete from tblGroupAccess where rcno=" & Convert.ToInt32(txtRcNo2.Text)
                'command2.Connection = conn
                'command2.ExecuteNonQuery()

                '  MessageBox.Message.Alert(Page, "Record deleted successfully!!!", "str")
                lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"
                'End If
                conn.Close()
                conn.Dispose()
                command.Dispose()
                ddlLocationID.SelectedIndex = 0
                txtLocationDescription.Text = ""
            Catch ex As Exception
                MessageBox.Message.Alert(Page, ex.ToString, "str")
            End Try
            EnableControls()


            SqlDataSource3.SelectCommand = "SELECT * FROM tblGroupAccessLocation where GroupAccess = '" & lblGroupAuthority6.Text & "'"
            SqlDataSource3.DataBind()
            GridView3.DataBind()

            'GridView3.DataSourceID = "SqlDataSource3"
            MakeMeNull()
            lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"
        End If

    End Sub

    Protected Sub btnSaveToolsAccess_Click(sender As Object, e As EventArgs) Handles btnSaveToolsAccess.Click
        lblAlert.Text = ""
        lblMessage.Text = ""
        'If txtRcno.Text = "" Or txtRcNo2.Text = "" Then
        If txtRcno.Text = "" Then
            ' MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
            lblAlert.Text = "SELECT RECORD TO EDIT"
            Return

        End If

        '     Try
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        'Dim ind As String
        'ind = txtIndustry.Text
        'ind = ind.Replace("'", "\\'")

        Dim command2 As MySqlCommand = New MySqlCommand

        command2.CommandType = CommandType.Text

        command2.CommandText = "SELECT * FROM tblGroupAccess where GroupAccess=@GroupAccess and rcno<>" & Convert.ToInt32(txtRcno.Text)
        command2.Parameters.AddWithValue("@GroupAccess", txtGroupAuthority.Text)
        command2.Connection = conn

        Dim dr1 As MySqlDataReader = command2.ExecuteReader()
        Dim dt1 As New DataTable
        dt1.Load(dr1)

        If dt1.Rows.Count > 0 Then

            ' MessageBox.Message.Alert(Page, "Group Authority already exists!!!", "str")
            lblAlert.Text = "GROUP AUTHORITY ALREADY EXISTS"
            txtGroupAuthority.Focus()
            Exit Sub
        Else

            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.CommandText = "SELECT * FROM tblGroupAccess where rcno=" & Convert.ToInt32(txtRcno.Text)
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then

                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                Dim qry As String

                qry = "update tblGroupAccess set "
                qry = qry + " x0401ToolsAddressVerification = @x0401ToolsAddressVerification,"
                qry = qry + " x0401ToolsFloorPlan = @x0401ToolsFloorPlan"

                qry = qry + "  where rcno = " & Convert.ToInt32(txtRcno.Text)

                command.CommandText = qry
                command.Parameters.Clear()

                'BillingCodes



                If chkToolsList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0401ToolsAddressverification", 1)
                Else
                    command.Parameters.AddWithValue("@x0401ToolsAddressverification", 0)
                End If

                If chkFloorPlanList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x0401ToolsFloorPlan", 1)
                Else
                    command.Parameters.AddWithValue("@x0401ToolsFloorPlan", 0)
                End If
                ''''''''''''''''''''''''''''''''''''''''''''




                ''''''''''''''''''''''''''''''''''''''
                command.Connection = conn

                command.ExecuteNonQuery()



                lblMessage.Text = "EDIT: TOOLS ACCESS SUCCESSFULLY UPDATED"
            End If
        End If

        conn.Close()

        'Catch ex As Exception
        '    MessageBox.Message.Alert(Page, ex.ToString, "str")
        'End Try
        'EnableSetupAccessControls()
        'txtModeSetupAccess.Text = ""

        EnableToolsAccessControls()
        txtModeToolsAccess.Text = ""

    End Sub

    Protected Sub btnEditToolsAccess_Click(sender As Object, e As EventArgs) Handles btnEditToolsAccess.Click
        lblAlert.Text = ""
        If txtRcno.Text = "" Then
            ' MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
            lblAlert.Text = "SELECT GROUP AUTHORITY TO EDIT"
            Return

        End If
        DisablebleToolsAccessControls()
        lblMessage.Text = "ACTION: EDIT TOOLS ACCESS RECORDS"
        txtModeToolsAccess.Text = "EDIT"
    End Sub

    Protected Sub btnCancelToolsAccess_Click(sender As Object, e As EventArgs) Handles btnCancelToolsAccess.Click
        lblAlert.Text = ""
        EnableToolsAccessControls()

    End Sub

    Protected Sub GridView2_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView2.PageIndexChanging
        GridView2.PageIndex = e.NewPageIndex

        SqlDataSource2.SelectCommand = "SELECT if(status='O', 'Active','InActive') as Active, staffid, name, nric, type, datejoin, dateleft, appointment, payrollid FROM tblstaff where secgroupauthority = '" & txtGroupAuthority.Text & "' order by Active,Staffid,Name"
        SqlDataSource2.DataBind()
        GridView2.DataBind()

        'SqlDataSource3.SelectCommand = "SELECT * FROM tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "'"
        'SqlDataSource3.DataBind()
        'GridView3.DataBind()
    End Sub

  
    Private Sub EnableAssetAccessControls()
        btnSaveAssetAccess.Enabled = False
        btnSaveAssetAccess.ForeColor = System.Drawing.Color.Gray
        btnCancelAssetAccess.Enabled = False
        btnCancelAssetAccess.ForeColor = System.Drawing.Color.Gray

        btnEditAssetAccess.Enabled = True
        btnEditAssetAccess.ForeColor = System.Drawing.Color.Black

        chkAssetList.Enabled = False
        chkAssetSupplierList.Enabled = False
        chkAssetBrandList.Enabled = False
        chkAssetClassList.Enabled = False
        chkAssetColorList.Enabled = False
        chkAssetGroupList.Enabled = False
        chkAssetModelList.Enabled = False
        chkAssetStatusList.Enabled = False
        chkAssetMovementTypeList.Enabled = False

        'chkUserStaffList.Enabled = False
        'chkReportsList.Enabled = False

        chkAssetSelectAll.Enabled = False

        txtModeAssetAccess.Text = ""
    End Sub

    Private Sub DisableAssetAccessControls()
        btnSaveAssetAccess.Enabled = True
        btnSaveAssetAccess.ForeColor = System.Drawing.Color.Black
        btnCancelAssetAccess.Enabled = True
        btnCancelAssetAccess.ForeColor = System.Drawing.Color.Black

        btnEditAssetAccess.Enabled = False
        btnEditAssetAccess.ForeColor = System.Drawing.Color.Gray

        chkAssetList.Enabled = True

        chkAssetList.Enabled = True
        chkAssetSupplierList.Enabled = True
        chkAssetBrandList.Enabled = True
        chkAssetClassList.Enabled = True
        chkAssetColorList.Enabled = True
        chkAssetGroupList.Enabled = True
        chkAssetModelList.Enabled = True
        chkAssetStatusList.Enabled = True
        chkAssetMovementTypeList.Enabled = True

        'chkUserStaffList.Enabled = True
        'chkReportsList.Enabled = True

        chkAssetSelectAll.Enabled = True

    End Sub

    Protected Sub btnEditAssetAccess_Click(sender As Object, e As EventArgs) Handles btnEditAssetAccess.Click
        lblAlert.Text = ""
        If txtRcno.Text = "" Then
            ' MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
            lblAlert.Text = "SELECT GROUP AUTHORITY TO EDIT"
            Return

        End If
        DisableAssetAccessControls()
        lblMessage.Text = "ACTION: EDIT ASSET ACCESS RECORDS"
        txtModeAssetAccess.Text = "EDIT"
    End Sub

    Protected Sub btnCancelAssetAccess_Click(sender As Object, e As EventArgs) Handles btnCancelAssetAccess.Click
        lblAlert.Text = ""
        EnableAssetAccessControls()
    End Sub

    Protected Sub btnSaveAssetAccess_Click(sender As Object, e As EventArgs) Handles btnSaveAssetAccess.Click
        lblMessage.Text = ""
        lblAlert.Text = ""
        'If txtRcno.Text = "" Or txtRcNo2.Text = "" Then
        If txtRcno.Text = "" Then
            ' MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
            lblAlert.Text = "SELECT RECORD TO EDIT"
            Return

        End If

        '     Try
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        'Dim ind As String
        'ind = txtIndustry.Text
        'ind = ind.Replace("'", "\\'")

        Dim command2 As MySqlCommand = New MySqlCommand

        command2.CommandType = CommandType.Text

        command2.CommandText = "SELECT * FROM tblGroupAccess where GroupAccess=@GroupAccess and rcno<>" & Convert.ToInt32(txtRcno.Text)
        command2.Parameters.AddWithValue("@GroupAccess", txtGroupAuthority.Text)
        command2.Connection = conn

        Dim dr1 As MySqlDataReader = command2.ExecuteReader()
        Dim dt1 As New DataTable
        dt1.Load(dr1)

        If dt1.Rows.Count > 0 Then

            ' MessageBox.Message.Alert(Page, "Group Authority already exists!!!", "str")
            lblAlert.Text = "Group Authority ALREADY EXISTS"
            txtGroupAuthority.Focus()
            Exit Sub
        Else

            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.CommandText = "SELECT * FROM tblGroupAccess where rcno=" & Convert.ToInt32(txtRcno.Text)
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then

                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                Dim qry As String = "update tblGroupAccess set x1301AssetView = @x1301AssetView,x1301AssetAdd = @x1301AssetAdd,x1301AssetEdit = @x1301AssetEdit,x1301AssetDelete = @x1301AssetDelete,x1301AssetPrint = @x1301AssetPrint,x1301AssetMovement = @x1301AssetMovement,x1302AssetSupplierView=@x1302AssetSupplierView,x1302AssetSupplierAdd=@x1302AssetSupplierAdd,x1302AssetSupplierEdit=@x1302AssetSupplierEdit,x1302AssetSupplierDelete=@x1302AssetSupplierDelete,x1302AssetBrandView=@x1302AssetBrandView,x1302AssetBrandAdd=@x1302AssetBrandAdd,x1302AssetBrandEdit=@x1302AssetBrandEdit,x1302AssetBrandDelete=@x1302AssetBrandDelete,x1302AssetClassView=@x1302AssetClassView,x1302AssetClassAdd=@x1302AssetClassAdd,x1302AssetClassEdit=@x1302AssetClassEdit,x1302AssetClassDelete=@x1302AssetClassDelete,x1302AssetColorView=@x1302AssetColorView,x1302AssetColorAdd=@x1302AssetColorAdd,x1302AssetColorEdit=@x1302AssetColorEdit,x1302AssetColorDelete=@x1302AssetColorDelete,x1302AssetClassView=@x1302AssetClassView,x1302AssetClassAdd=@x1302AssetClassAdd,x1302AssetClassEdit=@x1302AssetClassEdit,x1302AssetClassDelete=@x1302AssetClassDelete,x1302AssetGroupView=@x1302AssetGroupView,x1302AssetGroupAdd=@x1302AssetGroupAdd,x1302AssetGroupEdit=@x1302AssetGroupEdit,x1302AssetGroupDelete=@x1302AssetGroupDelete,x1302AssetModelView=@x1302AssetModelView,x1302AssetModelAdd=@x1302AssetModelAdd,x1302AssetModelEdit=@x1302AssetModelEdit,x1302AssetModelDelete=@x1302AssetModelDelete,x1302AssetStatusView=@x1302AssetStatusView,x1302AssetStatusAdd=@x1302AssetStatusAdd,x1302AssetStatusEdit=@x1302AssetStatusEdit,x1302AssetStatusDelete=@x1302AssetStatusDelete,x1302AssetMovementTypeView=@x1302AssetMovementTypeView,x1302AssetMovementTypeAdd=@x1302AssetMovementTypeAdd,x1302AssetMovementTypeEdit=@x1302AssetMovementTypeEdit,x1302AssetMovementTypeDelete=@x1302AssetMovementTypeDelete where rcno=" & Convert.ToInt32(txtRcno.Text)

                command.CommandText = qry
                command.Parameters.Clear()

                'Asset Access
                If chkAssetList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x1301AssetView", 1)
                Else
                    command.Parameters.AddWithValue("@x1301AssetView", 0)
                End If
                If chkAssetList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x1301AssetAdd", 1)
                Else
                    command.Parameters.AddWithValue("@x1301AssetAdd", 0)
                End If
                If chkAssetList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x1301AssetEdit", 1)
                Else
                    command.Parameters.AddWithValue("@x1301AssetEdit", 0)
                End If
                If chkAssetList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x1301AssetDelete", 1)
                Else
                    command.Parameters.AddWithValue("@x1301AssetDelete", 0)
                End If
                If chkAssetList.Items.FindByValue("Print").Selected = True Then
                    command.Parameters.AddWithValue("@x1301AssetPrint", 1)
                Else
                    command.Parameters.AddWithValue("@x1301AssetPrint", 0)
                End If
                If chkAssetList.Items.FindByValue("Movement").Selected = True Then
                    command.Parameters.AddWithValue("@x1301AssetMovement", 1)
                Else
                    command.Parameters.AddWithValue("@x1301AssetMovement", 0)
                End If

                'Asset Supplier Access


                If chkAssetSupplierList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x1302AssetSupplierView", 1)
                Else
                    command.Parameters.AddWithValue("@x1302AssetSupplierView", 0)
                End If
                If chkAssetSupplierList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x1302AssetSupplierAdd", 1)
                Else
                    command.Parameters.AddWithValue("@x1302AssetSupplierAdd", 0)
                End If
                If chkAssetSupplierList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x1302AssetSupplierEdit", 1)
                Else
                    command.Parameters.AddWithValue("@x1302AssetSupplierEdit", 0)
                End If
                If chkAssetSupplierList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x1302AssetSupplierDelete", 1)
                Else
                    command.Parameters.AddWithValue("@x1302AssetSupplierDelete", 0)
                End If

                'Asset Brand Access


                If chkAssetBrandList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x1302AssetBrandView", 1)
                Else
                    command.Parameters.AddWithValue("@x1302AssetBrandView", 0)
                End If
                If chkAssetBrandList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x1302AssetBrandAdd", 1)
                Else
                    command.Parameters.AddWithValue("@x1302AssetBrandAdd", 0)
                End If
                If chkAssetBrandList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x1302AssetBrandEdit", 1)
                Else
                    command.Parameters.AddWithValue("@x1302AssetBrandEdit", 0)
                End If
                If chkAssetBrandList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x1302AssetBrandDelete", 1)
                Else
                    command.Parameters.AddWithValue("@x1302AssetBrandDelete", 0)
                End If

                'Asset Class Access


                If chkAssetClassList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x1302AssetClassView", 1)
                Else
                    command.Parameters.AddWithValue("@x1302AssetClassView", 0)
                End If
                If chkAssetClassList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x1302AssetClassAdd", 1)
                Else
                    command.Parameters.AddWithValue("@x1302AssetClassAdd", 0)
                End If
                If chkAssetClassList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x1302AssetClassEdit", 1)
                Else
                    command.Parameters.AddWithValue("@x1302AssetClassEdit", 0)
                End If
                If chkAssetClassList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x1302AssetClassDelete", 1)
                Else
                    command.Parameters.AddWithValue("@x1302AssetClassDelete", 0)
                End If

                'Asset Color Access


                If chkAssetColorList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x1302AssetColorView", 1)
                Else
                    command.Parameters.AddWithValue("@x1302AssetColorView", 0)
                End If
                If chkAssetColorList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x1302AssetColorAdd", 1)
                Else
                    command.Parameters.AddWithValue("@x1302AssetColorAdd", 0)
                End If
                If chkAssetColorList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x1302AssetColorEdit", 1)
                Else
                    command.Parameters.AddWithValue("@x1302AssetColorEdit", 0)
                End If
                If chkAssetColorList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x1302AssetColorDelete", 1)
                Else
                    command.Parameters.AddWithValue("@x1302AssetColorDelete", 0)
                End If

                'Asset Group Access


                If chkAssetGroupList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x1302AssetGroupView", 1)
                Else
                    command.Parameters.AddWithValue("@x1302AssetGroupView", 0)
                End If
                If chkAssetGroupList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x1302AssetGroupAdd", 1)
                Else
                    command.Parameters.AddWithValue("@x1302AssetGroupAdd", 0)
                End If
                If chkAssetGroupList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x1302AssetGroupEdit", 1)
                Else
                    command.Parameters.AddWithValue("@x1302AssetGroupEdit", 0)
                End If
                If chkAssetGroupList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x1302AssetGroupDelete", 1)
                Else
                    command.Parameters.AddWithValue("@x1302AssetGroupDelete", 0)
                End If

                'Asset Model Access


                If chkAssetModelList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x1302AssetModelView", 1)
                Else
                    command.Parameters.AddWithValue("@x1302AssetModelView", 0)
                End If
                If chkAssetModelList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x1302AssetModelAdd", 1)
                Else
                    command.Parameters.AddWithValue("@x1302AssetModelAdd", 0)
                End If
                If chkAssetModelList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x1302AssetModelEdit", 1)
                Else
                    command.Parameters.AddWithValue("@x1302AssetModelEdit", 0)
                End If
                If chkAssetModelList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x1302AssetModelDelete", 1)
                Else
                    command.Parameters.AddWithValue("@x1302AssetModelDelete", 0)
                End If

                'Asset Status Access


                If chkAssetStatusList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x1302AssetStatusView", 1)
                Else
                    command.Parameters.AddWithValue("@x1302AssetStatusView", 0)
                End If
                If chkAssetStatusList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x1302AssetStatusAdd", 1)
                Else
                    command.Parameters.AddWithValue("@x1302AssetStatusAdd", 0)
                End If
                If chkAssetStatusList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x1302AssetStatusEdit", 1)
                Else
                    command.Parameters.AddWithValue("@x1302AssetStatusEdit", 0)
                End If
                If chkAssetStatusList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x1302AssetStatusDelete", 1)
                Else
                    command.Parameters.AddWithValue("@x1302AssetStatusDelete", 0)
                End If

                'Asset MovementType Access


                If chkAssetMovementTypeList.Items.FindByValue("Access").Selected = True Then
                    command.Parameters.AddWithValue("@x1302AssetMovementTypeView", 1)
                Else
                    command.Parameters.AddWithValue("@x1302AssetMovementTypeView", 0)
                End If
                If chkAssetMovementTypeList.Items.FindByValue("Add").Selected = True Then
                    command.Parameters.AddWithValue("@x1302AssetMovementTypeAdd", 1)
                Else
                    command.Parameters.AddWithValue("@x1302AssetMovementTypeAdd", 0)
                End If
                If chkAssetMovementTypeList.Items.FindByValue("Edit").Selected = True Then
                    command.Parameters.AddWithValue("@x1302AssetMovementTypeEdit", 1)
                Else
                    command.Parameters.AddWithValue("@x1302AssetMovementTypeEdit", 0)
                End If
                If chkAssetMovementTypeList.Items.FindByValue("Delete").Selected = True Then
                    command.Parameters.AddWithValue("@x1302AssetMovementTypeDelete", 1)
                Else
                    command.Parameters.AddWithValue("@x1302AssetMovementTypeDelete", 0)
                End If

                command.Connection = conn

                command.ExecuteNonQuery()


                lblMessage.Text = "EDIT: ASSET ACCESS SUCCESSFULLY UPDATED"
            End If
        End If

        conn.Close()

        'Catch ex As Exception
        '    MessageBox.Message.Alert(Page, ex.ToString, "str")
        'End Try
        EnableAssetAccessControls()
        txtModeAssetAccess.Text = ""
    End Sub


    Protected Sub btnAddAssetGroupAccess_Click(sender As Object, e As EventArgs) Handles btnAddAssetGroupAccess.Click

        ddlAssetGroup.SelectedIndex = 0
        ddlAssetGroup.Enabled = True

        btnAddAssetGroupAccess.Enabled = False
        btnAddAssetGroupAccess.ForeColor = System.Drawing.Color.Gray

        btnEditAssetGroupAccess.Enabled = False
        btnEditAssetGroupAccess.ForeColor = System.Drawing.Color.Gray

        btnSaveAssetGroupAccess.Enabled = True
        btnSaveAssetGroupAccess.ForeColor = System.Drawing.Color.Black

        btnCancelAssetGroupAccess.Enabled = True
        btnCancelAssetGroupAccess.ForeColor = System.Drawing.Color.Black

        txtMode.Text = "NEW"
        lblMessage.Text = "ACTION: ADD RECORD"
    End Sub

    Protected Sub btnEditAssetGroupAccess_Click(sender As Object, e As EventArgs) Handles btnEditAssetGroupAccess.Click
        ddlAssetGroup.Enabled = True

        btnAddAssetGroupAccess.Enabled = False
        btnAddAssetGroupAccess.ForeColor = System.Drawing.Color.Gray

        btnEditAssetGroupAccess.Enabled = False
        btnEditAssetGroupAccess.ForeColor = System.Drawing.Color.Gray

        btnSaveAssetGroupAccess.Enabled = True
        btnSaveAssetGroupAccess.ForeColor = System.Drawing.Color.Black

        btnCancelAssetGroupAccess.Enabled = True
        btnCancelAssetGroupAccess.ForeColor = System.Drawing.Color.Black

        lblMessage.Text = "ACTION: EDIT RECORD"
        txtMode.Text = "EDIT"
    End Sub

    Private Sub EnableAssetGroupAccessControls()
        btnSaveAssetGroupAccess.Enabled = False
        btnSaveAssetGroupAccess.ForeColor = System.Drawing.Color.Gray
        btnCancelAssetGroupAccess.Enabled = False
        btnCancelAssetGroupAccess.ForeColor = System.Drawing.Color.Gray

        btnAddAssetGroupAccess.Enabled = True
        btnAddAssetGroupAccess.ForeColor = System.Drawing.Color.Black

        btnEditAssetGroupAccess.Enabled = False
        btnEditAssetGroupAccess.ForeColor = System.Drawing.Color.Gray

        ddlAssetGroup.Enabled = False

        'btnDelete.Enabled = False
        'btnDelete.ForeColor = System.Drawing.Color.Gray

        'btnQuit.Enabled = True
        'btnQuit.ForeColor = System.Drawing.Color.Black
        'btnPrint.Enabled = True
        'btnPrint.ForeColor = System.Drawing.Color.Black
        'txtGroupAuthority.Enabled = False
        'txtComments.Enabled = False
        ' chkAccess.Enabled = False

        'txtMode.Text = ""
    End Sub

    Protected Sub btnSaveAssetGroupAccess_Click(sender As Object, e As EventArgs) Handles btnSaveAssetGroupAccess.Click
        If ddlAssetGroup.Text = "" Then
            ' MessageBox.Message.Alert(Page, "City cannot be blank!!!", "str")
            lblAlert.Text = "ASSET GROUP CANNOT BE BLANK"
            Return

        End If

        'Dim hyphenpos As Integer
        'hyphenpos = 0
        'hyphenpos = (ddlLocationID.Text.IndexOf(":"))

        'Dim locationid As String = ""
        'locationid = Left(ddlLocationID.Text, (hyphenpos - 1))

        If txtMode.Text = "NEW" Then
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                command1.CommandText = "SELECT * FROM tblassetgroupaccess where assetgroup=@assetgroup and GroupAccess = @Groupaccess "
                command1.Parameters.AddWithValue("@assetgroup", ddlAssetGroup.Text)
                command1.Parameters.AddWithValue("@Groupaccess", lblGroupAuthority8.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    ' MessageBox.Message.Alert(Page, "Record already exists!!!", "str")
                    lblAlert.Text = "RECORD ALREADY EXISTS"
                    ddlLocationID.Focus()
                    Exit Sub
                Else

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "INSERT INTO tblassetgroupaccess(groupaccess,assetgroup, CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn)"
                    qry = qry + "VALUES(@groupaccess,@assetgroup, @CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn);"

                    command.CommandText = qry
                    command.Parameters.Clear()

                    If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                        command.Parameters.AddWithValue("@groupaccess", lblGroupAuthority8.Text.ToUpper)

                        If ddlAssetGroup.SelectedIndex = 0 Then
                            command.Parameters.AddWithValue("@assetgroup", "")
                        Else
                            command.Parameters.AddWithValue("@assetgroup", ddlAssetGroup.Text.ToUpper)
                        End If

                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        'command.Parameters.AddWithValue("@countrycode", 0)


                    ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                        command.Parameters.AddWithValue("@groupaccess", lblGroupAuthority8.Text.ToUpper)

                        If ddlAssetGroup.SelectedIndex = 0 Then
                            command.Parameters.AddWithValue("@assetgroup", "")
                        Else
                            command.Parameters.AddWithValue("@assetgroup", ddlAssetGroup.Text)
                        End If
                        'command.Parameters.AddWithValue("@country", ddlCountry.Text)
                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                        command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))


                    End If


                    command.Connection = conn

                    command.ExecuteNonQuery()
                    txtRcnoLocationAccess.Text = command.LastInsertedId

                    '  MessageBox.Message.Alert(Page, "Record added successfully!!!", "str")
                    lblMessage.Text = "ADD: RECORD SUCCESSFULLY ADDED"
                    lblAlert.Text = ""

                End If
                conn.Close()
                conn.Dispose()
                command1.Dispose()
                dt.Dispose()
                ddlAssetGroup.Enabled = False


            Catch ex As Exception
                MessageBox.Message.Alert(Page, "Error!!!" + ex.Message.ToString, "str")
            End Try
            EnableControls()
            txtMode.Text = ""
        ElseIf txtMode.Text = "EDIT" Then
            If txtRcno.Text = "" Then
                '  MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
                lblAlert.Text = "SELECT RECORD TO EDIT"
                Return
            End If

            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command2 As MySqlCommand = New MySqlCommand

                command2.CommandType = CommandType.Text

                command2.CommandText = "SELECT * FROM tblassetgroupaccess where assetgroup=@assetgroup and GroupAccess = @Groupaccess and rcno<>" & Convert.ToInt32(txtRcnoLocationAccess.Text)
                command2.Parameters.AddWithValue("@assetgroup", ddlAssetGroup.Text)
                command2.Parameters.AddWithValue("@Groupaccess", lblGroupAuthority8.Text)
                command2.Connection = conn

                Dim dr1 As MySqlDataReader = command2.ExecuteReader()
                Dim dt1 As New DataTable
                dt1.Load(dr1)

                If dt1.Rows.Count > 0 Then

                    '  MessageBox.Message.Alert(Page, "city already exists!!!", "str")
                    lblAlert.Text = "ASSET GROUP ALREADY EXISTS"
                    ddlLocationID.Focus()
                    Exit Sub
                Else

                    Dim command1 As MySqlCommand = New MySqlCommand

                    command1.CommandType = CommandType.Text

                    command1.CommandText = "SELECT * FROM tblassetgroupaccess where rcno=" & Convert.ToInt32(txtRcNoAssetGroupAccess.Text)
                    command1.Connection = conn

                    Dim dr As MySqlDataReader = command1.ExecuteReader()
                    Dim dt As New DataTable
                    dt.Load(dr)

                    If dt.Rows.Count > 0 Then

                        Dim command As MySqlCommand = New MySqlCommand

                        command.CommandType = CommandType.Text
                        Dim qry As String
                        If txtExists.Text = "True" Then
                            'qry = "update tblgroupaccesslocation set location=@location,LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(txtRcnoLocationAccess.Text)

                        Else
                            qry = "update tblassetgroupaccess set groupaccess=@groupaccess,assetgroup=@assetgroup,LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(txtRcnoLocationAccess.Text)

                        End If

                        command.CommandText = qry
                        command.Parameters.Clear()

                        If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                            command.Parameters.AddWithValue("@groupaccess", lblGroupAuthority8.Text.ToUpper)

                            If ddlAssetGroup.SelectedIndex = 0 Then
                                command.Parameters.AddWithValue("@assetgroup", "")
                            Else
                                command.Parameters.AddWithValue("@assetgroup", ddlAssetGroup.Text.ToUpper)
                            End If

                            'command.Parameters.AddWithValue("@country", ddlCountry.Text.ToUpper)
                            command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                            command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                        ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                            command.Parameters.AddWithValue("@groupaccess", lblGroupAuthority8.Text.ToUpper)

                            If ddlAssetGroup.SelectedIndex = 0 Then
                                command.Parameters.AddWithValue("@assetgroup", "")
                            Else
                                command.Parameters.AddWithValue("@assetgroup", ddlAssetGroup.Text)
                            End If
                            'command.Parameters.AddWithValue("@country", ddlCountry.Text)
                            command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                            command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                        End If

                        command.Connection = conn

                        command.ExecuteNonQuery()

                        If txtExists.Text = "True" Then
                            ' MessageBox.Message.Alert(Page, "Record updated successfully!!! Record is in use, so City cannot be updated!!!", "str")
                            lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED. RECORD IS IN USE, SO CITY CANNOT BE UPDATED"
                            lblAlert.Text = ""
                        Else
                            ' MessageBox.Message.Alert(Page, "Record updated successfully!!!", "str")
                            lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED"
                            lblAlert.Text = ""
                        End If
                    End If
                End If

                conn.Close()
                If txtMode.Text = "NEW" Then
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "USERACCESS", ddlAssetGroup.Text, "ADD", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
                Else
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "USERACCESS", ddlAssetGroup.Text, "EDIT", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
                End If
            Catch ex As Exception
                MessageBox.Message.Alert(Page, "Error!!!", "str")
            End Try
            'EnableControls()
            txtMode.Text = ""


        End If

        SqlDataSource4.SelectCommand = "SELECT * FROM tblassetgroupaccess where GroupAccess = '" & lblGroupAuthority8.Text & "'"
        SqlDataSource4.DataBind()
        grdAssetGroup.DataBind()

        EnableAssetGroupAccessControls()

        'btnSaveLocationAccess.Enabled = False
        'btnAddAccessLocation.ForeColor = System.Drawing.Color.Gray

        'btnCancelLocationAccess.Enabled = False
        'btnAddAccessLocation.ForeColor = System.Drawing.Color.Gray


        '   MakeMeNull()
        If CheckIfExists() = True Then
            txtExists.Text = "True"
        Else
            txtExists.Text = "False"
        End If
    End Sub

    Protected Sub btnCancelAssetGroupAccess_Click(sender As Object, e As EventArgs) Handles btnCancelAssetGroupAccess.Click

        btnAddAssetGroupAccess.Enabled = True
        btnAddAssetGroupAccess.ForeColor = System.Drawing.Color.Black


        btnSaveAssetGroupAccess.Enabled = False
        btnSaveAssetGroupAccess.ForeColor = System.Drawing.Color.Gray

        btnCancelAssetGroupAccess.Enabled = False
        btnCancelAssetGroupAccess.ForeColor = System.Drawing.Color.Gray
        ddlAssetGroup.Enabled = False
    End Sub

    Protected Sub grdAssetGroup_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdAssetGroup.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='silver';this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#E4E4E4';")
            'e.Row.Attributes.Add("onmousedown", "this.style.backgroundColor='#738A9C';")
            'e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#738A9C';")

            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(grdAssetGroup, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Protected Sub grdAssetGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grdAssetGroup.SelectedIndexChanged
        If txtMode.Text = "Edit" Then
            lblAlert.Text = "CANNOT SELECT RECORD IN EDIT MODE. SAVE OR CANCEL TO PROCEED"
            Return
        End If


        Dim concatstr As String = ""

        If grdAssetGroup.SelectedRow.Cells(2).Text = "&nbsp;" Then
            ddlAssetGroup.SelectedIndex = 0
        Else
            'concatstr = Server.HtmlDecode(GridView3.SelectedRow.Cells(2).Text).ToString.Trim & " : " & Server.HtmlDecode(GridView3.SelectedRow.Cells(3).Text).ToString.Trim
            ddlAssetGroup.Text = Server.HtmlDecode(grdAssetGroup.SelectedRow.Cells(2).Text).ToString.Trim
        End If


        If grdAssetGroup.SelectedRow.Cells(1).Text = "&nbsp;" Then
            txtRcNoAssetGroupAccess.Text = "0"
        Else
            txtRcNoAssetGroupAccess.Text = Server.HtmlDecode(grdAssetGroup.SelectedRow.Cells(1).Text).ToString
        End If



        btnEditAssetGroupAccess.Enabled = True
        btnEditAssetGroupAccess.ForeColor = System.Drawing.Color.Black

    End Sub

    Protected Sub btnDeleteAssetGroupAccess_Click(sender As Object, e As EventArgs) Handles btnDeleteAssetGroupAccess.Click
        lblMessage.Text = ""
        If txtRcNoAssetGroupAccess.Text = "" Then
            '  MessageBox.Message.Alert(Page, "Select a record to delete!!!", "str")
            lblAlert.Text = "SELECT RECORD TO DELETE"
            Return

        End If
        lblMessage.Text = "ACTION: DELETE RECORD"

        Dim confirmValue As String = Request.Form("confirm_value")
        If confirmValue = "Yes" Then

            'If txtExists.Text = "True" Then
            '    ' MessageBox.Message.Alert(Page, "Record is in use, cannot be modified!!!", "str")
            '    lblAlert.Text = "RECORD IS IN USE, CANNOT BE DELETED"
            '    Return
            'End If


            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                'Dim command1 As MySqlCommand = New MySqlCommand

                'command1.CommandType = CommandType.Text

                'command1.CommandText = "SELECT * FROM tblGroupAccess where rcno=" & Convert.ToInt32(txtRcno.Text)
                'command1.Connection = conn

                'Dim dr As MySqlDataReader = command1.ExecuteReader()
                'Dim dt As New DataTable
                'dt.Load(dr)

                'If dt.Rows.Count > 0 Then

                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text

                Dim qry As String = "delete from tblAssetGroupAccess where rcno=" & Convert.ToInt32(txtRcNoAssetGroupAccess.Text)

                command.CommandText = qry
                command.Connection = conn

                command.ExecuteNonQuery()

                'Dim command2 As MySqlCommand = New MySqlCommand
                'command2.CommandType = CommandType.Text
                'command2.CommandText = "delete from tblGroupAccess where rcno=" & Convert.ToInt32(txtRcNo2.Text)
                'command2.Connection = conn
                'command2.ExecuteNonQuery()

                '  MessageBox.Message.Alert(Page, "Record deleted successfully!!!", "str")
                lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"
                'End If
                conn.Close()
                conn.Dispose()
                command.Dispose()
                ddlAssetGroup.SelectedIndex = 0
                '    txtLocationDescription.Text = ""
            Catch ex As Exception
                MessageBox.Message.Alert(Page, ex.ToString, "str")
            End Try
            EnableControls()


            SqlDataSource4.SelectCommand = "SELECT * FROM tblAssetGroupAccess where GroupAccess = '" & lblGroupAuthority8.Text & "'"
            SqlDataSource4.DataBind()
            grdAssetGroup.DataBind()

            'GridView3.DataSourceID = "SqlDataSource3"
            MakeMeNull()
            lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"
        End If
    End Sub
End Class


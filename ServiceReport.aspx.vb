
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html
Imports iTextSharp.text.html.simpleparser
Imports System.Drawing
Imports System.IO
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data



Partial Class ServiceReport
    Inherits System.Web.UI.Page


    Public Shared SReportFileName As String = ""
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim SvcRecordNo As String = Convert.ToString(Session("SvcRecordNo"))
        lblRecordNo.Text = SvcRecordNo

        RetrieveData()
        ' PrintData()
        Data()
        'SReportFileName = "C:\Users\Sasi\Downloads\a.pdf"
        'Dim print As String = "<script language=JavaScript>document.Pdf2.printAll()</script>"
        'ClientScript.RegisterStartupScript(Page.GetType, "Report", print)
       

    End Sub


    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Verifies that the control is rendered

    End Sub

    Protected Sub Data()

         Dim doc1 = New Document()
        Dim path As String = Server.MapPath("PDFs")
        '   Dim writer As PdfWriter = PdfWriter.GetInstance(doc1, New FileStream(path & Convert.ToString("/" + lblRecordNo.Text + ".pdf"), FileMode.Create))
        Dim writer As PdfWriter = PdfWriter.GetInstance(doc1, Response.OutputStream)
        'Dim bfTimes As BaseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, False)
        'Dim times As New iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)

        doc1.Open()

        Dim imagepath As String = Server.MapPath("Images")
        Using fs As New FileStream(imagepath + "/logo.png", FileMode.Open)
            Dim logo As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(System.Drawing.Image.FromStream(fs), System.Drawing.Imaging.ImageFormat.Png)

            logo.Alignment = iTextSharp.text.Image.TEXTWRAP Or iTextSharp.text.Image.ALIGN_RIGHT
            logo.ScaleToFit(150.0F, 25.0F)
            logo.IndentationLeft = 10.0F

            doc1.Add(logo)
        End Using

        'Text Data - First Line

        Dim normalFont = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 9)
        Dim boldFont = FontFactory.GetFont(BaseFont.TIMES_BOLD, 9)


        Dim phrase1 = New Phrase()
        phrase1.Add(New Chunk("Service Date        : ", boldFont))
        phrase1.Add(New Chunk(lblServiceDate.Text, normalFont))
        phrase1.Add(New Chunk("      Sch Type : ", boldFont))
        phrase1.Add(New Chunk(lblSchType.Text, normalFont))

        phrase1.Add(New Chunk("      Time In : ", boldFont))
        phrase1.Add(New Chunk(lblTimeIn.Text, normalFont))
        phrase1.Add(New Chunk("      Time Out : ", boldFont))
        phrase1.Add(New Chunk(lblTimeOut.Text, normalFont))
        doc1.Add(New Paragraph(phrase1))
        ' second line

        Dim phrase2 As New Phrase
        phrase2.Add(New Chunk("Customer ID       : ", boldFont))
        phrase2.Add(New Chunk(lblCustomerID.Text, normalFont))
        doc1.Add(New Paragraph(phrase2))

        ' third line

        Dim phrase3 As New Phrase

        phrase3.Add(New Chunk("Customer Name  : ", boldFont))
        phrase3.Add(New Chunk(lblCustomerName.Text, normalFont))

        Dim table As New PdfPTable(2)
        table.WidthPercentage = 100

        Dim cell As New PdfPCell()
        cell.AddElement(phrase3)
        cell.Padding = 0
        cell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT
        cell.Border = PdfCell.NO_BORDER

        table.AddCell(cell)

        table.AddCell(getCell("SERVICE REPORT", PdfPCell.ALIGN_RIGHT))

        doc1.Add(table)

        ' fourth line

        Dim phrase4 As New Phrase
        phrase4.Add(New Chunk("Service Address  : ", boldFont))
        phrase4.Add(New Chunk(lblSvcAddress.Text, normalFont))
        '  doc1.Add(New Paragraph(phrase4))

        Dim tablePhrase4 As New PdfPTable(2)
        tablePhrase4.WidthPercentage = 100

        Dim cellPhrase4 As New PdfPCell()
        cellPhrase4.AddElement(phrase4)
        cellPhrase4.Padding = 0
        cellPhrase4.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT
        cellPhrase4.Border = PdfCell.NO_BORDER

        tablePhrase4.AddCell(cellPhrase4)

        tablePhrase4.AddCell(getCell("Report No : " + lblRecordNo.Text, PdfPCell.ALIGN_RIGHT))
        'cellPhrase4.Border = 0
        'cellPhrase4.BorderColorBottom = New BaseColor(System.Drawing.Color.Black)
        'cellPhrase4.BorderWidthBottom = 1.0F


        doc1.Add(tablePhrase4)

        'fifth line
        Dim phrase5 As New Phrase
        phrase5.Add(New Chunk("Attention             : ", boldFont))
        phrase5.Add(New Chunk(lblAttention.Text, normalFont))
        doc1.Add(New Paragraph(phrase5))


        ' draw a line

        Dim line1 As New iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, iTextSharp.text.Color.BLACK, Element.ALIGN_LEFT, 1)
        doc1.Add(New Chunk(line1))

        'sixth line
        Dim phrase6 As New Phrase
        phrase6.Add(New Chunk("Contract No       : ", boldFont))
        phrase6.Add(New Chunk(lblContractNo.Text, normalFont))
        doc1.Add(New Paragraph(phrase6))

        'blank line
        doc1.Add(New Phrase(Environment.NewLine))

        'Target pest details

        Dim chunk As New Chunk("Target Pest and Details :", boldFont)
        chunk.SetUnderline(0.5F, -1.5F)
        doc1.Add(chunk)

        Dim phrase7 As New Phrase
        phrase7.Add(New Chunk(lblTargetPest.Text, normalFont))

        phrase7.Add(New Chunk(lblReason.Text, normalFont))
        doc1.Add(New Paragraph(lblTargetPest.Text, normalFont))
        doc1.Add(New Paragraph(lblReason.Text, normalFont))

        'blank line
        doc1.Add(New Phrase(Environment.NewLine))

        'Services performed
        Dim chunk1 As New Chunk("Services Performed :", boldFont)
        chunk1.SetUnderline(0.5F, -1.5F)
        doc1.Add(chunk1)
        doc1.Add(New Paragraph(lblAction.Text, normalFont))

        ' last line

        Dim pSign As New Paragraph
        Dim psign1 As New Paragraph
        Dim pSign2 As New Paragraph
        Dim pSign3 As New Paragraph

        pSign.Add(New Chunk(Environment.NewLine))
        pSign.Add(New Chunk(Environment.NewLine))

        'client sign
        'Dim conn As MySqlConnection = New MySqlConnection()

        'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        'conn.Open()
        'Dim command1 As MySqlCommand = New MySqlCommand

        'command1.CommandType = CommandType.Text

        'command1.CommandText = "SELECT * FROM tblservicerecord where recordno=@recordno"
        'command1.Parameters.AddWithValue("@recordno", lblRecordNo.Text)
        'command1.Connection = conn

        'Dim dr As MySqlDataReader = command1.ExecuteReader()
        'Dim dt As New DataTable
        'dt.Load(dr)

        'If dt.Rows.Count > 0 Then
        '    Dim bytes As [Byte]() = DirectCast(dt.Rows(0)("CustomerSign"), [Byte]())
        '    Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
        '    Dim image__1 As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(bytes)
        '    Dim imageChunk As New Chunk(image__1, 0, 0)
        '  pSign.Add(imageChunk)

        'End If



        Dim smallline1 As New iTextSharp.text.pdf.draw.LineSeparator(0.0F, 40.0F, iTextSharp.text.Color.BLACK, Element.ALIGN_LEFT, 1)
        '  doc1.Add(New Chunk(smallline1))

        psign1.Add(New Chunk(smallline1))
        pSign2.Add(New Chunk(lblClientSignName.Text, normalFont))
        pSign3.Add(New Chunk("Client's Name & Signature", boldFont))

        Dim tablelast As New PdfPTable(2)
        tablelast.WidthPercentage = 100

        Dim widths As Single() = New Single() {80, 20}
        tablelast.SetWidths(widths)
        '   tablelast.LockedWidth = True

        Dim cell1 As New PdfPCell()

        cell1.AddElement(pSign)
        cell1.AddElement(psign1)
        cell1.AddElement(pSign2)
        cell1.AddElement(pSign3)

        cell1.Padding = 0
        cell1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER
        cell1.Border = PdfCell.NO_BORDER


        'Dim cell2 As New PdfPCell()
        'cell2.AddElement(New Chunk(""))
        'cell2.Border = PdfCell.NO_BORDER

        Dim cell3 As New PdfPCell

        Dim pSvc As New Paragraph
        Dim pSvc1 As New Paragraph
        Dim pSvc2 As New Paragraph

        pSvc.Add(New Chunk("Serviced By", boldFont))
        pSvc1.Add(New Chunk(smallline1))
        pSvc2.Add(New Chunk(lblServicedBy.Text, normalFont))

        cell3.AddElement(pSvc)
        cell3.AddElement(pSvc1)
        cell3.AddElement(pSvc2)

        cell3.Padding = 0
        cell3.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT
        cell3.Border = PdfCell.NO_BORDER
        tablelast.AddCell(cell1)
        tablelast.AddCell(cell3)
        '    tablelast.AddCell(getCell("SERVICE REPORT", PdfPCell.ALIGN_RIGHT))

        doc1.Add(tablelast)
        'blank line
        doc1.Add(New Phrase(Environment.NewLine))

        Dim lastline As New iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, iTextSharp.text.Color.BLACK, Element.ALIGN_LEFT, 1)
        lastline.LineWidth = 2

        doc1.Add(lastline)
        doc1.Close()

        Response.ContentType = "application/pdf"
        Response.AddHeader("content-disposition", "attachment;filename=" + Server.MapPath("PDFs") + "\" + lblRecordNo.Text + ".pdf")
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Write(doc1)
        Response.[End]()
        '    End Using

    End Sub


    Public Function getCell(text As [String], alignment As Integer) As PdfPCell
        Dim boldFont = FontFactory.GetFont(BaseFont.TIMES_BOLD, 10)

        Dim cell As New PdfPCell(New Phrase(text, boldFont))
        cell.HorizontalAlignment = Element.ALIGN_RIGHT
        cell.Border = PdfCell.NO_BORDER
        Return cell
    End Function

    Protected Sub RetrieveData()
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()
        Dim command1 As MySqlCommand = New MySqlCommand

        command1.CommandType = CommandType.Text

        command1.CommandText = "SELECT * FROM tblservicerecord where recordno=@recordno"
        command1.Parameters.AddWithValue("@recordno", lblRecordNo.Text)
        command1.Connection = conn

        Dim dr As MySqlDataReader = command1.ExecuteReader()
        Dim dt As New DataTable
        dt.Load(dr)

        If dt.Rows.Count > 0 Then
           
            If dt.Rows(0)("ServiceDate").ToString = DBNull.Value.ToString Then
            Else
                lblServiceDate.Text = Convert.ToDateTime(dt.Rows(0)("ServiceDate")).ToString("dd/MM/yyyy")
            End If
            lblSchType.Text = dt.Rows(0)("ScheduleType").ToString
            lblTimeIn.Text = dt.Rows(0)("TimeIn").ToString
            lblTimeOut.Text = dt.Rows(0)("TimeOut").ToString
            lblCustomerID.Text = dt.Rows(0)("CustCode").ToString
            lblCustomerName.Text = dt.Rows(0)("CustName").ToString
            lblAttention.Text = dt.Rows(0)("Contact").ToString
            lblContractNo.Text = dt.Rows(0)("ContractNo").ToString

            lblClientSignName.Text = dt.Rows(0)("CustomerSignName").ToString

          

            'Service Record detail

            Dim command2 As MySqlCommand = New MySqlCommand

            command2.CommandType = CommandType.Text

            command2.CommandText = "SELECT * FROM tblservicerecorddet where recordno=@recordno"
            command2.Parameters.AddWithValue("@recordno", lblRecordNo.Text)
            command2.Connection = conn

            Dim dr1 As MySqlDataReader = command2.ExecuteReader()
            Dim dt1 As New DataTable
            dt1.Load(dr1)

            If dt1.Rows.Count > 0 Then
                lblTargetPest.Text = dt1.Rows(0)("Warranty").ToString
                lblReason.Text = dt1.Rows(0)("Reason").ToString
                lblAction.Text = dt1.Rows(0)("Action").ToString

            End If

            'Service Record Staff

            Dim command3 As MySqlCommand = New MySqlCommand

            command3.CommandType = CommandType.Text

            command3.CommandText = "SELECT * FROM tblservicerecordstaff where recordno=@recordno"
            command3.Parameters.AddWithValue("@recordno", lblRecordNo.Text)
            command3.Connection = conn

            Dim dr2 As MySqlDataReader = command3.ExecuteReader()
            Dim dt2 As New DataTable
            dt2.Load(dr2)

            If dt2.Rows.Count > 0 Then
                For i As Integer = 0 To dt2.Rows.Count - 1
                    If i = 0 Then
                        lblServicedBy.Text = dt2.Rows(i)("StaffID").ToString
                    Else
                        lblServicedBy.Text = lblServicedBy.Text + Environment.NewLine + dt2.Rows(i)("StaffID").ToString
                    End If

                Next

            End If
        End If
        conn.Close()

    End Sub

    Protected Sub PrintData()
        Using sw As New StringWriter()
            Using hw As New HtmlTextWriter(sw)
               Panel1.RenderControl(hw)
                Dim sr As New StringReader(sw.ToString())
                Dim pdfDoc As New Document
                'Dim pdfDoc As New Document(PageSize.A4, 10, 10, 10, 5)
                Dim htmlparser As New HTMLWorker(pdfDoc)
                PdfWriter.GetInstance(pdfDoc, Response.OutputStream)
                pdfDoc.Open()
                Dim imagepath As String = Server.MapPath("Images")
                Using fs As New FileStream(imagepath + "/logo.png", FileMode.Open)
                    Dim logo As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(System.Drawing.Image.FromStream(fs), System.Drawing.Imaging.ImageFormat.Png)
                   
                    logo.Alignment = iTextSharp.text.Image.TEXTWRAP Or iTextSharp.text.Image.ALIGN_RIGHT
                    logo.ScaleToFit(150.0F, 25.0F)
                    logo.IndentationLeft = 10.0F
               
                    pdfDoc.Add(logo)
                End Using

                htmlparser.Parse(sr)
                pdfDoc.Close()

                Response.ContentType = "application/pdf"
                Response.AddHeader("content-disposition", "attachment;filename=Report.pdf")
                Response.Cache.SetCacheability(HttpCacheability.NoCache)
                Response.Write(pdfDoc)
                Response.[End]()
            End Using
        End Using
    End Sub

    Protected Sub PrintData1()
        Dim attachment As String = "attachment; filename=" + lblRecordNo.Text + "a.pdf"
        Response.ClearContent()
        Response.AddHeader("content-disposition", attachment)
        Response.ContentType = "application/pdf"

        'Dim s_tw As New StringWriter()
        'Dim h_textw As New HtmlTextWriter(s_tw)
        'h_textw.AddStyleAttribute("font-name", "Calibri")
        'h_textw.AddStyleAttribute("font-size", "10pt")
        'h_textw.AddStyleAttribute("color", "Black")
        'Panel1.RenderControl(h_textw)
        'Name of the Panel

        Dim doc As New Document()
        doc = New Document(PageSize.A4, 5, 5, 15, 5)
        doc.Open()
        doc.Add(New Paragraph("GIF"))
        'Dim output = New MemoryStream()
        'Dim writer = PdfWriter.GetInstance(doc, output)

        '  Dim logo = iTextSharp.text.Image.GetInstance(New Uri("http://sita.bsproject.biz/Images/searchbutton.jpg"))
        'Dim imagepath As String = Server.MapPath("Images")
        'Using fs As New FileStream(imagepath + "/logo.png", FileMode.Open)
        '    Dim logo As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(System.Drawing.Image.FromStream(fs), System.Drawing.Imaging.ImageFormat.Png)
        '    '   logo.SetAbsolutePosition(0, 0)
        '    doc.Add(logo)
        'End Using



        'Dim logo = iTextSharp.text.Image.GetInstance("http://sita.bsproject.biz/Images/logo.png", System.Drawing.Imaging.ImageFormat.Png)
        'logo.SetAbsolutePosition(0, 0)
        'doc.Add(logo)
        'FontFactory.GetFont("Verdana", 80, iTextSharp.text.Color.RED)
        'PdfWriter.GetInstance(doc, Response.OutputStream)
        'doc.Open()
        'Dim s_tr As New StringReader(s_tw.ToString())
        'Dim html_worker As New HTMLWorker(doc)
        'html_worker.Parse(s_tr)

        'Dim over As PdfContentByte = writer.DirectContent
        'over.SaveState()
        'over.BeginText()
        'over.SetFontAndSize(BaseFont.CreateFont(), 9)
        ''  over.ShowTextAligned(Element.ALIGN_LEFT, s_tr, 10, 10, 0)
        'over.SetLineWidth(0.3F)
        'over.EndText()
        'over.RestoreState()




        doc.Close()
        Response.Write(doc)
        Response.End()

    End Sub
End Class

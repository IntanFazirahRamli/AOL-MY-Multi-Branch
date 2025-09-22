Imports System.Data
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.IO
Imports NPOI.SS.UserModel
Imports NPOI.XSSF.UserModel
Imports NPOI.HSSF.UserModel


Partial Class RV_Select_Portfolio
    Inherits System.Web.UI.Page


    '    Private Sub PopulateTblMovementrptNEW(conn As MySqlConnection, startdate As DateTime, enddate As DateTime)

    '        '''''''''''''''''

    '        Dim qrysel As String = ""
    '        Dim qrysel1 As String = ""

    '        If ddlCompanyGrp.Text = "-1" Then
    '        Else
    '            qrysel = qrysel + " and companygroup= '" + ddlCompanyGrp.Text + "'"
    '            qrysel1 = qrysel1 + " and rev.companygroup= '" + ddlCompanyGrp.Text + "'"

    '        End If
    '        If ddlContractGroup.Text = "-1" Then
    '        Else
    '            qrysel = qrysel + " and contractgroup= '" + ddlContractGroup.Text + "'"
    '            qrysel1 = qrysel1 + " and contractgroup= '" + ddlContractGroup.Text + "'"

    '        End If
    '        'If ddlCategory.Text = "-1" Then
    '        'Else
    '        '    qrysel = qrysel + " and categoryid= '" + ddlCategory.Text + "'"
    '        '    qrysel1 = qrysel1 + " and categoryid= '" + ddlCategory.Text + "'"

    '        'End If
    '        'If ddlSalesMan.Text = "-1" Then
    '        'Else
    '        '    qrysel = qrysel + " and salesman= '" + ddlSalesMan.Text + "'"
    '        '    qrysel1 = qrysel1 + " and salesman= '" + ddlSalesMan.Text + "'"

    '        'End If
    '        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
    '            qrysel = qrysel + " and location in [" + Convert.ToString(Session("Branch")) + "]"
    '            qrysel1 = qrysel1 + " and location in [" + Convert.ToString(Session("Branch")) + "]"

    '        End If


    '        ''''''''''''''''''
    '        Dim selection As String = ""

    '        'Dim cmdOpening As MySqlCommand = New MySqlCommand
    '        Dim command As MySqlCommand = New MySqlCommand


    '        '''''''''''''
    '        Dim fieldscategoryTerminationDecrease As String
    '        Dim fieldscategoryOthers As String
    '        'Dim fields As String
    '        Dim category As String
    '        Dim qry As String

    '        category = ""
    '        fieldscategoryTerminationDecrease = ""
    '        fieldscategoryOthers = ""
    '        qry = ""



    '        'Start: 1 New New Opening
    '        category = "OPENING PORTFOLIO"
    '        Dim cmdOpening As MySqlCommand = New MySqlCommand
    '        Dim sqlstrInsert As String
    '        sqlstrInsert = ""


    '        sqlstrInsert = "INSERT INTO tblmovementrpt(RptCategory,CustCode,CustName,Total, ContractGroup,CompanyGroup, AnnualValue,ContractNo,ContractStatus,Duration,DurationMS,AgreeValue,RecordLevel,AccountID,ReportName,CreatedBy,CreatedOn,ContractGroupCategory) "
    '        sqlstrInsert = sqlstrInsert + " SELECT '" & category & "', CustCode,CustName, tblcontract.AgreeValue , tblcontract.ContractGroup,CompanyGroup, vwcontractportfolio.PortfolioValue, tblContract.ContractNo, Status, tblContract.Duration,tblContract.DurationMS, tblcontract.AgreeValue, '01', AccountID, '" & rbtnSelect.SelectedValue.ToString & "', '" & txtCreatedBy.Text & "', @CreatedOn, tblcontract.CategoryID "
    '        sqlstrInsert = sqlstrInsert + " from tblcontract, tblcontractgroup, vwcontractportfolio  where 1 = 1 "
    '        sqlstrInsert = sqlstrInsert + " and tblContract.ContractGroup = tblcontractgroup.ContractGroup  "
    '        sqlstrInsert = sqlstrInsert + " and tblContract.ContractNo = vwcontractportfolio.ContractNo"
    '        sqlstrInsert = sqlstrInsert + " and tblcontractgroup.IncludeinPortfolio ='1'"
    '        sqlstrInsert = sqlstrInsert + " and vwcontractportfolio.PortfolioValue > 0 "
    '        sqlstrInsert = sqlstrInsert + " and (isnull(tblcontract.ActualEnd)  and (tblcontract.EndDate > @startdate)  and (tblcontract.StartDate < @startdate)) "
    '        sqlstrInsert = sqlstrInsert + " and tblContract.Status in ('O','P')  "

    '        sqlstrInsert = sqlstrInsert + qrysel

    '        cmdOpening.CommandType = CommandType.Text

    '        cmdOpening.CommandText = sqlstrInsert
    '        cmdOpening.Parameters.Clear()

    '        cmdOpening.Parameters.AddWithValue("@startdate", startdate.ToString("yyyy-MM-dd"))
    '        cmdOpening.Parameters.AddWithValue("@enddate", enddate.ToString("yyyy-MM-dd"))
    '        cmdOpening.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
    '        cmdOpening.Connection = conn
    '        cmdOpening.ExecuteNonQuery()



    '        'End :1 New New Opening

    '        'Start: 1 New New New
    '        category = "NEW"
    '        Dim cmdNew As MySqlCommand = New MySqlCommand
    '        Dim sqlstrNew As String
    '        sqlstrInsert = ""


    '        sqlstrNew = "INSERT INTO tblmovementrpt(RptCategory,CustCode,CustName,Total, ContractGroup,CompanyGroup,AnnualValue,ContractNo,ContractStatus,Duration,DurationMS,AgreeValue,RecordLevel,AccountID,ReportName,CreatedBy,CreatedOn,ContractGroupCategory) "
    '        sqlstrNew = sqlstrNew + " SELECT '" & category & "', CustCode,CustName, tblcontract.AgreeValue , tblcontract.ContractGroup,CompanyGroup, vwcontractportfolio.PortfolioValue, tblContract.ContractNo, Status, tblContract.Duration,tblContract.DurationMS, tblcontract.AgreeValue, '02', AccountID, '" & rbtnSelect.SelectedValue.ToString & "', '" & txtCreatedBy.Text & "', @CreatedOn,  tblcontract.CategoryID  "
    '        sqlstrNew = sqlstrNew + " from tblcontract, tblcontractgroup, vwcontractportfolio  where 1 = 1 "
    '        sqlstrNew = sqlstrNew + " and tblContract.ContractGroup = tblcontractgroup.ContractGroup  "
    '        sqlstrNew = sqlstrNew + " and tblContract.ContractNo = vwcontractportfolio.ContractNo"
    '        sqlstrNew = sqlstrNew + " and tblcontractgroup.IncludeinPortfolio ='1'"
    '        sqlstrNew = sqlstrNew + " and vwcontractportfolio.PortfolioValue > 0 "
    '        sqlstrNew = sqlstrNew + " and (isnull(tblcontract.ActualEnd) and (tblcontract.EndDate > @startdate)  and (tblcontract.StartDate >= @startdate) and (tblcontract.StartDate <= @enddate)) "
    '        sqlstrNew = sqlstrNew + " and tblContract.Status in ('O','P')  "
    '        sqlstrNew = sqlstrNew + " and tblContract.AgreementType = 'NEW'"

    '        sqlstrNew = sqlstrNew + qrysel

    '        cmdNew.CommandType = CommandType.Text

    '        cmdNew.CommandText = sqlstrNew
    '        cmdNew.Parameters.Clear()

    '        cmdNew.Parameters.AddWithValue("@startdate", startdate.ToString("yyyy-MM-dd"))
    '        cmdNew.Parameters.AddWithValue("@enddate", enddate.ToString("yyyy-MM-dd"))
    '        cmdNew.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
    '        cmdNew.Connection = conn
    '        cmdNew.ExecuteNonQuery()

    '        'End :1 New New New


    '        'Start: 1 New New Addition
    '        category = "ADDITIONS"
    '        Dim cmdAddition As MySqlCommand = New MySqlCommand
    '        Dim sqlstrAddition As String
    '        sqlstrInsert = ""


    '        sqlstrAddition = "INSERT INTO tblmovementrpt(RptCategory,CustCode,CustName,Total, ContractGroup,CompanyGroup,AnnualValue,ContractNo,ContractStatus,Duration,DurationMS,AgreeValue,RecordLevel,AccountID,ReportName,CreatedBy,CreatedOn,ContractGroupCategory) "
    '        sqlstrAddition = sqlstrAddition + " SELECT '" & category & "', CustCode,CustName, tblcontract.AgreeValue , tblcontract.ContractGroup,CompanyGroup, vwcontractportfolio.PortfolioValue, tblContract.ContractNo, Status, tblContract.Duration,tblContract.DurationMS, tblcontract.AgreeValue, '03', AccountID, '" & rbtnSelect.SelectedValue.ToString & "', '" & txtCreatedBy.Text & "', @CreatedOn,  tblcontract.CategoryID  "
    '        sqlstrAddition = sqlstrAddition + " from tblcontract, tblcontractgroup, vwcontractportfolio  where 1 = 1 "
    '        sqlstrAddition = sqlstrAddition + " and tblContract.ContractGroup = tblcontractgroup.ContractGroup  "
    '        sqlstrAddition = sqlstrAddition + " and tblContract.ContractNo = vwcontractportfolio.ContractNo"
    '        sqlstrAddition = sqlstrAddition + " and tblcontractgroup.IncludeinPortfolio ='1'"
    '        sqlstrAddition = sqlstrAddition + " and vwcontractportfolio.PortfolioValue > 0 "
    '        sqlstrAddition = sqlstrAddition + " and (isnull(tblcontract.ActualEnd) and (tblcontract.EndDate > @startdate)  and (tblcontract.StartDate >= @startdate) and (tblcontract.StartDate <= @enddate)) "
    '        sqlstrAddition = sqlstrAddition + " and tblContract.Status in ('O','P')  "
    '        sqlstrAddition = sqlstrAddition + " and tblContract.AgreementType = 'ADDITION'"

    '        sqlstrAddition = sqlstrAddition + qrysel

    '        cmdAddition.CommandType = CommandType.Text

    '        cmdAddition.CommandText = sqlstrAddition
    '        cmdAddition.Parameters.Clear()

    '        cmdAddition.Parameters.AddWithValue("@startdate", startdate.ToString("yyyy-MM-dd"))
    '        cmdAddition.Parameters.AddWithValue("@enddate", enddate.ToString("yyyy-MM-dd"))
    '        cmdAddition.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
    '        cmdAddition.Connection = conn
    '        cmdAddition.ExecuteNonQuery()

    '        'End :1 New New Addition


    '        'Start: 1 New New Termination
    '        category = "TERMINATIONS"
    '        Dim cmdTermination As MySqlCommand = New MySqlCommand
    '        Dim sqlstrTermination As String
    '        sqlstrTermination = ""


    '        sqlstrTermination = "INSERT INTO tblmovementrpt(RptCategory,CustCode,CustName,Total, ContractGroup,CompanyGroup,AnnualValue,ContractNo,ContractStatus,Duration,DurationMS,AgreeValue,RecordLevel,AccountID,ReportName,CreatedBy,CreatedOn,ContractGroupCategory) "
    '        sqlstrTermination = sqlstrTermination + " SELECT '" & category & "', CustCode,CustName, tblcontract.AgreeValue * (-1) , tblcontract.ContractGroup,CompanyGroup, vwcontractportfolio.PortfolioValue * (-1), tblContract.ContractNo, Status, tblContract.Duration,tblContract.DurationMS, tblcontract.AgreeValue * (-1), '05', AccountID, '" & rbtnSelect.SelectedValue.ToString & "', '" & txtCreatedBy.Text & "', @CreatedOn,  tblcontract.CategoryID  "
    '        sqlstrTermination = sqlstrTermination + " from tblcontract, tblcontractgroup, vwcontractportfolio  where 1 = 1 "
    '        sqlstrTermination = sqlstrTermination + " and tblContract.ContractGroup = tblcontractgroup.ContractGroup  "
    '        sqlstrTermination = sqlstrTermination + " and tblContract.ContractNo = vwcontractportfolio.ContractNo"
    '        sqlstrTermination = sqlstrTermination + " and tblcontractgroup.IncludeinPortfolio ='1'"
    '        sqlstrTermination = sqlstrTermination + " and vwcontractportfolio.PortfolioValue > 0 "
    '        sqlstrTermination = sqlstrTermination + " and ((tblcontract.ActualEnd >= @startdate) and (tblcontract.ActualEnd <= @enddate)  and (tblcontract.StartDate <= @enddate)) "
    '        sqlstrTermination = sqlstrTermination + " and ((tblcontract.Status = 'X')  OR (tblcontract.Status = 'T') or (tblcontract.Status = 'E')) and tblcontract.RenewalST <> 'R'  "
    '        'sqlstrTermination = sqlstrTermination + " and tblContract.AgreementType = 'ADDITION'"

    '        sqlstrTermination = sqlstrTermination + qrysel

    '        cmdTermination.CommandType = CommandType.Text

    '        cmdTermination.CommandText = sqlstrTermination
    '        cmdTermination.Parameters.Clear()

    '        cmdTermination.Parameters.AddWithValue("@startdate", startdate.ToString("yyyy-MM-dd"))
    '        cmdTermination.Parameters.AddWithValue("@enddate", enddate.ToString("yyyy-MM-dd"))
    '        cmdTermination.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
    '        cmdTermination.Connection = conn
    '        cmdTermination.ExecuteNonQuery()

    '        'End :1 New New Termination

    '        'where ((tblcontract.ActualEnd >= @startdate) and (tblcontract.ActualEnd <= @enddate)  and (tblcontract.StartDate <= @enddate)) and ((tblcontract.Status = 'X')  OR (tblcontract.Status = 'T') or (tblcontract.Status = 'E')) and tblcontract.RenewalST <> 'R'


    '        '        '1

    '        '        category = "OPENING PORTFOLIO"
    '        '        Dim cmdOpening As MySqlCommand = New MySqlCommand
    '        '        'Dim command As MySqlCommand = New MySqlCommand
    '        '        cmdOpening.Connection = conn
    '        '        cmdOpening.CommandType = CommandType.Text

    '        '        '(tblcontract.EndDate > @startdate)    and (tblcontract.ServiceStart < @startdate)
    '        '        qry = "select " + fieldscategoryOthers + " from tblcontract  where (isnull(tblcontract.ActualEnd)   and (tblcontract.EndDate > @startdate)  and (tblcontract.StartDate < @startdate))    and ((tblcontract.Status = 'O')  OR (tblcontract.Status = 'P')) AND PORTFOLIOYN=1 "
    '        '        qry = qry + qrysel

    '        '        cmdOpening.CommandText = qry

    '        '        If category <> "NETT GAIN" Or category <> "CLOSING PORTFOLIO" Then
    '        '            cmdOpening.Parameters.AddWithValue("@startdate", startdate.ToString("yyyy-MM-dd"))
    '        '            cmdOpening.Parameters.AddWithValue("@enddate", enddate.ToString("yyyy-MM-dd"))
    '        '        End If

    '        '        Dim drOpening As MySqlDataReader = cmdOpening.ExecuteReader()
    '        '        Dim dtOpening As New DataTable
    '        '        dtOpening.Load(drOpening)

    '        '        If dtOpening.Rows.Count > 0 Then
    '        '            'Dim command As MySqlCommand = New MySqlCommand
    '        '            command.Connection = conn

    '        '            For i As Long = 0 To dtOpening.Rows.Count - 1

    '        '                '''''

    '        '                Dim sqlstrIncludeInportfolio As String
    '        '                sqlstrIncludeInportfolio = ""

    '        '                sqlstrIncludeInportfolio = "SELECT Category, IncludeinPortfolio FROM tblcontractgroup where IncludeInPortfolio = '1' and contractgroup = '" & dtOpening.Rows(i)("ContractGroup") & "'"

    '        '                Dim commandIncludeInportfolio As MySqlCommand = New MySqlCommand

    '        '                Dim connIncludeInportfolio As MySqlConnection = New MySqlConnection()
    '        '                connIncludeInportfolio.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    '        '                connIncludeInportfolio.Open()

    '        '                commandIncludeInportfolio.CommandType = CommandType.Text
    '        '                commandIncludeInportfolio.CommandText = sqlstrIncludeInportfolio
    '        '                commandIncludeInportfolio.Connection = connIncludeInportfolio

    '        '                Dim drIncludeInportfolio As MySqlDataReader = commandIncludeInportfolio.ExecuteReader()
    '        '                Dim dtIncludeInportfolio As New DataTable
    '        '                dtIncludeInportfolio.Load(drIncludeInportfolio)

    '        '                connIncludeInportfolio.Close()
    '        '                commandIncludeInportfolio.Dispose()

    '        '                If dtIncludeInportfolio.Rows.Count = 0 Then

    '        '                    GoTo nextrec
    '        '                End If

    '        '                '''''

    '        '                If category = "NETT GAIN" Or category = "CLOSING PORTFOLIO" Then
    '        '                    command.CommandType = CommandType.Text

    '        '                    command.CommandText = "INSERT INTO tblmovementrpt(RptCategory,CustCode,CustName,Total,ContractGroup,CompanyGroup,AnnualValue,ContractNo,ServStartDate,EndDate,ActualEnd,ContractStatus,Duration,DurationMS,AgreeValue,RecordLevel,AccountID,ReportName,CreatedBy,CreatedOn,ContractGroupCategory)VALUES(@RptCategory,@CustCode,@CustName,@Total,@ContractGroup,@CompanyGroup,@AnnualValue,@ContractNo,@ServStartDate,@EndDate,@ActualEnd,@ContractStatus,@Duration,@DurationMS,@AgreeValue,@RecordLevel,@AccountID,@ReportName,@CreatedBy,@CreatedOn,@ContractGroupCategory);"
    '        '                    command.Parameters.Clear()

    '        '                    command.Parameters.AddWithValue("@RptCategory", category)
    '        '                    command.Parameters.AddWithValue("@CustCode", "")
    '        '                    command.Parameters.AddWithValue("@CustName", "")
    '        '                    command.Parameters.AddWithValue("@ContractGroup", dtOpening.Rows(i)("ContractGroup"))
    '        '                    command.Parameters.AddWithValue("@CompanyGroup", dtOpening.Rows(i)("CompanyGroup"))
    '        '                    command.Parameters.AddWithValue("@ContractNo", "")
    '        '                    command.Parameters.AddWithValue("@ServStartDate", DBNull.Value)
    '        '                    command.Parameters.AddWithValue("@EndDate", DBNull.Value)
    '        '                    command.Parameters.AddWithValue("@ActualEnd", DBNull.Value)
    '        '                    command.Parameters.AddWithValue("@ContractStatus", "")
    '        '                    command.Parameters.AddWithValue("@Duration", 0)
    '        '                    command.Parameters.AddWithValue("@DurationMS", "")
    '        '                    command.Parameters.AddWithValue("@RecordLevel", "01")
    '        '                    command.Parameters.AddWithValue("@AccountID", "")
    '        '                    command.Parameters.AddWithValue("@ReportName", rbtnSelect.SelectedValue.ToString)
    '        '                    command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
    '        '                    command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
    '        '                    command.Parameters.AddWithValue("@Total", dtOpening.Rows(i)("SUM(AgreeValue)"))

    '        '                    command.Parameters.AddWithValue("@AnnualValue", dtOpening.Rows(i)("SUM(AnnualValue)"))

    '        '                    command.Parameters.AddWithValue("@AgreeValue", dtOpening.Rows(i)("SUM(AgreeValue)"))
    '        '                    command.Parameters.AddWithValue("@ContractGroupCategory", "")

    '        '                Else

    '        '                    Dim portfoliovalue As Double = 0


    '        '                    'If chkNew.Checked = False Then
    '        '                    '    If String.IsNullOrEmpty(dtOpening.Rows(i)("ViewPortfolioValue").ToString) = False Then
    '        '                    '        portfoliovalue = dtOpening.Rows(i)("ViewPortfolioValue")

    '        '                    '    Else
    '        '                    '        portfoliovalue = dtOpening.Rows(i)("PortfolioValue")

    '        '                    '    End If
    '        '                    'Else
    '        '                    '    portfoliovalue = dtOpening.Rows(i)("PortfolioValue")
    '        '                    'End If

    '        '                    portfoliovalue = dtOpening.Rows(i)("PortfolioValue")
    '        '                    command.CommandType = CommandType.Text
    '        '                    command.CommandText = "INSERT INTO tblmovementrpt(RptCategory,CustCode,CustName,Total,ContractGroup,CompanyGroup,AnnualValue,ContractNo,ServStartDate,EndDate,ActualEnd,ContractStatus,Duration,DurationMS,AgreeValue,RecordLevel,AccountID,ReportName,CreatedBy,CreatedOn,ContractGroupCategory)VALUES(@RptCategory,@CustCode,@CustName,@Total,@ContractGroup,@CompanyGroup,@AnnualValue,@ContractNo,@ServStartDate,@EndDate,@ActualEnd,@ContractStatus,@Duration,@DurationMS,@AgreeValue,@RecordLevel,@AccountID,@ReportName,@CreatedBy,@CreatedOn,@ContractGroupCategory);"
    '        '                    command.Parameters.Clear()

    '        '                    command.Parameters.AddWithValue("@RptCategory", category)
    '        '                    command.Parameters.AddWithValue("@CustCode", dtOpening.Rows(i)("CustCode"))
    '        '                    command.Parameters.AddWithValue("@CustName", dtOpening.Rows(i)("CustName"))
    '        '                    command.Parameters.AddWithValue("@ContractGroup", dtOpening.Rows(i)("ContractGroup"))
    '        '                    command.Parameters.AddWithValue("@CompanyGroup", dtOpening.Rows(i)("CompanyGroup"))
    '        '                    command.Parameters.AddWithValue("@ContractNo", dtOpening.Rows(i)("ContractNo"))
    '        '                    command.Parameters.AddWithValue("@ServStartDate", dtOpening.Rows(i)("ServiceStart"))
    '        '                    command.Parameters.AddWithValue("@EndDate", dtOpening.Rows(i)("EndDate"))
    '        '                    command.Parameters.AddWithValue("@ActualEnd", dtOpening.Rows(i)("ActualEnd"))
    '        '                    command.Parameters.AddWithValue("@ContractStatus", dtOpening.Rows(i)("Status"))
    '        '                    command.Parameters.AddWithValue("@Duration", dtOpening.Rows(i)("Duration"))
    '        '                    command.Parameters.AddWithValue("@DurationMS", dtOpening.Rows(i)("DurationMS"))
    '        '                    command.Parameters.AddWithValue("@RecordLevel", "01")
    '        '                    command.Parameters.AddWithValue("@AccountID", dtOpening.Rows(i)("AccountID"))
    '        '                    command.Parameters.AddWithValue("@ReportName", rbtnSelect.SelectedValue.ToString)
    '        '                    command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
    '        '                    command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
    '        '                    command.Parameters.AddWithValue("@Total", dtOpening.Rows(i)("AgreeValue"))

    '        '                    command.Parameters.AddWithValue("@AnnualValue", portfoliovalue)

    '        '                    command.Parameters.AddWithValue("@AgreeValue", dtOpening.Rows(i)("AgreeValue"))
    '        '                    command.Parameters.AddWithValue("@ContractGroupCategory", dtOpening.Rows(i)("CategoryID"))

    '        '                End If

    '        '                command.Connection = conn

    '        '                command.ExecuteNonQuery()


    '        'nextrec:
    '        '            Next

    '        '        End If

    '        '        cmdOpening.Dispose()
    '        '        dtOpening.Dispose()
    '        '        drOpening.Close()


    '        '        '2

    '        '        ''''''''''''''''

    '        '        category = "NEW"
    '        '        qry = ""

    '        '        Dim cmdNew As MySqlCommand = New MySqlCommand
    '        '        'Dim command As MySqlCommand = New MySqlCommand
    '        '        cmdNew.Connection = conn
    '        '        cmdNew.CommandType = CommandType.Text

    '        '        '(isnull(tblcontract.ActualEnd) and (tblcontract.EndDate > @startdate)  and (tblcontract.ServiceStart >= @startdate) and (tblcontract.ServiceStart <= @enddate)
    '        '        qry = "select " + fieldscategoryOthers + " from tblcontract  where (isnull(tblcontract.ActualEnd) and (tblcontract.EndDate > @startdate)  and (tblcontract.StartDate >= @startdate) and (tblcontract.StartDate <= @enddate)) and ((tblcontract.Status = 'O')  OR (tblcontract.Status = 'P')) AND PORTFOLIOYN=1 and AgreementType = 'NEW'"
    '        '        qry = qry + qrysel

    '        '        cmdNew.CommandText = qry

    '        '        If category <> "NETT GAIN" Or category <> "CLOSING PORTFOLIO" Then
    '        '            cmdNew.Parameters.AddWithValue("@startdate", startdate.ToString("yyyy-MM-dd"))
    '        '            cmdNew.Parameters.AddWithValue("@enddate", enddate.ToString("yyyy-MM-dd"))
    '        '        End If

    '        '        Dim drNew As MySqlDataReader = cmdNew.ExecuteReader()
    '        '        Dim dtNew As New DataTable
    '        '        dtNew.Load(drNew)

    '        '        If dtNew.Rows.Count > 0 Then
    '        '            'Dim command As MySqlCommand = New MySqlCommand
    '        '            command.Connection = conn

    '        '            For i As Int16 = 0 To dtNew.Rows.Count - 1

    '        '                '''''

    '        '                Dim sqlstrIncludeInportfolio As String
    '        '                sqlstrIncludeInportfolio = ""

    '        '                sqlstrIncludeInportfolio = "SELECT Category, IncludeinPortfolio FROM tblcontractgroup where IncludeInPortfolio = '1' and contractgroup = '" & dtNew.Rows(i)("ContractGroup") & "'"

    '        '                Dim commandIncludeInportfolioNEW As MySqlCommand = New MySqlCommand

    '        '                Dim connIncludeInportfolioNEW As MySqlConnection = New MySqlConnection()
    '        '                connIncludeInportfolioNEW.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    '        '                connIncludeInportfolioNEW.Open()

    '        '                commandIncludeInportfolioNEW.CommandType = CommandType.Text
    '        '                commandIncludeInportfolioNEW.CommandText = sqlstrIncludeInportfolio
    '        '                commandIncludeInportfolioNEW.Connection = connIncludeInportfolioNEW

    '        '                Dim drIncludeInportfolioNEW As MySqlDataReader = commandIncludeInportfolioNEW.ExecuteReader()
    '        '                Dim dtIncludeInportfolioNEW As New DataTable
    '        '                dtIncludeInportfolioNEW.Load(drIncludeInportfolioNEW)

    '        '                connIncludeInportfolioNEW.Close()
    '        '                commandIncludeInportfolioNEW.Dispose()

    '        '                If dtIncludeInportfolioNEW.Rows.Count = 0 Then

    '        '                    GoTo nextrec1
    '        '                End If

    '        '                '''''

    '        '                If category = "NETT GAIN" Or category = "CLOSING PORTFOLIO" Then
    '        '                    command.CommandType = CommandType.Text

    '        '                    command.CommandText = "INSERT INTO tblmovementrpt(RptCategory,CustCode,CustName,Total,ContractGroup,CompanyGroup,AnnualValue,ContractNo,ServStartDate,EndDate,ActualEnd,ContractStatus,Duration,DurationMS,AgreeValue,RecordLevel,AccountID,ReportName,CreatedBy,CreatedOn,ContractGroupCategory)VALUES(@RptCategory,@CustCode,@CustName,@Total,@ContractGroup,@CompanyGroup,@AnnualValue,@ContractNo,@ServStartDate,@EndDate,@ActualEnd,@ContractStatus,@Duration,@DurationMS,@AgreeValue,@RecordLevel,@AccountID,@ReportName,@CreatedBy,@CreatedOn,@ContractGroupCategory);"
    '        '                    command.Parameters.Clear()

    '        '                    command.Parameters.AddWithValue("@RptCategory", category)
    '        '                    command.Parameters.AddWithValue("@CustCode", "")
    '        '                    command.Parameters.AddWithValue("@CustName", "")
    '        '                    command.Parameters.AddWithValue("@ContractGroup", dtNew.Rows(i)("ContractGroup"))
    '        '                    command.Parameters.AddWithValue("@CompanyGroup", dtNew.Rows(i)("CompanyGroup"))
    '        '                    command.Parameters.AddWithValue("@ContractNo", "")
    '        '                    command.Parameters.AddWithValue("@ServStartDate", DBNull.Value)
    '        '                    command.Parameters.AddWithValue("@EndDate", DBNull.Value)
    '        '                    command.Parameters.AddWithValue("@ActualEnd", DBNull.Value)
    '        '                    command.Parameters.AddWithValue("@ContractStatus", "")
    '        '                    command.Parameters.AddWithValue("@Duration", 0)
    '        '                    command.Parameters.AddWithValue("@DurationMS", "")
    '        '                    command.Parameters.AddWithValue("@RecordLevel", "09")
    '        '                    command.Parameters.AddWithValue("@AccountID", "")
    '        '                    command.Parameters.AddWithValue("@ReportName", rbtnSelect.SelectedValue.ToString)
    '        '                    command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
    '        '                    command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
    '        '                    command.Parameters.AddWithValue("@Total", dtNew.Rows(i)("SUM(AgreeValue)"))

    '        '                    command.Parameters.AddWithValue("@AnnualValue", dtNew.Rows(i)("SUM(AnnualValue)"))

    '        '                    command.Parameters.AddWithValue("@AgreeValue", dtNew.Rows(i)("SUM(AgreeValue)"))
    '        '                    command.Parameters.AddWithValue("@ContractGroupCategory", "")

    '        '                Else

    '        '                    Dim portfoliovalue As Double = 0

    '        '                    portfoliovalue = dtNew.Rows(i)("PortfolioValue")
    '        '                    command.CommandType = CommandType.Text
    '        '                    command.CommandText = "INSERT INTO tblmovementrpt(RptCategory,CustCode,CustName,Total,ContractGroup,CompanyGroup,AnnualValue,ContractNo,ServStartDate,EndDate,ActualEnd,ContractStatus,Duration,DurationMS,AgreeValue,RecordLevel,AccountID,ReportName,CreatedBy,CreatedOn,ContractGroupCategory)VALUES(@RptCategory,@CustCode,@CustName,@Total,@ContractGroup,@CompanyGroup,@AnnualValue,@ContractNo,@ServStartDate,@EndDate,@ActualEnd,@ContractStatus,@Duration,@DurationMS,@AgreeValue,@RecordLevel,@AccountID,@ReportName,@CreatedBy,@CreatedOn,@ContractGroupCategory);"
    '        '                    command.Parameters.Clear()

    '        '                    command.Parameters.AddWithValue("@RptCategory", category)
    '        '                    command.Parameters.AddWithValue("@CustCode", dtNew.Rows(i)("CustCode"))
    '        '                    command.Parameters.AddWithValue("@CustName", dtNew.Rows(i)("CustName"))
    '        '                    command.Parameters.AddWithValue("@ContractGroup", dtNew.Rows(i)("ContractGroup"))
    '        '                    command.Parameters.AddWithValue("@CompanyGroup", dtNew.Rows(i)("CompanyGroup"))
    '        '                    command.Parameters.AddWithValue("@ContractNo", dtNew.Rows(i)("ContractNo"))
    '        '                    command.Parameters.AddWithValue("@ServStartDate", dtNew.Rows(i)("ServiceStart"))
    '        '                    command.Parameters.AddWithValue("@EndDate", dtNew.Rows(i)("EndDate"))
    '        '                    command.Parameters.AddWithValue("@ActualEnd", dtNew.Rows(i)("ActualEnd"))
    '        '                    command.Parameters.AddWithValue("@ContractStatus", dtNew.Rows(i)("Status"))
    '        '                    command.Parameters.AddWithValue("@Duration", dtNew.Rows(i)("Duration"))
    '        '                    command.Parameters.AddWithValue("@DurationMS", dtNew.Rows(i)("DurationMS"))
    '        '                    command.Parameters.AddWithValue("@RecordLevel", "09")
    '        '                    command.Parameters.AddWithValue("@AccountID", dtNew.Rows(i)("AccountID"))
    '        '                    command.Parameters.AddWithValue("@ReportName", rbtnSelect.SelectedValue.ToString)
    '        '                    command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
    '        '                    command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
    '        '                    command.Parameters.AddWithValue("@Total", dtNew.Rows(i)("AgreeValue"))

    '        '                    command.Parameters.AddWithValue("@AnnualValue", portfoliovalue)

    '        '                    command.Parameters.AddWithValue("@AgreeValue", dtNew.Rows(i)("AgreeValue"))
    '        '                    command.Parameters.AddWithValue("@ContractGroupCategory", dtNew.Rows(i)("CategoryID"))

    '        '                End If

    '        '                command.Connection = conn

    '        '                command.ExecuteNonQuery()


    '        'nextrec1:
    '        '            Next

    '        '        End If

    '        '        cmdNew.Dispose()
    '        '        dtNew.Dispose()
    '        '        drNew.Close()



    '        '        '3

    '        '        ''''''''''''''''

    '        '        category = "ADDITION"
    '        '        qry = ""

    '        '        Dim cmdAddition As MySqlCommand = New MySqlCommand
    '        '        'Dim command As MySqlCommand = New MySqlCommand
    '        '        cmdAddition.Connection = conn
    '        '        cmdAddition.CommandType = CommandType.Text

    '        '        '(isnull(tblcontract.ActualEnd) and (tblcontract.EndDate > @startdate)  and (tblcontract.ServiceStart >= @startdate) and (tblcontract.ServiceStart <= @enddate)
    '        '        qry = "select " + fieldscategoryOthers + " from tblcontract  where (isnull(tblcontract.ActualEnd) and (tblcontract.EndDate > @startdate)  and (tblcontract.StartDate >= @startdate) and (tblcontract.StartDate <= @enddate)) and ((tblcontract.Status = 'O')  OR (tblcontract.Status = 'P')) AND PORTFOLIOYN=1 and AgreementType = 'ADDITION'"
    '        '        qry = qry + qrysel

    '        '        cmdAddition.CommandText = qry

    '        '        If category <> "NETT GAIN" Or category <> "CLOSING PORTFOLIO" Then
    '        '            cmdAddition.Parameters.AddWithValue("@startdate", startdate.ToString("yyyy-MM-dd"))
    '        '            cmdAddition.Parameters.AddWithValue("@enddate", enddate.ToString("yyyy-MM-dd"))
    '        '        End If

    '        '        Dim drAddition As MySqlDataReader = cmdAddition.ExecuteReader()
    '        '        Dim dtAddition As New DataTable
    '        '        dtAddition.Load(drAddition)

    '        '        If dtAddition.Rows.Count > 0 Then
    '        '            'Dim command As MySqlCommand = New MySqlCommand
    '        '            command.Connection = conn

    '        '            For i As Int16 = 0 To dtAddition.Rows.Count - 1

    '        '                '''''

    '        '                Dim sqlstrIncludeInportfolio As String
    '        '                sqlstrIncludeInportfolio = ""

    '        '                sqlstrIncludeInportfolio = "SELECT Category, IncludeinPortfolio FROM tblcontractgroup where IncludeInPortfolio = '1' and contractgroup = '" & dtAddition.Rows(i)("ContractGroup") & "'"

    '        '                Dim commandIncludeInportfolio As MySqlCommand = New MySqlCommand

    '        '                Dim connIncludeInportfolio As MySqlConnection = New MySqlConnection()
    '        '                connIncludeInportfolio.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    '        '                connIncludeInportfolio.Open()

    '        '                commandIncludeInportfolio.CommandType = CommandType.Text
    '        '                commandIncludeInportfolio.CommandText = sqlstrIncludeInportfolio
    '        '                commandIncludeInportfolio.Connection = connIncludeInportfolio

    '        '                Dim drIncludeInportfolio As MySqlDataReader = commandIncludeInportfolio.ExecuteReader()
    '        '                Dim dtIncludeInportfolio As New DataTable
    '        '                dtIncludeInportfolio.Load(drIncludeInportfolio)

    '        '                connIncludeInportfolio.Close()
    '        '                commandIncludeInportfolio.Dispose()

    '        '                If dtIncludeInportfolio.Rows.Count = 0 Then

    '        '                    GoTo nextrecAddition
    '        '                End If

    '        '                '''''

    '        '                If category = "NETT GAIN" Or category = "CLOSING PORTFOLIO" Then
    '        '                    command.CommandType = CommandType.Text

    '        '                    command.CommandText = "INSERT INTO tblmovementrpt(RptCategory,CustCode,CustName,Total,ContractGroup,CompanyGroup,AnnualValue,ContractNo,ServStartDate,EndDate,ActualEnd,ContractStatus,Duration,DurationMS,AgreeValue,RecordLevel,AccountID,ReportName,CreatedBy,CreatedOn,ContractGroupCategory)VALUES(@RptCategory,@CustCode,@CustName,@Total,@ContractGroup,@CompanyGroup,@AnnualValue,@ContractNo,@ServStartDate,@EndDate,@ActualEnd,@ContractStatus,@Duration,@DurationMS,@AgreeValue,@RecordLevel,@AccountID,@ReportName,@CreatedBy,@CreatedOn,@ContractGroupCategory);"
    '        '                    command.Parameters.Clear()

    '        '                    command.Parameters.AddWithValue("@RptCategory", category)
    '        '                    command.Parameters.AddWithValue("@CustCode", "")
    '        '                    command.Parameters.AddWithValue("@CustName", "")
    '        '                    command.Parameters.AddWithValue("@ContractGroup", dtAddition.Rows(i)("ContractGroup"))
    '        '                    command.Parameters.AddWithValue("@CompanyGroup", dtAddition.Rows(i)("CompanyGroup"))
    '        '                    command.Parameters.AddWithValue("@ContractNo", "")
    '        '                    command.Parameters.AddWithValue("@ServStartDate", DBNull.Value)
    '        '                    command.Parameters.AddWithValue("@EndDate", DBNull.Value)
    '        '                    command.Parameters.AddWithValue("@ActualEnd", DBNull.Value)
    '        '                    command.Parameters.AddWithValue("@ContractStatus", "")
    '        '                    command.Parameters.AddWithValue("@Duration", 0)
    '        '                    command.Parameters.AddWithValue("@DurationMS", "")
    '        '                    command.Parameters.AddWithValue("@RecordLevel", "02")
    '        '                    command.Parameters.AddWithValue("@AccountID", "")
    '        '                    command.Parameters.AddWithValue("@ReportName", rbtnSelect.SelectedValue.ToString)
    '        '                    command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
    '        '                    command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
    '        '                    command.Parameters.AddWithValue("@Total", dtAddition.Rows(i)("SUM(AgreeValue)"))

    '        '                    command.Parameters.AddWithValue("@AnnualValue", dtAddition.Rows(i)("SUM(AnnualValue)"))

    '        '                    command.Parameters.AddWithValue("@AgreeValue", dtAddition.Rows(i)("SUM(AgreeValue)"))
    '        '                    command.Parameters.AddWithValue("@ContractGroupCategory", "")

    '        '                Else

    '        '                    Dim portfoliovalue As Double = 0

    '        '                    portfoliovalue = dtAddition.Rows(i)("PortfolioValue")
    '        '                    command.CommandType = CommandType.Text
    '        '                    command.CommandText = "INSERT INTO tblmovementrpt(RptCategory,CustCode,CustName,Total,ContractGroup,CompanyGroup,AnnualValue,ContractNo,ServStartDate,EndDate,ActualEnd,ContractStatus,Duration,DurationMS,AgreeValue,RecordLevel,AccountID,ReportName,CreatedBy,CreatedOn,ContractGroupCategory)VALUES(@RptCategory,@CustCode,@CustName,@Total,@ContractGroup,@CompanyGroup,@AnnualValue,@ContractNo,@ServStartDate,@EndDate,@ActualEnd,@ContractStatus,@Duration,@DurationMS,@AgreeValue,@RecordLevel,@AccountID,@ReportName,@CreatedBy,@CreatedOn,@ContractGroupCategory);"
    '        '                    command.Parameters.Clear()

    '        '                    command.Parameters.AddWithValue("@RptCategory", category)
    '        '                    command.Parameters.AddWithValue("@CustCode", dtAddition.Rows(i)("CustCode"))
    '        '                    command.Parameters.AddWithValue("@CustName", dtAddition.Rows(i)("CustName"))
    '        '                    command.Parameters.AddWithValue("@ContractGroup", dtAddition.Rows(i)("ContractGroup"))
    '        '                    command.Parameters.AddWithValue("@CompanyGroup", dtAddition.Rows(i)("CompanyGroup"))
    '        '                    command.Parameters.AddWithValue("@ContractNo", dtAddition.Rows(i)("ContractNo"))
    '        '                    command.Parameters.AddWithValue("@ServStartDate", dtAddition.Rows(i)("ServiceStart"))
    '        '                    command.Parameters.AddWithValue("@EndDate", dtAddition.Rows(i)("EndDate"))
    '        '                    command.Parameters.AddWithValue("@ActualEnd", dtAddition.Rows(i)("ActualEnd"))
    '        '                    command.Parameters.AddWithValue("@ContractStatus", dtAddition.Rows(i)("Status"))
    '        '                    command.Parameters.AddWithValue("@Duration", dtAddition.Rows(i)("Duration"))
    '        '                    command.Parameters.AddWithValue("@DurationMS", dtAddition.Rows(i)("DurationMS"))
    '        '                    command.Parameters.AddWithValue("@RecordLevel", "02")
    '        '                    command.Parameters.AddWithValue("@AccountID", dtAddition.Rows(i)("AccountID"))
    '        '                    command.Parameters.AddWithValue("@ReportName", rbtnSelect.SelectedValue.ToString)
    '        '                    command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
    '        '                    command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
    '        '                    command.Parameters.AddWithValue("@Total", dtAddition.Rows(i)("AgreeValue"))

    '        '                    command.Parameters.AddWithValue("@AnnualValue", portfoliovalue)

    '        '                    command.Parameters.AddWithValue("@AgreeValue", dtAddition.Rows(i)("AgreeValue"))
    '        '                    command.Parameters.AddWithValue("@ContractGroupCategory", dtAddition.Rows(i)("CategoryID"))

    '        '                End If

    '        '                command.Connection = conn

    '        '                command.ExecuteNonQuery()


    '        'nextrecAddition:
    '        '            Next

    '        '        End If

    '        '        cmdAddition.Dispose()
    '        '        dtAddition.Dispose()
    '        '        drAddition.Close()

    '        ''''''''''''''''''''4


    '        '4

    '        ''''''''''''''''


    '        qry = ""
    '        Dim IsIncreaseOrDecrease As String
    '        IsIncreaseOrDecrease = ""

    '        Dim cmdIncreaseDecrease As MySqlCommand = New MySqlCommand
    '        'Dim command As MySqlCommand = New MySqlCommand
    '        cmdIncreaseDecrease.Connection = conn
    '        cmdIncreaseDecrease.CommandType = CommandType.Text

    '        'If category = "TERMINATIONS" Or category = "DECREASES" Then
    '        '    fields = "tblcontract.ContractNo AS ContractNo,tblcontract.Status AS Status,tblcontract.ContactType AS ContactType,tblcontract.CustCode AS CustCode,tblcontract.CustName AS CustName,tblcontract.CustAddr AS CustAddr,tblcontract.ContractDate AS ContractDate,-tblcontract.AgreeValue AS AgreeValue,tblcontract.Duration AS Duration,tblcontract.DurationMs AS DurationMs,tblcontract.StartDate AS StartDate,tblcontract.EndDate AS EndDate,tblcontract.ActualEnd AS ActualEnd,tblcontract.OContractNo AS OContractNo,tblcontract.RenewalSt AS RenewalSt,tblcontract.RenewalDate AS RenewalDate,tblcontract.ContractGroup AS ContractGroup,tblcontract.RenewalContractNo AS RenewalContractNo,tblcontract.ServiceStart AS ServiceStart,tblcontract.ServiceEnd AS ServiceEnd,tblcontract.ServiceFrequence AS ServiceFrequence,tblcontract.ContractValue AS ContractValue,tblcontract.MainContractNo AS MainContractNo,tblcontract.CompanyGroup AS CompanyGroup,tblcontract.ValuePerMonth AS ValuePerMonth,tblcontract.AccountID AS AccountID,tblcontract.PortfolioYN AS PortfolioYN,-tblcontract.PortfolioValue as PortfolioValue,-vw.PortfolioValue AS ViewPortfolioValue,tblcontract.ActualServiceStartDate AS ActualServiceStartDate,tblcontract.AgreementType AS AgreementType,tblcontract.CategoryID "
    '        'Else
    '        '    fields = "tblcontract.ContractNo AS ContractNo,tblcontract.Status AS Status,tblcontract.ContactType AS ContactType,tblcontract.CustCode AS CustCode,tblcontract.CustName AS CustName,tblcontract.CustAddr AS CustAddr,tblcontract.ContractDate AS ContractDate,tblcontract.AgreeValue AS AgreeValue,tblcontract.Duration AS Duration,tblcontract.DurationMs AS DurationMs,tblcontract.StartDate AS StartDate,tblcontract.EndDate AS EndDate,tblcontract.ActualEnd AS ActualEnd,tblcontract.OContractNo AS OContractNo,tblcontract.RenewalSt AS RenewalSt,tblcontract.RenewalDate AS RenewalDate,tblcontract.ContractGroup AS ContractGroup,tblcontract.RenewalContractNo AS RenewalContractNo,tblcontract.ServiceStart AS ServiceStart,tblcontract.ServiceEnd AS ServiceEnd,tblcontract.ServiceFrequence AS ServiceFrequence,tblcontract.ContractValue AS ContractValue,tblcontract.MainContractNo AS MainContractNo,tblcontract.CompanyGroup AS CompanyGroup,tblcontract.ValuePerMonth AS ValuePerMonth,tblcontract.AccountID AS AccountID,tblcontract.PortfolioYN AS PortfolioYN,tblcontract.PortfolioValue AS PortfolioValue,vw.PortfolioValue AS ViewPortfolioValue,tblcontract.ActualServiceStartDate AS ActualServiceStartDate,tblcontract.AgreementType AS AgreementType,tblcontract.CategoryID "
    '        'End If
    '        '(isnull(tblcontract.ActualEnd) and (tblcontract.EndDate > @startdate)  and (tblcontract.ServiceStart >= @startdate) and (tblcontract.ServiceStart <= @enddate)




    '        ''''''''''''
    '        Dim sqlstrIncreaseDecrease As String
    '        sqlstrIncreaseDecrease = ""

    '        sqlstrIncreaseDecrease = sqlstrIncreaseDecrease + " SELECT tblcontract.ContractNo AS ContractNo,tblcontract.Status AS Status,tblcontract.ContactType AS ContactType,tblcontract.CustCode AS CustCode,tblcontract.CustName AS CustName,tblcontract.CustAddr AS CustAddr,tblcontract.ContractDate AS ContractDate,tblcontract.AgreeValue AS AgreeValue,tblcontract.Duration AS Duration,tblcontract.DurationMs AS DurationMs,tblcontract.StartDate AS StartDate,tblcontract.EndDate AS EndDate,tblcontract.ActualEnd AS ActualEnd,tblcontract.OContractNo AS OContractNo,tblcontract.RenewalSt AS RenewalSt,tblcontract.RenewalDate AS RenewalDate,tblcontract.ContractGroup AS ContractGroup,tblcontract.RenewalContractNo AS RenewalContractNo,tblcontract.ServiceStart AS ServiceStart,tblcontract.ServiceEnd AS ServiceEnd,tblcontract.ServiceFrequence AS ServiceFrequence,tblcontract.ContractValue AS ContractValue,tblcontract.MainContractNo AS MainContractNo,tblcontract.CompanyGroup AS CompanyGroup,tblcontract.ValuePerMonth AS ValuePerMonth,tblcontract.AccountID AS AccountID,tblcontract.PortfolioYN AS PortfolioYN,vwcontractportfolio.PortfolioValue AS PortfolioValue,tblcontract.ActualServiceStartDate AS ActualServiceStartDate,tblcontract.AgreementType AS AgreementType,tblcontract.CategoryID  "
    '        sqlstrIncreaseDecrease = sqlstrIncreaseDecrease + " from tblcontract, tblcontractgroup, vwcontractportfolio  where 1 = 1 "
    '        sqlstrIncreaseDecrease = sqlstrIncreaseDecrease + " and tblContract.ContractGroup = tblcontractgroup.ContractGroup  "
    '        sqlstrIncreaseDecrease = sqlstrIncreaseDecrease + " and tblContract.ContractNo = vwcontractportfolio.ContractNo"
    '        sqlstrIncreaseDecrease = sqlstrIncreaseDecrease + " and tblcontractgroup.IncludeinPortfolio ='1'"
    '        sqlstrIncreaseDecrease = sqlstrIncreaseDecrease + " and vwcontractportfolio.PortfolioValue > 0 "
    '        sqlstrIncreaseDecrease = sqlstrIncreaseDecrease + " and (isnull(tblcontract.ActualEnd) and (tblcontract.EndDate > @startdate)  and (tblcontract.StartDate >= @startdate) and (tblcontract.StartDate <= @enddate)) "
    '        sqlstrIncreaseDecrease = sqlstrIncreaseDecrease + " and tblContract.Status in ('O','P')  "
    '        sqlstrIncreaseDecrease = sqlstrIncreaseDecrease + " and ((AgreementType = 'PRICE INCREASE') or (AgreementType = 'PRICE DECREASE') or (AgreementType = 'NO PRICE CHANGE') or (AgreementType = 'RENEWAL'))"



    '        '''''''''''''
    '        'fieldscategoryOthers = "tblcontract.ContractNo AS ContractNo,tblcontract.Status AS Status,tblcontract.ContactType AS ContactType,tblcontract.CustCode AS CustCode,tblcontract.CustName AS CustName,tblcontract.CustAddr AS CustAddr,tblcontract.ContractDate AS ContractDate,tblcontract.AgreeValue AS AgreeValue,tblcontract.Duration AS Duration,tblcontract.DurationMs AS DurationMs,tblcontract.StartDate AS StartDate,tblcontract.EndDate AS EndDate,tblcontract.ActualEnd AS ActualEnd,tblcontract.OContractNo AS OContractNo,tblcontract.RenewalSt AS RenewalSt,tblcontract.RenewalDate AS RenewalDate,tblcontract.ContractGroup AS ContractGroup,tblcontract.RenewalContractNo AS RenewalContractNo,tblcontract.ServiceStart AS ServiceStart,tblcontract.ServiceEnd AS ServiceEnd,tblcontract.ServiceFrequence AS ServiceFrequence,tblcontract.ContractValue AS ContractValue,tblcontract.MainContractNo AS MainContractNo,tblcontract.CompanyGroup AS CompanyGroup,tblcontract.ValuePerMonth AS ValuePerMonth,tblcontract.AccountID AS AccountID,tblcontract.PortfolioYN AS PortfolioYN,tblcontract.PortfolioValue AS PortfolioValue,vw.PortfolioValue AS ViewPortfolioValue,tblcontract.ActualServiceStartDate AS ActualServiceStartDate,tblcontract.AgreementType AS AgreementType,tblcontract.CategoryID "

    '        'qry = "select " + fieldscategoryOthers + " from tblcontract  where (isnull(tblcontract.ActualEnd) and (tblcontract.EndDate > @startdate)  and (tblcontract.StartDate >= @startdate) and (tblcontract.StartDate <= @enddate)) and ((tblcontract.Status = 'O')  or (tblcontract.Status = 'P')) AND PORTFOLIOYN=1 and ((AgreementType = 'PRICE INCREASE') or (AgreementType = 'PRICE DECREASE') or (AgreementType = 'NO PRICE CHANGE') or (AgreementType = 'RENEWAL'))"
    '        'qry = qry + qrysel

    '        cmdIncreaseDecrease.CommandText = sqlstrIncreaseDecrease

    '        If category <> "NETT GAIN" Or category <> "CLOSING PORTFOLIO" Then
    '            cmdIncreaseDecrease.Parameters.AddWithValue("@startdate", startdate.ToString("yyyy-MM-dd"))
    '            cmdIncreaseDecrease.Parameters.AddWithValue("@enddate", enddate.ToString("yyyy-MM-dd"))
    '        End If

    '        Dim drIncreaseDecrease As MySqlDataReader = cmdIncreaseDecrease.ExecuteReader()
    '        Dim dtIncreaseDecrease As New DataTable
    '        dtIncreaseDecrease.Load(drIncreaseDecrease)

    '        If dtIncreaseDecrease.Rows.Count > 0 Then
    '            'Dim command As MySqlCommand = New MySqlCommand
    '            command.Connection = conn

    '            For i As Int16 = 0 To dtIncreaseDecrease.Rows.Count - 1

    '                ' '''''

    '                'Dim sqlstrIncludeInportfolio As String
    '                'sqlstrIncludeInportfolio = ""

    '                'sqlstrIncludeInportfolio = "SELECT Category, IncludeinPortfolio FROM tblcontractgroup where IncludeInPortfolio = '1' and contractgroup = '" & dtIncreaseDecrease.Rows(i)("ContractGroup") & "'"

    '                'Dim commandIncludeInportfolio As MySqlCommand = New MySqlCommand

    '                'Dim connIncludeInportfolio As MySqlConnection = New MySqlConnection()
    '                'connIncludeInportfolio.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    '                'connIncludeInportfolio.Open()

    '                'commandIncludeInportfolio.CommandType = CommandType.Text
    '                'commandIncludeInportfolio.CommandText = sqlstrIncludeInportfolio
    '                'commandIncludeInportfolio.Connection = connIncludeInportfolio

    '                'Dim drIncludeInportfolio As MySqlDataReader = commandIncludeInportfolio.ExecuteReader()
    '                'Dim dtIncludeInportfolio As New DataTable
    '                'dtIncludeInportfolio.Load(drIncludeInportfolio)

    '                'connIncludeInportfolio.Close()
    '                'commandIncludeInportfolio.Dispose()

    '                'If dtIncludeInportfolio.Rows.Count = 0 Then

    '                '    GoTo nextrecIncreaseDecrease
    '                'End If

    '                ' '''''

    '                ' '''''

    '                If String.IsNullOrEmpty(dtIncreaseDecrease.Rows(i)("OContractNo")) = False Then

    '                    Dim sqlstrOContractNo As String
    '                    sqlstrOContractNo = ""

    '                    sqlstrOContractNo = "SELECT ContractNo, AgreeValue FROM tblcontract where ContractNo = '" & dtIncreaseDecrease.Rows(i)("OContractNo") & "'"

    '                    Dim commandOContractNo As MySqlCommand = New MySqlCommand

    '                    Dim connOContractNo As MySqlConnection = New MySqlConnection()
    '                    connOContractNo.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    '                    connOContractNo.Open()

    '                    commandOContractNo.CommandType = CommandType.Text
    '                    commandOContractNo.CommandText = sqlstrOContractNo
    '                    commandOContractNo.Connection = connOContractNo

    '                    Dim drOContractNo As MySqlDataReader = commandOContractNo.ExecuteReader()
    '                    Dim dtOContractNo As New DataTable
    '                    dtOContractNo.Load(drOContractNo)

    '                    connOContractNo.Close()
    '                    commandOContractNo.Dispose()

    '                    If dtOContractNo.Rows.Count > 0 Then
    '                        If dtIncreaseDecrease.Rows(i)("AgreeValue") > dtOContractNo.Rows(0)("AgreeValue") Then
    '                            IsIncreaseOrDecrease = "INCREASES"
    '                        ElseIf dtIncreaseDecrease.Rows(i)("AgreeValue") < dtOContractNo.Rows(0)("AgreeValue") Then
    '                            IsIncreaseOrDecrease = "DECREASES"
    '                        Else
    '                            IsIncreaseOrDecrease = "NO CHANGE"
    '                        End If

    '                    Else
    '                        IsIncreaseOrDecrease = "NO CHANGE"
    '                    End If
    '                Else
    '                    If dtIncreaseDecrease.Rows(i)("AgreementType") = "PRICE INCREASE" Then
    '                        IsIncreaseOrDecrease = "INCREASES"
    '                    ElseIf dtIncreaseDecrease.Rows(i)("AgreementType") = "PRICE DECREASE" Then
    '                        IsIncreaseOrDecrease = "DECREASES"
    '                    Else
    '                        IsIncreaseOrDecrease = "NO CHANGE"
    '                    End If
    '                End If
    '                '''''

    '                Dim portfoliovalue As Double = 0

    '                portfoliovalue = dtIncreaseDecrease.Rows(i)("PortfolioValue")
    '                command.CommandType = CommandType.Text
    '                command.CommandText = "INSERT INTO tblmovementrpt(RptCategory,CustCode,CustName,Total,ContractGroup,CompanyGroup,AnnualValue,ContractNo,ServStartDate,EndDate,ActualEnd,ContractStatus,Duration,DurationMS,AgreeValue,RecordLevel,AccountID,ReportName,CreatedBy,CreatedOn,ContractGroupCategory)VALUES(@RptCategory,@CustCode,@CustName,@Total,@ContractGroup,@CompanyGroup,@AnnualValue,@ContractNo,@ServStartDate,@EndDate,@ActualEnd,@ContractStatus,@Duration,@DurationMS,@AgreeValue,@RecordLevel,@AccountID,@ReportName,@CreatedBy,@CreatedOn,@ContractGroupCategory);"
    '                command.Parameters.Clear()

    '                If IsIncreaseOrDecrease = "INCREASES" Then
    '                    command.Parameters.AddWithValue("@RptCategory", IsIncreaseOrDecrease)
    '                    command.Parameters.AddWithValue("@RecordLevel", "04")
    '                    command.Parameters.AddWithValue("@AnnualValue", portfoliovalue)
    '                    command.Parameters.AddWithValue("@AgreeValue", dtIncreaseDecrease.Rows(i)("AgreeValue"))
    '                ElseIf IsIncreaseOrDecrease = "DECREASES" Then
    '                    command.Parameters.AddWithValue("@RptCategory", IsIncreaseOrDecrease)
    '                    command.Parameters.AddWithValue("@RecordLevel", "06")
    '                    command.Parameters.AddWithValue("@AnnualValue", portfoliovalue * (-1))
    '                    command.Parameters.AddWithValue("@AgreeValue", dtIncreaseDecrease.Rows(i)("AgreeValue") * (-1))
    '                ElseIf IsIncreaseOrDecrease = "NO CHANGE" Then
    '                    command.Parameters.AddWithValue("@RptCategory", "OPENING PORTFOLIO")
    '                    command.Parameters.AddWithValue("@RecordLevel", "01")
    '                    command.Parameters.AddWithValue("@AnnualValue", portfoliovalue)
    '                    command.Parameters.AddWithValue("@AgreeValue", dtIncreaseDecrease.Rows(i)("AgreeValue"))
    '                End If

    '                command.Parameters.AddWithValue("@CustCode", dtIncreaseDecrease.Rows(i)("CustCode"))
    '                command.Parameters.AddWithValue("@CustName", dtIncreaseDecrease.Rows(i)("CustName"))
    '                command.Parameters.AddWithValue("@ContractGroup", dtIncreaseDecrease.Rows(i)("ContractGroup"))
    '                command.Parameters.AddWithValue("@CompanyGroup", dtIncreaseDecrease.Rows(i)("CompanyGroup"))
    '                command.Parameters.AddWithValue("@ContractNo", dtIncreaseDecrease.Rows(i)("ContractNo"))
    '                command.Parameters.AddWithValue("@ServStartDate", dtIncreaseDecrease.Rows(i)("ServiceStart"))
    '                command.Parameters.AddWithValue("@EndDate", dtIncreaseDecrease.Rows(i)("EndDate"))
    '                command.Parameters.AddWithValue("@ActualEnd", dtIncreaseDecrease.Rows(i)("ActualEnd"))
    '                command.Parameters.AddWithValue("@ContractStatus", dtIncreaseDecrease.Rows(i)("Status"))
    '                command.Parameters.AddWithValue("@Duration", dtIncreaseDecrease.Rows(i)("Duration"))
    '                command.Parameters.AddWithValue("@DurationMS", dtIncreaseDecrease.Rows(i)("DurationMS"))
    '                'command.Parameters.AddWithValue("@RecordLevel", "02")
    '                command.Parameters.AddWithValue("@AccountID", dtIncreaseDecrease.Rows(i)("AccountID"))
    '                command.Parameters.AddWithValue("@ReportName", rbtnSelect.SelectedValue.ToString)
    '                command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
    '                command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
    '                command.Parameters.AddWithValue("@Total", dtIncreaseDecrease.Rows(i)("AgreeValue"))
    '                command.Parameters.AddWithValue("@ContractGroupCategory", dtIncreaseDecrease.Rows(i)("CategoryID"))

    '                command.Connection = conn

    '                command.ExecuteNonQuery()


    'nextrecIncreaseDecrease:
    '            Next

    '        End If

    '        cmdAddition.Dispose()
    '        'dtAddition.Dispose()
    '        'drAddition.Close()
    '        '''''''''''''''''''4




    '        '        '5

    '        '        ''''''''''''''''

    '        '        category = "TERMINATIONS"
    '        '        qry = ""

    '        '        Dim cmdTermination As MySqlCommand = New MySqlCommand
    '        '        'Dim command As MySqlCommand = New MySqlCommand
    '        '        cmdTermination.Connection = conn
    '        '        cmdTermination.CommandType = CommandType.Text

    '        '        fieldscategoryTerminationDecrease = "tblcontract.ContractNo AS ContractNo,tblcontract.Status AS Status,tblcontract.ContactType AS ContactType,tblcontract.CustCode AS CustCode,tblcontract.CustName AS CustName,tblcontract.CustAddr AS CustAddr,tblcontract.ContractDate AS ContractDate,-tblcontract.AgreeValue AS AgreeValue,tblcontract.Duration AS Duration,tblcontract.DurationMs AS DurationMs,tblcontract.StartDate AS StartDate,tblcontract.EndDate AS EndDate,tblcontract.ActualEnd AS ActualEnd,tblcontract.OContractNo AS OContractNo,tblcontract.RenewalSt AS RenewalSt,tblcontract.RenewalDate AS RenewalDate,tblcontract.ContractGroup AS ContractGroup,tblcontract.RenewalContractNo AS RenewalContractNo,tblcontract.ServiceStart AS ServiceStart,tblcontract.ServiceEnd AS ServiceEnd,tblcontract.ServiceFrequence AS ServiceFrequence,tblcontract.ContractValue AS ContractValue,tblcontract.MainContractNo AS MainContractNo,tblcontract.CompanyGroup AS CompanyGroup,tblcontract.ValuePerMonth AS ValuePerMonth,tblcontract.AccountID AS AccountID,tblcontract.PortfolioYN AS PortfolioYN,-tblcontract.PortfolioValue as PortfolioValue,tblcontract.ActualServiceStartDate AS ActualServiceStartDate,tblcontract.AgreementType AS AgreementType,tblcontract.CategoryID "


    '        '       qry = "select " + fieldscategoryTerminationDecrease + " from tblcontract  where ((tblcontract.ActualEnd >= @startdate) and (tblcontract.ActualEnd <= @enddate)  and (tblcontract.StartDate <= @enddate)) and ((tblcontract.Status = 'X')  OR (tblcontract.Status = 'T') or (tblcontract.Status = 'E')) and tblcontract.RenewalST <> 'R' AND PORTFOLIOYN=1"
    '        '        qry = qry + qrysel

    '        '        cmdTermination.CommandText = qry

    '        '        If category <> "NETT GAIN" Or category <> "CLOSING PORTFOLIO" Then
    '        '            cmdTermination.Parameters.AddWithValue("@startdate", startdate.ToString("yyyy-MM-dd"))
    '        '            cmdTermination.Parameters.AddWithValue("@enddate", enddate.ToString("yyyy-MM-dd"))
    '        '        End If

    '        '        Dim drTermination As MySqlDataReader = cmdTermination.ExecuteReader()
    '        '        Dim dtTermination As New DataTable
    '        '        dtTermination.Load(drTermination)

    '        '        If dtTermination.Rows.Count > 0 Then
    '        '            'Dim command As MySqlCommand = New MySqlCommand
    '        '            command.Connection = conn

    '        '            For i As Int16 = 0 To dtTermination.Rows.Count - 1

    '        '                '''''

    '        '                Dim sqlstrIncludeInportfolio As String
    '        '                sqlstrIncludeInportfolio = ""

    '        '                sqlstrIncludeInportfolio = "SELECT Category, IncludeinPortfolio FROM tblcontractgroup where IncludeInPortfolio = '1' and contractgroup = '" & dtTermination.Rows(i)("ContractGroup") & "'"

    '        '                Dim commandIncludeInportfolio As MySqlCommand = New MySqlCommand

    '        '                Dim connIncludeInportfolio As MySqlConnection = New MySqlConnection()
    '        '                connIncludeInportfolio.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    '        '                connIncludeInportfolio.Open()

    '        '                commandIncludeInportfolio.CommandType = CommandType.Text
    '        '                commandIncludeInportfolio.CommandText = sqlstrIncludeInportfolio
    '        '                commandIncludeInportfolio.Connection = connIncludeInportfolio

    '        '                Dim drIncludeInportfolio As MySqlDataReader = commandIncludeInportfolio.ExecuteReader()
    '        '                Dim dtIncludeInportfolio As New DataTable
    '        '                dtIncludeInportfolio.Load(drIncludeInportfolio)

    '        '                connIncludeInportfolio.Close()
    '        '                commandIncludeInportfolio.Dispose()

    '        '                If dtIncludeInportfolio.Rows.Count = 0 Then

    '        '                    GoTo nextrecTermination
    '        '                End If

    '        '                '''''

    '        '                If category = "NETT GAIN" Or category = "CLOSING PORTFOLIO" Then
    '        '                    command.CommandType = CommandType.Text

    '        '                    command.CommandText = "INSERT INTO tblmovementrpt(RptCategory,CustCode,CustName,Total,ContractGroup,CompanyGroup,AnnualValue,ContractNo,ServStartDate,EndDate,ActualEnd,ContractStatus,Duration,DurationMS,AgreeValue,RecordLevel,AccountID,ReportName,CreatedBy,CreatedOn,ContractGroupCategory)VALUES(@RptCategory,@CustCode,@CustName,@Total,@ContractGroup,@CompanyGroup,@AnnualValue,@ContractNo,@ServStartDate,@EndDate,@ActualEnd,@ContractStatus,@Duration,@DurationMS,@AgreeValue,@RecordLevel,@AccountID,@ReportName,@CreatedBy,@CreatedOn,@ContractGroupCategory);"
    '        '                    command.Parameters.Clear()

    '        '                    command.Parameters.AddWithValue("@RptCategory", category)
    '        '                    command.Parameters.AddWithValue("@CustCode", "")
    '        '                    command.Parameters.AddWithValue("@CustName", "")
    '        '                    command.Parameters.AddWithValue("@ContractGroup", dtTermination.Rows(i)("ContractGroup"))
    '        '                    command.Parameters.AddWithValue("@CompanyGroup", dtTermination.Rows(i)("CompanyGroup"))
    '        '                    command.Parameters.AddWithValue("@ContractNo", "")
    '        '                    command.Parameters.AddWithValue("@ServStartDate", DBNull.Value)
    '        '                    command.Parameters.AddWithValue("@EndDate", DBNull.Value)
    '        '                    command.Parameters.AddWithValue("@ActualEnd", DBNull.Value)
    '        '                    command.Parameters.AddWithValue("@ContractStatus", "")
    '        '                    command.Parameters.AddWithValue("@Duration", 0)
    '        '                    command.Parameters.AddWithValue("@DurationMS", "")
    '        '                    command.Parameters.AddWithValue("@RecordLevel", "02")
    '        '                    command.Parameters.AddWithValue("@AccountID", "")
    '        '                    command.Parameters.AddWithValue("@ReportName", rbtnSelect.SelectedValue.ToString)
    '        '                    command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
    '        '                    command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
    '        '                    command.Parameters.AddWithValue("@Total", dtTermination.Rows(i)("SUM(AgreeValue)"))

    '        '                    command.Parameters.AddWithValue("@AnnualValue", dtTermination.Rows(i)("SUM(AnnualValue)"))

    '        '                    command.Parameters.AddWithValue("@AgreeValue", dtTermination.Rows(i)("SUM(AgreeValue)"))
    '        '                    command.Parameters.AddWithValue("@ContractGroupCategory", "")

    '        '                Else

    '        '                    Dim portfoliovalue As Double = 0

    '        '                    portfoliovalue = dtTermination.Rows(i)("PortfolioValue")
    '        '                    command.CommandType = CommandType.Text
    '        '                    command.CommandText = "INSERT INTO tblmovementrpt(RptCategory,CustCode,CustName,Total,ContractGroup,CompanyGroup,AnnualValue,ContractNo,ServStartDate,EndDate,ActualEnd,ContractStatus,Duration,DurationMS,AgreeValue,RecordLevel,AccountID,ReportName,CreatedBy,CreatedOn,ContractGroupCategory)VALUES(@RptCategory,@CustCode,@CustName,@Total,@ContractGroup,@CompanyGroup,@AnnualValue,@ContractNo,@ServStartDate,@EndDate,@ActualEnd,@ContractStatus,@Duration,@DurationMS,@AgreeValue,@RecordLevel,@AccountID,@ReportName,@CreatedBy,@CreatedOn,@ContractGroupCategory);"
    '        '                    command.Parameters.Clear()

    '        '                    command.Parameters.AddWithValue("@RptCategory", category)
    '        '                    command.Parameters.AddWithValue("@CustCode", dtTermination.Rows(i)("CustCode"))
    '        '                    command.Parameters.AddWithValue("@CustName", dtTermination.Rows(i)("CustName"))
    '        '                    command.Parameters.AddWithValue("@ContractGroup", dtTermination.Rows(i)("ContractGroup"))
    '        '                    command.Parameters.AddWithValue("@CompanyGroup", dtTermination.Rows(i)("CompanyGroup"))
    '        '                    command.Parameters.AddWithValue("@ContractNo", dtTermination.Rows(i)("ContractNo"))
    '        '                    command.Parameters.AddWithValue("@ServStartDate", dtTermination.Rows(i)("ServiceStart"))
    '        '                    command.Parameters.AddWithValue("@EndDate", dtTermination.Rows(i)("EndDate"))
    '        '                    command.Parameters.AddWithValue("@ActualEnd", dtTermination.Rows(i)("ActualEnd"))
    '        '                    command.Parameters.AddWithValue("@ContractStatus", dtTermination.Rows(i)("Status"))
    '        '                    command.Parameters.AddWithValue("@Duration", dtTermination.Rows(i)("Duration"))
    '        '                    command.Parameters.AddWithValue("@DurationMS", dtTermination.Rows(i)("DurationMS"))
    '        '                    command.Parameters.AddWithValue("@RecordLevel", "05")
    '        '                    command.Parameters.AddWithValue("@AccountID", dtTermination.Rows(i)("AccountID"))
    '        '                    command.Parameters.AddWithValue("@ReportName", rbtnSelect.SelectedValue.ToString)
    '        '                    command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
    '        '                    command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
    '        '                    command.Parameters.AddWithValue("@Total", dtTermination.Rows(i)("AgreeValue"))

    '        '                    command.Parameters.AddWithValue("@AnnualValue", portfoliovalue)

    '        '                    command.Parameters.AddWithValue("@AgreeValue", dtTermination.Rows(i)("AgreeValue"))
    '        '                    command.Parameters.AddWithValue("@ContractGroupCategory", dtTermination.Rows(i)("CategoryID"))

    '        '                End If

    '        '                command.Connection = conn

    '        '                command.ExecuteNonQuery()


    '        'nextrecTermination:
    '        '            Next

    '        '        End If

    '        '        cmdTermination.Dispose()
    '        '        dtTermination.Dispose()
    '        '        drTermination.Close()



    '        ''''''''''''''''''

    '        '7


    '        ''''''''''''''''

    '        category = "NETT GAIN"
    '        qry = ""

    '        Dim cmdNettGain As MySqlCommand = New MySqlCommand
    '        'Dim command As MySqlCommand = New MySqlCommand
    '        cmdNettGain.Connection = conn
    '        cmdNettGain.CommandType = CommandType.Text

    '        'fieldscategoryTerminationDecrease = "tblcontract.ContractNo AS ContractNo,tblcontract.Status AS Status,tblcontract.ContactType AS ContactType,tblcontract.CustCode AS CustCode,tblcontract.CustName AS CustName,tblcontract.CustAddr AS CustAddr,tblcontract.ContractDate AS ContractDate,-tblcontract.AgreeValue AS AgreeValue,tblcontract.Duration AS Duration,tblcontract.DurationMs AS DurationMs,tblcontract.StartDate AS StartDate,tblcontract.EndDate AS EndDate,tblcontract.ActualEnd AS ActualEnd,tblcontract.OContractNo AS OContractNo,tblcontract.RenewalSt AS RenewalSt,tblcontract.RenewalDate AS RenewalDate,tblcontract.ContractGroup AS ContractGroup,tblcontract.RenewalContractNo AS RenewalContractNo,tblcontract.ServiceStart AS ServiceStart,tblcontract.ServiceEnd AS ServiceEnd,tblcontract.ServiceFrequence AS ServiceFrequence,tblcontract.ContractValue AS ContractValue,tblcontract.MainContractNo AS MainContractNo,tblcontract.CompanyGroup AS CompanyGroup,tblcontract.ValuePerMonth AS ValuePerMonth,tblcontract.AccountID AS AccountID,tblcontract.PortfolioYN AS PortfolioYN,-tblcontract.PortfolioValue as PortfolioValue,tblcontract.ActualServiceStartDate AS ActualServiceStartDate,tblcontract.AgreementType AS AgreementType,tblcontract.CategoryID "


    '        '(isnull(tblcontract.ActualEnd) and (tblcontract.EndDate >= @startdate) and (tblcontract.EndDate <= @enddate)  and (tblcontract.ServiceStart <= @enddate)
    '        'qry = "select " + fieldscategoryTerminationDecrease + " from tblcontract  where ((tblcontract.ActualEnd >= @startdate) and (tblcontract.ActualEnd <= @enddate)  and (tblcontract.ServiceStart <= @enddate)) and ((tblcontract.Status = 'R')  OR (tblcontract.Status = 'T') or (tblcontract.Status = 'E')) and tblcontract.RenewalST <> 'R' AND PORTFOLIOYN=1"
    '        qry = "SELECT *,SUM(AGREEVALUE),sum(ANNUALVALUE) FROM TBLMOVEMENTRPT WHERE (RPTcategory='NEW' OR RPTcategory='ADDITIONS' OR RPTCATEGORY='INCREASES' OR RPTCATEGORY='TERMINATIONS' OR RPTCATEGORY='DECREASES') AND REPORTNAME='" + rbtnSelect.SelectedValue.ToString + "' AND CREATEDBY='" + txtCreatedBy.Text + "'"


    '        cmdNettGain.CommandText = qry

    '        If category <> "NETT GAIN" Or category <> "CLOSING PORTFOLIO" Then
    '            cmdNettGain.Parameters.AddWithValue("@startdate", startdate.ToString("yyyy-MM-dd"))
    '            cmdNettGain.Parameters.AddWithValue("@enddate", enddate.ToString("yyyy-MM-dd"))
    '        End If

    '        Dim drNettGain As MySqlDataReader = cmdNettGain.ExecuteReader()
    '        Dim dtNettGain As New DataTable
    '        dtNettGain.Load(drNettGain)

    '        If dtNettGain.Rows.Count > 0 Then
    '            'Dim command As MySqlCommand = New MySqlCommand
    '            command.Connection = conn

    '            For i As Int16 = 0 To dtNettGain.Rows.Count - 1

    '                ' '''''

    '                'Dim sqlstrIncludeInportfolio As String
    '                'sqlstrIncludeInportfolio = ""

    '                'sqlstrIncludeInportfolio = "SELECT Category, IncludeinPortfolio FROM tblcontractgroup where IncludeInPortfolio = '1' and contractgroup = '" & dtNettGain.Rows(i)("ContractGroup") & "'"

    '                'Dim commandIncludeInportfolio As MySqlCommand = New MySqlCommand

    '                'Dim connIncludeInportfolio As MySqlConnection = New MySqlConnection()
    '                'connIncludeInportfolio.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    '                'connIncludeInportfolio.Open()

    '                'commandIncludeInportfolio.CommandType = CommandType.Text
    '                'commandIncludeInportfolio.CommandText = sqlstrIncludeInportfolio
    '                'commandIncludeInportfolio.Connection = connIncludeInportfolio

    '                'Dim drIncludeInportfolio As MySqlDataReader = commandIncludeInportfolio.ExecuteReader()
    '                'Dim dtIncludeInportfolio As New DataTable
    '                'dtIncludeInportfolio.Load(drIncludeInportfolio)

    '                'connIncludeInportfolio.Close()
    '                'commandIncludeInportfolio.Dispose()

    '                'If dtIncludeInportfolio.Rows.Count = 0 Then

    '                '    GoTo nextrecNettGain
    '                'End If

    '                ' '''''

    '                If category = "NETT GAIN" Or category = "CLOSING PORTFOLIO" Then
    '                    command.CommandType = CommandType.Text

    '                    command.CommandText = "INSERT INTO tblmovementrpt(RptCategory,CustCode,CustName,Total,ContractGroup,CompanyGroup,AnnualValue,ContractNo,ServStartDate,EndDate,ActualEnd,ContractStatus,Duration,DurationMS,AgreeValue,RecordLevel,AccountID,ReportName,CreatedBy,CreatedOn,ContractGroupCategory)VALUES(@RptCategory,@CustCode,@CustName,@Total,@ContractGroup,@CompanyGroup,@AnnualValue,@ContractNo,@ServStartDate,@EndDate,@ActualEnd,@ContractStatus,@Duration,@DurationMS,@AgreeValue,@RecordLevel,@AccountID,@ReportName,@CreatedBy,@CreatedOn,@ContractGroupCategory);"
    '                    command.Parameters.Clear()

    '                    command.Parameters.AddWithValue("@RptCategory", category)
    '                    command.Parameters.AddWithValue("@CustCode", "")
    '                    command.Parameters.AddWithValue("@CustName", "")
    '                    command.Parameters.AddWithValue("@ContractGroup", "")
    '                    command.Parameters.AddWithValue("@CompanyGroup", "")
    '                    command.Parameters.AddWithValue("@ContractNo", "")
    '                    command.Parameters.AddWithValue("@ServStartDate", DBNull.Value)
    '                    command.Parameters.AddWithValue("@EndDate", DBNull.Value)
    '                    command.Parameters.AddWithValue("@ActualEnd", DBNull.Value)
    '                    command.Parameters.AddWithValue("@ContractStatus", "")
    '                    command.Parameters.AddWithValue("@Duration", 0)
    '                    command.Parameters.AddWithValue("@DurationMS", "")
    '                    command.Parameters.AddWithValue("@RecordLevel", "07")
    '                    command.Parameters.AddWithValue("@AccountID", "")
    '                    command.Parameters.AddWithValue("@ReportName", rbtnSelect.SelectedValue.ToString)
    '                    command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
    '                    command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
    '                    command.Parameters.AddWithValue("@Total", dtNettGain.Rows(i)("SUM(AgreeValue)"))

    '                    command.Parameters.AddWithValue("@AnnualValue", dtNettGain.Rows(i)("SUM(AnnualValue)"))

    '                    command.Parameters.AddWithValue("@AgreeValue", dtNettGain.Rows(i)("SUM(AgreeValue)"))
    '                    command.Parameters.AddWithValue("@ContractGroupCategory", "")

    '                Else

    '                    Dim portfoliovalue As Double = 0

    '                    portfoliovalue = dtNettGain.Rows(i)("PortfolioValue")
    '                    command.CommandType = CommandType.Text
    '                    command.CommandText = "INSERT INTO tblmovementrpt(RptCategory,CustCode,CustName,Total,ContractGroup,CompanyGroup,AnnualValue,ContractNo,ServStartDate,EndDate,ActualEnd,ContractStatus,Duration,DurationMS,AgreeValue,RecordLevel,AccountID,ReportName,CreatedBy,CreatedOn,ContractGroupCategory)VALUES(@RptCategory,@CustCode,@CustName,@Total,@ContractGroup,@CompanyGroup,@AnnualValue,@ContractNo,@ServStartDate,@EndDate,@ActualEnd,@ContractStatus,@Duration,@DurationMS,@AgreeValue,@RecordLevel,@AccountID,@ReportName,@CreatedBy,@CreatedOn,@ContractGroupCategory);"
    '                    command.Parameters.Clear()

    '                    command.Parameters.AddWithValue("@RptCategory", category)
    '                    command.Parameters.AddWithValue("@CustCode", dtNettGain.Rows(i)("CustCode"))
    '                    command.Parameters.AddWithValue("@CustName", dtNettGain.Rows(i)("CustName"))
    '                    command.Parameters.AddWithValue("@ContractGroup", dtNettGain.Rows(i)("ContractGroup"))
    '                    command.Parameters.AddWithValue("@CompanyGroup", dtNettGain.Rows(i)("CompanyGroup"))
    '                    command.Parameters.AddWithValue("@ContractNo", dtNettGain.Rows(i)("ContractNo"))
    '                    command.Parameters.AddWithValue("@ServStartDate", dtNettGain.Rows(i)("ServiceStart"))
    '                    command.Parameters.AddWithValue("@EndDate", dtNettGain.Rows(i)("EndDate"))
    '                    command.Parameters.AddWithValue("@ActualEnd", dtNettGain.Rows(i)("ActualEnd"))
    '                    command.Parameters.AddWithValue("@ContractStatus", dtNettGain.Rows(i)("Status"))
    '                    command.Parameters.AddWithValue("@Duration", dtNettGain.Rows(i)("Duration"))
    '                    command.Parameters.AddWithValue("@DurationMS", dtNettGain.Rows(i)("DurationMS"))
    '                    command.Parameters.AddWithValue("@RecordLevel", "07")
    '                    command.Parameters.AddWithValue("@AccountID", dtNettGain.Rows(i)("AccountID"))
    '                    command.Parameters.AddWithValue("@ReportName", rbtnSelect.SelectedValue.ToString)
    '                    command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
    '                    command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
    '                    command.Parameters.AddWithValue("@Total", dtNettGain.Rows(i)("AgreeValue"))

    '                    command.Parameters.AddWithValue("@AnnualValue", portfoliovalue)

    '                    command.Parameters.AddWithValue("@AgreeValue", dtNettGain.Rows(i)("AgreeValue"))
    '                    command.Parameters.AddWithValue("@ContractGroupCategory", dtNettGain.Rows(i)("CategoryID"))

    '                End If

    '                command.Connection = conn

    '                command.ExecuteNonQuery()


    'nextrecNettGain:
    '            Next

    '        End If

    '        cmdNettGain.Dispose()
    '        dtNettGain.Dispose()
    '        drNettGain.Close()



    '        ''''''''''''''''''

    '        ''''''''''''''''


    '        '8


    '        ''''''''''''''''

    '        category = "CLOSING PORTFOLIO"
    '        qry = ""

    '        Dim cmdClosingPortfolio As MySqlCommand = New MySqlCommand
    '        'Dim command As MySqlCommand = New MySqlCommand
    '        cmdClosingPortfolio.Connection = conn
    '        cmdClosingPortfolio.CommandType = CommandType.Text

    '        'fieldscategoryTerminationDecrease = "tblcontract.ContractNo AS ContractNo,tblcontract.Status AS Status,tblcontract.ContactType AS ContactType,tblcontract.CustCode AS CustCode,tblcontract.CustName AS CustName,tblcontract.CustAddr AS CustAddr,tblcontract.ContractDate AS ContractDate,-tblcontract.AgreeValue AS AgreeValue,tblcontract.Duration AS Duration,tblcontract.DurationMs AS DurationMs,tblcontract.StartDate AS StartDate,tblcontract.EndDate AS EndDate,tblcontract.ActualEnd AS ActualEnd,tblcontract.OContractNo AS OContractNo,tblcontract.RenewalSt AS RenewalSt,tblcontract.RenewalDate AS RenewalDate,tblcontract.ContractGroup AS ContractGroup,tblcontract.RenewalContractNo AS RenewalContractNo,tblcontract.ServiceStart AS ServiceStart,tblcontract.ServiceEnd AS ServiceEnd,tblcontract.ServiceFrequence AS ServiceFrequence,tblcontract.ContractValue AS ContractValue,tblcontract.MainContractNo AS MainContractNo,tblcontract.CompanyGroup AS CompanyGroup,tblcontract.ValuePerMonth AS ValuePerMonth,tblcontract.AccountID AS AccountID,tblcontract.PortfolioYN AS PortfolioYN,-tblcontract.PortfolioValue as PortfolioValue,tblcontract.ActualServiceStartDate AS ActualServiceStartDate,tblcontract.AgreementType AS AgreementType,tblcontract.CategoryID "


    '        '(isnull(tblcontract.ActualEnd) and (tblcontract.EndDate >= @startdate) and (tblcontract.EndDate <= @enddate)  and (tblcontract.ServiceStart <= @enddate)
    '        'qry = "select " + fieldscategoryTerminationDecrease + " from tblcontract  where ((tblcontract.ActualEnd >= @startdate) and (tblcontract.ActualEnd <= @enddate)  and (tblcontract.ServiceStart <= @enddate)) and ((tblcontract.Status = 'R')  OR (tblcontract.Status = 'T') or (tblcontract.Status = 'E')) and tblcontract.RenewalST <> 'R' AND PORTFOLIOYN=1"
    '        'qry = "SELECT *,SUM(AGREEVALUE),sum(ANNUALVALUE) FROM TBLMOVEMENTRPT WHERE (RPTcategory='REVISIONS' OR RPTcategory='ADDITIONS' OR RPTCATEGORY='INCREASES' OR RPTCATEGORY='TERMINATIONS' OR RPTCATEGORY='DECREASES') AND REPORTNAME='" + rbtnSelect.SelectedValue.ToString + "' AND CREATEDBY='" + txtCreatedBy.Text + "'"

    '        qry = "SELECT *,SUM(AGREEVALUE),sum(ANNUALVALUE) FROM TBLMOVEMENTRPT WHERE REPORTNAME='" + rbtnSelect.SelectedValue.ToString + "' AND CREATEDBY='" + txtCreatedBy.Text + "'"

    '        cmdClosingPortfolio.CommandText = qry

    '        If category <> "NETT GAIN" Or category <> "CLOSING PORTFOLIO" Then
    '            cmdClosingPortfolio.Parameters.AddWithValue("@startdate", startdate.ToString("yyyy-MM-dd"))
    '            cmdClosingPortfolio.Parameters.AddWithValue("@enddate", enddate.ToString("yyyy-MM-dd"))
    '        End If

    '        Dim drClosingPortfolio As MySqlDataReader = cmdClosingPortfolio.ExecuteReader()
    '        Dim dtClosingPortfolio As New DataTable
    '        dtClosingPortfolio.Load(drClosingPortfolio)

    '        If dtClosingPortfolio.Rows.Count > 0 Then
    '            'Dim command As MySqlCommand = New MySqlCommand
    '            command.Connection = conn

    '            For i As Int16 = 0 To dtClosingPortfolio.Rows.Count - 1

    '                ' '''''

    '                'Dim sqlstrIncludeInportfolio As String
    '                'sqlstrIncludeInportfolio = ""

    '                'sqlstrIncludeInportfolio = "SELECT Category, IncludeinPortfolio FROM tblcontractgroup where IncludeInPortfolio = '1' and contractgroup = '" & dtClosingPortfolio.Rows(i)("ContractGroup") & "'"

    '                'Dim commandIncludeInportfolio As MySqlCommand = New MySqlCommand

    '                'Dim connIncludeInportfolio As MySqlConnection = New MySqlConnection()
    '                'connIncludeInportfolio.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    '                'connIncludeInportfolio.Open()

    '                'commandIncludeInportfolio.CommandType = CommandType.Text
    '                'commandIncludeInportfolio.CommandText = sqlstrIncludeInportfolio
    '                'commandIncludeInportfolio.Connection = connIncludeInportfolio

    '                'Dim drIncludeInportfolio As MySqlDataReader = commandIncludeInportfolio.ExecuteReader()
    '                'Dim dtIncludeInportfolio As New DataTable
    '                'dtIncludeInportfolio.Load(drIncludeInportfolio)

    '                'connIncludeInportfolio.Close()
    '                'commandIncludeInportfolio.Dispose()

    '                'If dtIncludeInportfolio.Rows.Count = 0 Then

    '                '    GoTo nextrecClosingPortfolio
    '                'End If

    '                ' '''''

    '                If category = "NETT GAIN" Or category = "CLOSING PORTFOLIO" Then
    '                    command.CommandType = CommandType.Text

    '                    command.CommandText = "INSERT INTO tblmovementrpt(RptCategory,CustCode,CustName,Total,ContractGroup,CompanyGroup,AnnualValue,ContractNo,ServStartDate,EndDate,ActualEnd,ContractStatus,Duration,DurationMS,AgreeValue,RecordLevel,AccountID,ReportName,CreatedBy,CreatedOn,ContractGroupCategory)VALUES(@RptCategory,@CustCode,@CustName,@Total,@ContractGroup,@CompanyGroup,@AnnualValue,@ContractNo,@ServStartDate,@EndDate,@ActualEnd,@ContractStatus,@Duration,@DurationMS,@AgreeValue,@RecordLevel,@AccountID,@ReportName,@CreatedBy,@CreatedOn,@ContractGroupCategory);"
    '                    command.Parameters.Clear()

    '                    command.Parameters.AddWithValue("@RptCategory", category)
    '                    command.Parameters.AddWithValue("@CustCode", "")
    '                    command.Parameters.AddWithValue("@CustName", "")
    '                    command.Parameters.AddWithValue("@ContractGroup", "")
    '                    command.Parameters.AddWithValue("@CompanyGroup", "")
    '                    command.Parameters.AddWithValue("@ContractNo", "")
    '                    command.Parameters.AddWithValue("@ServStartDate", DBNull.Value)
    '                    command.Parameters.AddWithValue("@EndDate", DBNull.Value)
    '                    command.Parameters.AddWithValue("@ActualEnd", DBNull.Value)
    '                    command.Parameters.AddWithValue("@ContractStatus", "")
    '                    command.Parameters.AddWithValue("@Duration", 0)
    '                    command.Parameters.AddWithValue("@DurationMS", "")
    '                    command.Parameters.AddWithValue("@RecordLevel", "08")
    '                    command.Parameters.AddWithValue("@AccountID", "")
    '                    command.Parameters.AddWithValue("@ReportName", rbtnSelect.SelectedValue.ToString)
    '                    command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
    '                    command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
    '                    command.Parameters.AddWithValue("@Total", dtClosingPortfolio.Rows(i)("SUM(AgreeValue)"))

    '                    command.Parameters.AddWithValue("@AnnualValue", dtClosingPortfolio.Rows(i)("SUM(AnnualValue)"))

    '                    command.Parameters.AddWithValue("@AgreeValue", dtClosingPortfolio.Rows(i)("SUM(AgreeValue)"))
    '                    command.Parameters.AddWithValue("@ContractGroupCategory", "")

    '                Else

    '                    Dim portfoliovalue As Double = 0

    '                    portfoliovalue = dtClosingPortfolio.Rows(i)("PortfolioValue")
    '                    command.CommandType = CommandType.Text
    '                    command.CommandText = "INSERT INTO tblmovementrpt(RptCategory,CustCode,CustName,Total,ContractGroup,CompanyGroup,AnnualValue,ContractNo,ServStartDate,EndDate,ActualEnd,ContractStatus,Duration,DurationMS,AgreeValue,RecordLevel,AccountID,ReportName,CreatedBy,CreatedOn,ContractGroupCategory)VALUES(@RptCategory,@CustCode,@CustName,@Total,@ContractGroup,@CompanyGroup,@AnnualValue,@ContractNo,@ServStartDate,@EndDate,@ActualEnd,@ContractStatus,@Duration,@DurationMS,@AgreeValue,@RecordLevel,@AccountID,@ReportName,@CreatedBy,@CreatedOn,@ContractGroupCategory);"
    '                    command.Parameters.Clear()

    '                    command.Parameters.AddWithValue("@RptCategory", category)
    '                    command.Parameters.AddWithValue("@CustCode", dtClosingPortfolio.Rows(i)("CustCode"))
    '                    command.Parameters.AddWithValue("@CustName", dtClosingPortfolio.Rows(i)("CustName"))
    '                    command.Parameters.AddWithValue("@ContractGroup", dtClosingPortfolio.Rows(i)("ContractGroup"))
    '                    command.Parameters.AddWithValue("@CompanyGroup", dtClosingPortfolio.Rows(i)("CompanyGroup"))
    '                    command.Parameters.AddWithValue("@ContractNo", dtClosingPortfolio.Rows(i)("ContractNo"))
    '                    command.Parameters.AddWithValue("@ServStartDate", dtClosingPortfolio.Rows(i)("ServiceStart"))
    '                    command.Parameters.AddWithValue("@EndDate", dtClosingPortfolio.Rows(i)("EndDate"))
    '                    command.Parameters.AddWithValue("@ActualEnd", dtClosingPortfolio.Rows(i)("ActualEnd"))
    '                    command.Parameters.AddWithValue("@ContractStatus", dtClosingPortfolio.Rows(i)("Status"))
    '                    command.Parameters.AddWithValue("@Duration", dtClosingPortfolio.Rows(i)("Duration"))
    '                    command.Parameters.AddWithValue("@DurationMS", dtClosingPortfolio.Rows(i)("DurationMS"))
    '                    command.Parameters.AddWithValue("@RecordLevel", "08")
    '                    command.Parameters.AddWithValue("@AccountID", dtClosingPortfolio.Rows(i)("AccountID"))
    '                    command.Parameters.AddWithValue("@ReportName", rbtnSelect.SelectedValue.ToString)
    '                    command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
    '                    command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
    '                    command.Parameters.AddWithValue("@Total", dtNettGain.Rows(i)("AgreeValue"))

    '                    command.Parameters.AddWithValue("@AnnualValue", portfoliovalue)

    '                    command.Parameters.AddWithValue("@AgreeValue", dtClosingPortfolio.Rows(i)("AgreeValue"))
    '                    command.Parameters.AddWithValue("@ContractGroupCategory", dtClosingPortfolio.Rows(i)("CategoryID"))

    '                End If

    '                command.Connection = conn

    '                command.ExecuteNonQuery()


    'nextrecClosingPortfolio:
    '            Next

    '        End If

    '        cmdNettGain.Dispose()
    '        dtNettGain.Dispose()
    '        drClosingPortfolio.Close()


    '        ''''''''''''''''
    '        command.Dispose()

    '    End Sub

    Protected Sub btnPrintServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnPrintServiceRecordList.Click
        lblAlert.Text = ""
        If GetData() = True Then


            If rbtnSelect.SelectedValue = "2" Then
                Session.Add("ReportType", "PortfolioMovementDetail")
                ' Response.Redirect("RV_PortfolioMovementDetail.aspx")
                Dim url As String = "RV_Export_Portfolio.aspx"
                Dim s As String = "window.open('" & url + "', '_blank');"
                ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
            ElseIf rbtnSelect.SelectedValue = "3" Then
                If chkGroupByContactType.Checked = True Then
                    Session.Add("ReportType", "PortfolioMovementSummaryByContactType")
                Else
                    Session.Add("ReportType", "PortfolioMovementSummary")
                End If

                Dim url As String = "RV_Export_Portfolio.aspx"
                Dim s As String = "window.open('" & url + "', '_blank');"
                ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
                '  Response.Redirect("RV_PortfolioMovementSummary.aspx")
            ElseIf rbtnSelect.SelectedValue = "4" Then
                If chkGroupByContactType.Checked = True Then
                    Session.Add("ReportType", "PortfolioValueSummaryByContGrpCategoryByContactType")
                Else
                    Session.Add("ReportType", "PortfolioValueSummaryByContGrpCategory")
                End If
                'Response.Redirect("RV_PortfolioValueSummaryByContGrpCategory.aspx")
                Dim url As String = "RV_Export_Portfolio.aspx"
                Dim s As String = "window.open('" & url + "', '_blank');"
                ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
            ElseIf rbtnSelect.SelectedValue = "5" Then
                If chkGroupByContactType.Checked = True Then
                    Session.Add("ReportType", "PortfolioValueSummaryByReportGrpCategoryByContactType")
                Else
                    Session.Add("ReportType", "PortfolioValueSummaryByReportGrpCategory")
                End If
                'Response.Redirect("RV_PortfolioValueSummaryByContGrpCategory.aspx")
                Dim url As String = "RV_Export_Portfolio.aspx"
                Dim s As String = "window.open('" & url + "', '_blank');"
                ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
            End If

        Else
            Return

        End If



    End Sub

    '    Protected Function RetrieveQuery(Category As String) As String

    '        Dim qry As String = ""
    '        Dim fields As String = ""
    '        Dim qrysel As String = ""
    '        Dim qrysel1 As String = ""

    '        If ddlCompanyGrp.Text = "-1" Then
    '        Else
    '            qrysel = qrysel + " and companygroup= '" + ddlCompanyGrp.Text + "'"
    '            qrysel1 = qrysel1 + " and rev.companygroup= '" + ddlCompanyGrp.Text + "'"

    '        End If
    '        If ddlContractGroup.Text = "-1" Then
    '        Else
    '            qrysel = qrysel + " and contractgroup= '" + ddlContractGroup.Text + "'"
    '            qrysel1 = qrysel1 + " and contractgroup= '" + ddlContractGroup.Text + "'"

    '        End If
    '        'If ddlCategory.Text = "-1" Then
    '        'Else
    '        '    qrysel = qrysel + " and categoryid= '" + ddlCategory.Text + "'"
    '        '    qrysel1 = qrysel1 + " and categoryid= '" + ddlCategory.Text + "'"

    '        'End If
    '        'If ddlSalesMan.Text = "-1" Then
    '        'Else
    '        '    qrysel = qrysel + " and salesman= '" + ddlSalesMan.Text + "'"
    '        '    qrysel1 = qrysel1 + " and salesman= '" + ddlSalesMan.Text + "'"

    '        'End If
    '        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
    '               qrysel = qrysel + " and location in [" + Convert.ToString(Session("Branch")) + "]"
    '            qrysel1 = qrysel1 + " and location in [" + Convert.ToString(Session("Branch")) + "]"

    '        End If


    '        If Category = "TERMINATIONS" Or Category = "DECREASES" Then
    '            fields = "tblcontract.ContractNo AS ContractNo,tblcontract.Status AS Status,tblcontract.ContactType AS ContactType,tblcontract.CustCode AS CustCode,tblcontract.CustName AS CustName,tblcontract.CustAddr AS CustAddr,tblcontract.ContractDate AS ContractDate,-tblcontract.AgreeValue AS AgreeValue,tblcontract.Duration AS Duration,tblcontract.DurationMs AS DurationMs,tblcontract.StartDate AS StartDate,tblcontract.EndDate AS EndDate,tblcontract.ActualEnd AS ActualEnd,tblcontract.OContractNo AS OContractNo,tblcontract.RenewalSt AS RenewalSt,tblcontract.RenewalDate AS RenewalDate,tblcontract.ContractGroup AS ContractGroup,tblcontract.RenewalContractNo AS RenewalContractNo,tblcontract.ServiceStart AS ServiceStart,tblcontract.ServiceEnd AS ServiceEnd,tblcontract.ServiceFrequence AS ServiceFrequence,tblcontract.ContractValue AS ContractValue,tblcontract.MainContractNo AS MainContractNo,tblcontract.CompanyGroup AS CompanyGroup,tblcontract.ValuePerMonth AS ValuePerMonth,tblcontract.AccountID AS AccountID,tblcontract.PortfolioYN AS PortfolioYN,-tblcontract.PortfolioValue as PortfolioValue,-vw.PortfolioValue AS ViewPortfolioValue,tblcontract.ActualServiceStartDate AS ActualServiceStartDate,tblcontract.AgreementType AS AgreementType,tblcontract.CategoryID "
    '        Else
    '            fields = "tblcontract.ContractNo AS ContractNo,tblcontract.Status AS Status,tblcontract.ContactType AS ContactType,tblcontract.CustCode AS CustCode,tblcontract.CustName AS CustName,tblcontract.CustAddr AS CustAddr,tblcontract.ContractDate AS ContractDate,tblcontract.AgreeValue AS AgreeValue,tblcontract.Duration AS Duration,tblcontract.DurationMs AS DurationMs,tblcontract.StartDate AS StartDate,tblcontract.EndDate AS EndDate,tblcontract.ActualEnd AS ActualEnd,tblcontract.OContractNo AS OContractNo,tblcontract.RenewalSt AS RenewalSt,tblcontract.RenewalDate AS RenewalDate,tblcontract.ContractGroup AS ContractGroup,tblcontract.RenewalContractNo AS RenewalContractNo,tblcontract.ServiceStart AS ServiceStart,tblcontract.ServiceEnd AS ServiceEnd,tblcontract.ServiceFrequence AS ServiceFrequence,tblcontract.ContractValue AS ContractValue,tblcontract.MainContractNo AS MainContractNo,tblcontract.CompanyGroup AS CompanyGroup,tblcontract.ValuePerMonth AS ValuePerMonth,tblcontract.AccountID AS AccountID,tblcontract.PortfolioYN AS PortfolioYN,tblcontract.PortfolioValue AS PortfolioValue,vw.PortfolioValue AS ViewPortfolioValue,tblcontract.ActualServiceStartDate AS ActualServiceStartDate,tblcontract.AgreementType AS AgreementType,tblcontract.CategoryID "
    '        End If



    '        'If Category = "OPENING PORTFOLIO" Then
    '        '    qry = "select " + fields + " from tblcontract left outer join vwcontractportfolio as vw on tblcontract.contractno = vw.contractno where (isnull(tblcontract.ActualEnd) and (tblcontract.EndDate > @startdate)    and (tblcontract.ServiceStart < @startdate)    and (tblcontract.Status <> 'v')    and (tblcontract.Status <> 't') AND PORTFOLIOYN=1 "
    '        '    qry = qry + qrysel

    '        '    qry = qry + ") union select " + fields + " from tblcontract left outer join vwcontractportfolio as vw on tblcontract.contractno = vw.contractno where ((tblcontract.ActualEnd > @startdate)    and (tblcontract.ServiceStart < @startdate)    and (tblcontract.Status <> 'v')    and (tblcontract.Status <> 't') AND PORTFOLIOYN=1 " + qrysel + ")"

    '        'ElseIf Category = "ADDITIONS" Then
    '        '    qry = "select " + fields + " from tblcontract left outer join vwcontractportfolio as vw on tblcontract.contractno = vw.contractno where (isnull(tblcontract.ActualEnd) and (tblcontract.EndDate > @startdate)  and (tblcontract.ServiceStart >= @startdate) and (tblcontract.ServiceStart <= @enddate) and "
    '        '    qry = qry + "(ocontractno is null OR ocontractno ='') and renewalst<>'R'   and (tblcontract.Status <> 'v')    and (tblcontract.Status <> 't') AND PORTFOLIOYN=1 "
    '        '    qry = qry + qrysel
    '        '    qry = qry + ") union select " + fields + " from tblcontract left outer join vwcontractportfolio as vw on tblcontract.contractno = vw.contractno where ((tblcontract.ActualEnd > @startdate)  and (tblcontract.ServiceStart >= @startdate) and (tblcontract.ServiceStart <= @enddate) and "
    '        '    qry = qry + "(ocontractno is null OR ocontractno ='') and (tblcontract.Status <> 'v')    and (tblcontract.Status <> 't') AND PORTFOLIOYN=1 "
    '        '    qry = qry + qrysel + ")"
    '        'ElseIf Category = "REVISIONS" Then
    '        '    qry = "SELECT * FROM ("
    '        '    qry = qry + "select " + fields + " from tblcontract left outer join vwcontractportfolio as vw on tblcontract.contractno = vw.contractno where ((tblcontract.ACTUALEND < @startdate)  OR (tblcontract.ACTUALEND > @ENDdate)) and "
    '        '    qry = qry + "(tblcontract.Status = 'R') AND PORTFOLIOYN=1 "
    '        '    qry = qry + qrysel
    '        '    qry = qry + ")  as rev,tblcontract as cont where rev.contractno=cont.maincontractno and rev.agreevalue=cont.agreevalue"
    '        '    qry = qry + " AND  CONT.PORTFOLIOYN=1 AND CONT.SERVICESTART>=@startdate AND CONT.SERVICESTART<=@enddate"
    '        '    qry = qry + " AND CONT.STATUS<>'T' AND CONT.STATUS<>'V' AND CONT.STATUS<>'r'"
    '        '    qry = qry + " AND CONT.CONTRACTDATE>=@startdate AND CONT.CONTRACTDATE<=@enddate"

    '        'ElseIf Category = "INCREASES" Then
    '        '    qry = "SELECT * FROM ("
    '        '    qry = qry + "select " + fields + " from tblcontract left outer join vwcontractportfolio as vw on tblcontract.contractno = vw.contractno where (isnull(tblcontract.ActualEnd) and (tblcontract.EndDate >= @startdate)  and (tblcontract.ServiceStart >= @startdate) and (tblcontract.ServiceStart <= @enddate) and "
    '        '    qry = qry + "(ocontractno is NOT null OR ocontractno <>'') and (tblcontract.Status <> 'v')    and (tblcontract.Status <> 't') AND PORTFOLIOYN=1 "
    '        '    qry = qry + qrysel
    '        '    qry = qry + ") union select " + fields + " from tblcontract left outer join vwcontractportfolio as vw on tblcontract.contractno = vw.contractno where ((tblcontract.ActualEnd >= @startdate)  and (tblcontract.ServiceStart >= @startdate) and (tblcontract.ServiceStart <= @enddate) and "
    '        '    qry = qry + "(ocontractno is NOT null OR ocontractno <>'') and (tblcontract.Status <> 'v')    and (tblcontract.Status <> 't') AND PORTFOLIOYN=1" + qrysel + ") "
    '        '    qry = qry + ")  as rev,tblcontract as cont where rev.ocontractno=cont.contractno and rev.agreevalue>=cont.agreevalue"

    '        'ElseIf Category = "TERMINATIONS" Then
    '        '    qry = "select " + fields + " from tblcontract left outer join vwcontractportfolio as vw on tblcontract.contractno = vw.contractno where (isnull(tblcontract.ActualEnd) and (tblcontract.EndDate >= @startdate) and (tblcontract.EndDate <= @enddate)  and (tblcontract.ServiceStart <= @enddate) and "
    '        '    qry = qry + "(tblcontract.Status <> 'v')    and (tblcontract.Status <> 't') AND PORTFOLIOYN=1 "
    '        '    qry = qry + qrysel
    '        '    qry = qry + ") union select " + fields + " from tblcontract left outer join vwcontractportfolio as vw on tblcontract.contractno = vw.contractno where ((tblcontract.ActualEnd >= @startdate) and (tblcontract.ActualEnd <= @enddate) and (tblcontract.ServiceStart <= @enddate) and "
    '        '    qry = qry + "(tblcontract.Status <> 'v')    and (tblcontract.Status <> 't') AND PORTFOLIOYN=1" + qrysel + ") "

    '        'ElseIf Category = "DECREASES" Then
    '        '    qry = "SELECT * FROM ("

    '        '    qry = qry + "select " + fields + " from tblcontract left outer join vwcontractportfolio as vw on tblcontract.contractno = vw.contractno where (tblcontract.ServiceStart >= @startdate) and (tblcontract.ServiceStart <= @enddate) and "
    '        '    qry = qry + "(ocontractno is NOT null OR ocontractno <>'') and (tblcontract.Status <> 'v') and (tblcontract.Status <> 't') AND PORTFOLIOYN=1"
    '        '    qry = qry + qrysel
    '        '    qry = qry + ") as rev,tblcontract as cont where rev.ocontractno=cont.contractno and rev.agreevalue<cont.agreevalue"

    '        'ElseIf Category = "NETT GAIN" Then
    '        '    qry = "SELECT *,SUM(AGREEVALUE),sum(ANNUALVALUE) FROM TBLMOVEMENTRPT WHERE (RPTcategory='REVISIONS' OR RPTcategory='ADDITIONS' OR RPTCATEGORY='INCREASES' OR RPTCATEGORY='TERMINATIONS' OR RPTCATEGORY='DECREASES') AND REPORTNAME='" + rbtnSelect.SelectedValue.ToString + "' AND CREATEDBY='" + txtCreatedBy.Text + "'"

    '        'ElseIf Category = "CLOSING PORTFOLIO" Then
    '        '    qry = "SELECT *,SUM(AGREEVALUE),sum(ANNUALVALUE) FROM TBLMOVEMENTRPT WHERE REPORTNAME='" + rbtnSelect.SelectedValue.ToString + "' AND CREATEDBY='" + txtCreatedBy.Text + "'"
    '        'End If



    '        '07/02/19

    '        If Category = "OPENING PORTFOLIO" Then
    '            qry = "select " + fields + " from tblcontract left outer join vwcontractportfolio as vw on tblcontract.contractno = vw.contractno where (isnull(tblcontract.ActualEnd) and (tblcontract.EndDate > @startdate)    and (tblcontract.ServiceStart < @startdate)    and (tblcontract.Status <> 'v')    and (tblcontract.Status <> 't') AND PORTFOLIOYN=1 "
    '            qry = qry + qrysel

    '            qry = qry + ") union select " + fields + " from tblcontract left outer join vwcontractportfolio as vw on tblcontract.contractno = vw.contractno where ((tblcontract.ActualEnd > @startdate)    and (tblcontract.ServiceStart < @startdate)    and (tblcontract.Status <> 'v')    and (tblcontract.Status <> 't') AND PORTFOLIOYN=1 " + qrysel + ")"

    '        ElseIf Category = "ADDITIONS" Then
    '            qry = "select " + fields + " from tblcontract left outer join vwcontractportfolio as vw on tblcontract.contractno = vw.contractno where (isnull(tblcontract.ActualEnd) and (tblcontract.EndDate > @startdate)  and (tblcontract.ServiceStart >= @startdate) and (tblcontract.ServiceStart <= @enddate) and "
    '            qry = qry + "(ocontractno is null OR ocontractno ='') and renewalst<>'R'   and (tblcontract.Status <> 'v')    and (tblcontract.Status <> 't') AND PORTFOLIOYN=1 "
    '            qry = qry + qrysel
    '            qry = qry + ") union select " + fields + " from tblcontract left outer join vwcontractportfolio as vw on tblcontract.contractno = vw.contractno where ((tblcontract.ActualEnd > @startdate)  and (tblcontract.ServiceStart >= @startdate) and (tblcontract.ServiceStart <= @enddate) and "
    '            qry = qry + "(ocontractno is null OR ocontractno ='') and (tblcontract.Status <> 'v')    and (tblcontract.Status <> 't') AND PORTFOLIOYN=1 "
    '            qry = qry + qrysel + ")"
    '        ElseIf Category = "REVISIONS" Then
    '            qry = "SELECT * FROM ("
    '            qry = qry + "select " + fields + " from tblcontract left outer join vwcontractportfolio as vw on tblcontract.contractno = vw.contractno where ((tblcontract.ACTUALEND < @startdate)  OR (tblcontract.ACTUALEND > @ENDdate)) and "
    '            qry = qry + "(tblcontract.Status = 'R') AND PORTFOLIOYN=1 "
    '            qry = qry + qrysel
    '            qry = qry + ")  as rev,tblcontract as cont where rev.contractno=cont.maincontractno and rev.agreevalue=cont.agreevalue"
    '            qry = qry + " AND  CONT.PORTFOLIOYN=1 AND CONT.SERVICESTART>=@startdate AND CONT.SERVICESTART<=@enddate"
    '            qry = qry + " AND CONT.STATUS<>'T' AND CONT.STATUS<>'V' AND CONT.STATUS<>'r'"
    '            qry = qry + " AND CONT.CONTRACTDATE>=@startdate AND CONT.CONTRACTDATE<=@enddate"

    '        ElseIf Category = "INCREASES" Then
    '            qry = "SELECT * FROM ("
    '            qry = qry + "select " + fields + " from tblcontract left outer join vwcontractportfolio as vw on tblcontract.contractno = vw.contractno where (isnull(tblcontract.ActualEnd) and (tblcontract.EndDate >= @startdate)  and (tblcontract.ServiceStart >= @startdate) and (tblcontract.ServiceStart <= @enddate) and "
    '            qry = qry + "(ocontractno is NOT null OR ocontractno <>'') and (tblcontract.Status <> 'v')    and (tblcontract.Status <> 't') AND PORTFOLIOYN=1 "
    '            qry = qry + qrysel
    '            qry = qry + ") union select " + fields + " from tblcontract left outer join vwcontractportfolio as vw on tblcontract.contractno = vw.contractno where ((tblcontract.ActualEnd >= @startdate)  and (tblcontract.ServiceStart >= @startdate) and (tblcontract.ServiceStart <= @enddate) and "
    '            qry = qry + "(ocontractno is NOT null OR ocontractno <>'') and (tblcontract.Status <> 'v')    and (tblcontract.Status <> 't') AND PORTFOLIOYN=1" + qrysel + ") "
    '            qry = qry + ")  as rev,tblcontract as cont where rev.ocontractno=cont.contractno and rev.agreevalue>=cont.agreevalue"

    '        ElseIf Category = "TERMINATIONS" Then
    '            qry = "select " + fields + " from tblcontract left outer join vwcontractportfolio as vw on tblcontract.contractno = vw.contractno where (isnull(tblcontract.ActualEnd) and (tblcontract.EndDate >= @startdate) and (tblcontract.EndDate <= @enddate)  and (tblcontract.ServiceStart <= @enddate) and "
    '            qry = qry + "(tblcontract.Status <> 'v')    and (tblcontract.Status <> 't') AND PORTFOLIOYN=1 "
    '            qry = qry + qrysel
    '            qry = qry + ") union select " + fields + " from tblcontract left outer join vwcontractportfolio as vw on tblcontract.contractno = vw.contractno where ((tblcontract.ActualEnd >= @startdate) and (tblcontract.ActualEnd <= @enddate) and (tblcontract.ServiceStart <= @enddate) and "
    '            qry = qry + "(tblcontract.Status <> 'v')    and (tblcontract.Status <> 't') AND PORTFOLIOYN=1" + qrysel + ") "

    '        ElseIf Category = "DECREASES" Then
    '            qry = "SELECT * FROM ("

    '            qry = qry + "select " + fields + " from tblcontract left outer join vwcontractportfolio as vw on tblcontract.contractno = vw.contractno where (tblcontract.ServiceStart >= @startdate) and (tblcontract.ServiceStart <= @enddate) and "
    '            qry = qry + "(ocontractno is NOT null OR ocontractno <>'') and (tblcontract.Status <> 'v') and (tblcontract.Status <> 't') AND PORTFOLIOYN=1"
    '            qry = qry + qrysel
    '            qry = qry + ") as rev,tblcontract as cont where rev.ocontractno=cont.contractno and   abs(rev.agreevalue) < cont.agreevalue"

    '            'ElseIf Category = "DECREASES" Then
    '            '    qry = "SELECT * FROM ("

    '            '    qry = qry + "select " + fields + " from tblcontract left outer join vwcontractportfolio as vw on tblcontract.contractno = vw.contractno where (tblcontract.ServiceStart >= @startdate) and (tblcontract.ServiceStart <= @enddate) and "
    '            '    qry = qry + "(ocontractno is NOT null OR ocontractno <>'') and (tblcontract.Status <> 'v') and (tblcontract.Status <> 't') AND PORTFOLIOYN=1"
    '            '    qry = qry + qrysel
    '            '    qry = qry + ") union select " + fields + " from tblcontract left outer join vwcontractportfolio as vw on tblcontract.contractno = vw.contractno where ((tblcontract.ServiceStart >= @startdate)   and (tblcontract.ServiceStart <= @enddate) and "
    '            '    qry = qry + "(ocontractno is NOT null OR ocontractno <>'') and (tblcontract.Status <> 'v')    and (tblcontract.Status <> 't') AND PORTFOLIOYN=1" + qrysel + ") "
    '            '    qry = qry + ")  as rev,tblcontract as cont where rev.ocontractno=cont.contractno and rev.agreevalue < cont.agreevalue"

    '        ElseIf Category = "NETT GAIN" Then
    '            qry = "SELECT *,SUM(AGREEVALUE),sum(ANNUALVALUE) FROM TBLMOVEMENTRPT WHERE (RPTcategory='REVISIONS' OR RPTcategory='ADDITIONS' OR RPTCATEGORY='INCREASES' OR RPTCATEGORY='TERMINATIONS' OR RPTCATEGORY='DECREASES') AND REPORTNAME='" + rbtnSelect.SelectedValue.ToString + "' AND CREATEDBY='" + txtCreatedBy.Text + "'"

    '        ElseIf Category = "CLOSING PORTFOLIO" Then
    '            qry = "SELECT *,SUM(AGREEVALUE),sum(ANNUALVALUE) FROM TBLMOVEMENTRPT WHERE REPORTNAME='" + rbtnSelect.SelectedValue.ToString + "' AND CREATEDBY='" + txtCreatedBy.Text + "'"
    '        End If

    '        '07/02/19



    '        'If Category = "INCREASES" Or Category = "DECREASES" Then

    '        '    If ddlCompanyGrp.Text = "-1" Then
    '        '    Else
    '        '        qry = qry + " and rev.companygroup= '" + ddlCompanyGrp.Text + "'"

    '        '    End If


    '        '    If ddlContractGroup.Text = "-1" Then
    '        '    Else
    '        '        qry = qry + " and rev.contractgroup= '" + ddlContractGroup.Text + "'"

    '        '    End If


    '        '    If ddlCategory.Text = "-1" Then
    '        '    Else
    '        '        qry = qry + " and rev.contractgroupcategory= '" + ddlCategory.Text + "'"

    '        '    End If
    '        '    If ddlSalesMan.Text = "-1" Then
    '        '    Else
    '        '        qry = qry + " and rev.salesman= '" + ddlSalesMan.Text + "'"

    '        '    End If
    '        '    qry = qry + " order by rev.accountid"
    '        'Else

    '        '    If ddlCompanyGrp.Text = "-1" Then
    '        '    Else
    '        '        qry = qry + " and companygroup= '" + ddlCompanyGrp.Text + "'"

    '        '    End If


    '        '    If ddlContractGroup.Text = "-1" Then
    '        '    Else
    '        '        qry = qry + " and contractgroup= '" + ddlContractGroup.Text + "'"

    '        '    End If
    '        '    If ddlCategory.Text = "-1" Then
    '        '    Else
    '        '        qry = qry + " and contractgroupcategory= '" + ddlCategory.Text + "'"

    '        '    End If
    '        '    If ddlSalesMan.Text = "-1" Then
    '        '    Else
    '        '        qry = qry + " and salesman= '" + ddlSalesMan.Text + "'"

    '        '    End If
    '        '    qry = qry + " order by accountid"

    '        'End If

    '        Return qry

    '    End Function


    '    Protected Sub PortfolioCalc(conn As MySqlConnection, category As String, level As String, startdate As DateTime, enddate As DateTime)
    '        Dim selection As String = ""

    '        Dim cmdOpening As MySqlCommand = New MySqlCommand

    '        cmdOpening.Connection = conn

    '        cmdOpening.CommandType = CommandType.Text

    '        cmdOpening.CommandText = RetrieveQuery(category)
    '        If category <> "NETT GAIN" Or category <> "CLOSING PORTFOLIO" Then
    '            cmdOpening.Parameters.AddWithValue("@startdate", startdate.ToString("yyyy-MM-dd"))
    '            cmdOpening.Parameters.AddWithValue("@enddate", enddate.ToString("yyyy-MM-dd"))
    '        End If

    '        Dim drOpening As MySqlDataReader = cmdOpening.ExecuteReader()
    '        Dim dtOpening As New DataTable
    '        dtOpening.Load(drOpening)

    '        If dtOpening.Rows.Count > 0 Then
    '            Dim command As MySqlCommand = New MySqlCommand
    '            command.Connection = conn

    '            For i As Int16 = 0 To dtOpening.Rows.Count - 1

    '                '''''

    '                Dim sqlstrIncludeInportfolio As String
    '                sqlstrIncludeInportfolio = ""

    '                sqlstrIncludeInportfolio = "SELECT Category, IncludeinPortfolio FROM tblcontractgroup where IncludeInPortfolio = '1' and contractgroup = '" & dtOpening.Rows(i)("ContractGroup") & "'"

    '                Dim commandIncludeInportfolio As MySqlCommand = New MySqlCommand

    '                Dim connIncludeInportfolio As MySqlConnection = New MySqlConnection()
    '                connIncludeInportfolio.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    '                connIncludeInportfolio.Open()

    '                commandIncludeInportfolio.CommandType = CommandType.Text
    '                commandIncludeInportfolio.CommandText = sqlstrIncludeInportfolio
    '                commandIncludeInportfolio.Connection = connIncludeInportfolio

    '                Dim drIncludeInportfolio As MySqlDataReader = commandIncludeInportfolio.ExecuteReader()
    '                Dim dtIncludeInportfolio As New DataTable
    '                dtIncludeInportfolio.Load(drIncludeInportfolio)

    '                connIncludeInportfolio.Close()
    '                commandIncludeInportfolio.Dispose()

    '                If dtIncludeInportfolio.Rows.Count = 0 Then

    '                    GoTo nextrec
    '                End If

    '                '''''


    '                If category = "NETT GAIN" Or category = "CLOSING PORTFOLIO" Then
    '                    command.CommandType = CommandType.Text

    '                    command.CommandText = "INSERT INTO tblmovementrpt(RptCategory,CustCode,CustName,Total,ContractGroup,CompanyGroup,AnnualValue,ContractNo,ServStartDate,EndDate,ActualEnd,ContractStatus,Duration,DurationMS,AgreeValue,RecordLevel,AccountID,ReportName,CreatedBy,CreatedOn,ContractGroupCategory)VALUES(@RptCategory,@CustCode,@CustName,@Total,@ContractGroup,@CompanyGroup,@AnnualValue,@ContractNo,@ServStartDate,@EndDate,@ActualEnd,@ContractStatus,@Duration,@DurationMS,@AgreeValue,@RecordLevel,@AccountID,@ReportName,@CreatedBy,@CreatedOn,@ContractGroupCategory);"
    '                    command.Parameters.Clear()

    '                    command.Parameters.AddWithValue("@RptCategory", category)
    '                    command.Parameters.AddWithValue("@CustCode", "")
    '                    command.Parameters.AddWithValue("@CustName", "")
    '                    command.Parameters.AddWithValue("@ContractGroup", dtOpening.Rows(i)("ContractGroup"))
    '                    command.Parameters.AddWithValue("@CompanyGroup", dtOpening.Rows(i)("CompanyGroup"))
    '                    command.Parameters.AddWithValue("@ContractNo", "")
    '                    command.Parameters.AddWithValue("@ServStartDate", DBNull.Value)
    '                    command.Parameters.AddWithValue("@EndDate", DBNull.Value)
    '                    command.Parameters.AddWithValue("@ActualEnd", DBNull.Value)
    '                    command.Parameters.AddWithValue("@ContractStatus", "")
    '                    command.Parameters.AddWithValue("@Duration", 0)
    '                    command.Parameters.AddWithValue("@DurationMS", "")
    '                    command.Parameters.AddWithValue("@RecordLevel", level)
    '                    command.Parameters.AddWithValue("@AccountID", "")
    '                    command.Parameters.AddWithValue("@ReportName", rbtnSelect.SelectedValue.ToString)
    '                    command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
    '                    command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
    '                    command.Parameters.AddWithValue("@Total", dtOpening.Rows(i)("SUM(AgreeValue)"))

    '                    command.Parameters.AddWithValue("@AnnualValue", dtOpening.Rows(i)("SUM(AnnualValue)"))

    '                    command.Parameters.AddWithValue("@AgreeValue", dtOpening.Rows(i)("SUM(AgreeValue)"))
    '                    command.Parameters.AddWithValue("@ContractGroupCategory", "")

    '                Else

    '                    Dim portfoliovalue As Double = 0



    '                    If String.IsNullOrEmpty(dtOpening.Rows(i)("ViewPortfolioValue").ToString) = False Then
    '                        portfoliovalue = dtOpening.Rows(i)("ViewPortfolioValue")

    '                    Else
    '                        portfoliovalue = dtOpening.Rows(i)("PortfolioValue")

    '                    End If

    '                    command.CommandType = CommandType.Text
    '                    command.CommandText = "INSERT INTO tblmovementrpt(RptCategory,CustCode,CustName,Total,ContractGroup,CompanyGroup,AnnualValue,ContractNo,ServStartDate,EndDate,ActualEnd,ContractStatus,Duration,DurationMS,AgreeValue,RecordLevel,AccountID,ReportName,CreatedBy,CreatedOn,ContractGroupCategory)VALUES(@RptCategory,@CustCode,@CustName,@Total,@ContractGroup,@CompanyGroup,@AnnualValue,@ContractNo,@ServStartDate,@EndDate,@ActualEnd,@ContractStatus,@Duration,@DurationMS,@AgreeValue,@RecordLevel,@AccountID,@ReportName,@CreatedBy,@CreatedOn,@ContractGroupCategory);"
    '                    command.Parameters.Clear()

    '                    command.Parameters.AddWithValue("@RptCategory", category)
    '                    command.Parameters.AddWithValue("@CustCode", dtOpening.Rows(i)("CustCode"))
    '                    command.Parameters.AddWithValue("@CustName", dtOpening.Rows(i)("CustName"))
    '                    command.Parameters.AddWithValue("@ContractGroup", dtOpening.Rows(i)("ContractGroup"))
    '                    command.Parameters.AddWithValue("@CompanyGroup", dtOpening.Rows(i)("CompanyGroup"))
    '                    command.Parameters.AddWithValue("@ContractNo", dtOpening.Rows(i)("ContractNo"))
    '                    command.Parameters.AddWithValue("@ServStartDate", dtOpening.Rows(i)("ServiceStart"))
    '                    command.Parameters.AddWithValue("@EndDate", dtOpening.Rows(i)("EndDate"))
    '                    command.Parameters.AddWithValue("@ActualEnd", dtOpening.Rows(i)("ActualEnd"))
    '                    command.Parameters.AddWithValue("@ContractStatus", dtOpening.Rows(i)("Status"))
    '                    command.Parameters.AddWithValue("@Duration", dtOpening.Rows(i)("Duration"))
    '                    command.Parameters.AddWithValue("@DurationMS", dtOpening.Rows(i)("DurationMS"))
    '                    command.Parameters.AddWithValue("@RecordLevel", level)
    '                    command.Parameters.AddWithValue("@AccountID", dtOpening.Rows(i)("AccountID"))
    '                    command.Parameters.AddWithValue("@ReportName", rbtnSelect.SelectedValue.ToString)
    '                    command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
    '                    command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
    '                    command.Parameters.AddWithValue("@Total", dtOpening.Rows(i)("AgreeValue"))

    '                    command.Parameters.AddWithValue("@AnnualValue", portfoliovalue)

    '                    command.Parameters.AddWithValue("@AgreeValue", dtOpening.Rows(i)("AgreeValue"))
    '                    command.Parameters.AddWithValue("@ContractGroupCategory", dtOpening.Rows(i)("CategoryID"))

    '                End If

    '                command.Connection = conn

    '                command.ExecuteNonQuery()

    '                command.Dispose()
    'nextrec:
    '            Next

    '        End If

    '        dtOpening.Clear()
    '        dtOpening.Dispose()
    '        drOpening.Close()
    '        cmdOpening.Dispose()

    '    End Sub
    Protected Sub btnCloseServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnCloseServiceRecordList.Click
        txtSvcDateFrom.Text = ""
        txtSvcDateTo.Text = ""
        ddlCompanyGrp.SelectedIndex = 0
        txtServiceID.SelectedIndex = 0
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim UserID As String = Convert.ToString(Session("UserID"))
        txtCreatedBy.Text = UserID
        If txtCreatedBy.Text = "SEN" Then
            chkNew.Enabled = True
            chkNew.Visible = True
        Else
            'chkNew.Enabled = True
            chkNew.Visible = False
        End If

        txtCreatedOn.Attributes.Add("readonly", "readonly")
    End Sub


    Private Function getdatasetSen() As DataTable
        Dim lInvoiceNo As String
        lInvoiceNo = ""
        Try
            'Dim dt2 As New DataTable
            Dim dt As New DataTable()
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        
            conn.Open()

            '''''''''''''''''''''''''''''''''''''''''''''''''''


            If rbtnSelect.SelectedValue.ToString = "2" Then
                Dim command2 As MySqlCommand = New MySqlCommand

                command2.CommandType = CommandType.Text

                Dim qry As String = "SELECT RptCategory as Category , CompanyGroup, AccountID, CustName as 'Customer Name',  ContractNo, ContractGroup, ContractGroupCategory, ContractStatus, Duration, DurationMS, AgreeValue, AnnualValue   "
                qry = qry + " FROM tblmovementrpt where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"
                qry = qry + "  and reportname='" + rbtnSelect.SelectedValue.ToString + "'"
                qry = qry + " Order BY RecordLevel, ContractNo"

                command2.CommandText = qry
                command2.Connection = conn

                Dim dr2 As MySqlDataReader = command2.ExecuteReader()
                Dim dt2 As New DataTable
                dt2.Load(dr2)

                Dim drTotal As DataRow = dt2.NewRow
                'Dim TotalInvoice As Decimal = 0
                'Dim TotalBalance As Decimal = 0
                'Dim TotalCurrent As Decimal = 0
                'Dim Total1to10 As Decimal = 0
                'Dim Total11to30 As Decimal = 0
                'Dim Total31to60 As Decimal = 0
                'Dim Total61to90 As Decimal = 0
                'Dim Total91to150 As Decimal = 0
                'Dim Total151to180 As Decimal = 0
                'Dim Total180 As Decimal = 0

                'Dim svcaddr As String = ""
                'Dim billcontactperson As String = ""
                'Dim billmobile As String = ""
                'Dim billtel As String = ""
                'Dim billfax As String = ""
                'Dim billtel2 As String = ""
                'Dim billcontact1email As String = ""

                For Each dr As DataRow In dt2.Rows
                    drTotal.Item("Category") = dr.Item("Category")
                    'If dr.Item("UnpaidBalance").ToString <> DBNull.Value.ToString Then
                    '    TotalBalance = TotalBalance + dr.Item("UnpaidBalance")
                    'End If
                    'If dr.Item("Current").ToString <> DBNull.Value.ToString Then
                    '    TotalCurrent = TotalCurrent + dr.Item("Current")
                    'End If
                    'If dr.Item("1To10").ToString <> DBNull.Value.ToString Then
                    '    Total1to10 = Total1to10 + dr.Item("1To10")
                    'End If
                    'If dr.Item("11To30").ToString <> DBNull.Value.ToString Then
                    '    Total11to30 = Total11to30 + dr.Item("11To30")
                    'End If
                    'If dr.Item("31To60").ToString <> DBNull.Value.ToString Then
                    '    Total31to60 = Total31to60 + dr.Item("31To60")
                    'End If
                    'If dr.Item("61To90").ToString <> DBNull.Value.ToString Then
                    '    Total61to90 = Total61to90 + dr.Item("61To90")
                    'End If
                    'If dr.Item("91To150").ToString <> DBNull.Value.ToString Then
                    '    Total91to150 = Total91to150 + dr.Item("91To150")
                    'End If
                    'If dr.Item("151To180").ToString <> DBNull.Value.ToString Then
                    '    Total151to180 = Total151to180 + dr.Item("151To180")
                    'End If
                    'If dr.Item(">180").ToString <> DBNull.Value.ToString Then
                    '    Total180 = Total180 + dr.Item(">180")
                    'End If


                Next

                'drTotal.Item(0) = "GrandTotal"
                ''drTotal.Item("TotalOutstanding") = TotalInvoice
                'drTotal.Item("UnpaidBalance") = TotalBalance
                'drTotal.Item("Current") = TotalCurrent
                'drTotal.Item("1To10") = Total1to10
                'drTotal.Item("11To30") = Total11to30
                'drTotal.Item("31To60") = Total31to60
                'drTotal.Item("61To90") = Total61to90
                'drTotal.Item("91To150") = Total91to150
                'drTotal.Item("151To180") = Total151to180
                'drTotal.Item(">180") = Total180
                'dt2.Rows.Add(drTotal)


                Return dt2
            ElseIf rbtnSelect.SelectedValue.ToString = "3" Then
                  Dim command2 As MySqlCommand = New MySqlCommand

                command2.CommandType = CommandType.Text

                Dim qry As String = "SELECT RptCategory as Category , sum(AnnualValue) as AnnualValue  "
                qry = qry + " FROM tblmovementrpt where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"
                qry = qry + "  and reportname='" + rbtnSelect.SelectedValue.ToString + "'"
                qry = qry + " group by RecordLevel "
                qry = qry + " Order BY RecordLevel"

                command2.CommandText = qry
                command2.Connection = conn

                Dim dr2 As MySqlDataReader = command2.ExecuteReader()
                Dim dt2 As New DataTable
                dt2.Load(dr2)

                Dim drTotal As DataRow = dt2.NewRow
                'Dim TotalInvoice As Decimal = 0
                'Dim TotalBalance As Decimal = 0
                'Dim TotalCurrent As Decimal = 0
                'Dim Total1to10 As Decimal = 0
                'Dim Total11to30 As Decimal = 0
                'Dim Total31to60 As Decimal = 0
                'Dim Total61to90 As Decimal = 0
                'Dim Total91to150 As Decimal = 0
                'Dim Total151to180 As Decimal = 0
                'Dim Total180 As Decimal = 0

                'Dim svcaddr As String = ""
                'Dim billcontactperson As String = ""
                'Dim billmobile As String = ""
                'Dim billtel As String = ""
                'Dim billfax As String = ""
                'Dim billtel2 As String = ""
                'Dim billcontact1email As String = ""

                For Each dr As DataRow In dt2.Rows
                    drTotal.Item("Category") = dr.Item("Category")
                    'If dr.Item("UnpaidBalance").ToString <> DBNull.Value.ToString Then
                    '    TotalBalance = TotalBalance + dr.Item("UnpaidBalance")
                    'End If
                    'If dr.Item("Current").ToString <> DBNull.Value.ToString Then
                    '    TotalCurrent = TotalCurrent + dr.Item("Current")
                    'End If
                    'If dr.Item("1To10").ToString <> DBNull.Value.ToString Then
                    '    Total1to10 = Total1to10 + dr.Item("1To10")
                    'End If
                    'If dr.Item("11To30").ToString <> DBNull.Value.ToString Then
                    '    Total11to30 = Total11to30 + dr.Item("11To30")
                    'End If
                    'If dr.Item("31To60").ToString <> DBNull.Value.ToString Then
                    '    Total31to60 = Total31to60 + dr.Item("31To60")
                    'End If
                    'If dr.Item("61To90").ToString <> DBNull.Value.ToString Then
                    '    Total61to90 = Total61to90 + dr.Item("61To90")
                    'End If
                    'If dr.Item("91To150").ToString <> DBNull.Value.ToString Then
                    '    Total91to150 = Total91to150 + dr.Item("91To150")
                    'End If
                    'If dr.Item("151To180").ToString <> DBNull.Value.ToString Then
                    '    Total151to180 = Total151to180 + dr.Item("151To180")
                    'End If
                    'If dr.Item(">180").ToString <> DBNull.Value.ToString Then
                    '    Total180 = Total180 + dr.Item(">180")
                    'End If


                Next

                'drTotal.Item(0) = "GrandTotal"
                ''drTotal.Item("TotalOutstanding") = TotalInvoice
                'drTotal.Item("UnpaidBalance") = TotalBalance
                'drTotal.Item("Current") = TotalCurrent
                'drTotal.Item("1To10") = Total1to10
                'drTotal.Item("11To30") = Total11to30
                'drTotal.Item("31To60") = Total31to60
                'drTotal.Item("61To90") = Total61to90
                'drTotal.Item("91To150") = Total91to150
                'drTotal.Item("151To180") = Total151to180
                'drTotal.Item(">180") = Total180
                'dt2.Rows.Add(drTotal)


                Return dt2
            End If




            ''''''''''''''''''''''''''''''''''''''''''''''''''''


            If rbtnSelect.SelectedValue.ToString = "4" Then
                   Dim command2 As MySqlCommand = New MySqlCommand

                command2.CommandType = CommandType.Text

                Dim qry As String = "SELECT RptCategory as Category , ContractGroupCategory, ContractGroup, sum(AnnualValue) as AnnualValue  "
                qry = qry + " FROM tblmovementrpt where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"
                qry = qry + "  and reportname='" + rbtnSelect.SelectedValue.ToString + "'"
                qry = qry + " group by RecordLevel, ContractGroupCategory, ContractGroup "
                qry = qry + " Order BY RecordLevel,ContractGroupCategory, ContractGroup"

                command2.CommandText = qry
                command2.Connection = conn

                Dim dr2 As MySqlDataReader = command2.ExecuteReader()
                Dim dt2 As New DataTable
                dt2.Load(dr2)

                Dim drTotal As DataRow = dt2.NewRow
                'Dim TotalInvoice As Decimal = 0
                'Dim TotalBalance As Decimal = 0
                'Dim TotalCurrent As Decimal = 0
                'Dim Total1to10 As Decimal = 0
                'Dim Total11to30 As Decimal = 0
                'Dim Total31to60 As Decimal = 0
                'Dim Total61to90 As Decimal = 0
                'Dim Total91to150 As Decimal = 0
                'Dim Total151to180 As Decimal = 0
                'Dim Total180 As Decimal = 0

                'Dim svcaddr As String = ""
                'Dim billcontactperson As String = ""
                'Dim billmobile As String = ""
                'Dim billtel As String = ""
                'Dim billfax As String = ""
                'Dim billtel2 As String = ""
                'Dim billcontact1email As String = ""

                For Each dr As DataRow In dt2.Rows
                    drTotal.Item("Category") = dr.Item("Category")
                    'If dr.Item("UnpaidBalance").ToString <> DBNull.Value.ToString Then
                    '    TotalBalance = TotalBalance + dr.Item("UnpaidBalance")
                    'End If
                    'If dr.Item("Current").ToString <> DBNull.Value.ToString Then
                    '    TotalCurrent = TotalCurrent + dr.Item("Current")
                    'End If
                    'If dr.Item("1To10").ToString <> DBNull.Value.ToString Then
                    '    Total1to10 = Total1to10 + dr.Item("1To10")
                    'End If
                    'If dr.Item("11To30").ToString <> DBNull.Value.ToString Then
                    '    Total11to30 = Total11to30 + dr.Item("11To30")
                    'End If
                    'If dr.Item("31To60").ToString <> DBNull.Value.ToString Then
                    '    Total31to60 = Total31to60 + dr.Item("31To60")
                    'End If
                    'If dr.Item("61To90").ToString <> DBNull.Value.ToString Then
                    '    Total61to90 = Total61to90 + dr.Item("61To90")
                    'End If
                    'If dr.Item("91To150").ToString <> DBNull.Value.ToString Then
                    '    Total91to150 = Total91to150 + dr.Item("91To150")
                    'End If
                    'If dr.Item("151To180").ToString <> DBNull.Value.ToString Then
                    '    Total151to180 = Total151to180 + dr.Item("151To180")
                    'End If
                    'If dr.Item(">180").ToString <> DBNull.Value.ToString Then
                    '    Total180 = Total180 + dr.Item(">180")
                    'End If


                Next

                'drTotal.Item(0) = "GrandTotal"
                ''drTotal.Item("TotalOutstanding") = TotalInvoice
                'drTotal.Item("UnpaidBalance") = TotalBalance
                'drTotal.Item("Current") = TotalCurrent
                'drTotal.Item("1To10") = Total1to10
                'drTotal.Item("11To30") = Total11to30
                'drTotal.Item("31To60") = Total31to60
                'drTotal.Item("61To90") = Total61to90
                'drTotal.Item("91To150") = Total91to150
                'drTotal.Item("151To180") = Total151to180
                'drTotal.Item(">180") = Total180
                'dt2.Rows.Add(drTotal)




                Return dt2
         
            End If
           
            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            'InsertIntoTblWebEventLog("AGEING - " + Session("UserID"), "Page_Load", ex.Message.ToString, lInvoiceNo)
            ''Return dt2
            ''Exit Function
        End Try

    End Function

       Private Function GetDataSet() As DataTable
        Dim dt As New DataTable()
        Dim conn As MySqlConnection = New MySqlConnection()
        Dim cmd As MySqlCommand = New MySqlCommand

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

        Dim sda As New MySqlDataAdapter()
        cmd.CommandType = CommandType.Text


        If rbtnSelect.SelectedValue.ToString = "3" Then
            cmd.CommandText = txtQuerySumm.Text
        ElseIf rbtnSelect.SelectedValue.ToString = "4" Then
            cmd.CommandText = txtQueryCGSumm.Text
     
        ElseIf rbtnSelect.SelectedValue.ToString = "5" Then
            cmd.CommandText = txtQueryRGSumm.Text
        Else

            cmd.CommandText = txtQuery.Text

        End If
    
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

    Protected Sub btnPrintExportToExcel_Click(sender As Object, e As EventArgs) Handles btnPrintExportToExcel.Click
        lblAlert.Text = ""
        If GetData() = True Then
            InsertIntoTblWebEventLog("Excel", txtQuery.Text, txtCreatedBy.Text)


            Dim dt As DataTable = GetDataSet()

            If chkSalesmanIndividual.Checked Then

                '         Dim departments = From r In dt.Rows.OfType(Of DataRow)() Group r By __groupByKey1__ = r("Salesman") Into g Select New With 
                '        {Key
                '    .Department = g.Key, Key
                '        .Data = g
                '}


                WriteExcelWithNPOI(dt, "salesmanattachment")
            Else
                WriteExcelWithNPOI(dt, "xlsx")
            End If

            Return

            Dim attachment As String = ""

            If rbtnSelect.SelectedValue.ToString = "2" Then
                attachment = "attachment; filename=PortfolioDetail" + txtCriteria.Text + ".xls"
            ElseIf rbtnSelect.SelectedValue.ToString = "3" Then
                attachment = "attachment; filename=PortfolioSummary" + txtCriteria.Text + ".xls"
            ElseIf rbtnSelect.SelectedValue.ToString = "4" Then
                attachment = "attachment; filename=PortfolioSummaryByContractGroup" + txtCriteria.Text + ".xls"
            ElseIf rbtnSelect.SelectedValue.ToString = "5" Then
                attachment = "attachment; filename=PortfolioSummaryByReportGroup" + txtCriteria.Text + ".xls"
            End If


            Response.ClearContent()
            Response.AddHeader("content-disposition", attachment)
            Response.ContentType = "application/vnd.ms-excel"
            Dim tab As String = ""
            For Each dc As DataColumn In dt.Columns
                Response.Write(tab + dc.ColumnName)
                tab = vbTab
            Next
            Response.Write(vbLf)
            Dim i As Integer
            For Each dr As DataRow In dt.Rows
                tab = ""
                For i = 0 To dt.Columns.Count - 1
                    Response.Write(tab & dr(i).ToString())
                    tab = vbTab
                Next
                Response.Write(vbLf)
            Next
            Response.[End]()

            dt.Clear()

        End If

    End Sub

    Private Function GetData() As Boolean
        lblAlert.Text = ""

        If String.IsNullOrEmpty(txtSvcDateFrom.Text) Then
            lblAlert.Text = "ENTER DATE FROM"
            Return False
        End If

        If String.IsNullOrEmpty(txtSvcDateTo.Text) Then
            lblAlert.Text = "ENTER DATE TO"
            Return False
        End If

        If String.IsNullOrEmpty(txtSvcDateFrom.Text) = False And String.IsNullOrEmpty(txtSvcDateTo.Text) = False Then
            If Convert.ToDateTime(txtSvcDateFrom.Text) > Convert.ToDateTime(txtSvcDateTo.Text) Then
                lblAlert.Text = "DATE FROM should be less than DATE TO"
                Return False
            End If
        End If


        '''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim strSql As String = "INSERT INTO tblEventLog (StaffID,Module,DocRef,Action,ComputerName," & _
              "Serial, LogDate, Comments,SOURCESQLID) " & _
              "VALUES (@StaffID, @Module, @DocRef, @Action, @ComputerName, @Serial, @LogDate,  @Comments, @SOURCESQLID)"
        '"VALUES (@StaffID, @Module, @DocRef, @Action, @ComputerName, @Serial, @LogDate, @Amount, @BaseValue, @BaseGSTValue, @CustCode, @Comments, @SOURCESQLID)"


        Dim command As MySqlCommand = New MySqlCommand
        command.CommandType = CommandType.Text
        command.CommandText = strSql
        command.Parameters.Clear()
        'Convert.ToDateTime(txtContractStart.Text).ToString("yyyy-MM-dd"))
        command.Parameters.AddWithValue("@StaffID", Session("UserID"))
        command.Parameters.AddWithValue("@Module", "REPORTS")
        command.Parameters.AddWithValue("@DocRef", "PORTFOLIO")
        command.Parameters.AddWithValue("@Action", "")
        command.Parameters.AddWithValue("@ComputerName", Strings.Left(My.Computer.Name.ToString, 20))
        command.Parameters.AddWithValue("@Serial", "")
        'command.Parameters.AddWithValue("@LogDate", Convert.ToString(Session("SysDate")))
        command.Parameters.AddWithValue("@LogDate", Convert.ToDateTime(Session("SysTime")))
        'command.Parameters.AddWithValue("@Amount", 0)
        'command.Parameters.AddWithValue("@BaseValue", 0)
        'command.Parameters.AddWithValue("@BaseGSTValue", 0)
        'command.Parameters.AddWithValue("@CustCode", "")
        command.Parameters.AddWithValue("@Comments", "")
        command.Parameters.AddWithValue("@SOURCESQLID", 0)
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        'Dim conn As MySqlConnection = New MySqlConnection(constr)
        conn.Open()
        command.Connection = conn
        command.ExecuteNonQuery()

        command.Dispose()


        Dim startdate As DateTime
        Dim enddate As DateTime
        Dim selection As String
        selection = ""

        Dim criteria As String = ""
        Dim ContractGroup As String = String.Empty
        'Dim CompanyGroup As String = String.Empty
        'Dim Salesman As String = String.Empty

        If String.IsNullOrEmpty(txtSvcDateFrom.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtSvcDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then
            Else
                MessageBox.Message.Alert(Page, "Service Date From is invalid", "str")
                Return False
            End If
            startdate = Convert.ToDateTime(txtSvcDateFrom.Text).ToString("yyyy-MM-dd")
            If selection = "" Then
                selection = "Date >= " + d.ToString("dd-MM-yyyy")

                criteria = "_" + d.ToString("yyyyMMdd")
            Else
                selection = selection + ", Date >= " + d.ToString("dd-MM-yyyy")
            End If
        Else
            startdate = DateTime.MinValue
        End If

        If String.IsNullOrEmpty(txtSvcDateTo.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtSvcDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then
            Else
                MessageBox.Message.Alert(Page, "Service Date To is invalid", "str")
                Return False
            End If
            enddate = Convert.ToDateTime(txtSvcDateTo.Text).ToString("yyyy-MM-dd")
            If selection = "" Then
                selection = "Date <= " + d.ToString("dd-MM-yyyy")
                criteria = criteria + "_" + d.ToString("yyyyMMdd")
            Else
                selection = selection + ", Date <= " + d.ToString("dd-MM-yyyy")
                criteria = criteria + "_" + d.ToString("yyyyMMdd")
            End If
        Else
            enddate = DateTime.MaxValue
        End If


        If ddlCompanyGrp.Text = "-1" Then
        Else
            ' selformula = selformula + " and {tblmovementrpt1.CompanyGroup}='" + ddlCompanyGrp.Text + "'"
            'qry = qry + " and CompanyGroup = '" + ddlCompanyGrp.Text + "'"
            'qrySumm = qrySumm + " and CompanyGroup = '" + ddlCompanyGrp.Text + "'"
            'qryCGSumm = qryCGSumm + " and CompanyGroup = '" + ddlCompanyGrp.Text + "'"

            criteria = criteria + "_" + ddlCompanyGrp.Text

            If selection = "" Then
                selection = "CompanyGroup : " + ddlCompanyGrp.Text
            Else
                selection = selection + ", CompanyGroup : " + ddlCompanyGrp.Text
            End If
        End If

        'If ddlContractGroup.Text = "-1" Then
        'Else
        '    selformula = selformula + " and {tblmovementrpt1.ContractGroup}='" + ddlContractGroup.Text + "'"
        '    qry = qry + " and ContractGroup = '" + ddlContractGroup.Text + "'"
        '    qrySumm = qrySumm + " and ContractGroup = '" + ddlContractGroup.Text + "'"

        '    criteria = criteria + "_" + ddlContractGroup.Text


        '    If selection = "" Then
        '        selection = "ContractGroup : " + ddlContractGroup.Text
        '    Else
        '        selection = selection + ", ContractGroup : " + ddlContractGroup.Text
        '    End If
        'End If


        Dim YrStrList2 As List(Of [String]) = New List(Of String)()
        Dim YrStrList3 As List(Of [String]) = New List(Of String)()

        For Each item As ListItem In txtServiceID.Items
            If item.Selected Then

                YrStrList2.Add("""" + item.Value + """")
                YrStrList3.Add(item.Value)

            End If
        Next

        If YrStrList2.Count > 0 Then

            Dim YrStr As [String] = [String].Join(",", YrStrList2.ToArray)
            Dim YrStr2 As [String] = [String].Join(",", YrStrList3.ToArray)

            '  selFormula = selFormula + " and {tblservicerecorddet1.ServiceID} in [" + YrStr + "]"
            'selformula = selformula + " and ({tblmovementrpt1.ContractGroup} in [" + YrStr + "]"
            'selformula = selformula + " or {tblmovementrpt1.ContractGroup} = '" + YrStr2 + "')"

            If selection = "" Then
                selection = "ContractGroup : " + YrStr
            Else
                selection = selection + ", ContractGroup : " + YrStr
            End If

            ContractGroup = YrStr


            'qry = qry + " and ContractGroup in (" + YrStr2 + ")"
            'qrySumm = qrySumm + " and ContractGroup in (" + YrStr2 + ")"
            'qryCGSumm = qryCGSumm + " and ContractGroup in (" + YrStr2 + ")"

          
            '    qry = qry + " and ContractGroup = '" + ddlContractGroup.Text + "'"
            '    qrySumm = qrySumm + " and ContractGroup = '" + ddlContractGroup.Text + "'"


        End If

        'If ddlCategory.Text = "-1" Then
        'Else
        '    If selection = "" Then
        '        selection = "Category : " + ddlCategory.Text
        '    Else
        '        selection = selection + ", Category : " + ddlCategory.Text
        '    End If
        'End If
        If ddlSalesMan.Text = "-1" Then
        Else
            'selformula = selformula + " and {tblmovementrpt1.Salesman}='" + ddlSalesMan.Text + "'"
            'qry = qry + " and Salesman = '" + ddlSalesMan.Text + "'"
            'qrySumm = qrySumm + " and Salesman = '" + ddlSalesMan.Text + "'"
            'qryCGSumm = qryCGSumm + " and Salesman = '" + ddlSalesMan.Text + "'"

            criteria = criteria + "_" + ddlSalesMan.Text

            If selection = "" Then
                selection = "Salesman = " + ddlSalesMan.Text
            Else
                selection = selection + ", Salesman = " + ddlSalesMan.Text
            End If
        End If

        Dim qryCGSumm As String = ""
        Dim qryRGSumm As String = ""

        Dim qryOpening As String = ""
        Dim selformula As String = "{tblmovementrpt1.ReportName}='" + rbtnSelect.SelectedValue.ToString + "' and {tblmovementrpt1.CreatedBy}='" + txtCreatedBy.Text + "'"

        Dim qry As String = "SELECT RecordLevel,RptCategory as Category,RptSubCategory as SubCategory,"
        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            qry = qry + "Location,"
            If selection = "" Then
                selection = "Branch/Location : " + Convert.ToString(Session("Branch"))
            Else
                selection = selection + ", Branch/Location : " + Convert.ToString(Session("Branch"))
            End If
        End If

        qry = qry + "Salesman,ContractGroup,ReportGroup,CompanyGroup,Industry,ContractStatus,ContractNo,ContactType,AccountID,"
        If rbtnSelect.SelectedValue.ToString = "6" Then
            qry = qry + "(SELECT group_concat(distinct LocationID separator ',') FROM TBLContractdet where ContractNo=A.ContractNo and ContractNo<>'') as LocationID,"
        End If
        qry = qry + "replace(replace(replace(trim(CustName), char(10), ' '), char(13), ' '),'\t', ' ') as 'Customer Name',"
        qry = qry + "replace(replace(replace(ServiceAddress, char(10), ' '), char(13), ' '),'\t', ' ') as ServiceAddress,replace(replace(replace(ServiceDescription, char(10), ' '), char(13), ' '),'\t', ' ') as ServiceDescription,"
        qry = qry + "AgreeValue as LatestAgreeValue,Duration,DurationMS,ServStartDate as StartDate,EndDate,ActualEnd,AnnualValue as PortfolioValue,Percentage/100 as Percentage,PortfolioTerminationDate as TransactionDate,"
        qry = qry + "concat(CASE WHEN INCLUDEINFINALREPORT='YES' THEN 'INCLUDED - ' WHEN INCLUDEINFINALREPORT='NO' THEN 'NOT INCLUDED - ' ELSE '' END,TerminationCode,' - ',TerminationDescription) as TerminationCode,replace(replace(trim(TerminationComments), char(10), ' '), char(13), ' ') as 'Comments',IncludeInFinalReport,"
        qry = qry + "OnHoldCode,OnHoldDate,OnHoldDescription,OnHoldComments"
        qry = qry + " FROM tblmovementrpt A where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"
        qry = qry + "  and reportname='" + rbtnSelect.SelectedValue.ToString + "'"
     

        Dim qrySumm As String = "SELECT RptCategory as Category,RptSubCategory as SubCategory,sum(AnnualValue) as PortfolioValue  "
        qrySumm = qrySumm + " FROM tblmovementrpt where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"
        qrySumm = qrySumm + "  and reportname='" + rbtnSelect.SelectedValue.ToString + "'"


        qryCGSumm = "SELECT RptCategory as Category,RptSubCategory as SubCategory,ContractGroupCategory,ContractGroup,sum(AnnualValue) as PortfolioValue  "
        qrycgSumm = qrycgSumm + " FROM tblmovementrpt where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"
        qrycgSumm = qrycgSumm + "  and reportname='" + rbtnSelect.SelectedValue.ToString + "'"

        qryRGSumm = "SELECT RptCategory as Category,RptSubCategory as SubCategory,ReportGroup,sum(AnnualValue) as PortfolioValue  "
        qryRGSumm = qryRGSumm + " FROM tblmovementrpt where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"
        qryRGSumm = qryRGSumm + "  and reportname='" + rbtnSelect.SelectedValue.ToString + "'"


        If chkExcludeTerminationContracts.Checked = True Then
            qry = qry + " and (INCLUDEINFINALREPORT = 'YES' OR INCLUDEINFINALREPORT IS NULL)"
            qrySumm = qrySumm + " and (INCLUDEINFINALREPORT = 'YES' OR INCLUDEINFINALREPORT IS NULL)"
            qryCGSumm = qryCGSumm + " and (INCLUDEINFINALREPORT = 'YES' OR INCLUDEINFINALREPORT IS NULL)"
            qryRGSumm = qryRGSumm + " and (INCLUDEINFINALREPORT = 'YES' OR INCLUDEINFINALREPORT IS NULL)"
        End If

        If rbtnSelect.SelectedValue.ToString = "2" Then

            If chkIncludeOpeningDetails.Checked = True Then
                Session.Add("OpeningDetail", "True")
                If selection = "" Then
                    selection = "Include Opening Details"
                Else
                    selection = selection + ", Include Opening Details"
                End If
            Else
                Session.Add("OpeningDetail", "False")
                qry = qry + " and rptcategory<>'opening'"
                qryOpening = "SELECT RecordLevel,RptCategory as Category,RptSubCategory as SubCategory,"
                qryOpening = qryOpening + "'' as Salesman,'' as ContractGroup,'' as CompanyGroup,'' as Industry,'' as ContractStatus,"
                qryOpening = qryOpening + "'' as ContractNo,'' as AccountID,'' as 'Customer Name','' as AgreeValue,'' as Duration,'' as DurationMS,'' as StartDate,'' as EndDate,'' as ActualEnd,sum(AnnualValue) as PortfolioValue,'' as TransactionDate,'' as TerminationCode,'' as Comments"
                qryOpening = qryOpening + " FROM tblmovementrpt where CreatedBy='" + Convert.ToString(Session("UserID")) + "' and reportname='" + rbtnSelect.SelectedValue.ToString + "'"
                qryOpening = qryOpening + " and rptcategory='opening' group by rptcategory"
            End If

        End If

        'qryRGSumm = qryCGSumm
        'qryRGSumm = qryRGSumm.Replace(",ContractGroup,", ",ReportGroup,")

        qry = qry + " Order BY RecordLevel,Category,SubCategory,"
        qrySumm = qrySumm + " group by RecordLevel,RptSubCategory "
        qrySumm = qrySumm + " Order BY RecordLevel,RptCategory,RptSubCategory,"

        qryCGSumm = qryCGSumm + " group BY RecordLevel,RptCategory,RptSubCategory,ContractGroupCategory,ContractGroup"
        qryCGSumm = qryCGSumm + " order BY RecordLevel,RptCategory,RptSubCategory,"

        qryRGSumm = qryRGSumm + " group BY RecordLevel,RptCategory,RptSubCategory,ReportGroup"
        qryRGSumm = qryRGSumm + " order BY RecordLevel,RptCategory,RptSubCategory,"


        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            qry = qry + "Location,"
            qrySumm = qrySumm + "Location,"
            qryCGSumm = qryCGSumm + "Location,"
            qryRGSumm = qryRGSumm + "Location,"
        End If
        qry = qry + "Salesman,ContractGroup,ContractStatus,AccountID,contractno"
        qrySumm = qrySumm + "Salesman,ContractGroup,CompanyGroup,ContractStatus,AccountID,contractno"
        qryCGSumm = qryCGSumm + "ContractGroup,Salesman,CompanyGroup,ContractStatus,AccountID,contractno"
        qryRGSumm = qryRGSumm + "ReportGroup,Salesman,CompanyGroup,ContractStatus,AccountID,contractno"

        'If ddlCompanyGrp.Text = "-1" And ddlSalesMan.Text = "-1" And String.IsNullOrEmpty(ContractGroup) = False Then

        '    qry = qry + " and rptcategory not in ('CLOSING','NET GAIN')"
        '    qry = qry + " UNION " + qrynew + " and rptcategory in ('CLOSING','NET GAIN')"
        'End If

        If rbtnSelect.SelectedValue.ToString = "2" And chkIncludeOpeningDetails.Checked = False Then
            txtQuery.Text = qryOpening + " union " + qry
        Else
            txtQuery.Text = qry
        End If

        txtQuerySumm.Text = qrySumm
        txtQueryCGSumm.Text = qryCGSumm
        txtQueryRGSumm.Text = qryRGSumm

        txtCriteria.Text = criteria

        If chkSalesmanIndividual.Checked Then
            If ddlSalesMan.Text = "-1" Then

            End If
        End If

        InsertIntoTblWebEventLog("GetData", Session("Branch"), txtContractNo.Text)

        Dim command1 As MySqlCommand = New MySqlCommand
        command1.CommandType = CommandType.StoredProcedure

        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            If String.IsNullOrEmpty(txtContractNo.Text) Then
                command1.CommandText = "spportfoliomovementnewDistLocation1"
                InsertIntoTblWebEventLog("GetData1", Session("Branch"), txtContractNo.Text)

            Else
                command1.CommandText = "spportfoliomovementnewDistContractLocation1"
            End If
        Else
            If String.IsNullOrEmpty(txtContractNo.Text) Then
                command1.CommandText = "spportfoliomovementDistributionNew11"
            Else
                command1.CommandText = "spportfoliomovementContractDistribution"
            End If
        End If

        ''If RadioButtonList1.SelectedValue = "1" Then
        ''    command1.CommandText = "spportfoliomovement"
        ''ElseIf RadioButtonList1.SelectedValue = "2" Then
        'If Convert.ToString(Session("LocationEnabled")) = "Y" Then
        '    If String.IsNullOrEmpty(txtContractNo.Text) Then
        '        ' If chkDistribution.Checked = True Then
        '        command1.CommandText = "spportfoliomovementnewDistLocation"
        '        'Else
        '        '    command1.CommandText = "spportfoliomovementnewLocation"
        '        'End If

        '    Else
        '        ' If chkDistribution.Checked = True Then
        '        command1.CommandText = "spportfoliomovementnewDistContractLocation"
        '        '    Else
        '        '    command1.CommandText = "spportfoliomovementnewContractLocation"
        '        'End If

        '    End If

        'Else
        '    If String.IsNullOrEmpty(txtContractNo.Text) Then
        '        'If chkDistribution.Checked = True Then
        '        command1.CommandText = "spportfoliomovementDistributionNew"
        '        'Else
        '        '    command1.CommandText = "spportfoliomovementnew1"
        '        'End If

        '    Else

        '        'If chkDistribution.Checked = True Then
        '        command1.CommandText = "spportfoliomovementContractDistribution"
        '        '    Else
        '        '    command1.CommandText = "spportfoliomovementContractnew1"
        '        'End If

        '    End If

        'End If

        '  End If

        command1.Parameters.Clear()

        command1.Parameters.AddWithValue("@pr_startdate", startdate.ToString("yyyy-MM-dd"))
        command1.Parameters.AddWithValue("@pr_enddate", enddate.ToString("yyyy-MM-dd"))
        command1.Parameters.AddWithValue("@pr_ReportName", rbtnSelect.SelectedValue.ToString)
        command1.Parameters.AddWithValue("@pr_CreatedBy", txtCreatedBy.Text)
        If ddlCompanyGrp.Text = "-1" Then
            command1.Parameters.AddWithValue("@pr_CompanyGroup", DBNull.Value.ToString)
        Else
            command1.Parameters.AddWithValue("@pr_CompanyGroup", ddlCompanyGrp.Text)
        End If
        If ddlSalesMan.Text = "-1" Then
            command1.Parameters.AddWithValue("@pr_SalesMan", DBNull.Value.ToString)
        Else
            command1.Parameters.AddWithValue("@pr_SalesMan", ddlSalesMan.Text)
        End If
        If ContractGroup = String.Empty Then
            command1.Parameters.AddWithValue("@pr_ContractGroup", DBNull.Value.ToString)
        Else
            command1.Parameters.AddWithValue("@pr_ContractGroup", ContractGroup)
        End If

        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            command1.Parameters.AddWithValue("pr_Location", Convert.ToString(Session("Branch")))
        End If

        If String.IsNullOrEmpty(txtContractNo.Text) = False Then
            command1.Parameters.AddWithValue("pr_ContractNo", txtContractNo.Text.Trim)
        End If
        If chkZeroValueRevisions.Checked Then
            command1.Parameters.AddWithValue("@pr_Revisions", "True")
        Else
            command1.Parameters.AddWithValue("@pr_Revisions", "False")
        End If
        command1.Connection = conn
        command1.ExecuteScalar()

        command1.Dispose()

        conn.Close()
        conn.Dispose()


        Session.Add("selFormula", selformula)
        Session.Add("selection", selection)

        Return True

    End Function

    Public Sub WriteExcelWithNPOI(ByVal dt As DataTable, ByVal extension As String)
        Dim workbook As IWorkbook

        If extension = "xlsx" Or extension = "salesmanattachment" Then
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
        '  cell1.SetCellValue(Session("Selection").ToString)
        cell1.SetCellValue("(" + ConfigurationManager.AppSettings("DomainName").ToString() + ")" + Session("Selection").ToString)

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

        Dim _percentCellStyle As ICellStyle = Nothing

        If _percentCellStyle Is Nothing Then
            _percentCellStyle = workbook.CreateCellStyle()
            _percentCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium
            _percentCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium
            _percentCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium
            _percentCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium

            _percentCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.00%")
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

        If rbtnSelect.SelectedValue.ToString = "2" Then
            If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim row As IRow = sheet1.CreateRow(i + 2)

                    For j As Integer = 0 To dt.Columns.Count - 1
                        Dim cell As ICell = row.CreateCell(j)

                        '  If j = 14 Or j = 15 Or j = 20 Then
                        '  If j = 15 Or j = 16 Or j = 21 Then
                        If j = 16 Or j = 17 Or j = 22 Then
                            If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                cell.SetCellValue(d)
                            Else
                                Dim d As Double = Convert.ToDouble("0.00")
                                cell.SetCellValue(d)

                            End If
                            cell.CellStyle = _doubleCellStyle
                        ElseIf j = 23 Then
                            If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                cell.SetCellValue(d)
                            Else
                                If chkDistribution.Checked Then
                                    Dim d As Double = Convert.ToDouble("0.00%")
                                    cell.SetCellValue(d)
                                Else
                                    Dim d As Double = Convert.ToDouble("0.00")
                                    cell.SetCellValue(d)
                                End If



                            End If
                            cell.CellStyle = _percentCellStyle
                            ' ElseIf j = 17 Or j = 18 Or j = 19 Or j = 21 Then
                            '   ElseIf j = 18 Or j = 19 Or j = 20 Or j = 23 Then
                        ElseIf j = 19 Or j = 20 Or j = 21 Or j = 24 Then
                            If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
                                cell.SetCellValue(d)
                                'Else
                                '    Dim d As Double = Convert.ToDouble("0.00")
                                '    cell.SetCellValue(d)

                            End If
                            cell.CellStyle = dateCellStyle
                        Else
                            cell.SetCellValue(dt.Rows(i)(j).ToString)
                            cell.CellStyle = AllCellStyle

                        End If
                        If i = dt.Rows.Count - 1 Then
                            sheet1.AutoSizeColumn(j)
                        End If
                    Next
                Next
            Else
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim row As IRow = sheet1.CreateRow(i + 2)

                    For j As Integer = 0 To dt.Columns.Count - 1
                        Dim cell As ICell = row.CreateCell(j)
                        '    If j = 14 Or j = 15 Or j = 20 Then
                        If j = 15 Or j = 16 Or j = 21 Then
                            'If j = 13 Or j = 14 Or j = 19 Then
                            If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                cell.SetCellValue(d)
                            Else
                                Dim d As Double = Convert.ToDouble("0.00")
                                cell.SetCellValue(d)

                            End If
                            cell.CellStyle = _doubleCellStyle
                        ElseIf j = 22 Then
                            'If j = 13 Or j = 14 Or j = 19 Then
                            If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                cell.SetCellValue(d)
                            Else
                                If chkDistribution.Checked Then
                                    Dim d As Double = Convert.ToDouble("0.00%")
                                    cell.SetCellValue(d)
                                Else
                                    Dim d As Double = Convert.ToDouble("0.00")
                                    cell.SetCellValue(d)
                                End If

                            End If
                            cell.CellStyle = _percentCellStyle
                            '   ElseIf j = 16 Or j = 17 Or j = 18 Or j = 20 Then
                            '  ElseIf j = 17 Or j = 18 Or j = 19 Or j = 22 Then
                        ElseIf j = 18 Or j = 19 Or j = 20 Or j = 23 Then
                            If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
                                cell.SetCellValue(d)
                                'Else
                                '    Dim d As Double = Convert.ToDouble("0.00")
                                '    cell.SetCellValue(d)

                            End If
                            cell.CellStyle = dateCellStyle
                        Else
                            cell.SetCellValue(dt.Rows(i)(j).ToString)
                            cell.CellStyle = AllCellStyle

                        End If
                        If i = dt.Rows.Count - 1 Then
                            sheet1.AutoSizeColumn(j)
                        End If
                    Next
                Next
            End If

        ElseIf rbtnSelect.SelectedValue.ToString = "3" Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim row As IRow = sheet1.CreateRow(i + 2)

                For j As Integer = 0 To dt.Columns.Count - 1
                    Dim cell As ICell = row.CreateCell(j)

                    If j = 2 Then
                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                            cell.SetCellValue(d)
                        Else
                            Dim d As Double = Convert.ToDouble("0.00")
                            cell.SetCellValue(d)
                        End If
                        cell.CellStyle = _doubleCellStyle
                    ElseIf j = -1 Then
                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                            Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
                            cell.SetCellValue(d)
                            'Else
                            '    Dim d As Double = Convert.ToDouble("0.00")
                            '    cell.SetCellValue(d)
                        End If
                        cell.CellStyle = dateCellStyle
                    Else
                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                        cell.CellStyle = AllCellStyle
                    End If
                    If i = dt.Rows.Count - 1 Then
                        sheet1.AutoSizeColumn(j)
                    End If
                Next
            Next
        ElseIf rbtnSelect.SelectedValue.ToString = "4" Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim row As IRow = sheet1.CreateRow(i + 2)

                For j As Integer = 0 To dt.Columns.Count - 1
                    Dim cell As ICell = row.CreateCell(j)

                    If j = 4 Then
                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                            cell.SetCellValue(d)
                        Else
                            Dim d As Double = Convert.ToDouble("0.00")
                            cell.SetCellValue(d)

                        End If
                        cell.CellStyle = _doubleCellStyle
                    ElseIf j = -1 Then
                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                            Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
                            cell.SetCellValue(d)
                            'Else
                            '    Dim d As Double = Convert.ToDouble("0.00")
                            '    cell.SetCellValue(d)

                        End If
                        cell.CellStyle = dateCellStyle
                    Else
                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                        cell.CellStyle = AllCellStyle

                    End If
                    If i = dt.Rows.Count - 1 Then
                        sheet1.AutoSizeColumn(j)
                    End If
                Next
            Next
        ElseIf rbtnSelect.SelectedValue.ToString = "5" Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim row As IRow = sheet1.CreateRow(i + 2)

                For j As Integer = 0 To dt.Columns.Count - 1
                    Dim cell As ICell = row.CreateCell(j)

                    If j = 3 Then
                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                            cell.SetCellValue(d)
                        Else
                            Dim d As Double = Convert.ToDouble("0.00")
                            cell.SetCellValue(d)

                        End If
                        cell.CellStyle = _doubleCellStyle
                    ElseIf j = -1 Then
                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                            Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
                            cell.SetCellValue(d)
                            'Else
                            '    Dim d As Double = Convert.ToDouble("0.00")
                            '    cell.SetCellValue(d)

                        End If
                        cell.CellStyle = dateCellStyle
                    Else
                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                        cell.CellStyle = AllCellStyle

                    End If
                    If i = dt.Rows.Count - 1 Then
                        sheet1.AutoSizeColumn(j)
                    End If
                Next
            Next
        ElseIf rbtnSelect.SelectedValue.ToString = "6" Then
            If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim row As IRow = sheet1.CreateRow(i + 2)

                    For j As Integer = 0 To dt.Columns.Count - 1
                        Dim cell As ICell = row.CreateCell(j)

                        '  If j = 14 Or j = 15 Or j = 20 Then
                        '  If j = 15 Or j = 16 Or j = 21 Then
                        ' If j = 16 Or j = 17 Or j = 22 Then
                        If j = 17 Or j = 18 Or j = 23 Then
                            If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                cell.SetCellValue(d)
                            Else
                                Dim d As Double = Convert.ToDouble("0.00")
                                cell.SetCellValue(d)

                            End If
                            cell.CellStyle = _doubleCellStyle
                        ElseIf j = 24 Then
                            If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                cell.SetCellValue(d)
                            Else
                                If chkDistribution.Checked Then
                                    Dim d As Double = Convert.ToDouble("0.00%")
                                    cell.SetCellValue(d)
                                Else
                                    Dim d As Double = Convert.ToDouble("0.00")
                                    cell.SetCellValue(d)
                                End If



                            End If
                            cell.CellStyle = _percentCellStyle
                            ' ElseIf j = 17 Or j = 18 Or j = 19 Or j = 21 Then
                            '   ElseIf j = 18 Or j = 19 Or j = 20 Or j = 23 Then
                        ElseIf j = 20 Or j = 21 Or j = 22 Or j = 25 Then
                            If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
                                cell.SetCellValue(d)
                                'Else
                                '    Dim d As Double = Convert.ToDouble("0.00")
                                '    cell.SetCellValue(d)

                            End If
                            cell.CellStyle = dateCellStyle
                        Else
                            cell.SetCellValue(dt.Rows(i)(j).ToString)
                            cell.CellStyle = AllCellStyle

                        End If
                        If i = dt.Rows.Count - 1 Then
                            sheet1.AutoSizeColumn(j)
                        End If
                    Next
                Next
            Else
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim row As IRow = sheet1.CreateRow(i + 2)

                    For j As Integer = 0 To dt.Columns.Count - 1
                        Dim cell As ICell = row.CreateCell(j)
                        '    If j = 14 Or j = 15 Or j = 20 Then
                        If j = 16 Or j = 17 Or j = 22 Then
                            'If j = 13 Or j = 14 Or j = 19 Then
                            If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                cell.SetCellValue(d)
                            Else
                                Dim d As Double = Convert.ToDouble("0.00")
                                cell.SetCellValue(d)

                            End If
                            cell.CellStyle = _doubleCellStyle
                        ElseIf j = 23 Then
                            'If j = 13 Or j = 14 Or j = 19 Then
                            If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                cell.SetCellValue(d)
                            Else
                                If chkDistribution.Checked Then
                                    Dim d As Double = Convert.ToDouble("0.00%")
                                    cell.SetCellValue(d)
                                Else
                                    Dim d As Double = Convert.ToDouble("0.00")
                                    cell.SetCellValue(d)
                                End If

                            End If
                            cell.CellStyle = _percentCellStyle
                            '   ElseIf j = 16 Or j = 17 Or j = 18 Or j = 20 Then
                            '  ElseIf j = 17 Or j = 18 Or j = 19 Or j = 22 Then
                        ElseIf j = 19 Or j = 20 Or j = 21 Or j = 24 Then
                            If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
                                cell.SetCellValue(d)
                                'Else
                                '    Dim d As Double = Convert.ToDouble("0.00")
                                '    cell.SetCellValue(d)

                            End If
                            cell.CellStyle = dateCellStyle
                        Else
                            cell.SetCellValue(dt.Rows(i)(j).ToString)
                            cell.CellStyle = AllCellStyle

                        End If
                        If i = dt.Rows.Count - 1 Then
                            sheet1.AutoSizeColumn(j)
                        End If
                    Next
                Next
            End If
        End If

        Using exportData = New MemoryStream()
            Response.Clear()

            '  Dim criteria As String = "_" + txtSvcDateFrom.Text + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmss")
            Dim attachment As String = ""

            If rbtnSelect.SelectedValue.ToString = "2" Then
                attachment = "attachment; filename=PortfolioDetail" + txtCriteria.Text
            ElseIf rbtnSelect.SelectedValue.ToString = "3" Then
                attachment = "attachment; filename=PortfolioSummary" + txtCriteria.Text
            ElseIf rbtnSelect.SelectedValue.ToString = "4" Then
                attachment = "attachment; filename=PortfolioSummaryByContractGroup" + txtCriteria.Text
            ElseIf rbtnSelect.SelectedValue.ToString = "5" Then
                attachment = "attachment; filename=PortfolioSummaryByReportGroup" + txtCriteria.Text
            ElseIf rbtnSelect.SelectedValue.ToString = "6" Then
                attachment = "attachment; filename=PortfolioDetailwithLocationID" + txtCriteria.Text
            End If

            If extension = "xlsx" Then
                workbook.Write(exportData)
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Response.AddHeader("Content-Disposition", String.Format(attachment + ".xlsx"))
                Response.BinaryWrite(exportData.ToArray())
            ElseIf extension = "xls" Then
                workbook.Write(exportData)
                Response.ContentType = "application/vnd.ms-excel"
                Response.AddHeader("Content-Disposition", String.Format(attachment + ".xls"))
                Response.BinaryWrite(exportData.GetBuffer())
            ElseIf extension = "salesmanattachment" Then
                If (Not System.IO.Directory.Exists(Server.MapPath("~/Uploads/Portfolio/") + txtCreatedBy.Text + "\" + DateTime.Now.ToString("yyyy-MM-dd") + "\")) Then
                    System.IO.Directory.CreateDirectory(Server.MapPath("~/Uploads/Portfolio/") + txtCreatedBy.Text + "\" + DateTime.Now.ToString("yyyy-MM-dd") + "\")
                End If

                Dim filePath As String = ""
                filePath = Server.MapPath("~/Uploads/Portfolio/") + txtCreatedBy.Text + "/" + DateTime.Now.ToString("yyyy-MM-dd") + "/" + ddlSalesMan.Text + ".xlsx"
                Dim xfile As FileStream = New FileStream(filePath, FileMode.Create, System.IO.FileAccess.Write)
                workbook.Write(xfile)
                xfile.Close()

                'Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                'Response.AppendHeader("Content-Disposition", String.Format(ddlSalesMan.Text + ".xlsx"))
                'Response.BinaryWrite(exportData.ToArray())
                'Response.WriteFile(filePath)
                'Response.End()

                'Response.ContentType = ContentType
                'Response.AppendHeader("Content-Disposition", ("attachment; filename=" + Path.GetFileName(filePath)))
                'Response.WriteFile(filePath)
                'Response.End()
            End If

            Response.[End]()
        End Using
    End Sub

    Protected Sub rbtnSelect_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rbtnSelect.SelectedIndexChanged
        If rbtnSelect.SelectedValue.ToString = "2" Then
            chkIncludeOpeningDetails.Enabled = True
            chkGroupByContactType.Enabled = True

        ElseIf rbtnSelect.SelectedValue.ToString = "3" Then
            chkIncludeOpeningDetails.Enabled = False
            chkGroupByContactType.Enabled = True

        ElseIf rbtnSelect.SelectedValue.ToString = "4" Then
            chkIncludeOpeningDetails.Enabled = False
            chkGroupByContactType.Enabled = True

        End If
    End Sub

    Public Sub InsertIntoTblWebEventLog(events As String, errorMsg As String, ID As String)
        Try
            Dim conn As New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

            Dim insCmds As New MySqlCommand()
            insCmds.CommandType = CommandType.Text

            Dim insQuery As String = "Insert into tblWebEventLog(LoginId, Event, Error,ID, CreatedOn)"
            insQuery += " values(@LoginId,@Event,@Error,@ID,@CreatedOn);"

            insCmds.CommandText = insQuery

            insCmds.Parameters.Clear()
            insCmds.Parameters.AddWithValue("@LoginId", "PORTFOLIO MOVEMENT - " + Session("UserID"))
            insCmds.Parameters.AddWithValue("@Event", events)
            insCmds.Parameters.AddWithValue("@Error", errorMsg)
            insCmds.Parameters.AddWithValue("@ID", ID)
            insCmds.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))

            conn.Open()
            insCmds.Connection = conn
            insCmds.ExecuteNonQuery()
            conn.Close()
            conn.Dispose()
        Catch ex As Exception
            InsertIntoTblWebEventLog("InsertIntoTblWebEventLog", ex.Message.ToString, txtCreatedBy.Text)
        End Try
    End Sub
End Class

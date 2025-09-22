Imports Microsoft.VisualBasic
Imports System
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Web.Services
Imports System.Web.Script.Services
Imports System.IO
Imports System.Web.Script.Serialization
Imports System.Threading.Tasks
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports Microsoft.IdentityModel.Clients.ActiveDirectory
Imports System.Net
Imports NPOI.HSSF.UserModel
Imports NPOI.SS.UserModel
Imports NPOI.SS.Util

Partial Class FloorPlan
    Inherits System.Web.UI.Page
    Private token As AuthenticationResult

    Dim apiGetSitesPath As String = ConfigurationManager.AppSettings("apiGetSites")
    Dim apiGetDevicesBySiteIDPath As String = ConfigurationManager.AppSettings("apiGetDevicesBySiteID")
    Dim apiGetDeviceByDeviceIDPath As String = ConfigurationManager.AppSettings("apiGetDevicesByDeviceID")
    Dim apiGetDeviceTypeByDeviceIDPath As String = ConfigurationManager.AppSettings("apiGetDeviceTypeByDeviceId")
    Dim apiGetDeviceEventsByDeviceIdPath As String = ConfigurationManager.AppSettings("apiGetDeviceEventsByDeviceId")


    Private AccessToken As String
    Private schedule As System.Threading.Timer
    Private apitoken As String = ""

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        'hdnConnectionString.Value = Session("ConnectionStringNew")

        If Not IsPostBack Then
           

            Dim sLocationID As String
            sLocationID = Request.QueryString("LocationID").ToString()
            'Session("LocationID") = sLocationID
            'idCustomerName.InnerText = Request.QueryString("CustomerName").ToString()

            Dim sCustomerDetails As StringBuilder = New StringBuilder

            Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            Using con As New MySqlConnection(constr)
                Using cmd As New MySqlCommand()
                    cmd.CommandType = CommandType.Text

                    Dim insQuery1 As String = "SELECT LocationID,AccountID,ServiceName,Address1,AddCountry from tblCompanylocation where LocationID ='" & sLocationID & "'"
                    cmd.CommandText = insQuery1
                    cmd.Connection = con
                    con.Open()
                    Dim reader As MySqlDataReader = cmd.ExecuteReader()

                    Do While reader.Read()
                        sCustomerDetails.Append(reader("LocationID").ToString() + "<br/>" + reader("ServiceName").ToString() + "<br/>" + reader("AccountID").ToString() + "<br/>" + reader("Address1").ToString() + "<br/>" + reader("AddCountry").ToString())
                        idCustomerName.InnerText = reader("ServiceName").ToString()
                    Loop
                    con.Close()
                End Using
            End Using
            lblCustomerName.Text = sCustomerDetails.ToString()
            BindFloorPlanDropdown(sLocationID)

            HiddenUserID.Value = Session("UserID")

            Dim floorplanID As Integer
            If ddlfloorPlanList.SelectedValue <> "" Then
                floorplanID = ddlfloorPlanList.SelectedValue
            End If
            ''DeviceDislay(floorplanID)
            ''Getapi 
            'Try

            '    Dim result1 = GetToken()
            '    If AccessToken = "" Then
            '        lblProcess.Text = "Error Occured..."
            '        Return
            '    End If
            '    Dim result As String
            '    result = CallApi(sLocationID)
            '    result = "Data from API Successfully Saved..."
            'Catch ex As Exception
            '    'lblProcess.Text = "Error " + ex.Message
            'End Try



            HiddenDatetimeNow.Value = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)

            'Bind ddlDevicePlacedTimeFrom drop down

            ddlDevicePlacedTimeFrom.Items.Insert(0, New ListItem("--SELECT--", ""))
            ddlDevicePlacedTimeFrom.Items.Insert(1, New ListItem("12:00 AM", "12:00 AM"))
            ddlDevicePlacedTimeFrom.Items.Insert(2, New ListItem("01:00 AM", "01:00 AM"))
            ddlDevicePlacedTimeFrom.Items.Insert(3, New ListItem("02:00 AM", "02:00 AM"))
            ddlDevicePlacedTimeFrom.Items.Insert(4, New ListItem("03:00 AM", "03:00 AM"))
            ddlDevicePlacedTimeFrom.Items.Insert(5, New ListItem("04:00 AM", "04:00 AM"))
            ddlDevicePlacedTimeFrom.Items.Insert(6, New ListItem("05:00 AM", "05:00 AM"))
            ddlDevicePlacedTimeFrom.Items.Insert(7, New ListItem("06:00 AM", "06:00 AM"))
            ddlDevicePlacedTimeFrom.Items.Insert(8, New ListItem("07:00 AM", "07:00 AM"))
            ddlDevicePlacedTimeFrom.Items.Insert(9, New ListItem("08:00 AM", "08:00 AM"))
            ddlDevicePlacedTimeFrom.Items.Insert(10, New ListItem("09:00 AM", "09:00 AM"))
            ddlDevicePlacedTimeFrom.Items.Insert(11, New ListItem("10:00 AM", "10:00 AM"))
            ddlDevicePlacedTimeFrom.Items.Insert(12, New ListItem("11:00 AM", "11:00 AM"))
            ddlDevicePlacedTimeFrom.Items.Insert(13, New ListItem("12:00 PM", "12:00 PM"))
            ddlDevicePlacedTimeFrom.Items.Insert(14, New ListItem("01:00 PM", "01:00 PM"))
            ddlDevicePlacedTimeFrom.Items.Insert(15, New ListItem("02:00 PM", "02:00 PM"))
            ddlDevicePlacedTimeFrom.Items.Insert(16, New ListItem("03:00 PM", "03:00 PM"))
            ddlDevicePlacedTimeFrom.Items.Insert(17, New ListItem("04:00 PM", "04:00 PM"))
            ddlDevicePlacedTimeFrom.Items.Insert(18, New ListItem("05:00 PM", "05:00 PM"))
            ddlDevicePlacedTimeFrom.Items.Insert(19, New ListItem("06:00 PM", "06:00 PM"))
            ddlDevicePlacedTimeFrom.Items.Insert(20, New ListItem("07:00 PM", "07:00 PM"))
            ddlDevicePlacedTimeFrom.Items.Insert(21, New ListItem("08:00 PM", "08:00 PM"))
            ddlDevicePlacedTimeFrom.Items.Insert(22, New ListItem("09:00 PM", "09:00 PM"))
            ddlDevicePlacedTimeFrom.Items.Insert(23, New ListItem("10:00 PM", "10:00 PM"))
            ddlDevicePlacedTimeFrom.Items.Insert(24, New ListItem("11:00 PM", "11:00 PM"))

            Dim resultToken = GetToken()
            Dim tokenno As String = ""
            tokenno = AccessToken
            HiddenToken.Value = tokenno

        End If
    End Sub

    Private Function Authorize() As Task
        Dim authority As String = ConfigurationManager.AppSettings.[Get]("authority")
        Dim clientId As String = ConfigurationManager.AppSettings.[Get]("clientId")
        Dim redirecturl As String = ConfigurationManager.AppSettings.[Get]("redirectUri")
        Dim resource As String = ConfigurationManager.AppSettings.[Get]("resource")
        Dim clientSecret As String = ConfigurationManager.AppSettings.[Get]("clientSecret")
        Dim grant_type As String = ConfigurationManager.AppSettings.[Get]("grant_type")

        Return Task.Run(Async Function()

                            Dim token As String = ""
                            Using client = New HttpClient()
                                Try

                                    Dim data = {
                                        New KeyValuePair(Of String, String)("grant_type", grant_type),
                                        New KeyValuePair(Of String, String)("client_id", clientId),
                                        New KeyValuePair(Of String, String)("client_secret", clientSecret),
                                        New KeyValuePair(Of String, String)("resource", resource)
                                    }

                                    Dim apiGetDevicesByDeviceId1 As String = "https://login.microsoftonline.com/70e08322-214a-4b74-ae88-494e6a91bee1/oauth2/token"
                                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
                                    Dim response As HttpResponseMessage = client.PostAsync(apiGetDevicesByDeviceId1, New FormUrlEncodedContent(data)).Result
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


        'Return Task.Run(Async Function()
        '                    Dim clientCredential = New ClientCredential(clientId, clientSecret)
        '                    Dim context As AuthenticationContext = New AuthenticationContext(authority, False)
        '                    token = Await context.AcquireTokenAsync(resource, clientCredential)
        '                End Function)

    End Function

    Private Async Function GetToken() As Task
        Authorize().Wait()
    End Function

    Public Sub BindFloorPlanDropdown(ByVal sLocationID As String)

        Dim FloorPlanlist As New List(Of FloorPlan)
        Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        Using con As New MySqlConnection(constr)
            Using cmd As New MySqlCommand()
                cmd.CommandType = CommandType.Text

                Dim insQuery2 As String = "select FloorPlanID, FloorPlanName from tblcustomerlocationfloorplan where LocationID='" & sLocationID & "'"
                cmd.CommandText = insQuery2
                cmd.Connection = con
                con.Open()

                Dim reader2 As MySqlDataReader = cmd.ExecuteReader()
                Do While reader2.Read()
                    Dim item As New FloorPlan()
                    If Not IsDBNull(reader2("FloorPlanID")) Then
                        item.floorID = CType(reader2("FloorPlanID"), Integer)
                    Else
                        item.floorID = 0
                    End If
                    ' item.floorID = reader2("FloorPlanID")
                    item.floorName = reader2("FloorPlanName").ToString()
                    FloorPlanlist.Add(item)
                Loop
                con.Close()

            End Using
        End Using

        'Binding ddlfloorPlanList0
        ddlfloorPlanList.DataSource = FloorPlanlist
        ddlfloorPlanList.DataTextField = "floorName"
        ddlfloorPlanList.DataValueField = "floorID"
        ddlfloorPlanList.DataBind()

    End Sub
    'Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
    Protected Sub btnUpload_Click(sender As Object, e As EventArgs)

        Dim sLocationID As String = ""
        sLocationID = Request.QueryString("LocationID").ToString()
        'If Not (Session("LocationID") Is Nothing) Then
        '    sLocationID = Session("LocationID")

        'End If


        Dim filename As String = Path.GetFileName(avatarUpload.PostedFile.FileName)
        If filename = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "ShowStatus", "alertpopup();", True)
            Return
        End If

        Dim FloorPlanID As Int32 = 0


        Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        ' Get maximum FloorPlanID
        Using con As New MySqlConnection(constr)
            Dim query As String = "SELECT MAX(FloorPlanID) as  max_FloorPlanID FROM tblcustomerlocationfloorplan"
            Using cmd As New MySqlCommand(query)
                cmd.Connection = con
                con.Open()
                Dim sumOfPrice As Integer = 0
                Dim obj As Object = cmd.ExecuteScalar()
                If Not obj Is Nothing Then
                    If Not IsDBNull(obj) Then
                        FloorPlanID = Convert.ToInt32(obj)
                    End If

                End If
                con.Close()
            End Using
        End Using
        If Not IsNothing(FloorPlanID) Then
            FloorPlanID = FloorPlanID + 1
        Else
            FloorPlanID = 0

        End If


        ' Store tblcustomerlocationfloorplan data into db
        Dim contentType As String = avatarUpload.PostedFile.ContentType
        Using fs As Stream = avatarUpload.PostedFile.InputStream
            Using br As New BinaryReader(fs)
                Dim bytes As Byte() = br.ReadBytes(DirectCast(fs.Length, Long))
                Using con As New MySqlConnection(constr)
                    Dim query As String = "INSERT INTO tblcustomerlocationfloorplan(LocationID, FloorPlanID, FloorPlanName, FloorPlanImage, FileName, ContentType) VALUES (@LocationID, @FloorPlanID, @FloorPlanName, @FloorPlanImage, @FileName, @ContentType)"
                    Using cmd As New MySqlCommand(query)
                        cmd.Connection = con
                        cmd.Parameters.AddWithValue("@LocationID", Request.QueryString("LocationID").ToString())
                        cmd.Parameters.AddWithValue("@FloorPlanID", FloorPlanID)
                        cmd.Parameters.AddWithValue("@FloorPlanName", txtFloorPlanName.Text)

                        cmd.Parameters.AddWithValue("@FileName", filename)
                        cmd.Parameters.AddWithValue("@ContentType", contentType)
                        cmd.Parameters.AddWithValue("@FloorPlanImage", bytes)
                        con.Open()
                        cmd.ExecuteNonQuery()
                        con.Close()
                    End Using
                End Using
            End Using
        End Using
        BindFloorPlanDropdown(sLocationID)
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "ShowStatus", "hidepopup();", True)
    End Sub

    'Protected Sub closeFileDilog(sender As Object, e As EventArgs) Handles FileUpload1.Disposed



    'End Sub

    Protected Sub btnAddFloorPlan_Click(sender As Object, e As EventArgs)
        txtFloorPlanName.Text = ""
        Image1.ImageUrl = Nothing
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "GetDeviceDislayList", "GetDeviceDislayList();", True)
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "ShowStatus", "showpopup();", True)
    End Sub
    'Public Sub DeviceDislay(ByVal floorplanID As Integer)
    '    Dim sLocationID As String
    '    sLocationID = Request.QueryString("LocationID").ToString()


    '    Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    '    'Get Floor plan Drop Down data
    '    Dim Devicelist As New List(Of DeviceEvents1)
    '    'If Not Me.IsPostBack Then
    '    Using con As New MySqlConnection(constr)
    '        Using cmd As New MySqlCommand()
    '            cmd.CommandType = CommandType.Text
    '            Dim insQuery As String = "SELECT * FROM tblcustomerlocationdevices WHERE locationID='" & sLocationID & "' AND (deviceid NOT IN (SELECT deviceid FROM tblfloorplanitems WHERE floorPlanID =  " & floorplanID & "))"
    '            cmd.CommandText = insQuery
    '            cmd.Connection = con
    '            con.Open()
    '            Dim reader As MySqlDataReader = cmd.ExecuteReader()
    '            Do While reader.Read()
    '                Dim item As New DeviceEvents1()
    '                item.DeviceName = reader("DeviceName").ToString()
    '                item.DeviceID = reader("DeviceID").ToString()
    '                Devicelist.Add(item)
    '            Loop
    '            con.Close()
    '        End Using
    '    End Using

    '    Dim Devicedisplay As String = ""

    '    Devicedisplay = "<ul style='color: blue!important;height: 500px;overflow: auto;list-style-type: none;text-align: left !important;'>"

    '    If Devicelist.Count > 0 Then

    '        For Each item As DeviceEvents1 In Devicelist
    '            Devicedisplay += " <li style='height: 21px;'><span class='drag' >" + item.DeviceID + "</span><hr></li>"
    '        Next item
    '    End If

    '    Devicedisplay += "</ul>"

    '    idDevicedisplay.InnerHtml = Devicedisplay
    'End Sub
    <System.Web.Services.WebMethod()>
    Public Shared Function GetFloorPlanImage(ByVal floorplanID As Integer) As String
        Dim sdata As String = ""

        Dim bytes As Byte()
        Dim base64String As String = ""

        Dim isRecordExist As Boolean = False

        Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        Using con As New MySqlConnection(constr)
            Using cmd As New MySqlCommand()
                cmd.CommandType = CommandType.Text
                Dim insQuery As String = "SELECT * FROM tblcustomerlocationfloorplan WHERE floorPlanID =  " & floorplanID & ""

                cmd.CommandText = insQuery
                cmd.Connection = con
                con.Open()
                Dim reader As MySqlDataReader = cmd.ExecuteReader()
                '{"name":"Item 1","XCoordinate":"blue","YCoordinate":"blue","CountValue":"blue"}
                Do While reader.Read()
                    isRecordExist = True

                    If Not IsDBNull(TryCast(reader("FloorPlanImage"), Byte())) Then
                        bytes = TryCast(reader("FloorPlanImage"), Byte())

                        Try
                            base64String = Convert.ToBase64String(bytes, 0, bytes.Length)
                        Catch ex As Exception
                            base64String = ""
                        End Try

                    End If

                Loop
                con.Close()
            End Using
        End Using

        sdata = "["

        sdata += "{""name"":"""
        sdata += base64String
        sdata += """},"
        sdata = sdata.Substring(0, (sdata.Length - 1))
        sdata = sdata + "]"
        Return sdata
    End Function
    <System.Web.Services.WebMethod()>
    Public Shared Function UploadFloorPlanDevice(ByVal floorplanID As Integer, ByVal JsonFloorPlanDevices As String, ByVal Deletedevices As String) As String
        Dim sdata As String = ""
        Dim DeviceID As String
        Dim isdeleted As Integer
        Dim isinserted As Integer
        Dim FloorPlanItemslist As List(Of FloorPlanDevices) = New List(Of FloorPlanDevices)
        Dim serializer As JavaScriptSerializer = New JavaScriptSerializer()
        FloorPlanItemslist = serializer.Deserialize(Of List(Of FloorPlanDevices))(JsonFloorPlanDevices)

        For Each item As FloorPlanDevices In FloorPlanItemslist
            item.DevicePlacedDate = DateTime.ParseExact(item.DevicePlacedDateText, "dd/MM/yyyy hh:mm:ss tt", System.Globalization.DateTimeFormatInfo.InvariantInfo)
        Next

        Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        'Using con As New MySqlConnection(constr)
        '    Dim query As String = "Delete  FROM tblfloorplanitems where FloorPlanID=" & floorplanID & ""
        '    Using cmd As New MySqlCommand(query)
        '        cmd.Connection = con
        '        con.Open()
        '        isdeleted = cmd.ExecuteNonQuery()
        '        con.Close()
        '    End Using
        'End Using


        Dim allDevicesInDB As New List(Of FloorPlanDevices)

        Using con As New MySqlConnection(constr)

            Dim insQuery As String = "SELECT * FROM tblfloorplanitems where FloorPlanID=" & floorplanID & ""

            Using cmd As New MySqlCommand(insQuery)
                cmd.CommandText = insQuery
                cmd.Connection = con
                con.Open()
                Dim reader As MySqlDataReader = cmd.ExecuteReader()
                Do While reader.Read()
                    Dim item As New FloorPlanDevices()
                    If Not IsDBNull(reader("DeviceID")) Then
                        item.deviceid = CType(reader("DeviceID"), String)
                    Else
                        item.deviceid = ""
                    End If
                    If Not IsDBNull(reader("RcNo")) Then
                        item.RcNo = CType(reader("RcNo"), Integer)
                    Else
                        item.RcNo = 0
                    End If
                    'item.deviceid = reader("DeviceID").ToString()
                    'item.RcNo = reader("RcNo")
                    allDevicesInDB.Add(item)
                Loop
                con.Close()
            End Using
        End Using


        If (FloorPlanItemslist.Count > 0) And (allDevicesInDB.Count > 0) Then


            Dim List2 = allDevicesInDB.Where(Function(item) Not FloorPlanItemslist.Any(Function(item2) item2.deviceid = item.deviceid)).ToList()

            For Each item As FloorPlanDevices In List2

                Using con As New MySqlConnection(constr)
                    Dim query As String = "Delete  FROM tblfloorplanitems where DeviceId='" & item.deviceid & "'"
                    Using cmd As New MySqlCommand(query)
                        cmd.Connection = con
                        con.Open()
                        isdeleted = cmd.ExecuteNonQuery()
                        con.Close()
                    End Using

                End Using

            Next

        ElseIf (FloorPlanItemslist.Count = 0) And (allDevicesInDB.Count > 0) Then

            For Each item As FloorPlanDevices In allDevicesInDB

                Using con As New MySqlConnection(constr)
                    Dim query As String = "Delete  FROM tblfloorplanitems where DeviceId='" & item.deviceid & "'"
                    Using cmd As New MySqlCommand(query)
                        cmd.Connection = con
                        con.Open()
                        isdeleted = cmd.ExecuteNonQuery()
                        con.Close()
                    End Using

                End Using

            Next

        End If

        Dim deletedeviceslist() As String
        If Deletedevices <> "" Then
            deletedeviceslist = Deletedevices.Split(",")
            For i = 0 To deletedeviceslist.Count - 1
                Using con As New MySqlConnection(constr)
                    Dim query As String = "Delete  FROM tblfloorplanitems where RcNo='" & deletedeviceslist(i) & "'"
                    Using cmd As New MySqlCommand(query)
                        cmd.Connection = con
                        con.Open()
                        isdeleted = cmd.ExecuteNonQuery()
                        con.Close()
                    End Using
                End Using
            Next
        End If


        If (FloorPlanItemslist.Count > 0) Then

            For Each item As FloorPlanDevices In FloorPlanItemslist

                If (item.deviceid <> "" And item.deviceid <> Nothing) Then 'To insert new device
                    If item.RcNo = 0 Then
                        Using con As New MySqlConnection(constr)
                            Dim query As String = "INSERT INTO tblfloorplanitems(FloorPlanID, DeviceID, XCoordinate, YCoordinate, Description, DevicePlacedDate,AliasName,CreatedOn, CreatedBy) VALUES (@FloorPlanID, @DeviceID, @XCoordinate, @YCoordinate, @Description, @DevicePlacedDate,@AliasName,@CreatedOn,@CreatedBy)"
                            Using cmd As New MySqlCommand(query)
                                cmd.Connection = con
                                cmd.Parameters.AddWithValue("@FloorPlanID", floorplanID)
                                cmd.Parameters.AddWithValue("@DeviceID", item.deviceid)
                                cmd.Parameters.AddWithValue("@XCoordinate", item.lat)
                                cmd.Parameters.AddWithValue("@YCoordinate", item.lng)
                                cmd.Parameters.AddWithValue("@Description", item.Description)
                                cmd.Parameters.AddWithValue("@DevicePlacedDate", item.DevicePlacedDate)
                                cmd.Parameters.AddWithValue("@AliasName", item.AliasName)
                                cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now())
                                cmd.Parameters.AddWithValue("@CreatedBy", item.CreatedBy)
                                con.Open()
                                isinserted = cmd.ExecuteNonQuery()
                                con.Close()
                            End Using
                        End Using
                    Else

                        Dim devicelist As New List(Of FloorPlanItems)
                        Dim deviceobj As New FloorPlanItems

                        Using con As New MySqlConnection(constr) 'To get the exsisting device coordinates
                            Using cmd As New MySqlCommand()
                                cmd.CommandType = CommandType.Text

                                'Dim insQuery As String = "SELECT DeviceID,XCoordinate,YCoordinate,CreatedOn,CreatedBy,DevicePlacedDate from tblfloorplanitems where RcNo = " & item.RcNo & " "
                                Dim insQuery As String = "SELECT RcNo ,DeviceID, XCoordinate,YCoordinate,Description,CreatedOn,CreatedBy,DevicePlacedDate from tblfloorplanitems where DeviceID = " & item.deviceid & " and floorplanID =  " & floorplanID & "  ORDER BY Rcno DESC LIMIT 1"

                                cmd.CommandText = insQuery
                                cmd.Connection = con
                                con.Open()
                                Dim reader As MySqlDataReader = cmd.ExecuteReader()
                                Do While reader.Read()
                                    If Not IsDBNull(reader("DeviceID")) Then
                                        deviceobj.DeviceID = CType(reader("DeviceID"), String)
                                    Else
                                        deviceobj.DeviceID = ""
                                    End If
                                    If Not IsDBNull(reader("RcNo")) Then
                                        deviceobj.RcNo = CType(reader("RcNo"), Integer)
                                    Else
                                        deviceobj.RcNo = 0
                                    End If

                                    If Not IsDBNull(reader("DevicePlacedDate")) Then
                                        deviceobj.DevicePlacedDate = reader("DevicePlacedDate")
                                        deviceobj.DevicePlacedDateText = deviceobj.DevicePlacedDate.ToString("dd/MM/yyyy hh:mm:ss tt")
                                    Else
                                        deviceobj.DevicePlacedDate = DateTime.Now
                                        deviceobj.DevicePlacedDateText = deviceobj.DevicePlacedDate.ToString("dd/MM/yyyy hh:mm:ss tt")
                                    End If

                                    If Not IsDBNull(reader("XCoordinate")) Then
                                        deviceobj.XCoordinate = CType(reader("XCoordinate"), String)
                                    Else
                                        deviceobj.XCoordinate = ""
                                    End If

                                    If Not IsDBNull(reader("YCoordinate")) Then
                                        deviceobj.YCoordinate = CType(reader("YCoordinate"), String)
                                    Else
                                        deviceobj.YCoordinate = ""
                                    End If

                                    If Not IsDBNull(reader("Description")) Then
                                        deviceobj.Description = CType(reader("Description"), String)
                                    Else
                                        deviceobj.Description = ""
                                    End If

                                    If Not IsDBNull(reader("CreatedOn")) Then
                                        deviceobj.CreatedOn = CType(reader("CreatedOn"), String)
                                    Else
                                        deviceobj.CreatedOn = ""
                                    End If
                                    If Not IsDBNull(reader("CreatedBy")) Then
                                        deviceobj.CreatedBy = CType(reader("CreatedBy"), String)
                                    Else
                                        deviceobj.CreatedBy = ""
                                    End If
                                    ' deviceobj.DeviceID = reader("DeviceID").ToString()
                                    'deviceobj.XCoordinate = reader("XCoordinate").ToString()
                                    'deviceobj.YCoordinate = reader("YCoordinate").ToString()
                                Loop
                                con.Close()
                            End Using
                        End Using
                        Dim xcordinateValue As String = ""
                        Dim ycordinateValue As String = ""
                        If deviceobj.XCoordinate IsNot Nothing Then
                            xcordinateValue = deviceobj.XCoordinate.ToString() ' To remove zeros at the end of x coordinate from db
                            xcordinateValue = xcordinateValue.TrimEnd(New String({"0", "."}))
                        End If

                        If deviceobj.YCoordinate IsNot Nothing Then
                            ycordinateValue = deviceobj.YCoordinate.ToString() ' To remove zeros at the end of y coordinate from db
                            ycordinateValue = ycordinateValue.TrimEnd(New String({"0", "."}))
                        End If

                        If (xcordinateValue <> item.lat) Or (ycordinateValue <> item.lng) Then 'To check whether the position is changed and then insert if the coordinates not match with existing coordinates
                            Using con As New MySqlConnection(constr)
                                Dim query As String = ""
                                If item.DevicePlacedDateText = deviceobj.DevicePlacedDateText Then
                                    query = "UPDATE tblfloorplanitems SET FloorPlanID=@FloorPlanID,DeviceID=@DeviceID,XCoordinate=@XCoordinate,YCoordinate=@YCoordinate,Description=@Description,DevicePlacedDate=@DevicePlacedDate,AliasName=@AliasName,UpdatedOn=@UpdatedOn,UpdatedBy=@UpdatedBy WHERE" & _
                                            " RcNo =@RcNo"
                                Else
                                    query = "INSERT INTO tblfloorplanitems(FloorPlanID, DeviceID, XCoordinate, YCoordinate, Description, DevicePlacedDate,AliasName,CreatedOn,CreatedBy) VALUES (@FloorPlanID, @DeviceID, @XCoordinate, @YCoordinate,@Description,@DevicePlacedDate,@AliasName,@UpdatedOn,@UpdatedBy)"
                                End If

                                Using cmd As New MySqlCommand(query)
                                    cmd.Connection = con
                                    cmd.Parameters.AddWithValue("@FloorPlanID", floorplanID)
                                    cmd.Parameters.AddWithValue("@DeviceID", item.deviceid)
                                    cmd.Parameters.AddWithValue("@XCoordinate", item.lat)
                                    cmd.Parameters.AddWithValue("@YCoordinate", item.lng)
                                    cmd.Parameters.AddWithValue("@Description", item.Description)
                                    cmd.Parameters.AddWithValue("@DevicePlacedDate", item.DevicePlacedDate)
                                    cmd.Parameters.AddWithValue("@AliasName", item.AliasName)
                                    cmd.Parameters.AddWithValue("@UpdatedOn", DateTime.Now())
                                    cmd.Parameters.AddWithValue("@UpdatedBy", item.UpdatedBy)
                                    cmd.Parameters.AddWithValue("@RcNo", item.RcNo)
                                    con.Open()
                                    isinserted = cmd.ExecuteNonQuery()
                                    con.Close()
                                End Using
                            End Using
                        ElseIf (deviceobj.Description <> item.Description) Then
                            'To update Description of the device when changed
                            Using con As New MySqlConnection(constr)
                                Dim query As String = ""
                                query = "UPDATE tblfloorplanitems SET Description=@Description,UpdatedOn=@UpdatedOn,UpdatedBy=@UpdatedBy WHERE RcNo =@RcNo "
                                Using cmd As New MySqlCommand(query)
                                    cmd.Connection = con
                                    cmd.Parameters.AddWithValue("@Description", item.Description)
                                    cmd.Parameters.AddWithValue("@UpdatedOn", DateTime.Now())
                                    cmd.Parameters.AddWithValue("@UpdatedBy", item.UpdatedBy)
                                    cmd.Parameters.AddWithValue("@RcNo", item.RcNo)
                                    con.Open()
                                    isinserted = cmd.ExecuteNonQuery()
                                    con.Close()
                                End Using
                            End Using
                        End If

                    End If

                End If
            Next



        End If
        If isinserted > 0 Or isdeleted > 0 Then
            sdata = "Floor Plan Saved Successfully"
        End If
        Return sdata

    End Function

    <System.Web.Services.WebMethod()>
    Public Shared Function getDevicePlacedDate(ByVal DevicePlacedDateyear As String, ByVal DevicePlacedDatemonth As String, ByVal DevicePlacedDateday As String, ByVal DevicePlacedDateHour As String, ByVal DevicePlacedDateMin As String, ByVal DevicePlacedDateSec As String, ByVal deviceID As String, ByVal floorplanID As Integer) As String

        Dim selectedDevicePlacedDate As New DateTime?

        If DevicePlacedDateyear <> "" Then
            selectedDevicePlacedDate = New DateTime(CType(DevicePlacedDateyear, Integer), CType(DevicePlacedDatemonth, Integer), CType(DevicePlacedDateday, Integer),
                                                    CType(DevicePlacedDateHour, Integer), CType(DevicePlacedDateMin, Integer), CType(DevicePlacedDateSec, Integer))

        End If

        Dim datetimeNowValue As String

        If Not selectedDevicePlacedDate Is Nothing Then
            datetimeNowValue = selectedDevicePlacedDate.Value.ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)
        Else
            datetimeNowValue = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)
        End If

        Dim noOfDuplicates As String = "0"

        Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        Using con As New MySqlConnection(constr)
            Using cmd As New MySqlCommand()
                cmd.CommandType = CommandType.Text
                Dim insQuery As String = "select count(*) as NoOfDuplicates from tblfloorplanitems where DeviceID ='" & deviceID & "' AND deviceplaceddate ='" & (selectedDevicePlacedDate.Value.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)) & "' and floorPlanID =" & floorplanID & " ;"
                cmd.CommandText = insQuery
                cmd.Connection = con
                con.Open()
                Dim reader As MySqlDataReader = cmd.ExecuteReader()
                Do While reader.Read()
                    If Not IsDBNull(reader("NoOfDuplicates")) Then
                        noOfDuplicates = CType(reader("NoOfDuplicates"), String)
                    End If
                Loop

                con.Close()
            End Using
        End Using



        Dim sdata As String = ""

        sdata = "["

        sdata += "{""datetimeNowValue"":"""
        sdata += datetimeNowValue
        sdata += """,""noOfDuplicates"":"""
        sdata += noOfDuplicates
        sdata += """}"

        sdata = sdata + "]"

        Return sdata
    End Function

    <System.Web.Services.WebMethod()>
    Public Shared Function Checkaliasduplicate(ByVal locationID As String, ByVal aliasName As String) As String

        Dim isAliasExist As Boolean = False

        Dim list As New List(Of AliasNameExists)
        Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        Using con As New MySqlConnection(constr)
            Using cmd As New MySqlCommand()
                cmd.CommandType = CommandType.Text
                Dim insQuery As String = "select A.LocationID,B.AliasName from tblCustomerLocationFloorPlan A inner join tblFloorPlanItems B on A.FloorPlanID=B.FloorPlanID Where A.LocationID = '" & locationID & "' AND B.aliasname = '" & aliasName & "';"
                cmd.CommandText = insQuery
                cmd.Connection = con
                con.Open()
                Dim reader As MySqlDataReader = cmd.ExecuteReader()
                Do While reader.Read()
                    isAliasExist = True
                    Exit Do
                Loop

                con.Close()
            End Using
        End Using



        Dim sdata As String = ""

        sdata = "["
        sdata += "{""duplicate"":"""
        sdata += isAliasExist.ToString()
        sdata += """}"
        sdata = sdata + "]"
        Return sdata

    End Function


    Protected Sub hdnFloorPlanchange_ValueChanged(sender As Object, e As EventArgs)

        Dim locationID = HiddenLocationID.Value
        Dim sdata As String = ""
        Dim bytes As Byte()
        Dim base64String As String = ""
        Dim list As New List(Of DeviceEvents1)
        Dim isRecordExist As Boolean = False
        Dim result As String = ""

        'Dim resultToken = GetToken()
        Dim tokenno As String = ""
        'tokenno = AccessToken

        ''Generate token start
        'Dim authority As String = ConfigurationManager.AppSettings.[Get]("authority")
        'Dim clientId As String = ConfigurationManager.AppSettings.[Get]("clientId")
        'Dim redirecturl As String = ConfigurationManager.AppSettings.[Get]("redirectUri")
        'Dim resource As String = ConfigurationManager.AppSettings.[Get]("resource")
        'Dim clientSecret As String = ConfigurationManager.AppSettings.[Get]("clientSecret")
        'Dim token As AuthenticationResult
        'Dim AccessToken As String

        'Dim resultToken = Task.Run(Async Function()
        '                               Dim clientCredential = New ClientCredential(clientId, clientSecret)
        '                               Dim context As AuthenticationContext = New AuthenticationContext(authority, False)
        '                               token = Await context.AcquireTokenAsync(resource, clientCredential)
        '                           End Function)

        'Dim tokenno As String
        'tokenno = AccessToken
        ''Generate token end


        Dim apiGetSitesList As List(Of apiGetSiteItem) = New List(Of apiGetSiteItem)()
        Dim serializer As JavaScriptSerializer = New JavaScriptSerializer()
        Dim apiGetSitesPath As String = ConfigurationManager.AppSettings("apiGetSites")

        Dim filter As String = "Filter=externalSiteId eq '" & locationID & "'"

        Using client = New HttpClient()

            If Not String.IsNullOrWhiteSpace(tokenno) Then
                client.DefaultRequestHeaders.Clear()
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings.[Get]("subscriptionId"))
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " & tokenno)
                client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
            End If

            Try
                Dim apiGetSitesPath1 As String = apiGetSitesPath & "?" & filter
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
                Dim response As HttpResponseMessage = client.GetAsync(apiGetSitesPath1).Result
                Dim responseString = response.Content.ReadAsStringAsync().Result

                If response.IsSuccessStatusCode Then
                    serializer = New JavaScriptSerializer()
                    Dim jObject = Newtonsoft.Json.Linq.JObject.Parse(responseString)
                    Dim result1 As String
                    result1 = jObject("items").ToString()
                    apiGetSitesList = serializer.Deserialize(Of List(Of apiGetSiteItem))(result1)

                End If

            Catch ex As Exception
                result = "Error in apiGetSitesPath - " & ex.Message
            End Try
        End Using

        Dim apiGetSiteItem As apiGetSiteItem = apiGetSitesList.Where(Function(x) x.externalSiteId = locationID).FirstOrDefault()

        Dim apiGetDevicebySiteIdlist As List(Of apiGetDeviceBySiteIdItem) = New List(Of apiGetDeviceBySiteIdItem)()

        Dim apiGetDevicebySiteIdlist1 As List(Of apiGetDeviceBySiteIdItem) = New List(Of apiGetDeviceBySiteIdItem)()


        Dim apiGetDevicesBySiteIDPath As String = ConfigurationManager.AppSettings("apiGetDevicesBySiteID")

        Dim pageIndex As Integer
        Dim totalPages As Integer
        Dim paging As Integer
        Dim PAGE_SIZE As Double
        pageIndex = 0
        totalPages = 0
        PAGE_SIZE = 1000


        If Not apiGetSiteItem Is Nothing Then
            Try
                Using client = New HttpClient()
                    If Not String.IsNullOrWhiteSpace(tokenno) Then
                        client.DefaultRequestHeaders.Clear()
                        client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings.[Get]("subscriptionId"))
                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " & tokenno)
                        client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
                    End If
                    Dim apiGetDevicesBySiteIDPath1 As String = apiGetDevicesBySiteIDPath.Replace("id", apiGetSiteItem.id)
                    apiGetDevicesBySiteIDPath1 = apiGetDevicesBySiteIDPath1 + "?PageSize=" + PAGE_SIZE.ToString()
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
                    Dim response As HttpResponseMessage = client.GetAsync(apiGetDevicesBySiteIDPath1).Result
                    Dim responseString = response.Content.ReadAsStringAsync().Result
                    If response.IsSuccessStatusCode Then
                        Dim jObject = Newtonsoft.Json.Linq.JObject.Parse(responseString)
                        Dim result1 As String
                        result1 = jObject("items").ToString()
                        pageIndex = Convert.ToInt32(jObject("pageIndex").ToString())
                        totalPages = Convert.ToInt32(jObject("totalPages").ToString())

                        apiGetDevicebySiteIdlist1 = serializer.Deserialize(Of List(Of apiGetDeviceBySiteIdItem))(result1)
                        apiGetDevicebySiteIdlist.AddRange(apiGetDevicebySiteIdlist1)

                    End If
                End Using
            Catch ex As Exception
                result = "Error in apiGetDevicesBySiteIDPath - " & ex.Message
            End Try


            ' Paging start
            If pageIndex <> (totalPages - 1) Then
                For paging = 1 To (totalPages - 1)

                    Try
                        Using client = New HttpClient()
                            If Not String.IsNullOrWhiteSpace(tokenno) Then
                                client.DefaultRequestHeaders.Clear()
                                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings.[Get]("subscriptionId"))
                                client.DefaultRequestHeaders.Add("Authorization", "Bearer " & tokenno)
                                client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
                            End If
                            Dim apiGetDevicesBySiteIDPath1 As String = apiGetDevicesBySiteIDPath.Replace("id", apiGetSiteItem.id)
                            'apiGetDevicesBySiteIDPath1 = apiGetDevicesBySiteIDPath1 + "?PageSize=" + PAGE_SIZE.ToString()
                            apiGetDevicesBySiteIDPath1 = apiGetDevicesBySiteIDPath1 + "?PageSize=" + PAGE_SIZE.ToString() + "&pageIndex=" + paging.ToString()
                            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
                            Dim response As HttpResponseMessage = client.GetAsync(apiGetDevicesBySiteIDPath1).Result
                            Dim responseString = response.Content.ReadAsStringAsync().Result
                            If response.IsSuccessStatusCode Then
                                Dim jObject = Newtonsoft.Json.Linq.JObject.Parse(responseString)
                                Dim result1 As String
                                result1 = jObject("items").ToString()

                                apiGetDevicebySiteIdlist1 = serializer.Deserialize(Of List(Of apiGetDeviceBySiteIdItem))(result1)
                                apiGetDevicebySiteIdlist.AddRange(apiGetDevicebySiteIdlist1)

                            End If
                        End Using
                    Catch ex As Exception
                        result = "Error in apiGetDevicesBySiteIDPath - " & ex.Message
                    End Try

                Next
            End If
            ' Paging end
        End If

        Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        Using con As New MySqlConnection(constr)
            Using cmd As New MySqlCommand()
                cmd.CommandType = CommandType.Text
                'todo
                Dim insQuery As String = "SELECT * FROM tblcustomerlocationdevices WHERE locationID = '" & locationID & "' AND (deviceid not IN (SELECT deviceid FROM tblfloorplanitems where FloorPlanID in (select FloorPlanID from tblCustomerLocationFloorPlan where LocationID = '" & locationID & "')))  order by Description desc"
                cmd.CommandText = insQuery
                cmd.Connection = con
                con.Open()
                Dim reader As MySqlDataReader = cmd.ExecuteReader()
                Do While reader.Read()
                    isRecordExist = True
                    Dim item As New DeviceEvents1()
                    If Not IsDBNull(reader("DeviceType")) Then
                        item.DeviceType = CType(reader("DeviceType"), String)
                    Else
                        item.DeviceType = ""
                    End If

                    If Not IsDBNull(reader("DeviceID")) Then
                        item.DeviceID = CType(reader("DeviceID"), String)
                    Else
                        item.DeviceID = ""
                    End If

                    If Not IsDBNull(reader("DeviceName")) Then
                        item.DeviceName = CType(reader("DeviceName"), String)
                    Else
                        item.DeviceName = ""
                    End If

                    If Not IsDBNull(reader("Description")) Then
                        item.Description = CType(reader("Description"), String)
                    Else
                        item.Description = ""
                    End If

                    'item.DeviceType = reader("DeviceType").ToString()
                    'item.DeviceID = reader("DeviceID").ToString()
                    'item.DeviceName = reader("DeviceName").ToString()

                    list.Add(item)
                Loop
                con.Close()
            End Using
        End Using


        For Each item As DeviceEvents1 In list
            Using con As New MySqlConnection(constr)
                Using cmd As New MySqlCommand()
                    cmd.CommandType = CommandType.Text
                    Dim insQuery As String = "SELECT * FROM tbldevicetype WHERE devicetype = '" & item.DeviceType & "' "
                    cmd.CommandText = insQuery
                    cmd.Connection = con
                    con.Open()
                    Dim reader As MySqlDataReader = cmd.ExecuteReader()
                    Do While reader.Read()
                        If Not IsDBNull(TryCast(reader("Icon"), Byte())) Then
                            bytes = TryCast(reader("Icon"), Byte())
                            Try
                                base64String = Convert.ToBase64String(bytes, 0, bytes.Length)
                                item.DeviceUrl = "data:image/png;base64," & base64String
                            Catch ex As Exception
                                base64String = ""
                            End Try
                        End If
                    Loop
                    If item.DeviceUrl Is Nothing Then
                        item.DeviceUrl = "NoImage"
                    End If
                    con.Close()
                End Using
            End Using


            'Get device ID desc from api returned list start
            If Not apiGetDevicebySiteIdlist Is Nothing And apiGetDevicebySiteIdlist.Count > 0 Then
                item.Description = apiGetDevicebySiteIdlist.OrderByDescending(Function(s) s.comment).Where(Function(x) x.serialNumber = item.DeviceID).Select(Function(x) x.comment).FirstOrDefault()
            Else
                item.Description = ""
            End If
            'Get device ID desc from api returned list end
        Next

        Dim devicedisplaytext As String = ""

        If isRecordExist = True Then
            devicedisplaytext = "<ul  id='uldevicetype' style='color: blue!important;height: 500px;overflow: auto;list-style-type: none;text-align: left !important;margin-left: -40px;'>"
            Dim i As Integer = 0

            For Each item As DeviceEvents1 In list

                devicedisplaytext += " <li id='node" + i.ToString() + "' style='height: 100px;'><span class='drag' itemtype='DEVICE'  RcNo = '0' devicetype='" + item.DeviceType + "' deviceid='" + item.DeviceID + "' devicedescription='" + RemoveSpecialCharacter(item.Description) + "' devicename='" + item.DeviceName + "' deviceurl='" + item.DeviceUrl + "'>" + item.DeviceType + " - " + item.DeviceID + " <br/> " + RemoveSpecialCharacter(item.Description) + "</span><hr></li>"
                i = i + 1
            Next item
            devicedisplaytext += "</ul>"
        End If

        idDevicedisplay.InnerHtml = ""
        idDevicedisplay.InnerHtml = devicedisplaytext

    End Sub



    <System.Web.Services.WebMethod()>
    Public Shared Function GetDeviceDislay(ByVal floorplanID As Integer, ByVal LocationID As String, ByVal Token As String) As String

        Dim sdata As String = ""

        Dim bytes As Byte()
        Dim base64String As String = ""
        Dim list As New List(Of DeviceEvents1)
        Dim isRecordExist As Boolean = False
        'If Not Me.IsPostBack Then

        Dim result As String = ""

        ''Generate token start
        'Dim authority As String = ConfigurationManager.AppSettings.[Get]("authority")
        'Dim clientId As String = ConfigurationManager.AppSettings.[Get]("clientId")
        'Dim redirecturl As String = ConfigurationManager.AppSettings.[Get]("redirectUri")
        'Dim resource As String = ConfigurationManager.AppSettings.[Get]("resource")
        'Dim clientSecret As String = ConfigurationManager.AppSettings.[Get]("clientSecret")
        'Dim AccessToken As String

        'Dim resultToken = Task.Run(Async Function()
        '                               Dim clientCredential = New ClientCredential(clientId, clientSecret)
        '                               Dim context As AuthenticationContext = New AuthenticationContext(authority, False)
        '                               Token = Await context.AcquireTokenAsync(resource, clientCredential)
        '                           End Function)

        'Dim tokenno As String
        'tokenno = AccessToken
        ''Generate token end


        Dim apiGetSitesList As List(Of apiGetSiteItem) = New List(Of apiGetSiteItem)()
        Dim serializer As JavaScriptSerializer = New JavaScriptSerializer()
        Dim apiGetSitesPath As String = ConfigurationManager.AppSettings("apiGetSites")

        Dim filter As String = "Filter=externalSiteId eq '" & LocationID & "'"

        Using client = New HttpClient()

            If Not String.IsNullOrWhiteSpace(Token) Then
                client.DefaultRequestHeaders.Clear()
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings.[Get]("subscriptionId"))
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " & Token)
                client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
            End If

            Try
                Dim apiGetSitesPath1 As String = apiGetSitesPath & "?" & filter
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
                Dim response As HttpResponseMessage = client.GetAsync(apiGetSitesPath1).Result
                Dim responseString = response.Content.ReadAsStringAsync().Result

                If response.IsSuccessStatusCode Then
                    serializer = New JavaScriptSerializer()
                    Dim jObject = Newtonsoft.Json.Linq.JObject.Parse(responseString)
                    Dim result1 As String
                    result1 = jObject("items").ToString()
                    apiGetSitesList = serializer.Deserialize(Of List(Of apiGetSiteItem))(result1)

                End If

            Catch ex As Exception
                result = "Error in apiGetSitesPath - " & ex.Message
                Return result
            End Try
        End Using

        Dim apiGetSiteItem As apiGetSiteItem = apiGetSitesList.Where(Function(x) x.externalSiteId = LocationID).FirstOrDefault()
        Dim apiGetDevicebySiteIdlist As List(Of apiGetDeviceBySiteIdItem) = New List(Of apiGetDeviceBySiteIdItem)()
        Dim apiGetDevicebySiteIdlist1 As List(Of apiGetDeviceBySiteIdItem) = New List(Of apiGetDeviceBySiteIdItem)()
        Dim apiGetDevicesBySiteIDPath As String = ConfigurationManager.AppSettings("apiGetDevicesBySiteID")
        Dim PAGE_SIZE As Double
        Dim pageIndex As Integer
        Dim totalPages As Integer
        Dim paging As Integer
        PAGE_SIZE = 1000

        If Not apiGetSiteItem Is Nothing Then

            Try
                Using client = New HttpClient()
                    If Not String.IsNullOrWhiteSpace(Token) Then
                        client.DefaultRequestHeaders.Clear()
                        client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings.[Get]("subscriptionId"))
                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " & Token)
                        client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
                    End If
                    Dim apiGetDevicesBySiteIDPath1 As String = apiGetDevicesBySiteIDPath.Replace("id", apiGetSiteItem.id)
                    apiGetDevicesBySiteIDPath1 = apiGetDevicesBySiteIDPath1 + "?PageSize=" + PAGE_SIZE.ToString()
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
                    Dim response As HttpResponseMessage = client.GetAsync(apiGetDevicesBySiteIDPath1).Result
                    Dim responseString = response.Content.ReadAsStringAsync().Result
                    If response.IsSuccessStatusCode Then
                        Dim jObject = Newtonsoft.Json.Linq.JObject.Parse(responseString)
                        Dim result1 As String

                        pageIndex = Convert.ToInt32(jObject("pageIndex").ToString())
                        totalPages = Convert.ToInt32(jObject("totalPages").ToString())

                        result1 = jObject("items").ToString()
                        apiGetDevicebySiteIdlist1 = serializer.Deserialize(Of List(Of apiGetDeviceBySiteIdItem))(result1)
                        apiGetDevicebySiteIdlist.AddRange(apiGetDevicebySiteIdlist1)

                    End If
                End Using
            Catch ex As Exception
                result = "Error in apiGetDevicesBySiteIDPath - " & ex.Message
            End Try

            ' Paging start
            If pageIndex <> (totalPages - 1) Then
                For paging = 1 To (totalPages - 1)

                    Try
                        Using client = New HttpClient()
                            If Not String.IsNullOrWhiteSpace(Token) Then
                                client.DefaultRequestHeaders.Clear()
                                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings.[Get]("subscriptionId"))
                                client.DefaultRequestHeaders.Add("Authorization", "Bearer " & Token)
                                client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
                            End If
                            Dim apiGetDevicesBySiteIDPath1 As String = apiGetDevicesBySiteIDPath.Replace("id", apiGetSiteItem.id)
                            apiGetDevicesBySiteIDPath1 = apiGetDevicesBySiteIDPath1 + "?PageSize=" + PAGE_SIZE.ToString() + "&pageIndex=" + paging.ToString()
                            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
                            Dim response As HttpResponseMessage = client.GetAsync(apiGetDevicesBySiteIDPath1).Result
                            Dim responseString = response.Content.ReadAsStringAsync().Result
                            If response.IsSuccessStatusCode Then
                                Dim jObject = Newtonsoft.Json.Linq.JObject.Parse(responseString)
                                Dim result1 As String
                                result1 = jObject("items").ToString()
                                apiGetDevicebySiteIdlist1 = serializer.Deserialize(Of List(Of apiGetDeviceBySiteIdItem))(result1)
                                apiGetDevicebySiteIdlist.AddRange(apiGetDevicebySiteIdlist1)

                            End If
                        End Using
                    Catch ex As Exception
                        result = "Error in apiGetDevicesBySiteIDPath - " & ex.Message
                    End Try
                Next
            End If
            ' Paging end


        End If

        Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

        Using con As New MySqlConnection(constr)
            Using cmd As New MySqlCommand()
                cmd.CommandType = CommandType.Text
                'todo
                Dim insQuery As String = "SELECT * FROM tblcustomerlocationdevices WHERE locationID = '" & LocationID & "'" & _
                                         " AND (deviceid not IN (SELECT deviceid FROM tblfloorplanitems where FloorPlanID in " & _
                                            "(select FloorPlanID from tblCustomerLocationFloorPlan where LocationID = '" & LocationID & "' )))  order by Description desc"
                cmd.CommandText = insQuery
                cmd.Connection = con
                con.Open()
                Dim reader As MySqlDataReader = cmd.ExecuteReader()
                Do While reader.Read()
                    isRecordExist = True
                    Dim item As New DeviceEvents1()
                    If Not IsDBNull(reader("DeviceType")) Then
                        item.DeviceType = CType(reader("DeviceType"), String)
                    Else
                        item.DeviceType = ""
                    End If

                    If Not IsDBNull(reader("DeviceID")) Then
                        item.DeviceID = CType(reader("DeviceID"), String)
                    Else
                        item.DeviceID = ""
                    End If

                    If Not IsDBNull(reader("DeviceName")) Then
                        item.DeviceName = CType(reader("DeviceName"), String)
                    Else
                        item.DeviceName = ""
                    End If

                    'If Not IsDBNull(reader("Description")) Then
                    '    item.Description = CType(reader("Description"), String)
                    'Else
                    '    item.Description = ""
                    'End If

                    'item.DeviceType = reader("DeviceType").ToString()
                    'item.DeviceID = reader("DeviceID").ToString()
                    'item.DeviceName = reader("DeviceName").ToString()

                    list.Add(item)
                Loop
                con.Close()
            End Using
        End Using


        For Each item As DeviceEvents1 In list
            Using con As New MySqlConnection(constr)
                Using cmd As New MySqlCommand()
                    cmd.CommandType = CommandType.Text
                    Dim insQuery As String = "SELECT * FROM tbldevicetype WHERE devicetype = '" & item.DeviceType & "' "
                    cmd.CommandText = insQuery
                    cmd.Connection = con
                    con.Open()
                    Dim reader As MySqlDataReader = cmd.ExecuteReader()
                    Do While reader.Read()
                        If Not IsDBNull(TryCast(reader("Icon"), Byte())) Then
                            bytes = TryCast(reader("Icon"), Byte())
                            Try
                                base64String = Convert.ToBase64String(bytes, 0, bytes.Length)
                                item.DeviceUrl = "data:image/png;base64," & base64String
                            Catch ex As Exception
                                base64String = ""
                            End Try
                        End If
                    Loop
                    If item.DeviceUrl Is Nothing Then
                        item.DeviceUrl = "NoImage"
                    End If
                    con.Close()
                End Using
            End Using

            'Get device ID desc from api returned list start

            If Not apiGetDevicebySiteIdlist Is Nothing And apiGetDevicebySiteIdlist.Count > 0 Then
                item.Description = apiGetDevicebySiteIdlist.OrderByDescending(Function(x) x.comment).Where(Function(x) x.serialNumber = item.DeviceID).Select(Function(x) x.comment).FirstOrDefault()
            Else
                item.Description = ""
            End If
            'Get device ID desc from api returned list end

            If Not item.Description Is Nothing Then
                If item.Description.ToLower().Contains("<comment>") Then
                    item.Description = item.Description.Replace("<comment>", "")
                    item.Description = item.Description.Replace("</comment>", "")
                End If
            End If

        Next


        If isRecordExist = True Then
            sdata = "["
            For Each item As DeviceEvents1 In list

                sdata += "{""deviceid"":"""
                sdata += item.DeviceID

                sdata += """,""devicetype"":"""
                sdata += item.DeviceType
                sdata += """,""itemtype"":"""
                sdata += "DEVICE"

                sdata += """,""devicename"":"""
                sdata += item.DeviceName
                sdata += """,""deviceUrl"":"""
                sdata += item.DeviceUrl

                sdata += """,""devicedescription"":"""
                sdata += RemoveSpecialCharacter(item.Description)

                sdata += """},"
            Next item

            sdata = sdata.Substring(0, (sdata.Length - 1))
            sdata = sdata + "]"
        End If

        Return sdata
    End Function




    <System.Web.Services.WebMethod()>
    Public Shared Function SyncAllDevicesDescription(ByVal FloorplanID As Integer, ByVal LocationID As String, ByVal Token As String) As String

        Dim sdata As String = ""

        Dim list As New List(Of DeviceEvents1)
        Dim result As String = ""

        Dim apiGetSitesList As List(Of apiGetSiteItem) = New List(Of apiGetSiteItem)()
        Dim serializer As JavaScriptSerializer = New JavaScriptSerializer()
        Dim apiGetSitesPath As String = ConfigurationManager.AppSettings("apiGetSites")

        Dim filter As String = "Filter=externalSiteId eq '" & LocationID & "'"

        Using client = New HttpClient()

            If Not String.IsNullOrWhiteSpace(Token) Then
                client.DefaultRequestHeaders.Clear()
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings.[Get]("subscriptionId"))
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " & Token)
                client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
            End If

            Try
                Dim apiGetSitesPath1 As String = apiGetSitesPath & "?" & filter
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
                Dim response As HttpResponseMessage = client.GetAsync(apiGetSitesPath1).Result
                Dim responseString = response.Content.ReadAsStringAsync().Result

                If response.IsSuccessStatusCode Then
                    serializer = New JavaScriptSerializer()
                    Dim jObject = Newtonsoft.Json.Linq.JObject.Parse(responseString)
                    Dim result1 As String
                    result1 = jObject("items").ToString()
                    apiGetSitesList = serializer.Deserialize(Of List(Of apiGetSiteItem))(result1)

                End If

            Catch ex As Exception
                result = "Error in apiGetSitesPath - " & ex.Message
                Return result
            End Try
        End Using

        Dim apiGetSiteItem As apiGetSiteItem = apiGetSitesList.Where(Function(x) x.externalSiteId = LocationID).FirstOrDefault()
        Dim apiGetDevicebySiteIdlist As List(Of apiGetDeviceBySiteIdItem) = New List(Of apiGetDeviceBySiteIdItem)()
        Dim apiGetDevicebySiteIdlist1 As List(Of apiGetDeviceBySiteIdItem) = New List(Of apiGetDeviceBySiteIdItem)()
        Dim apiGetDevicesBySiteIDPath As String = ConfigurationManager.AppSettings("apiGetDevicesBySiteID")

        Dim pageIndex As Integer
        Dim totalPages As Integer
        Dim paging As Integer
        Dim PAGE_SIZE As Double
        pageIndex = 0
        totalPages = 0

        PAGE_SIZE = 1000

        If Not apiGetSiteItem Is Nothing Then

            Try
                Using client = New HttpClient()
                    If Not String.IsNullOrWhiteSpace(Token) Then
                        client.DefaultRequestHeaders.Clear()
                        client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings.[Get]("subscriptionId"))
                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " & Token)
                        client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
                    End If
                    Dim apiGetDevicesBySiteIDPath1 As String = apiGetDevicesBySiteIDPath.Replace("id", apiGetSiteItem.id)
                    apiGetDevicesBySiteIDPath1 = apiGetDevicesBySiteIDPath1 + "?PageSize=" + PAGE_SIZE.ToString()
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
                    Dim response As HttpResponseMessage = client.GetAsync(apiGetDevicesBySiteIDPath1).Result
                    Dim responseString = response.Content.ReadAsStringAsync().Result
                    If response.IsSuccessStatusCode Then
                        Dim jObject = Newtonsoft.Json.Linq.JObject.Parse(responseString)
                        Dim result1 As String

                        pageIndex = Convert.ToInt32(jObject("pageIndex").ToString())
                        totalPages = Convert.ToInt32(jObject("totalPages").ToString())

                        result1 = jObject("items").ToString()
                        apiGetDevicebySiteIdlist1 = serializer.Deserialize(Of List(Of apiGetDeviceBySiteIdItem))(result1)
                        apiGetDevicebySiteIdlist.AddRange(apiGetDevicebySiteIdlist1)

                    End If
                End Using
            Catch ex As Exception
                result = "Error in apiGetDevicesBySiteIDPath - " & ex.Message
            End Try

            ' Paging start
            If pageIndex <> (totalPages - 1) Then
                For paging = 1 To (totalPages - 1)
                    Try

                        Using client = New HttpClient()
                            If Not String.IsNullOrWhiteSpace(Token) Then
                                client.DefaultRequestHeaders.Clear()
                                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings.[Get]("subscriptionId"))
                                client.DefaultRequestHeaders.Add("Authorization", "Bearer " & Token)
                                client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
                            End If
                            Dim apiGetDevicesBySiteIDPath1 As String = apiGetDevicesBySiteIDPath.Replace("id", apiGetSiteItem.id)
                            apiGetDevicesBySiteIDPath1 = apiGetDevicesBySiteIDPath1 + "?PageSize=" + PAGE_SIZE.ToString() + "&pageIndex=" + paging.ToString()
                            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
                            Dim response As HttpResponseMessage = client.GetAsync(apiGetDevicesBySiteIDPath1).Result
                            Dim responseString = response.Content.ReadAsStringAsync().Result
                            If response.IsSuccessStatusCode Then
                                Dim jObject = Newtonsoft.Json.Linq.JObject.Parse(responseString)
                                Dim result1 As String
                                result1 = jObject("items").ToString()
                                apiGetDevicebySiteIdlist1 = serializer.Deserialize(Of List(Of apiGetDeviceBySiteIdItem))(result1)
                                apiGetDevicebySiteIdlist.AddRange(apiGetDevicebySiteIdlist1)

                            End If
                        End Using
                    Catch ex As Exception
                        result = "Error in apiGetDevicesBySiteIDPath - " & ex.Message
                    End Try
                Next
            End If
            ' Paging start
        End If


        If (Not apiGetDevicebySiteIdlist Is Nothing) And (apiGetDevicebySiteIdlist.Count > 0) Then

            For Each item As apiGetDeviceBySiteIdItem In apiGetDevicebySiteIdlist

                Dim description As String = item.comment

                If description.ToLower().Contains("<comment>") Then
                    description = description.Replace("<comment>", "")
                    description = description.Replace("</comment>", "")
                End If

                Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                Using con As New MySqlConnection(constr)
                    Dim query As String = "UPDATE tblfloorplanitems SET Description = @Description WHERE DeviceID =@DeviceID AND  FloorPlanID =@FloorPlanID "
                    Using cmd As New MySqlCommand(query)
                        cmd.Connection = con
                        cmd.Parameters.AddWithValue("@Description", description)
                        cmd.Parameters.AddWithValue("@DeviceID", item.serialNumber)
                        cmd.Parameters.AddWithValue("@FloorPlanID", FloorplanID)
                        con.Open()
                        cmd.ExecuteScalar()
                        con.Close()
                    End Using
                End Using
            Next

        End If



        sdata = "["

        sdata += "{""msg"":"""
        sdata += "Device description of this floorplan Synced successfully"
        sdata += """},"

        sdata = sdata.Substring(0, (sdata.Length - 1))
        sdata = sdata + "]"

        Return sdata
    End Function

    <System.Web.Services.WebMethod()>
    Public Shared Function syncIndividualDeviceDescription(ByVal DeviceID As String, ByVal LocationID As String, ByVal Token As String) As String

        Dim sdata As String = ""

        Dim bytes As Byte()
        Dim base64String As String = ""
        Dim list As New List(Of DeviceEvents1)
        'If Not Me.IsPostBack Then

        Dim result As String = ""

        ''Generate token start
        'Dim authority As String = ConfigurationManager.AppSettings.[Get]("authority")
        'Dim clientId As String = ConfigurationManager.AppSettings.[Get]("clientId")
        'Dim redirecturl As String = ConfigurationManager.AppSettings.[Get]("redirectUri")
        'Dim resource As String = ConfigurationManager.AppSettings.[Get]("resource")
        'Dim clientSecret As String = ConfigurationManager.AppSettings.[Get]("clientSecret")
        'Dim AccessToken As String

        'Dim resultToken = Task.Run(Async Function()
        '                               Dim clientCredential = New ClientCredential(clientId, clientSecret)
        '                               Dim context As AuthenticationContext = New AuthenticationContext(authority, False)
        '                               Token = Await context.AcquireTokenAsync(resource, clientCredential)
        '                           End Function)

        'Dim tokenno As String
        'tokenno = AccessToken
        ''Generate token end


        Dim apiGetSitesList As List(Of apiGetSiteItem) = New List(Of apiGetSiteItem)()
        Dim serializer As JavaScriptSerializer = New JavaScriptSerializer()
        Dim apiGetSitesPath As String = ConfigurationManager.AppSettings("apiGetSites")

        Dim filter As String = "Filter=externalSiteId eq '" & LocationID & "'"

        Using client = New HttpClient()

            If Not String.IsNullOrWhiteSpace(Token) Then
                client.DefaultRequestHeaders.Clear()
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings.[Get]("subscriptionId"))
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " & Token)
                client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
            End If

            Try
                Dim apiGetSitesPath1 As String = apiGetSitesPath & "?" & filter
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
                Dim response As HttpResponseMessage = client.GetAsync(apiGetSitesPath1).Result
                Dim responseString = response.Content.ReadAsStringAsync().Result

                If response.IsSuccessStatusCode Then
                    serializer = New JavaScriptSerializer()
                    Dim jObject = Newtonsoft.Json.Linq.JObject.Parse(responseString)
                    Dim result1 As String
                    result1 = jObject("items").ToString()
                    apiGetSitesList = serializer.Deserialize(Of List(Of apiGetSiteItem))(result1)

                End If

            Catch ex As Exception
                result = "Error in apiGetSitesPath - " & ex.Message
                Return result
            End Try
        End Using

        Dim apiGetSiteItem As apiGetSiteItem = apiGetSitesList.Where(Function(x) x.externalSiteId = LocationID).FirstOrDefault()
        Dim apiGetDevicebySiteIdlist As List(Of apiGetDeviceBySiteIdItem) = New List(Of apiGetDeviceBySiteIdItem)()
        Dim apiGetDevicebySiteIdlist1 As List(Of apiGetDeviceBySiteIdItem) = New List(Of apiGetDeviceBySiteIdItem)()
        Dim apiGetDevicesBySiteIDPath As String = ConfigurationManager.AppSettings("apiGetDevicesBySiteID")
        Dim pageIndex As Integer
        Dim totalPages As Integer
        Dim paging As Integer
        Dim PAGE_SIZE As Double
        pageIndex = 0
        totalPages = 0
        PAGE_SIZE = 1000


        If Not apiGetSiteItem Is Nothing Then

            Try
                Using client = New HttpClient()
                    If Not String.IsNullOrWhiteSpace(Token) Then
                        client.DefaultRequestHeaders.Clear()
                        client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings.[Get]("subscriptionId"))
                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " & Token)
                        client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
                    End If
                    Dim apiGetDevicesBySiteIDPath1 As String = apiGetDevicesBySiteIDPath.Replace("id", apiGetSiteItem.id)
                    apiGetDevicesBySiteIDPath1 = apiGetDevicesBySiteIDPath1 + "?PageSize=" + PAGE_SIZE.ToString()
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
                    Dim response As HttpResponseMessage = client.GetAsync(apiGetDevicesBySiteIDPath1).Result
                    Dim responseString = response.Content.ReadAsStringAsync().Result
                    If response.IsSuccessStatusCode Then
                        Dim jObject = Newtonsoft.Json.Linq.JObject.Parse(responseString)
                        Dim result1 As String
                        result1 = jObject("items").ToString()
                        pageIndex = Convert.ToInt32(jObject("pageIndex").ToString())
                        totalPages = Convert.ToInt32(jObject("totalPages").ToString())
                        apiGetDevicebySiteIdlist1 = serializer.Deserialize(Of List(Of apiGetDeviceBySiteIdItem))(result1)
                        apiGetDevicebySiteIdlist.AddRange(apiGetDevicebySiteIdlist1)

                    End If
                End Using
            Catch ex As Exception
                result = "Error in apiGetDevicesBySiteIDPath - " & ex.Message
            End Try

            ' Paging start
            If pageIndex <> (totalPages - 1) Then
                For paging = 1 To (totalPages - 1)


                    Try
                        Using client = New HttpClient()
                            If Not String.IsNullOrWhiteSpace(Token) Then
                                client.DefaultRequestHeaders.Clear()
                                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings.[Get]("subscriptionId"))
                                client.DefaultRequestHeaders.Add("Authorization", "Bearer " & Token)
                                client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
                            End If
                            Dim apiGetDevicesBySiteIDPath1 As String = apiGetDevicesBySiteIDPath.Replace("id", apiGetSiteItem.id)
                            apiGetDevicesBySiteIDPath1 = apiGetDevicesBySiteIDPath1 + "?PageSize=" + PAGE_SIZE.ToString() + "&pageIndex=" + paging.ToString()
                            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
                            Dim response As HttpResponseMessage = client.GetAsync(apiGetDevicesBySiteIDPath1).Result
                            Dim responseString = response.Content.ReadAsStringAsync().Result
                            If response.IsSuccessStatusCode Then
                                Dim jObject = Newtonsoft.Json.Linq.JObject.Parse(responseString)
                                Dim result1 As String
                                result1 = jObject("items").ToString()
                                apiGetDevicebySiteIdlist1 = serializer.Deserialize(Of List(Of apiGetDeviceBySiteIdItem))(result1)
                                apiGetDevicebySiteIdlist.AddRange(apiGetDevicebySiteIdlist1)

                            End If
                        End Using
                    Catch ex As Exception
                        result = "Error in apiGetDevicesBySiteIDPath - " & ex.Message
                    End Try
                Next
            End If
            ' Paging end



        End If


        'Get device ID desc from api returned list start
        Dim Description As String

        If Not apiGetDevicebySiteIdlist Is Nothing And apiGetDevicebySiteIdlist.Count > 0 Then
            Description = apiGetDevicebySiteIdlist.OrderByDescending(Function(x) x.comment).Where(Function(x) x.serialNumber = DeviceID).Select(Function(x) x.comment).FirstOrDefault()
        Else
            Description = ""
        End If
        'Get device ID desc from api returned list end



        If Description.ToLower().Contains("<comment>") Then
            Description = Description.Replace("<comment>", "")
            Description = Description.Replace("</comment>", "")
        End If



        sdata = "["

        sdata += "{""deviceid"":"""
        sdata += DeviceID

        sdata += """,""devicedescription"":"""
        sdata += RemoveSpecialCharacter(Description)

        sdata += """},"

        sdata = sdata.Substring(0, (sdata.Length - 1))
        sdata = sdata + "]"

        Return sdata
    End Function



    <System.Web.Services.WebMethod()>
    Public Shared Function GetDeviceidDetails(ByVal floorplanID As Integer, ByVal DeviceID As String) As String
        Dim sdata As String = ""
        Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        Dim list As New List(Of FloorPlanDevices)
        Using con As New MySqlConnection(constr)

            Dim insQuery As String = "SELECT * FROM tblfloorplanitems where deviceid = '" & DeviceID & "' AND floorplanid = " & floorplanID & " "

            Using cmd As New MySqlCommand(insQuery)
                cmd.CommandText = insQuery
                cmd.Connection = con
                con.Open()
                Dim reader As MySqlDataReader = cmd.ExecuteReader()
                Do While reader.Read()
                    Dim item As New FloorPlanDevices()
                    If Not IsDBNull(reader("DeviceID")) Then
                        item.deviceid = CType(reader("DeviceID"), String)
                    Else
                        item.deviceid = ""
                    End If
                    If Not IsDBNull(reader("DevicePlacedDate")) Then
                        Dim DevicePlacedDate = CType(reader("DevicePlacedDate"), DateTime)
                        item.DevicePlacedDateText = DevicePlacedDate.ToString("dd/MM/yyyy hh:mm:ss tt")
                    Else
                        item.DevicePlacedDateText = ""
                    End If

                    list.Add(item)
                Loop
                con.Close()
            End Using
        End Using

        sdata = "["
        For Each item As FloorPlanDevices In list

            sdata += "{""DevicePlaceddate"":"""
            sdata += item.DevicePlacedDateText

            sdata += """},"
        Next item

        sdata = sdata.Substring(0, (sdata.Length - 1))
        sdata = sdata + "]"

        Return sdata

    End Function


    <System.Web.Services.WebMethod()>
    Public Shared Function GetFloorPlanDevices(ByVal floorplanID As Integer, ByVal ShowLabel As String, ByVal LocationID As String) As String

        Dim sdata As String = ""
        Dim bytes As Byte()
        Dim base64String As String = ""
        Dim list As New List(Of FloorPlanItems)
        Dim isRecordExist As Boolean = False
        'If Not Me.IsPostBack Then
        Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

        'To get latest devices 
        Using con As New MySqlConnection(constr)
            Using cmd As New MySqlCommand()
                cmd.CommandType = CommandType.Text
                Dim insQuery As String = "SELECT DeviceID, MAX(rcno) AS RcNo,count(*) as Devicecount FROM tblfloorplanitems WHERE floorplanid = " & floorplanID & "  GROUP BY deviceid"

                cmd.CommandText = insQuery
                cmd.Connection = con
                con.Open()
                Dim reader As MySqlDataReader = cmd.ExecuteReader()
                Do While reader.Read()
                    isRecordExist = True
                    Dim item As New FloorPlanItems()
                    If Not IsDBNull(reader("RcNo")) Then
                        item.RcNo = CType(reader("RcNo"), Integer)
                    Else
                        item.RcNo = 0
                    End If
                    If Not IsDBNull(reader("DeviceID")) Then
                        item.DeviceID = CType(reader("DeviceID"), String)
                    Else
                        item.DeviceID = ""
                    End If
                    If Not IsDBNull(reader("Devicecount")) Then
                        item.DeviceCount = CType(reader("Devicecount"), String)
                    Else
                        item.DeviceCount = ""
                    End If

                    ' item.RcNo = reader("RcNo")
                    'item.DeviceID = reader("DeviceID").ToString()
                    list.Add(item)
                Loop
                con.Close()
            End Using
        End Using

        For Each item As FloorPlanItems In list


            Using con As New MySqlConnection(constr)
                Using cmd As New MySqlCommand()
                    cmd.CommandType = CommandType.Text

                    Dim insQuery As String = "SELECT XCoordinate,YCoordinate,DevicePlacedDate,AliasName,Description from tblfloorplanitems where RcNo = " & item.RcNo & " "

                    cmd.CommandText = insQuery
                    cmd.Connection = con
                    con.Open()
                    Dim reader As MySqlDataReader = cmd.ExecuteReader()
                    Do While reader.Read()

                        If Not IsDBNull(reader("XCoordinate")) Then
                            item.XCoordinate = CType(reader("XCoordinate"), String)
                        Else
                            item.XCoordinate = ""
                        End If

                        If Not IsDBNull(reader("YCoordinate")) Then
                            item.YCoordinate = CType(reader("YCoordinate"), String)
                        Else
                            item.YCoordinate = ""
                        End If

                        If Not IsDBNull(reader("AliasName")) Then
                            item.AliasName = CType(reader("AliasName"), String)
                        Else
                            item.AliasName = ""
                        End If
                        If Not IsDBNull(reader("DevicePlacedDate")) Then
                            item.DevicePlacedDate = reader("DevicePlacedDate")
                        Else
                            item.DevicePlacedDate = DateTime.Now
                        End If
                        If Not Convert.IsDBNull(reader("Description")) Then
                            item.Description = CType(reader("Description"), String)
                        Else
                            item.Description = ""
                        End If


                    Loop
                    con.Close()
                End Using
            End Using

            Using con As New MySqlConnection(constr)
                Using cmd As New MySqlCommand()
                    cmd.CommandType = CommandType.Text
                    'todo
                    Dim insQuery As String = "SELECT CL.devicetype,CL.deviceName,TD.Icon from  tblcustomerlocationdevices CL " & _
                                            "INNER JOIN tbldevicetype TD on TD.devicetype = CL.devicetype " & _
                                            " where CL.DeviceID= '" & item.DeviceID & "' AND deviceid IN (SELECT deviceid FROM tblfloorplanitems where FloorPlanID = " & floorplanID & ") and CL.LocationID='" & LocationID & "'"

                    cmd.CommandText = insQuery
                    cmd.Connection = con
                    con.Open()
                    Dim reader As MySqlDataReader = cmd.ExecuteReader()
                    Do While reader.Read()

                        If Not IsDBNull(reader("DeviceName")) Then
                            item.DeviceName = CType(reader("DeviceName"), String)
                        Else
                            item.DeviceName = ""
                        End If

                        If Not IsDBNull(reader("DeviceType")) Then
                            item.DeviceType = CType(reader("DeviceType"), String)
                        Else
                            item.DeviceType = ""
                        End If
                        'item.DeviceName = reader("DeviceName").ToString()
                        'item.DeviceType = reader("DeviceType").ToString()
                        If Not IsDBNull(TryCast(reader("Icon"), Byte())) Then
                            bytes = TryCast(reader("Icon"), Byte())
                            Try
                                base64String = Convert.ToBase64String(bytes, 0, bytes.Length)
                                item.DeviceUrl = "data:image/png;base64," & base64String
                            Catch ex As Exception
                                base64String = ""
                            End Try
                        End If
                    Loop
                    If item.DeviceUrl Is Nothing Then
                        item.DeviceUrl = "NoImage"
                    End If
                    con.Close()
                End Using
            End Using

            'Dim isdescriptionexists As Boolean = False
            'Using con As New MySqlConnection(constr)
            '    Using cmd As New MySqlCommand()
            '        cmd.CommandType = CommandType.Text
            '        Dim insQuery As String = "SELECT description FROM tblcustomerlocationdevices  WHERE deviceid ='" & item.DeviceID & "'  "
            '        cmd.CommandText = insQuery
            '        cmd.Connection = con
            '        con.Open()
            '        Dim reader As MySqlDataReader = cmd.ExecuteReader()
            '        Do While reader.Read()
            '            isdescriptionexists = True
            '            If Not Convert.IsDBNull(reader("description")) Then
            '                item.Description = CType(reader("description"), String)
            '            Else
            '                item.Description = ""
            '            End If
            '        Loop
            '        If isdescriptionexists = False Then
            '            item.Description = ""
            '        End If
            '        con.Close()
            '    End Using
            'End Using
        Next



        If isRecordExist = True Then
            sdata = "["
            For Each item As FloorPlanItems In list

                sdata += "{""deviceid"":"""
                sdata += item.DeviceID
                sdata += """,""position"":{""lat"":"
                sdata += item.XCoordinate
                sdata += ",""lng"":"
                sdata += item.YCoordinate
                sdata += "},""devicename"":"""
                sdata += item.DeviceName


                If Not item.Description Is Nothing Then
                    If item.Description.ToLower().Contains("<comment>") Then
                        item.Description = item.Description.Replace("<comment>", "")
                        item.Description = item.Description.Replace("</comment>", "")
                    End If
                End If


                If item.DeviceID.ToUpper().Contains("ARROW") Then

                    If item.DeviceID.ToUpper().Contains("UPARROW") Then
                        sdata += """,""devicetype"":"""
                        sdata += "UpArrow"
                        sdata += """,""itemtype"":"""
                        sdata += "ARROW"
                    ElseIf item.DeviceID.ToUpper().Contains("DOWNARROW") Then

                        sdata += """,""devicetype"":"""
                        sdata += "DownArrow"
                        sdata += """,""itemtype"":"""
                        sdata += "ARROW"
                    ElseIf item.DeviceID.ToUpper().Contains("RIGHTARROW") And Not (item.DeviceID.ToUpper().Contains("UPRIGHT")) And Not (item.DeviceID.ToUpper().Contains("DOWNRIGHT")) Then

                        sdata += """,""devicetype"":"""
                        sdata += "RightArrow"
                        sdata += """,""itemtype"":"""
                        sdata += "ARROW"
                    ElseIf item.DeviceID.ToUpper().Contains("LEFTARROW") And Not (item.DeviceID.ToUpper().Contains("UPLEFT")) And Not (item.DeviceID.ToUpper().Contains("DOWNLEFT")) Then

                        sdata += """,""devicetype"":"""
                        sdata += "LeftArrow"
                        sdata += """,""itemtype"":"""
                        sdata += "ARROW"
                    ElseIf item.DeviceID.ToUpper().Contains("UPLEFTARROW") Then

                        sdata += """,""devicetype"":"""
                        sdata += "UpLeftArrow"
                        sdata += """,""itemtype"":"""
                        sdata += "ARROW"
                    ElseIf item.DeviceID.ToUpper().Contains("UPRIGHTARROW") Then

                        sdata += """,""devicetype"":"""
                        sdata += "UpRightArrow"
                        sdata += """,""itemtype"":"""
                        sdata += "ARROW"
                    ElseIf item.DeviceID.ToUpper().Contains("DOWNLEFTARROW") Then

                        sdata += """,""devicetype"":"""
                        sdata += "DownLeftArrow"
                        sdata += """,""itemtype"":"""
                        sdata += "ARROW"
                    ElseIf item.DeviceID.ToUpper().Contains("DOWNRIGHTARROW") Then

                        sdata += """,""devicetype"":"""
                        sdata += "DownRightArrow"
                        sdata += """,""itemtype"":"""
                        sdata += "ARROW"
                    End If

                Else
                    sdata += """,""devicetype"":"""
                    sdata += item.DeviceType
                    sdata += """,""itemtype"":"""
                    sdata += "DEVICE"
                End If

                sdata += """,""deviceurl"":"""
                sdata += item.DeviceUrl

                sdata += """,""RcNo"":"""
                sdata += item.RcNo.ToString()

                sdata += """,""DevicePlacedDate"":"""
                sdata += item.DevicePlacedDate.ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)

                sdata += """,""AliasName"":"""
                sdata += item.AliasName

                sdata += """,""ShowLabel"":"""
                sdata += ShowLabel

                sdata += """,""Description"":"""
                sdata += RemoveSpecialCharacter(item.Description)

                sdata += """,""DeviceCount"":"""
                sdata += item.DeviceCount.ToString()

                sdata += """},"
            Next item

            sdata = sdata.Substring(0, (sdata.Length - 1))
            sdata = sdata + "]"
        End If
        Return sdata
    End Function

    Public Shared Function RemoveSpecialCharacter(ByVal Description As String) As String
        If Not Description Is Nothing And Description <> "" Then
            Description = Description.Trim()
            Description = Description.Replace(vbCr, "").Replace(vbLf, "")
            Description = Replace(Description, """", "")
            Description = Replace(Description, "[", "")
            Description = Replace(Description, "]", "")
            Description = Replace(Description, ")", "")
            Description = Replace(Description, "(", "")
            Return Description
        Else
            Return ""
        End If
    End Function

    '<System.Web.Services.WebMethod()>
    'Public Shared Function DeleteFloorPlan(ByVal floorplanID As Integer) As String
    '    Dim sdata As String = ""
    '    Dim isdeleted As Integer
    '    Dim list As New List(Of FloorPlanItems)
    '    Dim isRecordExist As Boolean = False
    '    'If Not Me.IsPostBack Then
    '    Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    '    Using con As New MySqlConnection(constr)
    '        Using cmd As New MySqlCommand()
    '            cmd.CommandType = CommandType.Text
    '            Dim insQuery As String = "Delete from tblcustomerlocationfloorplan where floorplanId = " & floorplanID & " "

    '            cmd.CommandText = insQuery
    '            cmd.Connection = con
    '            con.Open()
    '            isdeleted = cmd.ExecuteNonQuery()
    '            con.Close()
    '        End Using
    '    End Using

    '    If isdeleted = 1 Then
    '        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "ShowStatus", "showpopup();", True)

    '    End If

    '    Return sdata
    'End Function

    Protected Sub ddlfloorPlanList_SelectedIndexChanged(sender As Object, e As EventArgs)
        ''ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "ShowStatus", "onclickGraph();", True)
        'Dim floorplanID As Integer
        'If ddlfloorPlanList.SelectedValue <> "" Then
        '    floorplanID = ddlfloorPlanList.SelectedValue
        'End If
        'DeviceDislay(floorplanID)
    End Sub

    Protected Sub DeviceEventListBox_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Protected Sub btndelete_Click(sender As Object, e As EventArgs)
        Dim sLocationID As String = ""
        sLocationID = Request.QueryString("LocationID").ToString()
        'If Not (Session("LocationID") Is Nothing) Then
        '    sLocationID = Session("LocationID")

        'End If

        Dim sdata As String = ""
        Dim isdeleted As Integer
        Dim list As New List(Of FloorPlanItems)
        Dim isRecordExist As Boolean = False

        Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        Using con As New MySqlConnection(constr)
            Using cmd As New MySqlCommand()
                cmd.CommandType = CommandType.Text
                Dim insQuery As String = "Delete from tblfloorplanitems where floorplanId = " & ddlfloorPlanList.SelectedValue & " "

                cmd.CommandText = insQuery
                cmd.Connection = con
                con.Open()
                isdeleted = cmd.ExecuteNonQuery()
                con.Close()
            End Using
        End Using


        'If Not Me.IsPostBack Then
        Using con As New MySqlConnection(constr)
            Using cmd As New MySqlCommand()
                cmd.CommandType = CommandType.Text
                Dim insQuery As String = "Delete from tblcustomerlocationfloorplan where floorplanId = " & ddlfloorPlanList.SelectedValue & " "

                cmd.CommandText = insQuery
                cmd.Connection = con
                con.Open()
                isdeleted = cmd.ExecuteNonQuery()
                con.Close()
            End Using
        End Using

        If isdeleted > 0 Then
            BindFloorPlanDropdown(sLocationID)
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "ShowStatus", "deletepopup();", True)
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "ShowStatus", "ddlfloorPlanChange();", True)


        End If
    End Sub

    Protected Sub btnexporttoexcel_ServerClick(sender As Object, e As EventArgs)

        Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        Dim tblfloorplanitemslist As New List(Of tblfloorplanItemsdevices)
        Using con As New MySqlConnection(constr)
            Using cmd As New MySqlCommand()
                cmd.CommandType = CommandType.Text
                Dim insQuery As String = "SELECT *  FROM tblfloorplanitems WHERE floorplanid = " & ddlfloorPlanList.SelectedValue & ""
                cmd.CommandText = insQuery
                cmd.Connection = con
                con.Open()
                Dim reader As MySqlDataReader = cmd.ExecuteReader()
                Do While reader.Read()

                    Dim item As New tblfloorplanItemsdevices()
                    If Not IsDBNull(reader("DeviceID")) Then
                        item.DeviceID = CType(reader("DeviceID"), String)
                    Else
                        item.DeviceID = ""
                    End If
                    If Not IsDBNull(reader("AliasName")) Then
                        item.AliasName = CType(reader("AliasName"), String)
                    Else
                        item.AliasName = ""
                    End If
                    tblfloorplanitemslist.Add(item)
                Loop
                con.Close()
            End Using
        End Using
        If tblfloorplanitemslist.Count > 0 Then
            For Each item In tblfloorplanitemslist
                Using con As New MySqlConnection(constr)
                    Using cmd As New MySqlCommand()
                        cmd.CommandType = CommandType.Text
                        Dim insQuery As String = "SELECT *  FROM tblcustomerlocationfloorplan WHERE floorplanid = " & ddlfloorPlanList.SelectedValue & ""
                        cmd.CommandText = insQuery
                        cmd.Connection = con
                        con.Open()
                        Dim reader As MySqlDataReader = cmd.ExecuteReader()
                        Do While reader.Read()
                            If Not IsDBNull(reader("FloorPlanName")) Then
                                item.FloorplanName = CType(reader("FloorPlanName"), String)
                            Else
                                item.FloorplanName = ""
                            End If
                        Loop
                        con.Close()
                    End Using
                End Using

                Using con As New MySqlConnection(constr)
                    Using cmd As New MySqlCommand()
                        cmd.CommandType = CommandType.Text
                        'todo
                        Dim insQuery As String = "SELECT *  FROM tblcustomerlocationdevices WHERE deviceid = '" & item.DeviceID & "' and deviceid IN (SELECT deviceid FROM tblfloorplanitems where FloorPlanID = " & ddlfloorPlanList.SelectedValue & ")"
                        cmd.CommandText = insQuery
                        cmd.Connection = con
                        con.Open()
                        Dim reader As MySqlDataReader = cmd.ExecuteReader()
                        Do While reader.Read()
                            If Not IsDBNull(reader("Description")) Then
                                item.description = CType(reader("Description"), String)
                            Else
                                item.description = ""
                            End If
                        Loop
                        con.Close()
                    End Using
                End Using
            Next
        End If
        Dim workbook As HSSFWorkbook = New HSSFWorkbook()
        Dim sheet = workbook.CreateSheet()
        Dim headerFont As NPOI.SS.UserModel.IFont = workbook.CreateFont()
        headerFont.FontHeightInPoints = 11
        headerFont.FontName = "Calibri"

        sheet.SetColumnWidth(0, 6000)
        sheet.SetColumnWidth(1, 6000)
        sheet.SetColumnWidth(2, 6000)
        sheet.SetColumnWidth(3, 6000)

        Dim headerRow = sheet.CreateRow(0)
        headerRow.CreateCell(0).SetCellValue("Alias")
        headerRow.CreateCell(1).SetCellValue("SerialNumber")
        headerRow.CreateCell(2).SetCellValue("Description")
        headerRow.CreateCell(3).SetCellValue("FloorPlan Name")

        Dim rowNumber As Integer = 1
        Dim format As IDataFormat = workbook.CreateDataFormat()
        Dim textStyle As ICellStyle = workbook.CreateCellStyle()
        textStyle.DataFormat = format.GetFormat("text")
        For Each item In tblfloorplanitemslist
            rowNumber += 1
            Dim row = sheet.CreateRow(rowNumber)
            row.CreateCell(0).SetCellValue(item.AliasName)
            row.CreateCell(1).SetCellValue(item.DeviceID)
            row.CreateCell(2).SetCellValue(item.description)
            row.CreateCell(3).SetCellValue(item.FloorplanName)

            row.Cells(0).CellStyle = textStyle
            row.Cells(1).CellStyle = textStyle
            row.Cells(2).CellStyle = textStyle
            row.Cells(3).CellStyle = textStyle


        Next

        Dim filename As String
        filename = "FloorPlandata_" + Request.QueryString("LocationID").ToString() + "_" + ddlfloorPlanList.SelectedItem.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls"
        filename = filename.Replace(" ", "_")

        Dim output As MemoryStream = New MemoryStream()
        Response.Clear()
        workbook.Write(output)
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=" + filename + "")
        Response.Charset = ""
        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
        Response.BinaryWrite(output.GetBuffer())
        Response.End()
    End Sub

    <System.Web.Services.WebMethod()>
    Public Shared Function GetDeviceIconsImage() As String
        Dim sdata As String = ""
        Dim bytes As Byte()
        Dim base64String As String = ""
        Dim isRecordExist As Boolean = False
        Dim list As New List(Of DevicetypesIcons)

        Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        Using con As New MySqlConnection(constr)
            Using cmd As New MySqlCommand()
                cmd.CommandType = CommandType.Text
                Dim insQuery As String = "SELECT * from tbldevicetype GROUP BY DeviceType"
                'Dim insQuery As String
                cmd.CommandText = insQuery
                cmd.Connection = con
                con.Open()
                Dim reader As MySqlDataReader = cmd.ExecuteReader()
                Do While reader.Read()
                    isRecordExist = True
                    Dim item As New DevicetypesIcons
                    If Not IsDBNull(TryCast(reader("Icon"), Byte())) Then
                        item.bytes = TryCast(reader("Icon"), Byte())
                        Try
                            item.base64string = Convert.ToBase64String(item.bytes, 0, item.bytes.Length)
                        Catch ex As Exception
                            item.base64string = ""
                        End Try
                    End If

                    If Not IsDBNull(reader("DeviceDescription")) Then
                        item.description = CType(reader("DeviceDescription"), String)
                    Else
                        item.description = ""
                    End If
                    list.Add(item)
                Loop
                con.Close()
            End Using
        End Using

        If isRecordExist = True Then
            sdata = "["
            For Each item As DevicetypesIcons In list

                sdata += "{""name"":"""
                sdata += item.base64string

                sdata += """,""Description"":"""
                sdata += RemoveSpecialCharacter(item.description)


                sdata += """},"
            Next item

            sdata = sdata.Substring(0, (sdata.Length - 1))
            sdata = sdata + "]"
        End If
        Return sdata

    End Function


    'To display the DevicePlaced Date and Corresponding Description Start

    <System.Web.Services.WebMethod()>
    Public Shared Function GetDeviceHistory(ByVal floorplanID As String, ByVal deviceId As String) As String

        Dim sdata As String = ""
        Dim isRecordExist As Boolean = False
        Dim tblfloorplanitemslist As New List(Of FloorPlanItems)
        Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

        Using con As New MySqlConnection(constr)
            Using cmd As New MySqlCommand()
                cmd.CommandType = CommandType.Text
                Dim insQuery As String = "SELECT *  FROM tblfloorplanitems WHERE floorplanid = " & floorplanID & " and Deviceid = '" & deviceId & "'"
                cmd.CommandText = insQuery
                cmd.Connection = con
                con.Open()
                Dim reader As MySqlDataReader = cmd.ExecuteReader()
                Do While reader.Read()
                    isRecordExist = True
                    Dim item As New FloorPlanItems()
                    If Not IsDBNull(reader("DevicePlacedDate")) Then
                        item.DevicePlacedDate = reader("DevicePlacedDate")
                    Else
                        item.DevicePlacedDate = DateTime.Now
                    End If
                    If Not IsDBNull(reader("Description")) Then
                        item.Description = CType(reader("Description"), String)
                    Else
                        item.Description = ""
                    End If
                    tblfloorplanitemslist.Add(item)
                Loop
                con.Close()
            End Using
        End Using

        If isRecordExist = True Then
            sdata = "["
            For Each item As FloorPlanItems In tblfloorplanitemslist

                sdata += "{""DevicePlacedDate"":"""
                sdata += item.DevicePlacedDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)

                sdata += """,""Description"":"""
                sdata += RemoveSpecialCharacter(item.Description)

                sdata += """,""DevicePlacedTime"":"""
                sdata += item.DevicePlacedDate.ToString("hh:mm:ss tt", CultureInfo.InvariantCulture)


                sdata += """},"
            Next item

            sdata = sdata.Substring(0, (sdata.Length - 1))
            sdata = sdata + "]"
        End If
        Return sdata

    End Function

    'To display the DevicePlaced Date and Corresponding Description End

    Protected Sub btnFloorPlanThreshold_Click(sender As Object, e As EventArgs)

        ClearThresholdValues()

        Dim threshold As New tblFloorPlanThreshold()
        Dim floorPlanID As Integer
        floorPlanID = ddlfloorPlanList.SelectedValue

        threshold = GetFloorPlanThreshold(floorPlanID)

        If Not threshold Is Nothing Then

            HiddenFloorPlanThresholdRCNo.Value = threshold.RcNo
            lblFloorPlantext.Text = ddlfloorPlanList.SelectedItem.Text
            'Daily
            txtDailyNoActivityLabel.Text = threshold.DailyNoActivityLabel
            txtDailyLowLabel.Text = threshold.DailyLowActivityLabel
            txtDailyMediumLabel.Text = threshold.DailyMediumActivityLabel
            txtDailyHighLabel.Text = threshold.DailyHighActivityLabel

            txtDailyNoActivity.Text = threshold.DailyNoActivity
            txtDailyLow.Text = threshold.DailyLowActivity
            txtDailyMedium.Text = threshold.DailyMediumActivity
            txtDailyHigh.Text = threshold.DailyHighActivity

            txtDailyNoActivityColor.Text = threshold.DailyNoActivityColor
            txtDailyLowColor.Text = threshold.DailyLowActivityColor
            txtDailyMediumColor.Text = threshold.DailyMediumActivityColor
            txtDailyHighColor.Text = threshold.DailyHighActivityColor

            'Weekly
            txtWeeklyNoActivityLabel.Text = threshold.WeeklyNoActivityLabel
            txtWeeklyLowLabel.Text = threshold.WeeklyLowActivityLabel
            txtWeeklyMediumLabel.Text = threshold.WeeklyMediumActivityLabel
            txtWeeklyHighLabel.Text = threshold.WeeklyHighActivityLabel

            txtWeeklyNoActivity.Text = threshold.WeeklyNoActivity
            txtWeeklyLow.Text = threshold.WeeklyLowActivity
            txtWeeklyMedium.Text = threshold.WeeklyMediumActivity
            txtWeeklyHigh.Text = threshold.WeeklyHighActivity

            txtWeeklyNoActivityColor.Text = threshold.WeeklyNoActivityColor
            txtWeeklyLowColor.Text = threshold.WeeklyLowActivityColor
            txtWeeklyMediumColor.Text = threshold.WeeklyMediumActivityColor
            txtWeeklyHighColor.Text = threshold.WeeklyHighActivityColor

            'Monthly
            txtMonthlyNoActivityLabel.Text = threshold.MonthlyNoActivityLabel
            txtMonthlyLowLabel.Text = threshold.MonthlyLowActivityLabel
            txtMonthlyMediumLabel.Text = threshold.MonthlyMediumActivityLabel
            txtMonthlyHighLabel.Text = threshold.MonthlyHighActivityLabel

            txtMonthlyNoActivity.Text = threshold.MonthlyNoActivity
            txtMonthlyLow.Text = threshold.MonthlyLowActivity
            txtMonthlyMedium.Text = threshold.MonthlyMediumActivity
            txtMonthlyHigh.Text = threshold.MonthlyHighActivity

            txtMonthlyNoActivityColor.Text = threshold.MonthlyNoActivityColor
            txtMonthlyLowColor.Text = threshold.MonthlyLowActivityColor
            txtMonthlyMediumColor.Text = threshold.MonthlyMediumActivityColor
            txtMonthlyHighColor.Text = threshold.MonthlyHighActivityColor

            'Yearly
            txtYearlyNoActivityLabel.Text = threshold.YearlyNoActivityLabel
            txtYearlyLowLabel.Text = threshold.YearlyLowActivityLabel
            txtYearlyMediumLabel.Text = threshold.YearlyMediumActivityLabel
            txtYearlyHighLabel.Text = threshold.YearlyHighActivityLabel

            txtYearlyNoActivity.Text = threshold.YearlyNoActivity
            txtYearlyLow.Text = threshold.YearlyLowActivity
            txtYearlyMedium.Text = threshold.YearlyMediumActivity
            txtYearlyHigh.Text = threshold.YearlyHighActivity

            txtYearlyNoActivityColor.Text = threshold.YearlyNoActivityColor
            txtYearlyLowColor.Text = threshold.YearlyLowActivityColor
            txtYearlyMediumColor.Text = threshold.YearlyMediumActivityColor
            txtYearlyHighColor.Text = threshold.YearlyHighActivityColor

        End If


        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "ShowStatus", "showFloorPlanThresholdpopup();", True)


    End Sub

    Protected Sub btnSaveFloorPlanThreshold_Click(sender As Object, e As EventArgs)

        Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

        If HiddenFloorPlanThresholdRCNo.Value = 0 Then
            'Add new Device Event Threshold
            Using con As New MySqlConnection(constr)
                Dim query As String = "INSERT INTO tblfloorplanthreshold(FloorPlanID,DailyNoActivity,DailyNoActivityLabel,DailyNoActivityColor,DailyLowActivity,DailyLowActivityLabel,DailyLowActivityColor,DailyMediumActivity,DailyMediumActivityLabel,DailyMediumActivityColor,DailyHighActivity,DailyHighActivityLabel,DailyHighActivityColor," & _
                                        "WeeklyNoActivity,WeeklyNoActivityLabel,WeeklyNoActivityColor,WeeklyLowActivity,WeeklyLowActivityLabel,WeeklyLowActivityColor,WeeklyMediumActivity,WeeklyMediumActivityLabel,WeeklyMediumActivityColor,WeeklyHighActivity,WeeklyHighActivityLabel,WeeklyHighActivityColor," & _
                                        "MonthlyNoActivity,MonthlyNoActivityLabel,MonthlyNoActivityColor,MonthlyLowActivity,MonthlyLowActivityLabel,MonthlyLowActivityColor,MonthlyMediumActivity,MonthlyMediumActivityLabel,MonthlyMediumActivityColor,MonthlyHighActivity,MonthlyHighActivityLabel,MonthlyHighActivityColor," & _
                                        "YearlyNoActivity,YearlyNoActivityLabel,YearlyNoActivityColor,YearlyLowActivity,YearlyLowActivityLabel,YearlyLowActivityColor,YearlyMediumActivity,YearlyMediumActivityLabel,YearlyMediumActivityColor,YearlyHighActivity,YearlyHighActivityLabel,YearlyHighActivityColor" & _
                                        ") " & _
                                        " VALUES(@FloorPlanID,@DailyNoActivity,@DailyNoActivityLabel,@DailyNoActivityColor,@DailyLowActivity,@DailyLowActivityLabel,@DailyLowActivityColor,@DailyMediumActivity,@DailyMediumActivityLabel,@DailyMediumActivityColor,@DailyHighActivity,@DailyHighActivityLabel,@DailyHighActivityColor," & _
                                        "@WeeklyNoActivity,@WeeklyNoActivityLabel,@WeeklyNoActivityColor,@WeeklyLowActivity,@WeeklyLowActivityLabel,@WeeklyLowActivityColor,@WeeklyMediumActivity,@WeeklyMediumActivityLabel,@WeeklyMediumActivityColor,@WeeklyHighActivity,@WeeklyHighActivityLabel,@WeeklyHighActivityColor," & _
                                        "@MonthlyNoActivity,@MonthlyNoActivityLabel,@MonthlyNoActivityColor,@MonthlyLowActivity,@MonthlyLowActivityLabel,@MonthlyLowActivityColor,@MonthlyMediumActivity,@MonthlyMediumActivityLabel,@MonthlyMediumActivityColor,@MonthlyHighActivity,@MonthlyHighActivityLabel,@MonthlyHighActivityColor," & _
                                        "@YearlyNoActivity,@YearlyNoActivityLabel,@YearlyNoActivityColor,@YearlyLowActivity,@YearlyLowActivityLabel,@YearlyLowActivityColor,@YearlyMediumActivity,@YearlyMediumActivityLabel,@YearlyMediumActivityColor,@YearlyHighActivity,@YearlyHighActivityLabel,@YearlyHighActivityColor)"

                Using cmd As New MySqlCommand(query)
                    cmd.Connection = con


                    cmd.Parameters.AddWithValue("@FloorPlanID", ddlfloorPlanList.SelectedValue)
                    cmd.Parameters.AddWithValue("@DailyNoActivity", txtDailyNoActivity.Text)
                    cmd.Parameters.AddWithValue("@DailyNoActivityLabel", txtDailyNoActivityLabel.Text)
                    cmd.Parameters.AddWithValue("@DailyNoActivityColor", txtDailyNoActivityColor.Text)
                    cmd.Parameters.AddWithValue("@DailyLowActivity", txtDailyLow.Text)
                    cmd.Parameters.AddWithValue("@DailyLowActivityLabel", txtDailyLowLabel.Text)
                    cmd.Parameters.AddWithValue("@DailyLowActivityColor", txtDailyLowColor.Text)
                    cmd.Parameters.AddWithValue("@DailyMediumActivity", txtDailyMedium.Text)
                    cmd.Parameters.AddWithValue("@DailyMediumActivityLabel", txtDailyMediumLabel.Text)
                    cmd.Parameters.AddWithValue("@DailyMediumActivityColor", txtDailyMediumColor.Text)
                    cmd.Parameters.AddWithValue("@DailyHighActivity", txtDailyHigh.Text)
                    cmd.Parameters.AddWithValue("@DailyHighActivityLabel", txtDailyHighLabel.Text)
                    cmd.Parameters.AddWithValue("@DailyHighActivityColor", txtDailyHighColor.Text)

                    cmd.Parameters.AddWithValue("@WeeklyNoActivity", txtWeeklyNoActivity.Text)
                    cmd.Parameters.AddWithValue("@WeeklyNoActivityLabel", txtWeeklyNoActivityLabel.Text)
                    cmd.Parameters.AddWithValue("@WeeklyNoActivityColor", txtWeeklyNoActivityColor.Text)
                    cmd.Parameters.AddWithValue("@WeeklyLowActivity", txtWeeklyLow.Text)
                    cmd.Parameters.AddWithValue("@WeeklyLowActivityLabel", txtWeeklyLowLabel.Text)
                    cmd.Parameters.AddWithValue("@WeeklyLowActivityColor", txtWeeklyLowColor.Text)
                    cmd.Parameters.AddWithValue("@WeeklyMediumActivity", txtWeeklyMedium.Text)
                    cmd.Parameters.AddWithValue("@WeeklyMediumActivityLabel", txtWeeklyMediumLabel.Text)
                    cmd.Parameters.AddWithValue("@WeeklyMediumActivityColor", txtWeeklyMediumColor.Text)
                    cmd.Parameters.AddWithValue("@WeeklyHighActivity", txtWeeklyHigh.Text)
                    cmd.Parameters.AddWithValue("@WeeklyHighActivityLabel", txtWeeklyHighLabel.Text)
                    cmd.Parameters.AddWithValue("@WeeklyHighActivityColor", txtWeeklyHighColor.Text)

                    cmd.Parameters.AddWithValue("@MonthlyNoActivity", txtMonthlyNoActivity.Text)
                    cmd.Parameters.AddWithValue("@MonthlyNoActivityLabel", txtMonthlyNoActivityLabel.Text)
                    cmd.Parameters.AddWithValue("@MonthlyNoActivityColor", txtMonthlyNoActivityColor.Text)
                    cmd.Parameters.AddWithValue("@MonthlyLowActivity", txtMonthlyLow.Text)
                    cmd.Parameters.AddWithValue("@MonthlyLowActivityLabel", txtMonthlyLowLabel.Text)
                    cmd.Parameters.AddWithValue("@MonthlyLowActivityColor", txtMonthlyLowColor.Text)
                    cmd.Parameters.AddWithValue("@MonthlyMediumActivity", txtMonthlyMedium.Text)
                    cmd.Parameters.AddWithValue("@MonthlyMediumActivityLabel", txtMonthlyMediumLabel.Text)
                    cmd.Parameters.AddWithValue("@MonthlyMediumActivityColor", txtMonthlyMediumColor.Text)
                    cmd.Parameters.AddWithValue("@MonthlyHighActivity", txtMonthlyHigh.Text)
                    cmd.Parameters.AddWithValue("@MonthlyHighActivityLabel", txtMonthlyHighLabel.Text)
                    cmd.Parameters.AddWithValue("@MonthlyHighActivityColor", txtMonthlyHighColor.Text)

                    cmd.Parameters.AddWithValue("@YearlyNoActivity", txtYearlyNoActivity.Text)
                    cmd.Parameters.AddWithValue("@YearlyNoActivityLabel", txtYearlyNoActivityLabel.Text)
                    cmd.Parameters.AddWithValue("@YearlyNoActivityColor", txtYearlyNoActivityColor.Text)
                    cmd.Parameters.AddWithValue("@YearlyLowActivity", txtYearlyLow.Text)
                    cmd.Parameters.AddWithValue("@YearlyLowActivityLabel", txtYearlyLowLabel.Text)
                    cmd.Parameters.AddWithValue("@YearlyLowActivityColor", txtYearlyLowColor.Text)
                    cmd.Parameters.AddWithValue("@YearlyMediumActivity", txtYearlyMedium.Text)
                    cmd.Parameters.AddWithValue("@YearlyMediumActivityLabel", txtYearlyMediumLabel.Text)
                    cmd.Parameters.AddWithValue("@YearlyMediumActivityColor", txtYearlyMediumColor.Text)
                    cmd.Parameters.AddWithValue("@YearlyHighActivity", txtYearlyHigh.Text)
                    cmd.Parameters.AddWithValue("@YearlyHighActivityLabel", txtYearlyHighLabel.Text)
                    cmd.Parameters.AddWithValue("@YearlyHighActivityColor", txtYearlyHighColor.Text)

                    con.Open()
                    cmd.ExecuteNonQuery()
                    'To Get Last inserted id
                    cmd.Parameters.AddWithValue("newId", cmd.LastInsertedId)
                    HiddenFloorPlanThresholdRCNo.Value = Convert.ToInt32(cmd.Parameters("@newId").Value)

                    con.Close()
                End Using
            End Using
        Else

            'Update existing Device Event Threshold
            Using con As New MySqlConnection(constr)
                Dim query As String = " UPDATE tblfloorplanthreshold SET FloorPlanID = @FloorPlanID ,DailyNoActivity = @DailyNoActivity ,DailyNoActivityLabel = @DailyNoActivityLabel ,DailyNoActivityColor = @DailyNoActivityColor ,DailyLowActivity = @DailyLowActivity " & _
                                        ",DailyLowActivityLabel = @DailyLowActivityLabel ,DailyLowActivityColor = @DailyLowActivityColor ,DailyMediumActivity = @DailyMediumActivity ,DailyMediumActivityLabel = @DailyMediumActivityLabel ,DailyMediumActivityColor = @DailyMediumActivityColor" & _
                                        ",DailyHighActivity = @DailyHighActivity ,DailyHighActivityLabel = @DailyHighActivityLabel,DailyHighActivityColor = @DailyHighActivityColor," & _
                                        "WeeklyNoActivity = @WeeklyNoActivity ,WeeklyNoActivityLabel = @WeeklyNoActivityLabel ,WeeklyNoActivityColor = @WeeklyNoActivityColor ,WeeklyLowActivity = @WeeklyLowActivity " & _
                                        ",WeeklyLowActivityLabel = @WeeklyLowActivityLabel ,WeeklyLowActivityColor = @WeeklyLowActivityColor ,WeeklyMediumActivity = @WeeklyMediumActivity ,WeeklyMediumActivityLabel = @WeeklyMediumActivityLabel ,WeeklyMediumActivityColor = @WeeklyMediumActivityColor" & _
                                        ",WeeklyHighActivity = @WeeklyHighActivity ,WeeklyHighActivityLabel = @WeeklyHighActivityLabel,WeeklyHighActivityColor = @WeeklyHighActivityColor," & _
                                        "MonthlyNoActivity = @MonthlyNoActivity ,MonthlyNoActivityLabel = @MonthlyNoActivityLabel ,MonthlyNoActivityColor = @MonthlyNoActivityColor ,MonthlyLowActivity = @MonthlyLowActivity " & _
                                        ",MonthlyLowActivityLabel = @MonthlyLowActivityLabel ,MonthlyLowActivityColor = @MonthlyLowActivityColor ,MonthlyMediumActivity = @MonthlyMediumActivity ,MonthlyMediumActivityLabel = @MonthlyMediumActivityLabel ,MonthlyMediumActivityColor = @MonthlyMediumActivityColor" & _
                                        ",MonthlyHighActivity = @MonthlyHighActivity ,MonthlyHighActivityLabel = @MonthlyHighActivityLabel,MonthlyHighActivityColor = @MonthlyHighActivityColor," & _
                                        "YearlyNoActivity = @YearlyNoActivity ,YearlyNoActivityLabel = @YearlyNoActivityLabel ,YearlyNoActivityColor = @YearlyNoActivityColor ,YearlyLowActivity = @YearlyLowActivity " & _
                                        ",YearlyLowActivityLabel = @YearlyLowActivityLabel ,YearlyLowActivityColor = @YearlyLowActivityColor ,YearlyMediumActivity = @YearlyMediumActivity ,YearlyMediumActivityLabel = @YearlyMediumActivityLabel ,YearlyMediumActivityColor = @YearlyMediumActivityColor" & _
                                        ",YearlyHighActivity = @YearlyHighActivity ,YearlyHighActivityLabel = @YearlyHighActivityLabel,YearlyHighActivityColor = @YearlyHighActivityColor " & _
                                        "WHERE RcNo = @RcNo "

                Using cmd As New MySqlCommand(query)
                    cmd.Connection = con

                    cmd.Parameters.AddWithValue("@RcNo", HiddenFloorPlanThresholdRCNo.Value)

                    cmd.Parameters.AddWithValue("@FloorPlanID", ddlfloorPlanList.SelectedValue)
                    cmd.Parameters.AddWithValue("@DailyNoActivity", txtDailyNoActivity.Text)
                    cmd.Parameters.AddWithValue("@DailyNoActivityLabel", txtDailyNoActivityLabel.Text)
                    cmd.Parameters.AddWithValue("@DailyNoActivityColor", txtDailyNoActivityColor.Text)
                    cmd.Parameters.AddWithValue("@DailyLowActivity", txtDailyLow.Text)
                    cmd.Parameters.AddWithValue("@DailyLowActivityLabel", txtDailyLowLabel.Text)
                    cmd.Parameters.AddWithValue("@DailyLowActivityColor", txtDailyLowColor.Text)
                    cmd.Parameters.AddWithValue("@DailyMediumActivity", txtDailyMedium.Text)
                    cmd.Parameters.AddWithValue("@DailyMediumActivityLabel", txtDailyMediumLabel.Text)
                    cmd.Parameters.AddWithValue("@DailyMediumActivityColor", txtDailyMediumColor.Text)
                    cmd.Parameters.AddWithValue("@DailyHighActivity", txtDailyHigh.Text)
                    cmd.Parameters.AddWithValue("@DailyHighActivityLabel", txtDailyHighLabel.Text)
                    cmd.Parameters.AddWithValue("@DailyHighActivityColor", txtDailyHighColor.Text)

                    cmd.Parameters.AddWithValue("@WeeklyNoActivity", txtWeeklyNoActivity.Text)
                    cmd.Parameters.AddWithValue("@WeeklyNoActivityLabel", txtWeeklyNoActivityLabel.Text)
                    cmd.Parameters.AddWithValue("@WeeklyNoActivityColor", txtWeeklyNoActivityColor.Text)
                    cmd.Parameters.AddWithValue("@WeeklyLowActivity", txtWeeklyLow.Text)
                    cmd.Parameters.AddWithValue("@WeeklyLowActivityLabel", txtWeeklyLowLabel.Text)
                    cmd.Parameters.AddWithValue("@WeeklyLowActivityColor", txtWeeklyLowColor.Text)
                    cmd.Parameters.AddWithValue("@WeeklyMediumActivity", txtWeeklyMedium.Text)
                    cmd.Parameters.AddWithValue("@WeeklyMediumActivityLabel", txtWeeklyMediumLabel.Text)
                    cmd.Parameters.AddWithValue("@WeeklyMediumActivityColor", txtWeeklyMediumColor.Text)
                    cmd.Parameters.AddWithValue("@WeeklyHighActivity", txtWeeklyHigh.Text)
                    cmd.Parameters.AddWithValue("@WeeklyHighActivityLabel", txtWeeklyHighLabel.Text)
                    cmd.Parameters.AddWithValue("@WeeklyHighActivityColor", txtWeeklyHighColor.Text)

                    cmd.Parameters.AddWithValue("@MonthlyNoActivity", txtMonthlyNoActivity.Text)
                    cmd.Parameters.AddWithValue("@MonthlyNoActivityLabel", txtMonthlyNoActivityLabel.Text)
                    cmd.Parameters.AddWithValue("@MonthlyNoActivityColor", txtMonthlyNoActivityColor.Text)
                    cmd.Parameters.AddWithValue("@MonthlyLowActivity", txtMonthlyLow.Text)
                    cmd.Parameters.AddWithValue("@MonthlyLowActivityLabel", txtMonthlyLowLabel.Text)
                    cmd.Parameters.AddWithValue("@MonthlyLowActivityColor", txtMonthlyLowColor.Text)
                    cmd.Parameters.AddWithValue("@MonthlyMediumActivity", txtMonthlyMedium.Text)
                    cmd.Parameters.AddWithValue("@MonthlyMediumActivityLabel", txtMonthlyMediumLabel.Text)
                    cmd.Parameters.AddWithValue("@MonthlyMediumActivityColor", txtMonthlyMediumColor.Text)
                    cmd.Parameters.AddWithValue("@MonthlyHighActivity", txtMonthlyHigh.Text)
                    cmd.Parameters.AddWithValue("@MonthlyHighActivityLabel", txtMonthlyHighLabel.Text)
                    cmd.Parameters.AddWithValue("@MonthlyHighActivityColor", txtMonthlyHighColor.Text)

                    cmd.Parameters.AddWithValue("@YearlyNoActivity", txtYearlyNoActivity.Text)
                    cmd.Parameters.AddWithValue("@YearlyNoActivityLabel", txtYearlyNoActivityLabel.Text)
                    cmd.Parameters.AddWithValue("@YearlyNoActivityColor", txtYearlyNoActivityColor.Text)
                    cmd.Parameters.AddWithValue("@YearlyLowActivity", txtYearlyLow.Text)
                    cmd.Parameters.AddWithValue("@YearlyLowActivityLabel", txtYearlyLowLabel.Text)
                    cmd.Parameters.AddWithValue("@YearlyLowActivityColor", txtYearlyLowColor.Text)
                    cmd.Parameters.AddWithValue("@YearlyMediumActivity", txtYearlyMedium.Text)
                    cmd.Parameters.AddWithValue("@YearlyMediumActivityLabel", txtYearlyMediumLabel.Text)
                    cmd.Parameters.AddWithValue("@YearlyMediumActivityColor", txtYearlyMediumColor.Text)
                    cmd.Parameters.AddWithValue("@YearlyHighActivity", txtYearlyHigh.Text)
                    cmd.Parameters.AddWithValue("@YearlyHighActivityLabel", txtYearlyHighLabel.Text)
                    cmd.Parameters.AddWithValue("@YearlyHighActivityColor", txtYearlyHighColor.Text)

                    con.Open()
                    cmd.ExecuteNonQuery()
                    con.Close()

                End Using
            End Using

        End If

        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "ShowStatus", "hideFloorPlanThresholdpopup();", True)


    End Sub

    Public Function GetFloorPlanThreshold(ByVal FloorPlanID As Integer) As tblFloorPlanThreshold
        Dim item As New tblFloorPlanThreshold()
        Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        Using con As New MySqlConnection(constr)
            Using cmd As New MySqlCommand()
                cmd.CommandType = CommandType.Text
                Dim insQuery As String = "SELECT * FROM tblfloorplanthreshold WHERE FloorPlanID = " & FloorPlanID & ""
                cmd.CommandText = insQuery
                cmd.Connection = con
                con.Open()
                Dim reader As MySqlDataReader = cmd.ExecuteReader()
                Do While reader.Read()
                    If Not Convert.IsDBNull(reader("RcNo")) Then
                        item.RcNo = CType(reader("RcNo"), Integer)
                    End If

                    'Daily

                    If Not Convert.IsDBNull(reader("DailyNoActivity")) Then
                        item.DailyNoActivity = CType(reader("DailyNoActivity"), Integer)
                    Else
                        item.DailyNoActivity = 0
                    End If
                    If Not Convert.IsDBNull(reader("DailyNoActivityLabel")) Then
                        item.DailyNoActivityLabel = CType(reader("DailyNoActivityLabel"), String)
                    Else
                        item.DailyNoActivityLabel = ""
                    End If
                    If Not Convert.IsDBNull(reader("DailyNoActivityColor")) Then
                        item.DailyNoActivityColor = CType(reader("DailyNoActivityColor"), String)
                    Else
                        item.DailyNoActivityColor = ""
                    End If
                    If Not Convert.IsDBNull(reader("DailyLowActivity")) Then
                        item.DailyLowActivity = CType(reader("DailyLowActivity"), Integer)
                    Else
                        item.DailyLowActivity = 0
                    End If
                    If Not Convert.IsDBNull(reader("DailyLowActivityLabel")) Then
                        item.DailyLowActivityLabel = CType(reader("DailyLowActivityLabel"), String)
                    Else
                        item.DailyLowActivityLabel = ""
                    End If
                    If Not Convert.IsDBNull(reader("DailyLowActivityColor")) Then
                        item.DailyLowActivityColor = CType(reader("DailyLowActivityColor"), String)
                    Else
                        item.DailyLowActivityColor = ""
                    End If
                    If Not Convert.IsDBNull(reader("DailyMediumActivity")) Then
                        item.DailyMediumActivity = CType(reader("DailyMediumActivity"), Integer)
                    Else
                        item.DailyMediumActivity = 0
                    End If
                    If Not Convert.IsDBNull(reader("DailyMediumActivityLabel")) Then
                        item.DailyMediumActivityLabel = CType(reader("DailyMediumActivityLabel"), String)
                    Else
                        item.DailyMediumActivityLabel = ""
                    End If
                    If Not Convert.IsDBNull(reader("DailyMediumActivityColor")) Then
                        item.DailyMediumActivityColor = CType(reader("DailyMediumActivityColor"), String)
                    Else
                        item.DailyMediumActivityColor = ""
                    End If
                    If Not Convert.IsDBNull(reader("DailyHighActivity")) Then
                        item.DailyHighActivity = CType(reader("DailyHighActivity"), Integer)
                    Else
                        item.DailyHighActivity = 0
                    End If
                    If Not Convert.IsDBNull(reader("DailyHighActivityLabel")) Then
                        item.DailyHighActivityLabel = CType(reader("DailyHighActivityLabel"), String)
                    Else
                        item.DailyHighActivityLabel = ""
                    End If
                    If Not Convert.IsDBNull(reader("DailyHighActivityColor")) Then
                        item.DailyHighActivityColor = CType(reader("DailyHighActivityColor"), String)
                    Else
                        item.DailyHighActivityColor = ""
                    End If

                    'Weekly

                    If Not Convert.IsDBNull(reader("WeeklyNoActivity")) Then
                        item.WeeklyNoActivity = CType(reader("WeeklyNoActivity"), Integer)
                    Else
                        item.WeeklyNoActivity = 0
                    End If
                    If Not Convert.IsDBNull(reader("WeeklyNoActivityLabel")) Then
                        item.WeeklyNoActivityLabel = CType(reader("WeeklyNoActivityLabel"), String)
                    Else
                        item.WeeklyNoActivityLabel = ""
                    End If
                    If Not Convert.IsDBNull(reader("WeeklyNoActivityColor")) Then
                        item.WeeklyNoActivityColor = CType(reader("WeeklyNoActivityColor"), String)
                    Else
                        item.WeeklyNoActivityColor = ""
                    End If
                    If Not Convert.IsDBNull(reader("WeeklyLowActivity")) Then
                        item.WeeklyLowActivity = CType(reader("WeeklyLowActivity"), Integer)
                    Else
                        item.WeeklyLowActivity = 0
                    End If
                    If Not Convert.IsDBNull(reader("WeeklyLowActivityLabel")) Then
                        item.WeeklyLowActivityLabel = CType(reader("WeeklyLowActivityLabel"), String)
                    Else
                        item.WeeklyLowActivityLabel = ""
                    End If
                    If Not Convert.IsDBNull(reader("WeeklyLowActivityColor")) Then
                        item.WeeklyLowActivityColor = CType(reader("WeeklyLowActivityColor"), String)
                    Else
                        item.WeeklyLowActivityColor = ""
                    End If
                    If Not Convert.IsDBNull(reader("WeeklyMediumActivity")) Then
                        item.WeeklyMediumActivity = CType(reader("WeeklyMediumActivity"), Integer)
                    Else
                        item.WeeklyMediumActivity = 0
                    End If
                    If Not Convert.IsDBNull(reader("WeeklyMediumActivityLabel")) Then
                        item.WeeklyMediumActivityLabel = CType(reader("WeeklyMediumActivityLabel"), String)
                    Else
                        item.WeeklyMediumActivityLabel = ""
                    End If
                    If Not Convert.IsDBNull(reader("WeeklyMediumActivityColor")) Then
                        item.WeeklyMediumActivityColor = CType(reader("WeeklyMediumActivityColor"), String)
                    Else
                        item.WeeklyMediumActivityColor = ""
                    End If
                    If Not Convert.IsDBNull(reader("WeeklyHighActivity")) Then
                        item.WeeklyHighActivity = CType(reader("WeeklyHighActivity"), Integer)
                    Else
                        item.WeeklyHighActivity = 0
                    End If
                    If Not Convert.IsDBNull(reader("WeeklyHighActivityLabel")) Then
                        item.WeeklyHighActivityLabel = CType(reader("WeeklyHighActivityLabel"), String)
                    Else
                        item.WeeklyHighActivityLabel = ""
                    End If
                    If Not Convert.IsDBNull(reader("WeeklyHighActivityColor")) Then
                        item.WeeklyHighActivityColor = CType(reader("WeeklyHighActivityColor"), String)
                    Else
                        item.WeeklyHighActivityColor = ""
                    End If

                    'Monthly

                    If Not Convert.IsDBNull(reader("MonthlyNoActivity")) Then
                        item.MonthlyNoActivity = CType(reader("MonthlyNoActivity"), Integer)
                    Else
                        item.MonthlyNoActivity = 0
                    End If
                    If Not Convert.IsDBNull(reader("MonthlyNoActivityLabel")) Then
                        item.MonthlyNoActivityLabel = CType(reader("MonthlyNoActivityLabel"), String)
                    Else
                        item.MonthlyNoActivityLabel = ""
                    End If
                    If Not Convert.IsDBNull(reader("MonthlyNoActivityColor")) Then
                        item.MonthlyNoActivityColor = CType(reader("MonthlyNoActivityColor"), String)
                    Else
                        item.MonthlyNoActivityColor = ""
                    End If
                    If Not Convert.IsDBNull(reader("MonthlyLowActivity")) Then
                        item.MonthlyLowActivity = CType(reader("MonthlyLowActivity"), Integer)
                    Else
                        item.MonthlyLowActivity = 0
                    End If
                    If Not Convert.IsDBNull(reader("MonthlyLowActivityLabel")) Then
                        item.MonthlyLowActivityLabel = CType(reader("MonthlyLowActivityLabel"), String)
                    Else
                        item.MonthlyLowActivityLabel = ""
                    End If
                    If Not Convert.IsDBNull(reader("MonthlyLowActivityColor")) Then
                        item.MonthlyLowActivityColor = CType(reader("MonthlyLowActivityColor"), String)
                    Else
                        item.MonthlyLowActivityColor = ""
                    End If
                    If Not Convert.IsDBNull(reader("MonthlyMediumActivity")) Then
                        item.MonthlyMediumActivity = CType(reader("MonthlyMediumActivity"), Integer)
                    Else
                        item.MonthlyMediumActivity = 0
                    End If
                    If Not Convert.IsDBNull(reader("MonthlyMediumActivityLabel")) Then
                        item.MonthlyMediumActivityLabel = CType(reader("MonthlyMediumActivityLabel"), String)
                    Else
                        item.MonthlyMediumActivityLabel = ""
                    End If
                    If Not Convert.IsDBNull(reader("MonthlyMediumActivityColor")) Then
                        item.MonthlyMediumActivityColor = CType(reader("MonthlyMediumActivityColor"), String)
                    Else
                        item.MonthlyMediumActivityColor = ""
                    End If
                    If Not Convert.IsDBNull(reader("MonthlyHighActivity")) Then
                        item.MonthlyHighActivity = CType(reader("MonthlyHighActivity"), Integer)
                    Else
                        item.MonthlyHighActivity = 0
                    End If
                    If Not Convert.IsDBNull(reader("MonthlyHighActivityLabel")) Then
                        item.MonthlyHighActivityLabel = CType(reader("MonthlyHighActivityLabel"), String)
                    Else
                        item.MonthlyHighActivityLabel = ""
                    End If
                    If Not Convert.IsDBNull(reader("MonthlyHighActivityColor")) Then
                        item.MonthlyHighActivityColor = CType(reader("MonthlyHighActivityColor"), String)
                    Else
                        item.MonthlyHighActivityColor = ""
                    End If

                    'Yearly

                    If Not Convert.IsDBNull(reader("YearlyNoActivity")) Then
                        item.YearlyNoActivity = CType(reader("YearlyNoActivity"), Integer)
                    Else
                        item.YearlyNoActivity = 0
                    End If
                    If Not Convert.IsDBNull(reader("YearlyNoActivityLabel")) Then
                        item.YearlyNoActivityLabel = CType(reader("YearlyNoActivityLabel"), String)
                    Else
                        item.YearlyNoActivityLabel = ""
                    End If
                    If Not Convert.IsDBNull(reader("YearlyNoActivityColor")) Then
                        item.YearlyNoActivityColor = CType(reader("YearlyNoActivityColor"), String)
                    Else
                        item.YearlyNoActivityColor = ""
                    End If
                    If Not Convert.IsDBNull(reader("YearlyLowActivity")) Then
                        item.YearlyLowActivity = CType(reader("YearlyLowActivity"), Integer)
                    Else
                        item.YearlyLowActivity = 0
                    End If
                    If Not Convert.IsDBNull(reader("YearlyLowActivityLabel")) Then
                        item.YearlyLowActivityLabel = CType(reader("YearlyLowActivityLabel"), String)
                    Else
                        item.YearlyLowActivityLabel = ""
                    End If
                    If Not Convert.IsDBNull(reader("YearlyLowActivityColor")) Then
                        item.YearlyLowActivityColor = CType(reader("YearlyLowActivityColor"), String)
                    Else
                        item.YearlyLowActivityColor = ""
                    End If
                    If Not Convert.IsDBNull(reader("YearlyMediumActivity")) Then
                        item.YearlyMediumActivity = CType(reader("YearlyMediumActivity"), Integer)
                    Else
                        item.YearlyMediumActivity = 0
                    End If
                    If Not Convert.IsDBNull(reader("YearlyMediumActivityLabel")) Then
                        item.YearlyMediumActivityLabel = CType(reader("YearlyMediumActivityLabel"), String)
                    Else
                        item.YearlyMediumActivityLabel = ""
                    End If
                    If Not Convert.IsDBNull(reader("YearlyMediumActivityColor")) Then
                        item.YearlyMediumActivityColor = CType(reader("YearlyMediumActivityColor"), String)
                    Else
                        item.YearlyMediumActivityColor = ""
                    End If
                    If Not Convert.IsDBNull(reader("YearlyHighActivity")) Then
                        item.YearlyHighActivity = CType(reader("YearlyHighActivity"), Integer)
                    Else
                        item.YearlyHighActivity = 0
                    End If
                    If Not Convert.IsDBNull(reader("YearlyHighActivityLabel")) Then
                        item.YearlyHighActivityLabel = CType(reader("YearlyHighActivityLabel"), String)
                    Else
                        item.YearlyHighActivityLabel = ""
                    End If
                    If Not Convert.IsDBNull(reader("YearlyHighActivityColor")) Then
                        item.YearlyHighActivityColor = CType(reader("YearlyHighActivityColor"), String)
                    Else
                        item.YearlyHighActivityColor = ""
                    End If
                Loop
                con.Close()
            End Using
        End Using

        If item.RcNo = 0 Then

            Using con As New MySqlConnection(constr)
                Using cmd As New MySqlCommand()
                    cmd.CommandType = CommandType.Text
                    Dim insQuery As String = "SELECT * FROM tblfloorplanthreshold WHERE FloorPlanID IS NULL LIMIT 1"
                    cmd.CommandText = insQuery
                    cmd.Connection = con
                    con.Open()
                    Dim reader As MySqlDataReader = cmd.ExecuteReader()
                    Do While reader.Read()

                        'Daily

                        If Not Convert.IsDBNull(reader("DailyNoActivity")) Then
                            item.DailyNoActivity = CType(reader("DailyNoActivity"), Integer)
                        Else
                            item.DailyNoActivity = 0
                        End If
                        If Not Convert.IsDBNull(reader("DailyNoActivityLabel")) Then
                            item.DailyNoActivityLabel = CType(reader("DailyNoActivityLabel"), String)
                        Else
                            item.DailyNoActivityLabel = ""
                        End If
                        If Not Convert.IsDBNull(reader("DailyNoActivityColor")) Then
                            item.DailyNoActivityColor = CType(reader("DailyNoActivityColor"), String)
                        Else
                            item.DailyNoActivityColor = ""
                        End If
                        If Not Convert.IsDBNull(reader("DailyLowActivity")) Then
                            item.DailyLowActivity = CType(reader("DailyLowActivity"), Integer)
                        Else
                            item.DailyLowActivity = 0
                        End If
                        If Not Convert.IsDBNull(reader("DailyLowActivityLabel")) Then
                            item.DailyLowActivityLabel = CType(reader("DailyLowActivityLabel"), String)
                        Else
                            item.DailyLowActivityLabel = ""
                        End If
                        If Not Convert.IsDBNull(reader("DailyLowActivityColor")) Then
                            item.DailyLowActivityColor = CType(reader("DailyLowActivityColor"), String)
                        Else
                            item.DailyLowActivityColor = ""
                        End If
                        If Not Convert.IsDBNull(reader("DailyMediumActivity")) Then
                            item.DailyMediumActivity = CType(reader("DailyMediumActivity"), Integer)
                        Else
                            item.DailyMediumActivity = 0
                        End If
                        If Not Convert.IsDBNull(reader("DailyMediumActivityLabel")) Then
                            item.DailyMediumActivityLabel = CType(reader("DailyMediumActivityLabel"), String)
                        Else
                            item.DailyMediumActivityLabel = ""
                        End If
                        If Not Convert.IsDBNull(reader("DailyMediumActivityColor")) Then
                            item.DailyMediumActivityColor = CType(reader("DailyMediumActivityColor"), String)
                        Else
                            item.DailyMediumActivityColor = ""
                        End If
                        If Not Convert.IsDBNull(reader("DailyHighActivity")) Then
                            item.DailyHighActivity = CType(reader("DailyHighActivity"), Integer)
                        Else
                            item.DailyHighActivity = 0
                        End If
                        If Not Convert.IsDBNull(reader("DailyHighActivityLabel")) Then
                            item.DailyHighActivityLabel = CType(reader("DailyHighActivityLabel"), String)
                        Else
                            item.DailyHighActivityLabel = ""
                        End If
                        If Not Convert.IsDBNull(reader("DailyHighActivityColor")) Then
                            item.DailyHighActivityColor = CType(reader("DailyHighActivityColor"), String)
                        Else
                            item.DailyHighActivityColor = ""
                        End If

                        'Weekly

                        If Not Convert.IsDBNull(reader("WeeklyNoActivity")) Then
                            item.WeeklyNoActivity = CType(reader("WeeklyNoActivity"), Integer)
                        Else
                            item.WeeklyNoActivity = 0
                        End If
                        If Not Convert.IsDBNull(reader("WeeklyNoActivityLabel")) Then
                            item.WeeklyNoActivityLabel = CType(reader("WeeklyNoActivityLabel"), String)
                        Else
                            item.WeeklyNoActivityLabel = ""
                        End If
                        If Not Convert.IsDBNull(reader("WeeklyNoActivityColor")) Then
                            item.WeeklyNoActivityColor = CType(reader("WeeklyNoActivityColor"), String)
                        Else
                            item.WeeklyNoActivityColor = ""
                        End If
                        If Not Convert.IsDBNull(reader("WeeklyLowActivity")) Then
                            item.WeeklyLowActivity = CType(reader("WeeklyLowActivity"), Integer)
                        Else
                            item.WeeklyLowActivity = 0
                        End If
                        If Not Convert.IsDBNull(reader("WeeklyLowActivityLabel")) Then
                            item.WeeklyLowActivityLabel = CType(reader("WeeklyLowActivityLabel"), String)
                        Else
                            item.WeeklyLowActivityLabel = ""
                        End If
                        If Not Convert.IsDBNull(reader("WeeklyLowActivityColor")) Then
                            item.WeeklyLowActivityColor = CType(reader("WeeklyLowActivityColor"), String)
                        Else
                            item.WeeklyLowActivityColor = ""
                        End If
                        If Not Convert.IsDBNull(reader("WeeklyMediumActivity")) Then
                            item.WeeklyMediumActivity = CType(reader("WeeklyMediumActivity"), Integer)
                        Else
                            item.WeeklyMediumActivity = 0
                        End If
                        If Not Convert.IsDBNull(reader("WeeklyMediumActivityLabel")) Then
                            item.WeeklyMediumActivityLabel = CType(reader("WeeklyMediumActivityLabel"), String)
                        Else
                            item.WeeklyMediumActivityLabel = ""
                        End If
                        If Not Convert.IsDBNull(reader("WeeklyMediumActivityColor")) Then
                            item.WeeklyMediumActivityColor = CType(reader("WeeklyMediumActivityColor"), String)
                        Else
                            item.WeeklyMediumActivityColor = ""
                        End If
                        If Not Convert.IsDBNull(reader("WeeklyHighActivity")) Then
                            item.WeeklyHighActivity = CType(reader("WeeklyHighActivity"), Integer)
                        Else
                            item.WeeklyHighActivity = 0
                        End If
                        If Not Convert.IsDBNull(reader("WeeklyHighActivityLabel")) Then
                            item.WeeklyHighActivityLabel = CType(reader("WeeklyHighActivityLabel"), String)
                        Else
                            item.WeeklyHighActivityLabel = ""
                        End If
                        If Not Convert.IsDBNull(reader("WeeklyHighActivityColor")) Then
                            item.WeeklyHighActivityColor = CType(reader("WeeklyHighActivityColor"), String)
                        Else
                            item.WeeklyHighActivityColor = ""
                        End If

                        'Monthly

                        If Not Convert.IsDBNull(reader("MonthlyNoActivity")) Then
                            item.MonthlyNoActivity = CType(reader("MonthlyNoActivity"), Integer)
                        Else
                            item.MonthlyNoActivity = 0
                        End If
                        If Not Convert.IsDBNull(reader("MonthlyNoActivityLabel")) Then
                            item.MonthlyNoActivityLabel = CType(reader("MonthlyNoActivityLabel"), String)
                        Else
                            item.MonthlyNoActivityLabel = ""
                        End If
                        If Not Convert.IsDBNull(reader("MonthlyNoActivityColor")) Then
                            item.MonthlyNoActivityColor = CType(reader("MonthlyNoActivityColor"), String)
                        Else
                            item.MonthlyNoActivityColor = ""
                        End If
                        If Not Convert.IsDBNull(reader("MonthlyLowActivity")) Then
                            item.MonthlyLowActivity = CType(reader("MonthlyLowActivity"), Integer)
                        Else
                            item.MonthlyLowActivity = 0
                        End If
                        If Not Convert.IsDBNull(reader("MonthlyLowActivityLabel")) Then
                            item.MonthlyLowActivityLabel = CType(reader("MonthlyLowActivityLabel"), String)
                        Else
                            item.MonthlyLowActivityLabel = ""
                        End If
                        If Not Convert.IsDBNull(reader("MonthlyLowActivityColor")) Then
                            item.MonthlyLowActivityColor = CType(reader("MonthlyLowActivityColor"), String)
                        Else
                            item.MonthlyLowActivityColor = ""
                        End If
                        If Not Convert.IsDBNull(reader("MonthlyMediumActivity")) Then
                            item.MonthlyMediumActivity = CType(reader("MonthlyMediumActivity"), Integer)
                        Else
                            item.MonthlyMediumActivity = 0
                        End If
                        If Not Convert.IsDBNull(reader("MonthlyMediumActivityLabel")) Then
                            item.MonthlyMediumActivityLabel = CType(reader("MonthlyMediumActivityLabel"), String)
                        Else
                            item.MonthlyMediumActivityLabel = ""
                        End If
                        If Not Convert.IsDBNull(reader("MonthlyMediumActivityColor")) Then
                            item.MonthlyMediumActivityColor = CType(reader("MonthlyMediumActivityColor"), String)
                        Else
                            item.MonthlyMediumActivityColor = ""
                        End If
                        If Not Convert.IsDBNull(reader("MonthlyHighActivity")) Then
                            item.MonthlyHighActivity = CType(reader("MonthlyHighActivity"), Integer)
                        Else
                            item.MonthlyHighActivity = 0
                        End If
                        If Not Convert.IsDBNull(reader("MonthlyHighActivityLabel")) Then
                            item.MonthlyHighActivityLabel = CType(reader("MonthlyHighActivityLabel"), String)
                        Else
                            item.MonthlyHighActivityLabel = ""
                        End If
                        If Not Convert.IsDBNull(reader("MonthlyHighActivityColor")) Then
                            item.MonthlyHighActivityColor = CType(reader("MonthlyHighActivityColor"), String)
                        Else
                            item.MonthlyHighActivityColor = ""
                        End If

                        'Yearly

                        If Not Convert.IsDBNull(reader("YearlyNoActivity")) Then
                            item.YearlyNoActivity = CType(reader("YearlyNoActivity"), Integer)
                        Else
                            item.YearlyNoActivity = 0
                        End If
                        If Not Convert.IsDBNull(reader("YearlyNoActivityLabel")) Then
                            item.YearlyNoActivityLabel = CType(reader("YearlyNoActivityLabel"), String)
                        Else
                            item.YearlyNoActivityLabel = ""
                        End If
                        If Not Convert.IsDBNull(reader("YearlyNoActivityColor")) Then
                            item.YearlyNoActivityColor = CType(reader("YearlyNoActivityColor"), String)
                        Else
                            item.YearlyNoActivityColor = ""
                        End If
                        If Not Convert.IsDBNull(reader("YearlyLowActivity")) Then
                            item.YearlyLowActivity = CType(reader("YearlyLowActivity"), Integer)
                        Else
                            item.YearlyLowActivity = 0
                        End If
                        If Not Convert.IsDBNull(reader("YearlyLowActivityLabel")) Then
                            item.YearlyLowActivityLabel = CType(reader("YearlyLowActivityLabel"), String)
                        Else
                            item.YearlyLowActivityLabel = ""
                        End If
                        If Not Convert.IsDBNull(reader("YearlyLowActivityColor")) Then
                            item.YearlyLowActivityColor = CType(reader("YearlyLowActivityColor"), String)
                        Else
                            item.YearlyLowActivityColor = ""
                        End If
                        If Not Convert.IsDBNull(reader("YearlyMediumActivity")) Then
                            item.YearlyMediumActivity = CType(reader("YearlyMediumActivity"), Integer)
                        Else
                            item.YearlyMediumActivity = 0
                        End If
                        If Not Convert.IsDBNull(reader("YearlyMediumActivityLabel")) Then
                            item.YearlyMediumActivityLabel = CType(reader("YearlyMediumActivityLabel"), String)
                        Else
                            item.YearlyMediumActivityLabel = ""
                        End If
                        If Not Convert.IsDBNull(reader("YearlyMediumActivityColor")) Then
                            item.YearlyMediumActivityColor = CType(reader("YearlyMediumActivityColor"), String)
                        Else
                            item.YearlyMediumActivityColor = ""
                        End If
                        If Not Convert.IsDBNull(reader("YearlyHighActivity")) Then
                            item.YearlyHighActivity = CType(reader("YearlyHighActivity"), Integer)
                        Else
                            item.YearlyHighActivity = 0
                        End If
                        If Not Convert.IsDBNull(reader("YearlyHighActivityLabel")) Then
                            item.YearlyHighActivityLabel = CType(reader("YearlyHighActivityLabel"), String)
                        Else
                            item.YearlyHighActivityLabel = ""
                        End If
                        If Not Convert.IsDBNull(reader("YearlyHighActivityColor")) Then
                            item.YearlyHighActivityColor = CType(reader("YearlyHighActivityColor"), String)
                        Else
                            item.YearlyHighActivityColor = ""
                        End If


                    Loop
                    con.Close()
                End Using
            End Using

        End If

        Return item
    End Function

    Public Sub ClearThresholdValues()

        HiddenFloorPlanThresholdRCNo.Value = 0
        lblFloorPlantext.Text = ddlfloorPlanList.SelectedItem.Text
        'Daily
        txtDailyNoActivityLabel.Text = "No Activity"
        txtDailyLowLabel.Text = "Low Activity"
        txtDailyMediumLabel.Text = "Medium Activity"
        txtDailyHighLabel.Text = "High Activity"

        txtDailyNoActivity.Text = ""
        txtDailyLow.Text = ""
        txtDailyMedium.Text = ""
        txtDailyHigh.Text = ""

        txtDailyNoActivityColor.Text = ""
        txtDailyLowColor.Text = ""
        txtDailyMediumColor.Text = ""
        txtDailyHighColor.Text = ""

        'Weekly
        txtWeeklyNoActivityLabel.Text = "No Activity"
        txtWeeklyLowLabel.Text = "Low Activity"
        txtWeeklyMediumLabel.Text = "Medium Activity"
        txtWeeklyHighLabel.Text = "High Activity"

        txtWeeklyNoActivity.Text = ""
        txtWeeklyLow.Text = ""
        txtWeeklyMedium.Text = ""
        txtWeeklyHigh.Text = ""

        txtWeeklyNoActivityColor.Text = ""
        txtWeeklyLowColor.Text = ""
        txtWeeklyMediumColor.Text = ""
        txtWeeklyHighColor.Text = ""

        'Monthly
        txtMonthlyNoActivityLabel.Text = "No Activity"
        txtMonthlyLowLabel.Text = "Low Activity"
        txtMonthlyMediumLabel.Text = "Medium Activity"
        txtMonthlyHighLabel.Text = "High Activity"

        txtMonthlyNoActivity.Text = ""
        txtMonthlyLow.Text = ""
        txtMonthlyMedium.Text = ""
        txtMonthlyHigh.Text = ""

        txtMonthlyNoActivityColor.Text = ""
        txtMonthlyLowColor.Text = ""
        txtMonthlyMediumColor.Text = ""
        txtMonthlyHighColor.Text = ""

        'Yearly
        txtYearlyNoActivityLabel.Text = "No Activity"
        txtYearlyLowLabel.Text = "Low Activity"
        txtYearlyMediumLabel.Text = "Medium Activity"
        txtYearlyHighLabel.Text = "High Activity"

        txtYearlyNoActivity.Text = ""
        txtYearlyLow.Text = ""
        txtYearlyMedium.Text = ""
        txtYearlyHigh.Text = ""

        txtYearlyNoActivityColor.Text = ""
        txtYearlyLowColor.Text = ""
        txtYearlyMediumColor.Text = ""
        txtYearlyHighColor.Text = ""

    End Sub

    <System.Web.Services.WebMethod()>
    Public Shared Function getSearchData(ByVal searchText As String, ByVal locationid As String) As String
        Dim list As List(Of SearchClass) = New List(Of SearchClass)

        If Not String.IsNullOrEmpty(searchText) Then

            If searchText.Contains(ControlChars.Quote) Then
                searchText = searchText.Replace(ControlChars.Quote, "'")
            End If

            Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            Using con As New MySqlConnection(constr)
                Using cmd As New MySqlCommand()
                    cmd.CommandType = CommandType.Text
                    Dim insQuery As String = ""

                    'insQuery = "SELECT DISTINCT T.FloorPlanID AS FloorPlanID, T.FloorPlanName AS FloorPlanName,F.AliasName as AliasName,F.Description as Description,F.DevicePlacedDate as DevicePlacedDate,F.DeviceID as DeviceID," & _
                    '          "F.XCoordinate AS XCoordinate,F.YCoordinate as YCoordinate " & _
                    '          "FROM tblFloorPlanItems F LEFT JOIN tbldeviceevents E ON E.DeviceID = F.DeviceID INNER JOIN tblcustomerlocationfloorplan T ON T.floorPlanID = F.floorPlanID WHERE " & _
                    '          " T.LocationID = '" + locationid + "'"

                    insQuery = "SELECT DISTINCT T.FloorPlanID AS FloorPlanID, T.FloorPlanName AS FloorPlanName,F.AliasName as AliasName,F.Description as Description,F.DevicePlacedDate as DevicePlacedDate,F.DeviceID as DeviceID," & _
                              "F.XCoordinate AS XCoordinate,F.YCoordinate as YCoordinate " & _
                              "FROM tblFloorPlanItems F INNER JOIN tblcustomerlocationfloorplan T ON T.floorPlanID = F.floorPlanID WHERE " & _
                              " T.LocationID = '" + locationid + "'"


                    If Not String.IsNullOrEmpty(searchText) Then
                        insQuery += " AND ((F.DeviceID LIKE ""%" + searchText + "%"") OR (F.AliasName LIKE ""%" + searchText + "%"") OR (F.Description LIKE ""%" + searchText + "%"")  ) "
                        insQuery += " ORDER BY F.DevicePlacedDate DESC"
                    End If


                    cmd.CommandText = insQuery
                    cmd.Connection = con
                    con.Open()
                    Dim reader As MySqlDataReader = cmd.ExecuteReader()
                    Do While reader.Read()
                        Dim item As New SearchClass()

                        If Not IsDBNull(reader("DeviceID")) Then
                            item.SerialNo = CType(reader("DeviceID"), String)
                        Else
                            item.SerialNo = ""
                        End If

                        If Not IsDBNull(reader("DevicePlacedDate")) Then
                            item.PlacementDate = CType(reader("DevicePlacedDate"), DateTime)
                            item.PlacementDateText = item.PlacementDate.ToString("dd/MM/yyyy")
                        Else
                            item.PlacementDateText = ""
                        End If

                        If Not IsDBNull(reader("AliasName")) Then
                            item.AliasName = CType(reader("AliasName"), String)
                        Else
                            item.AliasName = ""
                        End If


                        If Not IsDBNull(reader("Description")) Then
                            item.Description = CType(reader("Description"), String)
                        Else
                            item.Description = ""
                        End If

                        If Not IsDBNull(reader("FloorPlanName")) Then
                            item.Location = CType(reader("FloorPlanName"), String)
                        Else
                            item.Location = ""
                        End If

                        If Not Convert.IsDBNull(reader("XCoordinate")) Then
                            item.XCoordinate = CType(reader("XCoordinate"), String)
                        End If

                        If Not Convert.IsDBNull(reader("YCoordinate")) Then
                            item.YCoordinate = CType(reader("YCoordinate"), String)
                        End If
                        If Not Convert.IsDBNull(reader("FloorPlanID")) Then
                            item.FloorPlanID = CType(reader("FloorPlanID"), Integer)
                        Else
                            item.FloorPlanID = 0
                        End If

                        list.Add(item)
                    Loop
                    con.Close()
                End Using
            End Using
        End If

        Dim sdata As String = ""

        If Not list Is Nothing And list.Count > 0 Then
            sdata = "["
            For Each item As SearchClass In list

                sdata += "{""SerialNo"":"""
                sdata += item.SerialNo
                sdata += """,""AliasName"":"""
                sdata += item.AliasName
                sdata += """,""Description"":"""
                sdata += RemoveSpecialCharacter(item.Description)
                sdata += """,""PlacementDateText"":"""
                sdata += item.PlacementDateText
                sdata += """,""Location"":"""
                sdata += item.Location
                sdata += """,""XCoordinate"":"""
                sdata += item.XCoordinate
                sdata += """,""YCoordinate"":"""
                sdata += item.YCoordinate
                sdata += """,""FloorPlanID"":"""
                sdata += item.FloorPlanID.ToString()
                sdata += """},"
            Next
            sdata = sdata.TrimEnd(",")
            sdata = sdata + "]"
        End If

        Return sdata
    End Function


End Class


Public Class FloorPlan
    Public Property floorID() As String
    Public Property floorName() As String
End Class

Public Class DeviceEvents1
    Public Property AccountID() As String
    Public Property LocationID() As String
    Public Property MasterID() As String
    Public Property Aliases() As String
    Public Property DeviceID() As String
    Public Property DeviceMode() As String
    Public Property DeviceType() As String
    Public Property DeviceName() As String
    Public Property FloorplanID() As Integer
    Public Property LocationComments() As String
    Public Property DeviceUrl() As String
    Public Property Description() As String
End Class

Public Class FloorPlanDevices

    Public Property RcNo() As Integer
    Public Property deviceid() As String
    Public Property devicename() As String
    Public Property devicetype() As String
    'Public Property name() As String
    Public Property lat() As String
    Public Property lng() As String
    Public Property position() As List(Of Object)
    Public Property DevicePlacedDateText() As String
    Public Property DevicePlacedDate() As DateTime
    Public Property AliasName() As String
    Public Property UserID() As String
    Public Property CreatedOn() As String
    Public Property CreatedBy() As String
    Public Property UpdatedOn() As String
    Public Property UpdatedBy() As String

    Public Property Description() As String
End Class
Public Class FloorPlanItems
    Public Property RcNo() As Integer
    Public Property DeviceID() As String
    Public Property DeviceName() As String
    Public Property DeviceType() As String
    Public Property DeviceUrl() As String
    Public Property AliasName() As String
    Public Property UserID() As String
    Public Property CreatedOn() As String
    Public Property CreatedBy() As String
    Public Property UpdatedOn() As String
    Public Property UpdatedBy() As String
    Public Property DeviceCount() As String
    'Public Property name() As String

    Public Property XCoordinate() As String
    Public Property YCoordinate() As String
    Public Property DevicePlacedDate() As DateTime
    Public Property Description() As String
    Public Property DevicePlacedDateText() As String
End Class
Public Class Position
    Public Property lat() As String
    Public Property lng() As String
End Class


Public Class FloorPlanCompanylocation
    Public Property AccountID() As String
    Public Property LocationID() As String
    Public Property ServiceName() As String
    Public Property Address1() As String

End Class
Public Class FloorPlanapiGetSites
    Public Property pageIndex() As Integer
    Public Property pageSize() As Integer
    Public Property totalCount() As Integer
    Public Property totalPages() As Integer
    Public Property indexFrom() As Integer
    Public Property items As List(Of Dictionary(Of String, Object))
    'Public Property items() As List(Of apiGetSiteItem)
    Public Property hasPreviousPage() As Boolean
    Public Property hasNextPage() As Boolean
End Class
Public Class FloorPlanapiGetSiteItem
    Public Property id() As String
    Public Property siteName() As String
    Public Property street() As String
    Public Property zipCodeId() As String
    Public Property cityId() As String
    Public Property branchId() As String
    Public Property customerId() As String
    Public Property externalSiteId() As String
    Public Property longitude() As String
    Public Property latitude() As String
End Class
Public Class FloorPlanapiGetDevicebySiteId
    Public Property pageIndex() As Integer
    Public Property pageSize() As Integer
    Public Property totalCount() As Integer
    Public Property totalPages() As Integer
    Public Property indexFrom() As Integer

End Class
Public Class FloorPlanapiGetDeviceBySiteIdItem
    Public Property id() As String
    Public Property DeviceName() As String
    Public Property serialNumber() As String
    Public Property parentDeviceId() As String
    Public Property deviceCollectionTypeId() As String
    Public Property measuringPointId() As String
    Public Property installedTime() As String
    Public Property longitude() As Decimal
    Public Property latitude() As Decimal
    Public Property branchId() As String
    Public Property created() As String
    Public Property createdBy() As String
    Public Property lastUpdated() As String
    Public Property lastUpdatedBy() As String
    Public Property isParent() As String
End Class

Public Class FloorPlanapiGetDeviceTypeByDeviceId
    Public Property id() As String
    Public Property deviceTypeName() As String
End Class

Public Class FloorPlanapiGetDeviceEventsByDeviceId
    Public Property deviceSerial() As String
    Public Property sensorType() As String
    Public Property unixTimestamp() As String
    Public Property measuringPointId() As String
    Public Property value() As Double

End Class
Public Class Floorplantbldeviceevents
    Public Property DeviceID As String
    Public Property dtDevicedate As DateTime
End Class

Public Class tblfloorplanItemsdevices
    Public Property DeviceID As String
    Public Property AliasName As String
    Public Property description As String
    Public Property FloorplanName As String
End Class
Public Class AliasNameExists
    Public Property LocationID() As String
    Public Property AliasName() As String
End Class
Public Class DevicetypesIcons
    Public Property description() As String
    Public Property base64string() As String
    Public Property bytes() As Byte()
End Class
Public Class apiGetSiteItem
    Public Property id As String
    Public Property siteName As String
    Public Property street As String
    Public Property zipCodeId As String
    Public Property cityId As String
    Public Property branchId As String
    Public Property customerId As String
    Public Property externalSiteId As String
    Public Property longitude As String
    Public Property latitude As String
End Class
Public Class apiGetDeviceBySiteIdItem
    Public Property id As String
    Public Property DeviceName As String
    Public Property serialNumber As String
    Public Property parentDeviceId As String
    Public Property deviceCollectionTypeId As String
    Public Property measuringPointId As String
    Public Property installedTime As String
    Public Property longitude As Decimal
    Public Property latitude As Decimal
    Public Property branchId As String
    Public Property created As String
    Public Property createdBy As String
    Public Property lastUpdated As String
    Public Property lastUpdatedBy As String
    Public Property isParent As String
    Public Property comment As String
End Class

Public Class tblFloorPlanThreshold

    Public Property RcNo() As Integer
    Public Property FloorPlanID() As Integer

    Public Property DailyNoActivity() As Integer
    Public Property DailyNoActivityLabel() As String
    Public Property DailyNoActivityColor() As String
    Public Property DailyLowActivity() As Integer
    Public Property DailyLowActivityLabel() As String
    Public Property DailyLowActivityColor() As String
    Public Property DailyMediumActivity() As Integer
    Public Property DailyMediumActivityLabel() As String
    Public Property DailyMediumActivityColor() As String
    Public Property DailyHighActivity() As Integer
    Public Property DailyHighActivityLabel() As String
    Public Property DailyHighActivityColor() As String


    Public Property WeeklyNoActivity() As Integer
    Public Property WeeklyNoActivityLabel() As String
    Public Property WeeklyNoActivityColor() As String
    Public Property WeeklyLowActivity() As Integer
    Public Property WeeklyLowActivityLabel() As String
    Public Property WeeklyLowActivityColor() As String
    Public Property WeeklyMediumActivity() As Integer
    Public Property WeeklyMediumActivityLabel() As String
    Public Property WeeklyMediumActivityColor() As String
    Public Property WeeklyHighActivity() As Integer
    Public Property WeeklyHighActivityLabel() As String
    Public Property WeeklyHighActivityColor() As String

    Public Property MonthlyNoActivity() As Integer
    Public Property MonthlyNoActivityLabel() As String
    Public Property MonthlyNoActivityColor() As String
    Public Property MonthlyLowActivity() As Integer
    Public Property MonthlyLowActivityLabel() As String
    Public Property MonthlyLowActivityColor() As String
    Public Property MonthlyMediumActivity() As Integer
    Public Property MonthlyMediumActivityLabel() As String
    Public Property MonthlyMediumActivityColor() As String
    Public Property MonthlyHighActivity() As Integer
    Public Property MonthlyHighActivityLabel() As String
    Public Property MonthlyHighActivityColor() As String

    Public Property YearlyNoActivity() As Integer
    Public Property YearlyNoActivityLabel() As String
    Public Property YearlyNoActivityColor() As String
    Public Property YearlyLowActivity() As Integer
    Public Property YearlyLowActivityLabel() As String
    Public Property YearlyLowActivityColor() As String
    Public Property YearlyMediumActivity() As Integer
    Public Property YearlyMediumActivityLabel() As String
    Public Property YearlyMediumActivityColor() As String
    Public Property YearlyHighActivity() As Integer
    Public Property YearlyHighActivityLabel() As String
    Public Property YearlyHighActivityColor() As String

End Class
<Serializable()> Public Class SearchClass
    Public Property AliasName() As String
    Public Property SerialNo() As String
    Public Property Description() As String
    Public Property Location() As String
    Public Property PlacementDate() As DateTime
    Public Property PlacementDateText() As String
    Public Property XCoordinate() As String
    Public Property YCoordinate() As String
    Public Property FloorPlanID() As Integer
End Class


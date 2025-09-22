Imports Microsoft.VisualBasic
Imports System
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Net
Imports System.Web.Script.Serialization
Imports System.Threading
Imports System.Threading.Tasks
Imports Microsoft.IdentityModel.Clients.ActiveDirectory
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports Newtonsoft.Json
Imports NPOI.XSSF.UserModel
Imports NPOI.SS.UserModel

Partial Class SiteAndDeviceSetup
    Inherits System.Web.UI.Page

    Private token As AuthenticationResult
    Private apiGetSitesPath As String = ConfigurationManager.AppSettings("apiGetSites")
    Private apiGetDevicesBySiteIDPath As String = ConfigurationManager.AppSettings("apiGetDevicesBySiteID")
    Public apiGetDeviceTypeByDeviceIDPath As String = ConfigurationManager.AppSettings("apiGetDeviceTypeByDeviceId")
    Private apiGetDeviceEventsByDeviceIdPath As String = ConfigurationManager.AppSettings("apiGetDeviceEventsByDeviceId")
    Private apiGetDeviceByDeviceIDPath As String = ConfigurationManager.AppSettings("apiGetDevicesByDeviceID")

    Private apiGetCustomersPath As String = ConfigurationManager.AppSettings("apiGetCustomers")
    Private apiGetZipCodesPath As String = ConfigurationManager.AppSettings("apiGetZipCodes")
    Private apiGetCitiesPath As String = ConfigurationManager.AppSettings("apiGetCities")
    Private apiGetBranchesPath As String = ConfigurationManager.AppSettings("apiGetBranches")
    Private apiGetNewSitesPath As String = ConfigurationManager.AppSettings("apiGetNewSites")

    Private apiCreateCustomersPath As String = ConfigurationManager.AppSettings("apiCreateCustomers")
    Private apiCreateZipCodesPath As String = ConfigurationManager.AppSettings("apiCreateZipCodes")
    Private apiCreateCitiesPath As String = ConfigurationManager.AppSettings("apiCreateCities")
    Private apiCreateSitesPath As String = ConfigurationManager.AppSettings("apiCreateSites")
    Private apiGetCountryPath As String = ConfigurationManager.AppSettings("apiGetCountry")

    Private countryID As String = ConfigurationManager.AppSettings("CountryID")
    Private country As String = ConfigurationManager.AppSettings("Country")
    Private regionID As String = ConfigurationManager.AppSettings("RegionID")


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindcustomerGrid()
            BindCoutryDropdown()
            BindCityDropdown()
            BindZipDropdown()
            BindBranchDropdown()
        End If

    End Sub

    Private Sub BindcustomerGrid()

        Dim searchvalue = txtSearchCustomer.Text
        If searchvalue Is Nothing Then
            searchvalue = ""
        End If

        Dim resultToken = GetToken()
        Dim tokenno As String
        tokenno = token.AccessToken
        If tokenno = "" Then
            Dim resultmessage = "Unauthorized. Access token is missing or invalid"
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "ShowStatus", "warningpopup('" & resultmessage & "');", True)
        End If

        'Customer
        Dim apiGetList As List(Of apiGetSiteItem) = New List(Of apiGetSiteItem)()

        Dim pageSize As String = ""
        Dim totalCount As String = ""
        Dim totalpages As String = ""

        Using client = New HttpClient()
            If Not String.IsNullOrWhiteSpace(token.AccessToken) Then
                client.DefaultRequestHeaders.Clear()
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings.[Get]("subscriptionId"))
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " & tokenno)
                client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
            End If

            'check whether customer Exists
            'Dim filter As String = "pageSize eq 40"
            'Dim apiGetCustomersAPIPath = apiGetCustomersPath & "?" & filter
            ''Dim apiGetCustomersAPIPath = apiGetSitesPath

            Dim apiGetCustomersAPIPath = apiGetCustomersPath & "?Filter=countryId eq '" & countryID & "'"

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
            Dim response As HttpResponseMessage = client.GetAsync(apiGetCustomersAPIPath).Result
            Dim responseString = response.Content.ReadAsStringAsync().Result

            Dim serializer As New JavaScriptSerializer

            If response.IsSuccessStatusCode Then
                serializer = New JavaScriptSerializer()
                Dim jObject = Newtonsoft.Json.Linq.JObject.Parse(responseString)
                Dim result1 As String
                result1 = jObject("items").ToString()
                'apiGetList = serializer.Deserialize(Of List(Of apiGetSiteItem))(result1)

                'If searchvalue <> "" Then
                '    apiGetList = apiGetList.Where(Function(x) x.customerName.Contains(searchvalue) Or x.externalCustomerId.Contains(searchvalue) Or x.countryId.Contains(searchvalue)).ToList()
                'End If

                'For Each item In apiGetList
                '    item.countryName = country
                'Next

                pageSize = jObject("pageSize").ToString()
                totalCount = jObject("totalCount").ToString()
                totalpages = jObject("totalPages").ToString()

                'If totalpages > 1 Then
                '    Dim remaingCount As String = totalCount - pageSize

                '    Dim apiGetListAgain As List(Of apiGetSiteItem) = New List(Of apiGetSiteItem)()
                '    serializer = New JavaScriptSerializer()
                '    Dim apiGetCustomersAPIPathForRemainingRecords = apiGetCustomersPath & "?PageIndex= 1&PageSize= " & remaingCount & "&Filter=countryId eq '" & countryID & "'"
                '    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
                '    Dim responseAgain As HttpResponseMessage = client.GetAsync(apiGetCustomersAPIPathForRemainingRecords).Result
                '    Dim responseStringAgain = responseAgain.Content.ReadAsStringAsync().Result
                '    Dim jObjectAgain = Newtonsoft.Json.Linq.JObject.Parse(responseStringAgain)
                '    Dim result1Again As String
                '    result1Again = jObjectAgain("items").ToString()
                '    apiGetListAgain = serializer.Deserialize(Of List(Of apiGetSiteItem))(result1Again)
                '    apiGetList.AddRange(apiGetListAgain)

                'End If

                'If (totalpages > 1) Then
                '    Dim apiGetListAgain As List(Of apiGetSiteItem) = New List(Of apiGetSiteItem)()
                '    Dim index As Integer = 1
                '    For index = 1 To (totalpages - 1)
                '        serializer = New JavaScriptSerializer()
                '        Dim apiGetCustomersAPIPathForRemainingRecords = apiGetCustomersPath & "?PageIndex= " & index & "&Filter=countryId eq '" & countryID & "'"
                '        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
                '        Dim responseAgain As HttpResponseMessage = client.GetAsync(apiGetCustomersAPIPathForRemainingRecords).Result
                '        Dim responseStringAgain = responseAgain.Content.ReadAsStringAsync().Result
                '        Dim jObjectAgain = Newtonsoft.Json.Linq.JObject.Parse(responseStringAgain)
                '        Dim result1Again As String
                '        result1Again = jObjectAgain("items").ToString()
                '        apiGetListAgain = serializer.Deserialize(Of List(Of apiGetSiteItem))(result1)
                '        'For Each item In apiGetList
                '        '    item.customerName = country
                '        'Next
                '        apiGetList.AddRange(apiGetListAgain)
                '    Next
                'End If

            End If
        End Using

        Using client = New HttpClient()
            If Not String.IsNullOrWhiteSpace(token.AccessToken) Then
                client.DefaultRequestHeaders.Clear()
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings.[Get]("subscriptionId"))
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " & tokenno)
                client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
            End If

            Dim apiGetCustomersAPIPathForRemainingRecords = apiGetCustomersPath & "?PageSize= " & totalCount & "&Filter=countryId eq '" & countryID & "'"
            'Dim apiGetCustomersAPIPath = apiGetCustomersPath & "?Filter=countryId eq '" & countryID & "'"

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
            Dim response As HttpResponseMessage = client.GetAsync(apiGetCustomersAPIPathForRemainingRecords).Result
            Dim responseString = response.Content.ReadAsStringAsync().Result

            Dim serializer As New JavaScriptSerializer

            If response.IsSuccessStatusCode Then
                serializer = New JavaScriptSerializer()
                Dim jObject = Newtonsoft.Json.Linq.JObject.Parse(responseString)
                Dim result1 As String
                result1 = jObject("items").ToString()
                apiGetList = serializer.Deserialize(Of List(Of apiGetSiteItem))(result1)

                If searchvalue <> "" Then
                    apiGetList = apiGetList.Where(Function(x) (x.customerName.ToLower().Contains(searchvalue.ToLower())) Or (x.externalCustomerId.Contains(searchvalue))).ToList()
                End If
            End If

        End Using


        If Not apiGetList Is Nothing And apiGetList.Count >= 0 Then

            For Each item In apiGetList
                item.countryName = country
            Next

            Session("ApiCustomerList") = apiGetList
            GridCustomer.DataSource = apiGetList
            GridCustomer.DataBind()
        End If

    End Sub


    Protected Sub btnAddCustomer_Click(sender As Object, e As EventArgs)

        txtCustomerID.Text = ""
        txtCustomerName.Text = ""
        'BindCoutryDropdown()
        ddlCountry.SelectedIndex = "0"

        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "ShowStatus", "showCustomerpopup();", True)

    End Sub


    Private Sub BindBranchDropdown()

        Dim resultToken = GetToken()
        Dim tokenno As String
        tokenno = token.AccessToken
        If tokenno = "" Then
            Dim resultmessage = "Unauthorized. Access token is missing or invalid"
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "ShowStatus", "warningpopup('" & resultmessage & "');", True)
        End If

        Dim apiGetBranchsList As List(Of BranchModel) = New List(Of BranchModel)()

        Using client = New HttpClient()
            If Not String.IsNullOrWhiteSpace(token.AccessToken) Then
                client.DefaultRequestHeaders.Clear()
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings.[Get]("ImportCustomerandSite"))
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " & tokenno)
                client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
            End If

            Dim apiGetBranchAPIPath = apiGetBranchesPath & "?Filter=regionId eq '" & regionID & "'"

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
            Dim BranchResponse As HttpResponseMessage = client.GetAsync(apiGetBranchAPIPath).Result
            Dim BranchresponseString = BranchResponse.Content.ReadAsStringAsync().Result

            Dim serializer As New JavaScriptSerializer

            If BranchResponse.IsSuccessStatusCode Then
                serializer = New JavaScriptSerializer()
                Dim jObject = Newtonsoft.Json.Linq.JObject.Parse(BranchresponseString)
                Dim result1 As String
                result1 = jObject("items").ToString()
                apiGetBranchsList = serializer.Deserialize(Of List(Of BranchModel))(result1)
            End If
        End Using

        If Not apiGetBranchsList Is Nothing And apiGetBranchsList.Count > 0 Then
            ddlBranch.DataSource = apiGetBranchsList
            ddlBranch.DataTextField = "branchName"
            ddlBranch.DataValueField = "id"
            ddlBranch.DataBind()
        End If

        ddlBranch.Items.Insert(0, New ListItem("--Please Select--", "0"))

    End Sub


    Private Sub BindCoutryDropdown()

        Dim resultToken = GetToken()
        Dim tokenno As String
        tokenno = token.AccessToken
        If tokenno = "" Then
            Dim resultmessage = "Unauthorized. Access token is missing or invalid"
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "ShowStatus", "warningpopup('" & resultmessage & "');", True)
        End If

        Dim apiGetCountryList As List(Of apiGetCountryList) = New List(Of apiGetCountryList)()

        Using client = New HttpClient()
            If Not String.IsNullOrWhiteSpace(token.AccessToken) Then
                client.DefaultRequestHeaders.Clear()
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings.[Get]("ImportCustomerandSite"))
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " & tokenno)
                client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
            End If

            Dim apiGetCountryAPIPath = apiGetCountryPath

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
            Dim countryresponse As HttpResponseMessage = client.GetAsync(apiGetCountryAPIPath).Result
            Dim countryresponseString = countryresponse.Content.ReadAsStringAsync().Result

            Dim serializer As New JavaScriptSerializer

            If countryresponse.IsSuccessStatusCode Then
                serializer = New JavaScriptSerializer()
                Dim jObject = Newtonsoft.Json.Linq.JObject.Parse(countryresponseString)
                Dim result1 As String
                result1 = jObject("items").ToString()
                apiGetCountryList = serializer.Deserialize(Of List(Of apiGetCountryList))(result1)
            End If
        End Using

        If Not apiGetCountryList Is Nothing And apiGetCountryList.Count > 0 Then
            ddlCountry.DataSource = apiGetCountryList
            ddlCountry.DataTextField = "countryName"
            ddlCountry.DataValueField = "id"
            ddlCountry.DataBind()
        End If
        ddlCountry.Items.Insert(0, New ListItem("--Please Select--", "0"))

    End Sub

    Private Sub BindZipDropdown()

        Dim resultToken = GetToken()
        Dim tokenno As String
        tokenno = token.AccessToken
        If tokenno = "" Then
            Dim resultmessage = "Unauthorized. Access token is missing or invalid"
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "ShowStatus", "warningpopup('" & resultmessage & "');", True)
        End If

        Dim apiGetZipcodesList As List(Of ZipcodesModel) = New List(Of ZipcodesModel)()

        Using client = New HttpClient()
            If Not String.IsNullOrWhiteSpace(token.AccessToken) Then
                client.DefaultRequestHeaders.Clear()
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings.[Get]("ImportCustomerandSite"))
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " & tokenno)
                client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
            End If

            Dim apiGetZipcodesAPIPath = apiGetZipCodesPath

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
            Dim zipCodeResponse As HttpResponseMessage = client.GetAsync(apiGetZipcodesAPIPath).Result
            Dim zipCoderesponseString = zipCodeResponse.Content.ReadAsStringAsync().Result

            Dim serializer As New JavaScriptSerializer

            If zipCodeResponse.IsSuccessStatusCode Then
                serializer = New JavaScriptSerializer()
                Dim jObject = Newtonsoft.Json.Linq.JObject.Parse(zipCoderesponseString)
                Dim result1 As String
                result1 = jObject("items").ToString()
                apiGetZipcodesList = serializer.Deserialize(Of List(Of ZipcodesModel))(result1)
            End If
        End Using

        If Not apiGetZipcodesList Is Nothing And apiGetZipcodesList.Count > 0 Then
            ddlZipCode.DataSource = apiGetZipcodesList
            ddlZipCode.DataTextField = "zipCodeNumber"
            ddlZipCode.DataValueField = "id"
            ddlZipCode.DataBind()
        End If

        ddlZipCode.Items.Insert(0, New ListItem("--Please Select--", "0"))

    End Sub


    Private Sub BindCityDropdown()

        Dim resultToken = GetToken()
        Dim tokenno As String
        tokenno = token.AccessToken
        If tokenno = "" Then
            Dim resultmessage = "Unauthorized. Access token is missing or invalid"
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "ShowStatus", "warningpopup('" & resultmessage & "');", True)
        End If

        Dim apiGetCityList As List(Of CitiesModel) = New List(Of CitiesModel)()

        Using client = New HttpClient()
            If Not String.IsNullOrWhiteSpace(token.AccessToken) Then
                client.DefaultRequestHeaders.Clear()
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings.[Get]("ImportCustomerandSite"))
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " & tokenno)
                client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
            End If

            Dim apiGetCityAPIPath = apiGetCitiesPath

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
            Dim cityResponse As HttpResponseMessage = client.GetAsync(apiGetCityAPIPath).Result
            Dim cityresponseString = cityResponse.Content.ReadAsStringAsync().Result

            Dim serializer As New JavaScriptSerializer

            If cityResponse.IsSuccessStatusCode Then
                serializer = New JavaScriptSerializer()
                Dim jObject = Newtonsoft.Json.Linq.JObject.Parse(cityresponseString)
                Dim result1 As String
                result1 = jObject("items").ToString()
                apiGetCityList = serializer.Deserialize(Of List(Of CitiesModel))(result1)
            End If
        End Using

        If Not apiGetCityList Is Nothing And apiGetCityList.Count > 0 Then
            ddlCity.DataSource = apiGetCityList
            ddlCity.DataTextField = "cityName"
            ddlCity.DataValueField = "id"
            ddlCity.DataBind()
        End If

        ddlCity.Items.Insert(0, New ListItem("--Please Select--", "0"))
    End Sub

    Protected Sub btnSaveCustomer_Click(sender As Object, e As EventArgs)

        Dim resultToken = GetToken()
        Dim tokenno As String
        tokenno = token.AccessToken
        If tokenno = "" Then
            Dim resultmessage = "Unauthorized. Access token is missing or invalid"
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "ShowStatus", "warningpopup('" & resultmessage & "');", True)
        End If

        Using client = New HttpClient()
            If Not String.IsNullOrWhiteSpace(token.AccessToken) Then
                client.DefaultRequestHeaders.Clear()
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings.[Get]("ImportCustomerandSite"))
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " & tokenno)
                client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
            End If
            Dim apiPOSTCustomersAPIPath = apiCreateCustomersPath

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
            Dim customermodel As New CustomersModel

            If txtCustomerName.Text.Contains(",") Then
                txtCustomerName.Text = txtCustomerName.Text.TrimEnd(",")
            End If
            If txtCustomerID.Text.Contains(",") Then
                txtCustomerID.Text = txtCustomerID.Text.TrimEnd(",")
            End If

            customermodel.id = hiddenCustomerID.Value
            customermodel.CustomerName = txtCustomerName.Text.Trim()
            customermodel.ExternalCustomerId = txtCustomerID.Text.Trim()
            customermodel.countryId = ddlCountry.SelectedValue

    

            Dim json = JsonConvert.SerializeObject(customermodel)
            Dim stringContent = New StringContent(json, UnicodeEncoding.UTF8, "application/json")
            Dim response = client.PostAsync(apiPOSTCustomersAPIPath, stringContent)
            response.Wait()
            Dim result = response.Result.Content.ReadAsStringAsync().Result

            Dim resultserialize As New JavaScriptSerializer()
            Dim result1 As Dictionary(Of String, String) = resultserialize.Deserialize(Of Dictionary(Of String, String))(result)
            If result1.Count = 1 Then
                Dim customerID As String = result1("id")
                If customerID <> "" Then
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "ShowStatus", "closeCustomerpopup('Customer Saved Successfully');", True)
                End If
            Else
                If result1("StatusCode") = "400" Then
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "ShowStatus", "closeCustomerpopup('" & result1("Message") & "');", True)
                End If
            End If
            BindcustomerGrid()
        End Using
    End Sub


    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Using row1 As GridViewRow = CType((CType(sender, LinkButton)).Parent.Parent, GridViewRow)
            Dim idx As Integer = row1.RowIndex
            If idx <= GridCustomer.Rows.Count Then
                Dim row As GridViewRow = GridCustomer.Rows(idx)
                Dim lblid As Label = TryCast(row.FindControl("lblid"), Label)
                Dim lblCustomerName As Label = TryCast(row.FindControl("lblcustomerName"), Label)
                Dim lblcountryId As Label = TryCast(row.FindControl("lblcountryId"), Label)
                Dim lblexternalCustomerId As Label = TryCast(row.FindControl("lblexternalCustomerId"), Label)

                hiddenCustomerID.Value = lblid.Text
                txtCustomerID.Text = lblexternalCustomerId.Text
                txtCustomerName.Text = lblCustomerName.Text
                ddlCountry.SelectedValue = lblcountryId.Text
            End If
        End Using
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "ShowStatus", "showCustomerpopup();", True)
    End Sub

    Protected Sub lnkSelect_Click(sender As Object, e As EventArgs)
        Using row1 As GridViewRow = CType((CType(sender, LinkButton)).Parent.Parent, GridViewRow)
            Dim idx As Integer = row1.RowIndex
            If idx <= GridCustomer.Rows.Count Then
                Dim row As GridViewRow = GridCustomer.Rows(idx)
                Dim lblid As Label = TryCast(row.FindControl("lblid"), Label)
                Dim lblCustomerName As Label = TryCast(row.FindControl("lblcustomerName"), Label)
                Dim lblexternalCustomerId As Label = TryCast(row.FindControl("lblexternalCustomerId"), Label)
                idlblCustomerID.Text = lblexternalCustomerId.Text
                idlblCustName.Text = lblCustomerName.Text
                hiddenCustomerID.Value = lblid.Text
                BindSiteGrid()
            End If
        End Using

        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "ShowStatus", "showSiteTab();", True)
    End Sub

    Protected Sub txtSearchCustomer_TextChanged(sender As Object, e As EventArgs)

        'Dim customerList As List(Of apiGetSiteItem) = New List(Of apiGetSiteItem)
        'customerList = Session("ApiCustomerList")
        'Dim searchvalue = txtSearchCustomer.Text
        'If searchvalue <> "" Then
        '    customerList = customerList.Where(Function(x) x.customerName.Contains(searchvalue) Or x.id.Contains(searchvalue) Or x.countryId.Contains(searchvalue)).ToList()
        'End If

        'If customerList Is Nothing Or customerList.Count <= 0 Then
        '    customerList = New List(Of apiGetSiteItem)
        'End If

        'Session("ApiCustomerList") = customerList
        'GridCustomer.DataSource = customerList
        'GridCustomer.DataBind()

        BindcustomerGrid()

    End Sub

    Private Sub BindSiteGrid()

        Dim searchvalue = txtSearchSite.Text
        If searchvalue Is Nothing Then
            searchvalue = ""
        End If

        Dim resultToken = GetToken()
        Dim tokenno As String
        tokenno = token.AccessToken
        If tokenno = "" Then
            Dim resultmessage = "Unauthorized. Access token is missing or invalid"
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "ShowStatus", "warningpopup('" & resultmessage & "');", True)
        End If

        'Customer
        Dim apiGetList As List(Of SitesModel) = New List(Of SitesModel)()

        Using client = New HttpClient()
            If Not String.IsNullOrWhiteSpace(token.AccessToken) Then
                client.DefaultRequestHeaders.Clear()
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings.[Get]("ImportCustomerandSite"))
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " & tokenno)
                client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
            End If

            'check whether customer Exists
            'Dim filter As String = "pageSize eq 40"
            'Dim apiGetCustomersAPIPath = apiGetCustomersPath & "?" & filter
            ''Dim apiGetCustomersAPIPath = apiGetSitesPath

            Dim apiGetSitesAPIPath = apiGetNewSitesPath & "?Filter=customerId eq '" & hiddenCustomerID.Value & "'"

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
            Dim response As HttpResponseMessage = client.GetAsync(apiGetSitesAPIPath).Result
            Dim responseString = response.Content.ReadAsStringAsync().Result

            Dim serializer As New JavaScriptSerializer

            If response.IsSuccessStatusCode Then
                serializer = New JavaScriptSerializer()
                Dim jObject = Newtonsoft.Json.Linq.JObject.Parse(responseString)
                Dim result1 As String
                result1 = jObject("items").ToString()
                apiGetList = serializer.Deserialize(Of List(Of SitesModel))(result1)

                If searchvalue <> "" Then
                    apiGetList = apiGetList.Where(Function(x) x.SiteName.ToLower().Contains(searchvalue.ToLower()) Or x.ExternalSiteId.ToLower().Contains(searchvalue.ToLower()) Or x.Street.ToLower().Contains(searchvalue.ToLower())).ToList()
                End If

                'Dim totalpages As String = jObject("totalPages").ToString()
                'If (totalpages > 1) Then
                '    Dim apiGetListAgain As List(Of apiGetSiteItem) = New List(Of apiGetSiteItem)()
                '    Dim index As Integer = 1
                '    For index = 1 To totalpages
                '        serializer = New JavaScriptSerializer()
                '        Dim apiGetCustomersAPIPathForRemainingRecords = apiGetCustomersPath & "?PageIndex= " & index & "&Filter=countryId eq '" & countryID & "'"
                '        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
                '        Dim responseAgain As HttpResponseMessage = client.GetAsync(apiGetCustomersAPIPathForRemainingRecords).Result
                '        Dim responseStringAgain = responseAgain.Content.ReadAsStringAsync().Result
                '        Dim jObjectAgain = Newtonsoft.Json.Linq.JObject.Parse(responseStringAgain)
                '        Dim result1Again As String
                '        result1Again = jObjectAgain("items").ToString()
                '        apiGetListAgain = serializer.Deserialize(Of List(Of apiGetSiteItem))(result1)
                '        'For Each item In apiGetList
                '        '    item.customerName = country
                '        'Next
                '        apiGetList.AddRange(apiGetListAgain)
                '    Next
                'End If

            End If
        End Using

        If Not apiGetList Is Nothing And apiGetList.Count >= 0 Then

            For Each item As SitesModel In apiGetList
                item.BranchId = item.BranchId.TrimEnd(",")
                item.SiteName = item.SiteName.TrimEnd(",")
                item.Street = item.Street.TrimEnd(",")
                item.CityId = item.CityId.TrimEnd(",")
                item.ZipCodeId = item.ZipCodeId.TrimEnd(",")
                item.ExternalSiteId = item.ExternalSiteId.TrimEnd(",")
                item.id = item.id.TrimEnd(",")
                item.CustomerId = item.CustomerId.TrimEnd(",")
            Next

            Session("ApiSiteList") = apiGetList
            GridSite.DataSource = apiGetList
            GridSite.DataBind()
        End If

    End Sub


    Protected Sub btnAddSite_Click(sender As Object, e As EventArgs)

        txtSiteName.Text = ""
        txtStreetAddress.Text = ""
        txtState.Text = ""
        txtExternalSiteID.Text = ""
        ddlBranch.SelectedIndex = "0"
        ddlZipCode.SelectedIndex = "0"
        ddlCity.SelectedIndex = "0"

        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "ShowStatus", "showSitePopup();", True)
    End Sub

    Protected Sub txtSearchSite_TextChanged(sender As Object, e As EventArgs)
        If hiddenCustomerID.Value <> "" Then
            BindSiteGrid()
        End If
    End Sub

    Protected Sub btnImportSite_Click(sender As Object, e As EventArgs)

        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "ShowStatus", "showImportExcelPopup();", True)
    End Sub


    Protected Sub btnUpload_Click(sender As Object, e As EventArgs)
        Try

            'HtmlInputFile MyFile = (HtmlInputFile)(Page.FindControl("fileUpload"));
            'string strFileName = MyFile.PostedFile.FileName;//Grab   the file name from its fully qualified path at client
            ' string filePath = System.IO.Path.GetFileName(strFileName);


            Dim sfilename As String = ""
            sfilename = Path.GetFileName(fileUpload.PostedFile.FileName)

            Dim folderPath As String = Server.MapPath("~/Uploads/")
            If Not Directory.Exists(folderPath) Then
                Directory.CreateDirectory(folderPath)
            End If
            Dim fileName As String = folderPath + sfilename
            If System.IO.File.Exists(fileName) Then

                System.IO.File.Delete(fileName)
            End If
            'Save the File to the Directory (Folder).
            fileUpload.PostedFile.SaveAs(fileName)

            Dim file As FileStream = New FileStream(fileName, FileMode.Open, FileAccess.Read)
            Dim dropdownworkbook = New XSSFWorkbook(file)
            file.Close()
            file.Dispose()

            Session("Filename") = fileName

            Dim sheetList As New List(Of sheetNameList)

            For index = 0 To (dropdownworkbook.NumberOfSheets - 1)
                Dim sheet As New sheetNameList
                sheet.sheetName = dropdownworkbook(index).SheetName
                sheetList.Add(sheet)
            Next

            dropdownworkbook = Nothing

            'Verify Button Start

            Dim result As String
            result = readExcelData()

            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "ShowStatus", "warninguploadpopup('" & result & "');", True)

            fileUpload.Dispose()

            'ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "HideStatus", "stopLoading();", True)

            'Verify Button End

        Catch ex As Exception
        End Try
    End Sub

    Private Function readExcelData() As String
        Dim resultmessage As String = ""
        Dim cntrowtocheck As Integer = 0
        Dim fileLocation As String = ""
        Dim dataSet1 As DataSet = New DataSet()
        Dim countryName As String = ""

        If Not Session("Filename") Is Nothing Then
            fileLocation = Session("Filename")
        End If

        Dim CustomersList As New List(Of Customers)
        Dim msg As String = ""

        Try
            Dim file As FileStream = New FileStream(fileLocation, FileMode.Open, FileAccess.Read)
            Dim dropdownworkbook = New XSSFWorkbook(file)
            file.Close()
            file.Dispose()

            If Not dropdownworkbook Is Nothing Then
                Dim worksheet = CType(dropdownworkbook.GetSheetAt(0), ISheet)

                For index = 1 To worksheet.LastRowNum
                    Dim row As IRow = worksheet.GetRow(index)
                    Dim rowsCount As Integer = worksheet.PhysicalNumberOfRows - 1
                    Dim cellsCount As Integer = row.Cells.Count()

                    'If rowsCount <= 1 Then 'Row Checking
                    '    Return "Row count Must be atleast than 1(exclude Heading)!"
                    'End If

                    If cellsCount < 9 Then 'Column Checking
                        If row.GetCell(0) Is Nothing Then
                            Return "Branch Name Cannot be Empty!"
                        ElseIf row.GetCell(1) Is Nothing Then
                            Return "Smart Connect ID Cannot be Empty!"
                        ElseIf row.GetCell(2) Is Nothing Then
                            Return "Customer Name Cannot be Empty!"
                        ElseIf row.GetCell(3) Is Nothing Then
                            Return "Cust ID Cannot be Empty!"
                        ElseIf row.GetCell(4) Is Nothing Then
                            Return "Site Name Cannot be Empty!"
                        ElseIf row.GetCell(5) Is Nothing Then
                            Return "Site ID Cannot be Empty!"
                        ElseIf row.GetCell(6) Is Nothing Then
                            Return "Site Address Street Cannot be Empty!"
                        ElseIf row.GetCell(7) Is Nothing Then
                            Return "Site Address Zip Cannot be Empty!"
                        ElseIf row.GetCell(8) Is Nothing Then
                            Return "Site Address City Cannot be Empty!"
                        End If
                    End If

                    If Not row Is Nothing Then
                        Dim Customerobj As New Customers

                        If row.Cells.All(Function(d) d.CellType = CellType.Blank) Then
                            Continue For
                        Else
                            Customerobj.BranchName = GetCellAsString(row.GetCell(0))
                            Customerobj.SmartConnectID = GetCellAsString(row.GetCell(1))
                            Customerobj.CustomerName = GetCellAsString(row.GetCell(2))
                            Customerobj.CustID = GetCellAsString(row.GetCell(3))
                            Customerobj.SiteName = GetCellAsString(row.GetCell(4))
                            Customerobj.SiteID = GetCellAsString(row.GetCell(5))
                            Customerobj.SiteAddressStreet = GetCellAsString(row.GetCell(6))
                            Customerobj.SiteAddressZip = GetCellAsString(row.GetCell(7))
                            Customerobj.SiteAddressCity = GetCellAsString(row.GetCell(8))
                            CustomersList.Add(Customerobj)
                        End If
                    End If
                Next
            End If
            'API Call Start


            If Not dropdownworkbook Is Nothing Then
                Dim worksheet = CType(dropdownworkbook.GetSheetAt(0), ISheet)
                If worksheet.LastRowNum = 0 Then
                    Return "Row count Must be atleast 1(Exclude Heading)!"
                Else
                    Dim resultToken = GetToken()
                    Dim tokenno As String
                    tokenno = token.AccessToken
                    If tokenno = "" Then
                        resultmessage = "Unauthorized. Access token is missing or invalid"
                        Return resultmessage
                    End If

                    Dim apiGetList As List(Of apiGetSiteItem) = New List(Of apiGetSiteItem)()

                    Dim serializer As New JavaScriptSerializer
                    Dim IsCustomerExists As Boolean = False
                    Dim IsZipCodeExists As Boolean = False
                    Dim IsCityExists As Boolean = False

                    Dim customerID As String = ""
                    Dim externalCustomerID As String = ""
                    Dim zipCodeID As String = ""
                    Dim cityID As String = ""
                    Dim branchID As String = ""
                    Dim externalSiteID As String = ""

                    CustomersList = CustomersList.Where(Function(x) x.CustID = idlblCustomerID.Text).ToList()

                    If Not CustomersList Is Nothing Then
                        For Each customer In CustomersList
                            'To Check Existing Start


                            ' ''Customer
                            ''Using client = New HttpClient()
                            ''    If Not String.IsNullOrWhiteSpace(token.AccessToken) Then
                            ''        client.DefaultRequestHeaders.Clear()
                            ''        client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings.[Get]("subscriptionId"))
                            ''        client.DefaultRequestHeaders.Add("Authorization", "Bearer " & tokenno)
                            ''        client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
                            ''    End If

                            ''    'check whether customer Exists
                            ''    'Dim filter As String = "pageSize eq 40"
                            ''    'Dim apiGetCustomersAPIPath = apiGetCustomersPath & "?" & filter
                            ''    ''Dim apiGetCustomersAPIPath = apiGetSitesPath

                            ''    Dim apiGetCustomersAPIPath = apiGetCustomersPath & "?Filter=countryId eq '" & countryID & "'"

                            ''    'Dim apiGetCustomersAPIPath = apiGetCustomersPath & "?Filter=countryId eq '" & countryID & "';externalCustomerId eq '" & customer.CustID & "'"

                            ''    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
                            ''    Dim response As HttpResponseMessage = client.GetAsync(apiGetCustomersAPIPath).Result
                            ''    Dim responseString = response.Content.ReadAsStringAsync().Result

                            ''    If response.IsSuccessStatusCode Then
                            ''        serializer = New JavaScriptSerializer()
                            ''        Dim jObject = Newtonsoft.Json.Linq.JObject.Parse(responseString)
                            ''        Dim result1 As String
                            ''        result1 = jObject("items").ToString()
                            ''        apiGetList = serializer.Deserialize(Of List(Of apiGetSiteItem))(result1)

                            ''        Dim pageSize As String = jObject("pageSize").ToString()
                            ''        Dim totalCount As String = jObject("totalCount").ToString()
                            ''        Dim totalpages As String = jObject("totalpages").ToString()

                            ''        If totalpages > 1 Then
                            ''            Dim remaingCount As String = pageSize - totalCount

                            ''            Dim apiGetListAgain As List(Of apiGetSiteItem) = New List(Of apiGetSiteItem)()
                            ''            serializer = New JavaScriptSerializer()
                            ''            Dim apiGetCustomersAPIPathForRemainingRecords = apiGetCustomersPath & "?pageSize= " & remaingCount & "&Filter=countryId eq '" & countryID & "'"
                            ''            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
                            ''            Dim responseAgain As HttpResponseMessage = client.GetAsync(apiGetCustomersAPIPathForRemainingRecords).Result
                            ''            Dim responseStringAgain = responseAgain.Content.ReadAsStringAsync().Result
                            ''            Dim jObjectAgain = Newtonsoft.Json.Linq.JObject.Parse(responseStringAgain)
                            ''            Dim result1Again As String
                            ''            result1Again = jObjectAgain("items").ToString()
                            ''            apiGetListAgain = serializer.Deserialize(Of List(Of apiGetSiteItem))(result1)
                            ''            apiGetList.AddRange(apiGetListAgain)
                            ''        End If


                            ''        'For Each item In apiGetList
                            ''        '    If item.externalCustomerId = customer.CustID Then
                            ''        '        customer.IsCustomerExists = True
                            ''        '    End If
                            ''        'Next
                            ''        customer.IsCustomerExists = apiGetList.Any(Function(x) x.externalCustomerId = customer.CustID)
                            ''        If (customer.IsCustomerExists) Then
                            ''            customerID = apiGetList.Where(Function(x) x.externalCustomerId = customer.CustID).Select(Function(x) x.id).FirstOrDefault()
                            ''            externalCustomerID = apiGetList.Where(Function(x) x.externalCustomerId = customer.CustID).Select(Function(x) x.externalCustomerId).FirstOrDefault()
                            ''        End If
                            ''    End If

                            ''End Using

                            'Zipcode
                            Using client = New HttpClient()
                                If Not String.IsNullOrWhiteSpace(token.AccessToken) Then
                                    client.DefaultRequestHeaders.Clear()
                                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings.[Get]("ImportCustomerandSite"))
                                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " & tokenno)
                                    client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
                                End If

                                Dim apiGetZipcodeAPIPath = apiGetZipCodesPath & "?Filter=zipCodeNumber eq '" & customer.SiteAddressZip & "'"

                                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
                                Dim zipresponse As HttpResponseMessage = client.GetAsync(apiGetZipcodeAPIPath).Result
                                Dim zipresponseString = zipresponse.Content.ReadAsStringAsync().Result

                                If zipresponse.IsSuccessStatusCode Then
                                    serializer = New JavaScriptSerializer()
                                    Dim jObject = Newtonsoft.Json.Linq.JObject.Parse(zipresponseString)
                                    Dim result1 As String
                                    result1 = jObject("items").ToString()
                                    apiGetList = serializer.Deserialize(Of List(Of apiGetSiteItem))(result1)

                                    'For Each item In apiGetList
                                    '    If item.zipCodeNumber = customer.SiteAddressZip Then
                                    '        customer.IsZipCodeExists = True
                                    '    End If
                                    'Next

                                    customer.IsZipCodeExists = apiGetList.Any(Function(x) x.zipCodeNumber = customer.SiteAddressZip)
                                    If (customer.IsZipCodeExists) Then
                                        zipCodeID = apiGetList.Where(Function(x) x.zipCodeNumber = customer.SiteAddressZip).Select(Function(x) x.id).FirstOrDefault()
                                    End If
                                End If
                            End Using

                            'City
                            Using client = New HttpClient()
                                If Not String.IsNullOrWhiteSpace(token.AccessToken) Then
                                    client.DefaultRequestHeaders.Clear()
                                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings.[Get]("ImportCustomerandSite"))
                                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " & tokenno)
                                    client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
                                End If

                                Dim apiGetCitiesAPIPath = apiGetCitiesPath & "?Filter=cityName eq '" & customer.SiteAddressCity & "'"

                                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
                                Dim cityresponse As HttpResponseMessage = client.GetAsync(apiGetCitiesAPIPath).Result
                                Dim cityresponseString = cityresponse.Content.ReadAsStringAsync().Result

                                If cityresponse.IsSuccessStatusCode Then
                                    serializer = New JavaScriptSerializer()
                                    Dim jObject = Newtonsoft.Json.Linq.JObject.Parse(cityresponseString)
                                    Dim result1 As String
                                    result1 = jObject("items").ToString()
                                    apiGetList = serializer.Deserialize(Of List(Of apiGetSiteItem))(result1)

                                    'For Each item In apiGetList
                                    '    If item.cityName = customer.SiteAddressCity Then
                                    '        customer.IsCityExists = True
                                    '    End If
                                    'Next
                                    customer.IsCityExists = apiGetList.Any(Function(x) (x.cityName).ToLower() = (customer.SiteAddressCity).ToLower())
                                    If (customer.IsCityExists) Then
                                        cityID = apiGetList.Where(Function(x) (x.cityName).ToLower() = (customer.SiteAddressCity).ToLower()).Select(Function(x) x.id).FirstOrDefault()
                                    End If
                                End If
                            End Using

                            'Branch
                            Using client = New HttpClient()
                                If Not String.IsNullOrWhiteSpace(token.AccessToken) Then
                                    client.DefaultRequestHeaders.Clear()
                                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings.[Get]("ImportCustomerandSite"))
                                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " & tokenno)
                                    client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
                                End If

                                Dim apiGetBranchesAPIPath = apiGetBranchesPath & "?Filter=branchName eq '" & customer.BranchName & "'"

                                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
                                Dim branchresponse As HttpResponseMessage = client.GetAsync(apiGetBranchesAPIPath).Result
                                Dim branchresponseString = branchresponse.Content.ReadAsStringAsync().Result

                                If branchresponse.IsSuccessStatusCode Then
                                    serializer = New JavaScriptSerializer()
                                    Dim jObject = Newtonsoft.Json.Linq.JObject.Parse(branchresponseString)
                                    Dim result1 As String
                                    result1 = jObject("items").ToString()
                                    apiGetList = serializer.Deserialize(Of List(Of apiGetSiteItem))(result1)

                                    'For Each item In apiGetList
                                    '    If item.cityName = customer.SiteAddressCity Then
                                    '        customer.IsCityExists = True
                                    '    End If
                                    'Next
                                    customer.IsBranchesExists = apiGetList.Any(Function(x) (x.BranchName).ToLower() = (customer.BranchName).ToLower())
                                    If (customer.IsBranchesExists) Then
                                        branchID = apiGetList.Where(Function(x) (x.BranchName).ToLower() = (customer.BranchName).ToLower()).Select(Function(x) x.id).FirstOrDefault()
                                    End If

                                End If
                            End Using


                            'Site
                            Using client = New HttpClient()
                                If Not String.IsNullOrWhiteSpace(token.AccessToken) Then
                                    client.DefaultRequestHeaders.Clear()
                                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings.[Get]("ImportCustomerandSite"))
                                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " & tokenno)
                                    client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
                                End If

                                Dim apiGetSitesAPIPath = apiGetNewSitesPath & "?Filter=externalSiteId eq '" & customer.SiteID & "'"

                                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
                                Dim response As HttpResponseMessage = client.GetAsync(apiGetSitesAPIPath).Result
                                Dim responseString = response.Content.ReadAsStringAsync().Result

                                If response.IsSuccessStatusCode Then
                                    serializer = New JavaScriptSerializer()
                                    Dim jObject = Newtonsoft.Json.Linq.JObject.Parse(responseString)
                                    Dim result1 As String
                                    result1 = jObject("items").ToString()
                                    apiGetList = serializer.Deserialize(Of List(Of apiGetSiteItem))(result1)

                                    customer.IsSitesExists = apiGetList.Any(Function(x) x.externalSiteId = customer.SiteID)
                                    If (customer.IsSitesExists) Then
                                        externalSiteID = apiGetList.Where(Function(x) x.externalSiteId = customer.SiteID).Select(Function(x) x.externalSiteId).FirstOrDefault()
                                    End If
                                End If

                            End Using

                            'To Check Existing End

                            'Create(New customer, ZipCode, City, branch, Site)

                            'If Not customer.IsCustomerExists Then
                            '    Using client = New HttpClient()
                            '        If Not String.IsNullOrWhiteSpace(token.AccessToken) Then
                            '            client.DefaultRequestHeaders.Clear()
                            '            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings.[Get]("ImportCustomerandSite"))
                            '            client.DefaultRequestHeaders.Add("Authorization", "Bearer " & tokenno)
                            '            client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
                            '        End If
                            '        Dim apiPOSTCustomersAPIPath = apiCreateCustomersPath

                            '        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
                            '        Dim customermodel As New CreateCustomersModel
                            '        customermodel.CustomerName = customer.CustomerName
                            '        customermodel.ExternalCustomerId = customer.CustID
                            '        customermodel.countryId = countryID

                            '        Dim json = JsonConvert.SerializeObject(customermodel)
                            '        Dim stringContent = New StringContent(json, UnicodeEncoding.UTF8, "application/json")
                            '        Dim response = client.PostAsync(apiPOSTCustomersAPIPath, stringContent)
                            '        response.Wait()
                            '        Dim result = response.Result.Content.ReadAsStringAsync().Result

                            '        Dim resultserialize As New JavaScriptSerializer()
                            '        Dim result1 As Dictionary(Of String, String) = resultserialize.Deserialize(Of Dictionary(Of String, String))(result)
                            '        If result1.Count = 1 Then
                            '            customerID = result1("id")
                            '            externalCustomerID = customermodel.ExternalCustomerId
                            '        End If
                            '    End Using
                            'End If

                            If Not customer.IsZipCodeExists Then
                                Using client = New HttpClient()
                                    If Not String.IsNullOrWhiteSpace(token.AccessToken) Then
                                        client.DefaultRequestHeaders.Clear()
                                        client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings.[Get]("ImportCustomerandSite"))
                                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " & tokenno)
                                        client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
                                    End If
                                    Dim apiPOSTZipcodesAPIPath = apiCreateZipCodesPath

                                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
                                    Dim zipcodesModel As New CreateZipcodesModel
                                    zipcodesModel.zipCodeNumber = customer.SiteAddressZip

                                    Dim json = JsonConvert.SerializeObject(zipcodesModel)
                                    Dim stringContent = New StringContent(json, UnicodeEncoding.UTF8, "application/json")
                                    Dim response = client.PostAsync(apiPOSTZipcodesAPIPath, stringContent)
                                    response.Wait()
                                    Dim result = response.Result.Content.ReadAsStringAsync().Result

                                    Dim resultserialize As New JavaScriptSerializer()
                                    Dim result1 As Dictionary(Of String, String) = resultserialize.Deserialize(Of Dictionary(Of String, String))(result)
                                    If result1.Count = 1 Then
                                        zipCodeID = result1("id")
                                    End If

                                End Using
                            End If

                            If Not customer.IsCityExists Then
                                Using client = New HttpClient()
                                    If Not String.IsNullOrWhiteSpace(token.AccessToken) Then
                                        client.DefaultRequestHeaders.Clear()
                                        client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings.[Get]("ImportCustomerandSite"))
                                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " & tokenno)
                                        client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
                                    End If
                                    'check whether customer Exists
                                    Dim apiPOSTCitiesAPIPath = apiCreateCitiesPath

                                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
                                    Dim citiesModel As New CreateCitiesModel
                                    citiesModel.cityName = customer.SiteAddressCity

                                    Dim json = JsonConvert.SerializeObject(citiesModel)
                                    Dim stringContent = New StringContent(json, UnicodeEncoding.UTF8, "application/json")
                                    Dim response = client.PostAsync(apiPOSTCitiesAPIPath, stringContent)
                                    response.Wait()
                                    Dim result = response.Result.Content.ReadAsStringAsync().Result

                                    Dim resultserialize As New JavaScriptSerializer()
                                    Dim result1 As Dictionary(Of String, String) = resultserialize.Deserialize(Of Dictionary(Of String, String))(result)
                                    If result1.Count = 1 Then
                                        cityID = result1("id")
                                    End If
                                End Using
                            End If


                            'Site

                            If Not customer.IsSitesExists Then
                                Using client = New HttpClient()
                                    If Not String.IsNullOrWhiteSpace(token.AccessToken) Then
                                        client.DefaultRequestHeaders.Clear()
                                        client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings.[Get]("ImportCustomerandSite"))
                                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " & tokenno)
                                        client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
                                    End If

                                    Dim apiPOSTSitesAPIPath = apiCreateSitesPath

                                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
                                    Dim sitesModel As New SitesModel

                                    sitesModel.SiteName = customer.SiteName
                                    sitesModel.Street = customer.SiteAddressStreet
                                    sitesModel.ZipCodeId = zipCodeID
                                    sitesModel.CityId = cityID
                                    sitesModel.BranchId = branchID
                                    sitesModel.CustomerId = hiddenCustomerID.Value
                                    sitesModel.ExternalSiteId = customer.SiteID
                                    sitesModel.Longitude = "18.028520"
                                    sitesModel.Latitude = "59.309437"

                                    'sitesModel.SiteName = "Singapore"
                                    'sitesModel.Street = "Test Street"
                                    'sitesModel.Longitude = "18.028520"
                                    'sitesModel.Latitude = "59.309437"
                                    'sitesModel.ZipCodeId = "700a4441-cd8a-48ec-b402-cae50915b607"
                                    'sitesModel.CityId = "6d6c213d-b838-46e2-a8d8-1da982168230"
                                    'sitesModel.BranchId = "f3e366a6-1bac-4e14-a204-08d6cd7381fd"
                                    'sitesModel.CustomerId = "fafe4f27-0cb0-4a29-26e1-08d7a56e69ec"
                                    'sitesModel.ExternalSiteId = "123"
                                    'sitesModel.TimeZoneId = "Europe/Stockholm"

                                    Dim json = JsonConvert.SerializeObject(sitesModel)
                                    Dim stringContent = New StringContent(json, UnicodeEncoding.UTF8, "application/json")
                                    Dim response = client.PostAsync(apiPOSTSitesAPIPath, stringContent)
                                    response.Wait()
                                    Dim result = response.Result.Content.ReadAsStringAsync().Result

                                    Dim resultserialize As New JavaScriptSerializer()
                                    Dim result1 As Dictionary(Of String, String) = resultserialize.Deserialize(Of Dictionary(Of String, String))(result)
                                    If result1.Count = 1 Then
                                        externalSiteID = result1("id")
                                        msg = msg + "New site  " + customer.SiteName + " created successfully\n"
                                        BindSiteGrid()
                                    End If

                                End Using
                            End If
                        Next
                    End If
                End If
            End If


            'API Call End
        Catch ex As Exception
            Dim aa As String
            aa = ex.ToString()
            Return aa
        End Try
        If msg = "" Then
            msg = "No New site was created."
        End If
        Return msg
    End Function

    Private Function GetCellAsString(ByVal cell As ICell) As String
        Select Case cell.CellType
            Case CellType.Blank
                Return ""
            Case CellType.Boolean
                Return cell.BooleanCellValue.ToString()
            Case CellType.[Error]
                Return cell.ErrorCellValue.ToString()
            Case CellType.Formula
                Return cell.StringCellValue.ToString()
            Case CellType.Numeric
                Return If(DateUtil.IsCellDateFormatted(cell), cell.DateCellValue.ToString("dd/MM/yyyy"), cell.NumericCellValue.ToString())
            Case CellType.String
                Return cell.StringCellValue.ToString()
            Case CellType.Unknown
                Return "UnKnown"
            Case Else
                Return "Error"
        End Select
    End Function
    Protected Sub btnClose_Click(sender As Object, e As EventArgs)
        fileUpload.Dispose()
    End Sub



    Protected Sub lnkEditSite_Click(sender As Object, e As EventArgs)

    End Sub

    Protected Sub btnSaveSite_Click(sender As Object, e As EventArgs)

        Dim resultToken = GetToken()
        Dim tokenno As String
        tokenno = token.AccessToken
        If tokenno = "" Then
            Dim resultmessage = "Unauthorized. Access token is missing or invalid"
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "ShowStatus", "warningpopup('" & resultmessage & "');", True)
        End If


        Using client = New HttpClient()
            If Not String.IsNullOrWhiteSpace(token.AccessToken) Then
                client.DefaultRequestHeaders.Clear()
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings.[Get]("ImportCustomerandSite"))
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " & tokenno)
                client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
            End If

            Dim apiPOSTSitesAPIPath = apiCreateSitesPath

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
            Dim sitesModel As New SitesModel

            If txtSiteName.Text.Contains(",") Then
                txtSiteName.Text = txtSiteName.Text.TrimEnd(",")
            End If

            If txtStreetAddress.Text.Contains(",") Then
                txtStreetAddress.Text = txtStreetAddress.Text.TrimEnd(",")
            End If

            If txtExternalSiteID.Text.Contains(",") Then
                txtExternalSiteID.Text = txtExternalSiteID.Text.TrimEnd(",")
            End If

            sitesModel.SiteName = txtSiteName.Text.Trim()
            sitesModel.Street = txtStreetAddress.Text.Trim()
            sitesModel.ZipCodeId = ddlZipCode.Text
            sitesModel.CityId = ddlCity.Text
            sitesModel.BranchId = ddlBranch.Text
            sitesModel.CustomerId = hiddenCustomerID.Value
            sitesModel.ExternalSiteId = txtExternalSiteID.Text.Trim()
            sitesModel.Longitude = "18.028520"
            sitesModel.Latitude = "59.309437"

            'sitesModel.SiteName = "Singapore"
            'sitesModel.Street = "Test Street"
            'sitesModel.Longitude = "18.028520"
            'sitesModel.Latitude = "59.309437"
            'sitesModel.ZipCodeId = "700a4441-cd8a-48ec-b402-cae50915b607"
            'sitesModel.CityId = "6d6c213d-b838-46e2-a8d8-1da982168230"
            'sitesModel.BranchId = "f3e366a6-1bac-4e14-a204-08d6cd7381fd"
            'sitesModel.CustomerId = "fafe4f27-0cb0-4a29-26e1-08d7a56e69ec"
            'sitesModel.ExternalSiteId = "123"
            'sitesModel.TimeZoneId = "Europe/Stockholm"

            Dim json = JsonConvert.SerializeObject(sitesModel)
            Dim stringContent = New StringContent(json, UnicodeEncoding.UTF8, "application/json")
            Dim response = client.PostAsync(apiPOSTSitesAPIPath, stringContent)
            response.Wait()
            Dim result = response.Result.Content.ReadAsStringAsync().Result

            Dim resultserialize As New JavaScriptSerializer()
            Dim result1 As Dictionary(Of String, String) = resultserialize.Deserialize(Of Dictionary(Of String, String))(result)
            If result1.Count = 1 Then
                Dim externalSiteID As String = result1("id")
                If externalSiteID <> "" Then
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "ShowStatus", "closeSitepopup('Site Saved Successfully');", True)
                End If
            Else
                If result1("StatusCode") = "400" Then
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "ShowStatus", "closeSitepopup('" & result1("Message") & "');", True)
                End If
            End If
            BindSiteGrid()
        End Using
    End Sub
    Public Class CustomersModel
        Public Property CustomerName() As String
        Public Property ExternalCustomerId() As String
        Public Property countryId() As String
        Public Property id() As String
    End Class
    Public Class CreateCustomersModel
        Public Property CustomerName() As String
        Public Property ExternalCustomerId() As String
        Public Property countryId() As String
    End Class
    Private Async Function GetToken() As Task
        Authorize().Wait()
    End Function
    Private Function Authorize() As Task
        Dim authority As String = ConfigurationManager.AppSettings.[Get]("authority")
        Dim clientId As String = ConfigurationManager.AppSettings.[Get]("clientId")
        Dim redirecturl As String = ConfigurationManager.AppSettings.[Get]("redirectUri")
        Dim resource As String = ConfigurationManager.AppSettings.[Get]("resource")
        Dim clientSecret As String = ConfigurationManager.AppSettings.[Get]("clientSecret")
        Return Task.Run(Async Function()
                            Dim clientCredential = New ClientCredential(clientId, clientSecret)
                            Dim context As AuthenticationContext = New AuthenticationContext(authority, False)
                            token = Await context.AcquireTokenAsync(resource, clientCredential)
                        End Function)
    End Function

    Public Class apiGetSiteItem
        Public Property id() As String
        Public Property customerName() As String
        Public Property externalCustomerId() As String
        Public Property countryId() As String
        Public Property zipCodeNumber() As String
        Public Property cityName() As String
        Public Property BranchName() As String
        Public Property externalSiteId() As String
        Public Property countryName() As String
    End Class

    Public Class apiGetCountryList
        Public Property id() As String
        Public Property countryName() As String
        Public Property countryCode() As String
        Public Property mccInformationId() As String

    End Class

    Public Class ZipcodesModel
        Public Property id() As String
        Public Property zipCodeNumber() As String
    End Class
    Public Class CreateZipcodesModel
        Public Property zipCodeNumber() As String
    End Class

    Public Class CitiesModel
        Public Property id() As String
        Public Property cityName() As String
    End Class
    Public Class CreateCitiesModel
        Public Property cityName() As String
    End Class
    Public Class BranchModel
        Public Property id() As String
        Public Property regionId() As String
        Public Property branchName() As String
        Public Property externalBranchId() As String
    End Class

    Public Class SitesModel
        Public Property id() As String
        Public Property SiteName() As String
        Public Property Street() As String
        Public Property Longitude() As String
        Public Property Latitude() As String
        Public Property ZipCodeId() As String
        Public Property CityId() As String
        Public Property BranchId() As String
        Public Property CustomerId() As String
        Public Property ExternalSiteId() As String
        Public Property TimeZoneId() As String
    End Class

    Public Class Customers
        Public Property BranchName() As String
        Public Property SmartConnectID() As String
        Public Property CustomerName() As String
        Public Property CustID() As String
        Public Property SiteName() As String
        Public Property SiteID() As String
        Public Property SiteAddressStreet() As String
        Public Property SiteAddressZip() As String
        Public Property SiteAddressCity() As String
        Public Property IsCustomerExists() As Boolean
        Public Property IsZipCodeExists() As Boolean
        Public Property IsCityExists() As Boolean
        Public Property IsBranchesExists() As Boolean
        Public Property IsSitesExists() As Boolean
    End Class
    Public Class sheetNameList
        Public Property sheetName() As String
    End Class
End Class

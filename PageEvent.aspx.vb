
Partial Class PageEvent
    Inherits System.Web.UI.Page

    Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        Throw (New ArgumentNullException())
    End Sub

    Public Sub Page_Error(ByVal sender As Object, ByVal e As EventArgs)
        Dim objErr As Exception = Server.GetLastError().GetBaseException()
        Dim err As String = "<b>Error Caught in Page_Error event</b><hr><br>" & "<br><b>Error in: </b>" & Request.Url.ToString() & "<br><b>Error Message: </b>" & objErr.Message.ToString() & "<br><b>Stack Trace:</b><br>" & objErr.StackTrace.ToString()
        Response.Write(err.ToString())
        Server.ClearError()
    End Sub

End Class

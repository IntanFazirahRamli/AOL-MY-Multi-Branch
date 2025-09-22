Imports Microsoft.VisualBasic
Imports System
Imports System.Web
Imports System.Text
Imports System.Web.UI.WebControls


Namespace MessageBox

    Public Class Message
        Inherits WebControl

        Public Shared Sub Alert(ByRef aspxPage As System.Web.UI.Page, ByVal strMessage As String, ByVal strURL As String, ByVal strKey As String)
            'Alert message & Redirect webpage
            Dim strScript As String = "<script language=JavaScript>alert('" & strMessage & "'); window.location.href = '" + strURL + "';</script>"

            If (Not aspxPage.IsStartupScriptRegistered(strKey)) Then
                aspxPage.RegisterStartupScript(strKey, strScript)
            End If
        End Sub

        Public Shared Sub AlertBlock(ByRef aspxPage As System.Web.UI.Page, ByVal strMessage As String, ByVal strURL As String, ByVal strKey As String)
            'Alert message & Redirect webpage
            Dim strScript As String = "<script language=JavaScript>alert('" & strMessage & "'); window.location.href = '" + strURL + "';</script>"

            If (Not aspxPage.IsStartupScriptRegistered(strKey)) Then
                aspxPage.RegisterStartupScript(strKey, strScript)
            End If
        End Sub

        Public Shared Sub Alert(ByRef aspxPage As System.Web.UI.Page, ByVal strMessage As String, ByVal strKey As String)
            'Alert message
            strMessage = strMessage.Replace("'", "\'")
            Dim strScript As String = "<script language=JavaScript>alert('" & strMessage & "');</script>"

            If (Not aspxPage.IsStartupScriptRegistered(strKey)) Then
                aspxPage.RegisterStartupScript(strKey, strScript)
            End If
        End Sub

        Public Shared Sub Confirm(ByRef aspxPage As System.Web.UI.Page, ByVal strMessage As String, ByVal strKey As String, ByVal Value As String)
            'Confirmation Message
            Dim sb As StringBuilder = New StringBuilder()
            MsgBox("Arghya")
            strMessage = strMessage.Replace("\n", "\\n")
            strMessage = strMessage.Replace("\", "'")

            'sb.Append("<INPUT type=hidden value='0' name='" + hiddenfield_name + "'>")
            MsgBox(strMessage)
            MsgBox(strKey)

            sb.Append("<script language='javascript'>")
            sb.Append(" if(confirm( """ + strMessage + """ ))")
            sb.Append(" { ")
            'sb.Append("document.forms[0]." + hiddenfield_name + ".value='1';" + "document.forms[0].submit();}")
            sb.Append("document.forms['aspnetForm'].ctl00$ContentPlaceHolder1$hiddenMsg.value='" + Value + "';" + " document.forms['aspnetForm'].submit();}")
            sb.Append(" else { ")
            'sb.Append("document.forms[0]." + hiddenfield_name + ".value='0';}")
            sb.Append("document.forms['aspnetForm'].ctl00$ContentPlaceHolder1$hiddenMsg.value='0';}")
            sb.Append("</script>")
            aspxPage.RegisterStartupScript(strKey, sb.ToString)
        End Sub

        Public Shared Sub ConfirmHTML(ByRef aspxPage As System.Web.UI.Page, ByVal strMessage As String, ByVal strKey As String, ByVal Value As String)
            'Confirmation Message
            Dim sb As StringBuilder = New StringBuilder()

            strMessage = strMessage.Replace("\n", "\\n")
            strMessage = strMessage.Replace("\", "'")

            'sb.Append("<INPUT type=hidden value='0' name='" + hiddenfield_name + "'>")

            sb.Append("<script language='javascript'>")
            sb.Append(" if(confirm( """ + strMessage + """ ))")
            sb.Append(" { ")
            'sb.Append("document.forms[0]." + hiddenfield_name + ".value='1';" + "document.forms[0].submit();}")
            sb.Append("document.forms['form1'].hiddenMsg.value='" + Value + "';" + " document.forms['form1'].submit();}")
            sb.Append(" else { ")
            'sb.Append("document.forms[0]." + hiddenfield_name + ".value='0';}")
            sb.Append("document.forms['form1'].hiddenMsg.value='0';}")
            sb.Append("</script>")
            aspxPage.RegisterStartupScript(strKey, sb.ToString)
        End Sub

        Public Shared Sub Confirm(ByRef aspxPage As System.Web.UI.Page, ByVal strMessage As String, ByVal strKey As String, ByVal OKValue As String, ByVal CancelValue As String)
            'Confirmation Message
            'MsgBox("abc")
            Dim sb As StringBuilder = New StringBuilder()

            strMessage = strMessage.Replace("\n", "\\n")
            strMessage = strMessage.Replace("\", "'")

            'sb.Append("<INPUT type=hidden value='0' name='" + hiddenfield_name + "'>")

            sb.Append("<script language='javascript'>")
            sb.Append(" if(confirm( """ + strMessage + """ ))")
            sb.Append(" { ")
            'sb.Append("document.forms[0]." + hiddenfield_name + ".value='1';" + "document.forms[0].submit();}")
            sb.Append("document.forms['aspnetForm'].ctl00$ContentPlaceHolder1$hiddenMsg.value='" + OKValue + "';" + " document.forms['aspnetForm'].submit();}")
            sb.Append(" else { ")
            'sb.Append("document.forms[0]." + hiddenfield_name + ".value='0';}")
            sb.Append("document.forms['aspnetForm'].ctl00$ContentPlaceHolder1$hiddenMsg.value='" + CancelValue + "';" + " document.forms['aspnetForm'].submit();}")
            sb.Append("</script>")
            aspxPage.RegisterStartupScript(strKey, sb.ToString)
        End Sub

        Public Shared Sub ConfirmYesNo(ByRef aspxPage As System.Web.UI.Page, ByVal strMessage As String, ByVal strKey As String, ByVal Value As String)
            'Confirmation Message
            Dim sb As StringBuilder = New StringBuilder()

            strMessage = strMessage.Replace("\n", "\\n")
            strMessage = strMessage.Replace("\", "'")

            'sb.Append("<INPUT type=hidden value='0' name='" + hiddenfield_name + "'>")

            sb.Append("<script language='javascript'>")
            sb.Append(" if(confirm( """ + strMessage + """ ))")
            sb.Append(" { ")
            'sb.Append("document.forms[0]." + hiddenfield_name + ".value='1';" + "document.forms[0].submit();}")
            sb.Append("document.forms['aspnetForm'].ctl00$ContentPlaceHolder1$hiddenMsg.value='" + Value + "';" + " document.forms['aspnetForm'].submit();}")
            sb.Append(" else { ")
            'sb.Append("document.forms[0]." + hiddenfield_name + ".value='0';}")
            sb.Append("document.forms['aspnetForm'].ctl00$ContentPlaceHolder1$hiddenMsg.value='0';}")
            sb.Append("</script>")
            aspxPage.RegisterStartupScript(strKey, sb.ToString)
        End Sub

        Public Shared Sub ConfirmHTMLYEsNo(ByRef aspxPage As System.Web.UI.Page, ByVal strMessage As String, ByVal strKey As String, ByVal Value As String)
            'Confirmation Message
            Dim sb As StringBuilder = New StringBuilder()

            strMessage = strMessage.Replace("\n", "\\n")
            strMessage = strMessage.Replace("\", "'")

            'sb.Append("<INPUT type=hidden value='0' name='" + hiddenfield_name + "'>")

            sb.Append("<script language='javascript'>")
            sb.Append(" if(confirm( """ + strMessage + """ ))")
            sb.Append(" { ")
            'sb.Append("document.forms[0]." + hiddenfield_name + ".value='1';" + "document.forms[0].submit();}")
            sb.Append("document.forms['form1'].hiddenMsg.value='" + Value + "';" + " document.forms['form1'].submit();}")
            sb.Append(" else { ")
            'sb.Append("document.forms[0]." + hiddenfield_name + ".value='0';}")
            sb.Append("document.forms['form1'].hiddenMsg.value='0';}")
            sb.Append("</script>")
            aspxPage.RegisterStartupScript(strKey, sb.ToString)
        End Sub

        Public Shared Sub ConfirmYesNo(ByRef aspxPage As System.Web.UI.Page, ByVal strMessage As String, ByVal strKey As String, ByVal YESValue As String, ByVal NOValue As String)
            'MsgBox("Confirmation Message")
            Dim sb As StringBuilder = New StringBuilder()

            strMessage = strMessage.Replace("\n", "\\n")
            strMessage = strMessage.Replace("\", "'")

            'sb.Append("<INPUT type=hidden value='0' name='" + hiddenfield_name + "'>")

            sb.Append("<script language='javascript'>")
            sb.Append(" if(confirm( """ + strMessage + """ ))")
            sb.Append(" { ")
            'sb.Append("document.forms[0]." + hiddenfield_name + ".value='1';" + "document.forms[0].submit();}")
            sb.Append("window.document.forms['aspnetForm'].ctl00_ContentPlaceHolder1$hiddenMsg.value='" + YESValue + "';" + " window.document.forms['aspnetForm'].submit();}")
            sb.Append(" else { ")
            'sb.Append("document.forms[0]." + hiddenfield_name + ".value='0';}")
            sb.Append("window.document.forms['aspnetForm'].ctl00_ContentPlaceHolder1$hiddenMsg.value='" + NOValue + "';" + " window.document.forms['aspnetForm'].submit();}")
            sb.Append("</script>")
            aspxPage.RegisterStartupScript(strKey, sb.ToString)
        End Sub

    End Class

End Namespace

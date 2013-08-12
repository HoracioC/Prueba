Imports System.Web.Services
Imports System.IO
Imports System
Imports System.Configuration

Partial Class _Default
    Inherits System.Web.UI.Page

    <WebMethod()> _
    Public Shared Function KeepActiveSession() As Boolean

        If HttpContext.Current.Session("datos") IsNot Nothing Then
            Return True
        Else
            Return False
        End If

    End Function

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Response.Redirect("Materia.aspx")
    End Sub

End Class


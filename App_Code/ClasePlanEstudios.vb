Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class ClasePlanEstudios
    Public Shared CE_Normales As String = System.Configuration.ConfigurationManager.ConnectionStrings("SICENConnectionString").ConnectionString

    
    Public Shared Function PlanEstudiosGet() As PlanEstudio.PlanEstudio_GETDataTable

        Dim cmd As New SqlCommand("SICEN.PlanEstudiosRPT_Sel", New SqlConnection(CE_Normales))
        Dim da As New SqlDataAdapter(cmd)
        Dim dt As New PlanEstudio.PlanEstudio_GETDataTable
        Try
            da.Fill(dt)
        Catch ex As Exception
            LogXML.LogEx("ClasePlanEstudios - PlanEstudiosGet", ex.Message)
            Throw New Exception(ex.Message)
        Finally
            cmd.Connection.Close()
        End Try
        Return dt
    End Function
   
End Class


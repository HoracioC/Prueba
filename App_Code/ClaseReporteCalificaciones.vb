Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class ClaseReporteCalificaciones
    Public Shared CE_Normales As String = System.Configuration.ConfigurationManager.ConnectionStrings("SICENConnectionString").ConnectionString

    Public Shared Function ReporteCalificacionesGridGet() As DataTable
        Dim cmd As New SqlCommand("SICEN.ReporteCalificaciones_Grid_Sel2", New SqlConnection(CE_Normales))
        Dim da As New SqlDataAdapter(cmd)
        Dim dt As New DataSetReporteCalificaciones.ReporteCalificaciones_Grid_Sel2DataTable
        Try
            da.Fill(dt)
        Catch ex As Exception
            LogXML.LogEx("Clase_Generacion - GeneracionGrid_Sel", ex.Message)
            Throw New Exception(ex.Message)
        Finally
            cmd.Connection.Close()
        End Try
        Return dt
    End Function
End Class

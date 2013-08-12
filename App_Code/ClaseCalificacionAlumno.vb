Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class ClaseCalificacionAlumno
    Public Shared CE_Normales As String = System.Configuration.ConfigurationManager.ConnectionStrings("SICENConnectionString").ConnectionString

    Public Class O_Grupo
        Public id_Grupo As Integer = 0
    End Class

    Public Shared Function CalificacionesAlumnoGridGet(ByVal O_Grupo As O_Grupo) As DataTable
        Dim cmd As New SqlCommand("SICEN.AlumnoCalificaciones_Grid_Sel", New SqlConnection(CE_Normales))
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@id_Grupo", SqlDbType.Int).Value = O_Grupo.id_Grupo
        Dim da As New SqlDataAdapter(cmd)
        Dim dt As New DataTable()
        Try
            da.Fill(dt)
        Catch ex As Exception
            LogXML.LogEx("ClaseCalificacionAlumno - CalificacionesAlumnoGridGet", ex.Message)
            Throw New Exception(ex.Message)
        Finally
            cmd.Connection.Close()
        End Try
        Return dt
    End Function
End Class

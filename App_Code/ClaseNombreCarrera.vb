Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class ClaseNombreCarrera
    Public Shared CE_Normales As String = System.Configuration.ConfigurationManager.ConnectionStrings("SICENConnectionString").ConnectionString

    Public Class O_Nombre_Carrera
        Public id_Nombre_Carrera As Integer = 0
        Public Nombre_Carrera As String = Nothing
        Public Abreviatura As String = Nothing
    End Class

    Public Shared Function NombreCarreraGridGet() As DataTable
        Dim cmd As New SqlCommand("SICEN.Nombre_CarreraGrid_Sel", New SqlConnection(CE_Normales))
        Dim da As New SqlDataAdapter(cmd)
        Dim dt As New DataTable()
        Try
            da.Fill(dt)
        Catch ex As Exception
            LogXML.LogEx("ClaseNombreCarrera - NombreCarreraGridGet", ex.Message)
            Throw New Exception(ex.Message)
        Finally
            cmd.Connection.Close()
        End Try
        Return dt
    End Function

    Public Shared Function NombreCarreraSet(ByVal O_Nombre_Carrera As ClaseNombreCarrera.O_Nombre_Carrera) As Integer
        Dim cmd As New SqlCommand("SICEN.Nombre_Carrera_Ins", New SqlConnection(CE_Normales))
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@Nombre_Carrera", SqlDbType.Text).Value = O_Nombre_Carrera.Nombre_Carrera
        cmd.Parameters.Add("@Abreviatura", SqlDbType.Text).Value = O_Nombre_Carrera.Abreviatura

        Dim ok As Boolean
        Try
            If cmd.Connection.State = ConnectionState.Closed Then
                cmd.Connection.Open()
            End If
            cmd.ExecuteNonQuery()
            ok = True
        Catch ex As Exception
            LogXML.LogEx("ClaseNombreCarrera - NombreCarrera_Set", ex.Message)
            Throw New Exception(ex.Message)
            ok = False
        Finally
            cmd.Connection.Close()
        End Try
        Return ok
    End Function

    Public Shared Function NombreCarreraEdit(ByVal O_Nombre_Carrera As ClaseNombreCarrera.O_Nombre_Carrera) As Boolean
        Dim cmd As New SqlCommand("SICEN.Nombre_Carrera_Edit", New SqlConnection(CE_Normales))
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@id_Nombre_Carrera", SqlDbType.Int).Value = O_Nombre_Carrera.id_Nombre_Carrera
        cmd.Parameters.Add("@Nombre_Carrera", SqlDbType.Text).Value = O_Nombre_Carrera.Nombre_Carrera
        cmd.Parameters.Add("@Abreviatura", SqlDbType.Text).Value = O_Nombre_Carrera.Abreviatura

        Dim ok As Boolean
        Try
            If cmd.Connection.State = ConnectionState.Closed Then
                cmd.Connection.Open()
            End If
            cmd.ExecuteNonQuery()
            ok = True
        Catch ex As Exception
            ok = False
            LogXML.LogEx("ClaseNombreCarrera - NombreCarrrera_Edit", ex.Message)
            Throw New Exception(ex.Message)
        Finally
            cmd.Connection.Close()
        End Try
        Return ok
    End Function

    Public Shared Function NombreCarreraDel(ByVal O_Nombre_Carrera As ClaseNombreCarrera.O_Nombre_Carrera) As Boolean
        Dim cmd As New SqlCommand("SICEN.Nombre_Carrera_Del", New SqlConnection(CE_Normales))
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@id_Nombre_Carrera", SqlDbType.Int).Value = O_Nombre_Carrera.id_Nombre_Carrera

        Dim ok As Boolean
        Try
            If cmd.Connection.State = ConnectionState.Closed Then
                cmd.Connection.Open()
            End If
            cmd.ExecuteNonQuery()
            ok = True
        Catch ex As Exception
            ok = False
            LogXML.LogEx("ClaseNombreCarrera - NombreCarrera_Del", ex.Message)
            Throw New Exception(ex.Message)
        Finally
            cmd.Connection.Close()
        End Try
        Return ok
    End Function

End Class

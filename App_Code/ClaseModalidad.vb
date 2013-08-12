Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class ClaseModalidad
    Public Shared CE_Normales As String = System.Configuration.ConfigurationManager.ConnectionStrings("SICENConnectionString").ConnectionString

    Public Class O_Modalidad
        Public id_Modalidad As Integer = 0
        Public Nombre_Modalidad As String = Nothing
    End Class

    Public Shared Function ModalidadGridGet() As DataTable
        Dim cmd As New SqlCommand("SICEN.ModalidadGrid_Sel", New SqlConnection(CE_Normales))
        Dim da As New SqlDataAdapter(cmd)
        Dim dt As New DataTable()
        Try
            da.Fill(dt)
        Catch ex As Exception
            LogXML.LogEx("ClaseModalidad - ModalidadGridGet", ex.Message)
            Throw New Exception(ex.Message)
        Finally
            cmd.Connection.Close()
        End Try
        Return dt
    End Function

    Public Shared Function ModalidadSet(ByVal O_Modalidad As ClaseModalidad.O_Modalidad) As Integer
        Dim cmd As New SqlCommand("SICEN.Modalidad_Ins", New SqlConnection(CE_Normales))
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@Nombre_Modalidad", SqlDbType.Text).Value = O_Modalidad.Nombre_Modalidad

        Dim ok As Boolean
        Try
            If cmd.Connection.State = ConnectionState.Closed Then
                cmd.Connection.Open()
            End If
            cmd.ExecuteNonQuery()
            ok = True
        Catch ex As Exception
            LogXML.LogEx("ClaseModalidad - ModalidadSet", ex.Message)
            Throw New Exception(ex.Message)
            ok = False
        Finally
            cmd.Connection.Close()
        End Try
        Return ok
    End Function

    Public Shared Function ModalidadEdit(ByVal O_Modalidad As ClaseModalidad.O_Modalidad) As Boolean
        Dim cmd As New SqlCommand("SICEN.Modalidad_Edit", New SqlConnection(CE_Normales))
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@id_Modalidad", SqlDbType.Int).Value = O_Modalidad.id_Modalidad
        cmd.Parameters.Add("@Nombre_Modalidad", SqlDbType.Text).Value = O_Modalidad.Nombre_Modalidad

        Dim ok As Boolean
        Try
            If cmd.Connection.State = ConnectionState.Closed Then
                cmd.Connection.Open()
            End If
            cmd.ExecuteNonQuery()
            ok = True
        Catch ex As Exception
            ok = False
            LogXML.LogEx("ClaseModalidad - ModalidadEdit", ex.Message)
            Throw New Exception(ex.Message)
        Finally
            cmd.Connection.Close()
        End Try
        Return ok
    End Function

    Public Shared Function ModalidadDel(ByVal O_Modalidad As ClaseModalidad.O_Modalidad) As Boolean
        Dim cmd As New SqlCommand("SICEN.Modalidad_Del", New SqlConnection(CE_Normales))
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@id_Modalidad", SqlDbType.Int).Value = O_Modalidad.id_Modalidad

        Dim ok As Boolean
        Try
            If cmd.Connection.State = ConnectionState.Closed Then
                cmd.Connection.Open()
            End If
            cmd.ExecuteNonQuery()
            ok = True
        Catch ex As Exception
            ok = False
            LogXML.LogEx("ClaseModalidad - ModalidadDel", ex.Message)
            Throw New Exception(ex.Message)
        Finally
            cmd.Connection.Close()
        End Try
        Return ok
    End Function

End Class

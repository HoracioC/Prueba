Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class ClaseMateria
    Public Shared CE_Normales As String = System.Configuration.ConfigurationManager.ConnectionStrings("SICENConnectionString").ConnectionString

    Public Class O_Materia
        Public id_Materia As Integer = 0
        Public Nombre_Materia As String = Nothing
        Public Horas As Decimal = 0
        Public Creditos As Decimal = 0
    End Class

    Public Shared Function MateriaSet(ByVal O_Materia As ClaseMateria.O_Materia) As Integer
        Dim cmd As New SqlCommand("SICEN.Materia_Ins", New SqlConnection(CE_Normales))
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@Nombre_Materia", SqlDbType.Text).Value = O_Materia.Nombre_Materia
        cmd.Parameters.Add("@Horas", SqlDbType.Decimal).Value = O_Materia.Horas
        cmd.Parameters.Add("@Creditos", SqlDbType.Decimal).Value = O_Materia.Creditos


        Dim ok As Boolean
        Try
            If cmd.Connection.State = ConnectionState.Closed Then
                cmd.Connection.Open()
            End If
            cmd.ExecuteNonQuery()
            ok = True
        Catch ex As Exception
            LogXML.LogEx("ClaseMaterias - Materia_Set", ex.Message)
            Throw New Exception(ex.Message)
            ok = False
        Finally
            cmd.Connection.Close()
        End Try
        Return ok
    End Function

    Public Shared Function MateriaGridGet() As DataTable
        Dim cmd As New SqlCommand("SICEN.MateriaGrid_Sel", New SqlConnection(CE_Normales))
        Dim da As New SqlDataAdapter(cmd)
        Dim dt As New DataTable
        Try
            da.Fill(dt)
        Catch ex As Exception
            LogXML.LogEx("ClaseMateria - Materia_GridGet", ex.Message)
            Throw New Exception(ex.Message)
        Finally
            cmd.Connection.Close()
        End Try
        Return dt
    End Function

    Public Shared Function MateriaDel(ByVal O_Materia As ClaseMateria.O_Materia) As Boolean
        Dim cmd As New SqlCommand("SICEN.Materia_Del", New SqlConnection(CE_Normales))
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@id_Materia", SqlDbType.Int).Value = O_Materia.id_Materia

        Dim ok As Boolean
        Try
            If cmd.Connection.State = ConnectionState.Closed Then
                cmd.Connection.Open()
            End If
            cmd.ExecuteNonQuery()
            ok = True
        Catch ex As Exception
            ok = False
            LogXML.LogEx("ClaseMateria - Materia_Del", ex.Message)
            Throw New Exception(ex.Message)
        Finally
            cmd.Connection.Close()
        End Try
        Return ok
    End Function

    Public Shared Function MateriaEdit(ByVal O_Materia As ClaseMateria.O_Materia) As Boolean
        Dim cmd As New SqlCommand("SICEN.Materia_Edit", New SqlConnection(CE_Normales))
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@id_Materia", SqlDbType.Int).Value = O_Materia.id_Materia
        cmd.Parameters.Add("@Nombre_Materia", SqlDbType.Text).Value = O_Materia.Nombre_Materia
        cmd.Parameters.Add("@Horas", SqlDbType.Decimal).Value = O_Materia.Horas
        cmd.Parameters.Add("@Creditos", SqlDbType.Decimal).Value = O_Materia.Creditos

        Dim ok As Boolean
        Try
            If cmd.Connection.State = ConnectionState.Closed Then
                cmd.Connection.Open()
            End If
            cmd.ExecuteNonQuery()
            ok = True
        Catch ex As Exception
            ok = False
            LogXML.LogEx("ClaseMateria - Materia_Edit", ex.Message)
            Throw New Exception(ex.Message)
        Finally
            cmd.Connection.Close()
        End Try
        Return ok
    End Function

    Public Shared Function MateriaDdlGet() As DataTable
        Dim cmd As New SqlCommand("SICEN.MateriaDdl_Sel", New SqlConnection(CE_Normales))
        Dim da As New SqlDataAdapter(cmd)
        Dim dt As New DataTable
        Try
            da.Fill(dt)
        Catch ex As Exception
            LogXML.LogEx("ClaseMateria - Materia_DdlGet", ex.Message)
            Throw New Exception(ex.Message)
        Finally
            cmd.Connection.Close()
        End Try
        Return dt
    End Function
End Class


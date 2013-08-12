Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class ClaseStatusAlumno

    Public Shared CE_Normales As String = System.Configuration.ConfigurationManager.ConnectionStrings("SICENConnectionString").ConnectionString

    Public Class O_Status_Alumno
        Public id_Status_Alumno As Integer = 0
        Public Descripcion As String = Nothing
        Public Abreviacion As String = Nothing
    End Class

    Public Shared Function StatusAlumnoGridGet() As DataTable
        Dim cmd As New SqlCommand("SICEN.Status_AlumnoGrid_Sel", New SqlConnection(CE_Normales))
        Dim da As New SqlDataAdapter(cmd)
        Dim dt As New DataTable()
        Try
            da.Fill(dt)
        Catch ex As Exception
            LogXML.LogEx("ClaseStatusAlumno - StatusAlumnoGridGet", ex.Message)
            Throw New Exception(ex.Message)
        Finally
            cmd.Connection.Close()
        End Try
        Return dt
    End Function

    Public Shared Function StatusAlumnoSet(ByVal O_Status_Alumno As ClaseStatusAlumno.O_Status_Alumno) As Integer
        Dim cmd As New SqlCommand("SICEN.Status_Alumno_Ins", New SqlConnection(CE_Normales))
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@Descripcion", SqlDbType.Text).Value = O_Status_Alumno.Descripcion
        cmd.Parameters.Add("@Abreviacion", SqlDbType.Text).Value = O_Status_Alumno.Abreviacion

        Dim ok As Boolean
        Try
            If cmd.Connection.State = ConnectionState.Closed Then
                cmd.Connection.Open()
            End If
            cmd.ExecuteNonQuery()
            ok = True
        Catch ex As Exception
            LogXML.LogEx("ClaseStatusAlumno - StatusAlumnoSet", ex.Message)
            Throw New Exception(ex.Message)
            ok = False
        Finally
            cmd.Connection.Close()
        End Try
        Return ok
    End Function

    Public Shared Function StatusAlumnoEdit(ByVal O_Status_Alumno As ClaseStatusAlumno.O_Status_Alumno) As Boolean
        Dim cmd As New SqlCommand("SICEN.Status_Alumno_Edit", New SqlConnection(CE_Normales))
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@id_Status_Alumno", SqlDbType.Int).Value = O_Status_Alumno.id_Status_Alumno
        cmd.Parameters.Add("@Descripcion", SqlDbType.Text).Value = O_Status_Alumno.Descripcion
        cmd.Parameters.Add("@Abreviacion", SqlDbType.Text).Value = O_Status_Alumno.Abreviacion

        Dim ok As Boolean
        Try
            If cmd.Connection.State = ConnectionState.Closed Then
                cmd.Connection.Open()
            End If
            cmd.ExecuteNonQuery()
            ok = True
        Catch ex As Exception
            ok = False
            LogXML.LogEx("ClaseStatusAlumno - StatusAlumnoEdit", ex.Message)
            Throw New Exception(ex.Message)
        Finally
            cmd.Connection.Close()
        End Try
        Return ok
    End Function

    Public Shared Function StatusAlumnoDel(ByVal O_Status_Alumno As ClaseStatusAlumno.O_Status_Alumno) As Boolean
        Dim cmd As New SqlCommand("SICEN.Status_Alumno_Del", New SqlConnection(CE_Normales))
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@id_Status_Alumno", SqlDbType.Int).Value = O_Status_Alumno.id_Status_Alumno

        Dim ok As Boolean
        Try
            If cmd.Connection.State = ConnectionState.Closed Then
                cmd.Connection.Open()
            End If
            cmd.ExecuteNonQuery()
            ok = True
        Catch ex As Exception
            ok = False
            LogXML.LogEx("ClaseStatusAlumno - StatusAlumnoDel", ex.Message)
            Throw New Exception(ex.Message)
        Finally
            cmd.Connection.Close()
        End Try
        Return ok
    End Function

End Class

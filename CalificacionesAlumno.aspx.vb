Imports System.Data
Imports ClaseCalificacionAlumno
Imports Tools

Partial Class CalificacionesAlumno
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                Dim O_Grupo As New O_Grupo
                O_Grupo.id_Grupo = 122
                Dim dt As New DataTable
                dt = CalificacionesAlumnoGridGet(O_Grupo)
                Session("dtCalificaciones") = dt
                LlenarCalificacionAlumno()
            Catch ex As Exception
                lblEliminado.Text = EvaluarError(Err.Number, ex.Message)
            End Try
        End If
    End Sub

    Public Sub LlenarCalificacionAlumno()
       
        gridCalificaciones.DataSource = Session("dtCalificaciones")
        gridCalificaciones.DataBind()
        Session("total") = gridCalificaciones.Rows(0).Cells.Count

    End Sub


    Protected Sub gridCalificaciones_RowUpdating(sender As Object, e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gridCalificaciones.RowUpdating
        Dim dt = CType(Session("dtCalificaciones"), DataTable)

        'Update the values.
        Dim row = gridCalificaciones.Rows(e.RowIndex)
        For index = 1 To Convert.ToInt16(Session("total")) - 1
            If index > 4 Then
                dt.Rows(row.DataItemIndex)(index - 1) = (CType((row.Cells(index).Controls(0)), TextBox)).Text
            End If


        Next


        'Reset the edit index.
        gridCalificaciones.EditIndex = -1

        'Bind data to the GridView control.
        LlenarCalificacionAlumno()
    End Sub

    Protected Sub gridCalificaciones_RowEditing(sender As Object, e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gridCalificaciones.RowEditing
        gridCalificaciones.EditIndex = e.NewEditIndex
        LlenarCalificacionAlumno()
    End Sub

    Protected Sub gridCalificaciones_RowCancelingEdit(sender As Object, e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles gridCalificaciones.RowCancelingEdit
        'Reset the edit index.
        gridCalificaciones.EditIndex = -1

        'Bind data to the GridView control.
        LlenarCalificacionAlumno()
    End Sub

End Class

Imports System.Data
Imports ClaseStatusAlumno
Imports Tools

Partial Class StatusAlumno
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                LlenarStatusAlumno()
                EvaluarPantalla(0)

            Catch ex As Exception
                lblEliminado.Text = EvaluarError(Err.Number, ex.Message)
            End Try
        End If

    End Sub

    Public Sub LlenarStatusAlumno()
        Dim dt As New DataTable
        dt = StatusAlumnoGridGet()
        gridStatusAlumno.DataSource = dt
        gridStatusAlumno.DataBind()

        Dim sortedView As New DataView(dt)
        Session("GridDataTable") = sortedView

        Dim sortedView2 As New DataView(dt)
        Session("GridDataTablePaginacion") = sortedView
    End Sub

    Public Sub EvaluarPantalla(ByVal Pantalla As Integer)
        TabContainer1.ActiveTab = TabContainer1.Tabs(Pantalla)
    End Sub

    Public Sub Filtrar()
        Dim dt As New DataTable
        dt = StatusAlumnoGridGet()

        gridStatusAlumno.DataSource = dt
        gridStatusAlumno.DataBind()

        Dim sortedView As New DataView(dt)
        sortedView.RowFilter = "Descripcion LIKE '%" & txtFiltro.Text & "%'"
        gridStatusAlumno.DataSource = sortedView
        gridStatusAlumno.DataBind()
    End Sub

    Protected Sub ibtnCerrarEliminar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnCerrarEliminar.Click
        EvaluarPantalla(0)
    End Sub

    Public Property sortProperty() As SortDirection
        Get
            If ViewState("SortingState") Is Nothing Then
                ViewState("SortingState") = SortDirection.Ascending
            End If
            Return DirectCast(ViewState("SortingState"), SortDirection)
        End Get
        Set(value As SortDirection)
            ViewState("SortingState") = value
        End Set
    End Property

    Protected Sub gridviewSorting_Sorting(sender As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs)
        Try

            Dim sortingDirection As String = String.Empty
            If sortProperty = SortDirection.Ascending Then
                sortProperty = SortDirection.Descending
                sortingDirection = "Desc"
            Else
                sortProperty = SortDirection.Ascending
                sortingDirection = "Asc"
            End If

            Dim sortedView As New DataView(StatusAlumnoGridGet())
            sortedView.Sort = Convert.ToString(e.SortExpression) & " " & sortingDirection
            gridStatusAlumno.DataSource = sortedView
            gridStatusAlumno.DataBind()
            Session("GridDataTablePaginacion") = sortedView
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnOk_Click(sender As Object, e As System.EventArgs) Handles btnOk.Click
        EvaluarPantalla(0)
    End Sub

    Protected Sub btnAddStatusAlumno_Click(sender As Object, e As System.EventArgs) Handles btnAddStatusAlumno.Click
        EvaluarPantalla(1)
        lblStatusAlumno.Text = "Nuevo Status de Alumno"
        Limpiartxt()
    End Sub

    Protected Sub Limpiartxt()
        txtStatusAlumno.Text = Nothing
        txtAbreviacion.Text = Nothing
    End Sub

    Protected Sub btnGuardarStatusAlumno_Click(sender As Object, e As System.EventArgs) Handles btnGuardarStatusAlumno.Click
        Try
            Dim O_Status_Alumno As New ClaseStatusAlumno.O_Status_Alumno()

            O_Status_Alumno.Descripcion = txtStatusAlumno.Text
            O_Status_Alumno.Abreviacion = txtAbreviacion.Text

            If lblStatusAlumno.Text = "Editar Status de Alumno" Then
                Dim valorid As Integer = Int16.Parse(lblValoridStatusAlumno.Text)
                O_Status_Alumno.id_Status_Alumno = valorid
                If StatusAlumnoEdit(O_Status_Alumno) Then
                    Limpiartxt()
                Else
                End If

                lblStatusAlumno.Text = "Nuevo Status de Alumno"
            Else

                If StatusAlumnoSet(O_Status_Alumno) Then
                    Limpiartxt()
                End If
            End If
            LlenarStatusAlumno()
            EvaluarPantalla(0)

        Catch ex As Exception
            lblEliminado.Text = EvaluarError(Err.Number, ex.Message)
        End Try
        Filtrar()
    End Sub

    Protected Sub gridStatusAlumno_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridStatusAlumno.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(1).Visible = False
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(1).Visible = False
        End If
    End Sub

    Protected Sub gridStatusAlumno_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gridStatusAlumno.RowCommand
        Try
            Dim row As GridViewRow = DirectCast(TryCast(e.CommandSource, LinkButton).Parent.Parent, GridViewRow)
            If e.CommandName = "Editar" Then
                lblValoridStatusAlumno.Text = row.Cells(1).Text
                txtStatusAlumno.Text = HttpUtility.HtmlDecode(row.Cells(2).Text)
                txtAbreviacion.Text = HttpUtility.HtmlDecode(row.Cells(3).Text)

                lblStatusAlumno.Text = "Editar Status de Alumno"
                EvaluarPantalla(1)

            End If
            If e.CommandName = "Borrar" Then
                EvaluarPantalla(2)
                lblValoridStatusAlumno.Text = row.Cells(1).Text
                lblDescripcion.Text = row.Cells(2).Text
            End If
        Catch
        End Try
        Filtrar()
    End Sub

    Protected Sub btnFiltrar_Click(sender As Object, e As System.EventArgs) Handles btnFiltrar.Click
        Filtrar()

    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As System.EventArgs) Handles btnCancelar.Click
        EvaluarPantalla(0)
    End Sub

    Protected Sub ibtnCerrarStatusAlumno_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnCerrarStatusAlumno.Click
        EvaluarPantalla(0)
    End Sub

    Protected Sub btnCancelarE_Click(sender As Object, e As System.EventArgs) Handles btnCancelarE.Click
        EvaluarPantalla(0)
    End Sub

    Protected Sub btnEliminar_Click(sender As Object, e As System.EventArgs) Handles btnEliminar.Click
        Try
            Dim O_Status_Alumno As New ClaseStatusAlumno.O_Status_Alumno
            Dim valorid As Integer = Int16.Parse(lblValoridStatusAlumno.Text)
            O_Status_Alumno.id_Status_Alumno = valorid
            If StatusAlumnoDel(O_Status_Alumno) Then
                lblEliminado.Text = "Status de Alumno: " & lblDescripcion.Text & " eliminado correctamente"
                Limpiartxt()
            Else
                lblEliminado.Text = "Status de Alumno: " & lblDescripcion.Text & " no se ha eliminado correctamente"
            End If
            gridStatusAlumno.DataSource = Session("GridDataTablePaginacion")
            gridStatusAlumno.DataBind()

            EvaluarPantalla(3)
        Catch ex As Exception
            lblEliminado.Text = EvaluarError(Err.Number, ex.Message)
        End Try
        Filtrar()
    End Sub

    Protected Sub btnRecargar_Click(sender As Object, e As System.EventArgs) Handles btnRecargar.Click
        LlenarStatusAlumno()
        txtFiltro.Text = Nothing

    End Sub

    Protected Sub btnPaginacion_Click(sender As Object, e As System.EventArgs) Handles btnPaginacion.Click
        Try
            Dim iPageSize As Int32 = gridStatusAlumno.PageSize()
            If Int32.TryParse(txtPaginacion.Text, iPageSize) Then
                gridStatusAlumno.PageSize = iPageSize
                gridStatusAlumno.DataSource = StatusAlumnoGridGet()
                gridStatusAlumno.DataBind()
            End If

        Catch ex As Exception
            lblEliminado.Text = EvaluarError(Err.Number, ex.Message)
        End Try
    End Sub

    Protected Sub gridStatusAlumno_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gridStatusAlumno.PageIndexChanging
        Try
            gridStatusAlumno.PageIndex = e.NewPageIndex
            gridStatusAlumno.DataSource = Session("GridDataTablePaginacion")
            gridStatusAlumno.DataBind()

        Catch ex As Exception
            lblEliminado.Text = EvaluarError(Err.Number, ex.Message)
        End Try
    End Sub

End Class

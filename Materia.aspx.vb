Imports ClaseMateria
Imports Tools
Imports System.Data

Partial Class Materia
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                LlenarMaterias()
                Dim dt As New DataTable
                dt = MateriaGridGet()

                gridPlanE.DataSource = dt
                gridPlanE.DataBind()

                Dim sortedView As New DataView(dt)
                Session("GridDataTable") = sortedView

                Dim sortedView2 As New DataView(dt)
                Session("GridDataTablePaginacion") = sortedView

                EvaluarPantalla(0)

            Catch ex As Exception
                lblEliminado.Text = EvaluarError(Err.Number, ex.Message)
            End Try
        End If

    End Sub

    Public Sub LlenarMaterias()
        gridPlanE.DataSource = MateriaGridGet()
        gridPlanE.DataBind()
    End Sub

    Protected Sub gridPlanE_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gridPlanE.PageIndexChanging
        Try
            gridPlanE.PageIndex = e.NewPageIndex
            gridPlanE.DataSource = Session("GridDataTablePaginacion")
            gridPlanE.DataBind()

        Catch ex As Exception
            lblEliminado.Text = EvaluarError(err.Number, ex.Message)
        End Try

    End Sub

    Sub gridPlanE_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles gridPlanE.RowCommand
        Try
            Dim row As GridViewRow = DirectCast(TryCast(e.CommandSource, LinkButton).Parent.Parent, GridViewRow)
            If e.CommandName = "Editar" Then
                lblValoridMateria.Text = row.Cells(1).Text
                txtCreditos.Text = HttpUtility.HtmlDecode(row.Cells(4).Text)
                txtHoras.Text = HttpUtility.HtmlDecode(row.Cells(3).Text)
                txtMateria.Text = HttpUtility.HtmlDecode(row.Cells(2).Text)

                lblMateria.Text = "Editar Materia"
                EvaluarPantalla(1)

            End If
            If e.CommandName = "Borrar" Then
                EvaluarPantalla(2)
                lblValoridMateria.Text = row.Cells(1).Text
                lblMateriaDel.Text = row.Cells(2).Text
            End If
        Catch
        End Try
        Filtrar()
    End Sub

    Protected Sub gridPlanE_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridPlanE.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(1).Visible = False
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(1).Visible = False
        End If
    End Sub

    Sub Limpiartxt()
        txtCreditos.Text = ""
        txtMateria.Text = ""
        txtHoras.Text = ""
    End Sub

    Protected Sub btnPaginacion_Click(sender As Object, e As EventArgs) Handles btnPaginacion.Click
        Try
            Dim iPageSize As Int32 = gridPlanE.PageSize
            If Int32.TryParse(txtPaginacion.Text, iPageSize) Then
                gridPlanE.PageSize = iPageSize
                gridPlanE.DataSource = MateriaGridGet()
                gridPlanE.DataBind()
            End If

        Catch ex As Exception
            lblEliminado.Text = EvaluarError(Err.Number, ex.Message)
        End Try

    End Sub

    Protected Sub ibtnCerrarMateria_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnCerrarMateria.Click
        EvaluarPantalla(0)
    End Sub

    Public Sub MandarError(ByVal NumError As Integer, Mensaje As String)
        EvaluarPantalla(3)
        lblEliminado.Text = EvaluarError(Err.Number, Mensaje)
    End Sub

    Protected Sub ibtnCerrarEliminar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnCerrarEliminar.Click
        EvaluarPantalla(0)
    End Sub

    Public Sub EvaluarPantalla(ByVal Pantalla As Integer)
        TabContainer1.ActiveTab = TabContainer1.Tabs(Pantalla)
    End Sub

    Protected Sub btnFiltrar_Click(sender As Object, e As System.EventArgs) Handles btnFiltrar.Click
        Filtrar()
    End Sub

    Public Sub Filtrar()
        Dim dt As New DataTable
        dt = MateriaGridGet()

        gridPlanE.DataSource = dt
        gridPlanE.DataBind()

        Dim sortedView As New DataView(dt)

        sortedView.RowFilter = "Nombre_Materia LIKE '%" & txtFiltro.Text & "%'"

        gridPlanE.DataSource = sortedView
        gridPlanE.DataBind()
    End Sub

    Protected Sub btnRecargar_Click(sender As Object, e As System.EventArgs) Handles btnRecargar.Click
        LlenarMaterias()
        txtFiltro.Text = ""
    End Sub

    Protected Sub btnAddMateria_Click(sender As Object, e As System.EventArgs) Handles btnAddMateria.Click
        EvaluarPantalla(1)
        Limpiartxt()
    End Sub

    Protected Sub btnGuardarMateria_Click(sender As Object, e As System.EventArgs) Handles btnGuardarMateria.Click
        Try
            Dim O_Materia As New ClaseMateria.O_Materia

            O_Materia.Nombre_Materia = txtMateria.Text
            O_Materia.Horas = Convert.ToDecimal(txtHoras.Text)
            O_Materia.Creditos = Convert.ToDecimal(txtCreditos.Text)
            If lblMateria.Text = "Editar Materia" Then
                Dim valorid As Integer = Int16.Parse(lblValoridMateria.Text)
                O_Materia.id_Materia = valorid
                If MateriaEdit(O_Materia) Then
                    Limpiartxt()
                Else
                End If
                gridPlanE.DataSource = MateriaGridGet()
                gridPlanE.DataBind()
                lblMateria.Text = "Nueva Materia"
            Else
                If String.IsNullOrEmpty(txtHoras.Text) Then
                    txtHoras.Text = "0"
                End If
                If String.IsNullOrEmpty(txtCreditos.Text) Then
                    txtCreditos.Text = "0"
                End If
                If MateriaSet(O_Materia) Then
                    Limpiartxt()
                End If
            End If
            gridPlanE.DataSource = MateriaGridGet()
            gridPlanE.DataBind()
            EvaluarPantalla(0)

        Catch ex As Exception
            lblEliminado.Text = EvaluarError(Err.Number, ex.Message)
        End Try
        Filtrar()
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As System.EventArgs) Handles btnCancelar.Click
        EvaluarPantalla(0)
    End Sub

    Protected Sub btnCancelarE_Click(sender As Object, e As System.EventArgs) Handles btnCancelarE.Click
        EvaluarPantalla(0)
    End Sub

    Protected Sub btnEliminar_Click(sender As Object, e As System.EventArgs) Handles btnEliminar.Click
        Try
            Dim O_Materia As New ClaseMateria.O_Materia
            Dim valorid As Integer = Int16.Parse(lblValoridMateria.Text)
            O_Materia.id_Materia = valorid
            If MateriaDel(O_Materia) Then
                lblEliminado.Text = "Materia: " & lblMateriaDel.Text & " eliminado correctamente"
                Limpiartxt()
            Else
                lblEliminado.Text = "Materia: " & lblMateriaDel.Text & " no se a eliminado correctamente"
            End If
            gridPlanE.DataSource = Session("GridDataTablePaginacion")
            gridPlanE.DataBind()

            EvaluarPantalla(3)
        Catch ex As Exception
            lblEliminado.Text = EvaluarError(Err.Number, ex.Message)
        End Try
        Filtrar()
    End Sub

    Protected Sub btnOk_Click(sender As Object, e As System.EventArgs) Handles btnOk.Click
        EvaluarPantalla(0)
    End Sub

    Protected Sub gridviewSorting_Sorting(sender As Object, e As GridViewSortEventArgs)
        Try

            Dim sortingDirection As String = String.Empty
            If sortProperty = SortDirection.Ascending Then
                sortProperty = SortDirection.Descending
                sortingDirection = "Desc"
            Else
                sortProperty = SortDirection.Ascending
                sortingDirection = "Asc"
            End If

            Dim sortedView As New DataView(MateriaGridGet())
            sortedView.Sort = Convert.ToString(e.SortExpression) & " " & sortingDirection
            gridPlanE.DataSource = sortedView
            gridPlanE.DataBind()
            Session("GridDataTablePaginacion") = sortedView
        Catch ex As Exception

        End Try

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

    Protected Sub btnReporte_Click(sender As Object, e As System.EventArgs) Handles btnReporte.Click
        Session("ReporteDt_Reportes") = MateriaGridGet()
        Session("ReporteNombre_Reportes") = "Reportes/MateriaReporte.rdlc"
        Response.Redirect("Reportes.aspx")
    End Sub
End Class


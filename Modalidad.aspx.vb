Imports System.Data
Imports ClaseModalidad
Imports Tools
Partial Class Modalidad
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                LlenarModalidad()
                EvaluarPantalla(0)

            Catch ex As Exception
                lblEliminado.Text = EvaluarError(Err.Number, ex.Message)
            End Try
        End If

    End Sub

    Public Sub LlenarModalidad()
        Dim dt As New DataTable
        dt = ModalidadGridGet()
        gridModalidad.DataSource = dt
        gridModalidad.DataBind()

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
        dt = ModalidadGridGet()

        gridModalidad.DataSource = dt
        gridModalidad.DataBind()

        Dim sortedView As New DataView(dt)
        sortedView.RowFilter = "Nombre_Modalidad LIKE '%" & txtFiltro.Text & "%'"
        gridModalidad.DataSource = sortedView
        gridModalidad.DataBind()
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

            Dim sortedView As New DataView(ModalidadGridGet())
            sortedView.Sort = Convert.ToString(e.SortExpression) & " " & sortingDirection
            gridModalidad.DataSource = sortedView
            gridModalidad.DataBind()
            Session("GridDataTablePaginacion") = sortedView
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnOk_Click(sender As Object, e As System.EventArgs) Handles btnOk.Click
        EvaluarPantalla(0)
    End Sub

    Protected Sub btnAddModalidad_Click(sender As Object, e As System.EventArgs) Handles btnAddModalidad.Click
        EvaluarPantalla(1)
        lblModalidad.Text = "Nueva Modalidad"
        Limpiartxt()
    End Sub

    Protected Sub Limpiartxt()
        txtModalidad.Text = Nothing
    End Sub

    Protected Sub btnGuardarModalidad_Click(sender As Object, e As System.EventArgs) Handles btnGuardarModalidad.Click
        Try
            Dim O_Modalidad As New ClaseModalidad.O_Modalidad()

            O_Modalidad.Nombre_Modalidad = txtModalidad.Text

            If lblModalidad.Text = "Editar Modalidad" Then
                Dim valorid As Integer = Int16.Parse(lblValoridModalidad.Text)
                O_Modalidad.id_Modalidad = valorid
                If ModalidadEdit(O_Modalidad) Then
                    Limpiartxt()
                Else
                End If

                lblModalidad.Text = "Nueva Modalidad"
            Else

                If ModalidadSet(O_Modalidad) Then
                    Limpiartxt()
                End If
            End If
            LlenarModalidad()
            EvaluarPantalla(0)

        Catch ex As Exception
            lblEliminado.Text = EvaluarError(Err.Number, ex.Message)
        End Try
        Filtrar()
    End Sub

    Protected Sub gridModalidad_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridModalidad.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(1).Visible = False
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(1).Visible = False
        End If
    End Sub

    Protected Sub gridModalidad_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gridModalidad.RowCommand
        Try
            Dim row As GridViewRow = DirectCast(TryCast(e.CommandSource, LinkButton).Parent.Parent, GridViewRow)
            If e.CommandName = "Editar" Then
                lblValoridModalidad.Text = row.Cells(1).Text
                txtModalidad.Text = HttpUtility.HtmlDecode(row.Cells(2).Text)


                lblModalidad.Text = "Editar Modalidad"
                EvaluarPantalla(1)

            End If
            If e.CommandName = "Borrar" Then
                EvaluarPantalla(2)
                lblValoridModalidad.Text = row.Cells(1).Text
                lblModalidadEliminar.Text = row.Cells(2).Text
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

    Protected Sub ibtnCerrarModalidad_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnCerrarModalidad.Click
        EvaluarPantalla(0)
    End Sub

    Protected Sub btnCancelarE_Click(sender As Object, e As System.EventArgs) Handles btnCancelarE.Click
        EvaluarPantalla(0)
    End Sub

    Protected Sub btnEliminar_Click(sender As Object, e As System.EventArgs) Handles btnEliminar.Click
        Try
            Dim O_Modalidad As New ClaseModalidad.O_Modalidad
            Dim valorid As Integer = Int16.Parse(lblValoridModalidad.Text)
            O_Modalidad.id_Modalidad = valorid
            If ModalidadDel(O_Modalidad) Then
                lblEliminado.Text = "Modalidad: " & lblModalidadEliminar.Text & " eliminado correctamente"
                Limpiartxt()
            Else
                lblEliminado.Text = "Modalidad: " & lblModalidadEliminar.Text & " no se ha eliminado correctamente"
            End If
            gridModalidad.DataSource = Session("GridDataTablePaginacion")
            gridModalidad.DataBind()

            EvaluarPantalla(3)
        Catch ex As Exception
            lblEliminado.Text = EvaluarError(Err.Number, ex.Message)
        End Try
        Filtrar()
    End Sub

    Protected Sub btnRecargar_Click(sender As Object, e As System.EventArgs) Handles btnRecargar.Click
        LlenarModalidad()
        txtFiltro.Text = Nothing

    End Sub

    Protected Sub btnPaginacion_Click(sender As Object, e As System.EventArgs) Handles btnPaginacion.Click
        Try
            Dim iPageSize As Int32 = gridModalidad.PageSize()
            If Int32.TryParse(txtPaginacion.Text, iPageSize) Then
                gridModalidad.PageSize = iPageSize
                gridModalidad.DataSource = ModalidadGridGet()
                gridModalidad.DataBind()
            End If

        Catch ex As Exception
            lblEliminado.Text = EvaluarError(Err.Number, ex.Message)
        End Try
    End Sub

    Protected Sub gridModalidad_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gridModalidad.PageIndexChanging
        Try
            gridModalidad.PageIndex = e.NewPageIndex
            gridModalidad.DataSource = Session("GridDataTablePaginacion")
            gridModalidad.DataBind()

        Catch ex As Exception
            lblEliminado.Text = EvaluarError(Err.Number, ex.Message)
        End Try
    End Sub

End Class

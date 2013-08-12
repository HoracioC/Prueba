Imports System.Data
Imports ClaseNombreCarrera
Imports Tools
Partial Class NombreCarrera
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                LlenarNombreCarreras()
                EvaluarPantalla(0)

            Catch ex As Exception
                lblEliminado.Text = EvaluarError(Err.Number, ex.Message)
            End Try
        End If

    End Sub

    Public Sub LlenarNombreCarreras()
        Dim dt As New DataTable
        dt = NombreCarreraGridGet()
        gridNombreCarreras.DataSource = dt
        gridNombreCarreras.DataBind()

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
        dt = NombreCarreraGridGet()

        gridNombreCarreras.DataSource = dt
        gridNombreCarreras.DataBind()

        Dim sortedView As New DataView(dt)
        sortedView.RowFilter = "Nombre_Carrera LIKE '%" & txtFiltro.Text & "%'"
        gridNombreCarreras.DataSource = sortedView
        gridNombreCarreras.DataBind()
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

            Dim sortedView As New DataView(NombreCarreraGridGet())
            sortedView.Sort = Convert.ToString(e.SortExpression) & " " & sortingDirection
            gridNombreCarreras.DataSource = sortedView
            gridNombreCarreras.DataBind()
            Session("GridDataTablePaginacion") = sortedView
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnOk_Click(sender As Object, e As System.EventArgs) Handles btnOk.Click
        EvaluarPantalla(0)
    End Sub

    Protected Sub btnAddNombreCarrera_Click(sender As Object, e As System.EventArgs) Handles btnAddNombreCarrera.Click
        EvaluarPantalla(1)
        lblNombreCarrera.Text = "Nuevo Nombre de Carrera"
        Limpiartxt()
    End Sub

    Protected Sub Limpiartxt()
        txtNombreCarrera.Text = ""
        txtAbreviatura.Text = Nothing
    End Sub

    Protected Sub btnGuardarNombreCarrera_Click(sender As Object, e As System.EventArgs) Handles btnGuardarNombreCarrera.Click
        Try
            Dim O_Nombre_Carrera As New ClaseNombreCarrera.O_Nombre_Carrera()

            O_Nombre_Carrera.Nombre_Carrera = txtNombreCarrera.Text
            O_Nombre_Carrera.Abreviatura = txtAbreviatura.Text

            If lblNombreCarrera.Text = "Editar Nombre de Carrera" Then
                Dim valorid As Integer = Int16.Parse(lblValoridNombreCarrera.Text)
                O_Nombre_Carrera.id_Nombre_Carrera = valorid
                If NombreCarreraEdit(O_Nombre_Carrera) Then
                    Limpiartxt()
                Else
                End If
                
                lblNombreCarrera.Text = "Nuevo Nombre de Carrera"
            Else

                If NombreCarreraSet(O_Nombre_Carrera) Then
                    Limpiartxt()
                End If
            End If
            LlenarNombreCarreras()
            EvaluarPantalla(0)

        Catch ex As Exception
            lblEliminado.Text = EvaluarError(Err.Number, ex.Message)
        End Try
        Filtrar()
    End Sub

    Protected Sub gridNombreCarreras_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridNombreCarreras.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(1).Visible = False
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(1).Visible = False
        End If
    End Sub

    Protected Sub gridNombreCarreras_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gridNombreCarreras.RowCommand
        Try
            Dim row As GridViewRow = DirectCast(TryCast(e.CommandSource, LinkButton).Parent.Parent, GridViewRow)
            If e.CommandName = "Editar" Then
                lblValoridNombreCarrera.Text = row.Cells(1).Text
                txtNombreCarrera.Text = HttpUtility.HtmlDecode(row.Cells(2).Text)
                txtAbreviatura.Text = HttpUtility.HtmlDecode(row.Cells(3).Text)

                lblNombreCarrera.Text = "Editar Nombre de Carrera"
                EvaluarPantalla(1)

            End If
            If e.CommandName = "Borrar" Then
                EvaluarPantalla(2)
                lblValoridNombreCarrera.Text = row.Cells(1).Text
                lblNombreCarreraDel.Text = row.Cells(2).Text
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

    Protected Sub ibtnCerrarNombreCarrera_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnCerrarNombreCarrera.Click
        EvaluarPantalla(0)
    End Sub

    Protected Sub btnCancelarE_Click(sender As Object, e As System.EventArgs) Handles btnCancelarE.Click
        EvaluarPantalla(0)
    End Sub

    Protected Sub btnEliminar_Click(sender As Object, e As System.EventArgs) Handles btnEliminar.Click
        Try
            Dim O_Nombre_Carrera As New ClaseNombreCarrera.O_Nombre_Carrera
            Dim valorid As Integer = Int16.Parse(lblValoridNombreCarrera.Text)
            O_Nombre_Carrera.id_Nombre_Carrera = valorid
            If NombreCarreraDel(O_Nombre_Carrera) Then
                lblEliminado.Text = "Nombre de Carrera: " & lblNombreCarreraDel.Text & " eliminado correctamente"
                Limpiartxt()
            Else
                lblEliminado.Text = "Materia: " & lblNombreCarreraDel.Text & " no se a eliminado correctamente"
            End If
            gridNombreCarreras.DataSource = Session("GridDataTablePaginacion")
            gridNombreCarreras.DataBind()

            EvaluarPantalla(3)
        Catch ex As Exception
            lblEliminado.Text = EvaluarError(Err.Number, ex.Message)
        End Try
        Filtrar()
    End Sub

    Protected Sub btnRecargar_Click(sender As Object, e As System.EventArgs) Handles btnRecargar.Click
        LlenarNombreCarreras()
        txtFiltro.Text = Nothing

    End Sub

    Protected Sub btnPaginacion_Click(sender As Object, e As System.EventArgs) Handles btnPaginacion.Click
        Try
            Dim iPageSize As Int32 = gridNombreCarreras.PageSize()
            If Int32.TryParse(txtPaginacion.Text, iPageSize) Then
                gridNombreCarreras.PageSize = iPageSize
                gridNombreCarreras.DataSource = NombreCarreraGridGet()
                gridNombreCarreras.DataBind()
            End If

        Catch ex As Exception
            lblEliminado.Text = EvaluarError(Err.Number, ex.Message)
        End Try
    End Sub

    Protected Sub gridNombreCarreras_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gridNombreCarreras.PageIndexChanging
        Try
            gridNombreCarreras.PageIndex = e.NewPageIndex
            gridNombreCarreras.DataSource = Session("GridDataTablePaginacion")
            gridNombreCarreras.DataBind()

        Catch ex As Exception
            lblEliminado.Text = EvaluarError(Err.Number, ex.Message)
        End Try
    End Sub
End Class

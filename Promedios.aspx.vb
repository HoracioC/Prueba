Imports System.Data
Imports ClaseReporteCalificaciones
Imports CrystalDecisions


Partial Class Promedios
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim Dt As New DataSet

        Dt.Tables.Add(ReporteCalificacionesGridGet())
        Dim reporte2 As New CrystalReport
        reporte2.SetDataSource(Dt)
        CrystalReportViewer1.ReportSource = reporte2

    End Sub
End Class

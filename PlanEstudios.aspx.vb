Imports System.Data
Imports ClasePlanEstudios
Imports CrystalDecisions


Partial Class PlanEstudios
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim Dt As New DataSet

        Dt.Tables.Add(PlanEstudiosGet())
        Dim reporte2 As New PlanEstudiosRPT
        reporte2.SetDataSource(Dt)
        CrystalReportViewer1.ReportSource = reporte2

    End Sub
End Class

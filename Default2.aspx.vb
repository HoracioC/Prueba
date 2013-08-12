Imports System.Data
Partial Class Default2
    Inherits System.Web.UI.Page


    Protected Sub TaskGridView_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles TaskGridView.PageIndexChanging
        TaskGridView.PageIndex = e.NewPageIndex
        'Bind data to the GridView control.
        BindData()
    End Sub

    Protected Sub TaskGridView_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs) Handles TaskGridView.RowEditing
        'Set the edit index.
        TaskGridView.EditIndex = e.NewEditIndex
        'Bind data to the GridView control.
        BindData()
    End Sub

    
    Protected Sub TaskGridView_RowUpdating(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs) Handles TaskGridView.RowUpdating
        'Retrieve the table from the session object.
        Dim dt = CType(Session("TaskTable"), DataTable)

        'Update the values.
        Dim row = TaskGridView.Rows(e.RowIndex)
        dt.Rows(row.DataItemIndex)("Id") = (CType((row.Cells(1).Controls(0)), TextBox)).Text
        dt.Rows(row.DataItemIndex)("Description") = (CType((row.Cells(2).Controls(0)), TextBox)).Text
        dt.Rows(row.DataItemIndex)("IsComplete") = (CType((row.Cells(3).Controls(0)), CheckBox)).Checked

        'Reset the edit index.
        '

        'Bind data to the GridView control.
        BindData()
        TaskGridView.EditIndex = -1
    End Sub

    Private Sub BindData()
        TaskGridView.DataSource = Session("TaskTable")
        TaskGridView.DataBind()
    End Sub

    Protected Sub TaskGridView_RowCancelingEdit(sender As Object, e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles TaskGridView.RowCancelingEdit
        'Reset the edit index.
        TaskGridView.EditIndex = -1
        'Bind data to the GridView control.
        BindData()
    End Sub

    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            ' Create a new table.
            Dim taskTable As New DataTable("TaskList")

            ' Create the columns.
            taskTable.Columns.Add("Id", GetType(Integer))
            taskTable.Columns.Add("Description", GetType(String))
            taskTable.Columns.Add("IsComplete", GetType(Boolean))

            'Add data to the new table.
            For i = 0 To 19
                Dim tableRow = taskTable.NewRow()
                tableRow("Id") = i
                tableRow("Description") = "Task " + i.ToString()
                tableRow("IsComplete") = False
                taskTable.Rows.Add(tableRow)
            Next

            'Persist the table in the Session object.
            Session("TaskTable") = taskTable

            'Bind data to the GridView control.
            BindData()
        End If
    End Sub
End Class

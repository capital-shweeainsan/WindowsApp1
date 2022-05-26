Imports System
Imports System.Data.SqlClient

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Ctrl_KeyUp(sender As Object, e As KeyEventArgs) Handles txtProjectCD.KeyUp, txtCostCD.KeyUp, txtBrandCD.KeyUp, cboYear.KeyUp, cboSeason.KeyUp
        If e.KeyValue = Keys.Enter Then
            Me.SelectNextControl(sender, True, True, True, True)
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim constring As String = "Data Source=163.43.105.244;Initial Catalog=Accele_Test;User ID=sa;Password=admin123456!;Connect Timeout=30"
        Using con As New SqlConnection(constring)
            con.Open()
            Using cmd As New SqlCommand("Select *  from  D_Cost where ([CostCD] Is Null OR ([CostCD] ='" & txtCostCD.Text & "'))", con)
                cmd.CommandType = CommandType.Text
                Using sda As New SqlDataAdapter(cmd)
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        DataGridView1.DataSource = dt
                        con.Close()
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Application.Exit()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtCostCD.Clear()
        cboYear.SelectedValue = -1
        cboSeason.SelectedValue = -1
        txtBrandCD.Clear()
        txtProjectCD.Clear()
        txtCostCD.Select()
    End Sub
End Class

Imports System
Imports System.Data.SqlClient
Imports System.Drawing.Printing

Public Class CRUDTesting
    Dim connetionString As String
    Dim Connection As New SqlConnection("Data Source=163.43.105.244;Initial Catalog=Accele_Test;User ID=sa;Password=admin123456!;Connect Timeout=30")
    Dim cmd As SqlCommand
    Dim adapter As SqlDataAdapter
    Dim dt As DataTable
    Dim reader As SqlDataReader

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim insertQuery As String = "Insert Into M_Rate([BrandCD],[CategoryCD],[Rate]) values('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "')"
        Try
            ExecuteQuery(insertQuery)
            MsgBox("Data Inserted")
            TextBox1.Clear()
            TextBox2.Clear()
            TextBox3.Clear()
            TextBox1.Select()
            BindGrid()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ExecuteQuery(query As String)
        Dim command As New SqlCommand(query, Connection)
        Connection.Open()
        command.ExecuteNonQuery()
        Connection.Close()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Select()
        BindGrid()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox1.Select()
        Application.Exit()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If (MessageBox.Show("Are you sure to delete?", "Delete document", MessageBoxButtons.YesNo) = DialogResult.Yes) Then
            Dim deleteQuery As String = "Delete from  M_Rate where BrandCD='" & TextBox1.Text & "'"
            Try
                'reader = cmd.ExecuteReader
                ExecuteQuery(deleteQuery)
                MsgBox("Data Deleted")
                BindGrid()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub Ctrl_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox3.KeyUp, TextBox2.KeyUp, TextBox1.KeyUp
        If e.KeyValue = Keys.Enter Then
            Me.SelectNextControl(sender, True, True, True, True)
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim constring As String = "Data Source=163.43.105.244;Initial Catalog=Accele_Test;User ID=sa;Password=admin123456!;Connect Timeout=30"
        Using con As New SqlConnection(constring)
            con.Open()
            Using cmd As New SqlCommand("Select *  from  M_Rate where ([BrandCD] Is Null OR ([BrandCD] ='" & TextBox1.Text & "')) And ([CategoryCD] Is Null OR ([CategoryCD] like'%" & TextBox2.Text & "%')) AND ([Rate] Is Null OR ([Rate] like '%" & TextBox3.Text & "%'))", con)
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

    Private Sub BindGrid()
        Dim constring As String = "Data Source=163.43.105.244;Initial Catalog=Accele_Test;User ID=sa;Password=admin123456!;Connect Timeout=30"
        Using con As New SqlConnection(constring)
            con.Open()
            Using cmd As New SqlCommand("Select *  from  M_Rate", con)
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
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim updateQuery As String = "Update  M_Rate SET BrandCD='" & TextBox1.Text & "',CategoryCD='" & TextBox2.Text & "',Rate='" & TextBox3.Text & "' where BrandCD='" & TextBox1.Text & "'"
        Try
            'reader = cmd.ExecuteReader
            ExecuteQuery(updateQuery)
            MsgBox("Data Updated")
            BindGrid()
            TextBox1.Select()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class

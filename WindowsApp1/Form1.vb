Imports System
Imports System.Data.SqlClient
Imports System.Drawing.Printing

Public Class Form1
    Dim connetionString As String
    Dim Connection As SqlConnection
    Dim cmd As SqlCommand
    Dim adapter As SqlDataAdapter
    Dim dt As DataTable
    Dim i As String
    Dim WithEvents pd As New PrintDocument
    Dim ppd As New PrintPreviewDialog
    Dim longpaper As Integer

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        connetionString = "Data Source=163.43.105.244;Initial Catalog=Accele_Test;User ID=sa;Password=admin123456!;Connect Timeout=30"
        Connection = New SqlConnection(connetionString)
        Connection.Open()
        cmd = New SqlCommand("Insert Into M_Rate([BrandCD],[CategoryCD],[Rate]) values(@BrandCD,@CategoryCD,@Rate)", Connection)
        cmd.Parameters.AddWithValue("@BrandCD", SqlDbType.VarChar).Value = TextBox1.Text
        cmd.Parameters.AddWithValue("@CategoryCD", SqlDbType.VarChar).Value = TextBox2.Text
        cmd.Parameters.AddWithValue("@Rate", SqlDbType.VarChar).Value = TextBox3.Text
        Try
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            Connection.Close()
            MsgBox("Information stored in database")
            TextBox1.Clear()
            TextBox2.Clear()
            TextBox3.Clear()
            TextBox1.Select()
            BindGrid()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
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
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If (MessageBox.Show("Are you sure to delete?", "Delete document", MessageBoxButtons.YesNo) = DialogResult.Yes) Then
            connetionString = "Data Source=163.43.105.244;Initial Catalog=Accele_Test;User ID=sa;Password=admin123456!;Connect Timeout=30"
            Connection = New SqlConnection(connetionString)
            Connection.Open()
            cmd = New SqlCommand("Delete from  M_Rate where BrandCD=@BrandCD and CategoryCD=@CategoryCD and Rate=@Rate", Connection)
            cmd.Parameters.AddWithValue("@BrandCD", SqlDbType.VarChar).Value = TextBox1.Text
            cmd.Parameters.AddWithValue("@CategoryCD", SqlDbType.VarChar).Value = TextBox2.Text
            cmd.Parameters.AddWithValue("@Rate", SqlDbType.VarChar).Value = TextBox3.Text
            Try
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                Connection.Close()
                MsgBox("Delete Information in database")
                TextBox1.Clear()
                TextBox2.Clear()
                TextBox3.Clear()
                TextBox1.Select()
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
        BindGrid()
    End Sub

    Private Sub BindGrid()
        Dim constring As String = "Data Source=163.43.105.244;Initial Catalog=Accele_Test;User ID=sa;Password=admin123456!;Connect Timeout=30"
        Using con As New SqlConnection(constring)
            con.Open()
            Using cmd As New SqlCommand("Select *  from  M_Rate", con)
                cmd.CommandType = CommandType.Text
                Dim wtype As String = ""
                If RadioButton1.Checked = True Then
                    wtype = "Allowed"
                Else
                    wtype = "Not Allowed"
                End If
                cmd.Parameters.AddWithValue("@BrandCD", SqlDbType.VarChar).Value = TextBox1.Text
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

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        i = DataGridView1.SelectedCells.Item(0).Value.ToString()
        'MessageBox.Show(i)
        TextBox1.Text = i.ToString()
        TextBox2.Text = i.ToString()
        TextBox3.Text = i.ToString()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        connetionString = "Data Source=163.43.105.244;Initial Catalog=Accele_Test;User ID=sa;Password=admin123456!;Connect Timeout=30"
        Connection = New SqlConnection(connetionString)
        Connection.Open()
        cmd = New SqlCommand("Update  M_Rate SET BrandCD='" + TextBox1.Text + "',CategoryCD='" + TextBox2.Text + "'", Connection)
        cmd.Parameters.AddWithValue("@BrandCD", SqlDbType.VarChar).Value = TextBox1.Text
        cmd.Parameters.AddWithValue("@CategoryCD", SqlDbType.VarChar).Value = TextBox2.Text
        cmd.Parameters.AddWithValue("@Rate", SqlDbType.VarChar).Value = TextBox3.Text
        Try
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            Connection.Close()
            MsgBox("Update Information in database")
            TextBox1.Clear()
            TextBox2.Clear()
            TextBox3.Clear()
            TextBox1.Select()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim pageSetup As New PageSettings
        pageSetup.PaperSize = New PaperSize("Custom", 250, 500)
        pd.DefaultPageSettings = pageSetup
    End Sub

    Private Sub PrintPage(sender As Object, e As PrintPageEventArgs) Handles pd.PrintPage
        Dim pageSetup As New PageSettings
        pageSetup.PaperSize = New PaperSize("Custom", 250, 500)
        pd.DefaultPageSettings = pageSetup

        Dim F8 As New Font("Calibri", 8, FontStyle.Regular)
        Dim F10 As New Font("Calibri", 10, FontStyle.Regular)
        Dim F10b As New Font("Calibri", 10, FontStyle.Bold)
        Dim F14 As New Font("Calibri", 14, FontStyle.Bold)

        Dim leftmargin As Integer = pd.DefaultPageSettings.Margins.Left
        Dim centertmargin As Integer = pd.DefaultPageSettings.PaperSize.Width / 2
        Dim rightmargin As Integer = pd.DefaultPageSettings.PaperSize.Width

        Dim right As New StringFormat
        Dim center As New StringFormat
        right.Alignment = StringAlignment.Far
        center.Alignment = StringAlignment.Center

        Dim line As String
        line = "--------------------------------------------"
        e.Graphics.DrawString("Capital Knowledge", F14, Brushes.Black, centertmargin, 5, center)
        e.Graphics.DrawString("La Pyae Won Palaza", F10, Brushes.Black, centertmargin, 25, center)
        e.Graphics.DrawString("Tel +959969695317", F8, Brushes.Black, centertmargin, 40, center)

        e.Graphics.DrawString("Invoice ID", F8, Brushes.Black, 0, 60)
        e.Graphics.DrawString(":", F8, Brushes.Black, 50, 60)
        e.Graphics.DrawString("KFGS1564", F8, Brushes.Black, 70, 60)

        e.Graphics.DrawString(line, F8, Brushes.Black, 70, 60)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        ppd.Document = pd
        ppd.ShowDialog()
    End Sub
End Class

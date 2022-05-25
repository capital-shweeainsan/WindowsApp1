Imports System
Imports System.Data.SqlClient
Imports System.IO

Public Class Form1
    Dim connetionString As String
    Dim Connection As SqlConnection
    Dim cmd As SqlCommand
    Dim ms As New MemoryStream

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        OpenFileDialog1.FileName = ""
        OpenFileDialog1.Filter = "JPG File(*.Jpg)|*.jpg"
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            PictureBox1.Image = Image.FromFile(OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        connetionString = "Data Source=163.43.105.244;Initial Catalog=Accele_Test;User ID=sa;Password=admin123456!;Connect Timeout=30"
        Connection = New SqlConnection(connetionString)
        Connection.Open()
        cmd = New SqlCommand("Insert Into Testing_Table([ID],[Name],[Image]) values(@id,@name,@image)", Connection)
        PictureBox1.Image.Save(ms, PictureBox1.Image.RawFormat)
        cmd.Parameters.AddWithValue("@id", txtID.Text)
        cmd.Parameters.AddWithValue("@name", txtName.Text)
        cmd.Parameters.AddWithValue("@image", ms.ToArray)
        cmd.ExecuteNonQuery()
        Connection.Close()
        MessageBox.Show("Image Insert Successfully")
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        connetionString = "Data Source=163.43.105.244;Initial Catalog=Accele_Test;User ID=sa;Password=admin123456!;Connect Timeout=30"
        Connection = New SqlConnection(connetionString)
        Connection.Open()
        cmd = New SqlCommand("Select * from  Testing_Table where ID=@id And Name=@name", Connection)
        cmd.Parameters.AddWithValue("@id", txtID.Text)
        cmd.Parameters.AddWithValue("@name", txtName.Text)
        Using sda As New SqlDataAdapter(cmd)
            Using dt As New DataTable()
                sda.Fill(dt)
                Connection.Close()
            End Using
        End Using
    End Sub
End Class

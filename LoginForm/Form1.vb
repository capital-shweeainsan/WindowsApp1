﻿Imports System
Imports System.Data.SqlClient
Public Class Form1
    Dim username As String
    Dim password As String
    Dim connetionString As String
    Dim Connection As SqlConnection
    Dim cmd As SqlCommand
    Dim adapter As SqlDataAdapter
    Dim dt As DataTable
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtName.Select()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Ctrl_KeyUp(sender As Object, e As KeyEventArgs) Handles txtPassword.KeyUp, txtName.KeyUp
        If e.KeyValue = Keys.Enter Then
            Me.SelectNextControl(sender, True, True, True, True)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If txtName.Text = Nothing Or txtPassword.Text = Nothing Then
            MsgBox("Enter Credential", MsgBoxStyle.Exclamation)
            txtName.Select()
            Return
        Else
            connetionString = "Data Source=163.43.105.244;Initial Catalog=Accele_Test;User ID=sa;Password=admin123456!;Connect Timeout=30"
            Connection = New SqlConnection(connetionString)
            Connection.Open()
            cmd = New SqlCommand("Select * from M_User where [UserName]='" & txtName.Text & "' And [Password]='" & txtPassword.Text & "'", Connection)

            Using sda As New SqlDataAdapter(cmd)
                Using dt As New DataTable()
                    sda.Fill(dt)
                    If (dt.Rows.Count > 0) Then
                        MsgBox("Login Success", MsgBoxStyle.Information)
                    Else
                        MsgBox("Check UserName and Password", MsgBoxStyle.Critical)
                        txtName.Select()
                    End If
                    Connection.Close()
                End Using
            End Using
        End If
    End Sub

    Private Sub txtName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtName.KeyDown
        If e.KeyValue = Keys.Enter Then
            If String.IsNullOrWhiteSpace(txtName.Text) Then
                MessageBox.Show("Please Input User Name")
                txtName.Select()
            End If
        End If
    End Sub

    Private Sub txtPassword_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPassword.KeyDown
        If e.KeyValue = Keys.Enter Then
            If String.IsNullOrWhiteSpace(txtPassword.Text) Then
                MessageBox.Show("Please Input Password")
                txtPassword.Select()
            End If
        End If
    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If (e.KeyData = Keys.F1) Then
            Me.Close()
        End If
    End Sub
End Class

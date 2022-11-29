Imports System.Data.SqlClient
Public Class Form5
    Dim Z As String

    Private cn As Object
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Text = "BUSCAR"
        If Trim(TextBox1.Text) = "" Then
            MsgBox("INGREAR DATO", vbInformation, "AVISO")
        Else

            Dim conexion As String
            conexion = "Data Source=LAPTOPHP\MSSQLSERVER01;Initial Catalog=libreria;Integrated Security=True"
            Dim cn As New SqlConnection
            cn.ConnectionString = conexion

            Dim adaptador As New SqlDataAdapter(" SELECT * FROM libro WHERE Id_libro=" & TextBox1.Text & "", cn)
            Dim ds As New DataSet
            adaptador.Fill(ds, "datos")

            If ds.Tables("datos").Rows.Count > 0 Then
                TextBox2.Text = ds.Tables("datos").Rows(0).Item(1).ToString
                TextBox3.Text = ds.Tables("datos").Rows(0).Item(2).ToString
                TextBox4.Text = ds.Tables("datos").Rows(0).Item(3).ToString
                TextBox5.Text = ds.Tables("datos").Rows(0).Item(4).ToString
                TextBox6.Text = ds.Tables("datos").Rows(0).Item(5).ToString
                TextBox7.Text = ds.Tables("datos").Rows(0).Item(6).ToString
                TextBox8.Text = ds.Tables("datos").Rows(0).Item(7).ToString
                TextBox9.Text = ds.Tables("datos").Rows(0).Item(8).ToString



            Else
                MsgBox("NO EXISTE NINGUN DATO", vbInformation, "AVISO")
            End If
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Me.Text = "AGREGAR"

        Dim conexion As String
        conexion = "Data Source=LAPTOPHP\MSSQLSERVER01;Initial Catalog=libreria;Integrated Security=True"
        Dim cn As New SqlConnection
        cn.ConnectionString = conexion
        cn.Open()
        Dim adaptador2 As New SqlDataAdapter("select Id_libro from libro", cn)
        Dim ds As New DataSet
        adaptador2.Fill(ds, "datos")
        Dim adaptador3 As New SqlDataAdapter(" SELECT TOP 1 (Id_libro) FROM libro order by Id_libro DESC ", cn)
        Dim ds3 As New DataSet
        adaptador3.Fill(ds3, "datos3")
        If ds3.Tables("datos3").Rows.Count > 0 Then
            Z = ds3.Tables("datos3").Rows(0).Item(0).ToString
        End If
        Dim Y As Integer = 0
        For i As Integer = 0 To (Z - 1)
            If TextBox1.Text = ds.Tables("datos").Rows(i).Item(0) Then
                MsgBox("INGRESAR OTRO ID LIBRO ESE YA ESTA REGISTRADO", vbCritical, "AVISO")
                i = Z
            Else
                Y = 1


            End If

        Next

        If Y = 1 Then
            Dim adaptador As New SqlCommand(" insert into libro values(" & TextBox1.Text & "," & TextBox2.Text & ",'" & TextBox3.Text & "','" & TextBox4.Text & "'," & TextBox5.Text & "," & TextBox6.Text & ",'" & TextBox7.Text & "'," & TextBox8.Text & "," & TextBox9.Text & ") ", cn)

            adaptador.ExecuteNonQuery()

            MsgBox("SE REGISTRO CORRECTAMENTE", vbInformation, "AVISO")
            cn.Close()
        End If
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        TextBox7.Text = ""
        TextBox8.Text = ""
        TextBox9.Text = ""

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        Me.Text = "ELIMINAR"
        Dim conexion As String
        conexion = "Data Source=LAPTOPHP\MSSQLSERVER01;Initial Catalog=libreria;Integrated Security=True"
        Dim cn As New SqlConnection
        cn.ConnectionString = conexion
        cn.Open()

        Dim comando As New SqlCommand("delete from libro WHERE Id_libro = " & TextBox1.Text & " ", cn)
        comando.ExecuteNonQuery()
        MsgBox("SE ELIMINO CORRECTAMENTE", vbInformation, "AVISO")
        cn.Close()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Close()
        Form2.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Text = "ACTUALIZAR"
        Dim conexion As String
        conexion = "Data Source=LAPTOPHP\MSSQLSERVER01;Initial Catalog=libreria;Integrated Security=True"
        Dim cn As New SqlConnection
        cn.ConnectionString = conexion
        Try

            Dim adaptador As New SqlCommand("update libro set Codigo_libro = " & TextBox2.Text & ", Titulo= '" & TextBox3.Text & "' , Genero='" & TextBox4.Text & "', Num_Pag = " & TextBox5.Text & ", Año_Edicion = " & TextBox6.Text & ", Lugar_Edicion= '" & TextBox7.Text & "', Precio=" & TextBox8.Text & ", Stock=" & TextBox9.Text & " WHERE Id_libro = " & TextBox1.Text & " ", cn)
            cn.Open()
            adaptador.ExecuteNonQuery()
            MsgBox("SE HAN ACTUALIZADO LOS DATOS", vbInformation, "AVISO")
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox4.Text = ""
            TextBox5.Text = ""
            TextBox6.Text = ""
            TextBox7.Text = ""
            TextBox8.Text = ""
            TextBox9.Text = ""


        Catch ex As Exception
            MsgBox("NO SE PUDO ACTUALIZAR EL REGISTRO", vbCritical, "AVISO")
            cn.Close()
        End Try
    End Sub

    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim conexion As String
        conexion = "Data Source=LAPTOPHP\MSSQLSERVER01;Initial Catalog=libreria;Integrated Security=True"
        Dim cn As New SqlConnection
        cn.ConnectionString = conexion
        cn.Open()

        Dim adaptador As New SqlDataAdapter(" SELECT TOP 1 ( Id_libro) FROM libro ORDER BY  Id_libro DESC", cn)
        Dim ds As New DataSet
        adaptador.Fill(ds, "datos")

        If ds.Tables("datos").Rows.Count > 0 Then
            TextBox1.Text = ds.Tables("datos").Rows(0).Item(0).ToString + 1



        End If
    End Sub
End Class
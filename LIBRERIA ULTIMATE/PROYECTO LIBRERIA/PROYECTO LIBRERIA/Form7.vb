Imports System.Data.SqlClient
Public Class Form7


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Text = "BUSCAR"
        If Trim(TextBox1.Text) = "" Then
            MsgBox("INGREAR DATO", vbInformation, "AVISO")
        Else

            Dim conexion As String
            conexion = "Data Source=LAPTOPHP\MSSQLSERVER01;Initial Catalog=libreria;Integrated Security=True"
            Dim cn As New SqlConnection
            cn.ConnectionString = conexion

            Dim adaptador As New SqlDataAdapter(" SELECT * FROM inventario WHERE Id_inventario =" & TextBox1.Text & "", cn)
            Dim ds As New DataSet
            adaptador.Fill(ds, "datos")

            If ds.Tables("datos").Rows.Count > 0 Then
                TextBox2.Text = ds.Tables("datos").Rows(0).Item(1).ToString
                TextBox3.Text = ds.Tables("datos").Rows(0).Item(2).ToString
                DateTimePicker1.Value = ds.Tables("datos").Rows(0).Item(3).ToString
                TextBox5.Text = ds.Tables("datos").Rows(0).Item(4).ToString
                TextBox6.Text = ds.Tables("datos").Rows(0).Item(5).ToString


            Else
                MsgBox("NO EXISTE NINGUN DATO", vbInformation, "AVISO")
            End If
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Text = "AGREGAR"
        Dim conexion As String
        conexion = "Data Source=LAPTOPHP\MSSQLSERVER01;Initial Catalog=libreria;Integrated Security=True"
        Dim cn As New SqlConnection
        cn.ConnectionString = conexion

        Dim adaptador As New SqlCommand(" insert into inventario values(" & TextBox1.Text & ",'" & TextBox2.Text & "','" & TextBox3.Text & "', '" & DateTimePicker1.Value & "' ,'" & TextBox5.Text & "','" & TextBox6.Text & "') ", cn)
        cn.Open()
        adaptador.ExecuteNonQuery()
        MsgBox("SE REGISTRO CORRECTAMENTE", vbInformation, "AVISO")
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        cn.Close()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
        Form2.Show()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Text = "ACTUALIZAR"
        Dim conexion As String
        conexion = "Data Source=LAPTOPHP\MSSQLSERVER01;Initial Catalog=libreria;Integrated Security=True"
        Dim cn As New SqlConnection
        cn.ConnectionString = conexion
        Try

            Dim adaptador As New SqlCommand("update inventario set Clave = " & TextBox2.Text & ", Cantidad_libro = '" & TextBox3.Text & "', Fecha_Registro= '" & DateTimePicker1.Value & "', Pedidos = '" & TextBox5.Text & "', Id_libro= " & TextBox6.Text & " WHERE Id_inventario = " & TextBox1.Text & "  ", cn)
            cn.Open()
            adaptador.ExecuteNonQuery()
            MsgBox("SE HAN ACTUALIZADO LOS DATOS", vbInformation, "AVISO")
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox5.Text = ""
            TextBox6.Text = ""

        Catch ex As Exception
            MsgBox("NO SE PUDO ACTUALIZAR EL REGISTRO", vbCritical, "AVISO")
            cn.Close()
        End Try

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Text = "ELIMINAR"
        Dim conexion As String
        conexion = "Data Source=LAPTOPHP\MSSQLSERVER01;Initial Catalog=libreria;Integrated Security=True"
        Dim cn As New SqlConnection
        cn.ConnectionString = conexion
        cn.Open()

        Dim comando As New SqlCommand("delete from inventario WHERE Id_inventario = " & TextBox1.Text & " ", cn)
        comando.ExecuteNonQuery()
        MsgBox("SE ELIMINO CORRECTAMENTE", vbInformation, "AVISO")
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        cn.Close()
    End Sub

    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim conexion As String
        conexion = "Data Source=LAPTOPHP\MSSQLSERVER01;Initial Catalog=libreria;Integrated Security=True"
        Dim cn As New SqlConnection
        cn.ConnectionString = conexion
        cn.Open()

        Dim adaptador As New SqlDataAdapter(" SELECT TOP 1 ( Id_inventario) FROM inventario ORDER BY  Id_inventario DESC", cn)
        Dim ds As New DataSet
        adaptador.Fill(ds, "datos")

        If ds.Tables("datos").Rows.Count > 0 Then
            TextBox1.Text = ds.Tables("datos").Rows(0).Item(0).ToString + 1



        End If

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class
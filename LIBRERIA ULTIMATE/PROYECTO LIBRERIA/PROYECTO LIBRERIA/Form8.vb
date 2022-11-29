Imports System.Data.SqlClient
Public Class Form8
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Close()
        Form6.Show()
    End Sub
    Dim V As Integer
    Private Sub Form8_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Enabled = True
        Dim conexion As String
        conexion = "Data Source=LAPTOPHP\MSSQLSERVER01;Initial Catalog=libreria;Integrated Security=True"
        Dim cn As New SqlConnection
        cn.ConnectionString = conexion

        Dim adaptador2 As New SqlDataAdapter(" SELECT TOP 1 (id_venta) FROM VentaGeneral order by id_venta DESC", cn)
        Dim ds2 As New DataSet
        adaptador2.Fill(ds2, "datos2")

        If ds2.Tables("datos2").Rows.Count > 0 Then
            TextBox1.Text = ds2.Tables("datos2").Rows(0).Item(0).ToString
            V = TextBox1.Text
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        DataGridView2.DataSource = ""
        'CON ESTE CODIGO SE IMPIDE INGRESAR OTRO ID VENTA NO REGISTRADO
        If TextBox1.Text > V Then
            TextBox1.Text = V
        End If
        If TextBox1.Text = "" Then
            MsgBox("PORFAVOR INGRESE UN DATO")
        Else
            Dim conexion As String
            conexion = "Data Source=LAPTOPHP\MSSQLSERVER01;Initial Catalog=libreria;Integrated Security=True"
            Dim cn As New SqlConnection
            cn.ConnectionString = conexion

            Dim row(4) As String
            Dim adaptador As New SqlDataAdapter(" SELECT id_libro, precio, cantidad, importe FROM VentaDetalle WHERE Id_Venta =" & TextBox1.Text & "", cn)
            Dim ds As New DataSet
            adaptador.Fill(ds, "datos")
            DataGridView2.DataSource = ds.Tables("datos")
            Dim adaptador2 As New SqlDataAdapter(" SELECT id_empleado, id_cliente, fecha, subtotal, total FROM VentaGeneral WHERE id_venta =" & TextBox1.Text & "", cn)
            Dim ds2 As New DataSet
            adaptador2.Fill(ds2, "datos2")

            If ds2.Tables("datos2").Rows.Count > 0 Then
                TextBox3.Text = ds2.Tables("datos2").Rows(0).Item(0).ToString
                TextBox4.Text = ds2.Tables("datos2").Rows(0).Item(1).ToString
                TextBox2.Text = ds2.Tables("datos2").Rows(0).Item(2).ToString
                TextBox7.Text = ds2.Tables("datos2").Rows(0).Item(3).ToString
                TextBox8.Text = ds2.Tables("datos2").Rows(0).Item(4).ToString
            End If
            Dim adaptador3 As New SqlDataAdapter(" select Nombre+' '+Ap_Paterno+' '+Ap_Materno from cliente where Id_Cliente =" & TextBox4.Text & "  ", cn)
            Dim ds3 As New DataSet
            adaptador3.Fill(ds3, "datos3")
            If ds3.Tables("datos3").Rows.Count > 0 Then
                TextBox6.Text = ds3.Tables("datos3").Rows(0).Item(0).ToString

            End If

            Dim adaptador4 As New SqlDataAdapter(" select Nombre+' '+Ap_Paterno+' '+Ap_Materno from empleado where Id_Empleado =" & TextBox3.Text & "  ", cn)
            Dim ds4 As New DataSet
            adaptador4.Fill(ds4, "datos4")
            If ds4.Tables("datos4").Rows.Count > 0 Then
                TextBox5.Text = ds4.Tables("datos4").Rows(0).Item(0).ToString

            End If
        End If
    End Sub


End Class
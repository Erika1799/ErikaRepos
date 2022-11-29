Imports System.Data.SqlClient
Public Class Form6
    Dim X As Integer
    Dim Z As Integer = 0
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
        Form2.Show()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label8.Text = TimeOfDay

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        'este codigo impide que la barra de codigos de producto este vacia al querer ingresar un producto
        If TextBox4.Text = "" Then
            MsgBox("CAMPO VACIO", vbCritical, "AVISO")
        Else

            'conexion con la base de datos
            Dim conexion As String
            conexion = "Data Source=LAPTOPHP\MSSQLSERVER01;Initial Catalog=libreria;Integrated Security=True"
            Dim cn As New SqlConnection
            cn.ConnectionString = conexion
            'se lee el codigo proporcionado y se buscan los datos de dicho producto
            Dim row(6) As String
            Dim adaptador As New SqlDataAdapter(" SELECT Id_libro, Titulo, Precio, Stock FROM libro WHERE Id_libro =" & TextBox4.Text & "", cn)
            Dim ds As New DataSet
            adaptador.Fill(ds, "datos")
            'este codigo sirve cuando se introduce un producto que ya estaba en la venta, modifica su cantidad y sus precios
            Dim Y As Integer = 0
            For i As Integer = 0 To X
                If TextBox4.Text = DataGridView1.Rows(i).Cells(0).Value Then
                    DataGridView1.Rows(i).Cells(3).Value = DataGridView1.Rows(i).Cells(3).Value + 1
                    DataGridView1.Rows(i).Cells(4).Value = DataGridView1.Rows(i).Cells(2).Value * DataGridView1.Rows(i).Cells(3).Value

                    i = X
                    Y = 0
                Else
                    Y = 1
                End If
            Next
            'si el producto no formaba parte de la venta, este se introduce a ella con sus datos
            If Y = 1 Then
                If ds.Tables("datos").Rows.Count > 0 Then
                    row(0) = ds.Tables("datos").Rows(0).Item(0).ToString
                    row(1) = ds.Tables("datos").Rows(0).Item(1).ToString
                    row(3) = 1
                    row(2) = ds.Tables("datos").Rows(0).Item(2).ToString
                    row(4) = ds.Tables("datos").Rows(0).Item(2).ToString
                    row(5) = ds.Tables("datos").Rows(0).Item(3).ToString


                    Dim rowToSave As String() = New String() {row(0), row(1), row(2), row(3), row(4), row(5)}
                    DataGridView1.Rows.Add(row)
                    X = X + 1
                Else
                    'si no existe ningun producto con el codigo proporcionado se notificara
                    MsgBox("NO EXISTE NINGUN DATO CON EL IDENTIFICADOR PROPORCIONADO", vbCritical, "AVISO")
                End If
            End If
            'despues de agregar el producto se limpia la barra del codigo
            TextBox4.Text = ""
            'tambien se procede a actualizar el precio total de la compra
            TextBox3.Text = 0
            For i As Integer = 0 To X
                TextBox3.Text = TextBox3.Text + Convert.ToDouble(DataGridView1.Rows(i).Cells(4).Value)
            Next
            TextBox6.Text = TextBox3.Text + (TextBox3.Text * 0.16)
        End If

    End Sub


    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown
        'MANDA LLAMAR A TODOS LOS CLIENTES EN EL DATA
        Select Case e.KeyCode
            Case Keys.C
                Panel1.Visible = True
                TextBox2.Text = ""
        End Select
    End Sub
    Private Sub TextBox4_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox4.KeyDown
        'MANDA LLAMAR A TODOS LOS PRODUCTOS EN EL DATA
        Select Case e.KeyCode
            Case Keys.L
                Panel2.Visible = True


                Dim conexion As String
                conexion = "Data Source=LAPTOPHP\MSSQLSERVER01;Initial Catalog=libreria;Integrated Security=True"
                Dim cn As New SqlConnection
                cn.ConnectionString = conexion

                Dim adaptador As New SqlDataAdapter(" SELECT * from libro  ", cn)
                Dim ds As New DataSet
                adaptador.Fill(ds, "datos")


                DataGridView3.DataSource = ds.Tables("datos")
                TextBox4.Text = ""
        End Select
    End Sub

    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'TODO: esta línea de código carga datos en la tabla 'LibreriaDataSet.libro' Puede moverla o quitarla según sea necesario.


        'TODO: esta línea de código carga datos en la tabla 'LibreriaDataSet.cliente' Puede moverla o quitarla según sea necesario.

        Label1.Text = Date.Now.ToString("dd-MM-yyyy")
        Dim V As Integer
        Dim conexion As String
        conexion = "Data Source=LAPTOPHP\MSSQLSERVER01;Initial Catalog=libreria;Integrated Security=True"
        Dim cn As New SqlConnection
        cn.ConnectionString = conexion

        Dim adaptador As New SqlDataAdapter("select Nombre+ ' ' + Ap_Paterno+' '+Ap_Materno from empleado where Nombre = '" & Module1.EMPLEADO & "' ", cn)
        Dim ds As New DataSet
        adaptador.Fill(ds, "datos")
        If ds.Tables("datos").Rows.Count > 0 Then
            TextBox5.Text = ds.Tables("datos").Rows(0).Item(0).ToString

        End If

        'con este codigo obtenemos el numero de la ultima venta y se muestra la siguiente
        Dim adaptador2 As New SqlDataAdapter(" SELECT TOP 1 (id_venta) FROM VentaGeneral order by id_venta DESC ", cn)
        Dim ds2 As New DataSet
        adaptador2.Fill(ds2, "datos2")
        If ds2.Tables("datos2").Rows.Count > 0 Then
            V = ds2.Tables("datos2").Rows(0).Item(0).ToString

        End If
        TextBox1.Text = V + 1

    End Sub

    Private Sub DataGridView2_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView2.DoubleClick
        TextBox2.Text = DataGridView2.CurrentRow.Index + 1
        Panel1.Visible = False
        'SELECCIONA EL NOMBRE DEL CLIENTE EN EL TEXTBOX
        Dim conexion As String
        conexion = "Data Source=LAPTOPHP\MSSQLSERVER01;Initial Catalog=libreria;Integrated Security=True"
        Dim cn As New SqlConnection
        cn.ConnectionString = conexion
        Dim adaptador2 As New SqlDataAdapter(" select Nombre+' '+Ap_Paterno+' '+Ap_Materno from cliente where Id_Cliente =" & TextBox2.Text & "  ", cn)
        Dim ds2 As New DataSet
        adaptador2.Fill(ds2, "datos2")
        If ds2.Tables("datos2").Rows.Count > 0 Then
            TextBox7.Text = ds2.Tables("datos2").Rows(0).Item(0).ToString

        End If


    End Sub

    Private Sub DataGridView3_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView3.DoubleClick

        Dim FilaActual As Integer
        FilaActual = DataGridView3.CurrentRow.Index

        TextBox4.Text = DataGridView3.Rows(FilaActual).Cells(0).Value
        Call Button1_Click(sender, e)
        Panel2.Visible = False
        CheckBox1.Checked = False
        ComboBox1.Text = ""

    End Sub
    Dim D As Integer
    Private Sub DataGridView1_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.RowEnter
        Dim conexion As String
        conexion = "Data Source=LAPTOPHP\MSSQLSERVER01;Initial Catalog=libreria;Integrated Security=True"
        Dim cn As New SqlConnection
        cn.ConnectionString = conexion
        Dim STOCK As Integer
        For i As Integer = 0 To (X - 1)

            Dim adaptador As New SqlDataAdapter(" SELECT Stock FROM libro WHERE Id_libro =" & DataGridView1.Rows(i).Cells(0).Value & "", cn)
            Dim ds As New DataSet
            adaptador.Fill(ds, "datos")

            If ds.Tables("datos").Rows.Count > 0 Then
                STOCK = ds.Tables("datos").Rows(0).Item(0).ToString
            End If

            If DataGridView1.Rows(i).Cells(3).Value < 0 Then
                MsgBox("INGRESE UNA CANTIDAD VALIDA", vbCritical, "AVISO")
                DataGridView1.Rows(i).Cells(3).Value = 1
                DataGridView1.Rows(i).Cells(4).Value = DataGridView1.Rows(i).Cells(2).Value * DataGridView1.Rows(i).Cells(3).Value

            End If
            If DataGridView1.Rows(i).Cells(3).Value = 0 Then
                MsgBox("INGRESE UNA CANTIDAD VALIDA", vbCritical, "AVISO")
                DataGridView1.Rows(i).Cells(3).Value = 1
                DataGridView1.Rows(i).Cells(4).Value = DataGridView1.Rows(i).Cells(2).Value * DataGridView1.Rows(i).Cells(3).Value

            ElseIf DataGridView1.Rows(i).Cells(3).Value > STOCK Then
                MsgBox("INVENTARIO INSUFICIENTE", vbCritical, "AVISO")
                DataGridView1.Rows(i).Cells(3).Value = STOCK
                DataGridView1.Rows(i).Cells(4).Value = DataGridView1.Rows(i).Cells(2).Value * DataGridView1.Rows(i).Cells(3).Value

            Else
                DataGridView1.Rows(i).Cells(4).Value = DataGridView1.Rows(i).Cells(2).Value * DataGridView1.Rows(i).Cells(3).Value


            End If

        Next
        TextBox3.Text = 0
        For i As Integer = 0 To X
            TextBox3.Text = TextBox3.Text + Convert.ToDouble(DataGridView1.Rows(i).Cells(4).Value)

        Next
        TextBox6.Text = TextBox3.Text + (TextBox3.Text * 0.16)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TextBox6.Text = "" Or Label7.Text = "0" Then
            MsgBox("VENTA VACIA", vbCritical, "AVISO")
        Else

            If TextBox2.Text = "" Then
                MsgBox("SELECCIONAR UN CLIENTE", vbCritical, "AVISO")
            Else

                'CONEXION DE LA BASE DE DATOS
                Dim fecha As Date
                fecha = Now
                Dim conexion As String
                conexion = "Data Source=LAPTOPHP\MSSQLSERVER01;Initial Catalog=libreria;Integrated Security=True"
                Dim cn As New SqlConnection
                cn.ConnectionString = conexion
                Dim VALOR As Integer
                Dim adaptador8 As New SqlDataAdapter(" SELECT Id_Empleado FROM empleado WHERE Nombre ='" & Module1.EMPLEADO & "' ", cn)
                Dim ds8 As New DataSet
                adaptador8.Fill(ds8, "datos8")

                If ds8.Tables("datos8").Rows.Count > 0 Then
                    VALOR = ds8.Tables("datos8").Rows(0).Item(0).ToString
                End If


                'SE GURDAN LOS DATOS DE LA VENTA
                Dim adaptador As New SqlCommand(" insert into VentaGeneral values(" & TextBox1.Text & "," & VALOR & "," & TextBox2.Text & ",'" & fecha & "','" & TextBox3.Text & "','" & TextBox6.Text & "') ", cn)
                cn.Open()
                adaptador.ExecuteNonQuery()


                'tambien se guardan los datos del detalle de venta

                For i As Integer = 0 To (X - 1)
                    If DataGridView1.Rows(i).Cells(3).Value = 0 Then
                        'SI EL PRODUCTO TIENE LA CANTIDAD DE 0 NO SE PUEDE REALIZAR LA VENTA 

                    ElseIf DataGridView1.Rows(i).Cells(3).Value > 0 Then
                        Dim adaptador2 As New SqlCommand(" insert into VentaDetalle values(" & TextBox1.Text & "," & DataGridView1.Rows(i).Cells(0).Value & "," & DataGridView1.Rows(i).Cells(2).Value & "," & DataGridView1.Rows(i).Cells(3).Value & "," & DataGridView1.Rows(i).Cells(4).Value & ") ", cn)
                        adaptador2.ExecuteNonQuery()
                    End If
                Next
                'AQUI MANDA UN MENSAJE QUE LA VENTA YA ESTA REGISTRADA
                cn.Close()
                MsgBox("SU VENTA HA SIDO REGISTRADA", vbInformation, "AVISO")
                TextBox1.Text = TextBox1.Text + 1
                DataGridView1.Rows.Clear()
                X = 0
                TextBox3.Text = ""
                TextBox6.Text = ""
            End If

        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Form8.Show()
      End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        DataGridView1.Rows.Clear()
        X = 0
        TextBox3.Text = ""
        TextBox6.Text = ""

    End Sub

    Private Sub CheckBox1_Click(sender As Object, e As EventArgs) Handles CheckBox1.Click
        'sirve para indicar si se quieren buscar los datos por un filtro o no
        If CheckBox1.Checked = True Then
            ComboBox1.Enabled = True



        Else
            'si el filtro esta desactivado se mostraran todos los productos existentes
            ComboBox1.Enabled = False
            Dim conexion As String
            conexion = "Data Source=LAPTOPHP\MSSQLSERVER01;Initial Catalog=libreria;Integrated Security=True"
            Dim cn As New SqlConnection
            cn.ConnectionString = conexion



            Dim adaptador As New SqlDataAdapter(" SELECT * from libro   ", cn)
            Dim ds As New DataSet
            adaptador.Fill(ds, "datos")



            DataGridView3.DataSource = ds.Tables("datos")
        End If
    End Sub

    Private Sub ComboBox1_SelectedValueChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedValueChanged
        Dim conexion As String
        conexion = "Data Source=LAPTOPHP\MSSQLSERVER01;Initial Catalog=libreria;Integrated Security=True"
        Dim cn As New SqlConnection
        cn.ConnectionString = conexion



        Dim adaptador As New SqlDataAdapter(" SELECT * from libro  where Genero = '" & ComboBox1.Text & "' ", cn)
        Dim ds As New DataSet
        adaptador.Fill(ds, "datos")


        DataGridView3.DataSource = ds.Tables("datos")
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class
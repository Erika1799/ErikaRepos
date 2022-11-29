Imports System.Data.SqlClient
Public Class Form1
    Dim U As String
    Dim X As Integer
    Dim Z As Integer = 0
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Este codigo establece la conexion entre el programa y la base de datos
        Dim conexion As String
        conexion = "Data Source=LAPTOPHP\MSSQLSERVER01;Initial Catalog=libreria;Integrated Security=True"
        Dim cn As New SqlConnection
        cn.ConnectionString = conexion
        cn.Open()

        'Este codigo obtiene todos los id de trabajadores y sus contraseñas y los compara con las textbox
        Dim adaptador As New SqlDataAdapter(" SELECT Nombre, contraseña from empleado  ", cn)
        Dim ds As New DataSet
        adaptador.Fill(ds, "datos")
        For i As Integer = 0 To U-1
            If TextBox1.Text = ds.Tables("datos").Rows(i).Item(0).ToString And TextBox2.Text = ds.Tables("datos").Rows(i).Item(1).ToString Then
                X = 1 'si se comprueba una combinacion de usuario y contraseña se sale del bucle comparador con un valor de 1
                i = U - 1
                Module1.EMPLEADO = TextBox1.Text 'se envia al modulo el id del empleado
            Else
                X = 0 'cuando no se comprueba alguna verificacion se sale del bucle con un valor de 0
            End If
        Next
        If X = 0 Then ' si el valor es 0 se indicara que los datos no coinciden y no se permitira continuar mas alla
            MsgBox("Usuario o Contraseña incorrectos")


            TextBox1.Text = ""
            TextBox2.Text = ""
        Else 'por otra parte si el valor despues del bucle es 1 se continuara con el menu del programa
        Form2.Show()
            Me.Hide()
            TextBox1.Text = ""
            TextBox2.Text = ""
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim conexion As String
        conexion = "Data Source=LAPTOPHP\MSSQLSERVER01;Initial Catalog=libreria;Integrated Security=True"
        Dim cn As New SqlConnection
        cn.ConnectionString = conexion
        cn.Open()

        Dim adaptador2 As New SqlDataAdapter(" SELECT TOP 1 (Id_Empleado) FROM empleado  order by Id_Empleado DESC ", cn)
        Dim ds2 As New DataSet
        adaptador2.Fill(ds2, "datos2")
        If ds2.Tables("datos2").Rows.Count > 0 Then
            U = ds2.Tables("datos2").Rows(0).Item(0).ToString
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If MsgBox("¿DESEA CERRAR SESION?", vbQuestion + vbYesNo, "PREGUNTA") = vbYes Then
            End

        End If
    End Sub
End Class

Imports System.Data.SqlClient
Public Class Form4
    Dim SEXO As String
    Dim Z As String
    Private cn As Object
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Text = "AGREGAR"
        Dim conexion As String
        conexion = "Data Source=LAPTOPHP\MSSQLSERVER01;Initial Catalog=libreria;Integrated Security=True"
        Dim cn As New SqlConnection
        cn.ConnectionString = conexion
        cn.Open()
        Dim adaptador2 As New SqlDataAdapter("select Id_Empleado from empleado", cn)
        Dim ds As New DataSet
        adaptador2.Fill(ds, "datos")
        Dim adaptador3 As New SqlDataAdapter(" SELECT TOP 1 (Id_Empleado) FROM empleado order by Id_Empleado DESC ", cn)
        Dim ds3 As New DataSet
        adaptador3.Fill(ds3, "datos3")
        If ds3.Tables("datos3").Rows.Count > 0 Then
            Z = ds3.Tables("datos3").Rows(0).Item(0).ToString
        End If
        Dim Y As Integer = 0
        For i As Integer = 0 To (Z - 1)
            If TextBox1.Text = ds.Tables("datos").Rows(i).Item(0) Then
                MsgBox("INGRESAR OTRO ID EMPLEADO ESE YA ESTA REGISTRADO", vbCritical, "AVISO")
                i = Z
            Else
                Y = 1


            End If

        Next

        If RadioButton1.Checked Then
            SEXO = RadioButton1.Text
        ElseIf RadioButton2.Checked Then
            SEXO = RadioButton2.Text
        End If
        If Y = 1 Then

            Dim adaptador As New SqlCommand(" insert into empleado values(" & TextBox1.Text & ",'" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & SEXO & "','" & TextBox8.Text & "','" & TextBox9.Text & "', '" & TextBox7.Text & "'  ) ", cn)
            adaptador.ExecuteNonQuery()

            MsgBox("SE REGISTRO CORRECTAMENTE", vbInformation, "AVISO")
            cn.Close()
        End If
        TextBox1.Text = TextBox1.Text + 1
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        RadioButton1.Checked = False
        RadioButton2.Checked = False
        TextBox8.Text = ""
        TextBox9.Text = ""
        TextBox7.Text = ""


    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Text = "ELIMINAR"
        Dim conexion As String
        conexion = "Data Source=LAPTOPHP\MSSQLSERVER01;Initial Catalog=libreria;Integrated Security=True"
        Dim cn As New SqlConnection
        cn.ConnectionString = conexion
        cn.Open()

        Dim comando As New SqlCommand("delete from empleado WHERE Id_empleado = " & TextBox1.Text & " ", cn)
        comando.ExecuteNonQuery()
        MsgBox("SE ELIMINO CORRECTAMENTE", vbInformation, "AVISO")
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        RadioButton1.Checked = False
        RadioButton2.Checked = False
        TextBox8.Text = ""
        TextBox9.Text = ""
        TextBox7.Text = ""
        cn.Close()

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'EN ESTE CODIGO ME PERMITE MOSTRAR LOS DATOS DE CADA UNO DE LOS EMPLEADOS 
        Me.Text = "BUSCAR"
        If Trim(TextBox1.Text) = "" Then
            MsgBox("INGRESAR DATO", vbCritical, "AVISO")
        Else
            'ESTA ES TA CONEXION DE LA BASDE DE DATOS
            Dim conexion As String
            conexion = "Data Source=LAPTOPHP\MSSQLSERVER01;Initial Catalog=libreria;Integrated Security=True"
            Dim cn As New SqlConnection
            cn.ConnectionString = conexion

            Dim adaptador As New SqlDataAdapter(" SELECT * FROM empleado WHERE Id_Empleado =" & TextBox1.Text & "", cn)
            Dim ds As New DataSet
            adaptador.Fill(ds, "datos")

            If ds.Tables("datos").Rows.Count > 0 Then
                TextBox2.Text = ds.Tables("datos").Rows(0).Item(1).ToString
                TextBox3.Text = ds.Tables("datos").Rows(0).Item(2).ToString
                TextBox4.Text = ds.Tables("datos").Rows(0).Item(3).ToString
                TextBox5.Text = ds.Tables("datos").Rows(0).Item(4).ToString
                TextBox6.Text = ds.Tables("datos").Rows(0).Item(5).ToString
                SEXO = ds.Tables("datos").Rows(0).Item(6).ToString
                If SEXO = "FEMENINO" Then
                    RadioButton1.Checked = True
                    RadioButton2.Checked = False
                ElseIf SEXO = "MASCULINO" Then
                    RadioButton1.Checked = False
                    RadioButton2.Checked = True
                End If
                TextBox8.Text = ds.Tables("datos").Rows(0).Item(7).ToString
                TextBox9.Text = ds.Tables("datos").Rows(0).Item(8).ToString
                TextBox7.Text = ds.Tables("datos").Rows(0).Item(9).ToString
            Else
                MsgBox("NO EXISTE NINGUN DATO", vbInformation, "AVISO")
            End If
        End If

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Close()
        Form2.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Text = "ACTUALIZAR"
        Dim conexion As String
        conexion = "Data Source=LAPTOPHP\MSSQLSERVER01;Initial Catalog=libreria;Integrated Security=True"
        Dim cn As New SqlConnection
        cn.ConnectionString = conexion
        Try
            If RadioButton1.Checked Then
                SEXO = RadioButton1.Text
            ElseIf RadioButton2.Checked Then
                SEXO = RadioButton2.Text
            End If
            Dim adaptador As New SqlCommand("update empleado set Nombre = '" & TextBox2.Text & "', Ap_Paterno = '" & TextBox3.Text & "' , Ap_Materno= '" & TextBox4.Text & "', Telefono = '" & TextBox5.Text & "', Correo= '" & TextBox6.Text & "',Sexo= '" & SEXO & "', RFC = '" & TextBox8.Text & "', contraseña = '" & TextBox9.Text & "', Fecha_ingreso = '" & TextBox7.Text & "'  WHERE Id_Empleado = " & TextBox1.Text & "  ", cn)
            cn.Open()
            adaptador.ExecuteNonQuery()
            MsgBox("SE HAN ACTUALIZADO LOS DATOS", vbInformation, "AVISO")
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox4.Text = ""
            TextBox5.Text = ""
            TextBox6.Text = ""
            RadioButton1.Checked = False
            RadioButton2.Checked = False
            TextBox8.Text = ""
            TextBox9.Text = ""
            TextBox7.Text = ""

        Catch ex As Exception
            MsgBox("NO SE PUDO ACTUALIZAR EL REGISTRO", vbCritical, "AVISO")
            cn.Close()
        End Try
    End Sub

    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'ESTE CODIGO MUESTRA EL ULTIMO EMPLEADO REGISTRADO 
        Dim conexion As String
        conexion = "Data Source=LAPTOPHP\MSSQLSERVER01;Initial Catalog=libreria;Integrated Security=True"
        Dim cn As New SqlConnection
        cn.ConnectionString = conexion
        cn.Open()

        Dim adaptador As New SqlDataAdapter(" SELECT TOP 1 ( Id_Empleado) FROM empleado ORDER BY  Id_Empleado DESC", cn)
        Dim ds As New DataSet
        adaptador.Fill(ds, "datos")

        If ds.Tables("datos").Rows.Count > 0 Then
            TextBox1.Text = ds.Tables("datos").Rows(0).Item(0).ToString + 1
        End If
        RadioButton1.Checked = False
        RadioButton2.Checked = False
    End Sub

End Class
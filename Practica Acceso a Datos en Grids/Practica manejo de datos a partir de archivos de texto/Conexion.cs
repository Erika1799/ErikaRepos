using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica_manejo_de_datos_a_partir_de_archivos_de_texto
{
    internal class Conexion
    {
        static SqlConnection cnx;
        static string cadena = "Data Source=LAPTOPHP\\MSSQLSERVER01;Initial Catalog = Usuarios; Integrated Security = True";
            
        private static void Conectar()
        {
            cnx = new SqlConnection(cadena);
            cnx.Open();
        }
        private static void Desconectar()
        {
            cnx.Close();
        }
      
        public static DataTable EjecutaSeleccion(string consulta)
        {
            DataTable dt = new DataTable();
            Conectar();
            SqlDataAdapter da = new SqlDataAdapter(consulta, cnx);

            da.Fill(dt);

            Desconectar();
            return dt;

        }
    }
}

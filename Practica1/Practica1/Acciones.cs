using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ComboBox = System.Windows.Forms.ComboBox;
using TextBox = System.Windows.Forms.TextBox;
using System.Reflection;

namespace Practica1
{
    internal class Acciones
    {
       
        public static void LlenarCombo(string consulta, ComboBox combo, string Id_Cliente, string Nombre)
        {
            DataTable dt;   
            dt = Conexion.EjecutaSeleccion(consulta);
            if (dt == null)
            {
                return;
            }
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "Todos";
            dt.Rows.InsertAt(dr, 0);
            combo.DataSource = dt;
            combo.ValueMember = Id_Cliente;
            combo.DisplayMember = Nombre;
        }

    }
}

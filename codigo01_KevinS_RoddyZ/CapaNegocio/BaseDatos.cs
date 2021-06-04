using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CapaNegocio
{
    public class BaseDatos
    {
        public static SqlConnection ConexionBD()
        {
            //Uso de la base de datos
            SqlConnection conexion = new SqlConnection(@"Data Source=DESKTOP-QS8JAED\SQLEXPRESS;Initial Catalog=Contrato;Integrated Security=True");//String de Conexion
            conexion.Open();
            return conexion;
        }
    }
}

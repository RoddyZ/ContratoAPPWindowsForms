using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Codificador;

namespace CapaNegocio
{
    public class DAOPersona
    {
        public static int NuevaPersona(Persona p)
        {
            int resultado = 0;
            try
            {
                //Controlara el numero de filas afectadas
                SqlConnection con = BaseDatos.ConexionBD();                         //Abrimos la conexion a paritr del return de la clase BaseDatos
                SqlCommand comando = new SqlCommand("sp_IngresarPersonas", con);    //Ejecutamos el comando de la base, en este caso es un proceso Almacendo
                comando.CommandType = System.Data.CommandType.StoredProcedure;      //Especificamos el tipo de comando en este caso StoredProcedure (Procedimiento almacenado)
                                                                                    //Ingresamos los datos q espera el proceso almacenado con los parametros correspondientes del objeto Persona
                comando.Parameters.AddWithValue("@nombrePersona", p.NombrePersona);
                comando.Parameters.AddWithValue("@cedula", p.Cedula);
                comando.Parameters.AddWithValue("@departamento", p.Departamento);
                comando.Parameters.AddWithValue("@titulo", p.Titulo);
                comando.Parameters.AddWithValue("@tipoPersonal", p.TipoPersonal);
                comando.Parameters.AddWithValue("@estado", "SOLICITADO");          //Ingresamos el estado manualmente Solicitado, ya q en este estado debe quedar la persona si el ingreso es correcto
                resultado = comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                resultado = 0;
            }
            return resultado;

        }

        public static int ActualizarEstado(int idPersona,String estado)
        {
            int resultado = 0;
            SqlConnection con = BaseDatos.ConexionBD(); //Abrimos la conexion con la base de datos
            SqlCommand comando = new SqlCommand("sp_ActualizarEstado", con); //Creamos el comando para la usar un procedimiento almacenado
            comando.CommandType = System.Data.CommandType.StoredProcedure;//Indicamos que vamos a usar el procedimiento almacenado
            comando.Parameters.AddWithValue("@idPersona", idPersona);//Se envia los parametros del procedimiento alamacenado
            comando.Parameters.AddWithValue("@estado", estado);
            resultado = comando.ExecuteNonQuery();//Se ejecuta el comando
            return resultado;
        }
        //Funcion que permite recuperar los datos de la base de datos filtrando valores segun el estado de contratación
        public static List<Persona> todasPersonas(String comandoSql)//Recibe el string del comando SQL a ejecutar
        {
            List<Persona> personas = new List<Persona>();
            SqlConnection con = BaseDatos.ConexionBD();
            SqlCommand comando = new SqlCommand(comandoSql, con);
            SqlDataReader reader = comando.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())//Bucle que recupera los datos de la bd, los convierte en objetos y agrega a una lista
                {
                    Persona persona = new Persona();
                    persona.IdPersona = reader.GetInt32(0);
                    persona.NombrePersona = reader.GetString(1);
                    persona.Cedula = reader.GetString(2);
                    persona.Departamento = reader.GetString(3);
                    persona.Titulo = reader.GetString(4);
                    persona.TipoPersonal = reader.GetString(5);
                    persona.Estado = reader.GetString(6);
                    personas.Add(persona);
                }
            }

            return personas;
        }


        //Funcion que permite guardar el PDF
        public static int adjuntar(int idPersona, string nommbrePDF, byte[] archivoPDF)
        {
            SqlConnection con = BaseDatos.ConexionBD();
            SqlCommand comando = new SqlCommand("sp_AdjuntarPDF", con);
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@idPersona",idPersona);
            comando.Parameters.AddWithValue("@nombrePDF", nommbrePDF);
            comando.Parameters.AddWithValue("archivoPDF", archivoPDF);
            return comando.ExecuteNonQuery();
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using CapaNegocio;
using Codificador;


namespace Servidor
{
    class Program
    {
        static void Main(string[] args)
        {
            int puerto = 8080;
            byte[] bufferBandera = new byte[1];//buffer para reconocer la funcionalidad a realizar por el servidor
            IPAddress ip = IPAddress.Parse("127.0.0.1");

            TcpListener servidor = new TcpListener(ip, puerto);
            TcpClient manejoCliente;
            servidor.Start();
            List<Persona> personas = new List<Persona>();//Para enviar listas
            CodificadorTexto codificador = new CodificadorTexto();//permite relizar la codificacion de un objeto persona
            int datosLeidos;
            //Strings que permiten leer de los buffers.
            String datos;
            String estado;     
            while (true)
            {
                manejoCliente = servidor.AcceptTcpClient();//Acepta las conexion del cliente
                Console.WriteLine("El servidor ha aceptado a un cliente...");
                NetworkStream flujo = manejoCliente.GetStream();
                do
                {
                    flujo = manejoCliente.GetStream();   //Parametro necesario para, que al momento de ejecutar otro evento no llegue basura
                    datosLeidos = flujo.Read(bufferBandera, 0, bufferBandera.Length); //Lee las banderas para saber que opcion ejecutar.
                    if (datosLeidos > 0)
                    {
                    datos = Encoding.ASCII.GetString(bufferBandera);
                    Console.WriteLine("Se recibio: \n{0}", datos);
                        if (datos == "a")        //La bandera "a" se refiere al boton Personal contratado del FrmPrincipal 
                        {
                            //Recuperar personas
                            personas.Clear();//Limpia la lista de personas.
                            personas = DAOPersona.todasPersonas("select * from tblPersona where estado='CONTRATADO'");//Obtiene las personas contratadas
                            //-------------------------
                            byte [] bufferTx = Encoding.ASCII.GetBytes(Convert.ToString(personas.Count));//Buffer que permite enviar el tamaño de la lista a recibir
                            //Envia el tamano de la lista
                            flujo.Write(bufferTx, 0, bufferTx.Length);
                            //Envia la lista de personas
                            foreach (Persona item in personas)
                            {
                                byte[] datosCodificados = codificador.Codificar(item);//Codifica una personas con determinados caracteres delimitadores
                                flujo.Write(datosCodificados, 0, datosCodificados.Length);//Envio un obeto persona codificado
                            }
                        }
                        else if (datos == "b")  //La bandera "b" se refiere al boton EstadoContratado del FrmPrincipal 
                        {
                            //Recibe el nombre de la persona a buscar el estado en la BD
                            byte[] nombrePersona = new byte[50];//buffer que permite recibir el nombre de la persona a buscar estado
                            
                            int x=flujo.Read(nombrePersona, 0, nombrePersona.Length);//Lee el nombre de la persona a buscar estado
                            Array.Resize(ref nombrePersona, x);//Permite redimensionar el buffer segun el total de caracteres recibidos
                            datos = Encoding.ASCII.GetString(nombrePersona);//Convierte el buffer a string de datos
                            personas.Clear();
                            //Consulta a la bd
                            personas = DAOPersona.todasPersonas("select * from tblPersona where nombrePersona like '%" + datos + "%'");
                            byte[] bufferTx = Encoding.ASCII.GetBytes(Convert.ToString(personas.Count));//buffer para enviar el tamaño de la lista
                            //Envia el tamano de la lista
                            flujo.Write(bufferTx, 0, bufferTx.Length);
                            //Envia la lista de personas
                            foreach (Persona item in personas)
                            {
                                byte[] datosCodificados = codificador.Codificar(item);//Codifica una personas con determinados caracteres delimitadores
                                flujo.Write(datosCodificados, 0, datosCodificados.Length);//Envio un obeto persona codificado
                            }
                         
                        }
                        else if (datos == "c")  //La bandera "c" se refiere al boton Contratacion del FrmPrincipal 
                        {
                            DecodificadorTexto decodificador = new DecodificadorTexto();   //Creamos un decodificador que servira para leer el objeto
                            int aux= DAOPersona.NuevaPersona(decodificador.Decodificar(flujo));//Guarda en la base la nueva persona
                            string datosMensaje = Convert.ToString(aux);
                            byte[]bufferTx = Encoding.ASCII.GetBytes(datosMensaje);//buffer que permite al cliente indicar si se ingreso la persona o no
                            flujo.Write(bufferTx, 0, bufferTx.Length);
                        }
                        else if (datos == "d")  //La bandera "d" se refiere a actualizar el estado de solicitado a entrega documentos
                        {
                            byte[] buffidPersona = new byte[3];//Buffer que va a recibir el id de cliente
                            int x = flujo.Read(buffidPersona, 0, buffidPersona.Length);//Recibe el id de persona
                            Array.Resize(ref buffidPersona, x);//Se redimensiona el buffer
                            datos = Encoding.ASCII.GetString(buffidPersona);
                            int idPersona = Convert.ToInt32(datos);
                            estado = "ENTREGA DOCUMENTOS";
                            int aux = DAOPersona.ActualizarEstado(idPersona,estado);//Se actualiza en la base de acuerdo al id de la persona
                            string datosMensaje = Convert.ToString(aux);
                            byte[] bufferTx = Encoding.ASCII.GetBytes(datosMensaje);
                            flujo.Write(bufferTx, 0, bufferTx.Length);//Se envia la confirmacion de actualizacion de cambio de estado.
                        }
                        else if (datos == "e")//la bandera "e" va a cambiar el estado a contratado
                        {
                            //Similar a la bandera d, actualiza el estado a contratado en la bd
                            byte[] buffidPersona = new byte[3];
                            int x = flujo.Read(buffidPersona, 0, buffidPersona.Length);
                            Array.Resize(ref buffidPersona, x);
                            datos = Encoding.ASCII.GetString(buffidPersona);
                            int idPersona = Convert.ToInt32(datos);
                            estado = "CONTRATADO";
                            int aux = DAOPersona.ActualizarEstado(idPersona, estado);
                            string datosMensaje = Convert.ToString(aux);
                            byte[] bufferTx = Encoding.ASCII.GetBytes(datosMensaje);
                            flujo.Write(bufferTx, 0, bufferTx.Length);
                        }
                        else if (datos == "f")//la bandera "f" indica si se contrata o valida una persona
                        {
                            //Similar a la bandera e, actualiza el estado a contratado en la bd
                            byte[] buffidPersona = new byte[3];
                            int x=flujo.Read(buffidPersona, 0, buffidPersona.Length);
                            Array.Resize(ref buffidPersona, x);
                            datos = Encoding.ASCII.GetString(buffidPersona);
                            int idPersona = Convert.ToInt32(datos);
                            estado = "VALIDADO";
                            int aux = DAOPersona.ActualizarEstado(idPersona, estado);
                            string datosMensaje = Convert.ToString(aux);
                            byte[] bufferTx = Encoding.ASCII.GetBytes(datosMensaje);
                            flujo.Write(bufferTx, 0, bufferTx.Length);
                        }
                        else if (datos == "g")//la bandera "g" indica que se va a cargar un documento
                        {
                        //flujo.Flush();
                        Console.WriteLine("Estoy en G");
                        //Primero recupero el Id de la persona
                        byte[] buffidPersona = new byte[3];
                        int aux = flujo.Read(buffidPersona, 0, buffidPersona.Length);
                        Array.Resize(ref buffidPersona, aux);
                        datos = Encoding.ASCII.GetString(buffidPersona);
                        int idPersona = Convert.ToInt32(datos);
                        Console.WriteLine("El id q recibi es: "+idPersona);
                        //Recibo nombre del PDF
                        byte[] bufferNombre = new byte[100];
                        int tamañoNombrePDF= flujo.Read(bufferNombre, 0, bufferNombre.Length);  //obtiene el tamaño del buffer
                        Console.WriteLine("El tamaño del buffer es: ");
                        Array.Resize(ref bufferNombre, tamañoNombrePDF);  //Elimina los espacios demas en el buffer
                        datos = Encoding.ASCII.GetString(bufferNombre);
                      // datos = datos.Substring(0, flujo.Read(bufferNombre, 0, bufferNombre.Length));
                        string nombrePDF = Convert.ToString(datos);
                        Console.WriteLine("El nombre del pDF ES: "+nombrePDF);
                        //crear buffer  y obtener el tamaño del PDF
                        byte[] bufferTamañoPDF = new byte[10];

                        int tamañoflujoPDF= flujo.Read(bufferTamañoPDF, 0, bufferTamañoPDF.Length);
                        Array.Resize(ref bufferTamañoPDF, tamañoflujoPDF);
                        datos = Encoding.ASCII.GetString(bufferTamañoPDF);
                        int tamañoPDF = Convert.ToInt32(datos);

                        flujo.Write(bufferTamañoPDF, 0, bufferTamañoPDF.Length);//Confirma que se recibio el pdf

                        //Recibir PDF
                        byte[] Sizepdf = new byte[tamañoPDF];
                        flujo.Read(Sizepdf, 0, Sizepdf.Length);
                        byte[] file = Sizepdf;
                        Console.WriteLine("Me llego "+tamañoPDF+"   "+idPersona+"  "+nombrePDF);
                        //METODO PARA  AGREGAR EL PDF A LA BASE
                        DAOPersona.adjuntar(idPersona, nombrePDF, file);
                        Console.WriteLine("PDF Guardado");
                    }
                        Console.WriteLine("Mensaje Recibido");
                        Console.WriteLine("Se recibio: \n{0}", datos);
                    }
                } while (datosLeidos > 0);
                //Se cierra la conexion con el cliente
                flujo.Close();
                manejoCliente.Close();
            }
        }
       

    }
}

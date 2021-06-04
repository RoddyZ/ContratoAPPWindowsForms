using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Codificador;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Cliente
{
    public partial class FrmPrincipal : Form
    {
        //Arrastrar formulario
        [DllImport("user32.Dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.Dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hand, int wmsg, int wparam, int lparam);
        //Variables
        byte[] bufferTamanoLista_ID = new byte[3];//Buffer que permite recibir el tamaño de una lista
        byte[] bufferID= new byte[3];//Buffer que permite enviar el id de la persona
        List<Persona> personas = new List<Persona>();
        //Logica conexion TCP
        TcpClient cliente;
        NetworkStream flujo;
        public FrmPrincipal()
        {
            InitializeComponent();
        }
        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            try
            {
                IPAddress servidor = IPAddress.Parse("127.0.0.1");
                int puerto = 8080;
                IPEndPoint extremo = new IPEndPoint(servidor, puerto);
                cliente = new TcpClient();
                cliente.Connect(extremo);
            }
            catch (Exception)
            {
                MessageBox.Show("Error al conectarse","Mi primer Contrato",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        private void AbrirFrm(object frmHijo )//Funcion que permite abrir un formulario dentro del dock del formulario principal
        {
            if (this.PnlContenedor.Controls.Count>0)
            {
                this.PnlContenedor.Controls.RemoveAt(0);
            }
            Form fh = frmHijo as Form;
            fh.TopLevel = false;    //tOP LEVEL infdica si la ventana debe mostrarse como nivel superior
            fh.Dock = DockStyle.Fill;    //para acoplar al panel primario
            this.PnlContenedor.Controls.Add(fh);  //agregamos al contenedor el hijo
            this.PnlContenedor.Tag = fh;    //obtengo los objetos del contenedor
            fh.Show();
        }
        //Metodo click
        private void PnlTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        /**********************************************************************
       **************          Funciones               ***********************
       **********************************************************************/
        private void btnPersonalContratado_Click(object sender, EventArgs e)//Evento del boton personal contratado que recibe una lista de personas contratadas
        {
            bufferTamanoLista_ID = new byte[3];//vaciar el buffer
            //Recibir el tamanio de la lista
            flujo = cliente.GetStream();
            string datos = "a";

            byte[] bufferTx = Encoding.ASCII.GetBytes(datos);
            //Envia bandera
            flujo.Write(bufferTx,0,bufferTx.Length);

            //Recive el tamano de lista
            flujo.Read(bufferTamanoLista_ID, 0, bufferTamanoLista_ID.Length);
            int size = Convert.ToInt32(Encoding.ASCII.GetString(bufferTamanoLista_ID));
            
            DecodificadorTexto decodificador = new DecodificadorTexto();
            //Recibe la lista
            personas.Clear();//limpia la lista anterior de personas contratadas
            for (int i = 0; i < size; i++)
            {
                personas.Add(decodificador.Decodificar(flujo));
            }
            AbrirFrm(new FrmPersonasContratadas(personas));
        }
        private void BuscarEstado(string nombre)//Funcion que permite buscar el estado de una o mas personas segun el nombre de la persona enviada
        {
            
            flujo = cliente.GetStream();
            string datos = "b";
            byte[] bufferTx = Encoding.ASCII.GetBytes(datos);
            //Envia bandera
            flujo.Write(bufferTx, 0, bufferTx.Length);
            //Envia el nombre a buscar el estado
            bufferTx = Encoding.ASCII.GetBytes(nombre);
            flujo.Write(bufferTx, 0, bufferTx.Length);
            //Recive el tamano de lista
            bufferTamanoLista_ID = new byte[3];//vaciar el buffer
            flujo = cliente.GetStream();
            int x = flujo.Read(bufferTamanoLista_ID, 0, bufferTamanoLista_ID.Length);
            Array.Resize(ref bufferTamanoLista_ID, x);
           
            int size = Convert.ToInt32(Encoding.ASCII.GetString(bufferTamanoLista_ID));

            string datosNotifcacion = Convert.ToString(size);
            personas.Clear();//limpia la lista anterior de personas contratadas

            if (datosNotifcacion == "0")//en caso que el tamaño de la lista sea 0 indica que no se encontraron resultados
            {
                MessageBox.Show("No se encontraron resultados", "MiPrimerContrato", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else 
            {
                DecodificadorTexto decodificador = new DecodificadorTexto();
                //Recibe la lista 
                for (int i = 0; i < size; i++)//lazo que decodifica las persona recibidas y las agrega a una lista
                {
                    personas.Add(decodificador.Decodificar(flujo));
                }
                FrmEstadoContratacion frmEstado = new FrmEstadoContratacion(personas);
                //Suscricion a los eventos del formulario estado contratacion
                frmEstado.pasoNombre += new FrmEstadoContratacion.BuscarEstado(BuscarEstado);
                frmEstado.adjuntarDocumento += new FrmEstadoContratacion.ActualizarEstado(cambiarEstadoEntregaDoc);
                frmEstado.validarContrato += new FrmEstadoContratacion.ActualizarEstado(Aprobar_o_ValidarContrato);
                AbrirFrm(frmEstado);
            }
           
        }

        private void Aprobar_o_ValidarContrato(int idPersona)//Funcion que permite realizar la contratacion o validacion de personas
        {
            Console.WriteLine("aprobar");
            DialogResult result = MessageBox.Show("Aprobar El Contrato ?", "Mi Primer Contrato",MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question);

            switch (result)
            {
                case DialogResult.Yes:
                    EnviarCambioEstado(idPersona, "e");
                    break;
                case DialogResult.No:
                    EnviarCambioEstado(idPersona, "f");
                    break;
            }
          
        }

        private void cambiarEstadoEntregaDoc(int idPersona)//Funcion que permite enviar un pdf
        {
            enviarPDF(idPersona);     //Envio el PDF
        }

        private void enviarPDF(int idPersona)//Logica de envio de un documento pdf
        {
            //Primero envio la bandera
            flujo = cliente.GetStream();
            string datos = "g";
            byte[] bufferTx = Encoding.ASCII.GetBytes(datos);
            //Envia bandera
            flujo.Write(bufferTx, 0, bufferTx.Length);
            Console.WriteLine("La bandera q envio es: "+datos);
            //enviar id de la persona
            datos = Convert.ToString(idPersona);
            bufferTx= Encoding.ASCII.GetBytes(datos);
            flujo.Write(bufferTx, 0, bufferTx.Length);
            Console.WriteLine("La id q estoy enviando es: "+idPersona);
            
            openFileDialog1.Filter = "Archivos PDF |*.pdf";  //filtros para pdf
            openFileDialog1.FilterIndex = 1;    //Me devuelve a la carpeta q estaba antes
            openFileDialog1.RestoreDirectory = true;  //Para que se quede en el directorio q abri
           
            string nombreArchivo="";  //Recogere el nombre del archivo
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
               nombreArchivo = openFileDialog1.SafeFileName;  //No incluye la ruta del archivo
            }
           
            bufferTx = Encoding.ASCII.GetBytes(nombreArchivo);
            flujo.Write(bufferTx, 0, bufferTx.Length);
            Console.WriteLine("el nombre q estoy enviando es: "+nombreArchivo+" la dimension  es: ");
          
            //enviar
            byte[] file = null;
            Stream myStream = openFileDialog1.OpenFile();  //Obtenemos el archivo 
            using (MemoryStream ms = new MemoryStream())
            {
                myStream.CopyTo(ms);  //Copia los bytes a ms
                file = ms.ToArray();
            }
            //enviar tamano buffer
            byte[] tamaño = Encoding.ASCII.GetBytes(Convert.ToString(file.Length));
            flujo.Write(tamaño, 0, tamaño.Length);
            flujo.Read(tamaño,0,tamaño.Length);//Recibe una confirmacion del arhivo pdf enviado                                                 
            //Envio PDF
            flujo.Write(file, 0, file.Length);
            EnviarCambioEstado(idPersona, "d");    //Envia la bandera d la cual cambia el estado a ENTREGA DOCUEMENTOS en el servidor
        }
        private void MandarSolicitud(Persona a)// Funcion que envia la solicitud de una persona nueva a la base
        {
            //Aqui manda
            flujo = cliente.GetStream();
            string datos = "c";
            byte[] bufferTx = Encoding.ASCII.GetBytes(datos);
            //Envia bandera
            flujo.Write(bufferTx, 0, bufferTx.Length);
            //flujo.Flush();
            //flujo = cliente.GetStream();
            CodificadorTexto codificador = new CodificadorTexto();
            bufferTx = codificador.Codificar(a);
            flujo.Write(bufferTx, 0, bufferTx.Length);

            //Recibir notificacion del exito o fracaso del ingreso de persona
            byte[] bufferNotificacion =  new byte[1];
            flujo.Read(bufferNotificacion, 0, bufferNotificacion.Length);
            string datosNotifcacion = Encoding.ASCII.GetString(bufferNotificacion);

            if (datosNotifcacion=="1")//Se verifica si se  ingreso la nueva persona o no
            {
                MessageBox.Show("Ingreso Exitoso!", "MiPrimerContrato", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }else if (datosNotifcacion == "0")
            {
                MessageBox.Show("Lo sentimos ya existe una persona con el numero de cedula que ingreso", "MiPrimerContrato",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
 
        private void EnviarCambioEstado(int idPersona,String flag)//Funcion que permite cambiar los estados de una determinada persona en base a su ID
        {
            Console.WriteLine("Estoy cambiando el estado!");
            
            flujo = cliente.GetStream();  
            string datos = flag;
            Console.WriteLine("LA BANDERA1 ES : " + flag);
            byte[] bufferTx = Encoding.ASCII.GetBytes(datos);
            //Envia bandera
            flujo.Write(bufferTx, 0, bufferTx.Length);
            Console.WriteLine(  "LA BANDERA ES : "+flag);
            bufferID = Encoding.ASCII.GetBytes(Convert.ToString(idPersona));
            //Envia la id de persona
            flujo.Write(bufferID, 0, bufferID.Length);
            //Recibe respuesta de Actualizacion de estado
            byte[] bufferNotificacion = new byte[1];
            flujo.Read(bufferNotificacion, 0, bufferNotificacion.Length);
            string datosNotifcacion = Encoding.ASCII.GetString(bufferNotificacion);
            if (datosNotifcacion == "1")
            {
                MessageBox.Show("Actualizacion Exitosa!", "MiPrimerContrato", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (datosNotifcacion == "0")
            {
                MessageBox.Show("Error al actualizar", "MiPrimerContrato", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /**********************************************************************
         **************          EVENTOS BOTONES        ***********************
         **********************************************************************/
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
            flujo.Close();
            cliente.Close();
        }
        private void btnMaximixar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btnMinimizar.Visible = true;
            btnMaximixar.Visible = false;
        }
        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            btnMinimizar.Visible = false;
            btnMaximixar.Visible = true;
        }
        private void btnOcultar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            flujo = cliente.GetStream();
            this.Close();
            flujo.Close();
            cliente.Close();
        }
        private void btnContratacion_Click(object sender, EventArgs e)
        {
            FrmContratacion frm1 = new FrmContratacion();
            frm1.pasodeobjeto += new FrmContratacion.Paso(MandarSolicitud);
            AbrirFrm(frm1);
        }
        private void btnEstadoContratacion_Click(object sender, EventArgs e)
        {
            FrmEstadoContratacion frmEstado = new FrmEstadoContratacion(new List<Persona>());
            frmEstado.pasoNombre += new FrmEstadoContratacion.BuscarEstado(BuscarEstado);

            AbrirFrm(frmEstado);
        }
    }
}

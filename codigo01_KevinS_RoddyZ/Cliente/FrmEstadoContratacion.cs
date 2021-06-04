using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Codificador;

namespace Cliente
{
    public partial class FrmEstadoContratacion : Form
    {
        public delegate void BuscarEstado(String nombre);//Delegado para buscar estado en el formulario Principal
        public event BuscarEstado pasoNombre;//Evento que hara que el formulario principal busque el estado del nombre ingresado
        public delegate void ActualizarEstado(int idPersona);//Delegado para realizar la actualizacion de estados
        public event ActualizarEstado adjuntarDocumento;//En caso de ejecutar el boton adjuntarDocumento cargar documento y actualizar a Entrega Documentos
        public event ActualizarEstado validarContrato;//En caso de ejecutar el boton de validar cambiar el estado a contratado o validado.
        
        List<Persona> estadoPersonas = new List<Persona>();

        public FrmEstadoContratacion(List<Persona>temp)
        {
            InitializeComponent();
            estadoPersonas = temp;
            
        }

        private void btnVerEstado_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text!="")
            {
                pasoNombre(txtNombre.Text);
                errorProvider1.SetError(txtNombre, "");
            }
            else
            {
                errorProvider1.SetError(txtNombre, "Este campo es obligatorio!");
            }
            dgvEstadoPersonas.DataSource = null;//Evita excepcion cuando no se recibe un resultado de una persona buscada
            btnCargarDocumento.Visible = false;
            btnValidar.Visible = false;
        }

        private void FrmEstadoContratacion_Load(object sender, EventArgs e)
        {
            if (estadoPersonas.Count()!=0)//En el caso de que una lista venga vacia
            {
                dgvEstadoPersonas.DataSource = estadoPersonas;
                dgvEstadoPersonas.Columns["Departamento"].Visible = false;
                dgvEstadoPersonas.Columns["Titulo"].Visible = false;
                dgvEstadoPersonas.Columns["TipoPersonal"].Visible = false;
            }
            if (estadoPersonas.Count() == 1) //En el caso de haber recibido solo un resultado activa los botones correspondientes.
            {
                switch (estadoPersonas[0].Estado)
                {
                    case "SOLICITADO":
                        btnCargarDocumento.Visible = true;
                        break;
                    case "ENTREGA DOCUMENTOS":
                        btnValidar.Visible = true;
                        break;
                }
            }
        }

     
        private void dgvEstadoPersonas_CellClick(object sender, DataGridViewCellEventArgs e)//Evento de hacer click en el datagridview y escoger una persona
        {
            //Sirve en el caso de tener dos o mas resultados
            var filaSeleccionada = dgvEstadoPersonas.CurrentRow;
            int id = 0;
            if (filaSeleccionada != null)
            {
                id = Convert.ToInt32(filaSeleccionada.Cells[0].Value);
                if (filaSeleccionada.Cells[6].Value.ToString() == "SOLICITADO")
                {
                    btnCargarDocumento.Visible = true;
                    btnValidar.Visible = false;
                }
                else if (filaSeleccionada.Cells[6].Value.ToString() == "ENTREGA DOCUMENTOS")
                {
                    btnValidar.Visible = true;
                    btnCargarDocumento.Visible = false;
                }
                else
                {
                    btnValidar.Visible = false;
                    btnCargarDocumento.Visible = false;
                }

            }
        }

        private void btnValidar_Click(object sender, EventArgs e)
        {
            var filaSeleccionada = dgvEstadoPersonas.CurrentRow;
            int idPersona = 0;
            if (estadoPersonas.Count() == 1) //En el caso de haber recibido solo un resultado
            {
                switch (estadoPersonas[0].Estado)
                {
                    case "SOLICITADO":
                        adjuntarDocumento(estadoPersonas[0].IdPersona);
                        break;
                    case "ENTREGA DOCUMENTOS":
                        validarContrato(estadoPersonas[0].IdPersona);
                        break;
                }
            }
            else //Selecciona una persona en el caso de haber dos o mas resultados
            {
                if (filaSeleccionada != null)
                {
                    idPersona = Convert.ToInt32(filaSeleccionada.Cells[0].Value);
                    if (filaSeleccionada.Cells[6].Value.ToString() == "SOLICITADO")
                    {
                        adjuntarDocumento(idPersona);
                    }
                    if (filaSeleccionada.Cells[6].Value.ToString() == "ENTREGA DOCUMENTOS")
                    {
                        validarContrato(idPersona);
                    }
                }
            }
            dgvEstadoPersonas.DataSource = null;//Evita excepcion cuando no se recibe un resultado de una persona buscada
            btnCargarDocumento.Visible = false;
            btnValidar.Visible = false;

        }

        private void btnCargarDocumento_Click(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(dgvEstadoPersonas.CurrentRow.Cells[0].Value);  //Obtiene el id de la persona escogida
            Console.WriteLine("El id de la persona escogida es: "+x);      
            adjuntarDocumento(x);
            dgvEstadoPersonas.DataSource = null;//Evita excepcion cuando no se recibe un resultado de una persona buscada
            btnCargarDocumento.Visible = false;
            btnValidar.Visible = false;
        }
    }
    
}

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
    public partial class FrmContratacion : Form
    {
        int contador = 0;     //cuenta los numeros en cedula
        //Delegados y eventos para ingresar a la nueva persona a ser contrantada
        public delegate void Paso(Persona a);
        public event Paso pasodeobjeto;
        public FrmContratacion()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
   
            if (contador == 10 && cbxDepartamento.SelectedItem != null && cbxTitulo.SelectedItem != null && cbxTipoPersonal.SelectedItem != null && txtNombre.Text != "")
            {
                //Lectura de datos del formulario
                Persona persona = new Persona();
                persona.NombrePersona = txtNombre.Text;
                persona.Cedula = txtCedula.Text;
                persona.Titulo = cbxTitulo.SelectedItem.ToString();
                persona.Departamento = cbxDepartamento.SelectedItem.ToString();
                persona.TipoPersonal = cbxTipoPersonal.SelectedItem.ToString();
                persona.Estado = "SOLICITADO";
                pasodeobjeto(persona);
                txtNombre.Clear();
                txtCedula.Clear();
                cbxDepartamento.Text = null;
                cbxTitulo.Text = null;
                cbxTipoPersonal.Text = null;
                contador = 0;
            }
            else if (contador != 10 && cbxDepartamento.SelectedItem == null && cbxTitulo.SelectedItem == null && cbxTipoPersonal.SelectedItem == null && txtNombre.Text == "")
            {
                errorProvider1.SetError(txtCedula, "Se necesitan 10 digitos en este parametro!");
                errorProvider1.SetError(txtNombre, "Por favor escriba un nombre!");
                errorProvider1.SetError(cbxTipoPersonal, "Por favor escoga una opcion!");
                errorProvider1.SetError(cbxTitulo, "Por favor escoga una opcion!");
                errorProvider1.SetError(cbxDepartamento, "Por favor escoga una opcion!");
            }
            else if (contador != 10)
            {
                errorProvider1.SetError(txtCedula, "Se necesitan 10 digitos en este parametro!");
            }
            else if (cbxDepartamento.SelectedItem == null)
            {
                errorProvider1.SetError(cbxDepartamento, "Por favor escoga una opcion!");
            }
            else if (cbxTitulo.SelectedItem == null)
            {
                errorProvider1.SetError(cbxTitulo, "Por favor escoga una opcion!");
            }
            else if (cbxTipoPersonal.SelectedItem == null)
            {
                errorProvider1.SetError(cbxTipoPersonal, "Por favor escoga una opcion!");
            }
            else if (txtNombre.Text == "")
            {
                errorProvider1.SetError(txtNombre, "Por favor escriba un nombre!");
            }
        }

        //Validaciones
        private void cbxTipoPersonal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxTipoPersonal != null)
            {
                errorProvider1.SetError(cbxTipoPersonal, "");
            }
            else
            {
                errorProvider1.SetError(cbxTipoPersonal, "Por favor escoga una opcion!");
            }

        }
        private void cbxDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxDepartamento != null)
            {
                errorProvider1.SetError(cbxDepartamento, "");
            }
            else
            {
                errorProvider1.SetError(cbxDepartamento, "Por favor escoga una opcion!");
            }
        }

        private void cbxTitulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxTitulo != null)
            {
                errorProvider1.SetError(cbxTitulo, "");
            }
            else
            {
                errorProvider1.SetError(cbxTitulo, "Por favor escoga una opcion!");
            }
        }
        //------------------------------------------------------------

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {

            soloLetras(e);
        }

        private void txtCedula_KeyPress(object sender, KeyPressEventArgs e)
        {
            soloNumeros(e);
        }


        //---------------------Validaciones---------------------------------
        public void soloLetras(KeyPressEventArgs letra)   //evento al presionar una letrita
        {
            
            try
            {
                if (Char.IsLetter(letra.KeyChar))
                {

                    letra.Handled = false;   //Permite q se escriba la letra
                    errorProvider1.SetError(txtNombre, "");
                }
                else if (Char.IsControl(letra.KeyChar))   //Borrar
                {

                    letra.Handled = false;
                    errorProvider1.SetError(txtNombre, "");
                }
                else if (Char.IsSeparator(letra.KeyChar))
                {

                    letra.Handled = false;
                    errorProvider1.SetError(txtNombre, "");
                }
                else
                {

                    letra.Handled = true;   //No se escriba
                    errorProvider1.SetError(txtNombre, "Solo se admiten letras en este campo!");
                }

            }
            catch (Exception e)
            {

            }
        }
        public void soloNumeros(KeyPressEventArgs numero)   //evento al presionar una letrita
        {
           
            try
            {
                if (Char.IsNumber(numero.KeyChar))
                {

                    errorProvider1.SetError(txtCedula, "");
                    numero.Handled = false;   //Permite numero
                    contador++;
                }
                else if (Char.IsControl(numero.KeyChar))   //Borrar
                {

                    errorProvider1.SetError(txtCedula, "");
                    contador--;
                    numero.Handled = false;
                }
                else if (Char.IsSeparator(numero.KeyChar))   //espacio
                {

                    errorProvider1.SetError(txtCedula, "Solo se admiten numeros en este campo!");
                    numero.Handled = true;
                }
                else
                {
                    errorProvider1.SetError(txtCedula, "Solo se admiten numeros en este campo!");
                    numero.Handled = true;   //No se escriba
                }

            }
            catch (Exception )
            {

            }
        }
    }
}

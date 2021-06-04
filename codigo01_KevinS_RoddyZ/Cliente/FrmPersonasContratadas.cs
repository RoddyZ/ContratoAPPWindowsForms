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
    public partial class FrmPersonasContratadas : Form
    {
        //Listas que permiten mostrar informacion en el datagridview del form
        List<Persona> listaPersonaContratadas = new List<Persona>();
        List<Persona> listaFiltrada= new List<Persona>();
        public FrmPersonasContratadas(List<Persona> personas)
        {
            InitializeComponent();
            listaPersonaContratadas = personas;//Recibe la lista de personas contratadas
        }

        private void FrmPersonasContratadas_Load(object sender, EventArgs e)
        {
           
            dgvPersonasContratadas.DataSource = listaPersonaContratadas;//Se muestra en la datagridview las personas contratadas
            dgvPersonasContratadas.Columns["IdPersona"].Visible = false;
            dgvPersonasContratadas.Columns["Estado"].Visible = false;
            txtTotal.Text = Convert.ToString(listaPersonaContratadas.Count());//Muestra el total de persona contratadas
        }

        private void cbxTipoPersonal_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvPersonasContratadas.DataSource = "Nothing";//Vacia el datagridview
            listaFiltrada.Clear();//Vacia la lista
            foreach (Persona item in listaPersonaContratadas)
            {
                if (item.TipoPersonal==Convert.ToString(cbxTipoPersonal.SelectedItem))//Se filtra de acuerdo al tipo de personal
                {
                    listaFiltrada.Add(item);//añade a una nueva lista filtrada por el tipo de personal
                }
            }
            dgvPersonasContratadas.DataSource = listaFiltrada;
            dgvPersonasContratadas.Columns.Remove("IdPersona");//Quita la columna de ID
            dgvPersonasContratadas.Columns.Remove("Estado");//Quita la columna Estado
            txtTotal.Text = Convert.ToString(listaFiltrada.Count);
        }
    }
}

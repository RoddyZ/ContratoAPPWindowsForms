using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;     //Stream

namespace Codificador
{
    public class Persona
    {
        private int idPersona;
        private string nombrePersona;
        private string cedula;
        private string departamento;
        private string titulo;
        private string tipoPersonal;
        private string estado;

        public int IdPersona { get => idPersona; set => idPersona = value; }
        public string NombrePersona { get => nombrePersona; set => nombrePersona = value; }
        public string Cedula { get => cedula; set => cedula = value; }
        public string Departamento { get => departamento; set => departamento = value; }
        public string Titulo { get => titulo; set => titulo = value; }
        public string TipoPersonal { get => tipoPersonal; set => tipoPersonal = value; }
        public string Estado { get => estado; set => estado = value; }

        public Persona()
        {
        }

        public Persona(int idPersona, string nombrePersona, string cedula, string departamento, string titulo, string tipoPersonal, string estado)
        {
            this.IdPersona = idPersona;
            this.NombrePersona = nombrePersona;
            this.Cedula = cedula;
            this.Departamento = departamento;
            this.Titulo = titulo;
            this.TipoPersonal = tipoPersonal;
            this.Estado = estado;
        }

        public override string ToString()
        {
            String separador = "\n";
            String valor = "ID#=" + IdPersona + separador + "Nombre=" 
                + NombrePersona + separador + "Cedula=" + Cedula + separador + 
                "Departamento=" + Departamento + separador + "Titulo=" + Titulo + separador +
                "Tipo Personal =" + TipoPersonal + separador + "Estado= "+ Estado +separador;
            return valor;
        }
    }
    public interface CodificadorPersona
    {
        byte[] Codificar(Persona persona);
    }
    public interface DecodificadorPersona
    {
        Persona Decodificar(Stream dato);
        Persona Decodificar(byte[] paquete);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace Codificador
{
    public class ConstantesCodificadorTexto
    {
        public static readonly String CODIFICACION_POR_DEFECTO = "ascii";
        public static readonly int LONG_MAX_FLUJO = 1024;
    }
    public class CodificadorTexto : CodificadorPersona
    {
        public Encoding codificador;
        public CodificadorTexto() : this(ConstantesCodificadorTexto.CODIFICACION_POR_DEFECTO)
        {
        }
        public CodificadorTexto(string datos)
        {
            codificador = Encoding.GetEncoding(datos);
        }
         public byte[] Codificar(Persona elemento)//Metodo que permite realizar la codificacion usando caracteres especiales
        {
            String cadenaCodificada = elemento.IdPersona + " ";

            if (elemento.NombrePersona.IndexOf('\n') != -1)
                throw new IOException("Descripcion no valida (contiene un salto de linea)");

            cadenaCodificada = cadenaCodificada + elemento.NombrePersona + "\n";
            cadenaCodificada = cadenaCodificada + elemento.Cedula + " ";
            cadenaCodificada = cadenaCodificada + elemento.Departamento + " ";
            cadenaCodificada = cadenaCodificada + elemento.Titulo + " ";
            cadenaCodificada = cadenaCodificada + elemento.TipoPersonal + " ";
            cadenaCodificada = cadenaCodificada + elemento.Estado + "\n";
            if (cadenaCodificada.Length > ConstantesCodificadorTexto.LONG_MAX_FLUJO)
                throw new IOException("Longitud codificada demasiado grande");
            byte[] bufer = codificador.GetBytes(cadenaCodificada);
            return bufer;
        }

    }
    public class DecodificadorTexto : DecodificadorPersona
    {
        public Encoding decodificador;

        public DecodificadorTexto() : this(ConstantesCodificadorTexto.CODIFICACION_POR_DEFECTO)
        { }

        public DecodificadorTexto(String datoCodificado)
        {
            decodificador = Encoding.GetEncoding(datoCodificado);
        }

        public Persona Decodificar(Stream flujo)//Metodo que permite decodificar un objeto persona identificando distintos delimitadores
        {
            String idPersona, nombre, cedula, departamento, titulo, tipoPersonal, estado;
            byte[] espacios = decodificador.GetBytes(" ");
            byte[] saltoLinea = decodificador.GetBytes("\n");
            idPersona = decodificador.GetString(Entramar.SiguienteToken(flujo, espacios));
            nombre = decodificador.GetString(Entramar.SiguienteToken(flujo, saltoLinea));
            cedula = decodificador.GetString(Entramar.SiguienteToken(flujo, espacios));
            departamento = decodificador.GetString(Entramar.SiguienteToken(flujo, espacios));
            titulo = decodificador.GetString(Entramar.SiguienteToken(flujo, espacios));
            tipoPersonal = decodificador.GetString(Entramar.SiguienteToken(flujo, espacios));
            estado = decodificador.GetString(Entramar.SiguienteToken(flujo, saltoLinea));
            return new Persona(Convert.ToInt32(idPersona), nombre, cedula,departamento, titulo,tipoPersonal,estado);
        }

        public Persona Decodificar(byte[] paquete)
        {
            Stream cargaUtil = new MemoryStream(paquete, 0, paquete.Length, false);
            return Decodificar(cargaUtil);
        }
    }
}

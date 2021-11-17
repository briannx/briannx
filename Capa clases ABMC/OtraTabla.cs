using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySqlConnector;
using Capa_datos_ABMC;
using System.ComponentModel;

namespace Capa_clases_ABMC
{
    public class OtraTabla
    {
        private int id;

        [Browsable(false)]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string dato;

        [DisplayName("Clasificación")]
        public string Dato
        {
            get { return dato; }
            set { dato = value; }
        }

        public OtraTabla()
        {
            id = 0;
            Dato = "n/d";
        }

        public OtraTabla(string unDato)
        {
            id = 0;
            Dato = unDato;
        }

        public OtraTabla(int unId, string unDato)
        {
            id = unId;
            Dato = unDato;
        }

        public int Guardar()
        {
            try
            {
                //int nro = MySQL.ExecuteScalarSP("select idOT from otratabla WHERE dato='" + Dato + "'",CommandType.Text );
                //MySQL.ExecuteNonQuery("insert into Personas (Apyn, Direccion,Localidad,CP,FecNac,idOtraTabla) values ()",CommandType.Text ); \
                object a = MySQL.ExecuteScalarSP("Select max(idOT) from otratabla", CommandType.Text);
                MySQL.ExecuteNonQuerySP("insert into otratabla (idOT,dato) values (" + (a == DBNull.Value ? 0 : Convert.ToInt16(a) + 1).ToString() + ",'" + Dato + "')", CommandType.Text);
            }
            catch (MySqlException x)
            {
                Console.WriteLine(x.Message);
                return -1;
            }
            return 0;
        }

        public int Editar(string auxDato)
        {
            try
            {
                MySQL.ExecuteNonQuerySP("update otratabla set Dato = '" + auxDato + "'where idOT = " + id, CommandType.Text);
            }
            catch (MySqlException x)
            {
                Console.WriteLine(x.Message);
                return -1;
            }
            return 0;
        }

        public int Borrar()
        {
            try
            {
                //int nro = MySQL.ExecuteScalarSP("select idOT from otratabla WHERE dato='" + Dato + "'",CommandType.Text );
                //MySQL.ExecuteNonQuery("insert into Personas (Apyn, Direccion,Localidad,CP,FecNac,idOtraTabla) values ()",CommandType.Text ); \
                MySQL.ExecuteNonQuerySP("delete from otratabla where idOT =" + id + "", CommandType.Text);
            }
            catch (MySqlException x)
            {
                Console.WriteLine(x.Message);
                return -1;
            }
            return 0;

        }

        public override string ToString()
        {
            return Dato;
        }

        public static List<OtraTabla> Buscar(string unTexto)
        {
            List<OtraTabla> lista = new List<OtraTabla>();
            if (unTexto == "")
            {
                MySqlDataReader datos = MySQL.ExecuteReaderSP("Select idOT,dato from otratabla", CommandType.Text);
                while (datos.Read())
                {
                    OtraTabla otra = new OtraTabla(Convert.ToInt16(datos["idOT"]), datos["dato"].ToString());
                    lista.Add(otra);
                }
            }
            else
            {
                MySqlDataReader datos = MySQL.ExecuteReaderSP("Select idOT,dato from otratabla where dato LIKE '%" + unTexto + "%'", CommandType.Text);
                while (datos.Read())
                {
                    OtraTabla otra = new OtraTabla(Convert.ToInt16(datos["idOT"]), datos["dato"].ToString());
                    lista.Add(otra);
                }
            }

            return lista;
        }

        public static int BuscarId(string unTexto)
        {
            int valorId = -1;
            if (unTexto == "")
            {
            }
            else
            {
                Object id = MySQL.ExecuteScalarSP("Select idOT from otratabla where dato LIKE '%" + unTexto + "%'", CommandType.Text);
                valorId = Convert.ToInt16(id);

            }

            return valorId;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Capa_datos_ABMC;
using MySqlConnector;
using System.ComponentModel;

namespace Capa_clases_ABMC
{
    public class Personas
    {
        #region atributos
        private int id;
        [Browsable(false)]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string apyn;

        public string Producto
        {
            get { return apyn; }
            set { apyn = value; }
        }
        private string direccion;

        public string Marca
        {
            get { return direccion; }
            set { direccion = value; }
        }
        private string cp;

        public string Precio
        {
            get { return cp; }
            set { cp = value; }
        }
        private string localidad;

        public string Talle
        {
            get { return localidad; }
            set { localidad = value; }
        }
        private DateTime fecNac;

        public DateTime Fecha
        {
            get { return fecNac; }
            set { fecNac = value; }
        }
        private int idOT;
        [Browsable(false)]
        public int IdOT
        {
            get { return idOT; }
            set { idOT = value; }
        }

        private string dato;

        public string Vendedor
        {
            get { return dato; }
            set { dato = value; }
        }

        #endregion 
        #region constructores
        public Personas()
        {
            Id = 0;
            Producto = "n/d";
            Marca = "n/d";
            Precio = "n/d";
            Talle = "n/d";
            Fecha = new DateTime();
            IdOT = 0;
            Vendedor = "n/d";
        }

        public Personas(string unApyn)
        {
            Id = 0;
            Producto = unApyn;
            Marca = "n/d";
            Precio = "n/d";
            Talle = "n/d";
            Fecha = new DateTime();
            IdOT = 0;
            Vendedor = "n/d";
        }

        public Personas(string unApyn, string unaDireccion, string unCP, string unaLocalidd, DateTime unaFecNac, int auxDatoid, string unDato)
        {
            Id = 0;
            Producto = unApyn;
            Marca = unaDireccion;
            Precio = unCP;
            Talle = unaLocalidd;
            Fecha = unaFecNac;
            IdOT = auxDatoid;
            Vendedor = unDato;
        }

        public Personas(int unId, string unProducto, string unaMarca, string unPrecio, string unaTalle, DateTime unaFecha, int unIdDato, string unVendedor)
        {
            Id = unId;
            Producto = unProducto;
            Marca = unaMarca;
            Precio = unPrecio;
            Talle = unaTalle;
            Fecha = unaFecha;
            IdOT = unIdDato;
            Vendedor = unVendedor;
        }

        #endregion
        #region métodos

        public int Guardar()
        {
            try
            {
                object a = MySQL.ExecuteScalarSP("Select max(idPersonas) from personas", CommandType.Text);
                MySQL.ExecuteNonQuerySP("insert into personas (idPersonas,Apyn,Direccion,Localidad,CP,FecNac,idOtraTabla) values (" + (a == DBNull.Value ? 0 : Convert.ToInt16(a) + 1).ToString() + ",'" + this.Producto + "','" + this.Marca + "','" + this.Talle + "','" + this.Precio + "','" + this.Fecha.Year.ToString() + "-" + this.Fecha.Month.ToString() + "-" + this.Fecha.Day.ToString() + "'," + this.IdOT + ")", CommandType.Text);
            }
            catch(MySqlException)
            {
                return -1;
            }
            return 0;
        }

        public int Modificar(string auxApyn, DateTime auxFecnac, string auxDireccion, string auxLocalidad, string auxCp, int auxDatoid)
        {
            try
            {
                MySQL.ExecuteNonQuerySP("update personas set Apyn = '" + auxApyn + "',Direccion = '" + auxDireccion + "',Localidad = '" + auxLocalidad + "', CP = '" + auxCp + "',FecNac='" + auxFecnac.ToString("yyyy'-'MM'-'dd") + "',idOtraTabla = " + auxDatoid + " where idPersonas = " + this.id, CommandType.Text);
                //MySQL.ExecuteNonQuerySP("insert into personas (idPersonas,Apyn,Direccion,Localidad,CP,FecNac,idOtraTabla) values (" + (Convert.ToInt16(a) + 1).ToString() + ",'" + this.Apyn + "','" + this.Direccion + "','" + this.Localidad + "','" + this.CP + "','" + this.FecNac.Year.ToString() + "-" + this.FecNac.Month.ToString() + "-" + this.FecNac.Day.ToString() + "'," + this.IdOT + ")", CommandType.Text);
            }
            catch
            {
                return -1;
            }
            return 0;
        }

        public static List<Personas> Buscar(string unTexto)
        {
            List<Personas> lista = new List<Personas>();
            if (unTexto == "")
            {
                MySqlDataReader datos = MySQL.ExecuteReaderSP("Select idPersonas,Apyn,Direccion,Localidad,CP,FecNac,idOtraTabla,o.dato FROM `ejemplo71`.`personas` p inner join otratabla o on p.idOtraTabla=o.idOT LIMIT 1000;", CommandType.Text);
                while (datos.Read())
                {
                    Personas otra = new Personas(Convert.ToInt16(datos["idPersonas"]), datos["Apyn"].ToString(), datos["Direccion"].ToString(), datos["CP"].ToString(), datos["Localidad"].ToString(), DateTime.Parse(datos["FecNac"].ToString()), Convert.ToInt16(datos["idOtraTabla"]), datos["dato"].ToString());
                    lista.Add(otra);
                }
            }
            else
            {
                MySqlDataReader datos = MySQL.ExecuteReaderSP("SELECT idPersonas,Apyn,Direccion,Localidad,CP,FecNac,idOtraTabla,o.dato FROM `ejemplo71`.`personas` p inner join otratabla o on p.idOtraTabla=o.idOT " + unTexto + " LIMIT 1000;", CommandType.Text);
                while (datos.Read())
                {
                    Personas otra = new Personas(Convert.ToInt16(datos["idPersonas"]), datos["Apyn"].ToString(), datos["Direccion"].ToString(), datos["CP"].ToString(), datos["Localidad"].ToString(), DateTime.Parse(datos["FecNac"].ToString()), Convert.ToInt16(datos["idOtraTabla"].ToString()), datos["dato"].ToString());
                    lista.Add(otra);
                }
            }

            return lista;
        }

        public static int Borrar(int id)
        {
            try
            {
                //int nro = MySQL.ExecuteScalarSP("select idOT from otratabla WHERE dato='" + Dato + "'",CommandType.Text );
                //MySQL.ExecuteNonQuery("insert into Personas (Apyn, Direccion,Localidad,CP,FecNac,idOtraTabla) values ()",CommandType.Text ); \
                MySQL.ExecuteNonQuerySP("delete from personas where idPersonas = " + id , CommandType.Text);
            }
            catch
            {
                return -1;
            }
            return 0;

        }

        public static int BuscarId(string unTexto)
        {
            int valorId = -1;
            if (unTexto == "")
            {

            }
            else
            {
                Object id = MySQL.ExecuteScalarSP("Select idPersonas from personas where Apyn ='" + unTexto + "'", CommandType.Text);
                valorId = Convert.ToInt16(id);

            }

            return valorId;
        }
        #endregion
    }
}
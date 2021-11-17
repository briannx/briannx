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
    public class Base_de_datos
    {
        public static void crear()
        {
            MySQL.ExecuteNonQuerySP("CREATE DATABASE IF NOT EXISTS ejemplo71", CommandType.Text);
            MySQL.conexion += "database = ejemplo71;";
            MySQL.ExecuteNonQuerySP("CREATE TABLE IF NOT EXISTS otratabla (idOT INT(11) NOT NULL AUTO_INCREMENT,Dato VARCHAR(100) NULL DEFAULT NULL COLLATE 'latin1_swedish_ci',PRIMARY KEY (idOT) USING BTREE) COLLATE='latin1_swedish_ci' ENGINE=InnoDB AUTO_INCREMENT=5", CommandType.Text);
            MySQL.ExecuteNonQuerySP("CREATE TABLE IF NOT EXISTS personas (idPersonas INT(11) UNSIGNED ZEROFILL NOT NULL AUTO_INCREMENT,Apyn VARCHAR(100) NULL DEFAULT NULL COLLATE 'latin1_swedish_ci',Direccion VARCHAR(100) NULL DEFAULT NULL COLLATE 'latin1_swedish_ci',Localidad VARCHAR(100) NULL DEFAULT NULL COLLATE 'latin1_swedish_ci',CP VARCHAR(8) NULL DEFAULT NULL COLLATE 'latin1_swedish_ci',FecNac DATE NULL DEFAULT NULL,idOtraTabla INT(11) NULL DEFAULT NULL,PRIMARY KEY (idPersonas) USING BTREE,INDEX FK_personas_otratabla (idOtraTabla) USING BTREE,CONSTRAINT FK_personas_otratabla FOREIGN KEY (idOtraTabla) REFERENCES ejemplo71.otratabla (idOT) ON UPDATE CASCADE ON DELETE RESTRICT) COLLATE='latin1_swedish_ci' ENGINE=InnoDB AUTO_INCREMENT=2", CommandType.Text);
        }
    }
}

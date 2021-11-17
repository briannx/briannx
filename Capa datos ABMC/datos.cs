using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySqlConnector;

namespace Capa_datos_ABMC
{
    public class MySQL
    {


        //string myConnectionString = "Database=pruebaBBDD; Server=localhost; User Id=pruebaBBDDUser;Password=12345;persistsecurityinfo = True;  SslMode = none";
        //MySql.Data.MySqlClient.MySqlConnection myConnection = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
        //string myInsertQuery = "INSERT INTO clientes (nombre, apellidos, dni, direccion, fecnac) Values ('Juan','Perez', '12345678','Direc Juan','20000126')";
        //MySql.Data.MySqlClient.MySqlCommand myCommand = new MySql.Data.MySqlClient.MySqlCommand(myInsertQuery);
        //myCommand.Connection = myConnection;
        //    myConnection.Open();
        //    myCommand.ExecuteNonQuery();
        //    myCommand.Connection.Close();
        //    MessageBox.Show("ok");



        //private static string conexion = "Database=ejemplo71; Server=127.0.0.1; User Id=root; SslMode = none;Convert Zero Datetime=True";
        public static string conexion = "server=localhost;port=3307;user=root;sslmode=none;";
        // Set the connection, command, and then execute the command with non query.  
        public static Int32 ExecuteNonQuery(string commandText, CommandType commandType, params MySqlParameterCollection[] parameters)
        {
            using (MySqlConnection conn = new MySqlConnection(conexion))
            {
                using (MySqlCommand cmd = new MySqlCommand(commandText, conn))
                {
                    // There're three command types: StoredProcedure, Text, TableDirect. The TableDirect   
                    // type is only for OLE DB.    
                    cmd.CommandType = commandType;
                    cmd.Parameters.AddRange(parameters);

                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public static Int32 ExecuteNonQuerySP(string commandText, CommandType commandType)
        {
            using (MySqlConnection conn = new MySqlConnection(conexion))
            {
                using (MySqlCommand cmd = new MySqlCommand(commandText, conn))
                {
                    // There're three command types: StoredProcedure, Text, TableDirect. The TableDirect   
                    // type is only for OLE DB.    
                    cmd.CommandType = commandType;
                    //cmd.Parameters.AddRange(parameters);

                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }


        // Set the connection, command, and then execute the command and only return one value.  
        public static Object ExecuteScalar(String commandText,
            CommandType commandType, params MySqlParameterCollection[] parameters)
        {
            using (MySqlConnection conn = new MySqlConnection(conexion))
            {
                using (MySqlCommand cmd = new MySqlCommand(commandText, conn))
                {
                    cmd.CommandType = commandType;
                    cmd.Parameters.AddRange(parameters);

                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }

        public static Object ExecuteScalarSP(String commandText, CommandType commandType)
        {
            using (MySqlConnection conn = new MySqlConnection(conexion))
            {
                using (MySqlCommand cmd = new MySqlCommand(commandText, conn))
                {
                    cmd.CommandType = commandType;
                    //cmd.Parameters.AddRange(parameters);

                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }
        // Set the connection, command, and then execute the command with query and return the reader.  
        public static MySqlDataReader ExecuteReaderSP(String commandText,
            CommandType commandType)
        {
            MySqlConnection conn = new MySqlConnection(conexion);

            using (MySqlCommand cmd = new MySqlCommand(commandText, conn))
            {
                cmd.CommandType = commandType;
                //cmd.Parameters.AddRange(parameters);

                conn.Open();
                // When using CommandBehavior.CloseConnection, the connection will be closed when the   
                // IDataReader is closed.  
                MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                return reader;
            }
        }

        public static MySqlDataReader ExecuteReader(String commandText,
        CommandType commandType, MySqlParameterCollection param)
        {
            MySqlConnection conn = new MySqlConnection(conexion);

            using (MySqlCommand cmd = new MySqlCommand(commandText, conn))
            {
                cmd.CommandType = commandType;
                foreach (MySqlParameter p in param)
                {
                    cmd.Parameters.AddWithValue(p.ParameterName, p.Value);
                }
                conn.Open();
                // When using CommandBehavior.CloseConnection, the connection will be closed when the   
                // IDataReader is closed.  
                MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                return reader;
            }
        }


    }

}
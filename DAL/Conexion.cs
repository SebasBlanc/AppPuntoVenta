using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Driver para interactuar con un servidor SQL 
using System.Data.SqlClient;
using BLL;
using System.Data;
using System.Net.Http.Headers;

namespace DAL
{
    public class Conexion
    {
        //variable conexion
        private SqlConnection _connection;

        //variable para ejecutar transac-sql del lado del servidor
        private SqlCommand _command;

        //variable para leer datos desde la base datos
        private SqlDataReader _reader;

        //Variable para almacenar los datos del servidor donde nos vamos a conectar
        private string StringConexion;

        /// <summary>
        /// Constructo con parámetros 
        /// </summary>
        /// <param name="pStrConexion"></param>
        public Conexion(string pStrConexion)
        {
                StringConexion = pStrConexion; //se asignan los datos del servidor a conectar
        }


        public Usuario  ValidarUsuario(string pEmail,string pPw)
        {
            try
            {
                //Variable para almacenar los datos de la db
                Usuario usuario = null;

                //Se instancia una conexión
                _connection = new SqlConnection(StringConexion);

                //Se intenta abrir la conexión
                _connection.Open();

                //se instancia un comando  para utilizar el procedimiento almacenado
                _command = new SqlCommand();

                //Se debe asigna siempre la conexión
                _command.Connection = _connection;
            
                //Se indica el tipo de comando que va ejecutar el comando
                _command.CommandType = System.Data.CommandType.StoredProcedure;

                //Se indica el nombre del procedimiento almacenado
                _command.CommandText = "[Sp_Cns_Usuario]";


                //Se deben asignar los valores para los parámetros del procedimiento
                _command.Parameters.AddWithValue("pEmail", pEmail);
                _command.Parameters.AddWithValue("pPassword",pPw);

                //se ejecuta el procedimiento almacenado
                _reader = _command.ExecuteReader();

                //se valida si hay datos 
                if (_reader.Read())
                {
                    //Se instancia  un  object usuario
                    usuario = new Usuario();

                    //se rellenan los datos del objeto con la info de la db
                    usuario.Email = _reader.GetValue(0).ToString();
                    usuario.NombreCompleto = _reader.GetValue(1).ToString();
                    usuario.Password = _reader.GetValue(2).ToString();
                    usuario.FechaRegistro = DateTime.Parse(_reader.GetValue(3).ToString());
                    usuario.Estado = char.Parse(_reader.GetValue(4).ToString());    
                }
                //Siempre se debe cerrar la conexión
                _connection.Close();
                
                //Se deben liberar los recursos
                _connection.Dispose();
                _command.Dispose();
                _reader = null;

                //Se retonar el  object
                return usuario;

            }
            catch (Exception ex)
            {
                //en caso de un error se retorna la variable ex
                throw ex;
            }
        }

        public DataSet BuscarUsuarios(string pNombre)
        {
            try
            {
                DataSet datos = new DataSet();  //Contenerdor para mostrar los datos tabulados
                SqlDataAdapter adapter = new SqlDataAdapter(); //Adaptador encargado de acoplar los datos al DataSet

                _connection = new SqlConnection(StringConexion); //se instancia la conexión
                _connection.Open(); //se abre la conexión
                _command = new SqlCommand(); //se instancia el comando encargado de ejecutar el procedimiento almacenado
                _command.Connection = _connection; //se asigna la conexión al procedimiento almacenado
                _command.CommandType = CommandType.StoredProcedure; //se indica el tipo de comando
                _command.CommandText = "[Sp_Buscar_usuarios]"; //se indica el nombre del procedimiento almacenado a ejecutar
                _command.Parameters.AddWithValue("@Nombre", pNombre); //muy importante darle el valor al parámetro 

                adapter.SelectCommand = _command; //se asigna el comando al adaptador de datos
                adapter.Fill(datos); //se llena el DataSet con los datos del que devuelve el comando

                _connection.Close(); //siempre se debe cerrar la conexión 
                _connection.Dispose();//se liberan los recursos
                _command.Dispose();
                adapter.Dispose();
                
                //se retorna el datos lleno de información
                return datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarUsuario(string pEmail)
        {
            try
            {
                _connection = new SqlConnection(StringConexion); //se instancia la conexión
                _connection.Open();  //se abre la conexión 
                _command = new SqlCommand(); //se instancia el comando
                _command.Connection = _connection; // se asigna la conexión
                _command.CommandType= CommandType.StoredProcedure; //se indica el tipo de comando
                _command.CommandText = "[Sp_Del_Usuarios]"; //se indica el nombre del procedimiento a ejecutar
                _command.Parameters.AddWithValue("@Email",pEmail); //se asigna el valor al parámetro
                _command.ExecuteNonQuery(); //se ejecuta el procedimiento

                _connection.Close(); //siempre se debe cerrar la conexión
                _connection.Dispose (); //se liberan los recursos
                _command.Dispose();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}

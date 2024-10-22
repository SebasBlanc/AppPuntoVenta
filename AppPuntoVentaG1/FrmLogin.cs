using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//importación del  BLL
using BLL;

//importación del DAL
using DAL;

namespace AppPuntoVentaG1
{
    public partial class FrmLogin : Form
    {

        //variable  Objeto de tipo usuario
        private Usuario objUsuario = null;

        //variable para manejar la referencia de la conexion al servidor
        private Conexion _conexion = null;  

        /// <summary>
        /// Constructor por omision del formulario
        /// </summary>
        public FrmLogin()
        {
            InitializeComponent();

            //Se instancia la conexion, se envia como parámetro el string de conexión almacenado dentro del archivo config
            _conexion = new Conexion(ConfigurationManager.ConnectionStrings["StringConexion"].ConnectionString);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Environment.Exit(0); //finaliza la aplicación
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                //se llama al método intento autenticación
                IntentoAutenticacion();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void IntentoAutenticacion()
        {
            try
            {
                //Se crea una  instancia del objeto usuario
                objUsuario = new Usuario();

                //Se rellenan el objeto con los datos escritos a nivel de front-end
                objUsuario.Email = this.txtUsuario.Text.Trim();
                objUsuario.Password = this.txtPassword.Text.Trim();

                //Se realiza la verificación de credenciales
                if (_conexion.ValidarUsuario(objUsuario.Email, objUsuario.Password) != null)
                {
                    this.Close(); //Se cierra el formulario, las credenciales están correctas
                }
                else
                {   //cuando las credenciales fallan
                    LimpiarPantalla();
                    
                    //Se genera una excepción al usuario
                    throw new Exception("Usuario o password incorrectos");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Método encargado de limpiar la pantalla

        private void LimpiarPantalla()
        {
            this.txtUsuario.Clear();
            this.txtPassword.Clear();
        }
    



    }//cierre formulario
}//cierre namespaces

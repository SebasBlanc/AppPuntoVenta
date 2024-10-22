using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DAL;

namespace AppPuntoVentaG1
{
    public partial class FrmBuscarUsuarios : Form
    {
        //variable para manejar la referencia de la conexión
        private Conexion _conexion = null;

        public FrmBuscarUsuarios()
        {
            InitializeComponent();
            //se intancia la conexión y se envia como parámetro del string de conexión almacenado en el archivo AppConfig
            _conexion = new Conexion(ConfigurationManager.ConnectionStrings["StringConexion"].ConnectionString);

        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //cada vez que se escribe se ejecuta el método buscar
                Buscar(this.txtNombre.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void Buscar(string pNombre)
        {
            try
            {
                //se utiliza la clase conexión, usando el método de buscar
                this.dtgDatos.DataSource = _conexion.BuscarUsuarios(pNombre).Tables[0]; //se asigna los datos al datagrid
                this.dtgDatos.AutoResizeColumns(); //se realiza un ajuste automatico del ancho columnas
                this.dtgDatos.ReadOnly = true; //se marcan los datos como solo de lectura
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //ojo le preguntamos al usuario si está seguro de eliminar
                if (MessageBox.Show("Desea eliminar el usuario seleccionado?","Confirmar",
                    MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                {  //se valida que el usuario tenga seleccionada una fila en la lista
                    if (this.dtgDatos.SelectedRows.Count >0)
                    {
                        //se toma el email de la fila seleccionada por el usuario
                        EliminarUsuario(this.dtgDatos.SelectedRows[0].Cells["Email"].Value.ToString());
                    }
                    else
                    {   //se genera un error personalizado  indicando que debe seleccionar una fila
                        throw new Exception("Seleccione la fila del usuario que desea eliminar");
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EliminarUsuario(string email)
        {
            try
            {
                //se utiliza el método para eliminar los datos del usuario en la db
                _conexion.EliminarUsuario(email);

                //se actualiza la lista
                Buscar("");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}

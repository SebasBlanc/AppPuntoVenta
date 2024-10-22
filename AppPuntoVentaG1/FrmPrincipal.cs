using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppPuntoVentaG1
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //se muestra un mensaje de confirmación al usuario
            if (MessageBox.Show("Está seguro de cerrar el sistema?","Confirmar",
                MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //Si confirma se cierra la aplicación
                Environment.Exit(0);
            }
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            //Se muestra  un mensaje de notificación
            notifyIcon1.ShowBalloonTip(25);

            MostrarLogin();
        }

        private  void MostrarLogin()
        {
            //se declara e instancia una variable de tipo formulario login
            FrmLogin formulario = new FrmLogin();

            //se muestra el formulario para el proceso de autenticación
            formulario.ShowDialog();

            //se liberan los recursos
            formulario.Dispose();
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                MostrarBuscarUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void MostrarBuscarUsuarios()
        {
            try
            {
                FrmBuscarUsuarios frm = new FrmBuscarUsuarios();
                frm.ShowDialog();
                frm.Dispose();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }//cierre formulario
} //cierre namespaces

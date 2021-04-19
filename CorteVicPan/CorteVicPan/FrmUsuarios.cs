using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CorteVicPan
{
    public partial class FrmUsuarios : Form
    {
        Conexion con = new Conexion();
        public FrmUsuarios()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            
            string Nombre = txtNombre.Text;
            string Usuario = txtUsuario.Text;
            string Pass = txtPassword.Text;
            string Puesto = txtPuesto.Text;

            MessageBox.Show(con.AgregarUsuario(Nombre, Usuario, Pass, Puesto));

            txtNombre.Clear();
            txtUsuario.Clear();
            txtPassword.Clear();
        }
    }
}

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
    public partial class FrmLogin : Form
    {
        Conexion con = new Conexion();
        Validaciones val = new Validaciones();

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {

            Boolean verUsuario, verPass;

            int IdUsuario = 0;
            string Puesto = null;

            string Usuario = txtUsuario.Text;
            string Pass = txtPassword.Text;

            verUsuario = val.Vacio(txtUsuario);
            verPass = val.Vacio(txtPassword);

            if (verUsuario == true || verPass == true)
            {
                MessageBox.Show("Los campos Usuario y Contraceña son obligatorios");
                txtUsuario.Focus();
            }
            else
            {
                IdUsuario = con.Login(Usuario, Pass);
                Puesto = con.Puesto(Usuario, Pass);

                if (IdUsuario == -1 && Puesto == null)
                {
                    txtPassword.Clear();
                    txtUsuario.Clear();

                    txtUsuario.Focus();

                }
                else
                {
                    FrmPrincipal Principal = new FrmPrincipal(IdUsuario, Puesto);
                    this.Hide();
                    Principal.Show();
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                MessageBox.Show("Presionaste la tecla enter");
            }
        }
    }
}

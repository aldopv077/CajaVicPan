using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace CorteVicPan
{
    public partial class FrmPrincipal : Form
    {
        Conexion con = new Conexion();
        public FrmPrincipal(int IdUsuario,string Puesto)
        {
            InitializeComponent();
            this.IdUsuario = IdUsuario;
            this.Puesto = Puesto;
        }
        int IdUsuario;
        string Puesto;

        private void corteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCortedelDia cortedeldia = new FrmCortedelDia(IdUsuario, Puesto);
            cortedeldia.MdiParent = this;
            cortedeldia.Show();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int IdCorte;
            int IdSucursal;

            //Se Obtiene la fecha y la hora actual y la convertimos en el formato de (aaaa/mm/dd)
            DateTime fecha = DateTime.Now;
            string Fecha = Convert.ToString(fecha.Date.ToShortDateString());

            IdCorte = con.Corte(IdUsuario, Fecha);
            IdSucursal = con.IdSucursal(IdCorte);

            if (IdCorte != 0)
            {
                FrmReporte report = new FrmReporte(IdUsuario, IdCorte);
                report.MdiParent = this;
                report.Show();
            }
            else
            {
                MessageBox.Show("No es posible imprimir sin realizar un corte antes");
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Process[] pArray = Process.GetProcessesByName("CorteVicPan.exe");
            if (pArray.Length != 0)
            {
                pArray[0].Kill();
            }
        }

        private void agregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Puesto == "Administrador")
            {
                FrmUsuarios usuario = new FrmUsuarios();
                FrmCortedelDia cortedia = new FrmCortedelDia(IdUsuario, Puesto);
                usuario.MdiParent = this;
                cortedia.Hide();
                usuario.Show();
            }
            else
            {
                MessageBox.Show("No cuanta con los permisos para agregar usuarios");
            }
        }

        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
            FrmLogin login = new FrmLogin();
            login.Show();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            FrmCortedelDia cd = new FrmCortedelDia(IdUsuario,Puesto);
            cd.MdiParent=this;
            cd.Show();
        }
    }
}

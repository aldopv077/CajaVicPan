using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Reporting.WinForms;

namespace CorteVicPan
{
    public partial class FrmReporte : Form
    {
        public FrmReporte( int idUsuario, int idCorte)
        {
            InitializeComponent();
            this.IdCorte = idCorte;
            this.IdUsuario = idUsuario;
        }

        int IdUsuario, IdCorte;

        private void FrmReporte_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'dsBDCorteVicPan.TblCorte' Puede moverla o quitarla según sea necesario.
            this.TblCorteTableAdapter.Fill(this.dsBDCorteVicPan.TblCorte);
            // TODO: esta línea de código carga datos en la tabla 'dsBDCorteVicPan.TblConteo' Puede moverla o quitarla según sea necesario.
            this.TblConteoTableAdapter.Fill(this.dsBDCorteVicPan.TblConteo);
            // TODO: esta línea de código carga datos en la tabla 'dsBDCorteVicPan.TblFacturas' Puede moverla o quitarla según sea necesario.
            this.TblFacturasTableAdapter.Fill(this.dsBDCorteVicPan.TblFacturas);
            // TODO: esta línea de código carga datos en la tabla 'dsBDCorteVicPan.TblVales' Puede moverla o quitarla según sea necesario.
            this.TblValesTableAdapter.Fill(this.dsBDCorteVicPan.TblVales);
            // TODO: esta línea de código carga datos en la tabla 'dsBDCorteVicPan.TblRetiros' Puede moverla o quitarla según sea necesario.
            this.TblRetirosTableAdapter.Fill(this.dsBDCorteVicPan.TblRetiros);
            // TODO: esta línea de código carga datos en la tabla 'dsBDCorteVicPan.TblUsuarios' Puede moverla o quitarla según sea necesario.
            this.TblUsuariosTableAdapter.Fill(this.dsBDCorteVicPan.TblUsuarios);

            ReportParameter[] parametros = new ReportParameter[2];
            parametros[0] = new ReportParameter("IdCorte", IdCorte.ToString());
            parametros[1] = new ReportParameter("IdUsuario", IdUsuario.ToString());
            rpwCorte.LocalReport.SetParameters(parametros);
            this.rpwCorte.RefreshReport();
        }
    }
}

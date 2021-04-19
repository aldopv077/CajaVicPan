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
    public partial class FrmCortedelDia : Form
    {
        Conexion con = new Conexion();
        Validaciones val = new Validaciones();

        public FrmCortedelDia(int IdUsuario, string Puesto)
        {
            InitializeComponent();
            this.IdUsuario = IdUsuario;
            this.Puesto = Puesto;
        }

        int IdUsuario;
        string Puesto;
        int pocision;

        private void FrmCortedelDia_Load(object sender, EventArgs e)
        {
            rbAgregar.Checked = true;
            cmbUsuarios.Enabled = false;
            btnActualizar.Enabled = false;
            dtpFecha.Enabled = false;

            con.CargarFacturas(dgwFacturas, IdUsuario);
            con.CargarRetiros(dgwRetiros, IdUsuario);
            con.CargarVales(dgwVales, IdUsuario);

            con.LlenarLabel(IdUsuario, lbTotalConteo, lbTotalFac, lbTotalRet, lbTotalVales, lbTotalApl, lbTotalEntregado, lbTotalCorte, lbDiferencia, lbIdCorte);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            double suma = 0;
            double cincu = 0;

            int mil = Convert.ToInt32(txtMil.Text);
            int quin = Convert.ToInt32(txtQuin.Text);
            int dosc = Convert.ToInt32(txtDosc.Text);
            int cien = Convert.ToInt32(txtCien.Text);
            int cincp = Convert.ToInt32(txtCincp.Text);
            int veint = Convert.ToInt32(txtVeint.Text);
            int diez = Convert.ToInt32(txtDiez.Text);
            int cinco = Convert.ToInt32(txtCinco.Text);
            int dos = Convert.ToInt32(txtDos.Text);
            int uno = Convert.ToInt32(txtUno.Text);
            int cinc = Convert.ToInt32(txtCinc.Text);

            if (cmbTurnos.SelectedIndex >-1)
            {
                string Turno = cmbTurnos.SelectedItem.ToString();

                cincu = Convert.ToDouble(cinc) / 2;

                if (rbAgregar.Checked == true)
                {
                    suma = (mil * 1000) + (quin * 500) + (dosc * 200) + (cien * 100) + (cincp * 50) + (veint * 20) + (diez * 10) + (cinco * 5) + (dos * 2) + uno + cincu;

                    MessageBox.Show(con.InsertarConteo(IdUsuario, Turno, mil, quin, dosc, cien, cincp, veint, diez, cinco, dos, uno, cinc, suma));

                    con.LlenarLabel(IdUsuario, lbTotalConteo, lbTotalFac, lbTotalRet, lbTotalVales, lbTotalApl, lbTotalEntregado, lbTotalCorte, lbDiferencia,lbIdCorte);

                    txtMil.Clear();
                    txtQuin.Clear();
                    txtDosc.Clear();
                    txtCien.Clear();
                    txtCincp.Clear();
                    txtVeint.Clear();
                    txtDiez.Clear();
                    txtCinco.Clear();
                    txtDos.Clear();
                    txtUno.Clear();
                    txtCinc.Clear();
                }
            }
            else
            {
                MessageBox.Show("Debe elejir un turno");
            }
        }

        private void btnAgregarFac_Click(object sender, EventArgs e)
        {
            Boolean Ref, Conc, Mont;
            Ref = val.Vacio(txtReferenciaFac);
            Conc = val.Vacio(txtConceptoFac);
            Mont = val.Vacio(txtMontoFac);

            if (Ref != true || Conc != true || Mont != true)
            {
                int Referencia = Convert.ToInt32(txtReferenciaFac.Text);
                double Monto = Convert.ToDouble(txtMontoFac.Text);
                string Concepto = txtConceptoFac.Text;
                double TotalFac = 0;

                if (rbAgregar.Checked == true)
                {
                    TotalFac = con.InsertarFactura(IdUsuario, Referencia, Monto, Concepto);
                    con.CargarFacturas(dgwFacturas, IdUsuario);

                    con.LlenarLabel(IdUsuario, lbTotalConteo, lbTotalFac, lbTotalRet, lbTotalVales, lbTotalApl, lbTotalEntregado, lbTotalCorte, lbDiferencia, lbIdCorte);

                    txtMontoFac.Clear();
                    txtReferenciaFac.Clear();
                    txtConceptoFac.Clear();

                    txtReferenciaFac.Focus();
                }
            }
            else
            {
                MessageBox.Show("Los campos de este apartado son oblogatorios");
                txtReferenciaFac.Focus();
            }
        }

        private void btnEliminarFac_Click(object sender, EventArgs e)
        {
            string reffac = txtReferenciaFac.Text;
            string monto = txtMontoFac.Text;
            if (monto == "" && reffac == "")
            {
                MessageBox.Show("Debe elegir un registro para eliminarlo");
            }
            else
            {
                int RefFac = Convert.ToInt32(txtReferenciaFac.Text);
                double MontFac = Convert.ToDouble(txtMontoFac.Text);

                if (rbAgregar.Checked == true)
                {
                    MessageBox.Show(con.EliminarFactura(RefFac, MontFac, IdUsuario));
                    con.CargarFacturas(dgwFacturas, IdUsuario);
                    con.LlenarLabel(IdUsuario, lbTotalConteo, lbTotalFac, lbTotalRet, lbTotalVales, lbTotalApl, lbTotalEntregado, lbTotalCorte, lbDiferencia, lbIdCorte);
                }

                txtReferenciaFac.Clear();
                txtMontoFac.Clear();

                txtReferenciaFac.Focus();
            }
        }

        private void dgwFacturas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            pocision = dgwFacturas.CurrentRow.Index;
            txtReferenciaFac.Text = dgwFacturas[0, pocision].Value.ToString();
            txtMontoFac.Text = dgwFacturas[2, pocision].Value.ToString();
        }


        private void dgwVales_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            pocision = dgwVales.CurrentRow.Index;
            txtMontoVal.Text = dgwVales[1, pocision].Value.ToString();
        }

        private void btnAgregarVal_Click(object sender, EventArgs e)
        {
            Boolean Conc, Mont;
            Conc = val.Vacio(txtConceptoVal);
            Mont = val.Vacio(txtMontoVal);

            if (Conc != true || Mont != true)
            {
                double Monto = Convert.ToDouble(txtMontoVal.Text);
                string Concepto = txtConceptoVal.Text;
                double TotalVal = 0;

                if (rbAgregar.Checked == true)
                {
                    TotalVal = con.InsertarVales(IdUsuario, Monto, Concepto);

                    con.CargarVales(dgwVales, IdUsuario);

                    con.LlenarLabel(IdUsuario, lbTotalConteo, lbTotalFac, lbTotalRet, lbTotalVales, lbTotalApl, lbTotalEntregado, lbTotalCorte, lbDiferencia, lbIdCorte);

                    txtConceptoVal.Clear();
                    txtMontoVal.Clear();

                    txtConceptoVal.Focus();
                }
            }
            else
            {
                MessageBox.Show("Los campos de este apartado son oblogatorios");
                txtReferenciaRet.Focus();
            }
        }

        private void btnEliminarVal_Click(object sender, EventArgs e)
        {

            string mont = txtMontoVal.Text;
            if (mont == "")
            {
                MessageBox.Show("Debe de elegir un registro para eliminarlo");
            }
            else
            {
                double MontVal = Convert.ToDouble(txtMontoVal.Text);

                if (rbAgregar.Checked == true)
                {
                    MessageBox.Show(con.EliminarVales(MontVal, IdUsuario));
                    con.CargarVales(dgwVales, IdUsuario);
                    con.LlenarLabel(IdUsuario, lbTotalConteo, lbTotalFac, lbTotalRet, lbTotalVales, lbTotalApl, lbTotalEntregado, lbTotalCorte, lbDiferencia, lbIdCorte);
                }

                txtMontoVal.Clear();

                txtConceptoVal.Focus();
            }
        }

        private void btnAgregarRet_Click(object sender, EventArgs e)
        {
            Boolean Ref, Conc, Mont;
            Ref = val.Vacio(txtReferenciaRet);
            Conc = val.Vacio(txtConceptoRet);
            Mont = val.Vacio(txtMontoRet);

            if (Ref != true || Conc != true || Mont != true)
            {
                int Referencia = Convert.ToInt32(txtReferenciaRet.Text);
                double Monto = Convert.ToDouble(txtMontoRet.Text);
                string Concepto = txtConceptoRet.Text;
                double TotalRet = 0;

                if (rbAgregar.Checked == true)
                {
                    TotalRet = con.InsertarRetiro(IdUsuario, Referencia, Monto, Concepto);

                    con.CargarRetiros(dgwRetiros, IdUsuario);

                    con.LlenarLabel(IdUsuario, lbTotalConteo, lbTotalFac, lbTotalRet, lbTotalVales, lbTotalApl, lbTotalEntregado, lbTotalCorte, lbDiferencia, lbIdCorte);

                    txtConceptoRet.Clear();
                    txtMontoRet.Clear();
                    txtReferenciaRet.Clear();

                    txtReferenciaRet.Focus();
                }
            }
            else
            {
                MessageBox.Show("Los campos de este apartado son oblogatorios");
                txtReferenciaRet.Focus();
            }
        }

        private void dgwRetiros_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            pocision = dgwRetiros.CurrentRow.Index;
            txtReferenciaRet.Text = dgwRetiros[0, pocision].Value.ToString();
            txtMontoRet.Text = dgwRetiros[2, pocision].Value.ToString();
        }

        private void btnEliminatRet_Click(object sender, EventArgs e)
        {
            string refret = txtReferenciaRet.Text;
            string mont = txtMontoRet.Text;
            if (refret == "" && mont == "")
            {
                MessageBox.Show("Debe de elegir un registro para eliminarlo");
            }
            else
            {
                int RefRet = Convert.ToInt32(txtReferenciaRet.Text);
                double MontRet = Convert.ToDouble(txtMontoRet.Text);

                if (rbAgregar.Checked == true)
                {
                    MessageBox.Show(con.EliminarRetiros(RefRet, MontRet, IdUsuario));
                    con.CargarRetiros(dgwRetiros, IdUsuario);
                    con.LlenarLabel(IdUsuario, lbTotalConteo, lbTotalFac, lbTotalRet, lbTotalVales, lbTotalApl, lbTotalEntregado, lbTotalCorte, lbDiferencia, lbIdCorte);
                }

                txtReferenciaRet.Clear();
                txtMontoRet.Clear();

                txtReferenciaRet.Focus();
            }
        }

        private void btnAgregarApl_Click(object sender, EventArgs e)
        {

            Boolean Conc, Mont;
            Conc = val.Vacio(txtConceptoApl);
            Mont = val.Vacio(txtMontoApl);

            if (Conc != true || Mont != true)
            {
                double Monto = Convert.ToDouble(txtMontoApl.Text);
                string Concepto = txtConceptoApl.Text;
                double TotalApl = 0;
                int idus = cmbUsuarios.SelectedIndex;

                TotalApl = con.InsertarAplicaciones(idus, Monto, Concepto);

                con.CargarAplicaciones(dgwAplicaciones, idus);

                con.LlenarLabel(IdUsuario, lbTotalConteo, lbTotalFac, lbTotalRet, lbTotalVales, lbTotalApl, lbTotalEntregado, lbTotalCorte, lbDiferencia, lbIdCorte);

                txtMontoApl.Clear();
                txtConceptoApl.Clear();

                txtConceptoApl.Focus();
            }
            else
            {
                MessageBox.Show("Los campos de este apartado son oblogatorios");
                txtConceptoApl.Focus();
            }
        }

        private void dgwAplicaciones_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            pocision = dgwAplicaciones.CurrentRow.Index;
            txtMontoApl.Text = dgwAplicaciones[1, pocision].Value.ToString();
        }

        private void btnEliminarApl_Click(object sender, EventArgs e)
        {
            int idus = cmbUsuarios.SelectedIndex;
            if (idus == -1)
            {
                MessageBox.Show("Debe seleccionar un usuario");
            }
            else
            {
                string mont = txtMontoApl.Text;
                if (mont == "")
                {
                    MessageBox.Show("Debe elegir un registro para eliminar");
                }
                else
                {
                    double MontApl = Convert.ToDouble(txtMontoApl.Text);
                    string Fecha = dtpFecha.Text;
                    if (rbBuscar.Checked == true)
                    {
                        MessageBox.Show(con.EliminarAplicaciones(MontApl, idus, Fecha));
                        con.CargarAplicaciones(dgwAplicaciones, idus);
                        con.LlenarLabel(IdUsuario, lbTotalConteo, lbTotalFac, lbTotalRet, lbTotalVales, lbTotalApl, lbTotalEntregado, lbTotalCorte, lbDiferencia, lbIdCorte);
                    }

                    txtMontoApl.Clear();

                    txtConceptoApl.Focus();
                }
            }
        }

        private void txtMil_KeyPress(object sender, KeyPressEventArgs e)
        {
            val.Numeros(e);
        }

        private void txtQuin_KeyPress(object sender, KeyPressEventArgs e)
        {
            val.Numeros(e);
        }

        private void txtDosc_KeyPress(object sender, KeyPressEventArgs e)
        {
            val.Numeros(e);
        }

        private void txtCien_KeyPress(object sender, KeyPressEventArgs e)
        {
            val.Numeros(e);
        }

        private void txtCincp_KeyPress(object sender, KeyPressEventArgs e)
        {
            val.Numeros(e);
        }

        private void txtVein_KeyPress(object sender, KeyPressEventArgs e)
        {
            val.Numeros(e);
        }

        private void txtDiez_KeyPress(object sender, KeyPressEventArgs e)
        {
            val.Numeros(e);
        }

        private void txtCinco_KeyPress(object sender, KeyPressEventArgs e)
        {
            val.Numeros(e);
        }

        private void txtDos_KeyPress(object sender, KeyPressEventArgs e)
        {
            val.Numeros(e);
        }

        private void txtUno_KeyPress(object sender, KeyPressEventArgs e)
        {
            val.Numeros(e);
        }

        private void txtCinc_KeyPress(object sender, KeyPressEventArgs e)
        {
            val.Numeros(e);
        }

        private void txtReferenciaFac_KeyPress(object sender, KeyPressEventArgs e)
        {
            val.Numeros(e);
        }

        private void txtReferenciaRet_KeyPress(object sender, KeyPressEventArgs e)
        {
            val.Numeros(e);
        }

        private void txtConceptoFac_KeyPress(object sender, KeyPressEventArgs e)
        {
            val.Letras(e);
        }

        private void txtConceptoRet_KeyPress(object sender, KeyPressEventArgs e)
        {
            val.Letras(e);
        }

        private void txtConceptoVal_KeyPress(object sender, KeyPressEventArgs e)
        {
            val.Letras(e);
        }

        private void txtConceptoApl_KeyPress(object sender, KeyPressEventArgs e)
        {
            val.Letras(e);
        }
        private void txtMontoFac_KeyPress(object sender, KeyPressEventArgs e)
        {
            val.cantidad(e);
        }

        private void txtMontoRet_KeyPress(object sender, KeyPressEventArgs e)
        {
            val.cantidad(e);
        }

        private void txtMontoVal_KeyPress(object sender, KeyPressEventArgs e)
        {
            val.cantidad(e);
        }

        private void txtMontoApl_KeyPress(object sender, KeyPressEventArgs e)
        {
            val.cantidad(e);
        }

        private void txtImporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            val.cantidad(e);
        }

        private void txtMil_Validated(object sender, EventArgs e)
        {
            Boolean vacio;
            vacio = val.Vacio(txtMil);

            if (vacio == true)
            {
                MessageBox.Show("El Campo 1000 no puede estar vacio");
                txtMil.Focus();
            }
        }

        private void txtQuin_Validated(object sender, EventArgs e)
        {

            Boolean vacio;
            vacio = val.Vacio(txtQuin);

            if (vacio == true)
            {
                MessageBox.Show("El Campo 500 no puede estar vacio");
                txtQuin.Focus();
            }
        }

        private void txtDosc_Validated(object sender, EventArgs e)
        {

            Boolean vacio;
            vacio = val.Vacio(txtDosc);

            if (vacio == true)
            {
                MessageBox.Show("El Campo 200 no puede estar vacio");
                txtDosc.Focus();
            }
        }

        private void txtCien_Validated(object sender, EventArgs e)
        {
            Boolean vacio;
            vacio = val.Vacio(txtCien);

            if (vacio == true)
            {
                MessageBox.Show("El Campo 100 no puede estar vacio");
                txtCien.Focus();
            }
        }

        private void txtCincp_Validated(object sender, EventArgs e)
        {

            Boolean vacio;
            vacio = val.Vacio(txtCincp);

            if (vacio == true)
            {
                MessageBox.Show("El Campo 50 no puede estar vacio");
                txtCincp.Focus();
            }
        }

        private void txtVein_Validated(object sender, EventArgs e)
        {

            Boolean vacio;
            vacio = val.Vacio(txtVeint);

            if (vacio == true)
            {
                MessageBox.Show("El Campo 20 no puede estar vacio");
                txtVeint.Focus();
            }
        }

        private void txtDiez_Validated(object sender, EventArgs e)
        {

            Boolean vacio;
            vacio = val.Vacio(txtDiez);

            if (vacio == true)
            {
                MessageBox.Show("El Campo 10 no puede estar vacio");
                txtDiez.Focus();
            }
        }

        private void txtCinco_Validated(object sender, EventArgs e)
        {

            Boolean vacio;
            vacio = val.Vacio(txtCinco);

            if (vacio == true)
            {
                MessageBox.Show("El Campo 5 no puede estar vacio");
                txtCinco.Focus();
            }
        }

        private void txtDos_Validated(object sender, EventArgs e)
        {

            Boolean vacio;
            vacio = val.Vacio(txtDos);

            if (vacio == true)
            {
                MessageBox.Show("El Campo 2 no puede estar vacio");
                txtDos.Focus();
            }
        }

        private void txtUno_Validated(object sender, EventArgs e)
        {

            Boolean vacio;
            vacio = val.Vacio(txtUno);

            if (vacio == true)
            {
                MessageBox.Show("El Campo 1 no puede estar vacio");
                txtUno.Focus();
            }
        }

        private void txtCinc_Validated(object sender, EventArgs e)
        {

            Boolean vacio;
            vacio = val.Vacio(txtCinc);

            if (vacio == true)
            {
                MessageBox.Show("El Campo 0.50 no puede estar vacio");
                txtCinc.Focus();
            }
        }

        private void btnAgregarImporte_Click(object sender, EventArgs e)
        {
            Boolean Mont;
            Mont = val.Vacio(txtImporte);

            if (Mont != true)
            {
                double importe = Convert.ToDouble(txtImporte.Text);
                double Diferencia = 0;

                if (rbAgregar.Checked == true)
                {
                    Diferencia = con.Diferencia(importe, IdUsuario);
                    con.LlenarLabel(IdUsuario, lbTotalConteo, lbTotalFac, lbTotalRet, lbTotalVales, lbTotalApl, lbTotalEntregado, lbTotalCorte, lbDiferencia, lbIdCorte);
                    txtImporte.Clear();
                }
            }
        }

        private void txtReferenciaFac_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            val.Numeros(e);
        }

        private void txtConceptoFac_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            val.Letras(e);
        }

        private void txtMontoFac_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            val.cantidad(e);
        }

        private void txtConceptoVal_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            val.Letras(e);
        }

        private void txtMontoVal_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            val.cantidad(e);
        }

        private void txtReferenciaRet_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            val.Numeros(e);
        }

        private void txtConceptoRet_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            val.Letras(e);
        }

        private void txtMontoRet_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            val.cantidad(e);
        }

        private void txtMil_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            val.Numeros(e);
        }

        private void txtQuin_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            val.Numeros(e);
        }

        private void txtDosc_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            val.Numeros(e);
        }

        private void txtCien_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            val.Numeros(e);
        }

        private void txtCincp_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            val.Numeros(e);
        }

        private void txtVeint_KeyPress(object sender, KeyPressEventArgs e)
        {
            val.Numeros(e);
        }

        private void txtDiez_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            val.Numeros(e);
        }

        private void txtCinco_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            val.Numeros(e);
        }

        private void txtDos_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            val.Numeros(e);
        }

        private void txtUno_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            val.Numeros(e);
        }

        private void txtCinc_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            val.Numeros(e);
        }

        private void txtMil_Validated_1(object sender, EventArgs e)
        {

            Boolean vacio;
            vacio = val.Vacio(txtMil);

            if (vacio == true)
            {
                MessageBox.Show("El Campo 1000 no puede estar vacio");
                txtMil.Focus();
            }
        }

        private void txtQuin_Validated_1(object sender, EventArgs e)
        {
            Boolean vacio;
            vacio = val.Vacio(txtQuin);

            if (vacio == true)
            {
                MessageBox.Show("El Campo 500 no puede estar vacio");
                txtQuin.Focus();
            }
        }

        private void txtDosc_Validated_1(object sender, EventArgs e)
        {
            Boolean vacio;
            vacio = val.Vacio(txtDosc);

            if (vacio == true)
            {
                MessageBox.Show("El Campo 200 no puede estar vacio");
                txtDosc.Focus();
            }
        }

        private void txtCien_Validated_1(object sender, EventArgs e)
        {
            Boolean vacio;
            vacio = val.Vacio(txtCien);

            if (vacio == true)
            {
                MessageBox.Show("El Campo 100 no puede estar vacio");
                txtCien.Focus();
            }
        }

        private void txtCincp_Validated_1(object sender, EventArgs e)
        {
            Boolean vacio;
            vacio = val.Vacio(txtCincp);

            if (vacio == true)
            {
                MessageBox.Show("El Campo 50 no puede estar vacio");
                txtCincp.Focus();
            }
        }

        private void txtVeint_Validated(object sender, EventArgs e)
        {
            Boolean vacio;
            vacio = val.Vacio(txtVeint);

            if (vacio == true)
            {
                MessageBox.Show("El Campo 20 no puede estar vacio");
                txtVeint.Focus();
            }
        }

        private void txtDiez_Validated_1(object sender, EventArgs e)
        {
            Boolean vacio;
            vacio = val.Vacio(txtDiez);

            if (vacio == true)
            {
                MessageBox.Show("El Campo 10 no puede estar vacio");
                txtDiez.Focus();
            }
        }

        private void txtCinco_Validated_1(object sender, EventArgs e)
        {
            Boolean vacio;
            vacio = val.Vacio(txtCinco);

            if (vacio == true)
            {
                MessageBox.Show("El Campo 5 no puede estar vacio");
                txtCinco.Focus();
            }
        }

        private void txtDos_Validated_1(object sender, EventArgs e)
        {
            Boolean vacio;
            vacio = val.Vacio(txtDos);

            if (vacio == true)
            {
                MessageBox.Show("El Campo 2 no puede estar vacio");
                txtDos.Focus();
            }
        }

        private void txtUno_Validated_1(object sender, EventArgs e)
        {
            Boolean vacio;
            vacio = val.Vacio(txtUno);

            if (vacio == true)
            {
                MessageBox.Show("El Campo 1 no puede estar vacio");
                txtUno.Focus();
            }
        }

        private void txtCinc_Validated_1(object sender, EventArgs e)
        {
            Boolean vacio;
            vacio = val.Vacio(txtCinc);

            if (vacio == true)
            {
                MessageBox.Show("El Campo 0.50 no puede estar vacio");
                txtCinc.Focus();
            }
        }

        private void rbAgregar_CheckedChanged(object sender, EventArgs e)
        {

            btnActualizar.Enabled = false;
            cmbUsuarios.Enabled = false;
            dtpFecha.Enabled = false;
            txtConceptoApl.Enabled = false;
            txtMontoApl.Enabled = false;
            btnAgregarApl.Enabled = false;
            btnEliminarApl.Enabled = false;
            txtNoCorte.Enabled = false;

            txtCien.Enabled = true;
            txtCinc.Enabled = true;
            txtCinco.Enabled = true;
            txtCincp.Enabled = true;
            txtConceptoFac.Enabled = true;
            txtConceptoRet.Enabled = true;
            txtConceptoVal.Enabled = true;
            txtDiez.Enabled = true;
            txtDos.Enabled = true;
            txtDosc.Enabled = true;
            txtImporte.Enabled = true;
            txtMil.Enabled = true;
            txtMontoFac.Enabled = true;
            txtMontoRet.Enabled = true;
            txtMontoVal.Enabled = true;
            txtQuin.Enabled = true;
            txtReferenciaFac.Enabled = true;
            txtReferenciaRet.Enabled = true;
            txtUno.Enabled = true;
            txtVeint.Enabled = true;
            btnAgregar.Enabled = true;
            btnEliminarFac.Enabled = true;
            btnAgregarFac.Enabled = true;
            btnEliminarVal.Enabled = true;
            btnAgregarImporte.Enabled = true;
            btnEliminatRet.Enabled = true;
            btnAgregarVal.Enabled = true;
            btnAgregarRet.Enabled = true;
            btnEliminatRet.Enabled = true;
        }

        private void rbBuscar_CheckedChanged(object sender, EventArgs e)
        {
            if (Puesto == "Administrador")
            {
                btnActualizar.Enabled = true;
                cmbUsuarios.Enabled = true;
                dtpFecha.Enabled = true;
                txtConceptoApl.Enabled = true;
                txtMontoApl.Enabled = true;
                btnAgregarApl.Enabled = true;
                btnEliminarApl.Enabled = true;
                txtNoCorte.Enabled = true;

                txtCien.Enabled = false;
                txtCinc.Enabled = false;
                txtCinco.Enabled = false;
                txtCincp.Enabled = false;
                txtConceptoFac.Enabled = false;
                txtConceptoRet.Enabled = false;
                txtConceptoVal.Enabled = false;
                txtDiez.Enabled = false;
                txtDos.Enabled = false;
                txtDosc.Enabled = false;
                txtImporte.Enabled = false;
                txtMil.Enabled = false;
                txtMontoFac.Enabled = false;
                txtMontoRet.Enabled = false;
                txtMontoVal.Enabled = false;
                txtQuin.Enabled = false;
                txtReferenciaFac.Enabled = false;
                txtReferenciaRet.Enabled = false;
                txtUno.Enabled = false;
                txtVeint.Enabled = false;
                btnAgregar.Enabled = false;
                btnEliminarFac.Enabled = false;
                btnAgregarFac.Enabled = false;
                btnEliminarVal.Enabled = false;
                btnAgregarImporte.Enabled = false;
                btnEliminatRet.Enabled = false;
                btnAgregarVal.Enabled = false;
                btnAgregarRet.Enabled = false;
                btnEliminatRet.Enabled = false;

                con.llenarCombo(cmbUsuarios);
            }
            else
            {
                MessageBox.Show("Solo el administrador puede buscar");
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Boolean nocorte = val.Vacio(txtNoCorte);
            int combo = val.valbombobox(cmbUsuarios);

            if (nocorte == false && combo==-1)
            {
                int IdCorte = Convert.ToInt32(txtNoCorte.Text);

                string IdUsuario = cmbUsuarios.SelectedIndex.ToString();
                int idus = Convert.ToInt32(IdUsuario);
                string Fecha = dtpFecha.Text;

                con.BCargarAplicaciones(dgwAplicaciones, idus, Fecha, IdCorte);
                con.BCargarFacturas(dgwFacturas, idus, Fecha, IdCorte);
                con.BCargarRetiros(dgwRetiros, idus, Fecha, IdCorte);
                con.BCargarVales(dgwVales, idus, Fecha, IdCorte);

                con.BuscarCorte(IdCorte, idus, Fecha, lbTotalConteo, lbTotalFac, lbTotalRet, lbTotalVales, lbTotalApl, lbTotalEntregado, lbTotalCorte, lbDiferencia, lbIdCorte);
            }
            else if(nocorte==true && combo>-1)
            {
                int IdCorte = 0;

                string IdUsuario = cmbUsuarios.SelectedIndex.ToString();
                int idus = Convert.ToInt32(IdUsuario);
                string Fecha = dtpFecha.Text;

                con.BCargarAplicaciones(dgwAplicaciones, idus, Fecha, IdCorte);
                con.BCargarFacturas(dgwAplicaciones, idus, Fecha, IdCorte);
                con.BCargarRetiros(dgwAplicaciones, idus, Fecha, IdCorte);
                con.BCargarVales(dgwAplicaciones, idus, Fecha, IdCorte);

                con.BuscarCorte(IdCorte, idus, Fecha, lbTotalConteo, lbTotalFac, lbTotalRet, lbTotalVales, lbTotalApl, lbTotalEntregado, lbTotalCorte, lbDiferencia, lbIdCorte);
            }
        }
    }
}

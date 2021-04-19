using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Windows.Forms;
using CorteVicPan.Properties;
using System.Configuration;

namespace CorteVicPan
{
    class Conexion
    {
        SqlConnection cn;
        SqlCommand cmd;
        SqlCommand consulta;
        SqlCommand Facturas, Retiros, Vales, Aplicaciones, Cortes;
        SqlCommand DelFac, DelRet, DelVal, DelApl;
        SqlCommand ActCorte;
        SqlDataReader dr;
        DataTable dt;
        SqlDataAdapter adap;


        public static string ObtenerConexion()
        {
            return Settings.Default.BDCorteVicPanConnectionString;
        }

        /*public Conexion()
        {
            try
            {
                cn = new SqlConnection(ObtenerConexion());
                cn.Open();
                //MessageBox.Show("Conectado");
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se conecto con la BD" + ex.ToString());
            }
        }*/


        public Conexion()
        {
            try
            {
                cn = new SqlConnection("Data Source=Aldo-PC; Initial Catalog=BDCorteVicPan; User ID=sa; Password=12345678");
                cn.Open();
                //MessageBox.Show("Conectado");
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se conecto con la BD" + ex.ToString());
            }
        }


        //CONTEO
        public string InsertarConteo(int idUs, String Turno, int mil, int quin, int dosc, int cien, int cincp, int veint, int diez, int cinco, int dos, int uno, int cinc, double Total)
        {
            string salida = "Se ha insertado con exito";
            int contador = 0;
            int ContadorCorte = 0;
            try
            {
                consulta = new SqlCommand("SELECT TOP 1 IdConteo FROM TblConteo ORDER BY IdConteo DESC", cn);
                dr = consulta.ExecuteReader();
                if (dr.Read())
                {
                    contador = Convert.ToInt32(dr["IdConteo"]);
                    contador += 1;
                }
                else
                    contador = 1;
                dr.Close();

                DateTime fecha = DateTime.Now;
                string Fecha = Convert.ToString(fecha.Date.ToShortDateString());

                cmd = new SqlCommand("Insert into TblConteo(IdConteo,IdUsuario,Mil,Quinientos,Docientos,Cien, Cincuenta,Veinte,Diez,Cinco,Dos,Uno,CincuentaCen,TotalConteo,FechaCon) Values (" + contador + "," + idUs + "," + mil + "," + quin + "," + dosc + "," + cien + "," + cincp + "," + veint + "," + diez + "," + cinco + "," + dos + "," + uno + "," + cinc + "," + Total + " ,'" + Fecha + "')", cn);
                cmd.ExecuteNonQuery();


                consulta = new SqlCommand("SELECT TOP 1 IdCorte FROM TblCorte ORDER BY IdCorte DESC", cn);
                dr = consulta.ExecuteReader();
                if (dr.Read())
                {
                    ContadorCorte = Convert.ToInt32(dr["IdCorte"]);
                    ContadorCorte += 1;
                }
                else
                    ContadorCorte = 1;
                dr.Close();

                Cortes = new SqlCommand("INSERT INTO TblCorte (IdCorte,IdUsuario,IdConteo,Turno,TotalConteo,TotalCorte,Fecha) VALUES (" + ContadorCorte + "," + idUs + "," + contador + ",'" + Turno + "'," + Total + "," + Total + ",'" + Fecha + "')", cn);
                Cortes.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                salida = "No se conecto: " + e.ToString();
            }
            return salida;
        }

        //FACTURAS

        public double InsertarFactura(int idusuario, int Referencia, double Monto, string concepto)
        {
            //string salida = "Se ha agregado la factura con referencia: " + Referencia;
            double TFacturas = 0;
            try
            {
                int ContadorCorte = 0;
                int contador = 0;
                double TotalCorte = 0;

                //Seleccionar el ultimo IdFactura de la BD
                consulta = new SqlCommand("SELECT TOP 1 IdFacturas FROM TblFacturas ORDER BY IdFacturas DESC", cn);
                dr = consulta.ExecuteReader();
                if (dr.Read())
                {
                    //Si existe una factura toma el valdor del campo IdFactura y lo incrementa un numero mas
                    contador = Convert.ToInt32(dr["IdFacturas"]);
                    contador += 1;
                }
                else
                    //Si no exite nunguna factura tomará el valor de uno
                    contador = 1;
                dr.Close();


                //Se obtiene el ultimo idCorte de agregado.
                consulta = new SqlCommand("SELECT TOP 1 IdCorte,TotalCorte FROM TblCorte ORDER BY IdCorte DESC", cn);
                dr = consulta.ExecuteReader();
                if (dr.Read())
                {
                    ContadorCorte = Convert.ToInt32(dr["IdCorte"]);
                    TotalCorte = Convert.ToDouble(dr["TotalCorte"]);
                }
                dr.Close();

                //Tomamos la Fecha y hora actual y la convertimos en formato corte de (aaaa/mm/dd)
                DateTime fecha = DateTime.Now;
                string Fecha = Convert.ToString(fecha.Date.ToShortDateString());

                //Se Realiza la inserción de la factura. 
                cmd = new SqlCommand("INSERT INTO TblFacturas (IdFacturas,IdUsuario,IdCorte,ReferenciaFac,ConceptoFac,MontoFac,FechaFac) Values (" + contador + "," + idusuario + "," + ContadorCorte + "," + Referencia + ",'" + concepto + "'," + Monto + ",'" + Fecha + "')", cn);
                cmd.ExecuteNonQuery();

                //Obtendremos el total de las facturas agregadas por el usuario tomando como parametros el Id del usuario y la Fecha en la que se agrego la factura.
                Facturas = new SqlCommand("SELECT MontoFac FROM TblFacturas WHERE FechaFac='" + Fecha + "' AND IdUsuario=" + idusuario + "", cn);
                dr = Facturas.ExecuteReader();
                while (dr.Read())
                {
                    TFacturas += Convert.ToDouble(dr["MontoFac"]);
                }
                dr.Close();

                TotalCorte += Monto;

                //Se Actualiza el corte añadiendo solamente el total de las facturas agregadas 
                Cortes = new SqlCommand("UPDATE TblCorte SET TotalFacturas=" + TFacturas + ", TotalCorte=" + TotalCorte + " WHERE IdCorte=" + ContadorCorte + "", cn);
                Cortes.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("No se conectó" + e.ToString());
            }
            return TFacturas;
        }

        //VALES
        public double InsertarVales(int idUsuario, double Monto, string Concepto)
        {

            double TVale = 0;

            try
            {
                int contador = 0;
                int ContadorCorte = 0;
                double TotalCorte = 0;

                //Obtenemos el ultimo IdVale para inclementarlo y que sea el siguiente IdVale
                consulta = new SqlCommand("SELECT TOP 1 IdVales FROM TblVales ORDER BY IdVales DESC", cn);
                dr = consulta.ExecuteReader();
                if (dr.Read())
                {
                    contador = Convert.ToInt32(dr["IdVales"]);
                    contador += 1;
                }
                else
                    contador = 1;
                dr.Close();


                //Se obtiene el ultimo idCorte de agregado.
                consulta = new SqlCommand("SELECT TOP 1 IdCorte, TotalCorte FROM TblCorte ORDER BY IdCorte DESC", cn);
                dr = consulta.ExecuteReader();
                if (dr.Read())
                {
                    ContadorCorte = Convert.ToInt32(dr["IdCorte"]);
                    TotalCorte = Convert.ToDouble(dr["TotalCorte"]);
                }
                dr.Close();

                //Obtenemos la fecha y hora actual y la convertimos en formato de (aaaa/mm/dd)
                DateTime fecha = DateTime.Now;
                string Fecha = Convert.ToString(fecha.Date.ToShortDateString());

                //Realizamos la inserción del vale 
                cmd = new SqlCommand("INSERT INTO TblVales (IdVales,IdUsuario,IdCorte,ConceptoVal,MontoVal,FechaVal) Values (" + contador + "," + idUsuario + "," + ContadorCorte + ",'" + Concepto + "'," + Monto + ",'" + Fecha + "')", cn);
                cmd.ExecuteNonQuery();

                //Obtendremos el total de los vales agregados por el usuario tomando como parametros el Id del usuario y la Fecha en la que se agrego el vale.
                Vales = new SqlCommand("SELECT MontoVal FROM TblVales WHERE FechaVal='" + Fecha + "' AND IdUsuario=" + idUsuario + "", cn);
                dr = Vales.ExecuteReader();

                while (dr.Read())
                {
                    TVale += Convert.ToDouble(dr["MontoVal"]);
                }
                dr.Close();

                TotalCorte += Monto;

                //Se Actualiza el corte añadiendo solamente el total de las facturas agregadas 
                Cortes = new SqlCommand("UPDATE TblCorte SET TotalVales=" + TVale + ", TotalCorte=" + TotalCorte + " WHERE IdCorte=" + ContadorCorte + "", cn);
                Cortes.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("No se conectó" + e.ToString());
            }

            return TVale;
        }

        //APLICACIONES
        public double InsertarAplicaciones(int idUsuario, double Monto, string Concepto)
        {

            double TAplicaciones = 0;

            try
            {
                int contador = 0;
                int ContadorCorte = 0;
                double TotalCorte = 0;
                double MontoPos = 0;
                double CorteEntregado = 0;

                if (Monto < 0)
                    MontoPos = Monto * (-1);
                else
                    MontoPos = Monto;

                //Obtenemos el ultimo IdAplicaciones para incrementarlo y el resultdo sea el suguiente id
                consulta = new SqlCommand("SELECT TOP 1 IdAplicaciones FROM TblAplicaciones ORDER BY IdAplicaciones DESC", cn);
                dr = consulta.ExecuteReader();
                if (dr.Read())
                {
                    contador = Convert.ToInt32(dr["IdAplicaciones"]);
                    contador += 1;
                }
                else
                    contador = 1;
                dr.Close();

                //Se obtiene el ultimo idCorte de agregado.
                consulta = new SqlCommand("SELECT TOP 1 IdCorte, TotalCorte, CorteEntregado FROM TblCorte ORDER BY IdCorte DESC", cn);
                dr = consulta.ExecuteReader();
                if (dr.Read())
                {
                    ContadorCorte = Convert.ToInt32(dr["IdCorte"]);
                    TotalCorte = Convert.ToDouble(dr["TotalCorte"]);
                    CorteEntregado = Convert.ToDouble(dr["CorteEntregado"]);
                }
                dr.Close();

                //Obtenemos la fecha y hora actual y la convertimos en el formato de (aaaa/mm/dd)
                DateTime fecha = DateTime.Now;
                string Fecha = Convert.ToString(fecha.Date.ToShortDateString());

                //Se realiza la insercion de la aplicación
                cmd = new SqlCommand("INSERT INTO TblAplicaciones (IdAplicaciones,IdUsuario,IdCorte,ConceptoApl,MontoApl,FechaApl) Values (" + contador + "," + idUsuario + "," + ContadorCorte + ",'" + Concepto + "'," + MontoPos + ",'" + Fecha + "')", cn);
                cmd.ExecuteNonQuery();

                //Obtendremos el total de las aplicaciones agregadas por el usuario tomando como parametros el Id del usuario y la Fecha en la que se agrego la aplicación.
                Aplicaciones = new SqlCommand("SELECT MontoApl FROM TblAplicaciones WHERE FechaApl='" + Fecha + "' AND IdUsuario=" + idUsuario + "", cn);
                dr = Aplicaciones.ExecuteReader();
                while (dr.Read())
                {
                    TAplicaciones += Convert.ToDouble(dr["MontoApl"]);
                }
                dr.Close();

                TotalCorte += Monto;
                double Diferecia = TotalCorte - CorteEntregado;

                //Se Actualiza el corte añadiendo solamente el total de las aplicaciones agregadas 
                Cortes = new SqlCommand("UPDATE TblCorte SET TotalAplicaciones=" + TAplicaciones + ", TotalCorte=" + TotalCorte + ", Diferencia=" + Math.Round(Diferecia, 2) + " WHERE IdCorte=" + ContadorCorte + "", cn);
                Cortes.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("No se conectó" + e.ToString());
            }
            return TAplicaciones;
        }

        //RETIROS
        public double InsertarRetiro(int idusuario, int Referencia, double Monto, string concepto)
        {
            double TRetiros = 0;

            try
            {
                int contador = 0;
                int ContadorCorte = 0;
                double TotalCorte = 0;

                //Obtenemos el ultimi IdRetiro para incrementarlo y hacerlo el nuevo IdRetiro
                consulta = new SqlCommand("SELECT TOP 1 IdRetiros FROM TblRetiros ORDER BY IdRetiros DESC", cn);
                dr = consulta.ExecuteReader();
                if (dr.Read())
                {
                    contador = Convert.ToInt32(dr["IdRetiros"]);
                    contador += 1;
                }
                else
                    contador = 1;
                dr.Close();

                //Se obtiene el ultimo idCorte de agregado.
                consulta = new SqlCommand("SELECT TOP 1 IdCorte,TotalCorte FROM TblCorte ORDER BY IdCorte DESC", cn);
                dr = consulta.ExecuteReader();
                if (dr.Read())
                {
                    ContadorCorte = Convert.ToInt32(dr["IdCorte"]);
                    TotalCorte = Convert.ToDouble(dr["TotalCorte"]);
                }
                dr.Close();

                //Se Obtiene la fecha y la hora actual y la convertimos en el formato de (aaaa/mm/dd)
                DateTime fecha = DateTime.Now;
                string Fecha = Convert.ToString(fecha.Date.ToShortDateString());

                //Realizamos la inserción de los Retiros.
                cmd = new SqlCommand("INSERT INTO TblRetiros (IdRetiros,IdUsuario,IdCorte,ReferenciaRet,ConceptoRet,MontoRet,Fecharet) Values (" + contador + "," + idusuario + "," + ContadorCorte + "," + Referencia + ",'" + concepto + "'," + Monto + ",'" + Fecha + "')", cn);
                cmd.ExecuteNonQuery();

                //Obtendremos el total de los Retiros agregados por el usuario tomando como parametros el Id del usuario y la Fecha en la que se agrego el retiro.
                Retiros = new SqlCommand("SELECT MontoRet FROM TblRetiros WHERE FechaRet='" + Fecha + "' AND IdUsuario=" + idusuario + "", cn);
                dr = Retiros.ExecuteReader();
                while (dr.Read())
                {
                    TRetiros += Convert.ToDouble(dr["MontoRet"]);
                }
                dr.Close();

                TotalCorte += Monto;

                //Se Actualiza el corte añadiendo solamente el total de los retiros agregados 
                Cortes = new SqlCommand("UPDATE TblCorte SET TotalRetiros=" + TRetiros + ", TotalCorte=" + TotalCorte + " WHERE IdCorte=" + ContadorCorte + "", cn);
                Cortes.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("No se conectó" + e.ToString());
            }
            return TRetiros;
        }

        //DIFERENCIA

        public double Diferencia(double importe, int IdUsuario)
        {
            double diferencia = 0;
            try
            {
                double TotalCorte = 0;
                int IdCorte = 0;

                //Se Obtiene la fecha y la hora actual y la convertimos en el formato de (aaaa/mm/dd)
                DateTime fecha = DateTime.Now;
                string Fecha = Convert.ToString(fecha.Date.ToShortDateString());


                consulta = new SqlCommand("SELECT IdCorte, TotalCorte FROM TblCorte WHERE IdUsuario=" + IdUsuario + " AND Fecha='" + Fecha + "'", cn);
                dr = consulta.ExecuteReader();

                if (dr.Read())
                {
                    IdCorte = Convert.ToInt32(dr["IdCorte"]);
                    TotalCorte = Convert.ToDouble(dr["TotalCorte"]);
                }
                dr.Close();

                diferencia = TotalCorte - importe;

                Cortes = new SqlCommand("UPDATE TblCorte SET CorteEntregado=" + importe + ", Diferencia=" + Math.Round(diferencia, 2) + " WHERE IdCorte=" + IdCorte + "", cn);
                Cortes.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("No se conectó: " + e.ToString());
            }
            return diferencia;
        }


        //Cargar DataGridView Facturas
        public void CargarFacturas(DataGridView dgw, int IdUsuario)
        {
            try
            {
                //Se Obtiene la fecha y la hora actual y la convertimos en el formato de (aaaa/mm/dd)
                DateTime fecha = DateTime.Now;
                string Fecha = Convert.ToString(fecha.Date.ToShortDateString());

                adap = new SqlDataAdapter("SELECT ReferenciaFac As Referencia, ConceptoFac As Concepto, MontoFac As Morto FROM TblFacturas WHERE IdUsuario=" + IdUsuario + " AND FechaFac='" + Fecha + "' ORDER BY ReferenciaFac ASC", cn);
                dt = new DataTable();
                adap.Fill(dt);
                dgw.DataSource = dt;
            }
            catch (Exception e)
            {
                MessageBox.Show("No se han llenado las Facturas y Notas" + e.ToString());
            }
        }



        //Cargar DataGridView Aplicaciones
        public void CargarAplicaciones(DataGridView dgw, int IdUsuario)
        {
            try
            {
                //Se Obtiene la fecha y la hora actual y la convertimos en el formato de (aaaa/mm/dd)
                DateTime fecha = DateTime.Now;
                string Fecha = Convert.ToString(fecha.Date.ToShortDateString());

                adap = new SqlDataAdapter("SELECT ConceptoApl AS Concepto, MontoApl  AS Monto FROM TblAplicaciones WHERE IdUsuario=" + IdUsuario + " AND FechaApl='" + Fecha + "'", cn);
                dt = new DataTable();
                adap.Fill(dt);
                dgw.DataSource = dt;
            }
            catch (Exception e)
            {
                MessageBox.Show("No se han llenado las Aplicaciones" + e.ToString());
            }
        }

        //Cargar DataGridView Retiros
        public void CargarRetiros(DataGridView dgw, int IdUsuario)
        {
            try
            {
                //Se Obtiene la fecha y la hora actual y la convertimos en el formato de (aaaa/mm/dd)
                DateTime fecha = DateTime.Now;
                string Fecha = Convert.ToString(fecha.Date.ToShortDateString());

                adap = new SqlDataAdapter("SELECT ReferenciaRet AS Referencia, ConceptoRet As Concepto, MontoRet As Monto FROM TblRetiros WHERE IdUsuario=" + IdUsuario + " AND FechaRet='" + Fecha + "' ORDER BY ReferenciaRet asc", cn);
                dt = new DataTable();
                adap.Fill(dt);
                dgw.DataSource = dt;
            }
            catch (Exception e)
            {
                MessageBox.Show("No se han llenando los Retiros" + e.ToString());
            }
        }

        //Cargar DataGridView Vales
        public void CargarVales(DataGridView dgw, int IdUsuario)
        {
            try
            {
                //Se Obtiene la fecha y la hora actual y la convertimos en el formato de (aaaa/mm/dd)
                DateTime fecha = DateTime.Now;
                string Fecha = Convert.ToString(fecha.Date.ToShortDateString());

                adap = new SqlDataAdapter("SELECT ConceptoVal As Consepto, MontoVal As Monto FROM TblVales WHERE IdUsuario=" + IdUsuario + " AND FechaVal='" + Fecha + "'", cn);
                dt = new DataTable();
                adap.Fill(dt);
                dgw.DataSource = dt;
            }
            catch (Exception e)
            {
                MessageBox.Show("No se han llenado los Vales" + e.ToString());
            }
        }


        //LLENAR LOS LABEL

        public void LlenarLabel(int IdUsuario, Label lbTotalConteo, Label lbTotalFacturas, Label lbTotalRetiros, Label lbTotalVales, Label lbTotalAplicaciones, Label lbTotalEntregado, Label lbTotalImporte, Label lbDiferencia, Label lbIdCorte)
        {
            try
            {
                //Se Obtiene la fecha y la hora actual y la convertimos en el formato de (aaaa/mm/dd)
                DateTime fecha = DateTime.Now;
                string Fecha = Convert.ToString(fecha.Date.ToShortDateString());

                consulta = new SqlCommand("SELECT IdCorte, TotalFacturas, TotalVales,TotalAplicaciones, TotalConteo,TotalRetiros, CorteEntregado,TotalCorte,Diferencia FROM TblCorte WHERE IdUsuario=" + IdUsuario + " AND Fecha='" + Fecha + "'", cn);
                dr = consulta.ExecuteReader();

                if (dr.Read())
                {
                    lbIdCorte.Text = dr["IdCorte"].ToString();
                    lbTotalConteo.Text = "$" + dr["TotalConteo"].ToString();
                    lbTotalFacturas.Text = "$" + dr["TotalFacturas"].ToString();
                    lbTotalRetiros.Text = "$" + dr["TotalRetiros"].ToString();
                    lbTotalVales.Text = "$" + dr["TotalVales"].ToString();
                    lbTotalAplicaciones.Text = "$" + dr["TotalAplicaciones"].ToString();
                    lbTotalEntregado.Text = "$" + dr["TotalCorte"].ToString();
                    lbTotalImporte.Text = "$" + dr["CorteEntregado"].ToString();
                    lbDiferencia.Text = "$" + dr["Diferencia"].ToString();
                }
                dr.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("imposible llenar los campos" + e.ToString());
            }
        }
        //Agregar Usuarios
        public string AgregarUsuario(string Nombre, string Usuario, string pass, string puesto)
        {
            string salida = "Usuario " + Usuario + " agregado correctamente";
            try
            {
                int contador = 0;

                consulta = new SqlCommand("SELECT TOP 1 IdUsuario FROM TblUsuarios ORDER BY IdUsuario DESC", cn);
                dr = consulta.ExecuteReader();
                if (dr.Read())
                {
                    contador = Convert.ToInt32(dr["IdUsuario"]);
                    contador += 1;
                }
                else
                    contador = 1;
                dr.Close();

                cmd = new SqlCommand("INSERT INTO TblUsuarios (IdUsuario,Nombre,Usuario,Pass,Puesto) Values(" + contador + ",'" + Nombre + "','" + Usuario + "','" + pass + "','" + puesto + "')", cn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                salida = "Usuario no agregado" + e.ToString();
            }

            return salida;
        }
        //Llena el combobox con los nombres de los usuarios 
        public void llenarCombo(ComboBox cmbUsuarios)
        {
            try
            {
                cmbUsuarios.Items.Clear();
                consulta = new SqlCommand("SELECT Nombre FROM TblUsuarios ORDER BY IdUsuario", cn);
                dr = consulta.ExecuteReader();

                while (dr.Read())
                {
                    cmbUsuarios.Items.Add(dr["Nombre"].ToString());
                }
                dr.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("No se pudo llenar el Combo" + e.ToString());
            }

        }

        public void BuscarCorte(int idCorte, int IdUsuario, String Fecha, Label lbTotalConteo, Label lbTotalFacturas, Label lbTotalRetiros, Label lbTotalVales, Label lbTotalAplicaciones, Label lbTotalEntregado, Label lbTotalImporte, Label lbDiferencia,Label lbIdCorte)
        {

            try
            {
                if (idCorte == 0 && IdUsuario>-1)
                {
                    consulta = new SqlCommand("SELECT IdCorte,TotalFacturas, TotalVales,TotalAplicaciones, TotalConteo,TotalRetiros, CorteEntregado,TotalCorte,Diferencia FROM TblCorte WHERE IdUsuario=" + IdUsuario + " AND Fecha='" + Fecha + "'", cn);
                    dr = consulta.ExecuteReader();

                    if (dr.Read())
                    {
                        lbIdCorte.Text = dr["IdCorte"].ToString();
                        lbTotalConteo.Text = "$" + dr["TotalConteo"].ToString();
                        lbTotalFacturas.Text = "$" + dr["TotalFacturas"].ToString();
                        lbTotalRetiros.Text = "$" + dr["TotalRetiros"].ToString();
                        lbTotalVales.Text = "$" + dr["TotalVales"].ToString();
                        lbTotalAplicaciones.Text = "$" + dr["TotalAplicaciones"].ToString();
                        lbTotalEntregado.Text = "$" + dr["TotalCorte"].ToString();
                        lbTotalImporte.Text = "$" + dr["CorteEntregado"].ToString();
                        lbDiferencia.Text = "$" + dr["Diferencia"].ToString();
                    }
                    dr.Close();
                }
                else 
                {
                    consulta = new SqlCommand("SELECT IdCorte,TotalFacturas, TotalVales,TotalAplicaciones, TotalConteo,TotalRetiros, CorteEntregado,TotalCorte,Diferencia FROM TblCorte WHERE IdCorte="+idCorte+"", cn);
                    dr = consulta.ExecuteReader();

                    if (dr.Read())
                    {
                        lbIdCorte.Text = dr["IdCorte"].ToString();
                        lbTotalConteo.Text = "$" + dr["TotalConteo"].ToString();
                        lbTotalFacturas.Text = "$" + dr["TotalFacturas"].ToString();
                        lbTotalRetiros.Text = "$" + dr["TotalRetiros"].ToString();
                        lbTotalVales.Text = "$" + dr["TotalVales"].ToString();
                        lbTotalAplicaciones.Text = "$" + dr["TotalAplicaciones"].ToString();
                        lbTotalEntregado.Text = "$" + dr["TotalCorte"].ToString();
                        lbTotalImporte.Text = "$" + dr["CorteEntregado"].ToString();
                        lbDiferencia.Text = "$" + dr["Diferencia"].ToString();
                    }
                    dr.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Imposible llenar los campos:" + e.ToString());
            }
        }

        //Cargar DataGridView Facturas
        public void BCargarFacturas(DataGridView dgw, int IdUsuario, string Fecha,int IdCorte)
        {
            try
            {
                if (IdCorte == 0 && IdUsuario>-1)
                {
                    adap = new SqlDataAdapter("SELECT ReferenciaFac As Referencia, ConceptoFac As Concepto, MontoFac As Monto FROM TblFacturas WHERE IdUsuario=" + IdUsuario + " AND FechaFac='" + Fecha + "' ORDER BY ReferenciaFac asc", cn);
                    dt = new DataTable();
                    adap.Fill(dt);
                    dgw.DataSource = dt;
                }
                else 
                {
                    adap = new SqlDataAdapter("SELECT ReferenciaFac As Referencia, ConceptoFac As Concepto, MontoFac As Monto FROM TblFacturas WHERE IdCorte="+IdCorte+" ORDER BY ReferenciaFac asc", cn);
                    dt = new DataTable();
                    adap.Fill(dt);
                    dgw.DataSource = dt;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("No se han llenado las Facturas y Notas: " + e.ToString());
            }
        }



        //Cargar DataGridView Aplicaciones
        public void BCargarAplicaciones(DataGridView dgw, int IdUsuario, string Fecha,int IdCorte)
        {
            try
            {
                if (IdCorte != 0 && IdUsuario>-1)
                {
                    adap = new SqlDataAdapter("SELECT ConceptoApl AS Concepto, MontoApl  AS Monto FROM TblAplicaciones WHERE IdUsuario=" + IdUsuario + " AND FechaApl='" + Fecha + "'", cn);
                    dt = new DataTable();
                    adap.Fill(dt);
                    dgw.DataSource = dt;
                }
                else
                {
                    adap = new SqlDataAdapter("SELECT ConceptoApl AS Concepto, MontoApl  AS Monto FROM TblAplicaciones WHERE IdCorte="+IdCorte+"", cn);
                    dt = new DataTable();
                    adap.Fill(dt);
                    dgw.DataSource = dt;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("No se han llenado las Aplicaciones: " + e.ToString());
            }
        }

        //Cargar DataGridView Retiros
        public void BCargarRetiros(DataGridView dgw, int IdUsuario, string Fecha,int IdCorte)
        {
            try
            {
                if (IdCorte == 0 && IdUsuario>-1)
                {
                    adap = new SqlDataAdapter("SELECT ReferenciaRet AS Referencia, ConceptoRet As Concepto, MontoRet As Monto FROM TblRetiros WHERE IdUsuario=" + IdUsuario + " AND FechaRet='" + Fecha + "' ORDER BY ReferenciaRet asc", cn);
                    dt = new DataTable();
                    adap.Fill(dt);
                    dgw.DataSource = dt;
                }
                else
                {
                    adap = new SqlDataAdapter("SELECT ReferenciaRet AS Referencia, ConceptoRet As Concepto, MontoRet As Monto FROM TblRetiros WHERE IdCorte="+IdCorte+" ORDER BY ReferenciaRet asc", cn);
                    dt = new DataTable();
                    adap.Fill(dt);
                    dgw.DataSource = dt; 
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("No se han llenado los Retiros:" + e.ToString());
            }
        }

        //Cargar DataGridView Vales
        public void BCargarVales(DataGridView dgw, int IdUsuario, string Fecha,int IdCorte)
        {
            try
            {
                if (IdCorte == 0 && IdUsuario>-1)
                {
                    adap = new SqlDataAdapter("SELECT ConceptoVal As Consepto, MontoVal As Monto FROM TblVales WHERE IdUsuario=" + IdUsuario + " AND FechaVal='" + Fecha + "'", cn);
                    dt = new DataTable();
                    adap.Fill(dt);
                    dgw.DataSource = dt;
                }
                else
                {
                    adap = new SqlDataAdapter("SELECT ConceptoVal As Consepto, MontoVal As Monto FROM TblVales WHERE IdCorte="+IdCorte+"", cn);
                    dt = new DataTable();
                    adap.Fill(dt);
                    dgw.DataSource = dt;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("No se han llenado los Vales: " + e.ToString());
            }
        }

        //Login

        public int Login(string Usuario, string Password)
        {
            int IdUsuario = 0;
            try
            {
                consulta = new SqlCommand("SELECT IdUsuario FROM TblUsuarios WHERE Usuario='" + Usuario + "' AND Pass='" + Password + "'", cn);
                dr = consulta.ExecuteReader();

                if (dr.Read())
                {
                    IdUsuario = Convert.ToInt32(dr["IdUsuario"]);
                    // MessageBox.Show("El Puesto de usuario es: " + puesto);
                }
                else
                {
                    MessageBox.Show("El Usuario o la Contraseña no son correctas");
                    IdUsuario = -1;
                }

                dr.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("El usuario no existe: " + e.ToString());
            }
            return IdUsuario;
        }

        //Obtiene el puesto del usuario
        public string Puesto(string Usuario, string Password)
        {
            string puesto = null;
            try
            {
                consulta = new SqlCommand("SELECT Puesto FROM TblUsuarios WHERE Usuario='" + Usuario + "' AND Pass='" + Password + "'", cn);
                dr = consulta.ExecuteReader();
                if (dr.Read())
                {
                    puesto = dr["Puesto"].ToString();
                }
                else
                {
                    puesto = null;
                }
                dr.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("No se conecto: " + e.ToString());
            }
            return puesto;
        }

        //Obtiene el Id del corte 
        public int Corte(int IdUsuario, string Feha)
        {
            int IdCorte = 0;
            try
            {
                consulta = new SqlCommand("SELECT IdCorte FROM TblCorte Where IdUsuario=" + IdUsuario + " AND Fecha='" + Feha + "'", cn);
                dr = consulta.ExecuteReader();
                if (dr.Read())
                {
                    IdCorte = Convert.ToInt32(dr["IdCorte"]);
                }
                dr.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("No se conectó: " + e.ToString());
            }
            return IdCorte;
        }

        //ELIMINAR FACTURAS
        public string EliminarFactura(int Ref, double monto, int idUsuario)
        {
            string salida = "Se ha eliminado la factura con la referencia " + Ref.ToString();
            int IdCorte = 0;
            double TotalCorte = 0;
            double TotalFac = 0;

            try
            {
                DelFac = new SqlCommand("DELETE FROM TblFacturas WHERE ReferenciaFac=" + Ref + "", cn);
                DelFac.ExecuteNonQuery();

                //Tomamos la Fecha y hora actual y la convertimos en formato corte de (aaaa/mm/dd)
                DateTime fecha = DateTime.Now;
                string Fecha = Convert.ToString(fecha.Date.ToShortDateString());

                consulta = new SqlCommand("SELECT IdCorte, TotalCorte, TotalFacturas FROM TblCorte WHERE IdUsuario=" + idUsuario + " AND Fecha='" + Fecha + "'", cn);
                dr = consulta.ExecuteReader();
                if (dr.Read())
                {
                    IdCorte = Convert.ToInt32(dr["IdCorte"]);
                    TotalCorte = Convert.ToDouble(dr["TotalCorte"]) - monto;
                    TotalFac = Convert.ToDouble(dr["TotalFacturas"]) - monto;
                }
                dr.Close();
                ActCorte = new SqlCommand("UPDATE TblCorte SET TotalFacturas=" + TotalFac + ",TotalCorte=" + TotalCorte + " WHERE IdCorte=" + IdCorte + "", cn);
                ActCorte.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                salida = "No conectó: " + e.ToString();
            }
            return salida;
        }

        //ELIMINAR VALES
        public string EliminarVales(double monto, int idUsuario)
        {
            string salida = "Se ha eliminado el Vale";
            int IdCorte = 0;
            double TotalCorte = 0;
            double TotalVal = 0;

            try
            {

                //Tomamos la Fecha y hora actual y la convertimos en formato corte de (aaaa/mm/dd)
                DateTime fecha = DateTime.Now;
                string Fecha = Convert.ToString(fecha.Date.ToShortDateString());

                DelVal = new SqlCommand("DELETE FROM TblVales WHERE MontoVal=" + monto + " AND IdUsuario=" + idUsuario + " AND FechaVal='" + Fecha + "'", cn);
                DelVal.ExecuteNonQuery();

                consulta = new SqlCommand("SELECT IdCorte, TotalCorte, TotalVales FROM TblCorte WHERE IdUsuario=" + idUsuario + " AND Fecha='" + Fecha + "'", cn);
                dr = consulta.ExecuteReader();
                if (dr.Read())
                {
                    IdCorte = Convert.ToInt32(dr["IdCorte"]);
                    TotalCorte = Convert.ToDouble(dr["TotalCorte"]) - monto;
                    TotalVal = Convert.ToDouble(dr["TotalVales"]) - monto;
                }
                dr.Close();
                ActCorte = new SqlCommand("UPDATE TblCorte SET TotalVales=" + TotalVal + ",TotalCorte=" + TotalCorte + " WHERE IdCorte=" + IdCorte + "", cn);
                ActCorte.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                salida = "No conectó: " + e.ToString();
            }
            return salida;
        }

        //ELIMINAR RETIROS
        public string EliminarRetiros(int Ref, double monto, int idUsuario)
        {
            string salida = "Se ha eliminado el factura con la referencia " + Ref.ToString();
            int IdCorte = 0;
            double TotalCorte = 0;
            double TotalFac = 0;

            try
            {
                DelRet = new SqlCommand("DELETE FROM TblRetiros WHERE ReferenciaRet=" + Ref + "", cn);
                DelRet.ExecuteNonQuery();

                //Tomamos la Fecha y hora actual y la convertimos en formato corte de (aaaa/mm/dd)
                DateTime fecha = DateTime.Now;
                string Fecha = Convert.ToString(fecha.Date.ToShortDateString());

                consulta = new SqlCommand("SELECT IdCorte, TotalCorte, TotalRetiros FROM TblCorte WHERE IdUsuario=" + idUsuario + " AND Fecha='" + Fecha + "'", cn);
                dr = consulta.ExecuteReader();
                if (dr.Read())
                {
                    IdCorte = Convert.ToInt32(dr["IdCorte"]);
                    TotalCorte = Convert.ToDouble(dr["TotalCorte"]) - monto;
                    TotalFac = Convert.ToDouble(dr["TotalRetiros"]) - monto;
                }
                dr.Close();
                ActCorte = new SqlCommand("UPDATE TblCorte SET TotalRetiros=" + TotalFac + ",TotalCorte=" + TotalCorte + " WHERE IdCorte=" + IdCorte + "", cn);
                ActCorte.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                salida = "No conectó: " + e.ToString();
            }
            return salida;
        }

        //Eliminar Aplicaciones
        public string EliminarAplicaciones(double monto, int idUsuario, string Fecha)
        {
            string salida = "Se ha eliminado el Vale";
            int IdCorte = 0;
            double TotalCorte = 0;
            double TotalApl = 0;
            double Diferencia = 0;
            double TotalEntregado = 0;

            try
            {

                DelApl = new SqlCommand("DELETE FROM TblAplicaciones WHERE MontoApl=" + monto + " AND IdUsuario=" + idUsuario + " AND FechaApl='" + Fecha + "'", cn);
                DelApl.ExecuteNonQuery();

                consulta = new SqlCommand("SELECT IdCorte, TotalCorte, TotalAplicaciones, CorteEntregado FROM TblCorte WHERE IdUsuario=" + idUsuario + " AND Fecha='" + Fecha + "'", cn);
                dr = consulta.ExecuteReader();
                if (dr.Read())
                {
                    IdCorte = Convert.ToInt32(dr["IdCorte"]);
                    TotalCorte = Convert.ToDouble(dr["TotalCorte"]) - monto;
                    TotalApl = Convert.ToDouble(dr["TotalAplicaciones"]) - monto;
                    TotalEntregado = Convert.ToDouble(dr["CorteEntregado"]);
                }
                dr.Close();

                Diferencia = TotalCorte - TotalEntregado;

                ActCorte = new SqlCommand("UPDATE TblCorte SET TotalAplicaciones=" + TotalApl + ",TotalCorte=" + TotalCorte + ", Diferencia=" + Math.Round(Diferencia, 2) + " WHERE IdCorte=" + IdCorte + "", cn);
                ActCorte.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                salida = "No conectó: " + e.ToString();
            }
            return salida;

        }

        internal int IdSucursal(int IdCorte)
        {
            throw new NotImplementedException();
        }
    }
}

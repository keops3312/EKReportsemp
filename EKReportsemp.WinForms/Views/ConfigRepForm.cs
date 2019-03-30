﻿

namespace EKReportsemp.WinForms.Views
{


    #region Libraries (Librerias)
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Globalization;
    using System.Windows.Forms;
    using DevComponents.DotNetBar;
    using EKReportsemp.WinForms.Classes; 
    #endregion

    public partial class ConfigRepForm : Office2007Form
    {


        #region Properties (Propiedades)
       
        private BuscarLocalidad buscarLocalidad;
        private DataTable resultMisCajas;
        private ResultadosOperacion resultadosOperacion;

        
        #endregion

        #region Attributes (Atributos)
        public DataTable empresas;
        private DataTable resultadoReporte;
        private DataTable cajasSeleccionadas;
        private DataTable resumen;


        private string tipoReporte;
        private string conn;
        private int unifica;
        private int porSemana;
        private decimal iva=0;
        private decimal parte=0;
        DateTime fechaInicio;
        DateTime fechaFinal;
       
        #endregion

        #region Constructors
        public ConfigRepForm(DataTable empresa)
        {
            buscarLocalidad = new BuscarLocalidad();
            resultadosOperacion = new ResultadosOperacion();        
            resultadoReporte = new DataTable();

         


            InitializeComponent();

            CheckForIllegalCrossThreadCalls = false;
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;

            labelX5.Visible = false;
            labelX6.Visible = false;
            labelX7.Visible = false;
            txtIva.Visible = false;
            txtPorcentaje.Visible = false;

            empresas = new DataTable();
            empresas.Clear();
            empresas = empresa;

            resultMisCajas = new DataTable();
            resultMisCajas.Columns.Add();
            resultMisCajas.Columns.Add();
            resultMisCajas.Columns.Add();
            resultMisCajas.Columns.Add();
            resultMisCajas.Columns.Add();


            cajasSeleccionadas = new DataTable();
            cajasSeleccionadas.Columns.Add("Caja");
            cajasSeleccionadas.Columns.Add("NumCaja");
            cajasSeleccionadas.Columns.Add("BD");
            cajasSeleccionadas.Columns.Add("Empresa");
            cajasSeleccionadas.Columns.Add("Localidad");

            resumen = new DataTable();
            resumen.Columns.Add();
            resumen.Columns.Add();
            resumen.Columns.Add();
            resumen.Columns.Add();
            resumen.Columns.Add();
            resumen.Columns.Add();
            resumen.Columns.Add();
            resumen.Columns.Add();
            resumen.Columns.Add();
            resumen.Columns.Add();
            resumen.Columns.Add();
            resumen.Columns.Add();
            resumen.Columns.Add();
            resumen.Columns.Add();
            resumen.Columns.Add();


        }
        #endregion

        #region Methods (Metodos)
        private void Cargar()
        {
            //Combo
            cmbTipo.Items.Add("Prestamos");
            cmbTipo.Items.Add("Remisiones");
            cmbTipo.Items.Add("Notas de Pago");

            cmbTipo.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbTipo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbTipo.DropDownStyle = ComboBoxStyle.DropDownList;
            //Buscamos las sucursales que pertenezan a las empresas seleccionadas

            localidadesGrid.RowHeadersVisible = false;
            localidadesGrid.DefaultCellStyle.WrapMode = DataGridViewTriState.True; //muestro todo el texto
            DataTable listaEmpresas = new DataTable();
            listaEmpresas = buscarLocalidad.BuscarSucursales(empresas);

            foreach (DataRow item in listaEmpresas.Rows)
            {
                localidadesGrid.Rows.Add(true, item[0].ToString(), item[1].ToString(), item[2].ToString());

            }

           
            //Ahora Cargo las cajas

            cajasGrid.RowHeadersVisible = false;
            cajasGrid.DefaultCellStyle.WrapMode = DataGridViewTriState.True; //muestro todo el texto
            foreach (DataRow item in listaEmpresas.Rows)
            {
                
                string x = item[1].ToString();
                string y = item[2].ToString();
                BuscarCajas(x,y);

            }

            cajasGrid.Rows.Clear();
            foreach (DataRow item in resultMisCajas.Rows)
            {
                cajasGrid.Rows.Add(true,item[0].ToString(), item[1].ToString(), item[2].ToString(), item[3].ToString(), item[4].ToString());
            }
           


            //rango de Fechas
            int ano = DateTime.Now.Year;
            dateTimeInput1.Value = Convert.ToDateTime("01-01-" + ano);
            dateTimeInput2.Value = DateTime.Now;



        }
        
        private DataTable BuscarCajas(string BaseDatos,string Empresa)//
        {
           

            conn = ConfigurationManager.ConnectionStrings["SEMP2013_CNX"].ConnectionString;
            SqlConnection cn = new SqlConnection(conn);

            SqlDataAdapter cmd = new SqlDataAdapter(" USE " + BaseDatos + " " +
                " SELECT caja, numcaja " +
                " from selcaja ", cn);
            DataTable resultado = new DataTable();
            resultado.Clear();
            cmd.Fill(resultado);
            cajasSeleccionadas.Clear();
            foreach (DataRow item in resultado.Rows)
            {

                SqlDataAdapter cmd1 = new SqlDataAdapter(" USE " + BaseDatos + " " +
              " SELECT [Nombre Sucursal] " +
              " from localidades where impresora='RAIZ'", cn);
                DataTable resultado1 = new DataTable();
                resultado1.Clear();
                cmd1.Fill(resultado1);
                

                resultMisCajas.Rows.Add(item[0].ToString(), item[1].ToString(), BaseDatos,Empresa,resultado1.Rows[0][0].ToString());
                cajasSeleccionadas.Rows.Add(item[0].ToString(), item[1].ToString(), BaseDatos,Empresa, resultado1.Rows[0][0].ToString());
            }


            return resultMisCajas;

        }

        private void CargarEliminarCajas()
        {
            //Eliminamos las cajas del grid
            try
            {
               
            if (cajasGrid.Rows.Count > 0)
                {

                    cajasGrid.Rows.Clear();
                    //for (int i = cajasGrid.Rows.Count - 1; i >= 0; i--)
                    //{
                    //    cajasGrid.Rows.RemoveAt(i);
                    //}


                }

            ////Volvemos a llenar a decision del usuario
            string a, b;
            cajasSeleccionadas.Clear();
            foreach (DataGridViewRow r in localidadesGrid.Rows)
            {
                Boolean sel = Convert.ToBoolean(r.Cells[0].Value);
               

                if (sel == true)
                {
                        a = r.Cells[2].Value.ToString();
                        //recorremos la tabla base
                        foreach (DataRow res in resultMisCajas.Rows)
                        {

                            b = res[2].ToString();

                            if (a.Trim() == b.Trim())
                            {
                                cajasGrid.Rows.Add(true, res[0].ToString(), res[1].ToString(), res[2].ToString(), res[3].ToString(), res[4].ToString());
                                cajasSeleccionadas.Rows.Add(res[0].ToString(), res[1].ToString(), res[2].ToString(), res[3].ToString(),res[4].ToString());
                            }

                            
                        }




                }
            }


            // dataGridViewX1.DataSource = cajasSeleccionadas;

            }
            catch (Exception)
            {


            }

        }

        private void valoresRemision()
        {
            if (cmbTipo.Text == "Remisiones")
            {
                labelX5.Visible = true;
                labelX6.Visible = true;
                labelX7.Visible = true;
                txtIva.Visible = true;
                txtPorcentaje.Visible = true;
            }
            else
            {
                labelX7.Visible = false;
                labelX5.Visible = false;
                labelX6.Visible = false;
                txtIva.Visible = false;
                txtPorcentaje.Visible = false;

            }
        }

        private void BeginReport()
        {


            if (tipoReporte == "Prestamos")
            {

                resumen.Clear();

                foreach (DataRow item in cajasSeleccionadas.Rows)
                {
                    string caja, database,emp, bd, localidad,cajaletra;

                    caja = item[0].ToString();//TLX11
                    cajaletra = item[1].ToString();//CIS-TLX1-1
                    database = item[2].ToString();
                    bd = item[2].ToString();//SEMP2013_TLXX
                    emp = item[3].ToString();//Monte Ros
                    localidad = item[4].ToString();//Tula monte ros
                 
                    
                   
                   



                    resultadoReporte.Clear();

                    resultadoReporte = resultadosOperacion.PrestamosXdiaResumen(1, 2018, database, 1,
                                        fechaInicio, fechaFinal, conn, unifica, porSemana, caja,emp,localidad,cajaletra);


                    foreach (DataRow r in resultadoReporte.Rows)
                    {
                        resumen.Rows.Add(r[0].ToString(),  r[1].ToString(), r[2].ToString()
                                        , r[3].ToString(), r[4].ToString(), r[5].ToString(), 
                                        r[6].ToString(), r[7].ToString(), r[8].ToString(), r[9].ToString());



                    }




                }



            }



            if (tipoReporte == "Notas de Pago")
            {

                resumen.Clear();

                foreach (DataRow item in cajasSeleccionadas.Rows)
                {

                    string caja, database, emp, bd, localidad, cajaletra;
                    caja = item[0].ToString();//TLX11
                    cajaletra = item[1].ToString();//CIS-TLX1-1
                    database = item[2].ToString();
                    bd = item[2].ToString();//SEMP2013_TLXX
                    emp = item[3].ToString();//Monte Ros
                    localidad = item[4].ToString();//Tula monte ros



                    resultadoReporte.Clear();
                    
                    resultadoReporte = resultadosOperacion.NotasDePagoXdiaResumen(1,2018,database,1,fechaInicio,fechaFinal,
                        conn,unifica,porSemana,caja,emp,localidad, cajaletra);


                    foreach (DataRow r in resultadoReporte.Rows)
                    {
                      

                        resumen.Rows.Add(r[0].ToString(), r[1].ToString(), r[2].ToString()
                                        , r[3].ToString(), r[4].ToString(), r[5].ToString(), r[6].ToString(), r[7].ToString()
                                        , r[8].ToString(), r[9].ToString(), r[10].ToString(), r[11].ToString());
                    }




                }



            }


            if (tipoReporte == "Remisiones")
            {

                resumen.Clear();

                foreach (DataRow item in cajasSeleccionadas.Rows)
                {

                    string caja, database, emp, bd, localidad, cajaletra;
                    caja = item[0].ToString();//TLX11
                    cajaletra = item[1].ToString();//CIS-TLX1-1
                    database = item[2].ToString();
                    bd = item[2].ToString();//SEMP2013_TLXX
                    emp = item[3].ToString();//Monte Ros
                    localidad = item[4].ToString();//Tula monte ros



                    resultadoReporte.Clear();

                  
                    resultadoReporte.Clear();

                    resultadoReporte = resultadosOperacion.RemisionesXdiaResumen(1, 2018, database, 1,
                                        fechaInicio, fechaFinal, conn, unifica, porSemana, caja, iva, parte, emp, localidad, cajaletra);


                    foreach (DataRow r in resultadoReporte.Rows)
                    {
                       

                       

                        resumen.Rows.Add(r[0].ToString(),
                            r[1].ToString(),
                            r[2].ToString(),
                            r[3].ToString(),
                            r[4].ToString(),
                            r[5].ToString(),
                            r[6].ToString(),
                            r[7].ToString(),
                            r[8].ToString(),
                            decimal.Parse(r[9].ToString()).ToString("N2"),
                            decimal.Parse(r[10].ToString()).ToString("N2"),
                            decimal.Parse(r[11].ToString()).ToString("N2"),
                            decimal.Parse(r[12].ToString()).ToString("N2"),
                            decimal.Parse(r[13].ToString()).ToString("N2"),
                            decimal.Parse(r[14].ToString()).ToString("N2")
                            );
                    }




                }



            }







        }

        #endregion

        #region Events (Eventos)
        private void ConfigRepForm_Load(object sender, EventArgs e)
        {
            Cargar();
        }

        private void checkLocalidades_CheckedChanged(object sender, EventArgs e)
        {

            if (checkLocalidades.Checked == true)
            {
                foreach (DataGridViewRow item in localidadesGrid.Rows)
                {
                    item.Cells["Seleccionar"].Value = true;
                }

            }
            else
            {

                foreach (DataGridViewRow item in localidadesGrid.Rows)
                {
                    item.Cells["Seleccionar"].Value = false;
                }

                

            }

            CargarEliminarCajas();
        }

        private void checkCajas_CheckedChanged(object sender, EventArgs e)
        {
            if (checkCajas.Checked == true)
            {
                foreach (DataGridViewRow item in cajasGrid.Rows)
                {
                    item.Cells["Seleccionar2"].Value = true;
                }
            }
            else
            {

                foreach (DataGridViewRow item in cajasGrid.Rows)
                {
                    item.Cells["Seleccionar2"].Value = false;
                }

            }
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            CargarEliminarCajas();
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(cmbTipo.Text))
            {
                MessageBoxEx.EnableGlass = false;
                MessageBoxEx.Show("Seleccione tipo de reporte por favor",
                    "EKReport SEMP",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                cmbTipo.Focus();
                return;


            }
            //cuantas cajas hemos seleecionado
            int y = 0;
            foreach (DataGridViewRow item in cajasGrid.Rows)
            {
                Boolean z = Convert.ToBoolean(item.Cells[0].Value);
                if (z == true)
                {
                    y += 1;
                }

            }
            if (y == 0)
            {
                MessageBoxEx.EnableGlass = false;
                MessageBoxEx.Show("Seleccione por lo menos una caja por favor",
                    "EKReport SEMP",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                cajasGrid.Focus();
                return;


            }

            //Comprobar si escogieron un rango de fechas coherente

            if (dateTimeInput1.Value > dateTimeInput2.Value)
            {
                MessageBoxEx.EnableGlass = false;
                MessageBoxEx.Show("El rango de fechas no es coherente verifique por favor",
                    "EKReport SEMP",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                dateTimeInput1.Focus();
                return;
            }


            //Reviso si es Remisiones y veo si hay valores validos
            if (cmbTipo.Text == "Remisiones")
            {
                if (String.IsNullOrEmpty(txtIva.Text) || String.IsNullOrEmpty(txtPorcentaje.Text))
                {
                    MessageBoxEx.EnableGlass = false;
                    MessageBoxEx.Show("Ingrese valor de Iva y Porcentaje de Remision por favor",
                        "EKReport SEMP",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    txtIva.Focus();
                    return;


                }

                iva = decimal.Parse(txtIva.Text);
                parte = decimal.Parse(txtPorcentaje.Text);

                if (iva >= 1)
                {
                    MessageBoxEx.EnableGlass = false;
                    MessageBoxEx.Show("El valor del Iva debe ser menor a 1 \n " +
                        "(Por Ejemplo.25,.16,.15) Corrija por favor",
                        "EKReport SEMP",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    txtIva.Focus();
                    return;


                }

                if (parte > 100)
                {
                    MessageBoxEx.EnableGlass = false;
                    MessageBoxEx.Show("El valor del % de la venta debe ser menor o igual a 100 \n " +
                        " (Por Ejemplo: 1.0 , 2.5 , 10,20) Corrija por favor",
                        "EKReport SEMP",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    txtPorcentaje.Focus();
                    return;


                }


            }




            tipoReporte = cmbTipo.Text;

            unifica = (checkUnificar.Checked == true) ? 1 : 2;
            porSemana = (checkRangosSemana.Checked == true) ? 1 : 2;


            fechaInicio = dateTimeInput1.Value;
            fechaFinal = dateTimeInput2.Value;

            circularProgress1.IsRunning = true;
            circularProgress1.Visible = true;
            btnReporte.Enabled = false;
            //RECORREMOS LA CAJAS Y VEMOS CUALES ESTAN SELECCIONADAS
            string seleccionadaBD = "";
            cajasSeleccionadas.Clear();
            foreach (DataGridViewRow r in cajasGrid.Rows)
            {
                Boolean sel = Convert.ToBoolean(r.Cells[0].Value);
                string bd = r.Cells[3].Value.ToString();

                if (sel == true)
                {



                    if (unifica == 1)
                    {

                        if (seleccionadaBD != bd)
                        {
                            cajasSeleccionadas.Rows.Add(r.Cells[1].Value.ToString(), r.Cells[2].Value.ToString(),
                                r.Cells[3].Value.ToString(), r.Cells[4].Value.ToString(), r.Cells[5].Value.ToString());
                        }

                        seleccionadaBD = bd;



                    }
                    else
                    {
                        cajasSeleccionadas.Rows.Add(r.Cells[1].Value.ToString(), r.Cells[2].Value.ToString(),
                            r.Cells[3].Value.ToString(), r.Cells[4].Value.ToString(), r.Cells[5].Value.ToString());
                    }




                }
            }


            backgroundWorker1.RunWorkerAsync();


        }
        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            valoresRemision();
        }

        private void txtIva_KeyPress(object sender, KeyPressEventArgs e)
        {
            CultureInfo cc = System.Threading.Thread.CurrentThread.CurrentCulture;

            if (char.IsNumber(e.KeyChar) ||
                      e.KeyChar.ToString() == cc.NumberFormat.NumberDecimalSeparator)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void txtPorcentaje_KeyPress(object sender, KeyPressEventArgs e)
        {
            CultureInfo cc = System.Threading.Thread.CurrentThread.CurrentCulture;

            if (char.IsNumber(e.KeyChar) ||
                      e.KeyChar.ToString() == cc.NumberFormat.NumberDecimalSeparator)
                e.Handled = false;
            else
                e.Handled = true;
        }

        #endregion

        #region BackGroundWorkers (Hilos)
        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            BeginReport();
        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            circularProgress1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {

            circularProgress1.IsRunning = false;
            circularProgress1.Visible = false;
            btnReporte.Enabled = true;

            dataGridViewX1.DataSource = resumen;


            MessageBoxEx.EnableGlass = false;
            MessageBoxEx.Show("Ejercicio Realizado", "EK Report SEMP", MessageBoxButtons.OK, MessageBoxIcon.Information);


            diseñoDeResultado(resumen);



        }

        private void diseñoDeResultado(DataTable resumen)
        {
           
            dataGridViewX1.DataSource = resultadosOperacion.diseñoDeResultado(resumen, fechaInicio, fechaFinal);

        }
        #endregion




    }
}



namespace EKReportsemp.WinForms.Views
{


    #region Libraries (Librerias)
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
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

            empresas = new DataTable();
            empresas.Clear();
            empresas = empresa;

            resultMisCajas = new DataTable();
            resultMisCajas.Columns.Add();
            resultMisCajas.Columns.Add();
            resultMisCajas.Columns.Add();

            cajasSeleccionadas = new DataTable();
            cajasSeleccionadas.Columns.Add("Caja");
            cajasSeleccionadas.Columns.Add("NumCaja");
            cajasSeleccionadas.Columns.Add("BD");

            resumen = new DataTable();
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
                localidadesGrid.Rows.Add(true, item[0].ToString(), item[1].ToString());

            }

           
            //Ahora Cargo las cajas

            cajasGrid.RowHeadersVisible = false;
            cajasGrid.DefaultCellStyle.WrapMode = DataGridViewTriState.True; //muestro todo el texto
            foreach (DataRow item in listaEmpresas.Rows)
            {
                
                string x = item[1].ToString();

                BuscarCajas(x);

            }
            cajasGrid.Rows.Clear();
            foreach (DataRow item in resultMisCajas.Rows)
            {
                cajasGrid.Rows.Add(true,item[0].ToString(), item[1].ToString(), item[2].ToString());
            }
           


            //rango de Fechas
            int ano = DateTime.Now.Year;
            dateTimeInput1.Value = Convert.ToDateTime("01-01-" + ano);
            dateTimeInput2.Value = DateTime.Now;



        }


        public DataTable BuscarCajas(string BaseDatos)//
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
                resultMisCajas.Rows.Add(item[0].ToString(), item[1].ToString(), BaseDatos);
                cajasSeleccionadas.Rows.Add(item[0].ToString(), item[1].ToString(), BaseDatos);
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
                                cajasGrid.Rows.Add(true, res[0].ToString(), res[1].ToString(), res[2].ToString());
                                cajasSeleccionadas.Rows.Add(res[0].ToString(), res[1].ToString(), res[2].ToString());
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

        #endregion

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
            foreach  (DataGridViewRow item in cajasGrid.Rows)
            {
                Boolean z = Convert.ToBoolean(item.Cells[0].Value);
                if (z == true)
                {
                    y += 1;
                }

            }
            if (y==0)
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

            if(dateTimeInput1.Value > dateTimeInput2.Value)
            {
                MessageBoxEx.EnableGlass = false;
                MessageBoxEx.Show("El rango de fechas no es coherente verifique por favor",
                    "EKReport SEMP",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                dateTimeInput1.Focus();
                return;
            }



            tipoReporte = cmbTipo.Text;

            unifica = (checkUnificar.Checked == true)? 1 : 2;
            porSemana= (checkRangosSemana.Checked == true) ? 1 : 2;


            fechaInicio = dateTimeInput1.Value;
            fechaFinal = dateTimeInput2.Value;

            circularProgress1.IsRunning = true;
            circularProgress1.Visible = true;
            btnReporte.Enabled = false;
            //RECORREMOS LA CAJAS Y VEMOS CUALES ESTAN SELECCIONADAS
            string seleccionadaBD="";
            cajasSeleccionadas.Clear();
            foreach (DataGridViewRow r in cajasGrid.Rows)
            {
                Boolean sel = Convert.ToBoolean(r.Cells[0].Value);
                string bd = r.Cells[3].Value.ToString();

                if (sel == true)
                {


                  
                        if (unifica == 1)
                        {

                            if(seleccionadaBD != bd)
                            {
                                 cajasSeleccionadas.Rows.Add(r.Cells[1].Value.ToString(), r.Cells[2].Value.ToString(), r.Cells[3].Value.ToString());
                            }

                            seleccionadaBD = bd;
                             

                              
                        }
                        else
                        {
                            cajasSeleccionadas.Rows.Add(r.Cells[1].Value.ToString(), r.Cells[2].Value.ToString(), r.Cells[3].Value.ToString());
                        }

                  
   

                }
            }

         
            backgroundWorker1.RunWorkerAsync();


        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            BeginReport();
        }

        private void BeginReport()
        {
          

            if (tipoReporte == "Prestamos")
            {

                resumen.Clear();

                foreach (DataRow item in cajasSeleccionadas.Rows)
                {
                    string caja, database;
                   
                   
                    caja = item[1].ToString();
                    database = item[2].ToString();

                  

                    resultadoReporte.Clear();
                   
                    resultadoReporte = resultadosOperacion.PrestamosXdiaResumen(1, 2018, database, 1,
                                        fechaInicio, fechaFinal, conn, unifica, porSemana, caja);

                  
                        foreach (DataRow r in resultadoReporte.Rows)
                        {
                            resumen.Rows.Add(r[0].ToString(), r[1].ToString(), r[2].ToString()
                                            , r[3].ToString(), r[4].ToString(), r[5].ToString(), r[6].ToString(), r[7].ToString());
                        }
                    
                   


                }



            }



            if (tipoReporte == "Notas de Pago")
            {

                resumen.Clear();

                foreach (DataRow item in cajasSeleccionadas.Rows)
                {
                    string caja, database;


                    caja = item[1].ToString();
                    database = item[2].ToString();



                    resultadoReporte.Clear();

                    resultadoReporte = resultadosOperacion.NotasDePagoXdiaResumen(1, 2018, database, 1,
                                        fechaInicio, fechaFinal, conn, unifica, porSemana, caja);


                    foreach (DataRow r in resultadoReporte.Rows)
                    {

                        resumen.Rows.Add(r[3].ToString(), r[4].ToString(), r[5].ToString()
                                        , r[7].ToString(), r[8].ToString(), r[9].ToString(), r[11].ToString(), r[12].ToString());
                    }




                }



            }






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


            MessageBoxEx.EnableGlass = false;
            MessageBoxEx.Show("Ejercicio Realizado","EK Report SEMP", MessageBoxButtons.OK, MessageBoxIcon.Information);

            dataGridViewX1.DataSource = resumen;
           

          

        }
    }
}

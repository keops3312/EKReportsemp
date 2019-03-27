

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
        private LocationConexion locationConexion;
        private BuscarLocalidad buscarLocalidad;

        #endregion

        #region Attributes (Atributos)
        public DataTable empresas;

        #endregion

        #region Constructors
        public ConfigRepForm(DataTable empresa)
        {
            buscarLocalidad = new BuscarLocalidad();
            InitializeComponent();
            empresas = new DataTable();
            empresas = empresa;
        }
        #endregion


        #region Methods (Metodos)
        private void Cargar()
        {
            //Combo
            cmbTipo.Items.Add("Prestamos");
            cmbTipo.Items.Add("Remisiones");
            cmbTipo.Items.Add("Notas de Pago");
            //Buscamos las sucursales que pertenezan a las empresas seleccionadas
            
            //DataTable result = new DataTable();
            //result = buscarLocalidad.BuscarSucursales(empresas);
            localidadesGrid.RowHeadersVisible = false;
            localidadesGrid.DefaultCellStyle.WrapMode = DataGridViewTriState.True; //muestro todo el texto
            
            foreach (DataRow item in buscarLocalidad.BuscarSucursales(empresas).Rows)
            {
                localidadesGrid.Rows.Add(true, item[0].ToString(), item[1].ToString());

            }


            //Ahora Cargo las cajas

            cajasGrid.RowHeadersVisible = false;
            cajasGrid.DefaultCellStyle.WrapMode = DataGridViewTriState.True; //muestro todo el texto
            foreach (DataRow item in buscarLocalidad.BuscarSucursales(empresas).Rows)
            {
                DataTable cajas = new DataTable();

                cajas = BuscarCajas(item[1].ToString());


                foreach (DataRow itemC in cajas.Rows)
                {
                    cajasGrid.Rows.Add(true,itemC[0].ToString(), itemC[1].ToString());
                }

            }

        }


        public DataTable BuscarCajas(string BaseDatos)
        {
            DataTable result = new DataTable();
            result.Columns.Add();
            result.Columns.Add();

            string conn = ConfigurationManager.ConnectionStrings["SEMP2013_CNX"].ConnectionString;
            SqlConnection cn = new SqlConnection(conn);

            SqlDataAdapter cmd = new SqlDataAdapter(" USE " + BaseDatos + " " +
                " SELECT caja, numcaja " +
                " from selcaja ", cn);
            DataTable resultado = new DataTable();
            cmd.Fill(resultado);

            foreach (DataRow item in result.Rows)
            {
                result.Rows.Add(item[0].ToString(), item[1].ToString());
            }


            return result;

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
        #endregion
    }
}

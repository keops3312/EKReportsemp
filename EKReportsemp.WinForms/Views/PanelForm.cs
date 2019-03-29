

namespace EKReportsemp.WinForms.Views
{


    #region Libraries (Librerias)
    using System;
    using System.Data;
    using System.Windows.Forms;
    using DevComponents.DotNetBar;
    using EKReportsemp.WinForms.Classes; 
    #endregion
    public partial class PanelForm : Office2007Form
    {


        #region Properties (Propiedades)
        private LocationConexion locationConexion;
        private BuscarLocalidad buscarLocalidad;

        #endregion

        #region Attributes (Atributos)
        public string usuario, nivel, logotipo, localidad, sucursal;
        #endregion

        #region Events (Eventos)

        private void PanelForm_Load(object sender, EventArgs e)
        {


            cargar();
        }

        private void metroTileItem4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEmpezar_Click(object sender, EventArgs e)
        {
            //Comenzar a segunda ventana
            //enviamos un datatable con las empresas Seleccionadas
            DataTable result = new DataTable();
            result.Columns.Add();
            result.Clear();

            foreach (DataGridViewRow row in dataGridViewX1.Rows)
            {
                Boolean bol = Convert.ToBoolean(row.Cells[0].Value);
                if (bol == true)
                {
                    result.Rows.Add(row.Cells[1].Value.ToString());
                }
                    
            }
            
            ConfigRepForm configRepForm = new ConfigRepForm(result);
            configRepForm.Show();
            
        }

        private void checkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            if (checkSeleccionar.Checked == true)
            {
                foreach (DataGridViewRow item in dataGridViewX1.Rows)
                {
                    item.Cells["Seleccionar"].Value = true;
                }
            }
            else
            {

                foreach (DataGridViewRow item in dataGridViewX1.Rows)
                {
                    item.Cells["Seleccionar"].Value = false;
                }

            }


        }
        #endregion

        #region Methods (Metodos)
        private void cargar()
        {

            metroTileItem1.Text = usuario +
                                 "\n" + nivel;
            metroTileItem2.Text = localidad +
                                  "\n" + sucursal;
            metroTileItem3.Text = DateTime.Now.ToString(string.Format("dddd{2}dd{2}{0}{2}MMMM{2}{1}yyyy", "De", "Del", " "));

            DataTable result = new DataTable();
            result = buscarLocalidad.EmpresasComboList();
            dataGridViewX1.RowHeadersVisible = false;
            dataGridViewX1.DefaultCellStyle.WrapMode = DataGridViewTriState.True; //muestro todo el texto

            foreach (DataRow item in result.Rows)
            {
                dataGridViewX1.Rows.Add(true, item[0].ToString());

            }

            ////marcar las empresas

            //foreach (DataGridViewRow item in dataGridViewX1.Rows)
            //{
            //    item.Cells["Seleccionar"].Value = true;
            //}

        }

        #endregion

        #region Constructors (Constructores)
        public PanelForm()
        {
            InitializeComponent();
            locationConexion = new LocationConexion();
            buscarLocalidad = new BuscarLocalidad();

        }

        #endregion





    }
}

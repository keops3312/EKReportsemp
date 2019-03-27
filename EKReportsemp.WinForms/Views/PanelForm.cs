using DevComponents.DotNetBar;
using EKReportsemp.WinForms.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EKReportsemp.WinForms.Views
{
    public partial class PanelForm : Office2007Form
    {


        #region Properties (Propiedades)
        private LocationConexion locationConexion;
        private BuscarLocalidad buscarLocalidad;
        private PanelForm panelForm;

        #endregion

        public PanelForm()
        {
            InitializeComponent();
            locationConexion = new LocationConexion();
            buscarLocalidad = new BuscarLocalidad();

        }

        private void PanelForm_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            dataGridViewX1.DataSource = buscarLocalidad.EmpresasComboList();
        }
    }
}



namespace EKReportsemp.WinForms.Views
{
    #region Libraries (Librerias)

    using DevComponents.DotNetBar;
    using EKReportsemp.WinForms.Classes;
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    #endregion

    public partial class LoginForm : Office2007Form
    {
        #region MyRegion
        //[DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        //private static extern IntPtr CreateRoundRectRgn
        //(
        //    int nLeftRect,
        //    int nTopRect,
        //    int nRightRect,
        //    int nBottomRect,
        //    int nWidthEllipse,
        //    int nHeightEllipse


        //);
        //this in the the construcyor princiapal
        //this.FormBorderStyle = FormBorderStyle.None;
        //this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(10, 10,Width,Height, 10, 10));

        #endregion


        #region Properties (Propiedades)
        private LocationConexion locationConexion;
        private BuscarLocalidad buscarLocalidad;
        private PanelForm panelForm;

        #endregion

        #region Attributes
        private int Response;
        private string Clave;
        private string NombreAuditor;
        private string nivel;
        public int valor;
        String[] result;

     
        public int response
        {

            get
            {
                return Response;

            }
            set
            {
                Response = value;
            }
        }

        public string clave
        {
            get
            {
                return Clave;
            }
            set
            {
                Clave = value;
            }
        }

        public string nombreAuditor
        {
            get
            {
                return NombreAuditor;
            }
            set
            {
                NombreAuditor = value;
            }
        }



        #endregion

        #region Constructors (Constructores)
        public LoginForm()
        {
            InitializeComponent();
            locationConexion = new LocationConexion();
            buscarLocalidad = new BuscarLocalidad();
          
            backgroundWorker1.WorkerReportsProgress = true;

            backgroundWorker1.WorkerSupportsCancellation = true;
        }
        #endregion

        #region Methods (Metodos)
        public void buscar()
        {
            result = locationConexion.Scan();

        }

        private void ExitApp()
        {
            superValidator1.Enabled = false;
            Application.Exit();
        }

        private void ChekUser()
        {
            //check si esta visible el combo de ser asi guardamos la configuracion para poder trabajar con la sucursal

            if (string.IsNullOrEmpty(txtUser.Text) || string.IsNullOrWhiteSpace(txtUser.Text))
            {

                MessageBoxEx.EnableGlass = false;
                MessageBoxEx.Show(
                    "Ingrese Usuario Por favor", "EK ReportSemp",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUser.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {

                MessageBoxEx.EnableGlass = false;
                MessageBoxEx.Show(
                    "Ingrese Contraseña Por favor", "EK ReportSemp",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPassword.Focus();
                return;
            }

            //chek usuario y nivel

            nivel = buscarLocalidad.CheckUsuario(txtUser.Text, txtPassword.Text);




            if (!string.IsNullOrEmpty(nivel))
            {
                String[] array = new string[7];
                panelForm = new PanelForm();
                array = buscarLocalidad.DatosTitular(txtUser.Text, txtPassword.Text);
                panelForm.usuario = array[0].ToString();
                panelForm.nivel = array[1].ToString();
                panelForm.localidad = array[2].ToString();
                panelForm.sucursal = array[3].ToString();
                panelForm.logotipo = array[4].ToString();

                this.Hide();
                panelForm.loginForm = this;
                panelForm.ShowDialog();
            }
            else
            {

                MessageBoxEx.EnableGlass = false;
                MessageBoxEx.Show(
                    "Usuario y/o Contraseña Incorrectos\n" +
                    "VERIFICAR POR FAVOR", "EK ReportSemp",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtUser.Focus();
                return;

            }




        }
        #endregion

        #region Events (Eventos)
        private void LoginForm_Load(object sender, EventArgs e)
        {
            txtUser.Focus();

            if (valor == 1)
            {
                buscarLocalidad = new BuscarLocalidad();
                circularProgress1.Visible = false;
                circularProgress1.IsRunning = false;
                circularProgress1.ProgressText = "";
                String[] find = buscarLocalidad.LocalidadBuscada();
                lblResponse.Text = "Conexion Encontrada...\n" +
                 "Nom: " + find[0].ToString() + "\n" +
                 "Localidad: " + find[1].ToString() + "\n" +
                 "Direccion: " + find[2].ToString();

            }
            else
            {
                btnAcces.Enabled = false;
                circularProgress1.Visible = true;
                circularProgress1.IsRunning = true;
                circularProgress1.ProgressText = "Buscando...";
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void btnAcces_Click(object sender, EventArgs e)
        {

            ChekUser();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            ExitApp();
        }

        #endregion

        #region BackGroundWorkers (Hilos)
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            circularProgress1.Visible = false;
            circularProgress1.IsRunning = false;
            btnAcces.Enabled = true;
            lblResponse.Text = "Conexion Encontrada...\n" +
            "Nom: " + result[0].ToString() + "\n" +
            "Localidad: " + result[1].ToString() + "\n" +
            "Direccion: " + result[2].ToString();
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            circularProgress1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            buscar();
           
        }


        #endregion

       
    }
}

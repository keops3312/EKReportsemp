

namespace EKReportsemp.WinForms.Classes
{


    #region Libraries (Librerias)
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using EKReportsemp.WinForms.Context;
    #endregion


    public class BuscarLocalidad
    {
        #region Context
        private SEMP2013_Context db;

        public BuscarLocalidad()
        {
            db = new SEMP2013_Context();

        }
        #endregion

        #region Methods (metodos)
     
        
        #region LoginForm
        public String[] LocalidadBuscada()
        {

            String[] array = new string[3];
            var localidad = db.Localidades.Where(p => p.impresora == "RAIZ").First();

            array[0] = localidad.LOCALIDAD;
            array[1] = localidad.Nombre_Sucursal;
            array[2] = localidad.DIRECCION;


            return array;
        }

        public string CheckUsuario(string user, string password)
        {
            string result = "";
            result.DefaultIfEmpty();
            var usuario = db.PRVyusuarios.Where(p => p.USUARIO == user &&
                                                 p.CONTRASEÑA == password).FirstOrDefault();
            if (usuario != null)
            {
                if (usuario.TIPO_USUARIO.Contains("Sistemas") ||
                   usuario.TIPO_USUARIO.Contains("Junior usuario") ||
                   usuario.TIPO_USUARIO.Contains("Master usuario"))
                {

                    result = usuario.TIPO_USUARIO;
                }

            }

            return result;

        }
        #endregion

        #region PanelForm
        public DataTable Sucursales()
        {
            DataTable result = new DataTable();
            result.Columns.AddRange(new DataColumn[2]
            {
                 new DataColumn("bd"),
                 new DataColumn("Sucursal"),
            }
            );

            string conn = ConfigurationManager.ConnectionStrings["SEMP2013_CNX"].ConnectionString;
            SqlConnection cn = new SqlConnection(conn);
            SqlDataAdapter cmd = new SqlDataAdapter("USE SEMP2013_NAU_1 " +
                " SELECT BD,[Nombre Sucursal]  " +
                " from Localidades where Concepto='CASA DE EMPEÑO' ", cn);
            cmd.Fill(result);

            return result;
        }

        public String[] DatosTitular(string user, string password)
        {
            String[] array = new string[7];



            var usuario = db.PRVyusuarios.Where(p => p.USUARIO == user &&
                                                 p.CONTRASEÑA == password).FirstOrDefault();
            if (usuario != null)
            {
                if (usuario.TIPO_USUARIO.Contains("Sistemas") ||
                   usuario.TIPO_USUARIO.Contains("Junior usuario") ||
                   usuario.TIPO_USUARIO.Contains("Master usuario"))
                {
                    var empleado = db.Empleados.Where(c => c.NoEmpleado == usuario.NO_OPERADOR).FirstOrDefault();

                    var localidad = db.Localidades.Where(p => p.impresora == "RAIZ").First();





                    array[0] = empleado.Nombre_Completo;
                    array[1] = usuario.TIPO_USUARIO;
                    array[2] = localidad.LOCALIDAD;
                    array[3] = localidad.Nombre_Sucursal;
                    array[4] = localidad.Logotipo;




                }

            }



            return array;
        }
        //LLeno Empresas Encontradas
        public DataTable EmpresasComboList()
        {
            DataTable result = new DataTable();
            result.Columns.Add();

            var empresas = db.Empresas.ToList();
            foreach (var empresa in empresas)
            {
                result.Rows.Add(empresa.Empresa);
            }


            return result;
        }

        #endregion

        #region ConfigRepForm
        public DataTable BuscarSucursales(DataTable empresas)
        {
           
            DataTable result = new DataTable();
            result.Columns.Add();
            result.Columns.Add();

           
            foreach  (DataRow empresa in empresas.Rows)
            {
                string x = empresa[0].ToString();
                var sucursales = db.Localidades.Where(c => c.CONCEPTO == "CASA DE EMPEÑO"
                                && c.Empresa == x).ToList();

                foreach (var item in sucursales)
                {
                    result.Rows.Add(item.Nombre_Sucursal, item.BD);
                }
               
                
            }

            return result;
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


        #endregion







    }
}

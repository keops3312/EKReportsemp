

namespace EKReportsemp.WinForms.Classes
{


    #region Libraries (Librerias)
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;
    using EDsemp.Classes;
    #endregion
    public class ResultadosOperacion
    {

        #region Properties (Propiedades)
        DataTable resultPrestamosPorCaja;
        DataTable resultNotasDePagoPorCaja;
        DataTable resultRemisionesPorCaja;
        DataTable resultCajas;
        DataTable baseResult;
        SqlConnection cnn;
        SqlDataAdapter sqlDataAdapter;
        #endregion

        #region Atributes (Atributos)
        public string serv;
        public string bd;
        public string usr;
        public string psw;
        private string sqlcnx;
        #endregion

        #region Methods (Metodos)
        public string CheckDataConection()
        {


            //aligual que las demas aplicaciones cargaremos nuestra llave al servidor de oficinas para la conexion directa
            string cadena = "C:/SEMP2013/EKPolizaGastos/EKPolizaGastos/cdb.txt";

            using (StreamReader sr1 = new StreamReader(cadena, true))
            {

                string lineA = sr1.ReadLine();
                string lineB = sr1.ReadLine();
                string lineC = sr1.ReadLine();
                string lineF = sr1.ReadLine();

                //ahroa desecrypto la informacion             
                serv = Encriptar_Desencriptar.DecryptKeyMD5(lineA);
                bd = Encriptar_Desencriptar.DecryptKeyMD5(lineB);
                usr = Encriptar_Desencriptar.DecryptKeyMD5(lineC);
                psw = Encriptar_Desencriptar.DecryptKeyMD5(lineF);
                //ahora realizo la conexion par amostrar las sucursales


                sqlcnx = "Data Source=" + serv + " ;" +
                    "Initial Catalog=" + bd + ";" +
                    "Persist Security Info=True;" +
                    "User ID=" + usr + ";Password=" + psw + "";
                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = sqlcnx;
                conexion.Open();

                if (true)
                {
                    return sqlcnx;
                }




            }




        }

        #region Prestamos
        //Crea un DataTable de las operaciones de todas las cajas conteniendo las operaciones una por una por dia PRESTAMOS
        public DataTable PrestamosXdiaUnoAUno(int month, int year,
            string database, int opc, DateTime? fechaInicio,
            DateTime? fechaFinal, string cnx)
        {

            string sql;
            string caja;
            string mes;
            string contrato;
            decimal prestamo;
            string cajaLetra;
            DateTime fecha;

            int dias;



            DateTime inicio;
            resultCajas = new DataTable();
            resultPrestamosPorCaja = new DataTable("Prestamos");
            resultPrestamosPorCaja.Columns.Add("Contrato");
            resultPrestamosPorCaja.Columns.Add("Prestamo");
            resultPrestamosPorCaja.Columns.Add("Fecha");
            resultPrestamosPorCaja.Columns.Add("Caja");
            resultPrestamosPorCaja.Columns.Add("CajaLetra");
            resultPrestamosPorCaja.Columns.Add("Mes");
            resultPrestamosPorCaja.Columns.Add("Ano");
            resultPrestamosPorCaja.Columns.Add("Suc");


            dias = DateTime.DaysInMonth(year, month);



            mes = month.ToString();
            if (month < 10) { mes = "0" + month.ToString(); }


            sql = "USE " + database + " SELECT NumCaja as Cajas from selcaja";
            sqlDataAdapter = new SqlDataAdapter(sql, cnx);
            sqlDataAdapter.Fill(resultCajas);


            foreach (DataRow row in resultCajas.Rows)
            {
                caja = row[0].ToString();

                for (int i = 1; i <= dias; i++)
                {
                    inicio = DateTime.Parse(year + "-" + mes + "-" + i);
                    if (i < 10)
                    {
                        inicio = DateTime.Parse(year + "-" + mes + "-0" + i);
                    }


                    sql = "USE " + database + "  " +
                     "SELECT Contratos.contrato as Contrato , contratos.prestamo as Prestamo," +
                     " contratos.fechacons as Fecha, " + caja + ".Caja as Caja FROM  contratos INNER JOIN " + caja + " " +
                         " ON contratos.contrato = " + caja + ".contrato  " +
                            " WHERE contratos.Fechacons= '" + Convert.ToDateTime(inicio).ToString("dd-MM-yyyy") + "'" +
                                 " and " + caja + ".concepto LIKE '%PRESTAMO%' " +
                                        " and contratos.Status<>'CANCELADO'  order by contratos.contrato asc " +
                                             "";
                    sqlDataAdapter = new SqlDataAdapter(sql, cnx);
                    baseResult = new DataTable();
                    baseResult.Clear();
                    sqlDataAdapter.Fill(baseResult);
                    if (baseResult.Rows.Count > 0)
                    {
                        foreach (DataRow rows in baseResult.Rows)
                        {
                            contrato = rows[0].ToString();
                            prestamo = decimal.Parse(rows[1].ToString());
                            fecha = DateTime.Parse(rows[2].ToString());
                            cajaLetra = rows[3].ToString();
                            //mes
                            //año
                            //caja

                            resultPrestamosPorCaja.Rows.Add(contrato, prestamo, fecha, caja, cajaLetra, month, year, database.Substring(9));
                        }
                    }



                }


            }

            return resultPrestamosPorCaja;
        }

        //Crea un DataTable de la suma de las operaciones totales de prestamos de cajas por dia PRESTAMOS
        public DataTable PrestamosXdiaResumen(int month, int year,
            string database, int opc, DateTime fechaInicio,
            DateTime fechaFinal, string cnx, int unifica, int rangoSemana, 
            string caja,string emp, string loc,string cajaNom)//unifica 1=si 2=no--- rangoSemana 1=si 2=no
        {

            string sql;
          
            decimal prestamo;
            string cajaLetra;
            DateTime fecha;


            DateTime inicio;
            resultCajas = new DataTable();      

            resultPrestamosPorCaja = new DataTable("Prestamos");
            resultPrestamosPorCaja.Columns.Add("Fecha");
            resultPrestamosPorCaja.Columns.Add("Localidad");
            resultPrestamosPorCaja.Columns.Add("NomSuc");
            resultPrestamosPorCaja.Columns.Add("BD");
            resultPrestamosPorCaja.Columns.Add("Mes");
            resultPrestamosPorCaja.Columns.Add("Ano");
            resultPrestamosPorCaja.Columns.Add("Caja");
            resultPrestamosPorCaja.Columns.Add("CajaNom");
            resultPrestamosPorCaja.Columns.Add("Empresa");
            resultPrestamosPorCaja.Columns.Add("Prestamo");


          

            //SI LO QUIERE UNIFICADO//
            if (unifica == 1)
            {
                inicio = fechaInicio;

                while (inicio <= fechaFinal)
                {


                    sql = "USE " + database + "  " +
                     "SELECT  sum(contratos.prestamo) as Prestamo," +
                     " contratos.fechacons as Fecha FROM  contratos " +
                            " WHERE contratos.Fechacons= '" + Convert.ToDateTime(inicio).ToString("dd-MM-yyyy") + "'" +
                                  " and contratos.Status<>'CANCELADO' group by contratos.FechaCons" +
                                             "";

                    sqlDataAdapter = new SqlDataAdapter(sql, cnx);
                    baseResult = new DataTable();
                    baseResult.Clear();
                    sqlDataAdapter.Fill(baseResult);
                    if (baseResult.Rows.Count > 0)
                    {



                        prestamo = decimal.Parse(baseResult.Rows[0][0].ToString());
                        fecha = DateTime.Parse(baseResult.Rows[0][1].ToString());
                        //cajaLetra = baseResult.Rows[0][2].ToString();
                        //mes
                        //año
                        //caja

                      

                        resultPrestamosPorCaja.Rows.Add(fecha,loc, database.Substring(9),
                                                    database,fecha.Month.ToString(),fecha.Year.ToString(),
                                                      caja,cajaNom,emp,prestamo);


                    }
                    else {

                        resultPrestamosPorCaja.Rows.Add(inicio, loc, database.Substring(9),
                                                   database, inicio.Month.ToString(), inicio.Year.ToString(),
                                                     caja, cajaNom, emp, 0);

                       
                    }


                    inicio = inicio.AddDays(1);


                }
            }
            else
            {

                inicio = fechaInicio;


                while (inicio <= fechaFinal)
                {


                    sql = "USE " + database + "  " +
                     "SELECT  sum(contratos.prestamo) as Prestamo," +
                     " contratos.fechacons as Fecha, " + cajaNom + ".Caja as Caja FROM  contratos INNER JOIN " + cajaNom + " " +
                         " ON contratos.contrato = " + cajaNom + ".contrato  " +
                            " WHERE contratos.Fechacons= '" + Convert.ToDateTime(inicio).ToString("dd-MM-yyyy") + "'" +
                                 " and " + cajaNom + ".concepto LIKE '%PRESTAMO%' " +
                                        " and contratos.Status<>'CANCELADO' group by contratos.FechaCons, " + cajaNom + ".Caja " +
                                             "";

                    sqlDataAdapter = new SqlDataAdapter(sql, cnx);
                    baseResult = new DataTable();
                    baseResult.Clear();
                    sqlDataAdapter.Fill(baseResult);
                    if (baseResult.Rows.Count > 0)
                    {



                        prestamo = decimal.Parse(baseResult.Rows[0][0].ToString());
                        fecha = DateTime.Parse(baseResult.Rows[0][1].ToString());
                        cajaLetra = baseResult.Rows[0][2].ToString();
                        //mes
                        //año
                        //caja

                        resultPrestamosPorCaja.Rows.Add(fecha, loc, database.Substring(9),
                                                    database, fecha.Month.ToString(), fecha.Year.ToString(),
                                                      caja, cajaNom, emp, prestamo);


                    }
                    else
                    {

                        resultPrestamosPorCaja.Rows.Add(inicio, loc, database.Substring(9),
                                                   database, inicio.Month.ToString(), inicio.Year.ToString(),
                                                     caja, cajaNom, emp, 0);

                    }


                    inicio = inicio.AddDays(1);


                }


            }

            return resultPrestamosPorCaja;
        }


        #endregion


        #region Notas de Pago
        //Crea un DataTable de las operaciones de todas las cajas conteniendo las operaciones una por una por dia de NOTAS DE PAGO
        public DataTable NotasDePagoXdiaUnoAUno(int month, int year,
            string database, int opc, DateTime? fechaInicio,
            DateTime? fechaFinal, string cnx)
        {

            string sql;
            string caja;
            string mes;
            string contrato;
            string factura;
            string estatus;
            decimal total;
            decimal debe;
            decimal subtotal;
            decimal iva;
            decimal prestamo;
            string cajaLetra;
            DateTime fecha;

            int dias;



            DateTime inicio;
            resultCajas = new DataTable();
            resultNotasDePagoPorCaja = new DataTable("NotasDePago");
            resultNotasDePagoPorCaja.Columns.Add("Factura");
            resultNotasDePagoPorCaja.Columns.Add("Contrato");
            resultNotasDePagoPorCaja.Columns.Add("Debe");
            resultNotasDePagoPorCaja.Columns.Add("Subtotal");
            resultNotasDePagoPorCaja.Columns.Add("Iva");
            resultNotasDePagoPorCaja.Columns.Add("Total");
            resultNotasDePagoPorCaja.Columns.Add("Estatus");
            resultNotasDePagoPorCaja.Columns.Add("FechaFact");
            resultNotasDePagoPorCaja.Columns.Add("Caja");
            resultNotasDePagoPorCaja.Columns.Add("CajaLetra");
            resultNotasDePagoPorCaja.Columns.Add("Mes");
            resultNotasDePagoPorCaja.Columns.Add("Ano");
            resultNotasDePagoPorCaja.Columns.Add("Suc");
            resultNotasDePagoPorCaja.Columns.Add("Prestamo");


            dias = DateTime.DaysInMonth(year, month);



            mes = month.ToString();
            if (month < 10) { mes = "0" + month.ToString(); }


            sql = "USE " + database + " SELECT NumCaja as Cajas from selcaja";
            sqlDataAdapter = new SqlDataAdapter(sql, cnx);
            sqlDataAdapter.Fill(resultCajas);


            foreach (DataRow row in resultCajas.Rows)
            {
                caja = row[0].ToString();

                for (int i = 1; i <= dias; i++)
                {
                    inicio = DateTime.Parse(year + "-" + mes + "-" + i);
                    if (i < 10)
                    {
                        inicio = DateTime.Parse(year + "-" + mes + "-0" + i);
                    }


                    sql = "USE " + database + "  " +
                    "SELECT facturas.factura as Factura, facturas.contrato as Contrato ," + caja + ".debe as Debe, " +
                      " facturas.importefact as Subtotal, facturas.ivafact as Iva, facturas.totalfact as Total, facturas.status as Estatus" +
                      " , facturas.fechafact , " + caja + ".caja FROM  facturas " +
                      " INNER JOIN " + caja + " ON facturas.factura = " + caja + ".folio " +
                       " WHERE facturas.FechaFact =  '" + Convert.ToDateTime(inicio).ToString("dd-MM-yyyy") + "'" +
                       "  and " + caja + ".concepto like '%DESEMP%'  and facturas.status = 'VALIDO' order by facturas.factura asc" +
                                             "";
                    sqlDataAdapter = new SqlDataAdapter(sql, cnx);
                    baseResult = new DataTable();
                    baseResult.Clear();
                    sqlDataAdapter.Fill(baseResult);
                    if (baseResult.Rows.Count > 0)
                    {
                        foreach (DataRow rows in baseResult.Rows)
                        {
                            factura = rows[0].ToString();
                            contrato = rows[1].ToString();
                            debe = decimal.Parse(rows[2].ToString());
                            subtotal = decimal.Parse(rows[3].ToString());
                            iva = decimal.Parse(rows[4].ToString());
                            total = decimal.Parse(rows[5].ToString());
                            estatus = rows[6].ToString();
                            fecha = DateTime.Parse(rows[7].ToString());
                            cajaLetra = rows[8].ToString();
                            prestamo = debe - total;
                            //mes
                            //año
                            //caja

                            resultNotasDePagoPorCaja.Rows.Add(factura, contrato, debe, subtotal, iva,
                                total, estatus, fecha, caja, cajaLetra, month, year, database.Substring(9), prestamo);
                        }
                    }



                }


            }



            return resultNotasDePagoPorCaja;
        }

        //Crea un DataTable de la suma de las operaciones totales de prestamos de cajas por dia de NOTAS DE PAGO
        public DataTable NotasDePagoXdiaResumen(int month, int year, string database,
            int opc, DateTime fechaInicio,
            DateTime fechaFinal, string cnx, int unifica, int rangoSemana, string caja
            , string emp, string loc, string cajaNom)
        {

            string sql;
            decimal total;

            decimal subtotal;
            decimal iva;

            string cajaLetra;
            DateTime fecha;
            
            DateTime inicio;
            resultCajas = new DataTable();
            resultNotasDePagoPorCaja = new DataTable("NotasDePago");
            resultNotasDePagoPorCaja.Columns.Add("Fecha");
            resultNotasDePagoPorCaja.Columns.Add("Localidad");
            resultNotasDePagoPorCaja.Columns.Add("NomSuc");
            resultNotasDePagoPorCaja.Columns.Add("BD");
            resultNotasDePagoPorCaja.Columns.Add("Mes");
            resultNotasDePagoPorCaja.Columns.Add("Ano");
            resultNotasDePagoPorCaja.Columns.Add("Caja");
            resultNotasDePagoPorCaja.Columns.Add("CajaNom");
            resultNotasDePagoPorCaja.Columns.Add("Empresa");
            resultNotasDePagoPorCaja.Columns.Add("Subtotal");
            resultNotasDePagoPorCaja.Columns.Add("Iva");
            resultNotasDePagoPorCaja.Columns.Add("Total");



            //SI LO QUIERE UNIFICADO//
            if (unifica == 1)
            {
                inicio = fechaInicio;

                while (inicio <= fechaFinal)
                {

                    sql = "USE " + database + "  " +
                     " SELECT " +
                " sum(facturas.importefact), sum(facturas.ivafact), sum(facturas.totalfact), " +
                "facturas.fechafact FROM  facturas " +
                " WHERE facturas.FechaFact='" + Convert.ToDateTime(inicio).ToString("dd-MM-yyyy") + "' " +
                " and  facturas.status = 'VALIDO' " +
                " group by facturas.FechaFact ";

                    sqlDataAdapter = new SqlDataAdapter(sql, cnx);
                    baseResult = new DataTable();
                    baseResult.Clear();
                    sqlDataAdapter.Fill(baseResult);
                    if (baseResult.Rows.Count > 0)
                    {



                      


                        subtotal = decimal.Parse(baseResult.Rows[0][0].ToString());
                        iva = decimal.Parse(baseResult.Rows[0][1].ToString());
                        total = decimal.Parse(baseResult.Rows[0][2].ToString());
                        fecha = DateTime.Parse(baseResult.Rows[0][3].ToString());
                        //cajaLetra = baseResult.Rows[0][4].ToString();

                        //mes
                        //año
                        //caja

                      



                        resultNotasDePagoPorCaja.Rows.Add(fecha, loc, database.Substring(9), database,
                            fecha.Month.ToString(), fecha.Year.ToString(), caja, cajaNom,emp, subtotal, iva,
                            total);

                    }
                    else
                    {

                        resultNotasDePagoPorCaja.Rows.Add(inicio, loc, database.Substring(9), database,
                           inicio.Month.ToString(), inicio.Year.ToString(), caja, cajaNom, emp, 0, 0,
                           0);
                    }


                    inicio = inicio.AddDays(1);


                }
            }
            else
            {

                inicio = fechaInicio;

                while (inicio <= fechaFinal)
                {

                    sql = "USE " + database + "  " +
                     " SELECT " +
                " sum(facturas.importefact), sum(facturas.ivafact), sum(facturas.totalfact), " +
                "facturas.fechafact," + cajaNom + ".caja FROM  facturas " +
                " INNER JOIN " + cajaNom + " ON facturas.factura = " + cajaNom + ".folio " +
                " WHERE facturas.FechaFact = '" + Convert.ToDateTime(inicio).ToString("dd-MM-yyyy") + "' " +
                " and " + cajaNom + ".concepto like '%DESEMP%'  and facturas.status = 'VALIDO' " +
                " group by facturas.FechaFact , " + cajaNom + ".Caja";

                    sqlDataAdapter = new SqlDataAdapter(sql, cnx);
                    baseResult = new DataTable();
                    baseResult.Clear();
                    sqlDataAdapter.Fill(baseResult);
                    if (baseResult.Rows.Count > 0)
                    {


                        subtotal = decimal.Parse(baseResult.Rows[0][0].ToString());
                        iva = decimal.Parse(baseResult.Rows[0][1].ToString());
                        total = decimal.Parse(baseResult.Rows[0][2].ToString());
                        fecha = DateTime.Parse(baseResult.Rows[0][3].ToString());
                        cajaLetra = baseResult.Rows[0][4].ToString();

                        //mes
                        //año
                        //caja

                        resultNotasDePagoPorCaja.Rows.Add(fecha, loc, database.Substring(9), database,
                          fecha.Month.ToString(), fecha.Year.ToString(), caja, cajaNom, emp, subtotal, iva,
                          total);

                    }
                    else
                    {

                        resultNotasDePagoPorCaja.Rows.Add(inicio, loc, database.Substring(9), database,
                           inicio.Month.ToString(), inicio.Year.ToString(), caja, cajaNom, emp, 0, 0,
                           0);
                    }

                    inicio = inicio.AddDays(1);
                }

            }

            return resultNotasDePagoPorCaja;

          

        }


        #endregion


        #region Notas de Remision
        //Crea un DataTable de las operaciones de todas las cajas conteniendo las operaciones una por una por dia de REMISIONES
        public DataTable RemisionesXdiaUnoAUno(int month, int year,
            string database, int opc, DateTime? fechaInicio,
            DateTime? fechaFinal, string cnx, decimal iva, decimal parte)
        {

            string sql;
            string caja;
            string mes;

            decimal subtotalParte;
            decimal ivaParte;
            decimal ivaTotal;
            decimal importe;
            string cajaLetra;
            string NumRemision;
            DateTime fecha;

            int dias;



            DateTime inicio;
            resultCajas = new DataTable();
            resultRemisionesPorCaja = new DataTable("Remisiones");
            resultRemisionesPorCaja.Columns.Add("NumRemision");
            resultRemisionesPorCaja.Columns.Add("Fecha");

            resultRemisionesPorCaja.Columns.Add("Importe");//Total
            resultRemisionesPorCaja.Columns.Add("Iva");//Iva tomando el total

            resultRemisionesPorCaja.Columns.Add("IvaParte");
            resultRemisionesPorCaja.Columns.Add("subtotalParte");//EL 97.5 que se toma

            resultRemisionesPorCaja.Columns.Add("Caja");
            resultRemisionesPorCaja.Columns.Add("CajaLetra");
            resultRemisionesPorCaja.Columns.Add("Mes");
            resultRemisionesPorCaja.Columns.Add("Ano");
            resultRemisionesPorCaja.Columns.Add("Suc");



            dias = DateTime.DaysInMonth(year, month);



            mes = month.ToString();
            if (month < 10) { mes = "0" + month.ToString(); }


            sql = "USE " + database + " SELECT NumCaja as Cajas, Caja from selcaja";
            sqlDataAdapter = new SqlDataAdapter(sql, cnx);
            sqlDataAdapter.Fill(resultCajas);


            foreach (DataRow row in resultCajas.Rows)
            {
                caja = row[1].ToString();

                for (int i = 1; i <= dias; i++)
                {
                    inicio = DateTime.Parse(year + "-" + mes + "-" + i);
                    if (i < 10)
                    {
                        inicio = DateTime.Parse(year + "-" + mes + "-0" + i);
                    }


                    sql = "USE " + database + "  " +
                    "SELECT  NumRemision, Fecha,Sum(importe), Caja" +
                      "  FROM  Remisiones " +
                       " WHERE fecha =  '" + Convert.ToDateTime(inicio).ToString("dd-MM-yyyy") + "'" +
                       "  and status='VENDIDO' and caja='" + caja + "' group by numRemision, fecha,caja" +
                                             "";
                    sqlDataAdapter = new SqlDataAdapter(sql, cnx);
                    baseResult = new DataTable();
                    baseResult.Clear();
                    sqlDataAdapter.Fill(baseResult);
                    if (baseResult.Rows.Count > 0)
                    {
                        foreach (DataRow rows in baseResult.Rows)
                        {



                            NumRemision = rows[0].ToString();
                            fecha = DateTime.Parse(rows[1].ToString());
                            importe = decimal.Parse(rows[2].ToString());
                            cajaLetra = rows[3].ToString();

                            ivaTotal = importe * iva;
                            //tomando el dichoso 97.5

                            subtotalParte = importe * parte / 100;
                            ivaParte = subtotalParte * iva;

                            //mes
                            //año
                            //caja


                            resultRemisionesPorCaja.Rows.Add(NumRemision, fecha, importe, ivaTotal,
                               ivaParte, subtotalParte, row[0].ToString(), cajaLetra, month, year, database.Substring(9));
                        }
                    }



                }


            }



            return resultRemisionesPorCaja;
        }

        //Crea un DataTable de la suma de las operaciones totales de prestamos de cajas por dia de NOTAS DE PAGO
        public DataTable RemisionesXdiaResumen(int month, int year, string database,
            int opc, DateTime fechaInicio,
            DateTime fechaFinal, string cnx, int unifica, int rangoSemana, string caja, decimal iva, decimal parte,
           string  emp, string localidad,string cajaletra)
        {
            string sql;
           
            decimal subtotalParte;
            decimal totalParte;
            decimal Subtotal;
            decimal ivaParte;
            decimal ivaTotal;
            decimal total;
            string cajaLetra;

            DateTime fecha;

            DateTime inicio;
            resultCajas = new DataTable();

            resultRemisionesPorCaja = new DataTable("Remisiones");
            resultRemisionesPorCaja.Columns.Add("Fecha");
            resultRemisionesPorCaja.Columns.Add("Localidad");
            resultRemisionesPorCaja.Columns.Add("NomSuc");
            resultRemisionesPorCaja.Columns.Add("BD");
            resultRemisionesPorCaja.Columns.Add("Mes");
            resultRemisionesPorCaja.Columns.Add("Ano");
            resultRemisionesPorCaja.Columns.Add("Caja");
            resultRemisionesPorCaja.Columns.Add("CajaNom");
            resultRemisionesPorCaja.Columns.Add("Empresa");

            resultRemisionesPorCaja.Columns.Add("Subtotal");//Total
            resultRemisionesPorCaja.Columns.Add("Iva");//Iva tomando el total
            resultRemisionesPorCaja.Columns.Add("Total");//Total

            resultRemisionesPorCaja.Columns.Add("subtotalParte");//EL 97.5 que se toma
            resultRemisionesPorCaja.Columns.Add("IvaParte");
            resultRemisionesPorCaja.Columns.Add("TotalParte");//EL 97.5 que se toma


            //SI LO QUIERE UNIFICADO//
            if (unifica == 1)
            {
                inicio = fechaInicio;

                while (inicio <= fechaFinal)
                {
                    sql = "USE " + database + "  " +
                          "SELECT Fecha,Sum(importe)" +
                            "  FROM  Remisiones " +
                             " WHERE fecha =  '" + Convert.ToDateTime(inicio).ToString("dd-MM-yyyy") + "'" +
                             "  and status='VENDIDO' group by  fecha" +
                                                   "";
                    sqlDataAdapter = new SqlDataAdapter(sql, cnx);
                    baseResult = new DataTable();
                    baseResult.Clear();
                    sqlDataAdapter.Fill(baseResult);
                    if (baseResult.Rows.Count > 0)
                    {
                        foreach (DataRow rows in baseResult.Rows)
                        {
                           
                            fecha = DateTime.Parse(rows[0].ToString());

                            total = decimal.Parse(rows[1].ToString());

                            ivaTotal = total * iva;

                            Subtotal = total - ivaTotal;



                         
                            //tomando el dichoso 97.5 o cualquier porcentaje del total de la venta
                            totalParte = total * parte / 100;
                            ivaParte = totalParte * iva;
                            subtotalParte = totalParte - ivaParte;
                            //mes
                            //año
                            //caja
                            resultRemisionesPorCaja.Rows.Add(fecha,localidad, database.Substring(9),database,
                                fecha.Month.ToString(),fecha.Year.ToString() ,cajaletra,caja,emp,Subtotal, ivaTotal,total,
                                subtotalParte, ivaParte, totalParte);
                        }
                    }
                    else
                    {

                        resultRemisionesPorCaja.Rows.Add(inicio, localidad, database.Substring(9), database,
                                 inicio.Month.ToString(), inicio.Year.ToString(), cajaletra, caja, emp, 0,0,0,0,0,0);
                    }



                    inicio = inicio.AddDays(1);


                }
            }
            else
            {

                inicio = fechaInicio;

                while (inicio <= fechaFinal)
                {

                    sql = "USE " + database + "  " +
                           "SELECT Fecha,Sum(importe), Caja" +
                             "  FROM  Remisiones " +
                              " WHERE fecha ='" + Convert.ToDateTime(inicio).ToString("dd-MM-yyyy") + "'" +
                              "  and status='VENDIDO' and caja='" + caja + "' group by  fecha,caja" +
                                                    "";
                    sqlDataAdapter = new SqlDataAdapter(sql, cnx);
                    baseResult = new DataTable();
                    baseResult.Clear();
                    sqlDataAdapter.Fill(baseResult);
                    if (baseResult.Rows.Count > 0)
                    {
                        foreach (DataRow rows in baseResult.Rows)
                        {

                            fecha = DateTime.Parse(rows[0].ToString());

                            total = decimal.Parse(rows[1].ToString());

                            cajaLetra = rows[2].ToString();

                            ivaTotal = total * iva;

                            Subtotal = total - ivaTotal;

                            //tomando el dichoso 97.5 o cualquier porcentaje del total de la venta
                            totalParte = total * parte / 100;
                            ivaParte = totalParte * iva;
                            subtotalParte = totalParte - ivaParte;
                            //mes
                            //año
                            //caja
                            resultRemisionesPorCaja.Rows.Add(fecha, localidad, database.Substring(9), database,
                                fecha.Month.ToString(), fecha.Year.ToString(), cajaletra, caja, emp, Subtotal, ivaTotal, total,
                                subtotalParte, ivaParte, totalParte);
                        }
                    }
                    else
                    {

                        resultRemisionesPorCaja.Rows.Add(inicio, localidad, database.Substring(9), database,
                                 inicio.Month.ToString(), inicio.Year.ToString(), cajaletra, caja, emp, 0, 0, 0, 0, 0, 0);
                    }

                    inicio = inicio.AddDays(1);
                }

            }




            #region trash

            //dias = DateTime.DaysInMonth(year, month);



            //mes = month.ToString();
            //if (month < 10) { mes = "0" + month.ToString(); }


            //sql = "USE " + database + " SELECT NumCaja as Cajas, Caja from selcaja";
            //sqlDataAdapter = new SqlDataAdapter(sql, cnx);
            //sqlDataAdapter.Fill(resultCajas);


            //foreach (DataRow row in resultCajas.Rows)
            //{
            //    caja = row[1].ToString();

            //    for (int i = 1; i <= dias; i++)
            //    {
            //        inicio = DateTime.Parse(year + "-" + mes + "-" + i);
            //        if (i < 10)
            //        {
            //            inicio = DateTime.Parse(year + "-" + mes + "-0" + i);
            //        }


            //        sql = "USE " + database + "  " +
            //        "SELECT Fecha,Sum(importe), Caja" +
            //          "  FROM  Remisiones " +
            //           " WHERE fecha =  '" + Convert.ToDateTime(inicio).ToString("dd-MM-yyyy") + "'" +
            //           "  and status='VENDIDO' and caja='" + caja + "' group by  fecha,caja" +
            //                                 "";
            //        sqlDataAdapter = new SqlDataAdapter(sql, cnx);
            //        baseResult = new DataTable();
            //        baseResult.Clear();
            //        sqlDataAdapter.Fill(baseResult);
            //        if (baseResult.Rows.Count > 0)
            //        {
            //            foreach (DataRow rows in baseResult.Rows)
            //            {


            //                fecha = DateTime.Parse(rows[0].ToString());
            //                importe = decimal.Parse(rows[1].ToString());
            //                cajaLetra = rows[2].ToString();

            //                ivaTotal = importe * iva;
            //                //tomando el dichoso 97.5

            //                subtotalParte = importe * parte / 100;
            //                ivaParte = subtotalParte * iva;

            //                //mes
            //                //año
            //                //caja


            //                resultRemisionesPorCaja.Rows.Add(1, fecha, importe, ivaTotal,
            //                   ivaParte, subtotalParte, row[0].ToString(), cajaLetra, month, year, database.Substring(9));
            //            }
            //        }



            //    }


            //}


            #endregion

            return resultRemisionesPorCaja;

        }
        #endregion

        #region Diseño de Resultado Prestamos
        public DataTable diseñoDeResultado(DataTable resumen, DateTime fechaInicio, DateTime fechaFinal)
        {
            //tabla fechas
            DataTable fechas = new DataTable("Fechas");
            fechas.Columns.Add("Fecha");
            //cuantas columnas voy agregar
            string a, b = "";
            int distintas = 0;
            foreach (DataRow item in resumen.Rows)
            {
                a = item[2].ToString();

                if (a != b)
                {
                    b = item[2].ToString();

                    distintas += 1;
                }


            }

            for (int i = 1; i <= distintas; i++)
            {
                fechas.Columns.Add("Localidad");
                fechas.Columns.Add("Año");
                fechas.Columns.Add("Mes");
                fechas.Columns.Add("Prestamo");
            }





            //tomasmos los dias de diferencia para reccorer ese numero de fechas
            int dias = (int)(fechaFinal - fechaInicio).TotalDays;
            for (int i = 0; i <= dias; i++)
            {
                fechas.Rows.Add(resumen.Rows[i][0].ToString());
            }



            return fechas;
           

        }
        #endregion

        #endregion



    }
}



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
            DateTime fechaFinal, string cnx, int unifica, int rangoSemana, string caja)//unifica 1=si 2=no--- rangoSemana 1=si 2=no
        {

            string sql;
           // string caja;
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


           // dias = DateTime.DaysInMonth(year, month);



            //mes = month.ToString();
            //if (month < 10) { mes = "0" + month.ToString(); }


            //sql = "USE " + database + " SELECT NumCaja as Cajas from selcaja";
            //sqlDataAdapter = new SqlDataAdapter(sql, cnx);
            //sqlDataAdapter.Fill(resultCajas);



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

                        resultPrestamosPorCaja.Rows.Add(1, prestamo, fecha, caja, caja, month, year, database.Substring(9));


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
                         " contratos.fechacons as Fecha, " + caja + ".Caja as Caja FROM  contratos INNER JOIN " + caja + " " +
                             " ON contratos.contrato = " + caja + ".contrato  " +
                                " WHERE contratos.Fechacons= '" + Convert.ToDateTime(inicio).ToString("dd-MM-yyyy") + "'" +
                                     " and " + caja + ".concepto LIKE '%PRESTAMO%' " +
                                            " and contratos.Status<>'CANCELADO' group by contratos.FechaCons, " + caja + ".Caja " +
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

                            resultPrestamosPorCaja.Rows.Add(1, prestamo, fecha, caja, cajaLetra, month, year, database.Substring(9));


                        }


                        inicio = inicio.AddDays(1);


                    }


            }











            return resultPrestamosPorCaja;
        }




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
            DateTime fechaFinal, string cnx, int unifica, int rangoSemana, string caja)
        {

            string sql;
           // string caja;
           // string mes;

            decimal total;

            decimal subtotal;
            decimal iva;

            string cajaLetra;
            DateTime fecha;

           // int dias;



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


           // dias = DateTime.DaysInMonth(year, month);



            //mes = month.ToString();
            //if (month < 10) { mes = "0" + month.ToString(); }


            //sql = "USE " + database + " SELECT NumCaja as Cajas from selcaja";
            //sqlDataAdapter = new SqlDataAdapter(sql, cnx);
            //sqlDataAdapter.Fill(resultCajas);


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
                " WHERE facturas.FechaFact = '" + Convert.ToDateTime(inicio).ToString("dd-MM-yyyy") + "' " +
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
                        cajaLetra = baseResult.Rows[0][4].ToString();

                        //mes
                        //año
                        //caja

                        resultNotasDePagoPorCaja.Rows.Add(1, 1, 1, subtotal, iva,
                            total, 1, fecha, caja, caja, 1, 1, database.Substring(9), 1);

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
                "facturas.fechafact," + caja + ".caja FROM  facturas " +
                " INNER JOIN " + caja + " ON facturas.factura = " + caja + ".folio " +
                " WHERE facturas.FechaFact = '" + Convert.ToDateTime(inicio).ToString("dd-MM-yyyy") + "' " +
                " and " + caja + ".concepto like '%DESEMP%'  and facturas.status = 'VALIDO' " +
                " group by facturas.FechaFact , " + caja + ".Caja";

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

                        resultNotasDePagoPorCaja.Rows.Add(1, 1, 1, subtotal, iva,
                            total, 1, fecha, caja, cajaLetra, month, year, database.Substring(9), 1);

                    }


                }


                inicio = inicio.AddDays(1);


          


            }

            return resultNotasDePagoPorCaja;













            //foreach (DataRow row in resultCajas.Rows)
            //{
            //    caja = row[0].ToString();

            //    for (int i = 1; i <= dias; i++)
            //    {
            //        inicio = DateTime.Parse(year + "-" + mes + "-" + i);
            //        if (i < 10)
            //        {
            //            inicio = DateTime.Parse(year + "-" + mes + "-0" + i);
            //        }


            //        sql = "USE " + database + "  " +
            //             " SELECT " +
            //        " sum(facturas.importefact), sum(facturas.ivafact), sum(facturas.totalfact), " +
            //        "facturas.fechafact," + caja + ".caja FROM  facturas " +
            //        " INNER JOIN " + caja + " ON facturas.factura = " + caja + ".folio " +
            //        " WHERE facturas.FechaFact = '" + Convert.ToDateTime(inicio).ToString("dd-MM-yyyy") + "' " +
            //        " and " + caja + ".concepto like '%DESEMP%'  and facturas.status = 'VALIDO' " +
            //        " group by facturas.FechaFact , " + caja + ".Caja";

            //        sqlDataAdapter = new SqlDataAdapter(sql, cnx);
            //        baseResult = new DataTable();
            //        baseResult.Clear();
            //        sqlDataAdapter.Fill(baseResult);
            //        if (baseResult.Rows.Count > 0)
            //        {


            //            subtotal = decimal.Parse(baseResult.Rows[0][0].ToString());
            //            iva = decimal.Parse(baseResult.Rows[0][1].ToString());
            //            total = decimal.Parse(baseResult.Rows[0][2].ToString());
            //            fecha = DateTime.Parse(baseResult.Rows[0][3].ToString());
            //            cajaLetra = baseResult.Rows[0][4].ToString();

            //            //mes
            //            //año
            //            //caja

            //            resultNotasDePagoPorCaja.Rows.Add(1, 1, 1, subtotal, iva,
            //                total, 1, fecha, caja, cajaLetra, month, year, database.Substring(9), 1);

            //        }



            //    }


            //}




        }





        #endregion



    }
}

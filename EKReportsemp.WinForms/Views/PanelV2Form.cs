

namespace EKReportsemp.WinForms.Views
{

    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Linq;
    using System.Windows.Forms;
    using ClosedXML.Excel;
    using DevComponents.DotNetBar;

    using EKReportsemp.WinForms.Classes;
    using EKReportsemp.WinForms.Models;

    public partial class PanelV2Form : Office2007Form
    {
        private BuscarLocalidad buscarLocalidad;
        private DataTable resultMisCajas;
        private ResultadosOperacion resultadosOperacion;


        public DataTable Empresa;
        public DataTable selcajas;
        private DataTable resumen;
        private DataTable resultadoReporte;
        private int unifica;
        private string conn;
        private int porSemana;


        private string seleccion;

        public PanelV2Form(DataTable result)
        {
            InitializeComponent();
            buscarLocalidad = new BuscarLocalidad();
            resultadosOperacion = new ResultadosOperacion();
            resultadoReporte = new DataTable();


            Empresa = new DataTable();
            Empresa = result;

            date1.Value = DateTime.Now;
            date2.Value = DateTime.Now;


            selcajas = new DataTable();
            selcajas.Columns.Add("caja");
            selcajas.Columns.Add("bd");
            selcajas.Columns.Add("nom");
            selcajas.Columns.Add("empresa");
            selcajas.Columns.Add("marca");

        }

        private void PanelV2Form_Load(object sender, EventArgs e)
        {
            int i = 0;
            int loc = 0;


            foreach (DataRow item in Empresa.Rows)
            {

                treeView1.CheckBoxes = true;

                TreeNode raiz = new TreeNode();
                raiz.Text = item[0].ToString();
                raiz.ImageIndex = 0;

                // raiz.Nodes.Add(new TreeNode());
                treeView1.Nodes.Add(raiz);

                //treeView1.Nodes.Add(item[0].ToString());



                //Le agrego sus sucursales
                DataTable listaSucursales = new DataTable();
                listaSucursales = buscarLocalidad.BuscarSucursales(item[0].ToString());

                foreach (DataRow suc in listaSucursales.Rows)
                {


                    TreeNode raiza = new TreeNode();
                    raiza.Text = suc[0].ToString();
                    raiza.ImageIndex = 1;

                    //raiza.Nodes.Add(new TreeNode());
                    treeView1.Nodes[i].Nodes.Add(raiza);

                    // treeView1.Nodes[i].Nodes.Add(suc[0].ToString());





                    DataTable listaCajas = new DataTable();
                    listaCajas = buscarLocalidad.BuscarCajasA(suc[1].ToString());



                    //ahora cargamos las cajas
                    foreach (DataRow cajas in listaCajas.Rows)
                    {

                        TreeNode raizb = new TreeNode();
                        raizb.Text = cajas[0].ToString();
                        raizb.ImageIndex = 2;

                        //  raizb.Nodes.Add(new TreeNode());
                        treeView1.Nodes[i].Nodes[loc].Nodes.Add(raizb);

                        //treeView1.Nodes[i].Nodes[loc].Nodes.Add(cajas[0].ToString());

                    }

                    loc = loc + 1;



                }
                loc = 0;
                i = i + 1;

            }

        }

        private void checkBoxX1_CheckedChanged(object sender, EventArgs e)
        {
            TreeNodeCollection nodes = treeView1.Nodes;


            if (checkBoxX1.Checked == true)
            {
                foreach (TreeNode item in treeView1.Nodes)
                {
                    item.Checked = true;
                }

                foreach (TreeNode n in nodes)
                {

                    RecorrerNodosMarcar(n);
                }
            }
            else
            {
                foreach (TreeNode item in treeView1.Nodes)
                {
                    item.Checked = false;
                }

                foreach (TreeNode n in nodes)
                {

                    RecorrerNodosDesMarcar(n);
                }
            }




        }


        private void RecorrerNodosMarcar(TreeNode treeNode)
        {




            try
            {
                //Si el nodo que recibimos tiene hijos se recorrerá
                //para luego verificar si esta o no checado
                foreach (TreeNode tn in treeNode.Nodes)
                {
                    tn.Checked = true;

                    RecorrerNodosMarcar(tn);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        private void RecorrerNodosDesMarcar(TreeNode treeNode)
        {



            try
            {
                //Si el nodo que recibimos tiene hijos se recorrerá
                //para luego verificar si esta o no checado
                foreach (TreeNode tn in treeNode.Nodes)
                {
                    tn.Checked = false;

                    RecorrerNodosDesMarcar(tn);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        //Para marcar nodos Seleccionados dentro del nodo
        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            bool enProceso = false;

            if (enProceso) return;
            enProceso = true;

            try
            {
                MarcarNodos(e.Node, e.Node.Checked);
            }
            finally
            {
                enProceso = false;
            }
        }


        private void MarcarNodos(TreeNode nodo, bool marca)
        {
            foreach (TreeNode nodoHijo in nodo.Nodes)
            {
                nodoHijo.Checked = marca;
                MarcarNodos(nodoHijo, marca);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {

            this.Close();
        }







        private void btnInteres_Click(object sender, EventArgs e)
        {
            seleccion = "NotasDePago";
            CajasSeleccionadas();
            Verificacion();
        }

        private void Verificacion()
        {



            //Comprobar si escogieron un rango de fechas coherente

            if (date1.Value > date2.Value)
            {
                MessageBoxEx.EnableGlass = false;
                MessageBoxEx.Show("El rango de fechas no es coherente verifique por favor",
                    "EKReport SEMP",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                date1.Focus();
                return;
            }


            if (selcajas.Rows.Count == 0)
            {
                MessageBoxEx.EnableGlass = false;
                MessageBoxEx.Show("No has Seleccionado ninguna caja, seleeciona por favor",
                    "EKReport SEMP",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                treeView1.Focus();
                return;
            }


            //Reviso si es Remisiones y veo si hay valores validos
            //if (cmbTipo.Text == "Remisiones")
            //{
            //    if (String.IsNullOrEmpty(txtIva.Text) || String.IsNullOrEmpty(txtPorcentaje.Text))
            //    {
            //        MessageBoxEx.EnableGlass = false;
            //        MessageBoxEx.Show("Ingrese valor de Iva y Porcentaje de Remision por favor",
            //            "EKReport SEMP",
            //            MessageBoxButtons.OK,
            //            MessageBoxIcon.Exclamation);
            //        txtIva.Focus();
            //        return;


            //    }

            //    iva = decimal.Parse(txtIva.Text);
            //    parte = decimal.Parse(txtPorcentaje.Text);

            //    if (iva >= 1)
            //    {
            //        MessageBoxEx.EnableGlass = false;
            //        MessageBoxEx.Show("El valor del Iva debe ser menor a 1 \n " +
            //            "(Por Ejemplo.25,.16,.15) Corrija por favor",
            //            "EKReport SEMP",
            //            MessageBoxButtons.OK,
            //            MessageBoxIcon.Exclamation);
            //        txtIva.Focus();
            //        return;


            //    }

            //    if (parte > 100)
            //    {
            //        MessageBoxEx.EnableGlass = false;
            //        MessageBoxEx.Show("El valor del % de la venta debe ser menor o igual a 100 \n " +
            //            " (Por Ejemplo: 1.0 , 2.5 , 10,20) Corrija por favor",
            //            "EKReport SEMP",
            //            MessageBoxButtons.OK,
            //            MessageBoxIcon.Exclamation);
            //        txtPorcentaje.Focus();
            //        return;


            //    }


            //}




            //tipoReporte = cmbTipo.Text;

            unifica = (checkSemana.Checked == true) ? 1 : 2;
            // porSemana = (checkRangosSemana.Checked == true) ? 1 : 2;


            //fechaInicio = dateTimeInput1.Value;
            //fechaFinal = dateTimeInput2.Value;

            //circularProgress1.IsRunning = true;
            //circularProgress1.Visible = true;
            //btnReporte.Enabled = false;
            ////RECORREMOS LA CAJAS Y VEMOS CUALES ESTAN SELECCIONADAS
            //string seleccionadaBD = "";
            //cajasSeleccionadas.Clear();
            //foreach (DataGridViewRow r in cajasGrid.Rows)
            //{
            //    Boolean sel = Convert.ToBoolean(r.Cells[0].Value);
            //    string bd = r.Cells[3].Value.ToString();

            //    if (sel == true)
            //    {



            //        if (unifica == 1)
            //        {

            //            if (seleccionadaBD != bd)
            //            {
            //                cajasSeleccionadas.Rows.Add(r.Cells[1].Value.ToString(), r.Cells[2].Value.ToString(),
            //                    r.Cells[3].Value.ToString(), r.Cells[4].Value.ToString(), r.Cells[5].Value.ToString());
            //            }

            //            seleccionadaBD = bd;



            //        }
            //        else
            //        {
            //            cajasSeleccionadas.Rows.Add(r.Cells[1].Value.ToString(), r.Cells[2].Value.ToString(),
            //                r.Cells[3].Value.ToString(), r.Cells[4].Value.ToString(), r.Cells[5].Value.ToString());
            //        }




            //    }
            //}


            //backgroundWorker1.RunWorkerAsync();
            if (seleccion == "Prestamo")
            {
                AcumuladoTodosPrestamo(date1.Value, date2.Value, "TODOS");
            }



            if (seleccion == "NotasDePago")
            {
                GenricoNotasdePago(date1.Value, date2.Value);
            }






        }


        private void CajasSeleccionadas()
        {
            //VAMOS A RECORRER EL TREE VIEW Y VAMOS A CACHAR LAS CAJAS, SE BUSCA A QUE BASE DE DATOS PERTENECE TAMBIEN Y SUCURSAL ESO
            //DEBERA ESTAR ALMACENADO EN UN DATATABLE PARA RECORRER Y ASI OBTENER LOS RESULTADOS


            DataTable result = new DataTable();
            selcajas.Clear();
            TreeNodeCollection nodes = treeView1.Nodes;

            //Aqui recorres todos los nodos
            int nodosPrincipales = nodes.Count;
            int nodosSecundarios = 0;
            int nodoTerciario = 0;
            string cajaLeyenda;
            for (int i = 0; i < nodosPrincipales; i++)
            {
                nodosSecundarios = nodes[i].GetNodeCount(false);

                for (int j = 0; j < nodosSecundarios; j++)
                {
                    nodoTerciario = nodes[i].Nodes[j].GetNodeCount(false);

                    for (int k = 0; k < nodoTerciario; k++)
                    {
                        cajaLeyenda = nodes[i].Nodes[j].Nodes[k].Text;
                        if (nodes[i].Nodes[j].Nodes[k].Checked == true)
                        {
                            //MessageBox.Show(cajaLeyenda);
                            //Lo que voy hacer es agregar las cajas como en el ejercicio anterior para que ejecute la misma tarea de recorrido;
                            result.Clear();
                            result = buscarLocalidad.DatosCaja(cajaLeyenda);

                            selcajas.Rows.Add(cajaLeyenda, result.Rows[0][2].ToString()
                                , result.Rows[0][3].ToString()
                                , result.Rows[0][4].ToString()
                                , result.Rows[0][5].ToString());
                        }

                    }



                }


            }

            dataGridView1.DataSource = selcajas;//Esto essolo para probar despues eliminar


        }



        private void BeginReport(DateTime fechaInicio, DateTime fechaFinal)
        {
            #region MyRegion
            conn = ConfigurationManager.ConnectionStrings["SEMP2013_CNX"].ConnectionString;


            resumen = new DataTable();

            DataTable DISEÑO = new DataTable();
            DISEÑO.Columns.Add("Fecha");
            DISEÑO.Columns.Add("Mes");
            DISEÑO.Columns.Add("Año");

            string a, b = "";//para unifica
            string c, d = "";//para las cajas
            string f, g = "";//para las cajas
            int distintas = 0;
            int columna_dato = 0;
            DateTime inicio;
            inicio = fechaInicio;
            // Difference in days, hours, and minutes.
            TimeSpan ts = fechaFinal - fechaInicio;


            // Difference in days.
            int totalDias = ts.Days;

            //lleno mi tabla con la fecha y el año
            while (inicio <= fechaFinal)
            {

                DISEÑO.Rows.Add(inicio, inicio.Month.ToString(), inicio.Year.ToString());

                int domingo = (int)inicio.DayOfWeek;

                if (domingo == 0)
                {
                    DISEÑO.Rows.Add("TOTAL", inicio.Month.ToString(), inicio.Year.ToString());


                }


                inicio = inicio.AddDays(1);


            }
            DISEÑO.Rows.Add("TOTAL", inicio.Month.ToString(), inicio.Year.ToString());//ULTIMO DIA DEL MES
            DISEÑO.Rows.Add("TOTAL FINAL", inicio.Month.ToString(), inicio.Year.ToString());//TOTABILIZAR TODO EL MES




            if (seleccion == "Prestamo")
            {



                resumen.Clear();

                foreach (DataRow item in selcajas.Rows)
                {
                    string caja, database, emp, bd, localidad, cajaletra, marca;

                    caja = item[2].ToString();//TLX11
                    cajaletra = item[0].ToString();//CIS-TLX1-1
                    database = item[1].ToString();
                    bd = item[1].ToString();//SEMP2013_TLXX

                    emp = item[3].ToString();//COMERCIAL INTERMODAL
                    marca = item[4].ToString();//MONTE SOL

                    localidad = item[1].ToString().Substring(9);//TLA_3



                    if (radioTodos.Checked == true)//UNIFICAR LOS RESULTADOS POR EMPRESA valor en clase 1
                    {

                        a = item[3].ToString();//COMERCIAL INTERMODAL

                        if (a != b)
                        {
                            b = item[3].ToString();
                            //Y agregamos nuestra primera informacion de sucursal
                            DISEÑO.Columns.Add("Prestamos_" + a + "");//.Substring(9)

                            distintas += 1;

                            if (distintas == 1)
                            {
                                columna_dato += 3;
                            }
                            else
                            {
                                columna_dato += 1;
                            }


                            resultadoReporte.Clear();

                            resultadoReporte = resultadosOperacion.PrestamosXdiaResumen(1, 2018, bd, 1,
                                                fechaInicio, fechaFinal, conn, unifica, porSemana, caja, emp, localidad, cajaletra, 1);


                            int i = 0;
                            foreach (DataRow items in resultadoReporte.Rows)
                            {

                                DISEÑO.Rows[i][columna_dato] = items[9].ToString();
                                DISEÑO.AcceptChanges();
                                i = i + 1;
                            }
                            //POR SI NO OPERO EN TODO EL PERIODO 
                            if (resultadoReporte.Rows.Count == 0)
                            {
                                for (int z = 0; z < DISEÑO.Rows.Count; z++)//totalDias
                                {
                                    DISEÑO.Rows[z][columna_dato] = "0.00";
                                    DISEÑO.AcceptChanges();

                                }

                            }


                        }




                    }

                    if (radioCaja.Checked == true)
                    {


                    }

                    if (radioSucursal.Checked == true)
                    {


                    }

                    if (radioEmpresa.Checked == true)
                    {


                    }
                }

                dataGridView1.DataSource = DISEÑO;


                var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Prestamos");


                worksheet.Cell("B2").Value = "REPORTE DE OPERACIONES DE PRESTAMOS";
                worksheet.Cell("B3").Value = "TIPO DE REPORTE: ACUMULADO TODOS";
                worksheet.Cell("B4").Value = "DEL " + string.Format(date1.Value.ToString("dddd dd {0} MMMM {1} yyyy"), "de", "del") +
                                                    " AL " + string.Format(date2.Value.ToString("dddd dd {0} MMMM {1} yyyy"), "de", "del");


                //MIS ENCABEZADOS//

                int p = 2;
                for (int i = 0; i < DISEÑO.Columns.Count; i++)
                {
                    string nombrecolumna = DISEÑO.Columns[i].ColumnName;
                    worksheet.Cell(6, p).Value = nombrecolumna;
                    worksheet.Cell(6, p).Style.Fill.SetBackgroundColor(XLColor.Navy);
                    worksheet.Cell(6, p).Style.Font.FontColor = XLColor.White;
                    worksheet.Cell(6, p).Style.Font.Bold = true;
                    worksheet.Cell(6, p).Style.Font.FontSize = 16;
                    worksheet.Cell(6, p).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Cell(6, p).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    worksheet.Cell(6, p).Style.Border.BottomBorderColor = XLColor.Black;
                    p++;
                }

                //wb.Style.Border.RightBorderColor = XLColor.Red;
                //wb.Style.Border.LeftBorderColor = XLColor.Red;
                //wb.Style.Border.BottomBorderColor = XLColor.Red;
                //recorremos la tabla
                // worksheet.Range(7, 1, 7, 4).Merge().AddToNamed("Titles");
                worksheet.Cell(7, 2).InsertTable(DISEÑO.AsEnumerable());

                worksheet.Columns().AdjustToContents();//auto ajustables

                workbook.SaveAs("c:\\SEMP2013\\excel.xlsx");



                //CREAMOS EL EXCEL

                //        //if (unifica == 1)
                //        //{
                //        //    if (a != b)
                //        //    {
                //        //        b = item[2].ToString();
                //        //        //Y agregamos nuestra primera informacion de sucursal
                //        //        DISEÑO.Columns.Add("Prestamos_" + bd.Substring(9) + "");
                //        //        DISEÑO.Columns.Add("Espacio_" + distintas.ToString() + "");





                //        //        distintas += 1;

                //        //        if (distintas == 1)
                //        //        {
                //        //            columna_dato += 3;
                //        //        }
                //        //        else
                //        //        {
                //        //            columna_dato += 2;
                //        //        }
                //        //    }

                //        //}
                //        //else
                //        //{


                //        //    f = database;


                //        //    if (f == g)
                //        //    {
                //        //        //Y agregamos nuestra primera informacion de sucursal
                //        //        DISEÑO.Columns.Add("" + caja + "");
                //        //        columna_dato += 1;

                //        //    }


                //        //    if (string.IsNullOrEmpty(g))
                //        //    {
                //        //        //Y agregamos nuestra primera informacion de sucursal
                //        //        DISEÑO.Columns.Add("" + caja + "");

                //        //        columna_dato += 3;
                //        //        g = f;
                //        //    }


                //        //    if (f != g)
                //        //    {
                //        //        DISEÑO.Columns.Add("Total_" + g.Substring(9) + "");
                //        //        DISEÑO.Columns.Add("Espacio_" + g.Substring(9) + "");
                //        //        //Y agregamos nuestra primera informacion de sucursal
                //        //        DISEÑO.Columns.Add("" + caja + "");

                //        //        g = f;
                //        //        columna_dato += 3;



                //        //    }






                //        //}

                //        //resultadoReporte.Clear();

                //        //resultadoReporte = resultadosOperacion.PrestamosXdiaResumen(1, 2018, database, 1,
                //        //                    fechaInicio, fechaFinal, conn, unifica, porSemana, caja, emp, localidad, cajaletra);
                //        //////CONSTRUCCION DEL MODELO DE DATATABLE PARA LA PRESENTACION
                //        /////
                //        //int i = 0;
                //        //foreach (DataRow items in resultadoReporte.Rows)
                //        //{
                //        //    DISEÑO.Rows[i][columna_dato] = items[9].ToString();
                //        //    DISEÑO.AcceptChanges();
                //        //    i = i + 1;
                //        //}

                //        //if (resultadoReporte.Rows.Count == 0)
                //        //{
                //        //    for (int z = 0; z < totalDias; z++)
                //        //    {
                //        //        DISEÑO.Rows[z][columna_dato] = "0.00";
                //        //        DISEÑO.AcceptChanges();

                //        //    }

                //        //}



                //    }

                //    //if (unifica != 1)
                //    //{
                //    //    //Para la ultima sucursal se agregan las columnas
                //    //    DISEÑO.Columns.Add("Total_" + g.Substring(9) + "");
                //    //    DISEÑO.Columns.Add("Espacio_" + g.Substring(9) + "");

                //    //}
                //    //resumen = DISEÑO;

                //    //AHORA LA SUMA DE CADA UNO DE LOS BLOQUES


            }

            if (seleccion == "Notas de Pago")
            {


            }

            //if (tipoReporte == "Notas de Pago")
            //{



            //    resumen.Clear();

            //    foreach (DataRow item in cajasSeleccionadas.Rows)
            //    {
            //        string caja, database, emp, bd, localidad, cajaletra;

            //        caja = item[0].ToString();//TLX11
            //        cajaletra = item[1].ToString();//CIS-TLX1-1
            //        database = item[2].ToString();
            //        bd = item[2].ToString();//SEMP2013_TLXX
            //        emp = item[3].ToString();//Monte Ros
            //        localidad = item[4].ToString();//Tula monte ros



            //        c = item[0].ToString();
            //        a = item[2].ToString();

            //        if (unifica == 1)
            //        {
            //            if (a != b)
            //            {
            //                b = item[2].ToString();
            //                //Y agregamos nuestra primera informacion de sucursal
            //                DISEÑO.Columns.Add("SubTotal_" + bd.Substring(9) + "");
            //                DISEÑO.Columns.Add("Iva_" + bd.Substring(9) + "");
            //                DISEÑO.Columns.Add("Total_" + bd.Substring(9) + "");
            //                DISEÑO.Columns.Add("Espacio_" + distintas.ToString() + "");





            //                distintas += 1;

            //                if (distintas == 1)
            //                {
            //                    columna_dato += 3;
            //                }
            //                else
            //                {
            //                    columna_dato += 4;
            //                }
            //            }

            //        }
            //        else
            //        {


            //            f = database;


            //            if (f == g)
            //            {
            //                //Y agregamos nuestra primera informacion de sucursal
            //                DISEÑO.Columns.Add("SubTotal_" + caja + "");
            //                DISEÑO.Columns.Add("IVA_" + caja + "");
            //                DISEÑO.Columns.Add("Total_" + caja + "");
            //                columna_dato += 3;

            //            }


            //            if (string.IsNullOrEmpty(g))
            //            {
            //                //Y agregamos nuestra primera informacion de sucursal
            //                DISEÑO.Columns.Add("SubTotal_" + caja + "");
            //                DISEÑO.Columns.Add("IVA_" + caja + "");
            //                DISEÑO.Columns.Add("Total_" + caja + "");

            //                columna_dato += 3;
            //                g = f;
            //            }


            //            if (f != g)
            //            {
            //                DISEÑO.Columns.Add("Total_" + g.Substring(9) + "");
            //                DISEÑO.Columns.Add("Espacio_" + g.Substring(9) + "");
            //                //Y agregamos nuestra primera informacion de sucursal
            //                DISEÑO.Columns.Add("SubTotal_" + caja + "");
            //                DISEÑO.Columns.Add("IVA_" + caja + "");
            //                DISEÑO.Columns.Add("Total_" + caja + "");

            //                g = f;
            //                columna_dato += 5;



            //            }






            //        }

            //        resultadoReporte.Clear();

            //        resultadoReporte = resultadosOperacion.NotasDePagoXdiaResumen(1, 2018, database, 1, fechaInicio, fechaFinal,
            //               conn, unifica, porSemana, caja, emp, localidad, cajaletra);

            //        ////CONSTRUCCION DEL MODELO DE DATATABLE PARA LA PRESENTACION
            //        ///
            //        int i = 0;
            //        foreach (DataRow items in resultadoReporte.Rows)
            //        {
            //            DISEÑO.Rows[i][columna_dato] = items[9].ToString();
            //            DISEÑO.Rows[i][columna_dato + 1] = items[10].ToString();
            //            DISEÑO.Rows[i][columna_dato + 2] = items[11].ToString();
            //            DISEÑO.AcceptChanges();
            //            i = i + 1;
            //        }

            //        if (resultadoReporte.Rows.Count == 0)
            //        {
            //            for (int z = 0; z < totalDias; z++)
            //            {
            //                DISEÑO.Rows[z][columna_dato] = "0.00";
            //                DISEÑO.Rows[z][columna_dato + 1] = "0.00";
            //                DISEÑO.Rows[z][columna_dato + 2] = "0.00";
            //                DISEÑO.AcceptChanges();

            //            }

            //        }



            //    }

            //    if (unifica != 1)
            //    {
            //        //Para la ultima sucursal se agregan las columnas
            //        DISEÑO.Columns.Add("Total_" + g.Substring(9) + "");
            //        DISEÑO.Columns.Add("Espacio_" + g.Substring(9) + "");

            //    }
            //    resumen = DISEÑO;

            //    //AHORA LA SUMA DE CADA UNO DE LOS BLOQUES





            //    #region MyRegion
            //    //foreach (DataRow item in cajasSeleccionadas.Rows)
            //    //{

            //    //    string caja, database, emp, bd, localidad, cajaletra;
            //    //    caja = item[0].ToString();//TLX11
            //    //    cajaletra = item[1].ToString();//CIS-TLX1-1
            //    //    database = item[2].ToString();
            //    //    bd = item[2].ToString();//SEMP2013_TLXX
            //    //    emp = item[3].ToString();//Monte Ros
            //    //    localidad = item[4].ToString();//Tula monte ros



            //    //    resultadoReporte.Clear();

            //    //    resultadoReporte = resultadosOperacion.NotasDePagoXdiaResumen(1,2018,database,1,fechaInicio,fechaFinal,
            //    //        conn,unifica,porSemana,caja,emp,localidad, cajaletra);


            //    //    foreach (DataRow r in resultadoReporte.Rows)
            //    //    {


            //    //        resumen.Rows.Add(r[0].ToString(), r[1].ToString(), r[2].ToString()
            //    //                        , r[3].ToString(), r[4].ToString(), r[5].ToString(), r[6].ToString(), r[7].ToString()
            //    //                        , r[8].ToString(), r[9].ToString(), r[10].ToString(), r[11].ToString());
            //    //    }




            //    //}


            //    #endregion

            //}


            //if (tipoReporte == "Remisiones")
            //{

            //    resumen.Clear();




            //    foreach (DataRow item in cajasSeleccionadas.Rows)
            //    {
            //        string caja, database, emp, bd, localidad, cajaletra;

            //        caja = item[0].ToString();//TLX11
            //        cajaletra = item[1].ToString();//CIS-TLX1-1
            //        database = item[2].ToString();
            //        bd = item[2].ToString();//SEMP2013_TLXX
            //        emp = item[3].ToString();//Monte Ros
            //        localidad = item[4].ToString();//Tula monte ros



            //        c = item[0].ToString();
            //        a = item[2].ToString();

            //        if (unifica == 1)
            //        {
            //            if (a != b)
            //            {
            //                b = item[2].ToString();
            //                //Y agregamos nuestra primera informacion de sucursal

            //                //Subtotal, ivaTotal, total,
            //                //subtotalParte, ivaParte, totalParte);


            //                DISEÑO.Columns.Add("SubTotal_" + bd.Substring(9) + "");
            //                DISEÑO.Columns.Add("IvaTotal_" + bd.Substring(9) + "");
            //                DISEÑO.Columns.Add("Total_" + bd.Substring(9) + "");

            //                DISEÑO.Columns.Add("SubTotalParte_" + bd.Substring(9) + "");
            //                DISEÑO.Columns.Add("IvaTotalParte_" + bd.Substring(9) + "");
            //                DISEÑO.Columns.Add("TotalParte_" + bd.Substring(9) + "");

            //                DISEÑO.Columns.Add("Espacio_" + distintas.ToString() + "");





            //                distintas += 1;

            //                if (distintas == 1)
            //                {
            //                    columna_dato += 3;
            //                }
            //                else
            //                {
            //                    columna_dato += 7;
            //                }
            //            }

            //        }
            //        else
            //        {


            //            f = database;


            //            if (f == g)
            //            {
            //                //Y agregamos nuestra primera informacion de sucursal
            //                DISEÑO.Columns.Add("SubTotal_" + bd.Substring(9) + "");
            //                DISEÑO.Columns.Add("IvaTotal_" + bd.Substring(9) + "");
            //                DISEÑO.Columns.Add("Total_" + bd.Substring(9) + "");

            //                DISEÑO.Columns.Add("SubTotalParte_" + bd.Substring(9) + "");
            //                DISEÑO.Columns.Add("IvaTotalParte_" + bd.Substring(9) + "");
            //                DISEÑO.Columns.Add("TotalParte_" + bd.Substring(9) + "");

            //                DISEÑO.Columns.Add("Espacio_" + g.Substring(9) + "");

            //                columna_dato += 7;

            //            }


            //            if (string.IsNullOrEmpty(g))
            //            {
            //                //Y agregamos nuestra primera informacion de sucursal
            //                DISEÑO.Columns.Add("SubTotal_" + bd.Substring(9) + "");
            //                DISEÑO.Columns.Add("IvaTotal_" + bd.Substring(9) + "");
            //                DISEÑO.Columns.Add("Total_" + bd.Substring(9) + "");

            //                DISEÑO.Columns.Add("SubTotalParte_" + bd.Substring(9) + "");
            //                DISEÑO.Columns.Add("IvaTotalParte_" + bd.Substring(9) + "");
            //                DISEÑO.Columns.Add("TotalParte_" + bd.Substring(9) + "");

            //                DISEÑO.Columns.Add("Espacio_" + g.Substring(9) + "");


            //                columna_dato += 3;
            //                g = f;
            //            }


            //            if (f != g)
            //            {

            //                //Y agregamos nuestra primera informacion de sucursal
            //                DISEÑO.Columns.Add("SubTotal_" + bd.Substring(9) + "");
            //                DISEÑO.Columns.Add("IvaTotal_" + bd.Substring(9) + "");
            //                DISEÑO.Columns.Add("Total_" + bd.Substring(9) + "");

            //                DISEÑO.Columns.Add("SubTotalParte_" + bd.Substring(9) + "");
            //                DISEÑO.Columns.Add("IvaTotalParte_" + bd.Substring(9) + "");
            //                DISEÑO.Columns.Add("TotalParte_" + bd.Substring(9) + "");

            //                DISEÑO.Columns.Add("Total_" + g.Substring(9) + "");
            //                //DISEÑO.Columns.Add("Espacio_" + g.Substring(9) + "");



            //                g = f;
            //                columna_dato += 7;



            //            }






            //        }

            //        resultadoReporte.Clear();

            //        resultadoReporte = resultadosOperacion.RemisionesXdiaResumen(1, 2018, database, 1,
            //                               fechaInicio, fechaFinal, conn, unifica, porSemana, caja, iva, parte, emp, localidad, cajaletra);


            //        ////CONSTRUCCION DEL MODELO DE DATATABLE PARA LA PRESENTACION
            //        ///
            //        int i = 0;
            //        foreach (DataRow items in resultadoReporte.Rows)
            //        {
            //            DISEÑO.Rows[i][columna_dato] = items[9].ToString();//3
            //            DISEÑO.Rows[i][columna_dato + 1] = items[10].ToString();//4
            //            DISEÑO.Rows[i][columna_dato + 2] = items[11].ToString();//5
            //            DISEÑO.Rows[i][columna_dato + 3] = items[12].ToString();//6
            //            DISEÑO.Rows[i][columna_dato + 4] = items[13].ToString();//7
            //            DISEÑO.Rows[i][columna_dato + 5] = items[14].ToString();//8
            //            DISEÑO.AcceptChanges();
            //            i = i + 1;
            //        }

            //        if (resultadoReporte.Rows.Count == 0)
            //        {
            //            for (int z = 0; z < totalDias; z++)
            //            {
            //                DISEÑO.Rows[z][columna_dato] = "0.00";
            //                DISEÑO.Rows[z][columna_dato + 1] = "0.00";
            //                DISEÑO.Rows[z][columna_dato + 2] = "0.00";
            //                DISEÑO.Rows[z][columna_dato + 3] = "0.00";
            //                DISEÑO.Rows[z][columna_dato + 4] = "0.00";
            //                DISEÑO.Rows[z][columna_dato + 5] = "0.00";
            //                DISEÑO.AcceptChanges();

            //            }

            //        }



            //    }



            //    if (unifica != 1)
            //    {
            //        //Para la ultima sucursal se agregan las columnas
            //        DISEÑO.Columns.Add("Total_" + g.Substring(9) + "");
            //        DISEÑO.Columns.Add("Espacio_" + g.Substring(9) + "");


            //    }
            //    resumen = DISEÑO;






            //    #region MyRegion


            //    //resultRemisionesPorCaja.Rows.Add(fecha, localidad, database.Substring(9), database,
            //    //              fecha.Month.ToString(), fecha.Year.ToString(), cajaletra, caja, emp, Subtotal, ivaTotal, total,
            //    //              subtotalParte, ivaParte, totalParte);


            //    //foreach (DataRow item in cajasSeleccionadas.Rows)
            //    //{

            //    //    string caja, database, emp, bd, localidad, cajaletra;
            //    //    caja = item[0].ToString();//TLX11
            //    //    cajaletra = item[1].ToString();//CIS-TLX1-1
            //    //    database = item[2].ToString();
            //    //    bd = item[2].ToString();//SEMP2013_TLXX
            //    //    emp = item[3].ToString();//Monte Ros
            //    //    localidad = item[4].ToString();//Tula monte ros



            //    //    resultadoReporte.Clear();


            //    //    resultadoReporte.Clear();

            //    //    resultadoReporte = resultadosOperacion.RemisionesXdiaResumen(1, 2018, database, 1,
            //    //                        fechaInicio, fechaFinal, conn, unifica, porSemana, caja, iva, parte, emp, localidad, cajaletra);


            //    //    foreach (DataRow r in resultadoReporte.Rows)
            //    //    {




            //    //        resumen.Rows.Add(r[0].ToString(),
            //    //            r[1].ToString(),
            //    //            r[2].ToString(),
            //    //            r[3].ToString(),
            //    //            r[4].ToString(),
            //    //            r[5].ToString(),
            //    //            r[6].ToString(),
            //    //            r[7].ToString(),
            //    //            r[8].ToString(),
            //    //            decimal.Parse(r[9].ToString()).ToString("N2"),
            //    //            decimal.Parse(r[10].ToString()).ToString("N2"),
            //    //            decimal.Parse(r[11].ToString()).ToString("N2"),
            //    //            decimal.Parse(r[12].ToString()).ToString("N2"),
            //    //            decimal.Parse(r[13].ToString()).ToString("N2"),
            //    //            decimal.Parse(r[14].ToString()).ToString("N2")
            //    //            );
            //    //    }




            //    //} 
            //    #endregion



            //}


            #endregion

        }

        private void AcumuladoTodosPrestamo(DateTime fechaInicio, DateTime fechaFinal, string seleccion)
        {

            conn = ConfigurationManager.ConnectionStrings["SEMP2013_CNX"].ConnectionString;


            resumen = new DataTable();

            DataTable DISEÑO = new DataTable();
            DISEÑO.Columns.Add("Fecha");
            DISEÑO.Columns.Add("Mes");
            DISEÑO.Columns.Add("Año");

            string a, b = "";//para unifica
            string c, d = "";//para las cajas
            string f, g = "";//para las cajas
            int distintas = 0;
            int columna_dato = 0;
            DateTime inicio;
            inicio = fechaInicio;
            // Difference in days, hours, and minutes.
            TimeSpan ts = fechaFinal - fechaInicio;


            // Difference in days.
            int totalDias = ts.Days;

            //lleno mi tabla con la fecha y el año
            while (inicio <= fechaFinal)
            {

                DISEÑO.Rows.Add(string.Format(inicio.ToString("ddd dd {0} MMM {1} yyyy"), "de", "del"),
                                inicio.Month.ToString(), inicio.Year.ToString());

                int domingo = (int)inicio.DayOfWeek;

                if (domingo == 0)
                {
                    DISEÑO.Rows.Add("TOTAL", inicio.Month.ToString(), inicio.Year.ToString());


                }


                inicio = inicio.AddDays(1);


            }
            DISEÑO.Rows.Add("TOTAL", inicio.Month.ToString(), inicio.Year.ToString());//ULTIMO DIA DEL MES
            DISEÑO.Rows.Add("TOTAL FINAL", inicio.Month.ToString(), inicio.Year.ToString());//TOTABILIZAR TODO EL MES


            /*0 Cero el datatable contenedor de resultado de prestamos y la variable */
            DataTable resultadoPrestamos = new DataTable();
            /*1 primero un var que contenga las lista de las diferentes empresas*/
            List<seleccionReporte> seleccionReporteList = new List<seleccionReporte>();
            foreach (DataRow row in selcajas.Rows)
            {
                seleccionReporteList.Add(new seleccionReporte
                {

                    caja = row[0].ToString(),
                    bd = row[1].ToString(),
                    nom = row[2].ToString(),
                    empresa = row[3].ToString(),
                    marca = row[4].ToString(),

                });
            }


            if (radioTodos.Checked == true)
            {
                var listaEmpresas = seleccionReporteList.Select(cual => cual.empresa).
                    Distinct().ToList();


                //var listaEmpresas = buscarLocalidad.empresas();

                /*2 segundo recorremos una por una y de ahi ...*/
                foreach (var empresa in listaEmpresas)
                {
                    var listaSucursales = buscarLocalidad.listaSucursalesEmpresa(empresa);
                    /*3 tercero ahora si recorro cada una de las sucursales de la empresa seleccionada*/
                    resultadoPrestamos.Clear();


                    //Y agregamos nuestra primera informacion de sucursal
                    distintas += 1;
                    if (distintas == 1)
                    {
                        columna_dato += 3;
                    }
                    else
                    {
                        columna_dato += 1;
                    }
                    DISEÑO.Columns.Add("" + empresa + "", Type.GetType("System.Decimal"));//.Substring(9)
                                                                                          //DISEÑO.Columns[columna_dato].DefaultValue = 0;

                    foreach (var sucursal in listaSucursales)
                    {




                        resultadoReporte.Clear();

                        resultadoReporte = resultadosOperacion.PrestamosXdiaResumen(1, 2018, sucursal.sucursalBD, 1,
                                            fechaInicio, fechaFinal, conn, unifica, porSemana, "", "", "", "", 1);

                        //POR SI NO OPERO EN TODO EL PERIODO 
                        if (resultadoReporte.Rows.Count > 0)
                        {

                            int i = 0;
                            foreach (DataRow items in resultadoReporte.Rows)
                            {

                                string x = "0", y = "0";
                                decimal z = 0;
                                x = DISEÑO.Rows[i][columna_dato].ToString();
                                y = items[9].ToString();
                                if (string.IsNullOrEmpty(x))
                                {
                                    x = "0";
                                }

                                z = Convert.ToDecimal(x.ToString()) + Convert.ToDecimal(y.ToString());

                                DISEÑO.Rows[i][columna_dato] = decimal.Parse(z.ToString()).ToString("N2");

                                DISEÑO.AcceptChanges();
                                i = i + 1;
                            }


                        }







                    }




                }

                /*ahora obetenemos el conteo del numero de mrpesa que son*/
                int cantidadEmpresas = listaEmpresas.Count;


            }









            ///*AHORA EL EXCEL*/

            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("PrestamosGlobales");
            worksheet.PageSetup.PageOrientation = XLPageOrientation.Landscape;
            worksheet.PageSetup.FitToPages(1, 1);
            //ESTA EN PULGADAS
            worksheet.PageSetup.Margins.Top = 1;
            worksheet.PageSetup.Margins.Bottom = 1;
            worksheet.PageSetup.Margins.Left = 1;
            worksheet.PageSetup.Margins.Right = 1;
            worksheet.PageSetup.CenterHorizontally = true;
            worksheet.PageSetup.CenterVertically = true;


            worksheet.PageSetup.Header.Left.AddText("TIPO DE REPORTE: ACUMULADO TODOS");
            worksheet.PageSetup.Header.Center.AddText("TIPO DE REPORTE: ACUMULADO TODOS");

            worksheet.PageSetup.Header.Right.AddText("DEL " + string.Format(date1.Value.ToString("dddd dd {0} MMMM {1} yyyy"), "de", "del"));
            worksheet.PageSetup.Header.Right.AddNewLine();
            worksheet.PageSetup.Header.Right.AddText(" AL " + string.Format(date2.Value.ToString("dddd dd {0} MMMM {1} yyyy"), "de", "del"));

            worksheet.PageSetup.Footer.Center.AddText(XLHFPredefinedText.PageNumber, XLHFOccurrence.AllPages);
            worksheet.PageSetup.Footer.Center.AddText(" / ", XLHFOccurrence.AllPages);
            worksheet.PageSetup.Footer.Center.AddText(XLHFPredefinedText.NumberOfPages, XLHFOccurrence.AllPages);


            //worksheet.Cell("B2").Value = "REPORTE DE OPERACIONES DE PRESTAMOS";
            //worksheet.Cell("B3").Value = "TIPO DE REPORTE 'ACUMULADO TODOS'";
            //worksheet.Cell("B4").Value = "DEL " + string.Format(date1.Value.ToString("dddd dd {0} MMMM {1} yyyy"), "de", "del") +
            //                                   " AL " + string.Format(date2.Value.ToString("dddd dd {0} MMMM {1} yyyy"), "de", "del");

            //worksheet.Range("B2:H2").Merge();
            //worksheet.Range("B3:H3").Merge();
            //worksheet.Range("B4:H4").Merge();


            //MIS ENCABEZADOS//

            int p = 2;
            for (int i = 0; i < DISEÑO.Columns.Count; i++)
            {
                string nombrecolumna = DISEÑO.Columns[i].ColumnName;
                if (nombrecolumna.Length > 10)
                {
                    nombrecolumna = nombrecolumna.Substring(0, 10);
                }

                worksheet.Cell(6, p).Value = nombrecolumna;
                worksheet.Cell(6, p).Style.Fill.SetBackgroundColor(XLColor.Navy);
                worksheet.Cell(6, p).Style.Font.FontColor = XLColor.White;
                worksheet.Cell(6, p).Style.Font.Bold = true;
                worksheet.Cell(6, p).Style.Font.FontSize = 16;
                worksheet.Cell(6, p).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell(6, p).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                worksheet.Cell(6, p).Style.Border.BottomBorderColor = XLColor.Black;
                p++;
            }



            int fila = 7;
            int columna = 2;
            int estaEnLineaEspecial = 0;
            decimal suma_derecho = 0;
            for (int i = 0; i < DISEÑO.Rows.Count; i++)
            {

                for (int y = 0; y < DISEÑO.Columns.Count; y++) //foreach (DataColumn item in DISEÑO.Columns)
                {
                    string campo;
                    campo = DISEÑO.Rows[i][y].ToString();

                    if (campo.Equals("TOTAL") || campo.Equals("TOTAL FINAL"))
                    {
                        estaEnLineaEspecial = 1;
                    }

                    if (estaEnLineaEspecial == 1)
                    {



                        if (y > 2)
                        {

                            worksheet.Cell(fila, columna).Style.NumberFormat.Format = "#,##0.00";
                            worksheet.Cell(fila, columna).DataType = XLDataType.Number;
                            worksheet.Cell(fila, columna).Value = decimal.Parse(campo);



                        }
                        else
                        {
                            worksheet.Cell(fila, columna).Value = campo;
                        }

                        worksheet.Cell(fila, columna).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
                        worksheet.Cell(fila, columna).Style.Border.TopBorder = XLBorderStyleValues.Thick;
                        worksheet.Cell(fila, columna).Style.Border.BottomBorderColor = XLColor.Black;
                        worksheet.Cell(fila, columna).Style.Border.TopBorderColor = XLColor.Black;

                        worksheet.Cell(fila, columna).Style.Font.FontColor = XLColor.Black;
                        worksheet.Cell(fila, columna).Style.Font.Bold = true;
                        worksheet.Cell(fila, columna).Style.Font.FontSize = 16;

                        estaEnLineaEspecial = 1;
                    }
                    else
                    {
                        if (y > 2)
                        {

                            worksheet.Cell(fila, columna).Style.NumberFormat.Format = "#,##0.00";
                            worksheet.Cell(fila, columna).DataType = XLDataType.Number;
                            worksheet.Cell(fila, columna).Value = decimal.Parse(campo);

                        }
                        else
                        {
                            worksheet.Cell(fila, columna).Value = campo;
                        }



                        worksheet.Cell(fila, columna).Style.Font.FontColor = XLColor.Black;

                    }

                    columna += 1;

                }
                estaEnLineaEspecial = 0;
                columna = 2;
                fila += 1;


            }
            /*luego sumamos todo a partir de la ultima fila*/
            columna = DISEÑO.Columns.Count + 2;
            fila = 7;
            //el Header

            worksheet.Cell(6, columna).Value = "Total";
            worksheet.Cell(6, columna).Style.Fill.SetBackgroundColor(XLColor.Navy);
            worksheet.Cell(6, columna).Style.Font.FontColor = XLColor.White;
            worksheet.Cell(6, columna).Style.Font.Bold = true;
            worksheet.Cell(6, columna).Style.Font.FontSize = 16;
            worksheet.Cell(6, columna).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Cell(6, columna).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            worksheet.Cell(6, columna).Style.Border.BottomBorderColor = XLColor.Black;
            int IsColumnTotal = 0;
            for (int i = 0; i < DISEÑO.Rows.Count; i++)
            {
                for (int y = 3; y < DISEÑO.Columns.Count; y++) //foreach (DataColumn item in DISEÑO.Columns)
                {
                    string campo;


                    if (DISEÑO.Rows[i][0].ToString() == "TOTAL" ||
                        DISEÑO.Rows[i][0].ToString() == "TOTAL FINAL")
                    {
                        IsColumnTotal = 1;

                    }

                    campo = DISEÑO.Rows[i][y].ToString();

                    suma_derecho += decimal.Parse(campo);


                }

                if (IsColumnTotal == 1)
                {
                    worksheet.Cell(fila, columna).Style.NumberFormat.Format = "#,##0.00";
                    worksheet.Cell(fila, columna).DataType = XLDataType.Number;
                    worksheet.Cell(fila, columna).Value = suma_derecho;

                    worksheet.Cell(fila, columna).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
                    worksheet.Cell(fila, columna).Style.Border.TopBorder = XLBorderStyleValues.Thick;
                    worksheet.Cell(fila, columna).Style.Border.BottomBorderColor = XLColor.Black;
                    worksheet.Cell(fila, columna).Style.Border.TopBorderColor = XLColor.Black;

                    worksheet.Cell(fila, columna).Style.Font.FontColor = XLColor.Black;
                    worksheet.Cell(fila, columna).Style.Font.Bold = true;
                    worksheet.Cell(fila, columna).Style.Font.FontSize = 16;
                    IsColumnTotal = 0;
                }
                else
                {
                    worksheet.Cell(fila, columna).Style.NumberFormat.Format = "#,##0.00";
                    worksheet.Cell(fila, columna).DataType = XLDataType.Number;
                    worksheet.Cell(fila, columna).Value = suma_derecho;
                }


                fila += 1;
                suma_derecho = 0;
            }

            worksheet.Columns().AdjustToContents();//auto ajustables

            workbook.SaveAs("c:\\SEMP2013\\excel.xlsx");


            ///*mostramos el resultado*/
            //dataGridView1.DataSource = DISEÑO;
        }





        //PARA EMPRESAS Y SUCURSALES SOLAMENTE
        private void GenricoNotasdePago(DateTime fechaInicio, DateTime fechaFinal)
        {

            conn = ConfigurationManager.ConnectionStrings["SEMP2013_CNX"].ConnectionString;


            resumen = new DataTable();

            DataTable DISEÑO = new DataTable();
            DISEÑO.Columns.Add("Fecha");
            DISEÑO.Columns.Add("Mes");
            DISEÑO.Columns.Add("Año");

            string a, b = "";//para unifica
            string c, d = "";//para las cajas
            string f, g = "";//para las cajas
            int distintas = 0;
            int columna_dato = 0;
            DateTime inicio;
            inicio = fechaInicio;
            // Difference in days, hours, and minutes.
            TimeSpan ts = fechaFinal - fechaInicio;


            // Difference in days.
            int totalDias = ts.Days;

            //lleno mi tabla con la fecha y el año
            while (inicio <= fechaFinal)
            {

                DISEÑO.Rows.Add(string.Format(inicio.ToString("ddd dd {0} MMM {1} yyyy"), "de", "del"),
                                inicio.Month.ToString(), inicio.Year.ToString());

                int domingo = (int)inicio.DayOfWeek;

                if (domingo == 0)
                {
                    DISEÑO.Rows.Add("TOTAL", inicio.Month.ToString(), inicio.Year.ToString());


                }


                inicio = inicio.AddDays(1);


            }
            DISEÑO.Rows.Add("TOTAL", inicio.Month.ToString(), inicio.Year.ToString());//ULTIMO DIA DEL MES
            DISEÑO.Rows.Add("TOTAL FINAL", inicio.Month.ToString(), inicio.Year.ToString());//TOTABILIZAR TODO EL MES


            /*0 Cero el datatable contenedor de resultado de prestamos y la variable */
            DataTable resultadoNotasDePago = new DataTable();

            /*1 saber que opcion escojio*/


            /*acumulado 
             * TODO(es decir sin importar la empresa se suma TODO
             * Empresa(los totales de las cajas por empresa)
             * POR SUCURSAL(LOS TOTALES DE LAS CAJAS POR SUCURSAL
             
             pero haciendo enfasis de todo lo que se selecciono*/

            /*1 primero un var que contenga las lista la tabla*/
            List<seleccionReporte> seleccionReporteList = new List<seleccionReporte>();
            foreach (DataRow row in selcajas.Rows)
            {
                seleccionReporteList.Add(new seleccionReporte
                {

                    caja = row[0].ToString(),
                    bd = row[1].ToString(),
                    nom = row[2].ToString(),
                    empresa = row[3].ToString(),
                    marca = row[4].ToString(),

                });
            }



            if (radioTodos.Checked == true)
            {
                var listaAOperar = seleccionReporteList.Select(pr => pr.bd).Distinct().ToList();

                resultadoNotasDePago.Clear();

                DISEÑO.Columns.Add("SubTotal", Type.GetType("System.Decimal"));//.Substring(9)
                DISEÑO.Columns.Add("Iva", Type.GetType("System.Decimal"));//.Substring(9)
                DISEÑO.Columns.Add("Total", Type.GetType("System.Decimal"));//.Substring(9)

                /*COMO ES TODO VAMOS A SUMAR TODAS LAS SUCURSALES SIN IMPORTAR LA MARCA*/
                foreach (var sucursal in listaAOperar)
                {




                    resultadoReporte.Clear();

                    resultadoReporte = resultadosOperacion.NotasDePagoXdiaResumen(1, 2018, sucursal, 1,
                                        fechaInicio, fechaFinal, conn, 1, porSemana, "", "", "", "", 1);

                    //AHORA SUMO TODO LO QUE ENCUENTRE POR SUCURSAL-EMPRESA
                    if (resultadoReporte.Rows.Count > 0)
                    {

                        int i = 0;
                        foreach (DataRow items in resultadoReporte.Rows)
                        {

                            string x = "0", y = "0", z = "0";
                            string u, v, w = "0";
                            decimal subtotal, iva, total = 0;


                            x = items[9].ToString();//subtotal
                            y = items[10].ToString();//iva
                            z = items[11].ToString();//total


                            u = DISEÑO.Rows[i][3].ToString();
                            v = DISEÑO.Rows[i][4].ToString();
                            w = DISEÑO.Rows[i][5].ToString();



                            if (string.IsNullOrEmpty(u))
                            {
                                u = "0";
                            }
                            if (string.IsNullOrEmpty(v))
                            {
                                v = "0";
                            }
                            if (string.IsNullOrEmpty(w))
                            {
                                w = "0";
                            }


                            subtotal = Convert.ToDecimal(u.ToString()) + Convert.ToDecimal(x.ToString());

                            iva = Convert.ToDecimal(v.ToString()) + Convert.ToDecimal(y.ToString());

                            total = Convert.ToDecimal(w.ToString()) + Convert.ToDecimal(z.ToString());



                            DISEÑO.Rows[i][3] = decimal.Parse(subtotal.ToString()).ToString("N2");
                            DISEÑO.Rows[i][4] = decimal.Parse(iva.ToString()).ToString("N2");
                            DISEÑO.Rows[i][5] = decimal.Parse(total.ToString()).ToString("N2");

                            DISEÑO.AcceptChanges();
                            i = i + 1;
                        }


                    }


                }




                ///*AHORA EL EXCEL*/

                var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("NotasPagoGlobales");
                worksheet.PageSetup.PageOrientation = XLPageOrientation.Landscape;
                worksheet.PageSetup.FitToPages(1, 1);
                //EN PULGADAS
                worksheet.PageSetup.Margins.Top = 1;
                worksheet.PageSetup.Margins.Bottom = 1;
                worksheet.PageSetup.Margins.Left = 1;
                worksheet.PageSetup.Margins.Right = 1;
                worksheet.PageSetup.CenterHorizontally = true;
                worksheet.PageSetup.CenterVertically = true;


                var listaInfo = seleccionReporteList.Select(li => li.empresa).Distinct().ToList();

                worksheet.PageSetup.Header.Left.AddText("Empresas Calculadas");
                worksheet.PageSetup.Header.Left.AddNewLine();
                foreach (var item in listaInfo)
                {
                    worksheet.PageSetup.Header.Left.AddText(item.Substring(0, 9));
                    worksheet.PageSetup.Header.Left.AddNewLine();

                }


                worksheet.PageSetup.Header.Center.AddText("TIPO DE REPORTE: INTERESES GLOBALES");
                worksheet.PageSetup.Header.Center.AddNewLine();
                worksheet.PageSetup.Header.Center.AddText("del " + string.Format(date1.Value.ToString("dddd dd {0} MMMM {1} yyyy"), "de", "del"));
                worksheet.PageSetup.Header.Center.AddNewLine();
                worksheet.PageSetup.Header.Center.AddText(" al " + string.Format(date2.Value.ToString("dddd dd {0} MMMM {1} yyyy"), "de", "del"));

                worksheet.PageSetup.Footer.Center.AddText(XLHFPredefinedText.PageNumber, XLHFOccurrence.AllPages);
                worksheet.PageSetup.Footer.Center.AddText(" / ", XLHFOccurrence.AllPages);
                worksheet.PageSetup.Footer.Center.AddText(XLHFPredefinedText.NumberOfPages, XLHFOccurrence.AllPages);


                var listaInfo2 = seleccionReporteList.Select(li => li.bd).Distinct().ToList();

                worksheet.PageSetup.Header.Right.AddText("Sucursales Calculadas");
                worksheet.PageSetup.Header.Right.AddNewLine();
                foreach (var item in listaInfo2)
                {
                    worksheet.PageSetup.Header.Right.AddText(item.Substring(9, item.Length - 9));
                    worksheet.PageSetup.Header.Right.AddNewLine();

                }


                //MIS ENCABEZADOS//

                int p = 2;
                for (int i = 0; i < DISEÑO.Columns.Count; i++)
                {
                    string nombrecolumna = DISEÑO.Columns[i].ColumnName;
                    if (nombrecolumna.Length > 10)
                    {
                        nombrecolumna = nombrecolumna.Substring(0, 10);
                    }

                    worksheet.Cell(3, p).Value = nombrecolumna;
                    worksheet.Cell(3, p).Style.Fill.SetBackgroundColor(XLColor.Navy);
                    worksheet.Cell(3, p).Style.Font.FontColor = XLColor.White;
                    worksheet.Cell(3, p).Style.Font.Bold = true;
                    worksheet.Cell(3, p).Style.Font.FontSize = 16;
                    worksheet.Cell(3, p).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Cell(3, p).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    worksheet.Cell(3, p).Style.Border.BottomBorderColor = XLColor.Black;
                    p++;
                }



                int fila = 4;
                int columna = 2;
                int estaEnLineaEspecial = 0;

                for (int i = 0; i < DISEÑO.Rows.Count; i++)
                {

                    for (int y = 0; y < DISEÑO.Columns.Count; y++)
                    {
                        string campo;
                        campo = DISEÑO.Rows[i][y].ToString();

                        if (campo.Equals("TOTAL") || campo.Equals("TOTAL FINAL"))
                        {
                            estaEnLineaEspecial = 1;
                        }

                        if (estaEnLineaEspecial == 1)
                        {



                            if (y > 2)
                            {

                                worksheet.Cell(fila, columna).Style.NumberFormat.Format = "#,##0.00";
                                worksheet.Cell(fila, columna).DataType = XLDataType.Number;
                                worksheet.Cell(fila, columna).Value = decimal.Parse(campo);



                            }
                            else
                            {
                                worksheet.Cell(fila, columna).Value = campo;
                            }

                            worksheet.Cell(fila, columna).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
                            worksheet.Cell(fila, columna).Style.Border.TopBorder = XLBorderStyleValues.Thick;
                            worksheet.Cell(fila, columna).Style.Border.BottomBorderColor = XLColor.Black;
                            worksheet.Cell(fila, columna).Style.Border.TopBorderColor = XLColor.Black;

                            worksheet.Cell(fila, columna).Style.Font.FontColor = XLColor.Black;
                            worksheet.Cell(fila, columna).Style.Font.Bold = true;
                            worksheet.Cell(fila, columna).Style.Font.FontSize = 16;

                            estaEnLineaEspecial = 1;
                        }
                        else
                        {
                            if (y > 2)
                            {

                                worksheet.Cell(fila, columna).Style.NumberFormat.Format = "#,##0.00";
                                worksheet.Cell(fila, columna).DataType = XLDataType.Number;
                                worksheet.Cell(fila, columna).Value = decimal.Parse(campo);

                            }
                            else
                            {
                                worksheet.Cell(fila, columna).Value = campo;
                            }



                            worksheet.Cell(fila, columna).Style.Font.FontColor = XLColor.Black;

                        }

                        columna += 1;

                    }



                    estaEnLineaEspecial = 0;
                    columna = 2;
                    fila += 1;


                }


                worksheet.Columns().AdjustToContents();

                workbook.SaveAs("c:\\SEMP2013\\NotasGlobales.xlsx");

                MessageBox.Show("Realizado");



            }


            if (radioEmpresa.Checked == true)
            {
                var listaAOperar = seleccionReporteList.Select(pi => pi.empresa).Distinct().ToList();

                resultadoNotasDePago.Clear();


                /*COMO ES TODO VAMOS A SUMAR TODAS LAS SUCURSALES SIN IMPORTAR LA MARCA*/
                foreach (var empresa in listaAOperar)
                {
                    var sucursalesXempresa = buscarLocalidad.listaSucursalesEmpresa(empresa);

                    //Y agregamos nuestra primera informacion de sucursal
                    distintas += 1;
                    if (distintas == 1)
                    {
                        columna_dato += 3;
                    }
                    else
                    {
                        columna_dato += 3;
                    }
                    DISEÑO.Columns.Add("SubTotal-" + distintas, Type.GetType("System.Decimal"));
                    DISEÑO.Columns.Add("Iva.....-" + distintas, Type.GetType("System.Decimal"));
                    DISEÑO.Columns.Add("Total...-" + distintas, Type.GetType("System.Decimal"));

                    foreach (var sucursal in sucursalesXempresa)
                    {

                        resultadoReporte.Clear();

                        resultadoReporte = resultadosOperacion.NotasDePagoXdiaResumen(1, 2018, sucursal.sucursalBD, 1,
                                            fechaInicio, fechaFinal, conn, 1, porSemana, "", "", "", "", 1);

                        //POR SI NO OPERO EN TODO EL PERIODO 
                        if (resultadoReporte.Rows.Count > 0)
                        {

                            int i = 0;
                            foreach (DataRow items in resultadoReporte.Rows)
                            {

                                string x = "0", y = "0", z = "0";
                                string u, v, w = "0";
                                decimal subtotal, iva, total = 0;


                                x = items[9].ToString();//subtotal
                                y = items[10].ToString();//iva
                                z = items[11].ToString();//total


                                u = DISEÑO.Rows[i][columna_dato].ToString();
                                v = DISEÑO.Rows[i][columna_dato + 1].ToString();
                                w = DISEÑO.Rows[i][columna_dato + 2].ToString();



                                if (string.IsNullOrEmpty(u))
                                {
                                    u = "0";
                                }
                                if (string.IsNullOrEmpty(v))
                                {
                                    v = "0";
                                }
                                if (string.IsNullOrEmpty(w))
                                {
                                    w = "0";
                                }


                                subtotal = Convert.ToDecimal(u.ToString()) + Convert.ToDecimal(x.ToString());

                                iva = Convert.ToDecimal(v.ToString()) + Convert.ToDecimal(y.ToString());

                                total = Convert.ToDecimal(w.ToString()) + Convert.ToDecimal(z.ToString());



                                DISEÑO.Rows[i][columna_dato] = decimal.Parse(subtotal.ToString()).ToString("N2");
                                DISEÑO.Rows[i][columna_dato + 1] = decimal.Parse(iva.ToString()).ToString("N2");
                                DISEÑO.Rows[i][columna_dato + 2] = decimal.Parse(total.ToString()).ToString("N2");

                                DISEÑO.AcceptChanges();
                                i = i + 1;
                            }


                        }


                    }


                }

                ///*AHORA EL EXCEL*/

                var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("NotasPagoXEmpresa");
                /*luego sumamos todo a partir de la ultima fila*/
                var worksheet2 = workbook.Worksheets.Add("NotasPagoXEmpresaResumen");

                #region Libro Resumen
                worksheet2.PageSetup.PageOrientation = XLPageOrientation.Landscape;
                worksheet2.PageSetup.FitToPages(1, 1);
                //EN PULGADAS
                worksheet2.PageSetup.Margins.Top = 1;
                worksheet2.PageSetup.Margins.Bottom = 1;
                worksheet2.PageSetup.Margins.Left = 1;
                worksheet2.PageSetup.Margins.Right = 1;
                worksheet2.PageSetup.CenterHorizontally = true;
                worksheet2.PageSetup.CenterVertically = true;


                var listaInfo3 = seleccionReporteList.Select(list => list.bd).Distinct().ToList();
                worksheet2.PageSetup.Header.Left.AddText("Localidades Calculadas");
                worksheet2.PageSetup.Header.Left.AddNewLine();
                foreach (var item in listaInfo3)
                {
                    worksheet2.PageSetup.Header.Left.AddText(item.Substring(9, item.Length - 9));
                    worksheet2.PageSetup.Header.Left.AddNewLine();

                }







                worksheet2.PageSetup.Header.Center.AddText("TIPO DE REPORTE: NOTAS DE PAGO GLOBALES X EMPRESA");
                worksheet2.PageSetup.Header.Center.AddNewLine();
                worksheet2.PageSetup.Header.Center.AddText("TOTAL DE TODAS LAS EMPRESAS");

                worksheet2.PageSetup.Header.Right.AddText("DEL " + string.Format(date1.Value.ToString("dddd dd {0} MMMM {1} yyyy"), "de", "del"));
                worksheet2.PageSetup.Header.Right.AddNewLine();
                worksheet2.PageSetup.Header.Right.AddText(" AL " + string.Format(date2.Value.ToString("dddd dd {0} MMMM {1} yyyy"), "de", "del"));

                worksheet2.PageSetup.Footer.Center.AddText(XLHFPredefinedText.PageNumber, XLHFOccurrence.AllPages);
                worksheet2.PageSetup.Footer.Center.AddText(" / ", XLHFOccurrence.AllPages);
                worksheet2.PageSetup.Footer.Center.AddText(XLHFPredefinedText.NumberOfPages, XLHFOccurrence.AllPages);

                #endregion
                #region Libro Contenido
                worksheet.PageSetup.PageOrientation = XLPageOrientation.Landscape;
                worksheet.PageSetup.FitToPages(1, 1);
                //EN PULGADAS
                worksheet.PageSetup.Margins.Top = 1;
                worksheet.PageSetup.Margins.Bottom = 1;
                worksheet.PageSetup.Margins.Left = 1;
                worksheet.PageSetup.Margins.Right = 1;
                worksheet.PageSetup.CenterHorizontally = true;
                worksheet.PageSetup.CenterVertically = true;

                var listaInfo = seleccionReporteList.Select(lis => lis.bd).Distinct().ToList();
                worksheet.PageSetup.Header.Left.AddText("Localidades Calculadas");
                worksheet.PageSetup.Header.Left.AddNewLine();
                foreach (var item in listaInfo)
                {
                    worksheet.PageSetup.Header.Left.AddText(item.Substring(9, item.Length - 9));
                    worksheet.PageSetup.Header.Left.AddNewLine();

                }


                worksheet.PageSetup.Header.Center.AddText("TIPO DE REPORTE: NOTAS DE PAGO GLOBALES X EMPRESA");

                worksheet.PageSetup.Header.Right.AddText("DEL " + string.Format(date1.Value.ToString("dddd dd {0} MMMM {1} yyyy"), "de", "del"));
                worksheet.PageSetup.Header.Right.AddNewLine();
                worksheet.PageSetup.Header.Right.AddText(" AL " + string.Format(date2.Value.ToString("dddd dd {0} MMMM {1} yyyy"), "de", "del"));

                worksheet.PageSetup.Footer.Center.AddText(XLHFPredefinedText.PageNumber, XLHFOccurrence.AllPages);
                worksheet.PageSetup.Footer.Center.AddText(" / ", XLHFOccurrence.AllPages);
                worksheet.PageSetup.Footer.Center.AddText(XLHFPredefinedText.NumberOfPages, XLHFOccurrence.AllPages);

                #endregion


                //MIS ENCABEZADOS//

                int p = 2;
                for (int i = 0; i < DISEÑO.Columns.Count; i++)
                {
                    string nombrecolumna = DISEÑO.Columns[i].ColumnName;

                    if (nombrecolumna.Contains("Sub"))
                    {
                        nombrecolumna = "Sub";
                    }

                    if (nombrecolumna.Contains("Iva"))
                    {
                        nombrecolumna = "Iva";
                    }

                    if (nombrecolumna.Contains("Total"))
                    {
                        nombrecolumna = "Total";
                    }

                    worksheet.Cell(3, p).Value = nombrecolumna;
                    worksheet.Cell(3, p).Style.Fill.SetBackgroundColor(XLColor.Navy);
                    worksheet.Cell(3, p).Style.Font.FontColor = XLColor.White;
                    worksheet.Cell(3, p).Style.Font.Bold = true;
                    worksheet.Cell(3, p).Style.Font.FontSize = 16;
                    worksheet.Cell(3, p).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Cell(3, p).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    worksheet.Cell(3, p).Style.Border.BottomBorderColor = XLColor.Black;
                    p++;
                }


                //ENCABEZADOS PRINCIPALES
                int comb = 5;
                foreach (var empresa in listaAOperar)
                {


                    string nombrecolumna = empresa;

                    if (nombrecolumna.Length > 10)
                    {
                        nombrecolumna = nombrecolumna.Substring(0, 10);
                    }

                    worksheet.Cell(2, comb).Value = nombrecolumna;
                    worksheet.Cell(2, comb).Style.Fill.SetBackgroundColor(XLColor.Navy);
                    worksheet.Cell(2, comb).Style.Font.FontColor = XLColor.White;
                    worksheet.Cell(2, comb).Style.Font.Bold = true;
                    worksheet.Cell(2, comb).Style.Font.FontSize = 16;
                    worksheet.Cell(2, comb).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Cell(2, comb).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    worksheet.Cell(2, comb).Style.Border.BottomBorderColor = XLColor.Black;

                    var range = worksheet.Range(worksheet.Cell(2, comb).Address, worksheet.Cell(2, comb + 2).Address);
                    range.Merge();

                    comb += 3;

                }



                int fila = 4;
                int columna = 2;
                int estaEnLineaEspecial = 0;

                for (int i = 0; i < DISEÑO.Rows.Count; i++)
                {

                    for (int y = 0; y < DISEÑO.Columns.Count; y++)
                    {
                        string campo;
                        campo = DISEÑO.Rows[i][y].ToString();

                        if (campo.Equals("TOTAL") || campo.Equals("TOTAL FINAL"))
                        {
                            estaEnLineaEspecial = 1;
                        }

                        if (estaEnLineaEspecial == 1)
                        {



                            if (y > 2)
                            {

                                worksheet.Cell(fila, columna).Style.NumberFormat.Format = "#,##0.00";
                                worksheet.Cell(fila, columna).DataType = XLDataType.Number;
                                worksheet.Cell(fila, columna).Value = decimal.Parse(campo);



                            }
                            else
                            {
                                worksheet.Cell(fila, columna).Value = campo;
                            }

                            worksheet.Cell(fila, columna).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
                            worksheet.Cell(fila, columna).Style.Border.TopBorder = XLBorderStyleValues.Thick;
                            worksheet.Cell(fila, columna).Style.Border.BottomBorderColor = XLColor.Black;
                            worksheet.Cell(fila, columna).Style.Border.TopBorderColor = XLColor.Black;

                            worksheet.Cell(fila, columna).Style.Font.FontColor = XLColor.Black;
                            worksheet.Cell(fila, columna).Style.Font.Bold = true;
                            worksheet.Cell(fila, columna).Style.Font.FontSize = 16;

                            estaEnLineaEspecial = 1;
                        }
                        else
                        {
                            if (y > 2)
                            {

                                worksheet.Cell(fila, columna).Style.NumberFormat.Format = "#,##0.00";
                                worksheet.Cell(fila, columna).DataType = XLDataType.Number;
                                worksheet.Cell(fila, columna).Value = decimal.Parse(campo);

                            }
                            else
                            {
                                worksheet.Cell(fila, columna).Value = campo;
                            }



                            worksheet.Cell(fila, columna).Style.Font.FontColor = XLColor.Black;

                        }

                        columna += 1;

                    }



                    estaEnLineaEspecial = 0;
                    columna = 2;
                    fila += 1;


                }


                worksheet.Columns().AdjustToContents();





                //AHORA AGREGAMOS UNA PAGINA PARA EL TOTAL DE LAS EMPRESAS

                //COMO ES UNA NUEVA HOJA VOLVEMOS A CARGAR EL CALENDARIO

                //MIS ENCABEZADOS//

                int p2 = 2;
                for (int i = 0; i < 6; i++)//los primeros 5columnas
                {
                    string nombrecolumna = DISEÑO.Columns[i].ColumnName;

                    if (nombrecolumna.Contains("Sub"))
                    {
                        nombrecolumna = "Sub-Global";
                    }

                    if (nombrecolumna.Contains("Iva"))
                    {
                        nombrecolumna = "Iva-Global";
                    }

                    if (nombrecolumna.Contains("Total"))
                    {
                        nombrecolumna = "Total-Global";
                    }

                    worksheet2.Cell(3, p2).Value = nombrecolumna;
                    worksheet2.Cell(3, p2).Style.Fill.SetBackgroundColor(XLColor.Navy);
                    worksheet2.Cell(3, p2).Style.Font.FontColor = XLColor.White;
                    worksheet2.Cell(3, p2).Style.Font.Bold = true;
                    worksheet2.Cell(3, p2).Style.Font.FontSize = 16;
                    worksheet2.Cell(3, p2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet2.Cell(3, p2).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    worksheet2.Cell(3, p2).Style.Border.BottomBorderColor = XLColor.Black;
                    p2++;
                }
                //la fecha en la nueva hoja
                int IsColumnTotalL = 0;
                fila = 4;
                columna = 2;
                for (int i = 0; i < DISEÑO.Rows.Count; i++)
                {

                    string campo;
                    string campo2;
                    string campo3;


                    if (DISEÑO.Rows[i][0].ToString() == "TOTAL" ||
                        DISEÑO.Rows[i][0].ToString() == "TOTAL FINAL")
                    {
                        IsColumnTotalL = 1;

                    }

                    campo = DISEÑO.Rows[i][0].ToString();
                    campo2 = DISEÑO.Rows[i][1].ToString();
                    campo3 = DISEÑO.Rows[i][2].ToString();




                    if (IsColumnTotalL == 1)
                    {

                        worksheet2.Cell(fila, columna).Value = campo;


                        worksheet2.Cell(fila, columna + 1).Value = campo2;


                        worksheet2.Cell(fila, columna + 2).Value = campo3;

                        worksheet2.Cell(fila, columna).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
                        worksheet2.Cell(fila, columna).Style.Border.TopBorder = XLBorderStyleValues.Thick;
                        worksheet2.Cell(fila, columna).Style.Border.BottomBorderColor = XLColor.Black;
                        worksheet2.Cell(fila, columna).Style.Border.TopBorderColor = XLColor.Black;

                        worksheet2.Cell(fila, columna).Style.Font.FontColor = XLColor.Black;
                        worksheet2.Cell(fila, columna).Style.Font.Bold = true;
                        worksheet2.Cell(fila, columna).Style.Font.FontSize = 16;

                        //
                        worksheet2.Cell(fila, columna + 1).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
                        worksheet2.Cell(fila, columna + 1).Style.Border.TopBorder = XLBorderStyleValues.Thick;
                        worksheet2.Cell(fila, columna + 1).Style.Border.BottomBorderColor = XLColor.Black;
                        worksheet2.Cell(fila, columna + 1).Style.Border.TopBorderColor = XLColor.Black;

                        worksheet2.Cell(fila, columna + 1).Style.Font.FontColor = XLColor.Black;
                        worksheet2.Cell(fila, columna + 1).Style.Font.Bold = true;
                        worksheet2.Cell(fila, columna + 1).Style.Font.FontSize = 16;
                        //
                        worksheet2.Cell(fila, columna + 2).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
                        worksheet2.Cell(fila, columna + 2).Style.Border.TopBorder = XLBorderStyleValues.Thick;
                        worksheet2.Cell(fila, columna + 2).Style.Border.BottomBorderColor = XLColor.Black;
                        worksheet2.Cell(fila, columna + 2).Style.Border.TopBorderColor = XLColor.Black;

                        worksheet2.Cell(fila, columna + 2).Style.Font.FontColor = XLColor.Black;
                        worksheet2.Cell(fila, columna + 2).Style.Font.Bold = true;
                        worksheet2.Cell(fila, columna + 2).Style.Font.FontSize = 16;



                        IsColumnTotalL = 0;
                    }
                    else
                    {
                        worksheet2.Cell(fila, columna).Value = campo;


                        worksheet2.Cell(fila, columna + 1).Value = campo2;


                        worksheet2.Cell(fila, columna + 2).Value = campo3;
                    }

                    IsColumnTotalL = 0;
                    fila += 1;

                }


                //

                int IsColumnTotal = 0;
                decimal suma_Subtotal = 0;
                decimal suma_Iva = 0;
                decimal suma_total = 0;
                fila = 4;
                columna = 5;
                for (int i = 0; i < DISEÑO.Rows.Count; i++)
                {
                    for (int y = 3; y < DISEÑO.Columns.Count; y += 3) //foreach (DataColumn item in DISEÑO.Columns)
                    {
                        string campo;
                        string campo2;
                        string campo3;


                        if (DISEÑO.Rows[i][0].ToString() == "TOTAL" ||
                            DISEÑO.Rows[i][0].ToString() == "TOTAL FINAL")
                        {
                            IsColumnTotal = 1;

                        }

                        campo = DISEÑO.Rows[i][y].ToString();
                        campo2 = DISEÑO.Rows[i][y + 1].ToString();
                        campo3 = DISEÑO.Rows[i][y + 2].ToString();

                        suma_Subtotal += decimal.Parse(campo);
                        suma_Iva += decimal.Parse(campo2);
                        suma_total += decimal.Parse(campo3);


                    }

                    if (IsColumnTotal == 1)
                    {
                        worksheet2.Cell(fila, columna).Style.NumberFormat.Format = "#,##0.00";
                        worksheet2.Cell(fila, columna).DataType = XLDataType.Number;
                        worksheet2.Cell(fila, columna).Value = suma_Subtotal;

                        worksheet2.Cell(fila, columna + 1).Style.NumberFormat.Format = "#,##0.00";
                        worksheet2.Cell(fila, columna + 1).DataType = XLDataType.Number;
                        worksheet2.Cell(fila, columna + 1).Value = suma_Iva;

                        worksheet2.Cell(fila, columna + 2).Style.NumberFormat.Format = "#,##0.00";
                        worksheet2.Cell(fila, columna + 2).DataType = XLDataType.Number;
                        worksheet2.Cell(fila, columna + 2).Value = suma_total;

                        worksheet2.Cell(fila, columna).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
                        worksheet2.Cell(fila, columna).Style.Border.TopBorder = XLBorderStyleValues.Thick;
                        worksheet2.Cell(fila, columna).Style.Border.BottomBorderColor = XLColor.Black;
                        worksheet2.Cell(fila, columna).Style.Border.TopBorderColor = XLColor.Black;

                        worksheet2.Cell(fila, columna).Style.Font.FontColor = XLColor.Black;
                        worksheet2.Cell(fila, columna).Style.Font.Bold = true;
                        worksheet2.Cell(fila, columna).Style.Font.FontSize = 16;

                        //
                        worksheet2.Cell(fila, columna + 1).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
                        worksheet2.Cell(fila, columna + 1).Style.Border.TopBorder = XLBorderStyleValues.Thick;
                        worksheet2.Cell(fila, columna + 1).Style.Border.BottomBorderColor = XLColor.Black;
                        worksheet2.Cell(fila, columna + 1).Style.Border.TopBorderColor = XLColor.Black;

                        worksheet2.Cell(fila, columna + 1).Style.Font.FontColor = XLColor.Black;
                        worksheet2.Cell(fila, columna + 1).Style.Font.Bold = true;
                        worksheet2.Cell(fila, columna + 1).Style.Font.FontSize = 16;
                        //
                        worksheet2.Cell(fila, columna + 2).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
                        worksheet2.Cell(fila, columna + 2).Style.Border.TopBorder = XLBorderStyleValues.Thick;
                        worksheet2.Cell(fila, columna + 2).Style.Border.BottomBorderColor = XLColor.Black;
                        worksheet2.Cell(fila, columna + 2).Style.Border.TopBorderColor = XLColor.Black;

                        worksheet2.Cell(fila, columna + 2).Style.Font.FontColor = XLColor.Black;
                        worksheet2.Cell(fila, columna + 2).Style.Font.Bold = true;
                        worksheet2.Cell(fila, columna + 2).Style.Font.FontSize = 16;



                        IsColumnTotal = 0;
                    }
                    else
                    {
                        worksheet2.Cell(fila, columna).Style.NumberFormat.Format = "#,##0.00";
                        worksheet2.Cell(fila, columna).DataType = XLDataType.Number;
                        worksheet2.Cell(fila, columna).Value = suma_Subtotal;

                        worksheet2.Cell(fila, columna + 1).Style.NumberFormat.Format = "#,##0.00";
                        worksheet2.Cell(fila, columna + 1).DataType = XLDataType.Number;
                        worksheet2.Cell(fila, columna + 1).Value = suma_Iva;

                        worksheet2.Cell(fila, columna + 2).Style.NumberFormat.Format = "#,##0.00";
                        worksheet2.Cell(fila, columna + 2).DataType = XLDataType.Number;
                        worksheet2.Cell(fila, columna + 2).Value = suma_total;
                    }


                    fila += 1;
                    suma_Subtotal = 0;
                    suma_Iva = 0;
                    suma_total = 0;
                }

                ///


                worksheet2.Columns().AdjustToContents();
                workbook.SaveAs("c:\\SEMP2013\\NotasGlobalesEmpresa.xlsx");

                MessageBox.Show("Realizado");


            }


            if (radioSucursal.Checked == true)
            {
                var listaAOperar = seleccionReporteList.Select(pil => pil.bd).Distinct().ToList();

                resultadoNotasDePago.Clear();



                foreach (var sucursal in listaAOperar)
                {

                    //Y agregamos nuestra primera informacion de sucursal
                    distintas += 1;
                    if (distintas == 1)
                    {
                        columna_dato += 3;
                    }
                    else
                    {
                        columna_dato += 3;
                    }
                    DISEÑO.Columns.Add("SubTotal-" + distintas, Type.GetType("System.Decimal"));
                    DISEÑO.Columns.Add("Iva.....-" + distintas, Type.GetType("System.Decimal"));
                    DISEÑO.Columns.Add("Total...-" + distintas, Type.GetType("System.Decimal"));

                    resultadoReporte.Clear();

                    resultadoReporte = resultadosOperacion.NotasDePagoXdiaResumen(1, 2018, sucursal, 1,
                                        fechaInicio, fechaFinal, conn, 1, porSemana, "", "", "", "", 1);

                    //POR SI NO OPERO EN TODO EL PERIODO 
                    if (resultadoReporte.Rows.Count > 0)
                    {

                        int i = 0;
                        foreach (DataRow items in resultadoReporte.Rows)
                        {

                            string x = "0", y = "0", z = "0";
                            string u, v, w = "0";
                            decimal subtotal, iva, total = 0;


                            x = items[9].ToString();//subtotal
                            y = items[10].ToString();//iva
                            z = items[11].ToString();//total


                            u = DISEÑO.Rows[i][columna_dato].ToString();
                            v = DISEÑO.Rows[i][columna_dato + 1].ToString();
                            w = DISEÑO.Rows[i][columna_dato + 2].ToString();



                            if (string.IsNullOrEmpty(u))
                            {
                                u = "0";
                            }
                            if (string.IsNullOrEmpty(v))
                            {
                                v = "0";
                            }
                            if (string.IsNullOrEmpty(w))
                            {
                                w = "0";
                            }


                            subtotal = Convert.ToDecimal(u.ToString()) + Convert.ToDecimal(x.ToString());

                            iva = Convert.ToDecimal(v.ToString()) + Convert.ToDecimal(y.ToString());

                            total = Convert.ToDecimal(w.ToString()) + Convert.ToDecimal(z.ToString());



                            DISEÑO.Rows[i][columna_dato] = decimal.Parse(subtotal.ToString()).ToString("N2");
                            DISEÑO.Rows[i][columna_dato + 1] = decimal.Parse(iva.ToString()).ToString("N2");
                            DISEÑO.Rows[i][columna_dato + 2] = decimal.Parse(total.ToString()).ToString("N2");

                            DISEÑO.AcceptChanges();
                            i = i + 1;
                        }


                    }


                }

                ///*AHORA EL EXCEL POR CADA SUCURSAL UNA NUEVA HOJA*/
                var workbook = new XLWorkbook();

                int recorreColumna = 3;
                int recorre = 5;

                foreach (var sucursal in listaAOperar)
                {


                    #region Funciona

                    string nombrecolumna = "";

                    var worksheet = workbook.Worksheets.Add(sucursal);
                    #region Libro Contenido
                    worksheet.PageSetup.PageOrientation = XLPageOrientation.Landscape;
                    worksheet.PageSetup.FitToPages(1, 1);
                    //EN PULGADAS
                    worksheet.PageSetup.Margins.Top = 1;
                    worksheet.PageSetup.Margins.Bottom = 1;
                    worksheet.PageSetup.Margins.Left = 1;
                    worksheet.PageSetup.Margins.Right = 1;
                    worksheet.PageSetup.CenterHorizontally = true;
                    worksheet.PageSetup.CenterVertically = true;

                    var listaInfo = seleccionReporteList.Select(lis => lis.bd).Distinct().ToList();
                    worksheet.PageSetup.Header.Left.AddText("Localidades Calculadas");
                    worksheet.PageSetup.Header.Left.AddNewLine();
                    foreach (var item in listaInfo)
                    {
                        worksheet.PageSetup.Header.Left.AddText(item.Substring(9, item.Length - 9));
                        worksheet.PageSetup.Header.Left.AddNewLine();

                    }


                    worksheet.PageSetup.Header.Center.AddText("TIPO DE REPORTE: NOTAS DE PAGO GLOBALES X SUCURSAL");

                    worksheet.PageSetup.Header.Right.AddText("DEL " + string.Format(date1.Value.ToString("dddd dd {0} MMMM {1} yyyy"), "de", "del"));
                    worksheet.PageSetup.Header.Right.AddNewLine();
                    worksheet.PageSetup.Header.Right.AddText(" AL " + string.Format(date2.Value.ToString("dddd dd {0} MMMM {1} yyyy"), "de", "del"));

                    worksheet.PageSetup.Footer.Center.AddText(XLHFPredefinedText.PageNumber, XLHFOccurrence.AllPages);
                    worksheet.PageSetup.Footer.Center.AddText(" / ", XLHFOccurrence.AllPages);
                    worksheet.PageSetup.Footer.Center.AddText(XLHFPredefinedText.NumberOfPages, XLHFOccurrence.AllPages);

                    #endregion


                    //MIS ENCABEZADOS//

                    int p = 2;
                    for (int i = 0; i < 6; i++)
                    {
                        nombrecolumna = DISEÑO.Columns[i].ColumnName;

                        if (nombrecolumna.Contains("Sub"))
                        {
                            nombrecolumna = "Sub";
                        }

                        if (nombrecolumna.Contains("Iva"))
                        {
                            nombrecolumna = "Iva";
                        }

                        if (nombrecolumna.Contains("Total"))
                        {
                            nombrecolumna = "Total";
                        }

                        worksheet.Cell(3, p).Value = nombrecolumna;
                        worksheet.Cell(3, p).Style.Fill.SetBackgroundColor(XLColor.Navy);
                        worksheet.Cell(3, p).Style.Font.FontColor = XLColor.White;
                        worksheet.Cell(3, p).Style.Font.Bold = true;
                        worksheet.Cell(3, p).Style.Font.FontSize = 16;
                        worksheet.Cell(3, p).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        worksheet.Cell(3, p).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        worksheet.Cell(3, p).Style.Border.BottomBorderColor = XLColor.Black;
                        p++;
                    }



                    //ENCABEZADOS PRINCIPALES
                    int comb = 5;


                    nombrecolumna = sucursal;

                    if (nombrecolumna.Length > 10)
                    {
                        nombrecolumna = nombrecolumna.Substring(9, nombrecolumna.Length - 9);
                    }

                    worksheet.Cell(2, comb).Value = nombrecolumna;
                    worksheet.Cell(2, comb).Style.Fill.SetBackgroundColor(XLColor.Navy);
                    worksheet.Cell(2, comb).Style.Font.FontColor = XLColor.White;
                    worksheet.Cell(2, comb).Style.Font.Bold = true;
                    worksheet.Cell(2, comb).Style.Font.FontSize = 16;
                    worksheet.Cell(2, comb).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Cell(2, comb).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    worksheet.Cell(2, comb).Style.Border.BottomBorderColor = XLColor.Black;

                    var range = worksheet.Range(worksheet.Cell(2, comb).Address, worksheet.Cell(2, comb + 2).Address);
                    range.Merge();

                    int fila = 4;
                    int columna = 2;
                    int estaEnLineaEspecial = 0;

                    //PRIMERO EL CALENDARIO//
                    for (int i = 0; i < DISEÑO.Rows.Count; i++)
                    {
                       

                        for (int y = 0; y < 3; y++)//en columnas incrementa cada 3
                        {
                            string campo;
                            campo = DISEÑO.Rows[i][y].ToString();

                            if (campo.Equals("TOTAL") || campo.Equals("TOTAL FINAL"))
                            {
                                estaEnLineaEspecial = 1;
                            }

                            if (estaEnLineaEspecial == 1)
                            {

                                worksheet.Cell(fila, columna).Value = campo;


                                worksheet.Cell(fila, columna).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
                                worksheet.Cell(fila, columna).Style.Border.TopBorder = XLBorderStyleValues.Thick;
                                worksheet.Cell(fila, columna).Style.Border.BottomBorderColor = XLColor.Black;
                                worksheet.Cell(fila, columna).Style.Border.TopBorderColor = XLColor.Black;

                                worksheet.Cell(fila, columna).Style.Font.FontColor = XLColor.Black;
                                worksheet.Cell(fila, columna).Style.Font.Bold = true;
                                worksheet.Cell(fila, columna).Style.Font.FontSize = 16;


                            }
                            else
                            {

                                worksheet.Cell(fila, columna).Value = campo;
                                worksheet.Cell(fila, columna).Style.Font.FontColor = XLColor.Black;

                            }

                            columna += 1;

                        }



                        estaEnLineaEspecial = 0;
                        columna = 2;
                        fila += 1;


                    }

                    #endregion

                    //LUEGO LOS RESULTADOS
                    fila = 4;
                    columna = 5;
                    estaEnLineaEspecial = 0;


                    //AHORA LOS CALCULOS//
                    foreach (DataRow datarow in DISEÑO.Rows)
                    {

                        int desde= recorreColumna;
                        while (desde <= recorre)//3-5
                        {

                            string campo,campo2;

                            campo = datarow[0].ToString();
                            if (campo.Equals("TOTAL") || campo.Equals("TOTAL FINAL"))
                            {
                                estaEnLineaEspecial = 1;
                            }

                            campo2 = datarow[desde].ToString();



                            if (estaEnLineaEspecial == 1)
                            {




                                worksheet.Cell(fila, columna).Style.NumberFormat.Format = "#,##0.00";
                                worksheet.Cell(fila, columna).DataType = XLDataType.Number;
                                worksheet.Cell(fila, columna).Value = decimal.Parse(campo2);


                                worksheet.Cell(fila, columna).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
                                worksheet.Cell(fila, columna).Style.Border.TopBorder = XLBorderStyleValues.Thick;
                                worksheet.Cell(fila, columna).Style.Border.BottomBorderColor = XLColor.Black;
                                worksheet.Cell(fila, columna).Style.Border.TopBorderColor = XLColor.Black;

                                worksheet.Cell(fila, columna).Style.Font.FontColor = XLColor.Black;
                                worksheet.Cell(fila, columna).Style.Font.Bold = true;
                                worksheet.Cell(fila, columna).Style.Font.FontSize = 16;


                            }
                            else
                            {

                                worksheet.Cell(fila, columna).Style.NumberFormat.Format = "#,##0.00";
                                worksheet.Cell(fila, columna).DataType = XLDataType.Number;
                                worksheet.Cell(fila, columna).Value = decimal.Parse(campo2);
                                worksheet.Cell(fila, columna).Style.Font.FontColor = XLColor.Black;

                            }


                            desde++;

                            columna++;

                        }



                        estaEnLineaEspecial = 0;
                        columna = 5;
                        fila ++;


                    }

                 


                    recorreColumna = recorreColumna + 3;
                    recorre = recorre + 3;

                    worksheet.Columns().AdjustToContents();
                }






                /*luego sumamos todo a partir de la ultima fila*/
                var worksheet2 = workbook.Worksheets.Add("NotasPagoXSucursalResumen");

                #region Libro Resumen
                worksheet2.PageSetup.PageOrientation = XLPageOrientation.Landscape;
                worksheet2.PageSetup.FitToPages(1, 1);
                //EN PULGADAS
                worksheet2.PageSetup.Margins.Top = 1;
                worksheet2.PageSetup.Margins.Bottom = 1;
                worksheet2.PageSetup.Margins.Left = 1;
                worksheet2.PageSetup.Margins.Right = 1;
                worksheet2.PageSetup.CenterHorizontally = true;
                worksheet2.PageSetup.CenterVertically = true;


                var listaInfo3 = seleccionReporteList.Select(list => list.bd).Distinct().ToList();
                worksheet2.PageSetup.Header.Left.AddText("Localidades Calculadas");
                worksheet2.PageSetup.Header.Left.AddNewLine();
                foreach (var item in listaInfo3)
                {
                    worksheet2.PageSetup.Header.Left.AddText(item.Substring(9, item.Length - 9));
                    worksheet2.PageSetup.Header.Left.AddNewLine();

                }







                worksheet2.PageSetup.Header.Center.AddText("TIPO DE REPORTE: NOTAS DE PAGO GLOBALES X SUCURSAL");
                worksheet2.PageSetup.Header.Center.AddNewLine();
                worksheet2.PageSetup.Header.Center.AddText("TOTAL DE TODAS LAS SUCURSALES");

                worksheet2.PageSetup.Header.Right.AddText("DEL " + string.Format(date1.Value.ToString("dddd dd {0} MMMM {1} yyyy"), "de", "del"));
                worksheet2.PageSetup.Header.Right.AddNewLine();
                worksheet2.PageSetup.Header.Right.AddText(" AL " + string.Format(date2.Value.ToString("dddd dd {0} MMMM {1} yyyy"), "de", "del"));

                worksheet2.PageSetup.Footer.Center.AddText(XLHFPredefinedText.PageNumber, XLHFOccurrence.AllPages);
                worksheet2.PageSetup.Footer.Center.AddText(" / ", XLHFOccurrence.AllPages);
                worksheet2.PageSetup.Footer.Center.AddText(XLHFPredefinedText.NumberOfPages, XLHFOccurrence.AllPages);

                #endregion


                //AHORA AGREGAMOS UNA PAGINA PARA EL TOTAL DE LAS EMPRESAS

                //COMO ES UNA NUEVA HOJA VOLVEMOS A CARGAR EL CALENDARIO

                //MIS ENCABEZADOS//

                int p2 = 2;
                for (int i = 0; i < 6; i++)//los primeros 5columnas
                {
                    string nombrecolumna = DISEÑO.Columns[i].ColumnName;

                    if (nombrecolumna.Contains("Sub"))
                    {
                        nombrecolumna = "Sub-Global";
                    }

                    if (nombrecolumna.Contains("Iva"))
                    {
                        nombrecolumna = "Iva-Global";
                    }

                    if (nombrecolumna.Contains("Total"))
                    {
                        nombrecolumna = "Total-Global";
                    }

                    worksheet2.Cell(3, p2).Value = nombrecolumna;
                    worksheet2.Cell(3, p2).Style.Fill.SetBackgroundColor(XLColor.Navy);
                    worksheet2.Cell(3, p2).Style.Font.FontColor = XLColor.White;
                    worksheet2.Cell(3, p2).Style.Font.Bold = true;
                    worksheet2.Cell(3, p2).Style.Font.FontSize = 16;
                    worksheet2.Cell(3, p2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet2.Cell(3, p2).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    worksheet2.Cell(3, p2).Style.Border.BottomBorderColor = XLColor.Black;
                    p2++;
                }
                //la fecha en la nueva hoja
                int IsColumnTotalL = 0;
                int filaL = 4;
                int columnaL = 2;

                for (int i = 0; i < DISEÑO.Rows.Count; i++)
                {

                    string campo;
                    string campo2;
                    string campo3;


                    if (DISEÑO.Rows[i][0].ToString() == "TOTAL" ||
                        DISEÑO.Rows[i][0].ToString() == "TOTAL FINAL")
                    {
                        IsColumnTotalL = 1;

                    }

                    campo = DISEÑO.Rows[i][0].ToString();
                    campo2 = DISEÑO.Rows[i][1].ToString();
                    campo3 = DISEÑO.Rows[i][2].ToString();




                    if (IsColumnTotalL == 1)
                    {

                        worksheet2.Cell(filaL, columnaL).Value = campo;


                        worksheet2.Cell(filaL, columnaL + 1).Value = campo2;


                        worksheet2.Cell(filaL, columnaL + 2).Value = campo3;

                        worksheet2.Cell(filaL, columnaL).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
                        worksheet2.Cell(filaL, columnaL).Style.Border.TopBorder = XLBorderStyleValues.Thick;
                        worksheet2.Cell(filaL, columnaL).Style.Border.BottomBorderColor = XLColor.Black;
                        worksheet2.Cell(filaL, columnaL).Style.Border.TopBorderColor = XLColor.Black;

                        worksheet2.Cell(filaL, columnaL).Style.Font.FontColor = XLColor.Black;
                        worksheet2.Cell(filaL, columnaL).Style.Font.Bold = true;
                        worksheet2.Cell(filaL, columnaL).Style.Font.FontSize = 16;

                        //
                        worksheet2.Cell(filaL, columnaL + 1).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
                        worksheet2.Cell(filaL, columnaL + 1).Style.Border.TopBorder = XLBorderStyleValues.Thick;
                        worksheet2.Cell(filaL, columnaL + 1).Style.Border.BottomBorderColor = XLColor.Black;
                        worksheet2.Cell(filaL, columnaL + 1).Style.Border.TopBorderColor = XLColor.Black;

                        worksheet2.Cell(filaL, columnaL + 1).Style.Font.FontColor = XLColor.Black;
                        worksheet2.Cell(filaL, columnaL + 1).Style.Font.Bold = true;
                        worksheet2.Cell(filaL, columnaL + 1).Style.Font.FontSize = 16;
                        //
                        worksheet2.Cell(filaL, columnaL + 2).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
                        worksheet2.Cell(filaL, columnaL + 2).Style.Border.TopBorder = XLBorderStyleValues.Thick;
                        worksheet2.Cell(filaL, columnaL + 2).Style.Border.BottomBorderColor = XLColor.Black;
                        worksheet2.Cell(filaL, columnaL + 2).Style.Border.TopBorderColor = XLColor.Black;

                        worksheet2.Cell(filaL, columnaL + 2).Style.Font.FontColor = XLColor.Black;
                        worksheet2.Cell(filaL, columnaL + 2).Style.Font.Bold = true;
                        worksheet2.Cell(filaL, columnaL + 2).Style.Font.FontSize = 16;



                        IsColumnTotalL = 0;
                    }
                    else
                    {
                        worksheet2.Cell(filaL, columnaL).Value = campo;


                        worksheet2.Cell(filaL, columnaL + 1).Value = campo2;


                        worksheet2.Cell(filaL, columnaL + 2).Value = campo3;
                    }

                    IsColumnTotalL = 0;
                    filaL += 1;

                }


                //

                int IsColumnTotal = 0;
                decimal suma_Subtotal = 0;
                decimal suma_Iva = 0;
                decimal suma_total = 0;
                filaL = 4;
                columnaL = 5;
                for (int i = 0; i < DISEÑO.Rows.Count; i++)
                {
                    for (int y = 3; y < DISEÑO.Columns.Count; y += 3) //foreach (DataColumn item in DISEÑO.Columns)
                    {
                        string campo;
                        string campo2;
                        string campo3;


                        if (DISEÑO.Rows[i][0].ToString() == "TOTAL" ||
                            DISEÑO.Rows[i][0].ToString() == "TOTAL FINAL")
                        {
                            IsColumnTotal = 1;

                        }

                        campo = DISEÑO.Rows[i][y].ToString();
                        campo2 = DISEÑO.Rows[i][y + 1].ToString();
                        campo3 = DISEÑO.Rows[i][y + 2].ToString();

                        suma_Subtotal += decimal.Parse(campo);
                        suma_Iva += decimal.Parse(campo2);
                        suma_total += decimal.Parse(campo3);


                    }

                    if (IsColumnTotal == 1)
                    {
                        worksheet2.Cell(filaL, columnaL).Style.NumberFormat.Format = "#,##0.00";
                        worksheet2.Cell(filaL, columnaL).DataType = XLDataType.Number;
                        worksheet2.Cell(filaL, columnaL).Value = suma_Subtotal;

                        worksheet2.Cell(filaL, columnaL + 1).Style.NumberFormat.Format = "#,##0.00";
                        worksheet2.Cell(filaL, columnaL + 1).DataType = XLDataType.Number;
                        worksheet2.Cell(filaL, columnaL + 1).Value = suma_Iva;

                        worksheet2.Cell(filaL, columnaL + 2).Style.NumberFormat.Format = "#,##0.00";
                        worksheet2.Cell(filaL, columnaL + 2).DataType = XLDataType.Number;
                        worksheet2.Cell(filaL, columnaL + 2).Value = suma_total;

                        worksheet2.Cell(filaL, columnaL).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
                        worksheet2.Cell(filaL, columnaL).Style.Border.TopBorder = XLBorderStyleValues.Thick;
                        worksheet2.Cell(filaL, columnaL).Style.Border.BottomBorderColor = XLColor.Black;
                        worksheet2.Cell(filaL, columnaL).Style.Border.TopBorderColor = XLColor.Black;

                        worksheet2.Cell(filaL, columnaL).Style.Font.FontColor = XLColor.Black;
                        worksheet2.Cell(filaL, columnaL).Style.Font.Bold = true;
                        worksheet2.Cell(filaL, columnaL).Style.Font.FontSize = 16;

                        //
                        worksheet2.Cell(filaL, columnaL + 1).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
                        worksheet2.Cell(filaL, columnaL + 1).Style.Border.TopBorder = XLBorderStyleValues.Thick;
                        worksheet2.Cell(filaL, columnaL + 1).Style.Border.BottomBorderColor = XLColor.Black;
                        worksheet2.Cell(filaL, columnaL + 1).Style.Border.TopBorderColor = XLColor.Black;

                        worksheet2.Cell(filaL, columnaL + 1).Style.Font.FontColor = XLColor.Black;
                        worksheet2.Cell(filaL, columnaL + 1).Style.Font.Bold = true;
                        worksheet2.Cell(filaL, columnaL + 1).Style.Font.FontSize = 16;
                        //
                        worksheet2.Cell(filaL, columnaL + 2).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
                        worksheet2.Cell(filaL, columnaL + 2).Style.Border.TopBorder = XLBorderStyleValues.Thick;
                        worksheet2.Cell(filaL, columnaL + 2).Style.Border.BottomBorderColor = XLColor.Black;
                        worksheet2.Cell(filaL, columnaL + 2).Style.Border.TopBorderColor = XLColor.Black;

                        worksheet2.Cell(filaL, columnaL + 2).Style.Font.FontColor = XLColor.Black;
                        worksheet2.Cell(filaL, columnaL + 2).Style.Font.Bold = true;
                        worksheet2.Cell(filaL, columnaL + 2).Style.Font.FontSize = 16;



                        IsColumnTotal = 0;
                    }
                    else
                    {
                        worksheet2.Cell(filaL, columnaL).Style.NumberFormat.Format = "#,##0.00";
                        worksheet2.Cell(filaL, columnaL).DataType = XLDataType.Number;
                        worksheet2.Cell(filaL, columnaL).Value = suma_Subtotal;

                        worksheet2.Cell(filaL, columnaL + 1).Style.NumberFormat.Format = "#,##0.00";
                        worksheet2.Cell(filaL, columnaL + 1).DataType = XLDataType.Number;
                        worksheet2.Cell(filaL, columnaL + 1).Value = suma_Iva;

                        worksheet2.Cell(filaL, columnaL + 2).Style.NumberFormat.Format = "#,##0.00";
                        worksheet2.Cell(filaL, columnaL + 2).DataType = XLDataType.Number;
                        worksheet2.Cell(filaL, columnaL + 2).Value = suma_total;
                    }


                    filaL += 1;
                    suma_Subtotal = 0;
                    suma_Iva = 0;
                    suma_total = 0;
                }

                ///


                worksheet2.Columns().AdjustToContents();








                workbook.SaveAs("c:\\SEMP2013\\NotasGlobalesSucursales.xlsx");

                MessageBox.Show("Realizado");


            }



        }


        private void treeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {





        }

        private void btnPrestamos_Click(object sender, EventArgs e)
        {
            seleccion = "Prestamo";
            CajasSeleccionadas();
            Verificacion();

        }
    }
}

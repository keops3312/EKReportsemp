

namespace EKReportsemp.WinForms.Views
{

    using System;
    using System.Configuration;
    using System.Data;
    using System.Windows.Forms;
    using ClosedXML.Excel;
    using DevComponents.DotNetBar;
    using EKReportsemp.WinForms.Classes;



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
           
        }

        private void PanelV2Form_Load(object sender, EventArgs e)
        {
            int i = 0;
            int loc = 0;
           
           
            foreach  (DataRow item in Empresa.Rows)
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
                    tn.Checked =true;
                 
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


            if (selcajas.Rows.Count==0)
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
            BeginReport(date1.Value,date2.Value,"Prestamos");
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
            int nodosSecundarios=0;
            int nodoTerciario = 0;
            string cajaLeyenda;
            for (int i = 0; i < nodosPrincipales; i++)
            {
                nodosSecundarios= nodes[i].GetNodeCount(false);

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

                            selcajas.Rows.Add(cajaLeyenda,result.Rows[0][2].ToString(), result.Rows[0][3].ToString());
                        }

                    }


                    
                }

                
            }

            dataGridView1.DataSource = selcajas;//Esto essolo para probar despues eliminar

            
        }



        private void BeginReport(DateTime fechaInicio, DateTime fechaFinal,string tipo)
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




            if (tipo == "Prestamos")
            {

                resumen.Clear();

                foreach (DataRow item in selcajas.Rows)
                {
                    string caja, database, emp, bd, localidad, cajaletra;

                    caja = item[2].ToString();//TLX11
                    cajaletra = item[0].ToString();//CIS-TLX1-1
                    database = item[1].ToString();
                    bd = item[1].ToString();//SEMP2013_TLXX

                    emp = item[1].ToString();//Monte RosHAY QUE SACARLA

                    localidad = item[1].ToString().Substring(9);//TLA_3

                

                    if (radioTodos.Checked == true)//UNIFICAR LOS RESULTADOS POR
                    {
                       
                        a = item[1].ToString();//SEMP2013_NAU_1

                        if (a != b)
                        {
                            b = item[1].ToString();
                            //Y agregamos nuestra primera informacion de sucursal
                            DISEÑO.Columns.Add("Prestamos_" + bd.Substring(9) + "");

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
                                                fechaInicio, fechaFinal, conn, unifica, porSemana, caja, emp, localidad, cajaletra);


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
                    worksheet.Cell(6,p).Value = nombrecolumna;
                    worksheet.Cell(6, p).Style.Fill.SetBackgroundColor(XLColor.Navy);
                    worksheet.Cell(6, p).Style.Font.FontColor = XLColor.White;
                    worksheet.Cell(6, p).Style.Font.Bold=true;
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

        private void AcumuladoTodosPrestamo()
        {
            return;
        }


        private void treeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

          

          
            
        }
    }
}

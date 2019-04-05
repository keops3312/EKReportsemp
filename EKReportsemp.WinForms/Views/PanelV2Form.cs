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
    public partial class PanelV2Form : Office2007Form
    {


        DataTable Empresa;
        DataTable selcajas; 
        

        BuscarLocalidad buscarLocalidad;

        public PanelV2Form(DataTable result)
        {
            InitializeComponent();
            buscarLocalidad = new BuscarLocalidad();
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
            //int caja = 0;
           
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




            //int i = 0;
            //foreach (DataRow item in Empresa.Rows)
            //{
            //    advTree1.CheckedNodes = true;
            //    advTree1.Nodes.Add(item[0].ToString());
            //    //Le agrego sus sucursales
            //    DataTable listaSucursales = new DataTable();
            //    listaSucursales = buscarLocalidad.BuscarSucursales(item[0].ToString());

            //    foreach (DataRow suc in listaSucursales.Rows)
            //    {
            //        advTree1.Nodes[i].Nodes.Add(suc[0].ToString());

            //    }
            //    i = i + 1;

            //}


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

            //unifica = (checkUnificar.Checked == true) ? 1 : 2;
            //porSemana = (checkRangosSemana.Checked == true) ? 1 : 2;


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

            dataGridView1.DataSource = selcajas;

            
        }



        private void treeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

          

          
            
        }
    }
}

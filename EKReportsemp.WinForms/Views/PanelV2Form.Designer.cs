namespace EKReportsemp.WinForms.Views
{
    partial class PanelV2Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PanelV2Form));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.checkBoxX1 = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.btnInteres = new DevComponents.DotNetBar.ButtonX();
            this.btnRemision = new DevComponents.DotNetBar.ButtonX();
            this.btnPrestamos = new DevComponents.DotNetBar.ButtonX();
            this.btnClose = new DevComponents.DotNetBar.ButtonX();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.circularProgress1 = new DevComponents.DotNetBar.Controls.CircularProgress();
            this.checkVentas = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtPorcentaje = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txtIva = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.date2 = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.date1 = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.radioEmpresa = new System.Windows.Forms.RadioButton();
            this.radioSucursal = new System.Windows.Forms.RadioButton();
            this.radioCaja = new System.Windows.Forms.RadioButton();
            this.radioTodos = new System.Windows.Forms.RadioButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.checkSemana = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.BtnRemisionesGeneral = new DevComponents.DotNetBar.ButtonX();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker3 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker4 = new System.ComponentModel.BackgroundWorker();
            this.groupPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.date2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.date1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(24, 55);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(357, 603);
            this.treeView1.TabIndex = 5;
            this.treeView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCheck);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "empresaC.fw.png");
            this.imageList1.Images.SetKeyName(1, "localC.fw.png");
            this.imageList1.Images.SetKeyName(2, "cajaC.fw.png");
            // 
            // checkBoxX1
            // 
            // 
            // 
            // 
            this.checkBoxX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.checkBoxX1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxX1.Location = new System.Drawing.Point(24, 26);
            this.checkBoxX1.Name = "checkBoxX1";
            this.checkBoxX1.Size = new System.Drawing.Size(294, 23);
            this.checkBoxX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.checkBoxX1.TabIndex = 7;
            this.checkBoxX1.Text = "Marcas Todas las Empresas";
            this.checkBoxX1.CheckedChanged += new System.EventHandler(this.checkBoxX1_CheckedChanged);
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerColorTint = System.Drawing.Color.SteelBlue;
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2007VistaGlass;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154))))));
            // 
            // btnInteres
            // 
            this.btnInteres.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnInteres.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnInteres.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnInteres.Image = ((System.Drawing.Image)(resources.GetObject("btnInteres.Image")));
            this.btnInteres.ImageFixedSize = new System.Drawing.Size(56, 56);
            this.btnInteres.Location = new System.Drawing.Point(34, 41);
            this.btnInteres.Name = "btnInteres";
            this.btnInteres.Size = new System.Drawing.Size(249, 68);
            this.btnInteres.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnInteres.TabIndex = 8;
            this.btnInteres.Text = "Reporte de Operaciones de Interes";
            this.btnInteres.Click += new System.EventHandler(this.btnInteres_Click);
            // 
            // btnRemision
            // 
            this.btnRemision.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRemision.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnRemision.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnRemision.Image = ((System.Drawing.Image)(resources.GetObject("btnRemision.Image")));
            this.btnRemision.ImageFixedSize = new System.Drawing.Size(56, 56);
            this.btnRemision.Location = new System.Drawing.Point(34, 145);
            this.btnRemision.Name = "btnRemision";
            this.btnRemision.Size = new System.Drawing.Size(249, 68);
            this.btnRemision.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnRemision.TabIndex = 9;
            this.btnRemision.Text = "Reporte de Operaciones de Ventas";
            this.btnRemision.Click += new System.EventHandler(this.btnRemision_Click);
            // 
            // btnPrestamos
            // 
            this.btnPrestamos.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPrestamos.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnPrestamos.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPrestamos.Image = ((System.Drawing.Image)(resources.GetObject("btnPrestamos.Image")));
            this.btnPrestamos.ImageFixedSize = new System.Drawing.Size(56, 56);
            this.btnPrestamos.Location = new System.Drawing.Point(34, 239);
            this.btnPrestamos.Name = "btnPrestamos";
            this.btnPrestamos.Size = new System.Drawing.Size(249, 68);
            this.btnPrestamos.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPrestamos.TabIndex = 10;
            this.btnPrestamos.Text = "Reporte de Operaciones de Prestamos";
            this.btnPrestamos.Click += new System.EventHandler(this.btnPrestamos_Click);
            // 
            // btnClose
            // 
            this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageFixedSize = new System.Drawing.Size(50, 50);
            this.btnClose.Location = new System.Drawing.Point(221, 523);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(89, 49);
            this.btnClose.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnClose.TabIndex = 12;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupPanel1
            // 
            this.groupPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.circularProgress1);
            this.groupPanel1.Controls.Add(this.checkVentas);
            this.groupPanel1.Controls.Add(this.label1);
            this.groupPanel1.Controls.Add(this.labelX7);
            this.groupPanel1.Controls.Add(this.labelX6);
            this.groupPanel1.Controls.Add(this.labelX5);
            this.groupPanel1.Controls.Add(this.pictureBox1);
            this.groupPanel1.Controls.Add(this.txtPorcentaje);
            this.groupPanel1.Controls.Add(this.labelX2);
            this.groupPanel1.Controls.Add(this.txtIva);
            this.groupPanel1.Controls.Add(this.date2);
            this.groupPanel1.Controls.Add(this.date1);
            this.groupPanel1.Controls.Add(this.radioEmpresa);
            this.groupPanel1.Controls.Add(this.radioSucursal);
            this.groupPanel1.Controls.Add(this.radioCaja);
            this.groupPanel1.Controls.Add(this.radioTodos);
            this.groupPanel1.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupPanel1.Location = new System.Drawing.Point(399, 42);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(464, 616);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel1.TabIndex = 13;
            this.groupPanel1.Text = "Configuracion de Reporte";
            // 
            // circularProgress1
            // 
            // 
            // 
            // 
            this.circularProgress1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.circularProgress1.Location = new System.Drawing.Point(133, 173);
            this.circularProgress1.Name = "circularProgress1";
            this.circularProgress1.ProgressBarType = DevComponents.DotNetBar.eCircularProgressType.Dot;
            this.circularProgress1.Size = new System.Drawing.Size(146, 122);
            this.circularProgress1.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this.circularProgress1.TabIndex = 15;
            // 
            // checkVentas
            // 
            this.checkVentas.AutoSize = true;
            this.checkVentas.Location = new System.Drawing.Point(16, 264);
            this.checkVentas.Name = "checkVentas";
            this.checkVentas.Size = new System.Drawing.Size(139, 24);
            this.checkVentas.TabIndex = 37;
            this.checkVentas.Text = "Notas de Venta";
            this.checkVentas.UseVisualStyleBackColor = true;
            this.checkVentas.CheckedChanged += new System.EventHandler(this.checkVentas_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 291);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(202, 20);
            this.label1.TabIndex = 36;
            this.label1.Text = "Para Notas de Remision";
            // 
            // labelX7
            // 
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX7.Location = new System.Drawing.Point(17, 327);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(421, 19);
            this.labelX7.TabIndex = 35;
            this.labelX7.Text = "Ingrese el valor del Iva y el % a tomar del total de las ventas";
            // 
            // labelX6
            // 
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX6.Location = new System.Drawing.Point(26, 413);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(173, 23);
            this.labelX6.TabIndex = 34;
            this.labelX6.Text = "Porcentaje de la Venta";
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX5.Location = new System.Drawing.Point(102, 366);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(97, 23);
            this.labelX5.TabIndex = 33;
            this.labelX5.Text = "Valor de Iva";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(298, 39);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(128, 128);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // txtPorcentaje
            // 
            // 
            // 
            // 
            this.txtPorcentaje.Border.Class = "TextBoxBorder";
            this.txtPorcentaje.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPorcentaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPorcentaje.Location = new System.Drawing.Point(219, 410);
            this.txtPorcentaje.Name = "txtPorcentaje";
            this.txtPorcentaje.PreventEnterBeep = true;
            this.txtPorcentaje.Size = new System.Drawing.Size(60, 26);
            this.txtPorcentaje.TabIndex = 32;
            this.txtPorcentaje.Text = "2.5";
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(27, 507);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(419, 23);
            this.labelX2.TabIndex = 7;
            this.labelX2.Text = "Seleccione Rango de periodo";
            this.labelX2.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // txtIva
            // 
            // 
            // 
            // 
            this.txtIva.Border.Class = "TextBoxBorder";
            this.txtIva.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtIva.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIva.Location = new System.Drawing.Point(219, 363);
            this.txtIva.Name = "txtIva";
            this.txtIva.PreventEnterBeep = true;
            this.txtIva.Size = new System.Drawing.Size(55, 26);
            this.txtIva.TabIndex = 31;
            this.txtIva.Text = ".16";
            // 
            // date2
            // 
            this.date2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            // 
            // 
            // 
            this.date2.BackgroundStyle.Class = "DateTimeInputBackground";
            this.date2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.date2.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.date2.ButtonDropDown.Visible = true;
            this.date2.IsPopupCalendarOpen = false;
            this.date2.Location = new System.Drawing.Point(243, 536);
            // 
            // 
            // 
            // 
            // 
            // 
            this.date2.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.date2.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.date2.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.date2.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.date2.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.date2.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.date2.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.date2.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.date2.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.date2.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.date2.MonthCalendar.DisplayMonth = new System.DateTime(2019, 4, 1, 0, 0, 0, 0);
            // 
            // 
            // 
            this.date2.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.date2.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.date2.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.date2.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.date2.MonthCalendar.TodayButtonVisible = true;
            this.date2.Name = "date2";
            this.date2.Size = new System.Drawing.Size(200, 26);
            this.date2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.date2.TabIndex = 6;
            // 
            // date1
            // 
            this.date1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            // 
            // 
            // 
            this.date1.BackgroundStyle.Class = "DateTimeInputBackground";
            this.date1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.date1.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.date1.ButtonDropDown.Visible = true;
            this.date1.IsPopupCalendarOpen = false;
            this.date1.Location = new System.Drawing.Point(27, 536);
            // 
            // 
            // 
            // 
            // 
            // 
            this.date1.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.date1.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.date1.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.date1.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.date1.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.date1.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.date1.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.date1.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.date1.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.date1.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.date1.MonthCalendar.DisplayMonth = new System.DateTime(2019, 4, 1, 0, 0, 0, 0);
            // 
            // 
            // 
            this.date1.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.date1.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.date1.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.date1.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.date1.MonthCalendar.TodayButtonVisible = true;
            this.date1.Name = "date1";
            this.date1.Size = new System.Drawing.Size(200, 26);
            this.date1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.date1.TabIndex = 5;
            // 
            // radioEmpresa
            // 
            this.radioEmpresa.AutoSize = true;
            this.radioEmpresa.Location = new System.Drawing.Point(58, 193);
            this.radioEmpresa.Name = "radioEmpresa";
            this.radioEmpresa.Size = new System.Drawing.Size(172, 24);
            this.radioEmpresa.TabIndex = 3;
            this.radioEmpresa.TabStop = true;
            this.radioEmpresa.Text = "Detalle por Empresa";
            this.radioEmpresa.UseVisualStyleBackColor = true;
            // 
            // radioSucursal
            // 
            this.radioSucursal.AutoSize = true;
            this.radioSucursal.Location = new System.Drawing.Point(58, 143);
            this.radioSucursal.Name = "radioSucursal";
            this.radioSucursal.Size = new System.Drawing.Size(170, 24);
            this.radioSucursal.TabIndex = 2;
            this.radioSucursal.TabStop = true;
            this.radioSucursal.Text = "Detalle por Sucursal";
            this.radioSucursal.UseVisualStyleBackColor = true;
            // 
            // radioCaja
            // 
            this.radioCaja.AutoSize = true;
            this.radioCaja.Location = new System.Drawing.Point(58, 93);
            this.radioCaja.Name = "radioCaja";
            this.radioCaja.Size = new System.Drawing.Size(140, 24);
            this.radioCaja.TabIndex = 1;
            this.radioCaja.TabStop = true;
            this.radioCaja.Text = "Detalle por Caja";
            this.radioCaja.UseVisualStyleBackColor = true;
            // 
            // radioTodos
            // 
            this.radioTodos.AutoSize = true;
            this.radioTodos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Navy;
            this.radioTodos.Location = new System.Drawing.Point(58, 43);
            this.radioTodos.Name = "radioTodos";
            this.radioTodos.Size = new System.Drawing.Size(155, 24);
            this.radioTodos.TabIndex = 0;
            this.radioTodos.TabStop = true;
            this.radioTodos.Text = "Acumulado Todos";
            this.radioTodos.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(451, 11);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(51, 24);
            this.dataGridView1.TabIndex = 9;
            // 
            // checkSemana
            // 
            // 
            // 
            // 
            this.checkSemana.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.checkSemana.Location = new System.Drawing.Point(418, 12);
            this.checkSemana.Name = "checkSemana";
            this.checkSemana.Size = new System.Drawing.Size(27, 23);
            this.checkSemana.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.checkSemana.TabIndex = 4;
            this.checkSemana.Text = "Acumular por Semana";
            this.checkSemana.Visible = false;
            // 
            // groupPanel2
            // 
            this.groupPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel2.Controls.Add(this.BtnRemisionesGeneral);
            this.groupPanel2.Controls.Add(this.btnInteres);
            this.groupPanel2.Controls.Add(this.btnRemision);
            this.groupPanel2.Controls.Add(this.btnClose);
            this.groupPanel2.Controls.Add(this.btnPrestamos);
            this.groupPanel2.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupPanel2.Location = new System.Drawing.Point(872, 46);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new System.Drawing.Size(320, 612);
            // 
            // 
            // 
            this.groupPanel2.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel2.Style.BackColorGradientAngle = 90;
            this.groupPanel2.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel2.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderBottomWidth = 1;
            this.groupPanel2.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel2.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderLeftWidth = 1;
            this.groupPanel2.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderRightWidth = 1;
            this.groupPanel2.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderTopWidth = 1;
            this.groupPanel2.Style.CornerDiameter = 4;
            this.groupPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel2.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel2.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel2.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel2.TabIndex = 14;
            this.groupPanel2.Text = "Seleccione Reporte";
            // 
            // BtnRemisionesGeneral
            // 
            this.BtnRemisionesGeneral.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.BtnRemisionesGeneral.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BtnRemisionesGeneral.Image = global::EKReportsemp.WinForms.Properties.Resources.cajaC_fw;
            this.BtnRemisionesGeneral.Location = new System.Drawing.Point(34, 338);
            this.BtnRemisionesGeneral.Name = "BtnRemisionesGeneral";
            this.BtnRemisionesGeneral.Size = new System.Drawing.Size(249, 68);
            this.BtnRemisionesGeneral.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BtnRemisionesGeneral.TabIndex = 14;
            this.BtnRemisionesGeneral.Text = "Remisiones por Tipo de prenda";
            this.BtnRemisionesGeneral.Click += new System.EventHandler(this.BtnRemisionesGeneral_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.WorkerReportsProgress = true;
            this.backgroundWorker2.WorkerSupportsCancellation = true;
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            this.backgroundWorker2.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker2_ProgressChanged);
            this.backgroundWorker2.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker2_RunWorkerCompleted);
            // 
            // backgroundWorker3
            // 
            this.backgroundWorker3.WorkerReportsProgress = true;
            this.backgroundWorker3.WorkerSupportsCancellation = true;
            this.backgroundWorker3.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker3_DoWork);
            this.backgroundWorker3.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker3_ProgressChanged);
            this.backgroundWorker3.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker3_RunWorkerCompleted);
            // 
            // backgroundWorker4
            // 
            this.backgroundWorker4.WorkerReportsProgress = true;
            this.backgroundWorker4.WorkerSupportsCancellation = true;
            this.backgroundWorker4.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker4_DoWork);
            this.backgroundWorker4.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker4_ProgressChanged);
            this.backgroundWorker4.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker4_RunWorkerCompleted);
            // 
            // PanelV2Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1205, 670);
            this.Controls.Add(this.groupPanel2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupPanel1);
            this.Controls.Add(this.checkBoxX1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.checkSemana);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1221, 709);
            this.Name = "PanelV2Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Opciones y Configuracion de reporte";
            this.Load += new System.EventHandler(this.PanelV2Form_Load);
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.date2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.date1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TreeView treeView1;
        private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxX1;
        private System.Windows.Forms.ImageList imageList1;
        private DevComponents.DotNetBar.StyleManager styleManager1;
        private DevComponents.DotNetBar.ButtonX btnInteres;
        private DevComponents.DotNetBar.ButtonX btnRemision;
        private DevComponents.DotNetBar.ButtonX btnPrestamos;
        private DevComponents.DotNetBar.ButtonX btnClose;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput date2;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput date1;
        private DevComponents.DotNetBar.Controls.CheckBoxX checkSemana;
        private System.Windows.Forms.RadioButton radioEmpresa;
        private System.Windows.Forms.RadioButton radioSucursal;
        private System.Windows.Forms.RadioButton radioCaja;
        private System.Windows.Forms.RadioButton radioTodos;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPorcentaje;
        private DevComponents.DotNetBar.Controls.TextBoxX txtIva;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkVentas;
        private DevComponents.DotNetBar.Controls.CircularProgress circularProgress1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.ComponentModel.BackgroundWorker backgroundWorker3;
        private DevComponents.DotNetBar.ButtonX BtnRemisionesGeneral;
        private System.ComponentModel.BackgroundWorker backgroundWorker4;
    }
}
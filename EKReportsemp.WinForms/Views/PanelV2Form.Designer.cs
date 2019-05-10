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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.date2 = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.date1 = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.checkSemana = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.radioEmpresa = new System.Windows.Forms.RadioButton();
            this.radioSucursal = new System.Windows.Forms.RadioButton();
            this.radioCaja = new System.Windows.Forms.RadioButton();
            this.radioTodos = new System.Windows.Forms.RadioButton();
            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.groupPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.date2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.date1)).BeginInit();
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
            this.treeView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseDoubleClick);
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
            this.btnRemision.Location = new System.Drawing.Point(34, 175);
            this.btnRemision.Name = "btnRemision";
            this.btnRemision.Size = new System.Drawing.Size(249, 68);
            this.btnRemision.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnRemision.TabIndex = 9;
            this.btnRemision.Text = "Reporte de Operaciones de Notas de Remision";
            this.btnRemision.Click += new System.EventHandler(this.btnRemision_Click);
            // 
            // btnPrestamos
            // 
            this.btnPrestamos.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPrestamos.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnPrestamos.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPrestamos.Image = ((System.Drawing.Image)(resources.GetObject("btnPrestamos.Image")));
            this.btnPrestamos.ImageFixedSize = new System.Drawing.Size(56, 56);
            this.btnPrestamos.Location = new System.Drawing.Point(34, 314);
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
            this.groupPanel1.Controls.Add(this.dataGridView1);
            this.groupPanel1.Controls.Add(this.pictureBox1);
            this.groupPanel1.Controls.Add(this.labelX2);
            this.groupPanel1.Controls.Add(this.date2);
            this.groupPanel1.Controls.Add(this.date1);
            this.groupPanel1.Controls.Add(this.checkSemana);
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
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(58, 353);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(368, 130);
            this.dataGridView1.TabIndex = 9;
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
            // checkSemana
            // 
            // 
            // 
            // 
            this.checkSemana.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.checkSemana.Location = new System.Drawing.Point(58, 318);
            this.checkSemana.Name = "checkSemana";
            this.checkSemana.Size = new System.Drawing.Size(184, 23);
            this.checkSemana.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.checkSemana.TabIndex = 4;
            this.checkSemana.Text = "Acumular por Semana";
            this.checkSemana.CheckedChanged += new System.EventHandler(this.checkSemana_CheckedChanged);
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
            // groupPanel2
            // 
            this.groupPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
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
            // PanelV2Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1205, 670);
            this.Controls.Add(this.groupPanel2);
            this.Controls.Add(this.groupPanel1);
            this.Controls.Add(this.checkBoxX1);
            this.Controls.Add(this.treeView1);
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.date2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.date1)).EndInit();
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
    }
}
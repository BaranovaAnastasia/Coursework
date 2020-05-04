using System.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Graph_WinForms
{
    partial class MainWindow
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Build = new System.Windows.Forms.Button();
            this.Open = new System.Windows.Forms.Button();
            this.TopMenu = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UserManualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MovementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ResetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Tools = new System.Windows.Forms.Panel();
            this.ClearButton = new System.Windows.Forms.Button();
            this.CursorButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.VertexButton = new System.Windows.Forms.Button();
            this.EdgeButton = new System.Windows.Forms.Button();
            this.SandpilePalette = new System.Windows.Forms.DataGridView();
            this.RandomGraph = new System.Windows.Forms.Button();
            this.TimeTextBox = new System.Windows.Forms.TextBox();
            this.SandpileLabel = new System.Windows.Forms.Label();
            this.RandomAddingLabel = new System.Windows.Forms.Label();
            this.RandomAddingCheckBox = new System.Windows.Forms.CheckBox();
            this.SandpilePanel = new System.Windows.Forms.Panel();
            this.RadiusTrackBar = new System.Windows.Forms.TrackBar();
            this.RadiusLabel = new System.Windows.Forms.Label();
            this.SquareLattice = new System.Windows.Forms.Button();
            this.TriangleLattice = new System.Windows.Forms.Button();
            this.DrawingSurface = new System.Windows.Forms.PictureBox();
            this.CursorToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.VertexToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.ArcToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.EraserToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.ClearAllToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.RadiusValueLabel = new System.Windows.Forms.Label();
            this.ModePage = new System.Windows.Forms.TabPage();
            this.SandpileChartType2 = new System.Windows.Forms.CheckBox();
            this.SandpileChartType1 = new System.Windows.Forms.CheckBox();
            this.ActionsLabel = new System.Windows.Forms.Label();
            this.SpeedNumeric = new System.Windows.Forms.NumericUpDown();
            this.SpeedLabel = new System.Windows.Forms.Label();
            this.ModelingTypeLabel = new System.Windows.Forms.Label();
            this.SaveGifCheckBox = new System.Windows.Forms.CheckBox();
            this.ChartCheckBox = new System.Windows.Forms.CheckBox();
            this.AnimationCheckBox = new System.Windows.Forms.CheckBox();
            this.SandpileTypeCheckBox = new System.Windows.Forms.CheckBox();
            this.BasicTypeCheckBox = new System.Windows.Forms.CheckBox();
            this.ParamsPage = new System.Windows.Forms.TabPage();
            this.GridParameters = new System.Windows.Forms.DataGridView();
            this.ParametersLegendLabel = new System.Windows.Forms.Label();
            this.ParamsLabel = new System.Windows.Forms.Label();
            this.AdjacencyPage = new System.Windows.Forms.TabPage();
            this.ArcLengthLabel = new System.Windows.Forms.Label();
            this.GridAdjacencyMatrix = new System.Windows.Forms.DataGridView();
            this.ArcLengthContainer = new System.Windows.Forms.SplitContainer();
            this.ArcName = new System.Windows.Forms.ComboBox();
            this.ArcLength = new System.Windows.Forms.TextBox();
            this.AdjacencyMatrixLabel = new System.Windows.Forms.Label();
            this.OkLength = new System.Windows.Forms.Button();
            this.AppParameters = new System.Windows.Forms.TabControl();
            this.TopMenu.SuspendLayout();
            this.Tools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SandpilePalette)).BeginInit();
            this.SandpilePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RadiusTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DrawingSurface)).BeginInit();
            this.ModePage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SpeedNumeric)).BeginInit();
            this.ParamsPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridParameters)).BeginInit();
            this.AdjacencyPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridAdjacencyMatrix)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ArcLengthContainer)).BeginInit();
            this.ArcLengthContainer.Panel1.SuspendLayout();
            this.ArcLengthContainer.Panel2.SuspendLayout();
            this.ArcLengthContainer.SuspendLayout();
            this.AppParameters.SuspendLayout();
            this.SuspendLayout();
            // 
            // Build
            // 
            this.Build.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Build.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.Build.Location = new System.Drawing.Point(426, 350);
            this.Build.Name = "Build";
            this.Build.Size = new System.Drawing.Size(400, 62);
            this.Build.TabIndex = 0;
            this.Build.Text = "Build a new graph";
            this.Build.UseVisualStyleBackColor = true;
            this.Build.Click += new System.EventHandler(this.Build_Click);
            // 
            // Open
            // 
            this.Open.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Open.Font = new System.Drawing.Font("Segoe UI", 15.75F);
            this.Open.Location = new System.Drawing.Point(426, 490);
            this.Open.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Open.Name = "Open";
            this.Open.Size = new System.Drawing.Size(400, 62);
            this.Open.TabIndex = 1;
            this.Open.Text = "Open graph";
            this.Open.UseVisualStyleBackColor = true;
            this.Open.Click += new System.EventHandler(this.Open_Click);
            // 
            // TopMenu
            // 
            this.TopMenu.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.TopMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.TopMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.UserManualToolStripMenuItem,
            this.AboutToolStripMenuItem,
            this.MainMenuToolStripMenuItem,
            this.MovementToolStripMenuItem,
            this.StopToolStripMenuItem,
            this.ResetToolStripMenuItem});
            this.TopMenu.Location = new System.Drawing.Point(0, 0);
            this.TopMenu.Name = "TopMenu";
            this.TopMenu.Size = new System.Drawing.Size(1236, 33);
            this.TopMenu.TabIndex = 4;
            this.TopMenu.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newProjectToolStripMenuItem,
            this.openProjectToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(54, 29);
            this.FileToolStripMenuItem.Text = "File";
            // 
            // newProjectToolStripMenuItem
            // 
            this.newProjectToolStripMenuItem.Name = "newProjectToolStripMenuItem";
            this.newProjectToolStripMenuItem.Size = new System.Drawing.Size(217, 34);
            this.newProjectToolStripMenuItem.Text = "New Project";
            this.newProjectToolStripMenuItem.Click += new System.EventHandler(this.NewProjectToolStripMenuItem_Click);
            // 
            // openProjectToolStripMenuItem
            // 
            this.openProjectToolStripMenuItem.Name = "openProjectToolStripMenuItem";
            this.openProjectToolStripMenuItem.Size = new System.Drawing.Size(217, 34);
            this.openProjectToolStripMenuItem.Text = "Open Project";
            this.openProjectToolStripMenuItem.Click += new System.EventHandler(this.OpenProjectToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dataToolStripMenuItem,
            this.saveImageToolStripMenuItem,
            this.saveAllToolStripMenuItem});
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(217, 34);
            this.saveToolStripMenuItem.Text = "Save Graph";
            this.saveToolStripMenuItem.Visible = false;
            // 
            // dataToolStripMenuItem
            // 
            this.dataToolStripMenuItem.Name = "dataToolStripMenuItem";
            this.dataToolStripMenuItem.Size = new System.Drawing.Size(206, 34);
            this.dataToolStripMenuItem.Text = "Save Data";
            this.dataToolStripMenuItem.Click += new System.EventHandler(this.DataToolStripMenuItem_Click);
            // 
            // saveImageToolStripMenuItem
            // 
            this.saveImageToolStripMenuItem.Name = "saveImageToolStripMenuItem";
            this.saveImageToolStripMenuItem.Size = new System.Drawing.Size(206, 34);
            this.saveImageToolStripMenuItem.Text = "Save Image";
            this.saveImageToolStripMenuItem.Click += new System.EventHandler(this.SaveImageToolStripMenuItem_Click);
            // 
            // saveAllToolStripMenuItem
            // 
            this.saveAllToolStripMenuItem.Name = "saveAllToolStripMenuItem";
            this.saveAllToolStripMenuItem.Size = new System.Drawing.Size(206, 34);
            this.saveAllToolStripMenuItem.Text = "Save All";
            this.saveAllToolStripMenuItem.Click += new System.EventHandler(this.SaveAllToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(217, 34);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // UserManualToolStripMenuItem
            // 
            this.UserManualToolStripMenuItem.Name = "UserManualToolStripMenuItem";
            this.UserManualToolStripMenuItem.Size = new System.Drawing.Size(126, 29);
            this.UserManualToolStripMenuItem.Text = "User Manual";
            this.UserManualToolStripMenuItem.Click += new System.EventHandler(this.UserManualToolStripMenuItem_Click);
            // 
            // AboutToolStripMenuItem
            // 
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.Size = new System.Drawing.Size(78, 29);
            this.AboutToolStripMenuItem.Text = "About";
            this.AboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // MainMenuToolStripMenuItem
            // 
            this.MainMenuToolStripMenuItem.Name = "MainMenuToolStripMenuItem";
            this.MainMenuToolStripMenuItem.Size = new System.Drawing.Size(117, 29);
            this.MainMenuToolStripMenuItem.Text = "Main Menu";
            this.MainMenuToolStripMenuItem.Visible = false;
            this.MainMenuToolStripMenuItem.Click += new System.EventHandler(this.MainMenuToolStripMenuItem_Click);
            // 
            // MovementToolStripMenuItem
            // 
            this.MovementToolStripMenuItem.Name = "MovementToolStripMenuItem";
            this.MovementToolStripMenuItem.Size = new System.Drawing.Size(114, 29);
            this.MovementToolStripMenuItem.Text = "Movement";
            this.MovementToolStripMenuItem.Visible = false;
            this.MovementToolStripMenuItem.Click += new System.EventHandler(this.MovementToolStripMenuItem_Click);
            // 
            // StopToolStripMenuItem
            // 
            this.StopToolStripMenuItem.Name = "StopToolStripMenuItem";
            this.StopToolStripMenuItem.Size = new System.Drawing.Size(65, 29);
            this.StopToolStripMenuItem.Text = "Stop";
            this.StopToolStripMenuItem.Visible = false;
            this.StopToolStripMenuItem.Click += new System.EventHandler(this.StopToolStripMenuItem_Click);
            // 
            // ResetToolStripMenuItem
            // 
            this.ResetToolStripMenuItem.Name = "ResetToolStripMenuItem";
            this.ResetToolStripMenuItem.Size = new System.Drawing.Size(70, 29);
            this.ResetToolStripMenuItem.Text = "Reset";
            this.ResetToolStripMenuItem.Visible = false;
            this.ResetToolStripMenuItem.Click += new System.EventHandler(this.ResetToolStripMenuItem_Click);
            // 
            // Tools
            // 
            this.Tools.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Tools.Controls.Add(this.ClearButton);
            this.Tools.Controls.Add(this.CursorButton);
            this.Tools.Controls.Add(this.DeleteButton);
            this.Tools.Controls.Add(this.VertexButton);
            this.Tools.Controls.Add(this.EdgeButton);
            this.Tools.Controls.Add(this.SandpilePalette);
            this.Tools.Location = new System.Drawing.Point(10, 50);
            this.Tools.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Tools.Name = "Tools";
            this.Tools.Size = new System.Drawing.Size(95, 435);
            this.Tools.TabIndex = 7;
            this.Tools.Visible = false;
            // 
            // ClearButton
            // 
            this.ClearButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ClearButton.BackgroundImage")));
            this.ClearButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClearButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ClearButton.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ClearButton.Location = new System.Drawing.Point(10, 350);
            this.ClearButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(75, 75);
            this.ClearButton.TabIndex = 11;
            this.ClearAllToolTip.SetToolTip(this.ClearButton, "Сlick to delete the digraph");
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.EnabledChanged += new System.EventHandler(this.ClearAllButton_EnabledChanged);
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // CursorButton
            // 
            this.CursorButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CursorButton.BackgroundImage")));
            this.CursorButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.CursorButton.Enabled = false;
            this.CursorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CursorButton.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CursorButton.Location = new System.Drawing.Point(15, 15);
            this.CursorButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CursorButton.Name = "CursorButton";
            this.CursorButton.Size = new System.Drawing.Size(65, 65);
            this.CursorButton.TabIndex = 8;
            this.CursorToolTip.SetToolTip(this.CursorButton, "Move vertices using cursor");
            this.CursorButton.UseVisualStyleBackColor = true;
            this.CursorButton.EnabledChanged += new System.EventHandler(this.CursorButton_EnabledChanged);
            this.CursorButton.Click += new System.EventHandler(this.CursorButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("DeleteButton.BackgroundImage")));
            this.DeleteButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.DeleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteButton.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.DeleteButton.Location = new System.Drawing.Point(10, 265);
            this.DeleteButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(75, 75);
            this.DeleteButton.TabIndex = 10;
            this.EraserToolTip.SetToolTip(this.DeleteButton, "Double click on an arc or\nvertex to remove it");
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.EnabledChanged += new System.EventHandler(this.EraserButton_EnabledChanged);
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // VertexButton
            // 
            this.VertexButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("VertexButton.BackgroundImage")));
            this.VertexButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.VertexButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.VertexButton.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.VertexButton.Location = new System.Drawing.Point(10, 95);
            this.VertexButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.VertexButton.Name = "VertexButton";
            this.VertexButton.Size = new System.Drawing.Size(75, 75);
            this.VertexButton.TabIndex = 8;
            this.VertexToolTip.SetToolTip(this.VertexButton, "Click on the drawing surface\nto add a new vertex");
            this.VertexButton.UseVisualStyleBackColor = true;
            this.VertexButton.EnabledChanged += new System.EventHandler(this.VertexButton_EnabledChanged);
            this.VertexButton.Click += new System.EventHandler(this.VertexButton_Click);
            // 
            // EdgeButton
            // 
            this.EdgeButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("EdgeButton.BackgroundImage")));
            this.EdgeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.EdgeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EdgeButton.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.EdgeButton.Location = new System.Drawing.Point(10, 180);
            this.EdgeButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.EdgeButton.Name = "EdgeButton";
            this.EdgeButton.Size = new System.Drawing.Size(75, 75);
            this.EdgeButton.TabIndex = 9;
            this.ArcToolTip.SetToolTip(this.EdgeButton, "Сlick on the starting vertex\nand then on the ending one\nto add a new arc");
            this.EdgeButton.UseVisualStyleBackColor = true;
            this.EdgeButton.EnabledChanged += new System.EventHandler(this.EdgeButton_EnabledChanged);
            this.EdgeButton.Click += new System.EventHandler(this.EdgeButton_Click);
            // 
            // SandpilePalette
            // 
            this.SandpilePalette.AllowUserToAddRows = false;
            this.SandpilePalette.AllowUserToDeleteRows = false;
            this.SandpilePalette.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 7F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SandpilePalette.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.SandpilePalette.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SandpilePalette.Location = new System.Drawing.Point(0, 0);
            this.SandpilePalette.Name = "SandpilePalette";
            this.SandpilePalette.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 7F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SandpilePalette.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.SandpilePalette.RowHeadersWidth = 25;
            this.SandpilePalette.RowTemplate.Height = 28;
            this.SandpilePalette.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.SandpilePalette.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.SandpilePalette.Size = new System.Drawing.Size(93, 433);
            this.SandpilePalette.TabIndex = 36;
            this.SandpilePalette.Visible = false;
            this.SandpilePalette.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.SandpilePalette_RowPrePaint);
            this.SandpilePalette.SelectionChanged += new System.EventHandler(this.SandpilePalette_SelectionChanged);
            // 
            // RandomGraph
            // 
            this.RandomGraph.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RandomGraph.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.RandomGraph.Location = new System.Drawing.Point(426, 418);
            this.RandomGraph.Name = "RandomGraph";
            this.RandomGraph.Size = new System.Drawing.Size(400, 62);
            this.RandomGraph.TabIndex = 10;
            this.RandomGraph.Text = "Create random";
            this.RandomGraph.UseVisualStyleBackColor = true;
            this.RandomGraph.Click += new System.EventHandler(this.RandomGraph_Click);
            // 
            // TimeTextBox
            // 
            this.TimeTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.TimeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TimeTextBox.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.TimeTextBox.Location = new System.Drawing.Point(635, 50);
            this.TimeTextBox.Name = "TimeTextBox";
            this.TimeTextBox.ReadOnly = true;
            this.TimeTextBox.Size = new System.Drawing.Size(260, 29);
            this.TimeTextBox.TabIndex = 27;
            this.TimeTextBox.Text = " Elapsed time, s:  0";
            this.TimeTextBox.Visible = false;
            // 
            // SandpileLabel
            // 
            this.SandpileLabel.AutoSize = true;
            this.SandpileLabel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.SandpileLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SandpileLabel.ForeColor = System.Drawing.SystemColors.Window;
            this.SandpileLabel.Location = new System.Drawing.Point(0, 0);
            this.SandpileLabel.Name = "SandpileLabel";
            this.SandpileLabel.Size = new System.Drawing.Size(360, 25);
            this.SandpileLabel.TabIndex = 28;
            this.SandpileLabel.Text = "Select sink vertices and then click here          ";
            this.SandpileLabel.Click += new System.EventHandler(this.StockLabel_Click);
            // 
            // RandomAddingLabel
            // 
            this.RandomAddingLabel.AutoSize = true;
            this.RandomAddingLabel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.RandomAddingLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RandomAddingLabel.ForeColor = System.Drawing.SystemColors.Window;
            this.RandomAddingLabel.Location = new System.Drawing.Point(0, 30);
            this.RandomAddingLabel.Name = "RandomAddingLabel";
            this.RandomAddingLabel.Size = new System.Drawing.Size(361, 25);
            this.RandomAddingLabel.TabIndex = 29;
            this.RandomAddingLabel.Text = "Or click here to add sand randomly              ";
            this.RandomAddingLabel.Click += new System.EventHandler(this.RandomAddingLabel_Click);
            // 
            // RandomAddingCheckBox
            // 
            this.RandomAddingCheckBox.AutoSize = true;
            this.RandomAddingCheckBox.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RandomAddingCheckBox.ForeColor = System.Drawing.SystemColors.Window;
            this.RandomAddingCheckBox.Location = new System.Drawing.Point(5, 60);
            this.RandomAddingCheckBox.Name = "RandomAddingCheckBox";
            this.RandomAddingCheckBox.Size = new System.Drawing.Size(351, 25);
            this.RandomAddingCheckBox.TabIndex = 30;
            this.RandomAddingCheckBox.Text = "Always add randomly and don\'t ask me again";
            this.RandomAddingCheckBox.UseVisualStyleBackColor = true;
            this.RandomAddingCheckBox.CheckedChanged += new System.EventHandler(this.RandomAddingCheckBox_CheckedChanged);
            // 
            // SandpilePanel
            // 
            this.SandpilePanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.SandpilePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SandpilePanel.Controls.Add(this.SandpileLabel);
            this.SandpilePanel.Controls.Add(this.RandomAddingCheckBox);
            this.SandpilePanel.Controls.Add(this.RandomAddingLabel);
            this.SandpilePanel.Location = new System.Drawing.Point(125, 61);
            this.SandpilePanel.Name = "SandpilePanel";
            this.SandpilePanel.Size = new System.Drawing.Size(358, 32);
            this.SandpilePanel.TabIndex = 31;
            this.SandpilePanel.Visible = false;
            // 
            // RadiusTrackBar
            // 
            this.RadiusTrackBar.Location = new System.Drawing.Point(10, 526);
            this.RadiusTrackBar.Maximum = 20;
            this.RadiusTrackBar.Minimum = 8;
            this.RadiusTrackBar.Name = "RadiusTrackBar";
            this.RadiusTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.RadiusTrackBar.Size = new System.Drawing.Size(69, 134);
            this.RadiusTrackBar.TabIndex = 32;
            this.RadiusTrackBar.Value = 8;
            this.RadiusTrackBar.Visible = false;
            this.RadiusTrackBar.ValueChanged += new System.EventHandler(this.RadiusTrackBar_ValueChanged);
            this.RadiusTrackBar.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.Movement_PreviewKeyDown);
            // 
            // RadiusLabel
            // 
            this.RadiusLabel.AutoSize = true;
            this.RadiusLabel.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.RadiusLabel.Location = new System.Drawing.Point(6, 490);
            this.RadiusLabel.Name = "RadiusLabel";
            this.RadiusLabel.Size = new System.Drawing.Size(80, 42);
            this.RadiusLabel.TabIndex = 33;
            this.RadiusLabel.Text = "Vertex\n     Radius:";
            this.RadiusLabel.Visible = false;
            // 
            // SquareLattice
            // 
            this.SquareLattice.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.SquareLattice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SquareLattice.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SquareLattice.Location = new System.Drawing.Point(426, 560);
            this.SquareLattice.Name = "SquareLattice";
            this.SquareLattice.Size = new System.Drawing.Size(195, 37);
            this.SquareLattice.TabIndex = 34;
            this.SquareLattice.Text = "Square Lattice";
            this.SquareLattice.UseVisualStyleBackColor = true;
            this.SquareLattice.Click += new System.EventHandler(this.SquareLattice_Click);
            // 
            // TriangleLattice
            // 
            this.TriangleLattice.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.TriangleLattice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TriangleLattice.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TriangleLattice.Location = new System.Drawing.Point(631, 560);
            this.TriangleLattice.Name = "TriangleLattice";
            this.TriangleLattice.Size = new System.Drawing.Size(195, 37);
            this.TriangleLattice.TabIndex = 35;
            this.TriangleLattice.Text = "Triangular Lattice";
            this.TriangleLattice.UseVisualStyleBackColor = true;
            this.TriangleLattice.Click += new System.EventHandler(this.TriangleLattice_Click);
            // 
            // DrawingSurface
            // 
            this.DrawingSurface.BackColor = System.Drawing.Color.White;
            this.DrawingSurface.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.DrawingSurface.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DrawingSurface.Location = new System.Drawing.Point(115, 50);
            this.DrawingSurface.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.DrawingSurface.Name = "DrawingSurface";
            this.DrawingSurface.Size = new System.Drawing.Size(780, 628);
            this.DrawingSurface.TabIndex = 11;
            this.DrawingSurface.TabStop = false;
            this.DrawingSurface.Visible = false;
            this.DrawingSurface.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DrawingSurface_MouseClick);
            this.DrawingSurface.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DrawingSurface_MouseDoubleClick);
            this.DrawingSurface.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DrawingSurface_MouseDown);
            this.DrawingSurface.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DrawingSurface_MouseMove);
            this.DrawingSurface.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DrawingSurface_MouseUp);
            // 
            // CursorToolTip
            // 
            this.CursorToolTip.AutoPopDelay = 5000;
            this.CursorToolTip.InitialDelay = 300;
            this.CursorToolTip.ReshowDelay = 100;
            this.CursorToolTip.ToolTipTitle = "Cursor";
            // 
            // VertexToolTip
            // 
            this.VertexToolTip.AutoPopDelay = 5000;
            this.VertexToolTip.InitialDelay = 300;
            this.VertexToolTip.ReshowDelay = 100;
            this.VertexToolTip.ToolTipTitle = "Draw Vertex";
            // 
            // ArcToolTip
            // 
            this.ArcToolTip.AutoPopDelay = 5000;
            this.ArcToolTip.InitialDelay = 300;
            this.ArcToolTip.ReshowDelay = 100;
            this.ArcToolTip.ToolTipTitle = "Draw Arc";
            // 
            // EraserToolTip
            // 
            this.EraserToolTip.AutoPopDelay = 5000;
            this.EraserToolTip.InitialDelay = 300;
            this.EraserToolTip.ReshowDelay = 100;
            this.EraserToolTip.ToolTipTitle = "Eraser";
            // 
            // ClearAllToolTip
            // 
            this.ClearAllToolTip.AutoPopDelay = 5000;
            this.ClearAllToolTip.InitialDelay = 300;
            this.ClearAllToolTip.ReshowDelay = 100;
            this.ClearAllToolTip.ToolTipTitle = "Delete Digraph";
            // 
            // RadiusValueLabel
            // 
            this.RadiusValueLabel.AutoSize = true;
            this.RadiusValueLabel.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.RadiusValueLabel.Location = new System.Drawing.Point(7, 657);
            this.RadiusValueLabel.Name = "RadiusValueLabel";
            this.RadiusValueLabel.Size = new System.Drawing.Size(48, 21);
            this.RadiusValueLabel.TabIndex = 36;
            this.RadiusValueLabel.Text = "R = 8";
            this.RadiusValueLabel.Visible = false;
            // 
            // ModePage
            // 
            this.ModePage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ModePage.Controls.Add(this.SandpileChartType2);
            this.ModePage.Controls.Add(this.SandpileChartType1);
            this.ModePage.Controls.Add(this.ActionsLabel);
            this.ModePage.Controls.Add(this.SpeedNumeric);
            this.ModePage.Controls.Add(this.SpeedLabel);
            this.ModePage.Controls.Add(this.ModelingTypeLabel);
            this.ModePage.Controls.Add(this.SaveGifCheckBox);
            this.ModePage.Controls.Add(this.ChartCheckBox);
            this.ModePage.Controls.Add(this.AnimationCheckBox);
            this.ModePage.Controls.Add(this.SandpileTypeCheckBox);
            this.ModePage.Controls.Add(this.BasicTypeCheckBox);
            this.ModePage.Location = new System.Drawing.Point(4, 4);
            this.ModePage.Name = "ModePage";
            this.ModePage.Padding = new System.Windows.Forms.Padding(3);
            this.ModePage.Size = new System.Drawing.Size(277, 620);
            this.ModePage.TabIndex = 4;
            this.ModePage.Text = " Modeling Options";
            this.ModePage.UseVisualStyleBackColor = true;
            // 
            // SandpileChartType2
            // 
            this.SandpileChartType2.AutoSize = true;
            this.SandpileChartType2.Location = new System.Drawing.Point(40, 281);
            this.SandpileChartType2.Name = "SandpileChartType2";
            this.SandpileChartType2.Size = new System.Drawing.Size(218, 60);
            this.SandpileChartType2.TabIndex = 27;
            this.SandpileChartType2.Text = "Distribution of\n\ravalanche sizes chart";
            this.SandpileChartType2.UseVisualStyleBackColor = true;
            this.SandpileChartType2.Visible = false;
            this.SandpileChartType2.CheckedChanged += new System.EventHandler(this.SandpileChartType2_CheckedChanged);
            // 
            // SandpileChartType1
            // 
            this.SandpileChartType1.AutoSize = true;
            this.SandpileChartType1.Checked = true;
            this.SandpileChartType1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SandpileChartType1.Location = new System.Drawing.Point(40, 246);
            this.SandpileChartType1.Name = "SandpileChartType1";
            this.SandpileChartType1.Size = new System.Drawing.Size(226, 32);
            this.SandpileChartType1.TabIndex = 26;
            this.SandpileChartType1.Text = "Number of dots chart";
            this.SandpileChartType1.UseVisualStyleBackColor = true;
            this.SandpileChartType1.Visible = false;
            this.SandpileChartType1.CheckedChanged += new System.EventHandler(this.SandpileChartType1_CheckedChanged);
            // 
            // ActionsLabel
            // 
            this.ActionsLabel.AutoSize = true;
            this.ActionsLabel.Location = new System.Drawing.Point(6, 139);
            this.ActionsLabel.Name = "ActionsLabel";
            this.ActionsLabel.Size = new System.Drawing.Size(155, 28);
            this.ActionsLabel.TabIndex = 6;
            this.ActionsLabel.Text = "Needed Actions:";
            // 
            // SpeedNumeric
            // 
            this.SpeedNumeric.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.SpeedNumeric.Location = new System.Drawing.Point(11, 346);
            this.SpeedNumeric.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.SpeedNumeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SpeedNumeric.Name = "SpeedNumeric";
            this.SpeedNumeric.Size = new System.Drawing.Size(95, 31);
            this.SpeedNumeric.TabIndex = 24;
            this.SpeedNumeric.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // SpeedLabel
            // 
            this.SpeedLabel.AutoSize = true;
            this.SpeedLabel.Location = new System.Drawing.Point(6, 308);
            this.SpeedLabel.Name = "SpeedLabel";
            this.SpeedLabel.Size = new System.Drawing.Size(138, 28);
            this.SpeedLabel.TabIndex = 25;
            this.SpeedLabel.Text = "Speed, units/s:";
            // 
            // ModelingTypeLabel
            // 
            this.ModelingTypeLabel.AutoSize = true;
            this.ModelingTypeLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ModelingTypeLabel.Location = new System.Drawing.Point(6, 7);
            this.ModelingTypeLabel.Name = "ModelingTypeLabel";
            this.ModelingTypeLabel.Size = new System.Drawing.Size(233, 28);
            this.ModelingTypeLabel.TabIndex = 5;
            this.ModelingTypeLabel.Text = "Movement modelig type:";
            // 
            // SaveGifCheckBox
            // 
            this.SaveGifCheckBox.AutoSize = true;
            this.SaveGifCheckBox.Location = new System.Drawing.Point(11, 246);
            this.SaveGifCheckBox.Name = "SaveGifCheckBox";
            this.SaveGifCheckBox.Size = new System.Drawing.Size(109, 32);
            this.SaveGifCheckBox.TabIndex = 4;
            this.SaveGifCheckBox.Text = "Save Gif";
            this.SaveGifCheckBox.UseVisualStyleBackColor = true;
            // 
            // ChartCheckBox
            // 
            this.ChartCheckBox.AutoSize = true;
            this.ChartCheckBox.Location = new System.Drawing.Point(11, 208);
            this.ChartCheckBox.Name = "ChartCheckBox";
            this.ChartCheckBox.Size = new System.Drawing.Size(151, 32);
            this.ChartCheckBox.TabIndex = 3;
            this.ChartCheckBox.Text = "Create Chart ";
            this.ChartCheckBox.UseVisualStyleBackColor = true;
            this.ChartCheckBox.CheckedChanged += new System.EventHandler(this.ChartCheckBox_CheckedChanged);
            // 
            // AnimationCheckBox
            // 
            this.AnimationCheckBox.AutoSize = true;
            this.AnimationCheckBox.Checked = true;
            this.AnimationCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AnimationCheckBox.Enabled = false;
            this.AnimationCheckBox.Location = new System.Drawing.Point(11, 170);
            this.AnimationCheckBox.Name = "AnimationCheckBox";
            this.AnimationCheckBox.Size = new System.Drawing.Size(129, 32);
            this.AnimationCheckBox.TabIndex = 2;
            this.AnimationCheckBox.Text = "Animation";
            this.AnimationCheckBox.UseVisualStyleBackColor = true;
            // 
            // SandpileTypeCheckBox
            // 
            this.SandpileTypeCheckBox.AutoSize = true;
            this.SandpileTypeCheckBox.Location = new System.Drawing.Point(11, 76);
            this.SandpileTypeCheckBox.Name = "SandpileTypeCheckBox";
            this.SandpileTypeCheckBox.Size = new System.Drawing.Size(114, 32);
            this.SandpileTypeCheckBox.TabIndex = 1;
            this.SandpileTypeCheckBox.Text = "Sandpile";
            this.SandpileTypeCheckBox.UseVisualStyleBackColor = true;
            this.SandpileTypeCheckBox.CheckedChanged += new System.EventHandler(this.SandpileTypeCheckBox_CheckedChanged);
            // 
            // BasicTypeCheckBox
            // 
            this.BasicTypeCheckBox.AutoSize = true;
            this.BasicTypeCheckBox.Checked = true;
            this.BasicTypeCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.BasicTypeCheckBox.Location = new System.Drawing.Point(11, 38);
            this.BasicTypeCheckBox.Name = "BasicTypeCheckBox";
            this.BasicTypeCheckBox.Size = new System.Drawing.Size(81, 32);
            this.BasicTypeCheckBox.TabIndex = 0;
            this.BasicTypeCheckBox.Text = "Basic";
            this.BasicTypeCheckBox.UseVisualStyleBackColor = true;
            this.BasicTypeCheckBox.CheckedChanged += new System.EventHandler(this.BasicTypeCheckBox_CheckedChanged);
            // 
            // ParamsPage
            // 
            this.ParamsPage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ParamsPage.Controls.Add(this.GridParameters);
            this.ParamsPage.Controls.Add(this.ParametersLegendLabel);
            this.ParamsPage.Controls.Add(this.ParamsLabel);
            this.ParamsPage.Location = new System.Drawing.Point(4, 4);
            this.ParamsPage.Name = "ParamsPage";
            this.ParamsPage.Padding = new System.Windows.Forms.Padding(3);
            this.ParamsPage.Size = new System.Drawing.Size(277, 620);
            this.ParamsPage.TabIndex = 1;
            this.ParamsPage.Text = " Graph Parameters ";
            this.ParamsPage.UseVisualStyleBackColor = true;
            // 
            // GridParameters
            // 
            this.GridParameters.AllowUserToAddRows = false;
            this.GridParameters.AllowUserToDeleteRows = false;
            this.GridParameters.AllowUserToResizeColumns = false;
            this.GridParameters.AllowUserToResizeRows = false;
            this.GridParameters.BackgroundColor = System.Drawing.SystemColors.Control;
            this.GridParameters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridParameters.Location = new System.Drawing.Point(10, 42);
            this.GridParameters.Name = "GridParameters";
            this.GridParameters.RowHeadersWidth = 43;
            this.GridParameters.RowTemplate.Height = 28;
            this.GridParameters.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.GridParameters.Size = new System.Drawing.Size(255, 480);
            this.GridParameters.TabIndex = 21;
            this.GridParameters.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridParameters_CellValueChanged);
            this.GridParameters.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.GridParameters_RowPrePaint);
            this.GridParameters.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.GridParameters_RowsAdded);
            this.GridParameters.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.GridParameters_RowsRemoved);
            // 
            // ParametersLegendLabel
            // 
            this.ParametersLegendLabel.AutoSize = true;
            this.ParametersLegendLabel.Location = new System.Drawing.Point(10, 530);
            this.ParametersLegendLabel.Name = "ParametersLegendLabel";
            this.ParametersLegendLabel.Size = new System.Drawing.Size(205, 84);
            this.ParametersLegendLabel.TabIndex = 23;
            this.ParametersLegendLabel.Text = "th - Thresholds\n\rp  - Refractory Periods\n\rs   - Initial States";
            // 
            // ParamsLabel
            // 
            this.ParamsLabel.AutoSize = true;
            this.ParamsLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ParamsLabel.Location = new System.Drawing.Point(7, 4);
            this.ParamsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ParamsLabel.Name = "ParamsLabel";
            this.ParamsLabel.Size = new System.Drawing.Size(188, 28);
            this.ParamsLabel.TabIndex = 22;
            this.ParamsLabel.Text = "Digraph Parameters:";
            // 
            // AdjacencyPage
            // 
            this.AdjacencyPage.BackColor = System.Drawing.SystemColors.Control;
            this.AdjacencyPage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AdjacencyPage.Controls.Add(this.ArcLengthLabel);
            this.AdjacencyPage.Controls.Add(this.GridAdjacencyMatrix);
            this.AdjacencyPage.Controls.Add(this.ArcLengthContainer);
            this.AdjacencyPage.Controls.Add(this.AdjacencyMatrixLabel);
            this.AdjacencyPage.Controls.Add(this.OkLength);
            this.AdjacencyPage.Location = new System.Drawing.Point(4, 4);
            this.AdjacencyPage.Name = "AdjacencyPage";
            this.AdjacencyPage.Padding = new System.Windows.Forms.Padding(3);
            this.AdjacencyPage.Size = new System.Drawing.Size(277, 620);
            this.AdjacencyPage.TabIndex = 0;
            this.AdjacencyPage.Text = "Adjacency and Arcs Length ";
            // 
            // ArcLengthLabel
            // 
            this.ArcLengthLabel.AutoSize = true;
            this.ArcLengthLabel.Location = new System.Drawing.Point(10, 10);
            this.ArcLengthLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ArcLengthLabel.Name = "ArcLengthLabel";
            this.ArcLengthLabel.Size = new System.Drawing.Size(159, 28);
            this.ArcLengthLabel.TabIndex = 24;
            this.ArcLengthLabel.Text = "Arc:          Length:";
            // 
            // GridAdjacencyMatrix
            // 
            this.GridAdjacencyMatrix.AllowUserToAddRows = false;
            this.GridAdjacencyMatrix.AllowUserToDeleteRows = false;
            this.GridAdjacencyMatrix.AllowUserToResizeColumns = false;
            this.GridAdjacencyMatrix.AllowUserToResizeRows = false;
            this.GridAdjacencyMatrix.BackgroundColor = System.Drawing.SystemColors.Control;
            this.GridAdjacencyMatrix.ColumnHeadersHeight = 33;
            this.GridAdjacencyMatrix.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.GridAdjacencyMatrix.Location = new System.Drawing.Point(10, 152);
            this.GridAdjacencyMatrix.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GridAdjacencyMatrix.MultiSelect = false;
            this.GridAdjacencyMatrix.Name = "GridAdjacencyMatrix";
            this.GridAdjacencyMatrix.ReadOnly = true;
            this.GridAdjacencyMatrix.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.GridAdjacencyMatrix.RowHeadersWidth = 33;
            this.GridAdjacencyMatrix.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.GridAdjacencyMatrix.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.GridAdjacencyMatrix.Size = new System.Drawing.Size(250, 458);
            this.GridAdjacencyMatrix.TabIndex = 19;
            this.GridAdjacencyMatrix.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridAdjacencyMatrix_CellClick);
            this.GridAdjacencyMatrix.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.GridAdjacencyMatrix_RowPrePaint);
            // 
            // ArcLengthContainer
            // 
            this.ArcLengthContainer.Location = new System.Drawing.Point(10, 42);
            this.ArcLengthContainer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ArcLengthContainer.Name = "ArcLengthContainer";
            // 
            // ArcLengthContainer.Panel1
            // 
            this.ArcLengthContainer.Panel1.Controls.Add(this.ArcName);
            // 
            // ArcLengthContainer.Panel2
            // 
            this.ArcLengthContainer.Panel2.Controls.Add(this.ArcLength);
            this.ArcLengthContainer.Size = new System.Drawing.Size(256, 37);
            this.ArcLengthContainer.SplitterDistance = 81;
            this.ArcLengthContainer.SplitterWidth = 6;
            this.ArcLengthContainer.TabIndex = 23;
            // 
            // ArcName
            // 
            this.ArcName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ArcName.FormattingEnabled = true;
            this.ArcName.Location = new System.Drawing.Point(0, 0);
            this.ArcName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ArcName.Name = "ArcName";
            this.ArcName.Size = new System.Drawing.Size(78, 33);
            this.ArcName.TabIndex = 0;
            this.ArcName.TextChanged += new System.EventHandler(this.ArcName_TextChanged);
            // 
            // ArcLength
            // 
            this.ArcLength.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ArcLength.Location = new System.Drawing.Point(0, 0);
            this.ArcLength.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ArcLength.Name = "ArcLength";
            this.ArcLength.Size = new System.Drawing.Size(163, 31);
            this.ArcLength.TabIndex = 0;
            this.ArcLength.TextChanged += new System.EventHandler(this.ArcLength_TextChanged);
            // 
            // AdjacencyMatrixLabel
            // 
            this.AdjacencyMatrixLabel.AutoSize = true;
            this.AdjacencyMatrixLabel.Location = new System.Drawing.Point(4, 119);
            this.AdjacencyMatrixLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.AdjacencyMatrixLabel.Name = "AdjacencyMatrixLabel";
            this.AdjacencyMatrixLabel.Size = new System.Drawing.Size(166, 28);
            this.AdjacencyMatrixLabel.TabIndex = 22;
            this.AdjacencyMatrixLabel.Text = "Adjacency Matrix:";
            // 
            // OkLength
            // 
            this.OkLength.Cursor = System.Windows.Forms.Cursors.Default;
            this.OkLength.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OkLength.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.OkLength.Location = new System.Drawing.Point(174, 83);
            this.OkLength.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.OkLength.Name = "OkLength";
            this.OkLength.Size = new System.Drawing.Size(86, 34);
            this.OkLength.TabIndex = 1;
            this.OkLength.Text = "Ok";
            this.OkLength.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.OkLength.UseVisualStyleBackColor = true;
            this.OkLength.Click += new System.EventHandler(this.OkWeight_Click);
            // 
            // AppParameters
            // 
            this.AppParameters.Alignment = System.Windows.Forms.TabAlignment.Right;
            this.AppParameters.Controls.Add(this.AdjacencyPage);
            this.AppParameters.Controls.Add(this.ParamsPage);
            this.AppParameters.Controls.Add(this.ModePage);
            this.AppParameters.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AppParameters.Location = new System.Drawing.Point(906, 50);
            this.AppParameters.Multiline = true;
            this.AppParameters.Name = "AppParameters";
            this.AppParameters.SelectedIndex = 0;
            this.AppParameters.Size = new System.Drawing.Size(318, 628);
            this.AppParameters.TabIndex = 23;
            this.AppParameters.Visible = false;
            this.AppParameters.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.Movement_PreviewKeyDown);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1236, 697);
            this.Controls.Add(this.RadiusValueLabel);
            this.Controls.Add(this.TimeTextBox);
            this.Controls.Add(this.TriangleLattice);
            this.Controls.Add(this.SquareLattice);
            this.Controls.Add(this.RadiusLabel);
            this.Controls.Add(this.RadiusTrackBar);
            this.Controls.Add(this.SandpilePanel);
            this.Controls.Add(this.AppParameters);
            this.Controls.Add(this.RandomGraph);
            this.Controls.Add(this.Tools);
            this.Controls.Add(this.Open);
            this.Controls.Add(this.Build);
            this.Controls.Add(this.TopMenu);
            this.Controls.Add(this.DrawingSurface);
            this.KeyPreview = true;
            this.MainMenuStrip = this.TopMenu;
            this.MinimumSize = new System.Drawing.Size(1258, 753);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Points Movement Modeling Application";
            this.SizeChanged += new System.EventHandler(this.GraphBuilder_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GraphBuilder_KeyDown);
            this.TopMenu.ResumeLayout(false);
            this.TopMenu.PerformLayout();
            this.Tools.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SandpilePalette)).EndInit();
            this.SandpilePanel.ResumeLayout(false);
            this.SandpilePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RadiusTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DrawingSurface)).EndInit();
            this.ModePage.ResumeLayout(false);
            this.ModePage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SpeedNumeric)).EndInit();
            this.ParamsPage.ResumeLayout(false);
            this.ParamsPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridParameters)).EndInit();
            this.AdjacencyPage.ResumeLayout(false);
            this.AdjacencyPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridAdjacencyMatrix)).EndInit();
            this.ArcLengthContainer.Panel1.ResumeLayout(false);
            this.ArcLengthContainer.Panel2.ResumeLayout(false);
            this.ArcLengthContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ArcLengthContainer)).EndInit();
            this.ArcLengthContainer.ResumeLayout(false);
            this.AppParameters.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Build;
        private System.Windows.Forms.Button Open;
        private System.Windows.Forms.MenuStrip TopMenu;
        private System.Windows.Forms.Panel Tools;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.Button CursorButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button VertexButton;
        private System.Windows.Forms.Button EdgeButton;
        private System.Windows.Forms.Button RandomGraph;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UserManualToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MainMenuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem StopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MovementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ResetToolStripMenuItem;
        private System.Windows.Forms.TextBox TimeTextBox;
        private System.Windows.Forms.Label SandpileLabel;
        private System.Windows.Forms.Label RandomAddingLabel;
        private System.Windows.Forms.CheckBox RandomAddingCheckBox;
        private System.Windows.Forms.Panel SandpilePanel;
        private System.Windows.Forms.ToolStripMenuItem dataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAllToolStripMenuItem;
        private System.Windows.Forms.TrackBar RadiusTrackBar;
        private System.Windows.Forms.Label RadiusLabel;
        private System.Windows.Forms.Button SquareLattice;
        private System.Windows.Forms.Button TriangleLattice;
        private System.Windows.Forms.PictureBox DrawingSurface;
        private System.Windows.Forms.ToolTip CursorToolTip;
        private System.Windows.Forms.ToolTip VertexToolTip;
        private System.Windows.Forms.ToolTip ArcToolTip;
        private System.Windows.Forms.ToolTip EraserToolTip;
        private System.Windows.Forms.ToolTip ClearAllToolTip;
        private System.Windows.Forms.DataGridView SandpilePalette;
        private System.Windows.Forms.Label RadiusValueLabel;
        private System.Windows.Forms.TabPage ModePage;
        private System.Windows.Forms.CheckBox SandpileChartType2;
        private System.Windows.Forms.CheckBox SandpileChartType1;
        private System.Windows.Forms.Label ActionsLabel;
        private System.Windows.Forms.NumericUpDown SpeedNumeric;
        private System.Windows.Forms.Label SpeedLabel;
        private System.Windows.Forms.Label ModelingTypeLabel;
        private System.Windows.Forms.CheckBox SaveGifCheckBox;
        private System.Windows.Forms.CheckBox ChartCheckBox;
        private System.Windows.Forms.CheckBox AnimationCheckBox;
        private System.Windows.Forms.CheckBox SandpileTypeCheckBox;
        private System.Windows.Forms.CheckBox BasicTypeCheckBox;
        private System.Windows.Forms.TabPage ParamsPage;
        private System.Windows.Forms.Label ParametersLegendLabel;
        private System.Windows.Forms.Label ParamsLabel;
        private System.Windows.Forms.DataGridView GridParameters;
        private System.Windows.Forms.TabPage AdjacencyPage;
        private System.Windows.Forms.Label ArcLengthLabel;
        private System.Windows.Forms.DataGridView GridAdjacencyMatrix;
        private System.Windows.Forms.SplitContainer ArcLengthContainer;
        private System.Windows.Forms.ComboBox ArcName;
        private System.Windows.Forms.TextBox ArcLength;
        private System.Windows.Forms.Label AdjacencyMatrixLabel;
        private System.Windows.Forms.Button OkLength;
        private System.Windows.Forms.TabControl AppParameters;
    }
}


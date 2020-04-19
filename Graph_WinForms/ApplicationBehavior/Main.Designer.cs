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
            this.Build = new System.Windows.Forms.Button();
            this.Open = new System.Windows.Forms.Button();
            this.TopMenu = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.darkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UserManualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MovementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ResetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Tools = new System.Windows.Forms.Panel();
            this.ClearButton = new System.Windows.Forms.Button();
            this.CoursorButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.VertexButton = new System.Windows.Forms.Button();
            this.EdgeButton = new System.Windows.Forms.Button();
            this.OkLength = new System.Windows.Forms.Button();
            this.RandomGraph = new System.Windows.Forms.Button();
            this.DrawingSurface = new System.Windows.Forms.PictureBox();
            this.GridThresholds = new System.Windows.Forms.DataGridView();
            this.ThresholdsLabel = new System.Windows.Forms.Label();
            this.AppParameters = new System.Windows.Forms.TabControl();
            this.AdjacencyPage = new System.Windows.Forms.TabPage();
            this.ArcLengthLabel = new System.Windows.Forms.Label();
            this.GridAdjacencyMatrix = new System.Windows.Forms.DataGridView();
            this.ArcLengthContainer = new System.Windows.Forms.SplitContainer();
            this.ArcName = new System.Windows.Forms.ComboBox();
            this.ArcLength = new System.Windows.Forms.TextBox();
            this.AdjacencyMatrixLabel = new System.Windows.Forms.Label();
            this.ThresholdsPage = new System.Windows.Forms.TabPage();
            this.RefactoryPeriodsPage = new System.Windows.Forms.TabPage();
            this.RefractoryPeriodsLabel = new System.Windows.Forms.Label();
            this.GridRefractoryPeriods = new System.Windows.Forms.DataGridView();
            this.InitialStatePage = new System.Windows.Forms.TabPage();
            this.InitialStateLabel = new System.Windows.Forms.Label();
            this.GridInitialState = new System.Windows.Forms.DataGridView();
            this.ModePage = new System.Windows.Forms.TabPage();
            this.ActionsLabel = new System.Windows.Forms.Label();
            this.SpeedNumeric = new System.Windows.Forms.NumericUpDown();
            this.SpeedLabel = new System.Windows.Forms.Label();
            this.ModelingTypeLabel = new System.Windows.Forms.Label();
            this.SaveGifCheckBox = new System.Windows.Forms.CheckBox();
            this.ChartCheckBox = new System.Windows.Forms.CheckBox();
            this.AnimationCheckBox = new System.Windows.Forms.CheckBox();
            this.SandpileTypeCheckBox = new System.Windows.Forms.CheckBox();
            this.BasicTypeCheckBox = new System.Windows.Forms.CheckBox();
            this.TimeTextBox = new System.Windows.Forms.TextBox();
            this.TopMenu.SuspendLayout();
            this.Tools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DrawingSurface)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridThresholds)).BeginInit();
            this.AppParameters.SuspendLayout();
            this.AdjacencyPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridAdjacencyMatrix)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ArcLengthContainer)).BeginInit();
            this.ArcLengthContainer.Panel1.SuspendLayout();
            this.ArcLengthContainer.Panel2.SuspendLayout();
            this.ArcLengthContainer.SuspendLayout();
            this.ThresholdsPage.SuspendLayout();
            this.RefactoryPeriodsPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridRefractoryPeriods)).BeginInit();
            this.InitialStatePage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridInitialState)).BeginInit();
            this.ModePage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SpeedNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // Build
            // 
            this.Build.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Build.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.Build.Location = new System.Drawing.Point(446, 351);
            this.Build.Name = "Build";
            this.Build.Size = new System.Drawing.Size(345, 62);
            this.Build.TabIndex = 0;
            this.Build.Text = "Build a new graph";
            this.Build.UseVisualStyleBackColor = true;
            this.Build.Click += new System.EventHandler(this.Build_Click);
            // 
            // Open
            // 
            this.Open.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Open.Font = new System.Drawing.Font("Segoe UI", 15.75F);
            this.Open.Location = new System.Drawing.Point(446, 488);
            this.Open.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Open.Name = "Open";
            this.Open.Size = new System.Drawing.Size(345, 62);
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
            this.SettingsToolStripMenuItem,
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
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(217, 34);
            this.saveToolStripMenuItem.Text = "Save Graph";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(217, 34);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // SettingsToolStripMenuItem
            // 
            this.SettingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.languageToolStripMenuItem,
            this.modeToolStripMenuItem});
            this.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem";
            this.SettingsToolStripMenuItem.Size = new System.Drawing.Size(92, 29);
            this.SettingsToolStripMenuItem.Text = "Settings";
            // 
            // languageToolStripMenuItem
            // 
            this.languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            this.languageToolStripMenuItem.Size = new System.Drawing.Size(191, 34);
            this.languageToolStripMenuItem.Text = "Language";
            // 
            // modeToolStripMenuItem
            // 
            this.modeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.darkToolStripMenuItem,
            this.lightToolStripMenuItem});
            this.modeToolStripMenuItem.Name = "modeToolStripMenuItem";
            this.modeToolStripMenuItem.Size = new System.Drawing.Size(191, 34);
            this.modeToolStripMenuItem.Text = "Mode";
            // 
            // darkToolStripMenuItem
            // 
            this.darkToolStripMenuItem.Name = "darkToolStripMenuItem";
            this.darkToolStripMenuItem.Size = new System.Drawing.Size(153, 34);
            this.darkToolStripMenuItem.Text = "Dark";
            this.darkToolStripMenuItem.Click += new System.EventHandler(this.DarkToolStripMenuItem_Click);
            // 
            // lightToolStripMenuItem
            // 
            this.lightToolStripMenuItem.Name = "lightToolStripMenuItem";
            this.lightToolStripMenuItem.Size = new System.Drawing.Size(153, 34);
            this.lightToolStripMenuItem.Text = "Light";
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
            this.Tools.Controls.Add(this.CoursorButton);
            this.Tools.Controls.Add(this.DeleteButton);
            this.Tools.Controls.Add(this.VertexButton);
            this.Tools.Controls.Add(this.EdgeButton);
            this.Tools.Location = new System.Drawing.Point(10, 50);
            this.Tools.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Tools.Name = "Tools";
            this.Tools.Size = new System.Drawing.Size(95, 435);
            this.Tools.TabIndex = 7;
            this.Tools.Visible = false;
            // 
            // ClearButton
            // 
            this.ClearButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ClearButton.Location = new System.Drawing.Point(10, 350);
            this.ClearButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(75, 75);
            this.ClearButton.TabIndex = 11;
            this.ClearButton.Text = "Clear All";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // CoursorButton
            // 
            this.CoursorButton.Enabled = false;
            this.CoursorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CoursorButton.Location = new System.Drawing.Point(10, 10);
            this.CoursorButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CoursorButton.Name = "CoursorButton";
            this.CoursorButton.Size = new System.Drawing.Size(75, 75);
            this.CoursorButton.TabIndex = 8;
            this.CoursorButton.Text = "Cursor";
            this.CoursorButton.UseVisualStyleBackColor = true;
            this.CoursorButton.Click += new System.EventHandler(this.CursorButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteButton.Location = new System.Drawing.Point(10, 265);
            this.DeleteButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(75, 75);
            this.DeleteButton.TabIndex = 10;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // VertexButton
            // 
            this.VertexButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.VertexButton.Location = new System.Drawing.Point(10, 95);
            this.VertexButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.VertexButton.Name = "VertexButton";
            this.VertexButton.Size = new System.Drawing.Size(75, 75);
            this.VertexButton.TabIndex = 8;
            this.VertexButton.Text = "Vertex";
            this.VertexButton.UseVisualStyleBackColor = true;
            this.VertexButton.Click += new System.EventHandler(this.VertexButton_Click);
            // 
            // EdgeButton
            // 
            this.EdgeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EdgeButton.Location = new System.Drawing.Point(10, 180);
            this.EdgeButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.EdgeButton.Name = "EdgeButton";
            this.EdgeButton.Size = new System.Drawing.Size(75, 75);
            this.EdgeButton.TabIndex = 9;
            this.EdgeButton.Text = "Edge";
            this.EdgeButton.UseVisualStyleBackColor = true;
            this.EdgeButton.Click += new System.EventHandler(this.EdgeButton_Click);
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
            // RandomGraph
            // 
            this.RandomGraph.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RandomGraph.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.RandomGraph.Location = new System.Drawing.Point(446, 418);
            this.RandomGraph.Name = "RandomGraph";
            this.RandomGraph.Size = new System.Drawing.Size(345, 62);
            this.RandomGraph.TabIndex = 10;
            this.RandomGraph.Text = "Create random";
            this.RandomGraph.UseVisualStyleBackColor = true;
            this.RandomGraph.Click += new System.EventHandler(this.RandomGraph_Click);
            // 
            // DrawingSurface
            // 
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
            // GridThresholds
            // 
            this.GridThresholds.AllowUserToAddRows = false;
            this.GridThresholds.AllowUserToDeleteRows = false;
            this.GridThresholds.AllowUserToResizeColumns = false;
            this.GridThresholds.AllowUserToResizeRows = false;
            this.GridThresholds.BackgroundColor = System.Drawing.SystemColors.Control;
            this.GridThresholds.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridThresholds.Location = new System.Drawing.Point(10, 42);
            this.GridThresholds.Name = "GridThresholds";
            this.GridThresholds.RowHeadersWidth = 33;
            this.GridThresholds.RowTemplate.Height = 28;
            this.GridThresholds.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.GridThresholds.Size = new System.Drawing.Size(130, 572);
            this.GridThresholds.TabIndex = 21;
            this.GridThresholds.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridThresholds_CellValueChanged);
            this.GridThresholds.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.GridThresholds_RowPrePaint);
            // 
            // ThresholdsLabel
            // 
            this.ThresholdsLabel.AutoSize = true;
            this.ThresholdsLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ThresholdsLabel.Location = new System.Drawing.Point(10, 10);
            this.ThresholdsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ThresholdsLabel.Name = "ThresholdsLabel";
            this.ThresholdsLabel.Size = new System.Drawing.Size(110, 28);
            this.ThresholdsLabel.TabIndex = 22;
            this.ThresholdsLabel.Text = "Thresholds:";
            // 
            // AppParameters
            // 
            this.AppParameters.Alignment = System.Windows.Forms.TabAlignment.Right;
            this.AppParameters.Controls.Add(this.AdjacencyPage);
            this.AppParameters.Controls.Add(this.ThresholdsPage);
            this.AppParameters.Controls.Add(this.RefactoryPeriodsPage);
            this.AppParameters.Controls.Add(this.InitialStatePage);
            this.AppParameters.Controls.Add(this.ModePage);
            this.AppParameters.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AppParameters.Location = new System.Drawing.Point(906, 50);
            this.AppParameters.Multiline = true;
            this.AppParameters.Name = "AppParameters";
            this.AppParameters.SelectedIndex = 0;
            this.AppParameters.Size = new System.Drawing.Size(318, 628);
            this.AppParameters.TabIndex = 23;
            this.AppParameters.Visible = false;
            this.AppParameters.MouseLeave += new System.EventHandler(this.AppParameters_MouseLeave);
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
            this.AdjacencyPage.Text = " Adjacency ";
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
            // ThresholdsPage
            // 
            this.ThresholdsPage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ThresholdsPage.Controls.Add(this.ThresholdsLabel);
            this.ThresholdsPage.Controls.Add(this.GridThresholds);
            this.ThresholdsPage.Location = new System.Drawing.Point(4, 4);
            this.ThresholdsPage.Name = "ThresholdsPage";
            this.ThresholdsPage.Padding = new System.Windows.Forms.Padding(3);
            this.ThresholdsPage.Size = new System.Drawing.Size(277, 620);
            this.ThresholdsPage.TabIndex = 1;
            this.ThresholdsPage.Text = " Thresholds ";
            this.ThresholdsPage.UseVisualStyleBackColor = true;
            // 
            // RefactoryPeriodsPage
            // 
            this.RefactoryPeriodsPage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RefactoryPeriodsPage.Controls.Add(this.RefractoryPeriodsLabel);
            this.RefactoryPeriodsPage.Controls.Add(this.GridRefractoryPeriods);
            this.RefactoryPeriodsPage.Location = new System.Drawing.Point(4, 4);
            this.RefactoryPeriodsPage.Name = "RefactoryPeriodsPage";
            this.RefactoryPeriodsPage.Padding = new System.Windows.Forms.Padding(3);
            this.RefactoryPeriodsPage.Size = new System.Drawing.Size(277, 620);
            this.RefactoryPeriodsPage.TabIndex = 2;
            this.RefactoryPeriodsPage.Text = " Refractory Periods ";
            this.RefactoryPeriodsPage.UseVisualStyleBackColor = true;
            // 
            // RefractoryPeriodsLabel
            // 
            this.RefractoryPeriodsLabel.AutoSize = true;
            this.RefractoryPeriodsLabel.Location = new System.Drawing.Point(10, 10);
            this.RefractoryPeriodsLabel.Name = "RefractoryPeriodsLabel";
            this.RefractoryPeriodsLabel.Size = new System.Drawing.Size(174, 28);
            this.RefractoryPeriodsLabel.TabIndex = 22;
            this.RefractoryPeriodsLabel.Text = "Refractory Periods:";
            // 
            // GridRefractoryPeriods
            // 
            this.GridRefractoryPeriods.AllowUserToAddRows = false;
            this.GridRefractoryPeriods.AllowUserToDeleteRows = false;
            this.GridRefractoryPeriods.AllowUserToResizeColumns = false;
            this.GridRefractoryPeriods.AllowUserToResizeRows = false;
            this.GridRefractoryPeriods.BackgroundColor = System.Drawing.SystemColors.Control;
            this.GridRefractoryPeriods.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridRefractoryPeriods.Location = new System.Drawing.Point(10, 42);
            this.GridRefractoryPeriods.Name = "GridRefractoryPeriods";
            this.GridRefractoryPeriods.RowHeadersWidth = 33;
            this.GridRefractoryPeriods.RowTemplate.Height = 28;
            this.GridRefractoryPeriods.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.GridRefractoryPeriods.Size = new System.Drawing.Size(130, 572);
            this.GridRefractoryPeriods.TabIndex = 21;
            this.GridRefractoryPeriods.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridRefractoryPeriods_CellValueChanged);
            this.GridRefractoryPeriods.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.GridRefractoryPeriods_RowPrePaint);
            // 
            // InitialStatePage
            // 
            this.InitialStatePage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.InitialStatePage.Controls.Add(this.InitialStateLabel);
            this.InitialStatePage.Controls.Add(this.GridInitialState);
            this.InitialStatePage.Location = new System.Drawing.Point(4, 4);
            this.InitialStatePage.Name = "InitialStatePage";
            this.InitialStatePage.Padding = new System.Windows.Forms.Padding(3);
            this.InitialStatePage.Size = new System.Drawing.Size(277, 620);
            this.InitialStatePage.TabIndex = 3;
            this.InitialStatePage.Text = " Initial State ";
            this.InitialStatePage.UseVisualStyleBackColor = true;
            // 
            // InitialStateLabel
            // 
            this.InitialStateLabel.AutoSize = true;
            this.InitialStateLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.InitialStateLabel.Location = new System.Drawing.Point(10, 10);
            this.InitialStateLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.InitialStateLabel.Name = "InitialStateLabel";
            this.InitialStateLabel.Size = new System.Drawing.Size(113, 28);
            this.InitialStateLabel.TabIndex = 23;
            this.InitialStateLabel.Text = "Initial State:";
            // 
            // GridInitialState
            // 
            this.GridInitialState.AllowUserToAddRows = false;
            this.GridInitialState.AllowUserToDeleteRows = false;
            this.GridInitialState.AllowUserToResizeColumns = false;
            this.GridInitialState.AllowUserToResizeRows = false;
            this.GridInitialState.BackgroundColor = System.Drawing.SystemColors.Control;
            this.GridInitialState.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridInitialState.Location = new System.Drawing.Point(10, 42);
            this.GridInitialState.Name = "GridInitialState";
            this.GridInitialState.RowHeadersWidth = 33;
            this.GridInitialState.RowTemplate.Height = 28;
            this.GridInitialState.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.GridInitialState.Size = new System.Drawing.Size(130, 572);
            this.GridInitialState.TabIndex = 22;
            this.GridInitialState.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridInitialState_CellValueChanged);
            this.GridInitialState.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.GridInitialState_RowPrePaint);
            // 
            // ModePage
            // 
            this.ModePage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
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
            this.ModePage.Text = " Mode";
            this.ModePage.UseVisualStyleBackColor = true;
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
            // 
            // AnimationCheckBox
            // 
            this.AnimationCheckBox.AutoSize = true;
            this.AnimationCheckBox.Checked = true;
            this.AnimationCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
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
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1236, 697);
            this.Controls.Add(this.TimeTextBox);
            this.Controls.Add(this.AppParameters);
            this.Controls.Add(this.DrawingSurface);
            this.Controls.Add(this.RandomGraph);
            this.Controls.Add(this.Tools);
            this.Controls.Add(this.Open);
            this.Controls.Add(this.Build);
            this.Controls.Add(this.TopMenu);
            this.KeyPreview = true;
            this.MainMenuStrip = this.TopMenu;
            this.MinimumSize = new System.Drawing.Size(1228, 613);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Movement Modeling Application";
            this.SizeChanged += new System.EventHandler(this.GraphBuilder_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GraphBuilder_KeyDown);
            this.TopMenu.ResumeLayout(false);
            this.TopMenu.PerformLayout();
            this.Tools.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DrawingSurface)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridThresholds)).EndInit();
            this.AppParameters.ResumeLayout(false);
            this.AdjacencyPage.ResumeLayout(false);
            this.AdjacencyPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridAdjacencyMatrix)).EndInit();
            this.ArcLengthContainer.Panel1.ResumeLayout(false);
            this.ArcLengthContainer.Panel2.ResumeLayout(false);
            this.ArcLengthContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ArcLengthContainer)).EndInit();
            this.ArcLengthContainer.ResumeLayout(false);
            this.ThresholdsPage.ResumeLayout(false);
            this.ThresholdsPage.PerformLayout();
            this.RefactoryPeriodsPage.ResumeLayout(false);
            this.RefactoryPeriodsPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridRefractoryPeriods)).EndInit();
            this.InitialStatePage.ResumeLayout(false);
            this.InitialStatePage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridInitialState)).EndInit();
            this.ModePage.ResumeLayout(false);
            this.ModePage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SpeedNumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Build;
        private System.Windows.Forms.Button Open;
        private System.Windows.Forms.MenuStrip TopMenu;
        private System.Windows.Forms.Panel Tools;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.Button CoursorButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button VertexButton;
        private System.Windows.Forms.Button EdgeButton;
        private System.Windows.Forms.Button RandomGraph;
        private System.Windows.Forms.PictureBox DrawingSurface;
        private System.Windows.Forms.Button OkLength;
        private System.Windows.Forms.DataGridView GridThresholds;
        private System.Windows.Forms.Label ThresholdsLabel;
        private System.Windows.Forms.TabControl AppParameters;
        private System.Windows.Forms.TabPage AdjacencyPage;
        private System.Windows.Forms.TabPage ThresholdsPage;
        private System.Windows.Forms.TabPage RefactoryPeriodsPage;
        private System.Windows.Forms.TabPage InitialStatePage;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UserManualToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MainMenuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem StopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MovementToolStripMenuItem;
        private System.Windows.Forms.Label AdjacencyMatrixLabel;
        private System.Windows.Forms.SplitContainer ArcLengthContainer;
        private System.Windows.Forms.ComboBox ArcName;
        private System.Windows.Forms.TextBox ArcLength;
        private System.Windows.Forms.DataGridView GridAdjacencyMatrix;
        private System.Windows.Forms.Label ArcLengthLabel;
        private System.Windows.Forms.DataGridView GridRefractoryPeriods;
        private System.Windows.Forms.Label RefractoryPeriodsLabel;
        private System.Windows.Forms.Label InitialStateLabel;
        private System.Windows.Forms.DataGridView GridInitialState;
        private System.Windows.Forms.ToolStripMenuItem languageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modeToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown SpeedNumeric;
        private System.Windows.Forms.Label SpeedLabel;
        private System.Windows.Forms.TabPage ModePage;
        private System.Windows.Forms.Label ModelingTypeLabel;
        private System.Windows.Forms.CheckBox SaveGifCheckBox;
        private System.Windows.Forms.CheckBox ChartCheckBox;
        private System.Windows.Forms.CheckBox AnimationCheckBox;
        private System.Windows.Forms.CheckBox SandpileTypeCheckBox;
        private System.Windows.Forms.CheckBox BasicTypeCheckBox;
        private System.Windows.Forms.Label ActionsLabel;
        private System.Windows.Forms.ToolStripMenuItem darkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ResetToolStripMenuItem;
        private System.Windows.Forms.TextBox TimeTextBox;
    }
}


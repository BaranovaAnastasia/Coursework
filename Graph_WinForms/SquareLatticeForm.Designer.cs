namespace Graph_WinForms
{
    partial class SquareLatticeForm
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
            this.Yvalue = new System.Windows.Forms.NumericUpDown();
            this.SizeLabel = new System.Windows.Forms.Label();
            this.OK = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Xvalue = new System.Windows.Forms.NumericUpDown();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.Yvalue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Xvalue)).BeginInit();
            this.SuspendLayout();
            // 
            // Yvalue
            // 
            this.Yvalue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Yvalue.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.Yvalue.Location = new System.Drawing.Point(116, 54);
            this.Yvalue.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Yvalue.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.Yvalue.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.Yvalue.Name = "Yvalue";
            this.Yvalue.Size = new System.Drawing.Size(69, 42);
            this.Yvalue.TabIndex = 0;
            this.Yvalue.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // SizeLabel
            // 
            this.SizeLabel.AutoSize = true;
            this.SizeLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SizeLabel.Font = new System.Drawing.Font("Segoe UI Emoji", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SizeLabel.Location = new System.Drawing.Point(13, 12);
            this.SizeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.SizeLabel.Name = "SizeLabel";
            this.SizeLabel.Size = new System.Drawing.Size(369, 37);
            this.SizeLabel.TabIndex = 1;
            this.SizeLabel.Text = "Enter the Square Lattice Size";
            // 
            // OK
            // 
            this.OK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OK.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.OK.Location = new System.Drawing.Point(273, 189);
            this.OK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(112, 41);
            this.OK.TabIndex = 3;
            this.OK.Text = "Ok";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // Cancel
            // 
            this.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Cancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Cancel.Location = new System.Drawing.Point(13, 189);
            this.Cancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(112, 41);
            this.Cancel.TabIndex = 4;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(195, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "X";
            // 
            // Xvalue
            // 
            this.Xvalue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Xvalue.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.Xvalue.Location = new System.Drawing.Point(227, 54);
            this.Xvalue.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Xvalue.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.Xvalue.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.Xvalue.Name = "Xvalue";
            this.Xvalue.Size = new System.Drawing.Size(69, 42);
            this.Xvalue.TabIndex = 6;
            this.Xvalue.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(178, 105);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(113, 24);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // SquareLatticeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 244);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.Xvalue);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.SizeLabel);
            this.Controls.Add(this.Yvalue);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximumSize = new System.Drawing.Size(420, 300);
            this.Name = "SquareLatticeForm";
            this.Text = "Square Lattice";
            ((System.ComponentModel.ISupportInitialize)(this.Yvalue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Xvalue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown Yvalue;
        private System.Windows.Forms.Label SizeLabel;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown Xvalue;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}
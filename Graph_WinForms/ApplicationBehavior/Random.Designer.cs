namespace Graph_WinForms
{
    partial class Form2
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
            this.NumOfVertices = new System.Windows.Forms.NumericUpDown();
            this.VNLabel = new System.Windows.Forms.Label();
            this.VNRandom = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.NumOfVertices)).BeginInit();
            this.SuspendLayout();
            // 
            // NumOfVertices
            // 
            this.NumOfVertices.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NumOfVertices.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NumOfVertices.Location = new System.Drawing.Point(116, 54);
            this.NumOfVertices.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.NumOfVertices.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.NumOfVertices.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.NumOfVertices.Name = "NumOfVertices";
            this.NumOfVertices.Size = new System.Drawing.Size(180, 44);
            this.NumOfVertices.TabIndex = 0;
            this.NumOfVertices.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // VNLabel
            // 
            this.VNLabel.AutoSize = true;
            this.VNLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.VNLabel.Font = new System.Drawing.Font("Times New Roman", 14F);
            this.VNLabel.Location = new System.Drawing.Point(24, 17);
            this.VNLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.VNLabel.Name = "VNLabel";
            this.VNLabel.Size = new System.Drawing.Size(351, 33);
            this.VNLabel.TabIndex = 1;
            this.VNLabel.Text = "Choose the number of vertices";
            // 
            // VNRandom
            // 
            this.VNRandom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.VNRandom.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.VNRandom.Location = new System.Drawing.Point(116, 112);
            this.VNRandom.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.VNRandom.Name = "VNRandom";
            this.VNRandom.Size = new System.Drawing.Size(180, 49);
            this.VNRandom.TabIndex = 2;
            this.VNRandom.Text = "Random";
            this.VNRandom.UseVisualStyleBackColor = true;
            this.VNRandom.Click += new System.EventHandler(this.VNRandom_Click);
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.button2.Location = new System.Drawing.Point(280, 200);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 35);
            this.button2.TabIndex = 3;
            this.button2.Text = "Ok";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Cancel
            // 
            this.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Cancel.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.Cancel.Location = new System.Drawing.Point(10, 200);
            this.Cancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(112, 35);
            this.Cancel.TabIndex = 4;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 214);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.VNRandom);
            this.Controls.Add(this.VNLabel);
            this.Controls.Add(this.NumOfVertices);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximumSize = new System.Drawing.Size(410, 270);
            this.MinimumSize = new System.Drawing.Size(410, 270);
            this.Name = "Form2";
            this.Text = "b";
            ((System.ComponentModel.ISupportInitialize)(this.NumOfVertices)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown NumOfVertices;
        private System.Windows.Forms.Label VNLabel;
        private System.Windows.Forms.Button VNRandom;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button Cancel;
    }
}
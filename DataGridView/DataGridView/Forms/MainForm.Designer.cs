namespace DataGridView
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridView1 = new System.Windows.Forms.DataGridView();
            NameColumn = new DataGridViewTextBoxColumn();
            SizeColumn = new DataGridViewTextBoxColumn();
            Material = new DataGridViewTextBoxColumn();
            AmountColumn = new DataGridViewTextBoxColumn();
            MinCountColumn = new DataGridViewTextBoxColumn();
            PriceColumn = new DataGridViewTextBoxColumn();
            AllPriceColumn = new DataGridViewTextBoxColumn();
            panel1 = new Panel();
            label1 = new Label();
            btnAdd = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { NameColumn, SizeColumn, Material, AmountColumn, MinCountColumn, PriceColumn, AllPriceColumn });
            dataGridView1.Location = new Point(0, 126);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(971, 495);
            dataGridView1.TabIndex = 0;
            // 
            // NameColumn
            // 
            NameColumn.HeaderText = "Название";
            NameColumn.MinimumWidth = 6;
            NameColumn.Name = "NameColumn";
            NameColumn.Width = 125;
            // 
            // SizeColumn
            // 
            SizeColumn.HeaderText = "Размер";
            SizeColumn.MinimumWidth = 6;
            SizeColumn.Name = "SizeColumn";
            SizeColumn.Width = 125;
            // 
            // Material
            // 
            Material.HeaderText = "Материал";
            Material.MinimumWidth = 6;
            Material.Name = "Material";
            Material.Width = 125;
            // 
            // AmountColumn
            // 
            AmountColumn.HeaderText = "Количество на складе";
            AmountColumn.MinimumWidth = 6;
            AmountColumn.Name = "AmountColumn";
            AmountColumn.Width = 125;
            // 
            // MinCountColumn
            // 
            MinCountColumn.HeaderText = "Минимальный предел количества";
            MinCountColumn.MinimumWidth = 6;
            MinCountColumn.Name = "MinCountColumn";
            MinCountColumn.Width = 125;
            // 
            // PriceColumn
            // 
            PriceColumn.HeaderText = "Цена (без НДС)";
            PriceColumn.MinimumWidth = 6;
            PriceColumn.Name = "PriceColumn";
            PriceColumn.Width = 125;
            // 
            // AllPriceColumn
            // 
            AllPriceColumn.HeaderText = "Общая цена товара";
            AllPriceColumn.MinimumWidth = 6;
            AllPriceColumn.Name = "AllPriceColumn";
            AllPriceColumn.Width = 125;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(255, 255, 128);
            panel1.Controls.Add(btnAdd);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(971, 125);
            panel1.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Comic Sans MS", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.Location = new Point(336, 40);
            label1.Name = "label1";
            label1.Size = new Size(272, 39);
            label1.TabIndex = 0;
            label1.Text = "Просмотр товаров";
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(662, 50);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(94, 29);
            btnAdd.TabIndex = 2;
            btnAdd.Text = "Добавить";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click_1;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(971, 621);
            Controls.Add(panel1);
            Controls.Add(dataGridView1);
            Name = "MainForm";
            Text = "MainForm";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private Panel panel1;
        private Label label1;
        private DataGridViewTextBoxColumn NameColumn;
        private DataGridViewTextBoxColumn SizeColumn;
        private DataGridViewTextBoxColumn Material;
        private DataGridViewTextBoxColumn AmountColumn;
        private DataGridViewTextBoxColumn MinCountColumn;
        private DataGridViewTextBoxColumn PriceColumn;
        private DataGridViewTextBoxColumn AllPriceColumn;
        private Button btnAdd;
    }
}

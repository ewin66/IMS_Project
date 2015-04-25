namespace Viktor.IMS.Presentation.UI
{
    partial class Products
    {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.articlesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.productsDataGridView = new AC.ExtendedRenderer.Toolkit.KryptonGrid();
            this.tbColumnPLU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbColumnPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbColumnBar_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbColumnStock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbColumnUpdated = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lbltotalArticlesName = new System.Windows.Forms.Label();
            this.lblTotalArticles = new System.Windows.Forms.Label();
            this.lblArticlesWithStockName = new System.Windows.Forms.Label();
            this.lblArticlesWithStock = new System.Windows.Forms.Label();
            this.lblCumulativeAmountName = new System.Windows.Forms.Label();
            this.lblCumulativeAmount = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tbAddNewProduct = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.articlesBindingSource)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.productsDataGridView)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.productsDataGridView);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 52);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(828, 360);
            this.panel2.TabIndex = 8;
            // 
            // productsDataGridView
            // 
            this.productsDataGridView.AllowUserToAddRows = false;
            this.productsDataGridView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(232)))), ((int)(((byte)(246)))));
            this.productsDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.productsDataGridView.AutoGenerateColumns = false;
            this.productsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.productsDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.productsDataGridView.ColumnHeadersHeight = 25;
            this.productsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tbColumnPLU,
            this.tbColumnName,
            this.tbColumnPrice,
            this.tbColumnBar_code,
            this.tbColumnStock,
            this.tbColumnUpdated});
            this.productsDataGridView.DataSource = this.articlesBindingSource;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.productsDataGridView.DefaultCellStyle = dataGridViewCellStyle6;
            this.productsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.productsDataGridView.Location = new System.Drawing.Point(0, 0);
            this.productsDataGridView.Name = "productsDataGridView";
            this.productsDataGridView.ReadOnly = true;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.Padding = new System.Windows.Forms.Padding(1);
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.productsDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.productsDataGridView.RowHeadersWidth = 60;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle8.Padding = new System.Windows.Forms.Padding(3);
            this.productsDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.productsDataGridView.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Transparent;
            this.productsDataGridView.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Navy;
            this.productsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.productsDataGridView.Size = new System.Drawing.Size(828, 360);
            this.productsDataGridView.TabIndex = 1;
            this.productsDataGridView.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.productsDataGridView_CellMouseDoubleClick);
            // 
            // tbColumnPLU
            // 
            this.tbColumnPLU.DataPropertyName = "ProductId";
            this.tbColumnPLU.HeaderText = "Шифра";
            this.tbColumnPLU.Name = "tbColumnPLU";
            this.tbColumnPLU.ReadOnly = true;
            // 
            // tbColumnName
            // 
            this.tbColumnName.DataPropertyName = "ProductName";
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbColumnName.DefaultCellStyle = dataGridViewCellStyle3;
            this.tbColumnName.HeaderText = "Назив";
            this.tbColumnName.Name = "tbColumnName";
            this.tbColumnName.ReadOnly = true;
            // 
            // tbColumnPrice
            // 
            this.tbColumnPrice.DataPropertyName = "UnitPrice";
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbColumnPrice.DefaultCellStyle = dataGridViewCellStyle4;
            this.tbColumnPrice.HeaderText = "Цена";
            this.tbColumnPrice.Name = "tbColumnPrice";
            this.tbColumnPrice.ReadOnly = true;
            // 
            // tbColumnBar_code
            // 
            this.tbColumnBar_code.DataPropertyName = "BarCode1";
            this.tbColumnBar_code.HeaderText = "Баркод";
            this.tbColumnBar_code.Name = "tbColumnBar_code";
            this.tbColumnBar_code.ReadOnly = true;
            // 
            // tbColumnStock
            // 
            this.tbColumnStock.DataPropertyName = "UnitsInStock";
            this.tbColumnStock.HeaderText = "Количина";
            this.tbColumnStock.Name = "tbColumnStock";
            this.tbColumnStock.ReadOnly = true;
            // 
            // tbColumnUpdated
            // 
            this.tbColumnUpdated.DataPropertyName = "Updated";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.tbColumnUpdated.DefaultCellStyle = dataGridViewCellStyle5;
            this.tbColumnUpdated.HeaderText = "Последна промена";
            this.tbColumnUpdated.Name = "tbColumnUpdated";
            this.tbColumnUpdated.ReadOnly = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lbltotalArticlesName);
            this.panel1.Controls.Add(this.lblTotalArticles);
            this.panel1.Controls.Add(this.lblArticlesWithStockName);
            this.panel1.Controls.Add(this.lblArticlesWithStock);
            this.panel1.Controls.Add(this.lblCumulativeAmountName);
            this.panel1.Controls.Add(this.lblCumulativeAmount);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.tbAddNewProduct);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(828, 52);
            this.panel1.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(15, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Внеси назив:";
            // 
            // lbltotalArticlesName
            // 
            this.lbltotalArticlesName.AutoSize = true;
            this.lbltotalArticlesName.Location = new System.Drawing.Point(415, 3);
            this.lbltotalArticlesName.Name = "lbltotalArticlesName";
            this.lbltotalArticlesName.Size = new System.Drawing.Size(46, 13);
            this.lbltotalArticlesName.TabIndex = 4;
            this.lbltotalArticlesName.Text = "Вкупно:";
            // 
            // lblTotalArticles
            // 
            this.lblTotalArticles.AutoSize = true;
            this.lblTotalArticles.Location = new System.Drawing.Point(491, 3);
            this.lblTotalArticles.Name = "lblTotalArticles";
            this.lblTotalArticles.Size = new System.Drawing.Size(13, 13);
            this.lblTotalArticles.TabIndex = 5;
            this.lblTotalArticles.Text = "1";
            // 
            // lblArticlesWithStockName
            // 
            this.lblArticlesWithStockName.AutoSize = true;
            this.lblArticlesWithStockName.Location = new System.Drawing.Point(415, 17);
            this.lblArticlesWithStockName.Name = "lblArticlesWithStockName";
            this.lblArticlesWithStockName.Size = new System.Drawing.Size(66, 13);
            this.lblArticlesWithStockName.TabIndex = 6;
            this.lblArticlesWithStockName.Text = "Ажурирани:";
            // 
            // lblArticlesWithStock
            // 
            this.lblArticlesWithStock.AutoSize = true;
            this.lblArticlesWithStock.Location = new System.Drawing.Point(491, 17);
            this.lblArticlesWithStock.Name = "lblArticlesWithStock";
            this.lblArticlesWithStock.Size = new System.Drawing.Size(13, 13);
            this.lblArticlesWithStock.TabIndex = 7;
            this.lblArticlesWithStock.Text = "2";
            // 
            // lblCumulativeAmountName
            // 
            this.lblCumulativeAmountName.AutoSize = true;
            this.lblCumulativeAmountName.Location = new System.Drawing.Point(415, 31);
            this.lblCumulativeAmountName.Name = "lblCumulativeAmountName";
            this.lblCumulativeAmountName.Size = new System.Drawing.Size(79, 13);
            this.lblCumulativeAmountName.TabIndex = 8;
            this.lblCumulativeAmountName.Text = "Вкупен износ:";
            // 
            // lblCumulativeAmount
            // 
            this.lblCumulativeAmount.AutoSize = true;
            this.lblCumulativeAmount.Location = new System.Drawing.Point(491, 31);
            this.lblCumulativeAmount.Name = "lblCumulativeAmount";
            this.lblCumulativeAmount.Size = new System.Drawing.Size(13, 13);
            this.lblCumulativeAmount.TabIndex = 9;
            this.lblCumulativeAmount.Text = "3";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox1.Location = new System.Drawing.Point(100, 14);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(223, 26);
            this.textBox1.TabIndex = 2;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.Enter += new System.EventHandler(this.textBox1_Enter);
            // 
            // tbAddNewProduct
            // 
            this.tbAddNewProduct.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbAddNewProduct.Location = new System.Drawing.Point(741, 17);
            this.tbAddNewProduct.Name = "tbAddNewProduct";
            this.tbAddNewProduct.Size = new System.Drawing.Size(75, 23);
            this.tbAddNewProduct.TabIndex = 10;
            this.tbAddNewProduct.Text = "Додај нов";
            this.tbAddNewProduct.UseVisualStyleBackColor = true;
            this.tbAddNewProduct.Click += new System.EventHandler(this.btnAddProduct_Click);
            // 
            // Products
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 412);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Products";
            this.Text = "Преглед на Производи";
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.Deactivate += new System.EventHandler(this.MainForm_Deactivate);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.articlesBindingSource)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.productsDataGridView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Label lblCumulativeAmount;
    private System.Windows.Forms.Label lblCumulativeAmountName;
    private System.Windows.Forms.Label lblArticlesWithStock;
    private System.Windows.Forms.Label lblArticlesWithStockName;
    private System.Windows.Forms.Label lblTotalArticles;
    private System.Windows.Forms.Label lbltotalArticlesName;
    private AC.ExtendedRenderer.Toolkit.KryptonGrid productsDataGridView;
    private System.Windows.Forms.BindingSource articlesBindingSource;
    private System.Windows.Forms.DataGridViewTextBoxColumn tbColumnPLU;
    private System.Windows.Forms.DataGridViewTextBoxColumn tbColumnName;
    private System.Windows.Forms.DataGridViewTextBoxColumn tbColumnPrice;
    private System.Windows.Forms.DataGridViewTextBoxColumn tbColumnBar_code;
    private System.Windows.Forms.DataGridViewTextBoxColumn tbColumnStock;
    private System.Windows.Forms.DataGridViewTextBoxColumn tbColumnUpdated;
  }
}


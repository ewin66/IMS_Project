﻿using Viktor.IMS.Presentation.UIExtensions;

namespace Viktor.IMS.Presentation.UI
{
    partial class ProductDetails
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
            this.textBox3 = new AMS.TextBox.NumericTextBox();
            this.textBox2 = new AMS.TextBox.NumericTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.rbDomestic = new System.Windows.Forms.RadioButton();
            this.rbForeign = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblError = new System.Windows.Forms.Label();
            this.comboCategory = new System.Windows.Forms.ComboBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblReorderLevel = new System.Windows.Forms.Label();
            this.txtReorderLevel = new AMS.TextBox.NumericTextBox();
            this.btnAddNewCategory = new System.Windows.Forms.Button();
            this.txtUnitPurchasePrice = new AMS.TextBox.NumericTextBox();
            this.lblUnitPurchasePrice = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox3
            // 
            this.textBox3.AllowNegative = true;
            this.textBox3.DigitsInGroup = 0;
            this.textBox3.Flags = 0;
            this.textBox3.Location = new System.Drawing.Point(224, 86);
            this.textBox3.MaxDecimalPlaces = 4;
            this.textBox3.MaxWholeDigits = 9;
            this.textBox3.Name = "textBox3";
            this.textBox3.Prefix = "";
            this.textBox3.RangeMax = 1.7976931348623157E+308D;
            this.textBox3.RangeMin = -1.7976931348623157E+308D;
            this.textBox3.Size = new System.Drawing.Size(129, 23);
            this.textBox3.TabIndex = 2;
            // 
            // textBox2
            // 
            this.textBox2.AllowNegative = false;
            this.textBox2.DigitsInGroup = 0;
            this.textBox2.Flags = 65536;
            this.textBox2.Location = new System.Drawing.Point(68, 86);
            this.textBox2.MaxDecimalPlaces = 4;
            this.textBox2.MaxWholeDigits = 9;
            this.textBox2.Name = "textBox2";
            this.textBox2.Prefix = "";
            this.textBox2.RangeMax = 1.7976931348623157E+308D;
            this.textBox2.RangeMin = -1.7976931348623157E+308D;
            this.textBox2.Size = new System.Drawing.Size(129, 23);
            this.textBox2.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.Location = new System.Drawing.Point(207, 275);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 28);
            this.button2.TabIndex = 5;
            this.button2.Text = "Откажи";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(68, 275);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 28);
            this.button1.TabIndex = 4;
            this.button1.Text = "Зачувај";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(65, 215);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Баркод";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(221, 66);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Количина";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(65, 66);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Продажна цена";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(65, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Назив";
            // 
            // textBox4
            // 
            this.textBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox4.Location = new System.Drawing.Point(68, 236);
            this.textBox4.Margin = new System.Windows.Forms.Padding(4);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(285, 23);
            this.textBox4.TabIndex = 3;
            // 
            // textBox1
            // 
            this.textBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox1.Location = new System.Drawing.Point(68, 33);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.MaxLength = 22;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(285, 23);
            this.textBox1.TabIndex = 0;
            this.textBox1.Enter += new System.EventHandler(this.textBox1_Enter);
            // 
            // rbDomestic
            // 
            this.rbDomestic.AutoSize = true;
            this.rbDomestic.ForeColor = System.Drawing.Color.DimGray;
            this.rbDomestic.Location = new System.Drawing.Point(8, 15);
            this.rbDomestic.Name = "rbDomestic";
            this.rbDomestic.Size = new System.Drawing.Size(116, 21);
            this.rbDomestic.TabIndex = 8;
            this.rbDomestic.Text = "Македонски";
            this.rbDomestic.UseVisualStyleBackColor = true;
            // 
            // rbForeign
            // 
            this.rbForeign.AutoSize = true;
            this.rbForeign.ForeColor = System.Drawing.Color.DimGray;
            this.rbForeign.Location = new System.Drawing.Point(156, 15);
            this.rbForeign.Name = "rbForeign";
            this.rbForeign.Size = new System.Drawing.Size(79, 21);
            this.rbForeign.TabIndex = 9;
            this.rbForeign.Text = "Увозен";
            this.rbForeign.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbDomestic);
            this.groupBox1.Controls.Add(this.rbForeign);
            this.groupBox1.Location = new System.Drawing.Point(68, 163);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(285, 42);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lblError.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(73, 239);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(0, 16);
            this.lblError.TabIndex = 11;
            // 
            // comboCategory
            // 
            this.comboCategory.FormattingEnabled = true;
            this.comboCategory.Location = new System.Drawing.Point(399, 32);
            this.comboCategory.Name = "comboCategory";
            this.comboCategory.Size = new System.Drawing.Size(238, 24);
            this.comboCategory.TabIndex = 14;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCategory.ForeColor = System.Drawing.Color.DimGray;
            this.lblCategory.Location = new System.Drawing.Point(396, 12);
            this.lblCategory.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(90, 17);
            this.lblCategory.TabIndex = 15;
            this.lblCategory.Text = "Категорија";
            // 
            // lblReorderLevel
            // 
            this.lblReorderLevel.AutoSize = true;
            this.lblReorderLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblReorderLevel.ForeColor = System.Drawing.Color.DimGray;
            this.lblReorderLevel.Location = new System.Drawing.Point(396, 66);
            this.lblReorderLevel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReorderLevel.Name = "lblReorderLevel";
            this.lblReorderLevel.Size = new System.Drawing.Size(169, 17);
            this.lblReorderLevel.TabIndex = 16;
            this.lblReorderLevel.Text = "Минимална количина";
            // 
            // txtReorderLevel
            // 
            this.txtReorderLevel.AllowNegative = false;
            this.txtReorderLevel.DigitsInGroup = 0;
            this.txtReorderLevel.Flags = 65536;
            this.txtReorderLevel.Location = new System.Drawing.Point(399, 86);
            this.txtReorderLevel.MaxDecimalPlaces = 4;
            this.txtReorderLevel.MaxWholeDigits = 9;
            this.txtReorderLevel.Name = "txtReorderLevel";
            this.txtReorderLevel.Prefix = "";
            this.txtReorderLevel.RangeMax = 1.7976931348623157E+308D;
            this.txtReorderLevel.RangeMin = -1.7976931348623157E+308D;
            this.txtReorderLevel.Size = new System.Drawing.Size(238, 23);
            this.txtReorderLevel.TabIndex = 17;
            // 
            // btnAddNewCategory
            // 
            this.btnAddNewCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAddNewCategory.Location = new System.Drawing.Point(399, 128);
            this.btnAddNewCategory.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddNewCategory.Name = "btnAddNewCategory";
            this.btnAddNewCategory.Size = new System.Drawing.Size(187, 28);
            this.btnAddNewCategory.TabIndex = 18;
            this.btnAddNewCategory.Text = "Додај нова категорија";
            this.btnAddNewCategory.UseVisualStyleBackColor = true;
            this.btnAddNewCategory.Click += new System.EventHandler(this.btnAddNewCategory_Click);
            // 
            // txtUnitPurchasePrice
            // 
            this.txtUnitPurchasePrice.AllowNegative = false;
            this.txtUnitPurchasePrice.DigitsInGroup = 0;
            this.txtUnitPurchasePrice.Flags = 65536;
            this.txtUnitPurchasePrice.Location = new System.Drawing.Point(68, 138);
            this.txtUnitPurchasePrice.MaxDecimalPlaces = 4;
            this.txtUnitPurchasePrice.MaxWholeDigits = 9;
            this.txtUnitPurchasePrice.Name = "txtUnitPurchasePrice";
            this.txtUnitPurchasePrice.Prefix = "";
            this.txtUnitPurchasePrice.RangeMax = 1.7976931348623157E+308D;
            this.txtUnitPurchasePrice.RangeMin = -1.7976931348623157E+308D;
            this.txtUnitPurchasePrice.Size = new System.Drawing.Size(129, 23);
            this.txtUnitPurchasePrice.TabIndex = 19;
            // 
            // lblUnitPurchasePrice
            // 
            this.lblUnitPurchasePrice.AutoSize = true;
            this.lblUnitPurchasePrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblUnitPurchasePrice.ForeColor = System.Drawing.Color.DimGray;
            this.lblUnitPurchasePrice.Location = new System.Drawing.Point(65, 118);
            this.lblUnitPurchasePrice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUnitPurchasePrice.Name = "lblUnitPurchasePrice";
            this.lblUnitPurchasePrice.Size = new System.Drawing.Size(113, 17);
            this.lblUnitPurchasePrice.TabIndex = 20;
            this.lblUnitPurchasePrice.Text = "Набавна цена";
            // 
            // ProductDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 323);
            this.Controls.Add(this.txtUnitPurchasePrice);
            this.Controls.Add(this.lblUnitPurchasePrice);
            this.Controls.Add(this.btnAddNewCategory);
            this.Controls.Add(this.txtReorderLevel);
            this.Controls.Add(this.lblReorderLevel);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.comboCategory);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ProductDetails";
            this.Text = "Детали за производот";
            this.Activated += new System.EventHandler(this.RowDetails_Activated);
            this.Load += new System.EventHandler(this.RowDetails_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private AMS.TextBox.NumericTextBox textBox2;
        private AMS.TextBox.NumericTextBox textBox3;
        private System.Windows.Forms.RadioButton rbDomestic;
        private System.Windows.Forms.RadioButton rbForeign;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.ComboBox comboCategory;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblReorderLevel;
        private AMS.TextBox.NumericTextBox txtReorderLevel;
        private System.Windows.Forms.Button btnAddNewCategory;
        private AMS.TextBox.NumericTextBox txtUnitPurchasePrice;
        private System.Windows.Forms.Label lblUnitPurchasePrice;
    }
}
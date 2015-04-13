using AC.ExtendedRenderer.Navigator;
using AC.ExtendedRenderer.Toolkit.Drawing;
using System.Drawing;
using System.Windows.Forms;
namespace Viktor.IMS.Presentation.UI
{
    partial class HomeTabbed
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
            this.tabContainer = new AC.ExtendedRenderer.Navigator.KryptonTabControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.exitButton = new System.Windows.Forms.ToolStripButton();
            this.productsButton = new System.Windows.Forms.ToolStripButton();
            this.saleButton = new System.Windows.Forms.ToolStripButton();
            this.saleHomeButton = new System.Windows.Forms.ToolStripButton();
            this.reverseInitDevices = new System.Windows.Forms.ToolStripButton();
            this.showReportButton = new System.Windows.Forms.ToolStripButton();
            this.orderDetailsButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabContainer
            // 
            this.tabContainer.AllowCloseButton = true;
            this.tabContainer.AllowContextButton = true;
            this.tabContainer.AllowNavigatorButtons = true;
            this.tabContainer.AllowSelectedTabHigh = true;
            this.tabContainer.BorderWidth = 1;
            this.tabContainer.CornerRoundRadiusWidth = 12;
            this.tabContainer.CornerSymmetry = AC.ExtendedRenderer.Navigator.KryptonTabControl.CornSymmetry.Both;
            this.tabContainer.CornerType = AC.ExtendedRenderer.Toolkit.Drawing.DrawingMethods.CornerType.Rounded;
            this.tabContainer.CornerWidth = AC.ExtendedRenderer.Navigator.KryptonTabControl.CornWidth.Thin;
            this.tabContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabContainer.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabContainer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabContainer.HotTrack = true;
            this.tabContainer.ItemSize = new System.Drawing.Size(77, 25);
            this.tabContainer.Location = new System.Drawing.Point(0, 39);
            this.tabContainer.Name = "tabContainer";
            this.tabContainer.PreserveTabColor = false;
            this.tabContainer.SelectedIndex = 0;
            this.tabContainer.Size = new System.Drawing.Size(822, 371);
            this.tabContainer.TabIndex = 0;
            this.tabContainer.UseExtendedLayout = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitButton,
            this.productsButton,
            this.saleButton,
            this.saleHomeButton,
            this.reverseInitDevices,
            this.showReportButton,
            this.orderDetailsButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(822, 39);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // exitButton
            // 
            this.exitButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.exitButton.Image = global::Viktor.IMS.Presentation.Properties.Resources.Exit32;
            this.exitButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(36, 36);
            this.exitButton.ToolTipText = "Излез";
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // productsButton
            // 
            this.productsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.productsButton.Image = global::Viktor.IMS.Presentation.Properties.Resources.Goods32;
            this.productsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.productsButton.Margin = new System.Windows.Forms.Padding(20, 1, 0, 2);
            this.productsButton.Name = "productsButton";
            this.productsButton.Size = new System.Drawing.Size(36, 36);
            this.productsButton.ToolTipText = "Производи";
            this.productsButton.Click += new System.EventHandler(this.productsButton_Click);
            // 
            // saleButton
            // 
            this.saleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saleButton.Image = global::Viktor.IMS.Presentation.Properties.Resources.TradePoint32;
            this.saleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saleButton.Margin = new System.Windows.Forms.Padding(20, 1, 0, 2);
            this.saleButton.Name = "saleButton";
            this.saleButton.Size = new System.Drawing.Size(36, 36);
            this.saleButton.ToolTipText = "Продажба";
            this.saleButton.Click += new System.EventHandler(this.saleButton_Click);
            // 
            // saleHomeButton
            // 
            this.saleHomeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saleHomeButton.Image = global::Viktor.IMS.Presentation.Properties.Resources.Location32;
            this.saleHomeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saleHomeButton.Margin = new System.Windows.Forms.Padding(20, 1, 0, 2);
            this.saleHomeButton.Name = "saleHomeButton";
            this.saleHomeButton.Size = new System.Drawing.Size(36, 36);
            this.saleHomeButton.Click += new System.EventHandler(this.saleHomeButton_Click);
            // 
            // reverseInitDevices
            // 
            this.reverseInitDevices.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.reverseInitDevices.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.reverseInitDevices.Image = global::Viktor.IMS.Presentation.Properties.Resources.Settings32;
            this.reverseInitDevices.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.reverseInitDevices.Margin = new System.Windows.Forms.Padding(20, 1, 0, 2);
            this.reverseInitDevices.Name = "reverseInitDevices";
            this.reverseInitDevices.Size = new System.Drawing.Size(36, 36);
            this.reverseInitDevices.Text = "Повторна иницијализација на уреди";
            this.reverseInitDevices.Visible = false;
            this.reverseInitDevices.Click += new System.EventHandler(this.reverseInitDevices_Click);
            // 
            // showReportButton
            // 
            this.showReportButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.showReportButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.showReportButton.Image = global::Viktor.IMS.Presentation.Properties.Resources.Reports32;
            this.showReportButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showReportButton.Margin = new System.Windows.Forms.Padding(20, 1, 0, 2);
            this.showReportButton.Name = "showReportButton";
            this.showReportButton.Size = new System.Drawing.Size(36, 36);
            this.showReportButton.ToolTipText = "Потрошувачка";
            this.showReportButton.Click += new System.EventHandler(this.showReportButton_Click);
            // 
            // orderDetailsButton
            // 
            this.orderDetailsButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.orderDetailsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.orderDetailsButton.Image = global::Viktor.IMS.Presentation.Properties.Resources.History32;
            this.orderDetailsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.orderDetailsButton.Margin = new System.Windows.Forms.Padding(20, 1, 0, 2);
            this.orderDetailsButton.Name = "orderDetailsButton";
            this.orderDetailsButton.Size = new System.Drawing.Size(36, 36);
            this.orderDetailsButton.Text = "toolStripButton1";
            this.orderDetailsButton.ToolTipText = "Историја на продажба";
            this.orderDetailsButton.Click += new System.EventHandler(this.orderDetailsButton_Click);
            // 
            // HomeTabbed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 410);
            this.Controls.Add(this.tabContainer);
            this.Controls.Add(this.toolStrip1);
            this.IsMdiContainer = true;
            this.Name = "HomeTabbed";
            this.Text = "Главно мени";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton exitButton;
        private System.Windows.Forms.ToolStripButton productsButton;
        private System.Windows.Forms.ToolStripButton saleButton;
        private KryptonTabControl tabContainer;
        private ToolStripButton saleHomeButton;
        private ToolStripButton reverseInitDevices;
        private ToolStripButton showReportButton;
        private ToolStripButton orderDetailsButton;
    }
}
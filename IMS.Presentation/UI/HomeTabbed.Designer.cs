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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.exitButton = new System.Windows.Forms.ToolStripButton();
            this.productsButton = new System.Windows.Forms.ToolStripButton();
            this.saleButton = new System.Windows.Forms.ToolStripButton();
            this.tabContainer = new AC.ExtendedRenderer.Navigator.KryptonTabControl();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitButton,
            this.productsButton,
            this.saleButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(760, 39);
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
            this.saleButton.Name = "saleButton";
            this.saleButton.Size = new System.Drawing.Size(36, 36);
            this.saleButton.ToolTipText = "Продажба";
            this.saleButton.Click += new System.EventHandler(this.saleButton_Click);
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
            this.tabContainer.HotTrack = true;
            this.tabContainer.ItemSize = new System.Drawing.Size(77, 25);
            this.tabContainer.Location = new System.Drawing.Point(0, 39);
            this.tabContainer.Name = "tabContainer";
            this.tabContainer.PreserveTabColor = false;
            this.tabContainer.SelectedIndex = 0;
            this.tabContainer.Size = new System.Drawing.Size(760, 374);
            this.tabContainer.TabIndex = 0;
            this.tabContainer.UseExtendedLayout = false;
            //this.tabContainer.SelectedIndexChanged += new System.EventHandler(this.tabContainer_SelectedIndexChanged);
            this.tabContainer.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabContainer_Selected);
            //this.tabContainer.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.tabContainer_ControlAdded);
            // 
            // HomeTabbed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 413);
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
    }
}
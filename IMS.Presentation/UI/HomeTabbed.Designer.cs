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
            
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
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
            #region Tab Container
            tabContainer = new KryptonTabControl();
            this.tabContainer.AllowCloseButton = true;
            this.tabContainer.AllowContextButton = true;
            this.tabContainer.AllowNavigatorButtons = true;
            this.tabContainer.AllowSelectedTabHigh = true;
            this.tabContainer.BorderWidth = 1;
            this.tabContainer.CornerRoundRadiusWidth = 12;
            this.tabContainer.CornerSymmetry = KryptonTabControl.CornSymmetry.Both;
            this.tabContainer.CornerType = DrawingMethods.CornerType.Rounded;
            this.tabContainer.CornerWidth = KryptonTabControl.CornWidth.Thin;
            this.tabContainer.Dock = DockStyle.Fill;
            this.tabContainer.DrawMode = TabDrawMode.OwnerDrawFixed;
            this.tabContainer.HotTrack = true;
            this.tabContainer.ItemSize = new Size(77, 25);

            //this.tabContainer.Location = new System.Drawing.Point(0, 39);
            //this.tabContainer.Size = new System.Drawing.Size(760, 374);
            //this.tabContainer.TabIndex = 3;

            this.tabContainer.Location = new Point(0, 0);
            this.tabContainer.Name = "tabContainer";
            this.tabContainer.PreserveTabColor = false;
            this.tabContainer.SelectedIndex = 0;
            this.tabContainer.Size = new Size(641, 501);
            this.tabContainer.TabIndex = 0;
            this.tabContainer.UseExtendedLayout = false;
            #endregion
            //this.tabContainer.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabContainer_DrawItem);
            //this.tabContainer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabContainer_MouseDown);
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
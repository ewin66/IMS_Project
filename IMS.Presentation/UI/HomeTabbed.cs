using AC.ExtendedRenderer.Navigator;
using AC.ExtendedRenderer.Toolkit.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Viktor.IMS.Presentation.Enums;

namespace Viktor.IMS.Presentation.UI
{
    public partial class HomeTabbed : BaseForm
    {
        
        private bool m_bLayoutCalled = false;
        private DateTime m_dt;

        public HomeTabbed()
        {
            InitializeComponent();
            

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
            //this.TopMost = true;
            this.WindowState = FormWindowState.Maximized;
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.SplashScreen_Layout);            
        }

        private void SplashScreen_Layout(object sender, System.Windows.Forms.LayoutEventArgs e)
        {
            if (m_bLayoutCalled == false)
            {
                m_bLayoutCalled = true;
                m_dt = DateTime.Now;
                this.Activate();
                SplashScreen.SplashScreen.CloseForm();
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                Application.OpenForms[i].Close();
            }
            System.Windows.Forms.Application.Exit();
        }

        private void productsButton_Click(object sender, EventArgs e)
        {
            /*
            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                if (Application.OpenForms[i].Name != "HomeTabbed")
                    Application.OpenForms[i].Close();
            } 
            */
            MainForm main = new MainForm();
            main._repository = this._repository;
            main.FormBorderStyle = FormBorderStyle.None;
            main.TopLevel = false;

            //Added new TabPage
            TabPage tabPage = NewTabPage("Производи", "tbpProducts", (Control)main);
            this.tabContainer.TabPages.Add(tabPage);
            

            //Added form to tabpage
            main.Dock = DockStyle.Fill;
            main.WindowState = FormWindowState.Maximized;
            main.Show();

            // Duri otkako formata ke se loadira go selektirame soodvetniot tab za aktiven
            this.tabContainer.SelectTab(tabPage);
            this.tabContainer.SelectedTab.Controls[0].Select();
        }

        private void saleButton_Click(object sender, EventArgs e)
        {
            /*
            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                if (Application.OpenForms[i].Name != "HomeTabbed")
                    Application.OpenForms[i].Close();
            }
            */
            Sale sale = new Sale(CustomerType.RETAIL);
            sale.FormBorderStyle = FormBorderStyle.None;
            sale._repository = this._repository;
            sale.TopLevel = false;

            //Added new TabPage
            TabPage tabPage = NewTabPage("Продажба", "tbpSale", (Control)sale);
            this.tabContainer.TabPages.Add(tabPage);
            

            //Added form to tabpage
            sale.Dock = DockStyle.Fill;
            sale.WindowState = FormWindowState.Maximized;
            sale.Show();

            this.tabContainer.SelectTab(tabPage);
            this.tabContainer.SelectedTab.Controls[0].Select();
        }

        private void saleHomeButton_Click(object sender, EventArgs e)
        {
            /*
            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                if (Application.OpenForms[i].Name != "HomeTabbed")
                    Application.OpenForms[i].Close();
            }
            */
            Sale sale = new Sale(CustomerType.HOME);
            sale.FormBorderStyle = FormBorderStyle.None;
            sale._repository = this._repository;
            sale.TopLevel = false;

            //Added new TabPage
            TabPage tabPage = NewTabPage("Продажба", "tbpSale", (Control)sale);
            this.tabContainer.TabPages.Add(tabPage);


            //Added form to tabpage
            sale.Dock = DockStyle.Fill;
            sale.WindowState = FormWindowState.Maximized;
            sale.Show();

            this.tabContainer.SelectTab(tabPage);
            this.tabContainer.SelectedTab.Controls[0].Select();
        }
        /* Comment
        private void tabContainer_DrawItem(object sender, DrawItemEventArgs e)
        {
            //This code will render a "x" mark at the end of the Tab caption. 
            e.Graphics.DrawString("x", e.Font, Brushes.Black, e.Bounds.Right - 15, e.Bounds.Top + 4);
            e.Graphics.DrawString(this.tabContainer.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + 12, e.Bounds.Top + 4);
            e.DrawFocusRectangle();
        }

        private void tabContainer_MouseDown(object sender, MouseEventArgs e)
        {
            //Looping through the controls.
            for (int i = 0; i < this.tabContainer.TabPages.Count; i++)
            {
                Rectangle r = tabContainer.GetTabRect(i);
                //Getting the position of the "x" mark.
                Rectangle closeButton = new Rectangle(r.Right - 15, r.Top + 4, 9, 7);
                if (closeButton.Contains(e.Location))
                {
                    if (MessageBox.Show("Would you like to Close this Tab?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.tabContainer.TabPages.RemoveAt(i);
                        break;
                    }
                }
            }
        }
        */

        private TabPage NewTabPage(string text, string name, Control control)
        {
            TabPage tabPage = new TabPage();
            tabPage.BackColor = SystemColors.Window;
            tabPage.Controls.Add(control);
            tabPage.Location = new Point(4, 29);
            tabPage.Margin = new Padding(0);
            tabPage.Name = "tabPage1";
            tabPage.Size = new Size(633, 468);
            tabPage.TabIndex = 0;
            tabPage.Tag = (object)false;
            tabPage.Text = string.Format(" {0} ", text);
            tabPage.Name = name;
            return tabPage;
        }

        private void tabContainer_Selected(object sender, EventArgs e)
        {
            if (this.tabContainer.SelectedTab != null)
                this.tabContainer.SelectedTab.Controls[0].Select();
        }

        private void tabContainer_Selected(object sender, TabControlEventArgs e)
        {

        }

    }
}

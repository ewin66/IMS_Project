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
        private int _oldIndex = -1;

        public HomeTabbed()
        {
            InitializeComponent();

            #region Custom Init
            this.tabContainer.Deselecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabContainer_Deselecting);
            #endregion

            //this.TopMost = true;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
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
            TabPage tabPage = NewTabPage("Продажба дома", "tbpSale", (Control)sale);
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

        private void tabContainer_Selected(object sender, TabControlEventArgs e)
        {
            if (this.tabContainer.SelectedTab != null)
            {
                BaseForm baseForm = (BaseForm)this.tabContainer.SelectedTab.Controls.Cast<Control>().FirstOrDefault(x => x is BaseForm);
                if (baseForm != null)
                {
                    baseForm.Select();
                    baseForm.listener.Resume();
                }
            }
        }
        private void tabContainer_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            //_oldIndex = e.TabPageIndex;
            TabPage previousTab = e.TabPage;
            BaseForm baseForm = (BaseForm)previousTab.Controls.Cast<Control>().FirstOrDefault(x => x is BaseForm);
            if (baseForm != null)
            {
                baseForm.Select();
                baseForm.listener.Pause();
            }
        }


        /// <summary>
        ///  Sega za sega ne se koristat ovie metodi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (_oldIndex != -1)
            {
                if (CanChangeTab(_oldIndex, e.TabPageIndex))
                {
                    e.Cancel = true;
                }
            }
        }
        private bool CanChangeTab(int fromIndex, int toIndex)
        {
            // Put your logic here
            return true;
        }
        private void tabContainer_Selected()
        {
            //var picBox = this.tabContainer.TabPages.Cast<Control>()
            //.SelectMany(page => page.Controls.OfType<PictureBox>())
            //.First();
            //picBox.ImageLocation = "...";
            //TabPage tabPage = this.tabContainer.SelectedTab.Controls[0];
            //if (this.tabContainer.SelectedTab.Controls[0].Contains(BaseForm)     Contains.ContainsKey("rtb"))
            //RichTextBox selectedRtb = (RichTextBox)selectedTab.Controls["rtb"];
        }
    }
}

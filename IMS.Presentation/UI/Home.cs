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
    public partial class Home : BaseForm
    {
        private bool m_bLayoutCalled = false;
        private DateTime m_dt;

        public Home()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
            this.TopMost = true;
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
            System.Windows.Forms.Application.Exit();
        }

        private void productsButton_Click(object sender, EventArgs e)
        {
            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                if (Application.OpenForms[i].Name != "Home")
                    Application.OpenForms[i].Close();
            } 
            
            MainForm main = new MainForm();
            //main.MdiParent = this;
            //main.Parent = this.formContainer;
            //main.StartPosition = FormStartPosition.CenterScreen;

            main._repository = this._repository;
            //main.listener = this.listener;
            //main.Owner = this;
            main.Show();
        }

        private void saleButton_Click(object sender, EventArgs e)
        {
            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                if (Application.OpenForms[i].Name != "Home")
                    Application.OpenForms[i].Close();
            }
            Sale sale = new Sale(CustomerType.RETAIL);
            //sale.MdiParent = this;
            //sale.Parent = this.formContainer;
            //sale.StartPosition = FormStartPosition.CenterScreen;
            sale._repository = this._repository;
            //sale.listener = this.listener;
            //sale.Owner = this;
            sale.Show();
        }
    }
}

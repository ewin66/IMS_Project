using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Viktor.IMS.Presentation.UI
{
    public partial class Home : BaseForm
    {
        private bool m_bLayoutCalled = false;
        private DateTime m_dt;

        public Home()
        {
            InitializeComponent();
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
            MainForm main = new MainForm();
            main._repository = this._repository;
            main.Owner = this;
            main.Show();
        }

        private void saleButton_Click(object sender, EventArgs e)
        {
            Sale sale = new Sale();
            sale._repository = this._repository;
            sale.Owner = this;
            sale.Show();
        }
    }
}

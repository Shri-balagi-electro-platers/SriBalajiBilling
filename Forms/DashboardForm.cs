using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SriBalajiBilling.Forms
{
   

    public partial class DashboardForm : Form
    {
        public DashboardForm()
        {
            InitializeComponent();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        ProductForm productForm;
        private void productToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(productForm == null)
            {
                productForm = new ProductForm();
                productForm.MdiParent = this;
            }
            productForm.Show();
            productForm.Activate();
            
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {

        }
        BillForm billingForm;
        private void billingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (billingForm == null)
            {
                billingForm = new BillForm();
                billingForm.MdiParent = this;
            }
            billingForm.Show();
        }
    }
}

﻿using SriBalajiBilling.service;
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
    public partial class BillForm : Form
    {

        ProductService productService;
        BillService billService;
        public BillForm()
        {
            productService = new ProductService();
            billService = new BillService();
            InitializeComponent();
        }

        private void BillingForm_Load(object sender, EventArgs e)
        {
            List<string> productNames = productService.GetAllProductNames();
            comboproductName.DataSource = productNames;
            LoadBillNo();
        }

        private void LoadBillNo()
        {
            var billno = billService.GenerateBillNumber();
            txtBillNo.Text = billno.ToString();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
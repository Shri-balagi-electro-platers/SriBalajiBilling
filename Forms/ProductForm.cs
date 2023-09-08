using MongoDB.Bson;
using MongoDB.Driver;
using SriBalajiBilling.Models;
using SriBalajiBilling.service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SriBalajiBilling.Forms
{
    public partial class ProductForm : Form
    {
        ProductService productService;

        public ProductForm()
        {
            productService = new ProductService();
            InitializeComponent();
            DgvDisplayProduct();
            //this.ControlBox = false;
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {

        }

        private void productId_Click(object sender, EventArgs e)
        {

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtProductId.Text = productsdgv.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtProductName.Text = productsdgv.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtProductPrice.Text = productsdgv.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtProductDescription.Text = productsdgv.Rows[e.RowIndex].Cells[3].Value.ToString();
            
        }
        
        
        // custom methods
        public void DgvDisplayProduct()
        {
            List<Product> products = productService.GetAllProducts();
            //Console.WriteLine(products[0].ToProductString());
            productsdgv.DataSource = products;
            if(products != null && products.Count > 0)
            {
                //txtProductId.Text = Productsdgv.Rows[0].Cells[0].Value.ToString();
                txtProductName.Text = productsdgv.Rows[0].Cells[1].Value.ToString();
                txtProductPrice.Text = productsdgv.Rows[0].Cells[2].Value.ToString();
                txtProductDescription.Text = productsdgv.Rows[0].Cells[3].Value.ToString();
            }
        }

        public void DgvDisplayProductByKeyword(List<Product> listOfProducts)
        {
            
            //Console.WriteLine(products[0].ToProductString());
            productsdgv.DataSource = listOfProducts;
            if (listOfProducts != null && listOfProducts.Count > 0)
            {
                //txtProductId.Text = Productsdgv.Rows[0].Cells[0].Value.ToString();
                txtProductName.Text = productsdgv.Rows[0].Cells[1].Value.ToString();
                txtProductPrice.Text = productsdgv.Rows[0].Cells[2].Value.ToString();
                txtProductDescription.Text = productsdgv.Rows[0].Cells[3].Value.ToString();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Product product = new Product();
            product.name = txtProductName.Text;
            product.description = txtProductDescription.Text;
            product.price =   Double.Parse( txtProductPrice.Text);
            var boolAddedResult = productService.addProduct(product);
            if (boolAddedResult)
            {
                DgvDisplayProduct();
            }
        }

        

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Product product = new Product();
            var productId = txtProductId.Text;
            product.name = txtProductName.Text;
            product.description = txtProductDescription.Text;
            product.price = Double.Parse(txtProductPrice.Text);
            var isDataupdated = productService.updateProduct(product,productId);
            if (isDataupdated)
            {
                DgvDisplayProduct();
            }
            
            //var updateProductData = Builders<Product>.Update
            //    .Set(p=>p.name,product.name)
            //    .Set(p=>p.description,product.description)
            //    .Set(p=>p.price,product.price);
            
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            var searchingKeyword = txtSearch.Text;
            Console.WriteLine(searchingKeyword);
            if (searchingKeyword.Length !=null)
            {
                var listbyKeyword = productService.getProductByKeyword(searchingKeyword);
                if(listbyKeyword != null)
                Console.WriteLine(listbyKeyword.Count);
                Console.WriteLine(listbyKeyword);
                DgvDisplayProductByKeyword(listbyKeyword);
            }
            else
            {
                DgvDisplayProduct();
            }
        }

        private void lblProductID_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblSearch_Click(object sender, EventArgs e)
        {

        }
    }
}

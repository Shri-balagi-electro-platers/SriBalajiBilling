using SriBalajiBilling.Models;
using SriBalajiBilling.service;
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
            txtQty.Text = "1";
            LoadBillNo();
        }

        private void LoadBillNo()
        {
            var billno = billService.GenerateBillNumber();
            txtBillNo.Text = billno.ToString();
        }

        private void comboproductName_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected product name from the ComboBox
            string selectedProductName = comboproductName.SelectedItem.ToString();
            Product product = productService.GetProductByName(selectedProductName);
            txtprice.Text = product.price.ToString();
            try
            {
            txtAmount.Text = (int.Parse(txtprice.Text )* int.Parse(txtQty.Text)).ToString();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            txtsgst.Text = "18";
            txtcgst.Text = "18";
        }
        static int dgvRow = 0;
        private void btnAdd_Click(object sender, EventArgs e)
        {
            dgvbill.Rows.Add();
            dgvRow = dgvbill.Rows.Count - 1;
            // add the product to the dgv table
            dgvbill["productName", dgvRow].Value = comboproductName.Text;
            int price = Convert.ToInt32(txtprice.Text);
            dgvbill["price",dgvRow].Value = txtprice.Text;
            int qty = Convert.ToInt32(txtQty.Text);
            dgvbill["quantity", dgvRow].Value = txtQty.Text;
            dgvbill["sgst", dgvRow].Value = txtsgst.Text;
            dgvbill["cgst", dgvRow].Value = txtcgst.Text;
            double taxAmount = qty*price*(double.Parse(txtsgst.Text)/100)+ price*(double.Parse(txtcgst.Text) / 100);
            Console.WriteLine($"debug: tax Amount: {taxAmount} ");
            dgvbill["taxAmount", dgvRow].Value = taxAmount.ToString();
            double productAmount = taxAmount + int.Parse(txtAmount.Text);
            Console.WriteLine($"debug: productAmount: {productAmount}");
            dgvbill["productAmount", dgvRow].Value = productAmount.ToString();

            
            dgvRow++;
            dgvbill.Refresh();
            comboproductName.Focus();

            // to high light the currently added row in data grid view
            if(dgvbill.Rows.Count > 0 )
            {
                dgvbill.CurrentCell = dgvbill.Rows[dgvbill.Rows.Count - 1].Cells[1];
            }
            // to calculate the total amount
            CalculateTotalAmount();
            comboproductName.Text = "";
            txtprice.Text = "0";
            txtQty.Text = "1";
            txtAmount.Text= "0";
        }

        //private void dgvbill_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    int clickedIndexPos = e.RowIndex;
        //    DataGridViewRow  row = dgvbill.Rows[clickedIndexPos];
        //    comboproductName.Text = row.Cells[1].Value.ToString();
            
        //}

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtAmount.Text = (int.Parse(txtprice.Text) * int.Parse(txtQty.Text)).ToString();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dgvbill.SelectedRows) 
            {
                if (!row.IsNewRow) dgvbill.Rows.Remove(row);            
            }
            // to calculate the total amount after deleting the row
            CalculateTotalAmount();
        }

        // calculate the total amount
        public void CalculateTotalAmount()
        {
            double sum = 0;
            for(int i=0;i<dgvbill.Rows.Count;i++)
            {
                double eachProductAmount = Convert.ToDouble(dgvbill["productAmount", i].Value);
                try
                {
                    sum += eachProductAmount;
                }catch(Exception ex)
                {
                    Console.WriteLine($"debug: CalcualateTotalAmount exception msg:{ex.Message}");
                }
            }
            txtTotalAmount.Text = sum.ToString();
            CalculateNetPay();
        }

        // save the bill
        private bool SaveBill()
        {
            // Create a new Bill object and populate its properties
            Bill bill = new Bill
            {
                BillNo = txtBillNo.Text,
                CustomerName = txtCustomerName.Text,
                CustomerAddress = txtCustomerAddress.Text,
                GSTIN = txtgstinuin.Text,
                CustomerPan = txtCustomerPan.Text,
                CustomerState = txtCustomerState.Text,
                PlaceOfSupply = txtPlaceOfSupply.Text,
                Items = new List<BillItem>(),
                Discount = Convert.ToDouble( txtDiscount.Text),
                TotalAmount = Convert.ToDouble(txtTotalAmount.Text),
                NetPay = Convert.ToDouble(txtNetPay.Text)

                // Populate other properties as needed
            };

            // Iterate through DataGridView rows to populate bill items
            foreach (DataGridViewRow row in dgvbill.Rows)
            {
                BillItem item = new BillItem
                {
                    SerialNo = Convert.ToInt32(row.Cells["serialNo"].Value),
                    ProductName = row.Cells["productName"].Value.ToString(),
                    Price = Convert.ToDouble(row.Cells["price"].Value),
                    Quantity = Convert.ToInt32(row.Cells["quantity"].Value),
                    SGST = Convert.ToDouble(row.Cells["sgst"].Value),
                    CGST = Convert.ToDouble(row.Cells["cgst"].Value),
                    TaxAmount = Convert.ToDouble(row.Cells["taxAmount"].Value),
                    ProductAmount = Convert.ToDouble(row.Cells["productAmount"].Value)
                };
                // debug 
                //Console.WriteLine(item.ToString()); 
                bill.Items.Add(item);
            }

            // Call the service to save the bill
            bool isSaved = billService.savebill(bill);

            return isSaved;
            
        }


        public void CalculateNetPay()
        {
            txtNetPay.Text = txtTotalAmount.Text;
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            CalculateNetPay();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool isSaved = SaveBill();
                if (isSaved)
                {
                    MessageBox.Show("Bill saved successfully.");
                    // Optionally, clear the form or perform other actions after saving
                }
                else
                {
                    MessageBox.Show("Failed to save the bill.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show($"Failed to save {ex.Message}");
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Linq.Expressions;

namespace Inventory
{
    public partial class frmInventory : Form
    {
        private string _ProductName, _Category, _MfgDate, _ExpDate, _Description;
        private int _Quantity;
        private double _SellPrice;
        private BindingSource showProductList;

        public string Product_Name(string name)
        {
            if (!Regex.IsMatch(name, @"^[a-zA-Z]+$"))
            {
                throw new StringFormatException("Product name.");
            }
            else
            {
                return name;
            }
        }
        public int Quantity(string qty)
        {
            if (!Regex.IsMatch(qty, @"^[0-9]"))
            {
                throw new NumberFormatException("Quantity.");
            }
            else
            {
                return Convert.ToInt32(qty);
            }
        }
        public double SellingPrice(string price)
        {

            if (!Regex.IsMatch(price.ToString(), @"^(\d*\.)?\d+$"))
            {
                throw new CurrencyFormatException("Selling Price.");
            }
            else
            {
                return Convert.ToDouble(price);
            }
        }
        public frmInventory()
        {
            InitializeComponent();
            showProductList = new BindingSource();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ListOfProductCategory = new string[]
            {
                "Beverages",
                "Bread/Bakery",
                "Canned/Jarred Goods",
                "Dairy",
                "Frozen Goods",
                "Meat",
                "Personal Care",
                "Other"
            };
            foreach (string Category in ListOfProductCategory)
            {
                cbCategory.Items.Add(Category);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtProductName.Text = "";
            cbCategory.Text = "";
            dtPickerMfgDate.Text = "";
            dtPickerExpDate.Text = "";
            richTxtDescription.Text = "";
            txtQuantity.Text = "";
            txtSellPrice.Text = "";
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                _ProductName = Product_Name(txtProductName.Text);
                _Category = cbCategory.Text;
                _MfgDate = dtPickerMfgDate.Value.ToString("yyyy-MM-dd");
                _ExpDate = dtPickerExpDate.Value.ToString("yyyy-MM-dd");
                _Description = richTxtDescription.Text;
                _Quantity = Quantity(txtQuantity.Text);
                _SellPrice = SellingPrice(txtSellPrice.Text);
                showProductList.Add(new ProductClass(_ProductName, _Category, _MfgDate, _ExpDate, _SellPrice, _Quantity, _Description));
                gridViewProductList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                gridViewProductList.DataSource = showProductList;
            }
            catch (StringFormatException SFE)
            {
                MessageBox.Show($"Message: " + SFE.Message);
            }
            catch (NumberFormatException NFE)
            {
                MessageBox.Show($"Message: " + NFE.Message);
            }
            catch (CurrencyFormatException CFE)
            {
                MessageBox.Show($"Message: " + CFE.Message);
            }
            finally { gridViewProductList.Invalidate(); }
        }

        public class StringFormatException : Exception
        {
            public StringFormatException(string _ProductName) : base($"Invalid string format for {_ProductName}") { }
        }
        public class NumberFormatException : Exception
        {
            public NumberFormatException(string _Quantity) : base($"Invalid number format for {_Quantity}") { }
        }
        public class CurrencyFormatException : Exception
        {
            public CurrencyFormatException(string _SellPrice) : base($"Invalid currency format for {_SellPrice}") { }
        }
    }
}
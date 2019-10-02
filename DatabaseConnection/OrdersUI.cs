using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatabaseConnection
{
    public partial class OrdersUI : Form
    {
        public OrdersUI()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, EventArgs e)
        {

            bool isExecute = Add(customerNameTextBox.Text, itemNameTextBox.Text, Convert.ToInt32(quantityTextBox.Text),Convert.ToDouble(totalPriceTextBox.Text));
            if (isExecute)
            {
                MessageBox.Show("Saved!!");
            }
            else
            {
                MessageBox.Show("Not Saved!!");
            }
            ShowAll();
        }

        private bool Add(string cname, string iname, int quantity, double totalPrice)
        {
            bool isAdded = false;
            try
            {
                //connection
                string conn = @"Server=DESKTOP-QMNU0HM\RAIHANPLAYGROUND; DataBase=CoffeeShop; integrated Security=True";
                SqlConnection sqlConnection = new SqlConnection(conn);

                //command
                string insert = @"INSERT INTO Orders (CustomerName, ItemName, Quantity, TotalPrice) VALUES ('" + cname + "','" + iname + "', " + quantity + ","+totalPrice+")";
                SqlCommand sqlCommand = new SqlCommand(insert, sqlConnection);

                //open
                sqlConnection.Open();

                //execution
                int isExecute = sqlCommand.ExecuteNonQuery();
                if (isExecute > 0)
                {
                    isAdded = true;
                }

                //close
                sqlConnection.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            return isAdded;
        }

        private void showButton_Click(object sender, EventArgs e)
        {
            ShowAll();
        }

        private void ShowAll()
        {
            try
            {
                //connection
                string conn = @"Server=DESKTOP-QMNU0HM\RAIHANPLAYGROUND; DataBase=CoffeeShop; integrated Security=True";
                SqlConnection sqlConnection = new SqlConnection(conn);

                //command
                string show = @"SELECT * FROM Orders";
                SqlCommand sqlCommand = new SqlCommand(show, sqlConnection);

                //open
                sqlConnection.Open();

                //execution
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                {
                    showDataGridView.DataSource = dataTable;
                }
                else
                {
                    showDataGridView.DataSource = null;
                    MessageBox.Show("No Data Found");
                }

                //close
                sqlConnection.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(idTextBox.Text))
            {
                MessageBox.Show("id can not be empty");
                return;
            }
            bool DeleteDone = DeleteOrder(Convert.ToInt32(idTextBox.Text));
            if (DeleteDone)
            {
                MessageBox.Show("Order Deleted!!!!");
            }
            else
            {
                MessageBox.Show("Order Not Deleted!!!!");
            }
            ShowAll();
        }

        private bool DeleteOrder(int id)
        {
            bool isDeleted = false;
            try
            {
                //connection
                string conn = @"Server=DESKTOP-QMNU0HM\RAIHANPLAYGROUND; DataBase=CoffeeShop; integrated Security=True";
                SqlConnection sqlConnection = new SqlConnection(conn);

                //command
                string deleted = @"DELETE FROM Orders WHERE id = " + id + "";
                SqlCommand sqlCommand = new SqlCommand(deleted, sqlConnection);

                //open
                sqlConnection.Open();

                //execution
                int delete = sqlCommand.ExecuteNonQuery();
                if (delete > 0)
                {
                    isDeleted = true;
                }

                //close
                sqlConnection.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            return isDeleted;
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(idTextBox.Text))
            {
                MessageBox.Show("id can not be empty");
                return;
            }

            if (String.IsNullOrEmpty(customerNameTextBox.Text))
            {
                MessageBox.Show("Customer Name Cannot be empty");
                return;
            }

            if (String.IsNullOrEmpty(itemNameTextBox.Text))
            {
                MessageBox.Show("Item Name Cannot be empty");
                return;
            }

            if (String.IsNullOrEmpty(quantityTextBox.Text))
            {
                MessageBox.Show("Quantity Cannot be empty");
                return;
            }

            if (String.IsNullOrEmpty(totalPriceTextBox.Text))
            {
                MessageBox.Show("Total Price Cannot be empty");
                return;
            }

            if (UpdateOrder(Convert.ToInt32(idTextBox.Text), customerNameTextBox.Text, itemNameTextBox.Text, Convert.ToInt32(quantityTextBox.Text), Convert.ToDouble(totalPriceTextBox.Text)))
            {
                MessageBox.Show("Order Updated");
            }
            else
            {
                MessageBox.Show("Order Not Updated");
            }
            ShowAll();
        }

        private bool UpdateOrder(int id, string cname, string iname, int quantity, double totalPrice)
        {
            try
            {
                //connection
                string conn = @"Server=DESKTOP-QMNU0HM\RAIHANPLAYGROUND; DataBase=CoffeeShop; integrated Security=True";
                SqlConnection sqlConnection = new SqlConnection(conn);

                //command
                string update = @"UPDATE Orders SET CustomerName = '" + cname + "', ItemName = '" + iname + "', Quantity = " + quantity + ", TotalPrice= "+totalPrice+" WHERE Id = " + id + "";
                SqlCommand sqlCommand = new SqlCommand(update, sqlConnection);

                //open
                sqlConnection.Open();

                //execution
                int updated = sqlCommand.ExecuteNonQuery();
                if (updated > 0)
                {
                    return true;
                }


                //close
                sqlConnection.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            return false;
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            SearchCustomer(customerNameTextBox.Text);
        }

        private void SearchCustomer(string cname)
        {
            try
            {
                //connection
                string conn = @"Server=DESKTOP-QMNU0HM\RAIHANPLAYGROUND; DataBase=CoffeeShop; integrated Security=True";
                SqlConnection sqlConnection = new SqlConnection(conn);

                //command
                string search = @"SELECT * FROM Orders WHERE CustomerName = '" + cname + "'";
                SqlCommand sqlCommand = new SqlCommand(search, sqlConnection);

                //open
                sqlConnection.Open();

                //execution
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                {
                    showDataGridView.DataSource = dataTable;
                }
                else
                {
                    MessageBox.Show("No Data Matched!!!");
                }

                //close
                sqlConnection.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}

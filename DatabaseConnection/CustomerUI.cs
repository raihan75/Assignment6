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
    public partial class CustomerUI : Form
    {
        public CustomerUI()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (IsNameExists(nameTextBox.Text))
            {
                MessageBox.Show(nameTextBox.Text + " Already Exists!!!");
                return;
            }

            bool isExecute = Add(nameTextBox.Text, contactTextBox.Text, addressTextBox.Text);
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

        private bool Add(string name, string contact, string address)
        {
            bool isAdded = false;
            try
            {
                //connection
                string conn = @"Server=DESKTOP-QMNU0HM\RAIHANPLAYGROUND; DataBase=CoffeeShop; integrated Security=True";
                SqlConnection sqlConnection = new SqlConnection(conn);

                //command
                string insert = @"INSERT INTO Customers (Name, Contact, Address) VALUES ('" + name + "','" + contact + "', '"+address+"')";
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

        private bool IsNameExists(string name)
        {
            bool exists = false;

            try
            {
                //connection
                string conn = @"Server=DESKTOP-QMNU0HM\RAIHANPLAYGROUND; DataBase=CoffeeShop; integrated Security=True";
                SqlConnection sqlConnection = new SqlConnection(conn);

                //command
                string search = @"SELECT * FROM Customers WHERE Name = '" + name + "'";
                SqlCommand sqlCommand = new SqlCommand(search, sqlConnection);

                //open
                sqlConnection.Open();

                //execution
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                {
                    exists = true;
                }

                //close
                sqlConnection.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

            return exists;
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
                string show = @"SELECT * FROM Customers";
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
            bool DeleteDone = DeleteCustomer(Convert.ToInt32(idTextBox.Text));
            if (DeleteDone)
            {
                MessageBox.Show("Customer Deleted!!!!");
            }
            else
            {
                MessageBox.Show("Customer Not Deleted!!!!");
            }
            ShowAll();
        }

        private bool DeleteCustomer(int id)
        {
            bool isDeleted = false;
            try
            {
                //connection
                string conn = @"Server=DESKTOP-QMNU0HM\RAIHANPLAYGROUND; DataBase=CoffeeShop; integrated Security=True";
                SqlConnection sqlConnection = new SqlConnection(conn);

                //command
                string deleted= @"DELETE FROM Customers WHERE id = " + id + "";
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

            if (String.IsNullOrEmpty(nameTextBox.Text))
            {
                MessageBox.Show("Price Cannot be empty");
                return;
            }

            if (String.IsNullOrEmpty(contactTextBox.Text))
            {
                MessageBox.Show("Price Cannot be empty");
                return;
            }

            if (String.IsNullOrEmpty(addressTextBox.Text))
            {
                MessageBox.Show("Price Cannot be empty");
                return;
            }

            if (UpdateCustomer(Convert.ToInt32(idTextBox.Text), nameTextBox.Text, contactTextBox.Text, addressTextBox.Text))
            {
                MessageBox.Show("Item Updated");
            }
            else
            {
                MessageBox.Show("Item Not Updated");
            }
            ShowAll();
        }

        private bool UpdateCustomer(int id, string name, string contact, string address)
        {
            try
            {
                //connection
                string conn = @"Server=DESKTOP-QMNU0HM\RAIHANPLAYGROUND; DataBase=CoffeeShop; integrated Security=True";
                SqlConnection sqlConnection = new SqlConnection(conn);

                //command
                string update = @"UPDATE Customers SET Name = '" + name + "', Contact = '" + contact + "', Address = '"+address+"' WHERE Id = " + id + "";
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
            SearchCustomer(nameTextBox.Text);
        }

        private void SearchCustomer(string name)
        {
            try
            {
                //connection
                string conn = @"Server=DESKTOP-QMNU0HM\RAIHANPLAYGROUND; DataBase=CoffeeShop; integrated Security=True";
                SqlConnection sqlConnection = new SqlConnection(conn);

                //command
                string search = @"SELECT * FROM Customers WHERE Name = '" + name + "'";
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

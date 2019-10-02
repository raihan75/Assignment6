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
    public partial class ItemUi : Form
    {
        public ItemUi()
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
            if (String.IsNullOrEmpty(priceTextBox.Text))
            {
                MessageBox.Show("Price Can not Be Empty!!!");
                return;
            }

            bool isExecute = Add(nameTextBox.Text,Convert.ToDouble(priceTextBox.Text));
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

        private bool Add(string name, double price)
        {
            bool isAdded = false;
            try
            {
                //connection
                string conn = @"Server=DESKTOP-QMNU0HM\RAIHANPLAYGROUND; DataBase=CoffeeShop; integrated Security=True";
                SqlConnection sqlConnection = new SqlConnection(conn);

                //command
                string insert = @"INSERT INTO Items (Name, Price) VALUES ('" + name + "'," + price + ")";
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
                string search = @"SELECT * FROM Items WHERE Name = '"+name+"'";
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
                string show = @"SELECT * FROM Items";
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
            bool DeleteDone = DeleteItem(Convert.ToInt32(idTextBox.Text));
            if (DeleteDone)
            {
                MessageBox.Show("Item Deleted!!!!");
            }
            else
            {
                MessageBox.Show("Item Not Deleted!!!!");
            }
            ShowAll();
        }

        private bool DeleteItem(int id)
        {
            bool isDeleted = false;
            try
            {
                //connection
                string conn = @"Server=DESKTOP-QMNU0HM\RAIHANPLAYGROUND; DataBase=CoffeeShop; integrated Security=True";
                SqlConnection sqlConnection = new SqlConnection(conn);

                //command
                string show = @"DELETE FROM Items WHERE id = "+id+"";
                SqlCommand sqlCommand = new SqlCommand(show, sqlConnection);

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

            if (String.IsNullOrEmpty(priceTextBox.Text))
            {
                MessageBox.Show("Price Cannot be empty");
            }

            if(UpdateItem(Convert.ToInt32(idTextBox.Text), nameTextBox.Text, Convert.ToDouble(priceTextBox.Text)))
            {
                MessageBox.Show("Item Updated");
            }
            else
            {
                MessageBox.Show("Item Not Updated");
            }
            ShowAll();
        }

        private bool UpdateItem(int id, string name, double price)
        {
            try
            {
                //connection
                string conn = @"Server=DESKTOP-QMNU0HM\RAIHANPLAYGROUND; DataBase=CoffeeShop; integrated Security=True";
                SqlConnection sqlConnection = new SqlConnection(conn);

                //command
                string update = @"UPDATE Items SET Name = '"+name+"', Price = "+price+" WHERE Id = "+id+"";
                SqlCommand sqlCommand = new SqlCommand(update, sqlConnection);

                //open
                sqlConnection.Open();

                //execution
                int updated= sqlCommand.ExecuteNonQuery();
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
            SearchItem(nameTextBox.Text);
        }

        private void SearchItem(string name)
        {
            try
            {
                //connection
                string conn = @"Server=DESKTOP-QMNU0HM\RAIHANPLAYGROUND; DataBase=CoffeeShop; integrated Security=True";
                SqlConnection sqlConnection = new SqlConnection(conn);

                //command
                string search = @"SELECT * FROM Items WHERE Name = '"+name+"'";
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

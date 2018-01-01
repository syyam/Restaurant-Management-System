using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RestaurantManagementSystemFinal
{
    /// <summary>
    /// Interaction logic for Expense.xaml
    /// </summary>
    public partial class Expense : Window
    {
        public Expense()
        {
            InitializeComponent();
            fillData();
        }
        public void fillData()
        {
            string connetionString = null; //insertion code
            SqlConnection connection;
            SqlDataAdapter adapter = new SqlDataAdapter();

            connetionString = "Data Source=DESKTOP-J81TTMH;Initial Catalog=restaurant;User ID=sa;Password=safdar2311";
            connection = new SqlConnection(connetionString);
            try
            {
                String CmdString = "select 'Supply ' +supp_name as Expenses,supp_unit_price as Amount from supplies union select  'Salary ' + empname,empsalary from employees union select emisc as Misc ,eamount from expense where emisc != 'null'";
                SqlCommand cmd = new SqlCommand(CmdString, connection);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("stock");
                sda.Fill(dt);
                egrid.ItemsSource = dt.DefaultView;
                egrid.Items.Refresh();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong");
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string connetionString = null; //insertion code
            SqlConnection connection;
            SqlDataAdapter adapter = new SqlDataAdapter();

            connetionString = "Data Source=DESKTOP-J81TTMH;Initial Catalog=restaurant;User ID=sa;Password=safdar2311";
            connection = new SqlConnection(connetionString);
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "insert into expense (etype,eamount) values (@name,@quantity) ";
                command.Parameters.AddWithValue("@name", expensetype.Text);
                command.Parameters.AddWithValue("@quantity", expenseamount.Text);
                command.Connection = connection;
                int a = command.ExecuteNonQuery();
                if (a == 1)
                {
                    MessageBox.Show("Expense Record Updated Successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string connetionString = null; //insertion code
            SqlConnection connection;
            SqlDataAdapter adapter = new SqlDataAdapter();

            connetionString = "Data Source=DESKTOP-J81TTMH;Initial Catalog=restaurant;User ID=sa;Password=safdar2311";
            connection = new SqlConnection(connetionString);
            try
            {
                connection.Open();
               
                int sum = 0;
                SqlCommand cm = new SqlCommand();
                cm = new SqlCommand("select sum(Amount) from vw_expense", connection);
                sum = Convert.ToInt32(cm.ExecuteScalar());
                MessageBox.Show("Total Expense Incurred Rs."+ sum);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong" +ex);
            }
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();

        }

        private void Image_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
        }

        private void Image_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            MainWindow main = new MainWindow();
            main.Show();
        }
    }
}

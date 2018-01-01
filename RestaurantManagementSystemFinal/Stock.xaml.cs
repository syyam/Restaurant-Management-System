using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Stock.xaml
    /// </summary>
    public partial class Stock : Window
    {

        public Stock()
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
                String CmdString = "select distinct(product.name),stock.name,stock.quantity from product,stock where P_id in (select distinct(product_id) from product_stock) and stock_id in (select stock_id from product_stock)";
                SqlCommand cmd = new SqlCommand(CmdString, connection);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("stock");
                sda.Fill(dt);
                dgrid.ItemsSource = dt.DefaultView;
                dgrid.Items.Refresh();


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
                command.CommandText = "insert into Stock (name,quantity) values (@name,@quantity) ";
                command.Parameters.AddWithValue("@name", stockname.Text);
                command.Parameters.AddWithValue("@quantity", stockquantity.Text);
                command.Connection = connection;
                int a = command.ExecuteNonQuery();
                if (a == 1)
                {
                    SqlCommand cmds = new SqlCommand("insert into supplies (supp_name,supp_unit_price) values(@name,@price)", connection);
                    cmds.Parameters.AddWithValue("@name", stockname.Text);
                    cmds.Parameters.AddWithValue("@price", stockprice.Text);
                    cmds.ExecuteNonQuery();

                   

                    int x;
                    SqlCommand cmdq = new SqlCommand();
                    cmdq = new SqlCommand("select max(stock_id) from stock", connection);
                    x = Convert.ToInt32(cmdq.ExecuteScalar().ToString());

                    SqlCommand cmdd = new SqlCommand("insert into supplies_stock (stock_id) values (@x)", connection);
                   
                    cmdd.Parameters.AddWithValue("@x", x);
                    cmdd.ExecuteNonQuery();


                    MessageBox.Show("Stock Updated Successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong");
            }

        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}

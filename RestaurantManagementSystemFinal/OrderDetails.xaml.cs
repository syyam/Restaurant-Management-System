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
    /// Interaction logic for OrderDetails.xaml
    /// </summary>
    public partial class OrderDetails : Window
    {
        string d = null;
        string discount = null;
        public OrderDetails(string d,string discount)
        {
            this.d = d;
            this.discount = discount;
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
                String CmdString = "select * from product,Orders where P_id in (select distinct(p_id) from Orders_Product where o_id in (select o_id from Orders where exact_date = @date)) and Orders.discount_name = @discount";
                SqlCommand cmd = new SqlCommand(CmdString, connection);
                cmd.Parameters.AddWithValue("@date", d);
                cmd.Parameters.AddWithValue("@discount", discount);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("product");
                sda.Fill(dt);
                ordergrid.ItemsSource = dt.DefaultView;
                ordergrid.Items.Refresh();


            }
            catch (Exception ex)
            {
                
                MessageBox.Show("Something went wrong");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

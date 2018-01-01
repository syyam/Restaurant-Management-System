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
    /// Interaction logic for RecordDisplay.xaml
    /// </summary>
    public partial class RecordDisplay : Window
    {
        String dealSelected;
        int price;
        public RecordDisplay(String dealSelected,int price)
        {
            this.dealSelected = dealSelected;
            this.price = price;
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
                String CmdString = "select * from product where P_id in (select distinct(P_id) from product_deal where D_id = (select D_id from deal where name = @dealName)) and price > @price";
                SqlCommand cmd = new SqlCommand(CmdString, connection);
                cmd.Parameters.AddWithValue("@dealName", dealSelected);
                cmd.Parameters.AddWithValue("@price", price);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("product");
                sda.Fill(dt);
                recordgrid.ItemsSource = dt.DefaultView;
                recordgrid.Items.Refresh();


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

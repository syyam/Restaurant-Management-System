using System;
using System.Collections.Generic;
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
    /// Interaction logic for Bill.xaml
    /// </summary>
    public partial class Bill : Window
    {
        private List<PlaceOrders.Entry> myList;
        String discount;
        public Bill(List<PlaceOrders.Entry> myList,String discount)
        {
            InitializeComponent();
            this.myList = myList;
            this.discount = discount;
        }
        public class Entry
        {
            public string Name { get; internal set; }

            public int Price { get; internal set; }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string connetionString = null; //insertion code
            SqlConnection connection;
            SqlDataAdapter adapter = new SqlDataAdapter();
            connetionString = "Data Source=DESKTOP-J81TTMH;Initial Catalog=restaurant;User ID=sa;Password=safdar2311";
            connection = new SqlConnection(connetionString);

            int sum = 0;
            PlaceOrders.Entry[] arr = myList.ToArray();
            billdg.ItemsSource = myList;
            for(int i = 0; i < arr.Length; i++)
            {
                sum += arr[i].Price;
            }
            sumbox.Text = sum.ToString();
          
                connection.Open();
                double dos;
                SqlCommand cm = new SqlCommand();
                cm = new SqlCommand("select discount_amount from Discount where discount_type=@proname", connection);
                cm.Parameters.AddWithValue("@proname", discount);
                dos= Convert.ToInt32(cm.ExecuteScalar());

                double result = sum * (dos / 100);
                MessageBox.Show(result.ToString("N2"));
                afterdis.Text = result.ToString("N2");
        }
    }
    
}

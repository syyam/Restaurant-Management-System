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
    /// Interaction logic for Supplydisplay.xaml
    /// </summary>
    public partial class Supplydisplay : Window
    {
        String suplr = null;
        public Supplydisplay(String suplr)
        {
            this.suplr = suplr;
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

                    String CmdString = "select * from supplies where supp_id in (select supplies_id from supplier_supplies where supplier_id in (select supplier_id from supplier where supplier_name=@supplyName))";
                    SqlCommand cmd = new SqlCommand(CmdString, connection);
                if (suplr == null)
                    cmd.Parameters.AddWithValue("@supplyName", DBNull.Value);
                cmd.Parameters.AddWithValue("@supplyName", suplr);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable("supplies");
                    sda.Fill(dt);
                    supplygrid.ItemsSource = dt.DefaultView;
                    supplygrid.Items.Refresh();


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something went wrong" + suplr);
                }
            }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

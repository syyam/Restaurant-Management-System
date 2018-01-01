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
    /// Interaction logic for ExpenseRecords.xaml
    /// </summary>
    public partial class ExpenseRecords : Window
    {
        String salary = null;
        String supply = null;
           
        public ExpenseRecords(String salary,String supply)
        {
            this.salary = salary;
            this.supply = supply;
           
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
                
                String CmdString = "select * from vw_expense where Expenses in(select Expenses from vw_expense where Expenses like '%'+ @salary +'%'or Expenses in (select Expenses from vw_expense where Expenses like '%'+ @supply +'%'))";
                SqlCommand cmd = new SqlCommand(CmdString, connection);
                if(supply == null)
                cmd.Parameters.AddWithValue("@supply", DBNull.Value);
                else
                  cmd.Parameters.AddWithValue("@supply", supply);
                if (salary == null)
                    cmd.Parameters.AddWithValue("@salary", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@salary", salary);
               
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("product");
                sda.Fill(dt);
                ddgrid.ItemsSource = dt.DefaultView;
                ddgrid.Items.Refresh();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong" +ex);
            }
        }
    }
}

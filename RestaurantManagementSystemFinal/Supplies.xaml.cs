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
using System.Windows.Forms;

namespace RestaurantManagementSystemFinal
{
    /// <summary>
    /// Interaction logic for Supplies.xaml
    /// </summary>
    public partial class Supplies : Window
    {
        String valueOfItem;
        public Supplies()
        {
            InitializeComponent();
            BindListView();
            BindComboBoxSupplies(suppliescombo);
            BindComboBoxSupplier(suppliercombo);
        }
        public void BindComboBoxSupplies(System.Windows.Controls.ComboBox comboBoxName)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-J81TTMH;Initial Catalog=restaurant;User ID=sa;Password=safdar2311");
            SqlDataAdapter da = new SqlDataAdapter("select supp_name from supplies", con);
            DataSet ds = new DataSet();
            comboBoxName.Items.Clear();
            da.Fill(ds, "Supplies");
            comboBoxName.ItemsSource = ds.Tables[0].DefaultView;
            comboBoxName.DisplayMemberPath = ds.Tables[0].Columns["supp_name"].ToString();
        }
        public void BindComboBoxSupplier(System.Windows.Controls.ComboBox comboBoxName)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-J81TTMH;Initial Catalog=restaurant;User ID=sa;Password=safdar2311");
            SqlDataAdapter da = new SqlDataAdapter("select supplier_name from supplier", con);
            DataSet ds = new DataSet();
            comboBoxName.Items.Clear();
            da.Fill(ds, "Supplier");
            comboBoxName.ItemsSource = ds.Tables[0].DefaultView;
            comboBoxName.DisplayMemberPath = ds.Tables[0].Columns["supplier_name"].ToString();
        }
        public class Entry
        {
            public string Name { get; internal set; }

            public int Price { get; internal set; }
        }
        public void BindListView()
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-J81TTMH;Initial Catalog=restaurant;User ID=sa;Password=safdar2311");
            SqlCommand comm = new SqlCommand("select supp_name from supplies", con);
            DataTable dt = new DataTable();
            SqlDataAdapter data = new SqlDataAdapter(comm);
            data.Fill(dt);
            supplist.DataContext = dt.DefaultView;
            supplist.ItemsSource = dt.DefaultView;
            con.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String text = sup_name.Text;
            if (text.Trim() == "") return;
            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsNumber(text[i]) || char.IsPunctuation(text[i]))
                {
                    System.Windows.MessageBox.Show("Please enter a valid input");
                    sup_name.Text = "";
                    return;
                }

            }
            String text1 = sup_price.Text;
            if (text.Trim() == "") return;
            for (int i = 0; i < text1.Length; i++)
            {
                if (!char.IsNumber(text1[i]))
                {
                    System.Windows.MessageBox.Show("Please enter a valid input");
                    sup_name.Text = "";
                    return;
                }

            }
            string connetionString = null; //insertion code
            SqlConnection connection;
            SqlDataAdapter adapter = new SqlDataAdapter();

            connetionString = "Data Source=DESKTOP-J81TTMH;Initial Catalog=restaurant;User ID=sa;Password=safdar2311";
            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "insert into supplies (supp_name,supp_unit_price) values (@suppName,@suppPrice)";
                command.Parameters.AddWithValue("@suppName", sup_name.Text);
                command.Parameters.AddWithValue("@suppPrice", sup_price.Text);
                command.Connection = connection;
                int a = command.ExecuteNonQuery();
                if (a == 1)
                {
                    SqlCommand cmdes = new SqlCommand("insert into stock (name,quantity) values (@suppName,@suppPrice)", connection);
                    cmdes.Parameters.AddWithValue("@suppName", sup_name.Text);
                    cmdes.Parameters.AddWithValue("@suppPrice", sup_price.Text);
                    cmdes.ExecuteNonQuery();

                    int z;
                    SqlCommand cmded = new SqlCommand();
                    cmded = new SqlCommand("select max(supply_id) from supplies_stock", connection);
                    z = Convert.ToInt32(cmded.ExecuteScalar().ToString());

                    int x;
                    SqlCommand cmdq = new SqlCommand();
                    cmdq = new SqlCommand("select max(stock_id) from stock", connection);
                    x = Convert.ToInt32(cmdq.ExecuteScalar().ToString());

                    SqlCommand cmdd = new SqlCommand("insert into supplies_stock (supply_id,stock_id) values (@z,@x)", connection);
                    cmdd.Parameters.AddWithValue("@z", z);
                    cmdd.Parameters.AddWithValue("@x", x);
                    cmdd.ExecuteNonQuery();


                    System.Windows.MessageBox.Show("Supply Added Successfully");
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }

        }

        private void supplist_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRowView drv = (DataRowView)supplist.SelectedItem;
            valueOfItem = drv["supp_name"].ToString();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string connetionString = null; 
            SqlConnection connection;
            SqlDataAdapter adapter = new SqlDataAdapter();
            connetionString = "Data Source=DESKTOP-J81TTMH;Initial Catalog=restaurant;User ID=sa;Password=safdar2311";
            connection = new SqlConnection(connetionString);
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "insert into supplier (supplier_name,supplier_cont,supplier_city,company) values (@name,@contact,@city,@company)";
                command.Parameters.AddWithValue("@name", suppliername.Text);
                command.Parameters.AddWithValue("@contact", suppliercontact.Text);
                command.Parameters.AddWithValue("@city", suppliercity.Text);
                command.Parameters.AddWithValue("@company",suppliercompany.Text );
                command.Connection = connection;
                int a = command.ExecuteNonQuery();
                if(a == 1)
                {
                    System.Windows.MessageBox.Show("Supplier Added");
                }
            }
            catch(Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string connetionString = null;
            SqlConnection connection;
            SqlDataAdapter adapter = new SqlDataAdapter();
            connetionString = "Data Source=DESKTOP-J81TTMH;Initial Catalog=restaurant;User ID=sa;Password=safdar2311";
            connection = new SqlConnection(connetionString);
            try
            {
                connection.Open();
                string supplierid;
                SqlCommand cmded = new SqlCommand();
                cmded = new SqlCommand("select supplier_id from supplier where supplier_name = @name", connection);
                cmded.Parameters.AddWithValue("@name", suppliercombo.Text);
                supplierid = cmded.ExecuteScalar().ToString();

                string suppliesid;
                SqlCommand cmdse = new SqlCommand();
                cmdse = new SqlCommand("select supp_id from supplies where supp_name = @valueOfItem", connection);
                cmdse.Parameters.AddWithValue("@valueOfItem", suppliescombo.Text);
                suppliesid = cmdse.ExecuteScalar().ToString();

                SqlCommand cmds = new SqlCommand("insert into supplier_supplies (supplier_id,supplies_id,quantity) values (@supplierid,@suppliesid,@quantity)", connection);
                cmds.Parameters.AddWithValue("@supplierid", supplierid);
                cmds.Parameters.AddWithValue("@suppliesid", suppliesid);
                cmds.Parameters.AddWithValue("@quantity", quantity.Text);
                cmds.Connection = connection;
                cmds.ExecuteNonQuery();
                System.Windows.MessageBox.Show("Current Supplier Updated!");
            }
            catch(Exception ex)
            {
                System.Windows.MessageBox.Show("Something going wrong " + ex);
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

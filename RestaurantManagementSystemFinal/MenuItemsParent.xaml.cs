using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for MenuItemsParent.xaml
    /// </summary>
    public partial class MenuItemsParent : Window
    {
        public MenuItemsParent()
        {
            InitializeComponent();
            BindComboBox(combobox3);
            binddatagrid();
        }
        public void binddatagrid()
        {
            string connetionString = null; //insertion code
            SqlConnection connection;
            SqlDataAdapter adapter = new SqlDataAdapter();

            connetionString = "Data Source=DESKTOP-J81TTMH;Initial Catalog=restaurant;User ID=sa;Password=safdar2311";
            connection = new SqlConnection(connetionString);
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "select * from product";
            command.Connection = connection;
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable("product"); 
            da.Fill(dt);
            g1.ItemsSource = dt.DefaultView;
            g1.Items.Refresh();

        }
        public void BindComboBox(ComboBox comboBoxName)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-J81TTMH;Initial Catalog=restaurant;User ID=sa;Password=safdar2311");
            SqlDataAdapter da = new SqlDataAdapter("select name from deal", con);
            DataSet ds = new DataSet();
            comboBoxName.Items.Clear();
            da.Fill(ds, "deal");
            comboBoxName.ItemsSource = ds.Tables[0].DefaultView;
            comboBoxName.DisplayMemberPath = ds.Tables[0].Columns["name"].ToString();
        }

            private void Button_Click(object sender, RoutedEventArgs e)
        {
            string parentElement = null;
            if (burger.IsChecked == true)
            {
                parentElement = "Burger";
            }else if (sandwich.IsChecked == true)
            {
                parentElement = "Sandwich";
            }
            else if(pizza.IsChecked == true)
            {
                parentElement = "Pizzas";
            }
            String text = prod_name.Text;
            if (text.Trim() == "") return;
            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsNumber(text[i]) || char.IsPunctuation(text[i]))
                {
                    MessageBox.Show("Please enter a valid input");
                    prod_name.Text = "";
                    return;
                }

            }
            String text1 = prod_price.Text;
            if (text.Trim() == "") return;
            for (int i = 0; i < text1.Length; i++)
            {
                if (!char.IsNumber(text1[i]) || char.IsPunctuation(text1[i]))
                {
                    MessageBox.Show("Please enter a valid input");
                    prod_price.Text = "";
                    return;
                }

            }
            String text2 = prod_quantity.Text;
            if (text2.Trim() == "") return;
            for (int i = 0; i < text2.Length; i++)
            {
                if (!char.IsNumber(text2[i]) || char.IsPunctuation(text2[i]))
                {
                    MessageBox.Show("Please enter a valid input");
                    prod_quantity.Text = "";
                    return;
                }

            }

            if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text1) && !string.IsNullOrEmpty(text2))
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
                        command.CommandText = "insert into Product (name,price,quantity,deal,parentProduct) values (@name,@price,@quantity,@deal,@parent)";
                        command.Parameters.AddWithValue("@name", prod_name.Text);
                        command.Parameters.AddWithValue("@price", prod_price.Text);
                        command.Parameters.AddWithValue("@quantity", prod_quantity.Text);
                   if(combobox3.SelectedItem == null)
                    {
                        command.Parameters.AddWithValue("@deal",  (object)DBNull.Value);
                    }
                   else
                        command.Parameters.AddWithValue("@deal", combobox3.Text);
                   

                         command.Parameters.AddWithValue("@parent", parentElement);
                        command.Connection = connection;
                        int a = command.ExecuteNonQuery();
                        if (a == 1)
                        {
                            string productID;
                            SqlCommand cmded = new SqlCommand();
                            cmded = new SqlCommand("select P_id from product where @name = product.name", connection);
                            cmded.Parameters.AddWithValue("@name", prod_name.Text);
                            productID = cmded.ExecuteScalar().ToString();

                            string dealID = null;
                            SqlCommand cmd = new SqlCommand();
                            cmd = new SqlCommand("select D_id from deal where @deal = deal.name", connection);
                        if (combobox3.SelectedItem == null)
                        {
                            cmd.Parameters.AddWithValue("@deal", (object)DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@deal", combobox3.Text);
                            dealID = cmd.ExecuteScalar().ToString();
                        }
                          

                        SqlCommand cmds = new SqlCommand("insert into product_deal (P_id,D_id) values (@productID,@dealID)", connection);
                        cmds.Parameters.AddWithValue("@productID", productID);
                        cmds.Parameters.AddWithValue("@dealID", dealID);
                        cmds.ExecuteNonQuery();
                        if(parentElement == "Burger")
                        {
                            SqlCommand update = new SqlCommand("insert into product_stock(product_id,stock_id) select distinct (P_id), stock_id from product, stock where  parentProduct = 'Burger' and stock.name IN('buns', 'patties')", connection);
                            update.ExecuteNonQuery();
                        }
                        if (parentElement == "Sandwich")
                        {
                            SqlCommand update = new SqlCommand("insert into product_stock(product_id,stock_id) select distinct (P_id), stock_id from product, stock where  parentProduct = 'Sandwich' and stock.name IN('bread', 'cheese')", connection);
                            update.ExecuteNonQuery();
                        }
                        if (parentElement == "Pizza")
                        {
                            SqlCommand update = new SqlCommand("insert into product_stock(product_id,stock_id) select distinct (P_id), stock_id from product, stock where  parentProduct = 'Pizzas' and stock.name IN('cheese', 'mushrooms', 'olives', 'dough')", connection);
                            update.ExecuteNonQuery();
                        }

                        MessageBox.Show("Product Added Successfully");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                
               

            }

        }
       
        
       
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           
            String text1 = deal_name.Text;
            if (text1.Trim() == "") return;
            for (int i = 0; i < text1.Length; i++)
            {
                if (char.IsNumber(text1[i]))
                {
                    MessageBox.Show("Please enter a valid input");
                    deal_name.Text = "";
                    return;
                }

            }
            String text2 = deal_price.Text;
            if (text2.Trim() == "") return;
            for (int i = 0; i < text2.Length; i++)
            {
                if (!char.IsNumber(text2[i]) || char.IsPunctuation(text2[i]))
                {
                    MessageBox.Show("Please enter a valid input");
                    deal_price.Text = "";
                    return;
                }

            }

            if (!string.IsNullOrEmpty(text1) && !string.IsNullOrEmpty(text2) )
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
                    command.CommandText = "insert into deal (name,price) values (@name,@price)";
                    command.Parameters.AddWithValue("@name", deal_name.Text);
                    command.Parameters.AddWithValue("@price", deal_price.Text);
                    command.Connection = connection;
                    int a = command.ExecuteNonQuery();
                    if (a == 1)
                    {
                        MessageBox.Show("Deal Added Successfully");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }
            
         

        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
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
                command.CommandText = "delete from product_deal where p_id = (select max(p_id) from product_deal)";
                command.Connection = connection;
                int a = command.ExecuteNonQuery();
                if (a == 1)
                {
                    SqlCommand cmds = new SqlCommand();
                   cmds.CommandText = "delete from product_stock where product_id=(select max(product_id) from product_stock)";
                   cmds.Connection = connection;
                   cmds.ExecuteNonQuery();

                    SqlCommand cmd = new SqlCommand();
                   cmd.CommandText = "delete from product where P_id=(select max(P_id) from product)";
                    cmd.Connection = connection;
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Last item has been deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
    }
}

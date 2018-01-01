using System;
using System.Windows;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Linq;

namespace RestaurantManagementSystemFinal
{
    /// <summary>
    /// Interaction logic for PlaceOrders.xaml
    /// </summary>
    /// 
    
    public partial class PlaceOrders : Window
    {
        ObservableCollection<Entry> entries = new ObservableCollection<Entry>();
        int i = 0;
        int sum = 0;
        public PlaceOrders()
        {
            InitializeComponent();
            BindComboBoxDiscount(discount);
            BindListView();
            ds = new DataSet();
        }
        DataSet ds;


        public void BindComboBoxDiscount(System.Windows.Controls.ComboBox comboBoxName)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-J81TTMH;Initial Catalog=restaurant;User ID=sa;Password=safdar2311");
            SqlDataAdapter da = new SqlDataAdapter("select discount_type from Discount", con);
            DataSet ds = new DataSet();
            comboBoxName.Items.Clear();
            da.Fill(ds, "Discount");
            comboBoxName.ItemsSource = ds.Tables[0].DefaultView;
            comboBoxName.DisplayMemberPath = ds.Tables[0].Columns["discount_type"].ToString();
        }
        public void BindListView()
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-J81TTMH;Initial Catalog=restaurant;User ID=sa;Password=safdar2311");
            SqlCommand comm = new SqlCommand("select P_id,name,price,quantity,deal from product", con);
            DataTable dt = new DataTable();
            SqlDataAdapter data = new SqlDataAdapter(comm);
            data.Fill(dt);
            l1.DataContext = dt.DefaultView;
            l1.ItemsSource = dt.DefaultView;
            con.Close();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (homedelivery.IsChecked == true)
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
                    command.CommandText = "insert into Orders (exact_date,order_type,discount_name) values (GETDATE(),'dine in',@discount) ";
                    
                    command.Parameters.AddWithValue("@discount", discount.Text);
                    command.Connection = connection;
                    int a = command.ExecuteNonQuery();
                    if (a == 1)
                    {
                        for (int i = 0; i < entries.Count; i++)
                        {
                            string x;
                            SqlCommand cmd = new SqlCommand();
                            cmd = new SqlCommand("select P_id from product where name = @name", connection);
                            cmd.Parameters.AddWithValue("@name", entries[i].Name);
                            x = cmd.ExecuteScalar().ToString();
                            

                           
                            string y;
                            SqlCommand cmdd = new SqlCommand();
                            cmdd = new SqlCommand("select name from product where name = @name", connection);
                            cmdd.Parameters.AddWithValue("@name", entries[i].Name);
                            y = cmdd.ExecuteScalar().ToString();
                           

                           
                            string z;
                            SqlCommand cmded = new SqlCommand();
                            cmded = new SqlCommand("select exact_date from Orders where o_id in(select max(o_id) from Orders )", connection);
                            z = cmded.ExecuteScalar().ToString();


                            
                            SqlCommand cm = new SqlCommand();
                            cm = new SqlCommand("select price from product where name=@proname", connection);
                            cm.Parameters.AddWithValue("@proname", entries[i].Name);
                           sum +=  Convert.ToInt32(cm.ExecuteScalar());
                            

                            SqlCommand cmds = new SqlCommand("insert into Orders_Product (p_id,o_id,product_name,exact_date) select @x,max(o_id),@y,@z from Orders", connection);
                            cmds.Parameters.AddWithValue("@x", x);
                            cmds.Parameters.AddWithValue("@y", y);
                            cmds.Parameters.AddWithValue("@z", z);
                            cmds.ExecuteNonQuery();

                            SqlCommand cmden = new SqlCommand("update product set product.quantity = product.quantity - 1where product.P_id in (select product.P_id from product inner join Orders_Product ON product.P_id = Orders_Product.p_id where Orders_Product.o_id = (select max(o_id) from Orders_Product)) AND product.quantity > 0", connection);
                            cmden.ExecuteNonQuery();

                            SqlCommand cmdes = new SqlCommand("update stock set stock.quantity = stock.quantity - 1 where stock.stock_id in( select distinct(stock_id) from stock where stock_id in( select stock_id from product_stock where product_id = (select max(p_id) from product))) AND stock.quantity > 0", connection);
                            cmdes.ExecuteNonQuery();

                        }
                        
                        System.Windows.MessageBox.Show("Product Added Successfully");
                        List<Entry> myList = entries.ToList();
                        Bill bill = new Bill(myList,discount.Text);
                        bill.Show();
                        System.Windows.MessageBox.Show("Total Amount : " + "Rs" +sum.ToString());
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.ToString());
                }



            }
            else if (takeaway.IsChecked == true)
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
                    command.CommandText = "insert into Orders (exact_date,order_type,discount_name) values (GETDATE(),'takeaway',@discount) ";
                    
                    command.Parameters.AddWithValue("@discount", discount.Text);
                    command.Connection = connection;
                    int a = command.ExecuteNonQuery();
                    for (int i = 0; i < entries.Count; i++)
                    {
                        string x;
                        SqlCommand cmd = new SqlCommand();
                        cmd = new SqlCommand("select P_id from product where name = @name", connection);
                        cmd.Parameters.AddWithValue("@name", entries[i].Name);
                        x = cmd.ExecuteScalar().ToString();
                     

                       
                        string y;
                        SqlCommand cmdd = new SqlCommand();
                        cmdd = new SqlCommand("select name from product where name = @name", connection);
                        cmdd.Parameters.AddWithValue("@name", entries[i].Name);
                        y = cmdd.ExecuteScalar().ToString();

                        


                        string z;
                        SqlCommand cmded = new SqlCommand();
                        cmded = new SqlCommand("select exact_date from Orders where o_id in(select max(o_id) from Orders )", connection);
                        z = cmded.ExecuteScalar().ToString();

                        SqlCommand cm = new SqlCommand();
                        cm = new SqlCommand("select price from product where name=@proname", connection);
                        cm.Parameters.AddWithValue("@proname", entries[i].Name);
                        sum += Convert.ToInt32(cm.ExecuteScalar());

                        SqlCommand cmds = new SqlCommand("insert into Orders_Product (p_id,o_id,product_name,exact_date) select @x,max(o_id),@y,@z from Orders", connection);
                        cmds.Parameters.AddWithValue("@x", x);
                        cmds.Parameters.AddWithValue("@y", y);
                        cmds.Parameters.AddWithValue("@z", z);
                        cmds.ExecuteNonQuery();

                        SqlCommand cmden = new SqlCommand("update product set product.quantity = product.quantity - 1where product.P_id in (select product.P_id from product inner join Orders_Product ON product.P_id = Orders_Product.p_id where Orders_Product.o_id = (select max(o_id) from Orders_Product)) AND product.quantity > 0", connection);
                        cmden.ExecuteNonQuery();


                        SqlCommand cmdes = new SqlCommand("update stock set stock.quantity = stock.quantity - 1 where stock.stock_id in( select distinct(stock_id) from stock where stock_id in( select stock_id from product_stock where product_id = (select max(p_id) from product))) AND stock.quantity > 0", connection);
                        cmdes.ExecuteNonQuery();

                    }
                    List<Entry> myList = entries.ToList();
                    Bill bill = new Bill(myList,discount.Text);
                    bill.Show();
                    System.Windows.MessageBox.Show("Total Amount : " + "Rs" + sum.ToString());
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.ToString());
                }


            }
        }

     
        public class User
        {
            public int PID { get; set; }

            public string Name { get; set; }

            public int Price { get; set; }
            public string FirstName { get; internal set; }
            public string LastName { get; internal set; }
        }
        public class Entry
        {
            public string Name { get; internal set; }

            public int Price { get; internal set; }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ObservableCollection<User> users = new ObservableCollection<User>();
            users.Add(new User { FirstName = "firstname-1", LastName = "lastname-1" });
            users.Add(new User { FirstName = "firstname-2", LastName = "lastname-2" });
            users.Add(new User { FirstName = "firstname-3", LastName = "lastname-3" });
            users.Add(new User { FirstName = "firstname-4", LastName = "lastname-4" });
            DataGrid.ItemsSource = users;
        }

        private void l1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRowView drv = (DataRowView)l1.SelectedItem;
            String valueOfItem = drv["Name"].ToString();

            DataRowView dre = (DataRowView)l1.SelectedItem;
            int price = Convert.ToInt32(drv["Price"]);

            entries.Add(new Entry { Name = valueOfItem, Price = price });
            DataGrid.ItemsSource = entries;

            //System.Windows.MessageBox.Show(entries[0].Name);
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Image_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            entries.Clear();

        }

        private void Image_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow menu = new MainWindow();
            menu.Show();
        }
    }
}
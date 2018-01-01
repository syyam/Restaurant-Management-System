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
    /// Interaction logic for Report.xaml
    /// </summary>
    public partial class Report : Window
    {
        public Report()
        {
            InitializeComponent();
            BindComboBox(recordcombo1);
            BindDiscountBox(discombo);
            BindSupplier(supplier);
            BindSupplies(supplies);
        }
        public void BindSupplier(ComboBox supplier)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-J81TTMH;Initial Catalog=restaurant;User ID=sa;Password=safdar2311");
            SqlDataAdapter da = new SqlDataAdapter("select supplier_name from supplier", con);
            DataSet ds = new DataSet();
            supplier.Items.Clear();
            da.Fill(ds, "supplier");
            supplier.ItemsSource = ds.Tables[0].DefaultView;
            supplier.DisplayMemberPath = ds.Tables[0].Columns["supplier_name"].ToString();
        }
        public void BindSupplies(ComboBox supplies)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-J81TTMH;Initial Catalog=restaurant;User ID=sa;Password=safdar2311");
            SqlDataAdapter da = new SqlDataAdapter("select supp_name from supplies", con);
            DataSet ds = new DataSet();
            supplies.Items.Clear();
            da.Fill(ds, "supplies");
            supplies.ItemsSource = ds.Tables[0].DefaultView;
            supplies.DisplayMemberPath = ds.Tables[0].Columns["supp_name"].ToString();
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
        public void BindDiscountBox(ComboBox comboBoxName)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-J81TTMH;Initial Catalog=restaurant;User ID=sa;Password=safdar2311");
            SqlDataAdapter da = new SqlDataAdapter("select discount_type from Discount", con);
            DataSet ds = new DataSet();
            comboBoxName.Items.Clear();
            da.Fill(ds, "Discount");
            comboBoxName.ItemsSource = ds.Tables[0].DefaultView;
            comboBoxName.DisplayMemberPath = ds.Tables[0].Columns["discount_type"].ToString();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String dealSelected;
            int price;
            if (recordcombo1.SelectedIndex > -1)
            {
               dealSelected = recordcombo1.Text;
            }
            else
            {
                dealSelected = null;
            }

            if (string.IsNullOrWhiteSpace(priceofproduct.Text))
            {
                price = 0;
            }
            else
            {
                price = Convert.ToInt32(priceofproduct.Text);
            }
            RecordDisplay disp = new RecordDisplay(dealSelected,price);
            disp.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           
            string d = null;
            string discount = null;
            if(date.SelectedDate == null)
            {
                d = null;
            }
            else
            {
                d =  Convert.ToDateTime(date.Text).ToString("yyyy/MM/dd");
            }
            if (discombo.SelectedIndex > -1)
            {
                discount = discombo.Text;
            }
            else
            {
                discount = "";
            }
            OrderDetails ord = new OrderDetails(d,discount);
            ord.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            String supply, salary;
            if(supplycheck.IsChecked == true)
            {
                supply= "Supply";
            }
            else
            {
                supply = null;
            }
            if(salarycheck.IsChecked == true)
            {
                salary = "salary";
            }
            else
            {
                salary = null;
            }
          
            ExpenseRecords exp = new ExpenseRecords(supply,salary);
            exp.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            String suplr;
           
           
            if (supplier.SelectedIndex > -1)
            {
                suplr = supplier.Text;
            }
            else
            {
               suplr = null;
            }
          


            Supplydisplay disp = new Supplydisplay(suplr);
            disp.Show();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

           
            String supply;

        
            if (supplier.SelectedIndex > -1)
            {
                supply = supplies.Text;
            }
            else
            {
                supply = null;
            }


            Supplierdisplay disp = new Supplierdisplay(supply);
            disp.Show();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow win = new MainWindow();
            win.Show();
            this.Close();
        }
    }
}

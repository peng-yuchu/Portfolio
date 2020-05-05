using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Npgsql;

namespace Milestone1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public class Business
        {
            public string bid { get; set; }

            public string name { get; set; }

            public string state { get; set; }

            public string city { get; set; }

            public int postal_code { get; set; }

            public string address { get; set; }

            public double longitude { get; set; }

            public double latitude { get; set; }

            public double stars { get; set; }

            public int checkin_count { get; set; }

            public int tip_count { get; set; }

            public double distance { get; set; }
        }

        public class friendinfo
        {
            public string friend_name { get; set; }

            public string friend_total_like { get; set; }

            public string friend_avgstars { get; set; }

            public string friend_yelpingsince { get; set; }
        }

        public class friendtip
        {
            public string friendtips_name { get; set; }

            public string friendtips_business { get; set; }

            public string friendtips_city { get; set; }

            public string friendtips_text { get; set; }

            public string friendtips_date { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();
            addState();
            addColumn2Grid();
            addSortList();
        }

        private string buildConnectionString()
        {
            return "Host=localhost; Username=postgres;Port = 8212; Database=test; password = 123";
        }

        private void addState()
        {
            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT distinct state FROM business ORDER BY state";
                    try
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                            stateList.Items.Add(reader.GetString(0));
                    }
                    catch (NpgsqlException ex)
                    {
                        Console.WriteLine(ex.Message.ToString());
                        System.Windows.MessageBox.Show("SQL Error - " + ex.Message.ToString());

                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        private void addColumn2Grid()
        {
            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Header = "BusinessName";
            col1.Binding = new Binding("name");
            col1.Width = 170;
            businessGrid.Columns.Add(col1);

            DataGridTextColumn col2 = new DataGridTextColumn();
            col2.Header = "Address";
            col2.Binding = new Binding("address");
            col2.Width = 170;
            businessGrid.Columns.Add(col2);

            DataGridTextColumn col3 = new DataGridTextColumn();
            col3.Header = "City";
            col3.Binding = new Binding("city");
            col3.Width = 80;
            businessGrid.Columns.Add(col3);

            DataGridTextColumn col4 = new DataGridTextColumn();
            col4.Header = "State";
            col4.Binding = new Binding("state");
            col4.Width = 40;
            businessGrid.Columns.Add(col4);

            DataGridTextColumn col5 = new DataGridTextColumn();
            col5.Header = "Distance";
            col5.Binding = new Binding("distance");
            col5.Width = 80;
            businessGrid.Columns.Add(col5);

            DataGridTextColumn col6 = new DataGridTextColumn();
            col6.Header = "Stars";
            col6.Binding = new Binding("stars");
            col6.Width = 45;
            businessGrid.Columns.Add(col6);

            DataGridTextColumn col7 = new DataGridTextColumn();
            col7.Header = "Tip Counts";
            col7.Binding = new Binding("tip_count");
            col7.Width = 75;
            businessGrid.Columns.Add(col7);

            DataGridTextColumn col8 = new DataGridTextColumn();
            col8.Header = "Check In Counts";
            col8.Binding = new Binding("checkin_count");
            col8.Width = 110;
            businessGrid.Columns.Add(col8);

            DataGridTextColumn col9 = new DataGridTextColumn();
            col9.Header = "Name";
            col9.Binding = new Binding("friend_name");
            col9.Width = 120;
            friendslist.Columns.Add(col9);

            DataGridTextColumn col10 = new DataGridTextColumn();
            col10.Header = "TotalLikes";
            col10.Binding = new Binding("friend_total_like");
            col10.Width = 79;
            friendslist.Columns.Add(col10);

            DataGridTextColumn col11 = new DataGridTextColumn();
            col11.Header = "Avg Stars";
            col11.Binding = new Binding("friend_avgstars");
            col11.Width = 79;
            friendslist.Columns.Add(col11);

            DataGridTextColumn col12 = new DataGridTextColumn();
            col12.Header = "Yelping Since";
            col12.Binding = new Binding("friend_yelpingsince");
            col12.Width = 200;
            friendslist.Columns.Add(col12);

            DataGridTextColumn col13 = new DataGridTextColumn();
            col13.Header = "User Name";
            col13.Binding = new Binding("friendtips_name");
            col13.Width = 100;
            friendtipslist.Columns.Add(col13);

            DataGridTextColumn col14 = new DataGridTextColumn();
            col14.Header = "Business";
            col14.Binding = new Binding("friendtips_business");
            col14.Width = 130;
            friendtipslist.Columns.Add(col14);

            DataGridTextColumn col15 = new DataGridTextColumn();
            col15.Header = "City";
            col15.Binding = new Binding("friendtips_city");
            col15.Width = 80;
            friendtipslist.Columns.Add(col15);

            DataGridTextColumn col16 = new DataGridTextColumn();
            col16.Header = "Text";
            col16.Binding = new Binding("friendtips_text");
            col16.Width = 300;
            friendtipslist.Columns.Add(col16);

            DataGridTextColumn col17 = new DataGridTextColumn();
            col17.Header = "Date";
            col17.Binding = new Binding("friendtips_date");
            col17.Width = 140;
            friendtipslist.Columns.Add(col17);
        }

        private void executeQuery(string sqlstr, Action<NpgsqlDataReader> myf)
        {
            using(var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using(var cmd= new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = sqlstr;
                    try
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                            myf(reader);
                    }
                    catch (NpgsqlException ex)
                    {
                        Console.WriteLine(ex.Message.ToString());
                        System.Windows.MessageBox.Show("SQL Error - " + ex.Message.ToString());
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        private void addCityRow(NpgsqlDataReader R)
        {
            cityList.Items.Add(R.GetString(0));
        }

        private void addPostalCodeRow(NpgsqlDataReader R)
        {
            postalcodeList.Items.Add(R.GetInt64(0).ToString());
        }

        private void addCategoryRow(NpgsqlDataReader R)
        {
            categoryList.Items.Add(R.GetString(0));
        }

        private void addSortList()
        {
            sortlist.Items.Add("Business name");
            sortlist.Items.Add("Highest rating (stars)");
            sortlist.Items.Add("Most number of tips");
            sortlist.Items.Add("Most check-ins");
            sortlist.Items.Add("Nearest");
        }

        private void stateList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cityList.Items.Clear();
            if (stateList.SelectedIndex > -1)
            {
                string sqlStr = "SELECT distinct city FROM business WHERE state ='" + stateList.SelectedItem.ToString() + "' ORDER BY city";
                executeQuery(sqlStr, addCityRow);
                        
            }
        }

        private void cityListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            postalcodeList.Items.Clear();
            if (cityList.SelectedIndex > -1)
            {
                string sqlStr = "SELECT distinct postal_code FROM business WHERE city ='"+ cityList.SelectedItem.ToString() + "'ORDER BY postal_code";
                executeQuery(sqlStr, addPostalCodeRow);
            }
        }

        private void postalcodeListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            categoryList.Items.Clear();
            selected_categoryList.Items.Clear();
            if (postalcodeList.SelectedIndex > -1)
            {
                string sqlStr = "SELECT distinct category_name FROM categories INNER JOIN business ON business.business_id = categories.business_id WHERE business.postal_code =" + postalcodeList.SelectedItem.ToString() +"ORDER BY category_name";
                executeQuery(sqlStr, addCategoryRow);
            }
        }

        private void addbusinessGrid(NpgsqlDataReader R)
        { 
            businessGrid.Items.Add(new Business() { name = R.GetString(1), address = R.GetString(2), state = R.GetString(3), postal_code = R.GetInt32(4), city = R.GetString(5), stars = R.GetDouble(8), checkin_count = R.GetInt32(10), tip_count = R.GetInt32(11), bid = R.GetString(0)});

        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            businessGrid.Items.Clear();
            StringBuilder categoryitems = new StringBuilder();
            if (selected_categoryList.Items.Count>=1)
            {
                categoryitems.Append("FROM business WHERE business.business_id IN (SELECT business_id FROM categories WHERE category_name = '" + selected_categoryList.Items[0].ToString() + "') ");
                for (int i = 1; i < selected_categoryList.Items.Count; i++)
                {
                    categoryitems.Append("AND business.business_id IN (SELECT business_id FROM categories WHERE category_name = '" + selected_categoryList.Items[i].ToString() + "') ");
                }
            }
            else
            {
                categoryitems.Append("FROM categories WHERE category_name = '" + selected_categoryList.SelectedItem.ToString() + "' ");
            }
            string sqlStr = "SELECT * FROM business, (SELECT DISTINCT business_id as bid " + categoryitems + ") a " +
                        "WHERE postal_code = '" + postalcodeList.SelectedItem.ToString() + "' and a.bid = business.business_id ORDER BY business.name;";
            executeQuery(sqlStr, addbusinessGrid);
        }

        private void businessGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (businessGrid.SelectedIndex > -1)
            {
                Business B = businessGrid.Items[businessGrid.SelectedIndex] as Business;
                if ((B.bid != null) && (B.bid.ToString().CompareTo("") != 0))
                {
                    business_name.Content=B.name.ToString();
                    businessaddress.Content = B.address.ToString();
                    string sqlStr = "SELECT is_open FROM business WHERE business_id = '"+B.bid.ToString()+"';";
                    executeQuery(sqlStr, addOpenTime);
                }
            }
        }

        private void addOpenTime(NpgsqlDataReader R)
        {
            if (R.GetInt32(0) == 1)
            {
                businesshour.Content = "Open";
            }
            else
            {
                businesshour.Content = "Closed";
            }
        }

        private void addcategory_Click(object sender, RoutedEventArgs e)
        {
            selected_categoryList.Items.Add(categoryList.SelectedItem.ToString());
        }

        private void removecategory_Click(object sender, RoutedEventArgs e)
        {
            selected_categoryList.Items.Remove(selected_categoryList.SelectedItem.ToString());
        }

        private void showtipsbutton_Click(object sender, RoutedEventArgs e)
        {
            if (businessGrid.SelectedIndex > -1)
            {
                BusinessDetails businessWindow = new BusinessDetails(((Business)businessGrid.SelectedItem).bid, usersearchlist.SelectedItem.ToString());
                businessWindow.Show();
            }
        }

        private void addusernamelist(NpgsqlDataReader R)
        {
            usersearchlist.Items.Add(R.GetString(0));
        }

        private void sortlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            businessGrid.Items.Clear();
            StringBuilder categoryitems = new StringBuilder();
            if (selected_categoryList.Items.Count >= 1)
            {
                categoryitems.Append("FROM business WHERE business.business_id IN (SELECT business_id FROM categories WHERE category_name = '" + selected_categoryList.Items[0].ToString() + "') ");
                for (int i = 1; i < selected_categoryList.Items.Count; i++)
                {
                    categoryitems.Append("AND business.business_id IN (SELECT business_id FROM categories WHERE category_name = '" + selected_categoryList.Items[i].ToString() + "') ");
                }
            }
            else
            {
                categoryitems.Append("FROM categories WHERE category_name = '" + selected_categoryList.SelectedItem.ToString() + "' ");
            }
            string sqlStr = "SELECT * FROM business, (SELECT DISTINCT business_id as bid " + categoryitems + ") a " +
                        "WHERE postal_code = '" + postalcodeList.SelectedItem.ToString() + "' and a.bid = business.business_id ";

            if (sortlist.SelectedItem.ToString()=="Business name")
            {
                sqlStr = sqlStr + "ORDER BY business.name;";
            }
            else if (sortlist.SelectedItem.ToString() == "Highest rating (stars)")
            {
                sqlStr = sqlStr + "ORDER BY business.stars DESC;";
            }
            else if (sortlist.SelectedItem.ToString() == "Most number of tips")
            {
                sqlStr = sqlStr + "ORDER BY business.tip_count DESC;";
            }
            else if (sortlist.SelectedItem.ToString() == "Most check-ins")
            {
                sqlStr = sqlStr + "ORDER BY business.checkin_count DESC;";
            }
            else if (sortlist.SelectedItem.ToString() == "Nearest")
            {
                sqlStr = sqlStr + "ORDER BY business.stars;";
            }
            executeQuery(sqlStr, addbusinessGrid);
        }

        private void username_TextChanged(object sender, TextChangedEventArgs e)
        {
            usersearchlist.Items.Clear();
            friendslist.Items.Clear();
            friendtipslist.Items.Clear();

            string sqlStr = "SELECT user_id FROM users WHERE name = '" + username.Text.ToString() + "';";

            executeQuery(sqlStr, addusernamelist);
        }

        private void usersearchlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            friendslist.Items.Clear();
            friendtipslist.Items.Clear();
            if (usersearchlist.SelectedIndex > -1)
            {
                string sqlStr = "SELECT name, average_stars, fans_count, yelping_since, funny_count, cool_count, useful_count, tip_count, tip_like_count, longitude, latitude FROM users WHERE user_id = '" + usersearchlist.SelectedItem.ToString() + "';";
                executeQuery(sqlStr, addUserInfo);

                sqlStr = "SELECT distinct name, tip_like_count,  average_stars, yelping_since FROM users, (SELECT DISTINCT friend_id FROM friendswith WHERE user_id = '" + usersearchlist.SelectedItem.ToString() + "') a WHERE a.friend_id = users.user_id;";
                executeQuery(sqlStr, addFriendInfo);

                sqlStr = "SELECT users.name, business.name, business.city, tips.tip_text, tips.date_created FROM users, business, tips, (SELECT DISTINCT user_id FROM users, (SELECT DISTINCT friend_id FROM friendswith WHERE user_id = '" + usersearchlist.SelectedItem.ToString() + "') a WHERE a.friend_id = users.user_id) b WHERE b.user_id = users.user_id and business.business_id = tips.business_id and tips.user_id = b.user_id ORDER BY tips.date_created DESC;";
                executeQuery(sqlStr, addFriendTip);
            }
            
        }

        private void addUserInfo(NpgsqlDataReader R)
        {
            selected_username.Text = R.GetString(0);
            selected_userstars.Text = R.GetDouble(1).ToString();
            selected_userfans.Text = R.GetInt32(2).ToString();
            selected_useryelpingsince.Text = R.GetDate(3).ToString();
            selected_uservote_funny.Text = R.GetInt32(4).ToString();
            selected_uservote_cool.Text = R.GetInt32(5).ToString();
            selected_uservote_useful.Text = R.GetInt32(6).ToString();
            selected_usertip.Text = R.GetInt32(7).ToString();
            selected_usertiplike.Text = R.GetInt32(8).ToString();
            if (!String.IsNullOrEmpty(R.GetValue(9).ToString()))
            {
                selected_usertiplong.Text = R.GetDouble(9).ToString();
            }
            else
            {
                selected_usertiplong.Text = "0";
            }
            if (!String.IsNullOrEmpty(R.GetValue(10).ToString()))
            {
                selected_usertiplat.Text = R.GetDouble(10).ToString();
            }
            else
            {
                selected_usertiplat.Text = "0";
            }
        }

        private void addFriendInfo(NpgsqlDataReader R)
        {
            friendslist.Items.Add(new friendinfo() { friend_name = R.GetString(0), friend_total_like = R.GetInt32(1).ToString(), friend_avgstars = R.GetDouble(2).ToString(), friend_yelpingsince = R.GetDate(3).ToString() });
        }

        private void addFriendTip(NpgsqlDataReader R)
        {
            friendtipslist.Items.Add(new friendtip() { friendtips_name = R.GetString(0), friendtips_business = R.GetString(1), friendtips_city = R.GetString(2), friendtips_text = R.GetString(3), friendtips_date = R.GetTimeStamp(4).ToString() });
        }

        private void lat_long_edit_Click(object sender, RoutedEventArgs e)
        {
            string sqlStr = "UPDATE users SET longitude = " + selected_usertiplong.Text.ToString() + ", latitude = " + selected_usertiplat.Text.ToString() + " WHERE user_id = '" + usersearchlist.SelectedItem.ToString() + "';";
            executeQuery(sqlStr, adduserdistance);
        }

        private void adduserdistance(NpgsqlDataReader R)
        {

        }

        private void checkinbutton_Click(object sender, RoutedEventArgs e)
        {
            if (businessGrid.SelectedIndex > -1)
            {
                Checkin checkinWindow = new Checkin(((Business)businessGrid.SelectedItem).bid);
                checkinWindow.Show();
            }
        }
    }
}

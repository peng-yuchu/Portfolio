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
using System.Windows.Shapes;
using Npgsql;

namespace Milestone1
{
    /// <summary>
    /// Interaction logic for BusinessDetails.xaml
    /// </summary>
    public partial class BusinessDetails : Window
    {
        public BusinessDetails(string selectedBusiness, string selectedUser)
        {

            InitializeComponent();
            addColumn2Grid();
            LoadTips(selectedBusiness, selectedUser);
            bid.Content = selectedBusiness;
            user_id.Content = selectedUser;
        }

        private void addColumn2Grid()
        {
            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Header = "Date";
            col1.Binding = new Binding ("date");
            col1.Width = 100;
            tipGrid.Columns.Add(col1);

            DataGridTextColumn col2 = new DataGridTextColumn();
            col2.Header = "User Name";
            col2.Binding = new Binding("username");
            col2.Width = 100;
            tipGrid.Columns.Add(col2);

            DataGridTextColumn col3 = new DataGridTextColumn();
            col3.Header = "Likes";
            col3.Binding = new Binding("like");
            col3.Width = 100;
            tipGrid.Columns.Add(col3);

            DataGridTextColumn col4 = new DataGridTextColumn();
            col4.Header = "Text";
            col4.Binding = new Binding("text");
            col4.Width = 474;
            tipGrid.Columns.Add(col4);

            DataGridTextColumn col5 = new DataGridTextColumn();
            col5.Header = "User Name";
            col5.Binding = new Binding("friendname");
            col5.Width = 100;
            friendreviewlist.Columns.Add(col5);

            DataGridTextColumn col6 = new DataGridTextColumn();
            col6.Header = "Date";
            col6.Binding = new Binding("frienddate");
            col6.Width = 150;
            friendreviewlist.Columns.Add(col6);

            DataGridTextColumn col7 = new DataGridTextColumn();
            col7.Header = "Text";
            col7.Binding = new Binding("friendtext");
            col7.Width = 520;
            friendreviewlist.Columns.Add(col7);
        }


        public class friendTip
        {
            public string friendname { get; set; }

            public string frienddate { get; set; }

            public string friendtext { get; set; }
        }

        public class Tip
        {
            public string username { get; set; }
            public string text { get; set; }
            public int like { get; set; }
            public string date { get; set; }
        }

        private string buildConnectionString()
        {
            return "Host=localhost; Username=postgres;Port = 8212; Database=test; password = 123";
        }

        private void executeQuery(string sqlstr, Action<NpgsqlDataReader> myf)
        {
            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
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

        void addtiptogrid(NpgsqlDataReader R)
        {
            tipGrid.Items.Add(new Tip() { username = R.GetString(1), text = R.GetString(3), date = R.GetTimeStamp(0).ToString(), like = R.GetInt32(2) });
        }

        void addfriendtip(NpgsqlDataReader R)
        {
            friendreviewlist.Items.Add(new friendTip() { friendname = R.GetString(0), frienddate = R.GetTimeStamp(1).ToString(), friendtext = R.GetString(2) });
        }

        private void LoadTips(string selectedBusiness, string selectedUser)
        {
            tipGrid.Items.Clear();
            friendreviewlist.Items.Clear();
            string sqlStr = "SELECT tips.date_created, users.name, tips.like_count, tips.tip_text FROM business, tips, users WHERE business.business_id = tips.business_id AND users.user_id = tips.user_id AND tips.business_id = '" + selectedBusiness + "' ORDER BY tips.date_created DESC;";
            executeQuery(sqlStr, addtiptogrid);

            sqlStr = "SELECT DISTINCT users.name, tips.date_created, tips.tip_text FROM users, tips, (SELECT DISTINCT friend_id, business_id FROM friendswith, (SELECT DISTINCT user_id, business_id FROM tips WHERE business_id = '" + selectedBusiness + "')a WHERE friendswith.user_id = '" + selectedUser + "' AND a.user_id = friendswith.friend_id) b WHERE b.friend_id = users.user_id AND b.friend_id = tips.user_id AND tips.business_id = '"+ selectedBusiness+"';";
            executeQuery(sqlStr, addfriendtip);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(commentbox.Text.ToString()))
            {
                string sqlStr = "INSERT INTO tips(user_id, business_id, date_created, like_count, tip_text) VALUES ('" + user_id.Content.ToString() + "', '" + bid.Content.ToString() + "', CURRENT_TIMESTAMP, 0, '" + commentbox.Text.ToString() + "');";
                executeQuery(sqlStr, addTips);
                commentbox.Text = "";
            }
            LoadTips(bid.Content.ToString(), user_id.Content.ToString());
        }

        private void addTips(NpgsqlDataReader R)
        {

        }

        private void addlike(NpgsqlDataReader R)
        {

        }

        private void likebutton_Click(object sender, RoutedEventArgs e)
        {
            if (tipGrid.SelectedIndex>-1)
            {
                string sqlStr = "UPDATE tips SET like_count = like_count +1 WHERE tip_text = '" + ((Tip)tipGrid.SelectedItem).text.ToString() + "';";
                executeQuery(sqlStr, addlike);
            }
            LoadTips(bid.Content.ToString(), user_id.Content.ToString());
        }
    }

    
}

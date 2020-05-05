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
    /// Interaction logic for Checkin.xaml
    /// </summary>
    public partial class Checkin : Window
    {
        public Checkin(string selectedBusiness)
        {
            InitializeComponent();
            ColumnChart(selectedBusiness);
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

        private void ColumnChart(string selectedBusiness)
        {
            string strSql = "SELECT name FROM business WHERE business_id = '" + selectedBusiness + "';";
            executeQuery(strSql, ChangeHeader);
            bus_id.Content = selectedBusiness;
            List<KeyValuePair<string, int>> checkinchartData = new List<KeyValuePair<string, int>>();
            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT COUNT(*) FROM checksin WHERE business_id = '" + selectedBusiness + "' AND DATE_PART('month', checkin_time) = 01;";
                    try
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                            checkinchartData.Add(new KeyValuePair<string, int>("Januuary", Convert.ToInt32(reader.GetString(0))));
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
            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT COUNT(*) FROM checksin WHERE business_id = '" + selectedBusiness + "' AND DATE_PART('month', checkin_time) = 02;";
                    try
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                            checkinchartData.Add(new KeyValuePair<string, int>("Feburary", Convert.ToInt32(reader.GetString(0))));
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
            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT COUNT(*) FROM checksin WHERE business_id = '" + selectedBusiness + "' AND DATE_PART('month', checkin_time) = 03;";
                    try
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                            checkinchartData.Add(new KeyValuePair<string, int>("March", Convert.ToInt32(reader.GetString(0))));
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
            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT COUNT(*) FROM checksin WHERE business_id = '" + selectedBusiness + "' AND DATE_PART('month', checkin_time) = 04;";
                    try
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                            checkinchartData.Add(new KeyValuePair<string, int>("April", Convert.ToInt32(reader.GetString(0))));
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
            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT COUNT(*) FROM checksin WHERE business_id = '" + selectedBusiness + "' AND DATE_PART('month', checkin_time) = 05;";
                    try
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                            checkinchartData.Add(new KeyValuePair<string, int>("May", Convert.ToInt32(reader.GetString(0))));
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
            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT COUNT(*) FROM checksin WHERE business_id = '" + selectedBusiness + "' AND DATE_PART('month', checkin_time) = 06;";
                    try
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                            checkinchartData.Add(new KeyValuePair<string, int>("June", Convert.ToInt32(reader.GetString(0))));
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
            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT COUNT(*) FROM checksin WHERE business_id = '" + selectedBusiness + "' AND DATE_PART('month', checkin_time) = 07;";
                    try
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                            checkinchartData.Add(new KeyValuePair<string, int>("July", Convert.ToInt32(reader.GetString(0))));
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
            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT COUNT(*) FROM checksin WHERE business_id = '" + selectedBusiness + "' AND DATE_PART('month', checkin_time) = 08;";
                    try
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                            checkinchartData.Add(new KeyValuePair<string, int>("August", Convert.ToInt32(reader.GetString(0))));
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
            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT COUNT(*) FROM checksin WHERE business_id = '" + selectedBusiness + "' AND DATE_PART('month', checkin_time) = 09;";
                    try
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                            checkinchartData.Add(new KeyValuePair<string, int>("September", Convert.ToInt32(reader.GetString(0))));
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
            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT COUNT(*) FROM checksin WHERE business_id = '" + selectedBusiness + "' AND DATE_PART('month', checkin_time) = 10;";
                    try
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                            checkinchartData.Add(new KeyValuePair<string, int>("October", Convert.ToInt32(reader.GetString(0))));
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
            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT COUNT(*) FROM checksin WHERE business_id = '" + selectedBusiness + "' AND DATE_PART('month', checkin_time) = 11;";
                    try
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                            checkinchartData.Add(new KeyValuePair<string, int>("November", Convert.ToInt32(reader.GetString(0))));
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
            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT COUNT(*) FROM checksin WHERE business_id = '" + selectedBusiness + "' AND DATE_PART('month', checkin_time) = 12;";
                    try
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                            checkinchartData.Add(new KeyValuePair<string, int>("December", Convert.ToInt32(reader.GetString(0))));
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
            checkinchart.DataContext = checkinchartData;
        }


        private void ChangeHeader(NpgsqlDataReader R)
        {
            bid.Content = R.GetString(0);
        }
        private void checkinbutton_Click(object sender, RoutedEventArgs e)
        {
            string sqlStr = "INSERT INTO checksin(checkin_time, business_id) VALUES (CURRENT_TIMESTAMP, (SELECT business_id FROM business WHERE business.business_id = '"+ bus_id.Content.ToString()+"'));";
            executeQuery(sqlStr, addcheckin);
            ColumnChart(bus_id.Content.ToString());
        }

        private void addcheckin(NpgsqlDataReader R)
        {

        }
    }
}

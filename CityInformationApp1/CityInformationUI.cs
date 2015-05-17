using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CityInformationApp1
{
    public partial class CityInformationUI : Form
    {
        public CityInformationUI()
        {
            InitializeComponent();
        }

        City city = new City();

        private string connectionString = ConfigurationManager.ConnectionStrings["CityInfoAppConString"].ConnectionString;
        private void saveButton_Click(object sender, EventArgs e)
        {
            city.cityname = citynameTextBox.Text;
            city.aboutcity = cityaboutTextBox.Text;
            city.countryname = countryTextBox.Text;

            if (cityaboutTextBox.Text == "" || citynameTextBox.Text == "" || countryTextBox.Text=="")
            {
                MessageBox.Show("Fill out all the Boxes");
            
            }

           

            else if (IsCityNameExists(city.cityname)) 
            {
                MessageBox.Show("Please enter another name for the city this name already exists");
            }

            else if (city.cityname.Length<=4)
            {
                MessageBox.Show("City Name must be greater than 4 charcters.");
            }


            else 
            {
                cityInformationlLstView.Items.Clear();

                SqlConnection connection = new SqlConnection(connectionString);

                string query = "INSERT INTO city VALUES('"+city.cityname+"','"+city.aboutcity+"','"+city.countryname+"')";


                SqlCommand command = new SqlCommand(query,connection);


                connection.Open();

                int rowsAffected=command.ExecuteNonQuery();

                connection.Close();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Information Added Successfully");
                    GetTextBoxesClear();
                    GetDataInListView();
                }
                else 
                {
                    MessageBox.Show("Operation Failed");
                }
            }


        }

        private void GetDataInListView()
        {
            List<City> cities = new List<City>();

            SqlConnection connection = new SqlConnection(connectionString);

            string query = "SELECT * FROM city";

            SqlCommand command = new SqlCommand(query,connection);

            connection.Open();
            
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read()) 
            {
                City city1 = new City();

                city1.cityid = reader["c_id"].ToString();
                city1.cityname = reader["c_name"].ToString();
                city1.aboutcity=reader["c_about"].ToString();
                city1.countryname = reader["c_country"].ToString();

                cities.Add(city1);
            }


            foreach (var city in cities)
            {
                ListViewItem item = new ListViewItem();

                item.Text = city.cityid;
                item.SubItems.Add(city.cityname);
                item.SubItems.Add(city.aboutcity);
                item.SubItems.Add(city.countryname);

                cityInformationlLstView.Items.Add(item);
            }


        }

        private void GetTextBoxesClear()
        {
            cityaboutTextBox.Text = "";
            citynameTextBox.Text = "";
            countryTextBox.Text = "";
        }

        private bool IsCityNameExists(string p)
        {
            bool iscitynameexists = false;


            SqlConnection connection = new SqlConnection(connectionString);

            string query="SELECT c_name FROM city WHERE c_name='"+p+"'";
            SqlCommand command = new SqlCommand(query,connection);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {
                iscitynameexists = true;
            }
            reader.Close();
            connection.Close();

            return iscitynameexists;
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            List<City> cities = new List<City>();

            string search = searchTextBox.Text;

            if (cityRadioButton.Checked == true)
            {

                int id = 1;

                cityInformationlLstView.Items.Clear();

                SqlConnection connection = new SqlConnection(connectionString);

                string query = "SELECT * FROM city WHERE c_name LIKE'"+search+"%'";

                SqlCommand command = new SqlCommand(query,connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    City city1 = new City();


                    city1.cityid = reader["c_id"].ToString();
                    city1.cityname = reader["c_name"].ToString();
                    city1.aboutcity = reader["c_about"].ToString();
                    city1.countryname = reader["c_country"].ToString();

                    cities.Add(city1);
                }


                foreach (var city in cities)
                {
                    ListViewItem item = new ListViewItem();

                    item.Text = id.ToString();
                    item.SubItems.Add(city.cityname);
                    item.SubItems.Add(city.aboutcity);
                    item.SubItems.Add(city.countryname);

                    cityInformationlLstView.Items.Add(item);
                    id++;
                }

                

            }

            else if(countryRadioButton.Checked==true)
            {

                int id = 1;
                cityInformationlLstView.Items.Clear();

                SqlConnection connection = new SqlConnection(connectionString);

                string query = "SELECT * FROM city WHERE c_country LIKE'" + search + "%'";

                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    City city1=new City();

                    city1.cityid = reader["c_id"].ToString();
                    city1.cityname = reader["c_name"].ToString();
                    city1.aboutcity = reader["c_about"].ToString();
                    city1.countryname = reader["c_country"].ToString();

                    cities.Add(city1);
                }


                foreach (var city in cities)
                {
                    ListViewItem item = new ListViewItem();

                    item.Text = id.ToString();
                    item.SubItems.Add(city.cityname);
                    item.SubItems.Add(city.aboutcity);
                    item.SubItems.Add(city.countryname);

                    cityInformationlLstView.Items.Add(item);
                    id++;
                }



            }

        
        }

        private void CityInformationUI_Load(object sender, EventArgs e)
        {
            GetDataInListView();
        }
    }
}

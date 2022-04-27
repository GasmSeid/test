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

namespace GONKI
{
    /// <summary>
    /// Логика взаимодействия для Spisok_blag_org.xaml
    /// </summary>
    public partial class Spisok_blag_org : Page
    {
        private string _connection = @"Data Source=localhost;Initial Catalog=Karting;Integrated Security=True";
        public Spisok_blag_org()
        {
            InitializeComponent();
            string command = "Select * from Charity";
            SqlDataAdapter adapter = new SqlDataAdapter(command, _connection);
            DataSet charity = new DataSet();
            adapter.Fill(charity);
            listViewCharity.ItemsSource = charity.Tables[0].DefaultView;
        }
    }
}

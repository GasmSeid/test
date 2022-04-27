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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GONKI
{
    /// <summary>
    /// Логика взаимодействия для Reg_sponsora.xaml
    /// </summary>
    /// 
    public partial class Reg_sponsora : Page
    {
        public string SponsorName { get; set; }

        private int _totalSum = 0;
        private string _connection = @"Data Source=localhost;Initial Catalog=Karting;Integrated Security=True";
        public Reg_sponsora()
        {
            InitializeComponent();
            string command = "Select *" +
                   " From Racer join[User] on Racer.Email = [User].Email join Country on Racer.ID_Country = Country.ID_Country";
            SqlDataAdapter adapter = new SqlDataAdapter(command, _connection);
            DataSet racers = new DataSet();
            adapter.Fill(racers);

            cbRacers.ItemsSource = racers.Tables[0].DefaultView;

            textBlockSum.Text = _totalSum.ToString();
            tbSum.Text = _totalSum.ToString();
            Language = XmlLanguage.GetLanguage("ru-RU");
        }

        private void btnPlusSum_Click(object sender, RoutedEventArgs e)
        {
            _totalSum += 10;
            textBlockSum.Text = _totalSum.ToString();
            tbSum.Text = _totalSum.ToString();
        }

        private void btnMinusSum_Click(object sender, RoutedEventArgs e)
        {
            _totalSum -= 10;
            if (_totalSum < 0)
            {
                _totalSum = 0;
            }
            textBlockSum.Text = _totalSum.ToString();
            tbSum.Text = _totalSum.ToString();
        }

        private void btnPay_Click(object sender, RoutedEventArgs e)
        {
            if (SponsorIsValid())
            {
                SqlConnection sqlConnection = new SqlConnection(_connection);
                sqlConnection.Open();
                string getRegistrationIDcommand = $"select * from Registration where Racer_ID = {(int)cbRacers.SelectedValue}";
                SqlCommand sqlCommand = new SqlCommand(getRegistrationIDcommand, sqlConnection);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                reader.Read();
                int id = (int)reader["ID_Registration"];
                reader.Close();

                string command = $"Insert into Sponsorship(SponsorName, Amount, Registration_ID) values ('{tbName.Text}', '{Convert.ToInt32(tbSum.Text)}', '{id}')";

                sqlCommand = new SqlCommand(command, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();


            
                Thanks page = new Thanks();
                page.SetSum(tbSum.Text);
                DataRowView dataRowView = (DataRowView)cbRacers.SelectedItem;
                string firstName = dataRowView.Row.Field<string>("First_Name");
                string lastName = dataRowView.Row.Field<string>("Last_Name");
                string country = dataRowView.Row.Field<string>("Country_Name");
                page.SetInfo($"{lastName} {firstName}", country);
                Page.Content = new Thanks();
            }
        }

        private bool SponsorIsValid()
        {
            if (!ValidateSponsorName())
            {
                MessageBox.Show("Поле имя не должно быть пустым. Поле должно содержать буквы латинского алфавита");
                return false;
            }
            if (!ValidateRacer())
            {
                MessageBox.Show("Гонщик - обязательное поле");
                return false;
            }
            if (!ValidateCardOwner())
            {
                MessageBox.Show("Поле владельца карты не должно быть пустым. Поле должно содержать заглавные буквы латинского алфавита. ");
                return false;
            }
            if (!ValidateCardNumber())
            {
                MessageBox.Show("Номер карты не может быть пустым. Номер карты должен соответствовать маске ввода XXXX XXXX XXXX XXXX");
                return false;
            }
            if (!ValidateCVC())
            {
                MessageBox.Show("CVC не может быть пустым. Номер карты должен соответствовать маске ввода XXX");
                return false;
            }
            if (!ValidateDate())
            {
                MessageBox.Show("Срок действия не может быть пустым. Срок действия не может быть меньше текущей даты");
                return false;
            }


            return true;
        }

        private bool ValidateSponsorName()
        {
            string pattern = @"^[a-zA-Z]+$";
            if (tbName.Text == null)
                return false;
            if (!Regex.IsMatch(tbName.Text, pattern))
            {
                return false;
            }

            return true;
        }

        private bool ValidateCardOwner()
        {
            string pattern = @"^[A-Z]+\s[A-Z]+$";
            if (tbCardOwner.Text == null)
                return false;
            if (!Regex.IsMatch(tbCardOwner.Text, pattern))
            {
                return false;
            }

            return true;
        }

        private bool ValidateCardNumber()
        {
            string pattern = @"^[0-9]{4}\s[0-9]{4}\s[0-9]{4}\s[0-9]{4}$";
            if (tbCardNumber.Text == null)
                return false;
            if (!Regex.IsMatch(tbCardNumber.Text, pattern))
            {
                return false;
            }
            return true;
        }

        private bool ValidateCVC()
        {
            string pattern = @"^[0-9]{3}$";
            if (tbCVC.Text == null)
                return false;
            if (!Regex.IsMatch(tbCVC.Text, pattern))
            {
                return false;
            }
            return true;
        }
        private bool ValidateDate()
        {

            if (datePicker.SelectedDate == null)
                return false;
            if (datePicker.SelectedDate < DateTime.Now.Date)
            {
                return false;
            }


            return true;
        }

        private bool ValidateRacer()
        {
            if (cbRacers.SelectedValue == null)
                return false;

            return true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Page.Content = new HOME();
        }
    }
}

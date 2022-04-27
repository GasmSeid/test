using GONKI.DataSet1TableAdapters;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
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
using System.Windows.Threading;

namespace GONKI
{
    /// <summary>
    /// Логика взаимодействия для Registraciya_gonshika.xaml
    /// </summary>
    public partial class Registraciya_gonshika : Page
    {

        DataSet1 dataSet;
        CountryTableAdapter countryTableAdapter;
        UserTableAdapter userTableAdapter;
        RacerTableAdapter racerTableAdapter;
        GenderTableAdapter genderTableAdapter;
        prom_DanaTableAdapter prom_Dan;

        int age;
        int month;
        int day;
        byte[] imageData;

        private String ConnectionString;
        private SqlConnection connection;
        private SqlCommand cmd = new SqlCommand();
        private SqlDataReader reader;
        public Registraciya_gonshika()
        {
            InitializeComponent();

            dataSet = new DataSet1();
            countryTableAdapter = new CountryTableAdapter();
            userTableAdapter = new UserTableAdapter();
            racerTableAdapter = new RacerTableAdapter();
            genderTableAdapter = new GenderTableAdapter();
            prom_Dan = new prom_DanaTableAdapter();

            countryTableAdapter.Fill(dataSet.Country);
            userTableAdapter.Fill(dataSet.User);
            racerTableAdapter.Fill(dataSet.Racer);
            genderTableAdapter.Fill(dataSet.Gender);
            prom_Dan.Fill(dataSet.prom_Dana);

            sexCB.ItemsSource = dataSet.Gender.DefaultView;
            sexCB.DisplayMemberPath = "Gender_Name";
            sexCB.SelectedValuePath = "ID_Gender";
            sexCB.SelectedIndex = 0;

            stranaCB.ItemsSource = dataSet.Country.DefaultView;
            stranaCB.DisplayMemberPath = "Country_Name";
            stranaCB.SelectedValuePath = "ID_Country";
            stranaCB.SelectedIndex = 0;


            ConnectionString = @"Data Source=localhost;Initial Catalog=Karting;Integrated Security=True";
            connection = new SqlConnection(ConnectionString);
        }





        private void registerBTN_Click(object sender, RoutedEventArgs e)
        {
            if (!emailTB.Text.Equals("") && !passTB.Text.Equals("") && !repeatpassTB.Text.Equals("") && !familiaTB.Text.Equals("") && !nameTB.Text.Equals("") && !birthdayTB.Text.Equals("") && !photoTB.Text.Equals(""))
            {
                if (passTB.Text.Length >= 6)
                {
                    int upperCount = passTB.Text.Count(Char.IsUpper);
                    if ((passTB.Text.Contains('1') || passTB.Text.Contains('2') || passTB.Text.Contains('3') || passTB.Text.Contains('4') ||
                        passTB.Text.Contains('5') || passTB.Text.Contains('6') || passTB.Text.Contains('7') || passTB.Text.Contains('8') ||
                        passTB.Text.Contains('9') || passTB.Text.Contains('0') || passTB.Text.Contains('!') || passTB.Text.Contains('@') ||
                        passTB.Text.Contains('#') || passTB.Text.Contains('$') || passTB.Text.Contains('%') || passTB.Text.Contains('^')) && upperCount > 0)
                    {
                        if (passTB.Text == repeatpassTB.Text)
                        {
                           
                                cmd.CommandText = "SELECT * FROM [dbo].[User] "
                                                   + "WHERE Email = '" + emailTB.Text + "' ";
                                Console.WriteLine(cmd.CommandText);
                                cmd.Connection = connection;
                                connection.Open();
                                reader = cmd.ExecuteReader();
                                int i = 0;
                                while (reader.Read())
                                {
                                    i++;
                                }

                                if (i == 0)
                                {
                                    age = DateTime.Today.Year - birthdayTB.SelectedDate.Value.Year;
                                    if (age == 18)
                                    {
                                        month = DateTime.Today.Month - birthdayTB.SelectedDate.Value.Month;
                                        if (month == 0)
                                        {
                                            day = DateTime.Today.Day - birthdayTB.SelectedDate.Value.Day;
                                        }
                                        else if (month < 0)
                                        {
                                            day = 0;
                                        }
                                    }
                                    else if (age > 18)
                                    {
                                        month = 0;
                                        day = 0;
                                    }

                                connection.Close();
                                if (age < 18)
                                    {
                                        MessageBox.Show("Человеку нет 18 лет!", "Ошибка возраста");
                                    }
                                    else
                                    {
                                        if (month > 0)
                                        {
                                            MessageBox.Show("Человеку нет 18 лет!", "Ошибка возраста");
                                        }
                                        else
                                        {
                                            if (day > 0)
                                            {
                                                MessageBox.Show("Человеку нет 18 лет!", "Ошибка возраста");
                                            }
                                            else
                                            {
                                                if (emailTB.Text.Contains("@yandex.ru") || emailTB.Text.Contains("@mail.ru") || emailTB.Text.Contains("@gmail.ru") || emailTB.Text.Contains("@inbox.ru")
                                                    || emailTB.Text.Contains("@ok.ru") || emailTB.Text.Contains("@rambler.ru") || emailTB.Text.Contains("@yahoo.ru") || emailTB.Text.Contains("@mpt.ru")
                                                    || emailTB.Text.Contains("@yandex.com") || emailTB.Text.Contains("@mail.com") || emailTB.Text.Contains("@gmail.com") || emailTB.Text.Contains("@inbox.com")
                                                    || emailTB.Text.Contains("@ok.com") || emailTB.Text.Contains("@rambler.com") || emailTB.Text.Contains("@yahoo.com") || emailTB.Text.Contains("@mpt.com"))
                                                {
                                                    //Authorization.role = "R";
                                                    //Authorization.email = emailTB.Text;
                                                    
                                                    
                                                    userTableAdapter.Insert(emailTB.Text, passTB.Text, nameTB.Text, familiaTB.Text, "R");
                                                    racerTableAdapter.Insert((string)sexCB.SelectedValue, birthdayTB.SelectedDate.Value, photoTB.Text, (string)stranaCB.SelectedValue, emailTB.Text);

                                                //!!!!!!
                                                int ID = (int)racerTableAdapter.GetIdRacerByEmail(emailTB.Text);

                                                    prom_Dan.Insert((string)sexCB.SelectedValue, birthdayTB.SelectedDate.Value, (string)stranaCB.SelectedValue, emailTB.Text, nameTB.Text, familiaTB.Text, passTB.Text, photoTB.Text,ID);
                                                   
                                                    userTableAdapter.Fill(dataSet.User);
                                                    racerTableAdapter.Fill(dataSet.Racer);
                                                    prom_Dan.Fill(dataSet.prom_Dana);
                                                    connection.Close();
                                                    MessageBox.Show("Гонщик успешно зарегистрирован в системе!", "Успешная регистрация");
                                                connection.Close();
                                                Page.Content = new Reg_na_gonku();
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Введите корректную электронную почту!", "Ошибка почты");
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Гонщик с такой почтой уже существует!", "Ошибка регистрации");
                                }
                           
                        }
                        else
                        {
                            MessageBox.Show("Пароли не совпадают!", "Ошибка пароля");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пароль должен содержать одну цифру, один символ верхнего регистра и один из следующих символов: ! @ # $ % ^", "Ошибка пароля");
                    }
                }
                else
                {
                    MessageBox.Show("Пароль должен содержать минимум 6 символов!", "Ошибка пароля");
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля!", "Ошибка заполнения");
            }
        }

        private void prosmotrBTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Images files (*.png)|*.png|Images files (*.jpg)|*.jpg|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == true)
                    photoTB.Text = openFileDialog.FileName;
                using (FileStream fs = new FileStream(photoTB.Text, FileMode.Open))
                {
                    imageData = new byte[fs.Length];
                    fs.Read(imageData, 0, imageData.Length);
                }
                System.Windows.Controls.Image ImageContainer = new System.Windows.Controls.Image();
                ImageSource image = new BitmapImage(new Uri(photoTB.Text, UriKind.Absolute));
                ImageContainer.Source = image;
                avatarIMG.Source = ImageContainer.Source;
            }
            catch { MessageBox.Show("oshibka"); }
        }











        private void back_Click(object sender, RoutedEventArgs e)
        {
            Page.Content = new Reg_gonshiki();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Page.Content = new Menu_gonshika();

        }

       
    }
}

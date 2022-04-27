using GONKI.DataSet1TableAdapters;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
    /// Логика взаимодействия для Redaktirovanie_Profilya.xaml
    /// </summary>
    public partial class Redaktirovanie_Profilya : Page
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
        public Redaktirovanie_Profilya()
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

            sexCB.ItemsSource = dataSet.Gender.DefaultView;
            sexCB.DisplayMemberPath = "Gender_Name";
            sexCB.SelectedValuePath = "ID_Gender";
            sexCB.SelectedIndex = 0;

            stranaCB.ItemsSource = dataSet.Country.DefaultView;
            stranaCB.DisplayMemberPath = "Country_Name";
            stranaCB.SelectedValuePath = "ID_Country";
            stranaCB.SelectedIndex = 0;

            prom_Dan.Fill(dataSet.prom_Dana);
            Data.ItemsSource = dataSet.prom_Dana.DefaultView;
            Data.SelectionMode = DataGridSelectionMode.Single;
            Data.SelectedValuePath = "ID_racera";
            Data.CanUserAddRows = false;
            Data.CanUserDeleteRows = false;
            Data.IsReadOnly = true;

            prom_Dan.Fill(dataSet.prom_Dana);


            int count = dataSet.prom_Dana.Rows.Count;

            for (int i = 0; i < count; i++)
            {
                DataRowView dataRowView = (DataRowView)Data.Items[index: i];




                emailTB.Text = dataRowView.Row.Field<String>("Email");
                nameTB.Text = dataRowView.Row.Field<String>("Name");
                familiaTB.Text = dataRowView.Row.Field<String>("Familiya");
                passTB.Text = dataRowView.Row.Field<String>("Password");
                photoTB.Text = dataRowView.Row.Field<String>("Puti_k_IMG");
                birthdayTB.SelectedDate = dataRowView.Row.Field<DateTime>("DateOfBirth");
             

                prosmotrBTN_Click1();
            }



            repeatpassTB.Text = passTB.Text;








            ConnectionString = @"Data Source=localhost;Initial Catalog=Karting;Integrated Security=True";
            connection = new SqlConnection(ConnectionString);
        }


        private void registerBTN_Click(object sender, RoutedEventArgs e)
        {
            dataSet = new DataSet1();
            countryTableAdapter = new CountryTableAdapter();
            userTableAdapter = new UserTableAdapter();
            racerTableAdapter = new RacerTableAdapter();
            genderTableAdapter = new GenderTableAdapter();
            prom_Dan = new prom_DanaTableAdapter();

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
                            //try
                            //{
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


                                                    Data.ItemsSource = dataSet.Racer.DefaultView;
                                                    Data.SelectionMode = DataGridSelectionMode.Single;
                                                    Data.SelectedValuePath = "ID_Racer";
                                                    Data.CanUserAddRows = false;
                                                    Data.CanUserDeleteRows = false;
                                                    Data.IsReadOnly = true;
                                                    racerTableAdapter.Fill(dataSet.Racer);

                                                    int Verificate = 0;
                                                    int count1 = dataSet.User.Rows.Count;
                                                    for (int k = 0; k < Data.Items.Count; k++)
                                                    {
                                                        DataRowView dataRowView2 = (DataRowView)Data.Items[index: k];
                                                        if (dataRowView2 != null)
                                                        {
                                                            string email = dataRowView2.Row.Field<String>("Email");
                                                          


                                                            if (emailTB.Text == email)
                                                            {
                                                                Verificate = k;

                                                                racerTableAdapter.UpdateQuery((string)sexCB.SelectedValue, birthdayTB.Text, photoTB.Text, (string)stranaCB.SelectedValue, emailTB.Text, Verificate);

                                                                userTableAdapter.Fill(dataSet.User);
                                                                racerTableAdapter.Fill(dataSet.Racer);
                                                                prom_Dan.Fill(dataSet.prom_Dana);
                                                            }
                                                        }
                                                    }

                                                    Data.ItemsSource = dataSet.User.DefaultView;
                                                    Data.SelectionMode = DataGridSelectionMode.Single;
                                                    Data.SelectedValuePath = "Email";
                                                    Data.CanUserAddRows = false;
                                                    Data.CanUserDeleteRows = false;
                                                    Data.IsReadOnly = true;

                                                    for (int k = 0; k < Data.Items.Count; k++)
                                                    {
                                                        DataRowView dataRowView2 = (DataRowView)Data.Items[index: k];
                                                        if (dataRowView2 != null)
                                                        {
                                                            string email = dataRowView2.Row.Field<String>("Email");



                                                            if (emailTB.Text == email)
                                                            {
                                                                Verificate = k;

                                                                userTableAdapter.UpdateQuery(emailTB.Text, passTB.Text, nameTB.Text, familiaTB.Text, "R", email);
                                                       
                                                       
                                                        



                                                                prom_Dan.UpdateQuery((string)sexCB.SelectedValue, birthdayTB.Text, (string)stranaCB.SelectedValue, emailTB.Text, nameTB.Text, familiaTB.Text, passTB.Text, photoTB.Text, 0);


                                                             


                                                                userTableAdapter.Fill(dataSet.User);
                                                                racerTableAdapter.Fill(dataSet.Racer);
                                                                prom_Dan.Fill(dataSet.prom_Dana);
                                                                connection.Close();
                                                                MessageBox.Show("Гонщик успешно изменен в системе!", "Успешная изменение");
                                                               


                                                            prom_Dan.Fill(dataSet.prom_Dana);
                                                            Data.ItemsSource = dataSet.prom_Dana.DefaultView;
                                                            Data.SelectionMode = DataGridSelectionMode.Single;
                                                            Data.SelectedValuePath = "ID_racera";
                                                            Data.CanUserAddRows = false;
                                                            Data.CanUserDeleteRows = false;
                                                            Data.IsReadOnly = true;

                                                            prom_Dan.Fill(dataSet.prom_Dana);
                                                        }
                                                        }
                                                    }



                                                }
                                                else
                                                {
                                                    MessageBox.Show("Введите корректную электронную почту!", "Ошибка почты");
                                                }
                                            }
                                        }
                                    }
                               
                            //}
                            //catch
                            //{
                            //    MessageBox.Show("Ошибка чтения данных!", "Ошибка чтения БД");
                            //}
                            //finally
                            //{
                            //    connection.Close();
                            //}
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

        private void prosmotrBTN_Click1()
        {
           
            System.Windows.Controls.Image ImageContainer = new System.Windows.Controls.Image();
            ImageSource image = new BitmapImage(new Uri(photoTB.Text, UriKind.Absolute));
            ImageContainer.Source = image;
            avatarIMG.Source = ImageContainer.Source;
        }


        private void back_Click(object sender, RoutedEventArgs e)
        {
            Page.Content = new Menu_gonshika();
        }
    }
}

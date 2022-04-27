using GONKI.DataSet1TableAdapters;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для EditRacer.xaml
    /// </summary>
    public partial class EditRacer : Page
    {
        DataSet1 dataSet1 = new DataSet1();
        Registration_StatusTableAdapter RSTA = new Registration_StatusTableAdapter();
        RegistrationTableAdapter RegTA = new RegistrationTableAdapter();

        GenderTableAdapter GTA = new GenderTableAdapter();
        CountryTableAdapter CTA = new CountryTableAdapter();

        RacerTableAdapter RTA = new RacerTableAdapter();
        UserTableAdapter UTA = new UserTableAdapter();
        byte[] ImageData;
        public EditRacer(string RacerEmail, string RacerPassword, string RacerName, string RacerLastname, string RacerGender, int RacerRegStat, string RacerCountry, string RacerBirthday, string putIMG, byte [] ImageDate
)
        {
            InitializeComponent();
            {
                RSTA.Fill(dataSet1.Registration_Status);
                GTA.Fill(dataSet1.Gender);
                CTA.Fill(dataSet1.Country);
                UTA.Fill(dataSet1.User);
                RegTA.Fill(dataSet1.Registration);

                Gender.ItemsSource = dataSet1.Gender.DefaultView;
                Gender.SelectedValuePath = "ID_Gender";
                Gender.DisplayMemberPath = "Gender_Name";

                Status.ItemsSource = dataSet1.Registration_Status.DefaultView;
                Status.SelectedValuePath = "ID_Registration_Status";
                Status.DisplayMemberPath = "Statu_Name";

                Country.ItemsSource = dataSet1.Country.DefaultView;
                Country.SelectedValuePath = "ID_Country";
                Country.DisplayMemberPath = "Country_Name";

                EMail.Text = RacerEmail;
                pass.Text = RacerPassword;
                REpass.Text = RacerPassword;
                name.Text = RacerName;
                lastname.Text = RacerLastname;
                Gender.SelectedValue = RacerGender;
                Status.SelectedValue = RacerRegStat;
                Country.SelectedValue = RacerCountry;
                //date.Text = RacerBirthday;
                Date.SelectedDate = Convert.ToDateTime(RacerBirthday);
                photoTB.Text = putIMG;
                ImageData = ImageDate;
                prosmotrBTN_Click1();

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
                ImageData = new byte[fs.Length];
                fs.Read(ImageData, 0, ImageData.Length);
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


        private void Button_Click(object sender, RoutedEventArgs e)//отмена
        {
            Page.Content = new Koordinator_gonshiki();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Checking() == true)
                {
                    int id = (int)RTA.GetIdRacerByEmail(EMail.Text);//запрос получаения кода сотрудника по его мейлу

                    RTA.UpdateQuery((string)Gender.SelectedValue, Date.Text, photoTB.Text, (string)Country.SelectedValue, EMail.Text, id);
                    UTA.UpdateQuery(EMail.Text, pass.Text, name.Text, lastname.Text, "R", EMail.Text);
                    RegTA.UpdateQuery((int)Status.SelectedValue, id);


                    MessageBox.Show($"Успешно! Код гонщика: {id} ");
                }
            }
            catch
            {
                MessageBox.Show($"Ошибка при обновлении данных! Операция отклонена!");
            }

        }

        Regex spec = new Regex("[@#$_%-]");
        Regex engL = new Regex("[a-z]");
        Regex engB = new Regex("[A-Z]");
        Regex num = new Regex("[0-9]");

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {



        }
        private bool Checking()
        {
            int i = 0;
            if (!(String.IsNullOrWhiteSpace(pass.Text)) && passCheck(pass.Text) == true) { i++; } else { MessageBox.Show("Неправильный формат пароля!"); }
            if (!(String.IsNullOrWhiteSpace(REpass.Text)) && passCheck(REpass.Text) == true) { i++; } else { MessageBox.Show("Неправильный формат пароля!"); }
            if (pass.Text == REpass.Text) { i++; } else { MessageBox.Show("Пароли не совпадают!"); }


            if (!(String.IsNullOrWhiteSpace(name.Text))) { i++; } else { MessageBox.Show("Имя - обязательное поле!"); }
            if (!(String.IsNullOrWhiteSpace(lastname.Text))) { i++; } else { MessageBox.Show("Фамилия- обязательное поле!"); }
            if (!(String.IsNullOrWhiteSpace(Date.Text))) { i++; } else { MessageBox.Show("Дата рождения - - обязательное поле!"); }


            if (i == 6) return true;
            return false;
        }
        private bool passCheck(string Password)
        {
            int i = 0;
            if (spec.IsMatch(Password)) { i++; }
            if (engB.IsMatch(Password)) { i++; }
            if (engL.IsMatch(Password)) { i++; }
            if (num.IsMatch(Password)) { i++; }
            if (Password.Length >= 7) { i++; }

            if (i == 5) return true;

            return false;
        }
        private void pass_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
    }
}

using GONKI.DataSet1TableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для Koordinator_gonshiki.xaml
    /// </summary>
    public partial class Koordinator_gonshiki : Page
    {

        

        DataSet1 dataSet1 = new DataSet1();
        ViewUserTableAdapter VUTA = new ViewUserTableAdapter();
        UserTableAdapter UTA = new UserTableAdapter();
        Registration_StatusTableAdapter RSTA = new Registration_StatusTableAdapter();
        Event_TypeTableAdapter ETTA = new Event_TypeTableAdapter();

        public Koordinator_gonshiki()
        {
            InitializeComponent();

            VUTA.Fill(dataSet1.ViewUser);
            RSTA.Fill(dataSet1.Registration_Status);
            ETTA.Fill(dataSet1.Event_Type);


            //racers.ItemsSource = VUTA.SelectRacersFromUser().DefaultView;
            racers.ItemsSource = VUTA.GetRacers().DefaultView;

            racers.IsReadOnly = true;

            //racers.Columns[0].Visibility = Visibility.Hidden;
            //racers.Columns[1].Visibility = Visibility.Hidden;
            //racers.Columns[7].Visibility = Visibility.Hidden;

            regState.ItemsSource = dataSet1.Registration_Status.DefaultView;
            regState.SelectedValuePath = "ID_Registration_Status";
            regState.DisplayMemberPath = "Statu_Name";


            raceType.ItemsSource = dataSet1.Event_Type.DefaultView;
            raceType.SelectedValuePath = "ID_Event_type";
            raceType.DisplayMemberPath = "ID_Event_type";
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)//переход на редактированию
        {
            if (racers.SelectedItem != null)
            {
                DataRowView drv = (DataRowView)racers.SelectedItem;
                string email = (string)drv["Почта"];
                string RacerPassword = (string)drv["Пароль"];

                string RacerName = (string)drv["Имя"];
                string RacerLastname = (string)drv["Фамилия"];

                int RacerRegStat = (int)drv["ID_Registration_Status"];
                string RacerCountry = (string)drv["ID_Country"];
                string RacerGender = (string)drv["Gender"];
                string RacerBirthday = Convert.ToString(drv["DateOfBirth"]);
                string putIMG = (string)drv["Put"];
                byte[] imageData = (Byte[])drv["Image"];

                Page.Content = new EditRacer(email, RacerPassword, RacerName, RacerLastname, RacerGender, RacerRegStat, RacerCountry, RacerBirthday, putIMG, imageData);



            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (regState.SelectedItem != null)
            {
                racers.ItemsSource = VUTA.SortRegStatus((int)regState.SelectedValue).DefaultView;
            }
            if (raceType.SelectedItem != null)
            {
                racers.ItemsSource = VUTA.SortByEventType((string)raceType.SelectedValue).DefaultView;
            }

            racers.Columns[0].Visibility = Visibility.Hidden;
            racers.Columns[1].Visibility = Visibility.Hidden;
            racers.Columns[7].Visibility = Visibility.Hidden;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            // racers.ItemsSource = VUTA.SelectRacersFromUser().DefaultView;
            racers.ItemsSource = VUTA.GetRacers().DefaultView;
            racers.IsReadOnly = true;

            racers.Columns[0].Visibility = Visibility.Hidden;
            racers.Columns[1].Visibility = Visibility.Hidden;
            racers.Columns[7].Visibility = Visibility.Hidden;
            regState.SelectedItem = null;
            raceType.SelectedItem = null;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Page.Content = new HOME();
        }
    }
}

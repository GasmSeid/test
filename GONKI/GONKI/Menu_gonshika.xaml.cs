using GONKI.DataSet1TableAdapters;
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

namespace GONKI
{
    /// <summary>
    /// Логика взаимодействия для Menu_gonshika.xaml
    /// </summary>
    public partial class Menu_gonshika : Page
    {
        DataSet1 dataSet1 = new DataSet1();
        prom_DanaTableAdapter p_DTA = new prom_DanaTableAdapter();
      
        public Menu_gonshika()
        {
            InitializeComponent();



        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Page.Content = new Reg_na_gonku();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Page.Content = new Redaktirovanie_Profilya();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            Info nextPage = new Info();
            nextPage.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

            Page.Content = new MySponsor();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Page.Content = new MyResultsWin();
        }
    }
}

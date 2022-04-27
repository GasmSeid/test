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
using System.Windows.Threading;

namespace GONKI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Page
    {
        public MainWindow()
        {
            InitializeComponent();
            Page.Content = new HOME();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromSeconds(1);
            dt.Tick += dtTicker;
            dt.Start();




        }

        private int sek = 5;
        private int min = 1;
        private int hors = 1;
        private int day = 1;
        private int month = 0;
        private int age = 0;
        private void dtTicker(object sender, EventArgs e)
        {
            //  if (age != 0 && sek != 0 && min != 0 && hors != 0 && day != 0 && month != 0)

            if (sek == 0 && min == 0 && hors == 0 && day == 0 && month == 0)
            {
                if (age != 0)
                {
                    sek = 18;
                    min = 13;
                    hors = 5;
                    day = 5;
                    month = 11;
                    age = age - 1;
                }
                else { sek = 0; }
            }

            if (sek == 0 && min != 0)
            {
                sek = 60;
                min = min - 1;
            }
            if (min == 0 && hors != 0)
            {
                min = 59;
                hors = hors - 1;
            }
            if (hors == 0 && day != 0)
            {
                hors = 23;
                day = day - 1;
            }
            if (day == 0 && month != 0)
            {
                day = 30;
                month = month - 1;
            }
            if (month == 0 && age != 0)
            {
                month = 11;

            }


            sek--;

            TimerLabel.Content = "До начала события осталось: " + age.ToString() + " Лет " + month.ToString() + " Месяцев " + day.ToString() + " Дней " + hors.ToString() + " Часов " + min.ToString() + " Минут " + sek.ToString() + " Секунд ";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Page.Content = new HOME();
            prom_DanaTableAdapter pr = new prom_DanaTableAdapter();
            DataSet1 dataSet = new DataSet1();
            int count1 = dataSet.prom_Dana.Rows.Count;
            if (count1 != 0)
            {
                pr.DeleteQuery(0);
            }
        }
    }
}

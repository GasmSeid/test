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

namespace GONKI
{
    /// <summary>
    /// Логика взаимодействия для O_Sobitii.xaml
    /// </summary>
    public partial class O_Sobitii : Page
    {
        public O_Sobitii()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Page.Content = new Pred_result();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Page.Content = new KartSkills2017();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Page.Content = new Spisok_blag_org();
        }
    }
}

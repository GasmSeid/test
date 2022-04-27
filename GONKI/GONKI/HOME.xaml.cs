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
    /// Логика взаимодействия для HOME.xaml
    /// </summary>
    public partial class HOME : Page
    {
        public HOME()
        {
            InitializeComponent();
        }

        private void reg_gon_Click(object sender, RoutedEventArgs e)
        {
            Page.Content = new Reg_gonshiki();
          
        }

        private void reg_sponsors_Click(object sender, RoutedEventArgs e)
        {
            Page.Content = new Reg_sponsora();
        }

        private void o_sobitii_Click(object sender, RoutedEventArgs e)
        {
            Page.Content = new O_Sobitii();
        }

        private void vhod_v_sistemu_Click(object sender, RoutedEventArgs e)
        {
            Page.Content = new Autorization();
        }

       
    }
}

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
    /// Логика взаимодействия для KartSkills2017.xaml
    /// </summary>
    public partial class KartSkills2017 : Page
    {
        public KartSkills2017()
        {
            InitializeComponent();
        }

        private void btnMap_Click(object sender, RoutedEventArgs e)
        {
            MapPage nextPage = new MapPage();
            nextPage.Show();
          
        }
    }
}

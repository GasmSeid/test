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
    /// Логика взаимодействия для Reg_gonshiki.xaml
    /// </summary>
    public partial class Reg_gonshiki : Page
    {
        public Reg_gonshiki()
        {
            InitializeComponent();
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            Page.Content = new Autorization();
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            Page.Content = new Registraciya_gonshika();
        }
    }
}

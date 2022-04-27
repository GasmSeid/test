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
    /// Логика взаимодействия для Thanks.xaml
    /// </summary>
    public partial class Thanks : Page
    {
        public Thanks()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Page.Content = new HOME();
        }

        public void SetInfo(string name, string country)
        {
            textBlockInfo.Text = name + " из " + country;
        }

        public void SetSum(string sum)
        {
            textBlockSum.Text = sum;
        }
    }
}

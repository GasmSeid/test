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
using System.Windows.Shapes;

namespace GONKI
{
    /// <summary>
    /// Логика взаимодействия для Koordinator_sponsori.xaml
    /// </summary>
    public partial class Koordinator_sponsori : Page
    {

        DataSet1 dataSet1 = new DataSet1();
        CharityTableAdapter CTA = new CharityTableAdapter();
        ViewCharity2TableAdapter VC2TA = new ViewCharity2TableAdapter();
        ViewRegistrationTableAdapter VRTA = new ViewRegistrationTableAdapter();

        public Koordinator_sponsori()
        {
            InitializeComponent();

            VC2TA.Fill(dataSet1.ViewCharity2);
            CTA.Fill(dataSet1.Charity);
            VRTA.Fill(dataSet1.ViewRegistration);

            charity.ItemsSource = VC2TA.GetData().DefaultView;
            charity.IsReadOnly = true;

            CharityCount.Content = $" Всего благотворительных фондов: {dataSet1.Charity.Rows.Count}";
            int i = (int)VC2TA.GetSum();
            CharityAmount.Content = $"Общая сумма пожертвований: {i}$";
        }
    }
}

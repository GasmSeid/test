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
using GONKI.DataSet1TableAdapters;

namespace GONKI
{
    /// <summary>
    /// Логика взаимодействия для MyResultsWin.xaml
    /// </summary>
    public partial class MyResultsWin : Page
    {
        
        DataSet1 dataSet = new DataSet1();
        SponsorshipTableAdapter STA = new SponsorshipTableAdapter();
        RacerTableAdapter RTA = new RacerTableAdapter();
        prom_DanaTableAdapter prom = new prom_DanaTableAdapter();
        ViewResultTableAdapter VRTA = new ViewResultTableAdapter();
        //prom_DanaTableAdapter p_DTA = new prom_DanaTableAdapter(); 

        int IdRacer;
        string mail;
        string mail2;
        public MyResultsWin()
        {
            InitializeComponent();
           


            results.ItemsSource = dataSet.prom_Dana.DefaultView;
            results.SelectionMode = DataGridSelectionMode.Single;
            results.SelectedValuePath = "ID_racera";
            results.CanUserAddRows = false;
            results.CanUserDeleteRows = false;
            results.IsReadOnly = true;
            prom.Fill(dataSet.prom_Dana);
            DataRowView dataRowView1 = (DataRowView)results.Items[index: 0];
            mail = dataRowView1.Row.Field<String>("Email");


            results.ItemsSource = dataSet.Racer.DefaultView;
            results.SelectionMode = DataGridSelectionMode.Single;
            results.SelectedValuePath = "ID_Racer";
            results.CanUserAddRows = false;
            results.CanUserDeleteRows = false;
            results.IsReadOnly = true;
            RTA.Fill(dataSet.Racer);


            int count = dataSet.Racer.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                DataRowView dataRowView = (DataRowView)results.Items[index: i];

                mail2 = dataRowView1.Row.Field<String>("Email");

                if (mail2 == mail)
                {
                    count = 0;
                    IdRacer = dataRowView.Row.Field<int>("ID_Racer");
                }
              

            }


            results.ItemsSource = dataSet.ViewResult.DefaultView;
            results.SelectionMode = DataGridSelectionMode.Single;
            results.SelectedValuePath = "ID_Result";
            results.CanUserAddRows = false;
            results.CanUserDeleteRows = false;
            results.IsReadOnly = true;
            VRTA.Fill(dataSet.ViewResult);



        }

            private void results_Loaded(object sender, RoutedEventArgs e)
        {

            //p_DTA.Fill(dataSet1.prom_Dana);

            VRTA.Fill(dataSet.ViewResult);

            //int count = dataSet1.prom_Dana.Rows.Count;
            //for (int i = 0; i < count; i++)
            //{
               


            //}



            results.ItemsSource = VRTA.IDRacerSelectResult(IdRacer);
            results.IsReadOnly = true;

            results.Columns[0].Visibility = Visibility.Hidden;
            results.Columns[1].Visibility = Visibility.Hidden;
            results.Columns[2].Visibility = Visibility.Hidden;
            results.Columns[3].Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

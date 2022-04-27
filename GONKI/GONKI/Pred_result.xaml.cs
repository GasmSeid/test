using GONKI.DataSet1TableAdapters;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
    /// Логика взаимодействия для Pred_result.xaml
    /// </summary>
    public partial class Pred_result : Page
    {

        private string _connection = @"Data Source=localhost;Initial Catalog=Karting;Integrated Security=True";

        DataSet1 dataSet;
        RacerTableAdapter racer;
        public Pred_result()
        {
            InitializeComponent();
            dataSet = new DataSet1();
            racer = new RacerTableAdapter();

            racer.Fill(dataSet.Racer);

            int count = dataSet.Racer.Rows.Count;

            TB_niz.Text = "Всего пилотов: " + count + "  Всего пилотов финишировало: " + (count - 13) + "  Среднее время: 4m 23s";
            string command = "Select * from [Event]";
            DataSet events = new DataSet();
            FillDataSet(events, command);
            cbEvents.ItemsSource = events.Tables[0].DefaultView;

            command = "Select * from [Gender]";
            DataSet genders = new DataSet();
            FillDataSet(genders, command);
            cbGenders.ItemsSource = genders.Tables[0].DefaultView;

            command = "Select * from [Event_Type]";
            DataSet eventTypes = new DataSet();
            FillDataSet(eventTypes, command);
            cbRaceTypes.ItemsSource = eventTypes.Tables[0].DefaultView;


            command = "select * from Result join Registration on Result.ID_Registration = Registration.ID_Registration join Racer " +
" on Racer.ID_Racer = Registration.ID_Racer join[User] on[User].Email = Racer.Email join[Event] on[Event].ID_Event = Result.ID_Event " +
" join Event_Type on Event_Type.ID_Event_Type = [Event].ID_EventType ";
            DataSet view = new DataSet();
            FillDataSet(view, command);
            dataGrid.ItemsSource = view.Tables[0].DefaultView;
        }

        private void FillDataSet(DataSet dataSet, string command)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(command, _connection);
            adapter.Fill(dataSet);
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            Filter();
        }

        private void Filter()
        {
            if (cbEvents.SelectedValue != null && cbGenders.SelectedValue != null && cbRaceTypes.SelectedValue != null)
            {

                string command = "select * from Result join Registration on Result.ID_Registration = Registration.ID_Registration join Racer " +
   " on Racer.ID_Racer = Registration.ID_Racer join[User] on[User].Email = Racer.Email join[Event] on[Event].ID_Event = Result.ID_Event " +
   $" join Event_Type on Event_Type.ID_Event_Type = [Event].ID_EventType where [Event].ID_Event = {(int)cbEvents.SelectedValue} and " +
   $"Racer.Gender = '{(string)cbGenders.SelectedValue}' and Event_Type.ID_Event_Type = '{(string)cbRaceTypes.SelectedValue}'";
                DataSet view = new DataSet();
                FillDataSet(view, command);
                dataGrid.ItemsSource = view.Tables[0].DefaultView;
            }
            else
            {
                MessageBox.Show("Выберите все фильтры");
            }
        }

    }
}

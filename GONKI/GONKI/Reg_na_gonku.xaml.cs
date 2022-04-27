using GONKI.DataSet1TableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для Reg_na_gonku.xaml
    /// </summary>
    public partial class Reg_na_gonku : Page
    {
        DataSet1 dataSet1 = new DataSet1();
        CharityTableAdapter CTA = new CharityTableAdapter();
        RegistrationTableAdapter RegTA = new RegistrationTableAdapter();
        KitTableTableAdapter KTA = new KitTableTableAdapter();

        SponsorshipTableAdapter STA = new SponsorshipTableAdapter();
        RacerTableAdapter RTA = new RacerTableAdapter();
        prom_DanaTableAdapter prom = new prom_DanaTableAdapter();
        ViewResultTableAdapter VRTA = new ViewResultTableAdapter();

        int i = 0;
        string SelectedKitId = "A";

        int IdRacer;
        string mail;
        string mail2;

        public Reg_na_gonku()
        {
            InitializeComponent();

            CTA.Fill(dataSet1.Charity);
            RegTA.Fill(dataSet1.Registration);
            Charity.ItemsSource = dataSet1.Charity.DefaultView;
            Charity.SelectedValuePath = "ID_Сharity";
            Charity.DisplayMemberPath = "Charity_Name";
            Charity.SelectedIndex = 0;
            Itogo.Content = i.ToString();



            results.ItemsSource = dataSet1.prom_Dana.DefaultView;
            results.SelectionMode = DataGridSelectionMode.Single;
            results.SelectedValuePath = "ID_racera";
            results.CanUserAddRows = false;
            results.CanUserDeleteRows = false;
            results.IsReadOnly = true;
            prom.Fill(dataSet1.prom_Dana);
            DataRowView dataRowView1 = (DataRowView)results.Items[index: 0];
            mail = dataRowView1.Row.Field<String>("Email");


            results.ItemsSource = dataSet1.Racer.DefaultView;
            results.SelectionMode = DataGridSelectionMode.Single;
            results.SelectedValuePath = "ID_Racer";
            results.CanUserAddRows = false;
            results.CanUserDeleteRows = false;
            results.IsReadOnly = true;
            RTA.Fill(dataSet1.Racer);


            int count = dataSet1.Racer.Rows.Count;
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
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            Page.Content = new Menu_gonshika();
        }

        private void ZaezdA_Click(object sender, RoutedEventArgs e)
        {
            if (ZaezdA.IsChecked == true)
            { i += 25; }
            else { i -= 25; }
            Itogo.Content = i.ToString();
        }

        private void ZaezdB_Click(object sender, RoutedEventArgs e)
        {
            if (ZaezdB.IsChecked == true)
            { i += 40; }
            else { i -= 40; }
            Itogo.Content = i.ToString();
        }

        private void ZaezdC_Click(object sender, RoutedEventArgs e)
        {
            if (ZaezdC.IsChecked == true)
            { i += 60; }
            else { i -= 60; }
            Itogo.Content = i.ToString();
        }

        bool c;
        bool b;

        private void VarC_Checked(object sender, RoutedEventArgs e)
        {
            if (VarC.IsChecked == true)
            { i += 50; c = true; }
            if (b == true)
            {
                i -= 30;
                b = false;
            }
            Itogo.Content = i.ToString();
            SelectedKitId = "C";
        }

        private void VarB_Checked(object sender, RoutedEventArgs e)
        {
            if (VarB.IsChecked == true)
            { i += 30; b = true; }
            if (c == true)
            {
                i -= 50;
                c = false;
            }
            Itogo.Content = i.ToString();
            SelectedKitId = "B";
        }

        private void VarA_Checked(object sender, RoutedEventArgs e)
        {
            if (c == true)
            {
                i -= 50;
                c = false;
            }
            if (b == true)
            {
                i -= 30;
                b = false;
            }
            Itogo.Content = i.ToString();
            SelectedKitId = "A";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Regex  r = new Regex("[0-9]");
            if (r.IsMatch(costField.Text) && Convert.ToInt32(Itogo.Content) != 0)
            {

                RegTA.Insert(IdRacer, DateTime.Now, 1, Convert.ToInt32(costField.Text), (int)Charity.SelectedValue, Convert.ToInt32(Itogo.Content), SelectedKitId);
                MessageBox.Show("УСПЕШНО!");
            }
        }
    }
}

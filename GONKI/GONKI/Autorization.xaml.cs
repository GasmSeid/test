using GONKI.DataSet1TableAdapters;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
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
using System.Windows.Threading;

namespace GONKI
{
    /// <summary>
    /// Логика взаимодействия для Autorization.xaml
    /// </summary>
    /// 
    
    public partial class Autorization : Page
    {
        DataSet1 dataSet;
        UserTableAdapter user;

        public Autorization()
        {
            InitializeComponent();
            dataSet = new DataSet1();
            user = new UserTableAdapter();

            user.Fill(dataSet.User);

            Data.ItemsSource = dataSet.User.DefaultView;
            Data.SelectionMode = DataGridSelectionMode.Single;
            Data.SelectedValuePath = "Email";
            Data.CanUserAddRows = false;
            Data.CanUserDeleteRows = false;
            Data.IsReadOnly = true;

        }



        private void Btn_go_Click(object sender, RoutedEventArgs e)
        {


            ///////////////
            DataSet1 dataSet = new DataSet1();
            prom_DanaTableAdapter prom_Dan = new prom_DanaTableAdapter();
            prom_Dan.Fill(dataSet.prom_Dana);
            



            user = new UserTableAdapter();

            user.Fill(dataSet.User);

            Data.ItemsSource = dataSet.User.DefaultView;
            Data.SelectionMode = DataGridSelectionMode.Single;
            Data.SelectedValuePath = "Email";
            Data.CanUserAddRows = false;
            Data.CanUserDeleteRows = false;
            Data.IsReadOnly = true;
            user.Fill(dataSet.User);

           

            int count = dataSet.User.Rows.Count;

            for (int i = 0; i < count; i++)
            {
                DataRowView dataRowView3 = (DataRowView)Data.Items[index: i];



                
                string a1 = dataRowView3.Row.Field<String>("Email");
                string b1 = dataRowView3.Row.Field<String>("Password");
                string c1 = dataRowView3.Row.Field<String>("ID_Role");



                if (Login_tb.Text == a1 && Password_tb.Text == b1)
                {
                    if (c1 == "R")
                    {
                        

                        count = 0;
                        Page.Content = new Reg_na_gonku();

                    }
                    else if (c1 == "A")
                    {
                        count = 0;
                        Page.Content = new AdminMenu();

                    }
                    else if (c1 == "C")
                    {
                        RacerTableAdapter racer = new RacerTableAdapter();
                        racer.Fill(dataSet.Racer);



                        Data.ItemsSource = dataSet.Racer.DefaultView;
                        Data.SelectionMode = DataGridSelectionMode.Single;
                        Data.SelectedValuePath = "ID_Racer";
                        Data.CanUserAddRows = false;
                        Data.CanUserDeleteRows = false;
                        Data.IsReadOnly = true;



                        int count2 = dataSet.Racer.Rows.Count;


                        string sex;
                        DateTime birthday;
                        string strana;
                        string mail;
                        string name;
                        string fam;
                        string pass;
                        string photo;
                        int idR;


                        for (int l = 0; l < count2; l++)
                        {
                            DataRowView dataRowView = (DataRowView)Data.Items[index: l];

                            string a2 = dataRowView.Row.Field<String>("Email");

                            if (Login_tb.Text == a2)
                            {
                                idR = dataRowView.Row.Field<int>("ID_Racer");
                                sex = dataRowView.Row.Field<String>("Gender");
                                birthday = dataRowView.Row.Field<DateTime>("DateOfBirth");
                                photo = dataRowView.Row.Field<String>("Put");
                                strana = dataRowView.Row.Field<String>("ID_Country");
                                mail = dataRowView.Row.Field<String>("Email");



                                user.Fill(dataSet.User);

                                Data.ItemsSource = dataSet.User.DefaultView;
                                Data.SelectionMode = DataGridSelectionMode.Single;
                                Data.SelectedValuePath = "Email";
                                Data.CanUserAddRows = false;
                                Data.CanUserDeleteRows = false;
                                Data.IsReadOnly = true;

                                for (int k = 0; k < count2; k++)
                                {
                                    DataRowView dataRowView2 = (DataRowView)Data.Items[index: k];

                                    name = dataRowView2.Row.Field<String>("First_Name");
                                    fam = dataRowView2.Row.Field<String>("Last_Name");
                                    pass = dataRowView2.Row.Field<String>("Password");



                                    prom_Dan.Insert(sex, birthday, strana, mail, name, fam, pass, photo,idR);
                                }
                            }
                        }
                        count = 0;
                        Page.Content = new Koordinator_menu();

                    }
                }


            }
        }

    }
}

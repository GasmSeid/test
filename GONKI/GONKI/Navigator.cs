using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;


namespace WorldSkillsFramework
{
    class Navigator
    {
        public static void Navigate(object obj)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;         
            NavigationService navigationService = mainWindow.mainFrame.NavigationService;
            navigationService.Navigate(obj);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfApp2.Entities;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            try 
            {
                Helper.DbContext = new Entities.Context();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                Current.Shutdown();
            }
        }
    }
}

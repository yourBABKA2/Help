using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp2.Entities
{
    internal class Helper
    {
        public static Context DbContext;
        public static Frame MainFrame;
        public static User user;

        public static void ImportPhoto()
        {
            foreach (var file in Directory.GetFiles(@"C:\\Users\\chike\\Desktop\\import"))
            {
                var fileName = Path.GetFileNameWithoutExtension(file);
                foreach (var item in DbContext.SomeEvent.ToList())
                {
                    if (item.Name == fileName)
                    {
                        item.Photo = File.ReadAllBytes(file);
                    }
                }

            }
            SaveDb();
        }

        public static void SaveDb()
        {
            try
            {
                DbContext.SaveChanges();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static void SaveInvoke(Action action)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Непредвиденная ошибка:  {e.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp2.Entities;

namespace WpfApp2.View
{
    /// <summary>
    /// Логика взаимодействия для Catalog.xaml
    /// </summary>
    public partial class Catalog : Page
    {
        string search;
        public Catalog()
        {
            InitializeComponent();
            Init();
            Update();
        }

        public void Init()
        {
            if (Helper.user != null) TextBlockName.Text = Helper.user.FullName;
            else TextBlockName.Text = "Гостевой аккаунт";

            ComboSortByDate.SelectedIndex = 0;
            ComboSortByDate.ItemsSource = new List<string> { "По возрастанию", "По убыванию" };

            ComboDirection.SelectedIndex = 0;
            ComboDirection.SelectedIndex = 0;
            var direction = Helper.DbContext.Direction.ToList();
            var allDirection = new Direction { Name = "Все события" };
            Helper.DbContext.Entry(allDirection).State = System.Data.Entity.EntityState.Detached;
            direction.Add(allDirection);
            ComboDirection.DisplayMemberPath = "Name";
            ComboDirection.ItemsSource = direction;
        }

        public void Update()
        {
            List<Event> events = null;
            events = Helper.DbContext.SomeEvent.ToList();
            var count = events.Count;
            if (events != null)
            {
                SortByDate(ref events);
                SortByDirection(ref events);

                if (!String.IsNullOrEmpty(search))
                {
                    events = events.Where(x => x.Name.Contains(search)).ToList();
                }

                ListOfEvents.ItemsSource = events;
                var currentCount = events.Count;
                EventCount .Text = $"Количество мероприятий: {currentCount} из {count}";
            }
            else
            {
                MessageBox.Show("Не удалось получить данные из таблицы События");
                return;
            }
        }

        public void SortByDate(ref List<Event> events)
        {
            if (ComboSortByDate.SelectedIndex == 0) events = events.OrderBy(x => x.DateTime).ToList();
            else events = events.OrderByDescending(x => x.DateTime).ToList();
        }

        public void SortByDirection(ref List<Event> events)
        {
            if (ComboDirection.SelectedItem is Direction selectedDirection)
            {
                if (selectedDirection.Name == "Все события") return;
                events = events.Where(x => x.Direction.Name == selectedDirection.Name).ToList();
            }
        }

        private void ComboSortByDate_Changed(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void ComboDirection_Changed(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void OnTextBoxSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            search = TextBoxSearch.Text.Trim();
            Update();
        }
    }
}

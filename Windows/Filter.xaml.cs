using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace Practical_lesson_No._28.Windows
{
    /// <summary>
    /// Логика взаимодействия для Filter.xaml
    /// </summary>
    public partial class Filter : Window
    {
        public bool FilterEnable = false;

        public Filter()
        {
            InitializeComponent();
            CBName.DisplayMemberPath = "Name";
            foreach (var clubx in MainWindow.Instance.computerClubs)
            {
                CBName.Items.Add(clubx);
                CBOpeningHours.Items.Add(clubx.OpeningHours);
            }
        }

        private void ApplyFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TBDateStart.Text) && !DateTime.TryParseExact(TBDateStart.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateStart))
            {
                MessageBox.Show("Первая дата указана не правильно.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!string.IsNullOrEmpty(TBDateEnd.Text) && !DateTime.TryParseExact(TBDateEnd.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateEnd))
            {
                MessageBox.Show("Вторая дата указана не правильно.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!string.IsNullOrEmpty(TBTime.Text) && !DateTime.TryParseExact(TBTime.Text, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime time))
            {
                MessageBox.Show("Время указано не правильно.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            FilterEnable = true;
            MessageBox.Show("Был включён фильтр.", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Hide();
        }
    }
}

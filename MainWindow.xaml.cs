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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Practical_lesson_No._28
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Instance { get; private set; }
        public enum roles{ 
            admin,
            user,
            nothing
        }

        private roles role = roles.nothing;

        public List<Classes.ComputerClub> computerClubs = new List<Classes.ComputerClub>();
        public List<Classes.ComputerRental> computerRentals = new List<Classes.ComputerRental>();
        public MainWindow()
        {
            InitializeComponent();
            Instance = this;
        }

        void View(roles role)
        {
            this.role = role;

            computerClubs = Classes.ComputerClub.GetAll();
            computerRentals = Classes.ComputerRental.GetAll();

            ClubPanel.Children.Clear();
            if(role == roles.admin)
                ClubPanel.Children.Add(new Element.СomputerСlubElement(role));
            foreach (var x in computerClubs)
                ClubPanel.Children.Add(new Element.СomputerСlubElement(role, x));

            RentalPanel.Children.Clear();
            if (role == roles.admin)
                RentalPanel.Children.Add(new Element.ComputerRentalElement(role));
            foreach (var x in computerRentals)
                RentalPanel.Children.Add(new Element.ComputerRentalElement(role, x, computerClubs.Find(i => i.ClubID == x.ClubID)));
        }

        public void Admin_Click(object sender, RoutedEventArgs e)
            => View(roles.admin);
        

        public void User_Click(object sender, RoutedEventArgs e)
            => View(roles.user);

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            if(role == roles.nothing)
            {
                MessageBox.Show("Зайдите под администратором или пользователем.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }    
            Windows.Filter filter = new Windows.Filter();
            filter.ShowDialog();

            List<Classes.ComputerClub> clubs = computerClubs;
            List<Classes.ComputerRental> rental = computerRentals; 

            if (filter.CBName.SelectedIndex != -1)      
                clubs = clubs.FindAll(x => x == ((Classes.ComputerClub)filter.CBName.SelectedItem));

            if(!string.IsNullOrEmpty(filter.TBAddress.Text))
                clubs = clubs.FindAll(x => x.Address == filter.TBAddress.Text);

            if (filter.CBOpeningHours.SelectedIndex != -1)
                clubs = clubs.FindAll(x => x.OpeningHours == filter.CBOpeningHours.ToString());


            if (!string.IsNullOrEmpty(filter.TBClientFullName.Text))
                rental = rental.FindAll(x => x.ClientFullName == filter.TBClientFullName.Text);

            if (!string.IsNullOrEmpty(filter.TBDateStart.Text))
                rental = rental.FindAll(x => x.DateAndTime >= DateTime.ParseExact(filter.TBDateStart.Text, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None));

            if (!string.IsNullOrEmpty(filter.TBDateEnd.Text))
                rental = rental.FindAll(x => x.DateAndTime <= DateTime.ParseExact(filter.TBDateEnd.Text, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None));

            if (!string.IsNullOrEmpty(filter.TBTime.Text))
                rental = rental.FindAll(x => x.DateAndTime.ToString("HH:mm") == DateTime.ParseExact(filter.TBDateEnd.Text, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None).ToString());

            List<int> clubIds = clubs.Select(c => c.ClubID).ToList();
            rental = rental.FindAll(x => clubIds.Contains(x.ClubID));

            ClubPanel.Children.Clear();
            foreach (var x in clubs)
                ClubPanel.Children.Add(new Element.СomputerСlubElement(role, x));

            RentalPanel.Children.Clear();
            foreach (var x in rental)
                RentalPanel.Children.Add(new Element.ComputerRentalElement(role, x, computerClubs.Find(i => i.ClubID == x.ClubID)));

            filter.Close();
        }
    }
}

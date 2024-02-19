using System;
using System.Collections.Generic;
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
            user
        }
        public List<Classes.ComputerClub> computerClubs = new List<Classes.ComputerClub>();
        public List<Classes.ComputerRental> computerRentals = new List<Classes.ComputerRental>();
        public MainWindow()
        {
            InitializeComponent();
            Instance = this;
        }

        public void Admin_Click(object sender, RoutedEventArgs e)
        {
            computerClubs = Classes.ComputerClub.GetAll();
            computerRentals = Classes.ComputerRental.GetAll();

            ClubPanel.Children.Clear();
            ClubPanel.Children.Add(new Element.СomputerСlubElement(roles.admin));
            foreach (var x in computerClubs)
                ClubPanel.Children.Add(new Element.СomputerСlubElement(roles.admin, x));

            RentalPanel.Children.Clear();
            RentalPanel.Children.Add(new Element.ComputerRentalElement(roles.admin));
            foreach (var x in computerRentals)
                RentalPanel.Children.Add(new Element.ComputerRentalElement(roles.admin, x, computerClubs.Find(i => i.ClubID == x.ClubID)));
        }

        public void User_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

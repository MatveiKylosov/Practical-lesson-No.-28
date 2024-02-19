using Practical_lesson_No._28.Classes;
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
using System.Xml.Linq;

namespace Practical_lesson_No._28.Element
{
    /// <summary>
    /// Логика взаимодействия для ComputerRentalElement.xaml
    /// </summary>
    public partial class ComputerRentalElement : UserControl
    {
        Classes.ComputerRental computerRental;
        Classes.ComputerClub club;
        bool edit = false;

        public ComputerRentalElement(MainWindow.roles roles, Classes.ComputerRental computerRental = null, Classes.ComputerClub club = null)
        {
            InitializeComponent();
            if (roles == MainWindow.roles.user)
                InsertUpdateBT.Width = DeleteBT.Width = InsertUpdateBT.Height = DeleteBT.Height = 0;

            if ( computerRental != null)
            {
                ClubName.Text = MainWindow.Instance.computerClubs.Find(x => x.ClubID == computerRental.ClubID).Name;
                ClientFullName.Text = computerRental.ClientFullName;
                DateAndTime.Text = computerRental.DateAndTime.ToString("dd.MM.yyyy HH:mm");
                this.club = club;
                //combobox
            }
            else
            {
                TBClientFullName.Visibility = CBClubName.Visibility = TBDateAndTime.Visibility = Visibility.Visible;
                InsertUpdateBT.Content = "Добавить";
                DeleteBT.Content = "Стереть";
            }

            CBClubName.DisplayMemberPath = "Name";

            foreach (var clubx in MainWindow.Instance.computerClubs)
                CBClubName.Items.Add(clubx);

            this.computerRental = computerRental;
        }

        private void InsertUpdateBT_Click(object sender, RoutedEventArgs e)
        {
            if (computerRental != null)
            {
                if (edit)
                {
                    if (string.IsNullOrEmpty(TBClientFullName.Text) || CBClubName.SelectedIndex == -1 || string.IsNullOrEmpty(TBDateAndTime.Text) ||
                        !DateTime.TryParseExact(TBDateAndTime.Text, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
                        return;
                    
                    ClientFullName.Text = TBClientFullName.Text; 
                    DateAndTime.Text = TBDateAndTime.Text;
                    ClubName.Text = ((Classes.ComputerClub)CBClubName.SelectedItem).Name;
                    club = ((Classes.ComputerClub)CBClubName.SelectedItem);

                    computerRental.Update(date, TBClientFullName.Text, 0);

                    TBClientFullName.Visibility = CBClubName.Visibility = TBDateAndTime.Visibility = Visibility.Hidden;
                    DeleteBT.Content = "Удалить";

                    edit = false;
                }
                else
                {
                    TBClientFullName.Text = ClientFullName.Text;
                    TBDateAndTime.Text = DateAndTime.Text;
                    CBClubName.Text = club.Name;

                    TBClientFullName.Visibility = CBClubName.Visibility = TBDateAndTime.Visibility = Visibility.Visible;
                    DeleteBT.Content = "Отменить";
                    edit = true;
                }
            }
            else
            {
                if (CBClubName.SelectedIndex != -1 & !string.IsNullOrEmpty(TBClientFullName.Text) & !string.IsNullOrEmpty(TBDateAndTime.Text) &
                    DateTime.TryParseExact(TBDateAndTime.Text, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
                {
                    Classes.ComputerRental.Insert(date, TBClientFullName.Text, ((Classes.ComputerClub)CBClubName.SelectedItem).ClubID);
                    MainWindow.Instance.Admin_Click(null, null);
                }
            }
        }

        private void DeleteBT_Click(object sender, RoutedEventArgs e)
        {
            if (computerRental != null)
            {
                if (edit)
                {
                    TBClientFullName.Visibility = CBClubName.Visibility = TBDateAndTime.Visibility = Visibility.Hidden;
                    DeleteBT.Content = "Удалить";

                    edit = false;
                }
                else
                {
                    computerRental.Delete();
                    MainWindow.Instance.Admin_Click(null, null);
                }
            }
            else
            {
                TBClientFullName.Text = TBDateAndTime.Text = "";
                CBClubName.SelectedIndex = -1;
            }
        }
    }
}

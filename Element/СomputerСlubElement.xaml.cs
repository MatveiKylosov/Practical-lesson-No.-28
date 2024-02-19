using Practical_lesson_No._28.Classes;
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

namespace Practical_lesson_No._28.Element
{
    /// <summary>
    /// Логика взаимодействия для СomputerСlubElement.xaml
    /// </summary>
    public partial class СomputerСlubElement : UserControl
    {
        Classes.ComputerClub computerClub;
        bool edit = false;

        public СomputerСlubElement(MainWindow.roles roles, Classes.ComputerClub сomputerСlub = null)
        {
            InitializeComponent();
            if (roles == MainWindow.roles.user)
                InsertUpdateBT.Width = DeleteBT.Width = InsertUpdateBT.Height = DeleteBT.Height = 0;

            if (сomputerСlub != null)
            {
                Name.Text = сomputerСlub.Name;
                Address.Text = сomputerСlub.Address;
                OpeningHours.Text = сomputerСlub.OpeningHours;
            }
            else
            {
                TBName.Visibility = TBAddress.Visibility = TBOpeningHours.Visibility = Visibility.Visible;
                InsertUpdateBT.Content = "Добавить";
                DeleteBT.Content = "Стереть";
            }

            this.computerClub = сomputerСlub;
        }

        private void InsertUpdateBT_Click(object sender, RoutedEventArgs e)
        {
            if (computerClub != null)
            {
                if (edit)
                {
                    if (string.IsNullOrEmpty(TBName.Text) || string.IsNullOrEmpty(TBAddress.Text) || string.IsNullOrEmpty(TBOpeningHours.Text))
                        return;

                    Name.Text = TBName.Text;
                    Address.Text = TBAddress.Text;
                    OpeningHours.Text = TBOpeningHours.Text;

                    computerClub.Update(Name.Text, Address.Text, OpeningHours.Text);

                    TBName.Visibility = TBAddress.Visibility = TBOpeningHours.Visibility = Visibility.Hidden;
                    DeleteBT.Content = "Удалить";

                    edit = false;
                } else {
                    TBName.Text = Name.Text;
                    TBAddress.Text = Address.Text;
                    TBOpeningHours.Text = OpeningHours.Text;

                    TBName.Visibility = TBAddress.Visibility = TBOpeningHours.Visibility = Visibility.Visible;
                    DeleteBT.Content = "Отменить";
                    edit = true;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(TBName.Text) & !string.IsNullOrEmpty(TBAddress.Text) & !string.IsNullOrEmpty(TBOpeningHours.Text)) {
                    Classes.ComputerClub.Insert(TBName.Text, TBAddress.Text, TBOpeningHours.Text);
                    MainWindow.Instance.Admin_Click(null, null);
                }
            }
        }

        private void DeleteBT_Click(object sender, RoutedEventArgs e)
        {
            if(computerClub != null)
            {
                if (edit)
                {
                    TBName.Visibility = TBAddress.Visibility = TBOpeningHours.Visibility = Visibility.Hidden;
                    DeleteBT.Content = "Удалить";

                    edit = false;
                }
                else
                {
                    if (MainWindow.Instance.computerRentals.Exists(x => x.ClubID == computerClub.ClubID))
                        if (MessageBox.Show("В этом клубе есть аренды.\nЕсли удалить этот клуб то все его записи также удаляться.\nПродолжить удаление?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                            return;

                    foreach (var rent in MainWindow.Instance.computerRentals.FindAll(x => x.ClubID == computerClub.ClubID))
                        rent.Delete();

                    computerClub.Delete();
                    MainWindow.Instance.Admin_Click(null, null);
                }
            }
            else
                TBName.Text = TBAddress.Text = TBOpeningHours.Text = "";
        }
    }
}

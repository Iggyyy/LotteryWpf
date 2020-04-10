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
using PersonLib;

namespace SQLite_learning
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<PersonModel> people = new List<PersonModel>();
        public MainWindow()
        {
            InitializeComponent();
            LoadPeopleList();
        }

        public void LoadPeopleList()
        {
            people = SqliteDataAccess.LoadPeople();

            WireUpPeopleList();
        }
        

        public void WireUpPeopleList()
        {
            listPeopleListBox.ItemsSource = null;
            listPeopleListBox.ItemsSource = people;
            listPeopleListBox.DisplayMemberPath = "FullName";
        }


        private void addPersonButton_Click(object sender, RoutedEventArgs e)
        {
            PersonModel p = new PersonModel();

            p.FirstName = firstNameText.Text;
            p.LastName = lastNameTexr.Text;

            if (p.FirstName.Length > 1 && p.LastName.Length > 1)
            {
                SqliteDataAccess.SavePerson(p);
            }
            
            firstNameText.Text = "";
            lastNameTexr.Text = "";

        }

        private void refreshListButton_Click(object sender, RoutedEventArgs e)
        {

            LoadPeopleList();
        }

        private void drawWinnerButton_Click(object sender, RoutedEventArgs e)
        {
            int winnerIndex = new Random().Next(0, people.Count);
         
            winnerTextBlock.Text = $"Winner is:\n{people[winnerIndex].FullName}";
           
        }

        private void removeSelectedButton_Click(object sender, RoutedEventArgs e)
        {
            PersonModel selected = (PersonModel)listPeopleListBox.SelectedItem;
            
            if(selected != null)
                SqliteDataAccess.RemovePerson(selected);
        }
    }
}

using MyMVVMParser.Models;
using MyMVVMParser.VievModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace MyMVVMParser
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new ApplicationViewModel();
        }
    }
}
//    public partial class MainWindow : Window
//    {
//        VacancyContext db;
//        public MainWindow()
//        {
//            InitializeComponent();
//            db = new VacancyContext();
//            db.Vacancies.Load();
//            this.DataContext = db.Vacancies.Local.ToBindingList();
//        }
//        // добавление
//        private void Add_Click(object sender, RoutedEventArgs e)
//        {
//            VacancyWindow vacancyWindow = new VacancyWindow(new Vacancy());
//            if (vacancyWindow.ShowDialog() == true)
//            {
//                Vacancy vacancy = vacancyWindow.Vacancy;
//                db.Vacancies.Add(vacancy);
//                db.SaveChanges();
//            }
//        }
//        // редактирование
//        private void Edit_Click(object sender, RoutedEventArgs e)
//        {
//            // если ни одного объекта не выделено, выходим
//            if (vacancyList.SelectedItem == null) return;
//            // получаем выделенный объект
//            Vacancy vacancy = vacancyList.SelectedItem as Vacancy;

//            VacancyWindow vacancyWindow = new VacancyWindow(new Vacancy
//            {
//                Id = vacancy.Id,
//                Name = vacancy.Name,
//                Salary = vacancy.Salary,
//                Company = vacancy.Company,
//                Contact = vacancy.Contact,
//                Number = vacancy.Number,
//                Type = vacancy.Type,
//                About = vacancy.About
//            });

//            if (vacancyWindow.ShowDialog() == true)
//            {
//                // получаем измененный объект
//                vacancy = db.Vacancies.Find(vacancyWindow.Vacancy.Id);
//                if (vacancy != null)
//                {
//                    vacancy.Name = vacancyWindow.Vacancy.Name;
//                    vacancy.Company =vacancyWindow.Vacancy.Company;
//                    vacancy.Salary = vacancyWindow.Vacancy.Salary;
//                    vacancy.Contact = vacancyWindow.Vacancy.Contact;
//                    vacancy.Number = vacancyWindow.Vacancy.Number;
//                    vacancy.Type = vacancyWindow.Vacancy.Type;
//                    vacancy.About = vacancyWindow.Vacancy.About;
//                    db.Entry(vacancy).State = EntityState.Modified;
//                    db.SaveChanges();
//                }
//            }
//        }
//        // удаление
//        private void Delete_Click(object sender, RoutedEventArgs e)
//        {
//            // если ни одного объекта не выделено, выходим
//            if (vacancyList.SelectedItem == null) return;
//            // получаем выделенный объект
//            Vacancy vacancy = vacancyList.SelectedItem as Vacancy;
//            db.Vacancies.Remove(vacancy);
//            db.SaveChanges();
//        }
//    }
//}



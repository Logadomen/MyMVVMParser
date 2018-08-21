using MyMVVMParser.Models;
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
using System.Windows.Shapes;

namespace MyMVVMParser
{
    /// <summary>
    /// Логика взаимодействия для VacancyWindowxaml.xaml
    /// </summary>
    public partial class VacancyWindow : Window
    {
        public Vacancy Vacancy { get; private set; }

        public VacancyWindow(Vacancy p)
        {
            InitializeComponent();
            Vacancy = p;
            this.DataContext = Vacancy;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}

using HtmlAgilityPack;
using MyMVVMParser.Comands;
using MyMVVMParser.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyMVVMParser.VievModel
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        VacancyContext db;
        RelayCommand addCommand;
        RelayCommand editCommand;
        RelayCommand deleteCommand;
        IEnumerable<Vacancy> vacancies;

        public IEnumerable<Vacancy> Vacancies
        {
            get { return vacancies; }
            set
            {
                vacancies = value;
                OnPropertyChanged("vacancys");
            }
        }

        public ApplicationViewModel()
        {
            db = new VacancyContext();
            db.Vacancies.Load();
            Vacancies = db.Vacancies.Local.ToBindingList();
        }
        // команда добавления
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand((o) =>
                  {
                      var pageContent= LoadPage(@"https://kaluga.hh.ru/search/vacancy?text=&area=43&salary=&currency_code=RUR&only_with_salary=true&experience=doesNotMatter&order_by=publication_time&search_period=&items_on_page=50&no_magic=true");
                      var document = new HtmlDocument();
                      document.LoadHtml(pageContent);
                      for (int i = 1; i < 51; i++)
                      {
                          Vacancy vm = new Vacancy();
                          vm.Name = document.DocumentNode.SelectSingleNode("/html/body/div[5]/div[2]/div/div/div[3]/div/div/div[2]/div[2]/div/div/div[" + i + "]/div[1]/div[1]/div[1]/a").InnerText;


                          if (document.DocumentNode.SelectSingleNode("/html/body/div[5]/div[2]/div/div/div[3]/div/div/div[2]/div[2]/div/div/div[" + i + "]/div[1]/div[1]/div[2]/a[1]") == null)
                          {
                              vm.Company = document.DocumentNode.SelectSingleNode("/html/body/div[5]/div[2]/div/div/div[3]/div/div/div[2]/div[2]/div/div/div[" + i + "]/div[1]/div[1]/div[3]/a[1]").InnerText;
                          }
                          else
                          {
                              vm.Company = document.DocumentNode.SelectSingleNode("/html/body/div[5]/div[2]/div/div/div[3]/div/div/div[2]/div[2]/div/div/div[" + i + "]/div[1]/div[1]/div[2]/a[1]").InnerText;
                          }

                          vm.Salary = document.DocumentNode.SelectSingleNode("/html/body/div[5]/div[2]/div/div/div[3]/div/div/div[2]/div[2]/div/div/div[" + i + "]/div[1]/div[2]/div").InnerText;

                          vm.About = document.DocumentNode.SelectSingleNode("/html/body/div[5]/div[2]/div/div/div[3]/div/div/div[2]/div[2]/div/div/div[" + i + "]/div[2]/div[1]/div[1]").InnerText;

                          db.Vacancies.Add(vm);
                          db.SaveChanges();
                      }
                  }));
            }
        }
        // команда редактирования
        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                  (editCommand = new RelayCommand((selectedItem) =>
                  {
                      if (selectedItem == null) return;
                      // получаем выделенный объект
                      Vacancy vacancy = selectedItem as Vacancy;

                      Vacancy vm = new Vacancy()
                      {
                          Id = vacancy.Id,
                          Name = vacancy.Name,
                          Company = vacancy.Company,
                          Salary = vacancy.Salary,
                          About = vacancy.About
                      };
                      VacancyWindow vacancyWindow = new VacancyWindow(vm);


                      if (vacancyWindow.ShowDialog() == true)
                      {
                          // получаем измененный объект
                          vacancy = db.Vacancies.Find(vacancyWindow.Vacancy.Id);
                          if (vacancy != null)
                          {
                              vacancy.Name = vacancyWindow.Vacancy.Name;
                              vacancy.Company = vacancyWindow.Vacancy.Company;
                              vacancy.Salary = vacancyWindow.Vacancy.Salary;
                              vacancy.About = vacancyWindow.Vacancy.About;
                              db.Entry(vacancy).State = EntityState.Modified;
                              db.SaveChanges();
                          }
                      }
                  }));
            }
        }
        // команда удаления
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new RelayCommand((selectedItem) =>
                  {
                      if (selectedItem == null) return;
                      // получаем выделенный объект
                      Vacancy vacancy = selectedItem as Vacancy;
                      db.Vacancies.Remove(vacancy);
                      db.SaveChanges();
                  }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public static string LoadPage(string url)
        {
            var result = "";
            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var receiveStream = response.GetResponseStream();
                if (receiveStream != null)
                {
                    StreamReader readStream;
                    if (response.CharacterSet == null)
                        readStream = new StreamReader(receiveStream);
                    else
                        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                    result = readStream.ReadToEnd();
                    readStream.Close();
                }
                response.Close();
            }
            return result;
        }
    }
}

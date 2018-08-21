using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMVVMParser.Models
{
    class VacancyContext : DbContext
    {
        public VacancyContext() : base("DefaultConnection")
        {
        }
        public DbSet<Vacancy> Vacancies { get; set; }
    }
}


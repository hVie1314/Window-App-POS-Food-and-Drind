using POS.Models;
using POS.Services.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.ViewModels
{
    internal class LoginViewModel
    {
        public List<Employee> EmployeeDataForLogin { get; set; }
        public 
        private PostgresEmployeeDao DAO;
        public LoginViewModel()
        {
            DAO = new PostgresEmployeeDao();
        }
    }
}

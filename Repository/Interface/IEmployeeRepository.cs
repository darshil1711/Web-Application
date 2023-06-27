using CommonModel.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
   public  interface IEmployeeRepository
    {
        List<EmployeeDetails> AddEmployee(EmployeeDetails employeeDetail);
        IList<EmployeeDetails> GetAlldata();
        EmployeeDetails GetEmployeeById(int id);
        IList<EmployeeDesignation> GetDesignation();
        void UpdateEmployee(EmployeeDetails employee);
        void DeleteEmployee(int id);
    }
}

using CommonModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IEmployeeServices
    {

        void AddEmployee(EmployeeDetailsViewModel employee);
        IList<EmployeeDesignationViewModel> GetDesignationData();
        IList<EmployeeDetailsViewModel> GetAllData();
        EmployeeDetailsViewModel GetEmployeeDetails(int id);
        void UpdateEmployee(EmployeeDetailsViewModel employee);
        void DeleteEmployee(int id);
    }

}

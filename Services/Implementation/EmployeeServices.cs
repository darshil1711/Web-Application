using CommonModel.DBModel;
using CommonModel.ViewModel;
using Repository.Interface;
using Services.Interface;
using System;
using System.Collections.Generic;

namespace Services.Implementation
{
    public class EmployeeServices : IEmployeeServices
    {
        public readonly IEmployeeRepository _employeeRepository;

        public EmployeeServices(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public void AddEmployee(EmployeeDetailsViewModel employee)
        {
            try
            {
                var employeeDetails = new EmployeeDetails()

                {
                    Name = employee.Name,
                    DesignationId = employee.DesignationId,
                    ProfilePicture = employee.ProfilePicture,
                    Salary = employee.Salary,
                    DateofBirth = employee.DateofBirth,
                    Email = employee.Email,
                    Address = employee.Address,
                };

                _employeeRepository.AddEmployee(employeeDetails);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IList<EmployeeDesignationViewModel> GetDesignationData()
        {
            try
            {

                var employeedesignation = _employeeRepository.GetDesignation();
                List<EmployeeDesignationViewModel> employeedata = new List<EmployeeDesignationViewModel>();

                foreach (var Item in employeedesignation)
                {
                    employeedata.Add(new EmployeeDesignationViewModel()
                    {
                        ID = Item.ID,
                        Designation = Item.Designation,

                    });
                }
                return employeedata;
            }


            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IList<EmployeeDetailsViewModel> GetAllData()
        {
            try
            {
                var employeeList = _employeeRepository.GetAlldata();
                List<EmployeeDetailsViewModel> employeedata = new List<EmployeeDetailsViewModel>();

                foreach (var Item in employeeList)
                {
                    employeedata.Add(new EmployeeDetailsViewModel()
                    {
                        ID = Item.ID,
                        Name = Item.Name,
                        DesignationId = Item.DesignationId,
                        Designation = Item.Designation,
                        ProfilePicture = Item.ProfilePicture,
                        Salary = Item.Salary,
                        DateofBirth = Item.DateofBirth,
                        Email = Item.Email,
                        Address = Item.Address
                    });
                }
                return employeedata;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EmployeeDetailsViewModel GetEmployeeDetails(int id)
        {
            // Retrieve the existing employee details from the database
            var existingEmployee = _employeeRepository.GetEmployeeById(id);

            if (existingEmployee != null)
            {
                EmployeeDetailsViewModel employeedata = new EmployeeDetailsViewModel();

                // Update the necessary properties of the existing employee with the values from the provided employee object
                employeedata.Name = existingEmployee.Name;
                employeedata.ProfilePicture = existingEmployee.ProfilePicture;
                employeedata.DesignationId = existingEmployee.DesignationId;
                employeedata.Salary = existingEmployee.Salary;
                employeedata.DateofBirth = existingEmployee.DateofBirth;
                employeedata.Email = existingEmployee.Email;
                employeedata.Address = existingEmployee.Address;

                // Save the updated employee details back to the database
                return employeedata;
            }
            else
            {
                throw new Exception("Employee not found!"); // or handle the situation when the employee is not found in the database
            }
        }

        public void UpdateEmployee(EmployeeDetailsViewModel employee)
        {

            try
            {
                EmployeeDetails employeedata = new EmployeeDetails();
                {
                    employeedata.ID = employee.ID;
                    employeedata.Name = employee.Name;
                    employeedata.DesignationId = employee.DesignationId;
                    employeedata.ProfilePicture = employee.ProfilePicture;
                    employeedata.Salary = employee.Salary;
                    employeedata.DateofBirth = employee.DateofBirth;
                    employeedata.Email = employee.Email;
                    employeedata.Address = employee.Address;

                };

                _employeeRepository.UpdateEmployee(employeedata);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteEmployee(int id)
        {
            try
            {
                _employeeRepository.DeleteEmployee(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}


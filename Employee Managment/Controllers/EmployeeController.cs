using CommonModel.ViewModel;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace Employee_Managment.Controllers
{
    public class EmployeeController : Controller
    {
        public readonly IEmployeeServices _employeeServices;

        public EmployeeController(IEmployeeServices employeeServices)
        {
            _employeeServices = employeeServices;
        }

        // GET: Employee
        public ActionResult Index()
        {
            IList<EmployeeDetailsViewModel> employeedatalist = _employeeServices.GetAllData();
            return View(employeedatalist);
        }

        public ActionResult Create()
        {
            ViewBag.Designation = new SelectList(Designation(), "ID", "Designation");
            return View();
        }

        [HttpPost]
        public ActionResult AddEmployee(EmployeeDetailsViewModel employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (employee.File != null && employee.File.ContentLength > 0)
                    {
                        string filename = Path.GetFileNameWithoutExtension(employee.File.FileName) + DateTime.Now.ToString("ddMMyyyyhhmmssfff") + Path.GetExtension(employee.File.FileName);
                        string path = Server.MapPath("~/Images/");
                        string fullPath = Path.Combine(path, filename);
                        employee.File.SaveAs(fullPath);
                        employee.ProfilePicture = filename;

                    }

                    _employeeServices.AddEmployee(employee);

                    TempData["SuccessMessage"] = "Employee created successfully.";
                    string message = "Created the record successfully";
                    ViewBag.Message = message;
                    return RedirectToAction("Index");
                }

                ViewBag.Designation = new SelectList(Designation(), "ID", "Designation");
                return View(employee);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Failed to add employee. Error: " + ex.Message;
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            EmployeeDetailsViewModel emp = _employeeServices.GetEmployeeDetails(id);
            ViewBag.Designation = new SelectList(Designation(), "ID", "Designation", emp.ID);
            return View(emp);
        }

        [HttpPost]


        public ActionResult EditEmpDetails(EmployeeDetailsViewModel employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (employee.File != null && employee.File.ContentLength > 0)
                    {
                        string filename = Path.GetFileNameWithoutExtension(employee.File.FileName) + DateTime.Now.ToString("ddMMyyyyhhmmssfff") + Path.GetExtension(employee.File.FileName);
                        string path = Server.MapPath("~/Images/");
                        string fullPath = Path.Combine(path, filename);
                        employee.File.SaveAs(fullPath);
                        employee.ProfilePicture = filename;
                    }
                    else
                    {
                        // If no new profile picture is uploaded, retain the existing one
                        EmployeeDetailsViewModel existingEmployee = _employeeServices.GetEmployeeDetails(employee.ID);
                        employee.ProfilePicture = existingEmployee.ProfilePicture;
                    }

                    _employeeServices.UpdateEmployee(employee);
                    return RedirectToAction("Index");
                }

                ViewBag.Designation = new SelectList(Designation(), "ID", "Designation");
                return View(employee);
            }
            catch
            {
                ViewBag.Designation = new SelectList(Designation(), "ID", "Designation");
                return View();
            }
        }


        public ActionResult Delete(int id)
        {
            try
            {
                _employeeServices.DeleteEmployee(id);
                TempData["Success"] = "Employee Deleted Successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Failed to delete employee. Error: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IList<EmployeeDesignationViewModel> Designation()
        {
            return _employeeServices.GetDesignationData();
        }
    }
}

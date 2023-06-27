using CommonModel.DBModel;
using Dapper;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private SqlConnection con;

        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["EmployeesConnectionString"].ConnectionString;
            con = new SqlConnection(constr);
        }

        public List<EmployeeDetails> AddEmployee(EmployeeDetails employeeDetail)
        {
            try
            {
                var parameter = new DynamicParameters();
                using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeesConnectionString"].ConnectionString))
                {
                    connection.Open();
                    parameter.Add("@Name", employeeDetail.Name);
                    parameter.Add("@DesignationId", employeeDetail.DesignationId);
                    parameter.Add("@ProfilePicture", employeeDetail.ProfilePicture);
                    parameter.Add("@Salary", employeeDetail.Salary);
                    parameter.Add("@DateofBirth", employeeDetail.DateofBirth);
                    parameter.Add("@Email", employeeDetail.Email);
                    parameter.Add("@Address", employeeDetail.Address);
                    return connection.Query<EmployeeDetails>("EmployeeDetailsAddorEdit", parameter, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                throw;
            }
        }

        public IList<EmployeeDetails> GetAlldata()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeesConnectionString"].ConnectionString))
            {
                connection.Open();
                return connection.Query<EmployeeDetails>("EmployeeDetailsViewAll").ToList();
            }
        }

        public EmployeeDetails GetEmployeeById(int id)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeesConnectionString"].ConnectionString))
            {
                connection.Open();
                string query = "SELECT * FROM EmployeeDetails WHERE Id = @Id";
                return connection.QueryFirstOrDefault<EmployeeDetails>(query, new { Id = id });
            }
        }

        public IList<EmployeeDesignation> GetDesignation()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeesConnectionString"].ConnectionString))
            {
                connection.Open();
                return connection.Query<EmployeeDesignation>("GetEmployeeDesignations").ToList();
            }
        }

        public void UpdateEmployee(EmployeeDetails employee)
        {
            try
            {
                var parameter = new DynamicParameters();
                using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeesConnectionString"].ConnectionString))
                {
                    connection.Open();
                    parameter.Add("@ID", employee.ID);
                    parameter.Add("@Name", employee.Name);
                    parameter.Add("@DesignationId", employee.DesignationId);
                    parameter.Add("@ProfilePicture", employee.ProfilePicture);
                    parameter.Add("@Salary", employee.Salary);
                    parameter.Add("@DateofBirth", employee.DateofBirth);
                    parameter.Add("@Email", employee.Email);
                    parameter.Add("@Address", employee.Address);
                    connection.Query<EmployeeDetails>("EmployeeDetailsUpdate", parameter, commandType: CommandType.StoredProcedure);
                }
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
                using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeesConnectionString"].ConnectionString))
                {
                    connection.Open();
                    var parameter = new DynamicParameters();
                    parameter.Add("@ID", id);
                    connection.Execute("EmployeeDetailsDeletebyID", parameter, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                throw;
            }
        }
        public EmployeeDetails GetEmployeeDetails(int id)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeesConnectionString"].ConnectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM EmployeeDetails WHERE Id = @Id";
                    return connection.QueryFirstOrDefault<EmployeeDetails>(query, new { Id = id });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}




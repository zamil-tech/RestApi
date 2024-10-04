using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using RestApi.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public readonly IConfiguration _Configuration;

        public EmployeeController(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        // GET: api/Employee/GetAllEmployees
        [HttpGet]
        [Route("GetAllEmployees")]
        public IActionResult GetEmployees()
        {
            List<EmployeeModel> employees = new List<EmployeeModel>();

            try
            {
                // Get connection string from appsettings.json
                string connectionString = _Configuration.GetConnectionString("EmployeeDb");

                // Create SQL connection
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    // Open connection
                    con.Open();

                    // SQL command to select all employees
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Employees", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Process the rows
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            EmployeeModel employee = new EmployeeModel
                            {
                                EmployeeID = (int)dt.Rows[i]["EmployeeID"],
                                FirstName = (string)dt.Rows[i]["FirstName"],
                                LastName = (string)dt.Rows[i]["LastName"],
                                Position = (string)dt.Rows[i]["Position"],
                                Salary = (decimal)dt.Rows[i]["Salary"],
                                HireDate = (DateTime)dt.Rows[i]["HireDate"] 
                            };
                            employees.Add(employee);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error: {ex.Message}" });
            }

            // Return the list of employees as JSON
            return Ok(employees);
        }
    }
}

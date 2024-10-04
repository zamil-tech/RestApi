namespace RestApi.Models
{
    public class EmployeeModel
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Position { get; set; }
        public Decimal Salary { get; set; }
        public DateTime HireDate { get; set; }
    }
}

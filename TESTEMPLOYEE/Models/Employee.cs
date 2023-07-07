namespace TESTEMPLOYEE.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; } 

        public string SSN { get; set; }

        public DateTime HireDate { get; set; }  

        public DateTime DOB { get; set;}

        public DateTime Termination { get; set; }

        public decimal AnnualSalary { get; set; }   
    }
}

using System.Collections.Generic;
using System.Data.SqlClient;
using TESTEMPLOYEE.Models;
namespace TESTEMPLOYEE.DBA
{
    public class DataAccess
    {
        private static string _connString;
        IConfiguration _conf;

        public DataAccess(IConfiguration conf)
        {
            _conf = conf;
            GetConnectionString(conf);
        }

        public static void GetConnectionString(IConfiguration conf)
        {
            IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            _connString = conf.GetConnectionString("MasterDatabase").ToString();
        }


        public List<Employee> GetEmployee()
        {
            List<Employee> employees = new List<Employee>();

            using (SqlConnection conn = new SqlConnection(_connString))
            {
                try
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("GetEmployee", conn);

                    SqlDataReader reader =  cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        
                            Employee emp = new Employee();
                            if (reader["EmployeeType"].ToString() == "E")
                            {
                                emp.SSN = reader["SSN"].ToString();

                                emp.HireDate = Convert.ToDateTime(reader["HireDate"].ToString());

                                emp.EmployeeId = Convert.ToInt16(reader["EmployeeTypeId"].ToString());

                                employees.Add(emp);
                            }
                        }
                }
                
                catch (Exception e) { 

                    
                    conn.Close();   
                }
            }

            return employees;
        }


        public List<Contractor> GetContractor()
        {
            List<Contractor> contractors = new List<Contractor>();

            using (SqlConnection conn = new SqlConnection(_connString))
            {
                try
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("GetEmployee", conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {

                        Contractor ct = new Contractor();
                        if (reader["EmployeeType"].ToString() == "C")
                        {
                            ct.ContractorId = Convert.ToInt16(reader["EmployeeTypeId"].ToString());

                            ct.ContractDate = Convert.ToDateTime(reader["ContractDate"].ToString());

                            ct.HourRate = Convert.ToDecimal(reader["HourlyRate"].ToString());

                            ct.ContractorState = reader["ContractorState"].ToString();

                            contractors.Add(ct);
                        }
                    }
                }

                catch (Exception e)
                {


                    conn.Close();
                }
            }

            return contractors;
        }

        public bool UpdateContractor(Contractor contractor)
        {
            bool inserted = false;
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                try
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UpdateContractor", conn);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("ContractorId", contractor.ContractorId);

                    if (contractor.HourRate != 0)
                    {
                        cmd.Parameters.AddWithValue("HourRate", contractor.HourRate);
                    }

                    if (contractor.ContractorState != "" && contractor.ContractorState!=null)
                    {
                        cmd.Parameters.AddWithValue("ContractorState", contractor.ContractorState);
                    }

                    //SqlDataReader reader = cmd.ExecuteReader();


                    inserted = (cmd.ExecuteNonQuery()) > 0;
                }

                catch (Exception e)
                {


                    conn.Close();
                }

                return inserted;    
            }
        }

    }
}

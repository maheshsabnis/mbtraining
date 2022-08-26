using System;
using System.Data;
using System.Data.SqlClient;
namespace dbconnectedarchi
{
    public class DbOperation
    {
        SqlConnection Conn;
        SqlCommand Cmd;

        /// <summary>
        /// Lets Instantiate the SqlConnection Object
        /// </summary>
        public DbOperation()
        {
            Conn = new SqlConnection("Data Source=127.0.0.1;Initial Catalog=UCompany;User Id=sa;Password=MyPass@word");
        }

        public List<Department> GetDepartments()
        {
            List<Department> departments = new List<Department>();
            try
            {
                // Open the Connection
                Conn.Open();
                // 2. The Command Object Instance
                Cmd = new SqlCommand();
                // Over then connection to DB perform Tansactions
                Cmd.Connection = Conn;
                Cmd.CommandType = CommandType.Text;
                Cmd.CommandText = "Select * from Department";
                // 3. Execute the Statement
                SqlDataReader reader = Cmd.ExecuteReader();
                // 4. Read data using Reader anf put it in List
                while (reader.Read())
                {
                    departments.Add(
                          new Department() {
                              DeptNo = Convert.ToInt32(reader["DeptNo"]),
                              DeptName = reader["DeptName"].ToString(),
                              Location = reader["Location"].ToString(),
                              Capacity = Convert.ToInt32(reader["Capacity"])
                          }
                        );
                }
                // 5. CLose the Reader Done
                reader.Close();

            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error Occured while Processoing Request {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error {ex.Message}");
            }
            finally
            {
                Conn.Close();    
            }
            return departments;
        }

        public bool CreateDepartment(Department department)
        {
            bool inserted = false;
            try
            {
                Conn.Open();
                // Create a COmmand Object BAsed on Connection
                Cmd = Conn.CreateCommand();
                Cmd.CommandType = CommandType.Text;
                Cmd.CommandText = $"Insert into Department Values ({department.DeptNo}, '{department.DeptName}', '{department.Location}', {department.Capacity})";
                int result = Cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    inserted = true;
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error Occured while Processoing Request {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error {ex.Message}");
            }
            finally
            {
                Conn.Close();
            }
            return inserted;
        }

        public bool UpdateDepartment(Department department)
        {
            bool updated = false;
            try
            {
                Conn.Open();
                // Create a COmmand Object BAsed on Connection
                Cmd = Conn.CreateCommand();
                Cmd.CommandType = CommandType.Text;
                Cmd.CommandText = $"Update Department Set DeptName='{department.DeptName}', Location='{department.Location}',Capacity={department.Capacity} where DeptNo={department.DeptNo}";
                int result = Cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    updated = true;
                }

            }

            catch (SqlException ex)
            {
                Console.WriteLine($"Error Occured while Processoing Request {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error {ex.Message}");
            }
            finally
            {
                Conn.Close();
            }
            return updated;
        }


        public bool DeleteDepartment(int DeptNo)
        {
            bool deleted = false;
            try
            {
                Conn.Open();
                // Create a COmmand Object BAsed on Connection
                Cmd = Conn.CreateCommand();
                Cmd.CommandType = CommandType.Text;
                Cmd.CommandText = $"Delete From Department where DeptNo={DeptNo}";
                int result = Cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    deleted = true;
                }

            }

            catch (SqlException ex)
            {
                Console.WriteLine($"Error Occured while Processoing Request {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error {ex.Message}");
            }
            finally
            {
                Conn.Close();
            }
            return deleted;
        }


        public bool CreateDepartmentParameters(Department department)
        {
            bool inserted = false;
            try
            {
                Conn.Open();
                // Create a COmmand Object BAsed on Connection
                Cmd = Conn.CreateCommand();
                Cmd.CommandType = CommandType.Text;
                Cmd.CommandText = $"Insert into Department Values (@DeptNo,@DeptName, @Location,@Capacity)";

                // Set Prameters

                SqlParameter pDeptNo = new SqlParameter();
                pDeptNo.ParameterName = "@DeptNo";
                pDeptNo.Direction = ParameterDirection.Input;
                pDeptNo.DbType = DbType.Int32;
                pDeptNo.Value = department.DeptNo;
                // Add the Parameter into the Parameters collection
                // of the Commadn Object
                Cmd.Parameters.Add(pDeptNo);

                SqlParameter pDeptName = new SqlParameter();
                pDeptName.ParameterName = "@DeptName";
                pDeptName.Direction = ParameterDirection.Input;
                pDeptName.DbType = DbType.String;
                pDeptName.Size = 200;
                pDeptName.Value = department.DeptName;
                // Add the Parameter into the Parameters collection
                // of the Commadn Object
                Cmd.Parameters.Add(pDeptName);

                SqlParameter pLocation = new SqlParameter();
                pLocation.ParameterName = "@Location";
                pLocation.Direction = ParameterDirection.Input;
                pLocation.DbType = DbType.String;
                pLocation.Size = 100;
                pLocation.Value = department.Location;
                // Add the Parameter into the Parameters collection
                // of the Commadn Object
                Cmd.Parameters.Add(pLocation);


                SqlParameter pCapacity = new SqlParameter();
                pCapacity.ParameterName = "@Capacity";
                pCapacity.Direction = ParameterDirection.Input;
                pCapacity.DbType = DbType.Int32;
                pCapacity.Value = department.Capacity;
                // Add the Parameter into the Parameters collection
                // of the Commadn Object
                Cmd.Parameters.Add(pCapacity);


                int result = Cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    inserted = true;
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error Occured while Processoing Request {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error {ex.Message}");
            }
            finally
            {
                Conn.Close();
            }
            return inserted;
        }
    }
}


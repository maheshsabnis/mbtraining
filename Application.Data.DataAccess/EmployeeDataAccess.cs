using System;
using Application.Dal.Contract;
using Application.Entities;
namespace Application.Data.DataAccess;
using System.Data;
using System.Data.SqlClient;

public class EmployeeDataAccess : IDataAccess<Employee, int>
{
    SqlConnection Conn;
    SqlCommand Cmd;

    public EmployeeDataAccess()
    {
        Conn = new SqlConnection("Data Source=127.0.0.1;Initial Catalog=UCompany;User Id=sa;Password=MyPass@word");
    }

    Employee IDataAccess<Employee, int>.Create(Employee entity)
    {
        try
        {
            Conn.Open();
            // Create a COmmand Object BAsed on Connection
            Cmd = Conn.CreateCommand();
            Cmd.CommandType = CommandType.Text;
            Cmd.CommandText = $"Insert into Employee Values ({entity.EmpNo}, '{entity.EmpName}', '{entity.Designation}', {entity.Salary}, {entity.DeptNo})";
            int result = Cmd.ExecuteNonQuery();


        }
        catch (SqlException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            Conn.Close();
        }
        return entity;
    }

    Employee IDataAccess<Employee, int>.Delete(int id)
    {
        Employee employee = null;
        try
        {
            Conn.Open();
            // Create a COmmand Object BAsed on Connection
            Cmd = Conn.CreateCommand();
            Cmd.CommandType = CommandType.Text;
            Cmd.CommandText = $"Delete From Employee where EmpNo={id}";
            int result = Cmd.ExecuteNonQuery();

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
        return employee;
    }

    IEnumerable<Employee> IDataAccess<Employee, int>.Get()
    {
        List<Employee> employees = new List<Employee>();
        try
        {
            // Open the Connection
            Conn.Open();
            // 2. The Command Object Instance
            Cmd = new SqlCommand();
            // Over then connection to DB perform Tansactions
            Cmd.Connection = Conn;
            Cmd.CommandType = CommandType.Text;
            Cmd.CommandText = "Select * from Employee";
            // 3. Execute the Statement
            SqlDataReader reader = Cmd.ExecuteReader();
            // 4. Read data using Reader anf put it in List
            while (reader.Read())
            {
                employees.Add(
                      new Employee()
                      {
                          EmpNo = Convert.ToInt32(reader["EmpNo"]),
                          EmpName = reader["EmpName"].ToString(),
                          Designation = reader["Designation"].ToString(),
                          Salary = Convert.ToInt32(reader["Salary"]),
                          DeptNo = Convert.ToInt32(reader["DeptNo"]),
                         
                      }
                    );
            }
            // 5. CLose the Reader Done
            reader.Close();

        }
        catch (SqlException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            Conn.Close();
        }
        return employees;
    }

    Employee IDataAccess<Employee, int>.Get(int id)
    {
        Employee employee = null;
        try
        {
            Conn.Open();
            Cmd = Conn.CreateCommand();
            Cmd.CommandText = $"Select EmpNo, EmpName, Designation,Salary,DeptNo from Employee where EmpNo = {id}";
            SqlDataReader reader = Cmd.ExecuteReader();
            while (reader.Read())
            {
                new Employee()
                {
                    EmpNo = Convert.ToInt32(reader["EmpNo"]),
                    EmpName = reader["EmpName"].ToString(),
                    Designation = reader["Designation"].ToString(),
                    Salary = Convert.ToInt32(reader["Salary"]),
                    DeptNo = Convert.ToInt32(reader["DeptNo"]),

                };

            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            Conn.Close();
        }
        return employee;
    }

    Employee IDataAccess<Employee, int>.Update(int id, Employee entity)
    {
        try
        {
            Conn.Open();
            // Create a COmmand Object BAsed on Connection
            Cmd = Conn.CreateCommand();
            Cmd.CommandType = CommandType.Text;
            Cmd.CommandText = $"Update Employee Set EmpName='{entity.EmpName}', Designation='{entity.Designation}',Salary={entity.Salary}, DeptNo={entity.DeptNo} where EmpNo={entity.EmpNo}";
            int result = Cmd.ExecuteNonQuery();

        }
        catch (SqlException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            Conn.Close();
        }
        return entity;
    }
}
 

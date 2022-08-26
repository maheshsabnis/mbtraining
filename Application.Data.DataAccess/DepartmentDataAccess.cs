using Application.Entities;
using Application.Dal.Contract;
using System.Data;
using System.Data.SqlClient;
namespace Application.Data.DataAccess;

public class DepartmentDataAccess : IDataAccess<Department, int>
{
    SqlConnection Conn;
    SqlCommand Cmd;

    public DepartmentDataAccess()
    {
        Conn = new SqlConnection("Data Source=127.0.0.1;Initial Catalog=UCompany;User Id=sa;Password=MyPass@word");
    }

    Department IDataAccess<Department, int>.Create(Department entity)
    {
        try
        {
            Conn.Open();
            // Create a COmmand Object BAsed on Connection
            Cmd = Conn.CreateCommand();
            Cmd.CommandType = CommandType.Text;
            Cmd.CommandText = $"Insert into Department Values ({entity.DeptNo}, '{entity.DeptName}', '{entity.Location}', {entity.Capacity})";
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

    Department IDataAccess<Department, int>.Delete(int id)
    {
        Department department = null;
        try
        {
            Conn.Open();
            // Create a COmmand Object BAsed on Connection
            Cmd = Conn.CreateCommand();
            Cmd.CommandType = CommandType.Text;
            Cmd.CommandText = $"Delete From Department where DeptNo={id}";
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
        return department;
    }

    IEnumerable<Department> IDataAccess<Department, int>.Get()
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
                      new Department()
                      {
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
        return departments;

    }

    Department IDataAccess<Department, int>.Get(int id)
    {
        Department department = null;
        try
        {
            Conn.Open();
            Cmd = Conn.CreateCommand();
            Cmd.CommandText = $"Select DeptNo, DeptName, Location,Capacity from Department where DeptNo = {id}";
            SqlDataReader Reader = Cmd.ExecuteReader();
            while (Reader.Read())
            {
                department = new Department()
                {
                    DeptNo = Convert.ToInt32(Reader["DeptNo"]),
                    DeptName = Reader["DeptName"].ToString(),
                    Location = Reader["Location"].ToString(),
                    Capacity = Convert.ToInt32(Reader["Capacity"])
                };
            }
            Reader.Close();
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
        return department;
    }

    Department IDataAccess<Department, int>.Update(int id, Department entity)
    {
        try
        {
            Conn.Open();
            // Create a COmmand Object BAsed on Connection
            Cmd = Conn.CreateCommand();
            Cmd.CommandType = CommandType.Text;
            Cmd.CommandText = $"Update Department Set DeptName='{entity.DeptName}', Location='{entity.Location}',Capacity={entity.Capacity} where DeptNo={entity.DeptNo}";
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


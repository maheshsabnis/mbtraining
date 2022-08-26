using System.Data.SqlClient;
using Application.Entities;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Asynchronousn ADO.NET Programming");

DataAcces ds = new DataAcces();
 // Call to DataAccess is Async, the Main() method or a Caller method need not to wait for the call to complete
var departments = await ds.GetrDepartmentsAsync();

foreach (var item in departments)
{
    Console.WriteLine($"{item.DeptNo} {item.DeptName} {item.Location} {item.Capacity}");
}


Console.ReadLine();



public class DataAcces
{
    SqlConnection Conn = new SqlConnection("Data Source=127.0.0.1;Initial Catalog=UCompany;User Id=sa;Password=MyPass@word");
    SqlCommand Cmd;

    public DataAcces()
    {
        Conn.Open();

          Cmd = new SqlCommand();
        Cmd.Connection = Conn;
    }

    /// <summary>
    /// The 'async' keyword specifes that the Method will be executed Asynchronously
    /// The 'await' wil make sure that the async call is completed and data is received from it
    /// </summary>
    /// <returns></returns>

    public async Task<List<Department>> GetrDepartmentsAsync()
    {
        List<Department> departments = new List<Department>();

        Cmd.CommandText = "Select * from Department";

        SqlDataReader reader =   await Cmd.ExecuteReaderAsync();

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
        reader.Close();
        return departments;
    }

}


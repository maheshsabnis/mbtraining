using System.Data.SqlClient;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Using SP with Input and Output Parameters");

SqlConnection Conn = new SqlConnection("Data Source=127.0.0.1;Initial Catalog=UCompany;User Id=sa;Password=MyPass@word");
Conn.Open();

SqlCommand Cmd = new SqlCommand();
Cmd.Connection = Conn;
Cmd.CommandType = System.Data.CommandType.StoredProcedure;
Cmd.CommandText = "sp_GetSumSalaryByDeptNoWithOutParam";

Console.WriteLine("Enter DEptNo");
int DeptNo = Convert.ToInt32(Console.ReadLine()); 


SqlParameter pDeptNo = new SqlParameter();
pDeptNo.ParameterName = "@DeptNo";
pDeptNo.DbType = System.Data.DbType.Int32;
pDeptNo.Direction = System.Data.ParameterDirection.Input;
pDeptNo.Value = DeptNo;
Cmd.Parameters.Add(pDeptNo);

SqlParameter pResult = new SqlParameter();
pResult.ParameterName = "@Result";
pResult.DbType = System.Data.DbType.Int32;
pResult.Direction = System.Data.ParameterDirection.Output;
Cmd.Parameters.Add(pResult);

// Execute the SP
Cmd.ExecuteScalar();

Console.WriteLine($"Sum of Salary {DeptNo} is = {pResult.Value}");



Console.ReadLine();


using System;
using System.Data;
using System.Data.SqlClient;
using Application.Entities;
namespace DisconnectedArchitecture
{
    public class DataAccess
    {
        SqlConnection Conn;
        SqlDataAdapter AdDept;
        SqlDataAdapter AdEmp;
        DataSet Ds;

        public DataAccess()
        {
            Conn = new SqlConnection("Data Source=127.0.0.1;Initial Catalog=UCompany;User Id=sa;Password=MyPass@word");
            Conn.Open();

            AdDept = new SqlDataAdapter("Select * from Department",Conn);
            AdEmp = new SqlDataAdapter("Select * from Employee", Conn);
            Ds = new DataSet();
        }

        public void FillData()
        {
            // 1. The Primary Key oin DataSet
          AdDept.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            AdEmp.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            AdDept.Fill(Ds,"Department");
            AdEmp.Fill(Ds,"Employee");

            //Console.WriteLine("Xml Schema of The Department");
            //Console.WriteLine(Ds.GetXmlSchema());

            //Console.WriteLine();
            //Console.WriteLine("Data from Table in Xml Format");
            //Console.WriteLine(Ds.GetXml());

        }


        public void AddDepartment(Department department)
        {
            try
            {
                // 1. Create a new Row
                DataRow DrNew = Ds.Tables["Department"].NewRow();
                // 2. Set column Values
                DrNew["DeptNo"] = department.DeptNo;
                DrNew["DeptName"] = department.DeptName;
                DrNew["Location"] = department.Location;
                DrNew["Capacity"] = department.Capacity;
                // 3. Add this New Row in Rows COlleciton of Table in DataSet
                Ds.Tables["Department"].Rows.Add(DrNew);
                // 4.Create a Command Builder and pass the Adpater to it
                SqlCommandBuilder bldr = new SqlCommandBuilder(AdDept);
                // 5. Update Data in DataBase
                AdDept.Update(Ds, "Department");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void FindRecord(int dno)
        {
            try
            {
                // Write a Complex code to make the DeptNo as Primary Key

                //// Make the DeptNo COlumn in DataTable as Unique
                //Ds.Tables["Department"].Columns["DeptNo"].Unique = true;
                //// MAke the same co;lumn as Not Null
                //Ds.Tables["Department"].Columns["DeptNo"].AllowDBNull = false;

                //// Get Array of DataColumn for DeptNo

                //DataColumn[] dataColumns = new DataColumn[] { Ds.Tables["Department"].Columns["DeptNo"] };

                //// MAke this array as Primary key

                //Ds.Tables["Department"].PrimaryKey = dataColumns;


                // 1.Search Based in Primary Key
                DataRow DrFind = Ds.Tables["Department"].Rows.Find(dno);

                


                Console.WriteLine("Searched Record");
                Console.WriteLine($"{DrFind["DeptNo"]} {DrFind["DeptName"]} {DrFind["Location"]} {DrFind["Capacity"]}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public void UpdateRecord(Department department)
        {
           // Console.WriteLine($"Before Update {Ds.GetXml()}"); 
            // 1. Serach Record
            DataRow DrUpdate = Ds.Tables["Department"].Rows.Find(department.DeptNo);
            // 2. Update its data
            DrUpdate["DeptName"] = department.DeptName;
            DrUpdate["Location"] = department.Location;
            DrUpdate["Capacity"] = department.Capacity;

            // Console.WriteLine($"After Update {Ds.GetXml()}");
            Console.WriteLine();
            SqlCommandBuilder bldr = new SqlCommandBuilder(AdDept);
            AdDept.Update(Ds,"Department");
        }

        public void DeleteRecord(int dno)
        {
            // 1. Serach Record
            DataRow DrDelete = Ds.Tables["Department"].Rows.Find(dno);
            // 2. Delete the Record
            DrDelete.Delete();
            SqlCommandBuilder bldr = new SqlCommandBuilder(AdDept);
            AdDept.Update(Ds, "Department");
            Console.WriteLine("Deleted Successfully");
        }


        public void GetRelation()
        {
            // 1. Define DataRelation object

            DataRelation DeptEmp = new DataRelation("DeptEmp", Ds.Tables["Department"].Columns["DeptNo"], Ds.Tables["Employee"].Columns["DeptNo"]);

            // 2. Add this Relation in Database
            Ds.Relations.Add(DeptEmp);

            // 3. Lets print Employee for Each Department

            // 3.a. First Receive the Count of Records for Parent Table i.e. Department Tabe

            int ParentRowCount = Ds.Tables["Department"].Rows.Count;

            // 3.b. Lets Iterate over the Parent Table Rows and Get Child Rows 

            for (int i = 0; i < ParentRowCount; i++)
            {
                // 3.b.1. Print the Parent Row
                DataRow row = Ds.Tables["Department"].Rows[i];

                Console.WriteLine($"DeptNo {row["DeptNo"]} DeptName {row["DeptName"]}");
                // 3.b.2. REad Child Rows for the Parent using DataRelation Object
                foreach (DataRow childRow in row.GetChildRows(DeptEmp))
                {
                    // 3.b.3. Print Chidl Rows
                    Console.WriteLine($"EmpNo {childRow["EmpNo"]} EmpName {childRow["EmpName"]} Designation {childRow["Designation"]} Salary {childRow["Salary"]} DeptNo {childRow["DeptNo"]}");
                }
                Console.WriteLine();
            }

        }

    }
}


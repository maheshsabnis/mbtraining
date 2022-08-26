 
using System.ComponentModel.DataAnnotations;
namespace MVC_Code_First.Models;



public class Department  
{
    [Key]
    public int DeptNo { get; set; }
     
    public string DeptName { get; set; }
    
    public string Location { get; set; }
    
    public int Capacity { get; set; }
    // One-to-Many Relationship
    public ICollection<Employee> Employees { get; set; }
}

public class Employee 
{
    [Key]
    public int EmpNo { get; set; }
    
    public string EmpName { get; set; }
   
    public string Designation { get; set; }
    
    public int Salary { get; set; }
    
    public int DeptNo { get; set; }
    public Department Department { get; set; }  // Foreign Key Relationships
}
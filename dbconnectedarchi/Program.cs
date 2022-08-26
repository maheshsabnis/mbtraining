using dbconnectedarchi;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
DbOperation operation = new DbOperation();

var result = operation.GetDepartments();


Print(result);

Department dept = new Department()
{
     DeptNo = 201, DeptName= "Dept_201", Location="Hyderabad-Gacchiboli", Capacity=200
};

// var done = operation.CreateDepartment(dept);

// var done = operation.UpdateDepartment(dept);

// var done = operation.DeleteDepartment(201);

var done = operation.CreateDepartmentParameters(dept);

if (done)
{
    result = operation.GetDepartments();
    Print(result);
}

Console.ReadLine();



static void Print(IEnumerable<Department> departments)
{
    foreach (var item in departments)
    {
        Console.WriteLine($"{item.DeptNo} {item.DeptName} {item.Location} {item.Capacity}");
    }

}
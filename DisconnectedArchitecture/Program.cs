using DisconnectedArchitecture;
using Application.Entities;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Using ADO.NET Disonnected Archirecture");

DataAccess dataAccess = new DataAccess();

dataAccess.FillData();

Department dept = new Department()
{
     DeptNo = 80,DeptName = "Purchase(Software)", Location="Pune-East",Capacity=1000
};

// dataAccess.AddDepartment(dept);

// dataAccess.FindRecord(10);


// dataAccess.UpdateRecord(dept);

// dataAccess.DeleteRecord(dept.DeptNo);

dataAccess.GetRelation();

Console.ReadLine();


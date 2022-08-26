# Date: 17-Aug-2022

```` sql

Create Database UCompany;

Create Table Department (
   DeptNo int Primary Key,
   DeptName  varchar(200) Not Null,
   Location varchar(100) Not Null,
   Capacity int Not Null 
);

 
Insert into Department Values (10, 'IT', 'Pune', 8000);
Insert into Department Values (20, 'HRD', 'Pune', 100);
Insert into Department Values (30, 'Accts', 'Pune', 500);
Insert into Department Values (40, 'Trag', 'Pune', 20);
Insert into Department Values (50, 'Support', 'Pune', 900);
Insert into Department Values (60, 'Admin', 'Pune', 100);
 
Create Table Employee(
  EmpNo int Primary Key,
  EmpName varchar(400) Not Null,
  Designation varchar(100) Not Null,
  Salary int Not Null,
  DeptNo int Not null References Department(DeptNo) -- Syntax for Foreign Key
);


insert into Employee Values (101, 'Mahesh', 'Director', 900000, 10);
insert into Employee Values (102, 'Tejas', 'Director', 900000, 20);
insert into Employee Values (103, 'Ramesh', 'Director', 900000, 30);
insert into Employee Values (104, 'Sharad', 'Director', 900000, 40);
insert into Employee Values (105, 'Sanjay', 'Director', 900000, 50);
insert into Employee Values (106, 'Vijay', 'Manager', 900000, 10);
insert into Employee Values (107, 'Vilas', 'Manager', 900000, 20);
insert into Employee Values (108, 'Abhay', 'Manager', 900000, 30);
insert into Employee Values (109, 'Vivek', 'Manager', 900000, 40);
insert into Employee Values (110, 'Satish', 'Manager', 900000, 50);
insert into Employee Values (111, 'Mukesh', 'Lead', 900000, 10);
insert into Employee Values (112, 'Sandeep', 'Lead', 900000, 20);
insert into Employee Values (113, 'Vinay', 'Lead', 900000, 30);
insert into Employee Values (114, 'Kaustubh', 'Lead', 900000, 40);
insert into Employee Values (115, 'Tushar', 'Lead', 900000, 50);
insert into Employee Values (116, 'Krishna', 'Associate', 900000, 10);
insert into Employee Values (117, 'Arav', 'Associate', 900000, 20);
insert into Employee Values (118, 'Nainesh', 'Associate', 900000, 30);
insert into Employee Values (119, 'Aditya', 'Associate', 900000, 40);
insert into Employee Values (120, 'Sujay', 'Associate', 900000, 50);


Select * from Department;

````


1. Using ADO.NET Approach perform Following
    - Create an Interface as follows
        - IDeptDataAccess
            - bool Create(Department)
            - IEnumetable <Department>Get()
            - Department Get(int deptno)
            - bool Update(Department)
            - bool Delete(int DeptNo)
    - Create a DepartmentDbAccess class that implements IDeptDataAccess interface and perform CRUD Operations
    - Repeat the same for Employee (Interface and Class)
2. Create a Class Named 'Repoprts' that will have following methods
        - ListEmployees(string DeptName, string Designation)
            - If DeptName and Designation are blank or null then return all Employess
            - If DeptName is not empty or null but Designation is Empty or null the returns Employees by DeptName
            - If Desaignation is not empty or null but DeptName is Empty or null the returns Employees by Designation
            - If DeptName and Designation both has values then return Employees for that DeptName and For the Designation (AND Condition)
        - SearchEmployee(column,criteria)
            - If ColumnName == "Employee" and Criteria == 'A'
                - Search Employee By EmpName starts with the Critera value
            - If ColumnName == "Designation" and Criteria == 'A'
                - Search Employee By Designation starts with the Critera value
            - If ColumnName == "DeptName" and Criteria == 'A'
                - Search Employee By DeptName starts with the Critera value

# Date:18-Aug-2022
0. Review a Solution and if any questions then ask
1. CReate a DataAccess class for EMployees
2. Register Empoloyee Data Access in DI for ASp.NET COre Project
3. Create Repository for Employee
4. Register Employee Repository in DI
5. Create EmployeeController with Action Methods for CRUD Operations
6. Create Views for CRUD for EMployee
7. Make sure that the Link for EMployee to be present on Landing Page from _Layout.cshtml

# Date: 22-Aug-2022
1. Create a Controller that will have an Index  action method which will return a List of Departments and List of EMployees to a View i.e. Index.chstml
2. Show Departments and Employees on the Index.cshtml in a Table. Each Row of teh table which is showing List of Deparments must have a Link with text as 'ShowEmployees'. When this link is clicked, the Table shown List of Employees should show Only employees from the selected Department

3. Create a Custom Validator class that will check of the DeptNo is Already present in Department Table, if present then after clicking the Save button on Create.cshtml view, the error message MUST be shown

4. Modify Index.cshtml for Idnex() action Method of Employee Controller to Show DeptName in Employees table instead of showing the DeptNo

5. Modify the Create.chtml to show DeptName as RadioButton instaed of shown the HTML select element with Department Names in it

6. IMP: Validate the Logic of new Employee creation in such a way that, if the DeptName selected for Creating new EMployee is already full with its capacity the employee showuld not be added, instaed an Error Message saying "The Capaity for Selected DeptName is already full" must be displayed on the Create.cshtml view  


# Date: 23-Aug-2022

1. After SHowing Error Message on the Error View, when we navigate back to the Create/Edit pages for COntrollers, these pages MUST show the data that has caused errors
    - HINT: Session State
2. Modify the LogFilter for Logging the Execution Information for all controllers and their Action Methods in the Database
        - Create a LogData Table Table in Database as
            - LogId int Identity Primary Key
            - LogDate DateTime Not Null
            - ControllerName varchar(100) Not Null,
            - ActionName varchar(100) Not Null
        - Create an Entity Layer for  LogData Table
        - Create a Data Access Layer
        - Register the Dal DI Container of MVC App
        - Inject the DataAceess Layer in LogFilterAttribute class     

# Date:24-Aug-2022

1. Modify the AppExceptionFilterAttribute to Log Exception Messages in Exception Table in Database (same process like Storing Log in Database like assignment on Date:23-Aug)

# Date:25-Aug-2022

1. CReate a ASP.NET Core MVC App for following Needs
    - The Application will be used to perform the Identity for USers with following Rules
        - a. When the Appliaiton is Executed and loaded the Administrator Role USer will be created with User NAme and PAssword
                - e.g. User Name: admin@myapp.com Password: P@ssw0rd_
                - This will be an Administrator Role User
                - refer the following link
                    - https://www.webnethelper.com/2022/03/aspnet-core-6-using-role-based-security.html      
        - b. New Roles Can be Created only by Adminsitartor Role
        - c. New User can be directly Created using REgister View
        - d. The User will be able to Log-In only when the Administrator assign role to the user
    - Roles will be
        - Administrator
        - Manager
        - Customer
    - Customer Can Search Products and Can Place Order for the Product
    - Manager Can View The Order and Can Approve it
    - The Order can be made ready for disptach  only by the Administartor

    - Tables
        - Customer
            - CustomerId, NAme, Email, Mobile No, Address
        - Product
            - ProductId, ProductName, CategoryNAme, Unit Price
        - Order
            - OrderId, ProductId, CustomerId, Quantity, Total Price, Order Date, IsOrderApproved, IsOrderDispatched
            - Customer Place Order
            - Manager Can Approve
            - Administrator Can Make it Ready for Dispatch

# Date: 26-Aug-2022
1. Modify the ASp.NET Core MVC app by using Data Access Layer implemeneted using the Disconnected Architecture (Submit on 02-Sept-2022)            
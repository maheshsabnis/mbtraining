using System;
namespace mbaspnetcore6.Repositories
{
    public  class DepartmentValidator
    {

        public DepartmentValidator()
        {

        }

        public static List<string> ValidationMessages(Department dept)
        {
            List<string> errorMessages = new List<string>();

            if (dept.DeptNo <= 0) errorMessages.Add("DeptNo cannot be 0 or Negative");
            if (String.IsNullOrEmpty(dept.DeptName)) errorMessages.Add("DeptName cannot be null or Empty");


            if (dept.Capacity <= 0) errorMessages.Add("Capacity cannot be 0 or Negative");
            if (string.IsNullOrEmpty(dept.DeptName)) errorMessages.Add("Location cannot be null or Empty");

            return errorMessages;
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace mbaspnetcore6.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IServiceRepository<Department, int> deptRepo;
        private readonly IServiceRepository<Employee, int> empRepo;

        public EmployeeController(IServiceRepository<Employee, int> empRepo, IServiceRepository<Department, int> deptRepo)
        {
            this.empRepo = empRepo;
            this.deptRepo = deptRepo;
        }

        public IActionResult List()
        {
            ResponseStatus<Employee> response = new ResponseStatus<Employee>();
            response = empRepo.GetRecords();
            return View(response.Records);
        }


        public IActionResult Index()
        {
            ResponseStatus<Employee> response = new ResponseStatus<Employee>();
            // 1. Read data from TempData["DeptNo"]
            // var deptno = Convert.ToInt32(TempData["DeptNo"]);
            var deptno = HttpContext.Session.GetInt32("DeptNo");
            // Retrieving data from Session State
            var dept = HttpContext.Session.GetObject<Department>("Dept");
            if (deptno > 0)
            {
                // Read only those Employees from deptno
                var employees = empRepo.GetRecords().Records.Where(e=>e.DeptNo == deptno);
                response.Records = employees;

                // Keep Data in TempData
               // TempData.Keep();

                return View(response.Records);
            }
            else
            {
                // Sho all Employees
                response = empRepo.GetRecords();
                return View(response.Records);
            }

            
        }

        public IActionResult Create()
        {
             

            var emp = new Employee();

            // Pass a ViewBag with List of Deparetment Object

            List<SelectListItem> deptData = new List<SelectListItem>();
            // Get the Department Data
            foreach (var item in deptRepo.GetRecords().Records)
            {
                // PAss the Data To Display and Data To Select to SelectLoistItem
                deptData.Add(new SelectListItem(item.DeptName, item.DeptNo.ToString()));
            }
             // Pass this data to View
            ViewBag.depts = deptData;

            // return an EMpty View for Department 
            return View(emp);
        }

        [HttpPost]
        public IActionResult Create(Employee emp)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var response = empRepo.CreateRecord(emp);
                    // If the Add is successful then Redirect to the 'Idnex' Action Method
                    return RedirectToAction("Index");
                }
                else
                {
                    // Pass a ViewBag with List of Deparetment Object

                    List<SelectListItem> deptData = new List<SelectListItem>();
                    // Get the Department Data
                    foreach (var item in deptRepo.GetRecords().Records)
                    {
                        // PAss the Data To Display and Data To Select to SelectLoistItem
                        deptData.Add(new SelectListItem(item.DeptName, item.DeptNo.ToString()));
                    }
                    // Pass this data to View
                    ViewBag.depts = deptData;
                    return View(emp);
                }
            }
            catch (Exception ex)
            {
                return View(emp);
            }
        }

        public IActionResult Edit(int id)
        {
            var respose = empRepo.GetRecord(id);
            // returna View with the data toi be edited
            return View(respose.Record);
        }

        [HttpPost]
        public IActionResult Edit(int id, Employee emp)
        {
            try
            {
                var response = empRepo.UpdateRecord(id, emp);
                // If the Add is successful then Redirect to the 'Idnex' Action Method
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(emp);
            }
        }

        public IActionResult Delete(int id)
        {
            var respose = empRepo.DeleteRecord(id);
            // returna View with the data toi be edited
            return RedirectToAction("Index");
        }
    }
}
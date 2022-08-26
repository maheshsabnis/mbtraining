using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace mbaspnetcore6.Controllers
{

    /// <summary>
    /// Inject the Repository using Constructor Injection
    /// </summary>
    /// Apply the Action Filter
    ///
  //  [Authorize]
    [LogFilter]
    public class DepartmentController : Controller
    {
        private readonly IServiceRepository<Department, int> deptRepo;

       
        /// <summary>
        /// Injection
        /// </summary>
        /// <param name="repo"></param>
        public DepartmentController(IServiceRepository<Department, int> repo)
        {
            deptRepo = repo;
        }
        /// <summary>
        /// Action Method
        /// </summary>
        /// <returns></returns>
        /// Configuring the Security by using the Roles
        // [Authorize(Roles = "Admin,Manager,Clerk")]
        [Authorize(Policy = "ReadPolicy")]
        public IActionResult Index()
        {
            try
            {

                var response = deptRepo.GetRecords();
                return View(response.Records);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                // Return an Error Page

                // Hard-Coded Values for Controller and Action Method
                // These MUST be eliminated
                return View("Error", new ErrorViewModel()
                {
                     ErrorMessage = ex.Message,
                     ControllerName = "Department",
                     ActionName = "Index"
                });
            }
        }

        /// <summary>
        /// Method to Create a new Department
        /// </summary>
        /// <returns></returns>
        ///
        // [Authorize(Roles = "Admin,Manager")]
        [Authorize(Policy = "CreatePolicy")]
        public IActionResult Create()
        {
            var dept = new Department();
            // return an EMpty View for Department 
            return View(dept);
        }

        /// <summary>
        /// The Post method that will be used mapp the Http Post Request
        /// to the Current Action Method and Read data from Http Request Message Body
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(Department dept)
        {
            //try
            //{
                if (ModelState.IsValid)
                {
                    if (dept.Capacity < 0)
                        throw new Exception("Capacity cannot be -ve");
                    var response = deptRepo.CreateRecord(dept);

                    
                    // If the Add is successful then Redirect to the 'Idnex' Action Method
                    return RedirectToAction("Index");
                }
                else
                {
                    // Stey on the Same Page
                    return View(dept);
                }
            //}
            //catch (Exception ex)
            //{
            //    // Retrning Error Page by Eliminating
            //    // Hard-Coding
            //    // the 'controller' and 'action' are comming from Route Expression, refer Program.cs
            //    return View("Error", new ErrorViewModel()
            //    {
            //        ErrorMessage = ex.Message,
            //        ControllerName = this.RouteData.Values["controller"].ToString(),
            //        ActionName = this.RouteData.Values["action"].ToString()
            //    });
            //}
        }
        //  [Authorize(Roles = "Admin")]
        [Authorize(Policy = "EditDeletePolicy")]
        public IActionResult Edit(int id)
        {
            var respose = deptRepo.GetRecord(id);
            ViewBag.Message = "The Edit View";
            //ViewBag.ErrorMessages = new List<string>();
            // returna View with the data toi be edited
            return View(respose.Record);
        }

        [HttpPost]
        public IActionResult Edit(int id, Department dept)
        {
            try
            {
                var errorMessage = DepartmentValidator.ValidationMessages(dept);
                if (errorMessage.Count() > 0)
                {
                    // passing additional data to View
                    ViewBag.ErrorMessages = errorMessage;
                    return View(dept);
                }
                else
                {
                    var response = deptRepo.UpdateRecord(id, dept);
                    // If the Add is successful then Redirect to the 'Idnex' Action Method
                    return RedirectToAction("Index");
                }

                
            }
            catch (Exception ex)
            {
                return View(dept);
            }
        }
        //[Authorize(Roles = "Admin")]
        [Authorize(Policy = "EditDeletePolicy")]
        public IActionResult Delete(int id)
        {
            var respose = deptRepo.DeleteRecord(id);
            // returna View with the data toi be edited
            return RedirectToAction("Index");
        }

        public IActionResult ShowEmployees(int id)
        {
            // Using the Session State

            HttpContext.Session.SetInt32("DeptNo", id);

            var response = deptRepo.GetRecord(id);

            HttpContext.Session.SetObject<Department>("Dept",response.Record);


            // Store the DeptNo in TempData
          //  TempData["DeptNo"] = id;
            // Navigate to an Index() action method of the Employee Controller
            return RedirectToAction("Index", "Employee");
        }




        public IActionResult CustomTag()
        {
            
                var response = deptRepo.GetRecords();
                return View(response.Records);
            
        }

    }
}
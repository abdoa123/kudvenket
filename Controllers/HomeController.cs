using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using kudvenkit.Models;
using kudvenkit.Models.Repositry;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using kudvenkit.ViewModels;

namespace kudvenkit.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployee _emplyees;
        private readonly IHostingEnvironment hostingEnvironment;

        public HomeController(IEmployee employees,
                              IHostingEnvironment hostingEnvironment)
        {
            _emplyees = employees;
            this.hostingEnvironment = hostingEnvironment;

        }
        public IActionResult Index()
        {

            return View(_emplyees.GetAll());
        }
        public IActionResult details(int id)
        {
           
            Employee employee = _emplyees.GetEmployee(id);

            if (employee == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id);
            }

            return View(employee);

        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var employee = _emplyees.GetEmployee(id);
            EditViewModel employeeEditViewModel = new EditViewModel
            {
                Id = employee.Id,
                Name = employee.name,
                Email = employee.email,
                Department = employee.Department,
                photoPath = employee.PhotoPath
            };
            return View(employeeEditViewModel);
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        [HttpGet]
        public ViewResult create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateViewModel employee)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                // has selected an image to upload.
                if (employee.Photo != null)
                {
                    uniqueFileName = ProcessUploadedFile(employee);

                }
                // employee.Id = _emplyees.GetAll().Max(e => e.Id) + 1;

                Employee newEmployee = new Employee
                {
                    name = employee.Name,
                    email = employee.Email,
                    Department = employee.Department,
                    // Store the file name in PhotoPath property of the employee object
                    // which gets saved to the Employees database table
                    PhotoPath = uniqueFileName
                };
                _emplyees.AddEmployee(newEmployee);
                return RedirectToAction("details", new { id = newEmployee.Id });
                //return RedirectToAction("Index");
            }
            else
            {
                return View();
            }

        }
        [HttpPost]
        public IActionResult Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = _emplyees.GetEmployee(model.Id);
                employee.name = model.Name;
                employee.email = model.Email;
                employee.Department = model.Department;
                // has selected an image to upload.
                if (model.Photo != null)
                {
                    if (model.photoPath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath,
                            "images", model.photoPath);
                        System.IO.File.Delete(filePath);
                    }
                    employee.PhotoPath = ProcessUploadedFile(model);

                }
                // employee.Id = _emplyees.GetAll().Max(e => e.Id) + 1;   
                _emplyees.update(employee);
                return RedirectToAction("details", new { id = employee.Id });
                //return RedirectToAction("Index");
            }
            else
            {
                return View();
            }

        }

        private string ProcessUploadedFile(CreateViewModel model)
        {
            string uniqueFileName = null;

            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            ViewBag.ErrorMessage = "Sorry, the resource you requested could not be found";
           
            return View();
        }
    }
}

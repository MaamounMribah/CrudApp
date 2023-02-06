using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CrudApp.Data;
using CrudApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CrudApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext db;
        public EmployeeController(ApplicationDbContext db){
            this.db = db;
        }
        
        // Get EmployeeList
        public IActionResult Index()
        {
            List<Employee> employeeList=db.Employees.ToList();
            ViewBag.EmployeeList=employeeList;
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        //Create (Post)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            if(employee.Name==null){
                ModelState.AddModelError("NameError","The name of the employee cannot be empty");
            }
            if(employee.JobTitle==null){
                ModelState.AddModelError("JobTitle","The JobTitle of the employee cannot be empty");
            }
            if(ModelState.IsValid){
                db.Employees.Add(employee);
                db.SaveChanges();
                TempData["success"]="Employee "+employee.Name+" created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        //Edit (Get Edit View)
        [HttpGet] 
        public IActionResult Edit(int? employeeId)
        {
            if(employeeId==0|| employeeId==null){
                return NotFound();
            }
            var employeeFromDB = db.Employees.Find(employeeId);
            if(employeeFromDB==null){
                return NotFound();
            }
            ViewBag.Employee=employeeFromDB;
            return View();
        }
        //Edit (Put)
        [HttpPost] 
        
        public IActionResult Edit(Employee? employee)
        {
            if(employee.Name==null){
                ModelState.AddModelError("NameError","The name of the employee cannot be empty");
                TempData["edit_error"] ="error editing employee " + employee.Id;
            }
            if(employee.JobTitle==null){
                ModelState.AddModelError("JobTitleError","The Display Order of the employee cannot be empty");
                TempData["edit_error"] ="error editing employee " + employee.Id;
            }
            if(ModelState.IsValid){
                db.Employees.Update(employee);
                db.SaveChanges();
                TempData["edit"]="employee "+employee.Name+" was successfully updated";
                return RedirectToAction("Index");
            }
            ViewBag.Employee=employee;
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? employeeId){
            if(employeeId==0|| employeeId==null){
                return NotFound();
            }
            var employeeFromDB = db.Employees.Find(employeeId);
            if(employeeFromDB==null){
                
                return NotFound();
            }
            db.Employees.Remove(employeeFromDB);
            db.SaveChanges();
            TempData["delete"]="employee "+employeeFromDB.Name+" was successfully deleted";
            ViewBag.Employee=employeeFromDB;
            return RedirectToAction("Index");
        }
        // Read (More details conserning an employee)
        public IActionResult Details(int? employeeId){
            if(employeeId==0|| employeeId==null){
                return NotFound();
            }
            var employeeFromDB = db.Employees.Find(employeeId);
            if(employeeFromDB==null){
                
                return NotFound();
            }
            ViewBag.Employee = employeeFromDB;
            return View();
        }
    }
}
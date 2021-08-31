using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStudent _IStudentObject;
        private readonly IHostingEnvironment hostingenvironment;
        private readonly ILogger logger;

        public HomeController(IStudent IstudentObj, IHostingEnvironment hostingenvironment
            ,ILogger<HomeController> logger)
        {
            _IStudentObject = IstudentObj;
            this.hostingenvironment = hostingenvironment;
            this.logger = logger;
        }
        public ViewResult Index()
        {
            var models = _IStudentObject.GetAllStudent();
            return View(models);
        }
        public ViewResult Details(int? id)
        {
            //throw new Exception("Hello");
            logger.LogTrace("Trace Log");
            logger.LogDebug("Debug Log");
            logger.LogInformation("Information Log");
            logger.LogWarning("Warning Log");
            logger.LogError("Error Log");
            logger.LogCritical("Critical Log");
            Student student= _IStudentObject.getStudent(id.Value);
            if (student == null)
            {
                Response.StatusCode = 404;
                return View("StudentError", id.Value);
            }
            //Student model = _IStudentObject.getStudent(2);
            //ViewData["Student"] = model;
            //ViewData["PageTitle"] = "Students Details";
            //ViewBag.stud = model;
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Stydentprop = _IStudentObject.getStudent(id ?? 1),
                PageTitle = "Students Details"

            };

            return View(homeDetailsViewModel);
        }
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
        [HttpGet]
        public ViewResult Edit(int id)
        {
            Student student = _IStudentObject.getStudent(id);
            HomeEditsViewModel homeEditsViewModel = new HomeEditsViewModel
            {
                id = student.id,
                name = student.name,
                email = student.email,
                department = student.department,
                existingphoto = student.photopath,


            };
            return View(homeEditsViewModel);
        }

        [HttpPost]
        public IActionResult Edit(HomeEditsViewModel model)
        {
           
            if (ModelState.IsValid)
            {
                Student student = _IStudentObject.getStudent(model.id);
                student.name = model.name;
                student.email = model.email;
                student.department = model.department;
                if (model.photopath != null)
                {
                    if (model.existingphoto != null)
                    {
                        string photopath = Path.Combine(hostingenvironment.WebRootPath, "images",model.existingphoto);
                        System.IO.File.Delete(photopath);
                    }
                    student.photopath = photoupload(model);
                } 
                //Student newstudent = new Student
                //{
                //    name = model.name,
                //    email = model.email,
                //    department = model.department,
                //    photopath = unifilename
                //};
                _IStudentObject.Update(student);
                return RedirectToAction("Index");
            }
            return View();
        }

        private string photoupload(StudentCreateViewModel model)
        {
            string unifilename = null;
            if (model.photopath != null)
            {
                string uploadfolder = Path.Combine(hostingenvironment.WebRootPath, "images");
                unifilename = Guid.NewGuid().ToString() + "_" + model.photopath.FileName;
                string filepath = Path.Combine(uploadfolder, unifilename);
                using (var filestream = new FileStream(filepath, FileMode.Create))
                {
                    model.photopath.CopyTo(filestream);
                }

                   
            }

            return unifilename;
        }

        [HttpPost]
        public IActionResult Create(StudentCreateViewModel model)
        {
            if(ModelState.IsValid)
            {
                string unifilename = photoupload(model);
                Student newstudent = new Student
                {
                    name = model.name,
                    email = model.email,
                    department = model.department,
                    photopath = unifilename
                };
                _IStudentObject.Add(newstudent);
                return RedirectToAction("Details", new { id = newstudent.id });
            }
            return View();
        }
    }
}

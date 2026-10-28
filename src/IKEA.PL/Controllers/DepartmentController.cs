using IKEA.BLL.Models.Departments;
using IKEA.BLL.Services.Departments;
using IKEA.DAL.Models.Departments;
using IKEA.PL.ViewModels.Common.Departments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.Identity.Client;
using System.Net.WebSockets;

namespace IKEA.PL.Controllers
{
    public class DepartmentController : Controller
    {
        #region Services
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _environment;

        public DepartmentController(IDepartmentService departmentService,
            ILogger<DepartmentController> logger,
            IWebHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
            _departmentService = departmentService;

        }
        #endregion

        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            var departments = _departmentService.GetAllDepartments();

            return View(departments);
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {

            return View(new CreatedDepartmentDto
            {
                CreationDate = DateTime.Today
            });
        }

        [HttpPost]
        public IActionResult Create(CreatedDepartmentDto department)
        {
            if (!ModelState.IsValid)
                return View(department);

            var message = string.Empty;

            try
            {
                var result = _departmentService.CreatedDepartment(department);

                if (result > 0)
                    return RedirectToAction(nameof(Index));

                else
                {
                    message = "Department is not Created";
                    ModelState.AddModelError(string.Empty, message);
                    return View(department);
                }


            }
            catch (Exception ex)
            {
                // 1.Log Exception
                _logger.LogError(ex, ex.Message);

                // 2.Set Message
                message = _environment.IsDevelopment() ? ex.Message : "Department is not Created";

            }
            ModelState.AddModelError(string.Empty, message);
            return View(department);
        }

        #endregion

        #region Details
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id is null)
                return BadRequest();

            var department = _departmentService.GetDepartmentById(id.Value);

            if (department is null)
                return NotFound();

            return View(department);

        }
        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null)
                return BadRequest();

            var department = _departmentService.GetDepartmentById(id.Value);

            if (department is null)
                return NotFound();

            return View(new DepartmentEditViewModel()
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate,
            });

        }

        [HttpPost]
        public IActionResult Edit(DepartmentEditViewModel departmentVM)
        {
            if (!ModelState.IsValid)
                return View(departmentVM);

            var message = string.Empty;

            try
            {
                var UpdatedDepartments = new UpdatedDepartmentDto()
                {
                    Code = departmentVM.Code,
                    Name = departmentVM.Name,
                    Description = departmentVM.Description,
                    CreationDate = departmentVM.CreationDate,
                };

                var result = _departmentService.UpdatedDepartment(UpdatedDepartments) > 0;

                if (result)
                    return RedirectToAction(nameof(Index));

                message = "an error has occured during updating the department :(";
            }
            catch (Exception ex)
            {

                // 1.Log Exception
                _logger.LogError(ex, ex.Message);

                // 2.Set Message
                message = _environment.IsDevelopment() ? ex.Message : "an error has occured during updating the department :(";

            }

            ModelState.AddModelError(string.Empty, message);
            return View(departmentVM);


        }
        #endregion

        #region Delete
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null)
                return BadRequest();

            var department = _departmentService.GetDepartmentById(id.Value);

            if (department is null)
                return NotFound();

            return View(department);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                var deleted = _departmentService.DeleteDepartment(id);
                if (deleted)
                    return Ok();

                return BadRequest("Error deleting department.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Something went wrong.");
            }
        }
    }

        #endregion


    }


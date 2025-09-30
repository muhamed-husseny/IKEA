using IKEA.BLL.Models.Departments;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.Persistence.Repositories.Departments;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.Departments
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repository;

        public DepartmentService(IDepartmentRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<DepartmentToReturnDto> GetAllDepartments()
        {
            var departments = _repository.GetAllAsIQueryable().Select(department => new DepartmentToReturnDto()
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                //Description = department.Description,
                CreationDate = department.CreationDate,
            }).AsNoTracking().ToList();

           return departments;
             
        }

        public DepartmentDetailsToReturnDto? GetDepartmentById(int id)
        {
            var department = _repository.GetById(id);
            if(department is not null)
            return new DepartmentDetailsToReturnDto()
            {
                Id=department.Id,
                Name=department.Name,
                Code = department.Code,
                Description = department.Description,
                CreationDate = department.CreationDate,
                CreatedBy = department.CreatedBy,
                CreatedOn = department.CreatedOn,
                LastModifiedBy = department.LastModifiedBy,
                LastModifiedOn = department.LastModifiedOn,
            };
            return null;
        }

        public int CreatedDepartment(CreatedDepartmentDto departmentDto)
        {
            var department = new Department()
            {
                Code = departmentDto.Code,
                Name = departmentDto.Name,
                Description = departmentDto.Description,
                CreationDate = departmentDto.CreationDate,
                CreatedBy = 1,
                //CreatedOn = DateTime.Now,
                LastModifiedBy = 1, 
                LastModifiedOn = DateTime.Now,
            };

            return _repository.Add(department);
        }

        public int UpdatedDepartment(UpdatedDepartmentDto departmentDto)
        {
            var department = new Department()
            {
                Id = departmentDto.Id,
                Code = departmentDto.Code,
                Name = departmentDto.Name,
                Description = departmentDto.Description,
                CreationDate = departmentDto.CreationDate,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.Now,
            };

            return _repository.Update(department);
        }

        public bool DeleteDepartment(int id)
        {
            var department = _repository.GetById(id);
            if (department is not null) 
            return _repository.Delete(department) > 0;

            return false;
            
        }
       
    }
}

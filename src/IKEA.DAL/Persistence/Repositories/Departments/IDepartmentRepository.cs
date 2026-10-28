using System;
using System.Collections.Generic;
using IKEA.DAL.Models.Departments;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistence.Repositories.Departments
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll(bool AsNoTracking = true);    
        
        IQueryable<Department> GetAllAsIQueryable();
        Department? GetById(int id);
        int Add(Department entity);
        int Update(Department entity);
        int Delete(Department entity);

    }
}

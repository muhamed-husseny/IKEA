using IKEA.BLL.Models.Departments;

namespace IKEA.BLL.Services.Departments
{
    public interface IDepartmentService
    {
        IEnumerable<DepartmentToReturnDto> GetAllDepartments();
        DepartmentDetailsToReturnDto? GetDepartmentById(int id);

        int CreatedDepartment(CreatedDepartmentDto departmentDto);

        int UpdatedDepartment(UpdatedDepartmentDto departmentDto);

        bool DeleteDepartment(int id);

    }
}

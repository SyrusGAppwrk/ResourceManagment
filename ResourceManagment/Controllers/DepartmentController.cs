    using Microsoft.AspNetCore.Mvc;
using ResourceManagment.Models;
using ResourceManagment.Repository;

namespace ResourceManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetDepartments()
        {
            try
            {
                return Ok(await _departmentRepository.GetDepartments());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Data retrinving Problem");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetDepartments(int id)
        {
            try
            {
                var department = await _departmentRepository.GetDepartments(id);
                if (department == null)
                {
                    return NotFound();
                }
                return Ok(department);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Data Not Found");
            }
        }
        [HttpPost]
        public async Task<ActionResult> AddDepartments(Department department)
        {
            try
            {
                if (department == null)
                {
                    return BadRequest();
                }
                var CreatedDepartment = await _departmentRepository.AddDepartments(department);
                return CreatedAtAction(nameof(GetDepartments), new { id = CreatedDepartment.Id }, CreatedDepartment);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Data Not Found");
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Department>> UpdateDepartment(int id, Department department)
        {
            try
            {
                if (department.Id != id)
                {
                    return BadRequest("Id Not found");
                }
                var departmentupdate = await _departmentRepository.GetDepartments(id);
                if (departmentupdate == null)
                {
                    return NotFound($"Department id{id} Not Found");
                }
                return await _departmentRepository.UpdateDepartment(department);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Data Not Found");
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Department>> DeleteDepartment(int id)
        {
            try
            {
                var departmentdelete = await _departmentRepository.GetDepartments(id);
                if(departmentdelete==null)
                {
                    return NotFound($"Deleteable Id{id} Not Found");
                }
                return await _departmentRepository.DeleteDepartment(id);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Data Not Found");
            }
        }
    }
}
 
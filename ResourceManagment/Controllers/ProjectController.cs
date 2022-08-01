using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResourceManagment.Models;
using ResourceManagment.Repository;

namespace ResourceManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  //  [Authorize]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        public ProjectController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        [HttpGet]
         public async Task<ActionResult>GetProjects()
        {
            try
            {
                return Ok( _projectRepository.GetProjects());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Data retrinving Problem");
            }
        }
        [HttpGet("{id:int}")]
        public async Task <ActionResult> GetProjects(int id)
        {
            try
            {
                var projectid = await _projectRepository.GetProjects(id);
                if (projectid == null)
                {
                    return BadRequest("Id Not Found");
                }
                return Ok(projectid);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Data retrinving Problem");
            }
        }
        [HttpPost]
        public async Task<ActionResult> AddProjects(Project project)
        {
            try
            {
                if (project == null)
                {
                    return BadRequest();
                }
                var CreatedProject = await _projectRepository.AddProjects(project);
                return CreatedAtAction(nameof(GetProjects), new { id = CreatedProject.Id }, CreatedProject);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ("Data not Found"));
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Project>> UpdateProject(int id ,Project project)
        {
            try
            {
                if (project.Id!= id)
                {
                    return NotFound();
                }
                var result = await _projectRepository.GetProjects(id);
                if (result == null)
                {
                    return BadRequest("iD NOT FOUND");
                }
                return await _projectRepository.UpdateProject(project);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ("Data not Found"));
            }
        }
        
    }
}

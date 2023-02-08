using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WordGoal.API.Models;
using WordGoal.Data;
using WordGoal.Domain;

namespace WordGoal.API.Controllers
{
    [Route("api/projects")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly int _userId = 1;
        private readonly IWordGoalRepository _repo;
        private readonly IMapper _mapper;

        public ProjectsController(IMapper mapper, IWordGoalRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetProjects()
        {
            return Ok(_mapper.Map<IEnumerable<ProjectDto>>(await _repo.GetProjectsAsync(_userId)));
        }

        [HttpGet("{projectId}")]
        public async Task<ActionResult<ProjectDto>> GetProject(int projectId)
        {
            var project = await _repo.GetProjectAsync(_userId, projectId);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ProjectDto>(project));
        }

        // PUT: api/Projects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{projectId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutProject(int projectId, ProjectForCreationDto project)
        {
            var projectToModify = await _repo.GetProjectAsync(_userId, projectId);
            if (projectToModify == null)
            {
                return NotFound();
            }
            _mapper.Map(project, projectToModify);
            await _repo.SaveAsync();

            return Ok(_mapper.Map<ProjectDto>(projectToModify));
        }

        // POST: api/Projects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProjectDto>> PostProject(ProjectForCreationDto project)
        {
            var projectEntity = _mapper.Map<Project>(project);
            _repo.AddProject(projectEntity, _userId);
            await _repo.SaveAsync();

            var projectToReturn = _mapper.Map<ProjectDto>(projectEntity);

            return CreatedAtAction("GetProject", new { projectId = projectToReturn.Id }, projectToReturn);
        }

        // DELETE: api/Projects/5
        [HttpDelete("{projectId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProject(int projectId)
        {
            var project = await _repo.GetProjectAsync(_userId, projectId);

            if (project == null)
                return NotFound();

            _repo.DeleteProject(project);
            await _repo.SaveAsync();

            return NoContent();
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Practical_17.Models;
using Practical_17.Services;
using Practical_17.ViewModels;

namespace Practical_17.Controllers
{
    [ApiController]
    [Route("api/Students")]
    public class StudentsAutoController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<StudentsAutoController> _logger;
        public StudentsAutoController(IStudentRepository repository, IMapper mapper, ILogger<StudentsAutoController> logger)
        {
            _studentRepository = repository;
            _mapper = mapper;
            _logger = logger;
        } 
        // GET: api/students
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<StudentViewModel>>> GetAll()
        {
            var students = await _studentRepository.GetStudentsAsync();
            return Ok(_mapper.Map<IEnumerable<StudentViewModel>>(students));
        }
        // GET: api/students/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StudentViewModel>> GetById(int id)
        {
            var student = await _studentRepository.GetStudentAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<StudentViewModel>(student));
        }

        // POST: api/students
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StudentViewModel>> Create([FromBody] CreateStudentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var student = _mapper.Map<Student>(model);
            await _studentRepository.AddStudent(student);

            return CreatedAtAction(nameof(GetById),
                new { id = student.Id },
                _mapper.Map<StudentViewModel>(student));
        }

        // PUT: api/students/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update(int id, [FromBody] EditStudentViewModel model)
        {
            // Remove the ID check since we're not expecting it in the body
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var student = await _studentRepository.GetStudentAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            // Map all properties except ID
            _mapper.Map(model, student);
            student.Id = id; // Ensure ID remains unchanged

            await _studentRepository.UpdateStudentAsync(student);
            return NoContent();
        }

        // DELETE: api/students/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _studentRepository.GetStudentAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            await _studentRepository.DeleteStudentAsync(id);
            return NoContent();
        }

    }
}

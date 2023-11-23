using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;
using TaskModel = TrilhaApiDesafio.Models.Task;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly OrganizerContext _context;

        public TaskController(OrganizerContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var task  = _context.Tasks.Find(id);
            if(task is null)
                return NotFound();

            return Ok(task);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var tasks = _context.Tasks;
            return Ok(tasks);
        }

        [HttpGet("GetByTitle")]
        public IActionResult GetByTitle(string title)
        {
            var tasks = _context.Tasks.Where(x => x.Title.Contains(title));

            return Ok(tasks);
        }

        [HttpGet("GetByDate")]
        public IActionResult GetByDate(DateTime date)
        {
            var task = _context.Tasks.Where(x => x.Date.Date == date.Date);
            return Ok(task);
        }

        [HttpGet("GetByStatus")]
        public IActionResult GetByStatus(EnumStatsTask status)
        {
            var task = _context.Tasks.Where(x => x.Status == status);
            return Ok(task);
        }

        [HttpPost]
        public IActionResult Create(TaskModel task)
        {
            if (task.Date == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            _context.Add(task);
            _context.SaveChanges();
            
            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, TaskModel task)
        {
            var taskDb = _context.Tasks.Find(id);

            if (taskDb == null)
                return NotFound();

            if (task.Date == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            taskDb.Title = task.Title;    
            taskDb.Status = task.Status;    
            taskDb.Date = task.Date;    
            taskDb.Description = task.Description;    

            _context.Tasks.Update(taskDb);
            _context.SaveChanges();
            return Ok(taskDb);
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var taskDb = _context.Tasks.Find(id);

            if (taskDb == null)
                return NotFound();

            _context.Tasks.Remove(taskDb);

            _context.SaveChanges();
            return NoContent();
        }
    }
}

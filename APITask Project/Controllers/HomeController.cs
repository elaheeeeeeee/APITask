using APITask_Project.DTO;
using APITask_Project.EF;
using APITask_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace APITask_Project.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class HomeController : Controller
    {


        private readonly Context _context;
        public HomeController(Context context)
        {
            _context = context;
        }

        
        [HttpPost("seed")]
        public async Task<IActionResult> SeedData()
        {
            var myTasks = new List<MyTask>
            {
                new MyTask { Title = "Task1", Description = "APITask", IsCompleted = true, DueDate = new DateTime(2026, 3, 1) },
                new MyTask { Title = "Task2", Description = "BussinessTask", IsCompleted = false, DueDate = new DateTime(2026, 4, 8) },
            };

            await _context.MyTasks.AddRangeAsync(myTasks);
            await _context.SaveChangesAsync();

            return Ok("داده‌ها با موفقیت اضافه شدند.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await _context.MyTasks.ToListAsync();
            if (!tasks.Any())
                return NotFound("هیچ وظیفه‌ای پیدا نشد.");

            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _context.MyTasks.FindAsync(id);
            if (task == null)
                return NotFound("وظیفه‌ای با این شناسه پیدا نشد.");

            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> AddTask([FromBody] MyTaskDTO myTaskDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var task = new MyTask
            {
                Title = myTaskDTO.Title,
                Description = myTaskDTO.Description,
                DueDate = myTaskDTO.DueDate.Value,
                IsCompleted = myTaskDTO.IsCompleted.Value
            };

            await _context.MyTasks.AddAsync(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditTask(int id, [FromBody] MyTaskDTO myTaskDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var task = await _context.MyTasks.FindAsync(id);
            if (task == null)
                return NotFound("وظیفه‌ای با این شناسه پیدا نشد.");

            if (task.Title == myTaskDTO.Title &&
            task.Description == myTaskDTO.Description &&
            task.DueDate == myTaskDTO.DueDate &&
            task.IsCompleted == myTaskDTO.IsCompleted)
                   {
                        return NoContent();
                   }

            task.Title = myTaskDTO.Title;
            task.Description = myTaskDTO.Description;
            task.DueDate = myTaskDTO.DueDate.Value;
            task.IsCompleted = myTaskDTO.IsCompleted.Value;

            await _context.SaveChangesAsync();

            return Ok(new { message = "وظیفه با موفقیت آپدیت شد!", Task = task });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.MyTasks.FindAsync(id);
            if (task == null)
                return NotFound("وظیفه‌ای با این شناسه پیدا نشد.");

            _context.MyTasks.Remove(task);
            await _context.SaveChangesAsync();

            return Ok("وظیفه حذف شد.");
        }
    }
}




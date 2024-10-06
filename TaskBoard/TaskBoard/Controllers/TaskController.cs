using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using TaskBoard.Data;
using TaskBoard.Models;

namespace TaskBoard.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {

        private readonly TaskBoardAppDbContext data;

        public TaskController(TaskBoardAppDbContext context)
        {
            data = context;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new TaskFormViewModel();

            model.Boards = await GetBoards();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskFormViewModel model)
        {
            if (!(await GetBoards()).Any(b => b.Id == model.BoardId))
            {
                ModelState.AddModelError(nameof(model.BoardId), "Board does not exist");
            }

            if (!ModelState.IsValid)
            {
                model.Boards = await GetBoards();
                return View(model);
            }

            var entity = new TaskBoard.Data.Task()
            {
                BoardId = model.BoardId,
                CreatedOn = DateTime.Now,
                Description = model.Description,
                OwnerId = GetUserId(),
                Title = model.Title
            };

            await data.AddAsync(entity);
            await data.SaveChangesAsync();

            return RedirectToAction("Index", "Board");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var task = await data.Tasks
                .Where(t => t.Id == id)
                .Select(t => new TaskDetailsViewModel()
                {
                    Board = t.Board.Name,
                    Description = t.Description,
                    CreatedOn = t.CreatedOn != null && t.CreatedOn.HasValue
                 ? t.CreatedOn.Value.ToString("dd.MM.yyyy HH:mm")
                 : "",
                    Owner = t.Owner.UserName,
                    Title = t.Title
                })
                .FirstOrDefaultAsync();

            return View(task);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var task = await data.Tasks.FindAsync(id);

            if (task == null)
            {
                return BadRequest();
            }

            if (task.OwnerId != GetUserId())
            {
                return Unauthorized();
            }

            var model = new TaskFormViewModel()
            {
                Title=task.Title,
                BoardId = task.BoardId,
                Description = task.Description,
                Id = task.Id,
                Boards = await GetBoards()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TaskFormViewModel model, int id)
        {
            var task = await data.Tasks.FindAsync(id);

            if (task ==null)
            {
                return BadRequest();
            }

            if (task.OwnerId != GetUserId())
            {
                return Unauthorized();
            }

            if (!(await GetBoards()).Any(b => b.Id == model.BoardId))
            {
                ModelState.AddModelError(nameof(model.BoardId), "Board does not exist");
            }

            if (!ModelState.IsValid)
            { 
            model.Boards = await GetBoards();
                return View(model);
            }

            task.Title = model.Title;
            task.Description = model.Description;
            task.BoardId = model.BoardId;

            await data.SaveChangesAsync();

            return RedirectToAction("Index", "Board");
        }

        [HttpGet]
        public async Task<IActionResult> Delete()
        { 
        }



        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }


        private async Task<IEnumerable<TaskBoardModel>> GetBoards()
        {
            return await data.Boards
                 .Select(x => new TaskBoardModel
                 {
                     Id = x.Id,
                     Name = x.Name
                 })
                 .ToListAsync();
        }
    }
}

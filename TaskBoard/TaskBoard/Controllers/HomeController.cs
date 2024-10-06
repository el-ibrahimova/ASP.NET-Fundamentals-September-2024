using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskBoard.Data;
using TaskBoard.Models;
using Task = TaskBoard.Data.Task;

namespace TaskBoard.Controllers
{
    public class HomeController : Controller
    {
        private readonly TaskBoardAppDbContext data;

        public HomeController(TaskBoardAppDbContext context)
        {
            data = context;
        }

        [HttpGet]
       public async Task<IActionResult> Index()
       {
           var taskBoards = await data
               .Boards
               .AsNoTracking()
               .Select(b => b.Name)
               .Distinct()
               .ToListAsync();

            var tasksCount = new List<HomeBoardViewModel>();

            foreach (var boardName in taskBoards)
            {
                var tasksInBoard = data
                    .Tasks
                    .AsNoTracking()
                    .Where(t => t.Board.Name == boardName)
                    .Count(t => t.Board != null && t.Board.Name == boardName);

                tasksCount.Add(new HomeBoardViewModel()
                {
                    BoardName = boardName,
                    TasksCount = tasksInBoard
                });
            }

            var userTasksCount = -1;

            if (User.Identity.IsAuthenticated)
            {
                var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                userTasksCount = data
                    .Tasks
                    .AsNoTracking()
                    .Where(t => t.OwnerId == currentUserId)
                    .Count(t => t.OwnerId == currentUserId);
            }

            var homeModel = new HomeViewModel()
            {
                AllTasksCount = data.Tasks.Count(),
                BoardsWithTasksCount = tasksCount,
                UserTasksCount = userTasksCount
            };

            return View(homeModel);
        }
    }
}

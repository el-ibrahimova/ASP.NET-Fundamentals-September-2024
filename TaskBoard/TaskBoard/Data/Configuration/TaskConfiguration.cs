using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskBoard.Data.Configuration
{
    public class TaskConfiguration : IEntityTypeConfiguration<Task>
    {
        private IdentityUser testUser  = GetUser();

        private Board openBoard { get; set; } = new Board()
        {
            Id=1,
            Name ="Open"
        };
        private Board inProgressBoard = new Board()
        {
            Id = 2,
            Name = "In Progress"
        };
        private Board doneBoard = new Board()
        {
            Id = 3,
            Name = "Done"
        };


        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder
                .HasOne(t => t.Board)
                .WithMany(b => b.Tasks)
                .HasForeignKey(t => t.BoardId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(SeedTasks());
        }



        private IEnumerable<Task> SeedTasks()
        {
            Task[] tasks = new Task[]
            {
                new Task()
                {
                    Id = 1,
                    Title = "Improve CSS styles",
                    Description = "Implement better styling for all public pages",
                    CreatedOn = DateTime.Now.AddDays(-200),
                    OwnerId = testUser.Id,
                    BoardId = openBoard.Id
                },
                new Task()
                {
                    Id = 2,
                    Title = "Android Client App",
                    Description = "Create Android client app for the TaskBoard RESTful API",
                    CreatedOn = DateTime.Now.AddMonths(-5),
                    OwnerId = testUser.Id,
                    BoardId = openBoard.Id
                },
                new Task()
                {
                    Id = 3,
                    Title = "Desktop Client App",
                    Description = "Create Windows Forms desktop app client for the TaskBoard RESTful API",
                    CreatedOn = DateTime.Now.AddMonths(-1),
                    OwnerId = testUser.Id,
                    BoardId = inProgressBoard.Id
                },
                new Task()
                {
                    Id = 4,
                    Title = "Create Tasks",
                    Description = "Implement [Create Task] page for adding new tasks",
                    CreatedOn = DateTime.Now.AddYears(-1),
                    OwnerId = testUser.Id,
                    BoardId = doneBoard.Id
                }
            };

            return tasks;
        }

        private static IdentityUser GetUser()
        {
            var hasher = new PasswordHasher<IdentityUser>();

            var user = new IdentityUser()
            {
                UserName = "test@softuni.bg",
                NormalizedUserName = "TEST@SOFTUNI.BG"
            };

            user.PasswordHash = hasher.HashPassword(user, "softuni");

            return user;
        }
    }
}
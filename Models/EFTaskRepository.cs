using Microsoft.EntityFrameworkCore;

namespace WebApplication8.Models
{
    public class EFTaskRepository : ITaskRepository
    {
        private ApplicationDbContext _context;

        public EFTaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Task> Tasks =>
            _context.Tasks.Include(t => t.Category);

        public IQueryable<Category> Categories =>
            _context.Categories;

        public void AddTask(Task task)
        {
            _context.Tasks.Add(task);
        }

        public void UpdateTask(Task task)
        {
            _context.Tasks.Update(task);
        }

        public void DeleteTask(Task task)
        {
            _context.Tasks.Remove(task);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}

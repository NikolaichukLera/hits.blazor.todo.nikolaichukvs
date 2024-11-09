using Microsoft.EntityFrameworkCore;
using TodoServerApp.Data.Interfaces;

namespace TodoServerApp.Data.Services
{
    public class MSSQLDataService(ApplicationDbContext context) : IDataService
    {
        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return await context.TaskItem.ToArrayAsync();
        }

        public async Task SaveAsync(TaskItem taskItem)
        {
            if (taskItem.Id == 0)
            {
                taskItem.CreatedDate = DateTime.Now;
                await context.TaskItem.AddAsync(taskItem);
            }
            else
            {
                context.TaskItem.Update(taskItem);

            }
            await context.SaveChangesAsync();
        }

       

        public async Task DeleteAsync(int id)
        {
            var taskItem = await context.TaskItem.FirstAsync(x => x.Id == id);
            context.TaskItem.Remove(taskItem);
            await context.SaveChangesAsync();
        }

        public async Task<TaskItem> GetTaskItem(int id)
        {
            return await context.TaskItem.FirstAsync(x => x.Id == id);
        }

        public Task<TaskItem> GetTaskAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}

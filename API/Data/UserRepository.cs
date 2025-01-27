using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository(DataContext context) : IUserRepository
    {
        //private readonly DataContext _context;

        public async Task<IEnumerable<AppUser>> GetAllAsync()
        {
            return await context.Users.Include(x=>x.Photos)
                .ToListAsync();
        }

        public async Task<AppUser?> GetUserByIdAsync(int id)
        {
            return await context.Users.FindAsync(id);
        }

        public async Task<AppUser?> GetUserByUsernameAsync(string username)
        {
            return await context.Users.Include(x => x.Photos)
                .SingleOrDefaultAsync(x => x.Username == username);   
        }

        public async Task<bool> SaveAllAsync()
        {
            return await context.SaveChangesAsync()>0;
        }

        public void Update(AppUser user)
        {

            context.Entry(user).State = EntityState.Modified;
        }
    }
}

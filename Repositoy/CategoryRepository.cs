using Microsoft.EntityFrameworkCore;
using Yum.Data;
using Yum.Repositoy.IRepository;

namespace Yum.Repositoy
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Category> AddAsync(Category category)
        {
            await _db.Category.AddAsync(category);
            await _db.SaveChangesAsync();

            return category;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _db.Category.FirstOrDefaultAsync(c => c.Id == id);
            if (category != null)
            {
                _db.Category.Remove(category);
                return (await _db.SaveChangesAsync()) > 0;
            } else
            {
                return false;
            }
        }

        public async Task<Category> GetAsync(int id)
        {
            var category = await _db.Category.FirstOrDefaultAsync(c => c.Id == id);

            if (category == null) return new Category();

            return category;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _db.Category.ToListAsync();
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            var categoryFromDb = await _db.Category.FirstOrDefaultAsync(c => c.Id == category.Id);
            if (categoryFromDb != null)
            {
                categoryFromDb.Name = category.Name;
                _db.Category.Update(categoryFromDb);
                await _db.SaveChangesAsync();
                return categoryFromDb;
            }
            return category;
        }
    }
}

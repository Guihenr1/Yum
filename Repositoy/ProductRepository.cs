using Microsoft.EntityFrameworkCore;
using Yum.Data;
using Yum.Repositoy.IRepository;

namespace Yum.Repositoy
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductRepository(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<Product> AddAsync(Product product)
        {
            await _db.Product.AddAsync(product);
            await _db.SaveChangesAsync();

            return product;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _db.Product.FirstOrDefaultAsync(c => c.Id == id);
            if (product != null)
            {
                if (!string.IsNullOrEmpty(product.ImageUrl))
                {
                    var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, product.ImageUrl.TrimStart('/'));
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                }

                _db.Product.Remove(product);
                return (await _db.SaveChangesAsync()) > 0;
            } else
            {
                return false;
            }
        }

        public async Task<Product> GetAsync(int id)
        {
            var product = await _db.Product.FirstOrDefaultAsync(c => c.Id == id);

            if (product == null) return new Product();

            return product;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _db.Product.Include(x => x.Category).ToListAsync();
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            var productFromDb = await _db.Product.FirstOrDefaultAsync(c => c.Id == product.Id);
            if (productFromDb != null)
            {
                productFromDb.Name = product.Name;
                productFromDb.Price = product.Price;
                productFromDb.Description = product.Description;
                productFromDb.SpecialTag = product.SpecialTag;
                productFromDb.CategoryId = product.CategoryId;
                productFromDb.ImageUrl = product.ImageUrl;
                _db.Product.Update(productFromDb);
                await _db.SaveChangesAsync();
                return productFromDb;
            }
            return product;
        }
    }
}

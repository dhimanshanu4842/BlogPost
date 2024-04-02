using BlogApp.Data;
using BlogApp.Models;
using BlogApp.Repository.IRepository;

namespace BlogApp.Repository
{
    public class BlogService : IBlogService
    {
        private readonly BlogDbContext _context;
        public BlogService(BlogDbContext context)
        {
            _context = context;
        }
        public List<Post> GetPostByAutorId(int id)
        {
            var data = _context.Posts.Where(x => x.AuthorId == id).ToList();
            return data;
        }
        public int GetPostCountByCategory(int categoryId)
        {
            var data = _context.Posts.Where(x=>x.CategoryId == categoryId).Count();
            return data;
        }

        public Post GetPostById(int id)
        {
            var data = _context.Posts.Where(x => x.Id == id).FirstOrDefault();
            return data;
        } 
        public Author GetAuthorById(int id)
        {
            var data = _context.Authors.Where(x => x.Id == id).FirstOrDefault();
            return data;
        }
        public Category GetCategoryById(int id)
        {
            var data = _context.Categories.Where(x => x.Id == id).FirstOrDefault();
            return data;
        }

        
        public void UpdatePost(Post post)
        {
            _context.Posts.Update(post);
            Save();
            
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public List<Category> GetCategoryList()
        {
            var data = _context.Categories.ToList();
            return data;
        }
    }
}

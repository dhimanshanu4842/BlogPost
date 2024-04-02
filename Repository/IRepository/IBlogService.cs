using BlogApp.Models;

namespace BlogApp.Repository.IRepository
{
    public interface IBlogService
    {
        List<Post> GetPostByAutorId(int id);
        int GetPostCountByCategory(int categoryId);
        Post GetPostById(int postId);
        Author GetAuthorById(int authorId);
        Category GetCategoryById(int categoryId);
        void UpdatePost(Post post);

        List<Category> GetCategoryList();
        void Save();



    }
}

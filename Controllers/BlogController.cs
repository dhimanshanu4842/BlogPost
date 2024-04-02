using BlogApp.Models;
using BlogApp.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Reflection.Metadata;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BlogApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blog;
        private readonly APIResponse _apiRespone;
        public BlogController(IBlogService blog)
        {
            _blog = blog;
            _apiRespone = new APIResponse();
        }

        [HttpGet("{authorId}")]
        public APIResponse GetPostsByAuthorId(int authorId)
        {
            Log.Information("==========================GetPostsByAuthorId called======================");
            try
            {
                if (authorId <= 0)
                {
                    Log.Debug($"=====================Invalid authorId passed  to GetPostsByAuthorId id = {authorId}");

                    _apiRespone.StatusCode = StatusCodes.Status400BadRequest;
                    _apiRespone.IsSuccess = false;
                    _apiRespone.Message = "Autor Id must be valid Id";
                    return _apiRespone;
                }
                var authData = _blog.GetAuthorById(authorId);
                if (authData is null)
                {
                    Log.Debug($"=====================Author not found for  authorId = {authorId}======================");

                    _apiRespone.StatusCode = StatusCodes.Status404NotFound;
                    _apiRespone.IsSuccess = false;
                    _apiRespone.Message = "Author not found";
                    return _apiRespone;
                }

                var data = _blog.GetPostByAutorId(authorId);

                if (data.Count() == 0)
                {
                    Log.Debug($"===================Post not found for  authorId = {authorId}=========================");

                    _apiRespone.StatusCode = StatusCodes.Status404NotFound;
                    _apiRespone.IsSuccess = false;
                    _apiRespone.Message = "Post not found";
                    return _apiRespone;
                }
                Log.Information($"==================Data sucess with  authorId = {authorId}==========================");

                _apiRespone.StatusCode = StatusCodes.Status200OK;
                _apiRespone.IsSuccess = true;
                _apiRespone.Message = "Success";
                _apiRespone.Result = data;
                return _apiRespone;
            }
            catch (Exception ex)
            {
                Log.Error($"================Eror for author authorId  = {authorId}. Error = {ex}=====================");

                _apiRespone.StatusCode = StatusCodes.Status400BadRequest;
                _apiRespone.IsSuccess = false;
                _apiRespone.Message = "Something went wrong. Please try again";
                return _apiRespone;
            }
        }
        [HttpPut("UpdatePostCategory")]
        public APIResponse UpdatePostCategory(UpdatePostCategoryVM updatePostCategoryVM)
        {
            try
            {
                Log.Debug($"===================UpdatePostCategory called  category id = {updatePostCategoryVM.CategoryId} post id = {updatePostCategoryVM.PostId}===============");
                if (updatePostCategoryVM.CategoryId <= 0)
                {
                    Log.Debug($"================Invalid Category  id   = {updatePostCategoryVM.CategoryId}");

                    _apiRespone.StatusCode = StatusCodes.Status400BadRequest;
                    _apiRespone.IsSuccess = false;
                    _apiRespone.Message = "Invalid category Id";
                    return _apiRespone;
                }
                if (updatePostCategoryVM.PostId <= 0)
                {
                    Log.Debug($"================Invalid PostId  id   = {updatePostCategoryVM.PostId}");
                    _apiRespone.StatusCode = StatusCodes.Status400BadRequest;
                    _apiRespone.IsSuccess = false;
                    _apiRespone.Message = "Invalid post Id Post Id";
                    return _apiRespone;
                }

                var data = _blog.GetPostById(updatePostCategoryVM.PostId);
                if (data is null)
                {
                    Log.Debug($"================Post not found for id   = {updatePostCategoryVM.PostId}");
                    _apiRespone.StatusCode = StatusCodes.Status404NotFound;
                    _apiRespone.IsSuccess = false;
                    _apiRespone.Message = "Post not found";
                    return _apiRespone;
                }

                var catData = _blog.GetCategoryById(updatePostCategoryVM.CategoryId);
                if (catData is null)
                {
                    Log.Debug($"================Category not found for id   = {updatePostCategoryVM.CategoryId}");
                    _apiRespone.StatusCode = StatusCodes.Status404NotFound;
                    _apiRespone.IsSuccess = false;
                    _apiRespone.Message = "Category not found";
                    return _apiRespone;
                }

                data.CategoryId = updatePostCategoryVM.CategoryId;

                Log.Debug($"================Sucessfully updated  post wiit id = {updatePostCategoryVM.PostId}");

                _blog.UpdatePost(data);
                _apiRespone.StatusCode = StatusCodes.Status200OK;
                _apiRespone.IsSuccess = true;
                _apiRespone.Message = "Success";
                _apiRespone.Result = data;
                return _apiRespone;
            }
            catch (Exception ex)
            {
                Log.Error($"================Error while updating post ====================== {ex}");
                _apiRespone.StatusCode = StatusCodes.Status400BadRequest;
                _apiRespone.IsSuccess = false;
                _apiRespone.Message = "Something went wrong. Please try again";
                return _apiRespone;
            }
        }

        [HttpGet("GetPostCountByCategory")]
        public APIResponse GetPostCountByCategory()
        {
            try
            {
                Log.Information($"================GetPostCountByCategory called ======================");
                List<CategoryCountVM> categoryVm = new List<CategoryCountVM>();

                var categories = _blog.GetCategoryList();

                foreach (var category in categories)
                {
                    CategoryCountVM categoryData = new CategoryCountVM();
                    categoryData.Name = category.Type;
                    categoryData.Count = _blog.GetPostCountByCategory(category.Id);
                    categoryVm.Add(categoryData);
                }
                Log.Information($"================Successfully fetched categories with count ==================");

                _apiRespone.StatusCode = StatusCodes.Status200OK;
                _apiRespone.IsSuccess = true;
                _apiRespone.Message = "Success";
                _apiRespone.Result = categoryVm;
                return _apiRespone;
            }
            catch (Exception ex)
            {
                Log.Error($"================Error while updating post ================== {ex}");
                _apiRespone.StatusCode = StatusCodes.Status400BadRequest;
                _apiRespone.IsSuccess = false;
                _apiRespone.Message = "Something went wrong. Please try again";
                return _apiRespone;
            }
        }
    }
}

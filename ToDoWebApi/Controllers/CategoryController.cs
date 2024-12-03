using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoWebApi.DTOs;
using ToDoWebApi.Models;
using ToDoWebApi.Services;

namespace ToDoWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CategoryController: ControllerBase
{
    private readonly IResourceService<CategoryModel> _categoryService;
    private readonly IResourceService<UserModel> _userService;

    public CategoryController(IResourceService<CategoryModel> categoryService, IResourceService<UserModel> userService)
    {
        _categoryService = categoryService;
        _userService = userService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        try
        {
            var categories = await _categoryService.GetResources();
            return Ok(categories.Where(x => x.UserModel.Email == HttpContext.User.Identity?.Name));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound("Category not found");
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategory(int id)
    {
        try
        {
            return Ok(await _categoryService.GetResourceById(id));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound("Category not found");
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> PostCategory(CategoryDTO category)
    {
        try
        {
            var user = (await _userService.GetResources()).FirstOrDefault(x => x.Email == HttpContext.User.Identity?.Name);
            if (user == null)
            {
                return NotFound("User not found");
            }
            
            return Ok(await _categoryService.Create(new CategoryModel(category, user)));
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> PutCategory(CategoryDTO category)
    {
        try
        {
            var user = (await _userService.GetResources()).FirstOrDefault(x => x.Email == HttpContext.User.Identity?.Name);
            if (user == null)
            {
                return NotFound("User not found");
            }

            await _categoryService.Update(new CategoryModel(category, user));
            return Ok();
        }
        catch (KeyNotFoundException e)
        {
            return NotFound("Category not found");
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCategory(CategoryDTO category)
    {
        try
        {
            var user = (await _userService.GetResources()).FirstOrDefault(x => x.Email == HttpContext.User.Identity?.Name);
            if (user == null)
            {
                return NotFound("User not found");
            }

            await _categoryService.Delete(new CategoryModel(category, user));
            return Ok();
        }
        catch (KeyNotFoundException e)
        {
            return NotFound("Category not found");
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}
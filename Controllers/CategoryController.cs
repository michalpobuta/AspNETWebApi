using Microsoft.AspNetCore.Mvc;
using ToDoWebApi.Models;
using ToDoWebApi.Services;

namespace ToDoWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController: ControllerBase
{
    private readonly IResourceService<CategoryModel> _resourceService;

    public CategoryController(IResourceService<CategoryModel> resourceService)
    {
        _resourceService = resourceService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        try
        {
            return Ok(await _resourceService.GetResources());
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
            //Get Category for ID = id
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

   // Post Category

    // Put Category

    // Delete Category
}
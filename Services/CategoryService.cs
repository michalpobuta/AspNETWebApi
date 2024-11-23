using ToDoWebApi.Models;
using Task = System.Threading.Tasks.Task;

namespace ToDoWebApi.Services;

public class CategoryService : IResourceService<CategoryModel>
{
    private readonly SortedDictionary<int, CategoryModel> _categories = new();

    //Implement Interface
}
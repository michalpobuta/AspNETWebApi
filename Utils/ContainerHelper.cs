using ToDoWebApi.Models;
using ToDoWebApi.Services;

namespace ToDoWebApi.Utils;

public static class ContainerHelper
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddSingleton<IResourceService<CategoryModel>, CategoryService>();
        services.AddSingleton<IResourceService<TaskModel>, TaskService>();

        return services;
    }
}

using ToDoWebApi.Models;

namespace ToDoWebApi.Services;

public class UserService : IResourceService<UserModel>
{
    private readonly SortedDictionary<int, UserModel> _users = new();
    
    public Task<UserModel> Create(UserModel resource)
    {
        if (_users.TryAdd(resource.Id, resource))
            return Task.FromResult(resource);


        throw new Exception("Resource already exist") ;
    }

    public Task<UserModel> GetResourceById(int id)
    {
        if (_users.TryGetValue(id, out var task))
            return Task.FromResult(task);
        throw new KeyNotFoundException();
    }

    public Task<IList<UserModel>> GetResources()
    { 
        return Task.FromResult<IList<UserModel>>(_users.Values.ToList());
    }

    public Task Update(UserModel resource)
    {
        if (!_users.ContainsKey(resource.Id))
            throw new KeyNotFoundException();

        _users[resource.Id] = resource;
        
        return Task.CompletedTask;
    }

    public Task Delete(UserModel resource)
    {
        if (!_users.ContainsKey(resource.Id))
            throw new KeyNotFoundException();

        _users.Remove(resource.Id);
        
        return Task.CompletedTask;
    }
}
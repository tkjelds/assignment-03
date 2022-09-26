namespace Assignment3.Entities;

public class UserRepository : IUserRepository
{
    private readonly KanbanContext _context;

    public UserRepository(KanbanContext context)
    {
        _context = context;
    }
    public (Response Response, int UserId) Create(UserCreateDTO user)
    {
        try{
        // Add new user to context
        _context.Users.Add(new User{
            Name = user.Name,
            Email = user.Email
        });
        // Saver changes
        _context.SaveChanges();
        // return
        return (Response.Created,_context.Users.Where(u => user.Email == u.Email).First().Id);
        } catch {return (Response.Conflict,-1);}

    }

    public Response Delete(int userId, bool force = false)
    {
        // Check if force is used
        if(force != true) return Response.Conflict;
        // Check if user exists
        var _user = _context.Users.Where(u => u.Id == userId).First();
        if(_user == null) return (Response.Conflict);
        _context.Users.Remove(_user);
        _context.SaveChanges();
        return Response.Deleted;
    }

    public UserDTO Read(int userId)
    {
        // Check if user exists
        var _user = _context.Users.Where(u => u.Id == userId).First();
        if(_user == null) return null;
        return (new UserDTO(            
            _user.Id,
            _user.Name,
            _user.Email)

        );
    }

    public IReadOnlyCollection<UserDTO> ReadAll()
    {
        List<UserDTO> userDTOs = new List<UserDTO> {};
        foreach (var user in _context.Users)
        {
            userDTOs.Add(new UserDTO(user.Id,user.Name,user.Email));
        }
        return userDTOs;
    }

    public Response Update(UserUpdateDTO user)
    {
        // Check if user exists
        var _user = _context.Users.Where(u => u.Id == user.Id).First();
        if(_user == null) return Response.Conflict;
        _user.Email = user.Email;
        _user.Name = user.Name;
        _context.Users.Update(_user);
        _context.SaveChanges();
        return Response.Updated;
    }
}

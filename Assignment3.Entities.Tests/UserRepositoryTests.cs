namespace Assignment3.Entities.Tests;

public class UserRepositoryTests
{
    [Fact]
    public void UserRepoCreateTest()
    {
        // Given
        KanbanContext _context = new KanbanContext();
        UserRepository tr = new UserRepository(_context);
        _context.Users.RemoveRange(_context.Users);
        UserCreateDTO userCreateDTO = new UserCreateDTO("test","test@test.com");
        // When
        var actual = tr.Create(userCreateDTO);
        // Then
        Assert.Equal(Response.Created,  actual.Response);
    }
    [Fact]
    public void UserRepoDeleteWhenForceTest()
    {
        // Given
        KanbanContext _context = new KanbanContext();
        UserRepository tr = new UserRepository(_context);
        _context.Users.RemoveRange(_context.Users);
        UserCreateDTO userCreateDTO = new UserCreateDTO("test","test@test.com");
        // When
        var user = tr.Create(userCreateDTO);
        var actual = tr.Delete(user.UserId,true);
        // Then
        Assert.Equal(Response.Deleted,actual);
    }
    [Fact]
    public void UserRepoDeleteWhenNoForceTest()
    {
        // Given
        KanbanContext _context = new KanbanContext();
        UserRepository tr = new UserRepository(_context);
        _context.Users.RemoveRange(_context.Users);
        UserCreateDTO userCreateDTO = new UserCreateDTO("test","test@test.com");
        // When
        var user = tr.Create(userCreateDTO);
        var actual = tr.Delete(user.UserId,false);
        // Then
        Assert.Equal(Response.Conflict,actual);
    }
    [Fact]
    public void UserRepoReadTest()
    {
        // Given
        KanbanContext _context = new KanbanContext();
        UserRepository tr = new UserRepository(_context);
        _context.Users.RemoveRange(_context.Users);
        UserCreateDTO userCreateDTO = new UserCreateDTO("test","test@test.com");
        // When
        var user = tr.Create(userCreateDTO);
        var actual = tr.Read(user.UserId);
        // Then
        Assert.Equal("test@test.com",actual.Email);
    }
    [Fact]
    public void UserRepoReadFailTest()
    {
        // Given
        KanbanContext _context = new KanbanContext();
        UserRepository tr = new UserRepository(_context);
        _context.Users.RemoveRange(_context.Users);
        // When
        var actual = tr.Read(-1);
        // Then
        Assert.Equal(null,actual);
    }
    [Fact]
    public void UserRepoUpdateTest()
    {
        // Given
        KanbanContext _context = new KanbanContext();
        UserRepository tr = new UserRepository(_context);
        _context.Users.RemoveRange(_context.Users);
        UserCreateDTO userCreateDTO = new UserCreateDTO("ChangeMe","test@test.com");
        // When
        var user = tr.Create(userCreateDTO);
        var actual = tr.Update(new UserUpdateDTO(user.UserId,"I am changed","test@test.com"));
        var changedUser = tr.Read(user.UserId);
        // Then
        Assert.Equal("I am changed",changedUser.Name);
    }
}

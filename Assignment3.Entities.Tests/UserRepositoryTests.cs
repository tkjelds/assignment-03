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
}
